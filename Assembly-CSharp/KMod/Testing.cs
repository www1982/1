using System;

namespace KMod
{
	// Token: 0x02000F67 RID: 3943
	public static class Testing
	{
		// Token: 0x04005ADA RID: 23258
		public static Testing.DLLLoading dll_loading;

		// Token: 0x04005ADB RID: 23259
		public const Testing.SaveLoad SAVE_LOAD = Testing.SaveLoad.NoTesting;

		// Token: 0x04005ADC RID: 23260
		public const Testing.Install INSTALL = Testing.Install.NoTesting;

		// Token: 0x04005ADD RID: 23261
		public const Testing.Boot BOOT = Testing.Boot.NoTesting;

		// Token: 0x02002100 RID: 8448
		public enum DLLLoading
		{
			// Token: 0x04009708 RID: 38664
			NoTesting,
			// Token: 0x04009709 RID: 38665
			Fail,
			// Token: 0x0400970A RID: 38666
			UseModLoaderDLLExclusively
		}

		// Token: 0x02002101 RID: 8449
		public enum SaveLoad
		{
			// Token: 0x0400970C RID: 38668
			NoTesting,
			// Token: 0x0400970D RID: 38669
			FailSave,
			// Token: 0x0400970E RID: 38670
			FailLoad
		}

		// Token: 0x02002102 RID: 8450
		public enum Install
		{
			// Token: 0x04009710 RID: 38672
			NoTesting,
			// Token: 0x04009711 RID: 38673
			ForceUninstall,
			// Token: 0x04009712 RID: 38674
			ForceReinstall,
			// Token: 0x04009713 RID: 38675
			ForceUpdate
		}

		// Token: 0x02002103 RID: 8451
		public enum Boot
		{
			// Token: 0x04009715 RID: 38677
			NoTesting,
			// Token: 0x04009716 RID: 38678
			Crash
		}
	}
}
