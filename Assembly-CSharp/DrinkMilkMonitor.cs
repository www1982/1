using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Klei.AI;
using UnityEngine;

// Token: 0x02000595 RID: 1429
public class DrinkMilkMonitor : GameStateMachine<DrinkMilkMonitor, DrinkMilkMonitor.Instance, IStateMachineTarget, DrinkMilkMonitor.Def>
{
	// Token: 0x060020A5 RID: 8357 RVA: 0x000BC8B4 File Offset: 0x000BAAB4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.lookingToDrinkMilk;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.OnSignal(this.didFinishDrinkingMilk, this.applyEffect).Enter(delegate(DrinkMilkMonitor.Instance smi)
		{
			this.remainingSecondsForEffect.Set(Mathf.Clamp(this.remainingSecondsForEffect.Get(smi), 0f, 600f), smi, false);
		}).ParamTransition<float>(this.remainingSecondsForEffect, this.satisfied, (DrinkMilkMonitor.Instance smi, float val) => val > 0f);
		this.lookingToDrinkMilk.PreBrainUpdate(new Action<DrinkMilkMonitor.Instance>(DrinkMilkMonitor.FindMilkFeederTarget)).ToggleBehaviour(GameTags.Creatures.Behaviour_TryToDrinkMilkFromFeeder, (DrinkMilkMonitor.Instance smi) => !smi.targetMilkFeeder.IsNullOrStopped() && !smi.targetMilkFeeder.IsReserved(), null).Exit(delegate(DrinkMilkMonitor.Instance smi)
		{
			smi.targetMilkFeeder = null;
		});
		this.applyEffect.Enter(delegate(DrinkMilkMonitor.Instance smi)
		{
			this.remainingSecondsForEffect.Set(600f, smi, false);
		}).EnterTransition(this.satisfied, (DrinkMilkMonitor.Instance smi) => true);
		this.satisfied.Enter(delegate(DrinkMilkMonitor.Instance smi)
		{
			if (smi.def.consumesMilk)
			{
				smi.GetComponent<Effects>().Add("HadMilk", false).timeRemaining = this.remainingSecondsForEffect.Get(smi);
			}
		}).Exit(delegate(DrinkMilkMonitor.Instance smi)
		{
			if (smi.def.consumesMilk)
			{
				smi.GetComponent<Effects>().Remove("HadMilk");
			}
			this.remainingSecondsForEffect.Set(-1f, smi, false);
		}).ScheduleGoTo((DrinkMilkMonitor.Instance smi) => this.remainingSecondsForEffect.Get(smi), this.lookingToDrinkMilk)
			.Update(delegate(DrinkMilkMonitor.Instance smi, float deltaSeconds)
			{
				this.remainingSecondsForEffect.Delta(-deltaSeconds, smi);
				if (this.remainingSecondsForEffect.Get(smi) < 0f)
				{
					smi.GoTo(this.lookingToDrinkMilk);
				}
			}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x060020A6 RID: 8358 RVA: 0x000BCA24 File Offset: 0x000BAC24
	private static void FindMilkFeederTarget(DrinkMilkMonitor.Instance smi)
	{
		DrinkMilkMonitor.<>c__DisplayClass8_0 CS$<>8__locals1;
		CS$<>8__locals1.smi = smi;
		int num = Grid.PosToCell(CS$<>8__locals1.smi.gameObject);
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		List<MilkFeeder.Instance> items = Components.MilkFeeders.GetItems((int)Grid.WorldIdx[num]);
		if (items == null || items.Count == 0)
		{
			return;
		}
		using (ListPool<MilkFeeder.Instance, DrinkMilkMonitor>.PooledList pooledList = PoolsFor<DrinkMilkMonitor>.AllocateList<MilkFeeder.Instance>())
		{
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			if (cavityForCell != null && cavityForCell.room != null && cavityForCell.room.roomType == Db.Get().RoomTypes.CreaturePen)
			{
				foreach (MilkFeeder.Instance instance in items)
				{
					if (!instance.IsNullOrDestroyed())
					{
						int num2 = Grid.PosToCell(instance);
						if (Game.Instance.roomProber.GetCavityForCell(num2) == cavityForCell && instance.IsReadyToStartFeeding())
						{
							pooledList.Add(instance);
						}
					}
				}
			}
			DrinkMilkMonitor.<>c__DisplayClass8_1 CS$<>8__locals2;
			CS$<>8__locals2.canDrown = CS$<>8__locals1.smi.drowningMonitor != null && CS$<>8__locals1.smi.drowningMonitor.canDrownToDeath && !CS$<>8__locals1.smi.drowningMonitor.livesUnderWater;
			CS$<>8__locals1.smi.targetMilkFeeder = null;
			CS$<>8__locals1.smi.doesTargetMilkFeederHaveSpaceForCritter = false;
			CS$<>8__locals2.resultCost = -1;
			foreach (MilkFeeder.Instance instance2 in pooledList)
			{
				DrinkMilkMonitor.<>c__DisplayClass8_2 CS$<>8__locals3;
				CS$<>8__locals3.milkFeeder = instance2;
				if (DrinkMilkMonitor.<FindMilkFeederTarget>g__ConsiderCell|8_0(CS$<>8__locals1.smi.GetDrinkCellOf(CS$<>8__locals3.milkFeeder, false), ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3))
				{
					CS$<>8__locals1.smi.doesTargetMilkFeederHaveSpaceForCritter = false;
				}
				else if (DrinkMilkMonitor.<FindMilkFeederTarget>g__ConsiderCell|8_0(CS$<>8__locals1.smi.GetDrinkCellOf(CS$<>8__locals3.milkFeeder, true), ref CS$<>8__locals1, ref CS$<>8__locals2, ref CS$<>8__locals3))
				{
					CS$<>8__locals1.smi.doesTargetMilkFeederHaveSpaceForCritter = true;
				}
			}
		}
	}

	// Token: 0x060020AE RID: 8366 RVA: 0x000BCD4C File Offset: 0x000BAF4C
	[CompilerGenerated]
	internal static bool <FindMilkFeederTarget>g__ConsiderCell|8_0(int cell, ref DrinkMilkMonitor.<>c__DisplayClass8_0 A_1, ref DrinkMilkMonitor.<>c__DisplayClass8_1 A_2, ref DrinkMilkMonitor.<>c__DisplayClass8_2 A_3)
	{
		if (A_2.canDrown && !A_1.smi.drowningMonitor.IsCellSafe(cell))
		{
			return false;
		}
		int navigationCost = A_1.smi.navigator.GetNavigationCost(cell);
		if (navigationCost == -1)
		{
			return false;
		}
		if (navigationCost < A_2.resultCost || A_2.resultCost == -1)
		{
			A_2.resultCost = navigationCost;
			A_1.smi.targetMilkFeeder = A_3.milkFeeder;
			return true;
		}
		return false;
	}

	// Token: 0x040012FE RID: 4862
	public GameStateMachine<DrinkMilkMonitor, DrinkMilkMonitor.Instance, IStateMachineTarget, DrinkMilkMonitor.Def>.State lookingToDrinkMilk;

	// Token: 0x040012FF RID: 4863
	public GameStateMachine<DrinkMilkMonitor, DrinkMilkMonitor.Instance, IStateMachineTarget, DrinkMilkMonitor.Def>.State applyEffect;

	// Token: 0x04001300 RID: 4864
	public GameStateMachine<DrinkMilkMonitor, DrinkMilkMonitor.Instance, IStateMachineTarget, DrinkMilkMonitor.Def>.State satisfied;

	// Token: 0x04001301 RID: 4865
	private StateMachine<DrinkMilkMonitor, DrinkMilkMonitor.Instance, IStateMachineTarget, DrinkMilkMonitor.Def>.Signal didFinishDrinkingMilk;

	// Token: 0x04001302 RID: 4866
	private StateMachine<DrinkMilkMonitor, DrinkMilkMonitor.Instance, IStateMachineTarget, DrinkMilkMonitor.Def>.FloatParameter remainingSecondsForEffect;

	// Token: 0x020013FF RID: 5119
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006B55 RID: 27477
		public bool consumesMilk = true;

		// Token: 0x04006B56 RID: 27478
		public DrinkMilkStates.Def.DrinkCellOffsetGetFn drinkCellOffsetGetFn;
	}

	// Token: 0x02001400 RID: 5120
	public new class Instance : GameStateMachine<DrinkMilkMonitor, DrinkMilkMonitor.Instance, IStateMachineTarget, DrinkMilkMonitor.Def>.GameInstance
	{
		// Token: 0x06008C2B RID: 35883 RVA: 0x003552E4 File Offset: 0x003534E4
		public Instance(IStateMachineTarget master, DrinkMilkMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008C2C RID: 35884 RVA: 0x003552EE File Offset: 0x003534EE
		public void NotifyFinishedDrinkingMilkFrom(MilkFeeder.Instance milkFeeder)
		{
			if (milkFeeder != null && base.def.consumesMilk)
			{
				milkFeeder.ConsumeMilkForOneFeeding();
			}
			base.sm.didFinishDrinkingMilk.Trigger(base.smi);
		}

		// Token: 0x06008C2D RID: 35885 RVA: 0x0035531C File Offset: 0x0035351C
		public int GetDrinkCellOf(MilkFeeder.Instance milkFeeder, bool isTwoByTwoCritterCramped)
		{
			return Grid.OffsetCell(Grid.PosToCell(milkFeeder), base.def.drinkCellOffsetGetFn(milkFeeder, this, isTwoByTwoCritterCramped));
		}

		// Token: 0x04006B57 RID: 27479
		public MilkFeeder.Instance targetMilkFeeder;

		// Token: 0x04006B58 RID: 27480
		public bool doesTargetMilkFeederHaveSpaceForCritter;

		// Token: 0x04006B59 RID: 27481
		[MyCmpReq]
		public Navigator navigator;

		// Token: 0x04006B5A RID: 27482
		[MyCmpGet]
		public DrowningMonitor drowningMonitor;
	}
}
