using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C70 RID: 3184
[AddComponentMenu("KMonoBehaviour/scripts/BuildingChoresPanelDupeRow")]
public class BuildingChoresPanelDupeRow : KMonoBehaviour
{
	// Token: 0x06006148 RID: 24904 RVA: 0x00242052 File Offset: 0x00240252
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.button.onClick += this.OnClick;
	}

	// Token: 0x06006149 RID: 24905 RVA: 0x00242074 File Offset: 0x00240274
	public void Init(BuildingChoresPanel.DupeEntryData data)
	{
		this.choreConsumer = data.consumer;
		if (data.context.IsPotentialSuccess())
		{
			string text = ((data.context.chore.driver == data.consumer.choreDriver) ? DUPLICANTS.CHORES.PRECONDITIONS.CURRENT_ERRAND.text : string.Format(DUPLICANTS.CHORES.PRECONDITIONS.RANK_FORMAT.text, data.rank));
			this.label.text = DUPLICANTS.CHORES.PRECONDITIONS.SUCCESS_ROW.Replace("{Duplicant}", data.consumer.GetProperName()).Replace("{Rank}", text);
		}
		else
		{
			string text2 = data.context.chore.GetPreconditions()[data.context.failedPreconditionId].condition.description;
			DebugUtil.Assert(text2 != null, "Chore requires description!", data.context.chore.GetPreconditions()[data.context.failedPreconditionId].condition.id);
			if (data.context.chore.driver != null)
			{
				text2 = text2.Replace("{Assignee}", data.context.chore.driver.GetProperName());
			}
			text2 = text2.Replace("{Selected}", data.context.chore.gameObject.GetProperName());
			this.label.text = DUPLICANTS.CHORES.PRECONDITIONS.FAILURE_ROW.Replace("{Duplicant}", data.consumer.name).Replace("{Reason}", text2);
		}
		this.icon.sprite = JobsTableScreen.priorityInfo[data.personalPriority].sprite;
		this.toolTip.toolTip = BuildingChoresPanelDupeRow.TooltipForDupe(data.context, data.consumer, data.rank);
	}

	// Token: 0x0600614A RID: 24906 RVA: 0x00242247 File Offset: 0x00240447
	private void OnClick()
	{
		GameUtil.FocusCamera(this.choreConsumer.gameObject.transform.GetPosition() + Vector3.up, 2f, true, true);
	}

	// Token: 0x0600614B RID: 24907 RVA: 0x00242274 File Offset: 0x00240474
	private static string TooltipForDupe(Chore.Precondition.Context context, ChoreConsumer choreConsumer, int rank)
	{
		bool flag = context.IsPotentialSuccess();
		string text = (flag ? UI.DETAILTABS.BUILDING_CHORES.DUPE_TOOLTIP_SUCCEEDED : UI.DETAILTABS.BUILDING_CHORES.DUPE_TOOLTIP_FAILED);
		float num = 0f;
		int personalPriority = choreConsumer.GetPersonalPriority(context.chore.choreType);
		num += (float)(personalPriority * 10);
		int priority_value = context.chore.masterPriority.priority_value;
		num += (float)priority_value;
		float num2 = (float)context.priority / 10000f;
		num += num2;
		text = text.Replace("{Description}", (context.chore.driver == choreConsumer.choreDriver) ? UI.DETAILTABS.BUILDING_CHORES.DUPE_TOOLTIP_DESC_ACTIVE : UI.DETAILTABS.BUILDING_CHORES.DUPE_TOOLTIP_DESC_INACTIVE);
		string text2 = GameUtil.ChoreGroupsForChoreType(context.chore.choreType);
		string text3 = UI.UISIDESCREENS.MINIONTODOSIDESCREEN.TOOLTIP_NA.text;
		if (flag && context.chore.choreType.groups.Length != 0)
		{
			ChoreGroup choreGroup = context.chore.choreType.groups[0];
			for (int i = 1; i < context.chore.choreType.groups.Length; i++)
			{
				if (choreConsumer.GetPersonalPriority(choreGroup) < choreConsumer.GetPersonalPriority(context.chore.choreType.groups[i]))
				{
					choreGroup = context.chore.choreType.groups[i];
				}
			}
			text3 = choreGroup.Name;
		}
		text = text.Replace("{Name}", choreConsumer.name);
		text = text.Replace("{Errand}", GameUtil.GetChoreName(context.chore, context.data));
		if (!flag)
		{
			text = text.Replace("{FailedPrecondition}", context.chore.GetPreconditions()[context.failedPreconditionId].condition.description);
		}
		else
		{
			text = text.Replace("{Rank}", rank.ToString());
			text = text.Replace("{Groups}", text2);
			text = text.Replace("{BestGroup}", text3);
			text = text.Replace("{PersonalPriority}", JobsTableScreen.priorityInfo[personalPriority].name.text);
			text = text.Replace("{PersonalPriorityValue}", (personalPriority * 10).ToString());
			text = text.Replace("{Building}", context.chore.gameObject.GetProperName());
			text = text.Replace("{BuildingPriority}", priority_value.ToString());
			text = text.Replace("{TypePriority}", num2.ToString());
			text = text.Replace("{TotalPriority}", num.ToString());
		}
		return text;
	}

	// Token: 0x040041EE RID: 16878
	public Image icon;

	// Token: 0x040041EF RID: 16879
	public LocText label;

	// Token: 0x040041F0 RID: 16880
	public ToolTip toolTip;

	// Token: 0x040041F1 RID: 16881
	private ChoreConsumer choreConsumer;

	// Token: 0x040041F2 RID: 16882
	public KButton button;
}
