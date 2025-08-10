import asyncio
import json
import uuid
import websockets

class ApiClient:
    def __init__(self, uri):
        self.uri = uri
        self.websocket = None
        self.pending_requests = {}

    async def connect(self):
        self.websocket = await websockets.connect(self.uri)

    async def send_request(self, action, payload):
        if not self.websocket or self.websocket.closed:
            print("Websocket is not connected. Attempting to connect before sending.")
            await self.connect()

        request_id = str(uuid.uuid4())
        message = {
            "action": action,
            "payload": payload,
            "requestId": request_id
        }
        
        future = asyncio.get_event_loop().create_future()
        self.pending_requests[request_id] = future
        
        await self.websocket.send(json.dumps(message))
        
        try:
            response_payload = await asyncio.wait_for(future, timeout=20.0)
            return response_payload
        except asyncio.TimeoutError:
            return {"error": f"Request {request_id} timed out after 20 seconds."}
        finally:
            del self.pending_requests[request_id]

    async def listen_for_responses(self):
        while True:
            if not self.websocket or self.websocket.closed:
                try:
                    await self.connect()
                    print("Command server connection (re)established.")
                except Exception as e:
                    print(f"Failed to connect to command server: {e}. Retrying in 5s...")
                    await asyncio.sleep(5)
                    continue
            
            try:
                response_str = await self.websocket.recv()
                response = json.loads(response_str)
                request_id = response.get("requestId")
                if request_id and request_id in self.pending_requests:
                    future = self.pending_requests[request_id]
                    future.set_result(response.get("payload"))
            except websockets.exceptions.ConnectionClosed:
                print("Command server connection lost. Reconnecting...")
                self.websocket = None # Force reconnection in the next loop
                await asyncio.sleep(1) # Brief pause before retry


async def event_listener():
    uri = "ws://localhost:8181"
    while True:
        try:
            async with websockets.connect(uri) as websocket:
                print("Connected to event server.")
                while True:
                    message_str = await websocket.recv()
                    event = json.loads(message_str)
                    if event.get("type") != "State.Update":
                        print(json.dumps(event, indent=2))
        except (websockets.exceptions.ConnectionClosed, ConnectionRefusedError) as e:
            print(f"Event server connection error: {e}. Reconnecting in 5 seconds...")
            await asyncio.sleep(5)

async def run_full_test_suite(api_client):
    print("\n--- 1. GETTING STATE ---")
    state = await api_client.send_request("State.Get", {})
    world_id = state.get("worldId")
    print(f"World ID: {world_id}")

    print("\n--- 2. FINDING A DUPLICANT ---")
    find_payload = {"type": "Minion"}
    found_objects = await api_client.send_request("FindObjects", find_payload)
    duplicant_id = found_objects[0]['id'] if found_objects else None
    if not duplicant_id:
        print("Could not find a duplicant. Aborting test.")
        return
    print(f"Found Duplicant ID: {duplicant_id}")

    print("\n--- 3. BATCH SETTING PRIORITIES ---")
    priorities_payload = {
        "duplicantIds": [duplicant_id],
        "priorities": [
            {"id": "Art", "priority": {"group": "Major", "value": 5}},
            {"id": "Build", "priority": {"group": "Minor", "value": 1}}
        ]
    }
    await api_client.send_request("Duplicant.BatchSetPriorities", priorities_payload)
    print("Priorities set for duplicant.")

    print("\n--- 4. SETTING LOGISTICS POLICY ---")
    logistics_policy_payload = {
        "id": "food_policy",
        "rules": [{
            "itemType": "Food",
            "destinationType": "RationBox",
            "priority": 10
        }]
    }
    await api_client.send_request("Logistics.SetPolicy", logistics_policy_payload)
    print("Logistics policy for food has been set.")

    print("\n--- 5. DEPLOYING BLUEPRINT ---")
    blueprint_payload = {
        "name": "simple_dig_build",
        "steps": [
            {"type": "Dig", "locations": [{"x": 10, "y": 10}]},
            {"type": "Build", "buildingId": "Ladder", "locations": [{"x": 10, "y": 11}]}
        ]
    }
    await api_client.send_request("Blueprint.Deploy", blueprint_payload)
    print("Simple blueprint deployed.")

    print("\n--- 6. WAITING FOR 10 SECONDS ---")
    await asyncio.sleep(10)
    print("Wait finished.")

    print("\n--- 7. REMOVING LOGISTICS POLICY ---")
    remove_policy_payload = {"id": "food_policy"}
    await api_client.send_request("Logistics.RemovePolicy", remove_policy_payload)
    print("Logistics policy removed.")
    print("\n--- FULL TEST SUITE COMPLETED ---")


async def main():
    api_client = ApiClient("ws://localhost:8080")
    await api_client.connect()

    await asyncio.gather(
        api_client.listen_for_responses(),
        event_listener(),
        run_full_test_suite(api_client)
    )

if __name__ == "__main__":
    try:
        asyncio.run(main())
    except KeyboardInterrupt:
        print("Client stopped by user.")
