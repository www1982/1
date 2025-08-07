using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x020009D6 RID: 2518
public class BionicUpgradesMonitor : GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>
{
	// Token: 0x060049C7 RID: 18887 RVA: 0x001AB664 File Offset: 0x001A9864
	public static void CreateAssignableSlots(MinionAssignablesProxy minionAssignablesProxy)
	{
		AssignableSlot bionicUpgrade = Db.Get().AssignableSlots.BionicUpgrade;
		int num = Mathf.Max(0, 7);
		for (int i = 0; i < num; i++)
		{
			string text = (i + 2).ToString();
			BionicUpgradesMonitor.AddAssignableSlot(bionicUpgrade, text, minionAssignablesProxy);
		}
	}

	// Token: 0x060049C8 RID: 18888 RVA: 0x001AB6AC File Offset: 0x001A98AC
	private static void AddAssignableSlot(AssignableSlot bionicUpgradeSlot, string IDSufix, MinionAssignablesProxy minionAssignablesProxy)
	{
		Ownables component = minionAssignablesProxy.GetComponent<Ownables>();
		if (bionicUpgradeSlot is OwnableSlot)
		{
			OwnableSlotInstance ownableSlotInstance = new OwnableSlotInstance(component, (OwnableSlot)bionicUpgradeSlot);
			OwnableSlotInstance ownableSlotInstance2 = ownableSlotInstance;
			ownableSlotInstance2.ID += IDSufix;
			component.Add(ownableSlotInstance);
			return;
		}
		if (bionicUpgradeSlot is EquipmentSlot)
		{
			Equipment component2 = component.GetComponent<Equipment>();
			EquipmentSlotInstance equipmentSlotInstance = new EquipmentSlotInstance(component2, (EquipmentSlot)bionicUpgradeSlot);
			EquipmentSlotInstance equipmentSlotInstance2 = equipmentSlotInstance;
			equipmentSlotInstance2.ID += IDSufix;
			component2.Add(equipmentSlotInstance);
		}
	}

