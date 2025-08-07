using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F9 RID: 1529
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Placeable")]
public class Placeable : KMonoBehaviour
{
	// Token: 0x06002431 RID: 9265 RVA: 0x000CE330 File Offset: 0x000CC530
	public bool IsValidPlaceLocation(int cell, out string reason)
	{
		if (this.placementRules.Contains(Placeable.PlacementRules.RestrictToWorld) && (int)Grid.WorldIdx[cell] != this.restrictWorldId)
		{
			reason = UI.TOOLS.PLACE.REASONS.RESTRICT_TO_WORLD;
			return false;
		}
		if (!this.occupyArea.CanOccupyArea(cell, this.occupyArea.objectLayers[0]))
		{
			reason = UI.TOOLS.PLACE.REASONS.CAN_OCCUPY_AREA;
			return false;
		}
		if (this.placementRules.Contains(Placeable.PlacementRules.OnFoundation))
		{
			bool flag = this.occupyArea.TestAreaBelow(cell, null, new Func<int, object, bool>(this.FoundationTest));
			if (this.checkRootCellOnly)
			{
				flag = this.FoundationTest(Grid.CellBelow(cell), null);
			}
			if (!flag)
			{
				reason = UI.TOOLS.PLACE.REASONS.ON_FOUNDATION;
				return false;
			}
		}
		if (this.placementRules.Contains(Placeable.PlacementRules.VisibleToSpace))
		{
			bool flag2 = this.occupyArea.TestArea(cell, null, new Func<int, object, bool>(this.SunnySpaceTest));
			if (this.checkRootCellOnly)
			{
				flag2 = this.SunnySpaceTest(cell, null);
			}
			if (!flag2)
			{
				reason = UI.TOOLS.PLACE.REASONS.VISIBLE_TO_SPACE;
				return false;
			}
		}
		reason = "ok!";
		return true;
	}

	// Token: 0x06002432 RID: 9266 RVA: 0x000CE434 File Offset: 0x000CC634
	private bool SunnySpaceTest(int cell, object data)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = (int)Grid.WorldIdx[cell];
		if (num3 == 255)
		{
			return false;
		}
		WorldContainer world = ClusterManager.Instance.GetWorld(num3);
		int num4 = world.WorldOffset.y + world.WorldSize.y;
		return !Grid.Solid[cell] && !Grid.Foundation[cell] && (Grid.ExposedToSunlight[cell] >= 253 || this.ClearPathToSky(num, num2, num4));
	}

	// Token: 0x06002433 RID: 9267 RVA: 0x000CE4C8 File Offset: 0x000CC6C8
	private bool ClearPathToSky(int x, int startY, int top)
	{
		for (int i = startY; i < top; i++)
		{
			int num = Grid.XYToCell(x, i);
			if (Grid.Solid[num] || Grid.Foundation[num])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06002434 RID: 9268 RVA: 0x000CE506 File Offset: 0x000CC706
	private bool FoundationTest(int cell, object data)
	{
		return Grid.IsValidBuildingCell(cell) && (Grid.Solid[cell] || Grid.Foundation[cell]);
	}

	// Token: 0x0400150B RID: 5387
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x0400150C RID: 5388
	public string kAnimName;

	// Token: 0x0400150D RID: 5389
	public string animName;

	// Token: 0x0400150E RID: 5390
	public List<Placeable.PlacementRules> placementRules = new List<Placeable.PlacementRules>();

	// Token: 0x0400150F RID: 5391
	[NonSerialized]
	public int restrictWorldId;

	// Token: 0x04001510 RID: 5392
	public bool checkRootCellOnly;

	// Token: 0x02001489 RID: 5257
	public enum PlacementRules
	{
		// Token: 0x04006CDE RID: 27870
		OnFoundation,
		// Token: 0x04006CDF RID: 27871
		VisibleToSpace,
		// Token: 0x04006CE0 RID: 27872
		RestrictToWorld
	}
}
