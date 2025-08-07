using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000DBF RID: 3519
public class ScheduleScreenColumnEntry : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerDownHandler
{
	// Token: 0x06006F04 RID: 28420 RVA: 0x002A41B0 File Offset: 0x002A23B0
	public void OnPointerEnter(PointerEventData event_data)
	{
		this.RunCallbacks();
	}

	// Token: 0x06006F05 RID: 28421 RVA: 0x002A41B8 File Offset: 0x002A23B8
	private void RunCallbacks()
	{
		if (Input.GetMouseButton(0) && this.onLeftClick != null)
		{
			this.onLeftClick();
		}
	}

	// Token: 0x06006F06 RID: 28422 RVA: 0x002A41D5 File Offset: 0x002A23D5
	public void OnPointerDown(PointerEventData event_data)
	{
		this.RunCallbacks();
	}

	// Token: 0x04004C4D RID: 19533
	public Image image;

	// Token: 0x04004C4E RID: 19534
	public global::System.Action onLeftClick;
}
