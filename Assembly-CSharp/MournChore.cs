using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000489 RID: 1161
public class MournChore : Chore<MournChore.StatesInstance>
{
	// Token: 0x0600186D RID: 6253 RVA: 0x000883F4 File Offset: 0x000865F4
	private static int GetStandableCell(int cell, Navigator navigator)
	{
		foreach (CellOffset cellOffset in MournChore.ValidStandingOffsets)
		{
			if (Grid.IsCellOffsetValid(cell, cellOffset))
			{
				int num = Grid.OffsetCell(cell, cellOffset);
				if (!Grid.Reserved[num] && navigator.NavGrid.NavTable.IsValid(num, NavType.Floor) && navigator.GetNavigationCost(num) != -1)
				{
					return num;
				}
			}
		}
		return -1;
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x0008845C File Offset: 0x0008665C
	private static KAnimFile GetAnimFileName(MournChore.StatesInstance smi)
	{
		string text = "anim_react_mourning_kanim";
		GameObject gameObject = smi.sm.mourner.Get(smi);
		if (gameObject == null)
		{
			return Assets.GetAnim(text);
		}
		MinionIdentity component = gameObject.GetComponent<MinionIdentity>();
		if (component == null)
		{
			return Assets.GetAnim(text);
		}
		if (component.model == BionicMinionConfig.MODEL)
		{
			return Assets.GetAnim("anim_bionic_react_mourning_kanim");
		}
		return Assets.GetAnim(text);
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x000884E0 File Offset: 0x000866E0
	public MournChore(IStateMachineTarget master)
		: base(Db.Get().ChoreTypes.Mourn, master, master.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new MournChore.StatesInstance(this);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(ChorePreconditions.instance.NoDeadBodies, null);
		this.AddPrecondition(MournChore.HasValidMournLocation, master);
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x00088550 File Offset: 0x00086750
	public static Grave FindGraveToMournAt()
	{
		Grave grave = null;
		float num = -1f;
		foreach (object obj in Components.Graves)
		{
			Grave grave2 = (Grave)obj;
			if (grave2.burialTime > num)
			{
				num = grave2.burialTime;
				grave = grave2;
			}
		}
		return grave;
	}

	// Token: 0x06001871 RID: 6257 RVA: 0x000885C0 File Offset: 0x000867C0
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			global::Debug.LogError("MournChore null context.consumer");
			return;
		}
		if (base.smi == null)
		{
			global::Debug.LogError("MournChore null smi");
			return;
		}
		if (base.smi.sm == null)
		{
			global::Debug.LogError("MournChore null smi.sm");
			return;
		}
		if (MournChore.FindGraveToMournAt() == null)
		{
			global::Debug.LogError("MournChore no grave");
			return;
		}
		base.smi.sm.mourner.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x04000E37 RID: 3639
	private static readonly CellOffset[] ValidStandingOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(-1, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x04000E38 RID: 3640
	private static readonly Chore.Precondition HasValidMournLocation = new Chore.Precondition
	{
		id = "HasPlaceToStand",
		description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_PLACE_TO_STAND,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Navigator component = ((IStateMachineTarget)data).GetComponent<Navigator>();
			bool flag = false;
			Grave grave = MournChore.FindGraveToMournAt();
			if (grave != null && Grid.IsValidCell(MournChore.GetStandableCell(Grid.PosToCell(grave), component)))
			{
				flag = true;
			}
			return flag;
		}
	};

	// Token: 0x02001293 RID: 4755
	public class StatesInstance : GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.GameInstance
	{
		// Token: 0x060086F4 RID: 34548 RVA: 0x0034177F File Offset: 0x0033F97F
		public StatesInstance(MournChore master)
			: base(master)
		{
		}

		// Token: 0x060086F5 RID: 34549 RVA: 0x00341790 File Offset: 0x0033F990
		public void CreateLocator()
		{
			int num = Grid.PosToCell(MournChore.FindGraveToMournAt().transform.GetPosition());
			Navigator component = base.master.GetComponent<Navigator>();
			int standableCell = MournChore.GetStandableCell(num, component);
			if (standableCell < 0)
			{
				base.smi.GoTo(null);
				return;
			}
			Grid.Reserved[standableCell] = true;
			Vector3 vector = Grid.CellToPosCBC(standableCell, Grid.SceneLayer.Move);
			GameObject gameObject = ChoreHelpers.CreateLocator("MournLocator", vector);
			base.smi.sm.locator.Set(gameObject, base.smi, false);
			this.locatorCell = standableCell;
			base.smi.GoTo(base.sm.moveto);
		}

		// Token: 0x060086F6 RID: 34550 RVA: 0x00341834 File Offset: 0x0033FA34
		public void DestroyLocator()
		{
			if (this.locatorCell >= 0)
			{
				Grid.Reserved[this.locatorCell] = false;
				ChoreHelpers.DestroyLocator(base.sm.locator.Get(this));
				base.sm.locator.Set(null, this);
				this.locatorCell = -1;
			}
		}

		// Token: 0x040066CE RID: 26318
		private int locatorCell = -1;
	}

	// Token: 0x02001294 RID: 4756
	public class States : GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore>
	{
		// Token: 0x060086F7 RID: 34551 RVA: 0x0034188C File Offset: 0x0033FA8C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.findOffset;
			base.Target(this.mourner);
			this.root.ToggleAnims(new Func<MournChore.StatesInstance, KAnimFile>(MournChore.GetAnimFileName)).Exit("DestroyLocator", delegate(MournChore.StatesInstance smi)
			{
				smi.DestroyLocator();
			});
			this.findOffset.Enter("CreateLocator", delegate(MournChore.StatesInstance smi)
			{
				smi.CreateLocator();
			});
			this.moveto.InitializeStates(this.mourner, this.locator, this.mourn, null, null, null);
			this.mourn.PlayAnims((MournChore.StatesInstance smi) => MournChore.States.WORK_ANIMS, KAnim.PlayMode.Loop).ScheduleGoTo(10f, this.completed);
			this.completed.PlayAnim("working_pst").OnAnimQueueComplete(null).Exit(delegate(MournChore.StatesInstance smi)
			{
				this.mourner.Get<Effects>(smi).Remove(Db.Get().effects.Get("Mourning"));
			});
		}

		// Token: 0x040066CF RID: 26319
		public StateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.TargetParameter mourner;

		// Token: 0x040066D0 RID: 26320
		public StateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.TargetParameter locator;

		// Token: 0x040066D1 RID: 26321
		public GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.State findOffset;

		// Token: 0x040066D2 RID: 26322
		public GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.ApproachSubState<IApproachable> moveto;

		// Token: 0x040066D3 RID: 26323
		public GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.State mourn;

		// Token: 0x040066D4 RID: 26324
		public GameStateMachine<MournChore.States, MournChore.StatesInstance, MournChore, object>.State completed;

		// Token: 0x040066D5 RID: 26325
		private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "working_pre", "working_loop" };
	}
}
