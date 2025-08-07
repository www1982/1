using System;
using UnityEngine;

// Token: 0x020008C7 RID: 2247
public class Dream : Resource
{
	// Token: 0x06003E49 RID: 15945 RVA: 0x0015C6AC File Offset: 0x0015A8AC
	public Dream(string id, ResourceSet parent, string background, string[] icons_sprite_names)
		: base(id, parent, null)
	{
		this.Icons = new Sprite[icons_sprite_names.Length];
		this.BackgroundAnim = background;
		for (int i = 0; i < icons_sprite_names.Length; i++)
		{
			this.Icons[i] = Assets.GetSprite(icons_sprite_names[i]);
		}
	}

	// Token: 0x06003E4A RID: 15946 RVA: 0x0015C708 File Offset: 0x0015A908
	public Dream(string id, ResourceSet parent, string background, string[] icons_sprite_names, float durationPerImage)
		: this(id, parent, background, icons_sprite_names)
	{
		this.secondPerImage = durationPerImage;
	}

	// Token: 0x0400264A RID: 9802
	public string BackgroundAnim;

	// Token: 0x0400264B RID: 9803
	public Sprite[] Icons;

	// Token: 0x0400264C RID: 9804
	public float secondPerImage = 2.4f;
}
