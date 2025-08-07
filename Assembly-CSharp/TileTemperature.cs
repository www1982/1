using System;
using UnityEngine;

// Token: 0x02000BB8 RID: 3000
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/TileTemperature")]
public class TileTemperature : KMonoBehaviour
{
	// Token: 0x060059B7 RID: 22967 RVA: 0x00206980 File Offset: 0x00204B80
	protected override void OnPrefabInit()
	{
		this.primaryElement.getTemperatureCallback = new PrimaryElement.GetTemperatureCallback(TileTemperature.OnGetTemperature);
		this.primaryElement.setTemperatureCallback = new PrimaryElement.SetTemperatureCallback(TileTemperature.OnSetTemperature);
		base.OnPrefabInit();
	}

	// Token: 0x060059B8 RID: 22968 RVA: 0x002069B6 File Offset: 0x00204BB6
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060059B9 RID: 22969 RVA: 0x002069C0 File Offset: 0x00204BC0
	private static float OnGetTemperature(PrimaryElement primary_element)
	{
		SimCellOccupier component = primary_element.GetComponent<SimCellOccupier>();
		if (component != null && component.IsReady())
		{
			int num = Grid.PosToCell(primary_element.transform.GetPosition());
			return Grid.Temperature[num];
		}
		return primary_element.InternalTemperature;
	}

	// Token: 0x060059BA RID: 22970 RVA: 0x00206A08 File Offset: 0x00204C08
	private static void OnSetTemperature(PrimaryElement primary_element, float temperature)
	{
		SimCellOccupier component = primary_element.GetComponent<SimCellOccupier>();
		if (component != null && component.IsReady())
		{
			global::Debug.LogWarning("Only set a tile's temperature during initialization. Otherwise you should be modifying the cell via the sim!");
			return;
		}
		primary_element.InternalTemperature = temperature;
	}

	// Token: 0x04003B8E RID: 15246
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04003B8F RID: 15247
	[MyCmpReq]
	private KSelectable selectable;
}
