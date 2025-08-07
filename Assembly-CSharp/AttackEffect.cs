using System;

// Token: 0x0200082C RID: 2092
[Serializable]
public class AttackEffect
{
	// Token: 0x06003945 RID: 14661 RVA: 0x0013D85B File Offset: 0x0013BA5B
	public AttackEffect(string ID, float probability)
	{
		this.effectID = ID;
		this.effectProbability = probability;
	}

	// Token: 0x0400229B RID: 8859
	public string effectID;

	// Token: 0x0400229C RID: 8860
	public float effectProbability;
}
