using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000CFD RID: 3325
[AddComponentMenu("KMonoBehaviour/scripts/KUISelectable")]
public class KUISelectable : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600667B RID: 26235 RVA: 0x0026AF08 File Offset: 0x00269108
	protected override void OnPrefabInit()
	{
	}

	// Token: 0x0600667C RID: 26236 RVA: 0x0026AF0A File Offset: 0x0026910A
	protected override void OnSpawn()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x0600667D RID: 26237 RVA: 0x0026AF28 File Offset: 0x00269128
	public void SetTarget(GameObject target)
	{
		this.target = target;
	}

	// Token: 0x0600667E RID: 26238 RVA: 0x0026AF31 File Offset: 0x00269131
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.target != null)
		{
			SelectTool.Instance.SetHoverOverride(this.target.GetComponent<KSelectable>());
		}
	}

	// Token: 0x0600667F RID: 26239 RVA: 0x0026AF56 File Offset: 0x00269156
	public void OnPointerExit(PointerEventData eventData)
	{
		SelectTool.Instance.SetHoverOverride(null);
	}

	// Token: 0x06006680 RID: 26240 RVA: 0x0026AF63 File Offset: 0x00269163
	private void OnClick()
	{
		if (this.target != null)
		{
			SelectTool.Instance.Select(this.target.GetComponent<KSelectable>(), false);
		}
	}

	// Token: 0x06006681 RID: 26241 RVA: 0x0026AF89 File Offset: 0x00269189
	protected override void OnCmpDisable()
	{
		if (SelectTool.Instance != null)
		{
			SelectTool.Instance.SetHoverOverride(null);
		}
	}

	// Token: 0x04004636 RID: 17974
	private GameObject target;
}
