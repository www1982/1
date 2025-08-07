using System;

// Token: 0x02000645 RID: 1605
public class ReactableTransitionLayer : TransitionDriver.InterruptOverrideLayer
{
	// Token: 0x060026AC RID: 9900 RVA: 0x000DC57D File Offset: 0x000DA77D
	public ReactableTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x000DC586 File Offset: 0x000DA786
	protected override bool IsOverrideComplete()
	{
		return !this.reactionMonitor.IsReacting() && base.IsOverrideComplete();
	}

	// Token: 0x060026AE RID: 9902 RVA: 0x000DC5A0 File Offset: 0x000DA7A0
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		if (this.reactionMonitor == null)
		{
			this.reactionMonitor = navigator.GetSMI<ReactionMonitor.Instance>();
		}
		this.reactionMonitor.PollForReactables(transition);
		if (this.reactionMonitor.IsReacting())
		{
			base.BeginTransition(navigator, transition);
			transition.start = this.originalTransition.start;
			transition.end = this.originalTransition.end;
		}
	}

	// Token: 0x04001696 RID: 5782
	private ReactionMonitor.Instance reactionMonitor;
}
