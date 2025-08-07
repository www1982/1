using System;
using System.Collections.Generic;
using ProcGenGame;
using UnityEngine;

// Token: 0x02000E8F RID: 3727
[AddComponentMenu("KMonoBehaviour/scripts/CavityVisualizer")]
public class CavityVisualizer : KMonoBehaviour
{
	// Token: 0x060076C0 RID: 30400 RVA: 0x002D7690 File Offset: 0x002D5890
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		foreach (TerrainCell terrainCell in MobSpawning.NaturalCavities.Keys)
		{
			foreach (HashSet<int> hashSet in MobSpawning.NaturalCavities[terrainCell])
			{
				foreach (int num in hashSet)
				{
					this.cavityCells.Add(num);
				}
			}
		}
	}

	// Token: 0x060076C1 RID: 30401 RVA: 0x002D7768 File Offset: 0x002D5968
	private void OnDrawGizmosSelected()
	{
		if (this.drawCavity)
		{
			Color[] array = new Color[]
			{
				Color.blue,
				Color.yellow
			};
			int num = 0;
			foreach (TerrainCell terrainCell in MobSpawning.NaturalCavities.Keys)
			{
				Gizmos.color = array[num % array.Length];
				Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.125f);
				num++;
				foreach (HashSet<int> hashSet in MobSpawning.NaturalCavities[terrainCell])
				{
					foreach (int num2 in hashSet)
					{
						Gizmos.DrawCube(Grid.CellToPos(num2) + (Vector3.right / 2f + Vector3.up / 2f), Vector3.one);
					}
				}
			}
		}
		if (this.spawnCells != null && this.drawSpawnCells)
		{
			Gizmos.color = new Color(0f, 1f, 0f, 0.15f);
			foreach (int num3 in this.spawnCells)
			{
				Gizmos.DrawCube(Grid.CellToPos(num3) + (Vector3.right / 2f + Vector3.up / 2f), Vector3.one);
			}
		}
	}

	// Token: 0x0400529E RID: 21150
	public List<int> cavityCells = new List<int>();

	// Token: 0x0400529F RID: 21151
	public List<int> spawnCells = new List<int>();

	// Token: 0x040052A0 RID: 21152
	public bool drawCavity = true;

	// Token: 0x040052A1 RID: 21153
	public bool drawSpawnCells = true;
}
