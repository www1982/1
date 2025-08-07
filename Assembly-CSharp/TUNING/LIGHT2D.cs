using System;
using UnityEngine;

namespace TUNING
{
	// Token: 0x02000F92 RID: 3986
	public class LIGHT2D
	{
		// Token: 0x04005C58 RID: 23640
		public const int SUNLIGHT_MAX_DEFAULT = 80000;

		// Token: 0x04005C59 RID: 23641
		public static readonly Color LIGHT_BLUE = new Color(0.38f, 0.61f, 1f, 1f);

		// Token: 0x04005C5A RID: 23642
		public static readonly Color LIGHT_PURPLE = new Color(0.9f, 0.4f, 0.74f, 1f);

		// Token: 0x04005C5B RID: 23643
		public static readonly Color LIGHT_PINK = new Color(0.9f, 0.4f, 0.6f, 1f);

		// Token: 0x04005C5C RID: 23644
		public static readonly Color LIGHT_YELLOW = new Color(0.57f, 0.55f, 0.44f, 1f);

		// Token: 0x04005C5D RID: 23645
		public static readonly Color LIGHT_OVERLAY = new Color(0.56f, 0.56f, 0.56f, 1f);

		// Token: 0x04005C5E RID: 23646
		public static readonly Vector2 DEFAULT_DIRECTION = new Vector2(0f, -1f);

		// Token: 0x04005C5F RID: 23647
		public const int FLOORLAMP_LUX = 1000;

		// Token: 0x04005C60 RID: 23648
		public const float FLOORLAMP_RANGE = 4f;

		// Token: 0x04005C61 RID: 23649
		public const float FLOORLAMP_ANGLE = 0f;

		// Token: 0x04005C62 RID: 23650
		public const global::LightShape FLOORLAMP_SHAPE = global::LightShape.Circle;

		// Token: 0x04005C63 RID: 23651
		public static readonly Color FLOORLAMP_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005C64 RID: 23652
		public static readonly Color FLOORLAMP_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005C65 RID: 23653
		public static readonly Vector2 FLOORLAMP_OFFSET = new Vector2(0.05f, 1.5f);

		// Token: 0x04005C66 RID: 23654
		public static readonly Vector2 FLOORLAMP_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005C67 RID: 23655
		public const float CEILINGLIGHT_RANGE = 8f;

		// Token: 0x04005C68 RID: 23656
		public const float CEILINGLIGHT_ANGLE = 2.6f;

		// Token: 0x04005C69 RID: 23657
		public const global::LightShape CEILINGLIGHT_SHAPE = global::LightShape.Cone;

		// Token: 0x04005C6A RID: 23658
		public static readonly Color CEILINGLIGHT_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005C6B RID: 23659
		public static readonly Color CEILINGLIGHT_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005C6C RID: 23660
		public static readonly Vector2 CEILINGLIGHT_OFFSET = new Vector2(0.05f, 0.65f);

		// Token: 0x04005C6D RID: 23661
		public static readonly Vector2 CEILINGLIGHT_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005C6E RID: 23662
		public const int CEILINGLIGHT_LUX = 1800;

		// Token: 0x04005C6F RID: 23663
		public const float FOSSILSCULPTURE_RANGE = 8f;

		// Token: 0x04005C70 RID: 23664
		public const float FOSSILSCULPTURE_CEILING_RANGE = 8f;

		// Token: 0x04005C71 RID: 23665
		public const float FOSSILSCULPTURE_ANGLE = 0f;

		// Token: 0x04005C72 RID: 23666
		public const float FOSSILSCULPTURE_CEILING_ANGLE = 2.6f;

		// Token: 0x04005C73 RID: 23667
		public const int FOSSILSCULPTURE_LIGHT_WIDTH = 3;

		// Token: 0x04005C74 RID: 23668
		public const DiscreteShadowCaster.Direction FOSSILSCULPTURE_LIGHT_DIRECTION = DiscreteShadowCaster.Direction.North;

		// Token: 0x04005C75 RID: 23669
		public const DiscreteShadowCaster.Direction FOSSILSCULPTURE_CEILING_LIGHT_DIRECTION = DiscreteShadowCaster.Direction.South;

