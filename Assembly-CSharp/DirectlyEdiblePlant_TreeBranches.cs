using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x0200081D RID: 2077
public class DirectlyEdiblePlant_TreeBranches : KMonoBehaviour, IPlantConsumptionInstructions
{
	// Token: 0x060038F6 RID: 14582 RVA: 0x0013BFF0 File Offset: 0x0013A1F0
	protected override void OnSpawn()
	{
		this.trunk = base.gameObject.GetSMI<PlantBranchGrower.Instance>();
		base.OnSpawn();
	}

	// Token: 0x060038F7 RID: 14583 RVA: 0x0013C009 File Offset: 0x0013A209
	public bool CanPlantBeEaten()
	{
		return this.GetMaxBranchMaturity() >= this.MinimumEdibleMaturity;
	}

	// Token: 0x060038F8 RID: 14584 RVA: 0x0013C01C File Offset: 0x0013A21C
	public float ConsumePlant(float desiredUnitsToConsume)
	{
		float maxBranchMaturity = this.GetMaxBranchMaturity();
		float num = Mathf.Min(desiredUnitsToConsume, maxBranchMaturity);
		GameObject mostMatureBranch = this.GetMostMatureBranch();
		if (!mostMatureBranch)
		{
			return 0f;
		}
		Growing component = mostMatureBranch.GetComponent<Growing>();
		if (component)
		{
			Harvestable component2 = mostMatureBranch.GetComponent<Harvestable>();
			if (component2 != null)
			{
				component2.Trigger(2127324410, true);
			}
			component.ConsumeMass(num);
			return num;
		}
		mostMatureBranch.GetAmounts().Get(Db.Get().Amounts.Maturity.Id).ApplyDelta(-desiredUnitsToConsume);
		base.gameObject.Trigger(-1793167409, null);
		mostMatureBranch.Trigger(-1793167409, null);
		return desiredUnitsToConsume;
	}

	// Token: 0x060038F9 RID: 14585 RVA: 0x0013C0D4 File Offset: 0x0013A2D4
	public float PlantProductGrowthPerCycle()
	{
		Crop component = base.GetComponent<Crop>();
		string cropID = component.cropId;
		if (this.overrideCropID != null)
		{
			cropID = this.overrideCropID;
		}
		float num = CROPS.CROP_TYPES.Find((Crop.CropVal m) => m.cropId == cropID).cropDuration / 600f;
		return 1f / num;
	}

	// Token: 0x060038FA RID: 14586 RVA: 0x0013C138 File Offset: 0x0013A338
	public float GetMaxBranchMaturity()
	{
		float max_maturity = 0f;
		GameObject max_branch = null;
		this.trunk.ActionPerBranch(delegate(GameObject branch)
		{
			if (branch != null)
			{
				AmountInstance amountInstance = Db.Get().Amounts.Maturity.Lookup(branch);
				if (amountInstance != null)
				{
					float num = amountInstance.value / amountInstance.GetMax();
					if (num > max_maturity)
					{
						max_maturity = num;
						max_branch = branch;
					}
				}
			}
		});
		return max_maturity;
	}

	// Token: 0x060038FB RID: 14587 RVA: 0x0013C17C File Offset: 0x0013A37C
	private GameObject GetMostMatureBranch()
	{
		float max_maturity = 0f;
		GameObject max_branch = null;
		this.trunk.ActionPerBranch(delegate(GameObject branch)
		{
			if (branch != null)
			{
				AmountInstance amountInstance = Db.Get().Amounts.Maturity.Lookup(branch);
				if (amountInstance != null)
				{
					float num = amountInstance.value / amountInstance.GetMax();
					if (num > max_maturity)
					{
						max_maturity = num;
						max_branch = branch;
					}
				}
			}
		});
		return max_branch;
	}

	// Token: 0x060038FC RID: 14588 RVA: 0x0013C1C0 File Offset: 0x0013A3C0
	public string GetFormattedConsumptionPerCycle(float consumer_KGWorthOfCaloriesLostPerSecond)
	{
		float num = this.PlantProductGrowthPerCycle();
		return GameUtil.GetFormattedPlantGrowth(consumer_KGWorthOfCaloriesLostPerSecond * num * 100f, GameUtil.TimeSlice.PerCycle);
	}

	// Token: 0x060038FD RID: 14589 RVA: 0x0013C1E3 File Offset: 0x0013A3E3
	public CellOffset[] GetAllowedOffsets()
	{
		return null;
	}

	// Token: 0x060038FE RID: 14590 RVA: 0x0013C1E6 File Offset: 0x0013A3E6
	public Diet.Info.FoodType GetDietFoodType()
	{
		return Diet.Info.FoodType.EatPlantDirectly;
	}

	// Token: 0x04002262 RID: 8802
	private PlantBranchGrower.Instance trunk;

	// Token: 0x04002263 RID: 8803
	public float MinimumEdibleMaturity = 0.25f;

	// Token: 0x04002264 RID: 8804
	public string overrideCropID;
}
