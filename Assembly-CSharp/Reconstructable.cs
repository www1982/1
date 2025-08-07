using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000600 RID: 1536
public class Reconstructable : KMonoBehaviour
{
	// Token: 0x17000193 RID: 403
	// (get) Token: 0x0600245E RID: 9310 RVA: 0x000CF4E6 File Offset: 0x000CD6E6
	public bool AllowReconstruct
	{
		get
		{
			return this.deconstructable.allowDeconstruction && (this.building.Def.ShowInBuildMenu || SelectModuleSideScreen.moduleButtonSortOrder.Contains(this.building.Def.PrefabID));
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x0600245F RID: 9311 RVA: 0x000CF525 File Offset: 0x000CD725
	public Tag PrimarySelectedElementTag
	{
		get
		{
			return this.selectedElementsTags[0];
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06002460 RID: 9312 RVA: 0x000CF533 File Offset: 0x000CD733
	public bool ReconstructRequested
	{
		get
		{
			return this.reconstructRequested;
		}
	}

	// Token: 0x06002461 RID: 9313 RVA: 0x000CF53B File Offset: 0x000CD73B
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06002462 RID: 9314 RVA: 0x000CF544 File Offset: 0x000CD744
	public void RequestReconstruct(Tag newElement)
	{
		if (!this.deconstructable.allowDeconstruction)
		{
			return;
		}
		this.reconstructRequested = !this.reconstructRequested;
		if (this.reconstructRequested)
		{
			this.deconstructable.QueueDeconstruction(false);
			this.selectedElementsTags = new Tag[] { newElement };
		}
		else
		{
			this.deconstructable.CancelDeconstruction();
		}
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06002463 RID: 9315 RVA: 0x000CF5B8 File Offset: 0x000CD7B8
	public void CancelReconstructOrder()
	{
		this.reconstructRequested = false;
		this.deconstructable.CancelDeconstruction();
		base.Trigger(954267658, null);
	}

	// Token: 0x06002464 RID: 9316 RVA: 0x000CF5D8 File Offset: 0x000CD7D8
	public void TryCommenceReconstruct()
	{
		if (!this.deconstructable.allowDeconstruction)
		{
			return;
		}
		if (!this.reconstructRequested)
		{
			return;
		}
		string facadeID = this.building.GetComponent<BuildingFacade>().CurrentFacade;
		Vector3 position = this.building.transform.position;
		Orientation orientation = this.building.Orientation;
		GameScheduler.Instance.ScheduleNextFrame("Reconstruct", delegate(object data)
		{
			this.building.Def.TryPlace(null, position, orientation, this.selectedElementsTags, facadeID, false, 0);
		}, null, null);
	}

	// Token: 0x04001537 RID: 5431
	[MyCmpReq]
	private Deconstructable deconstructable;

	// Token: 0x04001538 RID: 5432
	[MyCmpReq]
	private Building building;

	// Token: 0x04001539 RID: 5433
	[Serialize]
	private Tag[] selectedElementsTags;

	// Token: 0x0400153A RID: 5434
	[Serialize]
	private bool reconstructRequested;
}
