using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000C9 RID: 201
[EntityConfigOrder(1)]
public class ButterflyConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000379 RID: 889 RVA: 0x0001E5FC File Offset: 0x0001C7FC
	public static GameObject CreateButterfly(string id, string name, string desc, string anim_file)
	{
		GameObject gameObject = BaseButterflyConfig.BaseButterfly(id, name, desc, anim_file, "ButterflyBaseTrait", null);
		gameObject.AddOrGetDef<AgeMonitor.Def>();
		gameObject.AddOrGetDef<FixedCapturableMonitor.Def>();
		Trait trait = Db.Get().CreateTrait("ButterflyBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 5f, name, false, false, true));
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0001E6A0 File Offset: 0x0001C8A0
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0001E6A7 File Offset: 0x0001C8A7
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001E6AA File Offset: 0x0001C8AA
	public GameObject CreatePrefab()
	{
		GameObject gameObject = ButterflyConfig.CreateButterfly("Butterfly", CREATURES.SPECIES.BUTTERFLY.NAME, CREATURES.SPECIES.BUTTERFLY.DESC, "pollinator_kanim");
		gameObject.AddTag(GameTags.Creatures.Pollinator);
		return gameObject;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0001E6DA File Offset: 0x0001C8DA
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0001E6DC File Offset: 0x0001C8DC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002AD RID: 685
	public const string ID = "Butterfly";

	// Token: 0x040002AE RID: 686
	public const string BASE_TRAIT_ID = "ButterflyBaseTrait";
}
