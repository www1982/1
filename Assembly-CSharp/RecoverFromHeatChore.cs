using System;
using UnityEngine;

// Token: 0x02000495 RID: 1173
public class RecoverFromHeatChore : Chore<RecoverFromHeatChore.Instance>
{
	// Token: 0x06001889 RID: 6281 RVA: 0x000890BC File Offset: 0x000872BC
	public RecoverFromHeatChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.RecoverFromHeat, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new RecoverFromHeatChore.Instance(this, target.gameObject);
		HeatImmunityMonitor.Instance chillyBones = target.gameObject.GetSMI<HeatImmunityMonitor.Instance>();
		Func<int> func = () => chillyBones.ShelterCell;
		this.AddPrecondition(ChorePreconditions.instance.CanMoveToDynamicCell, func);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
	}

	// Token: 0x020012AF RID: 4783
	public class States : GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore>
	{
		// Token: 0x06008754 RID: 34644 RVA: 0x003436F0 File Offset: 0x003418F0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approach;
			base.Target(this.entityRecovering);
			this.root.Enter("CreateLocator", delegate(RecoverFromHeatChore.Instance smi)
			{
				smi.CreateLocator();
			}).Enter("UpdateImmunityProvider", delegate(RecoverFromHeatChore.Instance smi)
			{
				smi.UpdateImmunityProvider();
			}).Exit("DestroyLocator", delegate(RecoverFromHeatChore.Instance smi)
			{
				smi.DestroyLocator();
			})
				.Update("UpdateLocator", delegate(RecoverFromHeatChore.Instance smi, float dt)
				{
					smi.UpdateLocator();
				}, UpdateRate.SIM_200ms, true)
				.Update("UpdateHeatImmunityProvider", delegate(RecoverFromHeatChore.Instance smi, float dt)
				{
					smi.UpdateImmunityProvider();
				}, UpdateRate.SIM_200ms, true);
			this.approach.InitializeStates(this.entityRecovering, this.locator, this.recover, null, null, null);
			this.recover.OnTargetLost(this.heatImmunityProvider, null).Enter("AnimOverride", delegate(RecoverFromHeatChore.Instance smi)
			{
				smi.cachedAnimName = RecoverFromHeatChore.States.GetAnimFileName(smi);
				smi.GetComponent<KAnimControllerBase>().AddAnimOverrides(Assets.GetAnim(smi.cachedAnimName), 0f);
			}).Exit(delegate(RecoverFromHeatChore.Instance smi)
			{
				if (smi.cachedAnimName != HashedString.Invalid)
				{
					smi.GetComponent<KAnimControllerBase>().RemoveAnimOverrides(Assets.GetAnim(smi.cachedAnimName));
				}
			})
				.DefaultState(this.recover.pre)
				.ToggleTag(GameTags.RecoveringFromHeat);
			this.recover.pre.Face(this.heatImmunityProvider, 0f).PlayAnim(new Func<RecoverFromHeatChore.Instance, string>(RecoverFromHeatChore.States.GetPreAnimName), KAnim.PlayMode.Once).OnAnimQueueComplete(this.recover.loop);
			this.recover.loop.PlayAnim(new Func<RecoverFromHeatChore.Instance, string>(RecoverFromHeatChore.States.GetLoopAnimName), KAnim.PlayMode.Once).OnAnimQueueComplete(this.recover.pst);
			this.recover.pst.QueueAnim(new Func<RecoverFromHeatChore.Instance, string>(RecoverFromHeatChore.States.GetPstAnimName), false, null).OnAnimQueueComplete(this.complete);
			this.complete.DefaultState(this.complete.evaluate);
			this.complete.evaluate.EnterTransition(this.complete.success, new StateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.Transition.ConditionCallback(RecoverFromHeatChore.States.IsImmunityProviderStillValid)).EnterTransition(this.complete.fail, GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.Not(new StateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.Transition.ConditionCallback(RecoverFromHeatChore.States.IsImmunityProviderStillValid)));
			this.complete.success.Enter(new StateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.State.Callback(RecoverFromHeatChore.States.ApplyHeatImmunityEffect)).ReturnSuccess();
			this.complete.fail.ReturnFailure();
		}

		// Token: 0x06008755 RID: 34645 RVA: 0x003439B0 File Offset: 0x00341BB0
		public static bool IsImmunityProviderStillValid(RecoverFromHeatChore.Instance smi)
		{
			HeatImmunityProvider.Instance lastKnownImmunityProvider = smi.lastKnownImmunityProvider;
			return lastKnownImmunityProvider != null && lastKnownImmunityProvider.CanBeUsed;
		}

		// Token: 0x06008756 RID: 34646 RVA: 0x003439D0 File Offset: 0x00341BD0
		public static void ApplyHeatImmunityEffect(RecoverFromHeatChore.Instance smi)
		{
			HeatImmunityProvider.Instance lastKnownImmunityProvider = smi.lastKnownImmunityProvider;
			if (lastKnownImmunityProvider != null)
			{
				lastKnownImmunityProvider.ApplyImmunityEffect(smi.gameObject, true);
			}
		}

		// Token: 0x06008757 RID: 34647 RVA: 0x003439F4 File Offset: 0x00341BF4
		public static HashedString GetAnimFileName(RecoverFromHeatChore.Instance smi)
		{
			return RecoverFromHeatChore.States.GetAnimFromHeatImmunityProvider(smi, (HeatImmunityProvider.Instance p) => p.GetAnimFileName(smi.sm.entityRecovering.Get(smi)));
		}

		// Token: 0x06008758 RID: 34648 RVA: 0x00343A2A File Offset: 0x00341C2A
		public static string GetPreAnimName(RecoverFromHeatChore.Instance smi)
		{
			return RecoverFromHeatChore.States.GetAnimFromHeatImmunityProvider(smi, (HeatImmunityProvider.Instance p) => p.PreAnimName);
		}

		// Token: 0x06008759 RID: 34649 RVA: 0x00343A51 File Offset: 0x00341C51
		public static string GetLoopAnimName(RecoverFromHeatChore.Instance smi)
		{
			return RecoverFromHeatChore.States.GetAnimFromHeatImmunityProvider(smi, (HeatImmunityProvider.Instance p) => p.LoopAnimName);
		}

		// Token: 0x0600875A RID: 34650 RVA: 0x00343A78 File Offset: 0x00341C78
		public static string GetPstAnimName(RecoverFromHeatChore.Instance smi)
		{
			return RecoverFromHeatChore.States.GetAnimFromHeatImmunityProvider(smi, (HeatImmunityProvider.Instance p) => p.PstAnimName);
		}

		// Token: 0x0600875B RID: 34651 RVA: 0x00343AA0 File Offset: 0x00341CA0
		public static string GetAnimFromHeatImmunityProvider(RecoverFromHeatChore.Instance smi, Func<HeatImmunityProvider.Instance, string> getCallback)
		{
			HeatImmunityProvider.Instance lastKnownImmunityProvider = smi.lastKnownImmunityProvider;
			if (lastKnownImmunityProvider != null)
			{
				return getCallback(lastKnownImmunityProvider);
			}
			return null;
		}

		// Token: 0x0400671C RID: 26396
		public GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.ApproachSubState<IApproachable> approach;

		// Token: 0x0400671D RID: 26397
		public GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.PreLoopPostState recover;

		// Token: 0x0400671E RID: 26398
		public GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.State remove_suit;

		// Token: 0x0400671F RID: 26399
		public RecoverFromHeatChore.States.CompleteStates complete;

		// Token: 0x04006720 RID: 26400
		public StateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.TargetParameter heatImmunityProvider;

		// Token: 0x04006721 RID: 26401
		public StateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.TargetParameter entityRecovering;

		// Token: 0x04006722 RID: 26402
		public StateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.TargetParameter locator;

		// Token: 0x0200266C RID: 9836
		public class CompleteStates : GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.State
		{
			// Token: 0x0400AACD RID: 43725
			public GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.State evaluate;

			// Token: 0x0400AACE RID: 43726
			public GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.State fail;

			// Token: 0x0400AACF RID: 43727
			public GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.State success;
		}
	}

	// Token: 0x020012B0 RID: 4784
	public class Instance : GameStateMachine<RecoverFromHeatChore.States, RecoverFromHeatChore.Instance, RecoverFromHeatChore, object>.GameInstance
	{
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600875D RID: 34653 RVA: 0x00343AC8 File Offset: 0x00341CC8
		public HeatImmunityProvider.Instance lastKnownImmunityProvider
		{
			get
			{
				if (!(base.sm.heatImmunityProvider.Get(this) == null))
				{
					return base.sm.heatImmunityProvider.Get(this).GetSMI<HeatImmunityProvider.Instance>();
				}
				return null;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600875E RID: 34654 RVA: 0x00343AFB File Offset: 0x00341CFB
		public HeatImmunityMonitor.Instance heatImmunityMonitor
		{
			get
			{
				return base.sm.entityRecovering.Get(this).GetSMI<HeatImmunityMonitor.Instance>();
			}
		}

		// Token: 0x0600875F RID: 34655 RVA: 0x00343B14 File Offset: 0x00341D14
		public Instance(RecoverFromHeatChore master, GameObject entityRecovering)
			: base(master)
		{
			base.sm.entityRecovering.Set(entityRecovering, this, false);
			HeatImmunityMonitor.Instance heatImmunityMonitor = this.heatImmunityMonitor;
			if (heatImmunityMonitor.NearestImmunityProvider != null && !heatImmunityMonitor.NearestImmunityProvider.isMasterNull)
			{
				base.sm.heatImmunityProvider.Set(heatImmunityMonitor.NearestImmunityProvider.gameObject, this, false);
			}
		}

		// Token: 0x06008760 RID: 34656 RVA: 0x00343B78 File Offset: 0x00341D78
		public void CreateLocator()
		{
			GameObject gameObject = ChoreHelpers.CreateLocator("RecoverWarmthLocator", Vector3.zero);
			base.sm.locator.Set(gameObject, this, false);
			this.UpdateLocator();
		}

		// Token: 0x06008761 RID: 34657 RVA: 0x00343BB0 File Offset: 0x00341DB0
		public void UpdateImmunityProvider()
		{
			HeatImmunityProvider.Instance nearestImmunityProvider = this.heatImmunityMonitor.NearestImmunityProvider;
			base.sm.heatImmunityProvider.Set((nearestImmunityProvider == null || nearestImmunityProvider.isMasterNull) ? null : nearestImmunityProvider.gameObject, this, false);
		}

		// Token: 0x06008762 RID: 34658 RVA: 0x00343BF0 File Offset: 0x00341DF0
		public void UpdateLocator()
		{
			int num = this.heatImmunityMonitor.ShelterCell;
			if (num == Grid.InvalidCell)
			{
				num = Grid.PosToCell(base.sm.entityRecovering.Get<Transform>(base.smi).GetPosition());
				this.DestroyLocator();
			}
			else
			{
				Vector3 vector = Grid.CellToPosCBC(num, Grid.SceneLayer.Move);
				base.sm.locator.Get<Transform>(base.smi).SetPosition(vector);
			}
			this.targetCell = num;
		}

		// Token: 0x06008763 RID: 34659 RVA: 0x00343C67 File Offset: 0x00341E67
		public void DestroyLocator()
		{
			ChoreHelpers.DestroyLocator(base.sm.locator.Get(this));
			base.sm.locator.Set(null, this);
		}

		// Token: 0x04006723 RID: 26403
		private int targetCell;

		// Token: 0x04006724 RID: 26404
		public HashedString cachedAnimName;
	}
}
