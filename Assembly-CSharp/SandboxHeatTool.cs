using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000984 RID: 2436
public class SandboxHeatTool : BrushTool
{
	// Token: 0x060046A5 RID: 18085 RVA: 0x00195F18 File Offset: 0x00194118
	public static void DestroyInstance()
	{
		SandboxHeatTool.instance = null;
	}

	// Token: 0x170004FB RID: 1275
	// (get) Token: 0x060046A6 RID: 18086 RVA: 0x00195F20 File Offset: 0x00194120
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x060046A7 RID: 18087 RVA: 0x00195F2C File Offset: 0x0019412C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxHeatTool.instance = this;
		this.viewMode = OverlayModes.Temperature.ID;
	}

	// Token: 0x060046A8 RID: 18088 RVA: 0x00195F45 File Offset: 0x00194145
	protected override string GetDragSound()
	{
		return "";
	}

	// Token: 0x060046A9 RID: 18089 RVA: 0x00195F4C File Offset: 0x0019414C
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060046AA RID: 18090 RVA: 0x00195F5C File Offset: 0x0019415C
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.temperatureAdditiveSlider.row.SetActive(true);
	}

	// Token: 0x060046AB RID: 18091 RVA: 0x00195FB3 File Offset: 0x001941B3
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060046AC RID: 18092 RVA: 0x00195FCC File Offset: 0x001941CC
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.recentlyAffectedCells)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.recentlyAffectedCellColor));
		}
		foreach (int num2 in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num2, this.radiusIndicatorColor));
		}
	}

	// Token: 0x060046AD RID: 18093 RVA: 0x00196084 File Offset: 0x00194284
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x060046AE RID: 18094 RVA: 0x00196090 File Offset: 0x00194290
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		if (this.recentlyAffectedCells.Contains(cell))
		{
			return;
		}
		this.recentlyAffectedCells.Add(cell);
		Game.CallbackInfo callbackInfo = new Game.CallbackInfo(delegate
		{
			this.recentlyAffectedCells.Remove(cell);
		}, false);
		int index = Game.Instance.callbackManager.Add(callbackInfo).index;
		float num = Grid.Temperature[cell];
		num += SandboxToolParameterMenu.instance.settings.GetFloatSetting("SandbosTools.TemperatureAdditive");
		GameUtil.TemperatureUnit temperatureUnit = GameUtil.temperatureUnit;
		if (temperatureUnit != GameUtil.TemperatureUnit.Celsius)
		{
			if (temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit)
			{
				num -= 255.372f;
			}
		}
		else
		{
			num -= 273.15f;
		}
		num = Mathf.Clamp(num, 1f, 9999f);
		int cell2 = cell;
		SimHashes id = Grid.Element[cell].id;
		CellElementEvent sandBoxTool = CellEventLogger.Instance.SandBoxTool;
		float num2 = Grid.Mass[cell];
		float num3 = num;
		int num4 = index;
		SimMessages.ReplaceElement(cell2, id, sandBoxTool, num2, num3, Grid.DiseaseIdx[cell], Grid.DiseaseCount[cell], num4);
		float currentValue = SandboxToolParameterMenu.instance.temperatureAdditiveSlider.inputField.currentValue;
		KFMOD.PlayUISoundWithLabeledParameter(GlobalAssets.GetSound("SandboxTool_HeatGun", false), "TemperatureSetting", (currentValue <= 0f) ? "Cooling" : "Heating");
	}

	// Token: 0x04002ECD RID: 11981
	public static SandboxHeatTool instance;

	// Token: 0x04002ECE RID: 11982
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x04002ECF RID: 11983
	protected Color recentlyAffectedCellColor = new Color(1f, 1f, 1f, 0.1f);
}
