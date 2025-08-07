using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000481 RID: 1153
public class FetchAreaChore : Chore<FetchAreaChore.StatesInstance>
{
	// Token: 0x17000073 RID: 115
	// (get) Token: 0x0600182F RID: 6191 RVA: 0x00086C87 File Offset: 0x00084E87
	public bool IsFetching
	{
		get
		{
			return base.smi.pickingup;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06001830 RID: 6192 RVA: 0x00086C94 File Offset: 0x00084E94
	public bool IsDelivering
	{
		get
		{
			return base.smi.delivering;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06001831 RID: 6193 RVA: 0x00086CA1 File Offset: 0x00084EA1
	public GameObject GetFetchTarget
	{
		get
		{
			return base.smi.sm.fetchTarget.Get(base.smi);
		}
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x00086CC0 File Offset: 0x00084EC0
	public FetchAreaChore(Chore.Precondition.Context context)
		: base(context.chore.choreType, context.consumerState.consumer, context.consumerState.choreProvider, false, null, null, null, context.masterPriority.priority_class, context.masterPriority.priority_value, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		this.showAvailabilityInHoverText = false;
		base.smi = new FetchAreaChore.StatesInstance(this, context);
	}

	// Token: 0x06001833 RID: 6195 RVA: 0x00086D28 File Offset: 0x00084F28
	public override void Cleanup()
	{
		base.Cleanup();
	}

	// Token: 0x06001834 RID: 6196 RVA: 0x00086D30 File Offset: 0x00084F30
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.Begin(context);
		base.Begin(context);
	}

	// Token: 0x06001835 RID: 6197 RVA: 0x00086D45 File Offset: 0x00084F45
	protected override void End(string reason)
	{
		base.smi.End();
		base.End(reason);
	}

	// Token: 0x06001836 RID: 6198 RVA: 0x00086D59 File Offset: 0x00084F59
	private void OnTagsChanged(object data)
	{
		if (base.smi.sm.fetchTarget.Get(base.smi) != null)
		{
			this.Fail("Tags changed");
		}
	}

	// Token: 0x06001837 RID: 6199 RVA: 0x00086D8C File Offset: 0x00084F8C
	private static bool IsPickupableStillValidForChore(Pickupable pickupable, FetchChore chore)
	{
		KPrefabID kprefabID = pickupable.KPrefabID;
		if ((chore.criteria == FetchChore.MatchCriteria.MatchID && !chore.tags.Contains(kprefabID.PrefabTag)) || (chore.criteria == FetchChore.MatchCriteria.MatchTags && !kprefabID.HasTag(chore.tagsFirst)))
		{
			global::Debug.Log(string.Format("Pickupable {0} is not valid for chore because it is not or does not contain one of these tags: {1}", pickupable, string.Join<Tag>(",", chore.tags)));
			return false;
		}
		if (chore.requiredTag.IsValid && !kprefabID.HasTag(chore.requiredTag))
		{
			global::Debug.Log(string.Format("Pickupable {0} is not valid for chore because it does not have the required tag: {1}", pickupable, chore.requiredTag));
			return false;
		}
		if (kprefabID.HasAnyTags(chore.forbiddenTags))
		{
			global::Debug.Log(string.Format("Pickupable {0} is not valid for chore because it has the forbidden tags: {1}", pickupable, string.Join<Tag>(",", chore.forbiddenTags)));
			return false;
		}
		return pickupable.isChoreAllowedToPickup(chore.choreType);
	}

	// Token: 0x06001838 RID: 6200 RVA: 0x00086E68 File Offset: 0x00085068
	public static void GatherNearbyFetchChores(FetchChore root_chore, Chore.Precondition.Context context, int x, int y, int radius, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> failed_contexts)
	{
		ListPool<ScenePartitionerEntry, FetchAreaChore>.PooledList pooledList = ListPool<ScenePartitionerEntry, FetchAreaChore>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(x - radius, y - radius, radius * 2 + 1, radius * 2 + 1, GameScenePartitioner.Instance.fetchChoreLayer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			(pooledList[i].obj as FetchChore).CollectChoresFromGlobalChoreProvider(context.consumerState, succeeded_contexts, null, failed_contexts, true);
		}
		pooledList.Recycle();
	}

	// Token: 0x0200127E RID: 4734
	public class StatesInstance : GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.GameInstance
	{
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060086A3 RID: 34467 RVA: 0x0033E946 File Offset: 0x0033CB46
		public Tag RootChore_RequiredTag
		{
			get
			{
				return this.rootChore.requiredTag;
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060086A4 RID: 34468 RVA: 0x0033E953 File Offset: 0x0033CB53
		public bool RootChore_ValidateRequiredTagOnTagChange
		{
			get
			{
				return this.rootChore.validateRequiredTagOnTagChange;
			}
		}

		// Token: 0x060086A5 RID: 34469 RVA: 0x0033E960 File Offset: 0x0033CB60
		public StatesInstance(FetchAreaChore master, Chore.Precondition.Context context)
			: base(master)
		{
			this.rootContext = context;
			this.rootChore = context.chore as FetchChore;
		}

		// Token: 0x060086A6 RID: 34470 RVA: 0x0033E9C4 File Offset: 0x0033CBC4
		public void Begin(Chore.Precondition.Context context)
		{
			base.sm.fetcher.Set(context.consumerState.gameObject, base.smi, false);
			this.chores.Clear();
			this.chores.Add(this.rootChore);
			int num;
			int num2;
			Grid.CellToXY(Grid.PosToCell(this.rootChore.destination.transform.GetPosition()), out num, out num2);
			ListPool<Chore.Precondition.Context, FetchAreaChore>.PooledList succeeded_contexts = ListPool<Chore.Precondition.Context, FetchAreaChore>.Allocate();
			ListPool<Chore.Precondition.Context, FetchAreaChore>.PooledList pooledList = ListPool<Chore.Precondition.Context, FetchAreaChore>.Allocate();
			if (this.rootChore.allowMultifetch)
			{
				FetchAreaChore.GatherNearbyFetchChores(this.rootChore, context, num, num2, 3, succeeded_contexts, pooledList);
			}
			float max_carry_weight = Mathf.Max(1f, Db.Get().Attributes.CarryAmount.Lookup(context.consumerState.consumer).GetTotalValue());
			Pickupable root_fetchable = context.data as Pickupable;
			if (root_fetchable == null)
			{
				global::Debug.Assert(succeeded_contexts.Count > 0, "succeeded_contexts was empty");
				FetchChore fetchChore = (FetchChore)succeeded_contexts[0].chore;
				global::Debug.Assert(fetchChore != null, "fetch_chore was null");
				DebugUtil.LogWarningArgs(new object[] { "Missing root_fetchable for FetchAreaChore", fetchChore.destination, fetchChore.tagsFirst });
				root_fetchable = fetchChore.FindFetchTarget(context.consumerState);
			}
			global::Debug.Assert(root_fetchable != null, "root_fetchable was null");
			ListPool<Pickupable, FetchAreaChore>.PooledList potential_fetchables = ListPool<Pickupable, FetchAreaChore>.Allocate();
			potential_fetchables.Add(root_fetchable);
			float fetch_amount_available = root_fetchable.UnreservedFetchAmount;
			max_carry_weight = Mathf.Max(root_fetchable.PrimaryElement.MassPerUnit, max_carry_weight);
			float minTakeAmount = root_fetchable.MinTakeAmount;
			int num3 = 0;
			int num4 = 0;
			Grid.CellToXY(Grid.PosToCell(root_fetchable.transform.GetPosition()), out num3, out num4);
			int num5 = 9;
			num3 -= 3;
			num4 -= 3;
			Tag root_fetchable_tag = root_fetchable.GetComponent<KPrefabID>().PrefabTag;
			Func<object, object, bool> func = delegate(object obj, object _)
			{
				if (fetch_amount_available > max_carry_weight)
				{
					return false;
				}
				Pickupable pickupable2 = obj as Pickupable;
				KPrefabID kprefabID = pickupable2.KPrefabID;
				if (pickupable2 == root_fetchable)
				{
					return true;
				}
				if (kprefabID.HasTag(GameTags.StoredPrivate))
				{
					return true;
				}
				if (kprefabID.PrefabTag != root_fetchable_tag)
				{
					return true;
				}
				if (pickupable2.UnreservedFetchAmount <= 0f)
				{
					return true;
				}
				if (this.rootChore.criteria == FetchChore.MatchCriteria.MatchID && !this.rootChore.tags.Contains(kprefabID.PrefabTag))
				{
					return true;
				}
				if (this.rootChore.criteria == FetchChore.MatchCriteria.MatchTags && !kprefabID.HasTag(this.rootChore.tagsFirst))
				{
					return true;
				}
				if (this.rootChore.requiredTag.IsValid && !kprefabID.HasTag(this.rootChore.requiredTag))
				{
					return true;
				}
				if (kprefabID.HasAnyTags(this.rootChore.forbiddenTags))
				{
					return true;
				}
				if (potential_fetchables.Contains(pickupable2))
				{
					return true;
				}
				if (!this.rootContext.consumerState.consumer.CanReach(pickupable2))
				{
					return true;
				}
				if (kprefabID.HasTag(GameTags.MarkedForMove))
				{
					return true;
				}
				if (!pickupable2.storage.IsNullOrDestroyed())
				{
					bool flag = true;
					foreach (Chore.Precondition.Context context3 in succeeded_contexts)
					{
						FetchChore fetchChore3 = context3.chore as FetchChore;
						if (!FetchManager.IsFetchablePickup(pickupable2, fetchChore3, fetchChore3.destination))
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						return true;
					}
				}
				float unreservedFetchAmount = pickupable2.UnreservedFetchAmount;
				potential_fetchables.Add(pickupable2);
				fetch_amount_available += unreservedFetchAmount;
				return potential_fetchables.Count < 10;
			};
			GameScenePartitioner.Instance.AsyncSafeVisit<object>(num3, num4, num5, num5, GameScenePartitioner.Instance.pickupablesLayer, func, null);
			GameScenePartitioner.Instance.AsyncSafeVisit<object>(num3, num4, num5, num5, GameScenePartitioner.Instance.storedPickupablesLayer, func, null);
			fetch_amount_available = Mathf.Min(max_carry_weight, fetch_amount_available);
			if (minTakeAmount > 0f)
			{
				fetch_amount_available -= fetch_amount_available % minTakeAmount;
			}
			this.deliveries.Clear();
			float num6 = Mathf.Min(this.rootChore.originalAmount, fetch_amount_available);
			if (minTakeAmount > 0f)
			{
				num6 -= num6 % minTakeAmount;
			}
			this.deliveries.Add(new FetchAreaChore.StatesInstance.Delivery(this.rootContext, num6, new Action<FetchChore>(this.OnFetchChoreCancelled)));
			float num7 = num6;
			int num8 = 0;
			while (num8 < succeeded_contexts.Count && num7 < fetch_amount_available)
			{
				Chore.Precondition.Context context2 = succeeded_contexts[num8];
				FetchChore fetchChore2 = context2.chore as FetchChore;
				if (fetchChore2 != this.rootChore && fetchChore2.overrideTarget == null && fetchChore2.driver == null && fetchChore2.tagsHash == this.rootChore.tagsHash && fetchChore2.requiredTag == this.rootChore.requiredTag && fetchChore2.forbidHash == this.rootChore.forbidHash)
				{
					num6 = Mathf.Min(fetchChore2.originalAmount, fetch_amount_available - num7);
					if (minTakeAmount > 0f)
					{
						num6 -= num6 % minTakeAmount;
					}
					this.chores.Add(fetchChore2);
					this.deliveries.Add(new FetchAreaChore.StatesInstance.Delivery(context2, num6, new Action<FetchChore>(this.OnFetchChoreCancelled)));
					num7 += num6;
					if (this.deliveries.Count >= 10)
					{
						break;
					}
				}
				num8++;
			}
			num7 = Mathf.Min(num7, fetch_amount_available);
			float num9 = num7;
			this.fetchables.Clear();
			int num10 = 0;
			while (num10 < potential_fetchables.Count && num9 > 0f)
			{
				Pickupable pickupable = potential_fetchables[num10];
				num9 -= pickupable.UnreservedFetchAmount;
				this.fetchables.Add(pickupable);
				num10++;
			}
			this.fetchAmountRequested = num7;
			this.reservations.Clear();
			succeeded_contexts.Recycle();
			pooledList.Recycle();
			potential_fetchables.Recycle();
		}

		// Token: 0x060086A7 RID: 34471 RVA: 0x0033EEA8 File Offset: 0x0033D0A8
		public void End()
		{
			foreach (FetchAreaChore.StatesInstance.Delivery delivery in this.deliveries)
			{
				delivery.Cleanup();
			}
			this.deliveries.Clear();
		}

		// Token: 0x060086A8 RID: 34472 RVA: 0x0033EF08 File Offset: 0x0033D108
		public void SetupDelivery()
		{
			if (this.deliveries.Count == 0)
			{
				this.StopSM("FetchAreaChoreComplete");
				return;
			}
			FetchAreaChore.StatesInstance.Delivery nextDelivery = this.deliveries[0];
			if (FetchAreaChore.StatesInstance.s_transientDeliveryTags.Contains(nextDelivery.chore.requiredTag))
			{
				nextDelivery.chore.requiredTag = Tag.Invalid;
			}
			this.deliverables.RemoveAll(delegate(Pickupable x)
			{
				if (x == null || x.FetchTotalAmount <= 0f)
				{
					return true;
				}
				if (x.KPrefabID.HasTag(GameTags.MarkedForMove))
				{
					return true;
				}
				if (!FetchAreaChore.IsPickupableStillValidForChore(x, nextDelivery.chore))
				{
					global::Debug.LogWarning(string.Format("Removing deliverable {0} for a delivery to {1} which did not request it", x, nextDelivery.chore.destination));
					return true;
				}
				return false;
			});
			if (this.deliverables.Count == 0)
			{
				this.StopSM("FetchAreaChoreComplete");
				return;
			}
			base.sm.deliveryDestination.Set(nextDelivery.destination, base.smi);
			base.sm.deliveryObject.Set(this.deliverables[0], base.smi);
			if (!(nextDelivery.destination != null))
			{
				base.smi.GoTo(base.sm.delivering.deliverfail);
				return;
			}
			if (!this.rootContext.consumerState.hasSolidTransferArm)
			{
				this.GoTo(base.sm.delivering.movetostorage);
				return;
			}
			if (this.rootContext.consumerState.consumer.IsWithinReach(this.deliveries[0].destination))
			{
				this.GoTo(base.sm.delivering.storing);
				return;
			}
			this.GoTo(base.sm.delivering.deliverfail);
		}

		// Token: 0x060086A9 RID: 34473 RVA: 0x0033F0A0 File Offset: 0x0033D2A0
		public void SetupFetch()
		{
			if (this.reservations.Count <= 0)
			{
				this.GoTo(base.sm.delivering.next);
				return;
			}
			this.SetFetchTarget(this.reservations[0].pickupable);
			base.sm.fetchResultTarget.Set(null, base.smi);
			base.sm.fetchAmount.Set(this.reservations[0].amount, base.smi, false);
			if (!(this.reservations[0].pickupable != null))
			{
				this.GoTo(base.sm.fetching.fetchfail);
				return;
			}
			if (!this.rootContext.consumerState.hasSolidTransferArm)
			{
				this.GoTo(base.sm.fetching.movetopickupable);
				return;
			}
			if (this.rootContext.consumerState.consumer.IsWithinReach(this.reservations[0].pickupable))
			{
				this.GoTo(base.sm.fetching.pickup);
				return;
			}
			this.GoTo(base.sm.fetching.fetchfail);
		}

		// Token: 0x060086AA RID: 34474 RVA: 0x0033F1E9 File Offset: 0x0033D3E9
		public void SetFetchTarget(Pickupable fetching)
		{
			base.sm.fetchTarget.Set(fetching, base.smi);
			if (fetching != null)
			{
				fetching.Subscribe(1122777325, new Action<object>(this.OnMarkForMove));
			}
		}

		// Token: 0x060086AB RID: 34475 RVA: 0x0033F224 File Offset: 0x0033D424
		public void DeliverFail()
		{
			if (this.deliveries.Count > 0)
			{
				this.deliveries[0].Cleanup();
				this.deliveries.RemoveAt(0);
			}
			this.GoTo(base.sm.delivering.next);
		}

		// Token: 0x060086AC RID: 34476 RVA: 0x0033F278 File Offset: 0x0033D478
		public void DeliverComplete()
		{
			Pickupable pickupable = base.sm.deliveryObject.Get<Pickupable>(base.smi);
			if (!(pickupable == null) && pickupable.FetchTotalAmount > 0f)
			{
				if (this.deliveries.Count > 0)
				{
					FetchAreaChore.StatesInstance.Delivery delivery = this.deliveries[0];
					Chore chore = delivery.chore;
					delivery.Complete(this.deliverables);
					delivery.Cleanup();
					if (this.deliveries.Count > 0 && this.deliveries[0].chore == chore)
					{
						this.deliveries.RemoveAt(0);
					}
				}
				this.GoTo(base.sm.delivering.next);
				return;
			}
			if (this.deliveries.Count > 0 && this.deliveries[0].chore.amount < PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
			{
				FetchAreaChore.StatesInstance.Delivery delivery2 = this.deliveries[0];
				Chore chore2 = delivery2.chore;
				delivery2.Complete(this.deliverables);
				delivery2.Cleanup();
				if (this.deliveries.Count > 0 && this.deliveries[0].chore == chore2)
				{
					this.deliveries.RemoveAt(0);
				}
				this.GoTo(base.sm.delivering.next);
				return;
			}
			base.smi.GoTo(base.sm.delivering.deliverfail);
		}

		// Token: 0x060086AD RID: 34477 RVA: 0x0033F3F4 File Offset: 0x0033D5F4
		public void FetchFail()
		{
			if (base.smi.sm.fetchTarget.Get(base.smi) != null)
			{
				base.smi.sm.fetchTarget.Get(base.smi).Unsubscribe(1122777325, new Action<object>(this.OnMarkForMove));
			}
			this.reservations[0].Cleanup();
			this.reservations.RemoveAt(0);
			this.GoTo(base.sm.fetching.next);
		}

		// Token: 0x060086AE RID: 34478 RVA: 0x0033F48C File Offset: 0x0033D68C
		public void FetchComplete()
		{
			this.reservations[0].Cleanup();
			this.reservations.RemoveAt(0);
			this.GoTo(base.sm.fetching.next);
		}

		// Token: 0x060086AF RID: 34479 RVA: 0x0033F4D0 File Offset: 0x0033D6D0
		public void SetupDeliverables()
		{
			foreach (GameObject gameObject in base.sm.fetcher.Get<Storage>(base.smi).items)
			{
				if (!(gameObject == null))
				{
					KPrefabID component = gameObject.GetComponent<KPrefabID>();
					if (!(component == null) && !component.HasTag(GameTags.MarkedForMove))
					{
						Pickupable component2 = component.GetComponent<Pickupable>();
						if (component2 != null)
						{
							this.deliverables.Add(component2);
						}
					}
				}
			}
		}

		// Token: 0x060086B0 RID: 34480 RVA: 0x0033F574 File Offset: 0x0033D774
		public void ReservePickupables()
		{
			ChoreConsumer choreConsumer = base.sm.fetcher.Get<ChoreConsumer>(base.smi);
			float num = this.fetchAmountRequested;
			foreach (Pickupable pickupable in this.fetchables)
			{
				if (num <= 0f)
				{
					break;
				}
				if (!pickupable.KPrefabID.HasTag(GameTags.MarkedForMove) && (pickupable.PrimaryElement.MassPerUnit <= 1f || num >= pickupable.PrimaryElement.MassPerUnit))
				{
					float num2 = Math.Min(num, pickupable.UnreservedFetchAmount);
					num -= num2;
					FetchAreaChore.StatesInstance.Reservation reservation = new FetchAreaChore.StatesInstance.Reservation(choreConsumer, pickupable, num2);
					this.reservations.Add(reservation);
				}
			}
		}

		// Token: 0x060086B1 RID: 34481 RVA: 0x0033F648 File Offset: 0x0033D848
		private void OnFetchChoreCancelled(FetchChore chore)
		{
			int i = 0;
			while (i < this.deliveries.Count)
			{
				if (this.deliveries[i].chore == chore)
				{
					if (this.deliveries.Count == 1)
					{
						this.StopSM("AllDelivericesCancelled");
						return;
					}
					if (i == 0)
					{
						base.sm.currentdeliverycancelled.Trigger(this);
						return;
					}
					this.deliveries[i].Cleanup();
					this.deliveries.RemoveAt(i);
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x060086B2 RID: 34482 RVA: 0x0033F6D4 File Offset: 0x0033D8D4
		public void UnreservePickupables()
		{
			foreach (FetchAreaChore.StatesInstance.Reservation reservation in this.reservations)
			{
				reservation.Cleanup();
			}
			this.reservations.Clear();
		}

		// Token: 0x060086B3 RID: 34483 RVA: 0x0033F734 File Offset: 0x0033D934
		public bool SameDestination(FetchChore fetch)
		{
			using (List<FetchChore>.Enumerator enumerator = this.chores.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.destination == fetch.destination)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060086B4 RID: 34484 RVA: 0x0033F798 File Offset: 0x0033D998
		public void OnMarkForMove(object data)
		{
			GameObject gameObject = base.smi.sm.fetchTarget.Get(base.smi);
			GameObject gameObject2 = data as GameObject;
			if (gameObject != null)
			{
				if (gameObject == gameObject2)
				{
					gameObject2.Unsubscribe(1122777325, new Action<object>(this.OnMarkForMove));
					base.smi.sm.fetchTarget.Set(null, base.smi);
					return;
				}
				global::Debug.LogError("Listening for MarkForMove on the incorrect fetch target. Subscriptions did not update correctly.");
			}
		}

		// Token: 0x04006673 RID: 26227
		private List<FetchChore> chores = new List<FetchChore>();

		// Token: 0x04006674 RID: 26228
		private List<Pickupable> fetchables = new List<Pickupable>();

		// Token: 0x04006675 RID: 26229
		private List<FetchAreaChore.StatesInstance.Reservation> reservations = new List<FetchAreaChore.StatesInstance.Reservation>();

		// Token: 0x04006676 RID: 26230
		private List<Pickupable> deliverables = new List<Pickupable>();

		// Token: 0x04006677 RID: 26231
		public List<FetchAreaChore.StatesInstance.Delivery> deliveries = new List<FetchAreaChore.StatesInstance.Delivery>();

		// Token: 0x04006678 RID: 26232
		private FetchChore rootChore;

		// Token: 0x04006679 RID: 26233
		private Chore.Precondition.Context rootContext;

		// Token: 0x0400667A RID: 26234
		private float fetchAmountRequested;

		// Token: 0x0400667B RID: 26235
		public bool delivering;

		// Token: 0x0400667C RID: 26236
		public bool pickingup;

		// Token: 0x0400667D RID: 26237
		private static Tag[] s_transientDeliveryTags = new Tag[]
		{
			GameTags.Garbage,
			GameTags.Creatures.Deliverable
		};

		// Token: 0x0200264A RID: 9802
		public struct Delivery
		{
			// Token: 0x17000CCB RID: 3275
			// (get) Token: 0x0600C309 RID: 49929 RVA: 0x0040B554 File Offset: 0x00409754
			// (set) Token: 0x0600C30A RID: 49930 RVA: 0x0040B55C File Offset: 0x0040975C
			public Storage destination { readonly get; private set; }

			// Token: 0x17000CCC RID: 3276
			// (get) Token: 0x0600C30B RID: 49931 RVA: 0x0040B565 File Offset: 0x00409765
			// (set) Token: 0x0600C30C RID: 49932 RVA: 0x0040B56D File Offset: 0x0040976D
			public float amount { readonly get; private set; }

			// Token: 0x17000CCD RID: 3277
			// (get) Token: 0x0600C30D RID: 49933 RVA: 0x0040B576 File Offset: 0x00409776
			// (set) Token: 0x0600C30E RID: 49934 RVA: 0x0040B57E File Offset: 0x0040977E
			public FetchChore chore { readonly get; private set; }

			// Token: 0x0600C30F RID: 49935 RVA: 0x0040B588 File Offset: 0x00409788
			public Delivery(Chore.Precondition.Context context, float amount_to_be_fetched, Action<FetchChore> on_cancelled)
			{
				this = default(FetchAreaChore.StatesInstance.Delivery);
				this.chore = context.chore as FetchChore;
				this.amount = this.chore.originalAmount;
				this.destination = this.chore.destination;
				this.chore.SetOverrideTarget(context.consumerState.consumer);
				this.onCancelled = on_cancelled;
				this.onFetchChoreCleanup = new Action<Chore>(this.OnFetchChoreCleanup);
				this.chore.FetchAreaBegin(context, amount_to_be_fetched);
				FetchChore chore = this.chore;
				chore.onCleanup = (Action<Chore>)Delegate.Combine(chore.onCleanup, this.onFetchChoreCleanup);
			}

			// Token: 0x0600C310 RID: 49936 RVA: 0x0040B638 File Offset: 0x00409838
			public void Complete(List<Pickupable> deliverables)
			{
				using (new KProfiler.Region("FAC.Delivery.Complete", null))
				{
					if (!(this.destination == null) && !this.destination.IsEndOfLife())
					{
						FetchChore chore = this.chore;
						chore.onCleanup = (Action<Chore>)Delegate.Remove(chore.onCleanup, this.onFetchChoreCleanup);
						float num = this.amount;
						Pickupable pickupable = null;
						int num2 = 0;
						while (num2 < deliverables.Count && num > 0f)
						{
							if (deliverables[num2] == null)
							{
								if (num < PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
								{
									this.destination.ForceStore(this.chore.tagsFirst, num);
								}
							}
							else if (!FetchAreaChore.IsPickupableStillValidForChore(deliverables[num2], this.chore))
							{
								global::Debug.LogError(string.Format("Attempting to store {0} in a {1} which did not request it", deliverables[num2], this.destination));
							}
							else
							{
								Pickupable pickupable2 = deliverables[num2].Take(num);
								if (pickupable2 != null && pickupable2.FetchTotalAmount > 0f)
								{
									num -= pickupable2.FetchTotalAmount;
									this.destination.Store(pickupable2.gameObject, false, false, true, false);
									pickupable = pickupable2;
									if (pickupable2 == deliverables[num2])
									{
										deliverables[num2] = null;
									}
								}
							}
							num2++;
						}
						if (this.chore.overrideTarget != null)
						{
							this.chore.FetchAreaEnd(this.chore.overrideTarget.GetComponent<ChoreDriver>(), pickupable, true);
						}
						this.chore = null;
					}
				}
			}

			// Token: 0x0600C311 RID: 49937 RVA: 0x0040B7F0 File Offset: 0x004099F0
			private void OnFetchChoreCleanup(Chore chore)
			{
				if (this.onCancelled != null)
				{
					this.onCancelled(chore as FetchChore);
				}
			}

			// Token: 0x0600C312 RID: 49938 RVA: 0x0040B80B File Offset: 0x00409A0B
			public void Cleanup()
			{
				if (this.chore != null)
				{
					FetchChore chore = this.chore;
					chore.onCleanup = (Action<Chore>)Delegate.Remove(chore.onCleanup, this.onFetchChoreCleanup);
					this.chore.FetchAreaEnd(null, null, false);
				}
			}

			// Token: 0x0400AA38 RID: 43576
			private Action<FetchChore> onCancelled;

			// Token: 0x0400AA39 RID: 43577
			private Action<Chore> onFetchChoreCleanup;
		}

		// Token: 0x0200264B RID: 9803
		public struct Reservation
		{
			// Token: 0x17000CCE RID: 3278
			// (get) Token: 0x0600C313 RID: 49939 RVA: 0x0040B844 File Offset: 0x00409A44
			// (set) Token: 0x0600C314 RID: 49940 RVA: 0x0040B84C File Offset: 0x00409A4C
			public float amount { readonly get; private set; }

			// Token: 0x17000CCF RID: 3279
			// (get) Token: 0x0600C315 RID: 49941 RVA: 0x0040B855 File Offset: 0x00409A55
			// (set) Token: 0x0600C316 RID: 49942 RVA: 0x0040B85D File Offset: 0x00409A5D
			public Pickupable pickupable { readonly get; private set; }

			// Token: 0x0600C317 RID: 49943 RVA: 0x0040B868 File Offset: 0x00409A68
			public Reservation(ChoreConsumer consumer, Pickupable pickupable, float reservation_amount)
			{
				this = default(FetchAreaChore.StatesInstance.Reservation);
				if (reservation_amount <= 0f)
				{
					global::Debug.LogError("Invalid amount: " + reservation_amount.ToString());
				}
				this.amount = reservation_amount;
				this.pickupable = pickupable;
				this.handle = pickupable.Reserve("FetchAreaChore", consumer.GetComponent<KPrefabID>().InstanceID, reservation_amount);
			}

			// Token: 0x0600C318 RID: 49944 RVA: 0x0040B8C5 File Offset: 0x00409AC5
			public void Cleanup()
			{
				if (this.pickupable != null)
				{
					this.pickupable.Unreserve("FetchAreaChore", this.handle);
				}
			}

			// Token: 0x0400AA3C RID: 43580
			private int handle;
		}
	}

	// Token: 0x0200127F RID: 4735
	public class States : GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore>
	{
		// Token: 0x060086B6 RID: 34486 RVA: 0x0033F840 File Offset: 0x0033DA40
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.fetching;
			base.Target(this.fetcher);
			this.fetching.DefaultState(this.fetching.next).Enter("ReservePickupables", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.ReservePickupables();
			}).Exit("UnreservePickupables", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.UnreservePickupables();
			})
				.Enter("pickingup-on", delegate(FetchAreaChore.StatesInstance smi)
				{
					smi.pickingup = true;
				})
				.Exit("pickingup-off", delegate(FetchAreaChore.StatesInstance smi)
				{
					smi.pickingup = false;
				});
			this.fetching.next.Enter("SetupFetch", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.SetupFetch();
			});
			this.fetching.movetopickupable.InitializeStates(new Func<FetchAreaChore.StatesInstance, NavTactic>(this.GetNavTactic), this.fetcher, this.fetchTarget, this.fetching.pickup, this.fetching.fetchfail, null).Target(this.fetchTarget).EventHandlerTransition(GameHashes.TagsChanged, this.fetching.fetchfail, (FetchAreaChore.StatesInstance smi, object obj) => smi.RootChore_ValidateRequiredTagOnTagChange && smi.RootChore_RequiredTag.IsValid && !this.fetchTarget.Get(smi).HasTag(smi.RootChore_RequiredTag))
				.Target(this.fetcher);
			this.fetching.pickup.DoPickup(this.fetchTarget, this.fetchResultTarget, this.fetchAmount, this.fetching.fetchcomplete, this.fetching.fetchfail).Exit(delegate(FetchAreaChore.StatesInstance smi)
			{
				GameObject gameObject = smi.sm.fetchTarget.Get(smi);
				if (gameObject != null)
				{
					gameObject.Unsubscribe(1122777325, new Action<object>(smi.OnMarkForMove));
				}
			});
			this.fetching.fetchcomplete.Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.FetchComplete();
			});
			this.fetching.fetchfail.Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.FetchFail();
			});
			this.delivering.DefaultState(this.delivering.next).OnSignal(this.currentdeliverycancelled, this.delivering.deliverfail).Enter("SetupDeliverables", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.SetupDeliverables();
			})
				.Enter("delivering-on", delegate(FetchAreaChore.StatesInstance smi)
				{
					smi.delivering = true;
				})
				.Exit("delivering-off", delegate(FetchAreaChore.StatesInstance smi)
				{
					smi.delivering = false;
				});
			this.delivering.next.Enter("SetupDelivery", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.SetupDelivery();
			});
			this.delivering.movetostorage.InitializeStates(new Func<FetchAreaChore.StatesInstance, NavTactic>(this.GetNavTactic), this.fetcher, this.deliveryDestination, this.delivering.storing, this.delivering.deliverfail, null).Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				if (this.deliveryObject.Get(smi) != null && this.deliveryObject.Get(smi).GetComponent<MinionIdentity>() != null)
				{
					this.deliveryObject.Get(smi).transform.SetLocalPosition(Vector3.zero);
					KBatchedAnimTracker component = this.deliveryObject.Get(smi).GetComponent<KBatchedAnimTracker>();
					component.symbol = new HashedString("snapTo_chest");
					component.offset = new Vector3(0f, 0f, 1f);
				}
			});
			this.delivering.storing.DoDelivery(this.fetcher, this.deliveryDestination, this.delivering.delivercomplete, this.delivering.deliverfail);
			this.delivering.deliverfail.Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.DeliverFail();
			});
			this.delivering.delivercomplete.Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.DeliverComplete();
			});
		}

		// Token: 0x060086B7 RID: 34487 RVA: 0x0033FC48 File Offset: 0x0033DE48
		private NavTactic GetNavTactic(FetchAreaChore.StatesInstance smi)
		{
			WorkerBase component = this.fetcher.Get(smi).GetComponent<WorkerBase>();
			if (component != null && component.IsFetchDrone())
			{
				return NavigationTactics.FetchDronePickup;
			}
			return NavigationTactics.ReduceTravelDistance;
		}

		// Token: 0x0400667E RID: 26238
		public FetchAreaChore.States.FetchStates fetching;

		// Token: 0x0400667F RID: 26239
		public FetchAreaChore.States.DeliverStates delivering;

		// Token: 0x04006680 RID: 26240
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter fetcher;

		// Token: 0x04006681 RID: 26241
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter fetchTarget;

		// Token: 0x04006682 RID: 26242
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter fetchResultTarget;

		// Token: 0x04006683 RID: 26243
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.FloatParameter fetchAmount;

		// Token: 0x04006684 RID: 26244
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter deliveryDestination;

		// Token: 0x04006685 RID: 26245
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter deliveryObject;

		// Token: 0x04006686 RID: 26246
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.FloatParameter deliveryAmount;

		// Token: 0x04006687 RID: 26247
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.Signal currentdeliverycancelled;

		// Token: 0x0200264E RID: 9806
		public class FetchStates : GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State
		{
			// Token: 0x0400AA45 RID: 43589
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State next;

			// Token: 0x0400AA46 RID: 43590
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.ApproachSubState<Pickupable> movetopickupable;

			// Token: 0x0400AA47 RID: 43591
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State pickup;

			// Token: 0x0400AA48 RID: 43592
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State fetchfail;

			// Token: 0x0400AA49 RID: 43593
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State fetchcomplete;
		}

		// Token: 0x0200264F RID: 9807
		public class DeliverStates : GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State
		{
			// Token: 0x0400AA4A RID: 43594
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State next;

			// Token: 0x0400AA4B RID: 43595
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.ApproachSubState<Storage> movetostorage;

			// Token: 0x0400AA4C RID: 43596
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State storing;

			// Token: 0x0400AA4D RID: 43597
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State deliverfail;

			// Token: 0x0400AA4E RID: 43598
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State delivercomplete;
		}
	}
}
