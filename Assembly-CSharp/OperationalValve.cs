using System;
using KSerialization;

// Token: 0x0200079E RID: 1950
[SerializationConfig(MemberSerialization.OptIn)]
public class OperationalValve : ValveBase
{
	// Token: 0x060033A4 RID: 13220 RVA: 0x00122273 File Offset: 0x00120473
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<OperationalValve>(-592767678, OperationalValve.OnOperationalChangedDelegate);
	}

	// Token: 0x060033A5 RID: 13221 RVA: 0x0012228C File Offset: 0x0012048C
	protected override void OnSpawn()
	{
		this.OnOperationalChanged(this.operational.IsOperational);
		base.OnSpawn();
	}

	// Token: 0x060033A6 RID: 13222 RVA: 0x001222AA File Offset: 0x001204AA
	protected override void OnCleanUp()
	{
		base.Unsubscribe<OperationalValve>(-592767678, OperationalValve.OnOperationalChangedDelegate, false);
		base.OnCleanUp();
	}

	// Token: 0x060033A7 RID: 13223 RVA: 0x001222C4 File Offset: 0x001204C4
	private void OnOperationalChanged(object data)
	{
		bool flag = (bool)data;
		if (flag)
		{
			base.CurrentFlow = base.MaxFlow;
		}
		else
		{
			base.CurrentFlow = 0f;
		}
		this.operational.SetActive(flag, false);
	}

	// Token: 0x060033A8 RID: 13224 RVA: 0x00122301 File Offset: 0x00120501
	protected override void OnMassTransfer(float amount)
	{
		this.isDispensing = amount > 0f;
	}

	// Token: 0x060033A9 RID: 13225 RVA: 0x00122314 File Offset: 0x00120514
	public override void UpdateAnim()
	{
		if (!this.operational.IsOperational)
		{
			this.controller.Queue("off", KAnim.PlayMode.Once, 1f, 0f);
			return;
		}
		if (this.isDispensing)
		{
			this.controller.Queue("on_flow", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		this.controller.Queue("on", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04001F0C RID: 7948
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001F0D RID: 7949
	private bool isDispensing;

	// Token: 0x04001F0E RID: 7950
	private static readonly EventSystem.IntraObjectHandler<OperationalValve> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<OperationalValve>(delegate(OperationalValve component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
