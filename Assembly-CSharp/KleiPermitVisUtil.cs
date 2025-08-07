using System;
using Database;
using UnityEngine;

// Token: 0x02000D1D RID: 3357
public static class KleiPermitVisUtil
{
	// Token: 0x06006762 RID: 26466 RVA: 0x0026FAD8 File Offset: 0x0026DCD8
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, BuildingFacadeResource buildingPermit)
	{
		KAnimFile anim = Assets.GetAnim(buildingPermit.AnimFile);
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(new KAnimFile[] { anim });
		buildingKAnim.Play(KleiPermitVisUtil.GetFirstAnimHash(anim), KAnim.PlayMode.Loop, 1f, 0f);
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06006763 RID: 26467 RVA: 0x0026FB40 File Offset: 0x0026DD40
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, BuildingDef buildingDef)
	{
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(buildingDef.AnimFiles);
		buildingKAnim.Play(KleiPermitVisUtil.GetFirstAnimHash(buildingDef.AnimFiles[0]), KAnim.PlayMode.Loop, 1f, 0f);
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06006764 RID: 26468 RVA: 0x0026FB98 File Offset: 0x0026DD98
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, ArtableStage artablePermit)
	{
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(new KAnimFile[] { Assets.GetAnim(artablePermit.animFile) });
		buildingKAnim.Play(artablePermit.anim, KAnim.PlayMode.Once, 1f, 0f);
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06006765 RID: 26469 RVA: 0x0026FC00 File Offset: 0x0026DE00
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, DbStickerBomb artablePermit)
	{
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(new KAnimFile[] { artablePermit.animFile });
		HashedString defaultStickerAnimHash = KleiPermitVisUtil.GetDefaultStickerAnimHash(artablePermit.animFile);
		if (defaultStickerAnimHash != null)
		{
			buildingKAnim.Play(defaultStickerAnimHash, KAnim.PlayMode.Once, 1f, 0f);
		}
		else
		{
			global::Debug.Assert(false, "Couldn't find default sticker for sticker " + artablePermit.Id);
			buildingKAnim.Play(KleiPermitVisUtil.GetFirstAnimHash(artablePermit.animFile), KAnim.PlayMode.Once, 1f, 0f);
		}
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06006766 RID: 26470 RVA: 0x0026FCA4 File Offset: 0x0026DEA4
	public static void ConfigureToRenderBuilding(KBatchedAnimController buildingKAnim, MonumentPartResource monumentPermit)
	{
		buildingKAnim.Stop();
		buildingKAnim.SwapAnims(new KAnimFile[] { monumentPermit.AnimFile });
		buildingKAnim.Play(monumentPermit.State, KAnim.PlayMode.Once, 1f, 0f);
		buildingKAnim.rectTransform().sizeDelta = 176f * Vector2.one;
	}

	// Token: 0x06006767 RID: 26471 RVA: 0x0026FD04 File Offset: 0x0026DF04
	public static void ConfigureBuildingPosition(RectTransform transform, PrefabDefinedUIPosition anchorPosition, BuildingDef buildingDef, Alignment alignment)
	{
		anchorPosition.SetOn(transform);
		transform.anchoredPosition += new Vector2(176f * (float)buildingDef.WidthInCells * -(alignment.x - 0.5f), 176f * (float)buildingDef.HeightInCells * -alignment.y);
	}

	// Token: 0x06006768 RID: 26472 RVA: 0x0026FD60 File Offset: 0x0026DF60
	public static void ConfigureBuildingPosition(RectTransform transform, Vector2 anchorPosition, BuildingDef buildingDef, Alignment alignment)
	{
		transform.anchoredPosition = anchorPosition + new Vector2(176f * (float)buildingDef.WidthInCells * -(alignment.x - 0.5f), 176f * (float)buildingDef.HeightInCells * -alignment.y);
	}

	// Token: 0x06006769 RID: 26473 RVA: 0x0026FDAE File Offset: 0x0026DFAE
	public static void ClearAnimation()
	{
		if (!KleiPermitVisUtil.buildingAnimateIn.IsNullOrDestroyed())
		{
			global::UnityEngine.Object.Destroy(KleiPermitVisUtil.buildingAnimateIn.gameObject);
		}
	}

	// Token: 0x0600676A RID: 26474 RVA: 0x0026FDCB File Offset: 0x0026DFCB
	public static void AnimateIn(KBatchedAnimController buildingKAnim, Updater extraUpdater = default(Updater))
	{
		KleiPermitVisUtil.ClearAnimation();
		KleiPermitVisUtil.buildingAnimateIn = KleiPermitBuildingAnimateIn.MakeFor(buildingKAnim, extraUpdater);
	}

	// Token: 0x0600676B RID: 26475 RVA: 0x0026FDDE File Offset: 0x0026DFDE
	public static HashedString GetFirstAnimHash(KAnimFile animFile)
	{
		return animFile.GetData().GetAnim(0).hash;
	}

	// Token: 0x0600676C RID: 26476 RVA: 0x0026FDF4 File Offset: 0x0026DFF4
	public static HashedString GetDefaultStickerAnimHash(KAnimFile stickerAnimFile)
	{
		KAnimFileData data = stickerAnimFile.GetData();
		for (int i = 0; i < data.animCount; i++)
		{
			KAnim.Anim anim = data.GetAnim(i);
			if (anim.name.StartsWith("idle_sticker"))
			{
				return anim.hash;
			}
		}
		return null;
	}

	// Token: 0x0600676D RID: 26477 RVA: 0x0026FE40 File Offset: 0x0026E040
	public static BuildLocationRule? GetBuildLocationRule(PermitResource permit)
	{
		BuildingDef buildingDef = KleiPermitVisUtil.GetBuildingDef(permit);
		if (buildingDef == null)
		{
			return null;
		}
		return new BuildLocationRule?(buildingDef.BuildLocationRule);
	}

	// Token: 0x0600676E RID: 26478 RVA: 0x0026FE74 File Offset: 0x0026E074
	public static BuildingDef GetBuildingDef(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = permit as BuildingFacadeResource;
		if (buildingFacadeResource != null)
		{
			GameObject gameObject = Assets.TryGetPrefab(buildingFacadeResource.PrefabID);
			if (gameObject == null)
			{
				return null;
			}
			BuildingComplete component = gameObject.GetComponent<BuildingComplete>();
			if (component == null || !component)
			{
				return null;
			}
			return component.Def;
		}
		else
		{
			ArtableStage artableStage = permit as ArtableStage;
			if (artableStage != null)
			{
				BuildingComplete component2 = Assets.GetPrefab(artableStage.prefabId).GetComponent<BuildingComplete>();
				if (component2 == null || !component2)
				{
					return null;
				}
				return component2.Def;
			}
			else
			{
				if (!(permit is MonumentPartResource))
				{
					return null;
				}
				BuildingComplete component3 = Assets.GetPrefab("MonumentBottom").GetComponent<BuildingComplete>();
				if (component3 == null || !component3)
				{
					return null;
				}
				return component3.Def;
			}
		}
	}

	// Token: 0x040046DE RID: 18142
	public const float TILE_SIZE_UI = 176f;

	// Token: 0x040046DF RID: 18143
	public static KleiPermitBuildingAnimateIn buildingAnimateIn;
}
