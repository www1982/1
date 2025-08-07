using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020004C5 RID: 1221
public class SparkleStreaker : GameStateMachine<SparkleStreaker, SparkleStreaker.Instance>
{
	// Token: 0x06001A23 RID: 6691 RVA: 0x0008F848 File Offset: 0x0008DA48
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false);
		this.overjoyed.DefaultState(this.overjoyed.idle).TagTransition(GameTags.Overjoyed, this.neutral, true).ToggleEffect("IsSparkleStreaker")
			.ToggleLoopingSound(this.soundPath, null, true, true, true)
			.Enter(delegate(SparkleStreaker.Instance smi)
			{
				smi.sparkleStreakFX = Util.KInstantiate(EffectPrefabs.Instance.SparkleStreakFX, smi.master.transform.GetPosition() + this.offset);
				smi.sparkleStreakFX.transform.SetParent(smi.master.transform);
				smi.sparkleStreakFX.SetActive(true);
				smi.CreatePasserbyReactable();
			})
			.Exit(delegate(SparkleStreaker.Instance smi)
			{
				Util.KDestroyGameObject(smi.sparkleStreakFX);
				smi.ClearPasserbyReactable();
			});
		this.overjoyed.idle.Enter(delegate(SparkleStreaker.Instance smi)
		{
			smi.SetSparkleSoundParam(0f);
		}).EventTransition(GameHashes.ObjectMovementStateChanged, this.overjoyed.moving, (SparkleStreaker.Instance smi) => smi.IsMoving());
		this.overjoyed.moving.Enter(delegate(SparkleStreaker.Instance smi)
		{
			smi.SetSparkleSoundParam(1f);
		}).EventTransition(GameHashes.ObjectMovementStateChanged, this.overjoyed.idle, (SparkleStreaker.Instance smi) => !smi.IsMoving());
	}

	// Token: 0x04000F05 RID: 3845
	private Vector3 offset = new Vector3(0f, 0f, 0.1f);

	// Token: 0x04000F06 RID: 3846
	public GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000F07 RID: 3847
	public SparkleStreaker.OverjoyedStates overjoyed;

	// Token: 0x04000F08 RID: 3848
	public string soundPath = GlobalAssets.GetSound("SparkleStreaker_lp", false);

	// Token: 0x04000F09 RID: 3849
	public HashedString SPARKLE_STREAKER_MOVING_PARAMETER = "sparkleStreaker_moving";

	// Token: 0x0200131A RID: 4890
	public class OverjoyedStates : GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x04006855 RID: 26709
		public GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x04006856 RID: 26710
		public GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.State moving;
	}

	// Token: 0x0200131B RID: 4891
	public new class Instance : GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060088B2 RID: 34994 RVA: 0x00349C47 File Offset: 0x00347E47
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060088B3 RID: 34995 RVA: 0x00349C50 File Offset: 0x00347E50
		public void CreatePasserbyReactable()
		{
			if (this.passerbyReactable == null)
			{
				EmoteReactable emoteReactable = new EmoteReactable(base.gameObject, "WorkPasserbyAcknowledgement", Db.Get().ChoreTypes.Emote, 5, 5, 0f, 600f, float.PositiveInfinity, 0f);
				Emote clapCheer = Db.Get().Emotes.Minion.ClapCheer;
				emoteReactable.SetEmote(clapCheer).SetThought(Db.Get().Thoughts.Happy).AddPrecondition(new Reactable.ReactablePrecondition(this.ReactorIsOnFloor));
				emoteReactable.RegisterEmoteStepCallbacks("clapcheer_pre", new Action<GameObject>(this.AddReactionEffect), null);
				this.passerbyReactable = emoteReactable;
			}
		}

		// Token: 0x060088B4 RID: 34996 RVA: 0x00349D0A File Offset: 0x00347F0A
		private void AddReactionEffect(GameObject reactor)
		{
			reactor.GetComponent<Effects>().Add("SawSparkleStreaker", true);
		}

		// Token: 0x060088B5 RID: 34997 RVA: 0x00349D1E File Offset: 0x00347F1E
		private bool ReactorIsOnFloor(GameObject reactor, Navigator.ActiveTransition transition)
		{
			return transition.end == NavType.Floor;
		}

		// Token: 0x060088B6 RID: 34998 RVA: 0x00349D29 File Offset: 0x00347F29
		public void ClearPasserbyReactable()
		{
			if (this.passerbyReactable != null)
			{
				this.passerbyReactable.Cleanup();
				this.passerbyReactable = null;
			}
		}

		// Token: 0x060088B7 RID: 34999 RVA: 0x00349D45 File Offset: 0x00347F45
		public bool IsMoving()
		{
			return base.smi.master.GetComponent<Navigator>().IsMoving();
		}

		// Token: 0x060088B8 RID: 35000 RVA: 0x00349D5C File Offset: 0x00347F5C
		public void SetSparkleSoundParam(float val)
		{
			base.GetComponent<LoopingSounds>().SetParameter(GlobalAssets.GetSound("SparkleStreaker_lp", false), "sparkleStreaker_moving", val);
		}

		// Token: 0x04006857 RID: 26711
		private Reactable passerbyReactable;

		// Token: 0x04006858 RID: 26712
		public GameObject sparkleStreakFX;
	}
}
