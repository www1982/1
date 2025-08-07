using System;

// Token: 0x02000471 RID: 1137
public class BeOfflineChore : Chore<BeOfflineChore.StatesInstance>
{
	// Token: 0x060017EE RID: 6126 RVA: 0x000849A4 File Offset: 0x00082BA4
	public static string GetPowerDownAnimPre(BeOfflineChore.StatesInstance smi)
	{
		NavType currentNavType = smi.gameObject.GetComponent<Navigator>().CurrentNavType;
		if (currentNavType == NavType.Ladder || currentNavType == NavType.Pole)
		{
			return "ladder_power_down";
		}
		return "power_down";
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x000849D8 File Offset: 0x00082BD8
	public static string GetPowerDownAnimLoop(BeOfflineChore.StatesInstance smi)
	{
		NavType currentNavType = smi.gameObject.GetComponent<Navigator>().CurrentNavType;
		if (currentNavType == NavType.Ladder || currentNavType == NavType.Pole)
		{
			return "ladder_power_down_idle";
		}
		return "power_down_idle";
	}

	// Token: 0x060017F0 RID: 6128 RVA: 0x00084A0C File Offset: 0x00082C0C
	public BeOfflineChore(IStateMachineTarget master)
		: base(Db.Get().ChoreTypes.BeOffline, master, master.GetComponent<ChoreProvider>(), true, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new BeOfflineChore.StatesInstance(this);
		this.AddPrecondition(ChorePreconditions.instance.NotInTube, null);
	}

	// Token: 0x04000DCD RID: 3533
	public const string EFFECT_NAME = "BionicOffline";

	// Token: 0x02001261 RID: 4705
	public class StatesInstance : GameStateMachine<BeOfflineChore.States, BeOfflineChore.StatesInstance, BeOfflineChore, object>.GameInstance
	{
		// Token: 0x06008619 RID: 34329 RVA: 0x0033B3B1 File Offset: 0x003395B1
		public StatesInstance(BeOfflineChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02001262 RID: 4706
	public class States : GameStateMachine<BeOfflineChore.States, BeOfflineChore.StatesInstance, BeOfflineChore>
	{
		// Token: 0x0600861A RID: 34330 RVA: 0x0033B3BC File Offset: 0x003395BC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleAnims("anim_bionic_kanim", 0f).ToggleStatusItem(Db.Get().DuplicantStatusItems.BionicOfflineIncapacitated, (BeOfflineChore.StatesInstance smi) => smi.master.gameObject.GetSMI<BionicBatteryMonitor.Instance>()).ToggleEffect("BionicOffline")
				.PlayAnim(new Func<BeOfflineChore.StatesInstance, string>(BeOfflineChore.GetPowerDownAnimPre), KAnim.PlayMode.Once)
				.QueueAnim(new Func<BeOfflineChore.StatesInstance, string>(BeOfflineChore.GetPowerDownAnimLoop), true, null);
		}
	}
}
