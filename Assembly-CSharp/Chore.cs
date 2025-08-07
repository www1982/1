using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020004A8 RID: 1192
public abstract class Chore
{
	// Token: 0x1700007C RID: 124
	// (get) Token: 0x060018CD RID: 6349
	// (set) Token: 0x060018CE RID: 6350
	public abstract int id { get; protected set; }

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x060018CF RID: 6351
	// (set) Token: 0x060018D0 RID: 6352
	public abstract int priorityMod { get; protected set; }

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060018D1 RID: 6353
	// (set) Token: 0x060018D2 RID: 6354
	public abstract ChoreType choreType { get; protected set; }

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060018D3 RID: 6355
	// (set) Token: 0x060018D4 RID: 6356
	public abstract ChoreDriver driver { get; protected set; }

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x060018D5 RID: 6357
	// (set) Token: 0x060018D6 RID: 6358
	public abstract ChoreDriver lastDriver { get; protected set; }

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x060018D7 RID: 6359
	public abstract bool isNull { get; }

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x060018D8 RID: 6360
	public abstract GameObject gameObject { get; }

	// Token: 0x060018D9 RID: 6361
	public abstract bool SatisfiesUrge(Urge urge);

	// Token: 0x060018DA RID: 6362
	public abstract bool IsValid();

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x060018DB RID: 6363
	// (set) Token: 0x060018DC RID: 6364
	public abstract IStateMachineTarget target { get; protected set; }

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060018DD RID: 6365
	// (set) Token: 0x060018DE RID: 6366
	public abstract bool isComplete { get; protected set; }

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x060018DF RID: 6367
	// (set) Token: 0x060018E0 RID: 6368
	public abstract bool IsPreemptable { get; protected set; }

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x060018E1 RID: 6369
	// (set) Token: 0x060018E2 RID: 6370
	public abstract ChoreConsumer overrideTarget { get; protected set; }

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x060018E3 RID: 6371
	// (set) Token: 0x060018E4 RID: 6372
	public abstract Prioritizable prioritizable { get; protected set; }

	// Token: 0x17000088 RID: 136
	// (get) Token: 0x060018E5 RID: 6373
	// (set) Token: 0x060018E6 RID: 6374
	public abstract ChoreProvider provider { get; set; }

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x060018E7 RID: 6375
	// (set) Token: 0x060018E8 RID: 6376
	public abstract bool runUntilComplete { get; set; }

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x060018E9 RID: 6377
	// (set) Token: 0x060018EA RID: 6378
	public abstract bool isExpanded { get; protected set; }

	// Token: 0x060018EB RID: 6379
	public abstract List<Chore.PreconditionInstance> GetPreconditions();

	// Token: 0x060018EC RID: 6380
	public abstract bool CanPreempt(Chore.Precondition.Context context);

	// Token: 0x060018ED RID: 6381
	public abstract void PrepareChore(ref Chore.Precondition.Context context);

	// Token: 0x060018EE RID: 6382
	public abstract void Cancel(string reason);

	// Token: 0x060018EF RID: 6383
	public abstract ReportManager.ReportType GetReportType();

	// Token: 0x060018F0 RID: 6384
	public abstract string GetReportName(string context = null);

	// Token: 0x060018F1 RID: 6385
	public abstract void AddPrecondition(Chore.Precondition precondition, object data = null);

