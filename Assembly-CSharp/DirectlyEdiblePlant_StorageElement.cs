using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200081C RID: 2076
public class DirectlyEdiblePlant_StorageElement : KMonoBehaviour, IPlantConsumptionInstructions
{
	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x060038EC RID: 14572 RVA: 0x0013BEAD File Offset: 0x0013A0AD
	public float MassGeneratedPerCycle
	{
		get
		{
			return this.rateProducedPerCycle * this.storageCapacity;
		}
	}

	// Token: 0x060038ED RID: 14573 RVA: 0x0013BEBC File Offset: 0x0013A0BC
	protected override void OnPrefabInit()
	{
		this.storageCapacity = this.storage.capacityKg;
		base.OnPrefabInit();
	}

	// Token: 0x060038EE RID: 14574 RVA: 0x0013BED8 File Offset: 0x0013A0D8
	public bool CanPlantBeEaten()
	{
		Tag tag = this.GetTagToConsume();
		return this.storage.GetMassAvailable(tag) / this.storage.capacityKg >= this.minimum_mass_percentageRequiredToEat;
	}

	// Token: 0x060038EF RID: 14575 RVA: 0x0013BF10 File Offset: 0x0013A110
	public float ConsumePlant(float desiredUnitsToConsume)
	{
		if (this.storage.MassStored() <= 0f)
		{
			return 0f;
		}
		Tag tag = this.GetTagToConsume();
		float massAvailable = this.storage.GetMassAvailable(tag);
		float num = Mathf.Min(desiredUnitsToConsume, massAvailable);
		this.storage.ConsumeIgnoringDisease(tag, num);
		return num;
	}

	// Token: 0x060038F0 RID: 14576 RVA: 0x0013BF5F File Offset: 0x0013A15F
	public float PlantProductGrowthPerCycle()
	{
		return this.MassGeneratedPerCycle;
	}

	// Token: 0x060038F1 RID: 14577 RVA: 0x0013BF67 File Offset: 0x0013A167
	private Tag GetTagToConsume()
	{
		if (!(this.tagToConsume != Tag.Invalid))
		{
			return this.storage.items[0].GetComponent<KPrefabID>().PrefabTag;
		}
		return this.tagToConsume;
	}

	// Token: 0x060038F2 RID: 14578 RVA: 0x0013BF9D File Offset: 0x0013A19D
	public string GetFormattedConsumptionPerCycle(float consumer_KGWorthOfCaloriesLostPerSecond)
	{
		return string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.EDIBLE_PLANT_INTERNAL_STORAGE, GameUtil.GetFormattedMass(consumer_KGWorthOfCaloriesLostPerSecond, GameUtil.TimeSlice.PerCycle, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), this.tagToConsume.ProperName());
	}

	// Token: 0x060038F3 RID: 14579 RVA: 0x0013BFC7 File Offset: 0x0013A1C7
	public CellOffset[] GetAllowedOffsets()
	{
		return this.edibleCellOffsets;
	}

	// Token: 0x060038F4 RID: 14580 RVA: 0x0013BFCF File Offset: 0x0013A1CF
	public Diet.Info.FoodType GetDietFoodType()
	{
		return Diet.Info.FoodType.EatPlantStorage;
	}

	// Token: 0x0400225B RID: 8795
	public CellOffset[] edibleCellOffsets;

	// Token: 0x0400225C RID: 8796
	public Tag tagToConsume = Tag.Invalid;

	// Token: 0x0400225D RID: 8797
	public float rateProducedPerCycle;

	// Token: 0x0400225E RID: 8798
	public float storageCapacity;

	// Token: 0x0400225F RID: 8799
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04002260 RID: 8800
	[MyCmpGet]
	private KPrefabID prefabID;

	// Token: 0x04002261 RID: 8801
	public float minimum_mass_percentageRequiredToEat = 0.25f;
}
