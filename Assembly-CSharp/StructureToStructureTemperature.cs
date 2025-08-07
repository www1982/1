using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BA9 RID: 2985
public class StructureToStructureTemperature : KMonoBehaviour
{
	// Token: 0x06005931 RID: 22833 RVA: 0x00203990 File Offset: 0x00201B90
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<StructureToStructureTemperature>(-1555603773, StructureToStructureTemperature.OnStructureTemperatureRegisteredDelegate);
	}

	// Token: 0x06005932 RID: 22834 RVA: 0x002039A9 File Offset: 0x00201BA9
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.DefineConductiveCells();
		GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.contactConductiveLayer, new Action<int, object>(this.OnAnyBuildingChanged));
	}

	// Token: 0x06005933 RID: 22835 RVA: 0x002039D7 File Offset: 0x00201BD7
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.RemoveGlobalLayerListener(GameScenePartitioner.Instance.contactConductiveLayer, new Action<int, object>(this.OnAnyBuildingChanged));
		this.UnregisterToSIM();
		base.OnCleanUp();
	}

	// Token: 0x06005934 RID: 22836 RVA: 0x00203A08 File Offset: 0x00201C08
	private void OnStructureTemperatureRegistered(object _sim_handle)
	{
		int num = (int)_sim_handle;
		this.RegisterToSIM(num);
	}

	// Token: 0x06005935 RID: 22837 RVA: 0x00203A24 File Offset: 0x00201C24
	private void RegisterToSIM(int sim_handle)
	{
		string name = this.building.Def.Name;
		SimMessages.RegisterBuildingToBuildingHeatExchange(sim_handle, Game.Instance.simComponentCallbackManager.Add(delegate(int sim_handle, object callback_data)
		{
			this.OnSimRegistered(sim_handle);
		}, null, "StructureToStructureTemperature.SimRegister").index);
	}

	// Token: 0x06005936 RID: 22838 RVA: 0x00203A71 File Offset: 0x00201C71
	private void OnSimRegistered(int sim_handle)
	{
		if (sim_handle != -1)
		{
			this.selfHandle = sim_handle;
			this.hasBeenRegister = true;
			if (this.buildingDestroyed)
			{
				this.UnregisterToSIM();
				return;
			}
			this.Refresh_InContactBuildings();
		}
	}

	// Token: 0x06005937 RID: 22839 RVA: 0x00203A9A File Offset: 0x00201C9A
	private void UnregisterToSIM()
	{
		if (this.hasBeenRegister)
		{
			SimMessages.RemoveBuildingToBuildingHeatExchange(this.selfHandle, -1);
		}
		this.buildingDestroyed = true;
	}

	// Token: 0x06005938 RID: 22840 RVA: 0x00203AB8 File Offset: 0x00201CB8
	private void DefineConductiveCells()
	{
		this.conductiveCells = new List<int>(this.building.PlacementCells);
		this.conductiveCells.Remove(this.building.GetUtilityInputCell());
		this.conductiveCells.Remove(this.building.GetUtilityOutputCell());
	}

	// Token: 0x06005939 RID: 22841 RVA: 0x00203B09 File Offset: 0x00201D09
	private void Add(StructureToStructureTemperature.InContactBuildingData buildingData)
	{
		if (this.inContactBuildings.Add(buildingData.buildingInContact))
		{
			SimMessages.AddBuildingToBuildingHeatExchange(this.selfHandle, buildingData.buildingInContact, buildingData.cellsInContact);
		}
	}

	// Token: 0x0600593A RID: 22842 RVA: 0x00203B35 File Offset: 0x00201D35
	private void Remove(int building)
	{
		if (this.inContactBuildings.Contains(building))
		{
			this.inContactBuildings.Remove(building);
			SimMessages.RemoveBuildingInContactFromBuildingToBuildingHeatExchange(this.selfHandle, building);
		}
	}

	// Token: 0x0600593B RID: 22843 RVA: 0x00203B60 File Offset: 0x00201D60
	private void OnAnyBuildingChanged(int _cell, object _data)
	{
		if (this.hasBeenRegister)
		{
			StructureToStructureTemperature.BuildingChangedObj buildingChangedObj = (StructureToStructureTemperature.BuildingChangedObj)_data;
			bool flag = false;
			int num = 0;
			for (int i = 0; i < buildingChangedObj.building.PlacementCells.Length; i++)
			{
				int num2 = buildingChangedObj.building.PlacementCells[i];
				if (this.conductiveCells.Contains(num2))
				{
					flag = true;
					num++;
				}
			}
			if (flag)
			{
				int simHandler = buildingChangedObj.simHandler;
				StructureToStructureTemperature.BuildingChangeType changeType = buildingChangedObj.changeType;
				if (changeType == StructureToStructureTemperature.BuildingChangeType.Created)
				{
					StructureToStructureTemperature.InContactBuildingData inContactBuildingData = new StructureToStructureTemperature.InContactBuildingData
					{
						buildingInContact = simHandler,
						cellsInContact = num
					};
					this.Add(inContactBuildingData);
					return;
				}
				if (changeType != StructureToStructureTemperature.BuildingChangeType.Destroyed)
				{
					return;
				}
				this.Remove(simHandler);
			}
		}
	}

	// Token: 0x0600593C RID: 22844 RVA: 0x00203C0C File Offset: 0x00201E0C
	private void Refresh_InContactBuildings()
	{
		foreach (StructureToStructureTemperature.InContactBuildingData inContactBuildingData in this.GetAll_InContact_Buildings())
		{
			this.Add(inContactBuildingData);
		}
	}

	// Token: 0x0600593D RID: 22845 RVA: 0x00203C60 File Offset: 0x00201E60
	private List<StructureToStructureTemperature.InContactBuildingData> GetAll_InContact_Buildings()
	{
		Dictionary<Building, int> dictionary = new Dictionary<Building, int>();
		List<StructureToStructureTemperature.InContactBuildingData> list = new List<StructureToStructureTemperature.InContactBuildingData>();
		List<GameObject> buildingsInCell = new List<GameObject>();
		using (List<int>.Enumerator enumerator = this.conductiveCells.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				int cell = enumerator.Current;
				buildingsInCell.Clear();
				Action<int> action = delegate(int layer)
				{
					GameObject gameObject = Grid.Objects[cell, layer];
					if (gameObject != null && !buildingsInCell.Contains(gameObject))
					{
						buildingsInCell.Add(gameObject);
					}
				};
				action(1);
				action(26);
				action(27);
				action(31);
				action(32);
				action(30);
				action(12);
				action(13);
				action(16);
				action(17);
				action(24);
				action(2);
				for (int i = 0; i < buildingsInCell.Count; i++)
				{
					Building building = ((buildingsInCell[i] == null) ? null : buildingsInCell[i].GetComponent<Building>());
					if (building != null && building.Def.UseStructureTemperature && building.PlacementCellsContainCell(cell))
					{
						if (!dictionary.ContainsKey(building))
						{
							dictionary.Add(building, 0);
						}
						Dictionary<Building, int> dictionary2 = dictionary;
						Building building2 = building;
						int num = dictionary2[building2];
						dictionary2[building2] = num + 1;
					}
				}
			}
		}
		foreach (Building building3 in dictionary.Keys)
		{
			HandleVector<int>.Handle handle = GameComps.StructureTemperatures.GetHandle(building3);
			if (handle != HandleVector<int>.InvalidHandle)
			{
				int simHandleCopy = GameComps.StructureTemperatures.GetPayload(handle).simHandleCopy;
				StructureToStructureTemperature.InContactBuildingData inContactBuildingData = new StructureToStructureTemperature.InContactBuildingData
				{
					buildingInContact = simHandleCopy,
					cellsInContact = dictionary[building3]
				};
				list.Add(inContactBuildingData);
			}
		}
		return list;
	}

	// Token: 0x04003B32 RID: 15154
	[MyCmpGet]
	private Building building;

	// Token: 0x04003B33 RID: 15155
	private List<int> conductiveCells;

	// Token: 0x04003B34 RID: 15156
	private HashSet<int> inContactBuildings = new HashSet<int>();

	// Token: 0x04003B35 RID: 15157
	private bool hasBeenRegister;

	// Token: 0x04003B36 RID: 15158
	private bool buildingDestroyed;

	// Token: 0x04003B37 RID: 15159
	private int selfHandle;

	// Token: 0x04003B38 RID: 15160
	protected static readonly EventSystem.IntraObjectHandler<StructureToStructureTemperature> OnStructureTemperatureRegisteredDelegate = new EventSystem.IntraObjectHandler<StructureToStructureTemperature>(delegate(StructureToStructureTemperature component, object data)
	{
		component.OnStructureTemperatureRegistered(data);
	});

	// Token: 0x02001CD7 RID: 7383
	public enum BuildingChangeType
	{
		// Token: 0x04008782 RID: 34690
		Created,
		// Token: 0x04008783 RID: 34691
		Destroyed,
		// Token: 0x04008784 RID: 34692
		Moved
	}

	// Token: 0x02001CD8 RID: 7384
	public struct InContactBuildingData
	{
		// Token: 0x04008785 RID: 34693
		public int buildingInContact;

		// Token: 0x04008786 RID: 34694
		public int cellsInContact;
	}

	// Token: 0x02001CD9 RID: 7385
	public struct BuildingChangedObj
	{
		// Token: 0x0600AC59 RID: 44121 RVA: 0x003C1A04 File Offset: 0x003BFC04
		public BuildingChangedObj(StructureToStructureTemperature.BuildingChangeType _changeType, Building _building, int sim_handler)
		{
			this.changeType = _changeType;
			this.building = _building;
			this.simHandler = sim_handler;
		}

		// Token: 0x04008787 RID: 34695
		public StructureToStructureTemperature.BuildingChangeType changeType;

		// Token: 0x04008788 RID: 34696
		public int simHandler;

		// Token: 0x04008789 RID: 34697
		public Building building;
	}
}
