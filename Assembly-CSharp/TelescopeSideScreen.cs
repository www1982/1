using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E3E RID: 3646
public class TelescopeSideScreen : SideScreenContent
{
	// Token: 0x0600738A RID: 29578 RVA: 0x002BE678 File Offset: 0x002BC878
	public TelescopeSideScreen()
	{
		this.refreshDisplayStateDelegate = new Action<object>(this.RefreshDisplayState);
	}

	// Token: 0x0600738B RID: 29579 RVA: 0x002BE694 File Offset: 0x002BC894
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.selectStarmapScreen.onClick += delegate
		{
			ManagementMenu.Instance.ToggleStarmap();
		};
		SpacecraftManager.instance.Subscribe(532901469, this.refreshDisplayStateDelegate);
		this.RefreshDisplayState(null);
	}

	// Token: 0x0600738C RID: 29580 RVA: 0x002BE6EE File Offset: 0x002BC8EE
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.RefreshDisplayState(null);
		this.target = SelectTool.Instance.selected.GetComponent<KMonoBehaviour>().gameObject;
	}

	// Token: 0x0600738D RID: 29581 RVA: 0x002BE717 File Offset: 0x002BC917
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.target)
		{
			this.target = null;
		}
	}

	// Token: 0x0600738E RID: 29582 RVA: 0x002BE733 File Offset: 0x002BC933
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.target)
		{
			this.target = null;
		}
	}

	// Token: 0x0600738F RID: 29583 RVA: 0x002BE74F File Offset: 0x002BC94F
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Telescope>() != null;
	}

	// Token: 0x06007390 RID: 29584 RVA: 0x002BE760 File Offset: 0x002BC960
	private void RefreshDisplayState(object data = null)
	{
		if (SelectTool.Instance.selected == null)
		{
			return;
		}
		if (SelectTool.Instance.selected.GetComponent<Telescope>() == null)
		{
			return;
		}
		if (!SpacecraftManager.instance.HasAnalysisTarget())
		{
			this.DescriptionText.text = "<b><color=#FF0000>" + UI.UISIDESCREENS.TELESCOPESIDESCREEN.NO_SELECTED_ANALYSIS_TARGET + "</color></b>";
			return;
		}
		string text = UI.UISIDESCREENS.TELESCOPESIDESCREEN.ANALYSIS_TARGET_SELECTED;
		this.DescriptionText.text = text;
	}

	// Token: 0x04004F88 RID: 20360
	public KButton selectStarmapScreen;

	// Token: 0x04004F89 RID: 20361
	public Image researchButtonIcon;

	// Token: 0x04004F8A RID: 20362
	public GameObject content;

	// Token: 0x04004F8B RID: 20363
	private GameObject target;

	// Token: 0x04004F8C RID: 20364
	private Action<object> refreshDisplayStateDelegate;

	// Token: 0x04004F8D RID: 20365
	public LocText DescriptionText;
}
