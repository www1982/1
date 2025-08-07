using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200074C RID: 1868
[SerializationConfig(MemberSerialization.OptIn)]
public class IceCooledFan : StateMachineComponent<IceCooledFan.StatesInstance>
{
	// Token: 0x06002F72 RID: 12146 RVA: 0x0010FB62 File Offset: 0x0010DD62
	public bool HasMaterial()
	{
		this.UpdateMeter();
		return this.iceStorage.MassStored() > 0f;
	}

	// Token: 0x06002F73 RID: 12147 RVA: 0x0010FB7C File Offset: 0x0010DD7C
	public void CheckWorking()
	{
		if (base.smi.master.workable.worker == null)
		{
			base.smi.GoTo(base.smi.sm.unworkable);
		}
	}

	// Token: 0x06002F74 RID: 12148 RVA: 0x0010FBB8 File Offset: 0x0010DDB8
	private void UpdateUnworkableStatusItems()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (!base.smi.EnvironmentNeedsCooling())
		{
			if (!component.HasStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther))
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther, this.minCooledTemperature);
			}
		}
		else if (component.HasStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther))
		{
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.CannotCoolFurther, false);
		}
		if (!base.smi.EnvironmentHighEnoughPressure())
		{
			if (!component.HasStatusItem(Db.Get().BuildingStatusItems.UnderPressure))
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.UnderPressure, this.minEnvironmentMass);
				return;
			}
		}
		else if (component.HasStatusItem(Db.Get().BuildingStatusItems.UnderPressure))
		{
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.UnderPressure, false);
		}
	}

	// Token: 0x06002F75 RID: 12149 RVA: 0x0010FCB8 File Offset: 0x0010DEB8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_waterbody", "meter_waterlevel" });
		base.smi.StartSM();
		base.GetComponent<ManualDeliveryKG>().SetStorage(this.iceStorage);
	}

	// Token: 0x06002F76 RID: 12150 RVA: 0x0010FD24 File Offset: 0x0010DF24
	private void UpdateMeter()
	{
		float num = 0f;
		foreach (GameObject gameObject in this.iceStorage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			num += component.Temperature;
		}
		num /= (float)this.iceStorage.items.Count;
		float num2 = Mathf.Clamp01((num - this.LOW_ICE_TEMP) / (this.targetTemperature - this.LOW_ICE_TEMP));
		this.meter.SetPositionPercent(1f - num2);
	}

	// Token: 0x06002F77 RID: 12151 RVA: 0x0010FDCC File Offset: 0x0010DFCC
	private void DoCooling(float dt)
	{
		float num = this.coolingRate * dt;
		foreach (GameObject gameObject in this.iceStorage.items)
		{
			GameUtil.DeltaThermalEnergy(gameObject.GetComponent<PrimaryElement>(), num, this.targetTemperature);
		}
		for (int i = this.iceStorage.items.Count; i > 0; i--)
		{
			GameObject gameObject2 = this.iceStorage.items[i - 1];
			if (gameObject2 != null && gameObject2.GetComponent<PrimaryElement>().Temperature > gameObject2.GetComponent<PrimaryElement>().Element.highTemp && gameObject2.GetComponent<PrimaryElement>().Element.HasTransitionUp)
			{
				PrimaryElement component = gameObject2.GetComponent<PrimaryElement>();
				this.iceStorage.AddLiquid(component.Element.highTempTransitionTarget, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, false, true);
				this.iceStorage.ConsumeIgnoringDisease(gameObject2);
			}
		}
		for (int j = this.iceStorage.items.Count; j > 0; j--)
		{
			GameObject gameObject3 = this.iceStorage.items[j - 1];
			if (gameObject3 != null && gameObject3.GetComponent<PrimaryElement>().Temperature >= this.targetTemperature)
			{
				this.iceStorage.Transfer(gameObject3, this.liquidStorage, true, true);
			}
		}
		if (!this.liquidStorage.IsEmpty())
		{
			this.liquidStorage.DropAll(false, false, new Vector3(1f, 0f, 0f), true, null);
		}
		this.UpdateMeter();
	}

	// Token: 0x04001C26 RID: 7206
	[SerializeField]
	public float minCooledTemperature;

	// Token: 0x04001C27 RID: 7207
	[SerializeField]
	public float minEnvironmentMass;

	// Token: 0x04001C28 RID: 7208
	[SerializeField]
	public float coolingRate;

	// Token: 0x04001C29 RID: 7209
	[SerializeField]
	public float targetTemperature;

	// Token: 0x04001C2A RID: 7210
	[SerializeField]
	public Vector2I minCoolingRange;

	// Token: 0x04001C2B RID: 7211
	[SerializeField]
	public Vector2I maxCoolingRange;

	// Token: 0x04001C2C RID: 7212
	[SerializeField]
	public Storage iceStorage;

	// Token: 0x04001C2D RID: 7213
	[SerializeField]
	public Storage liquidStorage;

	// Token: 0x04001C2E RID: 7214
	[SerializeField]
	public Tag consumptionTag;

	// Token: 0x04001C2F RID: 7215
	[MyCmpAdd]
	private ManuallySetRemoteWorkTargetComponent remoteChore;

	// Token: 0x04001C30 RID: 7216
	private float LOW_ICE_TEMP = 173.15f;

	// Token: 0x04001C31 RID: 7217
	[MyCmpAdd]
	private IceCooledFanWorkable workable;

	// Token: 0x04001C32 RID: 7218
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001C33 RID: 7219
	private MeterController meter;

	// Token: 0x0200161E RID: 5662
	public class StatesInstance : GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.GameInstance
	{
		// Token: 0x060093F4 RID: 37876 RVA: 0x0036DC91 File Offset: 0x0036BE91
		public StatesInstance(IceCooledFan smi)
			: base(smi)
		{
		}

		// Token: 0x060093F5 RID: 37877 RVA: 0x0036DC9C File Offset: 0x0036BE9C
		public bool IsWorkable()
		{
			bool flag = false;
			if (base.master.operational.IsOperational && this.EnvironmentNeedsCooling() && base.smi.master.HasMaterial() && base.smi.EnvironmentHighEnoughPressure())
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060093F6 RID: 37878 RVA: 0x0036DCE8 File Offset: 0x0036BEE8
		public bool EnvironmentNeedsCooling()
		{
			bool flag = false;
			int num = Grid.PosToCell(base.transform.GetPosition());
			for (int i = base.master.minCoolingRange.y; i < base.master.maxCoolingRange.y; i++)
			{
				for (int j = base.master.minCoolingRange.x; j < base.master.maxCoolingRange.x; j++)
				{
					CellOffset cellOffset = new CellOffset(j, i);
					int num2 = Grid.OffsetCell(num, cellOffset);
					if (Grid.Temperature[num2] > base.master.minCooledTemperature)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x060093F7 RID: 37879 RVA: 0x0036DD90 File Offset: 0x0036BF90
		public bool EnvironmentHighEnoughPressure()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			for (int i = base.master.minCoolingRange.y; i < base.master.maxCoolingRange.y; i++)
			{
				for (int j = base.master.minCoolingRange.x; j < base.master.maxCoolingRange.x; j++)
				{
					CellOffset cellOffset = new CellOffset(j, i);
					int num2 = Grid.OffsetCell(num, cellOffset);
					if (Grid.Mass[num2] >= base.master.minEnvironmentMass)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x0200161F RID: 5663
	public class States : GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan>
	{
		// Token: 0x060093F8 RID: 37880 RVA: 0x0036DE30 File Offset: 0x0036C030
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unworkable;
			this.root.Enter(delegate(IceCooledFan.StatesInstance smi)
			{
				smi.master.workable.SetWorkTime(float.PositiveInfinity);
			});
			this.workable.ToggleChore(new Func<IceCooledFan.StatesInstance, Chore>(IceCooledFan.States.CreateUseChore), new Action<IceCooledFan.StatesInstance, Chore>(IceCooledFan.States.SetRemoteChore), this.work_pst).EventTransition(GameHashes.ActiveChanged, this.workable.cooling, (IceCooledFan.StatesInstance smi) => smi.master.workable.worker != null).EventTransition(GameHashes.OperationalChanged, this.workable.cooling, (IceCooledFan.StatesInstance smi) => smi.master.workable.worker != null)
				.Transition(this.unworkable, (IceCooledFan.StatesInstance smi) => !smi.IsWorkable(), UpdateRate.SIM_200ms);
			this.workable.cooling.EventTransition(GameHashes.OperationalChanged, this.unworkable, (IceCooledFan.StatesInstance smi) => smi.master.workable.worker == null).EventHandler(GameHashes.ActiveChanged, delegate(IceCooledFan.StatesInstance smi)
			{
				smi.master.CheckWorking();
			}).Enter(delegate(IceCooledFan.StatesInstance smi)
			{
				smi.master.gameObject.GetComponent<ManualDeliveryKG>().Pause(true, "Working");
				if (!smi.EnvironmentNeedsCooling() || !smi.master.HasMaterial() || !smi.EnvironmentHighEnoughPressure())
				{
					smi.GoTo(this.unworkable);
				}
			})
				.Update("IceCooledFanCooling", delegate(IceCooledFan.StatesInstance smi, float dt)
				{
					smi.master.DoCooling(dt);
				}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(IceCooledFan.StatesInstance smi)
				{
					if (!smi.master.HasMaterial())
					{
						smi.master.gameObject.GetComponent<ManualDeliveryKG>().Pause(false, "Working");
					}
					smi.master.liquidStorage.DropAll(false, false, default(Vector3), true, null);
				});
			this.work_pst.ScheduleGoTo(2f, this.unworkable);
			this.unworkable.Update("IceFanUnworkableStatusItems", delegate(IceCooledFan.StatesInstance smi, float dt)
			{
				smi.master.UpdateUnworkableStatusItems();
			}, UpdateRate.SIM_200ms, false).Transition(this.workable.waiting, (IceCooledFan.StatesInstance smi) => smi.IsWorkable(), UpdateRate.SIM_200ms).Enter(delegate(IceCooledFan.StatesInstance smi)
			{
				smi.master.UpdateUnworkableStatusItems();
			})
				.Exit(delegate(IceCooledFan.StatesInstance smi)
				{
					smi.master.UpdateUnworkableStatusItems();
				});
		}

		// Token: 0x060093F9 RID: 37881 RVA: 0x0036E0B2 File Offset: 0x0036C2B2
		private static void SetRemoteChore(IceCooledFan.StatesInstance smi, Chore chore)
		{
			smi.master.remoteChore.SetChore(chore);
		}

		// Token: 0x060093FA RID: 37882 RVA: 0x0036E0C8 File Offset: 0x0036C2C8
		private static Chore CreateUseChore(IceCooledFan.StatesInstance smi)
		{
			return new WorkChore<IceCooledFanWorkable>(Db.Get().ChoreTypes.IceCooledFan, smi.master.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x040071E8 RID: 29160
		public IceCooledFan.States.Workable workable;

		// Token: 0x040071E9 RID: 29161
		public GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State unworkable;

		// Token: 0x040071EA RID: 29162
		public GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State work_pst;

		// Token: 0x0200279A RID: 10138
		public class Workable : GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State
		{
			// Token: 0x0400AF59 RID: 44889
			public GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State waiting;

			// Token: 0x0400AF5A RID: 44890
			public GameStateMachine<IceCooledFan.States, IceCooledFan.StatesInstance, IceCooledFan, object>.State cooling;
		}
	}
}
