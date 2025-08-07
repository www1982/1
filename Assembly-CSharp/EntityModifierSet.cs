using System;
using Database;

// Token: 0x02000C01 RID: 3073
public class EntityModifierSet : ModifierSet
{
	// Token: 0x06005CA6 RID: 23718 RVA: 0x0021C9AC File Offset: 0x0021ABAC
	public override void Initialize()
	{
		base.Initialize();
		this.DuplicantStatusItems = new DuplicantStatusItems(this.Root);
		this.ChoreGroups = new ChoreGroups(this.Root);
		base.LoadTraits();
	}

	// Token: 0x04003D97 RID: 15767
	public DuplicantStatusItems DuplicantStatusItems;

	// Token: 0x04003D98 RID: 15768
	public ChoreGroups ChoreGroups;
}
