using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C4C RID: 3148
public class AllDiagnosticsScreen : ShowOptimizedKScreen, ISim4000ms, ISim1000ms
{
	// Token: 0x06006031 RID: 24625 RVA: 0x00238615 File Offset: 0x00236815
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		AllDiagnosticsScreen.Instance = this;
		this.ConfigureDebugToggle();
	}

	// Token: 0x06006032 RID: 24626 RVA: 0x00238629 File Offset: 0x00236829
	protected override void OnForcedCleanUp()
	{
		AllDiagnosticsScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06006033 RID: 24627 RVA: 0x00238638 File Offset: 0x00236838
	private void ConfigureDebugToggle()
	{
		Game.Instance.Subscribe(1557339983, new Action<object>(this.DebugToggleRefresh));
		MultiToggle toggle = this.debugNotificationToggleCotainer.GetComponentInChildren<MultiToggle>();
		MultiToggle toggle2 = toggle;
		toggle2.onClick = (global::System.Action)Delegate.Combine(toggle2.onClick, new global::System.Action(delegate
		{
			DebugHandler.ToggleDisableNotifications();
			toggle.ChangeState(DebugHandler.NotificationsDisabled ? 1 : 0);
		}));
		this.DebugToggleRefresh(null);
		toggle.ChangeState(DebugHandler.NotificationsDisabled ? 1 : 0);
	}

	// Token: 0x06006034 RID: 24628 RVA: 0x002386BC File Offset: 0x002368BC
	private void DebugToggleRefresh(object data = null)
	{
		this.debugNotificationToggleCotainer.gameObject.SetActive(DebugHandler.InstantBuildMode);
	}

	// Token: 0x06006035 RID: 24629 RVA: 0x002386D4 File Offset: 0x002368D4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.ConsumeMouseScroll = true;
		this.Populate(null);
		Game.Instance.Subscribe(1983128072, new Action<object>(this.Populate));
		Game.Instance.Subscribe(-1280433810, new Action<object>(this.Populate));
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		this.clearSearchButton.onClick += delegate
		{
			this.searchInputField.text = "";
		};
		this.searchInputField.onValueChanged.AddListener(delegate(string value)
		{
			this.SearchFilter(value);
		});
		KInputTextField kinputTextField = this.searchInputField;
		kinputTextField.onFocus = (global::System.Action)Delegate.Combine(kinputTextField.onFocus, new global::System.Action(delegate
		{
			base.isEditing = true;
		}));
		this.searchInputField.onEndEdit.AddListener(delegate(string value)
		{
			base.isEditing = false;
		});
		this.Show(false);
	}

	// Token: 0x06006036 RID: 24630 RVA: 0x002387C1 File Offset: 0x002369C1
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			ManagementMenu.Instance.CloseAll();
			AllResourcesScreen.Instance.Show(false);
			this.RefreshSubrows();
		}
	}

	// Token: 0x06006037 RID: 24631 RVA: 0x002387E8 File Offset: 0x002369E8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.isHiddenButActive)
		{
			return;
		}
		if (e.TryConsume(global::Action.Escape))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
			this.Show(false);
			e.Consumed = true;
		}
		if (base.isEditing)
		{
			e.Consumed = true;
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06006038 RID: 24632 RVA: 0x0023883C File Offset: 0x00236A3C
	public int GetRowCount()
	{
		return this.diagnosticRows.Count;
	}

	// Token: 0x06006039 RID: 24633 RVA: 0x0023884C File Offset: 0x00236A4C
	public override void OnKeyUp(KButtonEvent e)
	{
		if (this.isHiddenButActive)
		{
			return;
		}
		if (PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
			this.Show(false);
			e.Consumed = true;
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x0600603A RID: 24634 RVA: 0x0023889D File Offset: 0x00236A9D
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x0600603B RID: 24635 RVA: 0x002388A4 File Offset: 0x00236AA4
	public void Populate(object data = null)
	{
		this.SpawnRows();
		foreach (string text in this.diagnosticRows.Keys)
		{
			Tag tag = text;
			this.currentlyDisplayedRows[tag] = true;
		}
		this.SearchFilter(this.searchInputField.text);
		this.RefreshRows();
	}

	// Token: 0x0600603C RID: 24636 RVA: 0x00238924 File Offset: 0x00236B24
	private void SpawnRows()
	{
		foreach (KeyValuePair<int, Dictionary<string, ColonyDiagnosticUtility.DisplaySetting>> keyValuePair in ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings)
		{
			foreach (KeyValuePair<string, ColonyDiagnosticUtility.DisplaySetting> keyValuePair2 in ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[keyValuePair.Key])
			{
				if (!this.diagnosticRows.ContainsKey(keyValuePair2.Key))
				{
					ColonyDiagnostic diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic(keyValuePair2.Key, keyValuePair.Key);
					if (!(diagnostic is WorkTimeDiagnostic) && !(diagnostic is ChoreGroupDiagnostic))
					{
						this.SpawnRow(diagnostic, this.rootListContainer);
					}
				}
			}
		}
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, GameObject> keyValuePair3 in this.diagnosticRows)
		{
			list.Add(keyValuePair3.Key);
		}
		list.Sort((string a, string b) => UI.StripLinkFormatting(ColonyDiagnosticUtility.Instance.GetDiagnosticName(a)).CompareTo(UI.StripLinkFormatting(ColonyDiagnosticUtility.Instance.GetDiagnosticName(b))));
		foreach (string text in list)
		{
			this.diagnosticRows[text].transform.SetAsLastSibling();
		}
	}

	// Token: 0x0600603D RID: 24637 RVA: 0x00238AD8 File Offset: 0x00236CD8
	private void SpawnRow(ColonyDiagnostic diagnostic, GameObject container)
	{
		if (diagnostic == null)
		{
			return;
		}
		if (!this.diagnosticRows.ContainsKey(diagnostic.id))
		{
			GameObject gameObject = Util.KInstantiateUI(this.diagnosticLinePrefab, container, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("NameLabel").SetText(diagnostic.name);
			string id2 = diagnostic.id;
			MultiToggle reference = component.GetReference<MultiToggle>("PinToggle");
			string id = diagnostic.id;
			reference.onClick = (global::System.Action)Delegate.Combine(reference.onClick, new global::System.Action(delegate
			{
				if (ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(diagnostic.id))
				{
					ColonyDiagnosticUtility.Instance.ClearDiagnosticTutorialSetting(diagnostic.id);
				}
				else
				{
					int activeWorldId = ClusterManager.Instance.activeWorldId;
					int num = ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[activeWorldId][id] - ColonyDiagnosticUtility.DisplaySetting.AlertOnly;
					if (num < 0)
					{
						num = 2;
					}
					ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[activeWorldId][id] = (ColonyDiagnosticUtility.DisplaySetting)num;
				}
				this.RefreshRows();
				ColonyDiagnosticScreen.Instance.RefreshAll();
			}));
			GraphBase component2 = component.GetReference<SparkLayer>("Chart").GetComponent<GraphBase>();
			component2.axis_x.min_value = 0f;
			component2.axis_x.max_value = 600f;
			component2.axis_x.guide_frequency = 120f;
			component2.RefreshGuides();
			this.diagnosticRows.Add(id2, gameObject);
			this.criteriaRows.Add(id2, new Dictionary<string, GameObject>());
			this.currentlyDisplayedRows.Add(id2, true);
			component.GetReference<Image>("Icon").sprite = Assets.GetSprite(diagnostic.icon);
			this.RefreshPinnedState(id2);
			RectTransform reference2 = component.GetReference<RectTransform>("SubRows");
			DiagnosticCriterion[] criteria = diagnostic.GetCriteria();
			for (int i = 0; i < criteria.Length; i++)
			{
				DiagnosticCriterion sub = criteria[i];
				GameObject gameObject2 = Util.KInstantiateUI(this.subDiagnosticLinePrefab, reference2.gameObject, true);
				gameObject2.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.DIAGNOSTICS_SCREEN.CRITERIA_TOOLTIP, diagnostic.name, sub.name));
				HierarchyReferences component3 = gameObject2.GetComponent<HierarchyReferences>();
				component3.GetReference<LocText>("Label").SetText(sub.name);
				this.criteriaRows[diagnostic.id].Add(sub.id, gameObject2);
				MultiToggle reference3 = component3.GetReference<MultiToggle>("PinToggle");
				reference3.onClick = (global::System.Action)Delegate.Combine(reference3.onClick, new global::System.Action(delegate
				{
					int activeWorldId2 = ClusterManager.Instance.activeWorldId;
					bool flag = ColonyDiagnosticUtility.Instance.IsCriteriaEnabled(activeWorldId2, diagnostic.id, sub.id);
					ColonyDiagnosticUtility.Instance.SetCriteriaEnabled(activeWorldId2, diagnostic.id, sub.id, !flag);
					this.RefreshSubrows();
				}));
			}
			this.subrowContainerOpen.Add(diagnostic.id, false);
			MultiToggle reference4 = component.GetReference<MultiToggle>("SubrowToggle");
			MultiToggle multiToggle = reference4;
			multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
			{
				this.subrowContainerOpen[diagnostic.id] = !this.subrowContainerOpen[diagnostic.id];
				this.RefreshSubrows();
			}));
			component.GetReference<MultiToggle>("MainToggle").onClick = reference4.onClick;
		}
	}

	// Token: 0x0600603E RID: 24638 RVA: 0x00238DF2 File Offset: 0x00236FF2
	private void FilterRowBySearch(Tag tag, string filter)
	{
		this.currentlyDisplayedRows[tag] = this.PassesSearchFilter(tag, filter);
	}

	// Token: 0x0600603F RID: 24639 RVA: 0x00238E08 File Offset: 0x00237008
	private void SearchFilter(string search)
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
		{
			this.FilterRowBySearch(keyValuePair.Key, search);
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.diagnosticRows)
		{
			this.currentlyDisplayedRows[keyValuePair2.Key] = this.PassesSearchFilter(keyValuePair2.Key, search);
		}
		this.SetRowsActive();
	}

	// Token: 0x06006040 RID: 24640 RVA: 0x00238ED4 File Offset: 0x002370D4
	private bool PassesSearchFilter(Tag tag, string filter)
	{
		if (string.IsNullOrEmpty(filter))
		{
			return true;
		}
		filter = filter.ToUpper();
		string text = tag.ToString();
		if (ColonyDiagnosticUtility.Instance.GetDiagnosticName(text).ToUpper().Contains(filter) || tag.Name.ToUpper().Contains(filter))
		{
			return true;
		}
		ColonyDiagnostic diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic(text, ClusterManager.Instance.activeWorldId);
		if (diagnostic == null)
		{
			return false;
		}
		DiagnosticCriterion[] criteria = diagnostic.GetCriteria();
		if (criteria == null)
		{
			return false;
		}
		DiagnosticCriterion[] array = criteria;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].name.ToUpper().Contains(filter))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006041 RID: 24641 RVA: 0x00238F84 File Offset: 0x00237184
	private void RefreshPinnedState(string diagnosticID)
	{
		if (!ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[ClusterManager.Instance.activeWorldId].ContainsKey(diagnosticID))
		{
			return;
		}
		MultiToggle reference = this.diagnosticRows[diagnosticID].GetComponent<HierarchyReferences>().GetReference<MultiToggle>("PinToggle");
		if (ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(diagnosticID))
		{
			reference.ChangeState(3);
		}
		else
		{
			switch (ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[ClusterManager.Instance.activeWorldId][diagnosticID])
			{
			case ColonyDiagnosticUtility.DisplaySetting.Always:
				reference.ChangeState(2);
				break;
			case ColonyDiagnosticUtility.DisplaySetting.AlertOnly:
				reference.ChangeState(1);
				break;
			case ColonyDiagnosticUtility.DisplaySetting.Never:
				reference.ChangeState(0);
				break;
			}
		}
		string text = "";
		if (ColonyDiagnosticUtility.Instance.IsDiagnosticTutorialDisabled(diagnosticID))
		{
			text = UI.DIAGNOSTICS_SCREEN.CLICK_TOGGLE_MESSAGE.TUTORIAL_DISABLED;
		}
		else
		{
			switch (ColonyDiagnosticUtility.Instance.diagnosticDisplaySettings[ClusterManager.Instance.activeWorldId][diagnosticID])
			{
			case ColonyDiagnosticUtility.DisplaySetting.Always:
				text = UI.DIAGNOSTICS_SCREEN.CLICK_TOGGLE_MESSAGE.NEVER;
				break;
			case ColonyDiagnosticUtility.DisplaySetting.AlertOnly:
				text = UI.DIAGNOSTICS_SCREEN.CLICK_TOGGLE_MESSAGE.ALWAYS;
				break;
			case ColonyDiagnosticUtility.DisplaySetting.Never:
				text = UI.DIAGNOSTICS_SCREEN.CLICK_TOGGLE_MESSAGE.ALERT_ONLY;
				break;
			}
		}
		reference.GetComponent<ToolTip>().SetSimpleTooltip(text);
	}

	// Token: 0x06006042 RID: 24642 RVA: 0x002390B8 File Offset: 0x002372B8
	public void RefreshRows()
	{
		WorldInventory worldInventory = ClusterManager.Instance.GetWorld(ClusterManager.Instance.activeWorldId).worldInventory;
		if (this.allowRefresh)
		{
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
			{
				HierarchyReferences component = keyValuePair.Value.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("AvailableLabel").SetText(keyValuePair.Key);
				component.GetReference<RectTransform>("SubRows").gameObject.SetActive(false);
				ColonyDiagnostic diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic(keyValuePair.Key, ClusterManager.Instance.activeWorldId);
				if (diagnostic != null)
				{
					component.GetReference<LocText>("AvailableLabel").SetText(diagnostic.GetAverageValueString());
					component.GetReference<Image>("Indicator").color = diagnostic.colors[diagnostic.LatestResult.opinion];
					ToolTip reference = component.GetReference<ToolTip>("Tooltip");
					reference.refreshWhileHovering = true;
					reference.SetSimpleTooltip(Strings.Get(new StringKey("STRINGS.UI.COLONY_DIAGNOSTICS." + diagnostic.id.ToUpper() + ".TOOLTIP_NAME")) + "\n" + diagnostic.LatestResult.GetFormattedMessage());
				}
				this.RefreshPinnedState(keyValuePair.Key);
			}
		}
		this.SetRowsActive();
		this.RefreshSubrows();
	}

	// Token: 0x06006043 RID: 24643 RVA: 0x00239248 File Offset: 0x00237448
	private void RefreshSubrows()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
		{
			HierarchyReferences component = keyValuePair.Value.GetComponent<HierarchyReferences>();
			component.GetReference<MultiToggle>("SubrowToggle").ChangeState((!this.subrowContainerOpen[keyValuePair.Key]) ? 0 : 1);
			component.GetReference<RectTransform>("SubRows").gameObject.SetActive(this.subrowContainerOpen[keyValuePair.Key]);
			int num = 0;
			foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.criteriaRows[keyValuePair.Key])
			{
				MultiToggle reference = keyValuePair2.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("PinToggle");
				int activeWorldId = ClusterManager.Instance.activeWorldId;
				string key = keyValuePair.Key;
				string key2 = keyValuePair2.Key;
				bool flag = ColonyDiagnosticUtility.Instance.IsCriteriaEnabled(activeWorldId, key, key2);
				reference.ChangeState(flag ? 1 : 0);
				if (flag)
				{
					num++;
				}
			}
			component.GetReference<LocText>("SubrowHeaderLabel").SetText(string.Format(UI.DIAGNOSTICS_SCREEN.CRITERIA_ENABLED_COUNT, num, this.criteriaRows[keyValuePair.Key].Count));
		}
	}

	// Token: 0x06006044 RID: 24644 RVA: 0x00239400 File Offset: 0x00237600
	private void RefreshCharts()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
		{
			HierarchyReferences component = keyValuePair.Value.GetComponent<HierarchyReferences>();
			ColonyDiagnostic diagnostic = ColonyDiagnosticUtility.Instance.GetDiagnostic(keyValuePair.Key, ClusterManager.Instance.activeWorldId);
			if (diagnostic != null)
			{
				SparkLayer reference = component.GetReference<SparkLayer>("Chart");
				Tracker tracker = diagnostic.tracker;
				if (tracker != null)
				{
					float num = 3000f;
					global::Tuple<float, float>[] array = tracker.ChartableData(num);
					reference.graph.axis_x.max_value = array[array.Length - 1].first;
					reference.graph.axis_x.min_value = reference.graph.axis_x.max_value - num;
					reference.RefreshLine(array, "resourceAmount");
				}
			}
		}
	}

	// Token: 0x06006045 RID: 24645 RVA: 0x002394FC File Offset: 0x002376FC
	private void SetRowsActive()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.diagnosticRows)
		{
			if (ColonyDiagnosticUtility.Instance.GetDiagnostic(keyValuePair.Key, ClusterManager.Instance.activeWorldId) == null)
			{
				this.currentlyDisplayedRows[keyValuePair.Key] = false;
			}
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.diagnosticRows)
		{
			if (keyValuePair2.Value.activeSelf != this.currentlyDisplayedRows[keyValuePair2.Key])
			{
				keyValuePair2.Value.SetActive(this.currentlyDisplayedRows[keyValuePair2.Key]);
			}
		}
	}

	// Token: 0x06006046 RID: 24646 RVA: 0x00239600 File Offset: 0x00237800
	public void Sim4000ms(float dt)
	{
		if (this.isHiddenButActive)
		{
			return;
		}
		this.RefreshCharts();
	}

	// Token: 0x06006047 RID: 24647 RVA: 0x00239611 File Offset: 0x00237811
	public void Sim1000ms(float dt)
	{
		if (this.isHiddenButActive)
		{
			return;
		}
		this.RefreshRows();
	}

	// Token: 0x04004130 RID: 16688
	private Dictionary<string, GameObject> diagnosticRows = new Dictionary<string, GameObject>();

	// Token: 0x04004131 RID: 16689
	private Dictionary<string, Dictionary<string, GameObject>> criteriaRows = new Dictionary<string, Dictionary<string, GameObject>>();

	// Token: 0x04004132 RID: 16690
	public GameObject rootListContainer;

	// Token: 0x04004133 RID: 16691
	public GameObject diagnosticLinePrefab;

	// Token: 0x04004134 RID: 16692
	public GameObject subDiagnosticLinePrefab;

	// Token: 0x04004135 RID: 16693
	public KButton closeButton;

	// Token: 0x04004136 RID: 16694
	public bool allowRefresh = true;

	// Token: 0x04004137 RID: 16695
	[SerializeField]
	private KInputTextField searchInputField;

	// Token: 0x04004138 RID: 16696
	[SerializeField]
	private KButton clearSearchButton;

	// Token: 0x04004139 RID: 16697
	public static AllDiagnosticsScreen Instance;

	// Token: 0x0400413A RID: 16698
	public Dictionary<Tag, bool> currentlyDisplayedRows = new Dictionary<Tag, bool>();

	// Token: 0x0400413B RID: 16699
	public Dictionary<Tag, bool> subrowContainerOpen = new Dictionary<Tag, bool>();

	// Token: 0x0400413C RID: 16700
	[SerializeField]
	private RectTransform debugNotificationToggleCotainer;
}
