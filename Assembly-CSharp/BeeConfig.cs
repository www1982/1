using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class BeeConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600036C RID: 876 RVA: 0x0001E4A4 File Offset: 0x0001C6A4
	public static GameObject CreateBee(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseBeeConfig.BaseBee(id, name, desc, anim_file, "BeeBaseTrait", DECOR.BONUS.TIER4, is_baby, null);
		Trait trait = Db.Get().CreateTrait("BeeBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 5f, name, false, false, true));
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0001E541 File Offset: 0x0001C741
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0001E548 File Offset: 0x0001C748
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0001E54B File Offset: 0x0001C74B
	public GameObject CreatePrefab()
	{
		return BeeConfig.CreateBee("Bee", global::STRINGS.CREATURES.SPECIES.BEE.NAME, global::STRINGS.CREATURES.SPECIES.BEE.DESC, "bee_kanim", false);
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0001E571 File Offset: 0x0001C771
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0001E573 File Offset: 0x0001C773
	public void OnSpawn(GameObject inst)
	{
		BaseBeeConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x040002AA RID: 682
	public const string ID = "Bee";

	// Token: 0x040002AB RID: 683
	public const string BASE_TRAIT_ID = "BeeBaseTrait";
}
