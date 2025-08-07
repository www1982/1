using System;
using UnityEngine;

// Token: 0x020006C6 RID: 1734
[AddComponentMenu("KMonoBehaviour/scripts/BuildingAttachPoint")]
public class BuildingAttachPoint : KMonoBehaviour
{
	// Token: 0x06002AB7 RID: 10935 RVA: 0x000F719A File Offset: 0x000F539A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.BuildingAttachPoints.Add(this);
		this.TryAttachEmptyHardpoints();
	}

	// Token: 0x06002AB8 RID: 10936 RVA: 0x000F71B3 File Offset: 0x000F53B3
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06002AB9 RID: 10937 RVA: 0x000F71BC File Offset: 0x000F53BC
	private void TryAttachEmptyHardpoints()
	{
		for (int i = 0; i < this.points.Length; i++)
		{
			if (!(this.points[i].attachedBuilding != null))
			{
				bool flag = false;
				int num = 0;
				while (num < Components.AttachableBuildings.Count && !flag)
				{
					if (Components.AttachableBuildings[num].attachableToTag == this.points[i].attachableType && Grid.OffsetCell(Grid.PosToCell(base.gameObject), this.points[i].position) == Grid.PosToCell(Components.AttachableBuildings[num]))
					{
						this.points[i].attachedBuilding = Components.AttachableBuildings[num];
						flag = true;
					}
					num++;
				}
			}
		}
	}

	// Token: 0x06002ABA RID: 10938 RVA: 0x000F7294 File Offset: 0x000F5494
	public bool AcceptsAttachment(Tag type, int cell)
	{
		int num = Grid.PosToCell(base.gameObject);
		for (int i = 0; i < this.points.Length; i++)
		{
			if (Grid.OffsetCell(num, this.points[i].position) == cell && this.points[i].attachableType == type)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002ABB RID: 10939 RVA: 0x000F72F6 File Offset: 0x000F54F6
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.BuildingAttachPoints.Remove(this);
	}

	// Token: 0x0400192B RID: 6443
	public BuildingAttachPoint.HardPoint[] points = new BuildingAttachPoint.HardPoint[0];

	// Token: 0x0200154B RID: 5451
	[Serializable]
	public struct HardPoint
	{
		// Token: 0x060090A4 RID: 37028 RVA: 0x00360DFD File Offset: 0x0035EFFD
		public HardPoint(CellOffset position, Tag attachableType, AttachableBuilding attachedBuilding)
		{
			this.position = position;
			this.attachableType = attachableType;
			this.attachedBuilding = attachedBuilding;
		}

		// Token: 0x04006F3C RID: 28476
		public CellOffset position;

		// Token: 0x04006F3D RID: 28477
		public Tag attachableType;

		// Token: 0x04006F3E RID: 28478
		public AttachableBuilding attachedBuilding;
	}
}
