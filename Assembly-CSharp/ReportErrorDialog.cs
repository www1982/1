using System;
using System.Collections.Generic;
using KMod;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DA5 RID: 3493
public class ReportErrorDialog : MonoBehaviour
{
	// Token: 0x06006D6B RID: 28011 RVA: 0x002969CC File Offset: 0x00294BCC
	private void Start()
	{
		ThreadedHttps<KleiMetrics>.Instance.EndSession(true);
		if (KScreenManager.Instance)
		{
			KScreenManager.Instance.DisableInput(true);
		}
		this.StackTrace.SetActive(false);
		this.CrashLabel.text = ((this.mode == ReportErrorDialog.Mode.SubmitError) ? UI.CRASHSCREEN.TITLE : UI.CRASHSCREEN.TITLE_MODS);
		this.CrashDescription.SetActive(this.mode == ReportErrorDialog.Mode.SubmitError);
		this.ModsInfo.SetActive(this.mode == ReportErrorDialog.Mode.DisableMods);
		if (this.mode == ReportErrorDialog.Mode.DisableMods)
		{
			this.BuildModsList();
		}
		this.submitButton.gameObject.SetActive(this.submitAction != null);
		this.submitButton.onClick += this.OnSelect_SUBMIT;
		this.moreInfoButton.onClick += this.OnSelect_MOREINFO;
		this.continueGameButton.gameObject.SetActive(this.continueAction != null);
		this.continueGameButton.onClick += this.OnSelect_CONTINUE;
		this.quitButton.onClick += this.OnSelect_QUIT;
		this.messageInputField.text = UI.CRASHSCREEN.BODY;
		KCrashReporter.onCrashReported += this.OpenRefMessage;
		KCrashReporter.onCrashUploadProgress += this.UpdateProgressBar;
	}

