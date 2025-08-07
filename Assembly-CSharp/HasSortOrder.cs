using System;
using UnityEngine;

// Token: 0x02000CE8 RID: 3304
[AddComponentMenu("KMonoBehaviour/scripts/HasSortOrder")]
public class HasSortOrder : KMonoBehaviour, IHasSortOrder
{
	// Token: 0x17000762 RID: 1890
	// (get) Token: 0x060065AE RID: 26030 RVA: 0x00264A20 File Offset: 0x00262C20
	// (set) Token: 0x060065AF RID: 26031 RVA: 0x00264A28 File Offset: 0x00262C28
	public int sortOrder { get; set; }
}
