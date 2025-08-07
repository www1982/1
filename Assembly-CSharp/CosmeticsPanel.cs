using System;
using System.Collections.Generic;
using Database;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CA3 RID: 3235
public class CosmeticsPanel : TargetPanel
{
	// Token: 0x06006385 RID: 25477 RVA: 0x002561D3 File Offset: 0x002543D3
	public override bool IsValidForTarget(GameObject target)
	{
		return true;
	}

	// Token: 0x06006386 RID: 25478 RVA: 0x002561D8 File Offset: 0x002543D8
	protected override void OnSelectTarget(GameObject target)
	{
		base.OnSelectTarget(target);
		BuildingFacade buildingFacade = this.selectedTarget.GetComponent<BuildingFacade>();
		MinionIdentity component = this.selectedTarget.GetComponent<MinionIdentity>();
		this.selectionPanel.OnFacadeSelectionChanged = null;
		this.outfitCategoryButtonContainer.SetActive(component != null);
		if (component != null)
		{
			ClothingOutfitTarget outfitTarget = ClothingOutfitTarget.FromMinion(this.selectedOutfitCategory, component.gameObject);
			this.selectionPanel.SetOutfitTarget(outfitTarget, this.selectedOutfitCategory);
			FacadeSelectionPanel facadeSelectionPanel = this.selectionPanel;
			facadeSelectionPanel.OnFacadeSelectionChanged = (global::System.Action)Delegate.Combine(facadeSelectionPanel.OnFacadeSelectionChanged, new global::System.Action(delegate
			{
				if (this.selectionPanel.SelectedFacade == null || this.selectionPanel.SelectedFacade == "DEFAULT_FACADE")
				{
					outfitTarget.WriteItems(this.selectedOutfitCategory, new string[0]);
				}
				else
				{
					outfitTarget.WriteItems(this.selectedOutfitCategory, ClothingOutfitTarget.FromTemplateId(this.selectionPanel.SelectedFacade).ReadItems());
				}
				this.Refresh();
			}));
		}
		else if (buildingFacade != null)
		{
			this.selectionPanel.SetBuildingDef(this.selectedTarget.GetComponent<Building>().Def.PrefabID, this.selectedTarget.GetComponent<BuildingFacade>().CurrentFacade);
			this.selectionPanel.OnFacadeSelectionChanged = null;
			FacadeSelectionPanel facadeSelectionPanel2 = this.selectionPanel;
			facadeSelectionPanel2.OnFacadeSelectionChanged = (global::System.Action)Delegate.Combine(facadeSelectionPanel2.OnFacadeSelectionChanged, new global::System.Action(delegate
			{
				if (this.selectionPanel.SelectedFacade == null || this.selectionPanel.SelectedFacade == "DEFAULT_FACADE" || Db.GetBuildingFacades().TryGet(this.selectionPanel.SelectedFacade).IsNullOrDestroyed())
				{
					buildingFacade.ApplyDefaultFacade(true);
				}
				else
				{
					buildingFacade.ApplyBuildingFacade(Db.GetBuildingFacades().Get(this.selectionPanel.SelectedFacade), true);
				}
				this.Refresh();
			}));
		}
		this.Refresh();
	}

	// Token: 0x06006387 RID: 25479 RVA: 0x00256318 File Offset: 0x00254518
	public override void OnDeselectTarget(GameObject target)
	{
		base.OnDeselectTarget(target);
	}

