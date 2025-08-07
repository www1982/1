using System;
using STRINGS;

// Token: 0x02000D4B RID: 3403
public class CodexUnlockedMessage : Message
{
	// Token: 0x06006989 RID: 27017 RVA: 0x0027F982 File Offset: 0x0027DB82
	public CodexUnlockedMessage()
	{
	}

	// Token: 0x0600698A RID: 27018 RVA: 0x0027F98A File Offset: 0x0027DB8A
	public CodexUnlockedMessage(string lock_id, string unlock_message)
	{
		this.lockId = lock_id;
		this.unlockMessage = unlock_message;
	}

	// Token: 0x0600698B RID: 27019 RVA: 0x0027F9A0 File Offset: 0x0027DBA0
	public string GetLockId()
	{
		return this.lockId;
	}

	// Token: 0x0600698C RID: 27020 RVA: 0x0027F9A8 File Offset: 0x0027DBA8
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x0600698D RID: 27021 RVA: 0x0027F9AF File Offset: 0x0027DBAF
	public override string GetMessageBody()
	{
		return UI.CODEX.CODEX_DISCOVERED_MESSAGE.BODY.Replace("{codex}", this.unlockMessage);
	}

	// Token: 0x0600698E RID: 27022 RVA: 0x0027F9C6 File Offset: 0x0027DBC6
	public override string GetTitle()
	{
		return UI.CODEX.CODEX_DISCOVERED_MESSAGE.TITLE;
	}

	// Token: 0x0600698F RID: 27023 RVA: 0x0027F9D2 File Offset: 0x0027DBD2
	public override string GetTooltip()
	{
		return this.GetMessageBody();
	}

	// Token: 0x06006990 RID: 27024 RVA: 0x0027F9DA File Offset: 0x0027DBDA
	public override bool IsValid()
	{
		return true;
	}

	// Token: 0x04004836 RID: 18486
	private string unlockMessage;

	// Token: 0x04004837 RID: 18487
	private string lockId;
}
