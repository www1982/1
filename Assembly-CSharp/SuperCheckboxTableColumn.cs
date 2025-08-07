using System;
using UnityEngine;

// Token: 0x02000D32 RID: 3378
public class SuperCheckboxTableColumn : CheckboxTableColumn
{
	// Token: 0x0600685D RID: 26717 RVA: 0x00276218 File Offset: 0x00274418
	public SuperCheckboxTableColumn(CheckboxTableColumn[] columns_affected, Action<IAssignableIdentity, GameObject> on_load_action, Func<IAssignableIdentity, GameObject, TableScreen.ResultValues> get_value_action, Action<GameObject> on_press_action, Action<GameObject, TableScreen.ResultValues> set_value_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip)
		: base(on_load_action, get_value_action, on_press_action, set_value_action, sort_comparison, on_tooltip, null, null)
	{
		this.columns_affected = columns_affected;
	}

	// Token: 0x0600685E RID: 26718 RVA: 0x00276254 File Offset: 0x00274454
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.prefab_super_checkbox, parent, true);
		if (widget_go.GetComponent<ToolTip>() != null)
		{
			widget_go.GetComponent<ToolTip>().OnToolTip = () => this.GetTooltip(widget_go.GetComponent<ToolTip>());
		}
		MultiToggle component = widget_go.GetComponent<MultiToggle>();
		component.onClick = (global::System.Action)Delegate.Combine(component.onClick, new global::System.Action(delegate
		{
			this.on_press_action(widget_go);
		}));
		return widget_go;
	}

	// Token: 0x0600685F RID: 26719 RVA: 0x002762E4 File Offset: 0x002744E4
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.prefab_super_checkbox, parent, true);
		if (widget_go.GetComponent<ToolTip>() != null)
		{
			widget_go.GetComponent<ToolTip>().OnToolTip = () => this.GetTooltip(widget_go.GetComponent<ToolTip>());
		}
		MultiToggle component = widget_go.GetComponent<MultiToggle>();
		component.onClick = (global::System.Action)Delegate.Combine(component.onClick, new global::System.Action(delegate
		{
			this.on_press_action(widget_go);
		}));
		return widget_go;
	}

	// Token: 0x06006860 RID: 26720 RVA: 0x00276374 File Offset: 0x00274574
	public override GameObject GetMinionWidget(GameObject parent)
	{
		GameObject widget_go = Util.KInstantiateUI(this.prefab_super_checkbox, parent, true);
		if (widget_go.GetComponent<ToolTip>() != null)
		{
			widget_go.GetComponent<ToolTip>().OnToolTip = () => this.GetTooltip(widget_go.GetComponent<ToolTip>());
		}
		MultiToggle component = widget_go.GetComponent<MultiToggle>();
		component.onClick = (global::System.Action)Delegate.Combine(component.onClick, new global::System.Action(delegate
		{
			this.on_press_action(widget_go);
		}));
		return widget_go;
	}

	// Token: 0x040047A3 RID: 18339
	public GameObject prefab_super_checkbox = Assets.UIPrefabs.TableScreenWidgets.SuperCheckbox_Horizontal;

	// Token: 0x040047A4 RID: 18340
	public CheckboxTableColumn[] columns_affected;
}
