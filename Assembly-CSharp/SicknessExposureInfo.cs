using System;

// Token: 0x02000A0A RID: 2570
[Serializable]
public struct SicknessExposureInfo
{
	// Token: 0x06004ADD RID: 19165 RVA: 0x001B1E00 File Offset: 0x001B0000
	public SicknessExposureInfo(string id, string infection_source_info)
	{
		this.sicknessID = id;
		this.sourceInfo = infection_source_info;
	}

	// Token: 0x04003187 RID: 12679
	public string sicknessID;

	// Token: 0x04003188 RID: 12680
	public string sourceInfo;
}
