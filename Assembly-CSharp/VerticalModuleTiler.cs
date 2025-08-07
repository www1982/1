using System;
using UnityEngine;

// Token: 0x02000434 RID: 1076
public class VerticalModuleTiler : KMonoBehaviour
{
	// Token: 0x0600164B RID: 5707 RVA: 0x0007E918 File Offset: 0x0007CB18
	protected override void OnSpawn()
	{
		OccupyArea component = base.GetComponent<OccupyArea>();
		if (component != null)
		{
			this.extents = component.GetExtents();
		}
		KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
		if (this.manageTopCap)
		{
			this.topCapWide = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), VerticalModuleTiler.topCapStr);
		}
		if (this.manageBottomCap)
		{
			this.bottomCapWide = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), VerticalModuleTiler.bottomCapStr);
		}
		this.PostReorderMove();
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x0007E98C File Offset: 0x0007CB8C
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x0007E9A4 File Offset: 0x0007CBA4
	public void PostReorderMove()
	{
		this.dirty = true;
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x0007E9AD File Offset: 0x0007CBAD
	private void OnNeighbourCellsUpdated(object data)
	{
		if (this == null || base.gameObject == null)
		{
			return;
		}
		if (this.partitionerEntry.IsValid())
		{
			this.UpdateEndCaps();
		}
	}

	// Token: 0x0600164F RID: 5711 RVA: 0x0007E9DC File Offset: 0x0007CBDC
	private void UpdateEndCaps()
	{
		int num;
		int num2;
		Grid.CellToXY(Grid.PosToCell(this), out num, out num2);
		int cellTop = this.GetCellTop();
		int cellBottom = this.GetCellBottom();
		if (Grid.IsValidCell(cellTop))
		{
			if (this.HasWideNeighbor(cellTop))
			{
				this.topCapSetting = VerticalModuleTiler.AnimCapType.FiveWide;
			}
			else
			{
				this.topCapSetting = VerticalModuleTiler.AnimCapType.ThreeWide;
			}
		}
		if (Grid.IsValidCell(cellBottom))
		{
			if (this.HasWideNeighbor(cellBottom))
			{
				this.bottomCapSetting = VerticalModuleTiler.AnimCapType.FiveWide;
			}
			else
			{
				this.bottomCapSetting = VerticalModuleTiler.AnimCapType.ThreeWide;
			}
		}
		if (this.manageTopCap)
		{
			this.topCapWide.Enable(this.topCapSetting == VerticalModuleTiler.AnimCapType.FiveWide);
		}
		if (this.manageBottomCap)
		{
			this.bottomCapWide.Enable(this.bottomCapSetting == VerticalModuleTiler.AnimCapType.FiveWide);
		}
	}

	// Token: 0x06001650 RID: 5712 RVA: 0x0007EA80 File Offset: 0x0007CC80
	private int GetCellTop()
	{
		int num = Grid.PosToCell(this);
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset cellOffset = new CellOffset(0, this.extents.y - num3 + this.extents.height);
		return Grid.OffsetCell(num, cellOffset);
	}

	// Token: 0x06001651 RID: 5713 RVA: 0x0007EAC4 File Offset: 0x0007CCC4
	private int GetCellBottom()
	{
		int num = Grid.PosToCell(this);
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset cellOffset = new CellOffset(0, this.extents.y - num3 - 1);
		return Grid.OffsetCell(num, cellOffset);
	}

	// Token: 0x06001652 RID: 5714 RVA: 0x0007EB00 File Offset: 0x0007CD00
	private bool HasWideNeighbor(int neighbour_cell)
	{
		bool flag = false;
		GameObject gameObject = Grid.Objects[neighbour_cell, (int)this.objectLayer];
		if (gameObject != null)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null && component.GetComponent<ReorderableBuilding>() != null && component.GetComponent<Building>().Def.WidthInCells >= 5)
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x06001653 RID: 5715 RVA: 0x0007EB60 File Offset: 0x0007CD60
	private void LateUpdate()
	{
		if (this.animController.Offset != this.m_previousAnimControllerOffset)
		{
			this.m_previousAnimControllerOffset = this.animController.Offset;
			this.bottomCapWide.Dirty();
			this.topCapWide.Dirty();
		}
		if (this.dirty)
		{
			if (this.partitionerEntry != HandleVector<int>.InvalidHandle)
			{
				GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
			}
			OccupyArea component = base.GetComponent<OccupyArea>();
			if (component != null)
			{
				this.extents = component.GetExtents();
			}
			Extents extents = new Extents(this.extents.x, this.extents.y - 1, this.extents.width, this.extents.height + 2);
			this.partitionerEntry = GameScenePartitioner.Instance.Add("VerticalModuleTiler.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnNeighbourCellsUpdated));
			this.UpdateEndCaps();
			this.dirty = false;
		}
	}

	// Token: 0x04000D16 RID: 3350
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04000D17 RID: 3351
	public ObjectLayer objectLayer = ObjectLayer.Building;

	// Token: 0x04000D18 RID: 3352
	private Extents extents;

	// Token: 0x04000D19 RID: 3353
	private VerticalModuleTiler.AnimCapType topCapSetting;

	// Token: 0x04000D1A RID: 3354
	private VerticalModuleTiler.AnimCapType bottomCapSetting;

	// Token: 0x04000D1B RID: 3355
	private bool manageTopCap = true;

	// Token: 0x04000D1C RID: 3356
	private bool manageBottomCap = true;

	// Token: 0x04000D1D RID: 3357
	private KAnimSynchronizedController topCapWide;

	// Token: 0x04000D1E RID: 3358
	private KAnimSynchronizedController bottomCapWide;

	// Token: 0x04000D1F RID: 3359
	private static readonly string topCapStr = "#cap_top_5";

	// Token: 0x04000D20 RID: 3360
	private static readonly string bottomCapStr = "#cap_bottom_5";

	// Token: 0x04000D21 RID: 3361
	private bool dirty;

	// Token: 0x04000D22 RID: 3362
	[MyCmpGet]
	private KAnimControllerBase animController;

	// Token: 0x04000D23 RID: 3363
	private Vector3 m_previousAnimControllerOffset;

	// Token: 0x02001221 RID: 4641
	private enum AnimCapType
	{
		// Token: 0x04006536 RID: 25910
		ThreeWide,
		// Token: 0x04006537 RID: 25911
		FiveWide
	}
}
