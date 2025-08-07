using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000472 RID: 1138
public class BingeEatChore : Chore<BingeEatChore.StatesInstance>
{
	// Token: 0x060017F1 RID: 6129 RVA: 0x00084A60 File Offset: 0x00082C60
	public BingeEatChore(IStateMachineTarget target, Action<Chore> on_complete = null)
		: base(Db.Get().ChoreTypes.BingeEat, target, target.GetComponent<ChoreProvider>(), false, on_complete, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new BingeEatChore.StatesInstance(this, target.gameObject);
		base.Subscribe(1121894420, new Action<object>(this.OnEat));
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x00084AC0 File Offset: 0x00082CC0
	private void OnEat(object data)
	{
		Edible edible = (Edible)data;
		if (edible != null)
		{
			base.smi.sm.bingeremaining.Set(Mathf.Max(0f, base.smi.sm.bingeremaining.Get(base.smi) - edible.unitsConsumed), base.smi, false);
		}
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x00084B26 File Offset: 0x00082D26
	public override void Cleanup()
	{
		base.Cleanup();
		base.Unsubscribe(1121894420, new Action<object>(this.OnEat));
	}

	// Token: 0x02001263 RID: 4707
	public class StatesInstance : GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.GameInstance
	{
		// Token: 0x0600861C RID: 34332 RVA: 0x0033B451 File Offset: 0x00339651
		public StatesInstance(BingeEatChore master, GameObject eater)
			: base(master)
		{
			base.sm.eater.Set(eater, base.smi, false);
			base.sm.bingeremaining.Set(2f, base.smi, false);
		}

		// Token: 0x0600861D RID: 34333 RVA: 0x0033B490 File Offset: 0x00339690
		public void FindFood()
		{
			Navigator component = base.GetComponent<Navigator>();
			int num = int.MaxValue;
			Edible edible = null;
			if (base.sm.bingeremaining.Get(base.smi) <= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
			{
				this.GoTo(base.sm.eat_pst);
				return;
			}
			foreach (Edible edible2 in Components.Edibles.Items)
			{
				if (!edible2.HasTag(GameTags.Dehydrated) && !(edible2 == null) && !(edible2 == base.sm.ediblesource.Get<Edible>(base.smi)) && !edible2.isBeingConsumed)
				{
					Pickupable component2 = edible2.GetComponent<Pickupable>();
					if (component2.UnreservedFetchAmount > 0f && component2.CouldBePickedUpByMinion(base.GetComponent<KPrefabID>().InstanceID) && !component2.HasTag(GameTags.StoredPrivate))
					{
						int navigationCost = component.GetNavigationCost(edible2);
						if (navigationCost != -1 && navigationCost < num)
						{
							num = navigationCost;
							edible = edible2;
						}
					}
				}
			}
			base.sm.ediblesource.Set(edible, base.smi);
			base.sm.requestedfoodunits.Set(base.sm.bingeremaining.Get(base.smi), base.smi, false);
			if (edible == null)
			{
				this.GoTo(base.sm.cantFindFood);
				return;
			}
			this.GoTo(base.sm.fetch);
		}

		// Token: 0x0600861E RID: 34334 RVA: 0x0033B62C File Offset: 0x0033982C
		public bool IsBingeEating()
		{
			return base.sm.isBingeEating.Get(base.smi);
		}
	}

	// Token: 0x02001264 RID: 4708
	public class States : GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore>
	{
		// Token: 0x0600861F RID: 34335 RVA: 0x0033B644 File Offset: 0x00339844
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.findfood;
			base.Target(this.eater);
			this.bingeEatingEffect = new Effect("Binge_Eating", DUPLICANTS.MODIFIERS.BINGE_EATING.NAME, DUPLICANTS.MODIFIERS.BINGE_EATING.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
			this.bingeEatingEffect.Add(new AttributeModifier(Db.Get().Attributes.Decor.Id, -30f, DUPLICANTS.MODIFIERS.BINGE_EATING.NAME, false, false, true));
			this.bingeEatingEffect.Add(new AttributeModifier("CaloriesDelta", -6666.6665f, DUPLICANTS.MODIFIERS.BINGE_EATING.NAME, false, false, true));
			Db.Get().effects.Add(this.bingeEatingEffect);
			this.root.ToggleEffect((BingeEatChore.StatesInstance smi) => this.bingeEatingEffect);
			this.noTarget.GoTo(this.finish);
			this.eat_pst.ToggleAnims("anim_eat_overeat_kanim", 0f).PlayAnim("working_pst").OnAnimQueueComplete(this.finish);
			this.finish.Enter(delegate(BingeEatChore.StatesInstance smi)
			{
				smi.StopSM("complete/no more food");
			});
			this.findfood.Enter("FindFood", delegate(BingeEatChore.StatesInstance smi)
			{
				smi.FindFood();
			});
			this.fetch.InitializeStates(this.eater, this.ediblesource, this.ediblechunk, this.requestedfoodunits, this.actualfoodunits, this.eat, this.cantFindFood);
			this.eat.ToggleAnims("anim_eat_overeat_kanim", 0f).QueueAnim("working_loop", true, null).Enter(delegate(BingeEatChore.StatesInstance smi)
			{
				this.isBingeEating.Set(true, smi, false);
			})
				.DoEat(this.ediblechunk, this.actualfoodunits, this.findfood, this.findfood)
				.Exit("ClearIsBingeEating", delegate(BingeEatChore.StatesInstance smi)
				{
					this.isBingeEating.Set(false, smi, false);
				});
			this.cantFindFood.ToggleAnims("anim_interrupt_binge_eat_kanim", 0f).PlayAnim("interrupt_binge_eat").OnAnimQueueComplete(this.noTarget);
		}

		// Token: 0x040065DD RID: 26077
		public StateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.TargetParameter eater;

		// Token: 0x040065DE RID: 26078
		public StateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.TargetParameter ediblesource;

		// Token: 0x040065DF RID: 26079
		public StateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.TargetParameter ediblechunk;

		// Token: 0x040065E0 RID: 26080
		public StateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.BoolParameter isBingeEating;

		// Token: 0x040065E1 RID: 26081
		public StateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.FloatParameter requestedfoodunits;

		// Token: 0x040065E2 RID: 26082
		public StateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.FloatParameter actualfoodunits;

		// Token: 0x040065E3 RID: 26083
		public StateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.FloatParameter bingeremaining;

		// Token: 0x040065E4 RID: 26084
		public GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.State noTarget;

		// Token: 0x040065E5 RID: 26085
		public GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.State findfood;

		// Token: 0x040065E6 RID: 26086
		public GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.State eat;

		// Token: 0x040065E7 RID: 26087
		public GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.State eat_pst;

		// Token: 0x040065E8 RID: 26088
		public GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.State cantFindFood;

		// Token: 0x040065E9 RID: 26089
		public GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.State finish;

		// Token: 0x040065EA RID: 26090
		public GameStateMachine<BingeEatChore.States, BingeEatChore.StatesInstance, BingeEatChore, object>.FetchSubState fetch;

		// Token: 0x040065EB RID: 26091
		private Effect bingeEatingEffect;
	}
}
