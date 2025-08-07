using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F2 RID: 2290
[AddComponentMenu("KMonoBehaviour/scripts/EntombedItemVisualizer")]
public class EntombedItemVisualizer : KMonoBehaviour
{
	// Token: 0x06003FEF RID: 16367 RVA: 0x00166FD4 File Offset: 0x001651D4
	public void Clear()
	{
		this.cellEntombedCounts.Clear();
	}

	// Token: 0x06003FF0 RID: 16368 RVA: 0x00166FE1 File Offset: 0x001651E1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.entombedItemPool = new GameObjectPool(new Func<GameObject>(this.InstantiateEntombedObject), 32);
	}

	// Token: 0x06003FF1 RID: 16369 RVA: 0x00167004 File Offset: 0x00165204
	public bool AddItem(int cell)
	{
		bool flag = false;
		if (Grid.Objects[cell, 9] == null)
		{
			flag = true;
			EntombedItemVisualizer.Data data;
			this.cellEntombedCounts.TryGetValue(cell, out data);
			if (data.refCount == 0)
			{
				GameObject instance = this.entombedItemPool.GetInstance();
				instance.transform.SetPosition(Grid.CellToPosCCC(cell, Grid.SceneLayer.FXFront));
				instance.transform.rotation = Quaternion.Euler(0f, 0f, global::UnityEngine.Random.value * 360f);
				KBatchedAnimController component = instance.GetComponent<KBatchedAnimController>();
				int num = global::UnityEngine.Random.Range(0, EntombedItemVisualizer.EntombedVisualizerAnims.Length);
				string text = EntombedItemVisualizer.EntombedVisualizerAnims[num];
				component.initialAnim = text;
				instance.SetActive(true);
				component.Play(text, KAnim.PlayMode.Once, 1f, 0f);
				data.controller = component;
			}
			data.refCount++;
			this.cellEntombedCounts[cell] = data;
		}
		return flag;
	}

	// Token: 0x06003FF2 RID: 16370 RVA: 0x001670F4 File Offset: 0x001652F4
	public void RemoveItem(int cell)
	{
		EntombedItemVisualizer.Data data;
		if (this.cellEntombedCounts.TryGetValue(cell, out data))
		{
			data.refCount--;
			if (data.refCount == 0)
			{
				this.ReleaseVisualizer(cell, data);
				return;
			}
			this.cellEntombedCounts[cell] = data;
		}
	}

	// Token: 0x06003FF3 RID: 16371 RVA: 0x0016713C File Offset: 0x0016533C
	public void ForceClear(int cell)
	{
		EntombedItemVisualizer.Data data;
		if (this.cellEntombedCounts.TryGetValue(cell, out data))
		{
			this.ReleaseVisualizer(cell, data);
		}
	}

	// Token: 0x06003FF4 RID: 16372 RVA: 0x00167164 File Offset: 0x00165364
	private void ReleaseVisualizer(int cell, EntombedItemVisualizer.Data data)
	{
		if (data.controller != null)
		{
			data.controller.gameObject.SetActive(false);
			this.entombedItemPool.ReleaseInstance(data.controller.gameObject);
		}
		this.cellEntombedCounts.Remove(cell);
	}

	// Token: 0x06003FF5 RID: 16373 RVA: 0x001671B3 File Offset: 0x001653B3
	public bool IsEntombedItem(int cell)
	{
		return this.cellEntombedCounts.ContainsKey(cell) && this.cellEntombedCounts[cell].refCount > 0;
	}

	// Token: 0x06003FF6 RID: 16374 RVA: 0x001671D9 File Offset: 0x001653D9
	private GameObject InstantiateEntombedObject()
	{
		GameObject gameObject = GameUtil.KInstantiate(this.entombedItemPrefab, Grid.SceneLayer.FXFront, null, 0);
		gameObject.SetActive(false);
		return gameObject;
	}

	// Token: 0x040027AB RID: 10155
	[SerializeField]
	private GameObject entombedItemPrefab;

	// Token: 0x040027AC RID: 10156
	private static readonly string[] EntombedVisualizerAnims = new string[] { "idle1", "idle2", "idle3", "idle4" };

	// Token: 0x040027AD RID: 10157
	private GameObjectPool entombedItemPool;

	// Token: 0x040027AE RID: 10158
	private Dictionary<int, EntombedItemVisualizer.Data> cellEntombedCounts = new Dictionary<int, EntombedItemVisualizer.Data>();

	// Token: 0x0200189C RID: 6300
	private struct Data
	{
		// Token: 0x04007972 RID: 31090
		public int refCount;

		// Token: 0x04007973 RID: 31091
		public KBatchedAnimController controller;
	}
}
