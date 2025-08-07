using System;

// Token: 0x02000D25 RID: 3365
public class Lure : GameStateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>
{
	// Token: 0x060067C9 RID: 26569 RVA: 0x00271CF8 File Offset: 0x0026FEF8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.off;
		this.off.DoNothing();
		this.on.Enter(new StateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.State.Callback(this.AddToScenePartitioner)).Exit(new StateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.State.Callback(this.RemoveFromScenePartitioner));
	}

	// Token: 0x060067CA RID: 26570 RVA: 0x00271D4C File Offset: 0x0026FF4C
	private void AddToScenePartitioner(Lure.Instance smi)
	{
		Extents extents = new Extents(smi.cell, smi.def.radius);
		smi.partitionerEntry = GameScenePartitioner.Instance.Add(this.name, smi, extents, GameScenePartitioner.Instance.lure, null);
	}

	// Token: 0x060067CB RID: 26571 RVA: 0x00271D94 File Offset: 0x0026FF94
	private void RemoveFromScenePartitioner(Lure.Instance smi)
	{
		GameScenePartitioner.Instance.Free(ref smi.partitionerEntry);
	}

	// Token: 0x04004730 RID: 18224
	public GameStateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.State off;

	// Token: 0x04004731 RID: 18225
	public GameStateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.State on;

	// Token: 0x02001EEE RID: 7918
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04008F4B RID: 36683
		public CellOffset[] defaultLurePoints = new CellOffset[1];

		// Token: 0x04008F4C RID: 36684
		public int radius = 50;

		// Token: 0x04008F4D RID: 36685
		public Tag[] initialLures;
	}

	// Token: 0x02001EEF RID: 7919
	public new class Instance : GameStateMachine<Lure, Lure.Instance, IStateMachineTarget, Lure.Def>.GameInstance
	{
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x0600B1D4 RID: 45524 RVA: 0x003D636C File Offset: 0x003D456C
		public int cell
		{
			get
			{
				if (this._cell == -1)
				{
					this._cell = Grid.PosToCell(base.transform.GetPosition());
				}
				return this._cell;
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600B1D5 RID: 45525 RVA: 0x003D6393 File Offset: 0x003D4593
		// (set) Token: 0x0600B1D6 RID: 45526 RVA: 0x003D63AF File Offset: 0x003D45AF
		public CellOffset[] LurePoints
		{
			get
			{
				if (this._lurePoints == null)
				{
					return base.def.defaultLurePoints;
				}
				return this._lurePoints;
			}
			set
			{
				this._lurePoints = value;
			}
		}

		// Token: 0x0600B1D7 RID: 45527 RVA: 0x003D63B8 File Offset: 0x003D45B8
		public Instance(IStateMachineTarget master, Lure.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600B1D8 RID: 45528 RVA: 0x003D63C9 File Offset: 0x003D45C9
		public override void StartSM()
		{
			base.StartSM();
			if (base.def.initialLures != null)
			{
				this.SetActiveLures(base.def.initialLures);
			}
		}

		// Token: 0x0600B1D9 RID: 45529 RVA: 0x003D63F0 File Offset: 0x003D45F0
		public void ChangeLureCellPosition(int newCell)
		{
			bool flag = base.IsInsideState(base.sm.on);
			if (flag)
			{
				this.GoTo(base.sm.off);
			}
			this.LurePoints = new CellOffset[] { Grid.GetOffset(Grid.PosToCell(base.smi.transform.GetPosition()), newCell) };
			this._cell = newCell;
			if (flag)
			{
				this.GoTo(base.sm.on);
			}
		}

		// Token: 0x0600B1DA RID: 45530 RVA: 0x003D646C File Offset: 0x003D466C
		public void SetActiveLures(Tag[] lures)
		{
			this.lures = lures;
			if (lures == null || lures.Length == 0)
			{
				this.GoTo(base.sm.off);
				return;
			}
			this.GoTo(base.sm.on);
		}

		// Token: 0x0600B1DB RID: 45531 RVA: 0x003D649F File Offset: 0x003D469F
		public bool IsActive()
		{
			return this.GetCurrentState() == base.sm.on;
		}

		// Token: 0x0600B1DC RID: 45532 RVA: 0x003D64B4 File Offset: 0x003D46B4
		public bool HasAnyLure(Tag[] creature_lures)
		{
			if (this.lures == null || creature_lures == null)
			{
				return false;
			}
			foreach (Tag tag in creature_lures)
			{
				foreach (Tag tag2 in this.lures)
				{
					if (tag == tag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04008F4E RID: 36686
		private int _cell = -1;

		// Token: 0x04008F4F RID: 36687
		private Tag[] lures;

		// Token: 0x04008F50 RID: 36688
		public HandleVector<int>.Handle partitionerEntry;

		// Token: 0x04008F51 RID: 36689
		private CellOffset[] _lurePoints;
	}
}
