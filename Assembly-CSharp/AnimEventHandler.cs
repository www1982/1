using System;
using UnityEngine;

// Token: 0x0200056C RID: 1388
[AddComponentMenu("KMonoBehaviour/scripts/AnimEventHandler")]
public class AnimEventHandler : KMonoBehaviour
{
	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06001EEF RID: 7919 RVA: 0x000B1A38 File Offset: 0x000AFC38
	// (remove) Token: 0x06001EF0 RID: 7920 RVA: 0x000B1A70 File Offset: 0x000AFC70
	private event AnimEventHandler.SetPos onWorkTargetSet;

	// Token: 0x06001EF1 RID: 7921 RVA: 0x000B1AA5 File Offset: 0x000AFCA5
	public int GetCachedCell()
	{
		return this.pickupable.cachedCell;
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x000B1AB4 File Offset: 0x000AFCB4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.cachedTransform = base.transform;
		this.pickupable = base.GetComponent<Pickupable>();
		foreach (KBatchedAnimTracker kbatchedAnimTracker in base.GetComponentsInChildren<KBatchedAnimTracker>(true))
		{
			if (kbatchedAnimTracker.useTargetPoint)
			{
				this.onWorkTargetSet += kbatchedAnimTracker.SetTarget;
			}
		}
		this.baseOffset = this.animCollider.offset;
		AnimEventHandlerManager.Instance.Add(this);
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x000B1B2F File Offset: 0x000AFD2F
	protected override void OnCleanUp()
	{
		AnimEventHandlerManager.Instance.Remove(this);
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x000B1B3C File Offset: 0x000AFD3C
	protected override void OnForcedCleanUp()
	{
		this.navigator = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x000B1B4B File Offset: 0x000AFD4B
	public HashedString GetContext()
	{
		return this.context;
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x000B1B53 File Offset: 0x000AFD53
	public void UpdateWorkTarget(Vector3 pos)
	{
		if (this.onWorkTargetSet != null)
		{
			this.onWorkTargetSet(pos);
		}
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x000B1B69 File Offset: 0x000AFD69
	public void SetContext(HashedString context)
	{
		this.context = context;
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x000B1B72 File Offset: 0x000AFD72
	public void SetTargetPos(Vector3 target_pos)
	{
		this.targetPos = target_pos;
	}

	// Token: 0x06001EF9 RID: 7929 RVA: 0x000B1B7B File Offset: 0x000AFD7B
	public Vector3 GetTargetPos()
	{
		return this.targetPos;
	}

	// Token: 0x06001EFA RID: 7930 RVA: 0x000B1B83 File Offset: 0x000AFD83
	public void ClearContext()
	{
		this.context = default(HashedString);
	}

	// Token: 0x06001EFB RID: 7931 RVA: 0x000B1B94 File Offset: 0x000AFD94
	public void UpdateOffset()
	{
		Vector3 pivotSymbolPosition = this.controller.GetPivotSymbolPosition();
		Vector3 vector = this.navigator.NavGrid.GetNavTypeData(this.navigator.CurrentNavType).animControllerOffset;
		Vector3 position = this.cachedTransform.position;
		Vector2 vector2 = new Vector2(this.baseOffset.x + pivotSymbolPosition.x - position.x - vector.x, this.baseOffset.y + pivotSymbolPosition.y - position.y + vector.y);
		if (this.animCollider.offset != vector2)
		{
			this.animCollider.offset = vector2;
		}
	}

	// Token: 0x040011F8 RID: 4600
	[MyCmpGet]
	private KBatchedAnimController controller;

	// Token: 0x040011F9 RID: 4601
	[MyCmpGet]
	private KBoxCollider2D animCollider;

	// Token: 0x040011FA RID: 4602
	[MyCmpGet]
	private Navigator navigator;

	// Token: 0x040011FB RID: 4603
	private Pickupable pickupable;

	// Token: 0x040011FC RID: 4604
	private Vector3 targetPos;

	// Token: 0x040011FD RID: 4605
	public Transform cachedTransform;

	// Token: 0x040011FF RID: 4607
	public Vector2 baseOffset;

	// Token: 0x04001200 RID: 4608
	private HashedString context;

	// Token: 0x020013AD RID: 5037
	// (Invoke) Token: 0x06008AF6 RID: 35574
	private delegate void SetPos(Vector3 pos);
}
