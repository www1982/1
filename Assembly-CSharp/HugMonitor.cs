using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000599 RID: 1433
public class HugMonitor : GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>
{
	// Token: 0x060020B6 RID: 8374 RVA: 0x000BD020 File Offset: 0x000BB220
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.normal;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.Update(new Action<HugMonitor.Instance, float>(this.UpdateHugEggCooldownTimer), UpdateRate.SIM_1000ms, false).ToggleBehaviour(GameTags.Creatures.WantsToTendEgg, (HugMonitor.Instance smi) => smi.UpdateHasTarget(), delegate(HugMonitor.Instance smi)
		{
			smi.hugTarget = null;
		});
		this.normal.DefaultState(this.normal.idle).ParamTransition<float>(this.hugFrenzyTimer, this.hugFrenzy, GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.IsGTZero);
		this.normal.idle.ParamTransition<float>(this.wantsHugCooldownTimer, this.normal.hugReady.seekingHug, GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.IsLTEZero).Update(new Action<HugMonitor.Instance, float>(this.UpdateWantsHugCooldownTimer), UpdateRate.SIM_1000ms, false);
		this.normal.hugReady.ToggleReactable(new Func<HugMonitor.Instance, Reactable>(this.GetHugReactable));
		GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State state = this.normal.hugReady.passiveHug.ParamTransition<float>(this.wantsHugCooldownTimer, this.normal.hugReady.seekingHug, GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.IsLTEZero).Update(new Action<HugMonitor.Instance, float>(this.UpdateWantsHugCooldownTimer), UpdateRate.SIM_1000ms, false);
		string text = CREATURES.STATUSITEMS.HUGMINIONWAITING.NAME;
		string text2 = CREATURES.STATUSITEMS.HUGMINIONWAITING.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main);
		this.normal.hugReady.seekingHug.ToggleBehaviour(GameTags.Creatures.WantsAHug, (HugMonitor.Instance smi) => true, delegate(HugMonitor.Instance smi)
		{
			this.wantsHugCooldownTimer.Set(smi.def.hugFrenzyCooldownFailed, smi, false);
			smi.GoTo(this.normal.hugReady.passiveHug);
		});
		this.hugFrenzy.ParamTransition<float>(this.hugFrenzyTimer, this.normal, (HugMonitor.Instance smi, float p) => p <= 0f && !smi.IsHugging()).Update(new Action<HugMonitor.Instance, float>(this.UpdateHugFrenzyTimer), UpdateRate.SIM_1000ms, false).ToggleEffect((HugMonitor.Instance smi) => smi.frenzyEffect)
			.ToggleLoopingSound(HugMonitor.soundPath, null, true, true, true)
			.Enter(delegate(HugMonitor.Instance smi)
			{
				smi.hugParticleFx = Util.KInstantiate(EffectPrefabs.Instance.HugFrenzyFX, smi.master.transform.GetPosition() + smi.hugParticleOffset);
				smi.hugParticleFx.transform.SetParent(smi.master.transform);
				smi.hugParticleFx.SetActive(true);
			})
			.Exit(delegate(HugMonitor.Instance smi)
			{
				Util.KDestroyGameObject(smi.hugParticleFx);
				this.wantsHugCooldownTimer.Set(smi.def.hugFrenzyCooldown, smi, false);
			});
	}

	// Token: 0x060020B7 RID: 8375 RVA: 0x000BD2A4 File Offset: 0x000BB4A4
	private Reactable GetHugReactable(HugMonitor.Instance smi)
	{
		return new HugMinionReactable(smi.gameObject);
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x000BD2B1 File Offset: 0x000BB4B1
	private void UpdateWantsHugCooldownTimer(HugMonitor.Instance smi, float dt)
	{
		this.wantsHugCooldownTimer.DeltaClamp(-dt, 0f, float.MaxValue, smi);
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x000BD2CC File Offset: 0x000BB4CC
	private void UpdateHugEggCooldownTimer(HugMonitor.Instance smi, float dt)
	{
		this.hugEggCooldownTimer.DeltaClamp(-dt, 0f, float.MaxValue, smi);
	}

	// Token: 0x060020BA RID: 8378 RVA: 0x000BD2E7 File Offset: 0x000BB4E7
	private void UpdateHugFrenzyTimer(HugMonitor.Instance smi, float dt)
	{
		this.hugFrenzyTimer.DeltaClamp(-dt, 0f, float.MaxValue, smi);
	}

	// Token: 0x04001306 RID: 4870
	private static string soundPath = GlobalAssets.GetSound("Squirrel_hug_frenzyFX", false);

	// Token: 0x04001307 RID: 4871
	private static Effect hugEffect;

	// Token: 0x04001308 RID: 4872
	private StateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.FloatParameter hugFrenzyTimer;

	// Token: 0x04001309 RID: 4873
	private StateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.FloatParameter wantsHugCooldownTimer;

	// Token: 0x0400130A RID: 4874
	private StateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.FloatParameter hugEggCooldownTimer;

	// Token: 0x0400130B RID: 4875
	public HugMonitor.NormalStates normal;

	// Token: 0x0400130C RID: 4876
	public GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State hugFrenzy;

	// Token: 0x0200140F RID: 5135
	public class HUGTUNING
	{
		// Token: 0x04006B88 RID: 27528
		public const float HUG_EGG_TIME = 15f;

		// Token: 0x04006B89 RID: 27529
		public const float HUG_DUPE_WAIT = 60f;

		// Token: 0x04006B8A RID: 27530
		public const float FRENZY_EGGS_PER_CYCLE = 6f;

		// Token: 0x04006B8B RID: 27531
		public const float FRENZY_EGG_TRAVEL_TIME_BUFFER = 5f;

		// Token: 0x04006B8C RID: 27532
		public const float HUG_FRENZY_DURATION = 120f;
	}

	// Token: 0x02001410 RID: 5136
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006B8D RID: 27533
		public float hugsPerCycle = 2f;

		// Token: 0x04006B8E RID: 27534
		public float scanningInterval = 30f;

		// Token: 0x04006B8F RID: 27535
		public float hugFrenzyDuration = 120f;

		// Token: 0x04006B90 RID: 27536
		public float hugFrenzyCooldown = 480f;

		// Token: 0x04006B91 RID: 27537
		public float hugFrenzyCooldownFailed = 120f;

		// Token: 0x04006B92 RID: 27538
		public float scanningIntervalFrenzy = 15f;

		// Token: 0x04006B93 RID: 27539
		public int maxSearchCost = 30;
	}

	// Token: 0x02001411 RID: 5137
	public class HugReadyStates : GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State
	{
		// Token: 0x04006B94 RID: 27540
		public GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State passiveHug;

		// Token: 0x04006B95 RID: 27541
		public GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State seekingHug;
	}

	// Token: 0x02001412 RID: 5138
	public class NormalStates : GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State
	{
		// Token: 0x04006B96 RID: 27542
		public GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.State idle;

		// Token: 0x04006B97 RID: 27543
		public HugMonitor.HugReadyStates hugReady;
	}

	// Token: 0x02001413 RID: 5139
	public new class Instance : GameStateMachine<HugMonitor, HugMonitor.Instance, IStateMachineTarget, HugMonitor.Def>.GameInstance
	{
		// Token: 0x06008C5C RID: 35932 RVA: 0x003559B8 File Offset: 0x00353BB8
		public Instance(IStateMachineTarget master, HugMonitor.Def def)
			: base(master, def)
		{
			this.frenzyEffect = Db.Get().effects.Get("HuggingFrenzy");
			this.RefreshSearchTime();
			if (HugMonitor.hugEffect == null)
			{
				HugMonitor.hugEffect = Db.Get().effects.Get("EggHug");
			}
			base.smi.sm.wantsHugCooldownTimer.Set(global::UnityEngine.Random.Range(base.smi.def.hugFrenzyCooldownFailed, base.smi.def.hugFrenzyCooldown), base.smi, false);
		}

		// Token: 0x06008C5D RID: 35933 RVA: 0x00355A50 File Offset: 0x00353C50
		private void RefreshSearchTime()
		{
			if (this.hugTarget == null)
			{
				base.smi.sm.hugEggCooldownTimer.Set(this.GetScanningInterval(), base.smi, false);
				return;
			}
			base.smi.sm.hugEggCooldownTimer.Set(this.GetHugInterval(), base.smi, false);
		}

		// Token: 0x06008C5E RID: 35934 RVA: 0x00355AB2 File Offset: 0x00353CB2
		private float GetScanningInterval()
		{
			if (!this.IsHuggingFrenzy())
			{
				return base.def.scanningInterval;
			}
			return base.def.scanningIntervalFrenzy;
		}

		// Token: 0x06008C5F RID: 35935 RVA: 0x00355AD3 File Offset: 0x00353CD3
		private float GetHugInterval()
		{
			if (this.IsHuggingFrenzy())
			{
				return 0f;
			}
			return 600f / base.def.hugsPerCycle;
		}

		// Token: 0x06008C60 RID: 35936 RVA: 0x00355AF4 File Offset: 0x00353CF4
		public bool IsHuggingFrenzy()
		{
			return base.smi.GetCurrentState() == base.smi.sm.hugFrenzy;
		}

		// Token: 0x06008C61 RID: 35937 RVA: 0x00355B13 File Offset: 0x00353D13
		public bool IsHugging()
		{
			return base.smi.GetSMI<AnimInterruptMonitor.Instance>().anims != null;
		}

		// Token: 0x06008C62 RID: 35938 RVA: 0x00355B28 File Offset: 0x00353D28
		public bool UpdateHasTarget()
		{
			if (this.hugTarget == null)
			{
				if (base.smi.sm.hugEggCooldownTimer.Get(base.smi) > 0f)
				{
					return false;
				}
				this.FindEgg();
				this.RefreshSearchTime();
			}
			return this.hugTarget != null;
		}

		// Token: 0x06008C63 RID: 35939 RVA: 0x00355B80 File Offset: 0x00353D80
		public void EnterHuggingFrenzy()
		{
			base.smi.sm.hugFrenzyTimer.Set(base.smi.def.hugFrenzyDuration, base.smi, false);
			base.smi.sm.hugEggCooldownTimer.Set(0f, base.smi, false);
		}

		// Token: 0x06008C64 RID: 35940 RVA: 0x00355BDC File Offset: 0x00353DDC
		private void FindEgg()
		{
			int num = Grid.PosToCell(base.gameObject);
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			int num2 = base.def.maxSearchCost;
			this.hugTarget = null;
			if (cavityForCell != null)
			{
				foreach (KPrefabID kprefabID in cavityForCell.eggs)
				{
					if (!kprefabID.HasTag(GameTags.Creatures.ReservedByCreature) && !kprefabID.GetComponent<Effects>().HasEffect(HugMonitor.hugEffect))
					{
						int num3 = Grid.PosToCell(kprefabID);
						if (kprefabID.HasTag(GameTags.Stored))
						{
							GameObject gameObject;
							KPrefabID kprefabID2;
							if (!Grid.ObjectLayers[1].TryGetValue(num3, out gameObject) || !gameObject.TryGetComponent<KPrefabID>(out kprefabID2) || !kprefabID2.IsPrefabID("EggIncubator"))
							{
								continue;
							}
							num3 = Grid.PosToCell(gameObject);
							kprefabID = kprefabID2;
						}
						int navigationCost = this.navigator.GetNavigationCost(num3);
						if (navigationCost != -1 && navigationCost < num2)
						{
							this.hugTarget = kprefabID;
							num2 = navigationCost;
						}
					}
				}
			}
		}

		// Token: 0x04006B98 RID: 27544
		public GameObject hugParticleFx;

		// Token: 0x04006B99 RID: 27545
		public Vector3 hugParticleOffset;

		// Token: 0x04006B9A RID: 27546
		public Effect frenzyEffect;

		// Token: 0x04006B9B RID: 27547
		public KPrefabID hugTarget;

		// Token: 0x04006B9C RID: 27548
		[MyCmpGet]
		private Navigator navigator;
	}
}
