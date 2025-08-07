using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000799 RID: 1945
public class OilChanger : GameStateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>
{
	// Token: 0x0600336E RID: 13166 RVA: 0x00121738 File Offset: 0x0011F938
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.inoperational;
		this.root.EventHandler(GameHashes.OnStorageChange, new StateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State.Callback(OilChanger.UpdateStorageMeter));
		this.inoperational.PlayAnim("off").Enter(new StateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State.Callback(OilChanger.LED_Off)).Enter(new StateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State.Callback(OilChanger.UpdateStorageMeter))
			.TagTransition(GameTags.Operational, this.operational, false);
		this.operational.PlayAnim("on").Enter(new StateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State.Callback(OilChanger.UpdateStorageMeter)).TagTransition(GameTags.Operational, this.inoperational, true)
			.DefaultState(this.operational.oilNeeded);
		this.operational.oilNeeded.Enter(new StateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State.Callback(OilChanger.LED_Off)).ToggleStatusItem(Db.Get().BuildingStatusItems.WaitingForMaterials, null).EventTransition(GameHashes.OnStorageChange, this.operational.ready, new StateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.Transition.ConditionCallback(OilChanger.HasEnoughLubricant));
		this.operational.ready.Enter(new StateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State.Callback(OilChanger.LED_On)).ToggleChore(new Func<OilChanger.Instance, Chore>(OilChanger.CreateChore), this.operational.oilNeeded);
	}

	// Token: 0x0600336F RID: 13167 RVA: 0x00121884 File Offset: 0x0011FA84
	public static bool HasEnoughLubricant(OilChanger.Instance smi)
	{
		return smi.OilAmount >= smi.def.MIN_LUBRICANT_MASS_TO_WORK;
	}

	// Token: 0x06003370 RID: 13168 RVA: 0x0012189C File Offset: 0x0011FA9C
	private static bool IsOperational(OilChanger.Instance smi)
	{
		return smi.IsOperational;
	}

	// Token: 0x06003371 RID: 13169 RVA: 0x001218A4 File Offset: 0x0011FAA4
	public static void UpdateStorageMeter(OilChanger.Instance smi)
	{
		smi.UpdateStorageMeter();
	}

	// Token: 0x06003372 RID: 13170 RVA: 0x001218AC File Offset: 0x0011FAAC
	public static void LED_On(OilChanger.Instance smi)
	{
		smi.SetLEDState(true);
	}

	// Token: 0x06003373 RID: 13171 RVA: 0x001218B5 File Offset: 0x0011FAB5
	public static void LED_Off(OilChanger.Instance smi)
	{
		smi.SetLEDState(false);
	}

	// Token: 0x06003374 RID: 13172 RVA: 0x001218C0 File Offset: 0x0011FAC0
	private static WorkChore<OilChangerWorkableUse> CreateChore(OilChanger.Instance smi)
	{
		return new WorkChore<OilChangerWorkableUse>(Db.Get().ChoreTypes.OilChange, smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.personalNeeds, 5, false, true);
	}

	// Token: 0x04001EE6 RID: 7910
	public const string STORAGE_METER_TARGET_NAME = "meter_target";

	// Token: 0x04001EE7 RID: 7911
	public const string STORAGE_METER_ANIM_NAME = "meter";

	// Token: 0x04001EE8 RID: 7912
	public const string LED_METER_TARGET_NAME = "light_target";

	// Token: 0x04001EE9 RID: 7913
	public const string LED_METER_ANIM_ON_NAME = "light_on";

	// Token: 0x04001EEA RID: 7914
	public const string LED_METER_ANIM_OFF_NAME = "light_off";

	// Token: 0x04001EEB RID: 7915
	public GameStateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State inoperational;

	// Token: 0x04001EEC RID: 7916
	public OilChanger.OperationalStates operational;

	// Token: 0x020016A2 RID: 5794
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007378 RID: 29560
		public float MIN_LUBRICANT_MASS_TO_WORK = 200f;
	}

	// Token: 0x020016A3 RID: 5795
	public class OperationalStates : GameStateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State
	{
		// Token: 0x04007379 RID: 29561
		public GameStateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State oilNeeded;

		// Token: 0x0400737A RID: 29562
		public GameStateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.State ready;
	}

	// Token: 0x020016A4 RID: 5796
	public new class Instance : GameStateMachine<OilChanger, OilChanger.Instance, IStateMachineTarget, OilChanger.Def>.GameInstance, IFetchList
	{
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06009612 RID: 38418 RVA: 0x00376C6F File Offset: 0x00374E6F
		public bool IsOperational
		{
			get
			{
				return this.operational.IsOperational;
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06009613 RID: 38419 RVA: 0x00376C7C File Offset: 0x00374E7C
		public float OilAmount
		{
			get
			{
				return this.storage.GetMassAvailable(GameTags.LubricatingOil);
			}
		}

		// Token: 0x06009614 RID: 38420 RVA: 0x00376C90 File Offset: 0x00374E90
		public Instance(IStateMachineTarget master, OilChanger.Def def)
		{
			Dictionary<Tag, float> dictionary = new Dictionary<Tag, float>();
			Tag lubricatingOil = GameTags.LubricatingOil;
			dictionary[lubricatingOil] = 0f;
			this.remainingLubricationMass = dictionary;
			base..ctor(master, def);
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			this.storage = base.GetComponent<Storage>();
			this.operational = base.GetComponent<Operational>();
			this.oilStorageMeter = new MeterController(component, "meter_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingFront, Array.Empty<string>());
			this.readyLightMeter = new MeterController(component, "light_target", "light_off", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingFront, Array.Empty<string>());
		}

		// Token: 0x06009615 RID: 38421 RVA: 0x00376D20 File Offset: 0x00374F20
		public void SetLEDState(bool isOn)
		{
			string text = (isOn ? "light_on" : "light_off");
			this.readyLightMeter.meterController.Play(text, KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x06009616 RID: 38422 RVA: 0x00376D60 File Offset: 0x00374F60
		public void UpdateStorageMeter()
		{
			float num = this.OilAmount / this.storage.capacityKg;
			this.oilStorageMeter.SetPositionPercent(num);
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06009617 RID: 38423 RVA: 0x00376D8C File Offset: 0x00374F8C
		public Storage Destination
		{
			get
			{
				return this.storage;
			}
		}

		// Token: 0x06009618 RID: 38424 RVA: 0x00376D94 File Offset: 0x00374F94
		public float GetMinimumAmount(Tag tag)
		{
			return base.def.MIN_LUBRICANT_MASS_TO_WORK;
		}

		// Token: 0x06009619 RID: 38425 RVA: 0x00376DA1 File Offset: 0x00374FA1
		public Dictionary<Tag, float> GetRemaining()
		{
			this.remainingLubricationMass[GameTags.LubricatingOil] = Mathf.Clamp(base.def.MIN_LUBRICANT_MASS_TO_WORK - this.OilAmount, 0f, base.def.MIN_LUBRICANT_MASS_TO_WORK);
			return this.remainingLubricationMass;
		}

		// Token: 0x0600961A RID: 38426 RVA: 0x00376DE0 File Offset: 0x00374FE0
		public Dictionary<Tag, float> GetRemainingMinimum()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400737B RID: 29563
		private Storage storage;

		// Token: 0x0400737C RID: 29564
		private Operational operational;

		// Token: 0x0400737D RID: 29565
		private MeterController oilStorageMeter;

		// Token: 0x0400737E RID: 29566
		private MeterController readyLightMeter;

		// Token: 0x0400737F RID: 29567
		private Dictionary<Tag, float> remainingLubricationMass;
	}
}
