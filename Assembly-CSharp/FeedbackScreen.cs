using System;
using Steamworks;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CCB RID: 3275
public class FeedbackScreen : KModalScreen
{
	// Token: 0x060064FA RID: 25850 RVA: 0x0025FBE0 File Offset: 0x0025DDE0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.title.SetText(UI.FRONTEND.FEEDBACK_SCREEN.TITLE);
		this.dismissButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.closeButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.bugForumsButton.onClick += delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/klei-bug-tracker/oni/");
		};
		this.suggestionForumsButton.onClick += delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/forums/forum/133-oxygen-not-included-suggestions-and-feedback/");
		};
		this.logsDirectoryButton.onClick += delegate
		{
			App.OpenWebURL(Util.LogsFolder());
		};
		this.saveFilesDirectoryButton.onClick += delegate
		{
			App.OpenWebURL(SaveLoader.GetSavePrefix());
		};
		if (SteamUtils.IsSteamRunningOnSteamDeck())
		{
			this.logsDirectoryButton.GetComponentInParent<VerticalLayoutGroup>().padding = new RectOffset(0, 0, 0, 0);
			this.saveFilesDirectoryButton.gameObject.SetActive(false);
			this.logsDirectoryButton.gameObject.SetActive(false);
		}
	}

	// Token: 0x040044FD RID: 17661
	public LocText title;

	// Token: 0x040044FE RID: 17662
	public KButton dismissButton;

	// Token: 0x040044FF RID: 17663
	public KButton closeButton;

	// Token: 0x04004500 RID: 17664
	public KButton bugForumsButton;

	// Token: 0x04004501 RID: 17665
	public KButton suggestionForumsButton;

	// Token: 0x04004502 RID: 17666
	public KButton logsDirectoryButton;

	// Token: 0x04004503 RID: 17667
	public KButton saveFilesDirectoryButton;
}
