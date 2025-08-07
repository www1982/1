using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using KSerialization;
using UnityEngine;

// Token: 0x0200050F RID: 1295
public abstract class StateMachine
{
	// Token: 0x06001BA2 RID: 7074 RVA: 0x00097083 File Offset: 0x00095283
	public StateMachine()
	{
		this.name = base.GetType().FullName;
	}

	// Token: 0x06001BA3 RID: 7075 RVA: 0x000970A8 File Offset: 0x000952A8
	public virtual void FreeResources()
	{
		this.name = null;
		if (this.defaultState != null)
		{
			this.defaultState.FreeResources();
		}
		this.defaultState = null;
		this.parameters = null;
	}

	// Token: 0x06001BA4 RID: 7076
	public abstract string[] GetStateNames();

	// Token: 0x06001BA5 RID: 7077
	public abstract StateMachine.BaseState GetState(string name);

	// Token: 0x06001BA6 RID: 7078
	public abstract void BindStates();

	// Token: 0x06001BA7 RID: 7079
	public abstract Type GetStateMachineInstanceType();

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000970D2 File Offset: 0x000952D2
	// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x000970DA File Offset: 0x000952DA
	public int version { get; protected set; }

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000970E3 File Offset: 0x000952E3
	// (set) Token: 0x06001BAB RID: 7083 RVA: 0x000970EB File Offset: 0x000952EB
	public StateMachine.SerializeType serializable { get; protected set; }