		// Token: 0x04005C76 RID: 23670
		public const global::LightShape FOSSILSCULPTURE_SHAPE = global::LightShape.Quad;

		// Token: 0x04005C77 RID: 23671
		public const global::LightShape FOSSILSCULPTURE_CEILING_SHAPE = global::LightShape.Quad;

		// Token: 0x04005C78 RID: 23672
		public static readonly Color FOSSILSCULPTURE_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005C79 RID: 23673
		public static readonly Color FOSSILSCULPTURE_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005C7A RID: 23674
		public static readonly Vector2 FOSSILSCULPTURE_OFFSET = new Vector2(0.05f, 0.65f);

		// Token: 0x04005C7B RID: 23675
		public static readonly Vector2 FOSSILSCULPTURE_CEILING_OFFSET = new Vector2(0.05f, 1.65f);

		// Token: 0x04005C7C RID: 23676
		public static readonly Vector2 FOSSILSCULPTURE_DIRECTION = Vector2.up;

		// Token: 0x04005C7D RID: 23677
		public static readonly Vector2 FOSSILSCULPTURE_CEILING_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005C7E RID: 23678
		public const int FOSSILSCULPTURE_LUX = 3000;

		// Token: 0x04005C7F RID: 23679
		public static readonly int SUNLAMP_LUX = (int)((float)BeachChairConfig.TAN_LUX * 4f);

		// Token: 0x04005C80 RID: 23680
		public const float SUNLAMP_RANGE = 16f;

		// Token: 0x04005C81 RID: 23681
		public const float SUNLAMP_ANGLE = 5.2f;

		// Token: 0x04005C82 RID: 23682
		public const global::LightShape SUNLAMP_SHAPE = global::LightShape.Cone;

		// Token: 0x04005C83 RID: 23683
		public static readonly Color SUNLAMP_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005C84 RID: 23684
		public static readonly Color SUNLAMP_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005C85 RID: 23685
		public static readonly Vector2 SUNLAMP_OFFSET = new Vector2(0f, 3.5f);

		// Token: 0x04005C86 RID: 23686
		public static readonly Vector2 SUNLAMP_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005C87 RID: 23687
		public const int MERCURYCEILINGLIGHT_LUX = 60000;

		// Token: 0x04005C88 RID: 23688
		public const float MERCURYCEILINGLIGHT_RANGE = 8f;

		// Token: 0x04005C89 RID: 23689
		public const float MERCURYCEILINGLIGHT_ANGLE = 2.6f;

		// Token: 0x04005C8A RID: 23690
		public const float MERCURYCEILINGLIGHT_FALLOFFRATE = 0.4f;

		// Token: 0x04005C8B RID: 23691
		public const int MERCURYCEILINGLIGHT_WIDTH = 3;

		// Token: 0x04005C8C RID: 23692
		public const global::LightShape MERCURYCEILINGLIGHT_SHAPE = global::LightShape.Quad;

		// Token: 0x04005C8D RID: 23693
		public static readonly Color MERCURYCEILINGLIGHT_LUX_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005C8E RID: 23694
		public static readonly Color MERCURYCEILINGLIGHT_COLOR = LIGHT2D.LIGHT_PINK;

		// Token: 0x04005C8F RID: 23695
		public static readonly Vector2 MERCURYCEILINGLIGHT_OFFSET = new Vector2(0.05f, 0.65f);

		// Token: 0x04005C90 RID: 23696
		public static readonly Vector2 MERCURYCEILINGLIGHT_DIRECTIONVECTOR = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005C91 RID: 23697
		public const DiscreteShadowCaster.Direction MERCURYCEILINGLIGHT_DIRECTION = DiscreteShadowCaster.Direction.South;

		// Token: 0x04005C92 RID: 23698
		public static readonly Color LIGHT_PREVIEW_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005C93 RID: 23699
		public const float HEADQUARTERS_RANGE = 5f;

		// Token: 0x04005C94 RID: 23700
		public const global::LightShape HEADQUARTERS_SHAPE = global::LightShape.Circle;

		// Token: 0x04005C95 RID: 23701
		public static readonly Color HEADQUARTERS_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005C96 RID: 23702
		public static readonly Color HEADQUARTERS_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005C97 RID: 23703
		public static readonly Vector2 HEADQUARTERS_OFFSET = new Vector2(0.5f, 3f);

