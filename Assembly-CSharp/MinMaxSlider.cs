using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E85 RID: 3717
[AddComponentMenu("KMonoBehaviour/scripts/MinMaxSlider")]
public class MinMaxSlider : KMonoBehaviour
{
	// Token: 0x17000817 RID: 2071
	// (get) Token: 0x06007673 RID: 30323 RVA: 0x002D4EBD File Offset: 0x002D30BD
	// (set) Token: 0x06007674 RID: 30324 RVA: 0x002D4EC5 File Offset: 0x002D30C5
	public MinMaxSlider.Mode mode { get; private set; }

	// Token: 0x06007675 RID: 30325 RVA: 0x002D4ED0 File Offset: 0x002D30D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ToolTip component = base.transform.parent.gameObject.GetComponent<ToolTip>();
		if (component != null)
		{
			global::UnityEngine.Object.DestroyImmediate(this.toolTip);
			this.toolTip = component;
		}
		this.minSlider.value = this.currentMinValue;
		this.maxSlider.value = this.currentMaxValue;
		this.minSlider.interactable = this.interactable;
		this.maxSlider.interactable = this.interactable;
		this.minSlider.maxValue = this.maxLimit;
		this.maxSlider.maxValue = this.maxLimit;
		this.minSlider.minValue = this.minLimit;
		this.maxSlider.minValue = this.minLimit;
		this.minSlider.direction = (this.maxSlider.direction = this.direction);
		if (this.isOverPowered != null)
		{
			this.isOverPowered.enabled = false;
		}
		this.minSlider.gameObject.SetActive(false);
		if (this.mode != MinMaxSlider.Mode.Single)
		{
			this.minSlider.gameObject.SetActive(true);
		}
		if (this.extraSlider != null)
		{
			this.extraSlider.value = this.currentExtraValue;
			this.extraSlider.wholeNumbers = (this.minSlider.wholeNumbers = (this.maxSlider.wholeNumbers = this.wholeNumbers));
			this.extraSlider.direction = this.direction;
			this.extraSlider.interactable = this.interactable;
			this.extraSlider.maxValue = this.maxLimit;
			this.extraSlider.minValue = this.minLimit;
			this.extraSlider.gameObject.SetActive(false);
			if (this.mode == MinMaxSlider.Mode.Triple)
			{
				this.extraSlider.gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06007676 RID: 30326 RVA: 0x002D50C0 File Offset: 0x002D32C0
	public void SetIcon(Image newIcon)
	{
		this.icon = newIcon;
		this.icon.gameObject.transform.SetParent(base.transform);
		this.icon.gameObject.transform.SetAsFirstSibling();
		this.icon.rectTransform().anchoredPosition = Vector2.zero;
	}

	// Token: 0x06007677 RID: 30327 RVA: 0x002D511C File Offset: 0x002D331C
	public void SetMode(MinMaxSlider.Mode mode)
	{
		this.mode = mode;
		if (mode == MinMaxSlider.Mode.Single && this.extraSlider != null)
		{
			this.extraSlider.gameObject.SetActive(false);
			this.extraSlider.handleRect.gameObject.SetActive(false);
		}
	}

	// Token: 0x06007678 RID: 30328 RVA: 0x002D5168 File Offset: 0x002D3368
	private void SetAnchor(RectTransform trans, Vector2 min, Vector2 max)
	{
		trans.anchorMin = min;
		trans.anchorMax = max;
	}

	// Token: 0x06007679 RID: 30329 RVA: 0x002D5178 File Offset: 0x002D3378
	public void SetMinMaxValue(float currentMin, float currentMax, float min, float max)
	{
		this.minSlider.value = currentMin;
		this.currentMinValue = currentMin;
		this.maxSlider.value = currentMax;
		this.currentMaxValue = currentMax;
		this.minLimit = min;
		this.maxLimit = max;
		this.minSlider.minValue = this.minLimit;
		this.maxSlider.minValue = this.minLimit;
		this.minSlider.maxValue = this.maxLimit;
		this.maxSlider.maxValue = this.maxLimit;
		if (this.extraSlider != null)
		{
			this.extraSlider.minValue = this.minLimit;
			this.extraSlider.maxValue = this.maxLimit;
		}
	}

	// Token: 0x0600767A RID: 30330 RVA: 0x002D5232 File Offset: 0x002D3432
	public void SetExtraValue(float current)
	{
		this.extraSlider.value = current;
		this.toolTip.toolTip = base.transform.parent.name + ": " + current.ToString("F2");
	}

	// Token: 0x0600767B RID: 30331 RVA: 0x002D5274 File Offset: 0x002D3474
	public void SetMaxValue(float current, float max)
	{
		float num = current / max * 100f;
		if (this.isOverPowered != null)
		{
			this.isOverPowered.enabled = num > 100f;
		}
		this.maxSlider.value = Mathf.Min(100f, num);
		if (this.toolTip != null)
		{
			this.toolTip.toolTip = string.Concat(new string[]
			{
				base.transform.parent.name,
				": ",
				current.ToString("F2"),
				"/",
				max.ToString("F2")
			});
		}
	}

	// Token: 0x0600767C RID: 30332 RVA: 0x002D5328 File Offset: 0x002D3528
	private void Update()
	{
		if (!this.interactable)
		{
			return;
		}
		this.minSlider.value = Mathf.Clamp(this.currentMinValue, this.minLimit, this.currentMinValue);
		this.maxSlider.value = Mathf.Max(this.minSlider.value, Mathf.Clamp(this.currentMaxValue, Mathf.Max(this.minSlider.value, this.minLimit), this.maxLimit));
		if (this.direction == Slider.Direction.LeftToRight || this.direction == Slider.Direction.RightToLeft)
		{
			this.minRect.anchorMax = new Vector2(this.minSlider.value / this.maxLimit, this.minRect.anchorMax.y);
			this.maxRect.anchorMax = new Vector2(this.maxSlider.value / this.maxLimit, this.maxRect.anchorMax.y);
			this.maxRect.anchorMin = new Vector2(this.minSlider.value / this.maxLimit, this.maxRect.anchorMin.y);
			return;
		}
		this.minRect.anchorMax = new Vector2(this.minRect.anchorMin.x, this.minSlider.value / this.maxLimit);
		this.maxRect.anchorMin = new Vector2(this.maxRect.anchorMin.x, this.minSlider.value / this.maxLimit);
	}

	// Token: 0x0600767D RID: 30333 RVA: 0x002D54B4 File Offset: 0x002D36B4
	public void OnMinValueChanged(float ignoreThis)
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockRange)
		{
			this.currentMaxValue = Mathf.Min(Mathf.Max(this.minLimit, this.minSlider.value) + this.range, this.maxLimit);
			this.currentMinValue = Mathf.Max(this.minLimit, Mathf.Min(this.maxSlider.value, this.currentMaxValue - this.range));
		}
		else
		{
			this.currentMinValue = Mathf.Clamp(this.minSlider.value, this.minLimit, Mathf.Min(this.maxSlider.value, this.currentMaxValue));
		}
		if (this.onMinChange != null)
		{
			this.onMinChange(this);
		}
	}

