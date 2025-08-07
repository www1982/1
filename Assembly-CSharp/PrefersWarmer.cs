using System;
using Klei.AI;
using STRINGS;
using TUNING;

// Token: 0x02000A2C RID: 2604
[SkipSaveFileSerialization]
public class PrefersWarmer : StateMachineComponent<PrefersWarmer.StatesInstance>
{
	// Token: 0x06004B9E RID: 19358 RVA: 0x001B6AD1 File Offset: 0x001B4CD1
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001AEE RID: 6894
	public class StatesInstance : GameStateMachine<PrefersWarmer.States, PrefersWarmer.StatesInstance, PrefersWarmer, object>.GameInstance
	{
		// Token: 0x0600A599 RID: 42393 RVA: 0x003A96F1 File Offset: 0x003A78F1
		public StatesInstance(PrefersWarmer master)
			: base(master)
		{
		}
	}

	// Token: 0x02001AEF RID: 6895
	public class States : GameStateMachine<PrefersWarmer.States, PrefersWarmer.StatesInstance, PrefersWarmer>
	{
		// Token: 0x0600A59A RID: 42394 RVA: 0x003A96FA File Offset: 0x003A78FA
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleAttributeModifier(DUPLICANTS.TRAITS.NEEDS.PREFERSWARMER.NAME, (PrefersWarmer.StatesInstance smi) => this.modifier, null);
		}

		// Token: 0x04008160 RID: 33120
		private AttributeModifier modifier = new AttributeModifier("ThermalConductivityBarrier", DUPLICANTSTATS.STANDARD.Temperature.Conductivity_Barrier_Modification.SKINNY, DUPLICANTS.TRAITS.NEEDS.PREFERSWARMER.NAME, false, false, true);
	}
}
