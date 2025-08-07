using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000E86 RID: 3718
[AddComponentMenu("KMonoBehaviour/scripts/MultiToggle")]
public class MultiToggle : KMonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	// Token: 0x17000818 RID: 2072
	// (get) Token: 0x06007683 RID: 30339 RVA: 0x002D57FD File Offset: 0x002D39FD
	public int CurrentState
	{
		get
		{
			return this.state;
		}
	}

	// Token: 0x06007684 RID: 30340 RVA: 0x002D5805 File Offset: 0x002D3A05
	public void NextState()
	{
		this.ChangeState((this.state + 1) % this.states.Length);
	}

	// Token: 0x06007685 RID: 30341 RVA: 0x002D581E File Offset: 0x002D3A1E
	protected virtual void Update()
	{
		if (this.clickHeldDown)
		{
			this.totalHeldTime += Time.unscaledDeltaTime;
			if (this.totalHeldTime > this.heldTimeThreshold && this.onHold != null)
			{
				this.onHold();
			}
		}
	}

	// Token: 0x06007686 RID: 30342 RVA: 0x002D585B File Offset: 0x002D3A5B
	protected override void OnDisable()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			this.RefreshHoverColor();
			this.pointerOver = false;
			this.StopHolding();
		}
	}

	// Token: 0x06007687 RID: 30343 RVA: 0x002D587D File Offset: 0x002D3A7D
	public void ChangeState(int new_state_index, bool forceRefreshState)
	{
		if (forceRefreshState)
		{
			this.stateDirty = true;
		}
		this.ChangeState(new_state_index);
	}

	// Token: 0x06007688 RID: 30344 RVA: 0x002D5890 File Offset: 0x002D3A90
	public void ChangeState(int new_state_index)
	{
		if (!this.stateDirty && new_state_index == this.state)
		{
			return;
		}
		this.stateDirty = false;
		this.state = new_state_index;
		try
		{
			this.toggle_image.sprite = this.states[new_state_index].sprite;
			this.toggle_image.color = this.states[new_state_index].color;
			if (this.states[new_state_index].use_rect_margins)
			{
				this.toggle_image.rectTransform().sizeDelta = this.states[new_state_index].rect_margins;
			}
		}
		catch
		{
			string text = base.gameObject.name;
			Transform transform = base.transform;
			while (transform.parent != null)
			{
				text = text.Insert(0, transform.name + ">");
				transform = transform.parent;
			}
			global::Debug.LogError("Multi Toggle state index out of range: " + text + " idx:" + new_state_index.ToString(), base.gameObject);
		}
		foreach (StatePresentationSetting statePresentationSetting in this.states[this.state].additional_display_settings)
		{
			if (!(statePresentationSetting.image_target == null))
			{
				statePresentationSetting.image_target.sprite = statePresentationSetting.sprite;
				statePresentationSetting.image_target.color = statePresentationSetting.color;
			}
		}
		this.RefreshHoverColor();
	}

	// Token: 0x06007689 RID: 30345 RVA: 0x002D5A0C File Offset: 0x002D3C0C
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		if (!this.allowRightClick && eventData.button == PointerEventData.InputButton.Right)
		{
			return;
		}
		if (this.states.Length - 1 < this.state)
		{
			global::Debug.LogWarning("Multi toggle has too few / no states");
		}
		bool flag = false;
		if (this.onDoubleClick != null && eventData.clickCount == 2)
		{
			flag = this.onDoubleClick();
		}
		if (this.onClick != null && !flag)
		{
			this.onClick();
		}
		this.RefreshHoverColor();
	}

	// Token: 0x0600768A RID: 30346 RVA: 0x002D5A84 File Offset: 0x002D3C84
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.pointerOver = true;
		if (!KInputManager.isFocused)
		{
			return;
		}
		KInputManager.SetUserActive();
		if (this.states.Length == 0)
		{
			return;
		}
		if (this.states[this.state].use_color_on_hover && this.states[this.state].color_on_hover != this.states[this.state].color)
		{
			this.toggle_image.color = this.states[this.state].color_on_hover;
		}
		if (this.states[this.state].use_rect_margins)
		{
			this.toggle_image.rectTransform().sizeDelta = this.states[this.state].rect_margins;
		}
		foreach (StatePresentationSetting statePresentationSetting in this.states[this.state].additional_display_settings)
		{
			if (!(statePresentationSetting.image_target == null) && statePresentationSetting.use_color_on_hover)
			{
				statePresentationSetting.image_target.color = statePresentationSetting.color_on_hover;
			}
		}
		if (this.onEnter != null)
		{
			this.onEnter();
		}
	}

	// Token: 0x0600768B RID: 30347 RVA: 0x002D5BC0 File Offset: 0x002D3DC0
	protected void RefreshHoverColor()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (this.pointerOver)
			{
				if (this.states[this.state].use_color_on_hover && this.states[this.state].color_on_hover != this.states[this.state].color)
				{
					this.toggle_image.color = this.states[this.state].color_on_hover;
				}
				foreach (StatePresentationSetting statePresentationSetting in this.states[this.state].additional_display_settings)
				{
					if (!(statePresentationSetting.image_target == null) && !(statePresentationSetting.image_target == null) && statePresentationSetting.use_color_on_hover)
					{
						statePresentationSetting.image_target.color = statePresentationSetting.color_on_hover;
					}
				}
			}
			return;
		}
		if (this.states.Length == 0)
		{
			return;
		}
		if (this.states[this.state].use_color_on_hover && this.states[this.state].color_on_hover != this.states[this.state].color)
		{
			this.toggle_image.color = this.states[this.state].color;
		}
		foreach (StatePresentationSetting statePresentationSetting2 in this.states[this.state].additional_display_settings)
		{
			if (!(statePresentationSetting2.image_target == null) && statePresentationSetting2.use_color_on_hover)
			{
				statePresentationSetting2.image_target.color = statePresentationSetting2.color;
			}
		}
	}

	// Token: 0x0600768C RID: 30348 RVA: 0x002D5D84 File Offset: 0x002D3F84
	public void OnPointerExit(PointerEventData eventData)
	{
		this.pointerOver = false;
		if (!KInputManager.isFocused)
		{
			return;
		}
		KInputManager.SetUserActive();
		if (this.states.Length == 0)
		{
			return;
		}
		if (this.states[this.state].use_color_on_hover && this.states[this.state].color_on_hover != this.states[this.state].color)
		{
			this.toggle_image.color = this.states[this.state].color;
		}
		if (this.states[this.state].use_rect_margins)
		{
			this.toggle_image.rectTransform().sizeDelta = this.states[this.state].rect_margins;
		}
		foreach (StatePresentationSetting statePresentationSetting in this.states[this.state].additional_display_settings)
		{
			if (!(statePresentationSetting.image_target == null) && statePresentationSetting.use_color_on_hover)
			{
				statePresentationSetting.image_target.color = statePresentationSetting.color;
			}
		}
		if (this.onExit != null)
		{
			this.onExit();
		}
	}

	// Token: 0x0600768D RID: 30349 RVA: 0x002D5EC0 File Offset: 0x002D40C0
	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if (!this.allowRightClick && eventData.button == PointerEventData.InputButton.Right)
		{
			return;
		}
		this.clickHeldDown = true;
		if (this.play_sound_on_click)
		{
			ToggleState toggleState = this.states[this.state];
			string on_click_override_sound_path = toggleState.on_click_override_sound_path;
			bool has_sound_parameter = toggleState.has_sound_parameter;
			if (on_click_override_sound_path == "")
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click", false));
				return;
			}
			if (on_click_override_sound_path != "" && has_sound_parameter)
			{
				KFMOD.PlayUISoundWithParameter(GlobalAssets.GetSound("General_Item_Click", false), toggleState.sound_parameter_name, toggleState.sound_parameter_value);
				KFMOD.PlayUISoundWithParameter(GlobalAssets.GetSound(on_click_override_sound_path, false), toggleState.sound_parameter_name, toggleState.sound_parameter_value);
				return;
			}
			KFMOD.PlayUISound(GlobalAssets.GetSound(on_click_override_sound_path, false));
		}
	}

	// Token: 0x0600768E RID: 30350 RVA: 0x002D5F7F File Offset: 0x002D417F
	public virtual void OnPointerUp(PointerEventData eventData)
	{
		if (!this.allowRightClick && eventData.button == PointerEventData.InputButton.Right)
		{
			return;
		}
		this.StopHolding();
	}

	// Token: 0x0600768F RID: 30351 RVA: 0x002D5F9C File Offset: 0x002D419C
	private void StopHolding()
	{
		if (this.clickHeldDown)
		{
			if (this.play_sound_on_release && this.states[this.state].on_release_override_sound_path != "")
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound(this.states[this.state].on_release_override_sound_path, false));
			}
			this.clickHeldDown = false;
			if (this.onStopHold != null)
			{
				this.onStopHold();
			}
		}
		this.totalHeldTime = 0f;
	}

	// Token: 0x04005265 RID: 21093
	[Header("Settings")]
	[SerializeField]
	public ToggleState[] states;

	// Token: 0x04005266 RID: 21094
	public bool play_sound_on_click = true;

	// Token: 0x04005267 RID: 21095
	public bool play_sound_on_release;

	// Token: 0x04005268 RID: 21096
	public Image toggle_image;

	// Token: 0x04005269 RID: 21097
	protected int state;

	// Token: 0x0400526A RID: 21098
	public global::System.Action onClick;

	// Token: 0x0400526B RID: 21099
	private bool stateDirty = true;

	// Token: 0x0400526C RID: 21100
	public Func<bool> onDoubleClick;

	// Token: 0x0400526D RID: 21101
	public global::System.Action onEnter;

	// Token: 0x0400526E RID: 21102
	public global::System.Action onExit;

	// Token: 0x0400526F RID: 21103
	public global::System.Action onHold;

	// Token: 0x04005270 RID: 21104
	public global::System.Action onStopHold;

	// Token: 0x04005271 RID: 21105
	public bool allowRightClick = true;

	// Token: 0x04005272 RID: 21106
	protected bool clickHeldDown;

	// Token: 0x04005273 RID: 21107
	protected float totalHeldTime;

	// Token: 0x04005274 RID: 21108
	protected float heldTimeThreshold = 0.4f;

	// Token: 0x04005275 RID: 21109
	private bool pointerOver;
}
