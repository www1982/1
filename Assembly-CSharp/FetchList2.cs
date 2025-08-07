using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200091B RID: 2331
public class FetchList2 : IFetchList
{
	// Token: 0x170004A2 RID: 1186
	// (get) Token: 0x060040D5 RID: 16597 RVA: 0x0016C354 File Offset: 0x0016A554
	// (set) Token: 0x060040D6 RID: 16598 RVA: 0x0016C35C File Offset: 0x0016A55C
	public bool ShowStatusItem
	{
		get
		{
			return this.bShowStatusItem;
		}
		set
		{
			this.bShowStatusItem = value;
		}
	}

	// Token: 0x170004A3 RID: 1187
	// (get) Token: 0x060040D7 RID: 16599 RVA: 0x0016C365 File Offset: 0x0016A565
	public bool IsComplete
	{
		get
		{
			return this.FetchOrders.Count == 0;
		}
	}

	// Token: 0x170004A4 RID: 1188
	// (get) Token: 0x060040D8 RID: 16600 RVA: 0x0016C378 File Offset: 0x0016A578
	public bool InProgress
	{
		get
		{
			if (this.FetchOrders.Count < 0)
			{
				return false;
			}
			bool flag = false;
			using (List<FetchOrder2>.Enumerator enumerator = this.FetchOrders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.InProgress)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}
	}

	// Token: 0x170004A5 RID: 1189
	// (get) Token: 0x060040D9 RID: 16601 RVA: 0x0016C3E4 File Offset: 0x0016A5E4
	// (set) Token: 0x060040DA RID: 16602 RVA: 0x0016C3EC File Offset: 0x0016A5EC
	public Storage Destination { get; private set; }

	// Token: 0x170004A6 RID: 1190
	// (get) Token: 0x060040DB RID: 16603 RVA: 0x0016C3F5 File Offset: 0x0016A5F5
	// (set) Token: 0x060040DC RID: 16604 RVA: 0x0016C3FD File Offset: 0x0016A5FD
	public int PriorityMod { get; private set; }

	// Token: 0x060040DD RID: 16605 RVA: 0x0016C408 File Offset: 0x0016A608
	public FetchList2(Storage destination, ChoreType chore_type)
	{
		this.Destination = destination;
		this.choreType = chore_type;
	}

	// Token: 0x060040DE RID: 16606 RVA: 0x0016C474 File Offset: 0x0016A674
	public void SetPriorityMod(int priorityMod)
	{
		this.PriorityMod = priorityMod;
		for (int i = 0; i < this.FetchOrders.Count; i++)
		{
			this.FetchOrders[i].SetPriorityMod(this.PriorityMod);
		}
	}

	// Token: 0x060040DF RID: 16607 RVA: 0x0016C4B8 File Offset: 0x0016A6B8
	public void Add(HashSet<Tag> tags, Tag requiredTag, Tag[] forbidden_tags = null, float amount = 1f, Operational.State operationalRequirementDEPRECATED = Operational.State.None)
	{
		foreach (Tag tag in tags)
		{
			if (!this.MinimumAmount.ContainsKey(tag))
			{
				this.MinimumAmount[tag] = amount;
			}
		}
		FetchOrder2 fetchOrder = new FetchOrder2(this.choreType, tags, FetchChore.MatchCriteria.MatchID, requiredTag, forbidden_tags, this.Destination, amount, operationalRequirementDEPRECATED, this.PriorityMod);
		this.FetchOrders.Add(fetchOrder);
	}

	// Token: 0x060040E0 RID: 16608 RVA: 0x0016C548 File Offset: 0x0016A748
	public void Add(HashSet<Tag> tags, Tag[] forbidden_tags = null, float amount = 1f, Operational.State operationalRequirementDEPRECATED = Operational.State.None)
	{
		foreach (Tag tag in tags)
		{
			if (!this.MinimumAmount.ContainsKey(tag))
			{
				this.MinimumAmount[tag] = amount;
			}
		}
		FetchOrder2 fetchOrder = new FetchOrder2(this.choreType, tags, FetchChore.MatchCriteria.MatchID, Tag.Invalid, forbidden_tags, this.Destination, amount, operationalRequirementDEPRECATED, this.PriorityMod);
		this.FetchOrders.Add(fetchOrder);
	}

