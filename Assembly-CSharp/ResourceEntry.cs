using System;
using System.Collections;
using System.Collections.Generic;
using Klei;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000DB0 RID: 3504
[AddComponentMenu("KMonoBehaviour/scripts/ResourceEntry")]
public class ResourceEntry : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISim4000ms
{
	// Token: 0x06006E19 RID: 28185 RVA: 0x0029CC84 File Offset: 0x0029AE84
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.QuantityLabel.color = this.AvailableColor;
		this.NameLabel.color = this.AvailableColor;
		this.button.onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06006E1A RID: 28186 RVA: 0x0029CCD5 File Offset: 0x0029AED5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.tooltip.OnToolTip = new Func<string>(this.OnToolTip);
		this.RefreshChart();
	}

	// Token: 0x06006E1B RID: 28187 RVA: 0x0029CCFC File Offset: 0x0029AEFC
	private void OnClick()
	{
		this.lastClickTime = Time.unscaledTime;
		if (this.cachedPickupables == null)
		{
			this.cachedPickupables = ClusterManager.Instance.activeWorld.worldInventory.CreatePickupablesList(this.Resource);
			base.StartCoroutine(this.ClearCachedPickupablesAfterThreshold());
		}
		if (this.cachedPickupables == null)
		{
			return;
		}
		Pickupable pickupable = null;
		for (int i = 0; i < this.cachedPickupables.Count; i++)
		{
			this.selectionIdx++;
			int num = this.selectionIdx % this.cachedPickupables.Count;
			pickupable = this.cachedPickupables[num];
			if (pickupable != null && !pickupable.KPrefabID.HasTag(GameTags.StoredPrivate))
			{
				break;
			}
		}
		if (pickupable != null)
		{
			Transform transform = pickupable.transform;
			if (pickupable.storage != null)
			{
				transform = pickupable.storage.transform;
			}
			SelectTool.Instance.SelectAndFocus(transform.transform.GetPosition(), transform.GetComponent<KSelectable>(), Vector3.zero);
			for (int j = 0; j < this.cachedPickupables.Count; j++)
			{
				Pickupable pickupable2 = this.cachedPickupables[j];
				if (pickupable2 != null)
				{
					KAnimControllerBase component = pickupable2.GetComponent<KAnimControllerBase>();
					if (component != null)
					{
						component.HighlightColour = this.HighlightColor;
					}
				}
			}
		}
	}

	// Token: 0x06006E1C RID: 28188 RVA: 0x0029CE58 File Offset: 0x0029B058
	private IEnumerator ClearCachedPickupablesAfterThreshold()
	{
		while (this.cachedPickupables != null && this.lastClickTime != 0f && Time.unscaledTime - this.lastClickTime < 10f)
		{
			yield return SequenceUtil.WaitForSeconds(1f);
		}
		this.cachedPickupables = null;
		yield break;
	}

	// Token: 0x06006E1D RID: 28189 RVA: 0x0029CE68 File Offset: 0x0029B068
	public void GetAmounts(EdiblesManager.FoodInfo food_info, bool doExtras, out float available, out float total, out float reserved)
	{
		available = ClusterManager.Instance.activeWorld.worldInventory.GetAmount(this.Resource, false);
		total = (doExtras ? ClusterManager.Instance.activeWorld.worldInventory.GetTotalAmount(this.Resource, false) : 0f);
		reserved = (doExtras ? MaterialNeeds.GetAmount(this.Resource, ClusterManager.Instance.activeWorldId, false) : 0f);
		if (food_info != null)
		{
			available *= food_info.CaloriesPerUnit;
			total *= food_info.CaloriesPerUnit;
			reserved *= food_info.CaloriesPerUnit;
		}
	}

	// Token: 0x06006E1E RID: 28190 RVA: 0x0029CF08 File Offset: 0x0029B108
	private void GetAmounts(bool doExtras, out float available, out float total, out float reserved)
	{
		EdiblesManager.FoodInfo foodInfo = ((this.Measure == GameUtil.MeasureUnit.kcal) ? EdiblesManager.GetFoodInfo(this.Resource.Name) : null);
		this.GetAmounts(foodInfo, doExtras, out available, out total, out reserved);
	}

	// Token: 0x06006E1F RID: 28191 RVA: 0x0029CF40 File Offset: 0x0029B140
	public void UpdateValue()
	{
		this.SetName(this.Resource.ProperName());
		bool allowInsufficientMaterialBuild = GenericGameSettings.instance.allowInsufficientMaterialBuild;
		float num;
		float num2;
		float num3;
		this.GetAmounts(allowInsufficientMaterialBuild, out num, out num2, out num3);
		if (this.currentQuantity != num)
		{
			this.currentQuantity = num;
			this.QuantityLabel.text = ResourceCategoryScreen.QuantityTextForMeasure(num, this.Measure);
		}
		Color color = this.AvailableColor;
		if (num3 > num2)
		{
			color = this.OverdrawnColor;
		}
		else if (num == 0f)
		{
			color = this.UnavailableColor;
		}
		if (this.QuantityLabel.color != color)
		{
			this.QuantityLabel.color = color;
		}
		if (this.NameLabel.color != color)
		{
			this.NameLabel.color = color;
		}
	}

	// Token: 0x06006E20 RID: 28192 RVA: 0x0029D008 File Offset: 0x0029B208
	private string OnToolTip()
	{
		float num;
		float num2;
		float num3;
		this.GetAmounts(true, out num, out num2, out num3);
		string text = this.NameLabel.text + "\n";
		text += string.Format(UI.RESOURCESCREEN.AVAILABLE_TOOLTIP, ResourceCategoryScreen.QuantityTextForMeasure(num, this.Measure), ResourceCategoryScreen.QuantityTextForMeasure(num3, this.Measure), ResourceCategoryScreen.QuantityTextForMeasure(num2, this.Measure));
		float delta = TrackerTool.Instance.GetResourceStatistic(ClusterManager.Instance.activeWorldId, this.Resource).GetDelta(150f);
		if (delta != 0f)
		{
			text = text + "\n\n" + string.Format(UI.RESOURCESCREEN.TREND_TOOLTIP, (delta > 0f) ? UI.RESOURCESCREEN.INCREASING_STR : UI.RESOURCESCREEN.DECREASING_STR, GameUtil.GetFormattedMass(Mathf.Abs(delta), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		}
		else
		{
			text = text + "\n\n" + UI.RESOURCESCREEN.TREND_TOOLTIP_NO_CHANGE;
		}
		return text;
	}

	// Token: 0x06006E21 RID: 28193 RVA: 0x0029D0FE File Offset: 0x0029B2FE
	public void SetName(string name)
	{
		this.NameLabel.text = name;
	}

	// Token: 0x06006E22 RID: 28194 RVA: 0x0029D10C File Offset: 0x0029B30C
	public void SetTag(Tag t, GameUtil.MeasureUnit measure)
	{
		this.Resource = t;
		this.Measure = measure;
		this.cachedPickupables = null;
	}

	// Token: 0x06006E23 RID: 28195 RVA: 0x0029D124 File Offset: 0x0029B324
	private void Hover(bool is_hovering)
	{
		if (ClusterManager.Instance.activeWorld.worldInventory == null)
		{
			return;
		}
		if (is_hovering)
		{
			this.Background.color = this.BackgroundHoverColor;
		}
		else
		{
			this.Background.color = new Color(0f, 0f, 0f, 0f);
		}
		ICollection<Pickupable> pickupables = ClusterManager.Instance.activeWorld.worldInventory.GetPickupables(this.Resource, false);
		if (pickupables == null)
		{
			return;
		}
		foreach (Pickupable pickupable in pickupables)
		{
			if (!(pickupable == null))
			{
				KAnimControllerBase component = pickupable.GetComponent<KAnimControllerBase>();
				if (!(component == null))
				{
					if (is_hovering)
					{
						component.HighlightColour = this.HighlightColor;
					}
					else
					{
						component.HighlightColour = Color.black;
					}
				}
			}
		}
	}

	// Token: 0x06006E24 RID: 28196 RVA: 0x0029D218 File Offset: 0x0029B418
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.Hover(true);
	}

	// Token: 0x06006E25 RID: 28197 RVA: 0x0029D221 File Offset: 0x0029B421
	public void OnPointerExit(PointerEventData eventData)
	{
		this.Hover(false);
	}

	// Token: 0x06006E26 RID: 28198 RVA: 0x0029D22C File Offset: 0x0029B42C
	public void SetSprite(Tag t)
	{
		Element element = ElementLoader.FindElementByName(this.Resource.Name);
		if (element != null)
		{
			Sprite uispriteFromMultiObjectAnim = Def.GetUISpriteFromMultiObjectAnim(element.substance.anim, "ui", false, "");
			if (uispriteFromMultiObjectAnim != null)
			{
				this.image.sprite = uispriteFromMultiObjectAnim;
			}
		}
	}

	// Token: 0x06006E27 RID: 28199 RVA: 0x0029D27E File Offset: 0x0029B47E
	public void SetSprite(Sprite sprite)
	{
		this.image.sprite = sprite;
	}

	// Token: 0x06006E28 RID: 28200 RVA: 0x0029D28C File Offset: 0x0029B48C
	public void Sim4000ms(float dt)
	{
		this.RefreshChart();
	}

	// Token: 0x06006E29 RID: 28201 RVA: 0x0029D294 File Offset: 0x0029B494
	private void RefreshChart()
	{
		if (this.sparkChart != null)
		{
			ResourceTracker resourceStatistic = TrackerTool.Instance.GetResourceStatistic(ClusterManager.Instance.activeWorldId, this.Resource);
			this.sparkChart.GetComponentInChildren<LineLayer>().RefreshLine(resourceStatistic.ChartableData(3000f), "resourceAmount");
			this.sparkChart.GetComponentInChildren<SparkLayer>().SetColor(Constants.NEUTRAL_COLOR);
		}
	}

	// Token: 0x04004B95 RID: 19349
	public Tag Resource;

	// Token: 0x04004B96 RID: 19350
	public GameUtil.MeasureUnit Measure;

	// Token: 0x04004B97 RID: 19351
	public LocText NameLabel;

	// Token: 0x04004B98 RID: 19352
	public LocText QuantityLabel;

	// Token: 0x04004B99 RID: 19353
	public Image image;

	// Token: 0x04004B9A RID: 19354
	[SerializeField]
	private Color AvailableColor;

	// Token: 0x04004B9B RID: 19355
	[SerializeField]
	private Color UnavailableColor;

	// Token: 0x04004B9C RID: 19356
	[SerializeField]
	private Color OverdrawnColor;

	// Token: 0x04004B9D RID: 19357
	[SerializeField]
	private Color HighlightColor;

	// Token: 0x04004B9E RID: 19358
	[SerializeField]
	private Color BackgroundHoverColor;

	// Token: 0x04004B9F RID: 19359
	[SerializeField]
	private Image Background;

	// Token: 0x04004BA0 RID: 19360
	[MyCmpGet]
	private ToolTip tooltip;

	// Token: 0x04004BA1 RID: 19361
	[MyCmpReq]
	private Button button;

	// Token: 0x04004BA2 RID: 19362
	public GameObject sparkChart;

	// Token: 0x04004BA3 RID: 19363
	private const float CLICK_RESET_TIME_THRESHOLD = 10f;

	// Token: 0x04004BA4 RID: 19364
	private int selectionIdx;

	// Token: 0x04004BA5 RID: 19365
	private float lastClickTime;

	// Token: 0x04004BA6 RID: 19366
	private List<Pickupable> cachedPickupables;

	// Token: 0x04004BA7 RID: 19367
	private float currentQuantity = float.MinValue;
}
