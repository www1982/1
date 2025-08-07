using System;
using System.Collections.Generic;
using ProcGen;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C73 RID: 3187
public class ClusterCategorySelectionScreen : NewGameFlowScreen
{
	// Token: 0x0600615A RID: 24922 RVA: 0x002426B8 File Offset: 0x002408B8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.closeButton.onClick += base.NavigateBackward;
		int num = 0;
		using (Dictionary<string, ClusterLayout>.ValueCollection.Enumerator enumerator = SettingsCache.clusterLayouts.clusterCache.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.clusterCategory == ClusterLayout.ClusterCategory.Special)
				{
					num++;
				}
			}
		}
		if (num > 0)
		{
			this.eventStyle.button.gameObject.SetActive(true);
			this.eventStyle.Init(this.descriptionArea, UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.EVENT_DESC, UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.EVENT_TITLE);
			MultiToggle button = this.eventStyle.button;
			button.onClick = (global::System.Action)Delegate.Combine(button.onClick, new global::System.Action(delegate
			{
				this.OnClickOption(ClusterLayout.ClusterCategory.Special);
			}));
		}
		if (DlcManager.IsExpansion1Active())
		{
			this.classicStyle.button.gameObject.SetActive(true);
			this.classicStyle.Init(this.descriptionArea, UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.CLASSIC_DESC, UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.CLASSIC_TITLE);
			MultiToggle button2 = this.classicStyle.button;
			button2.onClick = (global::System.Action)Delegate.Combine(button2.onClick, new global::System.Action(delegate
			{
				this.OnClickOption(ClusterLayout.ClusterCategory.SpacedOutVanillaStyle);
			}));
			this.spacedOutStyle.button.gameObject.SetActive(true);
			this.spacedOutStyle.Init(this.descriptionArea, UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.SPACEDOUT_DESC, UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.SPACEDOUT_TITLE);
			MultiToggle button3 = this.spacedOutStyle.button;
			button3.onClick = (global::System.Action)Delegate.Combine(button3.onClick, new global::System.Action(delegate
			{
				this.OnClickOption(ClusterLayout.ClusterCategory.SpacedOutStyle);
			}));
			this.panel.sizeDelta = ((num > 0) ? new Vector2(622f, this.panel.sizeDelta.y) : new Vector2(480f, this.panel.sizeDelta.y));
			return;
		}
		this.vanillaStyle.button.gameObject.SetActive(true);
		this.vanillaStyle.Init(this.descriptionArea, UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.VANILLA_DESC, UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.VANILLA_TITLE);
		MultiToggle button4 = this.vanillaStyle.button;
		button4.onClick = (global::System.Action)Delegate.Combine(button4.onClick, new global::System.Action(delegate
		{
			this.OnClickOption(ClusterLayout.ClusterCategory.Vanilla);
		}));
		this.panel.sizeDelta = new Vector2(480f, this.panel.sizeDelta.y);
		this.eventStyle.kanim.Play("lab_asteroid_standard", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x0600615B RID: 24923 RVA: 0x00242974 File Offset: 0x00240B74
	private void OnClickOption(ClusterLayout.ClusterCategory clusterCategory)
	{
		this.Deactivate();
		DestinationSelectPanel.ChosenClusterCategorySetting = (int)clusterCategory;
		base.NavigateForward();
	}

	// Token: 0x040041F8 RID: 16888
	public ClusterCategorySelectionScreen.ButtonConfig vanillaStyle;

	// Token: 0x040041F9 RID: 16889
	public ClusterCategorySelectionScreen.ButtonConfig classicStyle;

	// Token: 0x040041FA RID: 16890
	public ClusterCategorySelectionScreen.ButtonConfig spacedOutStyle;

	// Token: 0x040041FB RID: 16891
	public ClusterCategorySelectionScreen.ButtonConfig eventStyle;

	// Token: 0x040041FC RID: 16892
	[SerializeField]
	private LocText descriptionArea;

	// Token: 0x040041FD RID: 16893
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040041FE RID: 16894
	[SerializeField]
	private RectTransform panel;

	// Token: 0x02001E36 RID: 7734
	[Serializable]
	public class ButtonConfig
	{
		// Token: 0x0600AFE6 RID: 45030 RVA: 0x003D1510 File Offset: 0x003CF710
		public void Init(LocText descriptionArea, string hoverDescriptionText, string headerText)
		{
			this.descriptionArea = descriptionArea;
			this.hoverDescriptionText = hoverDescriptionText;
			this.headerLabel.SetText(headerText);
			MultiToggle multiToggle = this.button;
			multiToggle.onEnter = (global::System.Action)Delegate.Combine(multiToggle.onEnter, new global::System.Action(this.OnHoverEnter));
			MultiToggle multiToggle2 = this.button;
			multiToggle2.onExit = (global::System.Action)Delegate.Combine(multiToggle2.onExit, new global::System.Action(this.OnHoverExit));
			HierarchyReferences component = this.button.GetComponent<HierarchyReferences>();
			this.headerImage = component.GetReference<RectTransform>("HeaderBackground").GetComponent<Image>();
			this.selectionFrame = component.GetReference<RectTransform>("SelectionFrame").GetComponent<Image>();
		}

		// Token: 0x0600AFE7 RID: 45031 RVA: 0x003D15C0 File Offset: 0x003CF7C0
		private void OnHoverEnter()
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
			this.selectionFrame.SetAlpha(1f);
			this.headerImage.color = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
			this.descriptionArea.text = this.hoverDescriptionText;
		}

		// Token: 0x0600AFE8 RID: 45032 RVA: 0x003D1624 File Offset: 0x003CF824
		private void OnHoverExit()
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
			this.selectionFrame.SetAlpha(0f);
			this.headerImage.color = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
			this.descriptionArea.text = UI.FRONTEND.CLUSTERCATEGORYSELECTSCREEN.BLANK_DESC;
		}

		// Token: 0x04008CE4 RID: 36068
		public MultiToggle button;

		// Token: 0x04008CE5 RID: 36069
		public Image headerImage;

		// Token: 0x04008CE6 RID: 36070
		public LocText headerLabel;

		// Token: 0x04008CE7 RID: 36071
		public Image selectionFrame;

		// Token: 0x04008CE8 RID: 36072
		public KAnimControllerBase kanim;

		// Token: 0x04008CE9 RID: 36073
		private string hoverDescriptionText;

		// Token: 0x04008CEA RID: 36074
		private LocText descriptionArea;
	}
}
