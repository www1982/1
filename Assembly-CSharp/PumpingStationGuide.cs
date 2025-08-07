using System;
using UnityEngine;

// Token: 0x020005FC RID: 1532
[AddComponentMenu("KMonoBehaviour/scripts/PumpingStationGuide")]
public class PumpingStationGuide : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x0600244D RID: 9293 RVA: 0x000CF040 File Offset: 0x000CD240
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.parentController = this.parent.GetComponent<KBatchedAnimController>();
		this.guideController = base.GetComponent<KBatchedAnimController>();
		this.RefreshTint();
		this.RefreshDepthAvailable();
	}

	// Token: 0x0600244E RID: 9294 RVA: 0x000CF071 File Offset: 0x000CD271
	public void RefreshPosition()
	{
		if (this.guideController != null && this.guideController.IsMoving)
		{
			this.guideController.SetDirty();
		}
	}

	// Token: 0x0600244F RID: 9295 RVA: 0x000CF099 File Offset: 0x000CD299
	private void RefreshTint()
	{
		this.guideController.TintColour = this.parentController.TintColour;
	}

	// Token: 0x06002450 RID: 9296 RVA: 0x000CF0B4 File Offset: 0x000CD2B4
	private void RefreshDepthAvailable()
	{
		int depthAvailable = PumpingStationGuide.GetDepthAvailable(Grid.PosToCell(this), this.parent);
		if (depthAvailable != this.previousDepthAvailable)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			if (depthAvailable == 0)
			{
				component.enabled = false;
			}
			else
			{
				component.enabled = true;
				component.Play(new HashedString("place_pipe" + depthAvailable.ToString()), KAnim.PlayMode.Once, 1f, 0f);
			}
			if (this.occupyTiles)
			{
				PumpingStationGuide.OccupyArea(this.parent, depthAvailable);
			}
			this.previousDepthAvailable = depthAvailable;
		}
	}

	// Token: 0x06002451 RID: 9297 RVA: 0x000CF138 File Offset: 0x000CD338
	public void RenderEveryTick(float dt)
	{
		this.RefreshPosition();
		this.RefreshTint();
		this.RefreshDepthAvailable();
	}

	// Token: 0x06002452 RID: 9298 RVA: 0x000CF14C File Offset: 0x000CD34C
	public static void OccupyArea(GameObject go, int depth_available)
	{
		int num = Grid.PosToCell(go.transform.GetPosition());
		for (int i = 1; i <= 4; i++)
		{
			int num2 = Grid.OffsetCell(num, 0, -i);
			int num3 = Grid.OffsetCell(num, 1, -i);
			if (i <= depth_available)
			{
				Grid.ObjectLayers[1][num2] = go;
				Grid.ObjectLayers[1][num3] = go;
			}
			else
			{
				if (Grid.ObjectLayers[1].ContainsKey(num2) && Grid.ObjectLayers[1][num2] == go)
				{
					Grid.ObjectLayers[1][num2] = null;
				}
				if (Grid.ObjectLayers[1].ContainsKey(num3) && Grid.ObjectLayers[1][num3] == go)
				{
					Grid.ObjectLayers[1][num3] = null;
				}
			}
		}
	}

	// Token: 0x06002453 RID: 9299 RVA: 0x000CF21C File Offset: 0x000CD41C
	public static int GetDepthAvailable(int root_cell, GameObject pump)
	{
		int num = 4;
		int num2 = 0;
		for (int i = 1; i <= num; i++)
		{
			int num3 = Grid.OffsetCell(root_cell, 0, -i);
			int num4 = Grid.OffsetCell(root_cell, 1, -i);
			if (!Grid.IsValidCell(num3) || Grid.Solid[num3] || !Grid.IsValidCell(num4) || Grid.Solid[num4] || (Grid.ObjectLayers[1].ContainsKey(num3) && !(Grid.ObjectLayers[1][num3] == null) && !(Grid.ObjectLayers[1][num3] == pump)) || (Grid.ObjectLayers[1].ContainsKey(num4) && !(Grid.ObjectLayers[1][num4] == null) && !(Grid.ObjectLayers[1][num4] == pump)))
			{
				break;
			}
			num2 = i;
		}
		return num2;
	}

	// Token: 0x04001525 RID: 5413
	private int previousDepthAvailable = -1;

	// Token: 0x04001526 RID: 5414
	public GameObject parent;

	// Token: 0x04001527 RID: 5415
	public bool occupyTiles;

	// Token: 0x04001528 RID: 5416
	private KBatchedAnimController parentController;

	// Token: 0x04001529 RID: 5417
	private KBatchedAnimController guideController;
}
