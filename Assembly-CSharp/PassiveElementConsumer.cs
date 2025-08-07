using System;

// Token: 0x02000A54 RID: 2644
public class PassiveElementConsumer : ElementConsumer, IGameObjectEffectDescriptor
{
	// Token: 0x06004CAE RID: 19630 RVA: 0x001BCEF6 File Offset: 0x001BB0F6
	protected override bool IsActive()
	{
		return true;
	}
}
