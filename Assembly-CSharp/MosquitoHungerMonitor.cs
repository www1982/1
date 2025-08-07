using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200059C RID: 1436
public class MosquitoHungerMonitor : StateMachineComponent<MosquitoHungerMonitor.Instance>
{
	// Token: 0x060020C5 RID: 8389 RVA: 0x000BD563 File Offset: 0x000BB763
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060020C6 RID: 8390 RVA: 0x000BD56B File Offset: 0x000BB76B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x060020C7 RID: 8391 RVA: 0x000BD57E File Offset: 0x000BB77E
	private static void ClearTarget(MosquitoHungerMonitor.Instance smi)
	{
		smi.sm.victim.Set(null, smi);
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x000BD592 File Offset: 0x000BB792
	public static bool IsFed(MosquitoHungerMonitor.Instance smi)
	{
		return smi.IsFed;
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x000BD59A File Offset: 0x000BB79A
	public static bool HasValidVictim(MosquitoHungerMonitor.Instance smi)
	{
		return MosquitoHungerMonitor.HasValidVictim(smi, smi.Victim);
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x000BD5A8 File Offset: 0x000BB7A8
	public static bool HasValidVictim(MosquitoHungerMonitor.Instance smi, GameObject victimParam)
	{
		return victimParam != null && !MosquitoHungerMonitor.IsVictimForbidden(smi, victimParam.GetComponent<KPrefabID>(), true);
	}

	// Token: 0x060020CB RID: 8395 RVA: 0x000BD5C8 File Offset: 0x000BB7C8
	public static void LookForVictim(MosquitoHungerMonitor.Instance smi)
	{
		CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(smi));
		if (cavityForCell == null)
		{
			return;
		}
		int myWorldId = smi.GetMyWorldId();
		List<KPrefabID> list = new List<KPrefabID>();
		if (smi.master.CanBiteMinions)
		{
			List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(myWorldId, false);
			for (int i = 0; i < worldItems.Count; i++)
			{
				KPrefabID component = worldItems[i].GetComponent<KPrefabID>();
				if (!MosquitoHungerMonitor.IsVictimForbidden(smi, component, true))
				{
					list.Add(component);
				}
			}
		}
		for (int j = 0; j < cavityForCell.creatures.Count; j++)
		{
			KPrefabID kprefabID = cavityForCell.creatures[j];
			if (kprefabID.HasAnyTags(smi.master.AllowedTargetTags) && !MosquitoHungerMonitor.IsVictimForbidden(smi, kprefabID, false))
			{
				list.Add(kprefabID);
			}
		}
		KPrefabID kprefabID2 = ((list.Count > 0) ? list.GetRandom<KPrefabID>() : null);
		smi.sm.victim.Set(kprefabID2, smi);
	}

	// Token: 0x060020CC RID: 8396 RVA: 0x000BD6C8 File Offset: 0x000BB8C8
	private static bool IsVictimForbidden(MosquitoHungerMonitor.Instance smi, KPrefabID victim, bool mustBeInSameCavity = false)
	{
		int num = Grid.PosToCell(victim);
		if (mustBeInSameCavity)
		{
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(smi));
			if (Game.Instance.roomProber.GetCavityForCell(num) != cavityForCell)
			{
				return true;
			}
		}
		if (victim.HasAnyTags(smi.master.ForbiddenTargetTags))
		{
			return true;
		}
		Effects component = victim.GetComponent<Effects>();
		if (component.HasEffect("DupeMosquitoBite") || component.HasEffect("CritterMosquitoBite") || component.HasEffect("DupeMosquitoBiteSuppressed") || component.HasEffect("CritterMosquitoBiteSuppressed"))
		{
			return true;
		}
		OccupyArea component2 = victim.GetComponent<OccupyArea>();
		return !smi.navigator.CanReach(num, component2.OccupiedCellsOffsets);
	}

	// Token: 0x060020CD RID: 8397 RVA: 0x000BD77C File Offset: 0x000BB97C
	public static void InitiatePokeBehaviour(MosquitoHungerMonitor.Instance smi)
	{
		PokeMonitor.Instance smi2 = smi.GetSMI<PokeMonitor.Instance>();
		CellOffset[] array = smi.Victim.GetComponent<OccupyArea>().OccupiedCellsOffsets;
		for (int i = 0; i < 1; i++)
		{
			array = array.Expand();
		}
		smi2.InitiatePoke(smi.Victim, array);
	}

	// Token: 0x060020CE RID: 8398 RVA: 0x000BD7C4 File Offset: 0x000BB9C4
	public static void AbortPokeBehaviour(MosquitoHungerMonitor.Instance smi)
	{
		PokeMonitor.Instance smi2 = smi.GetSMI<PokeMonitor.Instance>();
		if (smi2 != null)
		{
			smi2.AbortPoke();
		}
	}

	// Token: 0x060020CF RID: 8399 RVA: 0x000BD7E4 File Offset: 0x000BB9E4
	public static void OnVictimPoked(MosquitoHungerMonitor.Instance smi, object victimOBJ)
	{
		if (victimOBJ == null)
		{
			return;
		}
		GameObject gameObject = (GameObject)victimOBJ;
		Effects component = gameObject.GetComponent<Effects>();
		bool flag = gameObject.HasTag(GameTags.BaseMinion);
		bool flag2 = false;
		foreach (string text in MosquitoHungerMonitor.ImmunityEffectNames)
		{
			flag2 = flag2 || component.HasEffect(text);
		}
		if (flag)
		{
			component.Add(flag2 ? "DupeMosquitoBiteSuppressed" : "DupeMosquitoBite", true);
		}
		else
		{
			component.Add(flag2 ? "CritterMosquitoBiteSuppressed" : "CritterMosquitoBite", true);
		}
		smi.ApplyFedEffect();
	}

	// Token: 0x04001313 RID: 4883
	public const string DupeMosquitoBiteEffectName = "DupeMosquitoBite";

	// Token: 0x04001314 RID: 4884
	public const string CritterMosquitoBiteEffectName = "CritterMosquitoBite";

	// Token: 0x04001315 RID: 4885
	public const string Dupe_SUPPRESSED_MosquitoBiteEffectName = "DupeMosquitoBiteSuppressed";

	// Token: 0x04001316 RID: 4886
	public const string Critter_SUPPRESSED_MosquitoBiteEffectName = "CritterMosquitoBiteSuppressed";

	// Token: 0x04001317 RID: 4887
	public const string MosquitoFedEffectName = "MosquitoFed";

	// Token: 0x04001318 RID: 4888
	public const int ReachabilityPadding = 1;

	// Token: 0x04001319 RID: 4889
	public bool CanBiteMinions = true;

	// Token: 0x0400131A RID: 4890
	public List<Tag> AllowedTargetTags;

	// Token: 0x0400131B RID: 4891
	public List<Tag> ForbiddenTargetTags;

	// Token: 0x0400131C RID: 4892
	public static string[] ImmunityEffectNames = new string[] { "HistamineSuppression" };

	// Token: 0x0200141A RID: 5146
	public class States : GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor>
	{
		// Token: 0x06008C7F RID: 35967 RVA: 0x00356008 File Offset: 0x00354208
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Never;
			default_state = this.satisfied;
			this.satisfied.EventTransition(GameHashes.EffectRemoved, this.hungry, GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.Not(new StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.Transition.ConditionCallback(MosquitoHungerMonitor.IsFed))).Enter(new StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.State.Callback(MosquitoHungerMonitor.ClearTarget));
			this.hungry.EventTransition(GameHashes.EffectAdded, this.satisfied, new StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.Transition.ConditionCallback(MosquitoHungerMonitor.IsFed)).DefaultState(this.hungry.lookingForVictim);
			this.hungry.lookingForVictim.ToggleStatusItem(CREATURES.STATUSITEMS.HUNGRY.NAME, CREATURES.STATUSITEMS.HUNGRY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, null).ParamTransition<GameObject>(this.victim, this.hungry.chaseVictim, GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.IsNotNull).PreBrainUpdate(new Action<MosquitoHungerMonitor.Instance>(MosquitoHungerMonitor.LookForVictim));
			this.hungry.chaseVictim.ParamTransition<GameObject>(this.victim, this.hungry.lookingForVictim, GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.IsNull).EventTransition(GameHashes.TargetLost, this.hungry.lookingForVictim, null).Enter(new StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.State.Callback(MosquitoHungerMonitor.InitiatePokeBehaviour))
				.EventHandler(GameHashes.EntityPoked, new GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.GameEvent.Callback(MosquitoHungerMonitor.OnVictimPoked))
				.Exit(new StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.State.Callback(MosquitoHungerMonitor.AbortPokeBehaviour))
				.Exit(new StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.State.Callback(MosquitoHungerMonitor.ClearTarget))
				.Target(this.victim)
				.EventTransition(GameHashes.TagsChanged, this.hungry.lookingForVictim, GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.Not(new StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.Transition.ConditionCallback(MosquitoHungerMonitor.HasValidVictim)))
				.EventTransition(GameHashes.EffectAdded, this.hungry.lookingForVictim, GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.Not(new StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.Transition.ConditionCallback(MosquitoHungerMonitor.HasValidVictim)));
		}

		// Token: 0x04006BAF RID: 27567
		public GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.State satisfied;

		// Token: 0x04006BB0 RID: 27568
		public MosquitoHungerMonitor.States.HungryStates hungry;

		// Token: 0x04006BB1 RID: 27569
		public StateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.TargetParameter victim;

		// Token: 0x02002739 RID: 10041
		public class HungryStates : GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.State
		{
			// Token: 0x0400AD2E RID: 44334
			public GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.State lookingForVictim;

			// Token: 0x0400AD2F RID: 44335
			public GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.State chaseVictim;
		}
	}

