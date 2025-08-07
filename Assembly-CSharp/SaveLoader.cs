using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zlib;
using Klei;
using Klei.AI;
using Klei.CustomSettings;
using KMod;
using KSerialization;
using Newtonsoft.Json;
using ProcGen;
using ProcGenGame;
using STRINGS;
using UnityEngine;

// Token: 0x02000AEC RID: 2796
[AddComponentMenu("KMonoBehaviour/scripts/SaveLoader")]
public class SaveLoader : KMonoBehaviour
{
	// Token: 0x170005BF RID: 1471
	// (get) Token: 0x0600518F RID: 20879 RVA: 0x001DA4A3 File Offset: 0x001D86A3
	// (set) Token: 0x06005190 RID: 20880 RVA: 0x001DA4AB File Offset: 0x001D86AB
	public bool loadedFromSave { get; private set; }

	// Token: 0x06005191 RID: 20881 RVA: 0x001DA4B4 File Offset: 0x001D86B4
	public static void DestroyInstance()
	{
		SaveLoader.Instance = null;
	}

	// Token: 0x170005C0 RID: 1472
	// (get) Token: 0x06005192 RID: 20882 RVA: 0x001DA4BC File Offset: 0x001D86BC
	// (set) Token: 0x06005193 RID: 20883 RVA: 0x001DA4C3 File Offset: 0x001D86C3
	public static SaveLoader Instance { get; private set; }

	// Token: 0x170005C1 RID: 1473
	// (get) Token: 0x06005194 RID: 20884 RVA: 0x001DA4CB File Offset: 0x001D86CB
	// (set) Token: 0x06005195 RID: 20885 RVA: 0x001DA4D3 File Offset: 0x001D86D3
	public Action<Cluster> OnWorldGenComplete { get; set; }

	// Token: 0x170005C2 RID: 1474
	// (get) Token: 0x06005196 RID: 20886 RVA: 0x001DA4DC File Offset: 0x001D86DC
	public Cluster Cluster
	{
		get
		{
			return this.m_cluster;
		}
	}

	// Token: 0x170005C3 RID: 1475
	// (get) Token: 0x06005197 RID: 20887 RVA: 0x001DA4E4 File Offset: 0x001D86E4
	public ClusterLayout ClusterLayout
	{
		get
		{
			if (this.m_clusterLayout == null)
			{
				this.m_clusterLayout = CustomGameSettings.Instance.GetCurrentClusterLayout();
			}
			return this.m_clusterLayout;
		}
	}

	// Token: 0x170005C4 RID: 1476
	// (get) Token: 0x06005198 RID: 20888 RVA: 0x001DA504 File Offset: 0x001D8704
	// (set) Token: 0x06005199 RID: 20889 RVA: 0x001DA50C File Offset: 0x001D870C
	public SaveGame.GameInfo GameInfo { get; private set; }

	// Token: 0x0600519A RID: 20890 RVA: 0x001DA515 File Offset: 0x001D8715
	protected override void OnPrefabInit()
	{
		SaveLoader.Instance = this;
		this.saveManager = base.GetComponent<SaveManager>();
	}

	// Token: 0x0600519B RID: 20891 RVA: 0x001DA529 File Offset: 0x001D8729
	private void MoveCorruptFile(string filename)
	{
	}

	// Token: 0x0600519C RID: 20892 RVA: 0x001DA52C File Offset: 0x001D872C
	protected override void OnSpawn()
	{
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		if (WorldGen.CanLoad(activeSaveFilePath))
		{
			Sim.SIM_Initialize(new Sim.GAME_MessageHandler(Sim.DLL_MessageHandler));
			SimMessages.CreateSimElementsTable(ElementLoader.elements);
			SimMessages.CreateDiseaseTable(Db.Get().Diseases);
			this.loadedFromSave = true;
			this.loadedFromSave = this.Load(activeSaveFilePath);
			this.saveFileCorrupt = !this.loadedFromSave;
			if (!this.loadedFromSave)
			{
				SaveLoader.SetActiveSaveFilePath(null);
				if (this.mustRestartOnFail)
				{
					this.MoveCorruptFile(activeSaveFilePath);
					Sim.Shutdown();
					App.LoadScene("frontend");
					return;
				}
			}
		}
		if (!this.loadedFromSave)
		{
			Sim.Shutdown();
			if (!string.IsNullOrEmpty(activeSaveFilePath))
			{
				DebugUtil.LogArgs(new object[] { "Couldn't load [" + activeSaveFilePath + "]" });
			}
			if (this.saveFileCorrupt)
			{
				this.MoveCorruptFile(activeSaveFilePath);
			}
			if (!this.LoadFromWorldGen())
			{
				DebugUtil.LogWarningArgs(new object[] { "Couldn't start new game with current world gen, moving file" });
				KMonoBehaviour.isLoadingScene = true;
				this.MoveCorruptFile(WorldGen.WORLDGEN_SAVE_FILENAME);
				App.LoadScene("frontend");
			}
		}
	}

	// Token: 0x0600519D RID: 20893 RVA: 0x001DA63C File Offset: 0x001D883C
	private static void CompressContents(BinaryWriter fileWriter, byte[] uncompressed, int length)
	{
		using (ZlibStream zlibStream = new ZlibStream(fileWriter.BaseStream, CompressionMode.Compress, Ionic.Zlib.CompressionLevel.BestSpeed))
		{
			zlibStream.Write(uncompressed, 0, length);
			zlibStream.Flush();
		}
	}

	// Token: 0x0600519E RID: 20894 RVA: 0x001DA684 File Offset: 0x001D8884
	private byte[] FloatToBytes(float[] floats)
	{
		byte[] array = new byte[floats.Length * 4];
		Buffer.BlockCopy(floats, 0, array, 0, array.Length);
		return array;
	}

	// Token: 0x0600519F RID: 20895 RVA: 0x001DA6A9 File Offset: 0x001D88A9
	private static byte[] DecompressContents(byte[] compressed)
	{
		return ZlibStream.UncompressBuffer(compressed);
	}

	// Token: 0x060051A0 RID: 20896 RVA: 0x001DA6B4 File Offset: 0x001D88B4
	private float[] BytesToFloat(byte[] bytes)
	{
		float[] array = new float[bytes.Length / 4];
		Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
		return array;
	}

