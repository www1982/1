using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000271 RID: 625
public class LargeImpactorKeepsake : GameStateMachine<LargeImpactorKeepsake, LargeImpactorKeepsake.Instance, IStateMachineTarget, LargeImpactorKeepsake.Def>
{
	// Token: 0x06000CA7 RID: 3239 RVA: 0x0004B8C4 File Offset: 0x00049AC4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.notification;
		this.notification.ParamTransition<bool>(this.HasNotificationBeenAknowledged, this.idle, GameStateMachine<LargeImpactorKeepsake, LargeImpactorKeepsake.Instance, IStateMachineTarget, LargeImpactorKeepsake.Def>.IsTrue).ToggleNotification(new Func<LargeImpactorKeepsake.Instance, Notification>(LargeImpactorKeepsake.GetNotification));
		this.idle.DoNothing();
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x0004B91A File Offset: 0x00049B1A
	public static Notification GetNotification(LargeImpactorKeepsake.Instance smi)
	{
		return smi.notification;
	}

	// Token: 0x0400089C RID: 2204
	private GameStateMachine<LargeImpactorKeepsake, LargeImpactorKeepsake.Instance, IStateMachineTarget, LargeImpactorKeepsake.Def>.State notification;

	// Token: 0x0400089D RID: 2205
	private GameStateMachine<LargeImpactorKeepsake, LargeImpactorKeepsake.Instance, IStateMachineTarget, LargeImpactorKeepsake.Def>.State idle;

	// Token: 0x0400089E RID: 2206
	private StateMachine<LargeImpactorKeepsake, LargeImpactorKeepsake.Instance, IStateMachineTarget, LargeImpactorKeepsake.Def>.BoolParameter HasNotificationBeenAknowledged;

	// Token: 0x0200119C RID: 4508
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200119D RID: 4509
	public new class Instance : GameStateMachine<LargeImpactorKeepsake, LargeImpactorKeepsake.Instance, IStateMachineTarget, LargeImpactorKeepsake.Def>.GameInstance
	{
		// Token: 0x06008314 RID: 33556 RVA: 0x003333C4 File Offset: 0x003315C4
		public Instance(IStateMachineTarget master, LargeImpactorKeepsake.Def def)
			: base(master, def)
		{
			this.notification = this.CreateDeathNotification();
		}

		// Token: 0x06008315 RID: 33557 RVA: 0x003333DC File Offset: 0x003315DC
		private Notification CreateDeathNotification()
		{
			string text = MISC.NOTIFICATIONS.LARGE_IMPACTOR_KEEPSAKE.NAME;
			NotificationType notificationType = NotificationType.Event;
			Func<List<Notification>, object, string> func = (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.LARGE_IMPACTOR_KEEPSAKE.TOOLTIP;
			object obj = null;
			bool flag = false;
			float num = 0f;
			Transform transform = base.gameObject.transform;
			return new Notification(text, notificationType, func, obj, flag, num, new Notification.ClickCallback(this.MarkAsAknowledgedAndFocusCamera), this, transform, true, true, false);
		}

		// Token: 0x06008316 RID: 33558 RVA: 0x00333440 File Offset: 0x00331640
		private void MarkAsAknowledgedAndFocusCamera(object data)
		{
			if (data == null)
			{
				return;
			}
			LargeImpactorKeepsake.Instance instance = (LargeImpactorKeepsake.Instance)data;
			instance.sm.HasNotificationBeenAknowledged.Set(true, instance, false);
			GameUtil.FocusCamera(base.gameObject.transform, true, true);
		}

		// Token: 0x0400638C RID: 25484
		public Notification notification;
	}
}