	// Token: 0x06001BAC RID: 7084 RVA: 0x000970F4 File Offset: 0x000952F4
	public virtual void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = null;
	}

	// Token: 0x06001BAD RID: 7085 RVA: 0x000970FC File Offset: 0x000952FC
	public void InitializeStateMachine()
	{
		this.debugSettings = StateMachineDebuggerSettings.Get().CreateEntry(base.GetType());
		StateMachine.BaseState baseState = null;
		this.InitializeStates(out baseState);
		DebugUtil.Assert(baseState != null);
		this.defaultState = baseState;
	}

	// Token: 0x06001BAE RID: 7086 RVA: 0x0009713C File Offset: 0x0009533C
	public void CreateStates(object state_machine)
	{
		foreach (FieldInfo fieldInfo in state_machine.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
		{
			bool flag = false;
			object[] customAttributes = fieldInfo.GetCustomAttributes(false);
			for (int j = 0; j < customAttributes.Length; j++)
			{
				if (customAttributes[j].GetType() == typeof(StateMachine.DoNotAutoCreate))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				if (fieldInfo.FieldType.IsSubclassOf(typeof(StateMachine.BaseState)))
				{
					StateMachine.BaseState baseState = (StateMachine.BaseState)Activator.CreateInstance(fieldInfo.FieldType);
					this.CreateStates(baseState);
					fieldInfo.SetValue(state_machine, baseState);
				}
				else if (fieldInfo.FieldType.IsSubclassOf(typeof(StateMachine.Parameter)))
				{
					StateMachine.Parameter parameter = (StateMachine.Parameter)fieldInfo.GetValue(state_machine);
					if (parameter == null)
					{
						parameter = (StateMachine.Parameter)Activator.CreateInstance(fieldInfo.FieldType);
						fieldInfo.SetValue(state_machine, parameter);
					}
					parameter.name = fieldInfo.Name;
					parameter.idx = this.parameters.Length;
					this.parameters = this.parameters.Append(parameter);
				}
				else if (fieldInfo.FieldType.IsSubclassOf(typeof(StateMachine)))
				{
					fieldInfo.SetValue(state_machine, this);
				}
			}
		}
	}

	// Token: 0x06001BAF RID: 7087 RVA: 0x00097285 File Offset: 0x00095485
	public StateMachine.BaseState GetDefaultState()
	{
		return this.defaultState;
	}

	// Token: 0x06001BB0 RID: 7088 RVA: 0x0009728D File Offset: 0x0009548D
	public int GetMaxDepth()
	{
		return this.maxDepth;
	}

	// Token: 0x06001BB1 RID: 7089 RVA: 0x00097295 File Offset: 0x00095495
	public override string ToString()
	{
		return this.name;
	}

	// Token: 0x0400104D RID: 4173
	protected string name;

	// Token: 0x0400104E RID: 4174
	protected int maxDepth;

	// Token: 0x0400104F RID: 4175
	protected StateMachine.BaseState defaultState;

	// Token: 0x04001050 RID: 4176
	protected StateMachine.Parameter[] parameters = new StateMachine.Parameter[0];

	// Token: 0x04001051 RID: 4177
	public int dataTableSize;

	// Token: 0x04001052 RID: 4178
	public int updateTableSize;

	// Token: 0x04001055 RID: 4181
	public StateMachineDebuggerSettings.Entry debugSettings;

	// Token: 0x04001056 RID: 4182
	public bool saveHistory;

	// Token: 0x02001356 RID: 4950
	public sealed class DoNotAutoCreate : Attribute
	{
	}

	// Token: 0x02001357 RID: 4951
	public enum Status
	{
		// Token: 0x0400691A RID: 26906
		Initialized,
		// Token: 0x0400691B RID: 26907
		Running,
		// Token: 0x0400691C RID: 26908
		Failed,
		// Token: 0x0400691D RID: 26909
		Success
	}

	// Token: 0x02001358 RID: 4952
	public class BaseDef
	{
		// Token: 0x06008A0E RID: 35342 RVA: 0x0034F08C File Offset: 0x0034D28C
		public StateMachine.Instance CreateSMI(IStateMachineTarget master)
		{
			return Singleton<StateMachineManager>.Instance.CreateSMIFromDef(master, this);
		}

		// Token: 0x06008A0F RID: 35343 RVA: 0x0034F09A File Offset: 0x0034D29A
		public Type GetStateMachineType()
		{
			return base.GetType().DeclaringType;
		}

		// Token: 0x06008A10 RID: 35344 RVA: 0x0034F0A7 File Offset: 0x0034D2A7
		public virtual void Configure(GameObject prefab)
		{
		}

		// Token: 0x0400691E RID: 26910
		public bool preventStartSMIOnSpawn;
	}

	// Token: 0x02001359 RID: 4953
	public class Category : Resource
	{
		// Token: 0x06008A12 RID: 35346 RVA: 0x0034F0B1 File Offset: 0x0034D2B1
		public Category(string id)
			: base(id, null, null)
		{
		}
	}

	// Token: 0x0200135A RID: 4954
	[SerializationConfig(MemberSerialization.OptIn)]
	public abstract class Instance
	{
		// Token: 0x06008A13 RID: 35347
		public abstract StateMachine.BaseState GetCurrentState();

		// Token: 0x06008A14 RID: 35348
		public abstract void GoTo(StateMachine.BaseState state);

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06008A15 RID: 35349
		public abstract float timeinstate { get; }

		// Token: 0x06008A16 RID: 35350
		public abstract IStateMachineTarget GetMaster();

		// Token: 0x06008A17 RID: 35351
		public abstract void StopSM(string reason);

		// Token: 0x06008A18 RID: 35352
		public abstract SchedulerHandle Schedule(float time, Action<object> callback, object callback_data = null);

		// Token: 0x06008A19 RID: 35353
		public abstract SchedulerHandle ScheduleNextFrame(Action<object> callback, object callback_data = null);

		// Token: 0x06008A1A RID: 35354 RVA: 0x0034F0BC File Offset: 0x0034D2BC
		public virtual void FreeResources()
		{
			this.stateMachine = null;
			if (this.subscribedEvents != null)
			{
				this.subscribedEvents.Clear();
			}
			this.subscribedEvents = null;
			this.parameterContexts = null;
			this.dataTable = null;
			this.updateTable = null;
		}

		// Token: 0x06008A1B RID: 35355 RVA: 0x0034F0F4 File Offset: 0x0034D2F4
		public Instance(StateMachine state_machine, IStateMachineTarget master)
		{
			this.stateMachine = state_machine;
			this.CreateParameterContexts();
			this.log = new LoggerFSSSS(this.stateMachine.name, 35);
		}

		// Token: 0x06008A1C RID: 35356 RVA: 0x0034F12C File Offset: 0x0034D32C
		public virtual void PostParamsInitialized()
		{
		}

		// Token: 0x06008A1D RID: 35357 RVA: 0x0034F12E File Offset: 0x0034D32E
		public bool IsRunning()
		{
			return this.GetCurrentState() != null;
		}

		// Token: 0x06008A1E RID: 35358 RVA: 0x0034F13C File Offset: 0x0034D33C
		public void GoTo(string state_name)
		{
			DebugUtil.DevAssert(!KMonoBehaviour.isLoadingScene, "Using Goto while scene was loaded", null);
			StateMachine.BaseState state = this.stateMachine.GetState(state_name);
			this.GoTo(state);
		}

		// Token: 0x06008A1F RID: 35359 RVA: 0x0034F170 File Offset: 0x0034D370
		public int GetStackSize()
		{
			return this.stackSize;
		}

		// Token: 0x06008A20 RID: 35360 RVA: 0x0034F178 File Offset: 0x0034D378
		public StateMachine GetStateMachine()
		{
			return this.stateMachine;
		}

		// Token: 0x06008A21 RID: 35361 RVA: 0x0034F180 File Offset: 0x0034D380
		[Conditional("UNITY_EDITOR")]
		public void Log(string a, string b = "", string c = "", string d = "")
		{
		}

		// Token: 0x06008A22 RID: 35362 RVA: 0x0034F182 File Offset: 0x0034D382
		public bool IsConsoleLoggingEnabled()
		{
			return this.enableConsoleLogging || this.stateMachine.debugSettings.enableConsoleLogging;
		}

		// Token: 0x06008A23 RID: 35363 RVA: 0x0034F19E File Offset: 0x0034D39E
		public bool IsBreakOnGoToEnabled()
		{
			return this.breakOnGoTo || this.stateMachine.debugSettings.breakOnGoTo;
		}

		// Token: 0x06008A24 RID: 35364 RVA: 0x0034F1BA File Offset: 0x0034D3BA
		public LoggerFSSSS GetLog()
		{
			return this.log;
		}

		// Token: 0x06008A25 RID: 35365 RVA: 0x0034F1C2 File Offset: 0x0034D3C2
		public StateMachine.Parameter.Context[] GetParameterContexts()
		{
			return this.parameterContexts;
		}

		// Token: 0x06008A26 RID: 35366 RVA: 0x0034F1CA File Offset: 0x0034D3CA
		public StateMachine.Parameter.Context GetParameterContext(StateMachine.Parameter parameter)
		{
			return this.parameterContexts[parameter.idx];
		}

		// Token: 0x06008A27 RID: 35367 RVA: 0x0034F1D9 File Offset: 0x0034D3D9
		public StateMachine.Status GetStatus()
		{
			return this.status;
		}

		// Token: 0x06008A28 RID: 35368 RVA: 0x0034F1E1 File Offset: 0x0034D3E1
		public void SetStatus(StateMachine.Status status)
		{
			this.status = status;
		}

		// Token: 0x06008A29 RID: 35369 RVA: 0x0034F1EA File Offset: 0x0034D3EA
		public void Error()
		{
			if (!StateMachine.Instance.error)
			{
				this.isCrashed = true;
				StateMachine.Instance.error = true;
				RestartWarning.ShouldWarn = true;
			}
		}

		// Token: 0x06008A2A RID: 35370 RVA: 0x0034F208 File Offset: 0x0034D408
		public override string ToString()
		{
			string text = "";
			if (this.GetCurrentState() != null)
			{
				text = this.GetCurrentState().name;
			}
			else if (this.GetStatus() != StateMachine.Status.Initialized)
			{
				text = this.GetStatus().ToString();
			}
			return this.stateMachine.ToString() + "(" + text + ")";
		}

		// Token: 0x06008A2B RID: 35371 RVA: 0x0034F26C File Offset: 0x0034D46C
		public virtual void StartSM()
		{
			if (!this.IsRunning())
			{
				StateMachineController component = this.GetComponent<StateMachineController>();
				MyAttributes.OnStart(this, component);
				StateMachine.BaseState defaultState = this.stateMachine.GetDefaultState();
				DebugUtil.Assert(defaultState != null);
				if (!component.Restore(this))
				{
					this.PostParamsInitialized();
					this.GoTo(defaultState);
				}
			}
		}

		// Token: 0x06008A2C RID: 35372 RVA: 0x0034F2BA File Offset: 0x0034D4BA
		public bool HasTag(Tag tag)
		{
			return this.GetComponent<KPrefabID>().HasTag(tag);
		}

		// Token: 0x06008A2D RID: 35373 RVA: 0x0034F2C8 File Offset: 0x0034D4C8
		public bool IsInsideState(StateMachine.BaseState state)
		{
			StateMachine.BaseState currentState = this.GetCurrentState();
			if (currentState == null)
			{
				return false;
			}
			bool flag = state == currentState;
			int num = 0;
			while (!flag && num < currentState.branch.Length && !(flag = state == currentState.branch[num]))
			{
				num++;
			}
			return flag;
		}

		// Token: 0x06008A2E RID: 35374 RVA: 0x0034F30C File Offset: 0x0034D50C
		public void ScheduleGoTo(float time, StateMachine.BaseState state)
		{
			if (this.scheduleGoToCallback == null)
			{
				this.scheduleGoToCallback = delegate(object d)
				{
					this.GoTo((StateMachine.BaseState)d);
				};
			}
			this.Schedule(time, this.scheduleGoToCallback, state);
		}

		// Token: 0x06008A2F RID: 35375 RVA: 0x0034F337 File Offset: 0x0034D537
		public void Subscribe(int hash, Action<object> handler)
		{
			this.GetMaster().Subscribe(hash, handler);
		}

		// Token: 0x06008A30 RID: 35376 RVA: 0x0034F347 File Offset: 0x0034D547
		public void Unsubscribe(int hash, Action<object> handler)
		{
			this.GetMaster().Unsubscribe(hash, handler);
		}

		// Token: 0x06008A31 RID: 35377 RVA: 0x0034F356 File Offset: 0x0034D556
		public void Trigger(int hash, object data = null)
		{
			this.GetMaster().GetComponent<KPrefabID>().Trigger(hash, data);
		}

		// Token: 0x06008A32 RID: 35378 RVA: 0x0034F36A File Offset: 0x0034D56A
		public ComponentType Get<ComponentType>()
		{
			return this.GetComponent<ComponentType>();
		}

		// Token: 0x06008A33 RID: 35379 RVA: 0x0034F372 File Offset: 0x0034D572
		public ComponentType GetComponent<ComponentType>()
		{
			return this.GetMaster().GetComponent<ComponentType>();
		}

		// Token: 0x06008A34 RID: 35380 RVA: 0x0034F380 File Offset: 0x0034D580
		private void CreateParameterContexts()
		{
			this.parameterContexts = new StateMachine.Parameter.Context[this.stateMachine.parameters.Length];
			for (int i = 0; i < this.stateMachine.parameters.Length; i++)
			{
				this.parameterContexts[i] = this.stateMachine.parameters[i].CreateContext();
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06008A35 RID: 35381 RVA: 0x0034F3D7 File Offset: 0x0034D5D7
		public GameObject gameObject
		{
			get
			{
				return this.GetMaster().gameObject;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06008A36 RID: 35382 RVA: 0x0034F3E4 File Offset: 0x0034D5E4
		public Transform transform
		{
			get
			{
				return this.gameObject.transform;
			}
		}

		// Token: 0x0400691F RID: 26911
		public string serializationSuffix;

		// Token: 0x04006920 RID: 26912
		protected LoggerFSSSS log;

		// Token: 0x04006921 RID: 26913
		protected StateMachine.Status status;

		// Token: 0x04006922 RID: 26914
		protected StateMachine stateMachine;

		// Token: 0x04006923 RID: 26915
		protected Stack<StateEvent.Context> subscribedEvents = new Stack<StateEvent.Context>();

		// Token: 0x04006924 RID: 26916
		protected int stackSize;

		// Token: 0x04006925 RID: 26917
		protected StateMachine.Parameter.Context[] parameterContexts;

		// Token: 0x04006926 RID: 26918
		public object[] dataTable;

		// Token: 0x04006927 RID: 26919
		public StateMachine.Instance.UpdateTableEntry[] updateTable;

		// Token: 0x04006928 RID: 26920
		private Action<object> scheduleGoToCallback;

		// Token: 0x04006929 RID: 26921
		public Action<string, StateMachine.Status> OnStop;

		// Token: 0x0400692A RID: 26922
		public bool breakOnGoTo;

		// Token: 0x0400692B RID: 26923
		public bool enableConsoleLogging;

		// Token: 0x0400692C RID: 26924
		public bool isCrashed;

		// Token: 0x0400692D RID: 26925
		public static bool error;

		// Token: 0x02002717 RID: 10007
		public struct UpdateTableEntry
		{
			// Token: 0x0400ACFD RID: 44285
			public HandleVector<int>.Handle handle;

			// Token: 0x0400ACFE RID: 44286
			public StateMachineUpdater.BaseUpdateBucket bucket;
		}
	}

	// Token: 0x0200135B RID: 4955
	[DebuggerDisplay("{longName}")]
	public class BaseState
	{
		// Token: 0x06008A38 RID: 35384 RVA: 0x0034F3FF File Offset: 0x0034D5FF
		public BaseState()
		{
			this.branch = new StateMachine.BaseState[1];
			this.branch[0] = this;
		}

		// Token: 0x06008A39 RID: 35385 RVA: 0x0034F41C File Offset: 0x0034D61C
		public void FreeResources()
		{
			if (this.name == null)
			{
				return;
			}
			this.name = null;
			if (this.defaultState != null)
			{
				this.defaultState.FreeResources();
			}
			this.defaultState = null;
			this.events = null;
			int num = 0;
			while (this.transitions != null && num < this.transitions.Count)
			{
				this.transitions[num].Clear();
				num++;
			}
			this.transitions = null;
			this.enterActions = null;
			this.exitActions = null;
			if (this.branch != null)
			{
				for (int i = 0; i < this.branch.Length; i++)
				{
					this.branch[i].FreeResources();
				}
			}
			this.branch = null;
			this.parent = null;
		}

		// Token: 0x06008A3A RID: 35386 RVA: 0x0034F4D4 File Offset: 0x0034D6D4
		public int GetStateCount()
		{
			return this.branch.Length;
		}

		// Token: 0x06008A3B RID: 35387 RVA: 0x0034F4DE File Offset: 0x0034D6DE
		public StateMachine.BaseState GetState(int idx)
		{
			return this.branch[idx];
		}

		// Token: 0x0400692E RID: 26926
		public string name;

		// Token: 0x0400692F RID: 26927
		public string longName;

		// Token: 0x04006930 RID: 26928
		public string debugPushName;

		// Token: 0x04006931 RID: 26929
		public string debugPopName;

		// Token: 0x04006932 RID: 26930
		public string debugExecuteName;

		// Token: 0x04006933 RID: 26931
		public StateMachine.BaseState defaultState;

		// Token: 0x04006934 RID: 26932
		public List<StateEvent> events;

		// Token: 0x04006935 RID: 26933
		public List<StateMachine.BaseTransition> transitions;

		// Token: 0x04006936 RID: 26934
		public List<StateMachine.UpdateAction> updateActions;

		// Token: 0x04006937 RID: 26935
		public List<StateMachine.Action> enterActions;

		// Token: 0x04006938 RID: 26936
		public List<StateMachine.Action> exitActions;

		// Token: 0x04006939 RID: 26937
		public StateMachine.BaseState[] branch;

		// Token: 0x0400693A RID: 26938
		public StateMachine.BaseState parent;
	}

	// Token: 0x0200135C RID: 4956
	public class BaseTransition
	{
		// Token: 0x06008A3C RID: 35388 RVA: 0x0034F4E8 File Offset: 0x0034D6E8
		public BaseTransition(int idx, string name, StateMachine.BaseState source_state, StateMachine.BaseState target_state)
		{
			this.idx = idx;
			this.name = name;
			this.sourceState = source_state;
			this.targetState = target_state;
		}

		// Token: 0x06008A3D RID: 35389 RVA: 0x0034F50D File Offset: 0x0034D70D
		public virtual void Evaluate(StateMachine.Instance smi)
		{
		}

		// Token: 0x06008A3E RID: 35390 RVA: 0x0034F50F File Offset: 0x0034D70F
		public virtual StateMachine.BaseTransition.Context Register(StateMachine.Instance smi)
		{
			return new StateMachine.BaseTransition.Context(this);
		}

		// Token: 0x06008A3F RID: 35391 RVA: 0x0034F517 File Offset: 0x0034D717
		public virtual void Unregister(StateMachine.Instance smi, StateMachine.BaseTransition.Context context)
		{
		}

		// Token: 0x06008A40 RID: 35392 RVA: 0x0034F519 File Offset: 0x0034D719
		public void Clear()
		{
			this.name = null;
			if (this.sourceState != null)
			{
				this.sourceState.FreeResources();
			}
			this.sourceState = null;
			if (this.targetState != null)
			{
				this.targetState.FreeResources();
			}
			this.targetState = null;
		}

		// Token: 0x0400693B RID: 26939
		public int idx;

		// Token: 0x0400693C RID: 26940
		public string name;

		// Token: 0x0400693D RID: 26941
		public StateMachine.BaseState sourceState;

		// Token: 0x0400693E RID: 26942
		public StateMachine.BaseState targetState;

		// Token: 0x02002718 RID: 10008
		public struct Context
		{
			// Token: 0x0600C5A7 RID: 50599 RVA: 0x00410C20 File Offset: 0x0040EE20
			public Context(StateMachine.BaseTransition transition)
			{
				this.idx = transition.idx;
				this.handlerId = 0;
			}

			// Token: 0x0400ACFF RID: 44287
			public int idx;

			// Token: 0x0400AD00 RID: 44288
			public int handlerId;
		}
	}

	// Token: 0x0200135D RID: 4957
	public struct UpdateAction
	{
		// Token: 0x0400693F RID: 26943
		public int updateTableIdx;

		// Token: 0x04006940 RID: 26944
		public UpdateRate updateRate;

		// Token: 0x04006941 RID: 26945
		public int nextBucketIdx;

		// Token: 0x04006942 RID: 26946
		public StateMachineUpdater.BaseUpdateBucket[] buckets;

		// Token: 0x04006943 RID: 26947
		public object updater;
	}

	// Token: 0x0200135E RID: 4958
	public struct Action
	{
		// Token: 0x06008A41 RID: 35393 RVA: 0x0034F556 File Offset: 0x0034D756
		public Action(string name, object callback)
		{
			this.name = name;
			this.callback = callback;
		}

		// Token: 0x04006944 RID: 26948
		public string name;

		// Token: 0x04006945 RID: 26949
		public object callback;
	}

	// Token: 0x0200135F RID: 4959
	public class ParameterTransition : StateMachine.BaseTransition
	{
		// Token: 0x06008A42 RID: 35394 RVA: 0x0034F566 File Offset: 0x0034D766
		public ParameterTransition(int idx, string name, StateMachine.BaseState source_state, StateMachine.BaseState target_state)
			: base(idx, name, source_state, target_state)
		{
		}
	}

	// Token: 0x02001360 RID: 4960
	public abstract class Parameter
	{
		// Token: 0x06008A43 RID: 35395
		public abstract StateMachine.Parameter.Context CreateContext();

		// Token: 0x04006946 RID: 26950
		public string name;

		// Token: 0x04006947 RID: 26951
		public int idx;

		// Token: 0x02002719 RID: 10009
		public abstract class Context
		{
			// Token: 0x0600C5A8 RID: 50600 RVA: 0x00410C35 File Offset: 0x0040EE35
			public Context(StateMachine.Parameter parameter)
			{
				this.parameter = parameter;
			}

			// Token: 0x0600C5A9 RID: 50601
			public abstract void Serialize(BinaryWriter writer);

			// Token: 0x0600C5AA RID: 50602
			public abstract void Deserialize(IReader reader, StateMachine.Instance smi);

			// Token: 0x0600C5AB RID: 50603 RVA: 0x00410C44 File Offset: 0x0040EE44
			public virtual void Cleanup()
			{
			}

			// Token: 0x0600C5AC RID: 50604
			public abstract void ShowEditor(StateMachine.Instance base_smi);

			// Token: 0x0600C5AD RID: 50605
			public abstract void ShowDevTool(StateMachine.Instance base_smi);

			// Token: 0x0400AD01 RID: 44289
			public StateMachine.Parameter parameter;
		}
	}

	// Token: 0x02001361 RID: 4961
	public enum SerializeType
	{
		// Token: 0x04006949 RID: 26953
		Never,
		// Token: 0x0400694A RID: 26954
		ParamsOnly,
		// Token: 0x0400694B RID: 26955
		CurrentStateOnly_DEPRECATED,
		// Token: 0x0400694C RID: 26956
		Both_DEPRECATED
	}
}
