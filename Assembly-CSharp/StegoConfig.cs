using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000167 RID: 359
[EntityConfigOrder(1)]
public class StegoConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006D6 RID: 1750 RVA: 0x0002FF9C File Offset: 0x0002E19C
	public static GameObject CreateStego(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseStegoConfig.BaseStego(id, name, desc, anim_file, "StegoBaseTrait", is_baby, null), StegoTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("StegoBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, StegoTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -StegoTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 50f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 200f, name, false, false, true));
		GameObject gameObject2 = BaseStegoConfig.SetupDiet(gameObject, BaseStegoConfig.StandardDiets(), StegoTuning.CALORIES_PER_UNIT_EATEN, StegoTuning.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x000300BC File Offset: 0x0002E2BC
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x000300C3 File Offset: 0x0002E2C3
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x000300C8 File Offset: 0x0002E2C8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(StegoConfig.CreateStego("Stego", CREATURES.SPECIES.STEGO.NAME, CREATURES.SPECIES.STEGO.DESC, "stego_kanim", false), this, "StegoEgg", CREATURES.SPECIES.STEGO.EGG_NAME, CREATURES.SPECIES.STEGO.DESC, "egg_stego_kanim", 8f, "StegoBaby", 120.00001f, 40f, StegoTuning.EGG_CHANCES_BASE, StegoConfig.EGG_SORT_ORDER, true, false, 1f, false);
		gameObject.AddTag(GameTags.LargeCreature);
		return gameObject;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x0003014E File Offset: 0x0002E34E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00030150 File Offset: 0x0002E350
	public void OnSpawn(GameObject inst)
	{
		new CritterEmoteMonitor.Instance(inst.GetComponent<StateMachineController>(), this.StegoEmotes).StartSM();
	}

	// Token: 0x0400052B RID: 1323
	public const string ID = "Stego";

	// Token: 0x0400052C RID: 1324
	public const string BASE_TRAIT_ID = "StegoBaseTrait";

	// Token: 0x0400052D RID: 1325
	public const string EGG_ID = "StegoEgg";

	// Token: 0x0400052E RID: 1326
	public static int EGG_SORT_ORDER;

	// Token: 0x0400052F RID: 1327
	public List<Emote> StegoEmotes = new List<Emote> { Db.Get().Emotes.Critter.Roar };
}
