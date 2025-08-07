using System;
using UnityEngine;

// Token: 0x0200068F RID: 1679
public class BionicAttributeUseFx : GameStateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance>
{
	// Token: 0x06002919 RID: 10521 RVA: 0x000EEBC4 File Offset: 0x000ECDC4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre;
		base.Target(this.fx);
		this.root.OnSignal(this.wasProductive, this.productive, (BionicAttributeUseFx.Instance smi) => smi.GetCurrentState() != smi.sm.pst).OnSignal(this.destroyFX, this.pst);
		this.pre.PlayAnim("bionic_upgrade_active_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.idle);
		this.idle.PlayAnim("bionic_upgrade_active_loop", KAnim.PlayMode.Loop);
		this.productive.QueueAnim("bionic_upgrade_active_achievement", false, null).OnAnimQueueComplete(this.idle);
		this.pst.PlayAnim("bionic_upgrade_active_pst").EventHandler(GameHashes.AnimQueueComplete, delegate(BionicAttributeUseFx.Instance smi)
		{
			smi.DestroyFX();
		});
	}

	// Token: 0x04001839 RID: 6201
	public StateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance, IStateMachineTarget, object>.Signal wasProductive;

	// Token: 0x0400183A RID: 6202
	public StateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance, IStateMachineTarget, object>.Signal destroyFX;

	// Token: 0x0400183B RID: 6203
	public StateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x0400183C RID: 6204
	public GameStateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance, IStateMachineTarget, object>.State pre;

	// Token: 0x0400183D RID: 6205
	public GameStateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x0400183E RID: 6206
	public GameStateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance, IStateMachineTarget, object>.State productive;

	// Token: 0x0400183F RID: 6207
	public GameStateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance, IStateMachineTarget, object>.State pst;

	// Token: 0x0200150B RID: 5387
	public new class Instance : GameStateMachine<BionicAttributeUseFx, BionicAttributeUseFx.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008FC6 RID: 36806 RVA: 0x0035E4F8 File Offset: 0x0035C6F8
		public Instance(IStateMachineTarget master, Vector3 offset)
			: base(master)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("bionic_upgrade_active_fx_kanim", master.gameObject.transform.GetPosition() + offset, master.gameObject.transform, true, Grid.SceneLayer.FXFront, false);
			base.sm.fx.Set(kbatchedAnimController.gameObject, base.smi, false);
		}

		// Token: 0x06008FC7 RID: 36807 RVA: 0x0035E55A File Offset: 0x0035C75A
		public void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
			base.smi.StopSM("destroyed");
		}
	}
}
