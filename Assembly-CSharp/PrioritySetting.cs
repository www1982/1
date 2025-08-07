using System;

// Token: 0x020004AC RID: 1196
public struct PrioritySetting : IComparable<PrioritySetting>
{
	// Token: 0x0600194A RID: 6474 RVA: 0x0008B8EC File Offset: 0x00089AEC
	public override int GetHashCode()
	{
		return ((int)((int)this.priority_class << 28)).GetHashCode() ^ this.priority_value.GetHashCode();
	}

	// Token: 0x0600194B RID: 6475 RVA: 0x0008B916 File Offset: 0x00089B16
	public static bool operator ==(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.Equals(rhs);
	}

	// Token: 0x0600194C RID: 6476 RVA: 0x0008B92B File Offset: 0x00089B2B
	public static bool operator !=(PrioritySetting lhs, PrioritySetting rhs)
	{
		return !lhs.Equals(rhs);
	}

	// Token: 0x0600194D RID: 6477 RVA: 0x0008B943 File Offset: 0x00089B43
	public static bool operator <=(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.CompareTo(rhs) <= 0;
	}

	// Token: 0x0600194E RID: 6478 RVA: 0x0008B953 File Offset: 0x00089B53
	public static bool operator >=(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.CompareTo(rhs) >= 0;
	}

	// Token: 0x0600194F RID: 6479 RVA: 0x0008B963 File Offset: 0x00089B63
	public static bool operator <(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.CompareTo(rhs) < 0;
	}

	// Token: 0x06001950 RID: 6480 RVA: 0x0008B970 File Offset: 0x00089B70
	public static bool operator >(PrioritySetting lhs, PrioritySetting rhs)
	{
		return lhs.CompareTo(rhs) > 0;
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x0008B97D File Offset: 0x00089B7D
	public override bool Equals(object obj)
	{
		return obj is PrioritySetting && ((PrioritySetting)obj).priority_class == this.priority_class && ((PrioritySetting)obj).priority_value == this.priority_value;
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x0008B9B4 File Offset: 0x00089BB4
	public int CompareTo(PrioritySetting other)
	{
		if (this.priority_class > other.priority_class)
		{
			return 1;
		}
		if (this.priority_class < other.priority_class)
		{
			return -1;
		}
		if (this.priority_value > other.priority_value)
		{
			return 1;
		}
		if (this.priority_value < other.priority_value)
		{
			return -1;
		}
		return 0;
	}

	// Token: 0x06001953 RID: 6483 RVA: 0x0008BA02 File Offset: 0x00089C02
	public PrioritySetting(PriorityScreen.PriorityClass priority_class, int priority_value)
	{
		this.priority_class = priority_class;
		this.priority_value = priority_value;
	}

	// Token: 0x04000E7F RID: 3711
	public PriorityScreen.PriorityClass priority_class;

	// Token: 0x04000E80 RID: 3712
	public int priority_value;
}
