using System;

// Token: 0x02000BD2 RID: 3026
public struct UtilityNetworkGridNode : IEquatable<UtilityNetworkGridNode>
{
	// Token: 0x06005A95 RID: 23189 RVA: 0x0020B5CF File Offset: 0x002097CF
	public bool Equals(UtilityNetworkGridNode other)
	{
		return this.connections == other.connections && this.networkIdx == other.networkIdx;
	}

	// Token: 0x06005A96 RID: 23190 RVA: 0x0020B5F0 File Offset: 0x002097F0
	public override bool Equals(object obj)
	{
		return ((UtilityNetworkGridNode)obj).Equals(this);
	}

	// Token: 0x06005A97 RID: 23191 RVA: 0x0020B611 File Offset: 0x00209811
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	// Token: 0x06005A98 RID: 23192 RVA: 0x0020B623 File Offset: 0x00209823
	public static bool operator ==(UtilityNetworkGridNode x, UtilityNetworkGridNode y)
	{
		return x.Equals(y);
	}

	// Token: 0x06005A99 RID: 23193 RVA: 0x0020B62D File Offset: 0x0020982D
	public static bool operator !=(UtilityNetworkGridNode x, UtilityNetworkGridNode y)
	{
		return !x.Equals(y);
	}

	// Token: 0x04003C12 RID: 15378
	public UtilityConnections connections;

	// Token: 0x04003C13 RID: 15379
	public int networkIdx;

	// Token: 0x04003C14 RID: 15380
	public const int InvalidNetworkIdx = -1;
}
