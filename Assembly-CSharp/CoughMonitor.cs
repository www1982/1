using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020009E0 RID: 2528
public class CoughMonitor : GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>
{
	// Token: 0x06004A07 RID: 18951 RVA: 0x001ACCB0 File Offset: 0x001AAEB0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.EventHandler(GameHashes.PoorAirQuality, new GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.GameEvent.Callback(this.OnBreatheDirtyAir)).ParamTransition<bool>(this.shouldCough, this.coughing, (CoughMonitor.Instance smi, bool bShouldCough) => bShouldCough);
		this.coughing.ToggleStatusItem(Db.Get().DuplicantStatusItems.Coughing, null).ToggleReactable((CoughMonitor.Instance smi) => smi.GetReactable()).ParamTransition<bool>(this.shouldCough, this.idle, (CoughMonitor.Instance smi, bool bShouldCough) => !bShouldCough);
	}

	// Token: 0x06004A08 RID: 18952 RVA: 0x001ACD8C File Offset: 0x001AAF8C
	private void OnBreatheDirtyAir(CoughMonitor.Instance smi, object data)
	{
		float timeInCycles = GameClock.Instance.GetTimeInCycles();
		if (timeInCycles > 0.1f && timeInCycles - smi.lastCoughTime <= 0.1f)
		{
			return;
		}
		float num = (float)data;
		float num2 = ((smi.lastConsumeTime <= 0f) ? 0f : (timeInCycles - smi.lastConsumeTime));
		smi.lastConsumeTime = timeInCycles;
		smi.amountConsumed -= 0.05f * num2;
		smi.amountConsumed = Mathf.Max(smi.amountConsumed, 0f);
		smi.amountConsumed += num;
		if (smi.amountConsumed >= 1f)
		{
			this.shouldCough.Set(true, smi, false);
			smi.lastConsumeTime = 0f;
			smi.amountConsumed = 0f;
		}
	}

	// Token: 0x040030D0 RID: 12496
	private const float amountToCough = 1f;

	// Token: 0x040030D1 RID: 12497
	private const float decayRate = 0.05f;

	// Token: 0x040030D2 RID: 12498
	private const float coughInterval = 0.1f;

	// Token: 0x040030D3 RID: 12499
	public GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.State idle;

	// Token: 0x040030D4 RID: 12500
	public GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.State coughing;

	// Token: 0x040030D5 RID: 12501
	public StateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.BoolParameter shouldCough = new StateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.BoolParameter(false);

	// Token: 0x02001A28 RID: 6696
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A29 RID: 6697
	public new class Instance : GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.GameInstance
	{
		// Token: 0x0600A26B RID: 41579 RVA: 0x003A119E File Offset: 0x0039F39E
		public Instance(IStateMachineTarget master, CoughMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A26C RID: 41580 RVA: 0x003A11A8 File Offset: 0x0039F3A8
		public Reactable GetReactable()
		{
			Emote cough_Small = Db.Get().Emotes.Minion.Cough_Small;
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "BadAirCough", Db.Get().ChoreTypes.Cough, 0f, 0f, float.PositiveInfinity, 0f);
			selfEmoteReactable.SetEmote(cough_Small);
			selfEmoteReactable.preventChoreInterruption = true;
			return selfEmoteReactable.RegisterEmoteStepCallbacks("react_small", null, new Action<GameObject>(this.FinishedCoughing));
		}

		// Token: 0x0600A26D RID: 41581 RVA: 0x003A1234 File Offset: 0x0039F434
		private void FinishedCoughing(GameObject cougher)
		{
			cougher.GetComponent<Effects>().Add("ContaminatedLungs", true);
			base.sm.shouldCough.Set(false, base.smi, false);
			base.smi.lastCoughTime = GameClock.Instance.GetTimeInCycles();
		}

		// Token: 0x04007ED6 RID: 32470
		[Serialize]
		public float lastCoughTime;

		// Token: 0x04007ED7 RID: 32471
		[Serialize]
		public float lastConsumeTime;

		// Token: 0x04007ED8 RID: 32472
		[Serialize]
		public float amountConsumed;
	}
}
