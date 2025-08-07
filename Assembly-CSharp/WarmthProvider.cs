using System;
using System.Collections.Generic;

// Token: 0x0200064C RID: 1612
public class WarmthProvider : GameStateMachine<WarmthProvider, WarmthProvider.Instance, IStateMachineTarget, WarmthProvider.Def>
{
	// Token: 0x060026F5 RID: 9973 RVA: 0x000DEBDE File Offset: 0x000DCDDE
	public static bool IsWarmCell(int cell)
	{
		return WarmthProvider.WarmCells.ContainsKey(cell) && WarmthProvider.WarmCells[cell] > 0;
	}

	// Token: 0x060026F6 RID: 9974 RVA: 0x000DEBFD File Offset: 0x000DCDFD
	public static int GetWarmthValue(int cell)
	{
		if (!WarmthProvider.WarmCells.ContainsKey(cell))
		{
			return -1;
		}
		return (int)WarmthProvider.WarmCells[cell];
	}

	// Token: 0x060026F7 RID: 9975 RVA: 0x000DEC1C File Offset: 0x000DCE1C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.off;
		this.off.EventTransition(GameHashes.ActiveChanged, this.on, (WarmthProvider.Instance smi) => smi.GetComponent<Operational>().IsActive).Enter(new StateMachine<WarmthProvider, WarmthProvider.Instance, IStateMachineTarget, WarmthProvider.Def>.State.Callback(WarmthProvider.RemoveWarmCells));
		this.on.EventTransition(GameHashes.ActiveChanged, this.off, (WarmthProvider.Instance smi) => !smi.GetComponent<Operational>().IsActive).TagTransition(GameTags.Operational, this.off, true).Enter(new StateMachine<WarmthProvider, WarmthProvider.Instance, IStateMachineTarget, WarmthProvider.Def>.State.Callback(WarmthProvider.AddWarmCells));
	}

	// Token: 0x060026F8 RID: 9976 RVA: 0x000DECD7 File Offset: 0x000DCED7
	private static void AddWarmCells(WarmthProvider.Instance smi)
	{
		smi.AddWarmCells();
	}

	// Token: 0x060026F9 RID: 9977 RVA: 0x000DECDF File Offset: 0x000DCEDF
	private static void RemoveWarmCells(WarmthProvider.Instance smi)
	{
		smi.RemoveWarmCells();
	}

	// Token: 0x040016C6 RID: 5830
	public static Dictionary<int, byte> WarmCells = new Dictionary<int, byte>();

	// Token: 0x040016C7 RID: 5831
	public GameStateMachine<WarmthProvider, WarmthProvider.Instance, IStateMachineTarget, WarmthProvider.Def>.State off;

	// Token: 0x040016C8 RID: 5832
	public GameStateMachine<WarmthProvider, WarmthProvider.Instance, IStateMachineTarget, WarmthProvider.Def>.State on;

	// Token: 0x020014D4 RID: 5332
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006E0B RID: 28171
		public Vector2I OriginOffset;

		// Token: 0x04006E0C RID: 28172
		public Vector2I RangeMin;

		// Token: 0x04006E0D RID: 28173
		public Vector2I RangeMax;

		// Token: 0x04006E0E RID: 28174
		public Func<int, bool> blockingCellCallback = new Func<int, bool>(Grid.IsSolidCell);
	}

	// Token: 0x020014D5 RID: 5333
	public new class Instance : GameStateMachine<WarmthProvider, WarmthProvider.Instance, IStateMachineTarget, WarmthProvider.Def>.GameInstance
	{
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06008F13 RID: 36627 RVA: 0x0035CBB0 File Offset: 0x0035ADB0
		public bool IsWarming
		{
			get
			{
				return base.IsInsideState(base.sm.on);
			}
		}

		// Token: 0x06008F14 RID: 36628 RVA: 0x0035CBC3 File Offset: 0x0035ADC3
		public Instance(IStateMachineTarget master, WarmthProvider.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008F15 RID: 36629 RVA: 0x0035CBD0 File Offset: 0x0035ADD0
		public override void StartSM()
		{
			EntityCellVisualizer component = base.GetComponent<EntityCellVisualizer>();
			if (component != null)
			{
				component.AddPort(EntityCellVisualizer.Ports.HeatSource, default(CellOffset));
			}
			this.WorldID = base.gameObject.GetMyWorldId();
			this.SetupRange();
			this.CreateCellListeners();
			base.StartSM();
		}

		// Token: 0x06008F16 RID: 36630 RVA: 0x0035CC24 File Offset: 0x0035AE24
		private void SetupRange()
		{
			Vector2I vector2I = Grid.PosToXY(base.transform.GetPosition());
			Vector2I vector2I2 = base.def.OriginOffset;
			this.range_min = base.def.RangeMin;
			this.range_max = base.def.RangeMax;
			Rotatable rotatable;
			if (base.gameObject.TryGetComponent<Rotatable>(out rotatable))
			{
				vector2I2 = rotatable.GetRotatedOffset(vector2I2);
				Vector2I rotatedOffset = rotatable.GetRotatedOffset(this.range_min);
				Vector2I rotatedOffset2 = rotatable.GetRotatedOffset(this.range_max);
				this.range_min.x = ((rotatedOffset.x < rotatedOffset2.x) ? rotatedOffset.x : rotatedOffset2.x);
				this.range_min.y = ((rotatedOffset.y < rotatedOffset2.y) ? rotatedOffset.y : rotatedOffset2.y);
				this.range_max.x = ((rotatedOffset.x > rotatedOffset2.x) ? rotatedOffset.x : rotatedOffset2.x);
				this.range_max.y = ((rotatedOffset.y > rotatedOffset2.y) ? rotatedOffset.y : rotatedOffset2.y);
			}
			this.origin = vector2I + vector2I2;
		}

		// Token: 0x06008F17 RID: 36631 RVA: 0x0035CD58 File Offset: 0x0035AF58
		public bool ContainsCell(int cell)
		{
			if (this.cellsInRange == null)
			{
				return false;
			}
			for (int i = 0; i < this.cellsInRange.Length; i++)
			{
				if (this.cellsInRange[i] == cell)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008F18 RID: 36632 RVA: 0x0035CD90 File Offset: 0x0035AF90
		private void UnmarkAllCellsInRange()
		{
			if (this.cellsInRange != null)
			{
				for (int i = 0; i < this.cellsInRange.Length; i++)
				{
					int num = this.cellsInRange[i];
					if (WarmthProvider.WarmCells.ContainsKey(num))
					{
						Dictionary<int, byte> warmCells = WarmthProvider.WarmCells;
						int num2 = num;
						byte b = warmCells[num2];
						warmCells[num2] = b - 1;
					}
				}
			}
			this.cellsInRange = null;
		}

		// Token: 0x06008F19 RID: 36633 RVA: 0x0035CDF0 File Offset: 0x0035AFF0
		private void UpdateCellsInRange()
		{
			this.UnmarkAllCellsInRange();
			Grid.PosToCell(this);
			List<int> list = new List<int>();
			for (int i = 0; i <= this.range_max.y - this.range_min.y; i++)
			{
				int num = this.origin.y + this.range_min.y + i;
				for (int j = 0; j <= this.range_max.x - this.range_min.x; j++)
				{
					int num2 = Grid.XYToCell(this.origin.x + this.range_min.x + j, num);
					if (Grid.IsValidCellInWorld(num2, this.WorldID) && this.IsCellVisible(num2))
					{
						list.Add(num2);
						if (!WarmthProvider.WarmCells.ContainsKey(num2))
						{
							WarmthProvider.WarmCells.Add(num2, 0);
						}
						Dictionary<int, byte> warmCells = WarmthProvider.WarmCells;
						int num3 = num2;
						byte b = warmCells[num3];
						warmCells[num3] = b + 1;
					}
				}
			}
			this.cellsInRange = list.ToArray();
		}

		// Token: 0x06008F1A RID: 36634 RVA: 0x0035CF02 File Offset: 0x0035B102
		public void AddWarmCells()
		{
			this.UpdateCellsInRange();
		}

		// Token: 0x06008F1B RID: 36635 RVA: 0x0035CF0A File Offset: 0x0035B10A
		public void RemoveWarmCells()
		{
			this.UnmarkAllCellsInRange();
		}

		// Token: 0x06008F1C RID: 36636 RVA: 0x0035CF12 File Offset: 0x0035B112
		protected override void OnCleanUp()
		{
			this.RemoveWarmCells();
			this.ClearCellListeners();
			base.OnCleanUp();
		}

		// Token: 0x06008F1D RID: 36637 RVA: 0x0035CF28 File Offset: 0x0035B128
		public bool IsCellVisible(int cell)
		{
			Vector2I vector2I = Grid.CellToXY(Grid.PosToCell(this));
			Vector2I vector2I2 = Grid.CellToXY(cell);
			return Grid.TestLineOfSight(vector2I.x, vector2I.y, vector2I2.x, vector2I2.y, base.def.blockingCellCallback, false, false);
		}

		// Token: 0x06008F1E RID: 36638 RVA: 0x0035CF72 File Offset: 0x0035B172
		public void OnSolidCellChanged(object obj)
		{
			if (this.IsWarming)
			{
				this.UpdateCellsInRange();
			}
		}

		// Token: 0x06008F1F RID: 36639 RVA: 0x0035CF84 File Offset: 0x0035B184
		private void CreateCellListeners()
		{
			Grid.PosToCell(this);
			List<HandleVector<int>.Handle> list = new List<HandleVector<int>.Handle>();
			for (int i = 0; i <= this.range_max.y - this.range_min.y; i++)
			{
				int num = this.origin.y + this.range_min.y + i;
				for (int j = 0; j <= this.range_max.x - this.range_min.x; j++)
				{
					int num2 = Grid.XYToCell(this.origin.x + this.range_min.x + j, num);
					if (Grid.IsValidCellInWorld(num2, this.WorldID))
					{
						list.Add(GameScenePartitioner.Instance.Add("WarmthProvider Visibility", base.gameObject, num2, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidCellChanged)));
					}
				}
			}
			this.partitionEntries = list.ToArray();
		}

		// Token: 0x06008F20 RID: 36640 RVA: 0x0035D074 File Offset: 0x0035B274
		private void ClearCellListeners()
		{
			if (this.partitionEntries != null)
			{
				for (int i = 0; i < this.partitionEntries.Length; i++)
				{
					HandleVector<int>.Handle handle = this.partitionEntries[i];
					GameScenePartitioner.Instance.Free(ref handle);
				}
			}
		}

		// Token: 0x04006E0F RID: 28175
		public int WorldID;

		// Token: 0x04006E10 RID: 28176
		private int[] cellsInRange;

		// Token: 0x04006E11 RID: 28177
		private HandleVector<int>.Handle[] partitionEntries;

		// Token: 0x04006E12 RID: 28178
		public Vector2I range_min;

		// Token: 0x04006E13 RID: 28179
		public Vector2I range_max;

		// Token: 0x04006E14 RID: 28180
		public Vector2I origin;
	}
}
