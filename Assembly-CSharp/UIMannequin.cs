using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using UnityEngine;

// Token: 0x02000E6F RID: 3695
public class UIMannequin : KMonoBehaviour, UIMinionOrMannequin.ITarget
{
	// Token: 0x17000812 RID: 2066
	// (get) Token: 0x060075DC RID: 30172 RVA: 0x002D2080 File Offset: 0x002D0280
	public GameObject SpawnedAvatar
	{
		get
		{
			if (this.spawn == null)
			{
				this.TrySpawn();
			}
			return this.spawn;
		}
	}

	// Token: 0x17000813 RID: 2067
	// (get) Token: 0x060075DD RID: 30173 RVA: 0x002D209C File Offset: 0x002D029C
	public Option<Personality> Personality
	{
		get
		{
			return default(Option<Personality>);
		}
	}

	// Token: 0x060075DE RID: 30174 RVA: 0x002D20B2 File Offset: 0x002D02B2
	protected override void OnSpawn()
	{
		this.TrySpawn();
	}

	// Token: 0x060075DF RID: 30175 RVA: 0x002D20BC File Offset: 0x002D02BC
	public void TrySpawn()
	{
		if (this.animController == null)
		{
			this.animController = Util.KInstantiateUI(Assets.GetPrefab(MannequinUIPortrait.ID), base.gameObject, false).GetComponent<KBatchedAnimController>();
			this.animController.LoadAnims();
			this.animController.gameObject.SetActive(true);
			this.animController.animScale = 0.38f;
			this.animController.Play("idle", KAnim.PlayMode.Paused, 1f, 0f);
			this.spawn = this.animController.gameObject;
			BaseMinionConfig.ConfigureSymbols(this.spawn, false);
			base.gameObject.AddOrGet<MinionVoiceProviderMB>().voice = Option.None;
		}
	}

	// Token: 0x060075E0 RID: 30176 RVA: 0x002D2184 File Offset: 0x002D0384
	public void SetOutfit(ClothingOutfitUtility.OutfitType outfitType, IEnumerable<ClothingItemResource> outfit)
	{
		bool flag = outfit.Count<ClothingItemResource>() == 0;
		if (this.shouldShowOutfitWithDefaultItems)
		{
			outfit = UIMinionOrMannequinITargetExtensions.GetOutfitWithDefaultItems(outfitType, outfit);
		}
		this.SpawnedAvatar.GetComponent<SymbolOverrideController>().RemoveAllSymbolOverrides(0);
		BaseMinionConfig.ConfigureSymbols(this.SpawnedAvatar, false);
		Accessorizer component = this.SpawnedAvatar.GetComponent<Accessorizer>();
		WearableAccessorizer component2 = this.SpawnedAvatar.GetComponent<WearableAccessorizer>();
		component.ApplyMinionPersonality(this.personalityToUseForDefaultClothing.UnwrapOr(Db.Get().Personalities.Get("ABE"), null));
		component2.ClearClothingItems(null);
		component2.ApplyClothingItems(outfitType, outfit);
		List<KAnimHashedString> list = new List<KAnimHashedString>(32);
		if (this.shouldShowOutfitWithDefaultItems && outfitType == ClothingOutfitUtility.OutfitType.Clothing)
		{
			list.Add("foot");
			list.Add("hand_paint");
			if (flag)
			{
				list.Add("belt");
			}
			if (!outfit.Select((ClothingItemResource item) => item.Category).Contains(PermitCategory.DupeTops))
			{
				list.Add("torso");
				list.Add("neck");
				list.Add("arm_lower");
				list.Add("arm_lower_sleeve");
				list.Add("arm_sleeve");
				list.Add("cuff");
			}
			if (!outfit.Select((ClothingItemResource item) => item.Category).Contains(PermitCategory.DupeGloves))
			{
				list.Add("arm_lower_sleeve");
				list.Add("cuff");
			}
			if (!outfit.Select((ClothingItemResource item) => item.Category).Contains(PermitCategory.DupeBottoms))
			{
				list.Add("leg");
				list.Add("pelvis");
			}
		}
		KAnimHashedString[] array = outfit.SelectMany((ClothingItemResource item) => item.AnimFile.GetData().build.symbols.Select((KAnim.Build.Symbol s) => s.hash)).Concat(list).ToArray<KAnimHashedString>();
		foreach (KAnim.Build.Symbol symbol in this.animController.AnimFiles[0].GetData().build.symbols)
		{
			if (symbol.hash == "mannequin_arm" || symbol.hash == "mannequin_body" || symbol.hash == "mannequin_headshape" || symbol.hash == "mannequin_leg")
			{
				this.animController.SetSymbolVisiblity(symbol.hash, true);
			}
			else
			{
				this.animController.SetSymbolVisiblity(symbol.hash, array.Contains(symbol.hash));
			}
		}
	}

	// Token: 0x060075E1 RID: 30177 RVA: 0x002D2494 File Offset: 0x002D0694
	private static ClothingItemResource GetItemForCategory(PermitCategory category, IEnumerable<ClothingItemResource> outfit)
	{
		foreach (ClothingItemResource clothingItemResource in outfit)
		{
			if (clothingItemResource.Category == category)
			{
				return clothingItemResource;
			}
		}
		return null;
	}

	// Token: 0x060075E2 RID: 30178 RVA: 0x002D24E8 File Offset: 0x002D06E8
	public void React(UIMinionOrMannequinReactSource source)
	{
		this.animController.Play("idle", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x040051C2 RID: 20930
	public const float ANIM_SCALE = 0.38f;

	// Token: 0x040051C3 RID: 20931
	private KBatchedAnimController animController;

	// Token: 0x040051C4 RID: 20932
	private GameObject spawn;

	// Token: 0x040051C5 RID: 20933
	public bool shouldShowOutfitWithDefaultItems = true;

	// Token: 0x040051C6 RID: 20934
	public Option<Personality> personalityToUseForDefaultClothing;
}
