using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020009BE RID: 2494
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ManualDeliveryKG")]
public class ManualDeliveryKG : KMonoBehaviour, ISim1000ms
{
	// Token: 0x17000513 RID: 1299
	// (get) Token: 0x060048C5 RID: 18629 RVA: 0x001A3D19 File Offset: 0x001A1F19
	public bool IsPaused
	{
		get
		{
			return this.paused;
		}
	}

	// Token: 0x17000514 RID: 1300
	// (get) Token: 0x060048C6 RID: 18630 RVA: 0x001A3D21 File Offset: 0x001A1F21
	public float Capacity
	{
		get
		{
			return this.capacity;
		}
	}

	// Token: 0x17000515 RID: 1301
	// (get) Token: 0x060048C7 RID: 18631 RVA: 0x001A3D29 File Offset: 0x001A1F29
	// (set) Token: 0x060048C8 RID: 18632 RVA: 0x001A3D31 File Offset: 0x001A1F31
	public Tag RequestedItemTag
	{
		get
		{
			return this.requestedItemTag;
		}
		set
		{
			this.requestedItemTag = value;
			this.AbortDelivery("Requested Item Tag Changed");
		}
	}

	// Token: 0x17000516 RID: 1302
	// (get) Token: 0x060048C9 RID: 18633 RVA: 0x001A3D45 File Offset: 0x001A1F45
	// (set) Token: 0x060048CA RID: 18634 RVA: 0x001A3D4D File Offset: 0x001A1F4D
	public Tag[] ForbiddenTags
	{
		get
		{
			return this.forbiddenTags;
		}
		set
		{
			this.forbiddenTags = value;
			this.AbortDelivery("Forbidden Tags Changed");
		}
	}

	// Token: 0x17000517 RID: 1303
	// (get) Token: 0x060048CB RID: 18635 RVA: 0x001A3D61 File Offset: 0x001A1F61
	public Storage DebugStorage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x17000518 RID: 1304
	// (get) Token: 0x060048CC RID: 18636 RVA: 0x001A3D69 File Offset: 0x001A1F69
	public FetchList2 DebugFetchList
	{
		get
		{
			return this.fetchList;
		}
	}

	// Token: 0x17000519 RID: 1305
	// (get) Token: 0x060048CD RID: 18637 RVA: 0x001A3D71 File Offset: 0x001A1F71
	private float MassStored
	{
		get
		{
			return this.storage.GetMassAvailable(this.requestedItemTag);
		}
	}

	// Token: 0x060048CE RID: 18638 RVA: 0x001A3D84 File Offset: 0x001A1F84
	protected override void OnSpawn()
	{
		base.OnSpawn();
		DebugUtil.Assert(this.choreTypeIDHash.IsValid, "ManualDeliveryKG Must have a valid chore type specified!", base.name);
		if (this.allowPause)
		{
			base.Subscribe<ManualDeliveryKG>(493375141, ManualDeliveryKG.OnRefreshUserMenuDelegate);
			base.Subscribe<ManualDeliveryKG>(-111137758, ManualDeliveryKG.OnRefreshUserMenuDelegate);
		}
		base.Subscribe<ManualDeliveryKG>(-592767678, ManualDeliveryKG.OnOperationalChangedDelegate);
		if (this.storage != null)
		{
			this.SetStorage(this.storage);
		}
		if (this.handlePrioritizable)
		{
			Prioritizable.AddRef(base.gameObject);
		}
		if (this.userPaused && this.allowPause)
		{
			this.OnPause();
		}
	}

	// Token: 0x060048CF RID: 18639 RVA: 0x001A3E30 File Offset: 0x001A2030
	protected override void OnCleanUp()
	{
		this.AbortDelivery("ManualDeliverKG destroyed");
		if (this.handlePrioritizable)
		{
			Prioritizable.RemoveRef(base.gameObject);
		}
		base.OnCleanUp();
	}

	// Token: 0x060048D0 RID: 18640 RVA: 0x001A3E58 File Offset: 0x001A2058
	public void SetStorage(Storage storage)
	{
		if (this.storage != null)
		{
			this.storage.Unsubscribe(this.onStorageChangeSubscription);
			this.onStorageChangeSubscription = -1;
		}
		this.AbortDelivery("storage pointer changed");
		this.storage = storage;
		if (this.storage != null && base.isSpawned)
		{
			global::Debug.Assert(this.onStorageChangeSubscription == -1);
			this.onStorageChangeSubscription = this.storage.Subscribe<ManualDeliveryKG>(-1697596308, ManualDeliveryKG.OnStorageChangedDelegate);
		}
	}

