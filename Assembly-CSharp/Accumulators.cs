using System;
using System.Collections.Generic;

// Token: 0x02000698 RID: 1688
public class Accumulators
{
	// Token: 0x06002931 RID: 10545 RVA: 0x000EFA61 File Offset: 0x000EDC61
	public Accumulators()
	{
		this.elapsedTime = 0f;
		this.accumulated = new KCompactedVector<float>(0);
		this.average = new KCompactedVector<float>(0);
	}

	// Token: 0x06002932 RID: 10546 RVA: 0x000EFA8C File Offset: 0x000EDC8C
	public HandleVector<int>.Handle Add(string name, KMonoBehaviour owner)
	{
		HandleVector<int>.Handle handle = this.accumulated.Allocate(0f);
		this.average.Allocate(0f);
		return handle;
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x000EFAAF File Offset: 0x000EDCAF
	public HandleVector<int>.Handle Remove(HandleVector<int>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return HandleVector<int>.InvalidHandle;
		}
		this.accumulated.Free(handle);
		this.average.Free(handle);
		return HandleVector<int>.InvalidHandle;
	}

	// Token: 0x06002934 RID: 10548 RVA: 0x000EFAE0 File Offset: 0x000EDCE0
	public void Sim200ms(float dt)
	{
		this.elapsedTime += dt;
		if (this.elapsedTime < 3f)
		{
			return;
		}
		this.elapsedTime -= 3f;
		List<float> dataList = this.accumulated.GetDataList();
		List<float> dataList2 = this.average.GetDataList();
		int count = dataList.Count;
		float num = 0.33333334f;
		for (int i = 0; i < count; i++)
		{
			dataList2[i] = dataList[i] * num;
			dataList[i] = 0f;
		}
	}

	// Token: 0x06002935 RID: 10549 RVA: 0x000EFB6F File Offset: 0x000EDD6F
	public float GetAverageRate(HandleVector<int>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return 0f;
		}
		return this.average.GetData(handle);
	}

	// Token: 0x06002936 RID: 10550 RVA: 0x000EFB8C File Offset: 0x000EDD8C
	public void Accumulate(HandleVector<int>.Handle handle, float amount)
	{
		float data = this.accumulated.GetData(handle);
		this.accumulated.SetData(handle, data + amount);
	}

	// Token: 0x04001858 RID: 6232
	private const float TIME_WINDOW = 3f;

	// Token: 0x04001859 RID: 6233
	private float elapsedTime;

	// Token: 0x0400185A RID: 6234
	private KCompactedVector<float> accumulated;

	// Token: 0x0400185B RID: 6235
	private KCompactedVector<float> average;
}