		// Token: 0x04005C98 RID: 23704
		public static readonly Vector2 EXOBASE_HEADQUARTERS_OFFSET = new Vector2(0f, 2.5f);

		// Token: 0x04005C99 RID: 23705
		public const float POI_TECH_UNLOCK_RANGE = 5f;

		// Token: 0x04005C9A RID: 23706
		public const float POI_TECH_UNLOCK_ANGLE = 2.6f;

		// Token: 0x04005C9B RID: 23707
		public const global::LightShape POI_TECH_UNLOCK_SHAPE = global::LightShape.Cone;

		// Token: 0x04005C9C RID: 23708
		public static readonly Color POI_TECH_UNLOCK_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005C9D RID: 23709
		public static readonly Color POI_TECH_UNLOCK_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005C9E RID: 23710
		public static readonly Vector2 POI_TECH_UNLOCK_OFFSET = new Vector2(0f, 3.4f);

		// Token: 0x04005C9F RID: 23711
		public const int POI_TECH_UNLOCK_LUX = 1800;

		// Token: 0x04005CA0 RID: 23712
		public static readonly Vector2 POI_TECH_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005CA1 RID: 23713
		public const float ENGINE_RANGE = 10f;

		// Token: 0x04005CA2 RID: 23714
		public const global::LightShape ENGINE_SHAPE = global::LightShape.Circle;

		// Token: 0x04005CA3 RID: 23715
		public const int ENGINE_LUX = 80000;

		// Token: 0x04005CA4 RID: 23716
		public const float WALLLIGHT_RANGE = 4f;

		// Token: 0x04005CA5 RID: 23717
		public const float WALLLIGHT_ANGLE = 0f;

		// Token: 0x04005CA6 RID: 23718
		public const global::LightShape WALLLIGHT_SHAPE = global::LightShape.Circle;

		// Token: 0x04005CA7 RID: 23719
		public static readonly Color WALLLIGHT_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005CA8 RID: 23720
		public static readonly Color WALLLIGHT_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005CA9 RID: 23721
		public static readonly Vector2 WALLLIGHT_OFFSET = new Vector2(0f, 0.5f);

		// Token: 0x04005CAA RID: 23722
		public static readonly Vector2 WALLLIGHT_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005CAB RID: 23723
		public const float LIGHTBUG_RANGE = 5f;

		// Token: 0x04005CAC RID: 23724
		public const float LIGHTBUG_ANGLE = 0f;

		// Token: 0x04005CAD RID: 23725
		public const global::LightShape LIGHTBUG_SHAPE = global::LightShape.Circle;

		// Token: 0x04005CAE RID: 23726
		public const int LIGHTBUG_LUX = 1800;

		// Token: 0x04005CAF RID: 23727
		public static readonly Color LIGHTBUG_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005CB0 RID: 23728
		public static readonly Color LIGHTBUG_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005CB1 RID: 23729
		public static readonly Color LIGHTBUG_COLOR_ORANGE = new Color(0.5686275f, 0.48235294f, 0.4392157f, 1f);

		// Token: 0x04005CB2 RID: 23730
		public static readonly Color LIGHTBUG_COLOR_PURPLE = new Color(0.49019608f, 0.4392157f, 0.5686275f, 1f);

		// Token: 0x04005CB3 RID: 23731
		public static readonly Color LIGHTBUG_COLOR_PINK = new Color(0.5686275f, 0.4392157f, 0.5686275f, 1f);

		// Token: 0x04005CB4 RID: 23732
		public static readonly Color LIGHTBUG_COLOR_BLUE = new Color(0.4392157f, 0.4862745f, 0.5686275f, 1f);

		// Token: 0x04005CB5 RID: 23733
		public static readonly Color LIGHTBUG_COLOR_CRYSTAL = new Color(0.5137255f, 0.6666667f, 0.6666667f, 1f);

		// Token: 0x04005CB6 RID: 23734
		public static readonly Color LIGHTBUG_COLOR_GREEN = new Color(0.43137255f, 1f, 0.53333336f, 1f);

		// Token: 0x04005CB7 RID: 23735
		public const int MAJORFOSSILDIGSITE_LAMP_LUX = 1000;

