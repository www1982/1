using System;
using System.Collections.Generic;

// Token: 0x020005C1 RID: 1473
public interface IAssignableIdentity
{
	// Token: 0x060021EB RID: 8683
	string GetProperName();

	// Token: 0x060021EC RID: 8684
	List<Ownables> GetOwners();

	// Token: 0x060021ED RID: 8685
	Ownables GetSoleOwner();

	// Token: 0x060021EE RID: 8686
	bool IsNull();

	// Token: 0x060021EF RID: 8687
	bool HasOwner(Assignables owner);

	// Token: 0x060021F0 RID: 8688
	int NumOwners();
}
