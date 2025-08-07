using System;
using Database;
using UnityEngine;

// Token: 0x0200068E RID: 1678
public class BalloonFX : GameStateMachine<BalloonFX, BalloonFX.Instance>
{
	// Token: 0x06002917 RID: 10519 RVA: 0x000EEB40 File Offset: 0x000ECD40
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.Target(this.fx);
		this.root.Exit("DestroyFX", delegate(BalloonFX.Instance smi)
		{
			smi.DestroyFX();
		});
	}

	// Token: 0x04001832 RID: 6194
	public StateMachine<BalloonFX, BalloonFX.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x04001833 RID: 6195
	public KAnimFile defaultAnim = Assets.GetAnim("balloon_anim_kanim");

	// Token: 0x04001834 RID: 6196
	private KAnimFile defaultBalloon = Assets.GetAnim("balloon_basic_red_kanim");

	// Token: 0x04001835 RID: 6197
	private const string defaultAnimName = "balloon_anim_kanim";

	// Token: 0x04001836 RID: 6198
	private const string balloonAnimName = "balloon_basic_red_kanim";

	// Token: 0x04001837 RID: 6199
	private const string TARGET_SYMBOL_TO_OVERRIDE = "body";

	// Token: 0x04001838 RID: 6200
	private const int TARGET_OVERRIDE_PRIORITY = 0;

	// Token: 0x02001509 RID: 5385
	public new class Instance : GameStateMachine<BalloonFX, BalloonFX.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008FC0 RID: 36800 RVA: 0x0035E2D4 File Offset: 0x0035C4D4
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.balloonAnimController = FXHelpers.CreateEffectOverride(new string[] { "balloon_anim_kanim", "balloon_basic_red_kanim" }, master.gameObject.transform.GetPosition() + new Vector3(0f, 0.3f, 1f), master.transform, true, Grid.SceneLayer.Creatures, false);
			base.sm.fx.Set(this.balloonAnimController.gameObject, base.smi, false);
			this.balloonAnimController.defaultAnim = "idle_default";
			master.GetComponent<KBatchedAnimController>().GetSynchronizer().Add(this.balloonAnimController.GetComponent<KBatchedAnimController>());
		}

		// Token: 0x06008FC1 RID: 36801 RVA: 0x0035E38C File Offset: 0x0035C58C
		public void SetBalloonSymbolOverride(BalloonOverrideSymbol balloonOverride)
		{
			KAnimFile kanimFile = (balloonOverride.animFile.IsSome() ? balloonOverride.animFile.Unwrap() : base.smi.sm.defaultBalloon);
			this.balloonAnimController.SwapAnims(new KAnimFile[]
			{
				base.smi.sm.defaultAnim,
				kanimFile
			});
			SymbolOverrideController component = this.balloonAnimController.GetComponent<SymbolOverrideController>();
			if (this.currentBodyOverrideSymbol.IsSome())
			{
				component.RemoveSymbolOverride("body", 0);
			}
			if (balloonOverride.symbol.IsNone())
			{
				if (this.currentBodyOverrideSymbol.IsSome())
				{
					component.AddSymbolOverride("body", base.smi.sm.defaultAnim.GetData().build.GetSymbol("body"), 0);
				}
				this.balloonAnimController.SetBatchGroupOverride(HashedString.Invalid);
			}
			else
			{
				component.AddSymbolOverride("body", balloonOverride.symbol.Unwrap(), 0);
				this.balloonAnimController.SetBatchGroupOverride(kanimFile.batchTag);
			}
			this.currentBodyOverrideSymbol = balloonOverride;
		}

		// Token: 0x06008FC2 RID: 36802 RVA: 0x0035E4BC File Offset: 0x0035C6BC
		public void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
		}

		// Token: 0x04006E98 RID: 28312
		private KBatchedAnimController balloonAnimController;

		// Token: 0x04006E99 RID: 28313
		private Option<BalloonOverrideSymbol> currentBodyOverrideSymbol;
	}
}
