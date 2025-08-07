using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E65 RID: 3685
public class TileScreen : KScreen
{
	// Token: 0x060075A0 RID: 30112 RVA: 0x002D0100 File Offset: 0x002CE300
	private bool SetSliderColour(float temperature, float transition_temperature)
	{
		if (Mathf.Abs(temperature - transition_temperature) < 5f)
		{
			this.temperatureSliderText.color = this.temperatureTransitionColour;
			this.temperatureSliderIcon.color = this.temperatureTransitionColour;
			return true;
		}
		this.temperatureSliderText.color = this.temperatureDefaultColour;
		this.temperatureSliderIcon.color = this.temperatureDefaultColour;
		return false;
	}

	// Token: 0x060075A1 RID: 30113 RVA: 0x002D0164 File Offset: 0x002CE364
	private void DisplayTileInfo()
	{
		Vector3 mousePos = KInputManager.GetMousePos();
		mousePos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
		int num = Grid.PosToCell(Camera.main.ScreenToWorldPoint(mousePos));
		if (Grid.IsValidCell(num) && Grid.IsVisible(num))
		{
			Element element = Grid.Element[num];
			this.nameLabel.text = element.name;
			float num2 = Grid.Mass[num];
			string text = "kg";
			if (num2 < 5f)
			{
				num2 *= 1000f;
				text = "g";
			}
			if (num2 < 5f)
			{
				num2 *= 1000f;
				text = "mg";
			}
			if (num2 < 5f)
			{
				num2 *= 1000f;
				text = "mcg";
				num2 = Mathf.Floor(num2);
			}
			this.massAmtLabel.text = string.Format("{0:0.0} {1}", num2, text);
			this.massTitleLabel.text = "mass";
			float num3 = Grid.Temperature[num];
			if (element.IsSolid)
			{
				this.solidIcon.gameObject.transform.parent.gameObject.SetActive(true);
				this.gasIcon.gameObject.transform.parent.gameObject.SetActive(false);
				this.massIcon.sprite = this.solidIcon.sprite;
				this.solidText.text = ((int)element.highTemp).ToString();
				this.gasText.text = "";
				this.liquidIcon.rectTransform.SetParent(this.solidIcon.transform.parent, true);
				this.liquidIcon.rectTransform.SetLocalPosition(new Vector3(0f, 64f));
				this.SetSliderColour(num3, element.highTemp);
				this.temperatureSlider.SetMinMaxValue(element.highTemp, Mathf.Min(element.highTemp + 100f, 4000f), Mathf.Max(element.highTemp - 100f, 0f), Mathf.Min(element.highTemp + 100f, 4000f));
			}
			else if (element.IsLiquid)
			{
				this.solidIcon.gameObject.transform.parent.gameObject.SetActive(true);
				this.gasIcon.gameObject.transform.parent.gameObject.SetActive(true);
				this.massIcon.sprite = this.liquidIcon.sprite;
				this.solidText.text = ((int)element.lowTemp).ToString();
				this.gasText.text = ((int)element.highTemp).ToString();
				this.liquidIcon.rectTransform.SetParent(this.temperatureSlider.transform.parent, true);
				this.liquidIcon.rectTransform.SetLocalPosition(new Vector3(-80f, 0f));
				if (!this.SetSliderColour(num3, element.lowTemp))
				{
					this.SetSliderColour(num3, element.highTemp);
				}
				this.temperatureSlider.SetMinMaxValue(element.lowTemp, element.highTemp, Mathf.Max(element.lowTemp - 100f, 0f), Mathf.Min(element.highTemp + 100f, 5200f));
			}
			else if (element.IsGas)
			{
				this.solidText.text = "";
				this.gasText.text = ((int)element.lowTemp).ToString();
				this.solidIcon.gameObject.transform.parent.gameObject.SetActive(false);
				this.gasIcon.gameObject.transform.parent.gameObject.SetActive(true);
				this.massIcon.sprite = this.gasIcon.sprite;
				this.SetSliderColour(num3, element.lowTemp);
				this.liquidIcon.rectTransform.SetParent(this.gasIcon.transform.parent, true);
				this.liquidIcon.rectTransform.SetLocalPosition(new Vector3(0f, -64f));
				this.temperatureSlider.SetMinMaxValue(0f, Mathf.Max(element.lowTemp - 100f, 0f), 0f, element.lowTemp + 100f);
			}
			this.temperatureSlider.SetExtraValue(num3);
			this.temperatureSliderText.text = GameUtil.GetFormattedTemperature((float)((int)num3), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false);
			Dictionary<int, float> info = FallingWater.instance.GetInfo(num);
			if (info.Count <= 0)
			{
				return;
			}
			List<Element> elements = ElementLoader.elements;
			using (Dictionary<int, float>.Enumerator enumerator = info.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, float> keyValuePair = enumerator.Current;
					Element element2 = elements[keyValuePair.Key];
					Text text2 = this.nameLabel;
					text2.text = text2.text + "\n" + element2.name + string.Format(" {0:0.00} kg", keyValuePair.Value);
				}
				return;
			}
		}
		this.nameLabel.text = "Unknown";
	}

	// Token: 0x060075A2 RID: 30114 RVA: 0x002D06D4 File Offset: 0x002CE8D4
	private void DisplayConduitFlowInfo()
	{
		HashedString mode = OverlayScreen.Instance.GetMode();
		UtilityNetworkManager<FlowUtilityNetwork, Vent> utilityNetworkManager = ((mode == OverlayModes.GasConduits.ID) ? Game.Instance.gasConduitSystem : Game.Instance.liquidConduitSystem);
		ConduitFlow conduitFlow = ((mode == OverlayModes.LiquidConduits.ID) ? Game.Instance.gasConduitFlow : Game.Instance.liquidConduitFlow);
		Vector3 mousePos = KInputManager.GetMousePos();
		mousePos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
		int num = Grid.PosToCell(Camera.main.ScreenToWorldPoint(mousePos));
		if (Grid.IsValidCell(num) && utilityNetworkManager.GetConnections(num, true) != (UtilityConnections)0)
		{
			ConduitFlow.ConduitContents contents = conduitFlow.GetContents(num);
			Element element = ElementLoader.FindElementByHash(contents.element);
			float num2 = contents.mass;
			float temperature = contents.temperature;
			this.nameLabel.text = element.name;
			string text = "kg";
			if (num2 < 5f)
			{
				num2 *= 1000f;
				text = "g";
			}
			this.massAmtLabel.text = string.Format("{0:0.0} {1}", num2, text);
			this.massTitleLabel.text = "mass";
			if (element.IsLiquid)
			{
				this.solidIcon.gameObject.transform.parent.gameObject.SetActive(true);
				this.gasIcon.gameObject.transform.parent.gameObject.SetActive(true);
				this.massIcon.sprite = this.liquidIcon.sprite;
				this.solidText.text = ((int)element.lowTemp).ToString();
				this.gasText.text = ((int)element.highTemp).ToString();
				this.liquidIcon.rectTransform.SetParent(this.temperatureSlider.transform.parent, true);
				this.liquidIcon.rectTransform.SetLocalPosition(new Vector3(-80f, 0f));
				if (!this.SetSliderColour(temperature, element.lowTemp))
				{
					this.SetSliderColour(temperature, element.highTemp);
				}
				this.temperatureSlider.SetMinMaxValue(element.lowTemp, element.highTemp, Mathf.Max(element.lowTemp - 100f, 0f), Mathf.Min(element.highTemp + 100f, 5200f));
			}
			else if (element.IsGas)
			{
				this.solidText.text = "";
				this.gasText.text = ((int)element.lowTemp).ToString();
				this.solidIcon.gameObject.transform.parent.gameObject.SetActive(false);
				this.gasIcon.gameObject.transform.parent.gameObject.SetActive(true);
				this.massIcon.sprite = this.gasIcon.sprite;
				this.SetSliderColour(temperature, element.lowTemp);
				this.liquidIcon.rectTransform.SetParent(this.gasIcon.transform.parent, true);
				this.liquidIcon.rectTransform.SetLocalPosition(new Vector3(0f, -64f));
				this.temperatureSlider.SetMinMaxValue(0f, Mathf.Max(element.lowTemp - 100f, 0f), 0f, element.lowTemp + 100f);
			}
			this.temperatureSlider.SetExtraValue(temperature);
			this.temperatureSliderText.text = GameUtil.GetFormattedTemperature((float)((int)temperature), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false);
			return;
		}
		this.nameLabel.text = "No Conduit";
		this.symbolLabel.text = "";
		this.massAmtLabel.text = "";
		this.massTitleLabel.text = "";
	}

	// Token: 0x060075A3 RID: 30115 RVA: 0x002D0AD4 File Offset: 0x002CECD4
	private void Update()
	{
		base.transform.SetPosition(KInputManager.GetMousePos());
		HashedString mode = OverlayScreen.Instance.GetMode();
		if (mode == OverlayModes.GasConduits.ID || mode == OverlayModes.LiquidConduits.ID)
		{
			this.DisplayConduitFlowInfo();
			return;
		}
		this.DisplayTileInfo();
	}

	// Token: 0x04005183 RID: 20867
	public Text nameLabel;

	// Token: 0x04005184 RID: 20868
	public Text symbolLabel;

	// Token: 0x04005185 RID: 20869
	public Text massTitleLabel;

	// Token: 0x04005186 RID: 20870
	public Text massAmtLabel;

	// Token: 0x04005187 RID: 20871
	public Image massIcon;

	// Token: 0x04005188 RID: 20872
	public MinMaxSlider temperatureSlider;

	// Token: 0x04005189 RID: 20873
	public Text temperatureSliderText;

	// Token: 0x0400518A RID: 20874
	public Image temperatureSliderIcon;

	// Token: 0x0400518B RID: 20875
	public Image solidIcon;

	// Token: 0x0400518C RID: 20876
	public Image liquidIcon;

	// Token: 0x0400518D RID: 20877
	public Image gasIcon;

	// Token: 0x0400518E RID: 20878
	public Text solidText;

	// Token: 0x0400518F RID: 20879
	public Text gasText;

	// Token: 0x04005190 RID: 20880
	[SerializeField]
	private Color temperatureDefaultColour;

	// Token: 0x04005191 RID: 20881
	[SerializeField]
	private Color temperatureTransitionColour;
}
