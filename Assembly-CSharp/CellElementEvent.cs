using System;
using System.Diagnostics;

// Token: 0x02000905 RID: 2309
public class CellElementEvent : CellEvent
{
	// Token: 0x0600406A RID: 16490 RVA: 0x0016927A File Offset: 0x0016747A
	public CellElementEvent(string id, string reason, bool is_send, bool enable_logging = true)
		: base(id, reason, is_send, enable_logging)
	{
	}

	// Token: 0x0600406B RID: 16491 RVA: 0x00169288 File Offset: 0x00167488
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, SimHashes element, int callback_id)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, (int)element, 0, this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x0600406C RID: 16492 RVA: 0x001692B4 File Offset: 0x001674B4
	public override string GetDescription(EventInstanceBase ev)
	{
		SimHashes data = (SimHashes)(ev as CellEventInstance).data;
		return string.Concat(new string[]
		{
			base.GetMessagePrefix(),
			"Element=",
			data.ToString(),
			" (",
			this.reason,
			")"
		});
	}
}
