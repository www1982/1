using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Database;
using UnityEngine;

// Token: 0x02000559 RID: 1369
public class BlueprintCollection
{
	// Token: 0x06001E38 RID: 7736 RVA: 0x000A48E6 File Offset: 0x000A2AE6
	public void AddBlueprintsFrom<T>(T provider) where T : BlueprintProvider
	{
		provider.blueprintCollection = this;
		provider.Internal_PreSetupBlueprints();
		provider.SetupBlueprints();
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x000A490C File Offset: 0x000A2B0C
	public void AddBlueprintsFrom(BlueprintCollection collection)
	{
		this.artables.AddRange(collection.artables);
		this.buildingFacades.AddRange(collection.buildingFacades);
		this.clothingItems.AddRange(collection.clothingItems);
		this.balloonArtistFacades.AddRange(collection.balloonArtistFacades);
		this.stickerBombFacades.AddRange(collection.stickerBombFacades);
		this.equippableFacades.AddRange(collection.equippableFacades);
		this.monumentParts.AddRange(collection.monumentParts);
		this.outfits.AddRange(collection.outfits);
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x000A49A4 File Offset: 0x000A2BA4
	public void PostProcess()
	{
		if (Application.isPlaying)
		{
			this.artables.RemoveAll(new Predicate<ArtableInfo>(BlueprintCollection.<PostProcess>g__ShouldExcludeBlueprint|10_0));
			this.buildingFacades.RemoveAll(new Predicate<BuildingFacadeInfo>(BlueprintCollection.<PostProcess>g__ShouldExcludeBlueprint|10_0));
			this.clothingItems.RemoveAll(new Predicate<ClothingItemInfo>(BlueprintCollection.<PostProcess>g__ShouldExcludeBlueprint|10_0));
			this.balloonArtistFacades.RemoveAll(new Predicate<BalloonArtistFacadeInfo>(BlueprintCollection.<PostProcess>g__ShouldExcludeBlueprint|10_0));
			this.stickerBombFacades.RemoveAll(new Predicate<StickerBombFacadeInfo>(BlueprintCollection.<PostProcess>g__ShouldExcludeBlueprint|10_0));
			this.equippableFacades.RemoveAll(new Predicate<EquippableFacadeInfo>(BlueprintCollection.<PostProcess>g__ShouldExcludeBlueprint|10_0));
			this.monumentParts.RemoveAll(new Predicate<MonumentPartInfo>(BlueprintCollection.<PostProcess>g__ShouldExcludeBlueprint|10_0));
			this.outfits.RemoveAll(new Predicate<ClothingOutfitResource>(BlueprintCollection.<PostProcess>g__ShouldExcludeBlueprint|10_0));
		}
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x000A4AE8 File Offset: 0x000A2CE8
	[CompilerGenerated]
	internal static bool <PostProcess>g__ShouldExcludeBlueprint|10_0(IHasDlcRestrictions blueprintDlcInfo)
	{
		if (!DlcManager.IsCorrectDlcSubscribed(blueprintDlcInfo))
		{
			return true;
		}
		IBlueprintInfo blueprintInfo = blueprintDlcInfo as IBlueprintInfo;
		KAnimFile kanimFile;
		if (blueprintInfo != null && !Assets.TryGetAnim(blueprintInfo.animFile, out kanimFile))
		{
			DebugUtil.DevAssert(false, string.Concat(new string[] { "Couldnt find anim \"", blueprintInfo.animFile, "\" for blueprint \"", blueprintInfo.id, "\"" }), null);
		}
		return false;
	}

	// Token: 0x0400119A RID: 4506
	public List<ArtableInfo> artables = new List<ArtableInfo>();

	// Token: 0x0400119B RID: 4507
	public List<BuildingFacadeInfo> buildingFacades = new List<BuildingFacadeInfo>();

	// Token: 0x0400119C RID: 4508
	public List<ClothingItemInfo> clothingItems = new List<ClothingItemInfo>();

	// Token: 0x0400119D RID: 4509
	public List<BalloonArtistFacadeInfo> balloonArtistFacades = new List<BalloonArtistFacadeInfo>();

	// Token: 0x0400119E RID: 4510
	public List<StickerBombFacadeInfo> stickerBombFacades = new List<StickerBombFacadeInfo>();

	// Token: 0x0400119F RID: 4511
	public List<EquippableFacadeInfo> equippableFacades = new List<EquippableFacadeInfo>();

	// Token: 0x040011A0 RID: 4512
	public List<MonumentPartInfo> monumentParts = new List<MonumentPartInfo>();

	// Token: 0x040011A1 RID: 4513
	public List<ClothingOutfitResource> outfits = new List<ClothingOutfitResource>();
}
