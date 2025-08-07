using System;
using UnityEngine;

// Token: 0x02000D38 RID: 3384
public class PrioritizeRowTableColumn : TableColumn
{
	// Token: 0x06006877 RID: 26743 RVA: 0x00276D14 File Offset: 0x00274F14
	public PrioritizeRowTableColumn(object user_data, Action<object, int> on_change_priority, Func<object, int, string> on_hover_widget)
		: base(null, null, null, null, null, false, "")
	{
		this.userData = user_data;
		this.onChangePriority = on_change_priority;
		this.onHoverWidget = on_hover_widget;
	}

	// Token: 0x06006878 RID: 26744 RVA: 0x00276D3C File Offset: 0x00274F3C
	public override GameObject GetMinionWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x06006879 RID: 26745 RVA: 0x00276D45 File Offset: 0x00274F45
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		return this.GetWidget(parent);
	}

	// Token: 0x0600687A RID: 26746 RVA: 0x00276D4E File Offset: 0x00274F4E
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		return Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.PrioritizeRowHeaderWidget, parent, true);
	}

	// Token: 0x0600687B RID: 26747 RVA: 0x00276D68 File Offset: 0x00274F68
	private GameObject GetWidget(GameObject parent)
	{
		GameObject gameObject = Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.PrioritizeRowWidget, parent, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		this.ConfigureButton(component, "UpButton", 1, gameObject);
		this.ConfigureButton(component, "DownButton", -1, gameObject);
		return gameObject;
	}

	// Token: 0x0600687C RID: 26748 RVA: 0x00276DB0 File Offset: 0x00274FB0
	private void ConfigureButton(HierarchyReferences refs, string ref_id, int delta, GameObject widget_go)
	{
		KButton kbutton = refs.GetReference(ref_id) as KButton;
		kbutton.onClick += delegate
		{
			this.onChangePriority(widget_go, delta);
		};
		ToolTip component = kbutton.GetComponent<ToolTip>();
		if (component != null)
		{
			component.OnToolTip = () => this.onHoverWidget(widget_go, delta);
		}
	}

	// Token: 0x040047B6 RID: 18358
	public object userData;

	// Token: 0x040047B7 RID: 18359
	private Action<object, int> onChangePriority;

	// Token: 0x040047B8 RID: 18360
	private Func<object, int, string> onHoverWidget;
}
