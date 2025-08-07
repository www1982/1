using System;
using System.Collections.Generic;

// Token: 0x02000AF3 RID: 2803
public class ScenePartitioner : ISim1000ms
{
	// Token: 0x06005252 RID: 21074 RVA: 0x001DFA70 File Offset: 0x001DDC70
	public ScenePartitioner(int node_size, int layer_count, int scene_width, int scene_height)
	{
		this.nodeSize = node_size;
		int num = scene_width / node_size;
		int num2 = scene_height / node_size;
		this.nodes = new ScenePartitioner.ScenePartitionerNode[layer_count, num2, num];
		for (int i = 0; i < this.nodes.GetLength(0); i++)
		{
			for (int j = 0; j < this.nodes.GetLength(1); j++)
			{
				for (int k = 0; k < this.nodes.GetLength(2); k++)
				{
					this.nodes[i, j, k].entries = new List<ScenePartitionerEntry>();
					this.nodes[i, j, k].entries_set = new HashSet<ScenePartitionerEntry>();
				}
			}
		}
		SimAndRenderScheduler.instance.Add(this, false);
	}

	// Token: 0x06005253 RID: 21075 RVA: 0x001DFB4C File Offset: 0x001DDD4C
	public void FreeResources()
	{
		for (int i = 0; i < this.nodes.GetLength(0); i++)
		{
			for (int j = 0; j < this.nodes.GetLength(1); j++)
			{
				for (int k = 0; k < this.nodes.GetLength(2); k++)
				{
					foreach (ScenePartitionerEntry scenePartitionerEntry in this.nodes[i, j, k].entries)
					{
						if (scenePartitionerEntry != null)
						{
							scenePartitionerEntry.partitioner = null;
							scenePartitionerEntry.obj = null;
						}
					}
					foreach (ScenePartitionerEntry scenePartitionerEntry2 in this.nodes[i, j, k].entries_set)
					{
						if (scenePartitionerEntry2 != null)
						{
							scenePartitionerEntry2.partitioner = null;
							scenePartitionerEntry2.obj = null;
						}
					}
					this.nodes[i, j, k].entries.Clear();
					this.nodes[i, j, k].entries_set.Clear();
				}
			}
		}
		this.nodes = null;
	}

	// Token: 0x06005254 RID: 21076 RVA: 0x001DFCA8 File Offset: 0x001DDEA8
	[Obsolete]
	public ScenePartitionerLayer CreateMask(HashedString name)
	{
		foreach (ScenePartitionerLayer scenePartitionerLayer in this.layers)
		{
			if (scenePartitionerLayer.name == name)
			{
				return scenePartitionerLayer;
			}
		}
		ScenePartitionerLayer scenePartitionerLayer2 = new ScenePartitionerLayer(name, this.layers.Count);
		this.layers.Add(scenePartitionerLayer2);
		DebugUtil.Assert(this.layers.Count <= this.nodes.GetLength(0));
		return scenePartitionerLayer2;
	}

	// Token: 0x06005255 RID: 21077 RVA: 0x001DFD48 File Offset: 0x001DDF48
	public ScenePartitionerLayer CreateMask(string name)
	{
		foreach (ScenePartitionerLayer scenePartitionerLayer in this.layers)
		{
			if (scenePartitionerLayer.name == name)
			{
				return scenePartitionerLayer;
			}
		}
		HashCache.Get().Add(name);
		ScenePartitionerLayer scenePartitionerLayer2 = new ScenePartitionerLayer(name, this.layers.Count);
		this.layers.Add(scenePartitionerLayer2);
		DebugUtil.Assert(this.layers.Count <= this.nodes.GetLength(0));
		return scenePartitionerLayer2;
	}

	// Token: 0x06005256 RID: 21078 RVA: 0x001DFE00 File Offset: 0x001DE000
	private int ClampNodeX(int x)
	{
		return Math.Min(Math.Max(x, 0), this.nodes.GetLength(2) - 1);
	}

	// Token: 0x06005257 RID: 21079 RVA: 0x001DFE1C File Offset: 0x001DE01C
	private int ClampNodeY(int y)
	{
		return Math.Min(Math.Max(y, 0), this.nodes.GetLength(1) - 1);
	}

