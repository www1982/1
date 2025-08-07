using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000DC5 RID: 3525
public class SelectableTextStyler : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06006F31 RID: 28465 RVA: 0x002A5713 File Offset: 0x002A3913
	private void Start()
	{
		this.SetState(this.state, SelectableTextStyler.HoverState.Normal);
	}

	// Token: 0x06006F32 RID: 28466 RVA: 0x002A5722 File Offset: 0x002A3922
	private void SetState(SelectableTextStyler.State state, SelectableTextStyler.HoverState hover_state)
	{
		if (state == SelectableTextStyler.State.Normal)
		{
			if (hover_state != SelectableTextStyler.HoverState.Normal)
			{
				if (hover_state == SelectableTextStyler.HoverState.Hovered)
				{
					this.target.textStyleSetting = this.normalHovered;
				}
			}
			else
			{
				this.target.textStyleSetting = this.normalNormal;
			}
		}
		this.target.ApplySettings();
	}

	// Token: 0x06006F33 RID: 28467 RVA: 0x002A575F File Offset: 0x002A395F
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.SetState(this.state, SelectableTextStyler.HoverState.Hovered);
	}

	// Token: 0x06006F34 RID: 28468 RVA: 0x002A576E File Offset: 0x002A396E
	public void OnPointerExit(PointerEventData eventData)
	{
		this.SetState(this.state, SelectableTextStyler.HoverState.Normal);
	}

	// Token: 0x06006F35 RID: 28469 RVA: 0x002A577D File Offset: 0x002A397D
	public void OnPointerClick(PointerEventData eventData)
	{
		this.SetState(this.state, SelectableTextStyler.HoverState.Normal);
	}

	// Token: 0x04004C76 RID: 19574
	[SerializeField]
	private LocText target;

	// Token: 0x04004C77 RID: 19575
	[SerializeField]
	private SelectableTextStyler.State state;

	// Token: 0x04004C78 RID: 19576
	[SerializeField]
	private TextStyleSetting normalNormal;

	// Token: 0x04004C79 RID: 19577
	[SerializeField]
	private TextStyleSetting normalHovered;

	// Token: 0x02001FE7 RID: 8167
	public enum State
	{
		// Token: 0x0400928B RID: 37515
		Normal
	}

	// Token: 0x02001FE8 RID: 8168
	public enum HoverState
	{
		// Token: 0x0400928D RID: 37517
		Normal,
		// Token: 0x0400928E RID: 37518
		Hovered
	}
}
