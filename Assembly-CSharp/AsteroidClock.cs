using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C67 RID: 3175
public class AsteroidClock : MonoBehaviour
{
	// Token: 0x060060DF RID: 24799 RVA: 0x0023CFC4 File Offset: 0x0023B1C4
	private void Awake()
	{
		this.UpdateOverlay();
	}

	// Token: 0x060060E0 RID: 24800 RVA: 0x0023CFCC File Offset: 0x0023B1CC
	private void Start()
	{
	}

	// Token: 0x060060E1 RID: 24801 RVA: 0x0023CFCE File Offset: 0x0023B1CE
	private void Update()
	{
		if (GameClock.Instance != null)
		{
			this.rotationTransform.rotation = Quaternion.Euler(0f, 0f, 360f * -GameClock.Instance.GetCurrentCycleAsPercentage());
		}
	}

	// Token: 0x060060E2 RID: 24802 RVA: 0x0023D008 File Offset: 0x0023B208
	private void UpdateOverlay()
	{
		float num = 0.125f;
		this.NightOverlay.fillAmount = num;
	}

	// Token: 0x04004174 RID: 16756
	public Transform rotationTransform;

	// Token: 0x04004175 RID: 16757
	public Image NightOverlay;
}
