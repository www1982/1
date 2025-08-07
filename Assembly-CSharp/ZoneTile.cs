using System;
using ProcGen;
using UnityEngine;

// Token: 0x02000BEC RID: 3052
[AddComponentMenu("KMonoBehaviour/scripts/ZoneTile")]
public class ZoneTile : KMonoBehaviour
{
	// Token: 0x06005BFA RID: 23546 RVA: 0x00213864 File Offset: 0x00211A64
	protected override void OnSpawn()
	{
		int[] placementCells = this.building.PlacementCells;
		for (int i = 0; i < placementCells.Length; i++)
		{
			SimMessages.ModifyCellWorldZone(placementCells[i], 0);
		}
		base.Subscribe<ZoneTile>(1606648047, ZoneTile.OnObjectReplacedDelegate);
	}

	// Token: 0x06005BFB RID: 23547 RVA: 0x002138A5 File Offset: 0x00211AA5
	protected override void OnCleanUp()
	{
		if (!this.wasReplaced)
		{
			this.ClearZone();
		}
	}

	// Token: 0x06005BFC RID: 23548 RVA: 0x002138B5 File Offset: 0x00211AB5
	private void OnObjectReplaced(object data)
	{
		this.ClearZone();
		this.wasReplaced = true;
	}

	// Token: 0x06005BFD RID: 23549 RVA: 0x002138C4 File Offset: 0x00211AC4
	private void ClearZone()
	{
		foreach (int num in this.building.PlacementCells)
		{
			GameObject gameObject;
			if (!Grid.ObjectLayers[(int)this.building.Def.ObjectLayer].TryGetValue(num, out gameObject) || !(gameObject != base.gameObject) || !(gameObject != null) || !(gameObject.GetComponent<ZoneTile>() != null))
			{
				SubWorld.ZoneType subWorldZoneType = global::World.Instance.zoneRenderData.GetSubWorldZoneType(num);
				byte b = ((subWorldZoneType == SubWorld.ZoneType.Space) ? byte.MaxValue : ((byte)subWorldZoneType));
				SimMessages.ModifyCellWorldZone(num, b);
			}
		}
	}

	// Token: 0x04003CE7 RID: 15591
	[MyCmpReq]
	public Building building;

	// Token: 0x04003CE8 RID: 15592
	private bool wasReplaced;

	// Token: 0x04003CE9 RID: 15593
	private static readonly EventSystem.IntraObjectHandler<ZoneTile> OnObjectReplacedDelegate = new EventSystem.IntraObjectHandler<ZoneTile>(delegate(ZoneTile component, object data)
	{
		component.OnObjectReplaced(data);
	});
}
