using System;
using Klei.AI;

// Token: 0x020009D7 RID: 2519
public class BionicWaterDamageMonitor : GameStateMachine<BionicWaterDamageMonitor, BionicWaterDamageMonitor.Instance, IStateMachineTarget, BionicWaterDamageMonitor.Def>
{
	// Token: 0x060049D3 RID: 18899 RVA: 0x001ABA24 File Offset: 0x001A9C24
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.safe;
		this.safe.Transition(this.suffering, new StateMachine<BionicWaterDamageMonitor, BionicWaterDamageMonitor.Instance, IStateMachineTarget, BionicWaterDamageMonitor.Def>.Transition.ConditionCallback(BionicWaterDamageMonitor.IsSuffering), UpdateRate.SIM_200ms);
		this.suffering.Transition(this.safe, GameStateMachine<BionicWaterDamageMonitor, BionicWaterDamageMonitor.Instance, IStateMachineTarget, BionicWaterDamageMonitor.Def>.Not(new StateMachine<BionicWaterDamageMonitor, BionicWaterDamageMonitor.Instance, IStateMachineTarget, BionicWaterDamageMonitor.Def>.Transition.ConditionCallback(BionicWaterDamageMonitor.IsSuffering)), UpdateRate.SIM_200ms).ToggleEffect("BionicWaterStress").ToggleReactable(new Func<BionicWaterDamageMonitor.Instance, Reactable>(BionicWaterDamageMonitor.ZapReactable));
	}

	// Token: 0x060049D4 RID: 18900 RVA: 0x001ABA9E File Offset: 0x001A9C9E
	private static Reactable ZapReactable(BionicWaterDamageMonitor.Instance smi)
	{
		return smi.GetZapReactable();
	}

	// Token: 0x060049D5 RID: 18901 RVA: 0x001ABAA6 File Offset: 0x001A9CA6
	private static bool IsSuffering(BionicWaterDamageMonitor.Instance smi)
	{
		return BionicWaterDamageMonitor.IsFloorWetWithIntolerantSubstance(smi);
	}

	// Token: 0x060049D6 RID: 18902 RVA: 0x001ABAB0 File Offset: 0x001A9CB0
	private static bool IsFloorWetWithIntolerantSubstance(BionicWaterDamageMonitor.Instance smi)
	{
		if (smi.master.gameObject.HasTag(GameTags.InTransitTube))
		{
			return false;
		}
		int num = Grid.PosToCell(smi);
		return Grid.IsValidCell(num) && Grid.Element[num].IsLiquid && !smi.kpid.HasTag(GameTags.HasAirtightSuit) && smi.def.IsElementIntolerable(Grid.Element[num].id);
	}

	// Token: 0x040030AC RID: 12460
	public const string EFFECT_NAME = "BionicWaterStress";

	// Token: 0x040030AD RID: 12461
	public GameStateMachine<BionicWaterDamageMonitor, BionicWaterDamageMonitor.Instance, IStateMachineTarget, BionicWaterDamageMonitor.Def>.State safe;

	// Token: 0x040030AE RID: 12462
	public GameStateMachine<BionicWaterDamageMonitor, BionicWaterDamageMonitor.Instance, IStateMachineTarget, BionicWaterDamageMonitor.Def>.State suffering;

	// Token: 0x02001A0F RID: 6671
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0600A20F RID: 41487 RVA: 0x003A06C0 File Offset: 0x0039E8C0
		public bool IsElementIntolerable(SimHashes element)
		{
			for (int i = 0; i < this.IntolerantToElements.Length; i++)
			{
				if (this.IntolerantToElements[i] == element)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04007E94 RID: 32404
		public readonly SimHashes[] IntolerantToElements = new SimHashes[]
		{
			SimHashes.Water,
			SimHashes.DirtyWater,
			SimHashes.SaltWater,
			SimHashes.Brine
		};

		// Token: 0x04007E95 RID: 32405
		public static float ZapInterval = 10f;
	}

	// Token: 0x02001A10 RID: 6672
	public new class Instance : GameStateMachine<BionicWaterDamageMonitor, BionicWaterDamageMonitor.Instance, IStateMachineTarget, BionicWaterDamageMonitor.Def>.GameInstance
	{
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600A212 RID: 41490 RVA: 0x003A0719 File Offset: 0x0039E919
		public bool IsAffectedByWaterDamage
		{
			get
			{
				return this.effects.HasEffect("BionicWaterStress");
			}
		}

		// Token: 0x0600A213 RID: 41491 RVA: 0x003A072B File Offset: 0x0039E92B
		public Instance(IStateMachineTarget master, BionicWaterDamageMonitor.Def def)
			: base(master, def)
		{
			this.effects = base.GetComponent<Effects>();
		}

		// Token: 0x0600A214 RID: 41492 RVA: 0x003A0744 File Offset: 0x0039E944
		public Reactable GetZapReactable()
		{
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, Db.Get().Emotes.Minion.WaterDamage.Id, Db.Get().ChoreTypes.WaterDamageZap, 0f, BionicWaterDamageMonitor.Def.ZapInterval, float.PositiveInfinity, 0f);
			Emote waterDamage = Db.Get().Emotes.Minion.WaterDamage;
			selfEmoteReactable.SetEmote(waterDamage);
			selfEmoteReactable.preventChoreInterruption = true;
			return selfEmoteReactable;
		}

		// Token: 0x04007E96 RID: 32406
		public Effects effects;

		// Token: 0x04007E97 RID: 32407
		[MyCmpGet]
		public KPrefabID kpid;
	}
}
