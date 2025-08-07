using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C6C RID: 3180
[AddComponentMenu("KMonoBehaviour/scripts/BatteryUI")]
public class BatteryUI : KMonoBehaviour
{
	// Token: 0x06006111 RID: 24849 RVA: 0x0023EA04 File Offset: 0x0023CC04
	private void Initialize()
	{
		if (this.unitLabel == null)
		{
			this.unitLabel = this.currentKJLabel.gameObject.GetComponentInChildrenOnly<LocText>();
		}
		if (this.sizeMap == null || this.sizeMap.Count == 0)
		{
			this.sizeMap = new Dictionary<float, float>();
			this.sizeMap.Add(20000f, 10f);
			this.sizeMap.Add(40000f, 25f);
			this.sizeMap.Add(60000f, 40f);
		}
	}

	// Token: 0x06006112 RID: 24850 RVA: 0x0023EA94 File Offset: 0x0023CC94
	public void SetContent(Battery bat)
	{
		if (bat == null || bat.GetMyWorldId() != ClusterManager.Instance.activeWorldId)
		{
			if (base.gameObject.activeSelf)
			{
				base.gameObject.SetActive(false);
			}
			return;
		}
		base.gameObject.SetActive(true);
		this.Initialize();
		RectTransform component = this.batteryBG.GetComponent<RectTransform>();
		float num = 0f;
		foreach (KeyValuePair<float, float> keyValuePair in this.sizeMap)
		{
			if (bat.Capacity <= keyValuePair.Key)
			{
				num = keyValuePair.Value;
				break;
			}
		}
		this.batteryBG.sprite = ((bat.Capacity >= 40000f) ? this.bigBatteryBG : this.regularBatteryBG);
		float num2 = 25f;
		component.sizeDelta = new Vector2(num, num2);
		BuildingEnabledButton component2 = bat.GetComponent<BuildingEnabledButton>();
		Color color;
		if (component2 != null && !component2.IsEnabled)
		{
			color = Color.gray;
		}
		else
		{
			color = ((bat.PercentFull >= bat.PreviousPercentFull) ? this.energyIncreaseColor : this.energyDecreaseColor);
		}
		this.batteryMeter.color = color;
		this.batteryBG.color = color;
		float num3 = this.batteryBG.GetComponent<RectTransform>().rect.height * bat.PercentFull;
		this.batteryMeter.GetComponent<RectTransform>().sizeDelta = new Vector2(num - 5.5f, num3 - 5.5f);
		color.a = 1f;
		if (this.currentKJLabel.color != color)
		{
			this.currentKJLabel.color = color;
			this.unitLabel.color = color;
		}
		this.currentKJLabel.text = bat.JoulesAvailable.ToString("F0");
	}

	// Token: 0x040041B7 RID: 16823
	[SerializeField]
	private LocText currentKJLabel;

	// Token: 0x040041B8 RID: 16824
	[SerializeField]
	private Image batteryBG;

	// Token: 0x040041B9 RID: 16825
	[SerializeField]
	private Image batteryMeter;

	// Token: 0x040041BA RID: 16826
	[SerializeField]
	private Sprite regularBatteryBG;

	// Token: 0x040041BB RID: 16827
	[SerializeField]
	private Sprite bigBatteryBG;

	// Token: 0x040041BC RID: 16828
	[SerializeField]
	private Color energyIncreaseColor = Color.green;

	// Token: 0x040041BD RID: 16829
	[SerializeField]
	private Color energyDecreaseColor = Color.red;

	// Token: 0x040041BE RID: 16830
	private LocText unitLabel;

	// Token: 0x040041BF RID: 16831
	private const float UIUnit = 10f;

	// Token: 0x040041C0 RID: 16832
	private Dictionary<float, float> sizeMap;
}
