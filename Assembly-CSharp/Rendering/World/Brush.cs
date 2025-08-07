using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000EA4 RID: 3748
	public class Brush
	{
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060077F7 RID: 30711 RVA: 0x002E8314 File Offset: 0x002E6514
		// (set) Token: 0x060077F8 RID: 30712 RVA: 0x002E831C File Offset: 0x002E651C
		public int Id { get; private set; }

		// Token: 0x060077F9 RID: 30713 RVA: 0x002E8328 File Offset: 0x002E6528
		public Brush(int id, string name, Material material, Mask mask, List<Brush> active_brushes, List<Brush> dirty_brushes, int width_in_tiles, MaterialPropertyBlock property_block)
		{
			this.Id = id;
			this.material = material;
			this.mask = mask;
			this.mesh = new DynamicMesh(name, new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, 0f)));
			this.activeBrushes = active_brushes;
			this.dirtyBrushes = dirty_brushes;
			this.layer = LayerMask.NameToLayer("World");
			this.widthInTiles = width_in_tiles;
			this.propertyBlock = property_block;
		}

		// Token: 0x060077FA RID: 30714 RVA: 0x002E83B6 File Offset: 0x002E65B6
		public void Add(int tile_idx)
		{
			this.tiles.Add(tile_idx);
			if (!this.dirty)
			{
				this.dirtyBrushes.Add(this);
				this.dirty = true;
			}
		}

		// Token: 0x060077FB RID: 30715 RVA: 0x002E83E0 File Offset: 0x002E65E0
		public void Remove(int tile_idx)
		{
			this.tiles.Remove(tile_idx);
			if (!this.dirty)
			{
				this.dirtyBrushes.Add(this);
				this.dirty = true;
			}
		}

		// Token: 0x060077FC RID: 30716 RVA: 0x002E840A File Offset: 0x002E660A
		public void SetMaskOffset(int offset)
		{
			this.mask.SetOffset(offset);
		}

		// Token: 0x060077FD RID: 30717 RVA: 0x002E8418 File Offset: 0x002E6618
		public void Refresh()
		{
			bool flag = this.mesh.Meshes.Length != 0;
			int count = this.tiles.Count;
			int num = count * 4;
			int num2 = count * 6;
			this.mesh.Reserve(num, num2);
			if (this.mesh.SetTriangles)
			{
				int num3 = 0;
				for (int i = 0; i < count; i++)
				{
					this.mesh.AddTriangle(num3);
					this.mesh.AddTriangle(2 + num3);
					this.mesh.AddTriangle(1 + num3);
					this.mesh.AddTriangle(1 + num3);
					this.mesh.AddTriangle(2 + num3);
					this.mesh.AddTriangle(3 + num3);
					num3 += 4;
				}
			}
			foreach (int num4 in this.tiles)
			{
				float num5 = (float)(num4 % this.widthInTiles);
				float num6 = (float)(num4 / this.widthInTiles);
				float num7 = 0f;
				this.mesh.AddVertex(new Vector3(num5 - 0.5f, num6 - 0.5f, num7));
				this.mesh.AddVertex(new Vector3(num5 + 0.5f, num6 - 0.5f, num7));
				this.mesh.AddVertex(new Vector3(num5 - 0.5f, num6 + 0.5f, num7));
				this.mesh.AddVertex(new Vector3(num5 + 0.5f, num6 + 0.5f, num7));
			}
			if (this.mesh.SetUVs)
			{
				for (int j = 0; j < count; j++)
				{
					this.mesh.AddUV(this.mask.UV0);
					this.mesh.AddUV(this.mask.UV1);
					this.mesh.AddUV(this.mask.UV2);
					this.mesh.AddUV(this.mask.UV3);
				}
			}
			this.dirty = false;
			this.mesh.Commit();
			if (this.mesh.Meshes.Length != 0)
			{
				if (!flag)
				{
					this.activeBrushes.Add(this);
					return;
				}
			}
			else if (flag)
			{
				this.activeBrushes.Remove(this);
			}
		}

		// Token: 0x060077FE RID: 30718 RVA: 0x002E8674 File Offset: 0x002E6874
		public void Render()
		{
			Vector3 vector = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.Ground));
			this.mesh.Render(vector, Quaternion.identity, this.material, this.layer, this.propertyBlock);
		}

		// Token: 0x060077FF RID: 30719 RVA: 0x002E86BC File Offset: 0x002E68BC
		public void SetMaterial(Material material, MaterialPropertyBlock property_block)
		{
			this.material = material;
			this.propertyBlock = property_block;
		}

		// Token: 0x04005342 RID: 21314
		private bool dirty;

		// Token: 0x04005343 RID: 21315
		private Material material;

		// Token: 0x04005344 RID: 21316
		private int layer;

		// Token: 0x04005345 RID: 21317
		private HashSet<int> tiles = new HashSet<int>();

		// Token: 0x04005346 RID: 21318
		private List<Brush> activeBrushes;

		// Token: 0x04005347 RID: 21319
		private List<Brush> dirtyBrushes;

		// Token: 0x04005348 RID: 21320
		private int widthInTiles;

		// Token: 0x04005349 RID: 21321
		private Mask mask;

		// Token: 0x0400534A RID: 21322
		private DynamicMesh mesh;

		// Token: 0x0400534B RID: 21323
		private MaterialPropertyBlock propertyBlock;
	}
}
