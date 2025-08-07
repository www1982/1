using System;

// Token: 0x02000A6D RID: 2669
public class SaltPlant : StateMachineComponent<SaltPlant.StatesInstance>
{
	// Token: 0x06004D3A RID: 19770 RVA: 0x001BF19D File Offset: 0x001BD39D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SaltPlant>(-724860998, SaltPlant.OnWiltDelegate);
		base.Subscribe<SaltPlant>(712767498, SaltPlant.OnWiltRecoverDelegate);
	}

	// Token: 0x06004D3B RID: 19771 RVA: 0x001BF1C7 File Offset: 0x001BD3C7
	private void OnWilt(object data = null)
	{
		base.gameObject.GetComponent<ElementConsumer>().EnableConsumption(false);
	}

	// Token: 0x06004D3C RID: 19772 RVA: 0x001BF1DA File Offset: 0x001BD3DA
	private void OnWiltRecover(object data = null)
	{
		base.gameObject.GetComponent<ElementConsumer>().EnableConsumption(true);
	}

	// Token: 0x04003354 RID: 13140
	private static readonly EventSystem.IntraObjectHandler<SaltPlant> OnWiltDelegate = new EventSystem.IntraObjectHandler<SaltPlant>(delegate(SaltPlant component, object data)
	{
		component.OnWilt(data);
	});

	// Token: 0x04003355 RID: 13141
	private static readonly EventSystem.IntraObjectHandler<SaltPlant> OnWiltRecoverDelegate = new EventSystem.IntraObjectHandler<SaltPlant>(delegate(SaltPlant component, object data)
	{
		component.OnWiltRecover(data);
	});

	// Token: 0x02001B3E RID: 6974
	public class StatesInstance : GameStateMachine<SaltPlant.States, SaltPlant.StatesInstance, SaltPlant, object>.GameInstance
	{
		// Token: 0x0600A6B0 RID: 42672 RVA: 0x003AE190 File Offset: 0x003AC390
		public StatesInstance(SaltPlant master)
			: base(master)
		{
		}
	}

	// Token: 0x02001B3F RID: 6975
	public class States : GameStateMachine<SaltPlant.States, SaltPlant.StatesInstance, SaltPlant>
	{
		// Token: 0x0600A6B1 RID: 42673 RVA: 0x003AE199 File Offset: 0x003AC399
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.alive;
			this.alive.DoNothing();
		}

		// Token: 0x040081F5 RID: 33269
		public GameStateMachine<SaltPlant.States, SaltPlant.StatesInstance, SaltPlant, object>.State alive;
	}
}
