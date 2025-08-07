using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DE7 RID: 3559
public class ConditionListSideScreen : SideScreenContent
{
	// Token: 0x0600707B RID: 28795 RVA: 0x002AD339 File Offset: 0x002AB539
	public override bool IsValidForTarget(GameObject target)
	{
		return false;
	}

	// Token: 0x0600707C RID: 28796 RVA: 0x002AD33C File Offset: 0x002AB53C
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		if (target != null)
		{
			this.targetConditionSet = target.GetComponent<IProcessConditionSet>();
		}
	}

	// Token: 0x0600707D RID: 28797 RVA: 0x002AD35A File Offset: 0x002AB55A
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.Refresh();
		}
	}

	// Token: 0x0600707E RID: 28798 RVA: 0x002AD36C File Offset: 0x002AB56C
	private void Refresh()
	{
		bool flag = false;
		List<ProcessCondition> conditionSet = this.targetConditionSet.GetConditionSet(ProcessCondition.ProcessConditionType.All);
		foreach (ProcessCondition processCondition in conditionSet)
		{
			if (!this.rows.ContainsKey(processCondition))
			{
				flag = true;
				break;
			}
		}
		foreach (KeyValuePair<ProcessCondition, GameObject> keyValuePair in this.rows)
		{
			if (!conditionSet.Contains(keyValuePair.Key))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.Rebuild();
		}
		foreach (KeyValuePair<ProcessCondition, GameObject> keyValuePair2 in this.rows)
		{
			ConditionListSideScreen.SetRowState(keyValuePair2.Value, keyValuePair2.Key);
		}
	}

	// Token: 0x0600707F RID: 28799 RVA: 0x002AD480 File Offset: 0x002AB680
	public static void SetRowState(GameObject row, ProcessCondition condition)
	{
		HierarchyReferences component = row.GetComponent<HierarchyReferences>();
		ProcessCondition.Status status = condition.EvaluateCondition();
		component.GetReference<LocText>("Label").text = condition.GetStatusMessage(status);
		switch (status)
		{
		case ProcessCondition.Status.Failure:
			component.GetReference<LocText>("Label").color = ConditionListSideScreen.failedColor;
			component.GetReference<Image>("Box").color = ConditionListSideScreen.failedColor;
			break;
		case ProcessCondition.Status.Warning:
			component.GetReference<LocText>("Label").color = ConditionListSideScreen.warningColor;
			component.GetReference<Image>("Box").color = ConditionListSideScreen.warningColor;
			break;
		case ProcessCondition.Status.Ready:
			component.GetReference<LocText>("Label").color = ConditionListSideScreen.readyColor;
			component.GetReference<Image>("Box").color = ConditionListSideScreen.readyColor;
			break;
		}
		component.GetReference<Image>("Check").gameObject.SetActive(status == ProcessCondition.Status.Ready);
		component.GetReference<Image>("Dash").gameObject.SetActive(false);
		row.GetComponent<ToolTip>().SetSimpleTooltip(condition.GetStatusTooltip(status));
	}

	// Token: 0x06007080 RID: 28800 RVA: 0x002AD58C File Offset: 0x002AB78C
	private void Rebuild()
	{
		this.ClearRows();
		this.BuildRows();
	}

	// Token: 0x06007081 RID: 28801 RVA: 0x002AD59C File Offset: 0x002AB79C
	private void ClearRows()
	{
		foreach (KeyValuePair<ProcessCondition, GameObject> keyValuePair in this.rows)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.rows.Clear();
	}

	// Token: 0x06007082 RID: 28802 RVA: 0x002AD600 File Offset: 0x002AB800
	private void BuildRows()
	{
		foreach (ProcessCondition processCondition in this.targetConditionSet.GetConditionSet(ProcessCondition.ProcessConditionType.All))
		{
			if (processCondition.ShowInUI())
			{
				GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.rowContainer, true);
				this.rows.Add(processCondition, gameObject);
			}
		}
	}

	// Token: 0x04004D6E RID: 19822
	public GameObject rowPrefab;

	// Token: 0x04004D6F RID: 19823
	public GameObject rowContainer;

	// Token: 0x04004D70 RID: 19824
	[Tooltip("This list is indexed by the ProcessCondition.Status enum")]
	public static Color readyColor = Color.black;

	// Token: 0x04004D71 RID: 19825
	public static Color failedColor = Color.red;

	// Token: 0x04004D72 RID: 19826
	public static Color warningColor = new Color(1f, 0.3529412f, 0f, 1f);

	// Token: 0x04004D73 RID: 19827
	private IProcessConditionSet targetConditionSet;

	// Token: 0x04004D74 RID: 19828
	private Dictionary<ProcessCondition, GameObject> rows = new Dictionary<ProcessCondition, GameObject>();
}
