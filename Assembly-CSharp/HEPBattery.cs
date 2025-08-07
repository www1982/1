using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200094C RID: 2380
public class HEPBattery : GameStateMachine<HEPBattery, HEPBattery.Instance, IStateMachineTarget, HEPBattery.Def>
{
	// Token: 0x0600441D RID: 17437 RVA: 0x0018820C File Offset: 0x0018640C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false).Update(delegate(HEPBattery.Instance smi, float dt)
		{
			smi.DoConsumeParticlesWhileDisabled(dt);
			smi.UpdateDecayStatusItem(false);
		}, UpdateRate.SIM_200ms, false);
		this.operational.Enter("SetActive(true)", delegate(HEPBattery.Instance smi)
		{
			smi.operational.SetActive(true, false);
		}).Exit("SetActive(false)", delegate(HEPBattery.Instance smi)
		{
			smi.operational.SetActive(false, false);
		}).PlayAnim("on", KAnim.PlayMode.Loop)
			.TagTransition(GameTags.Operational, this.inoperational, true)
			.Update(new Action<HEPBattery.Instance, float>(this.LauncherUpdate), UpdateRate.SIM_200ms, false);
	}

	// Token: 0x0600441E RID: 17438 RVA: 0x001882F4 File Offset: 0x001864F4
	public void LauncherUpdate(HEPBattery.Instance smi, float dt)
	{
		smi.UpdateDecayStatusItem(true);
		smi.UpdateMeter(null);
		smi.operational.SetActive(smi.particleStorage.Particles > 0f, false);
		smi.launcherTimer += dt;
		if (smi.launcherTimer < smi.def.minLaunchInterval || !smi.AllowSpawnParticles)
		{
			return;
		}
		if (smi.particleStorage.Particles >= smi.particleThreshold)
		{
			smi.launcherTimer = 0f;
			this.Fire(smi);
		}
	}

	// Token: 0x0600441F RID: 17439 RVA: 0x0018837C File Offset: 0x0018657C
	public void Fire(HEPBattery.Instance smi)
	{
		int highEnergyParticleOutputCell = smi.GetComponent<Building>().GetHighEnergyParticleOutputCell();
		GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("HighEnergyParticle"), Grid.CellToPosCCC(highEnergyParticleOutputCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2, null, 0);
		gameObject.SetActive(true);
		if (gameObject != null)
		{
			HighEnergyParticle component = gameObject.GetComponent<HighEnergyParticle>();
			component.payload = smi.particleStorage.ConsumeAndGet(smi.particleThreshold);
			component.SetDirection(smi.def.direction);
		}
	}

	// Token: 0x04002DA0 RID: 11680
	public static readonly HashedString FIRE_PORT_ID = "HEPBatteryFire";

	// Token: 0x04002DA1 RID: 11681
	public GameStateMachine<HEPBattery, HEPBattery.Instance, IStateMachineTarget, HEPBattery.Def>.State inoperational;

	// Token: 0x04002DA2 RID: 11682
	public GameStateMachine<HEPBattery, HEPBattery.Instance, IStateMachineTarget, HEPBattery.Def>.State operational;

	// Token: 0x0200195D RID: 6493
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007BDD RID: 31709
		public float particleDecayRate;

		// Token: 0x04007BDE RID: 31710
		public float minLaunchInterval;

		// Token: 0x04007BDF RID: 31711
		public float minSlider;

		// Token: 0x04007BE0 RID: 31712
		public float maxSlider;

		// Token: 0x04007BE1 RID: 31713
		public EightDirection direction;
	}

	// Token: 0x0200195E RID: 6494
	public new class Instance : GameStateMachine<HEPBattery, HEPBattery.Instance, IStateMachineTarget, HEPBattery.Def>.GameInstance, ISingleSliderControl, ISliderControl
	{
		// Token: 0x06009EE2 RID: 40674 RVA: 0x003979F8 File Offset: 0x00395BF8
		public Instance(IStateMachineTarget master, HEPBattery.Def def)
			: base(master, def)
		{
			base.Subscribe(-801688580, new Action<object>(this.OnLogicValueChanged));
			base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
			this.meterController = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
			this.UpdateMeter(null);
		}

		// Token: 0x06009EE3 RID: 40675 RVA: 0x00397A82 File Offset: 0x00395C82
		public void DoConsumeParticlesWhileDisabled(float dt)
		{
			if (this.m_skipFirstUpdate)
			{
				this.m_skipFirstUpdate = false;
				return;
			}
			this.particleStorage.ConsumeAndGet(dt * base.def.particleDecayRate);
			this.UpdateMeter(null);
		}

		// Token: 0x06009EE4 RID: 40676 RVA: 0x00397AB4 File Offset: 0x00395CB4
		public void UpdateMeter(object data = null)
		{
			this.meterController.SetPositionPercent(this.particleStorage.Particles / this.particleStorage.Capacity());
		}

		// Token: 0x06009EE5 RID: 40677 RVA: 0x00397AD8 File Offset: 0x00395CD8
		public void UpdateDecayStatusItem(bool hasPower)
		{
			if (!hasPower)
			{
				if (this.particleStorage.Particles > 0f)
				{
					if (this.statusHandle == Guid.Empty)
					{
						this.statusHandle = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.LosingRadbolts, null);
						return;
					}
				}
				else if (this.statusHandle != Guid.Empty)
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
					this.statusHandle = Guid.Empty;
					return;
				}
			}
			else if (this.statusHandle != Guid.Empty)
			{
				base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
				this.statusHandle = Guid.Empty;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06009EE6 RID: 40678 RVA: 0x00397B92 File Offset: 0x00395D92
		public bool AllowSpawnParticles
		{
			get
			{
				return this.hasLogicWire && this.isLogicActive;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06009EE7 RID: 40679 RVA: 0x00397BA4 File Offset: 0x00395DA4
		public bool HasLogicWire
		{
			get
			{
				return this.hasLogicWire;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06009EE8 RID: 40680 RVA: 0x00397BAC File Offset: 0x00395DAC
		public bool IsLogicActive
		{
			get
			{
				return this.isLogicActive;
			}
		}

		// Token: 0x06009EE9 RID: 40681 RVA: 0x00397BB4 File Offset: 0x00395DB4
		private LogicCircuitNetwork GetNetwork()
		{
			int portCell = base.GetComponent<LogicPorts>().GetPortCell(HEPBattery.FIRE_PORT_ID);
			return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}

		// Token: 0x06009EEA RID: 40682 RVA: 0x00397BE4 File Offset: 0x00395DE4
		private void OnLogicValueChanged(object data)
		{
			LogicValueChanged logicValueChanged = (LogicValueChanged)data;
			if (logicValueChanged.portID == HEPBattery.FIRE_PORT_ID)
			{
				this.isLogicActive = logicValueChanged.newValue > 0;
				this.hasLogicWire = this.GetNetwork() != null;
			}
		}

		// Token: 0x06009EEB RID: 40683 RVA: 0x00397C28 File Offset: 0x00395E28
		private void OnCopySettings(object data)
		{
			GameObject gameObject = data as GameObject;
			if (gameObject != null)
			{
				HEPBattery.Instance smi = gameObject.GetSMI<HEPBattery.Instance>();
				if (smi != null)
				{
					this.particleThreshold = smi.particleThreshold;
				}
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06009EEC RID: 40684 RVA: 0x00397C55 File Offset: 0x00395E55
		public string SliderTitleKey
		{
			get
			{
				return "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TITLE";
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06009EED RID: 40685 RVA: 0x00397C5C File Offset: 0x00395E5C
		public string SliderUnits
		{
			get
			{
				return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
			}
		}

		// Token: 0x06009EEE RID: 40686 RVA: 0x00397C68 File Offset: 0x00395E68
		public int SliderDecimalPlaces(int index)
		{
			return 0;
		}

		// Token: 0x06009EEF RID: 40687 RVA: 0x00397C6B File Offset: 0x00395E6B
		public float GetSliderMin(int index)
		{
			return base.def.minSlider;
		}

		// Token: 0x06009EF0 RID: 40688 RVA: 0x00397C78 File Offset: 0x00395E78
		public float GetSliderMax(int index)
		{
			return base.def.maxSlider;
		}

		// Token: 0x06009EF1 RID: 40689 RVA: 0x00397C85 File Offset: 0x00395E85
		public float GetSliderValue(int index)
		{
			return this.particleThreshold;
		}

		// Token: 0x06009EF2 RID: 40690 RVA: 0x00397C8D File Offset: 0x00395E8D
		public void SetSliderValue(float value, int index)
		{
			this.particleThreshold = value;
		}

		// Token: 0x06009EF3 RID: 40691 RVA: 0x00397C96 File Offset: 0x00395E96
		public string GetSliderTooltipKey(int index)
		{
			return "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP";
		}

		// Token: 0x06009EF4 RID: 40692 RVA: 0x00397C9D File Offset: 0x00395E9D
		string ISliderControl.GetSliderTooltip(int index)
		{
			return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP"), this.particleThreshold);
		}

		// Token: 0x04007BE2 RID: 31714
		[MyCmpReq]
		public HighEnergyParticleStorage particleStorage;

		// Token: 0x04007BE3 RID: 31715
		[MyCmpGet]
		public Operational operational;

		// Token: 0x04007BE4 RID: 31716
		[MyCmpAdd]
		public CopyBuildingSettings copyBuildingSettings;

		// Token: 0x04007BE5 RID: 31717
		[Serialize]
		public float launcherTimer;

		// Token: 0x04007BE6 RID: 31718
		[Serialize]
		public float particleThreshold = 50f;

		// Token: 0x04007BE7 RID: 31719
		public bool ShowWorkingStatus;

		// Token: 0x04007BE8 RID: 31720
		private bool m_skipFirstUpdate = true;

		// Token: 0x04007BE9 RID: 31721
		private MeterController meterController;

		// Token: 0x04007BEA RID: 31722
		private Guid statusHandle = Guid.Empty;

		// Token: 0x04007BEB RID: 31723
		private bool hasLogicWire;

		// Token: 0x04007BEC RID: 31724
		private bool isLogicActive;
	}
}
