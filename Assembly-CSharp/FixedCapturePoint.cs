using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005B9 RID: 1465
public class FixedCapturePoint : GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>
{
	// Token: 0x060021C9 RID: 8649 RVA: 0x000C38F8 File Offset: 0x000C1AF8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.operational;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.unoperational.TagTransition(GameTags.Operational, this.operational, false);
		this.operational.DefaultState(this.operational.manual).TagTransition(GameTags.Operational, this.unoperational, true);
		this.operational.manual.ParamTransition<bool>(this.automated, this.operational.automated, GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.IsTrue);
		this.operational.automated.ParamTransition<bool>(this.automated, this.operational.manual, GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.IsFalse).ToggleChore((FixedCapturePoint.Instance smi) => smi.CreateChore(), this.unoperational, this.unoperational).Update("FindFixedCapturable", delegate(FixedCapturePoint.Instance smi, float dt)
		{
			smi.FindFixedCapturable();
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x040013BC RID: 5052
	public static readonly Operational.Flag enabledFlag = new Operational.Flag("enabled", Operational.Flag.Type.Requirement);

	// Token: 0x040013BD RID: 5053
	private StateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.BoolParameter automated;

	// Token: 0x040013BE RID: 5054
	public GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.State unoperational;

	// Token: 0x040013BF RID: 5055
	public FixedCapturePoint.OperationalState operational;

	// Token: 0x02001456 RID: 5206
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006C4F RID: 27727
		public Func<FixedCapturePoint.Instance, FixedCapturableMonitor.Instance, bool> isAmountStoredOverCapacity;

		// Token: 0x04006C50 RID: 27728
		public Func<FixedCapturePoint.Instance, int> getTargetCapturePoint = delegate(FixedCapturePoint.Instance smi)
		{
			int num = Grid.PosToCell(smi);
			Navigator navigator = smi.targetCapturable.Navigator;
			if (Grid.IsValidCell(num - 1) && navigator.CanReach(num - 1))
			{
				return num - 1;
			}
			if (Grid.IsValidCell(num + 1) && navigator.CanReach(num + 1))
			{
				return num + 1;
			}
			return num;
		};

		// Token: 0x04006C51 RID: 27729
		public bool allowBabies;
	}

	// Token: 0x02001457 RID: 5207
	public class OperationalState : GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.State
	{
		// Token: 0x04006C52 RID: 27730
		public GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.State manual;

		// Token: 0x04006C53 RID: 27731
		public GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.State automated;
	}

	// Token: 0x02001458 RID: 5208
	[SerializationConfig(MemberSerialization.OptIn)]
	public new class Instance : GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.GameInstance
	{
		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06008D63 RID: 36195 RVA: 0x0035873C File Offset: 0x0035693C
		// (set) Token: 0x06008D64 RID: 36196 RVA: 0x00358744 File Offset: 0x00356944
		public FixedCapturableMonitor.Instance targetCapturable { get; private set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06008D65 RID: 36197 RVA: 0x0035874D File Offset: 0x0035694D
		// (set) Token: 0x06008D66 RID: 36198 RVA: 0x00358755 File Offset: 0x00356955
		public bool shouldCreatureGoGetCaptured { get; private set; }

		// Token: 0x06008D67 RID: 36199 RVA: 0x00358760 File Offset: 0x00356960
		public Instance(IStateMachineTarget master, FixedCapturePoint.Def def)
			: base(master, def)
		{
			base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
			this.captureCell = Grid.PosToCell(base.transform.GetPosition());
			this.critterCapactiy = base.GetComponent<BaggableCritterCapacityTracker>();
			this.operationComp = base.GetComponent<Operational>();
			this.logicPorts = base.GetComponent<LogicPorts>();
			if (this.logicPorts != null)
			{
				base.Subscribe(-801688580, new Action<object>(this.OnLogicEvent));
				this.operationComp.SetFlag(FixedCapturePoint.enabledFlag, !this.logicPorts.IsPortConnected("CritterPickUpInput") || this.logicPorts.GetInputValue("CritterPickUpInput") > 0);
				return;
			}
			this.operationComp.SetFlag(FixedCapturePoint.enabledFlag, true);
		}

		// Token: 0x06008D68 RID: 36200 RVA: 0x00358840 File Offset: 0x00356A40
		private void OnLogicEvent(object data)
		{
			LogicValueChanged logicValueChanged = (LogicValueChanged)data;
			if (logicValueChanged.portID == "CritterPickUpInput" && this.logicPorts.IsPortConnected("CritterPickUpInput"))
			{
				this.operationComp.SetFlag(FixedCapturePoint.enabledFlag, logicValueChanged.newValue > 0);
			}
		}

		// Token: 0x06008D69 RID: 36201 RVA: 0x0035889B File Offset: 0x00356A9B
		public override void StartSM()
		{
			base.StartSM();
			if (base.GetComponent<FixedCapturePoint.AutoWrangleCapture>() == null)
			{
				base.sm.automated.Set(true, this, false);
			}
		}

		// Token: 0x06008D6A RID: 36202 RVA: 0x003588C8 File Offset: 0x00356AC8
		private void OnCopySettings(object data)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject == null)
			{
				return;
			}
			FixedCapturePoint.Instance smi = gameObject.GetSMI<FixedCapturePoint.Instance>();
			if (smi == null)
			{
				return;
			}
			base.sm.automated.Set(base.sm.automated.Get(smi), this, false);
		}

		// Token: 0x06008D6B RID: 36203 RVA: 0x00358915 File Offset: 0x00356B15
		public bool GetAutomated()
		{
			return base.sm.automated.Get(this);
		}

		// Token: 0x06008D6C RID: 36204 RVA: 0x00358928 File Offset: 0x00356B28
		public void SetAutomated(bool automate)
		{
			base.sm.automated.Set(automate, this, false);
		}

		// Token: 0x06008D6D RID: 36205 RVA: 0x0035893E File Offset: 0x00356B3E
		public Chore CreateChore()
		{
			this.FindFixedCapturable();
			return new FixedCaptureChore(base.GetComponent<KPrefabID>());
		}

		// Token: 0x06008D6E RID: 36206 RVA: 0x00358954 File Offset: 0x00356B54
		public bool IsCreatureAvailableForFixedCapture()
		{
			if (!this.targetCapturable.IsNullOrStopped())
			{
				CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(this.captureCell);
				return FixedCapturePoint.Instance.CanCapturableBeCapturedAtCapturePoint(this.targetCapturable, this, cavityForCell, this.captureCell);
			}
			return false;
		}

		// Token: 0x06008D6F RID: 36207 RVA: 0x00358999 File Offset: 0x00356B99
		public void SetRancherIsAvailableForCapturing()
		{
			this.shouldCreatureGoGetCaptured = true;
		}

		// Token: 0x06008D70 RID: 36208 RVA: 0x003589A2 File Offset: 0x00356BA2
		public void ClearRancherIsAvailableForCapturing()
		{
			this.shouldCreatureGoGetCaptured = false;
		}

		// Token: 0x06008D71 RID: 36209 RVA: 0x003589AC File Offset: 0x00356BAC
		private static bool CanCapturableBeCapturedAtCapturePoint(FixedCapturableMonitor.Instance capturable, FixedCapturePoint.Instance capture_point, CavityInfo capture_cavity_info, int capture_cell)
		{
			if (!capturable.IsRunning())
			{
				return false;
			}
			if (capturable.targetCapturePoint != capture_point && !capturable.targetCapturePoint.IsNullOrStopped())
			{
				return false;
			}
			int num = Grid.PosToCell(capturable.transform.GetPosition());
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			return cavityForCell != null && cavityForCell == capture_cavity_info && !capturable.HasTag(GameTags.Creatures.Bagged) && (!capturable.isBaby || capture_point.def.allowBabies) && capturable.ChoreConsumer.IsChoreEqualOrAboveCurrentChorePriority<FixedCaptureStates>() && capturable.Navigator.GetNavigationCost(capture_cell) != -1 && capture_point.def.isAmountStoredOverCapacity(capture_point, capturable);
		}

		// Token: 0x06008D72 RID: 36210 RVA: 0x00358A60 File Offset: 0x00356C60
		public void FindFixedCapturable()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			if (cavityForCell == null)
			{
				this.ResetCapturePoint();
				return;
			}
			if (!this.targetCapturable.IsNullOrStopped() && !FixedCapturePoint.Instance.CanCapturableBeCapturedAtCapturePoint(this.targetCapturable, this, cavityForCell, num))
			{
				this.ResetCapturePoint();
			}
			if (this.targetCapturable.IsNullOrStopped())
			{
				foreach (object obj in Components.FixedCapturableMonitors)
				{
					FixedCapturableMonitor.Instance instance = (FixedCapturableMonitor.Instance)obj;
					if (FixedCapturePoint.Instance.CanCapturableBeCapturedAtCapturePoint(instance, this, cavityForCell, num))
					{
						this.targetCapturable = instance;
						if (!this.targetCapturable.IsNullOrStopped())
						{
							this.targetCapturable.targetCapturePoint = this;
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06008D73 RID: 36211 RVA: 0x00358B40 File Offset: 0x00356D40
		public void ResetCapturePoint()
		{
			base.Trigger(643180843, null);
			if (!this.targetCapturable.IsNullOrStopped())
			{
				this.targetCapturable.targetCapturePoint = null;
				this.targetCapturable.Trigger(1034952693, null);
				this.targetCapturable = null;
			}
		}

		// Token: 0x04006C56 RID: 27734
		public BaggableCritterCapacityTracker critterCapactiy;

		// Token: 0x04006C57 RID: 27735
		private int captureCell;

		// Token: 0x04006C58 RID: 27736
		private Operational operationComp;

		// Token: 0x04006C59 RID: 27737
		private LogicPorts logicPorts;
	}

	// Token: 0x02001459 RID: 5209
	public class AutoWrangleCapture : KMonoBehaviour, ICheckboxControl
	{
		// Token: 0x06008D74 RID: 36212 RVA: 0x00358B7F File Offset: 0x00356D7F
		protected override void OnSpawn()
		{
			base.OnSpawn();
			this.fcp = this.GetSMI<FixedCapturePoint.Instance>();
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06008D75 RID: 36213 RVA: 0x00358B93 File Offset: 0x00356D93
		string ICheckboxControl.CheckboxTitleKey
		{
			get
			{
				return UI.UISIDESCREENS.CAPTURE_POINT_SIDE_SCREEN.TITLE.key.String;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06008D76 RID: 36214 RVA: 0x00358BA4 File Offset: 0x00356DA4
		string ICheckboxControl.CheckboxLabel
		{
			get
			{
				return UI.UISIDESCREENS.CAPTURE_POINT_SIDE_SCREEN.AUTOWRANGLE;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06008D77 RID: 36215 RVA: 0x00358BB0 File Offset: 0x00356DB0
		string ICheckboxControl.CheckboxTooltip
		{
			get
			{
				return UI.UISIDESCREENS.CAPTURE_POINT_SIDE_SCREEN.AUTOWRANGLE_TOOLTIP;
			}
		}

		// Token: 0x06008D78 RID: 36216 RVA: 0x00358BBC File Offset: 0x00356DBC
		bool ICheckboxControl.GetCheckboxValue()
		{
			return this.fcp.GetAutomated();
		}

		// Token: 0x06008D79 RID: 36217 RVA: 0x00358BC9 File Offset: 0x00356DC9
		void ICheckboxControl.SetCheckboxValue(bool value)
		{
			this.fcp.SetAutomated(value);
		}

		// Token: 0x04006C5A RID: 27738
		private FixedCapturePoint.Instance fcp;
	}
}
