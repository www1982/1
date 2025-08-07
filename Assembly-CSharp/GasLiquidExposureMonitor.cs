using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020009EF RID: 2543
public class GasLiquidExposureMonitor : GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>
{
	// Token: 0x06004A40 RID: 19008 RVA: 0x001AE31C File Offset: 0x001AC51C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.normal;
		this.root.Update(new Action<GasLiquidExposureMonitor.Instance, float>(this.UpdateExposure), UpdateRate.SIM_33ms, false);
		this.normal.ParamTransition<bool>(this.isIrritated, this.irritated, (GasLiquidExposureMonitor.Instance smi, bool p) => this.isIrritated.Get(smi));
		this.irritated.ParamTransition<bool>(this.isIrritated, this.normal, (GasLiquidExposureMonitor.Instance smi, bool p) => !this.isIrritated.Get(smi)).ToggleStatusItem(Db.Get().DuplicantStatusItems.GasLiquidIrritation, (GasLiquidExposureMonitor.Instance smi) => smi).DefaultState(this.irritated.irritated);
		this.irritated.irritated.Transition(this.irritated.rubbingEyes, new StateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.Transition.ConditionCallback(GasLiquidExposureMonitor.CanReact), UpdateRate.SIM_200ms);
		this.irritated.rubbingEyes.Exit(delegate(GasLiquidExposureMonitor.Instance smi)
		{
			smi.lastReactTime = GameClock.Instance.GetTime();
		}).ToggleReactable((GasLiquidExposureMonitor.Instance smi) => smi.GetReactable()).OnSignal(this.reactFinished, this.irritated.irritated);
	}

	// Token: 0x06004A41 RID: 19009 RVA: 0x001AE469 File Offset: 0x001AC669
	private static bool CanReact(GasLiquidExposureMonitor.Instance smi)
	{
		return GameClock.Instance.GetTime() > smi.lastReactTime + 60f;
	}

	// Token: 0x06004A42 RID: 19010 RVA: 0x001AE484 File Offset: 0x001AC684
	private static void InitializeCustomRates()
	{
		if (GasLiquidExposureMonitor.customExposureRates != null)
		{
			return;
		}
		GasLiquidExposureMonitor.minorIrritationEffect = Db.Get().effects.Get("MinorIrritation");
		GasLiquidExposureMonitor.majorIrritationEffect = Db.Get().effects.Get("MajorIrritation");
		GasLiquidExposureMonitor.customExposureRates = new Dictionary<SimHashes, float>();
		float num = -1f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Water] = num;
		float num2 = -0.25f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.CarbonDioxide] = num2;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Oxygen] = num2;
		float num3 = 0f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.ContaminatedOxygen] = num3;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.DirtyWater] = num3;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.ViscoGel] = num3;
		float num4 = 0.5f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Hydrogen] = num4;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.SaltWater] = num4;
		float num5 = 1f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.ChlorineGas] = num5;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.EthanolGas] = num5;
		float num6 = 3f;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Chlorine] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.SourGas] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Brine] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Ethanol] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.SuperCoolant] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.CrudeOil] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Naphtha] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Petroleum] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.Mercury] = num6;
		GasLiquidExposureMonitor.customExposureRates[SimHashes.MercuryGas] = num6;
	}

	// Token: 0x06004A43 RID: 19011 RVA: 0x001AE648 File Offset: 0x001AC848
	public float GetCurrentExposure(GasLiquidExposureMonitor.Instance smi)
	{
		float num;
		if (GasLiquidExposureMonitor.customExposureRates.TryGetValue(smi.CurrentlyExposedToElement().id, out num))
		{
			return num;
		}
		return 0f;
	}

	// Token: 0x06004A44 RID: 19012 RVA: 0x001AE678 File Offset: 0x001AC878
	private void UpdateExposure(GasLiquidExposureMonitor.Instance smi, float dt)
	{
		GasLiquidExposureMonitor.InitializeCustomRates();
		float num = 0f;
		smi.isInAirtightEnvironment = false;
		smi.isImmuneToIrritability = false;
		int num2 = Grid.CellAbove(Grid.PosToCell(smi.gameObject));
		if (Grid.IsValidCell(num2))
		{
			Element element = Grid.Element[num2];
			float num3;
			if (!GasLiquidExposureMonitor.customExposureRates.TryGetValue(element.id, out num3))
			{
				if (Grid.Temperature[num2] >= -13657.5f && Grid.Temperature[num2] <= 27315f)
				{
					num3 = 1f;
				}
				else
				{
					num3 = 2f;
				}
			}
			if (smi.effects.HasImmunityTo(GasLiquidExposureMonitor.minorIrritationEffect) || smi.effects.HasImmunityTo(GasLiquidExposureMonitor.majorIrritationEffect))
			{
				smi.isImmuneToIrritability = true;
				num = GasLiquidExposureMonitor.customExposureRates[SimHashes.Oxygen];
			}
			if ((smi.master.gameObject.HasTag(GameTags.HasSuitTank) && smi.gameObject.GetComponent<SuitEquipper>().IsWearingAirtightSuit()) || smi.master.gameObject.HasTag(GameTags.InTransitTube))
			{
				smi.isInAirtightEnvironment = true;
				num = GasLiquidExposureMonitor.customExposureRates[SimHashes.Oxygen];
			}
			if (!smi.isInAirtightEnvironment && !smi.isImmuneToIrritability)
			{
				if (element.IsGas)
				{
					num = num3 * Grid.Mass[num2] / 1f;
				}
				else if (element.IsLiquid)
				{
					num = num3 * Grid.Mass[num2] / 1000f;
				}
			}
		}
		smi.exposureRate = num;
		smi.exposure += smi.exposureRate * dt;
		smi.exposure = MathUtil.Clamp(0f, 30f, smi.exposure);
		this.ApplyEffects(smi);
	}

	// Token: 0x06004A45 RID: 19013 RVA: 0x001AE828 File Offset: 0x001ACA28
	private void ApplyEffects(GasLiquidExposureMonitor.Instance smi)
	{
		if (smi.IsMinorIrritation())
		{
			if (smi.effects.Add(GasLiquidExposureMonitor.minorIrritationEffect, true) != null)
			{
				this.isIrritated.Set(true, smi, false);
				return;
			}
		}
		else if (smi.IsMajorIrritation())
		{
			if (smi.effects.Add(GasLiquidExposureMonitor.majorIrritationEffect, true) != null)
			{
				this.isIrritated.Set(true, smi, false);
				return;
			}
		}
		else
		{
			smi.effects.Remove(GasLiquidExposureMonitor.minorIrritationEffect);
			smi.effects.Remove(GasLiquidExposureMonitor.majorIrritationEffect);
			this.isIrritated.Set(false, smi, false);
		}
	}

	// Token: 0x06004A46 RID: 19014 RVA: 0x001AE8BA File Offset: 0x001ACABA
	public Effect GetAppliedEffect(GasLiquidExposureMonitor.Instance smi)
	{
		if (smi.IsMinorIrritation())
		{
			return GasLiquidExposureMonitor.minorIrritationEffect;
		}
		if (smi.IsMajorIrritation())
		{
			return GasLiquidExposureMonitor.majorIrritationEffect;
		}
		return null;
	}

	// Token: 0x040030FE RID: 12542
	public const float MIN_REACT_INTERVAL = 60f;

	// Token: 0x040030FF RID: 12543
	private static Dictionary<SimHashes, float> customExposureRates;

	// Token: 0x04003100 RID: 12544
	private static Effect minorIrritationEffect;

	// Token: 0x04003101 RID: 12545
	private static Effect majorIrritationEffect;

	// Token: 0x04003102 RID: 12546
	public StateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.BoolParameter isIrritated;

	// Token: 0x04003103 RID: 12547
	public StateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.Signal reactFinished;

	// Token: 0x04003104 RID: 12548
	public GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.State normal;

	// Token: 0x04003105 RID: 12549
	public GasLiquidExposureMonitor.IrritatedStates irritated;

	// Token: 0x02001A4F RID: 6735
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A50 RID: 6736
	public class TUNING
	{
		// Token: 0x04007F4A RID: 32586
		public const float MINOR_IRRITATION_THRESHOLD = 8f;

		// Token: 0x04007F4B RID: 32587
		public const float MAJOR_IRRITATION_THRESHOLD = 15f;

		// Token: 0x04007F4C RID: 32588
		public const float MAX_EXPOSURE = 30f;

		// Token: 0x04007F4D RID: 32589
		public const float GAS_UNITS = 1f;

		// Token: 0x04007F4E RID: 32590
		public const float LIQUID_UNITS = 1000f;

		// Token: 0x04007F4F RID: 32591
		public const float REDUCE_EXPOSURE_RATE_FAST = -1f;

		// Token: 0x04007F50 RID: 32592
		public const float REDUCE_EXPOSURE_RATE_SLOW = -0.25f;

		// Token: 0x04007F51 RID: 32593
		public const float NO_CHANGE = 0f;

		// Token: 0x04007F52 RID: 32594
		public const float SLOW_EXPOSURE_RATE = 0.5f;

		// Token: 0x04007F53 RID: 32595
		public const float NORMAL_EXPOSURE_RATE = 1f;

		// Token: 0x04007F54 RID: 32596
		public const float QUICK_EXPOSURE_RATE = 3f;

		// Token: 0x04007F55 RID: 32597
		public const float DEFAULT_MIN_TEMPERATURE = -13657.5f;

		// Token: 0x04007F56 RID: 32598
		public const float DEFAULT_MAX_TEMPERATURE = 27315f;

		// Token: 0x04007F57 RID: 32599
		public const float DEFAULT_LOW_RATE = 1f;

		// Token: 0x04007F58 RID: 32600
		public const float DEFAULT_HIGH_RATE = 2f;
	}

	// Token: 0x02001A51 RID: 6737
	public class IrritatedStates : GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.State
	{
		// Token: 0x04007F59 RID: 32601
		public GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.State irritated;

		// Token: 0x04007F5A RID: 32602
		public GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.State rubbingEyes;
	}

	// Token: 0x02001A52 RID: 6738
	public new class Instance : GameStateMachine<GasLiquidExposureMonitor, GasLiquidExposureMonitor.Instance, IStateMachineTarget, GasLiquidExposureMonitor.Def>.GameInstance
	{
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600A2FF RID: 41727 RVA: 0x003A269F File Offset: 0x003A089F
		public float minorIrritationThreshold
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x0600A300 RID: 41728 RVA: 0x003A26A6 File Offset: 0x003A08A6
		public Instance(IStateMachineTarget master, GasLiquidExposureMonitor.Def def)
			: base(master, def)
		{
			this.effects = master.GetComponent<Effects>();
		}

		// Token: 0x0600A301 RID: 41729 RVA: 0x003A26BC File Offset: 0x003A08BC
		public Reactable GetReactable()
		{
			Emote iritatedEyes = Db.Get().Emotes.Minion.IritatedEyes;
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "IrritatedEyes", Db.Get().ChoreTypes.Cough, 0f, 0f, float.PositiveInfinity, 0f);
			selfEmoteReactable.SetEmote(iritatedEyes);
			selfEmoteReactable.preventChoreInterruption = true;
			selfEmoteReactable.RegisterEmoteStepCallbacks("irritated_eyes", null, delegate(GameObject go)
			{
				base.sm.reactFinished.Trigger(this);
			});
			return selfEmoteReactable;
		}

		// Token: 0x0600A302 RID: 41730 RVA: 0x003A2748 File Offset: 0x003A0948
		public bool IsMinorIrritation()
		{
			return this.exposure >= 8f && this.exposure < 15f;
		}

		// Token: 0x0600A303 RID: 41731 RVA: 0x003A2766 File Offset: 0x003A0966
		public bool IsMajorIrritation()
		{
			return this.exposure >= 15f;
		}

		// Token: 0x0600A304 RID: 41732 RVA: 0x003A2778 File Offset: 0x003A0978
		public Element CurrentlyExposedToElement()
		{
			if (this.isInAirtightEnvironment)
			{
				return ElementLoader.GetElement(SimHashes.Oxygen.CreateTag());
			}
			int num = Grid.CellAbove(Grid.PosToCell(base.smi.gameObject));
			return Grid.Element[num];
		}

		// Token: 0x0600A305 RID: 41733 RVA: 0x003A27BA File Offset: 0x003A09BA
		public void ResetExposure()
		{
			this.exposure = 0f;
		}

		// Token: 0x04007F5B RID: 32603
		[Serialize]
		public float exposure;

		// Token: 0x04007F5C RID: 32604
		[Serialize]
		public float lastReactTime;

		// Token: 0x04007F5D RID: 32605
		[Serialize]
		public float exposureRate;

		// Token: 0x04007F5E RID: 32606
		public Effects effects;

		// Token: 0x04007F5F RID: 32607
		public bool isInAirtightEnvironment;

		// Token: 0x04007F60 RID: 32608
		public bool isImmuneToIrritability;
	}
}
