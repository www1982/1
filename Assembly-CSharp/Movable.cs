using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005E5 RID: 1509
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Movable")]
public class Movable : Workable
{
	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06002317 RID: 8983 RVA: 0x000C97C8 File Offset: 0x000C79C8
	public bool IsMarkedForMove
	{
		get
		{
			return this.isMarkedForMove;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06002318 RID: 8984 RVA: 0x000C97D0 File Offset: 0x000C79D0
	public Storage StorageProxy
	{
		get
		{
			if (this.storageProxy == null)
			{
				return null;
			}
			return this.storageProxy.Get();
		}
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x000C97E7 File Offset: 0x000C79E7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe(493375141, new Action<object>(this.OnRefreshUserMenu));
		base.Subscribe(1335436905, new Action<object>(this.OnSplitFromChunk));
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x000C9820 File Offset: 0x000C7A20
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.isMarkedForMove)
		{
			if (this.StorageProxy != null)
			{
				if (this.reachableChangedHandle < 0)
				{
					this.reachableChangedHandle = base.Subscribe(-1432940121, new Action<object>(this.OnReachableChanged));
				}
				if (this.storageReachableChangedHandle < 0)
				{
					this.storageReachableChangedHandle = this.StorageProxy.Subscribe(-1432940121, new Action<object>(this.OnReachableChanged));
				}
				if (this.cancelHandle < 0)
				{
					this.cancelHandle = base.Subscribe(2127324410, new Action<object>(this.CleanupMove));
				}
				if (this.tagsChangedHandle < 0)
				{
					this.tagsChangedHandle = base.Subscribe(-1582839653, new Action<object>(this.OnTagsChanged));
				}
				base.gameObject.AddTag(GameTags.MarkedForMove);
			}
			else
			{
				this.isMarkedForMove = false;
			}
		}
		if (Movable.IsCritterPickupable(base.gameObject))
		{
			this.skillsUpdateHandle = Game.Instance.Subscribe(-1523247426, new Action<object>(this.UpdateStatusItem));
			this.shouldShowSkillPerkStatusItem = this.isMarkedForMove;
			this.requiredSkillPerk = Db.Get().SkillPerks.CanWrangleCreatures.Id;
			this.UpdateStatusItem();
		}
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x000C9960 File Offset: 0x000C7B60
	private void OnReachableChanged(object data)
	{
		if (this.isMarkedForMove)
		{
			if (this.StorageProxy != null)
			{
				int num = Grid.PosToCell(this.pickupable);
				int num2 = Grid.PosToCell(this.StorageProxy);
				if (num != num2)
				{
					bool flag = MinionGroupProber.Get().IsReachable(num, OffsetGroups.Standard) && MinionGroupProber.Get().IsReachable(num2, OffsetGroups.Standard);
					if (this.pickupable.KPrefabID.HasTag(GameTags.Creatures.Confined))
					{
						flag = false;
					}
					KSelectable component = base.GetComponent<KSelectable>();
					this.pendingMoveGuid = component.ToggleStatusItem(Db.Get().MiscStatusItems.MarkedForMove, this.pendingMoveGuid, flag, this);
					this.storageUnreachableGuid = component.ToggleStatusItem(Db.Get().MiscStatusItems.MoveStorageUnreachable, this.storageUnreachableGuid, !flag, this);
					return;
				}
			}
			else
			{
				this.ClearMove();
			}
		}
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x000C9A40 File Offset: 0x000C7C40
	private void OnSplitFromChunk(object data)
	{
		Pickupable pickupable = data as Pickupable;
		if (pickupable != null)
		{
			Movable component = pickupable.GetComponent<Movable>();
			if (component.isMarkedForMove)
			{
				this.storageProxy = new Ref<Storage>(component.StorageProxy);
				this.MarkForMove();
			}
		}
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x000C9A83 File Offset: 0x000C7C83
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.isMarkedForMove && this.StorageProxy != null)
		{
			this.StorageProxy.GetComponent<CancellableMove>().RemoveMovable(this);
			this.ClearStorageProxy();
		}
	}

	// Token: 0x0600231E RID: 8990 RVA: 0x000C9AB8 File Offset: 0x000C7CB8
	private void CleanupMove(object data)
	{
		if (this.StorageProxy != null)
		{
			this.StorageProxy.GetComponent<CancellableMove>().OnCancel(this);
		}
	}

	// Token: 0x0600231F RID: 8991 RVA: 0x000C9AD9 File Offset: 0x000C7CD9
	private void OnTagsChanged(object data)
	{
		if (this.isMarkedForMove && !this.HasTagRequiredToMove() && this.StorageProxy != null)
		{
			this.StorageProxy.GetComponent<CancellableMove>().OnCancel(this);
		}
	}

	// Token: 0x06002320 RID: 8992 RVA: 0x000C9B0C File Offset: 0x000C7D0C
	public void ClearMove()
	{
		if (this.isMarkedForMove)
		{
			this.isMarkedForMove = false;
			KSelectable component = base.GetComponent<KSelectable>();
			this.pendingMoveGuid = component.RemoveStatusItem(this.pendingMoveGuid, false);
			this.storageUnreachableGuid = component.RemoveStatusItem(this.storageUnreachableGuid, false);
			this.ClearStorageProxy();
			base.gameObject.RemoveTag(GameTags.MarkedForMove);
			if (this.reachableChangedHandle != -1)
			{
				base.Unsubscribe(-1432940121, new Action<object>(this.OnReachableChanged));
				this.reachableChangedHandle = -1;
			}
			if (this.cancelHandle != -1)
			{
				base.Unsubscribe(2127324410, new Action<object>(this.CleanupMove));
				this.cancelHandle = -1;
			}
			if (this.tagsChangedHandle != -1)
			{
				base.Unsubscribe(-1582839653, new Action<object>(this.OnTagsChanged));
				this.tagsChangedHandle = -1;
			}
		}
		this.UpdateStatusItem();
	}

	// Token: 0x06002321 RID: 8993 RVA: 0x000C9BE9 File Offset: 0x000C7DE9
	private void ClearStorageProxy()
	{
		if (this.storageReachableChangedHandle != -1)
		{
			this.StorageProxy.Unsubscribe(-1432940121, new Action<object>(this.OnReachableChanged));
			this.storageReachableChangedHandle = -1;
		}
		this.storageProxy = null;
	}

	// Token: 0x06002322 RID: 8994 RVA: 0x000C9C1E File Offset: 0x000C7E1E
	private void OnClickMove()
	{
		MoveToLocationTool.Instance.Activate(this);
	}

	// Token: 0x06002323 RID: 8995 RVA: 0x000C9C2B File Offset: 0x000C7E2B
	private void OnClickCancel()
	{
		if (this.StorageProxy != null)
		{
			this.StorageProxy.GetComponent<CancellableMove>().OnCancel(this);
		}
	}

	// Token: 0x06002324 RID: 8996 RVA: 0x000C9C4C File Offset: 0x000C7E4C
	private void OnRefreshUserMenu(object data)
	{
		if (this.pickupable.KPrefabID.HasTag(GameTags.Stored) || !this.HasTagRequiredToMove())
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = (this.isMarkedForMove ? new KIconButtonMenu.ButtonInfo("action_control", UI.USERMENUACTIONS.PICKUPABLEMOVE.NAME_OFF, new global::System.Action(this.OnClickCancel), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.PICKUPABLEMOVE.TOOLTIP_OFF, true) : new KIconButtonMenu.ButtonInfo("action_control", UI.USERMENUACTIONS.PICKUPABLEMOVE.NAME, new global::System.Action(this.OnClickMove), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.PICKUPABLEMOVE.TOOLTIP, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06002325 RID: 8997 RVA: 0x000C9D05 File Offset: 0x000C7F05
	private bool HasTagRequiredToMove()
	{
		return this.tagRequiredForMove == Tag.Invalid || this.pickupable.KPrefabID.HasTag(this.tagRequiredForMove);
	}

	// Token: 0x06002326 RID: 8998 RVA: 0x000C9D31 File Offset: 0x000C7F31
	public void MoveToLocation(int cell)
	{
		this.CreateStorageProxy(cell);
		this.MarkForMove();
		base.gameObject.Trigger(1122777325, base.gameObject);
	}

	// Token: 0x06002327 RID: 8999 RVA: 0x000C9D58 File Offset: 0x000C7F58
	private void MarkForMove()
	{
		base.Trigger(2127324410, null);
		this.isMarkedForMove = true;
		this.OnReachableChanged(null);
		this.storageReachableChangedHandle = this.StorageProxy.Subscribe(-1432940121, new Action<object>(this.OnReachableChanged));
		this.reachableChangedHandle = base.Subscribe(-1432940121, new Action<object>(this.OnReachableChanged));
		this.StorageProxy.GetComponent<CancellableMove>().SetMovable(this);
		base.gameObject.AddTag(GameTags.MarkedForMove);
		this.cancelHandle = base.Subscribe(2127324410, new Action<object>(this.CleanupMove));
		this.tagsChangedHandle = base.Subscribe(-1582839653, new Action<object>(this.OnTagsChanged));
		this.UpdateStatusItem();
	}

	// Token: 0x06002328 RID: 9000 RVA: 0x000C9E1F File Offset: 0x000C801F
	private void UpdateStatusItem()
	{
		if (Movable.IsCritterPickupable(base.gameObject))
		{
			this.shouldShowSkillPerkStatusItem = this.isMarkedForMove;
			base.UpdateStatusItem(null);
		}
	}

	// Token: 0x06002329 RID: 9001 RVA: 0x000C9E41 File Offset: 0x000C8041
	public bool CanMoveTo(int cell)
	{
		return !Grid.IsSolidCell(cell) && Grid.IsWorldValidCell(cell) && base.gameObject.IsMyParentWorld(cell);
	}

	// Token: 0x0600232A RID: 9002 RVA: 0x000C9E64 File Offset: 0x000C8064
	private void CreateStorageProxy(int cell)
	{
		if (this.storageProxy == null || this.storageProxy.Get() == null)
		{
			if (Grid.Objects[cell, 44] != null)
			{
				Storage component = Grid.Objects[cell, 44].GetComponent<Storage>();
				this.storageProxy = new Ref<Storage>(component);
				return;
			}
			Vector3 vector = Grid.CellToPosCBC(cell, MoveToLocationTool.Instance.visualizerLayer);
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(MovePickupablePlacerConfig.ID), vector);
			Storage component2 = gameObject.GetComponent<Storage>();
			gameObject.SetActive(true);
			this.storageProxy = new Ref<Storage>(component2);
		}
	}

	// Token: 0x0600232B RID: 9003 RVA: 0x000C9F00 File Offset: 0x000C8100
	public static bool IsCritterPickupable(GameObject pickupable_go)
	{
		return pickupable_go.GetComponent<Capturable>();
	}

	// Token: 0x0400145D RID: 5213
	[MyCmpReq]
	private Pickupable pickupable;

	// Token: 0x0400145E RID: 5214
	public Tag tagRequiredForMove = Tag.Invalid;

	// Token: 0x0400145F RID: 5215
	[Serialize]
	private bool isMarkedForMove;

	// Token: 0x04001460 RID: 5216
	[Serialize]
	private Ref<Storage> storageProxy;

	// Token: 0x04001461 RID: 5217
	private int storageReachableChangedHandle = -1;

	// Token: 0x04001462 RID: 5218
	private int reachableChangedHandle = -1;

	// Token: 0x04001463 RID: 5219
	private int cancelHandle = -1;

	// Token: 0x04001464 RID: 5220
	private int tagsChangedHandle = -1;

	// Token: 0x04001465 RID: 5221
	private Guid pendingMoveGuid;

	// Token: 0x04001466 RID: 5222
	private Guid storageUnreachableGuid;

	// Token: 0x04001467 RID: 5223
	public Action<GameObject> onDeliveryComplete;

	// Token: 0x04001468 RID: 5224
	public Action<GameObject> onPickupComplete;
}
