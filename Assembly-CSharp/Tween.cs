using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000E6B RID: 3691
public class Tween : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060075C9 RID: 30153 RVA: 0x002D1648 File Offset: 0x002CF848
	private void Awake()
	{
		this.Selectable = base.GetComponent<Selectable>();
	}

	// Token: 0x060075CA RID: 30154 RVA: 0x002D1656 File Offset: 0x002CF856
	public void OnPointerEnter(PointerEventData data)
	{
		this.Direction = 1f;
	}

	// Token: 0x060075CB RID: 30155 RVA: 0x002D1663 File Offset: 0x002CF863
	public void OnPointerExit(PointerEventData data)
	{
		this.Direction = -1f;
	}

	// Token: 0x060075CC RID: 30156 RVA: 0x002D1670 File Offset: 0x002CF870
	private void Update()
	{
		if (this.Selectable.interactable)
		{
			float x = base.transform.localScale.x;
			float num = x + this.Direction * Time.unscaledDeltaTime * Tween.ScaleSpeed;
			num = Mathf.Min(num, Tween.Scale);
			num = Mathf.Max(num, 1f);
			if (num != x)
			{
				base.transform.localScale = new Vector3(num, num, 1f);
			}
		}
	}

	// Token: 0x040051AF RID: 20911
	private static float Scale = 1.025f;

	// Token: 0x040051B0 RID: 20912
	private static float ScaleSpeed = 0.5f;

	// Token: 0x040051B1 RID: 20913
	private Selectable Selectable;

	// Token: 0x040051B2 RID: 20914
	private float Direction = -1f;
}
