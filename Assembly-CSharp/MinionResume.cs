using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Database;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000ADF RID: 2783
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/MinionResume")]
public class MinionResume : IExperienceRecipient, ISaveLoadable, ISim200ms
{
	// Token: 0x170005A8 RID: 1448
	// (get) Token: 0x060050DE RID: 20702 RVA: 0x001D5A26 File Offset: 0x001D3C26
	public MinionIdentity GetIdentity
	{
		get
		{
			return this.identity;
		}
	}

	// Token: 0x170005A9 RID: 1449
	// (get) Token: 0x060050DF RID: 20703 RVA: 0x001D5A2E File Offset: 0x001D3C2E
	public float TotalExperienceGained
	{
		get
		{
			return this.totalExperienceGained;
		}
	}

	// Token: 0x170005AA RID: 1450
	// (get) Token: 0x060050E0 RID: 20704 RVA: 0x001D5A36 File Offset: 0x001D3C36
	public int TotalSkillPointsGained
	{
		get
		{
			return MinionResume.CalculateTotalSkillPointsGained(this.TotalExperienceGained);
		}
	}

	// Token: 0x060050E1 RID: 20705 RVA: 0x001D5A43 File Offset: 0x001D3C43
	public static int CalculateTotalSkillPointsGained(float experience)
	{
		return Mathf.FloorToInt(Mathf.Pow(experience / (float)SKILLS.TARGET_SKILLS_CYCLE / 600f, 1f / SKILLS.EXPERIENCE_LEVEL_POWER) * (float)SKILLS.TARGET_SKILLS_EARNED);
	}

	// Token: 0x170005AB RID: 1451
	// (get) Token: 0x060050E2 RID: 20706 RVA: 0x001D5A70 File Offset: 0x001D3C70
	public int SkillsMastered
	{
		get
		{
			int num = 0;
			foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
			{
				if (keyValuePair.Value)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x170005AC RID: 1452
	// (get) Token: 0x060050E3 RID: 20707 RVA: 0x001D5ACC File Offset: 0x001D3CCC
	public int AvailableSkillpoints
	{
		get
		{
			return this.TotalSkillPointsGained - this.SkillsMastered + ((this.GrantedSkillIDs == null) ? 0 : this.GrantedSkillIDs.Count);
		}
	}

	// Token: 0x060050E4 RID: 20708 RVA: 0x001D5AF4 File Offset: 0x001D3CF4
	[OnDeserialized]
	private void OnDeserializedMethod()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 7))
		{
			foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryByRoleID)
			{
				if (keyValuePair.Value && keyValuePair.Key != "NoRole")
				{
					this.ForceAddSkillPoint();
				}
			}
			foreach (KeyValuePair<HashedString, float> keyValuePair2 in this.AptitudeByRoleGroup)
			{
				this.AptitudeBySkillGroup[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}
		if (this.TotalSkillPointsGained > 1000 || this.TotalSkillPointsGained < 0)
		{
			this.ForceSetSkillPoints(100);
		}
	}

	// Token: 0x060050E5 RID: 20709 RVA: 0x001D5BF0 File Offset: 0x001D3DF0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.MinionResumes.Add(this);
	}

