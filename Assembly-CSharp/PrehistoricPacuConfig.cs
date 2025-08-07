using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200014C RID: 332
[EntityConfigOrder(1)]
public class PrehistoricPacuConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000635 RID: 1589 RVA: 0x0002D64C File Offset: 0x0002B84C
	public static GameObject CreatePrehistoricPacu(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BasePrehistoricPacuConfig.CreatePrefab(id, "PrehistoricPacuBaseTrait", name, desc, anim_file, is_baby, null, 273.15f, 333.15f, 253.15f, 373.15f), PrehistoricPacuTuning.PEN_SIZE_PER_CREATURE, false);
		EntityTemplates.CreateAndRegisterBaggedCreature(gameObject, true, true, false);
		Trait trait = Db.Get().CreateTrait("PrehistoricPacuBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PrehistoricPacuTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PrehistoricPacuTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 50f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		return gameObject;
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0002D76C File Offset: 0x0002B96C
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0002D773 File Offset: 0x0002B973
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0002D778 File Offset: 0x0002B978
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(PrehistoricPacuConfig.CreatePrehistoricPacu("PrehistoricPacu", CREATURES.SPECIES.PREHISTORICPACU.NAME, CREATURES.SPECIES.PREHISTORICPACU.DESC, "paculacanth_kanim", false), this, "PrehistoricPacuEgg", CREATURES.SPECIES.PREHISTORICPACU.EGG_NAME, CREATURES.SPECIES.PREHISTORICPACU.DESC, "egg_paculacanth_kanim", PrehistoricPacuTuning.EGG_MASS, "PrehistoricPacuBaby", 60.000004f, 20f, PrehistoricPacuTuning.EGG_CHANCES_BASE, 500, false, true, 0.75f, false);
		gameObject.AddTag(GameTags.LargeCreature);
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0002D809 File Offset: 0x0002BA09
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.AddOrGet<LoopingSounds>();
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0002D812 File Offset: 0x0002BA12
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004A2 RID: 1186
	public const string ID = "PrehistoricPacu";

	// Token: 0x040004A3 RID: 1187
	public const string BASE_TRAIT_ID = "PrehistoricPacuBaseTrait";

	// Token: 0x040004A4 RID: 1188
	public const string EGG_ID = "PrehistoricPacuEgg";

	// Token: 0x040004A5 RID: 1189
	public const int EGG_SORT_ORDER = 500;
}
