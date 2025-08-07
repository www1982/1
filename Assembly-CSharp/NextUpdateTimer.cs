using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000D7A RID: 3450
[AddComponentMenu("KMonoBehaviour/scripts/NextUpdateTimer")]
public class NextUpdateTimer : KMonoBehaviour
{
	// Token: 0x06006B53 RID: 27475 RVA: 0x0028898A File Offset: 0x00286B8A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.initialAnimScale = this.UpdateAnimController.animScale;
	}

	// Token: 0x06006B54 RID: 27476 RVA: 0x002889A3 File Offset: 0x00286BA3
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06006B55 RID: 27477 RVA: 0x002889AB File Offset: 0x00286BAB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RefreshReleaseTimes();
	}

	// Token: 0x06006B56 RID: 27478 RVA: 0x002889BC File Offset: 0x00286BBC
	public void UpdateReleaseTimes(string lastUpdateTime, string nextUpdateTime, string textOverride)
	{
		if (!global::System.DateTime.TryParse(lastUpdateTime, out this.currentReleaseDate))
		{
			global::Debug.LogWarning("Failed to parse last_update_time: " + lastUpdateTime);
		}
		if (!global::System.DateTime.TryParse(nextUpdateTime, out this.nextReleaseDate))
		{
			global::Debug.LogWarning("Failed to parse next_update_time: " + nextUpdateTime);
		}
		this.m_releaseTextOverride = textOverride;
		this.RefreshReleaseTimes();
	}

	// Token: 0x06006B57 RID: 27479 RVA: 0x00288A14 File Offset: 0x00286C14
	private void RefreshReleaseTimes()
	{
		TimeSpan timeSpan = this.nextReleaseDate - this.currentReleaseDate;
		TimeSpan timeSpan2 = this.nextReleaseDate - global::System.DateTime.UtcNow;
		TimeSpan timeSpan3 = global::System.DateTime.UtcNow - this.currentReleaseDate;
		string text = "4";
		string text2;
		if (!string.IsNullOrEmpty(this.m_releaseTextOverride))
		{
			text2 = this.m_releaseTextOverride;
		}
		else if (timeSpan2.TotalHours < 8.0)
		{
			text2 = UI.DEVELOPMENTBUILDS.UPDATES.TWENTY_FOUR_HOURS;
			text = "4";
		}
		else if (timeSpan2.TotalDays < 1.0)
		{
			text2 = string.Format(UI.DEVELOPMENTBUILDS.UPDATES.FINAL_WEEK, 1);
			text = "3";
		}
		else
		{
			int num = timeSpan2.Days % 7;
			int num2 = (timeSpan2.Days - num) / 7;
			if (num2 <= 0)
			{
				text2 = string.Format(UI.DEVELOPMENTBUILDS.UPDATES.FINAL_WEEK, num);
				text = "2";
			}
			else
			{
				text2 = string.Format(UI.DEVELOPMENTBUILDS.UPDATES.BIGGER_TIMES, num, num2);
				text = "1";
			}
		}
		this.TimerText.text = text2;
		this.UpdateAnimController.Play(text, KAnim.PlayMode.Loop, 1f, 0f);
		float num3 = Mathf.Clamp01((float)(timeSpan3.TotalSeconds / timeSpan.TotalSeconds));
		this.UpdateAnimMeterController.SetPositionPercent(num3);
	}

	// Token: 0x04004914 RID: 18708
	public LocText TimerText;

	// Token: 0x04004915 RID: 18709
	public KBatchedAnimController UpdateAnimController;

	// Token: 0x04004916 RID: 18710
	public KBatchedAnimController UpdateAnimMeterController;

	// Token: 0x04004917 RID: 18711
	public float initialAnimScale;

	// Token: 0x04004918 RID: 18712
	public global::System.DateTime nextReleaseDate;

	// Token: 0x04004919 RID: 18713
	public global::System.DateTime currentReleaseDate;

	// Token: 0x0400491A RID: 18714
	private string m_releaseTextOverride;
}
