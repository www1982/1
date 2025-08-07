using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020003C6 RID: 966
public class SweepStates : GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>
{
	// Token: 0x060013AE RID: 5038 RVA: 0x000701A8 File Offset: 0x0006E3A8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.beginPatrol;
		this.beginPatrol.Enter(delegate(SweepStates.Instance smi)
		{
			smi.sm.timeUntilBored.Set(30f, smi, false);
			smi.GoTo(this.moving);
			SweepStates.Instance smi2 = smi;
			smi2.OnStop = (Action<string, StateMachine.Status>)Delegate.Combine(smi2.OnStop, new Action<string, StateMachine.Status>(delegate(string data, StateMachine.Status status)
			{
				this.StopMoveSound(smi);
			}));
		});
		this.moving.Enter(delegate(SweepStates.Instance smi)
		{
		}).MoveTo((SweepStates.Instance smi) => this.GetNextCell(smi), this.pause, this.redirected, false).Update(delegate(SweepStates.Instance smi, float dt)
		{
			smi.sm.timeUntilBored.Set(smi.sm.timeUntilBored.Get(smi) - dt, smi, false);
			if (smi.sm.timeUntilBored.Get(smi) <= 0f)
			{
				smi.sm.bored.Set(true, smi, false);
				smi.sm.timeUntilBored.Set(30f, smi, false);
				smi.master.gameObject.GetSMI<AnimInterruptMonitor.Instance>().PlayAnim("react_bored");
			}
			StorageUnloadMonitor.Instance smi3 = smi.master.gameObject.GetSMI<StorageUnloadMonitor.Instance>();
			Storage storage = smi3.sm.sweepLocker.Get(smi3);
			if (storage != null && smi.sm.headingRight.Get(smi) == smi.master.transform.position.x > storage.transform.position.x)
			{
				Navigator component = smi.master.gameObject.GetComponent<Navigator>();
				if (component.GetNavigationCost(Grid.PosToCell(storage)) >= component.maxProbingRadius - 1)
				{
					smi.GoTo(smi.sm.emoteRedirected);
				}
			}
		}, UpdateRate.SIM_1000ms, false);
		this.emoteRedirected.Enter(delegate(SweepStates.Instance smi)
		{
			this.StopMoveSound(smi);
			int num = Grid.PosToCell(smi.master.gameObject);
			if (Grid.IsCellOffsetValid(num, this.headingRight.Get(smi) ? 1 : (-1), -1) && !Grid.Solid[Grid.OffsetCell(num, this.headingRight.Get(smi) ? 1 : (-1), -1)])
			{
				smi.Play("gap", KAnim.PlayMode.Once);
			}
			else
			{
				smi.Play("bump", KAnim.PlayMode.Once);
			}
			this.headingRight.Set(!this.headingRight.Get(smi), smi, false);
		}).OnAnimQueueComplete(this.pause);
		this.redirected.StopMoving().GoTo(this.emoteRedirected);
		this.sweep.PlayAnim("pickup").ToggleEffect("BotSweeping").Enter(delegate(SweepStates.Instance smi)
		{
			this.StopMoveSound(smi);
			smi.sm.bored.Set(false, smi, false);
			smi.sm.timeUntilBored.Set(30f, smi, false);
		})
			.OnAnimQueueComplete(this.moving);
		this.pause.Enter(delegate(SweepStates.Instance smi)
		{
			if (Grid.IsLiquid(Grid.PosToCell(smi)))
			{
				smi.GoTo(this.mopping);
				return;
			}
			if (this.TrySweep(smi))
			{
				smi.GoTo(this.sweep);
				return;
			}
			smi.GoTo(this.moving);
		});
		this.mopping.PlayAnim("mop_pre", KAnim.PlayMode.Once).QueueAnim("mop_loop", true, null).ToggleEffect("BotMopping")
			.Enter(delegate(SweepStates.Instance smi)
			{
				smi.sm.timeUntilBored.Set(30f, smi, false);
				smi.sm.bored.Set(false, smi, false);
				this.StopMoveSound(smi);
			})
			.Update(delegate(SweepStates.Instance smi, float dt)
			{
				if (smi.timeinstate > 16f || !Grid.IsLiquid(Grid.PosToCell(smi)))
				{
					smi.GoTo(this.moving);
					return;
				}
				this.TryMop(smi, dt);
			}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x00070319 File Offset: 0x0006E519
	public void StopMoveSound(SweepStates.Instance smi)
	{
		LoopingSounds component = smi.gameObject.GetComponent<LoopingSounds>();
		component.StopSound(GlobalAssets.GetSound("SweepBot_mvmt_lp", false));
		component.StopAllSounds();
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x0007033C File Offset: 0x0006E53C
	public void StartMoveSound(SweepStates.Instance smi)
	{
		LoopingSounds component = smi.gameObject.GetComponent<LoopingSounds>();
		if (!component.IsSoundPlaying(GlobalAssets.GetSound("SweepBot_mvmt_lp", false)))
		{
			component.StartSound(GlobalAssets.GetSound("SweepBot_mvmt_lp", false));
		}
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x0007037C File Offset: 0x0006E57C
	public void TryMop(SweepStates.Instance smi, float dt)
	{
		int cell = Grid.PosToCell(smi);
		if (Grid.IsLiquid(cell))
		{
			Moppable.MopCell(cell, Mathf.Min(Grid.Mass[cell], 10f * dt), delegate(Sim.MassConsumedCallback mass_cb_info, object data)
			{
				if (this == null)
				{
					return;
				}
				if (mass_cb_info.mass > 0f)
				{
					SubstanceChunk substanceChunk = LiquidSourceManager.Instance.CreateChunk(ElementLoader.elements[(int)mass_cb_info.elemIdx], mass_cb_info.mass, mass_cb_info.temperature, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount, Grid.CellToPosCCC(cell, Grid.SceneLayer.Ore));
					substanceChunk.transform.SetPosition(substanceChunk.transform.GetPosition() + new Vector3((global::UnityEngine.Random.value - 0.5f) * 0.5f, 0f, 0f));
					this.TryStore(substanceChunk.gameObject, smi);
				}
			});
		}
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x000703F0 File Offset: 0x0006E5F0
	public bool TrySweep(SweepStates.Instance smi)
	{
		int num = Grid.PosToCell(smi);
		GameObject gameObject = Grid.Objects[num, 3];
		if (gameObject != null)
		{
			for (ObjectLayerListItem objectLayerListItem = gameObject.GetComponent<Pickupable>().objectLayerListItem.nextItem; objectLayerListItem != null; objectLayerListItem = objectLayerListItem.nextItem)
			{
				if (this.TryStore(objectLayerListItem.gameObject, smi))
				{
					return true;
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x0007044C File Offset: 0x0006E64C
	public bool TryStore(GameObject go, SweepStates.Instance smi)
	{
		Pickupable pickupable = go.GetComponent<Pickupable>();
		if (pickupable == null)
		{
			return false;
		}
		Storage storage = smi.master.gameObject.GetComponents<Storage>()[1];
		if (storage.IsFull())
		{
			return false;
		}
		if (pickupable != null && pickupable.absorbable)
		{
			SingleEntityReceptacle component = smi.master.GetComponent<SingleEntityReceptacle>();
			if (pickupable.gameObject == component.Occupant)
			{
				return false;
			}
			bool flag;
			if (pickupable.TotalAmount > 10f)
			{
				pickupable.GetComponent<EntitySplitter>();
				pickupable = EntitySplitter.Split(pickupable, Mathf.Min(10f, storage.RemainingCapacity()), null);
				smi.gameObject.GetAmounts().GetValue(Db.Get().Amounts.InternalBattery.Id);
				storage.Store(pickupable.gameObject, false, false, true, false);
				flag = true;
			}
			else
			{
				smi.gameObject.GetAmounts().GetValue(Db.Get().Amounts.InternalBattery.Id);
				storage.Store(pickupable.gameObject, false, false, true, false);
				flag = true;
			}
			if (flag)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x00070568 File Offset: 0x0006E768
	public int GetNextCell(SweepStates.Instance smi)
	{
		int i = 0;
		int num = Grid.PosToCell(smi);
		int num2 = Grid.InvalidCell;
		if (!Grid.Solid[Grid.CellBelow(num)])
		{
			return Grid.InvalidCell;
		}
		if (Grid.Solid[num])
		{
			return Grid.InvalidCell;
		}
		while (i < 1)
		{
			num2 = (smi.sm.headingRight.Get(smi) ? Grid.CellRight(num) : Grid.CellLeft(num));
			if (!Grid.IsValidCell(num2) || Grid.Solid[num2] || !Grid.IsValidCell(Grid.CellBelow(num2)) || !Grid.Solid[Grid.CellBelow(num2)])
			{
				break;
			}
			num = num2;
			i++;
		}
		if (num == Grid.PosToCell(smi))
		{
			return Grid.InvalidCell;
		}
		return num;
	}

	// Token: 0x04000BE7 RID: 3047
	public const float TIME_UNTIL_BORED = 30f;

	// Token: 0x04000BE8 RID: 3048
	public const string MOVE_LOOP_SOUND = "SweepBot_mvmt_lp";

	// Token: 0x04000BE9 RID: 3049
	public StateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.BoolParameter headingRight;

	// Token: 0x04000BEA RID: 3050
	private StateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.FloatParameter timeUntilBored;

	// Token: 0x04000BEB RID: 3051
	public StateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.BoolParameter bored;

	// Token: 0x04000BEC RID: 3052
	private GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.State beginPatrol;

	// Token: 0x04000BED RID: 3053
	private GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.State moving;

	// Token: 0x04000BEE RID: 3054
	private GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.State pause;

	// Token: 0x04000BEF RID: 3055
	private GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.State mopping;

	// Token: 0x04000BF0 RID: 3056
	private GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.State redirected;

	// Token: 0x04000BF1 RID: 3057
	private GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.State emoteRedirected;

	// Token: 0x04000BF2 RID: 3058
	private GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.State sweep;

	// Token: 0x02001205 RID: 4613
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001206 RID: 4614
	public new class Instance : GameStateMachine<SweepStates, SweepStates.Instance, IStateMachineTarget, SweepStates.Def>.GameInstance
	{
		// Token: 0x060084AC RID: 33964 RVA: 0x0033655B File Offset: 0x0033475B
		public Instance(Chore<SweepStates.Instance> chore, SweepStates.Def def)
			: base(chore, def)
		{
		}

		// Token: 0x060084AD RID: 33965 RVA: 0x00336565 File Offset: 0x00334765
		public override void StartSM()
		{
			base.StartSM();
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().RobotStatusItems.Working, base.gameObject);
		}

		// Token: 0x060084AE RID: 33966 RVA: 0x0033659D File Offset: 0x0033479D
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().RobotStatusItems.Working, false);
		}
	}
}
