using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;

// Token: 0x0200090E RID: 2318
[SerializationConfig(MemberSerialization.OptIn)]
public class EventLogger<EventInstanceType, EventType> : KMonoBehaviour, ISaveLoadable where EventInstanceType : EventInstanceBase where EventType : EventBase
{
	// Token: 0x06004082 RID: 16514 RVA: 0x00169E0A File Offset: 0x0016800A
	public IEnumerator<EventInstanceType> GetEnumerator()
	{
		return this.EventInstances.GetEnumerator();
	}

	// Token: 0x06004083 RID: 16515 RVA: 0x00169E1C File Offset: 0x0016801C
	public EventType AddEvent(EventType ev)
	{
		for (int i = 0; i < this.Events.Count; i++)
		{
			if (this.Events[i].hash == ev.hash)
			{
				this.Events[i] = ev;
				return this.Events[i];
			}
		}
		this.Events.Add(ev);
		return ev;
	}

	// Token: 0x06004084 RID: 16516 RVA: 0x00169E89 File Offset: 0x00168089
	public EventInstanceType Add(EventInstanceType ev)
	{
		if (this.EventInstances.Count > 10000)
		{
			this.EventInstances.RemoveAt(0);
		}
		this.EventInstances.Add(ev);
		return ev;
	}

	// Token: 0x06004085 RID: 16517 RVA: 0x00169EB8 File Offset: 0x001680B8
	[OnDeserialized]
	protected internal void OnDeserialized()
	{
		if (this.EventInstances.Count > 10000)
		{
			this.EventInstances.RemoveRange(0, this.EventInstances.Count - 10000);
		}
		for (int i = 0; i < this.EventInstances.Count; i++)
		{
			for (int j = 0; j < this.Events.Count; j++)
			{
				if (this.Events[j].hash == this.EventInstances[i].eventHash)
				{
					this.EventInstances[i].ev = this.Events[j];
					break;
				}
			}
		}
	}

	// Token: 0x06004086 RID: 16518 RVA: 0x00169F77 File Offset: 0x00168177
	public void Clear()
	{
		this.EventInstances.Clear();
	}

	// Token: 0x0400283D RID: 10301
	private const int MAX_NUM_EVENTS = 10000;

	// Token: 0x0400283E RID: 10302
	private List<EventType> Events = new List<EventType>();

	// Token: 0x0400283F RID: 10303
	[Serialize]
	private List<EventInstanceType> EventInstances = new List<EventInstanceType>();
}
