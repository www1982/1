using System;

// Token: 0x02000AF4 RID: 2804
public class ScenePartitionerEntry
{
	// Token: 0x0600526D RID: 21101 RVA: 0x001E0C2C File Offset: 0x001DEE2C
	public ScenePartitionerEntry(string name, object obj, int x, int y, int width, int height, ScenePartitionerLayer layer, ScenePartitioner partitioner, Action<object> event_callback)
	{
		if (x < 0 || y < 0 || width >= 0)
		{
		}
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
		this.layer = layer.layer;
		this.partitioner = partitioner;
		this.eventCallback = event_callback;
		this.obj = obj;
	}

	// Token: 0x0600526E RID: 21102 RVA: 0x001E0C95 File Offset: 0x001DEE95
	public void UpdatePosition(int x, int y)
	{
		this.partitioner.UpdatePosition(x, y, this);
	}

	// Token: 0x0600526F RID: 21103 RVA: 0x001E0CA5 File Offset: 0x001DEEA5
	public void UpdatePosition(Extents e)
	{
		this.partitioner.UpdatePosition(e, this);
	}

	// Token: 0x06005270 RID: 21104 RVA: 0x001E0CB4 File Offset: 0x001DEEB4
	public void Release()
	{
		if (this.partitioner != null)
		{
			this.partitioner.Remove(this);
		}
	}

	// Token: 0x0400376D RID: 14189
	public int x;

	// Token: 0x0400376E RID: 14190
	public int y;

	// Token: 0x0400376F RID: 14191
	public int width;

	// Token: 0x04003770 RID: 14192
	public int height;

	// Token: 0x04003771 RID: 14193
	public int layer;

	// Token: 0x04003772 RID: 14194
	public int queryId;

	// Token: 0x04003773 RID: 14195
	public ScenePartitioner partitioner;

	// Token: 0x04003774 RID: 14196
	public Action<object> eventCallback;

	// Token: 0x04003775 RID: 14197
	public object obj;
}
