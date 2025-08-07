using System;
using System.Collections.Generic;
using System.IO;
using Klei;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x02000D93 RID: 3475
public class PasteBaseTemplateScreen : KScreen
{
	// Token: 0x06006C5E RID: 27742 RVA: 0x0028EB2E File Offset: 0x0028CD2E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		PasteBaseTemplateScreen.Instance = this;
		TemplateCache.Init();
		this.button_directory_up.onClick += this.UpDirectory;
		base.ConsumeMouseScroll = true;
		this.RefreshStampButtons();
	}

	// Token: 0x06006C5F RID: 27743 RVA: 0x0028EB65 File Offset: 0x0028CD65
	protected override void OnForcedCleanUp()
	{
		PasteBaseTemplateScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06006C60 RID: 27744 RVA: 0x0028EB74 File Offset: 0x0028CD74
	[ContextMenu("Refresh")]
	public void RefreshStampButtons()
	{
		this.directory_path_text.text = this.m_CurrentDirectory;
		this.button_directory_up.isInteractable = this.m_CurrentDirectory != PasteBaseTemplateScreen.NO_DIRECTORY;
		foreach (GameObject gameObject in this.m_template_buttons)
		{
			global::UnityEngine.Object.Destroy(gameObject);
		}
		this.m_template_buttons.Clear();
		if (this.m_CurrentDirectory == PasteBaseTemplateScreen.NO_DIRECTORY)
		{
			this.directory_path_text.text = "";
			using (List<string>.Enumerator enumerator2 = DlcManager.RELEASED_VERSIONS.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string dlcId = enumerator2.Current;
					if (Game.IsDlcActiveForCurrentSave(dlcId))
					{
						GameObject gameObject2 = global::Util.KInstantiateUI(this.prefab_directory_button, this.button_list_container, true);
						gameObject2.GetComponent<KButton>().onClick += delegate
						{
							this.UpdateDirectory(SettingsCache.GetScope(dlcId));
						};
						gameObject2.GetComponentInChildren<LocText>().text = ((dlcId == "") ? UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.BASE_GAME_FOLDER_NAME.text : SettingsCache.GetScope(dlcId));
						this.m_template_buttons.Add(gameObject2);
					}
				}
			}
			return;
		}
		string text = TemplateCache.RewriteTemplatePath(this.m_CurrentDirectory);
		if (Directory.Exists(text))
		{
			string[] directories = Directory.GetDirectories(text);
			for (int i = 0; i < directories.Length; i++)
			{
				string text2 = directories[i];
				string directory_name = global::System.IO.Path.GetFileNameWithoutExtension(text2);
				GameObject gameObject3 = global::Util.KInstantiateUI(this.prefab_directory_button, this.button_list_container, true);
				gameObject3.GetComponent<KButton>().onClick += delegate
				{
					this.UpdateDirectory(directory_name);
				};
				gameObject3.GetComponentInChildren<LocText>().text = directory_name;
				this.m_template_buttons.Add(gameObject3);
			}
		}
		ListPool<FileHandle, PasteBaseTemplateScreen>.PooledList pooledList = ListPool<FileHandle, PasteBaseTemplateScreen>.Allocate();
		FileSystem.GetFiles(TemplateCache.RewriteTemplatePath(this.m_CurrentDirectory), "*.yaml", pooledList);
		foreach (FileHandle fileHandle in pooledList)
		{
			string file_path_no_extension = global::System.IO.Path.GetFileNameWithoutExtension(fileHandle.full_path);
			GameObject gameObject4 = global::Util.KInstantiateUI(this.prefab_paste_button, this.button_list_container, true);
			gameObject4.GetComponent<KButton>().onClick += delegate
			{
				this.OnClickPasteButton(file_path_no_extension);
			};
			gameObject4.GetComponentInChildren<LocText>().text = file_path_no_extension;
			this.m_template_buttons.Add(gameObject4);
		}
	}

	// Token: 0x06006C61 RID: 27745 RVA: 0x0028EE60 File Offset: 0x0028D060
	private void UpdateDirectory(string relativePath)
	{
		if (this.m_CurrentDirectory == PasteBaseTemplateScreen.NO_DIRECTORY)
		{
			this.m_CurrentDirectory = "";
		}
		this.m_CurrentDirectory = FileSystem.CombineAndNormalize(new string[] { this.m_CurrentDirectory, relativePath });
		this.RefreshStampButtons();
	}

	// Token: 0x06006C62 RID: 27746 RVA: 0x0028EEB0 File Offset: 0x0028D0B0
	private void UpDirectory()
	{
		int num = this.m_CurrentDirectory.LastIndexOf("/");
		if (num > 0)
		{
			this.m_CurrentDirectory = this.m_CurrentDirectory.Substring(0, num);
		}
		else
		{
			string text;
			string text2;
			SettingsCache.GetDlcIdAndPath(this.m_CurrentDirectory, out text, out text2);
			if (text2.IsNullOrWhiteSpace())
			{
				this.m_CurrentDirectory = PasteBaseTemplateScreen.NO_DIRECTORY;
			}
			else
			{
				this.m_CurrentDirectory = SettingsCache.GetScope(text);
			}
		}
		this.RefreshStampButtons();
	}

	// Token: 0x06006C63 RID: 27747 RVA: 0x0028EF20 File Offset: 0x0028D120
	private void OnClickPasteButton(string template_name)
	{
		if (template_name == null)
		{
			return;
		}
		string text = FileSystem.CombineAndNormalize(new string[] { this.m_CurrentDirectory, template_name });
		DebugTool.Instance.DeactivateTool(null);
		DebugBaseTemplateButton.Instance.ClearSelection();
		DebugBaseTemplateButton.Instance.nameField.text = text;
		TemplateContainer template = TemplateCache.GetTemplate(text);
		StampTool.Instance.Activate(template, true, false);
	}

	// Token: 0x040049E4 RID: 18916
	public static PasteBaseTemplateScreen Instance;

	// Token: 0x040049E5 RID: 18917
	public GameObject button_list_container;

	// Token: 0x040049E6 RID: 18918
	public GameObject prefab_paste_button;

	// Token: 0x040049E7 RID: 18919
	public GameObject prefab_directory_button;

	// Token: 0x040049E8 RID: 18920
	public KButton button_directory_up;

	// Token: 0x040049E9 RID: 18921
	public LocText directory_path_text;

	// Token: 0x040049EA RID: 18922
	private List<GameObject> m_template_buttons = new List<GameObject>();

	// Token: 0x040049EB RID: 18923
	private static readonly string NO_DIRECTORY = "NONE";

	// Token: 0x040049EC RID: 18924
	private string m_CurrentDirectory = PasteBaseTemplateScreen.NO_DIRECTORY;
}
