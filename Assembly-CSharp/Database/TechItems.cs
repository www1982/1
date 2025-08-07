using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F16 RID: 3862
	public class TechItems : ResourceSet<TechItem>
	{
		// Token: 0x06007A0B RID: 31243 RVA: 0x0030422F File Offset: 0x0030242F
		public TechItems(ResourceSet parent)
			: base("TechItems", parent)
		{
		}

		// Token: 0x06007A0C RID: 31244 RVA: 0x00304240 File Offset: 0x00302440
		public void Init()
		{
			this.automationOverlay = this.AddTechItem("AutomationOverlay", RESEARCH.OTHER_TECH_ITEMS.AUTOMATION_OVERLAY.NAME, RESEARCH.OTHER_TECH_ITEMS.AUTOMATION_OVERLAY.DESC, this.GetSpriteFnBuilder("overlay_logic"), null, null, false);
			this.suitsOverlay = this.AddTechItem("SuitsOverlay", RESEARCH.OTHER_TECH_ITEMS.SUITS_OVERLAY.NAME, RESEARCH.OTHER_TECH_ITEMS.SUITS_OVERLAY.DESC, this.GetSpriteFnBuilder("overlay_suit"), null, null, false);
			this.betaResearchPoint = this.AddTechItem("BetaResearchPoint", RESEARCH.OTHER_TECH_ITEMS.BETA_RESEARCH_POINT.NAME, RESEARCH.OTHER_TECH_ITEMS.BETA_RESEARCH_POINT.DESC, this.GetSpriteFnBuilder("research_type_beta_icon"), null, null, false);
			this.gammaResearchPoint = this.AddTechItem("GammaResearchPoint", RESEARCH.OTHER_TECH_ITEMS.GAMMA_RESEARCH_POINT.NAME, RESEARCH.OTHER_TECH_ITEMS.GAMMA_RESEARCH_POINT.DESC, this.GetSpriteFnBuilder("research_type_gamma_icon"), null, null, false);
			this.orbitalResearchPoint = this.AddTechItem("OrbitalResearchPoint", RESEARCH.OTHER_TECH_ITEMS.ORBITAL_RESEARCH_POINT.NAME, RESEARCH.OTHER_TECH_ITEMS.ORBITAL_RESEARCH_POINT.DESC, this.GetSpriteFnBuilder("research_type_orbital_icon"), null, null, false);
			this.conveyorOverlay = this.AddTechItem("ConveyorOverlay", RESEARCH.OTHER_TECH_ITEMS.CONVEYOR_OVERLAY.NAME, RESEARCH.OTHER_TECH_ITEMS.CONVEYOR_OVERLAY.DESC, this.GetSpriteFnBuilder("overlay_conveyor"), null, null, false);
			this.jetSuit = this.AddTechItem("JetSuit", RESEARCH.OTHER_TECH_ITEMS.JET_SUIT.NAME, RESEARCH.OTHER_TECH_ITEMS.JET_SUIT.DESC, this.GetPrefabSpriteFnBuilder("Jet_Suit".ToTag()), null, null, false);
			if (this.jetSuit != null)
			{
				this.jetSuit.AddSearchTerms(SEARCH_TERMS.ATMOSUIT);
			}
			this.atmoSuit = this.AddTechItem("AtmoSuit", RESEARCH.OTHER_TECH_ITEMS.ATMO_SUIT.NAME, RESEARCH.OTHER_TECH_ITEMS.ATMO_SUIT.DESC, this.GetPrefabSpriteFnBuilder("Atmo_Suit".ToTag()), null, null, false);
			if (this.atmoSuit != null)
			{
				this.atmoSuit.AddSearchTerms(SEARCH_TERMS.ATMOSUIT);
			}
			this.oxygenMask = this.AddTechItem("OxygenMask", RESEARCH.OTHER_TECH_ITEMS.OXYGEN_MASK.NAME, RESEARCH.OTHER_TECH_ITEMS.OXYGEN_MASK.DESC, this.GetPrefabSpriteFnBuilder("Oxygen_Mask".ToTag()), null, null, false);
			if (this.oxygenMask != null)
			{
				this.oxygenMask.AddSearchTerms(SEARCH_TERMS.OXYGEN);
			}
			this.superLiquids = this.AddTechItem("SUPER_LIQUIDS", RESEARCH.OTHER_TECH_ITEMS.SUPER_LIQUIDS.NAME, RESEARCH.OTHER_TECH_ITEMS.SUPER_LIQUIDS.DESC, this.GetPrefabSpriteFnBuilder(SimHashes.ViscoGel.CreateTag()), null, null, false);
			this.deltaResearchPoint = this.AddTechItem("DeltaResearchPoint", RESEARCH.OTHER_TECH_ITEMS.DELTA_RESEARCH_POINT.NAME, RESEARCH.OTHER_TECH_ITEMS.DELTA_RESEARCH_POINT.DESC, this.GetSpriteFnBuilder("research_type_delta_icon"), DlcManager.EXPANSION1, null, false);
			this.leadSuit = this.AddTechItem("LeadSuit", RESEARCH.OTHER_TECH_ITEMS.LEAD_SUIT.NAME, RESEARCH.OTHER_TECH_ITEMS.LEAD_SUIT.DESC, this.GetPrefabSpriteFnBuilder("Lead_Suit".ToTag()), DlcManager.EXPANSION1, null, false);
			this.disposableElectrobankMetalOre = this.AddTechItem("DisposableElectrobank_RawMetal", RESEARCH.OTHER_TECH_ITEMS.DISPOSABLE_ELECTROBANK_METAL_ORE.NAME, RESEARCH.OTHER_TECH_ITEMS.DISPOSABLE_ELECTROBANK_METAL_ORE.DESC, this.GetPrefabSpriteFnBuilder("DisposableElectrobank_RawMetal".ToTag()), DlcManager.DLC3, null, false);
			if (this.disposableElectrobankMetalOre != null)
			{
				this.disposableElectrobankMetalOre.AddSearchTerms(SEARCH_TERMS.BATTERY);
			}
			this.lubricationStick = this.AddTechItem("LubricationStick", RESEARCH.OTHER_TECH_ITEMS.LUBRICATION_STICK.NAME, RESEARCH.OTHER_TECH_ITEMS.LUBRICATION_STICK.DESC, this.GetPrefabSpriteFnBuilder("LubricationStick".ToTag()), DlcManager.DLC3, null, false);
			if (this.lubricationStick != null)
			{
				this.lubricationStick.AddSearchTerms(SEARCH_TERMS.MEDICINE);
				this.lubricationStick.AddSearchTerms(SEARCH_TERMS.BIONIC);
			}
			this.disposableElectrobankUraniumOre = this.AddTechItem("DisposableElectrobank_UraniumOre", RESEARCH.OTHER_TECH_ITEMS.DISPOSABLE_ELECTROBANK_URANIUM_ORE.NAME, RESEARCH.OTHER_TECH_ITEMS.DISPOSABLE_ELECTROBANK_URANIUM_ORE.DESC, this.GetPrefabSpriteFnBuilder("DisposableElectrobank_UraniumOre".ToTag()), new string[] { "EXPANSION1_ID", "DLC3_ID" }, null, false);
			if (this.disposableElectrobankUraniumOre != null)
			{
				this.disposableElectrobankUraniumOre.AddSearchTerms(SEARCH_TERMS.BATTERY);
			}
			this.electrobank = this.AddTechItem("Electrobank", RESEARCH.OTHER_TECH_ITEMS.ELECTROBANK.NAME, RESEARCH.OTHER_TECH_ITEMS.ELECTROBANK.DESC, this.GetPrefabSpriteFnBuilder("Electrobank".ToTag()), DlcManager.DLC3, null, false);
			if (this.electrobank != null)
			{
				this.electrobank.AddSearchTerms(SEARCH_TERMS.BATTERY);
			}
			this.fetchDrone = this.AddTechItem("FetchDrone", RESEARCH.OTHER_TECH_ITEMS.FETCHDRONE.NAME, RESEARCH.OTHER_TECH_ITEMS.FETCHDRONE.DESC, this.GetPrefabSpriteFnBuilder("FetchDrone".ToTag()), DlcManager.DLC3, null, false);
			if (this.fetchDrone != null)
			{
				this.fetchDrone.AddSearchTerms(SEARCH_TERMS.ROBOT);
			}
			this.selfChargingElectrobank = this.AddTechItem("SelfChargingElectrobank", RESEARCH.OTHER_TECH_ITEMS.SELFCHARGINGELECTROBANK.NAME, RESEARCH.OTHER_TECH_ITEMS.SELFCHARGINGELECTROBANK.DESC, this.GetPrefabSpriteFnBuilder("SelfChargingElectrobank".ToTag()), new string[] { "EXPANSION1_ID", "DLC3_ID" }, null, false);
			if (this.selfChargingElectrobank != null)
			{
				this.selfChargingElectrobank.AddSearchTerms(SEARCH_TERMS.BATTERY);
			}
		}

		// Token: 0x06007A0D RID: 31245 RVA: 0x00304776 File Offset: 0x00302976
		private Func<string, bool, Sprite> GetSpriteFnBuilder(string spriteName)
		{
			return (string anim, bool centered) => Assets.GetSprite(spriteName);
		}

		// Token: 0x06007A0E RID: 31246 RVA: 0x0030478F File Offset: 0x0030298F
		private Func<string, bool, Sprite> GetPrefabSpriteFnBuilder(Tag prefabTag)
		{
			return (string anim, bool centered) => Def.GetUISprite(prefabTag, "ui", false).first;
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x003047A8 File Offset: 0x003029A8
		[Obsolete("Used AddTechItem with requiredDlcIds and forbiddenDlcIds instead.")]
		public TechItem AddTechItem(string id, string name, string description, Func<string, bool, Sprite> getUISprite, string[] DLCIds, bool poi_unlock = false)
		{
			string[] array;
			string[] array2;
			DlcManager.ConvertAvailableToRequireAndForbidden(DLCIds, out array, out array2);
			return this.AddTechItem(id, name, description, getUISprite, array, array2, poi_unlock);
		}

		// Token: 0x06007A10 RID: 31248 RVA: 0x003047D0 File Offset: 0x003029D0
		public TechItem AddTechItem(string id, string name, string description, Func<string, bool, Sprite> getUISprite, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null, bool poi_unlock = false)
		{
			if (!DlcManager.IsCorrectDlcSubscribed(requiredDlcIds, forbiddenDlcIds))
			{
				return null;
			}
			if (base.TryGet(id) != null)
			{
				DebugUtil.LogWarningArgs(new object[] { "Tried adding a tech item called", id, name, "but it was already added!" });
				return base.Get(id);
			}
			Tech techFromItemID = this.GetTechFromItemID(id);
			if (techFromItemID == null)
			{
				return null;
			}
			TechItem techItem = new TechItem(id, this, name, description, getUISprite, techFromItemID.Id, requiredDlcIds, forbiddenDlcIds, poi_unlock);
			techFromItemID.unlockedItems.Add(techItem);
			return techItem;
		}

		// Token: 0x06007A11 RID: 31249 RVA: 0x00304850 File Offset: 0x00302A50
		public bool IsTechItemComplete(string id)
		{
			bool flag = true;
			foreach (TechItem techItem in this.resources)
			{
				if (techItem.Id == id)
				{
					flag = techItem.IsComplete();
					break;
				}
			}
			return flag;
		}

		// Token: 0x06007A12 RID: 31250 RVA: 0x003048B8 File Offset: 0x00302AB8
		public Tech GetTechFromItemID(string itemId)
		{
			Techs techs = Db.Get().Techs;
			if (techs == null)
			{
				return null;
			}
			return techs.TryGetTechForTechItem(itemId);
		}

		// Token: 0x06007A13 RID: 31251 RVA: 0x003048D0 File Offset: 0x00302AD0
		public int GetTechTierForItem(string itemId)
		{
			Tech techFromItemID = this.GetTechFromItemID(itemId);
			if (techFromItemID != null)
			{
				return Techs.GetTier(techFromItemID);
			}
			return 0;
		}

		// Token: 0x04005942 RID: 22850
		public const string AUTOMATION_OVERLAY_ID = "AutomationOverlay";

		// Token: 0x04005943 RID: 22851
		public TechItem automationOverlay;

		// Token: 0x04005944 RID: 22852
		public const string SUITS_OVERLAY_ID = "SuitsOverlay";

		// Token: 0x04005945 RID: 22853
		public TechItem suitsOverlay;

		// Token: 0x04005946 RID: 22854
		public const string JET_SUIT_ID = "JetSuit";

		// Token: 0x04005947 RID: 22855
		public TechItem jetSuit;

		// Token: 0x04005948 RID: 22856
		public const string ATMO_SUIT_ID = "AtmoSuit";

		// Token: 0x04005949 RID: 22857
		public TechItem atmoSuit;

		// Token: 0x0400594A RID: 22858
		public const string OXYGEN_MASK_ID = "OxygenMask";

		// Token: 0x0400594B RID: 22859
		public TechItem oxygenMask;

		// Token: 0x0400594C RID: 22860
		public const string LEAD_SUIT_ID = "LeadSuit";

		// Token: 0x0400594D RID: 22861
		public TechItem leadSuit;

		// Token: 0x0400594E RID: 22862
		public TechItem disposableElectrobankMetalOre;

		// Token: 0x0400594F RID: 22863
		public TechItem lubricationStick;

		// Token: 0x04005950 RID: 22864
		public TechItem disposableElectrobankUraniumOre;

		// Token: 0x04005951 RID: 22865
		public TechItem electrobank;

		// Token: 0x04005952 RID: 22866
		public TechItem fetchDrone;

		// Token: 0x04005953 RID: 22867
		public TechItem selfChargingElectrobank;

		// Token: 0x04005954 RID: 22868
		public TechItem superLiquids;

		// Token: 0x04005955 RID: 22869
		public const string BETA_RESEARCH_POINT_ID = "BetaResearchPoint";

		// Token: 0x04005956 RID: 22870
		public TechItem betaResearchPoint;

		// Token: 0x04005957 RID: 22871
		public const string GAMMA_RESEARCH_POINT_ID = "GammaResearchPoint";

		// Token: 0x04005958 RID: 22872
		public TechItem gammaResearchPoint;

		// Token: 0x04005959 RID: 22873
		public const string DELTA_RESEARCH_POINT_ID = "DeltaResearchPoint";

		// Token: 0x0400595A RID: 22874
		public TechItem deltaResearchPoint;

		// Token: 0x0400595B RID: 22875
		public const string ORBITAL_RESEARCH_POINT_ID = "OrbitalResearchPoint";

		// Token: 0x0400595C RID: 22876
		public TechItem orbitalResearchPoint;

		// Token: 0x0400595D RID: 22877
		public const string CONVEYOR_OVERLAY_ID = "ConveyorOverlay";

		// Token: 0x0400595E RID: 22878
		public TechItem conveyorOverlay;
	}
}
