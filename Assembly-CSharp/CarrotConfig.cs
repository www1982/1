using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class CarrotConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000927 RID: 2343 RVA: 0x0003CD04 File Offset: 0x0003AF04
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0003CD0B File Offset: 0x0003AF0B
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0003CD10 File Offset: 0x0003AF10
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity(CarrotConfig.ID, global::STRINGS.ITEMS.FOOD.CARROT.NAME, global::STRINGS.ITEMS.FOOD.CARROT.DESC, 1f, false, Assets.GetAnim("purplerootVegetable_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.CARROT);
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0003CD74 File Offset: 0x0003AF74
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0003CD76 File Offset: 0x0003AF76
	public void OnSpawn(GameObject inst)
	{
		inst.Subscribe(-10536414, CarrotConfig.OnEatCompleteDelegate);
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0003CD8C File Offset: 0x0003AF8C
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
				if (global::UnityEngine.Random.value < CarrotConfig.SEEDS_PER_FRUIT_CHANCE)
				{
					num++;
				}
			}
			if (num > 0)
			{
				Vector3 vector = edible.transform.GetPosition() + new Vector3(0f, 0.05f, 0f);
				vector = Grid.CellToPosCCC(Grid.PosToCell(vector), Grid.SceneLayer.Ore);
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(new Tag("CarrotPlantSeed")), vector, Grid.SceneLayer.Ore, null, 0);
				PrimaryElement component = edible.GetComponent<PrimaryElement>();
				PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
				component2.Temperature = component.Temperature;
				component2.Units = (float)num;
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x040006A9 RID: 1705
	public static float SEEDS_PER_FRUIT_CHANCE = 0.05f;

	// Token: 0x040006AA RID: 1706
	public static string ID = "Carrot";

	// Token: 0x040006AB RID: 1707
	private static readonly EventSystem.IntraObjectHandler<Edible> OnEatCompleteDelegate = new EventSystem.IntraObjectHandler<Edible>(delegate(Edible component, object data)
	{
		CarrotConfig.OnEatComplete(component);
	});
}
