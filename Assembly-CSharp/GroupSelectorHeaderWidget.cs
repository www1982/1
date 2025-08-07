using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E83 RID: 3715
public class GroupSelectorHeaderWidget : MonoBehaviour
{
	// Token: 0x06007662 RID: 30306 RVA: 0x002D4704 File Offset: 0x002D2904
	public void Initialize(object widget_id, IList<GroupSelectorWidget.ItemData> options, GroupSelectorHeaderWidget.ItemCallbacks item_callbacks)
	{
		GroupSelectorHeaderWidget.<>c__DisplayClass11_0 CS$<>8__locals1 = new GroupSelectorHeaderWidget.<>c__DisplayClass11_0();
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.widget_id = widget_id;
		this.widgetID = CS$<>8__locals1.widget_id;
		this.options = options;
		this.itemCallbacks = item_callbacks;
		if (this.itemCallbacks.getTitleHoverText != null)
		{
			this.label.GetComponent<ToolTip>().OnToolTip = () => CS$<>8__locals1.<>4__this.itemCallbacks.getTitleHoverText(CS$<>8__locals1.widget_id);
		}
		bool adding_item2 = true;
		Func<object, IList<int>> <>9__5;
		Func<object, object, string> <>9__6;
		this.addItemButton.onClick += delegate
		{
			GroupSelectorHeaderWidget <>4__this = CS$<>8__locals1.<>4__this;
			Vector3 position = CS$<>8__locals1.<>4__this.addItemButton.transform.GetPosition();
			Func<object, IList<int>> func;
			if ((func = <>9__5) == null)
			{
				func = (<>9__5 = (object widget_go) => CS$<>8__locals1.<>4__this.itemCallbacks.getHeaderButtonOptions(widget_go, adding_item2));
			}
			Action<object> onItemAdded = CS$<>8__locals1.<>4__this.itemCallbacks.onItemAdded;
			Func<object, object, string> func2;
			if ((func2 = <>9__6) == null)
			{
				func2 = (<>9__6 = (object widget_go, object item_data) => CS$<>8__locals1.<>4__this.itemCallbacks.getItemHoverText(widget_go, adding_item2, item_data));
			}
			<>4__this.RebuildSubPanel(position, func, onItemAdded, func2);
		};
		bool adding_item = false;
		Func<object, IList<int>> <>9__8;
		Func<object, object, string> <>9__9;
		this.removeItemButton.onClick += delegate
		{
			GroupSelectorHeaderWidget <>4__this2 = CS$<>8__locals1.<>4__this;
			Vector3 position2 = CS$<>8__locals1.<>4__this.removeItemButton.transform.GetPosition();
			Func<object, IList<int>> func3;
			if ((func3 = <>9__8) == null)
			{
				func3 = (<>9__8 = (object widget_go) => CS$<>8__locals1.<>4__this.itemCallbacks.getHeaderButtonOptions(widget_go, adding_item));
			}
			Action<object> onItemRemoved = CS$<>8__locals1.<>4__this.itemCallbacks.onItemRemoved;
			Func<object, object, string> func4;
			if ((func4 = <>9__9) == null)
			{
				func4 = (<>9__9 = (object widget_go, object item_data) => CS$<>8__locals1.<>4__this.itemCallbacks.getItemHoverText(widget_go, adding_item, item_data));
			}
			<>4__this2.RebuildSubPanel(position2, func3, onItemRemoved, func4);
		};
		this.sortButton.onClick += delegate
		{
			GroupSelectorHeaderWidget <>4__this3 = CS$<>8__locals1.<>4__this;
			Vector3 position3 = CS$<>8__locals1.<>4__this.sortButton.transform.GetPosition();
			Func<object, IList<int>> getValidSortOptionIndices = CS$<>8__locals1.<>4__this.itemCallbacks.getValidSortOptionIndices;
			Action<object> action;
			if ((action = CS$<>8__locals1.<>9__10) == null)
			{
				action = (CS$<>8__locals1.<>9__10 = delegate(object item_data)
				{
					CS$<>8__locals1.<>4__this.itemCallbacks.onSort(CS$<>8__locals1.<>4__this.widgetID, item_data);
				});
			}
			Func<object, object, string> func5;
			if ((func5 = CS$<>8__locals1.<>9__11) == null)
			{
				func5 = (CS$<>8__locals1.<>9__11 = (object widget_go, object item_data) => CS$<>8__locals1.<>4__this.itemCallbacks.getSortHoverText(item_data));
			}
			<>4__this3.RebuildSubPanel(position3, getValidSortOptionIndices, action, func5);
		};
		if (this.itemCallbacks.getTitleButtonHoverText != null)
		{
			this.addItemButton.GetComponent<ToolTip>().OnToolTip = () => CS$<>8__locals1.<>4__this.itemCallbacks.getTitleButtonHoverText(CS$<>8__locals1.widget_id, true);
			this.removeItemButton.GetComponent<ToolTip>().OnToolTip = () => CS$<>8__locals1.<>4__this.itemCallbacks.getTitleButtonHoverText(CS$<>8__locals1.widget_id, false);
		}
	}

