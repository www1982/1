using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020008F1 RID: 2289
[AddComponentMenu("KMonoBehaviour/scripts/EntombedItemManager")]
public class EntombedItemManager : KMonoBehaviour, ISim33ms
{
	// Token: 0x06003FE3 RID: 16355 RVA: 0x001668AB File Offset: 0x00164AAB
	[OnDeserialized]
	private void OnDeserialized()
	{
		this.SpawnUncoveredObjects();
		this.AddMassToWorldIfPossible();
		this.PopulateEntombedItemVisualizers();
	}

	// Token: 0x06003FE4 RID: 16356 RVA: 0x001668C0 File Offset: 0x00164AC0
	public static bool CanEntomb(Pickupable pickupable)
	{
		if (pickupable == null)
		{
			return false;
		}
		if (pickupable.storage != null)
		{
			return false;
		}
		int num = Grid.PosToCell(pickupable);
		return Grid.IsValidCell(num) && Grid.Solid[num] && !(Grid.Objects[num, 9] != null) && (pickupable.PrimaryElement.Element.IsSolid && pickupable.GetComponent<ElementChunk>() != null);
	}

	// Token: 0x06003FE5 RID: 16357 RVA: 0x00166942 File Offset: 0x00164B42
	public void Add(Pickupable pickupable)
	{
		this.pickupables.Add(pickupable);
	}

	// Token: 0x06003FE6 RID: 16358 RVA: 0x00166950 File Offset: 0x00164B50
	public void Sim33ms(float dt)
	{
		EntombedItemVisualizer component = Game.Instance.GetComponent<EntombedItemVisualizer>();
		HashSetPool<Pickupable, EntombedItemManager>.PooledHashSet pooledHashSet = HashSetPool<Pickupable, EntombedItemManager>.Allocate();
		foreach (Pickupable pickupable in this.pickupables)
		{
			if (EntombedItemManager.CanEntomb(pickupable))
			{
				pooledHashSet.Add(pickupable);
			}
		}
		this.pickupables.Clear();
		foreach (Pickupable pickupable2 in pooledHashSet)
		{
			int num = Grid.PosToCell(pickupable2);
			PrimaryElement primaryElement = pickupable2.PrimaryElement;
			SimHashes elementID = primaryElement.ElementID;
			float mass = primaryElement.Mass;
			float temperature = primaryElement.Temperature;
			byte diseaseIdx = primaryElement.DiseaseIdx;
			int diseaseCount = primaryElement.DiseaseCount;
			Element element = Grid.Element[num];
			if (elementID == element.id && mass > 0.010000001f && Grid.Mass[num] + mass < element.maxMass)
			{
				SimMessages.AddRemoveSubstance(num, ElementLoader.FindElementByHash(elementID).idx, CellEventLogger.Instance.ElementConsumerSimUpdate, mass, temperature, diseaseIdx, diseaseCount, true, -1);
			}
			else
			{
				component.AddItem(num);
				this.cells.Add(num);
				this.elementIds.Add((int)elementID);
				this.masses.Add(mass);
				this.temperatures.Add(temperature);
				this.diseaseIndices.Add(diseaseIdx);
				this.diseaseCounts.Add(diseaseCount);
			}
			Util.KDestroyGameObject(pickupable2.gameObject);
		}
		pooledHashSet.Recycle();
	}

	// Token: 0x06003FE7 RID: 16359 RVA: 0x00166B18 File Offset: 0x00164D18
	public void OnSolidChanged(List<int> solid_changed_cells)
	{
		ListPool<int, EntombedItemManager>.PooledList pooledList = ListPool<int, EntombedItemManager>.Allocate();
		foreach (int num in solid_changed_cells)
		{
			if (!Grid.Solid[num])
			{
				pooledList.Add(num);
			}
		}
		ListPool<int, EntombedItemManager>.PooledList pooledList2 = ListPool<int, EntombedItemManager>.Allocate();
		for (int i = 0; i < this.cells.Count; i++)
		{
			int num2 = this.cells[i];
			foreach (int num3 in pooledList)
			{
				if (num2 == num3)
				{
					pooledList2.Add(i);
					break;
				}
			}
		}
		pooledList.Recycle();
		this.SpawnObjects(pooledList2);
		pooledList2.Recycle();
	}

	// Token: 0x06003FE8 RID: 16360 RVA: 0x00166C04 File Offset: 0x00164E04
	private void SpawnUncoveredObjects()
	{
		ListPool<int, EntombedItemManager>.PooledList pooledList = ListPool<int, EntombedItemManager>.Allocate();
		for (int i = 0; i < this.cells.Count; i++)
		{
			int num = this.cells[i];
			if (!Grid.Solid[num])
			{
				pooledList.Add(i);
			}
		}
		this.SpawnObjects(pooledList);
		pooledList.Recycle();
	}

