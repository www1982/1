using System;
using Database;
using KSerialization;
using TUNING;

// Token: 0x020008FC RID: 2300
public class EquippableBalloon : StateMachineComponent<EquippableBalloon.StatesInstance>
{
	// Token: 0x0600402A RID: 16426 RVA: 0x00168497 File Offset: 0x00166697
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.smi.transitionTime = GameClock.Instance.GetTime() + TRAITS.JOY_REACTIONS.JOY_REACTION_DURATION;
	}

	// Token: 0x0600402B RID: 16427 RVA: 0x001684BA File Offset: 0x001666BA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.ApplyBalloonOverrideToBalloonFx();
	}

	// Token: 0x0600402C RID: 16428 RVA: 0x001684D3 File Offset: 0x001666D3
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0600402D RID: 16429 RVA: 0x001684DB File Offset: 0x001666DB
	public void SetBalloonOverride(BalloonOverrideSymbol balloonOverride)
	{
		base.smi.facadeAnim = balloonOverride.animFileID;
		base.smi.symbolID = balloonOverride.animFileSymbolID;
		this.ApplyBalloonOverrideToBalloonFx();
	}

	// Token: 0x0600402E RID: 16430 RVA: 0x00168508 File Offset: 0x00166708
	public void ApplyBalloonOverrideToBalloonFx()
	{
		Equippable component = base.GetComponent<Equippable>();
		if (!component.IsNullOrDestroyed() && !component.assignee.IsNullOrDestroyed())
		{
			Ownables soleOwner = component.assignee.GetSoleOwner();
			if (soleOwner.IsNullOrDestroyed())
			{
				return;
			}
			BalloonFX.Instance smi = ((KMonoBehaviour)soleOwner.GetComponent<MinionAssignablesProxy>().target).GetSMI<BalloonFX.Instance>();
			if (!smi.IsNullOrDestroyed())
			{
				new BalloonOverrideSymbol(base.smi.facadeAnim, base.smi.symbolID).ApplyTo(smi);
			}
		}
	}

	// Token: 0x020018A1 RID: 6305
	public class StatesInstance : GameStateMachine<EquippableBalloon.States, EquippableBalloon.StatesInstance, EquippableBalloon, object>.GameInstance
	{
		// Token: 0x06009D36 RID: 40246 RVA: 0x00392569 File Offset: 0x00390769
		public StatesInstance(EquippableBalloon master)
			: base(master)
		{
		}

		// Token: 0x04007978 RID: 31096
		[Serialize]
		public float transitionTime;

		// Token: 0x04007979 RID: 31097
		[Serialize]
		public string facadeAnim;

		// Token: 0x0400797A RID: 31098
		[Serialize]
		public string symbolID;
	}

	// Token: 0x020018A2 RID: 6306
	public class States : GameStateMachine<EquippableBalloon.States, EquippableBalloon.StatesInstance, EquippableBalloon>
	{
		// Token: 0x06009D37 RID: 40247 RVA: 0x00392574 File Offset: 0x00390774
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.Transition(this.destroy, (EquippableBalloon.StatesInstance smi) => GameClock.Instance.GetTime() >= smi.transitionTime, UpdateRate.SIM_200ms);
			this.destroy.Enter(delegate(EquippableBalloon.StatesInstance smi)
			{
				smi.master.GetComponent<Equippable>().Unassign();
			});
		}

		// Token: 0x0400797B RID: 31099
		public GameStateMachine<EquippableBalloon.States, EquippableBalloon.StatesInstance, EquippableBalloon, object>.State destroy;
	}
}
