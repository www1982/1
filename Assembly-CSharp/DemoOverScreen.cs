using System;

// Token: 0x02000CB3 RID: 3251
public class DemoOverScreen : KModalScreen
{
	// Token: 0x06006419 RID: 25625 RVA: 0x00259832 File Offset: 0x00257A32
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Init();
		PlayerController.Instance.ActivateTool(SelectTool.Instance);
		SelectTool.Instance.Select(null, false);
	}

	// Token: 0x0600641A RID: 25626 RVA: 0x0025985B File Offset: 0x00257A5B
	private void Init()
	{
		this.QuitButton.onClick += delegate
		{
			this.Quit();
		};
	}

	// Token: 0x0600641B RID: 25627 RVA: 0x00259874 File Offset: 0x00257A74
	private void Quit()
	{
		PauseScreen.TriggerQuitGame();
	}

	// Token: 0x04004430 RID: 17456
	public KButton QuitButton;
}
