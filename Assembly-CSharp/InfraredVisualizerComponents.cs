using System;
using UnityEngine;

// Token: 0x020005C6 RID: 1478
public class InfraredVisualizerComponents : KGameObjectComponentManager<InfraredVisualizerData>
{
	// Token: 0x0600220A RID: 8714 RVA: 0x000C42F7 File Offset: 0x000C24F7
	public HandleVector<int>.Handle Add(GameObject go)
	{
		return base.Add(go, new InfraredVisualizerData(go));
	}

	// Token: 0x0600220B RID: 8715 RVA: 0x000C4308 File Offset: 0x000C2508
	public void UpdateTemperature()
	{
		GridArea visibleArea = GridVisibleArea.GetVisibleArea();
		for (int i = 0; i < this.data.Count; i++)
		{
			KAnimControllerBase controller = this.data[i].controller;
			if (controller != null)
			{
				Vector3 position = controller.transform.GetPosition();
				if (visibleArea.Min <= position && position <= visibleArea.Max)
				{
					this.data[i].Update();
				}
			}
		}
	}

	// Token: 0x0600220C RID: 8716 RVA: 0x000C4398 File Offset: 0x000C2598
	public void ClearOverlayColour()
	{
		Color32 color = Color.black;
		for (int i = 0; i < this.data.Count; i++)
		{
			KAnimControllerBase controller = this.data[i].controller;
			if (controller != null)
			{
				controller.OverlayColour = color;
			}
		}
	}

	// Token: 0x0600220D RID: 8717 RVA: 0x000C43ED File Offset: 0x000C25ED
	public static void ClearOverlayColour(KBatchedAnimController controller)
	{
		if (controller != null)
		{
			controller.OverlayColour = Color.black;
		}
	}
}
