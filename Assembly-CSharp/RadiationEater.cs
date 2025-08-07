using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000A84 RID: 2692
[SkipSaveFileSerialization]
public class RadiationEater : StateMachineComponent<RadiationEater.StatesInstance>
{
	// Token: 0x06004E29 RID: 20009 RVA: 0x001C4EFE File Offset: 0x001C30FE
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001B6A RID: 7018
	public class StatesInstance : GameStateMachine<RadiationEater.States, RadiationEater.StatesInstance, RadiationEater, object>.GameInstance
	{
		// Token: 0x0600A783 RID: 42883 RVA: 0x003B1226 File Offset: 0x003AF426
		public StatesInstance(RadiationEater master)
			: base(master)
		{
			this.radiationEating = new AttributeModifier(Db.Get().Attributes.RadiationRecovery.Id, TRAITS.RADIATION_EATER_RECOVERY, DUPLICANTS.TRAITS.RADIATIONEATER.NAME, false, false, true);
		}

		// Token: 0x0600A784 RID: 42884 RVA: 0x003B1260 File Offset: 0x003AF460
		public void OnEatRads(float radsEaten)
		{
			float num = Mathf.Abs(radsEaten) * TRAITS.RADS_TO_CALS;
			base.smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.Calories).ApplyDelta(num);
		}

		// Token: 0x040082E0 RID: 33504
		public AttributeModifier radiationEating;
	}

	// Token: 0x02001B6B RID: 7019
	public class States : GameStateMachine<RadiationEater.States, RadiationEater.StatesInstance, RadiationEater>
	{
		// Token: 0x0600A785 RID: 42885 RVA: 0x003B12AC File Offset: 0x003AF4AC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleAttributeModifier("Radiation Eating", (RadiationEater.StatesInstance smi) => smi.radiationEating, null).EventHandler(GameHashes.RadiationRecovery, delegate(RadiationEater.StatesInstance smi, object data)
			{
				float num = (float)data;
				smi.OnEatRads(num);
			});
		}
	}
}
