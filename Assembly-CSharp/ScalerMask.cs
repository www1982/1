using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000CBD RID: 3261
[AddComponentMenu("KMonoBehaviour/scripts/ScalerMask")]
public class ScalerMask : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x17000752 RID: 1874
	// (get) Token: 0x0600645D RID: 25693 RVA: 0x0025B562 File Offset: 0x00259762
	private RectTransform ThisTransform
	{
		get
		{
			if (this._thisTransform == null)
			{
				this._thisTransform = base.GetComponent<RectTransform>();
			}
			return this._thisTransform;
		}
	}

	// Token: 0x17000753 RID: 1875
	// (get) Token: 0x0600645E RID: 25694 RVA: 0x0025B584 File Offset: 0x00259784
	private LayoutElement ThisLayoutElement
	{
		get
		{
			if (this._thisLayoutElement == null)
			{
				this._thisLayoutElement = base.GetComponent<LayoutElement>();
			}
			return this._thisLayoutElement;
		}
	}

	// Token: 0x0600645F RID: 25695 RVA: 0x0025B5A8 File Offset: 0x002597A8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		DetailsScreen componentInParent = base.GetComponentInParent<DetailsScreen>();
		if (componentInParent)
		{
			DetailsScreen detailsScreen = componentInParent;
			detailsScreen.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Combine(detailsScreen.pointerEnterActions, new KScreen.PointerEnterActions(this.OnPointerEnterGrandparent));
			DetailsScreen detailsScreen2 = componentInParent;
			detailsScreen2.pointerExitActions = (KScreen.PointerExitActions)Delegate.Combine(detailsScreen2.pointerExitActions, new KScreen.PointerExitActions(this.OnPointerExitGrandparent));
		}
	}

	// Token: 0x06006460 RID: 25696 RVA: 0x0025B610 File Offset: 0x00259810
	protected override void OnCleanUp()
	{
		DetailsScreen componentInParent = base.GetComponentInParent<DetailsScreen>();
		if (componentInParent)
		{
			DetailsScreen detailsScreen = componentInParent;
			detailsScreen.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Remove(detailsScreen.pointerEnterActions, new KScreen.PointerEnterActions(this.OnPointerEnterGrandparent));
			DetailsScreen detailsScreen2 = componentInParent;
			detailsScreen2.pointerExitActions = (KScreen.PointerExitActions)Delegate.Remove(detailsScreen2.pointerExitActions, new KScreen.PointerExitActions(this.OnPointerExitGrandparent));
		}
		base.OnCleanUp();
	}

	// Token: 0x06006461 RID: 25697 RVA: 0x0025B678 File Offset: 0x00259878
	private void Update()
	{
		if (this.SourceTransform != null)
		{
			this.SourceTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, this.ThisTransform.rect.width);
		}
		if (this.SourceTransform != null && (!this.hoverLock || !this.grandparentIsHovered || this.isHovered || this.queuedSizeUpdate))
		{
			this.ThisLayoutElement.minHeight = this.SourceTransform.rect.height + this.topPadding + this.bottomPadding;
			this.SourceTransform.anchoredPosition = new Vector2(0f, -this.topPadding);
			this.queuedSizeUpdate = false;
		}
		if (this.hoverIndicator != null)
		{
			if (this.SourceTransform != null && this.SourceTransform.rect.height > this.ThisTransform.rect.height)
			{
				this.hoverIndicator.SetActive(true);
				return;
			}
			this.hoverIndicator.SetActive(false);
		}
	}

	// Token: 0x06006462 RID: 25698 RVA: 0x0025B78C File Offset: 0x0025998C
	public void UpdateSize()
	{
		this.queuedSizeUpdate = true;
	}

	// Token: 0x06006463 RID: 25699 RVA: 0x0025B795 File Offset: 0x00259995
	public void OnPointerEnterGrandparent(PointerEventData eventData)
	{
		this.grandparentIsHovered = true;
	}

	// Token: 0x06006464 RID: 25700 RVA: 0x0025B79E File Offset: 0x0025999E
	public void OnPointerExitGrandparent(PointerEventData eventData)
	{
		this.grandparentIsHovered = false;
	}

	// Token: 0x06006465 RID: 25701 RVA: 0x0025B7A7 File Offset: 0x002599A7
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.isHovered = true;
	}

	// Token: 0x06006466 RID: 25702 RVA: 0x0025B7B0 File Offset: 0x002599B0
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isHovered = false;
	}

	// Token: 0x0400447A RID: 17530
	public RectTransform SourceTransform;

	// Token: 0x0400447B RID: 17531
	private RectTransform _thisTransform;

	// Token: 0x0400447C RID: 17532
	private LayoutElement _thisLayoutElement;

	// Token: 0x0400447D RID: 17533
	public GameObject hoverIndicator;

	// Token: 0x0400447E RID: 17534
	public bool hoverLock;

	// Token: 0x0400447F RID: 17535
	private bool grandparentIsHovered;

	// Token: 0x04004480 RID: 17536
	private bool isHovered;

	// Token: 0x04004481 RID: 17537
	private bool queuedSizeUpdate = true;

	// Token: 0x04004482 RID: 17538
	public float topPadding;

	// Token: 0x04004483 RID: 17539
	public float bottomPadding;
}
