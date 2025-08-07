using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200059E RID: 1438
public class PollinateMonitor : GameStateMachine<PollinateMonitor, PollinateMonitor.Instance, IStateMachineTarget, PollinateMonitor.Def>
{
	// Token: 0x060020D5 RID: 8405 RVA: 0x000BD8BC File Offset: 0x000BBABC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.lookingForPlant;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.lookingForPlant.PreBrainUpdate(new Action<PollinateMonitor.Instance>(PollinateMonitor.FindPollinateTarget)).ToggleBehaviour(GameTags.Creatures.WantsToPollinate, (PollinateMonitor.Instance smi) => smi.IsValidTarget(), delegate(PollinateMonitor.Instance smi)
		{
			smi.GoTo(this.satisfied);
		});
		this.satisfied.Enter(delegate(PollinateMonitor.Instance smi)
		{
			this.remainingSecondsForEffect.Set(ButterflyTuning.SEARCH_COOLDOWN, smi, false);
		}).ScheduleGoTo((PollinateMonitor.Instance smi) => this.remainingSecondsForEffect.Get(smi), this.lookingForPlant);
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x000BD954 File Offset: 0x000BBB54
	private static void FindPollinateTarget(PollinateMonitor.Instance smi)
	{
		if (smi.IsValidTarget())
		{
			return;
		}
		KPrefabID kprefabID = smi.def.PlantSeeker.Scan(Grid.PosToXY(smi.transform.GetPosition()), smi.navigator);
		GameObject gameObject = ((kprefabID != null) ? kprefabID.gameObject : null);
		if (gameObject != smi.target)
		{
			if (gameObject == null)
			{
				smi.target = null;
				smi.targetCell = -1;
			}
			else
			{
				smi.target = gameObject;
				smi.targetCell = Grid.PosToCell(smi.target);
			}
			smi.Trigger(-255880159, null);
		}
	}

	// Token: 0x0400131D RID: 4893
	public static Tag ID = new Tag("PollinateMonitor");

	// Token: 0x0400131E RID: 4894
	public GameStateMachine<PollinateMonitor, PollinateMonitor.Instance, IStateMachineTarget, PollinateMonitor.Def>.State lookingForPlant;

	// Token: 0x0400131F RID: 4895
	public GameStateMachine<PollinateMonitor, PollinateMonitor.Instance, IStateMachineTarget, PollinateMonitor.Def>.State satisfied;

	// Token: 0x04001320 RID: 4896
	private StateMachine<PollinateMonitor, PollinateMonitor.Instance, IStateMachineTarget, PollinateMonitor.Def>.FloatParameter remainingSecondsForEffect;

	// Token: 0x0200141E RID: 5150
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06008C8E RID: 35982 RVA: 0x00356380 File Offset: 0x00354580
		public Navigator.Scanner<KPrefabID> PlantSeeker
		{
			get
			{
				if (this.plantSeeker == null)
				{
					this.plantSeeker = new Navigator.Scanner<KPrefabID>(this.radius, GameScenePartitioner.Instance.plants, new Func<KPrefabID, bool>(PollinateMonitor.Def.IsHarvestablePlant));
					this.plantSeeker.SetEarlyOutThreshold(5);
				}
				return this.plantSeeker;
			}
		}

		// Token: 0x06008C8F RID: 35983 RVA: 0x003563D0 File Offset: 0x003545D0
		private static bool IsHarvestablePlant(KPrefabID plant)
		{
			if (plant == null)
			{
				return false;
			}
			if (plant.HasTag(GameTags.Creatures.ReservedByCreature))
			{
				return false;
			}
			if (plant.HasTag("ButterflyPlant"))
			{
				return false;
			}
			if (!plant.HasTag(GameTags.GrowingPlant))
			{
				return false;
			}
			if (plant.HasTag(GameTags.FullyGrown))
			{
				return false;
			}
			Effects component = plant.GetComponent<Effects>();
			if (component == null)
			{
				return false;
			}
			for (int i = 0; i < PollinationMonitor.PollinationEffects.Length; i++)
			{
				HashedString hashedString = PollinationMonitor.PollinationEffects[i];
				if (component.HasEffect(hashedString))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04006BB8 RID: 27576
		public int radius = 10;

		// Token: 0x04006BB9 RID: 27577
		private Navigator.Scanner<KPrefabID> plantSeeker;
	}

	// Token: 0x0200141F RID: 5151
	public new class Instance : GameStateMachine<PollinateMonitor, PollinateMonitor.Instance, IStateMachineTarget, PollinateMonitor.Def>.GameInstance, IApproachableBehaviour, ICreatureMonitor
	{
		// Token: 0x06008C91 RID: 35985 RVA: 0x00356475 File Offset: 0x00354675
		public Instance(IStateMachineTarget master, PollinateMonitor.Def def)
			: base(master, def)
		{
			this.navigator = master.GetComponent<Navigator>();
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06008C92 RID: 35986 RVA: 0x0035648B File Offset: 0x0035468B
		public Tag Id
		{
			get
			{
				return PollinateMonitor.ID;
			}
		}

		// Token: 0x06008C93 RID: 35987 RVA: 0x00356492 File Offset: 0x00354692
		public bool IsValidTarget()
		{
			return !this.target.IsNullOrDestroyed() && this.navigator.GetNavigationCost(this.targetCell) != -1;
		}

		// Token: 0x06008C94 RID: 35988 RVA: 0x003564BA File Offset: 0x003546BA
		public GameObject GetTarget()
		{
			return this.target;
		}

		// Token: 0x06008C95 RID: 35989 RVA: 0x003564C2 File Offset: 0x003546C2
		public StatusItem GetApproachStatusItem()
		{
			return Db.Get().CreatureStatusItems.TravelingToPollinate;
		}

		// Token: 0x06008C96 RID: 35990 RVA: 0x003564D3 File Offset: 0x003546D3
		public StatusItem GetBehaviourStatusItem()
		{
			return Db.Get().CreatureStatusItems.Pollinating;
		}

		// Token: 0x06008C97 RID: 35991 RVA: 0x003564E4 File Offset: 0x003546E4
		public void OnSuccess()
		{
			Effects component = this.target.GetComponent<Effects>();
			if (component != null)
			{
				component.Add(Db.Get().effects.Get("ButterflyPollinated"), true);
			}
			this.target = null;
		}

		// Token: 0x04006BBA RID: 27578
		public GameObject target;

		// Token: 0x04006BBB RID: 27579
		public int targetCell;

		// Token: 0x04006BBC RID: 27580
		public Navigator navigator;
	}
}