	// Token: 0x06007663 RID: 30307 RVA: 0x002D481C File Offset: 0x002D2A1C
	private void RebuildSubPanel(Vector3 pos, Func<object, IList<int>> display_list_query, Action<object> on_item_selected, Func<object, object, string> get_item_hover_text)
	{
		this.itemsPanel.gameObject.transform.SetPosition(pos + new Vector3(2f, 2f, 0f));
		IList<int> list = display_list_query(this.widgetID);
		if (list.Count > 0)
		{
			this.ClearSubPanelOptions();
			foreach (int num in list)
			{
				int idx = num;
				GroupSelectorWidget.ItemData itemData = this.options[idx];
				GameObject gameObject = Util.KInstantiateUI(this.itemTemplate, this.itemsPanel.gameObject, true);
				KButton component = gameObject.GetComponent<KButton>();
				component.fgImage.sprite = this.options[idx].sprite;
				component.onClick += delegate
				{
					on_item_selected(this.options[idx].userData);
					this.RebuildSubPanel(pos, display_list_query, on_item_selected, get_item_hover_text);
				};
				if (get_item_hover_text != null)
				{
					gameObject.GetComponent<ToolTip>().OnToolTip = () => get_item_hover_text(this.widgetID, this.options[idx].userData);
				}
			}
			this.itemsPanel.GetComponent<GridLayoutGroup>().constraintCount = Mathf.Min(this.numExpectedPanelColumns, this.itemsPanel.childCount);
			this.itemsPanel.gameObject.SetActive(true);
			this.itemsPanel.GetComponent<Selectable>().Select();
			return;
		}
		this.CloseSubPanel();
	}

	// Token: 0x06007664 RID: 30308 RVA: 0x002D49DC File Offset: 0x002D2BDC
	public void CloseSubPanel()
	{
		this.ClearSubPanelOptions();
		this.itemsPanel.gameObject.SetActive(false);
	}

	// Token: 0x06007665 RID: 30309 RVA: 0x002D49F8 File Offset: 0x002D2BF8
	private void ClearSubPanelOptions()
	{
		foreach (object obj in this.itemsPanel.transform)
		{
			Util.KDestroyGameObject(((Transform)obj).gameObject);
		}
	}

	// Token: 0x04005234 RID: 21044
	public LocText label;

	// Token: 0x04005235 RID: 21045
	[SerializeField]
	private GameObject itemTemplate;

	// Token: 0x04005236 RID: 21046
	[SerializeField]
	private RectTransform itemsPanel;

	// Token: 0x04005237 RID: 21047
	[SerializeField]
	private KButton addItemButton;

	// Token: 0x04005238 RID: 21048
	[SerializeField]
	private KButton removeItemButton;

	// Token: 0x04005239 RID: 21049
	[SerializeField]
	private KButton sortButton;

	// Token: 0x0400523A RID: 21050
	[SerializeField]
	private int numExpectedPanelColumns = 3;

	// Token: 0x0400523B RID: 21051
	private object widgetID;

	// Token: 0x0400523C RID: 21052
	private GroupSelectorHeaderWidget.ItemCallbacks itemCallbacks;

	// Token: 0x0400523D RID: 21053
	private IList<GroupSelectorWidget.ItemData> options;

	// Token: 0x02002070 RID: 8304
	public struct ItemCallbacks
	{
		// Token: 0x04009435 RID: 37941
		public Func<object, string> getTitleHoverText;

		// Token: 0x04009436 RID: 37942
		public Func<object, bool, string> getTitleButtonHoverText;

		// Token: 0x04009437 RID: 37943
		public Func<object, bool, IList<int>> getHeaderButtonOptions;

		// Token: 0x04009438 RID: 37944
		public Action<object> onItemAdded;

		// Token: 0x04009439 RID: 37945
		public Action<object> onItemRemoved;

		// Token: 0x0400943A RID: 37946
		public Func<object, bool, object, string> getItemHoverText;

		// Token: 0x0400943B RID: 37947
		public Func<object, IList<int>> getValidSortOptionIndices;

		// Token: 0x0400943C RID: 37948
		public Func<object, string> getSortHoverText;

		// Token: 0x0400943D RID: 37949
		public Action<object, object> onSort;
	}
}
