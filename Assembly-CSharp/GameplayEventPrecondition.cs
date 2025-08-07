using System;

// Token: 0x020004BF RID: 1215
public class GameplayEventPrecondition
{
	// Token: 0x04000EF1 RID: 3825
	public string description;

	// Token: 0x04000EF2 RID: 3826
	public GameplayEventPrecondition.PreconditionFn condition;

	// Token: 0x04000EF3 RID: 3827
	public bool required;

	// Token: 0x04000EF4 RID: 3828
	public int priorityModifier;

	// Token: 0x020012FE RID: 4862
	// (Invoke) Token: 0x0600885A RID: 34906
	public delegate bool PreconditionFn();
}
