using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProcGen;
using UnityEngine;

// Token: 0x02000945 RID: 2373
public class Grid
{
	// Token: 0x0600437D RID: 17277 RVA: 0x001856DA File Offset: 0x001838DA
	private static void UpdateBuildMask(int i, Grid.BuildFlags flag, bool state)
	{
		if (state)
		{
			Grid.BuildMasks[i] |= flag;
			return;
		}
		Grid.BuildMasks[i] &= ~flag;
	}

	// Token: 0x0600437E RID: 17278 RVA: 0x00185702 File Offset: 0x00183902
	public static void SetSolid(int cell, bool solid, CellSolidEvent ev)
	{
		Grid.UpdateBuildMask(cell, Grid.BuildFlags.Solid, solid);
	}

	// Token: 0x0600437F RID: 17279 RVA: 0x0018570E File Offset: 0x0018390E
	private static void UpdateVisMask(int i, Grid.VisFlags flag, bool state)
	{
		if (state)
		{
			Grid.VisMasks[i] |= flag;
			return;
		}
		Grid.VisMasks[i] &= ~flag;
	}

	// Token: 0x06004380 RID: 17280 RVA: 0x00185736 File Offset: 0x00183936
	private static void UpdateNavValidatorMask(int i, Grid.NavValidatorFlags flag, bool state)
	{
		if (state)
		{
			Grid.NavValidatorMasks[i] |= flag;
			return;
		}
		Grid.NavValidatorMasks[i] &= ~flag;
	}

	// Token: 0x06004381 RID: 17281 RVA: 0x0018575E File Offset: 0x0018395E
	private static void UpdateNavMask(int i, Grid.NavFlags flag, bool state)
	{
		if (state)
		{
			Grid.NavMasks[i] |= flag;
			return;
		}
		Grid.NavMasks[i] &= ~flag;
	}

	// Token: 0x06004382 RID: 17282 RVA: 0x00185786 File Offset: 0x00183986
	public static void ResetNavMasksAndDetails()
	{
		Grid.NavMasks = null;
		Grid.tubeEntrances.Clear();
		Grid.restrictions.Clear();
		Grid.suitMarkers.Clear();
	}

	// Token: 0x06004383 RID: 17283 RVA: 0x001857AC File Offset: 0x001839AC
	public static bool DEBUG_GetRestrictions(int cell, out Grid.Restriction restriction)
	{
		return Grid.restrictions.TryGetValue(cell, out restriction);
	}

	// Token: 0x06004384 RID: 17284 RVA: 0x001857BC File Offset: 0x001839BC
	public static void RegisterRestriction(int cell, Grid.Restriction.Orientation orientation)
	{
		Grid.HasAccessDoor[cell] = true;
		Grid.restrictions[cell] = new Grid.Restriction
		{
			DirectionMasksForMinionInstanceID = new Dictionary<int, Grid.Restriction.Directions>(),
			orientation = orientation
		};
	}

	// Token: 0x06004385 RID: 17285 RVA: 0x001857FD File Offset: 0x001839FD
	public static void UnregisterRestriction(int cell)
	{
		Grid.restrictions.Remove(cell);
		Grid.HasAccessDoor[cell] = false;
	}

	// Token: 0x06004386 RID: 17286 RVA: 0x00185817 File Offset: 0x00183A17
	public static void SetRestriction(int cell, int minionInstanceID, Grid.Restriction.Directions directions)
	{
		Grid.restrictions[cell].DirectionMasksForMinionInstanceID[minionInstanceID] = directions;
	}

	// Token: 0x06004387 RID: 17287 RVA: 0x00185830 File Offset: 0x00183A30
	public static void ClearRestriction(int cell, int minionInstanceID)
	{
		Grid.restrictions[cell].DirectionMasksForMinionInstanceID.Remove(minionInstanceID);
	}

	// Token: 0x06004388 RID: 17288 RVA: 0x0018584C File Offset: 0x00183A4C
	public static bool HasPermission(int cell, int minionInstanceID, int fromCell, NavType fromNavType)
	{
		if (!Grid.HasAccessDoor[cell])
		{
			return true;
		}
		Grid.Restriction restriction = Grid.restrictions[cell];
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = Grid.CellToXY(fromCell);
		Grid.Restriction.Directions directions = (Grid.Restriction.Directions)0;
		int num = vector2I.x - vector2I2.x;
		int num2 = vector2I.y - vector2I2.y;
		switch (restriction.orientation)
		{
		case Grid.Restriction.Orientation.Vertical:
			if (num < 0)
			{
				directions |= Grid.Restriction.Directions.Left;
			}
			if (num > 0)
			{
				directions |= Grid.Restriction.Directions.Right;
			}
			break;
		case Grid.Restriction.Orientation.Horizontal:
			if (num2 > 0)
			{
				directions |= Grid.Restriction.Directions.Left;
			}
			if (num2 < 0)
			{
				directions |= Grid.Restriction.Directions.Right;
			}
			break;
		case Grid.Restriction.Orientation.SingleCell:
			if (Math.Abs(num) != 1 && Math.Abs(num2) != 1 && fromNavType != NavType.Teleport)
			{
				directions |= Grid.Restriction.Directions.Teleport;
			}
			break;
		}
		Grid.Restriction.Directions directions2 = (Grid.Restriction.Directions)0;
		return (!restriction.DirectionMasksForMinionInstanceID.TryGetValue(minionInstanceID, out directions2) && !restriction.DirectionMasksForMinionInstanceID.TryGetValue(-1, out directions2)) || (directions2 & directions) == (Grid.Restriction.Directions)0;
	}

	// Token: 0x06004389 RID: 17289 RVA: 0x0018592C File Offset: 0x00183B2C
	public static void RegisterTubeEntrance(int cell, int reservationCapacity)
	{
		DebugUtil.Assert(!Grid.tubeEntrances.ContainsKey(cell));
		Grid.HasTubeEntrance[cell] = true;
		Grid.tubeEntrances[cell] = new Grid.TubeEntrance
		{
			reservationCapacity = reservationCapacity,
			reservedInstanceIDs = new HashSet<int>()
		};
	}

	// Token: 0x0600438A RID: 17290 RVA: 0x00185980 File Offset: 0x00183B80
	public static void UnregisterTubeEntrance(int cell)
	{
		DebugUtil.Assert(Grid.tubeEntrances.ContainsKey(cell));
		Grid.HasTubeEntrance[cell] = false;
		Grid.tubeEntrances.Remove(cell);
	}

	// Token: 0x0600438B RID: 17291 RVA: 0x001859AC File Offset: 0x00183BAC
	public static bool ReserveTubeEntrance(int cell, int minionInstanceID, bool reserve)
	{
		Grid.TubeEntrance tubeEntrance = Grid.tubeEntrances[cell];
		HashSet<int> reservedInstanceIDs = tubeEntrance.reservedInstanceIDs;
		if (!reserve)
		{
			return reservedInstanceIDs.Remove(minionInstanceID);
		}
		DebugUtil.Assert(Grid.HasTubeEntrance[cell]);
		if (reservedInstanceIDs.Count == tubeEntrance.reservationCapacity)
		{
			return false;
		}
		DebugUtil.Assert(reservedInstanceIDs.Add(minionInstanceID));
		return true;
	}

	// Token: 0x0600438C RID: 17292 RVA: 0x00185A04 File Offset: 0x00183C04
	public static void SetTubeEntranceReservationCapacity(int cell, int newReservationCapacity)
	{
		DebugUtil.Assert(Grid.HasTubeEntrance[cell]);
		Grid.TubeEntrance tubeEntrance = Grid.tubeEntrances[cell];
		tubeEntrance.reservationCapacity = newReservationCapacity;
		Grid.tubeEntrances[cell] = tubeEntrance;
	}

	// Token: 0x0600438D RID: 17293 RVA: 0x00185A44 File Offset: 0x00183C44
	public static bool HasUsableTubeEntrance(int cell, int minionInstanceID)
	{
		if (!Grid.HasTubeEntrance[cell])
		{
			return false;
		}
		Grid.TubeEntrance tubeEntrance = Grid.tubeEntrances[cell];
		if (!tubeEntrance.operational)
		{
			return false;
		}
		HashSet<int> reservedInstanceIDs = tubeEntrance.reservedInstanceIDs;
		return reservedInstanceIDs.Count < tubeEntrance.reservationCapacity || reservedInstanceIDs.Contains(minionInstanceID);
	}

	// Token: 0x0600438E RID: 17294 RVA: 0x00185A94 File Offset: 0x00183C94
	public static bool HasReservedTubeEntrance(int cell, int minionInstanceID)
	{
		DebugUtil.Assert(Grid.HasTubeEntrance[cell]);
		return Grid.tubeEntrances[cell].reservedInstanceIDs.Contains(minionInstanceID);
	}

	// Token: 0x0600438F RID: 17295 RVA: 0x00185ABC File Offset: 0x00183CBC
	public static void SetTubeEntranceOperational(int cell, bool operational)
	{
		DebugUtil.Assert(Grid.HasTubeEntrance[cell]);
		Grid.TubeEntrance tubeEntrance = Grid.tubeEntrances[cell];
		tubeEntrance.operational = operational;
		Grid.tubeEntrances[cell] = tubeEntrance;
	}

	// Token: 0x06004390 RID: 17296 RVA: 0x00185AFC File Offset: 0x00183CFC
	public static void RegisterSuitMarker(int cell)
	{
		DebugUtil.Assert(!Grid.HasSuitMarker[cell]);
		Grid.HasSuitMarker[cell] = true;
		Grid.suitMarkers[cell] = new Grid.SuitMarker
		{
			suitCount = 0,
			lockerCount = 0,
			flags = Grid.SuitMarker.Flags.Operational,
			minionIDsWithSuitReservations = new HashSet<int>(),
			minionIDsWithEmptyLockerReservations = new HashSet<int>()
		};
	}

	// Token: 0x06004391 RID: 17297 RVA: 0x00185B6C File Offset: 0x00183D6C
	public static void UnregisterSuitMarker(int cell)
	{
		DebugUtil.Assert(Grid.HasSuitMarker[cell]);
		Grid.HasSuitMarker[cell] = false;
		Grid.suitMarkers.Remove(cell);
	}

