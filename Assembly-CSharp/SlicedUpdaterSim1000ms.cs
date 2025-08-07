using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000B17 RID: 2839
public abstract class SlicedUpdaterSim1000ms<T> : KMonoBehaviour, ISim200ms where T : KMonoBehaviour, ISlicedSim1000ms
{
	// Token: 0x060053A4 RID: 21412 RVA: 0x001E73A7 File Offset: 0x001E55A7
	protected override void OnPrefabInit()
	{
		this.InitializeSlices();
		base.OnPrefabInit();
		SlicedUpdaterSim1000ms<T>.instance = this;
	}

	// Token: 0x060053A5 RID: 21413 RVA: 0x001E73BB File Offset: 0x001E55BB
	protected override void OnForcedCleanUp()
	{
		SlicedUpdaterSim1000ms<T>.instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x060053A6 RID: 21414 RVA: 0x001E73CC File Offset: 0x001E55CC
	private void InitializeSlices()
	{
		int num = SlicedUpdaterSim1000ms<T>.NUM_200MS_BUCKETS * this.numSlicesPer200ms;
		this.m_slices = new List<SlicedUpdaterSim1000ms<T>.Slice>();
		for (int i = 0; i < num; i++)
		{
			this.m_slices.Add(new SlicedUpdaterSim1000ms<T>.Slice());
		}
		this.m_nextSliceIdx = 0;
	}

	// Token: 0x060053A7 RID: 21415 RVA: 0x001E7414 File Offset: 0x001E5614
	private int GetSliceIdx(T toBeUpdated)
	{
		return Mathf.Abs(toBeUpdated.GetComponent<KPrefabID>().InstanceID) % this.m_slices.Count;
	}

	// Token: 0x060053A8 RID: 21416 RVA: 0x001E7438 File Offset: 0x001E5638
	public void RegisterUpdate1000ms(T toBeUpdated)
	{
		SlicedUpdaterSim1000ms<T>.Slice slice = this.m_slices[this.GetSliceIdx(toBeUpdated)];
		slice.Register(toBeUpdated);
		DebugUtil.DevAssert(slice.Count < this.maxUpdatesPer200ms, string.Format("The SlicedUpdaterSim1000ms for {0} wants to update no more than {1} instances per 200ms tick, but a slice has grown more than the SlicedUpdaterSim1000ms can support.", typeof(T).Name, this.maxUpdatesPer200ms), null);
	}

	// Token: 0x060053A9 RID: 21417 RVA: 0x001E7495 File Offset: 0x001E5695
	public void UnregisterUpdate1000ms(T toBeUpdated)
	{
		this.m_slices[this.GetSliceIdx(toBeUpdated)].Unregister(toBeUpdated);
	}

	// Token: 0x060053AA RID: 21418 RVA: 0x001E74B0 File Offset: 0x001E56B0
	public void Sim200ms(float dt)
	{
		foreach (SlicedUpdaterSim1000ms<T>.Slice slice in this.m_slices)
		{
			slice.IncrementDt(dt);
		}
		int num = 0;
		int i = 0;
		while (i < this.numSlicesPer200ms)
		{
			SlicedUpdaterSim1000ms<T>.Slice slice2 = this.m_slices[this.m_nextSliceIdx];
			num += slice2.Count;
			if (num > this.maxUpdatesPer200ms && i > 0)
			{
				break;
			}
			slice2.Update();
			i++;
			this.m_nextSliceIdx = (this.m_nextSliceIdx + 1) % this.m_slices.Count;
		}
	}

	// Token: 0x04003839 RID: 14393
	private static int NUM_200MS_BUCKETS = 5;

	// Token: 0x0400383A RID: 14394
	public static SlicedUpdaterSim1000ms<T> instance;

	// Token: 0x0400383B RID: 14395
	[Serialize]
	public int maxUpdatesPer200ms = 300;

	// Token: 0x0400383C RID: 14396
	[Serialize]
	public int numSlicesPer200ms = 3;

	// Token: 0x0400383D RID: 14397
	private List<SlicedUpdaterSim1000ms<T>.Slice> m_slices;

	// Token: 0x0400383E RID: 14398
	private int m_nextSliceIdx;

	// Token: 0x02001C1C RID: 7196
	private class Slice
	{
		// Token: 0x0600A999 RID: 43417 RVA: 0x003B85E2 File Offset: 0x003B67E2
		public void Register(T toBeUpdated)
		{
			if (this.m_timeSinceLastUpdate == 0f)
			{
				this.m_updateList.Add(toBeUpdated);
				return;
			}
			this.m_recentlyAdded[toBeUpdated] = 0f;
		}

		// Token: 0x0600A99A RID: 43418 RVA: 0x003B860F File Offset: 0x003B680F
		public void Unregister(T toBeUpdated)
		{
			if (!this.m_updateList.Remove(toBeUpdated))
			{
				this.m_recentlyAdded.Remove(toBeUpdated);
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x0600A99B RID: 43419 RVA: 0x003B862C File Offset: 0x003B682C
		public int Count
		{
			get
			{
				return this.m_updateList.Count + this.m_recentlyAdded.Count;
			}
		}

		// Token: 0x0600A99C RID: 43420 RVA: 0x003B8645 File Offset: 0x003B6845
		public List<T> GetUpdateList()
		{
			List<T> list = new List<T>();
			list.AddRange(this.m_updateList);
			list.AddRange(this.m_recentlyAdded.Keys);
			return list;
		}

		// Token: 0x0600A99D RID: 43421 RVA: 0x003B866C File Offset: 0x003B686C
		public void Update()
		{
			foreach (T t in this.m_updateList)
			{
				t.SlicedSim1000ms(this.m_timeSinceLastUpdate);
			}
			foreach (KeyValuePair<T, float> keyValuePair in this.m_recentlyAdded)
			{
				keyValuePair.Key.SlicedSim1000ms(keyValuePair.Value);
				this.m_updateList.Add(keyValuePair.Key);
			}
			this.m_recentlyAdded.Clear();
			this.m_timeSinceLastUpdate = 0f;
		}

		// Token: 0x0600A99E RID: 43422 RVA: 0x003B8744 File Offset: 0x003B6944
		public void IncrementDt(float dt)
		{
			this.m_timeSinceLastUpdate += dt;
			if (this.m_recentlyAdded.Count > 0)
			{
				foreach (T t in new List<T>(this.m_recentlyAdded.Keys))
				{
					Dictionary<T, float> recentlyAdded = this.m_recentlyAdded;
					T t2 = t;
					recentlyAdded[t2] += dt;
				}
			}
		}

		// Token: 0x0400852E RID: 34094
		private float m_timeSinceLastUpdate;

		// Token: 0x0400852F RID: 34095
		private List<T> m_updateList = new List<T>();

		// Token: 0x04008530 RID: 34096
		private Dictionary<T, float> m_recentlyAdded = new Dictionary<T, float>();
	}
}
