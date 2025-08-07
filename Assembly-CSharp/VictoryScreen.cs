using System;
using UnityEngine;

// Token: 0x02000E78 RID: 3704
public class VictoryScreen : KModalScreen
{
	// Token: 0x0600761A RID: 30234 RVA: 0x002D32FF File Offset: 0x002D14FF
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Init();
	}

	// Token: 0x0600761B RID: 30235 RVA: 0x002D330D File Offset: 0x002D150D
	private void Init()
	{
		if (this.DismissButton)
		{
			this.DismissButton.onClick += delegate
			{
				this.Dismiss();
			};
		}
	}

	// Token: 0x0600761C RID: 30236 RVA: 0x002D3333 File Offset: 0x002D1533
	private void Retire()
	{
		if (RetireColonyUtility.SaveColonySummaryData())
		{
			this.Show(false);
		}
	}

	// Token: 0x0600761D RID: 30237 RVA: 0x002D3343 File Offset: 0x002D1543
	private void Dismiss()
	{
		this.Show(false);
	}

	// Token: 0x0600761E RID: 30238 RVA: 0x002D334C File Offset: 0x002D154C
	public void SetAchievements(string[] achievementIDs)
	{
		string text = "";
		for (int i = 0; i < achievementIDs.Length; i++)
		{
			if (i > 0)
			{
				text += "\n";
			}
			text += GameUtil.ApplyBoldString(Db.Get().ColonyAchievements.Get(achievementIDs[i]).Name);
			text = text + "\n" + Db.Get().ColonyAchievements.Get(achievementIDs[i]).description;
		}
		this.descriptionText.text = text;
	}

	// Token: 0x040051EC RID: 20972
	[SerializeField]
	private KButton DismissButton;

	// Token: 0x040051ED RID: 20973
	[SerializeField]
	private LocText descriptionText;
}
