using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200013C RID: 316
public class MorbRoverConfig : IEntityConfig
{
	// Token: 0x060005E5 RID: 1509 RVA: 0x0002C6FC File Offset: 0x0002A8FC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = BaseRoverConfig.BaseRover("MorbRover", global::STRINGS.ROBOTS.MODELS.MORB.NAME, GameTags.Robots.Models.MorbRover, global::STRINGS.ROBOTS.MODELS.MORB.DESC, "morbRover_kanim", 300f, 1f, 2f, global::TUNING.ROBOTS.MORBBOT.CARRY_CAPACITY, 1f, 1f, 3f, global::TUNING.ROBOTS.MORBBOT.HIT_POINTS, 180000f, 30f, Db.Get().Amounts.InternalBioBattery, false);
		gameObject.GetComponent<PrimaryElement>().SetElement(SimHashes.Steel, false);
		gameObject.GetComponent<Deconstructable>().customWorkTime = 10f;
		return gameObject;
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0002C794 File Offset: 0x0002A994
	public void OnPrefabInit(GameObject inst)
	{
		BaseRoverConfig.OnPrefabInit(inst, Db.Get().Amounts.InternalBioBattery);
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0002C7AB File Offset: 0x0002A9AB
	public void OnSpawn(GameObject inst)
	{
		BaseRoverConfig.OnSpawn(inst);
		inst.Subscribe(1623392196, new Action<object>(this.TriggerDeconstructChoreOnDeath));
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0002C7CC File Offset: 0x0002A9CC
	public void TriggerDeconstructChoreOnDeath(object obj)
	{
		if (obj != null)
		{
			Deconstructable component = ((GameObject)obj).GetComponent<Deconstructable>();
			if (!component.IsMarkedForDeconstruction())
			{
				component.QueueDeconstruction(false);
			}
		}
	}

	// Token: 0x04000465 RID: 1125
	public const string ID = "MorbRover";

	// Token: 0x04000466 RID: 1126
	public const SimHashes MATERIAL = SimHashes.Steel;

	// Token: 0x04000467 RID: 1127
	public const float MASS = 300f;

	// Token: 0x04000468 RID: 1128
	private const float WIDTH = 1f;

	// Token: 0x04000469 RID: 1129
	private const float HEIGHT = 2f;
}
