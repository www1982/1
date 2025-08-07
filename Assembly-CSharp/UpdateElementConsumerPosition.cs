using System;
using UnityEngine;

// Token: 0x02000147 RID: 327
[AddComponentMenu("KMonoBehaviour/scripts/UpdateElementConsumerPosition")]
public class UpdateElementConsumerPosition : KMonoBehaviour, ISim200ms
{
	// Token: 0x06000620 RID: 1568 RVA: 0x0002D3CA File Offset: 0x0002B5CA
	public void Sim200ms(float dt)
	{
		base.GetComponent<ElementConsumer>().RefreshConsumptionRate();
	}
}
