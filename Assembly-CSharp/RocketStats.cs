using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000B3B RID: 2875
public class RocketStats
{
	// Token: 0x06005549 RID: 21833 RVA: 0x001EF69E File Offset: 0x001ED89E
	public RocketStats(CommandModule commandModule)
	{
		this.commandModule = commandModule;
	}

	// Token: 0x0600554A RID: 21834 RVA: 0x001EF6B0 File Offset: 0x001ED8B0
	public float GetRocketMaxDistance()
	{
		float totalMass = this.GetTotalMass();
		float totalThrust = this.GetTotalThrust();
		float num = ROCKETRY.CalculateMassWithPenalty(totalMass);
		float num2 = Mathf.Max(0f, totalThrust - num);
		RoboPilotModule component = this.commandModule.GetComponent<RoboPilotModule>();
		if (component != null)
		{
			num2 = Mathf.Min(num2, component.GetDataBankRange());
		}
		return num2;
	}

	// Token: 0x0600554B RID: 21835 RVA: 0x001EF701 File Offset: 0x001ED901
	public float GetTotalMass()
	{
		return this.GetDryMass() + this.GetWetMass();
	}

	// Token: 0x0600554C RID: 21836 RVA: 0x001EF710 File Offset: 0x001ED910
	public float GetDryMass()
	{
		float num = 0f;
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			RocketModule component = gameObject.GetComponent<RocketModule>();
			if (component != null)
			{
				num += component.GetComponent<PrimaryElement>().Mass;
			}
		}
		return num;
	}

	// Token: 0x0600554D RID: 21837 RVA: 0x001EF78C File Offset: 0x001ED98C
	public float GetWetMass()
	{
		float num = 0f;
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			RocketModule component = gameObject.GetComponent<RocketModule>();
			if (component != null)
			{
				FuelTank component2 = component.GetComponent<FuelTank>();
				OxidizerTank component3 = component.GetComponent<OxidizerTank>();
				SolidBooster component4 = component.GetComponent<SolidBooster>();
				if (component2 != null)
				{
					num += component2.storage.MassStored();
				}
				if (component3 != null)
				{
					num += component3.storage.MassStored();
				}
				if (component4 != null)
				{
					num += component4.fuelStorage.MassStored();
				}
			}
		}
		return num;
	}

	// Token: 0x0600554E RID: 21838 RVA: 0x001EF858 File Offset: 0x001EDA58
	public Tag GetEngineFuelTag()
	{
		RocketEngine mainEngine = this.GetMainEngine();
		if (mainEngine != null)
		{
			return mainEngine.fuelTag;
		}
		return null;
	}

	// Token: 0x0600554F RID: 21839 RVA: 0x001EF884 File Offset: 0x001EDA84
	public float GetTotalFuel(bool includeBoosters = false)
	{
		float num = 0f;
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			FuelTank component = gameObject.GetComponent<FuelTank>();
			Tag engineFuelTag = this.GetEngineFuelTag();
			if (component != null)
			{
				num += component.storage.GetAmountAvailable(engineFuelTag);
			}
			if (includeBoosters)
			{
				SolidBooster component2 = gameObject.GetComponent<SolidBooster>();
				if (component2 != null)
				{
					num += component2.fuelStorage.GetAmountAvailable(component2.fuelTag);
				}
			}
		}
		return num;
	}

	// Token: 0x06005550 RID: 21840 RVA: 0x001EF934 File Offset: 0x001EDB34
	public float GetTotalOxidizer(bool includeBoosters = false)
	{
		float num = 0f;
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			OxidizerTank component = gameObject.GetComponent<OxidizerTank>();
			if (component != null)
			{
				num += component.GetTotalOxidizerAvailable();
			}
			if (includeBoosters)
			{
				SolidBooster component2 = gameObject.GetComponent<SolidBooster>();
				if (component2 != null)
				{
					num += component2.fuelStorage.GetAmountAvailable(GameTags.OxyRock);
				}
			}
		}
		return num;
	}

	// Token: 0x06005551 RID: 21841 RVA: 0x001EF9D4 File Offset: 0x001EDBD4
	public float GetAverageOxidizerEfficiency()
	{
		Dictionary<Tag, float> dictionary = new Dictionary<Tag, float>();
		dictionary[SimHashes.LiquidOxygen.CreateTag()] = 0f;
		dictionary[SimHashes.OxyRock.CreateTag()] = 0f;
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			OxidizerTank component = gameObject.GetComponent<OxidizerTank>();
			if (component != null)
			{
				foreach (KeyValuePair<Tag, float> keyValuePair in component.GetOxidizersAvailable())
				{
					if (dictionary.ContainsKey(keyValuePair.Key))
					{
						Dictionary<Tag, float> dictionary2 = dictionary;
						Tag key = keyValuePair.Key;
						dictionary2[key] += keyValuePair.Value;
					}
				}
			}
		}
		float num = 0f;
		float num2 = 0f;
		foreach (KeyValuePair<Tag, float> keyValuePair2 in dictionary)
		{
			num += keyValuePair2.Value * RocketStats.oxidizerEfficiencies[keyValuePair2.Key];
			num2 += keyValuePair2.Value;
		}
		if (num2 == 0f)
		{
			return 0f;
		}
		return num / num2 * 100f;
	}

	// Token: 0x06005552 RID: 21842 RVA: 0x001EFB64 File Offset: 0x001EDD64
	public float GetTotalThrust()
	{
		float totalFuel = this.GetTotalFuel(false);
		float totalOxidizer = this.GetTotalOxidizer(false);
		float averageOxidizerEfficiency = this.GetAverageOxidizerEfficiency();
		RocketEngine mainEngine = this.GetMainEngine();
		if (mainEngine == null)
		{
			return 0f;
		}
		return (mainEngine.requireOxidizer ? (Mathf.Min(totalFuel, totalOxidizer) * (mainEngine.efficiency * (averageOxidizerEfficiency / 100f))) : (totalFuel * mainEngine.efficiency)) + this.GetBoosterThrust();
	}

	// Token: 0x06005553 RID: 21843 RVA: 0x001EFBD0 File Offset: 0x001EDDD0
	public float GetBoosterThrust()
	{
		float num = 0f;
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			SolidBooster component = gameObject.GetComponent<SolidBooster>();
			if (component != null)
			{
				float amountAvailable = component.fuelStorage.GetAmountAvailable(ElementLoader.FindElementByHash(SimHashes.OxyRock).tag);
				float amountAvailable2 = component.fuelStorage.GetAmountAvailable(ElementLoader.FindElementByHash(SimHashes.Iron).tag);
				num += component.efficiency * Mathf.Min(amountAvailable, amountAvailable2);
			}
		}
		return num;
	}

	// Token: 0x06005554 RID: 21844 RVA: 0x001EFC84 File Offset: 0x001EDE84
	public float GetEngineEfficiency()
	{
		RocketEngine mainEngine = this.GetMainEngine();
		if (mainEngine != null)
		{
			return mainEngine.efficiency;
		}
		return 0f;
	}

	// Token: 0x06005555 RID: 21845 RVA: 0x001EFCB0 File Offset: 0x001EDEB0
	public RocketEngine GetMainEngine()
	{
		RocketEngine rocketEngine = null;
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			rocketEngine = gameObject.GetComponent<RocketEngine>();
			if (rocketEngine != null && rocketEngine.mainEngine)
			{
				break;
			}
		}
		return rocketEngine;
	}

	// Token: 0x06005556 RID: 21846 RVA: 0x001EFD24 File Offset: 0x001EDF24
	public float GetTotalOxidizableFuel()
	{
		float totalFuel = this.GetTotalFuel(false);
		float totalOxidizer = this.GetTotalOxidizer(false);
		return Mathf.Min(totalFuel, totalOxidizer);
	}

	// Token: 0x04003920 RID: 14624
	private CommandModule commandModule;

	// Token: 0x04003921 RID: 14625
	public static Dictionary<Tag, float> oxidizerEfficiencies = new Dictionary<Tag, float>
	{
		{
			SimHashes.OxyRock.CreateTag(),
			ROCKETRY.OXIDIZER_EFFICIENCY.LOW
		},
		{
			SimHashes.LiquidOxygen.CreateTag(),
			ROCKETRY.OXIDIZER_EFFICIENCY.HIGH
		}
	};
}
