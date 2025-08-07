using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000953 RID: 2387
public class HighEnergyParticleConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06004482 RID: 17538 RVA: 0x00189FE2 File Offset: 0x001881E2
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06004483 RID: 17539 RVA: 0x00189FE9 File Offset: 0x001881E9
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06004484 RID: 17540 RVA: 0x00189FEC File Offset: 0x001881EC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity("HighEnergyParticle", ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME, ITEMS.RADIATION.HIGHENERGYPARITCLE.DESC, 1f, false, Assets.GetAnim("spark_radial_high_energy_particles_kanim"), "travel_pre", Grid.SceneLayer.FXFront2, SimHashes.Creature, null, 293f);
		EntityTemplates.AddCollision(gameObject, EntityTemplates.CollisionShape.CIRCLE, 0.2f, 0.2f);
		gameObject.AddOrGet<LoopingSounds>();
		RadiationEmitter radiationEmitter = gameObject.AddOrGet<RadiationEmitter>();
		radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
		radiationEmitter.radiusProportionalToRads = false;
		radiationEmitter.emitRadiusX = 3;
		radiationEmitter.emitRadiusY = 3;
		radiationEmitter.emitRads = 0.4f * ((float)radiationEmitter.emitRadiusX / 6f);
		gameObject.AddComponent<HighEnergyParticle>().speed = 8f;
		return gameObject;
	}

	// Token: 0x06004485 RID: 17541 RVA: 0x0018A0A3 File Offset: 0x001882A3
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06004486 RID: 17542 RVA: 0x0018A0A5 File Offset: 0x001882A5
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04002DD4 RID: 11732
	public const int PARTICLE_SPEED = 8;

	// Token: 0x04002DD5 RID: 11733
	public const float PARTICLE_COLLISION_SIZE = 0.2f;

	// Token: 0x04002DD6 RID: 11734
	public const float PER_CELL_FALLOFF = 0.1f;

	// Token: 0x04002DD7 RID: 11735
	public const float FALLOUT_RATIO = 0.5f;

	// Token: 0x04002DD8 RID: 11736
	public const int MAX_PAYLOAD = 500;

	// Token: 0x04002DD9 RID: 11737
	public const int EXPLOSION_FALLOUT_TEMPERATURE = 5000;

	// Token: 0x04002DDA RID: 11738
	public const float EXPLOSION_FALLOUT_MASS_PER_PARTICLE = 0.001f;

	// Token: 0x04002DDB RID: 11739
	public const float EXPLOSION_EMIT_DURRATION = 1f;

	// Token: 0x04002DDC RID: 11740
	public const short EXPLOSION_EMIT_RADIUS = 6;

	// Token: 0x04002DDD RID: 11741
	public const string ID = "HighEnergyParticle";
}
