using System;
using System.Collections.Generic;
using Database;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000B6B RID: 2923
[SerializationConfig(MemberSerialization.OptIn)]
public class Spacecraft
{
	// Token: 0x06005725 RID: 22309 RVA: 0x001F94A1 File Offset: 0x001F76A1
	public Spacecraft(LaunchConditionManager launchConditions)
	{
		this.launchConditions = launchConditions;
	}

	// Token: 0x06005726 RID: 22310 RVA: 0x001F94D2 File Offset: 0x001F76D2
	public Spacecraft()
	{
	}

	// Token: 0x17000660 RID: 1632
	// (get) Token: 0x06005727 RID: 22311 RVA: 0x001F94FC File Offset: 0x001F76FC
	// (set) Token: 0x06005728 RID: 22312 RVA: 0x001F9509 File Offset: 0x001F7709
	public LaunchConditionManager launchConditions
	{
		get
		{
			return this.refLaunchConditions.Get();
		}
		set
		{
			this.refLaunchConditions.Set(value);
		}
	}

	// Token: 0x06005729 RID: 22313 RVA: 0x001F9517 File Offset: 0x001F7717
	public void SetRocketName(string newName)
	{
		this.rocketName = newName;
		this.UpdateNameOnRocketModules();
	}

	// Token: 0x0600572A RID: 22314 RVA: 0x001F9526 File Offset: 0x001F7726
	public string GetRocketName()
	{
		return this.rocketName;
	}

