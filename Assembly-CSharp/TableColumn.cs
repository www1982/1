using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000D40 RID: 3392
public class TableColumn : IRender1000ms
{
	// Token: 0x1700077B RID: 1915
	// (get) Token: 0x060068FD RID: 26877 RVA: 0x0027B6C6 File Offset: 0x002798C6
	public bool isRevealed
	{
		get
		{
			return this.revealed == null || this.revealed();
		}
	}

	// Token: 0x060068FE RID: 26878 RVA: 0x0027B6E0 File Offset: 0x002798E0
	public TableColumn(Action<IAssignableIdentity, GameObject> on_load_action, Comparison<IAssignableIdentity> sort_comparison, Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip = null, Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip = null, Func<bool> revealed = null, bool should_refresh_columns = false, string scrollerID = "")
	{
		this.on_load_action = on_load_action;
		this.sort_comparer = sort_comparison;
		this.on_tooltip = on_tooltip;
		this.on_sort_tooltip = on_sort_tooltip;
		this.revealed = revealed;
		this.scrollerID = scrollerID;
		if (should_refresh_columns)
		{
			SimAndRenderScheduler.instance.Add(this, false);
		}
	}

	// Token: 0x060068FF RID: 26879 RVA: 0x0027B73C File Offset: 0x0027993C
	protected string GetTooltip(ToolTip tool_tip_instance)
	{
		GameObject gameObject = tool_tip_instance.gameObject;
		HierarchyReferences component = tool_tip_instance.GetComponent<HierarchyReferences>();
		if (component != null && component.HasReference("Widget"))
		{
			gameObject = component.GetReference("Widget").gameObject;
		}
		TableRow tableRow = null;
		foreach (KeyValuePair<TableRow, GameObject> keyValuePair in this.widgets_by_row)
		{
			if (keyValuePair.Value == gameObject)
			{
				tableRow = keyValuePair.Key;
				break;
			}
		}
		if (tableRow != null && this.on_tooltip != null)
		{
			this.on_tooltip(tableRow.GetIdentity(), gameObject, tool_tip_instance);
		}
		return "";
	}

	// Token: 0x06006900 RID: 26880 RVA: 0x0027B804 File Offset: 0x00279A04
	protected string GetSortTooltip(ToolTip sort_tooltip_instance)
	{
		GameObject gameObject = sort_tooltip_instance.transform.parent.gameObject;
		TableRow tableRow = null;
		foreach (KeyValuePair<TableRow, GameObject> keyValuePair in this.widgets_by_row)
		{
			if (keyValuePair.Value == gameObject)
			{
				tableRow = keyValuePair.Key;
				break;
			}
		}
		if (tableRow != null && this.on_sort_tooltip != null)
		{
			this.on_sort_tooltip(tableRow.GetIdentity(), gameObject, sort_tooltip_instance);
		}
		return "";
	}

	// Token: 0x1700077C RID: 1916
	// (get) Token: 0x06006901 RID: 26881 RVA: 0x0027B8A8 File Offset: 0x00279AA8
	public bool isDirty
	{
		get
		{
			return this.dirty;
		}
	}

	// Token: 0x06006902 RID: 26882 RVA: 0x0027B8B0 File Offset: 0x00279AB0
	public bool ContainsWidget(GameObject widget)
	{
		return this.widgets_by_row.ContainsValue(widget);
	}

	// Token: 0x06006903 RID: 26883 RVA: 0x0027B8BE File Offset: 0x00279ABE
	public virtual GameObject GetMinionWidget(GameObject parent)
	{
		global::Debug.LogError("Table Column has no Widget prefab");
		return null;
	}

	// Token: 0x06006904 RID: 26884 RVA: 0x0027B8CB File Offset: 0x00279ACB
	public virtual GameObject GetHeaderWidget(GameObject parent)
	{
		global::Debug.LogError("Table Column has no Widget prefab");
		return null;
	}

	// Token: 0x06006905 RID: 26885 RVA: 0x0027B8D8 File Offset: 0x00279AD8
	public virtual GameObject GetDefaultWidget(GameObject parent)
	{
		global::Debug.LogError("Table Column has no Widget prefab");
		return null;
	}

	// Token: 0x06006906 RID: 26886 RVA: 0x0027B8E5 File Offset: 0x00279AE5
	public void Render1000ms(float dt)
	{
		this.MarkDirty(null, TableScreen.ResultValues.False);
	}

	// Token: 0x06006907 RID: 26887 RVA: 0x0027B8EF File Offset: 0x00279AEF
	public void MarkDirty(GameObject triggering_obj = null, TableScreen.ResultValues triggering_object_state = TableScreen.ResultValues.False)
	{
		this.dirty = true;
	}

	// Token: 0x06006908 RID: 26888 RVA: 0x0027B8F8 File Offset: 0x00279AF8
	public void MarkClean()
	{
		this.dirty = false;
	}

	// Token: 0x040047D2 RID: 18386
	public Action<IAssignableIdentity, GameObject> on_load_action;

	// Token: 0x040047D3 RID: 18387
	public Action<IAssignableIdentity, GameObject, ToolTip> on_tooltip;

	// Token: 0x040047D4 RID: 18388
	public Action<IAssignableIdentity, GameObject, ToolTip> on_sort_tooltip;

	// Token: 0x040047D5 RID: 18389
	public Comparison<IAssignableIdentity> sort_comparer;

	// Token: 0x040047D6 RID: 18390
	public Dictionary<TableRow, GameObject> widgets_by_row = new Dictionary<TableRow, GameObject>();

	// Token: 0x040047D7 RID: 18391
	public string scrollerID;

	// Token: 0x040047D8 RID: 18392
	public TableScreen screen;

	// Token: 0x040047D9 RID: 18393
	public MultiToggle column_sort_toggle;

	// Token: 0x040047DA RID: 18394
	private Func<bool> revealed;

	// Token: 0x040047DB RID: 18395
	protected bool dirty;
}
