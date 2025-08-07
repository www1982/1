using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006B9 RID: 1721
public class BionicUpgradeComponent : Assignable, IGameObjectEffectDescriptor
{
	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000F4E2A File Offset: 0x000F302A
	// (set) Token: 0x06002A41 RID: 10817 RVA: 0x000F4E21 File Offset: 0x000F3021
	public BionicUpgradeComponent.IWattageController WattageController { get; private set; }

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000F4E32 File Offset: 0x000F3032
	public float CurrentWattage
	{
		get
		{
			if (!this.HasWattageController)
			{
				return 0f;
			}
			return this.WattageController.GetCurrentWattageCost();
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06002A44 RID: 10820 RVA: 0x000F4E4D File Offset: 0x000F304D
	public string CurrentWattageName
	{
		get
		{
			if (!this.HasWattageController)
			{
				return string.Format(DUPLICANTS.MODIFIERS.BIONIC_WATTS.TOOLTIP.STANDARD_INACTIVE_TEMPLATE, this.GetProperName(), GameUtil.GetFormattedWattage(this.PotentialWattage, GameUtil.WattageFormatterUnit.Automatic, true));
			}
			return this.WattageController.GetCurrentWattageCostName();
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06002A45 RID: 10821 RVA: 0x000F4E85 File Offset: 0x000F3085
	public bool HasWattageController
	{
		get
		{
			return this.WattageController != null;
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06002A46 RID: 10822 RVA: 0x000F4E90 File Offset: 0x000F3090
	public float PotentialWattage
	{
		get
		{
			return this.data.WattageCost;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06002A47 RID: 10823 RVA: 0x000F4E9D File Offset: 0x000F309D
	public BionicUpgradeComponentConfig.BoosterType Booster
	{
		get
		{
			return this.data.Booster;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06002A48 RID: 10824 RVA: 0x000F4EAA File Offset: 0x000F30AA
	public Func<StateMachine.Instance, StateMachine.Instance> StateMachine
	{
		get
		{
			return this.data.stateMachine;
		}
	}

	// Token: 0x06002A49 RID: 10825 RVA: 0x000F4EB8 File Offset: 0x000F30B8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.data = BionicUpgradeComponentConfig.UpgradesData[base.gameObject.PrefabID()];
		base.AddAssignPrecondition(new Func<MinionAssignablesProxy, bool>(this.AssignablePrecondition_OnlyOnBionics));
		base.AddAssignPrecondition(new Func<MinionAssignablesProxy, bool>(this.AssignablePrecondition_HasAvailableSlots));
	}

	// Token: 0x06002A4A RID: 10826 RVA: 0x000F4F0C File Offset: 0x000F310C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Game.Instance.assignmentManager.Remove(this);
		this.customAssignmentUITooltipFunc = new Func<Assignables, string>(this.GetTooltipForBoosterAssignment);
		this.customAssignablesUITooltipFunc = new Func<Assignables, string>(this.GetTooltipForMinionAssigment);
		base.Subscribe(856640610, new Action<object>(this.RefreshStatusItem));
		this.RefreshStatusItem(null);
	}

	// Token: 0x06002A4B RID: 10827 RVA: 0x000F4F74 File Offset: 0x000F3174
	private void RefreshStatusItem(object data = null)
	{
		if (this.assignee == null && !base.gameObject.HasTag(GameTags.Stored))
		{
			if (this.unassignedStatusItem == Guid.Empty)
			{
				this.unassignedStatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.UnassignedBionicBooster, null);
				return;
			}
		}
		else if (this.unassignedStatusItem != Guid.Empty)
		{
			this.unassignedStatusItem = base.GetComponent<KSelectable>().RemoveStatusItem(this.unassignedStatusItem, false);
		}
	}

	// Token: 0x06002A4C RID: 10828 RVA: 0x000F4FFC File Offset: 0x000F31FC
	public string GetTooltipForMinionAssigment(Assignables assignables)
	{
		MinionAssignablesProxy component = assignables.GetComponent<MinionAssignablesProxy>();
		if (component == null)
		{
			return "ERROR N/A";
		}
		GameObject targetGameObject = component.GetTargetGameObject();
		if (targetGameObject == null)
		{
			return "ERROR N/A";
		}
		BionicUpgradesMonitor.Instance smi = targetGameObject.GetSMI<BionicUpgradesMonitor.Instance>();
		if (smi == null)
		{
			return "This Duplicant cannot install boosters";
		}
		int num = smi.CountBoosterAssignments(this.PrefabID());
		string text = ((num == 0) ? string.Format(UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.BOOSTER_ASSIGNMENT.NOT_ALREADY_ASSIGNED, smi.gameObject.GetProperName(), num) : string.Format(UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.BOOSTER_ASSIGNMENT.ALREADY_ASSIGNED, smi.gameObject.GetProperName(), num));
		string text2 = string.Format((smi.AssignedSlotCount < smi.UnlockedSlotCount) ? UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.BOOSTER_ASSIGNMENT.AVAILABLE_SLOTS : UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.BOOSTER_ASSIGNMENT.NO_AVAILABLE_SLOTS, targetGameObject.GetProperName(), smi.AssignedSlotCount, smi.UnlockedSlotCount);
		string text3 = "";
		List<AttributeInstance> list = new List<AttributeInstance>(targetGameObject.GetAttributes().AttributeTable).FindAll((AttributeInstance a) => a.Attribute.ShowInUI == Klei.AI.Attribute.Display.Skill);
		for (int i = 0; i < list.Count; i++)
		{
			string text4 = UIConstants.ColorPrefixWhite;
			if (list[i].GetTotalValue() > 0f)
			{
				text4 = UIConstants.ColorPrefixGreen;
			}
			else if (list[i].GetTotalValue() < 0f)
			{
				text4 = UIConstants.ColorPrefixRed;
			}
			text3 += string.Format("{0}: {1}", list[i].Name, text4 + list[i].GetFormattedValue() + UIConstants.ColorSuffix);
			if (i != list.Count - 1)
			{
				text3 += "\n";
			}
		}
		return string.Concat(new string[]
		{
			targetGameObject.GetProperName(),
			"\n\n",
			text,
			"\n\n",
			text2,
			"\n\n",
			text3
		});
	}

	// Token: 0x06002A4D RID: 10829 RVA: 0x000F5208 File Offset: 0x000F3408
	public string GetTooltipForBoosterAssignment(Assignables assignables)
	{
		MinionAssignablesProxy component = assignables.GetComponent<MinionAssignablesProxy>();
		if (component == null)
		{
			return "ERROR N/A";
		}
		GameObject targetGameObject = component.GetTargetGameObject();
		if (targetGameObject == null)
		{
			return "ERROR N/A";
		}
		BionicUpgradesMonitor.Instance smi = targetGameObject.GetSMI<BionicUpgradesMonitor.Instance>();
		if (smi == null)
		{
			return "ERROR N/A";
		}
		int num = smi.CountBoosterAssignments(this.PrefabID());
		string text = ((num == 0) ? string.Format(UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.BOOSTER_ASSIGNMENT.NOT_ALREADY_ASSIGNED, smi.gameObject.GetProperName(), num) : string.Format(UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.BOOSTER_ASSIGNMENT.ALREADY_ASSIGNED, smi.gameObject.GetProperName(), num));
		return BionicUpgradeComponentConfig.GenerateTooltipForBooster(this) + "\n\n" + text;
	}

	// Token: 0x06002A4E RID: 10830 RVA: 0x000F52B7 File Offset: 0x000F34B7
	public void InformOfWattageChanged()
	{
		global::System.Action onWattageCostChanged = this.OnWattageCostChanged;
		if (onWattageCostChanged == null)
		{
			return;
		}
		onWattageCostChanged();
	}

	// Token: 0x06002A4F RID: 10831 RVA: 0x000F52C9 File Offset: 0x000F34C9
	public void SetWattageController(BionicUpgradeComponent.IWattageController wattageController)
	{
		this.WattageController = wattageController;
	}

	// Token: 0x06002A50 RID: 10832 RVA: 0x000F52D4 File Offset: 0x000F34D4
	public override void Assign(IAssignableIdentity new_assignee)
	{
		AssignableSlotInstance assignableSlotInstance = null;
		if (new_assignee == this.assignee)
		{
			return;
		}
		if (new_assignee != this.assignee && (new_assignee is MinionIdentity || new_assignee is StoredMinionIdentity || new_assignee is MinionAssignablesProxy))
		{
			Ownables soleOwner = new_assignee.GetSoleOwner();
			if (soleOwner != null)
			{
				BionicUpgradesMonitor.Instance smi = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().GetSMI<BionicUpgradesMonitor.Instance>();
				if (smi != null)
				{
					BionicUpgradesMonitor.UpgradeComponentSlot firstEmptyAvailableSlot = smi.GetFirstEmptyAvailableSlot();
					if (firstEmptyAvailableSlot != null)
					{
						assignableSlotInstance = firstEmptyAvailableSlot.GetAssignableSlotInstance();
					}
				}
			}
		}
		base.Assign(new_assignee, assignableSlotInstance);
		base.Trigger(1980521255, null);
		this.RefreshStatusItem(null);
	}

	// Token: 0x06002A51 RID: 10833 RVA: 0x000F535E File Offset: 0x000F355E
	public override void Unassign()
	{
		base.Unassign();
		base.Trigger(1980521255, null);
		this.RefreshStatusItem(null);
	}

	// Token: 0x06002A52 RID: 10834 RVA: 0x000F5379 File Offset: 0x000F3579
	private bool AssignablePrecondition_OnlyOnBionics(MinionAssignablesProxy worker)
	{
		return worker.GetMinionModel() == BionicMinionConfig.MODEL;
	}

	// Token: 0x06002A53 RID: 10835 RVA: 0x000F538C File Offset: 0x000F358C
	private bool AssignablePrecondition_HasAvailableSlots(MinionAssignablesProxy worker)
	{
		if (SelectTool.Instance.selected != null && SelectTool.Instance.selected.gameObject == worker.GetTargetGameObject())
		{
			return true;
		}
		MinionIdentity minionIdentity = worker.target as MinionIdentity;
		if (minionIdentity != null)
		{
			BionicUpgradesMonitor.Instance smi = minionIdentity.GetSMI<BionicUpgradesMonitor.Instance>();
			if (smi == null)
			{
				return true;
			}
			foreach (BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot in smi.upgradeComponentSlots)
			{
				if (!upgradeComponentSlot.IsLocked && (upgradeComponentSlot.assignedUpgradeComponent == null || upgradeComponentSlot.assignedUpgradeComponent == this))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x000F542E File Offset: 0x000F362E
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(BionicUpgradeComponentConfig.UpgradesData[base.gameObject.PrefabID()].stateMachineDescription, null, Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x04001907 RID: 6407
	private BionicUpgradeComponentConfig.BionicUpgradeData data;

	// Token: 0x04001908 RID: 6408
	public global::System.Action OnWattageCostChanged;

	// Token: 0x04001909 RID: 6409
	private Guid unassignedStatusItem = Guid.Empty;

	// Token: 0x02001532 RID: 5426
	public interface IWattageController
	{
		// Token: 0x0600904A RID: 36938
		float GetCurrentWattageCost();

		// Token: 0x0600904B RID: 36939
		string GetCurrentWattageCostName();
	}
}
