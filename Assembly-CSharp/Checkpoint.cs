using System;
using STRINGS;
using UnityEngine;

// Token: 0x020006EA RID: 1770
public class Checkpoint : StateMachineComponent<Checkpoint.SMInstance>
{
	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000FD51A File Offset: 0x000FB71A
	private bool RedLightDesiredState
	{
		get
		{
			return this.hasLogicWire && !this.hasInputHigh && this.operational.IsOperational;
		}
	}

	// Token: 0x06002BFF RID: 11263 RVA: 0x000FD53C File Offset: 0x000FB73C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Checkpoint>(-801688580, Checkpoint.OnLogicValueChangedDelegate);
		base.Subscribe<Checkpoint>(-592767678, Checkpoint.OnOperationalChangedDelegate);
		base.smi.StartSM();
		if (Checkpoint.infoStatusItem_Logic == null)
		{
			Checkpoint.infoStatusItem_Logic = new StatusItem("CheckpointLogic", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			Checkpoint.infoStatusItem_Logic.resolveStringCallback = new Func<string, object, string>(Checkpoint.ResolveInfoStatusItem_Logic);
		}
		this.Refresh(this.redLight);
	}

	// Token: 0x06002C00 RID: 11264 RVA: 0x000FD5CD File Offset: 0x000FB7CD
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.ClearReactable();
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x000FD5DB File Offset: 0x000FB7DB
	public void RefreshLight()
	{
		if (this.redLight != this.RedLightDesiredState)
		{
			this.Refresh(this.RedLightDesiredState);
			this.statusDirty = true;
		}
		if (this.statusDirty)
		{
			this.RefreshStatusItem();
		}
	}

	// Token: 0x06002C02 RID: 11266 RVA: 0x000FD60C File Offset: 0x000FB80C
	private LogicCircuitNetwork GetNetwork()
	{
		int portCell = base.GetComponent<LogicPorts>().GetPortCell(Checkpoint.PORT_ID);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
	}

	// Token: 0x06002C03 RID: 11267 RVA: 0x000FD63A File Offset: 0x000FB83A
	private static string ResolveInfoStatusItem_Logic(string format_str, object data)
	{
		return ((Checkpoint)data).RedLight ? BUILDING.STATUSITEMS.CHECKPOINT.LOGIC_CONTROLLED_CLOSED : BUILDING.STATUSITEMS.CHECKPOINT.LOGIC_CONTROLLED_OPEN;
	}

	// Token: 0x06002C04 RID: 11268 RVA: 0x000FD65A File Offset: 0x000FB85A
	private void CreateNewReactable()
	{
		if (this.reactable == null)
		{
			this.reactable = new Checkpoint.CheckpointReactable(this);
		}
	}

	// Token: 0x06002C05 RID: 11269 RVA: 0x000FD670 File Offset: 0x000FB870
	private void OrphanReactable()
	{
		this.reactable = null;
	}

