using System;

// Token: 0x02000840 RID: 2112
public interface IConduitFlow
{
	// Token: 0x060039D7 RID: 14807
	void AddConduitUpdater(Action<float> callback, ConduitFlowPriority priority = ConduitFlowPriority.Default);

	// Token: 0x060039D8 RID: 14808
	void RemoveConduitUpdater(Action<float> callback);

	// Token: 0x060039D9 RID: 14809
	bool IsConduitEmpty(int cell);
}
