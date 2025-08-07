using System;
using UnityEngine;

// Token: 0x02000846 RID: 2118
public class ConversationType
{
	// Token: 0x06003A2D RID: 14893 RVA: 0x00143AC5 File Offset: 0x00141CC5
	public virtual void NewTarget(MinionIdentity speaker)
	{
	}

	// Token: 0x06003A2E RID: 14894 RVA: 0x00143AC7 File Offset: 0x00141CC7
	public virtual Conversation.Topic GetNextTopic(MinionIdentity speaker, Conversation.Topic lastTopic)
	{
		return null;
	}

	// Token: 0x06003A2F RID: 14895 RVA: 0x00143ACA File Offset: 0x00141CCA
	public virtual Sprite GetSprite(string topic)
	{
		return null;
	}

	// Token: 0x040023C2 RID: 9154
	public string id;

	// Token: 0x040023C3 RID: 9155
	public string target;
}
