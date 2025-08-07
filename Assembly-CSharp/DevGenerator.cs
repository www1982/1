using System;
using UnityEngine;

// Token: 0x02000710 RID: 1808
public class DevGenerator : Generator
{
	// Token: 0x06002D61 RID: 11617 RVA: 0x00104898 File Offset: 0x00102A98
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, circuitID != ushort.MaxValue);
		if (!this.operational.IsOperational)
		{
			return;
		}
		float num = this.wattageRating;
		if (num > 0f)
		{
			num *= dt;
			num = Mathf.Max(num, 1f * dt);
			base.GenerateJoules(num, false);
		}
	}

	// Token: 0x04001AB1 RID: 6833
	public float wattageRating = 100000f;
}
