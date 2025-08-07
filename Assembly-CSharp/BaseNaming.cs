using System;
using System.IO;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000C09 RID: 3081
[AddComponentMenu("KMonoBehaviour/scripts/BaseNaming")]
public class BaseNaming : KMonoBehaviour
{
	// Token: 0x06005CF0 RID: 23792 RVA: 0x0021E0A0 File Offset: 0x0021C2A0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.GenerateBaseName();
		this.shuffleBaseNameButton.onClick += this.GenerateBaseName;
		this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.OnEditing));
		this.minionSelectScreen = base.GetComponent<MinionSelectScreen>();
	}

	// Token: 0x06005CF1 RID: 23793 RVA: 0x0021E114 File Offset: 0x0021C314
	private bool CheckBaseName(string newName)
	{
		if (string.IsNullOrEmpty(newName))
		{
			return true;
		}
		string savePrefixAndCreateFolder = SaveLoader.GetSavePrefixAndCreateFolder();
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		if (this.minionSelectScreen != null)
		{
			bool flag = false;
			try
			{
				bool flag2 = Directory.Exists(Path.Combine(savePrefixAndCreateFolder, newName));
				bool flag3 = cloudSavePrefix != null && Directory.Exists(Path.Combine(cloudSavePrefix, newName));
				flag = flag2 || flag3;
			}
			catch (Exception ex)
			{
				flag = true;
				global::Debug.Log(string.Format("Base Naming / Warning / {0}", ex));
			}
			if (flag)
			{
				this.minionSelectScreen.SetProceedButtonActive(false, string.Format(UI.IMMIGRANTSCREEN.DUPLICATE_COLONY_NAME, newName));
				return false;
			}
			this.minionSelectScreen.SetProceedButtonActive(true, null);
		}
		return true;
	}

	// Token: 0x06005CF2 RID: 23794 RVA: 0x0021E1C4 File Offset: 0x0021C3C4
	private void OnEditing(string newName)
	{
		Util.ScrubInputField(this.inputField, false, false);
		this.CheckBaseName(this.inputField.text);
	}

	// Token: 0x06005CF3 RID: 23795 RVA: 0x0021E1E8 File Offset: 0x0021C3E8
	private void OnEndEdit(string newName)
	{
		if (Localization.HasDirtyWords(newName))
		{
			this.inputField.text = this.GenerateBaseNameString();
			newName = this.inputField.text;
		}
		if (string.IsNullOrEmpty(newName))
		{
			return;
		}
		if (newName.EndsWith(" "))
		{
			newName = newName.TrimEnd(' ');
		}
		if (!this.CheckBaseName(newName))
		{
			return;
		}
		this.inputField.text = newName;
		SaveGame.Instance.SetBaseName(newName);
		string text = Path.ChangeExtension(newName, ".sav");
		string savePrefixAndCreateFolder = SaveLoader.GetSavePrefixAndCreateFolder();
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		string text2 = savePrefixAndCreateFolder;
		if (SaveLoader.GetCloudSavesAvailable() && Game.Instance.SaveToCloudActive && cloudSavePrefix != null)
		{
			text2 = cloudSavePrefix;
		}
		SaveLoader.SetActiveSaveFilePath(Path.Combine(text2, newName, text));
	}

	// Token: 0x06005CF4 RID: 23796 RVA: 0x0021E29C File Offset: 0x0021C49C
	private void GenerateBaseName()
	{
		string text = this.GenerateBaseNameString();
		((LocText)this.inputField.placeholder).text = text;
		this.inputField.text = text;
		this.OnEndEdit(text);
	}

	// Token: 0x06005CF5 RID: 23797 RVA: 0x0021E2DC File Offset: 0x0021C4DC
	private string GenerateBaseNameString()
	{
		string text = LocString.GetStrings(typeof(NAMEGEN.COLONY.FORMATS)).GetRandom<string>();
		text = this.ReplaceStringWithRandom(text, "{noun}", LocString.GetStrings(typeof(NAMEGEN.COLONY.NOUN)));
		string[] strings = LocString.GetStrings(typeof(NAMEGEN.COLONY.ADJECTIVE));
		text = this.ReplaceStringWithRandom(text, "{adjective}", strings);
		text = this.ReplaceStringWithRandom(text, "{adjective2}", strings);
		text = this.ReplaceStringWithRandom(text, "{adjective3}", strings);
		return this.ReplaceStringWithRandom(text, "{adjective4}", strings);
	}

	// Token: 0x06005CF6 RID: 23798 RVA: 0x0021E363 File Offset: 0x0021C563
	private string ReplaceStringWithRandom(string fullString, string replacementKey, string[] replacementValues)
	{
		if (!fullString.Contains(replacementKey))
		{
			return fullString;
		}
		return fullString.Replace(replacementKey, replacementValues.GetRandom<string>());
	}

	// Token: 0x04003DCE RID: 15822
	[SerializeField]
	private KInputTextField inputField;

	// Token: 0x04003DCF RID: 15823
	[SerializeField]
	private KButton shuffleBaseNameButton;

	// Token: 0x04003DD0 RID: 15824
	private MinionSelectScreen minionSelectScreen;
}
