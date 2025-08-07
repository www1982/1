using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000749 RID: 1865
[SerializationConfig(MemberSerialization.OptIn)]
public class HighEnergyParticleSpawner : StateMachineComponent<HighEnergyParticleSpawner.StatesInstance>, IHighEnergyParticleDirection, IProgressBarSideScreen, ISingleSliderControl, ISliderControl
{
	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06002F50 RID: 12112 RVA: 0x0010F434 File Offset: 0x0010D634
	public float PredictedPerCycleConsumptionRate
	{
		get
		{
			return (float)Mathf.FloorToInt(this.recentPerSecondConsumptionRate * 0.1f * 600f);
		}
	}

	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06002F51 RID: 12113 RVA: 0x0010F44E File Offset: 0x0010D64E
	// (set) Token: 0x06002F52 RID: 12114 RVA: 0x0010F458 File Offset: 0x0010D658
	public EightDirection Direction
	{
		get
		{
			return this._direction;
		}
		set
		{
			this._direction = value;
			if (this.directionController != null)
			{
				this.directionController.SetRotation((float)(45 * EightDirectionUtil.GetDirectionIndex(this._direction)));
				this.directionController.controller.enabled = false;
				this.directionController.controller.enabled = true;
			}
		}
	}

	// Token: 0x06002F53 RID: 12115 RVA: 0x0010F4B0 File Offset: 0x0010D6B0
	private void OnCopySettings(object data)
	{
		HighEnergyParticleSpawner component = ((GameObject)data).GetComponent<HighEnergyParticleSpawner>();
		if (component != null)
		{
			this.Direction = component.Direction;
			this.particleThreshold = component.particleThreshold;
		}
	}

