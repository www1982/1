using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AF2 RID: 2802
[AddComponentMenu("KMonoBehaviour/scripts/GameScenePartitioner")]
public class GameScenePartitioner : KMonoBehaviour
{
	// Token: 0x170005C9 RID: 1481
	// (get) Token: 0x06005232 RID: 21042 RVA: 0x001DF26D File Offset: 0x001DD46D
	public static GameScenePartitioner Instance
	{
		get
		{
			global::Debug.Assert(GameScenePartitioner.instance != null);
			return GameScenePartitioner.instance;
		}
	}

	// Token: 0x06005233 RID: 21043 RVA: 0x001DF284 File Offset: 0x001DD484
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(GameScenePartitioner.instance == null);
		GameScenePartitioner.instance = this;
		this.partitioner = new ScenePartitioner(16, 67, Grid.WidthInCells, Grid.HeightInCells);
		this.solidChangedLayer = this.partitioner.CreateMask("SolidChanged");
		this.liquidChangedLayer = this.partitioner.CreateMask("LiquidChanged");
		this.digDestroyedLayer = this.partitioner.CreateMask("DigDestroyed");
		this.fogOfWarChangedLayer = this.partitioner.CreateMask("FogOfWarChanged");
		this.decorProviderLayer = this.partitioner.CreateMask("DecorProviders");
		this.attackableEntitiesLayer = this.partitioner.CreateMask("FactionedEntities");
		this.fetchChoreLayer = this.partitioner.CreateMask("FetchChores");
		this.pickupablesLayer = this.partitioner.CreateMask("Pickupables");
		this.storedPickupablesLayer = this.partitioner.CreateMask("StoredPickupables");
		this.pickupablesChangedLayer = this.partitioner.CreateMask("PickupablesChanged");
		this.plantsChangedLayer = this.partitioner.CreateMask("PlantsChanged");
		this.gasConduitsLayer = this.partitioner.CreateMask("GasConduit");
		this.liquidConduitsLayer = this.partitioner.CreateMask("LiquidConduit");
		this.solidConduitsLayer = this.partitioner.CreateMask("SolidConduit");
		this.noisePolluterLayer = this.partitioner.CreateMask("NoisePolluters");
		this.validNavCellChangedLayer = this.partitioner.CreateMask("validNavCellChangedLayer");
		this.dirtyNavCellUpdateLayer = this.partitioner.CreateMask("dirtyNavCellUpdateLayer");
		this.trapsLayer = this.partitioner.CreateMask("trapsLayer");
		this.floorSwitchActivatorLayer = this.partitioner.CreateMask("FloorSwitchActivatorLayer");
		this.floorSwitchActivatorChangedLayer = this.partitioner.CreateMask("FloorSwitchActivatorChangedLayer");
		this.collisionLayer = this.partitioner.CreateMask("Collision");
		this.lure = this.partitioner.CreateMask("Lure");
		this.plants = this.partitioner.CreateMask("Plants");
		this.industrialBuildings = this.partitioner.CreateMask("IndustrialBuildings");
		this.completeBuildings = this.partitioner.CreateMask("CompleteBuildings");
		this.prioritizableObjects = this.partitioner.CreateMask("PrioritizableObjects");
		this.contactConductiveLayer = this.partitioner.CreateMask("ContactConductiveLayer");
		this.objectLayers = new ScenePartitionerLayer[45];
		for (int i = 0; i < 45; i++)
		{
			ObjectLayer objectLayer = (ObjectLayer)i;
			this.objectLayers[i] = this.partitioner.CreateMask(objectLayer.ToString());
		}
	}

	// Token: 0x06005234 RID: 21044 RVA: 0x001DF550 File Offset: 0x001DD750
	protected override void OnForcedCleanUp()
	{
		GameScenePartitioner.instance = null;
		this.partitioner.FreeResources();
		this.partitioner = null;
		this.solidChangedLayer = null;
		this.liquidChangedLayer = null;
		this.digDestroyedLayer = null;
		this.fogOfWarChangedLayer = null;
		this.decorProviderLayer = null;
		this.attackableEntitiesLayer = null;
		this.fetchChoreLayer = null;
		this.pickupablesLayer = null;
		this.storedPickupablesLayer = null;
		this.plantsChangedLayer = null;
		this.pickupablesChangedLayer = null;
		this.gasConduitsLayer = null;
		this.liquidConduitsLayer = null;
		this.solidConduitsLayer = null;
		this.noisePolluterLayer = null;
		this.validNavCellChangedLayer = null;
		this.dirtyNavCellUpdateLayer = null;
		this.trapsLayer = null;
		this.floorSwitchActivatorLayer = null;
		this.floorSwitchActivatorChangedLayer = null;
		this.contactConductiveLayer = null;
		this.objectLayers = null;
	}

	// Token: 0x06005235 RID: 21045 RVA: 0x001DF610 File Offset: 0x001DD810
	protected override void OnSpawn()
	{
		base.OnSpawn();
		NavGrid navGrid = Pathfinding.Instance.GetNavGrid("MinionNavGrid");
		navGrid.OnNavGridUpdateComplete = (Action<IEnumerable<int>>)Delegate.Combine(navGrid.OnNavGridUpdateComplete, new Action<IEnumerable<int>>(this.OnNavGridUpdateComplete));
		NavTable navTable = navGrid.NavTable;
		navTable.OnValidCellChanged = (Action<int, NavType>)Delegate.Combine(navTable.OnValidCellChanged, new Action<int, NavType>(this.OnValidNavCellChanged));
	}

	// Token: 0x06005236 RID: 21046 RVA: 0x001DF67C File Offset: 0x001DD87C
	public HandleVector<int>.Handle Add(string name, object obj, int x, int y, int width, int height, ScenePartitionerLayer layer, Action<object> event_callback)
	{
		ScenePartitionerEntry scenePartitionerEntry = new ScenePartitionerEntry(name, obj, x, y, width, height, layer, this.partitioner, event_callback);
		this.partitioner.Add(scenePartitionerEntry);
		return this.scenePartitionerEntries.Allocate(scenePartitionerEntry);
	}

	// Token: 0x06005237 RID: 21047 RVA: 0x001DF6BC File Offset: 0x001DD8BC
	public HandleVector<int>.Handle Add(string name, object obj, Extents extents, ScenePartitionerLayer layer, Action<object> event_callback)
	{
		return this.Add(name, obj, extents.x, extents.y, extents.width, extents.height, layer, event_callback);
	}

	// Token: 0x06005238 RID: 21048 RVA: 0x001DF6F0 File Offset: 0x001DD8F0
	public HandleVector<int>.Handle Add(string name, object obj, int cell, ScenePartitionerLayer layer, Action<object> event_callback)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		return this.Add(name, obj, num, num2, 1, 1, layer, event_callback);
	}

	// Token: 0x06005239 RID: 21049 RVA: 0x001DF71B File Offset: 0x001DD91B
	public void AddGlobalLayerListener(ScenePartitionerLayer layer, Action<int, object> action)
	{
		layer.OnEvent = (Action<int, object>)Delegate.Combine(layer.OnEvent, action);
	}

	// Token: 0x0600523A RID: 21050 RVA: 0x001DF734 File Offset: 0x001DD934
	public void RemoveGlobalLayerListener(ScenePartitionerLayer layer, Action<int, object> action)
	{
		layer.OnEvent = (Action<int, object>)Delegate.Remove(layer.OnEvent, action);
	}

	// Token: 0x0600523B RID: 21051 RVA: 0x001DF74D File Offset: 0x001DD94D
	public void TriggerEvent(IEnumerable<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		this.partitioner.TriggerEvent(cells, layer, event_data);
	}

	// Token: 0x0600523C RID: 21052 RVA: 0x001DF75D File Offset: 0x001DD95D
	public void TriggerEvent(Extents extents, ScenePartitionerLayer layer, object event_data)
	{
		this.partitioner.TriggerEvent(extents.x, extents.y, extents.width, extents.height, layer, event_data);
	}

	// Token: 0x0600523D RID: 21053 RVA: 0x001DF784 File Offset: 0x001DD984
	public void TriggerEvent(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data)
	{
		this.partitioner.TriggerEvent(x, y, width, height, layer, event_data);
	}

	// Token: 0x0600523E RID: 21054 RVA: 0x001DF79C File Offset: 0x001DD99C
	public void TriggerEvent(int cell, ScenePartitionerLayer layer, object event_data)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		this.TriggerEvent(num, num2, 1, 1, layer, event_data);
	}

	// Token: 0x0600523F RID: 21055 RVA: 0x001DF7C3 File Offset: 0x001DD9C3
	public void GatherEntries(Extents extents, ScenePartitionerLayer layer, List<ScenePartitionerEntry> gathered_entries)
	{
		this.GatherEntries(extents.x, extents.y, extents.width, extents.height, layer, gathered_entries);
	}

	// Token: 0x06005240 RID: 21056 RVA: 0x001DF7E5 File Offset: 0x001DD9E5
	public void GatherEntries(int x_bottomLeft, int y_bottomLeft, int width, int height, ScenePartitionerLayer layer, List<ScenePartitionerEntry> gathered_entries)
	{
		this.partitioner.GatherEntries(x_bottomLeft, y_bottomLeft, width, height, layer, null, gathered_entries);
	}

	// Token: 0x06005241 RID: 21057 RVA: 0x001DF7FC File Offset: 0x001DD9FC
	public void Iterate<IteratorType>(int x, int y, int width, int height, ScenePartitionerLayer layer, ref IteratorType iterator) where IteratorType : GameScenePartitioner.Iterator
	{
		ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(x, y, width, height, layer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			ScenePartitionerEntry scenePartitionerEntry = pooledList[i];
			iterator.Iterate(scenePartitionerEntry.obj);
		}
		pooledList.Recycle();
	}

	// Token: 0x06005242 RID: 21058 RVA: 0x001DF854 File Offset: 0x001DDA54
	public void Iterate<IteratorType>(int cell, int radius, ScenePartitionerLayer layer, ref IteratorType iterator) where IteratorType : GameScenePartitioner.Iterator
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		this.Iterate<IteratorType>(num - radius, num2 - radius, radius * 2, radius * 2, layer, ref iterator);
	}

	// Token: 0x06005243 RID: 21059 RVA: 0x001DF884 File Offset: 0x001DDA84
	public IEnumerable<object> AsyncSafeEnumerate(int x, int y, int width, int height, ScenePartitionerLayer layer)
	{
		return this.partitioner.AsyncSafeEnumerate(x, y, width, height, layer);
	}

	// Token: 0x06005244 RID: 21060 RVA: 0x001DF898 File Offset: 0x001DDA98
	public void AsyncSafeVisit<ContextType>(int x, int y, int width, int height, ScenePartitionerLayer layer, Func<object, ContextType, bool> visitor, ContextType context)
	{
		this.partitioner.AsyncSafeVisit<ContextType>(x, y, width, height, layer, visitor, context);
	}

	// Token: 0x06005245 RID: 21061 RVA: 0x001DF8B0 File Offset: 0x001DDAB0
	public IEnumerable<object> AsyncSafeEnumerate(int cell, int radius, ScenePartitionerLayer layer)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		return this.AsyncSafeEnumerate(num - radius, num2 - radius, radius * 2, radius * 2, layer);
	}

	// Token: 0x06005246 RID: 21062 RVA: 0x001DF8E0 File Offset: 0x001DDAE0
	public void AsyncSafeVisit<ContextType>(int cell, int radius, ScenePartitionerLayer layer, Func<object, ContextType, bool> visitor, ContextType context)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		this.AsyncSafeVisit<ContextType>(num - radius, num2 - radius, radius * 2, radius * 2, layer, visitor, context);
	}

	// Token: 0x06005247 RID: 21063 RVA: 0x001DF912 File Offset: 0x001DDB12
	private void OnValidNavCellChanged(int cell, NavType nav_type)
	{
		this.changedCells.Add(cell);
	}

	// Token: 0x06005248 RID: 21064 RVA: 0x001DF920 File Offset: 0x001DDB20
	private void OnNavGridUpdateComplete(IEnumerable<int> dirty_nav_cells)
	{
		GameScenePartitioner.Instance.TriggerEvent(dirty_nav_cells, GameScenePartitioner.Instance.dirtyNavCellUpdateLayer, null);
		if (this.changedCells.Count > 0)
		{
			GameScenePartitioner.Instance.TriggerEvent(this.changedCells, GameScenePartitioner.Instance.validNavCellChangedLayer, null);
			this.changedCells.Clear();
		}
	}

	// Token: 0x06005249 RID: 21065 RVA: 0x001DF978 File Offset: 0x001DDB78
	public void UpdatePosition(HandleVector<int>.Handle handle, int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		this.UpdatePosition(handle, vector2I.x, vector2I.y);
	}

	// Token: 0x0600524A RID: 21066 RVA: 0x001DF99F File Offset: 0x001DDB9F
	public void UpdatePosition(HandleVector<int>.Handle handle, int x, int y)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.scenePartitionerEntries.GetData(handle).UpdatePosition(x, y);
	}

	// Token: 0x0600524B RID: 21067 RVA: 0x001DF9BE File Offset: 0x001DDBBE
	public void UpdatePosition(HandleVector<int>.Handle handle, Extents ext)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.scenePartitionerEntries.GetData(handle).UpdatePosition(ext);
	}

	// Token: 0x0600524C RID: 21068 RVA: 0x001DF9DC File Offset: 0x001DDBDC
	public void Free(ref HandleVector<int>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.scenePartitionerEntries.GetData(handle).Release();
		this.scenePartitionerEntries.Free(handle);
		handle.Clear();
	}

	// Token: 0x0600524D RID: 21069 RVA: 0x001DFA15 File Offset: 0x001DDC15
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.partitioner.Cleanup();
	}

	// Token: 0x0600524E RID: 21070 RVA: 0x001DFA28 File Offset: 0x001DDC28
	public bool DoDebugLayersContainItemsOnCell(int cell)
	{
		return this.partitioner.DoDebugLayersContainItemsOnCell(cell);
	}

	// Token: 0x0600524F RID: 21071 RVA: 0x001DFA36 File Offset: 0x001DDC36
	public List<ScenePartitionerLayer> GetLayers()
	{
		return this.partitioner.layers;
	}

	// Token: 0x06005250 RID: 21072 RVA: 0x001DFA43 File Offset: 0x001DDC43
	public void SetToggledLayers(HashSet<ScenePartitionerLayer> toggled_layers)
	{
		this.partitioner.toggledLayers = toggled_layers;
	}

	// Token: 0x04003745 RID: 14149
	public ScenePartitionerLayer solidChangedLayer;

	// Token: 0x04003746 RID: 14150
	public ScenePartitionerLayer liquidChangedLayer;

	// Token: 0x04003747 RID: 14151
	public ScenePartitionerLayer digDestroyedLayer;

	// Token: 0x04003748 RID: 14152
	public ScenePartitionerLayer fogOfWarChangedLayer;

	// Token: 0x04003749 RID: 14153
	public ScenePartitionerLayer decorProviderLayer;

	// Token: 0x0400374A RID: 14154
	public ScenePartitionerLayer attackableEntitiesLayer;

	// Token: 0x0400374B RID: 14155
	public ScenePartitionerLayer fetchChoreLayer;

	// Token: 0x0400374C RID: 14156
	public ScenePartitionerLayer pickupablesLayer;

	// Token: 0x0400374D RID: 14157
	public ScenePartitionerLayer storedPickupablesLayer;

	// Token: 0x0400374E RID: 14158
	public ScenePartitionerLayer pickupablesChangedLayer;

	// Token: 0x0400374F RID: 14159
	public ScenePartitionerLayer gasConduitsLayer;

	// Token: 0x04003750 RID: 14160
	public ScenePartitionerLayer liquidConduitsLayer;

	// Token: 0x04003751 RID: 14161
	public ScenePartitionerLayer solidConduitsLayer;

	// Token: 0x04003752 RID: 14162
	public ScenePartitionerLayer wiresLayer;

	// Token: 0x04003753 RID: 14163
	public ScenePartitionerLayer[] objectLayers;

	// Token: 0x04003754 RID: 14164
	public ScenePartitionerLayer noisePolluterLayer;

	// Token: 0x04003755 RID: 14165
	public ScenePartitionerLayer validNavCellChangedLayer;

	// Token: 0x04003756 RID: 14166
	public ScenePartitionerLayer dirtyNavCellUpdateLayer;

	// Token: 0x04003757 RID: 14167
	public ScenePartitionerLayer trapsLayer;

	// Token: 0x04003758 RID: 14168
	public ScenePartitionerLayer floorSwitchActivatorLayer;

	// Token: 0x04003759 RID: 14169
	public ScenePartitionerLayer floorSwitchActivatorChangedLayer;

	// Token: 0x0400375A RID: 14170
	public ScenePartitionerLayer collisionLayer;

	// Token: 0x0400375B RID: 14171
	public ScenePartitionerLayer lure;

	// Token: 0x0400375C RID: 14172
	public ScenePartitionerLayer plants;

	// Token: 0x0400375D RID: 14173
	public ScenePartitionerLayer plantsChangedLayer;

	// Token: 0x0400375E RID: 14174
	public ScenePartitionerLayer industrialBuildings;

	// Token: 0x0400375F RID: 14175
	public ScenePartitionerLayer completeBuildings;

	// Token: 0x04003760 RID: 14176
	public ScenePartitionerLayer prioritizableObjects;

	// Token: 0x04003761 RID: 14177
	public ScenePartitionerLayer contactConductiveLayer;

	// Token: 0x04003762 RID: 14178
	private ScenePartitioner partitioner;

	// Token: 0x04003763 RID: 14179
	private static GameScenePartitioner instance;

	// Token: 0x04003764 RID: 14180
	private KCompactedVector<ScenePartitionerEntry> scenePartitionerEntries = new KCompactedVector<ScenePartitionerEntry>(0);

	// Token: 0x04003765 RID: 14181
	private List<int> changedCells = new List<int>();

	// Token: 0x02001BFB RID: 7163
	public interface Iterator
	{
		// Token: 0x0600A950 RID: 43344
		void Iterate(object obj);

		// Token: 0x0600A951 RID: 43345
		void Cleanup();
	}
}
