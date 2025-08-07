using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D0A RID: 3338
public class KleiPermitDioramaVis : KMonoBehaviour
{
	// Token: 0x060066FE RID: 26366 RVA: 0x0026E5A7 File Offset: 0x0026C7A7
	protected override void OnPrefabInit()
	{
		this.Init();
	}

	// Token: 0x060066FF RID: 26367 RVA: 0x0026E5B0 File Offset: 0x0026C7B0
	private void Init()
	{
		if (this.initComplete)
		{
			return;
		}
		this.allVisList = ReflectionUtil.For<KleiPermitDioramaVis>(this).CollectValuesForFieldsThatInheritOrImplement<IKleiPermitDioramaVisTarget>(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
		foreach (IKleiPermitDioramaVisTarget kleiPermitDioramaVisTarget in this.allVisList)
		{
			kleiPermitDioramaVisTarget.ConfigureSetup();
		}
		this.initComplete = true;
	}

	// Token: 0x06006700 RID: 26368 RVA: 0x0026E620 File Offset: 0x0026C820
	public void ConfigureWith(PermitResource permit)
	{
		if (!this.initComplete)
		{
			this.Init();
		}
		foreach (IKleiPermitDioramaVisTarget kleiPermitDioramaVisTarget in this.allVisList)
		{
			kleiPermitDioramaVisTarget.GetGameObject().SetActive(false);
		}
		KleiPermitVisUtil.ClearAnimation();
		IKleiPermitDioramaVisTarget permitVisTarget = this.GetPermitVisTarget(permit);
		permitVisTarget.GetGameObject().SetActive(true);
		permitVisTarget.ConfigureWith(permit);
		string dlcIdFrom = permit.GetDlcIdFrom();
		if (DlcManager.IsDlcId(dlcIdFrom))
		{
			this.dlcImage.gameObject.SetActive(true);
			this.dlcImage.sprite = Assets.GetSprite(DlcManager.GetDlcSmallLogo(dlcIdFrom));
			return;
		}
		this.dlcImage.gameObject.SetActive(false);
	}

	// Token: 0x06006701 RID: 26369 RVA: 0x0026E6EC File Offset: 0x0026C8EC
	private IKleiPermitDioramaVisTarget GetPermitVisTarget(PermitResource permit)
	{
		KleiPermitDioramaVis.lastRenderedPermit = permit;
		if (permit == null)
		{
			return this.fallbackVis.WithError(string.Format("Given invalid permit: {0}", permit));
		}
		if (permit.Category == PermitCategory.Equipment || permit.Category == PermitCategory.DupeTops || permit.Category == PermitCategory.DupeBottoms || permit.Category == PermitCategory.DupeGloves || permit.Category == PermitCategory.DupeShoes || permit.Category == PermitCategory.DupeHats || permit.Category == PermitCategory.DupeAccessories || permit.Category == PermitCategory.AtmoSuitHelmet || permit.Category == PermitCategory.AtmoSuitBody || permit.Category == PermitCategory.AtmoSuitGloves || permit.Category == PermitCategory.AtmoSuitBelt || permit.Category == PermitCategory.AtmoSuitShoes)
		{
			return this.equipmentVis;
		}
		if (permit.Category == PermitCategory.Building)
		{
			BuildLocationRule? buildLocationRule = KleiPermitVisUtil.GetBuildLocationRule(permit);
			BuildingDef buildingDef = KleiPermitVisUtil.GetBuildingDef(permit);
			if (!buildingDef.BuildingComplete.GetComponent<Bed>().IsNullOrDestroyed())
			{
				return this.buildingOnFloorVis;
			}
			BuildingFacadeResource buildingFacadeResource = permit as BuildingFacadeResource;
			if (buildingFacadeResource != null)
			{
				if (buildingFacadeResource.PrefabID.Contains("Wire") || buildingFacadeResource.PrefabID.Contains("Ribbon"))
				{
					return this.buildingWiresAndAutomationVis;
				}
				if (buildingFacadeResource.PrefabID.Contains("Logic"))
				{
					return this.buildingAutomationGatesVis;
				}
			}
			if (buildingDef.PrefabID == "RockCrusher" || buildingDef.PrefabID == "GasReservoir" || buildingDef.PrefabID == "ArcadeMachine" || buildingDef.PrefabID == "MicrobeMusher" || buildingDef.PrefabID == "FlushToilet" || buildingDef.PrefabID == "WashSink" || buildingDef.PrefabID == "Headquarters" || buildingDef.PrefabID == "GourmetCookingStation")
			{
				return this.buildingOnFloorBigVis;
			}
			if (!buildingDef.BuildingComplete.GetComponent<RocketModule>().IsNullOrDestroyed() || !buildingDef.BuildingComplete.GetComponent<RocketEngine>().IsNullOrDestroyed())
			{
				return this.buildingRocketVis;
			}
			if (buildingDef.PrefabID == "PlanterBox" || buildingDef.PrefabID == "FlowerVase")
			{
				return this.buildingOnFloorBotanicalVis;
			}
			if (buildingDef.PrefabID == "ExteriorWall")
			{
				return this.wallpaperVis;
			}
			if (buildingDef.PrefabID == "FlowerVaseHanging" || buildingDef.PrefabID == "FlowerVaseHangingFancy")
			{
				return this.buildingHangingHookBotanicalVis;
			}
			if (buildLocationRule != null)
			{
				BuildLocationRule valueOrDefault = buildLocationRule.GetValueOrDefault();
				switch (valueOrDefault)
				{
				case BuildLocationRule.OnFloor:
					break;
				case BuildLocationRule.OnFloorOverSpace:
					goto IL_02B9;
				case BuildLocationRule.OnCeiling:
					return this.buildingOnCeilingVis.WithAlignment(Alignment.Top());
				case BuildLocationRule.OnWall:
					return this.buildingOnWallVis.WithAlignment(Alignment.Left());
				case BuildLocationRule.InCorner:
					return this.buildingInCeilingCornerVis.WithAlignment(Alignment.TopLeft());
				default:
					if (valueOrDefault != BuildLocationRule.OnFoundationRotatable)
					{
						goto IL_02B9;
					}
					break;
				}
				return this.buildingOnFloorVis;
			}
			IL_02B9:
			return this.fallbackVis.WithError(string.Format("No visualization available for building with BuildLocationRule of {0}", buildLocationRule));
		}
		else if (permit.Category == PermitCategory.Artwork)
		{
			BuildingDef buildingDef2 = KleiPermitVisUtil.GetBuildingDef(permit);
			if (buildingDef2.IsNullOrDestroyed())
			{
				return this.fallbackVis.WithError("Couldn't find building def for Artable " + permit.Id);
			}
			if (KleiPermitDioramaVis.<GetPermitVisTarget>g__Has|24_0<Sculpture>(buildingDef2))
			{
				if (buildingDef2.PrefabID == "WoodSculpture")
				{
					return this.artablePaintingVis;
				}
				return this.artableSculptureVis;
			}
			else
			{
				if (KleiPermitDioramaVis.<GetPermitVisTarget>g__Has|24_0<Painting>(buildingDef2))
				{
					return this.artablePaintingVis;
				}
				if (KleiPermitDioramaVis.<GetPermitVisTarget>g__Has|24_0<MonumentPart>(buildingDef2))
				{
					return this.monumentPartVis;
				}
				return this.fallbackVis.WithError("No visualization available for Artable " + permit.Id);
			}
		}
		else
		{
			if (permit.Category != PermitCategory.JoyResponse)
			{
				return this.fallbackVis.WithError("No visualization has been defined for permit with id \"" + permit.Id + "\"");
			}
			if (permit is BalloonArtistFacadeResource)
			{
				return this.joyResponseBalloonVis;
			}
			return this.fallbackVis.WithError("No visualization available for JoyResponse " + permit.Id);
		}
	}

	// Token: 0x06006702 RID: 26370 RVA: 0x0026EAC4 File Offset: 0x0026CCC4
	public static Sprite GetDioramaBackground(PermitCategory permitCategory)
	{
		switch (permitCategory)
		{
		case PermitCategory.DupeTops:
		case PermitCategory.DupeBottoms:
		case PermitCategory.DupeGloves:
		case PermitCategory.DupeShoes:
		case PermitCategory.DupeHats:
		case PermitCategory.DupeAccessories:
			return Assets.GetSprite("screen_bg_clothing");
		case PermitCategory.AtmoSuitHelmet:
		case PermitCategory.AtmoSuitBody:
		case PermitCategory.AtmoSuitGloves:
		case PermitCategory.AtmoSuitBelt:
		case PermitCategory.AtmoSuitShoes:
			return Assets.GetSprite("screen_bg_atmosuit");
		case PermitCategory.Building:
			return Assets.GetSprite("screen_bg_buildings");
		case PermitCategory.Artwork:
			return Assets.GetSprite("screen_bg_art");
		case PermitCategory.JoyResponse:
			return Assets.GetSprite("screen_bg_joyresponse");
		}
		return null;
	}

	// Token: 0x06006703 RID: 26371 RVA: 0x0026EB70 File Offset: 0x0026CD70
	public static Sprite GetDioramaBackground(ClothingOutfitUtility.OutfitType outfitType)
	{
		switch (outfitType)
		{
		case ClothingOutfitUtility.OutfitType.Clothing:
			return Assets.GetSprite("screen_bg_clothing");
		case ClothingOutfitUtility.OutfitType.JoyResponse:
			return Assets.GetSprite("screen_bg_joyresponse");
		case ClothingOutfitUtility.OutfitType.AtmoSuit:
			return Assets.GetSprite("screen_bg_atmosuit");
		default:
			return null;
		}
	}

	// Token: 0x06006705 RID: 26373 RVA: 0x0026EBCA File Offset: 0x0026CDCA
	[CompilerGenerated]
	internal static bool <GetPermitVisTarget>g__Has|24_0<T>(BuildingDef buildingDef) where T : Component
	{
		return !buildingDef.BuildingComplete.GetComponent<T>().IsNullOrDestroyed();
	}

	// Token: 0x04004692 RID: 18066
	[SerializeField]
	private Image dlcImage;

	// Token: 0x04004693 RID: 18067
	[SerializeField]
	private KleiPermitDioramaVis_Fallback fallbackVis;

	// Token: 0x04004694 RID: 18068
	[SerializeField]
	private KleiPermitDioramaVis_DupeEquipment equipmentVis;

	// Token: 0x04004695 RID: 18069
	[SerializeField]
	private KleiPermitDioramaVis_BuildingOnFloor buildingOnFloorVis;

	// Token: 0x04004696 RID: 18070
	[SerializeField]
	private KleiPermitDioramaVis_BuildingOnFloorBig buildingOnFloorBigVis;

	// Token: 0x04004697 RID: 18071
	[SerializeField]
	private KleiPermitDioramaVis_BuildingPresentationStand buildingOnWallVis;

	// Token: 0x04004698 RID: 18072
	[SerializeField]
	private KleiPermitDioramaVis_BuildingPresentationStand buildingOnCeilingVis;

	// Token: 0x04004699 RID: 18073
	[SerializeField]
	private KleiPermitDioramaVis_BuildingPresentationStand buildingInCeilingCornerVis;

	// Token: 0x0400469A RID: 18074
	[SerializeField]
	private KleiPermitDioramaVis_BuildingRocket buildingRocketVis;

	// Token: 0x0400469B RID: 18075
	[SerializeField]
	private KleiPermitDioramaVis_BuildingOnFloor buildingOnFloorBotanicalVis;

	// Token: 0x0400469C RID: 18076
	[SerializeField]
	private KleiPermitDioramaVis_BuildingHangingHook buildingHangingHookBotanicalVis;

	// Token: 0x0400469D RID: 18077
	[SerializeField]
	private KleiPermitDioramaVis_WiresAndAutomation buildingWiresAndAutomationVis;

	// Token: 0x0400469E RID: 18078
	[SerializeField]
	private KleiPermitDioramaVis_AutomationGates buildingAutomationGatesVis;

	// Token: 0x0400469F RID: 18079
	[SerializeField]
	private KleiPermitDioramaVis_Wallpaper wallpaperVis;

	// Token: 0x040046A0 RID: 18080
	[SerializeField]
	private KleiPermitDioramaVis_ArtablePainting artablePaintingVis;

	// Token: 0x040046A1 RID: 18081
	[SerializeField]
	private KleiPermitDioramaVis_ArtableSculpture artableSculptureVis;

	// Token: 0x040046A2 RID: 18082
	[SerializeField]
	private KleiPermitDioramaVis_JoyResponseBalloon joyResponseBalloonVis;

	// Token: 0x040046A3 RID: 18083
	[SerializeField]
	private KleiPermitDioramaVis_MonumentPart monumentPartVis;

	// Token: 0x040046A4 RID: 18084
	private bool initComplete;

	// Token: 0x040046A5 RID: 18085
	private IReadOnlyList<IKleiPermitDioramaVisTarget> allVisList;

	// Token: 0x040046A6 RID: 18086
	public static PermitResource lastRenderedPermit;
}
