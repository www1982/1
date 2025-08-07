using System;
using UnityEngine;

// Token: 0x02000CEB RID: 3307
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/InfoDescription")]
public class InfoDescription : KMonoBehaviour
{
	// Token: 0x17000763 RID: 1891
	// (get) Token: 0x060065BC RID: 26044 RVA: 0x00264C74 File Offset: 0x00262E74
	// (set) Token: 0x060065BB RID: 26043 RVA: 0x00264C4D File Offset: 0x00262E4D
	public string DescriptionLocString
	{
		get
		{
			return this.descriptionLocString;
		}
		set
		{
			this.descriptionLocString = value;
			if (this.descriptionLocString != null)
			{
				this.description = Strings.Get(this.descriptionLocString);
			}
		}
	}

	// Token: 0x060065BD RID: 26045 RVA: 0x00264C7C File Offset: 0x00262E7C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (!string.IsNullOrEmpty(this.nameLocString))
		{
			this.displayName = Strings.Get(this.nameLocString);
		}
		if (!string.IsNullOrEmpty(this.descriptionLocString))
		{
			this.description = Strings.Get(this.descriptionLocString);
		}
	}

	// Token: 0x040045AE RID: 17838
	public string nameLocString = "";

	// Token: 0x040045AF RID: 17839
	private string descriptionLocString = "";

	// Token: 0x040045B0 RID: 17840
	public string description;

	// Token: 0x040045B1 RID: 17841
	public string effect = "";

	// Token: 0x040045B2 RID: 17842
	public string displayName;
}
