using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class ElectrobankConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FDC RID: 4060 RVA: 0x0005F499 File Offset: 0x0005D699
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x06000FDD RID: 4061 RVA: 0x0005F4A0 File Offset: 0x0005D6A0
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FDE RID: 4062 RVA: 0x0005F4A4 File Offset: 0x0005D6A4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("Electrobank", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK.DESC, 20f, true, Assets.GetAnim("electrobank_large_kanim"), "idle1", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.5f, 0.8f, true, 0, SimHashes.Katairite, new List<Tag>
		{
			GameTags.ChargedPortableBattery,
			GameTags.PedestalDisplayable
		});
		if (!Assets.IsTagCountable(GameTags.ChargedPortableBattery))
		{
			Assets.AddCountableTag(GameTags.ChargedPortableBattery);
		}
		gameObject.AddTag(GameTags.IndustrialProduct);
		gameObject.AddComponent<Electrobank>().rechargeable = true;
		gameObject.AddOrGet<OccupyArea>().SetCellOffsets(EntityTemplates.GenerateOffsets(1, 1));
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.PENALTY.TIER0);
		return gameObject;
	}

	// Token: 0x06000FDF RID: 4063 RVA: 0x0005F567 File Offset: 0x0005D767
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000FE0 RID: 4064 RVA: 0x0005F569 File Offset: 0x0005D769
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A19 RID: 2585
	public const string ID = "Electrobank";

	// Token: 0x04000A1A RID: 2586
	public const float MASS = 20f;

	// Token: 0x04000A1B RID: 2587
	public const float POWER_CAPACITY = 120000f;

	// Token: 0x04000A1C RID: 2588
	public static ComplexRecipe recipe;
}
