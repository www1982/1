using System;
using FMODUnity;
using UnityEngine;

// Token: 0x02000C02 RID: 3074
public class GlobalResources : ScriptableObject
{
	// Token: 0x06005CA8 RID: 23720 RVA: 0x0021C9E4 File Offset: 0x0021ABE4
	public static GlobalResources Instance()
	{
		if (GlobalResources._Instance == null)
		{
			GlobalResources._Instance = Resources.Load<GlobalResources>("GlobalResources");
		}
		return GlobalResources._Instance;
	}

	// Token: 0x04003D99 RID: 15769
	public Material AnimMaterial;

	// Token: 0x04003D9A RID: 15770
	public Material AnimUIMaterial;

	// Token: 0x04003D9B RID: 15771
	public Material AnimPlaceMaterial;

	// Token: 0x04003D9C RID: 15772
	public Material AnimMaterialUIDesaturated;

	// Token: 0x04003D9D RID: 15773
	public Material AnimSimpleMaterial;

	// Token: 0x04003D9E RID: 15774
	public Material AnimOverlayMaterial;

	// Token: 0x04003D9F RID: 15775
	public Texture2D WhiteTexture;

	// Token: 0x04003DA0 RID: 15776
	public EventReference ConduitOverlaySoundLiquid;

	// Token: 0x04003DA1 RID: 15777
	public EventReference ConduitOverlaySoundGas;

	// Token: 0x04003DA2 RID: 15778
	public EventReference ConduitOverlaySoundSolid;

	// Token: 0x04003DA3 RID: 15779
	public EventReference AcousticDisturbanceSound;

	// Token: 0x04003DA4 RID: 15780
	public EventReference AcousticDisturbanceBubbleSound;

	// Token: 0x04003DA5 RID: 15781
	public EventReference WallDamageLayerSound;

	// Token: 0x04003DA6 RID: 15782
	public Sprite sadDupeAudio;

	// Token: 0x04003DA7 RID: 15783
	public Sprite sadDupe;

	// Token: 0x04003DA8 RID: 15784
	public Sprite baseGameLogoSmall;

	// Token: 0x04003DA9 RID: 15785
	public Sprite expansion1LogoSmall;

	// Token: 0x04003DAA RID: 15786
	private static GlobalResources _Instance;
}
