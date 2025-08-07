using System;

// Token: 0x020005BC RID: 1468
public class FossilSculptureLightMonitor : GameStateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>
{
	// Token: 0x060021DC RID: 8668 RVA: 0x000C3C4C File Offset: 0x000C1E4C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.noLit;
		this.noLit.TagTransition(GameTags.Operational, this.lit, false).EventHandler(GameHashes.WorkableCompleteWork, new StateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.State.Callback(FossilSculptureLightMonitor.HideLitEffect)).EventHandler(GameHashes.ArtableStateChanged, new StateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.State.Callback(FossilSculptureLightMonitor.HideLitEffect))
			.Enter(new StateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.State.Callback(FossilSculptureLightMonitor.HideLitEffect));
		this.lit.TagTransition(GameTags.Operational, this.noLit, true).EventHandler(GameHashes.WorkableCompleteWork, new StateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.State.Callback(FossilSculptureLightMonitor.ShowLitEffect)).EventHandler(GameHashes.ArtableStateChanged, new StateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.State.Callback(FossilSculptureLightMonitor.ShowLitEffect))
			.Enter(new StateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.State.Callback(FossilSculptureLightMonitor.ShowLitEffect));
	}

	// Token: 0x060021DD RID: 8669 RVA: 0x000C3D12 File Offset: 0x000C1F12
	public static void ShowLitEffect(FossilSculptureLightMonitor.Instance smi)
	{
		smi.SetAnimLitState(true);
	}

	// Token: 0x060021DE RID: 8670 RVA: 0x000C3D1B File Offset: 0x000C1F1B
	public static void HideLitEffect(FossilSculptureLightMonitor.Instance smi)
	{
		smi.SetAnimLitState(false);
	}

	// Token: 0x040013CB RID: 5067
	public const string LIT_LIGHT_BLOOM_SYMBOL_NAME = "statue_light_bloom";

	// Token: 0x040013CC RID: 5068
	public const string LIT_SHADING_SYMBOL_NAME = "shading_with_light";

	// Token: 0x040013CD RID: 5069
	public const string UNLIT_SHADING_SYMBOL_NAME = "shading_no_light";

	// Token: 0x040013CE RID: 5070
	public GameStateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.State noLit;

	// Token: 0x040013CF RID: 5071
	public GameStateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.State lit;

	// Token: 0x0200145C RID: 5212
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006C5F RID: 27743
		public bool usingBloom = true;
	}

	// Token: 0x0200145D RID: 5213
	public new class Instance : GameStateMachine<FossilSculptureLightMonitor, FossilSculptureLightMonitor.Instance, IStateMachineTarget, FossilSculptureLightMonitor.Def>.GameInstance
	{
		// Token: 0x06008D83 RID: 36227 RVA: 0x00358C2F File Offset: 0x00356E2F
		public Instance(IStateMachineTarget master, FossilSculptureLightMonitor.Def def)
			: base(master, def)
		{
			this.SetAnimLitState(false);
		}

		// Token: 0x06008D84 RID: 36228 RVA: 0x00358C40 File Offset: 0x00356E40
		public void SetAnimLitState(bool lit)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.SetSymbolVisiblity("statue_light_bloom", base.def.usingBloom && lit);
			component.SetSymbolVisiblity("shading_with_light", lit);
			component.SetSymbolVisiblity("shading_no_light", !lit);
		}
	}
}