	// Token: 0x0600572B RID: 22315 RVA: 0x001F9530 File Offset: 0x001F7730
	public void UpdateNameOnRocketModules()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.launchConditions.GetComponent<AttachableBuilding>()))
		{
			RocketModule component = gameObject.GetComponent<RocketModule>();
			if (component != null)
			{
				component.SetParentRocketName(this.rocketName);
			}
		}
	}

	// Token: 0x0600572C RID: 22316 RVA: 0x001F95A0 File Offset: 0x001F77A0
	public bool HasInvalidID()
	{
		return this.id == -1;
	}

	// Token: 0x0600572D RID: 22317 RVA: 0x001F95AB File Offset: 0x001F77AB
	public void SetID(int id)
	{
		this.id = id;
	}

	// Token: 0x0600572E RID: 22318 RVA: 0x001F95B4 File Offset: 0x001F77B4
	public void SetState(Spacecraft.MissionState state)
	{
		this.state = state;
	}

	// Token: 0x0600572F RID: 22319 RVA: 0x001F95BD File Offset: 0x001F77BD
	public void BeginMission(SpaceDestination destination)
	{
		this.missionElapsed = 0f;
		this.missionDuration = (float)destination.OneBasedDistance * ROCKETRY.MISSION_DURATION_SCALE / this.GetPilotNavigationEfficiency();
		this.SetState(Spacecraft.MissionState.Launching);
	}

	// Token: 0x06005730 RID: 22320 RVA: 0x001F95EC File Offset: 0x001F77EC
	private float GetPilotNavigationEfficiency()
	{
		float num = 1f;
		if (!this.launchConditions.GetComponent<CommandModule>().robotPilotControlled)
		{
			List<MinionStorage.Info> storedMinionInfo = this.launchConditions.GetComponent<MinionStorage>().GetStoredMinionInfo();
			if (storedMinionInfo.Count < 1)
			{
				return 1f;
			}
			StoredMinionIdentity component = storedMinionInfo[0].serializedMinion.Get().GetComponent<StoredMinionIdentity>();
			string text = Db.Get().Attributes.SpaceNavigation.Id;
			foreach (KeyValuePair<string, bool> keyValuePair in component.MasteryBySkillID)
			{
				foreach (SkillPerk skillPerk in Db.Get().Skills.Get(keyValuePair.Key).perks)
				{
					if (Game.IsCorrectDlcActiveForCurrentSave(skillPerk))
					{
						SkillAttributePerk skillAttributePerk = skillPerk as SkillAttributePerk;
						if (skillAttributePerk != null && skillAttributePerk.modifier.AttributeId == text)
						{
							num += skillAttributePerk.modifier.Value;
						}
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06005731 RID: 22321 RVA: 0x001F9730 File Offset: 0x001F7930
	public void ForceComplete()
	{
		this.missionElapsed = this.missionDuration;
	}

	// Token: 0x06005732 RID: 22322 RVA: 0x001F9740 File Offset: 0x001F7940
	public void ProgressMission(float deltaTime)
	{
		if (this.state == Spacecraft.MissionState.Underway)
		{
			this.missionElapsed += deltaTime;
			if (this.controlStationBuffTimeRemaining > 0f)
			{
				this.missionElapsed += deltaTime * 0.20000005f;
				this.controlStationBuffTimeRemaining -= deltaTime;
			}
			else
			{
				this.controlStationBuffTimeRemaining = 0f;
			}
			if (this.missionElapsed > this.missionDuration)
			{
				this.CompleteMission();
			}
		}
	}

	// Token: 0x06005733 RID: 22323 RVA: 0x001F97B4 File Offset: 0x001F79B4
	public float GetTimeLeft()
	{
		return this.missionDuration - this.missionElapsed;
	}

	// Token: 0x06005734 RID: 22324 RVA: 0x001F97C3 File Offset: 0x001F79C3
	public float GetDuration()
	{
		return this.missionDuration;
	}

	// Token: 0x06005735 RID: 22325 RVA: 0x001F97CB File Offset: 0x001F79CB
	public void CompleteMission()
	{
		SpacecraftManager.instance.PushReadyToLandNotification(this);
		this.SetState(Spacecraft.MissionState.WaitingToLand);
		this.Land();
	}

	// Token: 0x06005736 RID: 22326 RVA: 0x001F97E8 File Offset: 0x001F79E8
	private void Land()
	{
		this.launchConditions.Trigger(-1165815793, SpacecraftManager.instance.GetSpacecraftDestination(this.id));
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.launchConditions.GetComponent<AttachableBuilding>()))
		{
			if (gameObject != this.launchConditions.gameObject)
			{
				gameObject.Trigger(-1165815793, SpacecraftManager.instance.GetSpacecraftDestination(this.id));
			}
		}
	}

	// Token: 0x06005737 RID: 22327 RVA: 0x001F988C File Offset: 0x001F7A8C
	public void TemporallyTear()
	{
		SpacecraftManager.instance.hasVisitedWormHole = true;
		LaunchConditionManager launchConditions = this.launchConditions;
		for (int i = launchConditions.rocketModules.Count - 1; i >= 0; i--)
		{
			Storage component = launchConditions.rocketModules[i].GetComponent<Storage>();
			if (component != null)
			{
				component.ConsumeAllIgnoringDisease();
			}
			MinionStorage component2 = launchConditions.rocketModules[i].GetComponent<MinionStorage>();
			if (component2 != null)
			{
				List<MinionStorage.Info> storedMinionInfo = component2.GetStoredMinionInfo();
				for (int j = storedMinionInfo.Count - 1; j >= 0; j--)
				{
					component2.DeleteStoredMinion(storedMinionInfo[j].id);
				}
			}
			Util.KDestroyGameObject(launchConditions.rocketModules[i].gameObject);
		}
	}

	// Token: 0x06005738 RID: 22328 RVA: 0x001F994F File Offset: 0x001F7B4F
	public void GenerateName()
	{
		this.SetRocketName(GameUtil.GenerateRandomRocketName());
	}

	// Token: 0x04003A4E RID: 14926
	[Serialize]
	public int id = -1;

	// Token: 0x04003A4F RID: 14927
	[Serialize]
	public string rocketName = UI.STARMAP.DEFAULT_NAME;

	// Token: 0x04003A50 RID: 14928
	[Serialize]
	public float controlStationBuffTimeRemaining;

	// Token: 0x04003A51 RID: 14929
	[Serialize]
	public Ref<LaunchConditionManager> refLaunchConditions = new Ref<LaunchConditionManager>();

	// Token: 0x04003A52 RID: 14930
	[Serialize]
	public Spacecraft.MissionState state;

	// Token: 0x04003A53 RID: 14931
	[Serialize]
	private float missionElapsed;

	// Token: 0x04003A54 RID: 14932
	[Serialize]
	private float missionDuration;

	// Token: 0x02001C99 RID: 7321
	public enum MissionState
	{
		// Token: 0x040086CC RID: 34508
		Grounded,
		// Token: 0x040086CD RID: 34509
		Launching,
		// Token: 0x040086CE RID: 34510
		Underway,
		// Token: 0x040086CF RID: 34511
		WaitingToLand,
		// Token: 0x040086D0 RID: 34512
		Landing,
		// Token: 0x040086D1 RID: 34513
		Destroyed
	}
}
