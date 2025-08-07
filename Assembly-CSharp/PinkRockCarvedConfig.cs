using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000323 RID: 803
public class PinkRockCarvedConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600108E RID: 4238 RVA: 0x00062891 File Offset: 0x00060A91
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x00062898 File Offset: 0x00060A98
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0006289C File Offset: 0x00060A9C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("PinkRockCarved", global::STRINGS.CREATURES.SPECIES.PINKROCKCARVED.NAME, global::STRINGS.CREATURES.SPECIES.PINKROCKCARVED.DESC, 1f, true, Assets.GetAnim("pinkrock_decor_kanim"), "idle", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.CIRCLE, 0.5f, 0.5f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.RareMaterials,
			GameTags.MiscPickupable,
			GameTags.PedestalDisplayable,
			GameTags.Experimental
		});
		gameObject.AddOrGet<OccupyArea>();
		DecorProvider decorProvider = gameObject.AddOrGet<DecorProvider>();
		decorProvider.SetValues(global::TUNING.BUILDINGS.DECOR.BONUS.TIER1);
		decorProvider.overrideName = gameObject.GetProperName();
		Light2D light2D = gameObject.AddOrGet<Light2D>();
		light2D.overlayColour = LIGHT2D.PINKROCK_COLOR;
		light2D.Color = LIGHT2D.PINKROCK_COLOR;
		light2D.Range = 3f;
		light2D.Angle = 0f;
		light2D.Direction = LIGHT2D.PINKROCK_DIRECTION;
		light2D.Offset = LIGHT2D.PINKROCK_OFFSET;
		light2D.shape = global::LightShape.Circle;
		light2D.drawOverlay = true;
		light2D.disableOnStore = true;
		gameObject.GetComponent<KCircleCollider2D>().offset = new Vector2(0f, 0.25f);
		return gameObject;
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x000629C1 File Offset: 0x00060BC1
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x000629C3 File Offset: 0x00060BC3
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A84 RID: 2692
	public const string ID = "PinkRockCarved";
}