	// Token: 0x060040E1 RID: 16609 RVA: 0x0016C5DC File Offset: 0x0016A7DC
	public void Add(Tag tag, Tag[] forbidden_tags = null, float amount = 1f, Operational.State operationalRequirementDEPRECATED = Operational.State.None)
	{
		amount = FetchChore.GetMinimumFetchAmount(tag, amount);
		if (!this.MinimumAmount.ContainsKey(tag))
		{
			this.MinimumAmount[tag] = amount;
		}
		FetchOrder2 fetchOrder = new FetchOrder2(this.choreType, new HashSet<Tag> { tag }, FetchChore.MatchCriteria.MatchTags, Tag.Invalid, forbidden_tags, this.Destination, amount, operationalRequirementDEPRECATED, this.PriorityMod);
		this.FetchOrders.Add(fetchOrder);
	}

	// Token: 0x060040E2 RID: 16610 RVA: 0x0016C648 File Offset: 0x0016A848
	public float GetMinimumAmount(Tag tag)
	{
		float num = 0f;
		this.MinimumAmount.TryGetValue(tag, out num);
		return num;
	}

	// Token: 0x060040E3 RID: 16611 RVA: 0x0016C66B File Offset: 0x0016A86B
	private void OnFetchOrderComplete(FetchOrder2 fetch_order, Pickupable fetched_item)
	{
		this.FetchOrders.Remove(fetch_order);
		if (this.FetchOrders.Count == 0)
		{
			if (this.OnComplete != null)
			{
				this.OnComplete();
			}
			FetchListStatusItemUpdater.instance.RemoveFetchList(this);
			this.ClearStatus();
		}
	}

	// Token: 0x060040E4 RID: 16612 RVA: 0x0016C6AC File Offset: 0x0016A8AC
	public void Cancel(string reason)
	{
		FetchListStatusItemUpdater.instance.RemoveFetchList(this);
		this.ClearStatus();
		foreach (FetchOrder2 fetchOrder in this.FetchOrders)
		{
			fetchOrder.Cancel(reason);
		}
	}

	// Token: 0x060040E5 RID: 16613 RVA: 0x0016C710 File Offset: 0x0016A910
	public void UpdateRemaining()
	{
		this.Remaining.Clear();
		for (int i = 0; i < this.FetchOrders.Count; i++)
		{
			FetchOrder2 fetchOrder = this.FetchOrders[i];
			foreach (Tag tag in fetchOrder.Tags)
			{
				float num = 0f;
				this.Remaining.TryGetValue(tag, out num);
				this.Remaining[tag] = num + fetchOrder.AmountWaitingToFetch();
			}
		}
	}

	// Token: 0x060040E6 RID: 16614 RVA: 0x0016C7B8 File Offset: 0x0016A9B8
	public Dictionary<Tag, float> GetRemaining()
	{
		return this.Remaining;
	}

	// Token: 0x060040E7 RID: 16615 RVA: 0x0016C7C0 File Offset: 0x0016A9C0
	public Dictionary<Tag, float> GetRemainingMinimum()
	{
		Dictionary<Tag, float> dictionary = new Dictionary<Tag, float>();
		foreach (FetchOrder2 fetchOrder in this.FetchOrders)
		{
			foreach (Tag tag in fetchOrder.Tags)
			{
				dictionary[tag] = this.MinimumAmount[tag];
			}
		}
		foreach (GameObject gameObject in this.Destination.items)
		{
			if (gameObject != null)
			{
				Pickupable component = gameObject.GetComponent<Pickupable>();
				if (component != null)
				{
					KPrefabID kprefabID = component.KPrefabID;
					if (dictionary.ContainsKey(kprefabID.PrefabTag))
					{
						dictionary[kprefabID.PrefabTag] = Math.Max(dictionary[kprefabID.PrefabTag] - component.FetchTotalAmount, 0f);
					}
					foreach (Tag tag2 in kprefabID.Tags)
					{
						if (dictionary.ContainsKey(tag2))
						{
							dictionary[tag2] = Math.Max(dictionary[tag2] - component.FetchTotalAmount, 0f);
						}
					}
				}
			}
		}
		return dictionary;
	}

