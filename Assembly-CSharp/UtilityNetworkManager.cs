using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BD3 RID: 3027
public class UtilityNetworkManager<NetworkType, ItemType> : IUtilityNetworkMgr where NetworkType : UtilityNetwork, new() where ItemType : MonoBehaviour
{
	// Token: 0x17000690 RID: 1680
	// (get) Token: 0x06005A9A RID: 23194 RVA: 0x0020B63A File Offset: 0x0020983A
	public bool IsDirty
	{
		get
		{
			return this.dirty;
		}
	}

	// Token: 0x06005A9B RID: 23195 RVA: 0x0020B644 File Offset: 0x00209844
	public UtilityNetworkManager(int game_width, int game_height, int tile_layer)
	{
		this.tileLayer = tile_layer;
		this.networks = new List<UtilityNetwork>();
		this.Initialize(game_width, game_height);
	}

	// Token: 0x06005A9C RID: 23196 RVA: 0x0020B6D0 File Offset: 0x002098D0
	public void Initialize(int game_width, int game_height)
	{
		this.networks.Clear();
		this.physicalGrid = new UtilityNetworkGridNode[game_width * game_height];
		this.visualGrid = new UtilityNetworkGridNode[game_width * game_height];
		this.stashedVisualGrid = new UtilityNetworkGridNode[game_width * game_height];
		this.physicalNodes = new HashSet<int>();
		this.visualNodes = new HashSet<int>();
		this.visitedCells = new HashSet<int>();
		this.visitedVirtualKeys = new HashSet<object>();
		this.queuedVirtualKeys = new HashSet<object>();
		for (int i = 0; i < this.visualGrid.Length; i++)
		{
			this.visualGrid[i] = new UtilityNetworkGridNode
			{
				networkIdx = -1,
				connections = (UtilityConnections)0
			};
			this.physicalGrid[i] = new UtilityNetworkGridNode
			{
				networkIdx = -1,
				connections = (UtilityConnections)0
			};
		}
	}

	// Token: 0x06005A9D RID: 23197 RVA: 0x0020B7A8 File Offset: 0x002099A8
	public void Update()
	{
		if (this.dirty)
		{
			this.dirty = false;
			for (int i = 0; i < this.networks.Count; i++)
			{
				this.networks[i].Reset(this.physicalGrid);
			}
			this.networks.Clear();
			this.virtualKeyToNetworkIdx.Clear();
			this.RebuildNetworks(this.tileLayer, false);
			this.RebuildNetworks(this.tileLayer, true);
			if (this.onNetworksRebuilt != null)
			{
				this.onNetworksRebuilt(this.networks, this.GetNodes(true));
			}
		}
	}

	// Token: 0x06005A9E RID: 23198 RVA: 0x0020B844 File Offset: 0x00209A44
	protected UtilityNetworkGridNode[] GetGrid(bool is_physical_building)
	{
		if (!is_physical_building)
		{
			return this.visualGrid;
		}
		return this.physicalGrid;
	}

	// Token: 0x06005A9F RID: 23199 RVA: 0x0020B856 File Offset: 0x00209A56
	private HashSet<int> GetNodes(bool is_physical_building)
	{
		if (!is_physical_building)
		{
			return this.visualNodes;
		}
		return this.physicalNodes;
	}

