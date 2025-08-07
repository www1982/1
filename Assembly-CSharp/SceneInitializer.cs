using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000AEF RID: 2799
public class SceneInitializer : MonoBehaviour
{
	// Token: 0x170005C8 RID: 1480
	// (get) Token: 0x0600521E RID: 21022 RVA: 0x001DEC58 File Offset: 0x001DCE58
	// (set) Token: 0x0600521F RID: 21023 RVA: 0x001DEC5F File Offset: 0x001DCE5F
	public static SceneInitializer Instance { get; private set; }

	// Token: 0x06005220 RID: 21024 RVA: 0x001DEC68 File Offset: 0x001DCE68
	private void Awake()
	{
		Localization.SwapToLocalizedFont();
		string environmentVariable = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
		string text = Application.dataPath + Path.DirectorySeparatorChar.ToString() + "Plugins";
		if (!environmentVariable.Contains(text))
		{
			Environment.SetEnvironmentVariable("PATH", environmentVariable + Path.PathSeparator.ToString() + text, EnvironmentVariableTarget.Process);
		}
		SceneInitializer.Instance = this;
		this.PreLoadPrefabs();
	}

	// Token: 0x06005221 RID: 21025 RVA: 0x001DECD7 File Offset: 0x001DCED7
	private void OnDestroy()
	{
		SceneInitializer.Instance = null;
	}

	// Token: 0x06005222 RID: 21026 RVA: 0x001DECE0 File Offset: 0x001DCEE0
	private void PreLoadPrefabs()
	{
		foreach (GameObject gameObject in this.preloadPrefabs)
		{
			if (gameObject != null)
			{
				Util.KInstantiate(gameObject, gameObject.transform.GetPosition(), Quaternion.identity, base.gameObject, null, true, 0);
			}
		}
	}

	// Token: 0x06005223 RID: 21027 RVA: 0x001DED58 File Offset: 0x001DCF58
	public void NewSaveGamePrefab()
	{
		if (this.prefab_NewSaveGame != null && SaveGame.Instance == null)
		{
			Util.KInstantiate(this.prefab_NewSaveGame, base.gameObject, null);
		}
	}

	// Token: 0x06005224 RID: 21028 RVA: 0x001DED88 File Offset: 0x001DCF88
	public void PostLoadPrefabs()
	{
		foreach (GameObject gameObject in this.prefabs)
		{
			if (gameObject != null)
			{
				Util.KInstantiate(gameObject, base.gameObject, null);
			}
		}
	}

	// Token: 0x04003737 RID: 14135
	public const int MAXDEPTH = -30000;

	// Token: 0x04003738 RID: 14136
	public const int SCREENDEPTH = -1000;

	// Token: 0x0400373A RID: 14138
	public GameObject prefab_NewSaveGame;

	// Token: 0x0400373B RID: 14139
	public List<GameObject> preloadPrefabs = new List<GameObject>();

	// Token: 0x0400373C RID: 14140
	public List<GameObject> prefabs = new List<GameObject>();
}
