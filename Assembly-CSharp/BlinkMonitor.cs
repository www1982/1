using System;
using UnityEngine;

// Token: 0x020009D9 RID: 2521
public class BlinkMonitor : GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>
{
	// Token: 0x060049DA RID: 18906 RVA: 0x001ABCA8 File Offset: 0x001A9EA8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.Enter(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State.Callback(BlinkMonitor.CreateEyes)).Exit(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State.Callback(BlinkMonitor.DestroyEyes));
		this.satisfied.ScheduleGoTo(new Func<BlinkMonitor.Instance, float>(BlinkMonitor.GetRandomBlinkTime), this.blinking);
		this.blinking.EnterTransition(this.satisfied, GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.Not(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.Transition.ConditionCallback(BlinkMonitor.CanBlink))).Enter(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State.Callback(BlinkMonitor.BeginBlinking)).Update(new Action<BlinkMonitor.Instance, float>(BlinkMonitor.UpdateBlinking), UpdateRate.RENDER_EVERY_TICK, false)
			.Target(this.eyes)
			.OnAnimQueueComplete(this.satisfied)
			.Exit(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State.Callback(BlinkMonitor.EndBlinking));
	}

	// Token: 0x060049DB RID: 18907 RVA: 0x001ABD72 File Offset: 0x001A9F72
	private static bool CanBlink(BlinkMonitor.Instance smi)
	{
		return SpeechMonitor.IsAllowedToPlaySpeech(smi.gameObject) && smi.Get<Navigator>().CurrentNavType != NavType.Ladder;
	}

	// Token: 0x060049DC RID: 18908 RVA: 0x001ABD94 File Offset: 0x001A9F94
	private static float GetRandomBlinkTime(BlinkMonitor.Instance smi)
	{
		return global::UnityEngine.Random.Range(TuningData<BlinkMonitor.Tuning>.Get().randomBlinkIntervalMin, TuningData<BlinkMonitor.Tuning>.Get().randomBlinkIntervalMax);
	}

	// Token: 0x060049DD RID: 18909 RVA: 0x001ABDB0 File Offset: 0x001A9FB0
	private static void CreateEyes(BlinkMonitor.Instance smi)
	{
		smi.eyes = Util.KInstantiate(Assets.GetPrefab(EyeAnimation.ID), null, null).GetComponent<KBatchedAnimController>();
		smi.eyes.gameObject.SetActive(true);
		smi.sm.eyes.Set(smi.eyes.gameObject, smi, false);
	}

	// Token: 0x060049DE RID: 18910 RVA: 0x001ABE0D File Offset: 0x001AA00D
	private static void DestroyEyes(BlinkMonitor.Instance smi)
	{
		if (smi.eyes != null)
		{
			Util.KDestroyGameObject(smi.eyes);
			smi.eyes = null;
		}
	}

	// Token: 0x060049DF RID: 18911 RVA: 0x001ABE2F File Offset: 0x001AA02F
	public static void BeginBlinking(BlinkMonitor.Instance smi)
	{
		smi.eyes.Play(smi.eye_anim, KAnim.PlayMode.Once, 1f, 0f);
		BlinkMonitor.UpdateBlinking(smi, 0f);
	}

	// Token: 0x060049E0 RID: 18912 RVA: 0x001ABE5D File Offset: 0x001AA05D
	public static void EndBlinking(BlinkMonitor.Instance smi)
	{
		smi.GetComponent<SymbolOverrideController>().RemoveSymbolOverride(BlinkMonitor.HASH_SNAPTO_EYES, 3);
	}

	// Token: 0x060049E1 RID: 18913 RVA: 0x001ABE74 File Offset: 0x001AA074
	public static void UpdateBlinking(BlinkMonitor.Instance smi, float dt)
	{
		int currentFrameIndex = smi.eyes.GetCurrentFrameIndex();
		KAnimBatch batch = smi.eyes.GetBatch();
		if (currentFrameIndex == -1 || batch == null)
		{
			return;
		}
		KAnim.Anim.Frame frame;
		if (!smi.eyes.GetBatch().group.data.TryGetFrame(currentFrameIndex, out frame))
		{
			return;
		}
		HashedString hashedString = HashedString.Invalid;
		for (int i = 0; i < frame.numElements; i++)
		{
			int num = frame.firstElementIdx + i;
			if (num < batch.group.data.frameElements.Count)
			{
				KAnim.Anim.FrameElement frameElement = batch.group.data.frameElements[num];
				if (!(frameElement.symbol == HashedString.Invalid))
				{
					hashedString = frameElement.symbol;
					break;
				}
			}
		}
		smi.GetComponent<SymbolOverrideController>().AddSymbolOverride(BlinkMonitor.HASH_SNAPTO_EYES, smi.eyes.AnimFiles[0].GetData().build.GetSymbol(hashedString), 3);
	}

	// Token: 0x040030B2 RID: 12466
	public GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State satisfied;

	// Token: 0x040030B3 RID: 12467
	public GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State blinking;

	// Token: 0x040030B4 RID: 12468
	public StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.TargetParameter eyes;

	// Token: 0x040030B5 RID: 12469
	private static HashedString HASH_SNAPTO_EYES = "snapto_eyes";

	// Token: 0x02001A14 RID: 6676
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A15 RID: 6677
	public class Tuning : TuningData<BlinkMonitor.Tuning>
	{
		// Token: 0x04007EA2 RID: 32418
		public float randomBlinkIntervalMin;

		// Token: 0x04007EA3 RID: 32419
		public float randomBlinkIntervalMax;
	}

	// Token: 0x02001A16 RID: 6678
	public new class Instance : GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.GameInstance
	{
		// Token: 0x0600A225 RID: 41509 RVA: 0x003A09EE File Offset: 0x0039EBEE
		public Instance(IStateMachineTarget master, BlinkMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A226 RID: 41510 RVA: 0x003A09F8 File Offset: 0x0039EBF8
		public bool IsBlinking()
		{
			return base.IsInsideState(base.sm.blinking);
		}

		// Token: 0x0600A227 RID: 41511 RVA: 0x003A0A0B File Offset: 0x0039EC0B
		public void Blink()
		{
			this.GoTo(base.sm.blinking);
		}

		// Token: 0x04007EA4 RID: 32420
		public KBatchedAnimController eyes;

		// Token: 0x04007EA5 RID: 32421
		public string eye_anim;
	}
}
