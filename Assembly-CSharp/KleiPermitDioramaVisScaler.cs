using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000D0B RID: 3339
[ExecuteAlways]
public class KleiPermitDioramaVisScaler : UIBehaviour
{
	// Token: 0x06006706 RID: 26374 RVA: 0x0026EBE4 File Offset: 0x0026CDE4
	protected override void OnRectTransformDimensionsChange()
	{
		this.Layout();
	}

	// Token: 0x06006707 RID: 26375 RVA: 0x0026EBEC File Offset: 0x0026CDEC
	public void Layout()
	{
		KleiPermitDioramaVisScaler.Layout(this.root, this.scaleTarget, this.slot);
	}

	// Token: 0x06006708 RID: 26376 RVA: 0x0026EC08 File Offset: 0x0026CE08
	public static void Layout(RectTransform root, RectTransform scaleTarget, RectTransform slot)
	{
		float num = 2.125f;
		AspectRatioFitter aspectRatioFitter = slot.FindOrAddComponent<AspectRatioFitter>();
		aspectRatioFitter.aspectRatio = num;
		aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
		float num2 = 1700f;
		float num3 = Mathf.Max(0.1f, root.rect.width) / num2;
		float num4 = 800f;
		float num5 = Mathf.Max(0.1f, root.rect.height) / num4;
		float num6 = Mathf.Max(num3, num5);
		scaleTarget.localScale = Vector3.one * num6;
		scaleTarget.sizeDelta = new Vector2(1700f, 800f);
		scaleTarget.anchorMin = Vector2.one * 0.5f;
		scaleTarget.anchorMax = Vector2.one * 0.5f;
		scaleTarget.pivot = Vector2.one * 0.5f;
		scaleTarget.anchoredPosition = Vector2.zero;
	}

	// Token: 0x040046A7 RID: 18087
	public const float REFERENCE_WIDTH = 1700f;

	// Token: 0x040046A8 RID: 18088
	public const float REFERENCE_HEIGHT = 800f;

	// Token: 0x040046A9 RID: 18089
	[SerializeField]
	private RectTransform root;

	// Token: 0x040046AA RID: 18090
	[SerializeField]
	private RectTransform scaleTarget;

	// Token: 0x040046AB RID: 18091
	[SerializeField]
	private RectTransform slot;
}
