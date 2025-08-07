using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AE1 RID: 2785
public class Room : IAssignableIdentity
{
	// Token: 0x170005B6 RID: 1462
	// (get) Token: 0x06005131 RID: 20785 RVA: 0x001D7591 File Offset: 0x001D5791
	public List<KPrefabID> buildings
	{
		get
		{
			return this.cavity.buildings;
		}
	}

	// Token: 0x170005B7 RID: 1463
	// (get) Token: 0x06005132 RID: 20786 RVA: 0x001D759E File Offset: 0x001D579E
	public List<KPrefabID> plants
	{
		get
		{
			return this.cavity.plants;
		}
	}

	// Token: 0x06005133 RID: 20787 RVA: 0x001D75AB File Offset: 0x001D57AB
	public string GetProperName()
	{
		return this.roomType.Name;
	}

	// Token: 0x06005134 RID: 20788 RVA: 0x001D75B8 File Offset: 0x001D57B8
	public List<Ownables> GetOwners()
	{
		this.current_owners.Clear();
		foreach (KPrefabID kprefabID in this.GetPrimaryEntities())
		{
			if (kprefabID != null)
			{
				Ownable component = kprefabID.GetComponent<Ownable>();
				if (component != null && component.assignee != null && component.assignee != this)
				{
					foreach (Ownables ownables in component.assignee.GetOwners())
					{
						if (!this.current_owners.Contains(ownables))
						{
							this.current_owners.Add(ownables);
						}
					}
				}
			}
		}
		return this.current_owners;
	}

	// Token: 0x06005135 RID: 20789 RVA: 0x001D76A4 File Offset: 0x001D58A4
	public List<GameObject> GetBuildingsOnFloor()
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < this.buildings.Count; i++)
		{
			if (!Grid.Solid[Grid.PosToCell(this.buildings[i])] && Grid.Solid[Grid.CellBelow(Grid.PosToCell(this.buildings[i]))])
			{
				list.Add(this.buildings[i].gameObject);
			}
		}
		return list;
	}

	// Token: 0x06005136 RID: 20790 RVA: 0x001D7724 File Offset: 0x001D5924
	public Ownables GetSoleOwner()
	{
		List<Ownables> owners = this.GetOwners();
		if (owners.Count <= 0)
		{
			return null;
		}
		return owners[0];
	}

	// Token: 0x06005137 RID: 20791 RVA: 0x001D774C File Offset: 0x001D594C
	public bool HasOwner(Assignables owner)
	{
		return this.GetOwners().Find((Ownables x) => x == owner) != null;
	}

	// Token: 0x06005138 RID: 20792 RVA: 0x001D7783 File Offset: 0x001D5983
	public int NumOwners()
	{
		return this.GetOwners().Count;
	}

	// Token: 0x06005139 RID: 20793 RVA: 0x001D7790 File Offset: 0x001D5990
	public List<KPrefabID> GetPrimaryEntities()
	{
		this.primary_buildings.Clear();
		RoomType roomType = this.roomType;
		if (roomType.primary_constraint != null)
		{
			foreach (KPrefabID kprefabID in this.buildings)
			{
				if (kprefabID != null && roomType.primary_constraint.building_criteria(kprefabID))
				{
					this.primary_buildings.Add(kprefabID);
				}
			}
			foreach (KPrefabID kprefabID2 in this.plants)
			{
				if (kprefabID2 != null && roomType.primary_constraint.building_criteria(kprefabID2))
				{
					this.primary_buildings.Add(kprefabID2);
				}
			}
		}
		return this.primary_buildings;
	}

	// Token: 0x0600513A RID: 20794 RVA: 0x001D788C File Offset: 0x001D5A8C
	public void RetriggerBuildings()
	{
		foreach (KPrefabID kprefabID in this.buildings)
		{
			if (!(kprefabID == null))
			{
				kprefabID.Trigger(144050788, this);
			}
		}
		foreach (KPrefabID kprefabID2 in this.plants)
		{
			if (!(kprefabID2 == null))
			{
				kprefabID2.Trigger(144050788, this);
			}
		}
	}

	// Token: 0x0600513B RID: 20795 RVA: 0x001D7940 File Offset: 0x001D5B40
	public bool IsNull()
	{
		return false;
	}

	// Token: 0x0600513C RID: 20796 RVA: 0x001D7943 File Offset: 0x001D5B43
	public void CleanUp()
	{
		Game.Instance.assignmentManager.RemoveFromAllGroups(this);
	}

	// Token: 0x04003680 RID: 13952
	public CavityInfo cavity;

	// Token: 0x04003681 RID: 13953
	public RoomType roomType;

	// Token: 0x04003682 RID: 13954
	private List<KPrefabID> primary_buildings = new List<KPrefabID>();

	// Token: 0x04003683 RID: 13955
	private List<Ownables> current_owners = new List<Ownables>();
}
