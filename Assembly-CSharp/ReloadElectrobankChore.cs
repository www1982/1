using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000496 RID: 1174
public class ReloadElectrobankChore : Chore<ReloadElectrobankChore.Instance>
{
	// Token: 0x0600188A RID: 6282 RVA: 0x00089148 File Offset: 0x00087348
	public ReloadElectrobankChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.ReloadElectrobank, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new ReloadElectrobankChore.Instance(this, target.gameObject);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(ReloadElectrobankChore.ElectrobankIsNotNull, null);
	}

	// Token: 0x0600188B RID: 6283 RVA: 0x000891AC File Offset: 0x000873AC
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			global::Debug.LogError("ReloadElectrobankChore null context.consumer");
			return;
		}
		BionicBatteryMonitor.Instance smi = context.consumerState.consumer.GetSMI<BionicBatteryMonitor.Instance>();
		if (smi == null)
		{
			global::Debug.LogError("ReloadElectrobankChore null BionicBatteryMonitor.Instance");
			return;
		}
		Electrobank closestElectrobank = smi.GetClosestElectrobank();
		if (closestElectrobank == null)
		{
			global::Debug.LogError("ReloadElectrobankChore null electrobank.gameObject");
			return;
		}
		base.smi.sm.electrobankSource.Set(closestElectrobank.gameObject, base.smi, false);
		base.smi.sm.amountRequested.Set(closestElectrobank.GetComponent<PrimaryElement>().Mass, base.smi, false);
		base.smi.sm.dupe.Set(context.consumerState.consumer, base.smi);
		base.Begin(context);
	}

	// Token: 0x0600188C RID: 6284 RVA: 0x00089289 File Offset: 0x00087489
	public bool IsInstallingAtMessStation()
	{
		return base.smi.IsInsideState(base.smi.sm.installAtMessStation.install);
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x000892AB File Offset: 0x000874AB
	public static bool HasAnyDepletedBattery(ReloadElectrobankChore.Instance smi)
	{
		return ReloadElectrobankChore.GetAnyEmptyBattery(smi) != null;
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x000892B9 File Offset: 0x000874B9
	public static GameObject GetAnyEmptyBattery(ReloadElectrobankChore.Instance smi)
	{
		return smi.batteryMonitor.storage.FindFirst(GameTags.EmptyPortableBattery);
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x000892D0 File Offset: 0x000874D0
	public static void RemoveDepletedElectrobank(ReloadElectrobankChore.Instance smi)
	{
		GameObject anyEmptyBattery = ReloadElectrobankChore.GetAnyEmptyBattery(smi);
		if (anyEmptyBattery != null)
		{
			smi.batteryMonitor.storage.Drop(anyEmptyBattery, true);
		}
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x00089300 File Offset: 0x00087500
	public static void InstallElectrobank(ReloadElectrobankChore.Instance smi)
	{
		Storage[] components = smi.gameObject.GetComponents<Storage>();
		for (int i = 0; i < components.Length; i++)
		{
			if (components[i] != smi.batteryMonitor.storage && components[i].FindFirst(GameTags.ChargedPortableBattery) != null)
			{
				components[i].Transfer(smi.batteryMonitor.storage, false, false);
				break;
			}
		}
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_BionicBattery, true);
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x00089378 File Offset: 0x00087578
	private static void SetStoredItemVisibility(GameObject item, bool visible)
	{
		if (item == null)
		{
			return;
		}
		KBatchedAnimTracker component = item.GetComponent<KBatchedAnimTracker>();
		if (component != null)
		{
			component.enabled = visible;
		}
		Storage.MakeItemInvisible(item, !visible, false);
	}

	// Token: 0x04000E40 RID: 3648
	public const float LOOP_LENGTH = 4.333f;

	// Token: 0x04000E41 RID: 3649
	public static readonly Chore.Precondition ElectrobankIsNotNull = new Chore.Precondition
	{
		id = "ElectrobankIsNotNull",
		description = DUPLICANTS.CHORES.PRECONDITIONS.EDIBLE_IS_NOT_NULL,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return null != context.consumerState.consumer.GetSMI<BionicBatteryMonitor.Instance>().GetClosestElectrobank();
		}
	};

	// Token: 0x020012B2 RID: 4786
	public class States : GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore>
	{
		// Token: 0x06008766 RID: 34662 RVA: 0x00343CA6 File Offset: 0x00341EA6
		private bool IsMessStationInvalid(GameObject messStation)
		{
			return messStation == null || !messStation.GetComponent<Operational>().IsOperational;
		}

		// Token: 0x06008767 RID: 34663 RVA: 0x00343CC4 File Offset: 0x00341EC4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			this.defaultElectrobankSymbol = Assets.GetPrefab("Electrobank").GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbolByIndex(0U);
			this.depletedElectrobankSymbol = Assets.GetPrefab("EmptyElectrobank").GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbolByIndex(0U);
			default_state = this.fetch;
			base.Target(this.dupe);
			this.root.Enter("SetMessStation", delegate(ReloadElectrobankChore.Instance smi)
			{
				smi.UpdateMessStation();
			}).EventHandler(GameHashes.AssignablesChanged, delegate(ReloadElectrobankChore.Instance smi)
			{
				smi.UpdateMessStation();
			});
			this.fetch.InitializeStates(this.dupe, this.electrobankSource, this.pickedUpElectrobank, this.amountRequested, this.actualunits, this.installAtMessStation, null).OnTargetLost(this.electrobankSource, this.electrobankLost);
			this.installAtMessStation.Enter(delegate(ReloadElectrobankChore.Instance smi)
			{
				EatChore.StatesInstance.SetZ(this.pickedUpElectrobank.Get(smi), Grid.GetLayerZ(Grid.SceneLayer.Ore));
			}).EnterTransition(this.installAtSafeLocation, (ReloadElectrobankChore.Instance smi) => this.IsMessStationInvalid(this.messstation.Get(smi))).DefaultState(this.installAtMessStation.approach)
				.ParamTransition<GameObject>(this.messstation, this.installAtSafeLocation, (ReloadElectrobankChore.Instance _, GameObject messStation) => this.IsMessStationInvalid(messStation));
			this.installAtMessStation.approach.InitializeStates(this.dupe, this.messstation, this.installAtMessStation.removeDepletedBatteries, this.installAtSafeLocation, null, null);
			this.installAtMessStation.removeDepletedBatteries.InitializeStates(this.installAtMessStation.install);
			this.installAtMessStation.install.InitializeStates(this.complete, new ReloadElectrobankChore.States.MessStationInstallBatteryAnim()).Enter(delegate(ReloadElectrobankChore.Instance smi)
			{
				GameObject gameObject = this.dupe.Get(smi);
				EatChore.StatesInstance.SetZ(gameObject, Grid.GetLayerZ(Grid.SceneLayer.BuildingFront));
				EatChore.StatesInstance.SetZ(this.pickedUpElectrobank.Get(smi), Grid.GetLayerZ(Grid.SceneLayer.Ore));
				EatChore.StatesInstance.ApplyRoomAndSaltEffects(this.messstation.Get(smi), gameObject, new float?(1800f));
			}).Exit(delegate(ReloadElectrobankChore.Instance smi)
			{
				EatChore.StatesInstance.SetZ(this.dupe.Get(smi), Grid.GetLayerZ(Grid.SceneLayer.Move));
			});
			this.installAtSafeLocation.Enter("CreateSafeLocation", delegate(ReloadElectrobankChore.Instance smi)
			{
				ValueTuple<GameObject, int> valueTuple = EatChore.StatesInstance.CreateLocator(this.dupe.Get<Sensors>(smi), this.dupe.Get<Transform>(smi), "ReloadElectrobankLocator");
				GameObject item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				this.safeLocation.Set(item, smi, false);
				this.safeCellIndex.Set(item2, smi, false);
			}).Exit("DestroySafeLocation", delegate(ReloadElectrobankChore.Instance smi)
			{
				Grid.Reserved[this.safeCellIndex.Get(smi)] = false;
				ChoreHelpers.DestroyLocator(this.safeLocation.Get(smi));
				this.safeLocation.Set(null, smi);
			}).DefaultState(this.installAtSafeLocation.approach);
			this.installAtSafeLocation.approach.InitializeStates(this.dupe, this.safeLocation, this.installAtSafeLocation.removeDepletedBatteries, this.installAtSafeLocation.removeDepletedBatteries, null, null);
			this.installAtSafeLocation.removeDepletedBatteries.InitializeStates(this.installAtSafeLocation.install);
			this.installAtSafeLocation.install.InitializeStates(this.complete, new ReloadElectrobankChore.States.DefaultInstallBatteryAnim());
			this.complete.Enter(new StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State.Callback(ReloadElectrobankChore.InstallElectrobank)).ReturnSuccess();
			this.electrobankLost.Target(this.dupe).TriggerOnEnter(GameHashes.TargetElectrobankLost, null).ReturnFailure();
		}

		// Token: 0x04006726 RID: 26406
		public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.FetchSubState fetch;

		// Token: 0x04006727 RID: 26407
		public ReloadElectrobankChore.States.InstallAtMessStation installAtMessStation;

		// Token: 0x04006728 RID: 26408
		public ReloadElectrobankChore.States.InstallAtSafeLocation installAtSafeLocation;

		// Token: 0x04006729 RID: 26409
		public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State complete;

		// Token: 0x0400672A RID: 26410
		public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State electrobankLost;

		// Token: 0x0400672B RID: 26411
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.TargetParameter dupe;

		// Token: 0x0400672C RID: 26412
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.TargetParameter electrobankSource;

		// Token: 0x0400672D RID: 26413
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.TargetParameter lastDepletedElectrobankFound;

		// Token: 0x0400672E RID: 26414
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.TargetParameter pickedUpElectrobank;

		// Token: 0x0400672F RID: 26415
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.TargetParameter messstation;

		// Token: 0x04006730 RID: 26416
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.TargetParameter safeLocation;

		// Token: 0x04006731 RID: 26417
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.FloatParameter actualunits;

		// Token: 0x04006732 RID: 26418
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.FloatParameter amountRequested;

		// Token: 0x04006733 RID: 26419
		public StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.IntParameter safeCellIndex;

		// Token: 0x04006734 RID: 26420
		public KAnim.Build.Symbol defaultElectrobankSymbol;

		// Token: 0x04006735 RID: 26421
		public KAnim.Build.Symbol depletedElectrobankSymbol;

		// Token: 0x04006736 RID: 26422
		private const float ROOM_EFFECT_DURATION = 1800f;

		// Token: 0x0200266F RID: 9839
		public class RemoveDepletedBatteries : GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State
		{
			// Token: 0x0600C3AC RID: 50092 RVA: 0x0040C448 File Offset: 0x0040A648
			public ReloadElectrobankChore.States.RemoveDepletedBatteries InitializeStates(GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State nextState)
			{
				base.DefaultState(this.animate).EnterTransition(nextState, (ReloadElectrobankChore.Instance smi) => !ReloadElectrobankChore.HasAnyDepletedBattery(smi));
				this.animate.ToggleAnims("anim_bionic_kanim", 0f).PlayAnim("discharge", KAnim.PlayMode.Once).Enter("Add Symbol Override", delegate(ReloadElectrobankChore.Instance smi)
				{
					smi.ShowElectrobankSymbol(true, smi.sm.depletedElectrobankSymbol);
				})
					.Exit("Revert Symbol Override", delegate(ReloadElectrobankChore.Instance smi)
					{
						smi.ShowElectrobankSymbol(false, smi.sm.depletedElectrobankSymbol);
					})
					.OnAnimQueueComplete(this.end);
				this.end.Enter(new StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State.Callback(ReloadElectrobankChore.RemoveDepletedElectrobank)).EnterTransition(this.animate, new StateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.Transition.ConditionCallback(ReloadElectrobankChore.HasAnyDepletedBattery)).GoTo(nextState);
				return this;
			}

			// Token: 0x0400AADC RID: 43740
			public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State animate;

			// Token: 0x0400AADD RID: 43741
			public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State end;
		}

		// Token: 0x02002670 RID: 9840
		public interface IInstallBatteryAnim
		{
			// Token: 0x0600C3AE RID: 50094
			string GetBank();

			// Token: 0x0600C3AF RID: 50095
			string GetPrefix(ReloadElectrobankChore.Instance smi, ReloadElectrobankChore.States.IInstallBatteryAnim.Anim anim);

			// Token: 0x0600C3B0 RID: 50096
			bool ForceFacing();

			// Token: 0x0200387A RID: 14458
			public enum Anim
			{
				// Token: 0x0400E45F RID: 58463
				Pre,
				// Token: 0x0400E460 RID: 58464
				Loop,
				// Token: 0x0400E461 RID: 58465
				Pst
			}
		}

		// Token: 0x02002671 RID: 9841
		public class DefaultInstallBatteryAnim : ReloadElectrobankChore.States.IInstallBatteryAnim
		{
			// Token: 0x0600C3B1 RID: 50097 RVA: 0x0040C543 File Offset: 0x0040A743
			public string GetBank()
			{
				return "anim_bionic_kanim";
			}

			// Token: 0x0600C3B2 RID: 50098 RVA: 0x0040C54A File Offset: 0x0040A74A
			public string GetPrefix(ReloadElectrobankChore.Instance _smi, ReloadElectrobankChore.States.IInstallBatteryAnim.Anim _anim)
			{
				return "consume";
			}

			// Token: 0x0600C3B3 RID: 50099 RVA: 0x0040C551 File Offset: 0x0040A751
			public bool ForceFacing()
			{
				return false;
			}
		}

		// Token: 0x02002672 RID: 9842
		public class MessStationInstallBatteryAnim : ReloadElectrobankChore.States.IInstallBatteryAnim
		{
			// Token: 0x0600C3B5 RID: 50101 RVA: 0x0040C55C File Offset: 0x0040A75C
			public string GetBank()
			{
				return "anim_bionic_eat_table_kanim";
			}

			// Token: 0x0600C3B6 RID: 50102 RVA: 0x0040C564 File Offset: 0x0040A764
			public string GetPrefix(ReloadElectrobankChore.Instance smi, ReloadElectrobankChore.States.IInstallBatteryAnim.Anim anim)
			{
				MinionResume component = smi.GetComponent<MinionResume>();
				bool flag = component != null && component.CurrentHat != null;
				bool flag2 = false;
				GameObject gameObject = smi.sm.messstation.Get(smi);
				if (gameObject != null)
				{
					MessStation component2 = gameObject.GetComponent<MessStation>();
					if (component2 != null && component2.HasSalt)
					{
						flag2 = true;
					}
				}
				if (flag2 && flag)
				{
					return "salt_hat";
				}
				if (flag2)
				{
					return "salt";
				}
				if (!flag)
				{
					return "working";
				}
				if (anim == ReloadElectrobankChore.States.IInstallBatteryAnim.Anim.Loop)
				{
					return "working";
				}
				return "hat";
			}

			// Token: 0x0600C3B7 RID: 50103 RVA: 0x0040C5F4 File Offset: 0x0040A7F4
			public bool ForceFacing()
			{
				return true;
			}
		}

		// Token: 0x02002673 RID: 9843
		public class InstallBattery : GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State
		{
			// Token: 0x0600C3B9 RID: 50105 RVA: 0x0040C600 File Offset: 0x0040A800
			public ReloadElectrobankChore.States.InstallBattery InitializeStates(GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State nextState, ReloadElectrobankChore.States.IInstallBatteryAnim anim)
			{
				base.DefaultState(this.pre).ToggleAnims(anim.GetBank(), 0f).Enter("Add Symbol Override", delegate(ReloadElectrobankChore.Instance smi)
				{
					smi.StowElectrobank(false);
					if (anim.ForceFacing())
					{
						Facing component = smi.GetComponent<Facing>();
						if (component != null)
						{
							component.SetFacing(false);
						}
					}
				})
					.Exit("Revert Symbol Override", delegate(ReloadElectrobankChore.Instance smi)
					{
						smi.StowElectrobank(true);
					});
				this.pre.PlayAnim((ReloadElectrobankChore.Instance smi) => anim.GetPrefix(smi, ReloadElectrobankChore.States.IInstallBatteryAnim.Anim.Pre) + "_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.loop).ScheduleGoTo(5f, this.loop);
				this.loop.PlayAnim((ReloadElectrobankChore.Instance smi) => anim.GetPrefix(smi, ReloadElectrobankChore.States.IInstallBatteryAnim.Anim.Loop) + "_loop", KAnim.PlayMode.Loop).ScheduleGoTo(4.333f, this.pst);
				this.pst.PlayAnim((ReloadElectrobankChore.Instance smi) => anim.GetPrefix(smi, ReloadElectrobankChore.States.IInstallBatteryAnim.Anim.Pst) + "_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(nextState).ScheduleGoTo(5f, nextState);
				return this;
			}

			// Token: 0x0400AADE RID: 43742
			public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State pre;

			// Token: 0x0400AADF RID: 43743
			public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State loop;

			// Token: 0x0400AAE0 RID: 43744
			public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State pst;
		}

		// Token: 0x02002674 RID: 9844
		public class InstallAtMessStation : GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State
		{
			// Token: 0x0400AAE1 RID: 43745
			public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.ApproachSubState<MessStation> approach;

			// Token: 0x0400AAE2 RID: 43746
			public ReloadElectrobankChore.States.RemoveDepletedBatteries removeDepletedBatteries;

			// Token: 0x0400AAE3 RID: 43747
			public ReloadElectrobankChore.States.InstallBattery install;
		}

		// Token: 0x02002675 RID: 9845
		public class InstallAtSafeLocation : GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.State
		{
			// Token: 0x0400AAE4 RID: 43748
			public GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.ApproachSubState<IApproachable> approach;

			// Token: 0x0400AAE5 RID: 43749
			public ReloadElectrobankChore.States.RemoveDepletedBatteries removeDepletedBatteries;

			// Token: 0x0400AAE6 RID: 43750
			public ReloadElectrobankChore.States.InstallBattery install;
		}
	}

	// Token: 0x020012B3 RID: 4787
	public class Instance : GameStateMachine<ReloadElectrobankChore.States, ReloadElectrobankChore.Instance, ReloadElectrobankChore, object>.GameInstance
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06008770 RID: 34672 RVA: 0x00344101 File Offset: 0x00342301
		public BionicBatteryMonitor.Instance batteryMonitor
		{
			get
			{
				return base.sm.dupe.Get(this).GetSMI<BionicBatteryMonitor.Instance>();
			}
		}

		// Token: 0x06008771 RID: 34673 RVA: 0x00344119 File Offset: 0x00342319
		public Instance(ReloadElectrobankChore master, GameObject duplicant)
			: base(master)
		{
		}

		// Token: 0x06008772 RID: 34674 RVA: 0x00344122 File Offset: 0x00342322
		public void UpdateMessStation()
		{
			base.sm.messstation.Set(EatChore.StatesInstance.GetPreferredMessStation(base.sm.dupe.Get(this).GetComponent<MinionIdentity>()), this);
		}

		// Token: 0x06008773 RID: 34675 RVA: 0x00344150 File Offset: 0x00342350
		public void ShowElectrobankSymbol(bool show, KAnim.Build.Symbol symbol)
		{
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			if (show)
			{
				component.AddSymbolOverride(ReloadElectrobankChore.Instance.SYMBOL_NAME, symbol, 0);
			}
			else
			{
				component.RemoveSymbolOverride(ReloadElectrobankChore.Instance.SYMBOL_NAME, 0);
			}
			base.GetComponent<KBatchedAnimController>().SetSymbolVisiblity(ReloadElectrobankChore.Instance.SYMBOL_NAME, show);
		}

		// Token: 0x06008774 RID: 34676 RVA: 0x003441A4 File Offset: 0x003423A4
		public void StowElectrobank(bool stow)
		{
			GameObject gameObject = base.sm.pickedUpElectrobank.Get(this);
			ReloadElectrobankChore.SetStoredItemVisibility(gameObject, stow);
			KAnim.Build.Symbol symbol = ((gameObject != null) ? gameObject.GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbolByIndex(0U) : base.sm.defaultElectrobankSymbol);
			this.ShowElectrobankSymbol(!stow, symbol);
		}

		// Token: 0x04006737 RID: 26423
		private static readonly string SYMBOL_NAME = "object";
	}
}
