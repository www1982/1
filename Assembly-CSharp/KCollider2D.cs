using System;
using UnityEngine;

// Token: 0x020005CB RID: 1483
public abstract class KCollider2D : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06002230 RID: 8752 RVA: 0x000C4E2C File Offset: 0x000C302C
	// (set) Token: 0x06002231 RID: 8753 RVA: 0x000C4E34 File Offset: 0x000C3034
	public Vector2 offset
	{
		get
		{
			return this._offset;
		}
		set
		{
			this._offset = value;
			this.MarkDirty(false);
		}
	}

	// Token: 0x06002232 RID: 8754 RVA: 0x000C4E44 File Offset: 0x000C3044
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.autoRegisterSimRender = false;
	}

	// Token: 0x06002233 RID: 8755 RVA: 0x000C4E53 File Offset: 0x000C3053
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Singleton<CellChangeMonitor>.Instance.RegisterMovementStateChanged(base.transform, new Action<Transform, bool>(KCollider2D.OnMovementStateChanged));
		this.MarkDirty(true);
	}

	// Token: 0x06002234 RID: 8756 RVA: 0x000C4E7E File Offset: 0x000C307E
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Singleton<CellChangeMonitor>.Instance.UnregisterMovementStateChanged(base.transform, new Action<Transform, bool>(KCollider2D.OnMovementStateChanged));
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x06002235 RID: 8757 RVA: 0x000C4EB4 File Offset: 0x000C30B4
	public void MarkDirty(bool force = false)
	{
		bool flag = force || this.partitionerEntry.IsValid();
		if (!flag)
		{
			return;
		}
		Extents extents = this.GetExtents();
		if (!force && this.cachedExtents.x == extents.x && this.cachedExtents.y == extents.y && this.cachedExtents.width == extents.width && this.cachedExtents.height == extents.height)
		{
			return;
		}
		this.cachedExtents = extents;
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		if (flag)
		{
			this.partitionerEntry = GameScenePartitioner.Instance.Add(null, this, this.cachedExtents, GameScenePartitioner.Instance.collisionLayer, null);
		}
	}

	// Token: 0x06002236 RID: 8758 RVA: 0x000C4F6B File Offset: 0x000C316B
	private void OnMovementStateChanged(bool is_moving)
	{
		if (is_moving)
		{
			this.MarkDirty(false);
			SimAndRenderScheduler.instance.Add(this, false);
			return;
		}
		SimAndRenderScheduler.instance.Remove(this);
	}

	// Token: 0x06002237 RID: 8759 RVA: 0x000C4F8F File Offset: 0x000C318F
	private static void OnMovementStateChanged(Transform transform, bool is_moving)
	{
		transform.GetComponent<KCollider2D>().OnMovementStateChanged(is_moving);
	}

	// Token: 0x06002238 RID: 8760 RVA: 0x000C4F9D File Offset: 0x000C319D
	public void RenderEveryTick(float dt)
	{
		this.MarkDirty(false);
	}

	// Token: 0x06002239 RID: 8761
	public abstract bool Intersects(Vector2 pos);

	// Token: 0x0600223A RID: 8762
	public abstract Extents GetExtents();

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x0600223B RID: 8763
	public abstract Bounds bounds { get; }

	// Token: 0x040013EC RID: 5100
	[SerializeField]
	public Vector2 _offset;

	// Token: 0x040013ED RID: 5101
	private Extents cachedExtents;

	// Token: 0x040013EE RID: 5102
	private HandleVector<int>.Handle partitionerEntry;
}
