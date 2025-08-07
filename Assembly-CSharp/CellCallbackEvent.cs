using System;
using System.Diagnostics;

// Token: 0x02000903 RID: 2307
public class CellCallbackEvent : CellEvent
{
	// Token: 0x06004064 RID: 16484 RVA: 0x001691BC File Offset: 0x001673BC
	public CellCallbackEvent(string id, bool is_send, bool enable_logging = true)
		: base(id, "Callback", is_send, enable_logging)
	{
	}

	// Token: 0x06004065 RID: 16485 RVA: 0x001691CC File Offset: 0x001673CC
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, int callback_id)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, callback_id, 0, this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x06004066 RID: 16486 RVA: 0x001691F8 File Offset: 0x001673F8
	public override string GetDescription(EventInstanceBase ev)
	{
		CellEventInstance cellEventInstance = ev as CellEventInstance;
		return base.GetMessagePrefix() + "Callback=" + cellEventInstance.data.ToString();
	}
}
