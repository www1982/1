using System;

// Token: 0x02000CAA RID: 3242
public class CustomGameSettingWidget : KMonoBehaviour
{
	// Token: 0x14000022 RID: 34
	// (add) Token: 0x060063B2 RID: 25522 RVA: 0x002570F4 File Offset: 0x002552F4
	// (remove) Token: 0x060063B3 RID: 25523 RVA: 0x0025712C File Offset: 0x0025532C
	public event Action<CustomGameSettingWidget> onSettingChanged;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x060063B4 RID: 25524 RVA: 0x00257164 File Offset: 0x00255364
	// (remove) Token: 0x060063B5 RID: 25525 RVA: 0x0025719C File Offset: 0x0025539C
	public event global::System.Action onRefresh;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x060063B6 RID: 25526 RVA: 0x002571D4 File Offset: 0x002553D4
	// (remove) Token: 0x060063B7 RID: 25527 RVA: 0x0025720C File Offset: 0x0025540C
	public event global::System.Action onDestroy;

	// Token: 0x060063B8 RID: 25528 RVA: 0x00257241 File Offset: 0x00255441
	public virtual void Refresh()
	{
		if (this.onRefresh != null)
		{
			this.onRefresh();
		}
	}

	// Token: 0x060063B9 RID: 25529 RVA: 0x00257256 File Offset: 0x00255456
	public void Notify()
	{
		if (this.onSettingChanged != null)
		{
			this.onSettingChanged(this);
		}
	}

	// Token: 0x060063BA RID: 25530 RVA: 0x0025726C File Offset: 0x0025546C
	protected override void OnForcedCleanUp()
	{
		base.OnForcedCleanUp();
		if (this.onDestroy != null)
		{
			this.onDestroy();
		}
	}
}
