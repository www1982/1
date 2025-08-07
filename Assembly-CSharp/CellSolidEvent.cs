using System;
using System.Diagnostics;

// Token: 0x0200090A RID: 2314
public class CellSolidEvent : CellEvent
{
	// Token: 0x06004078 RID: 16504 RVA: 0x00169C68 File Offset: 0x00167E68
	public CellSolidEvent(string id, string reason, bool is_send, bool enable_logging = true)
		: base(id, reason, is_send, enable_logging)
	{
	}

	// Token: 0x06004079 RID: 16505 RVA: 0x00169C78 File Offset: 0x00167E78
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

	// Token: 0x0600407A RID: 16506 RVA: 0x00169CAC File Offset: 0x00167EAC
	public override string GetDescription(EventInstanceBase ev)
	{
		if ((ev as CellEventInstance).data == 1)
		{
			return base.GetMessagePrefix() + "Solid=true (" + this.reason + ")";
		}
		return base.GetMessagePrefix() + "Solid=false (" + this.reason + ")";
	}
}