	// Token: 0x06002F54 RID: 12116 RVA: 0x0010F4EA File Offset: 0x0010D6EA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<HighEnergyParticleSpawner>(-905833192, HighEnergyParticleSpawner.OnCopySettingsDelegate);
	}

	// Token: 0x06002F55 RID: 12117 RVA: 0x0010F504 File Offset: 0x0010D704
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.directionController = new EightDirectionController(base.GetComponent<KBatchedAnimController>(), "redirector_target", "redirect", EightDirectionController.Offset.Infront);
		this.Direction = this.Direction;
		this.particleController = new MeterController(base.GetComponent<KBatchedAnimController>(), "orb_target", "orb_off", Meter.Offset.NoChange, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		this.particleController.gameObject.AddOrGet<LoopingSounds>();
		this.progressMeterController = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
	}

	// Token: 0x06002F56 RID: 12118 RVA: 0x0010F5AF File Offset: 0x0010D7AF
	public float GetProgressBarMaxValue()
	{
		return this.particleThreshold;
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x0010F5B7 File Offset: 0x0010D7B7
	public float GetProgressBarFillPercentage()
	{
		return this.particleStorage.Particles / this.particleThreshold;
	}

	// Token: 0x06002F58 RID: 12120 RVA: 0x0010F5CB File Offset: 0x0010D7CB
	public string GetProgressBarTitleLabel()
	{
		return UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.PROGRESS_BAR_LABEL;
	}

	// Token: 0x06002F59 RID: 12121 RVA: 0x0010F5D8 File Offset: 0x0010D7D8
	public string GetProgressBarLabel()
	{
		return Mathf.FloorToInt(this.particleStorage.Particles).ToString() + "/" + Mathf.FloorToInt(this.particleThreshold).ToString();
	}

	// Token: 0x06002F5A RID: 12122 RVA: 0x0010F61A File Offset: 0x0010D81A
	public string GetProgressBarTooltip()
	{
		return UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.PROGRESS_BAR_TOOLTIP;
	}

	// Token: 0x06002F5B RID: 12123 RVA: 0x0010F626 File Offset: 0x0010D826
	public void DoConsumeParticlesWhileDisabled(float dt)
	{
		this.particleStorage.ConsumeAndGet(dt * 1f);
		this.progressMeterController.SetPositionPercent(this.GetProgressBarFillPercentage());
	}

	// Token: 0x06002F5C RID: 12124 RVA: 0x0010F64C File Offset: 0x0010D84C
	public void LauncherUpdate(float dt)
	{
		this.radiationSampleTimer += dt;
		if (this.radiationSampleTimer >= this.radiationSampleRate)
		{
			this.radiationSampleTimer -= this.radiationSampleRate;
			int num = Grid.PosToCell(this);
			float num2 = Grid.Radiation[num];
			if (num2 != 0f && this.particleStorage.RemainingCapacity() > 0f)
			{
				base.smi.sm.isAbsorbingRadiation.Set(true, base.smi, false);
				this.recentPerSecondConsumptionRate = num2 / 600f;
				this.particleStorage.Store(this.recentPerSecondConsumptionRate * this.radiationSampleRate * 0.1f);
			}
			else
			{
				this.recentPerSecondConsumptionRate = 0f;
				base.smi.sm.isAbsorbingRadiation.Set(false, base.smi, false);
			}
		}
		this.progressMeterController.SetPositionPercent(this.GetProgressBarFillPercentage());
		if (!this.particleVisualPlaying && this.particleStorage.Particles > this.particleThreshold / 2f)
		{
			this.particleController.meterController.Play("orb_pre", KAnim.PlayMode.Once, 1f, 0f);
			this.particleController.meterController.Queue("orb_idle", KAnim.PlayMode.Loop, 1f, 0f);
			this.particleVisualPlaying = true;
		}
		this.launcherTimer += dt;
		if (this.launcherTimer < this.minLaunchInterval)
		{
			return;
		}
		if (this.particleStorage.Particles >= this.particleThreshold)
		{
			this.launcherTimer = 0f;
			int highEnergyParticleOutputCell = base.GetComponent<Building>().GetHighEnergyParticleOutputCell();
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("HighEnergyParticle"), Grid.CellToPosCCC(highEnergyParticleOutputCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2, null, 0);
			gameObject.SetActive(true);
			if (gameObject != null)
			{
				HighEnergyParticle component = gameObject.GetComponent<HighEnergyParticle>();
				component.payload = this.particleStorage.ConsumeAndGet(this.particleThreshold);
				component.SetDirection(this.Direction);
				this.directionController.PlayAnim("redirect_send", KAnim.PlayMode.Once);
				this.directionController.controller.Queue("redirect", KAnim.PlayMode.Once, 1f, 0f);
				this.particleController.meterController.Play("orb_send", KAnim.PlayMode.Once, 1f, 0f);
				this.particleController.meterController.Queue("orb_off", KAnim.PlayMode.Once, 1f, 0f);
				this.particleVisualPlaying = false;
			}
		}
	}

	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06002F5D RID: 12125 RVA: 0x0010F8DD File Offset: 0x0010DADD
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TITLE";
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06002F5E RID: 12126 RVA: 0x0010F8E4 File Offset: 0x0010DAE4
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
		}
	}

	// Token: 0x06002F5F RID: 12127 RVA: 0x0010F8F0 File Offset: 0x0010DAF0
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x0010F8F3 File Offset: 0x0010DAF3
	public float GetSliderMin(int index)
	{
		return (float)this.minSlider;
	}

	// Token: 0x06002F61 RID: 12129 RVA: 0x0010F8FC File Offset: 0x0010DAFC
	public float GetSliderMax(int index)
	{
		return (float)this.maxSlider;
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x0010F905 File Offset: 0x0010DB05
	public float GetSliderValue(int index)
	{
		return this.particleThreshold;
	}

	// Token: 0x06002F63 RID: 12131 RVA: 0x0010F90D File Offset: 0x0010DB0D
	public void SetSliderValue(float value, int index)
	{
		this.particleThreshold = value;
	}

	// Token: 0x06002F64 RID: 12132 RVA: 0x0010F916 File Offset: 0x0010DB16
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP";
	}

	// Token: 0x06002F65 RID: 12133 RVA: 0x0010F91D File Offset: 0x0010DB1D
	string ISliderControl.GetSliderTooltip(int index)
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP"), this.particleThreshold);
	}

	// Token: 0x04001C10 RID: 7184
	[MyCmpReq]
	private HighEnergyParticleStorage particleStorage;

	// Token: 0x04001C11 RID: 7185
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001C12 RID: 7186
	private float recentPerSecondConsumptionRate;

	// Token: 0x04001C13 RID: 7187
	public int minSlider;

	// Token: 0x04001C14 RID: 7188
	public int maxSlider;

	// Token: 0x04001C15 RID: 7189
	[Serialize]
	private EightDirection _direction;

	// Token: 0x04001C16 RID: 7190
	public float minLaunchInterval;

	// Token: 0x04001C17 RID: 7191
	public float radiationSampleRate;

	// Token: 0x04001C18 RID: 7192
	[Serialize]
	public float particleThreshold = 50f;

	// Token: 0x04001C19 RID: 7193
	private EightDirectionController directionController;

	// Token: 0x04001C1A RID: 7194
	private float launcherTimer;

	// Token: 0x04001C1B RID: 7195
	private float radiationSampleTimer;

	// Token: 0x04001C1C RID: 7196
	private MeterController particleController;

	// Token: 0x04001C1D RID: 7197
	private bool particleVisualPlaying;

	// Token: 0x04001C1E RID: 7198
	private MeterController progressMeterController;

	// Token: 0x04001C1F RID: 7199
	[Serialize]
	public Ref<HighEnergyParticlePort> capturedByRef = new Ref<HighEnergyParticlePort>();

	// Token: 0x04001C20 RID: 7200
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001C21 RID: 7201
	private static readonly EventSystem.IntraObjectHandler<HighEnergyParticleSpawner> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<HighEnergyParticleSpawner>(delegate(HighEnergyParticleSpawner component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0200161A RID: 5658
	public class StatesInstance : GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.GameInstance
	{
		// Token: 0x060093EB RID: 37867 RVA: 0x0036DA07 File Offset: 0x0036BC07
		public StatesInstance(HighEnergyParticleSpawner smi)
			: base(smi)
		{
		}
	}

	// Token: 0x0200161B RID: 5659
	public class States : GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner>
	{
		// Token: 0x060093EC RID: 37868 RVA: 0x0036DA10 File Offset: 0x0036BC10
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.ready, false).DefaultState(this.inoperational.empty);
			this.inoperational.empty.EventTransition(GameHashes.OnParticleStorageChanged, this.inoperational.losing, (HighEnergyParticleSpawner.StatesInstance smi) => !smi.GetComponent<HighEnergyParticleStorage>().IsEmpty());
			this.inoperational.losing.ToggleStatusItem(Db.Get().BuildingStatusItems.LosingRadbolts, null).Update(delegate(HighEnergyParticleSpawner.StatesInstance smi, float dt)
			{
				smi.master.DoConsumeParticlesWhileDisabled(dt);
			}, UpdateRate.SIM_1000ms, false).EventTransition(GameHashes.OnParticleStorageChanged, this.inoperational.empty, (HighEnergyParticleSpawner.StatesInstance smi) => smi.GetComponent<HighEnergyParticleStorage>().IsEmpty());
			this.ready.TagTransition(GameTags.Operational, this.inoperational, true).DefaultState(this.ready.idle).Update(delegate(HighEnergyParticleSpawner.StatesInstance smi, float dt)
			{
				smi.master.LauncherUpdate(dt);
			}, UpdateRate.SIM_EVERY_TICK, false);
			this.ready.idle.ParamTransition<bool>(this.isAbsorbingRadiation, this.ready.absorbing, GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.IsTrue).PlayAnim("on");
			this.ready.absorbing.Enter("SetActive(true)", delegate(HighEnergyParticleSpawner.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit("SetActive(false)", delegate(HighEnergyParticleSpawner.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).ParamTransition<bool>(this.isAbsorbingRadiation, this.ready.idle, GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.IsFalse)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.CollectingHEP, (HighEnergyParticleSpawner.StatesInstance smi) => smi.master)
				.PlayAnim("working_loop", KAnim.PlayMode.Loop);
		}

		// Token: 0x040071E2 RID: 29154
		public StateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.BoolParameter isAbsorbingRadiation;

		// Token: 0x040071E3 RID: 29155
		public HighEnergyParticleSpawner.States.ReadyStates ready;

		// Token: 0x040071E4 RID: 29156
		public HighEnergyParticleSpawner.States.InoperationalStates inoperational;

		// Token: 0x02002797 RID: 10135
		public class InoperationalStates : GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State
		{
			// Token: 0x0400AF4D RID: 44877
			public GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State empty;

			// Token: 0x0400AF4E RID: 44878
			public GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State losing;
		}

		// Token: 0x02002798 RID: 10136
		public class ReadyStates : GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State
		{
			// Token: 0x0400AF4F RID: 44879
			public GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State idle;

			// Token: 0x0400AF50 RID: 44880
			public GameStateMachine<HighEnergyParticleSpawner.States, HighEnergyParticleSpawner.StatesInstance, HighEnergyParticleSpawner, object>.State absorbing;
		}
	}
}
