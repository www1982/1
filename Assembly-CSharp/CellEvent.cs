using System;

// Token: 0x02000906 RID: 2310
public class CellEvent : EventBase
{
	// Token: 0x0600406D RID: 16493 RVA: 0x00169312 File Offset: 0x00167512
	public CellEvent(string id, string reason, bool is_send, bool enable_logging = true)
		: base(id)
	{
		this.reason = reason;
		this.isSend = is_send;
		this.enableLogging = enable_logging;
	}

	// Token: 0x0600406E RID: 16494 RVA: 0x00169331 File Offset: 0x00167531
	public string GetMessagePrefix()
	{
		if (this.isSend)
		{
			return ">>>: ";
		}
		return "<<<: ";
	}

	// Token: 0x040027F3 RID: 10227
	public string reason;

	// Token: 0x040027F4 RID: 10228
	public bool isSend;

	// Token: 0x040027F5 RID: 10229
	public bool enableLogging;
}