	// Token: 0x060048D1 RID: 18641 RVA: 0x001A3EDC File Offset: 0x001A20DC
	public void Pause(bool pause, string reason)
	{
		if (this.paused != pause)
		{
			this.paused = pause;
			if (pause)
			{
				this.AbortDelivery(reason);
			}
		}
	}

	// Token: 0x060048D2 RID: 18642 RVA: 0x001A3EF8 File Offset: 0x001A20F8
	public void Sim1000ms(float dt)
	{
		this.UpdateDeliveryState();
	}

	// Token: 0x060048D3 RID: 18643 RVA: 0x001A3F00 File Offset: 0x001A2100
	[ContextMenu("UpdateDeliveryState")]
	public void UpdateDeliveryState()
	{
		if (!this.requestedItemTag.IsValid)
		{
			return;
		}
		if (this.storage == null)
		{
			return;
		}
		this.UpdateFetchList();
	}

	// Token: 0x060048D4 RID: 18644 RVA: 0x001A3F28 File Offset: 0x001A2128
	public void RequestDelivery()
	{
		if (this.fetchList != null)
		{
			return;
		}
		float massStored = this.MassStored;
		if (massStored < this.capacity)
		{
			this.CreateFetchChore(massStored);
		}
	}

	// Token: 0x060048D5 RID: 18645 RVA: 0x001A3F58 File Offset: 0x001A2158
	private void CreateFetchChore(float stored_mass)
	{
		float num = this.capacity - stored_mass;
		num = Mathf.Max(PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT, num);
		if (this.RoundFetchAmountToInt)
		{
			num = (float)((int)num);
			if (num < 0.1f)
			{
				return;
			}
		}
		ChoreType byHash = Db.Get().ChoreTypes.GetByHash(this.choreTypeIDHash);
		this.fetchList = new FetchList2(this.storage, byHash);
		this.fetchList.ShowStatusItem = this.ShowStatusItem;
		this.fetchList.MinimumAmount[this.requestedItemTag] = Mathf.Max(PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT, this.MinimumMass);
		FetchList2 fetchList = this.fetchList;
		Tag tag = this.requestedItemTag;
		float num2 = num;
		fetchList.Add(tag, this.forbiddenTags, num2, Operational.State.None);
		this.fetchList.Submit(new global::System.Action(this.OnFetchComplete), false);
	}

	// Token: 0x060048D6 RID: 18646 RVA: 0x001A4024 File Offset: 0x001A2224
	private void OnFetchComplete()
	{
		if (this.FillToCapacity && this.storage != null)
		{
			float amountAvailable = this.storage.GetAmountAvailable(this.requestedItemTag);
			if (amountAvailable < this.capacity)
			{
				this.CreateFetchChore(amountAvailable);
			}
		}
	}

	// Token: 0x060048D7 RID: 18647 RVA: 0x001A406C File Offset: 0x001A226C
	private void UpdateFetchList()
	{
		if (this.paused)
		{
			return;
		}
		if (this.fetchList != null && this.fetchList.IsComplete)
		{
			this.fetchList = null;
		}
		if (!(this.operational == null) && !this.operational.MeetsRequirements(this.operationalRequirement))
		{
			if (this.fetchList != null)
			{
				this.fetchList.Cancel("Operational requirements");
				this.fetchList = null;
				return;
			}
		}
		else if (this.fetchList == null)
		{
			if (this.MassStored < this.refillMass)
			{
				this.RequestDelivery();
				return;
			}
		}
		else if (this.FillToMinimumMass)
		{
			Dictionary<Tag, float> remaining = this.fetchList.GetRemaining();
			if (remaining.ContainsKey(this.requestedItemTag) && remaining[this.requestedItemTag] < this.MinimumMass)
			{
				this.AbortDelivery("Invalid Mass");
			}
		}
	}

