using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D72 RID: 3442
public class MotdBox_ImageButtonLayoutElement : LayoutElement
{
	// Token: 0x06006AFA RID: 27386 RVA: 0x00286778 File Offset: 0x00284978
	private void UpdateState()
	{
		MotdBox_ImageButtonLayoutElement.Style style = this.style;
		if (style == MotdBox_ImageButtonLayoutElement.Style.WidthExpandsBasedOnHeight)
		{
			this.flexibleHeight = 1f;
			this.preferredHeight = -1f;
			this.minHeight = -1f;
			this.flexibleWidth = 0f;
			this.preferredWidth = this.rectTransform().sizeDelta.y * this.heightToWidthRatio;
			this.minWidth = this.preferredWidth;
			this.ignoreLayout = false;
			return;
		}
		if (style != MotdBox_ImageButtonLayoutElement.Style.HeightExpandsBasedOnWidth)
		{
			return;
		}
		this.flexibleWidth = 1f;
		this.preferredWidth = -1f;
		this.minWidth = -1f;
		this.flexibleHeight = 0f;
		this.preferredHeight = this.rectTransform().sizeDelta.x / this.heightToWidthRatio;
		this.minHeight = this.preferredHeight;
		this.ignoreLayout = false;
	}

	// Token: 0x06006AFB RID: 27387 RVA: 0x0028684D File Offset: 0x00284A4D
	protected override void OnTransformParentChanged()
	{
		this.UpdateState();
		base.OnTransformParentChanged();
	}

	// Token: 0x06006AFC RID: 27388 RVA: 0x0028685B File Offset: 0x00284A5B
	protected override void OnRectTransformDimensionsChange()
	{
		this.UpdateState();
		base.OnRectTransformDimensionsChange();
	}

	// Token: 0x040048E3 RID: 18659
	[SerializeField]
	private float heightToWidthRatio;

	// Token: 0x040048E4 RID: 18660
	[SerializeField]
	private MotdBox_ImageButtonLayoutElement.Style style;

	// Token: 0x02001F5B RID: 8027
	private enum Style
	{
		// Token: 0x04009087 RID: 36999
		WidthExpandsBasedOnHeight,
		// Token: 0x04009088 RID: 37000
		HeightExpandsBasedOnWidth
	}
}