	// Token: 0x06005AA0 RID: 23200 RVA: 0x0020B868 File Offset: 0x00209A68
	public void ClearCell(int cell, bool is_physical_building)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		UtilityNetworkGridNode[] grid = this.GetGrid(is_physical_building);
		HashSet<int> nodes = this.GetNodes(is_physical_building);
		UtilityConnections connections = grid[cell].connections;
		grid[cell].connections = (UtilityConnections)0;
		Vector2I vector2I = Grid.CellToXY(cell);
		if (vector2I.x > 0 && (connections & UtilityConnections.Left) != (UtilityConnections)0)
		{
			UtilityNetworkGridNode[] array = grid;
			int num = Grid.CellLeft(cell);
			array[num].connections = array[num].connections & ~UtilityConnections.Right;
		}
		if (vector2I.x < Grid.WidthInCells - 1 && (connections & UtilityConnections.Right) != (UtilityConnections)0)
		{
			UtilityNetworkGridNode[] array2 = grid;
			int num2 = Grid.CellRight(cell);
			array2[num2].connections = array2[num2].connections & ~UtilityConnections.Left;
		}
		if (vector2I.y > 0 && (connections & UtilityConnections.Down) != (UtilityConnections)0)
		{
			UtilityNetworkGridNode[] array3 = grid;
			int num3 = Grid.CellBelow(cell);
			array3[num3].connections = array3[num3].connections & ~UtilityConnections.Up;
		}
		if (vector2I.y < Grid.HeightInCells - 1 && (connections & UtilityConnections.Up) != (UtilityConnections)0)
		{
			UtilityNetworkGridNode[] array4 = grid;
			int num4 = Grid.CellAbove(cell);
			array4[num4].connections = array4[num4].connections & ~UtilityConnections.Down;
		}
		nodes.Remove(cell);
		if (is_physical_building)
		{
			this.dirty = true;
			this.ClearCell(cell, false);
		}
	}

	// Token: 0x06005AA1 RID: 23201 RVA: 0x0020B968 File Offset: 0x00209B68
	private void QueueCellForVisit(UtilityNetworkGridNode[] grid, int dest_cell, UtilityConnections direction)
	{
		if (!Grid.IsValidCell(dest_cell))
		{
			return;
		}
		if (this.visitedCells.Contains(dest_cell))
		{
			return;
		}
		if (direction != (UtilityConnections)0 && (grid[dest_cell].connections & direction.InverseDirection()) == (UtilityConnections)0)
		{
			return;
		}
		if (Grid.Objects[dest_cell, this.tileLayer] != null)
		{
			this.visitedCells.Add(dest_cell);
			this.queued.Enqueue(dest_cell);
		}
	}

	// Token: 0x06005AA2 RID: 23202 RVA: 0x0020B9D8 File Offset: 0x00209BD8
	public void ForceRebuildNetworks()
	{
		this.dirty = true;
	}

	// Token: 0x06005AA3 RID: 23203 RVA: 0x0020B9E4 File Offset: 0x00209BE4
	public void AddToNetworks(int cell, object item, bool is_endpoint)
	{
		if (item != null)
		{
			if (is_endpoint)
			{
				if (this.endpoints.ContainsKey(cell))
				{
					global::Debug.LogWarning(string.Format("Cell {0} already has a utility network endpoint assigned. Adding {1} will stomp previous endpoint, destroying the object that's already there.", cell, item.ToString()));
					KMonoBehaviour kmonoBehaviour = this.endpoints[cell] as KMonoBehaviour;
					if (kmonoBehaviour != null)
					{
						Util.KDestroyGameObject(kmonoBehaviour);
					}
				}
				this.endpoints[cell] = item;
			}
			else
			{
				if (this.items.ContainsKey(cell))
				{
					global::Debug.LogWarning(string.Format("Cell {0} already has a utility network connector assigned. Adding {1} will stomp previous item, destroying the object that's already there.", cell, item.ToString()));
					KMonoBehaviour kmonoBehaviour2 = this.items[cell] as KMonoBehaviour;
					if (kmonoBehaviour2 != null)
					{
						Util.KDestroyGameObject(kmonoBehaviour2);
					}
				}
				this.items[cell] = item;
			}
		}
		this.dirty = true;
	}

	// Token: 0x06005AA4 RID: 23204 RVA: 0x0020BAB4 File Offset: 0x00209CB4
	public void AddToVirtualNetworks(object key, object item, bool is_endpoint)
	{
		if (item != null)
		{
			if (is_endpoint)
			{
				if (!this.virtualEndpoints.ContainsKey(key))
				{
					this.virtualEndpoints[key] = new List<object>();
				}
				this.virtualEndpoints[key].Add(item);
			}
			else
			{
				if (!this.virtualItems.ContainsKey(key))
				{
					this.virtualItems[key] = new List<object>();
				}
				this.virtualItems[key].Add(item);
			}
		}
		this.dirty = true;
	}

	// Token: 0x06005AA5 RID: 23205 RVA: 0x0020BB34 File Offset: 0x00209D34
	private unsafe void Reconnect(int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		int* ptr = stackalloc int[(UIntPtr)16];
		int* ptr2 = stackalloc int[(UIntPtr)16];
		int* ptr3 = stackalloc int[(UIntPtr)16];
		int num = 0;
		if (vector2I.y < Grid.HeightInCells - 1)
		{
			ptr[num] = Grid.CellAbove(cell);
			ptr2[num] = 4;
			ptr3[num] = 8;
			num++;
		}
		if (vector2I.y > 0)
		{
			ptr[num] = Grid.CellBelow(cell);
			ptr2[num] = 8;
			ptr3[num] = 4;
			num++;
		}
		if (vector2I.x > 0)
		{
			ptr[num] = Grid.CellLeft(cell);
			ptr2[num] = 1;
			ptr3[num] = 2;
			num++;
		}
		if (vector2I.x < Grid.WidthInCells - 1)
		{
			ptr[num] = Grid.CellRight(cell);
			ptr2[num] = 2;
			ptr3[num] = 1;
			num++;
		}
		UtilityConnections connections = this.physicalGrid[cell].connections;
		UtilityConnections connections2 = this.visualGrid[cell].connections;
		for (int i = 0; i < num; i++)
		{
			int num2 = ptr[i];
			UtilityConnections utilityConnections = (UtilityConnections)ptr2[i];
			UtilityConnections utilityConnections2 = (UtilityConnections)ptr3[i];
			if ((connections & utilityConnections) != (UtilityConnections)0)
			{
				if (this.physicalNodes.Contains(num2))
				{
					UtilityNetworkGridNode[] array = this.physicalGrid;
					int num3 = num2;
					array[num3].connections = array[num3].connections | utilityConnections2;
				}
				if (this.visualNodes.Contains(num2))
				{
					UtilityNetworkGridNode[] array2 = this.visualGrid;
					int num4 = num2;
					array2[num4].connections = array2[num4].connections | utilityConnections2;
				}
			}
			else if ((connections2 & utilityConnections) != (UtilityConnections)0 && (this.physicalNodes.Contains(num2) || this.visualNodes.Contains(num2)))
			{
				UtilityNetworkGridNode[] array3 = this.visualGrid;
				int num5 = num2;
				array3[num5].connections = array3[num5].connections | utilityConnections2;
			}
		}
	}

	// Token: 0x06005AA6 RID: 23206 RVA: 0x0020BD14 File Offset: 0x00209F14
	public void RemoveFromVirtualNetworks(object key, object item, bool is_endpoint)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.dirty = true;
		if (item != null)
		{
			if (is_endpoint)
			{
				this.virtualEndpoints[key].Remove(item);
				if (this.virtualEndpoints[key].Count == 0)
				{
					this.virtualEndpoints.Remove(key);
				}
			}
			else
			{
				this.virtualItems[key].Remove(item);
				if (this.virtualItems[key].Count == 0)
				{
					this.virtualItems.Remove(key);
				}
			}
			UtilityNetwork networkForVirtualKey = this.GetNetworkForVirtualKey(key);
			if (networkForVirtualKey != null)
			{
				networkForVirtualKey.RemoveItem(item);
			}
		}
	}

	// Token: 0x06005AA7 RID: 23207 RVA: 0x0020BDB0 File Offset: 0x00209FB0
	public void RemoveFromNetworks(int cell, object item, bool is_endpoint)
	{
		if (Game.IsQuitting())
		{
			return;
		}
		this.dirty = true;
		if (item != null)
		{
			if (is_endpoint)
			{
				this.endpoints.Remove(cell);
				int networkIdx = this.physicalGrid[cell].networkIdx;
				if (networkIdx != -1)
				{
					this.networks[networkIdx].RemoveItem(item);
					return;
				}
			}
			else
			{
				int networkIdx2 = this.physicalGrid[cell].networkIdx;
				this.physicalGrid[cell].connections = (UtilityConnections)0;
				this.physicalGrid[cell].networkIdx = -1;
				this.items.Remove(cell);
				this.Disconnect(cell);
				object obj;
				if (this.endpoints.TryGetValue(cell, out obj) && networkIdx2 != -1)
				{
					this.networks[networkIdx2].DisconnectItem(obj);
				}
			}
		}
	}

	// Token: 0x06005AA8 RID: 23208 RVA: 0x0020BE80 File Offset: 0x0020A080
	private unsafe void Disconnect(int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		int num = 0;
		int* ptr = stackalloc int[(UIntPtr)16];
		int* ptr2 = stackalloc int[(UIntPtr)16];
		if (vector2I.y < Grid.HeightInCells - 1)
		{
			ptr[num] = Grid.CellAbove(cell);
			ptr2[num] = -9;
			num++;
		}
		if (vector2I.y > 0)
		{
			ptr[num] = Grid.CellBelow(cell);
			ptr2[num] = -5;
			num++;
		}
		if (vector2I.x > 0)
		{
			ptr[num] = Grid.CellLeft(cell);
			ptr2[num] = -3;
			num++;
		}
		if (vector2I.x < Grid.WidthInCells - 1)
		{
			ptr[num] = Grid.CellRight(cell);
			ptr2[num] = -2;
			num++;
		}
		for (int i = 0; i < num; i++)
		{
			int num2 = ptr[i];
			int num3 = ptr2[i];
			int num4 = (int)(this.physicalGrid[num2].connections & (UtilityConnections)num3);
			this.physicalGrid[num2].connections = (UtilityConnections)num4;
		}
	}

	// Token: 0x06005AA9 RID: 23209 RVA: 0x0020BF8C File Offset: 0x0020A18C
	private unsafe void RebuildNetworks(int layer, bool is_physical)
	{
		UtilityNetworkGridNode[] grid = this.GetGrid(is_physical);
		HashSet<int> nodes = this.GetNodes(is_physical);
		this.visitedCells.Clear();
		this.visitedVirtualKeys.Clear();
		this.queuedVirtualKeys.Clear();
		this.queued.Clear();
		int* ptr = stackalloc int[(UIntPtr)16];
		int* ptr2 = stackalloc int[(UIntPtr)16];
		foreach (int num in nodes)
		{
			UtilityNetworkGridNode utilityNetworkGridNode = grid[num];
			if (!this.visitedCells.Contains(num))
			{
				this.queued.Enqueue(num);
				this.visitedCells.Add(num);
				NetworkType networkType = new NetworkType();
				networkType.id = this.networks.Count;
				this.networks.Add(networkType);
				while (this.queued.Count > 0)
				{
					int num2 = this.queued.Dequeue();
					utilityNetworkGridNode = grid[num2];
					object obj = null;
					object obj2 = null;
					if (is_physical)
					{
						if (this.items.TryGetValue(num2, out obj))
						{
							if (obj is IDisconnectable && (obj as IDisconnectable).IsDisconnected())
							{
								continue;
							}
							if (obj != null)
							{
								networkType.AddItem(obj);
							}
						}
						if (this.endpoints.TryGetValue(num2, out obj2) && obj2 != null)
						{
							networkType.AddItem(obj2);
						}
					}
					grid[num2].networkIdx = networkType.id;
					if (obj != null && obj2 != null)
					{
						networkType.ConnectItem(obj2);
					}
					Vector2I vector2I = Grid.CellToXY(num2);
					int num3 = 0;
					if (vector2I.x > 0)
					{
						ptr[num3] = Grid.CellLeft(num2);
						ptr2[num3] = 1;
						num3++;
					}
					if (vector2I.x < Grid.WidthInCells - 1)
					{
						ptr[num3] = Grid.CellRight(num2);
						ptr2[num3] = 2;
						num3++;
					}
					if (vector2I.y > 0)
					{
						ptr[num3] = Grid.CellBelow(num2);
						ptr2[num3] = 8;
						num3++;
					}
					if (vector2I.y < Grid.HeightInCells - 1)
					{
						ptr[num3] = Grid.CellAbove(num2);
						ptr2[num3] = 4;
						num3++;
					}
					for (int i = 0; i < num3; i++)
					{
						int num4 = ptr2[i];
						if ((utilityNetworkGridNode.connections & (UtilityConnections)num4) != (UtilityConnections)0)
						{
							int num5 = ptr[i];
							this.QueueCellForVisit(grid, num5, (UtilityConnections)num4);
						}
					}
					int num6;
					if (this.links.TryGetValue(num2, out num6))
					{
						this.QueueCellForVisit(grid, num6, (UtilityConnections)0);
					}
					object obj3;
					if (this.semiVirtualLinks.TryGetValue(num2, out obj3) && !this.visitedVirtualKeys.Contains(obj3))
					{
						this.visitedVirtualKeys.Add(obj3);
						this.virtualKeyToNetworkIdx[obj3] = networkType.id;
						if (this.virtualItems.ContainsKey(obj3))
						{
							foreach (object obj4 in this.virtualItems[obj3])
							{
								networkType.AddItem(obj4);
								networkType.ConnectItem(obj4);
							}
						}
						if (this.virtualEndpoints.ContainsKey(obj3))
						{
							foreach (object obj5 in this.virtualEndpoints[obj3])
							{
								networkType.AddItem(obj5);
								networkType.ConnectItem(obj5);
							}
						}
						foreach (KeyValuePair<int, object> keyValuePair in this.semiVirtualLinks)
						{
							if (keyValuePair.Value == obj3)
							{
								this.QueueCellForVisit(grid, keyValuePair.Key, (UtilityConnections)0);
							}
						}
					}
				}
			}
		}
		foreach (KeyValuePair<object, List<object>> keyValuePair2 in this.virtualItems)
		{
			if (!this.visitedVirtualKeys.Contains(keyValuePair2.Key))
			{
				NetworkType networkType2 = new NetworkType();
				networkType2.id = this.networks.Count;
				this.visitedVirtualKeys.Add(keyValuePair2.Key);
				this.virtualKeyToNetworkIdx[keyValuePair2.Key] = networkType2.id;
				foreach (object obj6 in keyValuePair2.Value)
				{
					networkType2.AddItem(obj6);
					networkType2.ConnectItem(obj6);
				}
				foreach (object obj7 in this.virtualEndpoints[keyValuePair2.Key])
				{
					networkType2.AddItem(obj7);
					networkType2.ConnectItem(obj7);
				}
				this.networks.Add(networkType2);
			}
		}
		foreach (KeyValuePair<object, List<object>> keyValuePair3 in this.virtualEndpoints)
		{
			if (!this.visitedVirtualKeys.Contains(keyValuePair3.Key))
			{
				NetworkType networkType3 = new NetworkType();
				networkType3.id = this.networks.Count;
				this.visitedVirtualKeys.Add(keyValuePair3.Key);
				this.virtualKeyToNetworkIdx[keyValuePair3.Key] = networkType3.id;
				foreach (object obj8 in this.virtualEndpoints[keyValuePair3.Key])
				{
					networkType3.AddItem(obj8);
					networkType3.ConnectItem(obj8);
				}
				this.networks.Add(networkType3);
			}
		}
	}

	// Token: 0x06005AAA RID: 23210 RVA: 0x0020C6E0 File Offset: 0x0020A8E0
	public UtilityNetwork GetNetworkForVirtualKey(object key)
	{
		int num;
		if (this.virtualKeyToNetworkIdx.TryGetValue(key, out num))
		{
			return this.networks[num];
		}
		return null;
	}

	// Token: 0x06005AAB RID: 23211 RVA: 0x0020C70C File Offset: 0x0020A90C
	public UtilityNetwork GetNetworkByID(int id)
	{
		UtilityNetwork utilityNetwork = null;
		if (0 <= id && id < this.networks.Count)
		{
			utilityNetwork = this.networks[id];
		}
		return utilityNetwork;
	}

	// Token: 0x06005AAC RID: 23212 RVA: 0x0020C73C File Offset: 0x0020A93C
	public UtilityNetwork GetNetworkForCell(int cell)
	{
		UtilityNetwork utilityNetwork = null;
		if (Grid.IsValidCell(cell) && 0 <= this.physicalGrid[cell].networkIdx && this.physicalGrid[cell].networkIdx < this.networks.Count)
		{
			utilityNetwork = this.networks[this.physicalGrid[cell].networkIdx];
		}
		return utilityNetwork;
	}

	// Token: 0x06005AAD RID: 23213 RVA: 0x0020C7A4 File Offset: 0x0020A9A4
	public UtilityNetwork GetNetworkForDirection(int cell, Direction direction)
	{
		cell = Grid.GetCellInDirection(cell, direction);
		if (!Grid.IsValidCell(cell))
		{
			return null;
		}
		UtilityNetworkGridNode utilityNetworkGridNode = this.GetGrid(true)[cell];
		UtilityNetwork utilityNetwork = null;
		if (utilityNetworkGridNode.networkIdx != -1 && utilityNetworkGridNode.networkIdx < this.networks.Count)
		{
			utilityNetwork = this.networks[utilityNetworkGridNode.networkIdx];
		}
		return utilityNetwork;
	}

	// Token: 0x06005AAE RID: 23214 RVA: 0x0020C804 File Offset: 0x0020AA04
	private UtilityConnections GetNeighboursAsConnections(int cell, HashSet<int> nodes)
	{
		UtilityConnections utilityConnections = (UtilityConnections)0;
		Vector2I vector2I = Grid.CellToXY(cell);
		if (vector2I.x > 0 && nodes.Contains(Grid.CellLeft(cell)))
		{
			utilityConnections |= UtilityConnections.Left;
		}
		if (vector2I.x < Grid.WidthInCells - 1 && nodes.Contains(Grid.CellRight(cell)))
		{
			utilityConnections |= UtilityConnections.Right;
		}
		if (vector2I.y > 0 && nodes.Contains(Grid.CellBelow(cell)))
		{
			utilityConnections |= UtilityConnections.Down;
		}
		if (vector2I.y < Grid.HeightInCells - 1 && nodes.Contains(Grid.CellAbove(cell)))
		{
			utilityConnections |= UtilityConnections.Up;
		}
		return utilityConnections;
	}

	// Token: 0x06005AAF RID: 23215 RVA: 0x0020C894 File Offset: 0x0020AA94
	public virtual void SetConnections(UtilityConnections connections, int cell, bool is_physical_building)
	{
		HashSet<int> nodes = this.GetNodes(is_physical_building);
		nodes.Add(cell);
		this.visualGrid[cell].connections = connections;
		if (is_physical_building)
		{
			this.dirty = true;
			UtilityConnections utilityConnections = (is_physical_building ? (connections & this.GetNeighboursAsConnections(cell, nodes)) : connections);
			this.physicalGrid[cell].connections = utilityConnections;
		}
		this.Reconnect(cell);
	}

	// Token: 0x06005AB0 RID: 23216 RVA: 0x0020C8F8 File Offset: 0x0020AAF8
	public UtilityConnections GetConnections(int cell, bool is_physical_building)
	{
		UtilityNetworkGridNode[] array = this.GetGrid(is_physical_building);
		UtilityConnections utilityConnections = array[cell].connections;
		if (!is_physical_building)
		{
			array = this.GetGrid(true);
			utilityConnections |= array[cell].connections;
		}
		return utilityConnections;
	}

	// Token: 0x06005AB1 RID: 23217 RVA: 0x0020C938 File Offset: 0x0020AB38
	public UtilityConnections GetDisplayConnections(int cell)
	{
		UtilityConnections utilityConnections = (UtilityConnections)0;
		UtilityNetworkGridNode[] array = this.GetGrid(false);
		UtilityConnections utilityConnections2 = utilityConnections | array[cell].connections;
		array = this.GetGrid(true);
		return utilityConnections2 | array[cell].connections;
	}

	// Token: 0x06005AB2 RID: 23218 RVA: 0x0020C970 File Offset: 0x0020AB70
	public virtual bool CanAddConnection(UtilityConnections new_connection, int cell, bool is_physical_building, out string fail_reason)
	{
		fail_reason = null;
		return true;
	}

	// Token: 0x06005AB3 RID: 23219 RVA: 0x0020C978 File Offset: 0x0020AB78
	public void AddConnection(UtilityConnections new_connection, int cell, bool is_physical_building)
	{
		string text;
		if (this.CanAddConnection(new_connection, cell, is_physical_building, out text))
		{
			if (is_physical_building)
			{
				this.dirty = true;
			}
			UtilityNetworkGridNode[] grid = this.GetGrid(is_physical_building);
			UtilityConnections connections = grid[cell].connections;
			grid[cell].connections = connections | new_connection;
		}
	}

	// Token: 0x06005AB4 RID: 23220 RVA: 0x0020C9BE File Offset: 0x0020ABBE
	public void StashVisualGrids()
	{
		Array.Copy(this.visualGrid, this.stashedVisualGrid, this.visualGrid.Length);
	}

	// Token: 0x06005AB5 RID: 23221 RVA: 0x0020C9D9 File Offset: 0x0020ABD9
	public void UnstashVisualGrids()
	{
		Array.Copy(this.stashedVisualGrid, this.visualGrid, this.visualGrid.Length);
	}

	// Token: 0x06005AB6 RID: 23222 RVA: 0x0020C9F4 File Offset: 0x0020ABF4
	public string GetVisualizerString(int cell)
	{
		UtilityConnections displayConnections = this.GetDisplayConnections(cell);
		return this.GetVisualizerString(displayConnections);
	}

	// Token: 0x06005AB7 RID: 23223 RVA: 0x0020CA10 File Offset: 0x0020AC10
	public string GetVisualizerString(UtilityConnections connections)
	{
		string text = "";
		if ((connections & UtilityConnections.Left) != (UtilityConnections)0)
		{
			text += "L";
		}
		if ((connections & UtilityConnections.Right) != (UtilityConnections)0)
		{
			text += "R";
		}
		if ((connections & UtilityConnections.Up) != (UtilityConnections)0)
		{
			text += "U";
		}
		if ((connections & UtilityConnections.Down) != (UtilityConnections)0)
		{
			text += "D";
		}
		if (text == "")
		{
			text = "None";
		}
		return text;
	}

	// Token: 0x06005AB8 RID: 23224 RVA: 0x0020CA7C File Offset: 0x0020AC7C
	public object GetEndpoint(int cell)
	{
		object obj = null;
		this.endpoints.TryGetValue(cell, out obj);
		return obj;
	}

	// Token: 0x06005AB9 RID: 23225 RVA: 0x0020CA9B File Offset: 0x0020AC9B
	public void AddSemiVirtualLink(int cell1, object virtualKey)
	{
		global::Debug.Assert(virtualKey != null, "Can not use a null key for a virtual network");
		this.semiVirtualLinks[cell1] = virtualKey;
		this.dirty = true;
	}

	// Token: 0x06005ABA RID: 23226 RVA: 0x0020CABF File Offset: 0x0020ACBF
	public void RemoveSemiVirtualLink(int cell1, object virtualKey)
	{
		global::Debug.Assert(virtualKey != null, "Can not use a null key for a virtual network");
		this.semiVirtualLinks.Remove(cell1);
		this.dirty = true;
	}

	// Token: 0x06005ABB RID: 23227 RVA: 0x0020CAE3 File Offset: 0x0020ACE3
	public void AddLink(int cell1, int cell2)
	{
		this.links[cell1] = cell2;
		this.links[cell2] = cell1;
		this.dirty = true;
	}

	// Token: 0x06005ABC RID: 23228 RVA: 0x0020CB06 File Offset: 0x0020AD06
	public void RemoveLink(int cell1, int cell2)
	{
		this.links.Remove(cell1);
		this.links.Remove(cell2);
		this.dirty = true;
	}

	// Token: 0x06005ABD RID: 23229 RVA: 0x0020CB29 File Offset: 0x0020AD29
	public void AddNetworksRebuiltListener(Action<IList<UtilityNetwork>, ICollection<int>> listener)
	{
		this.onNetworksRebuilt = (Action<IList<UtilityNetwork>, ICollection<int>>)Delegate.Combine(this.onNetworksRebuilt, listener);
	}

	// Token: 0x06005ABE RID: 23230 RVA: 0x0020CB42 File Offset: 0x0020AD42
	public void RemoveNetworksRebuiltListener(Action<IList<UtilityNetwork>, ICollection<int>> listener)
	{
		this.onNetworksRebuilt = (Action<IList<UtilityNetwork>, ICollection<int>>)Delegate.Remove(this.onNetworksRebuilt, listener);
	}

	// Token: 0x06005ABF RID: 23231 RVA: 0x0020CB5B File Offset: 0x0020AD5B
	public IList<UtilityNetwork> GetNetworks()
	{
		return this.networks;
	}

	// Token: 0x04003C15 RID: 15381
	private Dictionary<int, object> items = new Dictionary<int, object>();

	// Token: 0x04003C16 RID: 15382
	private Dictionary<int, object> endpoints = new Dictionary<int, object>();

	// Token: 0x04003C17 RID: 15383
	private Dictionary<object, List<object>> virtualItems = new Dictionary<object, List<object>>();

	// Token: 0x04003C18 RID: 15384
	private Dictionary<object, List<object>> virtualEndpoints = new Dictionary<object, List<object>>();

	// Token: 0x04003C19 RID: 15385
	private Dictionary<int, int> links = new Dictionary<int, int>();

	// Token: 0x04003C1A RID: 15386
	private Dictionary<int, object> semiVirtualLinks = new Dictionary<int, object>();

	// Token: 0x04003C1B RID: 15387
	private List<UtilityNetwork> networks;

	// Token: 0x04003C1C RID: 15388
	private Dictionary<object, int> virtualKeyToNetworkIdx = new Dictionary<object, int>();

	// Token: 0x04003C1D RID: 15389
	private HashSet<int> visitedCells;

	// Token: 0x04003C1E RID: 15390
	private HashSet<object> visitedVirtualKeys;

	// Token: 0x04003C1F RID: 15391
	private HashSet<object> queuedVirtualKeys;

	// Token: 0x04003C20 RID: 15392
	private Action<IList<UtilityNetwork>, ICollection<int>> onNetworksRebuilt;

	// Token: 0x04003C21 RID: 15393
	private Queue<int> queued = new Queue<int>();

	// Token: 0x04003C22 RID: 15394
	protected UtilityNetworkGridNode[] visualGrid;

	// Token: 0x04003C23 RID: 15395
	private UtilityNetworkGridNode[] stashedVisualGrid;

	// Token: 0x04003C24 RID: 15396
	protected UtilityNetworkGridNode[] physicalGrid;

	// Token: 0x04003C25 RID: 15397
	protected HashSet<int> physicalNodes;

	// Token: 0x04003C26 RID: 15398
	protected HashSet<int> visualNodes;

	// Token: 0x04003C27 RID: 15399
	private bool dirty;

	// Token: 0x04003C28 RID: 15400
	private int tileLayer = -1;
}
