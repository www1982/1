using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000E82 RID: 3714
public class DialogPanel : MonoBehaviour, IDeselectHandler, IEventSystemHandler
{
	// Token: 0x06007660 RID: 30304 RVA: 0x002D4684 File Offset: 0x002D2884
	public void OnDeselect(BaseEventData eventData)
	{
		if (this.destroyOnDeselect)
		{
			foreach (object obj in base.transform)
			{
				Util.KDestroyGameObject(((Transform)obj).gameObject);
			}
		}
		base.gameObject.SetActive(false);
	}

	// Token: 0x04005233 RID: 21043
	public bool destroyOnDeselect = true;
}
