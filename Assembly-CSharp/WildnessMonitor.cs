using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200088C RID: 2188
public class WildnessMonitor : GameStateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>
{
	// Token: 0x06003C35 RID: 15413 RVA: 0x0014D744 File Offset: 0x0014B944
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.tame;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.wild.Enter(new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State.Callback(WildnessMonitor.RefreshAmounts)).Enter(new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State.Callback(WildnessMonitor.HideDomesticationSymbol)).Transition(this.tame, (WildnessMonitor.Instance smi) => !WildnessMonitor.IsWild(smi), UpdateRate.SIM_1000ms)
			.ToggleEffect((WildnessMonitor.Instance smi) => smi.def.wildEffect)
			.ToggleTag(GameTags.Creatures.Wild);
		this.tame.Enter(new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State.Callback(WildnessMonitor.RefreshAmounts)).Enter(new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State.Callback(WildnessMonitor.ShowDomesticationSymbol)).Transition(this.wild, new StateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.Transition.ConditionCallback(WildnessMonitor.IsWild), UpdateRate.SIM_1000ms)
			.ToggleEffect((WildnessMonitor.Instance smi) => smi.def.tameEffect)
			.Enter(delegate(WildnessMonitor.Instance smi)
			{
				SaveGame.Instance.ColonyAchievementTracker.LogCritterTamed(smi.PrefabID());
			});
	}

	// Token: 0x06003C36 RID: 15414 RVA: 0x0014D86C File Offset: 0x0014BA6C
	private static void HideDomesticationSymbol(WildnessMonitor.Instance smi)
	{
		foreach (KAnimHashedString kanimHashedString in WildnessMonitor.DOMESTICATION_SYMBOLS)
		{
			smi.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(kanimHashedString, false);
		}
	}

	// Token: 0x06003C37 RID: 15415 RVA: 0x0014D8A4 File Offset: 0x0014BAA4
	private static void ShowDomesticationSymbol(WildnessMonitor.Instance smi)
	{
		foreach (KAnimHashedString kanimHashedString in WildnessMonitor.DOMESTICATION_SYMBOLS)
		{
			smi.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(kanimHashedString, true);
		}
	}

	// Token: 0x06003C38 RID: 15416 RVA: 0x0014D8DA File Offset: 0x0014BADA
	private static bool IsWild(WildnessMonitor.Instance smi)
	{
		return smi.wildness.value > 0f;
	}

	// Token: 0x06003C39 RID: 15417 RVA: 0x0014D8F0 File Offset: 0x0014BAF0
	private static void RefreshAmounts(WildnessMonitor.Instance smi)
	{
		bool flag = WildnessMonitor.IsWild(smi);
		smi.wildness.hide = !flag;
		AttributeInstance attributeInstance = Db.Get().CritterAttributes.Happiness.Lookup(smi.gameObject);
		if (attributeInstance != null)
		{
			attributeInstance.hide = flag;
		}
		AttributeInstance attributeInstance2 = Db.Get().CritterAttributes.Metabolism.Lookup(smi.gameObject);
		if (attributeInstance2 != null)
		{
			attributeInstance2.hide = flag;
		}
		AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(smi.gameObject);
		if (amountInstance != null)
		{
			amountInstance.hide = flag;
		}
		AmountInstance amountInstance2 = Db.Get().Amounts.Temperature.Lookup(smi.gameObject);
		if (amountInstance2 != null)
		{
			amountInstance2.hide = flag;
		}
		AmountInstance amountInstance3 = Db.Get().Amounts.Fertility.Lookup(smi.gameObject);
		if (amountInstance3 != null)
		{
			amountInstance3.hide = flag;
		}
		AmountInstance amountInstance4 = Db.Get().Amounts.MilkProduction.Lookup(smi.gameObject);
		if (amountInstance4 != null)
		{
			amountInstance4.hide = flag;
		}
		AmountInstance amountInstance5 = Db.Get().Amounts.Beckoning.Lookup(smi.gameObject);
		if (amountInstance5 != null)
		{
			amountInstance5.hide = flag;
		}
	}

	// Token: 0x040024EA RID: 9450
	public GameStateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State wild;

	// Token: 0x040024EB RID: 9451
	public GameStateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.State tame;

	// Token: 0x040024EC RID: 9452
	private static readonly KAnimHashedString[] DOMESTICATION_SYMBOLS = new KAnimHashedString[] { "tag", "snapto_tag" };

	// Token: 0x02001854 RID: 6228
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06009C34 RID: 39988 RVA: 0x003905CD File Offset: 0x0038E7CD
		public override void Configure(GameObject prefab)
		{
			prefab.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.Wildness.Id);
		}

		// Token: 0x0400789E RID: 30878
		public Effect wildEffect;

		// Token: 0x0400789F RID: 30879
		public Effect tameEffect;
	}

	// Token: 0x02001855 RID: 6229
	public new class Instance : GameStateMachine<WildnessMonitor, WildnessMonitor.Instance, IStateMachineTarget, WildnessMonitor.Def>.GameInstance
	{
		// Token: 0x06009C36 RID: 39990 RVA: 0x003905FB File Offset: 0x0038E7FB
		public Instance(IStateMachineTarget master, WildnessMonitor.Def def)
			: base(master, def)
		{
			this.wildness = Db.Get().Amounts.Wildness.Lookup(base.gameObject);
			this.wildness.value = this.wildness.GetMax();
		}

		// Token: 0x06009C37 RID: 39991 RVA: 0x0039063B File Offset: 0x0038E83B
		public bool IsWild()
		{
			return WildnessMonitor.IsWild(this);
		}

		// Token: 0x06009C38 RID: 39992 RVA: 0x00390644 File Offset: 0x0038E844
		[ContextMenu("Tame Critter")]
		public void DebugTame()
		{
			AmountInstance amountInstance = Db.Get().Amounts.Wildness.Lookup(base.gameObject);
			if (amountInstance != null)
			{
				amountInstance.value = 0f;
				base.smi.GoTo(base.sm.tame);
			}
		}

		// Token: 0x040078A0 RID: 30880
		public AmountInstance wildness;
	}
}
