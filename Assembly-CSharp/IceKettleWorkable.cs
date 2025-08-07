using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200074E RID: 1870
public class IceKettleWorkable : Workable
{
	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06002F87 RID: 12167 RVA: 0x00110493 File Offset: 0x0010E693
	// (set) Token: 0x06002F88 RID: 12168 RVA: 0x0011049B File Offset: 0x0010E69B
	public MeterController meter { get; private set; }

	// Token: 0x06002F89 RID: 12169 RVA: 0x001104A4 File Offset: 0x0010E6A4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_arrow", "meter_scale" });
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_icemelter_kettle_kanim") };
		this.synchronizeAnims = true;
		base.SetOffsets(new CellOffset[] { this.workCellOffset });
		base.SetWorkTime(5f);
		this.resetProgressOnStop = true;
		this.showProgressBar = false;
		this.storage.onDestroyItemsDropped = new Action<List<GameObject>>(this.RestoreStoredItemsInteractions);
		this.handler = base.Subscribe(-1697596308, new Action<object>(this.OnStorageChanged));
	}

	// Token: 0x06002F8A RID: 12170 RVA: 0x0011057E File Offset: 0x0010E77E
	protected override void OnSpawn()
	{
		this.AdjustStoredItemsPositionsAndWorkable();
	}

	// Token: 0x06002F8B RID: 12171 RVA: 0x00110588 File Offset: 0x0010E788
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		Pickupable.PickupableStartWorkInfo pickupableStartWorkInfo = (Pickupable.PickupableStartWorkInfo)worker.GetStartWorkInfo();
		this.meter.gameObject.SetActive(true);
		PrimaryElement component = pickupableStartWorkInfo.originalPickupable.GetComponent<PrimaryElement>();
		this.meter.SetSymbolTint(new KAnimHashedString("meter_fill"), component.Element.substance.colour);
		this.meter.SetSymbolTint(new KAnimHashedString("water1"), component.Element.substance.colour);
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x00110610 File Offset: 0x0010E810
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		float num = (this.workTime - base.WorkTimeRemaining) / this.workTime;
		this.meter.SetPositionPercent(Mathf.Clamp01(num));
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x0011064C File Offset: 0x0010E84C
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Storage component = worker.GetComponent<Storage>();
		Pickupable.PickupableStartWorkInfo pickupableStartWorkInfo = (Pickupable.PickupableStartWorkInfo)worker.GetStartWorkInfo();
		if (pickupableStartWorkInfo.amount > 0f)
		{
			this.storage.TransferMass(component, pickupableStartWorkInfo.originalPickupable.KPrefabID.PrefabID(), pickupableStartWorkInfo.amount, false, false, false);
		}
		GameObject gameObject = component.FindFirst(pickupableStartWorkInfo.originalPickupable.KPrefabID.PrefabID());
		if (gameObject != null)
		{
			pickupableStartWorkInfo.setResultCb(gameObject);
		}
		else
		{
			pickupableStartWorkInfo.setResultCb(null);
		}
		base.OnCompleteWork(worker);
		foreach (GameObject gameObject2 in component.items)
		{
			if (gameObject2.HasTag(GameTags.Liquid))
			{
				Pickupable component2 = gameObject2.GetComponent<Pickupable>();
				this.RestorePickupableInteractions(component2);
			}
		}
	}

	// Token: 0x06002F8E RID: 12174 RVA: 0x00110740 File Offset: 0x0010E940
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		this.meter.gameObject.SetActive(false);
	}

	// Token: 0x06002F8F RID: 12175 RVA: 0x0011075A File Offset: 0x0010E95A
	private void OnStorageChanged(object obj)
	{
		this.AdjustStoredItemsPositionsAndWorkable();
	}

	// Token: 0x06002F90 RID: 12176 RVA: 0x00110764 File Offset: 0x0010E964
	private void AdjustStoredItemsPositionsAndWorkable()
	{
		int num = Grid.PosToCell(this);
		Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(num, new CellOffset(0, 0)), Grid.SceneLayer.Ore);
		foreach (GameObject gameObject in this.storage.items)
		{
			Pickupable component = gameObject.GetComponent<Pickupable>();
			component.transform.SetPosition(vector);
			component.UpdateCachedCell(num);
			this.OverridePickupableInteractions(component);
		}
	}

	// Token: 0x06002F91 RID: 12177 RVA: 0x001107F4 File Offset: 0x0010E9F4
	private void OverridePickupableInteractions(Pickupable pickupable)
	{
		pickupable.AddTag(GameTags.LiquidSource);
		pickupable.targetWorkable = this;
		pickupable.SetOffsets(new CellOffset[] { this.workCellOffset });
	}

	// Token: 0x06002F92 RID: 12178 RVA: 0x00110821 File Offset: 0x0010EA21
	private void RestorePickupableInteractions(Pickupable pickupable)
	{
		pickupable.RemoveTag(GameTags.LiquidSource);
		pickupable.targetWorkable = pickupable;
		pickupable.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06002F93 RID: 12179 RVA: 0x00110840 File Offset: 0x0010EA40
	private void RestoreStoredItemsInteractions(List<GameObject> specificItems = null)
	{
		specificItems = ((specificItems == null) ? this.storage.items : specificItems);
		foreach (GameObject gameObject in specificItems)
		{
			Pickupable component = gameObject.GetComponent<Pickupable>();
			this.RestorePickupableInteractions(component);
		}
	}

	// Token: 0x06002F94 RID: 12180 RVA: 0x001108A8 File Offset: 0x0010EAA8
	protected override void OnCleanUp()
	{
		if (base.worker != null)
		{
			ChoreDriver component = base.worker.GetComponent<ChoreDriver>();
			base.worker.StopWork();
			component.StopChore();
		}
		this.RestoreStoredItemsInteractions(null);
		base.Unsubscribe(this.handler);
		base.OnCleanUp();
	}

	// Token: 0x04001C3F RID: 7231
	public Storage storage;

	// Token: 0x04001C40 RID: 7232
	private int handler;

	// Token: 0x04001C42 RID: 7234
	public CellOffset workCellOffset = new CellOffset(0, 0);
}
