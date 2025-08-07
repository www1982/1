using System;
using UnityEngine;

// Token: 0x02000ACB RID: 2763
[AddComponentMenu("KMonoBehaviour/scripts/Reservable")]
public class Reservable : KMonoBehaviour
{
	// Token: 0x1700059C RID: 1436
	// (get) Token: 0x06005051 RID: 20561 RVA: 0x001D1129 File Offset: 0x001CF329
	public GameObject ReservedBy
	{
		get
		{
			return this.reservedBy;
		}
	}

	// Token: 0x1700059D RID: 1437
	// (get) Token: 0x06005052 RID: 20562 RVA: 0x001D1131 File Offset: 0x001CF331
	public bool isReserved
	{
		get
		{
			return !(this.reservedBy == null);
		}
	}

	// Token: 0x06005053 RID: 20563 RVA: 0x001D1142 File Offset: 0x001CF342
	public bool Reserve(GameObject reserver)
	{
		if (this.reservedBy == null)
		{
			this.reservedBy = reserver;
			return true;
		}
		return false;
	}

	// Token: 0x06005054 RID: 20564 RVA: 0x001D115C File Offset: 0x001CF35C
	public void ClearReservation(GameObject reserver)
	{
		if (this.reservedBy == reserver)
		{
			this.reservedBy = null;
		}
	}

	// Token: 0x0400360F RID: 13839
	private GameObject reservedBy;
}
