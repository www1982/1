using System;
using UnityEngine;

// Token: 0x02000453 RID: 1107
public class GameObjectPool : ObjectPool<GameObject>
{
	// Token: 0x0600170B RID: 5899 RVA: 0x00081E29 File Offset: 0x00080029
	public GameObjectPool(Func<GameObject> instantiator, int initial_count = 0)
		: base(instantiator, initial_count)
	{
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x00081E33 File Offset: 0x00080033
	public override GameObject GetInstance()
	{
		return base.GetInstance();
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x00081E3C File Offset: 0x0008003C
	public void Destroy()
	{
		for (int i = this.unused.Count - 1; i >= 0; i--)
		{
			global::UnityEngine.Object.Destroy(this.unused.Pop());
		}
	}
}
