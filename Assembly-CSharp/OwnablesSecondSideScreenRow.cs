using System;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000E14 RID: 3604
public class OwnablesSecondSideScreenRow : KMonoBehaviour
{
	// Token: 0x170007DD RID: 2013
	// (get) Token: 0x060071C0 RID: 29120 RVA: 0x002B3DC9 File Offset: 0x002B1FC9
	// (set) Token: 0x060071BF RID: 29119 RVA: 0x002B3DC0 File Offset: 0x002B1FC0
	public AssignableSlotInstance minionSlotInstance { get; private set; }

	// Token: 0x170007DE RID: 2014
	// (get) Token: 0x060071C2 RID: 29122 RVA: 0x002B3DDA File Offset: 0x002B1FDA
	// (set) Token: 0x060071C1 RID: 29121 RVA: 0x002B3DD1 File Offset: 0x002B1FD1
	public Assignable item { get; private set; }

	// Token: 0x060071C3 RID: 29123 RVA: 0x002B3DE4 File Offset: 0x002B1FE4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.toggle = base.GetComponent<MultiToggle>();
		MultiToggle multiToggle = this.toggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.OnMultitoggleClicked));
		this.eyeButton.onClick.AddListener(new UnityAction(this.FocusCameraOnAssignedItem));
	}

	// Token: 0x060071C4 RID: 29124 RVA: 0x002B3E48 File Offset: 0x002B2048
	public void SetData(AssignableSlotInstance minion, Assignable item_assignable)
	{
		this.minionSlotInstance = minion;
		this.item = item_assignable;
		this.changeAssignmentListenerIDX = this.item.Subscribe(684616645, new Action<object>(this._OnItemAssignationChanged));
		this.destroyListenerIDX = this.item.Subscribe(1969584890, new Action<object>(this._OnRowItemDestroyed));
		this.customTooltipFunc = this.item.customAssignmentUITooltipFunc;
		this.Refresh();
	}

	// Token: 0x060071C5 RID: 29125 RVA: 0x002B3EC0 File Offset: 0x002B20C0
	public void Refresh()
	{
		if (this.item != null)
		{
			this.item.PrefabID();
			string properName = this.item.GetProperName();
			this.nameLabel.text = properName;
			this.icon.sprite = Def.GetUISprite(this.item.gameObject, "ui", false).first;
			bool flag = this.item.IsAssigned() && !this.minionSlotInstance.IsUnassigning() && this.minionSlotInstance.assignable != this.item;
			if (this.item.IsAssigned())
			{
				this.statusLabel.SetText(string.Format(flag ? OwnablesSecondSideScreenRow.ASSIGNED_TO_OTHER : OwnablesSecondSideScreenRow.ASSIGNED_TO_SELF, this.item.assignee.GetProperName()));
			}
			else
			{
				this.statusLabel.SetText(OwnablesSecondSideScreenRow.NOT_ASSIGNED);
			}
			if (this.customTooltipFunc == null)
			{
				InfoDescription component = this.item.gameObject.GetComponent<InfoDescription>();
				bool flag2 = component != null && !string.IsNullOrEmpty(component.description);
				string text = (flag2 ? component.description : properName);
				this.tooltip.SizingSetting = (flag2 ? ToolTip.ToolTipSizeSetting.MaxWidthWrapContent : ToolTip.ToolTipSizeSetting.DynamicWidthNoWrap);
				this.tooltip.SetSimpleTooltip(text);
			}
			else
			{
				this.tooltip.SizingSetting = ToolTip.ToolTipSizeSetting.MaxWidthWrapContent;
				this.tooltip.SetSimpleTooltip(this.customTooltipFunc(this.minionSlotInstance.assignables));
			}
		}
		else
		{
			this.nameLabel.text = OwnablesSecondSideScreenRow.NO_DATA_MESSAGE;
			this.tooltip.SetSimpleTooltip(null);
		}
		bool flag3 = this.item != null && this.minionSlotInstance != null && !this.minionSlotInstance.IsUnassigning() && this.minionSlotInstance.assignable == this.item;
		this.toggle.ChangeState(flag3 ? 1 : 0);
		this.emptyIcon.gameObject.SetActive(this.item == null);
		this.icon.gameObject.SetActive(this.item != null);
		this.eyeButton.gameObject.SetActive(this.item != null);
		this.statusLabel.gameObject.SetActive(this.item != null);
	}

	// Token: 0x060071C6 RID: 29126 RVA: 0x002B411C File Offset: 0x002B231C
	public void ClearData()
	{
		if (this.item != null)
		{
			if (this.destroyListenerIDX != -1)
			{
				this.item.Unsubscribe(this.destroyListenerIDX);
			}
			if (this.changeAssignmentListenerIDX != -1)
			{
				this.item.Unsubscribe(this.changeAssignmentListenerIDX);
			}
		}
		this.minionSlotInstance = null;
		this.item = null;
		this.destroyListenerIDX = -1;
		this.changeAssignmentListenerIDX = -1;
		this.Refresh();
	}

	// Token: 0x060071C7 RID: 29127 RVA: 0x002B418D File Offset: 0x002B238D
	private void _OnItemAssignationChanged(object o)
	{
		Action<OwnablesSecondSideScreenRow> onRowItemAssigneeChanged = this.OnRowItemAssigneeChanged;
		if (onRowItemAssigneeChanged == null)
		{
			return;
		}
		onRowItemAssigneeChanged(this);
	}

	// Token: 0x060071C8 RID: 29128 RVA: 0x002B41A0 File Offset: 0x002B23A0
	private void _OnRowItemDestroyed(object o)
	{
		Action<OwnablesSecondSideScreenRow> onRowItemDestroyed = this.OnRowItemDestroyed;
		if (onRowItemDestroyed == null)
		{
			return;
		}
		onRowItemDestroyed(this);
	}

	// Token: 0x060071C9 RID: 29129 RVA: 0x002B41B3 File Offset: 0x002B23B3
	private void OnMultitoggleClicked()
	{
		Action<OwnablesSecondSideScreenRow> onRowClicked = this.OnRowClicked;
		if (onRowClicked == null)
		{
			return;
		}
		onRowClicked(this);
	}

	// Token: 0x060071CA RID: 29130 RVA: 0x002B41C8 File Offset: 0x002B23C8
	private void FocusCameraOnAssignedItem()
	{
		if (this.item != null)
		{
			GameObject gameObject = this.item.gameObject;
			if (this.item.HasTag(GameTags.Equipped))
			{
				gameObject = this.item.assignee.GetOwners()[0].GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
			}
			GameUtil.FocusCamera(gameObject.transform, false, true);
		}
	}

	// Token: 0x04004E3C RID: 20028
	public static string NO_DATA_MESSAGE = UI.UISIDESCREENS.OWNABLESSIDESCREEN.NO_ITEM_FOUND;

	// Token: 0x04004E3D RID: 20029
	public static string NOT_ASSIGNED = UI.UISIDESCREENS.OWNABLESSECONDSIDESCREEN.NOT_ASSIGNED;

	// Token: 0x04004E3E RID: 20030
	public static string ASSIGNED_TO_SELF = UI.UISIDESCREENS.OWNABLESSECONDSIDESCREEN.ASSIGNED_TO_SELF_STATUS;

	// Token: 0x04004E3F RID: 20031
	public static string ASSIGNED_TO_OTHER = UI.UISIDESCREENS.OWNABLESSECONDSIDESCREEN.ASSIGNED_TO_OTHER_STATUS;

	// Token: 0x04004E40 RID: 20032
	public KImage icon;

	// Token: 0x04004E41 RID: 20033
	public KImage emptyIcon;

	// Token: 0x04004E42 RID: 20034
	public LocText nameLabel;

	// Token: 0x04004E43 RID: 20035
	public LocText statusLabel;

	// Token: 0x04004E44 RID: 20036
	public Button eyeButton;

	// Token: 0x04004E45 RID: 20037
	public ToolTip tooltip;

	// Token: 0x04004E46 RID: 20038
	public Action<OwnablesSecondSideScreenRow> OnRowItemAssigneeChanged;

	// Token: 0x04004E47 RID: 20039
	public Action<OwnablesSecondSideScreenRow> OnRowItemDestroyed;

	// Token: 0x04004E48 RID: 20040
	public Action<OwnablesSecondSideScreenRow> OnRowClicked;

	// Token: 0x04004E49 RID: 20041
	public Func<Assignables, string> customTooltipFunc;

	// Token: 0x04004E4C RID: 20044
	private MultiToggle toggle;

	// Token: 0x04004E4D RID: 20045
	private int changeAssignmentListenerIDX = -1;

	// Token: 0x04004E4E RID: 20046
	private int destroyListenerIDX = -1;
}
