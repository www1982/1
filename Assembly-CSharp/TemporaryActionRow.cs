using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000E63 RID: 3683
public class TemporaryActionRow : KMonoBehaviour, IRender200ms
{
	// Token: 0x1700080B RID: 2059
	// (get) Token: 0x06007570 RID: 30064 RVA: 0x002CF8A8 File Offset: 0x002CDAA8
	// (set) Token: 0x0600756F RID: 30063 RVA: 0x002CF89F File Offset: 0x002CDA9F
	public float MaxHeight { get; private set; }

	// Token: 0x1700080C RID: 2060
	// (get) Token: 0x06007572 RID: 30066 RVA: 0x002CF8B9 File Offset: 0x002CDAB9
	// (set) Token: 0x06007571 RID: 30065 RVA: 0x002CF8B0 File Offset: 0x002CDAB0
	public bool IsVisible { get; private set; }

	// Token: 0x1700080D RID: 2061
	// (get) Token: 0x06007573 RID: 30067 RVA: 0x002CF8C1 File Offset: 0x002CDAC1
	public bool ShouldProgressBarBeEnabled
	{
		get
		{
			return this.ShowTimeout && this.Lifetime > 0f && this.lastSpecifiedLifetime > 0f;
		}
	}

	// Token: 0x1700080E RID: 2062
	// (get) Token: 0x06007575 RID: 30069 RVA: 0x002CF8F0 File Offset: 0x002CDAF0
	// (set) Token: 0x06007574 RID: 30068 RVA: 0x002CF8E7 File Offset: 0x002CDAE7
	public float Lifetime { get; private set; } = -1f;

	// Token: 0x1700080F RID: 2063
	// (get) Token: 0x06007577 RID: 30071 RVA: 0x002CF901 File Offset: 0x002CDB01
	// (set) Token: 0x06007576 RID: 30070 RVA: 0x002CF8F8 File Offset: 0x002CDAF8
	public bool ShowTimeout { get; set; } = true;

	// Token: 0x17000810 RID: 2064
	// (get) Token: 0x06007579 RID: 30073 RVA: 0x002CF912 File Offset: 0x002CDB12
	// (set) Token: 0x06007578 RID: 30072 RVA: 0x002CF909 File Offset: 0x002CDB09
	public bool ShowOnSpawn { get; set; } = true;

	// Token: 0x17000811 RID: 2065
	// (get) Token: 0x0600757B RID: 30075 RVA: 0x002CF923 File Offset: 0x002CDB23
	// (set) Token: 0x0600757A RID: 30074 RVA: 0x002CF91A File Offset: 0x002CDB1A
	public bool HideOnClick { get; set; } = true;

