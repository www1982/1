using System;

// Token: 0x020009E8 RID: 2536
public class DoctorMonitor : GameStateMachine<DoctorMonitor, DoctorMonitor.Instance>
{
	// Token: 0x06004A25 RID: 18981 RVA: 0x001AD91F File Offset: 0x001ABB1F
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.ToggleUrge(Db.Get().Urges.Doctor);
	}

	// Token: 0x02001A40 RID: 6720
	public new class Instance : GameStateMachine<DoctorMonitor, DoctorMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A2C1 RID: 41665 RVA: 0x003A1E26 File Offset: 0x003A0026
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
