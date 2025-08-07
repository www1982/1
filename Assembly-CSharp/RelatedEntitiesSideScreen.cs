using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E22 RID: 3618
public class RelatedEntitiesSideScreen : SideScreenContent, ISim1000ms
{
	// Token: 0x0600726E RID: 29294 RVA: 0x002B80A0 File Offset: 0x002B62A0
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		this.rowPrefab.SetActive(false);
		if (show)
		{
			this.RefreshOptions(null);
		}
	}

	// Token: 0x0600726F RID: 29295 RVA: 0x002B80BF File Offset: 0x002B62BF
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IRelatedEntities>() != null;
	}

	// Token: 0x06007270 RID: 29296 RVA: 0x002B80CA File Offset: 0x002B62CA
	public override void SetTarget(GameObject target)
	{
		this.target = target;
		this.targetRelatedEntitiesComponent = target.GetComponent<IRelatedEntities>();
		this.RefreshOptions(null);
		this.uiRefreshSubHandle = Game.Instance.Subscribe(1980521255, new Action<object>(this.RefreshOptions));
	}

	// Token: 0x06007271 RID: 29297 RVA: 0x002B8107 File Offset: 0x002B6307
	public override void ClearTarget()
	{
		if (this.uiRefreshSubHandle != -1 && this.targetRelatedEntitiesComponent != null)
		{
			Game.Instance.Unsubscribe(this.uiRefreshSubHandle);
			this.uiRefreshSubHandle = -1;
		}
	}

	// Token: 0x06007272 RID: 29298 RVA: 0x002B8134 File Offset: 0x002B6334
	private void RefreshOptions(object data = null)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.ClearRows();
		foreach (KSelectable kselectable in this.targetRelatedEntitiesComponent.GetRelatedEntities())
		{
			this.AddRow(kselectable);
		}
	}

	// Token: 0x06007273 RID: 29299 RVA: 0x002B81A0 File Offset: 0x002B63A0
	private void ClearRows()
	{
		for (int i = this.rowContainer.childCount - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.rowContainer.GetChild(i));
		}
		this.rows.Clear();
	}

	// Token: 0x06007274 RID: 29300 RVA: 0x002B81E4 File Offset: 0x002B63E4
	private void AddRow(KSelectable entity)
	{
		GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.rowContainer.gameObject, true);
		gameObject.GetComponent<KButton>().onClick += delegate
		{
			SelectTool.Instance.SelectAndFocus(entity.transform.position, entity);
		};
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		component.GetReference<LocText>("label").SetText((SelectTool.Instance.selected == entity) ? ("<b>" + entity.GetProperName() + "</b>") : entity.GetProperName());
		component.GetReference<Image>("icon").sprite = Def.GetUISprite(entity.gameObject, "ui", false).first;
		this.rows.Add(entity, gameObject);
		this.RefreshMainStatus(entity);
	}

	// Token: 0x06007275 RID: 29301 RVA: 0x002B82CC File Offset: 0x002B64CC
	private void RefreshMainStatus(KSelectable entity)
	{
		if (entity.IsNullOrDestroyed())
		{
			return;
		}
		if (!this.rows.ContainsKey(entity))
		{
			return;
		}
		HierarchyReferences component = this.rows[entity].GetComponent<HierarchyReferences>();
		StatusItemGroup.Entry statusItem = entity.GetStatusItem(Db.Get().StatusItemCategories.Main);
		LocText reference = component.GetReference<LocText>("status");
		if (statusItem.data != null)
		{
			reference.gameObject.SetActive(true);
			reference.SetText(statusItem.item.GetName(statusItem.data));
			return;
		}
		reference.gameObject.SetActive(false);
		reference.SetText("");
	}

	// Token: 0x06007276 RID: 29302 RVA: 0x002B8368 File Offset: 0x002B6568
	public void Sim1000ms(float dt)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		foreach (KeyValuePair<KSelectable, GameObject> keyValuePair in this.rows)
		{
			this.RefreshMainStatus(keyValuePair.Key);
		}
	}

	// Token: 0x04004ECE RID: 20174
	private GameObject target;

	// Token: 0x04004ECF RID: 20175
	private IRelatedEntities targetRelatedEntitiesComponent;

	// Token: 0x04004ED0 RID: 20176
	public GameObject rowPrefab;

	// Token: 0x04004ED1 RID: 20177
	public RectTransform rowContainer;

	// Token: 0x04004ED2 RID: 20178
	public Dictionary<KSelectable, GameObject> rows = new Dictionary<KSelectable, GameObject>();

	// Token: 0x04004ED3 RID: 20179
	private int uiRefreshSubHandle = -1;
}
