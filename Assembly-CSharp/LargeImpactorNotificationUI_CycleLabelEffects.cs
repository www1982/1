using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B4A RID: 2890
public class LargeImpactorNotificationUI_CycleLabelEffects : MonoBehaviour
{
	// Token: 0x06005608 RID: 22024 RVA: 0x001F37FA File Offset: 0x001F19FA
	public void InitializeCycleLabelFocusMonitor()
	{
		this.AbortCycleLabelFocusMonitor();
		this.cycleLabelFocusCoroutine = base.StartCoroutine(this.CycleLabelFocusMonitor());
	}

	// Token: 0x06005609 RID: 22025 RVA: 0x001F3814 File Offset: 0x001F1A14
	public void AbortCycleLabelFocusMonitor()
	{
		if (this.cycleLabelFocusCoroutine != null)
		{
			base.StopCoroutine(this.cycleLabelFocusCoroutine);
			this.cycleLabelFocusCoroutine = null;
		}
	}

	// Token: 0x0600560A RID: 22026 RVA: 0x001F3831 File Offset: 0x001F1A31
	private IEnumerator CycleLabelFocusMonitor()
	{
		float previousVisibleValue = -1f;
		float visibleValue = 0f;
		for (;;)
		{
			visibleValue = Mathf.Clamp(visibleValue + Time.unscaledDeltaTime / (this.notificationTooltipComponent.isHovering ? this.cycleFocusSpeed : this.cycleUnfocusSpeed) * (float)(this.notificationTooltipComponent.isHovering ? 1 : (-1)), 0f, 1f);
			if (visibleValue != previousVisibleValue)
			{
				previousVisibleValue = visibleValue;
				this.cyclesLabelBackground.Opacity(visibleValue);
				this.numberOfCyclesLabel.Opacity(visibleValue);
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x04003971 RID: 14705
	public ToolTip notificationTooltipComponent;

	// Token: 0x04003972 RID: 14706
	public Image cyclesLabelBackground;

	// Token: 0x04003973 RID: 14707
	public LocText numberOfCyclesLabel;

	// Token: 0x04003974 RID: 14708
	private Coroutine cycleLabelFocusCoroutine;

	// Token: 0x04003975 RID: 14709
	private float cycleFocusSpeed = 0.2f;

	// Token: 0x04003976 RID: 14710
	private float cycleUnfocusSpeed = 0.4f;
}
