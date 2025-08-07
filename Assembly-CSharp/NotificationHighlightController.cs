using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D83 RID: 3459
public class NotificationHighlightController : KMonoBehaviour
{
	// Token: 0x06006BA1 RID: 27553 RVA: 0x0028A2B2 File Offset: 0x002884B2
	protected override void OnSpawn()
	{
		this.highlightBox = Util.KInstantiateUI<RectTransform>(this.highlightBoxPrefab.gameObject, base.gameObject, false);
		this.HideBox();
	}

	// Token: 0x06006BA2 RID: 27554 RVA: 0x0028A2D8 File Offset: 0x002884D8
	[ContextMenu("Force Update")]
	protected void LateUpdate()
	{
		bool flag = false;
		if (this.activeTargetNotification != null)
		{
			foreach (NotificationHighlightTarget notificationHighlightTarget in this.targets)
			{
				if (notificationHighlightTarget.targetKey == this.activeTargetNotification.highlightTarget)
				{
					this.SnapBoxToTarget(notificationHighlightTarget);
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			this.HideBox();
		}
	}

	// Token: 0x06006BA3 RID: 27555 RVA: 0x0028A35C File Offset: 0x0028855C
	public void AddTarget(NotificationHighlightTarget target)
	{
		this.targets.Add(target);
	}

	// Token: 0x06006BA4 RID: 27556 RVA: 0x0028A36A File Offset: 0x0028856A
	public void RemoveTarget(NotificationHighlightTarget target)
	{
		this.targets.Remove(target);
	}

	// Token: 0x06006BA5 RID: 27557 RVA: 0x0028A379 File Offset: 0x00288579
	public void SetActiveTarget(ManagementMenuNotification notification)
	{
		this.activeTargetNotification = notification;
	}

	// Token: 0x06006BA6 RID: 27558 RVA: 0x0028A382 File Offset: 0x00288582
	public void ClearActiveTarget(ManagementMenuNotification checkNotification)
	{
		if (checkNotification == this.activeTargetNotification)
		{
			this.activeTargetNotification = null;
		}
	}

	// Token: 0x06006BA7 RID: 27559 RVA: 0x0028A394 File Offset: 0x00288594
	public void ClearActiveTarget()
	{
		this.activeTargetNotification = null;
	}

	// Token: 0x06006BA8 RID: 27560 RVA: 0x0028A39D File Offset: 0x0028859D
	public void TargetViewed(NotificationHighlightTarget target)
	{
		if (this.activeTargetNotification != null && this.activeTargetNotification.highlightTarget == target.targetKey)
		{
			this.activeTargetNotification.View();
		}
	}

	// Token: 0x06006BA9 RID: 27561 RVA: 0x0028A3CC File Offset: 0x002885CC
	private void SnapBoxToTarget(NotificationHighlightTarget target)
	{
		RectTransform rectTransform = target.rectTransform();
		Vector3 position = rectTransform.GetPosition();
		this.highlightBox.sizeDelta = rectTransform.rect.size;
		this.highlightBox.SetPosition(position + new Vector3(rectTransform.rect.position.x, rectTransform.rect.position.y, 0f));
		RectMask2D componentInParent = rectTransform.GetComponentInParent<RectMask2D>();
		if (componentInParent != null)
		{
			RectTransform rectTransform2 = componentInParent.rectTransform();
			Vector3 vector = rectTransform2.TransformPoint(rectTransform2.rect.min);
			Vector3 vector2 = rectTransform2.TransformPoint(rectTransform2.rect.max);
			Vector3 vector3 = this.highlightBox.TransformPoint(this.highlightBox.rect.min);
			Vector3 vector4 = this.highlightBox.TransformPoint(this.highlightBox.rect.max);
			Vector3 vector5 = vector - vector3;
			Vector3 vector6 = vector2 - vector4;
			if (vector5.x > 0f)
			{
				this.highlightBox.anchoredPosition = this.highlightBox.anchoredPosition + new Vector2(vector5.x, 0f);
				this.highlightBox.sizeDelta -= new Vector2(vector5.x, 0f);
			}
			else if (vector5.y > 0f)
			{
				this.highlightBox.anchoredPosition = this.highlightBox.anchoredPosition + new Vector2(0f, vector5.y);
				this.highlightBox.sizeDelta -= new Vector2(0f, vector5.y);
			}
			if (vector6.x < 0f)
			{
				this.highlightBox.sizeDelta += new Vector2(vector6.x, 0f);
			}
			if (vector6.y < 0f)
			{
				this.highlightBox.sizeDelta += new Vector2(0f, vector6.y);
			}
		}
		this.highlightBox.gameObject.SetActive(this.highlightBox.sizeDelta.x > 0f && this.highlightBox.sizeDelta.y > 0f);
	}

	// Token: 0x06006BAA RID: 27562 RVA: 0x0028A65B File Offset: 0x0028885B
	private void HideBox()
	{
		this.highlightBox.gameObject.SetActive(false);
	}

	// Token: 0x04004958 RID: 18776
	public RectTransform highlightBoxPrefab;

	// Token: 0x04004959 RID: 18777
	private RectTransform highlightBox;

	// Token: 0x0400495A RID: 18778
	private List<NotificationHighlightTarget> targets = new List<NotificationHighlightTarget>();

	// Token: 0x0400495B RID: 18779
	private ManagementMenuNotification activeTargetNotification;
}
