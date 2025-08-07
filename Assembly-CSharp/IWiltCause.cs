using System;

// Token: 0x0200088E RID: 2190
public interface IWiltCause
{
	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x06003C4D RID: 15437
	string WiltStateString { get; }

	// Token: 0x1700042D RID: 1069
	// (get) Token: 0x06003C4E RID: 15438
	WiltCondition.Condition[] Conditions { get; }
}
