using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B12 RID: 2834
[AddComponentMenu("KMonoBehaviour/scripts/SituationalAnim")]
public class SituationalAnim : KMonoBehaviour
{
	// Token: 0x06005386 RID: 21382 RVA: 0x001E69E8 File Offset: 0x001E4BE8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SituationalAnim.Situation situation = this.GetSituation();
		DebugUtil.LogArgs(new object[] { "Situation is", situation });
		this.SetAnimForSituation(situation);
	}

	// Token: 0x06005387 RID: 21383 RVA: 0x001E6A28 File Offset: 0x001E4C28
	private void SetAnimForSituation(SituationalAnim.Situation situation)
	{
		foreach (global::Tuple<SituationalAnim.Situation, string> tuple in this.anims)
		{
			if ((tuple.first & situation) == tuple.first)
			{
				DebugUtil.LogArgs(new object[] { "Chose Anim", tuple.first, tuple.second });
				this.SetAnim(tuple.second);
				break;
			}
		}
	}

	// Token: 0x06005388 RID: 21384 RVA: 0x001E6ABC File Offset: 0x001E4CBC
	private void SetAnim(string animName)
	{
		base.GetComponent<KBatchedAnimController>().Play(animName, KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06005389 RID: 21385 RVA: 0x001E6ADC File Offset: 0x001E4CDC
	private SituationalAnim.Situation GetSituation()
	{
		SituationalAnim.Situation situation = (SituationalAnim.Situation)0;
		Extents extents = base.GetComponent<Building>().GetExtents();
		int x = extents.x;
		int num = extents.x + extents.width - 1;
		int y = extents.y;
		int num2 = extents.y + extents.height - 1;
		if (this.DoesSatisfy(this.GetSatisfactionForEdge(x, num, y - 1, y - 1), this.mustSatisfy))
		{
			situation |= SituationalAnim.Situation.Bottom;
		}
		if (this.DoesSatisfy(this.GetSatisfactionForEdge(x - 1, x - 1, y, num2), this.mustSatisfy))
		{
			situation |= SituationalAnim.Situation.Left;
		}
		if (this.DoesSatisfy(this.GetSatisfactionForEdge(x, num, num2 + 1, num2 + 1), this.mustSatisfy))
		{
			situation |= SituationalAnim.Situation.Top;
		}
		if (this.DoesSatisfy(this.GetSatisfactionForEdge(num + 1, num + 1, y, num2), this.mustSatisfy))
		{
			situation |= SituationalAnim.Situation.Right;
		}
		return situation;
	}

	// Token: 0x0600538A RID: 21386 RVA: 0x001E6BB0 File Offset: 0x001E4DB0
	private bool DoesSatisfy(SituationalAnim.MustSatisfy result, SituationalAnim.MustSatisfy requirement)
	{
		if (requirement == SituationalAnim.MustSatisfy.All)
		{
			return result == SituationalAnim.MustSatisfy.All;
		}
		if (requirement == SituationalAnim.MustSatisfy.Any)
		{
			return result > SituationalAnim.MustSatisfy.None;
		}
		return result == SituationalAnim.MustSatisfy.None;
	}

	// Token: 0x0600538B RID: 21387 RVA: 0x001E6BC8 File Offset: 0x001E4DC8
	private SituationalAnim.MustSatisfy GetSatisfactionForEdge(int minx, int maxx, int miny, int maxy)
	{
		bool flag = false;
		bool flag2 = true;
		for (int i = minx; i <= maxx; i++)
		{
			for (int j = miny; j <= maxy; j++)
			{
				int num = Grid.XYToCell(i, j);
				if (this.test(num))
				{
					flag = true;
				}
				else
				{
					flag2 = false;
				}
			}
		}
		if (flag2)
		{
			return SituationalAnim.MustSatisfy.All;
		}
		if (flag)
		{
			return SituationalAnim.MustSatisfy.Any;
		}
		return SituationalAnim.MustSatisfy.None;
	}

	// Token: 0x04003821 RID: 14369
	public List<global::Tuple<SituationalAnim.Situation, string>> anims;

	// Token: 0x04003822 RID: 14370
	public Func<int, bool> test;

	// Token: 0x04003823 RID: 14371
	public SituationalAnim.MustSatisfy mustSatisfy;

	// Token: 0x02001C18 RID: 7192
	[Flags]
	public enum Situation
	{
		// Token: 0x04008521 RID: 34081
		Left = 1,
		// Token: 0x04008522 RID: 34082
		Right = 2,
		// Token: 0x04008523 RID: 34083
		Top = 4,
		// Token: 0x04008524 RID: 34084
		Bottom = 8
	}

	// Token: 0x02001C19 RID: 7193
	public enum MustSatisfy
	{
		// Token: 0x04008526 RID: 34086
		None,
		// Token: 0x04008527 RID: 34087
		Any,
		// Token: 0x04008528 RID: 34088
		All
	}
}
