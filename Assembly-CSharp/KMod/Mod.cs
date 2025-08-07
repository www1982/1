using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Klei;
using Newtonsoft.Json;
using UnityEngine;

namespace KMod
{
	// Token: 0x02000F77 RID: 3959
	[JsonObject(MemberSerialization.OptIn)]
	[DebuggerDisplay("{title}")]
	public class Mod : IHasDlcRestrictions
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06007BA5 RID: 31653 RVA: 0x003120C1 File Offset: 0x003102C1
		// (set) Token: 0x06007BA6 RID: 31654 RVA: 0x003120C9 File Offset: 0x003102C9
		public Content available_content { get; private set; }

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06007BA7 RID: 31655 RVA: 0x003120D2 File Offset: 0x003102D2
		// (set) Token: 0x06007BA8 RID: 31656 RVA: 0x003120DA File Offset: 0x003102DA
		[JsonProperty]
		public string staticID { get; private set; }

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06007BA9 RID: 31657 RVA: 0x003120E3 File Offset: 0x003102E3
		// (set) Token: 0x06007BAA RID: 31658 RVA: 0x003120EB File Offset: 0x003102EB
		public LocString manage_tooltip { get; private set; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06007BAB RID: 31659 RVA: 0x003120F4 File Offset: 0x003102F4
		// (set) Token: 0x06007BAC RID: 31660 RVA: 0x003120FC File Offset: 0x003102FC
		public global::System.Action on_managed { get; private set; }

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06007BAD RID: 31661 RVA: 0x00312105 File Offset: 0x00310305
		public bool is_managed
		{
			get
			{
				return this.manage_tooltip != null;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06007BAE RID: 31662 RVA: 0x00312110 File Offset: 0x00310310
		// (set) Token: 0x06007BAF RID: 31663 RVA: 0x0031211D File Offset: 0x0031031D
		public string title
		{
			get
			{
				return this.label.title;
			}
			set
			{
				this.label.title = value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06007BB0 RID: 31664 RVA: 0x0031212B File Offset: 0x0031032B
		// (set) Token: 0x06007BB1 RID: 31665 RVA: 0x00312133 File Offset: 0x00310333
		public string description { get; set; }

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06007BB2 RID: 31666 RVA: 0x0031213C File Offset: 0x0031033C
		// (set) Token: 0x06007BB3 RID: 31667 RVA: 0x00312144 File Offset: 0x00310344
		public Content loaded_content { get; private set; }

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06007BB4 RID: 31668 RVA: 0x0031214D File Offset: 0x0031034D
		// (set) Token: 0x06007BB5 RID: 31669 RVA: 0x00312155 File Offset: 0x00310355
		public IFileSource file_source
		{
			get
			{
				return this._fileSource;
			}
			set
			{
				if (this._fileSource != null)
				{
					this._fileSource.Dispose();
				}
				this._fileSource = value;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06007BB6 RID: 31670 RVA: 0x00312171 File Offset: 0x00310371
		// (set) Token: 0x06007BB7 RID: 31671 RVA: 0x00312179 File Offset: 0x00310379
		public bool DevModCrashTriggered { get; private set; }

		// Token: 0x06007BB8 RID: 31672 RVA: 0x00312182 File Offset: 0x00310382
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007BB9 RID: 31673 RVA: 0x0031218A File Offset: 0x0031038A
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x06007BBA RID: 31674 RVA: 0x00312192 File Offset: 0x00310392
		[JsonConstructor]
		public Mod()
		{
		}

		// Token: 0x06007BBB RID: 31675 RVA: 0x003121A8 File Offset: 0x003103A8
		public void CopyPersistentDataTo(Mod other_mod)
		{
			other_mod.status = this.status;
			other_mod.enabledForDlc = ((this.enabledForDlc != null) ? new List<string>(this.enabledForDlc) : new List<string>());
			other_mod.crash_count = this.crash_count;
			other_mod.loaded_content = this.loaded_content;
			other_mod.loaded_mod_data = this.loaded_mod_data;
			other_mod.reinstall_path = this.reinstall_path;
		}

		// Token: 0x06007BBC RID: 31676 RVA: 0x00312214 File Offset: 0x00310414
		public Mod(Label label, string staticID, string description, IFileSource file_source, LocString manage_tooltip, global::System.Action on_managed)
		{
			this.label = label;
			this.status = Mod.Status.NotInstalled;
			this.staticID = staticID;
			this.description = description;
			this.file_source = file_source;
			this.manage_tooltip = manage_tooltip;
			this.on_managed = on_managed;
			this.loaded_content = (Content)0;
			this.available_content = (Content)0;
			this.ScanContent();
		}

		// Token: 0x06007BBD RID: 31677 RVA: 0x0031227A File Offset: 0x0031047A
		public bool IsEnabledForActiveDlc()
		{
			return this.IsEnabledForDlc(DlcManager.GetHighestActiveDlcId());
		}

		// Token: 0x06007BBE RID: 31678 RVA: 0x00312287 File Offset: 0x00310487
		public bool IsEnabledForDlc(string dlcId)
		{
			return this.enabledForDlc != null && this.enabledForDlc.Contains(dlcId);
		}

		// Token: 0x06007BBF RID: 31679 RVA: 0x0031229F File Offset: 0x0031049F
		public void SetEnabledForActiveDlc(bool enabled)
		{
			this.SetEnabledForDlc(DlcManager.GetHighestActiveDlcId(), enabled);
		}

		// Token: 0x06007BC0 RID: 31680 RVA: 0x003122B0 File Offset: 0x003104B0
		public void SetEnabledForDlc(string dlcId, bool set_enabled)
		{
			if (this.enabledForDlc == null)
			{
				this.enabledForDlc = new List<string>();
			}
			bool flag = this.enabledForDlc.Contains(dlcId);
			if (set_enabled && !flag)
			{
				this.enabledForDlc.Add(dlcId);
				return;
			}
			if (!set_enabled && flag)
			{
				this.enabledForDlc.Remove(dlcId);
			}
		}

		// Token: 0x06007BC1 RID: 31681 RVA: 0x00312308 File Offset: 0x00310508
		public void ScanContent()
		{
			this.ModDevLog(string.Format("{0} ({1}): Setting up mod.", this.label, this.label.id));
			this.available_content = (Content)0;
			if (this.file_source == null)
			{
				if (this.label.id.EndsWith(".zip"))
				{
					DebugUtil.DevAssert(false, "Does this actually get used ever?", null);
					this.file_source = new ZipFile(this.label.install_path);
				}
				else
				{
					this.file_source = new Directory(this.label.install_path);
				}
			}
			if (!this.file_source.Exists())
			{
				global::Debug.LogWarning(string.Format("{0}: File source does not appear to be valid, skipping. ({1})", this.label, this.label.install_path));
				return;
			}
			KModHeader header = KModUtil.GetHeader(this.file_source, this.label.defaultStaticID, this.label.title, this.description, this.IsDev);
			if (this.label.title != header.title)
			{
				global::Debug.Log(string.Concat(new string[]
				{
					"\t",
					this.label.title,
					" has a mod.yaml with the title `",
					header.title,
					"`, using that from now on."
				}));
			}
			if (this.label.defaultStaticID != header.staticID)
			{
				global::Debug.Log(string.Concat(new string[]
				{
					"\t",
					this.label.title,
					" has a mod.yaml with a staticID `",
					header.staticID,
					"`, using that from now on."
				}));
			}
			this.label.title = header.title;
			this.staticID = header.staticID;
			this.description = header.description;
			Mod.ArchivedVersion mostSuitableArchive = this.GetMostSuitableArchive();
			if (mostSuitableArchive == null)
			{
				global::Debug.LogWarning(string.Format("{0}: No archive supports this game version, skipping content.", this.label));
				this.contentCompatability = ModContentCompatability.DoesntSupportDLCConfig;
				this.available_content = (Content)0;
				this.SetEnabledForActiveDlc(false);
				return;
			}
			this.packagedModInfo = mostSuitableArchive.info;
			Content content;
			this.ScanContentFromSource(mostSuitableArchive.relativePath, out content);
			if (content == (Content)0)
			{
				global::Debug.LogWarning(string.Format("{0}: No supported content for mod, skipping content.", this.label));
				this.contentCompatability = ModContentCompatability.NoContent;
				this.available_content = (Content)0;
				this.SetEnabledForActiveDlc(false);
				return;
			}
			bool flag = mostSuitableArchive.info.APIVersion == 2;
			if ((content & Content.DLL) != (Content)0 && !flag)
			{
				global::Debug.LogWarning(string.Format("{0}: DLLs found but not using the correct API version.", this.label));
				this.contentCompatability = ModContentCompatability.OldAPI;
				this.available_content = (Content)0;
				this.SetEnabledForActiveDlc(false);
				return;
			}
			this.contentCompatability = ModContentCompatability.OK;
			this.available_content = content;
			this.relative_root = mostSuitableArchive.relativePath;
			global::Debug.Assert(this.content_source == null);
			this.content_source = new Directory(this.ContentPath);
			string text = (string.IsNullOrEmpty(this.relative_root) ? "root" : this.relative_root);
			global::Debug.Log(string.Format("{0}: Successfully loaded from path '{1}' with content '{2}'.", this.label, text, this.available_content.ToString()));
		}

		// Token: 0x06007BC2 RID: 31682 RVA: 0x00312638 File Offset: 0x00310838
		private Mod.ArchivedVersion GetMostSuitableArchive()
		{
			Mod.PackagedModInfo packagedModInfo = this.GetModInfoForFolder("");
			if (packagedModInfo == null)
			{
				if (!this.ScanContentFromSourceForTranslationsOnly(""))
				{
					global::Debug.Log(string.Format("{0}: Is missing a mod_info.yaml file and will not be loaded, which is required. See the stickied post in the Mods and Tools section on the Klei forums.", this.label));
					return null;
				}
				this.ModDevLogWarning(string.Format("{0}: No mod_info.yaml found, but since it's a translation we will load it.", this.label));
				packagedModInfo = new Mod.PackagedModInfo
				{
					minimumSupportedBuild = 0
				};
			}
			this.requiredDlcIds = packagedModInfo.requiredDlcIds;
			this.forbiddenDlcIds = packagedModInfo.forbiddenDlcIds;
			Mod.ArchivedVersion archivedVersion = new Mod.ArchivedVersion
			{
				relativePath = "",
				info = packagedModInfo
			};
			if (!this.file_source.Exists("archived_versions"))
			{
				this.ModDevLog(string.Format("\t{0}: No archived_versions for this mod, using root version directly.", this.label));
				if (!DlcManager.IsCorrectDlcSubscribed(packagedModInfo))
				{
					return null;
				}
				return archivedVersion;
			}
			else
			{
				List<FileSystemItem> list = new List<FileSystemItem>();
				this.file_source.GetTopLevelItems(list, "archived_versions");
				if (list.Count == 0)
				{
					this.ModDevLog(string.Format("\t{0}: No archived_versions for this mod, using root version directly.", this.label));
					if (!DlcManager.IsCorrectDlcSubscribed(packagedModInfo))
					{
						return null;
					}
					return archivedVersion;
				}
				else
				{
					List<Mod.ArchivedVersion> list2 = new List<Mod.ArchivedVersion>();
					list2.Add(archivedVersion);
					foreach (FileSystemItem fileSystemItem in list)
					{
						if (fileSystemItem.type != FileSystemItem.ItemType.File)
						{
							string text = Path.Combine("archived_versions", fileSystemItem.name);
							Mod.PackagedModInfo modInfoForFolder = this.GetModInfoForFolder(text);
							if (modInfoForFolder != null)
							{
								list2.Add(new Mod.ArchivedVersion
								{
									relativePath = text,
									info = modInfoForFolder
								});
							}
						}
					}
					list2 = list2.Where((Mod.ArchivedVersion v) => DlcManager.IsCorrectDlcSubscribed(v.info)).ToList<Mod.ArchivedVersion>();
					list2 = list2.Where((Mod.ArchivedVersion v) => v.info.APIVersion == 2 || v.info.APIVersion == 0).ToList<Mod.ArchivedVersion>();
					Mod.ArchivedVersion archivedVersion2 = (from v in list2
						where (long)v.info.minimumSupportedBuild <= 679336L
						orderby v.info.minimumSupportedBuild descending
						select v).FirstOrDefault<Mod.ArchivedVersion>();
					if (archivedVersion2 != null)
					{
						this.requiredDlcIds = archivedVersion2.info.requiredDlcIds;
						this.forbiddenDlcIds = archivedVersion2.info.forbiddenDlcIds;
					}
					if (archivedVersion2 == null)
					{
						return null;
					}
					return archivedVersion2;
				}
			}
		}

		// Token: 0x06007BC3 RID: 31683 RVA: 0x003128BC File Offset: 0x00310ABC
		private Mod.PackagedModInfo GetModInfoForFolder(string relative_root)
		{
			List<FileSystemItem> list = new List<FileSystemItem>();
			this.file_source.GetTopLevelItems(list, relative_root);
			bool flag = false;
			foreach (FileSystemItem fileSystemItem in list)
			{
				if (fileSystemItem.type == FileSystemItem.ItemType.File && fileSystemItem.name.ToLower() == "mod_info.yaml")
				{
					flag = true;
					break;
				}
			}
			string text = (string.IsNullOrEmpty(relative_root) ? "root" : relative_root);
			if (!flag)
			{
				this.ModDevLogWarning(string.Concat(new string[] { "\t", this.title, ": has no mod_info.yaml in folder '", text, "'" }));
				return null;
			}
			string text2 = this.file_source.Read(Path.Combine(relative_root, "mod_info.yaml"));
			if (string.IsNullOrEmpty(text2))
			{
				this.ModDevLogError(string.Format("\t{0}: Failed to read {1} in folder '{2}', skipping", this.label, "mod_info.yaml", text));
				return null;
			}
			YamlIO.ErrorHandler errorHandler = delegate(YamlIO.Error e, bool force_warning)
			{
				YamlIO.LogError(e, !this.IsDev);
			};
			Mod.PackagedModInfo packagedModInfo = YamlIO.Parse<Mod.PackagedModInfo>(text2, default(FileHandle), errorHandler, null);
			if (packagedModInfo == null)
			{
				this.ModDevLogError(string.Format("\t{0}: Failed to parse {1} in folder '{2}', text is {3}", new object[] { this.label, "mod_info.yaml", text, text2 }));
				return null;
			}
			if (packagedModInfo.supportedContent != null && packagedModInfo.requiredDlcIds == null && packagedModInfo.forbiddenDlcIds == null)
			{
				packagedModInfo.supportedContent = packagedModInfo.supportedContent.ToUpperInvariant();
				this.ModDevLogWarning(string.Format("\t{0}: {1} in folder '{2}' is using supportedContent which has been deprecated. See stickied post on the Klei forums.", this.label, "mod_info.yaml", text));
				bool flag2 = packagedModInfo.supportedContent.Contains("ALL");
				bool flag3 = packagedModInfo.supportedContent.Contains("VANILLA_ID");
				bool flag4 = packagedModInfo.supportedContent.Contains("EXPANSION1_ID");
				if (flag2)
				{
					packagedModInfo.requiredDlcIds = null;
					packagedModInfo.forbiddenDlcIds = null;
				}
				else
				{
					string text3 = "\\b\\w+_ID\\b";
					List<string> list2 = new List<string>();
					foreach (object obj in Regex.Matches(packagedModInfo.supportedContent, text3))
					{
						Match match = (Match)obj;
						if (!(match.Value == "VANILLA_ID") && (!(match.Value == "EXPANSION1_ID") || !flag3))
						{
							if (match.Value != "EXPANSION1_ID")
							{
								this.ModDevLogWarning(string.Format("\t{0}: {1} in folder '{2}' found a DLC '{3}' it didn't recognize, ignoring.", new object[] { this.label, "mod_info.yaml", text, match.Value }));
							}
							else
							{
								list2.Add(match.Value);
							}
						}
					}
					if (list2.Count > 0)
					{
						packagedModInfo.requiredDlcIds = list2.ToArray();
					}
					if (!flag4)
					{
						packagedModInfo.forbiddenDlcIds = DlcManager.EXPANSION1;
					}
				}
			}
			if (packagedModInfo.requiredDlcIds != null)
			{
				for (int i = 0; i < packagedModInfo.requiredDlcIds.Length; i++)
				{
					packagedModInfo.requiredDlcIds[i] = packagedModInfo.requiredDlcIds[i].ToUpperInvariant();
					if (!DlcManager.IsDlcId(packagedModInfo.requiredDlcIds[i]))
					{
						this.ModDevLogWarning(string.Format("\t{0}: {1} in folder '{2}' is using an unrecognized DLC in requiredDlcIds '{3}'", new object[]
						{
							this.label,
							"mod_info.yaml",
							text,
							packagedModInfo.requiredDlcIds[i]
						}));
					}
				}
			}
			if (packagedModInfo.forbiddenDlcIds != null)
			{
				for (int j = 0; j < packagedModInfo.forbiddenDlcIds.Length; j++)
				{
					packagedModInfo.forbiddenDlcIds[j] = packagedModInfo.forbiddenDlcIds[j].ToUpperInvariant();
					if (!DlcManager.IsDlcId(packagedModInfo.forbiddenDlcIds[j]))
					{
						this.ModDevLogWarning(string.Format("\t{0}: {1} in folder '{2}' is using an unrecognized DLC in forbiddenDlcIds '{3}'", new object[]
						{
							this.label,
							"mod_info.yaml",
							text,
							packagedModInfo.forbiddenDlcIds[j]
						}));
					}
				}
			}
			if (packagedModInfo.lastWorkingBuild != 0)
			{
				this.ModDevLogError(string.Format("\t{0}: {1} in folder '{2}' is using `{3}`, please upgrade this to `{4}`", new object[] { this.label, "mod_info.yaml", text, "lastWorkingBuild", "minimumSupportedBuild" }));
				if (packagedModInfo.minimumSupportedBuild == 0)
				{
					packagedModInfo.minimumSupportedBuild = packagedModInfo.lastWorkingBuild;
				}
			}
			this.ModDevLog(string.Format("\t{0}: Found valid mod_info.yaml in folder '{1}': requiredDlcIds='{2}', forbiddenDlcIds='{3}' at {4}", new object[]
			{
				this.label,
				text,
				packagedModInfo.requiredDlcIds.DebugToCommaSeparatedList(),
				packagedModInfo.forbiddenDlcIds.DebugToCommaSeparatedList(),
				packagedModInfo.minimumSupportedBuild
			}));
			return packagedModInfo;
		}

		// Token: 0x06007BC4 RID: 31684 RVA: 0x00312DAC File Offset: 0x00310FAC
		private bool ScanContentFromSource(string relativeRoot, out Content available)
		{
			available = (Content)0;
			List<FileSystemItem> list = new List<FileSystemItem>();
			this.file_source.GetTopLevelItems(list, relativeRoot);
			foreach (FileSystemItem fileSystemItem in list)
			{
				if (fileSystemItem.type == FileSystemItem.ItemType.Directory)
				{
					string text = fileSystemItem.name.ToLower();
					available |= this.AddDirectory(text);
				}
				else
				{
					string text2 = fileSystemItem.name.ToLower();
					available |= this.AddFile(text2);
				}
			}
			return available > (Content)0;
		}

		// Token: 0x06007BC5 RID: 31685 RVA: 0x00312E4C File Offset: 0x0031104C
		private bool ScanContentFromSourceForTranslationsOnly(string relativeRoot)
		{
			this.available_content = (Content)0;
			List<FileSystemItem> list = new List<FileSystemItem>();
			this.file_source.GetTopLevelItems(list, relativeRoot);
			foreach (FileSystemItem fileSystemItem in list)
			{
				if (fileSystemItem.type == FileSystemItem.ItemType.File && fileSystemItem.name.ToLower().EndsWith(".po"))
				{
					this.available_content |= Content.Translation;
				}
			}
			return this.available_content > (Content)0;
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06007BC6 RID: 31686 RVA: 0x00312EE4 File Offset: 0x003110E4
		public string ContentPath
		{
			get
			{
				return Path.Combine(this.label.install_path, this.relative_root);
			}
		}

		// Token: 0x06007BC7 RID: 31687 RVA: 0x00312EFC File Offset: 0x003110FC
		public bool IsEmpty()
		{
			return this.available_content == (Content)0;
		}

		// Token: 0x06007BC8 RID: 31688 RVA: 0x00312F08 File Offset: 0x00311108
		private Content AddDirectory(string directory)
		{
			Content content = (Content)0;
			string text = directory.TrimEnd('/');
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1519694028U)
			{
				if (num != 948591336U)
				{
					if (num != 1318520008U)
					{
						if (num == 1519694028U)
						{
							if (text == "elements")
							{
								content |= Content.LayerableFiles;
							}
						}
					}
					else if (text == "buildingfacades")
					{
						content |= Content.Animation;
					}
				}
				else if (text == "templates")
				{
					content |= Content.LayerableFiles;
				}
			}
			else if (num <= 3037049615U)
			{
				if (num != 2960291089U)
				{
					if (num == 3037049615U)
					{
						if (text == "worldgen")
						{
							content |= Content.LayerableFiles;
						}
					}
				}
				else if (text == "strings")
				{
					content |= Content.Strings;
				}
			}
			else if (num != 3319670096U)
			{
				if (num == 3570262116U)
				{
					if (text == "codex")
					{
						content |= Content.LayerableFiles;
					}
				}
			}
			else if (text == "anim")
			{
				content |= Content.Animation;
			}
			return content;
		}

		// Token: 0x06007BC9 RID: 31689 RVA: 0x00313018 File Offset: 0x00311218
		private Content AddFile(string file)
		{
			Content content = (Content)0;
			if (file.EndsWith(".dll"))
			{
				content |= Content.DLL;
			}
			if (file.EndsWith(".po"))
			{
				content |= Content.Translation;
			}
			return content;
		}

		// Token: 0x06007BCA RID: 31690 RVA: 0x0031304A File Offset: 0x0031124A
		private static void AccumulateExtensions(Content content, List<string> extensions)
		{
			if ((content & Content.DLL) != (Content)0)
			{
				extensions.Add(".dll");
			}
			if ((content & (Content.Strings | Content.Translation)) != (Content)0)
			{
				extensions.Add(".po");
			}
		}

		// Token: 0x06007BCB RID: 31691 RVA: 0x00313070 File Offset: 0x00311270
		[Conditional("DEBUG")]
		private void Assert(bool condition, string failure_message)
		{
			if (string.IsNullOrEmpty(this.title))
			{
				DebugUtil.Assert(condition, string.Format("{2}\n\t{0}\n\t{1}", this.title, this.label.ToString(), failure_message));
				return;
			}
			DebugUtil.Assert(condition, string.Format("{1}\n\t{0}", this.label.ToString(), failure_message));
		}

		// Token: 0x06007BCC RID: 31692 RVA: 0x003130D8 File Offset: 0x003112D8
		public void Install()
		{
			if (this.IsLocal)
			{
				this.status = Mod.Status.Installed;
				return;
			}
			this.status = Mod.Status.ReinstallPending;
			if (this.file_source == null)
			{
				return;
			}
			if (!FileUtil.DeleteDirectory(this.label.install_path, 0))
			{
				return;
			}
			if (!FileUtil.CreateDirectory(this.label.install_path, 0))
			{
				return;
			}
			this.file_source.CopyTo(this.label.install_path, null);
			this.file_source = new Directory(this.label.install_path);
			this.status = Mod.Status.Installed;
		}

		// Token: 0x06007BCD RID: 31693 RVA: 0x00313168 File Offset: 0x00311368
		public bool Uninstall()
		{
			this.SetEnabledForActiveDlc(false);
			if (this.loaded_content != (Content)0)
			{
				global::Debug.Log(string.Format("Can't uninstall {0}: still has loaded content: {1}", this.label.ToString(), this.loaded_content.ToString()));
				this.status = Mod.Status.UninstallPending;
				return false;
			}
			if (!this.IsLocal && !FileUtil.DeleteDirectory(this.label.install_path, 0))
			{
				global::Debug.Log(string.Format("Can't uninstall {0}: directory deletion failed", this.label.ToString()));
				this.status = Mod.Status.UninstallPending;
				return false;
			}
			this.status = Mod.Status.NotInstalled;
			return true;
		}

		// Token: 0x06007BCE RID: 31694 RVA: 0x00313210 File Offset: 0x00311410
		private bool LoadStrings()
		{
			string text = FileSystem.Normalize(Path.Combine(this.ContentPath, "strings"));
			if (!Directory.Exists(text))
			{
				return false;
			}
			int num = 0;
			foreach (FileInfo fileInfo in new DirectoryInfo(text).GetFiles())
			{
				if (!(fileInfo.Extension.ToLower() != ".po"))
				{
					num++;
					Localization.OverloadStrings(Localization.LoadStringsFile(fileInfo.FullName, false));
				}
			}
			return true;
		}

		// Token: 0x06007BCF RID: 31695 RVA: 0x0031328D File Offset: 0x0031148D
		private bool LoadTranslations()
		{
			return false;
		}

		// Token: 0x06007BD0 RID: 31696 RVA: 0x00313290 File Offset: 0x00311490
		private bool LoadAnimation()
		{
			string text = FileSystem.Normalize(Path.Combine(this.ContentPath, "anim"));
			if (!Directory.Exists(text))
			{
				return false;
			}
			int num = 0;
			DirectoryInfo[] directories = new DirectoryInfo(text).GetDirectories();
			for (int i = 0; i < directories.Length; i++)
			{
				foreach (DirectoryInfo directoryInfo in directories[i].GetDirectories())
				{
					KAnimFile.Mod mod = new KAnimFile.Mod();
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
					{
						if (fileInfo.Extension == ".png")
						{
							byte[] array = File.ReadAllBytes(fileInfo.FullName);
							Texture2D texture2D = new Texture2D(2, 2);
							texture2D.LoadImage(array);
							mod.textures.Add(texture2D);
						}
						else if (fileInfo.Extension == ".bytes")
						{
							string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
							byte[] array2 = File.ReadAllBytes(fileInfo.FullName);
							if (fileNameWithoutExtension.EndsWith("_anim"))
							{
								mod.anim = array2;
							}
							else if (fileNameWithoutExtension.EndsWith("_build"))
							{
								mod.build = array2;
							}
							else
							{
								DebugUtil.LogWarningArgs(new object[] { string.Format("Unhandled TextAsset ({0})...ignoring", fileInfo.FullName) });
							}
						}
						else
						{
							DebugUtil.LogWarningArgs(new object[] { string.Format("Unhandled asset ({0})...ignoring", fileInfo.FullName) });
						}
					}
					string text2 = directoryInfo.Name + "_kanim";
					if (mod.IsValid() && ModUtil.AddKAnimMod(text2, mod))
					{
						num++;
					}
				}
			}
			return true;
		}

		// Token: 0x06007BD1 RID: 31697 RVA: 0x00313454 File Offset: 0x00311654
		public void Load(Content content)
		{
			content &= this.available_content & ~this.loaded_content;
			if (content > (Content)0)
			{
				global::Debug.Log(string.Format("Loading mod content {2} [{0}:{1}] (provides {3})", new object[]
				{
					this.title,
					this.label.id,
					content.ToString(),
					this.available_content.ToString()
				}));
			}
			if ((content & Content.Strings) != (Content)0 && this.LoadStrings())
			{
				this.loaded_content |= Content.Strings;
			}
			if ((content & Content.Translation) != (Content)0 && this.LoadTranslations())
			{
				this.loaded_content |= Content.Translation;
			}
			if ((content & Content.DLL) != (Content)0)
			{
				this.loaded_mod_data = DLLLoader.LoadDLLs(this, this.staticID, this.ContentPath, this.IsDev);
				if (this.loaded_mod_data != null)
				{
					this.loaded_content |= Content.DLL;
				}
			}
			if ((content & Content.LayerableFiles) != (Content)0)
			{
				global::Debug.Assert(this.content_source != null, "Attempting to Load layerable files with content_source not initialized");
				FileSystem.file_sources.Insert(0, this.content_source.GetFileSystem());
				this.loaded_content |= Content.LayerableFiles;
			}
			if ((content & Content.Animation) != (Content)0 && this.LoadAnimation())
			{
				this.loaded_content |= Content.Animation;
			}
		}

		// Token: 0x06007BD2 RID: 31698 RVA: 0x00313593 File Offset: 0x00311793
		public void PostLoad(IReadOnlyList<Mod> mods)
		{
			if ((this.loaded_content & Content.DLL) != (Content)0 && this.loaded_mod_data != null)
			{
				DLLLoader.PostLoadDLLs(this.staticID, this.loaded_mod_data, mods);
			}
		}

		// Token: 0x06007BD3 RID: 31699 RVA: 0x003135B9 File Offset: 0x003117B9
		public void Unload(Content content)
		{
			content &= this.loaded_content;
			if ((content & Content.LayerableFiles) != (Content)0)
			{
				FileSystem.file_sources.Remove(this.content_source.GetFileSystem());
				this.loaded_content &= ~Content.LayerableFiles;
			}
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x003135F2 File Offset: 0x003117F2
		private void SetCrashCount(int new_crash_count)
		{
			this.crash_count = MathUtil.Clamp(0, 3, new_crash_count);
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06007BD5 RID: 31701 RVA: 0x00313602 File Offset: 0x00311802
		public bool IsDev
		{
			get
			{
				return this.label.distribution_platform == Label.DistributionPlatform.Dev;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06007BD6 RID: 31702 RVA: 0x00313612 File Offset: 0x00311812
		public bool IsLocal
		{
			get
			{
				return this.label.distribution_platform == Label.DistributionPlatform.Dev || this.label.distribution_platform == Label.DistributionPlatform.Local;
			}
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x00313632 File Offset: 0x00311832
		public void SetCrashed()
		{
			this.SetCrashCount(this.crash_count + 1);
			if (!this.IsDev)
			{
				this.SetEnabledForActiveDlc(false);
			}
		}

		// Token: 0x06007BD8 RID: 31704 RVA: 0x00313651 File Offset: 0x00311851
		public void Uncrash()
		{
			this.SetCrashCount(this.IsDev ? (this.crash_count - 1) : 0);
		}

		// Token: 0x06007BD9 RID: 31705 RVA: 0x0031366C File Offset: 0x0031186C
		public bool IsActive()
		{
			return this.loaded_content > (Content)0;
		}

		// Token: 0x06007BDA RID: 31706 RVA: 0x00313677 File Offset: 0x00311877
		public bool AllActive(Content content)
		{
			return (this.loaded_content & content) == content;
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x00313684 File Offset: 0x00311884
		public bool AllActive()
		{
			return (this.loaded_content & this.available_content) == this.available_content;
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x0031369B File Offset: 0x0031189B
		public bool AnyActive(Content content)
		{
			return (this.loaded_content & content) > (Content)0;
		}

		// Token: 0x06007BDD RID: 31709 RVA: 0x003136A8 File Offset: 0x003118A8
		public bool HasContent()
		{
			return this.available_content > (Content)0;
		}

		// Token: 0x06007BDE RID: 31710 RVA: 0x003136B3 File Offset: 0x003118B3
		public bool HasAnyContent(Content content)
		{
			return (this.available_content & content) > (Content)0;
		}

		// Token: 0x06007BDF RID: 31711 RVA: 0x003136C0 File Offset: 0x003118C0
		public bool HasOnlyTranslationContent()
		{
			return this.available_content == Content.Translation;
		}

		// Token: 0x06007BE0 RID: 31712 RVA: 0x003136CC File Offset: 0x003118CC
		public Texture2D GetPreviewImage()
		{
			string text = null;
			foreach (string text2 in Mod.PREVIEW_FILENAMES)
			{
				if (Directory.Exists(this.ContentPath) && File.Exists(Path.Combine(this.ContentPath, text2)))
				{
					text = text2;
					break;
				}
			}
			if (text == null)
			{
				return null;
			}
			Texture2D texture2D2;
			try
			{
				byte[] array = File.ReadAllBytes(Path.Combine(this.ContentPath, text));
				Texture2D texture2D = new Texture2D(2, 2);
				texture2D.LoadImage(array);
				texture2D2 = texture2D;
			}
			catch
			{
				global::Debug.LogWarning(string.Format("Mod {0} seems to have a preview.png but it didn't load correctly.", this.label));
				texture2D2 = null;
			}
			return texture2D2;
		}

		// Token: 0x06007BE1 RID: 31713 RVA: 0x00313798 File Offset: 0x00311998
		public void ModDevLog(string msg)
		{
			if (this.IsDev)
			{
				global::Debug.Log(msg);
			}
		}

		// Token: 0x06007BE2 RID: 31714 RVA: 0x003137A8 File Offset: 0x003119A8
		public void ModDevLogWarning(string msg)
		{
			if (this.IsDev)
			{
				global::Debug.LogWarning(msg);
			}
		}

		// Token: 0x06007BE3 RID: 31715 RVA: 0x003137B8 File Offset: 0x003119B8
		public void ModDevLogError(string msg)
		{
			if (this.IsDev)
			{
				this.DevModCrashTriggered = true;
				global::Debug.LogError(msg);
			}
		}

		// Token: 0x04005B01 RID: 23297
		public const int MOD_API_VERSION_NONE = 0;

		// Token: 0x04005B02 RID: 23298
		public const int MOD_API_VERSION_HARMONY1 = 1;

		// Token: 0x04005B03 RID: 23299
		public const int MOD_API_VERSION_HARMONY2 = 2;

		// Token: 0x04005B04 RID: 23300
		public const int MOD_API_VERSION = 2;

		// Token: 0x04005B05 RID: 23301
		[JsonProperty]
		public Label label;

		// Token: 0x04005B06 RID: 23302
		[JsonProperty]
		public Mod.Status status;

		// Token: 0x04005B07 RID: 23303
		[JsonProperty]
		public bool enabled;

		// Token: 0x04005B08 RID: 23304
		[JsonProperty]
		public List<string> enabledForDlc;

		// Token: 0x04005B0A RID: 23306
		[JsonProperty]
		public int crash_count;

		// Token: 0x04005B0B RID: 23307
		[JsonProperty]
		public string reinstall_path;

		// Token: 0x04005B0D RID: 23309
		public bool foundInStackTrace;

		// Token: 0x04005B0E RID: 23310
		public string relative_root = "";

		// Token: 0x04005B0F RID: 23311
		public Mod.PackagedModInfo packagedModInfo;

		// Token: 0x04005B14 RID: 23316
		public LoadedModData loaded_mod_data;

		// Token: 0x04005B15 RID: 23317
		private IFileSource _fileSource;

		// Token: 0x04005B16 RID: 23318
		public IFileSource content_source;

		// Token: 0x04005B17 RID: 23319
		public bool is_subscribed;

		// Token: 0x04005B19 RID: 23321
		private const string VANILLA_ID = "VANILLA_ID";

		// Token: 0x04005B1A RID: 23322
		private const string ALL_ID = "ALL";

		// Token: 0x04005B1B RID: 23323
		private const string ARCHIVED_VERSIONS_FOLDER = "archived_versions";

		// Token: 0x04005B1C RID: 23324
		private const string MOD_INFO_FILENAME = "mod_info.yaml";

		// Token: 0x04005B1D RID: 23325
		public ModContentCompatability contentCompatability;

		// Token: 0x04005B1E RID: 23326
		public string[] requiredDlcIds;

		// Token: 0x04005B1F RID: 23327
		public string[] forbiddenDlcIds;

		// Token: 0x04005B20 RID: 23328
		public const int MAX_CRASH_COUNT = 3;

		// Token: 0x04005B21 RID: 23329
		private static readonly List<string> PREVIEW_FILENAMES = new List<string> { "preview.png", "Preview.png", "PREVIEW.PNG" };

		// Token: 0x0200210B RID: 8459
		public enum Status
		{
			// Token: 0x04009727 RID: 38695
			NotInstalled,
			// Token: 0x04009728 RID: 38696
			Installed,
			// Token: 0x04009729 RID: 38697
			UninstallPending,
			// Token: 0x0400972A RID: 38698
			ReinstallPending
		}

		// Token: 0x0200210C RID: 8460
		public class ArchivedVersion
		{
			// Token: 0x0400972B RID: 38699
			public string relativePath;

			// Token: 0x0400972C RID: 38700
			public Mod.PackagedModInfo info;
		}

		// Token: 0x0200210D RID: 8461
		public class PackagedModInfo : IHasDlcRestrictions
		{
			// Token: 0x17000CB5 RID: 3253
			// (get) Token: 0x0600B8D6 RID: 47318 RVA: 0x003EA955 File Offset: 0x003E8B55
			// (set) Token: 0x0600B8D7 RID: 47319 RVA: 0x003EA95D File Offset: 0x003E8B5D
			[Obsolete("Use IHasDlcRestrictions interface instead")]
			public string supportedContent { get; set; }

			// Token: 0x17000CB6 RID: 3254
			// (get) Token: 0x0600B8D8 RID: 47320 RVA: 0x003EA966 File Offset: 0x003E8B66
			// (set) Token: 0x0600B8D9 RID: 47321 RVA: 0x003EA96E File Offset: 0x003E8B6E
			public string[] requiredDlcIds { get; set; }

			// Token: 0x17000CB7 RID: 3255
			// (get) Token: 0x0600B8DA RID: 47322 RVA: 0x003EA977 File Offset: 0x003E8B77
			// (set) Token: 0x0600B8DB RID: 47323 RVA: 0x003EA97F File Offset: 0x003E8B7F
			public string[] forbiddenDlcIds { get; set; }

			// Token: 0x17000CB8 RID: 3256
			// (get) Token: 0x0600B8DC RID: 47324 RVA: 0x003EA988 File Offset: 0x003E8B88
			// (set) Token: 0x0600B8DD RID: 47325 RVA: 0x003EA990 File Offset: 0x003E8B90
			[Obsolete("Use minimumSupportedBuild instead!")]
			public int lastWorkingBuild { get; set; }

			// Token: 0x17000CB9 RID: 3257
			// (get) Token: 0x0600B8DE RID: 47326 RVA: 0x003EA999 File Offset: 0x003E8B99
			// (set) Token: 0x0600B8DF RID: 47327 RVA: 0x003EA9A1 File Offset: 0x003E8BA1
			public int minimumSupportedBuild { get; set; }

			// Token: 0x17000CBA RID: 3258
			// (get) Token: 0x0600B8E0 RID: 47328 RVA: 0x003EA9AA File Offset: 0x003E8BAA
			// (set) Token: 0x0600B8E1 RID: 47329 RVA: 0x003EA9B2 File Offset: 0x003E8BB2
			public int APIVersion { get; set; }

			// Token: 0x17000CBB RID: 3259
			// (get) Token: 0x0600B8E2 RID: 47330 RVA: 0x003EA9BB File Offset: 0x003E8BBB
			// (set) Token: 0x0600B8E3 RID: 47331 RVA: 0x003EA9C3 File Offset: 0x003E8BC3
			public string version { get; set; }

			// Token: 0x0600B8E4 RID: 47332 RVA: 0x003EA9CC File Offset: 0x003E8BCC
			public string[] GetRequiredDlcIds()
			{
				return this.requiredDlcIds;
			}

			// Token: 0x0600B8E5 RID: 47333 RVA: 0x003EA9D4 File Offset: 0x003E8BD4
			public string[] GetForbiddenDlcIds()
			{
				return this.forbiddenDlcIds;
			}
		}
	}
}