	// Token: 0x0600767E RID: 30334 RVA: 0x002D5578 File Offset: 0x002D3778
	public void OnMaxValueChanged(float ignoreThis)
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockRange)
		{
			this.currentMinValue = Mathf.Max(this.maxSlider.value - this.range, this.minLimit);
			this.currentMaxValue = Mathf.Max(this.minSlider.value, Mathf.Clamp(this.maxSlider.value, Mathf.Max(this.currentMinValue + this.range, this.minLimit), this.maxLimit));
		}
		else
		{
			this.currentMaxValue = Mathf.Max(this.minSlider.value, Mathf.Clamp(this.maxSlider.value, Mathf.Max(this.minSlider.value, this.minLimit), this.maxLimit));
		}
		if (this.onMaxChange != null)
		{
			this.onMaxChange(this);
		}
	}

	// Token: 0x0600767F RID: 30335 RVA: 0x002D5658 File Offset: 0x002D3858
	public void Lock(bool shouldLock)
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockType == MinMaxSlider.LockingType.Drag)
		{
			this.lockRange = shouldLock;
			this.range = this.maxSlider.value - this.minSlider.value;
			this.mousePos = KInputManager.GetMousePos();
		}
	}

	// Token: 0x06007680 RID: 30336 RVA: 0x002D56A8 File Offset: 0x002D38A8
	public void ToggleLock()
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockType == MinMaxSlider.LockingType.Toggle)
		{
			this.lockRange = !this.lockRange;
			if (this.lockRange)
			{
				this.range = this.maxSlider.value - this.minSlider.value;
			}
		}
	}

	// Token: 0x06007681 RID: 30337 RVA: 0x002D56FC File Offset: 0x002D38FC
	public void OnDrag()
	{
		if (!this.interactable)
		{
			return;
		}
		if (this.lockRange && this.lockType == MinMaxSlider.LockingType.Drag)
		{
			float num = KInputManager.GetMousePos().x - this.mousePos.x;
			if (this.direction == Slider.Direction.TopToBottom || this.direction == Slider.Direction.BottomToTop)
			{
				num = KInputManager.GetMousePos().y - this.mousePos.y;
			}
			this.currentMinValue = Mathf.Max(this.currentMinValue + num, this.minLimit);
			this.mousePos = KInputManager.GetMousePos();
		}
	}

	// Token: 0x04005248 RID: 21064
	public MinMaxSlider.LockingType lockType = MinMaxSlider.LockingType.Drag;

	// Token: 0x0400524A RID: 21066
	public bool lockRange;

	// Token: 0x0400524B RID: 21067
	public bool interactable = true;

	// Token: 0x0400524C RID: 21068
	public float minLimit;

	// Token: 0x0400524D RID: 21069
	public float maxLimit = 100f;

	// Token: 0x0400524E RID: 21070
	public float range = 50f;

	// Token: 0x0400524F RID: 21071
	public float barWidth = 10f;

	// Token: 0x04005250 RID: 21072
	public float barHeight = 100f;

	// Token: 0x04005251 RID: 21073
	public float currentMinValue = 10f;

	// Token: 0x04005252 RID: 21074
	public float currentMaxValue = 90f;

	// Token: 0x04005253 RID: 21075
	public float currentExtraValue = 50f;

	// Token: 0x04005254 RID: 21076
	public Slider.Direction direction;

	// Token: 0x04005255 RID: 21077
	public bool wholeNumbers = true;

	// Token: 0x04005256 RID: 21078
	public Action<MinMaxSlider> onMinChange;

	// Token: 0x04005257 RID: 21079
	public Action<MinMaxSlider> onMaxChange;

	// Token: 0x04005258 RID: 21080
	public Slider minSlider;

	// Token: 0x04005259 RID: 21081
	public Slider maxSlider;

	// Token: 0x0400525A RID: 21082
	public Slider extraSlider;

	// Token: 0x0400525B RID: 21083
	public RectTransform minRect;

	// Token: 0x0400525C RID: 21084
	public RectTransform maxRect;

	// Token: 0x0400525D RID: 21085
	public RectTransform bgFill;

	// Token: 0x0400525E RID: 21086
	public RectTransform mgFill;

	// Token: 0x0400525F RID: 21087
	public RectTransform fgFill;

	// Token: 0x04005260 RID: 21088
	public Text title;

	// Token: 0x04005261 RID: 21089
	[MyCmpGet]
	public ToolTip toolTip;

	// Token: 0x04005262 RID: 21090
	public Image icon;

	// Token: 0x04005263 RID: 21091
	public Image isOverPowered;

	// Token: 0x04005264 RID: 21092
	private Vector3 mousePos;

	// Token: 0x02002079 RID: 8313
	public enum LockingType
	{
		// Token: 0x0400945C RID: 37980
		Toggle,
		// Token: 0x0400945D RID: 37981
		Drag
	}

	// Token: 0x0200207A RID: 8314
	public enum Mode
	{
		// Token: 0x0400945F RID: 37983
		Single,
		// Token: 0x04009460 RID: 37984
		Double,
		// Token: 0x04009461 RID: 37985
		Triple
	}
}
