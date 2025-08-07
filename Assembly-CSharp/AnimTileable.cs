using System;
using UnityEngine;

// Token: 0x020006A1 RID: 1697
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/AnimTileable")]
public class AnimTileable : KMonoBehaviour
{
	// Token: 0x06002953 RID: 10579 RVA: 0x000F017A File Offset: 0x000EE37A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.tags == null || this.tags.Length == 0)
		{
			this.tags = new Tag[] { base.GetComponent<KPrefabID>().PrefabTag };
		}
	}

	// Token: 0x06002954 RID: 10580 RVA: 0x000F01B4 File Offset: 0x000EE3B4
	protected override void OnSpawn()
	{
		OccupyArea component = base.GetComponent<OccupyArea>();
		if (component != null)
		{
			this.extents = component.GetExtents();
		}
		else
		{
			Building component2 = base.GetComponent<Building>();
			this.extents = component2.GetExtents();
		}
		Extents extents = new Extents(this.extents.x - 1, this.extents.y - 1, this.extents.width + 2, this.extents.height + 2);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("AnimTileable.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnNeighbourCellsUpdated));
		this.UpdateEndCaps();
	}

	// Token: 0x06002955 RID: 10581 RVA: 0x000F026C File Offset: 0x000EE46C
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002956 RID: 10582 RVA: 0x000F0284 File Offset: 0x000EE484
	private void UpdateEndCaps()
	{
		int num = Grid.PosToCell(this);
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		bool flag4 = true;
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset rotatedCellOffset = new CellOffset(this.extents.x - num2 - 1, 0);
		CellOffset rotatedCellOffset2 = new CellOffset(this.extents.x - num2 + this.extents.width, 0);
		CellOffset rotatedCellOffset3 = new CellOffset(0, this.extents.y - num3 + this.extents.height);
		CellOffset rotatedCellOffset4 = new CellOffset(0, this.extents.y - num3 - 1);
		Rotatable component = base.GetComponent<Rotatable>();
		if (component)
		{
			rotatedCellOffset = component.GetRotatedCellOffset(rotatedCellOffset);
			rotatedCellOffset2 = component.GetRotatedCellOffset(rotatedCellOffset2);
			rotatedCellOffset3 = component.GetRotatedCellOffset(rotatedCellOffset3);
			rotatedCellOffset4 = component.GetRotatedCellOffset(rotatedCellOffset4);
		}
		int num4 = Grid.OffsetCell(num, rotatedCellOffset);
		int num5 = Grid.OffsetCell(num, rotatedCellOffset2);
		int num6 = Grid.OffsetCell(num, rotatedCellOffset3);
		int num7 = Grid.OffsetCell(num, rotatedCellOffset4);
		if (Grid.IsValidCell(num4))
		{
			flag = !this.HasTileableNeighbour(num4);
		}
		if (Grid.IsValidCell(num5))
		{
			flag2 = !this.HasTileableNeighbour(num5);
		}
		if (Grid.IsValidCell(num6))
		{
			flag3 = !this.HasTileableNeighbour(num6);
		}
		if (Grid.IsValidCell(num7))
		{
			flag4 = !this.HasTileableNeighbour(num7);
		}
		foreach (KBatchedAnimController kbatchedAnimController in base.GetComponentsInChildren<KBatchedAnimController>())
		{
			foreach (KAnimHashedString kanimHashedString in AnimTileable.leftSymbols)
			{
				kbatchedAnimController.SetSymbolVisiblity(kanimHashedString, flag);
			}
			foreach (KAnimHashedString kanimHashedString2 in AnimTileable.rightSymbols)
			{
				kbatchedAnimController.SetSymbolVisiblity(kanimHashedString2, flag2);
			}
			foreach (KAnimHashedString kanimHashedString3 in AnimTileable.topSymbols)
			{
				kbatchedAnimController.SetSymbolVisiblity(kanimHashedString3, flag3);
			}
			foreach (KAnimHashedString kanimHashedString4 in AnimTileable.bottomSymbols)
			{
				kbatchedAnimController.SetSymbolVisiblity(kanimHashedString4, flag4);
			}
		}
	}

	// Token: 0x06002957 RID: 10583 RVA: 0x000F04BC File Offset: 0x000EE6BC
	private bool HasTileableNeighbour(int neighbour_cell)
	{
		bool flag = false;
		GameObject gameObject = Grid.Objects[neighbour_cell, (int)this.objectLayer];
		if (gameObject != null)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null && component.HasAnyTags(this.tags))
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x06002958 RID: 10584 RVA: 0x000F0507 File Offset: 0x000EE707
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

	// Token: 0x04001873 RID: 6259
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001874 RID: 6260
	public ObjectLayer objectLayer = ObjectLayer.Building;

	// Token: 0x04001875 RID: 6261
	public Tag[] tags;

	// Token: 0x04001876 RID: 6262
	private Extents extents;

	// Token: 0x04001877 RID: 6263
	private static readonly KAnimHashedString[] leftSymbols = new KAnimHashedString[]
	{
		new KAnimHashedString("cap_left"),
		new KAnimHashedString("cap_left_fg"),
		new KAnimHashedString("cap_left_place")
	};

	// Token: 0x04001878 RID: 6264
	private static readonly KAnimHashedString[] rightSymbols = new KAnimHashedString[]
	{
		new KAnimHashedString("cap_right"),
		new KAnimHashedString("cap_right_fg"),
		new KAnimHashedString("cap_right_place")
	};

	// Token: 0x04001879 RID: 6265
	private static readonly KAnimHashedString[] topSymbols = new KAnimHashedString[]
	{
		new KAnimHashedString("cap_top"),
		new KAnimHashedString("cap_top_fg"),
		new KAnimHashedString("cap_top_place")
	};

	// Token: 0x0400187A RID: 6266
	private static readonly KAnimHashedString[] bottomSymbols = new KAnimHashedString[]
	{
		new KAnimHashedString("cap_bottom"),
		new KAnimHashedString("cap_bottom_fg"),
		new KAnimHashedString("cap_bottom_place")
	};
}
