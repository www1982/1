using System;
using UnityEngine;

// Token: 0x0200032A RID: 810
public class TallowConfig : IOreConfig
{
	// Token: 0x17000044 RID: 68
	// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00062DA5 File Offset: 0x00060FA5
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.Tallow;
		}
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x00062DAC File Offset: 0x00060FAC
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateSolidOreEntity(this.ElementID, null);
	}

	// Token: 0x04000A8E RID: 2702
	public const string ID = "Tallow";

	// Token: 0x04000A8F RID: 2703
	public static readonly Tag TAG = TagManager.Create("Tallow");
}
