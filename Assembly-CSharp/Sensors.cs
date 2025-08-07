using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000503 RID: 1283
[AddComponentMenu("KMonoBehaviour/scripts/Sensors")]
public class Sensors : KMonoBehaviour
{
	// Token: 0x06001B78 RID: 7032 RVA: 0x00096946 File Offset: 0x00094B46
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<Brain>().onPreUpdate += this.OnBrainPreUpdate;
	}

	// Token: 0x06001B79 RID: 7033 RVA: 0x00096968 File Offset: 0x00094B68
	public SensorType GetSensor<SensorType>() where SensorType : Sensor
	{
		foreach (Sensor sensor in this.sensors)
		{
			if (typeof(SensorType).IsAssignableFrom(sensor.GetType()))
			{
				return (SensorType)((object)sensor);
			}
		}
		global::Debug.LogError("Missing sensor of type: " + typeof(SensorType).Name);
		return default(SensorType);
	}

	// Token: 0x06001B7A RID: 7034 RVA: 0x00096A00 File Offset: 0x00094C00
	public void Add(Sensor sensor)
	{
		this.sensors.Add(sensor);
		if (sensor.IsEnabled)
		{
			sensor.Update();
		}
	}

	// Token: 0x06001B7B RID: 7035 RVA: 0x00096A1C File Offset: 0x00094C1C
	public void UpdateSensors()
	{
		foreach (Sensor sensor in this.sensors)
		{
			if (sensor.IsEnabled)
			{
				sensor.Update();
			}
		}
	}

	// Token: 0x06001B7C RID: 7036 RVA: 0x00096A78 File Offset: 0x00094C78
	private void OnBrainPreUpdate()
	{
		this.UpdateSensors();
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x00096A80 File Offset: 0x00094C80
	public void ShowEditor()
	{
		foreach (Sensor sensor in this.sensors)
		{
			sensor.ShowEditor();
		}
	}

	// Token: 0x04001031 RID: 4145
	public List<Sensor> sensors = new List<Sensor>();
}
