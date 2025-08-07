using System;
using System.Collections.Generic;

// Token: 0x0200095A RID: 2394
[Obsolete("No longer used. Use IGameObjectEffectDescriptor instead", false)]
public interface IEffectDescriptor
{
	// Token: 0x060044C3 RID: 17603
	List<Descriptor> GetDescriptors(BuildingDef def);
}
