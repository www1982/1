using System;

// Token: 0x02000CD2 RID: 3282
public class GameOverScreen : KModalScreen
{
	// Token: 0x0600652F RID: 25903 RVA: 0x00260D06 File Offset: 0x0025EF06
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Init();
	}

	// Token: 0x06006530 RID: 25904 RVA: 0x00260D14 File Offset: 0x0025EF14
	private void Init()
	{
		if (this.QuitButton)
		{
			this.QuitButton.onClick += delegate
			{
				this.Quit();
			};
		}
		if (this.DismissButton)
		{
			this.DismissButton.onClick += delegate
			{
				this.Dismiss();
			};
		}
	}

	// Token: 0x06006531 RID: 25905 RVA: 0x00260D69 File Offset: 0x0025EF69
	private void Quit()
	{
		PauseScreen.TriggerQuitGame();
	}

	// Token: 0x06006532 RID: 25906 RVA: 0x00260D70 File Offset: 0x0025EF70
	private void Dismiss()
	{
		this.Show(false);
	}

	// Token: 0x04004521 RID: 17697
	public KButton DismissButton;

	// Token: 0x04004522 RID: 17698
	public KButton QuitButton;
}
