using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000A0 RID: 160
public static class CrabTuning
{
	// Token: 0x06000305 RID: 773 RVA: 0x00016D81 File Offset: 0x00014F81
	public static bool IsReadyToMolt(MoltDropperMonitor.Instance smi)
	{
		return CrabTuning.IsValidTimeToDrop(smi) && CrabTuning.IsValidDropCell(smi) && !smi.prefabID.HasTag(GameTags.Creatures.Hungry) && smi.prefabID.HasTag(GameTags.Creatures.Happy);
	}

	// Token: 0x06000306 RID: 774 RVA: 0x00016DB7 File Offset: 0x00014FB7
	public static bool IsValidTimeToDrop(MoltDropperMonitor.Instance smi)
	{
		return !smi.spawnedThisCycle && (smi.timeOfLastDrop <= 0f || GameClock.Instance.GetTime() - smi.timeOfLastDrop > 600f);
	}

	// Token: 0x06000307 RID: 775 RVA: 0x00016DEC File Offset: 0x00014FEC
	public static bool IsValidDropCell(MoltDropperMonitor.Instance smi)
	{
		int num = Grid.PosToCell(smi.transform.GetPosition());
		return Grid.IsValidCell(num) && Grid.Element[num].id != SimHashes.Ethanol;
	}

	// Token: 0x040001EA RID: 490
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabEgg".ToTag(),
			weight = 0.97f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabWoodEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabFreshWaterEgg".ToTag(),
			weight = 0.01f
		}
	};

	// Token: 0x040001EB RID: 491
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_WOOD = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabWoodEgg".ToTag(),
			weight = 0.65f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabFreshWaterEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x040001EC RID: 492
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_FRESH = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabWoodEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "CrabFreshWaterEgg".ToTag(),
			weight = 0.65f
		}
	};

	// Token: 0x040001ED RID: 493
	public static float STANDARD_CALORIES_PER_CYCLE = 100000f;

	// Token: 0x040001EE RID: 494
	public static float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x040001EF RID: 495
	public static float STANDARD_STOMACH_SIZE = CrabTuning.STANDARD_CALORIES_PER_CYCLE * CrabTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001F0 RID: 496
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x040001F1 RID: 497
	public static float EGG_MASS = 2f;

	// Token: 0x040001F2 RID: 498
	public static CellOffset[] DEFEND_OFFSETS = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(1, 0),
		new CellOffset(-1, 0),
		new CellOffset(1, 1),
		new CellOffset(-1, 1)
	};
}
