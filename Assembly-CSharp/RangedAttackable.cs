using System;
using UnityEngine;

// Token: 0x020005FE RID: 1534
public class RangedAttackable : AttackableBase
{
	// Token: 0x06002456 RID: 9302 RVA: 0x000CF343 File Offset: 0x000CD543
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002457 RID: 9303 RVA: 0x000CF34B File Offset: 0x000CD54B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.preferUnreservedCell = true;
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06002458 RID: 9304 RVA: 0x000CF365 File Offset: 0x000CD565
	public new int GetCell()
	{
		return Grid.PosToCell(this);
	}

	// Token: 0x06002459 RID: 9305 RVA: 0x000CF370 File Offset: 0x000CD570
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0f, 0.5f, 0.5f, 0.15f);
		foreach (CellOffset cellOffset in base.GetOffsets())
		{
			Gizmos.DrawCube(new Vector3(0.5f, 0.5f, 0f) + Grid.CellToPos(Grid.OffsetCell(Grid.PosToCell(base.gameObject), cellOffset)), Vector3.one);
		}
	}
}
