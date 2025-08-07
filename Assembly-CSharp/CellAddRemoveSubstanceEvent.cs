using System;
using System.Diagnostics;

// Token: 0x02000902 RID: 2306
public class CellAddRemoveSubstanceEvent : CellEvent
{
	// Token: 0x06004061 RID: 16481 RVA: 0x001690F9 File Offset: 0x001672F9
	public CellAddRemoveSubstanceEvent(string id, string reason, bool enable_logging = false)
		: base(id, reason, true, enable_logging)
	{
	}

	// Token: 0x06004062 RID: 16482 RVA: 0x00169108 File Offset: 0x00167308
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, SimHashes element, float amount, int callback_id)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, (int)element, (int)(amount * 1000f), this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x06004063 RID: 16483 RVA: 0x0016913C File Offset: 0x0016733C
	public override string GetDescription(EventInstanceBase ev)
	{
		CellEventInstance cellEventInstance = ev as CellEventInstance;
		SimHashes data = (SimHashes)cellEventInstance.data;
		return string.Concat(new string[]
		{
			base.GetMessagePrefix(),
			"Element=",
			data.ToString(),
			", Mass=",
			((float)cellEventInstance.data2 / 1000f).ToString(),
			" (",
			this.reason,
			")"
		});
	}
}
