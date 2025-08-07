using System;
using UnityEngine;

// Token: 0x02000D3B RID: 3387
public class ConsumableInfoTableColumn : CheckboxTableColumn
{
	// Token: 0x0600689B RID: 26779 RVA: 0x0027844C File Offset: 0x0027664C
	public ConsumableInfoTableColumn(IConsumableUIItem consumable_info, Action<IAssignableIdentity, GameObject> load_value_action, Func<IAssignableIdentity, GameObject, TableScreen.ResultValues> get_value_action, Action<GameObject> on_press_action, Action<GameObject, TableScreen.ResultValues> set_value_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip, Func<GameObject, string> get_header_label, Func<bool> reveal_test)
		: base(load_value_action, get_value_action, on_press_action, set_value_action, sort_comparison, on_tooltip, on_sort_tooltip, reveal_test)
	{
		this.consumable_info = consumable_info;
		this.get_header_label = get_header_label;
	}

	// Token: 0x0600689C RID: 26780 RVA: 0x0027847C File Offset: 0x0027667C
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		GameObject headerWidget = base.GetHeaderWidget(parent);
		if (headerWidget.GetComponentInChildren<LocText>() != null)
		{
			headerWidget.GetComponentInChildren<LocText>().text = this.get_header_label(headerWidget);
		}
		headerWidget.GetComponentInChildren<MultiToggle>().gameObject.SetActive(false);
		return headerWidget;
	}

	// Token: 0x040047BB RID: 18363
	public IConsumableUIItem consumable_info;

	// Token: 0x040047BC RID: 18364
	public Func<GameObject, string> get_header_label;
}
