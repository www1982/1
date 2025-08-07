using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000719 RID: 1817
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Door")]
public class Door : Workable, ISaveLoadable, ISim200ms, INavDoor
{
	// Token: 0x06002DA1 RID: 11681 RVA: 0x001056C8 File Offset: 0x001038C8
	private void OnCopySettings(object data)
	{
		Door component = ((GameObject)data).GetComponent<Door>();
		if (component != null)
		{
			this.QueueStateChange(component.requestedState);
		}
	}

	// Token: 0x06002DA2 RID: 11682 RVA: 0x001056F6 File Offset: 0x001038F6
	public Door()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06002DA3 RID: 11683 RVA: 0x00105726 File Offset: 0x00103926
	public Door.ControlState CurrentState
	{
		get
		{
			return this.controlState;
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x0010572E File Offset: 0x0010392E
	public Door.ControlState RequestedState
	{
		get
		{
			return this.requestedState;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x00105736 File Offset: 0x00103936
	public bool ShouldBlockFallingSand
	{
		get
		{
			return this.rotatable.GetOrientation() != this.verticalOrientation;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x0010574E File Offset: 0x0010394E
	public bool isSealed
	{
		get
		{
			return this.controller != null && this.controller.sm.isSealed.Get(this.controller);
		}
	}

	// Token: 0x06002DA7 RID: 11687 RVA: 0x00105778 File Offset: 0x00103978
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = Door.OVERRIDE_ANIMS;
		this.synchronizeAnims = false;
		base.SetWorkTime(3f);
		if (!string.IsNullOrEmpty(this.doorClosingSoundEventName))
		{
			this.doorClosingSound = GlobalAssets.GetSound(this.doorClosingSoundEventName, false);
		}
		if (!string.IsNullOrEmpty(this.doorOpeningSoundEventName))
		{
			this.doorOpeningSound = GlobalAssets.GetSound(this.doorOpeningSoundEventName, false);
		}
		base.Subscribe<Door>(-905833192, Door.OnCopySettingsDelegate);
	}

	// Token: 0x06002DA8 RID: 11688 RVA: 0x001057F7 File Offset: 0x001039F7
	private Door.ControlState GetNextState(Door.ControlState wantedState)
	{
		return (wantedState + 1) % Door.ControlState.NumStates;
	}

	// Token: 0x06002DA9 RID: 11689 RVA: 0x001057FE File Offset: 0x001039FE
	private static bool DisplacesGas(Door.DoorType type)
	{
		return type != Door.DoorType.Internal;
	}

	// Token: 0x06002DAA RID: 11690 RVA: 0x00105808 File Offset: 0x00103A08
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (base.GetComponent<KPrefabID>() != null)
		{
			this.log = new LoggerFSS("Door", 35);
		}
		if (!this.allowAutoControl && this.controlState == Door.ControlState.Auto)
		{
			this.controlState = Door.ControlState.Locked;
		}
		StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
		HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
		if (Door.DisplacesGas(this.doorType))
		{
			structureTemperatures.Bypass(handle);
		}
		this.controller = new Door.Controller.Instance(this);
		this.controller.StartSM();
		if (this.doorType == Door.DoorType.Sealed && !this.hasBeenUnsealed)
		{
			this.Seal();
		}
		this.UpdateDoorSpeed(this.operational.IsOperational);
		base.Subscribe<Door>(-592767678, Door.OnOperationalChangedDelegate);
		base.Subscribe<Door>(824508782, Door.OnOperationalChangedDelegate);
		base.Subscribe<Door>(-801688580, Door.OnLogicValueChangedDelegate);
		this.requestedState = this.CurrentState;
		this.ApplyRequestedControlState(true);
		int num = ((this.rotatable.GetOrientation() == Orientation.Neutral) ? (this.building.Def.WidthInCells * (this.building.Def.HeightInCells - 1)) : 0);
		int num2 = ((this.rotatable.GetOrientation() == Orientation.Neutral) ? this.building.Def.WidthInCells : this.building.Def.HeightInCells);
		for (int num3 = 0; num3 != num2; num3++)
		{
			int num4 = this.building.PlacementCells[num + num3];
			Grid.FakeFloor.Add(num4);
			Pathfinding.Instance.AddDirtyNavGridCell(num4);
		}
		List<int> list = new List<int>();
		foreach (int num5 in this.building.PlacementCells)
		{
			Grid.HasDoor[num5] = true;
			if (this.rotatable.IsRotated)
			{
				list.Add(Grid.CellAbove(num5));
				list.Add(Grid.CellBelow(num5));
			}
			else
			{
				list.Add(Grid.CellLeft(num5));
				list.Add(Grid.CellRight(num5));
			}
			SimMessages.SetCellProperties(num5, 8);
			if (Door.DisplacesGas(this.doorType))
			{
				Grid.RenderedByWorld[num5] = false;
			}
		}
	}

	// Token: 0x06002DAB RID: 11691 RVA: 0x00105A48 File Offset: 0x00103C48
	protected override void OnCleanUp()
	{
		this.UpdateDoorState(true);
		List<int> list = new List<int>();
		foreach (int num in this.building.PlacementCells)
		{
			SimMessages.ClearCellProperties(num, 12);
			Grid.RenderedByWorld[num] = Grid.Element[num].substance.renderedByWorld;
			Grid.FakeFloor.Remove(num);
			if (Grid.Element[num].IsSolid)
			{
				SimMessages.ReplaceAndDisplaceElement(num, SimHashes.Vacuum, CellEventLogger.Instance.DoorOpen, 0f, -1f, byte.MaxValue, 0, -1);
			}
			Pathfinding.Instance.AddDirtyNavGridCell(num);
			if (this.rotatable.IsRotated)
			{
				list.Add(Grid.CellAbove(num));
				list.Add(Grid.CellBelow(num));
			}
			else
			{
				list.Add(Grid.CellLeft(num));
				list.Add(Grid.CellRight(num));
			}
		}
		foreach (int num2 in this.building.PlacementCells)
		{
			Grid.HasDoor[num2] = false;
			Game.Instance.SetDupePassableSolid(num2, false, Grid.Solid[num2]);
			Grid.CritterImpassable[num2] = false;
			Grid.DupeImpassable[num2] = false;
			Pathfinding.Instance.AddDirtyNavGridCell(num2);
		}
		base.OnCleanUp();
	}

	// Token: 0x06002DAC RID: 11692 RVA: 0x00105BA4 File Offset: 0x00103DA4
	public void Seal()
	{
		this.controller.sm.isSealed.Set(true, this.controller, false);
	}

	// Token: 0x06002DAD RID: 11693 RVA: 0x00105BC4 File Offset: 0x00103DC4
	public void OrderUnseal()
	{
		this.controller.GoTo(this.controller.sm.Sealed.awaiting_unlock);
	}

	// Token: 0x06002DAE RID: 11694 RVA: 0x00105BE8 File Offset: 0x00103DE8
	private void RefreshControlState()
	{
		switch (this.controlState)
		{
		case Door.ControlState.Auto:
			this.controller.sm.isLocked.Set(false, this.controller, false);
			break;
		case Door.ControlState.Opened:
			this.controller.sm.isLocked.Set(false, this.controller, false);
			break;
		case Door.ControlState.Locked:
			this.controller.sm.isLocked.Set(true, this.controller, false);
			break;
		}
		base.Trigger(279163026, this.controlState);
		this.SetWorldState();
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.CurrentDoorControlState, this);
	}

	// Token: 0x06002DAF RID: 11695 RVA: 0x00105CB8 File Offset: 0x00103EB8
	private void OnOperationalChanged(object data)
	{
		bool isOperational = this.operational.IsOperational;
		if (isOperational != this.on)
		{
			this.UpdateDoorSpeed(isOperational);
			if (this.on && base.GetComponent<KPrefabID>().HasTag(GameTags.Transition))
			{
				this.SetActive(true);
				return;
			}
			this.SetActive(false);
		}
	}

	// Token: 0x06002DB0 RID: 11696 RVA: 0x00105D0C File Offset: 0x00103F0C
	private void UpdateDoorSpeed(bool powered)
	{
		this.on = powered;
		this.UpdateAnimAndSoundParams(powered);
		float positionPercent = this.animController.GetPositionPercent();
		this.animController.Play(this.animController.CurrentAnim.hash, this.animController.PlayMode, 1f, 0f);
		this.animController.SetPositionPercent(positionPercent);
	}

	// Token: 0x06002DB1 RID: 11697 RVA: 0x00105D70 File Offset: 0x00103F70
	private void UpdateAnimAndSoundParams(bool powered)
	{
		if (powered)
		{
			this.animController.PlaySpeedMultiplier = this.poweredAnimSpeed;
			if (this.doorClosingSound != null)
			{
				this.loopingSounds.UpdateFirstParameter(this.doorClosingSound, Door.SOUND_POWERED_PARAMETER, 1f);
			}
			if (this.doorOpeningSound != null)
			{
				this.loopingSounds.UpdateFirstParameter(this.doorOpeningSound, Door.SOUND_POWERED_PARAMETER, 1f);
				return;
			}
		}
		else
		{
			this.animController.PlaySpeedMultiplier = this.unpoweredAnimSpeed;
			if (this.doorClosingSound != null)
			{
				this.loopingSounds.UpdateFirstParameter(this.doorClosingSound, Door.SOUND_POWERED_PARAMETER, 0f);
			}
			if (this.doorOpeningSound != null)
			{
				this.loopingSounds.UpdateFirstParameter(this.doorOpeningSound, Door.SOUND_POWERED_PARAMETER, 0f);
			}
		}
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x00105E2F File Offset: 0x0010402F
	private void SetActive(bool active)
	{
		if (this.operational.IsOperational)
		{
			this.operational.SetActive(active, false);
		}
	}

	// Token: 0x06002DB3 RID: 11699 RVA: 0x00105E4C File Offset: 0x0010404C
	private void SetWorldState()
	{
		int[] placementCells = this.building.PlacementCells;
		bool flag = this.IsOpen();
		this.SetPassableState(flag, placementCells);
		this.SetSimState(flag, placementCells);
	}

	// Token: 0x06002DB4 RID: 11700 RVA: 0x00105E7C File Offset: 0x0010407C
	private void SetPassableState(bool is_door_open, IList<int> cells)
	{
		for (int i = 0; i < cells.Count; i++)
		{
			int num = cells[i];
			switch (this.doorType)
			{
			case Door.DoorType.Pressure:
			case Door.DoorType.ManualPressure:
			case Door.DoorType.Sealed:
			{
				Grid.CritterImpassable[num] = this.controlState != Door.ControlState.Opened;
				bool flag = !is_door_open;
				bool flag2 = this.controlState != Door.ControlState.Locked;
				Game.Instance.SetDupePassableSolid(num, flag2, flag);
				if (this.controlState == Door.ControlState.Opened)
				{
					this.doorOpenLiquidRefreshHack = true;
					this.doorOpenLiquidRefreshTime = 1f;
				}
				break;
			}
			case Door.DoorType.Internal:
				Grid.CritterImpassable[num] = this.controlState != Door.ControlState.Opened;
				Grid.DupeImpassable[num] = this.controlState == Door.ControlState.Locked;
				break;
			}
			Pathfinding.Instance.AddDirtyNavGridCell(num);
		}
	}

	// Token: 0x06002DB5 RID: 11701 RVA: 0x00105F54 File Offset: 0x00104154
	private void SetSimState(bool is_door_open, IList<int> cells)
	{
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		float num = component.Mass / (float)cells.Count;
		for (int i = 0; i < cells.Count; i++)
		{
			int num2 = cells[i];
			Door.DoorType doorType = this.doorType;
			if (doorType <= Door.DoorType.ManualPressure || doorType == Door.DoorType.Sealed)
			{
				World.Instance.groundRenderer.MarkDirty(num2);
				if (is_door_open)
				{
					SimMessages.Dig(num2, Game.Instance.callbackManager.Add(new Game.CallbackInfo(new global::System.Action(this.OnSimDoorOpened), false)).index, true);
					if (this.ShouldBlockFallingSand)
					{
						SimMessages.ClearCellProperties(num2, 4);
					}
					else
					{
						SimMessages.SetCellProperties(num2, 4);
					}
				}
				else
				{
					HandleVector<Game.CallbackInfo>.Handle handle = Game.Instance.callbackManager.Add(new Game.CallbackInfo(new global::System.Action(this.OnSimDoorClosed), false));
					float num3 = component.Temperature;
					if (num3 <= 0f)
					{
						num3 = component.Temperature;
					}
					SimMessages.ReplaceAndDisplaceElement(num2, component.ElementID, CellEventLogger.Instance.DoorClose, num, num3, byte.MaxValue, 0, handle.index);
					SimMessages.SetCellProperties(num2, 4);
				}
			}
		}
	}

	// Token: 0x06002DB6 RID: 11702 RVA: 0x00106074 File Offset: 0x00104274
	private void UpdateDoorState(bool cleaningUp)
	{
		foreach (int num in this.building.PlacementCells)
		{
			if (Grid.IsValidCell(num))
			{
				Grid.Foundation[num] = !cleaningUp;
			}
		}
	}

	// Token: 0x06002DB7 RID: 11703 RVA: 0x001060B8 File Offset: 0x001042B8
	public void QueueStateChange(Door.ControlState nextState)
	{
		if (this.requestedState != nextState)
		{
			this.requestedState = nextState;
		}
		else
		{
			this.requestedState = this.controlState;
		}
		if (this.requestedState == this.controlState)
		{
			if (this.changeStateChore != null)
			{
				this.changeStateChore.Cancel("Change state");
				this.changeStateChore = null;
				base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ChangeDoorControlState, false);
			}
			return;
		}
		if (DebugHandler.InstantBuildMode)
		{
			this.controlState = this.requestedState;
			this.RefreshControlState();
			this.OnOperationalChanged(null);
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ChangeDoorControlState, false);
			this.Open();
			this.Close();
			return;
		}
		if (this.changeStateChore != null)
		{
			this.changeStateChore.Cancel("Change state");
		}
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ChangeDoorControlState, this);
		this.changeStateChore = new WorkChore<Door>(Db.Get().ChoreTypes.Toggle, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x06002DB8 RID: 11704 RVA: 0x001061D8 File Offset: 0x001043D8
	private void OnSimDoorOpened()
	{
		if (this == null || !Door.DisplacesGas(this.doorType))
		{
			return;
		}
		StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
		HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
		structureTemperatures.UnBypass(handle);
		this.do_melt_check = false;
	}

	// Token: 0x06002DB9 RID: 11705 RVA: 0x0010621C File Offset: 0x0010441C
	private void OnSimDoorClosed()
	{
		if (this == null || !Door.DisplacesGas(this.doorType))
		{
			return;
		}
		StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
		HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
		structureTemperatures.Bypass(handle);
		this.do_melt_check = true;
	}

	// Token: 0x06002DBA RID: 11706 RVA: 0x0010625F File Offset: 0x0010445F
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		this.changeStateChore = null;
		this.ApplyRequestedControlState(false);
	}

	// Token: 0x06002DBB RID: 11707 RVA: 0x00106278 File Offset: 0x00104478
	public void Open()
	{
		if (this.openCount == 0 && Door.DisplacesGas(this.doorType))
		{
			StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
			HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
			if (handle.IsValid() && structureTemperatures.IsBypassed(handle))
			{
				int[] placementCells = this.building.PlacementCells;
				float num = 0f;
				int num2 = 0;
				foreach (int num3 in placementCells)
				{
					if (Grid.Mass[num3] > 0f)
					{
						num2++;
						num += Grid.Temperature[num3];
					}
				}
				if (num2 > 0)
				{
					num /= (float)placementCells.Length;
					PrimaryElement component = base.GetComponent<PrimaryElement>();
					KCrashReporter.Assert(num > 0f, "Door has calculated an invalid temperature", null);
					component.Temperature = num;
				}
			}
		}
		this.openCount++;
		Door.ControlState controlState = this.controlState;
		if (controlState > Door.ControlState.Opened)
		{
			return;
		}
		this.controller.sm.isOpen.Set(true, this.controller, false);
	}

	// Token: 0x06002DBC RID: 11708 RVA: 0x0010638C File Offset: 0x0010458C
	public void Close()
	{
		this.openCount = Mathf.Max(0, this.openCount - 1);
		if (this.openCount == 0 && Door.DisplacesGas(this.doorType))
		{
			StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
			HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
			PrimaryElement component = base.GetComponent<PrimaryElement>();
			if (handle.IsValid() && !structureTemperatures.IsBypassed(handle))
			{
				float temperature = structureTemperatures.GetPayload(handle).Temperature;
				component.Temperature = temperature;
			}
		}
		switch (this.controlState)
		{
		case Door.ControlState.Auto:
			if (this.openCount == 0)
			{
				this.controller.sm.isOpen.Set(false, this.controller, false);
				Game.Instance.userMenu.Refresh(base.gameObject);
			}
			break;
		case Door.ControlState.Opened:
			break;
		case Door.ControlState.Locked:
			this.controller.sm.isOpen.Set(false, this.controller, false);
			return;
		default:
			return;
		}
	}

	// Token: 0x06002DBD RID: 11709 RVA: 0x0010647D File Offset: 0x0010467D
	public bool IsPendingClose()
	{
		return this.controller.IsInsideState(this.controller.sm.closedelay);
	}

	// Token: 0x06002DBE RID: 11710 RVA: 0x0010649C File Offset: 0x0010469C
	public bool IsOpen()
	{
		return this.controller.IsInsideState(this.controller.sm.open) || this.controller.IsInsideState(this.controller.sm.closedelay) || this.controller.IsInsideState(this.controller.sm.closeblocked);
	}

	// Token: 0x06002DBF RID: 11711 RVA: 0x00106500 File Offset: 0x00104700
	private void ApplyRequestedControlState(bool force = false)
	{
		if (this.requestedState == this.controlState && !force)
		{
			return;
		}
		this.controlState = this.requestedState;
		this.RefreshControlState();
		this.OnOperationalChanged(null);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ChangeDoorControlState, false);
		base.Trigger(1734268753, this);
		if (!force)
		{
			this.Open();
			this.Close();
		}
	}

	// Token: 0x06002DC0 RID: 11712 RVA: 0x00106570 File Offset: 0x00104770
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID != Door.OPEN_CLOSE_PORT_ID)
		{
			return;
		}
		int newValue = logicValueChanged.newValue;
		if (this.changeStateChore != null)
		{
			this.changeStateChore.Cancel("Change state");
			this.changeStateChore = null;
		}
		this.requestedState = (LogicCircuitNetwork.IsBitActive(0, newValue) ? Door.ControlState.Opened : Door.ControlState.Locked);
		this.applyLogicChange = true;
	}

	// Token: 0x06002DC1 RID: 11713 RVA: 0x001065DC File Offset: 0x001047DC
	public void Sim200ms(float dt)
	{
		if (this == null)
		{
			return;
		}
		if (this.doorOpenLiquidRefreshHack)
		{
			this.doorOpenLiquidRefreshTime -= dt;
			if (this.doorOpenLiquidRefreshTime <= 0f)
			{
				this.doorOpenLiquidRefreshHack = false;
				foreach (int num in this.building.PlacementCells)
				{
					Pathfinding.Instance.AddDirtyNavGridCell(num);
				}
			}
		}
		if (this.applyLogicChange)
		{
			this.applyLogicChange = false;
			this.ApplyRequestedControlState(false);
		}
		if (this.do_melt_check)
		{
			StructureTemperatureComponents structureTemperatures = GameComps.StructureTemperatures;
			HandleVector<int>.Handle handle = structureTemperatures.GetHandle(base.gameObject);
			if (handle.IsValid() && structureTemperatures.IsBypassed(handle))
			{
				foreach (int num2 in this.building.PlacementCells)
				{
					if (!Grid.Solid[num2])
					{
						Util.KDestroyGameObject(this);
						return;
					}
				}
			}
		}
	}

	// Token: 0x06002DC3 RID: 11715 RVA: 0x00106765 File Offset: 0x00104965
	bool INavDoor.get_isSpawned()
	{
		return base.isSpawned;
	}

	// Token: 0x04001AD5 RID: 6869
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001AD6 RID: 6870
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001AD7 RID: 6871
	[MyCmpReq]
	private KBatchedAnimController animController;

	// Token: 0x04001AD8 RID: 6872
	[MyCmpReq]
	public Building building;

	// Token: 0x04001AD9 RID: 6873
	[MyCmpGet]
	private EnergyConsumer consumer;

	// Token: 0x04001ADA RID: 6874
	[MyCmpAdd]
	private LoopingSounds loopingSounds;

	// Token: 0x04001ADB RID: 6875
	public Orientation verticalOrientation;

	// Token: 0x04001ADC RID: 6876
	[SerializeField]
	public bool hasComplexUserControls;

	// Token: 0x04001ADD RID: 6877
	[SerializeField]
	public float unpoweredAnimSpeed = 0.25f;

	// Token: 0x04001ADE RID: 6878
	[SerializeField]
	public float poweredAnimSpeed = 1f;

	// Token: 0x04001ADF RID: 6879
	[SerializeField]
	public Door.DoorType doorType;

	// Token: 0x04001AE0 RID: 6880
	[SerializeField]
	public bool allowAutoControl = true;

	// Token: 0x04001AE1 RID: 6881
	[SerializeField]
	public string doorClosingSoundEventName;

	// Token: 0x04001AE2 RID: 6882
	[SerializeField]
	public string doorOpeningSoundEventName;

	// Token: 0x04001AE3 RID: 6883
	private string doorClosingSound;

	// Token: 0x04001AE4 RID: 6884
	private string doorOpeningSound;

	// Token: 0x04001AE5 RID: 6885
	private static readonly HashedString SOUND_POWERED_PARAMETER = "doorPowered";

	// Token: 0x04001AE6 RID: 6886
	private static readonly HashedString SOUND_PROGRESS_PARAMETER = "doorProgress";

	// Token: 0x04001AE7 RID: 6887
	[Serialize]
	private bool hasBeenUnsealed;

	// Token: 0x04001AE8 RID: 6888
	[Serialize]
	private Door.ControlState controlState;

	// Token: 0x04001AE9 RID: 6889
	private bool on;

	// Token: 0x04001AEA RID: 6890
	private bool do_melt_check;

	// Token: 0x04001AEB RID: 6891
	private int openCount;

	// Token: 0x04001AEC RID: 6892
	private Door.ControlState requestedState;

	// Token: 0x04001AED RID: 6893
	private Chore changeStateChore;

	// Token: 0x04001AEE RID: 6894
	private Door.Controller.Instance controller;

	// Token: 0x04001AEF RID: 6895
	private LoggerFSS log;

	// Token: 0x04001AF0 RID: 6896
	private const float REFRESH_HACK_DELAY = 1f;

	// Token: 0x04001AF1 RID: 6897
	private bool doorOpenLiquidRefreshHack;

	// Token: 0x04001AF2 RID: 6898
	private float doorOpenLiquidRefreshTime;

	// Token: 0x04001AF3 RID: 6899
	private static readonly EventSystem.IntraObjectHandler<Door> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Door>(delegate(Door component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001AF4 RID: 6900
	public static readonly HashedString OPEN_CLOSE_PORT_ID = new HashedString("DoorOpenClose");

	// Token: 0x04001AF5 RID: 6901
	private static readonly KAnimFile[] OVERRIDE_ANIMS = new KAnimFile[] { Assets.GetAnim("anim_use_remote_kanim") };

	// Token: 0x04001AF6 RID: 6902
	private static readonly EventSystem.IntraObjectHandler<Door> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<Door>(delegate(Door component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001AF7 RID: 6903
	private static readonly EventSystem.IntraObjectHandler<Door> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<Door>(delegate(Door component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001AF8 RID: 6904
	private bool applyLogicChange;

	// Token: 0x020015B7 RID: 5559
	public enum DoorType
	{
		// Token: 0x040070A0 RID: 28832
		Pressure,
		// Token: 0x040070A1 RID: 28833
		ManualPressure,
		// Token: 0x040070A2 RID: 28834
		Internal,
		// Token: 0x040070A3 RID: 28835
		Sealed
	}

	// Token: 0x020015B8 RID: 5560
	public enum ControlState
	{
		// Token: 0x040070A5 RID: 28837
		Auto,
		// Token: 0x040070A6 RID: 28838
		Opened,
		// Token: 0x040070A7 RID: 28839
		Locked,
		// Token: 0x040070A8 RID: 28840
		NumStates
	}

	// Token: 0x020015B9 RID: 5561
	public class Controller : GameStateMachine<Door.Controller, Door.Controller.Instance, Door>
	{
		// Token: 0x06009280 RID: 37504 RVA: 0x00366C70 File Offset: 0x00364E70
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.closed;
			this.root.Update("RefreshIsBlocked", delegate(Door.Controller.Instance smi, float dt)
			{
				smi.RefreshIsBlocked();
			}, UpdateRate.SIM_200ms, false).ParamTransition<bool>(this.isSealed, this.Sealed.closed, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue);
			this.closeblocked.PlayAnim("open").ParamTransition<bool>(this.isOpen, this.open, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue).ParamTransition<bool>(this.isBlocked, this.closedelay, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsFalse);
			this.closedelay.PlayAnim("open").ScheduleGoTo(0.5f, this.closing).ParamTransition<bool>(this.isOpen, this.open, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue)
				.ParamTransition<bool>(this.isBlocked, this.closeblocked, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue);
			this.closing.ParamTransition<bool>(this.isBlocked, this.closeblocked, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue).ToggleTag(GameTags.Transition).ToggleLoopingSound("Closing loop", (Door.Controller.Instance smi) => smi.master.doorClosingSound, (Door.Controller.Instance smi) => !string.IsNullOrEmpty(smi.master.doorClosingSound))
				.Enter("SetParams", delegate(Door.Controller.Instance smi)
				{
					smi.master.UpdateAnimAndSoundParams(smi.master.on);
				})
				.Update(delegate(Door.Controller.Instance smi, float dt)
				{
					if (smi.master.doorClosingSound != null)
					{
						smi.master.loopingSounds.UpdateSecondParameter(smi.master.doorClosingSound, Door.SOUND_PROGRESS_PARAMETER, smi.Get<KBatchedAnimController>().GetPositionPercent());
					}
				}, UpdateRate.SIM_33ms, false)
				.Enter("SetActive", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetActive(true);
				})
				.Exit("SetActive", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetActive(false);
				})
				.PlayAnim("closing")
				.OnAnimQueueComplete(this.closed);
			this.open.PlayAnim("open").ParamTransition<bool>(this.isOpen, this.closeblocked, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsFalse).Enter("SetWorldStateOpen", delegate(Door.Controller.Instance smi)
			{
				smi.master.SetWorldState();
			});
			this.closed.PlayAnim("closed").ParamTransition<bool>(this.isOpen, this.opening, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue).ParamTransition<bool>(this.isLocked, this.locking, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsTrue)
				.Enter("SetWorldStateClosed", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetWorldState();
				});
			this.locking.PlayAnim("locked_pre").OnAnimQueueComplete(this.locked).Enter("SetWorldStateClosed", delegate(Door.Controller.Instance smi)
			{
				smi.master.SetWorldState();
			});
			this.locked.PlayAnim("locked").ParamTransition<bool>(this.isLocked, this.unlocking, GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.IsFalse);
			this.unlocking.PlayAnim("locked_pst").OnAnimQueueComplete(this.closed);
			this.opening.ToggleTag(GameTags.Transition).ToggleLoopingSound("Opening loop", (Door.Controller.Instance smi) => smi.master.doorOpeningSound, (Door.Controller.Instance smi) => !string.IsNullOrEmpty(smi.master.doorOpeningSound)).Enter("SetParams", delegate(Door.Controller.Instance smi)
			{
				smi.master.UpdateAnimAndSoundParams(smi.master.on);
			})
				.Update(delegate(Door.Controller.Instance smi, float dt)
				{
					if (smi.master.doorOpeningSound != null)
					{
						smi.master.loopingSounds.UpdateSecondParameter(smi.master.doorOpeningSound, Door.SOUND_PROGRESS_PARAMETER, smi.Get<KBatchedAnimController>().GetPositionPercent());
					}
				}, UpdateRate.SIM_33ms, false)
				.Enter("SetActive", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetActive(true);
				})
				.Exit("SetActive", delegate(Door.Controller.Instance smi)
				{
					smi.master.SetActive(false);
				})
				.PlayAnim("opening")
				.OnAnimQueueComplete(this.open);
			this.Sealed.Enter(delegate(Door.Controller.Instance smi)
			{
				OccupyArea component = smi.master.GetComponent<OccupyArea>();
				for (int i = 0; i < component.OccupiedCellsOffsets.Length; i++)
				{
					Grid.PreventFogOfWarReveal[Grid.OffsetCell(Grid.PosToCell(smi.master.gameObject), component.OccupiedCellsOffsets[i])] = false;
				}
				smi.sm.isLocked.Set(true, smi, false);
				smi.master.controlState = Door.ControlState.Locked;
				smi.master.RefreshControlState();
				if (smi.master.GetComponent<Unsealable>().facingRight)
				{
					smi.master.GetComponent<KBatchedAnimController>().FlipX = true;
				}
			}).Enter("SetWorldStateClosed", delegate(Door.Controller.Instance smi)
			{
				smi.master.SetWorldState();
			}).Exit(delegate(Door.Controller.Instance smi)
			{
				smi.sm.isLocked.Set(false, smi, false);
				smi.master.GetComponent<AccessControl>().controlEnabled = true;
				smi.master.controlState = Door.ControlState.Opened;
				smi.master.RefreshControlState();
				smi.sm.isOpen.Set(true, smi, false);
				smi.sm.isLocked.Set(false, smi, false);
				smi.sm.isSealed.Set(false, smi, false);
			});
			this.Sealed.closed.PlayAnim("sealed", KAnim.PlayMode.Once);
			this.Sealed.awaiting_unlock.ToggleChore((Door.Controller.Instance smi) => this.CreateUnsealChore(smi, true), this.Sealed.chore_pst);
			this.Sealed.chore_pst.Enter(delegate(Door.Controller.Instance smi)
			{
				smi.master.hasBeenUnsealed = true;
				if (smi.master.GetComponent<Unsealable>().unsealed)
				{
					smi.GoTo(this.opening);
					FogOfWarMask.ClearMask(Grid.CellRight(Grid.PosToCell(smi.master.gameObject)));
					FogOfWarMask.ClearMask(Grid.CellLeft(Grid.PosToCell(smi.master.gameObject)));
					return;
				}
				smi.GoTo(this.Sealed.closed);
			});
		}

		// Token: 0x06009281 RID: 37505 RVA: 0x003671AC File Offset: 0x003653AC
		private Chore CreateUnsealChore(Door.Controller.Instance smi, bool approach_right)
		{
			return new WorkChore<Unsealable>(Db.Get().ChoreTypes.Toggle, smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x040070A9 RID: 28841
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State open;

		// Token: 0x040070AA RID: 28842
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State opening;

		// Token: 0x040070AB RID: 28843
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closed;

		// Token: 0x040070AC RID: 28844
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closing;

		// Token: 0x040070AD RID: 28845
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closedelay;

		// Token: 0x040070AE RID: 28846
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closeblocked;

		// Token: 0x040070AF RID: 28847
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State locking;

		// Token: 0x040070B0 RID: 28848
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State locked;

		// Token: 0x040070B1 RID: 28849
		public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State unlocking;

		// Token: 0x040070B2 RID: 28850
		public Door.Controller.SealedStates Sealed;

		// Token: 0x040070B3 RID: 28851
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter isOpen;

		// Token: 0x040070B4 RID: 28852
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter isLocked;

		// Token: 0x040070B5 RID: 28853
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter isBlocked;

		// Token: 0x040070B6 RID: 28854
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter isSealed;

		// Token: 0x040070B7 RID: 28855
		public StateMachine<Door.Controller, Door.Controller.Instance, Door, object>.BoolParameter sealDirectionRight;

		// Token: 0x02002774 RID: 10100
		public class SealedStates : GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State
		{
			// Token: 0x0400AE56 RID: 44630
			public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State closed;

			// Token: 0x0400AE57 RID: 44631
			public Door.Controller.SealedStates.AwaitingUnlock awaiting_unlock;

			// Token: 0x0400AE58 RID: 44632
			public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State chore_pst;

			// Token: 0x02003880 RID: 14464
			public class AwaitingUnlock : GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State
			{
				// Token: 0x0400E469 RID: 58473
				public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State awaiting_arrival;

				// Token: 0x0400E46A RID: 58474
				public GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.State unlocking;
			}
		}

		// Token: 0x02002775 RID: 10101
		public new class Instance : GameStateMachine<Door.Controller, Door.Controller.Instance, Door, object>.GameInstance
		{
			// Token: 0x0600C741 RID: 51009 RVA: 0x00413603 File Offset: 0x00411803
			public Instance(Door door)
				: base(door)
			{
			}

			// Token: 0x0600C742 RID: 51010 RVA: 0x0041360C File Offset: 0x0041180C
			public void RefreshIsBlocked()
			{
				bool flag = false;
				foreach (int num in this.building.PlacementCells)
				{
					if (Grid.Objects[num, 40] != null)
					{
						flag = true;
						break;
					}
				}
				base.sm.isBlocked.Set(flag, base.smi, false);
			}

			// Token: 0x0400AE59 RID: 44633
			[MyCmpReq]
			public Building building;
		}
	}
}
