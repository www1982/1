using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000461 RID: 1121
public class UIPrefabLocalPool
{
	// Token: 0x0600177B RID: 6011 RVA: 0x00082C80 File Offset: 0x00080E80
	public UIPrefabLocalPool(GameObject sourcePrefab, GameObject parent)
	{
		this.sourcePrefab = sourcePrefab;
		this.parent = parent;
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x00082CAC File Offset: 0x00080EAC
	public GameObject Borrow()
	{
		GameObject gameObject;
		if (this.availableInstances.Count == 0)
		{
			gameObject = Util.KInstantiateUI(this.sourcePrefab, this.parent, true);
		}
		else
		{
			gameObject = this.availableInstances.First<KeyValuePair<int, GameObject>>().Value;
			this.availableInstances.Remove(gameObject.GetInstanceID());
		}
		this.checkedOutInstances.Add(gameObject.GetInstanceID(), gameObject);
		gameObject.SetActive(true);
		gameObject.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x00082D26 File Offset: 0x00080F26
	public void Return(GameObject instance)
	{
		this.checkedOutInstances.Remove(instance.GetInstanceID());
		this.availableInstances.Add(instance.GetInstanceID(), instance);
		instance.SetActive(false);
	}

	// Token: 0x0600177E RID: 6014 RVA: 0x00082D54 File Offset: 0x00080F54
	public void ReturnAll()
	{
		foreach (KeyValuePair<int, GameObject> keyValuePair in this.checkedOutInstances)
		{
			int num;
			GameObject gameObject;
			keyValuePair.Deconstruct(out num, out gameObject);
			int num2 = num;
			GameObject gameObject2 = gameObject;
			this.availableInstances.Add(num2, gameObject2);
			gameObject2.SetActive(false);
		}
		this.checkedOutInstances.Clear();
	}

	// Token: 0x0600177F RID: 6015 RVA: 0x00082DD0 File Offset: 0x00080FD0
	public IEnumerable<GameObject> GetBorrowedObjects()
	{
		return this.checkedOutInstances.Values;
	}

	// Token: 0x04000DA3 RID: 3491
	public readonly GameObject sourcePrefab;

	// Token: 0x04000DA4 RID: 3492
	public readonly GameObject parent;

	// Token: 0x04000DA5 RID: 3493
	private Dictionary<int, GameObject> checkedOutInstances = new Dictionary<int, GameObject>();

	// Token: 0x04000DA6 RID: 3494
	private Dictionary<int, GameObject> availableInstances = new Dictionary<int, GameObject>();
}
