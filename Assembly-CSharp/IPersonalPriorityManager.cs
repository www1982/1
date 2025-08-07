using System;

// Token: 0x02000D3D RID: 3389
public interface IPersonalPriorityManager
{
	// Token: 0x060068A5 RID: 26789
	int GetAssociatedSkillLevel(ChoreGroup group);

	// Token: 0x060068A6 RID: 26790
	int GetPersonalPriority(ChoreGroup group);

	// Token: 0x060068A7 RID: 26791
	void SetPersonalPriority(ChoreGroup group, int value);

	// Token: 0x060068A8 RID: 26792
	bool IsChoreGroupDisabled(ChoreGroup group);

	// Token: 0x060068A9 RID: 26793
	void ResetPersonalPriorities();
}
