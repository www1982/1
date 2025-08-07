using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000DCC RID: 3532
public class AccessControlSideScreenRow : AccessControlSideScreenDoor
{
	// Token: 0x06006F62 RID: 28514 RVA: 0x002A6865 File Offset: 0x002A4A65
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.defaultButton.onValueChanged += this.OnDefaultButtonChanged;
	}

	// Token: 0x06006F63 RID: 28515 RVA: 0x002A6884 File Offset: 0x002A4A84
	private void OnDefaultButtonChanged(bool state)
	{
		this.UpdateButtonStates(!state);
		if (this.defaultClickedCallback != null)
		{
			this.defaultClickedCallback(this.targetIdentity, !state);
		}
	}

	// Token: 0x06006F64 RID: 28516 RVA: 0x002A68B0 File Offset: 0x002A4AB0
	protected override void UpdateButtonStates(bool isDefault)
	{
		base.UpdateButtonStates(isDefault);
		this.defaultButton.GetComponent<ToolTip>().SetSimpleTooltip(isDefault ? UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.SET_TO_CUSTOM : UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.SET_TO_DEFAULT);
		this.defaultControls.SetActive(isDefault);
		this.customControls.SetActive(!isDefault);
	}

	// Token: 0x06006F65 RID: 28517 RVA: 0x002A6904 File Offset: 0x002A4B04
	public void SetMinionContent(MinionAssignablesProxy identity, AccessControl.Permission permission, bool isDefault, Action<MinionAssignablesProxy, AccessControl.Permission> onPermissionChange, Action<MinionAssignablesProxy, bool> onDefaultClick)
	{
		base.SetContent(permission, onPermissionChange);
		if (identity == null)
		{
			global::Debug.LogError("Invalid data received.");
			return;
		}
		if (this.portraitInstance == null)
		{
			this.portraitInstance = Util.KInstantiateUI<CrewPortrait>(this.crewPortraitPrefab.gameObject, this.defaultButton.gameObject, false);
			this.portraitInstance.SetAlpha(1f);
		}
		this.targetIdentity = identity;
		this.portraitInstance.SetIdentityObject(identity, false);
		this.portraitInstance.SetSubTitle(isDefault ? UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.USING_DEFAULT : UI.UISIDESCREENS.ACCESS_CONTROL_SIDE_SCREEN.USING_CUSTOM);
		this.defaultClickedCallback = null;
		this.defaultButton.isOn = !isDefault;
		this.defaultClickedCallback = onDefaultClick;
	}

	// Token: 0x04004C9E RID: 19614
	[SerializeField]
	private CrewPortrait crewPortraitPrefab;

	// Token: 0x04004C9F RID: 19615
	private CrewPortrait portraitInstance;

	// Token: 0x04004CA0 RID: 19616
	public KToggle defaultButton;

	// Token: 0x04004CA1 RID: 19617
	public GameObject defaultControls;

	// Token: 0x04004CA2 RID: 19618
	public GameObject customControls;

	// Token: 0x04004CA3 RID: 19619
	private Action<MinionAssignablesProxy, bool> defaultClickedCallback;
}
