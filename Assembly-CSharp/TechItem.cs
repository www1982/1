using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AC7 RID: 2759
public class TechItem : Resource, IHasDlcRestrictions
{
	// Token: 0x0600502A RID: 20522 RVA: 0x001D0390 File Offset: 0x001CE590
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x0600502B RID: 20523 RVA: 0x001D0398 File Offset: 0x001CE598
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x0600502C RID: 20524 RVA: 0x001D03A0 File Offset: 0x001CE5A0
	[Obsolete("Use constructor with requiredDlcIds and forbiddenDlcIds")]
	public TechItem(string id, ResourceSet parent, string name, string description, Func<string, bool, Sprite> getUISprite, string parentTechId, string[] dlcIds, bool isPOIUnlock = false)
		: base(id, parent, name)
	{
		this.description = description;
		this.getUISprite = getUISprite;
		this.parentTechId = parentTechId;
		this.isPOIUnlock = isPOIUnlock;
		DlcManager.ConvertAvailableToRequireAndForbidden(dlcIds, out this.requiredDlcIds, out this.forbiddenDlcIds);
	}

	// Token: 0x0600502D RID: 20525 RVA: 0x001D03F4 File Offset: 0x001CE5F4
	public TechItem(string id, ResourceSet parent, string name, string description, Func<string, bool, Sprite> getUISprite, string parentTechId, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null, bool isPOIUnlock = false)
		: base(id, parent, name)
	{
		this.description = description;
		this.getUISprite = getUISprite;
		this.parentTechId = parentTechId;
		this.isPOIUnlock = isPOIUnlock;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x17000598 RID: 1432
	// (get) Token: 0x0600502E RID: 20526 RVA: 0x001D0445 File Offset: 0x001CE645
	public Tech ParentTech
	{
		get
		{
			return Db.Get().Techs.Get(this.parentTechId);
		}
	}

	// Token: 0x0600502F RID: 20527 RVA: 0x001D045C File Offset: 0x001CE65C
	public Sprite UISprite()
	{
		return this.getUISprite("ui", false);
	}

	// Token: 0x06005030 RID: 20528 RVA: 0x001D046F File Offset: 0x001CE66F
	public bool IsComplete()
	{
		return this.ParentTech.IsComplete() || this.IsPOIUnlocked();
	}

	// Token: 0x06005031 RID: 20529 RVA: 0x001D0488 File Offset: 0x001CE688
	private bool IsPOIUnlocked()
	{
		if (this.isPOIUnlock)
		{
			TechInstance techInstance = Research.Instance.Get(this.ParentTech);
			if (techInstance != null)
			{
				return techInstance.UnlockedPOITechIds.Contains(this.Id);
			}
		}
		return false;
	}

	// Token: 0x06005032 RID: 20530 RVA: 0x001D04C4 File Offset: 0x001CE6C4
	public void POIUnlocked()
	{
		DebugUtil.DevAssert(this.isPOIUnlock, "Trying to unlock tech item " + this.Id + " via POI and it's not marked as POI unlockable.", null);
		if (this.isPOIUnlock && !this.IsComplete())
		{
			Research.Instance.Get(this.ParentTech).UnlockPOITech(this.Id);
		}
	}

	// Token: 0x06005033 RID: 20531 RVA: 0x001D0520 File Offset: 0x001CE720
	public void AddSearchTerms(List<string> newSearchTerms)
	{
		foreach (string text in newSearchTerms)
		{
			this.searchTerms.Add(text);
		}
	}

	// Token: 0x06005034 RID: 20532 RVA: 0x001D0574 File Offset: 0x001CE774
	public void AddSearchTerms(string newSearchTerms)
	{
		SearchUtil.AddCommaDelimitedSearchTerms(newSearchTerms, this.searchTerms);
	}

	// Token: 0x040035F9 RID: 13817
	public string description;

	// Token: 0x040035FA RID: 13818
	public Func<string, bool, Sprite> getUISprite;

	// Token: 0x040035FB RID: 13819
	public string parentTechId;

	// Token: 0x040035FC RID: 13820
	public bool isPOIUnlock;

	// Token: 0x040035FD RID: 13821
	[Obsolete("Use required/forbidden instead")]
	public string[] dlcIds;

	// Token: 0x040035FE RID: 13822
	public string[] requiredDlcIds;

	// Token: 0x040035FF RID: 13823
	public string[] forbiddenDlcIds;

	// Token: 0x04003600 RID: 13824
	public List<string> searchTerms = new List<string>();
}
