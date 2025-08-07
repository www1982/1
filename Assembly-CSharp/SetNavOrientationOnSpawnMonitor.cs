using System;

// Token: 0x020005A1 RID: 1441
public class SetNavOrientationOnSpawnMonitor : GameStateMachine<SetNavOrientationOnSpawnMonitor, SetNavOrientationOnSpawnMonitor.Instance, IStateMachineTarget, SetNavOrientationOnSpawnMonitor.Def>
{
	// Token: 0x060020E3 RID: 8419 RVA: 0x000BDB28 File Offset: 0x000BBD28
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.root;
	}

	// Token: 0x02001426 RID: 5158
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001427 RID: 5159
	public new class Instance : GameStateMachine<SetNavOrientationOnSpawnMonitor, SetNavOrientationOnSpawnMonitor.Instance, IStateMachineTarget, SetNavOrientationOnSpawnMonitor.Def>.GameInstance
	{
		// Token: 0x06008CAC RID: 36012 RVA: 0x00356870 File Offset: 0x00354A70
		public Instance(IStateMachineTarget master, SetNavOrientationOnSpawnMonitor.Def def)
			: base(master, def)
		{
			base.Subscribe(1119167081, new Action<object>(this.SetSpawnOrientation));
		}

		// Token: 0x06008CAD RID: 36013 RVA: 0x00356894 File Offset: 0x00354A94
		public void SetSpawnOrientation(object o)
		{
			int num = Grid.PosToCell(this);
			if (!Grid.IsValidCell(num))
			{
				return;
			}
			int num2 = Grid.CellAbove(num);
			int num3 = Grid.CellBelow(num);
			if (Grid.IsValidCell(num2) && Grid.Solid[num2] && (!Grid.IsValidCell(num3) || !Grid.Solid[num3]))
			{
				base.gameObject.GetComponent<Navigator>().CurrentNavType = NavType.Ceiling;
			}
		}

		// Token: 0x06008CAE RID: 36014 RVA: 0x00356903 File Offset: 0x00354B03
		protected override void OnCleanUp()
		{
			base.Unsubscribe(1119167081, new Action<object>(this.SetSpawnOrientation));
			base.OnCleanUp();
		}
	}
}
