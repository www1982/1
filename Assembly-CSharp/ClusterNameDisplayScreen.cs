using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C79 RID: 3193
public class ClusterNameDisplayScreen : KScreen
{
	// Token: 0x060061A9 RID: 25001 RVA: 0x0024484F File Offset: 0x00242A4F
	public static void DestroyInstance()
	{
		ClusterNameDisplayScreen.Instance = null;
	}

	// Token: 0x060061AA RID: 25002 RVA: 0x00244857 File Offset: 0x00242A57
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ClusterNameDisplayScreen.Instance = this;
	}

	// Token: 0x060061AB RID: 25003 RVA: 0x00244865 File Offset: 0x00242A65
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060061AC RID: 25004 RVA: 0x00244870 File Offset: 0x00242A70
	public void AddNewEntry(ClusterGridEntity representedObject)
	{
		if (this.GetEntry(representedObject) != null)
		{
			return;
		}
		ClusterNameDisplayScreen.Entry entry = new ClusterNameDisplayScreen.Entry();
		entry.grid_entity = representedObject;
		GameObject gameObject = Util.KInstantiateUI(this.nameAndBarsPrefab, base.gameObject, true);
		entry.display_go = gameObject;
		gameObject.name = representedObject.name + " cluster overlay";
		entry.Name = representedObject.name;
		entry.refs = gameObject.GetComponent<HierarchyReferences>();
		entry.bars_go = entry.refs.GetReference<RectTransform>("Bars").gameObject;
		this.m_entries.Add(entry);
		if (representedObject.GetComponent<KSelectable>() != null)
		{
			this.UpdateName(representedObject);
			this.UpdateBars(representedObject);
		}
	}

	// Token: 0x060061AD RID: 25005 RVA: 0x00244920 File Offset: 0x00242B20
	private void LateUpdate()
	{
		if (App.isLoading || App.IsExiting)
		{
			return;
		}
		int num = this.m_entries.Count;
		int i = 0;
		while (i < num)
		{
			if (this.m_entries[i].grid_entity != null && ClusterMapScreen.GetRevealLevel(this.m_entries[i].grid_entity) == ClusterRevealLevel.Visible)
			{
				Transform gridEntityNameTarget = ClusterMapScreen.Instance.GetGridEntityNameTarget(this.m_entries[i].grid_entity);
				if (gridEntityNameTarget != null)
				{
					Vector3 position = gridEntityNameTarget.GetPosition();
					this.m_entries[i].display_go.GetComponent<RectTransform>().SetPositionAndRotation(position, Quaternion.identity);
					this.m_entries[i].display_go.SetActive(this.m_entries[i].grid_entity.IsVisible && this.m_entries[i].grid_entity.ShowName());
				}
				else if (this.m_entries[i].display_go.activeSelf)
				{
					this.m_entries[i].display_go.SetActive(false);
				}
				this.UpdateBars(this.m_entries[i].grid_entity);
				if (this.m_entries[i].bars_go != null)
				{
					this.m_entries[i].bars_go.GetComponentsInChildren<KCollider2D>(false, this.workingList);
					foreach (KCollider2D kcollider2D in this.workingList)
					{
						kcollider2D.MarkDirty(false);
					}
				}
				i++;
			}
			else
			{
				global::UnityEngine.Object.Destroy(this.m_entries[i].display_go);
				num--;
				this.m_entries[i] = this.m_entries[num];
			}
		}
		this.m_entries.RemoveRange(num, this.m_entries.Count - num);
	}

	// Token: 0x060061AE RID: 25006 RVA: 0x00244B38 File Offset: 0x00242D38
	public void UpdateName(ClusterGridEntity representedObject)
	{
		ClusterNameDisplayScreen.Entry entry = this.GetEntry(representedObject);
		if (entry == null)
		{
			return;
		}
		KSelectable component = representedObject.GetComponent<KSelectable>();
		entry.display_go.name = component.GetProperName() + " cluster overlay";
		LocText componentInChildren = entry.display_go.GetComponentInChildren<LocText>();
		if (componentInChildren != null)
		{
			componentInChildren.text = component.GetProperName();
		}
	}

	// Token: 0x060061AF RID: 25007 RVA: 0x00244B94 File Offset: 0x00242D94
	private void UpdateBars(ClusterGridEntity representedObject)
	{
		ClusterNameDisplayScreen.Entry entry = this.GetEntry(representedObject);
		if (entry == null)
		{
			return;
		}
		GenericUIProgressBar componentInChildren = entry.bars_go.GetComponentInChildren<GenericUIProgressBar>(true);
		if (entry.grid_entity.ShowProgressBar())
		{
			if (!componentInChildren.gameObject.activeSelf)
			{
				componentInChildren.gameObject.SetActive(true);
			}
			componentInChildren.SetFillPercentage(entry.grid_entity.GetProgress());
			return;
		}
		if (componentInChildren.gameObject.activeSelf)
		{
			componentInChildren.gameObject.SetActive(false);
		}
	}

	// Token: 0x060061B0 RID: 25008 RVA: 0x00244C0C File Offset: 0x00242E0C
	private ClusterNameDisplayScreen.Entry GetEntry(ClusterGridEntity entity)
	{
		return this.m_entries.Find((ClusterNameDisplayScreen.Entry entry) => entry.grid_entity == entity);
	}

	// Token: 0x04004243 RID: 16963
	public static ClusterNameDisplayScreen Instance;

	// Token: 0x04004244 RID: 16964
	public GameObject nameAndBarsPrefab;

	// Token: 0x04004245 RID: 16965
	[SerializeField]
	private Color selectedColor;

	// Token: 0x04004246 RID: 16966
	[SerializeField]
	private Color defaultColor;

	// Token: 0x04004247 RID: 16967
	private List<ClusterNameDisplayScreen.Entry> m_entries = new List<ClusterNameDisplayScreen.Entry>();

	// Token: 0x04004248 RID: 16968
	private List<KCollider2D> workingList = new List<KCollider2D>();

	// Token: 0x02001E3D RID: 7741
	private class Entry
	{
		// Token: 0x04008CFD RID: 36093
		public string Name;

		// Token: 0x04008CFE RID: 36094
		public ClusterGridEntity grid_entity;

		// Token: 0x04008CFF RID: 36095
		public GameObject display_go;

		// Token: 0x04008D00 RID: 36096
		public GameObject bars_go;

		// Token: 0x04008D01 RID: 36097
		public HierarchyReferences refs;
	}
}
