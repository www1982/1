using System;
using KSerialization;

// Token: 0x02000513 RID: 1299
[SerializationConfig(MemberSerialization.OptIn)]
public class StateMachineComponent<StateMachineInstanceType> : StateMachineComponent, ISaveLoadable where StateMachineInstanceType : StateMachine.Instance
{
	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06001BBF RID: 7103 RVA: 0x00097550 File Offset: 0x00095750
	public StateMachineInstanceType smi
	{
		get
		{
			if (this._smi == null)
			{
				this._smi = (StateMachineInstanceType)((object)Activator.CreateInstance(typeof(StateMachineInstanceType), new object[] { this }));
			}
			return this._smi;
		}
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x00097589 File Offset: 0x00095789
	public override StateMachine.Instance GetSMI()
	{
		return this._smi;
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x00097596 File Offset: 0x00095796
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this._smi != null)
		{
			this._smi.StopSM("StateMachineComponent.OnCleanUp");
			this._smi = default(StateMachineInstanceType);
		}
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x000975CC File Offset: 0x000957CC
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (base.isSpawned)
		{
			this.smi.StartSM();
		}
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x000975EC File Offset: 0x000957EC
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this._smi != null)
		{
			this._smi.StopSM("StateMachineComponent.OnDisable");
		}
	}

	// Token: 0x0400105B RID: 4187
	private StateMachineInstanceType _smi;
}
