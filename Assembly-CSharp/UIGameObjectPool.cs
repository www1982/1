using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C06 RID: 3078
public class UIGameObjectPool
{
	// Token: 0x170006D7 RID: 1751
	// (get) Token: 0x06005CD6 RID: 23766 RVA: 0x0021D9C9 File Offset: 0x0021BBC9
	public int ActiveElementsCount
	{
		get
		{
			return this.activeElements.Count;
		}
	}

	// Token: 0x170006D8 RID: 1752
	// (get) Token: 0x06005CD7 RID: 23767 RVA: 0x0021D9D6 File Offset: 0x0021BBD6
	public int FreeElementsCount
	{
		get
		{
			return this.freeElements.Count;
		}
	}

	// Token: 0x170006D9 RID: 1753
	// (get) Token: 0x06005CD8 RID: 23768 RVA: 0x0021D9E3 File Offset: 0x0021BBE3
	public int TotalElementsCount
	{
		get
		{
			return this.ActiveElementsCount + this.FreeElementsCount;
		}
	}

	// Token: 0x06005CD9 RID: 23769 RVA: 0x0021D9F2 File Offset: 0x0021BBF2
	public UIGameObjectPool(GameObject prefab)
	{
		this.prefab = prefab;
		this.freeElements = new List<GameObject>();
		this.activeElements = new List<GameObject>();
	}

	// Token: 0x06005CDA RID: 23770 RVA: 0x0021DA30 File Offset: 0x0021BC30
	public GameObject GetFreeElement(GameObject instantiateParent = null, bool forceActive = false)
	{
		if (this.freeElements.Count == 0)
		{
			this.activeElements.Add(Util.KInstantiateUI(this.prefab.gameObject, instantiateParent, false));
		}
		else
		{
			GameObject gameObject = this.freeElements[0];
			this.activeElements.Add(gameObject);
			if (gameObject.transform.parent != instantiateParent)
			{
				gameObject.transform.SetParent(instantiateParent.transform);
			}
			this.freeElements.RemoveAt(0);
		}
		GameObject gameObject2 = this.activeElements[this.activeElements.Count - 1];
		if (gameObject2.gameObject.activeInHierarchy != forceActive)
		{
			gameObject2.gameObject.SetActive(forceActive);
		}
		return gameObject2;
	}

	// Token: 0x06005CDB RID: 23771 RVA: 0x0021DAE8 File Offset: 0x0021BCE8
	public void ClearElement(GameObject element)
	{
		if (!this.activeElements.Contains(element))
		{
			object obj = (this.freeElements.Contains(element) ? (element.name + ": The element provided is already inactive") : (element.name + ": The element provided does not belong to this pool"));
			element.SetActive(false);
			if (this.disabledElementParent != null)
			{
				element.transform.SetParent(this.disabledElementParent);
			}
			global::Debug.LogError(obj);
			return;
		}
		if (this.disabledElementParent != null)
		{
			element.transform.SetParent(this.disabledElementParent);
		}
		element.SetActive(false);
		this.freeElements.Add(element);
		this.activeElements.Remove(element);
	}

	// Token: 0x06005CDC RID: 23772 RVA: 0x0021DBA0 File Offset: 0x0021BDA0
	public void ClearAll()
	{
		while (this.activeElements.Count > 0)
		{
			if (this.disabledElementParent != null)
			{
				this.activeElements[0].transform.SetParent(this.disabledElementParent);
			}
			this.activeElements[0].SetActive(false);
			this.freeElements.Add(this.activeElements[0]);
			this.activeElements.RemoveAt(0);
		}
	}

	// Token: 0x06005CDD RID: 23773 RVA: 0x0021DC1C File Offset: 0x0021BE1C
	public void DestroyAll()
	{
		this.DestroyAllActive();
		this.DestroyAllFree();
	}

	// Token: 0x06005CDE RID: 23774 RVA: 0x0021DC2A File Offset: 0x0021BE2A
	public void DestroyAllActive()
	{
		this.activeElements.ForEach(delegate(GameObject ae)
		{
			global::UnityEngine.Object.Destroy(ae);
		});
		this.activeElements.Clear();
	}

	// Token: 0x06005CDF RID: 23775 RVA: 0x0021DC61 File Offset: 0x0021BE61
	public void DestroyAllFree()
	{
		this.freeElements.ForEach(delegate(GameObject ae)
		{
			global::UnityEngine.Object.Destroy(ae);
		});
		this.freeElements.Clear();
	}

	// Token: 0x06005CE0 RID: 23776 RVA: 0x0021DC98 File Offset: 0x0021BE98
	public void ForEachActiveElement(Action<GameObject> predicate)
	{
		this.activeElements.ForEach(predicate);
	}

	// Token: 0x06005CE1 RID: 23777 RVA: 0x0021DCA6 File Offset: 0x0021BEA6
	public void ForEachFreeElement(Action<GameObject> predicate)
	{
		this.freeElements.ForEach(predicate);
	}

	// Token: 0x04003DC6 RID: 15814
	private GameObject prefab;

	// Token: 0x04003DC7 RID: 15815
	private List<GameObject> freeElements = new List<GameObject>();

	// Token: 0x04003DC8 RID: 15816
	private List<GameObject> activeElements = new List<GameObject>();

	// Token: 0x04003DC9 RID: 15817
	public Transform disabledElementParent;
}
