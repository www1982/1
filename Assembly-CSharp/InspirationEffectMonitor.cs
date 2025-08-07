using System;
using UnityEngine;

// Token: 0x020009F7 RID: 2551
public class InspirationEffectMonitor : GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>
{
	// Token: 0x06004A6A RID: 19050 RVA: 0x001AF328 File Offset: 0x001AD528
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.EventHandler(GameHashes.CatchyTune, new GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.GameEvent.Callback(this.OnCatchyTune)).ParamTransition<bool>(this.shouldCatchyTune, this.catchyTune, (InspirationEffectMonitor.Instance smi, bool shouldCatchyTune) => shouldCatchyTune);
		this.catchyTune.Exit(delegate(InspirationEffectMonitor.Instance smi)
		{
			this.shouldCatchyTune.Set(false, smi, false);
		}).ToggleEffect("HeardJoySinger").ToggleThought(Db.Get().Thoughts.CatchyTune, null)
			.EventHandler(GameHashes.StartWork, new GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.GameEvent.Callback(this.TryThinkCatchyTune))
			.ToggleStatusItem(Db.Get().DuplicantStatusItems.JoyResponse_HeardJoySinger, null)
			.Enter(delegate(InspirationEffectMonitor.Instance smi)
			{
				this.SingCatchyTune(smi);
			})
			.Update(delegate(InspirationEffectMonitor.Instance smi, float dt)
			{
				this.TryThinkCatchyTune(smi, null);
				this.inspirationTimeRemaining.Delta(-dt, smi);
			}, UpdateRate.SIM_4000ms, false)
			.ParamTransition<float>(this.inspirationTimeRemaining, this.idle, (InspirationEffectMonitor.Instance smi, float p) => p <= 0f);
	}

	// Token: 0x06004A6B RID: 19051 RVA: 0x001AF447 File Offset: 0x001AD647
	private void OnCatchyTune(InspirationEffectMonitor.Instance smi, object data)
	{
		this.inspirationTimeRemaining.Set(600f, smi, false);
		this.shouldCatchyTune.Set(true, smi, false);
	}

	// Token: 0x06004A6C RID: 19052 RVA: 0x001AF46B File Offset: 0x001AD66B
	private void TryThinkCatchyTune(InspirationEffectMonitor.Instance smi, object data)
	{
		if (global::UnityEngine.Random.Range(1, 101) > 66)
		{
			this.SingCatchyTune(smi);
		}
	}

	// Token: 0x06004A6D RID: 19053 RVA: 0x001AF480 File Offset: 0x001AD680
	private void SingCatchyTune(InspirationEffectMonitor.Instance smi)
	{
		smi.master.gameObject.GetSMI<ThoughtGraph.Instance>().AddThought(Db.Get().Thoughts.CatchyTune);
		if (!smi.GetSpeechMonitor().IsPlayingSpeech() && SpeechMonitor.IsAllowedToPlaySpeech(smi.gameObject))
		{
			smi.GetSpeechMonitor().PlaySpeech(Db.Get().Thoughts.CatchyTune.speechPrefix, Db.Get().Thoughts.CatchyTune.sound);
		}
	}

	// Token: 0x0400312B RID: 12587
	public StateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.BoolParameter shouldCatchyTune;

	// Token: 0x0400312C RID: 12588
	public StateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.FloatParameter inspirationTimeRemaining;

	// Token: 0x0400312D RID: 12589
	public GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.State idle;

	// Token: 0x0400312E RID: 12590
	public GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.State catchyTune;

	// Token: 0x02001A65 RID: 6757
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A66 RID: 6758
	public new class Instance : GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.GameInstance
	{
		// Token: 0x0600A36A RID: 41834 RVA: 0x003A3F2D File Offset: 0x003A212D
		public Instance(IStateMachineTarget master, InspirationEffectMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A36B RID: 41835 RVA: 0x003A3F37 File Offset: 0x003A2137
		public SpeechMonitor.Instance GetSpeechMonitor()
		{
			if (this.speechMonitor == null)
			{
				this.speechMonitor = base.master.gameObject.GetSMI<SpeechMonitor.Instance>();
			}
			return this.speechMonitor;
		}

		// Token: 0x04007F9E RID: 32670
		public SpeechMonitor.Instance speechMonitor;
	}
}
