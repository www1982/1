using System;
using UnityEngine;

// Token: 0x02000961 RID: 2401
public class CarePackageInfo : ITelepadDeliverable
{
	// Token: 0x060044F5 RID: 17653 RVA: 0x0018D411 File Offset: 0x0018B611
	public CarePackageInfo(string ID, float amount, Func<bool> requirement)
	{
		this.id = ID;
		this.quantity = amount;
		this.requirement = requirement;
	}

	// Token: 0x060044F6 RID: 17654 RVA: 0x0018D42E File Offset: 0x0018B62E
	public CarePackageInfo(string ID, float amount, Func<bool> requirement, string facadeID)
	{
		this.id = ID;
		this.quantity = amount;
		this.requirement = requirement;
		this.facadeID = facadeID;
	}

	// Token: 0x060044F7 RID: 17655 RVA: 0x0018D454 File Offset: 0x0018B654
	public GameObject Deliver(Vector3 location)
	{
		location += Vector3.right / 2f;
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(CarePackageConfig.ID), location);
		gameObject.SetActive(true);
		gameObject.GetComponent<CarePackage>().SetInfo(this);
		return gameObject;
	}

	// Token: 0x04002E24 RID: 11812
	public readonly string id;

	// Token: 0x04002E25 RID: 11813
	public readonly float quantity;

	// Token: 0x04002E26 RID: 11814
	public readonly Func<bool> requirement;

	// Token: 0x04002E27 RID: 11815
	public readonly string facadeID;
}
