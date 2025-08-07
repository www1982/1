using System;

// Token: 0x02000A07 RID: 2567
public class RoomMonitor : GameStateMachine<RoomMonitor, RoomMonitor.Instance>
{
	// Token: 0x06004ACB RID: 19147 RVA: 0x001B1975 File Offset: 0x001AFB75
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventHandler(GameHashes.PathAdvanced, new StateMachine<RoomMonitor, RoomMonitor.Instance, IStateMachineTarget, object>.State.Callback(RoomMonitor.UpdateRoomType));
	}

	// Token: 0x06004ACC RID: 19148 RVA: 0x001B199C File Offset: 0x001AFB9C
	private static void UpdateRoomType(RoomMonitor.Instance smi)
	{
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(smi.master.gameObject);
		if (roomOfGameObject != smi.currentRoom)
		{
			smi.currentRoom = roomOfGameObject;
			if (roomOfGameObject != null)
			{
				roomOfGameObject.cavity.OnEnter(smi.master.gameObject);
			}
		}
	}

	// Token: 0x02001A91 RID: 6801
	public new class Instance : GameStateMachine<RoomMonitor, RoomMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A413 RID: 42003 RVA: 0x003A5574 File Offset: 0x003A3774
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0400801E RID: 32798
		public Room currentRoom;
	}
}
