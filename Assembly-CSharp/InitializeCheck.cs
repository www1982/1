using System;
using System.IO;
using ProcGenGame;
using STRINGS;
using UnityEngine;

// Token: 0x02000962 RID: 2402
public class InitializeCheck : MonoBehaviour
{
	// Token: 0x170004EF RID: 1263
	// (get) Token: 0x060044F8 RID: 17656 RVA: 0x0018D4A0 File Offset: 0x0018B6A0
	// (set) Token: 0x060044F9 RID: 17657 RVA: 0x0018D4A7 File Offset: 0x0018B6A7
	public static InitializeCheck.SavePathIssue savePathState { get; private set; }

	// Token: 0x060044FA RID: 17658 RVA: 0x0018D4B0 File Offset: 0x0018B6B0
	private void Awake()
	{
		this.CheckForSavePathIssue();
		if (InitializeCheck.savePathState == InitializeCheck.SavePathIssue.Ok && !KCrashReporter.hasCrash)
		{
			AudioMixer.Create();
			App.LoadScene("frontend");
			return;
		}
		Canvas canvas = base.gameObject.AddComponent<Canvas>();
		canvas.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500f);
		canvas.rectTransform().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 500f);
		Camera camera = base.gameObject.AddComponent<Camera>();
		camera.orthographic = true;
		camera.orthographicSize = 200f;
		camera.backgroundColor = Color.black;
		camera.clearFlags = CameraClearFlags.Color;
		camera.nearClipPlane = 0f;
		global::Debug.Log("Cannot initialize filesystem. [" + InitializeCheck.savePathState.ToString() + "]");
		Localization.Initialize();
		GameObject.Find("BootCanvas").SetActive(false);
		this.ShowFileErrorDialogs();
	}

	// Token: 0x060044FB RID: 17659 RVA: 0x0018D589 File Offset: 0x0018B789
	private GameObject CreateUIRoot()
	{
		return Util.KInstantiate(this.rootCanvasPrefab, null, "CanvasRoot");
	}

	// Token: 0x060044FC RID: 17660 RVA: 0x0018D59C File Offset: 0x0018B79C
	private void ShowErrorDialog(string msg)
	{
		GameObject gameObject = this.CreateUIRoot();
		Util.KInstantiateUI<ConfirmDialogScreen>(this.confirmDialogScreen.gameObject, gameObject, true).PopupConfirmDialog(msg, new global::System.Action(this.Quit), null, null, null, null, null, null, this.sadDupe);
	}

	// Token: 0x060044FD RID: 17661 RVA: 0x0018D5E0 File Offset: 0x0018B7E0
	private void ShowFileErrorDialogs()
	{
		string text = null;
		switch (InitializeCheck.savePathState)
		{
		case InitializeCheck.SavePathIssue.WriteTestFail:
			text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_READ_ONLY, SaveLoader.GetSavePrefix());
			break;
		case InitializeCheck.SavePathIssue.SpaceTestFail:
			text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.SAVE_DIRECTORY_INSUFFICIENT_SPACE, SaveLoader.GetSavePrefix());
			break;
		case InitializeCheck.SavePathIssue.WorldGenFilesFail:
			text = string.Format(UI.FRONTEND.SUPPORTWARNINGS.WORLD_GEN_FILES, WorldGen.WORLDGEN_SAVE_FILENAME);
			break;
		}
		if (text != null)
		{
			this.ShowErrorDialog(text);
		}
	}

	// Token: 0x060044FE RID: 17662 RVA: 0x0018D658 File Offset: 0x0018B858
	private void CheckForSavePathIssue()
	{
		if (this.test_issue != InitializeCheck.SavePathIssue.Ok)
		{
			InitializeCheck.savePathState = this.test_issue;
			return;
		}
		string savePrefix = SaveLoader.GetSavePrefix();
		InitializeCheck.savePathState = InitializeCheck.SavePathIssue.Ok;
		try
		{
			SaveLoader.GetSavePrefixAndCreateFolder();
			using (FileStream fileStream = File.Open(savePrefix + InitializeCheck.testFile, FileMode.Create, FileAccess.Write))
			{
				new BinaryWriter(fileStream);
				fileStream.Close();
			}
		}
		catch
		{
			InitializeCheck.savePathState = InitializeCheck.SavePathIssue.WriteTestFail;
			goto IL_00C8;
		}
		using (FileStream fileStream2 = File.Open(savePrefix + InitializeCheck.testSave, FileMode.Create, FileAccess.Write))
		{
			try
			{
				fileStream2.SetLength(15000000L);
				new BinaryWriter(fileStream2);
				fileStream2.Close();
			}
			catch
			{
				fileStream2.Close();
				InitializeCheck.savePathState = InitializeCheck.SavePathIssue.SpaceTestFail;
				goto IL_00C8;
			}
		}
		try
		{
			using (File.Open(WorldGen.WORLDGEN_SAVE_FILENAME, FileMode.Append))
			{
			}
		}
		catch
		{
			InitializeCheck.savePathState = InitializeCheck.SavePathIssue.WorldGenFilesFail;
		}
		IL_00C8:
		try
		{
			if (File.Exists(savePrefix + InitializeCheck.testFile))
			{
				File.Delete(savePrefix + InitializeCheck.testFile);
			}
			if (File.Exists(savePrefix + InitializeCheck.testSave))
			{
				File.Delete(savePrefix + InitializeCheck.testSave);
			}
		}
		catch
		{
		}
	}

	// Token: 0x060044FF RID: 17663 RVA: 0x0018D7D0 File Offset: 0x0018B9D0
	private void Quit()
	{
		global::Debug.Log("Quitting...");
		App.Quit();
	}

	// Token: 0x04002E29 RID: 11817
	private static readonly string testFile = "testfile";

	// Token: 0x04002E2A RID: 11818
	private static readonly string testSave = "testsavefile";

	// Token: 0x04002E2B RID: 11819
	public Canvas rootCanvasPrefab;

	// Token: 0x04002E2C RID: 11820
	public ConfirmDialogScreen confirmDialogScreen;

	// Token: 0x04002E2D RID: 11821
	public Sprite sadDupe;

	// Token: 0x04002E2E RID: 11822
	private InitializeCheck.SavePathIssue test_issue;

	// Token: 0x02001974 RID: 6516
	public enum SavePathIssue
	{
		// Token: 0x04007C88 RID: 31880
		Ok,
		// Token: 0x04007C89 RID: 31881
		WriteTestFail,
		// Token: 0x04007C8A RID: 31882
		SpaceTestFail,
		// Token: 0x04007C8B RID: 31883
		WorldGenFilesFail
	}
}
