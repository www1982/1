using System;
using UnityEngine;

// Token: 0x02000917 RID: 2327
public struct FallerComponent
{
	// Token: 0x060040AA RID: 16554 RVA: 0x0016A7CC File Offset: 0x001689CC
	public FallerComponent(Transform transform, Vector2 initial_velocity)
	{
		this.transform = transform;
		this.transformInstanceId = transform.GetInstanceID();
		this.isFalling = false;
		this.initialVelocity = initial_velocity;
		this.partitionerEntry = default(HandleVector<int>.Handle);
		this.solidChangedCB = null;
		this.cellChangedCB = null;
		KCircleCollider2D component = transform.GetComponent<KCircleCollider2D>();
		if (component != null)
		{
			this.offset = component.radius;
			return;
		}
		KCollider2D component2 = transform.GetComponent<KCollider2D>();
		if (component2 != null)
		{
			this.offset = transform.GetPosition().y - component2.bounds.min.y;
			return;
		}
		this.offset = 0f;
	}

	// Token: 0x0400285E RID: 10334
	public Transform transform;

	// Token: 0x0400285F RID: 10335
	public int transformInstanceId;

	// Token: 0x04002860 RID: 10336
	public bool isFalling;

	// Token: 0x04002861 RID: 10337
	public float offset;

	// Token: 0x04002862 RID: 10338
	public Vector2 initialVelocity;

	// Token: 0x04002863 RID: 10339
	public HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002864 RID: 10340
	public Action<object> solidChangedCB;

	// Token: 0x04002865 RID: 10341
	public global::System.Action cellChangedCB;
}