	// Token: 0x06005258 RID: 21080 RVA: 0x001DFE38 File Offset: 0x001DE038
	private Extents GetNodeExtents(int x, int y, int width, int height)
	{
		Extents extents = default(Extents);
		extents.x = this.ClampNodeX(x / this.nodeSize);
		extents.y = this.ClampNodeY(y / this.nodeSize);
		extents.width = 1 + this.ClampNodeX((x + width) / this.nodeSize) - extents.x;
		extents.height = 1 + this.ClampNodeY((y + height) / this.nodeSize) - extents.y;
		return extents;
	}

	// Token: 0x06005259 RID: 21081 RVA: 0x001DFEB9 File Offset: 0x001DE0B9
	private Extents GetNodeExtents(ScenePartitionerEntry entry)
	{
		return this.GetNodeExtents(entry.x, entry.y, entry.width, entry.height);
	}

	// Token: 0x0600525A RID: 21082 RVA: 0x001DFEDC File Offset: 0x001DE0DC
	private void Insert(ScenePartitionerEntry entry)
	{
		if (entry.obj == null)
		{
			Debug.LogWarning("Trying to put null go into scene partitioner");
			return;
		}
		Extents nodeExtents = this.GetNodeExtents(entry);
		if (nodeExtents.x + nodeExtents.width > this.nodes.GetLength(2))
		{
			Debug.LogError(string.Concat(new string[]
			{
				entry.obj.ToString(),
				" x/w ",
				nodeExtents.x.ToString(),
				"/",
				nodeExtents.width.ToString(),
				" < ",
				this.nodes.GetLength(2).ToString()
			}));
		}
		if (nodeExtents.y + nodeExtents.height > this.nodes.GetLength(1))
		{
			Debug.LogError(string.Concat(new string[]
			{
				entry.obj.ToString(),
				" y/h ",
				nodeExtents.y.ToString(),
				"/",
				nodeExtents.height.ToString(),
				" < ",
				this.nodes.GetLength(1).ToString()
			}));
		}
		int layer = entry.layer;
		for (int i = nodeExtents.y; i < nodeExtents.y + nodeExtents.height; i++)
		{
			for (int j = nodeExtents.x; j < nodeExtents.x + nodeExtents.width; j++)
			{
				if (!this.nodes[layer, i, j].dirty)
				{
					this.nodes[layer, i, j].dirty = true;
					this.dirtyNodes.Add(new ScenePartitioner.DirtyNode
					{
						layer = layer,
						x = j,
						y = i
					});
				}
				if (this.nodes[layer, i, j].entries_set.Add(entry))
				{
					this.nodes[layer, i, j].entries.Add(entry);
				}
			}
		}
	}

	// Token: 0x0600525B RID: 21083 RVA: 0x001E00F0 File Offset: 0x001DE2F0
	private void Widthdraw(ScenePartitionerEntry entry)
	{
		Extents nodeExtents = this.GetNodeExtents(entry);
		if (nodeExtents.x + nodeExtents.width > this.nodes.GetLength(2))
		{
			Debug.LogError(string.Concat(new string[]
			{
				" x/w ",
				nodeExtents.x.ToString(),
				"/",
				nodeExtents.width.ToString(),
				" < ",
				this.nodes.GetLength(2).ToString()
			}));
		}
		if (nodeExtents.y + nodeExtents.height > this.nodes.GetLength(1))
		{
			Debug.LogError(string.Concat(new string[]
			{
				" y/h ",
				nodeExtents.y.ToString(),
				"/",
				nodeExtents.height.ToString(),
				" < ",
				this.nodes.GetLength(1).ToString()
			}));
		}
		int layer = entry.layer;
		for (int i = nodeExtents.y; i < nodeExtents.y + nodeExtents.height; i++)
		{
			for (int j = nodeExtents.x; j < nodeExtents.x + nodeExtents.width; j++)
			{
				if (this.nodes[layer, i, j].entries_set.Remove(entry))
				{
					this.nodes[layer, i, j].entries.Remove(entry);
					if (!this.nodes[layer, i, j].dirty)
					{
						this.nodes[layer, i, j].dirty = true;
						this.dirtyNodes.Add(new ScenePartitioner.DirtyNode
						{
							layer = layer,
							x = j,
							y = i
						});
					}
				}
			}
		}
	}

	// Token: 0x0600525C RID: 21084 RVA: 0x001E02D6 File Offset: 0x001DE4D6
	public ScenePartitionerEntry Add(ScenePartitionerEntry entry)
	{
		this.Insert(entry);
		return entry;
	}

	// Token: 0x0600525D RID: 21085 RVA: 0x001E02E0 File Offset: 0x001DE4E0
	public void UpdatePosition(int x, int y, ScenePartitionerEntry entry)
	{
		this.Widthdraw(entry);
		entry.x = x;
		entry.y = y;
		this.Insert(entry);
	}

