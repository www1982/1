using System;

// Token: 0x0200050E RID: 1294
public abstract class StateEvent
{
	// Token: 0x06001B9D RID: 7069 RVA: 0x00097049 File Offset: 0x00095249
	public StateEvent(string name)
	{
		this.name = name;
		this.debugName = "(Event)" + name;
	}

	// Token: 0x06001B9E RID: 7070 RVA: 0x00097069 File Offset: 0x00095269
	public virtual StateEvent.Context Subscribe(StateMachine.Instance smi)
	{
		return new StateEvent.Context(this);
	}

	// Token: 0x06001B9F RID: 7071 RVA: 0x00097071 File Offset: 0x00095271
	public virtual void Unsubscribe(StateMachine.Instance smi, StateEvent.Context context)
	{
	}

	// Token: 0x06001BA0 RID: 7072 RVA: 0x00097073 File Offset: 0x00095273
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x06001BA1 RID: 7073 RVA: 0x0009707B File Offset: 0x0009527B
	public string GetDebugName()
	{
		return this.debugName;
	}

	// Token: 0x0400104B RID: 4171
	protected string name;

	// Token: 0x0400104C RID: 4172
	private string debugName;

	// Token: 0x02001355 RID: 4949
	public struct Context
	{
		// Token: 0x06008A0C RID: 35340 RVA: 0x0034F074 File Offset: 0x0034D274
		public Context(StateEvent state_event)
		{
			this.stateEvent = state_event;
			this.data = 0;
		}

		// Token: 0x04006917 RID: 26903
		public StateEvent stateEvent;

		// Token: 0x04006918 RID: 26904
		public int data;
	}
}
