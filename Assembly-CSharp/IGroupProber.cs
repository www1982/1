using System;
using System.Collections.Generic;

// Token: 0x020009CB RID: 2507
public interface IGroupProber
{
	// Token: 0x0600493F RID: 18751
	void Occupy(object prober, short serial_no, IEnumerable<int> cells);

	// Token: 0x06004940 RID: 18752
	void SetValidSerialNos(object prober, short previous_serial_no, short serial_no);

	// Token: 0x06004941 RID: 18753
	bool ReleaseProber(object prober);
}
