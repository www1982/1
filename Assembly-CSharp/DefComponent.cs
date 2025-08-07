using System;
using UnityEngine;

// Token: 0x02000505 RID: 1285
[Serializable]
public class DefComponent<T> where T : Component
{
	// Token: 0x06001B84 RID: 7044 RVA: 0x00096BF0 File Offset: 0x00094DF0
	public DefComponent(T cmp)
	{
		this.cmp = cmp;
	}

	// Token: 0x06001B85 RID: 7045 RVA: 0x00096C00 File Offset: 0x00094E00
	public T Get(StateMachine.Instance smi)
	{
		T[] components = this.cmp.GetComponents<T>();
		int num = 0;
		while (num < components.Length && !(components[num] == this.cmp))
		{
			num++;
		}
		return smi.gameObject.GetComponents<T>()[num];
	}

	// Token: 0x06001B86 RID: 7046 RVA: 0x00096C5B File Offset: 0x00094E5B
	public static implicit operator DefComponent<T>(T cmp)
	{
		return new DefComponent<T>(cmp);
	}

	// Token: 0x04001036 RID: 4150
	[SerializeField]
	private T cmp;
}