	// Token: 0x06006388 RID: 25480 RVA: 0x00256324 File Offset: 0x00254524
	public void Refresh()
	{
		global::UnityEngine.Object component = this.selectedTarget.GetComponent<MinionIdentity>();
		BuildingFacade component2 = this.selectedTarget.GetComponent<BuildingFacade>();
		if (component != null)
		{
			ClothingOutfitTarget clothingOutfitTarget = ClothingOutfitTarget.FromMinion(this.selectedOutfitCategory, this.selectedTarget);
			this.editButton.gameObject.SetActive(true);
			this.mannequin.gameObject.SetActive(true);
			this.mannequin.SetOutfit(clothingOutfitTarget);
			this.buildingIcon.gameObject.SetActive(false);
			this.editButton.ClearOnClick();
			this.editButton.onClick += this.OnClickEditOutfit;
			this.nameLabel.SetText(clothingOutfitTarget.ReadName());
			this.descriptionLabel.SetText("");
		}
		else if (component2 != null)
		{
			this.editButton.gameObject.SetActive(false);
			this.mannequin.gameObject.SetActive(false);
			this.buildingIcon.gameObject.SetActive(true);
			if (component2.CurrentFacade != null && component2.CurrentFacade != "DEFAULT_FACADE" && !Db.GetBuildingFacades().TryGet(component2.CurrentFacade).IsNullOrDestroyed())
			{
				BuildingFacadeResource buildingFacadeResource = Db.GetBuildingFacades().Get(component2.CurrentFacade);
				this.nameLabel.SetText(buildingFacadeResource.Name);
				this.descriptionLabel.SetText(buildingFacadeResource.Description);
				this.buildingIcon.sprite = Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim(buildingFacadeResource.AnimFile), "ui", false, "");
			}
			else
			{
				string prefabID = component2.GetComponent<Building>().Def.PrefabID;
				StringEntry stringEntry;
				Strings.TryGet(string.Concat(new string[]
				{
					"STRINGS.BUILDINGS.PREFABS.",
					prefabID.ToString().ToUpperInvariant(),
					".FACADES.DEFAULT_",
					prefabID.ToString().ToUpperInvariant(),
					".NAME"
				}), out stringEntry);
				if (stringEntry == null)
				{
					Strings.TryGet("STRINGS.BUILDINGS.PREFABS." + prefabID.ToString().ToUpperInvariant() + ".NAME", out stringEntry);
				}
				StringEntry stringEntry2;
				Strings.TryGet(string.Concat(new string[]
				{
					"STRINGS.BUILDINGS.PREFABS.",
					prefabID.ToString().ToUpperInvariant(),
					".FACADES.DEFAULT_",
					prefabID.ToString().ToUpperInvariant(),
					".DESC"
				}), out stringEntry2);
				if (stringEntry2 == null)
				{
					Strings.TryGet("STRINGS.BUILDINGS.PREFABS." + prefabID.ToString().ToUpperInvariant() + ".DESC", out stringEntry2);
				}
				this.nameLabel.SetText((stringEntry != null) ? stringEntry : "");
				this.descriptionLabel.SetText((stringEntry2 != null) ? stringEntry2 : "");
				this.buildingIcon.sprite = Def.GetUISprite(prefabID, "ui", false).first;
			}
		}
		this.RefreshOutfitCategories();
		this.selectionPanel.Refresh();
	}

	// Token: 0x06006389 RID: 25481 RVA: 0x00256618 File Offset: 0x00254818
	public void OnClickEditOutfit()
	{
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot);
		MinionBrowserScreenConfig.MinionInstances(this.selectedTarget).ApplyAndOpenScreen(delegate
		{
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSupplyClosetSnapshot, STOP_MODE.ALLOWFADEOUT);
		});
	}

	// Token: 0x0600638A RID: 25482 RVA: 0x00256674 File Offset: 0x00254874
	private void RefreshOutfitCategories()
	{
		foreach (KeyValuePair<ClothingOutfitUtility.OutfitType, GameObject> keyValuePair in this.outfitCategories)
		{
			global::Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.outfitCategories.Clear();
		string[] array = new string[] { "outfit", "atmosuit" };
		Dictionary<ClothingOutfitUtility.OutfitType, string> dictionary = new Dictionary<ClothingOutfitUtility.OutfitType, string>();
		dictionary.Add(ClothingOutfitUtility.OutfitType.Clothing, UI.UISIDESCREENS.BLUEPRINT_TAB.SUBCATEGORY_OUTFIT);
		dictionary.Add(ClothingOutfitUtility.OutfitType.AtmoSuit, UI.UISIDESCREENS.BLUEPRINT_TAB.SUBCATEGORY_ATMOSUIT);
		for (int i = 0; i < 3; i++)
		{
			if (i != 1)
			{
				int idx = i;
				GameObject gameObject = global::Util.KInstantiateUI(this.outfitCategoryButtonPrefab, this.outfitCategoryButtonContainer, true);
				this.outfitCategories.Add((ClothingOutfitUtility.OutfitType)idx, gameObject);
				gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(dictionary[(ClothingOutfitUtility.OutfitType)i]);
				MultiToggle component = gameObject.GetComponent<MultiToggle>();
				component.onClick = (global::System.Action)Delegate.Combine(component.onClick, new global::System.Action(delegate
				{
					this.selectedOutfitCategory = (ClothingOutfitUtility.OutfitType)idx;
					this.Refresh();
					this.selectionPanel.SelectedOutfitCategory = this.selectedOutfitCategory;
				}));
				component.ChangeState((this.selectedOutfitCategory == (ClothingOutfitUtility.OutfitType)idx) ? 1 : 0);
			}
		}
	}

	// Token: 0x040043A9 RID: 17321
	[SerializeField]
	private GameObject cosmeticSlotContainer;

	// Token: 0x040043AA RID: 17322
	[SerializeField]
	private FacadeSelectionPanel selectionPanel;

	// Token: 0x040043AB RID: 17323
	[SerializeField]
	private LocText nameLabel;

	// Token: 0x040043AC RID: 17324
	[SerializeField]
	private LocText descriptionLabel;

	// Token: 0x040043AD RID: 17325
	[SerializeField]
	private KButton editButton;

	// Token: 0x040043AE RID: 17326
	[SerializeField]
	private UIMannequin mannequin;

	// Token: 0x040043AF RID: 17327
	[SerializeField]
	private Image buildingIcon;

	// Token: 0x040043B0 RID: 17328
	[SerializeField]
	private Dictionary<ClothingOutfitUtility.OutfitType, GameObject> outfitCategories = new Dictionary<ClothingOutfitUtility.OutfitType, GameObject>();

	// Token: 0x040043B1 RID: 17329
	[SerializeField]
	private GameObject outfitCategoryButtonPrefab;

	// Token: 0x040043B2 RID: 17330
	[SerializeField]
	private GameObject outfitCategoryButtonContainer;

	// Token: 0x040043B3 RID: 17331
	private ClothingOutfitUtility.OutfitType selectedOutfitCategory;
}
