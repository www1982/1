using System;
using UnityEngine;

// Token: 0x02000940 RID: 2368
public struct GravityComponent
{
	// Token: 0x06004374 RID: 17268 RVA: 0x00184FE4 File Offset: 0x001831E4
	public GravityComponent(Transform transform, global::System.Action on_landed, Vector2 initial_velocity, bool land_on_fake_floors, bool mayLeaveWorld)
	{
		this.transform = transform;
		this.elapsedTime = 0f;
		this.velocity = initial_velocity;
		this.onLanded = on_landed;
		this.landOnFakeFloors = land_on_fake_floors;
		this.mayLeaveWorld = mayLeaveWorld;
		this.collider2D = transform.GetComponent<KCollider2D>();
		this.extents = GravityComponent.GetExtents(this.collider2D);
	}

	// Token: 0x06004375 RID: 17269 RVA: 0x00185040 File Offset: 0x00183240
	public static float GetGroundOffset(KCollider2D collider)
	{
		if (collider != null)
		{
			return collider.bounds.extents.y - collider.offset.y;
		}
		return 0f;
	}

	// Token: 0x06004376 RID: 17270 RVA: 0x0018507B File Offset: 0x0018327B
	public static float GetGroundOffset(GravityComponent gravityComponent)
	{
		if (gravityComponent.collider2D != null)
		{
			return gravityComponent.extents.y - gravityComponent.collider2D.offset.y;
		}
		return 0f;
	}

	// Token: 0x06004377 RID: 17271 RVA: 0x001850B0 File Offset: 0x001832B0
	public static Vector2 GetExtents(KCollider2D collider)
	{
		if (collider != null)
		{
			return collider.bounds.extents;
		}
		return Vector2.zero;
	}

	// Token: 0x06004378 RID: 17272 RVA: 0x001850DF File Offset: 0x001832DF
	public static Vector2 GetOffset(KCollider2D collider)
	{
		if (collider != null)
		{
			return collider.offset;
		}
		return Vector2.zero;
	}

	// Token: 0x04002CF6 RID: 11510
	public Transform transform;

	// Token: 0x04002CF7 RID: 11511
	public Vector2 velocity;

	// Token: 0x04002CF8 RID: 11512
	public float elapsedTime;

	// Token: 0x04002CF9 RID: 11513
	public global::System.Action onLanded;

	// Token: 0x04002CFA RID: 11514
	public bool landOnFakeFloors;

	// Token: 0x04002CFB RID: 11515
	public bool mayLeaveWorld;

	// Token: 0x04002CFC RID: 11516
	public Vector2 extents;

	// Token: 0x04002CFD RID: 11517
	public KCollider2D collider2D;
}
