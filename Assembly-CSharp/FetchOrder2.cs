using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200091E RID: 2334
public class FetchOrder2
{
	// Token: 0x170004A7 RID: 1191
	// (get) Token: 0x06004106 RID: 16646 RVA: 0x0016DA58 File Offset: 0x0016BC58
	// (set) Token: 0x06004107 RID: 16647 RVA: 0x0016DA60 File Offset: 0x0016BC60
	public float TotalAmount { get; set; }

	// Token: 0x170004A8 RID: 1192
	// (get) Token: 0x06004108 RID: 16648 RVA: 0x0016DA69 File Offset: 0x0016BC69
	// (set) Token: 0x06004109 RID: 16649 RVA: 0x0016DA71 File Offset: 0x0016BC71
	public int PriorityMod { get; set; }

	// Token: 0x170004A9 RID: 1193
	// (get) Token: 0x0600410A RID: 16650 RVA: 0x0016DA7A File Offset: 0x0016BC7A
	// (set) Token: 0x0600410B RID: 16651 RVA: 0x0016DA82 File Offset: 0x0016BC82
	public HashSet<Tag> Tags { get; protected set; }

	// Token: 0x170004AA RID: 1194
	// (get) Token: 0x0600410C RID: 16652 RVA: 0x0016DA8B File Offset: 0x0016BC8B
	// (set) Token: 0x0600410D RID: 16653 RVA: 0x0016DA93 File Offset: 0x0016BC93
	public FetchChore.MatchCriteria Criteria { get; protected set; }

	// Token: 0x170004AB RID: 1195
	// (get) Token: 0x0600410E RID: 16654 RVA: 0x0016DA9C File Offset: 0x0016BC9C
	// (set) Token: 0x0600410F RID: 16655 RVA: 0x0016DAA4 File Offset: 0x0016BCA4
	public Tag RequiredTag { get; protected set; }

	// Token: 0x170004AC RID: 1196
	// (get) Token: 0x06004110 RID: 16656 RVA: 0x0016DAAD File Offset: 0x0016BCAD
	// (set) Token: 0x06004111 RID: 16657 RVA: 0x0016DAB5 File Offset: 0x0016BCB5
	public Tag[] ForbiddenTags { get; protected set; }

	// Token: 0x170004AD RID: 1197
	// (get) Token: 0x06004112 RID: 16658 RVA: 0x0016DABE File Offset: 0x0016BCBE
	// (set) Token: 0x06004113 RID: 16659 RVA: 0x0016DAC6 File Offset: 0x0016BCC6
	public Storage Destination { get; set; }

	// Token: 0x170004AE RID: 1198
	// (get) Token: 0x06004114 RID: 16660 RVA: 0x0016DACF File Offset: 0x0016BCCF
	// (set) Token: 0x06004115 RID: 16661 RVA: 0x0016DAD7 File Offset: 0x0016BCD7
	private float UnfetchedAmount
	{
		get
		{
			return this._UnfetchedAmount;
		}
		set
		{
			this._UnfetchedAmount = value;
			this.Assert(this._UnfetchedAmount <= this.TotalAmount, "_UnfetchedAmount <= TotalAmount");
			this.Assert(this._UnfetchedAmount >= 0f, "_UnfetchedAmount >= 0");
		}
	}

