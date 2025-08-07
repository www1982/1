using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C9A RID: 3226
public class CodexElementCategoryList : CodexCollapsibleHeader
{
	// Token: 0x1700073F RID: 1855
	// (get) Token: 0x06006327 RID: 25383 RVA: 0x00253E4A File Offset: 0x0025204A
	// (set) Token: 0x06006328 RID: 25384 RVA: 0x00253E52 File Offset: 0x00252052
	public Tag categoryTag { get; set; }

	// Token: 0x06006329 RID: 25385 RVA: 0x00253E5B File Offset: 0x0025205B
	public CodexElementCategoryList()
		: base(UI.CODEX.CATEGORYNAMES.ELEMENTS, null)
	{
	}

	// Token: 0x0600632A RID: 25386 RVA: 0x00253E7C File Offset: 0x0025207C
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		HierarchyReferences component = contentGameObject.GetComponent<HierarchyReferences>();
		base.ContentsGameObject = component.GetReference<RectTransform>("ContentContainer").gameObject;
		base.Configure(contentGameObject, displayPane, textStyles);
		Component reference = component.GetReference<RectTransform>("HeaderLabel");
		RectTransform reference2 = component.GetReference<RectTransform>("PrefabLabelWithIcon");
		this.ClearPanel(reference2.transform.parent, reference2);
		reference.GetComponent<LocText>().SetText(UI.CODEX.CATEGORYNAMES.ELEMENTS);
		foreach (GameObject gameObject in Assets.GetPrefabsWithTag(this.categoryTag))
		{
			GameObject gameObject2 = Util.KInstantiateUI(reference2.gameObject, reference2.parent.gameObject, true);
			Image componentInChildren = gameObject2.GetComponentInChildren<Image>();
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(gameObject, "ui", false);
			componentInChildren.sprite = uisprite.first;
			componentInChildren.color = uisprite.second;
			gameObject2.GetComponentInChildren<LocText>().SetText(gameObject.GetProperName());
			this.rows.Add(gameObject2);
		}
	}

	// Token: 0x0600632B RID: 25387 RVA: 0x00253F98 File Offset: 0x00252198
	private void ClearPanel(Transform containerToClear, Transform skipDestroyingPrefab)
	{
		skipDestroyingPrefab.SetAsFirstSibling();
		for (int i = containerToClear.childCount - 1; i >= 1; i--)
		{
			global::UnityEngine.Object.Destroy(containerToClear.GetChild(i).gameObject);
		}
		for (int j = this.rows.Count - 1; j >= 0; j--)
		{
			global::UnityEngine.Object.Destroy(this.rows[j].gameObject);
		}
		this.rows.Clear();
	}

	// Token: 0x0400430C RID: 17164
	private List<GameObject> rows = new List<GameObject>();
}
