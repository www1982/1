using System;
using System.Diagnostics;

// Token: 0x0200090B RID: 2315
public class CellSolidFilterEvent : CellEvent
{
	// Token: 0x0600407B RID: 16507 RVA: 0x00169CFE File Offset: 0x00167EFE
	public CellSolidFilterEvent(string id, bool enable_logging = true)
		: base(id, "filtered", false, enable_logging)
	{
	}

	// Token: 0x0600407C RID: 16508 RVA: 0x00169D10 File Offset: 0x00167F10
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, bool solid)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, solid ? 1 : 0, 0, this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x0600407D RID: 16509 RVA: 0x00169D44 File Offset: 0x00167F44
	public override string GetDescription(EventInstanceBase ev)
	{
		CellEventInstance cellEventInstance = ev as CellEventInstance;
		return base.GetMessagePrefix() + "Filtered Solid Event solid=" + cellEventInstance.data.ToString();
	}
}
