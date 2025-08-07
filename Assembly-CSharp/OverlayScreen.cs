using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000C2E RID: 3118
[AddComponentMenu("KMonoBehaviour/scripts/OverlayScreen")]
public class OverlayScreen : KMonoBehaviour
{
	// Token: 0x170006E5 RID: 1765
	// (get) Token: 0x06005EBB RID: 24251 RVA: 0x0022BDBE File Offset: 0x00229FBE
	public HashedString mode
	{
		get
		{
			return this.currentModeInfo.mode.ViewMode();
		}
	}

	// Token: 0x06005EBC RID: 24252 RVA: 0x0022BDD0 File Offset: 0x00229FD0
	protected override void OnPrefabInit()
	{
		global::Debug.Assert(OverlayScreen.Instance == null);
		OverlayScreen.Instance = this;
		this.powerLabelParent = GameObject.Find("WorldSpaceCanvas").GetComponent<Canvas>();
	}

	// Token: 0x06005EBD RID: 24253 RVA: 0x0022BDFD File Offset: 0x00229FFD
	protected override void OnLoadLevel()
	{
		this.harvestableNotificationPrefab = null;
		this.powerLabelParent = null;
		OverlayScreen.Instance = null;
		OverlayModes.Mode.Clear();
		this.modeInfos = null;
		this.currentModeInfo = default(OverlayScreen.ModeInfo);
		base.OnLoadLevel();
	}

