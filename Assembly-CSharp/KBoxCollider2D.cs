using System;
using UnityEngine;

// Token: 0x020005C9 RID: 1481
public class KBoxCollider2D : KCollider2D
{
	// Token: 0x1700015B RID: 347
	// (get) Token: 0x06002222 RID: 8738 RVA: 0x000C4992 File Offset: 0x000C2B92
	// (set) Token: 0x06002223 RID: 8739 RVA: 0x000C499A File Offset: 0x000C2B9A
	public Vector2 size
	{
		get
		{
			return this._size;
		}
		set
		{
			this._size = value;
			base.MarkDirty(false);
		}
	}

	// Token: 0x06002224 RID: 8740 RVA: 0x000C49AC File Offset: 0x000C2BAC
	public override Extents GetExtents()
	{
		Vector3 vector = base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f);
		Vector2 vector2 = this.size * 0.9999f;
		Vector2 vector3 = new Vector2(vector.x - vector2.x * 0.5f, vector.y - vector2.y * 0.5f);
		Vector2 vector4 = new Vector2(vector.x + vector2.x * 0.5f, vector.y + vector2.y * 0.5f);
		Vector2I vector2I = new Vector2I((int)vector3.x, (int)vector3.y);
		Vector2I vector2I2 = new Vector2I((int)vector4.x, (int)vector4.y);
		int num = vector2I2.x - vector2I.x + 1;
		int num2 = vector2I2.y - vector2I.y + 1;
		return new Extents(vector2I.x, vector2I.y, num, num2);
	}

	// Token: 0x06002225 RID: 8741 RVA: 0x000C4AB8 File Offset: 0x000C2CB8
	public override bool Intersects(Vector2 intersect_pos)
	{
		Vector3 vector = base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f);
		Vector2 vector2 = new Vector2(vector.x - this.size.x * 0.5f, vector.y - this.size.y * 0.5f);
		Vector2 vector3 = new Vector2(vector.x + this.size.x * 0.5f, vector.y + this.size.y * 0.5f);
		return intersect_pos.x >= vector2.x && intersect_pos.x <= vector3.x && intersect_pos.y >= vector2.y && intersect_pos.y <= vector3.y;
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06002226 RID: 8742 RVA: 0x000C4BA4 File Offset: 0x000C2DA4
	public override Bounds bounds
	{
		get
		{
			return new Bounds(base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f), new Vector3(this._size.x, this._size.y, 0f));
		}
	}

	// Token: 0x06002227 RID: 8743 RVA: 0x000C4C08 File Offset: 0x000C2E08
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(this.bounds.center, new Vector3(this._size.x, this._size.y, 0f));
	}

	// Token: 0x040013EA RID: 5098
	[SerializeField]
	private Vector2 _size;
}
