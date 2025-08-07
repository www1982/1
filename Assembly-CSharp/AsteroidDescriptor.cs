using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C68 RID: 3176
public struct AsteroidDescriptor
{
	// Token: 0x060060E4 RID: 24804 RVA: 0x0023D02F File Offset: 0x0023B22F
	public AsteroidDescriptor(string text, string tooltip, Color associatedColor, List<global::Tuple<string, Color, float>> bands = null, string associatedIcon = null)
	{
		this.text = text;
		this.tooltip = tooltip;
		this.associatedColor = associatedColor;
		this.bands = bands;
		this.associatedIcon = associatedIcon;
	}

	// Token: 0x04004176 RID: 16758
	public string text;

	// Token: 0x04004177 RID: 16759
	public string tooltip;

	// Token: 0x04004178 RID: 16760
	public List<global::Tuple<string, Color, float>> bands;

	// Token: 0x04004179 RID: 16761
	public Color associatedColor;

	// Token: 0x0400417A RID: 16762
	public string associatedIcon;
}
