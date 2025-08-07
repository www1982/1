using System;
using UnityEngine;

// Token: 0x02000D90 RID: 3472
public class LegendEntry
{
	// Token: 0x06006C41 RID: 27713 RVA: 0x0028DC0C File Offset: 0x0028BE0C
	public LegendEntry(string name, string desc, Color colour, string desc_arg = null, Sprite sprite = null, bool displaySprite = true)
	{
		this.name = name;
		this.desc = desc;
		this.colour = colour;
		this.desc_arg = desc_arg;
		this.sprite = ((sprite == null) ? Assets.instance.LegendColourBox : sprite);
		this.displaySprite = displaySprite;
	}

	// Token: 0x040049C8 RID: 18888
	public string name;

	// Token: 0x040049C9 RID: 18889
	public string desc;

	// Token: 0x040049CA RID: 18890
	public string desc_arg;

	// Token: 0x040049CB RID: 18891
	public Color colour;

	// Token: 0x040049CC RID: 18892
	public Sprite sprite;

	// Token: 0x040049CD RID: 18893
	public bool displaySprite;
}
