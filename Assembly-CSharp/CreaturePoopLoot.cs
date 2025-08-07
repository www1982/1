using System;
using UnityEngine;

// Token: 0x02000593 RID: 1427
public class CreaturePoopLoot : GameStateMachine<CreaturePoopLoot, CreaturePoopLoot.Instance, IStateMachineTarget, CreaturePoopLoot.Def>
{
	// Token: 0x0600209C RID: 8348 RVA: 0x000BC490 File Offset: 0x000BA690
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.EventTransition(GameHashes.Poop, this.roll, null);
		this.roll.Enter(new StateMachine<CreaturePoopLoot, CreaturePoopLoot.Instance, IStateMachineTarget, CreaturePoopLoot.Def>.State.Callback(CreaturePoopLoot.RollForLoot)).GoTo(this.idle);
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x000BC4E8 File Offset: 0x000BA6E8
	public static void RollForLoot(CreaturePoopLoot.Instance smi)
	{
		for (int i = 0; i < smi.def.Loot.Length; i++)
		{
			float value = global::UnityEngine.Random.value;
			CreaturePoopLoot.LootData lootData = smi.def.Loot[i];
			if (lootData.probability > 0f && value <= lootData.probability)
			{
				Tag tag = lootData.tag;
				Vector3 position = smi.transform.position;
				position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
				Util.KInstantiate(Assets.GetPrefab(tag), position).SetActive(true);
			}
		}
	}

	// Token: 0x040012F9 RID: 4857
	public GameStateMachine<CreaturePoopLoot, CreaturePoopLoot.Instance, IStateMachineTarget, CreaturePoopLoot.Def>.State idle;

	// Token: 0x040012FA RID: 4858
	public GameStateMachine<CreaturePoopLoot, CreaturePoopLoot.Instance, IStateMachineTarget, CreaturePoopLoot.Def>.State roll;

	// Token: 0x020013F9 RID: 5113
	public struct LootData
	{
		// Token: 0x04006B4C RID: 27468
		public Tag tag;

		// Token: 0x04006B4D RID: 27469
		public float probability;
	}

	// Token: 0x020013FA RID: 5114
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006B4E RID: 27470
		public CreaturePoopLoot.LootData[] Loot;
	}

	// Token: 0x020013FB RID: 5115
	public new class Instance : GameStateMachine<CreaturePoopLoot, CreaturePoopLoot.Instance, IStateMachineTarget, CreaturePoopLoot.Def>.GameInstance
	{
		// Token: 0x06008C23 RID: 35875 RVA: 0x00355265 File Offset: 0x00353465
		public Instance(IStateMachineTarget master, CreaturePoopLoot.Def def)
			: base(master, def)
		{
		}
	}
}
