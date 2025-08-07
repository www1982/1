using System;

// Token: 0x02000213 RID: 531
public class FossilMineWorkable : ComplexFabricatorWorkable
{
	// Token: 0x06000AA6 RID: 2726 RVA: 0x0004054B File Offset: 0x0003E74B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.shouldShowSkillPerkStatusItem = false;
	}
}
