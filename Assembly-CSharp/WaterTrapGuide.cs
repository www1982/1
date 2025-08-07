using System;
using UnityEngine;

// Token: 0x02000BE0 RID: 3040
public class WaterTrapGuide : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x06005B1E RID: 23326 RVA: 0x0020ECF1 File Offset: 0x0020CEF1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.parentController = this.parent.GetComponent<KBatchedAnimController>();
		this.guideController = base.GetComponent<KBatchedAnimController>();
		this.RefreshTint();
		this.RefreshDepthAvailable();
	}

	// Token: 0x06005B1F RID: 23327 RVA: 0x0020ED22 File Offset: 0x0020CF22
	private void RefreshTint()
	{
		this.guideController.TintColour = this.parentController.TintColour;
	}

	// Token: 0x06005B20 RID: 23328 RVA: 0x0020ED3A File Offset: 0x0020CF3A
	public void RefreshPosition()
	{
		if (this.guideController != null && this.guideController.IsMoving)
		{
			this.guideController.SetDirty();
		}
	}

	// Token: 0x06005B21 RID: 23329 RVA: 0x0020ED64 File Offset: 0x0020CF64
	private void RefreshDepthAvailable()
	{
		int depthAvailable = WaterTrapGuide.GetDepthAvailable(Grid.PosToCell(this), this.parent);
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
				WaterTrapGuide.OccupyArea(this.parent, depthAvailable);
			}
			this.previousDepthAvailable = depthAvailable;
		}
	}

	// Token: 0x06005B22 RID: 23330 RVA: 0x0020EDE8 File Offset: 0x0020CFE8
	public void RenderEveryTick(float dt)
	{
		this.RefreshPosition();
		this.RefreshTint();
		this.RefreshDepthAvailable();
	}

	// Token: 0x06005B23 RID: 23331 RVA: 0x0020EDFC File Offset: 0x0020CFFC
	public static void OccupyArea(GameObject go, int depth_available)
	{
		int num = Grid.PosToCell(go.transform.GetPosition());
		for (int i = 1; i <= 4; i++)
		{
			int num2 = Grid.OffsetCell(num, 0, -i);
			if (i <= depth_available)
			{
				Grid.ObjectLayers[1][num2] = go;
			}
			else if (Grid.ObjectLayers[1].ContainsKey(num2) && Grid.ObjectLayers[1][num2] == go)
			{
				Grid.ObjectLayers[1][num2] = null;
			}
		}
	}

	// Token: 0x06005B24 RID: 23332 RVA: 0x0020EE7C File Offset: 0x0020D07C
	public static int GetDepthAvailable(int root_cell, GameObject pump)
	{
		int num = 4;
		int num2 = 0;
		for (int i = 1; i <= num; i++)
		{
			int num3 = Grid.OffsetCell(root_cell, 0, -i);
			if (!Grid.IsValidCell(num3) || Grid.Solid[num3] || (Grid.ObjectLayers[1].ContainsKey(num3) && !(Grid.ObjectLayers[1][num3] == null) && !(Grid.ObjectLayers[1][num3] == pump)))
			{
				break;
			}
			num2 = i;
		}
		return num2;
	}

	// Token: 0x04003C82 RID: 15490
	private int previousDepthAvailable = -1;

	// Token: 0x04003C83 RID: 15491
	public GameObject parent;

	// Token: 0x04003C84 RID: 15492
	public bool occupyTiles;

	// Token: 0x04003C85 RID: 15493
	private KBatchedAnimController parentController;

	// Token: 0x04003C86 RID: 15494
	private KBatchedAnimController guideController;
}
