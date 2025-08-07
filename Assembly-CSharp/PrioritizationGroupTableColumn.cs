using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D37 RID: 3383
public class PrioritizationGroupTableColumn : TableColumn
{
	// Token: 0x06006872 RID: 26738 RVA: 0x00276AD0 File Offset: 0x00274CD0
	public PrioritizationGroupTableColumn(object user_data, Action<IAssignableIdentity, GameObject> on_load_action, Action<object, int> on_change_priority, Func<object, string> on_hover_widget, Action<object, int> on_change_header_priority, Func<object, string> on_hover_header_option_selector, Action<object> on_sort_clicked, Func<object, string> on_sort_hovered)
		: base(on_load_action, null, null, null, null, false, "")
	{
		this.userData = user_data;
		this.onChangePriority = on_change_priority;
		this.onHoverWidget = on_hover_widget;
		this.onHoverHeaderOptionSelector = on_hover_header_option_selector;
		this.onSortClicked = on_sort_clicked;
		this.onSortHovered = on_sort_hovered;
	}

	// Token: 0x06006873 RID: 26739 RVA: 0x00276B1C File Offset: 0x00274D1C
	public override GameObject GetMinionWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x06006874 RID: 26740 RVA: 0x00276B25 File Offset: 0x00274D25
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x06006875 RID: 26741 RVA: 0x00276B30 File Offset: 0x00274D30
	private GameObject GetWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.PriorityGroupSelector, parent, true);
		OptionSelector component = widget_go.GetComponent<OptionSelector>();
		component.Initialize(widget_go);
		component.OnChangePriority = delegate(object widget, int delta)
		{
			this.onChangePriority(widget, delta);
		};
		ToolTip[] componentsInChildren = widget_go.transform.GetComponentsInChildren<ToolTip>();
		if (componentsInChildren != null)
		{
			Func<string> <>9__1;
			foreach (ToolTip toolTip in componentsInChildren)
			{
				Func<string> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = () => this.onHoverWidget(widget_go));
				}
				toolTip.OnToolTip = func;
			}
		}
		return widget_go;
	}

	// Token: 0x06006876 RID: 26742 RVA: 0x00276BE4 File Offset: 0x00274DE4
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.PriorityGroupSelectorHeader, parent, true);
		HierarchyReferences component = widget_go.GetComponent<HierarchyReferences>();
		LayoutElement component2 = widget_go.GetComponentInChildren<LocText>().GetComponent<LayoutElement>();
		component2.preferredWidth = (component2.minWidth = 63f);
		Component reference = component.GetReference("Label");
		reference.GetComponent<LocText>().raycastTarget = true;
		ToolTip component3 = reference.GetComponent<ToolTip>();
		if (component3 != null)
		{
			component3.OnToolTip = () => this.onHoverWidget(widget_go);
		}
		MultiToggle componentInChildren = widget_go.GetComponentInChildren<MultiToggle>(true);
		this.column_sort_toggle = componentInChildren;
		MultiToggle multiToggle = componentInChildren;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			this.onSortClicked(widget_go);
		}));
		ToolTip component4 = componentInChildren.GetComponent<ToolTip>();
		if (component4 != null)
		{
			component4.OnToolTip = () => this.onSortHovered(widget_go);
		}
		ToolTip component5 = (component.GetReference("PrioritizeButton") as KButton).GetComponent<ToolTip>();
		if (component5 != null)
		{
			component5.OnToolTip = () => this.onHoverHeaderOptionSelector(widget_go);
		}
		return widget_go;
	}

	// Token: 0x040047B0 RID: 18352
	public object userData;

	// Token: 0x040047B1 RID: 18353
	private Action<object, int> onChangePriority;

	// Token: 0x040047B2 RID: 18354
	private Func<object, string> onHoverWidget;

	// Token: 0x040047B3 RID: 18355
	private Func<object, string> onHoverHeaderOptionSelector;

	// Token: 0x040047B4 RID: 18356
	private Action<object> onSortClicked;

	// Token: 0x040047B5 RID: 18357
	private Func<object, string> onSortHovered;
}