	// Token: 0x060040E8 RID: 16616 RVA: 0x0016C978 File Offset: 0x0016AB78
	public void Suspend(string reason)
	{
		foreach (FetchOrder2 fetchOrder in this.FetchOrders)
		{
			fetchOrder.Suspend(reason);
		}
	}

	// Token: 0x060040E9 RID: 16617 RVA: 0x0016C9CC File Offset: 0x0016ABCC
	public void Resume(string reason)
	{
		foreach (FetchOrder2 fetchOrder in this.FetchOrders)
		{
			fetchOrder.Resume(reason);
		}
	}

	// Token: 0x060040EA RID: 16618 RVA: 0x0016CA20 File Offset: 0x0016AC20
	public void Submit(global::System.Action on_complete, bool check_storage_contents)
	{
		this.OnComplete = on_complete;
		foreach (FetchOrder2 fetchOrder in this.FetchOrders.GetRange(0, this.FetchOrders.Count))
		{
			fetchOrder.Submit(new Action<FetchOrder2, Pickupable>(this.OnFetchOrderComplete), check_storage_contents, null);
		}
		if (!this.IsComplete && this.ShowStatusItem)
		{
			FetchListStatusItemUpdater.instance.AddFetchList(this);
		}
	}

	// Token: 0x060040EB RID: 16619 RVA: 0x0016CAB4 File Offset: 0x0016ACB4
	private void ClearStatus()
	{
		if (this.Destination != null)
		{
			KSelectable component = this.Destination.GetComponent<KSelectable>();
			if (component != null)
			{
				this.waitingForMaterialsHandle = component.RemoveStatusItem(this.waitingForMaterialsHandle, false);
				this.materialsUnavailableHandle = component.RemoveStatusItem(this.materialsUnavailableHandle, false);
				this.materialsUnavailableForRefillHandle = component.RemoveStatusItem(this.materialsUnavailableForRefillHandle, false);
			}
		}
	}

	// Token: 0x060040EC RID: 16620 RVA: 0x0016CB20 File Offset: 0x0016AD20
	public void UpdateStatusItem(MaterialsStatusItem status_item, ref Guid handle, bool should_add)
	{
		bool flag = handle != Guid.Empty;
		if (should_add != flag)
		{
			if (should_add)
			{
				KSelectable component = this.Destination.GetComponent<KSelectable>();
				if (component != null)
				{
					handle = component.AddStatusItem(status_item, this);
					GameScheduler.Instance.Schedule("Digging Tutorial", 2f, delegate(object obj)
					{
						Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Digging, true);
					}, null, null);
					return;
				}
			}
			else
			{
				KSelectable component2 = this.Destination.GetComponent<KSelectable>();
				if (component2 != null)
				{
					handle = component2.RemoveStatusItem(handle, false);
				}
			}
		}
	}

	// Token: 0x04002891 RID: 10385
	private global::System.Action OnComplete;

	// Token: 0x04002894 RID: 10388
	private ChoreType choreType;

	// Token: 0x04002895 RID: 10389
	public Guid waitingForMaterialsHandle = Guid.Empty;

	// Token: 0x04002896 RID: 10390
	public Guid materialsUnavailableForRefillHandle = Guid.Empty;

	// Token: 0x04002897 RID: 10391
	public Guid materialsUnavailableHandle = Guid.Empty;

	// Token: 0x04002898 RID: 10392
	public Dictionary<Tag, float> MinimumAmount = new Dictionary<Tag, float>();

	// Token: 0x04002899 RID: 10393
	public List<FetchOrder2> FetchOrders = new List<FetchOrder2>();

	// Token: 0x0400289A RID: 10394
	private Dictionary<Tag, float> Remaining = new Dictionary<Tag, float>();

	// Token: 0x0400289B RID: 10395
	private bool bShowStatusItem = true;
}
