using System;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x02000C71 RID: 3185
public class BuildingGroupScreen : KScreen
{
	// Token: 0x170006F6 RID: 1782
	// (get) Token: 0x0600614D RID: 24909 RVA: 0x002424F6 File Offset: 0x002406F6
	public static bool SearchIsEmpty
	{
		get
		{
			return BuildingGroupScreen.Instance == null || BuildingGroupScreen.Instance.inputField.text.IsNullOrWhiteSpace();
		}
	}

	// Token: 0x170006F7 RID: 1783
	// (get) Token: 0x0600614E RID: 24910 RVA: 0x0024251B File Offset: 0x0024071B
	public static bool IsEditing
	{
		get
		{
			return !(BuildingGroupScreen.Instance == null) && BuildingGroupScreen.Instance.isEditing;
		}
	}

	// Token: 0x0600614F RID: 24911 RVA: 0x00242536 File Offset: 0x00240736
	protected override void OnPrefabInit()
	{
		BuildingGroupScreen.Instance = this;
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06006150 RID: 24912 RVA: 0x0024254C File Offset: 0x0024074C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KInputTextField kinputTextField = this.inputField;
		kinputTextField.onFocus = (global::System.Action)Delegate.Combine(kinputTextField.onFocus, new global::System.Action(delegate
		{
			base.isEditing = true;
			UISounds.PlaySound(UISounds.Sound.ClickHUD);
			this.ConfigurePlanScreenForSearch();
		}));
		this.inputField.onEndEdit.AddListener(delegate(string value)
		{
			base.isEditing = false;
		});
		this.inputField.OnValueChangesPaused = delegate
		{
			PlanScreen.Instance.RefreshCategoryPanelTitle();
			PlanScreen.Instance.RefreshSearch();
		};
		this.inputField.placeholder.GetComponent<TextMeshProUGUI>().text = UI.BUILDMENU.SEARCH_TEXT_PLACEHOLDER;
		this.clearButton.onClick += this.ClearSearch;
	}

	// Token: 0x06006151 RID: 24913 RVA: 0x00242602 File Offset: 0x00240802
	protected override void OnActivate()
	{
		base.OnActivate();
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06006152 RID: 24914 RVA: 0x00242611 File Offset: 0x00240811
	public void ClearSearch()
	{
		this.inputField.text = "";
		this.inputField.ForceChangeValueRefresh();
	}

	// Token: 0x06006153 RID: 24915 RVA: 0x0024262E File Offset: 0x0024082E
	private void ConfigurePlanScreenForSearch()
	{
		PlanScreen.Instance.SoftCloseRecipe();
		PlanScreen.Instance.ClearSelection();
		PlanScreen.Instance.ForceRefreshAllBuildingToggles();
		PlanScreen.Instance.ConfigurePanelSize(null);
	}

	// Token: 0x040041F3 RID: 16883
	public static BuildingGroupScreen Instance;

	// Token: 0x040041F4 RID: 16884
	public KInputTextField inputField;

	// Token: 0x040041F5 RID: 16885
	[SerializeField]
	public KButton clearButton;
}
