using System;

// Token: 0x020003EC RID: 1004
public interface IShearable
{
	// Token: 0x06001487 RID: 5255
	bool IsFullyGrown();

	// Token: 0x06001488 RID: 5256
	void Shear();

	// Token: 0x06001489 RID: 5257
	global::Tuple<Tag, float> GetItemDroppedOnShear();
}
