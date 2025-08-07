using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E5B RID: 3675
[AddComponentMenu("KMonoBehaviour/scripts/StarmapPlanetVisualizer")]
public class StarmapPlanetVisualizer : KMonoBehaviour
{
	// Token: 0x040050E6 RID: 20710
	public Image image;

	// Token: 0x040050E7 RID: 20711
	public LocText label;

	// Token: 0x040050E8 RID: 20712
	public MultiToggle button;

	// Token: 0x040050E9 RID: 20713
	public RectTransform selection;

	// Token: 0x040050EA RID: 20714
	public GameObject analysisSelection;

	// Token: 0x040050EB RID: 20715
	public Image unknownBG;

	// Token: 0x040050EC RID: 20716
	public GameObject rocketIconContainer;
}
