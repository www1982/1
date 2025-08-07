using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CFA RID: 3322
public class KModalScreen : KScreen
{
	// Token: 0x0600665E RID: 26206 RVA: 0x0026A878 File Offset: 0x00268A78
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.backgroundRectTransform = KModalScreen.MakeScreenModal(this);
	}

	// Token: 0x0600665F RID: 26207 RVA: 0x0026A88C File Offset: 0x00268A8C
	public static RectTransform MakeScreenModal(KScreen screen)
	{
		screen.ConsumeMouseScroll = true;
		screen.activateOnSpawn = true;
		GameObject gameObject = new GameObject("background");
		gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
		gameObject.AddComponent<CanvasRenderer>();
		Image image = gameObject.AddComponent<Image>();
		image.color = new Color32(0, 0, 0, 160);
		image.raycastTarget = true;
		RectTransform component = gameObject.GetComponent<RectTransform>();
		component.SetParent(screen.transform);
		KModalScreen.ResizeBackground(component);
		return component;
	}

	// Token: 0x06006660 RID: 26208 RVA: 0x0026A900 File Offset: 0x00268B00
	public static void ResizeBackground(RectTransform rectTransform)
	{
		rectTransform.SetAsFirstSibling();
		rectTransform.SetLocalPosition(Vector3.zero);
		rectTransform.localScale = Vector3.one;
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.sizeDelta = new Vector2(0f, 0f);
	}

	// Token: 0x06006661 RID: 26209 RVA: 0x0026A96C File Offset: 0x00268B6C
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (CameraController.Instance != null)
		{
			CameraController.Instance.DisableUserCameraControl = true;
		}
		if (ScreenResize.Instance != null)
		{
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
		}
	}

	// Token: 0x06006662 RID: 26210 RVA: 0x0026A9CC File Offset: 0x00268BCC
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (CameraController.Instance != null)
		{
			CameraController.Instance.DisableUserCameraControl = false;
		}
		base.Trigger(476357528, null);
		if (ScreenResize.Instance != null)
		{
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (global::System.Action)Delegate.Remove(instance.OnResize, new global::System.Action(this.OnResize));
		}
	}

	// Token: 0x06006663 RID: 26211 RVA: 0x0026AA36 File Offset: 0x00268C36
	private void OnResize()
	{
		KModalScreen.ResizeBackground(this.backgroundRectTransform);
	}

	// Token: 0x06006664 RID: 26212 RVA: 0x0026AA43 File Offset: 0x00268C43
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x06006665 RID: 26213 RVA: 0x0026AA46 File Offset: 0x00268C46
	public override float GetSortKey()
	{
		return 100f;
	}

	// Token: 0x06006666 RID: 26214 RVA: 0x0026AA4D File Offset: 0x00268C4D
	protected override void OnActivate()
	{
		this.OnShow(true);
	}

	// Token: 0x06006667 RID: 26215 RVA: 0x0026AA56 File Offset: 0x00268C56
	protected override void OnDeactivate()
	{
		this.OnShow(false);
	}

	// Token: 0x06006668 RID: 26216 RVA: 0x0026AA60 File Offset: 0x00268C60
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (this.pause && SpeedControlScreen.Instance != null)
		{
			if (show && !this.shown)
			{
				SpeedControlScreen.Instance.Pause(false, false);
			}
			else if (!show && this.shown)
			{
				SpeedControlScreen.Instance.Unpause(false);
			}
			this.shown = show;
		}
	}

	// Token: 0x06006669 RID: 26217 RVA: 0x0026AAC0 File Offset: 0x00268CC0
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (Game.Instance != null && (e.TryConsume(global::Action.TogglePause) || e.TryConsume(global::Action.CycleSpeed)))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
		}
		if (!e.Consumed && (e.TryConsume(global::Action.Escape) || (e.TryConsume(global::Action.MouseRight) && this.canBackoutWithRightClick)))
		{
			this.Deactivate();
		}
		base.OnKeyDown(e);
		e.Consumed = true;
	}

	// Token: 0x0600666A RID: 26218 RVA: 0x0026AB3D File Offset: 0x00268D3D
	public override void OnKeyUp(KButtonEvent e)
	{
		base.OnKeyUp(e);
		e.Consumed = true;
	}

	// Token: 0x04004628 RID: 17960
	private bool shown;

	// Token: 0x04004629 RID: 17961
	public bool pause = true;

	// Token: 0x0400462A RID: 17962
	[Tooltip("Only used for main menu")]
	public bool canBackoutWithRightClick;

	// Token: 0x0400462B RID: 17963
	private RectTransform backgroundRectTransform;
}
