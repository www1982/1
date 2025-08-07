using System;
using Klei.AI;
using STRINGS;
using TUNING;

// Token: 0x0200093E RID: 2366
[SkipSaveFileSerialization]
public class GlowStick : StateMachineComponent<GlowStick.StatesInstance>
{
	// Token: 0x06004368 RID: 17256 RVA: 0x00184CA5 File Offset: 0x00182EA5
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001925 RID: 6437
	public class StatesInstance : GameStateMachine<GlowStick.States, GlowStick.StatesInstance, GlowStick, object>.GameInstance
	{
		// Token: 0x06009E7F RID: 40575 RVA: 0x00396EF0 File Offset: 0x003950F0
		public StatesInstance(GlowStick master)
			: base(master)
		{
			this._radiationEmitter.emitRads = 100f;
			this._radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
			this._radiationEmitter.emitRate = 0.5f;
			this._radiationEmitter.emitRadiusX = 3;
			this._radiationEmitter.emitRadiusY = 3;
			this.radiationResistance = new AttributeModifier(Db.Get().Attributes.RadiationResistance.Id, TRAITS.GLOWSTICK_RADIATION_RESISTANCE, DUPLICANTS.TRAITS.GLOWSTICK.NAME, false, false, true);
			this.luminescenceModifier = new AttributeModifier(Db.Get().Attributes.Luminescence.Id, TRAITS.GLOWSTICK_LUX_VALUE, DUPLICANTS.TRAITS.GLOWSTICK.NAME, false, false, true);
		}

		// Token: 0x04007B6E RID: 31598
		[MyCmpAdd]
		private RadiationEmitter _radiationEmitter;

		// Token: 0x04007B6F RID: 31599
		public AttributeModifier radiationResistance;

		// Token: 0x04007B70 RID: 31600
		public AttributeModifier luminescenceModifier;
	}

	// Token: 0x02001926 RID: 6438
	public class States : GameStateMachine<GlowStick.States, GlowStick.StatesInstance, GlowStick>
	{
		// Token: 0x06009E80 RID: 40576 RVA: 0x00396FAC File Offset: 0x003951AC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleComponent<RadiationEmitter>(false).ToggleAttributeModifier("Radiation Resistance", (GlowStick.StatesInstance smi) => smi.radiationResistance, null).ToggleAttributeModifier("Luminescence Modifier", (GlowStick.StatesInstance smi) => smi.luminescenceModifier, null);
		}
	}
}
