using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C21 RID: 3105
public class InspectSaveScreen : KModalScreen
{
	// Token: 0x06005E03 RID: 24067 RVA: 0x00225D61 File Offset: 0x00223F61
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.closeButton.onClick += this.CloseScreen;
		this.deleteSaveBtn.onClick += this.DeleteSave;
	}

	// Token: 0x06005E04 RID: 24068 RVA: 0x00225D97 File Offset: 0x00223F97
	private void CloseScreen()
	{
		LoadScreen.Instance.Show(true);
		this.Show(false);
	}

	// Token: 0x06005E05 RID: 24069 RVA: 0x00225DAB File Offset: 0x00223FAB
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			this.buttonPool.ClearAll();
			this.buttonFileMap.Clear();
		}
	}

	// Token: 0x06005E06 RID: 24070 RVA: 0x00225DD0 File Offset: 0x00223FD0
	public void SetTarget(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			global::Debug.LogError("The directory path provided is empty.");
			this.Show(false);
			return;
		}
		if (!Directory.Exists(path))
		{
			global::Debug.LogError("The directory provided does not exist.");
			this.Show(false);
			return;
		}
		if (this.buttonPool == null)
		{
			this.buttonPool = new UIPool<KButton>(this.backupBtnPrefab);
		}
		this.currentPath = path;
		List<string> list = (from filename in Directory.GetFiles(path)
			where Path.GetExtension(filename).ToLower() == ".sav"
			orderby File.GetLastWriteTime(filename) descending
			select filename).ToList<string>();
		string text = list[0];
		if (File.Exists(text))
		{
			this.mainSaveBtn.gameObject.SetActive(true);
			this.AddNewSave(this.mainSaveBtn, text);
		}
		else
		{
			this.mainSaveBtn.gameObject.SetActive(false);
		}
		if (list.Count > 1)
		{
			for (int i = 1; i < list.Count; i++)
			{
				this.AddNewSave(this.buttonPool.GetFreeElement(this.buttonGroup, true), list[i]);
			}
		}
		this.Show(true);
	}

	// Token: 0x06005E07 RID: 24071 RVA: 0x00225F08 File Offset: 0x00224108
	private void ConfirmDoAction(string message, global::System.Action action)
	{
		if (this.confirmScreen == null)
		{
			this.confirmScreen = Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, base.gameObject, false);
			this.confirmScreen.PopupConfirmDialog(message, action, delegate
			{
			}, null, null, null, null, null, null);
			this.confirmScreen.GetComponent<LayoutElement>().ignoreLayout = true;
			this.confirmScreen.gameObject.SetActive(true);
		}
	}

	// Token: 0x06005E08 RID: 24072 RVA: 0x00225F98 File Offset: 0x00224198
	private void DeleteSave()
	{
		if (string.IsNullOrEmpty(this.currentPath))
		{
			global::Debug.LogError("The path provided is not valid and cannot be deleted.");
			return;
		}
		this.ConfirmDoAction(UI.FRONTEND.LOADSCREEN.CONFIRMDELETE, delegate
		{
			string[] files = Directory.GetFiles(this.currentPath);
			for (int i = 0; i < files.Length; i++)
			{
				File.Delete(files[i]);
			}
			Directory.Delete(this.currentPath);
			this.CloseScreen();
		});
	}

	// Token: 0x06005E09 RID: 24073 RVA: 0x00225FCE File Offset: 0x002241CE
	private void AddNewSave(KButton btn, string file)
	{
	}

	// Token: 0x06005E0A RID: 24074 RVA: 0x00225FD0 File Offset: 0x002241D0
	private void ButtonClicked(KButton btn)
	{
		LoadingOverlay.Load(delegate
		{
			this.Load(this.buttonFileMap[btn]);
		});
	}

	// Token: 0x06005E0B RID: 24075 RVA: 0x00225FF5 File Offset: 0x002241F5
	private void Load(string filename)
	{
		if (Game.Instance != null)
		{
			LoadScreen.ForceStopGame();
		}
		SaveLoader.SetActiveSaveFilePath(filename);
		App.LoadScene("backend");
		this.Deactivate();
	}

	// Token: 0x06005E0C RID: 24076 RVA: 0x0022601F File Offset: 0x0022421F
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.CloseScreen();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x04003E9B RID: 16027
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003E9C RID: 16028
	[SerializeField]
	private KButton mainSaveBtn;

	// Token: 0x04003E9D RID: 16029
	[SerializeField]
	private KButton backupBtnPrefab;

	// Token: 0x04003E9E RID: 16030
	[SerializeField]
	private KButton deleteSaveBtn;

	// Token: 0x04003E9F RID: 16031
	[SerializeField]
	private GameObject buttonGroup;

	// Token: 0x04003EA0 RID: 16032
	private UIPool<KButton> buttonPool;

	// Token: 0x04003EA1 RID: 16033
	private Dictionary<KButton, string> buttonFileMap = new Dictionary<KButton, string>();

	// Token: 0x04003EA2 RID: 16034
	private ConfirmDialogScreen confirmScreen;

	// Token: 0x04003EA3 RID: 16035
	private string currentPath = "";
}
