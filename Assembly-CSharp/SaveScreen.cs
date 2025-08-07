using System;
using System.IO;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000C37 RID: 3127
public class SaveScreen : KModalScreen
{
	// Token: 0x06005F0B RID: 24331 RVA: 0x0022D278 File Offset: 0x0022B478
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.oldSaveButtonPrefab.gameObject.SetActive(false);
		this.newSaveButton.onClick += this.OnClickNewSave;
		this.closeButton.onClick += this.Deactivate;
	}

	// Token: 0x06005F0C RID: 24332 RVA: 0x0022D2CC File Offset: 0x0022B4CC
	protected override void OnCmpEnable()
	{
		foreach (SaveLoader.SaveFileEntry saveFileEntry in SaveLoader.GetAllColonyFiles(true, SearchOption.TopDirectoryOnly))
		{
			this.AddExistingSaveFile(saveFileEntry.path);
		}
		SpeedControlScreen.Instance.Pause(true, false);
	}

	// Token: 0x06005F0D RID: 24333 RVA: 0x0022D334 File Offset: 0x0022B534
	protected override void OnDeactivate()
	{
		SpeedControlScreen.Instance.Unpause(true);
		base.OnDeactivate();
	}

	// Token: 0x06005F0E RID: 24334 RVA: 0x0022D348 File Offset: 0x0022B548
	private void AddExistingSaveFile(string filename)
	{
		KButton kbutton = Util.KInstantiateUI<KButton>(this.oldSaveButtonPrefab.gameObject, this.oldSavesRoot.gameObject, true);
		HierarchyReferences component = kbutton.GetComponent<HierarchyReferences>();
		LocText component2 = component.GetReference<RectTransform>("Title").GetComponent<LocText>();
		TMP_Text component3 = component.GetReference<RectTransform>("Date").GetComponent<LocText>();
		global::System.DateTime lastWriteTime = File.GetLastWriteTime(filename);
		component2.text = string.Format("{0}", Path.GetFileNameWithoutExtension(filename));
		component3.text = string.Format("{0:H:mm:ss}" + Localization.GetFileDateFormat(0), lastWriteTime);
		kbutton.onClick += delegate
		{
			this.Save(filename);
		};
	}

	// Token: 0x06005F0F RID: 24335 RVA: 0x0022D404 File Offset: 0x0022B604
	public static string GetValidSaveFilename(string filename)
	{
		string text = ".sav";
		if (Path.GetExtension(filename).ToLower() != text)
		{
			filename += text;
		}
		return filename;
	}

	// Token: 0x06005F10 RID: 24336 RVA: 0x0022D434 File Offset: 0x0022B634
	public void Save(string filename)
	{
		filename = SaveScreen.GetValidSaveFilename(filename);
		if (File.Exists(filename))
		{
			ScreenPrefabs.Instance.ConfirmDoAction(string.Format(UI.FRONTEND.SAVESCREEN.OVERWRITEMESSAGE, Path.GetFileNameWithoutExtension(filename)), delegate
			{
				this.DoSave(filename);
			}, base.transform.parent);
			return;
		}
		this.DoSave(filename);
	}

	// Token: 0x06005F11 RID: 24337 RVA: 0x0022D4BC File Offset: 0x0022B6BC
	private void DoSave(string filename)
	{
		try
		{
			SaveLoader.Instance.Save(filename, false, true);
			PauseScreen.Instance.OnSaveComplete();
			this.Deactivate();
		}
		catch (IOException ex)
		{
			IOException ex2 = ex;
			IOException e = ex2;
			Util.KInstantiateUI(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.transform.parent.gameObject, true).GetComponent<ConfirmDialogScreen>().PopupConfirmDialog(string.Format(UI.FRONTEND.SAVESCREEN.IO_ERROR, e.ToString()), delegate
			{
				this.Deactivate();
			}, null, UI.FRONTEND.SAVESCREEN.REPORT_BUG, delegate
			{
				KCrashReporter.ReportError(e.Message, e.StackTrace.ToString(), null, null, null, true, new string[] { KCrashReporter.CRASH_CATEGORY.FILEIO }, null);
			}, null, null, null, null);
		}
	}

	// Token: 0x06005F12 RID: 24338 RVA: 0x0022D57C File Offset: 0x0022B77C
	public void OnClickNewSave()
	{
		FileNameDialog fileNameDialog = (FileNameDialog)KScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.FileNameDialog.gameObject, base.transform.parent.gameObject);
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		if (activeSaveFilePath != null)
		{
			string text = SaveLoader.GetOriginalSaveFileName(activeSaveFilePath);
			text = Path.GetFileNameWithoutExtension(text);
			fileNameDialog.SetTextAndSelect(text);
		}
		fileNameDialog.onConfirm = delegate(string filename)
		{
			filename = Path.Combine(SaveLoader.GetActiveSaveColonyFolder(), filename);
			this.Save(filename);
		};
	}

	// Token: 0x06005F13 RID: 24339 RVA: 0x0022D5E8 File Offset: 0x0022B7E8
	public override void OnKeyUp(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
		}
		e.Consumed = true;
	}

	// Token: 0x06005F14 RID: 24340 RVA: 0x0022D600 File Offset: 0x0022B800
	public override void OnKeyDown(KButtonEvent e)
	{
		e.Consumed = true;
	}

	// Token: 0x04003F5A RID: 16218
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003F5B RID: 16219
	[SerializeField]
	private KButton newSaveButton;

	// Token: 0x04003F5C RID: 16220
	[SerializeField]
	private KButton oldSaveButtonPrefab;

	// Token: 0x04003F5D RID: 16221
	[SerializeField]
	private Transform oldSavesRoot;
}