	// Token: 0x06004392 RID: 17298 RVA: 0x00185B98 File Offset: 0x00183D98
	public static bool ReserveSuit(int cell, int minionInstanceID, bool reserve)
	{
		DebugUtil.Assert(Grid.HasSuitMarker[cell]);
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		HashSet<int> minionIDsWithSuitReservations = suitMarker.minionIDsWithSuitReservations;
		if (!reserve)
		{
			bool flag = minionIDsWithSuitReservations.Remove(minionInstanceID);
			DebugUtil.Assert(flag);
			return flag;
		}
		if (minionIDsWithSuitReservations.Count >= suitMarker.suitCount)
		{
			return false;
		}
		DebugUtil.Assert(minionIDsWithSuitReservations.Add(minionInstanceID));
		return true;
	}

	// Token: 0x06004393 RID: 17299 RVA: 0x00185BF8 File Offset: 0x00183DF8
	public static bool ReserveEmptyLocker(int cell, int minionInstanceID, bool reserve)
	{
		DebugUtil.Assert(Grid.HasSuitMarker[cell], "No suit marker");
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		HashSet<int> minionIDsWithEmptyLockerReservations = suitMarker.minionIDsWithEmptyLockerReservations;
		if (!reserve)
		{
			bool flag = minionIDsWithEmptyLockerReservations.Remove(minionInstanceID);
			DebugUtil.Assert(flag, "Reservation not removed");
			return flag;
		}
		if (minionIDsWithEmptyLockerReservations.Count >= suitMarker.emptyLockerCount)
		{
			return false;
		}
		DebugUtil.Assert(minionIDsWithEmptyLockerReservations.Add(minionInstanceID), "Reservation not made");
		return true;
	}

	// Token: 0x06004394 RID: 17300 RVA: 0x00185C68 File Offset: 0x00183E68
	public static void UpdateSuitMarker(int cell, int fullLockerCount, int emptyLockerCount, Grid.SuitMarker.Flags flags, PathFinder.PotentialPath.Flags pathFlags)
	{
		DebugUtil.Assert(Grid.HasSuitMarker[cell]);
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		suitMarker.suitCount = fullLockerCount;
		suitMarker.lockerCount = fullLockerCount + emptyLockerCount;
		suitMarker.flags = flags;
		suitMarker.pathFlags = pathFlags;
		Grid.suitMarkers[cell] = suitMarker;
	}

	// Token: 0x06004395 RID: 17301 RVA: 0x00185CC0 File Offset: 0x00183EC0
	public static bool TryGetSuitMarkerFlags(int cell, out Grid.SuitMarker.Flags flags, out PathFinder.PotentialPath.Flags pathFlags)
	{
		if (Grid.HasSuitMarker[cell])
		{
			flags = Grid.suitMarkers[cell].flags;
			pathFlags = Grid.suitMarkers[cell].pathFlags;
			return true;
		}
		flags = (Grid.SuitMarker.Flags)0;
		pathFlags = PathFinder.PotentialPath.Flags.None;
		return false;
	}

	// Token: 0x06004396 RID: 17302 RVA: 0x00185CFC File Offset: 0x00183EFC
	public static bool HasSuit(int cell, int minionInstanceID)
	{
		if (!Grid.HasSuitMarker[cell])
		{
			return false;
		}
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		HashSet<int> minionIDsWithSuitReservations = suitMarker.minionIDsWithSuitReservations;
		return minionIDsWithSuitReservations.Count < suitMarker.suitCount || minionIDsWithSuitReservations.Contains(minionInstanceID);
	}

	// Token: 0x06004397 RID: 17303 RVA: 0x00185D44 File Offset: 0x00183F44
	public static bool HasEmptyLocker(int cell, int minionInstanceID)
	{
		if (!Grid.HasSuitMarker[cell])
		{
			return false;
		}
		Grid.SuitMarker suitMarker = Grid.suitMarkers[cell];
		HashSet<int> minionIDsWithEmptyLockerReservations = suitMarker.minionIDsWithEmptyLockerReservations;
		return minionIDsWithEmptyLockerReservations.Count < suitMarker.emptyLockerCount || minionIDsWithEmptyLockerReservations.Contains(minionInstanceID);
	}

	// Token: 0x06004398 RID: 17304 RVA: 0x00185D8C File Offset: 0x00183F8C
	public unsafe static void InitializeCells()
	{
		for (int num = 0; num != Grid.WidthInCells * Grid.HeightInCells; num++)
		{
			ushort num2 = Grid.elementIdx[num];
			Element element = ElementLoader.elements[(int)num2];
			Grid.Element[num] = element;
			if (element.IsSolid)
			{
				Grid.BuildMasks[num] |= Grid.BuildFlags.Solid;
			}
			else
			{
				Grid.BuildMasks[num] &= ~Grid.BuildFlags.Solid;
			}
			Grid.RenderedByWorld[num] = element.substance != null && element.substance.renderedByWorld && Grid.Objects[num, 9] == null;
		}
	}

	// Token: 0x06004399 RID: 17305 RVA: 0x00185E39 File Offset: 0x00184039
	public static bool IsInitialized()
	{
		return Grid.mass != null;
	}

	// Token: 0x0600439A RID: 17306 RVA: 0x00185E48 File Offset: 0x00184048
	public static int GetCellInDirection(int cell, Direction d)
	{
		switch (d)
		{
		case Direction.Up:
			return Grid.CellAbove(cell);
		case Direction.Right:
			return Grid.CellRight(cell);
		case Direction.Down:
			return Grid.CellBelow(cell);
		case Direction.Left:
			return Grid.CellLeft(cell);
		case Direction.None:
			return cell;
		}
		return -1;
	}

