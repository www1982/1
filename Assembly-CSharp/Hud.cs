using System;

// Token: 0x02000CE6 RID: 3302
public class Hud : KScreen
{
	// Token: 0x060065AA RID: 26026 RVA: 0x002649F1 File Offset: 0x00262BF1
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Help))
		{
			GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.ControlsScreen.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
		}
	}
}
