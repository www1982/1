using System;
using System.Collections.Generic;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D8C RID: 3468
public class OutfitDescriptionPanel : KMonoBehaviour
{
	// Token: 0x06006BFB RID: 27643 RVA: 0x0028BFDE File Offset: 0x0028A1DE
	public void Refresh(PermitResource permitResource, ClothingOutfitUtility.OutfitType outfitType, Option<Personality> personality)
	{
		if (permitResource != null)
		{
			this.Refresh(permitResource.Name, new string[] { permitResource.Id }, outfitType, personality);
			return;
		}
		this.Refresh(UI.OUTFIT_NAME.NONE, OutfitDescriptionPanel.NO_ITEMS, outfitType, personality);
	}

	// Token: 0x06006BFC RID: 27644 RVA: 0x0028C018 File Offset: 0x0028A218
	public void Refresh(Option<ClothingOutfitTarget> outfit, ClothingOutfitUtility.OutfitType outfitType, Option<Personality> personality)
	{
		if (outfit.IsSome())
		{
			this.Refresh(outfit.Unwrap().ReadName(), outfit.Unwrap().ReadItems(), outfitType, personality);
			if (personality.IsNone() && outfit.IsSome())
			{
				ClothingOutfitTarget.Implementation impl = outfit.Unwrap().impl;
				if (impl is ClothingOutfitTarget.DatabaseAuthoredTemplate)
				{
					ClothingOutfitTarget.DatabaseAuthoredTemplate databaseAuthoredTemplate = (ClothingOutfitTarget.DatabaseAuthoredTemplate)impl;
					string dlcIdFrom = databaseAuthoredTemplate.resource.GetDlcIdFrom();
					if (DlcManager.IsDlcId(dlcIdFrom))
					{
						this.collectionLabel.text = UI.KLEI_INVENTORY_SCREEN.COLLECTION.Replace("{Collection}", DlcManager.GetDlcTitle(dlcIdFrom));
						this.collectionLabel.gameObject.SetActive(true);
						this.collectionLabel.transform.SetAsLastSibling();
						return;
					}
				}
			}
		}
		else
		{
			this.Refresh(KleiItemsUI.GetNoneOutfitName(outfitType), OutfitDescriptionPanel.NO_ITEMS, outfitType, personality);
		}
	}

	// Token: 0x06006BFD RID: 27645 RVA: 0x0028C0F4 File Offset: 0x0028A2F4
	public void Refresh(OutfitDesignerScreen_OutfitState outfitState, Option<Personality> personality)
	{
		this.Refresh(outfitState.name, outfitState.GetItems(), outfitState.outfitType, personality);
	}

