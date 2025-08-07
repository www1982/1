using System;
using UnityEngine;

// Token: 0x02000AAF RID: 2735
public class LightingSettings : ScriptableObject
{
	// Token: 0x040034F2 RID: 13554
	[Header("Global")]
	public bool UpdateLightSettings;

	// Token: 0x040034F3 RID: 13555
	public float BloomScale;

	// Token: 0x040034F4 RID: 13556
	public Color32 LightColour = Color.white;

	// Token: 0x040034F5 RID: 13557
	[Header("Digging")]
	public float DigMapScale;

	// Token: 0x040034F6 RID: 13558
	public Color DigMapColour;

	// Token: 0x040034F7 RID: 13559
	public Texture2D DigDamageMap;

	// Token: 0x040034F8 RID: 13560
	[Header("State Transition")]
	public Texture2D StateTransitionMap;

	// Token: 0x040034F9 RID: 13561
	public Color StateTransitionColor;

	// Token: 0x040034FA RID: 13562
	public float StateTransitionUVScale;

	// Token: 0x040034FB RID: 13563
	public Vector2 StateTransitionUVOffsetRate;

	// Token: 0x040034FC RID: 13564
	[Header("Falling Solids")]
	public Texture2D FallingSolidMap;

	// Token: 0x040034FD RID: 13565
	public Color FallingSolidColor;

	// Token: 0x040034FE RID: 13566
	public float FallingSolidUVScale;

	// Token: 0x040034FF RID: 13567
	public Vector2 FallingSolidUVOffsetRate;

	// Token: 0x04003500 RID: 13568
	[Header("Metal Shine")]
	public Vector2 ShineCenter;

	// Token: 0x04003501 RID: 13569
	public Vector2 ShineRange;

	// Token: 0x04003502 RID: 13570
	public float ShineZoomSpeed;

	// Token: 0x04003503 RID: 13571
	[Header("Water")]
	public Color WaterTrimColor;

	// Token: 0x04003504 RID: 13572
	public float WaterTrimSize;

	// Token: 0x04003505 RID: 13573
	public float WaterAlphaTrimSize;

	// Token: 0x04003506 RID: 13574
	public float WaterAlphaThreshold;

	// Token: 0x04003507 RID: 13575
	public float WaterCubesAlphaThreshold;

	// Token: 0x04003508 RID: 13576
	public float WaterWaveAmplitude;

	// Token: 0x04003509 RID: 13577
	public float WaterWaveFrequency;

	// Token: 0x0400350A RID: 13578
	public float WaterWaveSpeed;

	// Token: 0x0400350B RID: 13579
	public float WaterDetailSpeed;

	// Token: 0x0400350C RID: 13580
	public float WaterDetailTiling;

	// Token: 0x0400350D RID: 13581
	public float WaterDetailTiling2;

	// Token: 0x0400350E RID: 13582
	public Vector2 WaterDetailDirection;

	// Token: 0x0400350F RID: 13583
	public float WaterWaveAmplitude2;

	// Token: 0x04003510 RID: 13584
	public float WaterWaveFrequency2;

	// Token: 0x04003511 RID: 13585
	public float WaterWaveSpeed2;

	// Token: 0x04003512 RID: 13586
	public float WaterCubeMapScale;

	// Token: 0x04003513 RID: 13587
	public float WaterColorScale;

	// Token: 0x04003514 RID: 13588
	public float WaterDistortionScaleStart;

	// Token: 0x04003515 RID: 13589
	public float WaterDistortionScaleEnd;

	// Token: 0x04003516 RID: 13590
	public float WaterDepthColorOpacityStart;

	// Token: 0x04003517 RID: 13591
	public float WaterDepthColorOpacityEnd;

	// Token: 0x04003518 RID: 13592
	[Header("Liquid")]
	public float LiquidMin;

	// Token: 0x04003519 RID: 13593
	public float LiquidMax;

	// Token: 0x0400351A RID: 13594
	public float LiquidCutoff;

	// Token: 0x0400351B RID: 13595
	public float LiquidTransparency;

