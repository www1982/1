using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000959 RID: 2393
public interface IGameObjectEffectDescriptor
{
	// Token: 0x060044C2 RID: 17602
	List<Descriptor> GetDescriptors(GameObject go);
}
