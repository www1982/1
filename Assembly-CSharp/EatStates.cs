using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class EatStates : GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>
{
	// Token: 0x06000433 RID: 1075 RVA: 0x00022E18 File Offset: 0x00021018
	private static Effect CreatePredationStunEffect()
	{
		return new Effect("StunnedEat", "", "", 5f, false, false, true, "", -1f, null, "")
		{
			tag = new Tag?(GameTags.Creatures.StunnedBeingEaten)
		};
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00022E64 File Offset: 0x00021064
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.goingtoeat;
		this.root.Enter(new StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State.Callback(EatStates.SetTarget)).Exit(new StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State.Callback(EatStates.UnreserveEdible));
		GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State state = this.goingtoeat.MoveTo(new Func<EatStates.Instance, int>(EatStates.GetEdibleCell), this.arrivedAtEdible, this.behaviourcomplete, false);
		string text = CREATURES.STATUSITEMS.HUNGRY.NAME;
		string text2 = CREATURES.STATUSITEMS.HUNGRY.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory statusItemCategory = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, statusItemCategory);
		this.arrivedAtEdible.EnterTransition(this.pounce, (EatStates.Instance smi) => smi.IsPredator).Transition(this.eating, (EatStates.Instance smi) => !smi.IsPredator, UpdateRate.SIM_200ms);
		GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State state2 = this.pounce.Face(this.target, 0f).DefaultState(this.pounce.pre);
		string text4 = CREATURES.STATUSITEMS.HUNTING.NAME;
		string text5 = CREATURES.STATUSITEMS.HUNTING.TOOLTIP;
		string text6 = "";
		StatusItem.IconType iconType2 = StatusItem.IconType.Info;
		NotificationType notificationType2 = NotificationType.Neutral;
		bool flag2 = false;
		statusItemCategory = Db.Get().StatusItemCategories.Main;
		state2.ToggleStatusItem(text4, text5, text6, iconType2, notificationType2, flag2, default(HashedString), 129022, null, null, statusItemCategory);
		this.pounce.pre.PlayAnim("pounce_pre").OnAnimQueueComplete(this.pounce.roll);
		this.pounce.roll.Enter(delegate(EatStates.Instance smi)
		{
			if (EatStates.CheckHuntSuccess(smi))
			{
				smi.GoTo(this.pounce.hit);
				return;
			}
			smi.GoTo(this.pounce.miss);
		});
		this.pounce.hit.Enter(delegate(EatStates.Instance smi)
		{
			EatStates.FreezeEdible(smi);
		}).QueueAnim("pounce_hit", false, null).OnAnimQueueComplete(this.eating);
		this.pounce.miss.Enter(delegate(EatStates.Instance smi)
		{
			EatStates.OnPounceMiss(smi);
		}).QueueAnim("pounce_miss", false, null).OnAnimQueueComplete(this.failedHunt);
		this.failedHunt.PlayAnim("idle_loop", KAnim.PlayMode.Loop).ScheduleGoTo(5f, this.behaviourcomplete);
		GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State state3 = this.eating.EnterTransition(this.behaviourcomplete, (EatStates.Instance smi) => EatStates.EdibleGotAway(smi)).Face(this.target, 0f).DefaultState(this.eating.pre);
		string text7 = CREATURES.STATUSITEMS.EATING.NAME;
		string text8 = CREATURES.STATUSITEMS.EATING.TOOLTIP;
		string text9 = "";
		StatusItem.IconType iconType3 = StatusItem.IconType.Info;
		NotificationType notificationType3 = NotificationType.Neutral;
		bool flag3 = false;
		statusItemCategory = Db.Get().StatusItemCategories.Main;
		state3.ToggleStatusItem(text7, text8, text9, iconType3, notificationType3, flag3, default(HashedString), 129022, null, null, statusItemCategory);
		this.eating.pre.Enter(delegate(EatStates.Instance smi)
		{
			EatStates.FreezeEdible(smi);
		}).QueueAnim((EatStates.Instance smi) => smi.eatAnims[0], false, null).OnAnimQueueComplete(this.eating.loop);
		this.eating.loop.Enter(new StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State.Callback(EatStates.EatComplete)).QueueAnim((EatStates.Instance smi) => smi.eatAnims[1], false, null).OnAnimQueueComplete(this.eating.pst);
		this.eating.pst.QueueAnim((EatStates.Instance smi) => smi.eatAnims[2], false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.Enter(delegate(EatStates.Instance smi)
		{
			smi.solidConsumer.ClearTargetEdible();
		}).PlayAnim("idle_loop", KAnim.PlayMode.Loop).BehaviourComplete(GameTags.Creatures.WantsToEat, false);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x0002328C File Offset: 0x0002148C
	private static void SetTarget(EatStates.Instance smi)
	{
		smi.solidConsumer = smi.GetSMI<SolidConsumerMonitor.Instance>();
		smi.sm.target.Set(smi.solidConsumer.targetEdible, smi, false);
		EatStates.ReserveEdible(smi);
		smi.OverrideEatAnims(smi, smi.solidConsumer.GetTargetEdibleEatAnims());
		smi.sm.offset.Set(smi.solidConsumer.targetEdibleOffset, smi, false);
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x000232FC File Offset: 0x000214FC
	private static void ReserveEdible(EatStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			DebugUtil.Assert(!gameObject.HasTag(GameTags.Creatures.ReservedByCreature));
			gameObject.AddTag(GameTags.Creatures.ReservedByCreature);
		}
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00023344 File Offset: 0x00021544
	private static void UnreserveEdible(EatStates.Instance smi)
	{
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			if (gameObject.HasTag(GameTags.Creatures.ReservedByCreature))
			{
				gameObject.RemoveTag(GameTags.Creatures.ReservedByCreature);
				return;
			}
			global::Debug.LogWarningFormat(smi.gameObject, "{0} UnreserveEdible but it wasn't reserved: {1}", new object[] { smi.gameObject, gameObject });
		}
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x000233A8 File Offset: 0x000215A8
	private static void EatComplete(EatStates.Instance smi)
	{
		PrimaryElement primaryElement = smi.sm.target.Get<PrimaryElement>(smi);
		if (primaryElement != null)
		{
			smi.lastMealElement = primaryElement.Element;
		}
		smi.Trigger(1386391852, smi.sm.target.Get<KPrefabID>(smi));
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x000233F8 File Offset: 0x000215F8
	private static bool EdibleGotAway(EatStates.Instance smi)
	{
		int edibleCell = EatStates.GetEdibleCell(smi);
		return Grid.PosToCell(smi) != edibleCell;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00023418 File Offset: 0x00021618
	private static void FreezeEdible(EatStates.Instance smi)
	{
		if (!smi.IsPredator)
		{
			return;
		}
		GameObject gameObject = smi.sm.target.Get(smi);
		Effects component = gameObject.GetComponent<Effects>();
		if (component != null)
		{
			component.Add(EatStates.PredationStunEffect, false);
		}
		Brain component2 = gameObject.GetComponent<Brain>();
		if (component2 != null)
		{
			Game.BrainScheduler.PrioritizeBrain(component2);
		}
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00023478 File Offset: 0x00021678
	private static void OnPounceMiss(EatStates.Instance smi)
	{
		smi.GetComponent<Effects>().Add("PredatorFailedHunt", true);
		GameObject gameObject = smi.sm.target.Get(smi);
		if (gameObject != null)
		{
			gameObject.Trigger(-787691065, smi.GetComponent<FactionAlignment>());
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x000234C4 File Offset: 0x000216C4
	private static bool HuntPredicateWild(GameObject obj)
	{
		if (obj == null)
		{
			return false;
		}
		AmountInstance amountInstance = Db.Get().Amounts.Age.Lookup(obj);
		if (amountInstance == null)
		{
			return true;
		}
		float num = amountInstance.value / amountInstance.GetMax();
		return num >= EatStates.HUNT_WILD_MIN_AGE && global::UnityEngine.Random.Range(0f, 1f) < EatStates.HUNT_WILD_PRED_RATE.Lerp(num);
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0002352C File Offset: 0x0002172C
	private static bool HuntPredicateTame(GameObject obj)
	{
		if (obj == null)
		{
			return false;
		}
		AmountInstance amountInstance = Db.Get().Amounts.Age.Lookup(obj);
		if (amountInstance == null)
		{
			return true;
		}
		float num = amountInstance.value / amountInstance.GetMax();
		return global::UnityEngine.Random.Range(0f, 1f) < EatStates.HUNT_TAME_PRED_RATE.Lerp(num);
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0002358C File Offset: 0x0002178C
	private static bool CheckHuntSuccess(EatStates.Instance smi)
	{
		WildnessMonitor.Instance smi2 = smi.gameObject.GetSMI<WildnessMonitor.Instance>();
		GameObject gameObject = smi.sm.target.Get(smi);
		WildnessMonitor.Instance instance = ((gameObject != null) ? gameObject.GetSMI<WildnessMonitor.Instance>() : null);
		bool flag = smi2 != null && smi2.IsWild();
		bool flag2 = instance != null && instance.IsWild();
		if (flag && flag2)
		{
			return EatStates.HuntPredicateWild(gameObject);
		}
		return EatStates.HuntPredicateTame(gameObject);
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x000235F4 File Offset: 0x000217F4
	private static int GetEdibleCell(EatStates.Instance smi)
	{
		if (smi.Edible == null)
		{
			return Grid.InvalidCell;
		}
		return Grid.PosToCell(smi.Edible.transform.GetPosition() + smi.sm.offset.Get(smi));
	}

	// Token: 0x04000314 RID: 788
	private static Effect PredationStunEffect = EatStates.CreatePredationStunEffect();

	// Token: 0x04000315 RID: 789
	public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.ApproachSubState<Pickupable> goingtoeat;

	// Token: 0x04000316 RID: 790
	public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State arrivedAtEdible;

	// Token: 0x04000317 RID: 791
	public EatStates.PounceState pounce;

	// Token: 0x04000318 RID: 792
	public EatStates.EatingState eating;

	// Token: 0x04000319 RID: 793
	public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State failedHunt;

	// Token: 0x0400031A RID: 794
	public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State behaviourcomplete;

	// Token: 0x0400031B RID: 795
	public StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.Vector3Parameter offset;

	// Token: 0x0400031C RID: 796
	public StateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.TargetParameter target;

	// Token: 0x0400031D RID: 797
	private static float HUNT_WILD_MIN_AGE = 0.825f;

	// Token: 0x0400031E RID: 798
	private static MathUtil.MinMax HUNT_WILD_PRED_RATE = new MathUtil.MinMax(0.1f, 1.1f);

	// Token: 0x0400031F RID: 799
	private static MathUtil.MinMax HUNT_TAME_PRED_RATE = new MathUtil.MinMax(0.4f, 1.05f);

	// Token: 0x020010E0 RID: 4320
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020010E1 RID: 4321
	public new class Instance : GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.GameInstance
	{
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060080EC RID: 33004 RVA: 0x0032E2C5 File Offset: 0x0032C4C5
		public GameObject Edible
		{
			get
			{
				return base.smi.sm.target.Get(this);
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x060080ED RID: 33005 RVA: 0x0032E2DD File Offset: 0x0032C4DD
		// (set) Token: 0x060080EE RID: 33006 RVA: 0x0032E2E5 File Offset: 0x0032C4E5
		public bool IsPredator { get; private set; }

		// Token: 0x060080EF RID: 33007 RVA: 0x0032E2EE File Offset: 0x0032C4EE
		public void OverrideEatAnims(EatStates.Instance smi, string[] preLoopPstAnims)
		{
			global::Debug.Assert(preLoopPstAnims != null && preLoopPstAnims.Length == 3);
			smi.eatAnims = preLoopPstAnims;
		}

		// Token: 0x060080F0 RID: 33008 RVA: 0x0032E308 File Offset: 0x0032C508
		public Instance(Chore<EatStates.Instance> chore, EatStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToEat);
			chore.AddPrecondition(ChorePreconditions.instance.DoesntHaveTag, GameTags.Creatures.SuppressedDiet);
			this.IsPredator = base.gameObject.GetComponent<FactionAlignment>().Alignment == FactionManager.FactionID.Predator;
		}

		// Token: 0x060080F1 RID: 33009 RVA: 0x0032E38E File Offset: 0x0032C58E
		public Element GetLatestMealElement()
		{
			return this.lastMealElement;
		}

		// Token: 0x0400617D RID: 24957
		public Element lastMealElement;

		// Token: 0x0400617E RID: 24958
		public SolidConsumerMonitor.Instance solidConsumer;

		// Token: 0x04006180 RID: 24960
		public string[] eatAnims = new string[] { "eat_pre", "eat_loop", "eat_pst" };
	}

	// Token: 0x020010E2 RID: 4322
	public class PounceState : GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State
	{
		// Token: 0x04006181 RID: 24961
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State pre;

		// Token: 0x04006182 RID: 24962
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State roll;

		// Token: 0x04006183 RID: 24963
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State hit;

		// Token: 0x04006184 RID: 24964
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State miss;
	}

	// Token: 0x020010E3 RID: 4323
	public class EatingState : GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State
	{
		// Token: 0x04006185 RID: 24965
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State pre;

		// Token: 0x04006186 RID: 24966
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State loop;

		// Token: 0x04006187 RID: 24967
		public GameStateMachine<EatStates, EatStates.Instance, IStateMachineTarget, EatStates.Def>.State pst;
	}
}
