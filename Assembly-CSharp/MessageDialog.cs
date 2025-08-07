using System;

// Token: 0x02000D53 RID: 3411
public abstract class MessageDialog : KMonoBehaviour
{
	// Token: 0x17000780 RID: 1920
	// (get) Token: 0x060069C9 RID: 27081 RVA: 0x0027FD4C File Offset: 0x0027DF4C
	public virtual bool CanDontShowAgain
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060069CA RID: 27082
	public abstract bool CanDisplay(Message message);

	// Token: 0x060069CB RID: 27083
	public abstract void SetMessage(Message message);

	// Token: 0x060069CC RID: 27084
	public abstract void OnClickAction();

	// Token: 0x060069CD RID: 27085 RVA: 0x0027FD4F File Offset: 0x0027DF4F
	public virtual void OnDontShowAgain()
	{
	}
}
