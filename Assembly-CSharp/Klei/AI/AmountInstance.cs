using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FD0 RID: 4048
	[SerializationConfig(MemberSerialization.OptIn)]
	[DebuggerDisplay("{amount.Name} {value} ({deltaAttribute.value}/{minAttribute.value}/{maxAttribute.value})")]
	public class AmountInstance : ModifierInstance<Amount>, ISaveLoadable, ISim200ms
	{
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06007D13 RID: 32019 RVA: 0x003215F4 File Offset: 0x0031F7F4
		public Amount amount
		{
			get
			{
				return this.modifier;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06007D14 RID: 32020 RVA: 0x003215FC File Offset: 0x0031F7FC
		// (set) Token: 0x06007D15 RID: 32021 RVA: 0x00321604 File Offset: 0x0031F804
		public bool paused
		{
			get
			{
				return this._paused;
			}
			set
			{
				this._paused = this.paused;
				if (this._paused)
				{
					this.Deactivate();
					return;
				}
				this.Activate();
			}
		}

		// Token: 0x06007D16 RID: 32022 RVA: 0x00321627 File Offset: 0x0031F827
		public float GetMin()
		{
			return this.minAttribute.GetTotalValue();
		}

		// Token: 0x06007D17 RID: 32023 RVA: 0x00321634 File Offset: 0x0031F834
		public float GetMax()
		{
			return this.maxAttribute.GetTotalValue();
		}

		// Token: 0x06007D18 RID: 32024 RVA: 0x00321641 File Offset: 0x0031F841
		public float GetDelta()
		{
			return this.deltaAttribute.GetTotalValue();
		}

		// Token: 0x06007D19 RID: 32025 RVA: 0x00321650 File Offset: 0x0031F850
		public AmountInstance(Amount amount, GameObject game_object)
			: base(game_object, amount)
		{
			Attributes attributes = game_object.GetAttributes();
			this.minAttribute = attributes.Add(amount.minAttribute);
			this.maxAttribute = attributes.Add(amount.maxAttribute);
			this.deltaAttribute = attributes.Add(amount.deltaAttribute);
		}

		// Token: 0x06007D1A RID: 32026 RVA: 0x003216A2 File Offset: 0x0031F8A2
		public float SetValue(float value)
		{
			this.value = Mathf.Min(Mathf.Max(value, this.GetMin()), this.GetMax());
			return this.value;
		}

		// Token: 0x06007D1B RID: 32027 RVA: 0x003216C7 File Offset: 0x0031F8C7
		public void Publish(float delta, float previous_value)
		{
			if (this.OnDelta != null)
			{
				this.OnDelta(delta);
			}
			if (this.OnMaxValueReached != null && previous_value < this.GetMax() && this.value >= this.GetMax())
			{
				this.OnMaxValueReached();
			}
		}

		// Token: 0x06007D1C RID: 32028 RVA: 0x00321708 File Offset: 0x0031F908
		public float ApplyDelta(float delta)
		{
			float num = this.value;
			this.SetValue(this.value + delta);
			this.Publish(delta, num);
			return this.value;
		}

		// Token: 0x06007D1D RID: 32029 RVA: 0x00321739 File Offset: 0x0031F939
		public string GetValueString()
		{
			return this.amount.GetValueString(this);
		}

		// Token: 0x06007D1E RID: 32030 RVA: 0x00321747 File Offset: 0x0031F947
		public string GetDescription()
		{
			return this.amount.GetDescription(this);
		}

		// Token: 0x06007D1F RID: 32031 RVA: 0x00321755 File Offset: 0x0031F955
		public string GetTooltip()
		{
			return this.amount.GetTooltip(this);
		}

		// Token: 0x06007D20 RID: 32032 RVA: 0x00321763 File Offset: 0x0031F963
		public void Activate()
		{
			SimAndRenderScheduler.instance.Add(this, false);
		}

		// Token: 0x06007D21 RID: 32033 RVA: 0x00321771 File Offset: 0x0031F971
		public void Sim200ms(float dt)
		{
		}

		// Token: 0x06007D22 RID: 32034 RVA: 0x00321774 File Offset: 0x0031F974
		public static void BatchUpdate(List<UpdateBucketWithUpdater<ISim200ms>.Entry> amount_instances, float time_delta)
		{
			if (time_delta == 0f)
			{
				return;
			}
			AmountInstance.BatchUpdateContext batchUpdateContext = new AmountInstance.BatchUpdateContext(amount_instances, time_delta);
			AmountInstance.AmmountInstanceBatchUpdateDispatcher.Instance.Reset(batchUpdateContext);
			GlobalJobManager.Run(AmountInstance.AmmountInstanceBatchUpdateDispatcher.Instance);
			AmountInstance.AmmountInstanceBatchUpdateDispatcher.Instance.Finish();
			AmountInstance.AmmountInstanceBatchUpdateDispatcher.Instance.Reset(AmountInstance.BatchUpdateContext.EmptyContext);
		}

		// Token: 0x06007D23 RID: 32035 RVA: 0x003217C1 File Offset: 0x0031F9C1
		public void Deactivate()
		{
			SimAndRenderScheduler.instance.Remove(this);
		}

		// Token: 0x04005E7B RID: 24187
		[Serialize]
		public float value;

		// Token: 0x04005E7C RID: 24188
		public AttributeInstance minAttribute;

		// Token: 0x04005E7D RID: 24189
		public AttributeInstance maxAttribute;

		// Token: 0x04005E7E RID: 24190
		public AttributeInstance deltaAttribute;

		// Token: 0x04005E7F RID: 24191
		public Action<float> OnDelta;

		// Token: 0x04005E80 RID: 24192
		public global::System.Action OnMaxValueReached;

		// Token: 0x04005E81 RID: 24193
		public bool hide;

		// Token: 0x04005E82 RID: 24194
		private bool _paused;

		// Token: 0x020025BC RID: 9660
		private struct BatchUpdateContext
		{
			// Token: 0x0600C175 RID: 49525 RVA: 0x00406639 File Offset: 0x00404839
			public BatchUpdateContext(List<UpdateBucketWithUpdater<ISim200ms>.Entry> amount_instances, float time_delta)
			{
				this.amount_instances = amount_instances;
				this.time_delta = time_delta;
			}

			// Token: 0x0400A89E RID: 43166
			public List<UpdateBucketWithUpdater<ISim200ms>.Entry> amount_instances;

			// Token: 0x0400A89F RID: 43167
			public float time_delta;

			// Token: 0x0400A8A0 RID: 43168
			public static AmountInstance.BatchUpdateContext EmptyContext = new AmountInstance.BatchUpdateContext(null, 0f);

			// Token: 0x02003858 RID: 14424
			public struct Result
			{
				// Token: 0x0400E403 RID: 58371
				public AmountInstance amount_instance;

				// Token: 0x0400E404 RID: 58372
				public float previous;

				// Token: 0x0400E405 RID: 58373
				public float delta;
			}
		}

		// Token: 0x020025BD RID: 9661
		private class AmmountInstanceBatchUpdateDispatcher : WorkItemCollectionWithThreadContex<AmountInstance.BatchUpdateContext, List<AmountInstance.BatchUpdateContext.Result>>
		{
			// Token: 0x17000CC6 RID: 3270
			// (get) Token: 0x0600C177 RID: 49527 RVA: 0x0040665B File Offset: 0x0040485B
			public static AmountInstance.AmmountInstanceBatchUpdateDispatcher Instance
			{
				get
				{
					if (AmountInstance.AmmountInstanceBatchUpdateDispatcher.instance == null || AmountInstance.AmmountInstanceBatchUpdateDispatcher.instance.threadContexts.Count != GlobalJobManager.ThreadCount)
					{
						AmountInstance.AmmountInstanceBatchUpdateDispatcher.instance = new AmountInstance.AmmountInstanceBatchUpdateDispatcher();
					}
					return AmountInstance.AmmountInstanceBatchUpdateDispatcher.instance;
				}
			}

			// Token: 0x0600C178 RID: 49528 RVA: 0x0040668C File Offset: 0x0040488C
			public AmmountInstanceBatchUpdateDispatcher()
			{
				this.threadContexts = new List<List<AmountInstance.BatchUpdateContext.Result>>(GlobalJobManager.ThreadCount);
				for (int i = 0; i < GlobalJobManager.ThreadCount; i++)
				{
					this.threadContexts.Add(new List<AmountInstance.BatchUpdateContext.Result>());
				}
			}

			// Token: 0x0600C179 RID: 49529 RVA: 0x004066CF File Offset: 0x004048CF
			public void Reset(AmountInstance.BatchUpdateContext context)
			{
				this.sharedData = context;
				if (context.amount_instances == null)
				{
					this.count = 0;
					return;
				}
				this.count = (context.amount_instances.Count + 512 - 1) / 512;
			}

			// Token: 0x0600C17A RID: 49530 RVA: 0x00406708 File Offset: 0x00404908
			public override void RunItem(int item, ref AmountInstance.BatchUpdateContext shared_data, List<AmountInstance.BatchUpdateContext.Result> thread_context, int threadIndex)
			{
				int num = item * 512;
				int num2 = Mathf.Min(num + 512, shared_data.amount_instances.Count);
				for (int i = num; i < num2; i++)
				{
					AmountInstance amountInstance = (AmountInstance)shared_data.amount_instances[i].data;
					float num3 = amountInstance.GetDelta() * shared_data.time_delta;
					if (num3 != 0f)
					{
						thread_context.Add(new AmountInstance.BatchUpdateContext.Result
						{
							amount_instance = amountInstance,
							previous = amountInstance.value,
							delta = num3
						});
						amountInstance.SetValue(amountInstance.value + num3);
					}
				}
			}

			// Token: 0x0600C17B RID: 49531 RVA: 0x004067A8 File Offset: 0x004049A8
			public void Finish()
			{
				foreach (List<AmountInstance.BatchUpdateContext.Result> list in this.threadContexts)
				{
					foreach (AmountInstance.BatchUpdateContext.Result result in list)
					{
						result.amount_instance.Publish(result.delta, result.previous);
					}
					list.Clear();
				}
			}

			// Token: 0x0400A8A1 RID: 43169
			private const int kBatchSize = 512;

			// Token: 0x0400A8A2 RID: 43170
			private static AmountInstance.AmmountInstanceBatchUpdateDispatcher instance;
		}
	}
}
