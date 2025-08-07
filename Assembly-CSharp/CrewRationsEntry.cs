using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000D2E RID: 3374
public class CrewRationsEntry : CrewListEntry
{
	// Token: 0x0600684C RID: 26700 RVA: 0x00275966 File Offset: 0x00273B66
	public override void Populate(MinionIdentity _identity)
	{
		base.Populate(_identity);
		this.rationMonitor = _identity.GetSMI<RationMonitor.Instance>();
		this.Refresh();
	}

	// Token: 0x0600684D RID: 26701 RVA: 0x00275984 File Offset: 0x00273B84
	public override void Refresh()
	{
		base.Refresh();
		this.rationsEatenToday.text = GameUtil.GetFormattedCalories(this.rationMonitor.GetRationsAteToday(), GameUtil.TimeSlice.None, true);
		if (this.identity == null)
		{
			return;
		}
		foreach (AmountInstance amountInstance in this.identity.GetAmounts())
		{
			float min = amountInstance.GetMin();
			float max = amountInstance.GetMax();
			float num = max - min;
			string text = Mathf.RoundToInt((num - (max - amountInstance.value)) / num * 100f).ToString();
			if (amountInstance.amount == Db.Get().Amounts.Stress)
			{
				this.currentStressText.text = amountInstance.GetValueString();
				this.currentStressText.GetComponent<ToolTip>().toolTip = amountInstance.GetTooltip();
				this.stressTrendImage.SetValue(amountInstance);
			}
			else if (amountInstance.amount == Db.Get().Amounts.Calories)
			{
				this.currentCaloriesText.text = text + "%";
				this.currentCaloriesText.GetComponent<ToolTip>().toolTip = amountInstance.GetTooltip();
			}
			else if (amountInstance.amount == Db.Get().Amounts.HitPoints)
			{
				this.currentHealthText.text = text + "%";
				this.currentHealthText.GetComponent<ToolTip>().toolTip = amountInstance.GetTooltip();
			}
		}
	}

	// Token: 0x0400478F RID: 18319
	public KButton incRationPerDayButton;

	// Token: 0x04004790 RID: 18320
	public KButton decRationPerDayButton;

	// Token: 0x04004791 RID: 18321
	public LocText rationPerDayText;

	// Token: 0x04004792 RID: 18322
	public LocText rationsEatenToday;

	// Token: 0x04004793 RID: 18323
	public LocText currentCaloriesText;

	// Token: 0x04004794 RID: 18324
	public LocText currentStressText;

	// Token: 0x04004795 RID: 18325
	public LocText currentHealthText;

	// Token: 0x04004796 RID: 18326
	public ValueTrendImageToggle stressTrendImage;

	// Token: 0x04004797 RID: 18327
	private RationMonitor.Instance rationMonitor;
}
