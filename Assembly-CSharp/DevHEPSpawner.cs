using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000711 RID: 1809
[SerializationConfig(MemberSerialization.OptIn)]
public class DevHEPSpawner : StateMachineComponent<DevHEPSpawner.StatesInstance>, IHighEnergyParticleDirection, ISingleSliderControl, ISliderControl
{
	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06002D63 RID: 11619 RVA: 0x00104918 File Offset: 0x00102B18
	// (set) Token: 0x06002D64 RID: 11620 RVA: 0x00104920 File Offset: 0x00102B20
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

	// Token: 0x06002D65 RID: 11621 RVA: 0x00104978 File Offset: 0x00102B78
	private void OnCopySettings(object data)
	{
		DevHEPSpawner component = ((GameObject)data).GetComponent<DevHEPSpawner>();
		if (component != null)
		{
			this.Direction = component.Direction;
			this.boltAmount = component.boltAmount;
		}
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x001049B2 File Offset: 0x00102BB2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<DevHEPSpawner>(-905833192, DevHEPSpawner.OnCopySettingsDelegate);
	}

	// Token: 0x06002D67 RID: 11623 RVA: 0x001049CC File Offset: 0x00102BCC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.directionController = new EightDirectionController(base.GetComponent<KBatchedAnimController>(), "redirector_target", "redirect", EightDirectionController.Offset.Infront);
		this.Direction = this.Direction;
		this.particleController = new MeterController(base.GetComponent<KBatchedAnimController>(), "orb_target", "orb_off", Meter.Offset.NoChange, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		this.particleController.gameObject.AddOrGet<LoopingSounds>();
		this.progressMeterController = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
	}

	// Token: 0x06002D68 RID: 11624 RVA: 0x00104A6C File Offset: 0x00102C6C
	public void LauncherUpdate(float dt)
	{
		if (this.boltAmount <= 0f)
		{
			return;
		}
		this.launcherTimer += dt;
		this.progressMeterController.SetPositionPercent(this.launcherTimer / 5f);
		if (this.launcherTimer > 5f)
		{
			this.launcherTimer -= 5f;
			int highEnergyParticleOutputCell = base.GetComponent<Building>().GetHighEnergyParticleOutputCell();
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("HighEnergyParticle"), Grid.CellToPosCCC(highEnergyParticleOutputCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2, null, 0);
			gameObject.SetActive(true);
			if (gameObject != null)
			{
				HighEnergyParticle component = gameObject.GetComponent<HighEnergyParticle>();
				component.payload = this.boltAmount;
				component.SetDirection(this.Direction);
				this.directionController.PlayAnim("redirect_send", KAnim.PlayMode.Once);
				this.directionController.controller.Queue("redirect", KAnim.PlayMode.Once, 1f, 0f);
				this.particleController.meterController.Play("orb_send", KAnim.PlayMode.Once, 1f, 0f);
				this.particleController.meterController.Queue("orb_off", KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}

	// Token: 0x1700026D RID: 621
	// (get) Token: 0x06002D69 RID: 11625 RVA: 0x00104BAB File Offset: 0x00102DAB
	public string SliderTitleKey
	{
		get
		{
			return "";
		}
	}

	// Token: 0x1700026E RID: 622
	// (get) Token: 0x06002D6A RID: 11626 RVA: 0x00104BB2 File Offset: 0x00102DB2
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
		}
	}

	// Token: 0x06002D6B RID: 11627 RVA: 0x00104BBE File Offset: 0x00102DBE
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x06002D6C RID: 11628 RVA: 0x00104BC1 File Offset: 0x00102DC1
	public float GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x00104BC8 File Offset: 0x00102DC8
	public float GetSliderMax(int index)
	{
		return 500f;
	}

	// Token: 0x06002D6E RID: 11630 RVA: 0x00104BCF File Offset: 0x00102DCF
	public float GetSliderValue(int index)
	{
		return this.boltAmount;
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x00104BD7 File Offset: 0x00102DD7
	public void SetSliderValue(float value, int index)
	{
		this.boltAmount = value;
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x00104BE0 File Offset: 0x00102DE0
	public string GetSliderTooltipKey(int index)
	{
		return "";
	}

	// Token: 0x06002D71 RID: 11633 RVA: 0x00104BE7 File Offset: 0x00102DE7
	string ISliderControl.GetSliderTooltip(int index)
	{
		return "";
	}

	// Token: 0x04001AB2 RID: 6834
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001AB3 RID: 6835
	[Serialize]
	private EightDirection _direction;

	// Token: 0x04001AB4 RID: 6836
	public float boltAmount;

	// Token: 0x04001AB5 RID: 6837
	private EightDirectionController directionController;

	// Token: 0x04001AB6 RID: 6838
	private float launcherTimer;

	// Token: 0x04001AB7 RID: 6839
	private MeterController particleController;

	// Token: 0x04001AB8 RID: 6840
	private MeterController progressMeterController;

	// Token: 0x04001AB9 RID: 6841
	[Serialize]
	public Ref<HighEnergyParticlePort> capturedByRef = new Ref<HighEnergyParticlePort>();

	// Token: 0x04001ABA RID: 6842
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001ABB RID: 6843
	private static readonly EventSystem.IntraObjectHandler<DevHEPSpawner> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<DevHEPSpawner>(delegate(DevHEPSpawner component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x020015AE RID: 5550
	public class StatesInstance : GameStateMachine<DevHEPSpawner.States, DevHEPSpawner.StatesInstance, DevHEPSpawner, object>.GameInstance
	{
		// Token: 0x06009258 RID: 37464 RVA: 0x00366A05 File Offset: 0x00364C05
		public StatesInstance(DevHEPSpawner smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020015AF RID: 5551
	public class States : GameStateMachine<DevHEPSpawner.States, DevHEPSpawner.StatesInstance, DevHEPSpawner>
	{
		// Token: 0x06009259 RID: 37465 RVA: 0x00366A10 File Offset: 0x00364C10
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.ready, false);
			this.ready.PlayAnim("on").TagTransition(GameTags.Operational, this.inoperational, true).Update(delegate(DevHEPSpawner.StatesInstance smi, float dt)
			{
				smi.master.LauncherUpdate(dt);
			}, UpdateRate.SIM_EVERY_TICK, false);
		}

		// Token: 0x0400708D RID: 28813
		public StateMachine<DevHEPSpawner.States, DevHEPSpawner.StatesInstance, DevHEPSpawner, object>.BoolParameter isAbsorbingRadiation;

		// Token: 0x0400708E RID: 28814
		public GameStateMachine<DevHEPSpawner.States, DevHEPSpawner.StatesInstance, DevHEPSpawner, object>.State ready;

		// Token: 0x0400708F RID: 28815
		public GameStateMachine<DevHEPSpawner.States, DevHEPSpawner.StatesInstance, DevHEPSpawner, object>.State inoperational;
	}
}
