using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000D88 RID: 3464
public class OptionsMenuScreen : KModalButtonMenu
{
	// Token: 0x06006BC9 RID: 27593 RVA: 0x0028ABF8 File Offset: 0x00288DF8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.keepMenuOpen = true;
		this.buttons = new List<KButtonMenu.ButtonInfo>
		{
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.GRAPHICS, global::Action.NumActions, new UnityAction(this.OnGraphicsOptions), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.AUDIO, global::Action.NumActions, new UnityAction(this.OnAudioOptions), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.GAME, global::Action.NumActions, new UnityAction(this.OnGameOptions), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.METRICS, global::Action.NumActions, new UnityAction(this.OnMetrics), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.FEEDBACK, global::Action.NumActions, new UnityAction(this.OnFeedback), null, null),
			new KButtonMenu.ButtonInfo(UI.FRONTEND.OPTIONS_SCREEN.CREDITS, global::Action.NumActions, new UnityAction(this.OnCredits), null, null)
		};
		this.closeButton.onClick += this.Deactivate;
		this.backButton.onClick += this.Deactivate;
	}

	// Token: 0x06006BCA RID: 27594 RVA: 0x0028AD3D File Offset: 0x00288F3D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.title.SetText(UI.FRONTEND.OPTIONS_SCREEN.TITLE);
		this.backButton.transform.SetAsLastSibling();
	}

	// Token: 0x06006BCB RID: 27595 RVA: 0x0028AD6C File Offset: 0x00288F6C
	protected override void OnActivate()
	{
		base.OnActivate();
		foreach (GameObject gameObject in this.buttonObjects)
		{
		}
	}

	// Token: 0x06006BCC RID: 27596 RVA: 0x0028AD98 File Offset: 0x00288F98
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06006BCD RID: 27597 RVA: 0x0028ADBA File Offset: 0x00288FBA
	private void OnGraphicsOptions()
	{
		base.ActivateChildScreen(this.graphicsOptionsScreenPrefab.gameObject);
	}

	// Token: 0x06006BCE RID: 27598 RVA: 0x0028ADCE File Offset: 0x00288FCE
	private void OnAudioOptions()
	{
		base.ActivateChildScreen(this.audioOptionsScreenPrefab.gameObject);
	}

	// Token: 0x06006BCF RID: 27599 RVA: 0x0028ADE2 File Offset: 0x00288FE2
	private void OnGameOptions()
	{
		base.ActivateChildScreen(this.gameOptionsScreenPrefab.gameObject);
	}

	// Token: 0x06006BD0 RID: 27600 RVA: 0x0028ADF6 File Offset: 0x00288FF6
	private void OnMetrics()
	{
		base.ActivateChildScreen(this.metricsScreenPrefab.gameObject);
	}

	// Token: 0x06006BD1 RID: 27601 RVA: 0x0028AE0A File Offset: 0x0028900A
	public void ShowMetricsScreen()
	{
		base.ActivateChildScreen(this.metricsScreenPrefab.gameObject);
	}

	// Token: 0x06006BD2 RID: 27602 RVA: 0x0028AE1E File Offset: 0x0028901E
	private void OnFeedback()
	{
		base.ActivateChildScreen(this.feedbackScreenPrefab.gameObject);
	}

	// Token: 0x06006BD3 RID: 27603 RVA: 0x0028AE32 File Offset: 0x00289032
	private void OnCredits()
	{
		base.ActivateChildScreen(this.creditsScreenPrefab.gameObject);
	}

	// Token: 0x06006BD4 RID: 27604 RVA: 0x0028AE46 File Offset: 0x00289046
	private void Update()
	{
		global::Debug.developerConsoleVisible = false;
	}

	// Token: 0x0400496D RID: 18797
	[SerializeField]
	private GameOptionsScreen gameOptionsScreenPrefab;

	// Token: 0x0400496E RID: 18798
	[SerializeField]
	private AudioOptionsScreen audioOptionsScreenPrefab;

	// Token: 0x0400496F RID: 18799
	[SerializeField]
	private GraphicsOptionsScreen graphicsOptionsScreenPrefab;

	// Token: 0x04004970 RID: 18800
	[SerializeField]
	private CreditsScreen creditsScreenPrefab;

	// Token: 0x04004971 RID: 18801
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04004972 RID: 18802
	[SerializeField]
	private MetricsOptionsScreen metricsScreenPrefab;

	// Token: 0x04004973 RID: 18803
	[SerializeField]
	private FeedbackScreen feedbackScreenPrefab;

	// Token: 0x04004974 RID: 18804
	[SerializeField]
	private LocText title;

	// Token: 0x04004975 RID: 18805
	[SerializeField]
	private KButton backButton;
}
