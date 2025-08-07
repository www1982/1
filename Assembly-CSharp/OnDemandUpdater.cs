using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A44 RID: 2628
public class OnDemandUpdater : MonoBehaviour
{
	// Token: 0x06004C44 RID: 19524 RVA: 0x001BA7B3 File Offset: 0x001B89B3
	public static void DestroyInstance()
	{
		OnDemandUpdater.Instance = null;
	}

	// Token: 0x06004C45 RID: 19525 RVA: 0x001BA7BB File Offset: 0x001B89BB
	private void Awake()
	{
		OnDemandUpdater.Instance = this;
	}

	// Token: 0x06004C46 RID: 19526 RVA: 0x001BA7C3 File Offset: 0x001B89C3
	public void Register(IUpdateOnDemand updater)
	{
		if (!this.Updaters.Contains(updater))
		{
			this.Updaters.Add(updater);
		}
	}

	// Token: 0x06004C47 RID: 19527 RVA: 0x001BA7DF File Offset: 0x001B89DF
	public void Unregister(IUpdateOnDemand updater)
	{
		if (this.Updaters.Contains(updater))
		{
			this.Updaters.Remove(updater);
		}
	}

	// Token: 0x06004C48 RID: 19528 RVA: 0x001BA7FC File Offset: 0x001B89FC
	private void Update()
	{
		for (int i = 0; i < this.Updaters.Count; i++)
		{
			if (this.Updaters[i] != null)
			{
				this.Updaters[i].UpdateOnDemand();
			}
		}
	}

	// Token: 0x0400328C RID: 12940
	private List<IUpdateOnDemand> Updaters = new List<IUpdateOnDemand>();

	// Token: 0x0400328D RID: 12941
	public static OnDemandUpdater Instance;
}