	// Token: 0x06006D6C RID: 28012 RVA: 0x00296B28 File Offset: 0x00294D28
	private void BuildModsList()
	{
		DebugUtil.Assert(Global.Instance != null && Global.Instance.modManager != null);
		Manager mod_mgr = Global.Instance.modManager;
		List<Mod> allCrashableMods = mod_mgr.GetAllCrashableMods();
		allCrashableMods.Sort((Mod x, Mod y) => y.foundInStackTrace.CompareTo(x.foundInStackTrace));
		foreach (Mod mod in allCrashableMods)
		{
			if (mod.foundInStackTrace && mod.label.distribution_platform != Label.DistributionPlatform.Dev)
			{
				mod_mgr.EnableMod(mod.label, false, this);
			}
			HierarchyReferences hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.modEntryPrefab, this.modEntryParent.gameObject, false);
			LocText reference = hierarchyReferences.GetReference<LocText>("Title");
			reference.text = mod.title;
			reference.color = (mod.foundInStackTrace ? Color.red : Color.white);
			MultiToggle toggle = hierarchyReferences.GetReference<MultiToggle>("EnabledToggle");
			toggle.ChangeState(mod.IsEnabledForActiveDlc() ? 1 : 0);
			Label mod_label = mod.label;
			MultiToggle toggle2 = toggle;
			toggle2.onClick = (global::System.Action)Delegate.Combine(toggle2.onClick, new global::System.Action(delegate
			{
				bool flag = !mod_mgr.IsModEnabled(mod_label);
				toggle.ChangeState(flag ? 1 : 0);
				mod_mgr.EnableMod(mod_label, flag, this);
			}));
			toggle.GetComponent<ToolTip>().OnToolTip = () => mod_mgr.IsModEnabled(mod_label) ? UI.FRONTEND.MODS.TOOLTIPS.ENABLED : UI.FRONTEND.MODS.TOOLTIPS.DISABLED;
			hierarchyReferences.gameObject.SetActive(true);
		}
	}

	// Token: 0x06006D6D RID: 28013 RVA: 0x00296CFC File Offset: 0x00294EFC
	private void Update()
	{
		global::Debug.developerConsoleVisible = false;
	}

	// Token: 0x06006D6E RID: 28014 RVA: 0x00296D04 File Offset: 0x00294F04
	private void OnDestroy()
	{
		if (KCrashReporter.terminateOnError)
		{
			App.Quit();
		}
		if (KScreenManager.Instance)
		{
			KScreenManager.Instance.DisableInput(false);
		}
		KCrashReporter.onCrashReported -= this.OpenRefMessage;
		KCrashReporter.onCrashUploadProgress -= this.UpdateProgressBar;
	}

	// Token: 0x06006D6F RID: 28015 RVA: 0x00296D56 File Offset: 0x00294F56
	public void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.OnSelect_QUIT();
		}
	}

	// Token: 0x06006D70 RID: 28016 RVA: 0x00296D67 File Offset: 0x00294F67
	public void PopupSubmitErrorDialog(string stackTrace, global::System.Action onSubmit, global::System.Action onQuit, global::System.Action onContinue)
	{
		this.mode = ReportErrorDialog.Mode.SubmitError;
		this.m_stackTrace = stackTrace;
		this.submitAction = onSubmit;
		this.quitAction = onQuit;
		this.continueAction = onContinue;
	}

	// Token: 0x06006D71 RID: 28017 RVA: 0x00296D8D File Offset: 0x00294F8D
	public void PopupDisableModsDialog(string stackTrace, global::System.Action onQuit, global::System.Action onContinue)
	{
		this.mode = ReportErrorDialog.Mode.DisableMods;
		this.m_stackTrace = stackTrace;
		this.quitAction = onQuit;
		this.continueAction = onContinue;
	}

	// Token: 0x06006D72 RID: 28018 RVA: 0x00296DAC File Offset: 0x00294FAC
	public void OnSelect_MOREINFO()
	{
		this.StackTrace.GetComponentInChildren<LocText>().text = this.m_stackTrace;
		this.StackTrace.SetActive(true);
		this.moreInfoButton.GetComponentInChildren<LocText>().text = UI.CRASHSCREEN.COPYTOCLIPBOARDBUTTON;
		this.moreInfoButton.ClearOnClick();
		this.moreInfoButton.onClick += this.OnSelect_COPYTOCLIPBOARD;
	}

	// Token: 0x06006D73 RID: 28019 RVA: 0x00296E17 File Offset: 0x00295017
	public void OnSelect_COPYTOCLIPBOARD()
	{
		TextEditor textEditor = new TextEditor();
		textEditor.text = this.m_stackTrace + "\nBuild: " + BuildWatermark.GetBuildText();
		textEditor.SelectAll();
		textEditor.Copy();
	}

	// Token: 0x06006D74 RID: 28020 RVA: 0x00296E44 File Offset: 0x00295044
	public void OnSelect_SUBMIT()
	{
		this.submitButton.GetComponentInChildren<LocText>().text = UI.CRASHSCREEN.REPORTING;
		this.submitButton.GetComponent<KButton>().isInteractable = false;
		this.Submit();
	}

	// Token: 0x06006D75 RID: 28021 RVA: 0x00296E77 File Offset: 0x00295077
	public void OnSelect_QUIT()
	{
		if (this.quitAction != null)
		{
			this.quitAction();
		}
	}

	// Token: 0x06006D76 RID: 28022 RVA: 0x00296E8C File Offset: 0x0029508C
	public void OnSelect_CONTINUE()
	{
		if (this.continueAction != null)
		{
			this.continueAction();
		}
	}

	// Token: 0x06006D77 RID: 28023 RVA: 0x00296EA4 File Offset: 0x002950A4
	public void OpenRefMessage(bool success)
	{
		this.submitButton.gameObject.SetActive(false);
		this.uploadInProgress.SetActive(false);
		this.referenceMessage.SetActive(true);
		this.messageText.text = (success ? UI.CRASHSCREEN.THANKYOU : UI.CRASHSCREEN.UPLOAD_FAILED);
		this.m_crashSubmitted = success;
	}

	// Token: 0x06006D78 RID: 28024 RVA: 0x00296F00 File Offset: 0x00295100
	public void OpenUploadingMessagee()
	{
		this.submitButton.gameObject.SetActive(false);
		this.uploadInProgress.SetActive(true);
		this.referenceMessage.SetActive(false);
		this.progressBar.fillAmount = 0f;
		this.progressText.text = UI.CRASHSCREEN.UPLOADINPROGRESS.Replace("{0}", GameUtil.GetFormattedPercent(0f, GameUtil.TimeSlice.None));
	}

	// Token: 0x06006D79 RID: 28025 RVA: 0x00296F6B File Offset: 0x0029516B
	public void OnSelect_MESSAGE()
	{
		if (!this.m_crashSubmitted)
		{
			Application.OpenURL("https://forums.kleientertainment.com/klei-bug-tracker/oni/");
		}
	}

	// Token: 0x06006D7A RID: 28026 RVA: 0x00296F7F File Offset: 0x0029517F
	public string UserMessage()
	{
		return this.messageInputField.text;
	}

	// Token: 0x06006D7B RID: 28027 RVA: 0x00296F8C File Offset: 0x0029518C
	private void Submit()
	{
		this.submitAction();
		this.OpenUploadingMessagee();
	}

	// Token: 0x06006D7C RID: 28028 RVA: 0x00296F9F File Offset: 0x0029519F
	public void UpdateProgressBar(float progress)
	{
		this.progressBar.fillAmount = progress;
		this.progressText.text = UI.CRASHSCREEN.UPLOADINPROGRESS.Replace("{0}", GameUtil.GetFormattedPercent(progress * 100f, GameUtil.TimeSlice.None));
	}

	// Token: 0x04004ABA RID: 19130
	private global::System.Action submitAction;

	// Token: 0x04004ABB RID: 19131
	private global::System.Action quitAction;

	// Token: 0x04004ABC RID: 19132
	private global::System.Action continueAction;

	// Token: 0x04004ABD RID: 19133
	public KInputTextField messageInputField;

	// Token: 0x04004ABE RID: 19134
	[Header("Message")]
	public GameObject referenceMessage;

	// Token: 0x04004ABF RID: 19135
	public LocText messageText;

	// Token: 0x04004AC0 RID: 19136
	[Header("Upload Progress")]
	public GameObject uploadInProgress;

	// Token: 0x04004AC1 RID: 19137
	public Image progressBar;

	// Token: 0x04004AC2 RID: 19138
	public LocText progressText;

	// Token: 0x04004AC3 RID: 19139
	private string m_stackTrace;

	// Token: 0x04004AC4 RID: 19140
	private bool m_crashSubmitted;

	// Token: 0x04004AC5 RID: 19141
	[SerializeField]
	private KButton submitButton;

	// Token: 0x04004AC6 RID: 19142
	[SerializeField]
	private KButton moreInfoButton;

	// Token: 0x04004AC7 RID: 19143
	[SerializeField]
	private KButton quitButton;

	// Token: 0x04004AC8 RID: 19144
	[SerializeField]
	private KButton continueGameButton;

	// Token: 0x04004AC9 RID: 19145
	[SerializeField]
	private LocText CrashLabel;

	// Token: 0x04004ACA RID: 19146
	[SerializeField]
	private GameObject CrashDescription;

	// Token: 0x04004ACB RID: 19147
	[SerializeField]
	private GameObject ModsInfo;

	// Token: 0x04004ACC RID: 19148
	[SerializeField]
	private GameObject StackTrace;

	// Token: 0x04004ACD RID: 19149
	[SerializeField]
	private GameObject modEntryPrefab;

	// Token: 0x04004ACE RID: 19150
	[SerializeField]
	private Transform modEntryParent;

	// Token: 0x04004ACF RID: 19151
	private ReportErrorDialog.Mode mode;

	// Token: 0x02001FA5 RID: 8101
	private enum Mode
	{
		// Token: 0x040091A7 RID: 37287
		SubmitError,
		// Token: 0x040091A8 RID: 37288
		DisableMods
	}
}
