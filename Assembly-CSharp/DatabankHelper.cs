using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200025B RID: 603
public abstract class DatabankHelper
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000C33 RID: 3123 RVA: 0x000497C8 File Offset: 0x000479C8
	public static string ID
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return "OrbitalResearchDatabank";
			}
			return "ResearchDatabank";
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000C34 RID: 3124 RVA: 0x000497DC File Offset: 0x000479DC
	public static Tag TAG
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return OrbitalResearchDatabankConfig.TAG;
			}
			return ResearchDatabankConfig.TAG;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000C35 RID: 3125 RVA: 0x000497F0 File Offset: 0x000479F0
	public static string RESEARCH_NAME
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return RESEARCH.TYPES.ORBITAL.NAME;
			}
			return RESEARCH.TYPES.GAMMA.NAME;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0004980E File Offset: 0x00047A0E
	public static string RESEARCH_CODEXID
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return "RESEARCHDLC1";
			}
			return "RESEARCH";
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x06000C37 RID: 3127 RVA: 0x00049822 File Offset: 0x00047A22
	public static string NAME
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return ITEMS.INDUSTRIAL_PRODUCTS.ORBITAL_RESEARCH_DATABANK.NAME;
			}
			return ITEMS.INDUSTRIAL_PRODUCTS.RESEARCH_DATABANK.NAME;
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00049840 File Offset: 0x00047A40
	public static string NAME_PLURAL
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return ITEMS.INDUSTRIAL_PRODUCTS.ORBITAL_RESEARCH_DATABANK.NAME_PLURAL;
			}
			return ITEMS.INDUSTRIAL_PRODUCTS.RESEARCH_DATABANK.NAME_PLURAL;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0004985E File Offset: 0x00047A5E
	public static string DESC
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return ITEMS.INDUSTRIAL_PRODUCTS.ORBITAL_RESEARCH_DATABANK.DESC;
			}
			return ITEMS.INDUSTRIAL_PRODUCTS.RESEARCH_DATABANK.DESC;
		}
	}

	// Token: 0x1700001D RID: 29
	// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0004987C File Offset: 0x00047A7C
	public static Sprite SPRITE
	{
		get
		{
			return Assets.GetSprite("ui_databank");
		}
	}

	// Token: 0x0400086F RID: 2159
	public const string CODEXID = "Databank";
}
