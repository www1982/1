using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B4 RID: 692
public class LonelyMinionHouseConfig : IBuildingConfig
{
	// Token: 0x06000E03 RID: 3587 RVA: 0x00051EB4 File Offset: 0x000500B4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LonelyMinionHouse";
		int num = 4;
		int num2 = 6;
		string text2 = "lonely_dupe_home_kanim";
		int num3 = 1000;
		float num4 = 480f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, LonelyMinionHouseConfig.HOUSE_DECOR, none, 0.2f);
		buildingDef.DefaultAnimState = "on";
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.AddLogicPowerPort = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(2, 1);
		buildingDef.ShowInBuildMenu = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x00051F6C File Offset: 0x0005016C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<NonEssentialEnergyConsumer>();
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
		Prioritizable.AddRef(go);
		go.GetComponent<Prioritizable>().SetMasterPriority(new PrioritySetting(PriorityScreen.PriorityClass.high, 5));
		Storage storage = go.AddOrGet<Storage>();
		KnockKnock knockKnock = go.AddOrGet<KnockKnock>();
		LonelyMinionHouse.Def def = go.AddOrGetDef<LonelyMinionHouse.Def>();
		storage.allowItemRemoval = false;
		storage.capacityKg = 250000f;
		storage.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
		storage.storageFullMargin = global::TUNING.STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
		storage.showCapacityStatusItem = true;
		storage.showCapacityAsMainStatus = true;
		knockKnock.triggerWorkReactions = false;
		knockKnock.synchronizeAnims = false;
		knockKnock.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_doorknock_kanim") };
		knockKnock.workAnims = new HashedString[] { "knocking_pre", "knocking_loop" };
		knockKnock.workingPstComplete = new HashedString[] { "knocking_pst" };
		knockKnock.workingPstFailed = null;
		knockKnock.SetButtonTextOverride(new ButtonMenuTextOverride
		{
			Text = CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.TEXT,
			CancelText = CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.CANCELTEXT,
			ToolTip = CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.TOOLTIP,
			CancelToolTip = CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.CANCEL_TOOLTIP
		});
		def.Story = Db.Get().Stories.LonelyMinion;
		def.CompletionData = new StoryCompleteData
		{
			KeepSakeSpawnOffset = default(CellOffset),
			CameraTargetOffset = new CellOffset(0, 3)
		};
		def.InitalLoreId = "story_trait_lonelyminion_initial";
		def.EventIntroInfo = new StoryManager.PopupInfo
		{
			Title = CODEX.STORY_TRAITS.LONELYMINION.BEGIN_POPUP.NAME,
			Description = CODEX.STORY_TRAITS.LONELYMINION.BEGIN_POPUP.DESCRIPTION,
			CloseButtonText = CODEX.STORY_TRAITS.CLOSE_BUTTON,
			TextureName = "minionhouseactivate_kanim",
			DisplayImmediate = true,
			PopupType = EventInfoDataHelper.PopupType.BEGIN
		};
		def.CompleteLoreId = "story_trait_lonelyminion_complete";
		def.EventCompleteInfo = new StoryManager.PopupInfo
		{
			Title = CODEX.STORY_TRAITS.LONELYMINION.END_POPUP.NAME,
			Description = CODEX.STORY_TRAITS.LONELYMINION.END_POPUP.DESCRIPTION,
			CloseButtonText = CODEX.STORY_TRAITS.LONELYMINION.END_POPUP.BUTTON,
			TextureName = "minionhousecomplete_kanim",
			PopupType = EventInfoDataHelper.PopupType.COMPLETE
		};
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x000521BC File Offset: 0x000503BC
	public override void DoPostConfigureComplete(GameObject go)
	{
		global::UnityEngine.Object.Destroy(go.GetComponent<BuildingEnabledButton>());
		go.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.None;
		this.ConfigureLights(go);
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x000521DC File Offset: 0x000503DC
	private void ConfigureLights(GameObject go)
	{
		GameObject gameObject = new GameObject("FestiveLights");
		gameObject.SetActive(false);
		gameObject.transform.SetParent(go.transform);
		gameObject.AddOrGet<Light2D>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		KBatchedAnimController component = go.GetComponent<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = component.AnimFiles;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.NoLayer;
		kbatchedAnimController.initialAnim = "meter_lights_off";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.FlipX = component.FlipX;
		kbatchedAnimController.FlipY = component.FlipY;
		KBatchedAnimTracker kbatchedAnimTracker = gameObject.AddComponent<KBatchedAnimTracker>();
		kbatchedAnimTracker.SetAnimControllers(kbatchedAnimController, component);
		kbatchedAnimTracker.symbol = "lights_target";
		kbatchedAnimTracker.offset = Vector3.zero;
		for (int i = 0; i < LonelyMinionHouseConfig.LIGHTS_SYMBOLS.Length; i++)
		{
			component.SetSymbolVisiblity(LonelyMinionHouseConfig.LIGHTS_SYMBOLS[i], false);
		}
	}

	// Token: 0x04000906 RID: 2310
	public const string ID = "LonelyMinionHouse";

	// Token: 0x04000907 RID: 2311
	public const string LORE_UNLOCK_PREFIX = "story_trait_lonelyminion_";

	// Token: 0x04000908 RID: 2312
	public const int FriendshipQuestCount = 3;

	// Token: 0x04000909 RID: 2313
	public const string METER_TARGET = "meter_storage_target";

	// Token: 0x0400090A RID: 2314
	public const string METER_ANIM = "meter";

	// Token: 0x0400090B RID: 2315
	public static readonly string[] METER_SYMBOLS = new string[] { "meter_storage", "meter_level" };

	// Token: 0x0400090C RID: 2316
	public const string BLINDS_TARGET = "blinds_target";

	// Token: 0x0400090D RID: 2317
	public const string BLINDS_PREFIX = "meter_blinds";

	// Token: 0x0400090E RID: 2318
	public static readonly string[] BLINDS_SYMBOLS = new string[] { "blinds_target", "blind", "blind_string", "blinds" };

	// Token: 0x0400090F RID: 2319
	private const string LIGHTS_TARGET = "lights_target";

	// Token: 0x04000910 RID: 2320
	private static readonly string[] LIGHTS_SYMBOLS = new string[] { "lights_target", "festive_lights", "lights_wire", "light_bulb", "snapTo_light_locator" };

	// Token: 0x04000911 RID: 2321
	public static readonly HashedString ANSWER = "answer";

	// Token: 0x04000912 RID: 2322
	public static readonly HashedString LIGHTS_OFF = "meter_lights_off";

	// Token: 0x04000913 RID: 2323
	public static readonly HashedString LIGHTS_ON = "meter_lights_on_loop";

	// Token: 0x04000914 RID: 2324
	public static readonly HashedString STORAGE = "storage_off";

	// Token: 0x04000915 RID: 2325
	public static readonly HashedString STORAGE_WORK_PST = "working_pst";

	// Token: 0x04000916 RID: 2326
	public static readonly HashedString[] STORAGE_WORKING = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x04000917 RID: 2327
	public static readonly EffectorValues HOUSE_DECOR = new EffectorValues
	{
		amount = -25,
		radius = 6
	};

	// Token: 0x04000918 RID: 2328
	public static readonly EffectorValues STORAGE_DECOR = DECOR.PENALTY.TIER1;
}
