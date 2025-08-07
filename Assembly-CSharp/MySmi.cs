using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x0200050D RID: 1293
public class MySmi : MyAttributeManager<StateMachine.Instance>
{
	// Token: 0x06001B99 RID: 7065 RVA: 0x00096F60 File Offset: 0x00095160
	public static void Init()
	{
		MyAttributes.Register(new MySmi(new Dictionary<Type, MethodInfo>
		{
			{
				typeof(MySmiGet),
				typeof(MySmi).GetMethod("FindSmi")
			},
			{
				typeof(MySmiReq),
				typeof(MySmi).GetMethod("RequireSmi")
			}
		}));
	}

	// Token: 0x06001B9A RID: 7066 RVA: 0x00096FC4 File Offset: 0x000951C4
	public MySmi(Dictionary<Type, MethodInfo> attributeMap)
		: base(attributeMap, null)
	{
	}

	// Token: 0x06001B9B RID: 7067 RVA: 0x00096FD0 File Offset: 0x000951D0
	public static StateMachine.Instance FindSmi<T>(KMonoBehaviour c, bool isStart) where T : StateMachine.Instance
	{
		StateMachineController component = c.GetComponent<StateMachineController>();
		if (component != null)
		{
			return component.GetSMI<T>();
		}
		return null;
	}

	// Token: 0x06001B9C RID: 7068 RVA: 0x00096FFC File Offset: 0x000951FC
	public static StateMachine.Instance RequireSmi<T>(KMonoBehaviour c, bool isStart) where T : StateMachine.Instance
	{
		if (isStart)
		{
			StateMachine.Instance instance = MySmi.FindSmi<T>(c, isStart);
			Debug.Assert(instance != null, string.Format("{0} '{1}' requires a StateMachineInstance of type {2}!", c.GetType().ToString(), c.name, typeof(T)));
			return instance;
		}
		return MySmi.FindSmi<T>(c, isStart);
	}
}
