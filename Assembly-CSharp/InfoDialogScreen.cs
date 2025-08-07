using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CEC RID: 3308
public class InfoDialogScreen : KModalScreen
{
	// Token: 0x060065BF RID: 26047 RVA: 0x00264CFE File Offset: 0x00262EFE
	public InfoScreenPlainText GetSubHeaderPrefab()
	{
		return this.subHeaderTemplate;
	}

	// Token: 0x060065C0 RID: 26048 RVA: 0x00264D06 File Offset: 0x00262F06
	public InfoScreenPlainText GetPlainTextPrefab()
	{
		return this.plainTextTemplate;
	}

	// Token: 0x060065C1 RID: 26049 RVA: 0x00264D0E File Offset: 0x00262F0E
	public InfoScreenLineItem GetLineItemPrefab()
	{
		return this.lineItemTemplate;
	}

	// Token: 0x060065C2 RID: 26050 RVA: 0x00264D16 File Offset: 0x00262F16
	public GameObject GetPrimaryButtonPrefab()
	{
		return this.leftButtonPrefab;
	}

	// Token: 0x060065C3 RID: 26051 RVA: 0x00264D1E File Offset: 0x00262F1E
	public GameObject GetSecondaryButtonPrefab()
	{
		return this.rightButtonPrefab;
	}

