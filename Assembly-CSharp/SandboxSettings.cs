using System;
using System.Collections.Generic;

// Token: 0x02000DB5 RID: 3509
public class SandboxSettings
{
	// Token: 0x06006E73 RID: 28275 RVA: 0x002A0309 File Offset: 0x0029E509
	public void AddIntSetting(string prefsKey, Action<int> setAction, int defaultValue)
	{
		this.intSettings.Add(new SandboxSettings.Setting<int>(prefsKey, setAction, defaultValue));
	}

	// Token: 0x06006E74 RID: 28276 RVA: 0x002A031E File Offset: 0x0029E51E
	public int GetIntSetting(string prefsKey)
	{
		return KPlayerPrefs.GetInt(prefsKey);
	}

	// Token: 0x06006E75 RID: 28277 RVA: 0x002A0328 File Offset: 0x0029E528
	public void SetIntSetting(string prefsKey, int value)
	{
		SandboxSettings.Setting<int> setting = this.intSettings.Find((SandboxSettings.Setting<int> match) => match.PrefsKey == prefsKey);
		if (setting == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"No intSetting named: ",
				prefsKey,
				" could be found amongst ",
				this.intSettings.Count.ToString(),
				" int settings."
			}));
		}
		setting.Value = value;
	}

	// Token: 0x06006E76 RID: 28278 RVA: 0x002A03AB File Offset: 0x0029E5AB
	public void RestoreIntSetting(string prefsKey)
	{
		if (KPlayerPrefs.HasKey(prefsKey))
		{
			this.SetIntSetting(prefsKey, this.GetIntSetting(prefsKey));
			return;
		}
		this.ForceDefaultIntSetting(prefsKey);
	}

	// Token: 0x06006E77 RID: 28279 RVA: 0x002A03CC File Offset: 0x0029E5CC
	public void ForceDefaultIntSetting(string prefsKey)
	{
		this.SetIntSetting(prefsKey, this.intSettings.Find((SandboxSettings.Setting<int> match) => match.PrefsKey == prefsKey).defaultValue);
	}

	// Token: 0x06006E78 RID: 28280 RVA: 0x002A040E File Offset: 0x0029E60E
	public void AddFloatSetting(string prefsKey, Action<float> setAction, float defaultValue)
	{
		this.floatSettings.Add(new SandboxSettings.Setting<float>(prefsKey, setAction, defaultValue));
	}

	// Token: 0x06006E79 RID: 28281 RVA: 0x002A0423 File Offset: 0x0029E623
	public float GetFloatSetting(string prefsKey)
	{
		return KPlayerPrefs.GetFloat(prefsKey);
	}

	// Token: 0x06006E7A RID: 28282 RVA: 0x002A042C File Offset: 0x0029E62C
	public void SetFloatSetting(string prefsKey, float value)
	{
		SandboxSettings.Setting<float> setting = this.floatSettings.Find((SandboxSettings.Setting<float> match) => match.PrefsKey == prefsKey);
		if (setting == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"No KPlayerPrefs float setting named: ",
				prefsKey,
				" could be found amongst ",
				this.floatSettings.Count.ToString(),
				" float settings."
			}));
		}
		setting.Value = value;
	}

	// Token: 0x06006E7B RID: 28283 RVA: 0x002A04AF File Offset: 0x0029E6AF
	public void RestoreFloatSetting(string prefsKey)
	{
		if (KPlayerPrefs.HasKey(prefsKey))
		{
			this.SetFloatSetting(prefsKey, this.GetFloatSetting(prefsKey));
			return;
		}
		this.ForceDefaultFloatSetting(prefsKey);
	}

	// Token: 0x06006E7C RID: 28284 RVA: 0x002A04D0 File Offset: 0x0029E6D0
	public void ForceDefaultFloatSetting(string prefsKey)
	{
		this.SetFloatSetting(prefsKey, this.floatSettings.Find((SandboxSettings.Setting<float> match) => match.PrefsKey == prefsKey).defaultValue);
	}

	// Token: 0x06006E7D RID: 28285 RVA: 0x002A0512 File Offset: 0x0029E712
	public void AddStringSetting(string prefsKey, Action<string> setAction, string defaultValue)
	{
		this.stringSettings.Add(new SandboxSettings.Setting<string>(prefsKey, setAction, defaultValue));
	}

	// Token: 0x06006E7E RID: 28286 RVA: 0x002A0527 File Offset: 0x0029E727
	public string GetStringSetting(string prefsKey)
	{
		return KPlayerPrefs.GetString(prefsKey);
	}

	// Token: 0x06006E7F RID: 28287 RVA: 0x002A0530 File Offset: 0x0029E730
	public void SetStringSetting(string prefsKey, string value)
	{
		SandboxSettings.Setting<string> setting = this.stringSettings.Find((SandboxSettings.Setting<string> match) => match.PrefsKey == prefsKey);
		if (setting == null)
		{
			Debug.LogError(string.Concat(new string[]
			{
				"No KPlayerPrefs string setting named: ",
				prefsKey,
				" could be found amongst ",
				this.stringSettings.Count.ToString(),
				" settings."
			}));
		}
		setting.Value = value;
	}

	// Token: 0x06006E80 RID: 28288 RVA: 0x002A05B3 File Offset: 0x0029E7B3
	public void RestoreStringSetting(string prefsKey)
	{
		if (KPlayerPrefs.HasKey(prefsKey))
		{
			this.SetStringSetting(prefsKey, this.GetStringSetting(prefsKey));
			return;
		}
		this.ForceDefaultStringSetting(prefsKey);
	}

	// Token: 0x06006E81 RID: 28289 RVA: 0x002A05D4 File Offset: 0x0029E7D4
	public void ForceDefaultStringSetting(string prefsKey)
	{
		this.SetStringSetting(prefsKey, this.stringSettings.Find((SandboxSettings.Setting<string> match) => match.PrefsKey == prefsKey).defaultValue);
	}

	// Token: 0x06006E82 RID: 28290 RVA: 0x002A0618 File Offset: 0x0029E818
	public SandboxSettings()
	{
		this.AddStringSetting("SandboxTools.SelectedEntity", delegate(string data)
		{
			KPlayerPrefs.SetString("SandboxTools.SelectedEntity", data);
			this.OnChangeEntity();
		}, "MushBar");
		this.AddIntSetting("SandboxTools.SelectedElement", delegate(int data)
		{
			KPlayerPrefs.SetInt("SandboxTools.SelectedElement", data);
			this.OnChangeElement(this.hasRestoredElement);
			this.hasRestoredElement = true;
		}, (int)ElementLoader.GetElementIndex(SimHashes.Oxygen));
		this.AddStringSetting("SandboxTools.SelectedDisease", delegate(string data)
		{
			KPlayerPrefs.SetString("SandboxTools.SelectedDisease", data);
			this.OnChangeDisease();
		}, Db.Get().Diseases.FoodGerms.Id);
		this.AddIntSetting("SandboxTools.DiseaseCount", delegate(int val)
		{
			KPlayerPrefs.SetInt("SandboxTools.DiseaseCount", val);
			this.OnChangeDiseaseCount();
		}, 0);
		this.AddStringSetting("SandboxTools.SelectedStory", delegate(string data)
		{
			KPlayerPrefs.SetString("SandboxTools.SelectedStory", data);
			this.OnChangeStory();
		}, Db.Get().Stories.resources[Db.Get().Stories.resources.Count - 1].Id);
		this.AddIntSetting("SandboxTools.BrushSize", delegate(int val)
		{
			KPlayerPrefs.SetInt("SandboxTools.BrushSize", val);
			this.OnChangeBrushSize();
		}, 1);
		this.AddFloatSetting("SandboxTools.NoiseScale", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandboxTools.NoiseScale", val);
			this.OnChangeNoiseScale();
		}, 1f);
		this.AddFloatSetting("SandboxTools.NoiseDensity", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandboxTools.NoiseDensity", val);
			this.OnChangeNoiseDensity();
		}, 1f);
		this.AddFloatSetting("SandboxTools.Mass", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandboxTools.Mass", val);
			this.OnChangeMass();
		}, 1f);
		this.AddFloatSetting("SandbosTools.Temperature", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandbosTools.Temperature", val);
			this.OnChangeTemperature();
		}, 300f);
		this.AddFloatSetting("SandbosTools.TemperatureAdditive", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandbosTools.TemperatureAdditive", val);
			this.OnChangeAdditiveTemperature();
		}, 5f);
		this.AddFloatSetting("SandbosTools.StressAdditive", delegate(float val)
		{
			KPlayerPrefs.SetFloat("SandbosTools.StressAdditive", val);
			this.OnChangeAdditiveStress();
		}, 50f);
		this.AddIntSetting("SandbosTools.MoraleAdjustment", delegate(int val)
		{
			KPlayerPrefs.SetInt("SandbosTools.MoraleAdjustment", val);
			this.OnChangeMoraleAdjustment();
		}, 50);
	}

	// Token: 0x06006E83 RID: 28291 RVA: 0x002A07F4 File Offset: 0x0029E9F4
	public void RestorePrefs()
	{
		foreach (SandboxSettings.Setting<int> setting in this.intSettings)
		{
			this.RestoreIntSetting(setting.PrefsKey);
		}
		foreach (SandboxSettings.Setting<float> setting2 in this.floatSettings)
		{
			this.RestoreFloatSetting(setting2.PrefsKey);
		}
		foreach (SandboxSettings.Setting<string> setting3 in this.stringSettings)
		{
			this.RestoreStringSetting(setting3.PrefsKey);
		}
	}

	// Token: 0x04004BEC RID: 19436
	private List<SandboxSettings.Setting<int>> intSettings = new List<SandboxSettings.Setting<int>>();

	// Token: 0x04004BED RID: 19437
	private List<SandboxSettings.Setting<float>> floatSettings = new List<SandboxSettings.Setting<float>>();

	// Token: 0x04004BEE RID: 19438
	private List<SandboxSettings.Setting<string>> stringSettings = new List<SandboxSettings.Setting<string>>();

	// Token: 0x04004BEF RID: 19439
	public bool InstantBuild = true;

	// Token: 0x04004BF0 RID: 19440
	private bool hasRestoredElement;

	// Token: 0x04004BF1 RID: 19441
	public Action<bool> OnChangeElement;

	// Token: 0x04004BF2 RID: 19442
	public global::System.Action OnChangeMass;

	// Token: 0x04004BF3 RID: 19443
	public global::System.Action OnChangeDisease;

	// Token: 0x04004BF4 RID: 19444
	public global::System.Action OnChangeDiseaseCount;

	// Token: 0x04004BF5 RID: 19445
	public global::System.Action OnChangeStory;

	// Token: 0x04004BF6 RID: 19446
	public global::System.Action OnChangeEntity;

	// Token: 0x04004BF7 RID: 19447
	public global::System.Action OnChangeBrushSize;

	// Token: 0x04004BF8 RID: 19448
	public global::System.Action OnChangeNoiseScale;

	// Token: 0x04004BF9 RID: 19449
	public global::System.Action OnChangeNoiseDensity;

	// Token: 0x04004BFA RID: 19450
	public global::System.Action OnChangeTemperature;

	// Token: 0x04004BFB RID: 19451
	public global::System.Action OnChangeAdditiveTemperature;

	// Token: 0x04004BFC RID: 19452
	public global::System.Action OnChangeAdditiveStress;

	// Token: 0x04004BFD RID: 19453
	public global::System.Action OnChangeMoraleAdjustment;

	// Token: 0x04004BFE RID: 19454
	public const string KEY_SELECTED_ENTITY = "SandboxTools.SelectedEntity";

	// Token: 0x04004BFF RID: 19455
	public const string KEY_SELECTED_ELEMENT = "SandboxTools.SelectedElement";

	// Token: 0x04004C00 RID: 19456
	public const string KEY_SELECTED_DISEASE = "SandboxTools.SelectedDisease";

	// Token: 0x04004C01 RID: 19457
	public const string KEY_DISEASE_COUNT = "SandboxTools.DiseaseCount";

	// Token: 0x04004C02 RID: 19458
	public const string KEY_SELECTED_STORY = "SandboxTools.SelectedStory";

	// Token: 0x04004C03 RID: 19459
	public const string KEY_BRUSH_SIZE = "SandboxTools.BrushSize";

	// Token: 0x04004C04 RID: 19460
	public const string KEY_NOISE_SCALE = "SandboxTools.NoiseScale";

	// Token: 0x04004C05 RID: 19461
	public const string KEY_NOISE_DENSITY = "SandboxTools.NoiseDensity";

	// Token: 0x04004C06 RID: 19462
	public const string KEY_MASS = "SandboxTools.Mass";

	// Token: 0x04004C07 RID: 19463
	public const string KEY_TEMPERATURE = "SandbosTools.Temperature";

	// Token: 0x04004C08 RID: 19464
	public const string KEY_TEMPERATURE_ADDITIVE = "SandbosTools.TemperatureAdditive";

	// Token: 0x04004C09 RID: 19465
	public const string KEY_STRESS_ADDITIVE = "SandbosTools.StressAdditive";

	// Token: 0x04004C0A RID: 19466
	public const string KEY_MORALE_ADJUSTMENT = "SandbosTools.MoraleAdjustment";

	// Token: 0x02001FC5 RID: 8133
	public class Setting<T>
	{
		// Token: 0x0600B42B RID: 46123 RVA: 0x003DC20D File Offset: 0x003DA40D
		public Setting(string prefsKey, Action<T> setAction, T defaultValue)
		{
			this.prefsKey = prefsKey;
			this.SetAction = setAction;
			this.defaultValue = defaultValue;
		}

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600B42C RID: 46124 RVA: 0x003DC22A File Offset: 0x003DA42A
		public string PrefsKey
		{
			get
			{
				return this.prefsKey;
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (set) Token: 0x0600B42D RID: 46125 RVA: 0x003DC232 File Offset: 0x003DA432
		public T Value
		{
			set
			{
				this.SetAction(value);
			}
		}

		// Token: 0x04009204 RID: 37380
		private string prefsKey;

		// Token: 0x04009205 RID: 37381
		private Action<T> SetAction;

		// Token: 0x04009206 RID: 37382
		public T defaultValue;
	}
}