	// Token: 0x06005EBE RID: 24254 RVA: 0x0022BE34 File Offset: 0x0022A034
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.techViewSound = KFMOD.CreateInstance(this.techViewSoundPath);
		this.techViewSoundPlaying = false;
		Shader.SetGlobalVector("_OverlayParams", Vector4.zero);
		this.RegisterModes();
		this.currentModeInfo = this.modeInfos[OverlayModes.None.ID];
	}

	// Token: 0x06005EBF RID: 24255 RVA: 0x0022BE8C File Offset: 0x0022A08C
	private void RegisterModes()
	{
		this.modeInfos.Clear();
		OverlayModes.None none = new OverlayModes.None();
		this.RegisterMode(none);
		this.RegisterMode(new OverlayModes.Oxygen());
		this.RegisterMode(new OverlayModes.Power(this.powerLabelParent, this.powerLabelPrefab, this.batUIPrefab, this.powerLabelOffset, this.batteryUIOffset, this.batteryUITransformerOffset, this.batteryUISmallTransformerOffset));
		this.RegisterMode(new OverlayModes.Temperature());
		this.RegisterMode(new OverlayModes.ThermalConductivity());
		this.RegisterMode(new OverlayModes.Light());
		this.RegisterMode(new OverlayModes.LiquidConduits());
		this.RegisterMode(new OverlayModes.GasConduits());
		this.RegisterMode(new OverlayModes.Decor());
		this.RegisterMode(new OverlayModes.Disease(this.powerLabelParent, this.diseaseOverlayPrefab));
		this.RegisterMode(new OverlayModes.Crop(this.powerLabelParent, this.harvestableNotificationPrefab));
		this.RegisterMode(new OverlayModes.Harvest());
		this.RegisterMode(new OverlayModes.Priorities());
		this.RegisterMode(new OverlayModes.HeatFlow());
		this.RegisterMode(new OverlayModes.Rooms());
		this.RegisterMode(new OverlayModes.Suit(this.powerLabelParent, this.suitOverlayPrefab));
		this.RegisterMode(new OverlayModes.Logic(this.logicModeUIPrefab));
		this.RegisterMode(new OverlayModes.SolidConveyor());
		this.RegisterMode(new OverlayModes.TileMode());
		this.RegisterMode(new OverlayModes.Radiation());
	}

	// Token: 0x06005EC0 RID: 24256 RVA: 0x0022BFD8 File Offset: 0x0022A1D8
	private void RegisterMode(OverlayModes.Mode mode)
	{
		this.modeInfos[mode.ViewMode()] = new OverlayScreen.ModeInfo
		{
			mode = mode
		};
	}

	// Token: 0x06005EC1 RID: 24257 RVA: 0x0022C007 File Offset: 0x0022A207
	private void LateUpdate()
	{
		this.currentModeInfo.mode.Update();
	}

	// Token: 0x06005EC2 RID: 24258 RVA: 0x0022C01C File Offset: 0x0022A21C
	public void ToggleOverlay(HashedString newMode, bool allowSound = true)
	{
		bool flag = allowSound && !(this.currentModeInfo.mode.ViewMode() == newMode);
		if (newMode != OverlayModes.None.ID)
		{
			ManagementMenu.Instance.CloseAll();
		}
		this.currentModeInfo.mode.Disable();
		if (newMode != this.currentModeInfo.mode.ViewMode() && newMode == OverlayModes.None.ID)
		{
			ManagementMenu.Instance.CloseAll();
		}
		SimDebugView.Instance.SetMode(newMode);
		if (!this.modeInfos.TryGetValue(newMode, out this.currentModeInfo))
		{
			this.currentModeInfo = this.modeInfos[OverlayModes.None.ID];
		}
		this.currentModeInfo.mode.Enable();
		if (flag)
		{
			this.UpdateOverlaySounds();
		}
		if (OverlayModes.None.ID == this.currentModeInfo.mode.ViewMode())
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().TechFilterOnMigrated, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			MusicManager.instance.SetDynamicMusicOverlayInactive();
			this.techViewSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			this.techViewSoundPlaying = false;
		}
		else if (!this.techViewSoundPlaying)
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().TechFilterOnMigrated);
			MusicManager.instance.SetDynamicMusicOverlayActive();
			this.techViewSound.start();
			this.techViewSoundPlaying = true;
		}
		if (this.OnOverlayChanged != null)
		{
			this.OnOverlayChanged(this.currentModeInfo.mode.ViewMode());
		}
		this.ActivateLegend();
	}

	// Token: 0x06005EC3 RID: 24259 RVA: 0x0022C1A3 File Offset: 0x0022A3A3
	private void ActivateLegend()
	{
		if (OverlayLegend.Instance == null)
		{
			return;
		}
		OverlayLegend.Instance.SetLegend(this.currentModeInfo.mode, false);
	}

	// Token: 0x06005EC4 RID: 24260 RVA: 0x0022C1C9 File Offset: 0x0022A3C9
	public void Refresh()
	{
		this.LateUpdate();
	}

	// Token: 0x06005EC5 RID: 24261 RVA: 0x0022C1D1 File Offset: 0x0022A3D1
	public HashedString GetMode()
	{
		if (this.currentModeInfo.mode == null)
		{
			return OverlayModes.None.ID;
		}
		return this.currentModeInfo.mode.ViewMode();
	}

	// Token: 0x06005EC6 RID: 24262 RVA: 0x0022C1F8 File Offset: 0x0022A3F8
	private void UpdateOverlaySounds()
	{
		string text = this.currentModeInfo.mode.GetSoundName();
		if (text != "")
		{
			text = GlobalAssets.GetSound(text, false);
			KMonoBehaviour.PlaySound(text);
		}
	}

	// Token: 0x04003F15 RID: 16149
	public static HashSet<Tag> WireIDs = new HashSet<Tag>();

	// Token: 0x04003F16 RID: 16150
	public static HashSet<Tag> GasVentIDs = new HashSet<Tag>();

	// Token: 0x04003F17 RID: 16151
	public static HashSet<Tag> LiquidVentIDs = new HashSet<Tag>();

	// Token: 0x04003F18 RID: 16152
	public static HashSet<Tag> HarvestableIDs = new HashSet<Tag>();

	// Token: 0x04003F19 RID: 16153
	public static HashSet<Tag> DiseaseIDs = new HashSet<Tag>();

	// Token: 0x04003F1A RID: 16154
	public static HashSet<Tag> SuitIDs = new HashSet<Tag>();

	// Token: 0x04003F1B RID: 16155
	public static HashSet<Tag> SolidConveyorIDs = new HashSet<Tag>();

	// Token: 0x04003F1C RID: 16156
	public static HashSet<Tag> RadiationIDs = new HashSet<Tag>();

	// Token: 0x04003F1D RID: 16157
	[SerializeField]
	public EventReference techViewSoundPath;

	// Token: 0x04003F1E RID: 16158
	private EventInstance techViewSound;

	// Token: 0x04003F1F RID: 16159
	private bool techViewSoundPlaying;

	// Token: 0x04003F20 RID: 16160
	public static OverlayScreen Instance;

	// Token: 0x04003F21 RID: 16161
	[Header("Power")]
	[SerializeField]
	private Canvas powerLabelParent;

	// Token: 0x04003F22 RID: 16162
	[SerializeField]
	private LocText powerLabelPrefab;

	// Token: 0x04003F23 RID: 16163
	[SerializeField]
	private BatteryUI batUIPrefab;

	// Token: 0x04003F24 RID: 16164
	[SerializeField]
	private Vector3 powerLabelOffset;

	// Token: 0x04003F25 RID: 16165
	[SerializeField]
	private Vector3 batteryUIOffset;

	// Token: 0x04003F26 RID: 16166
	[SerializeField]
	private Vector3 batteryUITransformerOffset;

	// Token: 0x04003F27 RID: 16167
	[SerializeField]
	private Vector3 batteryUISmallTransformerOffset;

	// Token: 0x04003F28 RID: 16168
	[SerializeField]
	private Color consumerColour;

	// Token: 0x04003F29 RID: 16169
	[SerializeField]
	private Color generatorColour;

	// Token: 0x04003F2A RID: 16170
	[SerializeField]
	private Color buildingDisabledColour = Color.gray;

	// Token: 0x04003F2B RID: 16171
	[Header("Circuits")]
	[SerializeField]
	private Color32 circuitUnpoweredColour;

	// Token: 0x04003F2C RID: 16172
	[SerializeField]
	private Color32 circuitSafeColour;

	// Token: 0x04003F2D RID: 16173
	[SerializeField]
	private Color32 circuitStrainingColour;

	// Token: 0x04003F2E RID: 16174
	[SerializeField]
	private Color32 circuitOverloadingColour;

	// Token: 0x04003F2F RID: 16175
	[Header("Crops")]
	[SerializeField]
	private GameObject harvestableNotificationPrefab;

	// Token: 0x04003F30 RID: 16176
	[Header("Disease")]
	[SerializeField]
	private GameObject diseaseOverlayPrefab;

	// Token: 0x04003F31 RID: 16177
	[Header("Suit")]
	[SerializeField]
	private GameObject suitOverlayPrefab;

	// Token: 0x04003F32 RID: 16178
	[Header("ToolTip")]
	[SerializeField]
	private TextStyleSetting TooltipHeader;

	// Token: 0x04003F33 RID: 16179
	[SerializeField]
	private TextStyleSetting TooltipDescription;

	// Token: 0x04003F34 RID: 16180
	[Header("Logic")]
	[SerializeField]
	private LogicModeUI logicModeUIPrefab;

	// Token: 0x04003F35 RID: 16181
	public Action<HashedString> OnOverlayChanged;

	// Token: 0x04003F36 RID: 16182
	private OverlayScreen.ModeInfo currentModeInfo;

	// Token: 0x04003F37 RID: 16183
	private Dictionary<HashedString, OverlayScreen.ModeInfo> modeInfos = new Dictionary<HashedString, OverlayScreen.ModeInfo>();

	// Token: 0x02001D9A RID: 7578
	private struct ModeInfo
	{
		// Token: 0x04008A1E RID: 35358
		public OverlayModes.Mode mode;
	}
}
