using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000D4D RID: 3405
public class DiscoveredSpaceMessage : Message
{
	// Token: 0x06006998 RID: 27032 RVA: 0x0027FA5A File Offset: 0x0027DC5A
	public DiscoveredSpaceMessage()
	{
	}

	// Token: 0x06006999 RID: 27033 RVA: 0x0027FA62 File Offset: 0x0027DC62
	public DiscoveredSpaceMessage(Vector3 pos)
	{
		this.cameraFocusPos = pos;
		this.cameraFocusPos.z = -40f;
	}

	// Token: 0x0600699A RID: 27034 RVA: 0x0027FA81 File Offset: 0x0027DC81
	public override string GetSound()
	{
		return "Discover_Space";
	}

	// Token: 0x0600699B RID: 27035 RVA: 0x0027FA88 File Offset: 0x0027DC88
	public override string GetMessageBody()
	{
		return MISC.NOTIFICATIONS.DISCOVERED_SPACE.TOOLTIP;
	}

	// Token: 0x0600699C RID: 27036 RVA: 0x0027FA94 File Offset: 0x0027DC94
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.DISCOVERED_SPACE.NAME;
	}

	// Token: 0x0600699D RID: 27037 RVA: 0x0027FAA0 File Offset: 0x0027DCA0
	public override string GetTooltip()
	{
		return null;
	}

	// Token: 0x0600699E RID: 27038 RVA: 0x0027FAA3 File Offset: 0x0027DCA3
	public override bool IsValid()
	{
		return true;
	}

	// Token: 0x0600699F RID: 27039 RVA: 0x0027FAA6 File Offset: 0x0027DCA6
	public override void OnClick()
	{
		this.OnDiscoveredSpaceClicked();
	}

	// Token: 0x060069A0 RID: 27040 RVA: 0x0027FAAE File Offset: 0x0027DCAE
	private void OnDiscoveredSpaceClicked()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound(this.GetSound(), false));
		MusicManager.instance.PlaySong("Stinger_Surface", false);
		CameraController.Instance.SetTargetPos(this.cameraFocusPos, 8f, true);
	}

	// Token: 0x04004839 RID: 18489
	[Serialize]
	private Vector3 cameraFocusPos;

	// Token: 0x0400483A RID: 18490
	private const string MUSIC_STINGER = "Stinger_Surface";
}
