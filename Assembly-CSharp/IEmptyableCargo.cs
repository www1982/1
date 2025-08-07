using System;

// Token: 0x02000E0B RID: 3595
public interface IEmptyableCargo
{
	// Token: 0x06007174 RID: 29044
	bool CanEmptyCargo();

	// Token: 0x06007175 RID: 29045
	void EmptyCargo();

	// Token: 0x170007C9 RID: 1993
	// (get) Token: 0x06007176 RID: 29046
	IStateMachineTarget master { get; }

	// Token: 0x170007CA RID: 1994
	// (get) Token: 0x06007177 RID: 29047
	bool CanAutoDeploy { get; }

	// Token: 0x170007CB RID: 1995
	// (get) Token: 0x06007178 RID: 29048
	// (set) Token: 0x06007179 RID: 29049
	bool AutoDeploy { get; set; }

	// Token: 0x170007CC RID: 1996
	// (get) Token: 0x0600717A RID: 29050
	bool ChooseDuplicant { get; }

	// Token: 0x170007CD RID: 1997
	// (get) Token: 0x0600717B RID: 29051
	bool ModuleDeployed { get; }

	// Token: 0x170007CE RID: 1998
	// (get) Token: 0x0600717C RID: 29052
	// (set) Token: 0x0600717D RID: 29053
	MinionIdentity ChosenDuplicant { get; set; }
}
