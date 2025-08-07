using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000A0E RID: 2574
public class SneezeMonitor : GameStateMachine<SneezeMonitor, SneezeMonitor.Instance>
{
	// Token: 0x06004AEF RID: 19183 RVA: 0x001B26DC File Offset: 0x001B08DC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.ParamTransition<bool>(this.isSneezy, this.sneezy, (SneezeMonitor.Instance smi, bool p) => p);
		this.sneezy.ParamTransition<bool>(this.isSneezy, this.idle, (SneezeMonitor.Instance smi, bool p) => !p).ToggleReactable((SneezeMonitor.Instance smi) => smi.GetReactable());
	}

	// Token: 0x0400319C RID: 12700
	public StateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.BoolParameter isSneezy = new StateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.BoolParameter(false);

	// Token: 0x0400319D RID: 12701
	public GameStateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x0400319E RID: 12702
	public GameStateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.State taking_medicine;

	// Token: 0x0400319F RID: 12703
	public GameStateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.State sneezy;

	// Token: 0x040031A0 RID: 12704
	public const float SINGLE_SNEEZE_TIME_MINOR = 140f;

	// Token: 0x040031A1 RID: 12705
	public const float SINGLE_SNEEZE_TIME_MAJOR = 70f;

	// Token: 0x040031A2 RID: 12706
	public const float SNEEZE_TIME_VARIANCE = 0.3f;

	// Token: 0x040031A3 RID: 12707
	public const float SHORT_SNEEZE_THRESHOLD = 5f;

	// Token: 0x02001AA2 RID: 6818
	public new class Instance : GameStateMachine<SneezeMonitor, SneezeMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A459 RID: 42073 RVA: 0x003A612C File Offset: 0x003A432C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.sneezyness = Db.Get().Attributes.Sneezyness.Lookup(master.gameObject);
			this.OnSneezyChange();
			AttributeInstance attributeInstance = this.sneezyness;
			attributeInstance.OnDirty = (global::System.Action)Delegate.Combine(attributeInstance.OnDirty, new global::System.Action(this.OnSneezyChange));
		}

		// Token: 0x0600A45A RID: 42074 RVA: 0x003A618D File Offset: 0x003A438D
		public override void StopSM(string reason)
		{
			AttributeInstance attributeInstance = this.sneezyness;
			attributeInstance.OnDirty = (global::System.Action)Delegate.Remove(attributeInstance.OnDirty, new global::System.Action(this.OnSneezyChange));
			base.StopSM(reason);
		}

		// Token: 0x0600A45B RID: 42075 RVA: 0x003A61C0 File Offset: 0x003A43C0
		public float NextSneezeInterval()
		{
			if (this.sneezyness.GetTotalValue() <= 0f)
			{
				return 70f;
			}
			float num = (this.IsMinorSneeze() ? 140f : 70f) / this.sneezyness.GetTotalValue();
			return global::UnityEngine.Random.Range(num * 0.7f, num * 1.3f);
		}

		// Token: 0x0600A45C RID: 42076 RVA: 0x003A6219 File Offset: 0x003A4419
		public bool IsMinorSneeze()
		{
			return this.sneezyness.GetTotalValue() <= 5f;
		}

		// Token: 0x0600A45D RID: 42077 RVA: 0x003A6230 File Offset: 0x003A4430
		private void OnSneezyChange()
		{
			base.smi.sm.isSneezy.Set(this.sneezyness.GetTotalValue() > 0f, base.smi, false);
		}

		// Token: 0x0600A45E RID: 42078 RVA: 0x003A6264 File Offset: 0x003A4464
		public Reactable GetReactable()
		{
			float num = this.NextSneezeInterval();
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "Sneeze", Db.Get().ChoreTypes.Cough, 0f, num, float.PositiveInfinity, 0f);
			string text = "sneeze";
			string text2 = "sneeze_pst";
			Emote emote = Db.Get().Emotes.Minion.Sneeze;
			if (this.IsMinorSneeze())
			{
				text = "sneeze_short";
				text2 = "sneeze_short_pst";
				emote = Db.Get().Emotes.Minion.Sneeze_Short;
			}
			selfEmoteReactable.SetEmote(emote);
			return selfEmoteReactable.RegisterEmoteStepCallbacks(text, new Action<GameObject>(this.TriggerDisurbance), null).RegisterEmoteStepCallbacks(text2, null, new Action<GameObject>(this.ResetSneeze));
		}

		// Token: 0x0600A45F RID: 42079 RVA: 0x003A6333 File Offset: 0x003A4533
		private void TriggerDisurbance(GameObject go)
		{
			if (this.IsMinorSneeze())
			{
				AcousticDisturbance.Emit(go, 2);
				return;
			}
			AcousticDisturbance.Emit(go, 3);
		}

		// Token: 0x0600A460 RID: 42080 RVA: 0x003A634C File Offset: 0x003A454C
		private void ResetSneeze(GameObject go)
		{
			base.smi.GoTo(base.sm.idle);
		}

		// Token: 0x04008051 RID: 32849
		private AttributeInstance sneezyness;

		// Token: 0x04008052 RID: 32850
		private StatusItem statusItem;
	}
}
