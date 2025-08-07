using System;
using Klei.AI;
using STRINGS;
using TUNING;

// Token: 0x02000A2B RID: 2603
[SkipSaveFileSerialization]
public class PrefersColder : StateMachineComponent<PrefersColder.StatesInstance>
{
	// Token: 0x06004B9C RID: 19356 RVA: 0x001B6ABC File Offset: 0x001B4CBC
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001AEC RID: 6892
	public class StatesInstance : GameStateMachine<PrefersColder.States, PrefersColder.StatesInstance, PrefersColder, object>.GameInstance
	{
		// Token: 0x0600A595 RID: 42389 RVA: 0x003A967A File Offset: 0x003A787A
		public StatesInstance(PrefersColder master)
			: base(master)
		{
		}
	}

	// Token: 0x02001AED RID: 6893
	public class States : GameStateMachine<PrefersColder.States, PrefersColder.StatesInstance, PrefersColder>
	{
		// Token: 0x0600A596 RID: 42390 RVA: 0x003A9683 File Offset: 0x003A7883
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleAttributeModifier(DUPLICANTS.TRAITS.NEEDS.PREFERSCOOLER.NAME, (PrefersColder.StatesInstance smi) => this.modifier, null);
		}

		// Token: 0x0400815F RID: 33119
		private AttributeModifier modifier = new AttributeModifier("ThermalConductivityBarrier", DUPLICANTSTATS.STANDARD.Temperature.Conductivity_Barrier_Modification.PUDGY, DUPLICANTS.TRAITS.NEEDS.PREFERSCOOLER.NAME, false, false, true);
	}
}
