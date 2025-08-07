using System;
using System.Collections.Generic;
using Database;

// Token: 0x02000562 RID: 1378
public abstract class BlueprintProvider : IHasDlcRestrictions
{
	// Token: 0x06001EA2 RID: 7842 RVA: 0x000A50E0 File Offset: 0x000A32E0
	protected void AddBuilding(string prefabConfigId, PermitRarity rarity, string permitId, string animFile)
	{
		this.blueprintCollection.buildingFacades.Add(new BuildingFacadeInfo(permitId, Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".NAME"), Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".DESC"), rarity, prefabConfigId, animFile, null, this.requiredDlcIds, this.forbiddenDlcIds));
	}

	// Token: 0x06001EA3 RID: 7843 RVA: 0x000A5154 File Offset: 0x000A3354
	protected void AddBuildingWithInteract(string prefabConfigId, PermitRarity rarity, string permitId, string animFile, Dictionary<string, string> interact_anim)
	{
		this.blueprintCollection.buildingFacades.Add(new BuildingFacadeInfo(permitId, Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".NAME"), Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".DESC"), rarity, prefabConfigId, animFile, interact_anim, this.requiredDlcIds, this.forbiddenDlcIds));
	}

	// Token: 0x06001EA4 RID: 7844 RVA: 0x000A51C8 File Offset: 0x000A33C8
	protected void AddClothing(BlueprintProvider.ClothingType clothingType, PermitRarity rarity, string permitId, string animFile)
	{
		this.blueprintCollection.clothingItems.Add(new ClothingItemInfo(permitId, Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".NAME"), Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".DESC"), (PermitCategory)clothingType, rarity, animFile, this.requiredDlcIds, this.forbiddenDlcIds));
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x000A523C File Offset: 0x000A343C
	protected BlueprintProvider.ArtableInfoAuthoringHelper AddArtable(BlueprintProvider.ArtableType artableType, PermitRarity rarity, string permitId, string animFile)
	{
		string text;
		switch (artableType)
		{
		case BlueprintProvider.ArtableType.Painting:
			text = "Canvas";
			break;
		case BlueprintProvider.ArtableType.PaintingTall:
			text = "CanvasTall";
			break;
		case BlueprintProvider.ArtableType.PaintingWide:
			text = "CanvasWide";
			break;
		case BlueprintProvider.ArtableType.Sculpture:
			text = "Sculpture";
			break;
		case BlueprintProvider.ArtableType.SculptureSmall:
			text = "SmallSculpture";
			break;
		case BlueprintProvider.ArtableType.SculptureIce:
			text = "IceSculpture";
			break;
		case BlueprintProvider.ArtableType.SculptureMetal:
			text = "MetalSculpture";
			break;
		case BlueprintProvider.ArtableType.SculptureMarble:
			text = "MarbleSculpture";
			break;
		case BlueprintProvider.ArtableType.SculptureWood:
			text = "WoodSculpture";
			break;
		case BlueprintProvider.ArtableType.FossilSculpture:
			text = "FossilSculpture";
			break;
		case BlueprintProvider.ArtableType.CeilingFossilSculpture:
			text = "CeilingFossilSculpture";
			break;
		default:
			text = null;
			break;
		}
		bool flag = true;
		if (text == null)
		{
			DebugUtil.DevAssert(false, "Failed to get buildingConfigId from " + artableType.ToString(), null);
			flag = false;
		}
		BlueprintProvider.ArtableInfoAuthoringHelper artableInfoAuthoringHelper;
		if (flag)
		{
			KAnimFile kanimFile;
			ArtableInfo artableInfo = new ArtableInfo(permitId, Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".NAME"), Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".DESC"), rarity, animFile, (!Assets.TryGetAnim(animFile, out kanimFile)) ? null : kanimFile.GetData().GetAnim(0).name, 0, false, "error", text, "", this.requiredDlcIds, this.forbiddenDlcIds);
			artableInfoAuthoringHelper = new BlueprintProvider.ArtableInfoAuthoringHelper(artableType, artableInfo);
			artableInfoAuthoringHelper.Quality(BlueprintProvider.ArtableQuality.LookingGreat);
			this.blueprintCollection.artables.Add(artableInfo);
		}
		else
		{
			artableInfoAuthoringHelper = default(BlueprintProvider.ArtableInfoAuthoringHelper);
		}
		return artableInfoAuthoringHelper;
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x000A53B8 File Offset: 0x000A35B8
	protected void AddJoyResponse(BlueprintProvider.JoyResponseType joyResponseType, PermitRarity rarity, string permitId, string animFile)
	{
		if (joyResponseType == BlueprintProvider.JoyResponseType.BallonSet)
		{
			this.blueprintCollection.balloonArtistFacades.Add(new BalloonArtistFacadeInfo(permitId, Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".NAME"), Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".DESC"), rarity, animFile, BalloonArtistFacadeType.ThreeSet, this.requiredDlcIds, this.forbiddenDlcIds));
			return;
		}
		throw new NotImplementedException("Missing case for " + joyResponseType.ToString());
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x000A544C File Offset: 0x000A364C
	protected void AddOutfit(BlueprintProvider.OutfitType outfitType, string outfitId, string[] permitIdList)
	{
		this.blueprintCollection.outfits.Add(new ClothingOutfitResource(outfitId, permitIdList, Strings.Get("STRINGS.BLUEPRINTS." + outfitId.ToUpper() + ".NAME"), (ClothingOutfitUtility.OutfitType)outfitType, this.requiredDlcIds, this.forbiddenDlcIds));
	}

	// Token: 0x06001EA8 RID: 7848 RVA: 0x000A549C File Offset: 0x000A369C
	protected void AddMonumentPart(BlueprintProvider.MonumentPart part, PermitRarity rarity, string permitId, string animFile)
	{
		string text = "";
		switch (part)
		{
		case BlueprintProvider.MonumentPart.Bottom:
			text = "base";
			break;
		case BlueprintProvider.MonumentPart.Middle:
			text = "mid";
			break;
		case BlueprintProvider.MonumentPart.Top:
			text = "top";
			break;
		}
		this.blueprintCollection.monumentParts.Add(new MonumentPartInfo(permitId, Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".NAME"), Strings.Get("STRINGS.BLUEPRINTS." + permitId.ToUpper() + ".DESC"), rarity, animFile, permitId.Replace("permit_", ""), text, (MonumentPartResource.Part)part, this.requiredDlcIds, this.forbiddenDlcIds));
	}

	// Token: 0x06001EA9 RID: 7849 RVA: 0x000A554E File Offset: 0x000A374E
	public virtual string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06001EAA RID: 7850 RVA: 0x000A5556 File Offset: 0x000A3756
	public virtual string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x06001EAB RID: 7851
	public abstract void SetupBlueprints();

	// Token: 0x06001EAC RID: 7852 RVA: 0x000A555E File Offset: 0x000A375E
	public void Internal_PreSetupBlueprints()
	{
		this.requiredDlcIds = this.GetRequiredDlcIds();
		this.forbiddenDlcIds = this.GetForbiddenDlcIds();
	}

	// Token: 0x040011E4 RID: 4580
	public BlueprintCollection blueprintCollection;

	// Token: 0x040011E5 RID: 4581
	private string[] requiredDlcIds;

	// Token: 0x040011E6 RID: 4582
	private string[] forbiddenDlcIds;

	// Token: 0x020013A2 RID: 5026
	public enum ArtableType
	{
		// Token: 0x04006A26 RID: 27174
		Painting,
		// Token: 0x04006A27 RID: 27175
		PaintingTall,
		// Token: 0x04006A28 RID: 27176
		PaintingWide,
		// Token: 0x04006A29 RID: 27177
		Sculpture,
		// Token: 0x04006A2A RID: 27178
		SculptureSmall,
		// Token: 0x04006A2B RID: 27179
		SculptureIce,
		// Token: 0x04006A2C RID: 27180
		SculptureMetal,
		// Token: 0x04006A2D RID: 27181
		SculptureMarble,
		// Token: 0x04006A2E RID: 27182
		SculptureWood,
		// Token: 0x04006A2F RID: 27183
		FossilSculpture,
		// Token: 0x04006A30 RID: 27184
		CeilingFossilSculpture
	}

	// Token: 0x020013A3 RID: 5027
	public enum ArtableQuality
	{
		// Token: 0x04006A32 RID: 27186
		LookingGreat,
		// Token: 0x04006A33 RID: 27187
		LookingOkay,
		// Token: 0x04006A34 RID: 27188
		LookingUgly
	}

	// Token: 0x020013A4 RID: 5028
	public enum ClothingType
	{
		// Token: 0x04006A36 RID: 27190
		DupeTops = 1,
		// Token: 0x04006A37 RID: 27191
		DupeBottoms,
		// Token: 0x04006A38 RID: 27192
		DupeGloves,
		// Token: 0x04006A39 RID: 27193
		DupeShoes,
		// Token: 0x04006A3A RID: 27194
		DupeHats,
		// Token: 0x04006A3B RID: 27195
		DupeAccessories,
		// Token: 0x04006A3C RID: 27196
		AtmoSuitHelmet,
		// Token: 0x04006A3D RID: 27197
		AtmoSuitBody,
		// Token: 0x04006A3E RID: 27198
		AtmoSuitGloves,
		// Token: 0x04006A3F RID: 27199
		AtmoSuitBelt,
		// Token: 0x04006A40 RID: 27200
		AtmoSuitShoes
	}

	// Token: 0x020013A5 RID: 5029
	public enum OutfitType
	{
		// Token: 0x04006A42 RID: 27202
		Clothing,
		// Token: 0x04006A43 RID: 27203
		AtmoSuit = 2
	}

	// Token: 0x020013A6 RID: 5030
	public enum JoyResponseType
	{
		// Token: 0x04006A45 RID: 27205
		BallonSet
	}

	// Token: 0x020013A7 RID: 5031
	public enum MonumentPart
	{
		// Token: 0x04006A47 RID: 27207
		Bottom,
		// Token: 0x04006A48 RID: 27208
		Top = 2,
		// Token: 0x04006A49 RID: 27209
		Middle = 1
	}

	// Token: 0x020013A8 RID: 5032
	protected readonly ref struct ArtableInfoAuthoringHelper
	{
		// Token: 0x06008AE8 RID: 35560 RVA: 0x00351BE2 File Offset: 0x0034FDE2
		public ArtableInfoAuthoringHelper(BlueprintProvider.ArtableType artableType, ArtableInfo artableInfo)
		{
			this.artableType = artableType;
			this.artableInfo = artableInfo;
		}

		// Token: 0x06008AE9 RID: 35561 RVA: 0x00351BF4 File Offset: 0x0034FDF4
		public void Quality(BlueprintProvider.ArtableQuality artableQuality)
		{
			if (this.artableInfo == null)
			{
				return;
			}
			int num;
			int num2;
			int num3;
			if (this.artableType == BlueprintProvider.ArtableType.SculptureWood)
			{
				num = 4;
				num2 = 8;
				num3 = 12;
			}
			else
			{
				num = 5;
				num2 = 10;
				num3 = 15;
			}
			int num4;
			bool flag;
			string text;
			switch (artableQuality)
			{
			case BlueprintProvider.ArtableQuality.LookingGreat:
				num4 = num3;
				flag = true;
				text = "LookingGreat";
				break;
			case BlueprintProvider.ArtableQuality.LookingOkay:
				num4 = num2;
				flag = false;
				text = "LookingOkay";
				break;
			case BlueprintProvider.ArtableQuality.LookingUgly:
				num4 = num;
				flag = false;
				text = "LookingUgly";
				break;
			default:
				throw new ArgumentException();
			}
			this.artableInfo.decor_value = num4;
			this.artableInfo.cheer_on_complete = flag;
			this.artableInfo.status_id = text;
		}

		// Token: 0x04006A4A RID: 27210
		private readonly BlueprintProvider.ArtableType artableType;

		// Token: 0x04006A4B RID: 27211
		private readonly ArtableInfo artableInfo;
	}
}
