using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000304 RID: 772
public class EmptyElectrobankConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FE2 RID: 4066 RVA: 0x0005F573 File Offset: 0x0005D773
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x06000FE3 RID: 4067 RVA: 0x0005F57A File Offset: 0x0005D77A
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FE4 RID: 4068 RVA: 0x0005F580 File Offset: 0x0005D780
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("EmptyElectrobank", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_EMPTY.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_EMPTY.DESC, 20f, true, Assets.GetAnim("electrobank_large_depleted_kanim"), "idle1", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.5f, 0.8f, true, 0, SimHashes.Katairite, new List<Tag>
		{
			GameTags.EmptyPortableBattery,
			GameTags.PedestalDisplayable
		});
		if (!Assets.IsTagCountable(GameTags.EmptyPortableBattery))
		{
			Assets.AddCountableTag(GameTags.EmptyPortableBattery);
		}
		gameObject.GetComponent<KCollider2D>();
		gameObject.AddTag(GameTags.IndustrialProduct);
		gameObject.AddOrGet<OccupyArea>().SetCellOffsets(EntityTemplates.GenerateOffsets(1, 1));
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.PENALTY.TIER0);
		return gameObject;
	}

	// Token: 0x06000FE5 RID: 4069 RVA: 0x0005F63E File Offset: 0x0005D83E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000FE6 RID: 4070 RVA: 0x0005F640 File Offset: 0x0005D840
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A1D RID: 2589
	public const string ID = "EmptyElectrobank";

	// Token: 0x04000A1E RID: 2590
	public const float MASS = 20f;
}
