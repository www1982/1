using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class CritterCondoConfig : IBuildingConfig
{
	// Token: 0x060001E1 RID: 481 RVA: 0x0000DC0C File Offset: 0x0000BE0C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CritterCondo";
		int num = 3;
		int num2 = 3;
		string text2 = "critter_condo_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] array = new float[] { 200f, 10f };
		string[] array2 = new string[] { "BuildableRaw", "BuildingFiber" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER3, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.AddSearchTerms(SEARCH_TERMS.CRITTER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.RANCHING);
		return buildingDef;
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000DCA6 File Offset: 0x0000BEA6
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000DCA8 File Offset: 0x0000BEA8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000DCAC File Offset: 0x0000BEAC
	public override void DoPostConfigureComplete(GameObject go)
	{
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.CreaturePen.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
		Effect effect = new Effect("InteractedWithCritterCondo", global::STRINGS.CREATURES.MODIFIERS.CRITTERCONDOINTERACTEFFECT.NAME, global::STRINGS.CREATURES.MODIFIERS.CRITTERCONDOINTERACTEFFECT.TOOLTIP, 600f, true, true, false, null, -1f, 0f, null, "");
		effect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 1f, global::STRINGS.CREATURES.MODIFIERS.CRITTERCONDOINTERACTEFFECT.NAME, false, false, true));
		Db.Get().effects.Add(effect);
		CritterCondo.Def def = go.AddOrGetDef<CritterCondo.Def>();
		def.IsCritterCondoOperationalCb = (CritterCondo.Instance condo_smi) => condo_smi.GetComponent<RoomTracker>().IsInCorrectRoom() && !condo_smi.GetComponent<Floodable>().IsFlooded;
		def.moveToStatusItem = new StatusItem("CRITTERCONDO.MOVINGTO", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		def.interactStatusItem = new StatusItem("CRITTERCONDO.INTERACTING", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		def.effectId = effect.Id;
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RanchStationType, false);
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x0000DDF2 File Offset: 0x0000BFF2
	public override void ConfigurePost(BuildingDef def)
	{
	}

	// Token: 0x0400012C RID: 300
	public const string ID = "CritterCondo";

	// Token: 0x0400012D RID: 301
	public const float EFFECT_DURATION_IN_SECONDS = 600f;
}