	// Token: 0x06002C06 RID: 11270 RVA: 0x000FD679 File Offset: 0x000FB879
	private void ClearReactable()
	{
		if (this.reactable != null)
		{
			this.reactable.Cleanup();
			this.reactable = null;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000FD695 File Offset: 0x000FB895
	public bool RedLight
	{
		get
		{
			return this.redLight;
		}
	}

	// Token: 0x06002C08 RID: 11272 RVA: 0x000FD6A0 File Offset: 0x000FB8A0
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == Checkpoint.PORT_ID)
		{
			this.hasInputHigh = LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue);
			this.hasLogicWire = this.GetNetwork() != null;
			this.statusDirty = true;
		}
	}

	// Token: 0x06002C09 RID: 11273 RVA: 0x000FD6EE File Offset: 0x000FB8EE
	private void OnOperationalChanged(object data)
	{
		this.statusDirty = true;
	}

	// Token: 0x06002C0A RID: 11274 RVA: 0x000FD6F8 File Offset: 0x000FB8F8
	private void RefreshStatusItem()
	{
		bool flag = this.operational.IsOperational && this.hasLogicWire;
		this.selectable.ToggleStatusItem(Checkpoint.infoStatusItem_Logic, flag, this);
		this.statusDirty = false;
	}

	// Token: 0x06002C0B RID: 11275 RVA: 0x000FD738 File Offset: 0x000FB938
	private void Refresh(bool redLightState)
	{
		this.redLight = redLightState;
		this.operational.SetActive(this.operational.IsOperational && this.redLight, false);
		base.smi.sm.redLight.Set(this.redLight, base.smi, false);
		if (this.redLight)
		{
			this.CreateNewReactable();
			return;
		}
		this.ClearReactable();
	}

	// Token: 0x040019FB RID: 6651
	[MyCmpReq]
	public Operational operational;

	// Token: 0x040019FC RID: 6652
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040019FD RID: 6653
	private static StatusItem infoStatusItem_Logic;

	// Token: 0x040019FE RID: 6654
	private Checkpoint.CheckpointReactable reactable;

	// Token: 0x040019FF RID: 6655
	public static readonly HashedString PORT_ID = "Checkpoint";

	// Token: 0x04001A00 RID: 6656
	private bool hasLogicWire;

	// Token: 0x04001A01 RID: 6657
	private bool hasInputHigh;

	// Token: 0x04001A02 RID: 6658
	private bool redLight;

	// Token: 0x04001A03 RID: 6659
	private bool statusDirty = true;

	// Token: 0x04001A04 RID: 6660
	private static readonly EventSystem.IntraObjectHandler<Checkpoint> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<Checkpoint>(delegate(Checkpoint component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001A05 RID: 6661
	private static readonly EventSystem.IntraObjectHandler<Checkpoint> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<Checkpoint>(delegate(Checkpoint component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x02001573 RID: 5491
	private class CheckpointReactable : Reactable
	{
		// Token: 0x06009143 RID: 37187 RVA: 0x00363004 File Offset: 0x00361204
		public CheckpointReactable(Checkpoint checkpoint)
			: base(checkpoint.gameObject, "CheckpointReactable", Db.Get().ChoreTypes.Checkpoint, 1, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
		{
			this.checkpoint = checkpoint;
			this.rotated = this.gameObject.GetComponent<Rotatable>().IsRotated;
			this.preventChoreInterruption = false;
		}

		// Token: 0x06009144 RID: 37188 RVA: 0x00363074 File Offset: 0x00361274
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (this.reactor != null)
			{
				return false;
			}
			if (this.checkpoint == null)
			{
				base.Cleanup();
				return false;
			}
			if (!this.checkpoint.RedLight)
			{
				return false;
			}
			if (this.rotated)
			{
				return transition.x < 0;
			}
			return transition.x > 0;
		}

		// Token: 0x06009145 RID: 37189 RVA: 0x003630D4 File Offset: 0x003612D4
		protected override void InternalBegin()
		{
			this.reactor_navigator = this.reactor.GetComponent<Navigator>();
			KBatchedAnimController component = this.reactor.GetComponent<KBatchedAnimController>();
			component.AddAnimOverrides(Assets.GetAnim("anim_idle_distracted_kanim"), 1f);
			component.Play("idle_pre", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
			this.checkpoint.OrphanReactable();
			this.checkpoint.CreateNewReactable();
		}

		// Token: 0x06009146 RID: 37190 RVA: 0x00363164 File Offset: 0x00361364
		public override void Update(float dt)
		{
			if (this.checkpoint == null || !this.checkpoint.RedLight || this.reactor_navigator == null)
			{
				base.Cleanup();
				return;
			}
			this.reactor_navigator.AdvancePath(false);
			if (!this.reactor_navigator.path.IsValid())
			{
				base.Cleanup();
				return;
			}
			NavGrid.Transition nextTransition = this.reactor_navigator.GetNextTransition();
			if (!(this.rotated ? (nextTransition.x < 0) : (nextTransition.x > 0)))
			{
				base.Cleanup();
			}
		}

		// Token: 0x06009147 RID: 37191 RVA: 0x003631F6 File Offset: 0x003613F6
		protected override void InternalEnd()
		{
			if (this.reactor != null)
			{
				this.reactor.GetComponent<KBatchedAnimController>().RemoveAnimOverrides(Assets.GetAnim("anim_idle_distracted_kanim"));
			}
		}

		// Token: 0x06009148 RID: 37192 RVA: 0x00363225 File Offset: 0x00361425
		protected override void InternalCleanup()
		{
		}

		// Token: 0x04006FB5 RID: 28597
		private Checkpoint checkpoint;

		// Token: 0x04006FB6 RID: 28598
		private Navigator reactor_navigator;

		// Token: 0x04006FB7 RID: 28599
		private bool rotated;
	}

	// Token: 0x02001574 RID: 5492
	public class SMInstance : GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.GameInstance
	{
		// Token: 0x06009149 RID: 37193 RVA: 0x00363227 File Offset: 0x00361427
		public SMInstance(Checkpoint master)
			: base(master)
		{
		}
	}

	// Token: 0x02001575 RID: 5493
	public class States : GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint>
	{
		// Token: 0x0600914A RID: 37194 RVA: 0x00363230 File Offset: 0x00361430
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.go;
			this.root.Update("RefreshLight", delegate(Checkpoint.SMInstance smi, float dt)
			{
				smi.master.RefreshLight();
			}, UpdateRate.SIM_200ms, false);
			this.stop.ParamTransition<bool>(this.redLight, this.go, GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.IsFalse).PlayAnim("red_light");
			this.go.ParamTransition<bool>(this.redLight, this.stop, GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.IsTrue).PlayAnim("green_light");
		}

		// Token: 0x04006FB8 RID: 28600
		public StateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.BoolParameter redLight;

		// Token: 0x04006FB9 RID: 28601
		public GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.State stop;

		// Token: 0x04006FBA RID: 28602
		public GameStateMachine<Checkpoint.States, Checkpoint.SMInstance, Checkpoint, object>.State go;
	}
}