	// Token: 0x0600439B RID: 17307 RVA: 0x00185E94 File Offset: 0x00184094
	public static bool Raycast(int cell, Vector2I direction, out int hitDistance, int maxDistance = 100, Grid.BuildFlags layerMask = Grid.BuildFlags.Any)
	{
		bool flag = false;
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = vector2I + direction * maxDistance;
		int num = cell;
		int num2 = Grid.XYToCell(vector2I2.x, vector2I2.y);
		int num3 = 0;
		int num4 = 0;
		float num5 = (float)maxDistance * 0.5f;
		while ((float)num3 < num5)
		{
			if (!Grid.IsValidCell(num) || (Grid.BuildMasks[num] & layerMask) != ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
			{
				flag = true;
				break;
			}
			if (!Grid.IsValidCell(num2) || (Grid.BuildMasks[num2] & layerMask) != ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
			{
				num4 = maxDistance - num3;
			}
			vector2I += direction;
			vector2I2 -= direction;
			num = Grid.XYToCell(vector2I.x, vector2I.y);
			num2 = Grid.XYToCell(vector2I2.x, vector2I2.y);
			num3++;
		}
		if (!flag && maxDistance % 2 == 0)
		{
			flag = !Grid.IsValidCell(num2) || (Grid.BuildMasks[num2] & layerMask) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
		}
		hitDistance = (flag ? num3 : ((num4 > 0) ? num4 : maxDistance));
		return flag | (hitDistance == num4);
	}

	// Token: 0x0600439C RID: 17308 RVA: 0x00185F95 File Offset: 0x00184195
	public static int CellAbove(int cell)
	{
		return cell + Grid.WidthInCells;
	}

	// Token: 0x0600439D RID: 17309 RVA: 0x00185F9E File Offset: 0x0018419E
	public static int CellBelow(int cell)
	{
		return cell - Grid.WidthInCells;
	}

	// Token: 0x0600439E RID: 17310 RVA: 0x00185FA7 File Offset: 0x001841A7
	public static int CellLeft(int cell)
	{
		if (cell % Grid.WidthInCells <= 0)
		{
			return Grid.InvalidCell;
		}
		return cell - 1;
	}

	// Token: 0x0600439F RID: 17311 RVA: 0x00185FBC File Offset: 0x001841BC
	public static int CellRight(int cell)
	{
		if (cell % Grid.WidthInCells >= Grid.WidthInCells - 1)
		{
			return Grid.InvalidCell;
		}
		return cell + 1;
	}

	// Token: 0x060043A0 RID: 17312 RVA: 0x00185FD8 File Offset: 0x001841D8
	public static CellOffset GetOffset(int cell)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		return new CellOffset(num, num2);
	}

	// Token: 0x060043A1 RID: 17313 RVA: 0x00185FFC File Offset: 0x001841FC
	public static int CellUpLeft(int cell)
	{
		int num = Grid.InvalidCell;
		if (cell < (Grid.HeightInCells - 1) * Grid.WidthInCells && cell % Grid.WidthInCells > 0)
		{
			num = cell - 1 + Grid.WidthInCells;
		}
		return num;
	}

	// Token: 0x060043A2 RID: 17314 RVA: 0x00186034 File Offset: 0x00184234
	public static int CellUpRight(int cell)
	{
		int num = Grid.InvalidCell;
		if (cell < (Grid.HeightInCells - 1) * Grid.WidthInCells && cell % Grid.WidthInCells < Grid.WidthInCells - 1)
		{
			num = cell + 1 + Grid.WidthInCells;
		}
		return num;
	}

	// Token: 0x060043A3 RID: 17315 RVA: 0x00186074 File Offset: 0x00184274
	public static int CellDownLeft(int cell)
	{
		int num = Grid.InvalidCell;
		if (cell > Grid.WidthInCells && cell % Grid.WidthInCells > 0)
		{
			num = cell - 1 - Grid.WidthInCells;
		}
		return num;
	}

	// Token: 0x060043A4 RID: 17316 RVA: 0x001860A4 File Offset: 0x001842A4
	public static int CellDownRight(int cell)
	{
		int num = Grid.InvalidCell;
		if (cell >= Grid.WidthInCells && cell % Grid.WidthInCells < Grid.WidthInCells - 1)
		{
			num = cell + 1 - Grid.WidthInCells;
		}
		return num;
	}

	// Token: 0x060043A5 RID: 17317 RVA: 0x001860DA File Offset: 0x001842DA
	public static bool IsCellLeftOf(int cell, int other_cell)
	{
		return Grid.CellColumn(cell) < Grid.CellColumn(other_cell);
	}

	// Token: 0x060043A6 RID: 17318 RVA: 0x001860EC File Offset: 0x001842EC
	public static bool IsCellOffsetOf(int cell, int target_cell, CellOffset[] target_offsets)
	{
		int num = target_offsets.Length;
		for (int i = 0; i < num; i++)
		{
			if (cell == Grid.OffsetCell(target_cell, target_offsets[i]))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060043A7 RID: 17319 RVA: 0x0018611C File Offset: 0x0018431C
	public static int GetCellDistance(int cell_a, int cell_b)
	{
		int num;
		int num2;
		Grid.CellToXY(cell_a, out num, out num2);
		int num3;
		int num4;
		Grid.CellToXY(cell_b, out num3, out num4);
		return Math.Abs(num - num3) + Math.Abs(num2 - num4);
	}

	// Token: 0x060043A8 RID: 17320 RVA: 0x00186150 File Offset: 0x00184350
	public static int GetCellRange(int cell_a, int cell_b)
	{
		int num;
		int num2;
		Grid.CellToXY(cell_a, out num, out num2);
		int num3;
		int num4;
		Grid.CellToXY(cell_b, out num3, out num4);
		return Math.Max(Math.Abs(num - num3), Math.Abs(num2 - num4));
	}

	// Token: 0x060043A9 RID: 17321 RVA: 0x00186188 File Offset: 0x00184388
	public static CellOffset GetOffset(int base_cell, int offset_cell)
	{
		int num;
		int num2;
		Grid.CellToXY(base_cell, out num, out num2);
		int num3;
		int num4;
		Grid.CellToXY(offset_cell, out num3, out num4);
		return new CellOffset(num3 - num, num4 - num2);
	}

	// Token: 0x060043AA RID: 17322 RVA: 0x001861B4 File Offset: 0x001843B4
	public static CellOffset GetCellOffsetDirection(int base_cell, int offset_cell)
	{
		CellOffset offset = Grid.GetOffset(base_cell, offset_cell);
		offset.x = Mathf.Clamp(offset.x, -1, 1);
		offset.y = Mathf.Clamp(offset.y, -1, 1);
		return offset;
	}

	// Token: 0x060043AB RID: 17323 RVA: 0x001861F2 File Offset: 0x001843F2
	public static int OffsetCell(int cell, CellOffset offset)
	{
		return cell + offset.x + offset.y * Grid.WidthInCells;
	}

	// Token: 0x060043AC RID: 17324 RVA: 0x00186209 File Offset: 0x00184409
	public static int OffsetCell(int cell, int x, int y)
	{
		return cell + x + y * Grid.WidthInCells;
	}

	// Token: 0x060043AD RID: 17325 RVA: 0x00186218 File Offset: 0x00184418
	public static bool IsCellOffsetValid(int cell, int x, int y)
	{
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		return num + x >= 0 && num + x < Grid.WidthInCells && num2 + y >= 0 && num2 + y < Grid.HeightInCells;
	}

	// Token: 0x060043AE RID: 17326 RVA: 0x00186253 File Offset: 0x00184453
	public static bool IsCellOffsetValid(int cell, CellOffset offset)
	{
		return Grid.IsCellOffsetValid(cell, offset.x, offset.y);
	}

	// Token: 0x060043AF RID: 17327 RVA: 0x00186267 File Offset: 0x00184467
	public static int PosToCell(StateMachine.Instance smi)
	{
		return Grid.PosToCell(smi.transform.GetPosition());
	}

	// Token: 0x060043B0 RID: 17328 RVA: 0x00186279 File Offset: 0x00184479
	public static int PosToCell(GameObject go)
	{
		return Grid.PosToCell(go.transform.GetPosition());
	}

	// Token: 0x060043B1 RID: 17329 RVA: 0x0018628B File Offset: 0x0018448B
	public static int PosToCell(KMonoBehaviour cmp)
	{
		return Grid.PosToCell(cmp.transform.GetPosition());
	}

	// Token: 0x060043B2 RID: 17330 RVA: 0x001862A0 File Offset: 0x001844A0
	public static bool IsValidBuildingCell(int cell)
	{
		if (!Grid.IsWorldValidCell(cell))
		{
			return false;
		}
		WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[cell]);
		if (world == null)
		{
			return false;
		}
		Vector2I vector2I = Grid.CellToXY(cell);
		return (float)vector2I.x >= world.minimumBounds.x && (float)vector2I.x <= world.maximumBounds.x && (float)vector2I.y >= world.minimumBounds.y && (float)vector2I.y <= world.maximumBounds.y - (float)Grid.TopBorderHeight;
	}

	// Token: 0x060043B3 RID: 17331 RVA: 0x00186337 File Offset: 0x00184537
	public static bool IsWorldValidCell(int cell)
	{
		return Grid.IsValidCell(cell) && Grid.WorldIdx[cell] != byte.MaxValue;
	}

	// Token: 0x060043B4 RID: 17332 RVA: 0x00186354 File Offset: 0x00184554
	public static bool IsValidCell(int cell)
	{
		return cell >= 0 && cell < Grid.CellCount;
	}

	// Token: 0x060043B5 RID: 17333 RVA: 0x00186364 File Offset: 0x00184564
	public static bool IsValidCellInWorld(int cell, int world)
	{
		return cell >= 0 && cell < Grid.CellCount && (int)Grid.WorldIdx[cell] == world;
	}

	// Token: 0x060043B6 RID: 17334 RVA: 0x0018637E File Offset: 0x0018457E
	public static bool IsActiveWorld(int cell)
	{
		return ClusterManager.Instance != null && ClusterManager.Instance.activeWorldId == (int)Grid.WorldIdx[cell];
	}

	// Token: 0x060043B7 RID: 17335 RVA: 0x001863A2 File Offset: 0x001845A2
	public static bool AreCellsInSameWorld(int cell, int world_cell)
	{
		return Grid.IsValidCell(cell) && Grid.IsValidCell(world_cell) && Grid.WorldIdx[cell] == Grid.WorldIdx[world_cell];
	}

	// Token: 0x060043B8 RID: 17336 RVA: 0x001863C6 File Offset: 0x001845C6
	public static bool IsCellOpenToSpace(int cell)
	{
		return !Grid.IsSolidCell(cell) && !(Grid.Objects[cell, 2] != null) && Grid.IsCellBiomeSpaceBiome(cell);
	}

	// Token: 0x060043B9 RID: 17337 RVA: 0x001863EE File Offset: 0x001845EE
	public static bool IsCellBiomeSpaceBiome(int cell)
	{
		return global::World.Instance.zoneRenderData.GetSubWorldZoneType(cell) == SubWorld.ZoneType.Space;
	}

	// Token: 0x060043BA RID: 17338 RVA: 0x00186404 File Offset: 0x00184604
	public static int PosToCell(Vector2 pos)
	{
		float x = pos.x;
		int num = (int)(pos.y + 0.05f);
		int num2 = (int)x;
		return num * Grid.WidthInCells + num2;
	}

	// Token: 0x060043BB RID: 17339 RVA: 0x00186430 File Offset: 0x00184630
	public static int PosToCell(Vector3 pos)
	{
		float x = pos.x;
		int num = (int)(pos.y + 0.05f);
		int num2 = (int)x;
		return num * Grid.WidthInCells + num2;
	}

	// Token: 0x060043BC RID: 17340 RVA: 0x0018645C File Offset: 0x0018465C
	public static void PosToXY(Vector3 pos, out int x, out int y)
	{
		Grid.CellToXY(Grid.PosToCell(pos), out x, out y);
	}

	// Token: 0x060043BD RID: 17341 RVA: 0x0018646B File Offset: 0x0018466B
	public static void PosToXY(Vector3 pos, out Vector2I xy)
	{
		Grid.CellToXY(Grid.PosToCell(pos), out xy.x, out xy.y);
	}

	// Token: 0x060043BE RID: 17342 RVA: 0x00186484 File Offset: 0x00184684
	public static Vector2I PosToXY(Vector3 pos)
	{
		Vector2I vector2I;
		Grid.CellToXY(Grid.PosToCell(pos), out vector2I.x, out vector2I.y);
		return vector2I;
	}

	// Token: 0x060043BF RID: 17343 RVA: 0x001864AB File Offset: 0x001846AB
	public static int XYToCell(int x, int y)
	{
		return x + y * Grid.WidthInCells;
	}

	// Token: 0x060043C0 RID: 17344 RVA: 0x001864B6 File Offset: 0x001846B6
	public static void CellToXY(int cell, out int x, out int y)
	{
		x = Grid.CellColumn(cell);
		y = Grid.CellRow(cell);
	}

	// Token: 0x060043C1 RID: 17345 RVA: 0x001864C8 File Offset: 0x001846C8
	public static Vector2I CellToXY(int cell)
	{
		return new Vector2I(Grid.CellColumn(cell), Grid.CellRow(cell));
	}

	// Token: 0x060043C2 RID: 17346 RVA: 0x001864DC File Offset: 0x001846DC
	public static Vector3 CellToPos(int cell, float x_offset, float y_offset, float z_offset)
	{
		int widthInCells = Grid.WidthInCells;
		float num = Grid.CellSizeInMeters * (float)(cell % widthInCells);
		float num2 = Grid.CellSizeInMeters * (float)(cell / widthInCells);
		return new Vector3(num + x_offset, num2 + y_offset, z_offset);
	}

	// Token: 0x060043C3 RID: 17347 RVA: 0x00186510 File Offset: 0x00184710
	public static Vector3 CellToPos(int cell)
	{
		int widthInCells = Grid.WidthInCells;
		float num = Grid.CellSizeInMeters * (float)(cell % widthInCells);
		float num2 = Grid.CellSizeInMeters * (float)(cell / widthInCells);
		return new Vector3(num, num2, 0f);
	}

	// Token: 0x060043C4 RID: 17348 RVA: 0x00186544 File Offset: 0x00184744
	public static Vector3 CellToPos2D(int cell)
	{
		int widthInCells = Grid.WidthInCells;
		float num = Grid.CellSizeInMeters * (float)(cell % widthInCells);
		float num2 = Grid.CellSizeInMeters * (float)(cell / widthInCells);
		return new Vector2(num, num2);
	}

	// Token: 0x060043C5 RID: 17349 RVA: 0x00186577 File Offset: 0x00184777
	public static int CellRow(int cell)
	{
		return cell / Grid.WidthInCells;
	}

	// Token: 0x060043C6 RID: 17350 RVA: 0x00186580 File Offset: 0x00184780
	public static int CellColumn(int cell)
	{
		return cell % Grid.WidthInCells;
	}

	// Token: 0x060043C7 RID: 17351 RVA: 0x00186589 File Offset: 0x00184789
	public static int ClampX(int x)
	{
		return Math.Min(Math.Max(x, 0), Grid.WidthInCells - 1);
	}

	// Token: 0x060043C8 RID: 17352 RVA: 0x0018659E File Offset: 0x0018479E
	public static int ClampY(int y)
	{
		return Math.Min(Math.Max(y, 0), Grid.HeightInCells - 1);
	}

	// Token: 0x060043C9 RID: 17353 RVA: 0x001865B4 File Offset: 0x001847B4
	public static Vector2I Constrain(Vector2I val)
	{
		val.x = Mathf.Max(0, Mathf.Min(val.x, Grid.WidthInCells - 1));
		val.y = Mathf.Max(0, Mathf.Min(val.y, Grid.HeightInCells - 1));
		return val;
	}

	// Token: 0x060043CA RID: 17354 RVA: 0x00186600 File Offset: 0x00184800
	public static void Reveal(int cell, byte visibility = 255, bool forceReveal = false)
	{
		bool flag = Grid.Spawnable[cell] == 0 && visibility > 0;
		Grid.Spawnable[cell] = Math.Max(visibility, Grid.Visible[cell]);
		if (forceReveal || !Grid.PreventFogOfWarReveal[cell])
		{
			Grid.Visible[cell] = Math.Max(visibility, Grid.Visible[cell]);
		}
		if (flag && Grid.OnReveal != null)
		{
			Grid.OnReveal(cell);
		}
	}

	// Token: 0x060043CB RID: 17355 RVA: 0x00186669 File Offset: 0x00184869
	public static ObjectLayer GetObjectLayerForConduitType(ConduitType conduit_type)
	{
		switch (conduit_type)
		{
		case ConduitType.Gas:
			return ObjectLayer.GasConduitConnection;
		case ConduitType.Liquid:
			return ObjectLayer.LiquidConduitConnection;
		case ConduitType.Solid:
			return ObjectLayer.SolidConduitConnection;
		default:
			throw new ArgumentException("Invalid value.", "conduit_type");
		}
	}

	// Token: 0x060043CC RID: 17356 RVA: 0x0018669C File Offset: 0x0018489C
	public static Vector3 CellToPos(int cell, CellAlignment alignment, Grid.SceneLayer layer)
	{
		switch (alignment)
		{
		case CellAlignment.Bottom:
			return Grid.CellToPosCBC(cell, layer);
		case CellAlignment.Top:
			return Grid.CellToPosCTC(cell, layer);
		case CellAlignment.Left:
			return Grid.CellToPosLCC(cell, layer);
		case CellAlignment.Right:
			return Grid.CellToPosRCC(cell, layer);
		case CellAlignment.RandomInternal:
		{
			Vector3 vector = new Vector3(global::UnityEngine.Random.Range(-0.3f, 0.3f), 0f, 0f);
			return Grid.CellToPosCCC(cell, layer) + vector;
		}
		}
		return Grid.CellToPosCCC(cell, layer);
	}

	// Token: 0x060043CD RID: 17357 RVA: 0x0018671E File Offset: 0x0018491E
	public static float GetLayerZ(Grid.SceneLayer layer)
	{
		return -Grid.HalfCellSizeInMeters - Grid.CellSizeInMeters * (float)layer * Grid.LayerMultiplier;
	}

	// Token: 0x060043CE RID: 17358 RVA: 0x00186735 File Offset: 0x00184935
	public static Vector3 CellToPosCCC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.HalfCellSizeInMeters, Grid.HalfCellSizeInMeters, Grid.GetLayerZ(layer));
	}