	// Token: 0x060048D8 RID: 18648 RVA: 0x001A413D File Offset: 0x001A233D
	public void AbortDelivery(string reason)
	{
		if (this.fetchList != null)
		{
			FetchList2 fetchList = this.fetchList;
			this.fetchList = null;
			fetchList.Cancel(reason);
		}
	}

	// Token: 0x060048D9 RID: 18649 RVA: 0x001A415A File Offset: 0x001A235A
	protected void OnStorageChanged(object data)
	{
		this.UpdateDeliveryState();
	}

	// Token: 0x060048DA RID: 18650 RVA: 0x001A4162 File Offset: 0x001A2362
	private void OnPause()
	{
		this.userPaused = true;
		this.Pause(true, "Forbid manual delivery");
	}

	// Token: 0x060048DB RID: 18651 RVA: 0x001A4177 File Offset: 0x001A2377
	private void OnResume()
	{
		this.userPaused = false;
		this.Pause(false, "Allow manual delivery");
	}

	// Token: 0x060048DC RID: 18652 RVA: 0x001A418C File Offset: 0x001A238C
	private void OnRefreshUserMenu(object data)
	{
		if (!this.allowPause)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((!this.paused) ? new KIconButtonMenu.ButtonInfo("action_move_to_storage", UI.USERMENUACTIONS.MANUAL_DELIVERY.NAME, new global::System.Action(this.OnPause), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MANUAL_DELIVERY.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_move_to_storage", UI.USERMENUACTIONS.MANUAL_DELIVERY.NAME_OFF, new global::System.Action(this.OnResume), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MANUAL_DELIVERY.TOOLTIP_OFF, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x060048DD RID: 18653 RVA: 0x001A422E File Offset: 0x001A242E
	private void OnOperationalChanged(object data)
	{
		this.UpdateDeliveryState();
	}

	// Token: 0x04002FEC RID: 12268
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002FED RID: 12269
	[SerializeField]
	private Storage storage;

	// Token: 0x04002FEE RID: 12270
	[SerializeField]
	public Tag requestedItemTag;

	// Token: 0x04002FEF RID: 12271
	private Tag[] forbiddenTags;

	// Token: 0x04002FF0 RID: 12272
	[SerializeField]
	public float capacity = 100f;

	// Token: 0x04002FF1 RID: 12273
	[SerializeField]
	public float refillMass = 10f;

	// Token: 0x04002FF2 RID: 12274
	[SerializeField]
	public float MinimumMass = 10f;

	// Token: 0x04002FF3 RID: 12275
	[SerializeField]
	public bool RoundFetchAmountToInt;

	// Token: 0x04002FF4 RID: 12276
	[SerializeField]
	public bool FillToCapacity;

	// Token: 0x04002FF5 RID: 12277
	[SerializeField]
	public Operational.State operationalRequirement;

	// Token: 0x04002FF6 RID: 12278
	[SerializeField]
	public bool allowPause;

	// Token: 0x04002FF7 RID: 12279
	[SerializeField]
	private bool paused;

	// Token: 0x04002FF8 RID: 12280
	[SerializeField]
	public HashedString choreTypeIDHash;

	// Token: 0x04002FF9 RID: 12281
	[Serialize]
	private bool userPaused;

	// Token: 0x04002FFA RID: 12282
	public bool handlePrioritizable = true;

	// Token: 0x04002FFB RID: 12283
	public bool ShowStatusItem = true;

	// Token: 0x04002FFC RID: 12284
	public bool FillToMinimumMass;

	// Token: 0x04002FFD RID: 12285
	private FetchList2 fetchList;

	// Token: 0x04002FFE RID: 12286
	private int onStorageChangeSubscription = -1;

	// Token: 0x04002FFF RID: 12287
	private static readonly EventSystem.IntraObjectHandler<ManualDeliveryKG> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<ManualDeliveryKG>(delegate(ManualDeliveryKG component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04003000 RID: 12288
	private static readonly EventSystem.IntraObjectHandler<ManualDeliveryKG> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<ManualDeliveryKG>(delegate(ManualDeliveryKG component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04003001 RID: 12289
	private static readonly EventSystem.IntraObjectHandler<ManualDeliveryKG> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<ManualDeliveryKG>(delegate(ManualDeliveryKG component, object data)
	{
		component.OnStorageChanged(data);
	});
}
