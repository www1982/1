using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class ChameleonConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000380 RID: 896 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
	public static GameObject CreateChameleon(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseChameleonConfig.BaseChameleon(id, name, desc, anim_file, "ChameleonBaseTrait", is_baby, null, 233.15f, 293.15f, 173.15f, 373.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, ChameleonTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("ChameleonBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, ChameleonTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -ChameleonTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, (float)ChameleonConfig.LIFESPAN, name, false, false, true));
		Diet diet = new Diet(new Diet.Info[]
		{
			new Diet.Info(new HashSet<Tag> { DewDripConfig.ID.ToTag() }, ChameleonConfig.POOP_ELEMENT, ChameleonConfig.CALORIES_PER_DRIP_EATEN, ChameleonConfig.KG_POOP_PER_DRIP, null, 0f, false, Diet.Info.FoodType.EatSolid, false, null)
		});
		CreatureCalorieMonitor.Def def = gameObject.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minConsumedCaloriesBeforePooping = ChameleonConfig.MIN_POOP_SIZE_IN_CALORIES;
		gameObject.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		gameObject.AddOrGetDef<SetNavOrientationOnSpawnMonitor.Def>();
		gameObject.AddTag(GameTags.OriginalCreature);
		EntityTemplates.AddSecondaryExcretion(gameObject, SimHashes.ChlorineGas, 0.005f);
		return gameObject;
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0001E88C File Offset: 0x0001CA8C
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001E893 File Offset: 0x0001CA93
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000383 RID: 899 RVA: 0x0001E898 File Offset: 0x0001CA98
	public virtual GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(ChameleonConfig.CreateChameleon("Chameleon", CREATURES.SPECIES.CHAMELEON.NAME, CREATURES.SPECIES.CHAMELEON.DESC, "chameleo_kanim", false), this, "ChameleonEgg", CREATURES.SPECIES.CHAMELEON.EGG_NAME, CREATURES.SPECIES.CHAMELEON.DESC, "egg_chameleo_kanim", ChameleonTuning.EGG_MASS, "ChameleonBaby", 0.6f * (float)ChameleonConfig.LIFESPAN, 0.2f * (float)ChameleonConfig.LIFESPAN, ChameleonTuning.EGG_CHANCES_BASE, ChameleonConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x06000384 RID: 900 RVA: 0x0001E921 File Offset: 0x0001CB21
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000385 RID: 901 RVA: 0x0001E923 File Offset: 0x0001CB23
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002AF RID: 687
	public const string ID = "Chameleon";

	// Token: 0x040002B0 RID: 688
	public const string BASE_TRAIT_ID = "ChameleonBaseTrait";

	// Token: 0x040002B1 RID: 689
	public const string EGG_ID = "ChameleonEgg";

	// Token: 0x040002B2 RID: 690
	public static Tag POOP_ELEMENT = SimHashes.BleachStone.CreateTag();

	// Token: 0x040002B3 RID: 691
	private static float DRIPS_EATEN_PER_CYCLE = 1f;

	// Token: 0x040002B4 RID: 692
	private static float CALORIES_PER_DRIP_EATEN = ChameleonTuning.STANDARD_CALORIES_PER_CYCLE / ChameleonConfig.DRIPS_EATEN_PER_CYCLE;

	// Token: 0x040002B5 RID: 693
	private static float KG_POOP_PER_DRIP = 10f;

	// Token: 0x040002B6 RID: 694
	private static float MIN_POOP_SIZE_IN_KG = 10f;

	// Token: 0x040002B7 RID: 695
	private static float MIN_POOP_SIZE_IN_CALORIES = ChameleonConfig.CALORIES_PER_DRIP_EATEN * ChameleonConfig.MIN_POOP_SIZE_IN_KG / ChameleonConfig.KG_POOP_PER_DRIP;

	// Token: 0x040002B8 RID: 696
	private static int LIFESPAN = 50;

	// Token: 0x040002B9 RID: 697
	public static int EGG_SORT_ORDER = 800;
}
