using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D23 RID: 3363
[AddComponentMenu("KMonoBehaviour/scripts/LogicRibbonDisplayUI")]
public class LogicControlInputUI : KMonoBehaviour
{
	// Token: 0x060067C3 RID: 26563 RVA: 0x00271A38 File Offset: 0x0026FC38
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.colourOn = GlobalAssets.Instance.colorSet.logicOn;
		this.colourOff = GlobalAssets.Instance.colorSet.logicOff;
		this.colourOn.a = (this.colourOff.a = byte.MaxValue);
		this.colourDisconnected = GlobalAssets.Instance.colorSet.logicDisconnected;
		this.icon.raycastTarget = false;
		this.border.raycastTarget = false;
	}

	// Token: 0x060067C4 RID: 26564 RVA: 0x00271AC0 File Offset: 0x0026FCC0
	public void SetContent(LogicCircuitNetwork network)
	{
		Color32 color = ((network == null) ? GlobalAssets.Instance.colorSet.logicDisconnected : (network.IsBitActive(0) ? this.colourOn : this.colourOff));
		this.icon.color = color;
	}

	// Token: 0x04004721 RID: 18209
	[SerializeField]
	private Image icon;

	// Token: 0x04004722 RID: 18210
	[SerializeField]
	private Image border;

	// Token: 0x04004723 RID: 18211
	[SerializeField]
	private LogicModeUI uiAsset;

	// Token: 0x04004724 RID: 18212
	private Color32 colourOn;

	// Token: 0x04004725 RID: 18213
	private Color32 colourOff;

	// Token: 0x04004726 RID: 18214
	private Color32 colourDisconnected;
}
