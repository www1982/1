using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020009FA RID: 2554
public class MilkProductionMonitor : GameStateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>
{
	// Token: 0x06004A77 RID: 19063 RVA: 0x001AFA44 File Offset: 0x001ADC44
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.producing;
		this.producing.DefaultState(this.producing.paused).EventHandler(GameHashes.CaloriesConsumed, delegate(MilkProductionMonitor.Instance smi, object data)
		{
			smi.OnCaloriesConsumed(data);
		});
		this.producing.paused.Transition(this.producing.full, new StateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.Transition.ConditionCallback(MilkProductionMonitor.IsFull), UpdateRate.SIM_1000ms).Transition(this.producing.producing, new StateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.Transition.ConditionCallback(MilkProductionMonitor.IsProducing), UpdateRate.SIM_1000ms);
		this.producing.producing.Transition(this.producing.full, new StateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.Transition.ConditionCallback(MilkProductionMonitor.IsFull), UpdateRate.SIM_1000ms).Transition(this.producing.paused, GameStateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.Not(new StateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.Transition.ConditionCallback(MilkProductionMonitor.IsProducing)), UpdateRate.SIM_1000ms);
		this.producing.full.ToggleStatusItem(Db.Get().CreatureStatusItems.MilkFull, null).Transition(this.producing.paused, GameStateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.Not(new StateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.Transition.ConditionCallback(MilkProductionMonitor.IsFull)), UpdateRate.SIM_1000ms).Enter(delegate(MilkProductionMonitor.Instance smi)
		{
			smi.gameObject.AddTag(GameTags.Creatures.RequiresMilking);
		});
	}

	// Token: 0x06004A78 RID: 19064 RVA: 0x001AFB98 File Offset: 0x001ADD98
	private static bool IsProducing(MilkProductionMonitor.Instance smi)
	{
		return !smi.IsFull && smi.IsUnderProductionEffect;
	}

	// Token: 0x06004A79 RID: 19065 RVA: 0x001AFBAA File Offset: 0x001ADDAA
	private static bool IsFull(MilkProductionMonitor.Instance smi)
	{
		return smi.IsFull;
	}

	// Token: 0x06004A7A RID: 19066 RVA: 0x001AFBB2 File Offset: 0x001ADDB2
	private static bool HasCapacity(MilkProductionMonitor.Instance smi)
	{
		return !smi.IsFull;
	}

	// Token: 0x04003138 RID: 12600
	public MilkProductionMonitor.ProducingStates producing;

	// Token: 0x02001A6E RID: 6766
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0600A38B RID: 41867 RVA: 0x003A42FC File Offset: 0x003A24FC
		public override void Configure(GameObject prefab)
		{
			prefab.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.MilkProduction.Id);
		}

		// Token: 0x04007FC1 RID: 32705
		public const SimHashes element = SimHashes.Milk;

		// Token: 0x04007FC2 RID: 32706
		public string effectId;

		// Token: 0x04007FC3 RID: 32707
		public float Capacity = 200f;

		// Token: 0x04007FC4 RID: 32708
		public float CaloriesPerCycle = 1000f;

		// Token: 0x04007FC5 RID: 32709
		public float HappinessRequired;
	}

	// Token: 0x02001A6F RID: 6767
	public class ProducingStates : GameStateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.State
	{
		// Token: 0x04007FC6 RID: 32710
		public GameStateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.State paused;

		// Token: 0x04007FC7 RID: 32711
		public GameStateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.State producing;

		// Token: 0x04007FC8 RID: 32712
		public GameStateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.State full;
	}

	// Token: 0x02001A70 RID: 6768
	public new class Instance : GameStateMachine<MilkProductionMonitor, MilkProductionMonitor.Instance, IStateMachineTarget, MilkProductionMonitor.Def>.GameInstance
	{
		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x0600A38E RID: 41870 RVA: 0x003A4348 File Offset: 0x003A2548
		public float MilkAmount
		{
			get
			{
				return this.MilkPercentage / 100f * base.def.Capacity;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x0600A38F RID: 41871 RVA: 0x003A4362 File Offset: 0x003A2562
		public float MilkPercentage
		{
			get
			{
				return this.milkAmountInstance.value;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600A390 RID: 41872 RVA: 0x003A436F File Offset: 0x003A256F
		public bool IsFull
		{
			get
			{
				return this.MilkPercentage >= this.milkAmountInstance.GetMax();
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600A391 RID: 41873 RVA: 0x003A4387 File Offset: 0x003A2587
		public bool IsUnderProductionEffect
		{
			get
			{
				return this.milkAmountInstance.GetDelta() > 0f;
			}
		}

		// Token: 0x0600A392 RID: 41874 RVA: 0x003A439B File Offset: 0x003A259B
		public Instance(IStateMachineTarget master, MilkProductionMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A393 RID: 41875 RVA: 0x003A43A8 File Offset: 0x003A25A8
		public override void StartSM()
		{
			this.milkAmountInstance = Db.Get().Amounts.MilkProduction.Lookup(base.gameObject);
			if (base.def.effectId != null)
			{
				this.effectInstance = this.effects.Get(base.smi.def.effectId);
			}
			base.StartSM();
		}

		// Token: 0x0600A394 RID: 41876 RVA: 0x003A440C File Offset: 0x003A260C
		public void OnCaloriesConsumed(object data)
		{
			if (base.def.effectId == null)
			{
				return;
			}
			CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = (CreatureCalorieMonitor.CaloriesConsumedEvent)data;
			this.effectInstance = this.effects.Get(base.smi.def.effectId);
			if (this.effectInstance == null)
			{
				this.effectInstance = this.effects.Add(base.smi.def.effectId, true);
			}
			this.effectInstance.timeRemaining += caloriesConsumedEvent.calories / base.smi.def.CaloriesPerCycle * 600f;
		}

		// Token: 0x0600A395 RID: 41877 RVA: 0x003A44A8 File Offset: 0x003A26A8
		private void RemoveMilk(float amount)
		{
			if (this.milkAmountInstance != null)
			{
				float num = Mathf.Min(this.milkAmountInstance.GetMin(), this.MilkPercentage - amount);
				this.milkAmountInstance.SetValue(num);
			}
		}

		// Token: 0x0600A396 RID: 41878 RVA: 0x003A44E4 File Offset: 0x003A26E4
		public PrimaryElement ExtractMilk(float desiredAmount)
		{
			float num = Mathf.Min(desiredAmount, this.MilkAmount);
			float temperature = base.GetComponent<PrimaryElement>().Temperature;
			if (num <= 0f)
			{
				return null;
			}
			this.RemoveMilk(num);
			PrimaryElement component = LiquidSourceManager.Instance.CreateChunk(SimHashes.Milk, num, temperature, 0, 0, base.transform.GetPosition()).GetComponent<PrimaryElement>();
			component.KeepZeroMassObject = false;
			return component;
		}

		// Token: 0x0600A397 RID: 41879 RVA: 0x003A4548 File Offset: 0x003A2748
		public PrimaryElement ExtractMilkIntoElementChunk(float desiredAmount, PrimaryElement elementChunk)
		{
			if (elementChunk == null || elementChunk.ElementID != SimHashes.Milk)
			{
				return null;
			}
			float num = Mathf.Min(desiredAmount, this.MilkAmount);
			float temperature = base.GetComponent<PrimaryElement>().Temperature;
			this.RemoveMilk(num);
			float mass = elementChunk.Mass;
			float finalTemperature = GameUtil.GetFinalTemperature(elementChunk.Temperature, mass, temperature, num);
			elementChunk.SetMassTemperature(mass + num, finalTemperature);
			return elementChunk;
		}

		// Token: 0x0600A398 RID: 41880 RVA: 0x003A45B0 File Offset: 0x003A27B0
		public PrimaryElement ExtractMilkIntoStorage(float desiredAmount, Storage storage)
		{
			float num = Mathf.Min(desiredAmount, this.MilkAmount);
			float temperature = base.GetComponent<PrimaryElement>().Temperature;
			this.RemoveMilk(num);
			return storage.AddLiquid(SimHashes.Milk, num, temperature, 0, 0, false, true);
		}

		// Token: 0x04007FC9 RID: 32713
		public Action<float> OnMilkAmountChanged;

		// Token: 0x04007FCA RID: 32714
		public AmountInstance milkAmountInstance;

		// Token: 0x04007FCB RID: 32715
		public EffectInstance effectInstance;

		// Token: 0x04007FCC RID: 32716
		[MyCmpGet]
		private Effects effects;
	}
}
