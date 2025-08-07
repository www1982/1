using System;
using UnityEngine;

// Token: 0x02000502 RID: 1282
public class Sensor
{
	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06001B6C RID: 7020 RVA: 0x00096895 File Offset: 0x00094A95
	// (set) Token: 0x06001B6B RID: 7019 RVA: 0x0009688C File Offset: 0x00094A8C
	public bool IsEnabled { get; private set; } = true;

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0009689D File Offset: 0x00094A9D
	// (set) Token: 0x06001B6E RID: 7022 RVA: 0x000968A5 File Offset: 0x00094AA5
	public string Name { get; private set; }

	// Token: 0x06001B6F RID: 7023 RVA: 0x000968AE File Offset: 0x00094AAE
	public Sensor(Sensors sensors, bool active)
	{
		this.sensors = sensors;
		this.SetActive(active);
		this.Name = base.GetType().Name;
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x000968DC File Offset: 0x00094ADC
	public Sensor(Sensors sensors)
	{
		this.sensors = sensors;
		this.Name = base.GetType().Name;
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x00096903 File Offset: 0x00094B03
	public ComponentType GetComponent<ComponentType>()
	{
		return this.sensors.GetComponent<ComponentType>();
	}

	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x06001B72 RID: 7026 RVA: 0x00096910 File Offset: 0x00094B10
	public GameObject gameObject
	{
		get
		{
			return this.sensors.gameObject;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0009691D File Offset: 0x00094B1D
	public Transform transform
	{
		get
		{
			return this.gameObject.transform;
		}
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x0009692A File Offset: 0x00094B2A
	public virtual void SetActive(bool enabled)
	{
		this.IsEnabled = enabled;
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x00096933 File Offset: 0x00094B33
	public void Trigger(int hash, object data = null)
	{
		this.sensors.Trigger(hash, data);
	}

	// Token: 0x06001B76 RID: 7030 RVA: 0x00096942 File Offset: 0x00094B42
	public virtual void Update()
	{
	}

	// Token: 0x06001B77 RID: 7031 RVA: 0x00096944 File Offset: 0x00094B44
	public virtual void ShowEditor()
	{
	}

	// Token: 0x0400102F RID: 4143
	protected Sensors sensors;
}
