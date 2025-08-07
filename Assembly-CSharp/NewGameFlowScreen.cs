using System;

// Token: 0x02000D77 RID: 3447
public abstract class NewGameFlowScreen : KModalScreen
{
	// Token: 0x1400002B RID: 43
	// (add) Token: 0x06006B3B RID: 27451 RVA: 0x002884E8 File Offset: 0x002866E8
	// (remove) Token: 0x06006B3C RID: 27452 RVA: 0x00288520 File Offset: 0x00286720
	public event global::System.Action OnNavigateForward;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x06006B3D RID: 27453 RVA: 0x00288558 File Offset: 0x00286758
	// (remove) Token: 0x06006B3E RID: 27454 RVA: 0x00288590 File Offset: 0x00286790
	public event global::System.Action OnNavigateBackward;

	// Token: 0x06006B3F RID: 27455 RVA: 0x002885C5 File Offset: 0x002867C5
	protected void NavigateBackward()
	{
		this.OnNavigateBackward();
	}

	// Token: 0x06006B40 RID: 27456 RVA: 0x002885D2 File Offset: 0x002867D2
	protected void NavigateForward()
	{
		this.OnNavigateForward();
	}

	// Token: 0x06006B41 RID: 27457 RVA: 0x002885DF File Offset: 0x002867DF
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (e.TryConsume(global::Action.MouseRight))
		{
			this.NavigateBackward();
		}
		base.OnKeyDown(e);
	}
}