	// Token: 0x060065C4 RID: 26052 RVA: 0x00264D26 File Offset: 0x00262F26
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.SetActive(false);
	}

	// Token: 0x060065C5 RID: 26053 RVA: 0x00264D3A File Offset: 0x00262F3A
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x060065C6 RID: 26054 RVA: 0x00264D40 File Offset: 0x00262F40
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!this.escapeCloses)
		{
			e.TryConsume(global::Action.Escape);
			return;
		}
		if (e.TryConsume(global::Action.Escape))
		{
			this.Deactivate();
			return;
		}
		if (PlayerController.Instance != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.Deactivate();
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x060065C7 RID: 26055 RVA: 0x00264D97 File Offset: 0x00262F97
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (!show && this.onDeactivateFn != null)
		{
			this.onDeactivateFn();
		}
	}

	// Token: 0x060065C8 RID: 26056 RVA: 0x00264DB6 File Offset: 0x00262FB6
	public InfoDialogScreen AddDefaultOK(bool escapeCloses = false)
	{
		this.AddOption(UI.CONFIRMDIALOG.OK, delegate(InfoDialogScreen d)
		{
			d.Deactivate();
		}, true);
		this.escapeCloses = escapeCloses;
		return this;
	}

	// Token: 0x060065C9 RID: 26057 RVA: 0x00264DF1 File Offset: 0x00262FF1
	public InfoDialogScreen AddDefaultCancel()
	{
		this.AddOption(UI.CONFIRMDIALOG.CANCEL, delegate(InfoDialogScreen d)
		{
			d.Deactivate();
		}, false);
		this.escapeCloses = true;
		return this;
	}

	// Token: 0x060065CA RID: 26058 RVA: 0x00264E2C File Offset: 0x0026302C
	public InfoDialogScreen AddOption(string text, Action<InfoDialogScreen> action, bool rightSide = false)
	{
		GameObject gameObject = Util.KInstantiateUI(rightSide ? this.rightButtonPrefab : this.leftButtonPrefab, rightSide ? this.rightButtonPanel : this.leftButtonPanel, true);
		gameObject.gameObject.GetComponentInChildren<LocText>().text = text;
		gameObject.gameObject.GetComponent<KButton>().onClick += delegate
		{
			action(this);
		};
		return this;
	}

	// Token: 0x060065CB RID: 26059 RVA: 0x00264EA4 File Offset: 0x002630A4
	public InfoDialogScreen AddOption(bool rightSide, out KButton button, out LocText buttonText)
	{
		GameObject gameObject = Util.KInstantiateUI(rightSide ? this.rightButtonPrefab : this.leftButtonPrefab, rightSide ? this.rightButtonPanel : this.leftButtonPanel, true);
		button = gameObject.GetComponent<KButton>();
		buttonText = gameObject.GetComponentInChildren<LocText>();
		return this;
	}

	// Token: 0x060065CC RID: 26060 RVA: 0x00264EEB File Offset: 0x002630EB
	public InfoDialogScreen SetHeader(string header)
	{
		this.header.text = header;
		return this;
	}

	// Token: 0x060065CD RID: 26061 RVA: 0x00264EFA File Offset: 0x002630FA
	public InfoDialogScreen AddSprite(Sprite sprite)
	{
		Util.KInstantiateUI<InfoScreenSpriteItem>(this.spriteItemTemplate.gameObject, this.contentContainer, false).SetSprite(sprite);
		return this;
	}

	// Token: 0x060065CE RID: 26062 RVA: 0x00264F1A File Offset: 0x0026311A
	public InfoDialogScreen AddPlainText(string text)
	{
		Util.KInstantiateUI<InfoScreenPlainText>(this.plainTextTemplate.gameObject, this.contentContainer, false).SetText(text);
		return this;
	}

	// Token: 0x060065CF RID: 26063 RVA: 0x00264F3A File Offset: 0x0026313A
	public InfoDialogScreen AddLineItem(string text, string tooltip)
	{
		InfoScreenLineItem infoScreenLineItem = Util.KInstantiateUI<InfoScreenLineItem>(this.lineItemTemplate.gameObject, this.contentContainer, false);
		infoScreenLineItem.SetText(text);
		infoScreenLineItem.SetTooltip(tooltip);
		return this;
	}

	// Token: 0x060065D0 RID: 26064 RVA: 0x00264F61 File Offset: 0x00263161
	public InfoDialogScreen AddSubHeader(string text)
	{
		Util.KInstantiateUI<InfoScreenPlainText>(this.subHeaderTemplate.gameObject, this.contentContainer, false).SetText(text);
		return this;
	}

	// Token: 0x060065D1 RID: 26065 RVA: 0x00264F84 File Offset: 0x00263184
	public InfoDialogScreen AddSpacer(float height)
	{
		GameObject gameObject = new GameObject("spacer");
		gameObject.SetActive(false);
		gameObject.transform.SetParent(this.contentContainer.transform, false);
		LayoutElement layoutElement = gameObject.AddComponent<LayoutElement>();
		layoutElement.minHeight = height;
		layoutElement.preferredHeight = height;
		layoutElement.flexibleHeight = 0f;
		gameObject.SetActive(true);
		return this;
	}

	// Token: 0x060065D2 RID: 26066 RVA: 0x00264FDE File Offset: 0x002631DE
	public InfoDialogScreen AddUI<T>(T prefab, out T spawn) where T : MonoBehaviour
	{
		spawn = Util.KInstantiateUI<T>(prefab.gameObject, this.contentContainer, true);
		return this;
	}

	// Token: 0x060065D3 RID: 26067 RVA: 0x00265000 File Offset: 0x00263200
	public InfoDialogScreen AddDescriptors(List<Descriptor> descriptors)
	{
		for (int i = 0; i < descriptors.Count; i++)
		{
			this.AddLineItem(descriptors[i].IndentedText(), descriptors[i].tooltipText);
		}
		return this;
	}

	// Token: 0x040045B3 RID: 17843
	[SerializeField]
	private InfoScreenPlainText subHeaderTemplate;

	// Token: 0x040045B4 RID: 17844
	[SerializeField]
	private InfoScreenPlainText plainTextTemplate;

	// Token: 0x040045B5 RID: 17845
	[SerializeField]
	private InfoScreenLineItem lineItemTemplate;

	// Token: 0x040045B6 RID: 17846
	[SerializeField]
	private InfoScreenSpriteItem spriteItemTemplate;

	// Token: 0x040045B7 RID: 17847
	[Space(10f)]
	[SerializeField]
	private LocText header;

	// Token: 0x040045B8 RID: 17848
	[SerializeField]
	private GameObject contentContainer;

	// Token: 0x040045B9 RID: 17849
	[SerializeField]
	private GameObject leftButtonPrefab;

	// Token: 0x040045BA RID: 17850
	[SerializeField]
	private GameObject rightButtonPrefab;

	// Token: 0x040045BB RID: 17851
	[SerializeField]
	private GameObject leftButtonPanel;

	// Token: 0x040045BC RID: 17852
	[SerializeField]
	private GameObject rightButtonPanel;

	// Token: 0x040045BD RID: 17853
	private bool escapeCloses;

	// Token: 0x040045BE RID: 17854
	public global::System.Action onDeactivateFn;
}
