using System;
using System.Collections.Generic;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000738 RID: 1848
public class GeoTuner : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>
{
	// Token: 0x06002EA7 RID: 11943 RVA: 0x0010B2A8 File Offset: 0x001094A8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.operational;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshAnimationGeyserSymbolType));
		this.nonOperational.DefaultState(this.nonOperational.off).OnSignal(this.geyserSwitchSignal, this.nonOperational.switchingGeyser).Enter(delegate(GeoTuner.Instance smi)
		{
			smi.RefreshLogicOutput();
		})
			.TagTransition(GameTags.Operational, this.operational, false);
		this.nonOperational.off.PlayAnim("off");
		this.nonOperational.switchingGeyser.QueueAnim("geyser_down", false, null).OnAnimQueueComplete(this.nonOperational.down);
		this.nonOperational.down.PlayAnim("geyser_up").QueueAnim("off", false, null).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshAnimationGeyserSymbolType))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.TriggerSoundsForGeyserChange));
		this.operational.PlayAnim("on").Enter(delegate(GeoTuner.Instance smi)
		{
			smi.RefreshLogicOutput();
		}).DefaultState(this.operational.idle)
			.TagTransition(GameTags.Operational, this.nonOperational, true);
		this.operational.idle.ParamTransition<GameObject>(this.AssignedGeyser, this.operational.geyserSelected, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsNotNull).ParamTransition<GameObject>(this.AssignedGeyser, this.operational.noGeyserSelected, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsNull);
		this.operational.noGeyserSelected.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerNoGeyserSelected, null).ParamTransition<GameObject>(this.AssignedGeyser, this.operational.geyserSelected.switchingGeyser, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsNotNull).Enter(delegate(GeoTuner.Instance smi)
		{
			smi.RefreshLogicOutput();
		})
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.DropStorage))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshStorageRequirements))
			.Exit(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ForgetWorkDoneByDupe))
			.Exit(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ResetExpirationTimer))
			.QueueAnim("geyser_down", false, null)
			.OnAnimQueueComplete(this.operational.noGeyserSelected.idle);
		this.operational.noGeyserSelected.idle.PlayAnim("geyser_up").QueueAnim("on", false, null).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshAnimationGeyserSymbolType))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.TriggerSoundsForGeyserChange));
		this.operational.geyserSelected.DefaultState(this.operational.geyserSelected.idle).ToggleStatusItem(Db.Get().BuildingStatusItems.GeoTunerGeyserStatus, null).ParamTransition<GameObject>(this.AssignedGeyser, this.operational.noGeyserSelected, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsNull)
			.OnSignal(this.geyserSwitchSignal, this.operational.geyserSelected.switchingGeyser)
			.Enter(delegate(GeoTuner.Instance smi)
			{
				smi.RefreshLogicOutput();
			});
		this.operational.geyserSelected.idle.ParamTransition<bool>(this.hasBeenWorkedByResearcher, this.operational.geyserSelected.broadcasting.active, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsTrue).ParamTransition<bool>(this.hasBeenWorkedByResearcher, this.operational.geyserSelected.researcherInteractionNeeded, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsFalse);
		this.operational.geyserSelected.switchingGeyser.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.DropStorageIfNotMatching)).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ForgetWorkDoneByDupe)).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ResetExpirationTimer))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshStorageRequirements))
			.Enter(delegate(GeoTuner.Instance smi)
			{
				smi.RefreshLogicOutput();
			})
			.QueueAnim("geyser_down", false, null)
			.OnAnimQueueComplete(this.operational.geyserSelected.switchingGeyser.down);
		this.operational.geyserSelected.switchingGeyser.down.QueueAnim("geyser_up", false, null).QueueAnim("on", false, null).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshAnimationGeyserSymbolType))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.TriggerSoundsForGeyserChange))
			.ScheduleActionNextFrame("Switch Animation Completed", delegate(GeoTuner.Instance smi)
			{
				smi.GoTo(this.operational.geyserSelected.idle);
			});
		this.operational.geyserSelected.researcherInteractionNeeded.EventTransition(GameHashes.UpdateRoom, this.operational.geyserSelected.researcherInteractionNeeded.blocked, (GeoTuner.Instance smi) => !GeoTuner.WorkRequirementsMet(smi)).EventTransition(GameHashes.UpdateRoom, this.operational.geyserSelected.researcherInteractionNeeded.available, new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.Transition.ConditionCallback(GeoTuner.WorkRequirementsMet)).EventTransition(GameHashes.OnStorageChange, this.operational.geyserSelected.researcherInteractionNeeded.blocked, (GeoTuner.Instance smi) => !GeoTuner.WorkRequirementsMet(smi))
			.EventTransition(GameHashes.OnStorageChange, this.operational.geyserSelected.researcherInteractionNeeded.available, new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.Transition.ConditionCallback(GeoTuner.WorkRequirementsMet))
			.ParamTransition<bool>(this.hasBeenWorkedByResearcher, this.operational.geyserSelected.broadcasting.active, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsTrue)
			.Exit(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ResetExpirationTimer));
		this.operational.geyserSelected.researcherInteractionNeeded.blocked.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerResearchNeeded, null).DoNothing();
		this.operational.geyserSelected.researcherInteractionNeeded.available.DefaultState(this.operational.geyserSelected.researcherInteractionNeeded.available.waitingForDupe).ToggleRecurringChore(new Func<GeoTuner.Instance, Chore>(this.CreateResearchChore), null).WorkableCompleteTransition((GeoTuner.Instance smi) => smi.workable, this.operational.geyserSelected.researcherInteractionNeeded.completed);
		this.operational.geyserSelected.researcherInteractionNeeded.available.waitingForDupe.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerResearchNeeded, null).WorkableStartTransition((GeoTuner.Instance smi) => smi.workable, this.operational.geyserSelected.researcherInteractionNeeded.available.inProgress);
		this.operational.geyserSelected.researcherInteractionNeeded.available.inProgress.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerResearchInProgress, null).WorkableStopTransition((GeoTuner.Instance smi) => smi.workable, this.operational.geyserSelected.researcherInteractionNeeded.available.waitingForDupe);
		this.operational.geyserSelected.researcherInteractionNeeded.completed.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.OnResearchCompleted));
		this.operational.geyserSelected.broadcasting.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerBroadcasting, null).Toggle("Tuning", new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ApplyTuning), new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RemoveTuning));
		this.operational.geyserSelected.broadcasting.onHold.PlayAnim("on").UpdateTransition(this.operational.geyserSelected.broadcasting.active, (GeoTuner.Instance smi, float dt) => !GeoTuner.GeyserExitEruptionTransition(smi, dt), UpdateRate.SIM_200ms, false);
		this.operational.geyserSelected.broadcasting.active.Toggle("EnergyConsumption", delegate(GeoTuner.Instance smi)
		{
			smi.operational.SetActive(true, false);
		}, delegate(GeoTuner.Instance smi)
		{
			smi.operational.SetActive(false, false);
		}).Toggle("BroadcastingAnimations", new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.PlayBroadcastingAnimation), new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.StopPlayingBroadcastingAnimation)).Update(new Action<GeoTuner.Instance, float>(GeoTuner.ExpirationTimerUpdate), UpdateRate.SIM_200ms, false)
			.UpdateTransition(this.operational.geyserSelected.broadcasting.onHold, new Func<GeoTuner.Instance, float, bool>(GeoTuner.GeyserExitEruptionTransition), UpdateRate.SIM_200ms, false)
			.ParamTransition<float>(this.expirationTimer, this.operational.geyserSelected.broadcasting.expired, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsLTEZero);
		this.operational.geyserSelected.broadcasting.expired.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ForgetWorkDoneByDupe)).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ResetExpirationTimer)).ScheduleActionNextFrame("Expired", delegate(GeoTuner.Instance smi)
		{
			smi.GoTo(this.operational.geyserSelected.researcherInteractionNeeded);
		});
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x0010BBF4 File Offset: 0x00109DF4
	private static void TriggerSoundsForGeyserChange(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser != null)
		{
			EventInstance eventInstance = default(EventInstance);
			switch (assignedGeyser.configuration.geyserType.shape)
			{
			case GeyserConfigurator.GeyserShape.Gas:
				eventInstance = SoundEvent.BeginOneShot(GeoTuner.gasGeyserTuningSoundPath, smi.transform.GetPosition(), 1f, false);
				break;
			case GeyserConfigurator.GeyserShape.Liquid:
				eventInstance = SoundEvent.BeginOneShot(GeoTuner.liquidGeyserTuningSoundPath, smi.transform.GetPosition(), 1f, false);
				break;
			case GeyserConfigurator.GeyserShape.Molten:
				eventInstance = SoundEvent.BeginOneShot(GeoTuner.metalGeyserTuningSoundPath, smi.transform.GetPosition(), 1f, false);
				break;
			}
			SoundEvent.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06002EA9 RID: 11945 RVA: 0x0010BCA0 File Offset: 0x00109EA0
	private static void RefreshStorageRequirements(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser == null)
		{
			smi.storage.capacityKg = 0f;
			smi.storage.storageFilters = null;
			smi.manualDelivery.capacity = 0f;
			smi.manualDelivery.refillMass = 0f;
			smi.manualDelivery.RequestedItemTag = null;
			smi.manualDelivery.AbortDelivery("No geyser is selected for tuning");
			return;
		}
		GeoTunerConfig.GeotunedGeyserSettings settingsForGeyser = smi.def.GetSettingsForGeyser(assignedGeyser);
		smi.storage.capacityKg = settingsForGeyser.quantity;
		smi.storage.storageFilters = new List<Tag> { settingsForGeyser.material };
		smi.manualDelivery.AbortDelivery("Switching to new delivery request");
		smi.manualDelivery.capacity = settingsForGeyser.quantity;
		smi.manualDelivery.refillMass = settingsForGeyser.quantity;
		smi.manualDelivery.MinimumMass = settingsForGeyser.quantity;
		smi.manualDelivery.RequestedItemTag = settingsForGeyser.material;
	}

	// Token: 0x06002EAA RID: 11946 RVA: 0x0010BDAC File Offset: 0x00109FAC
	private static void DropStorage(GeoTuner.Instance smi)
	{
		smi.storage.DropAll(false, false, default(Vector3), true, null);
	}

	// Token: 0x06002EAB RID: 11947 RVA: 0x0010BDD4 File Offset: 0x00109FD4
	private static void DropStorageIfNotMatching(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser != null)
		{
			GeoTunerConfig.GeotunedGeyserSettings settingsForGeyser = smi.def.GetSettingsForGeyser(assignedGeyser);
			List<GameObject> items = smi.storage.GetItems();
			if (smi.storage.GetItems() != null && items.Count > 0)
			{
				Tag tag = items[0].PrefabID();
				PrimaryElement component = items[0].GetComponent<PrimaryElement>();
				if (tag != settingsForGeyser.material)
				{
					smi.storage.DropAll(false, false, default(Vector3), true, null);
					return;
				}
				float num = component.Mass - settingsForGeyser.quantity;
				if (num > 0f)
				{
					smi.storage.DropSome(tag, num, false, false, default(Vector3), true, false);
					return;
				}
			}
		}
		else
		{
			smi.storage.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x06002EAC RID: 11948 RVA: 0x0010BEBC File Offset: 0x0010A0BC
	private static bool GeyserExitEruptionTransition(GeoTuner.Instance smi, float dt)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		return assignedGeyser != null && assignedGeyser.smi.GetCurrentState() != null && assignedGeyser.smi.GetCurrentState().parent != assignedGeyser.smi.sm.erupt;
	}

	// Token: 0x06002EAD RID: 11949 RVA: 0x0010BF0D File Offset: 0x0010A10D
	public static void OnResearchCompleted(GeoTuner.Instance smi)
	{
		smi.storage.ConsumeAllIgnoringDisease();
		smi.sm.hasBeenWorkedByResearcher.Set(true, smi, false);
	}

	// Token: 0x06002EAE RID: 11950 RVA: 0x0010BF2E File Offset: 0x0010A12E
	public static void PlayBroadcastingAnimation(GeoTuner.Instance smi)
	{
		smi.animController.Play("broadcasting", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06002EAF RID: 11951 RVA: 0x0010BF50 File Offset: 0x0010A150
	public static void StopPlayingBroadcastingAnimation(GeoTuner.Instance smi)
	{
		smi.animController.Play("broadcasting", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002EB0 RID: 11952 RVA: 0x0010BF72 File Offset: 0x0010A172
	public static void RefreshAnimationGeyserSymbolType(GeoTuner.Instance smi)
	{
		smi.RefreshGeyserSymbol();
	}

	// Token: 0x06002EB1 RID: 11953 RVA: 0x0010BF7A File Offset: 0x0010A17A
	public static float GetRemainingExpiraionTime(GeoTuner.Instance smi)
	{
		return smi.sm.expirationTimer.Get(smi);
	}

	// Token: 0x06002EB2 RID: 11954 RVA: 0x0010BF90 File Offset: 0x0010A190
	private static void ExpirationTimerUpdate(GeoTuner.Instance smi, float dt)
	{
		float num = GeoTuner.GetRemainingExpiraionTime(smi);
		num -= dt;
		smi.sm.expirationTimer.Set(num, smi, false);
	}

	// Token: 0x06002EB3 RID: 11955 RVA: 0x0010BFBC File Offset: 0x0010A1BC
	private static void ResetExpirationTimer(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser != null)
		{
			smi.sm.expirationTimer.Set(smi.def.GetSettingsForGeyser(assignedGeyser).duration, smi, false);
			return;
		}
		smi.sm.expirationTimer.Set(0f, smi, false);
	}

	// Token: 0x06002EB4 RID: 11956 RVA: 0x0010C016 File Offset: 0x0010A216
	private static void ForgetWorkDoneByDupe(GeoTuner.Instance smi)
	{
		smi.sm.hasBeenWorkedByResearcher.Set(false, smi, false);
		smi.workable.WorkTimeRemaining = smi.workable.GetWorkTime();
	}

	// Token: 0x06002EB5 RID: 11957 RVA: 0x0010C044 File Offset: 0x0010A244
	private Chore CreateResearchChore(GeoTuner.Instance smi)
	{
		return new WorkChore<GeoTunerWorkable>(Db.Get().ChoreTypes.Research, smi.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x06002EB6 RID: 11958 RVA: 0x0010C07C File Offset: 0x0010A27C
	private static void ApplyTuning(GeoTuner.Instance smi)
	{
		smi.GetAssignedGeyser().AddModification(smi.currentGeyserModification);
	}

	// Token: 0x06002EB7 RID: 11959 RVA: 0x0010C090 File Offset: 0x0010A290
	private static void RemoveTuning(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser != null)
		{
			assignedGeyser.RemoveModification(smi.currentGeyserModification);
		}
	}

	// Token: 0x06002EB8 RID: 11960 RVA: 0x0010C0B9 File Offset: 0x0010A2B9
	public static bool WorkRequirementsMet(GeoTuner.Instance smi)
	{
		return GeoTuner.IsInLabRoom(smi) && smi.storage.MassStored() == smi.storage.capacityKg;
	}

	// Token: 0x06002EB9 RID: 11961 RVA: 0x0010C0DD File Offset: 0x0010A2DD
	public static bool IsInLabRoom(GeoTuner.Instance smi)
	{
		return smi.roomTracker.IsInCorrectRoom();
	}

	// Token: 0x04001B97 RID: 7063
	private StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.Signal geyserSwitchSignal;

	// Token: 0x04001B98 RID: 7064
	private GeoTuner.NonOperationalState nonOperational;

	// Token: 0x04001B99 RID: 7065
	private GeoTuner.OperationalState operational;

	// Token: 0x04001B9A RID: 7066
	private StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.TargetParameter FutureGeyser;

	// Token: 0x04001B9B RID: 7067
	private StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.TargetParameter AssignedGeyser;

	// Token: 0x04001B9C RID: 7068
	public StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.BoolParameter hasBeenWorkedByResearcher;

	// Token: 0x04001B9D RID: 7069
	public StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.FloatParameter expirationTimer;

	// Token: 0x04001B9E RID: 7070
	public static string liquidGeyserTuningSoundPath = GlobalAssets.GetSound("GeoTuner_Tuning_Geyser", false);

	// Token: 0x04001B9F RID: 7071
	public static string gasGeyserTuningSoundPath = GlobalAssets.GetSound("GeoTuner_Tuning_Vent", false);

	// Token: 0x04001BA0 RID: 7072
	public static string metalGeyserTuningSoundPath = GlobalAssets.GetSound("GeoTuner_Tuning_Volcano", false);

	// Token: 0x04001BA1 RID: 7073
	public const string anim_switchGeyser_down = "geyser_down";

	// Token: 0x04001BA2 RID: 7074
	public const string anim_switchGeyser_up = "geyser_up";

	// Token: 0x04001BA3 RID: 7075
	private const string BroadcastingOnHoldAnimationName = "on";

	// Token: 0x04001BA4 RID: 7076
	private const string OnAnimName = "on";

	// Token: 0x04001BA5 RID: 7077
	private const string OffAnimName = "off";

	// Token: 0x04001BA6 RID: 7078
	private const string BroadcastingAnimationName = "broadcasting";

	// Token: 0x020015E7 RID: 5607
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06009332 RID: 37682 RVA: 0x00369B80 File Offset: 0x00367D80
		public GeoTunerConfig.GeotunedGeyserSettings GetSettingsForGeyser(Geyser geyser)
		{
			GeoTunerConfig.GeotunedGeyserSettings geotunedGeyserSettings;
			if (!this.geotunedGeyserSettings.TryGetValue(geyser.configuration.typeId, out geotunedGeyserSettings))
			{
				DebugUtil.DevLogError(string.Format("Geyser {0} is missing a Geotuner setting, using default", geyser.configuration.typeId));
				return this.defaultSetting;
			}
			return geotunedGeyserSettings;
		}

		// Token: 0x04007144 RID: 28996
		public string OUTPUT_LOGIC_PORT_ID;

		// Token: 0x04007145 RID: 28997
		public Dictionary<HashedString, GeoTunerConfig.GeotunedGeyserSettings> geotunedGeyserSettings;

		// Token: 0x04007146 RID: 28998
		public GeoTunerConfig.GeotunedGeyserSettings defaultSetting;
	}

	// Token: 0x020015E8 RID: 5608
	public class BroadcastingState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04007147 RID: 28999
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State active;

		// Token: 0x04007148 RID: 29000
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State onHold;

		// Token: 0x04007149 RID: 29001
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State expired;
	}

	// Token: 0x020015E9 RID: 5609
	public class ResearchProgress : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x0400714A RID: 29002
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State waitingForDupe;

		// Token: 0x0400714B RID: 29003
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State inProgress;
	}

	// Token: 0x020015EA RID: 5610
	public class ResearchState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x0400714C RID: 29004
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State blocked;

		// Token: 0x0400714D RID: 29005
		public GeoTuner.ResearchProgress available;

		// Token: 0x0400714E RID: 29006
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State completed;
	}

	// Token: 0x020015EB RID: 5611
	public class SwitchingGeyser : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x0400714F RID: 29007
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State down;
	}

	// Token: 0x020015EC RID: 5612
	public class GeyserSelectedState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04007150 RID: 29008
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State idle;

		// Token: 0x04007151 RID: 29009
		public GeoTuner.SwitchingGeyser switchingGeyser;

		// Token: 0x04007152 RID: 29010
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State resourceNeeded;

		// Token: 0x04007153 RID: 29011
		public GeoTuner.ResearchState researcherInteractionNeeded;

		// Token: 0x04007154 RID: 29012
		public GeoTuner.BroadcastingState broadcasting;
	}

	// Token: 0x020015ED RID: 5613
	public class SimpleIdleState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04007155 RID: 29013
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State idle;
	}

	// Token: 0x020015EE RID: 5614
	public class NonOperationalState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04007156 RID: 29014
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State off;

		// Token: 0x04007157 RID: 29015
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State switchingGeyser;

		// Token: 0x04007158 RID: 29016
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State down;
	}

	// Token: 0x020015EF RID: 5615
	public class OperationalState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04007159 RID: 29017
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State idle;

		// Token: 0x0400715A RID: 29018
		public GeoTuner.SimpleIdleState noGeyserSelected;

		// Token: 0x0400715B RID: 29019
		public GeoTuner.GeyserSelectedState geyserSelected;
	}

	// Token: 0x020015F0 RID: 5616
	public enum GeyserAnimTypeSymbols
	{
		// Token: 0x0400715D RID: 29021
		meter_gas,
		// Token: 0x0400715E RID: 29022
		meter_metal,
		// Token: 0x0400715F RID: 29023
		meter_liquid,
		// Token: 0x04007160 RID: 29024
		meter_board
	}

	// Token: 0x020015F1 RID: 5617
	public new class Instance : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.GameInstance
	{
		// Token: 0x0600933C RID: 37692 RVA: 0x00369C18 File Offset: 0x00367E18
		public Instance(IStateMachineTarget master, GeoTuner.Def def)
			: base(master, def)
		{
			this.originID = UI.StripLinkFormatting("GeoTuner") + " [" + base.gameObject.GetInstanceID().ToString() + "]";
			this.switchGeyserMeter = new MeterController(this.animController, "geyser_target", this.GetAnimationSymbol().ToString(), Meter.Offset.Behind, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		}

		// Token: 0x0600933D RID: 37693 RVA: 0x00369C94 File Offset: 0x00367E94
		public override void StartSM()
		{
			base.StartSM();
			Components.GeoTuners.Add(base.smi.GetMyWorldId(), this);
			Geyser assignedGeyser = this.GetAssignedGeyser();
			if (assignedGeyser != null)
			{
				assignedGeyser.Subscribe(-593169791, new Action<object>(this.OnEruptionStateChanged));
				this.RefreshModification();
			}
			this.RefreshLogicOutput();
			this.AssignFutureGeyser(this.GetFutureGeyser());
			base.gameObject.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
		}

		// Token: 0x0600933E RID: 37694 RVA: 0x00369D1A File Offset: 0x00367F1A
		public Geyser GetFutureGeyser()
		{
			if (base.smi.sm.FutureGeyser.IsNull(this))
			{
				return null;
			}
			return base.sm.FutureGeyser.Get(this).GetComponent<Geyser>();
		}

		// Token: 0x0600933F RID: 37695 RVA: 0x00369D4C File Offset: 0x00367F4C
		public Geyser GetAssignedGeyser()
		{
			if (base.smi.sm.AssignedGeyser.IsNull(this))
			{
				return null;
			}
			return base.sm.AssignedGeyser.Get(this).GetComponent<Geyser>();
		}

		// Token: 0x06009340 RID: 37696 RVA: 0x00369D80 File Offset: 0x00367F80
		public void AssignFutureGeyser(Geyser newFutureGeyser)
		{
			bool flag = newFutureGeyser != this.GetFutureGeyser();
			bool flag2 = this.GetAssignedGeyser() != newFutureGeyser;
			base.sm.FutureGeyser.Set(newFutureGeyser, this);
			if (flag)
			{
				if (flag2)
				{
					this.RecreateSwitchGeyserChore();
					return;
				}
				if (this.switchGeyserChore != null)
				{
					this.AbortSwitchGeyserChore("Future Geyser was set to current Geyser");
					return;
				}
			}
			else if (this.switchGeyserChore == null && flag2)
			{
				this.RecreateSwitchGeyserChore();
			}
		}

		// Token: 0x06009341 RID: 37697 RVA: 0x00369DF0 File Offset: 0x00367FF0
		private void AbortSwitchGeyserChore(string reason = "Aborting Switch Geyser Chore")
		{
			if (this.switchGeyserChore != null)
			{
				Chore chore = this.switchGeyserChore;
				chore.onComplete = (Action<Chore>)Delegate.Remove(chore.onComplete, new Action<Chore>(this.OnSwitchGeyserChoreCompleted));
				this.switchGeyserChore.Cancel(reason);
				this.switchGeyserChore = null;
			}
			this.switchGeyserChore = null;
		}

		// Token: 0x06009342 RID: 37698 RVA: 0x00369E48 File Offset: 0x00368048
		private Chore RecreateSwitchGeyserChore()
		{
			this.AbortSwitchGeyserChore("Recreating Chore");
			this.switchGeyserChore = new WorkChore<GeoTunerSwitchGeyserWorkable>(Db.Get().ChoreTypes.Toggle, this.switchGeyserWorkable, null, true, null, new Action<Chore>(this.ShowSwitchingGeyserStatusItem), new Action<Chore>(this.HideSwitchingGeyserStatusItem), true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			Chore chore = this.switchGeyserChore;
			chore.onComplete = (Action<Chore>)Delegate.Combine(chore.onComplete, new Action<Chore>(this.OnSwitchGeyserChoreCompleted));
			return this.switchGeyserChore;
		}

		// Token: 0x06009343 RID: 37699 RVA: 0x00369ED4 File Offset: 0x003680D4
		private void ShowSwitchingGeyserStatusItem(Chore chore)
		{
			base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.PendingSwitchToggle, null);
		}

		// Token: 0x06009344 RID: 37700 RVA: 0x00369EF7 File Offset: 0x003680F7
		private void HideSwitchingGeyserStatusItem(Chore chore)
		{
			base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingSwitchToggle, false);
		}

		// Token: 0x06009345 RID: 37701 RVA: 0x00369F1C File Offset: 0x0036811C
		private void OnSwitchGeyserChoreCompleted(Chore chore)
		{
			this.GetCurrentState();
			GeoTuner.NonOperationalState nonOperational = base.sm.nonOperational;
			Geyser futureGeyser = this.GetFutureGeyser();
			bool flag = this.GetAssignedGeyser() != futureGeyser;
			if (chore.isComplete && flag)
			{
				this.AssignGeyser(futureGeyser);
			}
			base.Trigger(1980521255, null);
		}

		// Token: 0x06009346 RID: 37702 RVA: 0x00369F70 File Offset: 0x00368170
		public void AssignGeyser(Geyser geyser)
		{
			Geyser assignedGeyser = this.GetAssignedGeyser();
			if (assignedGeyser != null && assignedGeyser != geyser)
			{
				GeoTuner.RemoveTuning(base.smi);
				assignedGeyser.Unsubscribe(-593169791, new Action<object>(base.smi.OnEruptionStateChanged));
			}
			Geyser geyser2 = assignedGeyser;
			base.sm.AssignedGeyser.Set(geyser, this);
			this.RefreshModification();
			if (geyser2 != geyser)
			{
				if (geyser != null)
				{
					geyser.Subscribe(-593169791, new Action<object>(this.OnEruptionStateChanged));
					geyser.Trigger(1763323737, null);
				}
				if (geyser2 != null)
				{
					geyser2.Trigger(1763323737, null);
				}
				base.sm.geyserSwitchSignal.Trigger(this);
			}
		}

		// Token: 0x06009347 RID: 37703 RVA: 0x0036A034 File Offset: 0x00368234
		private void RefreshModification()
		{
			Geyser assignedGeyser = this.GetAssignedGeyser();
			if (assignedGeyser != null)
			{
				GeoTunerConfig.GeotunedGeyserSettings settingsForGeyser = base.def.GetSettingsForGeyser(assignedGeyser);
				this.currentGeyserModification = settingsForGeyser.template;
				this.currentGeyserModification.originID = this.originID;
				this.enhancementDuration = settingsForGeyser.duration;
				assignedGeyser.Trigger(1763323737, null);
			}
			GeoTuner.RefreshStorageRequirements(this);
			GeoTuner.DropStorageIfNotMatching(this);
		}

		// Token: 0x06009348 RID: 37704 RVA: 0x0036A0A0 File Offset: 0x003682A0
		public void RefreshGeyserSymbol()
		{
			this.switchGeyserMeter.meterController.Play(this.GetAnimationSymbol().ToString(), KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x06009349 RID: 37705 RVA: 0x0036A0E4 File Offset: 0x003682E4
		private GeoTuner.GeyserAnimTypeSymbols GetAnimationSymbol()
		{
			GeoTuner.GeyserAnimTypeSymbols geyserAnimTypeSymbols = GeoTuner.GeyserAnimTypeSymbols.meter_board;
			Geyser assignedGeyser = base.smi.GetAssignedGeyser();
			if (assignedGeyser != null)
			{
				switch (assignedGeyser.configuration.geyserType.shape)
				{
				case GeyserConfigurator.GeyserShape.Gas:
					geyserAnimTypeSymbols = GeoTuner.GeyserAnimTypeSymbols.meter_gas;
					break;
				case GeyserConfigurator.GeyserShape.Liquid:
					geyserAnimTypeSymbols = GeoTuner.GeyserAnimTypeSymbols.meter_liquid;
					break;
				case GeyserConfigurator.GeyserShape.Molten:
					geyserAnimTypeSymbols = GeoTuner.GeyserAnimTypeSymbols.meter_metal;
					break;
				}
			}
			return geyserAnimTypeSymbols;
		}

		// Token: 0x0600934A RID: 37706 RVA: 0x0036A138 File Offset: 0x00368338
		public void OnEruptionStateChanged(object data)
		{
			bool flag = (bool)data;
			this.RefreshLogicOutput();
		}

		// Token: 0x0600934B RID: 37707 RVA: 0x0036A148 File Offset: 0x00368348
		public void RefreshLogicOutput()
		{
			Geyser assignedGeyser = this.GetAssignedGeyser();
			bool flag = this.GetCurrentState() != base.smi.sm.nonOperational;
			bool flag2 = assignedGeyser != null && this.GetCurrentState() != base.smi.sm.operational.noGeyserSelected;
			bool flag3 = assignedGeyser != null && assignedGeyser.smi.GetCurrentState() != null && (assignedGeyser.smi.GetCurrentState() == assignedGeyser.smi.sm.erupt || assignedGeyser.smi.GetCurrentState().parent == assignedGeyser.smi.sm.erupt);
			bool flag4 = flag && flag2 && flag3;
			this.logicPorts.SendSignal(base.def.OUTPUT_LOGIC_PORT_ID, flag4 ? 1 : 0);
			this.switchGeyserMeter.meterController.SetSymbolVisiblity("light_bloom", flag4);
		}

		// Token: 0x0600934C RID: 37708 RVA: 0x0036A244 File Offset: 0x00368444
		public void OnCopySettings(object data)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject != null)
			{
				GeoTuner.Instance smi = gameObject.GetSMI<GeoTuner.Instance>();
				if (smi != null && smi.GetFutureGeyser() != this.GetFutureGeyser())
				{
					Geyser futureGeyser = smi.GetFutureGeyser();
					if (futureGeyser != null && futureGeyser.GetAmountOfGeotunersPointingOrWillPointAtThisGeyser() < 5)
					{
						this.AssignFutureGeyser(smi.GetFutureGeyser());
					}
				}
			}
		}

		// Token: 0x0600934D RID: 37709 RVA: 0x0036A2A4 File Offset: 0x003684A4
		protected override void OnCleanUp()
		{
			Geyser assignedGeyser = this.GetAssignedGeyser();
			Components.GeoTuners.Remove(base.smi.GetMyWorldId(), this);
			if (assignedGeyser != null)
			{
				assignedGeyser.Unsubscribe(-593169791, new Action<object>(base.smi.OnEruptionStateChanged));
			}
			GeoTuner.RemoveTuning(this);
		}

		// Token: 0x04007161 RID: 29025
		[MyCmpReq]
		public Operational operational;

		// Token: 0x04007162 RID: 29026
		[MyCmpReq]
		public Storage storage;

		// Token: 0x04007163 RID: 29027
		[MyCmpReq]
		public ManualDeliveryKG manualDelivery;

		// Token: 0x04007164 RID: 29028
		[MyCmpReq]
		public GeoTunerWorkable workable;

		// Token: 0x04007165 RID: 29029
		[MyCmpReq]
		public GeoTunerSwitchGeyserWorkable switchGeyserWorkable;

		// Token: 0x04007166 RID: 29030
		[MyCmpReq]
		public LogicPorts logicPorts;

		// Token: 0x04007167 RID: 29031
		[MyCmpReq]
		public RoomTracker roomTracker;

		// Token: 0x04007168 RID: 29032
		[MyCmpReq]
		public KBatchedAnimController animController;

		// Token: 0x04007169 RID: 29033
		public MeterController switchGeyserMeter;

		// Token: 0x0400716A RID: 29034
		public string originID;

		// Token: 0x0400716B RID: 29035
		public float enhancementDuration;

		// Token: 0x0400716C RID: 29036
		public Geyser.GeyserModification currentGeyserModification;

		// Token: 0x0400716D RID: 29037
		private Chore switchGeyserChore;
	}
}