	// Token: 0x060018F2 RID: 6386
	public abstract void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> incomplete_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override);

	// Token: 0x060018F3 RID: 6387 RVA: 0x0008AC64 File Offset: 0x00088E64
	public void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
		this.CollectChores(consumer_state, succeeded_contexts, null, failed_contexts, is_attempting_override);
	}

	// Token: 0x060018F4 RID: 6388
	public abstract void Cleanup();

	// Token: 0x060018F5 RID: 6389
	public abstract void Fail(string reason);

	// Token: 0x060018F6 RID: 6390
	public abstract void Reserve(ChoreDriver reserver);

	// Token: 0x060018F7 RID: 6391
	public abstract void Begin(Chore.Precondition.Context context);

	// Token: 0x060018F8 RID: 6392
	public abstract bool InProgress();

	// Token: 0x060018F9 RID: 6393 RVA: 0x0008AC72 File Offset: 0x00088E72
	public virtual string ResolveString(string str)
	{
		return str;
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x0008AC75 File Offset: 0x00088E75
	public static int GetNextChoreID()
	{
		return ++Chore.nextId;
	}

	// Token: 0x04000E55 RID: 3669
	public PrioritySetting masterPriority;

	// Token: 0x04000E56 RID: 3670
	public bool showAvailabilityInHoverText = true;

	// Token: 0x04000E57 RID: 3671
	public Action<Chore> onExit;

	// Token: 0x04000E58 RID: 3672
	public Action<Chore> onComplete;

	// Token: 0x04000E59 RID: 3673
	private static int nextId;

	// Token: 0x04000E5A RID: 3674
	public const int MAX_PLAYER_BASIC_PRIORITY = 9;

	// Token: 0x04000E5B RID: 3675
	public const int MIN_PLAYER_BASIC_PRIORITY = 1;

	// Token: 0x04000E5C RID: 3676
	public const int MAX_PLAYER_HIGH_PRIORITY = 0;

	// Token: 0x04000E5D RID: 3677
	public const int MIN_PLAYER_HIGH_PRIORITY = 0;

	// Token: 0x04000E5E RID: 3678
	public const int MAX_PLAYER_EMERGENCY_PRIORITY = 1;

	// Token: 0x04000E5F RID: 3679
	public const int MIN_PLAYER_EMERGENCY_PRIORITY = 1;

	// Token: 0x04000E60 RID: 3680
	public const int DEFAULT_BASIC_PRIORITY = 5;

	// Token: 0x04000E61 RID: 3681
	public const int MAX_BASIC_PRIORITY = 10;

	// Token: 0x04000E62 RID: 3682
	public const int MIN_BASIC_PRIORITY = 0;

	// Token: 0x04000E63 RID: 3683
	public static bool ENABLE_PERSONAL_PRIORITIES = true;

	// Token: 0x04000E64 RID: 3684
	public static PrioritySetting DefaultPrioritySetting = new PrioritySetting(PriorityScreen.PriorityClass.basic, 5);

	// Token: 0x020012DD RID: 4829
	// (Invoke) Token: 0x06008805 RID: 34821
	public delegate bool PreconditionFn(ref Chore.Precondition.Context context, object data);

	// Token: 0x020012DE RID: 4830
	public struct PreconditionInstance
	{
		// Token: 0x040067C4 RID: 26564
		public Chore.Precondition condition;

		// Token: 0x040067C5 RID: 26565
		public object data;
	}

	// Token: 0x020012DF RID: 4831
	public struct Precondition
	{
		// Token: 0x040067C6 RID: 26566
		public string id;

		// Token: 0x040067C7 RID: 26567
		public string description;

		// Token: 0x040067C8 RID: 26568
		public int sortOrder;

		// Token: 0x040067C9 RID: 26569
		public Chore.PreconditionFn fn;

		// Token: 0x040067CA RID: 26570
		public bool canExecuteOnAnyThread;

		// Token: 0x0200268E RID: 9870
		[DebuggerDisplay("{chore.GetType()}, {chore.gameObject.name}")]
		public struct Context : IComparable<Chore.Precondition.Context>, IEquatable<Chore.Precondition.Context>
		{
			// Token: 0x0600C41C RID: 50204 RVA: 0x0040CD48 File Offset: 0x0040AF48
			public Context(Chore chore, ChoreConsumerState consumer_state, bool is_attempting_override, object data = null)
			{
				this.masterPriority = chore.masterPriority;
				this.personalPriority = consumer_state.consumer.GetPersonalPriority(chore.choreType);
				this.priority = 0;
				this.priorityMod = chore.priorityMod;
				this.consumerPriority = 0;
				this.interruptPriority = 0;
				this.cost = 0;
				this.chore = chore;
				this.consumerState = consumer_state;
				this.failedPreconditionId = -1;
				this.skippedPreconditions = false;
				this.isAttemptingOverride = is_attempting_override;
				this.data = data;
				this.choreTypeForPermission = chore.choreType;
				this.skipMoreSatisfyingEarlyPrecondition = RootMenu.Instance != null && RootMenu.Instance.IsBuildingChorePanelActive();
				this.SetPriority(chore);
			}

			// Token: 0x0600C41D RID: 50205 RVA: 0x0040CE00 File Offset: 0x0040B000
			public void Set(Chore chore, ChoreConsumerState consumer_state, bool is_attempting_override, object data = null)
			{
				this.masterPriority = chore.masterPriority;
				this.priority = 0;
				this.priorityMod = chore.priorityMod;
				this.consumerPriority = 0;
				this.interruptPriority = 0;
				this.cost = 0;
				this.chore = chore;
				this.consumerState = consumer_state;
				this.failedPreconditionId = -1;
				this.skippedPreconditions = false;
				this.isAttemptingOverride = is_attempting_override;
				this.data = data;
				this.choreTypeForPermission = chore.choreType;
				this.SetPriority(chore);
			}

			// Token: 0x0600C41E RID: 50206 RVA: 0x0040CE80 File Offset: 0x0040B080
			public void SetPriority(Chore chore)
			{
				this.priority = (Game.Instance.advancedPersonalPriorities ? chore.choreType.explicitPriority : chore.choreType.priority);
				this.priorityMod = chore.priorityMod;
				this.interruptPriority = chore.choreType.interruptPriority;
			}

			// Token: 0x0600C41F RID: 50207 RVA: 0x0040CED4 File Offset: 0x0040B0D4
			public bool IsSuccess()
			{
				return this.failedPreconditionId == -1 && !this.skippedPreconditions;
			}

			// Token: 0x0600C420 RID: 50208 RVA: 0x0040CEEA File Offset: 0x0040B0EA
			public bool IsComplete()
			{
				return !this.skippedPreconditions;
			}

			// Token: 0x0600C421 RID: 50209 RVA: 0x0040CEF8 File Offset: 0x0040B0F8
			public bool IsPotentialSuccess()
			{
				if (this.IsSuccess())
				{
					return true;
				}
				if (this.chore.driver == this.consumerState.choreDriver)
				{
					return true;
				}
				if (this.failedPreconditionId != -1)
				{
					if (this.failedPreconditionId >= 0 && this.failedPreconditionId < this.chore.GetPreconditions().Count)
					{
						return this.chore.GetPreconditions()[this.failedPreconditionId].condition.id == ChorePreconditions.instance.IsMoreSatisfyingLate.id;
					}
					DebugUtil.DevLogErrorFormat("failedPreconditionId out of range {0}/{1}", new object[]
					{
						this.failedPreconditionId,
						this.chore.GetPreconditions().Count
					});
				}
				return false;
			}

			// Token: 0x0600C422 RID: 50210 RVA: 0x0040CFC8 File Offset: 0x0040B1C8
			private void DoPreconditions(bool mainThreadOnly)
			{
				bool flag = Game.IsOnMainThread();
				List<Chore.PreconditionInstance> preconditions = this.chore.GetPreconditions();
				this.skippedPreconditions = false;
				int i = 0;
				while (i < preconditions.Count)
				{
					Chore.PreconditionInstance preconditionInstance = preconditions[i];
					if (preconditionInstance.condition.canExecuteOnAnyThread)
					{
						if (!mainThreadOnly)
						{
							goto IL_0043;
						}
					}
					else
					{
						if (flag)
						{
							goto IL_0043;
						}
						this.skippedPreconditions = true;
					}
					IL_006B:
					i++;
					continue;
					IL_0043:
					if (!preconditionInstance.condition.fn(ref this, preconditionInstance.data))
					{
						this.failedPreconditionId = i;
						this.skippedPreconditions = false;
						return;
					}
					goto IL_006B;
				}
			}

			// Token: 0x0600C423 RID: 50211 RVA: 0x0040D04D File Offset: 0x0040B24D
			public void RunPreconditions()
			{
				this.DoPreconditions(false);
			}

			// Token: 0x0600C424 RID: 50212 RVA: 0x0040D056 File Offset: 0x0040B256
			public void FinishPreconditions()
			{
				this.DoPreconditions(true);
			}

			// Token: 0x0600C425 RID: 50213 RVA: 0x0040D060 File Offset: 0x0040B260
			public int CompareTo(Chore.Precondition.Context obj)
			{
				bool flag = this.failedPreconditionId != -1;
				bool flag2 = obj.failedPreconditionId != -1;
				if (flag == flag2)
				{
					int num = this.masterPriority.priority_class - obj.masterPriority.priority_class;
					if (num != 0)
					{
						return num;
					}
					int num2 = this.personalPriority - obj.personalPriority;
					if (num2 != 0)
					{
						return num2;
					}
					int num3 = this.masterPriority.priority_value - obj.masterPriority.priority_value;
					if (num3 != 0)
					{
						return num3;
					}
					int num4 = this.priority - obj.priority;
					if (num4 != 0)
					{
						return num4;
					}
					int num5 = this.priorityMod - obj.priorityMod;
					if (num5 != 0)
					{
						return num5;
					}
					int num6 = this.consumerPriority - obj.consumerPriority;
					if (num6 != 0)
					{
						return num6;
					}
					int num7 = obj.cost - this.cost;
					if (num7 != 0)
					{
						return num7;
					}
					if (this.chore == null && obj.chore == null)
					{
						return 0;
					}
					if (this.chore == null)
					{
						return -1;
					}
					if (obj.chore == null)
					{
						return 1;
					}
					return this.chore.id - obj.chore.id;
				}
				else
				{
					if (!flag)
					{
						return 1;
					}
					return -1;
				}
			}

			// Token: 0x0600C426 RID: 50214 RVA: 0x0040D17C File Offset: 0x0040B37C
			public override bool Equals(object obj)
			{
				Chore.Precondition.Context context = (Chore.Precondition.Context)obj;
				return this.CompareTo(context) == 0;
			}

			// Token: 0x0600C427 RID: 50215 RVA: 0x0040D19A File Offset: 0x0040B39A
			public bool Equals(Chore.Precondition.Context other)
			{
				return this.CompareTo(other) == 0;
			}

			// Token: 0x0600C428 RID: 50216 RVA: 0x0040D1A6 File Offset: 0x0040B3A6
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x0600C429 RID: 50217 RVA: 0x0040D1B8 File Offset: 0x0040B3B8
			public static bool operator ==(Chore.Precondition.Context x, Chore.Precondition.Context y)
			{
				return x.CompareTo(y) == 0;
			}

			// Token: 0x0600C42A RID: 50218 RVA: 0x0040D1C5 File Offset: 0x0040B3C5
			public static bool operator !=(Chore.Precondition.Context x, Chore.Precondition.Context y)
			{
				return x.CompareTo(y) != 0;
			}

			// Token: 0x0600C42B RID: 50219 RVA: 0x0040D1D2 File Offset: 0x0040B3D2
			public static bool ShouldFilter(string filter, string text)
			{
				return !string.IsNullOrEmpty(filter) && (string.IsNullOrEmpty(text) || text.ToLower().IndexOf(filter) < 0);
			}

			// Token: 0x0400AB57 RID: 43863
			public PrioritySetting masterPriority;

			// Token: 0x0400AB58 RID: 43864
			public int personalPriority;

			// Token: 0x0400AB59 RID: 43865
			public int priority;

			// Token: 0x0400AB5A RID: 43866
			public int priorityMod;

			// Token: 0x0400AB5B RID: 43867
			public int interruptPriority;

			// Token: 0x0400AB5C RID: 43868
			public int cost;

			// Token: 0x0400AB5D RID: 43869
			public int consumerPriority;

			// Token: 0x0400AB5E RID: 43870
			public Chore chore;

			// Token: 0x0400AB5F RID: 43871
			public ChoreConsumerState consumerState;

			// Token: 0x0400AB60 RID: 43872
			public int failedPreconditionId;

			// Token: 0x0400AB61 RID: 43873
			public bool skippedPreconditions;

			// Token: 0x0400AB62 RID: 43874
			public object data;

			// Token: 0x0400AB63 RID: 43875
			public bool isAttemptingOverride;

			// Token: 0x0400AB64 RID: 43876
			public ChoreType choreTypeForPermission;

			// Token: 0x0400AB65 RID: 43877
			public bool skipMoreSatisfyingEarlyPrecondition;
		}
	}
}
