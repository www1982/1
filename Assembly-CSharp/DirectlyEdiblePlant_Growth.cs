using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200081B RID: 2075
public class DirectlyEdiblePlant_Growth : KMonoBehaviour, IPlantConsumptionInstructions
{
	// Token: 0x060038E4 RID: 14564 RVA: 0x0013BD04 File Offset: 0x00139F04
	public bool CanPlantBeEaten()
	{
		float num = 0.25f;
		float num2 = 0f;
		AmountInstance amountInstance = Db.Get().Amounts.Maturity.Lookup(base.gameObject);
		if (amountInstance != null)
		{
			num2 = amountInstance.value / amountInstance.GetMax();
		}
		return num2 >= num;
	}

	// Token: 0x060038E5 RID: 14565 RVA: 0x0013BD50 File Offset: 0x00139F50
	public float ConsumePlant(float desiredUnitsToConsume)
	{
		AmountInstance amountInstance = Db.Get().Amounts.Maturity.Lookup(this.growing.gameObject);
		float growthUnitToMaturityRatio = this.GetGrowthUnitToMaturityRatio(amountInstance.GetMax(), base.GetComponent<KPrefabID>());
		float num = amountInstance.value * growthUnitToMaturityRatio;
		float num2 = Mathf.Min(desiredUnitsToConsume, num);
		this.growing.ConsumeGrowthUnits(num2, growthUnitToMaturityRatio);
		return num2;
	}

	// Token: 0x060038E6 RID: 14566 RVA: 0x0013BDB8 File Offset: 0x00139FB8
	public float PlantProductGrowthPerCycle()
	{
		Crop crop = base.GetComponent<Crop>();
		float num = CROPS.CROP_TYPES.Find((Crop.CropVal m) => m.cropId == crop.cropId).cropDuration / 600f;
		return 1f / num;
	}

	// Token: 0x060038E7 RID: 14567 RVA: 0x0013BE00 File Offset: 0x0013A000
	private float GetGrowthUnitToMaturityRatio(float maturityMax, KPrefabID prefab_id)
	{
		ResourceSet<Trait> traits = Db.Get().traits;
		Tag prefabTag = prefab_id.PrefabTag;
		Trait trait = traits.Get(prefabTag.ToString() + "Original");
		if (trait != null)
		{
			AttributeModifier attributeModifier = trait.SelfModifiers.Find((AttributeModifier match) => match.AttributeId == "MaturityMax");
			if (attributeModifier != null)
			{
				return attributeModifier.Value / maturityMax;
			}
		}
		return 1f;
	}

	// Token: 0x060038E8 RID: 14568 RVA: 0x0013BE7C File Offset: 0x0013A07C
	public string GetFormattedConsumptionPerCycle(float consumer_KGWorthOfCaloriesLostPerSecond)
	{
		float num = this.PlantProductGrowthPerCycle();
		return GameUtil.GetFormattedPlantGrowth(consumer_KGWorthOfCaloriesLostPerSecond * num * 100f, GameUtil.TimeSlice.PerCycle);
	}

	// Token: 0x060038E9 RID: 14569 RVA: 0x0013BE9F File Offset: 0x0013A09F
	public CellOffset[] GetAllowedOffsets()
	{
		return null;
	}

	// Token: 0x060038EA RID: 14570 RVA: 0x0013BEA2 File Offset: 0x0013A0A2
	public Diet.Info.FoodType GetDietFoodType()
	{
		return Diet.Info.FoodType.EatPlantDirectly;
	}

	// Token: 0x0400225A RID: 8794
	[MyCmpGet]
	private Growing growing;
}
