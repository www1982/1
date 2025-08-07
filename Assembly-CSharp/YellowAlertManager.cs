using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000A1F RID: 2591
public class YellowAlertManager : GameStateMachine<YellowAlertManager, YellowAlertManager.Instance>
{
	// Token: 0x06004B3B RID: 19259 RVA: 0x001B499C File Offset: 0x001B2B9C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.off.ParamTransition<bool>(this.isOn, this.on, GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.IsTrue);
		this.on.Enter("EnterEvent", delegate(YellowAlertManager.Instance smi)
		{
			Game.Instance.Trigger(-741654735, null);
		}).Exit("ExitEvent", delegate(YellowAlertManager.Instance smi)
		{
			Game.Instance.Trigger(-2062778933, null);
		}).Enter("EnableVignette", delegate(YellowAlertManager.Instance smi)
		{
			Vignette.Instance.SetColor(new Color(1f, 1f, 0f, 0.1f));
		})
			.Exit("DisableVignette", delegate(YellowAlertManager.Instance smi)
			{
				Vignette.Instance.Reset();
			})
			.Enter("Sounds", delegate(YellowAlertManager.Instance smi)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("RedAlert_ON", false));
			})
			.ToggleLoopingSound(GlobalAssets.GetSound("RedAlert_LP", false), null, true, true, true)
			.ToggleNotification((YellowAlertManager.Instance smi) => smi.notification)
			.ParamTransition<bool>(this.isOn, this.off, GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.IsFalse);
		this.on_pst.Enter("Sounds", delegate(YellowAlertManager.Instance smi)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("RedAlert_OFF", false));
		});
	}

	// Token: 0x040031E0 RID: 12768
	public GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040031E1 RID: 12769
	public GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x040031E2 RID: 12770
	public GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.State on_pst;

	// Token: 0x040031E3 RID: 12771
	public StateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.BoolParameter isOn = new StateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.BoolParameter();

	// Token: 0x02001AD2 RID: 6866
	public new class Instance : GameStateMachine<YellowAlertManager, YellowAlertManager.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A549 RID: 42313 RVA: 0x003A8B7F File Offset: 0x003A6D7F
		public static void DestroyInstance()
		{
			YellowAlertManager.Instance.instance = null;
		}

		// Token: 0x0600A54A RID: 42314 RVA: 0x003A8B87 File Offset: 0x003A6D87
		public static YellowAlertManager.Instance Get()
		{
			return YellowAlertManager.Instance.instance;
		}

		// Token: 0x0600A54B RID: 42315 RVA: 0x003A8B90 File Offset: 0x003A6D90
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			YellowAlertManager.Instance.instance = this;
		}

		// Token: 0x0600A54C RID: 42316 RVA: 0x003A8BEC File Offset: 0x003A6DEC
		public bool IsOn()
		{
			return base.sm.isOn.Get(base.smi);
		}

		// Token: 0x0600A54D RID: 42317 RVA: 0x003A8C04 File Offset: 0x003A6E04
		public void HasTopPriorityChore(bool on)
		{
			this.hasTopPriorityChore = on;
			this.Refresh();
		}

		// Token: 0x0600A54E RID: 42318 RVA: 0x003A8C13 File Offset: 0x003A6E13
		private void Refresh()
		{
			base.sm.isOn.Set(this.hasTopPriorityChore, base.smi, false);
		}

		// Token: 0x04008110 RID: 33040
		private static YellowAlertManager.Instance instance;

		// Token: 0x04008111 RID: 33041
		private bool hasTopPriorityChore;

		// Token: 0x04008112 RID: 33042
		public Notification notification = new Notification(MISC.NOTIFICATIONS.YELLOWALERT.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.YELLOWALERT.TOOLTIP, null, false, 0f, null, null, null, true, false, false);
	}
}
