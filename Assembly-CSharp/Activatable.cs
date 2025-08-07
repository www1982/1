using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200056B RID: 1387
[AddComponentMenu("KMonoBehaviour/Workable/Activatable")]
public class Activatable : Workable, ISidescreenButtonControl
{
	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06001EDE RID: 7902 RVA: 0x000B178F File Offset: 0x000AF98F
	public bool IsActivated
	{
		get
		{
			return this.activated;
		}
	}

	// Token: 0x06001EDF RID: 7903 RVA: 0x000B1797 File Offset: 0x000AF997
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06001EE0 RID: 7904 RVA: 0x000B179F File Offset: 0x000AF99F
	protected override void OnSpawn()
	{
		this.UpdateFlag();
		if (this.awaitingActivation && this.activateChore == null)
		{
			this.CreateChore();
		}
	}

	// Token: 0x06001EE1 RID: 7905 RVA: 0x000B17BD File Offset: 0x000AF9BD
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.activated = true;
		if (this.onActivate != null)
		{
			this.onActivate();
		}
		this.awaitingActivation = false;
		this.UpdateFlag();
		Prioritizable.RemoveRef(base.gameObject);
		base.OnCompleteWork(worker);
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x000B17F8 File Offset: 0x000AF9F8
	private void UpdateFlag()
	{
		base.GetComponent<Operational>().SetFlag(this.Required ? Activatable.activatedFlagRequirement : Activatable.activatedFlagFunctional, this.activated);
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.DuplicantActivationRequired, !this.activated, null);
		base.Trigger(-1909216579, this.IsActivated);
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x000B1868 File Offset: 0x000AFA68
	private void CreateChore()
	{
		if (this.activateChore != null)
		{
			return;
		}
		Prioritizable.AddRef(base.gameObject);
		this.activateChore = new WorkChore<Activatable>(Db.Get().ChoreTypes.Toggle, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		if (!string.IsNullOrEmpty(this.requiredSkillPerk))
		{
			this.shouldShowSkillPerkStatusItem = true;
			this.requireMinionToWork = true;
			this.UpdateStatusItem(null);
		}
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x000B18D7 File Offset: 0x000AFAD7
	private void CancelChore()
	{
		if (this.activateChore == null)
		{
			return;
		}
		this.activateChore.Cancel("User cancelled");
		this.activateChore = null;
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x000B18F9 File Offset: 0x000AFAF9
	public int HorizontalGroupID()
	{
		return -1;
	}

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x000B18FC File Offset: 0x000AFAFC
	public string SidescreenButtonText
	{
		get
		{
			if (this.activateChore != null)
			{
				return this.textOverride.IsValid ? this.textOverride.CancelText : UI.USERMENUACTIONS.ACTIVATEBUILDING.ACTIVATE_CANCEL;
			}
			return this.textOverride.IsValid ? this.textOverride.Text : UI.USERMENUACTIONS.ACTIVATEBUILDING.ACTIVATE;
		}
	}

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000B195C File Offset: 0x000AFB5C
	public string SidescreenButtonTooltip
	{
		get
		{
			if (this.activateChore != null)
			{
				return this.textOverride.IsValid ? this.textOverride.CancelToolTip : UI.USERMENUACTIONS.ACTIVATEBUILDING.TOOLTIP_CANCEL;
			}
			return this.textOverride.IsValid ? this.textOverride.ToolTip : UI.USERMENUACTIONS.ACTIVATEBUILDING.TOOLTIP_ACTIVATE;
		}
	}

	// Token: 0x06001EE8 RID: 7912 RVA: 0x000B19BA File Offset: 0x000AFBBA
	public bool SidescreenEnabled()
	{
		return !this.activated;
	}

	// Token: 0x06001EE9 RID: 7913 RVA: 0x000B19C5 File Offset: 0x000AFBC5
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		this.textOverride = text;
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x000B19CE File Offset: 0x000AFBCE
	public void OnSidescreenButtonPressed()
	{
		if (this.activateChore == null)
		{
			this.CreateChore();
		}
		else
		{
			this.CancelChore();
		}
		this.awaitingActivation = this.activateChore != null;
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x000B19F5 File Offset: 0x000AFBF5
	public bool SidescreenButtonInteractable()
	{
		return !this.activated;
	}

	// Token: 0x06001EEC RID: 7916 RVA: 0x000B1A00 File Offset: 0x000AFC00
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x040011EF RID: 4591
	public bool Required = true;

	// Token: 0x040011F0 RID: 4592
	private static readonly Operational.Flag activatedFlagRequirement = new Operational.Flag("activated", Operational.Flag.Type.Requirement);

	// Token: 0x040011F1 RID: 4593
	private static readonly Operational.Flag activatedFlagFunctional = new Operational.Flag("activated", Operational.Flag.Type.Functional);

	// Token: 0x040011F2 RID: 4594
	[Serialize]
	private bool activated;

	// Token: 0x040011F3 RID: 4595
	[Serialize]
	private bool awaitingActivation;

	// Token: 0x040011F4 RID: 4596
	private Guid statusItem;

	// Token: 0x040011F5 RID: 4597
	private Chore activateChore;

	// Token: 0x040011F6 RID: 4598
	public global::System.Action onActivate;

	// Token: 0x040011F7 RID: 4599
	[SerializeField]
	private ButtonMenuTextOverride textOverride;
}
