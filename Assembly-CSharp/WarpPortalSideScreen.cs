using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E48 RID: 3656
public class WarpPortalSideScreen : SideScreenContent
{
	// Token: 0x06007430 RID: 29744 RVA: 0x002C19C8 File Offset: 0x002BFBC8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.buttonLabel.SetText(UI.UISIDESCREENS.WARPPORTALSIDESCREEN.BUTTON);
		this.cancelButtonLabel.SetText(UI.UISIDESCREENS.WARPPORTALSIDESCREEN.CANCELBUTTON);
		this.button.onClick += this.OnButtonClick;
		this.cancelButton.onClick += this.OnCancelClick;
		this.Refresh(null);
	}

	// Token: 0x06007431 RID: 29745 RVA: 0x002C1A3A File Offset: 0x002BFC3A
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<WarpPortal>() != null;
	}

	// Token: 0x06007432 RID: 29746 RVA: 0x002C1A48 File Offset: 0x002BFC48
	public override void SetTarget(GameObject target)
	{
		WarpPortal component = target.GetComponent<WarpPortal>();
		if (component == null)
		{
			global::Debug.LogError("Target doesn't have a WarpPortal associated with it.");
			return;
		}
		this.target = component;
		target.GetComponent<Assignable>().OnAssign += new Action<IAssignableIdentity>(this.Refresh);
		this.Refresh(null);
	}

	// Token: 0x06007433 RID: 29747 RVA: 0x002C1A98 File Offset: 0x002BFC98
	private void Update()
	{
		if (this.progressBar.activeSelf)
		{
			RectTransform rectTransform = this.progressBar.GetComponentsInChildren<Image>()[1].rectTransform;
			float num = this.target.rechargeProgress / 3000f;
			rectTransform.sizeDelta = new Vector2(rectTransform.transform.parent.GetComponent<LayoutElement>().minWidth * num, 24f);
			this.progressLabel.text = GameUtil.GetFormattedPercent(num * 100f, GameUtil.TimeSlice.None);
		}
	}

	// Token: 0x06007434 RID: 29748 RVA: 0x002C1B14 File Offset: 0x002BFD14
	private void OnButtonClick()
	{
		if (this.target.ReadyToWarp)
		{
			this.target.StartWarpSequence();
			this.Refresh(null);
		}
	}

	// Token: 0x06007435 RID: 29749 RVA: 0x002C1B35 File Offset: 0x002BFD35
	private void OnCancelClick()
	{
		this.target.CancelAssignment();
		this.Refresh(null);
	}

	// Token: 0x06007436 RID: 29750 RVA: 0x002C1B4C File Offset: 0x002BFD4C
	private void Refresh(object data = null)
	{
		this.progressBar.SetActive(false);
		this.cancelButton.gameObject.SetActive(false);
		if (!(this.target != null))
		{
			this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.IDLE;
			this.button.gameObject.SetActive(false);
			return;
		}
		if (this.target.ReadyToWarp)
		{
			this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.WAITING;
			this.button.gameObject.SetActive(true);
			this.cancelButton.gameObject.SetActive(true);
			return;
		}
		if (this.target.IsConsumed)
		{
			this.button.gameObject.SetActive(false);
			this.progressBar.SetActive(true);
			this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.CONSUMED;
			return;
		}
		if (this.target.IsWorking)
		{
			this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.UNDERWAY;
			this.button.gameObject.SetActive(false);
			this.cancelButton.gameObject.SetActive(true);
			return;
		}
		this.label.text = UI.UISIDESCREENS.WARPPORTALSIDESCREEN.IDLE;
		this.button.gameObject.SetActive(false);
	}

	// Token: 0x04004FF8 RID: 20472
	[SerializeField]
	private LocText label;

	// Token: 0x04004FF9 RID: 20473
	[SerializeField]
	private KButton button;

	// Token: 0x04004FFA RID: 20474
	[SerializeField]
	private LocText buttonLabel;

	// Token: 0x04004FFB RID: 20475
	[SerializeField]
	private KButton cancelButton;

	// Token: 0x04004FFC RID: 20476
	[SerializeField]
	private LocText cancelButtonLabel;

	// Token: 0x04004FFD RID: 20477
	[SerializeField]
	private WarpPortal target;

	// Token: 0x04004FFE RID: 20478
	[SerializeField]
	private GameObject contents;

	// Token: 0x04004FFF RID: 20479
	[SerializeField]
	private GameObject progressBar;

	// Token: 0x04005000 RID: 20480
	[SerializeField]
	private LocText progressLabel;
}
