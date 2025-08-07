using System;
using UnityEngine;

// Token: 0x02000E28 RID: 3624
public class RocketRestrictionSideScreen : SideScreenContent
{
	// Token: 0x060072A9 RID: 29353 RVA: 0x002B95FB File Offset: 0x002B77FB
	protected override void OnSpawn()
	{
		this.unrestrictedButton.onClick += this.ClickNone;
		this.spaceRestrictedButton.onClick += this.ClickSpace;
	}

	// Token: 0x060072AA RID: 29354 RVA: 0x002B962B File Offset: 0x002B782B
	public override int GetSideScreenSortOrder()
	{
		return 0;
	}

	// Token: 0x060072AB RID: 29355 RVA: 0x002B962E File Offset: 0x002B782E
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetSMI<RocketControlStation.StatesInstance>() != null;
	}

	// Token: 0x060072AC RID: 29356 RVA: 0x002B963C File Offset: 0x002B783C
	public override void SetTarget(GameObject new_target)
	{
		if (this.controlStation != null || this.controlStationLogicSubHandle != -1)
		{
			this.ClearTarget();
		}
		this.controlStation = new_target.GetComponent<RocketControlStation>();
		this.controlStationLogicSubHandle = this.controlStation.Subscribe(1861523068, new Action<object>(this.UpdateButtonStates));
		this.UpdateButtonStates(null);
	}

	// Token: 0x060072AD RID: 29357 RVA: 0x002B969B File Offset: 0x002B789B
	public override void ClearTarget()
	{
		if (this.controlStationLogicSubHandle != -1 && this.controlStation != null)
		{
			this.controlStation.Unsubscribe(this.controlStationLogicSubHandle);
			this.controlStationLogicSubHandle = -1;
		}
		this.controlStation = null;
	}

	// Token: 0x060072AE RID: 29358 RVA: 0x002B96D4 File Offset: 0x002B78D4
	private void UpdateButtonStates(object data = null)
	{
		bool flag = this.controlStation.IsLogicInputConnected();
		if (!flag)
		{
			this.unrestrictedButton.isOn = !this.controlStation.RestrictWhenGrounded;
			this.spaceRestrictedButton.isOn = this.controlStation.RestrictWhenGrounded;
		}
		this.unrestrictedButton.gameObject.SetActive(!flag);
		this.spaceRestrictedButton.gameObject.SetActive(!flag);
		this.automationControlled.gameObject.SetActive(flag);
	}

	// Token: 0x060072AF RID: 29359 RVA: 0x002B9758 File Offset: 0x002B7958
	private void ClickNone()
	{
		this.controlStation.RestrictWhenGrounded = false;
		this.UpdateButtonStates(null);
	}

	// Token: 0x060072B0 RID: 29360 RVA: 0x002B976D File Offset: 0x002B796D
	private void ClickSpace()
	{
		this.controlStation.RestrictWhenGrounded = true;
		this.UpdateButtonStates(null);
	}

	// Token: 0x04004EF4 RID: 20212
	private RocketControlStation controlStation;

	// Token: 0x04004EF5 RID: 20213
	[Header("Buttons")]
	public KToggle unrestrictedButton;

	// Token: 0x04004EF6 RID: 20214
	public KToggle spaceRestrictedButton;

	// Token: 0x04004EF7 RID: 20215
	public GameObject automationControlled;

	// Token: 0x04004EF8 RID: 20216
	private int controlStationLogicSubHandle = -1;
}
