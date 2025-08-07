using System;

// Token: 0x020008D2 RID: 2258
[Serializable]
public struct EffectorValues
{
	// Token: 0x06003EA8 RID: 16040 RVA: 0x00161420 File Offset: 0x0015F620
	public EffectorValues(int amt, int rad)
	{
		this.amount = amt;
		this.radius = rad;
	}

	// Token: 0x06003EA9 RID: 16041 RVA: 0x00161430 File Offset: 0x0015F630
	public override bool Equals(object obj)
	{
		return obj is EffectorValues && this.Equals((EffectorValues)obj);
	}

	// Token: 0x06003EAA RID: 16042 RVA: 0x00161448 File Offset: 0x0015F648
	public bool Equals(EffectorValues p)
	{
		return p != null && (this == p || (!(base.GetType() != p.GetType()) && this.amount == p.amount && this.radius == p.radius));
	}

	// Token: 0x06003EAB RID: 16043 RVA: 0x001614B6 File Offset: 0x0015F6B6
	public override int GetHashCode()
	{
		return this.amount ^ this.radius;
	}

	// Token: 0x06003EAC RID: 16044 RVA: 0x001614C5 File Offset: 0x0015F6C5
	public static bool operator ==(EffectorValues lhs, EffectorValues rhs)
	{
		if (lhs == null)
		{
			return rhs == null;
		}
		return lhs.Equals(rhs);
	}

	// Token: 0x06003EAD RID: 16045 RVA: 0x001614E3 File Offset: 0x0015F6E3
	public static bool operator !=(EffectorValues lhs, EffectorValues rhs)
	{
		return !(lhs == rhs);
	}

	// Token: 0x040026A8 RID: 9896
	public int amount;

	// Token: 0x040026A9 RID: 9897
	public int radius;
}
