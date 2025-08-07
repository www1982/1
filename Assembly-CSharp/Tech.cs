using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000AC5 RID: 2757
public class Tech : Resource
{
	// Token: 0x17000593 RID: 1427
	// (get) Token: 0x06005013 RID: 20499 RVA: 0x001CFD7C File Offset: 0x001CDF7C
	public bool FoundNode
	{
		get
		{
			return this.node != null;
		}
	}

	// Token: 0x17000594 RID: 1428
	// (get) Token: 0x06005014 RID: 20500 RVA: 0x001CFD87 File Offset: 0x001CDF87
	public Vector2 center
	{
		get
		{
			return this.node.center;
		}
	}

	// Token: 0x17000595 RID: 1429
	// (get) Token: 0x06005015 RID: 20501 RVA: 0x001CFD94 File Offset: 0x001CDF94
	public float width
	{
		get
		{
			return this.node.width;
		}
	}

	// Token: 0x17000596 RID: 1430
	// (get) Token: 0x06005016 RID: 20502 RVA: 0x001CFDA1 File Offset: 0x001CDFA1
	public float height
	{
		get
		{
			return this.node.height;
		}
	}

	// Token: 0x17000597 RID: 1431
	// (get) Token: 0x06005017 RID: 20503 RVA: 0x001CFDAE File Offset: 0x001CDFAE
	public List<ResourceTreeNode.Edge> edges
	{
		get
		{
			return this.node.edges;
		}
	}

	// Token: 0x06005018 RID: 20504 RVA: 0x001CFDBC File Offset: 0x001CDFBC
	public Tech(string id, List<string> unlockedItemIDs, Techs techs, Dictionary<string, float> overrideDefaultCosts = null)
		: base(id, techs, Strings.Get("STRINGS.RESEARCH.TECHS." + id.ToUpper() + ".NAME"))
	{
		this.desc = Strings.Get("STRINGS.RESEARCH.TECHS." + id.ToUpper() + ".DESC");
		this.unlockedItemIDs = unlockedItemIDs;
		if (overrideDefaultCosts != null && DlcManager.IsExpansion1Active())
		{
			foreach (KeyValuePair<string, float> keyValuePair in overrideDefaultCosts)
			{
				this.costsByResearchTypeID.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x06005019 RID: 20505 RVA: 0x001CFEC0 File Offset: 0x001CE0C0
	public void AddUnlockedItemIDs(params string[] ids)
	{
		foreach (string text in ids)
		{
			this.unlockedItemIDs.Add(text);
		}
	}

	// Token: 0x0600501A RID: 20506 RVA: 0x001CFEF0 File Offset: 0x001CE0F0
	public void RemoveUnlockedItemIDs(params string[] ids)
	{
		foreach (string text in ids)
		{
			if (!this.unlockedItemIDs.Remove(text))
			{
				DebugUtil.DevLogError("Tech item '" + text + "' does not exist to remove");
			}
		}
	}

	// Token: 0x0600501B RID: 20507 RVA: 0x001CFF34 File Offset: 0x001CE134
	public bool RequiresResearchType(string type)
	{
		return this.costsByResearchTypeID.ContainsKey(type) && this.costsByResearchTypeID[type] > 0f;
	}

	// Token: 0x0600501C RID: 20508 RVA: 0x001CFF59 File Offset: 0x001CE159
	public void SetNode(ResourceTreeNode node, string categoryID)
	{
		this.node = node;
		this.category = categoryID;
	}

	// Token: 0x0600501D RID: 20509 RVA: 0x001CFF6C File Offset: 0x001CE16C
	public bool CanAfford(ResearchPointInventory pointInventory)
	{
		foreach (KeyValuePair<string, float> keyValuePair in this.costsByResearchTypeID)
		{
			if (pointInventory.PointsByTypeID[keyValuePair.Key] < keyValuePair.Value)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600501E RID: 20510 RVA: 0x001CFFDC File Offset: 0x001CE1DC
	public string CostString(ResearchTypes types)
	{
		string text = "";
		foreach (KeyValuePair<string, float> keyValuePair in this.costsByResearchTypeID)
		{
			text += string.Format("{0}:{1}", types.GetResearchType(keyValuePair.Key).name.ToString(), keyValuePair.Value.ToString());
			text += "\n";
		}
		return text;
	}

	// Token: 0x0600501F RID: 20511 RVA: 0x001D0074 File Offset: 0x001CE274
	public bool IsComplete()
	{
		if (Research.Instance != null)
		{
			TechInstance techInstance = Research.Instance.Get(this);
			return techInstance != null && techInstance.IsComplete();
		}
		return false;
	}

	// Token: 0x06005020 RID: 20512 RVA: 0x001D00A8 File Offset: 0x001CE2A8
	public bool ArePrerequisitesComplete()
	{
		using (List<Tech>.Enumerator enumerator = this.requiredTech.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsComplete())
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06005021 RID: 20513 RVA: 0x001D0104 File Offset: 0x001CE304
	public void AddSearchTerms(string newSearchTerms)
	{
		SearchUtil.AddCommaDelimitedSearchTerms(newSearchTerms, this.searchTerms);
	}

	// Token: 0x040035EB RID: 13803
	public List<Tech> requiredTech = new List<Tech>();

	// Token: 0x040035EC RID: 13804
	public List<Tech> unlockedTech = new List<Tech>();

	// Token: 0x040035ED RID: 13805
	public List<TechItem> unlockedItems = new List<TechItem>();

	// Token: 0x040035EE RID: 13806
	public List<string> unlockedItemIDs = new List<string>();

	// Token: 0x040035EF RID: 13807
	public int tier;

	// Token: 0x040035F0 RID: 13808
	public Dictionary<string, float> costsByResearchTypeID = new Dictionary<string, float>();

	// Token: 0x040035F1 RID: 13809
	public string desc;

	// Token: 0x040035F2 RID: 13810
	public string category;

	// Token: 0x040035F3 RID: 13811
	public List<string> searchTerms = new List<string>();

	// Token: 0x040035F4 RID: 13812
	private ResourceTreeNode node;
}
