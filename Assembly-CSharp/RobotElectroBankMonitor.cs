using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000AD6 RID: 2774
public class RobotElectroBankMonitor : GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>
{
	// Token: 0x060050B0 RID: 20656 RVA: 0x001D44E8 File Offset: 0x001D26E8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.powered;
		this.root.Enter(delegate(RobotElectroBankMonitor.Instance smi)
		{
			smi.ElectroBankStorageChange(null);
		}).TagTransition(GameTags.Dead, this.deceased, false).TagTransition(GameTags.Creatures.Die, this.deceased, false);
		this.powered.DefaultState(this.powered.highBattery).ParamTransition<bool>(this.hasElectrobank, this.powerdown, GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.IsFalse).Update(delegate(RobotElectroBankMonitor.Instance smi, float dt)
		{
			RobotElectroBankMonitor.ConsumePower(smi, dt);
		}, UpdateRate.SIM_200ms, false);
		this.powered.highBattery.Transition(this.powered.lowBattery, GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.Not(new StateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.Transition.ConditionCallback(RobotElectroBankMonitor.ChargeDecent)), UpdateRate.SIM_200ms).Enter(delegate(RobotElectroBankMonitor.Instance smi)
		{
			this.UpdateBatteryMeter(smi, RobotElectroBankMonitor.BATTER_FULL_SYMBOL);
		});
		this.powered.lowBattery.Enter(delegate(RobotElectroBankMonitor.Instance smi)
		{
			RobotElectroBankMonitor.RequestBattery(smi);
			this.UpdateBatteryMeter(smi, RobotElectroBankMonitor.BATTER_LOW_SYMBOL);
		}).Transition(this.powered.highBattery, new StateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.Transition.ConditionCallback(RobotElectroBankMonitor.ChargeDecent), UpdateRate.SIM_200ms).ToggleStatusItem((RobotElectroBankMonitor.Instance smi) => Db.Get().RobotStatusItems.LowBatteryNoCharge, null);
		this.powerdown.Enter(delegate(RobotElectroBankMonitor.Instance smi)
		{
			RobotElectroBankMonitor.RequestBattery(smi);
		}).ToggleBehaviour(GameTags.Robots.Behaviours.NoElectroBank, (RobotElectroBankMonitor.Instance smi) => true, delegate(RobotElectroBankMonitor.Instance smi)
		{
			smi.GoTo(this.powered);
		});
		this.deceased.DoNothing();
	}

	// Token: 0x060050B1 RID: 20657 RVA: 0x001D46A7 File Offset: 0x001D28A7
	private void UpdateBatteryMeter(RobotElectroBankMonitor.Instance smi, HashedString symbol)
	{
		smi.UpdateBatteryState(symbol);
	}

	// Token: 0x060050B2 RID: 20658 RVA: 0x001D46B0 File Offset: 0x001D28B0
	public static bool ChargeDecent(RobotElectroBankMonitor.Instance smi)
	{
		float num = 0f;
		foreach (GameObject gameObject in smi.electroBankStorage.items)
		{
			if (!(gameObject == null))
			{
				num += gameObject.GetComponent<Electrobank>().Charge;
			}
		}
		return num >= smi.def.lowBatteryWarningPercent * 120000f;
	}

	// Token: 0x060050B3 RID: 20659 RVA: 0x001D4738 File Offset: 0x001D2938
	public static void ConsumePower(RobotElectroBankMonitor.Instance smi, float dt)
	{
		if (smi.electrobank == null)
		{
			RobotElectroBankMonitor.RequestBattery(smi);
			return;
		}
		float num = Mathf.Min(dt * Mathf.Abs(smi.bankAmount.GetDelta()), smi.electrobank.Charge);
		smi.electrobank.RemovePower(num, true);
		if (smi.electrobank != null)
		{
			smi.bankAmount.value = smi.electrobank.Charge;
		}
	}

	// Token: 0x060050B4 RID: 20660 RVA: 0x001D47AF File Offset: 0x001D29AF
	public static void RequestBattery(RobotElectroBankMonitor.Instance smi)
	{
		if (smi.fetchBatteryChore.IsPaused)
		{
			smi.fetchBatteryChore.Pause(smi.electrobank != null && RobotElectroBankMonitor.ChargeDecent(smi), "FlydoBattery");
		}
	}

	// Token: 0x0400363F RID: 13887
	public static readonly HashedString BATTER_SYMBOL = "meter_target";

	// Token: 0x04003640 RID: 13888
	public static readonly HashedString BATTER_FULL_SYMBOL = "battery_full";

	// Token: 0x04003641 RID: 13889
	public static readonly HashedString BATTER_LOW_SYMBOL = "battery_low";

	// Token: 0x04003642 RID: 13890
	public static readonly HashedString BATTER_DEAD_SYMBOL = "battery_dead";

	// Token: 0x04003643 RID: 13891
	public RobotElectroBankMonitor.PoweredState powered;

	// Token: 0x04003644 RID: 13892
	public GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.State deceased;

	// Token: 0x04003645 RID: 13893
	public GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.State powerdown;

	// Token: 0x04003646 RID: 13894
	public StateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.BoolParameter hasElectrobank;

	// Token: 0x02001BB4 RID: 7092
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040083C4 RID: 33732
		public float lowBatteryWarningPercent;
	}

	// Token: 0x02001BB5 RID: 7093
	public class PoweredState : GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.State
	{
		// Token: 0x040083C5 RID: 33733
		public GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.State highBattery;

		// Token: 0x040083C6 RID: 33734
		public GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.State lowBattery;
	}

	// Token: 0x02001BB6 RID: 7094
	public new class Instance : GameStateMachine<RobotElectroBankMonitor, RobotElectroBankMonitor.Instance, IStateMachineTarget, RobotElectroBankMonitor.Def>.GameInstance
	{
		// Token: 0x0600A857 RID: 43095 RVA: 0x003B4134 File Offset: 0x003B2334
		public Instance(IStateMachineTarget master, RobotElectroBankMonitor.Def def)
			: base(master, def)
		{
			this.fetchBatteryChore = base.GetComponent<ManualDeliveryKG>();
			foreach (Storage storage in master.gameObject.GetComponents<Storage>())
			{
				if (storage.storageID == GameTags.ChargedPortableBattery)
				{
					this.electroBankStorage = storage;
					break;
				}
			}
			foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.ChargedPortableBattery))
			{
				KPrefabID component = gameObject.GetComponent<KPrefabID>();
				this.batteryTags.Add(component.PrefabTag);
			}
			this.bankAmount = Db.Get().Amounts.InternalElectroBank.Lookup(master.gameObject);
			this.electroBankStorage.Subscribe(-1697596308, new Action<object>(this.ElectroBankStorageChange));
			this.ElectroBankStorageChange(null);
			TreeFilterable component2 = base.GetComponent<TreeFilterable>();
			component2.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Combine(component2.OnFilterChanged, new Action<HashSet<Tag>>(this.OnFilterChanged));
		}

		// Token: 0x0600A858 RID: 43096 RVA: 0x003B4260 File Offset: 0x003B2460
		public void ElectroBankStorageChange(object data = null)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject != null)
			{
				Pickupable component = gameObject.GetComponent<Pickupable>();
				if (component.storage != null && component.storage.storageID == GameTags.ChargedPortableBattery)
				{
					if (this.electroBankStorage.Count > 0 && this.electroBankStorage.items[0] != null)
					{
						this.electrobank = this.electroBankStorage.items[0].GetComponent<Electrobank>();
						this.bankAmount.value = this.electrobank.Charge;
					}
					else
					{
						this.electrobank = null;
					}
				}
				else if (this.electroBankStorage.Count <= 0)
				{
					this.electrobank = null;
					this.bankAmount.value = 0f;
					this.DropDischargedElectroBank(gameObject);
				}
				this.fetchBatteryChore.Pause(this.electrobank != null && RobotElectroBankMonitor.ChargeDecent(this), "Robot has sufficienct electrobank");
				base.sm.hasElectrobank.Set(this.electrobank != null, this, false);
				return;
			}
			if (this.electrobank == null)
			{
				if (this.electroBankStorage.Count > 0 && this.electroBankStorage.items[0] != null)
				{
					this.electrobank = this.electroBankStorage.items[0].GetComponent<Electrobank>();
					this.bankAmount.value = this.electrobank.Charge;
				}
				else
				{
					this.electrobank = null;
					this.bankAmount.value = 0f;
				}
				this.fetchBatteryChore.Pause(this.electrobank != null && RobotElectroBankMonitor.ChargeDecent(this), "Robot has sufficienct electrobank");
				base.sm.hasElectrobank.Set(this.electrobank != null, this, false);
			}
		}

		// Token: 0x0600A859 RID: 43097 RVA: 0x003B444C File Offset: 0x003B264C
		private void DropDischargedElectroBank(GameObject go)
		{
			Electrobank component = go.GetComponent<Electrobank>();
			if (component != null && component.HasTag(GameTags.ChargedPortableBattery) && !component.IsFullyCharged)
			{
				component.RemovePower(component.Charge, true);
			}
		}

		// Token: 0x0600A85A RID: 43098 RVA: 0x003B448C File Offset: 0x003B268C
		public void UpdateBatteryState(HashedString newState)
		{
			if (this.currentSymbolSwap.IsValid)
			{
				this.symbolOverrideController.RemoveSymbolOverride(this.currentSymbolSwap, 0);
			}
			KAnim.Build.Symbol symbol = this.animController.AnimFiles[0].GetData().build.GetSymbol(newState);
			this.symbolOverrideController.AddSymbolOverride(RobotElectroBankMonitor.BATTER_SYMBOL, symbol, 0);
			this.currentSymbolSwap = newState;
		}

		// Token: 0x0600A85B RID: 43099 RVA: 0x003B44F8 File Offset: 0x003B26F8
		private void OnFilterChanged(HashSet<Tag> allowed_tags)
		{
			if (this.fetchBatteryChore != null)
			{
				List<Tag> list = new List<Tag>();
				foreach (Tag tag in this.batteryTags)
				{
					if (!allowed_tags.Contains(tag))
					{
						list.Add(tag);
					}
				}
				this.fetchBatteryChore.ForbiddenTags = list.ToArray();
			}
		}

		// Token: 0x040083C7 RID: 33735
		public Storage electroBankStorage;

		// Token: 0x040083C8 RID: 33736
		public Electrobank electrobank;

		// Token: 0x040083C9 RID: 33737
		public ManualDeliveryKG fetchBatteryChore;

		// Token: 0x040083CA RID: 33738
		public AmountInstance bankAmount;

		// Token: 0x040083CB RID: 33739
		[MyCmpReq]
		private SymbolOverrideController symbolOverrideController;

		// Token: 0x040083CC RID: 33740
		[MyCmpReq]
		private KBatchedAnimController animController;

		// Token: 0x040083CD RID: 33741
		private HashedString currentSymbolSwap;

		// Token: 0x040083CE RID: 33742
		private HashSet<Tag> batteryTags = new HashSet<Tag>();
	}
}
