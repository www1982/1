using System;
using UnityEngine;

// Token: 0x0200098A RID: 2442
[AddComponentMenu("KMonoBehaviour/scripts/SelectMarker")]
public class SelectMarker : KMonoBehaviour
{
	// Token: 0x060046E8 RID: 18152 RVA: 0x00197B99 File Offset: 0x00195D99
	public void SetTargetTransform(Transform target_transform)
	{
		this.targetTransform = target_transform;
		this.LateUpdate();
	}

	// Token: 0x060046E9 RID: 18153 RVA: 0x00197BA8 File Offset: 0x00195DA8
	private void LateUpdate()
	{
		if (this.targetTransform == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		Vector3 position = this.targetTransform.GetPosition();
		KCollider2D component = this.targetTransform.GetComponent<KCollider2D>();
		if (component != null)
		{
			position.x = component.bounds.center.x;
			position.y = component.bounds.center.y + component.bounds.size.y / 2f + 0.1f;
		}
		else
		{
			position.y += 2f;
		}
		Vector3 vector = new Vector3(0f, (Mathf.Sin(Time.unscaledTime * 4f) + 1f) * this.animationOffset, 0f);
		base.transform.SetPosition(position + vector);
	}

	// Token: 0x04002EE6 RID: 12006
	public float animationOffset = 0.1f;

	// Token: 0x04002EE7 RID: 12007
	private Transform targetTransform;
}
