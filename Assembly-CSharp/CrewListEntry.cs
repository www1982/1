using System;
using Klei.AI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000D2B RID: 3371
[AddComponentMenu("KMonoBehaviour/scripts/CrewListEntry")]
public class CrewListEntry : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x1700076E RID: 1902
	// (get) Token: 0x06006820 RID: 26656 RVA: 0x0027497A File Offset: 0x00272B7A
	public MinionIdentity Identity
	{
		get
		{
			return this.identity;
		}
	}

	// Token: 0x06006821 RID: 26657 RVA: 0x00274982 File Offset: 0x00272B82
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.mouseOver = true;
		this.BGImage.enabled = true;
		this.BorderHighlight.color = new Color(0.65882355f, 0.2901961f, 0.4745098f);
	}

	// Token: 0x06006822 RID: 26658 RVA: 0x002749B6 File Offset: 0x00272BB6
	public void OnPointerExit(PointerEventData eventData)
	{
		this.mouseOver = false;
		this.BGImage.enabled = false;
		this.BorderHighlight.color = new Color(0.8f, 0.8f, 0.8f);
	}

	// Token: 0x06006823 RID: 26659 RVA: 0x002749EC File Offset: 0x00272BEC
	public void OnPointerClick(PointerEventData eventData)
	{
		bool flag = Time.unscaledTime - this.lastClickTime < 0.3f;
		this.SelectCrewMember(flag);
		this.lastClickTime = Time.unscaledTime;
	}

	// Token: 0x06006824 RID: 26660 RVA: 0x00274A20 File Offset: 0x00272C20
	public virtual void Populate(MinionIdentity _identity)
	{
		this.identity = _identity;
		if (this.portrait == null)
		{
			GameObject gameObject = ((this.crewPortraitParent != null) ? this.crewPortraitParent : base.gameObject);
			this.portrait = Util.KInstantiateUI<CrewPortrait>(this.PortraitPrefab.gameObject, gameObject, false);
			if (this.crewPortraitParent == null)
			{
				this.portrait.transform.SetSiblingIndex(2);
			}
		}
		this.portrait.SetIdentityObject(_identity, true);
	}

	// Token: 0x06006825 RID: 26661 RVA: 0x00274AA3 File Offset: 0x00272CA3
	public virtual void Refresh()
	{
	}

	// Token: 0x06006826 RID: 26662 RVA: 0x00274AA5 File Offset: 0x00272CA5
	public void RefreshCrewPortraitContent()
	{
		if (this.portrait != null)
		{
			this.portrait.ForceRefresh();
		}
	}

	// Token: 0x06006827 RID: 26663 RVA: 0x00274AC0 File Offset: 0x00272CC0
	private string seniorityString()
	{
		return this.identity.GetAttributes().GetProfessionString(true);
	}

	// Token: 0x06006828 RID: 26664 RVA: 0x00274AD4 File Offset: 0x00272CD4
	public void SelectCrewMember(bool focus)
	{
		if (focus)
		{
			SelectTool.Instance.SelectAndFocus(this.identity.transform.GetPosition(), this.identity.GetComponent<KSelectable>(), new Vector3(8f, 0f, 0f));
			return;
		}
		SelectTool.Instance.Select(this.identity.GetComponent<KSelectable>(), false);
	}

	// Token: 0x0400476D RID: 18285
	protected MinionIdentity identity;

	// Token: 0x0400476E RID: 18286
	protected CrewPortrait portrait;

	// Token: 0x0400476F RID: 18287
	public CrewPortrait PortraitPrefab;

	// Token: 0x04004770 RID: 18288
	public GameObject crewPortraitParent;

	// Token: 0x04004771 RID: 18289
	protected bool mouseOver;

	// Token: 0x04004772 RID: 18290
	public Image BorderHighlight;

	// Token: 0x04004773 RID: 18291
	public Image BGImage;

	// Token: 0x04004774 RID: 18292
	public float lastClickTime;
}
