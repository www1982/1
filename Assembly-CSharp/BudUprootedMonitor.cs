using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000854 RID: 2132
[AddComponentMenu("KMonoBehaviour/scripts/BudUprootedMonitor")]
public class BudUprootedMonitor : KMonoBehaviour
{
	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x06003A8B RID: 14987 RVA: 0x00145A13 File Offset: 0x00143C13
	public bool IsUprooted
	{
		get
		{
			return this.uprooted || base.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted);
		}
	}

	// Token: 0x06003A8C RID: 14988 RVA: 0x00145A2F File Offset: 0x00143C2F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<BudUprootedMonitor>(-216549700, BudUprootedMonitor.OnUprootedDelegate);
	}

	// Token: 0x06003A8D RID: 14989 RVA: 0x00145A48 File Offset: 0x00143C48
	public void SetParentObject(KPrefabID id)
	{
		this.parentObject = new Ref<KPrefabID>(id);
		base.Subscribe(id.gameObject, 1969584890, new Action<object>(this.OnLoseParent));
	}

	// Token: 0x06003A8E RID: 14990 RVA: 0x00145A74 File Offset: 0x00143C74
	private void OnLoseParent(object obj)
	{
		if (!this.uprooted && !base.isNull)
		{
			base.GetComponent<KPrefabID>().AddTag(GameTags.Uprooted, false);
			this.uprooted = true;
			base.Trigger(-216549700, null);
			if (this.destroyOnParentLost)
			{
				Util.KDestroyGameObject(base.gameObject);
			}
		}
	}

	// Token: 0x06003A8F RID: 14991 RVA: 0x00145AC8 File Offset: 0x00143CC8
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06003A90 RID: 14992 RVA: 0x00145AD0 File Offset: 0x00143CD0
	public static bool IsObjectUprooted(GameObject plant)
	{
		BudUprootedMonitor component = plant.GetComponent<BudUprootedMonitor>();
		return !(component == null) && component.IsUprooted;
	}

	// Token: 0x040023E4 RID: 9188
	[Serialize]
	public bool canBeUprooted = true;

	// Token: 0x040023E5 RID: 9189
	[Serialize]
	private bool uprooted;

	// Token: 0x040023E6 RID: 9190
	public bool destroyOnParentLost;

	// Token: 0x040023E7 RID: 9191
	public Ref<KPrefabID> parentObject = new Ref<KPrefabID>();

	// Token: 0x040023E8 RID: 9192
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040023E9 RID: 9193
	private static readonly EventSystem.IntraObjectHandler<BudUprootedMonitor> OnUprootedDelegate = new EventSystem.IntraObjectHandler<BudUprootedMonitor>(delegate(BudUprootedMonitor component, object data)
	{
		if (!component.uprooted)
		{
			component.GetComponent<KPrefabID>().AddTag(GameTags.Uprooted, false);
			component.uprooted = true;
			component.Trigger(-216549700, null);
		}
	});
}