	// Token: 0x0200141B RID: 5147
	public class Instance : GameStateMachine<MosquitoHungerMonitor.States, MosquitoHungerMonitor.Instance, MosquitoHungerMonitor, object>.GameInstance
	{
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06008C81 RID: 35969 RVA: 0x003561DF File Offset: 0x003543DF
		public GameObject Victim
		{
			get
			{
				return base.sm.victim.Get(this);
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06008C82 RID: 35970 RVA: 0x003561F2 File Offset: 0x003543F2
		public bool IsFed
		{
			get
			{
				return this.effects.HasEffect("MosquitoFed");
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06008C84 RID: 35972 RVA: 0x0035620D File Offset: 0x0035440D
		// (set) Token: 0x06008C83 RID: 35971 RVA: 0x00356204 File Offset: 0x00354404
		public Navigator navigator { get; private set; }

		// Token: 0x06008C85 RID: 35973 RVA: 0x00356215 File Offset: 0x00354415
		public Instance(MosquitoHungerMonitor master)
			: base(master)
		{
			this.effects = base.GetComponent<Effects>();
			this.navigator = base.GetComponent<Navigator>();
		}

		// Token: 0x06008C86 RID: 35974 RVA: 0x00356236 File Offset: 0x00354436
		public void ApplyFedEffect()
		{
			this.effects.Add("MosquitoFed", true);
		}

		// Token: 0x04006BB3 RID: 27571
		private Effects effects;
	}
}
