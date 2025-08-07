using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003DF RID: 991
public class CryoTankConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001443 RID: 5187 RVA: 0x000745EB File Offset: 0x000727EB
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x000745F2 File Offset: 0x000727F2
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x000745F8 File Offset: 0x000727F8
	public GameObject CreatePrefab()
	{
		string text = "CryoTank";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.CRYOTANK.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.CRYOTANK.DESC;
		float num = 100f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("cryo_chamber_kanim"), "off", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<KAnimControllerBase>().SetFGLayer(Grid.SceneLayer.BuildingFront);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		Workable workable = gameObject.AddOrGet<Workable>();
		workable.synchronizeAnims = false;
		workable.resetProgressOnStop = true;
		CryoTank cryoTank = gameObject.AddOrGet<CryoTank>();
		cryoTank.overrideAnim = "anim_interacts_cryo_activation_kanim";
		cryoTank.dropOffset = new CellOffset(1, 0);
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("cryotank_warning", UI.USERMENUACTIONS.READLORE.SEARCH_CRYO_TANK));
		gameObject.AddOrGet<Demolishable>().allowDemolition = false;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x000746EF File Offset: 0x000728EF
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x000746F1 File Offset: 0x000728F1
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000C46 RID: 3142
	public const string ID = "CryoTank";
}
