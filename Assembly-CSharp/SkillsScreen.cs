using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Database;
using Klei.AI;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E4F RID: 3663
public class SkillsScreen : KModalScreen
{
	// Token: 0x0600747F RID: 29823 RVA: 0x002C5BF8 File Offset: 0x002C3DF8
	public override float GetSortKey()
	{
		if (base.isEditing)
		{
			return 50f;
		}
		return 20f;
	}

	// Token: 0x17000808 RID: 2056
	// (get) Token: 0x06007480 RID: 29824 RVA: 0x002C5C0D File Offset: 0x002C3E0D
	// (set) Token: 0x06007481 RID: 29825 RVA: 0x002C5C2C File Offset: 0x002C3E2C
	public IAssignableIdentity CurrentlySelectedMinion
	{
		get
		{
			if (this.currentlySelectedMinion == null || this.currentlySelectedMinion.IsNull())
			{
				return null;
			}
			return this.currentlySelectedMinion;
		}
		set
		{
			this.currentlySelectedMinion = value;
			if (base.IsActive())
			{
				this.RefreshSelectedMinion();
				this.RefreshSkillWidgets();
				this.RefreshBoosters();
			}
		}
	}

	// Token: 0x06007482 RID: 29826 RVA: 0x002C5C4F File Offset: 0x002C3E4F
	protected override void OnSpawn()
	{
		ClusterManager.Instance.Subscribe(-1078710002, new Action<object>(this.WorldRemoved));
	}

