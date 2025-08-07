using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000350 RID: 848
public class AlgaeConfig : IOreConfig
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06001185 RID: 4485 RVA: 0x0006683B File Offset: 0x00064A3B
	public SimHashes ElementID
	{
		get
		{
			return SimHashes.Algae;
		}
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x00066842 File Offset: 0x00064A42
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateSolidOreEntity(this.ElementID, new List<Tag> { GameTags.Life });
	}
}
