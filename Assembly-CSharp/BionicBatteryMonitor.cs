using System;
using System.Collections.Generic;
using Klei.AI;
using Klei.CustomSettings;
using STRINGS;
using UnityEngine;

// Token: 0x020009D0 RID: 2512
public class BionicBatteryMonitor : GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>
{
	// Token: 0x06004976 RID: 18806 RVA: 0x001A99DC File Offset: 0x001A7BDC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.firstSpawn;
		this.firstSpawn.ParamTransition<bool>(this.InitialElectrobanksSpawned, this.online, GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.IsTrue).Enter(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.SpawnAndInstallInitialElectrobanks));
		this.online.TriggerOnEnter(GameHashes.BionicOnline, null).Transition(this.offline, new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Transition.ConditionCallback(BionicBatteryMonitor.DoesNotHaveCharge), UpdateRate.SIM_200ms).Enter(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.ReorganizeElectrobankStorage))
			.Update(new Action<BionicBatteryMonitor.Instance, float>(BionicBatteryMonitor.DischargeUpdate), UpdateRate.SIM_200ms, false)
			.DefaultState(this.online.idle);
		this.online.idle.ParamTransition<int>(this.ChargedElectrobankCount, this.online.critical, GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.IsLTEOne_Int).OnSignal(this.OnElectrobankStorageChanged, this.online.upkeep, new Func<BionicBatteryMonitor.Instance, bool>(BionicBatteryMonitor.WantsToUpkeep)).EventTransition(GameHashes.ScheduleBlocksChanged, this.online.upkeep, new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Transition.ConditionCallback(BionicBatteryMonitor.WantsToUpkeep))
			.EventTransition(GameHashes.ScheduleChanged, this.online.upkeep, new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Transition.ConditionCallback(BionicBatteryMonitor.WantsToUpkeep))
			.EventTransition(GameHashes.ScheduleBlocksTick, this.online.upkeep, new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Transition.ConditionCallback(BionicBatteryMonitor.WantsToUpkeep));
		this.online.upkeep.ParamTransition<int>(this.ChargedElectrobankCount, this.online.critical, GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.IsLTEOne_Int).EventTransition(GameHashes.ScheduleBlocksChanged, this.online.idle, GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Not(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Transition.ConditionCallback(BionicBatteryMonitor.WantsToUpkeep))).EventTransition(GameHashes.ScheduleChanged, this.online.idle, GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Not(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Transition.ConditionCallback(BionicBatteryMonitor.WantsToUpkeep)))
			.EventTransition(GameHashes.ScheduleBlocksTick, this.online.idle, GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Not(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Transition.ConditionCallback(BionicBatteryMonitor.WantsToUpkeep)))
			.OnSignal(this.OnElectrobankStorageChanged, this.online.idle, (BionicBatteryMonitor.Instance smi) => !BionicBatteryMonitor.WantsToUpkeep(smi))
			.DefaultState(this.online.upkeep.seekElectrobank);
		this.online.upkeep.seekElectrobank.ToggleUrge(Db.Get().Urges.ReloadElectrobank).ToggleChore((BionicBatteryMonitor.Instance smi) => new ReloadElectrobankChore(smi.master), this.online.idle);
		this.online.critical.DefaultState(this.online.critical.seekElectrobank).ParamTransition<int>(this.ChargedElectrobankCount, this.online.idle, GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.IsGTOne_Int).DoTutorial(Tutorial.TutorialMessages.TM_BionicBattery);
		this.online.critical.seekElectrobank.ToggleUrge(Db.Get().Urges.ReloadElectrobank).ToggleRecurringChore((BionicBatteryMonitor.Instance smi) => new ReloadElectrobankChore(smi.master), null);
		this.offline.DefaultState(this.offline.waitingForBatteryDelivery).ToggleTag(GameTags.Incapacitated).ToggleRecurringChore((BionicBatteryMonitor.Instance smi) => new BeOfflineChore(smi.master), null)
			.ToggleUrge(Db.Get().Urges.BeOffline)
			.Enter(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.SetOffline))
			.Enter(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.DropAllDischargedElectrobanks))
			.TriggerOnEnter(GameHashes.BionicOffline, null);
		this.offline.waitingForBatteryDelivery.ParamTransition<int>(this.ChargedElectrobankCount, this.offline.waitingForBatteryInstallation, GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.IsGTZero_Int).Toggle("Enable Delivery of new Electrobanks", new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.EnableManualDelivery), new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.DisableManualDelivery)).Toggle("Enable User Prioritization", new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.EnablePrioritizationComponent), new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.DisablePrioritizationComponent));
		this.offline.waitingForBatteryInstallation.Toggle("Enable User Prioritization", new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.EnablePrioritizationComponent), new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.DisablePrioritizationComponent)).Enter(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.StartReanimateWorkChore)).Exit(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.CancelReanimateWorkChore))
			.WorkableCompleteTransition(new Func<BionicBatteryMonitor.Instance, Workable>(BionicBatteryMonitor.GetReanimateWorkable), this.offline.reboot)
			.DefaultState(this.offline.waitingForBatteryInstallation.waiting);
		this.offline.waitingForBatteryInstallation.waiting.ToggleStatusItem(Db.Get().DuplicantStatusItems.BionicWaitingForReboot, null).WorkableStartTransition(new Func<BionicBatteryMonitor.Instance, Workable>(BionicBatteryMonitor.GetReanimateWorkable), this.offline.waitingForBatteryInstallation.working);
		this.offline.waitingForBatteryInstallation.working.WorkableStopTransition(new Func<BionicBatteryMonitor.Instance, Workable>(BionicBatteryMonitor.GetReanimateWorkable), this.offline.waitingForBatteryInstallation.waiting);
		this.offline.reboot.PlayAnim("power_up").OnAnimQueueComplete(this.online).ScheduleGoTo(10f, this.online)
			.Exit(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.AutomaticallyDropAllDepletedElectrobanks))
			.Exit(new StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State.Callback(BionicBatteryMonitor.SetOnline));
	}

	// Token: 0x06004977 RID: 18807 RVA: 0x001A9F33 File Offset: 0x001A8133
	public static ReanimateBionicWorkable GetReanimateWorkable(BionicBatteryMonitor.Instance smi)
	{
		return smi.reanimateWorkable;
	}

	// Token: 0x06004978 RID: 18808 RVA: 0x001A9F3B File Offset: 0x001A813B
	public static bool DoesNotHaveCharge(BionicBatteryMonitor.Instance smi)
	{
		return smi.CurrentCharge <= 0f;
	}

	// Token: 0x06004979 RID: 18809 RVA: 0x001A9F4D File Offset: 0x001A814D
	public static bool IsCriticallyLow(BionicBatteryMonitor.Instance smi)
	{
		return smi.ChargedElectrobankCount <= 1;
	}

	// Token: 0x0600497A RID: 18810 RVA: 0x001A9F5B File Offset: 0x001A815B
	public static bool ChargeIsBelowNotificationThreshold(BionicBatteryMonitor.Instance smi)
	{
		return smi.CurrentCharge <= 30000f;
	}

	// Token: 0x0600497B RID: 18811 RVA: 0x001A9F6D File Offset: 0x001A816D
	public static bool IsAnyElectrobankAvailableToBeFetched(BionicBatteryMonitor.Instance smi)
	{
		return smi.GetClosestElectrobank() != null;
	}

	// Token: 0x0600497C RID: 18812 RVA: 0x001A9F7B File Offset: 0x001A817B
	public static bool WantsToInstallNewBattery(BionicBatteryMonitor.Instance smi)
	{
		return BionicBatteryMonitor.IsCriticallyLow(smi) || (smi.InUpkeepTime && smi.ChargedElectrobankCount < smi.ElectrobankCountCapacity);
	}

	// Token: 0x0600497D RID: 18813 RVA: 0x001A9F9F File Offset: 0x001A819F
	public static bool WantsToUpkeep(BionicBatteryMonitor.Instance smi)
	{
		return BionicBatteryMonitor.WantsToInstallNewBattery(smi);
	}

	// Token: 0x0600497E RID: 18814 RVA: 0x001A9FA7 File Offset: 0x001A81A7
	public static void SpawnAndInstallInitialElectrobanks(BionicBatteryMonitor.Instance smi)
	{
		smi.SpawnAndInstallInitialElectrobanks();
	}

	// Token: 0x0600497F RID: 18815 RVA: 0x001A9FAF File Offset: 0x001A81AF
	public static void RefreshCharge(BionicBatteryMonitor.Instance smi)
	{
		smi.RefreshCharge();
	}

	// Token: 0x06004980 RID: 18816 RVA: 0x001A9FB7 File Offset: 0x001A81B7
	public static void EnableManualDelivery(BionicBatteryMonitor.Instance smi)
	{
		smi.SetManualDeliveryEnableState(true);
	}

	// Token: 0x06004981 RID: 18817 RVA: 0x001A9FC0 File Offset: 0x001A81C0
	public static void DisableManualDelivery(BionicBatteryMonitor.Instance smi)
	{
		smi.SetManualDeliveryEnableState(false);
	}

	// Token: 0x06004982 RID: 18818 RVA: 0x001A9FC9 File Offset: 0x001A81C9
	public static void StartReanimateWorkChore(BionicBatteryMonitor.Instance smi)
	{
		smi.CreateWorkableChore();
	}

	// Token: 0x06004983 RID: 18819 RVA: 0x001A9FD1 File Offset: 0x001A81D1
	public static void CancelReanimateWorkChore(BionicBatteryMonitor.Instance smi)
	{
		smi.CancelWorkChore();
	}

	// Token: 0x06004984 RID: 18820 RVA: 0x001A9FD9 File Offset: 0x001A81D9
	public static void SetOffline(BionicBatteryMonitor.Instance smi)
	{
		smi.SetOnlineState(false);
	}

	// Token: 0x06004985 RID: 18821 RVA: 0x001A9FE2 File Offset: 0x001A81E2
	public static void SetOnline(BionicBatteryMonitor.Instance smi)
	{
		smi.SetOnlineState(true);
	}

	// Token: 0x06004986 RID: 18822 RVA: 0x001A9FEB File Offset: 0x001A81EB
	public static void AutomaticallyDropAllDepletedElectrobanks(BionicBatteryMonitor.Instance smi)
	{
		smi.AutomaticallyDropAllDepletedElectrobanks();
	}

	// Token: 0x06004987 RID: 18823 RVA: 0x001A9FF3 File Offset: 0x001A81F3
	public static void ReorganizeElectrobankStorage(BionicBatteryMonitor.Instance smi)
	{
		smi.ReorganizeElectrobanks();
	}

	// Token: 0x06004988 RID: 18824 RVA: 0x001A9FFB File Offset: 0x001A81FB
	public static void DropAllDischargedElectrobanks(BionicBatteryMonitor.Instance smi)
	{
		smi.DropAllDischargedElectrobanks();
	}

	// Token: 0x06004989 RID: 18825 RVA: 0x001AA003 File Offset: 0x001A8203
	public static void EnablePrioritizationComponent(BionicBatteryMonitor.Instance smi)
	{
		Prioritizable.AddRef(smi.gameObject);
		smi.gameObject.Trigger(1980521255, null);
	}

	// Token: 0x0600498A RID: 18826 RVA: 0x001AA021 File Offset: 0x001A8221
	public static void DisablePrioritizationComponent(BionicBatteryMonitor.Instance smi)
	{
		Prioritizable.RemoveRef(smi.gameObject);
		smi.gameObject.Trigger(1980521255, null);
	}

	// Token: 0x0600498B RID: 18827 RVA: 0x001AA040 File Offset: 0x001A8240
	public static void DischargeUpdate(BionicBatteryMonitor.Instance smi, float dt)
	{
		float num = Mathf.Min(dt * smi.Wattage, smi.CurrentCharge);
		smi.ConsumePower(num);
	}

	// Token: 0x0600498C RID: 18828 RVA: 0x001AA068 File Offset: 0x001A8268
	private static BionicBatteryMonitor.WattageModifier MakeDifficultyModifier(string id, string desc, float watts)
	{
		return new BionicBatteryMonitor.WattageModifier(id, desc + ": <b>" + ((watts >= 0f) ? "+</b>" : "-</b>") + GameUtil.GetFormattedWattage(Mathf.Abs(watts), GameUtil.WattageFormatterUnit.Automatic, true), watts, watts);
	}

	// Token: 0x0600498D RID: 18829 RVA: 0x001AA0A0 File Offset: 0x001A82A0
	public static BionicBatteryMonitor.WattageModifier GetDifficultyModifier()
	{
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.BionicWattage);
		BionicBatteryMonitor.WattageModifier wattageModifier;
		if (BionicBatteryMonitor.difficultyWattages.TryGetValue(currentQualitySetting.id, out wattageModifier))
		{
			return wattageModifier;
		}
		return BionicBatteryMonitor.difficultyWattages["Default"];
	}

	// Token: 0x04003068 RID: 12392
	public const int DEFAULT_ELECTROBANK_COUNT = 4;

	// Token: 0x04003069 RID: 12393
	public const int BIONIC_SKILL_EXTRA_BATTERY_COUNT = 2;

	// Token: 0x0400306A RID: 12394
	public const int MAX_ELECTROBANK_COUNT = 6;

	// Token: 0x0400306B RID: 12395
	public const float DEFAULT_WATTS = 200f;

	// Token: 0x0400306C RID: 12396
	public const string INITIAL_ELECTROBANK_TYPE_ID = "DisposableElectrobank_RawMetal";

	// Token: 0x0400306D RID: 12397
	public static readonly string ChargedBatteryIcon = "<sprite=\"oni_sprite_assets\" name=\"oni_sprite_assets_charged_electrobank\">";

	// Token: 0x0400306E RID: 12398
	public static readonly string DischargedBatteryIcon = "<sprite=\"oni_sprite_assets\" name=\"oni_sprite_assets_discharged_electrobank\">";

	// Token: 0x0400306F RID: 12399
	public static readonly string CriticalBatteryIcon = "<sprite=\"oni_sprite_assets\" name=\"oni_sprite_assets_critical_electrobank\">";

	// Token: 0x04003070 RID: 12400
	public static readonly string SavingBatteryIcon = "<sprite=\"oni_sprite_assets\" name=\"oni_sprite_assets_saving_electrobank\">";

	// Token: 0x04003071 RID: 12401
	public static readonly string EmptySlotBatteryIcon = "<sprite=\"oni_sprite_assets\" name=\"oni_sprite_assets_empty_slot_electrobank\">";

	// Token: 0x04003072 RID: 12402
	private const string ANIM_NAME_REBOOT = "power_up";

	// Token: 0x04003073 RID: 12403
	public GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State firstSpawn;

	// Token: 0x04003074 RID: 12404
	public BionicBatteryMonitor.OnlineStates online;

	// Token: 0x04003075 RID: 12405
	public BionicBatteryMonitor.OfflineStates offline;

	// Token: 0x04003076 RID: 12406
	public StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Signal OnClosestAvailableElectrobankChangedSignal;

	// Token: 0x04003077 RID: 12407
	public StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.IntParameter ChargedElectrobankCount;

	// Token: 0x04003078 RID: 12408
	public StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.IntParameter DepletedElectrobankCount;

	// Token: 0x04003079 RID: 12409
	private StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.BoolParameter InitialElectrobanksSpawned;

	// Token: 0x0400307A RID: 12410
	private StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.BoolParameter IsOnline;

	// Token: 0x0400307B RID: 12411
	private StateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.Signal OnElectrobankStorageChanged;

	// Token: 0x0400307C RID: 12412
	private static readonly Dictionary<string, BionicBatteryMonitor.WattageModifier> difficultyWattages = new Dictionary<string, BionicBatteryMonitor.WattageModifier>
	{
		{
			"VeryHard",
			BionicBatteryMonitor.MakeDifficultyModifier("difficultyWattage", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.BIONICPOWERUSE.LEVELS.VERYHARD.NAME, 200f)
		},
		{
			"Hard",
			BionicBatteryMonitor.MakeDifficultyModifier("difficultyWattage", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.BIONICPOWERUSE.LEVELS.HARD.NAME, 100f)
		},
		{
			"Default",
			BionicBatteryMonitor.MakeDifficultyModifier("difficultyWattage", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.BIONICPOWERUSE.LEVELS.DEFAULT.NAME, 0f)
		},
		{
			"Easy",
			BionicBatteryMonitor.MakeDifficultyModifier("difficultyWattage", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.BIONICPOWERUSE.LEVELS.EASY.NAME, -100f)
		},
		{
			"VeryEasy",
			BionicBatteryMonitor.MakeDifficultyModifier("difficultyWattage", UI.FRONTEND.CUSTOMGAMESETTINGSSCREEN.SETTINGS.BIONICPOWERUSE.LEVELS.VERYEASY.NAME, -150f)
		}
	};

	// Token: 0x020019E7 RID: 6631
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020019E8 RID: 6632
	public struct WattageModifier
	{
		// Token: 0x0600A126 RID: 41254 RVA: 0x0039D896 File Offset: 0x0039BA96
		public WattageModifier(string id, string name, float value, float potentialValue)
		{
			this.id = id;
			this.name = name;
			this.value = value;
			this.potentialValue = potentialValue;
		}

		// Token: 0x04007E0D RID: 32269
		public float potentialValue;

		// Token: 0x04007E0E RID: 32270
		public float value;

		// Token: 0x04007E0F RID: 32271
		public string name;

		// Token: 0x04007E10 RID: 32272
		public string id;
	}

	// Token: 0x020019E9 RID: 6633
	public class OnlineStates : GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State
	{
		// Token: 0x04007E11 RID: 32273
		public GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State idle;

		// Token: 0x04007E12 RID: 32274
		public BionicBatteryMonitor.UpkeepStates upkeep;

		// Token: 0x04007E13 RID: 32275
		public BionicBatteryMonitor.UpkeepStates critical;
	}

	// Token: 0x020019EA RID: 6634
	public class UpkeepStates : GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State
	{
		// Token: 0x04007E14 RID: 32276
		public GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State seekElectrobank;
	}

	// Token: 0x020019EB RID: 6635
	public class OfflineStates : GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State
	{
		// Token: 0x04007E15 RID: 32277
		public GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State waitingForBatteryDelivery;

		// Token: 0x04007E16 RID: 32278
		public BionicBatteryMonitor.RebootWorkableState waitingForBatteryInstallation;

		// Token: 0x04007E17 RID: 32279
		public GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State reboot;
	}

	// Token: 0x020019EC RID: 6636
	public class RebootWorkableState : GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State
	{
		// Token: 0x04007E18 RID: 32280
		public GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State waiting;

		// Token: 0x04007E19 RID: 32281
		public GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.State working;
	}

	// Token: 0x020019ED RID: 6637
	public new class Instance : GameStateMachine<BionicBatteryMonitor, BionicBatteryMonitor.Instance, IStateMachineTarget, BionicBatteryMonitor.Def>.GameInstance
	{
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x0600A12B RID: 41259 RVA: 0x0039D8D5 File Offset: 0x0039BAD5
		public float Wattage
		{
			get
			{
				return this.GetBaseWattage() + this.GetModifiersWattage();
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x0600A12C RID: 41260 RVA: 0x0039D8E4 File Offset: 0x0039BAE4
		public bool IsOnline
		{
			get
			{
				return base.sm.IsOnline.Get(this);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600A12D RID: 41261 RVA: 0x0039D8F7 File Offset: 0x0039BAF7
		public bool InUpkeepTime
		{
			get
			{
				return this.schedulable.IsAllowed(Db.Get().ScheduleBlockTypes.Eat);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600A12E RID: 41262 RVA: 0x0039D913 File Offset: 0x0039BB13
		public bool HaveInitialElectrobanksBeenSpawned
		{
			get
			{
				return base.sm.InitialElectrobanksSpawned.Get(this);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600A12F RID: 41263 RVA: 0x0039D926 File Offset: 0x0039BB26
		public bool HasSpaceForNewElectrobank
		{
			get
			{
				return this.ElectrobankCount < this.ElectrobankCountCapacity;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600A130 RID: 41264 RVA: 0x0039D936 File Offset: 0x0039BB36
		public int ElectrobankCount
		{
			get
			{
				return this.ChargedElectrobankCount + this.DepletedElectrobankCount;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600A131 RID: 41265 RVA: 0x0039D945 File Offset: 0x0039BB45
		public int ChargedElectrobankCount
		{
			get
			{
				return base.sm.ChargedElectrobankCount.Get(this);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600A132 RID: 41266 RVA: 0x0039D958 File Offset: 0x0039BB58
		public int DepletedElectrobankCount
		{
			get
			{
				return base.sm.DepletedElectrobankCount.Get(this);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600A133 RID: 41267 RVA: 0x0039D96B File Offset: 0x0039BB6B
		public float CurrentCharge
		{
			get
			{
				return this.BionicBattery.value;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600A134 RID: 41268 RVA: 0x0039D978 File Offset: 0x0039BB78
		public int ElectrobankCountCapacity
		{
			get
			{
				return (int)base.gameObject.GetAttributes().Get(Db.Get().Attributes.BionicBatteryCountCapacity.Id).GetTotalValue();
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x0600A136 RID: 41270 RVA: 0x0039D9AD File Offset: 0x0039BBAD
		// (set) Token: 0x0600A135 RID: 41269 RVA: 0x0039D9A4 File Offset: 0x0039BBA4
		public ReanimateBionicWorkable reanimateWorkable { get; private set; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600A138 RID: 41272 RVA: 0x0039D9BE File Offset: 0x0039BBBE
		// (set) Token: 0x0600A137 RID: 41271 RVA: 0x0039D9B5 File Offset: 0x0039BBB5
		public List<BionicBatteryMonitor.WattageModifier> Modifiers { get; set; } = new List<BionicBatteryMonitor.WattageModifier>();

		// Token: 0x0600A139 RID: 41273 RVA: 0x0039D9C8 File Offset: 0x0039BBC8
		public Instance(IStateMachineTarget master, BionicBatteryMonitor.Def def)
			: base(master, def)
		{
			this.storage = base.gameObject.GetComponents<Storage>().FindFirst((Storage s) => s.storageID == GameTags.StoragesIds.BionicBatteryStorage);
			this.reanimateWorkable = base.GetComponent<ReanimateBionicWorkable>();
			this.schedulable = base.GetComponent<Schedulable>();
			this.manualDelivery = base.GetComponent<ManualDeliveryKG>();
			this.selectable = base.GetComponent<KSelectable>();
			this.prefabID = base.GetComponent<KPrefabID>();
			this.dataHolder = base.GetComponent<MinionStorageDataHolder>();
			MinionStorageDataHolder minionStorageDataHolder = this.dataHolder;
			minionStorageDataHolder.OnCopyBegins = (Action<StoredMinionIdentity>)Delegate.Combine(minionStorageDataHolder.OnCopyBegins, new Action<StoredMinionIdentity>(this.OnCopyMinionBegins));
			this.BionicBattery = Db.Get().Amounts.BionicInternalBattery.Lookup(base.gameObject);
			Storage storage = this.storage;
			storage.onDestroyItemsDropped = (Action<List<GameObject>>)Delegate.Combine(storage.onDestroyItemsDropped, new Action<List<GameObject>>(this.OnBatteriesDroppedFromDeath));
			Storage storage2 = this.storage;
			storage2.OnStorageChange = (Action<GameObject>)Delegate.Combine(storage2.OnStorageChange, new Action<GameObject>(this.OnElectrobankStorageChanged));
			base.Subscribe(540773776, new Action<object>(this.OnSkillsChanged));
			this.UpdateCapacityAmount();
			this.ApplyDifficultyModifiers();
		}

		// Token: 0x0600A13A RID: 41274 RVA: 0x0039DB1D File Offset: 0x0039BD1D
		public override void StartSM()
		{
			this.closestElectrobankSensor = base.GetComponent<Sensors>().GetSensor<ClosestElectrobankSensor>();
			ClosestElectrobankSensor closestElectrobankSensor = this.closestElectrobankSensor;
			closestElectrobankSensor.OnItemChanged = (Action<Electrobank>)Delegate.Combine(closestElectrobankSensor.OnItemChanged, new Action<Electrobank>(this.OnClosestElectrobankChanged));
			base.StartSM();
		}

		// Token: 0x0600A13B RID: 41275 RVA: 0x0039DB60 File Offset: 0x0039BD60
		private void OnCopyMinionBegins(StoredMinionIdentity destination)
		{
			MinionStorageDataHolder.DataPackData dataPackData = new MinionStorageDataHolder.DataPackData
			{
				Bools = new bool[] { this.HaveInitialElectrobanksBeenSpawned, this.IsOnline }
			};
			this.dataHolder.UpdateData(dataPackData);
		}

		// Token: 0x0600A13C RID: 41276 RVA: 0x0039DBA0 File Offset: 0x0039BDA0
		public override void PostParamsInitialized()
		{
			MinionStorageDataHolder.DataPack dataPack = this.dataHolder.GetDataPack<BionicBatteryMonitor.Instance>();
			if (dataPack != null && dataPack.IsStoringNewData)
			{
				MinionStorageDataHolder.DataPackData dataPackData = dataPack.ReadData();
				if (dataPackData != null)
				{
					bool flag = ((dataPackData.Bools != null && dataPackData.Bools.Length != 0) ? dataPackData.Bools[0] : this.HasSpaceForNewElectrobank);
					bool flag2 = ((dataPackData.Bools != null && dataPackData.Bools.Length > 1) ? dataPackData.Bools[1] : this.IsOnline);
					base.sm.InitialElectrobanksSpawned.Set(flag, this, false);
					base.sm.IsOnline.Set(flag2, this, false);
				}
			}
			this.RefreshCharge();
			base.PostParamsInitialized();
		}

		// Token: 0x0600A13D RID: 41277 RVA: 0x0039DC4C File Offset: 0x0039BE4C
		public void DropAllDischargedElectrobanks()
		{
			List<GameObject> list = new List<GameObject>();
			this.storage.Find(GameTags.EmptyPortableBattery, list);
			foreach (GameObject gameObject in list)
			{
				this.storage.Drop(gameObject, true);
			}
		}

		// Token: 0x0600A13E RID: 41278 RVA: 0x0039DCBC File Offset: 0x0039BEBC
		protected override void OnCleanUp()
		{
			if (this.dataHolder != null)
			{
				MinionStorageDataHolder minionStorageDataHolder = this.dataHolder;
				minionStorageDataHolder.OnCopyBegins = (Action<StoredMinionIdentity>)Delegate.Remove(minionStorageDataHolder.OnCopyBegins, new Action<StoredMinionIdentity>(this.OnCopyMinionBegins));
			}
			this.UpdateNotifications();
			base.OnCleanUp();
		}

		// Token: 0x0600A13F RID: 41279 RVA: 0x0039DD0A File Offset: 0x0039BF0A
		private void OnSkillsChanged(object o)
		{
			if (this.storage.capacityKg != (float)this.ElectrobankCountCapacity)
			{
				this.OnBatteryCapacityChanged();
			}
		}

		// Token: 0x0600A140 RID: 41280 RVA: 0x0039DD28 File Offset: 0x0039BF28
		private void ApplyDifficultyModifiers()
		{
			SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.BionicWattage);
			BionicBatteryMonitor.WattageModifier wattageModifier;
			if (BionicBatteryMonitor.difficultyWattages.TryGetValue(currentQualitySetting.id, out wattageModifier))
			{
				this.Modifiers.Add(wattageModifier);
			}
		}

		// Token: 0x0600A141 RID: 41281 RVA: 0x0039DD68 File Offset: 0x0039BF68
		private void UpdateCapacityAmount()
		{
			int num = this.ElectrobankCountCapacity - 4;
			this.BionicBattery.maxAttribute.ClearModifiers();
			this.BionicBattery.maxAttribute.Add(new AttributeModifier(Db.Get().Amounts.BionicInternalBattery.maxAttribute.Id, 120000f * (float)num, null, false, false, true));
		}

		// Token: 0x0600A142 RID: 41282 RVA: 0x0039DDC8 File Offset: 0x0039BFC8
		private void OnBatteryCapacityChanged()
		{
			this.UpdateCapacityAmount();
			for (int i = this.storage.Count - 1; i >= 0; i--)
			{
				if (this.storage.Count > this.ElectrobankCountCapacity)
				{
					GameObject gameObject = this.storage.items[i];
					Electrobank component = gameObject.GetComponent<Electrobank>();
					this.storage.Drop(gameObject, true);
					Vector3 position = gameObject.transform.position;
					position.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
					gameObject.transform.position = position;
					if (component != null && component.HasTag(GameTags.ChargedPortableBattery) && !component.IsFullyCharged)
					{
						component.RemovePower(component.Charge, true);
					}
				}
			}
			base.smi.storage.capacityKg = (float)this.ElectrobankCountCapacity;
		}

		// Token: 0x0600A143 RID: 41283 RVA: 0x0039DE9C File Offset: 0x0039C09C
		private void OnClosestElectrobankChanged(Electrobank newItem)
		{
			base.sm.OnClosestAvailableElectrobankChangedSignal.Trigger(this);
		}

		// Token: 0x0600A144 RID: 41284 RVA: 0x0039DEAF File Offset: 0x0039C0AF
		public float GetBaseWattage()
		{
			return 200f;
		}

		// Token: 0x0600A145 RID: 41285 RVA: 0x0039DEB8 File Offset: 0x0039C0B8
		public float GetModifiersWattage()
		{
			float num = 0f;
			foreach (BionicBatteryMonitor.WattageModifier wattageModifier in this.Modifiers)
			{
				num += wattageModifier.value;
			}
			return num;
		}

		// Token: 0x0600A146 RID: 41286 RVA: 0x0039DF14 File Offset: 0x0039C114
		private void OnElectrobankStorageChanged(object o)
		{
			this.ReorganizeElectrobanks();
			this.RefreshCharge();
			base.smi.sm.OnElectrobankStorageChanged.Trigger(this);
		}

		// Token: 0x0600A147 RID: 41287 RVA: 0x0039DF38 File Offset: 0x0039C138
		public void ReorganizeElectrobanks()
		{
			this.storage.items.Sort(delegate(GameObject b1, GameObject b2)
			{
				Electrobank component = b1.GetComponent<Electrobank>();
				Electrobank component2 = b2.GetComponent<Electrobank>();
				if (component == null)
				{
					return -1;
				}
				if (component2 == null)
				{
					return 1;
				}
				return component.Charge.CompareTo(component2.Charge);
			});
		}

		// Token: 0x0600A148 RID: 41288 RVA: 0x0039DF6C File Offset: 0x0039C16C
		public void CreateWorkableChore()
		{
			if (this.reanimateChore == null)
			{
				this.reanimateChore = new WorkChore<ReanimateBionicWorkable>(Db.Get().ChoreTypes.RescueIncapacitated, this.reanimateWorkable, null, true, null, null, null, true, null, false, false, null, false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, true);
				this.reanimateChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
			}
		}

		// Token: 0x0600A149 RID: 41289 RVA: 0x0039DFC8 File Offset: 0x0039C1C8
		public void CancelWorkChore()
		{
			if (this.reanimateChore != null)
			{
				this.reanimateChore.Cancel("BionicBatteryMonitor.CancelChore");
				this.reanimateChore = null;
			}
		}

		// Token: 0x0600A14A RID: 41290 RVA: 0x0039DFE9 File Offset: 0x0039C1E9
		public void SetOnlineState(bool online)
		{
			base.sm.IsOnline.Set(online, this, false);
			this.RefreshCharge();
		}

		// Token: 0x0600A14B RID: 41291 RVA: 0x0039E008 File Offset: 0x0039C208
		public void SetManualDeliveryEnableState(bool enable)
		{
			if (!enable)
			{
				this.manualDelivery.capacity = 0f;
				this.manualDelivery.refillMass = 0f;
				this.manualDelivery.RequestedItemTag = null;
				this.manualDelivery.AbortDelivery("Manual delivery disabled");
				return;
			}
			Tag[] array = new Tag[GameTags.BionicIncompatibleBatteries.Count];
			GameTags.BionicIncompatibleBatteries.CopyTo(array, 0);
			base.smi.storage.capacityKg = (float)this.ElectrobankCountCapacity;
			base.smi.manualDelivery.capacity = 1f;
			base.smi.manualDelivery.refillMass = 1f;
			base.smi.manualDelivery.MinimumMass = 1f;
			this.manualDelivery.ForbiddenTags = array;
			this.manualDelivery.RequestedItemTag = GameTags.ChargedPortableBattery;
		}

		// Token: 0x0600A14C RID: 41292 RVA: 0x0039E0E8 File Offset: 0x0039C2E8
		public GameObject GetFirstDischargedElectrobankInInventory()
		{
			return this.storage.FindFirst(GameTags.EmptyPortableBattery);
		}

		// Token: 0x0600A14D RID: 41293 RVA: 0x0039E0FA File Offset: 0x0039C2FA
		public Electrobank GetClosestElectrobank()
		{
			return this.closestElectrobankSensor.GetItem();
		}

		// Token: 0x0600A14E RID: 41294 RVA: 0x0039E108 File Offset: 0x0039C308
		public void RefreshCharge()
		{
			ListPool<GameObject, BionicBatteryMonitor.Instance>.PooledList pooledList = ListPool<GameObject, BionicBatteryMonitor.Instance>.Allocate();
			ListPool<GameObject, BionicBatteryMonitor.Instance>.PooledList pooledList2 = ListPool<GameObject, BionicBatteryMonitor.Instance>.Allocate();
			this.storage.Find(GameTags.ChargedPortableBattery, pooledList);
			this.storage.Find(GameTags.EmptyPortableBattery, pooledList2);
			float num = 0f;
			if (this.IsOnline)
			{
				for (int i = 0; i < pooledList.Count; i++)
				{
					Electrobank component = pooledList[i].GetComponent<Electrobank>();
					num += component.Charge;
				}
			}
			this.BionicBattery.SetValue(num);
			base.sm.ChargedElectrobankCount.Set(pooledList.Count, this, false);
			pooledList.Recycle();
			base.sm.DepletedElectrobankCount.Set(pooledList2.Count, this, false);
			pooledList2.Recycle();
			this.UpdateNotifications();
		}

		// Token: 0x0600A14F RID: 41295 RVA: 0x0039E1D0 File Offset: 0x0039C3D0
		public void ConsumePower(float joules)
		{
			ListPool<GameObject, BionicBatteryMonitor.Instance>.PooledList pooledList = ListPool<GameObject, BionicBatteryMonitor.Instance>.Allocate();
			this.storage.Find(GameTags.ChargedPortableBattery, pooledList);
			float num = joules;
			for (int i = 0; i < pooledList.Count; i++)
			{
				Electrobank component = pooledList[i].GetComponent<Electrobank>();
				float num2 = Mathf.Min(component.Charge, num);
				float num3 = component.RemovePower(num2, false);
				num -= num3;
				WorldResourceAmountTracker<ElectrobankTracker>.Get().RegisterAmountConsumed(component.ID, num3);
			}
			this.RefreshCharge();
			pooledList.Recycle();
		}

		// Token: 0x0600A150 RID: 41296 RVA: 0x0039E254 File Offset: 0x0039C454
		public void DebugAddCharge(float joules)
		{
			float num = MathF.Min(joules, (float)this.ElectrobankCountCapacity * 120000f - this.CurrentCharge);
			ListPool<GameObject, BionicBatteryMonitor.Instance>.PooledList pooledList = ListPool<GameObject, BionicBatteryMonitor.Instance>.Allocate();
			this.storage.Find(GameTags.ChargedPortableBattery, pooledList);
			int num2 = 0;
			while (num > 0f && num2 < pooledList.Count)
			{
				Electrobank component = pooledList[num2].GetComponent<Electrobank>();
				float num3 = Mathf.Min(120000f - component.Charge, num);
				component.AddPower(num3);
				num -= num3;
				num2++;
			}
			if (num > 0f && pooledList.Count < this.ElectrobankCountCapacity)
			{
				int num4 = this.storage.items.Count - 1;
				while (num > 0f && num4 >= 0)
				{
					GameObject gameObject = this.storage.items[num4];
					if (!(gameObject == null))
					{
						Electrobank electrobank = gameObject.GetComponent<Electrobank>();
						if (electrobank == null && gameObject.HasTag(GameTags.EmptyPortableBattery))
						{
							this.storage.Drop(gameObject, true);
							GameObject gameObject2 = Util.KInstantiate(Assets.GetPrefab("DisposableElectrobank_RawMetal"), base.transform.position);
							gameObject2.SetActive(true);
							electrobank = gameObject2.GetComponent<Electrobank>();
							float num5 = Mathf.Clamp(electrobank.Charge - num, 0f, float.MaxValue);
							electrobank.RemovePower(num5, true);
							num -= electrobank.Charge;
							this.storage.Store(gameObject2, false, false, true, false);
						}
					}
					num4--;
				}
			}
			if (num > 0f && this.storage.items.Count < this.ElectrobankCountCapacity)
			{
				do
				{
					GameObject gameObject3 = Util.KInstantiate(Assets.GetPrefab("DisposableElectrobank_RawMetal"), base.transform.position);
					gameObject3.SetActive(true);
					Electrobank component2 = gameObject3.GetComponent<Electrobank>();
					float num6 = Mathf.Clamp(component2.Charge - num, 0f, float.MaxValue);
					component2.RemovePower(num6, true);
					num -= component2.Charge;
					this.storage.Store(gameObject3, false, false, true, false);
				}
				while (num > 0f && this.storage.items.Count < this.ElectrobankCountCapacity && num > 0f);
			}
			this.RefreshCharge();
			pooledList.Recycle();
		}

		// Token: 0x0600A151 RID: 41297 RVA: 0x0039E4C4 File Offset: 0x0039C6C4
		private void UpdateNotifications()
		{
			this.criticalBatteryStatusItemGuid = this.selectable.ToggleStatusItem(Db.Get().DuplicantStatusItems.BionicCriticalBattery, this.criticalBatteryStatusItemGuid, BionicBatteryMonitor.ChargeIsBelowNotificationThreshold(base.smi) && !this.prefabID.HasTag(GameTags.Incapacitated) && !this.prefabID.HasTag(GameTags.Dead), base.gameObject);
		}

		// Token: 0x0600A152 RID: 41298 RVA: 0x0039E534 File Offset: 0x0039C734
		public bool AddOrUpdateModifier(BionicBatteryMonitor.WattageModifier modifier, bool triggerCallbacks = true)
		{
			int num = this.Modifiers.FindIndex((BionicBatteryMonitor.WattageModifier mod) => mod.id == modifier.id);
			bool flag;
			if (num >= 0)
			{
				flag = this.Modifiers[num].name != modifier.name || this.Modifiers[num].value != modifier.value || this.Modifiers[num].potentialValue != modifier.potentialValue;
				this.Modifiers[num] = modifier;
			}
			else
			{
				this.Modifiers.Add(modifier);
				flag = true;
			}
			if (flag)
			{
				this.Modifiers.Sort((BionicBatteryMonitor.WattageModifier a, BionicBatteryMonitor.WattageModifier b) => b.value.CompareTo(a.value));
			}
			if (triggerCallbacks)
			{
				base.Trigger(1361471071, this.Wattage);
			}
			return flag;
		}

		// Token: 0x0600A153 RID: 41299 RVA: 0x0039E640 File Offset: 0x0039C840
		public bool RemoveModifier(string modifierID, bool triggerCallbacks = true)
		{
			int num = this.Modifiers.FindIndex((BionicBatteryMonitor.WattageModifier mod) => mod.id == modifierID);
			if (num >= 0)
			{
				this.Modifiers.RemoveAt(num);
				if (triggerCallbacks)
				{
					base.Trigger(1361471071, this.Wattage);
				}
				this.Modifiers.Sort((BionicBatteryMonitor.WattageModifier a, BionicBatteryMonitor.WattageModifier b) => b.value.CompareTo(a.value));
				return true;
			}
			return false;
		}

		// Token: 0x0600A154 RID: 41300 RVA: 0x0039E6C8 File Offset: 0x0039C8C8
		private void OnBatteriesDroppedFromDeath(List<GameObject> items)
		{
			if (items != null)
			{
				for (int i = 0; i < items.Count; i++)
				{
					Electrobank component = items[i].GetComponent<Electrobank>();
					if (component != null && component.HasTag(GameTags.ChargedPortableBattery) && !component.IsFullyCharged)
					{
						component.RemovePower(component.Charge, true);
					}
				}
			}
		}

		// Token: 0x0600A155 RID: 41301 RVA: 0x0039E724 File Offset: 0x0039C924
		public void SpawnAndInstallInitialElectrobanks()
		{
			for (int i = 0; i < this.ElectrobankCountCapacity; i++)
			{
				GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("DisposableElectrobank_RawMetal"), base.transform.position);
				gameObject.SetActive(true);
				this.storage.Store(gameObject, false, false, true, false);
			}
			this.RefreshCharge();
			this.SetOnlineState(true);
			base.sm.InitialElectrobanksSpawned.Set(true, this, false);
		}

		// Token: 0x0600A156 RID: 41302 RVA: 0x0039E79C File Offset: 0x0039C99C
		public void AutomaticallyDropAllDepletedElectrobanks()
		{
			List<GameObject> list = new List<GameObject>();
			this.storage.Find(GameTags.EmptyPortableBattery, list);
			for (int i = 0; i < list.Count; i++)
			{
				GameObject gameObject = list[i];
				this.storage.Drop(gameObject, true);
			}
		}

		// Token: 0x04007E1A RID: 32282
		public Storage storage;

		// Token: 0x04007E1B RID: 32283
		public KPrefabID prefabID;

		// Token: 0x04007E1D RID: 32285
		private Schedulable schedulable;

		// Token: 0x04007E1E RID: 32286
		private AmountInstance BionicBattery;

		// Token: 0x04007E1F RID: 32287
		private ManualDeliveryKG manualDelivery;

		// Token: 0x04007E20 RID: 32288
		private ClosestElectrobankSensor closestElectrobankSensor;

		// Token: 0x04007E21 RID: 32289
		private KSelectable selectable;

		// Token: 0x04007E22 RID: 32290
		private MinionStorageDataHolder dataHolder;

		// Token: 0x04007E23 RID: 32291
		private Guid criticalBatteryStatusItemGuid;

		// Token: 0x04007E25 RID: 32293
		private Chore reanimateChore;
	}
}
