using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DD7 RID: 3543
public class BaseGameImpactorImperativeSideScreen : SideScreenContent
{
	// Token: 0x06006FD4 RID: 28628 RVA: 0x002A8EDC File Offset: 0x002A70DC
	public override bool IsValidForTarget(GameObject target)
	{
		if (DlcManager.IsExpansion1Active())
		{
			return false;
		}
		MissileLauncher.Instance smi = target.GetSMI<MissileLauncher.Instance>();
		return smi != null && this.StatusMonitor != null && smi.AmmunitionIsAllowed("MissileLongRange");
	}

	// Token: 0x170007B3 RID: 1971
	// (get) Token: 0x06006FD5 RID: 28629 RVA: 0x002A8F18 File Offset: 0x002A7118
	private LargeImpactorStatus.Instance StatusMonitor
	{
		get
		{
			if (this.statusMonitor == null)
			{
				GameplayEventInstance gameplayEventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.Id, -1);
				if (gameplayEventInstance != null)
				{
					LargeImpactorEvent.StatesInstance statesInstance = (LargeImpactorEvent.StatesInstance)gameplayEventInstance.smi;
					this.statusMonitor = statesInstance.impactorInstance.GetSMI<LargeImpactorStatus.Instance>();
				}
			}
			return this.statusMonitor;
		}
	}

	// Token: 0x06006FD6 RID: 28630 RVA: 0x002A8F78 File Offset: 0x002A7178
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetMissileLauncher = target.GetSMI<MissileLauncher.Instance>();
		this.Build();
	}

	// Token: 0x06006FD7 RID: 28631 RVA: 0x002A8F94 File Offset: 0x002A7194
	private void Build()
	{
		if (this.StatusMonitor != null)
		{
			this.healthBarFill.fillAmount = Mathf.Max((float)this.StatusMonitor.Health / (float)this.StatusMonitor.def.MAX_HEALTH, 0f);
			this.healthBarTooltip.toolTip = GameUtil.SafeStringFormat(UI.UISIDESCREENS.MISSILESELECTIONSIDESCREEN.VANILLALARGEIMPACTOR.HEALTH_BAR_TOOLTIP, new object[]
			{
				this.StatusMonitor.Health,
				this.StatusMonitor.def.MAX_HEALTH
			});
			this.timeBarFill.fillAmount = this.StatusMonitor.TimeRemainingBeforeCollision / LargeImpactorEvent.GetImpactTime();
			this.timeBarTooltip.toolTip = GameUtil.SafeStringFormat(UI.UISIDESCREENS.MISSILESELECTIONSIDESCREEN.VANILLALARGEIMPACTOR.TIME_UNTIL_COLLISION_TOOLTIP, new object[] { GameUtil.GetFormattedCycles(this.StatusMonitor.TimeRemainingBeforeCollision, "F1", false).Split(' ', StringSplitOptions.None)[0] });
		}
	}

	// Token: 0x04004CEB RID: 19691
	private MissileLauncher.Instance targetMissileLauncher;

	// Token: 0x04004CEC RID: 19692
	[SerializeField]
	private Image healthBarFill;

	// Token: 0x04004CED RID: 19693
	[SerializeField]
	private Image timeBarFill;

	// Token: 0x04004CEE RID: 19694
	[SerializeField]
	private LocText healthBarLabel;

	// Token: 0x04004CEF RID: 19695
	[SerializeField]
	private LocText timeBarLabel;

	// Token: 0x04004CF0 RID: 19696
	[SerializeField]
	private ToolTip healthBarTooltip;

	// Token: 0x04004CF1 RID: 19697
	[SerializeField]
	private ToolTip timeBarTooltip;

	// Token: 0x04004CF2 RID: 19698
	private LargeImpactorStatus.Instance statusMonitor;
}
