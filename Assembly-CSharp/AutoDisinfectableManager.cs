using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000573 RID: 1395
[AddComponentMenu("KMonoBehaviour/scripts/AutoDisinfectableManager")]
public class AutoDisinfectableManager : KMonoBehaviour, ISim1000ms
{
	// Token: 0x06001F2A RID: 7978 RVA: 0x000B29CF File Offset: 0x000B0BCF
	public static void DestroyInstance()
	{
		AutoDisinfectableManager.Instance = null;
	}

	// Token: 0x06001F2B RID: 7979 RVA: 0x000B29D7 File Offset: 0x000B0BD7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		AutoDisinfectableManager.Instance = this;
	}

	// Token: 0x06001F2C RID: 7980 RVA: 0x000B29E5 File Offset: 0x000B0BE5
	public void AddAutoDisinfectable(AutoDisinfectable auto_disinfectable)
	{
		this.autoDisinfectables.Add(auto_disinfectable);
	}

	// Token: 0x06001F2D RID: 7981 RVA: 0x000B29F3 File Offset: 0x000B0BF3
	public void RemoveAutoDisinfectable(AutoDisinfectable auto_disinfectable)
	{
		auto_disinfectable.CancelChore();
		this.autoDisinfectables.Remove(auto_disinfectable);
	}

	// Token: 0x06001F2E RID: 7982 RVA: 0x000B2A08 File Offset: 0x000B0C08
	public void Sim1000ms(float dt)
	{
		for (int i = 0; i < this.autoDisinfectables.Count; i++)
		{
			this.autoDisinfectables[i].RefreshChore();
		}
	}

	// Token: 0x04001216 RID: 4630
	private List<AutoDisinfectable> autoDisinfectables = new List<AutoDisinfectable>();

	// Token: 0x04001217 RID: 4631
	public static AutoDisinfectableManager Instance;
}
