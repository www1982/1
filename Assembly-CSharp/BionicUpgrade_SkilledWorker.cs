using System;
using Database;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006C0 RID: 1728
public class BionicUpgrade_SkilledWorker : BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>
{
	// Token: 0x06002A73 RID: 10867 RVA: 0x000F5B60 File Offset: 0x000F3D60
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.Inactive;
		this.root.Enter(new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.State.Callback(BionicUpgrade_SkilledWorker.ApplySkillPerks)).Exit(new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.State.Callback(BionicUpgrade_SkilledWorker.RemoveSkillPerks)).Enter(new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.State.Callback(BionicUpgrade_SkilledWorker.ApplyModifiers))
			.Exit(new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.State.Callback(BionicUpgrade_SkilledWorker.RemoveModifiers))
			.Enter(new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.State.Callback(BionicUpgrade_SkilledWorker.ApplyHats))
			.Exit(new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.State.Callback(BionicUpgrade_SkilledWorker.RemoveHats));
		this.Inactive.EventTransition(GameHashes.ScheduleBlocksChanged, this.Active, new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SkilledWorker.IsMinionWorkingOnlineAndNotInBatterySaveMode)).EventTransition(GameHashes.ScheduleChanged, this.Active, new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SkilledWorker.IsMinionWorkingOnlineAndNotInBatterySaveMode)).EventTransition(GameHashes.BionicOnline, this.Active, new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SkilledWorker.IsMinionWorkingOnlineAndNotInBatterySaveMode))
			.EventTransition(GameHashes.StartWork, this.Active, new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SkilledWorker.IsMinionWorkingOnlineAndNotInBatterySaveMode))
			.TriggerOnEnter(GameHashes.BionicUpgradeWattageChanged, null);
		this.Active.EventTransition(GameHashes.ScheduleBlocksChanged, this.Inactive, new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.IsInBedTimeChore)).EventTransition(GameHashes.ScheduleChanged, this.Inactive, new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.IsInBedTimeChore)).EventTransition(GameHashes.BionicOffline, this.Inactive, null)
			.EventTransition(GameHashes.StopWork, this.Inactive, null)
			.TriggerOnEnter(GameHashes.BionicUpgradeWattageChanged, null)
			.Enter(new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.State.Callback(BionicUpgrade_SkilledWorker.CreateFX))
			.Exit(new StateMachine<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def>.State.Callback(BionicUpgrade_SkilledWorker.ClearFX));
	}

	// Token: 0x06002A74 RID: 10868 RVA: 0x000F5CF9 File Offset: 0x000F3EF9
	public static void ApplySkillPerks(BionicUpgrade_SkilledWorker.Instance smi)
	{
		smi.resume.ApplyAdditionalSkillPerks(((BionicUpgrade_SkilledWorker.Def)smi.def).SkillPerksIds);
	}

	// Token: 0x06002A75 RID: 10869 RVA: 0x000F5D16 File Offset: 0x000F3F16
	public static void RemoveSkillPerks(BionicUpgrade_SkilledWorker.Instance smi)
	{
		smi.resume.RemoveAdditionalSkillPerks(((BionicUpgrade_SkilledWorker.Def)smi.def).SkillPerksIds);
	}

	// Token: 0x06002A76 RID: 10870 RVA: 0x000F5D33 File Offset: 0x000F3F33
	public static void ApplyModifiers(BionicUpgrade_SkilledWorker.Instance smi)
	{
		smi.ApplyModifiers();
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x000F5D3B File Offset: 0x000F3F3B
	public static void RemoveModifiers(BionicUpgrade_SkilledWorker.Instance smi)
	{
		smi.RemoveModifiers();
	}

	// Token: 0x06002A78 RID: 10872 RVA: 0x000F5D43 File Offset: 0x000F3F43
	public static void ApplyHats(BionicUpgrade_SkilledWorker.Instance smi)
	{
		smi.ApplyHats();
	}

	// Token: 0x06002A79 RID: 10873 RVA: 0x000F5D4B File Offset: 0x000F3F4B
	public static void RemoveHats(BionicUpgrade_SkilledWorker.Instance smi)
	{
		smi.RemoveHats();
	}

	// Token: 0x06002A7A RID: 10874 RVA: 0x000F5D53 File Offset: 0x000F3F53
	public static bool IsMinionWorkingOnlineAndNotInBatterySaveMode(BionicUpgrade_SkilledWorker.Instance smi)
	{
		return BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.IsOnline(smi) && !BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.IsInBedTimeChore(smi) && BionicUpgrade_SkilledWorker.IsMinionWorkingWithAttribute(smi);
	}

	// Token: 0x06002A7B RID: 10875 RVA: 0x000F5D70 File Offset: 0x000F3F70
	public static bool IsMinionWorkingWithAttribute(BionicUpgrade_SkilledWorker.Instance smi)
	{
		Workable workable = smi.worker.GetWorkable();
		return workable != null && smi.worker.GetState() == WorkerBase.State.Working && workable.GetWorkAttribute() != null && workable.GetWorkAttribute().Id == ((BionicUpgrade_SkilledWorker.Def)smi.def).AttributeId;
	}

	// Token: 0x06002A7C RID: 10876 RVA: 0x000F5DCA File Offset: 0x000F3FCA
	public static void CreateFX(BionicUpgrade_SkilledWorker.Instance smi)
	{
		BionicUpgrade_SkilledWorker.CreateAndReturnFX(smi);
	}

	// Token: 0x06002A7D RID: 10877 RVA: 0x000F5DD4 File Offset: 0x000F3FD4
	public static BionicAttributeUseFx.Instance CreateAndReturnFX(BionicUpgrade_SkilledWorker.Instance smi)
	{
		if (!smi.isMasterNull)
		{
			smi.fx = new BionicAttributeUseFx.Instance(smi.GetComponent<KMonoBehaviour>(), new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.FXFront)));
			smi.fx.StartSM();
			return smi.fx;
		}
		return null;
	}

	// Token: 0x06002A7E RID: 10878 RVA: 0x000F5E23 File Offset: 0x000F4023
	public static void ClearFX(BionicUpgrade_SkilledWorker.Instance smi)
	{
		smi.fx.sm.destroyFX.Trigger(smi.fx);
		smi.fx = null;
	}

	// Token: 0x02001543 RID: 5443
	public new class Def : BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.Def
	{
		// Token: 0x06009084 RID: 36996 RVA: 0x0036035B File Offset: 0x0035E55B
		public Def(string upgradeID, string attributeID, AttributeModifier[] modifiers = null, SkillPerk[] skillPerks = null, string[] hats = null)
			: base(upgradeID)
		{
			this.AttributeId = attributeID;
			this.modifiers = modifiers;
			this.SkillPerksIds = skillPerks;
			this.hats = hats;
		}

		// Token: 0x06009085 RID: 36997 RVA: 0x00360384 File Offset: 0x0035E584
		public override string GetDescription()
		{
			string text = "";
			if (this.SkillPerksIds.Length != 0)
			{
				text += UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.BOOSTER_ASSIGNMENT.HEADER_PERKS;
				for (int i = 0; i < this.SkillPerksIds.Length; i++)
				{
					text += "\n";
					text += SkillPerk.GetDescription(this.SkillPerksIds[i].Id);
				}
				if (this.modifiers.Length != 0)
				{
					text += "\n\n";
				}
			}
			if (this.modifiers.Length != 0)
			{
				text += UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.BOOSTER_ASSIGNMENT.HEADER_ATTRIBUTES;
				for (int j = 0; j < this.modifiers.Length; j++)
				{
					text += "\n";
					text = text + this.modifiers[j].GetName() + ": " + this.modifiers[j].GetFormattedString();
				}
			}
			return text;
		}

		// Token: 0x04006F23 RID: 28451
		public SkillPerk[] SkillPerksIds;

		// Token: 0x04006F24 RID: 28452
		public string AttributeId;

		// Token: 0x04006F25 RID: 28453
		public AttributeModifier[] modifiers;

		// Token: 0x04006F26 RID: 28454
		public string[] hats;
	}

	// Token: 0x02001544 RID: 5444
	public new class Instance : BionicUpgrade_SM<BionicUpgrade_SkilledWorker, BionicUpgrade_SkilledWorker.Instance>.BaseInstance
	{
		// Token: 0x06009086 RID: 36998 RVA: 0x0036045E File Offset: 0x0035E65E
		public Instance(IStateMachineTarget master, BionicUpgrade_SkilledWorker.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009087 RID: 36999 RVA: 0x00360468 File Offset: 0x0035E668
		public override float GetCurrentWattageCost()
		{
			if (base.IsInsideState(base.sm.Active))
			{
				return base.Data.WattageCost;
			}
			return 0f;
		}

		// Token: 0x06009088 RID: 37000 RVA: 0x00360490 File Offset: 0x0035E690
		public override string GetCurrentWattageCostName()
		{
			float currentWattageCost = this.GetCurrentWattageCost();
			if (base.IsInsideState(base.sm.Active))
			{
				string text = "<b>" + ((currentWattageCost >= 0f) ? "+" : "-") + "</b>";
				return string.Format(DUPLICANTS.MODIFIERS.BIONIC_WATTS.TOOLTIP.STANDARD_ACTIVE_TEMPLATE, this.upgradeComponent.GetProperName(), text + GameUtil.GetFormattedWattage(currentWattageCost, GameUtil.WattageFormatterUnit.Automatic, true));
			}
			return string.Format(DUPLICANTS.MODIFIERS.BIONIC_WATTS.TOOLTIP.STANDARD_INACTIVE_TEMPLATE, this.upgradeComponent.GetProperName(), GameUtil.GetFormattedWattage(this.upgradeComponent.PotentialWattage, GameUtil.WattageFormatterUnit.Automatic, true));
		}

		// Token: 0x06009089 RID: 37001 RVA: 0x00360530 File Offset: 0x0035E730
		public void ApplyModifiers()
		{
			Klei.AI.Attributes attributes = this.resume.GetIdentity.GetAttributes();
			foreach (AttributeModifier attributeModifier in ((BionicUpgrade_SkilledWorker.Def)base.smi.def).modifiers)
			{
				attributes.Add(attributeModifier);
			}
		}

		// Token: 0x0600908A RID: 37002 RVA: 0x00360580 File Offset: 0x0035E780
		public void RemoveModifiers()
		{
			Klei.AI.Attributes attributes = this.resume.GetIdentity.GetAttributes();
			foreach (AttributeModifier attributeModifier in ((BionicUpgrade_SkilledWorker.Def)base.smi.def).modifiers)
			{
				attributes.Remove(attributeModifier);
			}
		}

		// Token: 0x0600908B RID: 37003 RVA: 0x003605D0 File Offset: 0x0035E7D0
		public void ApplyHats()
		{
			string[] hats = ((BionicUpgrade_SkilledWorker.Def)base.smi.def).hats;
			if (hats == null)
			{
				return;
			}
			MinionResume component = base.GetComponent<MinionResume>();
			string properName = Assets.GetPrefab(base.smi.def.UpgradeID).GetProperName();
			foreach (string text in hats)
			{
				component.AddAdditionalHat(properName, text);
			}
		}

		// Token: 0x0600908C RID: 37004 RVA: 0x00360644 File Offset: 0x0035E844
		public void RemoveHats()
		{
			string[] hats = ((BionicUpgrade_SkilledWorker.Def)base.smi.def).hats;
			if (hats == null)
			{
				return;
			}
			MinionResume component = base.GetComponent<MinionResume>();
			string properName = Assets.GetPrefab(base.smi.def.UpgradeID).GetProperName();
			foreach (string text in hats)
			{
				component.RemoveAdditionalHat(properName, text);
			}
		}

		// Token: 0x04006F27 RID: 28455
		[MyCmpGet]
		public WorkerBase worker;

		// Token: 0x04006F28 RID: 28456
		[MyCmpGet]
		public MinionResume resume;

		// Token: 0x04006F29 RID: 28457
		public BionicAttributeUseFx.Instance fx;
	}
}