	// Token: 0x0600525E RID: 21086 RVA: 0x001E02FE File Offset: 0x001DE4FE
	public void UpdatePosition(Extents e, ScenePartitionerEntry entry)
	{
		this.Widthdraw(entry);
		entry.x = e.x;
		entry.y = e.y;
		entry.width = e.width;
		entry.height = e.height;
		this.Insert(entry);
	}

	// Token: 0x0600525F RID: 21087 RVA: 0x001E0340 File Offset: 0x001DE540
	public void Remove(ScenePartitionerEntry entry)
	{
		Extents nodeExtents = this.GetNodeExtents(entry);
		if (nodeExtents.x + nodeExtents.width > this.nodes.GetLength(2))
		{
			Debug.LogError(string.Concat(new string[]
			{
				" x/w ",
				nodeExtents.x.ToString(),
				"/",
				nodeExtents.width.ToString(),
				" < ",
				this.nodes.GetLength(2).ToString()
			}));
		}
		if (nodeExtents.y + nodeExtents.height > this.nodes.GetLength(1))
		{
			Debug.LogError(string.Concat(new string[]
			{
				" y/h ",
				nodeExtents.y.ToString(),
				"/",
				nodeExtents.height.ToString(),
				" < ",
				this.nodes.GetLength(1).ToString()
			}));
		}
		int layer = entry.layer;
		for (int i = nodeExtents.y; i < nodeExtents.y + nodeExtents.height; i++)
		{
			for (int j = nodeExtents.x; j < nodeExtents.x + nodeExtents.width; j++)
			{
				if (!this.nodes[layer, i, j].dirty)
				{
					this.nodes[layer, i, j].dirty = true;
					this.dirtyNodes.Add(new ScenePartitioner.DirtyNode
					{
						layer = layer,
						x = j,
						y = i
					});
				}
			}
		}
		entry.obj = null;
	}

	// Token: 0x06005260 RID: 21088 RVA: 0x001E04F0 File Offset: 0x001DE6F0
	public void Sim1000ms(float dt)
	{
		foreach (ScenePartitioner.DirtyNode dirtyNode in this.dirtyNodes)
		{
			this.nodes[dirtyNode.layer, dirtyNode.y, dirtyNode.x].entries_set.RemoveWhere(ScenePartitioner.removeCallback);
			List<ScenePartitionerEntry> entries = this.nodes[dirtyNode.layer, dirtyNode.y, dirtyNode.x].entries;
			for (int i = entries.Count - 1; i >= 0; i--)
			{
				if (ScenePartitioner.removeCallback(entries[i]))
				{
					entries.RemoveAt(i);
				}
			}
			this.nodes[dirtyNode.layer, dirtyNode.y, dirtyNode.x].dirty = false;
		}
		this.dirtyNodes.Clear();
	}

	// Token: 0x06005261 RID: 21089 RVA: 0x001E05F0 File Offset: 0x001DE7F0
	public void TriggerEvent(IEnumerable<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		ListPool<ScenePartitionerEntry, ScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, ScenePartitioner>.Allocate();
		this.queryId++;
		foreach (int num in cells)
		{
			int num2 = 0;
			int num3 = 0;
			Grid.CellToXY(num, out num2, out num3);
			this.GatherEntries(num2, num3, 1, 1, layer, event_data, pooledList, this.queryId);
		}
		this.RunLayerGlobalEvent(cells, layer, event_data);
		this.RunEntries(pooledList, event_data);
		pooledList.Recycle();
	}

	// Token: 0x06005262 RID: 21090 RVA: 0x001E067C File Offset: 0x001DE87C
	public void TriggerEvent(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data)
	{
		ListPool<ScenePartitionerEntry, ScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, ScenePartitioner>.Allocate();
		this.GatherEntries(x, y, width, height, layer, event_data, pooledList);
		this.RunLayerGlobalEvent(x, y, width, height, layer, event_data);
		this.RunEntries(pooledList, event_data);
		pooledList.Recycle();
	}

	// Token: 0x06005263 RID: 21091 RVA: 0x001E06C0 File Offset: 0x001DE8C0
	private void RunLayerGlobalEvent(IEnumerable<int> cells, ScenePartitionerLayer layer, object event_data)
	{
		if (layer.OnEvent != null)
		{
			foreach (int num in cells)
			{
				layer.OnEvent(num, event_data);
			}
		}
	}