	// Token: 0x0400351C RID: 13596
	public float LiquidAmountOffset;

	// Token: 0x0400351D RID: 13597
	public float LiquidMaxMass;

	// Token: 0x0400351E RID: 13598
	[Header("Grid")]
	public float GridLineWidth;

	// Token: 0x0400351F RID: 13599
	public float GridSize;

	// Token: 0x04003520 RID: 13600
	public float GridMaxIntensity;

	// Token: 0x04003521 RID: 13601
	public float GridMinIntensity;

	// Token: 0x04003522 RID: 13602
	public Color GridColor;

	// Token: 0x04003523 RID: 13603
	[Header("Terrain")]
	public float EdgeGlowCutoffStart;

	// Token: 0x04003524 RID: 13604
	public float EdgeGlowCutoffEnd;

	// Token: 0x04003525 RID: 13605
	public float EdgeGlowIntensity;

	// Token: 0x04003526 RID: 13606
	public int BackgroundLayers;

	// Token: 0x04003527 RID: 13607
	public float BackgroundBaseParallax;

	// Token: 0x04003528 RID: 13608
	public float BackgroundLayerParallax;

	// Token: 0x04003529 RID: 13609
	public float BackgroundDarkening;

	// Token: 0x0400352A RID: 13610
	public float BackgroundClip;

	// Token: 0x0400352B RID: 13611
	public float BackgroundUVScale;

	// Token: 0x0400352C RID: 13612
	public global::LightingSettings.EdgeLighting substanceEdgeParameters;

	// Token: 0x0400352D RID: 13613
	public global::LightingSettings.EdgeLighting tileEdgeParameters;

	// Token: 0x0400352E RID: 13614
	public float AnimIntensity;

	// Token: 0x0400352F RID: 13615
	public float GasMinOpacity;

	// Token: 0x04003530 RID: 13616
	public float GasMaxOpacity;

	// Token: 0x04003531 RID: 13617
	public Color[] DarkenTints;

	// Token: 0x04003532 RID: 13618
	public global::LightingSettings.LightingColours characterLighting;

	// Token: 0x04003533 RID: 13619
	public Color BrightenOverlayColour;

	// Token: 0x04003534 RID: 13620
	public Color[] ColdColours;

	// Token: 0x04003535 RID: 13621
	public Color[] HotColours;

	// Token: 0x04003536 RID: 13622
	[Header("Temperature Overlay Effects")]
	public Vector4 TemperatureParallax;

	// Token: 0x04003537 RID: 13623
	public Texture2D EmberTex;

	// Token: 0x04003538 RID: 13624
	public Texture2D FrostTex;

	// Token: 0x04003539 RID: 13625
	public Texture2D Thermal1Tex;

	// Token: 0x0400353A RID: 13626
	public Texture2D Thermal2Tex;

	// Token: 0x0400353B RID: 13627
	public Vector2 ColdFGUVOffset;

	// Token: 0x0400353C RID: 13628
	public Vector2 ColdMGUVOffset;

	// Token: 0x0400353D RID: 13629
	public Vector2 ColdBGUVOffset;

	// Token: 0x0400353E RID: 13630
	public Vector2 HotFGUVOffset;

	// Token: 0x0400353F RID: 13631
	public Vector2 HotMGUVOffset;

	// Token: 0x04003540 RID: 13632
	public Vector2 HotBGUVOffset;

	// Token: 0x04003541 RID: 13633
	public Texture2D DustTex;

	// Token: 0x04003542 RID: 13634
	public Color DustColour;

	// Token: 0x04003543 RID: 13635
	public float DustScale;

	// Token: 0x04003544 RID: 13636
	public Vector3 DustMovement;

	// Token: 0x04003545 RID: 13637
	public float ShowGas;

	// Token: 0x04003546 RID: 13638
	public float ShowTemperature;

	// Token: 0x04003547 RID: 13639
	public float ShowDust;

	// Token: 0x04003548 RID: 13640
	public float ShowShadow;

	// Token: 0x04003549 RID: 13641
	public Vector4 HeatHazeParameters;