	// Token: 0x060049C9 RID: 18889 RVA: 0x001AB724 File Offset: 0x001A9924
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.initialize;
		this.initialize.Enter(new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State.Callback(BionicUpgradesMonitor.InitializeSlots)).EnterTransition(this.firstSpawn, new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.Transition.ConditionCallback(BionicUpgradesMonitor.IsFirstTimeSpawningThisBionic)).EnterGoTo(this.inactive);
		this.firstSpawn.Enter(new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State.Callback(BionicUpgradesMonitor.SpawnAndInstallInitialUpgrade));
		this.inactive.EventTransition(GameHashes.BionicOnline, this.active, new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.Transition.ConditionCallback(BionicUpgradesMonitor.IsBionicOnline)).Enter(new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State.Callback(BionicUpgradesMonitor.UpdateBatteryMonitorWattageModifiers));
		this.active.DefaultState(this.active.idle).EventTransition(GameHashes.BionicOffline, this.inactive, GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.Not(new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.Transition.ConditionCallback(BionicUpgradesMonitor.IsBionicOnline))).EventHandler(GameHashes.BionicUpgradeWattageChanged, new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State.Callback(BionicUpgradesMonitor.UpdateBatteryMonitorWattageModifiers))
			.Enter(new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State.Callback(BionicUpgradesMonitor.UpdateBatteryMonitorWattageModifiers));
		this.active.idle.OnSignal(this.UpgradeSlotAssignationChanged, this.active.seeking, new Func<BionicUpgradesMonitor.Instance, bool>(BionicUpgradesMonitor.WantsToInstallNewUpgrades));
		this.active.seeking.OnSignal(this.UpgradeSlotAssignationChanged, this.active.idle, new Func<BionicUpgradesMonitor.Instance, bool>(BionicUpgradesMonitor.DoesNotWantsToInstallNewUpgrades)).DefaultState(this.active.seeking.inProgress);
		this.active.seeking.inProgress.ToggleChore((BionicUpgradesMonitor.Instance smi) => new SeekAndInstallBionicUpgradeChore(smi.master), this.active.idle, this.active.seeking.failed);
		this.active.seeking.failed.EnterTransition(this.active.idle, new StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.Transition.ConditionCallback(BionicUpgradesMonitor.DoesNotWantsToInstallNewUpgrades)).GoTo(this.active.seeking.inProgress);
	}

	// Token: 0x060049CA RID: 18890 RVA: 0x001AB928 File Offset: 0x001A9B28
	public static void InitializeSlots(BionicUpgradesMonitor.Instance smi)
	{
		smi.InitializeSlots();
	}

	// Token: 0x060049CB RID: 18891 RVA: 0x001AB930 File Offset: 0x001A9B30
	public static bool IsBionicOnline(BionicUpgradesMonitor.Instance smi)
	{
		return smi.IsOnline;
	}

	// Token: 0x060049CC RID: 18892 RVA: 0x001AB938 File Offset: 0x001A9B38
	public static bool WantsToInstallNewUpgrades(BionicUpgradesMonitor.Instance smi)
	{
		return smi.HasAnyUpgradeAssigned;
	}

	// Token: 0x060049CD RID: 18893 RVA: 0x001AB940 File Offset: 0x001A9B40
	public static bool DoesNotWantsToInstallNewUpgrades(BionicUpgradesMonitor.Instance smi)
	{
		return !BionicUpgradesMonitor.WantsToInstallNewUpgrades(smi);
	}

	// Token: 0x060049CE RID: 18894 RVA: 0x001AB94B File Offset: 0x001A9B4B
	public static bool HasUpgradesInstalled(BionicUpgradesMonitor.Instance smi)
	{
		return smi.HasAnyUpgradeInstalled;
	}

	// Token: 0x060049CF RID: 18895 RVA: 0x001AB953 File Offset: 0x001A9B53
	public static bool IsFirstTimeSpawningThisBionic(BionicUpgradesMonitor.Instance smi)
	{
		return !smi.sm.InitialUpgradeSpawned.Get(smi);
	}

	// Token: 0x060049D0 RID: 18896 RVA: 0x001AB969 File Offset: 0x001A9B69
	public static void UpdateBatteryMonitorWattageModifiers(BionicUpgradesMonitor.Instance smi)
	{
		smi.UpdateBatteryMonitorWattageModifiers();
	}

	// Token: 0x060049D1 RID: 18897 RVA: 0x001AB974 File Offset: 0x001A9B74
	public static void SpawnAndInstallInitialUpgrade(BionicUpgradesMonitor.Instance smi)
	{
		string text = smi.GetComponent<Traits>().GetTraitIds().Find((string t) => DUPLICANTSTATS.BIONICUPGRADETRAITS.Find((DUPLICANTSTATS.TraitVal st) => st.id == t).id == t);
		if (text != null)
		{
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(BionicUpgradeComponentConfig.GetBionicUpgradePrefabIDWithTraitID(text)), smi.master.transform.position);
			gameObject.SetActive(true);
			IAssignableIdentity component = smi.GetComponent<IAssignableIdentity>();
			BionicUpgradeComponent component2 = gameObject.GetComponent<BionicUpgradeComponent>();
			component2.Assign(component);
			smi.InstallUpgrade(component2);
		}
		smi.sm.InitialUpgradeSpawned.Set(true, smi, false);
		smi.GoTo(smi.sm.inactive);
	}

	// Token: 0x040030A5 RID: 12453
	public const int MAX_POSSIBLE_SLOT_COUNT = 8;

	// Token: 0x040030A6 RID: 12454
	public GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State initialize;

	// Token: 0x040030A7 RID: 12455
	public GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State firstSpawn;

	// Token: 0x040030A8 RID: 12456
	public GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State inactive;

	// Token: 0x040030A9 RID: 12457
	public BionicUpgradesMonitor.ActiveStates active;

	// Token: 0x040030AA RID: 12458
	private StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.Signal UpgradeSlotAssignationChanged;

	// Token: 0x040030AB RID: 12459
	private StateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.BoolParameter InitialUpgradeSpawned;

	// Token: 0x02001A08 RID: 6664
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A09 RID: 6665
	public class SeekingStates : GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State
	{
		// Token: 0x04007E7A RID: 32378
		public GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State inProgress;

		// Token: 0x04007E7B RID: 32379
		public GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State failed;
	}

	// Token: 0x02001A0A RID: 6666
	public class ActiveStates : GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State
	{
		// Token: 0x04007E7C RID: 32380
		public GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.State idle;

		// Token: 0x04007E7D RID: 32381
		public BionicUpgradesMonitor.SeekingStates seeking;
	}

	// Token: 0x02001A0B RID: 6667
	public new class Instance : GameStateMachine<BionicUpgradesMonitor, BionicUpgradesMonitor.Instance, IStateMachineTarget, BionicUpgradesMonitor.Def>.GameInstance
	{
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x0600A1D4 RID: 41428 RVA: 0x0039F8DA File Offset: 0x0039DADA
		public bool IsOnline
		{
			get
			{
				return this.batteryMonitor != null && this.batteryMonitor.IsOnline;
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600A1D5 RID: 41429 RVA: 0x0039F8F1 File Offset: 0x0039DAF1
		public bool HasAnyUpgradeAssigned
		{
			get
			{
				return this.upgradeComponentSlots != null && this.GetAnyAssignedSlot() != null;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600A1D6 RID: 41430 RVA: 0x0039F906 File Offset: 0x0039DB06
		public bool HasAnyUpgradeInstalled
		{
			get
			{
				return this.upgradeComponentSlots != null && this.GetAnyInstalledUpgradeSlot() != null;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600A1D7 RID: 41431 RVA: 0x0039F91B File Offset: 0x0039DB1B
		public int UnlockedSlotCount
		{
			get
			{
				return Math.Clamp((int)base.gameObject.GetAttributes().Get(Db.Get().Attributes.BionicBoosterSlots.Id).GetTotalValue(), 0, 8);
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600A1D8 RID: 41432 RVA: 0x0039F950 File Offset: 0x0039DB50
		public int AssignedSlotCount
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
				{
					if (this.upgradeComponentSlots[i].assignedUpgradeComponent != null)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x0600A1D9 RID: 41433 RVA: 0x0039F98C File Offset: 0x0039DB8C
		public Instance(IStateMachineTarget master, BionicUpgradesMonitor.Def def)
			: base(master, def)
		{
			IAssignableIdentity component = base.GetComponent<IAssignableIdentity>();
			this.dataHolder = base.GetComponent<MinionStorageDataHolder>();
			MinionStorageDataHolder minionStorageDataHolder = this.dataHolder;
			minionStorageDataHolder.OnCopyBegins = (Action<StoredMinionIdentity>)Delegate.Combine(minionStorageDataHolder.OnCopyBegins, new Action<StoredMinionIdentity>(this.OnCopyMinionBegins));
			this.batteryMonitor = base.gameObject.GetSMI<BionicBatteryMonitor.Instance>();
			this.navigator = base.GetComponent<Navigator>();
			this.minionOwnables = component.GetSoleOwner();
			this.upgradesStorage = base.gameObject.GetComponents<Storage>().FindFirst((Storage s) => s.storageID == GameTags.StoragesIds.BionicUpgradeStorage);
			this.CreateUpgradeSlots();
			base.Subscribe(540773776, new Action<object>(this.OnSlotCountAttributeChanged));
			Game.Instance.Trigger(-1523247426, this);
		}

		// Token: 0x0600A1DA RID: 41434 RVA: 0x0039FA68 File Offset: 0x0039DC68
		private void OnCopyMinionBegins(StoredMinionIdentity destination)
		{
			Tag[] array = new Tag[this.upgradeComponentSlots.Length];
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				array[i] = this.upgradeComponentSlots[i].InstalledUpgradeID;
			}
			MinionStorageDataHolder.DataPackData dataPackData = new MinionStorageDataHolder.DataPackData
			{
				Bools = new bool[] { base.smi.sm.InitialUpgradeSpawned.Get(base.smi) },
				Tags = array
			};
			this.dataHolder.UpdateData(dataPackData);
		}

		// Token: 0x0600A1DB RID: 41435 RVA: 0x0039FAF0 File Offset: 0x0039DCF0
		public override void PostParamsInitialized()
		{
			MinionStorageDataHolder.DataPack dataPack = this.dataHolder.GetDataPack<BionicUpgradesMonitor.Instance>();
			if (dataPack != null && dataPack.IsStoringNewData)
			{
				MinionStorageDataHolder.DataPackData dataPackData = dataPack.ReadData();
				if (dataPackData != null)
				{
					base.sm.InitialUpgradeSpawned.Set(dataPackData.Bools[0], base.smi, false);
					if (dataPackData.Tags != null)
					{
						for (int i = 0; i < Mathf.Min(dataPackData.Tags.Length, this.upgradeComponentSlots.Length); i++)
						{
							Tag tag = dataPackData.Tags[i];
							this.upgradeComponentSlots[i].DeserializeAction_OverrideInstalledUpgradePrefabID(tag);
						}
					}
				}
			}
			base.PostParamsInitialized();
		}

		// Token: 0x0600A1DC RID: 41436 RVA: 0x0039FB87 File Offset: 0x0039DD87
		protected override void OnCleanUp()
		{
			if (this.dataHolder != null)
			{
				MinionStorageDataHolder minionStorageDataHolder = this.dataHolder;
				minionStorageDataHolder.OnCopyBegins = (Action<StoredMinionIdentity>)Delegate.Remove(minionStorageDataHolder.OnCopyBegins, new Action<StoredMinionIdentity>(this.OnCopyMinionBegins));
			}
			base.OnCleanUp();
		}

		// Token: 0x0600A1DD RID: 41437 RVA: 0x0039FBC4 File Offset: 0x0039DDC4
		public void LockSlot(BionicUpgradesMonitor.UpgradeComponentSlot slot)
		{
			this.UninstallUpgrade(slot);
			if (slot.HasUpgradeComponentAssigned && slot.HasSpawned)
			{
				slot.InternalUninstall();
			}
			slot.InternalLock();
		}

		// Token: 0x0600A1DE RID: 41438 RVA: 0x0039FBE9 File Offset: 0x0039DDE9
		public void UnlockSlot(BionicUpgradesMonitor.UpgradeComponentSlot slot)
		{
			slot.InternalUnlock();
		}

		// Token: 0x0600A1DF RID: 41439 RVA: 0x0039FBF4 File Offset: 0x0039DDF4
		public void InstallUpgrade(BionicUpgradeComponent upgradeComponent)
		{
			BionicUpgradesMonitor.UpgradeComponentSlot slotForAssignedUpgrade = this.GetSlotForAssignedUpgrade(upgradeComponent);
			if (slotForAssignedUpgrade == null)
			{
				return;
			}
			slotForAssignedUpgrade.InternalInstall();
			Game.Instance.Trigger(-1523247426, this);
		}

		// Token: 0x0600A1E0 RID: 41440 RVA: 0x0039FC23 File Offset: 0x0039DE23
		public void UninstallUpgrade(BionicUpgradesMonitor.UpgradeComponentSlot slot)
		{
			if (slot != null && slot.HasUpgradeInstalled)
			{
				slot.InternalUninstall();
				Game.Instance.Trigger(-1523247426, this);
			}
		}

		// Token: 0x0600A1E1 RID: 41441 RVA: 0x0039FC48 File Offset: 0x0039DE48
		public void UpdateBatteryMonitorWattageModifiers()
		{
			bool flag = true;
			bool flag2 = false;
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				flag &= this.upgradeComponentSlots[i].HasUpgradeInstalled;
				string text = "UPGRADE_SLOT_" + i.ToString();
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeComponentSlots[i];
				if (!upgradeComponentSlot.HasUpgradeInstalled)
				{
					flag2 |= this.batteryMonitor.RemoveModifier(text, false);
				}
				else
				{
					BionicBatteryMonitor.WattageModifier wattageModifier = new BionicBatteryMonitor.WattageModifier
					{
						id = text,
						name = upgradeComponentSlot.installedUpgradeComponent.CurrentWattageName,
						value = upgradeComponentSlot.installedUpgradeComponent.CurrentWattage,
						potentialValue = upgradeComponentSlot.installedUpgradeComponent.PotentialWattage
					};
					flag2 |= this.batteryMonitor.AddOrUpdateModifier(wattageModifier, false);
				}
			}
			if (flag2)
			{
				this.batteryMonitor.Trigger(1361471071, null);
			}
			if (flag)
			{
				SaveGame.Instance.ColonyAchievementTracker.fullyBoostedBionic = true;
			}
		}

		// Token: 0x0600A1E2 RID: 41442 RVA: 0x0039FD40 File Offset: 0x0039DF40
		private void OnSlotCountAttributeChanged(object data)
		{
			int unlockedSlotCount = this.UnlockedSlotCount;
			bool flag = false;
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeComponentSlots[i];
				bool flag2 = i >= unlockedSlotCount;
				if (upgradeComponentSlot.IsLocked != flag2)
				{
					flag = true;
					if (flag2)
					{
						this.LockSlot(upgradeComponentSlot);
					}
					else
					{
						this.UnlockSlot(upgradeComponentSlot);
					}
				}
			}
			this.UpdateBatteryMonitorWattageModifiers();
			if (flag)
			{
				base.Trigger(1095596132, null);
			}
		}

		// Token: 0x0600A1E3 RID: 41443 RVA: 0x0039FDB0 File Offset: 0x0039DFB0
		private void CreateUpgradeSlots()
		{
			AssignableSlot bionicUpgrade = Db.Get().AssignableSlots.BionicUpgrade;
			this.minionOwnables.GetSlots(bionicUpgrade);
			this.upgradeComponentSlots = new BionicUpgradesMonitor.UpgradeComponentSlot[8];
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = new BionicUpgradesMonitor.UpgradeComponentSlot();
				this.upgradeComponentSlots[i] = upgradeComponentSlot;
			}
		}

		// Token: 0x0600A1E4 RID: 41444 RVA: 0x0039FE08 File Offset: 0x0039E008
		public void InitializeSlots()
		{
			AssignableSlot bionicUpgrade = Db.Get().AssignableSlots.BionicUpgrade;
			AssignableSlotInstance[] slots = this.minionOwnables.GetSlots(bionicUpgrade);
			int unlockedSlotCount = this.UnlockedSlotCount;
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeComponentSlots[i];
				this.InitializeUpgradeSlot(upgradeComponentSlot, slots[i]);
			}
			for (int j = 0; j < this.upgradeComponentSlots.Length; j++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot2 = this.upgradeComponentSlots[j];
				upgradeComponentSlot2.OnSpawn(this);
				bool flag = j >= unlockedSlotCount;
				if (flag != upgradeComponentSlot2.IsLocked)
				{
					if (flag)
					{
						this.LockSlot(upgradeComponentSlot2);
					}
					else
					{
						this.UnlockSlot(upgradeComponentSlot2);
					}
				}
			}
		}

		// Token: 0x0600A1E5 RID: 41445 RVA: 0x0039FEB8 File Offset: 0x0039E0B8
		private void InitializeUpgradeSlot(BionicUpgradesMonitor.UpgradeComponentSlot slot, AssignableSlotInstance assignableSlotInstance)
		{
			slot.Initialize(assignableSlotInstance, this.upgradesStorage, this);
			slot.OnInstalledUpgradeReassigned = (Action<BionicUpgradesMonitor.UpgradeComponentSlot, IAssignableIdentity>)Delegate.Combine(slot.OnInstalledUpgradeReassigned, new Action<BionicUpgradesMonitor.UpgradeComponentSlot, IAssignableIdentity>(this.OnInstalledUpgradeComponentReassigned));
			slot.OnAssignedUpgradeChanged = (Action<BionicUpgradesMonitor.UpgradeComponentSlot>)Delegate.Combine(slot.OnAssignedUpgradeChanged, new Action<BionicUpgradesMonitor.UpgradeComponentSlot>(this.OnSlotAssignationChanged));
		}

		// Token: 0x0600A1E6 RID: 41446 RVA: 0x0039FF17 File Offset: 0x0039E117
		private void OnSlotAssignationChanged(BionicUpgradesMonitor.UpgradeComponentSlot slot)
		{
			base.sm.UpgradeSlotAssignationChanged.Trigger(this);
		}

		// Token: 0x0600A1E7 RID: 41447 RVA: 0x0039FF2A File Offset: 0x0039E12A
		private void OnInstalledUpgradeComponentReassigned(BionicUpgradesMonitor.UpgradeComponentSlot slot, IAssignableIdentity new_assignee)
		{
			if (!slot.AssignedUpgradeMatchesInstalledUpgrade)
			{
				this.UninstallUpgrade(slot);
			}
		}

		// Token: 0x0600A1E8 RID: 41448 RVA: 0x0039FF3C File Offset: 0x0039E13C
		private BionicUpgradesMonitor.UpgradeComponentSlot GetSlotForAssignedUpgrade(BionicUpgradeComponent upgradeComponent)
		{
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeComponentSlots[i];
				if (upgradeComponentSlot != null && !upgradeComponentSlot.HasUpgradeInstalled && upgradeComponentSlot.HasUpgradeComponentAssigned && upgradeComponentSlot.assignedUpgradeComponent == upgradeComponent)
				{
					return upgradeComponentSlot;
				}
			}
			return null;
		}

		// Token: 0x0600A1E9 RID: 41449 RVA: 0x0039FF8C File Offset: 0x0039E18C
		public BionicUpgradesMonitor.UpgradeComponentSlot GetAnyAssignedSlot()
		{
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeComponentSlots[i];
				if (upgradeComponentSlot != null && !upgradeComponentSlot.HasUpgradeInstalled && upgradeComponentSlot.HasUpgradeComponentAssigned)
				{
					return upgradeComponentSlot;
				}
			}
			return null;
		}

		// Token: 0x0600A1EA RID: 41450 RVA: 0x0039FFCC File Offset: 0x0039E1CC
		public BionicUpgradesMonitor.UpgradeComponentSlot GetAnyReachableAssignedSlot()
		{
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeComponentSlots[i];
				if (upgradeComponentSlot != null && !upgradeComponentSlot.HasUpgradeInstalled && upgradeComponentSlot.HasUpgradeComponentAssigned && this.IsBionicUpgradeComponentObjectAbleToBePickedUp(upgradeComponentSlot.assignedUpgradeComponent))
				{
					return upgradeComponentSlot;
				}
			}
			return null;
		}

		// Token: 0x0600A1EB RID: 41451 RVA: 0x003A001C File Offset: 0x0039E21C
		public bool IsBionicUpgradeComponentObjectAbleToBePickedUp(BionicUpgradeComponent upgradecComponent)
		{
			Pickupable component = upgradecComponent.GetComponent<Pickupable>();
			return !(component == null) && !component.KPrefabID.HasTag(GameTags.StoredPrivate) && component.CouldBePickedUpByMinion(base.GetComponent<KPrefabID>().InstanceID) && this.navigator.CanReach(component);
		}

		// Token: 0x0600A1EC RID: 41452 RVA: 0x003A0078 File Offset: 0x0039E278
		private BionicUpgradesMonitor.UpgradeComponentSlot GetAnyInstalledUpgradeSlot()
		{
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeComponentSlots[i];
				if (upgradeComponentSlot != null && upgradeComponentSlot.HasUpgradeInstalled)
				{
					return upgradeComponentSlot;
				}
			}
			return null;
		}

		// Token: 0x0600A1ED RID: 41453 RVA: 0x003A00B0 File Offset: 0x0039E2B0
		public BionicUpgradesMonitor.UpgradeComponentSlot GetFirstEmptyAvailableSlot()
		{
			for (int i = 0; i < this.upgradeComponentSlots.Length; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeComponentSlots[i];
				if (!upgradeComponentSlot.IsLocked && !upgradeComponentSlot.HasUpgradeInstalled && !upgradeComponentSlot.HasUpgradeComponentAssigned)
				{
					return upgradeComponentSlot;
				}
			}
			return null;
		}

		// Token: 0x0600A1EE RID: 41454 RVA: 0x003A00F4 File Offset: 0x0039E2F4
		public int CountBoosterAssignments(Tag boosterID)
		{
			int num = 0;
			foreach (BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot in this.upgradeComponentSlots)
			{
				if (!(upgradeComponentSlot.assignedUpgradeComponent == null) && upgradeComponentSlot.assignedUpgradeComponent.PrefabID() == boosterID)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04007E7E RID: 32382
		[Serialize]
		public BionicUpgradesMonitor.UpgradeComponentSlot[] upgradeComponentSlots;

		// Token: 0x04007E7F RID: 32383
		private BionicBatteryMonitor.Instance batteryMonitor;

		// Token: 0x04007E80 RID: 32384
		private Storage upgradesStorage;

		// Token: 0x04007E81 RID: 32385
		private Ownables minionOwnables;

		// Token: 0x04007E82 RID: 32386
		private MinionStorageDataHolder dataHolder;

		// Token: 0x04007E83 RID: 32387
		private Navigator navigator;

		// Token: 0x02002862 RID: 10338
		[SerializationConfig(MemberSerialization.OptIn)]
		private struct StorageDataHolderData
		{
			// Token: 0x0400B31E RID: 45854
			[Serialize]
			public bool initialUpgradesSpawned;

			// Token: 0x0400B31F RID: 45855
			[Serialize]
			public Tag[] upgradeComponentSlotsInstalledTags;
		}
	}

	// Token: 0x02001A0C RID: 6668
	[SerializationConfig(MemberSerialization.OptIn)]
	public class UpgradeComponentSlot
	{
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600A1EF RID: 41455 RVA: 0x003A0142 File Offset: 0x0039E342
		public bool HasUpgradeInstalled
		{
			get
			{
				return this.installedUpgradePrefabID != Tag.Invalid;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600A1F0 RID: 41456 RVA: 0x003A0154 File Offset: 0x0039E354
		public bool HasUpgradeComponentAssigned
		{
			get
			{
				return this.assignableSlotInstance.IsAssigned() && !this.assignableSlotInstance.IsUnassigning();
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600A1F1 RID: 41457 RVA: 0x003A0173 File Offset: 0x0039E373
		public bool AssignedUpgradeMatchesInstalledUpgrade
		{
			get
			{
				return this.assignedUpgradeComponent == this.installedUpgradeComponent;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x0600A1F3 RID: 41459 RVA: 0x003A018F File Offset: 0x0039E38F
		// (set) Token: 0x0600A1F2 RID: 41458 RVA: 0x003A0186 File Offset: 0x0039E386
		public bool HasSpawned { get; private set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x0600A1F5 RID: 41461 RVA: 0x003A01A0 File Offset: 0x0039E3A0
		// (set) Token: 0x0600A1F4 RID: 41460 RVA: 0x003A0197 File Offset: 0x0039E397
		public bool IsLocked { get; private set; }

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x0600A1F6 RID: 41462 RVA: 0x003A01A8 File Offset: 0x0039E3A8
		public float WattageCost
		{
			get
			{
				if (!this.HasUpgradeInstalled)
				{
					return 0f;
				}
				return this.installedUpgradeComponent.CurrentWattage;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x0600A1F7 RID: 41463 RVA: 0x003A01C3 File Offset: 0x0039E3C3
		public Func<StateMachine.Instance, StateMachine.Instance> StateMachine
		{
			get
			{
				if (!this.HasUpgradeInstalled)
				{
					return null;
				}
				return this.installedUpgradeComponent.StateMachine;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600A1F8 RID: 41464 RVA: 0x003A01DA File Offset: 0x0039E3DA
		public Tag InstalledUpgradeID
		{
			get
			{
				return this.installedUpgradePrefabID;
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600A1F9 RID: 41465 RVA: 0x003A01E2 File Offset: 0x0039E3E2
		public BionicUpgradeComponent assignedUpgradeComponent
		{
			get
			{
				if (!this.assignableSlotInstance.IsUnassigning())
				{
					return this.assignableSlotInstance.assignable as BionicUpgradeComponent;
				}
				return null;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600A1FA RID: 41466 RVA: 0x003A0204 File Offset: 0x0039E404
		public BionicUpgradeComponent installedUpgradeComponent
		{
			get
			{
				if (this.HasUpgradeInstalled)
				{
					if (this._installedUpgradeComponent == null)
					{
						global::Debug.LogWarning("Error on BionicUpgradeMonitor. storage does not contains bionic upgrade with id " + this.InstalledUpgradeID.ToString() + " this could be due to loading an old save on a new version");
						this.installedUpgradePrefabID = Tag.Invalid;
					}
					return this._installedUpgradeComponent;
				}
				this._installedUpgradeComponent = null;
				return null;
			}
		}

		// Token: 0x0600A1FB RID: 41467 RVA: 0x003A0269 File Offset: 0x0039E469
		public void DeserializeAction_OverrideInstalledUpgradePrefabID(Tag installedUpgradePrefabID)
		{
			this.installedUpgradePrefabID = installedUpgradePrefabID;
		}

		// Token: 0x0600A1FD RID: 41469 RVA: 0x003A028C File Offset: 0x0039E48C
		public void Initialize(AssignableSlotInstance assignableSlotInstance, Storage storage, BionicUpgradesMonitor.Instance master)
		{
			this.assignableSlotInstance = assignableSlotInstance;
			this.assignableSlotInstance.assignables.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().Subscribe(-1585839766, new Action<object>(this.OnAssignablesChanged));
			this.storage = storage;
			this.master = master;
			this._lastAssignedUpgradeComponent = this.assignedUpgradeComponent;
		}

		// Token: 0x0600A1FE RID: 41470 RVA: 0x003A02E6 File Offset: 0x0039E4E6
		public AssignableSlotInstance GetAssignableSlotInstance()
		{
			return this.assignableSlotInstance;
		}

		// Token: 0x0600A1FF RID: 41471 RVA: 0x003A02F0 File Offset: 0x0039E4F0
		public void OnSpawn(BionicUpgradesMonitor.Instance smi)
		{
			if (this.HasUpgradeInstalled && this._installedUpgradeComponent == null)
			{
				GameObject gameObject = null;
				int num = 0;
				List<GameObject> list = new List<GameObject>();
				this.storage.Find(this.InstalledUpgradeID, list);
				while (num < list.Count && this._installedUpgradeComponent == null)
				{
					GameObject gameObject2 = list[num];
					bool flag = false;
					foreach (BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot in smi.upgradeComponentSlots)
					{
						if (upgradeComponentSlot != this && upgradeComponentSlot.HasSpawned && !(upgradeComponentSlot.InstalledUpgradeID != this.InstalledUpgradeID) && upgradeComponentSlot.installedUpgradeComponent.gameObject == gameObject2)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						gameObject = gameObject2;
						break;
					}
					num++;
				}
				if (gameObject != null)
				{
					this._installedUpgradeComponent = gameObject.GetComponent<BionicUpgradeComponent>();
					this.StartBoosterSM();
				}
			}
			if (this.HasUpgradeInstalled && this.installedUpgradeComponent != null)
			{
				if (!this.HasUpgradeComponentAssigned)
				{
					this.installedUpgradeComponent.Assign(this.assignableSlotInstance.assignables.GetComponent<MinionAssignablesProxy>(), this.assignableSlotInstance);
				}
				this.SubscribeToInstallledUpgradeAssignable();
			}
			this.HasSpawned = true;
		}

		// Token: 0x0600A200 RID: 41472 RVA: 0x003A042D File Offset: 0x0039E62D
		public void SubscribeToInstallledUpgradeAssignable()
		{
			this.UnsubscribeFromInstalledUpgradeAssignable();
			this.installedUpgradeSubscribeCallbackIDX = this.installedUpgradeComponent.Subscribe(684616645, new Action<object>(this.OnInstalledComponentReassigned));
		}

		// Token: 0x0600A201 RID: 41473 RVA: 0x003A0457 File Offset: 0x0039E657
		public void UnsubscribeFromInstalledUpgradeAssignable()
		{
			if (this.installedUpgradeSubscribeCallbackIDX != -1)
			{
				this.installedUpgradeComponent.Unsubscribe(this.installedUpgradeSubscribeCallbackIDX);
				this.installedUpgradeSubscribeCallbackIDX = -1;
			}
		}

		// Token: 0x0600A202 RID: 41474 RVA: 0x003A047C File Offset: 0x0039E67C
		private void OnInstalledComponentReassigned(object obj)
		{
			IAssignableIdentity assignableIdentity = ((obj == null) ? null : ((IAssignableIdentity)obj));
			Action<BionicUpgradesMonitor.UpgradeComponentSlot, IAssignableIdentity> onInstalledUpgradeReassigned = this.OnInstalledUpgradeReassigned;
			if (onInstalledUpgradeReassigned == null)
			{
				return;
			}
			onInstalledUpgradeReassigned(this, assignableIdentity);
		}

		// Token: 0x0600A203 RID: 41475 RVA: 0x003A04A8 File Offset: 0x0039E6A8
		private void OnAssignablesChanged(object o)
		{
			if (this._lastAssignedUpgradeComponent != this.assignedUpgradeComponent)
			{
				this._lastAssignedUpgradeComponent = this.assignedUpgradeComponent;
				Action<BionicUpgradesMonitor.UpgradeComponentSlot> onAssignedUpgradeChanged = this.OnAssignedUpgradeChanged;
				if (onAssignedUpgradeChanged == null)
				{
					return;
				}
				onAssignedUpgradeChanged(this);
			}
		}

		// Token: 0x0600A204 RID: 41476 RVA: 0x003A04DA File Offset: 0x0039E6DA
		private void StartBoosterSM()
		{
			this._upgradeSmi = this.installedUpgradeComponent.StateMachine(this.master);
			this._upgradeSmi.StartSM();
		}

		// Token: 0x0600A205 RID: 41477 RVA: 0x003A0504 File Offset: 0x0039E704
		public void InternalInstall()
		{
			if (!this.HasUpgradeInstalled && this.HasUpgradeComponentAssigned)
			{
				this.storage.Store(this.assignedUpgradeComponent.gameObject, true, false, true, false);
				this.installedUpgradePrefabID = this.assignedUpgradeComponent.PrefabID();
				this._installedUpgradeComponent = this.assignedUpgradeComponent;
				this.SubscribeToInstallledUpgradeAssignable();
				this.StartBoosterSM();
				GameObject targetGameObject = this.assignableSlotInstance.assignables.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject != null)
				{
					targetGameObject.Trigger(2000325176, null);
				}
			}
		}

		// Token: 0x0600A206 RID: 41478 RVA: 0x003A0590 File Offset: 0x0039E790
		public void InternalUninstall()
		{
			if (this.HasUpgradeInstalled)
			{
				this.UnsubscribeFromInstalledUpgradeAssignable();
				GameObject gameObject = this.installedUpgradeComponent.gameObject;
				this.installedUpgradeComponent.Unassign();
				this.storage.Drop(gameObject, true);
				this.installedUpgradePrefabID = Tag.Invalid;
				this._installedUpgradeComponent = null;
				if (this._upgradeSmi != null)
				{
					this._upgradeSmi.StopSM("Uninstall");
					this._upgradeSmi = null;
				}
				GameObject targetGameObject = this.assignableSlotInstance.assignables.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject != null)
				{
					targetGameObject.Trigger(2000325176, null);
				}
			}
		}

		// Token: 0x0600A207 RID: 41479 RVA: 0x003A062F File Offset: 0x0039E82F
		public void InternalLock()
		{
			this.IsLocked = true;
		}

		// Token: 0x0600A208 RID: 41480 RVA: 0x003A0638 File Offset: 0x0039E838
		public void InternalUnlock()
		{
			this.IsLocked = false;
		}

		// Token: 0x04007E86 RID: 32390
		private BionicUpgradeComponent _installedUpgradeComponent;

		// Token: 0x04007E87 RID: 32391
		private BionicUpgradeComponent _lastAssignedUpgradeComponent;

		// Token: 0x04007E88 RID: 32392
		[Serialize]
		private Tag installedUpgradePrefabID = Tag.Invalid;

		// Token: 0x04007E89 RID: 32393
		public Action<BionicUpgradesMonitor.UpgradeComponentSlot, IAssignableIdentity> OnInstalledUpgradeReassigned;

		// Token: 0x04007E8A RID: 32394
		public Action<BionicUpgradesMonitor.UpgradeComponentSlot> OnAssignedUpgradeChanged;

		// Token: 0x04007E8B RID: 32395
		private AssignableSlotInstance assignableSlotInstance;

		// Token: 0x04007E8C RID: 32396
		private Storage storage;

		// Token: 0x04007E8D RID: 32397
		private int installedUpgradeSubscribeCallbackIDX = -1;

		// Token: 0x04007E8E RID: 32398
		private StateMachine.Instance _upgradeSmi;

		// Token: 0x04007E8F RID: 32399
		private BionicUpgradesMonitor.Instance master;
	}
}