	// Token: 0x06005264 RID: 21092 RVA: 0x001E0718 File Offset: 0x001DE918
	private void RunLayerGlobalEvent(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data)
	{
		if (layer.OnEvent != null)
		{
			for (int i = y; i < y + height; i++)
			{
				for (int j = x; j < x + width; j++)
				{
					int num = Grid.XYToCell(j, i);
					if (Grid.IsValidCell(num))
					{
						layer.OnEvent(num, event_data);
					}
				}
			}
		}
	}

	// Token: 0x06005265 RID: 21093 RVA: 0x001E076C File Offset: 0x001DE96C
	private void RunEntries(List<ScenePartitionerEntry> gathered_entries, object event_data)
	{
		for (int i = 0; i < gathered_entries.Count; i++)
		{
			ScenePartitionerEntry scenePartitionerEntry = gathered_entries[i];
			if (scenePartitionerEntry.obj != null && scenePartitionerEntry.eventCallback != null)
			{
				scenePartitionerEntry.eventCallback(event_data);
			}
		}
	}

	// Token: 0x06005266 RID: 21094 RVA: 0x001E07B0 File Offset: 0x001DE9B0
	public void GatherEntries(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data, List<ScenePartitionerEntry> gathered_entries)
	{
		int num = this.queryId + 1;
		this.queryId = num;
		this.GatherEntries(x, y, width, height, layer, event_data, gathered_entries, num);
	}

	// Token: 0x06005267 RID: 21095 RVA: 0x001E07E0 File Offset: 0x001DE9E0
	public void GatherEntries(int x, int y, int width, int height, ScenePartitionerLayer layer, object event_data, List<ScenePartitionerEntry> gathered_entries, int query_id)
	{
		Extents nodeExtents = this.GetNodeExtents(x, y, width, height);
		int num = Math.Min(nodeExtents.y + nodeExtents.height, this.nodes.GetLength(1));
		int num2 = Math.Max(nodeExtents.y, 0);
		int num3 = Math.Max(nodeExtents.x, 0);
		int num4 = Math.Min(nodeExtents.x + nodeExtents.width, this.nodes.GetLength(2));
		int layer2 = layer.layer;
		for (int i = num2; i < num; i++)
		{
			for (int j = num3; j < num4; j++)
			{
				ListPool<ScenePartitionerEntry, ScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, ScenePartitioner>.Allocate();
				foreach (ScenePartitionerEntry scenePartitionerEntry in this.nodes[layer2, i, j].entries)
				{
					if (scenePartitionerEntry != null && scenePartitionerEntry.queryId != this.queryId)
					{
						if (scenePartitionerEntry.obj == null)
						{
							pooledList.Add(scenePartitionerEntry);
						}
						else if (x + width - 1 >= scenePartitionerEntry.x && x <= scenePartitionerEntry.x + scenePartitionerEntry.width - 1 && y + height - 1 >= scenePartitionerEntry.y && y <= scenePartitionerEntry.y + scenePartitionerEntry.height - 1)
						{
							scenePartitionerEntry.queryId = this.queryId;
							gathered_entries.Add(scenePartitionerEntry);
						}
					}
				}
				this.nodes[layer2, i, j].entries_set.ExceptWith(pooledList);
				List<ScenePartitionerEntry> entries = this.nodes[layer2, i, j].entries;
				foreach (ScenePartitionerEntry scenePartitionerEntry2 in pooledList)
				{
					entries.Remove(scenePartitionerEntry2);
				}
				pooledList.Recycle();
			}
		}
	}

	// Token: 0x06005268 RID: 21096 RVA: 0x001E09E4 File Offset: 0x001DEBE4
	public IEnumerable<object> AsyncSafeEnumerate(int x, int y, int width, int height, ScenePartitionerLayer layer)
	{
		Extents nodeExtents = this.GetNodeExtents(x, y, width, height);
		int max_y = Math.Min(nodeExtents.y + nodeExtents.height, this.nodes.GetLength(1));
		int num = Math.Max(nodeExtents.y, 0);
		int start_x = Math.Max(nodeExtents.x, 0);
		int max_x = Math.Min(nodeExtents.x + nodeExtents.width, this.nodes.GetLength(2));
		int layer_idx = layer.layer;
		int num2;
		for (int node_y = num; node_y < max_y; node_y = num2)
		{
			for (int node_x = start_x; node_x < max_x; node_x = num2)
			{
				foreach (ScenePartitionerEntry scenePartitionerEntry in this.nodes[layer_idx, node_y, node_x].entries)
				{
					if (scenePartitionerEntry != null && scenePartitionerEntry.obj != null && x + width - 1 >= scenePartitionerEntry.x && x <= scenePartitionerEntry.x + scenePartitionerEntry.width - 1 && y + height - 1 >= scenePartitionerEntry.y && y <= scenePartitionerEntry.y + scenePartitionerEntry.height - 1)
					{
						yield return scenePartitionerEntry.obj;
					}
				}
				List<ScenePartitionerEntry>.Enumerator enumerator = default(List<ScenePartitionerEntry>.Enumerator);
				num2 = node_x + 1;
			}
			num2 = node_y + 1;
		}
		yield break;
		yield break;
	}

