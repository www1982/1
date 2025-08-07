using System;
using System.Diagnostics;

// Token: 0x02000904 RID: 2308
public class CellDigEvent : CellEvent
{
	// Token: 0x06004067 RID: 16487 RVA: 0x00169227 File Offset: 0x00167427
	public CellDigEvent(bool enable_logging = true)
		: base("Dig", "Dig", true, enable_logging)
	{
	}

	// Token: 0x06004068 RID: 16488 RVA: 0x0016923C File Offset: 0x0016743C
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, int callback_id)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, 0, 0, this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x06004069 RID: 16489 RVA: 0x00169268 File Offset: 0x00167468
	public override string GetDescription(EventInstanceBase ev)
	{
		return base.GetMessagePrefix() + "Dig=true";
	}
}
