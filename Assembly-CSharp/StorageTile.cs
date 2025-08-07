using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020007D3 RID: 2003
public class StorageTile : GameStateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>
{
	// Token: 0x060035D1 RID: 13777 RVA: 0x0012C850 File Offset: 0x0012AA50
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.root.PlayAnim("on").EventHandler(GameHashes.OnStorageChange, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.OnStorageChanged)).EventHandler(GameHashes.StorageTileTargetItemChanged, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.RefreshContentVisuals));
		this.idle.Enter(new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.RefreshContentVisuals)).EventTransition(GameHashes.OnStorageChange, this.awaitingDelivery, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.Transition.ConditionCallback(StorageTile.IsAwaitingDelivery)).EventTransition(GameHashes.StorageTileTargetItemChanged, this.change, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.Transition.ConditionCallback(StorageTile.IsAwaitingForSettingChange));
		this.change.Enter(new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.RefreshContentVisuals)).EventTransition(GameHashes.StorageTileTargetItemChanged, this.idle, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.Transition.ConditionCallback(StorageTile.NoLongerAwaitingForSettingChange)).DefaultState(this.change.awaitingSettingsChange);
		this.change.awaitingSettingsChange.Enter(new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.StartWorkChore)).Exit(new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.CancelWorkChore)).ToggleStatusItem(Db.Get().BuildingStatusItems.ChangeStorageTileTarget, null)
			.WorkableCompleteTransition((StorageTile.Instance smi) => smi.GetWorkable(), this.change.complete);
		this.change.complete.Enter(new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.ApplySettings)).Enter(new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.DropUndesiredItems)).EnterTransition(this.idle, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.Transition.ConditionCallback(StorageTile.HasAnyDesiredItemStored))
			.EnterTransition(this.awaitingDelivery, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.Transition.ConditionCallback(StorageTile.IsAwaitingDelivery));
		this.awaitingDelivery.Enter(new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State.Callback(StorageTile.RefreshContentVisuals)).EventTransition(GameHashes.OnStorageChange, this.idle, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.Transition.ConditionCallback(StorageTile.HasAnyDesiredItemStored)).EventTransition(GameHashes.StorageTileTargetItemChanged, this.change, new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.Transition.ConditionCallback(StorageTile.IsAwaitingForSettingChange));
	}

	// Token: 0x060035D2 RID: 13778 RVA: 0x0012CA5B File Offset: 0x0012AC5B
	public static void DropUndesiredItems(StorageTile.Instance smi)
	{
		smi.DropUndesiredItems();
	}

	// Token: 0x060035D3 RID: 13779 RVA: 0x0012CA63 File Offset: 0x0012AC63
	public static void ApplySettings(StorageTile.Instance smi)
	{
		smi.ApplySettings();
	}

	// Token: 0x060035D4 RID: 13780 RVA: 0x0012CA6B File Offset: 0x0012AC6B
	public static void StartWorkChore(StorageTile.Instance smi)
	{
		smi.StartChangeSettingChore();
	}

	// Token: 0x060035D5 RID: 13781 RVA: 0x0012CA73 File Offset: 0x0012AC73
	public static void CancelWorkChore(StorageTile.Instance smi)
	{
		smi.CanceChangeSettingChore();
	}

	// Token: 0x060035D6 RID: 13782 RVA: 0x0012CA7B File Offset: 0x0012AC7B
	public static void RefreshContentVisuals(StorageTile.Instance smi)
	{
		smi.UpdateContentSymbol();
	}

	// Token: 0x060035D7 RID: 13783 RVA: 0x0012CA83 File Offset: 0x0012AC83
	public static bool IsAwaitingForSettingChange(StorageTile.Instance smi)
	{
		return smi.IsPendingChange;
	}

	// Token: 0x060035D8 RID: 13784 RVA: 0x0012CA8B File Offset: 0x0012AC8B
	public static bool NoLongerAwaitingForSettingChange(StorageTile.Instance smi)
	{
		return !smi.IsPendingChange;
	}

	// Token: 0x060035D9 RID: 13785 RVA: 0x0012CA96 File Offset: 0x0012AC96
	public static bool HasAnyDesiredItemStored(StorageTile.Instance smi)
	{
		return smi.HasAnyDesiredContents;
	}

	// Token: 0x060035DA RID: 13786 RVA: 0x0012CA9E File Offset: 0x0012AC9E
	public static void OnStorageChanged(StorageTile.Instance smi)
	{
		smi.PlayDoorAnimation();
		StorageTile.RefreshContentVisuals(smi);
	}

	// Token: 0x060035DB RID: 13787 RVA: 0x0012CAAC File Offset: 0x0012ACAC
	public static bool IsAwaitingDelivery(StorageTile.Instance smi)
	{
		return !smi.IsPendingChange && !smi.HasAnyDesiredContents;
	}

	// Token: 0x0400208F RID: 8335
	public const string METER_TARGET = "meter_target";

	// Token: 0x04002090 RID: 8336
	public const string METER_ANIMATION = "meter";

	// Token: 0x04002091 RID: 8337
	public static HashedString DOOR_SYMBOL_NAME = new HashedString("storage_door");

	// Token: 0x04002092 RID: 8338
	public static HashedString ITEM_SYMBOL_TARGET = new HashedString("meter_target_object");

	// Token: 0x04002093 RID: 8339
	public static HashedString ITEM_SYMBOL_NAME = new HashedString("object");

	// Token: 0x04002094 RID: 8340
	public const string ITEM_SYMBOL_ANIMATION = "meter_object";

	// Token: 0x04002095 RID: 8341
	public static HashedString ITEM_PREVIEW_SYMBOL_TARGET = new HashedString("meter_target_object_ui");

	// Token: 0x04002096 RID: 8342
	public static HashedString ITEM_PREVIEW_SYMBOL_NAME = new HashedString("object_ui");

	// Token: 0x04002097 RID: 8343
	public const string ITEM_PREVIEW_SYMBOL_ANIMATION = "meter_object_ui";

	// Token: 0x04002098 RID: 8344
	public static HashedString ITEM_PREVIEW_BACKGROUND_SYMBOL_NAME = new HashedString("placeholder");

	// Token: 0x04002099 RID: 8345
	public const string DEFAULT_ANIMATION_NAME = "on";

	// Token: 0x0400209A RID: 8346
	public const string STORAGE_CHANGE_ANIMATION_NAME = "door";

	// Token: 0x0400209B RID: 8347
	public const string SYMBOL_ANIMATION_NAME_AWAITING_DELIVERY = "ui";

	// Token: 0x0400209C RID: 8348
	public static Tag INVALID_TAG = GameTags.Void;

	// Token: 0x0400209D RID: 8349
	private StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.TagParameter TargetItemTag = new StateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.TagParameter(StorageTile.INVALID_TAG);

	// Token: 0x0400209E RID: 8350
	public GameStateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State idle;

	// Token: 0x0400209F RID: 8351
	public StorageTile.SettingsChangeStates change;

	// Token: 0x040020A0 RID: 8352
	public GameStateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State awaitingDelivery;

	// Token: 0x02001713 RID: 5907
	public class SpecificItemTagSizeInstruction
	{
		// Token: 0x0600978A RID: 38794 RVA: 0x0037CDB7 File Offset: 0x0037AFB7
		public SpecificItemTagSizeInstruction(Tag tag, float size)
		{
			this.tag = tag;
			this.sizeMultiplier = size;
		}

		// Token: 0x0400748B RID: 29835
		public Tag tag;

		// Token: 0x0400748C RID: 29836
		public float sizeMultiplier;
	}

	// Token: 0x02001714 RID: 5908
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0600978B RID: 38795 RVA: 0x0037CDD0 File Offset: 0x0037AFD0
		public StorageTile.SpecificItemTagSizeInstruction GetSizeInstructionForObject(GameObject obj)
		{
			if (this.specialItemCases == null)
			{
				return null;
			}
			KPrefabID component = obj.GetComponent<KPrefabID>();
			foreach (StorageTile.SpecificItemTagSizeInstruction specificItemTagSizeInstruction in this.specialItemCases)
			{
				if (component.HasTag(specificItemTagSizeInstruction.tag))
				{
					return specificItemTagSizeInstruction;
				}
			}
			return null;
		}

		// Token: 0x0400748D RID: 29837
		public float MaxCapacity;

		// Token: 0x0400748E RID: 29838
		public StorageTile.SpecificItemTagSizeInstruction[] specialItemCases;
	}

	// Token: 0x02001715 RID: 5909
	public class SettingsChangeStates : GameStateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State
	{
		// Token: 0x0400748F RID: 29839
		public GameStateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State awaitingSettingsChange;

		// Token: 0x04007490 RID: 29840
		public GameStateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.State complete;
	}

	// Token: 0x02001716 RID: 5910
	public new class Instance : GameStateMachine<StorageTile, StorageTile.Instance, IStateMachineTarget, StorageTile.Def>.GameInstance, IUserControlledCapacity
	{
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600978E RID: 38798 RVA: 0x0037CE28 File Offset: 0x0037B028
		public Tag TargetTag
		{
			get
			{
				return base.smi.sm.TargetItemTag.Get(base.smi);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600978F RID: 38799 RVA: 0x0037CE45 File Offset: 0x0037B045
		public bool HasContents
		{
			get
			{
				return this.storage.MassStored() > 0f;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06009790 RID: 38800 RVA: 0x0037CE59 File Offset: 0x0037B059
		public bool HasAnyDesiredContents
		{
			get
			{
				if (!(this.TargetTag == StorageTile.INVALID_TAG))
				{
					return this.AmountOfDesiredContentStored > 0f;
				}
				return !this.HasContents;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06009791 RID: 38801 RVA: 0x0037CE84 File Offset: 0x0037B084
		public float AmountOfDesiredContentStored
		{
			get
			{
				if (!(this.TargetTag == StorageTile.INVALID_TAG))
				{
					return this.storage.GetMassAvailable(this.TargetTag);
				}
				return 0f;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06009792 RID: 38802 RVA: 0x0037CEAF File Offset: 0x0037B0AF
		public bool IsPendingChange
		{
			get
			{
				return this.GetTreeFilterableCurrentTag() != this.TargetTag;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06009793 RID: 38803 RVA: 0x0037CEC2 File Offset: 0x0037B0C2
		// (set) Token: 0x06009794 RID: 38804 RVA: 0x0037CEDA File Offset: 0x0037B0DA
		public float UserMaxCapacity
		{
			get
			{
				return Mathf.Min(this.userMaxCapacity, this.storage.capacityKg);
			}
			set
			{
				this.userMaxCapacity = value;
				this.filteredStorage.FilterChanged();
				this.RefreshAmountMeter();
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06009795 RID: 38805 RVA: 0x0037CEF4 File Offset: 0x0037B0F4
		public float AmountStored
		{
			get
			{
				return this.storage.MassStored();
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06009796 RID: 38806 RVA: 0x0037CF01 File Offset: 0x0037B101
		public float MinCapacity
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06009797 RID: 38807 RVA: 0x0037CF08 File Offset: 0x0037B108
		public float MaxCapacity
		{
			get
			{
				return base.def.MaxCapacity;
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06009798 RID: 38808 RVA: 0x0037CF15 File Offset: 0x0037B115
		public bool WholeValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06009799 RID: 38809 RVA: 0x0037CF18 File Offset: 0x0037B118
		public LocString CapacityUnits
		{
			get
			{
				return GameUtil.GetCurrentMassUnit(false);
			}
		}

		// Token: 0x0600979A RID: 38810 RVA: 0x0037CF20 File Offset: 0x0037B120
		private Tag GetTreeFilterableCurrentTag()
		{
			if (this.treeFilterable.GetTags() != null && this.treeFilterable.GetTags().Count != 0)
			{
				return this.treeFilterable.GetTags().GetRandom<Tag>();
			}
			return StorageTile.INVALID_TAG;
		}

		// Token: 0x0600979B RID: 38811 RVA: 0x0037CF57 File Offset: 0x0037B157
		public StorageTileSwitchItemWorkable GetWorkable()
		{
			return base.smi.gameObject.GetComponent<StorageTileSwitchItemWorkable>();
		}

		// Token: 0x0600979C RID: 38812 RVA: 0x0037CF6C File Offset: 0x0037B16C
		public Instance(IStateMachineTarget master, StorageTile.Def def)
			: base(master, def)
		{
			this.itemSymbol = this.CreateSymbolOverrideCapsule(StorageTile.ITEM_SYMBOL_TARGET, StorageTile.ITEM_SYMBOL_NAME, "meter_object");
			this.itemSymbol.usingNewSymbolOverrideSystem = true;
			this.itemSymbolOverrideController = SymbolOverrideControllerUtil.AddToPrefab(this.itemSymbol.gameObject);
			this.itemPreviewSymbol = this.CreateSymbolOverrideCapsule(StorageTile.ITEM_PREVIEW_SYMBOL_TARGET, StorageTile.ITEM_PREVIEW_SYMBOL_NAME, "meter_object_ui");
			this.defaultItemSymbolScale = this.itemSymbol.transform.localScale.x;
			this.defaultItemLocalPosition = this.itemSymbol.transform.localPosition;
			this.doorSymbol = this.CreateEmptyKAnimController(StorageTile.DOOR_SYMBOL_NAME.ToString());
			this.doorSymbol.initialAnim = "on";
			foreach (KAnim.Build.Symbol symbol in this.doorSymbol.AnimFiles[0].GetData().build.symbols)
			{
				this.doorSymbol.SetSymbolVisiblity(symbol.hash, symbol.hash == StorageTile.DOOR_SYMBOL_NAME);
			}
			this.doorSymbol.transform.SetParent(this.animController.transform, false);
			this.doorSymbol.transform.SetLocalPosition(-Vector3.forward * 0.05f);
			this.doorSymbol.onAnimComplete += this.OnDoorAnimationCompleted;
			this.doorSymbol.gameObject.SetActive(true);
			this.animController.SetSymbolVisiblity(StorageTile.DOOR_SYMBOL_NAME, false);
			this.doorAnimLink = new KAnimLink(this.animController, this.doorSymbol);
			this.amountMeter = new MeterController(this.animController, "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
			ChoreType choreType = Db.Get().ChoreTypes.Get(this.choreTypeID);
			this.filteredStorage = new FilteredStorage(this.storage, null, this, false, choreType);
			base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
			base.Subscribe(1606648047, new Action<object>(this.OnObjectReplaced));
		}

		// Token: 0x0600979D RID: 38813 RVA: 0x0037D1D3 File Offset: 0x0037B3D3
		public override void StartSM()
		{
			base.StartSM();
			this.filteredStorage.FilterChanged();
		}

		// Token: 0x0600979E RID: 38814 RVA: 0x0037D1E6 File Offset: 0x0037B3E6
		public override void PostParamsInitialized()
		{
			if (this.TargetTag != StorageTile.INVALID_TAG && Assets.GetPrefab(this.TargetTag) == null)
			{
				this.SetTargetItem(StorageTile.INVALID_TAG);
				this.DropUndesiredItems();
			}
			base.PostParamsInitialized();
		}

		// Token: 0x0600979F RID: 38815 RVA: 0x0037D224 File Offset: 0x0037B424
		private void OnObjectReplaced(object data)
		{
			Constructable.ReplaceCallbackParameters replaceCallbackParameters = (Constructable.ReplaceCallbackParameters)data;
			List<GameObject> list = new List<GameObject>();
			Storage storage = this.storage;
			bool flag = false;
			bool flag2 = false;
			List<GameObject> list2 = list;
			storage.DropAll(flag, flag2, default(Vector3), true, list2);
			if (replaceCallbackParameters.Worker != null)
			{
				foreach (GameObject gameObject in list)
				{
					gameObject.GetComponent<Pickupable>().Trigger(580035959, replaceCallbackParameters.Worker);
				}
			}
		}

		// Token: 0x060097A0 RID: 38816 RVA: 0x0037D2B8 File Offset: 0x0037B4B8
		private void OnDoorAnimationCompleted(HashedString animName)
		{
			if (animName == "door")
			{
				this.doorSymbol.Play("on", KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x060097A1 RID: 38817 RVA: 0x0037D2EC File Offset: 0x0037B4EC
		private KBatchedAnimController CreateEmptyKAnimController(string name)
		{
			GameObject gameObject = new GameObject(base.gameObject.name + "-" + name);
			gameObject.SetActive(false);
			KBatchedAnimController kbatchedAnimController = gameObject.AddComponent<KBatchedAnimController>();
			kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("storagetile_kanim") };
			kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingFront;
			return kbatchedAnimController;
		}

		// Token: 0x060097A2 RID: 38818 RVA: 0x0037D348 File Offset: 0x0037B548
		private KBatchedAnimController CreateSymbolOverrideCapsule(HashedString symbolTarget, HashedString symbolName, string animationName)
		{
			KBatchedAnimController kbatchedAnimController = this.CreateEmptyKAnimController(symbolTarget.ToString());
			kbatchedAnimController.initialAnim = animationName;
			bool flag;
			Matrix4x4 symbolTransform = this.animController.GetSymbolTransform(symbolTarget, out flag);
			bool flag2;
			Matrix2x3 symbolLocalTransform = this.animController.GetSymbolLocalTransform(symbolTarget, out flag2);
			Vector3 vector = symbolTransform.GetColumn(3);
			Vector3 vector2 = Vector3.one * symbolLocalTransform.m00;
			kbatchedAnimController.transform.SetParent(base.transform, false);
			kbatchedAnimController.transform.SetPosition(vector);
			Vector3 localPosition = kbatchedAnimController.transform.localPosition;
			localPosition.z = -0.0025f;
			kbatchedAnimController.transform.localPosition = localPosition;
			kbatchedAnimController.transform.localScale = vector2;
			kbatchedAnimController.gameObject.SetActive(false);
			this.animController.SetSymbolVisiblity(symbolTarget, false);
			return kbatchedAnimController;
		}

		// Token: 0x060097A3 RID: 38819 RVA: 0x0037D420 File Offset: 0x0037B620
		private void OnCopySettings(object sourceOBJ)
		{
			if (sourceOBJ != null)
			{
				StorageTile.Instance smi = ((GameObject)sourceOBJ).GetSMI<StorageTile.Instance>();
				if (smi != null)
				{
					this.SetTargetItem(smi.TargetTag);
					this.UserMaxCapacity = smi.UserMaxCapacity;
				}
			}
		}

		// Token: 0x060097A4 RID: 38820 RVA: 0x0037D458 File Offset: 0x0037B658
		public void RefreshAmountMeter()
		{
			float num = ((this.UserMaxCapacity == 0f) ? 0f : Mathf.Clamp(this.AmountOfDesiredContentStored / this.UserMaxCapacity, 0f, 1f));
			this.amountMeter.SetPositionPercent(num);
		}

		// Token: 0x060097A5 RID: 38821 RVA: 0x0037D4A2 File Offset: 0x0037B6A2
		public void PlayDoorAnimation()
		{
			this.doorSymbol.Play("door", KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x060097A6 RID: 38822 RVA: 0x0037D4C4 File Offset: 0x0037B6C4
		public void SetTargetItem(Tag tag)
		{
			base.sm.TargetItemTag.Set(tag, this, false);
			base.gameObject.Trigger(-2076953849, null);
		}

		// Token: 0x060097A7 RID: 38823 RVA: 0x0037D4EC File Offset: 0x0037B6EC
		public void ApplySettings()
		{
			Tag treeFilterableCurrentTag = this.GetTreeFilterableCurrentTag();
			this.treeFilterable.RemoveTagFromFilter(treeFilterableCurrentTag);
		}

		// Token: 0x060097A8 RID: 38824 RVA: 0x0037D50C File Offset: 0x0037B70C
		public void DropUndesiredItems()
		{
			Vector3 vector = Grid.CellToPos(this.GetWorkable().LastCellWorkerUsed) + Vector3.right * Grid.CellSizeInMeters * 0.5f + Vector3.up * Grid.CellSizeInMeters * 0.5f;
			vector.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			if (this.TargetTag != StorageTile.INVALID_TAG)
			{
				this.treeFilterable.AddTagToFilter(this.TargetTag);
				GameObject[] array = this.storage.DropUnlessHasTag(this.TargetTag);
				if (array != null)
				{
					GameObject[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].transform.SetPosition(vector);
					}
				}
			}
			else
			{
				this.storage.DropAll(vector, false, false, default(Vector3), true, null);
			}
			this.storage.DropUnlessHasTag(this.TargetTag);
		}

		// Token: 0x060097A9 RID: 38825 RVA: 0x0037D5FC File Offset: 0x0037B7FC
		public void UpdateContentSymbol()
		{
			this.RefreshAmountMeter();
			bool flag = this.TargetTag == StorageTile.INVALID_TAG;
			if (flag && !this.HasContents)
			{
				this.itemSymbol.gameObject.SetActive(false);
				this.itemPreviewSymbol.gameObject.SetActive(false);
				this.animController.SetSymbolVisiblity(StorageTile.ITEM_PREVIEW_BACKGROUND_SYMBOL_NAME, false);
				return;
			}
			bool flag2 = !flag && (this.IsPendingChange || !this.HasAnyDesiredContents);
			string text = "";
			GameObject gameObject = ((this.TargetTag == StorageTile.INVALID_TAG) ? Assets.GetPrefab(this.storage.items[0].PrefabID()) : Assets.GetPrefab(this.TargetTag));
			KAnimFile animFileFromPrefabWithTag = global::Def.GetAnimFileFromPrefabWithTag(gameObject, flag2 ? "ui" : "", out text);
			this.animController.SetSymbolVisiblity(StorageTile.ITEM_PREVIEW_BACKGROUND_SYMBOL_NAME, flag2);
			this.itemPreviewSymbol.gameObject.SetActive(flag2);
			this.itemSymbol.gameObject.SetActive(!flag2);
			if (flag2)
			{
				this.itemPreviewSymbol.SwapAnims(new KAnimFile[] { animFileFromPrefabWithTag });
				this.itemPreviewSymbol.Play(text, KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			if (gameObject.HasTag(GameTags.Egg))
			{
				string text2 = text;
				if (!string.IsNullOrEmpty(text2))
				{
					this.itemSymbolOverrideController.ApplySymbolOverridesByAffix(animFileFromPrefabWithTag, text2, null, 0);
				}
				text = gameObject.GetComponent<KBatchedAnimController>().initialAnim;
			}
			else
			{
				this.itemSymbolOverrideController.RemoveAllSymbolOverrides(0);
				text = gameObject.GetComponent<KBatchedAnimController>().initialAnim;
			}
			this.itemSymbol.SwapAnims(new KAnimFile[] { animFileFromPrefabWithTag });
			this.itemSymbol.Play(text, KAnim.PlayMode.Once, 1f, 0f);
			StorageTile.SpecificItemTagSizeInstruction sizeInstructionForObject = base.def.GetSizeInstructionForObject(gameObject);
			this.itemSymbol.transform.localScale = Vector3.one * ((sizeInstructionForObject != null) ? sizeInstructionForObject.sizeMultiplier : this.defaultItemSymbolScale);
			KCollider2D component = gameObject.GetComponent<KCollider2D>();
			Vector3 vector = this.defaultItemLocalPosition;
			vector.y += ((component == null || component is KCircleCollider2D) ? 0f : (-component.offset.y * 0.5f));
			this.itemSymbol.transform.localPosition = vector;
		}

		// Token: 0x060097AA RID: 38826 RVA: 0x0037D867 File Offset: 0x0037BA67
		private void AbortChore()
		{
			if (this.chore != null)
			{
				this.chore.Cancel("Change settings Chore aborted");
				this.chore = null;
			}
		}

		// Token: 0x060097AB RID: 38827 RVA: 0x0037D888 File Offset: 0x0037BA88
		public void StartChangeSettingChore()
		{
			this.AbortChore();
			this.chore = new WorkChore<StorageTileSwitchItemWorkable>(Db.Get().ChoreTypes.Toggle, this.GetWorkable(), null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x060097AC RID: 38828 RVA: 0x0037D8CC File Offset: 0x0037BACC
		public void CanceChangeSettingChore()
		{
			this.AbortChore();
		}

		// Token: 0x04007491 RID: 29841
		[Serialize]
		private float userMaxCapacity = float.PositiveInfinity;

		// Token: 0x04007492 RID: 29842
		[MyCmpGet]
		private Storage storage;

		// Token: 0x04007493 RID: 29843
		[MyCmpGet]
		private KBatchedAnimController animController;

		// Token: 0x04007494 RID: 29844
		[MyCmpGet]
		private TreeFilterable treeFilterable;

		// Token: 0x04007495 RID: 29845
		private FilteredStorage filteredStorage;

		// Token: 0x04007496 RID: 29846
		private Chore chore;

		// Token: 0x04007497 RID: 29847
		private MeterController amountMeter;

		// Token: 0x04007498 RID: 29848
		private KBatchedAnimController doorSymbol;

		// Token: 0x04007499 RID: 29849
		private KBatchedAnimController itemSymbol;

		// Token: 0x0400749A RID: 29850
		private SymbolOverrideController itemSymbolOverrideController;

		// Token: 0x0400749B RID: 29851
		private KBatchedAnimController itemPreviewSymbol;

		// Token: 0x0400749C RID: 29852
		private KAnimLink doorAnimLink;

		// Token: 0x0400749D RID: 29853
		private string choreTypeID = Db.Get().ChoreTypes.StorageFetch.Id;

		// Token: 0x0400749E RID: 29854
		private float defaultItemSymbolScale = -1f;

		// Token: 0x0400749F RID: 29855
		private Vector3 defaultItemLocalPosition = Vector3.zero;
	}
}
