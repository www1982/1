using System;
using UnityEngine;

namespace Rendering.World
{
	// Token: 0x02000EA8 RID: 3752
	public struct Mask
	{
		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06007812 RID: 30738 RVA: 0x002E8B93 File Offset: 0x002E6D93
		// (set) Token: 0x06007813 RID: 30739 RVA: 0x002E8B9B File Offset: 0x002E6D9B
		public Vector2 UV0 { readonly get; private set; }

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x06007814 RID: 30740 RVA: 0x002E8BA4 File Offset: 0x002E6DA4
		// (set) Token: 0x06007815 RID: 30741 RVA: 0x002E8BAC File Offset: 0x002E6DAC
		public Vector2 UV1 { readonly get; private set; }

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x06007816 RID: 30742 RVA: 0x002E8BB5 File Offset: 0x002E6DB5
		// (set) Token: 0x06007817 RID: 30743 RVA: 0x002E8BBD File Offset: 0x002E6DBD
		public Vector2 UV2 { readonly get; private set; }

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06007818 RID: 30744 RVA: 0x002E8BC6 File Offset: 0x002E6DC6
		// (set) Token: 0x06007819 RID: 30745 RVA: 0x002E8BCE File Offset: 0x002E6DCE
		public Vector2 UV3 { readonly get; private set; }

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600781A RID: 30746 RVA: 0x002E8BD7 File Offset: 0x002E6DD7
		// (set) Token: 0x0600781B RID: 30747 RVA: 0x002E8BDF File Offset: 0x002E6DDF
		public bool IsOpaque { readonly get; private set; }

		// Token: 0x0600781C RID: 30748 RVA: 0x002E8BE8 File Offset: 0x002E6DE8
		public Mask(TextureAtlas atlas, int texture_idx, bool transpose, bool flip_x, bool flip_y, bool is_opaque)
		{
			this = default(Mask);
			this.atlas = atlas;
			this.texture_idx = texture_idx;
			this.transpose = transpose;
			this.flip_x = flip_x;
			this.flip_y = flip_y;
			this.atlas_offset = 0;
			this.IsOpaque = is_opaque;
			this.Refresh();
		}

		// Token: 0x0600781D RID: 30749 RVA: 0x002E8C36 File Offset: 0x002E6E36
		public void SetOffset(int offset)
		{
			this.atlas_offset = offset;
			this.Refresh();
		}

		// Token: 0x0600781E RID: 30750 RVA: 0x002E8C48 File Offset: 0x002E6E48
		public void Refresh()
		{
			int num = this.atlas_offset * 4 + this.atlas_offset;
			if (num + this.texture_idx >= this.atlas.items.Length)
			{
				num = 0;
			}
			Vector4 uvBox = this.atlas.items[num + this.texture_idx].uvBox;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			Vector2 zero3 = Vector2.zero;
			Vector2 zero4 = Vector2.zero;
			if (this.transpose)
			{
				float num2 = uvBox.x;
				float num3 = uvBox.z;
				if (this.flip_x)
				{
					num2 = uvBox.z;
					num3 = uvBox.x;
				}
				zero.x = num2;
				zero2.x = num2;
				zero3.x = num3;
				zero4.x = num3;
				float num4 = uvBox.y;
				float num5 = uvBox.w;
				if (this.flip_y)
				{
					num4 = uvBox.w;
					num5 = uvBox.y;
				}
				zero.y = num4;
				zero2.y = num5;
				zero3.y = num4;
				zero4.y = num5;
			}
			else
			{
				float num6 = uvBox.x;
				float num7 = uvBox.z;
				if (this.flip_x)
				{
					num6 = uvBox.z;
					num7 = uvBox.x;
				}
				zero.x = num6;
				zero2.x = num7;
				zero3.x = num6;
				zero4.x = num7;
				float num8 = uvBox.y;
				float num9 = uvBox.w;
				if (this.flip_y)
				{
					num8 = uvBox.w;
					num9 = uvBox.y;
				}
				zero.y = num9;
				zero2.y = num9;
				zero3.y = num8;
				zero4.y = num8;
			}
			this.UV0 = zero;
			this.UV1 = zero2;
			this.UV2 = zero3;
			this.UV3 = zero4;
		}

		// Token: 0x04005371 RID: 21361
		private TextureAtlas atlas;

		// Token: 0x04005372 RID: 21362
		private int texture_idx;

		// Token: 0x04005373 RID: 21363
		private bool transpose;

		// Token: 0x04005374 RID: 21364
		private bool flip_x;

		// Token: 0x04005375 RID: 21365
		private bool flip_y;

		// Token: 0x04005376 RID: 21366
		private int atlas_offset;

		// Token: 0x04005377 RID: 21367
		private const int TILES_PER_SET = 4;
	}
}
