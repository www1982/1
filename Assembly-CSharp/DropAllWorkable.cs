using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020008C9 RID: 2249
[AddComponentMenu("KMonoBehaviour/Workable/DropAllWorkable")]
public class DropAllWorkable : Workable
{
	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x06003E53 RID: 15955 RVA: 0x0015C9D5 File Offset: 0x0015ABD5
	// (set) Token: 0x06003E54 RID: 15956 RVA: 0x0015C9DD File Offset: 0x0015ABDD
	private Chore Chore
	{
		get
		{
			return this._chore;
		}
		set
		{
			this._chore = value;
			this.markedForDrop = this._chore != null;
		}
	}

	// Token: 0x06003E55 RID: 15957 RVA: 0x0015C9F5 File Offset: 0x0015ABF5
	protected DropAllWorkable()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06003E56 RID: 15958 RVA: 0x0015CA14 File Offset: 0x0015AC14
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<DropAllWorkable>(493375141, DropAllWorkable.OnRefreshUserMenuDelegate);
		base.Subscribe<DropAllWorkable>(-1697596308, DropAllWorkable.OnStorageChangeDelegate);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Emptying;
		this.synchronizeAnims = false;
		base.SetWorkTime(this.dropWorkTime);
		Prioritizable.AddRef(base.gameObject);
	}

	// Token: 0x06003E57 RID: 15959 RVA: 0x0015CA7C File Offset: 0x0015AC7C
	private Storage[] GetStorages()
	{
		if (this.storages == null)
		{
			this.storages = base.GetComponents<Storage>();
		}
		return this.storages;
	}

	// Token: 0x06003E58 RID: 15960 RVA: 0x0015CA98 File Offset: 0x0015AC98
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.showCmd = this.GetNewShowCmd();
		if (this.markedForDrop)
		{
			this.DropAll();
		}
	}

	// Token: 0x06003E59 RID: 15961 RVA: 0x0015CABC File Offset: 0x0015ACBC
	public void DropAll()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.OnCompleteWork(null);
		}
		else if (this.Chore == null)
		{
			ChoreType choreType = ((!string.IsNullOrEmpty(this.choreTypeID)) ? Db.Get().ChoreTypes.Get(this.choreTypeID) : Db.Get().ChoreTypes.EmptyStorage);
			this.Chore = new WorkChore<DropAllWorkable>(choreType, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}
		else
		{
			this.Chore.Cancel("Cancelled emptying");
			this.Chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(this.workerStatusItem, false);
			base.ShowProgressBar(false);
		}
		this.RefreshStatusItem();
	}

	// Token: 0x06003E5A RID: 15962 RVA: 0x0015CB70 File Offset: 0x0015AD70
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Storage[] array = this.GetStorages();
		for (int i = 0; i < array.Length; i++)
		{
			List<GameObject> list = new List<GameObject>(array[i].items);
			for (int j = 0; j < list.Count; j++)
			{
				GameObject gameObject = array[i].Drop(list[j], true);
				if (gameObject != null)
				{
					foreach (Tag tag in this.removeTags)
					{
						gameObject.RemoveTag(tag);
					}
					gameObject.Trigger(580035959, worker);
					if (this.resetTargetWorkableOnCompleteWork)
					{
						Pickupable component = gameObject.GetComponent<Pickupable>();
						component.targetWorkable = component;
						component.SetOffsetTable(OffsetGroups.InvertedStandardTable);
					}
				}
			}
		}
		this.Chore = null;
		this.RefreshStatusItem();
		base.Trigger(-1957399615, null);
	}

	// Token: 0x06003E5B RID: 15963 RVA: 0x0015CC6C File Offset: 0x0015AE6C
	private void OnRefreshUserMenu(object data)
	{
		if (this.showCmd)
		{
			KIconButtonMenu.ButtonInfo buttonInfo = ((this.Chore == null) ? new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.EMPTYSTORAGE.NAME, new global::System.Action(this.DropAll), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.EMPTYSTORAGE.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.EMPTYSTORAGE.NAME_OFF, new global::System.Action(this.DropAll), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.EMPTYSTORAGE.TOOLTIP_OFF, true));
			Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
		}
	}

	// Token: 0x06003E5C RID: 15964 RVA: 0x0015CD10 File Offset: 0x0015AF10
	private bool GetNewShowCmd()
	{
		bool flag = false;
		Storage[] array = this.GetStorages();
		for (int i = 0; i < array.Length; i++)
		{
			flag = flag || !array[i].IsEmpty();
		}
		return flag;
	}

	// Token: 0x06003E5D RID: 15965 RVA: 0x0015CD48 File Offset: 0x0015AF48
	private void OnStorageChange(object data)
	{
		bool newShowCmd = this.GetNewShowCmd();
		if (newShowCmd != this.showCmd)
		{
			this.showCmd = newShowCmd;
			Game.Instance.userMenu.Refresh(base.gameObject);
		}
	}

	// Token: 0x06003E5E RID: 15966 RVA: 0x0015CD84 File Offset: 0x0015AF84
	private void RefreshStatusItem()
	{
		if (this.Chore != null && this.statusItem == Guid.Empty)
		{
			KSelectable component = base.GetComponent<KSelectable>();
			this.statusItem = component.AddStatusItem(Db.Get().BuildingStatusItems.AwaitingEmptyBuilding, null);
			return;
		}
		if (this.Chore == null && this.statusItem != Guid.Empty)
		{
			KSelectable component2 = base.GetComponent<KSelectable>();
			this.statusItem = component2.RemoveStatusItem(this.statusItem, false);
		}
	}

	// Token: 0x0400265B RID: 9819
	[Serialize]
	private bool markedForDrop;

	// Token: 0x0400265C RID: 9820
	private Chore _chore;

	// Token: 0x0400265D RID: 9821
	private bool showCmd;

	// Token: 0x0400265E RID: 9822
	private Storage[] storages;

	// Token: 0x0400265F RID: 9823
	public float dropWorkTime = 0.1f;

	// Token: 0x04002660 RID: 9824
	public string choreTypeID;

	// Token: 0x04002661 RID: 9825
	[MyCmpAdd]
	private Prioritizable _prioritizable;

	// Token: 0x04002662 RID: 9826
	public List<Tag> removeTags;

	// Token: 0x04002663 RID: 9827
	public bool resetTargetWorkableOnCompleteWork;

	// Token: 0x04002664 RID: 9828
	private static readonly EventSystem.IntraObjectHandler<DropAllWorkable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<DropAllWorkable>(delegate(DropAllWorkable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04002665 RID: 9829
	private static readonly EventSystem.IntraObjectHandler<DropAllWorkable> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<DropAllWorkable>(delegate(DropAllWorkable component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04002666 RID: 9830
	private Guid statusItem;
}
