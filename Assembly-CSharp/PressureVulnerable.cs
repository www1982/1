using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200087C RID: 2172
[SkipSaveFileSerialization]
public class PressureVulnerable : StateMachineComponent<PressureVulnerable.StatesInstance>, IGameObjectEffectDescriptor, IWiltCause, ISlicedSim1000ms
{
	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x06003BBF RID: 15295 RVA: 0x0014B5A4 File Offset: 0x001497A4
	private OccupyArea occupyArea
	{
		get
		{
			if (this._occupyArea == null)
			{
				this._occupyArea = base.GetComponent<OccupyArea>();
			}
			return this._occupyArea;
		}
	}

	// Token: 0x06003BC0 RID: 15296 RVA: 0x0014B5C6 File Offset: 0x001497C6
	public bool IsSafeElement(Element element)
	{
		return this.safe_atmospheres == null || this.safe_atmospheres.Count == 0 || this.safe_atmospheres.Contains(element);
	}

	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x0014B5EE File Offset: 0x001497EE
	public PressureVulnerable.PressureState ExternalPressureState
	{
		get
		{
			return this.pressureState;
		}
	}

	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x0014B5F6 File Offset: 0x001497F6
	public bool IsLethal
	{
		get
		{
			return this.pressureState == PressureVulnerable.PressureState.LethalHigh || this.pressureState == PressureVulnerable.PressureState.LethalLow || !this.testAreaElementSafe;
		}
	}

	// Token: 0x17000417 RID: 1047
	// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x0014B614 File Offset: 0x00149814
	public bool IsNormal
	{
		get
		{
			return this.testAreaElementSafe && this.pressureState == PressureVulnerable.PressureState.Normal;
		}
	}

