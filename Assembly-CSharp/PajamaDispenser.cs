using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000239 RID: 569
public class PajamaDispenser : Workable, IDispenser
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000B6B RID: 2923 RVA: 0x00045B18 File Offset: 0x00043D18
	// (remove) Token: 0x06000B6C RID: 2924 RVA: 0x00045B50 File Offset: 0x00043D50
	public event global::System.Action OnStopWorkEvent;

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000B6D RID: 2925 RVA: 0x00045B85 File Offset: 0x00043D85
	// (set) Token: 0x06000B6E RID: 2926 RVA: 0x00045B90 File Offset: 0x00043D90
	private WorkChore<PajamaDispenser> Chore
	{
		get
		{
			return this.chore;
		}
		set
		{
			this.chore = value;
			if (this.chore != null)
			{
				base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.DispenseRequested, null);
				return;
			}
			base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.DispenseRequested, true);
		}
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x00045BEF File Offset: 0x00043DEF
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (PajamaDispenser.pajamaPrefab != null)
		{
			return;
		}
		PajamaDispenser.pajamaPrefab = Assets.GetPrefab(new Tag("SleepClinicPajamas"));
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x00045C1C File Offset: 0x00043E1C
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Vector3 targetPoint = this.GetTargetPoint();
		targetPoint.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingFront);
		Util.KInstantiate(PajamaDispenser.pajamaPrefab, targetPoint, Quaternion.identity, null, null, true, 0).SetActive(true);
		this.hasDispenseChore = false;
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x00045C60 File Offset: 0x00043E60
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		if (this.Chore != null && this.Chore.smi.IsRunning())
		{
			this.Chore.Cancel("work interrupted");
		}
		this.Chore = null;
		if (this.hasDispenseChore)
		{
			this.FetchPajamas();
		}
		if (this.OnStopWorkEvent != null)
		{
			this.OnStopWorkEvent();
		}
	}

	// Token: 0x06000B72 RID: 2930 RVA: 0x00045CC8 File Offset: 0x00043EC8
	[ContextMenu("fetch")]
	public void FetchPajamas()
	{
		if (this.Chore != null)
		{
			return;
		}
		this.hasDispenseChore = true;
		this.Chore = new WorkChore<PajamaDispenser>(Db.Get().ChoreTypes.EquipmentFetch, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, false);
		this.Chore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
	}

	// Token: 0x06000B73 RID: 2931 RVA: 0x00045D28 File Offset: 0x00043F28
	public void CancelFetch()
	{
		if (this.Chore == null)
		{
			return;
		}
		this.Chore.Cancel("User Cancelled");
		this.Chore = null;
		this.hasDispenseChore = false;
		base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.DispenseRequested, false);
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00045D7D File Offset: 0x00043F7D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.hasDispenseChore)
		{
			this.FetchPajamas();
		}
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x00045D93 File Offset: 0x00043F93
	public List<Tag> DispensedItems()
	{
		return PajamaDispenser.PajamaList;
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x00045D9A File Offset: 0x00043F9A
	public Tag SelectedItem()
	{
		return PajamaDispenser.PajamaList[0];
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x00045DA7 File Offset: 0x00043FA7
	public void SelectItem(Tag tag)
	{
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x00045DA9 File Offset: 0x00043FA9
	public void OnOrderDispense()
	{
		this.FetchPajamas();
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x00045DB1 File Offset: 0x00043FB1
	public void OnCancelDispense()
	{
		this.CancelFetch();
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00045DB9 File Offset: 0x00043FB9
	public bool HasOpenChore()
	{
		return this.Chore != null;
	}

	// Token: 0x040007E9 RID: 2025
	[Serialize]
	private bool hasDispenseChore;

	// Token: 0x040007EA RID: 2026
	private static GameObject pajamaPrefab = null;

	// Token: 0x040007EC RID: 2028
	private WorkChore<PajamaDispenser> chore;

	// Token: 0x040007ED RID: 2029
	private static List<Tag> PajamaList = new List<Tag> { "SleepClinicPajamas" };
}
