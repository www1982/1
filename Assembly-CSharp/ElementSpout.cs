using System;
using UnityEngine;

// Token: 0x02000937 RID: 2359
public class ElementSpout : StateMachineComponent<ElementSpout.StatesInstance>
{
	// Token: 0x060042FE RID: 17150 RVA: 0x001805D0 File Offset: 0x0017E7D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(base.transform.GetPosition());
		Grid.Objects[num, 2] = base.gameObject;
		base.smi.StartSM();
	}

	// Token: 0x060042FF RID: 17151 RVA: 0x00180611 File Offset: 0x0017E811
	public void SetEmitter(ElementEmitter emitter)
	{
		this.emitter = emitter;
	}

	// Token: 0x06004300 RID: 17152 RVA: 0x0018061A File Offset: 0x0017E81A
	public void ConfigureEmissionSettings(float emissionPollFrequency = 3f, float emissionIrregularity = 1.5f, float maxPressure = 1.5f, float perEmitAmount = 0.5f)
	{
		this.maxPressure = maxPressure;
		this.emissionPollFrequency = emissionPollFrequency;
		this.emissionIrregularity = emissionIrregularity;
		this.perEmitAmount = perEmitAmount;
	}

	// Token: 0x04002CB7 RID: 11447
	[SerializeField]
	private ElementEmitter emitter;

	// Token: 0x04002CB8 RID: 11448
	[MyCmpAdd]
	private KBatchedAnimController anim;

	// Token: 0x04002CB9 RID: 11449
	public float maxPressure = 1.5f;

	// Token: 0x04002CBA RID: 11450
	public float emissionPollFrequency = 3f;

	// Token: 0x04002CBB RID: 11451
	public float emissionIrregularity = 1.5f;

	// Token: 0x04002CBC RID: 11452
	public float perEmitAmount = 0.5f;

	// Token: 0x02001914 RID: 6420
	public class StatesInstance : GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.GameInstance
	{
		// Token: 0x06009E44 RID: 40516 RVA: 0x00395F9B File Offset: 0x0039419B
		public StatesInstance(ElementSpout smi)
			: base(smi)
		{
		}

		// Token: 0x06009E45 RID: 40517 RVA: 0x00395FA4 File Offset: 0x003941A4
		private bool CanEmitOnCell(int cell, float max_pressure, Element.State expected_state)
		{
			return Grid.Mass[cell] < max_pressure && (Grid.Element[cell].IsState(expected_state) || Grid.Element[cell].IsVacuum);
		}

		// Token: 0x06009E46 RID: 40518 RVA: 0x00395FD4 File Offset: 0x003941D4
		public bool CanEmitAnywhere()
		{
			int num = Grid.PosToCell(base.smi.transform.GetPosition());
			int num2 = Grid.CellLeft(num);
			int num3 = Grid.CellRight(num);
			int num4 = Grid.CellAbove(num);
			Element.State state = ElementLoader.FindElementByHash(base.smi.master.emitter.outputElement.elementHash).state;
			return false || this.CanEmitOnCell(num, base.smi.master.maxPressure, state) || this.CanEmitOnCell(num2, base.smi.master.maxPressure, state) || this.CanEmitOnCell(num3, base.smi.master.maxPressure, state) || this.CanEmitOnCell(num4, base.smi.master.maxPressure, state);
		}
	}

	// Token: 0x02001915 RID: 6421
	public class States : GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout>
	{
		// Token: 0x06009E47 RID: 40519 RVA: 0x003960AC File Offset: 0x003942AC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.DefaultState(this.idle.unblocked).Enter(delegate(ElementSpout.StatesInstance smi)
			{
				smi.Play("idle", KAnim.PlayMode.Once);
			}).ScheduleGoTo((ElementSpout.StatesInstance smi) => smi.master.emissionPollFrequency, this.emit);
			this.idle.unblocked.ToggleStatusItem(Db.Get().MiscStatusItems.SpoutPressureBuilding, null).Transition(this.idle.blocked, (ElementSpout.StatesInstance smi) => !smi.CanEmitAnywhere(), UpdateRate.SIM_200ms);
			this.idle.blocked.ToggleStatusItem(Db.Get().MiscStatusItems.SpoutOverPressure, null).Transition(this.idle.blocked, (ElementSpout.StatesInstance smi) => smi.CanEmitAnywhere(), UpdateRate.SIM_200ms);
			this.emit.DefaultState(this.emit.unblocked).Enter(delegate(ElementSpout.StatesInstance smi)
			{
				float num = 1f + global::UnityEngine.Random.Range(0f, smi.master.emissionIrregularity);
				float num2 = smi.master.perEmitAmount / num;
				smi.master.emitter.SetEmitting(true);
				smi.master.emitter.emissionFrequency = 1f;
				smi.master.emitter.outputElement.massGenerationRate = num2;
				smi.ScheduleGoTo(num, this.idle);
			});
			this.emit.unblocked.ToggleStatusItem(Db.Get().MiscStatusItems.SpoutEmitting, null).Enter(delegate(ElementSpout.StatesInstance smi)
			{
				smi.Play("emit", KAnim.PlayMode.Once);
				smi.master.emitter.SetEmitting(true);
			}).Transition(this.emit.blocked, (ElementSpout.StatesInstance smi) => !smi.CanEmitAnywhere(), UpdateRate.SIM_200ms);
			this.emit.blocked.ToggleStatusItem(Db.Get().MiscStatusItems.SpoutOverPressure, null).Enter(delegate(ElementSpout.StatesInstance smi)
			{
				smi.Play("idle", KAnim.PlayMode.Once);
				smi.master.emitter.SetEmitting(false);
			}).Transition(this.emit.unblocked, (ElementSpout.StatesInstance smi) => smi.CanEmitAnywhere(), UpdateRate.SIM_200ms);
		}

		// Token: 0x04007B1C RID: 31516
		public ElementSpout.States.Idle idle;

		// Token: 0x04007B1D RID: 31517
		public ElementSpout.States.Emitting emit;

		// Token: 0x02002841 RID: 10305
		public class Idle : GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State
		{
			// Token: 0x0400B282 RID: 45698
			public GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State unblocked;

			// Token: 0x0400B283 RID: 45699
			public GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State blocked;
		}

		// Token: 0x02002842 RID: 10306
		public class Emitting : GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State
		{
			// Token: 0x0400B284 RID: 45700
			public GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State unblocked;

			// Token: 0x0400B285 RID: 45701
			public GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State blocked;
		}
	}
}
