using System;

// Token: 0x02000DB7 RID: 3511
public class SaveActive : KScreen
{
	// Token: 0x06006EB5 RID: 28341 RVA: 0x002A2B1A File Offset: 0x002A0D1A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.SetAutoSaveCallbacks(new Game.SavingPreCB(this.ActiveateSaveIndicator), new Game.SavingActiveCB(this.SetActiveSaveIndicator), new Game.SavingPostCB(this.DeactivateSaveIndicator));
	}

	// Token: 0x06006EB6 RID: 28342 RVA: 0x002A2B50 File Offset: 0x002A0D50
	private void DoCallBack(HashedString name)
	{
		this.controller.onAnimComplete -= this.DoCallBack;
		this.readyForSaveCallback();
		this.readyForSaveCallback = null;
	}

	// Token: 0x06006EB7 RID: 28343 RVA: 0x002A2B7B File Offset: 0x002A0D7B
	private void ActiveateSaveIndicator(Game.CansaveCB cb)
	{
		this.readyForSaveCallback = cb;
		this.controller.onAnimComplete += this.DoCallBack;
		this.controller.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06006EB8 RID: 28344 RVA: 0x002A2BBB File Offset: 0x002A0DBB
	private void SetActiveSaveIndicator()
	{
		this.controller.Play("working_loop", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06006EB9 RID: 28345 RVA: 0x002A2BDD File Offset: 0x002A0DDD
	private void DeactivateSaveIndicator()
	{
		this.controller.Play("working_pst", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06006EBA RID: 28346 RVA: 0x002A2BFF File Offset: 0x002A0DFF
	public override void OnKeyDown(KButtonEvent e)
	{
	}

	// Token: 0x04004C1D RID: 19485
	[MyCmpGet]
	private KBatchedAnimController controller;

	// Token: 0x04004C1E RID: 19486
	private Game.CansaveCB readyForSaveCallback;
}
