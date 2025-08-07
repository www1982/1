using System;
using System.Collections.Generic;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x0200074D RID: 1869
public class IceKettle : GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>
{
	// Token: 0x06002F79 RID: 12153 RVA: 0x0010FFA0 File Offset: 0x0010E1A0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.noOperational;
		this.root.EventHandlerTransition(GameHashes.WorkableStartWork, this.inUse, (IceKettle.Instance smi, object obj) => true).EventHandler(GameHashes.OnStorageChange, delegate(IceKettle.Instance smi)
		{
			smi.UpdateMeter();
		});
		this.noOperational.TagTransition(GameTags.Operational, this.operational, false);
		this.operational.TagTransition(GameTags.Operational, this.noOperational, true).DefaultState(this.operational.idle);
		this.operational.idle.PlayAnim(IceKettle.IDEL_ANIM_STATE).DefaultState(this.operational.idle.waitingForSolids);
		this.operational.idle.waitingForSolids.ToggleStatusItem(Db.Get().BuildingStatusItems.KettleInsuficientSolids, null).EventTransition(GameHashes.OnStorageChange, this.operational.idle.waitingForSpaceInLiquidTank, (IceKettle.Instance smi) => IceKettle.HasEnoughSolidsToMelt(smi));
		this.operational.idle.waitingForSpaceInLiquidTank.ToggleStatusItem(Db.Get().BuildingStatusItems.KettleInsuficientLiquidSpace, null).EventTransition(GameHashes.OnStorageChange, this.operational.idle.notEnoughFuel, new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.Transition.ConditionCallback(IceKettle.LiquidTankHasCapacityForNextBatch));
		this.operational.idle.notEnoughFuel.ToggleStatusItem(Db.Get().BuildingStatusItems.KettleInsuficientFuel, null).EventTransition(GameHashes.OnStorageChange, this.operational.melting, new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.Transition.ConditionCallback(IceKettle.CanMeltNextBatch));
		this.operational.melting.Toggle("Operational Active State", new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State.Callback(IceKettle.SetOperationalActiveStatesTrue), new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State.Callback(IceKettle.SetOperationalActiveStatesFalse)).DefaultState(this.operational.melting.entering);
		this.operational.melting.entering.PlayAnim(IceKettle.BOILING_PRE_ANIM_NAME, KAnim.PlayMode.Once).OnAnimQueueComplete(this.operational.melting.working);
		this.operational.melting.working.ToggleStatusItem(Db.Get().BuildingStatusItems.KettleMelting, null).DefaultState(this.operational.melting.working.idle).PlayAnim(IceKettle.BOILING_LOOP_ANIM_NAME, KAnim.PlayMode.Loop);
		this.operational.melting.working.idle.ParamTransition<float>(this.MeltingTimer, this.operational.melting.working.complete, new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.Parameter<float>.Callback(IceKettle.IsDoneMelting)).Update(new Action<IceKettle.Instance, float>(IceKettle.MeltingTimerUpdate), UpdateRate.SIM_200ms, false);
		this.operational.melting.working.complete.Enter(new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State.Callback(IceKettle.ResetMeltingTimer)).Enter(new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State.Callback(IceKettle.MeltNextBatch)).EnterTransition(this.operational.melting.working.idle, new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.Transition.ConditionCallback(IceKettle.CanMeltNextBatch))
			.EnterTransition(this.operational.melting.exit, GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.Not(new StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.Transition.ConditionCallback(IceKettle.CanMeltNextBatch)));
		this.operational.melting.exit.PlayAnim(IceKettle.BOILING_PST_ANIM_NAME, KAnim.PlayMode.Once).OnAnimQueueComplete(this.operational.idle);
		this.inUse.EventHandlerTransition(GameHashes.WorkableStopWork, this.noOperational, (IceKettle.Instance smi, object obj) => true).ScheduleGoTo(new Func<IceKettle.Instance, float>(IceKettle.GetInUseTimeout), this.noOperational);
	}

	// Token: 0x06002F7A RID: 12154 RVA: 0x00110389 File Offset: 0x0010E589
	public static void SetOperationalActiveStatesTrue(IceKettle.Instance smi)
	{
		smi.operational.SetActive(true, false);
	}

	// Token: 0x06002F7B RID: 12155 RVA: 0x00110398 File Offset: 0x0010E598
	public static void SetOperationalActiveStatesFalse(IceKettle.Instance smi)
	{
		smi.operational.SetActive(false, false);
	}

	// Token: 0x06002F7C RID: 12156 RVA: 0x001103A7 File Offset: 0x0010E5A7
	public static float GetInUseTimeout(IceKettle.Instance smi)
	{
		return smi.InUseWorkableDuration + 1f;
	}

	// Token: 0x06002F7D RID: 12157 RVA: 0x001103B5 File Offset: 0x0010E5B5
	public static void ResetMeltingTimer(IceKettle.Instance smi)
	{
		smi.sm.MeltingTimer.Set(0f, smi, false);
	}

	// Token: 0x06002F7E RID: 12158 RVA: 0x001103CF File Offset: 0x0010E5CF
	public static bool HasEnoughSolidsToMelt(IceKettle.Instance smi)
	{
		return smi.HasAtLeastOneBatchOfSolidsWaitingToMelt;
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x001103D7 File Offset: 0x0010E5D7
	public static bool LiquidTankHasCapacityForNextBatch(IceKettle.Instance smi)
	{
		return smi.LiquidTankHasCapacityForNextBatch;
	}

	// Token: 0x06002F80 RID: 12160 RVA: 0x001103DF File Offset: 0x0010E5DF
	public static bool HasEnoughFuelForNextBacth(IceKettle.Instance smi)
	{
		return smi.HasEnoughFuelUnitsToMeltNextBatch;
	}

	// Token: 0x06002F81 RID: 12161 RVA: 0x001103E7 File Offset: 0x0010E5E7
	public static bool CanMeltNextBatch(IceKettle.Instance smi)
	{
		return smi.HasAtLeastOneBatchOfSolidsWaitingToMelt && IceKettle.LiquidTankHasCapacityForNextBatch(smi) && IceKettle.HasEnoughFuelForNextBacth(smi);
	}

	// Token: 0x06002F82 RID: 12162 RVA: 0x00110401 File Offset: 0x0010E601
	public static bool IsDoneMelting(IceKettle.Instance smi, float timePassed)
	{
		return timePassed >= smi.MeltDurationPerBatch;
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x00110410 File Offset: 0x0010E610
	public static void MeltingTimerUpdate(IceKettle.Instance smi, float dt)
	{
		float num = smi.sm.MeltingTimer.Get(smi);
		smi.sm.MeltingTimer.Set(num + dt, smi, false);
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x00110445 File Offset: 0x0010E645
	public static void MeltNextBatch(IceKettle.Instance smi)
	{
		smi.MeltNextBatch();
	}

	// Token: 0x04001C34 RID: 7220
	public static string LIQUID_METER_TARGET_NAME = "kettle_meter_target";

	// Token: 0x04001C35 RID: 7221
	public static string LIQUID_METER_ANIM_NAME = "meter_kettle";

	// Token: 0x04001C36 RID: 7222
	public static string IDEL_ANIM_STATE = "on";

	// Token: 0x04001C37 RID: 7223
	public static string BOILING_PRE_ANIM_NAME = "boiling_pre";

	// Token: 0x04001C38 RID: 7224
	public static string BOILING_LOOP_ANIM_NAME = "boiling_loop";

	// Token: 0x04001C39 RID: 7225
	public static string BOILING_PST_ANIM_NAME = "boiling_pst";

	// Token: 0x04001C3A RID: 7226
	private const float InUseTimeout = 5f;

	// Token: 0x04001C3B RID: 7227
	public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State noOperational;

	// Token: 0x04001C3C RID: 7228
	public IceKettle.OperationalStates operational;

	// Token: 0x04001C3D RID: 7229
	public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State inUse;

	// Token: 0x04001C3E RID: 7230
	public StateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.FloatParameter MeltingTimer;

	// Token: 0x02001620 RID: 5664
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x060093FD RID: 37885 RVA: 0x0036E164 File Offset: 0x0036C364
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			List<Descriptor> list = new List<Descriptor>();
			string text = string.Format(UI.BUILDINGEFFECTS.KETTLE_MELT_RATE, GameUtil.GetFormattedMass(this.KGMeltedPerSecond, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			string text2 = string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.KETTLE_MELT_RATE, GameUtil.GetFormattedMass(this.KGToMeltPerBatch, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedTemperature(this.TargetTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			Descriptor descriptor = new Descriptor(text, text2, Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
			return list;
		}

		// Token: 0x040071EB RID: 29163
		public SimHashes exhaust_tag;

		// Token: 0x040071EC RID: 29164
		public Tag targetElementTag;

		// Token: 0x040071ED RID: 29165
		public Tag fuelElementTag;

		// Token: 0x040071EE RID: 29166
		public float KGToMeltPerBatch;

		// Token: 0x040071EF RID: 29167
		public float KGMeltedPerSecond;

		// Token: 0x040071F0 RID: 29168
		public float TargetTemperature;

		// Token: 0x040071F1 RID: 29169
		public float EnergyPerUnitOfLumber;

		// Token: 0x040071F2 RID: 29170
		public float ExhaustMassPerUnitOfLumber;
	}

	// Token: 0x02001621 RID: 5665
	public class WorkingStates : GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State
	{
		// Token: 0x040071F3 RID: 29171
		public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State idle;

		// Token: 0x040071F4 RID: 29172
		public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State complete;
	}

	// Token: 0x02001622 RID: 5666
	public class MeltingStates : GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State
	{
		// Token: 0x040071F5 RID: 29173
		public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State entering;

		// Token: 0x040071F6 RID: 29174
		public IceKettle.WorkingStates working;

		// Token: 0x040071F7 RID: 29175
		public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State exit;
	}

	// Token: 0x02001623 RID: 5667
	public class IdleStates : GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State
	{
		// Token: 0x040071F8 RID: 29176
		public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State notEnoughFuel;

		// Token: 0x040071F9 RID: 29177
		public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State waitingForSolids;

		// Token: 0x040071FA RID: 29178
		public GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State waitingForSpaceInLiquidTank;
	}

	// Token: 0x02001624 RID: 5668
	public class OperationalStates : GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.State
	{
		// Token: 0x040071FB RID: 29179
		public IceKettle.MeltingStates melting;

		// Token: 0x040071FC RID: 29180
		public IceKettle.IdleStates idle;
	}

	// Token: 0x02001625 RID: 5669
	public new class Instance : GameStateMachine<IceKettle, IceKettle.Instance, IStateMachineTarget, IceKettle.Def>.GameInstance
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06009403 RID: 37891 RVA: 0x0036E205 File Offset: 0x0036C405
		public float CurrentTemperatureOfSolidsStored
		{
			get
			{
				if (this.kettleStorage.MassStored() <= 0f)
				{
					return 0f;
				}
				return this.kettleStorage.items[0].GetComponent<PrimaryElement>().Temperature;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06009404 RID: 37892 RVA: 0x0036E23A File Offset: 0x0036C43A
		public float MeltDurationPerBatch
		{
			get
			{
				return base.def.KGToMeltPerBatch / base.def.KGMeltedPerSecond;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06009405 RID: 37893 RVA: 0x0036E253 File Offset: 0x0036C453
		public float FuelUnitsAvailable
		{
			get
			{
				return this.fuelStorage.MassStored();
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06009406 RID: 37894 RVA: 0x0036E260 File Offset: 0x0036C460
		public bool HasAtLeastOneBatchOfSolidsWaitingToMelt
		{
			get
			{
				return this.kettleStorage.MassStored() >= base.def.KGToMeltPerBatch;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06009407 RID: 37895 RVA: 0x0036E27D File Offset: 0x0036C47D
		public bool HasEnoughFuelUnitsToMeltNextBatch
		{
			get
			{
				return this.kettleStorage.MassStored() <= 0f || this.FuelUnitsAvailable >= this.FuelRequiredForNextBratch;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06009408 RID: 37896 RVA: 0x0036E2A4 File Offset: 0x0036C4A4
		public bool LiquidTankHasCapacityForNextBatch
		{
			get
			{
				return this.outputStorage.RemainingCapacity() >= base.def.KGToMeltPerBatch;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06009409 RID: 37897 RVA: 0x0036E2C1 File Offset: 0x0036C4C1
		public float LiquidTankCapacity
		{
			get
			{
				return this.outputStorage.capacityKg;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x0600940A RID: 37898 RVA: 0x0036E2CE File Offset: 0x0036C4CE
		public float LiquidStored
		{
			get
			{
				return this.outputStorage.MassStored();
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x0600940B RID: 37899 RVA: 0x0036E2DB File Offset: 0x0036C4DB
		public float FuelRequiredForNextBratch
		{
			get
			{
				return this.GetUnitsOfFuelRequiredToMelt(this.elementToMelt, base.def.KGToMeltPerBatch, this.CurrentTemperatureOfSolidsStored);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x0600940C RID: 37900 RVA: 0x0036E2FA File Offset: 0x0036C4FA
		public float InUseWorkableDuration
		{
			get
			{
				return this.dupeWorkable.workTime;
			}
		}

		// Token: 0x0600940D RID: 37901 RVA: 0x0036E308 File Offset: 0x0036C508
		public Instance(IStateMachineTarget master, IceKettle.Def def)
			: base(master, def)
		{
			this.elementToMelt = ElementLoader.GetElement(def.targetElementTag);
			this.LiquidMeter = new MeterController(this.animController, IceKettle.LIQUID_METER_TARGET_NAME, IceKettle.LIQUID_METER_ANIM_NAME, Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingFront, Array.Empty<string>());
			Storage[] components = base.gameObject.GetComponents<Storage>();
			this.fuelStorage = components[0];
			this.kettleStorage = components[1];
			this.outputStorage = components[2];
		}

		// Token: 0x0600940E RID: 37902 RVA: 0x0036E378 File Offset: 0x0036C578
		public override void StartSM()
		{
			base.StartSM();
			this.UpdateMeter();
		}

		// Token: 0x0600940F RID: 37903 RVA: 0x0036E386 File Offset: 0x0036C586
		public void UpdateMeter()
		{
			this.LiquidMeter.SetPositionPercent(this.outputStorage.MassStored() / this.outputStorage.capacityKg);
		}

		// Token: 0x06009410 RID: 37904 RVA: 0x0036E3AC File Offset: 0x0036C5AC
		public void MeltNextBatch()
		{
			if (!this.HasAtLeastOneBatchOfSolidsWaitingToMelt)
			{
				return;
			}
			PrimaryElement component = this.kettleStorage.FindFirst(base.def.targetElementTag).GetComponent<PrimaryElement>();
			float num = Mathf.Min(this.GetUnitsOfFuelRequiredToMelt(this.elementToMelt, base.def.KGToMeltPerBatch, component.Temperature), this.FuelUnitsAvailable);
			float num2 = 0f;
			float num3 = 0f;
			SimUtil.DiseaseInfo diseaseInfo;
			this.kettleStorage.ConsumeAndGetDisease(this.elementToMelt.id.CreateTag(), base.def.KGToMeltPerBatch, out num2, out diseaseInfo, out num3);
			this.outputStorage.AddElement(this.elementToMelt.highTempTransitionTarget, num2, base.def.TargetTemperature, diseaseInfo.idx, diseaseInfo.count, false, true);
			float temperature = this.fuelStorage.FindFirst(base.def.fuelElementTag).GetComponent<PrimaryElement>().Temperature;
			this.fuelStorage.ConsumeIgnoringDisease(base.def.fuelElementTag, num);
			float num4 = num * base.def.ExhaustMassPerUnitOfLumber;
			Element element = ElementLoader.FindElementByHash(base.def.exhaust_tag);
			SimMessages.AddRemoveSubstance(Grid.PosToCell(base.gameObject), element.id, null, num4, temperature, byte.MaxValue, 0, true, -1);
		}

		// Token: 0x06009411 RID: 37905 RVA: 0x0036E4F0 File Offset: 0x0036C6F0
		public float GetUnitsOfFuelRequiredToMelt(Element elementToMelt, float massToMelt_KG, float elementToMelt_initialTemperature)
		{
			if (!elementToMelt.IsSolid)
			{
				return -1f;
			}
			float num = massToMelt_KG * elementToMelt.specificHeatCapacity * elementToMelt_initialTemperature;
			float targetTemperature = base.def.TargetTemperature;
			return (massToMelt_KG * elementToMelt.specificHeatCapacity * targetTemperature - num) / base.def.EnergyPerUnitOfLumber;
		}

		// Token: 0x040071FD RID: 29181
		private Storage fuelStorage;

		// Token: 0x040071FE RID: 29182
		private Storage kettleStorage;

		// Token: 0x040071FF RID: 29183
		private Storage outputStorage;

		// Token: 0x04007200 RID: 29184
		private Element elementToMelt;

		// Token: 0x04007201 RID: 29185
		private MeterController LiquidMeter;

		// Token: 0x04007202 RID: 29186
		[MyCmpGet]
		public Operational operational;

		// Token: 0x04007203 RID: 29187
		[MyCmpGet]
		private IceKettleWorkable dupeWorkable;

		// Token: 0x04007204 RID: 29188
		[MyCmpGet]
		private KBatchedAnimController animController;
	}
}
