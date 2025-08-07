using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200082D RID: 2093
[AddComponentMenu("KMonoBehaviour/scripts/FactionAlignment")]
public class FactionAlignment : KMonoBehaviour
{
	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x06003946 RID: 14662 RVA: 0x0013D871 File Offset: 0x0013BA71
	// (set) Token: 0x06003947 RID: 14663 RVA: 0x0013D879 File Offset: 0x0013BA79
	[MyCmpAdd]
	public Health health { get; private set; }

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x06003948 RID: 14664 RVA: 0x0013D882 File Offset: 0x0013BA82
	// (set) Token: 0x06003949 RID: 14665 RVA: 0x0013D88A File Offset: 0x0013BA8A
	public AttackableBase attackable { get; private set; }

	// Token: 0x0600394A RID: 14666 RVA: 0x0013D894 File Offset: 0x0013BA94
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.health = base.GetComponent<Health>();
		this.attackable = base.GetComponent<AttackableBase>();
		Components.FactionAlignments.Add(this);
		base.Subscribe<FactionAlignment>(493375141, FactionAlignment.OnRefreshUserMenuDelegate);
		base.Subscribe<FactionAlignment>(2127324410, FactionAlignment.SetPlayerTargetedFalseDelegate);
		base.Subscribe<FactionAlignment>(1502190696, FactionAlignment.OnQueueDestroyObjectDelegate);
		if (this.alignmentActive)
		{
			FactionManager.Instance.GetFaction(this.Alignment).Members.Add(this);
		}
		GameUtil.SubscribeToTags<FactionAlignment>(this, FactionAlignment.OnDeadTagAddedDelegate, true);
		this.SetPlayerTargeted(this.targeted);
		this.UpdateStatusItem();
	}

	// Token: 0x0600394B RID: 14667 RVA: 0x0013D93F File Offset: 0x0013BB3F
	protected override void OnPrefabInit()
	{
	}

	// Token: 0x0600394C RID: 14668 RVA: 0x0013D941 File Offset: 0x0013BB41
	private void OnDeath(object data)
	{
		this.SetAlignmentActive(false);
	}

	// Token: 0x0600394D RID: 14669 RVA: 0x0013D94C File Offset: 0x0013BB4C
	public void SetAlignmentActive(bool active)
	{
		this.SetPlayerTargetable(active);
		this.alignmentActive = active;
		if (active)
		{
			FactionManager.Instance.GetFaction(this.Alignment).Members.Add(this);
			return;
		}
		FactionManager.Instance.GetFaction(this.Alignment).Members.Remove(this);
	}

	// Token: 0x0600394E RID: 14670 RVA: 0x0013D9A3 File Offset: 0x0013BBA3
	public bool IsAlignmentActive()
	{
		return FactionManager.Instance.GetFaction(this.Alignment).Members.Contains(this);
	}

	// Token: 0x0600394F RID: 14671 RVA: 0x0013D9C0 File Offset: 0x0013BBC0
	public bool IsPlayerTargeted()
	{
		return this.targeted;
	}

	// Token: 0x06003950 RID: 14672 RVA: 0x0013D9C8 File Offset: 0x0013BBC8
	public void SetPlayerTargetable(bool state)
	{
		this.targetable = state && this.canBePlayerTargeted;
		if (!state)
		{
			this.SetPlayerTargeted(false);
		}
	}

	// Token: 0x06003951 RID: 14673 RVA: 0x0013D9E8 File Offset: 0x0013BBE8
	public void SetPlayerTargeted(bool state)
	{
		this.targeted = this.canBePlayerTargeted && state && this.targetable;
		if (state)
		{
			if (!Components.PlayerTargeted.Items.Contains(this))
			{
				Components.PlayerTargeted.Add(this);
			}
			this.SetPrioritizable(true);
		}
		else
		{
			Components.PlayerTargeted.Remove(this);
			this.SetPrioritizable(false);
		}
		this.UpdateStatusItem();
	}

	// Token: 0x06003952 RID: 14674 RVA: 0x0013DA50 File Offset: 0x0013BC50
	private void UpdateStatusItem()
	{
		if (this.targeted)
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.OrderAttack, null);
			return;
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.OrderAttack, false);
	}

	// Token: 0x06003953 RID: 14675 RVA: 0x0013DAA0 File Offset: 0x0013BCA0
	private void SetPrioritizable(bool enable)
	{
		Prioritizable component = base.GetComponent<Prioritizable>();
		if (component == null || !this.updatePrioritizable)
		{
			return;
		}
		if (enable && !this.hasBeenRegisterInPriority)
		{
			Prioritizable.AddRef(base.gameObject);
			this.hasBeenRegisterInPriority = true;
			return;
		}
		if (!enable && component.IsPrioritizable() && this.hasBeenRegisterInPriority)
		{
			Prioritizable.RemoveRef(base.gameObject);
			this.hasBeenRegisterInPriority = false;
		}
	}

	// Token: 0x06003954 RID: 14676 RVA: 0x0013DB09 File Offset: 0x0013BD09
	public void SwitchAlignment(FactionManager.FactionID newAlignment)
	{
		this.SetAlignmentActive(false);
		this.Alignment = newAlignment;
		this.SetAlignmentActive(true);
		base.Trigger(-971105736, newAlignment);
	}

	// Token: 0x06003955 RID: 14677 RVA: 0x0013DB31 File Offset: 0x0013BD31
	private void OnQueueDestroyObject()
	{
		FactionManager.Instance.GetFaction(this.Alignment).Members.Remove(this);
		Components.FactionAlignments.Remove(this);
	}

	// Token: 0x06003956 RID: 14678 RVA: 0x0013DB5C File Offset: 0x0013BD5C
	private void OnRefreshUserMenu(object data)
	{
		if (this.Alignment == FactionManager.FactionID.Duplicant)
		{
			return;
		}
		if (!this.canBePlayerTargeted)
		{
			return;
		}
		if (!this.IsAlignmentActive())
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((!this.targeted) ? new KIconButtonMenu.ButtonInfo("action_attack", UI.USERMENUACTIONS.ATTACK.NAME, delegate
		{
			this.SetPlayerTargeted(true);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.ATTACK.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_attack", UI.USERMENUACTIONS.CANCELATTACK.NAME, delegate
		{
			this.SetPlayerTargeted(false);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELATTACK.TOOLTIP, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x0400229F RID: 8863
	[MyCmpReq]
	public KPrefabID kprefabID;

	// Token: 0x040022A0 RID: 8864
	[SerializeField]
	public bool canBePlayerTargeted = true;

	// Token: 0x040022A1 RID: 8865
	[SerializeField]
	public bool updatePrioritizable = true;

	// Token: 0x040022A2 RID: 8866
	[Serialize]
	private bool alignmentActive = true;

	// Token: 0x040022A3 RID: 8867
	public FactionManager.FactionID Alignment;

	// Token: 0x040022A4 RID: 8868
	[Serialize]
	private bool targeted;

	// Token: 0x040022A5 RID: 8869
	[Serialize]
	private bool targetable = true;

	// Token: 0x040022A6 RID: 8870
	private bool hasBeenRegisterInPriority;

	// Token: 0x040022A7 RID: 8871
	private static readonly EventSystem.IntraObjectHandler<FactionAlignment> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<FactionAlignment>(GameTags.Dead, delegate(FactionAlignment component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x040022A8 RID: 8872
	private static readonly EventSystem.IntraObjectHandler<FactionAlignment> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<FactionAlignment>(delegate(FactionAlignment component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x040022A9 RID: 8873
	private static readonly EventSystem.IntraObjectHandler<FactionAlignment> SetPlayerTargetedFalseDelegate = new EventSystem.IntraObjectHandler<FactionAlignment>(delegate(FactionAlignment component, object data)
	{
		component.SetPlayerTargeted(false);
	});

	// Token: 0x040022AA RID: 8874
	private static readonly EventSystem.IntraObjectHandler<FactionAlignment> OnQueueDestroyObjectDelegate = new EventSystem.IntraObjectHandler<FactionAlignment>(delegate(FactionAlignment component, object data)
	{
		component.OnQueueDestroyObject();
	});
}
