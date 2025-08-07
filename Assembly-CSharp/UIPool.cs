using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C07 RID: 3079
public class UIPool<T> where T : MonoBehaviour
{
	// Token: 0x170006DA RID: 1754
	// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x0021DCB4 File Offset: 0x0021BEB4
	public int ActiveElementsCount
	{
		get
		{
			return this.activeElements.Count;
		}
	}

	// Token: 0x170006DB RID: 1755
	// (get) Token: 0x06005CE3 RID: 23779 RVA: 0x0021DCC1 File Offset: 0x0021BEC1
	public int FreeElementsCount
	{
		get
		{
			return this.freeElements.Count;
		}
	}

	// Token: 0x170006DC RID: 1756
	// (get) Token: 0x06005CE4 RID: 23780 RVA: 0x0021DCCE File Offset: 0x0021BECE
	public int TotalElementsCount
	{
		get
		{
			return this.ActiveElementsCount + this.FreeElementsCount;
		}
	}

	// Token: 0x06005CE5 RID: 23781 RVA: 0x0021DCDD File Offset: 0x0021BEDD
	public UIPool(T prefab)
	{
		this.prefab = prefab;
		this.freeElements = new List<T>();
		this.activeElements = new List<T>();
	}

	// Token: 0x06005CE6 RID: 23782 RVA: 0x0021DD18 File Offset: 0x0021BF18
	public T GetFreeElement(GameObject instantiateParent = null, bool forceActive = false)
	{
		if (this.freeElements.Count == 0)
		{
			this.activeElements.Add(Util.KInstantiateUI<T>(this.prefab.gameObject, instantiateParent, false));
		}
		else
		{
			T t = this.freeElements[0];
			this.activeElements.Add(t);
			if (t.transform.parent != instantiateParent)
			{
				t.transform.SetParent(instantiateParent.transform);
			}
			this.freeElements.RemoveAt(0);
		}
		T t2 = this.activeElements[this.activeElements.Count - 1];
		if (t2.gameObject.activeInHierarchy != forceActive)
		{
			t2.gameObject.SetActive(forceActive);
		}
		return t2;
	}

	// Token: 0x06005CE7 RID: 23783 RVA: 0x0021DDE8 File Offset: 0x0021BFE8
	public void ClearElement(T element)
	{
		if (!this.activeElements.Contains(element))
		{
			global::Debug.LogError(this.freeElements.Contains(element) ? "The element provided is already inactive" : "The element provided does not belong to this pool");
			return;
		}
		if (this.disabledElementParent != null)
		{
			element.gameObject.transform.SetParent(this.disabledElementParent);
		}
		element.gameObject.SetActive(false);
		this.freeElements.Add(element);
		this.activeElements.Remove(element);
	}

	// Token: 0x06005CE8 RID: 23784 RVA: 0x0021DE78 File Offset: 0x0021C078
	public void ClearAll()
	{
		while (this.activeElements.Count > 0)
		{
			if (this.disabledElementParent != null)
			{
				this.activeElements[0].gameObject.transform.SetParent(this.disabledElementParent);
			}
			this.activeElements[0].gameObject.SetActive(false);
			this.freeElements.Add(this.activeElements[0]);
			this.activeElements.RemoveAt(0);
		}
	}

	// Token: 0x06005CE9 RID: 23785 RVA: 0x0021DF0B File Offset: 0x0021C10B
	public void DestroyAll()
	{
		this.DestroyAllActive();
		this.DestroyAllFree();
	}

	// Token: 0x06005CEA RID: 23786 RVA: 0x0021DF19 File Offset: 0x0021C119
	public void DestroyAllActive()
	{
		this.activeElements.ForEach(delegate(T ae)
		{
			global::UnityEngine.Object.Destroy(ae.gameObject);
		});
		this.activeElements.Clear();
	}

	// Token: 0x06005CEB RID: 23787 RVA: 0x0021DF50 File Offset: 0x0021C150
	public void DestroyAllFree()
	{
		this.freeElements.ForEach(delegate(T ae)
		{
			global::UnityEngine.Object.Destroy(ae.gameObject);
		});
		this.freeElements.Clear();
	}

	// Token: 0x06005CEC RID: 23788 RVA: 0x0021DF87 File Offset: 0x0021C187
	public void ForEachActiveElement(Action<T> predicate)
	{
		this.activeElements.ForEach(predicate);
	}

	// Token: 0x06005CED RID: 23789 RVA: 0x0021DF95 File Offset: 0x0021C195
	public void ForEachFreeElement(Action<T> predicate)
	{
		this.freeElements.ForEach(predicate);
	}

	// Token: 0x04003DCA RID: 15818
	private T prefab;

	// Token: 0x04003DCB RID: 15819
	private List<T> freeElements = new List<T>();

	// Token: 0x04003DCC RID: 15820
	private List<T> activeElements = new List<T>();

	// Token: 0x04003DCD RID: 15821
	public Transform disabledElementParent;
}
