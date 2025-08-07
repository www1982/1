using System;
using UnityEngine;

// Token: 0x0200069E RID: 1694
[AddComponentMenu("KMonoBehaviour/scripts/AmbientSoundManager")]
public class AmbientSoundManager : KMonoBehaviour
{
	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x0600294C RID: 10572 RVA: 0x000F013D File Offset: 0x000EE33D
	// (set) Token: 0x0600294D RID: 10573 RVA: 0x000F0144 File Offset: 0x000EE344
	public static AmbientSoundManager Instance { get; private set; }

	// Token: 0x0600294E RID: 10574 RVA: 0x000F014C File Offset: 0x000EE34C
	public static void Destroy()
	{
		AmbientSoundManager.Instance = null;
	}

	// Token: 0x0600294F RID: 10575 RVA: 0x000F0154 File Offset: 0x000EE354
	protected override void OnPrefabInit()
	{
		AmbientSoundManager.Instance = this;
	}

	// Token: 0x06002950 RID: 10576 RVA: 0x000F015C File Offset: 0x000EE35C
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06002951 RID: 10577 RVA: 0x000F0164 File Offset: 0x000EE364
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		AmbientSoundManager.Instance = null;
	}

	// Token: 0x0400186A RID: 6250
	[MyCmpAdd]
	private LoopingSounds loopingSounds;
}
