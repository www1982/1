using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CC1 RID: 3265
public class DetailsScreenMaterialPanel : TargetScreen
{
	// Token: 0x0600649D RID: 25757 RVA: 0x0025CE95 File Offset: 0x0025B095
	public override bool IsValidForTarget(GameObject target)
	{
		return true;
	}

	// Token: 0x0600649E RID: 25758 RVA: 0x0025CE98 File Offset: 0x0025B098
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.openChangeMaterialPanelButton.onClick += delegate
		{
			this.OpenMaterialSelectionPanel();
			this.RefreshMaterialSelectionPanel();
			this.RefreshOrderChangeMaterialButton();
		};
	}

	// Token: 0x0600649F RID: 25759 RVA: 0x0025CEB8 File Offset: 0x0025B0B8
	public override void SetTarget(GameObject target)
	{
		if (this.selectedTarget != null)
		{
			this.selectedTarget.Unsubscribe(this.subHandle);
		}
		this.building = null;
		base.SetTarget(target);
		if (target == null)
		{
			return;
		}
		this.materialSelectionPanel.gameObject.SetActive(false);
		this.orderChangeMaterialButton.ClearOnClick();
		this.orderChangeMaterialButton.isInteractable = false;
		this.CloseMaterialSelectionPanel();
		this.building = this.selectedTarget.GetComponent<Building>();
		bool flag = Db.Get().TechItems.IsTechItemComplete(this.building.Def.PrefabID) || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive;
		this.openChangeMaterialPanelButton.isInteractable = target.GetComponent<Reconstructable>() != null && target.GetComponent<Reconstructable>().AllowReconstruct && flag;
		this.openChangeMaterialPanelButton.GetComponent<ToolTip>().SetSimpleTooltip(flag ? "" : string.Format(UI.PRODUCTINFO_REQUIRESRESEARCHDESC, Db.Get().TechItems.GetTechFromItemID(this.building.Def.PrefabID).Name));
		this.Refresh(null);
		this.subHandle = target.Subscribe(954267658, new Action<object>(this.RefreshOrderChangeMaterialButton));
		Game.Instance.Subscribe(1980521255, new Action<object>(this.Refresh));
	}

	// Token: 0x060064A0 RID: 25760 RVA: 0x0025D028 File Offset: 0x0025B228
	private void OpenMaterialSelectionPanel()
	{
		this.openChangeMaterialPanelButton.gameObject.SetActive(false);
		this.materialSelectionPanel.gameObject.SetActive(true);
		this.RefreshMaterialSelectionPanel();
		if (this.selectedTarget != null && this.building != null)
		{
			this.materialSelectionPanel.SelectSourcesMaterials(this.building);
		}
	}

	// Token: 0x060064A1 RID: 25761 RVA: 0x0025D08A File Offset: 0x0025B28A
	private void CloseMaterialSelectionPanel()
	{
		this.currentMaterialDescriptionRow.gameObject.SetActive(true);
		this.openChangeMaterialPanelButton.gameObject.SetActive(true);
		this.materialSelectionPanel.gameObject.SetActive(false);
	}

	// Token: 0x060064A2 RID: 25762 RVA: 0x0025D0BF File Offset: 0x0025B2BF
	public override void OnDeselectTarget(GameObject target)
	{
		base.OnDeselectTarget(target);
		this.Refresh(null);
	}

	// Token: 0x060064A3 RID: 25763 RVA: 0x0025D0CF File Offset: 0x0025B2CF
	private void Refresh(object data = null)
	{
		this.RefreshCurrentMaterial();
		this.RefreshMaterialSelectionPanel();
	}

	// Token: 0x060064A4 RID: 25764 RVA: 0x0025D0E0 File Offset: 0x0025B2E0
	private void RefreshCurrentMaterial()
	{
		if (this.selectedTarget == null)
		{
			return;
		}
		CellSelectionObject component = this.selectedTarget.GetComponent<CellSelectionObject>();
		Element element = ((component == null) ? this.selectedTarget.GetComponent<PrimaryElement>().Element : component.element);
		global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(element, "ui", false);
		this.currentMaterialIcon.sprite = uisprite.first;
		this.currentMaterialIcon.color = uisprite.second;
		if (component == null)
		{
			this.currentMaterialLabel.SetText(element.name + " x " + GameUtil.GetFormattedMass(this.selectedTarget.GetComponent<PrimaryElement>().Mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		}
		else
		{
			this.currentMaterialLabel.SetText(element.name);
		}
		this.currentMaterialDescription.SetText(element.description);
		List<Descriptor> materialDescriptors = GameUtil.GetMaterialDescriptors(element);
		if (materialDescriptors.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.EFFECTS_HEADER, ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.EFFECTS_HEADER, Descriptor.DescriptorType.Effect);
			materialDescriptors.Insert(0, descriptor);
			this.descriptorPanel.gameObject.SetActive(true);
			this.descriptorPanel.SetDescriptors(materialDescriptors);
			return;
		}
		this.descriptorPanel.gameObject.SetActive(false);
	}

	// Token: 0x060064A5 RID: 25765 RVA: 0x0025D22C File Offset: 0x0025B42C
	private void RefreshMaterialSelectionPanel()
	{
		if (this.selectedTarget == null)
		{
			return;
		}
		this.materialSelectionPanel.ClearSelectActions();
		if (!(this.building == null) && !(this.building is BuildingUnderConstruction))
		{
			this.materialSelectionPanel.ConfigureScreen(this.building.Def.CraftRecipe, (BuildingDef data) => true, (BuildingDef data) => "");
			this.materialSelectionPanel.AddSelectAction(new MaterialSelector.SelectMaterialActions(this.RefreshOrderChangeMaterialButton));
			Reconstructable component = this.selectedTarget.GetComponent<Reconstructable>();
			if (component != null && component.ReconstructRequested)
			{
				if (!this.materialSelectionPanel.gameObject.activeSelf)
				{
					this.OpenMaterialSelectionPanel();
					this.materialSelectionPanel.RefreshSelectors();
				}
				this.materialSelectionPanel.ForceSelectPrimaryTag(component.PrimarySelectedElementTag);
			}
		}
		this.confirmChangeRow.transform.SetAsLastSibling();
	}

	// Token: 0x060064A6 RID: 25766 RVA: 0x0025D345 File Offset: 0x0025B545
	private void RefreshOrderChangeMaterialButton()
	{
		this.RefreshOrderChangeMaterialButton(null);
	}

	// Token: 0x060064A7 RID: 25767 RVA: 0x0025D350 File Offset: 0x0025B550
	private void RefreshOrderChangeMaterialButton(object data = null)
	{
		if (this.selectedTarget == null)
		{
			return;
		}
		Reconstructable reconstructable = this.selectedTarget.GetComponent<Reconstructable>();
		bool flag = this.materialSelectionPanel.CurrentSelectedElement != null;
		this.orderChangeMaterialButton.isInteractable = flag && this.building.GetComponent<PrimaryElement>().Element.tag != this.materialSelectionPanel.CurrentSelectedElement;
		this.orderChangeMaterialButton.ClearOnClick();
		this.orderChangeMaterialButton.onClick += delegate
		{
			reconstructable.RequestReconstruct(this.materialSelectionPanel.CurrentSelectedElement);
			this.RefreshOrderChangeMaterialButton();
		};
		this.orderChangeMaterialButton.GetComponentInChildren<LocText>().SetText(reconstructable.ReconstructRequested ? UI.USERMENUACTIONS.RECONSTRUCT.CANCEL_RECONSTRUCT : UI.USERMENUACTIONS.RECONSTRUCT.REQUEST_RECONSTRUCT);
		this.orderChangeMaterialButton.GetComponent<ToolTip>().SetSimpleTooltip(reconstructable.ReconstructRequested ? UI.USERMENUACTIONS.RECONSTRUCT.CANCEL_RECONSTRUCT_TOOLTIP : UI.USERMENUACTIONS.RECONSTRUCT.REQUEST_RECONSTRUCT_TOOLTIP);
	}

	// Token: 0x040044A1 RID: 17569
	[Header("Current Material")]
	[SerializeField]
	private Image currentMaterialIcon;

	// Token: 0x040044A2 RID: 17570
	[SerializeField]
	private RectTransform currentMaterialDescriptionRow;

	// Token: 0x040044A3 RID: 17571
	[SerializeField]
	private LocText currentMaterialLabel;

	// Token: 0x040044A4 RID: 17572
	[SerializeField]
	private LocText currentMaterialDescription;

	// Token: 0x040044A5 RID: 17573
	[SerializeField]
	private DescriptorPanel descriptorPanel;

	// Token: 0x040044A6 RID: 17574
	[Header("Change Material")]
	[SerializeField]
	private MaterialSelectionPanel materialSelectionPanel;

	// Token: 0x040044A7 RID: 17575
	[SerializeField]
	private RectTransform confirmChangeRow;

	// Token: 0x040044A8 RID: 17576
	[SerializeField]
	private KButton orderChangeMaterialButton;

	// Token: 0x040044A9 RID: 17577
	[SerializeField]
	private KButton openChangeMaterialPanelButton;

	// Token: 0x040044AA RID: 17578
	private int subHandle = -1;

	// Token: 0x040044AB RID: 17579
	private Building building;
}
