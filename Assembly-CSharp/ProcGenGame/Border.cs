using System;
using System.Collections.Generic;
using ProcGen;
using UnityEngine;

namespace ProcGenGame
{
	// Token: 0x02000E9A RID: 3738
	public class Border : Path
	{
		// Token: 0x0600771D RID: 30493 RVA: 0x002DD02C File Offset: 0x002DB22C
		public Border(Neighbors neighbors, Vector2 e0, Vector2 e1)
		{
			this.neighbors = neighbors;
			Vector2 vector = e1 - e0;
			Vector2 normalized = new Vector2(-vector.y, vector.x).normalized;
			Vector2 vector2 = e0 + vector / 2f + normalized;
			if (neighbors.n0.poly.Contains(vector2))
			{
				base.AddSegment(e0, e1);
				return;
			}
			base.AddSegment(e1, e0);
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x002DD0A8 File Offset: 0x002DB2A8
		private void SetCell(int gridCell, float defaultTemperature, TerrainCell.SetValuesFunction SetValues, SeededRandom rnd)
		{
			WeightedSimHash weightedSimHash = WeightedRandom.Choose<WeightedSimHash>(this.element, rnd);
			TerrainCell.ElementOverride elementOverride = TerrainCell.GetElementOverride(weightedSimHash.element, weightedSimHash.overrides);
			if (!elementOverride.overrideTemperature)
			{
				elementOverride.pdelement.temperature = defaultTemperature;
			}
			SetValues(gridCell, elementOverride.element, elementOverride.pdelement, elementOverride.dc);
		}

		// Token: 0x0600771F RID: 30495 RVA: 0x002DD104 File Offset: 0x002DB304
		public void ConvertToMap(Chunk world, TerrainCell.SetValuesFunction SetValues, float neighbour0Temperature, float neighbour1Temperature, float midTemp, SeededRandom rnd, int snapLastCells)
		{
			for (int i = 0; i < this.pathElements.Count; i++)
			{
				Vector2 vector = this.pathElements[i].e1 - this.pathElements[i].e0;
				Vector2 normalized = new Vector2(-vector.y, vector.x).normalized;
				List<Vector2I> line = global::ProcGen.Util.GetLine(this.pathElements[i].e0, this.pathElements[i].e1);
				for (int j = 0; j < line.Count; j++)
				{
					int num = Grid.XYToCell(line[j].x, line[j].y);
					if (Grid.IsValidCell(num))
					{
						this.SetCell(num, midTemp, SetValues, rnd);
					}
					for (float num2 = 0.5f; num2 <= this.width; num2 += 1f)
					{
						float num3 = Mathf.Clamp01((num2 - 0.5f) / (this.width - 0.5f));
						if (num2 + (float)snapLastCells > this.width)
						{
							num3 = 1f;
						}
						Vector2 vector2 = line[j] + normalized * num2;
						float num4 = midTemp + (neighbour0Temperature - midTemp) * num3;
						num = Grid.XYToCell((int)vector2.x, (int)vector2.y);
						if (Grid.IsValidCell(num))
						{
							this.SetCell(num, num4, SetValues, rnd);
						}
						Vector2 vector3 = line[j] - normalized * num2;
						float num5 = midTemp + (neighbour1Temperature - midTemp) * num3;
						num = Grid.XYToCell((int)vector3.x, (int)vector3.y);
						if (Grid.IsValidCell(num))
						{
							this.SetCell(num, num5, SetValues, rnd);
						}
					}
				}
			}
		}

		// Token: 0x040052DB RID: 21211
		public Neighbors neighbors;

		// Token: 0x040052DC RID: 21212
		public List<WeightedSimHash> element;

		// Token: 0x040052DD RID: 21213
		public float width;
	}
}
