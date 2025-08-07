using System;
using UnityEngine;

// Token: 0x02000458 RID: 1112
public static class PresUtil
{
	// Token: 0x06001745 RID: 5957 RVA: 0x000824EC File Offset: 0x000806EC
	public static Promise MoveAndFade(RectTransform rect, Vector2 targetAnchoredPosition, float targetAlpha, float duration, Easing.EasingFn easing = null)
	{
		CanvasGroup canvasGroup = rect.FindOrAddComponent<CanvasGroup>();
		return rect.FindOrAddComponent<CoroutineRunner>().Run(Updater.Parallel(new Updater[]
		{
			Updater.Ease(delegate(float f)
			{
				canvasGroup.alpha = f;
			}, canvasGroup.alpha, targetAlpha, duration, easing, -1f),
			Updater.Ease(delegate(Vector2 v2)
			{
				rect.anchoredPosition = v2;
			}, rect.anchoredPosition, targetAnchoredPosition, duration, easing, -1f)
		}));
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x00082590 File Offset: 0x00080790
	public static Promise OffsetFromAndFade(RectTransform rect, Vector2 offset, float targetAlpha, float duration, Easing.EasingFn easing = null)
	{
		Vector2 anchoredPosition = rect.anchoredPosition;
		return PresUtil.MoveAndFade(rect, offset + anchoredPosition, targetAlpha, duration, easing);
	}

	// Token: 0x06001747 RID: 5959 RVA: 0x000825B8 File Offset: 0x000807B8
	public static Promise OffsetToAndFade(RectTransform rect, Vector2 offset, float targetAlpha, float duration, Easing.EasingFn easing = null)
	{
		Vector2 anchoredPosition = rect.anchoredPosition;
		rect.anchoredPosition = offset + anchoredPosition;
		return PresUtil.MoveAndFade(rect, anchoredPosition, targetAlpha, duration, easing);
	}
}
