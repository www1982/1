using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000316 RID: 790
public class LonelyMinionConfig : IEntityConfig
{
	// Token: 0x06001048 RID: 4168 RVA: 0x00061404 File Offset: 0x0005F604
	public GameObject CreatePrefab()
	{
		string text = DUPLICANTS.MODEL.STANDARD.NAME;
		GameObject gameObject = EntityTemplates.CreateEntity(LonelyMinionConfig.ID, text, true);
		gameObject.AddComponent<Accessorizer>();
		gameObject.AddOrGet<WearableAccessorizer>();
		gameObject.AddComponent<Storage>().doDiseaseTransfer = false;
		gameObject.AddComponent<StateMachineController>();
		LonelyMinion.Def def = gameObject.AddOrGetDef<LonelyMinion.Def>();
		def.Personality = Db.Get().Personalities.Get("JORGE");
		def.Personality.Disabled = true;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.defaultAnim = "idle_default";
		kbatchedAnimController.initialAnim = "idle_default";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.AnimFiles = new KAnimFile[]
		{
			Assets.GetAnim("body_comp_default_kanim"),
			Assets.GetAnim("anim_idles_default_kanim"),
			Assets.GetAnim("anim_interacts_lonely_dupe_kanim")
		};
		this.ConfigurePackageOverride(gameObject);
		SymbolOverrideController symbolOverrideController = SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		symbolOverrideController.applySymbolOverridesEveryFrame = true;
		symbolOverrideController.AddSymbolOverride("snapto_cheek", Assets.GetAnim("head_swap_kanim").GetData().build.GetSymbol(string.Format("cheek_00{0}", def.Personality.headShape)), 1);
		BaseMinionConfig.ConfigureSymbols(gameObject, true);
		return gameObject;
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x00061545 File Offset: 0x0005F745
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x00061547 File Offset: 0x0005F747
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x0006154C File Offset: 0x0005F74C
	private void ConfigurePackageOverride(GameObject go)
	{
		GameObject gameObject = new GameObject("PackageSnapPoint");
		gameObject.transform.SetParent(go.transform);
		KBatchedAnimController component = go.GetComponent<KBatchedAnimController>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.transform.position = Vector3.forward * -0.1f;
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("mushbar_kanim") };
		kbatchedAnimController.initialAnim = "object";
		component.SetSymbolVisiblity(LonelyMinionConfig.PARCEL_SNAPTO, false);
		KBatchedAnimTracker kbatchedAnimTracker = gameObject.AddOrGet<KBatchedAnimTracker>();
		kbatchedAnimTracker.controller = component;
		kbatchedAnimTracker.symbol = LonelyMinionConfig.PARCEL_SNAPTO;
	}

	// Token: 0x04000A54 RID: 2644
	public static string ID = "LonelyMinion";

	// Token: 0x04000A55 RID: 2645
	public const int VOICE_IDX = -2;

	// Token: 0x04000A56 RID: 2646
	public const int STARTING_SKILL_POINTS = 3;

	// Token: 0x04000A57 RID: 2647
	public const int BASE_ATTRIBUTE_LEVEL = 7;

	// Token: 0x04000A58 RID: 2648
	public const int AGE_MIN = 2190;

	// Token: 0x04000A59 RID: 2649
	public const int AGE_MAX = 3102;

	// Token: 0x04000A5A RID: 2650
	public const float MIN_IDLE_DELAY = 20f;

	// Token: 0x04000A5B RID: 2651
	public const float MAX_IDLE_DELAY = 40f;

	// Token: 0x04000A5C RID: 2652
	public const string IDLE_PREFIX = "idle_blinds";

	// Token: 0x04000A5D RID: 2653
	public static readonly HashedString GreetingCriteraId = "Neighbor";

	// Token: 0x04000A5E RID: 2654
	public static readonly HashedString FoodCriteriaId = "FoodQuality";

	// Token: 0x04000A5F RID: 2655
	public static readonly HashedString DecorCriteriaId = "Decor";

	// Token: 0x04000A60 RID: 2656
	public static readonly HashedString PowerCriteriaId = "SuppliedPower";

	// Token: 0x04000A61 RID: 2657
	public static readonly HashedString CHECK_MAIL = "mail_pre";

	// Token: 0x04000A62 RID: 2658
	public static readonly HashedString CHECK_MAIL_SUCCESS = "mail_success_pst";

	// Token: 0x04000A63 RID: 2659
	public static readonly HashedString CHECK_MAIL_FAILURE = "mail_failure_pst";

	// Token: 0x04000A64 RID: 2660
	public static readonly HashedString CHECK_MAIL_DUPLICATE = "mail_duplicate_pst";

	// Token: 0x04000A65 RID: 2661
	public static readonly HashedString FOOD_SUCCESS = "food_like_loop";

	// Token: 0x04000A66 RID: 2662
	public static readonly HashedString FOOD_FAILURE = "food_dislike_loop";

	// Token: 0x04000A67 RID: 2663
	public static readonly HashedString FOOD_DUPLICATE = "food_duplicate_loop";

	// Token: 0x04000A68 RID: 2664
	public static readonly HashedString FOOD_IDLE = "idle_food_quest";

	// Token: 0x04000A69 RID: 2665
	public static readonly HashedString DECOR_IDLE = "idle_decor_quest";

	// Token: 0x04000A6A RID: 2666
	public static readonly HashedString POWER_IDLE = "idle_power_quest";

	// Token: 0x04000A6B RID: 2667
	public static readonly HashedString BLINDS_IDLE_0 = "idle_blinds_0";

	// Token: 0x04000A6C RID: 2668
	public static readonly HashedString PARCEL_SNAPTO = "parcel_snapTo";

	// Token: 0x04000A6D RID: 2669
	public const string PERSONALITY_ID = "JORGE";

	// Token: 0x04000A6E RID: 2670
	public const string BODY_ANIM_FILE = "body_lonelyminion_kanim";
}
