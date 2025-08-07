using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AA3 RID: 2723
public class DiseaseVisualization : ScriptableObject
{
	// Token: 0x06004F00 RID: 20224 RVA: 0x001C8FBC File Offset: 0x001C71BC
	public DiseaseVisualization.Info GetInfo(HashedString id)
	{
		foreach (DiseaseVisualization.Info info in this.info)
		{
			if (id == info.name)
			{
				return info;
			}
		}
		return default(DiseaseVisualization.Info);
	}

	// Token: 0x0400346F RID: 13423
	public Sprite overlaySprite;

	// Token: 0x04003470 RID: 13424
	public List<DiseaseVisualization.Info> info = new List<DiseaseVisualization.Info>();

	// Token: 0x02001B89 RID: 7049
	[Serializable]
	public struct Info
	{
		// Token: 0x0600A7ED RID: 42989 RVA: 0x003B3275 File Offset: 0x003B1475
		public Info(string name)
		{
			this.name = name;
			this.overlayColourName = "germFoodPoisoning";
		}

		// Token: 0x04008326 RID: 33574
		public string name;

		// Token: 0x04008327 RID: 33575
		public string overlayColourName;
	}
}
