using System;
using UnityEngine;

// Token: 0x02000E1B RID: 3611
public class PlayerControlledToggleSideScreen : SideScreenContent, IRenderEveryTick
{
	// Token: 0x06007223 RID: 29219 RVA: 0x002B679C File Offset: 0x002B499C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.toggleButton.onClick += this.ClickToggle;
		this.togglePendingStatusItem = new StatusItem("PlayerControlledToggleSideScreen", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
	}

	// Token: 0x06007224 RID: 29220 RVA: 0x002B67EF File Offset: 0x002B49EF
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IPlayerControlledToggle>() != null;
	}

	// Token: 0x06007225 RID: 29221 RVA: 0x002B67FC File Offset: 0x002B49FC
	public void RenderEveryTick(float dt)
	{
		if (base.isActiveAndEnabled)
		{
			if (!this.keyDown && (Input.GetKeyDown(KeyCode.Return) & (Time.unscaledTime - this.lastKeyboardShortcutTime > 0.1f)))
			{
				if (SpeedControlScreen.Instance.IsPaused)
				{
					this.RequestToggle();
				}
				else
				{
					this.Toggle();
				}
				this.lastKeyboardShortcutTime = Time.unscaledTime;
				this.keyDown = true;
			}
			if (this.keyDown && Input.GetKeyUp(KeyCode.Return))
			{
				this.keyDown = false;
			}
		}
	}

	// Token: 0x06007226 RID: 29222 RVA: 0x002B687A File Offset: 0x002B4A7A
	private void ClickToggle()
	{
		if (SpeedControlScreen.Instance.IsPaused)
		{
			this.RequestToggle();
			return;
		}
		this.Toggle();
	}

	// Token: 0x06007227 RID: 29223 RVA: 0x002B6898 File Offset: 0x002B4A98
	private void RequestToggle()
	{
		this.target.ToggleRequested = !this.target.ToggleRequested;
		if (this.target.ToggleRequested && SpeedControlScreen.Instance.IsPaused)
		{
			this.target.GetSelectable().SetStatusItem(Db.Get().StatusItemCategories.Main, this.togglePendingStatusItem, this);
		}
		else
		{
			this.target.GetSelectable().SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
		}
		this.UpdateVisuals(this.target.ToggleRequested ? (!this.target.ToggledOn()) : this.target.ToggledOn(), true);
	}

	// Token: 0x06007228 RID: 29224 RVA: 0x002B6954 File Offset: 0x002B4B54
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IPlayerControlledToggle>();
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received is not an IPlayerControlledToggle");
			return;
		}
		this.UpdateVisuals(this.target.ToggleRequested ? (!this.target.ToggledOn()) : this.target.ToggledOn(), false);
		this.titleKey = this.target.SideScreenTitleKey;
	}

	// Token: 0x06007229 RID: 29225 RVA: 0x002B69D4 File Offset: 0x002B4BD4
	private void Toggle()
	{
		this.target.ToggledByPlayer();
		this.UpdateVisuals(this.target.ToggledOn(), true);
		this.target.ToggleRequested = false;
		this.target.GetSelectable().RemoveStatusItem(this.togglePendingStatusItem, false);
	}

	// Token: 0x0600722A RID: 29226 RVA: 0x002B6A24 File Offset: 0x002B4C24
	private void UpdateVisuals(bool state, bool smooth)
	{
		if (state != this.currentState)
		{
			if (smooth)
			{
				this.kbac.Play(state ? PlayerControlledToggleSideScreen.ON_ANIMS : PlayerControlledToggleSideScreen.OFF_ANIMS, KAnim.PlayMode.Once);
			}
			else
			{
				this.kbac.Play(state ? PlayerControlledToggleSideScreen.ON_ANIMS[1] : PlayerControlledToggleSideScreen.OFF_ANIMS[1], KAnim.PlayMode.Once, 1f, 0f);
			}
		}
		this.currentState = state;
	}

	// Token: 0x04004E8F RID: 20111
	public IPlayerControlledToggle target;

	// Token: 0x04004E90 RID: 20112
	public KButton toggleButton;

	// Token: 0x04004E91 RID: 20113
	protected static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on" };

	// Token: 0x04004E92 RID: 20114
	protected static readonly HashedString[] OFF_ANIMS = new HashedString[] { "off_pre", "off" };

	// Token: 0x04004E93 RID: 20115
	public float animScaleBase = 0.25f;

	// Token: 0x04004E94 RID: 20116
	private StatusItem togglePendingStatusItem;

	// Token: 0x04004E95 RID: 20117
	[SerializeField]
	private KBatchedAnimController kbac;

	// Token: 0x04004E96 RID: 20118
	private float lastKeyboardShortcutTime;

	// Token: 0x04004E97 RID: 20119
	private const float KEYBOARD_COOLDOWN = 0.1f;

	// Token: 0x04004E98 RID: 20120
	private bool keyDown;

	// Token: 0x04004E99 RID: 20121
	private bool currentState;
}
