using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CBC RID: 3260
public class DetailTabHeader : KMonoBehaviour
{
	// Token: 0x17000751 RID: 1873
	// (get) Token: 0x06006455 RID: 25685 RVA: 0x0025B17E File Offset: 0x0025937E
	public TargetPanel ActivePanel
	{
		get
		{
			if (this.tabPanels.ContainsKey(this.selectedTabID))
			{
				return this.tabPanels[this.selectedTabID];
			}
			return null;
		}
	}

	// Token: 0x06006456 RID: 25686 RVA: 0x0025B1A8 File Offset: 0x002593A8
	public void Init()
	{
		this.detailsScreen = DetailsScreen.Instance;
		this.MakeTab("SIMPLEINFO", UI.DETAILTABS.SIMPLEINFO.NAME, Assets.GetSprite("icon_display_screen_status"), UI.DETAILTABS.SIMPLEINFO.TOOLTIP, this.simpleInfoScreen);
		this.MakeTab("PERSONALITY", UI.DETAILTABS.PERSONALITY.NAME, Assets.GetSprite("icon_display_screen_bio"), UI.DETAILTABS.PERSONALITY.TOOLTIP, this.minionPersonalityPanel);
		this.MakeTab("BUILDINGCHORES", UI.DETAILTABS.BUILDING_CHORES.NAME, Assets.GetSprite("icon_display_screen_errands"), UI.DETAILTABS.BUILDING_CHORES.TOOLTIP, this.buildingInfoPanel);
		this.MakeTab("DETAILS", UI.DETAILTABS.DETAILS.NAME, Assets.GetSprite("icon_display_screen_properties"), UI.DETAILTABS.DETAILS.TOOLTIP, this.additionalDetailsPanel);
		this.ChangeToDefaultTab();
	}

	// Token: 0x06006457 RID: 25687 RVA: 0x0025B296 File Offset: 0x00259496
	private void MakeTabContents(GameObject panelToActivate)
	{
	}

	// Token: 0x06006458 RID: 25688 RVA: 0x0025B298 File Offset: 0x00259498
	private void MakeTab(string id, string label, Sprite sprite, string tooltip, GameObject panelToActivate)
	{
		GameObject gameObject = Util.KInstantiateUI(this.tabPrefab, this.tabContainer, true);
		gameObject.name = "tab: " + id;
		gameObject.GetComponent<ToolTip>().SetSimpleTooltip(tooltip);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("icon").sprite = sprite;
		component.GetReference<LocText>("label").text = label;
		MultiToggle component2 = gameObject.GetComponent<MultiToggle>();
		GameObject gameObject2 = Util.KInstantiateUI(panelToActivate, this.panelContainer.gameObject, true);
		TargetPanel component3 = gameObject2.GetComponent<TargetPanel>();
		component3.SetTarget(this.detailsScreen.target);
		this.tabPanels.Add(id, component3);
		string targetTab = id;
		MultiToggle multiToggle = component2;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			this.ChangeTab(targetTab);
		}));
		this.tabs.Add(id, component2);
		gameObject2.SetActive(false);
	}

	// Token: 0x06006459 RID: 25689 RVA: 0x0025B384 File Offset: 0x00259584
	private void ChangeTab(string id)
	{
		this.selectedTabID = id;
		foreach (KeyValuePair<string, MultiToggle> keyValuePair in this.tabs)
		{
			keyValuePair.Value.ChangeState((keyValuePair.Key == this.selectedTabID) ? 1 : 0);
		}
		foreach (KeyValuePair<string, TargetPanel> keyValuePair2 in this.tabPanels)
		{
			if (keyValuePair2.Key == id)
			{
				keyValuePair2.Value.gameObject.SetActive(true);
				keyValuePair2.Value.SetTarget(this.detailsScreen.target);
			}
			else
			{
				keyValuePair2.Value.SetTarget(null);
				keyValuePair2.Value.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x0600645A RID: 25690 RVA: 0x0025B490 File Offset: 0x00259690
	private void ChangeToDefaultTab()
	{
		this.ChangeTab("SIMPLEINFO");
	}

	// Token: 0x0600645B RID: 25691 RVA: 0x0025B4A0 File Offset: 0x002596A0
	public void RefreshTabDisplayForTarget(GameObject target)
	{
		foreach (KeyValuePair<string, TargetPanel> keyValuePair in this.tabPanels)
		{
			this.tabs[keyValuePair.Key].gameObject.SetActive(keyValuePair.Value.IsValidForTarget(target));
		}
		if (this.tabPanels[this.selectedTabID].IsValidForTarget(target))
		{
			this.ChangeTab(this.selectedTabID);
			return;
		}
		this.ChangeToDefaultTab();
	}

	// Token: 0x0400446D RID: 17517
	private Dictionary<string, MultiToggle> tabs = new Dictionary<string, MultiToggle>();

	// Token: 0x0400446E RID: 17518
	private string selectedTabID;

	// Token: 0x0400446F RID: 17519
	[SerializeField]
	private GameObject tabPrefab;

	// Token: 0x04004470 RID: 17520
	[SerializeField]
	private GameObject tabContainer;

	// Token: 0x04004471 RID: 17521
	[SerializeField]
	private GameObject panelContainer;

	// Token: 0x04004472 RID: 17522
	[Header("Screen Prefabs")]
	[SerializeField]
	private GameObject simpleInfoScreen;

	// Token: 0x04004473 RID: 17523
	[SerializeField]
	private GameObject minionPersonalityPanel;

	// Token: 0x04004474 RID: 17524
	[SerializeField]
	private GameObject buildingInfoPanel;

	// Token: 0x04004475 RID: 17525
	[SerializeField]
	private GameObject additionalDetailsPanel;

	// Token: 0x04004476 RID: 17526
	[SerializeField]
	private GameObject cosmeticsPanel;

	// Token: 0x04004477 RID: 17527
	[SerializeField]
	private GameObject materialPanel;

	// Token: 0x04004478 RID: 17528
	private DetailsScreen detailsScreen;

	// Token: 0x04004479 RID: 17529
	private Dictionary<string, TargetPanel> tabPanels = new Dictionary<string, TargetPanel>();
}
