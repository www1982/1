using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B8D RID: 2957
public class ConditionRobotPilotReady : ProcessCondition
{
	// Token: 0x0600585E RID: 22622 RVA: 0x001FF956 File Offset: 0x001FDB56
	public ConditionRobotPilotReady(RoboPilotModule module)
	{
		this.module = module;
		this.craftRegisterType = module.GetComponent<ILaunchableRocket>().registerType;
		if (this.craftRegisterType == LaunchableRocketRegisterType.Clustercraft)
		{
			this.craftInterface = module.GetComponent<RocketModuleCluster>().CraftInterface;
		}
	}

	// Token: 0x0600585F RID: 22623 RVA: 0x001FF990 File Offset: 0x001FDB90
	public override ProcessCondition.Status EvaluateCondition()
	{
		ProcessCondition.Status status = ProcessCondition.Status.Failure;
		LaunchableRocketRegisterType launchableRocketRegisterType = this.craftRegisterType;
		if (launchableRocketRegisterType != LaunchableRocketRegisterType.Spacecraft)
		{
			if (launchableRocketRegisterType == LaunchableRocketRegisterType.Clustercraft && this.HasDestination())
			{
				global::UnityEngine.Object component = this.craftInterface.GetComponent<Clustercraft>();
				ClusterTraveler component2 = this.craftInterface.GetComponent<ClusterTraveler>();
				if (component == null || component2 == null || component2.CurrentPath == null)
				{
					return ProcessCondition.Status.Failure;
				}
				int num = component2.RemainingTravelNodes();
				bool flag = this.module.HasResourcesToMove(num * 2);
				bool flag2 = this.module.HasResourcesToMove(num);
				if (flag)
				{
					status = ProcessCondition.Status.Ready;
				}
				else if (flag2 || this.RocketHasDupeControlStation())
				{
					status = ProcessCondition.Status.Warning;
				}
			}
		}
		else
		{
			int id = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.module.GetComponent<LaunchConditionManager>()).id;
			SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(id);
			if (spacecraftDestination == null)
			{
				status = ProcessCondition.Status.Failure;
			}
			else if (this.module.HasResourcesToMove(spacecraftDestination.OneBasedDistance * 2))
			{
				status = ProcessCondition.Status.Ready;
			}
			else
			{
				status = ProcessCondition.Status.Failure;
			}
		}
		return status;
	}

	// Token: 0x06005860 RID: 22624 RVA: 0x001FFA78 File Offset: 0x001FDC78
	private bool HasDestination()
	{
		if (this.craftRegisterType == LaunchableRocketRegisterType.Clustercraft)
		{
			return !this.module.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<RocketClusterDestinationSelector>().IsAtDestination();
		}
		if (this.craftRegisterType == LaunchableRocketRegisterType.Spacecraft)
		{
			int id = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.module.GetComponent<LaunchConditionManager>()).id;
			return SpacecraftManager.instance.GetSpacecraftDestination(id) != null;
		}
		return false;
	}

	// Token: 0x06005861 RID: 22625 RVA: 0x001FFAE4 File Offset: 0x001FDCE4
	private bool RocketHasDupeControlStation()
	{
		if (this.craftInterface != null)
		{
			PassengerRocketModule passengerModule = this.craftInterface.GetPassengerModule();
			if (passengerModule != null)
			{
				return passengerModule.CheckPilotBoarded();
			}
		}
		return false;
	}

	// Token: 0x06005862 RID: 22626 RVA: 0x001FFB1C File Offset: 0x001FDD1C
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.STATUS.READY;
		}
		if (status != ProcessCondition.Status.Warning)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.STATUS.FAILURE;
		}
		if (this.RocketHasDupeControlStation())
		{
			return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.STATUS.WARNING_NO_DATA_BANKS_HUMAN_PILOT;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.STATUS.WARNING;
	}

	// Token: 0x06005863 RID: 22627 RVA: 0x001FFB5C File Offset: 0x001FDD5C
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		LaunchableRocketRegisterType launchableRocketRegisterType = this.craftRegisterType;
		if (launchableRocketRegisterType != LaunchableRocketRegisterType.Spacecraft)
		{
			if (launchableRocketRegisterType == LaunchableRocketRegisterType.Clustercraft)
			{
				ClusterTraveler component = this.craftInterface.GetComponent<ClusterTraveler>();
				if (status == ProcessCondition.Status.Ready)
				{
					if (this.craftInterface.GetClusterDestinationSelector().IsAtDestination())
					{
						return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.READY_NO_DESTINATION, this.module.GetDataBanksStored());
					}
					int num = component.RemainingTravelNodes() * 2 * this.module.dataBankConsumption;
					return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.READY, this.module.GetDataBanksStored(), num);
				}
				else if (status == ProcessCondition.Status.Warning)
				{
					if (this.RocketHasDupeControlStation())
					{
						return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.WARNING_NO_DATA_BANKS_HUMAN_PILOT;
					}
					if (component == null || component.CurrentPath == null)
					{
						return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.FAILURE_NO_DESTINATION;
					}
					int num2 = component.RemainingTravelNodes() * 2 * this.module.dataBankConsumption;
					return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.WARNING, this.module.GetDataBanksStored(), num2);
				}
				else
				{
					if (this.HasDestination() && !(component == null) && component.CurrentPath != null)
					{
						int num3 = component.RemainingTravelNodes();
						return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.FAILURE, num3 * this.module.dataBankConsumption, this.module.GetDataBanksStored());
					}
					if (this.module.IsFull())
					{
						return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.READY_NO_DESTINATION, this.module.GetDataBanksStored());
					}
					return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.FAILURE_NO_DESTINATION;
				}
			}
		}
		else
		{
			int id = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.module.GetComponent<LaunchConditionManager>()).id;
			SpaceDestination spacecraftDestination = SpacecraftManager.instance.GetSpacecraftDestination(id);
			if (status == ProcessCondition.Status.Ready)
			{
				int num4 = spacecraftDestination.OneBasedDistance * 2;
				return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.READY, this.module.GetDataBanksStored(), num4);
			}
			if (status == ProcessCondition.Status.Warning)
			{
				if (spacecraftDestination != null)
				{
					int num5 = spacecraftDestination.OneBasedDistance * 2;
					return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.WARNING, this.module.GetDataBanksStored(), num5);
				}
			}
			else
			{
				if (spacecraftDestination != null)
				{
					return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.FAILURE, spacecraftDestination.OneBasedDistance * 2, this.module.GetDataBanksStored());
				}
				if (this.module.IsFull())
				{
					return string.Format(UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.READY_NO_DESTINATION, this.module.GetDataBanksStored());
				}
				return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.FAILURE_NO_DESTINATION;
			}
		}
		DebugUtil.DevAssert(false, "Rocket type " + this.craftRegisterType.ToString() + " does not have a status tooltip for " + status.ToString(), null);
		return UI.STARMAP.LAUNCHCHECKLIST.ROBOT_PILOT_DATA_REQUIREMENTS.TOOLTIP.FAILURE_NO_DESTINATION;
	}

	// Token: 0x06005864 RID: 22628 RVA: 0x001FFE39 File Offset: 0x001FE039
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AC9 RID: 15049
	private LaunchableRocketRegisterType craftRegisterType;

	// Token: 0x04003ACA RID: 15050
	private RoboPilotModule module;

	// Token: 0x04003ACB RID: 15051
	private CraftModuleInterface craftInterface;
}
