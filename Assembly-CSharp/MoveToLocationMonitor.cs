using System;
using STRINGS;

// Token: 0x020009FD RID: 2557
public class MoveToLocationMonitor : GameStateMachine<MoveToLocationMonitor, MoveToLocationMonitor.Instance, IStateMachineTarget, MoveToLocationMonitor.Def>
{
	// Token: 0x06004A84 RID: 19076 RVA: 0x001AFCE4 File Offset: 0x001ADEE4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.DoNothing();
		this.moving.ToggleChore((MoveToLocationMonitor.Instance smi) => new MoveChore(smi.master, Db.Get().ChoreTypes.MoveTo, (MoveChore.StatesInstance smii) => smi.targetCell, false), this.satisfied);
	}

	// Token: 0x0400313C RID: 12604
	public GameStateMachine<MoveToLocationMonitor, MoveToLocationMonitor.Instance, IStateMachineTarget, MoveToLocationMonitor.Def>.State satisfied;

	// Token: 0x0400313D RID: 12605
	public GameStateMachine<MoveToLocationMonitor, MoveToLocationMonitor.Instance, IStateMachineTarget, MoveToLocationMonitor.Def>.State moving;

	// Token: 0x02001A75 RID: 6773
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007FD2 RID: 32722
		public Tag[] invalidTagsForMoveTo = new Tag[0];
	}

	// Token: 0x02001A76 RID: 6774
	public new class Instance : GameStateMachine<MoveToLocationMonitor, MoveToLocationMonitor.Instance, IStateMachineTarget, MoveToLocationMonitor.Def>.GameInstance
	{
		// Token: 0x0600A3A3 RID: 41891 RVA: 0x003A4664 File Offset: 0x003A2864
		public Instance(IStateMachineTarget master, MoveToLocationMonitor.Def def)
			: base(master, def)
		{
			master.Subscribe(493375141, new Action<object>(this.OnRefreshUserMenu));
			this.kPrefabID = base.GetComponent<KPrefabID>();
		}

		// Token: 0x0600A3A4 RID: 41892 RVA: 0x003A4694 File Offset: 0x003A2894
		private void OnRefreshUserMenu(object data)
		{
			if (this.kPrefabID.HasAnyTags(base.def.invalidTagsForMoveTo))
			{
				return;
			}
			Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_control", UI.USERMENUACTIONS.MOVETOLOCATION.NAME, new global::System.Action(this.OnClickMoveToLocation), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MOVETOLOCATION.TOOLTIP, true), 0.2f);
		}

		// Token: 0x0600A3A5 RID: 41893 RVA: 0x003A4707 File Offset: 0x003A2907
		private void OnClickMoveToLocation()
		{
			MoveToLocationTool.Instance.Activate(base.GetComponent<Navigator>());
		}

		// Token: 0x0600A3A6 RID: 41894 RVA: 0x003A4719 File Offset: 0x003A2919
		public void MoveToLocation(int cell)
		{
			this.targetCell = cell;
			base.smi.GoTo(base.smi.sm.satisfied);
			base.smi.GoTo(base.smi.sm.moving);
		}

		// Token: 0x0600A3A7 RID: 41895 RVA: 0x003A4758 File Offset: 0x003A2958
		public override void StopSM(string reason)
		{
			base.master.Unsubscribe(493375141, new Action<object>(this.OnRefreshUserMenu));
			base.StopSM(reason);
		}

		// Token: 0x04007FD3 RID: 32723
		public int targetCell;

		// Token: 0x04007FD4 RID: 32724
		private KPrefabID kPrefabID;
	}
}
