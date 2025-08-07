using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000619 RID: 1561
[AddComponentMenu("KMonoBehaviour/scripts/StationaryChoreRangeVisualizer")]
[Obsolete("Deprecated, use RangeVisualizer")]
public class StationaryChoreRangeVisualizer : KMonoBehaviour
{
	// Token: 0x06002532 RID: 9522 RVA: 0x000D47E8 File Offset: 0x000D29E8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<StationaryChoreRangeVisualizer>(-1503271301, StationaryChoreRangeVisualizer.OnSelectDelegate);
		if (this.movable)
		{
			Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "StationaryChoreRangeVisualizer.OnSpawn");
			base.Subscribe<StationaryChoreRangeVisualizer>(-1643076535, StationaryChoreRangeVisualizer.OnRotatedDelegate);
		}
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x000D4848 File Offset: 0x000D2A48
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
		base.Unsubscribe<StationaryChoreRangeVisualizer>(-1503271301, StationaryChoreRangeVisualizer.OnSelectDelegate, false);
		base.Unsubscribe<StationaryChoreRangeVisualizer>(-1643076535, StationaryChoreRangeVisualizer.OnRotatedDelegate, false);
		this.ClearVisualizers();
		base.OnCleanUp();
	}

	// Token: 0x06002534 RID: 9524 RVA: 0x000D48A0 File Offset: 0x000D2AA0
	private void OnSelect(object data)
	{
		if ((bool)data)
		{
			SoundEvent.PlayOneShot(GlobalAssets.GetSound("RadialGrid_form", false), base.transform.position, 1f);
			this.UpdateVisualizers();
			return;
		}
		SoundEvent.PlayOneShot(GlobalAssets.GetSound("RadialGrid_disappear", false), base.transform.position, 1f);
		this.ClearVisualizers();
	}

	// Token: 0x06002535 RID: 9525 RVA: 0x000D4904 File Offset: 0x000D2B04
	private void OnRotated(object data)
	{
		this.UpdateVisualizers();
	}

	// Token: 0x06002536 RID: 9526 RVA: 0x000D490C File Offset: 0x000D2B0C
	private void OnCellChange()
	{
		this.UpdateVisualizers();
	}

	// Token: 0x06002537 RID: 9527 RVA: 0x000D4914 File Offset: 0x000D2B14
	private void UpdateVisualizers()
	{
		this.newCells.Clear();
		CellOffset rotatedCellOffset = this.vision_offset;
		if (this.rotatable)
		{
			rotatedCellOffset = this.rotatable.GetRotatedCellOffset(this.vision_offset);
		}
		int num = Grid.PosToCell(base.transform.gameObject);
		int num2;
		int num3;
		Grid.CellToXY(Grid.OffsetCell(num, rotatedCellOffset), out num2, out num3);
		for (int i = 0; i < this.height; i++)
		{
			for (int j = 0; j < this.width; j++)
			{
				CellOffset rotatedCellOffset2 = new CellOffset(this.x + j, this.y + i);
				if (this.rotatable)
				{
					rotatedCellOffset2 = this.rotatable.GetRotatedCellOffset(rotatedCellOffset2);
				}
				int num4 = Grid.OffsetCell(num, rotatedCellOffset2);
				if (Grid.IsValidCell(num4))
				{
					int num5;
					int num6;
					Grid.CellToXY(num4, out num5, out num6);
					if (Grid.TestLineOfSight(num2, num3, num5, num6, this.blocking_cb, this.blocking_tile_visible, false))
					{
						this.newCells.Add(num4);
					}
				}
			}
		}
		for (int k = this.visualizers.Count - 1; k >= 0; k--)
		{
			if (this.newCells.Contains(this.visualizers[k].cell))
			{
				this.newCells.Remove(this.visualizers[k].cell);
			}
			else
			{
				this.DestroyEffect(this.visualizers[k].controller);
				this.visualizers.RemoveAt(k);
			}
		}
		for (int l = 0; l < this.newCells.Count; l++)
		{
			KBatchedAnimController kbatchedAnimController = this.CreateEffect(this.newCells[l]);
			this.visualizers.Add(new StationaryChoreRangeVisualizer.VisData
			{
				cell = this.newCells[l],
				controller = kbatchedAnimController
			});
		}
	}

	// Token: 0x06002538 RID: 9528 RVA: 0x000D4B04 File Offset: 0x000D2D04
	private void ClearVisualizers()
	{
		for (int i = 0; i < this.visualizers.Count; i++)
		{
			this.DestroyEffect(this.visualizers[i].controller);
		}
		this.visualizers.Clear();
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x000D4B4C File Offset: 0x000D2D4C
	private KBatchedAnimController CreateEffect(int cell)
	{
		KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect(StationaryChoreRangeVisualizer.AnimName, Grid.CellToPosCCC(cell, this.sceneLayer), null, false, this.sceneLayer, true);
		kbatchedAnimController.destroyOnAnimComplete = false;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.Always;
		kbatchedAnimController.gameObject.SetActive(true);
		kbatchedAnimController.Play(StationaryChoreRangeVisualizer.PreAnims, KAnim.PlayMode.Loop);
		return kbatchedAnimController;
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x000D4B9E File Offset: 0x000D2D9E
	private void DestroyEffect(KBatchedAnimController controller)
	{
		controller.destroyOnAnimComplete = true;
		controller.Play(StationaryChoreRangeVisualizer.PostAnim, KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x040015D5 RID: 5589
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040015D6 RID: 5590
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x040015D7 RID: 5591
	public int x;

	// Token: 0x040015D8 RID: 5592
	public int y;

	// Token: 0x040015D9 RID: 5593
	public int width;

	// Token: 0x040015DA RID: 5594
	public int height;

	// Token: 0x040015DB RID: 5595
	public bool movable;

	// Token: 0x040015DC RID: 5596
	public Grid.SceneLayer sceneLayer = Grid.SceneLayer.FXFront;

	// Token: 0x040015DD RID: 5597
	public CellOffset vision_offset;

	// Token: 0x040015DE RID: 5598
	public Func<int, bool> blocking_cb = new Func<int, bool>(Grid.PhysicalBlockingCB);

	// Token: 0x040015DF RID: 5599
	public bool blocking_tile_visible = true;

	// Token: 0x040015E0 RID: 5600
	private static readonly string AnimName = "transferarmgrid_kanim";

	// Token: 0x040015E1 RID: 5601
	private static readonly HashedString[] PreAnims = new HashedString[] { "grid_pre", "grid_loop" };

	// Token: 0x040015E2 RID: 5602
	private static readonly HashedString PostAnim = "grid_pst";

	// Token: 0x040015E3 RID: 5603
	private List<StationaryChoreRangeVisualizer.VisData> visualizers = new List<StationaryChoreRangeVisualizer.VisData>();

	// Token: 0x040015E4 RID: 5604
	private List<int> newCells = new List<int>();

	// Token: 0x040015E5 RID: 5605
	private static readonly EventSystem.IntraObjectHandler<StationaryChoreRangeVisualizer> OnSelectDelegate = new EventSystem.IntraObjectHandler<StationaryChoreRangeVisualizer>(delegate(StationaryChoreRangeVisualizer component, object data)
	{
		component.OnSelect(data);
	});

	// Token: 0x040015E6 RID: 5606
	private static readonly EventSystem.IntraObjectHandler<StationaryChoreRangeVisualizer> OnRotatedDelegate = new EventSystem.IntraObjectHandler<StationaryChoreRangeVisualizer>(delegate(StationaryChoreRangeVisualizer component, object data)
	{
		component.OnRotated(data);
	});

	// Token: 0x020014AE RID: 5294
	private struct VisData
	{
		// Token: 0x04006D79 RID: 28025
		public int cell;

		// Token: 0x04006D7A RID: 28026
		public KBatchedAnimController controller;
	}
}
