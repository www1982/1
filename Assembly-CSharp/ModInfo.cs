using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Token: 0x02000331 RID: 817
[Serializable]
public struct ModInfo
{
	// Token: 0x04000AA6 RID: 2726
	[JsonConverter(typeof(StringEnumConverter))]
	public ModInfo.Source source;

	// Token: 0x04000AA7 RID: 2727
	[JsonConverter(typeof(StringEnumConverter))]
	public ModInfo.ModType type;

	// Token: 0x04000AA8 RID: 2728
	public string assetID;

	// Token: 0x04000AA9 RID: 2729
	public string assetPath;

	// Token: 0x04000AAA RID: 2730
	public bool enabled;

	// Token: 0x04000AAB RID: 2731
	public bool markedForDelete;

	// Token: 0x04000AAC RID: 2732
	public bool markedForUpdate;

	// Token: 0x04000AAD RID: 2733
	public string description;

	// Token: 0x04000AAE RID: 2734
	public ulong lastModifiedTime;

	// Token: 0x020011DC RID: 4572
	public enum Source
	{
		// Token: 0x0400645F RID: 25695
		Local,
		// Token: 0x04006460 RID: 25696
		Steam,
		// Token: 0x04006461 RID: 25697
		Rail
	}

	// Token: 0x020011DD RID: 4573
	public enum ModType
	{
		// Token: 0x04006463 RID: 25699
		WorldGen,
		// Token: 0x04006464 RID: 25700
		Scenario,
		// Token: 0x04006465 RID: 25701
		Mod
	}
}
