using System;
using System.Linq;
using FoodRehydrator;
using KSerialization;
using UnityEngine;

// Token: 0x0200089F RID: 2207
public class DehydratedFoodPackage : Workable, IApproachable
{
	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x06003D63 RID: 15715 RVA: 0x001568D4 File Offset: 0x00154AD4
	// (set) Token: 0x06003D64 RID: 15716 RVA: 0x00156903 File Offset: 0x00154B03
	public GameObject Rehydrator
	{
		get
		{
			Storage storage = base.gameObject.GetComponent<Pickupable>().storage;
			if (storage != null)
			{
				return storage.gameObject;
			}
			return null;
		}
		private set
		{
		}
	}

	// Token: 0x06003D65 RID: 15717 RVA: 0x00156905 File Offset: 0x00154B05
	public override BuildingFacade GetBuildingFacade()
	{
		if (!(this.Rehydrator != null))
		{
			return null;
		}
		return this.Rehydrator.GetComponent<BuildingFacade>();
	}

	// Token: 0x06003D66 RID: 15718 RVA: 0x00156922 File Offset: 0x00154B22
	public override KAnimControllerBase GetAnimController()
	{
		if (!(this.Rehydrator != null))
		{
			return null;
		}
		return this.Rehydrator.GetComponent<KAnimControllerBase>();
	}

	// Token: 0x06003D67 RID: 15719 RVA: 0x00156940 File Offset: 0x00154B40
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetOffsets(new CellOffset[]
		{
			default(CellOffset),
			new CellOffset(0, -1)
		});
		if (this.storage.items.Count < 1)
		{
			this.storage.ConsumeAllIgnoringDisease(this.FoodTag);
			int num = Grid.PosToCell(this);
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(this.FoodTag), Grid.CellToPosCBC(num, Grid.SceneLayer.Creatures), Grid.SceneLayer.Creatures, null, 0);
			gameObject.SetActive(true);
			gameObject.GetComponent<Edible>().Calories = 1000000f;
			this.storage.Store(gameObject, false, false, true, false);
		}
		base.Subscribe(-1697596308, new Action<object>(this.StorageChangeHandler));
		this.DehydrateItem(this.storage.items.ElementAtOrDefault(0));
	}

	// Token: 0x06003D68 RID: 15720 RVA: 0x00156A0C File Offset: 0x00154C0C
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		if (this.Rehydrator != null)
		{
			DehydratedManager component = this.Rehydrator.GetComponent<DehydratedManager>();
			if (component != null)
			{
				component.SetFabricatedFoodSymbol(this.FoodTag);
			}
			this.Rehydrator.GetComponent<AccessabilityManager>().SetActiveWorkable(this);
		}
	}

	// Token: 0x06003D69 RID: 15721 RVA: 0x00156A60 File Offset: 0x00154C60
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		if (this.storage.items.Count != 1)
		{
			DebugUtil.DevAssert(false, "OnCompleteWork invalid contents of package", null);
			return;
		}
		GameObject gameObject = this.storage.items[0];
		this.storage.Transfer(worker.GetComponent<Storage>(), false, false);
		DebugUtil.DevAssert(this.Rehydrator != null, "OnCompleteWork but no rehydrator", null);
		DehydratedManager component = this.Rehydrator.GetComponent<DehydratedManager>();
		this.Rehydrator.GetComponent<AccessabilityManager>().SetActiveWorkable(null);
		component.ConsumeResourcesForRehydration(base.gameObject, gameObject);
		DehydratedFoodPackage.RehydrateStartWorkItem rehydrateStartWorkItem = (DehydratedFoodPackage.RehydrateStartWorkItem)worker.GetStartWorkInfo();
		if (rehydrateStartWorkItem != null && rehydrateStartWorkItem.setResultCb != null && gameObject != null)
		{
			rehydrateStartWorkItem.setResultCb(gameObject);
		}
	}

	// Token: 0x06003D6A RID: 15722 RVA: 0x00156B24 File Offset: 0x00154D24
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		if (this.Rehydrator != null)
		{
			this.Rehydrator.GetComponent<AccessabilityManager>().SetActiveWorkable(null);
		}
	}

	// Token: 0x06003D6B RID: 15723 RVA: 0x00156B4C File Offset: 0x00154D4C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06003D6C RID: 15724 RVA: 0x00156B54 File Offset: 0x00154D54
	private void StorageChangeHandler(object obj)
	{
		GameObject gameObject = (GameObject)obj;
		DebugUtil.DevAssert(!this.storage.items.Contains(gameObject), "Attempting to add item to a dehydrated food package which is not allowed", null);
		this.RehydrateItem(gameObject);
	}

	// Token: 0x06003D6D RID: 15725 RVA: 0x00156B90 File Offset: 0x00154D90
	public void DehydrateItem(GameObject item)
	{
		DebugUtil.DevAssert(item != null, "Attempting to dehydrate contents of an empty packet", null);
		if (this.storage.items.Count != 1 || item == null)
		{
			DebugUtil.DevAssert(false, "DehydrateItem called, incorrect content", null);
			return;
		}
		item.AddTag(GameTags.Dehydrated);
	}

	// Token: 0x06003D6E RID: 15726 RVA: 0x00156BE4 File Offset: 0x00154DE4
	public void RehydrateItem(GameObject item)
	{
		if (this.storage.items.Count != 0)
		{
			DebugUtil.DevAssert(false, "RehydrateItem called, incorrect storage content", null);
			return;
		}
		item.RemoveTag(GameTags.Dehydrated);
		item.AddTag(GameTags.Rehydrated);
		item.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.RehydratedFood, null);
	}

	// Token: 0x06003D6F RID: 15727 RVA: 0x00156C48 File Offset: 0x00154E48
	private void Swap<Type>(ref Type a, ref Type b)
	{
		Type type = a;
		a = b;
		b = type;
	}

	// Token: 0x040025F6 RID: 9718
	[Serialize]
	public Tag FoodTag;

	// Token: 0x040025F7 RID: 9719
	[MyCmpReq]
	private Storage storage;

	// Token: 0x02001866 RID: 6246
	public class RehydrateStartWorkItem : WorkerBase.StartWorkInfo
	{
		// Token: 0x06009C7B RID: 40059 RVA: 0x00390CE5 File Offset: 0x0038EEE5
		public RehydrateStartWorkItem(DehydratedFoodPackage pkg, Action<GameObject> setResultCB)
			: base(pkg)
		{
			this.package = pkg;
			this.setResultCb = setResultCB;
		}

		// Token: 0x040078DB RID: 30939
		public DehydratedFoodPackage package;

		// Token: 0x040078DC RID: 30940
		public Action<GameObject> setResultCb;
	}
}
