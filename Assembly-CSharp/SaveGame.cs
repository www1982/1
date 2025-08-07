using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Klei.CustomSettings;
using KSerialization;
using Newtonsoft.Json;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x02000AEB RID: 2795
[SerializationConfig(global::KSerialization.MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SaveGame")]
public class SaveGame : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x170005BB RID: 1467
	// (get) Token: 0x0600517B RID: 20859 RVA: 0x001D9DBF File Offset: 0x001D7FBF
	// (set) Token: 0x0600517C RID: 20860 RVA: 0x001D9DC7 File Offset: 0x001D7FC7
	public int AutoSaveCycleInterval
	{
		get
		{
			return this.autoSaveCycleInterval;
		}
		set
		{
			this.autoSaveCycleInterval = value;
		}
	}

	// Token: 0x170005BC RID: 1468
	// (get) Token: 0x0600517D RID: 20861 RVA: 0x001D9DD0 File Offset: 0x001D7FD0
	// (set) Token: 0x0600517E RID: 20862 RVA: 0x001D9DD8 File Offset: 0x001D7FD8
	public Vector2I TimelapseResolution
	{
		get
		{
			return this.timelapseResolution;
		}
		set
		{
			this.timelapseResolution = value;
		}
	}

	// Token: 0x170005BD RID: 1469
	// (get) Token: 0x0600517F RID: 20863 RVA: 0x001D9DE1 File Offset: 0x001D7FE1
	public string BaseName
	{
		get
		{
			return this.baseName;
		}
	}

	// Token: 0x06005180 RID: 20864 RVA: 0x001D9DE9 File Offset: 0x001D7FE9
	public static void DestroyInstance()
	{
		SaveGame.Instance = null;
	}

	// Token: 0x170005BE RID: 1470
	// (get) Token: 0x06005181 RID: 20865 RVA: 0x001D9DF1 File Offset: 0x001D7FF1
	public ColonyAchievementTracker ColonyAchievementTracker
	{
		get
		{
			if (this.colonyAchievementTracker == null)
			{
				this.colonyAchievementTracker = base.GetComponent<ColonyAchievementTracker>();
			}
			return this.colonyAchievementTracker;
		}
	}

	// Token: 0x06005182 RID: 20866 RVA: 0x001D9E14 File Offset: 0x001D8014
	protected override void OnPrefabInit()
	{
		SaveGame.Instance = this;
		new ColonyRationMonitor.Instance(this).StartSM();
		this.entombedItemManager = base.gameObject.AddComponent<EntombedItemManager>();
		this.worldGenSpawner = base.gameObject.AddComponent<WorldGenSpawner>();
		base.gameObject.AddOrGetDef<GameplaySeasonManager.Def>();
		base.gameObject.AddOrGetDef<ClusterFogOfWarManager.Def>();
	}

	// Token: 0x06005183 RID: 20867 RVA: 0x001D9E6C File Offset: 0x001D806C
	[OnSerializing]
	private void OnSerialize()
	{
		this.speed = SpeedControlScreen.Instance.GetSpeed();
	}

	// Token: 0x06005184 RID: 20868 RVA: 0x001D9E7E File Offset: 0x001D807E
	[OnDeserializing]
	private void OnDeserialize()
	{
		this.baseName = SaveLoader.Instance.GameInfo.baseName;
	}

	// Token: 0x06005185 RID: 20869 RVA: 0x001D9E95 File Offset: 0x001D8095
	public int GetSpeed()
	{
		return this.speed;
	}

	// Token: 0x06005186 RID: 20870 RVA: 0x001D9EA0 File Offset: 0x001D80A0
	public byte[] GetSaveHeader(bool isAutoSave, bool isCompressed, out SaveGame.Header header)
	{
		string originalSaveFileName = SaveLoader.GetOriginalSaveFileName(SaveLoader.GetActiveSaveFilePath());
		string text = JsonConvert.SerializeObject(new SaveGame.GameInfo(GameClock.Instance.GetCycle(), Components.LiveMinionIdentities.Count, this.baseName, isAutoSave, originalSaveFileName, SaveLoader.Instance.GameInfo.clusterId, SaveLoader.Instance.GameInfo.worldTraits, SaveLoader.Instance.GameInfo.colonyGuid, SaveLoader.Instance.GameInfo.dlcIds, this.sandboxEnabled));
		byte[] bytes = Encoding.UTF8.GetBytes(text);
		header = default(SaveGame.Header);
		header.buildVersion = 679336U;
		header.headerSize = bytes.Length;
		header.headerVersion = 1U;
		header.compression = (isCompressed ? 1 : 0);
		return bytes;
	}

	// Token: 0x06005187 RID: 20871 RVA: 0x001D9F64 File Offset: 0x001D8164
	public static string GetSaveUniqueID(SaveGame.GameInfo info)
	{
		if (!(info.colonyGuid != Guid.Empty))
		{
			return info.baseName + "/" + info.clusterId;
		}
		return info.colonyGuid.ToString();
	}

	// Token: 0x06005188 RID: 20872 RVA: 0x001D9FA4 File Offset: 0x001D81A4
	public static global::Tuple<SaveGame.Header, SaveGame.GameInfo> GetFileInfo(string filename)
	{
		try
		{
			SaveGame.Header header;
			SaveGame.GameInfo gameInfo = SaveLoader.LoadHeader(filename, out header);
			if (gameInfo.saveMajorVersion >= 7)
			{
				return new global::Tuple<SaveGame.Header, SaveGame.GameInfo>(header, gameInfo);
			}
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Exception while loading " + filename);
			global::Debug.LogWarning(ex);
		}
		return null;
	}

	// Token: 0x06005189 RID: 20873 RVA: 0x001D9FFC File Offset: 0x001D81FC
	public static SaveGame.GameInfo GetHeader(IReader br, out SaveGame.Header header, string debugFileName)
	{
		header = default(SaveGame.Header);
		header.buildVersion = br.ReadUInt32();
		header.headerSize = br.ReadInt32();
		header.headerVersion = br.ReadUInt32();
		if (1U <= header.headerVersion)
		{
			header.compression = br.ReadInt32();
		}
		byte[] array = br.ReadBytes(header.headerSize);
		if (header.headerSize == 0 && !SaveGame.debug_SaveFileHeaderBlank_sent)
		{
			SaveGame.debug_SaveFileHeaderBlank_sent = true;
			global::Debug.LogWarning("SaveFileHeaderBlank - " + debugFileName);
		}
		SaveGame.GameInfo gameInfo = SaveGame.GetGameInfo(array);
		if (gameInfo.IsVersionOlderThan(7, 14) && gameInfo.worldTraits != null)
		{
			string[] worldTraits = gameInfo.worldTraits;
			for (int i = 0; i < worldTraits.Length; i++)
			{
				worldTraits[i] = worldTraits[i].Replace('\\', '/');
			}
		}
		if (gameInfo.IsVersionOlderThan(7, 20))
		{
			gameInfo.dlcId = "";
		}
		if (gameInfo.IsVersionOlderThan(7, 34))
		{
			gameInfo.dlcIds = new List<string> { gameInfo.dlcId };
		}
		return gameInfo;
	}

	// Token: 0x0600518A RID: 20874 RVA: 0x001DA0F5 File Offset: 0x001D82F5
	public static SaveGame.GameInfo GetGameInfo(byte[] data)
	{
		return JsonConvert.DeserializeObject<SaveGame.GameInfo>(Encoding.UTF8.GetString(data));
	}

	// Token: 0x0600518B RID: 20875 RVA: 0x001DA107 File Offset: 0x001D8307
	public void SetBaseName(string newBaseName)
	{
		if (string.IsNullOrEmpty(newBaseName))
		{
			global::Debug.LogWarning("Cannot give the base an empty name");
			return;
		}
		this.baseName = newBaseName;
	}

	// Token: 0x0600518C RID: 20876 RVA: 0x001DA123 File Offset: 0x001D8323
	protected override void OnSpawn()
	{
		ThreadedHttps<KleiMetrics>.Instance.SendProfileStats();
		Game.Instance.Trigger(-1917495436, null);
	}

	// Token: 0x0600518D RID: 20877 RVA: 0x001DA140 File Offset: 0x001D8340
	public List<global::Tuple<string, TextStyleSetting>> GetColonyToolTip()
	{
		List<global::Tuple<string, TextStyleSetting>> list = new List<global::Tuple<string, TextStyleSetting>>();
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout);
		ClusterLayout clusterLayout;
		SettingsCache.clusterLayouts.clusterCache.TryGetValue(currentQualitySetting.id, out clusterLayout);
		list.Add(new global::Tuple<string, TextStyleSetting>(this.baseName, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
		if (DlcManager.IsExpansion1Active())
		{
			StringEntry stringEntry = Strings.Get(clusterLayout.name);
			list.Add(new global::Tuple<string, TextStyleSetting>(stringEntry, ToolTipScreen.Instance.defaultTooltipBodyStyle));
		}
		if (GameClock.Instance != null)
		{
			list.Add(new global::Tuple<string, TextStyleSetting>(" ", null));
			list.Add(new global::Tuple<string, TextStyleSetting>(string.Format(UI.ASTEROIDCLOCK.CYCLES_OLD, GameUtil.GetCurrentCycle()), ToolTipScreen.Instance.defaultTooltipHeaderStyle));
			list.Add(new global::Tuple<string, TextStyleSetting>(string.Format(UI.ASTEROIDCLOCK.TIME_PLAYED, (GameClock.Instance.GetTimePlayedInSeconds() / 3600f).ToString("0.00")), ToolTipScreen.Instance.defaultTooltipBodyStyle));
		}
		int cameraActiveCluster = CameraController.Instance.cameraActiveCluster;
		WorldContainer world = ClusterManager.Instance.GetWorld(cameraActiveCluster);
		list.Add(new global::Tuple<string, TextStyleSetting>(" ", null));
		if (DlcManager.IsExpansion1Active())
		{
			list.Add(new global::Tuple<string, TextStyleSetting>(world.GetComponent<ClusterGridEntity>().Name, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
		}
		else
		{
			StringEntry stringEntry2 = Strings.Get(clusterLayout.name);
			list.Add(new global::Tuple<string, TextStyleSetting>(stringEntry2, ToolTipScreen.Instance.defaultTooltipHeaderStyle));
		}
		if (SaveLoader.Instance.GameInfo.worldTraits != null && SaveLoader.Instance.GameInfo.worldTraits.Length != 0)
		{
			string[] worldTraits = SaveLoader.Instance.GameInfo.worldTraits;
			for (int i = 0; i < worldTraits.Length; i++)
			{
				WorldTrait cachedWorldTrait = SettingsCache.GetCachedWorldTrait(worldTraits[i], false);
				if (cachedWorldTrait != null)
				{
					list.Add(new global::Tuple<string, TextStyleSetting>(Strings.Get(cachedWorldTrait.name), ToolTipScreen.Instance.defaultTooltipBodyStyle));
				}
				else
				{
					list.Add(new global::Tuple<string, TextStyleSetting>(WORLD_TRAITS.MISSING_TRAIT, ToolTipScreen.Instance.defaultTooltipBodyStyle));
				}
			}
		}
		else if (world.WorldTraitIds != null)
		{
			foreach (string text in world.WorldTraitIds)
			{
				WorldTrait cachedWorldTrait2 = SettingsCache.GetCachedWorldTrait(text, false);
				if (cachedWorldTrait2 != null)
				{
					list.Add(new global::Tuple<string, TextStyleSetting>(Strings.Get(cachedWorldTrait2.name), ToolTipScreen.Instance.defaultTooltipBodyStyle));
				}
				else
				{
					list.Add(new global::Tuple<string, TextStyleSetting>(WORLD_TRAITS.MISSING_TRAIT, ToolTipScreen.Instance.defaultTooltipBodyStyle));
				}
			}
			if (world.WorldTraitIds.Count == 0)
			{
				list.Add(new global::Tuple<string, TextStyleSetting>(WORLD_TRAITS.NO_TRAITS.NAME_SHORTHAND, ToolTipScreen.Instance.defaultTooltipBodyStyle));
			}
		}
		return list;
	}

	// Token: 0x040036E8 RID: 14056
	[Serialize]
	private int speed;

	// Token: 0x040036E9 RID: 14057
	[Serialize]
	public List<Tag> expandedResourceTags = new List<Tag>();

	// Token: 0x040036EA RID: 14058
	[Serialize]
	public int minGermCountForDisinfect = 10000;

	// Token: 0x040036EB RID: 14059
	[Serialize]
	public bool enableAutoDisinfect = true;

	// Token: 0x040036EC RID: 14060
	[Serialize]
	public bool sandboxEnabled;

	// Token: 0x040036ED RID: 14061
	[Serialize]
	public float relativeTemperatureOverlaySliderValue = 294.15f;

	// Token: 0x040036EE RID: 14062
	[Serialize]
	private int autoSaveCycleInterval = 1;

	// Token: 0x040036EF RID: 14063
	[Serialize]
	private Vector2I timelapseResolution = new Vector2I(512, 768);

	// Token: 0x040036F0 RID: 14064
	private string baseName;

	// Token: 0x040036F1 RID: 14065
	public static SaveGame Instance;

	// Token: 0x040036F2 RID: 14066
	private ColonyAchievementTracker colonyAchievementTracker;

	// Token: 0x040036F3 RID: 14067
	public EntombedItemManager entombedItemManager;

	// Token: 0x040036F4 RID: 14068
	public WorldGenSpawner worldGenSpawner;

	// Token: 0x040036F5 RID: 14069
	[MyCmpReq]
	public MaterialSelectorSerializer materialSelectorSerializer;

	// Token: 0x040036F6 RID: 14070
	private static bool debug_SaveFileHeaderBlank_sent;

	// Token: 0x02001BD8 RID: 7128
	public struct Header
	{
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x0600A8EF RID: 43247 RVA: 0x003B61E4 File Offset: 0x003B43E4
		public bool IsCompressed
		{
			get
			{
				return this.compression != 0;
			}
		}

		// Token: 0x0400843C RID: 33852
		public uint buildVersion;

		// Token: 0x0400843D RID: 33853
		public int headerSize;

		// Token: 0x0400843E RID: 33854
		public uint headerVersion;

		// Token: 0x0400843F RID: 33855
		public int compression;
	}

	// Token: 0x02001BD9 RID: 7129
	public struct GameInfo
	{
		// Token: 0x0600A8F0 RID: 43248 RVA: 0x003B61F0 File Offset: 0x003B43F0
		public GameInfo(int numberOfCycles, int numberOfDuplicants, string baseName, bool isAutoSave, string originalSaveName, string clusterId, string[] worldTraits, Guid colonyGuid, List<string> dlcIds, bool sandboxEnabled = false)
		{
			this.numberOfCycles = numberOfCycles;
			this.numberOfDuplicants = numberOfDuplicants;
			this.baseName = baseName;
			this.isAutoSave = isAutoSave;
			this.originalSaveName = originalSaveName;
			this.clusterId = clusterId;
			this.worldTraits = worldTraits;
			this.colonyGuid = colonyGuid;
			this.sandboxEnabled = sandboxEnabled;
			this.dlcIds = dlcIds;
			this.dlcId = null;
			this.saveMajorVersion = 7;
			this.saveMinorVersion = 36;
		}

		// Token: 0x0600A8F1 RID: 43249 RVA: 0x003B6260 File Offset: 0x003B4460
		public bool IsVersionOlderThan(int major, int minor)
		{
			return this.saveMajorVersion < major || (this.saveMajorVersion == major && this.saveMinorVersion < minor);
		}

		// Token: 0x0600A8F2 RID: 43250 RVA: 0x003B6281 File Offset: 0x003B4481
		public bool IsVersionExactly(int major, int minor)
		{
			return this.saveMajorVersion == major && this.saveMinorVersion == minor;
		}

		// Token: 0x0600A8F3 RID: 43251 RVA: 0x003B6298 File Offset: 0x003B4498
		public bool IsCompatableWithCurrentDlcConfiguration(out HashSet<string> dlcIdsToEnable, out HashSet<string> dlcIdToDisable)
		{
			dlcIdsToEnable = new HashSet<string>();
			foreach (string text in this.dlcIds)
			{
				if (!DlcManager.IsContentSubscribed(text))
				{
					dlcIdsToEnable.Add(text);
				}
			}
			dlcIdToDisable = new HashSet<string>();
			if (!this.dlcIds.Contains("EXPANSION1_ID") && DlcManager.IsExpansion1Active())
			{
				dlcIdToDisable.Add("EXPANSION1_ID");
			}
			return dlcIdsToEnable.Count == 0 && dlcIdToDisable.Count == 0;
		}

		// Token: 0x04008440 RID: 33856
		public int numberOfCycles;

		// Token: 0x04008441 RID: 33857
		public int numberOfDuplicants;

		// Token: 0x04008442 RID: 33858
		public string baseName;

		// Token: 0x04008443 RID: 33859
		public bool isAutoSave;

		// Token: 0x04008444 RID: 33860
		public string originalSaveName;

		// Token: 0x04008445 RID: 33861
		public int saveMajorVersion;

		// Token: 0x04008446 RID: 33862
		public int saveMinorVersion;

		// Token: 0x04008447 RID: 33863
		public string clusterId;

		// Token: 0x04008448 RID: 33864
		public string[] worldTraits;

		// Token: 0x04008449 RID: 33865
		public bool sandboxEnabled;

		// Token: 0x0400844A RID: 33866
		public Guid colonyGuid;

		// Token: 0x0400844B RID: 33867
		[Obsolete("Please use dlcIds instead.")]
		public string dlcId;

		// Token: 0x0400844C RID: 33868
		public List<string> dlcIds;
	}
}
