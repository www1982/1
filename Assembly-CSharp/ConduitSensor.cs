using System;

// Token: 0x02000701 RID: 1793
public abstract class ConduitSensor : Switch
{
	// Token: 0x06002CD9 RID: 11481
	protected abstract void ConduitUpdate(float dt);

	// Token: 0x06002CDA RID: 11482 RVA: 0x001021B4 File Offset: 0x001003B4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.animController = base.GetComponent<KBatchedAnimController>();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			Conduit.GetFlowManager(this.conduitType).AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
			return;
		}
		SolidConduit.GetFlowManager().AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
	}

	// Token: 0x06002CDB RID: 11483 RVA: 0x00102248 File Offset: 0x00100448
	protected override void OnCleanUp()
	{
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			Conduit.GetFlowManager(this.conduitType).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		}
		else
		{
			SolidConduit.GetFlowManager().RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		}
		base.OnCleanUp();
	}

	// Token: 0x06002CDC RID: 11484 RVA: 0x001022A3 File Offset: 0x001004A3
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x06002CDD RID: 11485 RVA: 0x001022B2 File Offset: 0x001004B2
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06002CDE RID: 11486 RVA: 0x001022D0 File Offset: 0x001004D0
	protected virtual void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			if (this.switchedOn)
			{
				this.animController.Play(ConduitSensor.ON_ANIMS, KAnim.PlayMode.Loop);
				return;
			}
			this.animController.Play(ConduitSensor.OFF_ANIMS, KAnim.PlayMode.Once);
		}
	}

	// Token: 0x04001A76 RID: 6774
	public ConduitType conduitType;

	// Token: 0x04001A77 RID: 6775
	protected bool wasOn;

	// Token: 0x04001A78 RID: 6776
	protected KBatchedAnimController animController;

	// Token: 0x04001A79 RID: 6777
	protected static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on" };

	// Token: 0x04001A7A RID: 6778
	protected static readonly HashedString[] OFF_ANIMS = new HashedString[] { "on_pst", "off" };
}
