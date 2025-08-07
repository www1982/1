using System;
using UnityEngine;

// Token: 0x02000E8A RID: 3722
public class ScreenResolutionMonitor : MonoBehaviour
{
	// Token: 0x06007696 RID: 30358 RVA: 0x002D619C File Offset: 0x002D439C
	private void Awake()
	{
		this.previousSize = new Vector2((float)Screen.width, (float)Screen.height);
	}

	// Token: 0x06007697 RID: 30359 RVA: 0x002D61B8 File Offset: 0x002D43B8
	private void Update()
	{
		if ((this.previousSize.x != (float)Screen.width || this.previousSize.y != (float)Screen.height) && Game.Instance != null)
		{
			Game.Instance.Trigger(445618876, null);
			this.previousSize.x = (float)Screen.width;
			this.previousSize.y = (float)Screen.height;
		}
		this.UpdateShouldUseGamepadUIMode();
	}

	// Token: 0x06007698 RID: 30360 RVA: 0x002D6230 File Offset: 0x002D4430
	public static bool UsingGamepadUIMode()
	{
		return ScreenResolutionMonitor.previousGamepadUIMode;
	}

	// Token: 0x06007699 RID: 30361 RVA: 0x002D6238 File Offset: 0x002D4438
	private void UpdateShouldUseGamepadUIMode()
	{
		bool flag = (Screen.dpi > 130f && Screen.height < 900) || KInputManager.currentControllerIsGamepad;
		if (flag != ScreenResolutionMonitor.previousGamepadUIMode)
		{
			ScreenResolutionMonitor.previousGamepadUIMode = flag;
			if (Game.Instance == null)
			{
				return;
			}
			Game.Instance.Trigger(-442024484, null);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound(flag ? "ControllerType_ToggleOn" : "ControllerType_ToggleOff", false));
		}
	}

	// Token: 0x0400528D RID: 21133
	[SerializeField]
	private Vector2 previousSize;

	// Token: 0x0400528E RID: 21134
	private static bool previousGamepadUIMode;

	// Token: 0x0400528F RID: 21135
	private const float HIGH_DPI = 130f;
}
