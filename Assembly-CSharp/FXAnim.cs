using System;
using UnityEngine;

// Token: 0x02000690 RID: 1680
public class FXAnim : GameStateMachine<FXAnim, FXAnim.Instance>
{
	// Token: 0x0600291B RID: 10523 RVA: 0x000EECBC File Offset: 0x000ECEBC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		base.Target(this.fx);
		this.loop.Enter(delegate(FXAnim.Instance smi)
		{
			smi.Enter();
		}).EventTransition(GameHashes.AnimQueueComplete, this.restart, null).Exit("Post", delegate(FXAnim.Instance smi)
		{
			smi.Exit();
		});
		this.restart.GoTo(this.loop);
	}

	// Token: 0x04001840 RID: 6208
	public StateMachine<FXAnim, FXAnim.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x04001841 RID: 6209
	public GameStateMachine<FXAnim, FXAnim.Instance, IStateMachineTarget, object>.State loop;

	// Token: 0x04001842 RID: 6210
	public GameStateMachine<FXAnim, FXAnim.Instance, IStateMachineTarget, object>.State restart;

	// Token: 0x0200150D RID: 5389
	public new class Instance : GameStateMachine<FXAnim, FXAnim.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008FCC RID: 36812 RVA: 0x0035E5BC File Offset: 0x0035C7BC
		public Instance(IStateMachineTarget master, string kanim_file, string anim, KAnim.PlayMode mode, Vector3 offset, Color32 tint_colour)
			: base(master)
		{
			this.animController = FXHelpers.CreateEffect(kanim_file, base.smi.master.transform.GetPosition() + offset, base.smi.master.transform, false, Grid.SceneLayer.Front, false);
			this.animController.gameObject.Subscribe(-1061186183, new Action<object>(this.OnAnimQueueComplete));
			this.animController.TintColour = tint_colour;
			base.sm.fx.Set(this.animController.gameObject, base.smi, false);
			this.anim = anim;
			this.mode = mode;
		}

		// Token: 0x06008FCD RID: 36813 RVA: 0x0035E66D File Offset: 0x0035C86D
		public void Enter()
		{
			this.animController.Play(this.anim, this.mode, 1f, 0f);
		}

		// Token: 0x06008FCE RID: 36814 RVA: 0x0035E695 File Offset: 0x0035C895
		public void Exit()
		{
			this.DestroyFX();
		}

		// Token: 0x06008FCF RID: 36815 RVA: 0x0035E69D File Offset: 0x0035C89D
		private void OnAnimQueueComplete(object data)
		{
			this.DestroyFX();
		}

		// Token: 0x06008FD0 RID: 36816 RVA: 0x0035E6A5 File Offset: 0x0035C8A5
		private void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
		}

		// Token: 0x04006E9F RID: 28319
		private string anim;

		// Token: 0x04006EA0 RID: 28320
		private KAnim.PlayMode mode;

		// Token: 0x04006EA1 RID: 28321
		private KBatchedAnimController animController;
	}
}
