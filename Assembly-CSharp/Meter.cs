using System;
using UnityEngine;

// Token: 0x020009C7 RID: 2503
[AddComponentMenu("KMonoBehaviour/scripts/Meter")]
public class Meter : KMonoBehaviour
{
	// Token: 0x020019DB RID: 6619
	public enum Offset
	{
		// Token: 0x04007DE5 RID: 32229
		Infront,
		// Token: 0x04007DE6 RID: 32230
		Behind,
		// Token: 0x04007DE7 RID: 32231
		UserSpecified,
		// Token: 0x04007DE8 RID: 32232
		NoChange
	}
}