	// Token: 0x0600757C RID: 30076 RVA: 0x002CF92C File Offset: 0x002CDB2C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.layoutElement = base.GetComponent<LayoutElement>();
		this.button = base.GetComponent<Button>();
		this.button.onClick.AddListener(new UnityAction(this._OnRowClicked));
		this.MaxHeight = this.layoutElement.minHeight;
		this.HideImmediatly();
	}

	// Token: 0x0600757D RID: 30077 RVA: 0x002CF98A File Offset: 0x002CDB8A
	private void Update()
	{
		if (!this.HasBeenShown && this.ShowOnSpawn)
		{
			this.RefreshContentWidth();
			if (this.Content.sizeDelta.x > 0f)
			{
				this.Show();
			}
		}
	}

	// Token: 0x0600757E RID: 30078 RVA: 0x002CF9C1 File Offset: 0x002CDBC1
	private void _OnRowClicked()
	{
		Action<TemporaryActionRow> onRowClicked = this.OnRowClicked;
		if (onRowClicked != null)
		{
			onRowClicked(this);
		}
		if (this.HideOnClick)
		{
			this.Hide();
		}
	}

	// Token: 0x0600757F RID: 30079 RVA: 0x002CF9E3 File Offset: 0x002CDBE3
	private void _OnRowHidden()
	{
		Action<TemporaryActionRow> onRowHidden = this.OnRowHidden;
		if (onRowHidden == null)
		{
			return;
		}
		onRowHidden(this);
	}

	// Token: 0x06007580 RID: 30080 RVA: 0x002CF9F6 File Offset: 0x002CDBF6
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (base.isSpawned)
		{
			this.RefreshContentWidth();
		}
	}

	// Token: 0x06007581 RID: 30081 RVA: 0x002CFA0C File Offset: 0x002CDC0C
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		this.HideImmediatly();
		this._OnRowHidden();
	}

	// Token: 0x06007582 RID: 30082 RVA: 0x002CFA20 File Offset: 0x002CDC20
	public void SetLifetime(float lifetime)
	{
		this.Lifetime = lifetime;
		this.lastSpecifiedLifetime = lifetime;
		this.UpdateTimeout();
	}

	// Token: 0x06007583 RID: 30083 RVA: 0x002CFA38 File Offset: 0x002CDC38
	private void UpdateTimeout()
	{
		bool shouldProgressBarBeEnabled = this.ShouldProgressBarBeEnabled;
		if (shouldProgressBarBeEnabled != this.TimeoutBarSection.gameObject.activeInHierarchy)
		{
			this.TimeoutBarSection.gameObject.SetActive(shouldProgressBarBeEnabled);
		}
		if (shouldProgressBarBeEnabled)
		{
			this.TimeoutImage.fillAmount = Mathf.Clamp(this.Lifetime / this.lastSpecifiedLifetime, 0f, 1f);
		}
	}

	// Token: 0x06007584 RID: 30084 RVA: 0x002CFA9C File Offset: 0x002CDC9C
	public void Render200ms(float dt)
	{
		if (this.HasBeenShown && this.Lifetime > 0f && this.IsVisible)
		{
			this.Lifetime -= dt;
			if (this.Lifetime <= 0f)
			{
				this.Hide();
			}
			this.UpdateTimeout();
		}
	}

	// Token: 0x06007585 RID: 30085 RVA: 0x002CFAED File Offset: 0x002CDCED
	public void Setup(string text, string tooltip, Sprite icon = null)
	{
		this.Label.SetText(text);
		this.Tooltip.SetSimpleTooltip(tooltip);
		this.Image.sprite = icon;
		this.IconSection.gameObject.SetActive(icon != null);
	}

	// Token: 0x06007586 RID: 30086 RVA: 0x002CFB2C File Offset: 0x002CDD2C
	public void Show()
	{
		this.AbortCoroutine();
		this.IsVisible = true;
		this.HasBeenShown = true;
		this.button.interactable = true;
		if (base.gameObject.activeInHierarchy)
		{
			this.SetContentToHiddenPosition();
			this.layoutCoroutine = this.RunEnterHeightAnimation(delegate
			{
				this.layoutCoroutine = this.RunEnterSlideAnimation(null);
			});
		}
	}

	// Token: 0x06007587 RID: 30087 RVA: 0x002CFB84 File Offset: 0x002CDD84
	public void HideImmediatly()
	{
		this.AbortCoroutine();
		this.IsVisible = false;
		this.Content.localPosition = new Vector3(-(base.transform as RectTransform).sizeDelta.x, this.Content.localPosition.y, this.Content.localPosition.z);
		this.layoutElement.minHeight = 0f;
		this.button.interactable = false;
	}

	// Token: 0x06007588 RID: 30088 RVA: 0x002CFC00 File Offset: 0x002CDE00
	public void Hide()
	{
		this.AbortCoroutine();
		this.IsVisible = false;
		this.button.interactable = false;
		if (base.gameObject.activeInHierarchy)
		{
			this.layoutCoroutine = this.RunExitSlideAnimation(delegate
			{
				this.layoutCoroutine = this.RunExitHeightAnimation(new global::System.Action(this._OnRowHidden));
			});
		}
	}

	// Token: 0x06007589 RID: 30089 RVA: 0x002CFC40 File Offset: 0x002CDE40
	private void AbortCoroutine()
	{
		if (this.layoutCoroutine != null)
		{
			base.StopCoroutine(this.layoutCoroutine);
			this.layoutCoroutine = null;
		}
	}

	// Token: 0x0600758A RID: 30090 RVA: 0x002CFC60 File Offset: 0x002CDE60
	private void RefreshContentWidth()
	{
		RectTransform rectTransform = base.transform as RectTransform;
		if (rectTransform.sizeDelta.x != this.Content.sizeDelta.x)
		{
			this.Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.sizeDelta.x);
		}
	}

	// Token: 0x0600758B RID: 30091 RVA: 0x002CFCB0 File Offset: 0x002CDEB0
	private void SetContentToHiddenPosition()
	{
		this.RefreshContentWidth();
		Vector3 vector = this.Content.anchoredPosition;
		vector.x = -(base.transform as RectTransform).sizeDelta.x;
		this.Content.anchoredPosition = vector;
	}

	// Token: 0x0600758C RID: 30092 RVA: 0x002CFD02 File Offset: 0x002CDF02
	private Coroutine RunEnterSlideAnimation(global::System.Action onAnimationEnds = null)
	{
		return base.StartCoroutine(this.SlideTransitionAnimation(0.4f, true, (float n) => Mathf.Sqrt(n), onAnimationEnds));
	}

	// Token: 0x0600758D RID: 30093 RVA: 0x002CFD36 File Offset: 0x002CDF36
	private Coroutine RunExitSlideAnimation(global::System.Action onAnimationEnds = null)
	{
		return base.StartCoroutine(this.SlideTransitionAnimation(0.4f, false, (float n) => Mathf.Pow(n, 2f), onAnimationEnds));
	}

	// Token: 0x0600758E RID: 30094 RVA: 0x002CFD6A File Offset: 0x002CDF6A
	private Coroutine RunEnterHeightAnimation(global::System.Action onAnimationEnds = null)
	{
		return base.StartCoroutine(this.HeightTransitionAnimation(0.5f, true, (float n) => Mathf.Sqrt(n), onAnimationEnds));
	}

	// Token: 0x0600758F RID: 30095 RVA: 0x002CFD9E File Offset: 0x002CDF9E
	private Coroutine RunExitHeightAnimation(global::System.Action onAnimationEnds = null)
	{
		return base.StartCoroutine(this.HeightTransitionAnimation(0.3f, false, (float n) => Mathf.Pow(n, 2f), onAnimationEnds));
	}

	// Token: 0x06007590 RID: 30096 RVA: 0x002CFDD2 File Offset: 0x002CDFD2
	private IEnumerator SlideTransitionAnimation(float duration, bool show, Func<float, float> curveModifier = null, global::System.Action onAnimationEnds = null)
	{
		float num = -(base.transform as RectTransform).sizeDelta.x;
		float num2 = 0f;
		float contentInitialXPosition = (show ? num : this.Content.anchoredPosition.x);
		float targetPosition = (show ? num2 : num);
		float timePassed = 0f;
		Vector3 vector = this.Content.anchoredPosition;
		while (timePassed < duration)
		{
			this.RefreshContentWidth();
			float num3 = timePassed / duration;
			if (curveModifier != null)
			{
				num3 = curveModifier(num3);
			}
			vector = this.Content.anchoredPosition;
			vector.x = Mathf.Lerp(contentInitialXPosition, targetPosition, num3);
			this.Content.anchoredPosition = vector;
			timePassed += Time.unscaledDeltaTime;
			yield return null;
		}
		this.RefreshContentWidth();
		vector = this.Content.anchoredPosition;
		vector.x = targetPosition;
		this.Content.anchoredPosition = vector;
		yield return null;
		if (onAnimationEnds != null)
		{
			onAnimationEnds();
		}
		yield break;
	}

	// Token: 0x06007591 RID: 30097 RVA: 0x002CFDFE File Offset: 0x002CDFFE
	private IEnumerator HeightTransitionAnimation(float duration, bool show, Func<float, float> curveModifier = null, global::System.Action onAnimationEnds = null)
	{
		Transform transform = base.transform;
		float initialHeight = this.layoutElement.minHeight;
		float targetHeight = (show ? this.MaxHeight : 0f);
		float timePassed = 0f;
		float num = this.layoutElement.minHeight;
		while (timePassed < duration)
		{
			this.RefreshContentWidth();
			float num2 = timePassed / duration;
			if (curveModifier != null)
			{
				num2 = curveModifier(num2);
			}
			num = Mathf.Lerp(initialHeight, targetHeight, num2);
			this.layoutElement.minHeight = num;
			timePassed += Time.unscaledDeltaTime;
			yield return null;
		}
		this.RefreshContentWidth();
		num = targetHeight;
		this.layoutElement.minHeight = num;
		yield return null;
		if (onAnimationEnds != null)
		{
			onAnimationEnds();
		}
		yield break;
	}

	// Token: 0x04005167 RID: 20839
	public const float ROW_HEIGHT_ANIM_ENTRY_DURATION = 0.5f;

	// Token: 0x04005168 RID: 20840
	public const float ROW_HEIGHT_ANIM_EXIT_DURATION = 0.3f;

	// Token: 0x04005169 RID: 20841
	public const float SLIDE_ENTER_ANIM_DURATION = 0.4f;

	// Token: 0x0400516A RID: 20842
	public const float SLIDE_EXIT_ANIM_DURATION = 0.4f;

	// Token: 0x0400516D RID: 20845
	public RectTransform Content;

	// Token: 0x0400516E RID: 20846
	public RectTransform IconSection;

	// Token: 0x0400516F RID: 20847
	public RectTransform TimeoutBarSection;

	// Token: 0x04005170 RID: 20848
	public KImage Image;

	// Token: 0x04005171 RID: 20849
	public Image TimeoutImage;

	// Token: 0x04005172 RID: 20850
	public LocText Label;

	// Token: 0x04005173 RID: 20851
	public ToolTip Tooltip;

	// Token: 0x04005174 RID: 20852
	public Action<TemporaryActionRow> OnRowClicked;

	// Token: 0x04005175 RID: 20853
	public Action<TemporaryActionRow> OnRowHidden;

	// Token: 0x04005176 RID: 20854
	private LayoutElement layoutElement;

	// Token: 0x04005177 RID: 20855
	private Coroutine layoutCoroutine;

	// Token: 0x04005178 RID: 20856
	private Button button;

	// Token: 0x0400517D RID: 20861
	private bool HasBeenShown;

	// Token: 0x0400517E RID: 20862
	private float lastSpecifiedLifetime = -1f;
}
