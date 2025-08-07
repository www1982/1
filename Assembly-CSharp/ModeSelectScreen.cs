using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D70 RID: 3440
public class ModeSelectScreen : NewGameFlowScreen
{
	// Token: 0x06006AED RID: 27373 RVA: 0x00286217 File Offset: 0x00284417
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.LoadWorldAndClusterData();
	}

	// Token: 0x06006AEE RID: 27374 RVA: 0x00286228 File Offset: 0x00284428
	protected override void OnSpawn()
	{
		base.OnSpawn();
		HierarchyReferences component = this.survivalButton.GetComponent<HierarchyReferences>();
		this.survivalButtonHeader = component.GetReference<RectTransform>("HeaderBackground").GetComponent<Image>();
		this.survivalButtonSelectionFrame = component.GetReference<RectTransform>("SelectionFrame").GetComponent<Image>();
		MultiToggle multiToggle = this.survivalButton;
		multiToggle.onEnter = (global::System.Action)Delegate.Combine(multiToggle.onEnter, new global::System.Action(this.OnHoverEnterSurvival));
		MultiToggle multiToggle2 = this.survivalButton;
		multiToggle2.onExit = (global::System.Action)Delegate.Combine(multiToggle2.onExit, new global::System.Action(this.OnHoverExitSurvival));
		MultiToggle multiToggle3 = this.survivalButton;
		multiToggle3.onClick = (global::System.Action)Delegate.Combine(multiToggle3.onClick, new global::System.Action(this.OnClickSurvival));
		HierarchyReferences component2 = this.nosweatButton.GetComponent<HierarchyReferences>();
		this.nosweatButtonHeader = component2.GetReference<RectTransform>("HeaderBackground").GetComponent<Image>();
		this.nosweatButtonSelectionFrame = component2.GetReference<RectTransform>("SelectionFrame").GetComponent<Image>();
		MultiToggle multiToggle4 = this.nosweatButton;
		multiToggle4.onEnter = (global::System.Action)Delegate.Combine(multiToggle4.onEnter, new global::System.Action(this.OnHoverEnterNosweat));
		MultiToggle multiToggle5 = this.nosweatButton;
		multiToggle5.onExit = (global::System.Action)Delegate.Combine(multiToggle5.onExit, new global::System.Action(this.OnHoverExitNosweat));
		MultiToggle multiToggle6 = this.nosweatButton;
		multiToggle6.onClick = (global::System.Action)Delegate.Combine(multiToggle6.onClick, new global::System.Action(this.OnClickNosweat));
		this.closeButton.onClick += base.NavigateBackward;
	}

	// Token: 0x06006AEF RID: 27375 RVA: 0x002863AC File Offset: 0x002845AC
	private void OnHoverEnterSurvival()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.survivalButtonSelectionFrame.SetAlpha(1f);
		this.survivalButtonHeader.color = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
		this.descriptionArea.text = UI.FRONTEND.MODESELECTSCREEN.SURVIVAL_DESC;
	}

	// Token: 0x06006AF0 RID: 27376 RVA: 0x00286414 File Offset: 0x00284614
	private void OnHoverExitSurvival()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.survivalButtonSelectionFrame.SetAlpha(0f);
		this.survivalButtonHeader.color = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
		this.descriptionArea.text = UI.FRONTEND.MODESELECTSCREEN.BLANK_DESC;
	}

	// Token: 0x06006AF1 RID: 27377 RVA: 0x0028647A File Offset: 0x0028467A
	private void OnClickSurvival()
	{
		this.Deactivate();
		CustomGameSettings.Instance.SetSurvivalDefaults();
		base.NavigateForward();
	}

	// Token: 0x06006AF2 RID: 27378 RVA: 0x00286492 File Offset: 0x00284692
	private void LoadWorldAndClusterData()
	{
		if (ModeSelectScreen.dataLoaded)
		{
			return;
		}
		CustomGameSettings.Instance.LoadClusters();
		Global.Instance.modManager.Report(base.gameObject);
		ModeSelectScreen.dataLoaded = true;
	}

	// Token: 0x06006AF3 RID: 27379 RVA: 0x002864C4 File Offset: 0x002846C4
	private void OnHoverEnterNosweat()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.nosweatButtonSelectionFrame.SetAlpha(1f);
		this.nosweatButtonHeader.color = new Color(0.7019608f, 0.3647059f, 0.53333336f, 1f);
		this.descriptionArea.text = UI.FRONTEND.MODESELECTSCREEN.NOSWEAT_DESC;
	}

	// Token: 0x06006AF4 RID: 27380 RVA: 0x0028652C File Offset: 0x0028472C
	private void OnHoverExitNosweat()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Mouseover", false));
		this.nosweatButtonSelectionFrame.SetAlpha(0f);
		this.nosweatButtonHeader.color = new Color(0.30980393f, 0.34117648f, 0.38431373f, 1f);
		this.descriptionArea.text = UI.FRONTEND.MODESELECTSCREEN.BLANK_DESC;
	}

	// Token: 0x06006AF5 RID: 27381 RVA: 0x00286592 File Offset: 0x00284792
	private void OnClickNosweat()
	{
		this.Deactivate();
		CustomGameSettings.Instance.SetNosweatDefaults();
		base.NavigateForward();
	}

	// Token: 0x040048CF RID: 18639
	[SerializeField]
	private MultiToggle nosweatButton;

	// Token: 0x040048D0 RID: 18640
	private Image nosweatButtonHeader;

	// Token: 0x040048D1 RID: 18641
	private Image nosweatButtonSelectionFrame;

	// Token: 0x040048D2 RID: 18642
	[SerializeField]
	private MultiToggle survivalButton;

	// Token: 0x040048D3 RID: 18643
	private Image survivalButtonHeader;

	// Token: 0x040048D4 RID: 18644
	private Image survivalButtonSelectionFrame;

	// Token: 0x040048D5 RID: 18645
	[SerializeField]
	private LocText descriptionArea;

	// Token: 0x040048D6 RID: 18646
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040048D7 RID: 18647
	[SerializeField]
	private KBatchedAnimController nosweatAnim;

	// Token: 0x040048D8 RID: 18648
	[SerializeField]
	private KBatchedAnimController survivalAnim;

	// Token: 0x040048D9 RID: 18649
	private static bool dataLoaded;
}
