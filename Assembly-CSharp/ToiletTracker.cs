using System;

// Token: 0x0200062F RID: 1583
public class ToiletTracker : WorldTracker
{
	// Token: 0x06002658 RID: 9816 RVA: 0x000DAEDF File Offset: 0x000D90DF
	public ToiletTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002659 RID: 9817 RVA: 0x000DAEE8 File Offset: 0x000D90E8
	public override void UpdateData()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600265A RID: 9818 RVA: 0x000DAEEF File Offset: 0x000D90EF
	public override string FormatValueString(float value)
	{
		return value.ToString();
	}
}
