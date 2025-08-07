import requests
import requests.exceptions
import json
import time
import random
from typing import Optional


BASE_URL = "http://127.0.0.1:8080/api" # 使用 127.0.0.1 避免潜在的 localhost 解析问题

# 全局上下文，用于在测试步骤间传递数据
class TestContext:
    object_id: Optional[int] = None
    world_id: Optional[int] = None
    duplicant_id: Optional[int] = None # 用于寻路测试
    cell_id: Optional[int] = None

def print_header(title):
    """打印漂亮的标题头"""
    print("\n" + "="*60)
    print(f"  {title.upper()}")
    print("="*60)

def make_api_call(endpoint, method="POST", payload=None):
    """通用API调用函数，包含详细的错误处理"""
    # 使用更稳健的方式拼接URL，避免双斜杠或缺少斜杠的问题
    url = f"{BASE_URL.rstrip('/')}/{endpoint.lstrip('/')}"
    print(f"--> {method} {url}")
    if payload:
        print(f"    Payload: {json.dumps(payload)}")

    response = None  # 初始化response以避免未绑定错误
    try:
        if method.upper() == "POST":
            response = requests.post(url, json=payload, timeout=20)
        else:
            response = requests.get(url, timeout=20)
        
        response.raise_for_status()  # 如果状态码是 4xx 或 5xx，则抛出HTTPError

        response_json = response.json()
        if response_json.get("status") == "success":
            print("✔️ 请求成功！")
            data = response_json.get("data")
            # 为了避免打印过多内容，对返回数据进行截断
            data_str = str(data)
            if len(data_str) > 250:
                print(f"    收到数据: {data_str[:250]}...")
            else:
                print(f"    收到数据: {data}")
            return data
        else:
            print(f"❌ API返回逻辑错误: {response_json.get('data')}")
            return None
            
    except requests.exceptions.HTTPError as e:
        print(f"❌ HTTP错误: {e.response.status_code} {e.response.reason}")
        try:
            # 尝试以JSON格式打印错误响应，以便调试
            print(f"    错误详情: {e.response.json()}")
        except json.JSONDecodeError:
            # 如果响应不是JSON，则以纯文本形式打印
            print(f"    错误详情 (非JSON): {e.response.text}")
        return None
    except requests.exceptions.RequestException as e:
        # 捕获连接超时、DNS错误等网络层面的问题
        print(f"❌ 网络连接错误: {e}")
        return None
    except json.JSONDecodeError:
        # 如果服务器返回的不是有效的JSON
        print("❌ 无法解析JSON响应。")
        if response is not None:
            print(f"    收到内容: {response.text}")
        return None


def run_ultimate_test_suite():
    """按逻辑顺序执行完整的测试流程"""

    # --- 阶段 1: 基础连接和状态获取 ---
    print_header("阶段 1: 基础连接与状态获取")
    if make_api_call("/") is None:
        print("测试终止：无法连接到API根路径。")
        return
    state_data = make_api_call("state", method="GET") # 获取全局状态
    if state_data is None:
        print("测试终止：无法获取游戏状态。")
        return

    try: # 从状态中提取测试所需的世界ID
        TestContext.world_id = int(list(state_data['Worlds'].keys())[0])
        print(f"\n成功获取状态！将使用世界 ID: {TestContext.world_id} 进行后续测试。")
    except (KeyError, IndexError):
        print("测试终止：未能从游戏状态中找到任何世界ID。")
        return

    # --- 阶段 2: 对象查找与交互 ---
    print_header("阶段 2: 对象查找、详情与交互")
    find_payload = {"worldId": TestContext.world_id, "componentName": "Storage"}
    object_ids = make_api_call("find_objects", payload=find_payload) # 查找所有储物箱

    if not object_ids:
        print("\n警告：未找到任何储物箱，对象相关的测试将被跳过。")
    else:
        TestContext.object_id = object_ids[0]
        print(f"\n将使用第一个储物箱进行测试，ID: {TestContext.object_id}")
       
        # 获取对象所有白名单详情
        make_api_call("get_object_details", payload={"objectId": TestContext.object_id})
       
        # 调用方法
        call_method_payload = {"objectId": TestContext.object_id, "component": "TreeFilterable", "method": "AddTagToFilter", "params": ["Compostable"]}
        make_api_call("call_method", payload=call_method_payload)

    # 查找复制人，为寻路测试做准备
    find_dupe_payload = {"worldId": TestContext.world_id, "componentName": "Prioritizable"}
    dupe_ids = make_api_call("find_objects", payload=find_dupe_payload)
    if dupe_ids: TestContext.duplicant_id = dupe_ids[0]

    # --- 阶段 3: 网格查询与全局指令 ---
    print_header("阶段 3: 网格查询、预检与全局指令")
   
    # 随机选择一个坐标并转换为 Cell ID
    x, y = random.randint(120, 130), random.randint(120, 130)
    coords_payload = {"x": x, "y": y}
    TestContext.cell_id = make_api_call("util/coords_to_cell", payload=coords_payload)
    if not TestContext.cell_id:
        TestContext.cell_id = 82042 # 设置一个默认值以防失败
        print(f"警告：无法从坐标转换cell_id，使用默认值 {TestContext.cell_id}")

    # 只有在cell_id有效时才执行相关测试
    if TestContext.cell_id is not None:
        cells_to_test = [TestContext.cell_id, TestContext.cell_id + 1, TestContext.cell_id + 256]
    
        # 获取多个格子的详细信息
        make_api_call("grid/get_cells", payload={"cells": cells_to_test})

        # 预检是否可以挖掘
        can_dig_result = make_api_call("precheck/dig", payload={"cells": cells_to_test})
    
        # 如果可以挖掘，则下达挖掘指令
        if can_dig_result and any(can_dig_result.values()):
            diggable_cells = [int(cell) for cell, can in can_dig_result.items() if can]
            make_api_call("dig", payload={"cells": diggable_cells}) # 挖掘
            time.sleep(1)

        # 预检是否可以建造
        if make_api_call("precheck/build", payload={"buildingId": "GasReservoir", "cell": TestContext.cell_id}):
            make_api_call("build", payload={"buildingId": "GasReservoir", "cell": TestContext.cell_id}) # 建造
            time.sleep(1)

        # 获取任务状态
        make_api_call("chores/get_status", payload={"cells": cells_to_test})
    else:
        print("\n跳过所有基于cell的测试，因为cell_id无效。")

    # --- 阶段 4: 寻路与研究 ---
    print_header("阶段 4: 寻路与研究")
    if TestContext.duplicant_id: # 寻路测试
        path_payload = {"navigatorId": TestContext.duplicant_id, "targetCell": TestContext.cell_id}
        make_api_call("pathfinding/get_path", payload=path_payload)
    else:
        print("\n跳过寻路测试，因为没有找到复制人。")

    make_api_call("research", payload={"techId": "AdvancedResearch"}) # 设置研究
    time.sleep(1)
    make_api_call("research", payload={"cancel": True}) # 取消研究

    # --- 阶段 5: 安全性测试 ---
    print_header("阶段 5: 安全性测试")
    if TestContext.object_id: # 尝试访问未授权的组件
        denied_payload = {"objectId": TestContext.object_id, "component": "Building", "property": "Def"}
        print("\n预期API会返回一个'access denied'错误...")
        make_api_call("get_property", payload=denied_payload)
    else:
        print("\n跳过安全性测试，因为没有找到可用的测试对象。")

    print_header("所有测试执行完毕")

if __name__ == "__main__":
    run_ultimate_test_suite()
