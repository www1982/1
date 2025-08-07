using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Klei;
using KMod;
using Newtonsoft.Json;
using Steamworks;
using STRINGS;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

// Token: 0x0200099F RID: 2463
public class KCrashReporter : MonoBehaviour
{
	// Token: 0x1400001C RID: 28
	// (add) Token: 0x06004765 RID: 18277 RVA: 0x0019B620 File Offset: 0x00199820
	// (remove) Token: 0x06004766 RID: 18278 RVA: 0x0019B654 File Offset: 0x00199854
	public static event Action<bool> onCrashReported;

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x06004767 RID: 18279 RVA: 0x0019B688 File Offset: 0x00199888
	// (remove) Token: 0x06004768 RID: 18280 RVA: 0x0019B6BC File Offset: 0x001998BC
	public static event Action<float> onCrashUploadProgress;

	// Token: 0x170004FF RID: 1279
	// (get) Token: 0x06004769 RID: 18281 RVA: 0x0019B6EF File Offset: 0x001998EF
	// (set) Token: 0x0600476A RID: 18282 RVA: 0x0019B6F6 File Offset: 0x001998F6
	public static bool hasReportedError { get; private set; }

	// Token: 0x0600476B RID: 18283 RVA: 0x0019B700 File Offset: 0x00199900
	private void OnEnable()
	{
		KCrashReporter.dataRoot = Application.dataPath;
		Application.logMessageReceived += this.HandleLog;
		KCrashReporter.ignoreAll = true;
		string text = Path.Combine(KCrashReporter.dataRoot, "hashes.json");
		if (File.Exists(text))
		{
			StringBuilder stringBuilder = new StringBuilder();
			MD5 md = MD5.Create();
			Dictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(text));
			if (dictionary.Count > 0)
			{
				bool flag = true;
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					string key = keyValuePair.Key;
					string value = keyValuePair.Value;
					stringBuilder.Length = 0;
					using (FileStream fileStream = new FileStream(Path.Combine(KCrashReporter.dataRoot, key), FileMode.Open, FileAccess.Read))
					{
						foreach (byte b in md.ComputeHash(fileStream))
						{
							stringBuilder.AppendFormat("{0:x2}", b);
						}
						if (stringBuilder.ToString() != value)
						{
							flag = false;
							break;
						}
					}
				}
				if (flag)
				{
					KCrashReporter.ignoreAll = false;
				}
			}
			else
			{
				KCrashReporter.ignoreAll = false;
			}
		}
		else
		{
			KCrashReporter.ignoreAll = false;
		}
		if (KCrashReporter.ignoreAll)
		{
			global::Debug.Log("Ignoring crash due to mismatched hashes.json entries.");
		}
		if (File.Exists("ignorekcrashreporter.txt"))
		{
			KCrashReporter.ignoreAll = true;
			global::Debug.Log("Ignoring crash due to ignorekcrashreporter.txt");
		}
		if (Application.isEditor && !GenericGameSettings.instance.enableEditorCrashReporting)
		{
			KCrashReporter.terminateOnError = false;
		}
	}

	// Token: 0x0600476C RID: 18284 RVA: 0x0019B8A8 File Offset: 0x00199AA8
	private void OnDisable()
	{
		Application.logMessageReceived -= this.HandleLog;
	}

	// Token: 0x0600476D RID: 18285 RVA: 0x0019B8BC File Offset: 0x00199ABC
	private void HandleLog(string msg, string stack_trace, LogType type)
	{
		if ((KCrashReporter.logCount += 1U) == 10000000U)
		{
			DebugUtil.DevLogError("Turning off logging to avoid increasing the file to an unreasonable size, please review the logs as they probably contain spam");
			global::Debug.DisableLogging();
		}
		if (KCrashReporter.ignoreAll)
		{
			return;
		}
		if (msg != null && msg.StartsWith(DebugUtil.START_CALLSTACK))
		{
			string text = msg;
			msg = text.Substring(text.IndexOf(DebugUtil.END_CALLSTACK, StringComparison.Ordinal) + DebugUtil.END_CALLSTACK.Length);
			stack_trace = text.Substring(DebugUtil.START_CALLSTACK.Length, text.IndexOf(DebugUtil.END_CALLSTACK, StringComparison.Ordinal) - DebugUtil.START_CALLSTACK.Length);
		}
		if (Array.IndexOf<string>(KCrashReporter.IgnoreStrings, msg) != -1)
		{
			return;
		}
		if (msg != null && msg.StartsWith("<RI.Hid>"))
		{
			return;
		}
		if (msg != null && msg.StartsWith("Failed to load cursor"))
		{
			return;
		}
		if (msg != null && msg.StartsWith("Failed to save a temporary cursor"))
		{
			return;
		}
		if (type == LogType.Exception)
		{
			RestartWarning.ShouldWarn = true;
		}
		if (this.errorScreen == null && (type == LogType.Exception || type == LogType.Error))
		{
			if (KCrashReporter.terminateOnError && KCrashReporter.hasCrash)
			{
				return;
			}
			if (SpeedControlScreen.Instance != null)
			{
				SpeedControlScreen.Instance.Pause(true, true);
			}
			string text2 = msg;
			string text3 = stack_trace;
			if (string.IsNullOrEmpty(text3))
			{
				text3 = new StackTrace(5, true).ToString();
			}
			if (App.isLoading)
			{
				if (!SceneInitializerLoader.deferred_error.IsValid)
				{
					SceneInitializerLoader.deferred_error = new SceneInitializerLoader.DeferredError
					{
						msg = text2,
						stack_trace = text3
					};
					return;
				}
			}
			else
			{
				this.ShowDialog(text2, text3, true, null, null);
			}
		}
	}

	// Token: 0x0600476E RID: 18286 RVA: 0x0019BA34 File Offset: 0x00199C34
	public bool ShowDialog(string error, string stack_trace, bool includeSaveFile = true, string[] extraCategories = null, string[] extraFiles = null)
	{
		if (this.errorScreen != null)
		{
			return false;
		}
		GameObject gameObject = GameObject.Find(KCrashReporter.error_canvas_name);
		if (gameObject == null)
		{
			gameObject = new GameObject();
			gameObject.name = KCrashReporter.error_canvas_name;
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;
			canvas.sortingOrder = 32767;
			gameObject.AddComponent<GraphicRaycaster>();
		}
		this.errorScreen = global::UnityEngine.Object.Instantiate<GameObject>(this.reportErrorPrefab, Vector3.zero, Quaternion.identity);
		this.errorScreen.transform.SetParent(gameObject.transform, false);
		ReportErrorDialog errorDialog = this.errorScreen.GetComponentInChildren<ReportErrorDialog>();
		string text = error + "\n\n" + stack_trace;
		KCrashReporter.hasCrash = true;
		if (Global.Instance != null && Global.Instance.modManager != null && Global.Instance.modManager.HasCrashableMods())
		{
			Exception ex = DebugUtil.RetrieveLastExceptionLogged();
			StackTrace stackTrace = ((ex != null) ? new StackTrace(ex) : new StackTrace(5, true));
			Global.Instance.modManager.SearchForModsInStackTrace(stackTrace);
			Global.Instance.modManager.SearchForModsInStackTrace(stack_trace);
			errorDialog.PopupDisableModsDialog(text, new global::System.Action(this.OnQuitToDesktop), (Global.Instance.modManager.IsInDevMode() || !KCrashReporter.terminateOnError) ? new global::System.Action(this.OnCloseErrorDialog) : null);
		}
		else
		{
			errorDialog.PopupSubmitErrorDialog(text, delegate
			{
				KCrashReporter.ReportError(error, stack_trace, this.confirmDialogPrefab, this.errorScreen, errorDialog.UserMessage(), includeSaveFile, extraCategories, extraFiles);
			}, new global::System.Action(this.OnQuitToDesktop), KCrashReporter.terminateOnError ? null : new global::System.Action(this.OnCloseErrorDialog));
		}
		return true;
	}

	// Token: 0x0600476F RID: 18287 RVA: 0x0019BC1C File Offset: 0x00199E1C
	private void OnCloseErrorDialog()
	{
		global::UnityEngine.Object.Destroy(this.errorScreen);
		this.errorScreen = null;
		KCrashReporter.hasCrash = false;
		if (SpeedControlScreen.Instance != null)
		{
			SpeedControlScreen.Instance.Unpause(true);
		}
	}

	// Token: 0x06004770 RID: 18288 RVA: 0x0019BC4E File Offset: 0x00199E4E
	private void OnQuitToDesktop()
	{
		App.Quit();
	}

	// Token: 0x06004771 RID: 18289 RVA: 0x0019BC58 File Offset: 0x00199E58
	private static string GetUserID()
	{
		if (DistributionPlatform.Initialized)
		{
			string[] array = new string[5];
			array[0] = DistributionPlatform.Inst.Name;
			array[1] = "ID_";
			array[2] = DistributionPlatform.Inst.LocalUser.Name;
			array[3] = "_";
			int num = 4;
			DistributionPlatform.UserId id = DistributionPlatform.Inst.LocalUser.Id;
			array[num] = ((id != null) ? id.ToString() : null);
			return string.Concat(array);
		}
		return "LocalUser_" + Environment.UserName;
	}

	// Token: 0x06004772 RID: 18290 RVA: 0x0019BCD4 File Offset: 0x00199ED4
	private static string GetLogContents()
	{
		string text = Util.LogFilePath();
		if (File.Exists(text))
		{
			using (FileStream fileStream = File.Open(text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					return streamReader.ReadToEnd();
				}
			}
		}
		return "";
	}

	// Token: 0x06004773 RID: 18291 RVA: 0x0019BD40 File Offset: 0x00199F40
	public static void ReportDevNotification(string notification_name, string stack_trace, string details = "", bool includeSaveFile = false, string[] extraCategories = null)
	{
		if (KCrashReporter.previouslyReportedDevNotifications == null)
		{
			KCrashReporter.previouslyReportedDevNotifications = new HashSet<int>();
		}
		details = notification_name + " - " + details;
		global::Debug.Log(details);
		int hashValue = new HashedString(notification_name).HashValue;
		bool hasReportedError = KCrashReporter.hasReportedError;
		if (!KCrashReporter.previouslyReportedDevNotifications.Contains(hashValue))
		{
			KCrashReporter.previouslyReportedDevNotifications.Add(hashValue);
			if (extraCategories != null)
			{
				Array.Resize<string>(ref extraCategories, extraCategories.Length + 1);
				extraCategories[extraCategories.Length - 1] = KCrashReporter.CRASH_CATEGORY.DEVNOTIFICATION;
			}
			else
			{
				extraCategories = new string[] { KCrashReporter.CRASH_CATEGORY.DEVNOTIFICATION };
			}
			KCrashReporter.ReportError("DevNotification: " + notification_name, stack_trace, null, null, details, includeSaveFile, extraCategories, null);
		}
		KCrashReporter.hasReportedError = hasReportedError;
	}

	// Token: 0x06004774 RID: 18292 RVA: 0x0019BDF0 File Offset: 0x00199FF0
	public static void ReportError(string msg, string stack_trace, ConfirmDialogScreen confirm_prefab, GameObject confirm_parent, string userMessage = "", bool includeSaveFile = true, string[] extraCategories = null, string[] extraFiles = null)
	{
		if (KPrivacyPrefs.instance.disableDataCollection)
		{
			return;
		}
		if (KCrashReporter.ignoreAll)
		{
			return;
		}
		global::Debug.Log("Reporting error.\n");
		if (msg != null)
		{
			global::Debug.Log(msg);
		}
		if (stack_trace != null)
		{
			global::Debug.Log(stack_trace);
		}
		KCrashReporter.hasReportedError = true;
		if (string.IsNullOrEmpty(msg))
		{
			msg = "No message";
		}
		Match match = KCrashReporter.failedToLoadModuleRegEx.Match(msg);
		if (match.Success)
		{
			string text = match.Groups[1].ToString();
			string text2 = match.Groups[2].ToString();
			string fileName = Path.GetFileName(text);
			msg = string.Concat(new string[] { "Failed to load '", fileName, "' with error '", text2, "'." });
		}
		if (string.IsNullOrEmpty(stack_trace))
		{
			string buildText = BuildWatermark.GetBuildText();
			stack_trace = string.Format("No stack trace {0}\n\n{1}", buildText, msg);
		}
		List<string> list = new List<string>();
		if (KCrashReporter.debugWasUsed)
		{
			list.Add("(Debug Used)");
		}
		if (KCrashReporter.haveActiveMods)
		{
			list.Add("(Mods Active)");
		}
		list.Add(msg);
		string[] array = new string[] { "Debug:LogError", "UnityEngine.Debug", "Output:LogError", "DebugUtil:Assert", "System.Array", "System.Collections", "KCrashReporter.Assert", "No stack trace." };
		foreach (string text3 in stack_trace.Split('\n', StringSplitOptions.None))
		{
			if (list.Count >= 5)
			{
				break;
			}
			if (!string.IsNullOrEmpty(text3))
			{
				bool flag = false;
				foreach (string text4 in array)
				{
					if (text3.StartsWith(text4))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(text3);
				}
			}
		}
		if (userMessage == UI.CRASHSCREEN.BODY.text || userMessage.IsNullOrWhiteSpace())
		{
			userMessage = "";
		}
		else
		{
			userMessage = "[" + BuildWatermark.GetBuildText() + "] " + userMessage;
		}
		userMessage = userMessage.Replace(stack_trace, "");
		KCrashReporter.Error error = new KCrashReporter.Error();
		if (extraCategories != null)
		{
			error.categories.AddRange(extraCategories);
		}
		error.callstack = stack_trace;
		if (KCrashReporter.disableDeduping)
		{
			error.callstack = error.callstack + "\n" + Guid.NewGuid().ToString();
		}
		error.fullstack = string.Format("{0}\n\n{1}", msg, stack_trace);
		error.summaryline = string.Join("\n", list.ToArray());
		error.userMessage = userMessage;
		List<string> list2 = new List<string>();
		if (includeSaveFile && KCrashReporter.MOST_RECENT_SAVEFILE != null)
		{
			list2.Add(KCrashReporter.MOST_RECENT_SAVEFILE);
			error.saveFilename = Path.GetFileName(KCrashReporter.MOST_RECENT_SAVEFILE);
		}
		if (extraFiles != null)
		{
			foreach (string text5 in extraFiles)
			{
				list2.Add(text5);
				error.extraFilenames.Add(Path.GetFileName(text5));
			}
		}
		string text6 = JsonConvert.SerializeObject(error);
		byte[] array4 = KCrashReporter.CreateArchiveZip(KCrashReporter.GetLogContents(), list2);
		global::System.Action action = delegate
		{
			if (confirm_prefab != null && confirm_parent != null)
			{
				((ConfirmDialogScreen)KScreenManager.Instance.StartScreen(confirm_prefab.gameObject, confirm_parent)).PopupConfirmDialog(UI.CRASHSCREEN.REPORTEDERROR_SUCCESS, null, null, null, null, null, null, null, null);
			}
		};
		Action<long> action2 = delegate(long errorCode)
		{
			if (confirm_prefab != null && confirm_parent != null)
			{
				string text7 = ((errorCode == 413L) ? UI.CRASHSCREEN.REPORTEDERROR_FAILURE_TOO_LARGE : UI.CRASHSCREEN.REPORTEDERROR_FAILURE);
				((ConfirmDialogScreen)KScreenManager.Instance.StartScreen(confirm_prefab.gameObject, confirm_parent)).PopupConfirmDialog(text7, null, null, null, null, null, null, null, null);
			}
		};
		KCrashReporter.pendingCrash = new KCrashReporter.PendingCrash
		{
			jsonString = text6,
			archiveData = array4,
			successCallback = action,
			failureCallback = action2
		};
	}

	// Token: 0x06004775 RID: 18293 RVA: 0x0019C16B File Offset: 0x0019A36B
	private static IEnumerator SubmitCrashAsync(string jsonString, byte[] archiveData, global::System.Action successCallback, Action<long> failureCallback)
	{
		bool success = false;
		Uri uri = new Uri("https://games-feedback.klei.com/submit");
		List<IMultipartFormSection> list = new List<IMultipartFormSection>
		{
			new MultipartFormDataSection("metadata", jsonString),
			new MultipartFormFileSection("archiveFile", archiveData, "Archive.zip", "application/octet-stream")
		};
		if (KleiAccount.KleiToken != null)
		{
			list.Add(new MultipartFormDataSection("loginToken", KleiAccount.KleiToken));
		}
		using (UnityWebRequest w = UnityWebRequest.Post(uri, list))
		{
			w.SendWebRequest();
			while (!w.isDone)
			{
				yield return null;
				if (KCrashReporter.onCrashUploadProgress != null)
				{
					KCrashReporter.onCrashUploadProgress(w.uploadProgress);
				}
			}
			if (w.result == UnityWebRequest.Result.Success)
			{
				global::UnityEngine.Debug.Log("Submitted crash!");
				if (successCallback != null)
				{
					successCallback();
				}
				success = true;
			}
			else
			{
				global::UnityEngine.Debug.Log("CrashReporter: Could not submit crash " + w.result.ToString());
				if (failureCallback != null)
				{
					failureCallback(w.responseCode);
				}
			}
		}
		UnityWebRequest w = null;
		if (KCrashReporter.onCrashReported != null)
		{
			KCrashReporter.onCrashReported(success);
		}
		yield break;
		yield break;
	}

	// Token: 0x06004776 RID: 18294 RVA: 0x0019C190 File Offset: 0x0019A390
	public static void ReportBug(string msg, GameObject confirmParent)
	{
		string text = "Bug Report From: " + KCrashReporter.GetUserID() + " at " + global::System.DateTime.Now.ToString();
		KCrashReporter.ReportError(msg, text, ScreenPrefabs.Instance.ConfirmDialogScreen, confirmParent, "", true, null, null);
	}

	// Token: 0x06004777 RID: 18295 RVA: 0x0019C1DC File Offset: 0x0019A3DC
	public static void Assert(bool condition, string message, string[] extraCategories = null)
	{
		if (!condition && !KCrashReporter.hasReportedError)
		{
			StackTrace stackTrace = new StackTrace(1, true);
			KCrashReporter.ReportError("ASSERT: " + message, stackTrace.ToString(), null, null, null, true, extraCategories, null);
		}
	}

	// Token: 0x06004778 RID: 18296 RVA: 0x0019C217 File Offset: 0x0019A417
	public static void ReportSimDLLCrash(string msg, string stack_trace, string dmp_filename)
	{
		if (KCrashReporter.hasReportedError)
		{
			return;
		}
		KCrashReporter.pendingReport = new KCrashReporter.PendingReport(msg, stack_trace, dmp_filename);
	}

	// Token: 0x06004779 RID: 18297 RVA: 0x0019C230 File Offset: 0x0019A430
	private static byte[] CreateArchiveZip(string log, List<string> files)
	{
		byte[] array2;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
			{
				if (files != null)
				{
					foreach (string text in files)
					{
						try
						{
							if (!File.Exists(text))
							{
								global::UnityEngine.Debug.Log("CrashReporter: file does not exist to include: " + text);
							}
							else
							{
								using (Stream stream = zipArchive.CreateEntry(Path.GetFileName(text), global::System.IO.Compression.CompressionLevel.Fastest).Open())
								{
									byte[] array = File.ReadAllBytes(text);
									stream.Write(array, 0, array.Length);
								}
							}
						}
						catch (Exception ex)
						{
							string text2 = "CrashReporter: Could not add file '";
							string text3 = text;
							string text4 = "' to archive: ";
							Exception ex2 = ex;
							global::UnityEngine.Debug.Log(text2 + text3 + text4 + ((ex2 != null) ? ex2.ToString() : null));
						}
					}
					using (Stream stream2 = zipArchive.CreateEntry("Player.log", global::System.IO.Compression.CompressionLevel.Fastest).Open())
					{
						byte[] bytes = Encoding.UTF8.GetBytes(log);
						stream2.Write(bytes, 0, bytes.Length);
					}
				}
			}
			array2 = memoryStream.ToArray();
		}
		return array2;
	}

	// Token: 0x0600477A RID: 18298 RVA: 0x0019C3F0 File Offset: 0x0019A5F0
	private void Update()
	{
		if (KCrashReporter.pendingReport != null)
		{
			KCrashReporter.PendingReport pendingReport = KCrashReporter.pendingReport;
			KCrashReporter.pendingReport = null;
			if (KCrashReporter.hasReportedError)
			{
				return;
			}
			KCrashReporter component = Global.Instance.GetComponent<KCrashReporter>();
			if (component != null)
			{
				component.ShowDialog(pendingReport.message, pendingReport.stack_trace, true, new string[] { KCrashReporter.CRASH_CATEGORY.SIM }, new string[] { pendingReport.additional_filename });
			}
			else
			{
				KCrashReporter.ReportError(pendingReport.message, pendingReport.stack_trace, null, null, "", true, new string[] { KCrashReporter.CRASH_CATEGORY.SIM }, new string[] { pendingReport.additional_filename });
			}
		}
		if (KCrashReporter.pendingCrash != null)
		{
			KCrashReporter.PendingCrash pendingCrash = KCrashReporter.pendingCrash;
			KCrashReporter.pendingCrash = null;
			global::Debug.Log("Submitting crash...");
			base.StartCoroutine(KCrashReporter.SubmitCrashAsync(pendingCrash.jsonString, pendingCrash.archiveData, pendingCrash.successCallback, pendingCrash.failureCallback));
		}
	}

	// Token: 0x04002F2F RID: 12079
	public static string MOST_RECENT_SAVEFILE = null;

	// Token: 0x04002F30 RID: 12080
	public const string CRASH_REPORTER_SERVER = "https://games-feedback.klei.com";

	// Token: 0x04002F31 RID: 12081
	public const uint MAX_LOGS = 10000000U;

	// Token: 0x04002F34 RID: 12084
	public static bool ignoreAll = false;

	// Token: 0x04002F35 RID: 12085
	public static bool debugWasUsed = false;

	// Token: 0x04002F36 RID: 12086
	public static bool haveActiveMods = false;

	// Token: 0x04002F37 RID: 12087
	public static uint logCount = 0U;

	// Token: 0x04002F38 RID: 12088
	public static string error_canvas_name = "ErrorCanvas";

	// Token: 0x04002F39 RID: 12089
	public static bool disableDeduping = false;

	// Token: 0x04002F3B RID: 12091
	public static bool hasCrash = false;

	// Token: 0x04002F3C RID: 12092
	private static readonly Regex failedToLoadModuleRegEx = new Regex("^Failed to load '(.*?)' with error (.*)", RegexOptions.Multiline);

	// Token: 0x04002F3D RID: 12093
	[SerializeField]
	private LoadScreen loadScreenPrefab;

	// Token: 0x04002F3E RID: 12094
	[SerializeField]
	private GameObject reportErrorPrefab;

	// Token: 0x04002F3F RID: 12095
	[SerializeField]
	private ConfirmDialogScreen confirmDialogPrefab;

	// Token: 0x04002F40 RID: 12096
	private GameObject errorScreen;

	// Token: 0x04002F41 RID: 12097
	public static bool terminateOnError = true;

	// Token: 0x04002F42 RID: 12098
	private static string dataRoot;

	// Token: 0x04002F43 RID: 12099
	private static readonly string[] IgnoreStrings = new string[] { "Releasing render texture whose render buffer is set as Camera's target buffer with Camera.SetTargetBuffers!", "The profiler has run out of samples for this frame. This frame will be skipped. Increase the sample limit using Profiler.maxNumberOfSamplesPerFrame", "Trying to add Text (LocText) for graphic rebuild while we are already inside a graphic rebuild loop. This is not supported.", "Texture has out of range width / height", "<I> Failed to get cursor position:\r\nSuccess.\r\n" };

	// Token: 0x04002F44 RID: 12100
	private static HashSet<int> previouslyReportedDevNotifications;

	// Token: 0x04002F45 RID: 12101
	private static KCrashReporter.PendingReport pendingReport;

	// Token: 0x04002F46 RID: 12102
	private static KCrashReporter.PendingCrash pendingCrash;

	// Token: 0x020019A4 RID: 6564
	public class CRASH_CATEGORY
	{
		// Token: 0x04007D05 RID: 32005
		public static string DEVNOTIFICATION = "DevNotification";

		// Token: 0x04007D06 RID: 32006
		public static string VANILLA = "Vanilla";

		// Token: 0x04007D07 RID: 32007
		public static string SPACEDOUT = "SpacedOut";

		// Token: 0x04007D08 RID: 32008
		public static string MODDED = "Modded";

		// Token: 0x04007D09 RID: 32009
		public static string DEBUGUSED = "DebugUsed";

		// Token: 0x04007D0A RID: 32010
		public static string SANDBOX = "Sandbox";

		// Token: 0x04007D0B RID: 32011
		public static string STEAMDECK = "SteamDeck";

		// Token: 0x04007D0C RID: 32012
		public static string SIM = "SimDll";

		// Token: 0x04007D0D RID: 32013
		public static string FILEIO = "FileIO";

		// Token: 0x04007D0E RID: 32014
		public static string MODSYSTEM = "ModSystem";

		// Token: 0x04007D0F RID: 32015
		public static string WORLDGENFAILURE = "WorldgenFailure";
	}

	// Token: 0x020019A5 RID: 6565
	private class Error
	{
		// Token: 0x0600A019 RID: 40985 RVA: 0x0039AE2C File Offset: 0x0039902C
		public Error()
		{
			this.userName = KCrashReporter.GetUserID();
			this.platform = Util.GetOperatingSystem();
			this.InitDefaultCategories();
			this.InitSku();
			this.InitSlackSummary();
			if (DistributionPlatform.Inst.Initialized)
			{
				string text;
				bool flag = !SteamApps.GetCurrentBetaName(out text, 100);
				this.branch = text;
				if (text == "public_playtest")
				{
					this.branch = "public_testing";
				}
				if (flag || (text == "public_testing" && !global::UnityEngine.Debug.isDebugBuild))
				{
					this.branch = "default";
				}
			}
		}

		// Token: 0x0600A01A RID: 40986 RVA: 0x0039AF78 File Offset: 0x00399178
		private void InitDefaultCategories()
		{
			if (DlcManager.IsPureVanilla())
			{
				this.categories.Add(KCrashReporter.CRASH_CATEGORY.VANILLA);
			}
			if (DlcManager.IsExpansion1Active())
			{
				this.categories.Add(KCrashReporter.CRASH_CATEGORY.SPACEDOUT);
			}
			foreach (string text in DlcManager.GetActiveDLCIds())
			{
				if (!(text == "EXPANSION1_ID"))
				{
					this.categories.Add(text);
				}
			}
			if (KCrashReporter.debugWasUsed)
			{
				this.categories.Add(KCrashReporter.CRASH_CATEGORY.DEBUGUSED);
			}
			if (KCrashReporter.haveActiveMods)
			{
				this.categories.Add(KCrashReporter.CRASH_CATEGORY.MODDED);
			}
			if (SaveGame.Instance != null && SaveGame.Instance.sandboxEnabled)
			{
				this.categories.Add(KCrashReporter.CRASH_CATEGORY.SANDBOX);
			}
			if (DistributionPlatform.Inst.Initialized && SteamUtils.IsSteamRunningOnSteamDeck())
			{
				this.categories.Add(KCrashReporter.CRASH_CATEGORY.STEAMDECK);
			}
		}

		// Token: 0x0600A01B RID: 40987 RVA: 0x0039B084 File Offset: 0x00399284
		private void InitSku()
		{
			this.sku = "steam";
			if (DistributionPlatform.Inst.Initialized)
			{
				string text;
				bool flag = !SteamApps.GetCurrentBetaName(out text, 100);
				if (text == "public_testing" || text == "preview" || text == "public_playtest" || text == "playtest")
				{
					if (global::UnityEngine.Debug.isDebugBuild)
					{
						this.sku = "steam-public-testing";
					}
					else
					{
						this.sku = "steam-release";
					}
				}
				if (flag || text == "release")
				{
					this.sku = "steam-release";
				}
			}
		}

		// Token: 0x0600A01C RID: 40988 RVA: 0x0039B124 File Offset: 0x00399324
		private void InitSlackSummary()
		{
			string buildText = BuildWatermark.GetBuildText();
			string text = ((GameClock.Instance != null) ? string.Format(" - Cycle {0}", GameClock.Instance.GetCycle() + 1) : "");
			int num;
			if (!(Global.Instance != null) || Global.Instance.modManager == null)
			{
				num = 0;
			}
			else
			{
				num = Global.Instance.modManager.mods.Count((Mod x) => x.IsEnabledForActiveDlc());
			}
			int num2 = num;
			string text2 = ((num2 > 0) ? string.Format(" - {0} active mods", num2) : "");
			this.slackSummary = string.Concat(new string[] { buildText, " ", this.platform, text, text2 });
		}

		// Token: 0x04007D10 RID: 32016
		public string game = "ONI";

		// Token: 0x04007D11 RID: 32017
		public string userName;

		// Token: 0x04007D12 RID: 32018
		public string platform;

		// Token: 0x04007D13 RID: 32019
		public string version = LaunchInitializer.BuildPrefix();

		// Token: 0x04007D14 RID: 32020
		public string branch = "default";

		// Token: 0x04007D15 RID: 32021
		public string sku = "";

		// Token: 0x04007D16 RID: 32022
		public int build = 679336;

		// Token: 0x04007D17 RID: 32023
		public string callstack = "";

		// Token: 0x04007D18 RID: 32024
		public string fullstack = "";

		// Token: 0x04007D19 RID: 32025
		public string summaryline = "";

		// Token: 0x04007D1A RID: 32026
		public string userMessage = "";

		// Token: 0x04007D1B RID: 32027
		public List<string> categories = new List<string>();

		// Token: 0x04007D1C RID: 32028
		public string slackSummary;

		// Token: 0x04007D1D RID: 32029
		public string logFilename = "Player.log";

		// Token: 0x04007D1E RID: 32030
		public string saveFilename = "";

		// Token: 0x04007D1F RID: 32031
		public string screenshotFilename = "";

		// Token: 0x04007D20 RID: 32032
		public List<string> extraFilenames = new List<string>();

		// Token: 0x04007D21 RID: 32033
		public string title = "";

		// Token: 0x04007D22 RID: 32034
		public bool isServer;

		// Token: 0x04007D23 RID: 32035
		public bool isDedicated;

		// Token: 0x04007D24 RID: 32036
		public bool isError = true;

		// Token: 0x04007D25 RID: 32037
		public string emote = "";
	}

	// Token: 0x020019A6 RID: 6566
	public class PendingCrash
	{
		// Token: 0x04007D26 RID: 32038
		public string jsonString;

		// Token: 0x04007D27 RID: 32039
		public byte[] archiveData;

		// Token: 0x04007D28 RID: 32040
		public global::System.Action successCallback;

		// Token: 0x04007D29 RID: 32041
		public Action<long> failureCallback;
	}

	// Token: 0x020019A7 RID: 6567
	public class PendingReport
	{
		// Token: 0x0600A01E RID: 40990 RVA: 0x0039B209 File Offset: 0x00399409
		public PendingReport(string msg, string stack_trace, string filename)
		{
			this.message = msg;
			this.stack_trace = stack_trace;
			this.additional_filename = filename;
		}

		// Token: 0x04007D2A RID: 32042
		public string message;

		// Token: 0x04007D2B RID: 32043
		public string stack_trace;

		// Token: 0x04007D2C RID: 32044
		public string additional_filename;
	}
}
