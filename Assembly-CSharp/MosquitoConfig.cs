using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200013D RID: 317
[EntityConfigOrder(1)]
public class MosquitoConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060005EA RID: 1514 RVA: 0x0002C800 File Offset: 0x0002AA00
	public static GameObject CreateMosquito(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseMosquitoConfig.BaseMosquito(id, name, desc, anim_file, "MosquitoBaseTrait", null, is_baby, 278.15f, 338.15f, 273.15f, 348.15f, "attack_pre", "attack_loop", "attack_pst", "STRINGS.CREATURES.STATUSITEMS.MOSQUITO_GOING_FOR_FOOD", "STRINGS.CREATURES.STATUSITEMS.EATING");
		gameObject.AddOrGetDef<AgeMonitor.Def>();
		Trait trait = Db.Get().CreateTrait("MosquitoBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 10f, name, false, false, true));
		return gameObject;
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0002C8C1 File Offset: 0x0002AAC1
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0002C8CC File Offset: 0x0002AACC
	public GameObject CreatePrefab()
	{
		CREATURES.SPECIES.MOSQUITO.NAME;
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(MosquitoConfig.CreateMosquito("Mosquito", CREATURES.SPECIES.MOSQUITO.NAME, CREATURES.SPECIES.MOSQUITO.DESC, "mosquito_kanim", false), this, "MosquitoEgg", CREATURES.SPECIES.MOSQUITO.EGG_NAME, CREATURES.SPECIES.MOSQUITO.DESC, "egg_mosquito_kanim", 1f, "MosquitoBaby", 4.5f, 2f, MosquitoTuning.EGG_CHANCES_BASE, MosquitoConfig.EGG_SORT_ORDER, false, false, 0.75f, false, true);
		gameObject.AddTag(GameTags.OriginalCreature);
		MosquitoHungerMonitor mosquitoHungerMonitor = gameObject.AddOrGet<MosquitoHungerMonitor>();
		mosquitoHungerMonitor.AllowedTargetTags = new List<Tag>
		{
			GameTags.BaseMinion,
			GameTags.Creature
		};
		mosquitoHungerMonitor.ForbiddenTargetTags = new List<Tag>
		{
			"Mosquito",
			GameTags.SwimmingCreature,
			GameTags.Dead,
			GameTags.HasAirtightSuit
		};
		gameObject.AddOrGetDef<AgeMonitor.Def>().minAgePercentOnSpawn = 0.5f;
		return gameObject;
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0002C9D0 File Offset: 0x0002ABD0
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0002C9D2 File Offset: 0x0002ABD2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400046A RID: 1130
	public const string BASE_TRAIT_ID = "MosquitoBaseTrait";

	// Token: 0x0400046B RID: 1131
	public const string ID = "Mosquito";

	// Token: 0x0400046C RID: 1132
	public const string EGG_ID = "MosquitoEgg";

	// Token: 0x0400046D RID: 1133
	public static int EGG_SORT_ORDER = 300;

	// Token: 0x0400046E RID: 1134
	public const int ADULT_LIFESPAN = 5;

	// Token: 0x0400046F RID: 1135
	public const int BABY_LIFESPAN = 5;

	// Token: 0x04000470 RID: 1136
	public const int LIFE_SPAN = 10;
}
