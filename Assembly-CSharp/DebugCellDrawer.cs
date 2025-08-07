using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000894 RID: 2196
[AddComponentMenu("KMonoBehaviour/scripts/DebugCellDrawer")]
public class DebugCellDrawer : KMonoBehaviour
{
	// Token: 0x06003CB4 RID: 15540 RVA: 0x00151094 File Offset: 0x0014F294
	private void Update()
	{
		for (int i = 0; i < this.cells.Count; i++)
		{
			if (this.cells[i] != PathFinder.InvalidCell)
			{
				DebugExtension.DebugPoint(Grid.CellToPosCCF(this.cells[i], Grid.SceneLayer.Background), 1f, 0f, true);
			}
		}
	}

	// Token: 0x0400252F RID: 9519
	public List<int> cells;
}
