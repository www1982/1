using System;
using KSerialization;

// Token: 0x02000D5D RID: 3421
public abstract class TargetMessage : Message
{
	// Token: 0x06006A16 RID: 27158 RVA: 0x00280966 File Offset: 0x0027EB66
	protected TargetMessage()
	{
	}

	// Token: 0x06006A17 RID: 27159 RVA: 0x0028096E File Offset: 0x0027EB6E
	public TargetMessage(KPrefabID prefab_id)
	{
		this.target = new MessageTarget(prefab_id);
	}

	// Token: 0x06006A18 RID: 27160 RVA: 0x00280982 File Offset: 0x0027EB82
	public MessageTarget GetTarget()
	{
		return this.target;
	}

	// Token: 0x06006A19 RID: 27161 RVA: 0x0028098A File Offset: 0x0027EB8A
	public override void OnCleanUp()
	{
		this.target.OnCleanUp();
	}

	// Token: 0x04004861 RID: 18529
	[Serialize]
	private MessageTarget target;
}
