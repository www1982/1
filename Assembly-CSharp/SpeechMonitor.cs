using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000A0F RID: 2575
public class SpeechMonitor : GameStateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>
{
	// Token: 0x06004AF1 RID: 19185 RVA: 0x001B2798 File Offset: 0x001B0998
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.Enter(new StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State.Callback(SpeechMonitor.CreateMouth)).Exit(new StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State.Callback(SpeechMonitor.DestroyMouth));
		this.satisfied.DoNothing();
		this.talking.Enter(new StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State.Callback(SpeechMonitor.BeginTalking)).Update(new Action<SpeechMonitor.Instance, float>(SpeechMonitor.UpdateTalking), UpdateRate.RENDER_EVERY_TICK, false).Target(this.mouth)
			.OnAnimQueueComplete(this.satisfied)
			.Exit(new StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State.Callback(SpeechMonitor.EndTalking));
	}

	// Token: 0x06004AF2 RID: 19186 RVA: 0x001B2834 File Offset: 0x001B0A34
	private static void CreateMouth(SpeechMonitor.Instance smi)
	{
		smi.mouth = global::Util.KInstantiate(Assets.GetPrefab(MouthAnimation.ID), null, null).GetComponent<KBatchedAnimController>();
		smi.mouth.gameObject.SetActive(true);
		smi.sm.mouth.Set(smi.mouth.gameObject, smi, false);
		smi.SetMouthId();
	}

	// Token: 0x06004AF3 RID: 19187 RVA: 0x001B2897 File Offset: 0x001B0A97
	private static void DestroyMouth(SpeechMonitor.Instance smi)
	{
		if (smi.mouth != null)
		{
			global::Util.KDestroyGameObject(smi.mouth);
			smi.mouth = null;
		}
	}

	// Token: 0x06004AF4 RID: 19188 RVA: 0x001B28BC File Offset: 0x001B0ABC
	private static string GetRandomSpeechAnim(SpeechMonitor.Instance smi)
	{
		return smi.speechPrefix + global::UnityEngine.Random.Range(1, TuningData<SpeechMonitor.Tuning>.Get().speechCount).ToString() + smi.mouthId;
	}

	// Token: 0x06004AF5 RID: 19189 RVA: 0x001B28F4 File Offset: 0x001B0AF4
	public static bool IsAllowedToPlaySpeech(GameObject go)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		if (component.HasTag(GameTags.Dead) || component.HasTag(GameTags.Incapacitated))
		{
			return false;
		}
		KBatchedAnimController component2 = go.GetComponent<KBatchedAnimController>();
		KAnim.Anim currentAnim = component2.GetCurrentAnim();
		return currentAnim == null || (GameAudioSheets.Get().IsAnimAllowedToPlaySpeech(currentAnim) && SpeechMonitor.CanOverrideHead(component2));
	}

	// Token: 0x06004AF6 RID: 19190 RVA: 0x001B294C File Offset: 0x001B0B4C
	private static bool CanOverrideHead(KBatchedAnimController kbac)
	{
		bool flag = true;
		KAnim.Anim currentAnim = kbac.GetCurrentAnim();
		if (currentAnim == null)
		{
			flag = false;
		}
		else if (currentAnim.animFile.name != SpeechMonitor.GENERIC_CONVO_ANIM_NAME)
		{
			int currentFrameIndex = kbac.GetCurrentFrameIndex();
			KAnim.Anim.Frame frame;
			if (currentFrameIndex <= 0)
			{
				flag = false;
			}
			else if (KAnimBatchManager.Instance().GetBatchGroupData(currentAnim.animFile.animBatchTag).TryGetFrame(currentFrameIndex, out frame) && frame.hasHead)
			{
				flag = false;
			}
		}
		return flag;
	}

	// Token: 0x06004AF7 RID: 19191 RVA: 0x001B29C0 File Offset: 0x001B0BC0
	public static void BeginTalking(SpeechMonitor.Instance smi)
	{
		smi.ev.clearHandle();
		if (smi.voiceEvent != null)
		{
			smi.ev = VoiceSoundEvent.PlayVoice(smi.voiceEvent, smi.GetComponent<KBatchedAnimController>(), 0f, false, false);
		}
		if (smi.ev.isValid())
		{
			smi.mouth.Play(SpeechMonitor.GetRandomSpeechAnim(smi), KAnim.PlayMode.Once, 1f, 0f);
			smi.mouth.Queue(SpeechMonitor.GetRandomSpeechAnim(smi), KAnim.PlayMode.Once, 1f, 0f);
			smi.mouth.Queue(SpeechMonitor.GetRandomSpeechAnim(smi), KAnim.PlayMode.Once, 1f, 0f);
			smi.mouth.Queue(SpeechMonitor.GetRandomSpeechAnim(smi), KAnim.PlayMode.Once, 1f, 0f);
		}
		else
		{
			smi.mouth.Play(SpeechMonitor.GetRandomSpeechAnim(smi), KAnim.PlayMode.Once, 1f, 0f);
			smi.mouth.Queue(SpeechMonitor.GetRandomSpeechAnim(smi), KAnim.PlayMode.Once, 1f, 0f);
		}
		SpeechMonitor.UpdateTalking(smi, 0f);
	}

	// Token: 0x06004AF8 RID: 19192 RVA: 0x001B2AE1 File Offset: 0x001B0CE1
	public static void EndTalking(SpeechMonitor.Instance smi)
	{
		smi.GetComponent<SymbolOverrideController>().RemoveSymbolOverride(SpeechMonitor.HASH_SNAPTO_MOUTH, 3);
	}

	// Token: 0x06004AF9 RID: 19193 RVA: 0x001B2AF8 File Offset: 0x001B0CF8
	public static KAnim.Anim.FrameElement GetFirstFrameElement(KBatchedAnimController controller)
	{
		KAnim.Anim.FrameElement frameElement = default(KAnim.Anim.FrameElement);
		frameElement.symbol = HashedString.Invalid;
		int currentFrameIndex = controller.GetCurrentFrameIndex();
		KAnimBatch batch = controller.GetBatch();
		if (currentFrameIndex == -1 || batch == null)
		{
			return frameElement;
		}
		KAnim.Anim.Frame frame;
		if (!controller.GetBatch().group.data.TryGetFrame(currentFrameIndex, out frame))
		{
			return frameElement;
		}
		for (int i = 0; i < frame.numElements; i++)
		{
			int num = frame.firstElementIdx + i;
			if (num < batch.group.data.frameElements.Count)
			{
				KAnim.Anim.FrameElement frameElement2 = batch.group.data.frameElements[num];
				if (!(frameElement2.symbol == HashedString.Invalid))
				{
					frameElement = frameElement2;
					break;
				}
			}
		}
		return frameElement;
	}

	// Token: 0x06004AFA RID: 19194 RVA: 0x001B2BBC File Offset: 0x001B0DBC
	public static void UpdateTalking(SpeechMonitor.Instance smi, float dt)
	{
		if (smi.ev.isValid())
		{
			PLAYBACK_STATE playback_STATE;
			smi.ev.getPlaybackState(out playback_STATE);
			if (playback_STATE == PLAYBACK_STATE.STOPPING || playback_STATE == PLAYBACK_STATE.STOPPED)
			{
				smi.GoTo(smi.sm.satisfied);
				smi.ev.clearHandle();
				return;
			}
		}
		KAnim.Anim.FrameElement firstFrameElement = SpeechMonitor.GetFirstFrameElement(smi.mouth);
		if (firstFrameElement.symbol == HashedString.Invalid)
		{
			return;
		}
		smi.Get<SymbolOverrideController>().AddSymbolOverride(SpeechMonitor.HASH_SNAPTO_MOUTH, smi.mouth.AnimFiles[0].GetData().build.GetSymbol(firstFrameElement.symbol), 3);
	}

	// Token: 0x040031A4 RID: 12708
	public GameStateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State satisfied;

	// Token: 0x040031A5 RID: 12709
	public GameStateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State talking;

	// Token: 0x040031A6 RID: 12710
	public static string PREFIX_SAD = "sad";

	// Token: 0x040031A7 RID: 12711
	public static string PREFIX_HAPPY = "happy";

	// Token: 0x040031A8 RID: 12712
	public static string PREFIX_SINGER = "sing";

	// Token: 0x040031A9 RID: 12713
	public StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.TargetParameter mouth;

	// Token: 0x040031AA RID: 12714
	private static HashedString HASH_SNAPTO_MOUTH = "snapto_mouth";

	// Token: 0x040031AB RID: 12715
	private static HashedString GENERIC_CONVO_ANIM_NAME = new HashedString("anim_generic_convo_kanim");

	// Token: 0x02001AA4 RID: 6820
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001AA5 RID: 6821
	public class Tuning : TuningData<SpeechMonitor.Tuning>
	{
		// Token: 0x04008057 RID: 32855
		public float randomSpeechIntervalMin;

		// Token: 0x04008058 RID: 32856
		public float randomSpeechIntervalMax;

		// Token: 0x04008059 RID: 32857
		public int speechCount;
	}

	// Token: 0x02001AA6 RID: 6822
	public new class Instance : GameStateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.GameInstance
	{
		// Token: 0x0600A468 RID: 42088 RVA: 0x003A6399 File Offset: 0x003A4599
		public Instance(IStateMachineTarget master, SpeechMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A469 RID: 42089 RVA: 0x003A63AE File Offset: 0x003A45AE
		public bool IsPlayingSpeech()
		{
			return base.IsInsideState(base.sm.talking);
		}

		// Token: 0x0600A46A RID: 42090 RVA: 0x003A63C1 File Offset: 0x003A45C1
		public void PlaySpeech(string speech_prefix, string voice_event)
		{
			this.speechPrefix = speech_prefix;
			this.voiceEvent = voice_event;
			this.GoTo(base.sm.talking);
		}

		// Token: 0x0600A46B RID: 42091 RVA: 0x003A63E4 File Offset: 0x003A45E4
		public void DrawMouth()
		{
			KAnim.Anim.FrameElement firstFrameElement = SpeechMonitor.GetFirstFrameElement(base.smi.mouth);
			if (firstFrameElement.symbol == HashedString.Invalid)
			{
				return;
			}
			KAnim.Build.Symbol symbol = base.smi.mouth.AnimFiles[0].GetData().build.GetSymbol(firstFrameElement.symbol);
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			base.GetComponent<SymbolOverrideController>().AddSymbolOverride(SpeechMonitor.HASH_SNAPTO_MOUTH, base.smi.mouth.AnimFiles[0].GetData().build.GetSymbol(firstFrameElement.symbol), 3);
			KAnim.Build.Symbol symbol2 = KAnimBatchManager.Instance().GetBatchGroupData(component.batchGroupID).GetSymbol(SpeechMonitor.HASH_SNAPTO_MOUTH);
			KAnim.Build.SymbolFrameInstance symbolFrameInstance = KAnimBatchManager.Instance().GetBatchGroupData(symbol.build.batchTag).symbolFrameInstances[symbol.firstFrameIdx + firstFrameElement.frame];
			symbolFrameInstance.buildImageIdx = base.GetComponent<SymbolOverrideController>().GetAtlasIdx(symbol.build.GetTexture(0));
			component.SetSymbolOverride(symbol2.firstFrameIdx, ref symbolFrameInstance);
		}

		// Token: 0x0600A46C RID: 42092 RVA: 0x003A64F8 File Offset: 0x003A46F8
		public void SetMouthId()
		{
			MinionIdentity component = base.GetComponent<MinionIdentity>();
			Personality personality = Db.Get().Personalities.Get(component.personalityResourceId);
			if (personality.speech_mouth > 0)
			{
				base.smi.mouthId = string.Format("_{0:000}", personality.mouth);
			}
		}

		// Token: 0x0400805A RID: 32858
		public KBatchedAnimController mouth;

		// Token: 0x0400805B RID: 32859
		public string speechPrefix = "happy";

		// Token: 0x0400805C RID: 32860
		public string voiceEvent;

		// Token: 0x0400805D RID: 32861
		public EventInstance ev;

		// Token: 0x0400805E RID: 32862
		public string mouthId;
	}
}
