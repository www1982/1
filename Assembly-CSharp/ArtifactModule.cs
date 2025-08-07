using System;
using UnityEngine;

// Token: 0x020006A6 RID: 1702
public class ArtifactModule : SingleEntityReceptacle, IRenderEveryTick
{
	// Token: 0x06002978 RID: 10616 RVA: 0x000F11F8 File Offset: 0x000EF3F8
	protected override void OnSpawn()
	{
		this.craft = this.module.CraftInterface.GetComponent<Clustercraft>();
		if (this.craft.Status == Clustercraft.CraftStatus.InFlight && base.occupyingObject != null)
		{
			base.occupyingObject.SetActive(false);
		}
		base.OnSpawn();
		base.Subscribe(705820818, new Action<object>(this.OnEnterSpace));
		base.Subscribe(-1165815793, new Action<object>(this.OnExitSpace));
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x000F1279 File Offset: 0x000EF479
	public void RenderEveryTick(float dt)
	{
		this.ArtifactTrackModulePosition();
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x000F1284 File Offset: 0x000EF484
	private void ArtifactTrackModulePosition()
	{
		this.occupyingObjectRelativePosition = this.animController.Offset + Vector3.up * 0.5f + new Vector3(0f, 0f, -1f);
		if (base.occupyingObject != null)
		{
			this.PositionOccupyingObject();
		}
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x000F12E3 File Offset: 0x000EF4E3
	private void OnEnterSpace(object data)
	{
		if (base.occupyingObject != null)
		{
			base.occupyingObject.SetActive(false);
		}
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x000F12FF File Offset: 0x000EF4FF
	private void OnExitSpace(object data)
	{
		if (base.occupyingObject != null)
		{
			base.occupyingObject.SetActive(true);
		}
	}

	// Token: 0x04001894 RID: 6292
	[MyCmpReq]
	private KBatchedAnimController animController;

	// Token: 0x04001895 RID: 6293
	[MyCmpReq]
	private RocketModuleCluster module;

	// Token: 0x04001896 RID: 6294
	private Clustercraft craft;
}
