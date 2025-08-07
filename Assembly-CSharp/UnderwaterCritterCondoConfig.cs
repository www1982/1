using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000431 RID: 1073
public class UnderwaterCritterCondoConfig : IBuildingConfig
{
	// Token: 0x0600161A RID: 5658 RVA: 0x0007DE50 File Offset: 0x0007C050
	public override BuildingDef CreateBuildingDef()
	{
		string text = "UnderwaterCritterCondo";
		int num = 3;
		int num2 = 3;
		string text2 = "underwater_critter_condo_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] array = new float[] { 200f };
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, plastics, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER3, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.Floodable = false;
		buildingDef.AddSearchTerms(SEARCH_TERMS.CRITTER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.RANCHING);
		buildingDef.AddSearchTerms(SEARCH_TERMS.WATER);
		return buildingDef;
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x0007DEE8 File Offset: 0x0007C0E8
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x0007DEEA File Offset: 0x0007C0EA
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x0007DEEC File Offset: 0x0007C0EC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<Submergable>();
		Effect effect = new Effect("InteractedWithUnderwaterCondo", global::STRINGS.CREATURES.MODIFIERS.CRITTERCONDOINTERACTEFFECT.NAME, global::STRINGS.CREATURES.MODIFIERS.UNDERWATERCRITTERCONDOINTERACTEFFECT.TOOLTIP, 600f, true, true, false, null, -1f, 0f, null, "");
		effect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 1f, global::STRINGS.CREATURES.MODIFIERS.CRITTERCONDOINTERACTEFFECT.NAME, false, false, true));
		Db.Get().effects.Add(effect);
		CritterCondo.Def def = go.AddOrGetDef<CritterCondo.Def>();
		def.IsCritterCondoOperationalCb = delegate(CritterCondo.Instance condo_smi)
		{
			Building component = condo_smi.GetComponent<Building>();
			for (int i = 0; i < component.PlacementCells.Length; i++)
			{
				if (!Grid.IsLiquid(component.PlacementCells[i]))
				{
					return false;
				}
			}
			return true;
		};
		def.UpdateForegroundVisibilitySymbols = delegate(KBatchedAnimController foreground_controller, bool is_large_critter)
		{
			if (foreground_controller != null)
			{
				foreground_controller.SetSymbolVisiblity("doorway_fg", !is_large_critter);
				foreground_controller.SetSymbolVisiblity("condo_fg", is_large_critter);
			}
		};
		def.moveToStatusItem = new StatusItem("UNDERWATERCRITTERCONDO.MOVINGTO", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		def.interactStatusItem = new StatusItem("UNDERWATERCRITTERCONDO.INTERACTING", "CREATURES", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
		def.condoTag = "UnderwaterCritterCondo";
		def.effectId = effect.Id;
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x0007E037 File Offset: 0x0007C237
	public override void ConfigurePost(BuildingDef def)
	{
	}

	// Token: 0x04000D0C RID: 3340
	public const string ID = "UnderwaterCritterCondo";

	// Token: 0x04000D0D RID: 3341
	public static readonly Operational.Flag Submerged = new Operational.Flag("Submerged", Operational.Flag.Type.Requirement);
}
