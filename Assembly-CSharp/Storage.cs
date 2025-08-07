using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200061D RID: 1565
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Storage")]
public class Storage : Workable, ISaveLoadableDetails, IGameObjectEffectDescriptor, IStorage
{
	// Token: 0x170001AA RID: 426
	// (get) Token: 0x06002574 RID: 9588 RVA: 0x000D5BCB File Offset: 0x000D3DCB
	public bool ShouldOnlyTransferFromLowerPriority
	{
		get
		{
			return this.onlyTransferFromLowerPriority || this.allowItemRemoval;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x06002575 RID: 9589 RVA: 0x000D5BDD File Offset: 0x000D3DDD
	// (set) Token: 0x06002576 RID: 9590 RVA: 0x000D5BE5 File Offset: 0x000D3DE5
	public bool allowUIItemRemoval { get; set; }

	// Token: 0x170001AC RID: 428
	public GameObject this[int idx]
	{
		get
		{
			return this.items[idx];
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06002578 RID: 9592 RVA: 0x000D5BFC File Offset: 0x000D3DFC
	public int Count
	{
		get
		{
			return this.items.Count;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06002579 RID: 9593 RVA: 0x000D5C09 File Offset: 0x000D3E09
	// (set) Token: 0x0600257A RID: 9594 RVA: 0x000D5C11 File Offset: 0x000D3E11
	public bool ShouldSaveItems
	{
		get
		{
			return this.shouldSaveItems;
		}
		set
		{
			this.shouldSaveItems = value;
		}
	}

	// Token: 0x0600257B RID: 9595 RVA: 0x000D5C1A File Offset: 0x000D3E1A
	public bool ShouldShowInUI()
	{
		return this.showInUI;
	}

	// Token: 0x0600257C RID: 9596 RVA: 0x000D5C22 File Offset: 0x000D3E22
	public List<GameObject> GetItems()
	{
		return this.items;
	}

	// Token: 0x0600257D RID: 9597 RVA: 0x000D5C2A File Offset: 0x000D3E2A
	public void SetDefaultStoredItemModifiers(List<Storage.StoredItemModifier> modifiers)
	{
		this.defaultStoredItemModifers = modifiers;
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x0600257E RID: 9598 RVA: 0x000D5C33 File Offset: 0x000D3E33
	public PrioritySetting masterPriority
	{
		get
		{
			if (this.prioritizable)
			{
				return this.prioritizable.GetMasterPriority();
			}
			return Chore.DefaultPrioritySetting;
		}
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x000D5C54 File Offset: 0x000D3E54
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		if (this.useGunForDelivery && worker.UsesMultiTool())
		{
			Workable.AnimInfo anim = base.GetAnim(worker);
			anim.smi = new MultitoolController.Instance(this, worker, "store", Assets.GetPrefab(EffectConfigs.OreAbsorbId));
			return anim;
		}
		return base.GetAnim(worker);
	}

	// Token: 0x06002580 RID: 9600 RVA: 0x000D5CAC File Offset: 0x000D3EAC
	public override Vector3 GetTargetPoint()
	{
		Vector3 vector = base.GetTargetPoint();
		if (this.useGunForDelivery && this.gunTargetOffset != Vector2.zero)
		{
			if (this.rotatable != null)
			{
				vector += this.rotatable.GetRotatedOffset(this.gunTargetOffset);
			}
			else
			{
				vector += new Vector3(this.gunTargetOffset.x, this.gunTargetOffset.y, 0f);
			}
		}
		return vector;
	}

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06002581 RID: 9601 RVA: 0x000D5D30 File Offset: 0x000D3F30
	// (remove) Token: 0x06002582 RID: 9602 RVA: 0x000D5D68 File Offset: 0x000D3F68
	public event global::System.Action OnStorageIncreased;

	// Token: 0x06002583 RID: 9603 RVA: 0x000D5DA0 File Offset: 0x000D3FA0
	protected override void OnPrefabInit()
	{
		if (this.useWideOffsets)
		{
			base.SetOffsetTable(OffsetGroups.InvertedWideTable);
		}
		else
		{
			base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		}
		this.showProgressBar = false;
		this.faceTargetWhenWorking = true;
		base.OnPrefabInit();
		GameUtil.SubscribeToTags<Storage>(this, Storage.OnDeadTagAddedDelegate, true);
		base.Subscribe<Storage>(1502190696, Storage.OnQueueDestroyObjectDelegate);
		base.Subscribe<Storage>(-905833192, Storage.OnCopySettingsDelegate);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Storing;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = false;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		this.SetupStorageStatusItems();
	}

	// Token: 0x06002584 RID: 9604 RVA: 0x000D5E48 File Offset: 0x000D4048
	private void SetupStorageStatusItems()
	{
		if (Storage.capacityStatusItem == null)
		{
			Storage.capacityStatusItem = new StatusItem("StorageLocker", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			Storage.capacityStatusItem.resolveStringCallback = delegate(string str, object data)
			{
				Storage storage = (Storage)data;
				float num = storage.MassStored();
				float num2 = storage.capacityKg;
				if (num > num2 - storage.storageFullMargin && num < num2)
				{
					num = num2;
				}
				else
				{
					num = Mathf.Floor(num);
				}
				string text = Util.FormatWholeNumber(num);
				IUserControlledCapacity component = storage.GetComponent<IUserControlledCapacity>();
				if (component != null)
				{
					num2 = Mathf.Min(component.UserMaxCapacity, num2);
				}
				string text2 = Util.FormatWholeNumber(num2);
				str = str.Replace("{Stored}", text);
				str = str.Replace("{Capacity}", text2);
				if (component != null)
				{
					str = str.Replace("{Units}", component.CapacityUnits);
				}
				else
				{
					str = str.Replace("{Units}", GameUtil.GetCurrentMassUnit(false));
				}
				return str;
			};
		}
		if (this.showCapacityStatusItem)
		{
			if (this.showCapacityAsMainStatus)
			{
				base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Storage.capacityStatusItem, this);
				return;
			}
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Stored, Storage.capacityStatusItem, this);
		}
	}

	// Token: 0x06002585 RID: 9605 RVA: 0x000D5F00 File Offset: 0x000D4100
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (!this.allowSettingOnlyFetchMarkedItems)
		{
			this.onlyFetchMarkedItems = false;
		}
		this.UpdateFetchCategory();
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x000D5F18 File Offset: 0x000D4118
	protected override void OnSpawn()
	{
		base.SetWorkTime(this.storageWorkTime);
		foreach (GameObject gameObject in this.items)
		{
			this.ApplyStoredItemModifiers(gameObject, true, true);
			if (this.sendOnStoreOnSpawn)
			{
				gameObject.Trigger(856640610, this);
			}
		}
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.SetSymbolVisiblity("sweep", this.onlyFetchMarkedItems);
		}
		Prioritizable component2 = base.GetComponent<Prioritizable>();
		if (component2 != null)
		{
			Prioritizable prioritizable = component2;
			prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnPriorityChanged));
		}
		this.UpdateFetchCategory();
		if (this.showUnreachableStatus)
		{
			base.Subscribe<Storage>(-1432940121, Storage.OnReachableChangedDelegate);
			new ReachabilityMonitor.Instance(this).StartSM();
		}
	}

	// Token: 0x06002587 RID: 9607 RVA: 0x000D6010 File Offset: 0x000D4210
	public GameObject Store(GameObject go, bool hide_popups = false, bool block_events = false, bool do_disease_transfer = true, bool is_deserializing = false)
	{
		if (go == null)
		{
			return null;
		}
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		GameObject gameObject = go;
		if (!hide_popups && PopFXManager.Instance != null)
		{
			LocString locString;
			Transform transform;
			if (this.fxPrefix == Storage.FXPrefix.Delivered)
			{
				locString = UI.DELIVERED;
				transform = base.transform;
			}
			else
			{
				locString = UI.PICKEDUP;
				transform = go.transform;
			}
			string text;
			if (!Assets.IsTagCountable(go.PrefabID()))
			{
				text = string.Format(locString, GameUtil.GetFormattedMass(component.Units, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), go.GetProperName());
			}
			else
			{
				text = string.Format(locString, (int)component.Units, go.GetProperName());
			}
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, text, transform, this.storageFXOffset, 1.5f, false, false);
		}
		go.transform.parent = base.transform;
		Vector3 vector = Grid.CellToPosCCC(Grid.PosToCell(this), Grid.SceneLayer.Move);
		vector.z = go.transform.GetPosition().z;
		go.transform.SetPosition(vector);
		if (!block_events && do_disease_transfer)
		{
			this.TransferDiseaseWithObject(go);
		}
		if (!is_deserializing)
		{
			Pickupable component2 = go.GetComponent<Pickupable>();
			if (component2 != null)
			{
				if (component2 != null && component2.prevent_absorb_until_stored)
				{
					component2.prevent_absorb_until_stored = false;
				}
				foreach (GameObject gameObject2 in this.items)
				{
					if (gameObject2 != null)
					{
						Pickupable component3 = gameObject2.GetComponent<Pickupable>();
						if (component3 != null && component3.TryAbsorb(component2, hide_popups, true))
						{
							if (!block_events)
							{
								base.Trigger(-1697596308, go);
								Action<GameObject> onStorageChange = this.OnStorageChange;
								if (onStorageChange != null)
								{
									onStorageChange(go);
								}
								base.Trigger(-778359855, this);
								if (this.OnStorageIncreased != null)
								{
									this.OnStorageIncreased();
								}
							}
							this.ApplyStoredItemModifiers(go, true, false);
							gameObject = gameObject2;
							go = null;
							break;
						}
					}
				}
			}
		}
		if (go != null)
		{
			this.items.Add(go);
			if (!is_deserializing)
			{
				this.ApplyStoredItemModifiers(go, true, false);
			}
			if (!block_events)
			{
				go.Trigger(856640610, this);
				base.Trigger(-1697596308, go);
				Action<GameObject> onStorageChange2 = this.OnStorageChange;
				if (onStorageChange2 != null)
				{
					onStorageChange2(go);
				}
				base.Trigger(-778359855, this);
				if (this.OnStorageIncreased != null)
				{
					this.OnStorageIncreased();
				}
			}
		}
		return gameObject;
	}

	// Token: 0x06002588 RID: 9608 RVA: 0x000D62A4 File Offset: 0x000D44A4
	public PrimaryElement AddElement(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, bool keep_zero_mass = false, bool do_disease_transfer = true)
	{
		Element element2 = ElementLoader.FindElementByHash(element);
		if (element2.IsGas)
		{
			return this.AddGasChunk(element, mass, temperature, disease_idx, disease_count, keep_zero_mass, do_disease_transfer);
		}
		if (element2.IsLiquid)
		{
			return this.AddLiquid(element, mass, temperature, disease_idx, disease_count, keep_zero_mass, do_disease_transfer);
		}
		if (element2.IsSolid)
		{
			return this.AddOre(element, mass, temperature, disease_idx, disease_count, keep_zero_mass, do_disease_transfer);
		}
		return null;
	}

	// Token: 0x06002589 RID: 9609 RVA: 0x000D6308 File Offset: 0x000D4508
	public PrimaryElement AddOre(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, bool keep_zero_mass = false, bool do_disease_transfer = true)
	{
		if (mass <= 0f)
		{
			return null;
		}
		PrimaryElement primaryElement = this.FindPrimaryElement(element);
		if (primaryElement != null)
		{
			float finalTemperature = GameUtil.GetFinalTemperature(primaryElement.Temperature, primaryElement.Mass, temperature, mass);
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			primaryElement.Mass += mass;
			primaryElement.Temperature = finalTemperature;
			primaryElement.AddDisease(disease_idx, disease_count, "Storage.AddOre");
			base.Trigger(-1697596308, primaryElement.gameObject);
			Action<GameObject> onStorageChange = this.OnStorageChange;
			if (onStorageChange != null)
			{
				onStorageChange(primaryElement.gameObject);
			}
		}
		else
		{
			Element element2 = ElementLoader.FindElementByHash(element);
			GameObject gameObject = element2.substance.SpawnResource(base.transform.GetPosition(), mass, temperature, disease_idx, disease_count, true, false, true);
			gameObject.GetComponent<Pickupable>().prevent_absorb_until_stored = true;
			element2.substance.ActivateSubstanceGameObject(gameObject, disease_idx, disease_count);
			this.Store(gameObject, true, false, do_disease_transfer, false);
		}
		return primaryElement;
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x000D63EC File Offset: 0x000D45EC
	public PrimaryElement AddLiquid(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, bool keep_zero_mass = false, bool do_disease_transfer = true)
	{
		if (mass <= 0f)
		{
			return null;
		}
		PrimaryElement primaryElement = this.FindPrimaryElement(element);
		if (primaryElement != null)
		{
			float finalTemperature = GameUtil.GetFinalTemperature(primaryElement.Temperature, primaryElement.Mass, temperature, mass);
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			primaryElement.Mass += mass;
			primaryElement.Temperature = finalTemperature;
			primaryElement.AddDisease(disease_idx, disease_count, "Storage.AddLiquid");
			base.Trigger(-1697596308, primaryElement.gameObject);
			Action<GameObject> onStorageChange = this.OnStorageChange;
			if (onStorageChange != null)
			{
				onStorageChange(primaryElement.gameObject);
			}
		}
		else
		{
			SubstanceChunk substanceChunk = LiquidSourceManager.Instance.CreateChunk(element, mass, temperature, disease_idx, disease_count, base.transform.GetPosition());
			primaryElement = substanceChunk.GetComponent<PrimaryElement>();
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			this.Store(substanceChunk.gameObject, true, false, do_disease_transfer, false);
		}
		return primaryElement;
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x000D64C0 File Offset: 0x000D46C0
	public PrimaryElement AddGasChunk(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, bool keep_zero_mass, bool do_disease_transfer = true)
	{
		if (mass <= 0f)
		{
			return null;
		}
		PrimaryElement primaryElement = this.FindPrimaryElement(element);
		if (primaryElement != null)
		{
			float mass2 = primaryElement.Mass;
			float finalTemperature = GameUtil.GetFinalTemperature(primaryElement.Temperature, mass2, temperature, mass);
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			primaryElement.SetMassTemperature(mass2 + mass, finalTemperature);
			primaryElement.AddDisease(disease_idx, disease_count, "Storage.AddGasChunk");
			base.Trigger(-1697596308, primaryElement.gameObject);
			Action<GameObject> onStorageChange = this.OnStorageChange;
			if (onStorageChange != null)
			{
				onStorageChange(primaryElement.gameObject);
			}
		}
		else
		{
			SubstanceChunk substanceChunk = GasSourceManager.Instance.CreateChunk(element, mass, temperature, disease_idx, disease_count, base.transform.GetPosition());
			primaryElement = substanceChunk.GetComponent<PrimaryElement>();
			primaryElement.KeepZeroMassObject = keep_zero_mass;
			this.Store(substanceChunk.gameObject, true, false, do_disease_transfer, false);
		}
		return primaryElement;
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x000D6588 File Offset: 0x000D4788
	public void Transfer(Storage target, bool block_events = false, bool hide_popups = false)
	{
		while (this.items.Count > 0)
		{
			this.Transfer(this.items[0], target, block_events, hide_popups);
		}
	}

	// Token: 0x0600258D RID: 9613 RVA: 0x000D65B0 File Offset: 0x000D47B0
	public bool TransferMass(Storage dest_storage, Tag tag, float amount, bool flatten = false, bool block_events = false, bool hide_popups = false)
	{
		float num = amount;
		while (num > 0f && this.GetAmountAvailable(tag) > 0f)
		{
			num -= this.Transfer(dest_storage, tag, num, block_events, hide_popups);
		}
		if (flatten)
		{
			dest_storage.Flatten(tag);
		}
		return num <= 0f;
	}

	// Token: 0x0600258E RID: 9614 RVA: 0x000D6600 File Offset: 0x000D4800
	public float Transfer(Storage dest_storage, Tag tag, float amount, bool block_events = false, bool hide_popups = false)
	{
		GameObject gameObject = this.FindFirst(tag);
		if (gameObject != null)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			if (amount < component.Units)
			{
				Pickupable component2 = gameObject.GetComponent<Pickupable>();
				Pickupable pickupable = component2.Take(amount);
				dest_storage.Store(pickupable.gameObject, hide_popups, block_events, true, false);
				if (!block_events)
				{
					base.Trigger(-1697596308, component2.gameObject);
					Action<GameObject> onStorageChange = this.OnStorageChange;
					if (onStorageChange != null)
					{
						onStorageChange(component2.gameObject);
					}
				}
			}
			else
			{
				this.Transfer(gameObject, dest_storage, block_events, hide_popups);
				amount = component.Units;
			}
			return amount;
		}
		return 0f;
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x000D669C File Offset: 0x000D489C
	public bool Transfer(GameObject go, Storage target, bool block_events = false, bool hide_popups = false)
	{
		this.items.RemoveAll((GameObject it) => it == null);
		int count = this.items.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.items[i] == go)
			{
				this.items.RemoveAt(i);
				this.ApplyStoredItemModifiers(go, false, false);
				target.Store(go, hide_popups, block_events, true, false);
				if (!block_events)
				{
					base.Trigger(-1697596308, go);
					Action<GameObject> onStorageChange = this.OnStorageChange;
					if (onStorageChange != null)
					{
						onStorageChange(go);
					}
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x000D6748 File Offset: 0x000D4948
	public void TransferUnitMass(Storage dest_storage, Tag tag, float unitAmount, bool flatten = false, bool block_events = false, bool hide_popups = false)
	{
		float num = 0f;
		GameObject gameObject = this.FindFirst(tag);
		while (num < unitAmount && gameObject != null)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			if (unitAmount < component.Units)
			{
				Pickupable component2 = gameObject.GetComponent<Pickupable>();
				Pickupable pickupable = component2.TakeUnit(unitAmount);
				dest_storage.Store(pickupable.gameObject, hide_popups, block_events, true, false);
				if (block_events)
				{
					break;
				}
				base.Trigger(-1697596308, component2.gameObject);
				Action<GameObject> onStorageChange = this.OnStorageChange;
				if (onStorageChange == null)
				{
					return;
				}
				onStorageChange(component2.gameObject);
				return;
			}
			else
			{
				this.Transfer(gameObject, dest_storage, block_events, hide_popups);
				num += component.Units;
				gameObject = this.FindFirst(tag);
			}
		}
	}

	// Token: 0x06002591 RID: 9617 RVA: 0x000D67F4 File Offset: 0x000D49F4
	public bool DropSome(Tag tag, float amount, bool ventGas = false, bool dumpLiquid = false, Vector3 offset = default(Vector3), bool doDiseaseTransfer = true, bool showInWorldNotification = false)
	{
		bool flag = false;
		float num = amount;
		ListPool<GameObject, Storage>.PooledList pooledList = ListPool<GameObject, Storage>.Allocate();
		this.Find(tag, pooledList);
		foreach (GameObject gameObject in pooledList)
		{
			Pickupable component = gameObject.GetComponent<Pickupable>();
			if (component)
			{
				Pickupable pickupable = component.Take(num);
				if (pickupable != null)
				{
					bool flag2 = false;
					if (ventGas || dumpLiquid)
					{
						Dumpable component2 = pickupable.GetComponent<Dumpable>();
						if (component2 != null)
						{
							if (ventGas && pickupable.GetComponent<PrimaryElement>().Element.IsGas)
							{
								component2.Dump(base.transform.GetPosition() + offset);
								flag2 = true;
								num -= pickupable.GetComponent<PrimaryElement>().Mass;
								base.Trigger(-1697596308, pickupable.gameObject);
								Action<GameObject> onStorageChange = this.OnStorageChange;
								if (onStorageChange != null)
								{
									onStorageChange(pickupable.gameObject);
								}
								flag = true;
								if (showInWorldNotification)
								{
									PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, pickupable.GetComponent<PrimaryElement>().Element.name + " " + GameUtil.GetFormattedMass(pickupable.TotalAmount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), pickupable.transform, this.storageFXOffset, 1.5f, false, false);
								}
							}
							if (dumpLiquid && pickupable.GetComponent<PrimaryElement>().Element.IsLiquid)
							{
								component2.Dump(base.transform.GetPosition() + offset);
								flag2 = true;
								num -= pickupable.GetComponent<PrimaryElement>().Mass;
								base.Trigger(-1697596308, pickupable.gameObject);
								Action<GameObject> onStorageChange2 = this.OnStorageChange;
								if (onStorageChange2 != null)
								{
									onStorageChange2(pickupable.gameObject);
								}
								flag = true;
								if (showInWorldNotification)
								{
									PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, pickupable.GetComponent<PrimaryElement>().Element.name + " " + GameUtil.GetFormattedMass(pickupable.TotalAmount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), pickupable.transform, this.storageFXOffset, 1.5f, false, false);
								}
							}
						}
					}
					if (!flag2)
					{
						Vector3 vector = Grid.CellToPosCCC(Grid.PosToCell(this), Grid.SceneLayer.Ore) + offset;
						pickupable.transform.SetPosition(vector);
						KBatchedAnimController component3 = pickupable.GetComponent<KBatchedAnimController>();
						if (component3)
						{
							component3.SetSceneLayer(Grid.SceneLayer.Ore);
						}
						num -= pickupable.GetComponent<PrimaryElement>().Mass;
						this.MakeWorldActive(pickupable.gameObject);
						base.Trigger(-1697596308, pickupable.gameObject);
						Action<GameObject> onStorageChange3 = this.OnStorageChange;
						if (onStorageChange3 != null)
						{
							onStorageChange3(pickupable.gameObject);
						}
						flag = true;
						if (showInWorldNotification)
						{
							PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, pickupable.GetComponent<PrimaryElement>().Element.name + " " + GameUtil.GetFormattedMass(pickupable.TotalAmount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), pickupable.transform, this.storageFXOffset, 1.5f, false, false);
						}
					}
				}
			}
			if (num <= 0f)
			{
				break;
			}
		}
		pooledList.Recycle();
		return flag;
	}

	// Token: 0x06002592 RID: 9618 RVA: 0x000D6B48 File Offset: 0x000D4D48
	public void DropAll(Vector3 position, bool vent_gas = false, bool dump_liquid = false, Vector3 offset = default(Vector3), bool do_disease_transfer = true, List<GameObject> collect_dropped_items = null)
	{
		while (this.items.Count > 0)
		{
			GameObject gameObject = this.items[0];
			if (do_disease_transfer)
			{
				this.TransferDiseaseWithObject(gameObject);
			}
			this.items.RemoveAt(0);
			if (gameObject != null)
			{
				bool flag = false;
				if (vent_gas || dump_liquid)
				{
					Dumpable component = gameObject.GetComponent<Dumpable>();
					if (component != null)
					{
						if (vent_gas && gameObject.GetComponent<PrimaryElement>().Element.IsGas)
						{
							component.Dump(position + offset);
							flag = true;
						}
						if (dump_liquid && gameObject.GetComponent<PrimaryElement>().Element.IsLiquid)
						{
							component.Dump(position + offset);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					gameObject.transform.SetPosition(position + offset);
					KBatchedAnimController component2 = gameObject.GetComponent<KBatchedAnimController>();
					if (component2)
					{
						component2.SetSceneLayer(Grid.SceneLayer.Ore);
					}
					this.MakeWorldActive(gameObject);
					if (collect_dropped_items != null)
					{
						collect_dropped_items.Add(gameObject);
					}
				}
			}
		}
	}

	// Token: 0x06002593 RID: 9619 RVA: 0x000D6C3D File Offset: 0x000D4E3D
	public void DropAll(bool vent_gas = false, bool dump_liquid = false, Vector3 offset = default(Vector3), bool do_disease_transfer = true, List<GameObject> collect_dropped_items = null)
	{
		this.DropAll(Grid.CellToPosCCC(Grid.PosToCell(this), Grid.SceneLayer.Ore), vent_gas, dump_liquid, offset, do_disease_transfer, collect_dropped_items);
	}

	// Token: 0x06002594 RID: 9620 RVA: 0x000D6C5C File Offset: 0x000D4E5C
	public void Drop(Tag t, List<GameObject> obj_list)
	{
		this.Find(t, obj_list);
		foreach (GameObject gameObject in obj_list)
		{
			this.Drop(gameObject, true);
		}
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x000D6CB8 File Offset: 0x000D4EB8
	public void Drop(Tag t)
	{
		ListPool<GameObject, Storage>.PooledList pooledList = ListPool<GameObject, Storage>.Allocate();
		this.Find(t, pooledList);
		foreach (GameObject gameObject in pooledList)
		{
			this.Drop(gameObject, true);
		}
		pooledList.Recycle();
	}

	// Token: 0x06002596 RID: 9622 RVA: 0x000D6D20 File Offset: 0x000D4F20
	public void DropUnlessMatching(FetchChore chore)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null))
			{
				KPrefabID component = this.items[i].GetComponent<KPrefabID>();
				if (!(((chore.criteria == FetchChore.MatchCriteria.MatchID && chore.tags.Contains(component.PrefabTag)) || (chore.criteria == FetchChore.MatchCriteria.MatchTags && component.HasTag(chore.tagsFirst))) & (!chore.requiredTag.IsValid || component.HasTag(chore.requiredTag)) & !component.HasAnyTags(chore.forbiddenTags)))
				{
					GameObject gameObject = this.items[i];
					this.items.RemoveAt(i);
					i--;
					this.TransferDiseaseWithObject(gameObject);
					this.MakeWorldActive(gameObject);
				}
			}
		}
	}

	// Token: 0x06002597 RID: 9623 RVA: 0x000D6E04 File Offset: 0x000D5004
	public GameObject[] DropUnlessHasTag(Tag tag)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null) && !this.items[i].GetComponent<KPrefabID>().HasTag(tag))
			{
				GameObject gameObject = this.items[i];
				this.items.RemoveAt(i);
				i--;
				this.TransferDiseaseWithObject(gameObject);
				this.MakeWorldActive(gameObject);
				Dumpable component = gameObject.GetComponent<Dumpable>();
				if (component != null)
				{
					component.Dump(base.transform.GetPosition());
				}
				list.Add(gameObject);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06002598 RID: 9624 RVA: 0x000D6EBC File Offset: 0x000D50BC
	public GameObject[] DropHasTags(Tag[] tag)
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null) && this.items[i].GetComponent<KPrefabID>().HasAllTags(tag))
			{
				GameObject gameObject = this.items[i];
				this.items.RemoveAt(i);
				i--;
				this.TransferDiseaseWithObject(gameObject);
				this.MakeWorldActive(gameObject);
				Dumpable component = gameObject.GetComponent<Dumpable>();
				if (component != null)
				{
					component.Dump(base.transform.GetPosition());
				}
				list.Add(gameObject);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06002599 RID: 9625 RVA: 0x000D6F74 File Offset: 0x000D5174
	public GameObject Drop(GameObject go, bool do_disease_transfer = true)
	{
		if (go == null)
		{
			return null;
		}
		int count = this.items.Count;
		for (int i = 0; i < count; i++)
		{
			if (!(go != this.items[i]))
			{
				this.items[i] = this.items[count - 1];
				this.items.RemoveAt(count - 1);
				if (do_disease_transfer)
				{
					this.TransferDiseaseWithObject(go);
				}
				this.MakeWorldActive(go);
				break;
			}
		}
		return go;
	}

	// Token: 0x0600259A RID: 9626 RVA: 0x000D6FF4 File Offset: 0x000D51F4
	public void RenotifyAll()
	{
		this.items.RemoveAll((GameObject it) => it == null);
		foreach (GameObject gameObject in this.items)
		{
			gameObject.Trigger(856640610, this);
		}
	}

	// Token: 0x0600259B RID: 9627 RVA: 0x000D7078 File Offset: 0x000D5278
	private void TransferDiseaseWithObject(GameObject obj)
	{
		if (obj == null || !this.doDiseaseTransfer || this.primaryElement == null)
		{
			return;
		}
		PrimaryElement component = obj.GetComponent<PrimaryElement>();
		if (component == null)
		{
			return;
		}
		SimUtil.DiseaseInfo invalid = SimUtil.DiseaseInfo.Invalid;
		invalid.idx = component.DiseaseIdx;
		invalid.count = (int)((float)component.DiseaseCount * 0.05f);
		SimUtil.DiseaseInfo invalid2 = SimUtil.DiseaseInfo.Invalid;
		invalid2.idx = this.primaryElement.DiseaseIdx;
		invalid2.count = (int)((float)this.primaryElement.DiseaseCount * 0.05f);
		component.ModifyDiseaseCount(-invalid.count, "Storage.TransferDiseaseWithObject");
		this.primaryElement.ModifyDiseaseCount(-invalid2.count, "Storage.TransferDiseaseWithObject");
		if (invalid.count > 0)
		{
			this.primaryElement.AddDisease(invalid.idx, invalid.count, "Storage.TransferDiseaseWithObject");
		}
		if (invalid2.count > 0)
		{
			component.AddDisease(invalid2.idx, invalid2.count, "Storage.TransferDiseaseWithObject");
		}
	}

	// Token: 0x0600259C RID: 9628 RVA: 0x000D7180 File Offset: 0x000D5380
	private void MakeWorldActive(GameObject go)
	{
		go.transform.parent = null;
		if (this.dropOffset != Vector2.zero)
		{
			go.transform.Translate(this.dropOffset);
		}
		go.Trigger(856640610, null);
		base.Trigger(-1697596308, go);
		Action<GameObject> onStorageChange = this.OnStorageChange;
		if (onStorageChange != null)
		{
			onStorageChange(go);
		}
		this.ApplyStoredItemModifiers(go, false, false);
		if (go != null)
		{
			PrimaryElement component = go.GetComponent<PrimaryElement>();
			if (component != null && component.KeepZeroMassObject)
			{
				component.KeepZeroMassObject = false;
				if (component.Mass <= 0f)
				{
					Util.KDestroyGameObject(go);
				}
			}
		}
	}

	// Token: 0x0600259D RID: 9629 RVA: 0x000D7230 File Offset: 0x000D5430
	public List<GameObject> Find(Tag tag, List<GameObject> result)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (!(gameObject == null) && gameObject.HasTag(tag))
			{
				result.Add(gameObject);
			}
		}
		return result;
	}

	// Token: 0x0600259E RID: 9630 RVA: 0x000D727C File Offset: 0x000D547C
	public GameObject FindFirst(Tag tag)
	{
		GameObject gameObject = null;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject2 = this.items[i];
			if (!(gameObject2 == null) && gameObject2.HasTag(tag))
			{
				gameObject = gameObject2;
				break;
			}
		}
		return gameObject;
	}

	// Token: 0x0600259F RID: 9631 RVA: 0x000D72C8 File Offset: 0x000D54C8
	public PrimaryElement FindFirstWithMass(Tag tag, float mass = 0f)
	{
		PrimaryElement primaryElement = null;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (!(gameObject == null) && gameObject.HasTag(tag))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.Mass > 0f && component.Mass >= mass)
				{
					primaryElement = component;
					break;
				}
			}
		}
		return primaryElement;
	}

	// Token: 0x060025A0 RID: 9632 RVA: 0x000D7330 File Offset: 0x000D5530
	private void Flatten(Tag tag_to_combine)
	{
		GameObject gameObject = this.FindFirst(tag_to_combine);
		if (gameObject == null)
		{
			return;
		}
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		for (int i = this.items.Count - 1; i >= 0; i--)
		{
			GameObject gameObject2 = this.items[i];
			if (gameObject2.HasTag(tag_to_combine) && gameObject2 != gameObject)
			{
				PrimaryElement component2 = gameObject2.GetComponent<PrimaryElement>();
				component.Mass += component2.Mass;
				this.ConsumeIgnoringDisease(gameObject2);
			}
		}
	}

	// Token: 0x060025A1 RID: 9633 RVA: 0x000D73B0 File Offset: 0x000D55B0
	public HashSet<Tag> GetAllIDsInStorage()
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			hashSet.Add(gameObject.PrefabID());
		}
		return hashSet;
	}

	// Token: 0x060025A2 RID: 9634 RVA: 0x000D73F4 File Offset: 0x000D55F4
	public GameObject Find(int ID)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (ID == gameObject.PrefabID().GetHashCode())
			{
				return gameObject;
			}
		}
		return null;
	}

	// Token: 0x060025A3 RID: 9635 RVA: 0x000D743E File Offset: 0x000D563E
	public void ConsumeAllIgnoringDisease()
	{
		this.ConsumeAllIgnoringDisease(Tag.Invalid);
	}

	// Token: 0x060025A4 RID: 9636 RVA: 0x000D744C File Offset: 0x000D564C
	public void ConsumeAllIgnoringDisease(Tag tag)
	{
		for (int i = this.items.Count - 1; i >= 0; i--)
		{
			if (!(tag != Tag.Invalid) || this.items[i].HasTag(tag))
			{
				this.ConsumeIgnoringDisease(this.items[i]);
			}
		}
	}

	// Token: 0x060025A5 RID: 9637 RVA: 0x000D74A4 File Offset: 0x000D56A4
	public void ConsumeAndGetDisease(Tag tag, float amount, out float amount_consumed, out SimUtil.DiseaseInfo disease_info, out float aggregate_temperature)
	{
		SimHashes simHashes;
		this.ConsumeAndGetDisease(tag, amount, out amount_consumed, out disease_info, out aggregate_temperature, out simHashes);
	}

	// Token: 0x060025A6 RID: 9638 RVA: 0x000D74C0 File Offset: 0x000D56C0
	public void ConsumeAndGetDisease(Tag tag, float amount, out float amount_consumed, out SimUtil.DiseaseInfo disease_info, out float aggregate_temperature, out SimHashes mostRelevantItemElement)
	{
		DebugUtil.Assert(tag.IsValid);
		amount_consumed = 0f;
		disease_info = SimUtil.DiseaseInfo.Invalid;
		mostRelevantItemElement = SimHashes.Vacuum;
		aggregate_temperature = 0f;
		bool flag = false;
		float num = 0f;
		int num2 = 0;
		while (num2 < this.items.Count && amount > 0f)
		{
			GameObject gameObject = this.items[num2];
			if (!(gameObject == null) && gameObject.HasTag(tag))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.Units > 0f)
				{
					flag = true;
					float num3 = Math.Min(component.Units, amount);
					global::Debug.Assert(num3 > 0f, "Delta amount was zero, which should be impossible.");
					aggregate_temperature = SimUtil.CalculateFinalTemperature(amount_consumed, aggregate_temperature, num3, component.Temperature);
					SimUtil.DiseaseInfo percentOfDisease = SimUtil.GetPercentOfDisease(component, num3 / component.Units);
					disease_info = SimUtil.CalculateFinalDiseaseInfo(disease_info, percentOfDisease);
					component.Units -= num3;
					component.ModifyDiseaseCount(-percentOfDisease.count, "Storage.ConsumeAndGetDisease");
					amount -= num3;
					amount_consumed += num3;
					if (num3 > num)
					{
						num = num3;
						mostRelevantItemElement = component.ElementID;
					}
				}
				if (component.Units <= 0f && !component.KeepZeroMassObject)
				{
					if (this.deleted_objects == null)
					{
						this.deleted_objects = new List<GameObject>();
					}
					this.deleted_objects.Add(gameObject);
				}
				base.Trigger(-1697596308, gameObject);
				Action<GameObject> onStorageChange = this.OnStorageChange;
				if (onStorageChange != null)
				{
					onStorageChange(gameObject);
				}
			}
			num2++;
		}
		if (!flag)
		{
			aggregate_temperature = base.GetComponent<PrimaryElement>().Temperature;
		}
		if (this.deleted_objects != null)
		{
			for (int i = 0; i < this.deleted_objects.Count; i++)
			{
				this.items.Remove(this.deleted_objects[i]);
				Util.KDestroyGameObject(this.deleted_objects[i]);
			}
			this.deleted_objects.Clear();
		}
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x000D76CC File Offset: 0x000D58CC
	public void ConsumeAndGetDisease(Recipe.Ingredient ingredient, out SimUtil.DiseaseInfo disease_info, out float temperature)
	{
		float num;
		this.ConsumeAndGetDisease(ingredient.tag, ingredient.amount, out num, out disease_info, out temperature);
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x000D76F0 File Offset: 0x000D58F0
	public void ConsumeIgnoringDisease(Tag tag, float amount)
	{
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		this.ConsumeAndGetDisease(tag, amount, out num, out diseaseInfo, out num2);
	}

	// Token: 0x060025A9 RID: 9641 RVA: 0x000D770C File Offset: 0x000D590C
	public void ConsumeIgnoringDisease(GameObject item_go)
	{
		if (this.items.Contains(item_go))
		{
			PrimaryElement component = item_go.GetComponent<PrimaryElement>();
			if (component != null && component.KeepZeroMassObject)
			{
				component.Units = 0f;
				component.ModifyDiseaseCount(-component.DiseaseCount, "consume item");
				base.Trigger(-1697596308, item_go);
				Action<GameObject> onStorageChange = this.OnStorageChange;
				if (onStorageChange == null)
				{
					return;
				}
				onStorageChange(item_go);
				return;
			}
			else
			{
				this.items.Remove(item_go);
				base.Trigger(-1697596308, item_go);
				Action<GameObject> onStorageChange2 = this.OnStorageChange;
				if (onStorageChange2 != null)
				{
					onStorageChange2(item_go);
				}
				item_go.DeleteObject();
			}
		}
	}

	// Token: 0x060025AA RID: 9642 RVA: 0x000D77AE File Offset: 0x000D59AE
	public GameObject Drop(int ID)
	{
		return this.Drop(this.Find(ID), true);
	}

	// Token: 0x060025AB RID: 9643 RVA: 0x000D77C0 File Offset: 0x000D59C0
	private void OnDeath(object data)
	{
		List<GameObject> list = new List<GameObject>();
		bool flag = true;
		bool flag2 = true;
		List<GameObject> list2 = list;
		this.DropAll(flag, flag2, default(Vector3), true, list2);
		if (this.onDestroyItemsDropped != null)
		{
			this.onDestroyItemsDropped(list);
		}
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x000D77FC File Offset: 0x000D59FC
	public bool IsFull()
	{
		return this.RemainingCapacity() <= 0f;
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x000D780E File Offset: 0x000D5A0E
	public bool IsEmpty()
	{
		return this.items.Count == 0;
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x000D781E File Offset: 0x000D5A1E
	public float Capacity()
	{
		return this.capacityKg;
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x000D7826 File Offset: 0x000D5A26
	public bool IsEndOfLife()
	{
		return this.endOfLife;
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x000D7830 File Offset: 0x000D5A30
	public float ExactMassStored()
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null))
			{
				PrimaryElement component = this.items[i].GetComponent<PrimaryElement>();
				if (component != null)
				{
					num += component.Units * component.MassPerUnit;
				}
			}
		}
		return num;
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x000D7899 File Offset: 0x000D5A99
	public float MassStored()
	{
		return (float)Mathf.RoundToInt(this.ExactMassStored() * 1000f) / 1000f;
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x000D78B4 File Offset: 0x000D5AB4
	public float UnitsStored()
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			if (!(this.items[i] == null))
			{
				PrimaryElement component = this.items[i].GetComponent<PrimaryElement>();
				if (component != null)
				{
					num += component.Units;
				}
			}
		}
		return (float)Mathf.RoundToInt(num * 1000f) / 1000f;
	}

	// Token: 0x060025B3 RID: 9651 RVA: 0x000D7928 File Offset: 0x000D5B28
	public bool Has(Tag tag)
	{
		bool flag = false;
		foreach (GameObject gameObject in this.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.HasTag(tag) && component.Mass > 0f)
				{
					flag = true;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x000D79A4 File Offset: 0x000D5BA4
	public PrimaryElement AddToPrimaryElement(SimHashes element, float additional_mass, float temperature)
	{
		PrimaryElement primaryElement = this.FindPrimaryElement(element);
		if (primaryElement != null)
		{
			float finalTemperature = GameUtil.GetFinalTemperature(primaryElement.Temperature, primaryElement.Mass, temperature, additional_mass);
			primaryElement.Mass += additional_mass;
			primaryElement.Temperature = finalTemperature;
		}
		return primaryElement;
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x000D79EC File Offset: 0x000D5BEC
	public PrimaryElement FindPrimaryElement(SimHashes element)
	{
		PrimaryElement primaryElement = null;
		foreach (GameObject gameObject in this.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.ElementID == element)
				{
					primaryElement = component;
					break;
				}
			}
		}
		return primaryElement;
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x000D7A58 File Offset: 0x000D5C58
	public float RemainingCapacity()
	{
		return this.capacityKg - this.MassStored();
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x000D7A67 File Offset: 0x000D5C67
	public bool GetOnlyFetchMarkedItems()
	{
		return this.onlyFetchMarkedItems;
	}

	// Token: 0x060025B8 RID: 9656 RVA: 0x000D7A6F File Offset: 0x000D5C6F
	public void SetOnlyFetchMarkedItems(bool is_set)
	{
		if (is_set != this.onlyFetchMarkedItems)
		{
			this.onlyFetchMarkedItems = is_set;
			this.UpdateFetchCategory();
			base.Trigger(644822890, null);
			base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("sweep", is_set);
		}
	}

	// Token: 0x060025B9 RID: 9657 RVA: 0x000D7AA9 File Offset: 0x000D5CA9
	private void UpdateFetchCategory()
	{
		if (this.fetchCategory == Storage.FetchCategory.Building)
		{
			return;
		}
		this.fetchCategory = (this.onlyFetchMarkedItems ? Storage.FetchCategory.StorageSweepOnly : Storage.FetchCategory.GeneralStorage);
	}

	// Token: 0x060025BA RID: 9658 RVA: 0x000D7AC6 File Offset: 0x000D5CC6
	protected override void OnCleanUp()
	{
		if (this.items.Count != 0)
		{
			global::Debug.LogWarning("Storage for [" + base.gameObject.name + "] is being destroyed but it still contains items!", base.gameObject);
		}
		base.OnCleanUp();
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x000D7B00 File Offset: 0x000D5D00
	private void OnQueueDestroyObject(object data)
	{
		this.endOfLife = true;
		List<GameObject> list = new List<GameObject>();
		bool flag = true;
		bool flag2 = false;
		List<GameObject> list2 = list;
		this.DropAll(flag, flag2, default(Vector3), true, list2);
		if (this.onDestroyItemsDropped != null)
		{
			this.onDestroyItemsDropped(list);
		}
		this.OnCleanUp();
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x000D7B49 File Offset: 0x000D5D49
	public void Remove(GameObject go, bool do_disease_transfer = true)
	{
		this.items.Remove(go);
		if (do_disease_transfer)
		{
			this.TransferDiseaseWithObject(go);
		}
		base.Trigger(-1697596308, go);
		Action<GameObject> onStorageChange = this.OnStorageChange;
		if (onStorageChange != null)
		{
			onStorageChange(go);
		}
		this.ApplyStoredItemModifiers(go, false, false);
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x000D7B8C File Offset: 0x000D5D8C
	public bool ForceStore(Tag tag, float amount)
	{
		global::Debug.Assert(amount < PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT);
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag))
			{
				gameObject.GetComponent<PrimaryElement>().Mass += amount;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x000D7BF4 File Offset: 0x000D5DF4
	public float GetAmountAvailable(Tag tag)
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag))
			{
				num += gameObject.GetComponent<PrimaryElement>().Units;
			}
		}
		return num;
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x000D7C4C File Offset: 0x000D5E4C
	public float GetAmountAvailable(Tag tag, Tag[] forbiddenTags = null)
	{
		if (forbiddenTags == null)
		{
			return this.GetAmountAvailable(tag);
		}
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag) && !gameObject.HasAnyTags(forbiddenTags))
			{
				num += gameObject.GetComponent<PrimaryElement>().Units;
			}
		}
		return num;
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x000D7CB8 File Offset: 0x000D5EB8
	public float GetUnitsAvailable(Tag tag)
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag))
			{
				num += gameObject.GetComponent<PrimaryElement>().Units;
			}
		}
		return num;
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x000D7D10 File Offset: 0x000D5F10
	public float GetMassAvailable(Tag tag)
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null && gameObject.HasTag(tag))
			{
				num += gameObject.GetComponent<PrimaryElement>().Mass;
			}
		}
		return num;
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x000D7D68 File Offset: 0x000D5F68
	public float GetMassAvailable(SimHashes element)
	{
		float num = 0f;
		for (int i = 0; i < this.items.Count; i++)
		{
			GameObject gameObject = this.items[i];
			if (gameObject != null)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.ElementID == element)
				{
					num += component.Mass;
				}
			}
		}
		return num;
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x000D7DC4 File Offset: 0x000D5FC4
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		if (this.showDescriptor)
		{
			descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.STORAGECAPACITY, GameUtil.GetFormattedMass(this.Capacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.STORAGECAPACITY, GameUtil.GetFormattedMass(this.Capacity(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Effect, false));
		}
		return descriptors;
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x000D7E34 File Offset: 0x000D6034
	public static void MakeItemTemperatureInsulated(GameObject go, bool is_stored, bool is_initializing)
	{
		SimTemperatureTransfer component = go.GetComponent<SimTemperatureTransfer>();
		if (component == null)
		{
			return;
		}
		component.enabled = !is_stored;
	}

	// Token: 0x060025C5 RID: 9669 RVA: 0x000D7E5C File Offset: 0x000D605C
	public static void MakeItemInvisible(GameObject go, bool is_stored, bool is_initializing)
	{
		if (is_initializing)
		{
			return;
		}
		bool flag = !is_stored;
		KAnimControllerBase component = go.GetComponent<KAnimControllerBase>();
		if (component != null && component.enabled != flag)
		{
			component.enabled = flag;
		}
		KSelectable component2 = go.GetComponent<KSelectable>();
		if (component2 != null && component2.enabled != flag)
		{
			component2.enabled = flag;
		}
	}

	// Token: 0x060025C6 RID: 9670 RVA: 0x000D7EB2 File Offset: 0x000D60B2
	public static void MakeItemSealed(GameObject go, bool is_stored, bool is_initializing)
	{
		if (go != null)
		{
			if (is_stored)
			{
				go.GetComponent<KPrefabID>().AddTag(GameTags.Sealed, false);
				return;
			}
			go.GetComponent<KPrefabID>().RemoveTag(GameTags.Sealed);
		}
	}

	// Token: 0x060025C7 RID: 9671 RVA: 0x000D7EE2 File Offset: 0x000D60E2
	public static void MakeItemPreserved(GameObject go, bool is_stored, bool is_initializing)
	{
		if (go != null)
		{
			if (is_stored)
			{
				go.GetComponent<KPrefabID>().AddTag(GameTags.Preserved, false);
				return;
			}
			go.GetComponent<KPrefabID>().RemoveTag(GameTags.Preserved);
		}
	}

	// Token: 0x060025C8 RID: 9672 RVA: 0x000D7F14 File Offset: 0x000D6114
	private void ApplyStoredItemModifiers(GameObject go, bool is_stored, bool is_initializing)
	{
		List<Storage.StoredItemModifier> list = this.defaultStoredItemModifers;
		for (int i = 0; i < list.Count; i++)
		{
			Storage.StoredItemModifier storedItemModifier = list[i];
			for (int j = 0; j < Storage.StoredItemModifierHandlers.Count; j++)
			{
				Storage.StoredItemModifierInfo storedItemModifierInfo = Storage.StoredItemModifierHandlers[j];
				if (storedItemModifierInfo.modifier == storedItemModifier)
				{
					storedItemModifierInfo.toggleState(go, is_stored, is_initializing);
					break;
				}
			}
		}
	}

	// Token: 0x060025C9 RID: 9673 RVA: 0x000D7F80 File Offset: 0x000D6180
	protected virtual void OnCopySettings(object data)
	{
		Storage component = ((GameObject)data).GetComponent<Storage>();
		if (component != null)
		{
			this.SetOnlyFetchMarkedItems(component.onlyFetchMarkedItems);
		}
	}

	// Token: 0x060025CA RID: 9674 RVA: 0x000D7FB0 File Offset: 0x000D61B0
	private void OnPriorityChanged(PrioritySetting priority)
	{
		foreach (GameObject gameObject in this.items)
		{
			gameObject.Trigger(-1626373771, this);
		}
	}

	// Token: 0x060025CB RID: 9675 RVA: 0x000D8008 File Offset: 0x000D6208
	private void OnReachableChanged(object data)
	{
		bool flag = (bool)data;
		KSelectable component = base.GetComponent<KSelectable>();
		if (flag)
		{
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.StorageUnreachable, false);
			return;
		}
		component.AddStatusItem(Db.Get().BuildingStatusItems.StorageUnreachable, this);
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x000D8054 File Offset: 0x000D6254
	public void SetContentsDeleteOffGrid(bool delete_off_grid)
	{
		for (int i = 0; i < this.items.Count; i++)
		{
			Pickupable component = this.items[i].GetComponent<Pickupable>();
			if (component != null)
			{
				component.deleteOffGrid = delete_off_grid;
			}
			Storage component2 = this.items[i].GetComponent<Storage>();
			if (component2 != null)
			{
				component2.SetContentsDeleteOffGrid(delete_off_grid);
			}
		}
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x000D80BC File Offset: 0x000D62BC
	private bool ShouldSaveItem(GameObject go)
	{
		if (!this.shouldSaveItems)
		{
			return false;
		}
		bool flag = false;
		if (go != null && go.GetComponent<SaveLoadRoot>() != null && go.GetComponent<PrimaryElement>().Mass > 0f)
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x000D8104 File Offset: 0x000D6304
	public void Serialize(BinaryWriter writer)
	{
		int num = 0;
		int count = this.items.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.ShouldSaveItem(this.items[i]))
			{
				num++;
			}
		}
		writer.Write(num);
		if (num == 0)
		{
			return;
		}
		if (this.items != null && this.items.Count > 0)
		{
			for (int j = 0; j < this.items.Count; j++)
			{
				GameObject gameObject = this.items[j];
				if (this.ShouldSaveItem(gameObject))
				{
					SaveLoadRoot component = gameObject.GetComponent<SaveLoadRoot>();
					if (component != null)
					{
						string name = gameObject.GetComponent<KPrefabID>().GetSaveLoadTag().Name;
						writer.WriteKleiString(name);
						component.Save(writer);
					}
					else
					{
						global::Debug.Log("Tried to save obj in storage but obj has no SaveLoadRoot", gameObject);
					}
				}
			}
		}
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x000D81E0 File Offset: 0x000D63E0
	public void Deserialize(IReader reader)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		this.ClearItems();
		int num4 = reader.ReadInt32();
		this.items = new List<GameObject>(num4);
		for (int i = 0; i < num4; i++)
		{
			float realtimeSinceStartup2 = Time.realtimeSinceStartup;
			Tag tag = TagManager.Create(reader.ReadKleiString());
			SaveLoadRoot saveLoadRoot = SaveLoadRoot.Load(tag, reader);
			num += Time.realtimeSinceStartup - realtimeSinceStartup2;
			if (saveLoadRoot != null)
			{
				KBatchedAnimController component = saveLoadRoot.GetComponent<KBatchedAnimController>();
				if (component != null)
				{
					component.enabled = false;
				}
				saveLoadRoot.SetRegistered(false);
				float realtimeSinceStartup3 = Time.realtimeSinceStartup;
				GameObject gameObject = this.Store(saveLoadRoot.gameObject, true, true, false, true);
				num2 += Time.realtimeSinceStartup - realtimeSinceStartup3;
				if (gameObject != null)
				{
					Pickupable component2 = gameObject.GetComponent<Pickupable>();
					if (component2 != null)
					{
						float realtimeSinceStartup4 = Time.realtimeSinceStartup;
						component2.OnStore(this);
						num3 += Time.realtimeSinceStartup - realtimeSinceStartup4;
					}
					Storable component3 = gameObject.GetComponent<Storable>();
					if (component3 != null)
					{
						float realtimeSinceStartup5 = Time.realtimeSinceStartup;
						component3.OnStore(this);
						num3 += Time.realtimeSinceStartup - realtimeSinceStartup5;
					}
					if (this.dropOnLoad)
					{
						this.Drop(saveLoadRoot.gameObject, true);
					}
				}
			}
			else
			{
				global::Debug.LogWarning("Tried to deserialize " + tag.ToString() + " into storage but failed", base.gameObject);
			}
		}
	}

	// Token: 0x060025D0 RID: 9680 RVA: 0x000D835C File Offset: 0x000D655C
	private void ClearItems()
	{
		foreach (GameObject gameObject in this.items)
		{
			gameObject.DeleteObject();
		}
		this.items.Clear();
	}

	// Token: 0x060025D1 RID: 9681 RVA: 0x000D83B8 File Offset: 0x000D65B8
	public void UpdateStoredItemCachedCells()
	{
		foreach (GameObject gameObject in this.items)
		{
			Pickupable component = gameObject.GetComponent<Pickupable>();
			if (component != null)
			{
				component.UpdateCachedCellFromStoragePosition();
			}
		}
	}

	// Token: 0x040015FC RID: 5628
	public bool allowItemRemoval;

	// Token: 0x040015FD RID: 5629
	public bool ignoreSourcePriority;

	// Token: 0x040015FE RID: 5630
	public bool onlyTransferFromLowerPriority;

	// Token: 0x040015FF RID: 5631
	public float capacityKg = 20000f;

	// Token: 0x04001600 RID: 5632
	public bool showDescriptor;

	// Token: 0x04001602 RID: 5634
	public bool doDiseaseTransfer = true;

	// Token: 0x04001603 RID: 5635
	public List<Tag> storageFilters;

	// Token: 0x04001604 RID: 5636
	public bool useGunForDelivery = true;

	// Token: 0x04001605 RID: 5637
	public bool sendOnStoreOnSpawn;

	// Token: 0x04001606 RID: 5638
	public bool showInUI = true;

	// Token: 0x04001607 RID: 5639
	public bool storeDropsFromButcherables;

	// Token: 0x04001608 RID: 5640
	public bool allowClearable;

	// Token: 0x04001609 RID: 5641
	public bool showCapacityStatusItem;

	// Token: 0x0400160A RID: 5642
	public bool showCapacityAsMainStatus;

	// Token: 0x0400160B RID: 5643
	public bool showUnreachableStatus;

	// Token: 0x0400160C RID: 5644
	public bool showSideScreenTitleBar;

	// Token: 0x0400160D RID: 5645
	public bool useWideOffsets;

	// Token: 0x0400160E RID: 5646
	public Action<List<GameObject>> onDestroyItemsDropped;

	// Token: 0x0400160F RID: 5647
	public Action<GameObject> OnStorageChange;

	// Token: 0x04001610 RID: 5648
	public Vector2 dropOffset = Vector2.zero;

	// Token: 0x04001611 RID: 5649
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001612 RID: 5650
	public Vector2 gunTargetOffset;

	// Token: 0x04001613 RID: 5651
	public Storage.FetchCategory fetchCategory;

	// Token: 0x04001614 RID: 5652
	public int storageNetworkID = -1;

	// Token: 0x04001615 RID: 5653
	public Tag storageID = GameTags.StoragesIds.DefaultStorage;

	// Token: 0x04001616 RID: 5654
	public float storageFullMargin;

	// Token: 0x04001617 RID: 5655
	public Vector3 storageFXOffset = Vector3.zero;

	// Token: 0x04001618 RID: 5656
	private static readonly EventSystem.IntraObjectHandler<Storage> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Storage>(delegate(Storage component, object data)
	{
		component.OnReachableChanged(data);
	});

	// Token: 0x04001619 RID: 5657
	public Storage.FXPrefix fxPrefix;

	// Token: 0x0400161A RID: 5658
	public List<GameObject> items = new List<GameObject>();

	// Token: 0x0400161B RID: 5659
	[MyCmpGet]
	public Prioritizable prioritizable;

	// Token: 0x0400161C RID: 5660
	[MyCmpGet]
	public Automatable automatable;

	// Token: 0x0400161D RID: 5661
	[MyCmpGet]
	protected PrimaryElement primaryElement;

	// Token: 0x0400161E RID: 5662
	public bool dropOnLoad;

	// Token: 0x0400161F RID: 5663
	protected float maxKGPerItem = float.MaxValue;

	// Token: 0x04001620 RID: 5664
	private bool endOfLife;

	// Token: 0x04001621 RID: 5665
	public bool allowSettingOnlyFetchMarkedItems = true;

	// Token: 0x04001622 RID: 5666
	[Serialize]
	private bool onlyFetchMarkedItems;

	// Token: 0x04001623 RID: 5667
	[Serialize]
	private bool shouldSaveItems = true;

	// Token: 0x04001624 RID: 5668
	public float storageWorkTime = 1.5f;

	// Token: 0x04001625 RID: 5669
	private static readonly List<Storage.StoredItemModifierInfo> StoredItemModifierHandlers = new List<Storage.StoredItemModifierInfo>
	{
		new Storage.StoredItemModifierInfo(Storage.StoredItemModifier.Hide, new Action<GameObject, bool, bool>(Storage.MakeItemInvisible)),
		new Storage.StoredItemModifierInfo(Storage.StoredItemModifier.Insulate, new Action<GameObject, bool, bool>(Storage.MakeItemTemperatureInsulated)),
		new Storage.StoredItemModifierInfo(Storage.StoredItemModifier.Seal, new Action<GameObject, bool, bool>(Storage.MakeItemSealed)),
		new Storage.StoredItemModifierInfo(Storage.StoredItemModifier.Preserve, new Action<GameObject, bool, bool>(Storage.MakeItemPreserved))
	};

	// Token: 0x04001626 RID: 5670
	[SerializeField]
	private List<Storage.StoredItemModifier> defaultStoredItemModifers = new List<Storage.StoredItemModifier> { Storage.StoredItemModifier.Hide };

	// Token: 0x04001627 RID: 5671
	public static readonly List<Storage.StoredItemModifier> StandardSealedStorage = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Seal
	};

	// Token: 0x04001628 RID: 5672
	public static readonly List<Storage.StoredItemModifier> StandardFabricatorStorage = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Preserve
	};

	// Token: 0x04001629 RID: 5673
	public static readonly List<Storage.StoredItemModifier> StandardInsulatedStorage = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Seal,
		Storage.StoredItemModifier.Insulate
	};

	// Token: 0x0400162B RID: 5675
	private static StatusItem capacityStatusItem;

	// Token: 0x0400162C RID: 5676
	private static readonly EventSystem.IntraObjectHandler<Storage> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<Storage>(GameTags.Dead, delegate(Storage component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x0400162D RID: 5677
	private static readonly EventSystem.IntraObjectHandler<Storage> OnQueueDestroyObjectDelegate = new EventSystem.IntraObjectHandler<Storage>(delegate(Storage component, object data)
	{
		component.OnQueueDestroyObject(data);
	});

	// Token: 0x0400162E RID: 5678
	private static readonly EventSystem.IntraObjectHandler<Storage> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Storage>(delegate(Storage component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0400162F RID: 5679
	private List<GameObject> deleted_objects;

	// Token: 0x020014B3 RID: 5299
	public enum StoredItemModifier
	{
		// Token: 0x04006D94 RID: 28052
		Insulate,
		// Token: 0x04006D95 RID: 28053
		Hide,
		// Token: 0x04006D96 RID: 28054
		Seal,
		// Token: 0x04006D97 RID: 28055
		Preserve
	}

	// Token: 0x020014B4 RID: 5300
	public enum FetchCategory
	{
		// Token: 0x04006D99 RID: 28057
		Building,
		// Token: 0x04006D9A RID: 28058
		GeneralStorage,
		// Token: 0x04006D9B RID: 28059
		StorageSweepOnly
	}

	// Token: 0x020014B5 RID: 5301
	public enum FXPrefix
	{
		// Token: 0x04006D9D RID: 28061
		Delivered,
		// Token: 0x04006D9E RID: 28062
		PickedUp
	}

	// Token: 0x020014B6 RID: 5302
	private struct StoredItemModifierInfo
	{
		// Token: 0x06008EA8 RID: 36520 RVA: 0x0035C332 File Offset: 0x0035A532
		public StoredItemModifierInfo(Storage.StoredItemModifier modifier, Action<GameObject, bool, bool> toggle_state)
		{
			this.modifier = modifier;
			this.toggleState = toggle_state;
		}

		// Token: 0x04006D9F RID: 28063
		public Storage.StoredItemModifier modifier;

		// Token: 0x04006DA0 RID: 28064
		public Action<GameObject, bool, bool> toggleState;
	}
}