	// Token: 0x06003FE9 RID: 16361 RVA: 0x00166C5C File Offset: 0x00164E5C
	private void AddMassToWorldIfPossible()
	{
		ListPool<int, EntombedItemManager>.PooledList pooledList = ListPool<int, EntombedItemManager>.Allocate();
		for (int i = 0; i < this.cells.Count; i++)
		{
			int num = this.cells[i];
			if (Grid.Solid[num] && Grid.Element[num].id == (SimHashes)this.elementIds[i])
			{
				pooledList.Add(i);
			}
		}
		pooledList.Sort();
		pooledList.Reverse();
		foreach (int num2 in pooledList)
		{
			EntombedItemManager.Item item = this.GetItem(num2);
			this.RemoveItem(num2);
			if (item.mass > 1E-45f)
			{
				SimMessages.AddRemoveSubstance(item.cell, ElementLoader.FindElementByHash((SimHashes)item.elementId).idx, CellEventLogger.Instance.ElementConsumerSimUpdate, item.mass, item.temperature, item.diseaseIdx, item.diseaseCount, false, -1);
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06003FEA RID: 16362 RVA: 0x00166D74 File Offset: 0x00164F74
	private void RemoveItem(int item_idx)
	{
		this.cells.RemoveAt(item_idx);
		this.elementIds.RemoveAt(item_idx);
		this.masses.RemoveAt(item_idx);
		this.temperatures.RemoveAt(item_idx);
		this.diseaseIndices.RemoveAt(item_idx);
		this.diseaseCounts.RemoveAt(item_idx);
	}

	// Token: 0x06003FEB RID: 16363 RVA: 0x00166DCC File Offset: 0x00164FCC
	private EntombedItemManager.Item GetItem(int item_idx)
	{
		return new EntombedItemManager.Item
		{
			cell = this.cells[item_idx],
			elementId = this.elementIds[item_idx],
			mass = this.masses[item_idx],
			temperature = this.temperatures[item_idx],
			diseaseIdx = this.diseaseIndices[item_idx],
			diseaseCount = this.diseaseCounts[item_idx]
		};
	}

	// Token: 0x06003FEC RID: 16364 RVA: 0x00166E54 File Offset: 0x00165054
	private void SpawnObjects(List<int> uncovered_item_indices)
	{
		uncovered_item_indices.Sort();
		uncovered_item_indices.Reverse();
		EntombedItemVisualizer component = Game.Instance.GetComponent<EntombedItemVisualizer>();
		foreach (int num in uncovered_item_indices)
		{
			EntombedItemManager.Item item = this.GetItem(num);
			component.RemoveItem(item.cell);
			this.RemoveItem(num);
			Element element = ElementLoader.FindElementByHash((SimHashes)item.elementId);
			if (element != null)
			{
				element.substance.SpawnResource(Grid.CellToPosCCC(item.cell, Grid.SceneLayer.Ore), item.mass, item.temperature, item.diseaseIdx, item.diseaseCount, false, false, false);
			}
		}
	}

	// Token: 0x06003FED RID: 16365 RVA: 0x00166F14 File Offset: 0x00165114
	private void PopulateEntombedItemVisualizers()
	{
		EntombedItemVisualizer component = Game.Instance.GetComponent<EntombedItemVisualizer>();
		foreach (int num in this.cells)
		{
			component.AddItem(num);
		}
	}

	// Token: 0x040027A4 RID: 10148
	[Serialize]
	private List<int> cells = new List<int>();

	// Token: 0x040027A5 RID: 10149
	[Serialize]
	private List<int> elementIds = new List<int>();

	// Token: 0x040027A6 RID: 10150
	[Serialize]
	private List<float> masses = new List<float>();

	// Token: 0x040027A7 RID: 10151
	[Serialize]
	private List<float> temperatures = new List<float>();

	// Token: 0x040027A8 RID: 10152
	[Serialize]
	private List<byte> diseaseIndices = new List<byte>();

	// Token: 0x040027A9 RID: 10153
	[Serialize]
	private List<int> diseaseCounts = new List<int>();

	// Token: 0x040027AA RID: 10154
	private List<Pickupable> pickupables = new List<Pickupable>();

	// Token: 0x0200189B RID: 6299
	private struct Item
	{
		// Token: 0x0400796C RID: 31084
		public int cell;

		// Token: 0x0400796D RID: 31085
		public int elementId;

		// Token: 0x0400796E RID: 31086
		public float mass;

		// Token: 0x0400796F RID: 31087
		public float temperature;

		// Token: 0x04007970 RID: 31088
		public byte diseaseIdx;

		// Token: 0x04007971 RID: 31089
		public int diseaseCount;
	}
}
