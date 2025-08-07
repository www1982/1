using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using ProcGen;
using ProcGenGame;
using Steamworks;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C23 RID: 3107
public class LoadScreen : KModalScreen
{
	// Token: 0x170006E4 RID: 1764
	// (get) Token: 0x06005E10 RID: 24080 RVA: 0x002260A8 File Offset: 0x002242A8
	// (set) Token: 0x06005E11 RID: 24081 RVA: 0x002260AF File Offset: 0x002242AF
	public static LoadScreen Instance { get; private set; }

	// Token: 0x06005E12 RID: 24082 RVA: 0x002260B7 File Offset: 0x002242B7
	public static void DestroyInstance()
	{
		LoadScreen.Instance = null;
	}

	// Token: 0x06005E13 RID: 24083 RVA: 0x002260C0 File Offset: 0x002242C0
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(LoadScreen.Instance == null);
		LoadScreen.Instance = this;
		base.OnPrefabInit();
		this.saveButtonPrefab.gameObject.SetActive(false);
		this.colonyListPool = new UIPool<HierarchyReferences>(this.saveButtonPrefab);
		if (SpeedControlScreen.Instance != null)
		{
			SpeedControlScreen.Instance.Pause(false, false);
		}
		if (this.closeButton != null)
		{
			this.closeButton.onClick += delegate
			{
				this.Deactivate();
			};
		}
		if (this.colonyCloudButton != null)
		{
			this.colonyCloudButton.onClick += delegate
			{
				this.ConvertAllToCloud();
			};
		}
		if (this.colonyLocalButton != null)
		{
			this.colonyLocalButton.onClick += delegate
			{
				this.ConvertAllToLocal();
			};
		}
		if (this.colonyInfoButton != null)
		{
			this.colonyInfoButton.onClick += delegate
			{
				this.ShowSaveInfo();
			};
		}
		if (this.loadMoreButton != null)
		{
			this.loadMoreButton.onClick += delegate
			{
				this.displayedPageCount++;
				this.RefreshColonyList();
				this.ShowColonyList();
			};
		}
	}

	// Token: 0x06005E14 RID: 24084 RVA: 0x002261DD File Offset: 0x002243DD
	private bool IsInMenu()
	{
		return App.GetCurrentSceneName() == "frontend";
	}

	// Token: 0x06005E15 RID: 24085 RVA: 0x002261EE File Offset: 0x002243EE
	private bool CloudSavesVisible()
	{
		return SaveLoader.GetCloudSavesAvailable() && this.IsInMenu();
	}

	// Token: 0x06005E16 RID: 24086 RVA: 0x00226200 File Offset: 0x00224400
	protected override void OnActivate()
	{
		base.OnActivate();
		WorldGen.LoadSettings(false);
		this.SetCloudSaveInfoActive(this.CloudSavesVisible());
		this.displayedPageCount = 1;
		this.RefreshColonyList();
		this.ShowColonyList();
		bool cloudSavesAvailable = SaveLoader.GetCloudSavesAvailable();
		this.cloudTutorialBouncer.gameObject.SetActive(cloudSavesAvailable);
		if (cloudSavesAvailable && !this.cloudTutorialBouncer.IsBouncing())
		{
			int @int = KPlayerPrefs.GetInt("LoadScreenCloudTutorialTimes", 0);
			if (@int < 5)
			{
				this.cloudTutorialBouncer.Bounce();
				KPlayerPrefs.SetInt("LoadScreenCloudTutorialTimes", @int + 1);
				KPlayerPrefs.GetInt("LoadScreenCloudTutorialTimes", 0);
			}
			else
			{
				this.cloudTutorialBouncer.gameObject.SetActive(false);
			}
		}
		if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
		{
			this.colonyInfoButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x06005E17 RID: 24087 RVA: 0x002262C8 File Offset: 0x002244C8
	private Dictionary<string, List<LoadScreen.SaveGameFileDetails>> GetColoniesDetails(List<SaveLoader.SaveFileEntry> files)
	{
		Dictionary<string, List<LoadScreen.SaveGameFileDetails>> dictionary = new Dictionary<string, List<LoadScreen.SaveGameFileDetails>>();
		if (files.Count <= 0)
		{
			return dictionary;
		}
		for (int i = 0; i < files.Count; i++)
		{
			if (this.IsFileValid(files[i].path))
			{
				global::Tuple<SaveGame.Header, SaveGame.GameInfo> fileInfo = SaveGame.GetFileInfo(files[i].path);
				SaveGame.Header first = fileInfo.first;
				SaveGame.GameInfo second = fileInfo.second;
				global::System.DateTime timeStamp = files[i].timeStamp;
				long num = 0L;
				try
				{
					num = new FileInfo(files[i].path).Length;
				}
				catch (Exception ex)
				{
					global::Debug.LogWarning("Failed to get size for file: " + files[i].ToString() + "\n" + ex.ToString());
				}
				LoadScreen.SaveGameFileDetails saveGameFileDetails = new LoadScreen.SaveGameFileDetails
				{
					BaseName = second.baseName,
					FileName = files[i].path,
					FileDate = timeStamp,
					FileHeader = first,
					FileInfo = second,
					Size = num,
					UniqueID = SaveGame.GetSaveUniqueID(second)
				};
				if (!dictionary.ContainsKey(saveGameFileDetails.UniqueID))
				{
					dictionary.Add(saveGameFileDetails.UniqueID, new List<LoadScreen.SaveGameFileDetails>());
				}
				dictionary[saveGameFileDetails.UniqueID].Add(saveGameFileDetails);
			}
		}
		return dictionary;
	}

	// Token: 0x06005E18 RID: 24088 RVA: 0x00226430 File Offset: 0x00224630
	private Dictionary<string, List<LoadScreen.SaveGameFileDetails>> GetColonies(bool sort)
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(sort, SaveLoader.SaveType.both);
		return this.GetColoniesDetails(allFiles);
	}

	// Token: 0x06005E19 RID: 24089 RVA: 0x0022644C File Offset: 0x0022464C
	private Dictionary<string, List<LoadScreen.SaveGameFileDetails>> GetLocalColonies(bool sort)
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(sort, SaveLoader.SaveType.local);
		return this.GetColoniesDetails(allFiles);
	}

	// Token: 0x06005E1A RID: 24090 RVA: 0x00226468 File Offset: 0x00224668
	private Dictionary<string, List<LoadScreen.SaveGameFileDetails>> GetCloudColonies(bool sort)
	{
		List<SaveLoader.SaveFileEntry> allFiles = SaveLoader.GetAllFiles(sort, SaveLoader.SaveType.cloud);
		return this.GetColoniesDetails(allFiles);
	}

	// Token: 0x06005E1B RID: 24091 RVA: 0x00226484 File Offset: 0x00224684
	private bool IsFileValid(string filename)
	{
		bool flag = false;
		try
		{
			SaveGame.Header header;
			flag = SaveLoader.LoadHeader(filename, out header).saveMajorVersion >= 7;
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Corrupted save file: " + filename + "\n" + ex.ToString());
		}
		return flag;
	}

	// Token: 0x06005E1C RID: 24092 RVA: 0x002264D8 File Offset: 0x002246D8
	private void CheckCloudLocalOverlap()
	{
		if (!SaveLoader.GetCloudSavesAvailable())
		{
			return;
		}
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		if (cloudSavePrefix == null)
		{
			return;
		}
		foreach (KeyValuePair<string, List<LoadScreen.SaveGameFileDetails>> keyValuePair in this.GetColonies(false))
		{
			bool flag = false;
			List<LoadScreen.SaveGameFileDetails> list = new List<LoadScreen.SaveGameFileDetails>();
			foreach (LoadScreen.SaveGameFileDetails saveGameFileDetails in keyValuePair.Value)
			{
				if (SaveLoader.IsSaveCloud(saveGameFileDetails.FileName))
				{
					flag = true;
				}
				else
				{
					list.Add(saveGameFileDetails);
				}
			}
			if (flag && list.Count != 0)
			{
				string baseName = list[0].BaseName;
				string text = global::System.IO.Path.Combine(SaveLoader.GetSavePrefix(), baseName);
				string text2 = global::System.IO.Path.Combine(cloudSavePrefix, baseName);
				if (!Directory.Exists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				global::Debug.Log("Saves / Found overlapped cloud/local saves for colony '" + baseName + "', moving to cloud...");
				foreach (LoadScreen.SaveGameFileDetails saveGameFileDetails2 in list)
				{
					string fileName = saveGameFileDetails2.FileName;
					string text3 = global::System.IO.Path.ChangeExtension(fileName, "png");
					string text4 = text2;
					if (SaveLoader.IsSaveAuto(fileName))
					{
						string text5 = global::System.IO.Path.Combine(text4, "auto_save");
						if (!Directory.Exists(text5))
						{
							Directory.CreateDirectory(text5);
						}
						text4 = text5;
					}
					string text6 = global::System.IO.Path.Combine(text4, global::System.IO.Path.GetFileName(fileName));
					global::Tuple<bool, bool> tuple;
					if (this.FileMatch(fileName, text6, out tuple))
					{
						global::Debug.Log("Saves / file match found for `" + fileName + "`...");
						this.MigrateFile(fileName, text6, false);
						string text7 = global::System.IO.Path.ChangeExtension(text6, "png");
						this.MigrateFile(text3, text7, true);
					}
					else
					{
						global::Debug.Log("Saves / no file match found for `" + fileName + "`... move as copy");
						string nextUsableSavePath = SaveLoader.GetNextUsableSavePath(text6);
						this.MigrateFile(fileName, nextUsableSavePath, false);
						string text8 = global::System.IO.Path.ChangeExtension(nextUsableSavePath, "png");
						this.MigrateFile(text3, text8, true);
					}
				}
				this.RemoveEmptyFolder(text);
			}
		}
	}

	// Token: 0x06005E1D RID: 24093 RVA: 0x00226750 File Offset: 0x00224950
	private void DeleteFileAndEmptyFolder(string file)
	{
		if (File.Exists(file))
		{
			File.Delete(file);
		}
		this.RemoveEmptyFolder(global::System.IO.Path.GetDirectoryName(file));
	}

	// Token: 0x06005E1E RID: 24094 RVA: 0x0022676C File Offset: 0x0022496C
	private void RemoveEmptyFolder(string path)
	{
		if (!Directory.Exists(path))
		{
			return;
		}
		if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory))
		{
			return;
		}
		if (Directory.EnumerateFileSystemEntries(path).Any<string>())
		{
			return;
		}
		try
		{
			Directory.Delete(path);
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("Failed to remove empty directory `" + path + "`...");
			global::Debug.LogWarning(ex);
		}
	}

	// Token: 0x06005E1F RID: 24095 RVA: 0x002267E0 File Offset: 0x002249E0
	private void RefreshColonyList()
	{
		if (this.colonyListPool != null)
		{
			this.colonyListPool.ClearAll();
		}
		this.CheckCloudLocalOverlap();
		Dictionary<string, List<LoadScreen.SaveGameFileDetails>> colonies = this.GetColonies(true);
		if (colonies.Count > 0)
		{
			int num = 0;
			foreach (KeyValuePair<string, List<LoadScreen.SaveGameFileDetails>> keyValuePair in colonies)
			{
				if (num >= this.displayedPageCount * 20)
				{
					break;
				}
				this.AddColonyToList(keyValuePair.Value);
				num++;
			}
			this.loadMoreButton.gameObject.SetActive(colonies.Count != num);
			this.loadMoreButton.gameObject.transform.SetAsLastSibling();
		}
	}

	// Token: 0x06005E20 RID: 24096 RVA: 0x002268A4 File Offset: 0x00224AA4
	private string GetFileHash(string path)
	{
		string text;
		using (MD5 md = MD5.Create())
		{
			using (FileStream fileStream = File.OpenRead(path))
			{
				text = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
			}
		}
		return text;
	}

	// Token: 0x06005E21 RID: 24097 RVA: 0x00226914 File Offset: 0x00224B14
	private bool FileMatch(string file, string other_file, out global::Tuple<bool, bool> matches)
	{
		matches = new global::Tuple<bool, bool>(false, false);
		if (!File.Exists(file))
		{
			return false;
		}
		if (!File.Exists(other_file))
		{
			return false;
		}
		bool flag = false;
		bool flag2 = false;
		try
		{
			string fileHash = this.GetFileHash(file);
			string fileHash2 = this.GetFileHash(other_file);
			FileInfo fileInfo = new FileInfo(file);
			FileInfo fileInfo2 = new FileInfo(other_file);
			flag = fileInfo.Length == fileInfo2.Length;
			flag2 = fileHash == fileHash2;
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning(string.Concat(new string[] { "FileMatch / file match failed for `", file, "` vs `", other_file, "`!" }));
			global::Debug.LogWarning(ex);
			return false;
		}
		matches.first = flag;
		matches.second = flag2;
		return flag && flag2;
	}

	// Token: 0x06005E22 RID: 24098 RVA: 0x002269DC File Offset: 0x00224BDC
	private bool MigrateFile(string source, string dest, bool ignoreMissing = false)
	{
		global::Debug.Log(string.Concat(new string[] { "Migration / moving `", source, "` to `", dest, "` ..." }));
		if (dest == source)
		{
			global::Debug.Log(string.Concat(new string[] { "Migration / ignored `", source, "` to `", dest, "` ... same location" }));
			return true;
		}
		global::Tuple<bool, bool> tuple;
		if (this.FileMatch(source, dest, out tuple))
		{
			global::Debug.Log("Migration / dest and source are identical size + hash ... removing original");
			try
			{
				this.DeleteFileAndEmptyFolder(source);
			}
			catch (Exception ex)
			{
				global::Debug.LogWarning("Migration / removing original failed for `" + source + "`!");
				global::Debug.LogWarning(ex);
				throw ex;
			}
			return true;
		}
		try
		{
			global::Debug.Log("Migration / copying...");
			File.Copy(source, dest, false);
		}
		catch (FileNotFoundException obj) when (ignoreMissing)
		{
			global::Debug.Log("Migration / File `" + source + "` wasn't found but we're ignoring that.");
			return true;
		}
		catch (Exception ex2)
		{
			global::Debug.LogWarning("Migration / copy failed for `" + source + "`! Leaving it alone");
			global::Debug.LogWarning(ex2);
			global::Debug.LogWarning("failed to convert colony: " + ex2.ToString());
			throw ex2;
		}
		global::Debug.Log("Migration / copy ok ...");
		global::Tuple<bool, bool> tuple2;
		if (!this.FileMatch(source, dest, out tuple2))
		{
			global::Debug.LogWarning("Migration / failed to match dest file for `" + source + "`!");
			global::Debug.LogWarning(string.Format("Migration / did hash match? {0} did size match? {1}", tuple2.second, tuple2.first));
			throw new Exception("Hash/Size didn't match for source and destination");
		}
		global::Debug.Log("Migration / hash validation ok ... removing original");
		try
		{
			this.DeleteFileAndEmptyFolder(source);
		}
		catch (Exception ex3)
		{
			global::Debug.LogWarning("Migration / removing original failed for `" + source + "`!");
			global::Debug.LogWarning(ex3);
			throw ex3;
		}
		global::Debug.Log("Migration / moved ok for `" + source + "`!");
		return true;
	}

	// Token: 0x06005E23 RID: 24099 RVA: 0x00226BE0 File Offset: 0x00224DE0
	private bool MigrateSave(string dest_root, string file, bool is_auto_save, out string saveError)
	{
		saveError = null;
		global::Tuple<SaveGame.Header, SaveGame.GameInfo> fileInfo = SaveGame.GetFileInfo(file);
		SaveGame.Header first = fileInfo.first;
		string text = fileInfo.second.baseName.TrimEnd(' ');
		string fileName = global::System.IO.Path.GetFileName(file);
		string text2 = global::System.IO.Path.Combine(dest_root, text);
		if (!Directory.Exists(text2))
		{
			text2 = Directory.CreateDirectory(text2).FullName;
		}
		string text3 = text2;
		if (is_auto_save)
		{
			string text4 = global::System.IO.Path.Combine(text2, "auto_save");
			if (!Directory.Exists(text4))
			{
				Directory.CreateDirectory(text4);
			}
			text3 = text4;
		}
		string text5 = global::System.IO.Path.Combine(text3, fileName);
		string text6 = global::System.IO.Path.ChangeExtension(file, "png");
		string text7 = global::System.IO.Path.ChangeExtension(text5, "png");
		try
		{
			this.MigrateFile(file, text5, false);
			this.MigrateFile(text6, text7, true);
		}
		catch (Exception ex)
		{
			saveError = ex.Message;
			return false;
		}
		return true;
	}

	// Token: 0x06005E24 RID: 24100 RVA: 0x00226CBC File Offset: 0x00224EBC
	private ValueTuple<int, int, ulong> GetSavesSizeAndCounts(List<LoadScreen.SaveGameFileDetails> list)
	{
		ulong num = 0UL;
		int num2 = 0;
		int num3 = 0;
		for (int i = 0; i < list.Count; i++)
		{
			LoadScreen.SaveGameFileDetails saveGameFileDetails = list[i];
			num += (ulong)saveGameFileDetails.Size;
			if (saveGameFileDetails.FileInfo.isAutoSave)
			{
				num3++;
			}
			else
			{
				num2++;
			}
		}
		return new ValueTuple<int, int, ulong>(num2, num3, num);
	}

	// Token: 0x06005E25 RID: 24101 RVA: 0x00226D14 File Offset: 0x00224F14
	private int CountValidSaves(string path, SearchOption searchType = SearchOption.AllDirectories)
	{
		int num = 0;
		List<SaveLoader.SaveFileEntry> saveFiles = SaveLoader.GetSaveFiles(path, false, searchType);
		for (int i = 0; i < saveFiles.Count; i++)
		{
			if (this.IsFileValid(saveFiles[i].path))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06005E26 RID: 24102 RVA: 0x00226D58 File Offset: 0x00224F58
	private ValueTuple<int, int> GetMigrationSaveCounts()
	{
		int num = this.CountValidSaves(SaveLoader.GetSavePrefixAndCreateFolder(), SearchOption.TopDirectoryOnly);
		int num2 = this.CountValidSaves(SaveLoader.GetAutoSavePrefix(), SearchOption.AllDirectories);
		return new ValueTuple<int, int>(num, num2);
	}

	// Token: 0x06005E27 RID: 24103 RVA: 0x00226D84 File Offset: 0x00224F84
	private ValueTuple<int, int> MigrateSaves(out string errorColony, out string errorMessage)
	{
		errorColony = null;
		errorMessage = null;
		int num = 0;
		string savePrefixAndCreateFolder = SaveLoader.GetSavePrefixAndCreateFolder();
		List<SaveLoader.SaveFileEntry> saveFiles = SaveLoader.GetSaveFiles(savePrefixAndCreateFolder, false, SearchOption.TopDirectoryOnly);
		for (int i = 0; i < saveFiles.Count; i++)
		{
			SaveLoader.SaveFileEntry saveFileEntry = saveFiles[i];
			if (this.IsFileValid(saveFileEntry.path))
			{
				string text;
				if (this.MigrateSave(savePrefixAndCreateFolder, saveFileEntry.path, false, out text))
				{
					num++;
				}
				else if (errorColony == null)
				{
					errorColony = saveFileEntry.path;
					errorMessage = text;
				}
			}
		}
		int num2 = 0;
		List<SaveLoader.SaveFileEntry> saveFiles2 = SaveLoader.GetSaveFiles(SaveLoader.GetAutoSavePrefix(), false, SearchOption.AllDirectories);
		for (int j = 0; j < saveFiles2.Count; j++)
		{
			SaveLoader.SaveFileEntry saveFileEntry2 = saveFiles2[j];
			if (this.IsFileValid(saveFileEntry2.path))
			{
				string text2;
				if (this.MigrateSave(savePrefixAndCreateFolder, saveFileEntry2.path, true, out text2))
				{
					num2++;
				}
				else if (errorColony == null)
				{
					errorColony = saveFileEntry2.path;
					errorMessage = text2;
				}
			}
		}
		return new ValueTuple<int, int>(num, num2);
	}

	// Token: 0x06005E28 RID: 24104 RVA: 0x00226E74 File Offset: 0x00225074
	public void ShowMigrationIfNecessary(bool fromMainMenu)
	{
		ValueTuple<int, int> migrationSaveCounts = this.GetMigrationSaveCounts();
		int saveCount = migrationSaveCounts.Item1;
		int autoCount = migrationSaveCounts.Item2;
		if (saveCount == 0 && autoCount == 0)
		{
			if (fromMainMenu)
			{
				this.Deactivate();
			}
			return;
		}
		base.Activate();
		this.migrationPanelRefs.gameObject.SetActive(true);
		KButton migrateButton = this.migrationPanelRefs.GetReference<RectTransform>("MigrateSaves").GetComponent<KButton>();
		KButton continueButton = this.migrationPanelRefs.GetReference<RectTransform>("Continue").GetComponent<KButton>();
		KButton moreInfoButton = this.migrationPanelRefs.GetReference<RectTransform>("MoreInfo").GetComponent<KButton>();
		KButton component = this.migrationPanelRefs.GetReference<RectTransform>("OpenSaves").GetComponent<KButton>();
		LocText statsText = this.migrationPanelRefs.GetReference<RectTransform>("CountText").GetComponent<LocText>();
		LocText infoText = this.migrationPanelRefs.GetReference<RectTransform>("InfoText").GetComponent<LocText>();
		migrateButton.gameObject.SetActive(true);
		continueButton.gameObject.SetActive(false);
		moreInfoButton.gameObject.SetActive(false);
		statsText.text = string.Format(UI.FRONTEND.LOADSCREEN.MIGRATE_COUNT, saveCount, autoCount);
		component.ClearOnClick();
		component.onClick += delegate
		{
			App.OpenWebURL(SaveLoader.GetSavePrefixAndCreateFolder());
		};
		migrateButton.ClearOnClick();
		migrateButton.onClick += delegate
		{
			migrateButton.gameObject.SetActive(false);
			string text;
			string text2;
			ValueTuple<int, int> valueTuple = this.MigrateSaves(out text, out text2);
			int item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			bool flag = text == null;
			string text3 = (flag ? UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT.text : UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES.Replace("{ErrorColony}", text).Replace("{ErrorMessage}", text2));
			statsText.text = string.Format(text3, new object[] { item, saveCount, item2, autoCount });
			infoText.gameObject.SetActive(false);
			if (flag)
			{
				continueButton.gameObject.SetActive(true);
			}
			else
			{
				moreInfoButton.gameObject.SetActive(true);
			}
			MainMenu.Instance.RefreshResumeButton(false);
			this.RefreshColonyList();
		};
		continueButton.ClearOnClick();
		continueButton.onClick += delegate
		{
			this.migrationPanelRefs.gameObject.SetActive(false);
			this.cloudTutorialBouncer.Bounce();
		};
		moreInfoButton.ClearOnClick();
		Action<InfoDialogScreen> <>9__4;
		Action<InfoDialogScreen> <>9__6;
		moreInfoButton.onClick += delegate
		{
			if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
			{
				InfoDialogScreen infoDialogScreen = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, this.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_TITLE).AddPlainText(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_PRE)
					.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM1, "")
					.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM2, "")
					.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM3, "")
					.AddPlainText(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_POST);
				string text4 = UI.CONFIRMDIALOG.OK;
				Action<InfoDialogScreen> action;
				if ((action = <>9__4) == null)
				{
					action = (<>9__4 = delegate(InfoDialogScreen d)
					{
						this.migrationPanelRefs.gameObject.SetActive(false);
						this.cloudTutorialBouncer.Bounce();
						d.Deactivate();
					});
				}
				infoDialogScreen.AddOption(text4, action, true).Activate();
				return;
			}
			InfoDialogScreen infoDialogScreen2 = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, this.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_TITLE).AddPlainText(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_PRE)
				.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM1, "")
				.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM2, "")
				.AddLineItem(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_ITEM3, "")
				.AddPlainText(UI.FRONTEND.LOADSCREEN.MIGRATE_RESULT_FAILURES_MORE_INFO_POST)
				.AddOption(UI.FRONTEND.LOADSCREEN.MIGRATE_FAILURES_FORUM_BUTTON, delegate(InfoDialogScreen d)
				{
					App.OpenWebURL("https://forums.kleientertainment.com/klei-bug-tracker/oni/");
				}, false);
			string text5 = UI.CONFIRMDIALOG.OK;
			Action<InfoDialogScreen> action2;
			if ((action2 = <>9__6) == null)
			{
				action2 = (<>9__6 = delegate(InfoDialogScreen d)
				{
					this.migrationPanelRefs.gameObject.SetActive(false);
					this.cloudTutorialBouncer.Bounce();
					d.Deactivate();
				});
			}
			infoDialogScreen2.AddOption(text5, action2, true).Activate();
		};
	}

	// Token: 0x06005E29 RID: 24105 RVA: 0x0022706D File Offset: 0x0022526D
	private void SetCloudSaveInfoActive(bool active)
	{
		this.colonyCloudButton.gameObject.SetActive(active);
		this.colonyLocalButton.gameObject.SetActive(active);
	}

	// Token: 0x06005E2A RID: 24106 RVA: 0x00227094 File Offset: 0x00225294
	private bool ConvertToLocalOrCloud(string fromRoot, string destRoot, string colonyName)
	{
		string text = global::System.IO.Path.Combine(fromRoot, colonyName);
		string text2 = global::System.IO.Path.Combine(destRoot, colonyName);
		global::Debug.Log(string.Concat(new string[] { "Convert / Colony '", colonyName, "' from `", text, "` => `", text2, "`" }));
		try
		{
			Directory.Move(text, text2);
			return true;
		}
		catch (Exception ex)
		{
			global::Debug.LogWarning("failed to convert colony: " + ex.ToString());
			string text3 = UI.FRONTEND.LOADSCREEN.CONVERT_ERROR.Replace("{Colony}", colonyName).Replace("{Error}", ex.Message);
			this.ShowConvertError(text3);
		}
		return false;
	}

	// Token: 0x06005E2B RID: 24107 RVA: 0x00227150 File Offset: 0x00225350
	private bool ConvertColonyToCloud(string colonyName)
	{
		string savePrefix = SaveLoader.GetSavePrefix();
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		if (cloudSavePrefix == null)
		{
			global::Debug.LogWarning("Failed to move colony to cloud, no cloud save prefix found (usually a userID is missing, not logged in?)");
			return false;
		}
		return this.ConvertToLocalOrCloud(savePrefix, cloudSavePrefix, colonyName);
	}

	// Token: 0x06005E2C RID: 24108 RVA: 0x00227184 File Offset: 0x00225384
	private bool ConvertColonyToLocal(string colonyName)
	{
		string savePrefix = SaveLoader.GetSavePrefix();
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		if (cloudSavePrefix == null)
		{
			global::Debug.LogWarning("Failed to move colony from cloud, no cloud save prefix found (usually a userID is missing, not logged in?)");
			return false;
		}
		return this.ConvertToLocalOrCloud(cloudSavePrefix, savePrefix, colonyName);
	}

	// Token: 0x06005E2D RID: 24109 RVA: 0x002271B8 File Offset: 0x002253B8
	private void DoConvertAllToLocal()
	{
		Dictionary<string, List<LoadScreen.SaveGameFileDetails>> cloudColonies = this.GetCloudColonies(false);
		if (cloudColonies.Count == 0)
		{
			return;
		}
		bool flag = true;
		foreach (KeyValuePair<string, List<LoadScreen.SaveGameFileDetails>> keyValuePair in cloudColonies)
		{
			flag &= this.ConvertColonyToLocal(keyValuePair.Value[0].BaseName);
		}
		if (flag)
		{
			string text = UI.PLATFORMS.STEAM;
			this.ShowSimpleDialog(UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_TO_LOCAL_SUCCESS.Replace("{Client}", text));
		}
		this.RefreshColonyList();
		MainMenu.Instance.RefreshResumeButton(false);
		SaveLoader.SetCloudSavesDefault(false);
	}

	// Token: 0x06005E2E RID: 24110 RVA: 0x00227274 File Offset: 0x00225474
	private void DoConvertAllToCloud()
	{
		Dictionary<string, List<LoadScreen.SaveGameFileDetails>> localColonies = this.GetLocalColonies(false);
		if (localColonies.Count == 0)
		{
			return;
		}
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, List<LoadScreen.SaveGameFileDetails>> keyValuePair in localColonies)
		{
			string baseName = keyValuePair.Value[0].BaseName;
			if (!list.Contains(baseName))
			{
				list.Add(baseName);
			}
		}
		bool flag = true;
		foreach (string text in list)
		{
			flag &= this.ConvertColonyToCloud(text);
		}
		if (flag)
		{
			string text2 = UI.PLATFORMS.STEAM;
			this.ShowSimpleDialog(UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_TO_CLOUD_SUCCESS.Replace("{Client}", text2));
		}
		this.RefreshColonyList();
		MainMenu.Instance.RefreshResumeButton(false);
		SaveLoader.SetCloudSavesDefault(true);
	}

	// Token: 0x06005E2F RID: 24111 RVA: 0x00227388 File Offset: 0x00225588
	private void ConvertAllToCloud()
	{
		string text = string.Format("{0}\n{1}\n", UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD_DETAILS, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_WARNING);
		KPlayerPrefs.SetInt("LoadScreenCloudTutorialTimes", 5);
		this.ConfirmCloudSaveMigrations(text, UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_COLONIES, UI.FRONTEND.LOADSCREEN.OPEN_SAVE_FOLDER, delegate
		{
			this.DoConvertAllToCloud();
		}, delegate
		{
			App.OpenWebURL(SaveLoader.GetSavePrefix());
		}, this.localToCloudSprite);
	}

	// Token: 0x06005E30 RID: 24112 RVA: 0x0022740C File Offset: 0x0022560C
	private void ConvertAllToLocal()
	{
		string text = string.Format("{0}\n{1}\n", UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL_DETAILS, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_WARNING);
		KPlayerPrefs.SetInt("LoadScreenCloudTutorialTimes", 5);
		this.ConfirmCloudSaveMigrations(text, UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL, UI.FRONTEND.LOADSCREEN.CONVERT_ALL_COLONIES, UI.FRONTEND.LOADSCREEN.OPEN_SAVE_FOLDER, delegate
		{
			this.DoConvertAllToLocal();
		}, delegate
		{
			App.OpenWebURL(SaveLoader.GetCloudSavePrefix());
		}, this.cloudToLocalSprite);
	}

	// Token: 0x06005E31 RID: 24113 RVA: 0x00227490 File Offset: 0x00225690
	private void ShowSaveInfo()
	{
		if (this.infoScreen == null)
		{
			this.infoScreen = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.SAVE_INFO_DIALOG_TITLE).AddSprite(this.infoSprite)
				.AddPlainText(UI.FRONTEND.LOADSCREEN.SAVE_INFO_DIALOG_TEXT)
				.AddOption(UI.FRONTEND.LOADSCREEN.OPEN_SAVE_FOLDER, delegate(InfoDialogScreen d)
				{
					App.OpenWebURL(SaveLoader.GetSavePrefix());
				}, true)
				.AddDefaultCancel();
			string cloudRoot = SaveLoader.GetCloudSavePrefix();
			if (cloudRoot != null && this.CloudSavesVisible())
			{
				this.infoScreen.AddOption(UI.FRONTEND.LOADSCREEN.OPEN_CLOUDSAVE_FOLDER, delegate(InfoDialogScreen d)
				{
					App.OpenWebURL(cloudRoot);
				}, true);
			}
			this.infoScreen.gameObject.SetActive(true);
		}
	}

	// Token: 0x06005E32 RID: 24114 RVA: 0x00227581 File Offset: 0x00225781
	protected override void OnDeactivate()
	{
		if (SpeedControlScreen.Instance != null)
		{
			SpeedControlScreen.Instance.Unpause(false);
		}
		this.selectedSave = null;
		base.OnDeactivate();
	}

	// Token: 0x06005E33 RID: 24115 RVA: 0x002275A8 File Offset: 0x002257A8
	private void ShowColonyList()
	{
		this.colonyListRoot.SetActive(true);
		this.colonyViewRoot.SetActive(false);
		this.currentColony = null;
		this.selectedSave = null;
	}

	// Token: 0x06005E34 RID: 24116 RVA: 0x002275D0 File Offset: 0x002257D0
	private bool CheckSaveVersion(LoadScreen.SaveGameFileDetails save, LocText display)
	{
		if (LoadScreen.IsSaveFileFromUnsupportedFutureBuild(save.FileHeader, save.FileInfo))
		{
			if (display != null)
			{
				display.text = string.Format(UI.FRONTEND.LOADSCREEN.SAVE_TOO_NEW, new object[]
				{
					save.FileName,
					save.FileHeader.buildVersion,
					save.FileInfo.saveMinorVersion,
					679336U,
					36
				});
			}
			return false;
		}
		if (save.FileInfo.saveMajorVersion < 7)
		{
			if (display != null)
			{
				display.text = string.Format(UI.FRONTEND.LOADSCREEN.UNSUPPORTED_SAVE_VERSION, new object[]
				{
					save.FileName,
					save.FileInfo.saveMajorVersion,
					save.FileInfo.saveMinorVersion,
					7,
					36
				});
			}
			return false;
		}
		return true;
	}

	// Token: 0x06005E35 RID: 24117 RVA: 0x002276D4 File Offset: 0x002258D4
	private bool CheckSaveDLCsCompatable(LoadScreen.SaveGameFileDetails save)
	{
		HashSet<string> hashSet;
		HashSet<string> hashSet2;
		return save.FileInfo.IsCompatableWithCurrentDlcConfiguration(out hashSet, out hashSet2);
	}

	// Token: 0x06005E36 RID: 24118 RVA: 0x002276F4 File Offset: 0x002258F4
	private string GetSaveDLCIncompatabilityTooltip(LoadScreen.SaveGameFileDetails save)
	{
		string text = "";
		HashSet<string> hashSet;
		HashSet<string> hashSet2;
		if (save.FileInfo.IsCompatableWithCurrentDlcConfiguration(out hashSet, out hashSet2))
		{
			text = null;
		}
		else
		{
			text = UI.FRONTEND.LOADSCREEN.TOOLTIP_SAVE_INCOMPATABLE_DLC_CONFIGURATION;
			foreach (string text2 in hashSet)
			{
				text = text + "\n" + string.Format(UI.FRONTEND.LOADSCREEN.TOOLTIP_SAVE_INCOMPATABLE_DLC_CONFIGURATION_ASK_TO_ENABLE, DlcManager.GetDlcTitle(text2));
			}
			foreach (string text3 in hashSet2)
			{
				text = text + "\n" + string.Format(UI.FRONTEND.LOADSCREEN.TOOLTIP_SAVE_INCOMPATABLE_DLC_CONFIGURATION_ASK_TO_DISABLE, DlcManager.GetDlcTitle(text3));
			}
		}
		return text;
	}

	// Token: 0x06005E37 RID: 24119 RVA: 0x002277E4 File Offset: 0x002259E4
	private void ShowColonySave(LoadScreen.SaveGameFileDetails save)
	{
		HierarchyReferences component = this.colonyViewRoot.GetComponent<HierarchyReferences>();
		component.GetReference<RectTransform>("Title").GetComponent<LocText>().text = save.BaseName;
		component.GetReference<RectTransform>("Date").GetComponent<LocText>().text = string.Format("{0:H:mm:ss} - " + Localization.GetFileDateFormat(0), save.FileDate.ToLocalTime());
		string text = save.FileInfo.clusterId;
		if (text != null && !SettingsCache.clusterLayouts.clusterCache.ContainsKey(text))
		{
			string text2 = SettingsCache.GetScope("EXPANSION1_ID") + text;
			if (SettingsCache.clusterLayouts.clusterCache.ContainsKey(text2))
			{
				text = text2;
			}
			else
			{
				DebugUtil.LogWarningArgs(new object[] { "Failed to find cluster " + text + " including the scoped path, setting to default cluster name." });
				global::Debug.Log("ClusterCache: " + string.Join(",", SettingsCache.clusterLayouts.clusterCache.Keys));
				text = WorldGenSettings.ClusterDefaultName;
			}
		}
		global::ProcGen.World world = ((text != null) ? SettingsCache.clusterLayouts.GetWorldData(text, 0) : null);
		string text3 = ((world != null) ? Strings.Get(world.name) : " - ");
		component.GetReference<LocText>("InfoWorld").text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_INFO_FMT, UI.FRONTEND.LOADSCREEN.WORLD_NAME, text3);
		component.GetReference<LocText>("InfoCycles").text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_INFO_FMT, UI.FRONTEND.LOADSCREEN.CYCLES_SURVIVED, save.FileInfo.numberOfCycles);
		component.GetReference<LocText>("InfoDupes").text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_INFO_FMT, UI.FRONTEND.LOADSCREEN.DUPLICANTS_ALIVE, save.FileInfo.numberOfDuplicants);
		TMP_Text component2 = component.GetReference<RectTransform>("FileSize").GetComponent<LocText>();
		string formattedBytes = GameUtil.GetFormattedBytes((ulong)save.Size);
		component2.text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_FILE_SIZE, formattedBytes);
		component.GetReference<RectTransform>("Filename").GetComponent<LocText>().text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_FILE_NAME, global::System.IO.Path.GetFileName(save.FileName));
		LocText component3 = component.GetReference<RectTransform>("AutoInfo").GetComponent<LocText>();
		component3.gameObject.SetActive(!this.CheckSaveVersion(save, component3));
		Image component4 = component.GetReference<RectTransform>("Preview").GetComponent<Image>();
		this.SetPreview(save.FileName, save.BaseName, component4, false);
		KButton component5 = component.GetReference<RectTransform>("DeleteButton").GetComponent<KButton>();
		component5.ClearOnClick();
		global::System.Action <>9__1;
		component5.onClick += delegate
		{
			LoadScreen <>4__this = this;
			global::System.Action action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate
				{
					int num = this.currentColony.IndexOf(save);
					this.currentColony.Remove(save);
					this.ShowColony(this.currentColony, num - 1);
				});
			}
			<>4__this.Delete(action);
		};
	}

	// Token: 0x06005E38 RID: 24120 RVA: 0x00227ACC File Offset: 0x00225CCC
	private void ShowColony(List<LoadScreen.SaveGameFileDetails> saves, int selectIndex = -1)
	{
		if (saves.Count <= 0)
		{
			this.RefreshColonyList();
			this.ShowColonyList();
			return;
		}
		this.currentColony = saves;
		this.colonyListRoot.SetActive(false);
		this.colonyViewRoot.SetActive(true);
		string baseName = saves[0].BaseName;
		HierarchyReferences component = this.colonyViewRoot.GetComponent<HierarchyReferences>();
		KButton component2 = component.GetReference<RectTransform>("Back").GetComponent<KButton>();
		component2.ClearOnClick();
		component2.onClick += delegate
		{
			this.ShowColonyList();
		};
		component.GetReference<RectTransform>("ColonyTitle").GetComponent<LocText>().text = string.Format(UI.FRONTEND.LOADSCREEN.COLONY_TITLE, baseName);
		GameObject gameObject = component.GetReference<RectTransform>("Content").gameObject;
		RectTransform reference = component.GetReference<RectTransform>("SaveTemplate");
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
			if (gameObject2 != null && gameObject2.name.Contains("Clone"))
			{
				global::UnityEngine.Object.Destroy(gameObject2);
			}
		}
		if (selectIndex < 0)
		{
			selectIndex = 0;
		}
		if (selectIndex > saves.Count - 1)
		{
			selectIndex = saves.Count - 1;
		}
		for (int j = 0; j < saves.Count; j++)
		{
			LoadScreen.SaveGameFileDetails save = saves[j];
			RectTransform rectTransform = global::UnityEngine.Object.Instantiate<RectTransform>(reference, gameObject.transform);
			HierarchyReferences component3 = rectTransform.GetComponent<HierarchyReferences>();
			rectTransform.gameObject.SetActive(true);
			component3.GetReference<RectTransform>("AutoLabel").gameObject.SetActive(save.FileInfo.isAutoSave);
			component3.GetReference<RectTransform>("SaveText").GetComponent<LocText>().text = global::System.IO.Path.GetFileNameWithoutExtension(save.FileName);
			component3.GetReference<RectTransform>("DateText").GetComponent<LocText>().text = string.Format("{0:H:mm:ss} - " + Localization.GetFileDateFormat(0), save.FileDate.ToLocalTime());
			component3.GetReference<RectTransform>("NewestLabel").gameObject.SetActive(j == 0);
			RectTransform reference2 = component3.GetReference<RectTransform>("DLCIconPrefab");
			foreach (string text in DlcManager.RELEASED_VERSIONS)
			{
				if (save.FileInfo.dlcIds.Contains(text))
				{
					GameObject gameObject3 = global::Util.KInstantiateUI(reference2.gameObject, reference2.transform.parent.gameObject, true);
					gameObject3.GetComponent<Image>().sprite = Assets.GetSprite(DlcManager.GetDlcSmallLogo(text));
					gameObject3.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.FRONTEND.LOADSCREEN.TOOLTIP_SAVE_USES_DLC, DlcManager.GetDlcTitle(text)));
				}
			}
			object obj = this.CheckSaveVersion(save, null) && this.CheckSaveDLCsCompatable(save);
			KButton button = rectTransform.GetComponent<KButton>();
			button.ClearOnClick();
			button.onClick += delegate
			{
				this.UpdateSelected(button, save.FileName, save.FileInfo.dlcIds);
				this.ShowColonySave(save);
			};
			object obj2 = obj;
			if (obj2 != null)
			{
				button.onDoubleClick += delegate
				{
					this.UpdateSelected(button, save.FileName, save.FileInfo.dlcIds);
					this.Load();
				};
			}
			KButton component4 = component3.GetReference<RectTransform>("LoadButton").GetComponent<KButton>();
			component4.ClearOnClick();
			if (obj2 == null)
			{
				component4.isInteractable = false;
				component4.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Disabled);
				component4.GetComponent<ToolTip>().SetSimpleTooltip(this.GetSaveDLCIncompatabilityTooltip(save));
			}
			else
			{
				component4.onClick += delegate
				{
					this.UpdateSelected(button, save.FileName, save.FileInfo.dlcIds);
					this.Load();
				};
			}
			if (j == selectIndex)
			{
				this.UpdateSelected(button, save.FileName, save.FileInfo.dlcIds);
				this.ShowColonySave(save);
			}
		}
	}

	// Token: 0x06005E39 RID: 24121 RVA: 0x00227ED0 File Offset: 0x002260D0
	private void AddColonyToList(List<LoadScreen.SaveGameFileDetails> saves)
	{
		if (saves.Count == 0)
		{
			return;
		}
		HierarchyReferences freeElement = this.colonyListPool.GetFreeElement(this.saveButtonRoot, true);
		saves.Sort((LoadScreen.SaveGameFileDetails x, LoadScreen.SaveGameFileDetails y) => y.FileDate.CompareTo(x.FileDate));
		LoadScreen.SaveGameFileDetails saveGameFileDetails = saves[0];
		string colonyName = saveGameFileDetails.BaseName;
		ValueTuple<int, int, ulong> savesSizeAndCounts = this.GetSavesSizeAndCounts(saves);
		int item = savesSizeAndCounts.Item1;
		int item2 = savesSizeAndCounts.Item2;
		string formattedBytes = GameUtil.GetFormattedBytes(savesSizeAndCounts.Item3);
		freeElement.GetReference<RectTransform>("HeaderTitle").GetComponent<LocText>().text = colonyName;
		freeElement.GetReference<RectTransform>("HeaderDate").GetComponent<LocText>().text = string.Format("{0:H:mm:ss} - " + Localization.GetFileDateFormat(0), saveGameFileDetails.FileDate.ToLocalTime());
		freeElement.GetReference<RectTransform>("SaveTitle").GetComponent<LocText>().text = string.Format(UI.FRONTEND.LOADSCREEN.SAVE_INFO, item, item2, formattedBytes);
		Image component = freeElement.GetReference<RectTransform>("Preview").GetComponent<Image>();
		this.SetPreview(saveGameFileDetails.FileName, colonyName, component, true);
		List<ValueTuple<Sprite, string>> list = new List<ValueTuple<Sprite, string>>();
		if (saveGameFileDetails.FileInfo.dlcIds.Contains("EXPANSION1_ID"))
		{
			list.Add(new ValueTuple<Sprite, string>(Assets.GetSprite(DlcManager.GetDlcSmallLogo("EXPANSION1_ID")), string.Format(UI.FRONTEND.LOADSCREEN.TOOLTIP_SAVE_USES_DLC, DlcManager.GetDlcTitle("EXPANSION1_ID"))));
		}
		foreach (string text in saveGameFileDetails.FileInfo.dlcIds)
		{
			if (DlcManager.IsDlcId(text) && !(text == "EXPANSION1_ID"))
			{
				list.Add(new ValueTuple<Sprite, string>(Assets.GetSprite(DlcManager.GetDlcSmallLogo(text)), string.Format(UI.FRONTEND.LOADSCREEN.TOOLTIP_SAVE_USES_DLC, DlcManager.GetDlcTitle(text))));
			}
		}
		GameObject gameObject = freeElement.transform.Find("Header").Find("DlcIcons").Find("Prefab_DlcIcon")
			.gameObject;
		gameObject.SetActive(false);
		for (int i = 0; i < gameObject.transform.parent.childCount; i++)
		{
			GameObject gameObject2 = gameObject.transform.parent.GetChild(i).gameObject;
			if (gameObject2 != gameObject)
			{
				global::UnityEngine.Object.Destroy(gameObject2);
			}
		}
		foreach (ValueTuple<Sprite, string> valueTuple in list)
		{
			GameObject gameObject3 = global::Util.KInstantiateUI(gameObject, gameObject.transform.parent.gameObject, true);
			Image component2 = gameObject3.GetComponent<Image>();
			ToolTip component3 = gameObject3.GetComponent<ToolTip>();
			component2.sprite = valueTuple.Item1;
			component3.SetSimpleTooltip(valueTuple.Item2);
		}
		Component reference = freeElement.GetReference<RectTransform>("LocationIcons");
		bool flag = this.CloudSavesVisible();
		reference.gameObject.SetActive(flag);
		if (flag)
		{
			LocText locationText = freeElement.GetReference<RectTransform>("LocationText").GetComponent<LocText>();
			bool isLocal = SaveLoader.IsSaveLocal(saveGameFileDetails.FileName);
			locationText.text = (isLocal ? UI.FRONTEND.LOADSCREEN.LOCAL_SAVE : UI.FRONTEND.LOADSCREEN.CLOUD_SAVE);
			KButton cloudButton = freeElement.GetReference<RectTransform>("CloudButton").GetComponent<KButton>();
			KButton localButton = freeElement.GetReference<RectTransform>("LocalButton").GetComponent<KButton>();
			cloudButton.gameObject.SetActive(!isLocal);
			cloudButton.ClearOnClick();
			global::System.Action <>9__4;
			cloudButton.onClick += delegate
			{
				string text2 = string.Format("{0}\n", UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL_DETAILS);
				LoadScreen <>4__this = this;
				string text3 = text2;
				string text4 = UI.FRONTEND.LOADSCREEN.CONVERT_TO_LOCAL;
				string text5 = UI.FRONTEND.LOADSCREEN.CONVERT_COLONY;
				string text6 = null;
				global::System.Action action;
				if ((action = <>9__4) == null)
				{
					action = (<>9__4 = delegate
					{
						cloudButton.gameObject.SetActive(false);
						isLocal = true;
						locationText.text = (isLocal ? UI.FRONTEND.LOADSCREEN.LOCAL_SAVE : UI.FRONTEND.LOADSCREEN.CLOUD_SAVE);
						this.ConvertColonyToLocal(colonyName);
						this.RefreshColonyList();
						MainMenu.Instance.RefreshResumeButton(false);
					});
				}
				<>4__this.ConfirmCloudSaveMigrations(text3, text4, text5, text6, action, null, this.cloudToLocalSprite);
			};
			localButton.gameObject.SetActive(isLocal);
			localButton.ClearOnClick();
			global::System.Action <>9__5;
			localButton.onClick += delegate
			{
				string text7 = string.Format("{0}\n", UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD_DETAILS);
				LoadScreen <>4__this2 = this;
				string text8 = text7;
				string text9 = UI.FRONTEND.LOADSCREEN.CONVERT_TO_CLOUD;
				string text10 = UI.FRONTEND.LOADSCREEN.CONVERT_COLONY;
				string text11 = null;
				global::System.Action action2;
				if ((action2 = <>9__5) == null)
				{
					action2 = (<>9__5 = delegate
					{
						localButton.gameObject.SetActive(false);
						isLocal = false;
						locationText.text = (isLocal ? UI.FRONTEND.LOADSCREEN.LOCAL_SAVE : UI.FRONTEND.LOADSCREEN.CLOUD_SAVE);
						this.ConvertColonyToCloud(colonyName);
						this.RefreshColonyList();
						MainMenu.Instance.RefreshResumeButton(false);
					});
				}
				<>4__this2.ConfirmCloudSaveMigrations(text8, text9, text10, text11, action2, null, this.localToCloudSprite);
			};
		}
		this.GetSaveDLCIncompatabilityTooltip(saveGameFileDetails);
		freeElement.GetReference<RectTransform>("Button").GetComponent<KButton>().onClick += delegate
		{
			this.ShowColony(saves, -1);
		};
		freeElement.transform.SetAsLastSibling();
	}

	// Token: 0x06005E3A RID: 24122 RVA: 0x00228370 File Offset: 0x00226570
	private void SetPreview(string filename, string basename, Image preview, bool fallbackToTimelapse = false)
	{
		preview.color = Color.black;
		preview.gameObject.SetActive(false);
		try
		{
			Sprite sprite = RetireColonyUtility.LoadColonyPreview(filename, basename, fallbackToTimelapse);
			if (!(sprite == null))
			{
				Rect rect = preview.rectTransform.parent.rectTransform().rect;
				preview.sprite = sprite;
				preview.color = (sprite ? Color.white : Color.black);
				float num = sprite.bounds.size.x / sprite.bounds.size.y;
				if ((double)num >= 1.77777777777778)
				{
					preview.rectTransform.sizeDelta = new Vector2(rect.height * num, rect.height);
				}
				else
				{
					preview.rectTransform.sizeDelta = new Vector2(rect.width, rect.width / num);
				}
				preview.gameObject.SetActive(true);
			}
		}
		catch (Exception ex)
		{
			global::Debug.Log(ex);
		}
	}

	// Token: 0x06005E3B RID: 24123 RVA: 0x00228480 File Offset: 0x00226680
	public static void ForceStopGame()
	{
		ThreadedHttps<KleiMetrics>.Instance.ClearGameFields();
		ThreadedHttps<KleiMetrics>.Instance.SendProfileStats();
		Game.Instance.SetIsLoading();
		Grid.CellCount = 0;
		Sim.Shutdown();
	}

	// Token: 0x06005E3C RID: 24124 RVA: 0x002284AC File Offset: 0x002266AC
	private static bool IsSaveFileFromUnsupportedFutureBuild(SaveGame.Header header, SaveGame.GameInfo gameInfo)
	{
		return gameInfo.saveMajorVersion > 7 || (gameInfo.saveMajorVersion == 7 && gameInfo.saveMinorVersion > 36) || header.buildVersion > 679336U;
	}

	// Token: 0x06005E3D RID: 24125 RVA: 0x002284DC File Offset: 0x002266DC
	private void UpdateSelected(KButton button, string filename, List<string> dlcIds)
	{
		if (this.selectedSave != null && this.selectedSave.button != null)
		{
			this.selectedSave.button.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Inactive);
		}
		if (this.selectedSave == null)
		{
			this.selectedSave = new LoadScreen.SelectedSave();
		}
		this.selectedSave.button = button;
		this.selectedSave.filename = filename;
		this.selectedSave.dlcIds = dlcIds;
		if (this.selectedSave.button != null)
		{
			this.selectedSave.button.GetComponent<ImageToggleState>().SetState(ImageToggleState.State.Active);
		}
	}

	// Token: 0x06005E3E RID: 24126 RVA: 0x0022857C File Offset: 0x0022677C
	private void Load()
	{
		if (!DlcManager.IsAllContentSubscribed(this.selectedSave.dlcIds))
		{
			string text = (this.selectedSave.dlcIds.Contains("") ? UI.FRONTEND.LOADSCREEN.VANILLA_RESTART : UI.FRONTEND.LOADSCREEN.EXPANSION1_RESTART);
			this.ConfirmDoAction(text, delegate
			{
				KPlayerPrefs.SetString("AutoResumeSaveFile", this.selectedSave.filename);
				DlcManager.ToggleDLC("EXPANSION1_ID");
			});
			return;
		}
		LoadingOverlay.Load(new global::System.Action(this.DoLoad));
	}

	// Token: 0x06005E3F RID: 24127 RVA: 0x002285E9 File Offset: 0x002267E9
	private void DoLoad()
	{
		if (this.selectedSave == null)
		{
			return;
		}
		LoadScreen.DoLoad(this.selectedSave.filename);
		this.Deactivate();
	}

	// Token: 0x06005E40 RID: 24128 RVA: 0x0022860C File Offset: 0x0022680C
	public static void DoLoad(string filename)
	{
		KCrashReporter.MOST_RECENT_SAVEFILE = filename;
		bool flag = true;
		SaveGame.Header header;
		SaveGame.GameInfo gameInfo = SaveLoader.LoadHeader(filename, out header);
		string text = null;
		string text2 = null;
		if (header.buildVersion > 679336U)
		{
			text = header.buildVersion.ToString();
			text2 = 679336U.ToString();
		}
		else if (gameInfo.saveMajorVersion < 7)
		{
			text = string.Format("v{0}.{1}", gameInfo.saveMajorVersion, gameInfo.saveMinorVersion);
			text2 = string.Format("v{0}.{1}", 7, 36);
		}
		if (!flag)
		{
			GameObject gameObject = ((FrontEndManager.Instance == null) ? GameScreenManager.Instance.ssOverlayCanvas : FrontEndManager.Instance.gameObject);
			global::Util.KInstantiateUI(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, gameObject, true).GetComponent<ConfirmDialogScreen>().PopupConfirmDialog(string.Format(UI.CRASHSCREEN.LOADFAILED, "Version Mismatch", text, text2), null, null, null, null, null, null, null, null);
			return;
		}
		if (Game.Instance != null)
		{
			LoadScreen.ForceStopGame();
		}
		SaveLoader.SetActiveSaveFilePath(filename);
		Time.timeScale = 0f;
		App.LoadScene("backend");
	}

	// Token: 0x06005E41 RID: 24129 RVA: 0x0022872D File Offset: 0x0022692D
	private void MoreInfo()
	{
		App.OpenWebURL("http://support.kleientertainment.com/customer/portal/articles/2776550");
	}

	// Token: 0x06005E42 RID: 24130 RVA: 0x0022873C File Offset: 0x0022693C
	private void Delete(global::System.Action onDelete)
	{
		if (this.selectedSave == null || string.IsNullOrEmpty(this.selectedSave.filename))
		{
			global::Debug.LogError("The path provided is not valid and cannot be deleted.");
			return;
		}
		this.ConfirmDoAction(string.Format(UI.FRONTEND.LOADSCREEN.CONFIRMDELETE, global::System.IO.Path.GetFileName(this.selectedSave.filename)), delegate
		{
			try
			{
				this.DeleteFileAndEmptyFolder(this.selectedSave.filename);
				string text = global::System.IO.Path.ChangeExtension(this.selectedSave.filename, "png");
				this.DeleteFileAndEmptyFolder(text);
				if (onDelete != null)
				{
					onDelete();
				}
				MainMenu.Instance.RefreshResumeButton(false);
			}
			catch (SystemException ex)
			{
				global::Debug.LogError(ex.ToString());
			}
		});
	}

	// Token: 0x06005E43 RID: 24131 RVA: 0x002287B3 File Offset: 0x002269B3
	private void ShowSimpleDialog(string title, string message)
	{
		global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(title).AddPlainText(message)
			.AddDefaultOK(false)
			.Activate();
	}

	// Token: 0x06005E44 RID: 24132 RVA: 0x002287E8 File Offset: 0x002269E8
	private void ConfirmCloudSaveMigrations(string message, string title, string confirmText, string backupText, global::System.Action commitAction, global::System.Action backupAction, Sprite sprite)
	{
		global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(title).AddSprite(sprite)
			.AddPlainText(message)
			.AddDefaultCancel()
			.AddOption(confirmText, delegate(InfoDialogScreen d)
			{
				d.Deactivate();
				commitAction();
			}, true)
			.Activate();
	}

	// Token: 0x06005E45 RID: 24133 RVA: 0x00228850 File Offset: 0x00226A50
	private void ShowConvertError(string message)
	{
		if (this.errorInfoScreen == null)
		{
			if (DistributionPlatform.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
			{
				this.errorInfoScreen = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.CONVERT_ERROR_TITLE).AddSprite(this.errorSprite)
					.AddPlainText(message)
					.AddDefaultOK(false);
				this.errorInfoScreen.Activate();
				return;
			}
			this.errorInfoScreen = global::Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.gameObject, false).SetHeader(UI.FRONTEND.LOADSCREEN.CONVERT_ERROR_TITLE).AddSprite(this.errorSprite)
				.AddPlainText(message)
				.AddOption(UI.FRONTEND.LOADSCREEN.MIGRATE_FAILURES_FORUM_BUTTON, delegate(InfoDialogScreen d)
				{
					App.OpenWebURL("https://forums.kleientertainment.com/klei-bug-tracker/oni/");
				}, false)
				.AddDefaultOK(false);
			this.errorInfoScreen.Activate();
		}
	}

	// Token: 0x06005E46 RID: 24134 RVA: 0x00228950 File Offset: 0x00226B50
	private void ConfirmDoAction(string message, global::System.Action action)
	{
		if (this.confirmScreen == null)
		{
			this.confirmScreen = global::Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.gameObject, false);
			this.confirmScreen.PopupConfirmDialog(message, action, delegate
			{
			}, null, null, null, null, null, null);
			this.confirmScreen.gameObject.SetActive(true);
		}
	}

	// Token: 0x06005E47 RID: 24135 RVA: 0x002289CF File Offset: 0x00226BCF
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.currentColony != null && e.TryConsume(global::Action.Escape))
		{
			this.ShowColonyList();
		}
		base.OnKeyDown(e);
	}

	// Token: 0x04003EA5 RID: 16037
	private const int MAX_CLOUD_TUTORIALS = 5;

	// Token: 0x04003EA6 RID: 16038
	private const string CLOUD_TUTORIAL_KEY = "LoadScreenCloudTutorialTimes";

	// Token: 0x04003EA7 RID: 16039
	private const int ITEMS_PER_PAGE = 20;

	// Token: 0x04003EA8 RID: 16040
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003EA9 RID: 16041
	[SerializeField]
	private GameObject saveButtonRoot;

	// Token: 0x04003EAA RID: 16042
	[SerializeField]
	private GameObject colonyListRoot;

	// Token: 0x04003EAB RID: 16043
	[SerializeField]
	private GameObject colonyViewRoot;

	// Token: 0x04003EAC RID: 16044
	[SerializeField]
	private HierarchyReferences migrationPanelRefs;

	// Token: 0x04003EAD RID: 16045
	[SerializeField]
	private HierarchyReferences saveButtonPrefab;

	// Token: 0x04003EAE RID: 16046
	[SerializeField]
	private KButton loadMoreButton;

	// Token: 0x04003EAF RID: 16047
	[Space]
	[SerializeField]
	private KButton colonyCloudButton;

	// Token: 0x04003EB0 RID: 16048
	[SerializeField]
	private KButton colonyLocalButton;

	// Token: 0x04003EB1 RID: 16049
	[SerializeField]
	private KButton colonyInfoButton;

	// Token: 0x04003EB2 RID: 16050
	[SerializeField]
	private Sprite localToCloudSprite;

	// Token: 0x04003EB3 RID: 16051
	[SerializeField]
	private Sprite cloudToLocalSprite;

	// Token: 0x04003EB4 RID: 16052
	[SerializeField]
	private Sprite errorSprite;

	// Token: 0x04003EB5 RID: 16053
	[SerializeField]
	private Sprite infoSprite;

	// Token: 0x04003EB6 RID: 16054
	[SerializeField]
	private Bouncer cloudTutorialBouncer;

	// Token: 0x04003EB7 RID: 16055
	public bool requireConfirmation = true;

	// Token: 0x04003EB8 RID: 16056
	private LoadScreen.SelectedSave selectedSave;

	// Token: 0x04003EB9 RID: 16057
	private List<LoadScreen.SaveGameFileDetails> currentColony;

	// Token: 0x04003EBA RID: 16058
	private UIPool<HierarchyReferences> colonyListPool;

	// Token: 0x04003EBB RID: 16059
	private ConfirmDialogScreen confirmScreen;

	// Token: 0x04003EBC RID: 16060
	private InfoDialogScreen infoScreen;

	// Token: 0x04003EBD RID: 16061
	private InfoDialogScreen errorInfoScreen;

	// Token: 0x04003EBE RID: 16062
	private ConfirmDialogScreen errorScreen;

	// Token: 0x04003EBF RID: 16063
	private InspectSaveScreen inspectScreenInstance;

	// Token: 0x04003EC0 RID: 16064
	private int displayedPageCount = 1;

	// Token: 0x02001D65 RID: 7525
	private struct SaveGameFileDetails
	{
		// Token: 0x0400892F RID: 35119
		public string BaseName;

		// Token: 0x04008930 RID: 35120
		public string FileName;

		// Token: 0x04008931 RID: 35121
		public string UniqueID;

		// Token: 0x04008932 RID: 35122
		public global::System.DateTime FileDate;

		// Token: 0x04008933 RID: 35123
		public SaveGame.Header FileHeader;

		// Token: 0x04008934 RID: 35124
		public SaveGame.GameInfo FileInfo;

		// Token: 0x04008935 RID: 35125
		public long Size;
	}

	// Token: 0x02001D66 RID: 7526
	private class SelectedSave
	{
		// Token: 0x04008936 RID: 35126
		public string filename;

		// Token: 0x04008937 RID: 35127
		public List<string> dlcIds;

		// Token: 0x04008938 RID: 35128
		public KButton button;
	}
}
