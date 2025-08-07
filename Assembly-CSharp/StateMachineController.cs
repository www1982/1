using System;
using System.Collections.Generic;
using System.IO;
using KSerialization;
using UnityEngine;

// Token: 0x02000514 RID: 1300
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/StateMachineController")]
public class StateMachineController : KMonoBehaviour, ISaveLoadableDetails, IStateMachineControllerHack
{
	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x0009761E File Offset: 0x0009581E
	public StateMachineController.CmpDef cmpdef
	{
		get
		{
			return this.defHandle.Get<StateMachineController.CmpDef>();
		}
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x0009762B File Offset: 0x0009582B
	public IEnumerator<StateMachine.Instance> GetEnumerator()
	{
		return this.stateMachines.GetEnumerator();
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x0009763D File Offset: 0x0009583D
	public void AddStateMachineInstance(StateMachine.Instance state_machine)
	{
		if (!this.stateMachines.Contains(state_machine))
		{
			this.stateMachines.Add(state_machine);
			MyAttributes.OnAwake(state_machine, this);
		}
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x00097660 File Offset: 0x00095860
	public void RemoveStateMachineInstance(StateMachine.Instance state_machine)
	{
		if (!state_machine.GetStateMachine().saveHistory && !state_machine.GetStateMachine().debugSettings.saveHistory)
		{
			this.stateMachines.Remove(state_machine);
		}
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x0009768E File Offset: 0x0009588E
	public bool HasStateMachineInstance(StateMachine.Instance state_machine)
	{
		return this.stateMachines.Contains(state_machine);
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x0009769C File Offset: 0x0009589C
	public void AddDef(StateMachine.BaseDef def)
	{
		this.cmpdef.defs.Add(def);
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x000976AF File Offset: 0x000958AF
	public LoggerFSSSS GetLog()
	{
		return this.log;
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x000976B7 File Offset: 0x000958B7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.log.SetName(base.name);
		base.Subscribe<StateMachineController>(1969584890, StateMachineController.OnTargetDestroyedDelegate);
		base.Subscribe<StateMachineController>(1502190696, StateMachineController.OnTargetDestroyedDelegate);
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x000976F4 File Offset: 0x000958F4
	private void OnTargetDestroyed(object data)
	{
		while (this.stateMachines.Count > 0)
		{
			StateMachine.Instance instance = this.stateMachines[0];
			instance.StopSM("StateMachineController.OnCleanUp");
			this.stateMachines.Remove(instance);
		}
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x00097738 File Offset: 0x00095938
	protected override void OnLoadLevel()
	{
		while (this.stateMachines.Count > 0)
		{
			StateMachine.Instance instance = this.stateMachines[0];
			instance.FreeResources();
			this.stateMachines.Remove(instance);
		}
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x00097778 File Offset: 0x00095978
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		while (this.stateMachines.Count > 0)
		{
			StateMachine.Instance instance = this.stateMachines[0];
			instance.StopSM("StateMachineController.OnCleanUp");
			this.stateMachines.Remove(instance);
		}
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000977C0 File Offset: 0x000959C0
	public void CreateSMIS()
	{
		if (!this.defHandle.IsValid())
		{
			return;
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			baseDef.CreateSMI(this);
		}
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x00097828 File Offset: 0x00095A28
	public void StartSMIS()
	{
		if (!this.defHandle.IsValid())
		{
			return;
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			if (!baseDef.preventStartSMIOnSpawn)
			{
				StateMachine.Instance smi = this.GetSMI(Singleton<StateMachineManager>.Instance.CreateStateMachine(baseDef.GetStateMachineType()).GetStateMachineInstanceType());
				if (smi != null && !smi.IsRunning())
				{
					smi.StartSM();
				}
			}
		}
	}

	// Token: 0x06001BD2 RID: 7122 RVA: 0x000978BC File Offset: 0x00095ABC
	public void Serialize(BinaryWriter writer)
	{
		this.serializer.Serialize(this.stateMachines, writer);
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x000978D0 File Offset: 0x00095AD0
	public void Deserialize(IReader reader)
	{
		this.serializer.Deserialize(reader);
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000978DE File Offset: 0x00095ADE
	public bool Restore(StateMachine.Instance smi)
	{
		return this.serializer.Restore(smi);
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x000978EC File Offset: 0x00095AEC
	public DefType GetDef<DefType>() where DefType : StateMachine.BaseDef
	{
		if (!this.defHandle.IsValid())
		{
			return default(DefType);
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			DefType defType = baseDef as DefType;
			if (defType != null)
			{
				return defType;
			}
		}
		return default(DefType);
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x00097978 File Offset: 0x00095B78
	public InterfaceType GetDefImplementingInterfaceOfType<InterfaceType>() where InterfaceType : class
	{
		if (!this.defHandle.IsValid())
		{
			return default(InterfaceType);
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			InterfaceType interfaceType = baseDef as InterfaceType;
			if (interfaceType != null)
			{
				return interfaceType;
			}
		}
		return default(InterfaceType);
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x00097A04 File Offset: 0x00095C04
	public List<DefType> GetDefs<DefType>() where DefType : StateMachine.BaseDef
	{
		List<DefType> list = new List<DefType>();
		if (!this.defHandle.IsValid())
		{
			return list;
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			DefType defType = baseDef as DefType;
			if (defType != null)
			{
				list.Add(defType);
			}
		}
		return list;
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x00097A84 File Offset: 0x00095C84
	public StateMachine.Instance GetSMI(Type type)
	{
		for (int i = 0; i < this.stateMachines.Count; i++)
		{
			StateMachine.Instance instance = this.stateMachines[i];
			if (type.IsAssignableFrom(instance.GetType()))
			{
				return instance;
			}
		}
		return null;
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x00097AC5 File Offset: 0x00095CC5
	public StateMachineInstanceType GetSMI<StateMachineInstanceType>() where StateMachineInstanceType : class
	{
		return this.GetSMI(typeof(StateMachineInstanceType)) as StateMachineInstanceType;
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x00097AE4 File Offset: 0x00095CE4
	public List<StateMachineInstanceType> GetAllSMI<StateMachineInstanceType>() where StateMachineInstanceType : class
	{
		List<StateMachineInstanceType> list = new List<StateMachineInstanceType>();
		foreach (StateMachine.Instance instance in this.stateMachines)
		{
			StateMachineInstanceType stateMachineInstanceType = instance as StateMachineInstanceType;
			if (stateMachineInstanceType != null)
			{
				list.Add(stateMachineInstanceType);
			}
		}
		return list;
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x00097B50 File Offset: 0x00095D50
	public List<IGameObjectEffectDescriptor> GetDescriptors()
	{
		List<IGameObjectEffectDescriptor> list = new List<IGameObjectEffectDescriptor>();
		if (!this.defHandle.IsValid())
		{
			return list;
		}
		foreach (StateMachine.BaseDef baseDef in this.cmpdef.defs)
		{
			if (baseDef is IGameObjectEffectDescriptor)
			{
				list.Add(baseDef as IGameObjectEffectDescriptor);
			}
		}
		return list;
	}

	// Token: 0x0400105C RID: 4188
	public DefHandle defHandle;

	// Token: 0x0400105D RID: 4189
	private List<StateMachine.Instance> stateMachines = new List<StateMachine.Instance>();

	// Token: 0x0400105E RID: 4190
	private LoggerFSSSS log = new LoggerFSSSS("StateMachineController", 35);

	// Token: 0x0400105F RID: 4191
	private StateMachineSerializer serializer = new StateMachineSerializer();

	// Token: 0x04001060 RID: 4192
	private static readonly EventSystem.IntraObjectHandler<StateMachineController> OnTargetDestroyedDelegate = new EventSystem.IntraObjectHandler<StateMachineController>(delegate(StateMachineController component, object data)
	{
		component.OnTargetDestroyed(data);
	});

	// Token: 0x02001374 RID: 4980
	public class CmpDef
	{
		// Token: 0x0400695D RID: 26973
		public List<StateMachine.BaseDef> defs = new List<StateMachine.BaseDef>();
	}
}
