using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000D55 RID: 3413
[SerializationConfig(MemberSerialization.OptIn)]
public class MessageTarget : ISaveLoadable
{
	// Token: 0x060069D9 RID: 27097 RVA: 0x0027FF90 File Offset: 0x0027E190
	public MessageTarget(KPrefabID prefab_id)
	{
		this.prefabId.Set(prefab_id);
		this.position = prefab_id.transform.GetPosition();
		this.name = "Unknown";
		KSelectable component = prefab_id.GetComponent<KSelectable>();
		if (component != null)
		{
			this.name = component.GetName();
		}
		prefab_id.Subscribe(-1940207677, new Action<object>(this.OnAbsorbedBy));
	}

	// Token: 0x060069DA RID: 27098 RVA: 0x0028000A File Offset: 0x0027E20A
	public Vector3 GetPosition()
	{
		if (this.prefabId.Get() != null)
		{
			return this.prefabId.Get().transform.GetPosition();
		}
		return this.position;
	}

	// Token: 0x060069DB RID: 27099 RVA: 0x0028003B File Offset: 0x0027E23B
	public KSelectable GetSelectable()
	{
		if (this.prefabId.Get() != null)
		{
			return this.prefabId.Get().transform.GetComponent<KSelectable>();
		}
		return null;
	}

	// Token: 0x060069DC RID: 27100 RVA: 0x00280067 File Offset: 0x0027E267
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x060069DD RID: 27101 RVA: 0x00280070 File Offset: 0x0027E270
	private void OnAbsorbedBy(object data)
	{
		if (this.prefabId.Get() != null)
		{
			this.prefabId.Get().Unsubscribe(-1940207677, new Action<object>(this.OnAbsorbedBy));
		}
		KPrefabID component = ((GameObject)data).GetComponent<KPrefabID>();
		component.Subscribe(-1940207677, new Action<object>(this.OnAbsorbedBy));
		this.prefabId.Set(component);
	}

	// Token: 0x060069DE RID: 27102 RVA: 0x002800E4 File Offset: 0x0027E2E4
	public void OnCleanUp()
	{
		if (this.prefabId.Get() != null)
		{
			this.prefabId.Get().Unsubscribe(-1940207677, new Action<object>(this.OnAbsorbedBy));
			this.prefabId.Set(null);
		}
	}

	// Token: 0x0400484A RID: 18506
	[Serialize]
	private Ref<KPrefabID> prefabId = new Ref<KPrefabID>();

	// Token: 0x0400484B RID: 18507
	[Serialize]
	private Vector3 position;

	// Token: 0x0400484C RID: 18508
	[Serialize]
	private string name;
}
