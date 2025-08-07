using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020004C3 RID: 1219
public class HappySinger : GameStateMachine<HappySinger, HappySinger.Instance>
{
	// Token: 0x06001A1B RID: 6683 RVA: 0x0008F470 File Offset: 0x0008D670
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false);
		this.overjoyed.DefaultState(this.overjoyed.idle).TagTransition(GameTags.Overjoyed, this.neutral, true).ToggleEffect("IsJoySinger")
			.ToggleLoopingSound(this.soundPath, null, true, true, true)
			.ToggleAnims("anim_loco_singer_kanim", 0f)
			.ToggleAnims("anim_idle_singer_kanim", 0f)
			.EventHandler(GameHashes.TagsChanged, delegate(HappySinger.Instance smi, object obj)
			{
				if (smi.musicParticleFX != null)
				{
					smi.musicParticleFX.SetActive(!smi.HasTag(GameTags.Asleep));
				}
			})
			.Enter(delegate(HappySinger.Instance smi)
			{
				smi.musicParticleFX = Util.KInstantiate(EffectPrefabs.Instance.HappySingerFX, smi.master.transform.GetPosition() + this.offset);
				smi.musicParticleFX.transform.SetParent(smi.master.transform);
				smi.CreatePasserbyReactable();
				smi.musicParticleFX.SetActive(!smi.HasTag(GameTags.Asleep));
			})
			.Update(delegate(HappySinger.Instance smi, float dt)
			{
				if (!smi.GetSpeechMonitor().IsPlayingSpeech() && SpeechMonitor.IsAllowedToPlaySpeech(smi.gameObject))
				{
					smi.GetSpeechMonitor().PlaySpeech(Db.Get().Thoughts.CatchyTune.speechPrefix, Db.Get().Thoughts.CatchyTune.sound);
				}
			}, UpdateRate.SIM_1000ms, false)
			.Exit(delegate(HappySinger.Instance smi)
			{
				smi.musicParticleFX.SetActive(false);
				Util.KDestroyGameObject(smi.musicParticleFX);
				smi.ClearPasserbyReactable();
			});
	}

	// Token: 0x04000EFD RID: 3837
	private Vector3 offset = new Vector3(0f, 0f, 0.1f);

	// Token: 0x04000EFE RID: 3838
	public GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000EFF RID: 3839
	public HappySinger.OverjoyedStates overjoyed;

	// Token: 0x04000F00 RID: 3840
	public string soundPath = GlobalAssets.GetSound("DupeSinging_NotesFX_LP", false);

	// Token: 0x02001314 RID: 4884
	public class OverjoyedStates : GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006844 RID: 26692
		public GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x04006845 RID: 26693
		public GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.State moving;
	}

	// Token: 0x02001315 RID: 4885
	public new class Instance : GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600889C RID: 34972 RVA: 0x003499C8 File Offset: 0x00347BC8
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600889D RID: 34973 RVA: 0x003499D4 File Offset: 0x00347BD4
		public void CreatePasserbyReactable()
		{
			if (this.passerbyReactable == null)
			{
				EmoteReactable emoteReactable = new EmoteReactable(base.gameObject, "WorkPasserbyAcknowledgement", Db.Get().ChoreTypes.Emote, 5, 5, 0f, 600f, float.PositiveInfinity, 0f);
				Emote sing = Db.Get().Emotes.Minion.Sing;
				emoteReactable.SetEmote(sing).SetThought(Db.Get().Thoughts.CatchyTune).AddPrecondition(new Reactable.ReactablePrecondition(this.ReactorIsOnFloor));
				emoteReactable.RegisterEmoteStepCallbacks("react", new Action<GameObject>(this.AddReactionEffect), null);
				this.passerbyReactable = emoteReactable;
			}
		}

		// Token: 0x0600889E RID: 34974 RVA: 0x00349A8E File Offset: 0x00347C8E
		public SpeechMonitor.Instance GetSpeechMonitor()
		{
			if (this.speechMonitor == null)
			{
				this.speechMonitor = base.master.gameObject.GetSMI<SpeechMonitor.Instance>();
			}
			return this.speechMonitor;
		}

		// Token: 0x0600889F RID: 34975 RVA: 0x00349AB4 File Offset: 0x00347CB4
		private void AddReactionEffect(GameObject reactor)
		{
			reactor.Trigger(-1278274506, null);
		}

		// Token: 0x060088A0 RID: 34976 RVA: 0x00349AC2 File Offset: 0x00347CC2
		private bool ReactorIsOnFloor(GameObject reactor, Navigator.ActiveTransition transition)
		{
			return transition.end == NavType.Floor;
		}

		// Token: 0x060088A1 RID: 34977 RVA: 0x00349ACD File Offset: 0x00347CCD
		public void ClearPasserbyReactable()
		{
			if (this.passerbyReactable != null)
			{
				this.passerbyReactable.Cleanup();
				this.passerbyReactable = null;
			}
		}

		// Token: 0x04006846 RID: 26694
		private Reactable passerbyReactable;

		// Token: 0x04006847 RID: 26695
		public GameObject musicParticleFX;

		// Token: 0x04006848 RID: 26696
		public SpeechMonitor.Instance speechMonitor;
	}
}
