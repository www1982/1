using System;
using UnityEngine;

// Token: 0x0200084B RID: 2123
public static class CreatureHelpers
{
	// Token: 0x06003A49 RID: 14921 RVA: 0x001444E0 File Offset: 0x001426E0
	public static bool isClear(int cell)
	{
		return Grid.IsValidCell(cell) && !Grid.Solid[cell] && !Grid.IsSubstantialLiquid(cell, 0.9f) && (!Grid.IsValidCell(Grid.CellBelow(cell)) || !Grid.IsLiquid(cell) || !Grid.IsLiquid(Grid.CellBelow(cell)));
	}

	// Token: 0x06003A4A RID: 14922 RVA: 0x00144538 File Offset: 0x00142738
	public static int FindNearbyBreathableCell(int currentLocation, SimHashes breathableElement)
	{
		return currentLocation;
	}

	// Token: 0x06003A4B RID: 14923 RVA: 0x0014453C File Offset: 0x0014273C
	public static bool cellsAreClear(int[] cells)
	{
		for (int i = 0; i < cells.Length; i++)
		{
			if (!Grid.IsValidCell(cells[i]))
			{
				return false;
			}
			if (!CreatureHelpers.isClear(cells[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003A4C RID: 14924 RVA: 0x00144570 File Offset: 0x00142770
	public static Vector3 PositionOfCurrentCell(Vector3 transformPosition)
	{
		return Grid.CellToPos(Grid.PosToCell(transformPosition));
	}

	// Token: 0x06003A4D RID: 14925 RVA: 0x0014457D File Offset: 0x0014277D
	public static Vector3 CenterPositionOfCell(int cell)
	{
		return Grid.CellToPos(cell) + new Vector3(0.5f, 0.5f, -2f);
	}

	// Token: 0x06003A4E RID: 14926 RVA: 0x001445A0 File Offset: 0x001427A0
	public static void DeselectCreature(GameObject creature)
	{
		KSelectable component = creature.GetComponent<KSelectable>();
		if (component != null && SelectTool.Instance.selected == component)
		{
			SelectTool.Instance.Select(null, false);
		}
	}

	// Token: 0x06003A4F RID: 14927 RVA: 0x001445DB File Offset: 0x001427DB
	public static bool isSwimmable(int cell)
	{
		return Grid.IsValidCell(cell) && !Grid.Solid[cell] && Grid.IsSubstantialLiquid(cell, 0.35f);
	}

	// Token: 0x06003A50 RID: 14928 RVA: 0x00144606 File Offset: 0x00142806
	public static bool isSolidGround(int cell)
	{
		return Grid.IsValidCell(cell) && Grid.Solid[cell];
	}

	// Token: 0x06003A51 RID: 14929 RVA: 0x00144622 File Offset: 0x00142822
	public static void FlipAnim(KAnimControllerBase anim, Vector3 heading)
	{
		if (heading.x < 0f)
		{
			anim.FlipX = true;
			return;
		}
		if (heading.x > 0f)
		{
			anim.FlipX = false;
		}
	}

	// Token: 0x06003A52 RID: 14930 RVA: 0x0014464D File Offset: 0x0014284D
	public static void FlipAnim(KBatchedAnimController anim, Vector3 heading)
	{
		if (heading.x < 0f)
		{
			anim.FlipX = true;
			return;
		}
		if (heading.x > 0f)
		{
			anim.FlipX = false;
		}
	}

	// Token: 0x06003A53 RID: 14931 RVA: 0x00144678 File Offset: 0x00142878
	public static Vector3 GetWalkMoveTarget(Transform transform, Vector2 Heading)
	{
		int num = Grid.PosToCell(transform.GetPosition());
		if (Heading.x == 1f)
		{
			if (CreatureHelpers.isClear(Grid.CellRight(num)) && CreatureHelpers.isClear(Grid.CellDownRight(num)) && CreatureHelpers.isClear(Grid.CellRight(Grid.CellRight(num))) && !CreatureHelpers.isClear(Grid.PosToCell(transform.GetPosition() + Vector3.right * 2f + Vector3.down)))
			{
				return transform.GetPosition() + Vector3.right * 2f;
			}
			if (CreatureHelpers.cellsAreClear(new int[]
			{
				Grid.CellRight(num),
				Grid.CellDownRight(num)
			}) && !CreatureHelpers.isClear(Grid.CellBelow(Grid.CellDownRight(num))))
			{
				return transform.GetPosition() + Vector3.right + Vector3.down;
			}
			if (CreatureHelpers.cellsAreClear(new int[]
			{
				Grid.OffsetCell(num, 1, 0),
				Grid.OffsetCell(num, 1, -1),
				Grid.OffsetCell(num, 1, -2)
			}) && !CreatureHelpers.isClear(Grid.OffsetCell(num, 1, -3)))
			{
				return transform.GetPosition() + Vector3.right + Vector3.down + Vector3.down;
			}
			if (CreatureHelpers.cellsAreClear(new int[]
			{
				Grid.OffsetCell(num, 1, 0),
				Grid.OffsetCell(num, 1, -1),
				Grid.OffsetCell(num, 1, -2),
				Grid.OffsetCell(num, 1, -3)
			}))
			{
				return transform.GetPosition();
			}
			if (CreatureHelpers.isClear(Grid.CellRight(num)))
			{
				return transform.GetPosition() + Vector3.right;
			}
			if (CreatureHelpers.isClear(Grid.CellUpRight(num)) && !Grid.Solid[Grid.CellAbove(num)] && Grid.Solid[Grid.CellRight(num)])
			{
				return transform.GetPosition() + Vector3.up + Vector3.right;
			}
			if (!Grid.Solid[Grid.CellAbove(num)] && !Grid.Solid[Grid.CellAbove(Grid.CellAbove(num))] && Grid.Solid[Grid.CellAbove(Grid.CellRight(num))] && CreatureHelpers.isClear(Grid.CellRight(Grid.CellAbove(Grid.CellAbove(num)))))
			{
				return transform.GetPosition() + Vector3.up + Vector3.up + Vector3.right;
			}
		}
		if (Heading.x == -1f)
		{
			if (CreatureHelpers.isClear(Grid.CellLeft(num)) && CreatureHelpers.isClear(Grid.CellDownLeft(num)) && CreatureHelpers.isClear(Grid.CellLeft(Grid.CellLeft(num))) && !CreatureHelpers.isClear(Grid.PosToCell(transform.GetPosition() + Vector3.left * 2f + Vector3.down)))
			{
				return transform.GetPosition() + Vector3.left * 2f;
			}
			if (CreatureHelpers.cellsAreClear(new int[]
			{
				Grid.CellLeft(num),
				Grid.CellDownLeft(num)
			}) && !CreatureHelpers.isClear(Grid.CellBelow(Grid.CellDownLeft(num))))
			{
				return transform.GetPosition() + Vector3.left + Vector3.down;
			}
			if (CreatureHelpers.cellsAreClear(new int[]
			{
				Grid.OffsetCell(num, -1, 0),
				Grid.OffsetCell(num, -1, -1),
				Grid.OffsetCell(num, -1, -2)
			}) && !CreatureHelpers.isClear(Grid.OffsetCell(num, -1, -3)))
			{
				return transform.GetPosition() + Vector3.left + Vector3.down + Vector3.down;
			}
			if (CreatureHelpers.cellsAreClear(new int[]
			{
				Grid.OffsetCell(num, -1, 0),
				Grid.OffsetCell(num, -1, -1),
				Grid.OffsetCell(num, -1, -2),
				Grid.OffsetCell(num, -1, -3)
			}))
			{
				return transform.GetPosition();
			}
			if (CreatureHelpers.isClear(Grid.CellLeft(Grid.PosToCell(transform.GetPosition()))))
			{
				return transform.GetPosition() + Vector3.left;
			}
			if (CreatureHelpers.isClear(Grid.CellUpLeft(num)) && !Grid.Solid[Grid.CellAbove(num)] && Grid.Solid[Grid.CellLeft(num)])
			{
				return transform.GetPosition() + Vector3.up + Vector3.left;
			}
			if (!Grid.Solid[Grid.CellAbove(num)] && !Grid.Solid[Grid.CellAbove(Grid.CellAbove(num))] && Grid.Solid[Grid.CellAbove(Grid.CellLeft(num))] && CreatureHelpers.isClear(Grid.CellLeft(Grid.CellAbove(Grid.CellAbove(num)))))
			{
				return transform.GetPosition() + Vector3.up + Vector3.up + Vector3.left;
			}
		}
		return transform.GetPosition();
	}

	// Token: 0x06003A54 RID: 14932 RVA: 0x00144B60 File Offset: 0x00142D60
	public static bool CrewNearby(Transform transform, int range = 6)
	{
		int num = Grid.PosToCell(transform.gameObject);
		for (int i = 1; i < range; i++)
		{
			int num2 = Grid.OffsetCell(num, i, 0);
			int num3 = Grid.OffsetCell(num, -i, 0);
			if (Grid.Objects[num2, 0] != null)
			{
				return true;
			}
			if (Grid.Objects[num3, 0] != null)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003A55 RID: 14933 RVA: 0x00144BC8 File Offset: 0x00142DC8
	public static bool CheckHorizontalClear(Vector3 startPosition, Vector3 endPosition)
	{
		int num = Grid.PosToCell(startPosition);
		int num2 = 1;
		if (endPosition.x < startPosition.x)
		{
			num2 = -1;
		}
		float num3 = Mathf.Abs(endPosition.x - startPosition.x);
		int num4 = 0;
		while ((float)num4 < num3)
		{
			int num5 = Grid.OffsetCell(num, num4 * num2, 0);
			if (Grid.Solid[num5])
			{
				return false;
			}
			num4++;
		}
		return true;
	}

	// Token: 0x06003A56 RID: 14934 RVA: 0x00144C2C File Offset: 0x00142E2C
	public static GameObject GetFleeTargetLocatorObject(GameObject self, GameObject threat)
	{
		if (threat == null)
		{
			global::Debug.LogWarning(self.name + " is trying to flee, bus has no threats");
			return null;
		}
		CreatureHelpers.fleeThreatInfo fleeThreatInfo;
		fleeThreatInfo.threatCell = Grid.PosToCell(threat);
		fleeThreatInfo.selfCell = Grid.PosToCell(self);
		fleeThreatInfo.nav = self.GetComponent<Navigator>();
		if (fleeThreatInfo.nav == null)
		{
			global::Debug.LogWarning(self.name + " is trying to flee, bus has no navigator component attached.");
			return null;
		}
		int num = GameUtil.FloodFillFindBest<CreatureHelpers.fleeThreatInfo>(CreatureHelpers.fleeCellRater, fleeThreatInfo, CreatureHelpers.fleeCellVaidator, Grid.PosToCell(self), 300);
		if (num != -1)
		{
			return ChoreHelpers.CreateLocator("GoToLocator", Grid.CellToPos(num));
		}
		return null;
	}

	// Token: 0x06003A57 RID: 14935 RVA: 0x00144CD8 File Offset: 0x00142ED8
	private static bool isInFavoredFleeDirection(int targetFleeCell, int threatCell, int selfCell)
	{
		bool flag = Grid.CellToPos(threatCell).x < Grid.CellToPos(selfCell).x;
		bool flag2 = Grid.CellToPos(threatCell).x < Grid.CellToPos(targetFleeCell).x;
		return flag == flag2;
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x00144D21 File Offset: 0x00142F21
	private static bool CanFleeTo(int cell, Navigator nav)
	{
		return nav.GetNavigationCost(cell, OffsetGroups.Use) != -1;
	}

	// Token: 0x040023CB RID: 9163
	private static Func<int, CreatureHelpers.fleeThreatInfo, float> fleeCellRater = (int cell, CreatureHelpers.fleeThreatInfo threat) => (float)Grid.GetCellDistance(cell, threat.threatCell) + (CreatureHelpers.isInFavoredFleeDirection(cell, threat.threatCell, threat.selfCell) ? 2f : 0f);

	// Token: 0x040023CC RID: 9164
	private static Func<int, CreatureHelpers.fleeThreatInfo, bool> fleeCellVaidator = (int cell, CreatureHelpers.fleeThreatInfo info) => CreatureHelpers.CanFleeTo(cell, info.nav);

	// Token: 0x020017C5 RID: 6085
	private struct fleeThreatInfo
	{
		// Token: 0x040076FB RID: 30459
		public int threatCell;

		// Token: 0x040076FC RID: 30460
		public int selfCell;

		// Token: 0x040076FD RID: 30461
		public Navigator nav;
	}
}
