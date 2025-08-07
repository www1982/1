using System;
using System.Collections.Generic;
using Klei;
using UnityEngine;

// Token: 0x02000C13 RID: 3091
public class CreditsScreen : KModalScreen
{
	// Token: 0x06005DA7 RID: 23975 RVA: 0x00223FEC File Offset: 0x002221EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (TextAsset textAsset in this.creditsFiles)
		{
			this.AddCredits(textAsset);
		}
		this.CloseButton.onClick += this.Close;
	}

	// Token: 0x06005DA8 RID: 23976 RVA: 0x00224036 File Offset: 0x00222236
	public void Close()
	{
		this.Deactivate();
	}

	// Token: 0x06005DA9 RID: 23977 RVA: 0x00224040 File Offset: 0x00222240
	private void AddCredits(TextAsset csv)
	{
		string[,] array = CSVReader.SplitCsvGrid(csv.text, csv.name);
		List<string> list = new List<string>();
		for (int i = 1; i < array.GetLength(1); i++)
		{
			string text = string.Format("{0} {1}", array[0, i], array[1, i]);
			if (!(text == " "))
			{
				list.Add(text);
			}
		}
		list.Shuffle<string>();
		string text2 = array[0, 0];
		GameObject gameObject = Util.KInstantiateUI(this.teamHeaderPrefab, this.entryContainer.gameObject, true);
		gameObject.GetComponent<LocText>().text = text2;
		this.teamContainers.Add(text2, gameObject);
		foreach (string text3 in list)
		{
			Util.KInstantiateUI(this.entryPrefab, this.teamContainers[text2], true).GetComponent<LocText>().text = text3;
		}
	}

	// Token: 0x04003E57 RID: 15959
	public GameObject entryPrefab;

	// Token: 0x04003E58 RID: 15960
	public GameObject teamHeaderPrefab;

	// Token: 0x04003E59 RID: 15961
	private Dictionary<string, GameObject> teamContainers = new Dictionary<string, GameObject>();

	// Token: 0x04003E5A RID: 15962
	public Transform entryContainer;

	// Token: 0x04003E5B RID: 15963
	public KButton CloseButton;

	// Token: 0x04003E5C RID: 15964
	public TextAsset[] creditsFiles;
}