	// Token: 0x0400354A RID: 13642
	public Texture2D HeatHazeTexture;

	// Token: 0x0400354B RID: 13643
	[Header("Biome")]
	public float WorldZoneGasBlend;

	// Token: 0x0400354C RID: 13644
	public float WorldZoneLiquidBlend;

	// Token: 0x0400354D RID: 13645
	public float WorldZoneForegroundBlend;

	// Token: 0x0400354E RID: 13646
	public float WorldZoneSimpleAnimBlend;

	// Token: 0x0400354F RID: 13647
	public float WorldZoneAnimBlend;

	// Token: 0x04003550 RID: 13648
	[Header("FX")]
	public Color32 SmokeDamageTint;

	// Token: 0x04003551 RID: 13649
	[Header("Building Damage")]
	public Texture2D BuildingDamagedTex;

	// Token: 0x04003552 RID: 13650
	public Vector4 BuildingDamagedUVParameters;

	// Token: 0x04003553 RID: 13651
	[Header("Disease")]
	public Texture2D DiseaseOverlayTex;

	// Token: 0x04003554 RID: 13652
	public Vector4 DiseaseOverlayTexInfo;

	// Token: 0x04003555 RID: 13653
	[Header("Conduits")]
	public ConduitFlowVisualizer.Tuning GasConduit;

	// Token: 0x04003556 RID: 13654
	public ConduitFlowVisualizer.Tuning LiquidConduit;

	// Token: 0x04003557 RID: 13655
	public SolidConduitFlowVisualizer.Tuning SolidConduit;

	// Token: 0x04003558 RID: 13656
	[Header("Radiation Overlay")]
	public bool ShowRadiation;

	// Token: 0x04003559 RID: 13657
	public Texture2D Radiation1Tex;

	// Token: 0x0400355A RID: 13658
	public Texture2D Radiation2Tex;

	// Token: 0x0400355B RID: 13659
	public Texture2D Radiation3Tex;

	// Token: 0x0400355C RID: 13660
	public Texture2D Radiation4Tex;

	// Token: 0x0400355D RID: 13661
	public Texture2D NoiseTex;

	// Token: 0x0400355E RID: 13662
	public Color RadColor;

	// Token: 0x0400355F RID: 13663
	public Vector2 Rad1UVOffset;

	// Token: 0x04003560 RID: 13664
	public Vector2 Rad2UVOffset;

	// Token: 0x04003561 RID: 13665
	public Vector2 Rad3UVOffset;

	// Token: 0x04003562 RID: 13666
	public Vector2 Rad4UVOffset;

	// Token: 0x04003563 RID: 13667
	public Vector4 RadUVScales;

	// Token: 0x04003564 RID: 13668
	public Vector2 Rad1Range;

	// Token: 0x04003565 RID: 13669
	public Vector2 Rad2Range;

	// Token: 0x04003566 RID: 13670
	public Vector2 Rad3Range;

	// Token: 0x04003567 RID: 13671
	public Vector2 Rad4Range;

	// Token: 0x02001B8E RID: 7054
	[Serializable]
	public struct EdgeLighting
	{
		// Token: 0x04008330 RID: 33584
		public float intensity;

		// Token: 0x04008331 RID: 33585
		public float edgeIntensity;

		// Token: 0x04008332 RID: 33586
		public float directSunlightScale;

		// Token: 0x04008333 RID: 33587
		public float power;
	}

	// Token: 0x02001B8F RID: 7055
	public enum TintLayers
	{
		// Token: 0x04008335 RID: 33589
		Background,
		// Token: 0x04008336 RID: 33590
		Midground,
		// Token: 0x04008337 RID: 33591
		Foreground,
		// Token: 0x04008338 RID: 33592
		NumLayers
	}

	// Token: 0x02001B90 RID: 7056
	[Serializable]
	public struct LightingColours
	{
		// Token: 0x04008339 RID: 33593
		public Color32 litColour;

		// Token: 0x0400833A RID: 33594
		public Color32 unlitColour;
	}
}
