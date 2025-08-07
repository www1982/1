using System;
using Klei.CustomSettings;
using ProcGen;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000CA8 RID: 3240
public class CustomGameSettingSeed : CustomGameSettingWidget
{
	// Token: 0x060063A4 RID: 25508 RVA: 0x00256D88 File Offset: 0x00254F88
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.Input.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		this.Input.onValueChanged.AddListener(new UnityAction<string>(this.OnValueChanged));
		this.RandomizeButton.onClick += this.GetNewRandomSeed;
	}

	// Token: 0x060063A5 RID: 25509 RVA: 0x00256DEA File Offset: 0x00254FEA
	public void Initialize(SeedSettingConfig config)
	{
		this.config = config;
		this.Label.text = config.label;
		this.ToolTip.toolTip = config.tooltip;
		this.GetNewRandomSeed();
	}

	// Token: 0x060063A6 RID: 25510 RVA: 0x00256E1C File Offset: 0x0025501C
	public override void Refresh()
	{
		base.Refresh();
		string currentQualitySettingLevelId = CustomGameSettings.Instance.GetCurrentQualitySettingLevelId(this.config);
		ClusterLayout currentClusterLayout = CustomGameSettings.Instance.GetCurrentClusterLayout();
		this.allowChange = currentClusterLayout.fixedCoordinate == -1;
		this.Input.interactable = this.allowChange;
		this.RandomizeButton.isInteractable = this.allowChange;
		if (this.allowChange)
		{
			this.InputToolTip.enabled = false;
			this.RandomizeButtonToolTip.enabled = false;
		}
		else
		{
			this.InputToolTip.enabled = true;
			this.RandomizeButtonToolTip.enabled = true;
			this.InputToolTip.SetSimpleTooltip(UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.WORLDGEN_SEED.FIXEDSEED);
			this.RandomizeButtonToolTip.SetSimpleTooltip(UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.WORLDGEN_SEED.FIXEDSEED);
		}
		this.Input.text = currentQualitySettingLevelId;
	}

	// Token: 0x060063A7 RID: 25511 RVA: 0x00256EEC File Offset: 0x002550EC
	private char ValidateInput(string text, int charIndex, char addedChar)
	{
		if ('0' > addedChar || addedChar > '9')
		{
			return '\0';
		}
		return addedChar;
	}

	// Token: 0x060063A8 RID: 25512 RVA: 0x00256EFC File Offset: 0x002550FC
	private void OnEndEdit(string text)
	{
		int num;
		try
		{
			num = Convert.ToInt32(text);
		}
		catch
		{
			num = 0;
		}
		this.SetSeed(num);
	}

	// Token: 0x060063A9 RID: 25513 RVA: 0x00256F30 File Offset: 0x00255130
	public void SetSeed(int seed)
	{
		seed = Mathf.Min(seed, int.MaxValue);
		CustomGameSettings.Instance.SetQualitySetting(this.config, seed.ToString());
		this.Refresh();
	}

	// Token: 0x060063AA RID: 25514 RVA: 0x00256F5C File Offset: 0x0025515C
	private void OnValueChanged(string text)
	{
		int num = 0;
		try
		{
			num = Convert.ToInt32(text);
		}
		catch
		{
			if (text.Length > 0)
			{
				this.Input.text = text.Substring(0, text.Length - 1);
			}
			else
			{
				this.Input.text = "";
			}
		}
		if (num > 2147483647)
		{
			this.Input.text = text.Substring(0, text.Length - 1);
		}
	}

	// Token: 0x060063AB RID: 25515 RVA: 0x00256FE0 File Offset: 0x002551E0
	private void GetNewRandomSeed()
	{
		int num = global::UnityEngine.Random.Range(0, int.MaxValue);
		this.SetSeed(num);
	}

	// Token: 0x040043CC RID: 17356
	[SerializeField]
	private LocText Label;

	// Token: 0x040043CD RID: 17357
	[SerializeField]
	private ToolTip ToolTip;

	// Token: 0x040043CE RID: 17358
	[SerializeField]
	private KInputTextField Input;

	// Token: 0x040043CF RID: 17359
	[SerializeField]
	private KButton RandomizeButton;

	// Token: 0x040043D0 RID: 17360
	[SerializeField]
	private ToolTip InputToolTip;

	// Token: 0x040043D1 RID: 17361
	[SerializeField]
	private ToolTip RandomizeButtonToolTip;

	// Token: 0x040043D2 RID: 17362
	private const int MAX_VALID_SEED = 2147483647;

	// Token: 0x040043D3 RID: 17363
	private SeedSettingConfig config;

	// Token: 0x040043D4 RID: 17364
	private bool allowChange = true;
}