	// Token: 0x06007483 RID: 29827 RVA: 0x002C5C70 File Offset: 0x002C3E70
	protected override void OnActivate()
	{
		base.ConsumeMouseScroll = true;
		base.OnActivate();
		this.BuildMinions();
		this.RefreshAll();
		this.SortRows((this.active_sort_method == null) ? this.compareByMinion : this.active_sort_method);
		Components.LiveMinionIdentities.OnAdd += this.OnAddMinionIdentity;
		Components.LiveMinionIdentities.OnRemove += this.OnRemoveMinionIdentity;
		this.CloseButton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
		MultiToggle multiToggle = this.dupeSortingToggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			this.SortRows(this.compareByMinion);
		}));
		MultiToggle multiToggle2 = this.moraleSortingToggle;
		multiToggle2.onClick = (global::System.Action)Delegate.Combine(multiToggle2.onClick, new global::System.Action(delegate
		{
			this.SortRows(this.compareByMorale);
		}));
		MultiToggle multiToggle3 = this.experienceSortingToggle;
		multiToggle3.onClick = (global::System.Action)Delegate.Combine(multiToggle3.onClick, new global::System.Action(delegate
		{
			this.SortRows(this.compareByExperience);
		}));
	}

	// Token: 0x06007484 RID: 29828 RVA: 0x002C5D80 File Offset: 0x002C3F80
	protected override void OnShow(bool show)
	{
		if (show)
		{
			if (this.CurrentlySelectedMinion == null && Components.LiveMinionIdentities.Count > 0)
			{
				this.CurrentlySelectedMinion = Components.LiveMinionIdentities.Items[0];
			}
			this.BuildMinions();
			if (this.boosterWidgets.Count == 0)
			{
				this.PopulateBoosters();
			}
			this.RefreshAll();
			this.SortRows((this.active_sort_method == null) ? this.compareByMinion : this.active_sort_method);
		}
		base.OnShow(show);
	}

	// Token: 0x06007485 RID: 29829 RVA: 0x002C5DFD File Offset: 0x002C3FFD
	public void RefreshAll()
	{
		this.dirty = false;
		this.RefreshSkillWidgets();
		this.RefreshSelectedMinion();
		this.RefreshBoosters();
		this.linesPending = true;
	}

	// Token: 0x06007486 RID: 29830 RVA: 0x002C5E1F File Offset: 0x002C401F
	private void RefreshSelectedMinion()
	{
		this.minionAnimWidget.SetPortraitAnimator(this.currentlySelectedMinion);
		this.RefreshProgressBars();
		this.RefreshHat();
	}

	// Token: 0x06007487 RID: 29831 RVA: 0x002C5E3E File Offset: 0x002C403E
	public void GetMinionIdentity(IAssignableIdentity assignableIdentity, out MinionIdentity minionIdentity, out StoredMinionIdentity storedMinionIdentity)
	{
		if (assignableIdentity is MinionAssignablesProxy)
		{
			minionIdentity = ((MinionAssignablesProxy)assignableIdentity).GetTargetGameObject().GetComponent<MinionIdentity>();
			storedMinionIdentity = ((MinionAssignablesProxy)assignableIdentity).GetTargetGameObject().GetComponent<StoredMinionIdentity>();
			return;
		}
		minionIdentity = assignableIdentity as MinionIdentity;
		storedMinionIdentity = assignableIdentity as StoredMinionIdentity;
	}

	// Token: 0x06007488 RID: 29832 RVA: 0x002C5E80 File Offset: 0x002C4080
	private void RefreshProgressBars()
	{
		if (this.currentlySelectedMinion == null || this.currentlySelectedMinion.IsNull())
		{
			return;
		}
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(this.currentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		HierarchyReferences component = this.expectationsTooltip.GetComponent<HierarchyReferences>();
		component.GetReference("Labels").gameObject.SetActive(minionIdentity != null);
		component.GetReference("MoraleBar").gameObject.SetActive(minionIdentity != null);
		component.GetReference("ExpectationBar").gameObject.SetActive(minionIdentity != null);
		component.GetReference("StoredMinion").gameObject.SetActive(minionIdentity == null);
		this.experienceProgressFill.gameObject.SetActive(minionIdentity != null);
		if (minionIdentity == null)
		{
			this.expectationsTooltip.SetSimpleTooltip(string.Format(UI.TABLESCREENS.INFORMATION_NOT_AVAILABLE_TOOLTIP, storedMinionIdentity.GetStorageReason(), this.currentlySelectedMinion.GetProperName()));
			this.experienceBarTooltip.SetSimpleTooltip(string.Format(UI.TABLESCREENS.INFORMATION_NOT_AVAILABLE_TOOLTIP, storedMinionIdentity.GetStorageReason(), this.currentlySelectedMinion.GetProperName()));
			this.EXPCount.text = "";
			this.duplicantLevelIndicator.text = UI.TABLESCREENS.NA;
			return;
		}
		MinionResume component2 = minionIdentity.GetComponent<MinionResume>();
		float num = MinionResume.CalculatePreviousExperienceBar(component2.TotalSkillPointsGained);
		float num2 = MinionResume.CalculateNextExperienceBar(component2.TotalSkillPointsGained);
		float num3 = (component2.TotalExperienceGained - num) / (num2 - num);
		this.EXPCount.text = Mathf.RoundToInt(component2.TotalExperienceGained - num).ToString() + " / " + Mathf.RoundToInt(num2 - num).ToString();
		this.duplicantLevelIndicator.text = component2.AvailableSkillpoints.ToString();
		this.experienceProgressFill.fillAmount = num3;
		this.experienceBarTooltip.SetSimpleTooltip(string.Format(UI.SKILLS_SCREEN.EXPERIENCE_TOOLTIP, Mathf.RoundToInt(num2 - num) - Mathf.RoundToInt(component2.TotalExperienceGained - num)));
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(component2);
		AttributeInstance attributeInstance2 = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(component2);
		float num4 = 0f;
		float num5 = 0f;
		if (!string.IsNullOrEmpty(this.hoveredSkillID) && !component2.HasMasteredSkill(this.hoveredSkillID))
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			list.Add(this.hoveredSkillID);
			while (list.Count > 0)
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					if (!component2.HasMasteredSkill(list[i]))
					{
						num4 += (float)(Db.Get().Skills.Get(list[i]).tier + 1);
						if (component2.AptitudeBySkillGroup.ContainsKey(Db.Get().Skills.Get(list[i]).skillGroup) && component2.AptitudeBySkillGroup[Db.Get().Skills.Get(list[i]).skillGroup] > 0f)
						{
							num5 += 1f;
						}
						foreach (string text in Db.Get().Skills.Get(list[i]).priorSkills)
						{
							list2.Add(text);
						}
					}
				}
				list.Clear();
				list.AddRange(list2);
				list2.Clear();
			}
		}
		float num6 = attributeInstance.GetTotalValue() + num5 / (attributeInstance2.GetTotalValue() + num4);
		float num7 = Mathf.Max(attributeInstance.GetTotalValue() + num5, attributeInstance2.GetTotalValue() + num4);
		while (this.moraleNotches.Count < Mathf.RoundToInt(num7))
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.moraleNotch, this.moraleNotch.transform.parent);
			gameObject.SetActive(true);
			this.moraleNotches.Add(gameObject);
		}
		while (this.moraleNotches.Count > Mathf.RoundToInt(num7))
		{
			GameObject gameObject2 = this.moraleNotches[this.moraleNotches.Count - 1];
			this.moraleNotches.Remove(gameObject2);
			global::UnityEngine.Object.Destroy(gameObject2);
		}
		for (int j = 0; j < this.moraleNotches.Count; j++)
		{
			if ((float)j < attributeInstance.GetTotalValue() + num5)
			{
				this.moraleNotches[j].GetComponentsInChildren<Image>()[1].color = this.moraleNotchColor;
			}
			else
			{
				this.moraleNotches[j].GetComponentsInChildren<Image>()[1].color = Color.clear;
			}
		}
		this.moraleProgressLabel.text = UI.SKILLS_SCREEN.MORALE + ": " + attributeInstance.GetTotalValue().ToString();
		if (num5 > 0f)
		{
			LocText locText = this.moraleProgressLabel;
			locText.text = locText.text + " + " + GameUtil.ApplyBoldString(GameUtil.ColourizeString(this.moraleNotchColor, num5.ToString()));
		}
		while (this.expectationNotches.Count < Mathf.RoundToInt(num7))
		{
			GameObject gameObject3 = global::UnityEngine.Object.Instantiate<GameObject>(this.expectationNotch, this.expectationNotch.transform.parent);
			gameObject3.SetActive(true);
			this.expectationNotches.Add(gameObject3);
		}
		while (this.expectationNotches.Count > Mathf.RoundToInt(num7))
		{
			GameObject gameObject4 = this.expectationNotches[this.expectationNotches.Count - 1];
			this.expectationNotches.Remove(gameObject4);
			global::UnityEngine.Object.Destroy(gameObject4);
		}
		for (int k = 0; k < this.expectationNotches.Count; k++)
		{
			if ((float)k < attributeInstance2.GetTotalValue() + num4)
			{
				if ((float)k < attributeInstance2.GetTotalValue())
				{
					this.expectationNotches[k].GetComponentsInChildren<Image>()[1].color = this.expectationNotchColor;
				}
				else
				{
					this.expectationNotches[k].GetComponentsInChildren<Image>()[1].color = this.expectationNotchProspectColor;
				}
			}
			else
			{
				this.expectationNotches[k].GetComponentsInChildren<Image>()[1].color = Color.clear;
			}
		}
		this.expectationsProgressLabel.text = UI.SKILLS_SCREEN.MORALE_EXPECTATION + ": " + attributeInstance2.GetTotalValue().ToString();
		if (num4 > 0f)
		{
			LocText locText2 = this.expectationsProgressLabel;
			locText2.text = locText2.text + " + " + GameUtil.ApplyBoldString(GameUtil.ColourizeString(this.expectationNotchColor, num4.ToString()));
		}
		if (num6 < 1f)
		{
			this.expectationWarning.SetActive(true);
			this.moraleWarning.SetActive(false);
		}
		else
		{
			this.expectationWarning.SetActive(false);
			this.moraleWarning.SetActive(true);
		}
		string text2 = "";
		Dictionary<string, float> dictionary = new Dictionary<string, float>();
		text2 = string.Concat(new string[]
		{
			text2,
			GameUtil.ApplyBoldString(UI.SKILLS_SCREEN.MORALE),
			": ",
			attributeInstance.GetTotalValue().ToString(),
			"\n"
		});
		for (int l = 0; l < attributeInstance.Modifiers.Count; l++)
		{
			dictionary.Add(attributeInstance.Modifiers[l].GetDescription(), attributeInstance.Modifiers[l].Value);
		}
		List<KeyValuePair<string, float>> list3 = dictionary.ToList<KeyValuePair<string, float>>();
		list3.Sort((KeyValuePair<string, float> pair1, KeyValuePair<string, float> pair2) => pair2.Value.CompareTo(pair1.Value));
		foreach (KeyValuePair<string, float> keyValuePair in list3)
		{
			text2 = string.Concat(new string[]
			{
				text2,
				"    • ",
				keyValuePair.Key,
				": ",
				(keyValuePair.Value > 0f) ? UIConstants.ColorPrefixGreen : UIConstants.ColorPrefixRed,
				keyValuePair.Value.ToString(),
				UIConstants.ColorSuffix,
				"\n"
			});
		}
		text2 += "\n";
		text2 = string.Concat(new string[]
		{
			text2,
			GameUtil.ApplyBoldString(UI.SKILLS_SCREEN.MORALE_EXPECTATION),
			": ",
			attributeInstance2.GetTotalValue().ToString(),
			"\n"
		});
		for (int m = 0; m < attributeInstance2.Modifiers.Count; m++)
		{
			text2 = string.Concat(new string[]
			{
				text2,
				"    • ",
				attributeInstance2.Modifiers[m].GetDescription(),
				": ",
				(attributeInstance2.Modifiers[m].Value > 0f) ? UIConstants.ColorPrefixRed : UIConstants.ColorPrefixGreen,
				attributeInstance2.Modifiers[m].GetFormattedString(),
				UIConstants.ColorSuffix,
				"\n"
			});
		}
		this.expectationsTooltip.SetSimpleTooltip(text2);
	}

	// Token: 0x06007489 RID: 29833 RVA: 0x002C6838 File Offset: 0x002C4A38
	private Tag SelectedMinionModel()
	{
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(this.currentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		if (minionIdentity != null)
		{
			return Db.Get().Personalities.Get(minionIdentity.personalityResourceId).model;
		}
		if (storedMinionIdentity != null)
		{
			return Db.Get().Personalities.Get(storedMinionIdentity.personalityResourceId).model;
		}
		return null;
	}

	// Token: 0x0600748A RID: 29834 RVA: 0x002C68A4 File Offset: 0x002C4AA4
	private void RefreshHat()
	{
		if (this.currentlySelectedMinion == null || this.currentlySelectedMinion.IsNull())
		{
			return;
		}
		List<IListableOption> list = new List<IListableOption>();
		string text = "";
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(this.currentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		if (minionIdentity != null)
		{
			MinionResume component = minionIdentity.GetComponent<MinionResume>();
			text = (string.IsNullOrEmpty(component.TargetHat) ? component.CurrentHat : component.TargetHat);
			foreach (MinionResume.HatInfo hatInfo in component.GetAllHats())
			{
				list.Add(new HatListable(hatInfo.Source, hatInfo.Hat));
			}
			this.hatDropDown.Initialize(list, new Action<IListableOption, object>(this.OnHatDropEntryClick), new Func<IListableOption, IListableOption, object, int>(this.hatDropDownSort), new Action<DropDownEntry, object>(this.hatDropEntryRefreshAction), false, this.currentlySelectedMinion);
		}
		else
		{
			text = (string.IsNullOrEmpty(storedMinionIdentity.targetHat) ? storedMinionIdentity.currentHat : storedMinionIdentity.targetHat);
		}
		this.hatDropDown.openButton.enabled = minionIdentity != null;
		this.selectedHat.transform.Find("Arrow").gameObject.SetActive(minionIdentity != null);
		this.selectedHat.sprite = Assets.GetSprite(string.IsNullOrEmpty(text) ? "hat_role_none" : text);
	}

	// Token: 0x0600748B RID: 29835 RVA: 0x002C6A28 File Offset: 0x002C4C28
	private void OnHatDropEntryClick(IListableOption skill, object data)
	{
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(this.currentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		if (minionIdentity == null)
		{
			return;
		}
		MinionResume component = minionIdentity.GetComponent<MinionResume>();
		string text = "hat_role_none";
		if (skill != null)
		{
			this.selectedHat.sprite = Assets.GetSprite((skill as HatListable).hat);
			if (component != null)
			{
				text = (skill as HatListable).hat;
				component.SetHats(component.CurrentHat, text);
				if (component.OwnsHat(text))
				{
					new PutOnHatChore(component, Db.Get().ChoreTypes.SwitchHat);
				}
			}
		}
		else
		{
			this.selectedHat.sprite = Assets.GetSprite(text);
			if (component != null)
			{
				component.SetHats(component.CurrentHat, null);
				component.ApplyTargetHat();
			}
		}
		IAssignableIdentity assignableIdentity = minionIdentity.assignableProxy.Get();
		foreach (SkillMinionWidget skillMinionWidget in this.sortableRows)
		{
			if (skillMinionWidget.assignableIdentity == assignableIdentity)
			{
				skillMinionWidget.RefreshHat(component.TargetHat);
			}
		}
	}

	// Token: 0x0600748C RID: 29836 RVA: 0x002C6B5C File Offset: 0x002C4D5C
	private void hatDropEntryRefreshAction(DropDownEntry entry, object targetData)
	{
		if (entry.entryData != null)
		{
			HatListable hatListable = entry.entryData as HatListable;
			entry.image.sprite = Assets.GetSprite(hatListable.hat);
		}
	}

	// Token: 0x0600748D RID: 29837 RVA: 0x002C6B98 File Offset: 0x002C4D98
	private int hatDropDownSort(IListableOption a, IListableOption b, object targetData)
	{
		return 0;
	}

	// Token: 0x0600748E RID: 29838 RVA: 0x002C6B9C File Offset: 0x002C4D9C
	private void Update()
	{
		if (this.dirty)
		{
			this.RefreshAll();
		}
		if (this.linesPending)
		{
			foreach (GameObject gameObject in this.skillWidgets.Values)
			{
				gameObject.GetComponent<SkillWidget>().RefreshLines();
			}
			this.linesPending = false;
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			this.scrollRect.AnalogUpdate(KInputManager.steamInputInterpreter.GetSteamCameraMovement() * this.scrollSpeed);
		}
	}

	// Token: 0x0600748F RID: 29839 RVA: 0x002C6C3C File Offset: 0x002C4E3C
	private void PopulateBoosters()
	{
		foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.BionicUpgrade))
		{
			Tag id = gameObject.GetComponent<KPrefabID>().PrefabID();
			GameObject gameObject2 = Util.KInstantiate(this.boosterPrefab, this.boosterContentGrid, gameObject.name);
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.SetActive(true);
			HierarchyReferences component = gameObject2.GetComponent<HierarchyReferences>();
			this.boosterWidgets.Add(gameObject.PrefabID(), component);
			component.GetReference<Image>("Icon").sprite = Def.GetUISprite(gameObject, "ui", false).first;
			gameObject2.GetComponentInChildren<LocText>().SetText(gameObject.GetProperName());
			KButton reference = component.GetReference<KButton>("AssignmentIncrementButton");
			reference.ClearOnClick();
			reference.onClick += delegate
			{
				this.IncrementBoosterAssignment(id);
			};
			KButton reference2 = component.GetReference<KButton>("AssignmentDecrementButton");
			reference2.ClearOnClick();
			reference2.onClick += delegate
			{
				this.DecrementBoosterAssignment(id);
			};
			foreach (GameObject gameObject3 in this.boosterSlotIcons)
			{
				Util.KDestroyGameObject(gameObject3);
			}
			this.boosterSlotIcons.Clear();
			for (int i = 0; i < 8; i++)
			{
				GameObject gameObject4 = Util.KInstantiateUI(this.boosterSlotIconPrefab, this.boosterSlotIconPrefab.transform.parent.gameObject, false);
				this.boosterSlotIcons.Add(gameObject4);
				int slotIdx = i;
				gameObject4.transform.GetChild(0).GetComponent<MultiToggle>().onClick = delegate
				{
					MinionIdentity minionIdentity;
					StoredMinionIdentity storedMinionIdentity;
					this.GetMinionIdentity(this.currentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
					if (minionIdentity == null)
					{
						return;
					}
					minionIdentity.GetSMI<BionicUpgradesMonitor.Instance>().upgradeComponentSlots[slotIdx].GetAssignableSlotInstance().Unassign(true);
					this.RefreshBoosters();
				};
			}
		}
	}

	// Token: 0x06007490 RID: 29840 RVA: 0x002C6E4C File Offset: 0x002C504C
	private void IncrementBoosterAssignment(Tag boosterType)
	{
		BionicUpgradeComponent bionicUpgradeComponent = this.FindAvailableBoosterOfType(boosterType);
		if (bionicUpgradeComponent != null)
		{
			bionicUpgradeComponent.Assign(this.CurrentlySelectedMinion);
		}
		this.RefreshBoosters();
	}

	// Token: 0x06007491 RID: 29841 RVA: 0x002C6E7C File Offset: 0x002C507C
	private void DecrementBoosterAssignment(Tag boosterType)
	{
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(this.currentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		BionicUpgradesMonitor.Instance smi = minionIdentity.GetSMI<BionicUpgradesMonitor.Instance>();
		if (smi == null)
		{
			bool flag = false;
			for (int i = smi.upgradeComponentSlots.Length - 1; i >= 0; i--)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = smi.upgradeComponentSlots[i];
				if (upgradeComponentSlot.assignedUpgradeComponent != null && upgradeComponentSlot.assignedUpgradeComponent.PrefabID() == boosterType && upgradeComponentSlot.HasUpgradeInstalled && upgradeComponentSlot.AssignedUpgradeMatchesInstalledUpgrade)
				{
					upgradeComponentSlot.GetAssignableSlotInstance().Unassign(true);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				for (int j = smi.upgradeComponentSlots.Length - 1; j >= 0; j--)
				{
					BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot2 = smi.upgradeComponentSlots[j];
					if (upgradeComponentSlot2.assignedUpgradeComponent != null && upgradeComponentSlot2.assignedUpgradeComponent.PrefabID() == boosterType)
					{
						upgradeComponentSlot2.GetAssignableSlotInstance().Unassign(true);
						break;
					}
				}
			}
		}
		this.RefreshBoosters();
	}

	// Token: 0x06007492 RID: 29842 RVA: 0x002C6F74 File Offset: 0x002C5174
	private BionicUpgradeComponent FindAvailableBoosterOfType(Tag boosterType)
	{
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(this.currentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		if (minionIdentity == null)
		{
			return null;
		}
		List<Pickupable> list = ClusterManager.Instance.GetWorld(minionIdentity.GetMyWorldId()).worldInventory.CreatePickupablesList(boosterType);
		if (list == null || list.Count == 0)
		{
			return null;
		}
		list = list.FindAll((Pickupable match) => match.GetComponent<BionicUpgradeComponent>().assignee == null);
		if (list == null || list.Count == 0)
		{
			return null;
		}
		using (List<Pickupable>.Enumerator enumerator = list.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				return enumerator.Current.GetComponent<BionicUpgradeComponent>();
			}
		}
		return null;
	}

	// Token: 0x06007493 RID: 29843 RVA: 0x002C7040 File Offset: 0x002C5240
	private void RefreshBoosters()
	{
		BionicUpgradesMonitor.Instance instance = null;
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(this.currentlySelectedMinion, out minionIdentity, out storedMinionIdentity);
		bool flag = this.SelectedMinionModel() == GameTags.Minions.Models.Bionic && minionIdentity != null;
		if (flag)
		{
			instance = minionIdentity.GetSMI<BionicUpgradesMonitor.Instance>();
			if (instance == null)
			{
				flag = false;
			}
		}
		if (flag)
		{
			this.equippedBoostersHeaderLabel.SetText(GameUtil.SafeStringFormat(UI.SKILLS_SCREEN.ASSIGNED_BOOSTERS_HEADER, new object[] { this.CurrentlySelectedMinion.GetProperName() }));
			this.assignedBoostersCountLabel.SetText(GameUtil.SafeStringFormat(UI.SKILLS_SCREEN.ASSIGNED_BOOSTERS_COUNT_LABEL, new object[] { instance.AssignedSlotCount, instance.UnlockedSlotCount }));
			this.boosterPanel.SetActive(true);
			this.boosterHeader.SetActive(true);
			float canvasScale = GameScreenManager.Instance.ssOverlayCanvas.GetComponent<KCanvasScaler>().GetCanvasScale();
			float num = (float)Screen.height / canvasScale * 0.4f;
			float num2 = 96f;
			this.skillsContainer.rectTransform().sizeDelta = new Vector2(0f, -1f * (num + num2));
			this.boosterPanel.rectTransform().sizeDelta = new Vector2(0f, num);
			this.boosterHeader.rectTransform().anchoredPosition = new Vector2(0f, num);
			for (int i = 0; i < this.boosterSlotIcons.Count; i++)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = instance.upgradeComponentSlots[7 - i];
				this.boosterSlotIcons[i].SetActive(true);
				if (i >= instance.upgradeComponentSlots.Length || instance.upgradeComponentSlots[i].IsLocked)
				{
					this.boosterSlotIcons[i].GetComponent<Image>().sprite = Assets.GetSprite("bionicUpgradeSlotLocked");
					this.boosterSlotIcons[i].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
					this.boosterSlotIcons[i].GetComponent<ToolTip>().SetSimpleTooltip(UI.SKILLS_SCREEN.BIONIC_UPGRADE_SLOT_LOCKED);
					this.boosterSlotIcons[i].transform.GetChild(0).gameObject.SetActive(false);
				}
				else if (instance.upgradeComponentSlots[i].assignedUpgradeComponent != null)
				{
					this.boosterSlotIcons[i].GetComponent<Image>().sprite = Def.GetUISprite(instance.upgradeComponentSlots[i].assignedUpgradeComponent.PrefabID(), "ui", false).first;
					this.boosterSlotIcons[i].GetComponent<ToolTip>().SetSimpleTooltip(instance.upgradeComponentSlots[i].assignedUpgradeComponent.GetProperName() + "\n\n" + UI.SKILLS_SCREEN.BIONIC_UPGRADE_SLOT_UNASSIGN);
					this.boosterSlotIcons[i].GetComponent<Image>().color = Color.white;
					this.boosterSlotIcons[i].transform.GetChild(0).gameObject.SetActive(true);
				}
				else
				{
					this.boosterSlotIcons[i].GetComponent<Image>().sprite = Assets.GetSprite("bionicUpgradeSlot");
					this.boosterSlotIcons[i].GetComponent<ToolTip>().SetSimpleTooltip(UI.SKILLS_SCREEN.BIONIC_UPGRADE_SLOT_AVAILABLE);
					this.boosterSlotIcons[i].GetComponent<Image>().color = Color.white;
					this.boosterSlotIcons[i].transform.GetChild(0).gameObject.SetActive(false);
				}
			}
			using (Dictionary<Tag, HierarchyReferences>.Enumerator enumerator = this.boosterWidgets.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<Tag, HierarchyReferences> widget = enumerator.Current;
					int num3 = 0;
					if (instance != null && instance.upgradeComponentSlots != null)
					{
						foreach (BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot2 in instance.upgradeComponentSlots)
						{
							if (upgradeComponentSlot2.assignedUpgradeComponent != null && upgradeComponentSlot2.assignedUpgradeComponent.PrefabID() == widget.Key)
							{
								num3++;
							}
						}
					}
					GameObject prefab = Assets.GetPrefab(widget.Key);
					TMP_Text reference = widget.Value.GetReference<LocText>("Label");
					string properName = prefab.GetProperName();
					reference.SetText(properName);
					float num4 = 0f;
					List<Pickupable> list = ClusterManager.Instance.GetWorld(minionIdentity.GetMyWorldId()).worldInventory.CreatePickupablesList(widget.Key);
					if (list != null && list.Count > 0)
					{
						list = list.FindAll((Pickupable match) => match.GetComponent<Assignable>().assignee == null);
						num4 = (float)list.Count;
					}
					if (num4 > 0f)
					{
						widget.Value.GetReference<Image>("Icon").material = GlobalResources.Instance().AnimUIMaterial;
						widget.Value.GetReference<Image>("Icon").color = new Color(1f, 1f, 1f, 1f);
					}
					else
					{
						widget.Value.GetReference<Image>("Icon").material = GlobalResources.Instance().AnimMaterialUIDesaturated;
						widget.Value.GetReference<Image>("Icon").color = new Color(1f, 1f, 1f, 0.5f);
					}
					string text = GameUtil.SafeStringFormat(UI.SKILLS_SCREEN.AVAILABLE_BOOSTERS_LABEL, new object[] { num4.ToString() });
					LocText reference2 = widget.Value.GetReference<LocText>("AvailableLabel");
					reference2.SetText(text);
					reference2.color = ((num4 > 0f) ? new Color(0.53f, 0.83f, 0.53f) : new Color(0.65f, 0.65f, 0.65f));
					string text2 = GameUtil.SafeStringFormat(UI.SKILLS_SCREEN.ASSIGNED_BOOSTERS_LABEL, new object[] { num3 });
					widget.Value.GetReference<LocText>("EquipCountLabel").SetText(text2);
					widget.Value.GetReference<ToolTip>("Tooltip").SetSimpleTooltip(string.Concat(new string[]
					{
						"<b>",
						prefab.GetProperName(),
						"</b>\n\n",
						BionicUpgradeComponentConfig.UpgradesData[widget.Key].stateMachineDescription,
						"\n\n",
						BionicUpgradeComponentConfig.GetColonyBoosterAssignmentString(widget.Key.Name)
					}));
					bool flag2 = instance.AssignedSlotCount < instance.UnlockedSlotCount;
					bool flag3 = num4 > 0f;
					MultiToggle component = widget.Value.gameObject.GetComponent<MultiToggle>();
					component.onClick = null;
					if (flag3 && flag2)
					{
						MultiToggle multiToggle = component;
						multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
						{
							this.IncrementBoosterAssignment(widget.Key);
						}));
					}
				}
				return;
			}
		}
		this.boosterPanel.SetActive(false);
		this.boosterHeader.SetActive(false);
		this.skillsContainer.rectTransform().sizeDelta = new Vector2(0f, 0f);
	}

	// Token: 0x06007494 RID: 29844 RVA: 0x002C77F8 File Offset: 0x002C59F8
	private void RefreshSkillWidgets()
	{
		int num = 1;
		foreach (SkillGroup skillGroup in Db.Get().SkillGroups.resources)
		{
			List<Skill> skillsBySkillGroup = this.GetSkillsBySkillGroup(skillGroup.Id);
			if (skillsBySkillGroup.Count > 0)
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				for (int i = 0; i < skillsBySkillGroup.Count; i++)
				{
					Skill skill = skillsBySkillGroup[i];
					if (!skill.deprecated && Game.IsCorrectDlcActiveForCurrentSave(skill))
					{
						if (!this.skillWidgets.ContainsKey(skill.Id))
						{
							while (skill.tier >= this.skillColumns.Count)
							{
								GameObject gameObject = Util.KInstantiateUI(this.Prefab_skillColumn, this.Prefab_tableLayout, true);
								this.skillColumns.Add(gameObject);
								HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
								if (this.skillColumns.Count % 2 == 0)
								{
									component.GetReference("BG").gameObject.SetActive(false);
								}
							}
							int num2 = 0;
							dictionary.TryGetValue(skill.tier, out num2);
							dictionary[skill.tier] = num2 + 1;
							GameObject gameObject2 = Util.KInstantiateUI(this.Prefab_skillWidget, this.skillColumns[skill.tier], true);
							this.skillWidgets.Add(skill.Id, gameObject2);
						}
						this.skillWidgets[skill.Id].GetComponent<SkillWidget>().Refresh(skill.Id);
					}
				}
				if (!this.skillGroupRow.ContainsKey(skillGroup.Id))
				{
					int num3 = 1;
					foreach (KeyValuePair<int, int> keyValuePair in dictionary)
					{
						num3 = Mathf.Max(num3, keyValuePair.Value);
					}
					this.skillGroupRow.Add(skillGroup.Id, num);
					num += num3;
				}
			}
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.skillWidgets)
		{
			if (Db.Get().Skills.Get(keyValuePair2.Key).requiredDuplicantModel != null)
			{
				keyValuePair2.Value.SetActive(Db.Get().Skills.Get(keyValuePair2.Key).requiredDuplicantModel == this.SelectedMinionModel());
			}
		}
		foreach (SkillMinionWidget skillMinionWidget in this.sortableRows)
		{
			skillMinionWidget.Refresh();
		}
		this.RefreshWidgetPositions();
	}

	// Token: 0x06007495 RID: 29845 RVA: 0x002C7B2C File Offset: 0x002C5D2C
	public void HoverSkill(string skillID)
	{
		this.hoveredSkillID = skillID;
		if (this.delayRefreshRoutine != null)
		{
			base.StopCoroutine(this.delayRefreshRoutine);
			this.delayRefreshRoutine = null;
		}
		if (string.IsNullOrEmpty(this.hoveredSkillID))
		{
			this.delayRefreshRoutine = base.StartCoroutine(this.DelayRefreshProgressBars());
			return;
		}
		this.RefreshProgressBars();
	}

	// Token: 0x06007496 RID: 29846 RVA: 0x002C7B81 File Offset: 0x002C5D81
	private IEnumerator DelayRefreshProgressBars()
	{
		yield return SequenceUtil.WaitForSecondsRealtime(0.1f);
		this.RefreshProgressBars();
		yield break;
	}

	// Token: 0x06007497 RID: 29847 RVA: 0x002C7B90 File Offset: 0x002C5D90
	public void RefreshWidgetPositions()
	{
		float num = 0f;
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.skillWidgets)
		{
			if (!(Db.Get().Skills.Get(keyValuePair.Key).requiredDuplicantModel != this.SelectedMinionModel()))
			{
				float rowPosition = this.GetRowPosition(keyValuePair.Key);
				num = Mathf.Max(rowPosition, num);
				keyValuePair.Value.rectTransform().anchoredPosition = Vector2.down * rowPosition;
			}
		}
		num = Mathf.Max(num, (float)this.layoutRowHeight);
		float num2 = (float)this.layoutRowHeight;
		foreach (GameObject gameObject in this.skillColumns)
		{
			gameObject.GetComponent<LayoutElement>().minHeight = num + num2;
		}
		this.linesPending = true;
	}

	// Token: 0x06007498 RID: 29848 RVA: 0x002C7CAC File Offset: 0x002C5EAC
	public float GetRowPosition(string skillID)
	{
		Skill skill = Db.Get().Skills.Get(skillID);
		int num = this.skillGroupRow[skill.skillGroup];
		int num2 = num;
		foreach (KeyValuePair<string, int> keyValuePair in this.skillGroupRow)
		{
			if (keyValuePair.Value <= num && this.SelectedMinionModel() != this.GetSkillsBySkillGroup(keyValuePair.Key)[0].requiredDuplicantModel)
			{
				num2--;
			}
		}
		num = num2;
		List<Skill> skillsBySkillGroup = this.GetSkillsBySkillGroup(skill.skillGroup);
		int num3 = 0;
		foreach (Skill skill2 in skillsBySkillGroup)
		{
			if (skill2 == skill)
			{
				break;
			}
			if (skill2.tier == skill.tier)
			{
				num3++;
			}
		}
		return (float)(this.layoutRowHeight * (num3 + num - 1));
	}

	// Token: 0x06007499 RID: 29849 RVA: 0x002C7DC8 File Offset: 0x002C5FC8
	private void OnAddMinionIdentity(MinionIdentity add)
	{
		this.BuildMinions();
		this.RefreshAll();
	}

	// Token: 0x0600749A RID: 29850 RVA: 0x002C7DD8 File Offset: 0x002C5FD8
	private void OnRemoveMinionIdentity(MinionIdentity remove)
	{
		if (remove != null)
		{
			if (this.CurrentlySelectedMinion == remove)
			{
				this.CurrentlySelectedMinion = null;
			}
			if (remove.assignableProxy.Get() == this.CurrentlySelectedMinion)
			{
				this.CurrentlySelectedMinion = null;
			}
		}
		this.BuildMinions();
		this.RefreshAll();
	}

	// Token: 0x0600749B RID: 29851 RVA: 0x002C7E28 File Offset: 0x002C6028
	private void BuildMinions()
	{
		for (int i = this.sortableRows.Count - 1; i >= 0; i--)
		{
			this.sortableRows[i].DeleteObject();
		}
		this.sortableRows.Clear();
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			GameObject gameObject = Util.KInstantiateUI(this.Prefab_minion, this.Prefab_minionLayout, true);
			gameObject.GetComponent<SkillMinionWidget>().SetMinon(minionIdentity.assignableProxy.Get());
			this.sortableRows.Add(gameObject.GetComponent<SkillMinionWidget>());
		}
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			foreach (MinionStorage.Info info in minionStorage.GetStoredMinionInfo())
			{
				if (info.serializedMinion != null)
				{
					StoredMinionIdentity storedMinionIdentity = info.serializedMinion.Get<StoredMinionIdentity>();
					GameObject gameObject2 = Util.KInstantiateUI(this.Prefab_minion, this.Prefab_minionLayout, true);
					gameObject2.GetComponent<SkillMinionWidget>().SetMinon(storedMinionIdentity.assignableProxy.Get());
					this.sortableRows.Add(gameObject2.GetComponent<SkillMinionWidget>());
				}
			}
		}
		foreach (int num in ClusterManager.Instance.GetWorldIDsSorted())
		{
			if (ClusterManager.Instance.GetWorld(num).IsDiscovered)
			{
				this.AddWorldDivider(num);
			}
		}
		foreach (KeyValuePair<int, GameObject> keyValuePair in this.worldDividers)
		{
			keyValuePair.Value.SetActive(ClusterManager.Instance.GetWorld(keyValuePair.Key).IsDiscovered && DlcManager.FeatureClusterSpaceEnabled());
			Component reference = keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference("NobodyRow");
			reference.gameObject.SetActive(true);
			using (IEnumerator enumerator6 = Components.MinionAssignablesProxy.GetEnumerator())
			{
				while (enumerator6.MoveNext())
				{
					if (((MinionAssignablesProxy)enumerator6.Current).GetTargetGameObject().GetComponent<KMonoBehaviour>().GetMyWorld()
						.id == keyValuePair.Key)
					{
						reference.gameObject.SetActive(false);
						break;
					}
				}
			}
		}
		if (this.CurrentlySelectedMinion == null && Components.LiveMinionIdentities.Count > 0)
		{
			this.CurrentlySelectedMinion = Components.LiveMinionIdentities.Items[0];
		}
	}

	// Token: 0x0600749C RID: 29852 RVA: 0x002C814C File Offset: 0x002C634C
	protected void AddWorldDivider(int worldId)
	{
		if (!this.worldDividers.ContainsKey(worldId))
		{
			GameObject gameObject = Util.KInstantiateUI(this.Prefab_worldDivider, this.Prefab_minionLayout, true);
			gameObject.GetComponentInChildren<Image>().color = ClusterManager.worldColors[worldId % ClusterManager.worldColors.Length];
			ClusterGridEntity component = ClusterManager.Instance.GetWorld(worldId).GetComponent<ClusterGridEntity>();
			gameObject.GetComponentInChildren<LocText>().SetText(component.Name);
			gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = component.GetUISprite();
			this.worldDividers.Add(worldId, gameObject);
		}
	}

	// Token: 0x0600749D RID: 29853 RVA: 0x002C81E4 File Offset: 0x002C63E4
	private void WorldRemoved(object worldId)
	{
		int num = (int)worldId;
		GameObject gameObject;
		if (this.worldDividers.TryGetValue(num, out gameObject))
		{
			global::UnityEngine.Object.Destroy(gameObject);
			this.worldDividers.Remove(num);
		}
	}

	// Token: 0x0600749E RID: 29854 RVA: 0x002C821B File Offset: 0x002C641B
	public Vector2 GetSkillWidgetLineTargetPosition(string skillID)
	{
		return this.skillWidgets[skillID].GetComponent<SkillWidget>().lines_right.GetPosition();
	}

	// Token: 0x0600749F RID: 29855 RVA: 0x002C823D File Offset: 0x002C643D
	public SkillWidget GetSkillWidget(string skill)
	{
		return this.skillWidgets[skill].GetComponent<SkillWidget>();
	}

	// Token: 0x060074A0 RID: 29856 RVA: 0x002C8250 File Offset: 0x002C6450
	public List<Skill> GetSkillsBySkillGroup(string skillGrp)
	{
		List<Skill> list = new List<Skill>();
		foreach (Skill skill in Db.Get().Skills.resources)
		{
			if (skill.skillGroup == skillGrp && !skill.deprecated)
			{
				list.Add(skill);
			}
		}
		return list;
	}

	// Token: 0x060074A1 RID: 29857 RVA: 0x002C82CC File Offset: 0x002C64CC
	private void SelectSortToggle(MultiToggle toggle)
	{
		this.dupeSortingToggle.ChangeState(0);
		this.experienceSortingToggle.ChangeState(0);
		this.moraleSortingToggle.ChangeState(0);
		if (toggle != null)
		{
			if (this.activeSortToggle == toggle)
			{
				this.sortReversed = !this.sortReversed;
			}
			this.activeSortToggle = toggle;
		}
		this.activeSortToggle.ChangeState(this.sortReversed ? 2 : 1);
	}

	// Token: 0x060074A2 RID: 29858 RVA: 0x002C8344 File Offset: 0x002C6544
	private void SortRows(Comparison<IAssignableIdentity> comparison)
	{
		this.active_sort_method = comparison;
		Dictionary<IAssignableIdentity, SkillMinionWidget> dictionary = new Dictionary<IAssignableIdentity, SkillMinionWidget>();
		foreach (SkillMinionWidget skillMinionWidget in this.sortableRows)
		{
			dictionary.Add(skillMinionWidget.assignableIdentity, skillMinionWidget);
		}
		Dictionary<int, List<IAssignableIdentity>> minionsByWorld = ClusterManager.Instance.MinionsByWorld;
		this.sortableRows.Clear();
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		int num = 0;
		int num2 = 0;
		foreach (KeyValuePair<int, List<IAssignableIdentity>> keyValuePair in minionsByWorld)
		{
			dictionary2.Add(keyValuePair.Key, num);
			num++;
			List<IAssignableIdentity> list = new List<IAssignableIdentity>();
			foreach (IAssignableIdentity assignableIdentity in keyValuePair.Value)
			{
				list.Add(assignableIdentity);
			}
			if (comparison != null)
			{
				list.Sort(comparison);
				if (this.sortReversed)
				{
					list.Reverse();
				}
			}
			num += list.Count;
			num2 += list.Count;
			for (int i = 0; i < list.Count; i++)
			{
				IAssignableIdentity assignableIdentity2 = list[i];
				SkillMinionWidget skillMinionWidget2 = dictionary[assignableIdentity2];
				this.sortableRows.Add(skillMinionWidget2);
			}
		}
		for (int j = 0; j < this.sortableRows.Count; j++)
		{
			this.sortableRows[j].gameObject.transform.SetSiblingIndex(j);
		}
		foreach (KeyValuePair<int, int> keyValuePair2 in dictionary2)
		{
			this.worldDividers[keyValuePair2.Key].transform.SetSiblingIndex(keyValuePair2.Value);
		}
	}

	// Token: 0x04005064 RID: 20580
	[SerializeField]
	private KButton CloseButton;

	// Token: 0x04005065 RID: 20581
	[Header("Prefabs")]
	[SerializeField]
	private GameObject Prefab_skillWidget;

	// Token: 0x04005066 RID: 20582
	[SerializeField]
	private GameObject Prefab_skillColumn;

	// Token: 0x04005067 RID: 20583
	[SerializeField]
	private GameObject Prefab_minion;

	// Token: 0x04005068 RID: 20584
	[SerializeField]
	private GameObject Prefab_minionLayout;

	// Token: 0x04005069 RID: 20585
	[SerializeField]
	private GameObject Prefab_tableLayout;

	// Token: 0x0400506A RID: 20586
	[SerializeField]
	private GameObject Prefab_worldDivider;

	// Token: 0x0400506B RID: 20587
	[Header("Sort Toggles")]
	[SerializeField]
	private MultiToggle dupeSortingToggle;

	// Token: 0x0400506C RID: 20588
	[SerializeField]
	private MultiToggle experienceSortingToggle;

	// Token: 0x0400506D RID: 20589
	[SerializeField]
	private MultiToggle moraleSortingToggle;

	// Token: 0x0400506E RID: 20590
	private MultiToggle activeSortToggle;

	// Token: 0x0400506F RID: 20591
	private bool sortReversed;

	// Token: 0x04005070 RID: 20592
	private Comparison<IAssignableIdentity> active_sort_method;

	// Token: 0x04005071 RID: 20593
	[Header("Duplicant Animation")]
	[SerializeField]
	private FullBodyUIMinionWidget minionAnimWidget;

	// Token: 0x04005072 RID: 20594
	[Header("Progress Bars")]
	[SerializeField]
	private ToolTip expectationsTooltip;

	// Token: 0x04005073 RID: 20595
	[SerializeField]
	private LocText moraleProgressLabel;

	// Token: 0x04005074 RID: 20596
	[SerializeField]
	private GameObject moraleWarning;

	// Token: 0x04005075 RID: 20597
	[SerializeField]
	private GameObject moraleNotch;

	// Token: 0x04005076 RID: 20598
	[SerializeField]
	private Color moraleNotchColor;

	// Token: 0x04005077 RID: 20599
	private List<GameObject> moraleNotches = new List<GameObject>();

	// Token: 0x04005078 RID: 20600
	[SerializeField]
	private LocText expectationsProgressLabel;

	// Token: 0x04005079 RID: 20601
	[SerializeField]
	private GameObject expectationWarning;

	// Token: 0x0400507A RID: 20602
	[SerializeField]
	private GameObject expectationNotch;

	// Token: 0x0400507B RID: 20603
	[SerializeField]
	private Color expectationNotchColor;

	// Token: 0x0400507C RID: 20604
	[SerializeField]
	private Color expectationNotchProspectColor;

	// Token: 0x0400507D RID: 20605
	private List<GameObject> expectationNotches = new List<GameObject>();

	// Token: 0x0400507E RID: 20606
	[SerializeField]
	private ToolTip experienceBarTooltip;

	// Token: 0x0400507F RID: 20607
	[SerializeField]
	private Image experienceProgressFill;

	// Token: 0x04005080 RID: 20608
	[SerializeField]
	private LocText EXPCount;

	// Token: 0x04005081 RID: 20609
	[SerializeField]
	private LocText duplicantLevelIndicator;

	// Token: 0x04005082 RID: 20610
	[SerializeField]
	private KScrollRect scrollRect;

	// Token: 0x04005083 RID: 20611
	[SerializeField]
	private float scrollSpeed = 7f;

	// Token: 0x04005084 RID: 20612
	[SerializeField]
	private DropDown hatDropDown;

	// Token: 0x04005085 RID: 20613
	[SerializeField]
	public Image selectedHat;

	// Token: 0x04005086 RID: 20614
	[SerializeField]
	private GameObject skillsContainer;

	// Token: 0x04005087 RID: 20615
	[SerializeField]
	private GameObject boosterPanel;

	// Token: 0x04005088 RID: 20616
	[SerializeField]
	private GameObject boosterHeader;

	// Token: 0x04005089 RID: 20617
	[SerializeField]
	private GameObject boosterContentGrid;

	// Token: 0x0400508A RID: 20618
	[SerializeField]
	private GameObject boosterPrefab;

	// Token: 0x0400508B RID: 20619
	private Dictionary<Tag, HierarchyReferences> boosterWidgets = new Dictionary<Tag, HierarchyReferences>();

	// Token: 0x0400508C RID: 20620
	[SerializeField]
	private LocText equippedBoostersHeaderLabel;

	// Token: 0x0400508D RID: 20621
	[SerializeField]
	private LocText assignedBoostersCountLabel;

	// Token: 0x0400508E RID: 20622
	[SerializeField]
	private GameObject boosterSlotIconPrefab;

	// Token: 0x0400508F RID: 20623
	private List<GameObject> boosterSlotIcons = new List<GameObject>();

	// Token: 0x04005090 RID: 20624
	private IAssignableIdentity currentlySelectedMinion;

	// Token: 0x04005091 RID: 20625
	private List<GameObject> rows = new List<GameObject>();

	// Token: 0x04005092 RID: 20626
	private List<SkillMinionWidget> sortableRows = new List<SkillMinionWidget>();

	// Token: 0x04005093 RID: 20627
	private Dictionary<int, GameObject> worldDividers = new Dictionary<int, GameObject>();

	// Token: 0x04005094 RID: 20628
	private string hoveredSkillID = "";

	// Token: 0x04005095 RID: 20629
	private Dictionary<string, GameObject> skillWidgets = new Dictionary<string, GameObject>();

	// Token: 0x04005096 RID: 20630
	private Dictionary<string, int> skillGroupRow = new Dictionary<string, int>();

	// Token: 0x04005097 RID: 20631
	private List<GameObject> skillColumns = new List<GameObject>();

	// Token: 0x04005098 RID: 20632
	private bool dirty;

	// Token: 0x04005099 RID: 20633
	private bool linesPending;

	// Token: 0x0400509A RID: 20634
	private int layoutRowHeight = 80;

	// Token: 0x0400509B RID: 20635
	private Coroutine delayRefreshRoutine;

	// Token: 0x0400509C RID: 20636
	protected Comparison<IAssignableIdentity> compareByExperience = delegate(IAssignableIdentity a, IAssignableIdentity b)
	{
		GameObject targetGameObject = ((MinionAssignablesProxy)a).GetTargetGameObject();
		GameObject targetGameObject2 = ((MinionAssignablesProxy)b).GetTargetGameObject();
		if (targetGameObject == null && targetGameObject2 == null)
		{
			return 0;
		}
		if (targetGameObject == null)
		{
			return -1;
		}
		if (targetGameObject2 == null)
		{
			return 1;
		}
		MinionResume component = targetGameObject.GetComponent<MinionResume>();
		MinionResume component2 = targetGameObject2.GetComponent<MinionResume>();
		if (component == null && component2 == null)
		{
			return 0;
		}
		if (component == null)
		{
			return -1;
		}
		if (component2 == null)
		{
			return 1;
		}
		float num = (float)component.AvailableSkillpoints;
		float num2 = (float)component2.AvailableSkillpoints;
		return num.CompareTo(num2);
	};

	// Token: 0x0400509D RID: 20637
	protected Comparison<IAssignableIdentity> compareByMinion = (IAssignableIdentity a, IAssignableIdentity b) => a.GetProperName().CompareTo(b.GetProperName());

	// Token: 0x0400509E RID: 20638
	protected Comparison<IAssignableIdentity> compareByMorale = delegate(IAssignableIdentity a, IAssignableIdentity b)
	{
		GameObject targetGameObject3 = ((MinionAssignablesProxy)a).GetTargetGameObject();
		GameObject targetGameObject4 = ((MinionAssignablesProxy)b).GetTargetGameObject();
		if (targetGameObject3 == null && targetGameObject4 == null)
		{
			return 0;
		}
		if (targetGameObject3 == null)
		{
			return -1;
		}
		if (targetGameObject4 == null)
		{
			return 1;
		}
		MinionResume component3 = targetGameObject3.GetComponent<MinionResume>();
		MinionResume component4 = targetGameObject4.GetComponent<MinionResume>();
		if (component3 == null && component4 == null)
		{
			return 0;
		}
		if (component3 == null)
		{
			return -1;
		}
		if (component4 == null)
		{
			return 1;
		}
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLife.Lookup(component3);
		Db.Get().Attributes.QualityOfLifeExpectation.Lookup(component3);
		AttributeInstance attributeInstance2 = Db.Get().Attributes.QualityOfLife.Lookup(component4);
		Db.Get().Attributes.QualityOfLifeExpectation.Lookup(component4);
		float totalValue = attributeInstance.GetTotalValue();
		float totalValue2 = attributeInstance2.GetTotalValue();
		return totalValue.CompareTo(totalValue2);
	};
}
