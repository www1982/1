using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000D4C RID: 3404
public class DeathMessage : TargetMessage
{
	// Token: 0x06006991 RID: 27025 RVA: 0x0027F9DD File Offset: 0x0027DBDD
	public DeathMessage()
	{
	}

	// Token: 0x06006992 RID: 27026 RVA: 0x0027F9F0 File Offset: 0x0027DBF0
	public DeathMessage(GameObject go, Death death)
		: base(go.GetComponent<KPrefabID>())
	{
		this.death.Set(death);
	}

	// Token: 0x06006993 RID: 27027 RVA: 0x0027FA15 File Offset: 0x0027DC15
	public override string GetSound()
	{
		return "";
	}

	// Token: 0x06006994 RID: 27028 RVA: 0x0027FA1C File Offset: 0x0027DC1C
	public override bool PlayNotificationSound()
	{
		return false;
	}

	// Token: 0x06006995 RID: 27029 RVA: 0x0027FA1F File Offset: 0x0027DC1F
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.DUPLICANTDIED.NAME;
	}

	// Token: 0x06006996 RID: 27030 RVA: 0x0027FA2B File Offset: 0x0027DC2B
	public override string GetTooltip()
	{
		return this.GetMessageBody();
	}

	// Token: 0x06006997 RID: 27031 RVA: 0x0027FA33 File Offset: 0x0027DC33
	public override string GetMessageBody()
	{
		return this.death.Get().description.Replace("{Target}", base.GetTarget().GetName());
	}

	// Token: 0x04004838 RID: 18488
	[Serialize]
	private ResourceRef<Death> death = new ResourceRef<Death>();
}
