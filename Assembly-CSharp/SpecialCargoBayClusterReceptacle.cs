using System;
using System.Linq;
using KSerialization;
using UnityEngine;

// Token: 0x0200040C RID: 1036
public class SpecialCargoBayClusterReceptacle : SingleEntityReceptacle, IBaggedStateAnimationInstructions
{
	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06001537 RID: 5431 RVA: 0x00078D3C File Offset: 0x00076F3C
	public bool IsRocketOnGround
	{
		get
		{
			return base.gameObject.HasTag(GameTags.RocketOnGround);
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06001538 RID: 5432 RVA: 0x00078D4E File Offset: 0x00076F4E
	public bool IsRocketInSpace
	{
		get
		{
			return base.gameObject.HasTag(GameTags.RocketInSpace);
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06001539 RID: 5433 RVA: 0x00078D60 File Offset: 0x00076F60
	private bool isDoorOpen
	{
		get
		{
			return this.capsule.sm.IsDoorOpen.Get(this.capsule);
		}
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x00078D7D File Offset: 0x00076F7D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.choreType = Db.Get().ChoreTypes.CreatureFetch;
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x00078D9C File Offset: 0x00076F9C
	protected override void OnSpawn()
	{
		this.capsule = base.gameObject.GetSMI<SpecialCargoBayCluster.Instance>();
		this.SetupLootSymbolObject();
		base.OnSpawn();
		this.SetTrappedCritterAnimations(base.Occupant);
		base.Subscribe(-1697596308, new Action<object>(this.OnCritterStorageChanged));
		base.Subscribe<SpecialCargoBayClusterReceptacle>(-887025858, SpecialCargoBayClusterReceptacle.OnRocketLandedDelegate);
		base.Subscribe<SpecialCargoBayClusterReceptacle>(-1447108533, SpecialCargoBayClusterReceptacle.OnCargoBayRelocatedDelegate);
		base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x00078E24 File Offset: 0x00077024
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject != null)
		{
			SpecialCargoBayClusterReceptacle component = gameObject.GetComponent<SpecialCargoBayClusterReceptacle>();
			if (component != null)
			{
				Tag tag = ((component.Occupant != null) ? component.Occupant.PrefabID() : component.requestedEntityTag);
				if (base.Occupant != null && base.Occupant.PrefabID() != tag)
				{
					this.ClearOccupant();
				}
				if (tag != this.requestedEntityTag && this.fetchChore != null)
				{
					base.CancelActiveRequest();
				}
				if (tag != Tag.Invalid)
				{
					this.CreateOrder(tag, component.requestedEntityAdditionalFilterTag);
				}
			}
		}
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x00078ED3 File Offset: 0x000770D3
	public override void CreateOrder(Tag entityTag, Tag additionalFilterTag)
	{
		base.CreateOrder(entityTag, additionalFilterTag);
		if (this.fetchChore != null)
		{
			this.fetchChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
		}
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x00078EFC File Offset: 0x000770FC
	public void SetupLootSymbolObject()
	{
		Vector3 storePositionForDrops = this.capsule.GetStorePositionForDrops();
		storePositionForDrops.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingUse);
		GameObject gameObject = new GameObject();
		gameObject.name = "lootSymbol";
		gameObject.transform.SetParent(base.transform, true);
		gameObject.SetActive(false);
		gameObject.transform.SetPosition(storePositionForDrops);
		KBatchedAnimTracker kbatchedAnimTracker = gameObject.AddOrGet<KBatchedAnimTracker>();
		kbatchedAnimTracker.symbol = "loot";
		kbatchedAnimTracker.forceAlwaysAlive = true;
		kbatchedAnimTracker.matchParentOffset = true;
		this.lootKBAC = gameObject.AddComponent<KBatchedAnimController>();
		this.lootKBAC.AnimFiles = new KAnimFile[] { Assets.GetAnim("mushbar_kanim") };
		this.lootKBAC.initialAnim = "object";
		this.buildingAnimCtr.SetSymbolVisiblity("loot", false);
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x00078FD4 File Offset: 0x000771D4
	protected override void ClearOccupant()
	{
		this.LastCritterDead = null;
		if (base.occupyingObject != null)
		{
			this.UnsubscribeFromOccupant();
		}
		this.originWorldID = -1;
		base.occupyingObject = null;
		base.UpdateActive();
		this.UpdateStatusItem();
		if (!this.isDoorOpen)
		{
			if (this.IsRocketOnGround)
			{
				this.SetLootSymbolImage(Tag.Invalid);
				this.capsule.OpenDoor();
			}
		}
		else
		{
			this.capsule.DropInventory();
		}
		base.Trigger(-731304873, base.occupyingObject);
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x0007905A File Offset: 0x0007725A
	private void OnCritterStorageChanged(object obj)
	{
		if (obj != null && this.storage.MassStored() == 0f && base.Occupant != null && base.Occupant == (GameObject)obj)
		{
			this.ClearOccupant();
		}
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x00079098 File Offset: 0x00077298
	protected override void SubscribeToOccupant()
	{
		base.SubscribeToOccupant();
		base.Subscribe(base.Occupant, -1582839653, new Action<object>(this.OnTrappedCritterTagsChanged));
		base.Subscribe(base.Occupant, 395373363, new Action<object>(this.OnCreatureInStorageDied));
		base.Subscribe(base.Occupant, 663420073, new Action<object>(this.OnBabyInStorageGrows));
		this.SetupCritterTracker();
		for (int i = 0; i < SpecialCargoBayClusterReceptacle.tagsForCritter.Length; i++)
		{
			Tag tag = SpecialCargoBayClusterReceptacle.tagsForCritter[i];
			base.Occupant.AddTag(tag);
		}
		base.Occupant.GetComponent<Health>().UpdateHealthBar();
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x00079148 File Offset: 0x00077348
	protected override void UnsubscribeFromOccupant()
	{
		base.UnsubscribeFromOccupant();
		base.Unsubscribe(base.Occupant, -1582839653, new Action<object>(this.OnTrappedCritterTagsChanged));
		base.Unsubscribe(base.Occupant, 395373363, new Action<object>(this.OnCreatureInStorageDied));
		base.Unsubscribe(base.Occupant, 663420073, new Action<object>(this.OnBabyInStorageGrows));
		this.RemoveCritterTracker();
		if (base.Occupant != null)
		{
			for (int i = 0; i < SpecialCargoBayClusterReceptacle.tagsForCritter.Length; i++)
			{
				Tag tag = SpecialCargoBayClusterReceptacle.tagsForCritter[i];
				base.occupyingObject.RemoveTag(tag);
			}
			base.occupyingObject.GetComponent<Health>().UpdateHealthBar();
		}
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x00079200 File Offset: 0x00077400
	public void SetLootSymbolImage(Tag productTag)
	{
		bool flag = productTag != Tag.Invalid;
		this.lootKBAC.gameObject.SetActive(flag);
		if (flag)
		{
			GameObject prefab = Assets.GetPrefab(productTag.ToString());
			this.lootKBAC.SwapAnims(prefab.GetComponent<KBatchedAnimController>().AnimFiles);
			this.lootKBAC.Play("object", KAnim.PlayMode.Loop, 1f, 0f);
		}
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x0007927B File Offset: 0x0007747B
	private void SetupCritterTracker()
	{
		if (base.Occupant != null)
		{
			KBatchedAnimTracker kbatchedAnimTracker = base.Occupant.AddOrGet<KBatchedAnimTracker>();
			kbatchedAnimTracker.symbol = "critter";
			kbatchedAnimTracker.forceAlwaysAlive = true;
			kbatchedAnimTracker.matchParentOffset = true;
		}
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x000792B4 File Offset: 0x000774B4
	private void RemoveCritterTracker()
	{
		if (base.Occupant != null)
		{
			KBatchedAnimTracker component = base.Occupant.GetComponent<KBatchedAnimTracker>();
			if (component != null)
			{
				global::UnityEngine.Object.Destroy(component);
			}
		}
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x000792EA File Offset: 0x000774EA
	protected override void ConfigureOccupyingObject(GameObject source)
	{
		this.originWorldID = source.GetMyWorldId();
		source.GetComponent<Baggable>().SetWrangled();
		this.SetTrappedCritterAnimations(source);
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x0007930C File Offset: 0x0007750C
	private void OnBabyInStorageGrows(object obj)
	{
		int num = this.originWorldID;
		this.UnsubscribeFromOccupant();
		GameObject gameObject = (GameObject)obj;
		this.storage.Store(gameObject, false, false, true, false);
		base.occupyingObject = gameObject;
		this.ConfigureOccupyingObject(gameObject);
		this.originWorldID = num;
		this.PositionOccupyingObject();
		this.SubscribeToOccupant();
		this.UpdateStatusItem();
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x00079368 File Offset: 0x00077568
	private void OnTrappedCritterTagsChanged(object obj)
	{
		if (base.Occupant != null && base.Occupant.HasTag(GameTags.Creatures.Die) && this.LastCritterDead != base.Occupant)
		{
			this.capsule.PlayDeathCloud();
			this.LastCritterDead = base.Occupant;
			this.RemoveCritterTracker();
			base.Occupant.GetComponent<KBatchedAnimController>().SetVisiblity(false);
			Butcherable component = base.Occupant.GetComponent<Butcherable>();
			if (component != null && component.drops != null && component.drops.Count > 0)
			{
				this.SetLootSymbolImage(component.drops.Keys.ToList<string>()[0].ToTag());
			}
			else
			{
				this.SetLootSymbolImage(Tag.Invalid);
			}
			if (this.IsRocketInSpace)
			{
				DeathStates.Instance smi = base.Occupant.GetSMI<DeathStates.Instance>();
				smi.GoTo(smi.sm.pst);
			}
		}
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x0007945C File Offset: 0x0007765C
	private void OnCreatureInStorageDied(object drops_obj)
	{
		GameObject[] array = drops_obj as GameObject[];
		if (array != null)
		{
			foreach (GameObject gameObject in array)
			{
				this.sideProductStorage.Store(gameObject, false, false, true, false);
			}
		}
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x00079496 File Offset: 0x00077696
	private void SetTrappedCritterAnimations(GameObject critter)
	{
		if (critter != null)
		{
			KBatchedAnimController component = critter.GetComponent<KBatchedAnimController>();
			component.FlipX = false;
			component.Play("rocket_biological", KAnim.PlayMode.Loop, 1f, 0f);
			component.enabled = false;
			component.enabled = true;
		}
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x000794D6 File Offset: 0x000776D6
	protected override void PositionOccupyingObject()
	{
		if (base.Occupant != null)
		{
			base.Occupant.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.BuildingUse);
			this.SetupCritterTracker();
		}
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x00079500 File Offset: 0x00077700
	protected override void UpdateStatusItem()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		bool flag = base.Occupant != null;
		if (component != null)
		{
			if (flag)
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.SpecialCargoBayClusterCritterStored, this);
			}
			else
			{
				component.RemoveStatusItem(Db.Get().BuildingStatusItems.SpecialCargoBayClusterCritterStored, false);
			}
		}
		base.UpdateStatusItem();
	}

	// Token: 0x0600154D RID: 5453 RVA: 0x00079563 File Offset: 0x00077763
	private void OnCargoBayRelocated(object data)
	{
		if (base.Occupant != null)
		{
			KBatchedAnimController component = base.Occupant.GetComponent<KBatchedAnimController>();
			component.enabled = false;
			component.enabled = true;
		}
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x0007958C File Offset: 0x0007778C
	private void OnRocketLanded(object data)
	{
		if (base.Occupant != null)
		{
			ClusterManager.Instance.MigrateCritter(base.Occupant, base.gameObject.GetMyWorldId(), this.originWorldID);
			this.originWorldID = base.Occupant.GetMyWorldId();
		}
		if (base.Occupant == null && !this.isDoorOpen)
		{
			this.SetLootSymbolImage(Tag.Invalid);
			if (this.sideProductStorage.MassStored() > 0f)
			{
				this.capsule.OpenDoor();
			}
		}
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x00079617 File Offset: 0x00077817
	public string GetBaggedAnimationName()
	{
		return "rocket_biological";
	}

	// Token: 0x04000C98 RID: 3224
	public const string TRAPPED_CRITTER_ANIM_NAME = "rocket_biological";

	// Token: 0x04000C99 RID: 3225
	[MyCmpReq]
	private SymbolOverrideController symbolOverrideComponent;

	// Token: 0x04000C9A RID: 3226
	[MyCmpGet]
	private KBatchedAnimController buildingAnimCtr;

	// Token: 0x04000C9B RID: 3227
	private KBatchedAnimController lootKBAC;

	// Token: 0x04000C9C RID: 3228
	public Storage sideProductStorage;

	// Token: 0x04000C9D RID: 3229
	private SpecialCargoBayCluster.Instance capsule;

	// Token: 0x04000C9E RID: 3230
	private GameObject LastCritterDead;

	// Token: 0x04000C9F RID: 3231
	[Serialize]
	private int originWorldID;

	// Token: 0x04000CA0 RID: 3232
	private static Tag[] tagsForCritter = new Tag[]
	{
		GameTags.Creatures.TrappedInCargoBay,
		GameTags.Creatures.PausedHunger,
		GameTags.Creatures.PausedReproduction,
		GameTags.Creatures.PreventGrowAnimation,
		GameTags.HideHealthBar,
		GameTags.PreventDeadAnimation
	};

	// Token: 0x04000CA1 RID: 3233
	private static readonly EventSystem.IntraObjectHandler<SpecialCargoBayClusterReceptacle> OnRocketLandedDelegate = new EventSystem.IntraObjectHandler<SpecialCargoBayClusterReceptacle>(delegate(SpecialCargoBayClusterReceptacle component, object data)
	{
		component.OnRocketLanded(data);
	});

	// Token: 0x04000CA2 RID: 3234
	private static readonly EventSystem.IntraObjectHandler<SpecialCargoBayClusterReceptacle> OnCargoBayRelocatedDelegate = new EventSystem.IntraObjectHandler<SpecialCargoBayClusterReceptacle>(delegate(SpecialCargoBayClusterReceptacle component, object data)
	{
		component.OnCargoBayRelocated(data);
	});
}
