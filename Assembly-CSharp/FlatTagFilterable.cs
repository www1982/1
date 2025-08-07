using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200072A RID: 1834
public class FlatTagFilterable : KMonoBehaviour
{
	// Token: 0x06002E47 RID: 11847 RVA: 0x0010949B File Offset: 0x0010769B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		TreeFilterable component = base.GetComponent<TreeFilterable>();
		component.filterByStorageCategoriesOnSpawn = false;
		component.UpdateFilters(new HashSet<Tag>(this.selectedTags));
		base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
	}

	// Token: 0x06002E48 RID: 11848 RVA: 0x001094D8 File Offset: 0x001076D8
	public void SelectTag(Tag tag, bool state)
	{
		global::Debug.Assert(this.tagOptions.Contains(tag), "The tag " + tag.Name + " is not valid for this filterable - it must be added to tagOptions");
		if (state)
		{
			if (!this.selectedTags.Contains(tag))
			{
				this.selectedTags.Add(tag);
			}
		}
		else if (this.selectedTags.Contains(tag))
		{
			this.selectedTags.Remove(tag);
		}
		base.GetComponent<TreeFilterable>().UpdateFilters(new HashSet<Tag>(this.selectedTags));
	}

	// Token: 0x06002E49 RID: 11849 RVA: 0x0010955C File Offset: 0x0010775C
	public void ToggleTag(Tag tag)
	{
		this.SelectTag(tag, !this.selectedTags.Contains(tag));
	}

	// Token: 0x06002E4A RID: 11850 RVA: 0x00109574 File Offset: 0x00107774
	public string GetHeaderText()
	{
		return this.headerText;
	}

	// Token: 0x06002E4B RID: 11851 RVA: 0x0010957C File Offset: 0x0010777C
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (base.GetComponent<KPrefabID>().PrefabID() != gameObject.GetComponent<KPrefabID>().PrefabID())
		{
			return;
		}
		this.selectedTags.Clear();
		foreach (Tag tag in gameObject.GetComponent<FlatTagFilterable>().selectedTags)
		{
			this.SelectTag(tag, true);
		}
		base.GetComponent<TreeFilterable>().UpdateFilters(new HashSet<Tag>(this.selectedTags));
	}

	// Token: 0x04001B52 RID: 6994
	[Serialize]
	public List<Tag> selectedTags = new List<Tag>();

	// Token: 0x04001B53 RID: 6995
	public List<Tag> tagOptions = new List<Tag>();

	// Token: 0x04001B54 RID: 6996
	public string headerText;

	// Token: 0x04001B55 RID: 6997
	public bool displayOnlyDiscoveredTags = true;

	// Token: 0x04001B56 RID: 6998
	public bool currentlyUserAssignable = true;
}
