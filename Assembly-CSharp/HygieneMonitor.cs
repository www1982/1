using System;
using Klei.AI;

// Token: 0x020009F4 RID: 2548
public class HygieneMonitor : GameStateMachine<HygieneMonitor, HygieneMonitor.Instance>
{
	// Token: 0x06004A63 RID: 19043 RVA: 0x001AF098 File Offset: 0x001AD298
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.needsshower;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.clean.EventTransition(GameHashes.EffectRemoved, this.needsshower, (HygieneMonitor.Instance smi) => smi.NeedsShower());
		this.needsshower.EventTransition(GameHashes.EffectAdded, this.clean, (HygieneMonitor.Instance smi) => !smi.NeedsShower()).ToggleUrge(Db.Get().Urges.Shower).Enter(delegate(HygieneMonitor.Instance smi)
		{
			smi.SetDirtiness(1f);
		});
	}

	// Token: 0x0400311E RID: 12574
	public StateMachine<HygieneMonitor, HygieneMonitor.Instance, IStateMachineTarget, object>.FloatParameter dirtiness;

	// Token: 0x0400311F RID: 12575
	public GameStateMachine<HygieneMonitor, HygieneMonitor.Instance, IStateMachineTarget, object>.State clean;

	// Token: 0x04003120 RID: 12576
	public GameStateMachine<HygieneMonitor, HygieneMonitor.Instance, IStateMachineTarget, object>.State needsshower;

	// Token: 0x02001A60 RID: 6752
	public new class Instance : GameStateMachine<HygieneMonitor, HygieneMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A354 RID: 41812 RVA: 0x003A3D03 File Offset: 0x003A1F03
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.effects = master.GetComponent<Effects>();
		}

		// Token: 0x0600A355 RID: 41813 RVA: 0x003A3D18 File Offset: 0x003A1F18
		public float GetDirtiness()
		{
			return base.sm.dirtiness.Get(this);
		}

		// Token: 0x0600A356 RID: 41814 RVA: 0x003A3D2B File Offset: 0x003A1F2B
		public void SetDirtiness(float dirtiness)
		{
			base.sm.dirtiness.Set(dirtiness, this, false);
		}

		// Token: 0x0600A357 RID: 41815 RVA: 0x003A3D41 File Offset: 0x003A1F41
		public bool NeedsShower()
		{
			return !this.effects.HasEffect(Shower.SHOWER_EFFECT);
		}

		// Token: 0x04007F94 RID: 32660
		private Effects effects;
	}
}