		// Token: 0x04005CB8 RID: 23736
		public const float MAJORFOSSILDIGSITE_LAMP_RANGE = 3f;

		// Token: 0x04005CB9 RID: 23737
		public static readonly Vector2 MAJORFOSSILDIGSITE_LAMP_OFFSET = new Vector2(-0.15f, 2.35f);

		// Token: 0x04005CBA RID: 23738
		public static readonly Vector2 LIGHTBUG_OFFSET = new Vector2(0.05f, 0.25f);

		// Token: 0x04005CBB RID: 23739
		public static readonly Vector2 LIGHTBUG_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005CBC RID: 23740
		public const int PLASMALAMP_LUX = 666;

		// Token: 0x04005CBD RID: 23741
		public const float PLASMALAMP_RANGE = 2f;

		// Token: 0x04005CBE RID: 23742
		public const float PLASMALAMP_ANGLE = 0f;

		// Token: 0x04005CBF RID: 23743
		public const global::LightShape PLASMALAMP_SHAPE = global::LightShape.Circle;

		// Token: 0x04005CC0 RID: 23744
		public static readonly Color PLASMALAMP_COLOR = LIGHT2D.LIGHT_PURPLE;

		// Token: 0x04005CC1 RID: 23745
		public static readonly Color PLASMALAMP_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005CC2 RID: 23746
		public static readonly Vector2 PLASMALAMP_OFFSET = new Vector2(0.05f, 0.5f);

		// Token: 0x04005CC3 RID: 23747
		public static readonly Vector2 PLASMALAMP_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005CC4 RID: 23748
		public const int MAGMALAMP_LUX = 666;

		// Token: 0x04005CC5 RID: 23749
		public const float MAGMALAMP_RANGE = 2f;

		// Token: 0x04005CC6 RID: 23750
		public const float MAGMALAMP_ANGLE = 0f;

		// Token: 0x04005CC7 RID: 23751
		public const global::LightShape MAGMALAMP_SHAPE = global::LightShape.Cone;

		// Token: 0x04005CC8 RID: 23752
		public static readonly Color MAGMALAMP_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04005CC9 RID: 23753
		public static readonly Color MAGMALAMP_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005CCA RID: 23754
		public static readonly Vector2 MAGMALAMP_OFFSET = new Vector2(0.05f, 0.33f);

		// Token: 0x04005CCB RID: 23755
		public static readonly Vector2 MAGMALAMP_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005CCC RID: 23756
		public const int BIOLUMROCK_LUX = 666;

		// Token: 0x04005CCD RID: 23757
		public const float BIOLUMROCK_RANGE = 2f;

		// Token: 0x04005CCE RID: 23758
		public const float BIOLUMROCK_ANGLE = 0f;

		// Token: 0x04005CCF RID: 23759
		public const global::LightShape BIOLUMROCK_SHAPE = global::LightShape.Cone;

		// Token: 0x04005CD0 RID: 23760
		public static readonly Color BIOLUMROCK_COLOR = LIGHT2D.LIGHT_BLUE;

		// Token: 0x04005CD1 RID: 23761
		public static readonly Color BIOLUMROCK_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005CD2 RID: 23762
		public static readonly Vector2 BIOLUMROCK_OFFSET = new Vector2(0.05f, 0.33f);

		// Token: 0x04005CD3 RID: 23763
		public static readonly Vector2 BIOLUMROCK_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04005CD4 RID: 23764
		public const float PINKROCK_RANGE = 2f;

		// Token: 0x04005CD5 RID: 23765
		public const float PINKROCK_ANGLE = 0f;

		// Token: 0x04005CD6 RID: 23766
		public const global::LightShape PINKROCK_SHAPE = global::LightShape.Circle;

		// Token: 0x04005CD7 RID: 23767
		public static readonly Color PINKROCK_COLOR = LIGHT2D.LIGHT_PINK;

		// Token: 0x04005CD8 RID: 23768
		public static readonly Color PINKROCK_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04005CD9 RID: 23769
		public static readonly Vector2 PINKROCK_OFFSET = new Vector2(0.05f, 0.33f);

		// Token: 0x04005CDA RID: 23770
		public static readonly Vector2 PINKROCK_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;
	}
}
