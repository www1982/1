using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class RemoteWorkerConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000678 RID: 1656 RVA: 0x0002E6F5 File Offset: 0x0002C8F5
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0002E6FC File Offset: 0x0002C8FC
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0002E700 File Offset: 0x0002C900
	public GameObject CreatePrefab()
	{
		string text = DUPLICANTS.MODEL.REMOTEWORKER.NAME;
		string text2 = DUPLICANTS.MODEL.REMOTEWORKER.DESC;
		GameObject gameObject = EntityTemplates.CreateEntity(RemoteWorkerConfig.ID, text, true);
		gameObject.AddOrGet<InfoDescription>().description = text2;
		gameObject.AddComponent<Accessorizer>();
		gameObject.AddOrGet<WearableAccessorizer>();
		gameObject.AddComponent<StateMachineController>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.defaultAnim = "in_dock_idle";
		kbatchedAnimController.initialAnim = "in_dock_idle";
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.AnimFiles = new KAnimFile[]
		{
			Assets.GetAnim("body_comp_default_kanim"),
			Assets.GetAnim("anim_idles_default_kanim"),
			Assets.GetAnim("anim_loco_new_kanim"),
			Assets.GetAnim(RemoteWorkerConfig.DOCK_ANIM_OVERRIDES)
		};
		gameObject.AddOrGet<AnimEventHandler>();
		SymbolOverrideController symbolOverrideController = SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		symbolOverrideController.applySymbolOverridesEveryFrame = true;
		symbolOverrideController.AddSymbolOverride("snapto_cheek", Assets.GetAnim("head_swap_kanim").GetData().build.GetSymbol("cheek_007"), 1);
		BaseMinionConfig.ConfigureSymbols(gameObject, true);
		Accessorizer component = gameObject.GetComponent<Accessorizer>();
		component.ApplyBodyData(RemoteWorkerConfig.CreateBodyData());
		component.ApplyAccessories();
		gameObject.AddTag(GameTags.Experimental);
		gameObject.AddTag(GameTags.Robot);
		KBoxCollider2D kboxCollider2D = gameObject.AddOrGet<KBoxCollider2D>();
		kboxCollider2D.size = new Vector2f(1f, 2f);
		kboxCollider2D.offset = new Vector2f(0f, 1f);
		KBoxCollider2D kboxCollider2D2 = gameObject.AddOrGet<KBoxCollider2D>();
		kboxCollider2D2.offset = new Vector2(0f, 0.75f);
		kboxCollider2D2.size = new Vector2(1f, 1.5f);
		Navigator navigator = gameObject.AddOrGet<Navigator>();
		navigator.NavGridName = "WalkerBabyNavGrid";
		navigator.CurrentNavType = NavType.Floor;
		navigator.defaultSpeed = 1f;
		navigator.updateProber = true;
		navigator.maxProbingRadius = 0;
		navigator.sceneLayer = Grid.SceneLayer.Creatures;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.ElementID = SimHashes.Steel;
		primaryElement.Mass = 200f;
		gameObject.AddComponent<RemoteWorkerExperienceProxy>();
		gameObject.AddComponent<RemoteWorker>();
		gameObject.AddComponent<RemoteWorkerSM>();
		gameObject.AddComponent<ChoreConsumer>();
		gameObject.AddComponent<Pickupable>();
		gameObject.AddComponent<SaveLoadRoot>();
		gameObject.AddComponent<Storage>().SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		gameObject.AddOrGet<Clearable>().isClearable = false;
		gameObject.AddOrGetDef<CreatureFallMonitor.Def>().canSwim = false;
		return gameObject;
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0002E95C File Offset: 0x0002CB5C
	public void OnPrefabInit(GameObject go)
	{
		Navigator navigator = go.AddOrGet<Navigator>();
		navigator.SetAbilities(new CreaturePathFinderAbilities(navigator));
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0002E96F File Offset: 0x0002CB6F
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0002E974 File Offset: 0x0002CB74
	public static KCompBuilder.BodyData CreateBodyData()
	{
		return new KCompBuilder.BodyData
		{
			eyes = HashCache.Get().Add("eyes_014"),
			hair = HashCache.Get().Add("hair_051"),
			headShape = HashCache.Get().Add("headshape_006"),
			mouth = HashCache.Get().Add("mouth_007"),
			neck = HashCache.Get().Add("neck"),
			arms = HashCache.Get().Add("arm_sleeve_006"),
			armslower = HashCache.Get().Add("arm_lower_sleeve_006"),
			body = HashCache.Get().Add("torso_006"),
			hat = HashedString.Invalid,
			faceFX = HashedString.Invalid,
			armLowerSkin = HashCache.Get().Add("arm_lower_001"),
			armUpperSkin = HashCache.Get().Add("arm_upper_001"),
			legSkin = HashCache.Get().Add("leg_skin_001"),
			neck = HashCache.Get().Add("neck_006"),
			legs = HashCache.Get().Add("leg_006"),
			belt = HashCache.Get().Add("belt_006"),
			pelvis = HashCache.Get().Add("pelvis_006"),
			foot = HashCache.Get().Add("foot_006"),
			hand = HashCache.Get().Add("hand_paint_006"),
			cuff = HashCache.Get().Add("cuff_006")
		};
	}

	// Token: 0x040004DE RID: 1246
	public static readonly string ID = "RemoteWorker";

	// Token: 0x040004DF RID: 1247
	public const float MASS_KG = 200f;

	// Token: 0x040004E0 RID: 1248
	public const float DEBRIS_MASS_KG = 42f;

	// Token: 0x040004E1 RID: 1249
	public static readonly string DOCK_ANIM_OVERRIDES = "anim_interacts_remote_work_dock_kanim";

	// Token: 0x040004E2 RID: 1250
	public static readonly string IDLE_IN_DOCK_ANIM = "in_dock_idle";

	// Token: 0x040004E3 RID: 1251
	public static readonly string BUILD_MATERIAL = "Steel";

	// Token: 0x040004E4 RID: 1252
	public static readonly Tag BUILD_MATERIAL_TAG = new Tag(RemoteWorkerConfig.BUILD_MATERIAL);
}
