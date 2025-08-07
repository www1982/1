using System;
using UnityEngine;

// Token: 0x02000712 RID: 1810
public class DevLifeSupport : KMonoBehaviour, ISim200ms
{
	// Token: 0x06002D74 RID: 11636 RVA: 0x00104C1D File Offset: 0x00102E1D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.elementConsumer != null)
		{
			this.elementConsumer.EnableConsumption(true);
		}
	}

	// Token: 0x06002D75 RID: 11637 RVA: 0x00104C40 File Offset: 0x00102E40
	public void Sim200ms(float dt)
	{
		Vector2I vector2I = new Vector2I(-this.effectRadius, -this.effectRadius);
		Vector2I vector2I2 = new Vector2I(this.effectRadius, this.effectRadius);
		int num;
		int num2;
		Grid.PosToXY(base.transform.GetPosition(), out num, out num2);
		int num3 = Grid.XYToCell(num, num2);
		if (Grid.IsValidCell(num3))
		{
			int num4 = (int)Grid.WorldIdx[num3];
			for (int i = vector2I.y; i <= vector2I2.y; i++)
			{
				for (int j = vector2I.x; j <= vector2I2.x; j++)
				{
					int num5 = Grid.XYToCell(num + j, num2 + i);
					if (Grid.IsValidCellInWorld(num5, num4))
					{
						float num6 = (this.targetTemperature - Grid.Temperature[num5]) * Grid.Element[num5].specificHeatCapacity * Grid.Mass[num5];
						if (!Mathf.Approximately(0f, num6))
						{
							SimMessages.ModifyEnergy(num5, num6 * 0.2f, 5000f, (num6 > 0f) ? SimMessages.EnergySourceID.DebugHeat : SimMessages.EnergySourceID.DebugCool);
						}
					}
				}
			}
		}
	}

	// Token: 0x04001ABC RID: 6844
	[MyCmpReq]
	private ElementConsumer elementConsumer;

	// Token: 0x04001ABD RID: 6845
	public float targetTemperature = 294.15f;

	// Token: 0x04001ABE RID: 6846
	public int effectRadius = 7;

	// Token: 0x04001ABF RID: 6847
	private const float temperatureControlK = 0.2f;
}
