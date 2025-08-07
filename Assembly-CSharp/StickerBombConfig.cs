using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class StickerBombConfig : IEntityConfig
{
	// Token: 0x06001599 RID: 5529 RVA: 0x0007AC9C File Offset: 0x00078E9C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity("StickerBomb", global::STRINGS.BUILDINGS.PREFABS.STICKERBOMB.NAME, global::STRINGS.BUILDINGS.PREFABS.STICKERBOMB.DESC, 1f, true, Assets.GetAnim("sticker_a_kanim"), "off", Grid.SceneLayer.Backwall, SimHashes.Creature, null, 293f);
		EntityTemplates.AddCollision(gameObject, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f);
		gameObject.AddOrGet<StickerBomb>();
		return gameObject;
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x0007AD06 File Offset: 0x00078F06
	public void OnPrefabInit(GameObject inst)
	{
		inst.AddOrGet<OccupyArea>().SetCellOffsets(new CellOffset[1]);
		inst.AddComponent<Modifiers>();
		inst.AddOrGet<DecorProvider>().SetValues(DECOR.BONUS.TIER2);
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x0007AD30 File Offset: 0x00078F30
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000CCD RID: 3277
	public const string ID = "StickerBomb";
}
