using System;
using Klei.AI;

// Token: 0x0200080A RID: 2058
public class ChilledBones : GameStateMachine<ChilledBones, ChilledBones.Instance, IStateMachineTarget, ChilledBones.Def>
{
	// Token: 0x060037F9 RID: 14329 RVA: 0x001369CC File Offset: 0x00134BCC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.normal;
		this.normal.UpdateTransition(this.chilled, new Func<ChilledBones.Instance, float, bool>(this.IsChilling), UpdateRate.SIM_200ms, false);
		this.chilled.ToggleEffect("ChilledBones").UpdateTransition(this.normal, new Func<ChilledBones.Instance, float, bool>(this.IsNotChilling), UpdateRate.SIM_200ms, false);
	}

	// Token: 0x060037FA RID: 14330 RVA: 0x00136A32 File Offset: 0x00134C32
	public bool IsNotChilling(ChilledBones.Instance smi, float dt)
	{
		return !this.IsChilling(smi, dt);
	}

	// Token: 0x060037FB RID: 14331 RVA: 0x00136A3F File Offset: 0x00134C3F
	public bool IsChilling(ChilledBones.Instance smi, float dt)
	{
		return smi.IsChilled;
	}

	// Token: 0x04002210 RID: 8720
	public const string EFFECT_NAME = "ChilledBones";

	// Token: 0x04002211 RID: 8721
	public GameStateMachine<ChilledBones, ChilledBones.Instance, IStateMachineTarget, ChilledBones.Def>.State normal;

	// Token: 0x04002212 RID: 8722
	public GameStateMachine<ChilledBones, ChilledBones.Instance, IStateMachineTarget, ChilledBones.Def>.State chilled;

	// Token: 0x02001767 RID: 5991
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400758F RID: 30095
		public float THRESHOLD = -1f;
	}

	// Token: 0x02001768 RID: 5992
	public new class Instance : GameStateMachine<ChilledBones, ChilledBones.Instance, IStateMachineTarget, ChilledBones.Def>.GameInstance
	{
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060098E9 RID: 39145 RVA: 0x00382D1F File Offset: 0x00380F1F
		public float TemperatureTransferAttribute
		{
			get
			{
				return this.minionModifiers.GetAttributes().GetValue(this.bodyTemperatureTransferAttribute.Id) * 600f;
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060098EA RID: 39146 RVA: 0x00382D42 File Offset: 0x00380F42
		public bool IsChilled
		{
			get
			{
				return this.TemperatureTransferAttribute < base.def.THRESHOLD;
			}
		}

		// Token: 0x060098EB RID: 39147 RVA: 0x00382D57 File Offset: 0x00380F57
		public Instance(IStateMachineTarget master, ChilledBones.Def def)
			: base(master, def)
		{
			this.bodyTemperatureTransferAttribute = Db.Get().Attributes.TryGet("TemperatureDelta");
		}

		// Token: 0x04007590 RID: 30096
		[MyCmpGet]
		public MinionModifiers minionModifiers;

		// Token: 0x04007591 RID: 30097
		public Klei.AI.Attribute bodyTemperatureTransferAttribute;
	}
}
