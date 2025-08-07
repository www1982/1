using System;

// Token: 0x020006BF RID: 1727
public class BionicUpgrade_Skill : GameStateMachine<BionicUpgrade_Skill, BionicUpgrade_Skill.Instance, IStateMachineTarget, BionicUpgrade_Skill.Def>
{
	// Token: 0x06002A6F RID: 10863 RVA: 0x000F5B0C File Offset: 0x000F3D0C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.root;
		this.root.Enter(new StateMachine<BionicUpgrade_Skill, BionicUpgrade_Skill.Instance, IStateMachineTarget, BionicUpgrade_Skill.Def>.State.Callback(BionicUpgrade_Skill.EnableEffect)).Exit(new StateMachine<BionicUpgrade_Skill, BionicUpgrade_Skill.Instance, IStateMachineTarget, BionicUpgrade_Skill.Def>.State.Callback(BionicUpgrade_Skill.DisableEffect));
	}

	// Token: 0x06002A70 RID: 10864 RVA: 0x000F5B46 File Offset: 0x000F3D46
	public static void EnableEffect(BionicUpgrade_Skill.Instance smi)
	{
		smi.ApplySkill();
	}

	// Token: 0x06002A71 RID: 10865 RVA: 0x000F5B4E File Offset: 0x000F3D4E
	public static void DisableEffect(BionicUpgrade_Skill.Instance smi)
	{
		smi.RemoveSkill();
	}

	// Token: 0x02001541 RID: 5441
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006F21 RID: 28449
		public string SKILL_ID;
	}

	// Token: 0x02001542 RID: 5442
	public new class Instance : GameStateMachine<BionicUpgrade_Skill, BionicUpgrade_Skill.Instance, IStateMachineTarget, BionicUpgrade_Skill.Def>.GameInstance
	{
		// Token: 0x06009081 RID: 36993 RVA: 0x00360315 File Offset: 0x0035E515
		public Instance(IStateMachineTarget master, BionicUpgrade_Skill.Def def)
			: base(master, def)
		{
			this.resume = base.GetComponent<MinionResume>();
		}

		// Token: 0x06009082 RID: 36994 RVA: 0x0036032B File Offset: 0x0035E52B
		public void ApplySkill()
		{
			this.resume.GrantSkill(base.def.SKILL_ID);
		}

		// Token: 0x06009083 RID: 36995 RVA: 0x00360343 File Offset: 0x0035E543
		public void RemoveSkill()
		{
			this.resume.UngrantSkill(base.def.SKILL_ID);
		}

		// Token: 0x04006F22 RID: 28450
		private MinionResume resume;
	}
}
