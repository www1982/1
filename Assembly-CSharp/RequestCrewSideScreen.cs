using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000E25 RID: 3621
public class RequestCrewSideScreen : SideScreenContent
{
	// Token: 0x06007282 RID: 29314 RVA: 0x002B8718 File Offset: 0x002B6918
	protected override void OnSpawn()
	{
		this.changeCrewButton.onClick += this.OnChangeCrewButtonPressed;
		this.crewReleaseButton.onClick += this.CrewRelease;
		this.crewRequestButton.onClick += this.CrewRequest;
		this.toggleMap.Add(this.crewReleaseButton, PassengerRocketModule.RequestCrewState.Release);
		this.toggleMap.Add(this.crewRequestButton, PassengerRocketModule.RequestCrewState.Request);
		this.Refresh();
	}

	// Token: 0x06007283 RID: 29315 RVA: 0x002B8794 File Offset: 0x002B6994
	public override int GetSideScreenSortOrder()
	{
		return 100;
	}

	// Token: 0x06007284 RID: 29316 RVA: 0x002B8798 File Offset: 0x002B6998
	public override bool IsValidForTarget(GameObject target)
	{
		PassengerRocketModule component = target.GetComponent<PassengerRocketModule>();
		RocketControlStation component2 = target.GetComponent<RocketControlStation>();
		if (component != null)
		{
			return component.GetMyWorld() != null;
		}
		if (component2 != null)
		{
			RocketControlStation.StatesInstance smi = component2.GetSMI<RocketControlStation.StatesInstance>();
			return !smi.sm.IsInFlight(smi) && !smi.sm.IsLaunching(smi);
		}
		return false;
	}

	// Token: 0x06007285 RID: 29317 RVA: 0x002B87FA File Offset: 0x002B69FA
	public override void SetTarget(GameObject target)
	{
		if (target.GetComponent<RocketControlStation>() != null)
		{
			this.rocketModule = target.GetMyWorld().GetComponent<Clustercraft>().ModuleInterface.GetPassengerModule();
		}
		else
		{
			this.rocketModule = target.GetComponent<PassengerRocketModule>();
		}
		this.Refresh();
	}

	// Token: 0x06007286 RID: 29318 RVA: 0x002B8839 File Offset: 0x002B6A39
	private void Refresh()
	{
		this.RefreshRequestButtons();
	}

	// Token: 0x06007287 RID: 29319 RVA: 0x002B8841 File Offset: 0x002B6A41
	private void CrewRelease()
	{
		this.rocketModule.RequestCrewBoard(PassengerRocketModule.RequestCrewState.Release);
		this.RefreshRequestButtons();
	}

	// Token: 0x06007288 RID: 29320 RVA: 0x002B8855 File Offset: 0x002B6A55
	private void CrewRequest()
	{
		this.rocketModule.RequestCrewBoard(PassengerRocketModule.RequestCrewState.Request);
		this.RefreshRequestButtons();
	}

	// Token: 0x06007289 RID: 29321 RVA: 0x002B886C File Offset: 0x002B6A6C
	private void RefreshRequestButtons()
	{
		foreach (KeyValuePair<KToggle, PassengerRocketModule.RequestCrewState> keyValuePair in this.toggleMap)
		{
			this.RefreshRequestButton(keyValuePair.Key);
		}
	}

	// Token: 0x0600728A RID: 29322 RVA: 0x002B88C8 File Offset: 0x002B6AC8
	private void RefreshRequestButton(KToggle button)
	{
		ImageToggleState[] array;
		if (this.toggleMap[button] == this.rocketModule.PassengersRequested)
		{
			button.isOn = true;
			array = button.GetComponentsInChildren<ImageToggleState>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive();
			}
			button.GetComponent<ImageToggleStateThrobber>().enabled = false;
			return;
		}
		button.isOn = false;
		array = button.GetComponentsInChildren<ImageToggleState>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetInactive();
		}
		button.GetComponent<ImageToggleStateThrobber>().enabled = false;
	}

	// Token: 0x0600728B RID: 29323 RVA: 0x002B8950 File Offset: 0x002B6B50
	private void OnChangeCrewButtonPressed()
	{
		if (this.activeChangeCrewSideScreen == null)
		{
			this.activeChangeCrewSideScreen = (AssignmentGroupControllerSideScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.changeCrewSideScreenPrefab, UI.UISIDESCREENS.ASSIGNMENTGROUPCONTROLLER.TITLE);
			this.activeChangeCrewSideScreen.SetTarget(this.rocketModule.gameObject);
			return;
		}
		DetailsScreen.Instance.ClearSecondarySideScreen();
		this.activeChangeCrewSideScreen = null;
	}

	// Token: 0x0600728C RID: 29324 RVA: 0x002B89B8 File Offset: 0x002B6BB8
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			DetailsScreen.Instance.ClearSecondarySideScreen();
			this.activeChangeCrewSideScreen = null;
		}
	}

	// Token: 0x04004ED9 RID: 20185
	private PassengerRocketModule rocketModule;

	// Token: 0x04004EDA RID: 20186
	public KToggle crewReleaseButton;

	// Token: 0x04004EDB RID: 20187
	public KToggle crewRequestButton;

	// Token: 0x04004EDC RID: 20188
	private Dictionary<KToggle, PassengerRocketModule.RequestCrewState> toggleMap = new Dictionary<KToggle, PassengerRocketModule.RequestCrewState>();

	// Token: 0x04004EDD RID: 20189
	public KButton changeCrewButton;

	// Token: 0x04004EDE RID: 20190
	public KScreen changeCrewSideScreenPrefab;

	// Token: 0x04004EDF RID: 20191
	private AssignmentGroupControllerSideScreen activeChangeCrewSideScreen;
}
