using System;
using System.Collections.Generic;

// Token: 0x02000454 RID: 1108
public class HashMapObjectPool<PoolKey, PoolValue>
{
	// Token: 0x0600170E RID: 5902 RVA: 0x00081E71 File Offset: 0x00080071
	public HashMapObjectPool(Func<PoolKey, PoolValue> instantiator, int initialCount = 0)
	{
		this.initialCount = initialCount;
		this.instantiator = instantiator;
	}

	// Token: 0x0600170F RID: 5903 RVA: 0x00081E94 File Offset: 0x00080094
	public HashMapObjectPool(HashMapObjectPool<PoolKey, PoolValue>.IPoolDescriptor[] descriptors, int initialCount = 0)
	{
		this.initialCount = initialCount;
		for (int i = 0; i < descriptors.Length; i++)
		{
			if (this.objectPoolMap.ContainsKey(descriptors[i].PoolId))
			{
				Debug.LogWarning(string.Format("HshMapObjectPool alaready contains key of {0}! Skipping!", descriptors[i].PoolId));
			}
			else
			{
				this.objectPoolMap[descriptors[i].PoolId] = new ObjectPool<PoolValue>(new Func<PoolValue>(descriptors[i].GetInstance), initialCount);
			}
		}
	}

	// Token: 0x06001710 RID: 5904 RVA: 0x00081F24 File Offset: 0x00080124
	public PoolValue GetInstance(PoolKey poolId)
	{
		ObjectPool<PoolValue> objectPool;
		if (!this.objectPoolMap.TryGetValue(poolId, out objectPool))
		{
			objectPool = (this.objectPoolMap[poolId] = new ObjectPool<PoolValue>(new Func<PoolValue>(this.PoolInstantiator), this.initialCount));
		}
		this.currentPoolId = poolId;
		return objectPool.GetInstance();
	}

	// Token: 0x06001711 RID: 5905 RVA: 0x00081F78 File Offset: 0x00080178
	public void ReleaseInstance(PoolKey poolId, PoolValue inst)
	{
		ObjectPool<PoolValue> objectPool;
		if (inst == null || !this.objectPoolMap.TryGetValue(poolId, out objectPool))
		{
			return;
		}
		objectPool.ReleaseInstance(inst);
	}

	// Token: 0x06001712 RID: 5906 RVA: 0x00081FA8 File Offset: 0x000801A8
	private PoolValue PoolInstantiator()
	{
		if (this.instantiator == null)
		{
			return default(PoolValue);
		}
		return this.instantiator(this.currentPoolId);
	}

	// Token: 0x04000D8D RID: 3469
	private Dictionary<PoolKey, ObjectPool<PoolValue>> objectPoolMap = new Dictionary<PoolKey, ObjectPool<PoolValue>>();

	// Token: 0x04000D8E RID: 3470
	private int initialCount;

	// Token: 0x04000D8F RID: 3471
	private PoolKey currentPoolId;

	// Token: 0x04000D90 RID: 3472
	private Func<PoolKey, PoolValue> instantiator;

	// Token: 0x0200122D RID: 4653
	public interface IPoolDescriptor
	{
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x0600855D RID: 34141
		PoolKey PoolId { get; }

		// Token: 0x0600855E RID: 34142
		PoolValue GetInstance();
	}
}
