using System;
using KSerialization;

// Token: 0x0200090D RID: 2317
[SerializationConfig(MemberSerialization.OptIn)]
public class EventInstanceBase : ISaveLoadable
{
	// Token: 0x06004080 RID: 16512 RVA: 0x00169D90 File Offset: 0x00167F90
	public EventInstanceBase(EventBase ev)
	{
		this.frame = GameClock.Instance.GetFrame();
		this.eventHash = ev.hash;
		this.ev = ev;
	}

	// Token: 0x06004081 RID: 16513 RVA: 0x00169DBC File Offset: 0x00167FBC
	public override string ToString()
	{
		string text = "[" + this.frame.ToString() + "] ";
		if (this.ev != null)
		{
			return text + this.ev.GetDescription(this);
		}
		return text + "Unknown event";
	}

	// Token: 0x0400283A RID: 10298
	[Serialize]
	public int frame;

	// Token: 0x0400283B RID: 10299
	[Serialize]
	public int eventHash;

	// Token: 0x0400283C RID: 10300
	public EventBase ev;
}
