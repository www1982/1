using System;

// Token: 0x020002DC RID: 732
public class ArtifactTier
{
	// Token: 0x06000EDF RID: 3807 RVA: 0x00057945 File Offset: 0x00055B45
	public ArtifactTier(StringKey str_key, EffectorValues values, float payload_drop_chance)
	{
		this.decorValues = values;
		this.name_key = str_key;
		this.payloadDropChance = payload_drop_chance;
	}

	// Token: 0x040009B0 RID: 2480
	public EffectorValues decorValues;

	// Token: 0x040009B1 RID: 2481
	public StringKey name_key;

	// Token: 0x040009B2 RID: 2482
	public float payloadDropChance;
}
