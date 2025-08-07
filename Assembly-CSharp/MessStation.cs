using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000785 RID: 1925
[AddComponentMenu("KMonoBehaviour/Workable/MessStation")]
public class MessStation : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x060032C8 RID: 13000 RVA: 0x0011DD9A File Offset: 0x0011BF9A
	protected override void OnPrefabInit()
	{
		this.ownable.AddAssignPrecondition(new Func<MinionAssignablesProxy, bool>(this.HasCaloriesOwnablePrecondition));
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_machine_kanim") };
	}

	// Token: 0x060032C9 RID: 13001 RVA: 0x0011DDD8 File Offset: 0x0011BFD8
	public static bool CanBeAssignedTo(IAssignableIdentity assignee)
	{
		MinionAssignablesProxy minionAssignablesProxy = assignee as MinionAssignablesProxy;
		if (minionAssignablesProxy == null)
		{
			return false;
		}
		MinionIdentity minionIdentity = minionAssignablesProxy.target as MinionIdentity;
		return !(minionIdentity == null) && (Db.Get().Amounts.Calories.Lookup(minionIdentity) != null || (Game.IsDlcActiveForCurrentSave("DLC3_ID") && minionIdentity.model == BionicMinionConfig.MODEL));
	}

	// Token: 0x060032CA RID: 13002 RVA: 0x0011DE45 File Offset: 0x0011C045
	private bool HasCaloriesOwnablePrecondition(MinionAssignablesProxy worker)
	{
		return MessStation.CanBeAssignedTo(worker);
	}

	// Token: 0x060032CB RID: 13003 RVA: 0x0011DE4D File Offset: 0x0011C04D
	protected override void OnCompleteWork(WorkerBase worker)
	{
		worker.GetWorkable().GetComponent<Edible>().CompleteWork(worker);
	}

	// Token: 0x060032CC RID: 13004 RVA: 0x0011DE60 File Offset: 0x0011C060
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new MessStation.MessStationSM.Instance(this);
		this.smi.StartSM();
	}

	// Token: 0x060032CD RID: 13005 RVA: 0x0011DE80 File Offset: 0x0011C080
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (go.GetComponent<Storage>().Has(TableSaltConfig.ID.ToTag()))
		{
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.MESS_TABLE_SALT, TableSaltTuning.MORALE_MODIFIER), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.MESS_TABLE_SALT, TableSaltTuning.MORALE_MODIFIER), Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x060032CE RID: 13006 RVA: 0x0011DEEA File Offset: 0x0011C0EA
	public bool HasSalt
	{
		get
		{
			return this.smi.HasSalt;
		}
	}

	// Token: 0x04001E75 RID: 7797
	[MyCmpGet]
	private Ownable ownable;

	// Token: 0x04001E76 RID: 7798
	private MessStation.MessStationSM.Instance smi;

	// Token: 0x02001677 RID: 5751
	public class MessStationSM : GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation>
	{
		// Token: 0x0600954C RID: 38220 RVA: 0x003738CC File Offset: 0x00371ACC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.salt.none;
			this.salt.none.Transition(this.salt.salty, (MessStation.MessStationSM.Instance smi) => smi.HasSalt, UpdateRate.SIM_200ms).PlayAnim("off");
			this.salt.salty.Transition(this.salt.none, (MessStation.MessStationSM.Instance smi) => !smi.HasSalt, UpdateRate.SIM_200ms).PlayAnim("salt").EventTransition(GameHashes.EatStart, this.eating, null);
			this.eating.Transition(this.salt.salty, (MessStation.MessStationSM.Instance smi) => smi.HasSalt && !smi.IsEating(), UpdateRate.SIM_200ms).Transition(this.salt.none, (MessStation.MessStationSM.Instance smi) => !smi.HasSalt && !smi.IsEating(), UpdateRate.SIM_200ms).PlayAnim("off");
		}

		// Token: 0x040072CE RID: 29390
		public MessStation.MessStationSM.SaltState salt;

		// Token: 0x040072CF RID: 29391
		public GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.State eating;

		// Token: 0x020027B1 RID: 10161
		public class SaltState : GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.State
		{
			// Token: 0x0400AFDE RID: 45022
			public GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.State none;

			// Token: 0x0400AFDF RID: 45023
			public GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.State salty;
		}

		// Token: 0x020027B2 RID: 10162
		public new class Instance : GameStateMachine<MessStation.MessStationSM, MessStation.MessStationSM.Instance, MessStation, object>.GameInstance
		{
			// Token: 0x0600C8BF RID: 51391 RVA: 0x00414FD1 File Offset: 0x004131D1
			public Instance(MessStation master)
				: base(master)
			{
				this.saltStorage = master.GetComponent<Storage>();
				this.assigned = master.GetComponent<Assignable>();
			}

			// Token: 0x17000CD8 RID: 3288
			// (get) Token: 0x0600C8C0 RID: 51392 RVA: 0x00414FF2 File Offset: 0x004131F2
			public bool HasSalt
			{
				get
				{
					return this.saltStorage.Has(TableSaltConfig.ID.ToTag());
				}
			}

			// Token: 0x0600C8C1 RID: 51393 RVA: 0x0041500C File Offset: 0x0041320C
			public bool IsEating()
			{
				if (this.assigned == null || this.assigned.assignee == null)
				{
					return false;
				}
				Ownables soleOwner = this.assigned.assignee.GetSoleOwner();
				if (soleOwner == null)
				{
					return false;
				}
				GameObject targetGameObject = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject == null)
				{
					return false;
				}
				ChoreDriver component = targetGameObject.GetComponent<ChoreDriver>();
				if (component == null)
				{
					return false;
				}
				if (!component.HasChore())
				{
					return false;
				}
				ReloadElectrobankChore reloadElectrobankChore = component.GetCurrentChore() as ReloadElectrobankChore;
				if (reloadElectrobankChore != null)
				{
					return reloadElectrobankChore.IsInstallingAtMessStation();
				}
				return component.GetCurrentChore().choreType.urge == Db.Get().Urges.Eat;
			}

			// Token: 0x0400AFE0 RID: 45024
			private Storage saltStorage;

			// Token: 0x0400AFE1 RID: 45025
			private Assignable assigned;
		}
	}
}
