using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200057E RID: 1406
public class CancellableMove : Cancellable
{
	// Token: 0x17000135 RID: 309
	// (get) Token: 0x06001FCF RID: 8143 RVA: 0x000B6C63 File Offset: 0x000B4E63
	public List<Ref<Movable>> movingObjects
	{
		get
		{
			return this.movables;
		}
	}

	// Token: 0x06001FD0 RID: 8144 RVA: 0x000B6C6C File Offset: 0x000B4E6C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Prioritizable component = base.GetComponent<Prioritizable>();
		if (!component.IsPrioritizable())
		{
			component.AddRef();
		}
		if (this.fetchChore == null)
		{
			GameObject nextTarget = this.GetNextTarget();
			if (!(nextTarget != null) || nextTarget.IsNullOrDestroyed())
			{
				global::Debug.LogWarning("MovePickupable spawned with no objects to move. Destroying placer.");
				Util.KDestroyGameObject(base.gameObject);
				return;
			}
			this.fetchChore = new MovePickupableChore(this, nextTarget, new Action<Chore>(this.OnChoreEnd));
		}
		base.Subscribe(493375141, new Action<object>(this.OnRefreshUserMenu));
		base.Subscribe(2127324410, new Action<object>(this.OnCancel));
		base.GetComponent<KPrefabID>().AddTag(GameTags.HasChores, false);
		int num = Grid.PosToCell(this);
		Grid.Objects[num, 44] = base.gameObject;
	}

	// Token: 0x06001FD1 RID: 8145 RVA: 0x000B6D44 File Offset: 0x000B4F44
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		int num = Grid.PosToCell(this);
		Grid.Objects[num, 44] = null;
		Prioritizable.RemoveRef(base.gameObject);
	}

	// Token: 0x06001FD2 RID: 8146 RVA: 0x000B6D77 File Offset: 0x000B4F77
	public void CancelAll()
	{
		this.OnCancel(null);
	}

	// Token: 0x06001FD3 RID: 8147 RVA: 0x000B6D80 File Offset: 0x000B4F80
	public void OnCancel(Movable cancel_movable = null)
	{
		for (int i = this.movables.Count - 1; i >= 0; i--)
		{
			Ref<Movable> @ref = this.movables[i];
			if (@ref != null)
			{
				Movable movable = @ref.Get();
				if (cancel_movable == null || movable == cancel_movable)
				{
					movable.ClearMove();
					this.movables.RemoveAt(i);
				}
			}
		}
		if (this.fetchChore != null)
		{
			this.fetchChore.Cancel("CancelMove");
			if (this.fetchChore.driver == null && this.movables.Count <= 0)
			{
				Util.KDestroyGameObject(base.gameObject);
			}
		}
	}

	// Token: 0x06001FD4 RID: 8148 RVA: 0x000B6E24 File Offset: 0x000B5024
	protected override void OnCancel(object data)
	{
		this.OnCancel(null);
	}

	// Token: 0x06001FD5 RID: 8149 RVA: 0x000B6E30 File Offset: 0x000B5030
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_control", UI.USERMENUACTIONS.PICKUPABLEMOVE.NAME_OFF, new global::System.Action(this.CancelAll), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.PICKUPABLEMOVE.TOOLTIP_OFF, true), 1f);
	}

	// Token: 0x06001FD6 RID: 8150 RVA: 0x000B6E8C File Offset: 0x000B508C
	public void SetMovable(Movable movable)
	{
		if (this.fetchChore == null)
		{
			this.fetchChore = new MovePickupableChore(this, movable.gameObject, new Action<Chore>(this.OnChoreEnd));
		}
		if (this.movables.Find((Ref<Movable> move) => move.Get() == movable) == null)
		{
			this.movables.Add(new Ref<Movable>(movable));
		}
	}

	// Token: 0x06001FD7 RID: 8151 RVA: 0x000B6F00 File Offset: 0x000B5100
	public void OnChoreEnd(Chore chore)
	{
		GameObject nextTarget = this.GetNextTarget();
		if (nextTarget == null)
		{
			Util.KDestroyGameObject(base.gameObject);
			return;
		}
		this.fetchChore = new MovePickupableChore(this, nextTarget, new Action<Chore>(this.OnChoreEnd));
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x000B6F42 File Offset: 0x000B5142
	public bool IsDeliveryComplete()
	{
		this.ValidateMovables();
		return this.movables.Count <= 0;
	}

	// Token: 0x06001FD9 RID: 8153 RVA: 0x000B6F5C File Offset: 0x000B515C
	public void RemoveMovable(Movable moved)
	{
		for (int i = this.movables.Count - 1; i >= 0; i--)
		{
			if (this.movables[i].Get() == null || this.movables[i].Get() == moved)
			{
				this.movables.RemoveAt(i);
			}
		}
		if (this.movables.Count <= 0)
		{
			this.OnCancel(null);
		}
	}

	// Token: 0x06001FDA RID: 8154 RVA: 0x000B6FD4 File Offset: 0x000B51D4
	public GameObject GetNextTarget()
	{
		this.ValidateMovables();
		if (this.movables.Count > 0)
		{
			return this.movables[0].Get().gameObject;
		}
		return null;
	}

	// Token: 0x06001FDB RID: 8155 RVA: 0x000B7004 File Offset: 0x000B5204
	private void ValidateMovables()
	{
		for (int i = this.movables.Count - 1; i >= 0; i--)
		{
			if (this.movables[i] == null)
			{
				this.movables.RemoveAt(i);
			}
			else
			{
				Movable movable = this.movables[i].Get();
				if (movable == null)
				{
					this.movables.RemoveAt(i);
				}
				else if (Grid.PosToCell(movable) == Grid.PosToCell(this))
				{
					movable.ClearMove();
					this.movables.RemoveAt(i);
				}
			}
		}
	}

	// Token: 0x04001284 RID: 4740
	[Serialize]
	private List<Ref<Movable>> movables = new List<Ref<Movable>>();

	// Token: 0x04001285 RID: 4741
	private MovePickupableChore fetchChore;
}
