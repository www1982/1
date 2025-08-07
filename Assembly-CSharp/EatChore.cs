using System;
using System.Collections.Generic;
using FoodRehydrator;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200047C RID: 1148
public class EatChore : Chore<EatChore.StatesInstance>
{
	// Token: 0x0600181D RID: 6173 RVA: 0x00086510 File Offset: 0x00084710
	public EatChore(IStateMachineTarget master)
		: base(Db.Get().ChoreTypes.Eat, master, master.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime)
	{
		base.smi = new EatChore.StatesInstance(this);
		this.showAvailabilityInHoverText = false;
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(EatChore.EdibleIsNotNull, null);
	}

	// Token: 0x0600181E RID: 6174 RVA: 0x00086578 File Offset: 0x00084778
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			global::Debug.LogError("EATCHORE null context.consumer");
			return;
		}
		RationMonitor.Instance smi = context.consumerState.consumer.GetSMI<RationMonitor.Instance>();
		if (smi == null)
		{
			global::Debug.LogError("EATCHORE null RationMonitor.Instance");
			return;
		}
		Edible edible = smi.GetEdible();
		if (edible.gameObject == null)
		{
			global::Debug.LogError("EATCHORE null edible.gameObject");
			return;
		}
		if (base.smi == null)
		{
			global::Debug.LogError("EATCHORE null smi");
			return;
		}
		if (base.smi.sm == null)
		{
			global::Debug.LogError("EATCHORE null smi.sm");
			return;
		}
		if (base.smi.sm.ediblesource == null)
		{
			global::Debug.LogError("EATCHORE null smi.sm.ediblesource");
			return;
		}
		base.smi.sm.ediblesource.Set(edible.gameObject, base.smi, false);
		KCrashReporter.Assert(edible.FoodInfo.CaloriesPerUnit > 0f, edible.GetProperName() + " has invalid calories per unit. Will result in NaNs", null);
		AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(this.gameObject);
		float num = (amountInstance.GetMax() - amountInstance.value) / edible.FoodInfo.CaloriesPerUnit;
		KCrashReporter.Assert(num > 0f, "EatChore is requesting an invalid amount of food", null);
		base.smi.sm.requestedfoodunits.Set(num, base.smi, false);
		base.smi.sm.eater.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x04000E15 RID: 3605
	public static readonly Chore.Precondition EdibleIsNotNull = new Chore.Precondition
	{
		id = "EdibleIsNotNull",
		description = DUPLICANTS.CHORES.PRECONDITIONS.EDIBLE_IS_NOT_NULL,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return null != context.consumerState.consumer.GetSMI<RationMonitor.Instance>().GetEdible();
		}
	};

	// Token: 0x02001273 RID: 4723
	public class StatesInstance : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.GameInstance
	{
		// Token: 0x06008680 RID: 34432 RVA: 0x0033DBFC File Offset: 0x0033BDFC
		public StatesInstance(EatChore master)
			: base(master)
		{
		}

		// Token: 0x06008681 RID: 34433 RVA: 0x0033DC08 File Offset: 0x0033BE08
		public static Assignable GetPreferredMessStation(MinionIdentity minionId)
		{
			Ownables soleOwner = minionId.GetSoleOwner();
			List<Assignable> list = Game.Instance.assignmentManager.GetPreferredAssignables(soleOwner, Db.Get().AssignableSlots.MessStation);
			if (list.Count == 0)
			{
				soleOwner.AutoAssignSlot(Db.Get().AssignableSlots.MessStation);
				list = Game.Instance.assignmentManager.GetPreferredAssignables(soleOwner, Db.Get().AssignableSlots.MessStation);
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list[0];
		}

		// Token: 0x06008682 RID: 34434 RVA: 0x0033DC8C File Offset: 0x0033BE8C
		public void UpdateMessStation()
		{
			base.smi.sm.messstation.Set(EatChore.StatesInstance.GetPreferredMessStation(base.sm.eater.Get(base.smi).GetComponent<MinionIdentity>()), base.smi);
		}

		// Token: 0x06008683 RID: 34435 RVA: 0x0033DCCC File Offset: 0x0033BECC
		public bool UseSalt()
		{
			if (base.smi.sm.messstation != null && base.smi.sm.messstation.Get(base.smi) != null)
			{
				MessStation component = base.smi.sm.messstation.Get(base.smi).GetComponent<MessStation>();
				return component != null && component.HasSalt;
			}
			return false;
		}

		// Token: 0x06008684 RID: 34436 RVA: 0x0033DD44 File Offset: 0x0033BF44
		public static ValueTuple<GameObject, int> CreateLocator(Sensors sensors, Transform transform, string locatorName)
		{
			int num = sensors.GetSensor<SafeCellSensor>().GetCellQuery();
			if (num == Grid.InvalidCell)
			{
				num = Grid.PosToCell(transform.GetPosition());
			}
			Vector3 vector = Grid.CellToPosCBC(num, Grid.SceneLayer.Move);
			Grid.Reserved[num] = true;
			return new ValueTuple<GameObject, int>(ChoreHelpers.CreateLocator(locatorName, vector), num);
		}

		// Token: 0x06008685 RID: 34437 RVA: 0x0033DD94 File Offset: 0x0033BF94
		public void CreateLocator()
		{
			ValueTuple<GameObject, int> valueTuple = EatChore.StatesInstance.CreateLocator(base.sm.eater.Get<Sensors>(base.smi), base.sm.eater.Get<Transform>(base.smi), "EatLocator");
			GameObject item = valueTuple.Item1;
			this.locatorCell = valueTuple.Item2;
			base.sm.locator.Set(item, this, false);
		}

		// Token: 0x06008686 RID: 34438 RVA: 0x0033DDFF File Offset: 0x0033BFFF
		public void DestroyLocator()
		{
			Grid.Reserved[this.locatorCell] = false;
			ChoreHelpers.DestroyLocator(base.sm.locator.Get(this));
			base.sm.locator.Set(null, this);
		}

		// Token: 0x06008687 RID: 34439 RVA: 0x0033DE3C File Offset: 0x0033C03C
		public static void SetZ(GameObject go, float z)
		{
			Vector3 position = go.transform.GetPosition();
			position.z = z;
			go.transform.SetPosition(position);
		}

		// Token: 0x06008688 RID: 34440 RVA: 0x0033DE6C File Offset: 0x0033C06C
		public static void ApplyRoomAndSaltEffects(GameObject messStation, GameObject diner, float? effectDurationOverride = null)
		{
			Effects component = diner.GetComponent<Effects>();
			Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(messStation);
			Storage component2 = messStation.GetComponent<Storage>();
			EffectInstance effectInstance = null;
			if (component2 != null && component2.Has(TableSaltConfig.ID.ToTag()))
			{
				component2.ConsumeIgnoringDisease(TableSaltConfig.ID.ToTag(), TableSaltTuning.CONSUMABLE_RATE);
				effectInstance = component.Add("MessTableSalt", true);
				messStation.Trigger(1356255274, null);
			}
			if (effectDurationOverride != null)
			{
				List<EffectInstance> list = null;
				if (roomOfGameObject != null)
				{
					roomOfGameObject.roomType.TriggerRoomEffects(messStation.GetComponent<KPrefabID>(), component, out list);
				}
				if (effectInstance != null)
				{
					if (list == null)
					{
						list = new List<EffectInstance>();
					}
					list.Add(effectInstance);
				}
				if (list == null)
				{
					return;
				}
				using (List<EffectInstance>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EffectInstance effectInstance2 = enumerator.Current;
						effectInstance2.timeRemaining = effectDurationOverride.Value;
					}
					return;
				}
			}
			if (roomOfGameObject != null)
			{
				roomOfGameObject.roomType.TriggerRoomEffects(messStation.GetComponent<KPrefabID>(), component);
			}
		}

		// Token: 0x04006652 RID: 26194
		private int locatorCell;
	}

	// Token: 0x02001274 RID: 4724
	public class States : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore>
	{
		// Token: 0x06008689 RID: 34441 RVA: 0x0033DF7C File Offset: 0x0033C17C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.chooseaction;
			base.Target(this.eater);
			this.root.Enter("SetMessStation", delegate(EatChore.StatesInstance smi)
			{
				smi.UpdateMessStation();
			}).EventHandler(GameHashes.AssignablesChanged, delegate(EatChore.StatesInstance smi)
			{
				smi.UpdateMessStation();
			});
			this.chooseaction.EnterTransition(this.rehydrate, (EatChore.StatesInstance smi) => this.ediblesource.Get(smi).HasTag(GameTags.Dehydrated)).EnterTransition(this.fetch, (EatChore.StatesInstance smi) => true);
			this.rehydrate.Enter(delegate(EatChore.StatesInstance smi)
			{
				DehydratedFoodPackage component = this.ediblesource.Get(smi).GetComponent<Pickupable>().storage.gameObject.GetComponent<DehydratedFoodPackage>();
				this.rehydrate.foodpackage.Set(component, smi);
				GameObject rehydrator = component.Rehydrator;
				this.rehydrate.rehydrator.Set((rehydrator != null) ? component.Rehydrator.GetComponent<AccessabilityManager>() : null, smi, false);
				AccessabilityManager accessabilityManager = this.rehydrate.rehydrator.Get(smi);
				if (!(accessabilityManager != null))
				{
					smi.GoTo(null);
					return;
				}
				GameObject gameObject = this.eater.Get(smi);
				if (accessabilityManager.CanAccess(gameObject))
				{
					accessabilityManager.Reserve(this.eater.Get(smi));
					return;
				}
				smi.GoTo(null);
			}).Exit(delegate(EatChore.StatesInstance smi)
			{
				AccessabilityManager accessabilityManager2 = this.rehydrate.rehydrator.Get(smi);
				if (accessabilityManager2 != null)
				{
					accessabilityManager2.Unreserve();
				}
			}).DefaultState(this.rehydrate.approach);
			this.rehydrate.approach.InitializeStates(this.eater, this.rehydrate.foodpackage, this.rehydrate.work, null, null, NavigationTactics.ReduceTravelDistance).OnTargetLost(this.ediblesource, null);
			this.rehydrate.work.ToggleWork("Rehydrate", delegate(EatChore.StatesInstance smi)
			{
				WorkerBase workerBase = this.eater.Get<WorkerBase>(smi);
				DehydratedFoodPackage dehydratedFoodPackage = this.rehydrate.foodpackage.Get<DehydratedFoodPackage>(smi);
				workerBase.StartWork(new DehydratedFoodPackage.RehydrateStartWorkItem(dehydratedFoodPackage, delegate(GameObject result)
				{
					this.ediblechunk.Set(result, smi, false);
				}));
			}, delegate(EatChore.StatesInstance smi)
			{
				AccessabilityManager accessabilityManager3 = this.rehydrate.rehydrator.Get(smi);
				return !(accessabilityManager3 == null) && accessabilityManager3.CanAccess(this.eater.Get<WorkerBase>(smi).gameObject);
			}, this.eatatmessstation, null);
			this.fetch.InitializeStates(this.eater, this.ediblesource, this.ediblechunk, this.requestedfoodunits, this.actualfoodunits, this.eatatmessstation, null);
			this.eatatmessstation.DefaultState(this.eatatmessstation.moveto).ParamTransition<GameObject>(this.messstation, this.eatonfloorstate, (EatChore.StatesInstance smi, GameObject p) => p == null).ParamTransition<GameObject>(this.messstation, this.eatonfloorstate, (EatChore.StatesInstance smi, GameObject p) => p != null && !p.GetComponent<Operational>().IsOperational);
			this.eatatmessstation.moveto.InitializeStates(this.eater, this.messstation, this.eatatmessstation.eat, this.eatonfloorstate, null, null);
			this.eatatmessstation.eat.Enter("AnimOverride", delegate(EatChore.StatesInstance smi)
			{
				smi.GetComponent<KAnimControllerBase>().AddAnimOverrides(Assets.GetAnim("anim_eat_table_kanim"), 0f);
			}).DoEat(this.ediblechunk, this.actualfoodunits, null, null).Enter(delegate(EatChore.StatesInstance smi)
			{
				GameObject gameObject2 = this.eater.Get(smi);
				EatChore.StatesInstance.SetZ(gameObject2, Grid.GetLayerZ(Grid.SceneLayer.BuildingFront));
				EatChore.StatesInstance.ApplyRoomAndSaltEffects(this.messstation.Get(smi), gameObject2, null);
			})
				.Exit(delegate(EatChore.StatesInstance smi)
				{
					EatChore.StatesInstance.SetZ(this.eater.Get(smi), Grid.GetLayerZ(Grid.SceneLayer.Move));
					smi.GetComponent<KAnimControllerBase>().RemoveAnimOverrides(Assets.GetAnim("anim_eat_table_kanim"));
				});
			this.eatonfloorstate.DefaultState(this.eatonfloorstate.moveto).Enter("CreateLocator", delegate(EatChore.StatesInstance smi)
			{
				smi.CreateLocator();
			}).Exit("DestroyLocator", delegate(EatChore.StatesInstance smi)
			{
				smi.DestroyLocator();
			});
			this.eatonfloorstate.moveto.InitializeStates(this.eater, this.locator, this.eatonfloorstate.eat, this.eatonfloorstate.eat, null, null);
			this.eatonfloorstate.eat.ToggleAnims("anim_eat_floor_kanim", 0f).DoEat(this.ediblechunk, this.actualfoodunits, null, null);
		}

		// Token: 0x04006653 RID: 26195
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter eater;

		// Token: 0x04006654 RID: 26196
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter ediblesource;

		// Token: 0x04006655 RID: 26197
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter ediblechunk;

		// Token: 0x04006656 RID: 26198
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter messstation;

		// Token: 0x04006657 RID: 26199
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.FloatParameter requestedfoodunits;

		// Token: 0x04006658 RID: 26200
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.FloatParameter actualfoodunits;

		// Token: 0x04006659 RID: 26201
		public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter locator;

		// Token: 0x0400665A RID: 26202
		public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State chooseaction;

		// Token: 0x0400665B RID: 26203
		public EatChore.States.RehydrateSubState rehydrate;

		// Token: 0x0400665C RID: 26204
		public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.FetchSubState fetch;

		// Token: 0x0400665D RID: 26205
		public EatChore.States.EatOnFloorState eatonfloorstate;

		// Token: 0x0400665E RID: 26206
		public EatChore.States.EatAtMessStationState eatatmessstation;

		// Token: 0x02002642 RID: 9794
		public class EatOnFloorState : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State
		{
			// Token: 0x0400AA19 RID: 43545
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.ApproachSubState<IApproachable> moveto;

			// Token: 0x0400AA1A RID: 43546
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State eat;
		}

		// Token: 0x02002643 RID: 9795
		public class EatAtMessStationState : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State
		{
			// Token: 0x0400AA1B RID: 43547
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.ApproachSubState<MessStation> moveto;

			// Token: 0x0400AA1C RID: 43548
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State eat;
		}

		// Token: 0x02002644 RID: 9796
		public class RehydrateSubState : GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State
		{
			// Token: 0x0400AA1D RID: 43549
			public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.TargetParameter foodpackage;

			// Token: 0x0400AA1E RID: 43550
			public StateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.ObjectParameter<AccessabilityManager> rehydrator;

			// Token: 0x0400AA1F RID: 43551
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.ApproachSubState<DehydratedFoodPackage> approach;

			// Token: 0x0400AA20 RID: 43552
			public GameStateMachine<EatChore.States, EatChore.StatesInstance, EatChore, object>.State work;
		}
	}
}
