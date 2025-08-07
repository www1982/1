using System;
using System.Collections.Generic;
using Database;
using STRINGS;
using UnityEngine;

// Token: 0x0200093A RID: 2362
public class GeothermalPlantComponent : KMonoBehaviour, ICheckboxListGroupControl, IRelatedEntities
{
	// Token: 0x170004C9 RID: 1225
	// (get) Token: 0x06004332 RID: 17202 RVA: 0x00181B78 File Offset: 0x0017FD78
	string ICheckboxListGroupControl.Title
	{
		get
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.SIDESCREENS.BRING_ONLINE_TITLE;
		}
	}

	// Token: 0x170004CA RID: 1226
	// (get) Token: 0x06004333 RID: 17203 RVA: 0x00181B84 File Offset: 0x0017FD84
	string ICheckboxListGroupControl.Description
	{
		get
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.SIDESCREENS.BRING_ONLINE_DESC;
		}
	}

	// Token: 0x06004334 RID: 17204 RVA: 0x00181B90 File Offset: 0x0017FD90
	public ICheckboxListGroupControl.ListGroup[] GetData()
	{
		ColonyAchievement activateGeothermalPlant = Db.Get().ColonyAchievements.ActivateGeothermalPlant;
		ICheckboxListGroupControl.CheckboxItem[] array = new ICheckboxListGroupControl.CheckboxItem[activateGeothermalPlant.requirementChecklist.Count];
		for (int i = 0; i < array.Length; i++)
		{
			ICheckboxListGroupControl.CheckboxItem checkboxItem = default(ICheckboxListGroupControl.CheckboxItem);
			bool flag = activateGeothermalPlant.requirementChecklist[i].Success();
			checkboxItem.isOn = flag;
			checkboxItem.text = (activateGeothermalPlant.requirementChecklist[i] as VictoryColonyAchievementRequirement).Name();
			checkboxItem.tooltip = activateGeothermalPlant.requirementChecklist[i].GetProgress(flag);
			array[i] = checkboxItem;
		}
		return new ICheckboxListGroupControl.ListGroup[]
		{
			new ICheckboxListGroupControl.ListGroup(activateGeothermalPlant.Name, array, null, null)
		};
	}

	// Token: 0x06004335 RID: 17205 RVA: 0x00181C4A File Offset: 0x0017FE4A
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x06004336 RID: 17206 RVA: 0x00181C4D File Offset: 0x0017FE4D
	public int CheckboxSideScreenSortOrder()
	{
		return 100;
	}

	// Token: 0x06004337 RID: 17207 RVA: 0x00181C51 File Offset: 0x0017FE51
	public static bool GeothermalControllerRepaired()
	{
		return SaveGame.Instance.ColonyAchievementTracker.GeothermalControllerRepaired;
	}

	// Token: 0x06004338 RID: 17208 RVA: 0x00181C62 File Offset: 0x0017FE62
	public static bool GeothermalFacilityDiscovered()
	{
		return SaveGame.Instance.ColonyAchievementTracker.GeothermalFacilityDiscovered;
	}

	// Token: 0x06004339 RID: 17209 RVA: 0x00181C73 File Offset: 0x0017FE73
	protected override void OnSpawn()
	{
		base.Subscribe(-1503271301, new Action<object>(this.OnObjectSelect));
	}

	// Token: 0x0600433A RID: 17210 RVA: 0x00181C8D File Offset: 0x0017FE8D
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0600433B RID: 17211 RVA: 0x00181C98 File Offset: 0x0017FE98
	public static void DisplayPopup(string title, string desc, HashedString anim, global::System.Action onDismissCallback, Transform clickFocus = null)
	{
		EventInfoData eventInfoData = new EventInfoData(title, desc, anim);
		if (Components.LiveMinionIdentities.Count >= 2)
		{
			int num = global::UnityEngine.Random.Range(0, Components.LiveMinionIdentities.Count);
			int num2 = global::UnityEngine.Random.Range(1, Components.LiveMinionIdentities.Count);
			eventInfoData.minions = new GameObject[]
			{
				Components.LiveMinionIdentities[num].gameObject,
				Components.LiveMinionIdentities[(num + num2) % Components.LiveMinionIdentities.Count].gameObject
			};
		}
		else if (Components.LiveMinionIdentities.Count == 1)
		{
			eventInfoData.minions = new GameObject[] { Components.LiveMinionIdentities[0].gameObject };
		}
		eventInfoData.AddDefaultOption(onDismissCallback);
		eventInfoData.clickFocus = clickFocus;
		EventInfoScreen.ShowPopup(eventInfoData);
	}

	// Token: 0x0600433C RID: 17212 RVA: 0x00181D64 File Offset: 0x0017FF64
	protected void RevealAllVentsAndController()
	{
		foreach (WorldGenSpawner.Spawnable spawnable in SaveGame.Instance.worldGenSpawner.GetSpawnablesWithTag(true, new Tag[] { "GeothermalVentEntity" }))
		{
			int num;
			int num2;
			Grid.CellToXY(spawnable.cell, out num, out num2);
			GridVisibility.Reveal(num, num2 + 2, 5, 5f);
		}
		foreach (WorldGenSpawner.Spawnable spawnable2 in SaveGame.Instance.worldGenSpawner.GetSpawnablesWithTag(true, new Tag[] { "GeothermalControllerEntity" }))
		{
			int num3;
			int num4;
			Grid.CellToXY(spawnable2.cell, out num3, out num4);
			GridVisibility.Reveal(num3, num4 + 3, 7, 7f);
		}
		SelectTool.Instance.Select(null, true);
	}

	// Token: 0x0600433D RID: 17213 RVA: 0x00181E74 File Offset: 0x00180074
	protected void OnObjectSelect(object clicked)
	{
		base.Unsubscribe(-1503271301, new Action<object>(this.OnObjectSelect));
		if (SaveGame.Instance.ColonyAchievementTracker.GeothermalFacilityDiscovered)
		{
			return;
		}
		SaveGame.Instance.ColonyAchievementTracker.GeothermalFacilityDiscovered = true;
		GeothermalPlantComponent.DisplayPopup(COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.POPUPS.GEOTHERMAL_DISCOVERED_TITLE, COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.POPUPS.GEOTHERMAL_DISOCVERED_DESC, "geothermalplantintro_kanim", new global::System.Action(this.RevealAllVentsAndController), null);
	}

	// Token: 0x0600433E RID: 17214 RVA: 0x00181EEC File Offset: 0x001800EC
	public static void OnVentingHotMaterial(int worldid)
	{
		foreach (GeothermalVent geothermalVent in Components.GeothermalVents.GetItems(worldid))
		{
			if (geothermalVent.IsQuestEntombed())
			{
				geothermalVent.SetQuestComplete();
				if (!SaveGame.Instance.ColonyAchievementTracker.GeothermalClearedEntombedVent)
				{
					GeothermalVictorySequence.VictoryVent = geothermalVent;
					GeothermalPlantComponent.DisplayPopup(COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.POPUPS.GEOPLANT_ERRUPTED_TITLE, COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.POPUPS.GEOPLANT_ERRUPTED_DESC, "geothermalplantachievement_kanim", delegate
					{
						SaveGame.Instance.ColonyAchievementTracker.GeothermalClearedEntombedVent = true;
					}, null);
					break;
				}
			}
		}
	}

	// Token: 0x0600433F RID: 17215 RVA: 0x00181FA8 File Offset: 0x001801A8
	public List<KSelectable> GetRelatedEntities()
	{
		List<KSelectable> list = new List<KSelectable>();
		int myWorldId = this.GetMyWorldId();
		foreach (GeothermalController geothermalController in Components.GeothermalControllers.GetItems(myWorldId))
		{
			list.Add(geothermalController.GetComponent<KSelectable>());
		}
		foreach (GeothermalVent geothermalVent in Components.GeothermalVents.GetItems(myWorldId))
		{
			list.Add(geothermalVent.GetComponent<KSelectable>());
		}
		return list;
	}

	// Token: 0x04002CD3 RID: 11475
	public const string POPUP_DISCOVERED_KANIM = "geothermalplantintro_kanim";

	// Token: 0x04002CD4 RID: 11476
	public const string POPUP_PROGRESS_KANIM = "geothermalplantonline_kanim";

	// Token: 0x04002CD5 RID: 11477
	public const string POPUP_COMPLETE_KANIM = "geothermalplantachievement_kanim";
}
