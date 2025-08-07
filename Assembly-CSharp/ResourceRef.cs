using System;
using System.Runtime.Serialization;
using KSerialization;

// Token: 0x02000ACD RID: 2765
[SerializationConfig(MemberSerialization.OptIn)]
public class ResourceRef<ResourceType> : ISaveLoadable where ResourceType : Resource
{
	// Token: 0x0600505A RID: 20570 RVA: 0x001D1228 File Offset: 0x001CF428
	public ResourceRef(ResourceType resource)
	{
		this.Set(resource);
	}

	// Token: 0x0600505B RID: 20571 RVA: 0x001D1237 File Offset: 0x001CF437
	public ResourceRef()
	{
	}

	// Token: 0x1700059E RID: 1438
	// (get) Token: 0x0600505C RID: 20572 RVA: 0x001D123F File Offset: 0x001CF43F
	public ResourceGuid Guid
	{
		get
		{
			return this.guid;
		}
	}

	// Token: 0x0600505D RID: 20573 RVA: 0x001D1247 File Offset: 0x001CF447
	public ResourceType Get()
	{
		return this.resource;
	}

	// Token: 0x0600505E RID: 20574 RVA: 0x001D124F File Offset: 0x001CF44F
	public void Set(ResourceType resource)
	{
		this.guid = null;
		this.resource = resource;
	}

	// Token: 0x0600505F RID: 20575 RVA: 0x001D125F File Offset: 0x001CF45F
	[OnSerializing]
	private void OnSerializing()
	{
		if (this.resource == null)
		{
			this.guid = null;
			return;
		}
		this.guid = this.resource.Guid;
	}

	// Token: 0x06005060 RID: 20576 RVA: 0x001D128C File Offset: 0x001CF48C
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.guid != null)
		{
			this.resource = Db.Get().GetResource<ResourceType>(this.guid);
			if (this.resource != null)
			{
				this.guid = null;
			}
		}
	}

	// Token: 0x04003613 RID: 13843
	[Serialize]
	private ResourceGuid guid;

	// Token: 0x04003614 RID: 13844
	private ResourceType resource;
}
