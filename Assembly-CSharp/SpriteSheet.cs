using System;
using UnityEngine;

// Token: 0x02000615 RID: 1557
[Serializable]
public struct SpriteSheet
{
	// Token: 0x040015B2 RID: 5554
	public string name;

	// Token: 0x040015B3 RID: 5555
	public int numFrames;

	// Token: 0x040015B4 RID: 5556
	public int numXFrames;

	// Token: 0x040015B5 RID: 5557
	public Vector2 uvFrameSize;

	// Token: 0x040015B6 RID: 5558
	public int renderLayer;

	// Token: 0x040015B7 RID: 5559
	public Material material;

	// Token: 0x040015B8 RID: 5560
	public Texture2D texture;
}
