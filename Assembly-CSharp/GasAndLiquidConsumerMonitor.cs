using System;
using UnityEngine;

// Token: 0x02000598 RID: 1432
public class GasAndLiquidConsumerMonitor : GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>
{
	// Token: 0x060020B3 RID: 8371 RVA: 0x000BCE98 File Offset: 0x000BB098
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.cooldown;
		this.cooldown.Enter("ClearTargetCell", delegate(GasAndLiquidConsumerMonitor.Instance smi)
		{
			smi.ClearTargetCell();
		}).ScheduleGoTo((GasAndLiquidConsumerMonitor.Instance smi) => global::UnityEngine.Random.Range(smi.def.minCooldown, smi.def.maxCooldown), this.satisfied);
		this.satisfied.Enter("ClearTargetCell", delegate(GasAndLiquidConsumerMonitor.Instance smi)
		{
			smi.ClearTargetCell();
		}).TagTransition((GasAndLiquidConsumerMonitor.Instance smi) => smi.def.transitionTag, this.looking, false);
		this.looking.ToggleBehaviour((GasAndLiquidConsumerMonitor.Instance smi) => smi.def.behaviourTag, (GasAndLiquidConsumerMonitor.Instance smi) => smi.targetCell != -1, delegate(GasAndLiquidConsumerMonitor.Instance smi)
		{
			smi.GoTo(this.cooldown);
		}).TagTransition((GasAndLiquidConsumerMonitor.Instance smi) => smi.def.transitionTag, this.satisfied, true).PreBrainUpdate(delegate(GasAndLiquidConsumerMonitor.Instance smi)
		{
			smi.FindElement();
		});
	}

	// Token: 0x04001303 RID: 4867
	private GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>.State cooldown;

	// Token: 0x04001304 RID: 4868
	private GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>.State satisfied;

	// Token: 0x04001305 RID: 4869
	private GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>.State looking;

	// Token: 0x0200140B RID: 5131
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006B6F RID: 27503
		public Tag[] transitionTag = new Tag[] { GameTags.Creatures.Hungry };

		// Token: 0x04006B70 RID: 27504
		public Tag behaviourTag = GameTags.Creatures.WantsToEat;

		// Token: 0x04006B71 RID: 27505
		public float minCooldown = 5f;

		// Token: 0x04006B72 RID: 27506
		public float maxCooldown = 5f;

		// Token: 0x04006B73 RID: 27507
		public Diet diet;

		// Token: 0x04006B74 RID: 27508
		public float consumptionRate = 0.5f;

		// Token: 0x04006B75 RID: 27509
		public Tag consumableElementTag = Tag.Invalid;
	}

	// Token: 0x0200140C RID: 5132
	public new class Instance : GameStateMachine<GasAndLiquidConsumerMonitor, GasAndLiquidConsumerMonitor.Instance, IStateMachineTarget, GasAndLiquidConsumerMonitor.Def>.GameInstance
	{
		// Token: 0x06008C43 RID: 35907 RVA: 0x00355564 File Offset: 0x00353764
		public Instance(IStateMachineTarget master, GasAndLiquidConsumerMonitor.Def def)
			: base(master, def)
		{
			this.navigator = base.smi.GetComponent<Navigator>();
			DebugUtil.Assert(base.smi.def.diet != null || this.storage != null, "GasAndLiquidConsumerMonitor needs either a diet or a storage");
		}

		// Token: 0x06008C44 RID: 35908 RVA: 0x003555BC File Offset: 0x003537BC
		public void ClearTargetCell()
		{
			this.targetCell = -1;
			this.massUnavailableFrameCount = 0;
		}

		// Token: 0x06008C45 RID: 35909 RVA: 0x003555CC File Offset: 0x003537CC
		public void FindElement()
		{
			this.targetCell = -1;
			this.FindTargetCell();
		}

		// Token: 0x06008C46 RID: 35910 RVA: 0x003555DB File Offset: 0x003537DB
		public Element GetTargetElement()
		{
			return this.targetElement;
		}

		// Token: 0x06008C47 RID: 35911 RVA: 0x003555E4 File Offset: 0x003537E4
		public bool IsConsumableCell(int cell, out Element element)
		{
			element = Grid.Element[cell];
			bool flag = true;
			bool flag2 = true;
			if (base.smi.def.consumableElementTag != Tag.Invalid)
			{
				flag = element.HasTag(base.smi.def.consumableElementTag);
			}
			if (base.smi.def.diet != null)
			{
				flag2 = false;
				Diet.Info[] infos = base.smi.def.diet.infos;
				for (int i = 0; i < infos.Length; i++)
				{
					if (infos[i].IsMatch(element.tag))
					{
						flag2 = true;
						break;
					}
				}
			}
			return flag && flag2;
		}

		// Token: 0x06008C48 RID: 35912 RVA: 0x00355684 File Offset: 0x00353884
		public void FindTargetCell()
		{
			GasAndLiquidConsumerMonitor.ConsumableCellQuery consumableCellQuery = new GasAndLiquidConsumerMonitor.ConsumableCellQuery(base.smi, 25);
			this.navigator.RunQuery(consumableCellQuery);
			if (consumableCellQuery.success)
			{
				this.targetCell = consumableCellQuery.GetResultCell();
				this.targetElement = consumableCellQuery.targetElement;
			}
		}

		// Token: 0x06008C49 RID: 35913 RVA: 0x003556CC File Offset: 0x003538CC
		public void Consume(float dt)
		{
			int index = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(GasAndLiquidConsumerMonitor.Instance.OnMassConsumedCallback), this, "GasAndLiquidConsumerMonitor").index;
			SimMessages.ConsumeMass(Grid.PosToCell(this), this.targetElement.id, base.def.consumptionRate * dt, 3, index);
		}

		// Token: 0x06008C4A RID: 35914 RVA: 0x00355728 File Offset: 0x00353928
		private static void OnMassConsumedCallback(Sim.MassConsumedCallback mcd, object data)
		{
			((GasAndLiquidConsumerMonitor.Instance)data).OnMassConsumed(mcd);
		}

		// Token: 0x06008C4B RID: 35915 RVA: 0x00355738 File Offset: 0x00353938
		private void OnMassConsumed(Sim.MassConsumedCallback mcd)
		{
			if (!base.IsRunning())
			{
				return;
			}
			if (mcd.mass > 0f)
			{
				if (base.def.diet != null)
				{
					this.massUnavailableFrameCount = 0;
					Diet.Info dietInfo = base.def.diet.GetDietInfo(this.targetElement.tag);
					if (dietInfo == null)
					{
						return;
					}
					float num = dietInfo.ConvertConsumptionMassToCalories(mcd.mass);
					CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = new CreatureCalorieMonitor.CaloriesConsumedEvent
					{
						tag = this.targetElement.tag,
						calories = num
					};
					base.Trigger(-2038961714, caloriesConsumedEvent);
					return;
				}
				else if (this.storage != null)
				{
					this.storage.AddElement(this.targetElement.id, mcd.mass, mcd.temperature, mcd.diseaseIdx, mcd.diseaseCount, false, true);
					return;
				}
			}
			else
			{
				this.massUnavailableFrameCount++;
				if (this.massUnavailableFrameCount >= 2)
				{
					base.Trigger(801383139, null);
				}
			}
		}

		// Token: 0x04006B76 RID: 27510
		public int targetCell = -1;

		// Token: 0x04006B77 RID: 27511
		private Element targetElement;

		// Token: 0x04006B78 RID: 27512
		private Navigator navigator;

		// Token: 0x04006B79 RID: 27513
		private int massUnavailableFrameCount;

		// Token: 0x04006B7A RID: 27514
		[MyCmpGet]
		private Storage storage;
	}

	// Token: 0x0200140D RID: 5133
	public class ConsumableCellQuery : PathFinderQuery
	{
		// Token: 0x06008C4C RID: 35916 RVA: 0x00355838 File Offset: 0x00353A38
		public ConsumableCellQuery(GasAndLiquidConsumerMonitor.Instance smi, int maxIterations)
		{
			this.smi = smi;
			this.maxIterations = maxIterations;
		}

		// Token: 0x06008C4D RID: 35917 RVA: 0x00355850 File Offset: 0x00353A50
		public override bool IsMatch(int cell, int parent_cell, int cost)
		{
			int num = Grid.CellAbove(cell);
			this.success = this.smi.IsConsumableCell(cell, out this.targetElement) || (Grid.IsValidCell(num) && this.smi.IsConsumableCell(num, out this.targetElement));
			if (!this.success)
			{
				int num2 = this.maxIterations - 1;
				this.maxIterations = num2;
				return num2 <= 0;
			}
			return true;
		}

		// Token: 0x04006B7B RID: 27515
		public bool success;

		// Token: 0x04006B7C RID: 27516
		public Element targetElement;

		// Token: 0x04006B7D RID: 27517
		private GasAndLiquidConsumerMonitor.Instance smi;

		// Token: 0x04006B7E RID: 27518
		private int maxIterations;
	}
}
