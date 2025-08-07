using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E3 RID: 483
public class MushroomConfig : IEntityConfig
{
	// Token: 0x060009A8 RID: 2472 RVA: 0x0003DA38 File Offset: 0x0003BC38
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity(MushroomConfig.ID, global::STRINGS.ITEMS.FOOD.MUSHROOM.NAME, global::STRINGS.ITEMS.FOOD.MUSHROOM.DESC, 1f, false, Assets.GetAnim("funguscap_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.77f, 0.48f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.MUSHROOM);
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0003DA9C File Offset: 0x0003BC9C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0003DA9E File Offset: 0x0003BC9E
	public void OnSpawn(GameObject inst)
	{
		inst.Subscribe(-10536414, MushroomConfig.OnEatCompleteDelegate);
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0003DAB4 File Offset: 0x0003BCB4
	private static void OnEatComplete(Edible edible)
	{
		if (edible != null)
		{
			int num = 0;
			float unitsConsumed = edible.unitsConsumed;
			int num2 = Mathf.FloorToInt(unitsConsumed);
			float num3 = unitsConsumed % 1f;
			if (global::UnityEngine.Random.value < num3)
			{
				num2++;
			}
			for (int i = 0; i < num2; i++)
			{
				if (global::UnityEngine.Random.value < MushroomConfig.SEEDS_PER_FRUIT_CHANCE)
				{
					num++;
				}
			}
			if (num > 0)
			{
				Vector3 vector = edible.transform.GetPosition() + new Vector3(0f, 0.05f, 0f);
				vector = Grid.CellToPosCCC(Grid.PosToCell(vector), Grid.SceneLayer.Ore);
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(new Tag("MushroomSeed")), vector, Grid.SceneLayer.Ore, null, 0);
				PrimaryElement component = edible.GetComponent<PrimaryElement>();
				PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
				component2.Temperature = component.Temperature;
				component2.Units = (float)num;
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x040006DB RID: 1755
	public static float SEEDS_PER_FRUIT_CHANCE = 0.05f;

	// Token: 0x040006DC RID: 1756
	public static string ID = "Mushroom";

	// Token: 0x040006DD RID: 1757
	private static readonly EventSystem.IntraObjectHandler<Edible> OnEatCompleteDelegate = new EventSystem.IntraObjectHandler<Edible>(delegate(Edible component, object data)
	{
		MushroomConfig.OnEatComplete(component);
	});
}
