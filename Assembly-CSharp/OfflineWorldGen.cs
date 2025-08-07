using System;
using System.Collections.Generic;
using System.Threading;
using Klei.CustomSettings;
using ProcGenGame;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000E90 RID: 3728
[AddComponentMenu("KMonoBehaviour/scripts/OfflineWorldGen")]
public class OfflineWorldGen : KMonoBehaviour
{
	// Token: 0x060076C3 RID: 30403 RVA: 0x002D79AC File Offset: 0x002D5BAC
	private void TrackProgress(string text)
	{
		if (this.trackProgress)
		{
			global::Debug.Log(text);
		}
	}

	// Token: 0x060076C4 RID: 30404 RVA: 0x002D79BC File Offset: 0x002D5BBC
	public static bool CanLoadSave()
	{
		bool flag = WorldGen.CanLoad(SaveLoader.GetActiveSaveFilePath());
		if (!flag)
		{
			SaveLoader.SetActiveSaveFilePath(null);
			flag = WorldGen.CanLoad(WorldGen.WORLDGEN_SAVE_FILENAME);
		}
		return flag;
	}

	// Token: 0x060076C5 RID: 30405 RVA: 0x002D79EC File Offset: 0x002D5BEC
	public void Generate()
	{
		this.doWorldGen = !OfflineWorldGen.CanLoadSave();
		this.updateText.gameObject.SetActive(false);
		this.percentText.gameObject.SetActive(false);
		this.doWorldGen |= this.debug;
		if (this.doWorldGen)
		{
			this.seedText.text = string.Format(UI.WORLDGEN.USING_PLAYER_SEED, this.seed);
			this.titleText.text = UI.FRONTEND.WORLDGENSCREEN.TITLE.ToString();
			this.mainText.text = UI.WORLDGEN.CHOOSEWORLDSIZE.ToString();
			for (int i = 0; i < this.validDimensions.Length; i++)
			{
				GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.buttonPrefab);
				gameObject.SetActive(true);
				RectTransform component = gameObject.GetComponent<RectTransform>();
				component.SetParent(this.buttonRoot);
				component.localScale = Vector3.one;
				TMP_Text componentInChildren = gameObject.GetComponentInChildren<LocText>();
				OfflineWorldGen.ValidDimensions validDimensions = this.validDimensions[i];
				componentInChildren.text = validDimensions.name.ToString();
				int idx = i;
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.DoWorldGen(idx);
					this.ToggleGenerationUI();
				};
			}
			if (this.validDimensions.Length == 1)
			{
				this.DoWorldGen(0);
				this.ToggleGenerationUI();
			}
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
			this.OnResize();
		}
		else
		{
			this.titleText.text = UI.FRONTEND.WORLDGENSCREEN.LOADINGGAME.ToString();
			this.mainText.gameObject.SetActive(false);
			this.currentConvertedCurrentStage = UI.WORLDGEN.COMPLETE.key;
			this.currentPercent = 1f;
			this.updateText.gameObject.SetActive(false);
			this.percentText.gameObject.SetActive(false);
			this.RemoveButtons();
		}
		this.buttonPrefab.SetActive(false);
	}

	// Token: 0x060076C6 RID: 30406 RVA: 0x002D7BEC File Offset: 0x002D5DEC
	private void OnResize()
	{
		float canvasScale = base.GetComponentInParent<KCanvasScaler>().GetCanvasScale();
		if (this.asteriodAnim != null)
		{
			this.asteriodAnim.animScale = 0.005f * (1f / canvasScale);
		}
	}

	// Token: 0x060076C7 RID: 30407 RVA: 0x002D7C34 File Offset: 0x002D5E34
	private void ToggleGenerationUI()
	{
		this.percentText.gameObject.SetActive(false);
		this.updateText.gameObject.SetActive(true);
		this.titleText.text = UI.FRONTEND.WORLDGENSCREEN.GENERATINGWORLD.ToString();
		if (this.titleText != null && this.titleText.gameObject != null)
		{
			this.titleText.gameObject.SetActive(false);
		}
		if (this.buttonRoot != null && this.buttonRoot.gameObject != null)
		{
			this.buttonRoot.gameObject.SetActive(false);
		}
	}

	// Token: 0x060076C8 RID: 30408 RVA: 0x002D7CDC File Offset: 0x002D5EDC
	private bool UpdateProgress(StringKey stringKeyRoot, float completePercent, WorldGenProgressStages.Stages stage)
	{
		if (this.currentStage != stage)
		{
			this.currentStage = stage;
		}
		if (this.currentStringKeyRoot.Hash != stringKeyRoot.Hash)
		{
			this.currentConvertedCurrentStage = stringKeyRoot;
			this.currentStringKeyRoot = stringKeyRoot;
		}
		else
		{
			int num = (int)completePercent * 10;
			LocString locString = this.convertList.Find((LocString s) => s.key.Hash == stringKeyRoot.Hash);
			if (num != 0 && locString != null)
			{
				this.currentConvertedCurrentStage = new StringKey(locString.key.String + num.ToString());
			}
		}
		float num2 = 0f;
		float num3 = 0f;
		float num4 = WorldGenProgressStages.StageWeights[(int)stage].Value * completePercent;
		for (int i = 0; i < WorldGenProgressStages.StageWeights.Length; i++)
		{
			num3 += WorldGenProgressStages.StageWeights[i].Value;
			if (i < (int)this.currentStage)
			{
				num2 += WorldGenProgressStages.StageWeights[i].Value;
			}
		}
		float num5 = (num2 + num4) / num3;
		this.currentPercent = num5;
		return !this.shouldStop;
	}

	// Token: 0x060076C9 RID: 30409 RVA: 0x002D7E04 File Offset: 0x002D6004
	private void Update()
	{
		if (this.loadTriggered)
		{
			return;
		}
		if (this.currentConvertedCurrentStage.String == null)
		{
			return;
		}
		this.errorMutex.WaitOne();
		int count = this.errors.Count;
		this.errorMutex.ReleaseMutex();
		if (count > 0)
		{
			this.DoExitFlow();
			return;
		}
		this.updateText.text = Strings.Get(this.currentConvertedCurrentStage.String);
		if (!this.debug && this.currentConvertedCurrentStage.Hash == UI.WORLDGEN.COMPLETE.key.Hash && this.currentPercent >= 1f && this.cluster.IsGenerationComplete)
		{
			if (KCrashReporter.terminateOnError && KCrashReporter.hasCrash)
			{
				return;
			}
			this.percentText.text = "";
			this.loadTriggered = true;
			App.LoadScene(this.mainGameLevel);
			return;
		}
		else
		{
			if (this.currentPercent < 0f)
			{
				this.DoExitFlow();
				return;
			}
			if (this.currentPercent > 0f && !this.percentText.gameObject.activeSelf)
			{
				this.percentText.gameObject.SetActive(false);
			}
			this.percentText.text = GameUtil.GetFormattedPercent(this.currentPercent * 100f, GameUtil.TimeSlice.None);
			this.meterAnim.SetPositionPercent(this.currentPercent);
			return;
		}
	}

	// Token: 0x060076CA RID: 30410 RVA: 0x002D7F58 File Offset: 0x002D6158
	private void DisplayErrors()
	{
		this.errorMutex.WaitOne();
		if (this.errors.Count > 0)
		{
			foreach (OfflineWorldGen.ErrorInfo errorInfo in this.errors)
			{
				Util.KInstantiateUI<ConfirmDialogScreen>(ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject, FrontEndManager.Instance.gameObject, true).PopupConfirmDialog(errorInfo.errorDesc, new global::System.Action(this.OnConfirmExit), null, null, null, null, null, null, null);
			}
		}
		this.errorMutex.ReleaseMutex();
	}

	// Token: 0x060076CB RID: 30411 RVA: 0x002D8008 File Offset: 0x002D6208
	private void DoExitFlow()
	{
		if (this.startedExitFlow)
		{
			return;
		}
		this.startedExitFlow = true;
		this.percentText.text = UI.WORLDGEN.RESTARTING.ToString();
		this.loadTriggered = true;
		Sim.Shutdown();
		this.DisplayErrors();
	}

	// Token: 0x060076CC RID: 30412 RVA: 0x002D8041 File Offset: 0x002D6241
	private void OnConfirmExit()
	{
		App.LoadScene(this.frontendGameLevel);
	}

	// Token: 0x060076CD RID: 30413 RVA: 0x002D8050 File Offset: 0x002D6250
	private void RemoveButtons()
	{
		for (int i = this.buttonRoot.childCount - 1; i >= 0; i--)
		{
			global::UnityEngine.Object.Destroy(this.buttonRoot.GetChild(i).gameObject);
		}
	}

	// Token: 0x060076CE RID: 30414 RVA: 0x002D808B File Offset: 0x002D628B
	private void DoWorldGen(int selectedDimension)
	{
		this.RemoveButtons();
		this.DoWorldGenInitialize();
	}

	// Token: 0x060076CF RID: 30415 RVA: 0x002D809C File Offset: 0x002D629C
	private void DoWorldGenInitialize()
	{
		string text = "";
		Func<int, WorldGen, bool> func = null;
		this.seed = CustomGameSettings.Instance.GetCurrentWorldgenSeed();
		text = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout).id;
		List<string> list = new List<string>();
		foreach (string text2 in CustomGameSettings.Instance.GetCurrentStories())
		{
			list.Add(Db.Get().Stories.Get(text2).worldgenStoryTraitKey);
		}
		this.cluster = new Cluster(text, this.seed, list, true, false, false);
		this.cluster.ShouldSkipWorldCallback = func;
		this.cluster.Generate(new WorldGen.OfflineCallbackFunction(this.UpdateProgress), new Action<OfflineWorldGen.ErrorInfo>(this.OnError), this.seed, this.seed, this.seed, this.seed, true, false, false);
	}

	// Token: 0x060076D0 RID: 30416 RVA: 0x002D819C File Offset: 0x002D639C
	private void OnError(OfflineWorldGen.ErrorInfo error)
	{
		this.errorMutex.WaitOne();
		this.errors.Add(error);
		this.errorMutex.ReleaseMutex();
	}

	// Token: 0x040052A2 RID: 21154
	[SerializeField]
	private RectTransform buttonRoot;

	// Token: 0x040052A3 RID: 21155
	[SerializeField]
	private GameObject buttonPrefab;

	// Token: 0x040052A4 RID: 21156
	[SerializeField]
	private RectTransform chooseLocationPanel;

	// Token: 0x040052A5 RID: 21157
	[SerializeField]
	private GameObject locationButtonPrefab;

	// Token: 0x040052A6 RID: 21158
	private const float baseScale = 0.005f;

	// Token: 0x040052A7 RID: 21159
	private Mutex errorMutex = new Mutex();

	// Token: 0x040052A8 RID: 21160
	private List<OfflineWorldGen.ErrorInfo> errors = new List<OfflineWorldGen.ErrorInfo>();

	// Token: 0x040052A9 RID: 21161
	private OfflineWorldGen.ValidDimensions[] validDimensions = new OfflineWorldGen.ValidDimensions[]
	{
		new OfflineWorldGen.ValidDimensions
		{
			width = 256,
			height = 384,
			name = UI.FRONTEND.WORLDGENSCREEN.SIZES.STANDARD.key
		}
	};

	// Token: 0x040052AA RID: 21162
	public string frontendGameLevel = "frontend";

	// Token: 0x040052AB RID: 21163
	public string mainGameLevel = "backend";

	// Token: 0x040052AC RID: 21164
	private bool shouldStop;

	// Token: 0x040052AD RID: 21165
	private StringKey currentConvertedCurrentStage;

	// Token: 0x040052AE RID: 21166
	private float currentPercent;

	// Token: 0x040052AF RID: 21167
	public bool debug;

	// Token: 0x040052B0 RID: 21168
	private bool trackProgress = true;

	// Token: 0x040052B1 RID: 21169
	private bool doWorldGen;

	// Token: 0x040052B2 RID: 21170
	[SerializeField]
	private LocText titleText;

	// Token: 0x040052B3 RID: 21171
	[SerializeField]
	private LocText mainText;

	// Token: 0x040052B4 RID: 21172
	[SerializeField]
	private LocText updateText;

	// Token: 0x040052B5 RID: 21173
	[SerializeField]
	private LocText percentText;

	// Token: 0x040052B6 RID: 21174
	[SerializeField]
	private LocText seedText;

	// Token: 0x040052B7 RID: 21175
	[SerializeField]
	private KBatchedAnimController meterAnim;

	// Token: 0x040052B8 RID: 21176
	[SerializeField]
	private KBatchedAnimController asteriodAnim;

	// Token: 0x040052B9 RID: 21177
	private Cluster cluster;

	// Token: 0x040052BA RID: 21178
	private StringKey currentStringKeyRoot;

	// Token: 0x040052BB RID: 21179
	private List<LocString> convertList = new List<LocString>
	{
		UI.WORLDGEN.SETTLESIM,
		UI.WORLDGEN.BORDERS,
		UI.WORLDGEN.PROCESSING,
		UI.WORLDGEN.COMPLETELAYOUT,
		UI.WORLDGEN.WORLDLAYOUT,
		UI.WORLDGEN.GENERATENOISE,
		UI.WORLDGEN.BUILDNOISESOURCE,
		UI.WORLDGEN.GENERATESOLARSYSTEM
	};

	// Token: 0x040052BC RID: 21180
	private WorldGenProgressStages.Stages currentStage;

	// Token: 0x040052BD RID: 21181
	private bool loadTriggered;

	// Token: 0x040052BE RID: 21182
	private bool startedExitFlow;

	// Token: 0x040052BF RID: 21183
	private int seed;

	// Token: 0x02002082 RID: 8322
	public struct ErrorInfo
	{
		// Token: 0x0400947B RID: 38011
		public string errorDesc;

		// Token: 0x0400947C RID: 38012
		public Exception exception;
	}

	// Token: 0x02002083 RID: 8323
	[Serializable]
	private struct ValidDimensions
	{
		// Token: 0x0400947D RID: 38013
		public int width;

		// Token: 0x0400947E RID: 38014
		public int height;

		// Token: 0x0400947F RID: 38015
		public StringKey name;
	}
}
