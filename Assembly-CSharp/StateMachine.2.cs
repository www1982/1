using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ImGuiNET;
using KSerialization;
using UnityEngine;

// Token: 0x02000510 RID: 1296
public class StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> : StateMachine where StateMachineInstanceType : StateMachine.Instance where MasterType : IStateMachineTarget
{
	// Token: 0x06001BB2 RID: 7090 RVA: 0x000972A0 File Offset: 0x000954A0
	public override string[] GetStateNames()
	{
		List<string> list = new List<string>();
		foreach (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state in this.states)
		{
			list.Add(state.name);
		}
		return list.ToArray();
	}

	// Token: 0x06001BB3 RID: 7091 RVA: 0x00097304 File Offset: 0x00095504
	public void Target(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter target)
	{
		this.stateTarget = target;
	}

	// Token: 0x06001BB4 RID: 7092 RVA: 0x00097310 File Offset: 0x00095510
	public void BindState(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State parent_state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, string state_name)
	{
		if (parent_state != null)
		{
			state_name = parent_state.name + "." + state_name;
		}
		state.name = state_name;
		state.longName = this.name + "." + state_name;
		state.debugPushName = "PuS: " + state.longName;
		state.debugPopName = "PoS: " + state.longName;
		state.debugExecuteName = "EA: " + state.longName;
		List<StateMachine.BaseState> list;
		if (parent_state != null)
		{
			list = new List<StateMachine.BaseState>(parent_state.branch);
		}
		else
		{
			list = new List<StateMachine.BaseState>();
		}
		list.Add(state);
		state.parent = parent_state;
		state.branch = list.ToArray();
		this.maxDepth = Math.Max(state.branch.Length, this.maxDepth);
		this.states.Add(state);
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x000973EC File Offset: 0x000955EC
	public void BindStates(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State parent_state, object state_machine)
	{
		foreach (FieldInfo fieldInfo in state_machine.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
		{
			if (fieldInfo.FieldType.IsSubclassOf(typeof(StateMachine.BaseState)))
			{
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)fieldInfo.GetValue(state_machine);
				if (state != parent_state)
				{
					string name = fieldInfo.Name;
					this.BindState(parent_state, state, name);
					this.BindStates(state, state);
				}
			}
		}
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x0009745B File Offset: 0x0009565B
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.InitializeStates(out default_state);
	}

	// Token: 0x06001BB7 RID: 7095 RVA: 0x00097464 File Offset: 0x00095664
	public override void BindStates()
	{
		this.BindStates(null, this);
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x0009746E File Offset: 0x0009566E
	public override Type GetStateMachineInstanceType()
	{
		return typeof(StateMachineInstanceType);
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x0009747C File Offset: 0x0009567C
	public override StateMachine.BaseState GetState(string state_name)
	{
		foreach (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state in this.states)
		{
			if (state.name == state_name)
			{
				return state;
			}
		}
		return null;
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x000974E0 File Offset: 0x000956E0
	public override void FreeResources()
	{
		for (int i = 0; i < this.states.Count; i++)
		{
			this.states[i].FreeResources();
		}
		this.states.Clear();
		base.FreeResources();
	}

	// Token: 0x04001057 RID: 4183
	private List<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State> states = new List<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State>();

	// Token: 0x04001058 RID: 4184
	public StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter masterTarget;

	// Token: 0x04001059 RID: 4185
	[StateMachine.DoNotAutoCreate]
	protected StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter stateTarget;

	// Token: 0x02001362 RID: 4962
	public class GenericInstance : StateMachine.Instance
	{
		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06008A45 RID: 35397 RVA: 0x0034F57B File Offset: 0x0034D77B
		// (set) Token: 0x06008A46 RID: 35398 RVA: 0x0034F583 File Offset: 0x0034D783
		public StateMachineType sm { get; private set; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06008A47 RID: 35399 RVA: 0x0034F58C File Offset: 0x0034D78C
		protected StateMachineInstanceType smi
		{
			get
			{
				return (StateMachineInstanceType)((object)this);
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06008A48 RID: 35400 RVA: 0x0034F594 File Offset: 0x0034D794
		// (set) Token: 0x06008A49 RID: 35401 RVA: 0x0034F59C File Offset: 0x0034D79C
		public MasterType master { get; private set; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06008A4A RID: 35402 RVA: 0x0034F5A5 File Offset: 0x0034D7A5
		// (set) Token: 0x06008A4B RID: 35403 RVA: 0x0034F5AD File Offset: 0x0034D7AD
		public DefType def { get; set; }

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06008A4C RID: 35404 RVA: 0x0034F5B6 File Offset: 0x0034D7B6
		public bool isMasterNull
		{
			get
			{
				return this.internalSm.masterTarget.IsNull((StateMachineInstanceType)((object)this));
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06008A4D RID: 35405 RVA: 0x0034F5CE File Offset: 0x0034D7CE
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType> internalSm
		{
			get
			{
				return (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>)((object)this.sm);
			}
		}

		// Token: 0x06008A4E RID: 35406 RVA: 0x0034F5E0 File Offset: 0x0034D7E0
		protected virtual void OnCleanUp()
		{
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06008A4F RID: 35407 RVA: 0x0034F5E2 File Offset: 0x0034D7E2
		public override float timeinstate
		{
			get
			{
				return Time.time - this.stateEnterTime;
			}
		}

		// Token: 0x06008A50 RID: 35408 RVA: 0x0034F5F0 File Offset: 0x0034D7F0
		public override void FreeResources()
		{
			this.updateHandle.FreeResources();
			this.updateHandle = default(SchedulerHandle);
			this.controller = null;
			if (this.gotoStack != null)
			{
				this.gotoStack.Clear();
			}
			this.gotoStack = null;
			if (this.transitionStack != null)
			{
				this.transitionStack.Clear();
			}
			this.transitionStack = null;
			if (this.currentSchedulerGroup != null)
			{
				this.currentSchedulerGroup.FreeResources();
			}
			this.currentSchedulerGroup = null;
			if (this.stateStack != null)
			{
				for (int i = 0; i < this.stateStack.Length; i++)
				{
					if (this.stateStack[i].schedulerGroup != null)
					{
						this.stateStack[i].schedulerGroup.FreeResources();
					}
				}
			}
			this.stateStack = null;
			base.FreeResources();
		}

		// Token: 0x06008A51 RID: 35409 RVA: 0x0034F6BC File Offset: 0x0034D8BC
		public GenericInstance(MasterType master)
			: base((StateMachine)((object)Singleton<StateMachineManager>.Instance.CreateStateMachine<StateMachineType>()), master)
		{
			this.master = master;
			this.stateStack = new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry[this.stateMachine.GetMaxDepth()];
			for (int i = 0; i < this.stateStack.Length; i++)
			{
				this.stateStack[i].schedulerGroup = Singleton<StateMachineManager>.Instance.CreateSchedulerGroup();
			}
			this.sm = (StateMachineType)((object)this.stateMachine);
			this.dataTable = new object[base.GetStateMachine().dataTableSize];
			this.updateTable = new StateMachine.Instance.UpdateTableEntry[base.GetStateMachine().updateTableSize];
			this.controller = master.GetComponent<StateMachineController>();
			if (this.controller == null)
			{
				this.controller = master.gameObject.AddComponent<StateMachineController>();
			}
			this.internalSm.masterTarget.Set(master.gameObject, this.smi, false);
			this.controller.AddStateMachineInstance(this);
		}

		// Token: 0x06008A52 RID: 35410 RVA: 0x0034F7F8 File Offset: 0x0034D9F8
		public override IStateMachineTarget GetMaster()
		{
			return this.master;
		}

		// Token: 0x06008A53 RID: 35411 RVA: 0x0034F808 File Offset: 0x0034DA08
		private void PushEvent(StateEvent evt)
		{
			StateEvent.Context context = evt.Subscribe(this);
			this.subscribedEvents.Push(context);
		}

		// Token: 0x06008A54 RID: 35412 RVA: 0x0034F82C File Offset: 0x0034DA2C
		private void PopEvent()
		{
			StateEvent.Context context = this.subscribedEvents.Pop();
			context.stateEvent.Unsubscribe(this, context);
		}

		// Token: 0x06008A55 RID: 35413 RVA: 0x0034F854 File Offset: 0x0034DA54
		private bool TryEvaluateTransitions(StateMachine.BaseState state, int goto_id)
		{
			if (state.transitions == null)
			{
				return true;
			}
			bool flag = true;
			for (int i = 0; i < state.transitions.Count; i++)
			{
				StateMachine.BaseTransition baseTransition = state.transitions[i];
				if (goto_id != this.gotoId)
				{
					flag = false;
					break;
				}
				baseTransition.Evaluate(this.smi);
			}
			return flag;
		}

		// Token: 0x06008A56 RID: 35414 RVA: 0x0034F8B0 File Offset: 0x0034DAB0
		private void PushTransitions(StateMachine.BaseState state)
		{
			if (state.transitions == null)
			{
				return;
			}
			for (int i = 0; i < state.transitions.Count; i++)
			{
				StateMachine.BaseTransition baseTransition = state.transitions[i];
				this.PushTransition(baseTransition);
			}
		}

		// Token: 0x06008A57 RID: 35415 RVA: 0x0034F8F0 File Offset: 0x0034DAF0
		private void PushTransition(StateMachine.BaseTransition transition)
		{
			StateMachine.BaseTransition.Context context = transition.Register(this.smi);
			this.transitionStack.Push(context);
		}

		// Token: 0x06008A58 RID: 35416 RVA: 0x0034F91C File Offset: 0x0034DB1C
		private void PopTransition(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state)
		{
			StateMachine.BaseTransition.Context context = this.transitionStack.Pop();
			state.transitions[context.idx].Unregister(this.smi, context);
		}

		// Token: 0x06008A59 RID: 35417 RVA: 0x0034F958 File Offset: 0x0034DB58
		private void PushState(StateMachine.BaseState state)
		{
			int num = this.gotoId;
			this.currentActionIdx = -1;
			if (state.events != null)
			{
				foreach (StateEvent stateEvent in state.events)
				{
					this.PushEvent(stateEvent);
				}
			}
			this.PushTransitions(state);
			if (state.updateActions != null)
			{
				for (int i = 0; i < state.updateActions.Count; i++)
				{
					StateMachine.UpdateAction updateAction = state.updateActions[i];
					int updateTableIdx = updateAction.updateTableIdx;
					int nextBucketIdx = updateAction.nextBucketIdx;
					updateAction.nextBucketIdx = (updateAction.nextBucketIdx + 1) % updateAction.buckets.Length;
					UpdateBucketWithUpdater<StateMachineInstanceType> updateBucketWithUpdater = (UpdateBucketWithUpdater<StateMachineInstanceType>)updateAction.buckets[nextBucketIdx];
					this.smi.updateTable[updateTableIdx].bucket = updateBucketWithUpdater;
					this.smi.updateTable[updateTableIdx].handle = updateBucketWithUpdater.Add(this.smi, Singleton<StateMachineUpdater>.Instance.GetFrameTime(updateAction.updateRate, updateBucketWithUpdater.frame), (UpdateBucketWithUpdater<StateMachineInstanceType>.IUpdater)updateAction.updater);
					state.updateActions[i] = updateAction;
				}
			}
			this.stateEnterTime = Time.time;
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry[] array = this.stateStack;
			int stackSize = this.stackSize;
			this.stackSize = stackSize + 1;
			array[stackSize].state = state;
			this.currentSchedulerGroup = this.stateStack[this.stackSize - 1].schedulerGroup;
			if (!this.TryEvaluateTransitions(state, num))
			{
				return;
			}
			if (num != this.gotoId)
			{
				return;
			}
			this.ExecuteActions((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)state, state.enterActions);
			int num2 = this.gotoId;
		}

		// Token: 0x06008A5A RID: 35418 RVA: 0x0034FB34 File Offset: 0x0034DD34
		private void ExecuteActions(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, List<StateMachine.Action> actions)
		{
			if (actions == null)
			{
				return;
			}
			int num = this.gotoId;
			this.currentActionIdx++;
			while (this.currentActionIdx < actions.Count && num == this.gotoId)
			{
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback callback = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State.Callback)actions[this.currentActionIdx].callback;
				try
				{
					callback(this.smi);
				}
				catch (Exception ex)
				{
					if (!StateMachine.Instance.error)
					{
						base.Error();
						string text = "(NULL).";
						IStateMachineTarget master = this.GetMaster();
						if (!master.isNull)
						{
							KPrefabID component = master.GetComponent<KPrefabID>();
							if (component != null)
							{
								text = "(" + component.PrefabTag.ToString() + ").";
							}
							else
							{
								text = "(" + base.gameObject.name + ").";
							}
						}
						string text2 = string.Concat(new string[]
						{
							"Exception in: ",
							text,
							this.stateMachine.ToString(),
							".",
							state.name,
							"."
						});
						if (this.currentActionIdx > 0 && this.currentActionIdx < actions.Count)
						{
							text2 += actions[this.currentActionIdx].name;
						}
						DebugUtil.LogException(this.controller, text2, ex);
					}
				}
				this.currentActionIdx++;
			}
			this.currentActionIdx = 2147483646;
		}

		// Token: 0x06008A5B RID: 35419 RVA: 0x0034FCC8 File Offset: 0x0034DEC8
		private void PopState()
		{
			this.currentActionIdx = -1;
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry[] array = this.stateStack;
			int num = this.stackSize - 1;
			this.stackSize = num;
			StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry stackEntry = array[num];
			StateMachine.BaseState state = stackEntry.state;
			int num2 = 0;
			while (state.transitions != null && num2 < state.transitions.Count)
			{
				this.PopTransition((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)state);
				num2++;
			}
			if (state.events != null)
			{
				for (int i = 0; i < state.events.Count; i++)
				{
					this.PopEvent();
				}
			}
			if (state.updateActions != null)
			{
				foreach (StateMachine.UpdateAction updateAction in state.updateActions)
				{
					int updateTableIdx = updateAction.updateTableIdx;
					StateMachineUpdater.BaseUpdateBucket baseUpdateBucket = (UpdateBucketWithUpdater<StateMachineInstanceType>)this.smi.updateTable[updateTableIdx].bucket;
					this.smi.updateTable[updateTableIdx].bucket = null;
					baseUpdateBucket.Remove(this.smi.updateTable[updateTableIdx].handle);
				}
			}
			stackEntry.schedulerGroup.Reset();
			this.currentSchedulerGroup = stackEntry.schedulerGroup;
			this.ExecuteActions((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)state, state.exitActions);
		}

		// Token: 0x06008A5C RID: 35420 RVA: 0x0034FE2C File Offset: 0x0034E02C
		public override SchedulerHandle Schedule(float time, Action<object> callback, object callback_data = null)
		{
			string text = null;
			return Singleton<StateMachineManager>.Instance.Schedule(text, time, callback, callback_data, this.currentSchedulerGroup);
		}

		// Token: 0x06008A5D RID: 35421 RVA: 0x0034FE50 File Offset: 0x0034E050
		public override SchedulerHandle ScheduleNextFrame(Action<object> callback, object callback_data = null)
		{
			string text = null;
			return Singleton<StateMachineManager>.Instance.ScheduleNextFrame(text, callback, callback_data, this.currentSchedulerGroup);
		}

		// Token: 0x06008A5E RID: 35422 RVA: 0x0034FE72 File Offset: 0x0034E072
		public override void StartSM()
		{
			if (this.controller != null && !this.controller.HasStateMachineInstance(this))
			{
				this.controller.AddStateMachineInstance(this);
			}
			base.StartSM();
		}

		// Token: 0x06008A5F RID: 35423 RVA: 0x0034FEA4 File Offset: 0x0034E0A4
		public override void StopSM(string reason)
		{
			if (StateMachine.Instance.error)
			{
				return;
			}
			if (this.controller != null)
			{
				this.controller.RemoveStateMachineInstance(this);
			}
			if (!base.IsRunning())
			{
				return;
			}
			this.gotoId++;
			while (this.stackSize > 0)
			{
				this.PopState();
			}
			if (this.master != null && this.controller != null)
			{
				this.controller.RemoveStateMachineInstance(this);
			}
			if (this.status == StateMachine.Status.Running)
			{
				base.SetStatus(StateMachine.Status.Failed);
			}
			if (this.OnStop != null)
			{
				this.OnStop(reason, this.status);
			}
			for (int i = 0; i < this.parameterContexts.Length; i++)
			{
				this.parameterContexts[i].Cleanup();
			}
			this.OnCleanUp();
		}

		// Token: 0x06008A60 RID: 35424 RVA: 0x0034FF72 File Offset: 0x0034E172
		private void FinishStateInProgress(StateMachine.BaseState state)
		{
			if (state.enterActions == null)
			{
				return;
			}
			this.ExecuteActions((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State)state, state.enterActions);
		}

		// Token: 0x06008A61 RID: 35425 RVA: 0x0034FF90 File Offset: 0x0034E190
		public override void GoTo(StateMachine.BaseState base_state)
		{
			if (App.IsExiting)
			{
				return;
			}
			if (StateMachine.Instance.error)
			{
				return;
			}
			if (this.isMasterNull)
			{
				return;
			}
			if (this.smi.IsNullOrDestroyed())
			{
				return;
			}
			try
			{
				if (base.IsBreakOnGoToEnabled())
				{
					Debugger.Break();
				}
				if (base_state != null)
				{
					while (base_state.defaultState != null)
					{
						base_state = base_state.defaultState;
					}
				}
				if (this.GetCurrentState() == null)
				{
					base.SetStatus(StateMachine.Status.Running);
				}
				if (this.gotoStack.Count > 100)
				{
					string text = "Potential infinite transition loop detected in state machine: " + this.ToString() + "\nGoto stack:\n";
					foreach (StateMachine.BaseState baseState in this.gotoStack)
					{
						text = text + "\n" + baseState.name;
					}
					global::Debug.LogError(text);
					base.Error();
				}
				else
				{
					this.gotoStack.Push(base_state);
					if (base_state == null)
					{
						this.StopSM("StateMachine.GoTo(null)");
						this.gotoStack.Pop();
					}
					else
					{
						int num = this.gotoId + 1;
						this.gotoId = num;
						int num2 = num;
						StateMachine.BaseState[] branch = (base_state as StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State).branch;
						int num3 = 0;
						while (num3 < this.stackSize && num3 < branch.Length && this.stateStack[num3].state == branch[num3])
						{
							num3++;
						}
						int num4 = this.stackSize - 1;
						if (num4 >= 0 && num4 == num3 - 1)
						{
							this.FinishStateInProgress(this.stateStack[num4].state);
						}
						while (this.stackSize > num3 && num2 == this.gotoId)
						{
							this.PopState();
						}
						int num5 = num3;
						while (num5 < branch.Length && num2 == this.gotoId)
						{
							this.PushState(branch[num5]);
							num5++;
						}
						this.gotoStack.Pop();
					}
				}
			}
			catch (Exception ex)
			{
				if (!StateMachine.Instance.error)
				{
					base.Error();
					string text2 = "(Stop)";
					if (base_state != null)
					{
						text2 = base_state.name;
					}
					string text3 = "(NULL).";
					if (!this.GetMaster().isNull)
					{
						text3 = "(" + base.gameObject.name + ").";
					}
					string text4 = string.Concat(new string[]
					{
						"Exception in: ",
						text3,
						this.stateMachine.ToString(),
						".GoTo(",
						text2,
						")"
					});
					DebugUtil.LogErrorArgs(this.controller, new object[] { text4 + "\n" + ex.ToString() });
				}
			}
		}

		// Token: 0x06008A62 RID: 35426 RVA: 0x0035025C File Offset: 0x0034E45C
		public override StateMachine.BaseState GetCurrentState()
		{
			if (this.stackSize > 0)
			{
				return this.stateStack[this.stackSize - 1].state;
			}
			return null;
		}

		// Token: 0x0400694D RID: 26957
		private float stateEnterTime;

		// Token: 0x0400694E RID: 26958
		private int gotoId;

		// Token: 0x0400694F RID: 26959
		private int currentActionIdx = -1;

		// Token: 0x04006950 RID: 26960
		private SchedulerHandle updateHandle;

		// Token: 0x04006951 RID: 26961
		private Stack<StateMachine.BaseState> gotoStack = new Stack<StateMachine.BaseState>();

		// Token: 0x04006952 RID: 26962
		protected Stack<StateMachine.BaseTransition.Context> transitionStack = new Stack<StateMachine.BaseTransition.Context>();

		// Token: 0x04006956 RID: 26966
		protected StateMachineController controller;

		// Token: 0x04006957 RID: 26967
		private SchedulerGroup currentSchedulerGroup;

		// Token: 0x04006958 RID: 26968
		private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.GenericInstance.StackEntry[] stateStack;

		// Token: 0x0200271A RID: 10010
		public struct StackEntry
		{
			// Token: 0x0400AD02 RID: 44290
			public StateMachine.BaseState state;

			// Token: 0x0400AD03 RID: 44291
			public SchedulerGroup schedulerGroup;
		}
	}

	// Token: 0x02001363 RID: 4963
	public class State : StateMachine.BaseState
	{
		// Token: 0x04006959 RID: 26969
		protected StateMachineType sm;

		// Token: 0x0200271B RID: 10011
		// (Invoke) Token: 0x0600C5AF RID: 50607
		public delegate void Callback(StateMachineInstanceType smi);
	}

	// Token: 0x02001364 RID: 4964
	public new abstract class ParameterTransition : StateMachine.ParameterTransition
	{
		// Token: 0x06008A64 RID: 35428 RVA: 0x00350289 File Offset: 0x0034E489
		public ParameterTransition(int idx, string name, StateMachine.BaseState source_state, StateMachine.BaseState target_state)
			: base(idx, name, source_state, target_state)
		{
		}
	}

	// Token: 0x02001365 RID: 4965
	public class Transition : StateMachine.BaseTransition
	{
		// Token: 0x06008A65 RID: 35429 RVA: 0x00350296 File Offset: 0x0034E496
		public Transition(string name, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State source_state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State target_state, int idx, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition)
			: base(idx, name, source_state, target_state)
		{
			this.condition = condition;
		}

		// Token: 0x06008A66 RID: 35430 RVA: 0x003502AB File Offset: 0x0034E4AB
		public override string ToString()
		{
			if (this.targetState != null)
			{
				return this.name + "->" + this.targetState.name;
			}
			return this.name + "->(Stop)";
		}

		// Token: 0x0400695A RID: 26970
		public StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Transition.ConditionCallback condition;

		// Token: 0x0200271C RID: 10012
		// (Invoke) Token: 0x0600C5B3 RID: 50611
		public delegate bool ConditionCallback(StateMachineInstanceType smi);
	}

	// Token: 0x02001366 RID: 4966
	public abstract class Parameter<ParameterType> : StateMachine.Parameter
	{
		// Token: 0x06008A67 RID: 35431 RVA: 0x003502E1 File Offset: 0x0034E4E1
		public Parameter()
		{
		}

		// Token: 0x06008A68 RID: 35432 RVA: 0x003502E9 File Offset: 0x0034E4E9
		public Parameter(ParameterType default_value)
		{
			this.defaultValue = default_value;
		}

		// Token: 0x06008A69 RID: 35433 RVA: 0x003502F8 File Offset: 0x0034E4F8
		public ParameterType Set(ParameterType value, StateMachineInstanceType smi, bool silenceEvents = false)
		{
			((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this)).Set(value, smi, silenceEvents);
			return value;
		}

		// Token: 0x06008A6A RID: 35434 RVA: 0x00350314 File Offset: 0x0034E514
		public ParameterType Get(StateMachineInstanceType smi)
		{
			return ((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this)).value;
		}

		// Token: 0x06008A6B RID: 35435 RVA: 0x0035032C File Offset: 0x0034E52C
		public StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context GetContext(StateMachineInstanceType smi)
		{
			return (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this);
		}

		// Token: 0x0400695B RID: 26971
		public ParameterType defaultValue;

		// Token: 0x0400695C RID: 26972
		public bool isSignal;

		// Token: 0x0200271D RID: 10013
		// (Invoke) Token: 0x0600C5B7 RID: 50615
		public delegate bool Callback(StateMachineInstanceType smi, ParameterType p);

		// Token: 0x0200271E RID: 10014
		public class Transition : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.ParameterTransition
		{
			// Token: 0x0600C5BA RID: 50618 RVA: 0x00410C46 File Offset: 0x0040EE46
			public Transition(int idx, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType> parameter, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.State state, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Callback callback)
				: base(idx, parameter.name, null, state)
			{
				this.parameter = parameter;
				this.callback = callback;
			}

			// Token: 0x0600C5BB RID: 50619 RVA: 0x00410C68 File Offset: 0x0040EE68
			public override void Evaluate(StateMachine.Instance smi)
			{
				StateMachineInstanceType stateMachineInstanceType = smi as StateMachineInstanceType;
				global::Debug.Assert(stateMachineInstanceType != null);
				if (this.parameter.isSignal && this.callback == null)
				{
					return;
				}
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)stateMachineInstanceType.GetParameterContext(this.parameter);
				if (this.callback(stateMachineInstanceType, context.value))
				{
					stateMachineInstanceType.GoTo(this.targetState);
				}
			}

			// Token: 0x0600C5BC RID: 50620 RVA: 0x00410CE1 File Offset: 0x0040EEE1
			private void Trigger(StateMachineInstanceType smi)
			{
				smi.GoTo(this.targetState);
			}

			// Token: 0x0600C5BD RID: 50621 RVA: 0x00410CF4 File Offset: 0x0040EEF4
			public override StateMachine.BaseTransition.Context Register(StateMachine.Instance smi)
			{
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this.parameter);
				if (this.parameter.isSignal && this.callback == null)
				{
					StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context2 = context;
					context2.onDirty = (Action<StateMachineInstanceType>)Delegate.Combine(context2.onDirty, new Action<StateMachineInstanceType>(this.Trigger));
				}
				else
				{
					StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context3 = context;
					context3.onDirty = (Action<StateMachineInstanceType>)Delegate.Combine(context3.onDirty, new Action<StateMachineInstanceType>(this.Evaluate));
				}
				return new StateMachine.BaseTransition.Context(this);
			}

			// Token: 0x0600C5BE RID: 50622 RVA: 0x00410D78 File Offset: 0x0040EF78
			public override void Unregister(StateMachine.Instance smi, StateMachine.BaseTransition.Context transitionContext)
			{
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context = (StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context)smi.GetParameterContext(this.parameter);
				if (this.parameter.isSignal && this.callback == null)
				{
					StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context2 = context;
					context2.onDirty = (Action<StateMachineInstanceType>)Delegate.Remove(context2.onDirty, new Action<StateMachineInstanceType>(this.Trigger));
					return;
				}
				StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Context context3 = context;
				context3.onDirty = (Action<StateMachineInstanceType>)Delegate.Remove(context3.onDirty, new Action<StateMachineInstanceType>(this.Evaluate));
			}

			// Token: 0x0600C5BF RID: 50623 RVA: 0x00410DF2 File Offset: 0x0040EFF2
			public override string ToString()
			{
				if (this.targetState != null)
				{
					return this.parameter.name + "->" + this.targetState.name;
				}
				return this.parameter.name + "->(Stop)";
			}

			// Token: 0x0400AD04 RID: 44292
			private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType> parameter;

			// Token: 0x0400AD05 RID: 44293
			private StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ParameterType>.Callback callback;
		}

		// Token: 0x0200271F RID: 10015
		public new abstract class Context : StateMachine.Parameter.Context
		{
			// Token: 0x0600C5C0 RID: 50624 RVA: 0x00410E32 File Offset: 0x0040F032
			public Context(StateMachine.Parameter parameter, ParameterType default_value)
				: base(parameter)
			{
				this.value = default_value;
			}

			// Token: 0x0600C5C1 RID: 50625 RVA: 0x00410E42 File Offset: 0x0040F042
			public virtual void Set(ParameterType value, StateMachineInstanceType smi, bool silenceEvents = false)
			{
				if (!EqualityComparer<ParameterType>.Default.Equals(value, this.value))
				{
					this.value = value;
					if (!silenceEvents && this.onDirty != null)
					{
						this.onDirty(smi);
					}
				}
			}

			// Token: 0x0400AD06 RID: 44294
			public ParameterType value;

			// Token: 0x0400AD07 RID: 44295
			public Action<StateMachineInstanceType> onDirty;
		}
	}

	// Token: 0x02001367 RID: 4967
	public class BoolParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<bool>
	{
		// Token: 0x06008A6C RID: 35436 RVA: 0x0035033F File Offset: 0x0034E53F
		public BoolParameter()
		{
		}

		// Token: 0x06008A6D RID: 35437 RVA: 0x00350347 File Offset: 0x0034E547
		public BoolParameter(bool default_value)
			: base(default_value)
		{
		}

		// Token: 0x06008A6E RID: 35438 RVA: 0x00350350 File Offset: 0x0034E550
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.BoolParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02002720 RID: 10016
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<bool>.Context
		{
			// Token: 0x0600C5C2 RID: 50626 RVA: 0x00410E75 File Offset: 0x0040F075
			public Context(StateMachine.Parameter parameter, bool default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5C3 RID: 50627 RVA: 0x00410E7F File Offset: 0x0040F07F
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value ? 1 : 0);
			}

			// Token: 0x0600C5C4 RID: 50628 RVA: 0x00410E94 File Offset: 0x0040F094
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = reader.ReadByte() > 0;
			}

			// Token: 0x0600C5C5 RID: 50629 RVA: 0x00410EA5 File Offset: 0x0040F0A5
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5C6 RID: 50630 RVA: 0x00410EA8 File Offset: 0x0040F0A8
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				bool value = this.value;
				if (ImGui.Checkbox(this.parameter.name, ref value))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(value, stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x02001368 RID: 4968
	public class Vector3Parameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<Vector3>
	{
		// Token: 0x06008A6F RID: 35439 RVA: 0x0035035E File Offset: 0x0034E55E
		public Vector3Parameter()
		{
		}

		// Token: 0x06008A70 RID: 35440 RVA: 0x00350366 File Offset: 0x0034E566
		public Vector3Parameter(Vector3 default_value)
			: base(default_value)
		{
		}

		// Token: 0x06008A71 RID: 35441 RVA: 0x0035036F File Offset: 0x0034E56F
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Vector3Parameter.Context(this, this.defaultValue);
		}

		// Token: 0x02002721 RID: 10017
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<Vector3>.Context
		{
			// Token: 0x0600C5C7 RID: 50631 RVA: 0x00410EE0 File Offset: 0x0040F0E0
			public Context(StateMachine.Parameter parameter, Vector3 default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5C8 RID: 50632 RVA: 0x00410EEA File Offset: 0x0040F0EA
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value.x);
				writer.Write(this.value.y);
				writer.Write(this.value.z);
			}

			// Token: 0x0600C5C9 RID: 50633 RVA: 0x00410F1F File Offset: 0x0040F11F
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value.x = reader.ReadSingle();
				this.value.y = reader.ReadSingle();
				this.value.z = reader.ReadSingle();
			}

			// Token: 0x0600C5CA RID: 50634 RVA: 0x00410F54 File Offset: 0x0040F154
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5CB RID: 50635 RVA: 0x00410F58 File Offset: 0x0040F158
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				Vector3 value = this.value;
				if (ImGui.InputFloat3(this.parameter.name, ref value))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(value, stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x02001369 RID: 4969
	public class EnumParameter<EnumType> : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<EnumType>
	{
		// Token: 0x06008A72 RID: 35442 RVA: 0x0035037D File Offset: 0x0034E57D
		public EnumParameter(EnumType default_value)
			: base(default_value)
		{
		}

		// Token: 0x06008A73 RID: 35443 RVA: 0x00350386 File Offset: 0x0034E586
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.EnumParameter<EnumType>.Context(this, this.defaultValue);
		}

		// Token: 0x02002722 RID: 10018
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<EnumType>.Context
		{
			// Token: 0x0600C5CC RID: 50636 RVA: 0x00410F90 File Offset: 0x0040F190
			public Context(StateMachine.Parameter parameter, EnumType default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5CD RID: 50637 RVA: 0x00410F9A File Offset: 0x0040F19A
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write((int)((object)this.value));
			}

			// Token: 0x0600C5CE RID: 50638 RVA: 0x00410FB2 File Offset: 0x0040F1B2
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = (EnumType)((object)reader.ReadInt32());
			}

			// Token: 0x0600C5CF RID: 50639 RVA: 0x00410FCA File Offset: 0x0040F1CA
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5D0 RID: 50640 RVA: 0x00410FCC File Offset: 0x0040F1CC
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				string[] names = Enum.GetNames(typeof(EnumType));
				Array values = Enum.GetValues(typeof(EnumType));
				int num = Array.IndexOf(values, this.value);
				if (ImGui.Combo(this.parameter.name, ref num, names, names.Length))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set((EnumType)((object)values.GetValue(num)), stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x0200136A RID: 4970
	public class FloatParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>
	{
		// Token: 0x06008A74 RID: 35444 RVA: 0x00350394 File Offset: 0x0034E594
		public FloatParameter()
		{
		}

		// Token: 0x06008A75 RID: 35445 RVA: 0x0035039C File Offset: 0x0034E59C
		public FloatParameter(float default_value)
			: base(default_value)
		{
		}

		// Token: 0x06008A76 RID: 35446 RVA: 0x003503A8 File Offset: 0x0034E5A8
		public float Delta(float delta_value, StateMachineInstanceType smi)
		{
			float num = base.Get(smi);
			num += delta_value;
			base.Set(num, smi, false);
			return num;
		}

		// Token: 0x06008A77 RID: 35447 RVA: 0x003503CC File Offset: 0x0034E5CC
		public float DeltaClamp(float delta_value, float min_value, float max_value, StateMachineInstanceType smi)
		{
			float num = base.Get(smi);
			num += delta_value;
			num = Mathf.Clamp(num, min_value, max_value);
			base.Set(num, smi, false);
			return num;
		}

		// Token: 0x06008A78 RID: 35448 RVA: 0x003503FB File Offset: 0x0034E5FB
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.FloatParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02002723 RID: 10019
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<float>.Context
		{
			// Token: 0x0600C5D1 RID: 50641 RVA: 0x0041103E File Offset: 0x0040F23E
			public Context(StateMachine.Parameter parameter, float default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5D2 RID: 50642 RVA: 0x00411048 File Offset: 0x0040F248
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value);
			}

			// Token: 0x0600C5D3 RID: 50643 RVA: 0x00411056 File Offset: 0x0040F256
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = reader.ReadSingle();
			}

			// Token: 0x0600C5D4 RID: 50644 RVA: 0x00411064 File Offset: 0x0040F264
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5D5 RID: 50645 RVA: 0x00411068 File Offset: 0x0040F268
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				float value = this.value;
				if (ImGui.InputFloat(this.parameter.name, ref value))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(value, stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x0200136B RID: 4971
	public class IntParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<int>
	{
		// Token: 0x06008A79 RID: 35449 RVA: 0x00350409 File Offset: 0x0034E609
		public IntParameter()
		{
		}

		// Token: 0x06008A7A RID: 35450 RVA: 0x00350411 File Offset: 0x0034E611
		public IntParameter(int default_value)
			: base(default_value)
		{
		}

		// Token: 0x06008A7B RID: 35451 RVA: 0x0035041C File Offset: 0x0034E61C
		public int Delta(int delta_value, StateMachineInstanceType smi)
		{
			int num = base.Get(smi);
			num += delta_value;
			base.Set(num, smi, false);
			return num;
		}

		// Token: 0x06008A7C RID: 35452 RVA: 0x00350440 File Offset: 0x0034E640
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.IntParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02002724 RID: 10020
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<int>.Context
		{
			// Token: 0x0600C5D6 RID: 50646 RVA: 0x004110A0 File Offset: 0x0040F2A0
			public Context(StateMachine.Parameter parameter, int default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5D7 RID: 50647 RVA: 0x004110AA File Offset: 0x0040F2AA
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value);
			}

			// Token: 0x0600C5D8 RID: 50648 RVA: 0x004110B8 File Offset: 0x0040F2B8
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = reader.ReadInt32();
			}

			// Token: 0x0600C5D9 RID: 50649 RVA: 0x004110C6 File Offset: 0x0040F2C6
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5DA RID: 50650 RVA: 0x004110C8 File Offset: 0x0040F2C8
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				int value = this.value;
				if (ImGui.InputInt(this.parameter.name, ref value))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(value, stateMachineInstanceType, false);
				}
			}
		}
	}

	// Token: 0x0200136C RID: 4972
	public class LongParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<long>
	{
		// Token: 0x06008A7D RID: 35453 RVA: 0x0035044E File Offset: 0x0034E64E
		public LongParameter()
		{
		}

		// Token: 0x06008A7E RID: 35454 RVA: 0x00350456 File Offset: 0x0034E656
		public LongParameter(long default_value)
			: base(default_value)
		{
		}

		// Token: 0x06008A7F RID: 35455 RVA: 0x00350460 File Offset: 0x0034E660
		public long Delta(long delta_value, StateMachineInstanceType smi)
		{
			long num = base.Get(smi);
			num += delta_value;
			base.Set(num, smi, false);
			return num;
		}

		// Token: 0x06008A80 RID: 35456 RVA: 0x00350484 File Offset: 0x0034E684
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.LongParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02002725 RID: 10021
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<long>.Context
		{
			// Token: 0x0600C5DB RID: 50651 RVA: 0x00411100 File Offset: 0x0040F300
			public Context(StateMachine.Parameter parameter, long default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5DC RID: 50652 RVA: 0x0041110A File Offset: 0x0040F30A
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value);
			}

			// Token: 0x0600C5DD RID: 50653 RVA: 0x00411118 File Offset: 0x0040F318
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = reader.ReadInt64();
			}

			// Token: 0x0600C5DE RID: 50654 RVA: 0x00411126 File Offset: 0x0040F326
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5DF RID: 50655 RVA: 0x00411128 File Offset: 0x0040F328
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				long value = this.value;
			}
		}
	}

	// Token: 0x0200136D RID: 4973
	public class ResourceParameter<ResourceType> : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ResourceType> where ResourceType : Resource
	{
		// Token: 0x06008A81 RID: 35457 RVA: 0x00350494 File Offset: 0x0034E694
		public ResourceParameter()
			: base(default(ResourceType))
		{
		}

		// Token: 0x06008A82 RID: 35458 RVA: 0x003504B0 File Offset: 0x0034E6B0
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.ResourceParameter<ResourceType>.Context(this, this.defaultValue);
		}

		// Token: 0x02002726 RID: 10022
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ResourceType>.Context
		{
			// Token: 0x0600C5E0 RID: 50656 RVA: 0x00411131 File Offset: 0x0040F331
			public Context(StateMachine.Parameter parameter, ResourceType default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5E1 RID: 50657 RVA: 0x0041113C File Offset: 0x0040F33C
			public override void Serialize(BinaryWriter writer)
			{
				string text = "";
				if (this.value != null)
				{
					if (this.value.Guid == null)
					{
						global::Debug.LogError("Cannot serialize resource with invalid guid: " + this.value.Id);
					}
					else
					{
						text = this.value.Guid.Guid;
					}
				}
				writer.WriteKleiString(text);
			}

			// Token: 0x0600C5E2 RID: 50658 RVA: 0x004111B4 File Offset: 0x0040F3B4
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				string text = reader.ReadKleiString();
				if (text != "")
				{
					ResourceGuid resourceGuid = new ResourceGuid(text, null);
					this.value = Db.Get().GetResource<ResourceType>(resourceGuid);
				}
			}

			// Token: 0x0600C5E3 RID: 50659 RVA: 0x004111EE File Offset: 0x0040F3EE
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5E4 RID: 50660 RVA: 0x004111F0 File Offset: 0x0040F3F0
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				string text = "None";
				if (this.value != null)
				{
					text = this.value.ToString();
				}
				ImGui.LabelText(this.parameter.name, text);
			}
		}
	}

	// Token: 0x0200136E RID: 4974
	public class TagParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<Tag>
	{
		// Token: 0x06008A83 RID: 35459 RVA: 0x003504BE File Offset: 0x0034E6BE
		public TagParameter()
		{
		}

		// Token: 0x06008A84 RID: 35460 RVA: 0x003504C6 File Offset: 0x0034E6C6
		public TagParameter(Tag default_value)
			: base(default_value)
		{
		}

		// Token: 0x06008A85 RID: 35461 RVA: 0x003504CF File Offset: 0x0034E6CF
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TagParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02002727 RID: 10023
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<Tag>.Context
		{
			// Token: 0x0600C5E5 RID: 50661 RVA: 0x00411232 File Offset: 0x0040F432
			public Context(StateMachine.Parameter parameter, Tag default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5E6 RID: 50662 RVA: 0x0041123C File Offset: 0x0040F43C
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value.GetHash());
			}

			// Token: 0x0600C5E7 RID: 50663 RVA: 0x0041124F File Offset: 0x0040F44F
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value = new Tag(reader.ReadInt32());
			}

			// Token: 0x0600C5E8 RID: 50664 RVA: 0x00411262 File Offset: 0x0040F462
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5E9 RID: 50665 RVA: 0x00411264 File Offset: 0x0040F464
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				ImGui.LabelText(this.parameter.name, this.value.ToString());
			}
		}
	}

	// Token: 0x0200136F RID: 4975
	public class AxialIParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<AxialI>
	{
		// Token: 0x06008A86 RID: 35462 RVA: 0x003504DD File Offset: 0x0034E6DD
		public AxialIParameter()
		{
		}

		// Token: 0x06008A87 RID: 35463 RVA: 0x003504E5 File Offset: 0x0034E6E5
		public AxialIParameter(AxialI default_value)
			: base(default_value)
		{
		}

		// Token: 0x06008A88 RID: 35464 RVA: 0x003504EE File Offset: 0x0034E6EE
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.AxialIParameter.Context(this, this.defaultValue);
		}

		// Token: 0x02002728 RID: 10024
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<AxialI>.Context
		{
			// Token: 0x0600C5EA RID: 50666 RVA: 0x00411287 File Offset: 0x0040F487
			public Context(StateMachine.Parameter parameter, AxialI default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5EB RID: 50667 RVA: 0x00411291 File Offset: 0x0040F491
			public override void Serialize(BinaryWriter writer)
			{
				writer.Write(this.value.r);
				writer.Write(this.value.q);
			}

			// Token: 0x0600C5EC RID: 50668 RVA: 0x004112B5 File Offset: 0x0040F4B5
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				this.value.r = reader.ReadInt32();
				this.value.q = reader.ReadInt32();
			}

			// Token: 0x0600C5ED RID: 50669 RVA: 0x004112D9 File Offset: 0x0040F4D9
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5EE RID: 50670 RVA: 0x004112DB File Offset: 0x0040F4DB
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				ImGui.LabelText(this.parameter.name, this.value.ToString());
			}
		}
	}

	// Token: 0x02001370 RID: 4976
	public class ObjectParameter<ObjectType> : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ObjectType> where ObjectType : class
	{
		// Token: 0x06008A89 RID: 35465 RVA: 0x003504FC File Offset: 0x0034E6FC
		public ObjectParameter()
			: base(default(ObjectType))
		{
		}

		// Token: 0x06008A8A RID: 35466 RVA: 0x00350518 File Offset: 0x0034E718
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.ObjectParameter<ObjectType>.Context(this, this.defaultValue);
		}

		// Token: 0x02002729 RID: 10025
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<ObjectType>.Context
		{
			// Token: 0x0600C5EF RID: 50671 RVA: 0x004112FE File Offset: 0x0040F4FE
			public Context(StateMachine.Parameter parameter, ObjectType default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5F0 RID: 50672 RVA: 0x00411308 File Offset: 0x0040F508
			public override void Serialize(BinaryWriter writer)
			{
				DebugUtil.DevLogError("ObjectParameter cannot be serialized");
			}

			// Token: 0x0600C5F1 RID: 50673 RVA: 0x00411314 File Offset: 0x0040F514
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				DebugUtil.DevLogError("ObjectParameter cannot be serialized");
			}

			// Token: 0x0600C5F2 RID: 50674 RVA: 0x00411320 File Offset: 0x0040F520
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5F3 RID: 50675 RVA: 0x00411324 File Offset: 0x0040F524
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				string text = "None";
				if (this.value != null)
				{
					text = this.value.ToString();
				}
				ImGui.LabelText(this.parameter.name, text);
			}
		}
	}

	// Token: 0x02001371 RID: 4977
	public class TargetParameter : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<GameObject>
	{
		// Token: 0x06008A8B RID: 35467 RVA: 0x00350526 File Offset: 0x0034E726
		public TargetParameter()
			: base(null)
		{
		}

		// Token: 0x06008A8C RID: 35468 RVA: 0x00350530 File Offset: 0x0034E730
		public SMT GetSMI<SMT>(StateMachineInstanceType smi) where SMT : StateMachine.Instance
		{
			GameObject gameObject = base.Get(smi);
			if (gameObject != null)
			{
				SMT smi2 = gameObject.GetSMI<SMT>();
				if (smi2 != null)
				{
					return smi2;
				}
				global::Debug.LogError(gameObject.name + " does not have state machine " + typeof(StateMachineType).Name);
			}
			return default(SMT);
		}

		// Token: 0x06008A8D RID: 35469 RVA: 0x0035058C File Offset: 0x0034E78C
		public bool IsNull(StateMachineInstanceType smi)
		{
			return base.Get(smi) == null;
		}

		// Token: 0x06008A8E RID: 35470 RVA: 0x0035059C File Offset: 0x0034E79C
		public ComponentType Get<ComponentType>(StateMachineInstanceType smi)
		{
			GameObject gameObject = base.Get(smi);
			if (gameObject != null)
			{
				ComponentType component = gameObject.GetComponent<ComponentType>();
				if (component != null)
				{
					return component;
				}
				global::Debug.LogError(gameObject.name + " does not have component " + typeof(ComponentType).Name);
			}
			return default(ComponentType);
		}

		// Token: 0x06008A8F RID: 35471 RVA: 0x003505F8 File Offset: 0x0034E7F8
		public ComponentType AddOrGet<ComponentType>(StateMachineInstanceType smi) where ComponentType : Component
		{
			GameObject gameObject = base.Get(smi);
			if (gameObject != null)
			{
				ComponentType componentType = gameObject.GetComponent<ComponentType>();
				if (componentType == null)
				{
					componentType = gameObject.AddComponent<ComponentType>();
				}
				return componentType;
			}
			return default(ComponentType);
		}

		// Token: 0x06008A90 RID: 35472 RVA: 0x00350640 File Offset: 0x0034E840
		public void Set(KMonoBehaviour value, StateMachineInstanceType smi)
		{
			GameObject gameObject = null;
			if (value != null)
			{
				gameObject = value.gameObject;
			}
			base.Set(gameObject, smi, false);
		}

		// Token: 0x06008A91 RID: 35473 RVA: 0x00350669 File Offset: 0x0034E869
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.TargetParameter.Context(this, this.defaultValue);
		}

		// Token: 0x0200272A RID: 10026
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<GameObject>.Context
		{
			// Token: 0x0600C5F4 RID: 50676 RVA: 0x00411366 File Offset: 0x0040F566
			public Context(StateMachine.Parameter parameter, GameObject default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5F5 RID: 50677 RVA: 0x00411370 File Offset: 0x0040F570
			public override void Serialize(BinaryWriter writer)
			{
				if (this.value != null)
				{
					int instanceID = this.value.GetComponent<KPrefabID>().InstanceID;
					writer.Write(instanceID);
					return;
				}
				writer.Write(0);
			}

			// Token: 0x0600C5F6 RID: 50678 RVA: 0x004113AC File Offset: 0x0040F5AC
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
				try
				{
					int num = reader.ReadInt32();
					if (num != 0)
					{
						KPrefabID instance = KPrefabIDTracker.Get().GetInstance(num);
						if (instance != null)
						{
							this.value = instance.gameObject;
							this.objectDestroyedHandler = instance.Subscribe(1969584890, new Action<object>(this.OnObjectDestroyed));
						}
						this.m_smi = (StateMachineInstanceType)((object)smi);
					}
				}
				catch (Exception ex)
				{
					if (!SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 20))
					{
						global::Debug.LogWarning("Missing statemachine target params. " + ex.Message);
					}
				}
			}

			// Token: 0x0600C5F7 RID: 50679 RVA: 0x00411450 File Offset: 0x0040F650
			public override void Cleanup()
			{
				base.Cleanup();
				if (this.value != null)
				{
					this.value.GetComponent<KMonoBehaviour>().Unsubscribe(this.objectDestroyedHandler);
					this.objectDestroyedHandler = 0;
				}
			}

			// Token: 0x0600C5F8 RID: 50680 RVA: 0x00411484 File Offset: 0x0040F684
			public override void Set(GameObject value, StateMachineInstanceType smi, bool silenceEvents = false)
			{
				this.m_smi = smi;
				if (this.value != null)
				{
					this.value.GetComponent<KMonoBehaviour>().Unsubscribe(this.objectDestroyedHandler);
					this.objectDestroyedHandler = 0;
				}
				if (value != null)
				{
					this.objectDestroyedHandler = value.GetComponent<KMonoBehaviour>().Subscribe(1969584890, new Action<object>(this.OnObjectDestroyed));
				}
				base.Set(value, smi, silenceEvents);
			}

			// Token: 0x0600C5F9 RID: 50681 RVA: 0x004114F7 File Offset: 0x0040F6F7
			private void OnObjectDestroyed(object data)
			{
				this.Set(null, this.m_smi, false);
			}

			// Token: 0x0600C5FA RID: 50682 RVA: 0x00411507 File Offset: 0x0040F707
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C5FB RID: 50683 RVA: 0x0041150C File Offset: 0x0040F70C
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				if (this.value != null)
				{
					ImGui.LabelText(this.parameter.name, this.value.name);
					return;
				}
				ImGui.LabelText(this.parameter.name, "null");
			}

			// Token: 0x0400AD08 RID: 44296
			private StateMachineInstanceType m_smi;

			// Token: 0x0400AD09 RID: 44297
			private int objectDestroyedHandler;
		}
	}

	// Token: 0x02001372 RID: 4978
	public class SignalParameter
	{
	}

	// Token: 0x02001373 RID: 4979
	public class Signal : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter>
	{
		// Token: 0x06008A93 RID: 35475 RVA: 0x0035067F File Offset: 0x0034E87F
		public Signal()
			: base(null)
		{
			this.isSignal = true;
		}

		// Token: 0x06008A94 RID: 35476 RVA: 0x0035068F File Offset: 0x0034E88F
		public void Trigger(StateMachineInstanceType smi)
		{
			((StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Signal.Context)smi.GetParameterContext(this)).Set(null, smi, false);
		}

		// Token: 0x06008A95 RID: 35477 RVA: 0x003506AA File Offset: 0x0034E8AA
		public override StateMachine.Parameter.Context CreateContext()
		{
			return new StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Signal.Context(this, this.defaultValue);
		}

		// Token: 0x0200272B RID: 10027
		public new class Context : StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.Parameter<StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter>.Context
		{
			// Token: 0x0600C5FC RID: 50684 RVA: 0x00411558 File Offset: 0x0040F758
			public Context(StateMachine.Parameter parameter, StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter default_value)
				: base(parameter, default_value)
			{
			}

			// Token: 0x0600C5FD RID: 50685 RVA: 0x00411562 File Offset: 0x0040F762
			public override void Serialize(BinaryWriter writer)
			{
			}

			// Token: 0x0600C5FE RID: 50686 RVA: 0x00411564 File Offset: 0x0040F764
			public override void Deserialize(IReader reader, StateMachine.Instance smi)
			{
			}

			// Token: 0x0600C5FF RID: 50687 RVA: 0x00411566 File Offset: 0x0040F766
			public override void Set(StateMachine<StateMachineType, StateMachineInstanceType, MasterType, DefType>.SignalParameter value, StateMachineInstanceType smi, bool silenceEvents = false)
			{
				if (!silenceEvents && this.onDirty != null)
				{
					this.onDirty(smi);
				}
			}

			// Token: 0x0600C600 RID: 50688 RVA: 0x0041157F File Offset: 0x0040F77F
			public override void ShowEditor(StateMachine.Instance base_smi)
			{
			}

			// Token: 0x0600C601 RID: 50689 RVA: 0x00411584 File Offset: 0x0040F784
			public override void ShowDevTool(StateMachine.Instance base_smi)
			{
				if (ImGui.Button(this.parameter.name))
				{
					StateMachineInstanceType stateMachineInstanceType = (StateMachineInstanceType)((object)base_smi);
					this.Set(null, stateMachineInstanceType, false);
				}
			}
		}
	}
}
