using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000507 RID: 1287
public abstract class GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> where StateMachineType : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> where StateMachineInstanceType : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameInstance where MasterType : IStateMachineTarget
{
	// Token: 0x06001B89 RID: 7049 RVA: 0x00096C77 File Offset: 0x00094E77
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.InitializeStates(out default_state);
	}

	// Token: 0x06001B8A RID: 7050 RVA: 0x00096C80 File Offset: 0x00094E80
	public static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback And(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback first_condition, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback second_condition)
	{
		return (StateMachineInstanceType smi) => first_condition(smi) && second_condition(smi);
	}

	// Token: 0x06001B8B RID: 7051 RVA: 0x00096CA0 File Offset: 0x00094EA0
	public static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback Or(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback first_condition, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback second_condition)
	{
		return (StateMachineInstanceType smi) => first_condition(smi) || second_condition(smi);
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x00096CC0 File Offset: 0x00094EC0
	public static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback Not(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback transition_cb)
	{
		return (StateMachineInstanceType smi) => !transition_cb(smi);
	}

	// Token: 0x06001B8D RID: 7053 RVA: 0x00096CD9 File Offset: 0x00094ED9
	public override void BindStates()
	{
		base.BindState(null, this.root, "root");
		base.BindStates(this.root, this);
	}

	// Token: 0x04001038 RID: 4152
	public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State root = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State();

	// Token: 0x04001039 RID: 4153
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<bool>.Callback IsFalse = (StateMachineInstanceType smi, bool p) => !p;

	// Token: 0x0400103A RID: 4154
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<bool>.Callback IsTrue = (StateMachineInstanceType smi, bool p) => p;

	// Token: 0x0400103B RID: 4155
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsZero = (StateMachineInstanceType smi, float p) => p == 0f;

	// Token: 0x0400103C RID: 4156
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsLTZero = (StateMachineInstanceType smi, float p) => p < 0f;

	// Token: 0x0400103D RID: 4157
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsLTEZero = (StateMachineInstanceType smi, float p) => p <= 0f;

	// Token: 0x0400103E RID: 4158
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsGTZero = (StateMachineInstanceType smi, float p) => p > 0f;

	// Token: 0x0400103F RID: 4159
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsGTEZero = (StateMachineInstanceType smi, float p) => p >= 0f;

	// Token: 0x04001040 RID: 4160
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsOne = (StateMachineInstanceType smi, float p) => p == 1f;

	// Token: 0x04001041 RID: 4161
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsLTOne = (StateMachineInstanceType smi, float p) => p < 1f;

	// Token: 0x04001042 RID: 4162
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsLTEOne = (StateMachineInstanceType smi, float p) => p <= 1f;

	// Token: 0x04001043 RID: 4163
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsGTOne = (StateMachineInstanceType smi, float p) => p > 1f;

	// Token: 0x04001044 RID: 4164
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Callback IsGTEOne = (StateMachineInstanceType smi, float p) => p >= 1f;

	// Token: 0x04001045 RID: 4165
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<GameObject>.Callback IsNotNull = (StateMachineInstanceType smi, GameObject p) => p != null;

	// Token: 0x04001046 RID: 4166
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<GameObject>.Callback IsNull = (StateMachineInstanceType smi, GameObject p) => p == null;

	// Token: 0x04001047 RID: 4167
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<int>.Callback IsZero_Int = (StateMachineInstanceType smi, int p) => p == 0;

	// Token: 0x04001048 RID: 4168
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<int>.Callback IsLTEOne_Int = (StateMachineInstanceType smi, int p) => p <= 1;

	// Token: 0x04001049 RID: 4169
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<int>.Callback IsGTOne_Int = (StateMachineInstanceType smi, int p) => p > 1;

	// Token: 0x0400104A RID: 4170
	protected static StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<int>.Callback IsGTZero_Int = (StateMachineInstanceType smi, int p) => p > 0;

	// Token: 0x02001343 RID: 4931
	public class PreLoopPostState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x040068F6 RID: 26870
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State pre;

		// Token: 0x040068F7 RID: 26871
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State loop;

		// Token: 0x040068F8 RID: 26872
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State pst;
	}

	// Token: 0x02001344 RID: 4932
	public class WorkingState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x040068F9 RID: 26873
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State waiting;

		// Token: 0x040068FA RID: 26874
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State working_pre;

		// Token: 0x040068FB RID: 26875
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State working_loop;

		// Token: 0x040068FC RID: 26876
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State working_pst;
	}

	// Token: 0x02001345 RID: 4933
	public class GameInstance : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance
	{
		// Token: 0x06008930 RID: 35120 RVA: 0x0034B0C1 File Offset: 0x003492C1
		public void Queue(string anim, KAnim.PlayMode mode = KAnim.PlayMode.Once)
		{
			base.smi.GetComponent<KBatchedAnimController>().Queue(anim, mode, 1f, 0f);
		}

		// Token: 0x06008931 RID: 35121 RVA: 0x0034B0E9 File Offset: 0x003492E9
		public void Play(string anim, KAnim.PlayMode mode = KAnim.PlayMode.Once)
		{
			base.smi.GetComponent<KBatchedAnimController>().Play(anim, mode, 1f, 0f);
		}

		// Token: 0x06008932 RID: 35122 RVA: 0x0034B111 File Offset: 0x00349311
		public GameInstance(MasterType master, DefType def)
			: base(master)
		{
			base.def = def;
		}

		// Token: 0x06008933 RID: 35123 RVA: 0x0034B121 File Offset: 0x00349321
		public GameInstance(MasterType master)
			: base(master)
		{
		}
	}

	// Token: 0x02001346 RID: 4934
	public class TagTransitionData : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition
	{
		// Token: 0x06008934 RID: 35124 RVA: 0x0034B12A File Offset: 0x0034932A
		public TagTransitionData(string name, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State source_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state, int idx, Tag[] tags, bool on_remove, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target, Func<StateMachineInstanceType, Tag[]> tags_callback = null)
			: base(name, source_state, target_state, idx, null)
		{
			this.tags = tags;
			this.onRemove = on_remove;
			this.target = target;
			this.tags_callback = tags_callback;
		}

		// Token: 0x06008935 RID: 35125 RVA: 0x0034B158 File Offset: 0x00349358
		public override void Evaluate(StateMachine.Instance smi)
		{
			StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
			global::Debug.Assert(stateMachineInstanceType != null);
			if (!this.onRemove)
			{
				if (!this.HasAllTags(stateMachineInstanceType))
				{
					return;
				}
			}
			else if (this.HasAnyTags(stateMachineInstanceType))
			{
				return;
			}
			this.ExecuteTransition(stateMachineInstanceType);
		}

		// Token: 0x06008936 RID: 35126 RVA: 0x0034B1A2 File Offset: 0x003493A2
		private bool HasAllTags(StateMachineInstanceType smi)
		{
			return this.target.Get(smi).GetComponent<KPrefabID>().HasAllTags((this.tags_callback != null) ? this.tags_callback(smi) : this.tags);
		}

		// Token: 0x06008937 RID: 35127 RVA: 0x0034B1D6 File Offset: 0x003493D6
		private bool HasAnyTags(StateMachineInstanceType smi)
		{
			return this.target.Get(smi).GetComponent<KPrefabID>().HasAnyTags((this.tags_callback != null) ? this.tags_callback(smi) : this.tags);
		}

		// Token: 0x06008938 RID: 35128 RVA: 0x0034B20A File Offset: 0x0034940A
		private void ExecuteTransition(StateMachineInstanceType smi)
		{
			if (this.is_executing)
			{
				return;
			}
			this.is_executing = true;
			smi.GoTo(this.targetState);
			this.is_executing = false;
		}

		// Token: 0x06008939 RID: 35129 RVA: 0x0034B234 File Offset: 0x00349434
		private void OnCallback(StateMachineInstanceType smi)
		{
			if (this.target.Get(smi) == null)
			{
				return;
			}
			if (!this.onRemove)
			{
				if (!this.HasAllTags(smi))
				{
					return;
				}
			}
			else if (this.HasAnyTags(smi))
			{
				return;
			}
			this.ExecuteTransition(smi);
		}

		// Token: 0x0600893A RID: 35130 RVA: 0x0034B270 File Offset: 0x00349470
		public override StateMachine.BaseTransition.Context Register(StateMachine.Instance smi)
		{
			StateMachineInstanceType smi_internal = smi as StateMachineInstanceType;
			global::Debug.Assert(smi_internal != null);
			StateMachine.BaseTransition.Context context = base.Register(smi_internal);
			context.handlerId = this.target.Get(smi_internal).Subscribe(-1582839653, delegate(object data)
			{
				this.OnCallback(smi_internal);
			});
			return context;
		}

		// Token: 0x0600893B RID: 35131 RVA: 0x0034B2F0 File Offset: 0x003494F0
		public override void Unregister(StateMachine.Instance smi, StateMachine.BaseTransition.Context context)
		{
			StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
			global::Debug.Assert(stateMachineInstanceType != null);
			base.Unregister(stateMachineInstanceType, context);
			if (this.target.Get(stateMachineInstanceType) != null)
			{
				this.target.Get(stateMachineInstanceType).Unsubscribe(context.handlerId);
			}
		}

		// Token: 0x040068FD RID: 26877
		private Tag[] tags;

		// Token: 0x040068FE RID: 26878
		private bool onRemove;

		// Token: 0x040068FF RID: 26879
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target;

		// Token: 0x04006900 RID: 26880
		private bool is_executing;

		// Token: 0x04006901 RID: 26881
		private Func<StateMachineInstanceType, Tag[]> tags_callback;
	}

	// Token: 0x02001347 RID: 4935
	public class EventTransitionData : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition
	{
		// Token: 0x0600893C RID: 35132 RVA: 0x0034B34F File Offset: 0x0034954F
		public EventTransitionData(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State source_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state, int idx, GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target)
			: base(evt.ToString(), source_state, target_state, idx, condition)
		{
			this.evtId = evt;
			this.target = target;
			this.globalEventSystemCallback = global_event_system_callback;
		}

		// Token: 0x0600893D RID: 35133 RVA: 0x0034B384 File Offset: 0x00349584
		public override void Evaluate(StateMachine.Instance smi)
		{
			StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
			global::Debug.Assert(stateMachineInstanceType != null);
			if (this.condition != null && this.condition(stateMachineInstanceType))
			{
				this.ExecuteTransition(stateMachineInstanceType);
			}
		}

		// Token: 0x0600893E RID: 35134 RVA: 0x0034B3C8 File Offset: 0x003495C8
		private void ExecuteTransition(StateMachineInstanceType smi)
		{
			smi.GoTo(this.targetState);
		}

		// Token: 0x0600893F RID: 35135 RVA: 0x0034B3DB File Offset: 0x003495DB
		private void OnCallback(StateMachineInstanceType smi)
		{
			if (this.condition == null || this.condition(smi))
			{
				this.ExecuteTransition(smi);
			}
		}

		// Token: 0x06008940 RID: 35136 RVA: 0x0034B3FC File Offset: 0x003495FC
		public override StateMachine.BaseTransition.Context Register(StateMachine.Instance smi)
		{
			StateMachineInstanceType smi_internal = smi as StateMachineInstanceType;
			global::Debug.Assert(smi_internal != null);
			StateMachine.BaseTransition.Context context = base.Register(smi_internal);
			Action<object> action = delegate(object d)
			{
				this.OnCallback(smi_internal);
			};
			GameObject gameObject;
			if (this.globalEventSystemCallback != null)
			{
				gameObject = this.globalEventSystemCallback(smi_internal).gameObject;
			}
			else
			{
				gameObject = this.target.Get(smi_internal);
				if (gameObject == null)
				{
					throw new InvalidOperationException("TargetParameter: " + this.target.name + " is null");
				}
			}
			context.handlerId = gameObject.Subscribe((int)this.evtId, action);
			return context;
		}

		// Token: 0x06008941 RID: 35137 RVA: 0x0034B4CC File Offset: 0x003496CC
		public override void Unregister(StateMachine.Instance smi, StateMachine.BaseTransition.Context context)
		{
			StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
			global::Debug.Assert(stateMachineInstanceType != null);
			base.Unregister(stateMachineInstanceType, context);
			GameObject gameObject = null;
			if (this.globalEventSystemCallback != null)
			{
				KMonoBehaviour kmonoBehaviour = this.globalEventSystemCallback(stateMachineInstanceType);
				if (kmonoBehaviour != null)
				{
					gameObject = kmonoBehaviour.gameObject;
				}
			}
			else
			{
				gameObject = this.target.Get(stateMachineInstanceType);
			}
			if (gameObject != null)
			{
				gameObject.Unsubscribe(context.handlerId);
			}
		}

		// Token: 0x04006902 RID: 26882
		private GameHashes evtId;

		// Token: 0x04006903 RID: 26883
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target;

		// Token: 0x04006904 RID: 26884
		private Func<StateMachineInstanceType, KMonoBehaviour> globalEventSystemCallback;
	}

	// Token: 0x02001348 RID: 4936
	public new class State : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x06008942 RID: 35138 RVA: 0x0034B54C File Offset: 0x0034974C
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter GetStateTarget()
		{
			if (this.stateTarget != null)
			{
				return this.stateTarget;
			}
			if (this.parent != null)
			{
				return ((GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)this.parent).GetStateTarget();
			}
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter targetParameter = this.sm.stateTarget;
			if (targetParameter == null)
			{
				return this.sm.masterTarget;
			}
			return targetParameter;
		}

		// Token: 0x06008943 RID: 35139 RVA: 0x0034B5A8 File Offset: 0x003497A8
		public int CreateDataTableEntry()
		{
			StateMachineType stateMachineType = this.sm;
			int dataTableSize = stateMachineType.dataTableSize;
			stateMachineType.dataTableSize = dataTableSize + 1;
			return dataTableSize;
		}

		// Token: 0x06008944 RID: 35140 RVA: 0x0034B5D0 File Offset: 0x003497D0
		public int CreateUpdateTableEntry()
		{
			StateMachineType stateMachineType = this.sm;
			int updateTableSize = stateMachineType.updateTableSize;
			stateMachineType.updateTableSize = updateTableSize + 1;
			return updateTableSize;
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06008945 RID: 35141 RVA: 0x0034B5F8 File Offset: 0x003497F8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State root
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06008946 RID: 35142 RVA: 0x0034B5FB File Offset: 0x003497FB
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoNothing()
		{
			return this;
		}

		// Token: 0x06008947 RID: 35143 RVA: 0x0034B600 File Offset: 0x00349800
		private static List<StateMachine.Action> AddAction(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback, List<StateMachine.Action> actions, bool add_to_end)
		{
			if (actions == null)
			{
				actions = new List<StateMachine.Action>();
			}
			StateMachine.Action action = new StateMachine.Action(name, callback);
			if (add_to_end)
			{
				actions.Add(action);
			}
			else
			{
				actions.Insert(0, action);
			}
			return actions;
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06008948 RID: 35144 RVA: 0x0034B635 File Offset: 0x00349835
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State master
		{
			get
			{
				this.stateTarget = this.sm.masterTarget;
				return this;
			}
		}

		// Token: 0x06008949 RID: 35145 RVA: 0x0034B64E File Offset: 0x0034984E
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Target(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target)
		{
			this.stateTarget = target;
			return this;
		}

		// Token: 0x0600894A RID: 35146 RVA: 0x0034B658 File Offset: 0x00349858
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Update(Action<StateMachineInstanceType, float> callback, UpdateRate update_rate = UpdateRate.SIM_200ms, bool load_balance = false)
		{
			return this.Update(this.sm.name + "." + this.name, callback, update_rate, load_balance);
		}

		// Token: 0x0600894B RID: 35147 RVA: 0x0034B683 File Offset: 0x00349883
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BatchUpdate(UpdateBucketWithUpdater<StateMachineInstanceType>.BatchUpdateDelegate batch_update, UpdateRate update_rate = UpdateRate.SIM_200ms)
		{
			return this.BatchUpdate(this.sm.name + "." + this.name, batch_update, update_rate);
		}

		// Token: 0x0600894C RID: 35148 RVA: 0x0034B6AD File Offset: 0x003498AD
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Enter(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			return this.Enter("Enter", callback);
		}

		// Token: 0x0600894D RID: 35149 RVA: 0x0034B6BB File Offset: 0x003498BB
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Exit(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			return this.Exit("Exit", callback);
		}

		// Token: 0x0600894E RID: 35150 RVA: 0x0034B6CC File Offset: 0x003498CC
		private GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InternalUpdate(string name, UpdateBucketWithUpdater<StateMachineInstanceType>.IUpdater bucket_updater, UpdateRate update_rate, bool load_balance, UpdateBucketWithUpdater<StateMachineInstanceType>.BatchUpdateDelegate batch_update = null)
		{
			int num = this.CreateUpdateTableEntry();
			if (this.updateActions == null)
			{
				this.updateActions = new List<StateMachine.UpdateAction>();
			}
			StateMachine.UpdateAction updateAction = default(StateMachine.UpdateAction);
			updateAction.updateTableIdx = num;
			updateAction.updateRate = update_rate;
			updateAction.updater = bucket_updater;
			int num2 = 1;
			if (load_balance)
			{
				num2 = Singleton<StateMachineUpdater>.Instance.GetFrameCount(update_rate);
			}
			updateAction.buckets = new StateMachineUpdater.BaseUpdateBucket[num2];
			for (int i = 0; i < num2; i++)
			{
				UpdateBucketWithUpdater<StateMachineInstanceType> updateBucketWithUpdater = new UpdateBucketWithUpdater<StateMachineInstanceType>(name);
				updateBucketWithUpdater.batch_update_delegate = batch_update;
				Singleton<StateMachineUpdater>.Instance.AddBucket(update_rate, updateBucketWithUpdater);
				updateAction.buckets[i] = updateBucketWithUpdater;
			}
			this.updateActions.Add(updateAction);
			return this;
		}

		// Token: 0x0600894F RID: 35151 RVA: 0x0034B774 File Offset: 0x00349974
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State UpdateTransition(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State destination_state, Func<StateMachineInstanceType, float, bool> callback, UpdateRate update_rate = UpdateRate.SIM_200ms, bool load_balance = false)
		{
			Action<StateMachineInstanceType, float> checkCallback = delegate(StateMachineInstanceType smi, float dt)
			{
				if (callback(smi, dt))
				{
					smi.GoTo(destination_state);
				}
			};
			this.Enter(delegate(StateMachineInstanceType smi)
			{
				checkCallback(smi, 0f);
			});
			this.Update(checkCallback, update_rate, load_balance);
			return this;
		}

		// Token: 0x06008950 RID: 35152 RVA: 0x0034B7CB File Offset: 0x003499CB
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Update(string name, Action<StateMachineInstanceType, float> callback, UpdateRate update_rate = UpdateRate.SIM_200ms, bool load_balance = false)
		{
			return this.InternalUpdate(name, new BucketUpdater<StateMachineInstanceType>(callback), update_rate, load_balance, null);
		}

		// Token: 0x06008951 RID: 35153 RVA: 0x0034B7DE File Offset: 0x003499DE
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BatchUpdate(string name, UpdateBucketWithUpdater<StateMachineInstanceType>.BatchUpdateDelegate batch_update, UpdateRate update_rate = UpdateRate.SIM_200ms)
		{
			return this.InternalUpdate(name, null, update_rate, false, batch_update);
		}

		// Token: 0x06008952 RID: 35154 RVA: 0x0034B7EB File Offset: 0x003499EB
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State FastUpdate(string name, UpdateBucketWithUpdater<StateMachineInstanceType>.IUpdater updater, UpdateRate update_rate = UpdateRate.SIM_200ms, bool load_balance = false)
		{
			return this.InternalUpdate(name, updater, update_rate, load_balance, null);
		}

		// Token: 0x06008953 RID: 35155 RVA: 0x0034B7F9 File Offset: 0x003499F9
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Enter(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			this.enterActions = GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.AddAction(name, callback, this.enterActions, true);
			return this;
		}

		// Token: 0x06008954 RID: 35156 RVA: 0x0034B810 File Offset: 0x00349A10
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Exit(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			this.exitActions = GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.AddAction(name, callback, this.exitActions, false);
			return this;
		}

		// Token: 0x06008955 RID: 35157 RVA: 0x0034B828 File Offset: 0x00349A28
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Toggle(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback enter_callback, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback exit_callback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleEnter(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.dataTable[data_idx] = GameStateMachineHelper.HasToggleEnteredFlag;
				enter_callback(smi);
			});
			this.Exit("ToggleExit(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				if (smi.dataTable[data_idx] != null)
				{
					smi.dataTable[data_idx] = null;
					exit_callback(smi);
				}
			});
			return this;
		}

		// Token: 0x06008956 RID: 35158 RVA: 0x0034B89C File Offset: 0x00349A9C
		private void Break(StateMachineInstanceType smi)
		{
		}

		// Token: 0x06008957 RID: 35159 RVA: 0x0034B89E File Offset: 0x00349A9E
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BreakOnEnter()
		{
			return this.Enter(delegate(StateMachineInstanceType smi)
			{
				this.Break(smi);
			});
		}

		// Token: 0x06008958 RID: 35160 RVA: 0x0034B8B2 File Offset: 0x00349AB2
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BreakOnExit()
		{
			return this.Exit(delegate(StateMachineInstanceType smi)
			{
				this.Break(smi);
			});
		}

		// Token: 0x06008959 RID: 35161 RVA: 0x0034B8C8 File Offset: 0x00349AC8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State AddEffect(string effect_name)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddEffect(" + effect_name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Add(effect_name, true);
			});
			return this;
		}

		// Token: 0x0600895A RID: 35162 RVA: 0x0034B918 File Offset: 0x00349B18
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleAnims(Func<StateMachineInstanceType, KAnimFile> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableAnims()", delegate(StateMachineInstanceType smi)
			{
				KAnimFile kanimFile = chooser_callback(smi);
				if (kanimFile == null)
				{
					return;
				}
				state_target.Get<KAnimControllerBase>(smi).AddAnimOverrides(kanimFile, 0f);
			});
			this.Exit("Disableanims()", delegate(StateMachineInstanceType smi)
			{
				KAnimFile kanimFile2 = chooser_callback(smi);
				if (kanimFile2 == null)
				{
					return;
				}
				state_target.Get<KAnimControllerBase>(smi).RemoveAnimOverrides(kanimFile2);
			});
			return this;
		}

		// Token: 0x0600895B RID: 35163 RVA: 0x0034B970 File Offset: 0x00349B70
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleAnims(Func<StateMachineInstanceType, HashedString> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableAnims()", delegate(StateMachineInstanceType smi)
			{
				HashedString hashedString = chooser_callback(smi);
				if (hashedString == null)
				{
					return;
				}
				if (hashedString.IsValid)
				{
					KAnimFile anim = Assets.GetAnim(hashedString);
					if (anim == null)
					{
						string text = "Missing anims: ";
						HashedString hashedString2 = hashedString;
						global::Debug.LogWarning(text + hashedString2.ToString());
						return;
					}
					state_target.Get<KAnimControllerBase>(smi).AddAnimOverrides(anim, 0f);
				}
			});
			this.Exit("Disableanims()", delegate(StateMachineInstanceType smi)
			{
				HashedString hashedString3 = chooser_callback(smi);
				if (hashedString3 == null)
				{
					return;
				}
				if (hashedString3.IsValid)
				{
					KAnimFile anim2 = Assets.GetAnim(hashedString3);
					if (anim2 != null)
					{
						state_target.Get<KAnimControllerBase>(smi).RemoveAnimOverrides(anim2);
					}
				}
			});
			return this;
		}

		// Token: 0x0600895C RID: 35164 RVA: 0x0034B9C8 File Offset: 0x00349BC8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleAnims(string anim_file, float priority = 0f)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Toggle("ToggleAnims(" + anim_file + ")", delegate(StateMachineInstanceType smi)
			{
				KAnimFile anim = Assets.GetAnim(anim_file);
				if (anim == null)
				{
					global::Debug.LogError("Trying to add missing override anims:" + anim_file);
				}
				state_target.Get<KAnimControllerBase>(smi).AddAnimOverrides(anim, priority);
			}, delegate(StateMachineInstanceType smi)
			{
				KAnimFile anim2 = Assets.GetAnim(anim_file);
				state_target.Get<KAnimControllerBase>(smi).RemoveAnimOverrides(anim2);
			});
			return this;
		}

		// Token: 0x0600895D RID: 35165 RVA: 0x0034BA2C File Offset: 0x00349C2C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleAttributeModifier(string modifier_name, Func<StateMachineInstanceType, AttributeModifier> callback, Func<StateMachineInstanceType, bool> condition = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddAttributeModifier( " + modifier_name + " )", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					AttributeModifier attributeModifier = callback(smi);
					DebugUtil.Assert(smi.dataTable[data_idx] == null);
					smi.dataTable[data_idx] = attributeModifier;
					state_target.Get(smi).GetAttributes().Add(attributeModifier);
				}
			});
			this.Exit("RemoveAttributeModifier( " + modifier_name + " )", delegate(StateMachineInstanceType smi)
			{
				if (smi.dataTable[data_idx] != null)
				{
					AttributeModifier attributeModifier2 = (AttributeModifier)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					GameObject gameObject = state_target.Get(smi);
					if (gameObject != null)
					{
						gameObject.GetAttributes().Remove(attributeModifier2);
					}
				}
			});
			return this;
		}

		// Token: 0x0600895E RID: 35166 RVA: 0x0034BAAC File Offset: 0x00349CAC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleLoopingSound(string event_name, Func<StateMachineInstanceType, bool> condition = null, bool pause_on_game_pause = true, bool enable_culling = true, bool enable_camera_scaled_position = true)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("StartLoopingSound( " + event_name + " )", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					state_target.Get(smi).GetComponent<LoopingSounds>().StartSound(event_name, pause_on_game_pause, enable_culling, enable_camera_scaled_position);
				}
			});
			this.Exit("StopLoopingSound( " + event_name + " )", delegate(StateMachineInstanceType smi)
			{
				state_target.Get(smi).GetComponent<LoopingSounds>().StopSound(event_name);
			});
			return this;
		}

		// Token: 0x0600895F RID: 35167 RVA: 0x0034BB44 File Offset: 0x00349D44
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleLoopingSound(string state_label, Func<StateMachineInstanceType, string> event_name_callback, Func<StateMachineInstanceType, bool> condition = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("StartLoopingSound( " + state_label + " )", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					string text = event_name_callback(smi);
					smi.dataTable[data_idx] = text;
					state_target.Get(smi).GetComponent<LoopingSounds>().StartSound(text);
				}
			});
			this.Exit("StopLoopingSound( " + state_label + " )", delegate(StateMachineInstanceType smi)
			{
				if (smi.dataTable[data_idx] != null)
				{
					state_target.Get(smi).GetComponent<LoopingSounds>().StopSound((string)smi.dataTable[data_idx]);
					smi.dataTable[data_idx] = null;
				}
			});
			return this;
		}

		// Token: 0x06008960 RID: 35168 RVA: 0x0034BBC4 File Offset: 0x00349DC4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State RefreshUserMenuOnEnter()
		{
			this.Enter("RefreshUserMenuOnEnter()", delegate(StateMachineInstanceType smi)
			{
				UserMenu userMenu = Game.Instance.userMenu;
				MasterType master = smi.master;
				userMenu.Refresh(master.gameObject);
			});
			return this;
		}

		// Token: 0x06008961 RID: 35169 RVA: 0x0034BBF4 File Offset: 0x00349DF4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State WorkableStartTransition(Func<StateMachineInstanceType, Workable> get_workable_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("Enter WorkableStartTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable3 = get_workable_callback(smi);
				if (workable3 != null)
				{
					Action<Workable, Workable.WorkableEvent> action = delegate(Workable workable, Workable.WorkableEvent evt)
					{
						if (evt == Workable.WorkableEvent.WorkStarted)
						{
							smi.GoTo(target_state);
						}
					};
					smi.dataTable[data_idx] = action;
					Workable workable2 = workable3;
					workable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(workable2.OnWorkableEventCB, action);
				}
			});
			this.Exit("Exit WorkableStartTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable4 = get_workable_callback(smi);
				if (workable4 != null)
				{
					Action<Workable, Workable.WorkableEvent> action2 = (Action<Workable, Workable.WorkableEvent>)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					Workable workable5 = workable4;
					workable5.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(workable5.OnWorkableEventCB, action2);
				}
			});
			return this;
		}

		// Token: 0x06008962 RID: 35170 RVA: 0x0034BC7C File Offset: 0x00349E7C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State WorkableStopTransition(Func<StateMachineInstanceType, Workable> get_workable_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("Enter WorkableStopTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable3 = get_workable_callback(smi);
				if (workable3 != null)
				{
					Action<Workable, Workable.WorkableEvent> action = delegate(Workable workable, Workable.WorkableEvent evt)
					{
						if (evt == Workable.WorkableEvent.WorkStopped)
						{
							smi.GoTo(target_state);
						}
					};
					smi.dataTable[data_idx] = action;
					Workable workable2 = workable3;
					workable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(workable2.OnWorkableEventCB, action);
				}
			});
			this.Exit("Exit WorkableStopTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable4 = get_workable_callback(smi);
				if (workable4 != null)
				{
					Action<Workable, Workable.WorkableEvent> action2 = (Action<Workable, Workable.WorkableEvent>)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					Workable workable5 = workable4;
					workable5.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(workable5.OnWorkableEventCB, action2);
				}
			});
			return this;
		}

		// Token: 0x06008963 RID: 35171 RVA: 0x0034BD04 File Offset: 0x00349F04
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State WorkableCompleteTransition(Func<StateMachineInstanceType, Workable> get_workable_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("Enter WorkableCompleteTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable3 = get_workable_callback(smi);
				if (workable3 != null)
				{
					Action<Workable, Workable.WorkableEvent> action = delegate(Workable workable, Workable.WorkableEvent evt)
					{
						if (evt == Workable.WorkableEvent.WorkCompleted)
						{
							smi.GoTo(target_state);
						}
					};
					smi.dataTable[data_idx] = action;
					Workable workable2 = workable3;
					workable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(workable2.OnWorkableEventCB, action);
				}
			});
			this.Exit("Exit WorkableCompleteTransition(" + target_state.longName + ")", delegate(StateMachineInstanceType smi)
			{
				Workable workable4 = get_workable_callback(smi);
				if (workable4 != null)
				{
					Action<Workable, Workable.WorkableEvent> action2 = (Action<Workable, Workable.WorkableEvent>)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					Workable workable5 = workable4;
					workable5.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(workable5.OnWorkableEventCB, action2);
				}
			});
			return this;
		}

		// Token: 0x06008964 RID: 35172 RVA: 0x0034BD8C File Offset: 0x00349F8C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleGravity()
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddComponent<Gravity>()", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject = state_target.Get(smi);
				smi.dataTable[data_idx] = gameObject;
				GameComps.Gravities.Add(gameObject, Vector2.zero, null);
			});
			this.Exit("RemoveComponent<Gravity>()", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject2 = (GameObject)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				GameComps.Gravities.Remove(gameObject2);
			});
			return this;
		}

		// Token: 0x06008965 RID: 35173 RVA: 0x0034BDE8 File Offset: 0x00349FE8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleGravity(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State landed_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.EventTransition(GameHashes.Landed, landed_state, null);
			this.Toggle("GravityComponent", delegate(StateMachineInstanceType smi)
			{
				GameComps.Gravities.Add(state_target.Get(smi), Vector2.zero, null);
			}, delegate(StateMachineInstanceType smi)
			{
				GameComps.Gravities.Remove(state_target.Get(smi));
			});
			return this;
		}

		// Token: 0x06008966 RID: 35174 RVA: 0x0034BE3C File Offset: 0x0034A03C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleThought(Func<StateMachineInstanceType, Thought> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableThought()", delegate(StateMachineInstanceType smi)
			{
				Thought thought = chooser_callback(smi);
				state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().AddThought(thought);
			});
			this.Exit("DisableThought()", delegate(StateMachineInstanceType smi)
			{
				Thought thought2 = chooser_callback(smi);
				state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().RemoveThought(thought2);
			});
			return this;
		}

		// Token: 0x06008967 RID: 35175 RVA: 0x0034BE94 File Offset: 0x0034A094
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleThought(Thought thought, Func<StateMachineInstanceType, bool> condition_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition_callback == null || condition_callback(smi))
				{
					state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().AddThought(thought);
				}
			});
			if (condition_callback != null)
			{
				this.Update("ValidateThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi, float dt)
				{
					if (condition_callback(smi))
					{
						state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().AddThought(thought);
						return;
					}
					state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().RemoveThought(thought);
				}, UpdateRate.SIM_200ms, false);
			}
			this.Exit("RemoveThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get(smi).GetSMI<ThoughtGraph.Instance>().RemoveThought(thought);
			});
			return this;
		}

		// Token: 0x06008968 RID: 35176 RVA: 0x0034BF54 File Offset: 0x0034A154
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleCreatureThought(Func<StateMachineInstanceType, Thought> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableCreatureThought()", delegate(StateMachineInstanceType smi)
			{
				Thought thought = chooser_callback(smi);
				state_target.Get(smi).GetSMI<CreatureThoughtGraph.Instance>().AddThought(thought);
			});
			this.Exit("DisableCreatureThought()", delegate(StateMachineInstanceType smi)
			{
				Thought thought2 = chooser_callback(smi);
				CreatureThoughtGraph.Instance smi2 = state_target.Get(smi).GetSMI<CreatureThoughtGraph.Instance>();
				if (smi2 != null)
				{
					smi2.RemoveThought(thought2);
				}
			});
			return this;
		}

		// Token: 0x06008969 RID: 35177 RVA: 0x0034BFAC File Offset: 0x0034A1AC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleCreatureThought(Thought thought, Func<StateMachineInstanceType, bool> condition_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddCreatureThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition_callback == null || condition_callback(smi))
				{
					state_target.Get(smi).GetSMI<CreatureThoughtGraph.Instance>().AddThought(thought);
				}
			});
			if (condition_callback != null)
			{
				this.Update("ValidateCreatureThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi, float dt)
				{
					if (condition_callback(smi))
					{
						state_target.Get(smi).GetSMI<CreatureThoughtGraph.Instance>().AddThought(thought);
						return;
					}
					state_target.Get(smi).GetSMI<CreatureThoughtGraph.Instance>().RemoveThought(thought);
				}, UpdateRate.SIM_200ms, false);
			}
			this.Exit("RemoveCreatureThought(" + thought.Id + ")", delegate(StateMachineInstanceType smi)
			{
				CreatureThoughtGraph.Instance smi2 = state_target.Get(smi).GetSMI<CreatureThoughtGraph.Instance>();
				if (smi2 != null)
				{
					smi2.RemoveThought(thought);
				}
			});
			return this;
		}

		// Token: 0x0600896A RID: 35178 RVA: 0x0034C06C File Offset: 0x0034A26C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleExpression(Func<StateMachineInstanceType, Expression> chooser_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddExpression", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<FaceGraph>(smi).AddExpression(chooser_callback(smi));
			});
			this.Exit("RemoveExpression", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<FaceGraph>(smi).RemoveExpression(chooser_callback(smi));
			});
			return this;
		}

		// Token: 0x0600896B RID: 35179 RVA: 0x0034C0C4 File Offset: 0x0034A2C4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleExpression(Expression expression, Func<StateMachineInstanceType, bool> condition = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddExpression(" + expression.Id + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					state_target.Get<FaceGraph>(smi).AddExpression(expression);
				}
			});
			if (condition != null)
			{
				this.Update("ValidateExpression(" + expression.Id + ")", delegate(StateMachineInstanceType smi, float dt)
				{
					if (condition(smi))
					{
						state_target.Get<FaceGraph>(smi).AddExpression(expression);
						return;
					}
					state_target.Get<FaceGraph>(smi).RemoveExpression(expression);
				}, UpdateRate.SIM_200ms, false);
			}
			this.Exit("RemoveExpression(" + expression.Id + ")", delegate(StateMachineInstanceType smi)
			{
				FaceGraph faceGraph = state_target.Get<FaceGraph>(smi);
				if (faceGraph != null)
				{
					faceGraph.RemoveExpression(expression);
				}
			});
			return this;
		}

		// Token: 0x0600896C RID: 35180 RVA: 0x0034C184 File Offset: 0x0034A384
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleMainStatusItem(StatusItem status_item, Func<StateMachineInstanceType, object> callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddMainStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				object obj = ((callback != null) ? callback(smi) : smi);
				state_target.Get<KSelectable>(smi).SetStatusItem(Db.Get().StatusItemCategories.Main, status_item, obj);
			});
			this.Exit("RemoveMainStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null)
				{
					kselectable.SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
				}
			});
			return this;
		}

		// Token: 0x0600896D RID: 35181 RVA: 0x0034C20C File Offset: 0x0034A40C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleMainStatusItem(Func<StateMachineInstanceType, StatusItem> status_item_cb, Func<StateMachineInstanceType, object> callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddMainStatusItem(DynamicGeneration)", delegate(StateMachineInstanceType smi)
			{
				object obj = ((callback != null) ? callback(smi) : smi);
				state_target.Get<KSelectable>(smi).SetStatusItem(Db.Get().StatusItemCategories.Main, status_item_cb(smi), obj);
			});
			this.Exit("RemoveMainStatusItem(DynamicGeneration)", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null)
				{
					kselectable.SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
				}
			});
			return this;
		}

		// Token: 0x0600896E RID: 35182 RVA: 0x0034C26C File Offset: 0x0034A46C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleCategoryStatusItem(StatusItemCategory category, StatusItem status_item, object data = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter(string.Concat(new string[] { "AddCategoryStatusItem(", category.Id, ", ", status_item.Id, ")" }), delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KSelectable>(smi).SetStatusItem(category, status_item, (data != null) ? data : smi);
			});
			this.Exit(string.Concat(new string[] { "RemoveCategoryStatusItem(", category.Id, ", ", status_item.Id, ")" }), delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null)
				{
					kselectable.SetStatusItem(category, null, null);
				}
			});
			return this;
		}

		// Token: 0x0600896F RID: 35183 RVA: 0x0034C348 File Offset: 0x0034A548
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(StatusItem status_item, object data = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				object obj = data;
				if (obj == null)
				{
					obj = smi;
				}
				Guid guid = state_target.Get<KSelectable>(smi).AddStatusItem(status_item, obj);
				smi.dataTable[data_idx] = guid;
			});
			this.Exit("RemoveStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null && smi.dataTable[data_idx] != null)
				{
					Guid guid2 = (Guid)smi.dataTable[data_idx];
					kselectable.RemoveStatusItem(guid2, false);
				}
				smi.dataTable[data_idx] = null;
			});
			return this;
		}

		// Token: 0x06008970 RID: 35184 RVA: 0x0034C3DC File Offset: 0x0034A5DC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleSnapOn(string snap_on)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("SnapOn(" + snap_on + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<SnapOn>(smi).AttachSnapOnByName(snap_on);
			});
			this.Exit("SnapOff(" + snap_on + ")", delegate(StateMachineInstanceType smi)
			{
				SnapOn snapOn = state_target.Get<SnapOn>(smi);
				if (snapOn != null)
				{
					snapOn.DetachSnapOnByName(snap_on);
				}
			});
			return this;
		}

		// Token: 0x06008971 RID: 35185 RVA: 0x0034C454 File Offset: 0x0034A654
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleTag(Tag tag)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddTag(" + tag.Name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KPrefabID>(smi).AddTag(tag, false);
			});
			this.Exit("RemoveTag(" + tag.Name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KPrefabID>(smi).RemoveTag(tag);
			});
			return this;
		}

		// Token: 0x06008972 RID: 35186 RVA: 0x0034C4D8 File Offset: 0x0034A6D8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleTag(Func<StateMachineInstanceType, Tag> behaviour_tag_cb)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddTag(DynamicallyConstructed)", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KPrefabID>(smi).AddTag(behaviour_tag_cb(smi), false);
			});
			this.Exit("RemoveTag(DynamicallyConstructed)", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<KPrefabID>(smi).RemoveTag(behaviour_tag_cb(smi));
			});
			return this;
		}

		// Token: 0x06008973 RID: 35187 RVA: 0x0034C52F File Offset: 0x0034A72F
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(StatusItem status_item, Func<StateMachineInstanceType, object> callback)
		{
			return this.ToggleStatusItem(status_item, callback, null);
		}

		// Token: 0x06008974 RID: 35188 RVA: 0x0034C53C File Offset: 0x0034A73C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(StatusItem status_item, Func<StateMachineInstanceType, object> callback, StatusItemCategory category)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				if (category == null)
				{
					object obj = ((callback != null) ? callback(smi) : null);
					Guid guid = state_target.Get<KSelectable>(smi).AddStatusItem(status_item, obj);
					smi.dataTable[data_idx] = guid;
					return;
				}
				object obj2 = ((callback != null) ? callback(smi) : null);
				Guid guid2 = state_target.Get<KSelectable>(smi).SetStatusItem(category, status_item, obj2);
				smi.dataTable[data_idx] = guid2;
			});
			this.Exit("RemoveStatusItem(" + status_item.Id + ")", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null && smi.dataTable[data_idx] != null)
				{
					if (category == null)
					{
						Guid guid3 = (Guid)smi.dataTable[data_idx];
						kselectable.RemoveStatusItem(guid3, false);
					}
					else
					{
						kselectable.SetStatusItem(category, null, null);
					}
				}
				smi.dataTable[data_idx] = null;
			});
			return this;
		}

		// Token: 0x06008975 RID: 35189 RVA: 0x0034C5D8 File Offset: 0x0034A7D8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(Func<StateMachineInstanceType, StatusItem> status_item_cb, Func<StateMachineInstanceType, object> data_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("AddStatusItem(DynamicallyConstructed)", delegate(StateMachineInstanceType smi)
			{
				StatusItem statusItem = status_item_cb(smi);
				if (statusItem != null)
				{
					object obj = ((data_callback != null) ? data_callback(smi) : null);
					Guid guid = state_target.Get<KSelectable>(smi).AddStatusItem(statusItem, obj);
					smi.dataTable[data_idx] = guid;
				}
			});
			this.Exit("RemoveStatusItem(DynamicallyConstructed)", delegate(StateMachineInstanceType smi)
			{
				KSelectable kselectable = state_target.Get<KSelectable>(smi);
				if (kselectable != null && smi.dataTable[data_idx] != null)
				{
					Guid guid2 = (Guid)smi.dataTable[data_idx];
					kselectable.RemoveStatusItem(guid2, false);
				}
				smi.dataTable[data_idx] = null;
			});
			return this;
		}

		// Token: 0x06008976 RID: 35190 RVA: 0x0034C644 File Offset: 0x0034A844
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleFX(Func<StateMachineInstanceType, StateMachine.Instance> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("EnableFX()", delegate(StateMachineInstanceType smi)
			{
				StateMachine.Instance instance = callback(smi);
				if (instance != null)
				{
					instance.StartSM();
					smi.dataTable[data_idx] = instance;
				}
			});
			this.Exit("DisableFX()", delegate(StateMachineInstanceType smi)
			{
				StateMachine.Instance instance2 = (StateMachine.Instance)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				if (instance2 != null)
				{
					instance2.StopSM("ToggleFX.Exit");
				}
			});
			return this;
		}

		// Token: 0x06008977 RID: 35191 RVA: 0x0034C69C File Offset: 0x0034A89C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BehaviourComplete(Func<StateMachineInstanceType, Tag> tag_cb, bool on_exit = false)
		{
			if (on_exit)
			{
				this.Exit("BehaviourComplete()", delegate(StateMachineInstanceType smi)
				{
					smi.Trigger(-739654666, tag_cb(smi));
					smi.GoTo(null);
				});
			}
			else
			{
				this.Enter("BehaviourComplete()", delegate(StateMachineInstanceType smi)
				{
					smi.Trigger(-739654666, tag_cb(smi));
					smi.GoTo(null);
				});
			}
			return this;
		}

		// Token: 0x06008978 RID: 35192 RVA: 0x0034C6EC File Offset: 0x0034A8EC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State BehaviourComplete(Tag tag, bool on_exit = false)
		{
			if (on_exit)
			{
				this.Exit("BehaviourComplete(" + tag.ToString() + ")", delegate(StateMachineInstanceType smi)
				{
					smi.Trigger(-739654666, tag);
					smi.GoTo(null);
				});
			}
			else
			{
				this.Enter("BehaviourComplete(" + tag.ToString() + ")", delegate(StateMachineInstanceType smi)
				{
					smi.Trigger(-739654666, tag);
					smi.GoTo(null);
				});
			}
			return this;
		}

		// Token: 0x06008979 RID: 35193 RVA: 0x0034C774 File Offset: 0x0034A974
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleBehaviour(Tag behaviour_tag, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback precondition, Action<StateMachineInstanceType> on_complete = null)
		{
			Func<object, bool> precondition_cb = (object obj) => precondition(obj as StateMachineInstanceType);
			this.Enter("AddPrecondition", delegate(StateMachineInstanceType smi)
			{
				if (smi.GetComponent<ChoreConsumer>() != null)
				{
					smi.GetComponent<ChoreConsumer>().AddBehaviourPrecondition(behaviour_tag, precondition_cb, smi);
				}
			});
			this.Exit("RemovePrecondition", delegate(StateMachineInstanceType smi)
			{
				if (smi.GetComponent<ChoreConsumer>() != null)
				{
					smi.GetComponent<ChoreConsumer>().RemoveBehaviourPrecondition(behaviour_tag, precondition_cb, smi);
				}
			});
			this.ToggleTag(behaviour_tag);
			if (on_complete != null)
			{
				this.EventHandler(GameHashes.BehaviourTagComplete, delegate(StateMachineInstanceType smi, object data)
				{
					if ((Tag)data == behaviour_tag)
					{
						on_complete(smi);
					}
				});
			}
			return this;
		}

		// Token: 0x0600897A RID: 35194 RVA: 0x0034C80C File Offset: 0x0034AA0C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleBehaviour(Func<StateMachineInstanceType, Tag> behaviour_tag_cb, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback precondition, Action<StateMachineInstanceType> on_complete = null)
		{
			Func<object, bool> precondition_cb = (object obj) => precondition(obj as StateMachineInstanceType);
			this.Enter("AddPrecondition", delegate(StateMachineInstanceType smi)
			{
				if (smi.GetComponent<ChoreConsumer>() != null)
				{
					smi.GetComponent<ChoreConsumer>().AddBehaviourPrecondition(behaviour_tag_cb(smi), precondition_cb, smi);
				}
			});
			this.Exit("RemovePrecondition", delegate(StateMachineInstanceType smi)
			{
				if (smi.GetComponent<ChoreConsumer>() != null)
				{
					smi.GetComponent<ChoreConsumer>().RemoveBehaviourPrecondition(behaviour_tag_cb(smi), precondition_cb, smi);
				}
			});
			this.ToggleTag(behaviour_tag_cb);
			if (on_complete != null)
			{
				this.EventHandler(GameHashes.BehaviourTagComplete, delegate(StateMachineInstanceType smi, object data)
				{
					if ((Tag)data == behaviour_tag_cb(smi))
					{
						on_complete(smi);
					}
				});
			}
			return this;
		}

		// Token: 0x0600897B RID: 35195 RVA: 0x0034C8A4 File Offset: 0x0034AAA4
		public void ClearFetch(StateMachineInstanceType smi, int fetch_data_idx, int callback_data_idx)
		{
			FetchList2 fetchList = (FetchList2)smi.dataTable[fetch_data_idx];
			if (fetchList != null)
			{
				smi.dataTable[fetch_data_idx] = null;
				smi.dataTable[callback_data_idx] = null;
				fetchList.Cancel("ClearFetchListFromSM");
			}
		}

		// Token: 0x0600897C RID: 35196 RVA: 0x0034C8F0 File Offset: 0x0034AAF0
		public void SetupFetch(Func<StateMachineInstanceType, FetchList2> create_fetchlist_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state, StateMachineInstanceType smi, int fetch_data_idx, int callback_data_idx)
		{
			FetchList2 fetchList = create_fetchlist_callback(smi);
			global::System.Action action = delegate
			{
				this.ClearFetch(smi, fetch_data_idx, callback_data_idx);
				smi.GoTo(target_state);
			};
			fetchList.Submit(action, true);
			smi.dataTable[fetch_data_idx] = fetchList;
			smi.dataTable[callback_data_idx] = action;
		}

		// Token: 0x0600897D RID: 35197 RVA: 0x0034C97C File Offset: 0x0034AB7C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleFetch(Func<StateMachineInstanceType, FetchList2> create_fetchlist_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleFetchEnter()", delegate(StateMachineInstanceType smi)
			{
				this.SetupFetch(create_fetchlist_callback, target_state, smi, data_idx, callback_data_idx);
			});
			this.Exit("ToggleFetchExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearFetch(smi, data_idx, callback_data_idx);
			});
			return this;
		}

		// Token: 0x0600897E RID: 35198 RVA: 0x0034C9F0 File Offset: 0x0034ABF0
		private void ClearChore(StateMachineInstanceType smi, int chore_data_idx, int callback_data_idx)
		{
			Chore chore = (Chore)smi.dataTable[chore_data_idx];
			if (chore != null)
			{
				Action<Chore> action = (Action<Chore>)smi.dataTable[callback_data_idx];
				smi.dataTable[chore_data_idx] = null;
				smi.dataTable[callback_data_idx] = null;
				Chore chore2 = chore;
				chore2.onExit = (Action<Chore>)Delegate.Remove(chore2.onExit, action);
				chore.Cancel("ClearGlobalChore");
			}
		}

		// Token: 0x0600897F RID: 35199 RVA: 0x0034CA64 File Offset: 0x0034AC64
		private Chore SetupChore(Func<StateMachineInstanceType, Chore> create_chore_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state, StateMachineInstanceType smi, int chore_data_idx, int callback_data_idx, bool is_success_state_reentrant, bool is_failure_state_reentrant)
		{
			Chore chore = create_chore_callback(smi);
			DebugUtil.DevAssert(!chore.IsPreemptable, "ToggleChore can't be used with preemptable chores! :( (but it should...)", null);
			chore.runUntilComplete = false;
			Action<Chore> action = delegate(Chore chore_param)
			{
				bool isComplete = chore.isComplete;
				if ((isComplete & is_success_state_reentrant) || (is_failure_state_reentrant && !isComplete))
				{
					this.SetupChore(create_chore_callback, success_state, failure_state, smi, chore_data_idx, callback_data_idx, is_success_state_reentrant, is_failure_state_reentrant);
					return;
				}
				GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state = success_state;
				if (!isComplete)
				{
					state = failure_state;
				}
				this.ClearChore(smi, chore_data_idx, callback_data_idx);
				smi.GoTo(state);
			};
			Chore chore2 = chore;
			chore2.onExit = (Action<Chore>)Delegate.Combine(chore2.onExit, action);
			smi.dataTable[chore_data_idx] = chore;
			smi.dataTable[callback_data_idx] = action;
			return chore;
		}

		// Token: 0x06008980 RID: 35200 RVA: 0x0034CB5C File Offset: 0x0034AD5C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleRecurringChore(Func<StateMachineInstanceType, Chore> callback, Func<StateMachineInstanceType, bool> condition = null)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleRecurringChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					this.SetupChore(callback, this, this, smi, data_idx, callback_data_idx, true, true);
				}
			});
			this.Exit("ToggleRecurringChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
			});
			return this;
		}

		// Token: 0x06008981 RID: 35201 RVA: 0x0034CBD0 File Offset: 0x0034ADD0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleRecurringChore(Func<StateMachineInstanceType, Chore> callback, Action<StateMachineInstanceType, Chore> processChore, Func<StateMachineInstanceType, bool> condition = null)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleRecurringChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				if (condition == null || condition(smi))
				{
					Chore chore = this.SetupChore(callback, this, this, smi, data_idx, callback_data_idx, true, true);
					processChore(smi, chore);
				}
			});
			this.Exit("ToggleRecurringChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
				processChore(smi, null);
			});
			return this;
		}

		// Token: 0x06008982 RID: 35202 RVA: 0x0034CC48 File Offset: 0x0034AE48
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleChore(Func<StateMachineInstanceType, Chore> callback, Action<StateMachineInstanceType, Chore> processChore, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				Chore chore = this.SetupChore(callback, target_state, target_state, smi, data_idx, callback_data_idx, false, false);
				processChore(smi, chore);
			});
			this.Exit("ToggleChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
				processChore(smi, null);
			});
			return this;
		}

		// Token: 0x06008983 RID: 35203 RVA: 0x0034CCC0 File Offset: 0x0034AEC0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleChore(Func<StateMachineInstanceType, Chore> callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			this.Enter("ToggleChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				this.SetupChore(callback, target_state, target_state, smi, data_idx, callback_data_idx, false, false);
			});
			this.Exit("ToggleChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
			});
			return this;
		}

		// Token: 0x06008984 RID: 35204 RVA: 0x0034CD34 File Offset: 0x0034AF34
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleChore(Func<StateMachineInstanceType, Chore> callback, Action<StateMachineInstanceType, Chore> processChore, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			bool is_success_state_reentrant = success_state == this;
			bool is_failure_state_reentrant = failure_state == this;
			this.Enter("ToggleChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				Chore chore = this.SetupChore(callback, success_state, failure_state, smi, data_idx, callback_data_idx, is_success_state_reentrant, is_failure_state_reentrant);
				processChore(smi, chore);
			});
			this.Exit("ToggleChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
				processChore(smi, null);
			});
			return this;
		}

		// Token: 0x06008985 RID: 35205 RVA: 0x0034CDD4 File Offset: 0x0034AFD4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleChore(Func<StateMachineInstanceType, Chore> callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			int data_idx = this.CreateDataTableEntry();
			int callback_data_idx = this.CreateDataTableEntry();
			bool is_success_state_reentrant = success_state == this;
			bool is_failure_state_reentrant = failure_state == this;
			this.Enter("ToggleChoreEnter()", delegate(StateMachineInstanceType smi)
			{
				this.SetupChore(callback, success_state, failure_state, smi, data_idx, callback_data_idx, is_success_state_reentrant, is_failure_state_reentrant);
			});
			this.Exit("ToggleChoreExit()", delegate(StateMachineInstanceType smi)
			{
				this.ClearChore(smi, data_idx, callback_data_idx);
			});
			return this;
		}

		// Token: 0x06008986 RID: 35206 RVA: 0x0034CE6C File Offset: 0x0034B06C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleReactable(Func<StateMachineInstanceType, Reactable> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter(delegate(StateMachineInstanceType smi)
			{
				smi.dataTable[data_idx] = callback(smi);
			});
			this.Exit(delegate(StateMachineInstanceType smi)
			{
				Reactable reactable = (Reactable)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				if (reactable != null)
				{
					reactable.Cleanup();
				}
			});
			return this;
		}

		// Token: 0x06008987 RID: 35207 RVA: 0x0034CEBC File Offset: 0x0034B0BC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State RemoveEffect(string effect_name)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("RemoveEffect(" + effect_name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Remove(effect_name);
			});
			return this;
		}

		// Token: 0x06008988 RID: 35208 RVA: 0x0034CF0C File Offset: 0x0034B10C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleEffect(string effect_name)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddEffect(" + effect_name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Add(effect_name, false);
			});
			this.Exit("RemoveEffect(" + effect_name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Remove(effect_name);
			});
			return this;
		}

		// Token: 0x06008989 RID: 35209 RVA: 0x0034CF84 File Offset: 0x0034B184
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleEffect(Func<StateMachineInstanceType, Effect> callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddEffect()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Add(callback(smi), false);
			});
			this.Exit("RemoveEffect()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Remove(callback(smi));
			});
			return this;
		}

		// Token: 0x0600898A RID: 35210 RVA: 0x0034CFDC File Offset: 0x0034B1DC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleEffect(Func<StateMachineInstanceType, string> callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddEffect()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Add(callback(smi), false);
			});
			this.Exit("RemoveEffect()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Effects>(smi).Remove(callback(smi));
			});
			return this;
		}

		// Token: 0x0600898B RID: 35211 RVA: 0x0034D033 File Offset: 0x0034B233
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State LogOnExit(Func<StateMachineInstanceType, string> callback)
		{
			this.Enter("Log()", delegate(StateMachineInstanceType smi)
			{
			});
			return this;
		}

		// Token: 0x0600898C RID: 35212 RVA: 0x0034D061 File Offset: 0x0034B261
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State LogOnEnter(Func<StateMachineInstanceType, string> callback)
		{
			this.Exit("Log()", delegate(StateMachineInstanceType smi)
			{
			});
			return this;
		}

		// Token: 0x0600898D RID: 35213 RVA: 0x0034D090 File Offset: 0x0034B290
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleUrge(Urge urge)
		{
			return this.ToggleUrge((StateMachineInstanceType smi) => urge);
		}

		// Token: 0x0600898E RID: 35214 RVA: 0x0034D0BC File Offset: 0x0034B2BC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleUrge(Func<StateMachineInstanceType, Urge> urge_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("AddUrge()", delegate(StateMachineInstanceType smi)
			{
				Urge urge = urge_callback(smi);
				state_target.Get<ChoreConsumer>(smi).AddUrge(urge);
			});
			this.Exit("RemoveUrge()", delegate(StateMachineInstanceType smi)
			{
				Urge urge2 = urge_callback(smi);
				ChoreConsumer choreConsumer = state_target.Get<ChoreConsumer>(smi);
				if (choreConsumer != null)
				{
					choreConsumer.RemoveUrge(urge2);
				}
			});
			return this;
		}

		// Token: 0x0600898F RID: 35215 RVA: 0x0034D113 File Offset: 0x0034B313
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnTargetLost(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter parameter, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state)
		{
			this.ParamTransition<GameObject>(parameter, target_state, (StateMachineInstanceType smi, GameObject p) => p == null);
			return this;
		}

		// Token: 0x06008990 RID: 35216 RVA: 0x0034D140 File Offset: 0x0034B340
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleBrain(string reason)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("StopBrain(" + reason + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Brain>(smi).Stop(reason);
			});
			this.Exit("ResetBrain(" + reason + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Brain>(smi).Reset(reason);
			});
			return this;
		}

		// Token: 0x06008991 RID: 35217 RVA: 0x0034D1B8 File Offset: 0x0034B3B8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PreBrainUpdate(Action<StateMachineInstanceType> callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			int data_idx = this.CreateDataTableEntry();
			this.Enter("EnablePreBrainUpdate", delegate(StateMachineInstanceType smi)
			{
				global::System.Action action = delegate
				{
					callback(smi);
				};
				smi.dataTable[data_idx] = action;
				Brain brain = state_target.Get<Brain>(smi);
				DebugUtil.AssertArgs(brain != null, new object[] { "PreBrainUpdate cannot find a brain" });
				brain.onPreUpdate += action;
			});
			this.Exit("DisablePreBrainUpdate", delegate(StateMachineInstanceType smi)
			{
				global::System.Action action2 = (global::System.Action)smi.dataTable[data_idx];
				state_target.Get<Brain>(smi).onPreUpdate -= action2;
				smi.dataTable[data_idx] = null;
			});
			return this;
		}

		// Token: 0x06008992 RID: 35218 RVA: 0x0034D21C File Offset: 0x0034B41C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TriggerOnEnter(GameHashes evt, Func<StateMachineInstanceType, object> callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("Trigger(" + evt.ToString() + ")", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject = state_target.Get(smi);
				object obj = ((callback != null) ? callback(smi) : null);
				gameObject.Trigger((int)evt, obj);
			});
			return this;
		}

		// Token: 0x06008993 RID: 35219 RVA: 0x0034D280 File Offset: 0x0034B480
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TriggerOnExit(GameHashes evt, Func<StateMachineInstanceType, object> callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Exit("Trigger(" + evt.ToString() + ")", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject = state_target.Get(smi);
				if (gameObject != null)
				{
					object obj = ((callback != null) ? callback(smi) : null);
					gameObject.Trigger((int)evt, obj);
				}
			});
			return this;
		}

		// Token: 0x06008994 RID: 35220 RVA: 0x0034D2E4 File Offset: 0x0034B4E4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStateMachineList(Func<StateMachineInstanceType, Func<StateMachineInstanceType, StateMachine.Instance>[]> getListCallback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("EnableListOfStateMachines()", delegate(StateMachineInstanceType smi)
			{
				Func<StateMachineInstanceType, StateMachine.Instance>[] array = getListCallback(smi);
				StateMachine.Instance[] array2 = new StateMachine.Instance[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					StateMachine.Instance instance = array[i](smi);
					instance.StartSM();
					array2[i] = instance;
				}
				smi.dataTable[data_idx] = array2;
			});
			this.Exit("DisableListOfStateMachines()", delegate(StateMachineInstanceType smi)
			{
				Func<StateMachineInstanceType, StateMachine.Instance>[] array3 = getListCallback(smi);
				StateMachine.Instance[] array4 = (StateMachine.Instance[])smi.dataTable[data_idx];
				for (int j = array3.Length - 1; j >= 0; j--)
				{
					StateMachine.Instance instance2 = array4[j];
					if (instance2 != null)
					{
						instance2.StopSM("ToggleListOfStateMachines.Exit");
					}
				}
				smi.dataTable[data_idx] = null;
			});
			return this;
		}

		// Token: 0x06008995 RID: 35221 RVA: 0x0034D33C File Offset: 0x0034B53C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStateMachine(Func<StateMachineInstanceType, StateMachine.Instance> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter("EnableStateMachine()", delegate(StateMachineInstanceType smi)
			{
				StateMachine.Instance instance = callback(smi);
				smi.dataTable[data_idx] = instance;
				instance.StartSM();
			});
			this.Exit("DisableStateMachine()", delegate(StateMachineInstanceType smi)
			{
				StateMachine.Instance instance2 = (StateMachine.Instance)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				if (instance2 != null)
				{
					instance2.StopSM("ToggleStateMachine.Exit");
				}
			});
			return this;
		}

		// Token: 0x06008996 RID: 35222 RVA: 0x0034D394 File Offset: 0x0034B594
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleComponentIfFound<ComponentType>(bool disable = false) where ComponentType : MonoBehaviour
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableComponent(" + typeof(ComponentType).Name + ")", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject = state_target.Get(smi);
				if (gameObject != null)
				{
					ComponentType component = gameObject.GetComponent<ComponentType>();
					if (component != null)
					{
						component.enabled = !disable;
					}
				}
			});
			this.Exit("DisableComponent(" + typeof(ComponentType).Name + ")", delegate(StateMachineInstanceType smi)
			{
				GameObject gameObject2 = state_target.Get(smi);
				if (gameObject2 != null)
				{
					ComponentType component2 = gameObject2.GetComponent<ComponentType>();
					if (component2 != null)
					{
						component2.enabled = disable;
					}
				}
			});
			return this;
		}

		// Token: 0x06008997 RID: 35223 RVA: 0x0034D420 File Offset: 0x0034B620
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleComponent<ComponentType>(bool disable = false) where ComponentType : MonoBehaviour
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableComponent(" + typeof(ComponentType).Name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<ComponentType>(smi).enabled = !disable;
			});
			this.Exit("DisableComponent(" + typeof(ComponentType).Name + ")", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<ComponentType>(smi).enabled = disable;
			});
			return this;
		}

		// Token: 0x06008998 RID: 35224 RVA: 0x0034D4AC File Offset: 0x0034B6AC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeOperationalFlag(Operational.Flag flag, bool init_val = false)
		{
			this.Enter(string.Concat(new string[]
			{
				"InitOperationalFlag (",
				flag.Name,
				", ",
				init_val.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				smi.GetComponent<Operational>().SetFlag(flag, init_val);
			});
			return this;
		}

		// Token: 0x06008999 RID: 35225 RVA: 0x0034D520 File Offset: 0x0034B720
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleOperationalFlag(Operational.Flag flag)
		{
			this.Enter("ToggleOperationalFlag True (" + flag.Name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.GetComponent<Operational>().SetFlag(flag, true);
			});
			this.Exit("ToggleOperationalFlag False (" + flag.Name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.GetComponent<Operational>().SetFlag(flag, false);
			});
			return this;
		}

		// Token: 0x0600899A RID: 35226 RVA: 0x0034D598 File Offset: 0x0034B798
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleReserve(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter reserver, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter pickup_target, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter requested_amount, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter actual_amount)
		{
			int data_idx = this.CreateDataTableEntry();
			this.Enter(string.Concat(new string[] { "Reserve(", pickup_target.name, ", ", requested_amount.name, ")" }), delegate(StateMachineInstanceType smi)
			{
				Pickupable pickupable = pickup_target.Get<Pickupable>(smi);
				GameObject gameObject = reserver.Get(smi);
				float num = requested_amount.Get(smi);
				float num2 = Mathf.Max(1f, Db.Get().Attributes.CarryAmount.Lookup(gameObject).GetTotalValue());
				float num3 = Math.Min(num, num2);
				num3 = Math.Min(num3, pickupable.UnreservedFetchAmount);
				if (num3 <= 0f)
				{
					pickupable.PrintReservations();
					global::Debug.LogError(string.Concat(new string[]
					{
						num2.ToString(),
						", ",
						num.ToString(),
						", ",
						pickupable.UnreservedFetchAmount.ToString(),
						", ",
						num3.ToString()
					}));
				}
				actual_amount.Set(num3, smi, false);
				int num4 = pickupable.Reserve("ToggleReserve", gameObject.GetComponent<KPrefabID>().InstanceID, num3);
				smi.dataTable[data_idx] = num4;
			});
			this.Exit(string.Concat(new string[] { "Unreserve(", pickup_target.name, ", ", requested_amount.name, ")" }), delegate(StateMachineInstanceType smi)
			{
				int num5 = (int)smi.dataTable[data_idx];
				smi.dataTable[data_idx] = null;
				Pickupable pickupable2 = pickup_target.Get<Pickupable>(smi);
				if (pickupable2 != null)
				{
					pickupable2.Unreserve("ToggleReserve", num5);
				}
			});
			return this;
		}

		// Token: 0x0600899B RID: 35227 RVA: 0x0034D67C File Offset: 0x0034B87C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleWork(string work_type, Action<StateMachineInstanceType> callback, Func<StateMachineInstanceType, bool> validate_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("StartWork(" + work_type + ")", delegate(StateMachineInstanceType smi)
			{
				if (validate_callback(smi))
				{
					callback(smi);
					return;
				}
				smi.GoTo(failure_state);
			});
			this.Update("Work(" + work_type + ")", delegate(StateMachineInstanceType smi, float dt)
			{
				if (validate_callback(smi))
				{
					WorkerBase.WorkResult workResult = state_target.Get<WorkerBase>(smi).Work(dt);
					if (workResult == WorkerBase.WorkResult.Success)
					{
						smi.GoTo(success_state);
						return;
					}
					if (workResult == WorkerBase.WorkResult.Failed)
					{
						smi.GoTo(failure_state);
						return;
					}
				}
				else
				{
					smi.GoTo(failure_state);
				}
			}, UpdateRate.SIM_33ms, false);
			this.Exit("StopWork()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<WorkerBase>(smi).StopWork();
			});
			return this;
		}

		// Token: 0x0600899C RID: 35228 RVA: 0x0034D71C File Offset: 0x0034B91C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleWork<WorkableType>(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter source_target, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state, Func<StateMachineInstanceType, bool> is_valid_cb) where WorkableType : Workable
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.ToggleWork(typeof(WorkableType).Name, delegate(StateMachineInstanceType smi)
			{
				Workable workable = source_target.Get<WorkableType>(smi);
				state_target.Get<WorkerBase>(smi).StartWork(new WorkerBase.StartWorkInfo(workable));
			}, (StateMachineInstanceType smi) => source_target.Get<WorkableType>(smi) != null && (is_valid_cb == null || is_valid_cb(smi)), success_state, failure_state);
			return this;
		}

		// Token: 0x0600899D RID: 35229 RVA: 0x0034D77C File Offset: 0x0034B97C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoEat(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter source_target, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter amount, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.ToggleWork("Eat", delegate(StateMachineInstanceType smi)
			{
				Edible edible = source_target.Get<Edible>(smi);
				WorkerBase workerBase = state_target.Get<WorkerBase>(smi);
				float num = amount.Get(smi);
				workerBase.StartWork(new Edible.EdibleStartWorkInfo(edible, num));
			}, (StateMachineInstanceType smi) => source_target.Get<Edible>(smi) != null, success_state, failure_state);
			return this;
		}

		// Token: 0x0600899E RID: 35230 RVA: 0x0034D7D4 File Offset: 0x0034B9D4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoSleep(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter sleeper, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter bed, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.ToggleWork("Sleep", delegate(StateMachineInstanceType smi)
			{
				WorkerBase workerBase = state_target.Get<WorkerBase>(smi);
				Sleepable sleepable = bed.Get<Sleepable>(smi);
				workerBase.StartWork(new WorkerBase.StartWorkInfo(sleepable));
			}, (StateMachineInstanceType smi) => bed.Get<Sleepable>(smi) != null, success_state, failure_state);
			return this;
		}

		// Token: 0x0600899F RID: 35231 RVA: 0x0034D824 File Offset: 0x0034BA24
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoDelivery(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter worker_param, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter storage_param, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			this.ToggleWork("Pickup", delegate(StateMachineInstanceType smi)
			{
				WorkerBase workerBase = worker_param.Get<WorkerBase>(smi);
				Storage storage = storage_param.Get<Storage>(smi);
				workerBase.StartWork(new WorkerBase.StartWorkInfo(storage));
			}, (StateMachineInstanceType smi) => storage_param.Get<Storage>(smi) != null, success_state, failure_state);
			return this;
		}

		// Token: 0x060089A0 RID: 35232 RVA: 0x0034D870 File Offset: 0x0034BA70
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoPickup(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter source_target, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter result_target, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter amount, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.ToggleWork("Pickup", delegate(StateMachineInstanceType smi)
			{
				Pickupable pickupable = source_target.Get<Pickupable>(smi);
				WorkerBase workerBase = state_target.Get<WorkerBase>(smi);
				float num = amount.Get(smi);
				workerBase.StartWork(new Pickupable.PickupableStartWorkInfo(pickupable, num, delegate(GameObject result)
				{
					result_target.Set(result, smi, false);
				}));
			}, (StateMachineInstanceType smi) => source_target.Get<Pickupable>(smi) != null || result_target.Get<Pickupable>(smi) != null, success_state, failure_state);
			return this;
		}

		// Token: 0x060089A1 RID: 35233 RVA: 0x0034D8D0 File Offset: 0x0034BAD0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleNotification(Func<StateMachineInstanceType, Notification> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("EnableNotification()", delegate(StateMachineInstanceType smi)
			{
				Notification notification = callback(smi);
				smi.dataTable[data_idx] = notification;
				state_target.AddOrGet<Notifier>(smi).Add(notification, "");
			});
			this.Exit("DisableNotification()", delegate(StateMachineInstanceType smi)
			{
				Notification notification2 = (Notification)smi.dataTable[data_idx];
				if (notification2 != null)
				{
					if (state_target != null)
					{
						Notifier notifier = state_target.Get<Notifier>(smi);
						if (notifier != null)
						{
							notifier.Remove(notification2);
						}
					}
					smi.dataTable[data_idx] = null;
				}
			});
			return this;
		}

		// Token: 0x060089A2 RID: 35234 RVA: 0x0034D934 File Offset: 0x0034BB34
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoReport(ReportManager.ReportType reportType, Func<StateMachineInstanceType, float> callback, Func<StateMachineInstanceType, string> context_callback = null)
		{
			this.Enter("DoReport()", delegate(StateMachineInstanceType smi)
			{
				float num = callback(smi);
				string text = ((context_callback != null) ? context_callback(smi) : null);
				ReportManager.Instance.ReportValue(reportType, num, text, null);
			});
			return this;
		}

		// Token: 0x060089A3 RID: 35235 RVA: 0x0034D978 File Offset: 0x0034BB78
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoNotification(Func<StateMachineInstanceType, Notification> callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("DoNotification()", delegate(StateMachineInstanceType smi)
			{
				Notification notification = callback(smi);
				state_target.AddOrGet<Notifier>(smi).Add(notification, "");
			});
			return this;
		}

		// Token: 0x060089A4 RID: 35236 RVA: 0x0034D9B8 File Offset: 0x0034BBB8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DoTutorial(Tutorial.TutorialMessages msg)
		{
			this.Enter("DoTutorial()", delegate(StateMachineInstanceType smi)
			{
				Tutorial.Instance.TutorialMessage(msg, true);
			});
			return this;
		}

		// Token: 0x060089A5 RID: 35237 RVA: 0x0034D9EC File Offset: 0x0034BBEC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleScheduleCallback(string name, Func<StateMachineInstanceType, float> time_cb, Action<StateMachineInstanceType> callback)
		{
			int data_idx = this.CreateDataTableEntry();
			Action<object> <>9__2;
			this.Enter("AddScheduledCallback(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				GameScheduler instance = GameScheduler.Instance;
				string name2 = name;
				float num = time_cb(smi);
				Action<object> action;
				if ((action = <>9__2) == null)
				{
					action = (<>9__2 = delegate(object smi_data)
					{
						callback((StateMachineInstanceType)((object)smi_data));
					});
				}
				SchedulerHandle schedulerHandle = instance.Schedule(name2, num, action, smi, null);
				DebugUtil.Assert(smi.dataTable[data_idx] == null);
				smi.dataTable[data_idx] = schedulerHandle;
			});
			this.Exit("RemoveScheduledCallback(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				if (smi.dataTable[data_idx] != null)
				{
					SchedulerHandle schedulerHandle2 = (SchedulerHandle)smi.dataTable[data_idx];
					smi.dataTable[data_idx] = null;
					schedulerHandle2.ClearScheduler();
				}
			});
			return this;
		}

		// Token: 0x060089A6 RID: 35238 RVA: 0x0034DA74 File Offset: 0x0034BC74
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleGoTo(Func<StateMachineInstanceType, float> time_cb, StateMachine.BaseState state)
		{
			this.Enter("ScheduleGoTo(" + state.name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.ScheduleGoTo(time_cb(smi), state);
			});
			return this;
		}

		// Token: 0x060089A7 RID: 35239 RVA: 0x0034DAC4 File Offset: 0x0034BCC4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleGoTo(float time, StateMachine.BaseState state)
		{
			string[] array = new string[5];
			array[0] = "ScheduleGoTo(";
			array[1] = time.ToString();
			array[2] = ", ";
			int num = 3;
			StateMachine.BaseState state2 = state;
			array[num] = ((state2 != null) ? state2.name : null);
			array[4] = ")";
			this.Enter(string.Concat(array), delegate(StateMachineInstanceType smi)
			{
				smi.ScheduleGoTo(time, state);
			});
			return this;
		}

		// Token: 0x060089A8 RID: 35240 RVA: 0x0034DB40 File Offset: 0x0034BD40
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleAction(string name, Func<StateMachineInstanceType, float> time_cb, Action<StateMachineInstanceType> action)
		{
			this.Enter("ScheduleAction(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.Schedule(time_cb(smi), delegate(object obj)
				{
					action(smi);
				}, null);
			});
			return this;
		}

		// Token: 0x060089A9 RID: 35241 RVA: 0x0034DB88 File Offset: 0x0034BD88
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleAction(string name, float time, Action<StateMachineInstanceType> action)
		{
			this.Enter(string.Concat(new string[]
			{
				"ScheduleAction(",
				time.ToString(),
				", ",
				name,
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				smi.Schedule(time, delegate(object obj)
				{
					action(smi);
				}, null);
			});
			return this;
		}

		// Token: 0x060089AA RID: 35242 RVA: 0x0034DBF4 File Offset: 0x0034BDF4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleActionNextFrame(string name, Action<StateMachineInstanceType> action)
		{
			this.Enter("ScheduleActionNextFrame(" + name + ")", delegate(StateMachineInstanceType smi)
			{
				smi.ScheduleNextFrame(delegate(object obj)
				{
					action(smi);
				}, null);
			});
			return this;
		}

		// Token: 0x060089AB RID: 35243 RVA: 0x0034DC34 File Offset: 0x0034BE34
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandler(GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			return this.EventHandler(evt, global_event_system_callback, delegate(StateMachineInstanceType smi, object d)
			{
				callback(smi);
			});
		}

		// Token: 0x060089AC RID: 35244 RVA: 0x0034DC64 File Offset: 0x0034BE64
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandler(GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent.Callback callback)
		{
			if (this.events == null)
			{
				this.events = new List<StateEvent>();
			}
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter targetParameter = this.GetStateTarget();
			GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent gameEvent = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent(evt, callback, targetParameter, global_event_system_callback);
			this.events.Add(gameEvent);
			return this;
		}

		// Token: 0x060089AD RID: 35245 RVA: 0x0034DCA4 File Offset: 0x0034BEA4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandler(GameHashes evt, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback)
		{
			return this.EventHandler(evt, delegate(StateMachineInstanceType smi, object d)
			{
				callback(smi);
			});
		}

		// Token: 0x060089AE RID: 35246 RVA: 0x0034DCD1 File Offset: 0x0034BED1
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandler(GameHashes evt, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent.Callback callback)
		{
			this.EventHandler(evt, null, callback);
			return this;
		}

		// Token: 0x060089AF RID: 35247 RVA: 0x0034DCE0 File Offset: 0x0034BEE0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandlerTransition(GameHashes evt, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, Func<StateMachineInstanceType, object, bool> callback)
		{
			return this.EventHandler(evt, delegate(StateMachineInstanceType smi, object d)
			{
				if (callback(smi, d))
				{
					smi.GoTo(state);
				}
			});
		}

		// Token: 0x060089B0 RID: 35248 RVA: 0x0034DD14 File Offset: 0x0034BF14
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventHandlerTransition(GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, Func<StateMachineInstanceType, object, bool> callback)
		{
			return this.EventHandler(evt, global_event_system_callback, delegate(StateMachineInstanceType smi, object d)
			{
				if (callback(smi, d))
				{
					smi.GoTo(state);
				}
			});
		}

		// Token: 0x060089B1 RID: 35249 RVA: 0x0034DD4C File Offset: 0x0034BF4C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ParamTransition<ParameterType>(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType> parameter, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Callback callback)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self!", null);
			if (this.transitions == null)
			{
				this.transitions = new List<StateMachine.BaseTransition>();
			}
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Transition transition = new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Transition(this.transitions.Count, parameter, state, callback);
			this.transitions.Add(transition);
			return this;
		}

		// Token: 0x060089B2 RID: 35250 RVA: 0x0034DDA0 File Offset: 0x0034BFA0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnSignal(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Signal signal, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, Func<StateMachineInstanceType, bool> callback)
		{
			this.ParamTransition<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter>(signal, state, (StateMachineInstanceType smi, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter p) => callback(smi));
			return this;
		}

		// Token: 0x060089B3 RID: 35251 RVA: 0x0034DDD0 File Offset: 0x0034BFD0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnSignal(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Signal signal, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			this.ParamTransition<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter>(signal, state, null);
			return this;
		}

		// Token: 0x060089B4 RID: 35252 RVA: 0x0034DDE0 File Offset: 0x0034BFE0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EnterTransition(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition)
		{
			string text = "(Stop)";
			if (state != null)
			{
				text = state.name;
			}
			this.Enter("Transition(" + text + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition(smi))
				{
					smi.GoTo(state);
				}
			});
			return this;
		}

		// Token: 0x060089B5 RID: 35253 RVA: 0x0034DE40 File Offset: 0x0034C040
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Transition(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition, UpdateRate update_rate = UpdateRate.SIM_200ms)
		{
			string text = "(Stop)";
			if (state != null)
			{
				text = state.name;
			}
			this.Enter("Transition(" + text + ")", delegate(StateMachineInstanceType smi)
			{
				if (condition(smi))
				{
					smi.GoTo(state);
				}
			});
			this.FastUpdate("Transition(" + text + ")", new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.TransitionUpdater(condition, state), update_rate, false);
			return this;
		}

		// Token: 0x060089B6 RID: 35254 RVA: 0x0034DEC9 File Offset: 0x0034C0C9
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State DefaultState(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State default_state)
		{
			this.defaultState = default_state;
			return this;
		}

		// Token: 0x060089B7 RID: 35255 RVA: 0x0034DED4 File Offset: 0x0034C0D4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EnterGoTo(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self", null);
			string text = "(null)";
			if (state != null)
			{
				text = state.name;
			}
			this.Enter("GoTo(" + text + ")", delegate(StateMachineInstanceType smi)
			{
				smi.GoTo(state);
			});
			return this;
		}

		// Token: 0x060089B8 RID: 35256 RVA: 0x0034DF44 File Offset: 0x0034C144
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State GoTo(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self", null);
			string text = "(null)";
			if (state != null)
			{
				text = state.name;
			}
			this.Update("GoTo(" + text + ")", delegate(StateMachineInstanceType smi, float dt)
			{
				smi.GoTo(state);
			}, UpdateRate.SIM_200ms, false);
			return this;
		}

		// Token: 0x060089B9 RID: 35257 RVA: 0x0034DFB8 File Offset: 0x0034C1B8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State StopMoving()
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target = this.GetStateTarget();
			this.Enter("StopMoving()", delegate(StateMachineInstanceType smi)
			{
				target.Get<Navigator>(smi).Stop(false, true);
			});
			return this;
		}

		// Token: 0x060089BA RID: 35258 RVA: 0x0034DFF0 File Offset: 0x0034C1F0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStationaryIdling()
		{
			this.GetStateTarget();
			this.ToggleTag(GameTags.StationaryIdling);
			return this;
		}

		// Token: 0x060089BB RID: 35259 RVA: 0x0034E008 File Offset: 0x0034C208
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnBehaviourComplete(Tag behaviour, Action<StateMachineInstanceType> cb)
		{
			this.EventHandler(GameHashes.BehaviourTagComplete, delegate(StateMachineInstanceType smi, object d)
			{
				if ((Tag)d == behaviour)
				{
					cb(smi);
				}
			});
			return this;
		}

		// Token: 0x060089BC RID: 35260 RVA: 0x0034E042 File Offset: 0x0034C242
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State MoveTo(Func<StateMachineInstanceType, int> cell_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state = null, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State fail_state = null, bool update_cell = false)
		{
			return this.MoveTo(cell_callback, null, success_state, fail_state, update_cell);
		}

		// Token: 0x060089BD RID: 35261 RVA: 0x0034E050 File Offset: 0x0034C250
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State MoveTo(Func<StateMachineInstanceType, int> cell_callback, Func<StateMachineInstanceType, CellOffset[]> cell_offsets_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state = null, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State fail_state = null, bool update_cell = false)
		{
			this.EventTransition(GameHashes.DestinationReached, success_state, null);
			this.EventTransition(GameHashes.NavigationFailed, fail_state, null);
			CellOffset[] default_offset = new CellOffset[1];
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("MoveTo()", delegate(StateMachineInstanceType smi)
			{
				int num = cell_callback(smi);
				Navigator navigator = state_target.Get<Navigator>(smi);
				CellOffset[] array = default_offset;
				if (cell_offsets_callback != null)
				{
					array = cell_offsets_callback(smi);
				}
				navigator.GoTo(num, array);
			});
			if (update_cell)
			{
				this.Update("MoveTo()", delegate(StateMachineInstanceType smi, float dt)
				{
					int num2 = cell_callback(smi);
					state_target.Get<Navigator>(smi).UpdateTarget(num2);
				}, UpdateRate.SIM_200ms, false);
			}
			this.Exit("StopMoving()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get(smi).GetComponent<Navigator>().Stop(false, true);
			});
			return this;
		}

		// Token: 0x060089BE RID: 35262 RVA: 0x0034E0F8 File Offset: 0x0034C2F8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State MoveTo<ApproachableType>(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter move_parameter, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State fail_state = null, CellOffset[] override_offsets = null, NavTactic tactic = null) where ApproachableType : IApproachable
		{
			this.EventTransition(GameHashes.DestinationReached, success_state, null);
			this.EventTransition(GameHashes.NavigationFailed, fail_state, null);
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			CellOffset[] offsets;
			this.Enter("MoveTo(" + move_parameter.name + ")", delegate(StateMachineInstanceType smi)
			{
				offsets = override_offsets;
				IApproachable approachable = move_parameter.Get<ApproachableType>(smi);
				KMonoBehaviour kmonoBehaviour = move_parameter.Get<KMonoBehaviour>(smi);
				if (kmonoBehaviour == null)
				{
					smi.GoTo(fail_state);
					return;
				}
				Navigator component = state_target.Get(smi).GetComponent<Navigator>();
				if (offsets == null)
				{
					offsets = approachable.GetOffsets();
				}
				component.GoTo(kmonoBehaviour, offsets, tactic);
			});
			this.Exit("StopMoving()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Navigator>(smi).Stop(false, true);
			});
			return this;
		}

		// Token: 0x060089BF RID: 35263 RVA: 0x0034E19C File Offset: 0x0034C39C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State MoveTo<ApproachableType>(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter move_parameter, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, Func<StateMachineInstanceType, CellOffset[]> override_offsets, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State fail_state = null, NavTactic tactic = null) where ApproachableType : IApproachable
		{
			this.EventTransition(GameHashes.DestinationReached, success_state, null);
			this.EventTransition(GameHashes.NavigationFailed, fail_state, null);
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			CellOffset[] offsets;
			this.Enter("MoveTo(" + move_parameter.name + ")", delegate(StateMachineInstanceType smi)
			{
				offsets = override_offsets(smi);
				IApproachable approachable = move_parameter.Get<ApproachableType>(smi);
				KMonoBehaviour kmonoBehaviour = move_parameter.Get<KMonoBehaviour>(smi);
				if (kmonoBehaviour == null)
				{
					smi.GoTo(fail_state);
					return;
				}
				Navigator component = state_target.Get(smi).GetComponent<Navigator>();
				if (offsets == null)
				{
					offsets = approachable.GetOffsets();
				}
				component.GoTo(kmonoBehaviour, offsets, tactic);
			});
			this.Exit("StopMoving()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Navigator>(smi).Stop(false, true);
			});
			return this;
		}

		// Token: 0x060089C0 RID: 35264 RVA: 0x0034E240 File Offset: 0x0034C440
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State MoveTo<ApproachableType>(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter move_parameter, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, Func<StateMachineInstanceType, NavTactic> nav_tactic, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State fail_state = null, CellOffset[] override_offsets = null) where ApproachableType : IApproachable
		{
			this.EventTransition(GameHashes.DestinationReached, success_state, null);
			this.EventTransition(GameHashes.NavigationFailed, fail_state, null);
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			CellOffset[] offsets;
			this.Enter("MoveTo(" + move_parameter.name + ")", delegate(StateMachineInstanceType smi)
			{
				offsets = override_offsets;
				IApproachable approachable = move_parameter.Get<ApproachableType>(smi);
				KMonoBehaviour kmonoBehaviour = move_parameter.Get<KMonoBehaviour>(smi);
				if (kmonoBehaviour == null)
				{
					smi.GoTo(fail_state);
					return;
				}
				Navigator component = state_target.Get(smi).GetComponent<Navigator>();
				if (offsets == null)
				{
					offsets = approachable.GetOffsets();
				}
				NavTactic navTactic = nav_tactic(smi);
				component.GoTo(kmonoBehaviour, offsets, navTactic);
			});
			this.Exit("StopMoving()", delegate(StateMachineInstanceType smi)
			{
				state_target.Get<Navigator>(smi).Stop(false, true);
			});
			return this;
		}

		// Token: 0x060089C1 RID: 35265 RVA: 0x0034E2E4 File Offset: 0x0034C4E4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State Face(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter face_target, float x_offset = 0f)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("Face", delegate(StateMachineInstanceType smi)
			{
				if (face_target != null)
				{
					IApproachable approachable = face_target.Get<IApproachable>(smi);
					if (approachable != null)
					{
						float num = approachable.transform.GetPosition().x + x_offset;
						state_target.Get<Facing>(smi).Face(num);
					}
				}
			});
			return this;
		}

		// Token: 0x060089C2 RID: 35266 RVA: 0x0034E32C File Offset: 0x0034C52C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TagTransition(Tag[] tags, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, bool on_remove = false)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self!", null);
			if (this.transitions == null)
			{
				this.transitions = new List<StateMachine.BaseTransition>();
			}
			GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagTransitionData tagTransitionData = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagTransitionData(tags.ToString(), this, state, this.transitions.Count, tags, on_remove, this.GetStateTarget(), null);
			this.transitions.Add(tagTransitionData);
			return this;
		}

		// Token: 0x060089C3 RID: 35267 RVA: 0x0034E390 File Offset: 0x0034C590
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TagTransition(Func<StateMachineInstanceType, Tag[]> tags_cb, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, bool on_remove = false)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self!", null);
			if (this.transitions == null)
			{
				this.transitions = new List<StateMachine.BaseTransition>();
			}
			GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagTransitionData tagTransitionData = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagTransitionData("DynamicTransition", this, state, this.transitions.Count, null, on_remove, this.GetStateTarget(), tags_cb);
			this.transitions.Add(tagTransitionData);
			return this;
		}

		// Token: 0x060089C4 RID: 35268 RVA: 0x0034E3F0 File Offset: 0x0034C5F0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State TagTransition(Tag tag, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, bool on_remove = false)
		{
			return this.TagTransition(new Tag[] { tag }, state, on_remove);
		}

		// Token: 0x060089C5 RID: 35269 RVA: 0x0034E408 File Offset: 0x0034C608
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventTransition(GameHashes evt, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition = null)
		{
			DebugUtil.DevAssert(state != this, "Can't transition to self!", null);
			if (this.transitions == null)
			{
				this.transitions = new List<StateMachine.BaseTransition>();
			}
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter targetParameter = this.GetStateTarget();
			GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.EventTransitionData eventTransitionData = new GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.EventTransitionData(this, state, this.transitions.Count, evt, global_event_system_callback, condition, targetParameter);
			this.transitions.Add(eventTransitionData);
			return this;
		}

		// Token: 0x060089C6 RID: 35270 RVA: 0x0034E466 File Offset: 0x0034C666
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State EventTransition(GameHashes evt, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition = null)
		{
			return this.EventTransition(evt, null, state, condition);
		}

		// Token: 0x060089C7 RID: 35271 RVA: 0x0034E472 File Offset: 0x0034C672
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ScheduleChange(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State targetState, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback callback)
		{
			return this.EventTransition(GameHashes.ScheduleBlocksChanged, targetState, callback).EventTransition(GameHashes.ScheduleChanged, targetState, callback).EventTransition(GameHashes.ScheduleBlocksTick, targetState, callback);
		}

		// Token: 0x060089C8 RID: 35272 RVA: 0x0034E499 File Offset: 0x0034C699
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ReturnSuccess()
		{
			this.Enter("ReturnSuccess()", delegate(StateMachineInstanceType smi)
			{
				smi.SetStatus(StateMachine.Status.Success);
				smi.StopSM("GameStateMachine.ReturnSuccess()");
			});
			return this;
		}

		// Token: 0x060089C9 RID: 35273 RVA: 0x0034E4C7 File Offset: 0x0034C6C7
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ReturnFailure()
		{
			this.Enter("ReturnFailure()", delegate(StateMachineInstanceType smi)
			{
				smi.SetStatus(StateMachine.Status.Failed);
				smi.StopSM("GameStateMachine.ReturnFailure()");
			});
			return this;
		}

		// Token: 0x060089CA RID: 35274 RVA: 0x0034E4F8 File Offset: 0x0034C6F8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State ToggleStatusItem(string name, string tooltip, string icon = "", StatusItem.IconType icon_type = StatusItem.IconType.Info, NotificationType notification_type = NotificationType.Neutral, bool allow_multiples = false, HashedString render_overlay = default(HashedString), int status_overlays = 129022, Func<string, StateMachineInstanceType, string> resolve_string_callback = null, Func<string, StateMachineInstanceType, string> resolve_tooltip_callback = null, StatusItemCategory category = null)
		{
			StatusItem statusItem = new StatusItem(this.longName, name, tooltip, icon, icon_type, notification_type, allow_multiples, render_overlay, status_overlays, true, null);
			if (resolve_string_callback != null)
			{
				statusItem.resolveStringCallback = (string str, object obj) => resolve_string_callback(str, (StateMachineInstanceType)((object)obj));
			}
			if (resolve_tooltip_callback != null)
			{
				statusItem.resolveTooltipCallback = (string str, object obj) => resolve_tooltip_callback(str, (StateMachineInstanceType)((object)obj));
			}
			this.ToggleStatusItem(statusItem, (StateMachineInstanceType smi) => smi, category);
			return this;
		}

		// Token: 0x060089CB RID: 35275 RVA: 0x0034E594 File Offset: 0x0034C794
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(string anim)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			KAnim.PlayMode mode = KAnim.PlayMode.Once;
			this.Enter(string.Concat(new string[]
			{
				"PlayAnim(",
				anim,
				", ",
				mode.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060089CC RID: 35276 RVA: 0x0034E618 File Offset: 0x0034C818
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(Func<StateMachineInstanceType, string> anim_cb, KAnim.PlayMode mode = KAnim.PlayMode.Once)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("PlayAnim(" + mode.ToString() + ")", delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim_cb(smi), mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060089CD RID: 35277 RVA: 0x0034E67C File Offset: 0x0034C87C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(Func<StateMachineInstanceType, string> anim_cb, Func<StateMachineInstanceType, KAnim.PlayMode> mode_cb)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("PlayAnim(Dynamic)", delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim_cb(smi), mode_cb(smi), 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060089CE RID: 35278 RVA: 0x0034E6C4 File Offset: 0x0034C8C4
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(string anim, KAnim.PlayMode mode)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter(string.Concat(new string[]
			{
				"PlayAnim(",
				anim,
				", ",
				mode.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060089CF RID: 35279 RVA: 0x0034E748 File Offset: 0x0034C948
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnim(string anim, KAnim.PlayMode mode, Func<StateMachineInstanceType, string> suffix_callback)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter(string.Concat(new string[]
			{
				"PlayAnim(",
				anim,
				", ",
				mode.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				string text = "";
				if (suffix_callback != null)
				{
					text = suffix_callback(smi);
				}
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Play(anim + text, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060089D0 RID: 35280 RVA: 0x0034E7D0 File Offset: 0x0034C9D0
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State QueueAnim(Func<StateMachineInstanceType, string> anim_cb, bool loop = false, Func<StateMachineInstanceType, string> suffix_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			KAnim.PlayMode mode = KAnim.PlayMode.Once;
			if (loop)
			{
				mode = KAnim.PlayMode.Loop;
			}
			this.Enter("QueueAnim(" + mode.ToString() + ")", delegate(StateMachineInstanceType smi)
			{
				string text = "";
				if (suffix_callback != null)
				{
					text = suffix_callback(smi);
				}
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Queue(anim_cb(smi) + text, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060089D1 RID: 35281 RVA: 0x0034E844 File Offset: 0x0034CA44
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State QueueAnim(string anim, bool loop = false, Func<StateMachineInstanceType, string> suffix_callback = null)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			KAnim.PlayMode mode = KAnim.PlayMode.Once;
			if (loop)
			{
				mode = KAnim.PlayMode.Loop;
			}
			this.Enter(string.Concat(new string[]
			{
				"QueueAnim(",
				anim,
				", ",
				mode.ToString(),
				")"
			}), delegate(StateMachineInstanceType smi)
			{
				string text = "";
				if (suffix_callback != null)
				{
					text = suffix_callback(smi);
				}
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					kanimControllerBase.Queue(anim + text, mode, 1f, 0f);
				}
			});
			return this;
		}

		// Token: 0x060089D2 RID: 35282 RVA: 0x0034E8D8 File Offset: 0x0034CAD8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnims(Func<StateMachineInstanceType, HashedString[]> anims_callback, KAnim.PlayMode mode = KAnim.PlayMode.Once)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("PlayAnims", delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					HashedString[] array = anims_callback(smi);
					kanimControllerBase.Play(array, mode);
				}
			});
			return this;
		}

		// Token: 0x060089D3 RID: 35283 RVA: 0x0034E920 File Offset: 0x0034CB20
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State PlayAnims(Func<StateMachineInstanceType, HashedString[]> anims_callback, Func<StateMachineInstanceType, KAnim.PlayMode> mode_cb)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("PlayAnims", delegate(StateMachineInstanceType smi)
			{
				KAnimControllerBase kanimControllerBase = state_target.Get<KAnimControllerBase>(smi);
				if (kanimControllerBase != null)
				{
					HashedString[] array = anims_callback(smi);
					KAnim.PlayMode playMode = mode_cb(smi);
					kanimControllerBase.Play(array, playMode);
				}
			});
			return this;
		}

		// Token: 0x060089D4 RID: 35284 RVA: 0x0034E968 File Offset: 0x0034CB68
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State OnAnimQueueComplete(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter state_target = this.GetStateTarget();
			this.Enter("CheckIfAnimQueueIsEmpty", delegate(StateMachineInstanceType smi)
			{
				if (state_target.Get<KBatchedAnimController>(smi).IsStopped())
				{
					smi.GoTo(state);
				}
			});
			return this.EventTransition(GameHashes.AnimQueueComplete, state, null);
		}

		// Token: 0x060089D5 RID: 35285 RVA: 0x0034E9B8 File Offset: 0x0034CBB8
		internal void EventHandler()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04006905 RID: 26885
		[StateMachine.DoNotAutoCreate]
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter stateTarget;

		// Token: 0x0200269F RID: 9887
		private class TransitionUpdater : UpdateBucketWithUpdater<StateMachineInstanceType>.IUpdater
		{
			// Token: 0x0600C45E RID: 50270 RVA: 0x0040DBCE File Offset: 0x0040BDCE
			public TransitionUpdater(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
			{
				this.condition = condition;
				this.state = state;
			}

			// Token: 0x0600C45F RID: 50271 RVA: 0x0040DBE4 File Offset: 0x0040BDE4
			public void Update(StateMachineInstanceType smi, float dt)
			{
				if (this.condition(smi))
				{
					smi.GoTo(this.state);
				}
			}

			// Token: 0x0400AB9E RID: 43934
			private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition;

			// Token: 0x0400AB9F RID: 43935
			private GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state;
		}
	}

	// Token: 0x02001349 RID: 4937
	public class GameEvent : StateEvent
	{
		// Token: 0x060089D9 RID: 35289 RVA: 0x0034E9D9 File Offset: 0x0034CBD9
		public GameEvent(GameHashes id, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent.Callback callback, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target, Func<StateMachineInstanceType, KMonoBehaviour> global_event_system_callback)
			: base(id.ToString())
		{
			this.id = id;
			this.target = target;
			this.callback = callback;
			this.globalEventSystemCallback = global_event_system_callback;
		}

		// Token: 0x060089DA RID: 35290 RVA: 0x0034EA0C File Offset: 0x0034CC0C
		public override StateEvent.Context Subscribe(StateMachine.Instance smi)
		{
			StateEvent.Context context = base.Subscribe(smi);
			StateMachineInstanceType cast_smi = (StateMachineInstanceType)((object)smi);
			Action<object> action = delegate(object d)
			{
				if (StateMachine.Instance.error)
				{
					return;
				}
				this.callback(cast_smi, d);
			};
			if (this.globalEventSystemCallback != null)
			{
				KMonoBehaviour kmonoBehaviour = this.globalEventSystemCallback(cast_smi);
				context.data = kmonoBehaviour.Subscribe((int)this.id, action);
			}
			else
			{
				context.data = this.target.Get(cast_smi).Subscribe((int)this.id, action);
			}
			return context;
		}

		// Token: 0x060089DB RID: 35291 RVA: 0x0034EA9C File Offset: 0x0034CC9C
		public override void Unsubscribe(StateMachine.Instance smi, StateEvent.Context context)
		{
			StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)smi);
			if (this.globalEventSystemCallback != null)
			{
				KMonoBehaviour kmonoBehaviour = this.globalEventSystemCallback(stateMachineInstanceType);
				if (kmonoBehaviour != null)
				{
					kmonoBehaviour.Unsubscribe(context.data);
					return;
				}
			}
			else
			{
				GameObject gameObject = this.target.Get(stateMachineInstanceType);
				if (gameObject != null)
				{
					gameObject.Unsubscribe(context.data);
				}
			}
		}

		// Token: 0x04006906 RID: 26886
		private GameHashes id;

		// Token: 0x04006907 RID: 26887
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target;

		// Token: 0x04006908 RID: 26888
		private GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GameEvent.Callback callback;

		// Token: 0x04006909 RID: 26889
		private Func<StateMachineInstanceType, KMonoBehaviour> globalEventSystemCallback;

		// Token: 0x02002711 RID: 10001
		// (Invoke) Token: 0x0600C596 RID: 50582
		public delegate void Callback(StateMachineInstanceType smi, object callback_data);
	}

	// Token: 0x0200134A RID: 4938
	public class ApproachSubState<ApproachableType> : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State where ApproachableType : IApproachable
	{
		// Token: 0x060089DC RID: 35292 RVA: 0x0034EAFD File Offset: 0x0034CCFD
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter mover, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter move_target, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state = null, CellOffset[] override_offsets = null, NavTactic tactic = null)
		{
			base.root.Target(mover).OnTargetLost(move_target, failure_state).MoveTo<ApproachableType>(move_target, success_state, failure_state, override_offsets, (tactic == null) ? NavigationTactics.ReduceTravelDistance : tactic);
			return this;
		}

		// Token: 0x060089DD RID: 35293 RVA: 0x0034EB2D File Offset: 0x0034CD2D
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter mover, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter move_target, Func<StateMachineInstanceType, CellOffset[]> override_offsets, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state = null, NavTactic tactic = null)
		{
			base.root.Target(mover).OnTargetLost(move_target, failure_state).MoveTo<ApproachableType>(move_target, success_state, override_offsets, failure_state, (tactic == null) ? NavigationTactics.ReduceTravelDistance : tactic);
			return this;
		}

		// Token: 0x060089DE RID: 35294 RVA: 0x0034EB5D File Offset: 0x0034CD5D
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(Func<StateMachineInstanceType, NavTactic> navTactic, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter mover, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter move_target, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state = null, CellOffset[] override_offsets = null)
		{
			base.root.Target(mover).OnTargetLost(move_target, failure_state).MoveTo<ApproachableType>(move_target, success_state, navTactic, failure_state, override_offsets);
			return this;
		}
	}

	// Token: 0x0200134B RID: 4939
	public class DebugGoToSubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060089E0 RID: 35296 RVA: 0x0034EB8C File Offset: 0x0034CD8C
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State exit_state)
		{
			base.root.Enter("GoToCursor", delegate(StateMachineInstanceType smi)
			{
				this.GoToCursor(smi);
			}).EventHandler(GameHashes.DebugGoTo, (StateMachineInstanceType smi) => Game.Instance, delegate(StateMachineInstanceType smi)
			{
				this.GoToCursor(smi);
			}).EventTransition(GameHashes.DestinationReached, exit_state, null)
				.EventTransition(GameHashes.NavigationFailed, exit_state, null);
			return this;
		}

		// Token: 0x060089E1 RID: 35297 RVA: 0x0034EC04 File Offset: 0x0034CE04
		public void GoToCursor(StateMachineInstanceType smi)
		{
			smi.GetComponent<Navigator>().GoTo(Grid.PosToCell(DebugHandler.GetMousePos()), new CellOffset[1]);
		}
	}

	// Token: 0x0200134C RID: 4940
	public class DropSubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060089E5 RID: 35301 RVA: 0x0034EC44 File Offset: 0x0034CE44
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter carrier, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter item, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter drop_target, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state = null)
		{
			base.root.Target(carrier).Enter("Drop", delegate(StateMachineInstanceType smi)
			{
				Storage storage = carrier.Get<Storage>(smi);
				GameObject gameObject = item.Get(smi);
				storage.Drop(gameObject, true);
				int num = Grid.CellAbove(Grid.PosToCell(drop_target.Get<Transform>(smi).GetPosition()));
				gameObject.transform.SetPosition(Grid.CellToPosCCC(num, Grid.SceneLayer.Move));
				smi.GoTo(success_state);
			});
			return this;
		}
	}

	// Token: 0x0200134D RID: 4941
	public class FetchSubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060089E7 RID: 35303 RVA: 0x0034ECA8 File Offset: 0x0034CEA8
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter fetcher, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter pickup_source, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter pickup_chunk, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter requested_amount, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter actual_amount, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success_state, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State failure_state = null)
		{
			base.Target(fetcher);
			base.root.DefaultState(this.approach).ToggleReserve(fetcher, pickup_source, requested_amount, actual_amount);
			this.approach.InitializeStates(fetcher, pickup_source, this.pickup, null, null, NavigationTactics.ReduceTravelDistance).OnTargetLost(pickup_source, failure_state);
			this.pickup.DoPickup(pickup_source, pickup_chunk, actual_amount, success_state, failure_state).EventTransition(GameHashes.AbortWork, failure_state, null);
			return this;
		}

		// Token: 0x0400690A RID: 26890
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.ApproachSubState<Pickupable> approach;

		// Token: 0x0400690B RID: 26891
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State pickup;

		// Token: 0x0400690C RID: 26892
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State success;
	}

	// Token: 0x0200134E RID: 4942
	public class HungrySubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060089E9 RID: 35305 RVA: 0x0034ED28 File Offset: 0x0034CF28
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target, StatusItem status_item)
		{
			base.Target(target);
			base.root.DefaultState(this.satisfied);
			this.satisfied.EventTransition(GameHashes.AddUrge, this.hungry, (StateMachineInstanceType smi) => GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.HungrySubState.IsHungry(smi));
			this.hungry.EventTransition(GameHashes.RemoveUrge, this.satisfied, (StateMachineInstanceType smi) => !GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.HungrySubState.IsHungry(smi)).ToggleStatusItem(status_item, null);
			return this;
		}

		// Token: 0x060089EA RID: 35306 RVA: 0x0034EDC3 File Offset: 0x0034CFC3
		private static bool IsHungry(StateMachineInstanceType smi)
		{
			return smi.GetComponent<ChoreConsumer>().HasUrge(Db.Get().Urges.Eat);
		}

		// Token: 0x0400690D RID: 26893
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State satisfied;

		// Token: 0x0400690E RID: 26894
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State hungry;
	}

	// Token: 0x0200134F RID: 4943
	public class PlantAliveSubState : GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State
	{
		// Token: 0x060089EC RID: 35308 RVA: 0x0034EDEC File Offset: 0x0034CFEC
		public GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State InitializeStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter plant, GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State death_state = null)
		{
			base.root.Target(plant).TagTransition(GameTags.Uprooted, death_state, false).EventTransition(GameHashes.TooColdFatal, death_state, (StateMachineInstanceType smi) => GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.PlantAliveSubState.isLethalTemperature(plant.Get(smi)))
				.EventTransition(GameHashes.TooHotFatal, death_state, (StateMachineInstanceType smi) => GameStateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.PlantAliveSubState.isLethalTemperature(plant.Get(smi)))
				.EventTransition(GameHashes.Drowned, death_state, null);
			return this;
		}

		// Token: 0x060089ED RID: 35309 RVA: 0x0034EE60 File Offset: 0x0034D060
		public bool ForceUpdateStatus(GameObject plant)
		{
			TemperatureVulnerable component = plant.GetComponent<TemperatureVulnerable>();
			EntombVulnerable component2 = plant.GetComponent<EntombVulnerable>();
			PressureVulnerable component3 = plant.GetComponent<PressureVulnerable>();
			return (component == null || !component.IsLethal) && (component2 == null || !component2.GetEntombed) && (component3 == null || !component3.IsLethal);
		}

		// Token: 0x060089EE RID: 35310 RVA: 0x0034EEBC File Offset: 0x0034D0BC
		private static bool isLethalTemperature(GameObject plant)
		{
			TemperatureVulnerable component = plant.GetComponent<TemperatureVulnerable>();
			return !(component == null) && (component.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.LethalCold || component.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.LethalHot);
		}
	}
}
