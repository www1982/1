using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AD1 RID: 2769
public class ReusableTrap : GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>
{
	// Token: 0x06005081 RID: 20609 RVA: 0x001D3380 File Offset: 0x001D1580
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.operational;
		this.noOperational.TagTransition(GameTags.Operational, this.operational, false).Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.RefreshLogicOutput)).DefaultState(this.noOperational.idle);
		this.noOperational.idle.EnterTransition(this.noOperational.releasing, new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.Transition.ConditionCallback(ReusableTrap.StorageContainsCritter)).ParamTransition<bool>(this.IsArmed, this.noOperational.disarming, GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.IsTrue).PlayAnim("off");
		this.noOperational.releasing.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.MarkAsUnarmed)).Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.Release)).PlayAnim(new Func<ReusableTrap.Instance, string>(ReusableTrap.GetReleaseAnimationName), KAnim.PlayMode.Once)
			.OnAnimQueueComplete(this.noOperational.idle);
		this.noOperational.disarming.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.MarkAsUnarmed)).PlayAnim("abort_armed").OnAnimQueueComplete(this.noOperational.idle);
		this.operational.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.RefreshLogicOutput)).TagTransition(GameTags.Operational, this.noOperational, true).DefaultState(this.operational.unarmed);
		this.operational.unarmed.ParamTransition<bool>(this.IsArmed, this.operational.armed, GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.IsTrue).EnterTransition(this.operational.capture.idle, new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.Transition.ConditionCallback(ReusableTrap.StorageContainsCritter)).ToggleStatusItem(Db.Get().BuildingStatusItems.TrapNeedsArming, null)
			.PlayAnim("unarmed")
			.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.DisableTrapTrigger))
			.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.StartArmTrapWorkChore))
			.Exit(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.CancelArmTrapWorkChore))
			.WorkableCompleteTransition(new Func<ReusableTrap.Instance, Workable>(ReusableTrap.GetWorkable), this.operational.armed);
		this.operational.armed.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.MarkAsArmed)).EnterTransition(this.operational.capture.idle, new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.Transition.ConditionCallback(ReusableTrap.StorageContainsCritter)).PlayAnim("armed", KAnim.PlayMode.Loop)
			.ToggleStatusItem(Db.Get().BuildingStatusItems.TrapArmed, null)
			.Toggle("Enable/Disable Trap Trigger", new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.EnableTrapTrigger), new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.DisableTrapTrigger))
			.Toggle("Enable/Disable Lure", new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.ActivateLure), new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.DisableLure))
			.EventHandlerTransition(GameHashes.TrapTriggered, this.operational.capture.capturing, new Func<ReusableTrap.Instance, object, bool>(ReusableTrap.HasCritter_OnTrapTriggered));
		this.operational.capture.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.RefreshLogicOutput)).Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.DisableTrapTrigger)).Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.MarkAsUnarmed))
			.ToggleTag(GameTags.Trapped)
			.DefaultState(this.operational.capture.capturing)
			.EventHandlerTransition(GameHashes.OnStorageChange, this.operational.capture.release, new Func<ReusableTrap.Instance, object, bool>(ReusableTrap.OnStorageEmptied));
		this.operational.capture.capturing.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.SetupCapturingAnimations)).Update(new Action<ReusableTrap.Instance, float>(ReusableTrap.OptionalCapturingAnimationUpdate), UpdateRate.RENDER_EVERY_TICK, false).PlayAnim(new Func<ReusableTrap.Instance, string>(ReusableTrap.GetCaptureAnimationName), KAnim.PlayMode.Once)
			.OnAnimQueueComplete(this.operational.capture.idle)
			.Exit(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.UnsetCapturingAnimations));
		this.operational.capture.idle.TriggerOnEnter(GameHashes.TrapCaptureCompleted, null).ToggleStatusItem(Db.Get().BuildingStatusItems.TrapHasCritter, (ReusableTrap.Instance smi) => smi.CapturedCritter).PlayAnim(new Func<ReusableTrap.Instance, string>(ReusableTrap.GetIdleAnimationName), KAnim.PlayMode.Once);
		this.operational.capture.release.Enter(new StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State.Callback(ReusableTrap.RefreshLogicOutput)).QueueAnim(new Func<ReusableTrap.Instance, string>(ReusableTrap.GetReleaseAnimationName), false, null).OnAnimQueueComplete(this.operational.unarmed);
	}

	// Token: 0x06005082 RID: 20610 RVA: 0x001D37F7 File Offset: 0x001D19F7
	public static void RefreshLogicOutput(ReusableTrap.Instance smi)
	{
		smi.RefreshLogicOutput();
	}

	// Token: 0x06005083 RID: 20611 RVA: 0x001D37FF File Offset: 0x001D19FF
	public static void Release(ReusableTrap.Instance smi)
	{
		smi.Release();
	}

	// Token: 0x06005084 RID: 20612 RVA: 0x001D3807 File Offset: 0x001D1A07
	public static void StartArmTrapWorkChore(ReusableTrap.Instance smi)
	{
		smi.CreateWorkableChore();
	}

	// Token: 0x06005085 RID: 20613 RVA: 0x001D380F File Offset: 0x001D1A0F
	public static void CancelArmTrapWorkChore(ReusableTrap.Instance smi)
	{
		smi.CancelWorkChore();
	}

	// Token: 0x06005086 RID: 20614 RVA: 0x001D3817 File Offset: 0x001D1A17
	public static string GetIdleAnimationName(ReusableTrap.Instance smi)
	{
		if (!smi.IsCapturingLargeCritter)
		{
			return "capture_idle";
		}
		return "capture_idle_large";
	}

	// Token: 0x06005087 RID: 20615 RVA: 0x001D382C File Offset: 0x001D1A2C
	public static string GetCaptureAnimationName(ReusableTrap.Instance smi)
	{
		if (!smi.IsCapturingLargeCritter)
		{
			return "capture";
		}
		return "capture_large";
	}

	// Token: 0x06005088 RID: 20616 RVA: 0x001D3841 File Offset: 0x001D1A41
	public static string GetReleaseAnimationName(ReusableTrap.Instance smi)
	{
		if (!smi.WasLastCritterLarge)
		{
			return "release";
		}
		return "release_large";
	}

	// Token: 0x06005089 RID: 20617 RVA: 0x001D3856 File Offset: 0x001D1A56
	public static bool OnStorageEmptied(ReusableTrap.Instance smi, object obj)
	{
		return !smi.HasCritter;
	}

	// Token: 0x0600508A RID: 20618 RVA: 0x001D3861 File Offset: 0x001D1A61
	public static bool HasCritter_OnTrapTriggered(ReusableTrap.Instance smi, object capturedItem)
	{
		return smi.HasCritter;
	}

	// Token: 0x0600508B RID: 20619 RVA: 0x001D3869 File Offset: 0x001D1A69
	public static bool StorageContainsCritter(ReusableTrap.Instance smi)
	{
		return smi.HasCritter;
	}

	// Token: 0x0600508C RID: 20620 RVA: 0x001D3871 File Offset: 0x001D1A71
	public static bool StorageIsEmpty(ReusableTrap.Instance smi)
	{
		return !smi.HasCritter;
	}

	// Token: 0x0600508D RID: 20621 RVA: 0x001D387C File Offset: 0x001D1A7C
	public static void EnableTrapTrigger(ReusableTrap.Instance smi)
	{
		smi.SetTrapTriggerActiveState(true);
	}

	// Token: 0x0600508E RID: 20622 RVA: 0x001D3885 File Offset: 0x001D1A85
	public static void DisableTrapTrigger(ReusableTrap.Instance smi)
	{
		smi.SetTrapTriggerActiveState(false);
	}

	// Token: 0x0600508F RID: 20623 RVA: 0x001D388E File Offset: 0x001D1A8E
	public static ArmTrapWorkable GetWorkable(ReusableTrap.Instance smi)
	{
		return smi.GetWorkable();
	}

	// Token: 0x06005090 RID: 20624 RVA: 0x001D3896 File Offset: 0x001D1A96
	public static void ActivateLure(ReusableTrap.Instance smi)
	{
		smi.SetLureActiveState(true);
	}

	// Token: 0x06005091 RID: 20625 RVA: 0x001D389F File Offset: 0x001D1A9F
	public static void DisableLure(ReusableTrap.Instance smi)
	{
		smi.SetLureActiveState(false);
	}

	// Token: 0x06005092 RID: 20626 RVA: 0x001D38A8 File Offset: 0x001D1AA8
	public static void SetupCapturingAnimations(ReusableTrap.Instance smi)
	{
		smi.SetupCapturingAnimations();
	}

	// Token: 0x06005093 RID: 20627 RVA: 0x001D38B0 File Offset: 0x001D1AB0
	public static void UnsetCapturingAnimations(ReusableTrap.Instance smi)
	{
		smi.UnsetCapturingAnimations();
	}

	// Token: 0x06005094 RID: 20628 RVA: 0x001D38B8 File Offset: 0x001D1AB8
	public static void OptionalCapturingAnimationUpdate(ReusableTrap.Instance smi, float dt)
	{
		if (smi.def.usingSymbolChaseCapturingAnimations && smi.lastCritterCapturedAnimController != null)
		{
			if (smi.lastCritterCapturedAnimController.currentAnim != smi.CAPTURING_CRITTER_ANIMATION_NAME)
			{
				smi.lastCritterCapturedAnimController.Play(smi.CAPTURING_CRITTER_ANIMATION_NAME, KAnim.PlayMode.Once, 1f, 0f);
			}
			bool flag;
			Vector3 vector = smi.animController.GetSymbolTransform(smi.CAPTURING_SYMBOL_NAME, out flag).GetColumn(3);
			smi.lastCritterCapturedAnimController.transform.SetPosition(vector);
		}
	}

	// Token: 0x06005095 RID: 20629 RVA: 0x001D395A File Offset: 0x001D1B5A
	public static void MarkAsArmed(ReusableTrap.Instance smi)
	{
		smi.sm.IsArmed.Set(true, smi, false);
		smi.gameObject.AddTag(GameTags.TrapArmed);
	}

	// Token: 0x06005096 RID: 20630 RVA: 0x001D3980 File Offset: 0x001D1B80
	public static void MarkAsUnarmed(ReusableTrap.Instance smi)
	{
		smi.sm.IsArmed.Set(false, smi, false);
		smi.gameObject.RemoveTag(GameTags.TrapArmed);
	}

	// Token: 0x04003624 RID: 13860
	public const string CAPTURE_ANIMATION_NAME = "capture";

	// Token: 0x04003625 RID: 13861
	public const string CAPTURE_LARGE_ANIMATION_NAME = "capture_large";

	// Token: 0x04003626 RID: 13862
	public const string CAPTURE_IDLE_ANIMATION_NAME = "capture_idle";

	// Token: 0x04003627 RID: 13863
	public const string CAPTURE_IDLE_LARGE_ANIMATION_NAME = "capture_idle_large";

	// Token: 0x04003628 RID: 13864
	public const string CAPTURE_RELEASE_ANIMATION_NAME = "release";

	// Token: 0x04003629 RID: 13865
	public const string CAPTURE_RELEASE_LARGE_ANIMATION_NAME = "release_large";

	// Token: 0x0400362A RID: 13866
	public const string UNARMED_ANIMATION_NAME = "unarmed";

	// Token: 0x0400362B RID: 13867
	public const string ARMED_ANIMATION_NAME = "armed";

	// Token: 0x0400362C RID: 13868
	public const string ABORT_ARMED_ANIMATION = "abort_armed";

	// Token: 0x0400362D RID: 13869
	public StateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.BoolParameter IsArmed;

	// Token: 0x0400362E RID: 13870
	public ReusableTrap.NonOperationalStates noOperational;

	// Token: 0x0400362F RID: 13871
	public ReusableTrap.OperationalStates operational;

	// Token: 0x02001BA4 RID: 7076
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600A823 RID: 43043 RVA: 0x003B3AC9 File Offset: 0x003B1CC9
		public bool usingLure
		{
			get
			{
				return this.lures != null && this.lures.Length != 0;
			}
		}

		// Token: 0x0400838B RID: 33675
		public string OUTPUT_LOGIC_PORT_ID;

		// Token: 0x0400838C RID: 33676
		public Tag[] lures;

		// Token: 0x0400838D RID: 33677
		public CellOffset releaseCellOffset = CellOffset.none;

		// Token: 0x0400838E RID: 33678
		public bool usingSymbolChaseCapturingAnimations;

		// Token: 0x0400838F RID: 33679
		public Func<string> getTrappedAnimationNameCallback;
	}

	// Token: 0x02001BA5 RID: 7077
	public class CaptureStates : GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State
	{
		// Token: 0x04008390 RID: 33680
		public GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State capturing;

		// Token: 0x04008391 RID: 33681
		public GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State idle;

		// Token: 0x04008392 RID: 33682
		public GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State release;
	}

	// Token: 0x02001BA6 RID: 7078
	public class OperationalStates : GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State
	{
		// Token: 0x04008393 RID: 33683
		public GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State unarmed;

		// Token: 0x04008394 RID: 33684
		public GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State armed;

		// Token: 0x04008395 RID: 33685
		public ReusableTrap.CaptureStates capture;
	}

	// Token: 0x02001BA7 RID: 7079
	public class NonOperationalStates : GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State
	{
		// Token: 0x04008396 RID: 33686
		public GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State idle;

		// Token: 0x04008397 RID: 33687
		public GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State releasing;

		// Token: 0x04008398 RID: 33688
		public GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.State disarming;
	}

	// Token: 0x02001BA8 RID: 7080
	public new class Instance : GameStateMachine<ReusableTrap, ReusableTrap.Instance, IStateMachineTarget, ReusableTrap.Def>.GameInstance, TrappedStates.ITrapStateAnimationInstructions
	{
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600A828 RID: 43048 RVA: 0x003B3B0A File Offset: 0x003B1D0A
		public bool IsCapturingLargeCritter
		{
			get
			{
				return this.HasCritter && this.CapturedCritter.HasTag(GameTags.LargeCreature);
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600A829 RID: 43049 RVA: 0x003B3B26 File Offset: 0x003B1D26
		public bool HasCritter
		{
			get
			{
				return !this.storage.IsEmpty();
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600A82A RID: 43050 RVA: 0x003B3B36 File Offset: 0x003B1D36
		public GameObject CapturedCritter
		{
			get
			{
				if (!this.HasCritter)
				{
					return null;
				}
				return this.storage.items[0];
			}
		}

		// Token: 0x0600A82B RID: 43051 RVA: 0x003B3B53 File Offset: 0x003B1D53
		public ArmTrapWorkable GetWorkable()
		{
			return this.workable;
		}

		// Token: 0x0600A82C RID: 43052 RVA: 0x003B3B5C File Offset: 0x003B1D5C
		public void RefreshLogicOutput()
		{
			bool flag = base.IsInsideState(base.sm.operational) && this.HasCritter;
			this.logicPorts.SendSignal(base.def.OUTPUT_LOGIC_PORT_ID, flag ? 1 : 0);
		}

		// Token: 0x0600A82D RID: 43053 RVA: 0x003B3BA8 File Offset: 0x003B1DA8
		public Instance(IStateMachineTarget master, ReusableTrap.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A82E RID: 43054 RVA: 0x003B3BC8 File Offset: 0x003B1DC8
		public override void StartSM()
		{
			base.StartSM();
			if (this.HasCritter)
			{
				this.WasLastCritterLarge = this.IsCapturingLargeCritter;
			}
			ArmTrapWorkable armTrapWorkable = this.workable;
			armTrapWorkable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(armTrapWorkable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkEvent));
		}

		// Token: 0x0600A82F RID: 43055 RVA: 0x003B3C18 File Offset: 0x003B1E18
		private void OnWorkEvent(Workable workable, Workable.WorkableEvent state)
		{
			if (state == Workable.WorkableEvent.WorkStopped && workable.GetPercentComplete() < 1f && workable.GetPercentComplete() != 0f && base.IsInsideState(base.sm.operational.unarmed))
			{
				this.animController.Play("unarmed", KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x0600A830 RID: 43056 RVA: 0x003B3C7B File Offset: 0x003B1E7B
		public void SetTrapTriggerActiveState(bool active)
		{
			this.trapTrigger.enabled = active;
		}

		// Token: 0x0600A831 RID: 43057 RVA: 0x003B3C8C File Offset: 0x003B1E8C
		public void SetLureActiveState(bool activate)
		{
			if (base.def.usingLure)
			{
				Lure.Instance smi = base.gameObject.GetSMI<Lure.Instance>();
				if (smi != null)
				{
					smi.SetActiveLures(activate ? base.def.lures : null);
				}
			}
		}

		// Token: 0x0600A832 RID: 43058 RVA: 0x003B3CCC File Offset: 0x003B1ECC
		public void SetupCapturingAnimations()
		{
			if (this.HasCritter)
			{
				this.WasLastCritterLarge = this.IsCapturingLargeCritter;
				this.lastCritterCapturedAnimController = this.CapturedCritter.GetComponent<KBatchedAnimController>();
			}
		}

		// Token: 0x0600A833 RID: 43059 RVA: 0x003B3CF4 File Offset: 0x003B1EF4
		public void UnsetCapturingAnimations()
		{
			this.trapTrigger.SetStoredPosition(this.CapturedCritter);
			if (base.def.usingSymbolChaseCapturingAnimations && this.lastCritterCapturedAnimController != null)
			{
				this.lastCritterCapturedAnimController.Play("trapped", KAnim.PlayMode.Loop, 1f, 0f);
			}
			this.lastCritterCapturedAnimController = null;
		}

		// Token: 0x0600A834 RID: 43060 RVA: 0x003B3D54 File Offset: 0x003B1F54
		public void CreateWorkableChore()
		{
			if (this.chore == null)
			{
				this.chore = new WorkChore<ArmTrapWorkable>(Db.Get().ChoreTypes.ArmTrap, this.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			}
		}

		// Token: 0x0600A835 RID: 43061 RVA: 0x003B3D9A File Offset: 0x003B1F9A
		public void CancelWorkChore()
		{
			if (this.chore != null)
			{
				this.chore.Cancel("GroundTrap.CancelChore");
				this.chore = null;
			}
		}

		// Token: 0x0600A836 RID: 43062 RVA: 0x003B3DBC File Offset: 0x003B1FBC
		public void Release()
		{
			if (this.HasCritter)
			{
				this.WasLastCritterLarge = this.IsCapturingLargeCritter;
				Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(base.smi.transform.GetPosition()), base.def.releaseCellOffset), Grid.SceneLayer.Creatures);
				List<GameObject> list = new List<GameObject>();
				Storage storage = this.storage;
				bool flag = false;
				bool flag2 = false;
				List<GameObject> list2 = list;
				storage.DropAll(flag, flag2, default(Vector3), true, list2);
				foreach (GameObject gameObject in list)
				{
					gameObject.transform.SetPosition(vector);
					KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
					if (component != null)
					{
						component.SetSceneLayer(Grid.SceneLayer.Creatures);
					}
				}
			}
		}

		// Token: 0x0600A837 RID: 43063 RVA: 0x003B3E90 File Offset: 0x003B2090
		public string GetTrappedAnimationName()
		{
			if (base.def.getTrappedAnimationNameCallback != null)
			{
				return base.def.getTrappedAnimationNameCallback();
			}
			return null;
		}

		// Token: 0x04008399 RID: 33689
		public string CAPTURING_CRITTER_ANIMATION_NAME = "caught_loop";

		// Token: 0x0400839A RID: 33690
		public string CAPTURING_SYMBOL_NAME = "creatureSymbol";

		// Token: 0x0400839B RID: 33691
		[MyCmpGet]
		private Storage storage;

		// Token: 0x0400839C RID: 33692
		[MyCmpGet]
		private ArmTrapWorkable workable;

		// Token: 0x0400839D RID: 33693
		[MyCmpGet]
		private TrapTrigger trapTrigger;

		// Token: 0x0400839E RID: 33694
		[MyCmpGet]
		public KBatchedAnimController animController;

		// Token: 0x0400839F RID: 33695
		[MyCmpGet]
		public LogicPorts logicPorts;

		// Token: 0x040083A0 RID: 33696
		public bool WasLastCritterLarge;

		// Token: 0x040083A1 RID: 33697
		public KBatchedAnimController lastCritterCapturedAnimController;

		// Token: 0x040083A2 RID: 33698
		private Chore chore;
	}
}
