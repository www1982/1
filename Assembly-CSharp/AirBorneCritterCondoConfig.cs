using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class AirBorneCritterCondoConfig : IBuildingConfig
{
	// Token: 0x06000056 RID: 86 RVA: 0x000044AC File Offset: 0x000026AC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "AirBorneCritterCondo";
		int num = 3;
		int num2 = 3;
		string text2 = "critter_condo_airborne_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] array = new float[] { 200f };
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnCeiling;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, plastics, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER3, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.AddSearchTerms(SEARCH_TERMS.CRITTER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.RANCHING);
		return buildingDef;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00004538 File Offset: 0x00002738
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000453A File Offset: 0x0000273A
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000453C File Offset: 0x0000273C
	public override void DoPostConfigureComplete(GameObject go)
	{
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.CreaturePen.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
		Effect effect = new Effect("InteractedWithAirborneCondo", global::STRINGS.CREATURES.MODIFIERS.CRITTERCONDOINTERACTEFFECT.NAME, global::STRINGS.CREATURES.MODIFIERS.AIRBORNECRITTERCONDOINTERACTEFFECT.TOOLTIP, 600f, true, true, false, null, -1f, 0f, null, "");
		effect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 1f, global::STRINGS.CREATURES.MODIFIERS.CRITTERCONDOINTERACTEFFECT.NAME, false, false, true));
		Db.Get().effects.Add(effect);
		CritterCondo.Def def = go.AddOrGetDef<CritterCondo.Def>();
		def.IsCritterCondoOperationalCb = (CritterCondo.Instance condo_smi) => condo_smi.GetComponent<RoomTracker>().IsInCorrectRoom();
		def.moveToStatusItem = new StatusItem("AIRBORNECRITTERCONDO.MOVINGTO", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		def.interactStatusItem = new StatusItem("AIRBORNECRITTERCONDO.INTERACTING", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		def.condoTag = "AirBorneCritterCondo";
		def.effectId = effect.Id;
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RanchStationType, false);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00004692 File Offset: 0x00002892
	public override void ConfigurePost(BuildingDef def)
	{
	}

	// Token: 0x04000049 RID: 73
	public const string ID = "AirBorneCritterCondo";
}
