using System;
using KSerialization;
using STRINGS;

// Token: 0x02000D61 RID: 3425
public class WorldDetectedMessage : Message
{
	// Token: 0x06006A2A RID: 27178 RVA: 0x00280BAB File Offset: 0x0027EDAB
	public WorldDetectedMessage()
	{
	}

	// Token: 0x06006A2B RID: 27179 RVA: 0x00280BB3 File Offset: 0x0027EDB3
	public WorldDetectedMessage(WorldContainer world)
	{
		this.worldID = world.id;
	}

	// Token: 0x06006A2C RID: 27180 RVA: 0x00280BC7 File Offset: 0x0027EDC7
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x06006A2D RID: 27181 RVA: 0x00280BD0 File Offset: 0x0027EDD0
	public override string GetMessageBody()
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(this.worldID);
		return string.Format(MISC.NOTIFICATIONS.WORLDDETECTED.MESSAGEBODY, world.GetProperName());
	}

	// Token: 0x06006A2E RID: 27182 RVA: 0x00280C03 File Offset: 0x0027EE03
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.WORLDDETECTED.NAME;
	}

	// Token: 0x06006A2F RID: 27183 RVA: 0x00280C10 File Offset: 0x0027EE10
	public override string GetTooltip()
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(this.worldID);
		return string.Format(MISC.NOTIFICATIONS.WORLDDETECTED.TOOLTIP, world.GetProperName());
	}

	// Token: 0x06006A30 RID: 27184 RVA: 0x00280C43 File Offset: 0x0027EE43
	public override bool IsValid()
	{
		return this.worldID != 255;
	}

	// Token: 0x0400486F RID: 18543
	[Serialize]
	private int worldID;
}
