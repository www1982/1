using System;
using UnityEngine;

// Token: 0x02000D41 RID: 3393
public class DividerColumn : TableColumn
{
	// Token: 0x06006909 RID: 26889 RVA: 0x0027B904 File Offset: 0x00279B04
	public DividerColumn(Func<bool> revealed = null, string scrollerID = "")
		: base(delegate(IAssignableIdentity minion, GameObject widget_go)
		{
			if (revealed != null)
			{
				if (revealed())
				{
					if (!widget_go.activeSelf)
					{
						widget_go.SetActive(true);
						return;
					}
				}
				else if (widget_go.activeSelf)
				{
					widget_go.SetActive(false);
					return;
				}
			}
			else
			{
				widget_go.SetActive(true);
			}
		}, null, null, null, revealed, false, scrollerID)
	{
	}

	// Token: 0x0600690A RID: 26890 RVA: 0x0027B93B File Offset: 0x00279B3B
	public override GameObject GetDefaultWidget(GameObject parent)
	{
		return Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.Spacer, parent, true);
	}

	// Token: 0x0600690B RID: 26891 RVA: 0x0027B953 File Offset: 0x00279B53
	public override GameObject GetMinionWidget(GameObject parent)
	{
		return Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.Spacer, parent, true);
	}

	// Token: 0x0600690C RID: 26892 RVA: 0x0027B96B File Offset: 0x00279B6B
	public override GameObject GetHeaderWidget(GameObject parent)
	{
		return Util.KInstantiateUI(Assets.UIPrefabs.TableScreenWidgets.Spacer, parent, true);
	}
}
