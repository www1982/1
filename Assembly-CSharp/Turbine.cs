using System;
using Klei;
using KSerialization;
using UnityEngine;

// Token: 0x020007EC RID: 2028
[AddComponentMenu("KMonoBehaviour/scripts/Turbine")]
public class Turbine : KMonoBehaviour
{
	// Token: 0x0600371A RID: 14106 RVA: 0x00132380 File Offset: 0x00130580
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.simEmitCBHandle = Game.Instance.massEmitCallbackManager.Add(new Action<Sim.MassEmittedCallback, object>(Turbine.OnSimEmittedCallback), this, "TurbineEmit");
		BuildingDef def = base.GetComponent<BuildingComplete>().Def;
		this.srcCells = new int[def.WidthInCells];
		this.destCells = new int[def.WidthInCells];
		int num = Grid.PosToCell(this);
		for (int i = 0; i < def.WidthInCells; i++)
		{
			int num2 = i - (def.WidthInCells - 1) / 2;
			this.srcCells[i] = Grid.OffsetCell(num, new CellOffset(num2, -1));
			this.destCells[i] = Grid.OffsetCell(num, new CellOffset(num2, def.HeightInCells - 1));
		}
		this.smi = new Turbine.Instance(this);
		this.smi.StartSM();
		this.CreateMeter();
	}

	// Token: 0x0600371B RID: 14107 RVA: 0x0013245C File Offset: 0x0013065C
	private void CreateMeter()
	{
		this.meter = new MeterController(base.gameObject.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_OL", "meter_frame", "meter_fill" });
		this.smi.UpdateMeter();
	}

	// Token: 0x0600371C RID: 14108 RVA: 0x001324B8 File Offset: 0x001306B8
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("cleanup");
		}
		Game.Instance.massEmitCallbackManager.Release(this.simEmitCBHandle, "Turbine");
		this.simEmitCBHandle.Clear();
		base.OnCleanUp();
	}

	// Token: 0x0600371D RID: 14109 RVA: 0x0013250C File Offset: 0x0013070C
	private void Pump(float dt)
	{
		float num = this.pumpKGRate * dt / (float)this.srcCells.Length;
		foreach (int num2 in this.srcCells)
		{
			HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(Turbine.OnSimConsumeCallback), this, "TurbineConsume");
			SimMessages.ConsumeMass(num2, this.srcElem, num, 1, handle.index);
		}
	}

	// Token: 0x0600371E RID: 14110 RVA: 0x0013257A File Offset: 0x0013077A
	private static void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		((Turbine)data).OnSimConsume(mass_cb_info);
	}

	// Token: 0x0600371F RID: 14111 RVA: 0x00132588 File Offset: 0x00130788
	private void OnSimConsume(Sim.MassConsumedCallback mass_cb_info)
	{
		if (mass_cb_info.mass > 0f)
		{
			this.storedTemperature = SimUtil.CalculateFinalTemperature(this.storedMass, this.storedTemperature, mass_cb_info.mass, mass_cb_info.temperature);
			this.storedMass += mass_cb_info.mass;
			SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(this.diseaseIdx, this.diseaseCount, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount);
			this.diseaseIdx = diseaseInfo.idx;
			this.diseaseCount = diseaseInfo.count;
			if (this.storedMass > this.minEmitMass && this.simEmitCBHandle.IsValid())
			{
				float num = this.storedMass / (float)this.destCells.Length;
				int num2 = this.diseaseCount / this.destCells.Length;
				Game.Instance.massEmitCallbackManager.GetItem(this.simEmitCBHandle);
				int[] array = this.destCells;
				for (int i = 0; i < array.Length; i++)
				{
					SimMessages.EmitMass(array[i], mass_cb_info.elemIdx, num, this.emitTemperature, this.diseaseIdx, num2, this.simEmitCBHandle.index);
				}
				this.storedMass = 0f;
				this.storedTemperature = 0f;
				this.diseaseIdx = byte.MaxValue;
				this.diseaseCount = 0;
			}
		}
	}

	// Token: 0x06003720 RID: 14112 RVA: 0x001326D2 File Offset: 0x001308D2
	private static void OnSimEmittedCallback(Sim.MassEmittedCallback info, object data)
	{
		((Turbine)data).OnSimEmitted(info);
	}

	// Token: 0x06003721 RID: 14113 RVA: 0x001326E0 File Offset: 0x001308E0
	private void OnSimEmitted(Sim.MassEmittedCallback info)
	{
		if (info.suceeded != 1)
		{
			this.storedTemperature = SimUtil.CalculateFinalTemperature(this.storedMass, this.storedTemperature, info.mass, info.temperature);
			this.storedMass += info.mass;
			if (info.diseaseIdx != 255)
			{
				SimUtil.DiseaseInfo diseaseInfo = new SimUtil.DiseaseInfo
				{
					idx = this.diseaseIdx,
					count = this.diseaseCount
				};
				SimUtil.DiseaseInfo diseaseInfo2 = new SimUtil.DiseaseInfo
				{
					idx = info.diseaseIdx,
					count = info.diseaseCount
				};
				SimUtil.DiseaseInfo diseaseInfo3 = SimUtil.CalculateFinalDiseaseInfo(diseaseInfo, diseaseInfo2);
				this.diseaseIdx = diseaseInfo3.idx;
				this.diseaseCount = diseaseInfo3.count;
			}
		}
	}

	// Token: 0x06003722 RID: 14114 RVA: 0x001327A4 File Offset: 0x001309A4
	public static void InitializeStatusItems()
	{
		Turbine.inputBlockedStatusItem = new StatusItem("TURBINE_BLOCKED_INPUT", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		Turbine.outputBlockedStatusItem = new StatusItem("TURBINE_BLOCKED_OUTPUT", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		Turbine.spinningUpStatusItem = new StatusItem("TURBINE_SPINNING_UP", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
		Turbine.activeStatusItem = new StatusItem("TURBINE_ACTIVE", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
		Turbine.activeStatusItem.resolveStringCallback = delegate(string str, object data)
		{
			Turbine turbine = (Turbine)data;
			str = string.Format(str, (int)turbine.currentRPM);
			return str;
		};
		Turbine.insufficientMassStatusItem = new StatusItem("TURBINE_INSUFFICIENT_MASS", "BUILDING", "status_item_resource_unavailable", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022, null);
		Turbine.insufficientMassStatusItem.resolveTooltipCallback = delegate(string str, object data)
		{
			Turbine turbine2 = (Turbine)data;
			str = str.Replace("{MASS}", GameUtil.GetFormattedMass(turbine2.requiredMassFlowDifferential, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			str = str.Replace("{SRC_ELEMENT}", ElementLoader.FindElementByHash(turbine2.srcElem).name);
			return str;
		};
		Turbine.insufficientTemperatureStatusItem = new StatusItem("TURBINE_INSUFFICIENT_TEMPERATURE", "BUILDING", "status_item_plant_temperature", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022, null);
		Turbine.insufficientTemperatureStatusItem.resolveStringCallback = new Func<string, object, string>(Turbine.ResolveStrings);
		Turbine.insufficientTemperatureStatusItem.resolveTooltipCallback = new Func<string, object, string>(Turbine.ResolveStrings);
	}

	// Token: 0x06003723 RID: 14115 RVA: 0x00132920 File Offset: 0x00130B20
	private static string ResolveStrings(string str, object data)
	{
		Turbine turbine = (Turbine)data;
		str = str.Replace("{SRC_ELEMENT}", ElementLoader.FindElementByHash(turbine.srcElem).name);
		str = str.Replace("{ACTIVE_TEMPERATURE}", GameUtil.GetFormattedTemperature(turbine.minActiveTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		return str;
	}

	// Token: 0x04002153 RID: 8531
	public SimHashes srcElem;

	// Token: 0x04002154 RID: 8532
	public float requiredMassFlowDifferential = 3f;

	// Token: 0x04002155 RID: 8533
	public float activePercent = 0.75f;

	// Token: 0x04002156 RID: 8534
	public float minEmitMass;

	// Token: 0x04002157 RID: 8535
	public float minActiveTemperature = 400f;

	// Token: 0x04002158 RID: 8536
	public float emitTemperature = 300f;

	// Token: 0x04002159 RID: 8537
	public float maxRPM;

	// Token: 0x0400215A RID: 8538
	public float rpmAcceleration;

	// Token: 0x0400215B RID: 8539
	public float rpmDeceleration;

	// Token: 0x0400215C RID: 8540
	public float minGenerationRPM;

	// Token: 0x0400215D RID: 8541
	public float pumpKGRate;

	// Token: 0x0400215E RID: 8542
	private static readonly HashedString TINT_SYMBOL = new HashedString("meter_fill");

	// Token: 0x0400215F RID: 8543
	[Serialize]
	private float storedMass;

	// Token: 0x04002160 RID: 8544
	[Serialize]
	private float storedTemperature;

	// Token: 0x04002161 RID: 8545
	[Serialize]
	private byte diseaseIdx = byte.MaxValue;

	// Token: 0x04002162 RID: 8546
	[Serialize]
	private int diseaseCount;

	// Token: 0x04002163 RID: 8547
	[MyCmpGet]
	private Generator generator;

	// Token: 0x04002164 RID: 8548
	[Serialize]
	private float currentRPM;

	// Token: 0x04002165 RID: 8549
	private int[] srcCells;

	// Token: 0x04002166 RID: 8550
	private int[] destCells;

	// Token: 0x04002167 RID: 8551
	private Turbine.Instance smi;

	// Token: 0x04002168 RID: 8552
	private static StatusItem inputBlockedStatusItem;

	// Token: 0x04002169 RID: 8553
	private static StatusItem outputBlockedStatusItem;

	// Token: 0x0400216A RID: 8554
	private static StatusItem insufficientMassStatusItem;

	// Token: 0x0400216B RID: 8555
	private static StatusItem insufficientTemperatureStatusItem;

	// Token: 0x0400216C RID: 8556
	private static StatusItem activeStatusItem;

	// Token: 0x0400216D RID: 8557
	private static StatusItem spinningUpStatusItem;

	// Token: 0x0400216E RID: 8558
	private MeterController meter;

	// Token: 0x0400216F RID: 8559
	private HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle simEmitCBHandle = HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.InvalidHandle;

	// Token: 0x02001747 RID: 5959
	public class States : GameStateMachine<Turbine.States, Turbine.Instance, Turbine>
	{
		// Token: 0x0600987C RID: 39036 RVA: 0x00380B88 File Offset: 0x0037ED88
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			Turbine.InitializeStatusItems();
			default_state = this.operational;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational.spinningUp, (Turbine.Instance smi) => smi.master.GetComponent<Operational>().IsOperational).QueueAnim("off", false, null).Enter(delegate(Turbine.Instance smi)
			{
				smi.master.currentRPM = 0f;
				smi.UpdateMeter();
			});
			this.operational.DefaultState(this.operational.spinningUp).EventTransition(GameHashes.OperationalChanged, this.inoperational, (Turbine.Instance smi) => !smi.master.GetComponent<Operational>().IsOperational).Update("UpdateOperational", delegate(Turbine.Instance smi, float dt)
			{
				smi.UpdateState(dt);
			}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(Turbine.Instance smi)
				{
					smi.DisableStatusItems();
				});
			this.operational.idle.QueueAnim("on", false, null);
			this.operational.spinningUp.ToggleStatusItem((Turbine.Instance smi) => Turbine.spinningUpStatusItem, (Turbine.Instance smi) => smi.master).QueueAnim("buildup", true, null);
			this.operational.active.Update("UpdateActive", delegate(Turbine.Instance smi, float dt)
			{
				smi.master.Pump(dt);
			}, UpdateRate.SIM_200ms, false).ToggleStatusItem((Turbine.Instance smi) => Turbine.activeStatusItem, (Turbine.Instance smi) => smi.master).Enter(delegate(Turbine.Instance smi)
			{
				smi.GetComponent<KAnimControllerBase>().Play(Turbine.States.ACTIVE_ANIMS, KAnim.PlayMode.Loop);
				smi.GetComponent<Operational>().SetActive(true, false);
			})
				.Exit(delegate(Turbine.Instance smi)
				{
					smi.master.GetComponent<Generator>().ResetJoules();
					smi.GetComponent<Operational>().SetActive(false, false);
				});
		}

		// Token: 0x04007532 RID: 30002
		public GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State inoperational;

		// Token: 0x04007533 RID: 30003
		public Turbine.States.OperationalStates operational;

		// Token: 0x04007534 RID: 30004
		private static readonly HashedString[] ACTIVE_ANIMS = new HashedString[] { "working_pre", "working_loop" };

		// Token: 0x020027F7 RID: 10231
		public class OperationalStates : GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State
		{
			// Token: 0x0400B14E RID: 45390
			public GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State idle;

			// Token: 0x0400B14F RID: 45391
			public GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State spinningUp;

			// Token: 0x0400B150 RID: 45392
			public GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State active;
		}
	}

	// Token: 0x02001748 RID: 5960
	public class Instance : GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.GameInstance
	{
		// Token: 0x0600987F RID: 39039 RVA: 0x00380E15 File Offset: 0x0037F015
		public Instance(Turbine master)
			: base(master)
		{
		}

		// Token: 0x06009880 RID: 39040 RVA: 0x00380E4C File Offset: 0x0037F04C
		public void UpdateState(float dt)
		{
			float num = (this.CanSteamFlow(ref this.insufficientMass, ref this.insufficientTemperature) ? base.master.rpmAcceleration : (-base.master.rpmDeceleration));
			base.master.currentRPM = Mathf.Clamp(base.master.currentRPM + dt * num, 0f, base.master.maxRPM);
			this.UpdateMeter();
			this.UpdateStatusItems();
			StateMachine.BaseState currentState = base.smi.GetCurrentState();
			if (base.master.currentRPM >= base.master.minGenerationRPM)
			{
				if (currentState != base.sm.operational.active)
				{
					base.smi.GoTo(base.sm.operational.active);
				}
				base.smi.master.generator.GenerateJoules(base.smi.master.generator.WattageRating * dt, false);
				return;
			}
			if (base.master.currentRPM > 0f)
			{
				if (currentState != base.sm.operational.spinningUp)
				{
					base.smi.GoTo(base.sm.operational.spinningUp);
					return;
				}
			}
			else if (currentState != base.sm.operational.idle)
			{
				base.smi.GoTo(base.sm.operational.idle);
			}
		}

		// Token: 0x06009881 RID: 39041 RVA: 0x00380FB4 File Offset: 0x0037F1B4
		public void UpdateMeter()
		{
			if (base.master.meter != null)
			{
				float num = Mathf.Clamp01(base.master.currentRPM / base.master.maxRPM);
				base.master.meter.SetPositionPercent(num);
				base.master.meter.SetSymbolTint(Turbine.TINT_SYMBOL, (num >= base.master.activePercent) ? Color.green : Color.red);
			}
		}

		// Token: 0x06009882 RID: 39042 RVA: 0x00381038 File Offset: 0x0037F238
		private bool CanSteamFlow(ref bool insufficient_mass, ref bool insufficient_temperature)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = float.PositiveInfinity;
			this.isInputBlocked = false;
			for (int i = 0; i < base.master.srcCells.Length; i++)
			{
				int num4 = base.master.srcCells[i];
				float num5 = Grid.Mass[num4];
				if (Grid.Element[num4].id == base.master.srcElem)
				{
					num = Mathf.Max(num, num5);
				}
				float num6 = Grid.Temperature[num4];
				num2 = Mathf.Max(num2, num6);
				ushort num7 = Grid.ElementIdx[num4];
				Element element = ElementLoader.elements[(int)num7];
				if (element.IsLiquid || element.IsSolid)
				{
					this.isInputBlocked = true;
				}
			}
			this.isOutputBlocked = false;
			for (int j = 0; j < base.master.destCells.Length; j++)
			{
				int num8 = base.master.destCells[j];
				float num9 = Grid.Mass[num8];
				num3 = Mathf.Min(num3, num9);
				ushort num10 = Grid.ElementIdx[num8];
				Element element2 = ElementLoader.elements[(int)num10];
				if (element2.IsLiquid || element2.IsSolid)
				{
					this.isOutputBlocked = true;
				}
			}
			insufficient_mass = num - num3 < base.master.requiredMassFlowDifferential;
			insufficient_temperature = num2 < base.master.minActiveTemperature;
			return !insufficient_mass && !insufficient_temperature;
		}

		// Token: 0x06009883 RID: 39043 RVA: 0x003811B4 File Offset: 0x0037F3B4
		public void UpdateStatusItems()
		{
			KSelectable component = base.GetComponent<KSelectable>();
			this.inputBlockedHandle = this.UpdateStatusItem(Turbine.inputBlockedStatusItem, this.isInputBlocked, this.inputBlockedHandle, component);
			this.outputBlockedHandle = this.UpdateStatusItem(Turbine.outputBlockedStatusItem, this.isOutputBlocked, this.outputBlockedHandle, component);
			this.insufficientMassHandle = this.UpdateStatusItem(Turbine.insufficientMassStatusItem, this.insufficientMass, this.insufficientMassHandle, component);
			this.insufficientTemperatureHandle = this.UpdateStatusItem(Turbine.insufficientTemperatureStatusItem, this.insufficientTemperature, this.insufficientTemperatureHandle, component);
		}

		// Token: 0x06009884 RID: 39044 RVA: 0x00381240 File Offset: 0x0037F440
		private Guid UpdateStatusItem(StatusItem item, bool show, Guid current_handle, KSelectable ksel)
		{
			Guid guid = current_handle;
			if (show != (current_handle != Guid.Empty))
			{
				if (show)
				{
					guid = ksel.AddStatusItem(item, base.master);
				}
				else
				{
					guid = ksel.RemoveStatusItem(current_handle, false);
				}
			}
			return guid;
		}

		// Token: 0x06009885 RID: 39045 RVA: 0x0038127C File Offset: 0x0037F47C
		public void DisableStatusItems()
		{
			KSelectable component = base.GetComponent<KSelectable>();
			component.RemoveStatusItem(this.inputBlockedHandle, false);
			component.RemoveStatusItem(this.outputBlockedHandle, false);
			component.RemoveStatusItem(this.insufficientMassHandle, false);
			component.RemoveStatusItem(this.insufficientTemperatureHandle, false);
		}

		// Token: 0x04007535 RID: 30005
		public bool isInputBlocked;

		// Token: 0x04007536 RID: 30006
		public bool isOutputBlocked;

		// Token: 0x04007537 RID: 30007
		public bool insufficientMass;

		// Token: 0x04007538 RID: 30008
		public bool insufficientTemperature;

		// Token: 0x04007539 RID: 30009
		private Guid inputBlockedHandle = Guid.Empty;

		// Token: 0x0400753A RID: 30010
		private Guid outputBlockedHandle = Guid.Empty;

		// Token: 0x0400753B RID: 30011
		private Guid insufficientMassHandle = Guid.Empty;

		// Token: 0x0400753C RID: 30012
		private Guid insufficientTemperatureHandle = Guid.Empty;
	}
}
