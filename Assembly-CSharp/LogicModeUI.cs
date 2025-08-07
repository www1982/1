using System;
using UnityEngine;

// Token: 0x02000C2D RID: 3117
public class LogicModeUI : ScriptableObject
{
	// Token: 0x04003F05 RID: 16133
	[Header("Base Assets")]
	public Sprite inputSprite;

	// Token: 0x04003F06 RID: 16134
	public Sprite outputSprite;

	// Token: 0x04003F07 RID: 16135
	public Sprite resetSprite;

	// Token: 0x04003F08 RID: 16136
	public GameObject prefab;

	// Token: 0x04003F09 RID: 16137
	public GameObject ribbonInputPrefab;

	// Token: 0x04003F0A RID: 16138
	public GameObject ribbonOutputPrefab;

	// Token: 0x04003F0B RID: 16139
	public GameObject controlInputPrefab;

	// Token: 0x04003F0C RID: 16140
	[Header("Colouring")]
	public Color32 colourOn = new Color32(0, byte.MaxValue, 0, 0);

	// Token: 0x04003F0D RID: 16141
	public Color32 colourOff = new Color32(byte.MaxValue, 0, 0, 0);

	// Token: 0x04003F0E RID: 16142
	public Color32 colourDisconnected = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x04003F0F RID: 16143
	public Color32 colourOnProtanopia = new Color32(179, 204, 0, 0);

	// Token: 0x04003F10 RID: 16144
	public Color32 colourOffProtanopia = new Color32(166, 51, 102, 0);

	// Token: 0x04003F11 RID: 16145
	public Color32 colourOnDeuteranopia = new Color32(128, 0, 128, 0);

	// Token: 0x04003F12 RID: 16146
	public Color32 colourOffDeuteranopia = new Color32(byte.MaxValue, 153, 0, 0);

	// Token: 0x04003F13 RID: 16147
	public Color32 colourOnTritanopia = new Color32(51, 102, byte.MaxValue, 0);

	// Token: 0x04003F14 RID: 16148
	public Color32 colourOffTritanopia = new Color32(byte.MaxValue, 153, 0, 0);
}
