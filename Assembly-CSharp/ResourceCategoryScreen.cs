using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000DAF RID: 3503
public class ResourceCategoryScreen : KScreen
{
	// Token: 0x06006E11 RID: 28177 RVA: 0x0029C8E4 File Offset: 0x0029AAE4
	public static void DestroyInstance()
	{
		ResourceCategoryScreen.Instance = null;
	}

	// Token: 0x06006E12 RID: 28178 RVA: 0x0029C8EC File Offset: 0x0029AAEC
	protected override void OnActivate()
	{
		base.OnActivate();
		ResourceCategoryScreen.Instance = this;
		base.ConsumeMouseScroll = true;
		MultiToggle hiderButton = this.HiderButton;
		hiderButton.onClick = (global::System.Action)Delegate.Combine(hiderButton.onClick, new global::System.Action(this.OnHiderClick));
		this.OnHiderClick();
		this.CreateTagSetHeaders(GameTags.MaterialCategories, GameUtil.MeasureUnit.mass);
		this.CreateTagSetHeaders(GameTags.CalorieCategories, GameUtil.MeasureUnit.kcal);
		this.CreateTagSetHeaders(GameTags.UnitCategories, GameUtil.MeasureUnit.quantity);
		if (!this.DisplayedCategories.ContainsKey(GameTags.Miscellaneous))
		{
			ResourceCategoryHeader resourceCategoryHeader = this.NewCategoryHeader(GameTags.Miscellaneous, GameUtil.MeasureUnit.mass);
			this.DisplayedCategories.Add(GameTags.Miscellaneous, resourceCategoryHeader);
		}
		this.DisplayedCategoryKeys = this.DisplayedCategories.Keys.ToArray<Tag>();
	}

	// Token: 0x06006E13 RID: 28179 RVA: 0x0029C9A4 File Offset: 0x0029ABA4
	private void CreateTagSetHeaders(IEnumerable<Tag> set, GameUtil.MeasureUnit measure)
	{
		foreach (Tag tag in set)
		{
			ResourceCategoryHeader resourceCategoryHeader = this.NewCategoryHeader(tag, measure);
			this.DisplayedCategories.Add(tag, resourceCategoryHeader);
		}
	}

	// Token: 0x06006E14 RID: 28180 RVA: 0x0029C9FC File Offset: 0x0029ABFC
	private void OnHiderClick()
	{
		this.HiderButton.NextState();
		if (this.HiderButton.CurrentState == 0)
		{
			this.targetContentHideHeight = 0f;
			return;
		}
		this.targetContentHideHeight = Mathf.Min(((float)Screen.height - this.maxHeightPadding) / GameScreenManager.Instance.ssOverlayCanvas.GetComponent<KCanvasScaler>().GetCanvasScale(), this.CategoryContainer.rectTransform().rect.height);
	}

	// Token: 0x06006E15 RID: 28181 RVA: 0x0029CA74 File Offset: 0x0029AC74
	private void Update()
	{
		if (ClusterManager.Instance.activeWorld.worldInventory == null)
		{
			return;
		}
		if (this.HideTarget.minHeight != this.targetContentHideHeight)
		{
			float num = this.HideTarget.minHeight;
			float num2 = this.targetContentHideHeight - num;
			num2 = Mathf.Clamp(num2 * this.HideSpeedFactor * Time.unscaledDeltaTime, (num2 > 0f) ? (-num2) : num2, (num2 > 0f) ? num2 : (-num2));
			num += num2;
			this.HideTarget.minHeight = num;
		}
		for (int i = 0; i < 1; i++)
		{
			Tag tag = this.DisplayedCategoryKeys[this.categoryUpdatePacer];
			ResourceCategoryHeader resourceCategoryHeader = this.DisplayedCategories[tag];
			if (DiscoveredResources.Instance.IsDiscovered(tag) && !resourceCategoryHeader.gameObject.activeInHierarchy)
			{
				resourceCategoryHeader.gameObject.SetActive(true);
			}
			resourceCategoryHeader.UpdateContents();
			this.categoryUpdatePacer = (this.categoryUpdatePacer + 1) % this.DisplayedCategoryKeys.Length;
		}
		if (this.HiderButton.CurrentState != 0)
		{
			this.targetContentHideHeight = Mathf.Min(((float)Screen.height - this.maxHeightPadding) / GameScreenManager.Instance.ssOverlayCanvas.GetComponent<KCanvasScaler>().GetCanvasScale(), this.CategoryContainer.rectTransform().rect.height);
		}
		if (MeterScreen.Instance != null && !MeterScreen.Instance.StartValuesSet)
		{
			MeterScreen.Instance.InitializeValues();
		}
	}

	// Token: 0x06006E16 RID: 28182 RVA: 0x0029CBE7 File Offset: 0x0029ADE7
	private ResourceCategoryHeader NewCategoryHeader(Tag categoryTag, GameUtil.MeasureUnit measure)
	{
		GameObject gameObject = Util.KInstantiateUI(this.Prefab_CategoryBar, this.CategoryContainer.gameObject, false);
		gameObject.name = "CategoryHeader_" + categoryTag.Name;
		ResourceCategoryHeader component = gameObject.GetComponent<ResourceCategoryHeader>();
		component.SetTag(categoryTag, measure);
		return component;
	}

	// Token: 0x06006E17 RID: 28183 RVA: 0x0029CC24 File Offset: 0x0029AE24
	public static string QuantityTextForMeasure(float quantity, GameUtil.MeasureUnit measure)
	{
		switch (measure)
		{
		case GameUtil.MeasureUnit.mass:
			return GameUtil.GetFormattedMass(quantity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
		case GameUtil.MeasureUnit.kcal:
			return GameUtil.GetFormattedCalories(quantity, GameUtil.TimeSlice.None, true);
		}
		return quantity.ToString();
	}

	// Token: 0x04004B8A RID: 19338
	public static ResourceCategoryScreen Instance;

	// Token: 0x04004B8B RID: 19339
	public GameObject Prefab_CategoryBar;

	// Token: 0x04004B8C RID: 19340
	public Transform CategoryContainer;

	// Token: 0x04004B8D RID: 19341
	public MultiToggle HiderButton;

	// Token: 0x04004B8E RID: 19342
	public KLayoutElement HideTarget;

	// Token: 0x04004B8F RID: 19343
	private float HideSpeedFactor = 12f;

	// Token: 0x04004B90 RID: 19344
	private float maxHeightPadding = 480f;

	// Token: 0x04004B91 RID: 19345
	private float targetContentHideHeight;

	// Token: 0x04004B92 RID: 19346
	public Dictionary<Tag, ResourceCategoryHeader> DisplayedCategories = new Dictionary<Tag, ResourceCategoryHeader>();

	// Token: 0x04004B93 RID: 19347
	private Tag[] DisplayedCategoryKeys;

	// Token: 0x04004B94 RID: 19348
	private int categoryUpdatePacer;
}
