using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000C7B RID: 3195
public class SubEntry : IHasDlcRestrictions
{
	// Token: 0x060061C6 RID: 25030 RVA: 0x002464E6 File Offset: 0x002446E6
	public SubEntry()
	{
	}

	// Token: 0x060061C7 RID: 25031 RVA: 0x002464FC File Offset: 0x002446FC
	public SubEntry(string id, string parentEntryID, List<ContentContainer> contentContainers, string name)
	{
		this.id = id;
		this.parentEntryID = parentEntryID;
		this.name = name;
		this.contentContainers = contentContainers;
		if (!string.IsNullOrEmpty(this.lockID))
		{
			foreach (ContentContainer contentContainer in contentContainers)
			{
				contentContainer.lockID = this.lockID;
			}
		}
		if (string.IsNullOrEmpty(this.sortString))
		{
			if (!string.IsNullOrEmpty(this.title))
			{
				this.sortString = UI.StripLinkFormatting(this.title);
				return;
			}
			this.sortString = UI.StripLinkFormatting(name);
		}
	}

	// Token: 0x170006F9 RID: 1785
	// (get) Token: 0x060061C8 RID: 25032 RVA: 0x002465C4 File Offset: 0x002447C4
	// (set) Token: 0x060061C9 RID: 25033 RVA: 0x002465CC File Offset: 0x002447CC
	public List<ContentContainer> contentContainers { get; set; }

	// Token: 0x170006FA RID: 1786
	// (get) Token: 0x060061CA RID: 25034 RVA: 0x002465D5 File Offset: 0x002447D5
	// (set) Token: 0x060061CB RID: 25035 RVA: 0x002465DD File Offset: 0x002447DD
	public string parentEntryID { get; set; }

	// Token: 0x170006FB RID: 1787
	// (get) Token: 0x060061CC RID: 25036 RVA: 0x002465E6 File Offset: 0x002447E6
	// (set) Token: 0x060061CD RID: 25037 RVA: 0x002465EE File Offset: 0x002447EE
	public string id { get; set; }

	// Token: 0x170006FC RID: 1788
	// (get) Token: 0x060061CE RID: 25038 RVA: 0x002465F7 File Offset: 0x002447F7
	// (set) Token: 0x060061CF RID: 25039 RVA: 0x002465FF File Offset: 0x002447FF
	public string name { get; set; }

	// Token: 0x170006FD RID: 1789
	// (get) Token: 0x060061D0 RID: 25040 RVA: 0x00246608 File Offset: 0x00244808
	// (set) Token: 0x060061D1 RID: 25041 RVA: 0x00246610 File Offset: 0x00244810
	public string title { get; set; }

	// Token: 0x170006FE RID: 1790
	// (get) Token: 0x060061D2 RID: 25042 RVA: 0x00246619 File Offset: 0x00244819
	// (set) Token: 0x060061D3 RID: 25043 RVA: 0x00246621 File Offset: 0x00244821
	public string subtitle { get; set; }

	// Token: 0x170006FF RID: 1791
	// (get) Token: 0x060061D4 RID: 25044 RVA: 0x0024662A File Offset: 0x0024482A
	// (set) Token: 0x060061D5 RID: 25045 RVA: 0x00246632 File Offset: 0x00244832
	public Sprite icon { get; set; }

	// Token: 0x17000700 RID: 1792
	// (get) Token: 0x060061D6 RID: 25046 RVA: 0x0024663B File Offset: 0x0024483B
	// (set) Token: 0x060061D7 RID: 25047 RVA: 0x00246643 File Offset: 0x00244843
	public int layoutPriority { get; set; }

	// Token: 0x17000701 RID: 1793
	// (get) Token: 0x060061D8 RID: 25048 RVA: 0x0024664C File Offset: 0x0024484C
	// (set) Token: 0x060061D9 RID: 25049 RVA: 0x00246654 File Offset: 0x00244854
	public bool disabled { get; set; }

	// Token: 0x17000702 RID: 1794
	// (get) Token: 0x060061DA RID: 25050 RVA: 0x0024665D File Offset: 0x0024485D
	// (set) Token: 0x060061DB RID: 25051 RVA: 0x00246665 File Offset: 0x00244865
	public string lockID { get; set; }

	// Token: 0x17000703 RID: 1795
	// (get) Token: 0x060061DC RID: 25052 RVA: 0x0024666E File Offset: 0x0024486E
	// (set) Token: 0x060061DD RID: 25053 RVA: 0x00246676 File Offset: 0x00244876
	public string[] requiredDlcIds { get; set; }

	// Token: 0x17000704 RID: 1796
	// (get) Token: 0x060061DE RID: 25054 RVA: 0x0024667F File Offset: 0x0024487F
	// (set) Token: 0x060061DF RID: 25055 RVA: 0x00246687 File Offset: 0x00244887
	public string[] forbiddenDlcIds { get; set; }

	// Token: 0x060061E0 RID: 25056 RVA: 0x00246690 File Offset: 0x00244890
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x060061E1 RID: 25057 RVA: 0x00246698 File Offset: 0x00244898
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x17000705 RID: 1797
	// (get) Token: 0x060061E2 RID: 25058 RVA: 0x002466A0 File Offset: 0x002448A0
	// (set) Token: 0x060061E3 RID: 25059 RVA: 0x002466A8 File Offset: 0x002448A8
	public string sortString { get; set; }

	// Token: 0x0400424F RID: 16975
	public ContentContainer lockedContentContainer;

	// Token: 0x04004256 RID: 16982
	public Color iconColor = Color.white;
}
