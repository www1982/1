using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000DC2 RID: 3522
[AddComponentMenu("KMonoBehaviour/scripts/ScheduledUIInstantiation")]
public class ScheduledUIInstantiation : KMonoBehaviour
{
	// Token: 0x06006F23 RID: 28451 RVA: 0x002A4FC2 File Offset: 0x002A31C2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.InstantiateOnAwake)
		{
			this.InstantiateElements(null);
			return;
		}
		Game.Instance.Subscribe((int)this.InstantiationEvent, new Action<object>(this.InstantiateElements));
	}

	// Token: 0x06006F24 RID: 28452 RVA: 0x002A4FF8 File Offset: 0x002A31F8
	public void InstantiateElements(object data)
	{
		if (this.completed)
		{
			return;
		}
		this.completed = true;
		foreach (ScheduledUIInstantiation.Instantiation instantiation in this.UIElements)
		{
			if (instantiation.RequiredDlcId.IsNullOrWhiteSpace() || Game.IsDlcActiveForCurrentSave(instantiation.RequiredDlcId))
			{
				foreach (GameObject gameObject in instantiation.prefabs)
				{
					Vector3 vector = gameObject.rectTransform().anchoredPosition;
					GameObject gameObject2 = Util.KInstantiateUI(gameObject, instantiation.parent.gameObject, false);
					gameObject2.rectTransform().anchoredPosition = vector;
					gameObject2.rectTransform().localScale = Vector3.one;
					this.instantiatedObjects.Add(gameObject2);
				}
			}
		}
		if (!this.InstantiateOnAwake)
		{
			base.Unsubscribe((int)this.InstantiationEvent, new Action<object>(this.InstantiateElements));
		}
	}

	// Token: 0x06006F25 RID: 28453 RVA: 0x002A50E8 File Offset: 0x002A32E8
	public T GetInstantiatedObject<T>() where T : Component
	{
		for (int i = 0; i < this.instantiatedObjects.Count; i++)
		{
			if (this.instantiatedObjects[i].GetComponent(typeof(T)) != null)
			{
				return this.instantiatedObjects[i].GetComponent(typeof(T)) as T;
			}
		}
		return default(T);
	}

	// Token: 0x04004C6A RID: 19562
	public ScheduledUIInstantiation.Instantiation[] UIElements;

	// Token: 0x04004C6B RID: 19563
	public bool InstantiateOnAwake;

	// Token: 0x04004C6C RID: 19564
	public GameHashes InstantiationEvent = GameHashes.StartGameUser;

	// Token: 0x04004C6D RID: 19565
	private bool completed;

	// Token: 0x04004C6E RID: 19566
	private List<GameObject> instantiatedObjects = new List<GameObject>();

	// Token: 0x02001FDD RID: 8157
	[Serializable]
	public struct Instantiation
	{
		// Token: 0x0400926A RID: 37482
		public string Name;

		// Token: 0x0400926B RID: 37483
		public string Comment;

		// Token: 0x0400926C RID: 37484
		public GameObject[] prefabs;

		// Token: 0x0400926D RID: 37485
		public Transform parent;

		// Token: 0x0400926E RID: 37486
		public string RequiredDlcId;
	}
}
