using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000E03 RID: 3587
public class LaunchPadSideScreen : SideScreenContent
{
	// Token: 0x06007128 RID: 28968 RVA: 0x002B08F9 File Offset: 0x002AEAF9
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.startNewRocketbutton.onClick += this.ClickStartNewRocket;
		this.devAutoRocketButton.onClick += this.ClickAutoRocket;
	}

	// Token: 0x06007129 RID: 28969 RVA: 0x002B092F File Offset: 0x002AEB2F
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show)
		{
			DetailsScreen.Instance.ClearSecondarySideScreen();
		}
	}

	// Token: 0x0600712A RID: 28970 RVA: 0x002B0945 File Offset: 0x002AEB45
	public override int GetSideScreenSortOrder()
	{
		return 100;
	}

	// Token: 0x0600712B RID: 28971 RVA: 0x002B0949 File Offset: 0x002AEB49
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LaunchPad>() != null;
	}

	// Token: 0x0600712C RID: 28972 RVA: 0x002B0958 File Offset: 0x002AEB58
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		if (this.refreshEventHandle != -1)
		{
			this.selectedPad.Unsubscribe(this.refreshEventHandle);
		}
		this.selectedPad = new_target.GetComponent<LaunchPad>();
		if (this.selectedPad == null)
		{
			global::Debug.LogError("The gameObject received does not contain a LaunchPad component");
			return;
		}
		this.refreshEventHandle = this.selectedPad.Subscribe(-887025858, new Action<object>(this.RefreshWaitingToLandList));
		this.RefreshRocketButton();
		this.RefreshWaitingToLandList(null);
	}

	// Token: 0x0600712D RID: 28973 RVA: 0x002B09E8 File Offset: 0x002AEBE8
	private void RefreshWaitingToLandList(object data = null)
	{
		for (int i = this.waitingToLandRows.Count - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.waitingToLandRows[i]);
		}
		this.waitingToLandRows.Clear();
		this.nothingWaitingRow.SetActive(true);
		AxialI myWorldLocation = this.selectedPad.GetMyWorldLocation();
		foreach (ClusterGridEntity clusterGridEntity in ClusterGrid.Instance.GetEntitiesInRange(myWorldLocation, 1))
		{
			Clustercraft craft = clusterGridEntity as Clustercraft;
			if (!(craft == null) && craft.Status == Clustercraft.CraftStatus.InFlight && (!craft.IsFlightInProgress() || !(craft.Destination != myWorldLocation)))
			{
				GameObject gameObject = Util.KInstantiateUI(this.landableRocketRowPrefab, this.landableRowContainer, true);
				gameObject.GetComponentInChildren<LocText>().text = craft.Name;
				this.waitingToLandRows.Add(gameObject);
				KButton componentInChildren = gameObject.GetComponentInChildren<KButton>();
				componentInChildren.GetComponentInChildren<LocText>().SetText((craft.ModuleInterface.GetClusterDestinationSelector().GetDestinationPad() == this.selectedPad) ? UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.CANCEL_LAND_BUTTON : UI.UISIDESCREENS.LAUNCHPADSIDESCREEN.LAND_BUTTON);
				string text;
				componentInChildren.isInteractable = craft.CanLandAtPad(this.selectedPad, out text) != Clustercraft.PadLandingStatus.CanNeverLand;
				if (!componentInChildren.isInteractable)
				{
					componentInChildren.GetComponent<ToolTip>().SetSimpleTooltip(text);
				}
				else
				{
					componentInChildren.GetComponent<ToolTip>().ClearMultiStringTooltip();
				}
				componentInChildren.onClick += delegate
				{
					if (craft.ModuleInterface.GetClusterDestinationSelector().GetDestinationPad() == this.selectedPad)
					{
						craft.GetComponent<ClusterDestinationSelector>().SetDestination(craft.Location);
					}
					else
					{
						craft.LandAtPad(this.selectedPad);
					}
					this.RefreshWaitingToLandList(null);
				};
				this.nothingWaitingRow.SetActive(false);
			}
		}
	}

	// Token: 0x0600712E RID: 28974 RVA: 0x002B0BE8 File Offset: 0x002AEDE8
	private void ClickStartNewRocket()
	{
		((SelectModuleSideScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.changeModuleSideScreen, UI.UISIDESCREENS.ROCKETMODULESIDESCREEN.CHANGEMODULEPANEL)).SetLaunchPad(this.selectedPad);
	}

	// Token: 0x0600712F RID: 28975 RVA: 0x002B0C14 File Offset: 0x002AEE14
	private void RefreshRocketButton()
	{
		bool isOperational = this.selectedPad.GetComponent<Operational>().IsOperational;
		this.startNewRocketbutton.isInteractable = this.selectedPad.LandedRocket == null && isOperational;
		if (!isOperational)
		{
			this.startNewRocketbutton.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_PAD_DISABLED);
		}
		else
		{
			this.startNewRocketbutton.GetComponent<ToolTip>().ClearMultiStringTooltip();
		}
		this.devAutoRocketButton.isInteractable = this.selectedPad.LandedRocket == null;
		this.devAutoRocketButton.gameObject.SetActive(DebugHandler.InstantBuildMode);
	}

	// Token: 0x06007130 RID: 28976 RVA: 0x002B0CB0 File Offset: 0x002AEEB0
	private void ClickAutoRocket()
	{
		AutoRocketUtility.StartAutoRocket(this.selectedPad);
	}

	// Token: 0x04004DDC RID: 19932
	public GameObject content;

	// Token: 0x04004DDD RID: 19933
	private LaunchPad selectedPad;

	// Token: 0x04004DDE RID: 19934
	public LocText DescriptionText;

	// Token: 0x04004DDF RID: 19935
	public GameObject landableRocketRowPrefab;

	// Token: 0x04004DE0 RID: 19936
	public GameObject newRocketPanel;

	// Token: 0x04004DE1 RID: 19937
	public KButton startNewRocketbutton;

	// Token: 0x04004DE2 RID: 19938
	public KButton devAutoRocketButton;

	// Token: 0x04004DE3 RID: 19939
	public GameObject landableRowContainer;

	// Token: 0x04004DE4 RID: 19940
	public GameObject nothingWaitingRow;

	// Token: 0x04004DE5 RID: 19941
	public KScreen changeModuleSideScreen;

	// Token: 0x04004DE6 RID: 19942
	private int refreshEventHandle = -1;

	// Token: 0x04004DE7 RID: 19943
	public List<GameObject> waitingToLandRows = new List<GameObject>();
}
