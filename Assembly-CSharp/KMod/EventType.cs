using System;

namespace KMod
{
	// Token: 0x02000F7A RID: 3962
	public enum EventType
	{
		// Token: 0x04005B33 RID: 23347
		LoadError,
		// Token: 0x04005B34 RID: 23348
		NotFound,
		// Token: 0x04005B35 RID: 23349
		InstallInfoInaccessible,
		// Token: 0x04005B36 RID: 23350
		OutOfOrder,
		// Token: 0x04005B37 RID: 23351
		ExpectedActive,
		// Token: 0x04005B38 RID: 23352
		ExpectedInactive,
		// Token: 0x04005B39 RID: 23353
		ActiveDuringCrash,
		// Token: 0x04005B3A RID: 23354
		InstallFailed,
		// Token: 0x04005B3B RID: 23355
		Installed,
		// Token: 0x04005B3C RID: 23356
		Uninstalled,
		// Token: 0x04005B3D RID: 23357
		VersionUpdate,
		// Token: 0x04005B3E RID: 23358
		AvailableContentChanged,
		// Token: 0x04005B3F RID: 23359
		RestartRequested,
		// Token: 0x04005B40 RID: 23360
		BadWorldGen,
		// Token: 0x04005B41 RID: 23361
		Deactivated,
		// Token: 0x04005B42 RID: 23362
		DisabledEarlyAccess,
		// Token: 0x04005B43 RID: 23363
		DownloadFailed
	}
}
