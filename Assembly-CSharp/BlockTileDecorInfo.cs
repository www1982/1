using System;
using Rendering;
using UnityEngine;

// Token: 0x02000BFA RID: 3066
public class BlockTileDecorInfo : ScriptableObject
{
	// Token: 0x06005C67 RID: 23655 RVA: 0x0021BF5C File Offset: 0x0021A15C
	public void PostProcess()
	{
		if (this.decor != null && this.atlas != null && this.atlas.items != null)
		{
			for (int i = 0; i < this.decor.Length; i++)
			{
				if (this.decor[i].variants != null && this.decor[i].variants.Length != 0)
				{
					for (int j = 0; j < this.decor[i].variants.Length; j++)
					{
						bool flag = false;
						foreach (TextureAtlas.Item item in this.atlas.items)
						{
							string text = item.name;
							int num = text.IndexOf("/");
							if (num != -1)
							{
								text = text.Substring(num + 1);
							}
							if (this.decor[i].variants[j].name == text)
							{
								this.decor[i].variants[j].atlasItem = item;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							DebugUtil.LogErrorArgs(new object[]
							{
								base.name,
								"/",
								this.decor[i].name,
								"could not find ",
								this.decor[i].variants[j].name,
								"in",
								this.atlas.name
							});
						}
					}
				}
			}
		}
	}

	// Token: 0x04003D34 RID: 15668
	public TextureAtlas atlas;

	// Token: 0x04003D35 RID: 15669
	public TextureAtlas atlasSpec;

	// Token: 0x04003D36 RID: 15670
	public int sortOrder;

	// Token: 0x04003D37 RID: 15671
	public BlockTileDecorInfo.Decor[] decor;

	// Token: 0x02001D3B RID: 7483
	[Serializable]
	public struct ImageInfo
	{
		// Token: 0x0400888A RID: 34954
		public string name;

		// Token: 0x0400888B RID: 34955
		public Vector3 offset;

		// Token: 0x0400888C RID: 34956
		[NonSerialized]
		public TextureAtlas.Item atlasItem;
	}

	// Token: 0x02001D3C RID: 7484
	[Serializable]
	public struct Decor
	{
		// Token: 0x0400888D RID: 34957
		public string name;

		// Token: 0x0400888E RID: 34958
		[EnumFlags]
		public BlockTileRenderer.Bits requiredConnections;

		// Token: 0x0400888F RID: 34959
		[EnumFlags]
		public BlockTileRenderer.Bits forbiddenConnections;

		// Token: 0x04008890 RID: 34960
		public float probabilityCutoff;

		// Token: 0x04008891 RID: 34961
		public BlockTileDecorInfo.ImageInfo[] variants;

		// Token: 0x04008892 RID: 34962
		public int sortOrder;
	}
}
