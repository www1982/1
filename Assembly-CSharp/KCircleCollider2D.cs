using System;
using UnityEngine;

// Token: 0x020005CA RID: 1482
public class KCircleCollider2D : KCollider2D
{
	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06002229 RID: 8745 RVA: 0x000C4C5A File Offset: 0x000C2E5A
	// (set) Token: 0x0600222A RID: 8746 RVA: 0x000C4C62 File Offset: 0x000C2E62
	public float radius
	{
		get
		{
			return this._radius;
		}
		set
		{
			this._radius = value;
			base.MarkDirty(false);
		}
	}

	// Token: 0x0600222B RID: 8747 RVA: 0x000C4C74 File Offset: 0x000C2E74
	public override Extents GetExtents()
	{
		Vector3 vector = base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f);
		Vector2 vector2 = new Vector2(vector.x - this.radius, vector.y - this.radius);
		Vector2 vector3 = new Vector2(vector.x + this.radius, vector.y + this.radius);
		int num = (int)vector3.x - (int)vector2.x + 1;
		int num2 = (int)vector3.y - (int)vector2.y + 1;
		return new Extents((int)(vector.x - this._radius), (int)(vector.y - this._radius), num, num2);
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x0600222C RID: 8748 RVA: 0x000C4D38 File Offset: 0x000C2F38
	public override Bounds bounds
	{
		get
		{
			return new Bounds(base.transform.GetPosition() + new Vector3(base.offset.x, base.offset.y, 0f), new Vector3(this._radius * 2f, this._radius * 2f, 0f));
		}
	}

	// Token: 0x0600222D RID: 8749 RVA: 0x000C4D9C File Offset: 0x000C2F9C
	public override bool Intersects(Vector2 pos)
	{
		Vector3 position = base.transform.GetPosition();
		Vector2 vector = new Vector2(position.x, position.y) + base.offset;
		return (pos - vector).sqrMagnitude <= this._radius * this._radius;
	}

	// Token: 0x0600222E RID: 8750 RVA: 0x000C4DF4 File Offset: 0x000C2FF4
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(this.bounds.center, this.radius);
	}

	// Token: 0x040013EB RID: 5099
	[SerializeField]
	private float _radius;
}
