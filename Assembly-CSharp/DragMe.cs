using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200000C RID: 12
public class DragMe : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x0600002B RID: 43 RVA: 0x00002B24 File Offset: 0x00000D24
	public void OnBeginDrag(PointerEventData eventData)
	{
		Canvas canvas = DragMe.FindInParents<Canvas>(base.gameObject);
		if (canvas == null)
		{
			return;
		}
		this.m_DraggingIcon = global::UnityEngine.Object.Instantiate<GameObject>(base.gameObject, canvas.transform, false);
		GraphicRaycaster component = this.m_DraggingIcon.GetComponent<GraphicRaycaster>();
		if (component != null)
		{
			component.enabled = false;
		}
		this.m_DraggingIcon.name = "dragObj";
		this.m_DraggingIcon.transform.SetAsLastSibling();
		RectTransform component2 = this.m_DraggingIcon.GetComponent<RectTransform>();
		component2.pivot = Vector2.zero;
		component2.sizeDelta = base.GetComponent<RectTransform>().rect.size;
		this.x = this.m_DraggingIcon.transform.position.x;
		Canvas component3 = this.m_DraggingIcon.GetComponent<Canvas>();
		component3.overrideSorting = true;
		component3.sortingOrder = 99;
		if (this.dragOnSurfaces)
		{
			this.m_DraggingPlane = base.transform as RectTransform;
		}
		else
		{
			this.m_DraggingPlane = canvas.transform as RectTransform;
		}
		this.SetDraggedPosition(eventData);
		this.listener.OnBeginDrag(eventData.position);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002C41 File Offset: 0x00000E41
	public void OnDrag(PointerEventData data)
	{
		if (this.m_DraggingIcon != null)
		{
			this.SetDraggedPosition(data);
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002C58 File Offset: 0x00000E58
	private void SetDraggedPosition(PointerEventData data)
	{
		if (this.dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
		{
			this.m_DraggingPlane = data.pointerEnter.transform as RectTransform;
		}
		RectTransform component = this.m_DraggingIcon.GetComponent<RectTransform>();
		Vector3 vector;
		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(this.m_DraggingPlane, data.position, data.pressEventCamera, out vector))
		{
			vector.x = this.x + 5f;
			vector.y -= component.sizeDelta.y / 2f;
			component.position = vector;
			component.rotation = this.m_DraggingPlane.rotation;
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002D17 File Offset: 0x00000F17
	public void OnEndDrag(PointerEventData eventData)
	{
		this.listener.OnEndDrag(eventData.position);
		if (this.m_DraggingIcon != null)
		{
			global::UnityEngine.Object.Destroy(this.m_DraggingIcon);
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002D44 File Offset: 0x00000F44
	public static T FindInParents<T>(GameObject go) where T : Component
	{
		if (go == null)
		{
			return default(T);
		}
		T t = default(T);
		Transform transform = go.transform.parent;
		while (transform != null && t == null)
		{
			t = transform.gameObject.GetComponent<T>();
			transform = transform.parent;
		}
		return t;
	}

	// Token: 0x04000031 RID: 49
	public bool dragOnSurfaces = true;

	// Token: 0x04000032 RID: 50
	private GameObject m_DraggingIcon;

	// Token: 0x04000033 RID: 51
	private RectTransform m_DraggingPlane;

	// Token: 0x04000034 RID: 52
	private float x;

	// Token: 0x04000035 RID: 53
	public DragMe.IDragListener listener;

	// Token: 0x02001024 RID: 4132
	public interface IDragListener
	{
		// Token: 0x06007F21 RID: 32545
		void OnBeginDrag(Vector2 position);

		// Token: 0x06007F22 RID: 32546
		void OnEndDrag(Vector2 position);
	}
}
