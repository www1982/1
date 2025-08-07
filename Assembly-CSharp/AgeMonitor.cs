using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200084E RID: 2126
public class AgeMonitor : GameStateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>
{
	// Token: 0x06003A6F RID: 14959 RVA: 0x001453F0 File Offset: 0x001435F0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.alive;
		this.alive.ToggleAttributeModifier("Aging", (AgeMonitor.Instance smi) => this.aging, null).Transition(this.time_to_die, new StateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.Transition.ConditionCallback(AgeMonitor.TimeToDie), UpdateRate.SIM_1000ms).Update(new Action<AgeMonitor.Instance, float>(AgeMonitor.UpdateOldStatusItem), UpdateRate.SIM_1000ms, false);
		this.time_to_die.Enter(new StateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.State.Callback(AgeMonitor.Die));
		this.aging = new AttributeModifier(Db.Get().Amounts.Age.deltaAttribute.Id, 0.0016666667f, CREATURES.MODIFIERS.AGE.NAME, false, false, true);
	}

	// Token: 0x06003A70 RID: 14960 RVA: 0x0014549C File Offset: 0x0014369C
	private static void Die(AgeMonitor.Instance smi)
	{
		smi.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Generic);
	}

	// Token: 0x06003A71 RID: 14961 RVA: 0x001454B8 File Offset: 0x001436B8
	private static bool TimeToDie(AgeMonitor.Instance smi)
	{
		return smi.age.value >= smi.age.GetMax();
	}

	// Token: 0x06003A72 RID: 14962 RVA: 0x001454D8 File Offset: 0x001436D8
	private static void UpdateOldStatusItem(AgeMonitor.Instance smi, float dt)
	{
		bool flag = smi.age.value > smi.age.GetMax() * 0.9f;
		smi.oldStatusGuid = smi.kselectable.ToggleStatusItem(Db.Get().CreatureStatusItems.Old, smi.oldStatusGuid, flag, smi);
	}

	// Token: 0x040023D6 RID: 9174
	public GameStateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.State alive;

	// Token: 0x040023D7 RID: 9175
	public GameStateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.State time_to_die;

	// Token: 0x040023D8 RID: 9176
	private AttributeModifier aging;

	// Token: 0x020017CB RID: 6091
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06009A73 RID: 39539 RVA: 0x0038A705 File Offset: 0x00388905
		public override void Configure(GameObject prefab)
		{
			prefab.AddOrGet<Modifiers>().initialAmounts.Add(Db.Get().Amounts.Age.Id);
		}

		// Token: 0x04007705 RID: 30469
		public float minAgePercentOnSpawn;

		// Token: 0x04007706 RID: 30470
		public float maxAgePercentOnSpawn = 0.75f;
	}

	// Token: 0x020017CC RID: 6092
	public new class Instance : GameStateMachine<AgeMonitor, AgeMonitor.Instance, IStateMachineTarget, AgeMonitor.Def>.GameInstance
	{
		// Token: 0x06009A75 RID: 39541 RVA: 0x0038A740 File Offset: 0x00388940
		public Instance(IStateMachineTarget master, AgeMonitor.Def def)
			: base(master, def)
		{
			this.age = Db.Get().Amounts.Age.Lookup(base.gameObject);
			base.Subscribe(1119167081, delegate(object data)
			{
				this.RandomizeAge();
			});
		}

		// Token: 0x06009A76 RID: 39542 RVA: 0x0038A78C File Offset: 0x0038898C
		public void RandomizeAge()
		{
			this.age.value = Mathf.Lerp(this.age.GetMax() * base.def.minAgePercentOnSpawn, this.age.GetMax() * base.def.maxAgePercentOnSpawn, global::UnityEngine.Random.value);
			AmountInstance amountInstance = Db.Get().Amounts.Fertility.Lookup(base.gameObject);
			if (amountInstance != null)
			{
				amountInstance.value = this.age.value / this.age.GetMax() * amountInstance.GetMax() * 1.75f;
				amountInstance.value = Mathf.Min(amountInstance.value, amountInstance.GetMax() * 0.9f);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06009A77 RID: 39543 RVA: 0x0038A841 File Offset: 0x00388A41
		public float CyclesUntilDeath
		{
			get
			{
				return this.age.GetMax() - this.age.value;
			}
		}

		// Token: 0x04007707 RID: 30471
		public AmountInstance age;

		// Token: 0x04007708 RID: 30472
		public Guid oldStatusGuid;

		// Token: 0x04007709 RID: 30473
		[MyCmpReq]
		public KSelectable kselectable;
	}
}
