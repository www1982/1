using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000A18 RID: 2584
public class ThreatMonitor : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>
{
	// Token: 0x06004B19 RID: 19225 RVA: 0x001B3C40 File Offset: 0x001B1E40
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.safe;
		this.root.EventHandler(GameHashes.SafeFromThreats, delegate(ThreatMonitor.Instance smi, object d)
		{
			smi.OnSafe(d);
		}).EventHandler(GameHashes.Attacked, delegate(ThreatMonitor.Instance smi, object d)
		{
			smi.OnAttacked(d);
		}).EventHandler(GameHashes.ObjectDestroyed, delegate(ThreatMonitor.Instance smi, object d)
		{
			smi.Cleanup(d);
		});
		this.safe.Enter(delegate(ThreatMonitor.Instance smi)
		{
			smi.revengeThreat.Clear();
		}).Enter(new StateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State.Callback(ThreatMonitor.SeekThreats)).EventHandler(GameHashes.FactionChanged, new StateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State.Callback(ThreatMonitor.SeekThreats));
		this.safe.passive.DoNothing();
		this.safe.seeking.PreBrainUpdate(delegate(ThreatMonitor.Instance smi)
		{
			smi.RefreshThreat(null);
		});
		this.threatened.duplicant.Transition(this.safe, GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.Not(new StateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.Transition.ConditionCallback(ThreatMonitor.DupeHasValidTarget)), UpdateRate.SIM_200ms);
		this.threatened.duplicant.ShouldFight.ToggleChore(new Func<ThreatMonitor.Instance, Chore>(this.CreateAttackChore), this.safe).Update("DupeUpdateTarget", new Action<ThreatMonitor.Instance, float>(ThreatMonitor.DupeUpdateTarget), UpdateRate.SIM_200ms, false);
		this.threatened.duplicant.ShoudFlee.ToggleChore(new Func<ThreatMonitor.Instance, Chore>(this.CreateFleeChore), this.safe);
		this.threatened.creature.ToggleBehaviour(GameTags.Creatures.Flee, (ThreatMonitor.Instance smi) => !smi.WillFight(), delegate(ThreatMonitor.Instance smi)
		{
			smi.GoTo(this.safe);
		}).ToggleBehaviour(GameTags.Creatures.Attack, (ThreatMonitor.Instance smi) => smi.WillFight(), delegate(ThreatMonitor.Instance smi)
		{
			smi.GoTo(this.safe);
		}).Update("CritterCalmUpdate", new Action<ThreatMonitor.Instance, float>(ThreatMonitor.CritterCalmUpdate), UpdateRate.SIM_200ms, false)
			.PreBrainUpdate(new Action<ThreatMonitor.Instance>(ThreatMonitor.CritterUpdateThreats));
	}

	// Token: 0x06004B1A RID: 19226 RVA: 0x001B3E9C File Offset: 0x001B209C
	private static void SeekThreats(ThreatMonitor.Instance smi)
	{
		Faction faction = FactionManager.Instance.GetFaction(smi.alignment.Alignment);
		if (smi.IAmADuplicant || faction.CanAttack)
		{
			smi.GoTo(smi.sm.safe.seeking);
			return;
		}
		smi.GoTo(smi.sm.safe.passive);
	}

	// Token: 0x06004B1B RID: 19227 RVA: 0x001B3EFC File Offset: 0x001B20FC
	private static bool DupeHasValidTarget(ThreatMonitor.Instance smi)
	{
		bool flag = false;
		if (smi.MainThreat != null && smi.MainThreat.GetComponent<FactionAlignment>().IsPlayerTargeted())
		{
			IApproachable component = smi.MainThreat.GetComponent<RangedAttackable>();
			if (component != null)
			{
				flag = smi.navigator.GetNavigationCost(component) != -1;
			}
		}
		return flag;
	}

	// Token: 0x06004B1C RID: 19228 RVA: 0x001B3F4E File Offset: 0x001B214E
	private static void DupeUpdateTarget(ThreatMonitor.Instance smi, float dt)
	{
		if (!ThreatMonitor.DupeHasValidTarget(smi))
		{
			smi.Trigger(2144432245, null);
		}
	}

	// Token: 0x06004B1D RID: 19229 RVA: 0x001B3F64 File Offset: 0x001B2164
	private static void CritterCalmUpdate(ThreatMonitor.Instance smi, float dt)
	{
		if (smi.isMasterNull)
		{
			return;
		}
		if (smi.revengeThreat.target != null && smi.revengeThreat.Calm(dt, smi.alignment))
		{
			smi.Trigger(-21431934, null);
		}
	}

	// Token: 0x06004B1E RID: 19230 RVA: 0x001B3FA2 File Offset: 0x001B21A2
	private static void CritterUpdateThreats(ThreatMonitor.Instance smi)
	{
		if (smi.isMasterNull)
		{
			return;
		}
		if (!smi.CheckForThreats() && !ThreatMonitor.IsInSafeState(smi))
		{
			smi.GoTo(smi.sm.safe);
		}
	}

	// Token: 0x06004B1F RID: 19231 RVA: 0x001B3FCE File Offset: 0x001B21CE
	private static bool IsInSafeState(ThreatMonitor.Instance smi)
	{
		return smi.GetCurrentState() == smi.sm.safe.passive || smi.GetCurrentState() == smi.sm.safe.seeking;
	}

	// Token: 0x06004B20 RID: 19232 RVA: 0x001B4002 File Offset: 0x001B2202
	private Chore CreateAttackChore(ThreatMonitor.Instance smi)
	{
		return new AttackChore(smi.master, smi.MainThreat);
	}

	// Token: 0x06004B21 RID: 19233 RVA: 0x001B4015 File Offset: 0x001B2215
	private Chore CreateFleeChore(ThreatMonitor.Instance smi)
	{
		return new FleeChore(smi.master, smi.MainThreat);
	}

	// Token: 0x040031CE RID: 12750
	public ThreatMonitor.SafeStates safe;

	// Token: 0x040031CF RID: 12751
	public ThreatMonitor.ThreatenedStates threatened;

	// Token: 0x02001ABE RID: 6846
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040080BB RID: 32955
		public Health.HealthState fleethresholdState = Health.HealthState.Injured;

		// Token: 0x040080BC RID: 32956
		public Tag[] friendlyCreatureTags;

		// Token: 0x040080BD RID: 32957
		public int maxSearchEntities = 50;

		// Token: 0x040080BE RID: 32958
		public int maxSearchDistance = 20;

		// Token: 0x040080BF RID: 32959
		public CellOffset[] offsets = OffsetGroups.Use;
	}

	// Token: 0x02001ABF RID: 6847
	public class SafeStates : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State
	{
		// Token: 0x040080C0 RID: 32960
		public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State passive;

		// Token: 0x040080C1 RID: 32961
		public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State seeking;
	}

	// Token: 0x02001AC0 RID: 6848
	public class ThreatenedStates : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State
	{
		// Token: 0x040080C2 RID: 32962
		public ThreatMonitor.ThreatenedDuplicantStates duplicant;

		// Token: 0x040080C3 RID: 32963
		public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State creature;
	}

	// Token: 0x02001AC1 RID: 6849
	public class ThreatenedDuplicantStates : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State
	{
		// Token: 0x040080C4 RID: 32964
		public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State ShoudFlee;

		// Token: 0x040080C5 RID: 32965
		public GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.State ShouldFight;
	}

	// Token: 0x02001AC2 RID: 6850
	public struct Grudge
	{
		// Token: 0x0600A4E1 RID: 42209 RVA: 0x003A74A4 File Offset: 0x003A56A4
		public void Reset(FactionAlignment revengeTarget)
		{
			this.target = revengeTarget;
			float num = 10f;
			this.grudgeTime = num;
		}

		// Token: 0x0600A4E2 RID: 42210 RVA: 0x003A74C8 File Offset: 0x003A56C8
		public bool Calm(float dt, FactionAlignment self)
		{
			if (this.grudgeTime <= 0f)
			{
				return true;
			}
			this.grudgeTime = Mathf.Max(0f, this.grudgeTime - dt);
			if (this.grudgeTime == 0f)
			{
				if (FactionManager.Instance.GetDisposition(self.Alignment, this.target.Alignment) != FactionManager.Disposition.Attack)
				{
					PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, UI.GAMEOBJECTEFFECTS.FORGAVEATTACKER, self.transform, 2f, true);
				}
				this.Clear();
				return true;
			}
			return false;
		}

		// Token: 0x0600A4E3 RID: 42211 RVA: 0x003A755B File Offset: 0x003A575B
		public void Clear()
		{
			this.grudgeTime = 0f;
			this.target = null;
		}

		// Token: 0x0600A4E4 RID: 42212 RVA: 0x003A7570 File Offset: 0x003A5770
		public bool IsValidRevengeTarget(bool isDuplicant)
		{
			return this.target != null && this.target.IsAlignmentActive() && (this.target.health == null || !this.target.health.IsDefeated()) && (!isDuplicant || !this.target.IsPlayerTargeted());
		}

		// Token: 0x040080C6 RID: 32966
		public FactionAlignment target;

		// Token: 0x040080C7 RID: 32967
		public float grudgeTime;
	}

	// Token: 0x02001AC3 RID: 6851
	public new class Instance : GameStateMachine<ThreatMonitor, ThreatMonitor.Instance, IStateMachineTarget, ThreatMonitor.Def>.GameInstance
	{
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600A4E5 RID: 42213 RVA: 0x003A75D2 File Offset: 0x003A57D2
		public GameObject MainThreat
		{
			get
			{
				return this.mainThreat;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600A4E6 RID: 42214 RVA: 0x003A75DA File Offset: 0x003A57DA
		public bool IAmADuplicant
		{
			get
			{
				return this.alignment.Alignment == FactionManager.FactionID.Duplicant;
			}
		}

		// Token: 0x0600A4E7 RID: 42215 RVA: 0x003A75EC File Offset: 0x003A57EC
		public Instance(IStateMachineTarget master, ThreatMonitor.Def def)
			: base(master, def)
		{
			this.alignment = master.GetComponent<FactionAlignment>();
			this.navigator = master.GetComponent<Navigator>();
			this.choreDriver = master.GetComponent<ChoreDriver>();
			this.health = master.GetComponent<Health>();
			this.choreConsumer = master.GetComponent<ChoreConsumer>();
			this.refreshThreatDelegate = new Action<object>(this.RefreshThreat);
		}

		// Token: 0x0600A4E8 RID: 42216 RVA: 0x003A765A File Offset: 0x003A585A
		public void ClearMainThreat()
		{
			this.SetMainThreat(null);
		}

		// Token: 0x0600A4E9 RID: 42217 RVA: 0x003A7664 File Offset: 0x003A5864
		public void SetMainThreat(GameObject threat)
		{
			if (threat == this.mainThreat)
			{
				return;
			}
			if (this.mainThreat != null)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
				if (threat == null)
				{
					base.Trigger(2144432245, null);
				}
			}
			if (this.mainThreat != null)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
			}
			this.mainThreat = threat;
			if (this.mainThreat != null)
			{
				this.mainThreatFaction = this.mainThreat.GetComponent<FactionAlignment>().Alignment;
				this.mainThreat.Subscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Subscribe(1969584890, this.refreshThreatDelegate);
			}
		}

		// Token: 0x0600A4EA RID: 42218 RVA: 0x003A7762 File Offset: 0x003A5962
		public bool HasThreat()
		{
			return this.MainThreat != null;
		}

		// Token: 0x0600A4EB RID: 42219 RVA: 0x003A7770 File Offset: 0x003A5970
		public void OnSafe(object data)
		{
			if (this.revengeThreat.target != null)
			{
				if (!this.revengeThreat.target.GetComponent<FactionAlignment>().IsAlignmentActive())
				{
					this.revengeThreat.Clear();
				}
				this.ClearMainThreat();
			}
		}

		// Token: 0x0600A4EC RID: 42220 RVA: 0x003A77B0 File Offset: 0x003A59B0
		public void OnAttacked(object data)
		{
			FactionAlignment factionAlignment = (FactionAlignment)data;
			this.revengeThreat.Reset(factionAlignment);
			Game.BrainScheduler.PrioritizeBrain(base.GetComponent<Brain>());
			if (this.mainThreat == null)
			{
				this.SetMainThreat(factionAlignment.gameObject);
				this.GoToThreatened();
			}
			else if (!this.WillFight())
			{
				this.GoToThreatened();
			}
			if (factionAlignment.GetComponent<Bee>())
			{
				Chore chore = ((this.choreDriver != null) ? this.choreDriver.GetCurrentChore() : null);
				if (chore != null && chore.gameObject.GetComponent<HiveWorkableEmpty>() != null)
				{
					chore.gameObject.GetComponent<HiveWorkableEmpty>().wasStung = true;
				}
			}
		}

		// Token: 0x0600A4ED RID: 42221 RVA: 0x003A7864 File Offset: 0x003A5A64
		public bool WillFight()
		{
			if (this.choreConsumer != null)
			{
				if (!this.choreConsumer.IsPermittedByUser(Db.Get().ChoreGroups.Combat))
				{
					return false;
				}
				if (!this.choreConsumer.IsPermittedByTraits(Db.Get().ChoreGroups.Combat))
				{
					return false;
				}
			}
			return (this.IAmADuplicant || base.smi.mainThreatFaction != FactionManager.FactionID.Predator) && this.health.State < base.smi.def.fleethresholdState;
		}

		// Token: 0x0600A4EE RID: 42222 RVA: 0x003A78F8 File Offset: 0x003A5AF8
		private void GotoThreatResponse()
		{
			Chore currentChore = base.smi.master.GetComponent<ChoreDriver>().GetCurrentChore();
			if (this.WillFight() && this.mainThreat.GetComponent<FactionAlignment>().IsPlayerTargeted())
			{
				base.smi.GoTo(base.smi.sm.threatened.duplicant.ShouldFight);
				return;
			}
			if (currentChore != null && currentChore.target != null && currentChore.target != base.master && currentChore.target.GetComponent<Pickupable>() != null)
			{
				return;
			}
			base.smi.GoTo(base.smi.sm.threatened.duplicant.ShoudFlee);
		}

		// Token: 0x0600A4EF RID: 42223 RVA: 0x003A79AD File Offset: 0x003A5BAD
		public void GoToThreatened()
		{
			if (this.IAmADuplicant)
			{
				this.GotoThreatResponse();
				return;
			}
			base.smi.GoTo(base.sm.threatened.creature);
		}

		// Token: 0x0600A4F0 RID: 42224 RVA: 0x003A79D9 File Offset: 0x003A5BD9
		public void Cleanup(object data)
		{
			if (this.mainThreat)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
			}
		}

		// Token: 0x0600A4F1 RID: 42225 RVA: 0x003A7A14 File Offset: 0x003A5C14
		public void RefreshThreat(object data)
		{
			if (!base.IsRunning())
			{
				return;
			}
			if (base.smi.CheckForThreats())
			{
				this.GoToThreatened();
				return;
			}
			if (!ThreatMonitor.IsInSafeState(base.smi))
			{
				base.Trigger(-21431934, null);
				base.smi.GoTo(base.sm.safe);
			}
		}

		// Token: 0x0600A4F2 RID: 42226 RVA: 0x003A7A70 File Offset: 0x003A5C70
		public bool CheckForThreats()
		{
			if (base.isMasterNull)
			{
				return false;
			}
			GameObject gameObject;
			if (this.revengeThreat.IsValidRevengeTarget(this.IAmADuplicant))
			{
				gameObject = this.revengeThreat.target.gameObject;
			}
			else if (this.IAmADuplicant)
			{
				gameObject = this.FindThreatDuplicant();
			}
			else
			{
				gameObject = this.FindThreatOther();
			}
			this.SetMainThreat(gameObject);
			return gameObject != null;
		}

		// Token: 0x0600A4F3 RID: 42227 RVA: 0x003A7AD4 File Offset: 0x003A5CD4
		private GameObject FindThreatDuplicant()
		{
			this.threats.Clear();
			if (this.WillFight())
			{
				foreach (object obj in Components.PlayerTargeted)
				{
					FactionAlignment factionAlignment = (FactionAlignment)obj;
					if (!factionAlignment.IsNullOrDestroyed() && factionAlignment.IsPlayerTargeted() && !factionAlignment.health.IsDefeated() && this.navigator.CanReach(factionAlignment.attackable.GetCell(), base.smi.def.offsets))
					{
						this.threats.Add(factionAlignment);
					}
				}
			}
			return this.PickBestTarget(this.threats);
		}

		// Token: 0x0600A4F4 RID: 42228 RVA: 0x003A7B98 File Offset: 0x003A5D98
		private GameObject FindThreatOther()
		{
			this.threats.Clear();
			this.GatherThreats();
			return this.PickBestTarget(this.threats);
		}

		// Token: 0x0600A4F5 RID: 42229 RVA: 0x003A7BB8 File Offset: 0x003A5DB8
		private void GatherThreats()
		{
			ListPool<ScenePartitionerEntry, ThreatMonitor>.PooledList pooledList = ListPool<ScenePartitionerEntry, ThreatMonitor>.Allocate();
			Extents extents = new Extents(Grid.PosToCell(base.gameObject), base.def.maxSearchDistance);
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.attackableEntitiesLayer, pooledList);
			int count = pooledList.Count;
			int num = Mathf.Min(count, base.def.maxSearchEntities);
			for (int i = 0; i < num; i++)
			{
				if (this.currentUpdateIndex >= count)
				{
					this.currentUpdateIndex = 0;
				}
				ScenePartitionerEntry scenePartitionerEntry = pooledList[this.currentUpdateIndex];
				this.currentUpdateIndex++;
				FactionAlignment factionAlignment = scenePartitionerEntry.obj as FactionAlignment;
				if (!(factionAlignment.transform == null) && !(factionAlignment == this.alignment) && (base.def.friendlyCreatureTags == null || !factionAlignment.kprefabID.HasAnyTags(base.def.friendlyCreatureTags)) && factionAlignment.IsAlignmentActive() && FactionManager.Instance.GetDisposition(this.alignment.Alignment, factionAlignment.Alignment) == FactionManager.Disposition.Attack && this.navigator.CanReach(factionAlignment.attackable.GetCell(), base.smi.def.offsets))
				{
					this.threats.Add(factionAlignment);
				}
			}
			pooledList.Recycle();
		}

		// Token: 0x0600A4F6 RID: 42230 RVA: 0x003A7D14 File Offset: 0x003A5F14
		public GameObject PickBestTarget(List<FactionAlignment> threats)
		{
			float num = 1f;
			Vector2 vector = base.gameObject.transform.GetPosition();
			GameObject gameObject = null;
			float num2 = float.PositiveInfinity;
			for (int i = threats.Count - 1; i >= 0; i--)
			{
				FactionAlignment factionAlignment = threats[i];
				float num3 = Vector2.Distance(vector, factionAlignment.transform.GetPosition()) / num;
				if (num3 < num2)
				{
					num2 = num3;
					gameObject = factionAlignment.gameObject;
				}
			}
			return gameObject;
		}

		// Token: 0x040080C8 RID: 32968
		public FactionAlignment alignment;

		// Token: 0x040080C9 RID: 32969
		public Navigator navigator;

		// Token: 0x040080CA RID: 32970
		public ChoreDriver choreDriver;

		// Token: 0x040080CB RID: 32971
		private Health health;

		// Token: 0x040080CC RID: 32972
		private ChoreConsumer choreConsumer;

		// Token: 0x040080CD RID: 32973
		public ThreatMonitor.Grudge revengeThreat;

		// Token: 0x040080CE RID: 32974
		public int currentUpdateIndex;

		// Token: 0x040080CF RID: 32975
		private GameObject mainThreat;

		// Token: 0x040080D0 RID: 32976
		private FactionManager.FactionID mainThreatFaction;

		// Token: 0x040080D1 RID: 32977
		private List<FactionAlignment> threats = new List<FactionAlignment>();

		// Token: 0x040080D2 RID: 32978
		private Action<object> refreshThreatDelegate;
	}
}
