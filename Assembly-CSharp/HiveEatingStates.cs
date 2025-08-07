using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class HiveEatingStates : GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>
{
	// Token: 0x0600045B RID: 1115 RVA: 0x000241B4 File Offset: 0x000223B4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.eating;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State state = this.eating;
		string text = CREATURES.STATUSITEMS.HIVE_DIGESTING.NAME;
		string text2 = CREATURES.STATUSITEMS.HIVE_DIGESTING.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).DefaultState(this.eating.pre).Enter(delegate(HiveEatingStates.Instance smi)
		{
			smi.TurnOn();
		})
			.Exit(delegate(HiveEatingStates.Instance smi)
			{
				smi.TurnOff();
			});
		this.eating.pre.PlayAnim("eating_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.eating.loop);
		this.eating.loop.PlayAnim("eating_loop", KAnim.PlayMode.Loop).Update(delegate(HiveEatingStates.Instance smi, float dt)
		{
			smi.EatOreFromStorage(smi, dt);
		}, UpdateRate.SIM_4000ms, false).EventTransition(GameHashes.OnStorageChange, this.eating.pst, (HiveEatingStates.Instance smi) => !smi.storage.FindFirst(smi.def.consumedOre));
		this.eating.pst.PlayAnim("eating_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToEat, false);
	}

	// Token: 0x04000333 RID: 819
	public HiveEatingStates.EatingStates eating;

	// Token: 0x04000334 RID: 820
	public GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State behaviourcomplete;

	// Token: 0x020010F6 RID: 4342
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06008123 RID: 33059 RVA: 0x0032E7B4 File Offset: 0x0032C9B4
		public Def(Tag consumedOre)
		{
			this.consumedOre = consumedOre;
		}

		// Token: 0x040061A8 RID: 25000
		public Tag consumedOre;
	}

	// Token: 0x020010F7 RID: 4343
	public class EatingStates : GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State
	{
		// Token: 0x040061A9 RID: 25001
		public GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State pre;

		// Token: 0x040061AA RID: 25002
		public GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State loop;

		// Token: 0x040061AB RID: 25003
		public GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.State pst;
	}

	// Token: 0x020010F8 RID: 4344
	public new class Instance : GameStateMachine<HiveEatingStates, HiveEatingStates.Instance, IStateMachineTarget, HiveEatingStates.Def>.GameInstance
	{
		// Token: 0x06008125 RID: 33061 RVA: 0x0032E7CB File Offset: 0x0032C9CB
		public Instance(Chore<HiveEatingStates.Instance> chore, HiveEatingStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToEat);
		}

		// Token: 0x06008126 RID: 33062 RVA: 0x0032E7EF File Offset: 0x0032C9EF
		public void TurnOn()
		{
			this.emitter.emitRads = 600f * this.emitter.emitRate;
			this.emitter.Refresh();
		}

		// Token: 0x06008127 RID: 33063 RVA: 0x0032E818 File Offset: 0x0032CA18
		public void TurnOff()
		{
			this.emitter.emitRads = 0f;
			this.emitter.Refresh();
		}

		// Token: 0x06008128 RID: 33064 RVA: 0x0032E838 File Offset: 0x0032CA38
		public void EatOreFromStorage(HiveEatingStates.Instance smi, float dt)
		{
			GameObject gameObject = smi.storage.FindFirst(smi.def.consumedOre);
			if (!gameObject)
			{
				return;
			}
			float num = 0.25f;
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component == null)
			{
				return;
			}
			PrimaryElement component2 = component.GetComponent<PrimaryElement>();
			if (component2 == null)
			{
				return;
			}
			Diet.Info dietInfo = smi.GetSMI<BeehiveCalorieMonitor.Instance>().stomach.diet.GetDietInfo(component.PrefabTag);
			if (dietInfo == null)
			{
				return;
			}
			AmountInstance amountInstance = Db.Get().Amounts.Calories.Lookup(smi.gameObject);
			float num2 = amountInstance.GetMax() - amountInstance.value;
			float num3 = dietInfo.ConvertCaloriesToConsumptionMass(num2);
			float num4 = num * dt;
			if (num3 < num4)
			{
				num4 = num3;
			}
			num4 = Mathf.Min(num4, component2.Mass);
			component2.Mass -= num4;
			Pickupable component3 = component2.GetComponent<Pickupable>();
			if (component3.storage != null)
			{
				component3.storage.Trigger(-1452790913, smi.gameObject);
				component3.storage.Trigger(-1697596308, smi.gameObject);
			}
			float num5 = dietInfo.ConvertConsumptionMassToCalories(num4);
			CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = new CreatureCalorieMonitor.CaloriesConsumedEvent
			{
				tag = component.PrefabTag,
				calories = num5
			};
			smi.gameObject.Trigger(-2038961714, caloriesConsumedEvent);
		}

		// Token: 0x040061AC RID: 25004
		[MyCmpReq]
		public Storage storage;

		// Token: 0x040061AD RID: 25005
		[MyCmpReq]
		private RadiationEmitter emitter;
	}
}
