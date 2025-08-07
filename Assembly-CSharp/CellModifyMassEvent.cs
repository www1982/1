using System;
using System.Diagnostics;

// Token: 0x02000909 RID: 2313
public class CellModifyMassEvent : CellEvent
{
	// Token: 0x06004075 RID: 16501 RVA: 0x00169BA7 File Offset: 0x00167DA7
	public CellModifyMassEvent(string id, string reason, bool enable_logging = false)
		: base(id, reason, true, enable_logging)
	{
	}

	// Token: 0x06004076 RID: 16502 RVA: 0x00169BB4 File Offset: 0x00167DB4
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void Log(int cell, SimHashes element, float amount)
	{
		if (!this.enableLogging)
		{
			return;
		}
		CellEventInstance cellEventInstance = new CellEventInstance(cell, (int)element, (int)(amount * 1000f), this);
		CellEventLogger.Instance.Add(cellEventInstance);
	}

	// Token: 0x06004077 RID: 16503 RVA: 0x00169BE8 File Offset: 0x00167DE8
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
