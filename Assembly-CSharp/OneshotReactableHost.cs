using System;
using UnityEngine;

// Token: 0x02000A45 RID: 2629
[AddComponentMenu("KMonoBehaviour/scripts/OneshotReactableHost")]
public class OneshotReactableHost : KMonoBehaviour
{
	// Token: 0x06004C4A RID: 19530 RVA: 0x001BA851 File Offset: 0x001B8A51
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("CleanupOneshotReactable", this.lifetime, new Action<object>(this.OnExpire), null, null);
	}

	// Token: 0x06004C4B RID: 19531 RVA: 0x001BA87D File Offset: 0x001B8A7D
	public void SetReactable(Reactable reactable)
	{
		this.reactable = reactable;
	}

	// Token: 0x06004C4C RID: 19532 RVA: 0x001BA888 File Offset: 0x001B8A88
	private void OnExpire(object obj)
	{
		if (!this.reactable.IsReacting)
		{
			this.reactable.Cleanup();
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		GameScheduler.Instance.Schedule("CleanupOneshotReactable", 0.5f, new Action<object>(this.OnExpire), null, null);
	}

	// Token: 0x0400328E RID: 12942
	private Reactable reactable;

	// Token: 0x0400328F RID: 12943
	public float lifetime = 1f;
}
