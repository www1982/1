using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using KMod;
using TUNING;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x020006A9 RID: 1705
[AddComponentMenu("KMonoBehaviour/scripts/Assets")]
public class Assets : KMonoBehaviour, ISerializationCallbackReceiver
{
	// Token: 0x0600298D RID: 10637 RVA: 0x000F1654 File Offset: 0x000EF854
	protected override void OnPrefabInit()
	{
		Assets.instance = this;
		if (KPlayerPrefs.HasKey("TemperatureUnit"))
		{
			GameUtil.temperatureUnit = (GameUtil.TemperatureUnit)KPlayerPrefs.GetInt("TemperatureUnit");
		}
		if (KPlayerPrefs.HasKey("MassUnit"))
		{
			GameUtil.massUnit = (GameUtil.MassUnit)KPlayerPrefs.GetInt("MassUnit");
		}
		RecipeManager.DestroyInstance();
		RecipeManager.Get();
		Assets.AnimMaterial = this.AnimMaterialAsset;
		Assets.Prefabs = new List<KPrefabID>(this.PrefabAssets.Where((KPrefabID x) => x != null));
		Assets.PrefabsByTag.Clear();
		Assets.PrefabsByAdditionalTags.Clear();
		Assets.CountableTags.Clear();
		Assets.Sprites = new Dictionary<HashedString, Sprite>();
		foreach (Sprite sprite in this.SpriteAssets)
		{
			if (!(sprite == null))
			{
				HashedString hashedString = new HashedString(sprite.name);
				Assets.Sprites.Add(hashedString, sprite);
			}
		}
		Assets.TintedSprites = this.TintedSpriteAssets.Where((TintedSprite x) => x != null && x.sprite != null).ToList<TintedSprite>();
		Assets.Materials = this.MaterialAssets.Where((Material x) => x != null).ToList<Material>();
		Assets.Textures = this.TextureAssets.Where((Texture2D x) => x != null).ToList<Texture2D>();
		Assets.TextureAtlases = this.TextureAtlasAssets.Where((TextureAtlas x) => x != null).ToList<TextureAtlas>();
		Assets.BlockTileDecorInfos = this.BlockTileDecorInfoAssets.Where((BlockTileDecorInfo x) => x != null).ToList<BlockTileDecorInfo>();
		this.LoadAnims();
		Assets.UIPrefabs = this.UIPrefabAssets;
		Assets.DebugFont = this.DebugFontAsset;
		AsyncLoadManager<IGlobalAsyncLoader>.Run();
		GameAudioSheets.Get().Initialize();
		this.SubstanceListHookup();
		this.CreatePrefabs();
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x000F18AC File Offset: 0x000EFAAC
	private void CreatePrefabs()
	{
		Db.Get();
		Assets.BuildingDefs = new List<BuildingDef>();
		foreach (KPrefabID kprefabID in this.PrefabAssets)
		{
			if (!(kprefabID == null))
			{
				kprefabID.InitializeTags(true);
				Assets.AddPrefab(kprefabID);
			}
		}
		LegacyModMain.Load();
		Db.Get().PostProcess();
		ComplexRecipeManager.Get().PostProcess();
	}

	// Token: 0x0600298F RID: 10639 RVA: 0x000F1938 File Offset: 0x000EFB38
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Db.Get();
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x000F1948 File Offset: 0x000EFB48
	private static void TryAddCountableTag(KPrefabID prefab)
	{
		foreach (Tag tag in GameTags.DisplayAsUnits)
		{
			if (prefab.HasTag(tag))
			{
				Assets.AddCountableTag(prefab.PrefabTag);
				break;
			}
		}
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x000F19A4 File Offset: 0x000EFBA4
	public static void AddCountableTag(Tag tag)
	{
		Assets.CountableTags.Add(tag);
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x000F19B2 File Offset: 0x000EFBB2
	public static bool IsTagCountable(Tag tag)
	{
		return Assets.CountableTags.Contains(tag);
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x000F19BF File Offset: 0x000EFBBF
	private static void TryAddSolidTransferArmConveyableTag(KPrefabID prefab)
	{
		if (prefab.HasAnyTags(STORAGEFILTERS.SOLID_TRANSFER_ARM_CONVEYABLE))
		{
			Assets.SolidTransferArmConeyableTags.Add(prefab.PrefabTag);
		}
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x000F19DF File Offset: 0x000EFBDF
	public static bool IsTagSolidTransferArmConveyable(Tag tag)
	{
		return Assets.SolidTransferArmConeyableTags.Contains(tag);
	}

	// Token: 0x06002995 RID: 10645 RVA: 0x000F19EC File Offset: 0x000EFBEC
	private void LoadAnims()
	{
		KAnimBatchManager.DestroyInstance();
		KAnimGroupFile.DestroyInstance();
		KGlobalAnimParser.DestroyInstance();
		KAnimBatchManager.CreateInstance();
		KGlobalAnimParser.CreateInstance();
		KAnimGroupFile.LoadGroupResourceFile();
		if (BundledAssetsLoader.instance.Expansion1Assets != null)
		{
			this.AnimAssets.AddRange(BundledAssetsLoader.instance.Expansion1Assets.AnimAssets);
		}
		foreach (BundledAssets bundledAssets in BundledAssetsLoader.instance.DlcAssetsList)
		{
			this.AnimAssets.AddRange(bundledAssets.AnimAssets);
		}
		Assets.Anims = this.AnimAssets.Where((KAnimFile x) => x != null).ToList<KAnimFile>();
		Assets.Anims.AddRange(Assets.ModLoadedKAnims);
		Assets.AnimTable.Clear();
		foreach (KAnimFile kanimFile in Assets.Anims)
		{
			if (kanimFile != null)
			{
				HashedString hashedString = kanimFile.name;
				Assets.AnimTable[hashedString] = kanimFile;
			}
		}
		KAnimGroupFile.MapNamesToAnimFiles(Assets.AnimTable);
		Global.Instance.modManager.Load(Content.Animation);
		Assets.Anims.AddRange(Assets.ModLoadedKAnims);
		foreach (KAnimFile kanimFile2 in Assets.ModLoadedKAnims)
		{
			if (kanimFile2 != null)
			{
				HashedString hashedString2 = kanimFile2.name;
				Assets.AnimTable[hashedString2] = kanimFile2;
			}
		}
		global::Debug.Assert(Assets.AnimTable.Count > 0, "Anim Assets not yet loaded");
		KAnimGroupFile.LoadAll();
		foreach (KAnimFile kanimFile3 in Assets.Anims)
		{
			kanimFile3.FinalizeLoading();
		}
		KAnimBatchManager.Instance().CompleteInit();
	}

	// Token: 0x06002996 RID: 10646 RVA: 0x000F1C30 File Offset: 0x000EFE30
	private void SubstanceListHookup()
	{
		Dictionary<string, SubstanceTable> dictionary = new Dictionary<string, SubstanceTable> { { "", this.substanceTable } };
		if (BundledAssetsLoader.instance.Expansion1Assets != null)
		{
			dictionary["EXPANSION1_ID"] = BundledAssetsLoader.instance.Expansion1Assets.SubstanceTable;
		}
		Hashtable hashtable = new Hashtable();
		ElementsAudio.Instance.LoadData(AsyncLoadManager<IGlobalAsyncLoader>.AsyncLoader<ElementAudioFileLoader>.Get().entries);
		ElementLoader.Load(ref hashtable, dictionary);
		List<Element> list = ElementLoader.elements.FindAll((Element e) => e.HasTag(GameTags.StartingMetalOre));
		GameTags.StartingMetalOres = new Tag[list.Count];
		for (int i = 0; i < list.Count; i++)
		{
			GameTags.StartingMetalOres[i] = list[i].tag;
		}
		GameTags.BasicMetalOres = GameTags.StartingMetalOres.Append(GameTags.BasicMetalOres);
		List<Element> list2 = ElementLoader.elements.FindAll((Element e) => e.HasTag(GameTags.StartingRefinedMetal));
		GameTags.StartingRefinedMetals = new Tag[list2.Count];
		for (int j = 0; j < list2.Count; j++)
		{
			GameTags.StartingRefinedMetals[j] = list2[j].tag;
		}
		GameTags.BasicRefinedMetals = GameTags.StartingRefinedMetals.Append(GameTags.BasicRefinedMetals);
	}

	// Token: 0x06002997 RID: 10647 RVA: 0x000F1D9A File Offset: 0x000EFF9A
	public static string GetSimpleSoundEventName(EventReference event_ref)
	{
		return Assets.GetSimpleSoundEventName(KFMOD.GetEventReferencePath(event_ref));
	}

	// Token: 0x06002998 RID: 10648 RVA: 0x000F1DA8 File Offset: 0x000EFFA8
	public static string GetSimpleSoundEventName(string path)
	{
		string text = null;
		if (!Assets.simpleSoundEventNames.TryGetValue(path, out text))
		{
			int num = path.LastIndexOf('/');
			text = ((num != -1) ? path.Substring(num + 1) : path);
			Assets.simpleSoundEventNames[path] = text;
		}
		return text;
	}

	// Token: 0x06002999 RID: 10649 RVA: 0x000F1DF0 File Offset: 0x000EFFF0
	private static BuildingDef GetDef(IList<BuildingDef> defs, string prefab_id)
	{
		int count = defs.Count;
		for (int i = 0; i < count; i++)
		{
			if (defs[i].PrefabID == prefab_id)
			{
				return defs[i];
			}
		}
		return null;
	}

	// Token: 0x0600299A RID: 10650 RVA: 0x000F1E2D File Offset: 0x000F002D
	public static BuildingDef GetBuildingDef(string prefab_id)
	{
		return Assets.GetDef(Assets.BuildingDefs, prefab_id);
	}

	// Token: 0x0600299B RID: 10651 RVA: 0x000F1E3C File Offset: 0x000F003C
	public static TintedSprite GetTintedSprite(string name)
	{
		TintedSprite tintedSprite = null;
		if (Assets.TintedSprites != null)
		{
			for (int i = 0; i < Assets.TintedSprites.Count; i++)
			{
				if (Assets.TintedSprites[i].sprite.name == name)
				{
					tintedSprite = Assets.TintedSprites[i];
					break;
				}
			}
		}
		return tintedSprite;
	}

	// Token: 0x0600299C RID: 10652 RVA: 0x000F1E94 File Offset: 0x000F0094
	public static Sprite GetSprite(HashedString name)
	{
		Sprite sprite = null;
		if (Assets.Sprites != null)
		{
			Assets.Sprites.TryGetValue(name, out sprite);
		}
		return sprite;
	}

	// Token: 0x0600299D RID: 10653 RVA: 0x000F1EB9 File Offset: 0x000F00B9
	public static VideoClip GetVideo(string name)
	{
		return Resources.Load<VideoClip>("video_webm/" + name);
	}

	// Token: 0x0600299E RID: 10654 RVA: 0x000F1ECC File Offset: 0x000F00CC
	public static Texture2D GetTexture(string name)
	{
		Texture2D texture2D = null;
		if (Assets.Textures != null)
		{
			for (int i = 0; i < Assets.Textures.Count; i++)
			{
				if (Assets.Textures[i].name == name)
				{
					texture2D = Assets.Textures[i];
					break;
				}
			}
		}
		return texture2D;
	}

	// Token: 0x0600299F RID: 10655 RVA: 0x000F1F20 File Offset: 0x000F0120
	public static ComicData GetComic(string id)
	{
		foreach (ComicData comicData in Assets.instance.comics)
		{
			if (comicData.name == id)
			{
				return comicData;
			}
		}
		return null;
	}

	// Token: 0x060029A0 RID: 10656 RVA: 0x000F1F5C File Offset: 0x000F015C
	public static void AddPrefab(KPrefabID prefab)
	{
		if (prefab == null)
		{
			return;
		}
		prefab.InitializeTags(true);
		prefab.UpdateSaveLoadTag();
		if (Assets.PrefabsByTag.ContainsKey(prefab.PrefabTag))
		{
			string text = "Tried loading prefab with duplicate tag, ignoring: ";
			Tag prefabTag = prefab.PrefabTag;
			global::Debug.LogWarning(text + prefabTag.ToString());
			return;
		}
		Assets.PrefabsByTag[prefab.PrefabTag] = prefab;
		foreach (Tag tag in prefab.Tags)
		{
			if (!Assets.PrefabsByAdditionalTags.ContainsKey(tag))
			{
				Assets.PrefabsByAdditionalTags[tag] = new List<KPrefabID>();
			}
			Assets.PrefabsByAdditionalTags[tag].Add(prefab);
		}
		Assets.Prefabs.Add(prefab);
		Assets.TryAddCountableTag(prefab);
		Assets.TryAddSolidTransferArmConveyableTag(prefab);
		if (Assets.OnAddPrefab != null)
		{
			Assets.OnAddPrefab(prefab);
		}
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x000F2060 File Offset: 0x000F0260
	public static void RegisterOnAddPrefab(Action<KPrefabID> on_add)
	{
		Assets.OnAddPrefab = (Action<KPrefabID>)Delegate.Combine(Assets.OnAddPrefab, on_add);
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			on_add(kprefabID);
		}
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x000F20C8 File Offset: 0x000F02C8
	public static void UnregisterOnAddPrefab(Action<KPrefabID> on_add)
	{
		Assets.OnAddPrefab = (Action<KPrefabID>)Delegate.Remove(Assets.OnAddPrefab, on_add);
	}

	// Token: 0x060029A3 RID: 10659 RVA: 0x000F20DF File Offset: 0x000F02DF
	public static void ClearOnAddPrefab()
	{
		Assets.OnAddPrefab = null;
	}

	// Token: 0x060029A4 RID: 10660 RVA: 0x000F20E8 File Offset: 0x000F02E8
	public static GameObject GetPrefab(Tag tag)
	{
		GameObject gameObject = Assets.TryGetPrefab(tag);
		if (gameObject == null)
		{
			string text = "Missing prefab: ";
			Tag tag2 = tag;
			global::Debug.LogWarning(text + tag2.ToString());
		}
		return gameObject;
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x000F2124 File Offset: 0x000F0324
	public static GameObject TryGetPrefab(Tag tag)
	{
		KPrefabID kprefabID = null;
		Assets.PrefabsByTag.TryGetValue(tag, out kprefabID);
		if (!(kprefabID != null))
		{
			return null;
		}
		return kprefabID.gameObject;
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x000F2154 File Offset: 0x000F0354
	public static List<GameObject> GetPrefabsWithTag(Tag tag)
	{
		List<GameObject> list = new List<GameObject>();
		if (Assets.PrefabsByAdditionalTags.ContainsKey(tag))
		{
			for (int i = 0; i < Assets.PrefabsByAdditionalTags[tag].Count; i++)
			{
				list.Add(Assets.PrefabsByAdditionalTags[tag][i].gameObject);
			}
		}
		return list;
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x000F21AC File Offset: 0x000F03AC
	public static List<GameObject> GetPrefabsWithComponent<Type>()
	{
		List<GameObject> list = new List<GameObject>();
		for (int i = 0; i < Assets.Prefabs.Count; i++)
		{
			if (Assets.Prefabs[i].GetComponent<Type>() != null)
			{
				list.Add(Assets.Prefabs[i].gameObject);
			}
		}
		return list;
	}

	// Token: 0x060029A8 RID: 10664 RVA: 0x000F2204 File Offset: 0x000F0404
	public static List<Type> GetPrefabsWithComponentAsListOfComponents<Type>()
	{
		List<Type> list = new List<Type>();
		for (int i = 0; i < Assets.Prefabs.Count; i++)
		{
			Type component = Assets.Prefabs[i].GetComponent<Type>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x060029A9 RID: 10665 RVA: 0x000F224D File Offset: 0x000F044D
	public static GameObject GetPrefabWithComponent<Type>()
	{
		List<GameObject> prefabsWithComponent = Assets.GetPrefabsWithComponent<Type>();
		global::Debug.Assert(prefabsWithComponent.Count > 0, "There are no prefabs of type " + typeof(Type).Name);
		return prefabsWithComponent[0];
	}

	// Token: 0x060029AA RID: 10666 RVA: 0x000F2284 File Offset: 0x000F0484
	public static List<Tag> GetPrefabTagsWithComponent<Type>()
	{
		List<Tag> list = new List<Tag>();
		for (int i = 0; i < Assets.Prefabs.Count; i++)
		{
			if (Assets.Prefabs[i].GetComponent<Type>() != null)
			{
				list.Add(Assets.Prefabs[i].PrefabID());
			}
		}
		return list;
	}

	// Token: 0x060029AB RID: 10667 RVA: 0x000F22DC File Offset: 0x000F04DC
	public static Assets GetInstanceEditorOnly()
	{
		Assets[] array = (Assets[])Resources.FindObjectsOfTypeAll(typeof(Assets));
		if (array != null)
		{
			int num = array.Length;
		}
		return array[0];
	}

	// Token: 0x060029AC RID: 10668 RVA: 0x000F2308 File Offset: 0x000F0508
	public static TextureAtlas GetTextureAtlas(string name)
	{
		foreach (TextureAtlas textureAtlas in Assets.TextureAtlases)
		{
			if (textureAtlas.name == name)
			{
				return textureAtlas;
			}
		}
		return null;
	}

	// Token: 0x060029AD RID: 10669 RVA: 0x000F2368 File Offset: 0x000F0568
	public static Material GetMaterial(string name)
	{
		foreach (Material material in Assets.Materials)
		{
			if (material.name == name)
			{
				return material;
			}
		}
		return null;
	}

	// Token: 0x060029AE RID: 10670 RVA: 0x000F23C8 File Offset: 0x000F05C8
	public static BlockTileDecorInfo GetBlockTileDecorInfo(string name)
	{
		foreach (BlockTileDecorInfo blockTileDecorInfo in Assets.BlockTileDecorInfos)
		{
			if (blockTileDecorInfo.name == name)
			{
				return blockTileDecorInfo;
			}
		}
		global::Debug.LogError("Could not find BlockTileDecorInfo named [" + name + "]");
		return null;
	}

	// Token: 0x060029AF RID: 10671 RVA: 0x000F2440 File Offset: 0x000F0640
	public static KAnimFile GetAnim(HashedString name)
	{
		if (!name.IsValid)
		{
			global::Debug.LogWarning("Invalid hash name");
			return null;
		}
		KAnimFile kanimFile = null;
		Assets.AnimTable.TryGetValue(name, out kanimFile);
		if (kanimFile == null)
		{
			global::Debug.LogWarning("Missing Anim: [" + name.ToString() + "]. You may have to run Collect Anim on the Assets prefab");
		}
		return kanimFile;
	}

	// Token: 0x060029B0 RID: 10672 RVA: 0x000F249D File Offset: 0x000F069D
	public static bool TryGetAnim(HashedString name, out KAnimFile anim)
	{
		if (!name.IsValid)
		{
			global::Debug.LogWarning("Invalid hash name");
			anim = null;
			return false;
		}
		Assets.AnimTable.TryGetValue(name, out anim);
		return anim != null;
	}

	// Token: 0x060029B1 RID: 10673 RVA: 0x000F24CC File Offset: 0x000F06CC
	public void OnAfterDeserialize()
	{
		this.TintedSpriteAssets = this.TintedSpriteAssets.Where((TintedSprite x) => x != null && x.sprite != null).ToList<TintedSprite>();
		this.TintedSpriteAssets.Sort((TintedSprite a, TintedSprite b) => a.name.CompareTo(b.name));
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x000F2538 File Offset: 0x000F0738
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x000F253C File Offset: 0x000F073C
	public static void AddBuildingDef(BuildingDef def)
	{
		Assets.BuildingDefs = Assets.BuildingDefs.Where((BuildingDef x) => x.PrefabID != def.PrefabID).ToList<BuildingDef>();
		Assets.BuildingDefs.Add(def);
	}

	// Token: 0x040018A0 RID: 6304
	public static List<KAnimFile> ModLoadedKAnims = new List<KAnimFile>();

	// Token: 0x040018A1 RID: 6305
	private static Action<KPrefabID> OnAddPrefab;

	// Token: 0x040018A2 RID: 6306
	public static List<BuildingDef> BuildingDefs;

	// Token: 0x040018A3 RID: 6307
	public List<KPrefabID> PrefabAssets = new List<KPrefabID>();

	// Token: 0x040018A4 RID: 6308
	public static List<KPrefabID> Prefabs = new List<KPrefabID>();

	// Token: 0x040018A5 RID: 6309
	private static HashSet<Tag> CountableTags = new HashSet<Tag>();

	// Token: 0x040018A6 RID: 6310
	private static HashSet<Tag> SolidTransferArmConeyableTags = new HashSet<Tag>();

	// Token: 0x040018A7 RID: 6311
	public List<Sprite> SpriteAssets;

	// Token: 0x040018A8 RID: 6312
	public static Dictionary<HashedString, Sprite> Sprites;

	// Token: 0x040018A9 RID: 6313
	public List<string> videoClipNames;

	// Token: 0x040018AA RID: 6314
	private const string VIDEO_ASSET_PATH = "video_webm";

	// Token: 0x040018AB RID: 6315
	public List<TintedSprite> TintedSpriteAssets;

	// Token: 0x040018AC RID: 6316
	public static List<TintedSprite> TintedSprites;

	// Token: 0x040018AD RID: 6317
	public List<Texture2D> TextureAssets;

	// Token: 0x040018AE RID: 6318
	public static List<Texture2D> Textures;

	// Token: 0x040018AF RID: 6319
	public static List<TextureAtlas> TextureAtlases;

	// Token: 0x040018B0 RID: 6320
	public List<TextureAtlas> TextureAtlasAssets;

	// Token: 0x040018B1 RID: 6321
	public static List<Material> Materials;

	// Token: 0x040018B2 RID: 6322
	public List<Material> MaterialAssets;

	// Token: 0x040018B3 RID: 6323
	public static List<Shader> Shaders;

	// Token: 0x040018B4 RID: 6324
	public List<Shader> ShaderAssets;

	// Token: 0x040018B5 RID: 6325
	public static List<BlockTileDecorInfo> BlockTileDecorInfos;

	// Token: 0x040018B6 RID: 6326
	public List<BlockTileDecorInfo> BlockTileDecorInfoAssets;

	// Token: 0x040018B7 RID: 6327
	public Material AnimMaterialAsset;

	// Token: 0x040018B8 RID: 6328
	public static Material AnimMaterial;

	// Token: 0x040018B9 RID: 6329
	public DiseaseVisualization DiseaseVisualization;

	// Token: 0x040018BA RID: 6330
	public Sprite LegendColourBox;

	// Token: 0x040018BB RID: 6331
	public Texture2D invalidAreaTex;

	// Token: 0x040018BC RID: 6332
	public Assets.UIPrefabData UIPrefabAssets;

	// Token: 0x040018BD RID: 6333
	public static Assets.UIPrefabData UIPrefabs;

	// Token: 0x040018BE RID: 6334
	private static Dictionary<Tag, KPrefabID> PrefabsByTag = new Dictionary<Tag, KPrefabID>();

	// Token: 0x040018BF RID: 6335
	private static Dictionary<Tag, List<KPrefabID>> PrefabsByAdditionalTags = new Dictionary<Tag, List<KPrefabID>>();

	// Token: 0x040018C0 RID: 6336
	public List<KAnimFile> AnimAssets;

	// Token: 0x040018C1 RID: 6337
	public static List<KAnimFile> Anims;

	// Token: 0x040018C2 RID: 6338
	private static Dictionary<HashedString, KAnimFile> AnimTable = new Dictionary<HashedString, KAnimFile>();

	// Token: 0x040018C3 RID: 6339
	public Font DebugFontAsset;

	// Token: 0x040018C4 RID: 6340
	public static Font DebugFont;

	// Token: 0x040018C5 RID: 6341
	public SubstanceTable substanceTable;

	// Token: 0x040018C6 RID: 6342
	[SerializeField]
	public TextAsset elementAudio;

	// Token: 0x040018C7 RID: 6343
	[SerializeField]
	public TextAsset personalitiesFile;

	// Token: 0x040018C8 RID: 6344
	public LogicModeUI logicModeUIData;

	// Token: 0x040018C9 RID: 6345
	public CommonPlacerConfig.CommonPlacerAssets commonPlacerAssets;

	// Token: 0x040018CA RID: 6346
	public DigPlacerConfig.DigPlacerAssets digPlacerAssets;

	// Token: 0x040018CB RID: 6347
	public MopPlacerConfig.MopPlacerAssets mopPlacerAssets;

	// Token: 0x040018CC RID: 6348
	public MovePickupablePlacerConfig.MovePickupablePlacerAssets movePickupToPlacerAssets;

	// Token: 0x040018CD RID: 6349
	public ComicData[] comics;

	// Token: 0x040018CE RID: 6350
	public static Assets instance;

	// Token: 0x040018CF RID: 6351
	private static Dictionary<string, string> simpleSoundEventNames = new Dictionary<string, string>();

	// Token: 0x02001525 RID: 5413
	[Serializable]
	public struct UIPrefabData
	{
		// Token: 0x04006ED6 RID: 28374
		public ProgressBar ProgressBar;

		// Token: 0x04006ED7 RID: 28375
		public HealthBar HealthBar;

		// Token: 0x04006ED8 RID: 28376
		public GameObject ResourceVisualizer;

		// Token: 0x04006ED9 RID: 28377
		public GameObject KAnimVisualizer;

		// Token: 0x04006EDA RID: 28378
		public Image RegionCellBlocked;

		// Token: 0x04006EDB RID: 28379
		public RectTransform PriorityOverlayIcon;

		// Token: 0x04006EDC RID: 28380
		public RectTransform HarvestWhenReadyOverlayIcon;

		// Token: 0x04006EDD RID: 28381
		public Assets.TableScreenAssets TableScreenWidgets;
	}

	// Token: 0x02001526 RID: 5414
	[Serializable]
	public struct TableScreenAssets
	{
		// Token: 0x04006EDE RID: 28382
		public Material DefaultUIMaterial;

		// Token: 0x04006EDF RID: 28383
		public Material DesaturatedUIMaterial;

		// Token: 0x04006EE0 RID: 28384
		public GameObject MinionPortrait;

		// Token: 0x04006EE1 RID: 28385
		public GameObject GenericPortrait;

		// Token: 0x04006EE2 RID: 28386
		public GameObject TogglePortrait;

		// Token: 0x04006EE3 RID: 28387
		public GameObject ButtonLabel;

		// Token: 0x04006EE4 RID: 28388
		public GameObject ButtonLabelWhite;

		// Token: 0x04006EE5 RID: 28389
		public GameObject Label;

		// Token: 0x04006EE6 RID: 28390
		public GameObject LabelHeader;

		// Token: 0x04006EE7 RID: 28391
		public GameObject Checkbox;

		// Token: 0x04006EE8 RID: 28392
		public GameObject BlankCell;

		// Token: 0x04006EE9 RID: 28393
		public GameObject SuperCheckbox_Horizontal;

		// Token: 0x04006EEA RID: 28394
		public GameObject SuperCheckbox_Vertical;

		// Token: 0x04006EEB RID: 28395
		public GameObject Spacer;

		// Token: 0x04006EEC RID: 28396
		public GameObject NumericDropDown;

		// Token: 0x04006EED RID: 28397
		public GameObject DropDownHeader;

		// Token: 0x04006EEE RID: 28398
		public GameObject PriorityGroupSelector;

		// Token: 0x04006EEF RID: 28399
		public GameObject PriorityGroupSelectorHeader;

		// Token: 0x04006EF0 RID: 28400
		public GameObject PrioritizeRowWidget;

		// Token: 0x04006EF1 RID: 28401
		public GameObject PrioritizeRowHeaderWidget;
	}
}
