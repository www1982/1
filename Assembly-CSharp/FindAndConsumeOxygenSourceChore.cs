using System;
using System.Collections.Generic;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x02000483 RID: 1155
public class FindAndConsumeOxygenSourceChore : Chore<FindAndConsumeOxygenSourceChore.Instance>
{
	// Token: 0x06001855 RID: 6229 RVA: 0x000878AC File Offset: 0x00085AAC
	public FindAndConsumeOxygenSourceChore(IStateMachineTarget target, bool critical)
		: base(critical ? Db.Get().ChoreTypes.FindOxygenSourceItem_Critical : Db.Get().ChoreTypes.FindOxygenSourceItem, target, target.GetComponent<ChoreProvider>(), false, null, null, null, critical ? PriorityScreen.PriorityClass.compulsory : PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new FindAndConsumeOxygenSourceChore.Instance(this, target.gameObject);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(FindAndConsumeOxygenSourceChore.OxygenSourceItemIsNotNull, null);
	}

	// Token: 0x06001856 RID: 6230 RVA: 0x0008792C File Offset: 0x00085B2C
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			global::Debug.LogError("FindAndConsumeOxygenSourceChore null context.consumer");
			return;
		}
		BionicOxygenTankMonitor.Instance smi = context.consumerState.consumer.GetSMI<BionicOxygenTankMonitor.Instance>();
		if (smi == null)
		{
			global::Debug.LogError("FindAndConsumeOxygenSourceChore null BionicOxygenTankMonitor.Instance");
			return;
		}
		Pickupable closestOxygenSource = smi.GetClosestOxygenSource();
		if (closestOxygenSource == null)
		{
			global::Debug.LogError("FindAndConsumeOxygenSourceChore null oxygenSourceItem.gameObject");
			return;
		}
		base.smi.sm.oxygenSourceItem.Set(closestOxygenSource.gameObject, base.smi, false);
		base.smi.sm.amountRequested.Set(Mathf.Min(smi.SpaceAvailableInTank, closestOxygenSource.UnreservedAmount), base.smi, false);
		base.smi.sm.dupe.Set(context.consumerState.consumer, base.smi);
		base.Begin(context);
	}

	// Token: 0x06001857 RID: 6231 RVA: 0x00087A0F File Offset: 0x00085C0F
	public static bool IsNotAllowedByScheduleAndChoreIsNotCritical(FindAndConsumeOxygenSourceChore.Instance smi)
	{
		return !FindAndConsumeOxygenSourceChore.IsCriticalChore(smi) && !FindAndConsumeOxygenSourceChore.IsAllowedBySchedule(smi);
	}

	// Token: 0x06001858 RID: 6232 RVA: 0x00087A24 File Offset: 0x00085C24
	public static bool IsAllowedBySchedule(FindAndConsumeOxygenSourceChore.Instance smi)
	{
		return BionicOxygenTankMonitor.IsAllowedToSeekOxygenBySchedule(smi.oxygenTankMonitor);
	}

	// Token: 0x06001859 RID: 6233 RVA: 0x00087A31 File Offset: 0x00085C31
	public static bool IsCriticalChore(FindAndConsumeOxygenSourceChore.Instance smi)
	{
		return smi.master.choreType == Db.Get().ChoreTypes.FindOxygenSourceItem_Critical;
	}

	// Token: 0x0600185A RID: 6234 RVA: 0x00087A50 File Offset: 0x00085C50
	public static void ExtractOxygenFromItem(FindAndConsumeOxygenSourceChore.Instance smi)
	{
		GameObject gameObject = smi.sm.pickedUpItem.Get(smi);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		if (component.Element.IsGas)
		{
			Storage[] components = smi.gameObject.GetComponents<Storage>();
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i] != smi.oxygenTankMonitor.storage)
				{
					List<GameObject> list = new List<GameObject>();
					components[i].Find(GameTags.Breathable, list);
					using (List<GameObject>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current != null)
							{
								float num;
								SimUtil.DiseaseInfo diseaseInfo;
								float num2;
								components[i].ConsumeAndGetDisease(component.Element.tag, component.Mass, out num, out diseaseInfo, out num2);
								smi.oxygenTankMonitor.storage.AddGasChunk(component.Element.id, num, num2, diseaseInfo.idx, diseaseInfo.count, false, true);
								break;
							}
						}
					}
				}
			}
			return;
		}
		SimHashes simHashes = SimHashes.Oxygen;
		if (ElementLoader.GetElement(component.Element.sublimateId.CreateTag()).HasTag(GameTags.Breathable))
		{
			simHashes = component.Element.sublimateId;
		}
		smi.oxygenTankMonitor.storage.AddGasChunk(simHashes, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, false, true);
		Util.KDestroyGameObject(gameObject);
	}

	// Token: 0x0600185B RID: 6235 RVA: 0x00087BD4 File Offset: 0x00085DD4
	public static void SetOverrideAnimSymbol(FindAndConsumeOxygenSourceChore.Instance smi, bool overriding)
	{
		GameObject gameObject = smi.sm.pickedUpItem.Get(smi);
		if (gameObject != null)
		{
			KBatchedAnimTracker component = gameObject.GetComponent<KBatchedAnimTracker>();
			if (component != null)
			{
				component.enabled = !overriding;
			}
			Storage.MakeItemInvisible(gameObject, overriding, false);
		}
		if (!overriding)
		{
			smi.RemoveSymbolOverrideObject();
			return;
		}
		if (gameObject != null)
		{
			PrimaryElement component2 = gameObject.GetComponent<PrimaryElement>();
			smi.ShowBottleSymbolOverrideObject(component2.Element);
		}
	}

	// Token: 0x0600185C RID: 6236 RVA: 0x00087C44 File Offset: 0x00085E44
	public static void TriggerOxygenItemLostSignal(FindAndConsumeOxygenSourceChore.Instance smi)
	{
		if (smi.oxygenTankMonitor != null)
		{
			smi.oxygenTankMonitor.sm.OxygenSourceItemLostSignal.Trigger(smi.oxygenTankMonitor);
		}
	}

	// Token: 0x0600185D RID: 6237 RVA: 0x00087C6C File Offset: 0x00085E6C
	public static float GetConsumeDuration(FindAndConsumeOxygenSourceChore.Instance smi)
	{
		float num = smi.sm.actualunits.Get(smi) / BionicOxygenTankMonitor.OXYGEN_TANK_CAPACITY_KG;
		return Mathf.Max(24f * num, 4.333f);
	}

	// Token: 0x04000E2B RID: 3627
	public const string CANISTER_BODY_SYMBOL_NAME = "canister";

	// Token: 0x04000E2C RID: 3628
	public const string CANISTER_CAP_SYMBOL_NAME = "cap";

	// Token: 0x04000E2D RID: 3629
	public const string CANISTER_CAP_COLOR_SYMBOL_NAME = "substance_tinter_cap";

	// Token: 0x04000E2E RID: 3630
	public const string CANISTER_BODY_COLOR_SYMBOL_NAME = "substance_tinter";

	// Token: 0x04000E2F RID: 3631
	public const float MAX_LOOP_DURATION = 24f;

	// Token: 0x04000E30 RID: 3632
	public const float MIN_LOOP_DURATION = 4.333f;

	// Token: 0x04000E31 RID: 3633
	public static readonly Chore.Precondition OxygenSourceItemIsNotNull = new Chore.Precondition
	{
		id = "OxygenSourceIsNotNull",
		description = DUPLICANTS.CHORES.PRECONDITIONS.EDIBLE_IS_NOT_NULL,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Pickupable closestOxygenSource = context.consumerState.consumer.GetSMI<BionicOxygenTankMonitor.Instance>().GetClosestOxygenSource();
			return closestOxygenSource != null && closestOxygenSource.UnreservedAmount > 0f;
		}
	};

	// Token: 0x02001284 RID: 4740
	public class States : GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore>
	{
		// Token: 0x060086C2 RID: 34498 RVA: 0x0033FF60 File Offset: 0x0033E160
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.fetch;
			base.Target(this.dupe);
			this.fetch.InitializeStates(this.dupe, this.oxygenSourceItem, this.pickedUpItem, this.amountRequested, this.actualunits, this.consume, null).OnTargetLost(this.oxygenSourceItem, this.oxygenSourceLost).ScheduleChange(this.scheduleFailure, new StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.Transition.ConditionCallback(FindAndConsumeOxygenSourceChore.IsNotAllowedByScheduleAndChoreIsNotCritical));
			this.consume.Target(this.pickedUpItem).OnTargetLost(this.pickedUpItem, this.oxygenSourceLost).Target(this.dupe)
				.ScheduleChange(this.scheduleFailure, new StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.Transition.ConditionCallback(FindAndConsumeOxygenSourceChore.IsNotAllowedByScheduleAndChoreIsNotCritical))
				.DefaultState(this.consume.pre)
				.ToggleAnims("anim_bionic_kanim", 0f)
				.ToggleTag(GameTags.RecoveringBreath)
				.Enter("Add Symbol Override", delegate(FindAndConsumeOxygenSourceChore.Instance smi)
				{
					FindAndConsumeOxygenSourceChore.SetOverrideAnimSymbol(smi, true);
				})
				.Exit("Revert Symbol Override", delegate(FindAndConsumeOxygenSourceChore.Instance smi)
				{
					FindAndConsumeOxygenSourceChore.SetOverrideAnimSymbol(smi, false);
				});
			this.consume.pre.PlayAnim("consume_canister_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.consume.loop).ScheduleGoTo(3f, this.consume.loop);
			this.consume.loop.PlayAnim("consume_canister_loop", KAnim.PlayMode.Loop).ScheduleGoTo(new Func<FindAndConsumeOxygenSourceChore.Instance, float>(FindAndConsumeOxygenSourceChore.GetConsumeDuration), this.consume.pst);
			this.consume.pst.PlayAnim("consume_canister_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.complete).ScheduleGoTo(3f, this.complete);
			this.complete.Enter(new StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State.Callback(FindAndConsumeOxygenSourceChore.ExtractOxygenFromItem)).ReturnSuccess();
			this.scheduleFailure.Target(this.dupe).ReturnFailure();
			this.oxygenSourceLost.Target(this.dupe).Enter(new StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State.Callback(FindAndConsumeOxygenSourceChore.TriggerOxygenItemLostSignal)).ReturnFailure();
		}

		// Token: 0x04006691 RID: 26257
		public GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.FetchSubState fetch;

		// Token: 0x04006692 RID: 26258
		public FindAndConsumeOxygenSourceChore.States.InstallState consume;

		// Token: 0x04006693 RID: 26259
		public GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State complete;

		// Token: 0x04006694 RID: 26260
		public GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State oxygenSourceLost;

		// Token: 0x04006695 RID: 26261
		public GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State scheduleFailure;

		// Token: 0x04006696 RID: 26262
		public StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.TargetParameter dupe;

		// Token: 0x04006697 RID: 26263
		public StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.TargetParameter oxygenSourceItem;

		// Token: 0x04006698 RID: 26264
		public StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.TargetParameter pickedUpItem;

		// Token: 0x04006699 RID: 26265
		public StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.FloatParameter actualunits;

		// Token: 0x0400669A RID: 26266
		public StateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.FloatParameter amountRequested;

		// Token: 0x02002651 RID: 9809
		public class InstallState : GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State
		{
			// Token: 0x0400AA5E RID: 43614
			public GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State pre;

			// Token: 0x0400AA5F RID: 43615
			public GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State loop;

			// Token: 0x0400AA60 RID: 43616
			public GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.State pst;
		}
	}

	// Token: 0x02001285 RID: 4741
	public class Instance : GameStateMachine<FindAndConsumeOxygenSourceChore.States, FindAndConsumeOxygenSourceChore.Instance, FindAndConsumeOxygenSourceChore, object>.GameInstance, BionicOxygenTankMonitor.IChore
	{
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060086C4 RID: 34500 RVA: 0x0034019F File Offset: 0x0033E39F
		public BionicOxygenTankMonitor.Instance oxygenTankMonitor
		{
			get
			{
				return base.sm.dupe.Get(this).GetSMI<BionicOxygenTankMonitor.Instance>();
			}
		}

		// Token: 0x060086C5 RID: 34501 RVA: 0x003401B7 File Offset: 0x0033E3B7
		public Instance(FindAndConsumeOxygenSourceChore master, GameObject duplicant)
			: base(master)
		{
		}

		// Token: 0x060086C6 RID: 34502 RVA: 0x003401C0 File Offset: 0x0033E3C0
		public bool IsConsumingOxygen()
		{
			return !base.IsInsideState(base.sm.fetch);
		}

		// Token: 0x060086C7 RID: 34503 RVA: 0x003401D8 File Offset: 0x0033E3D8
		public void ShowBottleSymbolOverrideObject(Element elementOfCanister)
		{
			if (this.canisterBodySymbolOverrideObject == null)
			{
				KAnimFile[] anims = elementOfCanister.substance.anims;
				GameObject gameObject = Util.NewGameObject(base.gameObject, "canister_symbol");
				gameObject.transform.SetParent(base.gameObject.transform, false);
				gameObject.SetActive(false);
				this.canisterBodySymbolOverrideObject = gameObject.AddComponent<KBatchedAnimController>();
				this.canisterBodySymbolOverrideObject.AnimFiles = anims;
				this.canisterBodySymbolOverrideObject.initialAnim = "idle1";
				this.canisterBodySymbolOverrideObject.SetSymbolVisiblity("cap", false);
				this.canisterBodySymbolOverrideObject.SetSymbolVisiblity("substance_tinter_cap", false);
				KBatchedAnimTracker kbatchedAnimTracker = gameObject.AddComponent<KBatchedAnimTracker>();
				kbatchedAnimTracker.symbol = new HashedString("canister");
				kbatchedAnimTracker.offset = Vector3.zero;
				kbatchedAnimTracker.matchParentOffset = true;
				kbatchedAnimTracker.forceAlwaysAlive = true;
				kbatchedAnimTracker.forceAlwaysVisible = true;
				gameObject.SetActive(true);
				Color32 colour = elementOfCanister.substance.colour;
				colour.a = byte.MaxValue;
				this.canisterBodySymbolOverrideObject.SetSymbolTint(new KAnimHashedString("substance_tinter"), colour);
			}
			if (this.canisterCapSymbolOverrideObject == null)
			{
				KAnimFile[] anims2 = elementOfCanister.substance.anims;
				GameObject gameObject2 = Util.NewGameObject(base.gameObject, "canister_cap_symbol");
				gameObject2.transform.SetParent(base.gameObject.transform, false);
				gameObject2.SetActive(false);
				this.canisterCapSymbolOverrideObject = gameObject2.AddComponent<KBatchedAnimController>();
				this.canisterCapSymbolOverrideObject.AnimFiles = anims2;
				this.canisterCapSymbolOverrideObject.initialAnim = "cap";
				KBatchedAnimTracker kbatchedAnimTracker2 = gameObject2.AddComponent<KBatchedAnimTracker>();
				kbatchedAnimTracker2.symbol = new HashedString("cap");
				kbatchedAnimTracker2.offset = Vector3.zero;
				kbatchedAnimTracker2.matchParentOffset = true;
				kbatchedAnimTracker2.forceAlwaysAlive = true;
				kbatchedAnimTracker2.forceAlwaysVisible = true;
				gameObject2.SetActive(true);
				Color32 colour2 = elementOfCanister.substance.colour;
				colour2.a = byte.MaxValue;
				this.canisterCapSymbolOverrideObject.SetSymbolTint(new KAnimHashedString("substance_tinter_cap"), colour2);
			}
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			bool flag;
			Vector3 vector = component.GetSymbolTransform("canister", out flag).GetColumn(3);
			vector.z = this.canisterBodySymbolOverrideObject.transform.parent.position.z - 0.01f;
			this.canisterBodySymbolOverrideObject.transform.position = vector;
			bool flag2;
			Vector3 vector2 = component.GetSymbolTransform("canister", out flag2).GetColumn(3);
			vector2.z = vector.z - 0.01f;
			this.canisterCapSymbolOverrideObject.transform.position = vector2;
			component.SetSymbolVisiblity("canister", false);
			component.SetSymbolVisiblity("cap", false);
		}

		// Token: 0x060086C8 RID: 34504 RVA: 0x003404B4 File Offset: 0x0033E6B4
		public void RemoveSymbolOverrideObject()
		{
			if (this.canisterBodySymbolOverrideObject != null)
			{
				this.canisterBodySymbolOverrideObject.gameObject.DeleteObject();
				this.canisterBodySymbolOverrideObject = null;
			}
			if (this.canisterCapSymbolOverrideObject != null)
			{
				this.canisterCapSymbolOverrideObject.gameObject.DeleteObject();
				this.canisterCapSymbolOverrideObject = null;
			}
		}

		// Token: 0x060086C9 RID: 34505 RVA: 0x0034050B File Offset: 0x0033E70B
		protected override void OnCleanUp()
		{
			this.RemoveSymbolOverrideObject();
			base.OnCleanUp();
		}

		// Token: 0x0400669B RID: 26267
		public KBatchedAnimController canisterBodySymbolOverrideObject;

		// Token: 0x0400669C RID: 26268
		public KBatchedAnimController canisterCapSymbolOverrideObject;
	}
}
