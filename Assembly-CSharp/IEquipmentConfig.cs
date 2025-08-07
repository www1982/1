using System;
using UnityEngine;

// Token: 0x020008F4 RID: 2292
public interface IEquipmentConfig
{
	// Token: 0x06004007 RID: 16391
	EquipmentDef CreateEquipmentDef();

	// Token: 0x06004008 RID: 16392
	void DoPostConfigure(GameObject go);

	// Token: 0x06004009 RID: 16393 RVA: 0x00167A3B File Offset: 0x00165C3B
	[Obsolete("Use IHasDlcRestrictions instead")]
	string[] GetDlcIds()
	{
		return null;
	}
}