	// Token: 0x06003BC4 RID: 15300 RVA: 0x0014B62C File Offset: 0x0014982C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Amounts amounts = base.gameObject.GetAmounts();
		this.displayPressureAmount = amounts.Add(new AmountInstance(Db.Get().Amounts.AirPressure, base.gameObject));
	}

	// Token: 0x06003BC5 RID: 15301 RVA: 0x0014B674 File Offset: 0x00149874
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SlicedUpdaterSim1000ms<PressureVulnerable>.instance.RegisterUpdate1000ms(this);
		this.cell = Grid.PosToCell(this);
		base.smi.sm.pressure.Set(1f, base.smi, false);
		base.smi.sm.safe_element.Set(this.testAreaElementSafe, base.smi, false);
		base.smi.StartSM();
	}

	// Token: 0x06003BC6 RID: 15302 RVA: 0x0014B6EE File Offset: 0x001498EE
	protected override void OnCleanUp()
	{
		SlicedUpdaterSim1000ms<PressureVulnerable>.instance.UnregisterUpdate1000ms(this);
		base.OnCleanUp();
	}

	// Token: 0x06003BC7 RID: 15303 RVA: 0x0014B704 File Offset: 0x00149904
	public void Configure(SimHashes[] safeAtmospheres = null)
	{
		this.pressure_sensitive = false;
		this.pressureWarning_Low = float.MinValue;
		this.pressureLethal_Low = float.MinValue;
		this.pressureLethal_High = float.MaxValue;
		this.pressureWarning_High = float.MaxValue;
		this.safe_atmospheres = new HashSet<Element>();
		if (safeAtmospheres != null)
		{
			foreach (SimHashes simHashes in safeAtmospheres)
			{
				this.safe_atmospheres.Add(ElementLoader.FindElementByHash(simHashes));
			}
		}
	}

	// Token: 0x06003BC8 RID: 15304 RVA: 0x0014B778 File Offset: 0x00149978
	public void Configure(float pressureWarningLow = 0.25f, float pressureLethalLow = 0.01f, float pressureWarningHigh = 10f, float pressureLethalHigh = 30f, SimHashes[] safeAtmospheres = null)
	{
		this.pressure_sensitive = true;
		this.pressureWarning_Low = pressureWarningLow;
		this.pressureLethal_Low = pressureLethalLow;
		this.pressureLethal_High = pressureLethalHigh;
		this.pressureWarning_High = pressureWarningHigh;
		this.safe_atmospheres = new HashSet<Element>();
		if (safeAtmospheres != null)
		{
			foreach (SimHashes simHashes in safeAtmospheres)
			{
				this.safe_atmospheres.Add(ElementLoader.FindElementByHash(simHashes));
			}
		}
	}

	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x0014B7DF File Offset: 0x001499DF
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[]
			{
				WiltCondition.Condition.Pressure,
				WiltCondition.Condition.AtmosphereElement
			};
		}
	}

	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x06003BCA RID: 15306 RVA: 0x0014B7F0 File Offset: 0x001499F0
	public string WiltStateString
	{
		get
		{
			string text = "";
			if (base.smi.IsInsideState(base.smi.sm.warningLow) || base.smi.IsInsideState(base.smi.sm.lethalLow))
			{
				text += Db.Get().CreatureStatusItems.AtmosphericPressureTooLow.resolveStringCallback(CREATURES.STATUSITEMS.ATMOSPHERICPRESSURETOOLOW.NAME, this);
			}
			else if (base.smi.IsInsideState(base.smi.sm.warningHigh) || base.smi.IsInsideState(base.smi.sm.lethalHigh))
			{
				text += Db.Get().CreatureStatusItems.AtmosphericPressureTooHigh.resolveStringCallback(CREATURES.STATUSITEMS.ATMOSPHERICPRESSURETOOHIGH.NAME, this);
			}
			else if (base.smi.IsInsideState(base.smi.sm.unsafeElement))
			{
				text += Db.Get().CreatureStatusItems.WrongAtmosphere.resolveStringCallback(CREATURES.STATUSITEMS.WRONGATMOSPHERE.NAME, this);
			}
			return text;
		}
	}

	// Token: 0x06003BCB RID: 15307 RVA: 0x0014B91D File Offset: 0x00149B1D
	public bool IsSafePressure(float pressure)
	{
		return !this.pressure_sensitive || (pressure > this.pressureLethal_Low && pressure < this.pressureLethal_High);
	}

	// Token: 0x06003BCC RID: 15308 RVA: 0x0014B940 File Offset: 0x00149B40
	public void SlicedSim1000ms(float dt)
	{
		float num = base.smi.sm.pressure.Get(base.smi) * 0.7f + this.GetPressureOverArea(this.cell) * 0.3f;
		this.safe_element *= 0.7f;
		if (this.testAreaElementSafe)
		{
			this.safe_element += 0.3f;
		}
		this.displayPressureAmount.value = num;
		bool flag = this.safe_atmospheres == null || this.safe_atmospheres.Count == 0 || this.safe_element >= 0.06f;
		base.smi.sm.safe_element.Set(flag, base.smi, false);
		base.smi.sm.pressure.Set(num, base.smi, false);
	}

	// Token: 0x06003BCD RID: 15309 RVA: 0x0014BA1F File Offset: 0x00149C1F
	public float GetExternalPressure()
	{
		return this.GetPressureOverArea(this.cell);
	}

	// Token: 0x06003BCE RID: 15310 RVA: 0x0014BA30 File Offset: 0x00149C30
	private float GetPressureOverArea(int cell)
	{
		bool flag = this.testAreaElementSafe;
		PressureVulnerable.testAreaPressure = 0f;
		PressureVulnerable.testAreaCount = 0;
		PressureVulnerable.testAreaSafeElementCount = 0;
		this.testAreaElementSafe = false;
		this.currentAtmoElement = null;
		this.occupyArea.TestArea(cell, this, PressureVulnerable.testAreaCB);
		this.testAreaElementSafe = (this.allCellsMustBeSafe ? (PressureVulnerable.testAreaSafeElementCount == this.occupyArea.OccupiedCellsOffsets.Length) : (PressureVulnerable.testAreaSafeElementCount > 0));
		PressureVulnerable.testAreaPressure = ((PressureVulnerable.testAreaCount > 0) ? (PressureVulnerable.testAreaPressure / (float)PressureVulnerable.testAreaCount) : 0f);
		if (this.testAreaElementSafe != flag)
		{
			base.Trigger(-2023773544, null);
		}
		return PressureVulnerable.testAreaPressure;
	}

	// Token: 0x06003BCF RID: 15311 RVA: 0x0014BAE4 File Offset: 0x00149CE4
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.pressure_sensitive)
		{
			list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_PRESSURE, GameUtil.GetFormattedMass(this.pressureWarning_Low, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_PRESSURE, GameUtil.GetFormattedMass(this.pressureWarning_Low, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		}
		if (this.safe_atmospheres != null && this.safe_atmospheres.Count > 0)
		{
			string text = "";
			bool flag = false;
			bool flag2 = false;
			foreach (Element element in this.safe_atmospheres)
			{
				flag |= element.IsGas;
				flag2 |= element.IsLiquid;
				text = text + "\n        • " + element.name;
			}
			if (flag && flag2)
			{
				list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_ATMOSPHERE, text), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_ATMOSPHERE_MIXED, text), Descriptor.DescriptorType.Requirement, false));
			}
			if (flag)
			{
				list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_ATMOSPHERE, text), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_ATMOSPHERE, text), Descriptor.DescriptorType.Requirement, false));
			}
			else
			{
				list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_ATMOSPHERE, text), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_ATMOSPHERE_LIQUID, text), Descriptor.DescriptorType.Requirement, false));
			}
		}
		return list;
	}

	// Token: 0x040024A0 RID: 9376
	private const float kTrailingWeight = 0.7f;

	// Token: 0x040024A1 RID: 9377
	private const float kLeadingWeight = 0.3f;

	// Token: 0x040024A2 RID: 9378
	private const float kSafeElementThreshold = 0.06f;

	// Token: 0x040024A3 RID: 9379
	private float safe_element = 1f;

	// Token: 0x040024A4 RID: 9380
	private OccupyArea _occupyArea;

	// Token: 0x040024A5 RID: 9381
	public float pressureLethal_Low;

	// Token: 0x040024A6 RID: 9382
	public float pressureWarning_Low;

	// Token: 0x040024A7 RID: 9383
	public float pressureWarning_High;

	// Token: 0x040024A8 RID: 9384
	public float pressureLethal_High;

	// Token: 0x040024A9 RID: 9385
	private static float testAreaPressure;

	// Token: 0x040024AA RID: 9386
	private static int testAreaCount;

	// Token: 0x040024AB RID: 9387
	private static int testAreaSafeElementCount;

	// Token: 0x040024AC RID: 9388
	public bool testAreaElementSafe = true;

	// Token: 0x040024AD RID: 9389
	public Element currentAtmoElement;

	// Token: 0x040024AE RID: 9390
	private static Func<int, object, bool> testAreaCB = delegate(int test_cell, object data)
	{
		PressureVulnerable pressureVulnerable = (PressureVulnerable)data;
		if (!Grid.IsSolidCell(test_cell))
		{
			Element element = Grid.Element[test_cell];
			if (pressureVulnerable.IsSafeElement(element))
			{
				PressureVulnerable.testAreaPressure += Grid.Mass[test_cell];
				PressureVulnerable.testAreaCount++;
				PressureVulnerable.testAreaSafeElementCount++;
				pressureVulnerable.currentAtmoElement = element;
			}
			if (pressureVulnerable.currentAtmoElement == null)
			{
				pressureVulnerable.currentAtmoElement = element;
			}
		}
		return true;
	};

	// Token: 0x040024AF RID: 9391
	private AmountInstance displayPressureAmount;

	// Token: 0x040024B0 RID: 9392
	public bool pressure_sensitive = true;

	// Token: 0x040024B1 RID: 9393
	public bool allCellsMustBeSafe;

	// Token: 0x040024B2 RID: 9394
	public HashSet<Element> safe_atmospheres = new HashSet<Element>();

	// Token: 0x040024B3 RID: 9395
	private int cell;

	// Token: 0x040024B4 RID: 9396
	private PressureVulnerable.PressureState pressureState = PressureVulnerable.PressureState.Normal;

	// Token: 0x02001831 RID: 6193
	public class StatesInstance : GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.GameInstance
	{
		// Token: 0x06009BD9 RID: 39897 RVA: 0x0038F372 File Offset: 0x0038D572
		public StatesInstance(PressureVulnerable master)
			: base(master)
		{
			if (Db.Get().Amounts.Maturity.Lookup(base.gameObject) != null)
			{
				this.hasMaturity = true;
			}
		}

		// Token: 0x04007836 RID: 30774
		public bool hasMaturity;
	}

	// Token: 0x02001832 RID: 6194
	public class States : GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable>
	{
		// Token: 0x06009BDA RID: 39898 RVA: 0x0038F3A0 File Offset: 0x0038D5A0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.normal;
			this.lethalLow.ParamTransition<float>(this.pressure, this.warningLow, (PressureVulnerable.StatesInstance smi, float p) => p > smi.master.pressureLethal_Low).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse).Enter(delegate(PressureVulnerable.StatesInstance smi)
			{
				smi.master.pressureState = PressureVulnerable.PressureState.LethalLow;
			})
				.TriggerOnEnter(GameHashes.LowPressureFatal, null);
			this.lethalHigh.ParamTransition<float>(this.pressure, this.warningHigh, (PressureVulnerable.StatesInstance smi, float p) => p < smi.master.pressureLethal_High).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse).Enter(delegate(PressureVulnerable.StatesInstance smi)
			{
				smi.master.pressureState = PressureVulnerable.PressureState.LethalHigh;
			})
				.TriggerOnEnter(GameHashes.HighPressureFatal, null);
			this.warningLow.ParamTransition<float>(this.pressure, this.lethalLow, (PressureVulnerable.StatesInstance smi, float p) => p < smi.master.pressureLethal_Low).ParamTransition<float>(this.pressure, this.normal, (PressureVulnerable.StatesInstance smi, float p) => p > smi.master.pressureWarning_Low).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse)
				.Enter(delegate(PressureVulnerable.StatesInstance smi)
				{
					smi.master.pressureState = PressureVulnerable.PressureState.WarningLow;
				})
				.TriggerOnEnter(GameHashes.LowPressureWarning, null);
			this.unsafeElement.ParamTransition<bool>(this.safe_element, this.normal, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsTrue).TriggerOnExit(GameHashes.CorrectAtmosphere, null).TriggerOnEnter(GameHashes.WrongAtmosphere, null);
			this.warningHigh.ParamTransition<float>(this.pressure, this.lethalHigh, (PressureVulnerable.StatesInstance smi, float p) => p > smi.master.pressureLethal_High).ParamTransition<float>(this.pressure, this.normal, (PressureVulnerable.StatesInstance smi, float p) => p < smi.master.pressureWarning_High).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse)
				.Enter(delegate(PressureVulnerable.StatesInstance smi)
				{
					smi.master.pressureState = PressureVulnerable.PressureState.WarningHigh;
				})
				.TriggerOnEnter(GameHashes.HighPressureWarning, null);
			this.normal.ParamTransition<float>(this.pressure, this.warningHigh, (PressureVulnerable.StatesInstance smi, float p) => p > smi.master.pressureWarning_High).ParamTransition<float>(this.pressure, this.warningLow, (PressureVulnerable.StatesInstance smi, float p) => p < smi.master.pressureWarning_Low).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse)
				.Enter(delegate(PressureVulnerable.StatesInstance smi)
				{
					smi.master.pressureState = PressureVulnerable.PressureState.Normal;
				})
				.TriggerOnEnter(GameHashes.OptimalPressureAchieved, null);
		}

		// Token: 0x04007837 RID: 30775
		public StateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.FloatParameter pressure;

		// Token: 0x04007838 RID: 30776
		public StateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.BoolParameter safe_element;

		// Token: 0x04007839 RID: 30777
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State unsafeElement;

		// Token: 0x0400783A RID: 30778
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State lethalLow;

		// Token: 0x0400783B RID: 30779
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State lethalHigh;

		// Token: 0x0400783C RID: 30780
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State warningLow;

		// Token: 0x0400783D RID: 30781
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State warningHigh;

		// Token: 0x0400783E RID: 30782
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State normal;
	}

	// Token: 0x02001833 RID: 6195
	public enum PressureState
	{
		// Token: 0x04007840 RID: 30784
		LethalLow,
		// Token: 0x04007841 RID: 30785
		WarningLow,
		// Token: 0x04007842 RID: 30786
		Normal,
		// Token: 0x04007843 RID: 30787
		WarningHigh,
		// Token: 0x04007844 RID: 30788
		LethalHigh
	}
}
