using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AE4 RID: 2788
public class RoomProber : ISim1000ms
{
	// Token: 0x06005144 RID: 20804 RVA: 0x001D8868 File Offset: 0x001D6A68
	public RoomProber()
	{
		this.CellCavityID = new HandleVector<int>.Handle[Grid.CellCount];
		this.floodFiller = new RoomProber.CavityFloodFiller(this.CellCavityID);
		for (int i = 0; i < this.CellCavityID.Length; i++)
		{
			this.solidChanges.Add(i);
		}
		this.ProcessSolidChanges();
		this.RefreshRooms();
		Game instance = Game.Instance;
		instance.OnSpawnComplete = (global::System.Action)Delegate.Combine(instance.OnSpawnComplete, new global::System.Action(this.Refresh));
		World instance2 = World.Instance;
		instance2.OnSolidChanged = (Action<int>)Delegate.Combine(instance2.OnSolidChanged, new Action<int>(this.SolidChangedEvent));
		GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingsChanged));
		GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[2], new Action<int, object>(this.OnBuildingsChanged));
	}

	// Token: 0x06005145 RID: 20805 RVA: 0x001D89B1 File Offset: 0x001D6BB1
	public void Refresh()
	{
		this.ProcessSolidChanges();
		this.RefreshRooms();
	}

	// Token: 0x06005146 RID: 20806 RVA: 0x001D89BF File Offset: 0x001D6BBF
	private void SolidChangedEvent(int cell)
	{
		this.SolidChangedEvent(cell, true);
	}

	// Token: 0x06005147 RID: 20807 RVA: 0x001D89C9 File Offset: 0x001D6BC9
	private void OnBuildingsChanged(int cell, object building)
	{
		if (this.GetCavityForCell(cell) != null)
		{
			this.solidChanges.Add(cell);
			this.dirty = true;
		}
	}

	// Token: 0x06005148 RID: 20808 RVA: 0x001D89E8 File Offset: 0x001D6BE8
	public void SolidChangedEvent(int cell, bool ignoreDoors)
	{
		if (ignoreDoors && Grid.HasDoor[cell])
		{
			return;
		}
		this.solidChanges.Add(cell);
		this.dirty = true;
	}

	// Token: 0x06005149 RID: 20809 RVA: 0x001D8A10 File Offset: 0x001D6C10
	private CavityInfo CreateNewCavity()
	{
		CavityInfo cavityInfo = new CavityInfo();
		cavityInfo.handle = this.cavityInfos.Allocate(cavityInfo);
		return cavityInfo;
	}

	// Token: 0x0600514A RID: 20810 RVA: 0x001D8A38 File Offset: 0x001D6C38
	private unsafe void ProcessSolidChanges()
	{
		int* ptr = stackalloc int[(UIntPtr)20];
		*ptr = 0;
		ptr[1] = -Grid.WidthInCells;
		ptr[2] = -1;
		ptr[3] = 1;
		ptr[4] = Grid.WidthInCells;
		foreach (int num in this.solidChanges)
		{
			for (int i = 0; i < 5; i++)
			{
				int num2 = num + ptr[i];
				if (Grid.IsValidCell(num2))
				{
					this.floodFillSet.Add(num2);
					HandleVector<int>.Handle handle = this.CellCavityID[num2];
					if (handle.IsValid())
					{
						this.CellCavityID[num2] = HandleVector<int>.InvalidHandle;
						this.releasedIDs.Add(handle);
					}
				}
			}
		}
		CavityInfo cavityInfo = this.CreateNewCavity();
		foreach (int num3 in this.floodFillSet)
		{
			if (!this.visitedCells.Contains(num3))
			{
				HandleVector<int>.Handle handle2 = this.CellCavityID[num3];
				if (!handle2.IsValid())
				{
					CavityInfo cavityInfo2 = cavityInfo;
					this.floodFiller.Reset(cavityInfo2.handle);
					GameUtil.FloodFillConditional(num3, new Func<int, bool>(this.floodFiller.ShouldContinue), this.visitedCells, null);
					if (this.floodFiller.NumCells > 0)
					{
						cavityInfo2.numCells = this.floodFiller.NumCells;
						cavityInfo2.minX = this.floodFiller.MinX;
						cavityInfo2.minY = this.floodFiller.MinY;
						cavityInfo2.maxX = this.floodFiller.MaxX;
						cavityInfo2.maxY = this.floodFiller.MaxY;
						cavityInfo = this.CreateNewCavity();
					}
				}
			}
		}
		if (cavityInfo.numCells == 0)
		{
			this.releasedIDs.Add(cavityInfo.handle);
		}
		foreach (HandleVector<int>.Handle handle3 in this.releasedIDs)
		{
			CavityInfo data = this.cavityInfos.GetData(handle3);
			this.releasedCritters.AddRange(data.creatures);
			if (data.room != null)
			{
				this.ClearRoom(data.room);
			}
			this.cavityInfos.Free(handle3);
		}
		this.RebuildDirtyCavities();
		this.releasedIDs.Clear();
		this.visitedCells.Clear();
		this.solidChanges.Clear();
		this.floodFillSet.Clear();
	}

	// Token: 0x0600514B RID: 20811 RVA: 0x001D8D08 File Offset: 0x001D6F08
	private void RebuildDirtyCavities()
	{
		int maxRoomSize = TuningData<RoomProber.Tuning>.Get().maxRoomSize;
		HashSetPool<RoomProber.BuildingId, RoomProber>.PooledHashSet pooledHashSet = HashSetPool<RoomProber.BuildingId, RoomProber>.Allocate();
		foreach (int num in this.visitedCells)
		{
			HandleVector<int>.Handle handle = this.CellCavityID[num];
			if (handle.IsValid())
			{
				CavityInfo data = this.cavityInfos.GetData(handle);
				if (data.numCells > 0 && data.numCells <= maxRoomSize)
				{
					GameObject gameObject = Grid.Objects[num, 1];
					if (!(gameObject == null))
					{
						KPrefabID component = gameObject.GetComponent<KPrefabID>();
						RoomProber.BuildingId buildingId = new RoomProber.BuildingId
						{
							prefab = component.GetHashCode(),
							instance = component.InstanceID
						};
						if (pooledHashSet.Add(buildingId))
						{
							if (component.HasTag(GameTags.RoomProberBuilding))
							{
								data.AddBuilding(component);
							}
							else if (component.HasTag(GameTags.Plant))
							{
								data.AddPlants(component);
							}
						}
					}
				}
			}
		}
		pooledHashSet.Recycle();
	}

	// Token: 0x0600514C RID: 20812 RVA: 0x001D8E38 File Offset: 0x001D7038
	public void Sim1000ms(float dt)
	{
		if (this.dirty)
		{
			this.ProcessSolidChanges();
			this.RefreshRooms();
		}
	}

	// Token: 0x0600514D RID: 20813 RVA: 0x001D8E50 File Offset: 0x001D7050
	private void CreateRoom(CavityInfo cavity)
	{
		global::Debug.Assert(cavity.room == null);
		Room room = new Room();
		room.cavity = cavity;
		cavity.room = room;
		this.rooms.Add(room);
		room.roomType = Db.Get().RoomTypes.GetRoomType(room);
		this.AssignBuildingsToRoom(room);
	}

	// Token: 0x0600514E RID: 20814 RVA: 0x001D8EA8 File Offset: 0x001D70A8
	private void ClearRoom(Room room)
	{
		this.UnassignBuildingsToRoom(room);
		room.CleanUp();
		this.rooms.Remove(room);
	}

	// Token: 0x0600514F RID: 20815 RVA: 0x001D8EC4 File Offset: 0x001D70C4
	private void RefreshRooms()
	{
		int maxRoomSize = TuningData<RoomProber.Tuning>.Get().maxRoomSize;
		foreach (CavityInfo cavityInfo in this.cavityInfos.GetDataList())
		{
			if (cavityInfo.dirty)
			{
				global::Debug.Assert(cavityInfo.room == null, "I expected info.room to always be null by this point");
				if (cavityInfo.numCells > 0)
				{
					if (cavityInfo.numCells <= maxRoomSize)
					{
						this.CreateRoom(cavityInfo);
					}
					foreach (KPrefabID kprefabID in cavityInfo.buildings)
					{
						kprefabID.Trigger(144050788, cavityInfo.room);
					}
					foreach (KPrefabID kprefabID2 in cavityInfo.plants)
					{
						kprefabID2.Trigger(144050788, cavityInfo.room);
					}
				}
				cavityInfo.dirty = false;
			}
		}
		foreach (KPrefabID kprefabID3 in this.releasedCritters)
		{
			if (kprefabID3 != null)
			{
				OvercrowdingMonitor.Instance smi = kprefabID3.GetSMI<OvercrowdingMonitor.Instance>();
				if (smi != null)
				{
					smi.RoomRefreshUpdateCavity();
				}
			}
		}
		this.releasedCritters.Clear();
		this.dirty = false;
	}

	// Token: 0x06005150 RID: 20816 RVA: 0x001D9068 File Offset: 0x001D7268
	private void AssignBuildingsToRoom(Room room)
	{
		global::Debug.Assert(room != null);
		RoomType roomType = room.roomType;
		if (roomType == Db.Get().RoomTypes.Neutral)
		{
			return;
		}
		foreach (KPrefabID kprefabID in room.buildings)
		{
			if (!(kprefabID == null) && !kprefabID.HasTag(GameTags.NotRoomAssignable))
			{
				Assignable component = kprefabID.GetComponent<Assignable>();
				if (component != null && (roomType.primary_constraint == null || !roomType.primary_constraint.building_criteria(kprefabID.GetComponent<KPrefabID>())))
				{
					component.Assign(room);
				}
			}
		}
	}

	// Token: 0x06005151 RID: 20817 RVA: 0x001D9124 File Offset: 0x001D7324
	private void UnassignKPrefabIDs(Room room, List<KPrefabID> list)
	{
		foreach (KPrefabID kprefabID in list)
		{
			if (!(kprefabID == null))
			{
				kprefabID.Trigger(144050788, null);
				Assignable component = kprefabID.GetComponent<Assignable>();
				if (component != null && component.assignee == room)
				{
					component.Unassign();
				}
			}
		}
	}

	// Token: 0x06005152 RID: 20818 RVA: 0x001D91A0 File Offset: 0x001D73A0
	private void UnassignBuildingsToRoom(Room room)
	{
		global::Debug.Assert(room != null);
		this.UnassignKPrefabIDs(room, room.buildings);
		this.UnassignKPrefabIDs(room, room.plants);
	}

	// Token: 0x06005153 RID: 20819 RVA: 0x001D91C8 File Offset: 0x001D73C8
	public void UpdateRoom(CavityInfo cavity)
	{
		if (cavity == null)
		{
			return;
		}
		if (cavity.room != null)
		{
			this.ClearRoom(cavity.room);
			cavity.room = null;
		}
		this.CreateRoom(cavity);
		foreach (KPrefabID kprefabID in cavity.buildings)
		{
			if (kprefabID != null)
			{
				kprefabID.Trigger(144050788, cavity.room);
			}
		}
		foreach (KPrefabID kprefabID2 in cavity.plants)
		{
			if (kprefabID2 != null)
			{
				kprefabID2.Trigger(144050788, cavity.room);
			}
		}
	}

	// Token: 0x06005154 RID: 20820 RVA: 0x001D92AC File Offset: 0x001D74AC
	public Room GetRoomOfGameObject(GameObject go)
	{
		if (go == null)
		{
			return null;
		}
		int num = Grid.PosToCell(go);
		if (!Grid.IsValidCell(num))
		{
			return null;
		}
		CavityInfo cavityForCell = this.GetCavityForCell(num);
		if (cavityForCell == null)
		{
			return null;
		}
		return cavityForCell.room;
	}

	// Token: 0x06005155 RID: 20821 RVA: 0x001D92E8 File Offset: 0x001D74E8
	public bool IsInRoomType(GameObject go, RoomType checkType)
	{
		Room roomOfGameObject = this.GetRoomOfGameObject(go);
		if (roomOfGameObject != null)
		{
			RoomType roomType = roomOfGameObject.roomType;
			return checkType == roomType;
		}
		return false;
	}

	// Token: 0x06005156 RID: 20822 RVA: 0x001D9310 File Offset: 0x001D7510
	private CavityInfo GetCavityInfo(HandleVector<int>.Handle id)
	{
		CavityInfo cavityInfo = null;
		if (id.IsValid())
		{
			cavityInfo = this.cavityInfos.GetData(id);
		}
		return cavityInfo;
	}

	// Token: 0x06005157 RID: 20823 RVA: 0x001D9338 File Offset: 0x001D7538
	public CavityInfo GetCavityForCell(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return null;
		}
		HandleVector<int>.Handle handle = this.CellCavityID[cell];
		return this.GetCavityInfo(handle);
	}

	// Token: 0x040036BE RID: 14014
	public List<Room> rooms = new List<Room>();

	// Token: 0x040036BF RID: 14015
	private KCompactedVector<CavityInfo> cavityInfos = new KCompactedVector<CavityInfo>(1024);

	// Token: 0x040036C0 RID: 14016
	private HandleVector<int>.Handle[] CellCavityID;

	// Token: 0x040036C1 RID: 14017
	private bool dirty = true;

	// Token: 0x040036C2 RID: 14018
	private HashSet<int> solidChanges = new HashSet<int>();

	// Token: 0x040036C3 RID: 14019
	private HashSet<int> visitedCells = new HashSet<int>();

	// Token: 0x040036C4 RID: 14020
	private HashSet<int> floodFillSet = new HashSet<int>();

	// Token: 0x040036C5 RID: 14021
	private HashSet<HandleVector<int>.Handle> releasedIDs = new HashSet<HandleVector<int>.Handle>();

	// Token: 0x040036C6 RID: 14022
	private RoomProber.CavityFloodFiller floodFiller;

	// Token: 0x040036C7 RID: 14023
	private List<KPrefabID> releasedCritters = new List<KPrefabID>();

	// Token: 0x02001BD1 RID: 7121
	public class Tuning : TuningData<RoomProber.Tuning>
	{
		// Token: 0x0400842B RID: 33835
		public int maxRoomSize;
	}

	// Token: 0x02001BD2 RID: 7122
	private class CavityFloodFiller
	{
		// Token: 0x0600A8DD RID: 43229 RVA: 0x003B5D5E File Offset: 0x003B3F5E
		public CavityFloodFiller(HandleVector<int>.Handle[] grid)
		{
			this.grid = grid;
		}

		// Token: 0x0600A8DE RID: 43230 RVA: 0x003B5D6D File Offset: 0x003B3F6D
		public void Reset(HandleVector<int>.Handle search_id)
		{
			this.cavityID = search_id;
			this.numCells = 0;
			this.minX = int.MaxValue;
			this.minY = int.MaxValue;
			this.maxX = 0;
			this.maxY = 0;
		}

		// Token: 0x0600A8DF RID: 43231 RVA: 0x003B5DA1 File Offset: 0x003B3FA1
		private static bool IsWall(int cell)
		{
			return (Grid.BuildMasks[cell] & (Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation)) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor) || Grid.HasDoor[cell];
		}

		// Token: 0x0600A8E0 RID: 43232 RVA: 0x003B5DC0 File Offset: 0x003B3FC0
		public bool ShouldContinue(int flood_cell)
		{
			if (RoomProber.CavityFloodFiller.IsWall(flood_cell))
			{
				this.grid[flood_cell] = HandleVector<int>.InvalidHandle;
				return false;
			}
			this.grid[flood_cell] = this.cavityID;
			int num;
			int num2;
			Grid.CellToXY(flood_cell, out num, out num2);
			this.minX = Math.Min(num, this.minX);
			this.minY = Math.Min(num2, this.minY);
			this.maxX = Math.Max(num, this.maxX);
			this.maxY = Math.Max(num2, this.maxY);
			this.numCells++;
			return true;
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600A8E1 RID: 43233 RVA: 0x003B5E5B File Offset: 0x003B405B
		public int NumCells
		{
			get
			{
				return this.numCells;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600A8E2 RID: 43234 RVA: 0x003B5E63 File Offset: 0x003B4063
		public int MinX
		{
			get
			{
				return this.minX;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600A8E3 RID: 43235 RVA: 0x003B5E6B File Offset: 0x003B406B
		public int MinY
		{
			get
			{
				return this.minY;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600A8E4 RID: 43236 RVA: 0x003B5E73 File Offset: 0x003B4073
		public int MaxX
		{
			get
			{
				return this.maxX;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x0600A8E5 RID: 43237 RVA: 0x003B5E7B File Offset: 0x003B407B
		public int MaxY
		{
			get
			{
				return this.maxY;
			}
		}

		// Token: 0x0400842C RID: 33836
		private HandleVector<int>.Handle[] grid;

		// Token: 0x0400842D RID: 33837
		private HandleVector<int>.Handle cavityID;

		// Token: 0x0400842E RID: 33838
		private int numCells;

		// Token: 0x0400842F RID: 33839
		private int minX;

		// Token: 0x04008430 RID: 33840
		private int minY;

		// Token: 0x04008431 RID: 33841
		private int maxX;

		// Token: 0x04008432 RID: 33842
		private int maxY;
	}

	// Token: 0x02001BD3 RID: 7123
	private struct BuildingId
	{
		// Token: 0x04008433 RID: 33843
		public int prefab;

		// Token: 0x04008434 RID: 33844
		public int instance;
	}
}