	// Token: 0x06006BFE RID: 27646 RVA: 0x0028C110 File Offset: 0x0028A310
	public void Refresh(string outfitName, string[] outfitItemIds, ClothingOutfitUtility.OutfitType outfitType, Option<Personality> personality)
	{
		this.ClearItemDescRows();
		using (DictionaryPool<PermitCategory, Option<PermitResource>, OutfitDescriptionPanel>.PooledDictionary pooledDictionary = PoolsFor<OutfitDescriptionPanel>.AllocateDict<PermitCategory, Option<PermitResource>>())
		{
			using (ListPool<PermitResource, OutfitDescriptionPanel>.PooledList pooledList = PoolsFor<OutfitDescriptionPanel>.AllocateList<PermitResource>())
			{
				switch (outfitType)
				{
				case ClothingOutfitUtility.OutfitType.Clothing:
					this.outfitNameLabel.SetText(outfitName);
					this.outfitDescriptionLabel.gameObject.SetActive(false);
					foreach (PermitCategory permitCategory in ClothingOutfitUtility.PERMIT_CATEGORIES_FOR_CLOTHING)
					{
						pooledDictionary.Add(permitCategory, Option.None);
					}
					break;
				case ClothingOutfitUtility.OutfitType.JoyResponse:
					if (outfitItemIds != null && outfitItemIds.Length != 0)
					{
						if (Db.Get().Permits.BalloonArtistFacades.TryGet(outfitItemIds[0]) != null)
						{
							this.outfitDescriptionLabel.gameObject.SetActive(true);
							string text = DUPLICANTS.TRAITS.BALLOONARTIST.NAME;
							this.outfitNameLabel.SetText(text);
							this.outfitDescriptionLabel.SetText(outfitName);
						}
					}
					else
					{
						this.outfitNameLabel.SetText(outfitName);
						this.outfitDescriptionLabel.gameObject.SetActive(false);
					}
					pooledDictionary.Add(PermitCategory.JoyResponse, Option.None);
					break;
				case ClothingOutfitUtility.OutfitType.AtmoSuit:
					this.outfitNameLabel.SetText(outfitName);
					this.outfitDescriptionLabel.gameObject.SetActive(false);
					foreach (PermitCategory permitCategory2 in ClothingOutfitUtility.PERMIT_CATEGORIES_FOR_ATMO_SUITS)
					{
						pooledDictionary.Add(permitCategory2, Option.None);
					}
					break;
				}
				foreach (string text2 in outfitItemIds)
				{
					PermitResource permitResource = Db.Get().Permits.Get(text2);
					Option<PermitResource> option;
					if (pooledDictionary.TryGetValue(permitResource.Category, out option) && !option.HasValue)
					{
						pooledDictionary[permitResource.Category] = permitResource;
					}
					else
					{
						pooledList.Add(permitResource);
					}
				}
				foreach (KeyValuePair<PermitCategory, Option<PermitResource>> keyValuePair in pooledDictionary)
				{
					PermitCategory permitCategory3;
					Option<PermitResource> option2;
					keyValuePair.Deconstruct(out permitCategory3, out option2);
					PermitCategory permitCategory4 = permitCategory3;
					Option<PermitResource> option3 = option2;
					if (option3.HasValue)
					{
						this.AddItemDescRow(option3.Value);
					}
					else
					{
						this.AddItemDescRow(KleiItemsUI.GetNoneClothingItemIcon(permitCategory4, personality), KleiItemsUI.GetNoneClothingItemStrings(permitCategory4).Item1, null, 1f);
					}
				}
				foreach (PermitResource permitResource2 in pooledList)
				{
					ClothingItemResource clothingItemResource = (ClothingItemResource)permitResource2;
					this.AddItemDescRow(clothingItemResource);
				}
			}
		}
		bool flag = ClothingOutfitTarget.DoesContainLockedItems(outfitItemIds);
		this.usesUnownedItemsLabel.transform.SetAsLastSibling();
		if (!flag)
		{
			this.usesUnownedItemsLabel.gameObject.SetActive(false);
		}
		else
		{
			this.usesUnownedItemsLabel.SetText(KleiItemsUI.WrapWithColor(UI.OUTFIT_DESCRIPTION.CONTAINS_NON_OWNED_ITEMS, KleiItemsUI.TEXT_COLOR__PERMIT_NOT_OWNED));
			this.usesUnownedItemsLabel.gameObject.SetActive(true);
		}
		this.collectionLabel.gameObject.SetActive(false);
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.Refresh(outfitName, outfitItemIds, outfitType, personality);
		});
	}

	// Token: 0x06006BFF RID: 27647 RVA: 0x0028C4F0 File Offset: 0x0028A6F0
	private void ClearItemDescRows()
	{
		for (int i = 0; i < this.itemDescriptionRows.Count; i++)
		{
			global::UnityEngine.Object.Destroy(this.itemDescriptionRows[i]);
		}
		this.itemDescriptionRows.Clear();
	}

	// Token: 0x06006C00 RID: 27648 RVA: 0x0028C530 File Offset: 0x0028A730
	private void AddItemDescRow(PermitResource permit)
	{
		PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
		bool flag = permit.IsUnlocked();
		string text = (flag ? null : UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWN_NONE);
		this.AddItemDescRow(permitPresentationInfo.sprite, permit.Name, text, flag ? 1f : 0.7f);
	}

	// Token: 0x06006C01 RID: 27649 RVA: 0x0028C580 File Offset: 0x0028A780
	private void AddItemDescRow(Sprite icon, string text, string tooltip = null, float alpha = 1f)
	{
		GameObject gameObject = Util.KInstantiateUI(this.itemDescriptionRowPrefab, this.itemDescriptionContainer, true);
		this.itemDescriptionRows.Add(gameObject);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("Icon").sprite = icon;
		component.GetReference<LocText>("Label").SetText(text);
		gameObject.AddOrGet<CanvasGroup>().alpha = alpha;
		gameObject.AddOrGet<NonDrawingGraphic>();
		if (tooltip != null)
		{
			gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(tooltip);
			return;
		}
		gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
	}

	// Token: 0x0400499A RID: 18842
	[SerializeField]
	public LocText outfitNameLabel;

	// Token: 0x0400499B RID: 18843
	[SerializeField]
	public LocText outfitDescriptionLabel;

	// Token: 0x0400499C RID: 18844
	[SerializeField]
	private GameObject itemDescriptionRowPrefab;

	// Token: 0x0400499D RID: 18845
	[SerializeField]
	private GameObject itemDescriptionContainer;

	// Token: 0x0400499E RID: 18846
	[SerializeField]
	private LocText collectionLabel;

	// Token: 0x0400499F RID: 18847
	[SerializeField]
	private LocText usesUnownedItemsLabel;

	// Token: 0x040049A0 RID: 18848
	private List<GameObject> itemDescriptionRows = new List<GameObject>();

	// Token: 0x040049A1 RID: 18849
	public static readonly string[] NO_ITEMS = new string[0];
}
