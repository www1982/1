using System;
using STRINGS;

// Token: 0x02000B7E RID: 2942
public class ConditionDestinationReachable : ProcessCondition
{
	// Token: 0x06005807 RID: 22535 RVA: 0x001FE123 File Offset: 0x001FC323
	public ConditionDestinationReachable(RocketModule module)
	{
		this.module = module;
		this.craftRegisterType = module.GetComponent<ILaunchableRocket>().registerType;
	}

	// Token: 0x06005808 RID: 22536 RVA: 0x001FE144 File Offset: 0x001FC344
	public override ProcessCondition.Status EvaluateCondition()
	{
		ProcessCondition.Status status = ProcessCondition.Status.Failure;
		LaunchableRocketRegisterType launchableRocketRegisterType = this.craftRegisterType;
		if (launchableRocketRegisterType != LaunchableRocketRegisterType.Spacecraft)
		{
			if (launchableRocketRegisterType == LaunchableRocketRegisterType.Clustercraft)
			{
				if (!this.module.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<RocketClusterDestinationSelector>().IsAtDestination())
				{
					status = ProcessCondition.Status.Ready;
				}
			}
		}
		else
		{
			int id = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.module.GetComponent<LaunchConditionManager>()).id;
			SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(id);
			if (spacecraftDestination != null && this.CanReachSpacecraftDestination(spacecraftDestination) && spacecraftDestination.GetDestinationType().visitable)
			{
				status = ProcessCondition.Status.Ready;
			}
		}
		return status;
	}

	// Token: 0x06005809 RID: 22537 RVA: 0x001FE1C8 File Offset: 0x001FC3C8
	public bool CanReachSpacecraftDestination(SpaceDestination destination)
	{
		Debug.Assert(!DlcManager.FeatureClusterSpaceEnabled());
		float rocketMaxDistance = this.module.GetComponent<CommandModule>().rocketStats.GetRocketMaxDistance();
		return (float)destination.OneBasedDistance * 10000f <= rocketMaxDistance;
	}

	// Token: 0x0600580A RID: 22538 RVA: 0x001FE20C File Offset: 0x001FC40C
	public SpaceDestination GetSpacecraftDestination()
	{
		Debug.Assert(!DlcManager.FeatureClusterSpaceEnabled());
		int id = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.module.GetComponent<LaunchConditionManager>()).id;
		return SpacecraftManager.instance.GetSpacecraftDestination(id);
	}

	// Token: 0x0600580B RID: 22539 RVA: 0x001FE24C File Offset: 0x001FC44C
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text = "";
		LaunchableRocketRegisterType launchableRocketRegisterType = this.craftRegisterType;
		if (launchableRocketRegisterType != LaunchableRocketRegisterType.Spacecraft)
		{
			if (launchableRocketRegisterType == LaunchableRocketRegisterType.Clustercraft)
			{
				text = UI.STARMAP.DESTINATIONSELECTION.REACHABLE;
			}
		}
		else if (status == ProcessCondition.Status.Ready && this.GetSpacecraftDestination() != null)
		{
			text = UI.STARMAP.DESTINATIONSELECTION.REACHABLE;
		}
		else if (this.GetSpacecraftDestination() != null)
		{
			text = UI.STARMAP.DESTINATIONSELECTION.UNREACHABLE;
		}
		else
		{
			text = UI.STARMAP.DESTINATIONSELECTION.NOTSELECTED;
		}
		return text;
	}

	// Token: 0x0600580C RID: 22540 RVA: 0x001FE2B8 File Offset: 0x001FC4B8
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text = "";
		LaunchableRocketRegisterType launchableRocketRegisterType = this.craftRegisterType;
		if (launchableRocketRegisterType != LaunchableRocketRegisterType.Spacecraft)
		{
			if (launchableRocketRegisterType == LaunchableRocketRegisterType.Clustercraft)
			{
				if (status == ProcessCondition.Status.Ready)
				{
					text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.REACHABLE;
				}
				else
				{
					text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.NOTSELECTED;
				}
			}
		}
		else if (status == ProcessCondition.Status.Ready && this.GetSpacecraftDestination() != null)
		{
			text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.REACHABLE;
		}
		else if (this.GetSpacecraftDestination() != null)
		{
			text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.UNREACHABLE;
		}
		else
		{
			text = UI.STARMAP.DESTINATIONSELECTION_TOOLTIP.NOTSELECTED;
		}
		return text;
	}

	// Token: 0x0600580D RID: 22541 RVA: 0x001FE333 File Offset: 0x001FC533
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AB0 RID: 15024
	private LaunchableRocketRegisterType craftRegisterType;

	// Token: 0x04003AB1 RID: 15025
	private RocketModule module;
}
