using System;
using KSerialization;

// Token: 0x0200079D RID: 1949
[SerializationConfig(MemberSerialization.OptIn)]
public class OperationalControlledSwitch : CircuitSwitch
{
	// Token: 0x0600339F RID: 13215 RVA: 0x00122209 File Offset: 0x00120409
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.manuallyControlled = false;
	}

	// Token: 0x060033A0 RID: 13216 RVA: 0x00122218 File Offset: 0x00120418
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<OperationalControlledSwitch>(-592767678, OperationalControlledSwitch.OnOperationalChangedDelegate);
	}

	// Token: 0x060033A1 RID: 13217 RVA: 0x00122234 File Offset: 0x00120434
	private void OnOperationalChanged(object data)
	{
		bool flag = (bool)data;
		this.SetState(flag);
	}

	// Token: 0x04001F0B RID: 7947
	private static readonly EventSystem.IntraObjectHandler<OperationalControlledSwitch> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<OperationalControlledSwitch>(delegate(OperationalControlledSwitch component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
