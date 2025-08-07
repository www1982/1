using System;
using UnityEngine;

// Token: 0x02000CF9 RID: 3321
public class KModalButtonMenu : KButtonMenu
{
	// Token: 0x06006650 RID: 26192 RVA: 0x0026A68D File Offset: 0x0026888D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.modalBackground = KModalScreen.MakeScreenModal(this);
	}

	// Token: 0x06006651 RID: 26193 RVA: 0x0026A6A1 File Offset: 0x002688A1
	protected override void OnCmpEnable()
	{
		KModalScreen.ResizeBackground(this.modalBackground);
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
	}

	// Token: 0x06006652 RID: 26194 RVA: 0x0026A6D4 File Offset: 0x002688D4
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.childDialog == null)
		{
			base.Trigger(476357528, null);
		}
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (global::System.Action)Delegate.Remove(instance.OnResize, new global::System.Action(this.OnResize));
	}

	// Token: 0x06006653 RID: 26195 RVA: 0x0026A727 File Offset: 0x00268927
	private void OnResize()
	{
		KModalScreen.ResizeBackground(this.modalBackground);
	}

	// Token: 0x06006654 RID: 26196 RVA: 0x0026A734 File Offset: 0x00268934
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x06006655 RID: 26197 RVA: 0x0026A737 File Offset: 0x00268937
	public override float GetSortKey()
	{
		return 100f;
	}

	// Token: 0x06006656 RID: 26198 RVA: 0x0026A740 File Offset: 0x00268940
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (SpeedControlScreen.Instance != null)
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
		if (CameraController.Instance != null)
		{
			CameraController.Instance.DisableUserCameraControl = show;
		}
	}

	// Token: 0x06006657 RID: 26199 RVA: 0x0026A7AF File Offset: 0x002689AF
	public override void OnKeyDown(KButtonEvent e)
	{
		base.OnKeyDown(e);
		e.Consumed = true;
	}

	// Token: 0x06006658 RID: 26200 RVA: 0x0026A7BF File Offset: 0x002689BF
	public override void OnKeyUp(KButtonEvent e)
	{
		base.OnKeyUp(e);
		e.Consumed = true;
	}

	// Token: 0x06006659 RID: 26201 RVA: 0x0026A7CF File Offset: 0x002689CF
	public void SetBackgroundActive(bool active)
	{
	}

	// Token: 0x0600665A RID: 26202 RVA: 0x0026A7D4 File Offset: 0x002689D4
	protected GameObject ActivateChildScreen(GameObject screenPrefab)
	{
		GameObject gameObject = Util.KInstantiateUI(screenPrefab, base.transform.parent.gameObject, false);
		this.childDialog = gameObject;
		gameObject.Subscribe(476357528, new Action<object>(this.Unhide));
		this.Hide();
		return gameObject;
	}

	// Token: 0x0600665B RID: 26203 RVA: 0x0026A81F File Offset: 0x00268A1F
	private void Hide()
	{
		this.panelRoot.rectTransform().localScale = Vector3.zero;
	}

	// Token: 0x0600665C RID: 26204 RVA: 0x0026A836 File Offset: 0x00268A36
	private void Unhide(object data = null)
	{
		this.panelRoot.rectTransform().localScale = Vector3.one;
		this.childDialog.Unsubscribe(476357528, new Action<object>(this.Unhide));
		this.childDialog = null;
	}

	// Token: 0x04004624 RID: 17956
	private bool shown;

	// Token: 0x04004625 RID: 17957
	[SerializeField]
	private GameObject panelRoot;

	// Token: 0x04004626 RID: 17958
	private GameObject childDialog;

	// Token: 0x04004627 RID: 17959
	private RectTransform modalBackground;
}