	// Token: 0x060051A1 RID: 20897 RVA: 0x001DA6DC File Offset: 0x001D88DC
	private SaveFileRoot PrepSaveFile()
	{
		SaveFileRoot saveFileRoot = new SaveFileRoot();
		saveFileRoot.WidthInCells = Grid.WidthInCells;
		saveFileRoot.HeightInCells = Grid.HeightInCells;
		saveFileRoot.streamed["GridVisible"] = Grid.Visible;
		saveFileRoot.streamed["GridSpawnable"] = Grid.Spawnable;
		saveFileRoot.streamed["GridDamage"] = this.FloatToBytes(Grid.Damage);
		Global.Instance.modManager.SendMetricsEvent();
		saveFileRoot.active_mods = new List<Label>();
		foreach (Mod mod in Global.Instance.modManager.mods)
		{
			if (mod.IsEnabledForActiveDlc())
			{
				saveFileRoot.active_mods.Add(mod.label);
			}
		}
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				Camera.main.transform.parent.GetComponent<CameraController>().Save(binaryWriter);
			}
			saveFileRoot.streamed["Camera"] = memoryStream.ToArray();
		}
		return saveFileRoot;
	}

	// Token: 0x060051A2 RID: 20898 RVA: 0x001DA838 File Offset: 0x001D8A38
	private void Save(BinaryWriter writer)
	{
		writer.WriteKleiString("world");
		Serializer.Serialize(this.PrepSaveFile(), writer);
		Game.SaveSettings(writer);
		Sim.Save(writer, 0, 0);
		this.saveManager.Save(writer);
		Game.Instance.Save(writer);
	}

	// Token: 0x060051A3 RID: 20899 RVA: 0x001DA878 File Offset: 0x001D8A78
	private bool Load(IReader reader)
	{
		global::Debug.Assert(reader.ReadKleiString() == "world");
		Deserializer deserializer = new Deserializer(reader);
		SaveFileRoot saveFileRoot = new SaveFileRoot();
		deserializer.Deserialize(saveFileRoot);
		if ((this.GameInfo.saveMajorVersion == 7 || this.GameInfo.saveMinorVersion < 8) && saveFileRoot.requiredMods != null)
		{
			saveFileRoot.active_mods = new List<Label>();
			foreach (ModInfo modInfo in saveFileRoot.requiredMods)
			{
				saveFileRoot.active_mods.Add(new Label
				{
					id = modInfo.assetID,
					version = (long)modInfo.lastModifiedTime,
					distribution_platform = Label.DistributionPlatform.Steam,
					title = modInfo.description
				});
			}
			saveFileRoot.requiredMods.Clear();
		}
		global::KMod.Manager modManager = Global.Instance.modManager;
		modManager.Load(Content.LayerableFiles);
		if (!modManager.MatchFootprint(saveFileRoot.active_mods, Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation))
		{
			DebugUtil.LogWarningArgs(new object[] { "Mod footprint of save file doesn't match current mod configuration" });
		}
		string text = string.Format("Mod Footprint ({0}):", saveFileRoot.active_mods.Count);
		foreach (Label label in saveFileRoot.active_mods)
		{
			text = text + "\n  - " + label.title;
		}
		global::Debug.Log(text);
		this.LogActiveMods();
		Global.Instance.modManager.SendMetricsEvent();
		WorldGen.LoadSettings(false);
		CustomGameSettings.Instance.LoadClusters();
		if (this.GameInfo.clusterId == null)
		{
			SaveGame.GameInfo gameInfo = this.GameInfo;
			if (!string.IsNullOrEmpty(saveFileRoot.clusterID))
			{
				gameInfo.clusterId = saveFileRoot.clusterID;
			}
			else
			{
				try
				{
					gameInfo.clusterId = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout).id;
				}
				catch
				{
					gameInfo.clusterId = WorldGenSettings.ClusterDefaultName;
					CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.ClusterLayout, gameInfo.clusterId);
				}
			}
			this.GameInfo = gameInfo;
		}
		Game.clusterId = this.GameInfo.clusterId;
		Game.LoadSettings(deserializer);
		GridSettings.Reset(saveFileRoot.WidthInCells, saveFileRoot.HeightInCells);
		if (Application.isPlaying)
		{
			Singleton<KBatchedAnimUpdater>.Instance.InitializeGrid();
		}
		Sim.SIM_Initialize(new Sim.GAME_MessageHandler(Sim.DLL_MessageHandler));
		SimMessages.CreateSimElementsTable(ElementLoader.elements);
		Sim.AllocateCells(saveFileRoot.WidthInCells, saveFileRoot.HeightInCells, false);
		SimMessages.CreateDiseaseTable(Db.Get().Diseases);
		Sim.HandleMessage(SimMessageHashes.ClearUnoccupiedCells, 0, null);
		IReader reader2;
		if (saveFileRoot.streamed.ContainsKey("Sim"))
		{
			reader2 = new FastReader(saveFileRoot.streamed["Sim"]);
		}
		else
		{
			reader2 = reader;
		}
		if (Sim.LoadWorld(reader2) != 0)
		{
			DebugUtil.LogWarningArgs(new object[] { "\n--- Error loading save ---\nSimDLL found bad data\n" });
			Sim.Shutdown();
			return false;
		}
		Sim.Start();
		SceneInitializer.Instance.PostLoadPrefabs();
		this.mustRestartOnFail = true;
		if (!this.saveManager.Load(reader))
		{
			Sim.Shutdown();
			DebugUtil.LogWarningArgs(new object[] { "\n--- Error loading save ---\n" });
			SaveLoader.SetActiveSaveFilePath(null);
			return false;
		}
		Grid.Visible = saveFileRoot.streamed["GridVisible"];
		if (saveFileRoot.streamed.ContainsKey("GridSpawnable"))
		{
			Grid.Spawnable = saveFileRoot.streamed["GridSpawnable"];
		}
		Grid.Damage = this.BytesToFloat(saveFileRoot.streamed["GridDamage"]);
		Game.Instance.Load(deserializer);
		CameraSaveData.Load(new FastReader(saveFileRoot.streamed["Camera"]));
		ClusterManager.Instance.InitializeWorldGrid();
		SimMessages.DefineWorldOffsets(ClusterManager.Instance.WorldContainers.Select((WorldContainer container) => new SimMessages.WorldOffsetData
		{
			worldOffsetX = container.WorldOffset.x,
			worldOffsetY = container.WorldOffset.y,
			worldSizeX = container.WorldSize.x,
			worldSizeY = container.WorldSize.y
		}).ToList<SimMessages.WorldOffsetData>());
		return true;
	}

	// Token: 0x060051A4 RID: 20900 RVA: 0x001DAC9C File Offset: 0x001D8E9C
	private void LogActiveMods()
	{
		string text = string.Format("Active Mods ({0}):", Global.Instance.modManager.mods.Count((Mod x) => x.IsEnabledForActiveDlc()));
		foreach (Mod mod in Global.Instance.modManager.mods)
		{
			if (mod.IsEnabledForActiveDlc())
			{
				text = text + "\n  - " + mod.title;
			}
		}
		global::Debug.Log(text);
	}

	// Token: 0x060051A5 RID: 20901 RVA: 0x001DAD54 File Offset: 0x001D8F54
	public static string GetSavePrefix()
	{
		return global::System.IO.Path.Combine(global::Util.RootFolder(), string.Format("{0}{1}", "save_files", global::System.IO.Path.DirectorySeparatorChar));
	}

	// Token: 0x060051A6 RID: 20902 RVA: 0x001DAD7C File Offset: 0x001D8F7C
	public static string GetCloudSavePrefix()
	{
		string text = global::System.IO.Path.Combine(global::Util.RootFolder(), string.Format("{0}{1}", "cloud_save_files", global::System.IO.Path.DirectorySeparatorChar));
		string userID = SaveLoader.GetUserID();
		if (string.IsNullOrEmpty(userID))
		{
			return null;
		}
		text = global::System.IO.Path.Combine(text, userID);
		if (!global::System.IO.Directory.Exists(text))
		{
			global::System.IO.Directory.CreateDirectory(text);
		}
		return text;
	}

	// Token: 0x060051A7 RID: 20903 RVA: 0x001DADD8 File Offset: 0x001D8FD8
	public static string GetSavePrefixAndCreateFolder()
	{
		string savePrefix = SaveLoader.GetSavePrefix();
		if (!global::System.IO.Directory.Exists(savePrefix))
		{
			global::System.IO.Directory.CreateDirectory(savePrefix);
		}
		return savePrefix;
	}

	// Token: 0x060051A8 RID: 20904 RVA: 0x001DADFC File Offset: 0x001D8FFC
	public static string GetUserID()
	{
		DistributionPlatform.User localUser = DistributionPlatform.Inst.LocalUser;
		if (localUser == null)
		{
			return null;
		}
		return localUser.Id.ToString();
	}

	// Token: 0x060051A9 RID: 20905 RVA: 0x001DAE24 File Offset: 0x001D9024
	public static string GetNextUsableSavePath(string filename)
	{
		int num = 0;
		string text = global::System.IO.Path.ChangeExtension(filename, null);
		while (File.Exists(filename))
		{
			filename = SaveScreen.GetValidSaveFilename(string.Format("{0} ({1})", text, num));
			num++;
		}
		return filename;
	}

	// Token: 0x060051AA RID: 20906 RVA: 0x001DAE62 File Offset: 0x001D9062
	public static string GetOriginalSaveFileName(string filename)
	{
		if (!filename.Contains("/") && !filename.Contains("\\"))
		{
			return filename;
		}
		filename.Replace('\\', '/');
		return global::System.IO.Path.GetFileName(filename);
	}

	// Token: 0x060051AB RID: 20907 RVA: 0x001DAE91 File Offset: 0x001D9091
	public static bool IsSaveAuto(string filename)
	{
		filename = filename.Replace('\\', '/');
		return filename.Contains("/auto_save/");
	}

	// Token: 0x060051AC RID: 20908 RVA: 0x001DAEAA File Offset: 0x001D90AA
	public static bool IsSaveLocal(string filename)
	{
		filename = filename.Replace('\\', '/');
		return filename.Contains("/save_files/");
	}

	// Token: 0x060051AD RID: 20909 RVA: 0x001DAEC3 File Offset: 0x001D90C3
	public static bool IsSaveCloud(string filename)
	{
		filename = filename.Replace('\\', '/');
		return filename.Contains("/cloud_save_files/");
	}

	// Token: 0x060051AE RID: 20910 RVA: 0x001DAEDC File Offset: 0x001D90DC
	public static string GetAutoSavePrefix()
	{
		string text = global::System.IO.Path.Combine(SaveLoader.GetSavePrefixAndCreateFolder(), string.Format("{0}{1}", "auto_save", global::System.IO.Path.DirectorySeparatorChar));
		if (!global::System.IO.Directory.Exists(text))
		{
			global::System.IO.Directory.CreateDirectory(text);
		}
		return text;
	}

	// Token: 0x060051AF RID: 20911 RVA: 0x001DAF1D File Offset: 0x001D911D
	public static void SetActiveSaveFilePath(string path)
	{
		KPlayerPrefs.SetString("SaveFilenameKey/", path);
	}

	// Token: 0x060051B0 RID: 20912 RVA: 0x001DAF2A File Offset: 0x001D912A
	public static string GetActiveSaveFilePath()
	{
		return KPlayerPrefs.GetString("SaveFilenameKey/");
	}

	// Token: 0x060051B1 RID: 20913 RVA: 0x001DAF38 File Offset: 0x001D9138
	public static string GetActiveAutoSavePath()
	{
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		if (activeSaveFilePath == null)
		{
			return SaveLoader.GetAutoSavePrefix();
		}
		return global::System.IO.Path.Combine(global::System.IO.Path.GetDirectoryName(activeSaveFilePath), "auto_save");
	}

	// Token: 0x060051B2 RID: 20914 RVA: 0x001DAF64 File Offset: 0x001D9164
	public static string GetAutosaveFilePath()
	{
		return SaveLoader.GetAutoSavePrefix() + "AutoSave Cycle 1.sav";
	}

	// Token: 0x060051B3 RID: 20915 RVA: 0x001DAF78 File Offset: 0x001D9178
	public static string GetActiveSaveColonyFolder()
	{
		string text = SaveLoader.GetActiveSaveFolder();
		if (text == null)
		{
			text = global::System.IO.Path.Combine(SaveLoader.GetSavePrefix(), SaveLoader.Instance.GameInfo.baseName);
		}
		return text;
	}

	// Token: 0x060051B4 RID: 20916 RVA: 0x001DAFAC File Offset: 0x001D91AC
	public static string GetActiveSaveFolder()
	{
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		if (!string.IsNullOrEmpty(activeSaveFilePath))
		{
			return global::System.IO.Path.GetDirectoryName(activeSaveFilePath);
		}
		return null;
	}

	// Token: 0x060051B5 RID: 20917 RVA: 0x001DAFD0 File Offset: 0x001D91D0
	public static List<SaveLoader.SaveFileEntry> GetSaveFiles(string save_dir, bool sort, SearchOption search = SearchOption.AllDirectories)
	{
		List<SaveLoader.SaveFileEntry> list = new List<SaveLoader.SaveFileEntry>();
		if (string.IsNullOrEmpty(save_dir))
		{
			return list;
		}
		try
		{
			if (!global::System.IO.Directory.Exists(save_dir))
			{
				global::System.IO.Directory.CreateDirectory(save_dir);
			}
			foreach (string text in global::System.IO.Directory.GetFiles(save_dir, "*.sav", search))
			{
				try
				{
					if (!text.StartsWith("._"))
					{
						global::System.DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(text);
						SaveLoader.SaveFileEntry saveFileEntry = new SaveLoader.SaveFileEntry
						{
							path = text,
							timeStamp = lastWriteTimeUtc
						};
						list.Add(saveFileEntry);
					}
				}
				catch (Exception ex)
				{
					global::Debug.LogWarning("Problem reading file: " + text + "\n" + ex.ToString());
				}
			}
			if (sort)
			{
				list.Sort((SaveLoader.SaveFileEntry x, SaveLoader.SaveFileEntry y) => y.timeStamp.CompareTo(x.timeStamp));
			}
		}
		catch (Exception ex2)
		{
			string text2 = null;
			if (ex2 is UnauthorizedAccessException)
			{
				text2 = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_READ_ONLY, save_dir);
			}
			else if (ex2 is IOException)
			{
				text2 = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_INSUFFICIENT_SPACE, save_dir);
			}
			if (text2 == null)
			{
				throw ex2;
			}
			GameObject gameObject = ((FrontEndManager.Instance == null) ? GameScreenManager.Instance.ssOverlayCanvas : FrontEndManager.Instance.gameObject);
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, gameObject, true).GetComponent<ConfirmDialogScreen>().PopupConfirmDialog(text2, null, null, null, null, null, null, null, null);
		}
		return list;
	}

	// Token: 0x060051B6 RID: 20918 RVA: 0x001DB15C File Offset: 0x001D935C
	public static List<SaveLoader.SaveFileEntry> GetAllFiles(bool sort, SaveLoader.SaveType type = SaveLoader.SaveType.both)
	{
		switch (type)
		{
		case SaveLoader.SaveType.local:
			return SaveLoader.GetSaveFiles(SaveLoader.GetSavePrefixAndCreateFolder(), sort, SearchOption.AllDirectories);
		case SaveLoader.SaveType.cloud:
			return SaveLoader.GetSaveFiles(SaveLoader.GetCloudSavePrefix(), sort, SearchOption.AllDirectories);
		case SaveLoader.SaveType.both:
		{
			List<SaveLoader.SaveFileEntry> saveFiles = SaveLoader.GetSaveFiles(SaveLoader.GetSavePrefixAndCreateFolder(), false, SearchOption.AllDirectories);
			List<SaveLoader.SaveFileEntry> saveFiles2 = SaveLoader.GetSaveFiles(SaveLoader.GetCloudSavePrefix(), false, SearchOption.AllDirectories);
			saveFiles.AddRange(saveFiles2);
			if (sort)
			{
				saveFiles.Sort((SaveLoader.SaveFileEntry x, SaveLoader.SaveFileEntry y) => y.timeStamp.CompareTo(x.timeStamp));
			}
			return saveFiles;
		}
		default:
			return new List<SaveLoader.SaveFileEntry>();
		}
	}

	// Token: 0x060051B7 RID: 20919 RVA: 0x001DB1E7 File Offset: 0x001D93E7
	public static List<SaveLoader.SaveFileEntry> GetAllColonyFiles(bool sort, SearchOption search = SearchOption.TopDirectoryOnly)
	{
		return SaveLoader.GetSaveFiles(SaveLoader.GetActiveSaveColonyFolder(), sort, search);
	}

	// Token: 0x060051B8 RID: 20920 RVA: 0x001DB1F5 File Offset: 0x001D93F5
	public static bool GetCloudSavesDefault()
	{
		return !(SaveLoader.GetCloudSavesDefaultPref() == "Disabled");
	}

	// Token: 0x060051B9 RID: 20921 RVA: 0x001DB20C File Offset: 0x001D940C
	public static string GetCloudSavesDefaultPref()
	{
		string text = KPlayerPrefs.GetString("SavesDefaultToCloud", "Enabled");
		if (text != "Enabled" && text != "Disabled")
		{
			text = "Enabled";
		}
		return text;
	}

	// Token: 0x060051BA RID: 20922 RVA: 0x001DB24A File Offset: 0x001D944A
	public static void SetCloudSavesDefault(bool value)
	{
		SaveLoader.SetCloudSavesDefaultPref(value ? "Enabled" : "Disabled");
	}

	// Token: 0x060051BB RID: 20923 RVA: 0x001DB260 File Offset: 0x001D9460
	public static void SetCloudSavesDefaultPref(string pref)
	{
		if (pref != "Enabled" && pref != "Disabled")
		{
			global::Debug.LogWarning("Ignoring cloud saves default pref `" + pref + "` as it's not valid, expected `Enabled` or `Disabled`");
			return;
		}
		KPlayerPrefs.SetString("SavesDefaultToCloud", pref);
	}

	// Token: 0x060051BC RID: 20924 RVA: 0x001DB29D File Offset: 0x001D949D
	public static bool GetCloudSavesAvailable()
	{
		return !string.IsNullOrEmpty(SaveLoader.GetUserID()) && SaveLoader.GetCloudSavePrefix() != null;
	}

	// Token: 0x060051BD RID: 20925 RVA: 0x001DB2B8 File Offset: 0x001D94B8
	public static string GetLatestSaveForCurrentDLC()
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(true, SaveLoader.SaveType.both);
		for (int i = 0; i < allFiles.Count; i++)
		{
			global::Tuple<SaveGame.Header, SaveGame.GameInfo> fileInfo = SaveGame.GetFileInfo(allFiles[i].path);
			if (fileInfo != null)
			{
				SaveGame.Header first = fileInfo.first;
				SaveGame.GameInfo second = fileInfo.second;
				HashSet<string> hashSet;
				HashSet<string> hashSet2;
				if (second.saveMajorVersion >= 7 && second.IsCompatableWithCurrentDlcConfiguration(out hashSet, out hashSet2))
				{
					return allFiles[i].path;
				}
			}
		}
		return null;
	}

	// Token: 0x060051BE RID: 20926 RVA: 0x001DB32C File Offset: 0x001D952C
	public void InitialSave()
	{
		string text = SaveLoader.GetActiveSaveFilePath();
		if (string.IsNullOrEmpty(text))
		{
			text = SaveLoader.GetAutosaveFilePath();
		}
		else if (!text.Contains(".sav"))
		{
			text += ".sav";
		}
		this.LogActiveMods();
		this.Save(text, false, true);
	}

	// Token: 0x060051BF RID: 20927 RVA: 0x001DB378 File Offset: 0x001D9578
	public string Save(string filename, bool isAutoSave = false, bool updateSavePointer = true)
	{
		global::KSerialization.Manager.Clear();
		string directoryName = global::System.IO.Path.GetDirectoryName(filename);
		try
		{
			if (directoryName != null && !global::System.IO.Directory.Exists(directoryName))
			{
				global::System.IO.Directory.CreateDirectory(directoryName);
			}
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Problem creating save folder for " + filename + "!\n" + ex.ToString());
		}
		this.ReportSaveMetrics(isAutoSave);
		RetireColonyUtility.SaveColonySummaryData();
		if (isAutoSave && !GenericGameSettings.instance.keepAllAutosaves)
		{
			List<SaveLoader.SaveFileEntry> saveFiles = SaveLoader.GetSaveFiles(SaveLoader.GetActiveAutoSavePath(), true, SearchOption.AllDirectories);
			List<string> list = new List<string>();
			foreach (SaveLoader.SaveFileEntry saveFileEntry in saveFiles)
			{
				global::Tuple<SaveGame.Header, SaveGame.GameInfo> fileInfo = SaveGame.GetFileInfo(saveFileEntry.path);
				if (fileInfo != null && SaveGame.GetSaveUniqueID(fileInfo.second) == SaveLoader.Instance.GameInfo.colonyGuid.ToString())
				{
					list.Add(saveFileEntry.path);
				}
			}
			for (int i = list.Count - 1; i >= 9; i--)
			{
				string text = list[i];
				try
				{
					global::Debug.Log("Deleting old autosave: " + text);
					File.Delete(text);
				}
				catch (Exception ex2)
				{
					global::Debug.LogWarning("Problem deleting autosave: " + text + "\n" + ex2.ToString());
				}
				string text2 = global::System.IO.Path.ChangeExtension(text, ".png");
				try
				{
					if (File.Exists(text2))
					{
						File.Delete(text2);
					}
				}
				catch (Exception ex3)
				{
					global::Debug.LogWarning("Problem deleting autosave screenshot: " + text2 + "\n" + ex3.ToString());
				}
			}
		}
		using (MemoryStream memoryStream = new MemoryStream((int)((float)this.lastUncompressedSize * 1.1f)))
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
			{
				this.Save(binaryWriter);
				this.lastUncompressedSize = (int)memoryStream.Length;
				try
				{
					using (BinaryWriter binaryWriter2 = new BinaryWriter(File.Open(filename, FileMode.Create)))
					{
						SaveGame.Header header;
						byte[] saveHeader = SaveGame.Instance.GetSaveHeader(isAutoSave, this.compressSaveData, out header);
						binaryWriter2.Write(header.buildVersion);
						binaryWriter2.Write(header.headerSize);
						binaryWriter2.Write(header.headerVersion);
						binaryWriter2.Write(header.compression);
						binaryWriter2.Write(saveHeader);
						global::KSerialization.Manager.SerializeDirectory(binaryWriter2);
						if (this.compressSaveData)
						{
							SaveLoader.CompressContents(binaryWriter2, memoryStream.GetBuffer(), (int)memoryStream.Length);
						}
						else
						{
							binaryWriter2.Write(memoryStream.ToArray());
						}
						KCrashReporter.MOST_RECENT_SAVEFILE = filename;
						Stats.Print();
					}
				}
				catch (Exception ex4)
				{
					if (ex4 is UnauthorizedAccessException)
					{
						DebugUtil.LogArgs(new object[] { "UnauthorizedAccessException for " + filename });
						((ConfirmDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)).PopupConfirmDialog(string.Format(UI.CRASHSCREEN.SAVEFAILED, "Unauthorized Access Exception"), null, null, null, null, null, null, null, null);
						return SaveLoader.GetActiveSaveFilePath();
					}
					if (ex4 is IOException)
					{
						DebugUtil.LogArgs(new object[] { "IOException (probably out of disk space) for " + filename });
						((ConfirmDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay)).PopupConfirmDialog(string.Format(UI.CRASHSCREEN.SAVEFAILED, "IOException. You may not have enough free space!"), null, null, null, null, null, null, null, null);
						return SaveLoader.GetActiveSaveFilePath();
					}
					throw ex4;
				}
			}
		}
		if (updateSavePointer)
		{
			SaveLoader.SetActiveSaveFilePath(filename);
		}
		Game.Instance.timelapser.SaveColonyPreview(filename);
		DebugUtil.LogArgs(new object[]
		{
			"Saved to",
			"[" + filename + "]"
		});
		GC.Collect();
		return filename;
	}

	// Token: 0x060051C0 RID: 20928 RVA: 0x001DB820 File Offset: 0x001D9A20
	public static SaveGame.GameInfo LoadHeader(string filename, out SaveGame.Header header)
	{
		byte[] array = new byte[512];
		SaveGame.GameInfo header2;
		using (FileStream fileStream = File.OpenRead(filename))
		{
			fileStream.Read(array, 0, 512);
			header2 = SaveGame.GetHeader(new FastReader(array), out header, filename);
		}
		return header2;
	}

	// Token: 0x060051C1 RID: 20929 RVA: 0x001DB878 File Offset: 0x001D9A78
	public bool Load(string filename)
	{
		SaveLoader.SetActiveSaveFilePath(filename);
		try
		{
			global::KSerialization.Manager.Clear();
			byte[] array = File.ReadAllBytes(filename);
			IReader reader = new FastReader(array);
			SaveGame.Header header;
			this.GameInfo = SaveGame.GetHeader(reader, out header, filename);
			ThreadedHttps<KleiMetrics>.Instance.SetExpansionsActive(this.GameInfo.dlcIds);
			DebugUtil.LogArgs(new object[] { string.Format("Loading save file: {4}\n headerVersion:{0}, buildVersion:{1}, headerSize:{2}, IsCompressed:{3}", new object[] { header.headerVersion, header.buildVersion, header.headerSize, header.IsCompressed, filename }) });
			DebugUtil.LogArgs(new object[] { string.Format("GameInfo loaded from save header:\n  numberOfCycles:{0},\n  numberOfDuplicants:{1},\n  baseName:{2},\n  isAutoSave:{3},\n  originalSaveName:{4},\n  clusterId:{5},\n  worldTraits:{6},\n  colonyGuid:{7},\n  saveVersion:{8}.{9}", new object[]
			{
				this.GameInfo.numberOfCycles,
				this.GameInfo.numberOfDuplicants,
				this.GameInfo.baseName,
				this.GameInfo.isAutoSave,
				this.GameInfo.originalSaveName,
				this.GameInfo.clusterId,
				(this.GameInfo.worldTraits != null && this.GameInfo.worldTraits.Length != 0) ? string.Join(", ", this.GameInfo.worldTraits) : "<i>none</i>",
				this.GameInfo.colonyGuid,
				this.GameInfo.saveMajorVersion,
				this.GameInfo.saveMinorVersion
			}) });
			string originalSaveName = this.GameInfo.originalSaveName;
			if (originalSaveName.Contains("/") || originalSaveName.Contains("\\"))
			{
				string originalSaveFileName = SaveLoader.GetOriginalSaveFileName(originalSaveName);
				SaveGame.GameInfo gameInfo = this.GameInfo;
				gameInfo.originalSaveName = originalSaveFileName;
				this.GameInfo = gameInfo;
				global::Debug.Log(string.Concat(new string[]
				{
					"Migration / Save originalSaveName updated from: `",
					originalSaveName,
					"` => `",
					this.GameInfo.originalSaveName,
					"`"
				}));
			}
			if (this.GameInfo.saveMajorVersion == 7 && this.GameInfo.saveMinorVersion < 4)
			{
				Helper.SetTypeInfoMask((SerializationTypeInfo)191);
			}
			global::KSerialization.Manager.DeserializeDirectory(reader);
			if (header.IsCompressed)
			{
				int num = array.Length - reader.Position;
				byte[] array2 = new byte[num];
				Array.Copy(array, reader.Position, array2, 0, num);
				byte[] array3 = SaveLoader.DecompressContents(array2);
				this.lastUncompressedSize = array3.Length;
				IReader reader2 = new FastReader(array3);
				this.Load(reader2);
			}
			else
			{
				this.lastUncompressedSize = array.Length;
				this.Load(reader);
			}
			KCrashReporter.MOST_RECENT_SAVEFILE = filename;
			if (this.GameInfo.isAutoSave && !string.IsNullOrEmpty(this.GameInfo.originalSaveName))
			{
				string originalSaveFileName2 = SaveLoader.GetOriginalSaveFileName(this.GameInfo.originalSaveName);
				string text;
				if (SaveLoader.IsSaveCloud(filename))
				{
					string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
					if (cloudSavePrefix != null)
					{
						text = global::System.IO.Path.Combine(cloudSavePrefix, this.GameInfo.baseName, originalSaveFileName2);
					}
					else
					{
						text = global::System.IO.Path.Combine(global::System.IO.Path.GetDirectoryName(filename).Replace("auto_save", ""), this.GameInfo.baseName, originalSaveFileName2);
					}
				}
				else
				{
					text = global::System.IO.Path.Combine(SaveLoader.GetSavePrefix(), this.GameInfo.baseName, originalSaveFileName2);
				}
				if (text != null)
				{
					SaveLoader.SetActiveSaveFilePath(text);
				}
			}
		}
		catch (Exception ex)
		{
			DebugUtil.LogWarningArgs(new object[] { "\n--- Error loading save ---\n" + ex.Message + "\n" + ex.StackTrace });
			Sim.Shutdown();
			SaveLoader.SetActiveSaveFilePath(null);
			return false;
		}
		Stats.Print();
		DebugUtil.LogArgs(new object[]
		{
			"Loaded",
			"[" + filename + "]"
		});
		DebugUtil.LogArgs(new object[]
		{
			"World Seeds",
			string.Concat(new string[]
			{
				"[",
				this.clusterDetailSave.globalWorldSeed.ToString(),
				"/",
				this.clusterDetailSave.globalWorldLayoutSeed.ToString(),
				"/",
				this.clusterDetailSave.globalTerrainSeed.ToString(),
				"/",
				this.clusterDetailSave.globalNoiseSeed.ToString(),
				"]"
			})
		});
		GC.Collect();
		return true;
	}

	// Token: 0x060051C2 RID: 20930 RVA: 0x001DBD0C File Offset: 0x001D9F0C
	public bool LoadFromWorldGen()
	{
		DebugUtil.LogArgs(new object[] { "Attempting to start a new game with current world gen" });
		WorldGen.LoadSettings(false);
		FastReader fastReader = new FastReader(File.ReadAllBytes(WorldGen.WORLDGEN_SAVE_FILENAME));
		this.m_cluster = Cluster.Load(fastReader);
		ListPool<SimSaveFileStructure, SaveLoader>.PooledList pooledList = ListPool<SimSaveFileStructure, SaveLoader>.Allocate();
		this.m_cluster.LoadClusterSim(pooledList, fastReader);
		SaveGame.GameInfo gameInfo = this.GameInfo;
		gameInfo.clusterId = this.m_cluster.Id;
		gameInfo.colonyGuid = Guid.NewGuid();
		ClusterLayout currentClusterLayout = CustomGameSettings.Instance.GetCurrentClusterLayout();
		gameInfo.dlcIds = new List<string>(currentClusterLayout.requiredDlcIds);
		foreach (string text in CustomGameSettings.Instance.GetCurrentDlcMixingIds())
		{
			if (!gameInfo.dlcIds.Contains(text))
			{
				gameInfo.dlcIds.Add(text);
			}
		}
		this.GameInfo = gameInfo;
		ThreadedHttps<KleiMetrics>.Instance.SetExpansionsActive(this.GameInfo.dlcIds);
		if (pooledList.Count != this.m_cluster.worlds.Count)
		{
			global::Debug.LogError("Attempt failed. Failed to load all worlds.");
			pooledList.Recycle();
			return false;
		}
		GridSettings.Reset(this.m_cluster.size.x, this.m_cluster.size.y);
		if (Application.isPlaying)
		{
			Singleton<KBatchedAnimUpdater>.Instance.InitializeGrid();
		}
		this.clusterDetailSave = new WorldDetailSave();
		foreach (SimSaveFileStructure simSaveFileStructure in pooledList)
		{
			this.clusterDetailSave.globalNoiseSeed = simSaveFileStructure.worldDetail.globalNoiseSeed;
			this.clusterDetailSave.globalTerrainSeed = simSaveFileStructure.worldDetail.globalTerrainSeed;
			this.clusterDetailSave.globalWorldLayoutSeed = simSaveFileStructure.worldDetail.globalWorldLayoutSeed;
			this.clusterDetailSave.globalWorldSeed = simSaveFileStructure.worldDetail.globalWorldSeed;
			Vector2 vector = Grid.CellToPos2D(Grid.PosToCell(new Vector2I(simSaveFileStructure.x, simSaveFileStructure.y)));
			foreach (WorldDetailSave.OverworldCell overworldCell in simSaveFileStructure.worldDetail.overworldCells)
			{
				for (int num = 0; num != overworldCell.poly.Vertices.Count; num++)
				{
					List<Vector2> vertices = overworldCell.poly.Vertices;
					int num2 = num;
					vertices[num2] += vector;
				}
				overworldCell.poly.RefreshBounds();
			}
			this.clusterDetailSave.overworldCells.AddRange(simSaveFileStructure.worldDetail.overworldCells);
		}
		Sim.SIM_Initialize(new Sim.GAME_MessageHandler(Sim.DLL_MessageHandler));
		SimMessages.CreateSimElementsTable(ElementLoader.elements);
		Sim.AllocateCells(this.m_cluster.size.x, this.m_cluster.size.y, false);
		SimMessages.DefineWorldOffsets(this.m_cluster.worlds.Select((WorldGen world) => new SimMessages.WorldOffsetData
		{
			worldOffsetX = world.WorldOffset.x,
			worldOffsetY = world.WorldOffset.y,
			worldSizeX = world.WorldSize.x,
			worldSizeY = world.WorldSize.y
		}).ToList<SimMessages.WorldOffsetData>());
		SimMessages.CreateDiseaseTable(Db.Get().Diseases);
		Sim.HandleMessage(SimMessageHashes.ClearUnoccupiedCells, 0, null);
		try
		{
			foreach (SimSaveFileStructure simSaveFileStructure2 in pooledList)
			{
				FastReader fastReader2 = new FastReader(simSaveFileStructure2.Sim);
				if (Sim.Load(fastReader2) != 0)
				{
					DebugUtil.LogWarningArgs(new object[] { "\n--- Error loading save ---\nSimDLL found bad data\n" });
					Sim.Shutdown();
					pooledList.Recycle();
					return false;
				}
			}
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("--- Error loading Sim FROM NEW WORLDGEN ---" + ex.Message + "\n" + ex.StackTrace);
			Sim.Shutdown();
			pooledList.Recycle();
			return false;
		}
		global::Debug.Log("Attempt success");
		Sim.Start();
		SceneInitializer.Instance.PostLoadPrefabs();
		SceneInitializer.Instance.NewSaveGamePrefab();
		this.cachedGSD = this.m_cluster.currentWorld.SpawnData;
		this.OnWorldGenComplete.Signal(this.m_cluster);
		OniMetrics.LogEvent(OniMetrics.Event.NewSave, "NewGame", true);
		StoryManager.Instance.InitialSaveSetup();
		ThreadedHttps<KleiMetrics>.Instance.IncrementGameCount();
		OniMetrics.SendEvent(OniMetrics.Event.NewSave, "New Save");
		pooledList.Recycle();
		return true;
	}

	// Token: 0x170005C5 RID: 1477
	// (get) Token: 0x060051C3 RID: 20931 RVA: 0x001DC210 File Offset: 0x001DA410
	// (set) Token: 0x060051C4 RID: 20932 RVA: 0x001DC218 File Offset: 0x001DA418
	public GameSpawnData cachedGSD { get; private set; }

	// Token: 0x170005C6 RID: 1478
	// (get) Token: 0x060051C5 RID: 20933 RVA: 0x001DC221 File Offset: 0x001DA421
	// (set) Token: 0x060051C6 RID: 20934 RVA: 0x001DC229 File Offset: 0x001DA429
	public WorldDetailSave clusterDetailSave { get; private set; }

	// Token: 0x060051C7 RID: 20935 RVA: 0x001DC232 File Offset: 0x001DA432
	public void SetWorldDetail(WorldDetailSave worldDetail)
	{
		this.clusterDetailSave = worldDetail;
	}

	// Token: 0x060051C8 RID: 20936 RVA: 0x001DC23C File Offset: 0x001DA43C
	private void ReportSaveMetrics(bool is_auto_save)
	{
		if (ThreadedHttps<KleiMetrics>.Instance == null || !ThreadedHttps<KleiMetrics>.Instance.enabled || this.saveManager == null)
		{
			return;
		}
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary[GameClock.NewCycleKey] = GameClock.Instance.GetCycle() + 1;
		dictionary["IsAutoSave"] = is_auto_save;
		dictionary["SavedPrefabs"] = this.GetSavedPrefabMetrics();
		dictionary["ResourcesAccessible"] = this.GetWorldInventoryMetrics();
		dictionary["MinionMetrics"] = this.GetMinionMetrics();
		dictionary["WorldMetrics"] = this.GetWorldMetrics();
		if (is_auto_save)
		{
			dictionary["DailyReport"] = this.GetDailyReportMetrics();
			dictionary["PerformanceMeasurements"] = this.GetPerformanceMeasurements();
			dictionary["AverageFrameTime"] = this.GetFrameTime();
		}
		dictionary["CustomGameSettings"] = CustomGameSettings.Instance.GetSettingsForMetrics();
		dictionary["CustomMixingSettings"] = CustomGameSettings.Instance.GetSettingsForMixingMetrics();
		ThreadedHttps<KleiMetrics>.Instance.SendEvent(dictionary, "ReportSaveMetrics");
	}

	// Token: 0x060051C9 RID: 20937 RVA: 0x001DC358 File Offset: 0x001DA558
	private List<SaveLoader.MinionMetricsData> GetMinionMetrics()
	{
		List<SaveLoader.MinionMetricsData> list = new List<SaveLoader.MinionMetricsData>();
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			if (!(minionIdentity == null))
			{
				Amounts amounts = minionIdentity.gameObject.GetComponent<Modifiers>().amounts;
				List<SaveLoader.MinionAttrFloatData> list2 = new List<SaveLoader.MinionAttrFloatData>(amounts.Count);
				foreach (AmountInstance amountInstance in amounts)
				{
					float value = amountInstance.value;
					if (!float.IsNaN(value) && !float.IsInfinity(value))
					{
						list2.Add(new SaveLoader.MinionAttrFloatData
						{
							Name = amountInstance.modifier.Id,
							Value = amountInstance.value
						});
					}
				}
				MinionResume component = minionIdentity.gameObject.GetComponent<MinionResume>();
				float totalExperienceGained = component.TotalExperienceGained;
				List<string> list3 = new List<string>();
				foreach (KeyValuePair<string, bool> keyValuePair in component.MasteryBySkillID)
				{
					if (keyValuePair.Value)
					{
						list3.Add(keyValuePair.Key);
					}
				}
				list.Add(new SaveLoader.MinionMetricsData
				{
					Name = minionIdentity.name,
					Modifiers = list2,
					TotalExperienceGained = totalExperienceGained,
					Skills = list3
				});
			}
		}
		return list;
	}

	// Token: 0x060051CA RID: 20938 RVA: 0x001DC528 File Offset: 0x001DA728
	private List<SaveLoader.SavedPrefabMetricsData> GetSavedPrefabMetrics()
	{
		Dictionary<Tag, List<SaveLoadRoot>> lists = this.saveManager.GetLists();
		List<SaveLoader.SavedPrefabMetricsData> list = new List<SaveLoader.SavedPrefabMetricsData>(lists.Count);
		foreach (KeyValuePair<Tag, List<SaveLoadRoot>> keyValuePair in lists)
		{
			Tag key = keyValuePair.Key;
			List<SaveLoadRoot> value = keyValuePair.Value;
			if (value.Count > 0)
			{
				list.Add(new SaveLoader.SavedPrefabMetricsData
				{
					PrefabName = key.ToString(),
					Count = value.Count
				});
			}
		}
		return list;
	}

	// Token: 0x060051CB RID: 20939 RVA: 0x001DC5D4 File Offset: 0x001DA7D4
	private List<SaveLoader.WorldInventoryMetricsData> GetWorldInventoryMetrics()
	{
		Dictionary<Tag, float> allWorldsAccessibleAmounts = ClusterManager.Instance.GetAllWorldsAccessibleAmounts();
		List<SaveLoader.WorldInventoryMetricsData> list = new List<SaveLoader.WorldInventoryMetricsData>(allWorldsAccessibleAmounts.Count);
		foreach (KeyValuePair<Tag, float> keyValuePair in allWorldsAccessibleAmounts)
		{
			float value = keyValuePair.Value;
			if (!float.IsInfinity(value) && !float.IsNaN(value))
			{
				list.Add(new SaveLoader.WorldInventoryMetricsData
				{
					Name = keyValuePair.Key.ToString(),
					Amount = value
				});
			}
		}
		return list;
	}

	// Token: 0x060051CC RID: 20940 RVA: 0x001DC680 File Offset: 0x001DA880
	private List<SaveLoader.DailyReportMetricsData> GetDailyReportMetrics()
	{
		List<SaveLoader.DailyReportMetricsData> list = new List<SaveLoader.DailyReportMetricsData>();
		int cycle = GameClock.Instance.GetCycle();
		ReportManager.DailyReport dailyReport = ReportManager.Instance.FindReport(cycle);
		if (dailyReport != null)
		{
			foreach (ReportManager.ReportEntry reportEntry in dailyReport.reportEntries)
			{
				SaveLoader.DailyReportMetricsData dailyReportMetricsData = default(SaveLoader.DailyReportMetricsData);
				dailyReportMetricsData.Name = reportEntry.reportType.ToString();
				if (!float.IsInfinity(reportEntry.Net) && !float.IsNaN(reportEntry.Net))
				{
					dailyReportMetricsData.Net = new float?(reportEntry.Net);
				}
				if (SaveLoader.force_infinity)
				{
					dailyReportMetricsData.Net = null;
				}
				if (!float.IsInfinity(reportEntry.Positive) && !float.IsNaN(reportEntry.Positive))
				{
					dailyReportMetricsData.Positive = new float?(reportEntry.Positive);
				}
				if (!float.IsInfinity(reportEntry.Negative) && !float.IsNaN(reportEntry.Negative))
				{
					dailyReportMetricsData.Negative = new float?(reportEntry.Negative);
				}
				list.Add(dailyReportMetricsData);
			}
			list.Add(new SaveLoader.DailyReportMetricsData
			{
				Name = "MinionCount",
				Net = new float?((float)Components.LiveMinionIdentities.Count),
				Positive = new float?(0f),
				Negative = new float?(0f)
			});
		}
		return list;
	}

	// Token: 0x060051CD RID: 20941 RVA: 0x001DC818 File Offset: 0x001DAA18
	private List<SaveLoader.PerformanceMeasurement> GetPerformanceMeasurements()
	{
		List<SaveLoader.PerformanceMeasurement> list = new List<SaveLoader.PerformanceMeasurement>();
		if (Global.Instance != null)
		{
			PerformanceMonitor component = Global.Instance.GetComponent<PerformanceMonitor>();
			list.Add(new SaveLoader.PerformanceMeasurement
			{
				name = "FramesAbove30",
				value = component.NumFramesAbove30
			});
			list.Add(new SaveLoader.PerformanceMeasurement
			{
				name = "FramesBelow30",
				value = component.NumFramesBelow30
			});
			component.Reset();
		}
		return list;
	}

	// Token: 0x060051CE RID: 20942 RVA: 0x001DC8A0 File Offset: 0x001DAAA0
	private float GetFrameTime()
	{
		PerformanceMonitor component = Global.Instance.GetComponent<PerformanceMonitor>();
		DebugUtil.LogArgs(new object[]
		{
			"Average frame time:",
			1f / component.FPS
		});
		return 1f / component.FPS;
	}

	// Token: 0x060051CF RID: 20943 RVA: 0x001DC8EC File Offset: 0x001DAAEC
	private List<SaveLoader.WorldMetricsData> GetWorldMetrics()
	{
		List<SaveLoader.WorldMetricsData> list = new List<SaveLoader.WorldMetricsData>();
		if (Global.Instance != null)
		{
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				if (!worldContainer.IsModuleInterior)
				{
					float num = (worldContainer.IsDiscovered ? worldContainer.DiscoveryTimestamp : (-1f));
					float num2 = (worldContainer.IsDupeVisited ? worldContainer.DupeVisitedTimestamp : (-1f));
					list.Add(new SaveLoader.WorldMetricsData
					{
						Name = worldContainer.worldName,
						DiscoveryTimestamp = num,
						DupeVisitedTimestamp = num2
					});
				}
			}
		}
		return list;
	}

	// Token: 0x060051D0 RID: 20944 RVA: 0x001DC9B8 File Offset: 0x001DABB8
	[Obsolete("Use Game.IsDlcActiveForCurrentSave instead")]
	public bool IsDLCActiveForCurrentSave(string dlcid)
	{
		return DlcManager.IsContentSubscribed(dlcid) && (dlcid == "" || dlcid == "" || this.GameInfo.dlcIds.Contains(dlcid));
	}

	// Token: 0x060051D1 RID: 20945 RVA: 0x001DC9F4 File Offset: 0x001DABF4
	[Obsolete("Use Game methods instead")]
	public bool IsDlcListActiveForCurrentSave(string[] dlcIds)
	{
		if (dlcIds == null || dlcIds.Length == 0)
		{
			return true;
		}
		foreach (string text in dlcIds)
		{
			if (text == "")
			{
				return true;
			}
			if (Game.IsDlcActiveForCurrentSave(text))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060051D2 RID: 20946 RVA: 0x001DCA38 File Offset: 0x001DAC38
	[Obsolete("Use Game methods instead")]
	public bool IsAllDlcActiveForCurrentSave(string[] dlcIds)
	{
		if (dlcIds == null || dlcIds.Length == 0)
		{
			return true;
		}
		foreach (string text in dlcIds)
		{
			if (!(text == "") && !Game.IsDlcActiveForCurrentSave(text))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060051D3 RID: 20947 RVA: 0x001DCA7C File Offset: 0x001DAC7C
	[Obsolete("Use Game methods instead")]
	public bool IsAnyDlcActiveForCurrentSave(string[] dlcIds)
	{
		if (dlcIds == null || dlcIds.Length == 0)
		{
			return false;
		}
		foreach (string text in dlcIds)
		{
			if (!(text == "") && Game.IsDlcActiveForCurrentSave(text))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060051D4 RID: 20948 RVA: 0x001DCABE File Offset: 0x001DACBE
	[Obsolete("Use Game's version")]
	public bool IsCorrectDlcActiveForCurrentSave(string[] required, string[] forbidden)
	{
		return this.IsAllDlcActiveForCurrentSave(required) && !this.IsAnyDlcActiveForCurrentSave(forbidden);
	}

	// Token: 0x060051D5 RID: 20949 RVA: 0x001DCAD8 File Offset: 0x001DACD8
	public string GetSaveLoadContentLetters()
	{
		if (this.GameInfo.dlcIds.Count <= 0)
		{
			return "V";
		}
		string text = "";
		foreach (string text2 in this.GameInfo.dlcIds)
		{
			text += DlcManager.GetContentLetter(text2);
		}
		return text;
	}

	// Token: 0x060051D6 RID: 20950 RVA: 0x001DCB58 File Offset: 0x001DAD58
	public void UpgradeActiveSaveDLCInfo(string dlcId, bool trigger_load = false)
	{
		string activeSaveFolder = SaveLoader.GetActiveSaveFolder();
		string text = SaveGame.Instance.BaseName + UI.FRONTEND.OPTIONS_SCREEN.TOGGLE_SANDBOX_SCREEN.BACKUP_SAVE_GAME_APPEND + ".sav";
		string text2 = global::System.IO.Path.Combine(activeSaveFolder, text);
		this.Save(text2, false, false);
		if (!this.GameInfo.dlcIds.Contains(dlcId))
		{
			this.GameInfo.dlcIds.Add(dlcId);
		}
		string current_save = SaveLoader.GetActiveSaveFilePath();
		this.Save(SaveLoader.GetActiveSaveFilePath(), false, false);
		if (trigger_load)
		{
			LoadingOverlay.Load(delegate
			{
				LoadScreen.DoLoad(current_save);
			});
		}
	}

	// Token: 0x040036F7 RID: 14071
	[MyCmpGet]
	private GridSettings gridSettings;

	// Token: 0x040036F9 RID: 14073
	private bool saveFileCorrupt;

	// Token: 0x040036FA RID: 14074
	private bool compressSaveData = true;

	// Token: 0x040036FB RID: 14075
	private int lastUncompressedSize;

	// Token: 0x040036FC RID: 14076
	public bool saveAsText;

	// Token: 0x040036FD RID: 14077
	public const string MAINMENU_LEVELNAME = "launchscene";

	// Token: 0x040036FE RID: 14078
	public const string FRONTEND_LEVELNAME = "frontend";

	// Token: 0x040036FF RID: 14079
	public const string BACKEND_LEVELNAME = "backend";

	// Token: 0x04003700 RID: 14080
	public const string SAVE_EXTENSION = ".sav";

	// Token: 0x04003701 RID: 14081
	public const string AUTOSAVE_FOLDER = "auto_save";

	// Token: 0x04003702 RID: 14082
	public const string CLOUDSAVE_FOLDER = "cloud_save_files";

	// Token: 0x04003703 RID: 14083
	public const string SAVE_FOLDER = "save_files";

	// Token: 0x04003704 RID: 14084
	public const int MAX_AUTOSAVE_FILES = 10;

	// Token: 0x04003706 RID: 14086
	[NonSerialized]
	public SaveManager saveManager;

	// Token: 0x04003708 RID: 14088
	private Cluster m_cluster;

	// Token: 0x04003709 RID: 14089
	private ClusterLayout m_clusterLayout;

	// Token: 0x0400370B RID: 14091
	private const string CorruptFileSuffix = "_";

	// Token: 0x0400370C RID: 14092
	private const float SAVE_BUFFER_HEAD_ROOM = 0.1f;

	// Token: 0x0400370D RID: 14093
	private bool mustRestartOnFail;

	// Token: 0x04003710 RID: 14096
	public const string METRIC_SAVED_PREFAB_KEY = "SavedPrefabs";

	// Token: 0x04003711 RID: 14097
	public const string METRIC_IS_AUTO_SAVE_KEY = "IsAutoSave";

	// Token: 0x04003712 RID: 14098
	public const string METRIC_WAS_DEBUG_EVER_USED = "WasDebugEverUsed";

	// Token: 0x04003713 RID: 14099
	public const string METRIC_IS_SANDBOX_ENABLED = "IsSandboxEnabled";

	// Token: 0x04003714 RID: 14100
	public const string METRIC_RESOURCES_ACCESSIBLE_KEY = "ResourcesAccessible";

	// Token: 0x04003715 RID: 14101
	public const string METRIC_DAILY_REPORT_KEY = "DailyReport";

	// Token: 0x04003716 RID: 14102
	public const string METRIC_WORLD_METRICS_KEY = "WorldMetrics";

	// Token: 0x04003717 RID: 14103
	public const string METRIC_MINION_METRICS_KEY = "MinionMetrics";

	// Token: 0x04003718 RID: 14104
	public const string METRIC_CUSTOM_GAME_SETTINGS = "CustomGameSettings";

	// Token: 0x04003719 RID: 14105
	public const string METRIC_CUSTOM_MIXING_SETTINGS = "CustomMixingSettings";

	// Token: 0x0400371A RID: 14106
	public const string METRIC_PERFORMANCE_MEASUREMENTS = "PerformanceMeasurements";

	// Token: 0x0400371B RID: 14107
	public const string METRIC_FRAME_TIME = "AverageFrameTime";

	// Token: 0x0400371C RID: 14108
	private static bool force_infinity;

	// Token: 0x02001BDA RID: 7130
	public class FlowUtilityNetworkInstance
	{
		// Token: 0x0400844D RID: 33869
		public int id = -1;

		// Token: 0x0400844E RID: 33870
		public SimHashes containedElement = SimHashes.Vacuum;

		// Token: 0x0400844F RID: 33871
		public float containedMass;

		// Token: 0x04008450 RID: 33872
		public float containedTemperature;
	}

	// Token: 0x02001BDB RID: 7131
	[SerializationConfig(global::KSerialization.MemberSerialization.OptOut)]
	public class FlowUtilityNetworkSaver : ISaveLoadable
	{
		// Token: 0x0600A8F5 RID: 43253 RVA: 0x003B635E File Offset: 0x003B455E
		public FlowUtilityNetworkSaver()
		{
			this.gas = new List<SaveLoader.FlowUtilityNetworkInstance>();
			this.liquid = new List<SaveLoader.FlowUtilityNetworkInstance>();
		}

		// Token: 0x04008451 RID: 33873
		public List<SaveLoader.FlowUtilityNetworkInstance> gas;

		// Token: 0x04008452 RID: 33874
		public List<SaveLoader.FlowUtilityNetworkInstance> liquid;
	}

	// Token: 0x02001BDC RID: 7132
	public struct SaveFileEntry
	{
		// Token: 0x04008453 RID: 33875
		public string path;

		// Token: 0x04008454 RID: 33876
		public global::System.DateTime timeStamp;
	}

	// Token: 0x02001BDD RID: 7133
	public enum SaveType
	{
		// Token: 0x04008456 RID: 33878
		local,
		// Token: 0x04008457 RID: 33879
		cloud,
		// Token: 0x04008458 RID: 33880
		both
	}

	// Token: 0x02001BDE RID: 7134
	private struct MinionAttrFloatData
	{
		// Token: 0x04008459 RID: 33881
		public string Name;

		// Token: 0x0400845A RID: 33882
		public float Value;
	}

	// Token: 0x02001BDF RID: 7135
	private struct MinionMetricsData
	{
		// Token: 0x0400845B RID: 33883
		public string Name;

		// Token: 0x0400845C RID: 33884
		public List<SaveLoader.MinionAttrFloatData> Modifiers;

		// Token: 0x0400845D RID: 33885
		public float TotalExperienceGained;

		// Token: 0x0400845E RID: 33886
		public List<string> Skills;
	}

	// Token: 0x02001BE0 RID: 7136
	private struct SavedPrefabMetricsData
	{
		// Token: 0x0400845F RID: 33887
		public string PrefabName;

		// Token: 0x04008460 RID: 33888
		public int Count;
	}

	// Token: 0x02001BE1 RID: 7137
	private struct WorldInventoryMetricsData
	{
		// Token: 0x04008461 RID: 33889
		public string Name;

		// Token: 0x04008462 RID: 33890
		public float Amount;
	}

	// Token: 0x02001BE2 RID: 7138
	private struct DailyReportMetricsData
	{
		// Token: 0x04008463 RID: 33891
		public string Name;

		// Token: 0x04008464 RID: 33892
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public float? Net;

		// Token: 0x04008465 RID: 33893
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public float? Positive;

		// Token: 0x04008466 RID: 33894
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public float? Negative;
	}

	// Token: 0x02001BE3 RID: 7139
	private struct PerformanceMeasurement
	{
		// Token: 0x04008467 RID: 33895
		public string name;

		// Token: 0x04008468 RID: 33896
		public float value;
	}

	// Token: 0x02001BE4 RID: 7140
	private struct WorldMetricsData
	{
		// Token: 0x04008469 RID: 33897
		public string Name;

		// Token: 0x0400846A RID: 33898
		public float DiscoveryTimestamp;

		// Token: 0x0400846B RID: 33899
		public float DupeVisitedTimestamp;
	}
}
