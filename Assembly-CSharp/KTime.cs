using System;
using UnityEngine;

// Token: 0x020009A1 RID: 2465
[AddComponentMenu("KMonoBehaviour/scripts/KTime")]
public class KTime : KMonoBehaviour
{
	// Token: 0x17000500 RID: 1280
	// (get) Token: 0x0600478E RID: 18318 RVA: 0x0019C65B File Offset: 0x0019A85B
	// (set) Token: 0x0600478F RID: 18319 RVA: 0x0019C663 File Offset: 0x0019A863
	public float UnscaledGameTime { get; set; }

	// Token: 0x17000501 RID: 1281
	// (get) Token: 0x06004790 RID: 18320 RVA: 0x0019C66C File Offset: 0x0019A86C
	// (set) Token: 0x06004791 RID: 18321 RVA: 0x0019C673 File Offset: 0x0019A873
	public static KTime Instance { get; private set; }

	// Token: 0x06004792 RID: 18322 RVA: 0x0019C67B File Offset: 0x0019A87B
	public static void DestroyInstance()
	{
		KTime.Instance = null;
	}

	// Token: 0x06004793 RID: 18323 RVA: 0x0019C683 File Offset: 0x0019A883
	protected override void OnPrefabInit()
	{
		KTime.Instance = this;
		this.UnscaledGameTime = Time.unscaledTime;
	}

	// Token: 0x06004794 RID: 18324 RVA: 0x0019C696 File Offset: 0x0019A896
	protected override void OnCleanUp()
	{
		KTime.Instance = null;
	}

	// Token: 0x06004795 RID: 18325 RVA: 0x0019C69E File Offset: 0x0019A89E
	public void Update()
	{
		if (!SpeedControlScreen.Instance.IsPaused)
		{
			this.UnscaledGameTime += Time.unscaledDeltaTime;
		}
	}
}
