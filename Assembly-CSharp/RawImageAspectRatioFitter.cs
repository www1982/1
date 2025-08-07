using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DA4 RID: 3492
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class RawImageAspectRatioFitter : AspectRatioFitter
{
	// Token: 0x06006D67 RID: 28007 RVA: 0x00296944 File Offset: 0x00294B44
	private void UpdateAspectRatio()
	{
		if (this.targetImage != null && this.targetImage.texture != null)
		{
			base.aspectRatio = (float)this.targetImage.texture.width / (float)this.targetImage.texture.height;
			return;
		}
		base.aspectRatio = 1f;
	}

	// Token: 0x06006D68 RID: 28008 RVA: 0x002969A7 File Offset: 0x00294BA7
	protected override void OnTransformParentChanged()
	{
		this.UpdateAspectRatio();
		base.OnTransformParentChanged();
	}

	// Token: 0x06006D69 RID: 28009 RVA: 0x002969B5 File Offset: 0x00294BB5
	protected override void OnRectTransformDimensionsChange()
	{
		this.UpdateAspectRatio();
		base.OnRectTransformDimensionsChange();
	}

	// Token: 0x04004AB9 RID: 19129
	[SerializeField]
	private RawImage targetImage;
}
