using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C3A RID: 3130
public class StoryMessageScreen : KScreen
{
	// Token: 0x170006E8 RID: 1768
	// (set) Token: 0x06005F3C RID: 24380 RVA: 0x00230403 File Offset: 0x0022E603
	public string title
	{
		set
		{
			this.titleLabel.SetText(value);
		}
	}

	// Token: 0x170006E9 RID: 1769
	// (set) Token: 0x06005F3D RID: 24381 RVA: 0x00230411 File Offset: 0x0022E611
	public string body
	{
		set
		{
			this.bodyLabel.SetText(value);
		}
	}

	// Token: 0x06005F3E RID: 24382 RVA: 0x0023041F File Offset: 0x0022E61F
	public override float GetSortKey()
	{
		return 8f;
	}

	// Token: 0x06005F3F RID: 24383 RVA: 0x00230426 File Offset: 0x0022E626
	protected override void OnSpawn()
	{
		base.OnSpawn();
		StoryMessageScreen.HideInterface(true);
		CameraController.Instance.FadeOut(0.5f, 1f, null);
	}

	// Token: 0x06005F40 RID: 24384 RVA: 0x00230449 File Offset: 0x0022E649
	private IEnumerator ExpandPanel()
	{
		this.content.gameObject.SetActive(true);
		yield return SequenceUtil.WaitForSecondsRealtime(0.25f);
		float height = 0f;
		while (height < 299f)
		{
			height = Mathf.Lerp(this.dialog.rectTransform().sizeDelta.y, 300f, Time.unscaledDeltaTime * 15f);
			this.dialog.rectTransform().sizeDelta = new Vector2(this.dialog.rectTransform().sizeDelta.x, height);
			yield return 0;
		}
		CameraController.Instance.FadeOut(0.5f, 1f, null);
		yield return null;
		yield break;
	}

	// Token: 0x06005F41 RID: 24385 RVA: 0x00230458 File Offset: 0x0022E658
	private IEnumerator CollapsePanel()
	{
		float height = 300f;
		while (height > 0f)
		{
			height = Mathf.Lerp(this.dialog.rectTransform().sizeDelta.y, -1f, Time.unscaledDeltaTime * 15f);
			this.dialog.rectTransform().sizeDelta = new Vector2(this.dialog.rectTransform().sizeDelta.x, height);
			yield return 0;
		}
		this.content.gameObject.SetActive(false);
		if (this.OnClose != null)
		{
			this.OnClose();
			this.OnClose = null;
		}
		this.Deactivate();
		yield return null;
		yield break;
	}

	// Token: 0x06005F42 RID: 24386 RVA: 0x00230468 File Offset: 0x0022E668
	public static void HideInterface(bool hide)
	{
		SelectTool.Instance.Select(null, true);
		NotificationScreen.Instance.Show(!hide);
		OverlayMenu.Instance.Show(!hide);
		if (PlanScreen.Instance != null)
		{
			PlanScreen.Instance.Show(!hide);
		}
		if (BuildMenu.Instance != null)
		{
			BuildMenu.Instance.Show(!hide);
		}
		ManagementMenu.Instance.Show(!hide);
		ToolMenu.Instance.Show(!hide);
		ToolMenu.Instance.PriorityScreen.Show(!hide);
		ColonyDiagnosticScreen.Instance.Show(!hide);
		PinnedResourcesPanel.Instance.Show(!hide);
		TopLeftControlScreen.Instance.Show(!hide);
		if (WorldSelector.Instance != null)
		{
			WorldSelector.Instance.Show(!hide);
		}
		global::DateTime.Instance.Show(!hide);
		if (BuildWatermark.Instance != null)
		{
			BuildWatermark.Instance.Show(!hide);
		}
		PopFXManager.Instance.Show(!hide);
	}

	// Token: 0x06005F43 RID: 24387 RVA: 0x00230580 File Offset: 0x0022E780
	public void Update()
	{
		if (!this.startFade)
		{
			return;
		}
		Color color = this.bg.color;
		color.a -= 0.01f;
		if (color.a <= 0f)
		{
			color.a = 0f;
		}
		this.bg.color = color;
	}

	// Token: 0x06005F44 RID: 24388 RVA: 0x002305D8 File Offset: 0x0022E7D8
	protected override void OnActivate()
	{
		base.OnActivate();
		SelectTool.Instance.Select(null, false);
		this.button.onClick += delegate
		{
			base.StartCoroutine(this.CollapsePanel());
		};
		this.dialog.GetComponent<KScreen>().Show(false);
		this.startFade = false;
		CameraController.Instance.DisableUserCameraControl = true;
		KFMOD.PlayUISound(this.dialogSound);
		this.dialog.GetComponent<KScreen>().Activate();
		this.dialog.GetComponent<KScreen>().SetShouldFadeIn(true);
		this.dialog.GetComponent<KScreen>().Show(true);
		MusicManager.instance.PlaySong("Music_Victory_01_Message", false);
		base.StartCoroutine(this.ExpandPanel());
	}

	// Token: 0x06005F45 RID: 24389 RVA: 0x0023068C File Offset: 0x0022E88C
	protected override void OnDeactivate()
	{
		base.IsActive();
		base.OnDeactivate();
		MusicManager.instance.StopSong("Music_Victory_01_Message", true, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		if (this.restoreInterfaceOnClose)
		{
			CameraController.Instance.DisableUserCameraControl = false;
			CameraController.Instance.FadeIn(0f, 1f, null);
			StoryMessageScreen.HideInterface(false);
		}
	}

	// Token: 0x06005F46 RID: 24390 RVA: 0x002306E5 File Offset: 0x0022E8E5
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape))
		{
			base.StartCoroutine(this.CollapsePanel());
		}
		e.Consumed = true;
	}

	// Token: 0x06005F47 RID: 24391 RVA: 0x00230704 File Offset: 0x0022E904
	public override void OnKeyUp(KButtonEvent e)
	{
		e.Consumed = true;
	}

	// Token: 0x04003F7F RID: 16255
	private const float ALPHA_SPEED = 0.01f;

	// Token: 0x04003F80 RID: 16256
	[SerializeField]
	private Image bg;

	// Token: 0x04003F81 RID: 16257
	[SerializeField]
	private GameObject dialog;

	// Token: 0x04003F82 RID: 16258
	[SerializeField]
	private KButton button;

	// Token: 0x04003F83 RID: 16259
	[SerializeField]
	private EventReference dialogSound;

	// Token: 0x04003F84 RID: 16260
	[SerializeField]
	private LocText titleLabel;

	// Token: 0x04003F85 RID: 16261
	[SerializeField]
	private LocText bodyLabel;

	// Token: 0x04003F86 RID: 16262
	private const float expandedHeight = 300f;

	// Token: 0x04003F87 RID: 16263
	[SerializeField]
	private GameObject content;

	// Token: 0x04003F88 RID: 16264
	public bool restoreInterfaceOnClose = true;

	// Token: 0x04003F89 RID: 16265
	public global::System.Action OnClose;

	// Token: 0x04003F8A RID: 16266
	private bool startFade;
}
