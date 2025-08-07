using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ArabicSupport;
using Klei;
using KMod;
using Steamworks;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x020009AC RID: 2476
public static class Localization
{
	// Token: 0x17000508 RID: 1288
	// (get) Token: 0x06004803 RID: 18435 RVA: 0x0019FF93 File Offset: 0x0019E193
	public static TMP_FontAsset FontAsset
	{
		get
		{
			return Localization.sFontAsset;
		}
	}

	// Token: 0x17000509 RID: 1289
	// (get) Token: 0x06004804 RID: 18436 RVA: 0x0019FF9A File Offset: 0x0019E19A
	public static bool IsRightToLeft
	{
		get
		{
			return Localization.sLocale != null && Localization.sLocale.IsRightToLeft;
		}
	}

	// Token: 0x06004805 RID: 18437 RVA: 0x0019FFB0 File Offset: 0x0019E1B0
	private static IEnumerable<Type> CollectLocStringTreeRoots(string locstrings_namespace, Assembly assembly)
	{
		return from type in assembly.GetTypes()
			where type.IsClass && type.Namespace == locstrings_namespace && !type.IsNested
			select type;
	}

	// Token: 0x06004806 RID: 18438 RVA: 0x0019FFE4 File Offset: 0x0019E1E4
	private static Dictionary<string, object> MakeRuntimeLocStringTree(Type locstring_tree_root)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (FieldInfo fieldInfo in locstring_tree_root.GetFields())
		{
			if (!(fieldInfo.FieldType != typeof(LocString)))
			{
				if (!fieldInfo.IsStatic)
				{
					DebugUtil.DevLogError("LocString fields must be static, skipping. " + fieldInfo.Name);
				}
				else
				{
					LocString locString = (LocString)fieldInfo.GetValue(null);
					if (locString == null)
					{
						global::Debug.LogError("Tried to generate LocString for " + fieldInfo.Name + " but it is null so skipping");
					}
					else
					{
						dictionary[fieldInfo.Name] = locString.text;
					}
				}
			}
		}
		foreach (Type type in locstring_tree_root.GetNestedTypes())
		{
			Dictionary<string, object> dictionary2 = Localization.MakeRuntimeLocStringTree(type);
			if (dictionary2.Count > 0)
			{
				dictionary[type.Name] = dictionary2;
			}
		}
		return dictionary;
	}

	// Token: 0x06004807 RID: 18439 RVA: 0x001A00CC File Offset: 0x0019E2CC
	private static void WriteStringsTemplate(string path, StreamWriter writer, Dictionary<string, object> runtime_locstring_tree)
	{
		List<string> list = new List<string>(runtime_locstring_tree.Keys);
		list.Sort();
		foreach (string text in list)
		{
			string text2 = path + "." + text;
			object obj = runtime_locstring_tree[text];
			if (obj.GetType() != typeof(string))
			{
				Localization.WriteStringsTemplate(text2, writer, obj as Dictionary<string, object>);
			}
			else
			{
				string text3 = obj as string;
				text3 = text3.Replace("\\", "\\\\");
				text3 = text3.Replace("\"", "\\\"");
				text3 = text3.Replace("\n", "\\n");
				text3 = text3.Replace("’", "'");
				text3 = text3.Replace("“", "\\\"");
				text3 = text3.Replace("”", "\\\"");
				text3 = text3.Replace("…", "...");
				writer.WriteLine("#. " + text2);
				writer.WriteLine("msgctxt \"{0}\"", text2);
				writer.WriteLine("msgid \"" + text3 + "\"");
				writer.WriteLine("msgstr \"\"");
				writer.WriteLine("");
			}
		}
	}

	// Token: 0x06004808 RID: 18440 RVA: 0x001A024C File Offset: 0x0019E44C
	public static void GenerateStringsTemplate(string locstrings_namespace, Assembly assembly, string output_filename, Dictionary<string, object> current_runtime_locstring_forest)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (Type type in Localization.CollectLocStringTreeRoots(locstrings_namespace, assembly))
		{
			Dictionary<string, object> dictionary2 = Localization.MakeRuntimeLocStringTree(type);
			if (dictionary2.Count > 0)
			{
				dictionary[type.Name] = dictionary2;
			}
		}
		if (current_runtime_locstring_forest != null)
		{
			dictionary.Concat(current_runtime_locstring_forest);
		}
		using (StreamWriter streamWriter = new StreamWriter(output_filename, false, new UTF8Encoding(false)))
		{
			streamWriter.WriteLine("msgid \"\"");
			streamWriter.WriteLine("msgstr \"\"");
			streamWriter.WriteLine("\"Application: Oxygen Not Included\"");
			streamWriter.WriteLine("\"POT Version: 2.0\"");
			streamWriter.WriteLine("");
			Localization.WriteStringsTemplate(locstrings_namespace, streamWriter, dictionary);
		}
		DebugUtil.LogArgs(new object[] { "Generated " + output_filename });
	}

	// Token: 0x06004809 RID: 18441 RVA: 0x001A0348 File Offset: 0x0019E548
	public static void GenerateStringsTemplate(Type locstring_tree_root, string output_folder)
	{
		output_folder = FileSystem.Normalize(output_folder);
		if (!FileUtil.CreateDirectory(output_folder, 5))
		{
			return;
		}
		Localization.GenerateStringsTemplate(locstring_tree_root.Namespace, Assembly.GetAssembly(locstring_tree_root), FileSystem.Normalize(Path.Combine(output_folder, string.Format("{0}_template.pot", locstring_tree_root.Namespace.ToLower()))), null);
	}

	// Token: 0x0600480A RID: 18442 RVA: 0x001A039C File Offset: 0x0019E59C
	public static void Initialize()
	{
		DebugUtil.LogArgs(new object[] { "Localization.Initialize!" });
		bool flag = false;
		switch (Localization.GetSelectedLanguageType())
		{
		case Localization.SelectedLanguageType.None:
			Localization.sFontAsset = Localization.GetFont(Localization.GetDefaultLocale().FontName);
			break;
		case Localization.SelectedLanguageType.Preinstalled:
		{
			string currentLanguageCode = Localization.GetCurrentLanguageCode();
			if (!string.IsNullOrEmpty(currentLanguageCode))
			{
				DebugUtil.LogArgs(new object[] { "Localization Initialize... Preinstalled localization" });
				DebugUtil.LogArgs(new object[] { " -> ", currentLanguageCode });
				Localization.LoadPreinstalledTranslation(currentLanguageCode);
			}
			else
			{
				flag = true;
			}
			break;
		}
		case Localization.SelectedLanguageType.UGC:
			if (LanguageOptionsScreen.HasInstalledLanguage())
			{
				DebugUtil.LogArgs(new object[] { "Localization Initialize... Mod-based localization" });
				string savedLanguageMod = LanguageOptionsScreen.GetSavedLanguageMod();
				if (LanguageOptionsScreen.SetCurrentLanguage(savedLanguageMod))
				{
					DebugUtil.LogArgs(new object[] { " -> Loaded language from mod: " + savedLanguageMod });
				}
				else
				{
					DebugUtil.LogArgs(new object[] { " -> Failed to load language from mod: " + savedLanguageMod });
				}
			}
			else
			{
				flag = true;
			}
			break;
		}
		if (flag)
		{
			Localization.ClearLanguage();
		}
	}

	// Token: 0x0600480B RID: 18443 RVA: 0x001A04A0 File Offset: 0x0019E6A0
	public static void VerifyTranslationModSubscription(GameObject context)
	{
		if (Localization.GetSelectedLanguageType() != Localization.SelectedLanguageType.UGC)
		{
			return;
		}
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (LanguageOptionsScreen.HasInstalledLanguage())
		{
			return;
		}
		PublishedFileId_t publishedFileId_t = new PublishedFileId_t((ulong)KPlayerPrefs.GetInt("InstalledLanguage", (int)PublishedFileId_t.Invalid.m_PublishedFileId));
		Label label = new Label
		{
			distribution_platform = Label.DistributionPlatform.Steam,
			id = publishedFileId_t.ToString()
		};
		string text = UI.FRONTEND.TRANSLATIONS_SCREEN.UNKNOWN;
		foreach (Mod mod in Global.Instance.modManager.mods)
		{
			if (mod.label.Match(label))
			{
				text = mod.title;
				break;
			}
		}
		Localization.ClearLanguage();
		KScreen component = KScreenManager.AddChild(context, ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<KScreen>();
		component.Activate();
		ConfirmDialogScreen component2 = component.GetComponent<ConfirmDialogScreen>();
		string text2 = UI.CONFIRMDIALOG.DIALOG_HEADER;
		string text3 = string.Format(UI.FRONTEND.TRANSLATIONS_SCREEN.MISSING_LANGUAGE_PACK, text);
		string text4 = UI.FRONTEND.TRANSLATIONS_SCREEN.RESTART;
		component2.PopupConfirmDialog(text3, new global::System.Action(App.instance.Restart), null, null, null, text2, text4, null, null);
	}

	// Token: 0x0600480C RID: 18444 RVA: 0x001A05E8 File Offset: 0x0019E7E8
	public static void LoadPreinstalledTranslation(string code)
	{
		if (!string.IsNullOrEmpty(code) && code != Localization.DEFAULT_LANGUAGE_CODE)
		{
			string preinstalledLocalizationFilePath = Localization.GetPreinstalledLocalizationFilePath(code);
			if (Localization.LoadLocalTranslationFile(Localization.SelectedLanguageType.Preinstalled, preinstalledLocalizationFilePath))
			{
				KPlayerPrefs.SetString(Localization.SELECTED_LANGUAGE_CODE_KEY, code);
				return;
			}
		}
		else
		{
			Localization.ClearLanguage();
		}
	}

	// Token: 0x0600480D RID: 18445 RVA: 0x001A062B File Offset: 0x0019E82B
	public static bool LoadLocalTranslationFile(Localization.SelectedLanguageType source, string path)
	{
		if (!File.Exists(path))
		{
			return false;
		}
		bool flag = Localization.LoadTranslationFromLines(File.ReadAllLines(path, Encoding.UTF8));
		if (flag)
		{
			KPlayerPrefs.SetString(Localization.SELECTED_LANGUAGE_TYPE_KEY, source.ToString());
			return flag;
		}
		Localization.ClearLanguage();
		return flag;
	}

	// Token: 0x0600480E RID: 18446 RVA: 0x001A0668 File Offset: 0x0019E868
	private static bool LoadTranslationFromLines(string[] lines)
	{
		if (lines == null || lines.Length == 0)
		{
			return false;
		}
		Localization.sLocale = Localization.GetLocale(lines);
		DebugUtil.LogArgs(new object[]
		{
			" -> Locale is now ",
			Localization.sLocale.ToString()
		});
		bool flag = Localization.LoadTranslation(lines, false);
		if (flag)
		{
			Localization.currentFontName = Localization.GetFontName(lines);
			Localization.SwapToLocalizedFont(Localization.currentFontName);
		}
		return flag;
	}

	// Token: 0x0600480F RID: 18447 RVA: 0x001A06CC File Offset: 0x0019E8CC
	public static bool LoadTranslation(string[] lines, bool isTemplate = false)
	{
		bool flag;
		try
		{
			Localization.OverloadStrings(Localization.ExtractTranslatedStrings(lines, isTemplate));
			flag = true;
		}
		catch (Exception ex)
		{
			DebugUtil.LogWarningArgs(new object[] { ex });
			flag = false;
		}
		return flag;
	}

	// Token: 0x06004810 RID: 18448 RVA: 0x001A0710 File Offset: 0x0019E910
	public static Dictionary<string, string> LoadStringsFile(string path, bool isTemplate)
	{
		return Localization.ExtractTranslatedStrings(File.ReadAllLines(path, Encoding.UTF8), isTemplate);
	}

	// Token: 0x06004811 RID: 18449 RVA: 0x001A0724 File Offset: 0x0019E924
	public static Dictionary<string, string> ExtractTranslatedStrings(string[] lines, bool isTemplate = false)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		Localization.Entry entry = default(Localization.Entry);
		string text = (isTemplate ? "msgid" : "msgstr");
		for (int i = 0; i < lines.Length; i++)
		{
			string text2 = lines[i];
			if (text2 == null || text2.Length == 0)
			{
				entry = default(Localization.Entry);
			}
			else
			{
				string text3 = Localization.GetParameter("msgctxt", i, lines);
				if (text3 != null)
				{
					entry.msgctxt = text3;
				}
				text3 = Localization.GetParameter(text, i, lines);
				if (text3 != null)
				{
					entry.msgstr = text3;
				}
			}
			if (entry.IsPopulated)
			{
				dictionary[entry.msgctxt] = entry.msgstr;
				entry = default(Localization.Entry);
			}
		}
		return dictionary;
	}

	// Token: 0x06004812 RID: 18450 RVA: 0x001A07D0 File Offset: 0x0019E9D0
	private static string FixupString(string result)
	{
		result = result.Replace("\\n", "\n");
		result = result.Replace("\\\"", "\"");
		result = result.Replace("<style=“", "<style=\"");
		result = result.Replace("”>", "\">");
		result = result.Replace("<color=^p", "<color=#");
		return result;
	}

	// Token: 0x06004813 RID: 18451 RVA: 0x001A0838 File Offset: 0x0019EA38
	private static string GetParameter(string key, int idx, string[] all_lines)
	{
		if (!all_lines[idx].StartsWith(key))
		{
			return null;
		}
		List<string> list = new List<string>();
		string text = all_lines[idx];
		text = text.Substring(key.Length + 1, text.Length - key.Length - 1);
		list.Add(text);
		for (int i = idx + 1; i < all_lines.Length; i++)
		{
			string text2 = all_lines[i];
			if (!text2.StartsWith("\""))
			{
				break;
			}
			list.Add(text2);
		}
		string text3 = "";
		foreach (string text4 in list)
		{
			if (text4.EndsWith("\r"))
			{
				text4 = text4.Substring(0, text4.Length - 1);
			}
			text4 = text4.Substring(1, text4.Length - 2);
			text4 = Localization.FixupString(text4);
			text3 += text4;
		}
		return text3;
	}

	// Token: 0x06004814 RID: 18452 RVA: 0x001A0938 File Offset: 0x0019EB38
	private static void AddAssembly(string locstrings_namespace, Assembly assembly)
	{
		List<Assembly> list;
		if (!Localization.translatable_assemblies.TryGetValue(locstrings_namespace, out list))
		{
			list = new List<Assembly>();
			Localization.translatable_assemblies.Add(locstrings_namespace, list);
		}
		list.Add(assembly);
	}

	// Token: 0x06004815 RID: 18453 RVA: 0x001A096D File Offset: 0x0019EB6D
	public static void AddAssembly(Assembly assembly)
	{
		Localization.AddAssembly("STRINGS", assembly);
	}

	// Token: 0x06004816 RID: 18454 RVA: 0x001A097C File Offset: 0x0019EB7C
	public static void RegisterForTranslation(Type locstring_tree_root)
	{
		Assembly assembly = Assembly.GetAssembly(locstring_tree_root);
		Localization.AddAssembly(locstring_tree_root.Namespace, assembly);
		string text = locstring_tree_root.Namespace + ".";
		foreach (Type type in Localization.CollectLocStringTreeRoots(locstring_tree_root.Namespace, assembly))
		{
			LocString.CreateLocStringKeys(type, text);
		}
	}

	// Token: 0x06004817 RID: 18455 RVA: 0x001A09F4 File Offset: 0x0019EBF4
	public static void OverloadStrings(Dictionary<string, string> translated_strings)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		foreach (KeyValuePair<string, List<Assembly>> keyValuePair in Localization.translatable_assemblies)
		{
			foreach (Assembly assembly in keyValuePair.Value)
			{
				foreach (Type type in Localization.CollectLocStringTreeRoots(keyValuePair.Key, assembly))
				{
					string text4 = keyValuePair.Key + "." + type.Name;
					Localization.OverloadStrings(translated_strings, text4, type, ref text, ref text2, ref text3);
				}
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			DebugUtil.LogArgs(new object[] { "TRANSLATION ERROR! The following have missing or mismatched parameters:\n" + text });
		}
		if (!string.IsNullOrEmpty(text2))
		{
			DebugUtil.LogArgs(new object[] { "TRANSLATION ERROR! The following have mismatched <link> tags:\n" + text2 });
		}
		if (!string.IsNullOrEmpty(text3))
		{
			DebugUtil.LogArgs(new object[] { "TRANSLATION ERROR! The following do not have the same amount of <link> tags as the english string which can cause nested link errors:\n" + text3 });
		}
	}

	// Token: 0x06004818 RID: 18456 RVA: 0x001A0B68 File Offset: 0x0019ED68
	public static void OverloadStrings(Dictionary<string, string> translated_strings, string path, Type locstring_hierarchy, ref string parameter_errors, ref string link_errors, ref string link_count_errors)
	{
		foreach (FieldInfo fieldInfo in locstring_hierarchy.GetFields())
		{
			if (!(fieldInfo.FieldType != typeof(LocString)))
			{
				string text = path + "." + fieldInfo.Name;
				string text2 = null;
				if (translated_strings.TryGetValue(text, out text2))
				{
					LocString locString = (LocString)fieldInfo.GetValue(null);
					LocString locString2 = new LocString(text2, text);
					if (!Localization.AreParametersPreserved(locString.text, text2))
					{
						parameter_errors = parameter_errors + "\t" + text + "\n";
					}
					else if (!Localization.HasSameOrLessLinkCountAsEnglish(locString.text, text2))
					{
						link_count_errors = link_count_errors + "\t" + text + "\n";
					}
					else if (!Localization.HasMatchingLinkTags(text2, 0))
					{
						link_errors = link_errors + "\t" + text + "\n";
					}
					else
					{
						fieldInfo.SetValue(null, locString2);
					}
				}
			}
		}
		foreach (Type type in locstring_hierarchy.GetNestedTypes())
		{
			string text3 = path + "." + type.Name;
			Localization.OverloadStrings(translated_strings, text3, type, ref parameter_errors, ref link_errors, ref link_count_errors);
		}
	}

	// Token: 0x06004819 RID: 18457 RVA: 0x001A0CA2 File Offset: 0x0019EEA2
	public static string GetDefaultLocalizationFilePath()
	{
		return Path.Combine(Application.streamingAssetsPath, "strings/strings_template.pot");
	}

	// Token: 0x0600481A RID: 18458 RVA: 0x001A0CB3 File Offset: 0x0019EEB3
	public static string GetModLocalizationFilePath()
	{
		return Path.Combine(Application.streamingAssetsPath, "strings/strings.po");
	}

	// Token: 0x0600481B RID: 18459 RVA: 0x001A0CC4 File Offset: 0x0019EEC4
	public static string GetPreinstalledLocalizationFilePath(string code)
	{
		string text = "strings/strings_preinstalled_" + code + ".po";
		return Path.Combine(Application.streamingAssetsPath, text);
	}

	// Token: 0x0600481C RID: 18460 RVA: 0x001A0CED File Offset: 0x0019EEED
	public static string GetPreinstalledLocalizationTitle(string code)
	{
		return Strings.Get("STRINGS.UI.FRONTEND.TRANSLATIONS_SCREEN.PREINSTALLED_LANGUAGES." + code.ToUpper());
	}

	// Token: 0x0600481D RID: 18461 RVA: 0x001A0D0C File Offset: 0x0019EF0C
	public static Texture2D GetPreinstalledLocalizationImage(string code)
	{
		string text = Path.Combine(Application.streamingAssetsPath, "strings/preinstalled_icon_" + code + ".png");
		if (File.Exists(text))
		{
			byte[] array = File.ReadAllBytes(text);
			Texture2D texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(array);
			return texture2D;
		}
		return null;
	}

	// Token: 0x0600481E RID: 18462 RVA: 0x001A0D54 File Offset: 0x0019EF54
	public static void SetLocale(Localization.Locale locale)
	{
		Localization.sLocale = locale;
		DebugUtil.LogArgs(new object[]
		{
			" -> Locale is now ",
			Localization.sLocale.ToString()
		});
	}

	// Token: 0x0600481F RID: 18463 RVA: 0x001A0D7C File Offset: 0x0019EF7C
	public static Localization.Locale GetLocale()
	{
		return Localization.sLocale;
	}

	// Token: 0x06004820 RID: 18464 RVA: 0x001A0D84 File Offset: 0x0019EF84
	private static string GetFontParam(string line)
	{
		string text = null;
		if (line.StartsWith("\"Font:"))
		{
			text = line.Substring("\"Font:".Length).Trim();
			text = text.Replace("\\n", "");
			text = text.Replace("\"", "");
		}
		return text;
	}

	// Token: 0x06004821 RID: 18465 RVA: 0x001A0DDC File Offset: 0x0019EFDC
	public static string GetCurrentLanguageCode()
	{
		switch (Localization.GetSelectedLanguageType())
		{
		case Localization.SelectedLanguageType.None:
			return Localization.DEFAULT_LANGUAGE_CODE;
		case Localization.SelectedLanguageType.Preinstalled:
			return KPlayerPrefs.GetString(Localization.SELECTED_LANGUAGE_CODE_KEY);
		case Localization.SelectedLanguageType.UGC:
			return LanguageOptionsScreen.GetInstalledLanguageCode();
		default:
			return "";
		}
	}

	// Token: 0x06004822 RID: 18466 RVA: 0x001A0E20 File Offset: 0x0019F020
	public static Localization.SelectedLanguageType GetSelectedLanguageType()
	{
		return (Localization.SelectedLanguageType)Enum.Parse(typeof(Localization.SelectedLanguageType), KPlayerPrefs.GetString(Localization.SELECTED_LANGUAGE_TYPE_KEY, Localization.SelectedLanguageType.None.ToString()), true);
	}

	// Token: 0x06004823 RID: 18467 RVA: 0x001A0E5C File Offset: 0x0019F05C
	private static string GetLanguageCode(string line)
	{
		string text = null;
		if (line.StartsWith("\"Language:"))
		{
			text = line.Substring("\"Language:".Length).Trim();
			text = text.Replace("\\n", "");
			text = text.Replace("\"", "");
		}
		return text;
	}

	// Token: 0x06004824 RID: 18468 RVA: 0x001A0EB4 File Offset: 0x0019F0B4
	private static Localization.Locale GetLocaleForCode(string code)
	{
		Localization.Locale locale = null;
		foreach (Localization.Locale locale2 in Localization.Locales)
		{
			if (locale2.MatchesCode(code))
			{
				locale = locale2;
				break;
			}
		}
		return locale;
	}

	// Token: 0x06004825 RID: 18469 RVA: 0x001A0F10 File Offset: 0x0019F110
	public static Localization.Locale GetLocale(string[] lines)
	{
		Localization.Locale locale = null;
		string text = null;
		if (lines != null && lines.Length != 0)
		{
			foreach (string text2 in lines)
			{
				if (text2 != null && text2.Length != 0)
				{
					text = Localization.GetLanguageCode(text2);
					if (text != null)
					{
						locale = Localization.GetLocaleForCode(text);
					}
					if (text != null)
					{
						break;
					}
				}
			}
		}
		if (locale == null)
		{
			locale = Localization.GetDefaultLocale();
		}
		if (text != null && locale.Code == "")
		{
			locale.SetCode(text);
		}
		return locale;
	}

	// Token: 0x06004826 RID: 18470 RVA: 0x001A0F85 File Offset: 0x0019F185
	private static string GetFontName(string filename)
	{
		return Localization.GetFontName(File.ReadAllLines(filename, Encoding.UTF8));
	}

	// Token: 0x06004827 RID: 18471 RVA: 0x001A0F98 File Offset: 0x0019F198
	public static Localization.Locale GetDefaultLocale()
	{
		Localization.Locale locale = null;
		foreach (Localization.Locale locale2 in Localization.Locales)
		{
			if (locale2.Lang == Localization.Language.Unspecified)
			{
				locale = new Localization.Locale(locale2);
				break;
			}
		}
		return locale;
	}

	// Token: 0x06004828 RID: 18472 RVA: 0x001A0FF8 File Offset: 0x0019F1F8
	public static string GetDefaultFontName()
	{
		string text = null;
		foreach (Localization.Locale locale in Localization.Locales)
		{
			if (locale.Lang == Localization.Language.Unspecified)
			{
				text = locale.FontName;
				break;
			}
		}
		return text;
	}

	// Token: 0x06004829 RID: 18473 RVA: 0x001A1058 File Offset: 0x0019F258
	public static string ValidateFontName(string fontName)
	{
		foreach (Localization.Locale locale in Localization.Locales)
		{
			if (locale.MatchesFont(fontName))
			{
				return locale.FontName;
			}
		}
		return null;
	}

	// Token: 0x0600482A RID: 18474 RVA: 0x001A10B8 File Offset: 0x0019F2B8
	public static string GetFontName(string[] lines)
	{
		string text = null;
		if (lines != null)
		{
			foreach (string text2 in lines)
			{
				if (!string.IsNullOrEmpty(text2))
				{
					string fontParam = Localization.GetFontParam(text2);
					if (fontParam != null)
					{
						text = Localization.ValidateFontName(fontParam);
					}
				}
				if (text != null)
				{
					break;
				}
			}
		}
		if (text == null)
		{
			if (Localization.sLocale != null)
			{
				text = Localization.sLocale.FontName;
			}
			else
			{
				text = Localization.GetDefaultFontName();
			}
		}
		return text;
	}

	// Token: 0x0600482B RID: 18475 RVA: 0x001A111B File Offset: 0x0019F31B
	public static void SwapToLocalizedFont()
	{
		Localization.SwapToLocalizedFont(Localization.currentFontName);
	}

	// Token: 0x0600482C RID: 18476 RVA: 0x001A1128 File Offset: 0x0019F328
	public static bool SwapToLocalizedFont(string fontname)
	{
		if (string.IsNullOrEmpty(fontname))
		{
			return false;
		}
		Localization.sFontAsset = Localization.GetFont(fontname);
		foreach (TextStyleSetting textStyleSetting in Resources.FindObjectsOfTypeAll<TextStyleSetting>())
		{
			if (textStyleSetting != null)
			{
				textStyleSetting.sdfFont = Localization.sFontAsset;
			}
		}
		bool isRightToLeft = Localization.IsRightToLeft;
		foreach (LocText locText in Resources.FindObjectsOfTypeAll<LocText>())
		{
			if (locText != null)
			{
				locText.SwapFont(Localization.sFontAsset, isRightToLeft);
			}
		}
		return true;
	}

	// Token: 0x0600482D RID: 18477 RVA: 0x001A11B0 File Offset: 0x0019F3B0
	private static bool SetFont(Type target_type, object target, TMP_FontAsset font, bool is_right_to_left, HashSet<MemberInfo> excluded_members)
	{
		if (target_type == null || target == null || font == null)
		{
			return false;
		}
		foreach (FieldInfo fieldInfo in target_type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
		{
			if (!excluded_members.Contains(fieldInfo))
			{
				if (fieldInfo.FieldType == typeof(TextStyleSetting))
				{
					((TextStyleSetting)fieldInfo.GetValue(target)).sdfFont = font;
				}
				else if (fieldInfo.FieldType == typeof(LocText))
				{
					((LocText)fieldInfo.GetValue(target)).SwapFont(font, is_right_to_left);
				}
				else if (fieldInfo.FieldType == typeof(GameObject))
				{
					foreach (Component component in ((GameObject)fieldInfo.GetValue(target)).GetComponents<Component>())
					{
						Localization.SetFont(component.GetType(), component, font, is_right_to_left, excluded_members);
					}
				}
				else if (fieldInfo.MemberType == MemberTypes.Field && fieldInfo.FieldType != fieldInfo.DeclaringType)
				{
					Localization.SetFont(fieldInfo.FieldType, fieldInfo.GetValue(target), font, is_right_to_left, excluded_members);
				}
			}
		}
		return true;
	}

	// Token: 0x0600482E RID: 18478 RVA: 0x001A12E9 File Offset: 0x0019F4E9
	public static bool SetFont<T>(T target, TMP_FontAsset font, bool is_right_to_left, HashSet<MemberInfo> excluded_members)
	{
		return Localization.SetFont(typeof(T), target, font, is_right_to_left, excluded_members);
	}

	// Token: 0x0600482F RID: 18479 RVA: 0x001A1304 File Offset: 0x0019F504
	public static TMP_FontAsset GetFont(string fontname)
	{
		foreach (TMP_FontAsset tmp_FontAsset in Resources.FindObjectsOfTypeAll<TMP_FontAsset>())
		{
			if (tmp_FontAsset.name == fontname)
			{
				return tmp_FontAsset;
			}
		}
		return null;
	}

	// Token: 0x06004830 RID: 18480 RVA: 0x001A133C File Offset: 0x0019F53C
	private static bool HasSameOrLessTokenCount(string english_string, string translated_string, string token)
	{
		int num = english_string.Split(new string[] { token }, StringSplitOptions.None).Length;
		int num2 = translated_string.Split(new string[] { token }, StringSplitOptions.None).Length;
		return num >= num2;
	}

	// Token: 0x06004831 RID: 18481 RVA: 0x001A1378 File Offset: 0x0019F578
	private static bool HasSameOrLessLinkCountAsEnglish(string english_string, string translated_string)
	{
		return Localization.HasSameOrLessTokenCount(english_string, translated_string, "<link") && Localization.HasSameOrLessTokenCount(english_string, translated_string, "</link");
	}

	// Token: 0x06004832 RID: 18482 RVA: 0x001A1398 File Offset: 0x0019F598
	private static bool HasMatchingLinkTags(string str, int idx = 0)
	{
		int num = str.IndexOf("<link", idx);
		int num2 = str.IndexOf("</link", idx);
		if (num == -1 && num2 == -1)
		{
			return true;
		}
		if (num == -1 && num2 != -1)
		{
			return false;
		}
		if (num != -1 && num2 == -1)
		{
			return false;
		}
		if (num2 < num)
		{
			return false;
		}
		int num3 = str.IndexOf("<link", num + 1);
		return (num < 0 || num3 == -1 || num3 >= num2) && Localization.HasMatchingLinkTags(str, num2 + 1);
	}

	// Token: 0x06004833 RID: 18483 RVA: 0x001A140C File Offset: 0x0019F60C
	private static bool AreParametersPreserved(string old_string, string new_string)
	{
		MatchCollection matchCollection = Regex.Matches(old_string, "({.[^}]*?})(?!.*\\1)");
		MatchCollection matchCollection2 = Regex.Matches(new_string, "({.[^}]*?})(?!.*\\1)");
		bool flag = false;
		if (matchCollection == null && matchCollection2 == null)
		{
			flag = true;
		}
		else if (matchCollection != null && matchCollection2 != null && matchCollection.Count == matchCollection2.Count)
		{
			flag = true;
			foreach (object obj in matchCollection)
			{
				string text = obj.ToString();
				bool flag2 = false;
				foreach (object obj2 in matchCollection2)
				{
					string text2 = obj2.ToString();
					if (text == text2)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					flag = false;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x06004834 RID: 18484 RVA: 0x001A1504 File Offset: 0x0019F704
	public static bool HasDirtyWords(string str)
	{
		return Localization.FilterDirtyWords(str) != str;
	}

	// Token: 0x06004835 RID: 18485 RVA: 0x001A1512 File Offset: 0x0019F712
	public static string FilterDirtyWords(string str)
	{
		return DistributionPlatform.Inst.ApplyWordFilter(str);
	}

	// Token: 0x06004836 RID: 18486 RVA: 0x001A151F File Offset: 0x0019F71F
	public static string GetFileDateFormat(int format_idx)
	{
		return "{" + format_idx.ToString() + ":dd / MMM / yyyy}";
	}

	// Token: 0x06004837 RID: 18487 RVA: 0x001A1538 File Offset: 0x0019F738
	public static void ClearLanguage()
	{
		DebugUtil.LogArgs(new object[] { " -> Clearing selected language! Either it didn't load correct or returning to english by menu." });
		Localization.sFontAsset = null;
		Localization.sLocale = null;
		KPlayerPrefs.SetString(Localization.SELECTED_LANGUAGE_TYPE_KEY, Localization.SelectedLanguageType.None.ToString());
		KPlayerPrefs.SetString(Localization.SELECTED_LANGUAGE_CODE_KEY, "");
		Localization.SwapToLocalizedFont(Localization.GetDefaultLocale().FontName);
		string defaultLocalizationFilePath = Localization.GetDefaultLocalizationFilePath();
		if (File.Exists(defaultLocalizationFilePath))
		{
			Localization.LoadTranslation(File.ReadAllLines(defaultLocalizationFilePath, Encoding.UTF8), true);
		}
		LanguageOptionsScreen.CleanUpSavedLanguageMod();
	}

	// Token: 0x06004838 RID: 18488 RVA: 0x001A15C4 File Offset: 0x0019F7C4
	private static string ReverseText(string source)
	{
		char[] array = new char[] { '\n' };
		string[] array2 = source.Split(array);
		string text = "";
		int num = 0;
		foreach (string text2 in array2)
		{
			num++;
			char[] array4 = new char[text2.Length];
			for (int j = 0; j < text2.Length; j++)
			{
				array4[array4.Length - 1 - j] = text2[j];
			}
			text += new string(array4);
			if (num < array2.Length)
			{
				text += "\n";
			}
		}
		return text;
	}

	// Token: 0x06004839 RID: 18489 RVA: 0x001A1668 File Offset: 0x0019F868
	public static string Fixup(string text)
	{
		if (Localization.sLocale != null && text != null && text != "" && Localization.sLocale.Lang == Localization.Language.Arabic)
		{
			return Localization.ReverseText(ArabicFixer.Fix(text));
		}
		return text;
	}

	// Token: 0x04002F9E RID: 12190
	private static TMP_FontAsset sFontAsset = null;

	// Token: 0x04002F9F RID: 12191
	private static readonly List<Localization.Locale> Locales = new List<Localization.Locale>
	{
		new Localization.Locale(Localization.Language.Chinese, Localization.Direction.LeftToRight, "zh", "NotoSansCJKsc-Regular"),
		new Localization.Locale(Localization.Language.Japanese, Localization.Direction.LeftToRight, "ja", "NotoSansCJKjp-Regular"),
		new Localization.Locale(Localization.Language.Korean, Localization.Direction.LeftToRight, "ko", "NotoSansCJKkr-Regular"),
		new Localization.Locale(Localization.Language.Russian, Localization.Direction.LeftToRight, "ru", "RobotoCondensed-Regular"),
		new Localization.Locale(Localization.Language.Thai, Localization.Direction.LeftToRight, "th", "NotoSansThai-Regular"),
		new Localization.Locale(Localization.Language.Arabic, Localization.Direction.RightToLeft, "ar", "NotoNaskhArabic-Regular"),
		new Localization.Locale(Localization.Language.Hebrew, Localization.Direction.RightToLeft, "he", "NotoSansHebrew-Regular"),
		new Localization.Locale(Localization.Language.Unspecified, Localization.Direction.LeftToRight, "", "RobotoCondensed-Regular")
	};

	// Token: 0x04002FA0 RID: 12192
	private static Localization.Locale sLocale = null;

	// Token: 0x04002FA1 RID: 12193
	private static string currentFontName = null;

	// Token: 0x04002FA2 RID: 12194
	public static string DEFAULT_LANGUAGE_CODE = "en";

	// Token: 0x04002FA3 RID: 12195
	public static readonly List<string> PreinstalledLanguages = new List<string>
	{
		Localization.DEFAULT_LANGUAGE_CODE,
		"zh_klei",
		"ko_klei",
		"ru_klei"
	};

	// Token: 0x04002FA4 RID: 12196
	public static string SELECTED_LANGUAGE_TYPE_KEY = "SelectedLanguageType";

	// Token: 0x04002FA5 RID: 12197
	public static string SELECTED_LANGUAGE_CODE_KEY = "SelectedLanguageCode";

	// Token: 0x04002FA6 RID: 12198
	private static Dictionary<string, List<Assembly>> translatable_assemblies = new Dictionary<string, List<Assembly>>();

	// Token: 0x04002FA7 RID: 12199
	public const BindingFlags non_static_data_member_fields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

	// Token: 0x04002FA8 RID: 12200
	private const string start_link_token = "<link";

	// Token: 0x04002FA9 RID: 12201
	private const string end_link_token = "</link";

	// Token: 0x020019B9 RID: 6585
	public enum Language
	{
		// Token: 0x04007D8B RID: 32139
		Chinese,
		// Token: 0x04007D8C RID: 32140
		Japanese,
		// Token: 0x04007D8D RID: 32141
		Korean,
		// Token: 0x04007D8E RID: 32142
		Russian,
		// Token: 0x04007D8F RID: 32143
		Thai,
		// Token: 0x04007D90 RID: 32144
		Arabic,
		// Token: 0x04007D91 RID: 32145
		Hebrew,
		// Token: 0x04007D92 RID: 32146
		Unspecified
	}

	// Token: 0x020019BA RID: 6586
	public enum Direction
	{
		// Token: 0x04007D94 RID: 32148
		LeftToRight,
		// Token: 0x04007D95 RID: 32149
		RightToLeft
	}

	// Token: 0x020019BB RID: 6587
	public class Locale
	{
		// Token: 0x0600A0B0 RID: 41136 RVA: 0x0039C1DF File Offset: 0x0039A3DF
		public Locale(Localization.Locale other)
		{
			this.mLanguage = other.mLanguage;
			this.mDirection = other.mDirection;
			this.mCode = other.mCode;
			this.mFontName = other.mFontName;
		}

		// Token: 0x0600A0B1 RID: 41137 RVA: 0x0039C217 File Offset: 0x0039A417
		public Locale(Localization.Language language, Localization.Direction direction, string code, string fontName)
		{
			this.mLanguage = language;
			this.mDirection = direction;
			this.mCode = code.ToLower();
			this.mFontName = fontName;
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x0600A0B2 RID: 41138 RVA: 0x0039C241 File Offset: 0x0039A441
		public Localization.Language Lang
		{
			get
			{
				return this.mLanguage;
			}
		}

		// Token: 0x0600A0B3 RID: 41139 RVA: 0x0039C249 File Offset: 0x0039A449
		public void SetCode(string code)
		{
			this.mCode = code;
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x0600A0B4 RID: 41140 RVA: 0x0039C252 File Offset: 0x0039A452
		public string Code
		{
			get
			{
				return this.mCode;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600A0B5 RID: 41141 RVA: 0x0039C25A File Offset: 0x0039A45A
		public string FontName
		{
			get
			{
				return this.mFontName;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600A0B6 RID: 41142 RVA: 0x0039C262 File Offset: 0x0039A462
		public bool IsRightToLeft
		{
			get
			{
				return this.mDirection == Localization.Direction.RightToLeft;
			}
		}

		// Token: 0x0600A0B7 RID: 41143 RVA: 0x0039C26D File Offset: 0x0039A46D
		public bool MatchesCode(string language_code)
		{
			return language_code.ToLower().Contains(this.mCode);
		}

		// Token: 0x0600A0B8 RID: 41144 RVA: 0x0039C280 File Offset: 0x0039A480
		public bool MatchesFont(string fontname)
		{
			return fontname.ToLower() == this.mFontName.ToLower();
		}

		// Token: 0x0600A0B9 RID: 41145 RVA: 0x0039C298 File Offset: 0x0039A498
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.mCode,
				":",
				this.mLanguage.ToString(),
				":",
				this.mDirection.ToString(),
				":",
				this.mFontName
			});
		}

		// Token: 0x04007D96 RID: 32150
		private Localization.Language mLanguage;

		// Token: 0x04007D97 RID: 32151
		private string mCode;

		// Token: 0x04007D98 RID: 32152
		private string mFontName;

		// Token: 0x04007D99 RID: 32153
		private Localization.Direction mDirection;
	}

	// Token: 0x020019BC RID: 6588
	private struct Entry
	{
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600A0BA RID: 41146 RVA: 0x0039C302 File Offset: 0x0039A502
		public bool IsPopulated
		{
			get
			{
				return this.msgctxt != null && this.msgstr != null && this.msgstr.Length > 0;
			}
		}

		// Token: 0x04007D9A RID: 32154
		public string msgctxt;

		// Token: 0x04007D9B RID: 32155
		public string msgstr;
	}

	// Token: 0x020019BD RID: 6589
	public enum SelectedLanguageType
	{
		// Token: 0x04007D9D RID: 32157
		None,
		// Token: 0x04007D9E RID: 32158
		Preinstalled,
		// Token: 0x04007D9F RID: 32159
		UGC
	}
}