	// Token: 0x060050E6 RID: 20710 RVA: 0x001D5C04 File Offset: 0x001D3E04
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.GrantedSkillIDs.RemoveAll((string x) => Db.Get().Skills.TryGet(x) == null);
		List<string> list = new List<string>();
		foreach (string text in this.MasteryBySkillID.Keys)
		{
			if (Db.Get().Skills.TryGet(text) == null)
			{
				list.Add(text);
			}
		}
		foreach (string text2 in list)
		{
			this.MasteryBySkillID.Remove(text2);
		}
		if (this.GrantedSkillIDs == null)
		{
			this.GrantedSkillIDs = new List<string>();
		}
		List<string> list2 = new List<string>();
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && Db.Get().Skills.Get(keyValuePair.Key).deprecated)
			{
				list2.Add(keyValuePair.Key);
			}
		}
		foreach (string text3 in list2)
		{
			this.UnmasterSkill(text3);
		}
		foreach (KeyValuePair<string, bool> keyValuePair2 in this.MasteryBySkillID)
		{
			if (keyValuePair2.Value)
			{
				Skill skill = Db.Get().Skills.Get(keyValuePair2.Key);
				foreach (SkillPerk skillPerk in skill.perks)
				{
					if (Game.IsCorrectDlcActiveForCurrentSave(skillPerk))
					{
						if (skillPerk.OnRemove != null)
						{
							skillPerk.OnRemove(this);
						}
						if (skillPerk.OnApply != null)
						{
							skillPerk.OnApply(this);
						}
					}
				}
				if (!this.ownedHats.ContainsKey(skill.hat))
				{
					this.ownedHats.Add(skill.hat, true);
				}
			}
		}
		this.UpdateExpectations();
		this.UpdateMorale();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		MinionResume.ApplyHat(this.currentHat, component);
		this.ShowNewSkillPointNotification();
	}

	// Token: 0x060050E7 RID: 20711 RVA: 0x001D5EE0 File Offset: 0x001D40E0
	public void RestoreResume(Dictionary<string, bool> MasteryBySkillID, Dictionary<HashedString, float> AptitudeBySkillGroup, List<string> GrantedSkillIDs, float totalExperienceGained)
	{
		this.MasteryBySkillID = MasteryBySkillID;
		this.GrantedSkillIDs = ((GrantedSkillIDs != null) ? GrantedSkillIDs : new List<string>());
		this.AptitudeBySkillGroup = AptitudeBySkillGroup;
		this.totalExperienceGained = totalExperienceGained;
	}

	// Token: 0x060050E8 RID: 20712 RVA: 0x001D5F09 File Offset: 0x001D4109
	protected override void OnCleanUp()
	{
		Components.MinionResumes.Remove(this);
		if (this.lastSkillNotification != null)
		{
			Game.Instance.GetComponent<Notifier>().Remove(this.lastSkillNotification);
			this.lastSkillNotification = null;
		}
		base.OnCleanUp();
	}

	// Token: 0x060050E9 RID: 20713 RVA: 0x001D5F40 File Offset: 0x001D4140
	public bool HasMasteredSkill(string skillId)
	{
		return this.MasteryBySkillID.ContainsKey(skillId) && this.MasteryBySkillID[skillId];
	}

	// Token: 0x060050EA RID: 20714 RVA: 0x001D5F60 File Offset: 0x001D4160
	public void UpdateUrge()
	{
		if (this.targetHat != this.currentHat)
		{
			if (!base.gameObject.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.LearnSkill))
			{
				base.gameObject.GetComponent<ChoreConsumer>().AddUrge(Db.Get().Urges.LearnSkill);
				return;
			}
		}
		else
		{
			base.gameObject.GetComponent<ChoreConsumer>().RemoveUrge(Db.Get().Urges.LearnSkill);
		}
	}

	// Token: 0x170005AD RID: 1453
	// (get) Token: 0x060050EB RID: 20715 RVA: 0x001D5FE0 File Offset: 0x001D41E0
	public string CurrentRole
	{
		get
		{
			return this.currentRole;
		}
	}

	// Token: 0x170005AE RID: 1454
	// (get) Token: 0x060050EC RID: 20716 RVA: 0x001D5FE8 File Offset: 0x001D41E8
	public string CurrentHat
	{
		get
		{
			return this.currentHat;
		}
	}

	// Token: 0x170005AF RID: 1455
	// (get) Token: 0x060050ED RID: 20717 RVA: 0x001D5FF0 File Offset: 0x001D41F0
	public string TargetHat
	{
		get
		{
			return this.targetHat;
		}
	}

	// Token: 0x060050EE RID: 20718 RVA: 0x001D5FF8 File Offset: 0x001D41F8
	public void SetHats(string current, string target)
	{
		this.currentHat = current;
		this.targetHat = target;
	}

	// Token: 0x060050EF RID: 20719 RVA: 0x001D6008 File Offset: 0x001D4208
	public void ClearAdditionalHats()
	{
		this.AdditionalHats.Clear();
	}

	// Token: 0x060050F0 RID: 20720 RVA: 0x001D6018 File Offset: 0x001D4218
	public void AddAdditionalHat(string context, string hat)
	{
		MinionResume.HatInfo hatInfo = null;
		foreach (MinionResume.HatInfo hatInfo2 in this.AdditionalHats)
		{
			if (hatInfo2.Source == context && hatInfo2.Hat == hat)
			{
				hatInfo = hatInfo2;
				break;
			}
		}
		if (hatInfo != null)
		{
			hatInfo.count++;
			return;
		}
		this.AdditionalHats.Add(new MinionResume.HatInfo(context, hat));
	}

	// Token: 0x060050F1 RID: 20721 RVA: 0x001D60AC File Offset: 0x001D42AC
	public void RemoveAdditionalHat(string context, string hat)
	{
		MinionResume.HatInfo hatInfo = null;
		foreach (MinionResume.HatInfo hatInfo2 in this.AdditionalHats)
		{
			if (hatInfo2.Source == context && hatInfo2.Hat == hat)
			{
				hatInfo2.count--;
				hatInfo = hatInfo2;
				break;
			}
		}
		if (hatInfo != null && hatInfo.count <= 0)
		{
			this.AdditionalHats.Remove(hatInfo);
			if (this.currentHat == hat)
			{
				this.RemoveHat();
			}
		}
	}

	// Token: 0x060050F2 RID: 20722 RVA: 0x001D6158 File Offset: 0x001D4358
	public void SetCurrentRole(string role_id)
	{
		this.currentRole = role_id;
	}

	// Token: 0x170005B0 RID: 1456
	// (get) Token: 0x060050F3 RID: 20723 RVA: 0x001D6161 File Offset: 0x001D4361
	public string TargetRole
	{
		get
		{
			return this.targetRole;
		}
	}

	// Token: 0x060050F4 RID: 20724 RVA: 0x001D616C File Offset: 0x001D436C
	public void ApplyAdditionalSkillPerks(SkillPerk[] perks)
	{
		foreach (SkillPerk skillPerk in perks)
		{
			if (Game.IsCorrectDlcActiveForCurrentSave(skillPerk))
			{
				this.AdditionalGrantedSkillPerkIDs.Add(skillPerk.IdHash);
				if (skillPerk.OnApply != null)
				{
					skillPerk.OnApply(this);
				}
			}
		}
		Game.Instance.Trigger(-1523247426, null);
	}

	// Token: 0x060050F5 RID: 20725 RVA: 0x001D61CC File Offset: 0x001D43CC
	public void RemoveAdditionalSkillPerks(SkillPerk[] perks)
	{
		foreach (SkillPerk skillPerk in perks)
		{
			if (Game.IsCorrectDlcActiveForCurrentSave(skillPerk))
			{
				this.AdditionalGrantedSkillPerkIDs.Remove(skillPerk.IdHash);
				if (skillPerk.OnRemove != null)
				{
					skillPerk.OnRemove(this);
				}
			}
		}
	}

	// Token: 0x060050F6 RID: 20726 RVA: 0x001D621C File Offset: 0x001D441C
	private void ApplySkillPerksForSkill(string skillId)
	{
		foreach (SkillPerk skillPerk in Db.Get().Skills.Get(skillId).perks)
		{
			if (Game.IsCorrectDlcActiveForCurrentSave(skillPerk) && skillPerk.OnApply != null)
			{
				skillPerk.OnApply(this);
			}
		}
	}

	// Token: 0x060050F7 RID: 20727 RVA: 0x001D6294 File Offset: 0x001D4494
	private void RemoveSkillPerksForSkill(string skillId)
	{
		foreach (SkillPerk skillPerk in Db.Get().Skills.Get(skillId).perks)
		{
			if (Game.IsCorrectDlcActiveForCurrentSave(skillPerk) && skillPerk.OnRemove != null)
			{
				skillPerk.OnRemove(this);
			}
		}
	}

	// Token: 0x060050F8 RID: 20728 RVA: 0x001D630C File Offset: 0x001D450C
	public void Sim200ms(float dt)
	{
		this.DEBUG_SecondsAlive += dt;
		if (!base.GetComponent<KPrefabID>().HasTag(GameTags.Dead))
		{
			this.DEBUG_PassiveExperienceGained += dt * SKILLS.PASSIVE_EXPERIENCE_PORTION;
			this.AddExperience(dt * SKILLS.PASSIVE_EXPERIENCE_PORTION);
		}
	}

	// Token: 0x060050F9 RID: 20729 RVA: 0x001D635C File Offset: 0x001D455C
	public bool IsAbleToLearnSkill(string skillId)
	{
		Skill skill = Db.Get().Skills.Get(skillId);
		string choreGroupID = Db.Get().SkillGroups.Get(skill.skillGroup).choreGroupID;
		if (!string.IsNullOrEmpty(choreGroupID))
		{
			Traits component = base.GetComponent<Traits>();
			if (component != null && component.IsChoreGroupDisabled(choreGroupID))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060050FA RID: 20730 RVA: 0x001D63C0 File Offset: 0x001D45C0
	public bool BelowMoraleExpectation(Skill skill)
	{
		float num = Db.Get().Attributes.QualityOfLife.Lookup(this).GetTotalValue();
		float totalValue = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(this).GetTotalValue();
		int moraleExpectation = skill.GetMoraleExpectation();
		if (this.AptitudeBySkillGroup.ContainsKey(skill.skillGroup) && this.AptitudeBySkillGroup[skill.skillGroup] > 0f)
		{
			num += 1f;
		}
		return totalValue + (float)moraleExpectation <= num;
	}

	// Token: 0x060050FB RID: 20731 RVA: 0x001D6450 File Offset: 0x001D4650
	public bool HasMasteredDirectlyRequiredSkillsForSkill(Skill skill)
	{
		for (int i = 0; i < skill.priorSkills.Count; i++)
		{
			if (!this.HasMasteredSkill(skill.priorSkills[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060050FC RID: 20732 RVA: 0x001D648A File Offset: 0x001D468A
	public bool HasSkillPointsRequiredForSkill(Skill skill)
	{
		return this.AvailableSkillpoints >= 1;
	}

	// Token: 0x060050FD RID: 20733 RVA: 0x001D6498 File Offset: 0x001D4698
	public bool HasSkillAptitude(Skill skill)
	{
		return this.AptitudeBySkillGroup.ContainsKey(skill.skillGroup) && this.AptitudeBySkillGroup[skill.skillGroup] > 0f;
	}

	// Token: 0x060050FE RID: 20734 RVA: 0x001D64D2 File Offset: 0x001D46D2
	public bool HasBeenGrantedSkill(Skill skill)
	{
		return this.GrantedSkillIDs != null && this.GrantedSkillIDs.Contains(skill.Id);
	}

	// Token: 0x060050FF RID: 20735 RVA: 0x001D64F4 File Offset: 0x001D46F4
	public bool HasBeenGrantedSkill(string id)
	{
		return this.GrantedSkillIDs != null && this.GrantedSkillIDs.Contains(id);
	}

	// Token: 0x06005100 RID: 20736 RVA: 0x001D6514 File Offset: 0x001D4714
	public MinionResume.SkillMasteryConditions[] GetSkillMasteryConditions(string skillId)
	{
		List<MinionResume.SkillMasteryConditions> list = new List<MinionResume.SkillMasteryConditions>();
		Skill skill = Db.Get().Skills.Get(skillId);
		if (this.HasSkillAptitude(skill))
		{
			list.Add(MinionResume.SkillMasteryConditions.SkillAptitude);
		}
		if (!this.BelowMoraleExpectation(skill))
		{
			list.Add(MinionResume.SkillMasteryConditions.StressWarning);
		}
		if (!this.IsAbleToLearnSkill(skillId))
		{
			list.Add(MinionResume.SkillMasteryConditions.UnableToLearn);
		}
		if (!this.HasSkillPointsRequiredForSkill(skill))
		{
			list.Add(MinionResume.SkillMasteryConditions.NeedsSkillPoints);
		}
		if (!this.HasMasteredDirectlyRequiredSkillsForSkill(skill))
		{
			list.Add(MinionResume.SkillMasteryConditions.MissingPreviousSkill);
		}
		return list.ToArray();
	}

	// Token: 0x06005101 RID: 20737 RVA: 0x001D658E File Offset: 0x001D478E
	public bool CanMasterSkill(MinionResume.SkillMasteryConditions[] masteryConditions)
	{
		return !Array.Exists<MinionResume.SkillMasteryConditions>(masteryConditions, (MinionResume.SkillMasteryConditions element) => element == MinionResume.SkillMasteryConditions.UnableToLearn || element == MinionResume.SkillMasteryConditions.NeedsSkillPoints || element == MinionResume.SkillMasteryConditions.MissingPreviousSkill);
	}

	// Token: 0x06005102 RID: 20738 RVA: 0x001D65BC File Offset: 0x001D47BC
	public bool OwnsHat(string hatId)
	{
		using (List<MinionResume.HatInfo>.Enumerator enumerator = this.AdditionalHats.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Hat == hatId)
				{
					return true;
				}
			}
		}
		return this.ownedHats.ContainsKey(hatId) && this.ownedHats[hatId];
	}

	// Token: 0x06005103 RID: 20739 RVA: 0x001D6638 File Offset: 0x001D4838
	public List<MinionResume.HatInfo> GetAllHats()
	{
		List<MinionResume.HatInfo> list = new List<MinionResume.HatInfo>();
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value)
			{
				Skill skill = Db.Get().Skills.TryGet(keyValuePair.Key);
				if (!skill.hat.IsNullOrWhiteSpace())
				{
					list.Add(new MinionResume.HatInfo(skill.Name, skill.hat));
				}
			}
		}
		list.AddRange(this.AdditionalHats);
		return list;
	}

	// Token: 0x06005104 RID: 20740 RVA: 0x001D66DC File Offset: 0x001D48DC
	public void SkillLearned()
	{
		if (base.gameObject.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.LearnSkill))
		{
			base.gameObject.GetComponent<ChoreConsumer>().RemoveUrge(Db.Get().Urges.LearnSkill);
		}
		foreach (string text in this.ownedHats.Keys.ToList<string>())
		{
			this.ownedHats[text] = true;
		}
		if (this.targetHat != null && this.currentHat != this.targetHat)
		{
			this.CreateHatChangeChore();
		}
	}

	// Token: 0x06005105 RID: 20741 RVA: 0x001D67A0 File Offset: 0x001D49A0
	public void CreateHatChangeChore()
	{
		if (this.lastHatChore != null)
		{
			this.lastHatChore.Cancel("New Hat");
		}
		this.lastHatChore = new PutOnHatChore(this, Db.Get().ChoreTypes.SwitchHat);
	}

	// Token: 0x06005106 RID: 20742 RVA: 0x001D67D8 File Offset: 0x001D49D8
	public void MasterSkill(string skillId)
	{
		if (!base.gameObject.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.LearnSkill))
		{
			base.gameObject.GetComponent<ChoreConsumer>().AddUrge(Db.Get().Urges.LearnSkill);
		}
		this.MasteryBySkillID[skillId] = true;
		this.ApplySkillPerksForSkill(skillId);
		this.UpdateExpectations();
		this.UpdateMorale();
		this.TriggerMasterSkillEvents();
		GameScheduler.Instance.Schedule("Morale Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Morale, true);
		}, null, null);
		if (!this.ownedHats.ContainsKey(Db.Get().Skills.Get(skillId).hat))
		{
			this.ownedHats.Add(Db.Get().Skills.Get(skillId).hat, false);
		}
		if (this.AvailableSkillpoints == 0 && this.lastSkillNotification != null)
		{
			Game.Instance.GetComponent<Notifier>().Remove(this.lastSkillNotification);
			this.lastSkillNotification = null;
		}
	}

	// Token: 0x06005107 RID: 20743 RVA: 0x001D68F0 File Offset: 0x001D4AF0
	public void UnmasterSkill(string skillId)
	{
		if (this.MasteryBySkillID.ContainsKey(skillId))
		{
			this.MasteryBySkillID.Remove(skillId);
			this.RemoveSkillPerksForSkill(skillId);
			this.UpdateExpectations();
			this.UpdateMorale();
			this.TriggerMasterSkillEvents();
		}
	}

	// Token: 0x06005108 RID: 20744 RVA: 0x001D6928 File Offset: 0x001D4B28
	public void GrantSkill(string skillId)
	{
		if (this.GrantedSkillIDs == null)
		{
			this.GrantedSkillIDs = new List<string>();
		}
		if (!this.HasBeenGrantedSkill(skillId))
		{
			this.MasteryBySkillID[skillId] = true;
			this.ApplySkillPerksForSkill(skillId);
			this.GrantedSkillIDs.Add(skillId);
			this.UpdateExpectations();
			this.UpdateMorale();
			this.TriggerMasterSkillEvents();
			if (!this.ownedHats.ContainsKey(Db.Get().Skills.Get(skillId).hat))
			{
				this.ownedHats.Add(Db.Get().Skills.Get(skillId).hat, false);
			}
		}
	}

	// Token: 0x06005109 RID: 20745 RVA: 0x001D69C8 File Offset: 0x001D4BC8
	public void UngrantSkill(string skillId)
	{
		if (this.GrantedSkillIDs != null)
		{
			this.GrantedSkillIDs.RemoveAll((string match) => match == skillId);
		}
		this.UnmasterSkill(skillId);
	}

	// Token: 0x0600510A RID: 20746 RVA: 0x001D6A10 File Offset: 0x001D4C10
	public Sprite GetSkillGrantSourceIcon(string skillID)
	{
		if (!this.GrantedSkillIDs.Contains(skillID))
		{
			return null;
		}
		BionicUpgradesMonitor.Instance smi = base.gameObject.GetSMI<BionicUpgradesMonitor.Instance>();
		if (smi != null)
		{
			foreach (BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot in smi.upgradeComponentSlots)
			{
				if (upgradeComponentSlot.HasUpgradeInstalled)
				{
					return Def.GetUISprite(upgradeComponentSlot.installedUpgradeComponent.gameObject, "ui", false).first;
				}
			}
		}
		return Assets.GetSprite("skill_granted_trait");
	}

	// Token: 0x0600510B RID: 20747 RVA: 0x001D6A88 File Offset: 0x001D4C88
	private void TriggerMasterSkillEvents()
	{
		base.Trigger(540773776, null);
		Game.Instance.Trigger(-1523247426, this);
	}

	// Token: 0x0600510C RID: 20748 RVA: 0x001D6AA6 File Offset: 0x001D4CA6
	public void ForceSetSkillPoints(int points)
	{
		this.totalExperienceGained = MinionResume.CalculatePreviousExperienceBar(points);
	}

	// Token: 0x0600510D RID: 20749 RVA: 0x001D6AB4 File Offset: 0x001D4CB4
	public void ForceAddSkillPoint()
	{
		this.AddExperience(MinionResume.CalculateNextExperienceBar(this.TotalSkillPointsGained) - this.totalExperienceGained);
	}

	// Token: 0x0600510E RID: 20750 RVA: 0x001D6ACE File Offset: 0x001D4CCE
	public static float CalculateNextExperienceBar(int current_skill_points)
	{
		return Mathf.Pow((float)(current_skill_points + 1) / (float)SKILLS.TARGET_SKILLS_EARNED, SKILLS.EXPERIENCE_LEVEL_POWER) * (float)SKILLS.TARGET_SKILLS_CYCLE * 600f;
	}

	// Token: 0x0600510F RID: 20751 RVA: 0x001D6AF2 File Offset: 0x001D4CF2
	public static float CalculatePreviousExperienceBar(int current_skill_points)
	{
		return Mathf.Pow((float)current_skill_points / (float)SKILLS.TARGET_SKILLS_EARNED, SKILLS.EXPERIENCE_LEVEL_POWER) * (float)SKILLS.TARGET_SKILLS_CYCLE * 600f;
	}

	// Token: 0x06005110 RID: 20752 RVA: 0x001D6B14 File Offset: 0x001D4D14
	private void UpdateExpectations()
	{
		int num = 0;
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && !this.HasBeenGrantedSkill(keyValuePair.Key))
			{
				Skill skill = Db.Get().Skills.Get(keyValuePair.Key);
				num += skill.tier + 1;
			}
		}
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(this);
		if (this.skillsMoraleExpectationModifier != null)
		{
			attributeInstance.Remove(this.skillsMoraleExpectationModifier);
			this.skillsMoraleExpectationModifier = null;
		}
		if (num > 0)
		{
			this.skillsMoraleExpectationModifier = new AttributeModifier(attributeInstance.Id, (float)num, DUPLICANTS.NEEDS.QUALITYOFLIFE.EXPECTATION_MOD_NAME, false, false, true);
			attributeInstance.Add(this.skillsMoraleExpectationModifier);
		}
	}

	// Token: 0x06005111 RID: 20753 RVA: 0x001D6C00 File Offset: 0x001D4E00
	private void UpdateMorale()
	{
		int num = 0;
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && !this.HasBeenGrantedSkill(keyValuePair.Key))
			{
				Skill skill = Db.Get().Skills.Get(keyValuePair.Key);
				float num2 = 0f;
				if (this.AptitudeBySkillGroup.TryGetValue(new HashedString(skill.skillGroup), out num2))
				{
					num += (int)num2;
				}
			}
		}
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(this);
		if (this.skillsMoraleModifier != null)
		{
			attributeInstance.Remove(this.skillsMoraleModifier);
			this.skillsMoraleModifier = null;
		}
		if (num > 0)
		{
			this.skillsMoraleModifier = new AttributeModifier(attributeInstance.Id, (float)num, DUPLICANTS.NEEDS.QUALITYOFLIFE.APTITUDE_SKILLS_MOD_NAME, false, false, true);
			attributeInstance.Add(this.skillsMoraleModifier);
		}
	}

	// Token: 0x06005112 RID: 20754 RVA: 0x001D6D08 File Offset: 0x001D4F08
	private void OnSkillPointGained()
	{
		Game.Instance.Trigger(1505456302, this);
		this.ShowNewSkillPointNotification();
		if (PopFXManager.Instance != null)
		{
			string text = MISC.NOTIFICATIONS.SKILL_POINT_EARNED.NAME.Replace("{Duplicant}", this.identity.GetProperName());
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, text, base.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
		}
		new UpgradeFX.Instance(base.gameObject.GetComponent<KMonoBehaviour>(), new Vector3(0f, 0f, -0.1f)).StartSM();
	}

	// Token: 0x06005113 RID: 20755 RVA: 0x001D6DB4 File Offset: 0x001D4FB4
	private void ShowNewSkillPointNotification()
	{
		if (this.AvailableSkillpoints == 1)
		{
			this.lastSkillNotification = new ManagementMenuNotification(global::Action.ManageSkills, NotificationValence.Good, this.identity.GetSoleOwner().gameObject.GetInstanceID().ToString(), MISC.NOTIFICATIONS.SKILL_POINT_EARNED.NAME.Replace("{Duplicant}", this.identity.GetProperName()), NotificationType.Good, new Func<List<Notification>, object, string>(this.GetSkillPointGainedTooltip), this.identity, true, 0f, delegate(object d)
			{
				ManagementMenu.Instance.OpenSkills(this.identity);
			}, null, null, true);
			base.GetComponent<Notifier>().Add(this.lastSkillNotification, "");
		}
	}

	// Token: 0x06005114 RID: 20756 RVA: 0x001D6E50 File Offset: 0x001D5050
	private string GetSkillPointGainedTooltip(List<Notification> notifications, object data)
	{
		return MISC.NOTIFICATIONS.SKILL_POINT_EARNED.TOOLTIP.Replace("{Duplicant}", ((MinionIdentity)data).GetProperName());
	}

	// Token: 0x06005115 RID: 20757 RVA: 0x001D6E6C File Offset: 0x001D506C
	public void SetAptitude(HashedString skillGroupID, float amount)
	{
		this.AptitudeBySkillGroup[skillGroupID] = amount;
	}

	// Token: 0x06005116 RID: 20758 RVA: 0x001D6E7C File Offset: 0x001D507C
	public float GetAptitudeExperienceMultiplier(HashedString skillGroupId, float buildingFrequencyMultiplier)
	{
		float num = 0f;
		this.AptitudeBySkillGroup.TryGetValue(skillGroupId, out num);
		return 1f + num * SKILLS.APTITUDE_EXPERIENCE_MULTIPLIER * buildingFrequencyMultiplier;
	}

	// Token: 0x06005117 RID: 20759 RVA: 0x001D6EB0 File Offset: 0x001D50B0
	public void AddExperience(float amount)
	{
		float num = this.totalExperienceGained;
		float num2 = MinionResume.CalculateNextExperienceBar(this.TotalSkillPointsGained);
		this.totalExperienceGained += amount;
		if (base.isSpawned && this.totalExperienceGained >= num2 && num < num2)
		{
			this.OnSkillPointGained();
		}
	}

	// Token: 0x06005118 RID: 20760 RVA: 0x001D6EFC File Offset: 0x001D50FC
	public override void AddExperienceWithAptitude(string skillGroupId, float amount, float buildingMultiplier)
	{
		float num = amount * this.GetAptitudeExperienceMultiplier(skillGroupId, buildingMultiplier) * SKILLS.ACTIVE_EXPERIENCE_PORTION;
		this.DEBUG_ActiveExperienceGained += num;
		this.AddExperience(num);
	}

	// Token: 0x06005119 RID: 20761 RVA: 0x001D6F34 File Offset: 0x001D5134
	public bool HasPerk(HashedString perkId)
	{
		using (List<HashedString>.Enumerator enumerator = this.AdditionalGrantedSkillPerkIDs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == perkId)
				{
					return true;
				}
			}
		}
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && Db.Get().Skills.Get(keyValuePair.Key).GivesPerk(perkId))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600511A RID: 20762 RVA: 0x001D6FF4 File Offset: 0x001D51F4
	public bool HasPerk(SkillPerk perk)
	{
		using (List<HashedString>.Enumerator enumerator = this.AdditionalGrantedSkillPerkIDs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == perk.IdHash)
				{
					return true;
				}
			}
		}
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value && Db.Get().Skills.Get(keyValuePair.Key).GivesPerk(perk))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600511B RID: 20763 RVA: 0x001D70BC File Offset: 0x001D52BC
	public void RemoveHat()
	{
		MinionResume.RemoveHat(base.GetComponent<KBatchedAnimController>());
		this.currentHat = null;
		this.targetHat = null;
	}

	// Token: 0x0600511C RID: 20764 RVA: 0x001D70D8 File Offset: 0x001D52D8
	public static void RemoveHat(KBatchedAnimController controller)
	{
		AccessorySlot hat = Db.Get().AccessorySlots.Hat;
		Accessorizer component = controller.GetComponent<Accessorizer>();
		if (component != null)
		{
			Accessory accessory = component.GetAccessory(hat);
			if (accessory != null)
			{
				component.RemoveAccessory(accessory);
			}
		}
		else
		{
			controller.GetComponent<SymbolOverrideController>().TryRemoveSymbolOverride(hat.targetSymbolId, 4);
		}
		controller.SetSymbolVisiblity(hat.targetSymbolId, false);
		controller.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, false);
		controller.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, true);
	}

	// Token: 0x0600511D RID: 20765 RVA: 0x001D7174 File Offset: 0x001D5374
	public static void AddHat(string hat_id, KBatchedAnimController controller)
	{
		AccessorySlot hat = Db.Get().AccessorySlots.Hat;
		Accessory accessory = hat.Lookup(hat_id);
		if (accessory == null)
		{
			global::Debug.LogWarning("Missing hat: " + hat_id);
		}
		Accessorizer component = controller.GetComponent<Accessorizer>();
		if (component != null)
		{
			Accessory accessory2 = component.GetAccessory(Db.Get().AccessorySlots.Hat);
			if (accessory2 != null)
			{
				component.RemoveAccessory(accessory2);
			}
			if (accessory != null)
			{
				component.AddAccessory(accessory);
			}
		}
		else
		{
			SymbolOverrideController component2 = controller.GetComponent<SymbolOverrideController>();
			component2.TryRemoveSymbolOverride(hat.targetSymbolId, 4);
			component2.AddSymbolOverride(hat.targetSymbolId, accessory.symbol, 4);
		}
		controller.SetSymbolVisiblity(hat.targetSymbolId, true);
		controller.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, true);
		controller.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, false);
	}

	// Token: 0x0600511E RID: 20766 RVA: 0x001D725C File Offset: 0x001D545C
	public void ApplyTargetHat()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		MinionResume.ApplyHat(this.targetHat, component);
		this.currentHat = this.targetHat;
		this.targetHat = null;
	}

	// Token: 0x0600511F RID: 20767 RVA: 0x001D728F File Offset: 0x001D548F
	public static void ApplyHat(string hat_id, KBatchedAnimController controller)
	{
		if (hat_id.IsNullOrWhiteSpace())
		{
			MinionResume.RemoveHat(controller);
			return;
		}
		MinionResume.AddHat(hat_id, controller);
	}

	// Token: 0x06005120 RID: 20768 RVA: 0x001D72A7 File Offset: 0x001D54A7
	public string GetSkillsSubtitle()
	{
		return string.Format(DUPLICANTS.NEEDS.QUALITYOFLIFE.TOTAL_SKILL_POINTS, this.TotalSkillPointsGained);
	}

	// Token: 0x06005121 RID: 20769 RVA: 0x001D72C4 File Offset: 0x001D54C4
	public static bool AnyMinionHasPerk(string perk, int worldId = -1)
	{
		using (List<MinionResume>.Enumerator enumerator = ((worldId >= 0) ? Components.MinionResumes.GetWorldItems(worldId, true) : Components.MinionResumes.Items).Where((MinionResume minion) => !minion.HasTag(GameTags.Dead)).ToList<MinionResume>().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.HasPerk(perk))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06005122 RID: 20770 RVA: 0x001D7364 File Offset: 0x001D5564
	public static bool AnyOtherMinionHasPerk(string perk, MinionResume me)
	{
		foreach (MinionResume minionResume in Components.MinionResumes.Items)
		{
			if (!(minionResume == me) && minionResume.HasPerk(perk))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005123 RID: 20771 RVA: 0x001D73D4 File Offset: 0x001D55D4
	public void ResetSkillLevels(bool returnSkillPoints = true)
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, bool> keyValuePair in this.MasteryBySkillID)
		{
			if (keyValuePair.Value)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (string text in list)
		{
			this.UnmasterSkill(text);
		}
	}

	// Token: 0x04003666 RID: 13926
	[MyCmpReq]
	private MinionIdentity identity;

	// Token: 0x04003667 RID: 13927
	[Serialize]
	public Dictionary<string, bool> MasteryByRoleID = new Dictionary<string, bool>();

	// Token: 0x04003668 RID: 13928
	[Serialize]
	public Dictionary<string, bool> MasteryBySkillID = new Dictionary<string, bool>();

	// Token: 0x04003669 RID: 13929
	[Serialize]
	public List<string> GrantedSkillIDs = new List<string>();

	// Token: 0x0400366A RID: 13930
	private List<HashedString> AdditionalGrantedSkillPerkIDs = new List<HashedString>();

	// Token: 0x0400366B RID: 13931
	private List<MinionResume.HatInfo> AdditionalHats = new List<MinionResume.HatInfo>();

	// Token: 0x0400366C RID: 13932
	[Serialize]
	public Dictionary<HashedString, float> AptitudeByRoleGroup = new Dictionary<HashedString, float>();

	// Token: 0x0400366D RID: 13933
	[Serialize]
	public Dictionary<HashedString, float> AptitudeBySkillGroup = new Dictionary<HashedString, float>();

	// Token: 0x0400366E RID: 13934
	[Serialize]
	private string currentRole = "NoRole";

	// Token: 0x0400366F RID: 13935
	[Serialize]
	private string targetRole = "NoRole";

	// Token: 0x04003670 RID: 13936
	[Serialize]
	private string currentHat;

	// Token: 0x04003671 RID: 13937
	[Serialize]
	private string targetHat;

	// Token: 0x04003672 RID: 13938
	private Dictionary<string, bool> ownedHats = new Dictionary<string, bool>();

	// Token: 0x04003673 RID: 13939
	[Serialize]
	private float totalExperienceGained;

	// Token: 0x04003674 RID: 13940
	private Notification lastSkillNotification;

	// Token: 0x04003675 RID: 13941
	private PutOnHatChore lastHatChore;

	// Token: 0x04003676 RID: 13942
	private AttributeModifier skillsMoraleExpectationModifier;

	// Token: 0x04003677 RID: 13943
	private AttributeModifier skillsMoraleModifier;

	// Token: 0x04003678 RID: 13944
	public float DEBUG_PassiveExperienceGained;

	// Token: 0x04003679 RID: 13945
	public float DEBUG_ActiveExperienceGained;

	// Token: 0x0400367A RID: 13946
	public float DEBUG_SecondsAlive;

	// Token: 0x02001BC7 RID: 7111
	public class HatInfo
	{
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600A889 RID: 43145 RVA: 0x003B4B0C File Offset: 0x003B2D0C
		public string Source { get; }

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600A88A RID: 43146 RVA: 0x003B4B14 File Offset: 0x003B2D14
		public string Hat { get; }

		// Token: 0x0600A88B RID: 43147 RVA: 0x003B4B1C File Offset: 0x003B2D1C
		public HatInfo(string source, string hat)
		{
			this.Source = source;
			this.Hat = hat;
			this.count = 1;
		}

		// Token: 0x040083F3 RID: 33779
		public int count;
	}

	// Token: 0x02001BC8 RID: 7112
	public enum SkillMasteryConditions
	{
		// Token: 0x040083F5 RID: 33781
		SkillAptitude,
		// Token: 0x040083F6 RID: 33782
		StressWarning,
		// Token: 0x040083F7 RID: 33783
		UnableToLearn,
		// Token: 0x040083F8 RID: 33784
		NeedsSkillPoints,
		// Token: 0x040083F9 RID: 33785
		MissingPreviousSkill
	}
}
