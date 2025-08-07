using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CE9 RID: 3305
[ExecuteAlways]
[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
public class ImageAspectRatioFitter : AspectRatioFitter
{
	// Token: 0x060065B1 RID: 26033 RVA: 0x00264A3C File Offset: 0x00262C3C
	private void UpdateAspectRatio()
	{
		if (this.targetImage != null && this.targetImage.sprite != null)
		{
			base.aspectRatio = this.targetImage.sprite.rect.width / this.targetImage.sprite.rect.height;
			return;
		}
		base.aspectRatio = 1f;
	}

	// Token: 0x060065B2 RID: 26034 RVA: 0x00264AAD File Offset: 0x00262CAD
	protected override void OnTransformParentChanged()
	{
		this.UpdateAspectRatio();
		base.OnTransformParentChanged();
	}

	// Token: 0x060065B3 RID: 26035 RVA: 0x00264ABB File Offset: 0x00262CBB
	protected override void OnRectTransformDimensionsChange()
	{
		this.UpdateAspectRatio();
		base.OnRectTransformDimensionsChange();
	}

	// Token: 0x040045A9 RID: 17833
	[SerializeField]
	private Image targetImage;
}
