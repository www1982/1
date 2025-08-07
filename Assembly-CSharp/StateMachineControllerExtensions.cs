using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000515 RID: 1301
public static class StateMachineControllerExtensions
{
	// Token: 0x06001BDE RID: 7134 RVA: 0x00097C18 File Offset: 0x00095E18
	public static StateMachineInstanceType GetSMI<StateMachineInstanceType>(this StateMachine.Instance smi) where StateMachineInstanceType : StateMachine.Instance
	{
		return smi.gameObject.GetSMI<StateMachineInstanceType>();
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x00097C25 File Offset: 0x00095E25
	public static DefType GetDef<DefType>(this Component cmp) where DefType : StateMachine.BaseDef
	{
		return cmp.gameObject.GetDef<DefType>();
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x00097C34 File Offset: 0x00095E34
	public static DefType GetDef<DefType>(this GameObject go) where DefType : StateMachine.BaseDef
	{
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component == null)
		{
			return default(DefType);
		}
		return component.GetDef<DefType>();
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x00097C64 File Offset: 0x00095E64
	public static InterfaceType GetDefImplementingInterface<InterfaceType>(this GameObject go) where InterfaceType : class
	{
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component == null)
		{
			return default(InterfaceType);
		}
		return component.GetDefImplementingInterfaceOfType<InterfaceType>();
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x00097C91 File Offset: 0x00095E91
	public static StateMachineInstanceType GetSMI<StateMachineInstanceType>(this Component cmp) where StateMachineInstanceType : class
	{
		return cmp.gameObject.GetSMI<StateMachineInstanceType>();
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x00097CA0 File Offset: 0x00095EA0
	public static StateMachineInstanceType GetSMI<StateMachineInstanceType>(this GameObject go) where StateMachineInstanceType : class
	{
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			return component.GetSMI<StateMachineInstanceType>();
		}
		return default(StateMachineInstanceType);
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x00097CCD File Offset: 0x00095ECD
	public static List<StateMachineInstanceType> GetAllSMI<StateMachineInstanceType>(this Component cmp) where StateMachineInstanceType : class
	{
		return cmp.gameObject.GetAllSMI<StateMachineInstanceType>();
	}

	// Token: 0x06001BE5 RID: 7141 RVA: 0x00097CDC File Offset: 0x00095EDC
	public static List<StateMachineInstanceType> GetAllSMI<StateMachineInstanceType>(this GameObject go) where StateMachineInstanceType : class
	{
		StateMachineController component = go.GetComponent<StateMachineController>();
		if (component != null)
		{
			return component.GetAllSMI<StateMachineInstanceType>();
		}
		return new List<StateMachineInstanceType>();
	}
}
