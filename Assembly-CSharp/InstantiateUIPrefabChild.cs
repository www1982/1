using System;
using UnityEngine;

// Token: 0x02000CF1 RID: 3313
[AddComponentMenu("KMonoBehaviour/scripts/InstantiateUIPrefabChild")]
public class InstantiateUIPrefabChild : KMonoBehaviour
{
	// Token: 0x060065F7 RID: 26103 RVA: 0x00265BB7 File Offset: 0x00263DB7
	protected override void OnPrefabInit()
	{
		if (this.InstantiateOnAwake)
		{
			this.Instantiate();
		}
	}

	// Token: 0x060065F8 RID: 26104 RVA: 0x00265BC8 File Offset: 0x00263DC8
	public void Instantiate()
	{
		if (this.alreadyInstantiated)
		{
			global::Debug.LogWarning(base.gameObject.name + "trying to instantiate UI prefabs multiple times.");
			return;
		}
		this.alreadyInstantiated = true;
		foreach (GameObject gameObject in this.prefabs)
		{
			if (!(gameObject == null))
			{
				Vector3 vector = gameObject.rectTransform().anchoredPosition;
				GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.transform.SetParent(base.transform);
				gameObject2.rectTransform().anchoredPosition = vector;
				gameObject2.rectTransform().localScale = Vector3.one;
				if (this.setAsFirstSibling)
				{
					gameObject2.transform.SetAsFirstSibling();
				}
			}
		}
	}

	// Token: 0x040045DA RID: 17882
	public GameObject[] prefabs;

	// Token: 0x040045DB RID: 17883
	public bool InstantiateOnAwake = true;

	// Token: 0x040045DC RID: 17884
	private bool alreadyInstantiated;

	// Token: 0x040045DD RID: 17885
	public bool setAsFirstSibling;
}
