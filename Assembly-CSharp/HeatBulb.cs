using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000A62 RID: 2658
[AddComponentMenu("KMonoBehaviour/scripts/HeatBulb")]
public class HeatBulb : KMonoBehaviour, ISim200ms
{
	// Token: 0x06004CFE RID: 19710 RVA: 0x001BE29D File Offset: 0x001BC49D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.kanim.Play("off", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06004CFF RID: 19711 RVA: 0x001BE2C8 File Offset: 0x001BC4C8
	public void Sim200ms(float dt)
	{
		float num = this.kjConsumptionRate * dt;
		Vector2I vector2I = this.maxCheckOffset - this.minCheckOffset + 1;
		int num2 = vector2I.x * vector2I.y;
		float num3 = num / (float)num2;
		int num4;
		int num5;
		Grid.PosToXY(base.transform.GetPosition(), out num4, out num5);
		for (int i = this.minCheckOffset.y; i <= this.maxCheckOffset.y; i++)
		{
			for (int j = this.minCheckOffset.x; j <= this.maxCheckOffset.x; j++)
			{
				int num6 = Grid.XYToCell(num4 + j, num5 + i);
				if (Grid.IsValidCell(num6) && Grid.Temperature[num6] > this.minTemperature)
				{
					this.kjConsumed += num3;
					SimMessages.ModifyEnergy(num6, -num3, 5000f, SimMessages.EnergySourceID.HeatBulb);
				}
			}
		}
		float num7 = this.lightKJConsumptionRate * dt;
		if (this.kjConsumed > num7)
		{
			if (!this.lightSource.enabled)
			{
				this.kanim.Play("open", KAnim.PlayMode.Once, 1f, 0f);
				this.kanim.Queue("on", KAnim.PlayMode.Once, 1f, 0f);
				this.lightSource.enabled = true;
			}
			this.kjConsumed -= num7;
			return;
		}
		if (this.lightSource.enabled)
		{
			this.kanim.Play("close", KAnim.PlayMode.Once, 1f, 0f);
			this.kanim.Queue("off", KAnim.PlayMode.Once, 1f, 0f);
		}
		this.lightSource.enabled = false;
	}

	// Token: 0x04003325 RID: 13093
	[SerializeField]
	private float minTemperature;

	// Token: 0x04003326 RID: 13094
	[SerializeField]
	private float kjConsumptionRate;

	// Token: 0x04003327 RID: 13095
	[SerializeField]
	private float lightKJConsumptionRate;

	// Token: 0x04003328 RID: 13096
	[SerializeField]
	private Vector2I minCheckOffset;

	// Token: 0x04003329 RID: 13097
	[SerializeField]
	private Vector2I maxCheckOffset;

	// Token: 0x0400332A RID: 13098
	[MyCmpGet]
	private Light2D lightSource;

	// Token: 0x0400332B RID: 13099
	[MyCmpGet]
	private KBatchedAnimController kanim;

	// Token: 0x0400332C RID: 13100
	[Serialize]
	private float kjConsumed;
}
