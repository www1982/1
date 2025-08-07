using System;
using System.Collections.Generic;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x02000784 RID: 1924
public class MercuryLight : GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>
{
	// Token: 0x060032C0 RID: 12992 RVA: 0x0011D908 File Offset: 0x0011BB08
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.noOperational;
		this.noOperational.Enter(new StateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State.Callback(MercuryLight.SetOperationalActiveFlagOff)).ParamTransition<float>(this.Charge, this.noOperational.depleating, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsGTZero).ParamTransition<float>(this.Charge, this.noOperational.idle, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsLTEZero);
		this.noOperational.depleating.TagTransition(GameTags.Operational, this.operational, false).PlayAnim("depleating", KAnim.PlayMode.Loop).ToggleStatusItem(Db.Get().BuildingStatusItems.EmittingLight, null)
			.ToggleStatusItem(Db.Get().BuildingStatusItems.MercuryLight_Depleating, null)
			.ParamTransition<float>(this.Charge, this.noOperational.depleated, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsLTEZero)
			.Update(new Action<MercuryLight.Instance, float>(MercuryLight.DepleteUpdate), UpdateRate.SIM_200ms, false);
		this.noOperational.depleated.TagTransition(GameTags.Operational, this.operational, false).PlayAnim("on_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.noOperational.idle);
		this.noOperational.idle.TagTransition(GameTags.Operational, this.noOperational.exit, false).PlayAnim("off", KAnim.PlayMode.Once).ToggleStatusItem(Db.Get().BuildingStatusItems.MercuryLight_Depleated, null);
		this.noOperational.exit.PlayAnim("on_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.operational);
		this.operational.TagTransition(GameTags.Operational, this.noOperational, true).DefaultState(this.operational.darkness).Update(new Action<MercuryLight.Instance, float>(MercuryLight.ConsumeFuelUpdate), UpdateRate.SIM_200ms, false);
		this.operational.darkness.Enter(new StateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State.Callback(MercuryLight.SetOperationalActiveFlagOff)).ParamTransition<bool>(this.HasEnoughFuel, this.operational.light, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsTrue).ParamTransition<float>(this.Charge, this.operational.darkness.depleating, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsGTZero)
			.ParamTransition<float>(this.Charge, this.operational.darkness.idle, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsLTEZero);
		this.operational.darkness.depleating.PlayAnim("depleating", KAnim.PlayMode.Loop).ToggleStatusItem(Db.Get().BuildingStatusItems.EmittingLight, null).ToggleStatusItem(Db.Get().BuildingStatusItems.MercuryLight_Depleating, null)
			.ParamTransition<float>(this.Charge, this.operational.darkness.depleated, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsLTEZero)
			.Update(new Action<MercuryLight.Instance, float>(MercuryLight.DepleteUpdate), UpdateRate.SIM_200ms, false);
		this.operational.darkness.depleated.PlayAnim("on_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.operational.darkness.idle);
		this.operational.darkness.idle.PlayAnim("off", KAnim.PlayMode.Once).ToggleStatusItem(Db.Get().BuildingStatusItems.MercuryLight_Depleated, null).ParamTransition<float>(this.Charge, this.operational.darkness.depleating, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsGTZero);
		this.operational.light.Enter(new StateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State.Callback(MercuryLight.SetOperationalActiveFlagOn)).PlayAnim("on", KAnim.PlayMode.Loop).ParamTransition<bool>(this.HasEnoughFuel, this.operational.darkness, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsFalse)
			.ToggleStatusItem(Db.Get().BuildingStatusItems.EmittingLight, null)
			.DefaultState(this.operational.light.charging);
		this.operational.light.charging.ToggleStatusItem(Db.Get().BuildingStatusItems.MercuryLight_Charging, null).ParamTransition<float>(this.Charge, this.operational.light.idle, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsGTEOne).Update(new Action<MercuryLight.Instance, float>(MercuryLight.ChargeUpdate), UpdateRate.SIM_200ms, false);
		this.operational.light.idle.ToggleStatusItem(Db.Get().BuildingStatusItems.MercuryLight_Charged, null).ParamTransition<float>(this.Charge, this.operational.light.charging, GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.IsLTOne);
	}

	// Token: 0x060032C1 RID: 12993 RVA: 0x0011DD48 File Offset: 0x0011BF48
	public static void SetOperationalActiveFlagOn(MercuryLight.Instance smi)
	{
		smi.operational.SetActive(true, false);
	}

	// Token: 0x060032C2 RID: 12994 RVA: 0x0011DD57 File Offset: 0x0011BF57
	public static void SetOperationalActiveFlagOff(MercuryLight.Instance smi)
	{
		smi.operational.SetActive(false, false);
	}

	// Token: 0x060032C3 RID: 12995 RVA: 0x0011DD66 File Offset: 0x0011BF66
	public static void DepleteUpdate(MercuryLight.Instance smi, float dt)
	{
		smi.DepleteUpdate(dt);
	}

	// Token: 0x060032C4 RID: 12996 RVA: 0x0011DD6F File Offset: 0x0011BF6F
	public static void ChargeUpdate(MercuryLight.Instance smi, float dt)
	{
		smi.ChargeUpdate(dt);
	}

	// Token: 0x060032C5 RID: 12997 RVA: 0x0011DD78 File Offset: 0x0011BF78
	public static void ConsumeFuelUpdate(MercuryLight.Instance smi, float dt)
	{
		smi.ConsumeFuelUpdate(dt);
	}

	// Token: 0x04001E69 RID: 7785
	private static Tag ELEMENT_TAG = SimHashes.Mercury.CreateTag();

	// Token: 0x04001E6A RID: 7786
	private const string ON_ANIM_NAME = "on";

	// Token: 0x04001E6B RID: 7787
	private const string ON_PRE_ANIM_NAME = "on_pre";

	// Token: 0x04001E6C RID: 7788
	private const string TRANSITION_TO_OFF_ANIM_NAME = "on_pst";

	// Token: 0x04001E6D RID: 7789
	private const string DEPLEATING_ANIM_NAME = "depleating";

	// Token: 0x04001E6E RID: 7790
	private const string OFF_ANIM_NAME = "off";

	// Token: 0x04001E6F RID: 7791
	private const string LIGHT_LEVEL_METER_TARGET_NAME = "meter_target";

	// Token: 0x04001E70 RID: 7792
	private const string LIGHT_LEVEL_METER_ANIM_NAME = "meter";

	// Token: 0x04001E71 RID: 7793
	public MercuryLight.Darknesstates noOperational;

	// Token: 0x04001E72 RID: 7794
	public MercuryLight.OperationalStates operational;

	// Token: 0x04001E73 RID: 7795
	public StateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.FloatParameter Charge;

	// Token: 0x04001E74 RID: 7796
	public StateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.BoolParameter HasEnoughFuel;

	// Token: 0x02001672 RID: 5746
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x0600953D RID: 38205 RVA: 0x0037364C File Offset: 0x0037184C
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			string text = MercuryLight.ELEMENT_TAG.ProperName();
			List<Descriptor> list = new List<Descriptor>();
			Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(this.FUEL_MASS_PER_SECOND, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMED, text, GameUtil.GetFormattedMass(this.FUEL_MASS_PER_SECOND, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false);
			list.Add(descriptor);
			return list;
		}

		// Token: 0x040072BE RID: 29374
		public float MAX_LUX;

		// Token: 0x040072BF RID: 29375
		public float TURN_ON_DELAY;

		// Token: 0x040072C0 RID: 29376
		public float FUEL_MASS_PER_SECOND;
	}

	// Token: 0x02001673 RID: 5747
	public class LightStates : GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State
	{
		// Token: 0x040072C1 RID: 29377
		public GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State charging;

		// Token: 0x040072C2 RID: 29378
		public GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State idle;
	}

	// Token: 0x02001674 RID: 5748
	public class Darknesstates : GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State
	{
		// Token: 0x040072C3 RID: 29379
		public GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State depleating;

		// Token: 0x040072C4 RID: 29380
		public GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State depleated;

		// Token: 0x040072C5 RID: 29381
		public GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State idle;

		// Token: 0x040072C6 RID: 29382
		public GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State exit;
	}

	// Token: 0x02001675 RID: 5749
	public class OperationalStates : GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.State
	{
		// Token: 0x040072C7 RID: 29383
		public MercuryLight.LightStates light;

		// Token: 0x040072C8 RID: 29384
		public MercuryLight.Darknesstates darkness;
	}

	// Token: 0x02001676 RID: 5750
	public new class Instance : GameStateMachine<MercuryLight, MercuryLight.Instance, IStateMachineTarget, MercuryLight.Def>.GameInstance
	{
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06009542 RID: 38210 RVA: 0x003736DF File Offset: 0x003718DF
		public bool HasEnoughFuel
		{
			get
			{
				return base.sm.HasEnoughFuel.Get(this);
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06009543 RID: 38211 RVA: 0x003736F2 File Offset: 0x003718F2
		public int LuxLevel
		{
			get
			{
				return Mathf.FloorToInt(base.smi.ChargeLevel * base.def.MAX_LUX);
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06009544 RID: 38212 RVA: 0x00373711 File Offset: 0x00371911
		public float ChargeLevel
		{
			get
			{
				return base.smi.sm.Charge.Get(this);
			}
		}

		// Token: 0x06009545 RID: 38213 RVA: 0x0037372C File Offset: 0x0037192C
		public Instance(IStateMachineTarget master, MercuryLight.Def def)
			: base(master, def)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			this.lightIntensityMeterController = new MeterController(component, "meter_target", "meter", Meter.Offset.NoChange, Grid.SceneLayer.Building, Array.Empty<string>());
		}

		// Token: 0x06009546 RID: 38214 RVA: 0x00373766 File Offset: 0x00371966
		public override void StartSM()
		{
			base.StartSM();
			this.SetChargeLevel(this.ChargeLevel);
		}

		// Token: 0x06009547 RID: 38215 RVA: 0x0037377C File Offset: 0x0037197C
		public void DepleteUpdate(float dt)
		{
			float num = Mathf.Clamp(this.ChargeLevel - dt / base.def.TURN_ON_DELAY, 0f, 1f);
			this.SetChargeLevel(num);
		}

		// Token: 0x06009548 RID: 38216 RVA: 0x003737B4 File Offset: 0x003719B4
		public void ChargeUpdate(float dt)
		{
			float num = Mathf.Clamp(this.ChargeLevel + dt / base.def.TURN_ON_DELAY, 0f, 1f);
			this.SetChargeLevel(num);
		}

		// Token: 0x06009549 RID: 38217 RVA: 0x003737EC File Offset: 0x003719EC
		public void SetChargeLevel(float value)
		{
			base.sm.Charge.Set(value, this, false);
			this.light.Lux = this.LuxLevel;
			this.light.FullRefresh();
			bool flag = this.ChargeLevel > 0f;
			if (this.light.enabled != flag)
			{
				this.light.enabled = flag;
			}
			this.lightIntensityMeterController.SetPositionPercent(value);
		}

		// Token: 0x0600954A RID: 38218 RVA: 0x00373860 File Offset: 0x00371A60
		public void ConsumeFuelUpdate(float dt)
		{
			float num = base.def.FUEL_MASS_PER_SECOND * dt;
			if (this.storage.MassStored() < num)
			{
				base.sm.HasEnoughFuel.Set(false, this, false);
				return;
			}
			float num2;
			SimUtil.DiseaseInfo diseaseInfo;
			float num3;
			this.storage.ConsumeAndGetDisease(MercuryLight.ELEMENT_TAG, num, out num2, out diseaseInfo, out num3);
			base.sm.HasEnoughFuel.Set(true, this, false);
		}

		// Token: 0x0600954B RID: 38219 RVA: 0x003738C9 File Offset: 0x00371AC9
		public bool CanRun()
		{
			return true;
		}

		// Token: 0x040072C9 RID: 29385
		[MyCmpGet]
		public Operational operational;

		// Token: 0x040072CA RID: 29386
		[MyCmpGet]
		private Light2D light;

		// Token: 0x040072CB RID: 29387
		[MyCmpGet]
		private Storage storage;

		// Token: 0x040072CC RID: 29388
		[MyCmpGet]
		private ConduitConsumer conduitConsumer;

		// Token: 0x040072CD RID: 29389
		private MeterController lightIntensityMeterController;
	}
}
