using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001EB RID: 491
public class PrickleFruitConfig : IEntityConfig
{
	// Token: 0x060009D1 RID: 2513 RVA: 0x0003DF24 File Offset: 0x0003C124
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity(PrickleFruitConfig.ID, global::STRINGS.ITEMS.FOOD.PRICKLEFRUIT.NAME, global::STRINGS.ITEMS.FOOD.PRICKLEFRUIT.DESC, 1f, false, Assets.GetAnim("bristleberry_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.PRICKLEFRUIT);
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0003DF88 File Offset: 0x0003C188
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0003DF8A File Offset: 0x0003C18A
	public void OnSpawn(GameObject inst)
	{
		inst.Subscribe(-10536414, PrickleFruitConfig.OnEatCompleteDelegate);
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0003DFA0 File Offset: 0x0003C1A0
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
				if (global::UnityEngine.Random.value < PrickleFruitConfig.SEEDS_PER_FRUIT_CHANCE)
				{
					num++;
				}
			}
			if (num > 0)
			{
				Vector3 vector = edible.transform.GetPosition() + new Vector3(0f, 0.05f, 0f);
				vector = Grid.CellToPosCCC(Grid.PosToCell(vector), Grid.SceneLayer.Ore);
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(new Tag("PrickleFlowerSeed")), vector, Grid.SceneLayer.Ore, null, 0);
				PrimaryElement component = edible.GetComponent<PrimaryElement>();
				PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
				component2.Temperature = component.Temperature;
				component2.Units = (float)num;
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x040006ED RID: 1773
	public static float SEEDS_PER_FRUIT_CHANCE = 0.05f;

	// Token: 0x040006EE RID: 1774
	public static string ID = "PrickleFruit";

	// Token: 0x040006EF RID: 1775
	private static readonly EventSystem.IntraObjectHandler<Edible> OnEatCompleteDelegate = new EventSystem.IntraObjectHandler<Edible>(delegate(Edible component, object data)
	{
		PrickleFruitConfig.OnEatComplete(component);
	});
}