	// Token: 0x06005269 RID: 21097 RVA: 0x001E0A1C File Offset: 0x001DEC1C
	public void AsyncSafeVisit<ContextType>(int x, int y, int width, int height, ScenePartitionerLayer layer, Func<object, ContextType, bool> visitor, ContextType context)
	{
		Extents nodeExtents = this.GetNodeExtents(x, y, width, height);
		int num = Math.Min(nodeExtents.y + nodeExtents.height, this.nodes.GetLength(1));
		int num2 = Math.Max(nodeExtents.y, 0);
		int num3 = Math.Max(nodeExtents.x, 0);
		int num4 = Math.Min(nodeExtents.x + nodeExtents.width, this.nodes.GetLength(2));
		int layer2 = layer.layer;
		for (int i = num2; i < num; i++)
		{
			for (int j = num3; j < num4; j++)
			{
				foreach (ScenePartitionerEntry scenePartitionerEntry in this.nodes[layer2, i, j].entries)
				{
					if (scenePartitionerEntry != null && scenePartitionerEntry.obj != null && x + width - 1 >= scenePartitionerEntry.x && x <= scenePartitionerEntry.x + scenePartitionerEntry.width - 1 && y + height - 1 >= scenePartitionerEntry.y && y <= scenePartitionerEntry.y + scenePartitionerEntry.height - 1 && !visitor(scenePartitionerEntry.obj, context))
					{
						return;
					}
				}
			}
		}
	}

	// Token: 0x0600526A RID: 21098 RVA: 0x001E0B78 File Offset: 0x001DED78
	public void Cleanup()
	{
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x0600526B RID: 21099 RVA: 0x001E0B88 File Offset: 0x001DED88
	public bool DoDebugLayersContainItemsOnCell(int cell)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		List<ScenePartitionerEntry> list = new List<ScenePartitionerEntry>();
		foreach (ScenePartitionerLayer scenePartitionerLayer in this.toggledLayers)
		{
			list.Clear();
			GameScenePartitioner.Instance.GatherEntries(num, num2, 1, 1, scenePartitionerLayer, list);
			if (list.Count > 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04003766 RID: 14182
	public List<ScenePartitionerLayer> layers = new List<ScenePartitionerLayer>();

	// Token: 0x04003767 RID: 14183
	private int nodeSize;

	// Token: 0x04003768 RID: 14184
	private List<ScenePartitioner.DirtyNode> dirtyNodes = new List<ScenePartitioner.DirtyNode>();

	// Token: 0x04003769 RID: 14185
	private ScenePartitioner.ScenePartitionerNode[,,] nodes;

	// Token: 0x0400376A RID: 14186
	private int queryId;

	// Token: 0x0400376B RID: 14187
	private static readonly Predicate<ScenePartitionerEntry> removeCallback = (ScenePartitionerEntry entry) => entry == null || entry.obj == null;

	// Token: 0x0400376C RID: 14188
	public HashSet<ScenePartitionerLayer> toggledLayers = new HashSet<ScenePartitionerLayer>();

	// Token: 0x02001BFC RID: 7164
	private struct ScenePartitionerNode
	{
		// Token: 0x040084A6 RID: 33958
		public List<ScenePartitionerEntry> entries;

		// Token: 0x040084A7 RID: 33959
		public HashSet<ScenePartitionerEntry> entries_set;

		// Token: 0x040084A8 RID: 33960
		public bool dirty;
	}

	// Token: 0x02001BFD RID: 7165
	private struct DirtyNode
	{
		// Token: 0x040084A9 RID: 33961
		public int layer;

		// Token: 0x040084AA RID: 33962
		public int x;

		// Token: 0x040084AB RID: 33963
		public int y;
	}
}
