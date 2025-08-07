using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000AC4 RID: 2756
public class ResearchTypes
{
	// Token: 0x06005011 RID: 20497 RVA: 0x001CFA44 File Offset: 0x001CDC44
	public ResearchTypes()
	{
		ResearchType researchType = new ResearchType("basic", RESEARCH.TYPES.ALPHA.NAME, RESEARCH.TYPES.ALPHA.DESC, Assets.GetSprite("research_type_alpha_icon"), new Color(0.59607846f, 0.6666667f, 0.9137255f), new Recipe.Ingredient[]
		{
			new Recipe.Ingredient("Dirt".ToTag(), 100f)
		}, 600f, "research_center_kanim", new string[] { "ResearchCenter" }, RESEARCH.TYPES.ALPHA.RECIPEDESC);
		this.Types.Add(researchType);
		ResearchType researchType2 = new ResearchType("advanced", RESEARCH.TYPES.BETA.NAME, RESEARCH.TYPES.BETA.DESC, Assets.GetSprite("research_type_beta_icon"), new Color(0.6f, 0.38431373f, 0.5686275f), new Recipe.Ingredient[]
		{
			new Recipe.Ingredient("Water".ToTag(), 25f)
		}, 1200f, "research_center_kanim", new string[] { "AdvancedResearchCenter" }, RESEARCH.TYPES.BETA.RECIPEDESC);
		this.Types.Add(researchType2);
		ResearchType researchType3 = new ResearchType("space", RESEARCH.TYPES.GAMMA.NAME, RESEARCH.TYPES.GAMMA.DESC, Assets.GetSprite("research_type_gamma_icon"), new Color32(240, 141, 44, byte.MaxValue), null, 2400f, "research_center_kanim", new string[] { "CosmicResearchCenter" }, RESEARCH.TYPES.GAMMA.RECIPEDESC);
		this.Types.Add(researchType3);
		ResearchType researchType4 = new ResearchType("nuclear", RESEARCH.TYPES.DELTA.NAME, RESEARCH.TYPES.DELTA.DESC, Assets.GetSprite("research_type_delta_icon"), new Color32(231, 210, 17, byte.MaxValue), null, 2400f, "research_center_kanim", new string[] { "NuclearResearchCenter" }, RESEARCH.TYPES.DELTA.RECIPEDESC);
		this.Types.Add(researchType4);
		ResearchType researchType5 = new ResearchType("orbital", RESEARCH.TYPES.ORBITAL.NAME, RESEARCH.TYPES.ORBITAL.DESC, Assets.GetSprite("research_type_orbital_icon"), new Color32(240, 141, 44, byte.MaxValue), null, 2400f, "research_center_kanim", new string[] { "OrbitalResearchCenter", "DLC1CosmicResearchCenter" }, RESEARCH.TYPES.ORBITAL.RECIPEDESC);
		this.Types.Add(researchType5);
	}

	// Token: 0x06005012 RID: 20498 RVA: 0x001CFD08 File Offset: 0x001CDF08
	public ResearchType GetResearchType(string id)
	{
		foreach (ResearchType researchType in this.Types)
		{
			if (id == researchType.id)
			{
				return researchType;
			}
		}
		global::Debug.LogWarning(string.Format("No research with type id {0} found", id));
		return null;
	}

	// Token: 0x040035EA RID: 13802
	public List<ResearchType> Types = new List<ResearchType>();

	// Token: 0x02001B9D RID: 7069
	public class ID
	{
		// Token: 0x04008365 RID: 33637
		public const string BASIC = "basic";

		// Token: 0x04008366 RID: 33638
		public const string ADVANCED = "advanced";

		// Token: 0x04008367 RID: 33639
		public const string SPACE = "space";

		// Token: 0x04008368 RID: 33640
		public const string NUCLEAR = "nuclear";

		// Token: 0x04008369 RID: 33641
		public const string ORBITAL = "orbital";
	}
}
