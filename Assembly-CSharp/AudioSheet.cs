using System;
using UnityEngine;

// Token: 0x02000550 RID: 1360
[Serializable]
public class AudioSheet
{
	// Token: 0x04001180 RID: 4480
	public TextAsset asset;

	// Token: 0x04001181 RID: 4481
	public string defaultType;

	// Token: 0x04001182 RID: 4482
	public AudioSheet.SoundInfo[] soundInfos;

	// Token: 0x0200139D RID: 5021
	public class SoundInfo : Resource
	{
		// Token: 0x040069FC RID: 27132
		public string File;

		// Token: 0x040069FD RID: 27133
		public string Anim;

		// Token: 0x040069FE RID: 27134
		public string Type;

		// Token: 0x040069FF RID: 27135
		public string RequiredDlcId;

		// Token: 0x04006A00 RID: 27136
		public float MinInterval;

		// Token: 0x04006A01 RID: 27137
		public string Name0;

		// Token: 0x04006A02 RID: 27138
		public int Frame0;

		// Token: 0x04006A03 RID: 27139
		public string Name1;

		// Token: 0x04006A04 RID: 27140
		public int Frame1;

		// Token: 0x04006A05 RID: 27141
		public string Name2;

		// Token: 0x04006A06 RID: 27142
		public int Frame2;

		// Token: 0x04006A07 RID: 27143
		public string Name3;

		// Token: 0x04006A08 RID: 27144
		public int Frame3;

		// Token: 0x04006A09 RID: 27145
		public string Name4;

		// Token: 0x04006A0A RID: 27146
		public int Frame4;

		// Token: 0x04006A0B RID: 27147
		public string Name5;

		// Token: 0x04006A0C RID: 27148
		public int Frame5;

		// Token: 0x04006A0D RID: 27149
		public string Name6;

		// Token: 0x04006A0E RID: 27150
		public int Frame6;

		// Token: 0x04006A0F RID: 27151
		public string Name7;

		// Token: 0x04006A10 RID: 27152
		public int Frame7;

		// Token: 0x04006A11 RID: 27153
		public string Name8;

		// Token: 0x04006A12 RID: 27154
		public int Frame8;

		// Token: 0x04006A13 RID: 27155
		public string Name9;

		// Token: 0x04006A14 RID: 27156
		public int Frame9;

		// Token: 0x04006A15 RID: 27157
		public string Name10;

		// Token: 0x04006A16 RID: 27158
		public int Frame10;

		// Token: 0x04006A17 RID: 27159
		public string Name11;

		// Token: 0x04006A18 RID: 27160
		public int Frame11;
	}
}
