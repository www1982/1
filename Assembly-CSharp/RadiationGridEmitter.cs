using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AB4 RID: 2740
[Serializable]
public class RadiationGridEmitter
{
	// Token: 0x06004F79 RID: 20345 RVA: 0x001CBB08 File Offset: 0x001C9D08
	public RadiationGridEmitter(int originCell, int intensity)
	{
		this.originCell = originCell;
		this.intensity = intensity;
	}

	// Token: 0x06004F7A RID: 20346 RVA: 0x001CBB58 File Offset: 0x001C9D58
	public void Emit()
	{
		this.scanCells.Clear();
		Vector2 vector = Grid.CellToPosCCC(this.originCell, Grid.SceneLayer.Building);
		for (float num = (float)this.direction - (float)this.angle / 2f; num < (float)this.direction + (float)this.angle / 2f; num += (float)(this.angle / this.projectionCount))
		{
			float num2 = global::UnityEngine.Random.Range((float)(-(float)this.angle / this.projectionCount) / 2f, (float)(this.angle / this.projectionCount) / 2f);
			Vector2 vector2 = new Vector2(Mathf.Cos((num + num2) * 3.1415927f / 180f), Mathf.Sin((num + num2) * 3.1415927f / 180f));
			int num3 = 3;
			float num4 = (float)(this.intensity / 4);
			Vector2 vector3 = vector2;
			float num5 = 0f;
			while ((double)num4 > 0.01 && num5 < (float)RadiationGridEmitter.MAX_EMIT_DISTANCE)
			{
				num5 += 1f / (float)num3;
				int num6 = Grid.PosToCell(vector + vector3 * num5);
				if (!Grid.IsValidCell(num6))
				{
					break;
				}
				if (!this.scanCells.Contains(num6))
				{
					SimMessages.ModifyRadiationOnCell(num6, (float)Mathf.RoundToInt(num4), -1);
					this.scanCells.Add(num6);
				}
				num4 *= Mathf.Max(0f, 1f - Mathf.Pow(Grid.Mass[num6], 1.25f) * Grid.Element[num6].molarMass / 1000000f);
				num4 *= global::UnityEngine.Random.Range(0.96f, 0.98f);
			}
		}
	}

	// Token: 0x06004F7B RID: 20347 RVA: 0x001CBD0B File Offset: 0x001C9F0B
	private int CalculateFalloff(float falloffRate, int cell, int origin)
	{
		return Mathf.Max(1, Mathf.RoundToInt(falloffRate * (float)Mathf.Max(Grid.GetCellDistance(origin, cell), 1)));
	}

	// Token: 0x04003573 RID: 13683
	private static int MAX_EMIT_DISTANCE = 128;

	// Token: 0x04003574 RID: 13684
	public int originCell = -1;

	// Token: 0x04003575 RID: 13685
	public int intensity = 1;

	// Token: 0x04003576 RID: 13686
	public int projectionCount = 20;

	// Token: 0x04003577 RID: 13687
	public int direction;

	// Token: 0x04003578 RID: 13688
	public int angle = 360;

	// Token: 0x04003579 RID: 13689
	public bool enabled;

	// Token: 0x0400357A RID: 13690
	private HashSet<int> scanCells = new HashSet<int>();
}
