using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B2 RID: 1714
[AddComponentMenu("KMonoBehaviour/scripts/AttachableBuilding")]
public class AttachableBuilding : KMonoBehaviour
{
	// Token: 0x06002A19 RID: 10777 RVA: 0x000F3F8C File Offset: 0x000F218C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.RegisterWithAttachPoint(true);
		Components.AttachableBuildings.Add(this);
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this))
		{
			AttachableBuilding component = gameObject.GetComponent<AttachableBuilding>();
			if (component != null && component.onAttachmentNetworkChanged != null)
			{
				component.onAttachmentNetworkChanged(this);
			}
		}
	}

	// Token: 0x06002A1A RID: 10778 RVA: 0x000F4014 File Offset: 0x000F2214
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06002A1B RID: 10779 RVA: 0x000F401C File Offset: 0x000F221C
	public void RegisterWithAttachPoint(bool register)
	{
		BuildingDef buildingDef = null;
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		BuildingUnderConstruction component2 = base.GetComponent<BuildingUnderConstruction>();
		if (component != null)
		{
			buildingDef = component.Def;
		}
		else if (component2 != null)
		{
			buildingDef = component2.Def;
		}
		int num = Grid.OffsetCell(Grid.PosToCell(base.gameObject), buildingDef.attachablePosition);
		bool flag = false;
		int num2 = 0;
		while (!flag && num2 < Components.BuildingAttachPoints.Count)
		{
			for (int i = 0; i < Components.BuildingAttachPoints[num2].points.Length; i++)
			{
				if (num == Grid.OffsetCell(Grid.PosToCell(Components.BuildingAttachPoints[num2]), Components.BuildingAttachPoints[num2].points[i].position))
				{
					if (register)
					{
						Components.BuildingAttachPoints[num2].points[i].attachedBuilding = this;
					}
					else if (Components.BuildingAttachPoints[num2].points[i].attachedBuilding == this)
					{
						Components.BuildingAttachPoints[num2].points[i].attachedBuilding = null;
					}
					flag = true;
					break;
				}
			}
			num2++;
		}
	}

	// Token: 0x06002A1C RID: 10780 RVA: 0x000F4164 File Offset: 0x000F2364
	public static void GetAttachedBelow(AttachableBuilding searchStart, ref List<GameObject> buildings)
	{
		AttachableBuilding attachableBuilding = searchStart;
		while (attachableBuilding != null)
		{
			BuildingAttachPoint attachedTo = attachableBuilding.GetAttachedTo();
			attachableBuilding = null;
			if (attachedTo != null)
			{
				buildings.Add(attachedTo.gameObject);
				attachableBuilding = attachedTo.GetComponent<AttachableBuilding>();
			}
		}
	}

	// Token: 0x06002A1D RID: 10781 RVA: 0x000F41A4 File Offset: 0x000F23A4
	public static int CountAttachedBelow(AttachableBuilding searchStart)
	{
		int num = 0;
		AttachableBuilding attachableBuilding = searchStart;
		while (attachableBuilding != null)
		{
			BuildingAttachPoint attachedTo = attachableBuilding.GetAttachedTo();
			attachableBuilding = null;
			if (attachedTo != null)
			{
				num++;
				attachableBuilding = attachedTo.GetComponent<AttachableBuilding>();
			}
		}
		return num;
	}

	// Token: 0x06002A1E RID: 10782 RVA: 0x000F41E0 File Offset: 0x000F23E0
	public static void GetAttachedAbove(AttachableBuilding searchStart, ref List<GameObject> buildings)
	{
		BuildingAttachPoint buildingAttachPoint = searchStart.GetComponent<BuildingAttachPoint>();
		while (buildingAttachPoint != null)
		{
			bool flag = false;
			foreach (BuildingAttachPoint.HardPoint hardPoint in buildingAttachPoint.points)
			{
				if (flag)
				{
					break;
				}
				if (hardPoint.attachedBuilding != null)
				{
					foreach (object obj in Components.AttachableBuildings)
					{
						AttachableBuilding attachableBuilding = (AttachableBuilding)obj;
						if (attachableBuilding == hardPoint.attachedBuilding)
						{
							buildings.Add(attachableBuilding.gameObject);
							buildingAttachPoint = attachableBuilding.GetComponent<BuildingAttachPoint>();
							flag = true;
						}
					}
				}
			}
			if (!flag)
			{
				buildingAttachPoint = null;
			}
		}
	}

	// Token: 0x06002A1F RID: 10783 RVA: 0x000F42BC File Offset: 0x000F24BC
	public static void NotifyBuildingsNetworkChanged(List<GameObject> buildings, AttachableBuilding attachable = null)
	{
		foreach (GameObject gameObject in buildings)
		{
			AttachableBuilding component = gameObject.GetComponent<AttachableBuilding>();
			if (component != null && component.onAttachmentNetworkChanged != null)
			{
				component.onAttachmentNetworkChanged(attachable);
			}
		}
	}

	// Token: 0x06002A20 RID: 10784 RVA: 0x000F4328 File Offset: 0x000F2528
	public static List<GameObject> GetAttachedNetwork(AttachableBuilding searchStart)
	{
		List<GameObject> list = new List<GameObject>();
		list.Add(searchStart.gameObject);
		AttachableBuilding.GetAttachedAbove(searchStart, ref list);
		AttachableBuilding.GetAttachedBelow(searchStart, ref list);
		return list;
	}

	// Token: 0x06002A21 RID: 10785 RVA: 0x000F4358 File Offset: 0x000F2558
	public BuildingAttachPoint GetAttachedTo()
	{
		for (int i = 0; i < Components.BuildingAttachPoints.Count; i++)
		{
			for (int j = 0; j < Components.BuildingAttachPoints[i].points.Length; j++)
			{
				if (Components.BuildingAttachPoints[i].points[j].attachedBuilding == this && (Components.BuildingAttachPoints[i].points[j].attachedBuilding.GetComponent<Deconstructable>() == null || !Components.BuildingAttachPoints[i].points[j].attachedBuilding.GetComponent<Deconstructable>().HasBeenDestroyed))
				{
					return Components.BuildingAttachPoints[i];
				}
			}
		}
		return null;
	}

	// Token: 0x06002A22 RID: 10786 RVA: 0x000F4422 File Offset: 0x000F2622
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		AttachableBuilding.NotifyBuildingsNetworkChanged(AttachableBuilding.GetAttachedNetwork(this), this);
		this.RegisterWithAttachPoint(false);
		Components.AttachableBuildings.Remove(this);
	}

	// Token: 0x040018F2 RID: 6386
	public Tag attachableToTag;

	// Token: 0x040018F3 RID: 6387
	public Action<object> onAttachmentNetworkChanged;
}
