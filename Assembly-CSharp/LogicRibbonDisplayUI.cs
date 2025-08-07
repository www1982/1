using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D24 RID: 3364
[AddComponentMenu("KMonoBehaviour/scripts/LogicRibbonDisplayUI")]
public class LogicRibbonDisplayUI : KMonoBehaviour
{
	// Token: 0x060067C6 RID: 26566 RVA: 0x00271B14 File Offset: 0x0026FD14
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.colourOn = GlobalAssets.Instance.colorSet.logicOn;
		this.colourOff = GlobalAssets.Instance.colorSet.logicOff;
		this.colourOn.a = (this.colourOff.a = byte.MaxValue);
		this.wire1.raycastTarget = false;
		this.wire2.raycastTarget = false;
		this.wire3.raycastTarget = false;
		this.wire4.raycastTarget = false;
	}

	// Token: 0x060067C7 RID: 26567 RVA: 0x00271BA0 File Offset: 0x0026FDA0
	public void SetContent(LogicCircuitNetwork network)
	{
		Color32 color = this.colourDisconnected;
		List<Color32> list = new List<Color32>();
		for (int i = 0; i < this.bitDepth; i++)
		{
			list.Add((network == null) ? color : (network.IsBitActive(i) ? this.colourOn : this.colourOff));
		}
		if (this.wire1.color != list[0])
		{
			this.wire1.color = list[0];
		}
		if (this.wire2.color != list[1])
		{
			this.wire2.color = list[1];
		}
		if (this.wire3.color != list[2])
		{
			this.wire3.color = list[2];
		}
		if (this.wire4.color != list[3])
		{
			this.wire4.color = list[3];
		}
	}

	// Token: 0x04004727 RID: 18215
	[SerializeField]
	private Image wire1;

	// Token: 0x04004728 RID: 18216
	[SerializeField]
	private Image wire2;

	// Token: 0x04004729 RID: 18217
	[SerializeField]
	private Image wire3;

	// Token: 0x0400472A RID: 18218
	[SerializeField]
	private Image wire4;

	// Token: 0x0400472B RID: 18219
	[SerializeField]
	private LogicModeUI uiAsset;

	// Token: 0x0400472C RID: 18220
	private Color32 colourOn;

	// Token: 0x0400472D RID: 18221
	private Color32 colourOff;

	// Token: 0x0400472E RID: 18222
	private Color32 colourDisconnected = new Color(255f, 255f, 255f, 255f);

	// Token: 0x0400472F RID: 18223
	private int bitDepth = 4;
}
