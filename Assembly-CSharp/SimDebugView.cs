using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using TUNING;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

// Token: 0x02000B0F RID: 2831
[AddComponentMenu("KMonoBehaviour/scripts/SimDebugView")]
public class SimDebugView : KMonoBehaviour
{
	// Token: 0x06005328 RID: 21288 RVA: 0x001E3CFF File Offset: 0x001E1EFF
	public static void DestroyInstance()
	{
		SimDebugView.Instance = null;
	}

	// Token: 0x06005329 RID: 21289 RVA: 0x001E3D07 File Offset: 0x001E1F07
	protected override void OnPrefabInit()
	{
		SimDebugView.Instance = this;
		this.material = global::UnityEngine.Object.Instantiate<Material>(this.material);
		this.diseaseMaterial = global::UnityEngine.Object.Instantiate<Material>(this.diseaseMaterial);
	}

	// Token: 0x0600532A RID: 21290 RVA: 0x001E3D34 File Offset: 0x001E1F34
	protected override void OnSpawn()
	{
		SimDebugViewCompositor.Instance.material.SetColor("_Color0", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[0].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color1", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[1].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color2", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[2].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color3", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[3].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color4", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[4].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color5", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[5].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color6", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[6].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color7", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[7].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color0", GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[0].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color1", GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[1].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color2", GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[2].colorName));
		this.SetMode(global::OverlayModes.None.ID);
	}

	// Token: 0x0600532B RID: 21291 RVA: 0x001E3FC0 File Offset: 0x001E21C0
	public void OnReset()
	{
		this.plane = SimDebugView.CreatePlane("SimDebugView", base.transform);
		this.tex = SimDebugView.CreateTexture(out this.texBytes, Grid.WidthInCells, Grid.HeightInCells);
		this.plane.GetComponent<Renderer>().sharedMaterial = this.material;
		this.plane.GetComponent<Renderer>().sharedMaterial.mainTexture = this.tex;
		this.plane.transform.SetLocalPosition(new Vector3(0f, 0f, -6f));
		this.SetMode(global::OverlayModes.None.ID);
	}

	// Token: 0x0600532C RID: 21292 RVA: 0x001E405F File Offset: 0x001E225F
	public static Texture2D CreateTexture(int width, int height)
	{
		return new Texture2D(width, height)
		{
			name = "SimDebugView",
			wrapMode = TextureWrapMode.Clamp,
			filterMode = FilterMode.Point
		};
	}

	// Token: 0x0600532D RID: 21293 RVA: 0x001E4081 File Offset: 0x001E2281
	public static Texture2D CreateTexture(out byte[] textureBytes, int width, int height)
	{
		textureBytes = new byte[width * height * 4];
		return new Texture2D(width, height, TextureUtil.TextureFormatToGraphicsFormat(TextureFormat.RGBA32), TextureCreationFlags.None)
		{
			name = "SimDebugView",
			wrapMode = TextureWrapMode.Clamp,
			filterMode = FilterMode.Point
		};
	}

	// Token: 0x0600532E RID: 21294 RVA: 0x001E40B8 File Offset: 0x001E22B8
	public static Texture2D CreateTexture(int width, int height, Color col)
	{
		Color[] array = new Color[width * height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = col;
		}
		Texture2D texture2D = new Texture2D(width, height);
		texture2D.SetPixels(array);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x0600532F RID: 21295 RVA: 0x001E40F8 File Offset: 0x001E22F8
	public static GameObject CreatePlane(string layer, Transform parent)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "overlayViewDisplayPlane";
		gameObject.SetLayerRecursively(LayerMask.NameToLayer(layer));
		gameObject.transform.SetParent(parent);
		gameObject.transform.SetPosition(Vector3.zero);
		gameObject.AddComponent<MeshRenderer>().reflectionProbeUsage = ReflectionProbeUsage.Off;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		meshFilter.mesh = mesh;
		int num = 4;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		int[] array3 = new int[6];
		float num2 = 2f * (float)Grid.HeightInCells;
		array = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3((float)Grid.WidthInCells, 0f, 0f),
			new Vector3(0f, num2, 0f),
			new Vector3(Grid.WidthInMeters, num2, 0f)
		};
		array2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 2f),
			new Vector2(1f, 2f)
		};
		array3 = new int[] { 0, 2, 1, 1, 2, 3 };
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		Vector2 vector = new Vector2((float)Grid.WidthInCells, num2);
		mesh.bounds = new Bounds(new Vector3(0.5f * vector.x, 0.5f * vector.y, 0f), new Vector3(vector.x, vector.y, 0f));
		return gameObject;
	}

	// Token: 0x06005330 RID: 21296 RVA: 0x001E42D0 File Offset: 0x001E24D0
	private void Update()
	{
		if (this.plane == null)
		{
			return;
		}
		bool flag = this.mode != global::OverlayModes.None.ID;
		this.plane.SetActive(flag);
		SimDebugViewCompositor.Instance.Toggle(flag && !GameUtil.IsCapturingTimeLapse());
		SimDebugViewCompositor.Instance.material.SetVector("_Thresholds0", new Vector4(0.1f, 0.2f, 0.3f, 0.4f));
		SimDebugViewCompositor.Instance.material.SetVector("_Thresholds1", new Vector4(0.5f, 0.6f, 0.7f, 0.8f));
		float num = 0f;
		if (this.mode == global::OverlayModes.ThermalConductivity.ID || this.mode == global::OverlayModes.Temperature.ID)
		{
			num = 1f;
		}
		SimDebugViewCompositor.Instance.material.SetVector("_ThresholdParameters", new Vector4(num, this.thresholdRange, this.thresholdOpacity, 0f));
		if (flag)
		{
			this.UpdateData(this.tex, this.texBytes, this.mode, 192);
		}
	}

	// Token: 0x06005331 RID: 21297 RVA: 0x001E43F6 File Offset: 0x001E25F6
	private static void SetDefaultBilinear(SimDebugView instance, Texture texture)
	{
		Renderer component = instance.plane.GetComponent<Renderer>();
		component.sharedMaterial = instance.material;
		component.sharedMaterial.mainTexture = instance.tex;
		texture.filterMode = FilterMode.Bilinear;
	}

	// Token: 0x06005332 RID: 21298 RVA: 0x001E4426 File Offset: 0x001E2626
	private static void SetDefaultPoint(SimDebugView instance, Texture texture)
	{
		Renderer component = instance.plane.GetComponent<Renderer>();
		component.sharedMaterial = instance.material;
		component.sharedMaterial.mainTexture = instance.tex;
		texture.filterMode = FilterMode.Point;
	}

	// Token: 0x06005333 RID: 21299 RVA: 0x001E4456 File Offset: 0x001E2656
	private static void SetDisease(SimDebugView instance, Texture texture)
	{
		Renderer component = instance.plane.GetComponent<Renderer>();
		component.sharedMaterial = instance.diseaseMaterial;
		component.sharedMaterial.mainTexture = instance.tex;
		texture.filterMode = FilterMode.Bilinear;
	}

	// Token: 0x06005334 RID: 21300 RVA: 0x001E4488 File Offset: 0x001E2688
	public void UpdateData(Texture2D texture, byte[] textureBytes, HashedString viewMode, byte alpha)
	{
		Action<SimDebugView, Texture> action;
		if (!this.dataUpdateFuncs.TryGetValue(viewMode, out action))
		{
			action = new Action<SimDebugView, Texture>(SimDebugView.SetDefaultPoint);
		}
		action(this, texture);
		int num;
		int num2;
		int num3;
		int num4;
		Grid.GetVisibleExtents(out num, out num2, out num3, out num4);
		this.selectedPathProber = null;
		KSelectable selected = SelectTool.Instance.selected;
		if (selected != null)
		{
			this.selectedPathProber = selected.GetComponent<PathProber>();
		}
		this.updateSimViewWorkItems.Reset(new SimDebugView.UpdateSimViewSharedData(this, this.texBytes, viewMode, this));
		int num5 = 16;
		for (int i = num2; i <= num4; i += num5)
		{
			int num6 = Math.Min(i + num5 - 1, num4);
			this.updateSimViewWorkItems.Add(new SimDebugView.UpdateSimViewWorkItem(num, i, num3, num6));
		}
		this.currentFrame = Time.frameCount;
		this.selectedCell = Grid.PosToCell(Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos()));
		GlobalJobManager.Run(this.updateSimViewWorkItems);
		texture.LoadRawTextureData(textureBytes);
		texture.Apply();
	}

	// Token: 0x06005335 RID: 21301 RVA: 0x001E4583 File Offset: 0x001E2783
	public void SetGameGridMode(SimDebugView.GameGridMode mode)
	{
		this.gameGridMode = mode;
	}

	// Token: 0x06005336 RID: 21302 RVA: 0x001E458C File Offset: 0x001E278C
	public SimDebugView.GameGridMode GetGameGridMode()
	{
		return this.gameGridMode;
	}

	// Token: 0x06005337 RID: 21303 RVA: 0x001E4594 File Offset: 0x001E2794
	public void SetMode(HashedString mode)
	{
		this.mode = mode;
		Game.Instance.gameObject.Trigger(1798162660, mode);
	}

	// Token: 0x06005338 RID: 21304 RVA: 0x001E45B7 File Offset: 0x001E27B7
	public HashedString GetMode()
	{
		return this.mode;
	}

	// Token: 0x06005339 RID: 21305 RVA: 0x001E45C0 File Offset: 0x001E27C0
	public static Color TemperatureToColor(float temperature, float minTempExpected, float maxTempExpected)
	{
		float num = Mathf.Clamp((temperature - minTempExpected) / (maxTempExpected - minTempExpected), 0f, 1f);
		return Color.HSVToRGB((10f + (1f - num) * 171f) / 360f, 1f, 1f);
	}

	// Token: 0x0600533A RID: 21306 RVA: 0x001E460C File Offset: 0x001E280C
	public static Color LiquidTemperatureToColor(float temperature, float minTempExpected, float maxTempExpected)
	{
		float num = (temperature - minTempExpected) / (maxTempExpected - minTempExpected);
		float num2 = Mathf.Clamp(num, 0.5f, 1f);
		float num3 = Mathf.Clamp(num, 0f, 1f);
		return Color.HSVToRGB((10f + (1f - num2) * 171f) / 360f, num3, 1f);
	}

	// Token: 0x0600533B RID: 21307 RVA: 0x001E4668 File Offset: 0x001E2868
	public static Color SolidTemperatureToColor(float temperature, float minTempExpected, float maxTempExpected)
	{
		float num = Mathf.Clamp((temperature - minTempExpected) / (maxTempExpected - minTempExpected), 0.5f, 1f);
		float num2 = 1f;
		return Color.HSVToRGB((10f + (1f - num) * 171f) / 360f, num2, 1f);
	}

	// Token: 0x0600533C RID: 21308 RVA: 0x001E46B8 File Offset: 0x001E28B8
	public static Color GasTemperatureToColor(float temperature, float minTempExpected, float maxTempExpected)
	{
		float num = Mathf.Clamp((temperature - minTempExpected) / (maxTempExpected - minTempExpected), 0f, 0.5f);
		float num2 = 1f;
		return Color.HSVToRGB((10f + (1f - num) * 171f) / 360f, num2, 1f);
	}

	// Token: 0x0600533D RID: 21309 RVA: 0x001E4708 File Offset: 0x001E2908
	public Color NormalizedTemperature(float actualTemperature)
	{
		float num = this.user_temperatureThresholds[0];
		float num2 = this.user_temperatureThresholds[1];
		float num3 = num2 - num;
		if (actualTemperature < num)
		{
			return GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[0].colorName);
		}
		if (actualTemperature > num2)
		{
			return GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[this.temperatureThresholds.Length - 1].colorName);
		}
		int num4 = 0;
		float num5 = 0f;
		Game.TemperatureOverlayModes temperatureOverlayMode = Game.Instance.temperatureOverlayMode;
		if (temperatureOverlayMode != Game.TemperatureOverlayModes.AbsoluteTemperature)
		{
			if (temperatureOverlayMode == Game.TemperatureOverlayModes.RelativeTemperature)
			{
				float num6 = num;
				for (int i = 0; i < SimDebugView.relativeTemperatureColorIntervals.Length; i++)
				{
					if (actualTemperature < num6 + SimDebugView.relativeTemperatureColorIntervals[i] * num3)
					{
						num4 = i;
						break;
					}
					num6 += SimDebugView.relativeTemperatureColorIntervals[i] * num3;
				}
				num5 = (actualTemperature - num6) / (SimDebugView.relativeTemperatureColorIntervals[num4] * num3);
			}
		}
		else
		{
			float num7 = num;
			for (int j = 0; j < SimDebugView.absoluteTemperatureColorIntervals.Length; j++)
			{
				if (actualTemperature < num7 + SimDebugView.absoluteTemperatureColorIntervals[j])
				{
					num4 = j;
					break;
				}
				num7 += SimDebugView.absoluteTemperatureColorIntervals[j];
			}
			num5 = (actualTemperature - num7) / SimDebugView.absoluteTemperatureColorIntervals[num4];
		}
		return Color.Lerp(GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[num4].colorName), GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[num4 + 1].colorName), num5);
	}

	// Token: 0x0600533E RID: 21310 RVA: 0x001E489C File Offset: 0x001E2A9C
	public Color NormalizedHeatFlow(int cell)
	{
		int num = 0;
		int num2 = 0;
		float thermalComfort = GameUtil.GetThermalComfort(GameTags.Minions.Models.Standard, cell, -DUPLICANTSTATS.STANDARD.BaseStats.DUPLICANT_BASE_GENERATION_KILOWATTS);
		for (int i = 0; i < this.heatFlowThresholds.Length; i++)
		{
			if (thermalComfort <= this.heatFlowThresholds[i].value)
			{
				num2 = i;
				break;
			}
			num = i;
			num2 = i;
		}
		float num3 = 0f;
		if (num != num2)
		{
			num3 = (thermalComfort - this.heatFlowThresholds[num].value) / (this.heatFlowThresholds[num2].value - this.heatFlowThresholds[num].value);
		}
		num3 = Mathf.Max(num3, 0f);
		num3 = Mathf.Min(num3, 1f);
		Color color = Color.Lerp(GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[num].colorName), GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[num2].colorName), num3);
		if (Grid.Solid[cell])
		{
			color = Color.black;
		}
		return color;
	}

	// Token: 0x0600533F RID: 21311 RVA: 0x001E49C2 File Offset: 0x001E2BC2
	private static bool IsInsulated(int cell)
	{
		return (Grid.Element[cell].state & Element.State.TemperatureInsulated) > Element.State.Vacuum;
	}

	// Token: 0x06005340 RID: 21312 RVA: 0x001E49D8 File Offset: 0x001E2BD8
	private static Color GetDiseaseColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (Grid.DiseaseIdx[cell] != 255)
		{
			Disease disease = Db.Get().Diseases[(int)Grid.DiseaseIdx[cell]];
			color = GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName);
			color.a = SimUtil.DiseaseCountToAlpha(Grid.DiseaseCount[cell]);
		}
		else
		{
			color.a = 0f;
		}
		return color;
	}

	// Token: 0x06005341 RID: 21313 RVA: 0x001E4A59 File Offset: 0x001E2C59
	private static Color GetHeatFlowColour(SimDebugView instance, int cell)
	{
		return instance.NormalizedHeatFlow(cell);
	}

	// Token: 0x06005342 RID: 21314 RVA: 0x001E4A62 File Offset: 0x001E2C62
	private static Color GetBlack(SimDebugView instance, int cell)
	{
		return Color.black;
	}

	// Token: 0x06005343 RID: 21315 RVA: 0x001E4A6C File Offset: 0x001E2C6C
	public static Color GetLightColour(SimDebugView instance, int cell)
	{
		Color color = GlobalAssets.Instance.colorSet.lightOverlay;
		color.a = Mathf.Clamp(Mathf.Sqrt((float)(Grid.LightIntensity[cell] + LightGridManager.previewLux[cell])) / Mathf.Sqrt(80000f), 0f, 1f);
		if (Grid.LightIntensity[cell] > DUPLICANTSTATS.STANDARD.Light.LUX_SUNBURN)
		{
			float num = ((float)Grid.LightIntensity[cell] + (float)LightGridManager.previewLux[cell] - (float)DUPLICANTSTATS.STANDARD.Light.LUX_SUNBURN) / (float)(80000 - DUPLICANTSTATS.STANDARD.Light.LUX_SUNBURN);
			num /= 10f;
			color.r += Mathf.Min(0.1f, PerlinSimplexNoise.noise(Grid.CellToPos2D(cell).x / 8f, Grid.CellToPos2D(cell).y / 8f + (float)instance.currentFrame / 32f) * num);
		}
		return color;
	}

	// Token: 0x06005344 RID: 21316 RVA: 0x001E4B7C File Offset: 0x001E2D7C
	public static Color GetRadiationColour(SimDebugView instance, int cell)
	{
		float num = Mathf.Clamp(Mathf.Sqrt(Grid.Radiation[cell]) / 30f, 0f, 1f);
		return new Color(0.2f, 0.9f, 0.3f, num);
	}

	// Token: 0x06005345 RID: 21317 RVA: 0x001E4BC4 File Offset: 0x001E2DC4
	public static Color GetRoomsColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (Grid.IsValidCell(instance.selectedCell))
		{
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(cell);
			if (cavityForCell != null && cavityForCell.room != null)
			{
				Room room = cavityForCell.room;
				color = GlobalAssets.Instance.colorSet.GetColorByName(room.roomType.category.colorName);
				color.a = 0.45f;
				if (Game.Instance.roomProber.GetCavityForCell(instance.selectedCell) == cavityForCell)
				{
					color.a += 0.3f;
				}
			}
		}
		return color;
	}

	// Token: 0x06005346 RID: 21318 RVA: 0x001E4C64 File Offset: 0x001E2E64
	public static Color GetJoulesColour(SimDebugView instance, int cell)
	{
		float num = Grid.Element[cell].specificHeatCapacity * Grid.Temperature[cell] * (Grid.Mass[cell] * 1000f);
		float num2 = 0.5f * num / (ElementLoader.FindElementByHash(SimHashes.SandStone).specificHeatCapacity * 294f * 1000000f);
		return Color.Lerp(Color.black, Color.red, num2);
	}

	// Token: 0x06005347 RID: 21319 RVA: 0x001E4CD0 File Offset: 0x001E2ED0
	public static Color GetNormalizedTemperatureColourMode(SimDebugView instance, int cell)
	{
		switch (Game.Instance.temperatureOverlayMode)
		{
		case Game.TemperatureOverlayModes.AbsoluteTemperature:
			return SimDebugView.GetNormalizedTemperatureColour(instance, cell);
		case Game.TemperatureOverlayModes.AdaptiveTemperature:
			return SimDebugView.GetNormalizedTemperatureColour(instance, cell);
		case Game.TemperatureOverlayModes.HeatFlow:
			return SimDebugView.GetHeatFlowColour(instance, cell);
		case Game.TemperatureOverlayModes.StateChange:
			return SimDebugView.GetStateChangeProximityColour(instance, cell);
		default:
			return SimDebugView.GetNormalizedTemperatureColour(instance, cell);
		}
	}

	// Token: 0x06005348 RID: 21320 RVA: 0x001E4D28 File Offset: 0x001E2F28
	public static Color GetStateChangeProximityColour(SimDebugView instance, int cell)
	{
		float num = Grid.Temperature[cell];
		Element element = Grid.Element[cell];
		float num2 = element.lowTemp;
		float num3 = element.highTemp;
		if (element.IsGas)
		{
			num3 = Mathf.Min(num2 + 150f, num3);
			return SimDebugView.GasTemperatureToColor(num, num2, num3);
		}
		if (element.IsSolid)
		{
			num2 = Mathf.Max(num3 - 150f, num2);
			return SimDebugView.SolidTemperatureToColor(num, num2, num3);
		}
		return SimDebugView.TemperatureToColor(num, num2, num3);
	}

	// Token: 0x06005349 RID: 21321 RVA: 0x001E4DA0 File Offset: 0x001E2FA0
	public static Color GetNormalizedTemperatureColour(SimDebugView instance, int cell)
	{
		float num = Grid.Temperature[cell];
		return instance.NormalizedTemperature(num);
	}

	// Token: 0x0600534A RID: 21322 RVA: 0x001E4DC0 File Offset: 0x001E2FC0
	private static Color GetGameGridColour(SimDebugView instance, int cell)
	{
		Color color = new Color32(0, 0, 0, byte.MaxValue);
		switch (instance.gameGridMode)
		{
		case SimDebugView.GameGridMode.GameSolidMap:
			color = (Grid.Solid[cell] ? Color.white : Color.black);
			break;
		case SimDebugView.GameGridMode.Lighting:
			color = ((Grid.LightCount[cell] > 0 || LightGridManager.previewLux[cell] > 0) ? Color.white : Color.black);
			break;
		case SimDebugView.GameGridMode.DigAmount:
			if (Grid.Element[cell].IsSolid)
			{
				float num = Grid.Damage[cell] / 255f;
				color = Color.HSVToRGB(1f - num, 1f, 1f);
			}
			break;
		case SimDebugView.GameGridMode.DupePassable:
			color = (Grid.DupePassable[cell] ? Color.white : Color.black);
			break;
		}
		return color;
	}

	// Token: 0x0600534B RID: 21323 RVA: 0x001E4EA1 File Offset: 0x001E30A1
	public Color32 GetColourForID(int id)
	{
		return this.networkColours[id % this.networkColours.Length];
	}

	// Token: 0x0600534C RID: 21324 RVA: 0x001E4EB8 File Offset: 0x001E30B8
	private static Color GetThermalConductivityColour(SimDebugView instance, int cell)
	{
		bool flag = SimDebugView.IsInsulated(cell);
		Color black = Color.black;
		float num = instance.maxThermalConductivity - instance.minThermalConductivity;
		if (!flag && num != 0f)
		{
			float num2 = (Grid.Element[cell].thermalConductivity - instance.minThermalConductivity) / num;
			num2 = Mathf.Max(num2, 0f);
			num2 = Mathf.Min(num2, 1f);
			black = new Color(num2, num2, num2);
		}
		return black;
	}

	// Token: 0x0600534D RID: 21325 RVA: 0x001E4F24 File Offset: 0x001E3124
	private static Color GetPressureMapColour(SimDebugView instance, int cell)
	{
		Color32 color = Color.black;
		if (Grid.Pressure[cell] > 0f)
		{
			float num = Mathf.Clamp((Grid.Pressure[cell] - instance.minPressureExpected) / (instance.maxPressureExpected - instance.minPressureExpected), 0f, 1f) * 0.9f;
			color = new Color(num, num, num, 1f);
		}
		return color;
	}

	// Token: 0x0600534E RID: 21326 RVA: 0x001E4F9C File Offset: 0x001E319C
	private static Color GetOxygenMapColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (!Grid.IsLiquid(cell) && !Grid.Solid[cell])
		{
			if (Grid.Mass[cell] > SimDebugView.minimumBreathable && (Grid.Element[cell].id == SimHashes.Oxygen || Grid.Element[cell].id == SimHashes.ContaminatedOxygen))
			{
				float num = Mathf.Clamp((Grid.Mass[cell] - SimDebugView.minimumBreathable) / SimDebugView.optimallyBreathable, 0f, 1f);
				color = instance.breathableGradient.Evaluate(num);
			}
			else
			{
				color = instance.unbreathableColour;
			}
		}
		return color;
	}

	// Token: 0x0600534F RID: 21327 RVA: 0x001E5044 File Offset: 0x001E3244
	private static Color GetTileColour(SimDebugView instance, int cell)
	{
		float num = 0.33f;
		Color color = new Color(num, num, num);
		Element element = Grid.Element[cell];
		bool flag = false;
		foreach (Tag tag in Game.Instance.tileOverlayFilters)
		{
			if (element.HasTag(tag))
			{
				flag = true;
			}
		}
		if (flag)
		{
			color = element.substance.uiColour;
		}
		return color;
	}

	// Token: 0x06005350 RID: 21328 RVA: 0x001E50D4 File Offset: 0x001E32D4
	private static Color GetTileTypeColour(SimDebugView instance, int cell)
	{
		return Grid.Element[cell].substance.uiColour;
	}

	// Token: 0x06005351 RID: 21329 RVA: 0x001E50EC File Offset: 0x001E32EC
	private static Color GetStateMapColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		switch (Grid.Element[cell].state & Element.State.Solid)
		{
		case Element.State.Gas:
			color = Color.yellow;
			break;
		case Element.State.Liquid:
			color = Color.green;
			break;
		case Element.State.Solid:
			color = Color.blue;
			break;
		}
		return color;
	}

	// Token: 0x06005352 RID: 21330 RVA: 0x001E5140 File Offset: 0x001E3340
	private static Color GetSolidLiquidMapColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		switch (Grid.Element[cell].state & Element.State.Solid)
		{
		case Element.State.Liquid:
			color = Color.green;
			break;
		case Element.State.Solid:
			color = Color.blue;
			break;
		}
		return color;
	}

	// Token: 0x06005353 RID: 21331 RVA: 0x001E518C File Offset: 0x001E338C
	private static Color GetStateChangeColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		Element element = Grid.Element[cell];
		if (!element.IsVacuum)
		{
			float num = Grid.Temperature[cell];
			float num2 = element.lowTemp * 0.05f;
			float num3 = Mathf.Abs(num - element.lowTemp) / num2;
			float num4 = element.highTemp * 0.05f;
			float num5 = Mathf.Abs(num - element.highTemp) / num4;
			float num6 = Mathf.Max(0f, 1f - Mathf.Min(num3, num5));
			color = Color.Lerp(Color.black, Color.red, num6);
		}
		return color;
	}

	// Token: 0x06005354 RID: 21332 RVA: 0x001E5224 File Offset: 0x001E3424
	private static Color GetDecorColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (!Grid.Solid[cell])
		{
			float num = GameUtil.GetDecorAtCell(cell) / 100f;
			if (num > 0f)
			{
				color = Color.Lerp(GlobalAssets.Instance.colorSet.decorBaseline, GlobalAssets.Instance.colorSet.decorPositive, Mathf.Abs(num));
			}
			else
			{
				color = Color.Lerp(GlobalAssets.Instance.colorSet.decorBaseline, GlobalAssets.Instance.colorSet.decorNegative, Mathf.Abs(num));
			}
		}
		return color;
	}

	// Token: 0x06005355 RID: 21333 RVA: 0x001E52C4 File Offset: 0x001E34C4
	private static Color GetDangerColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		SimDebugView.DangerAmount dangerAmount = SimDebugView.DangerAmount.None;
		if (!Grid.Element[cell].IsSolid)
		{
			float num = 0f;
			if (Grid.Temperature[cell] < SimDebugView.minMinionTemperature)
			{
				num = Mathf.Abs(Grid.Temperature[cell] - SimDebugView.minMinionTemperature);
			}
			if (Grid.Temperature[cell] > SimDebugView.maxMinionTemperature)
			{
				num = Mathf.Abs(Grid.Temperature[cell] - SimDebugView.maxMinionTemperature);
			}
			if (num > 0f)
			{
				if (num < 10f)
				{
					dangerAmount = SimDebugView.DangerAmount.VeryLow;
				}
				else if (num < 30f)
				{
					dangerAmount = SimDebugView.DangerAmount.Low;
				}
				else if (num < 100f)
				{
					dangerAmount = SimDebugView.DangerAmount.Moderate;
				}
				else if (num < 200f)
				{
					dangerAmount = SimDebugView.DangerAmount.High;
				}
				else if (num < 400f)
				{
					dangerAmount = SimDebugView.DangerAmount.VeryHigh;
				}
				else if (num > 800f)
				{
					dangerAmount = SimDebugView.DangerAmount.Extreme;
				}
			}
		}
		if (dangerAmount < SimDebugView.DangerAmount.VeryHigh && (Grid.Element[cell].IsVacuum || (Grid.Element[cell].IsGas && (Grid.Element[cell].id != SimHashes.Oxygen || Grid.Pressure[cell] < SimDebugView.minMinionPressure))))
		{
			dangerAmount++;
		}
		if (dangerAmount != SimDebugView.DangerAmount.None)
		{
			float num2 = (float)dangerAmount / 6f;
			color = Color.HSVToRGB((80f - num2 * 80f) / 360f, 1f, 1f);
		}
		return color;
	}

	// Token: 0x06005356 RID: 21334 RVA: 0x001E540C File Offset: 0x001E360C
	private static Color GetSimCheckErrorMapColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		Element element = Grid.Element[cell];
		float num = Grid.Mass[cell];
		float num2 = Grid.Temperature[cell];
		if (float.IsNaN(num) || float.IsNaN(num2) || num > 10000f || num2 > 10000f)
		{
			return Color.red;
		}
		if (element.IsVacuum)
		{
			if (num2 != 0f)
			{
				color = Color.yellow;
			}
			else if (num != 0f)
			{
				color = Color.blue;
			}
			else
			{
				color = Color.gray;
			}
		}
		else if (num2 < 10f)
		{
			color = Color.red;
		}
		else if (Grid.Mass[cell] < 1f && Grid.Pressure[cell] < 1f)
		{
			color = Color.green;
		}
		else if (num2 > element.highTemp + 3f && element.highTempTransition != null)
		{
			color = Color.magenta;
		}
		else if (num2 < element.lowTemp + 3f && element.lowTempTransition != null)
		{
			color = Color.cyan;
		}
		return color;
	}

	// Token: 0x06005357 RID: 21335 RVA: 0x001E5514 File Offset: 0x001E3714
	private static Color GetFakeFloorColour(SimDebugView instance, int cell)
	{
		if (!Grid.FakeFloor[cell])
		{
			return Color.black;
		}
		return Color.cyan;
	}

	// Token: 0x06005358 RID: 21336 RVA: 0x001E552E File Offset: 0x001E372E
	private static Color GetFoundationColour(SimDebugView instance, int cell)
	{
		if (!Grid.Foundation[cell])
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x06005359 RID: 21337 RVA: 0x001E5548 File Offset: 0x001E3748
	private static Color GetDupePassableColour(SimDebugView instance, int cell)
	{
		if (!Grid.DupePassable[cell])
		{
			return Color.black;
		}
		return Color.green;
	}

	// Token: 0x0600535A RID: 21338 RVA: 0x001E5562 File Offset: 0x001E3762
	private static Color GetCritterImpassableColour(SimDebugView instance, int cell)
	{
		if (!Grid.CritterImpassable[cell])
		{
			return Color.black;
		}
		return Color.yellow;
	}

	// Token: 0x0600535B RID: 21339 RVA: 0x001E557C File Offset: 0x001E377C
	private static Color GetDupeImpassableColour(SimDebugView instance, int cell)
	{
		if (!Grid.DupeImpassable[cell])
		{
			return Color.black;
		}
		return Color.red;
	}

	// Token: 0x0600535C RID: 21340 RVA: 0x001E5596 File Offset: 0x001E3796
	private static Color GetMinionOccupiedColour(SimDebugView instance, int cell)
	{
		if (!(Grid.Objects[cell, 0] != null))
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x0600535D RID: 21341 RVA: 0x001E55B7 File Offset: 0x001E37B7
	private static Color GetMinionGroupProberColour(SimDebugView instance, int cell)
	{
		if (!MinionGroupProber.Get().IsReachable(cell))
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x0600535E RID: 21342 RVA: 0x001E55D1 File Offset: 0x001E37D1
	private static Color GetPathProberColour(SimDebugView instance, int cell)
	{
		if (!(instance.selectedPathProber != null) || instance.selectedPathProber.GetCost(cell) == -1)
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x0600535F RID: 21343 RVA: 0x001E55FB File Offset: 0x001E37FB
	private static Color GetReservedColour(SimDebugView instance, int cell)
	{
		if (!Grid.Reserved[cell])
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x06005360 RID: 21344 RVA: 0x001E5615 File Offset: 0x001E3815
	private static Color GetAllowPathFindingColour(SimDebugView instance, int cell)
	{
		if (!Grid.AllowPathfinding[cell])
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x06005361 RID: 21345 RVA: 0x001E5630 File Offset: 0x001E3830
	private static Color GetMassColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (!SimDebugView.IsInsulated(cell))
		{
			float num = Grid.Mass[cell];
			if (num > 0f)
			{
				float num2 = (num - SimDebugView.Instance.minMassExpected) / (SimDebugView.Instance.maxMassExpected - SimDebugView.Instance.minMassExpected);
				color = Color.HSVToRGB(1f - num2, 1f, 1f);
			}
		}
		return color;
	}

	// Token: 0x06005362 RID: 21346 RVA: 0x001E569A File Offset: 0x001E389A
	public static Color GetScenePartitionerColour(SimDebugView instance, int cell)
	{
		if (!GameScenePartitioner.Instance.DoDebugLayersContainItemsOnCell(cell))
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x040037E8 RID: 14312
	[SerializeField]
	public Material material;

	// Token: 0x040037E9 RID: 14313
	public Material diseaseMaterial;

	// Token: 0x040037EA RID: 14314
	public bool hideFOW;

	// Token: 0x040037EB RID: 14315
	public const int colourSize = 4;

	// Token: 0x040037EC RID: 14316
	private byte[] texBytes;

	// Token: 0x040037ED RID: 14317
	private int currentFrame;

	// Token: 0x040037EE RID: 14318
	[SerializeField]
	private Texture2D tex;

	// Token: 0x040037EF RID: 14319
	[SerializeField]
	private GameObject plane;

	// Token: 0x040037F0 RID: 14320
	private HashedString mode = global::OverlayModes.Power.ID;

	// Token: 0x040037F1 RID: 14321
	private SimDebugView.GameGridMode gameGridMode = SimDebugView.GameGridMode.DigAmount;

	// Token: 0x040037F2 RID: 14322
	private PathProber selectedPathProber;

	// Token: 0x040037F3 RID: 14323
	public float minTempExpected = 173.15f;

	// Token: 0x040037F4 RID: 14324
	public float maxTempExpected = 423.15f;

	// Token: 0x040037F5 RID: 14325
	public float minMassExpected = 1.0001f;

	// Token: 0x040037F6 RID: 14326
	public float maxMassExpected = 10000f;

	// Token: 0x040037F7 RID: 14327
	public float minPressureExpected = 1.300003f;

	// Token: 0x040037F8 RID: 14328
	public float maxPressureExpected = 201.3f;

	// Token: 0x040037F9 RID: 14329
	public float minThermalConductivity;

	// Token: 0x040037FA RID: 14330
	public float maxThermalConductivity = 30f;

	// Token: 0x040037FB RID: 14331
	public float thresholdRange = 0.001f;

	// Token: 0x040037FC RID: 14332
	public float thresholdOpacity = 0.8f;

	// Token: 0x040037FD RID: 14333
	public static float minimumBreathable = 0.05f;

	// Token: 0x040037FE RID: 14334
	public static float optimallyBreathable = 1f;

	// Token: 0x040037FF RID: 14335
	public SimDebugView.ColorThreshold[] temperatureThresholds;

	// Token: 0x04003800 RID: 14336
	public Vector2 user_temperatureThresholds = Vector2.zero;

	// Token: 0x04003801 RID: 14337
	public SimDebugView.ColorThreshold[] heatFlowThresholds;

	// Token: 0x04003802 RID: 14338
	public Color32[] networkColours;

	// Token: 0x04003803 RID: 14339
	public Gradient breathableGradient = new Gradient();

	// Token: 0x04003804 RID: 14340
	public Color32 unbreathableColour = new Color(0.5f, 0f, 0f);

	// Token: 0x04003805 RID: 14341
	public Color32[] toxicColour = new Color32[]
	{
		new Color(0.5f, 0f, 0.5f),
		new Color(1f, 0f, 1f)
	};

	// Token: 0x04003806 RID: 14342
	public static SimDebugView Instance;

	// Token: 0x04003807 RID: 14343
	private WorkItemCollection<SimDebugView.UpdateSimViewWorkItem, SimDebugView.UpdateSimViewSharedData> updateSimViewWorkItems = new WorkItemCollection<SimDebugView.UpdateSimViewWorkItem, SimDebugView.UpdateSimViewSharedData>();

	// Token: 0x04003808 RID: 14344
	private int selectedCell;

	// Token: 0x04003809 RID: 14345
	private Dictionary<HashedString, Action<SimDebugView, Texture>> dataUpdateFuncs = new Dictionary<HashedString, Action<SimDebugView, Texture>>
	{
		{
			global::OverlayModes.Temperature.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDefaultBilinear)
		},
		{
			global::OverlayModes.Oxygen.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDefaultBilinear)
		},
		{
			global::OverlayModes.Decor.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDefaultBilinear)
		},
		{
			global::OverlayModes.TileMode.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDefaultPoint)
		},
		{
			global::OverlayModes.Disease.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDisease)
		}
	};

	// Token: 0x0400380A RID: 14346
	private static float[] relativeTemperatureColorIntervals = new float[] { 0.4f, 0.05f, 0.05f, 0.05f, 0.05f, 0.2f, 0.2f };

	// Token: 0x0400380B RID: 14347
	private static float[] absoluteTemperatureColorIntervals = new float[] { 273.15f, 10f, 10f, 10f, 7f, 63f, 1700f, 10000f };

	// Token: 0x0400380C RID: 14348
	private Dictionary<HashedString, Func<SimDebugView, int, Color>> getColourFuncs = new Dictionary<HashedString, Func<SimDebugView, int, Color>>
	{
		{
			global::OverlayModes.ThermalConductivity.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetThermalConductivityColour)
		},
		{
			global::OverlayModes.Temperature.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetNormalizedTemperatureColourMode)
		},
		{
			global::OverlayModes.Disease.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDiseaseColour)
		},
		{
			global::OverlayModes.Decor.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDecorColour)
		},
		{
			global::OverlayModes.Oxygen.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetOxygenMapColour)
		},
		{
			global::OverlayModes.Light.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetLightColour)
		},
		{
			global::OverlayModes.Radiation.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetRadiationColour)
		},
		{
			global::OverlayModes.Rooms.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetRoomsColour)
		},
		{
			global::OverlayModes.TileMode.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetTileColour)
		},
		{
			global::OverlayModes.Suit.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetBlack)
		},
		{
			global::OverlayModes.Priorities.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetBlack)
		},
		{
			global::OverlayModes.Crop.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetBlack)
		},
		{
			global::OverlayModes.Harvest.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetBlack)
		},
		{
			SimDebugView.OverlayModes.GameGrid,
			new Func<SimDebugView, int, Color>(SimDebugView.GetGameGridColour)
		},
		{
			SimDebugView.OverlayModes.StateChange,
			new Func<SimDebugView, int, Color>(SimDebugView.GetStateChangeColour)
		},
		{
			SimDebugView.OverlayModes.SimCheckErrorMap,
			new Func<SimDebugView, int, Color>(SimDebugView.GetSimCheckErrorMapColour)
		},
		{
			SimDebugView.OverlayModes.Foundation,
			new Func<SimDebugView, int, Color>(SimDebugView.GetFoundationColour)
		},
		{
			SimDebugView.OverlayModes.FakeFloor,
			new Func<SimDebugView, int, Color>(SimDebugView.GetFakeFloorColour)
		},
		{
			SimDebugView.OverlayModes.DupePassable,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDupePassableColour)
		},
		{
			SimDebugView.OverlayModes.DupeImpassable,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDupeImpassableColour)
		},
		{
			SimDebugView.OverlayModes.CritterImpassable,
			new Func<SimDebugView, int, Color>(SimDebugView.GetCritterImpassableColour)
		},
		{
			SimDebugView.OverlayModes.MinionGroupProber,
			new Func<SimDebugView, int, Color>(SimDebugView.GetMinionGroupProberColour)
		},
		{
			SimDebugView.OverlayModes.PathProber,
			new Func<SimDebugView, int, Color>(SimDebugView.GetPathProberColour)
		},
		{
			SimDebugView.OverlayModes.Reserved,
			new Func<SimDebugView, int, Color>(SimDebugView.GetReservedColour)
		},
		{
			SimDebugView.OverlayModes.AllowPathFinding,
			new Func<SimDebugView, int, Color>(SimDebugView.GetAllowPathFindingColour)
		},
		{
			SimDebugView.OverlayModes.Danger,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDangerColour)
		},
		{
			SimDebugView.OverlayModes.MinionOccupied,
			new Func<SimDebugView, int, Color>(SimDebugView.GetMinionOccupiedColour)
		},
		{
			SimDebugView.OverlayModes.Pressure,
			new Func<SimDebugView, int, Color>(SimDebugView.GetPressureMapColour)
		},
		{
			SimDebugView.OverlayModes.TileType,
			new Func<SimDebugView, int, Color>(SimDebugView.GetTileTypeColour)
		},
		{
			SimDebugView.OverlayModes.State,
			new Func<SimDebugView, int, Color>(SimDebugView.GetStateMapColour)
		},
		{
			SimDebugView.OverlayModes.SolidLiquid,
			new Func<SimDebugView, int, Color>(SimDebugView.GetSolidLiquidMapColour)
		},
		{
			SimDebugView.OverlayModes.Mass,
			new Func<SimDebugView, int, Color>(SimDebugView.GetMassColour)
		},
		{
			SimDebugView.OverlayModes.Joules,
			new Func<SimDebugView, int, Color>(SimDebugView.GetJoulesColour)
		},
		{
			SimDebugView.OverlayModes.ScenePartitioner,
			new Func<SimDebugView, int, Color>(SimDebugView.GetScenePartitionerColour)
		}
	};

	// Token: 0x0400380D RID: 14349
	public static readonly Color[] dbColours = new Color[]
	{
		new Color(0f, 0f, 0f, 0f),
		new Color(1f, 1f, 1f, 0.3f),
		new Color(0.7058824f, 0.8235294f, 1f, 0.2f),
		new Color(0f, 0.3137255f, 1f, 0.3f),
		new Color(0.7058824f, 1f, 0.7058824f, 0.5f),
		new Color(0.078431375f, 1f, 0f, 0.7f),
		new Color(1f, 0.9019608f, 0.7058824f, 0.9f),
		new Color(1f, 0.8235294f, 0f, 0.9f),
		new Color(1f, 0.7176471f, 0.3019608f, 0.9f),
		new Color(1f, 0.41568628f, 0f, 0.9f),
		new Color(1f, 0.7058824f, 0.7058824f, 1f),
		new Color(1f, 0f, 0f, 1f),
		new Color(1f, 0f, 0f, 1f)
	};

	// Token: 0x0400380E RID: 14350
	private static float minMinionTemperature = 260f;

	// Token: 0x0400380F RID: 14351
	private static float maxMinionTemperature = 310f;

	// Token: 0x04003810 RID: 14352
	private static float minMinionPressure = 80f;

	// Token: 0x02001C12 RID: 7186
	public static class OverlayModes
	{
		// Token: 0x040084EE RID: 34030
		public static readonly HashedString Mass = "Mass";

		// Token: 0x040084EF RID: 34031
		public static readonly HashedString Pressure = "Pressure";

		// Token: 0x040084F0 RID: 34032
		public static readonly HashedString GameGrid = "GameGrid";

		// Token: 0x040084F1 RID: 34033
		public static readonly HashedString ScenePartitioner = "ScenePartitioner";

		// Token: 0x040084F2 RID: 34034
		public static readonly HashedString ConduitUpdates = "ConduitUpdates";

		// Token: 0x040084F3 RID: 34035
		public static readonly HashedString Flow = "Flow";

		// Token: 0x040084F4 RID: 34036
		public static readonly HashedString StateChange = "StateChange";

		// Token: 0x040084F5 RID: 34037
		public static readonly HashedString SimCheckErrorMap = "SimCheckErrorMap";

		// Token: 0x040084F6 RID: 34038
		public static readonly HashedString DupePassable = "DupePassable";

		// Token: 0x040084F7 RID: 34039
		public static readonly HashedString Foundation = "Foundation";

		// Token: 0x040084F8 RID: 34040
		public static readonly HashedString FakeFloor = "FakeFloor";

		// Token: 0x040084F9 RID: 34041
		public static readonly HashedString CritterImpassable = "CritterImpassable";

		// Token: 0x040084FA RID: 34042
		public static readonly HashedString DupeImpassable = "DupeImpassable";

		// Token: 0x040084FB RID: 34043
		public static readonly HashedString MinionGroupProber = "MinionGroupProber";

		// Token: 0x040084FC RID: 34044
		public static readonly HashedString PathProber = "PathProber";

		// Token: 0x040084FD RID: 34045
		public static readonly HashedString Reserved = "Reserved";

		// Token: 0x040084FE RID: 34046
		public static readonly HashedString AllowPathFinding = "AllowPathFinding";

		// Token: 0x040084FF RID: 34047
		public static readonly HashedString Danger = "Danger";

		// Token: 0x04008500 RID: 34048
		public static readonly HashedString MinionOccupied = "MinionOccupied";

		// Token: 0x04008501 RID: 34049
		public static readonly HashedString TileType = "TileType";

		// Token: 0x04008502 RID: 34050
		public static readonly HashedString State = "State";

		// Token: 0x04008503 RID: 34051
		public static readonly HashedString SolidLiquid = "SolidLiquid";

		// Token: 0x04008504 RID: 34052
		public static readonly HashedString Joules = "Joules";
	}

	// Token: 0x02001C13 RID: 7187
	public enum GameGridMode
	{
		// Token: 0x04008506 RID: 34054
		GameSolidMap,
		// Token: 0x04008507 RID: 34055
		Lighting,
		// Token: 0x04008508 RID: 34056
		RoomMap,
		// Token: 0x04008509 RID: 34057
		Style,
		// Token: 0x0400850A RID: 34058
		PlantDensity,
		// Token: 0x0400850B RID: 34059
		DigAmount,
		// Token: 0x0400850C RID: 34060
		DupePassable
	}

	// Token: 0x02001C14 RID: 7188
	[Serializable]
	public struct ColorThreshold
	{
		// Token: 0x0400850D RID: 34061
		public string colorName;

		// Token: 0x0400850E RID: 34062
		public float value;
	}

	// Token: 0x02001C15 RID: 7189
	private struct UpdateSimViewSharedData
	{
		// Token: 0x0600A98D RID: 43405 RVA: 0x003B832E File Offset: 0x003B652E
		public UpdateSimViewSharedData(SimDebugView instance, byte[] texture_bytes, HashedString sim_view_mode, SimDebugView sim_debug_view)
		{
			this.instance = instance;
			this.textureBytes = texture_bytes;
			this.simViewMode = sim_view_mode;
			this.simDebugView = sim_debug_view;
		}

		// Token: 0x0400850F RID: 34063
		public SimDebugView instance;

		// Token: 0x04008510 RID: 34064
		public HashedString simViewMode;

		// Token: 0x04008511 RID: 34065
		public SimDebugView simDebugView;

		// Token: 0x04008512 RID: 34066
		public byte[] textureBytes;
	}

	// Token: 0x02001C16 RID: 7190
	private struct UpdateSimViewWorkItem : IWorkItem<SimDebugView.UpdateSimViewSharedData>
	{
		// Token: 0x0600A98E RID: 43406 RVA: 0x003B8350 File Offset: 0x003B6550
		public UpdateSimViewWorkItem(int x0, int y0, int x1, int y1)
		{
			this.x0 = Mathf.Clamp(x0, 0, Grid.WidthInCells - 1);
			this.x1 = Mathf.Clamp(x1, 0, Grid.WidthInCells - 1);
			this.y0 = Mathf.Clamp(y0, 0, Grid.HeightInCells - 1);
			this.y1 = Mathf.Clamp(y1, 0, Grid.HeightInCells - 1);
		}

		// Token: 0x0600A98F RID: 43407 RVA: 0x003B83B0 File Offset: 0x003B65B0
		public void Run(SimDebugView.UpdateSimViewSharedData shared_data, int threadIndex)
		{
			Func<SimDebugView, int, Color> func;
			if (!shared_data.instance.getColourFuncs.TryGetValue(shared_data.simViewMode, out func))
			{
				func = new Func<SimDebugView, int, Color>(SimDebugView.GetBlack);
			}
			for (int i = this.y0; i <= this.y1; i++)
			{
				int num = Grid.XYToCell(this.x0, i);
				int num2 = Grid.XYToCell(this.x1, i);
				for (int j = num; j <= num2; j++)
				{
					int num3 = j * 4;
					if (Grid.IsActiveWorld(j))
					{
						Color color = func(shared_data.instance, j);
						shared_data.textureBytes[num3] = (byte)(Mathf.Min(color.r, 1f) * 255f);
						shared_data.textureBytes[num3 + 1] = (byte)(Mathf.Min(color.g, 1f) * 255f);
						shared_data.textureBytes[num3 + 2] = (byte)(Mathf.Min(color.b, 1f) * 255f);
						shared_data.textureBytes[num3 + 3] = (byte)(Mathf.Min(color.a, 1f) * 255f);
					}
					else
					{
						shared_data.textureBytes[num3] = 0;
						shared_data.textureBytes[num3 + 1] = 0;
						shared_data.textureBytes[num3 + 2] = 0;
						shared_data.textureBytes[num3 + 3] = 0;
					}
				}
			}
		}

		// Token: 0x04008513 RID: 34067
		private int x0;

		// Token: 0x04008514 RID: 34068
		private int y0;

		// Token: 0x04008515 RID: 34069
		private int x1;

		// Token: 0x04008516 RID: 34070
		private int y1;
	}

	// Token: 0x02001C17 RID: 7191
	public enum DangerAmount
	{
		// Token: 0x04008518 RID: 34072
		None,
		// Token: 0x04008519 RID: 34073
		VeryLow,
		// Token: 0x0400851A RID: 34074
		Low,
		// Token: 0x0400851B RID: 34075
		Moderate,
		// Token: 0x0400851C RID: 34076
		High,
		// Token: 0x0400851D RID: 34077
		VeryHigh,
		// Token: 0x0400851E RID: 34078
		Extreme,
		// Token: 0x0400851F RID: 34079
		MAX_DANGERAMOUNT = 6
	}
}
