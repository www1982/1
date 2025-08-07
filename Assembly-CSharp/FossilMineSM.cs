using System;

// Token: 0x02000732 RID: 1842
public class FossilMineSM : ComplexFabricatorSM
{
	// Token: 0x06002E6D RID: 11885 RVA: 0x0010A5A6 File Offset: 0x001087A6
	protected override void OnSpawn()
	{
	}

	// Token: 0x06002E6E RID: 11886 RVA: 0x0010A5A8 File Offset: 0x001087A8
	public void Activate()
	{
		base.smi.StartSM();
	}

	// Token: 0x06002E6F RID: 11887 RVA: 0x0010A5B5 File Offset: 0x001087B5
	public void Deactivate()
	{
		base.smi.StopSM("FossilMine.Deactivated");
	}
}
