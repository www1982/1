using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BF2 RID: 3058
public class GroundMasks : ScriptableObject
{
	// Token: 0x06005C30 RID: 23600 RVA: 0x00218CC4 File Offset: 0x00216EC4
	public void Initialize()
	{
		if (this.maskAtlas == null || this.maskAtlas.items == null)
		{
			return;
		}
		this.biomeMasks = new Dictionary<string, GroundMasks.BiomeMaskData>();
		foreach (TextureAtlas.Item item in this.maskAtlas.items)
		{
			string name = item.name;
			int num = name.IndexOf('/');
			string text = name.Substring(0, num);
			string text2 = name.Substring(num + 1, 4);
			text = text.ToLower();
			for (int num2 = text.IndexOf('_'); num2 != -1; num2 = text.IndexOf('_'))
			{
				text = text.Remove(num2, 1);
			}
			GroundMasks.BiomeMaskData biomeMaskData = null;
			if (!this.biomeMasks.TryGetValue(text, out biomeMaskData))
			{
				biomeMaskData = new GroundMasks.BiomeMaskData(text);
				this.biomeMasks[text] = biomeMaskData;
			}
			int num3 = Convert.ToInt32(text2, 2);
			GroundMasks.Tile tile = biomeMaskData.tiles[num3];
			if (tile.variationUVs == null)
			{
				tile.isSource = true;
				tile.variationUVs = new GroundMasks.UVData[1];
			}
			else
			{
				GroundMasks.UVData[] array = new GroundMasks.UVData[tile.variationUVs.Length + 1];
				Array.Copy(tile.variationUVs, array, tile.variationUVs.Length);
				tile.variationUVs = array;
			}
			Vector4 vector = new Vector4(item.uvBox.x, item.uvBox.w, item.uvBox.z, item.uvBox.y);
			Vector2 vector2 = new Vector2(vector.x, vector.y);
			Vector2 vector3 = new Vector2(vector.z, vector.y);
			Vector2 vector4 = new Vector2(vector.x, vector.w);
			Vector2 vector5 = new Vector2(vector.z, vector.w);
			GroundMasks.UVData uvdata = new GroundMasks.UVData(vector2, vector3, vector4, vector5);
			tile.variationUVs[tile.variationUVs.Length - 1] = uvdata;
			biomeMaskData.tiles[num3] = tile;
		}
		foreach (KeyValuePair<string, GroundMasks.BiomeMaskData> keyValuePair in this.biomeMasks)
		{
			keyValuePair.Value.GenerateRotations();
			keyValuePair.Value.Validate();
		}
	}

	// Token: 0x06005C31 RID: 23601 RVA: 0x00218F28 File Offset: 0x00217128
	[ContextMenu("Print Variations")]
	private void Regenerate()
	{
		this.Initialize();
		string text = "Listing all variations:\n";
		foreach (KeyValuePair<string, GroundMasks.BiomeMaskData> keyValuePair in this.biomeMasks)
		{
			GroundMasks.BiomeMaskData value = keyValuePair.Value;
			text = text + "Biome: " + value.name + "\n";
			for (int i = 1; i < value.tiles.Length; i++)
			{
				GroundMasks.Tile tile = value.tiles[i];
				text += string.Format("  tile {0}: {1} variations\n", Convert.ToString(i, 2).PadLeft(4, '0'), tile.variationUVs.Length);
			}
		}
		global::Debug.Log(text);
	}

	// Token: 0x04003D10 RID: 15632
	public TextureAtlas maskAtlas;

	// Token: 0x04003D11 RID: 15633
	[NonSerialized]
	public Dictionary<string, GroundMasks.BiomeMaskData> biomeMasks;

	// Token: 0x02001D33 RID: 7475
	public struct UVData
	{
		// Token: 0x0600AD77 RID: 44407 RVA: 0x003C48D5 File Offset: 0x003C2AD5
		public UVData(Vector2 bl, Vector2 br, Vector2 tl, Vector2 tr)
		{
			this.bl = bl;
			this.br = br;
			this.tl = tl;
			this.tr = tr;
		}

		// Token: 0x0400886F RID: 34927
		public Vector2 bl;

		// Token: 0x04008870 RID: 34928
		public Vector2 br;

		// Token: 0x04008871 RID: 34929
		public Vector2 tl;

		// Token: 0x04008872 RID: 34930
		public Vector2 tr;
	}

	// Token: 0x02001D34 RID: 7476
	public struct Tile
	{
		// Token: 0x04008873 RID: 34931
		public bool isSource;

		// Token: 0x04008874 RID: 34932
		public GroundMasks.UVData[] variationUVs;
	}

	// Token: 0x02001D35 RID: 7477
	public class BiomeMaskData
	{
		// Token: 0x0600AD78 RID: 44408 RVA: 0x003C48F4 File Offset: 0x003C2AF4
		public BiomeMaskData(string name)
		{
			this.name = name;
			this.tiles = new GroundMasks.Tile[16];
		}

		// Token: 0x0600AD79 RID: 44409 RVA: 0x003C4910 File Offset: 0x003C2B10
		public void GenerateRotations()
		{
			for (int i = 1; i < 15; i++)
			{
				if (!this.tiles[i].isSource)
				{
					GroundMasks.Tile tile = this.tiles[i];
					tile.variationUVs = this.GetNonNullRotationUVs(i);
					this.tiles[i] = tile;
				}
			}
		}

		// Token: 0x0600AD7A RID: 44410 RVA: 0x003C4968 File Offset: 0x003C2B68
		public GroundMasks.UVData[] GetNonNullRotationUVs(int dest_mask)
		{
			GroundMasks.UVData[] array = null;
			int num = dest_mask;
			for (int i = 0; i < 3; i++)
			{
				int num2 = num & 1;
				int num3 = (num & 2) >> 1;
				int num4 = (num & 4) >> 2;
				int num5 = ((num & 8) >> 3 << 2) | num4 | (num3 << 3) | (num2 << 1);
				if (this.tiles[num5].isSource)
				{
					array = new GroundMasks.UVData[this.tiles[num5].variationUVs.Length];
					for (int j = 0; j < this.tiles[num5].variationUVs.Length; j++)
					{
						GroundMasks.UVData uvdata = this.tiles[num5].variationUVs[j];
						GroundMasks.UVData uvdata2 = uvdata;
						switch (i)
						{
						case 0:
							uvdata2 = new GroundMasks.UVData(uvdata.tl, uvdata.bl, uvdata.tr, uvdata.br);
							break;
						case 1:
							uvdata2 = new GroundMasks.UVData(uvdata.tr, uvdata.tl, uvdata.br, uvdata.bl);
							break;
						case 2:
							uvdata2 = new GroundMasks.UVData(uvdata.br, uvdata.tr, uvdata.bl, uvdata.tl);
							break;
						default:
							global::Debug.LogError("Unhandled rotation case");
							break;
						}
						array[j] = uvdata2;
					}
					break;
				}
				num = num5;
			}
			return array;
		}

		// Token: 0x0600AD7B RID: 44411 RVA: 0x003C4AC8 File Offset: 0x003C2CC8
		public void Validate()
		{
			for (int i = 1; i < this.tiles.Length; i++)
			{
				if (this.tiles[i].variationUVs == null)
				{
					DebugUtil.LogErrorArgs(new object[] { this.name, "has invalid tile at index", i });
				}
			}
		}

		// Token: 0x04008875 RID: 34933
		public string name;

		// Token: 0x04008876 RID: 34934
		public GroundMasks.Tile[] tiles;
	}
}