	// Token: 0x06004116 RID: 16662 RVA: 0x0016DB18 File Offset: 0x0016BD18
	public FetchOrder2(ChoreType chore_type, HashSet<Tag> tags, FetchChore.MatchCriteria criteria, Tag required_tag, Tag[] forbidden_tags, Storage destination, float amount, Operational.State operationalRequirementDEPRECATED = Operational.State.None, int priorityMod = 0)
	{
		if (amount <= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
		{
			DebugUtil.LogWarningArgs(new object[] { string.Format("FetchOrder2 {0} is requesting {1} {2} to {3}", new object[]
			{
				chore_type.Id,
				tags,
				amount,
				(destination != null) ? destination.name : "to nowhere"
			}) });
		}
		this.choreType = chore_type;
		this.Tags = tags;
		this.Criteria = criteria;
		this.RequiredTag = required_tag;
		this.ForbiddenTags = forbidden_tags;
		this.Destination = destination;
		this.TotalAmount = amount;
		this.UnfetchedAmount = amount;
		this.PriorityMod = priorityMod;
		this.operationalRequirement = operationalRequirementDEPRECATED;
	}

	// Token: 0x170004AF RID: 1199
	// (get) Token: 0x06004117 RID: 16663 RVA: 0x0016DBE4 File Offset: 0x0016BDE4
	public bool InProgress
	{
		get
		{
			bool flag = false;
			using (List<FetchChore>.Enumerator enumerator = this.Chores.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.InProgress())
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}
	}

	// Token: 0x06004118 RID: 16664 RVA: 0x0016DC40 File Offset: 0x0016BE40
	private void IssueTask()
	{
		if (this.UnfetchedAmount > 0f)
		{
			this.SetFetchTask(this.UnfetchedAmount);
			this.UnfetchedAmount = 0f;
		}
	}

	// Token: 0x06004119 RID: 16665 RVA: 0x0016DC68 File Offset: 0x0016BE68
	public void SetPriorityMod(int priorityMod)
	{
		this.PriorityMod = priorityMod;
		for (int i = 0; i < this.Chores.Count; i++)
		{
			this.Chores[i].SetPriorityMod(this.PriorityMod);
		}
	}

	// Token: 0x0600411A RID: 16666 RVA: 0x0016DCAC File Offset: 0x0016BEAC
	private void SetFetchTask(float amount)
	{
		FetchChore fetchChore = new FetchChore(this.choreType, this.Destination, amount, this.Tags, this.Criteria, this.RequiredTag, this.ForbiddenTags, null, true, new Action<Chore>(this.OnFetchChoreComplete), new Action<Chore>(this.OnFetchChoreBegin), new Action<Chore>(this.OnFetchChoreEnd), this.operationalRequirement, this.PriorityMod);
		fetchChore.validateRequiredTagOnTagChange = this.validateRequiredTagOnTagChange;
		this.Chores.Add(fetchChore);
	}

	// Token: 0x0600411B RID: 16667 RVA: 0x0016DD30 File Offset: 0x0016BF30
	private void OnFetchChoreEnd(Chore chore)
	{
		FetchChore fetchChore = (FetchChore)chore;
		if (this.Chores.Contains(fetchChore))
		{
			this.UnfetchedAmount += fetchChore.amount;
			fetchChore.Cancel("FetchChore Redistribution");
			this.Chores.Remove(fetchChore);
			this.IssueTask();
		}
	}

	// Token: 0x0600411C RID: 16668 RVA: 0x0016DD84 File Offset: 0x0016BF84
	private void OnFetchChoreComplete(Chore chore)
	{
		FetchChore fetchChore = (FetchChore)chore;
		this.Chores.Remove(fetchChore);
		if (this.Chores.Count == 0 && this.OnComplete != null)
		{
			this.OnComplete(this, fetchChore.fetchTarget);
		}
	}

	// Token: 0x0600411D RID: 16669 RVA: 0x0016DDCC File Offset: 0x0016BFCC
	private void OnFetchChoreBegin(Chore chore)
	{
		FetchChore fetchChore = (FetchChore)chore;
		this.UnfetchedAmount += fetchChore.originalAmount - fetchChore.amount;
		this.IssueTask();
		if (this.OnBegin != null)
		{
			this.OnBegin(this, fetchChore.fetchTarget);
		}
	}

	// Token: 0x0600411E RID: 16670 RVA: 0x0016DE1C File Offset: 0x0016C01C
	public void Cancel(string reason)
	{
		while (this.Chores.Count > 0)
		{
			FetchChore fetchChore = this.Chores[0];
			fetchChore.Cancel(reason);
			this.Chores.Remove(fetchChore);
		}
	}

	// Token: 0x0600411F RID: 16671 RVA: 0x0016DE5A File Offset: 0x0016C05A
	public void Suspend(string reason)
	{
		global::Debug.LogError("UNIMPLEMENTED!");
	}

	// Token: 0x06004120 RID: 16672 RVA: 0x0016DE66 File Offset: 0x0016C066
	public void Resume(string reason)
	{
		global::Debug.LogError("UNIMPLEMENTED!");
	}

	// Token: 0x06004121 RID: 16673 RVA: 0x0016DE74 File Offset: 0x0016C074
	public void Submit(Action<FetchOrder2, Pickupable> on_complete, bool check_storage_contents, Action<FetchOrder2, Pickupable> on_begin = null)
	{
		this.OnComplete = on_complete;
		this.OnBegin = on_begin;
		this.checkStorageContents = check_storage_contents;
		if (check_storage_contents)
		{
			Pickupable pickupable = null;
			this.UnfetchedAmount = this.GetRemaining(out pickupable);
			if (this.UnfetchedAmount > this.Destination.storageFullMargin)
			{
				this.IssueTask();
				return;
			}
			if (this.OnComplete != null)
			{
				this.OnComplete(this, pickupable);
				return;
			}
		}
		else
		{
			this.IssueTask();
		}
	}

	// Token: 0x06004122 RID: 16674 RVA: 0x0016DEE0 File Offset: 0x0016C0E0
	public bool IsMaterialOnStorage(Storage storage, ref float amount, ref Pickupable out_item)
	{
		foreach (GameObject gameObject in this.Destination.items)
		{
			if (gameObject != null)
			{
				Pickupable component = gameObject.GetComponent<Pickupable>();
				if (component != null)
				{
					KPrefabID kprefabID = component.KPrefabID;
					foreach (Tag tag in this.Tags)
					{
						if (kprefabID.HasTag(tag))
						{
							amount = component.FetchTotalAmount;
							out_item = component;
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06004123 RID: 16675 RVA: 0x0016DFAC File Offset: 0x0016C1AC
	public float AmountWaitingToFetch()
	{
		if (!this.checkStorageContents)
		{
			float num = this.UnfetchedAmount;
			for (int i = 0; i < this.Chores.Count; i++)
			{
				num += this.Chores[i].AmountWaitingToFetch();
			}
			return num;
		}
		Pickupable pickupable;
		return this.GetRemaining(out pickupable);
	}

	// Token: 0x06004124 RID: 16676 RVA: 0x0016DFFC File Offset: 0x0016C1FC
	public float GetRemaining(out Pickupable out_item)
	{
		float num = this.TotalAmount;
		float num2 = 0f;
		out_item = null;
		if (this.IsMaterialOnStorage(this.Destination, ref num2, ref out_item))
		{
			num = Math.Max(num - num2, 0f);
		}
		return num;
	}

	// Token: 0x06004125 RID: 16677 RVA: 0x0016E03C File Offset: 0x0016C23C
	public bool IsComplete()
	{
		for (int i = 0; i < this.Chores.Count; i++)
		{
			if (!this.Chores[i].isComplete)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004126 RID: 16678 RVA: 0x0016E078 File Offset: 0x0016C278
	private void Assert(bool condition, string message)
	{
		if (condition)
		{
			return;
		}
		string text = "FetchOrder error: " + message;
		if (this.Destination == null)
		{
			text += "\nDestination: None";
		}
		else
		{
			text = text + "\nDestination: " + this.Destination.name;
		}
		text = text + "\nTotal Amount: " + this.TotalAmount.ToString();
		text = text + "\nUnfetched Amount: " + this._UnfetchedAmount.ToString();
		global::Debug.LogError(text);
	}

	// Token: 0x040028A4 RID: 10404
	public Action<FetchOrder2, Pickupable> OnComplete;

	// Token: 0x040028A5 RID: 10405
	public Action<FetchOrder2, Pickupable> OnBegin;

	// Token: 0x040028AA RID: 10410
	public bool validateRequiredTagOnTagChange;

	// Token: 0x040028AE RID: 10414
	public List<FetchChore> Chores = new List<FetchChore>();

	// Token: 0x040028AF RID: 10415
	private ChoreType choreType;

	// Token: 0x040028B0 RID: 10416
	private float _UnfetchedAmount;

	// Token: 0x040028B1 RID: 10417
	private bool checkStorageContents;

	// Token: 0x040028B2 RID: 10418
	private Operational.State operationalRequirement = Operational.State.None;
}
