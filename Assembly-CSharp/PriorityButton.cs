using System;
using UnityEngine;

// Token: 0x02000D9E RID: 3486
[AddComponentMenu("KMonoBehaviour/scripts/PriorityButton")]
public class PriorityButton : KMonoBehaviour
{
	// Token: 0x1700079E RID: 1950
	// (get) Token: 0x06006D1E RID: 27934 RVA: 0x00294C80 File Offset: 0x00292E80
	// (set) Token: 0x06006D1F RID: 27935 RVA: 0x00294C88 File Offset: 0x00292E88
	public PrioritySetting priority
	{
		get
		{
			return this._priority;
		}
		set
		{
			this._priority = value;
			if (this.its != null)
			{
				if (this.priority.priority_class == PriorityScreen.PriorityClass.high)
				{
					this.its.colorStyleSetting = this.highStyle;
				}
				else
				{
					this.its.colorStyleSetting = this.normalStyle;
				}
				this.its.RefreshColorStyle();
				this.its.ResetColor();
			}
		}
	}

	// Token: 0x06006D20 RID: 27936 RVA: 0x00294CF2 File Offset: 0x00292EF2
	protected override void OnPrefabInit()
	{
		this.toggle.onClick += this.OnClick;
	}

	// Token: 0x06006D21 RID: 27937 RVA: 0x00294D0B File Offset: 0x00292F0B
	private void OnClick()
	{
		if (this.playSelectionSound)
		{
			PriorityScreen.PlayPriorityConfirmSound(this.priority);
		}
		if (this.onClick != null)
		{
			this.onClick(this.priority);
		}
	}

	// Token: 0x04004A7A RID: 19066
	public KToggle toggle;

	// Token: 0x04004A7B RID: 19067
	public LocText text;

	// Token: 0x04004A7C RID: 19068
	public ToolTip tooltip;

	// Token: 0x04004A7D RID: 19069
	[MyCmpGet]
	private ImageToggleState its;

	// Token: 0x04004A7E RID: 19070
	public ColorStyleSetting normalStyle;

	// Token: 0x04004A7F RID: 19071
	public ColorStyleSetting highStyle;

	// Token: 0x04004A80 RID: 19072
	public bool playSelectionSound = true;

	// Token: 0x04004A81 RID: 19073
	public Action<PrioritySetting> onClick;

	// Token: 0x04004A82 RID: 19074
	private PrioritySetting _priority;
}
