using System;
using System.Collections.Generic;

// Token: 0x020008E8 RID: 2280
public class EnergySim
{
	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x06003FA6 RID: 16294 RVA: 0x00165AC9 File Offset: 0x00163CC9
	public HashSet<Generator> Generators
	{
		get
		{
			return this.generators;
		}
	}

	// Token: 0x06003FA7 RID: 16295 RVA: 0x00165AD1 File Offset: 0x00163CD1
	public void AddGenerator(Generator generator)
	{
		this.generators.Add(generator);
	}

	// Token: 0x06003FA8 RID: 16296 RVA: 0x00165AE0 File Offset: 0x00163CE0
	public void RemoveGenerator(Generator generator)
	{
		this.generators.Remove(generator);
	}

	// Token: 0x06003FA9 RID: 16297 RVA: 0x00165AEF File Offset: 0x00163CEF
	public void AddManualGenerator(ManualGenerator manual_generator)
	{
		this.manualGenerators.Add(manual_generator);
	}

	// Token: 0x06003FAA RID: 16298 RVA: 0x00165AFE File Offset: 0x00163CFE
	public void RemoveManualGenerator(ManualGenerator manual_generator)
	{
		this.manualGenerators.Remove(manual_generator);
	}

	// Token: 0x06003FAB RID: 16299 RVA: 0x00165B0D File Offset: 0x00163D0D
	public void AddBattery(Battery battery)
	{
		this.batteries.Add(battery);
	}

	// Token: 0x06003FAC RID: 16300 RVA: 0x00165B1C File Offset: 0x00163D1C
	public void RemoveBattery(Battery battery)
	{
		this.batteries.Remove(battery);
	}

	// Token: 0x06003FAD RID: 16301 RVA: 0x00165B2B File Offset: 0x00163D2B
	public void AddEnergyConsumer(EnergyConsumer energy_consumer)
	{
		this.energyConsumers.Add(energy_consumer);
	}

	// Token: 0x06003FAE RID: 16302 RVA: 0x00165B3A File Offset: 0x00163D3A
	public void RemoveEnergyConsumer(EnergyConsumer energy_consumer)
	{
		this.energyConsumers.Remove(energy_consumer);
	}

	// Token: 0x06003FAF RID: 16303 RVA: 0x00165B4C File Offset: 0x00163D4C
	public void EnergySim200ms(float dt)
	{
		foreach (Generator generator in this.generators)
		{
			generator.EnergySim200ms(dt);
		}
		foreach (ManualGenerator manualGenerator in this.manualGenerators)
		{
			manualGenerator.EnergySim200ms(dt);
		}
		foreach (Battery battery in this.batteries)
		{
			battery.EnergySim200ms(dt);
		}
		foreach (EnergyConsumer energyConsumer in this.energyConsumers)
		{
			energyConsumer.EnergySim200ms(dt);
		}
	}

	// Token: 0x04002783 RID: 10115
	private HashSet<Generator> generators = new HashSet<Generator>();

	// Token: 0x04002784 RID: 10116
	private HashSet<ManualGenerator> manualGenerators = new HashSet<ManualGenerator>();

	// Token: 0x04002785 RID: 10117
	private HashSet<Battery> batteries = new HashSet<Battery>();

	// Token: 0x04002786 RID: 10118
	private HashSet<EnergyConsumer> energyConsumers = new HashSet<EnergyConsumer>();
}
