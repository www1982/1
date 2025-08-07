using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000893 RID: 2195
public class DeathLoot : GameStateMachine<DeathLoot, DeathLoot.Instance, IStateMachineTarget, DeathLoot.Def>
{
	// Token: 0x06003CB2 RID: 15538 RVA: 0x0015107B File Offset: 0x0014F27B
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.root;
	}

	// Token: 0x0400252E RID: 9518
	private StateMachine<DeathLoot, DeathLoot.Instance, IStateMachineTarget, DeathLoot.Def>.BoolParameter WasLoopDropped;

	// Token: 0x0200185E RID: 6238
	public class Loot
	{
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06009C5D RID: 40029 RVA: 0x0039099A File Offset: 0x0038EB9A
		// (set) Token: 0x06009C5C RID: 40028 RVA: 0x00390991 File Offset: 0x0038EB91
		public Tag Id { get; private set; } = Tag.Invalid;

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06009C5F RID: 40031 RVA: 0x003909AB File Offset: 0x0038EBAB
		// (set) Token: 0x06009C5E RID: 40030 RVA: 0x003909A2 File Offset: 0x0038EBA2
		public bool IsElement { get; private set; }

		// Token: 0x06009C60 RID: 40032 RVA: 0x003909B3 File Offset: 0x0038EBB3
		public Loot(Tag tag)
		{
			this.Id = tag;
			this.IsElement = false;
			this.Quantity = 1f;
		}

		// Token: 0x06009C61 RID: 40033 RVA: 0x003909DF File Offset: 0x0038EBDF
		public Loot(SimHashes element, float quantity)
		{
			this.Id = element.CreateTag();
			this.IsElement = true;
			this.Quantity = quantity;
		}

		// Token: 0x040078C7 RID: 30919
		public float Quantity;
	}

	// Token: 0x0200185F RID: 6239
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040078C8 RID: 30920
		public DeathLoot.Loot[] loot;

		// Token: 0x040078C9 RID: 30921
		public CellOffset lootSpawnOffset;
	}

	// Token: 0x02001860 RID: 6240
	public new class Instance : GameStateMachine<DeathLoot, DeathLoot.Instance, IStateMachineTarget, DeathLoot.Def>.GameInstance
	{
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06009C63 RID: 40035 RVA: 0x00390A14 File Offset: 0x0038EC14
		public bool WasLoopDropped
		{
			get
			{
				return base.sm.WasLoopDropped.Get(base.smi);
			}
		}

		// Token: 0x06009C64 RID: 40036 RVA: 0x00390A2C File Offset: 0x0038EC2C
		public Instance(IStateMachineTarget master, DeathLoot.Def def)
			: base(master, def)
		{
			base.Subscribe(1623392196, new Action<object>(this.OnDeath));
		}

		// Token: 0x06009C65 RID: 40037 RVA: 0x00390A4D File Offset: 0x0038EC4D
		private void OnDeath(object obj)
		{
			if (!this.WasLoopDropped)
			{
				base.sm.WasLoopDropped.Set(true, this, false);
				this.CreateLoot();
			}
		}

		// Token: 0x06009C66 RID: 40038 RVA: 0x00390A74 File Offset: 0x0038EC74
		public GameObject[] CreateLoot()
		{
			if (base.def.loot == null)
			{
				return null;
			}
			GameObject[] array = new GameObject[base.def.loot.Length];
			for (int i = 0; i < base.def.loot.Length; i++)
			{
				DeathLoot.Loot loot = base.def.loot[i];
				if (!(loot.Id == Tag.Invalid))
				{
					GameObject gameObject = Scenario.SpawnPrefab(this.GetLootSpawnCell(), 0, 0, loot.Id.ToString(), Grid.SceneLayer.Ore);
					gameObject.SetActive(true);
					Edible component = gameObject.GetComponent<Edible>();
					if (component)
					{
						ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, component.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.BUTCHERED, "{0}", gameObject.GetProperName()), UI.ENDOFDAYREPORT.NOTES.BUTCHERED_CONTEXT);
					}
					if (loot.IsElement)
					{
						PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
						if (component2 != null)
						{
							component2.Mass = loot.Quantity;
						}
					}
					array[i] = gameObject;
				}
			}
			return array;
		}

		// Token: 0x06009C67 RID: 40039 RVA: 0x00390B84 File Offset: 0x0038ED84
		public int GetLootSpawnCell()
		{
			int num = Grid.PosToCell(base.gameObject);
			int num2 = Grid.OffsetCell(num, base.def.lootSpawnOffset);
			if (Grid.IsWorldValidCell(num2) && Grid.IsValidCellInWorld(num2, base.gameObject.GetMyWorldId()))
			{
				return num2;
			}
			return num;
		}

		// Token: 0x06009C68 RID: 40040 RVA: 0x00390BCD File Offset: 0x0038EDCD
		protected override void OnCleanUp()
		{
			base.Unsubscribe(1623392196, new Action<object>(this.OnDeath));
		}
	}
}