	// Token: 0x060043CF RID: 17359 RVA: 0x0018674D File Offset: 0x0018494D
	public static Vector3 CellToPosCBC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.HalfCellSizeInMeters, 0.01f, Grid.GetLayerZ(layer));
	}

	// Token: 0x060043D0 RID: 17360 RVA: 0x00186765 File Offset: 0x00184965
	public static Vector3 CellToPosCCF(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.HalfCellSizeInMeters, Grid.HalfCellSizeInMeters, -Grid.CellSizeInMeters * (float)layer * Grid.LayerMultiplier);
	}

	// Token: 0x060043D1 RID: 17361 RVA: 0x00186786 File Offset: 0x00184986
	public static Vector3 CellToPosLCC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, 0.01f, Grid.HalfCellSizeInMeters, Grid.GetLayerZ(layer));
	}

	// Token: 0x060043D2 RID: 17362 RVA: 0x0018679E File Offset: 0x0018499E
	public static Vector3 CellToPosRCC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.CellSizeInMeters - 0.01f, Grid.HalfCellSizeInMeters, Grid.GetLayerZ(layer));
	}

	// Token: 0x060043D3 RID: 17363 RVA: 0x001867BC File Offset: 0x001849BC
	public static Vector3 CellToPosRBC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.CellSizeInMeters - 0.01f, 0.01f, Grid.GetLayerZ(layer));
	}

	// Token: 0x060043D4 RID: 17364 RVA: 0x001867DA File Offset: 0x001849DA
	public static Vector3 CellToPosLBC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, 0.01f, 0.01f, Grid.GetLayerZ(layer));
	}

	// Token: 0x060043D5 RID: 17365 RVA: 0x001867F2 File Offset: 0x001849F2
	public static Vector3 CellToPosCTC(int cell, Grid.SceneLayer layer)
	{
		return Grid.CellToPos(cell, Grid.HalfCellSizeInMeters, Grid.CellSizeInMeters - 0.01f, Grid.GetLayerZ(layer));
	}

	// Token: 0x060043D6 RID: 17366 RVA: 0x00186810 File Offset: 0x00184A10
	public static bool IsSolidCell(int cell)
	{
		return Grid.IsValidCell(cell) && Grid.Solid[cell];
	}

	// Token: 0x060043D7 RID: 17367 RVA: 0x00186828 File Offset: 0x00184A28
	public unsafe static bool IsSubstantialLiquid(int cell, float threshold = 0.35f)
	{
		if (Grid.IsValidCell(cell))
		{
			ushort num = Grid.elementIdx[cell];
			if ((int)num < ElementLoader.elements.Count)
			{
				Element element = ElementLoader.elements[(int)num];
				if (element.IsLiquid && Grid.mass[cell] >= element.defaultValues.mass * threshold)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060043D8 RID: 17368 RVA: 0x00186888 File Offset: 0x00184A88
	public static bool IsVisiblyInLiquid(Vector2 pos)
	{
		int num = Grid.PosToCell(pos);
		if (Grid.IsValidCell(num) && Grid.IsLiquid(num))
		{
			int num2 = Grid.CellAbove(num);
			if (Grid.IsValidCell(num2) && Grid.IsLiquid(num2))
			{
				return true;
			}
			float num3 = Grid.Mass[num];
			float num4 = (float)((int)pos.y) - pos.y;
			if (num3 / 1000f <= num4)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060043D9 RID: 17369 RVA: 0x001868EC File Offset: 0x00184AEC
	public static bool IsNavigatableLiquid(int cell)
	{
		int num = Grid.CellAbove(cell);
		if (!Grid.IsValidCell(cell) || !Grid.IsValidCell(num))
		{
			return false;
		}
		if (Grid.IsSubstantialLiquid(cell, 0.35f))
		{
			return true;
		}
		if (Grid.IsLiquid(cell))
		{
			if (Grid.Element[num].IsLiquid)
			{
				return true;
			}
			if (Grid.Element[num].IsSolid)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060043DA RID: 17370 RVA: 0x0018694A File Offset: 0x00184B4A
	public static bool IsLiquid(int cell)
	{
		return ElementLoader.elements[(int)Grid.ElementIdx[cell]].IsLiquid;
	}

	// Token: 0x060043DB RID: 17371 RVA: 0x0018696B File Offset: 0x00184B6B
	public static bool IsGas(int cell)
	{
		return ElementLoader.elements[(int)Grid.ElementIdx[cell]].IsGas;
	}

	// Token: 0x060043DC RID: 17372 RVA: 0x0018698C File Offset: 0x00184B8C
	public static void GetVisibleExtents(out int min_x, out int min_y, out int max_x, out int max_y)
	{
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		min_y = (int)vector2.y;
		max_y = (int)(vector.y + 0.5f);
		min_x = (int)vector2.x;
		max_x = (int)(vector.x + 0.5f);
	}

	// Token: 0x060043DD RID: 17373 RVA: 0x00186A25 File Offset: 0x00184C25
	public static void GetVisibleExtents(out Vector2I min, out Vector2I max)
	{
		Grid.GetVisibleExtents(out min.x, out min.y, out max.x, out max.y);
	}

	// Token: 0x060043DE RID: 17374 RVA: 0x00186A44 File Offset: 0x00184C44
	public static void GetVisibleCellRangeInActiveWorld(out Vector2I min, out Vector2I max, int padding = 4, float rangeScale = 1.5f)
	{
		Grid.GetVisibleExtents(out min.x, out min.y, out max.x, out max.y);
		min.x -= padding;
		min.y -= padding;
		if (CameraController.Instance != null && DlcManager.IsExpansion1Active())
		{
			Vector2I vector2I;
			Vector2I vector2I2;
			CameraController.Instance.GetWorldCamera(out vector2I, out vector2I2);
			min.x = Math.Min(vector2I.x + vector2I2.x - 1, Math.Max(vector2I.x, min.x));
			min.y = Math.Min(vector2I.y + vector2I2.y - 1, Math.Max(vector2I.y, min.y));
			max.x += padding;
			max.y += padding;
			max.x = Math.Min(vector2I.x + vector2I2.x - 1, Math.Max(vector2I.x, max.x));
			max.y = Math.Min(vector2I.y + vector2I2.y - 1 + 20, Math.Max(vector2I.y, max.y));
			return;
		}
		min.x = Math.Min((int)((float)Grid.WidthInCells * rangeScale) - 1, Math.Max(0, min.x));
		min.y = Math.Min((int)((float)Grid.HeightInCells * rangeScale) - 1, Math.Max(0, min.y));
		max.x += padding;
		max.y += padding;
		max.x = Math.Min((int)((float)Grid.WidthInCells * rangeScale) - 1, Math.Max(0, max.x));
		max.y = Math.Min((int)((float)Grid.HeightInCells * rangeScale) - 1, Math.Max(0, max.y));
	}

	// Token: 0x060043DF RID: 17375 RVA: 0x00186C10 File Offset: 0x00184E10
	public static Extents GetVisibleExtentsInActiveWorld(int padding = 4, float rangeScale = 1.5f)
	{
		Vector2I vector2I;
		Vector2I vector2I2;
		Grid.GetVisibleCellRangeInActiveWorld(out vector2I, out vector2I2, 4, 1.5f);
		return new Extents(vector2I.x, vector2I.y, vector2I2.x - vector2I.x, vector2I2.y - vector2I.y);
	}

	// Token: 0x060043E0 RID: 17376 RVA: 0x00186C57 File Offset: 0x00184E57
	public static bool IsVisible(int cell)
	{
		return Grid.Visible[cell] > 0 || !PropertyTextures.IsFogOfWarEnabled;
	}

	// Token: 0x060043E1 RID: 17377 RVA: 0x00186C6D File Offset: 0x00184E6D
	public static bool VisibleBlockingCB(int cell)
	{
		return !Grid.Transparent[cell] && Grid.IsSolidCell(cell);
	}

	// Token: 0x060043E2 RID: 17378 RVA: 0x00186C84 File Offset: 0x00184E84
	public static bool VisibilityTest(int x, int y, int x2, int y2, bool blocking_tile_visible = false)
	{
		return Grid.TestLineOfSight(x, y, x2, y2, Grid.VisibleBlockingDelegate, blocking_tile_visible, false);
	}

	// Token: 0x060043E3 RID: 17379 RVA: 0x00186C98 File Offset: 0x00184E98
	public static bool VisibilityTest(int cell, int target_cell, bool blocking_tile_visible = false)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = 0;
		int num4 = 0;
		Grid.CellToXY(target_cell, out num3, out num4);
		return Grid.VisibilityTest(num, num2, num3, num4, blocking_tile_visible);
	}

	// Token: 0x060043E4 RID: 17380 RVA: 0x00186CCB File Offset: 0x00184ECB
	public static bool PhysicalBlockingCB(int cell)
	{
		return Grid.Solid[cell];
	}

	// Token: 0x060043E5 RID: 17381 RVA: 0x00186CD8 File Offset: 0x00184ED8
	public static bool IsPhysicallyAccessible(int x, int y, int x2, int y2, bool blocking_tile_visible = false)
	{
		return Grid.FastTestLineOfSightSolid(x, y, x2, y2);
	}

	// Token: 0x060043E6 RID: 17382 RVA: 0x00186CE4 File Offset: 0x00184EE4
	public static void CollectCellsInLine(int startCell, int endCell, HashSet<int> outputCells)
	{
		int num = 2;
		int cellDistance = Grid.GetCellDistance(startCell, endCell);
		Vector2 vector = (Grid.CellToPos(endCell) - Grid.CellToPos(startCell)).normalized;
		for (float num2 = 0f; num2 < (float)cellDistance; num2 = Mathf.Min(num2 + 1f / (float)num, (float)cellDistance))
		{
			int num3 = Grid.PosToCell(Grid.CellToPos(startCell) + vector * num2);
			if (Grid.GetCellDistance(startCell, num3) <= cellDistance)
			{
				outputCells.Add(num3);
			}
		}
	}

	// Token: 0x060043E7 RID: 17383 RVA: 0x00186D70 File Offset: 0x00184F70
	public static bool IsRangeExposedToSunlight(int cell, int scanRadius, CellOffset scanShape, out int cellsClear, int clearThreshold = 1)
	{
		cellsClear = 0;
		if (Grid.IsValidCell(cell) && (int)Grid.ExposedToSunlight[cell] >= clearThreshold)
		{
			cellsClear++;
		}
		bool flag = true;
		bool flag2 = true;
		int num = 1;
		while (num <= scanRadius && (flag || flag2))
		{
			int num2 = Grid.OffsetCell(cell, scanShape.x * num, scanShape.y * num);
			int num3 = Grid.OffsetCell(cell, -scanShape.x * num, scanShape.y * num);
			if (Grid.IsValidCell(num2) && (int)Grid.ExposedToSunlight[num2] >= clearThreshold)
			{
				cellsClear++;
			}
			if (Grid.IsValidCell(num3) && (int)Grid.ExposedToSunlight[num3] >= clearThreshold)
			{
				cellsClear++;
			}
			num++;
		}
		return cellsClear > 0;
	}

	// Token: 0x060043E8 RID: 17384 RVA: 0x00186E24 File Offset: 0x00185024
	public static int FindMidSkyCellAlignedWithCellInWorld(int cellToAlignWith, int worldID)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(worldID);
		int num = Grid.XYToCell(Grid.CellToXY(cellToAlignWith).x, world.WorldOffset.y + world.Height);
		int num2 = cellToAlignWith;
		int invalidCell = Grid.InvalidCell;
		int num3 = Grid.InvalidCell;
		while (num3 == Grid.InvalidCell && Grid.CellToXY(num2).y < world.WorldOffset.y + world.Height)
		{
			if (Grid.IsCellBiomeSpaceBiome(num2))
			{
				num3 = num2;
				break;
			}
			num2 = Grid.CellAbove(num2);
		}
		return Grid.XYToCell(Grid.CellToXY(cellToAlignWith).x, (int)((float)(Grid.CellToXY(num).y + Grid.CellToXY(num3).y) * 0.5f));
	}

	// Token: 0x060043E9 RID: 17385 RVA: 0x00186EE0 File Offset: 0x001850E0
	public static bool FastTestLineOfSightSolid(int x, int y, int x2, int y2)
	{
		int num = x2 - x;
		int num2 = y2 - y;
		int num3 = 0;
		int num5;
		int num4 = (num5 = Math.Sign(num));
		int num6 = Math.Sign(num2);
		int num7 = Math.Abs(num);
		int num8 = Math.Abs(num2);
		if (num7 <= num8)
		{
			num7 = Math.Abs(num2);
			num8 = Math.Abs(num);
			if (num2 < 0)
			{
				num3 = -1;
			}
			else if (num2 > 0)
			{
				num3 = 1;
			}
			num5 = 0;
		}
		int num9 = num7 >> 1;
		int num10 = num4 + num6 * Grid.WidthInCells;
		int num11 = num5 + num3 * Grid.WidthInCells;
		int num12 = Grid.XYToCell(x, y);
		for (int i = 1; i < num7; i++)
		{
			num9 += num8;
			if (num9 < num7)
			{
				num12 += num11;
			}
			else
			{
				num9 -= num7;
				num12 += num10;
			}
			if (Grid.Solid[num12])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060043EA RID: 17386 RVA: 0x00186FB0 File Offset: 0x001851B0
	public static bool TestLineOfSightFixedBlockingVisible(int x, int y, int x2, int y2, Func<int, bool> blocking_cb, bool blocking_tile_visible, bool allow_invalid_cells = false)
	{
		int num = x;
		int num2 = y;
		int num3 = x2 - x;
		int num4 = y2 - y;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		if (num3 < 0)
		{
			num5 = -1;
		}
		else if (num3 > 0)
		{
			num5 = 1;
		}
		if (num4 < 0)
		{
			num6 = -1;
		}
		else if (num4 > 0)
		{
			num6 = 1;
		}
		if (num3 < 0)
		{
			num7 = -1;
		}
		else if (num3 > 0)
		{
			num7 = 1;
		}
		int num9 = Math.Abs(num3);
		int num10 = Math.Abs(num4);
		if (num9 <= num10)
		{
			num9 = Math.Abs(num4);
			num10 = Math.Abs(num3);
			if (num4 < 0)
			{
				num8 = -1;
			}
			else if (num4 > 0)
			{
				num8 = 1;
			}
			num7 = 0;
		}
		int num11 = num9 >> 1;
		for (int i = 0; i <= num9; i++)
		{
			int num12 = Grid.XYToCell(x, y);
			if (!allow_invalid_cells && !Grid.IsValidCell(num12))
			{
				return false;
			}
			bool flag = blocking_cb(num12);
			if ((x != num || y != num2) && flag)
			{
				return blocking_tile_visible && x == x2 && y == y2;
			}
			num11 += num10;
			if (num11 >= num9)
			{
				num11 -= num9;
				x += num5;
				y += num6;
			}
			else
			{
				x += num7;
				y += num8;
			}
		}
		return true;
	}

	// Token: 0x060043EB RID: 17387 RVA: 0x001870CC File Offset: 0x001852CC
	public static bool TestLineOfSight(int x, int y, int x2, int y2, Func<int, bool> blocking_cb, Func<int, bool> blocking_tile_visible_cb, bool allow_invalid_cells = false)
	{
		int num = x;
		int num2 = y;
		int num3 = x2 - x;
		int num4 = y2 - y;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		if (num3 < 0)
		{
			num5 = -1;
		}
		else if (num3 > 0)
		{
			num5 = 1;
		}
		if (num4 < 0)
		{
			num6 = -1;
		}
		else if (num4 > 0)
		{
			num6 = 1;
		}
		if (num3 < 0)
		{
			num7 = -1;
		}
		else if (num3 > 0)
		{
			num7 = 1;
		}
		int num9 = Math.Abs(num3);
		int num10 = Math.Abs(num4);
		if (num9 <= num10)
		{
			num9 = Math.Abs(num4);
			num10 = Math.Abs(num3);
			if (num4 < 0)
			{
				num8 = -1;
			}
			else if (num4 > 0)
			{
				num8 = 1;
			}
			num7 = 0;
		}
		int num11 = num9 >> 1;
		for (int i = 0; i <= num9; i++)
		{
			int num12 = Grid.XYToCell(x, y);
			if (!allow_invalid_cells && !Grid.IsValidCell(num12))
			{
				return false;
			}
			bool flag = blocking_cb(num12);
			if ((x != num || y != num2) && flag)
			{
				return blocking_tile_visible_cb(num12) && x == x2 && y == y2;
			}
			num11 += num10;
			if (num11 >= num9)
			{
				num11 -= num9;
				x += num5;
				y += num6;
			}
			else
			{
				x += num7;
				y += num8;
			}
		}
		return true;
	}

	// Token: 0x060043EC RID: 17388 RVA: 0x001871F3 File Offset: 0x001853F3
	public static bool TestLineOfSight(int x, int y, int x2, int y2, Func<int, bool> blocking_cb, bool blocking_tile_visible = false, bool allow_invalid_cells = false)
	{
		return Grid.TestLineOfSightFixedBlockingVisible(x, y, x2, y2, blocking_cb, blocking_tile_visible, allow_invalid_cells);
	}

	// Token: 0x060043ED RID: 17389 RVA: 0x00187204 File Offset: 0x00185404
	public static bool GetFreeGridSpace(Vector2I size, out Vector2I offset)
	{
		Vector2I gridOffset = BestFit.GetGridOffset(ClusterManager.Instance.WorldContainers, size, out offset);
		if (gridOffset.X <= Grid.WidthInCells && gridOffset.Y <= Grid.HeightInCells)
		{
			SimMessages.SimDataResizeGridAndInitializeVacuumCells(gridOffset, size.x, size.y, offset.x, offset.y);
			Game.Instance.roomProber.Refresh();
			return true;
		}
		return false;
	}

	// Token: 0x060043EE RID: 17390 RVA: 0x00187270 File Offset: 0x00185470
	public static void FreeGridSpace(Vector2I size, Vector2I offset)
	{
		SimMessages.SimDataFreeCells(size.x, size.y, offset.x, offset.y);
		for (int i = offset.y; i < size.y + offset.y + 1; i++)
		{
			for (int j = offset.x - 1; j < size.x + offset.x + 1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (Grid.IsValidCell(num))
				{
					Grid.Element[num] = ElementLoader.FindElementByHash(SimHashes.Vacuum);
				}
			}
		}
		Game.Instance.roomProber.Refresh();
	}

	// Token: 0x060043EF RID: 17391 RVA: 0x0018730A File Offset: 0x0018550A
	[Conditional("UNITY_EDITOR")]
	public static void DrawBoxOnCell(int cell, Color color, float offset = 0f)
	{
		Grid.CellToPos(cell) + new Vector3(0.5f, 0.5f, 0f);
	}

	// Token: 0x04002D3C RID: 11580
	public static readonly CellOffset[] DefaultOffset = new CellOffset[1];

	// Token: 0x04002D3D RID: 11581
	public static float WidthInMeters;

	// Token: 0x04002D3E RID: 11582
	public static float HeightInMeters;

	// Token: 0x04002D3F RID: 11583
	public static int WidthInCells;

	// Token: 0x04002D40 RID: 11584
	public static int HeightInCells;

	// Token: 0x04002D41 RID: 11585
	public static float CellSizeInMeters;

	// Token: 0x04002D42 RID: 11586
	public static float InverseCellSizeInMeters;

	// Token: 0x04002D43 RID: 11587
	public static float HalfCellSizeInMeters;

	// Token: 0x04002D44 RID: 11588
	public static int CellCount;

	// Token: 0x04002D45 RID: 11589
	public static int InvalidCell = -1;

	// Token: 0x04002D46 RID: 11590
	public static int TopBorderHeight = 2;

	// Token: 0x04002D47 RID: 11591
	public static Dictionary<int, GameObject>[] ObjectLayers;

	// Token: 0x04002D48 RID: 11592
	public static Action<int> OnReveal;

	// Token: 0x04002D49 RID: 11593
	public static Vector3 OffWorldPosition = new Vector3(-1f, -1f, 0f);

	// Token: 0x04002D4A RID: 11594
	public static Grid.BuildFlags[] BuildMasks;

	// Token: 0x04002D4B RID: 11595
	public static Grid.BuildFlagsFoundationIndexer Foundation;

	// Token: 0x04002D4C RID: 11596
	public static Grid.BuildFlagsSolidIndexer Solid;

	// Token: 0x04002D4D RID: 11597
	public static Grid.BuildFlagsDupeImpassableIndexer DupeImpassable;

	// Token: 0x04002D4E RID: 11598
	public static Grid.BuildFlagsFakeFloorIndexer FakeFloor;

	// Token: 0x04002D4F RID: 11599
	public static Grid.BuildFlagsDupePassableIndexer DupePassable;

	// Token: 0x04002D50 RID: 11600
	public static Grid.BuildFlagsImpassableIndexer CritterImpassable;

	// Token: 0x04002D51 RID: 11601
	public static Grid.BuildFlagsDoorIndexer HasDoor;

	// Token: 0x04002D52 RID: 11602
	public static Grid.VisFlags[] VisMasks;

	// Token: 0x04002D53 RID: 11603
	public static Grid.VisFlagsRevealedIndexer Revealed;

	// Token: 0x04002D54 RID: 11604
	public static Grid.VisFlagsPreventFogOfWarRevealIndexer PreventFogOfWarReveal;

	// Token: 0x04002D55 RID: 11605
	public static Grid.VisFlagsRenderedByWorldIndexer RenderedByWorld;

	// Token: 0x04002D56 RID: 11606
	public static Grid.VisFlagsAllowPathfindingIndexer AllowPathfinding;

	// Token: 0x04002D57 RID: 11607
	public static Grid.NavValidatorFlags[] NavValidatorMasks;

	// Token: 0x04002D58 RID: 11608
	public static Grid.NavValidatorFlagsLadderIndexer HasLadder;

	// Token: 0x04002D59 RID: 11609
	public static Grid.NavValidatorFlagsPoleIndexer HasPole;

	// Token: 0x04002D5A RID: 11610
	public static Grid.NavValidatorFlagsTubeIndexer HasTube;

	// Token: 0x04002D5B RID: 11611
	public static Grid.NavValidatorFlagsNavTeleporterIndexer HasNavTeleporter;

	// Token: 0x04002D5C RID: 11612
	public static Grid.NavValidatorFlagsUnderConstructionIndexer IsTileUnderConstruction;

	// Token: 0x04002D5D RID: 11613
	public static Grid.NavFlags[] NavMasks;

	// Token: 0x04002D5E RID: 11614
	private static Grid.NavFlagsAccessDoorIndexer HasAccessDoor;

	// Token: 0x04002D5F RID: 11615
	public static Grid.NavFlagsTubeEntranceIndexer HasTubeEntrance;

	// Token: 0x04002D60 RID: 11616
	public static Grid.NavFlagsPreventIdleTraversalIndexer PreventIdleTraversal;

	// Token: 0x04002D61 RID: 11617
	public static Grid.NavFlagsReservedIndexer Reserved;

	// Token: 0x04002D62 RID: 11618
	public static Grid.NavFlagsSuitMarkerIndexer HasSuitMarker;

	// Token: 0x04002D63 RID: 11619
	private static Dictionary<int, Grid.Restriction> restrictions = new Dictionary<int, Grid.Restriction>();

	// Token: 0x04002D64 RID: 11620
	private static Dictionary<int, Grid.TubeEntrance> tubeEntrances = new Dictionary<int, Grid.TubeEntrance>();

	// Token: 0x04002D65 RID: 11621
	private static Dictionary<int, Grid.SuitMarker> suitMarkers = new Dictionary<int, Grid.SuitMarker>();

	// Token: 0x04002D66 RID: 11622
	public unsafe static ushort* elementIdx;

	// Token: 0x04002D67 RID: 11623
	public unsafe static float* temperature;

	// Token: 0x04002D68 RID: 11624
	public unsafe static float* radiation;

	// Token: 0x04002D69 RID: 11625
	public unsafe static float* mass;

	// Token: 0x04002D6A RID: 11626
	public unsafe static byte* properties;

	// Token: 0x04002D6B RID: 11627
	public unsafe static byte* strengthInfo;

	// Token: 0x04002D6C RID: 11628
	public unsafe static byte* insulation;

	// Token: 0x04002D6D RID: 11629
	public unsafe static byte* diseaseIdx;

	// Token: 0x04002D6E RID: 11630
	public unsafe static int* diseaseCount;

	// Token: 0x04002D6F RID: 11631
	public unsafe static byte* exposedToSunlight;

	// Token: 0x04002D70 RID: 11632
	public unsafe static float* AccumulatedFlowValues = null;

	// Token: 0x04002D71 RID: 11633
	public static byte[] Visible;

	// Token: 0x04002D72 RID: 11634
	public static byte[] Spawnable;

	// Token: 0x04002D73 RID: 11635
	public static float[] Damage;

	// Token: 0x04002D74 RID: 11636
	public static float[] Decor;

	// Token: 0x04002D75 RID: 11637
	public static bool[] GravitasFacility;

	// Token: 0x04002D76 RID: 11638
	public static byte[] WorldIdx;

	// Token: 0x04002D77 RID: 11639
	public static float[] Loudness;

	// Token: 0x04002D78 RID: 11640
	public static Element[] Element;

	// Token: 0x04002D79 RID: 11641
	public static int[] LightCount;

	// Token: 0x04002D7A RID: 11642
	public static Grid.PressureIndexer Pressure;

	// Token: 0x04002D7B RID: 11643
	public static Grid.LiquidImpermeableIndexer LiquidImpermeable;

	// Token: 0x04002D7C RID: 11644
	public static Grid.TransparentIndexer Transparent;

	// Token: 0x04002D7D RID: 11645
	public static Grid.ElementIdxIndexer ElementIdx;

	// Token: 0x04002D7E RID: 11646
	public static Grid.TemperatureIndexer Temperature;

	// Token: 0x04002D7F RID: 11647
	public static Grid.RadiationIndexer Radiation;

	// Token: 0x04002D80 RID: 11648
	public static Grid.MassIndexer Mass;

	// Token: 0x04002D81 RID: 11649
	public static Grid.PropertiesIndexer Properties;

	// Token: 0x04002D82 RID: 11650
	public static Grid.ExposedToSunlightIndexer ExposedToSunlight;

	// Token: 0x04002D83 RID: 11651
	public static Grid.StrengthInfoIndexer StrengthInfo;

	// Token: 0x04002D84 RID: 11652
	public static Grid.Insulationndexer Insulation;

	// Token: 0x04002D85 RID: 11653
	public static Grid.DiseaseIdxIndexer DiseaseIdx;

	// Token: 0x04002D86 RID: 11654
	public static Grid.DiseaseCountIndexer DiseaseCount;

	// Token: 0x04002D87 RID: 11655
	public static Grid.LightIntensityIndexer LightIntensity;

	// Token: 0x04002D88 RID: 11656
	public static Grid.AccumulatedFlowIndexer AccumulatedFlow;

	// Token: 0x04002D89 RID: 11657
	public static Grid.ObjectLayerIndexer Objects;

	// Token: 0x04002D8A RID: 11658
	public static float LayerMultiplier = 1f;

	// Token: 0x04002D8B RID: 11659
	private static readonly Func<int, bool> VisibleBlockingDelegate = (int cell) => Grid.VisibleBlockingCB(cell);

	// Token: 0x04002D8C RID: 11660
	private static readonly Func<int, bool> PhysicalBlockingDelegate = (int cell) => Grid.PhysicalBlockingCB(cell);

	// Token: 0x0200192E RID: 6446
	[Flags]
	public enum BuildFlags : byte
	{
		// Token: 0x04007B8F RID: 31631
		Solid = 1,
		// Token: 0x04007B90 RID: 31632
		Foundation = 2,
		// Token: 0x04007B91 RID: 31633
		Door = 4,
		// Token: 0x04007B92 RID: 31634
		DupePassable = 8,
		// Token: 0x04007B93 RID: 31635
		DupeImpassable = 16,
		// Token: 0x04007B94 RID: 31636
		CritterImpassable = 32,
		// Token: 0x04007B95 RID: 31637
		FakeFloor = 192,
		// Token: 0x04007B96 RID: 31638
		Any = 255
	}

	// Token: 0x0200192F RID: 6447
	public struct BuildFlagsFoundationIndexer
	{
		// Token: 0x17000AB4 RID: 2740
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.Foundation) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.Foundation, value);
			}
		}
	}

	// Token: 0x02001930 RID: 6448
	public struct BuildFlagsSolidIndexer
	{
		// Token: 0x17000AB5 RID: 2741
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.Solid) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
		}
	}

	// Token: 0x02001931 RID: 6449
	public struct BuildFlagsDupeImpassableIndexer
	{
		// Token: 0x17000AB6 RID: 2742
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.DupeImpassable) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.DupeImpassable, value);
			}
		}
	}

	// Token: 0x02001932 RID: 6450
	public struct BuildFlagsFakeFloorIndexer
	{
		// Token: 0x17000AB7 RID: 2743
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.FakeFloor) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
		}

		// Token: 0x06009EA7 RID: 40615 RVA: 0x00397628 File Offset: 0x00395828
		public void Add(int i)
		{
			Grid.BuildFlags buildFlags = Grid.BuildMasks[i];
			int num = (int)(((buildFlags & Grid.BuildFlags.FakeFloor) >> 6) + 1);
			num = Math.Min(num, 3);
			Grid.BuildMasks[i] = (buildFlags & ~Grid.BuildFlags.FakeFloor) | ((Grid.BuildFlags)(num << 6) & Grid.BuildFlags.FakeFloor);
		}

		// Token: 0x06009EA8 RID: 40616 RVA: 0x00397668 File Offset: 0x00395868
		public void Remove(int i)
		{
			Grid.BuildFlags buildFlags = Grid.BuildMasks[i];
			int num = (int)(((buildFlags & Grid.BuildFlags.FakeFloor) >> 6) - Grid.BuildFlags.Solid);
			num = Math.Max(num, 0);
			Grid.BuildMasks[i] = (buildFlags & ~Grid.BuildFlags.FakeFloor) | ((Grid.BuildFlags)(num << 6) & Grid.BuildFlags.FakeFloor);
		}
	}

	// Token: 0x02001933 RID: 6451
	public struct BuildFlagsDupePassableIndexer
	{
		// Token: 0x17000AB8 RID: 2744
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.DupePassable) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.DupePassable, value);
			}
		}
	}

	// Token: 0x02001934 RID: 6452
	public struct BuildFlagsImpassableIndexer
	{
		// Token: 0x17000AB9 RID: 2745
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.CritterImpassable) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.CritterImpassable, value);
			}
		}
	}

	// Token: 0x02001935 RID: 6453
	public struct BuildFlagsDoorIndexer
	{
		// Token: 0x17000ABA RID: 2746
		public bool this[int i]
		{
			get
			{
				return (Grid.BuildMasks[i] & Grid.BuildFlags.Door) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
			}
			set
			{
				Grid.UpdateBuildMask(i, Grid.BuildFlags.Door, value);
			}
		}
	}

	// Token: 0x02001936 RID: 6454
	[Flags]
	public enum VisFlags : byte
	{
		// Token: 0x04007B98 RID: 31640
		Revealed = 1,
		// Token: 0x04007B99 RID: 31641
		PreventFogOfWarReveal = 2,
		// Token: 0x04007B9A RID: 31642
		RenderedByWorld = 4,
		// Token: 0x04007B9B RID: 31643
		AllowPathfinding = 8
	}

	// Token: 0x02001937 RID: 6455
	public struct VisFlagsRevealedIndexer
	{
		// Token: 0x17000ABB RID: 2747
		public bool this[int i]
		{
			get
			{
				return (Grid.VisMasks[i] & Grid.VisFlags.Revealed) > (Grid.VisFlags)0;
			}
			set
			{
				Grid.UpdateVisMask(i, Grid.VisFlags.Revealed, value);
			}
		}
	}

	// Token: 0x02001938 RID: 6456
	public struct VisFlagsPreventFogOfWarRevealIndexer
	{
		// Token: 0x17000ABC RID: 2748
		public bool this[int i]
		{
			get
			{
				return (Grid.VisMasks[i] & Grid.VisFlags.PreventFogOfWarReveal) > (Grid.VisFlags)0;
			}
			set
			{
				Grid.UpdateVisMask(i, Grid.VisFlags.PreventFogOfWarReveal, value);
			}
		}
	}

	// Token: 0x02001939 RID: 6457
	public struct VisFlagsRenderedByWorldIndexer
	{
		// Token: 0x17000ABD RID: 2749
		public bool this[int i]
		{
			get
			{
				return (Grid.VisMasks[i] & Grid.VisFlags.RenderedByWorld) > (Grid.VisFlags)0;
			}
			set
			{
				Grid.UpdateVisMask(i, Grid.VisFlags.RenderedByWorld, value);
			}
		}
	}

	// Token: 0x0200193A RID: 6458
	public struct VisFlagsAllowPathfindingIndexer
	{
		// Token: 0x17000ABE RID: 2750
		public bool this[int i]
		{
			get
			{
				return (Grid.VisMasks[i] & Grid.VisFlags.AllowPathfinding) > (Grid.VisFlags)0;
			}
			set
			{
				Grid.UpdateVisMask(i, Grid.VisFlags.AllowPathfinding, value);
			}
		}
	}

	// Token: 0x0200193B RID: 6459
	[Flags]
	public enum NavValidatorFlags : byte
	{
		// Token: 0x04007B9D RID: 31645
		Ladder = 1,
		// Token: 0x04007B9E RID: 31646
		Pole = 2,
		// Token: 0x04007B9F RID: 31647
		Tube = 4,
		// Token: 0x04007BA0 RID: 31648
		NavTeleporter = 8,
		// Token: 0x04007BA1 RID: 31649
		UnderConstruction = 16
	}

	// Token: 0x0200193C RID: 6460
	public struct NavValidatorFlagsLadderIndexer
	{
		// Token: 0x17000ABF RID: 2751
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.Ladder) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.Ladder, value);
			}
		}
	}

	// Token: 0x0200193D RID: 6461
	public struct NavValidatorFlagsPoleIndexer
	{
		// Token: 0x17000AC0 RID: 2752
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.Pole) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.Pole, value);
			}
		}
	}

	// Token: 0x0200193E RID: 6462
	public struct NavValidatorFlagsTubeIndexer
	{
		// Token: 0x17000AC1 RID: 2753
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.Tube) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.Tube, value);
			}
		}
	}

	// Token: 0x0200193F RID: 6463
	public struct NavValidatorFlagsNavTeleporterIndexer
	{
		// Token: 0x17000AC2 RID: 2754
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.NavTeleporter) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.NavTeleporter, value);
			}
		}
	}

	// Token: 0x02001940 RID: 6464
	public struct NavValidatorFlagsUnderConstructionIndexer
	{
		// Token: 0x17000AC3 RID: 2755
		public bool this[int i]
		{
			get
			{
				return (Grid.NavValidatorMasks[i] & Grid.NavValidatorFlags.UnderConstruction) > (Grid.NavValidatorFlags)0;
			}
			set
			{
				Grid.UpdateNavValidatorMask(i, Grid.NavValidatorFlags.UnderConstruction, value);
			}
		}
	}

	// Token: 0x02001941 RID: 6465
	[Flags]
	public enum NavFlags : byte
	{
		// Token: 0x04007BA3 RID: 31651
		AccessDoor = 1,
		// Token: 0x04007BA4 RID: 31652
		TubeEntrance = 2,
		// Token: 0x04007BA5 RID: 31653
		PreventIdleTraversal = 4,
		// Token: 0x04007BA6 RID: 31654
		Reserved = 8,
		// Token: 0x04007BA7 RID: 31655
		SuitMarker = 16
	}

	// Token: 0x02001942 RID: 6466
	public struct NavFlagsAccessDoorIndexer
	{
		// Token: 0x17000AC4 RID: 2756
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.AccessDoor) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.AccessDoor, value);
			}
		}
	}

	// Token: 0x02001943 RID: 6467
	public struct NavFlagsTubeEntranceIndexer
	{
		// Token: 0x17000AC5 RID: 2757
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.TubeEntrance) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.TubeEntrance, value);
			}
		}
	}

	// Token: 0x02001944 RID: 6468
	public struct NavFlagsPreventIdleTraversalIndexer
	{
		// Token: 0x17000AC6 RID: 2758
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.PreventIdleTraversal) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.PreventIdleTraversal, value);
			}
		}
	}

	// Token: 0x02001945 RID: 6469
	public struct NavFlagsReservedIndexer
	{
		// Token: 0x17000AC7 RID: 2759
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.Reserved) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.Reserved, value);
			}
		}
	}

	// Token: 0x02001946 RID: 6470
	public struct NavFlagsSuitMarkerIndexer
	{
		// Token: 0x17000AC8 RID: 2760
		public bool this[int i]
		{
			get
			{
				return (Grid.NavMasks[i] & Grid.NavFlags.SuitMarker) > (Grid.NavFlags)0;
			}
			set
			{
				Grid.UpdateNavMask(i, Grid.NavFlags.SuitMarker, value);
			}
		}
	}

	// Token: 0x02001947 RID: 6471
	public struct Restriction
	{
		// Token: 0x04007BA8 RID: 31656
		public const int DefaultID = -1;

		// Token: 0x04007BA9 RID: 31657
		public Dictionary<int, Grid.Restriction.Directions> DirectionMasksForMinionInstanceID;

		// Token: 0x04007BAA RID: 31658
		public Grid.Restriction.Orientation orientation;

		// Token: 0x02002847 RID: 10311
		[Flags]
		public enum Directions : byte
		{
			// Token: 0x0400B2A2 RID: 45730
			Left = 1,
			// Token: 0x0400B2A3 RID: 45731
			Right = 2,
			// Token: 0x0400B2A4 RID: 45732
			Teleport = 4
		}

		// Token: 0x02002848 RID: 10312
		public enum Orientation : byte
		{
			// Token: 0x0400B2A6 RID: 45734
			Vertical,
			// Token: 0x0400B2A7 RID: 45735
			Horizontal,
			// Token: 0x0400B2A8 RID: 45736
			SingleCell
		}
	}

	// Token: 0x02001948 RID: 6472
	private struct TubeEntrance
	{
		// Token: 0x04007BAB RID: 31659
		public bool operational;

		// Token: 0x04007BAC RID: 31660
		public int reservationCapacity;

		// Token: 0x04007BAD RID: 31661
		public HashSet<int> reservedInstanceIDs;
	}

	// Token: 0x02001949 RID: 6473
	public struct SuitMarker
	{
		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06009ECB RID: 40651 RVA: 0x00397845 File Offset: 0x00395A45
		public int emptyLockerCount
		{
			get
			{
				return this.lockerCount - this.suitCount;
			}
		}

		// Token: 0x04007BAE RID: 31662
		public int suitCount;

		// Token: 0x04007BAF RID: 31663
		public int lockerCount;

		// Token: 0x04007BB0 RID: 31664
		public Grid.SuitMarker.Flags flags;

		// Token: 0x04007BB1 RID: 31665
		public PathFinder.PotentialPath.Flags pathFlags;

		// Token: 0x04007BB2 RID: 31666
		public HashSet<int> minionIDsWithSuitReservations;

		// Token: 0x04007BB3 RID: 31667
		public HashSet<int> minionIDsWithEmptyLockerReservations;

		// Token: 0x02002849 RID: 10313
		[Flags]
		public enum Flags : byte
		{
			// Token: 0x0400B2AA RID: 45738
			OnlyTraverseIfUnequipAvailable = 1,
			// Token: 0x0400B2AB RID: 45739
			Operational = 2,
			// Token: 0x0400B2AC RID: 45740
			Rotated = 4
		}
	}

	// Token: 0x0200194A RID: 6474
	public struct ObjectLayerIndexer
	{
		// Token: 0x17000ACA RID: 2762
		public GameObject this[int cell, int layer]
		{
			get
			{
				GameObject gameObject = null;
				Grid.ObjectLayers[layer].TryGetValue(cell, out gameObject);
				return gameObject;
			}
			set
			{
				if (value == null)
				{
					Grid.ObjectLayers[layer].Remove(cell);
				}
				else
				{
					Grid.ObjectLayers[layer][cell] = value;
				}
				GameScenePartitioner.Instance.TriggerEvent(cell, GameScenePartitioner.Instance.objectLayers[layer], value);
			}
		}
	}

	// Token: 0x0200194B RID: 6475
	public struct PressureIndexer
	{
		// Token: 0x17000ACB RID: 2763
		public unsafe float this[int i]
		{
			get
			{
				return Grid.mass[i] * 101.3f;
			}
		}
	}

	// Token: 0x0200194C RID: 6476
	public struct LiquidImpermeableIndexer
	{
		// Token: 0x17000ACC RID: 2764
		public unsafe bool this[int i]
		{
			get
			{
				return (Grid.properties[i] & 2) > 0;
			}
		}
	}

	// Token: 0x0200194D RID: 6477
	public struct TransparentIndexer
	{
		// Token: 0x17000ACD RID: 2765
		public unsafe bool this[int i]
		{
			get
			{
				return (Grid.properties[i] & 16) > 0;
			}
		}
	}

	// Token: 0x0200194E RID: 6478
	public struct ElementIdxIndexer
	{
		// Token: 0x17000ACE RID: 2766
		public unsafe ushort this[int i]
		{
			get
			{
				return Grid.elementIdx[i];
			}
		}
	}

	// Token: 0x0200194F RID: 6479
	public struct TemperatureIndexer
	{
		// Token: 0x17000ACF RID: 2767
		public unsafe float this[int i]
		{
			get
			{
				return Grid.temperature[i];
			}
		}
	}

	// Token: 0x02001950 RID: 6480
	public struct RadiationIndexer
	{
		// Token: 0x17000AD0 RID: 2768
		public unsafe float this[int i]
		{
			get
			{
				return Grid.radiation[i];
			}
		}
	}

	// Token: 0x02001951 RID: 6481
	public struct MassIndexer
	{
		// Token: 0x17000AD1 RID: 2769
		public unsafe float this[int i]
		{
			get
			{
				return Grid.mass[i];
			}
		}
	}

	// Token: 0x02001952 RID: 6482
	public struct PropertiesIndexer
	{
		// Token: 0x17000AD2 RID: 2770
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.properties[i];
			}
		}
	}

	// Token: 0x02001953 RID: 6483
	public struct ExposedToSunlightIndexer
	{
		// Token: 0x17000AD3 RID: 2771
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.exposedToSunlight[i];
			}
		}
	}

	// Token: 0x02001954 RID: 6484
	public struct StrengthInfoIndexer
	{
		// Token: 0x17000AD4 RID: 2772
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.strengthInfo[i];
			}
		}
	}

	// Token: 0x02001955 RID: 6485
	public struct Insulationndexer
	{
		// Token: 0x17000AD5 RID: 2773
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.insulation[i];
			}
		}
	}

	// Token: 0x02001956 RID: 6486
	public struct DiseaseIdxIndexer
	{
		// Token: 0x17000AD6 RID: 2774
		public unsafe byte this[int i]
		{
			get
			{
				return Grid.diseaseIdx[i];
			}
		}
	}

	// Token: 0x02001957 RID: 6487
	public struct DiseaseCountIndexer
	{
		// Token: 0x17000AD7 RID: 2775
		public unsafe int this[int i]
		{
			get
			{
				return Grid.diseaseCount[i];
			}
		}
	}

	// Token: 0x02001958 RID: 6488
	public struct AccumulatedFlowIndexer
	{
		// Token: 0x17000AD8 RID: 2776
		public unsafe float this[int i]
		{
			get
			{
				return Grid.AccumulatedFlowValues[i];
			}
		}
	}

	// Token: 0x02001959 RID: 6489
	public struct LightIntensityIndexer
	{
		// Token: 0x17000AD9 RID: 2777
		public unsafe int this[int i]
		{
			get
			{
				float num = Game.Instance.currentFallbackSunlightIntensity;
				WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[i]);
				if (world != null)
				{
					num = world.currentSunlightIntensity;
				}
				int num2 = (int)((float)Grid.exposedToSunlight[i] / 255f * num);
				int num3 = Grid.LightCount[i];
				return num2 + num3;
			}
		}
	}

	// Token: 0x0200195A RID: 6490
	public enum SceneLayer
	{
		// Token: 0x04007BB5 RID: 31669
		WorldSelection = -3,
		// Token: 0x04007BB6 RID: 31670
		NoLayer,
		// Token: 0x04007BB7 RID: 31671
		Background,
		// Token: 0x04007BB8 RID: 31672
		Backwall = 1,
		// Token: 0x04007BB9 RID: 31673
		Gas,
		// Token: 0x04007BBA RID: 31674
		GasConduits,
		// Token: 0x04007BBB RID: 31675
		GasConduitBridges,
		// Token: 0x04007BBC RID: 31676
		LiquidConduits,
		// Token: 0x04007BBD RID: 31677
		LiquidConduitBridges,
		// Token: 0x04007BBE RID: 31678
		SolidConduits,
		// Token: 0x04007BBF RID: 31679
		SolidConduitContents,
		// Token: 0x04007BC0 RID: 31680
		SolidConduitBridges,
		// Token: 0x04007BC1 RID: 31681
		Wires,
		// Token: 0x04007BC2 RID: 31682
		WireBridges,
		// Token: 0x04007BC3 RID: 31683
		WireBridgesFront,
		// Token: 0x04007BC4 RID: 31684
		LogicWires,
		// Token: 0x04007BC5 RID: 31685
		LogicGates,
		// Token: 0x04007BC6 RID: 31686
		LogicGatesFront,
		// Token: 0x04007BC7 RID: 31687
		InteriorWall,
		// Token: 0x04007BC8 RID: 31688
		GasFront,
		// Token: 0x04007BC9 RID: 31689
		BuildingBack,
		// Token: 0x04007BCA RID: 31690
		Building,
		// Token: 0x04007BCB RID: 31691
		BuildingUse,
		// Token: 0x04007BCC RID: 31692
		BuildingFront,
		// Token: 0x04007BCD RID: 31693
		TransferArm,
		// Token: 0x04007BCE RID: 31694
		Ore,
		// Token: 0x04007BCF RID: 31695
		Creatures,
		// Token: 0x04007BD0 RID: 31696
		Move,
		// Token: 0x04007BD1 RID: 31697
		Front,
		// Token: 0x04007BD2 RID: 31698
		GlassTile,
		// Token: 0x04007BD3 RID: 31699
		Liquid,
		// Token: 0x04007BD4 RID: 31700
		Ground,
		// Token: 0x04007BD5 RID: 31701
		TileMain,
		// Token: 0x04007BD6 RID: 31702
		TileFront,
		// Token: 0x04007BD7 RID: 31703
		FXFront,
		// Token: 0x04007BD8 RID: 31704
		FXFront2,
		// Token: 0x04007BD9 RID: 31705
		SceneMAX
	}
}
