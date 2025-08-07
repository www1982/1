using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000DF7 RID: 3575
[AddComponentMenu("KMonoBehaviour/scripts/FilterSideScreenRow")]
public class FilterSideScreenRow : SingleItemSelectionRow
{
	// Token: 0x170007C3 RID: 1987
	// (get) Token: 0x060070DC RID: 28892 RVA: 0x002AEC5C File Offset: 0x002ACE5C
	public override string InvalidTagTitle
	{
		get
		{
			return UI.UISIDESCREENS.FILTERSIDESCREEN.NO_SELECTION;
		}
	}

	// Token: 0x060070DD RID: 28893 RVA: 0x002AEC68 File Offset: 0x002ACE68
	protected override void SetIcon(Sprite sprite, Color color)
	{
		if (this.icon != null)
		{
			this.icon.gameObject.SetActive(false);
		}
	}
}
