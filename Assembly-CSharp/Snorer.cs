using System;
using UnityEngine;

// Token: 0x02000B19 RID: 2841
[SkipSaveFileSerialization]
public class Snorer : StateMachineComponent<Snorer.StatesInstance>
{
	// Token: 0x060053C3 RID: 21443 RVA: 0x001E77DA File Offset: 0x001E59DA
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x0400384B RID: 14411
	private static readonly HashedString HeadHash = "snapTo_mouth";

	// Token: 0x02001C1E RID: 7198
	public class StatesInstance : GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.GameInstance
	{
		// Token: 0x0600A9A5 RID: 43429 RVA: 0x003B881D File Offset: 0x003B6A1D
		public StatesInstance(Snorer master)
			: base(master)
		{
		}

		// Token: 0x0600A9A6 RID: 43430 RVA: 0x003B8828 File Offset: 0x003B6A28
		public bool IsSleeping()
		{
			StaminaMonitor.Instance smi = base.master.GetSMI<StaminaMonitor.Instance>();
			return smi != null && smi.IsSleeping();
		}

		// Token: 0x0600A9A7 RID: 43431 RVA: 0x003B884C File Offset: 0x003B6A4C
		public void StartSmallSnore()
		{
			this.snoreHandle = GameScheduler.Instance.Schedule("snorelines", 2f, new Action<object>(this.StartSmallSnoreInternal), null, null);
		}

		// Token: 0x0600A9A8 RID: 43432 RVA: 0x003B8878 File Offset: 0x003B6A78
		private void StartSmallSnoreInternal(object data)
		{
			this.snoreHandle.ClearScheduler();
			bool flag;
			Matrix4x4 symbolTransform = base.smi.master.GetComponent<KBatchedAnimController>().GetSymbolTransform(Snorer.HeadHash, out flag);
			if (flag)
			{
				Vector3 vector = symbolTransform.GetColumn(3);
				vector.z = Grid.GetLayerZ(Grid.SceneLayer.FXFront);
				this.snoreEffect = FXHelpers.CreateEffect("snore_fx_kanim", vector, null, false, Grid.SceneLayer.Front, false);
				this.snoreEffect.destroyOnAnimComplete = true;
				this.snoreEffect.Play("snore", KAnim.PlayMode.Loop, 1f, 0f);
			}
		}

		// Token: 0x0600A9A9 RID: 43433 RVA: 0x003B890E File Offset: 0x003B6B0E
		public void StopSmallSnore()
		{
			this.snoreHandle.ClearScheduler();
			if (this.snoreEffect != null)
			{
				this.snoreEffect.PlayMode = KAnim.PlayMode.Once;
			}
			this.snoreEffect = null;
		}

		// Token: 0x0600A9AA RID: 43434 RVA: 0x003B893C File Offset: 0x003B6B3C
		public void StartSnoreBGEffect()
		{
			AcousticDisturbance.Emit(base.smi.master.gameObject, 3);
		}

		// Token: 0x0600A9AB RID: 43435 RVA: 0x003B8954 File Offset: 0x003B6B54
		public void StopSnoreBGEffect()
		{
		}

		// Token: 0x04008532 RID: 34098
		private SchedulerHandle snoreHandle;

		// Token: 0x04008533 RID: 34099
		private KBatchedAnimController snoreEffect;

		// Token: 0x04008534 RID: 34100
		private KBatchedAnimController snoreBGEffect;

		// Token: 0x04008535 RID: 34101
		private const float BGEmissionRadius = 3f;
	}

	// Token: 0x02001C1F RID: 7199
	public class States : GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer>
	{
		// Token: 0x0600A9AC RID: 43436 RVA: 0x003B8958 File Offset: 0x003B6B58
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.TagTransition(GameTags.Dead, null, false);
			this.idle.Transition(this.sleeping, (Snorer.StatesInstance smi) => smi.IsSleeping(), UpdateRate.SIM_200ms);
			this.sleeping.DefaultState(this.sleeping.quiet).Enter(delegate(Snorer.StatesInstance smi)
			{
				smi.StartSmallSnore();
			}).Exit(delegate(Snorer.StatesInstance smi)
			{
				smi.StopSmallSnore();
			})
				.Transition(this.idle, (Snorer.StatesInstance smi) => !smi.master.GetSMI<StaminaMonitor.Instance>().IsSleeping(), UpdateRate.SIM_200ms);
			this.sleeping.quiet.Enter("ScheduleNextSnore", delegate(Snorer.StatesInstance smi)
			{
				smi.ScheduleGoTo(this.GetNewInterval(), this.sleeping.snoring);
			});
			this.sleeping.snoring.Enter(delegate(Snorer.StatesInstance smi)
			{
				smi.StartSnoreBGEffect();
			}).ToggleExpression(Db.Get().Expressions.Relief, null).ScheduleGoTo(3f, this.sleeping.quiet)
				.Exit(delegate(Snorer.StatesInstance smi)
				{
					smi.StopSnoreBGEffect();
				});
		}

		// Token: 0x0600A9AD RID: 43437 RVA: 0x003B8ADC File Offset: 0x003B6CDC
		private float GetNewInterval()
		{
			return Mathf.Min(Mathf.Max(Util.GaussianRandom(5f, 1f), 3f), 10f);
		}

		// Token: 0x04008536 RID: 34102
		public GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.State idle;

		// Token: 0x04008537 RID: 34103
		public Snorer.States.SleepStates sleeping;

		// Token: 0x020028AE RID: 10414
		public class SleepStates : GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.State
		{
			// Token: 0x0400B456 RID: 46166
			public GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.State quiet;

			// Token: 0x0400B457 RID: 46167
			public GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.State snoring;
		}
	}
}
