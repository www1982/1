using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200086E RID: 2158
[AddComponentMenu("KMonoBehaviour/scripts/FoundationMonitor")]
public class FoundationMonitor : KMonoBehaviour
{
	// Token: 0x06003B49 RID: 15177 RVA: 0x001490DC File Offset: 0x001472DC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.position = Grid.PosToCell(base.gameObject);
		foreach (CellOffset cellOffset in this.monitorCells)
		{
			int num = Grid.OffsetCell(this.position, cellOffset);
			if (Grid.IsValidCell(this.position) && Grid.IsValidCell(num))
			{
				this.partitionerEntries.Add(GameScenePartitioner.Instance.Add("FoundationMonitor.OnSpawn", base.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnGroundChanged)));
			}
			this.OnGroundChanged(null);
		}
	}

	// Token: 0x06003B4A RID: 15178 RVA: 0x00149180 File Offset: 0x00147380
	protected override void OnCleanUp()
	{
		foreach (HandleVector<int>.Handle handle in this.partitionerEntries)
		{
			GameScenePartitioner.Instance.Free(ref handle);
		}
		base.OnCleanUp();
	}

	// Token: 0x06003B4B RID: 15179 RVA: 0x001491E0 File Offset: 0x001473E0
	public bool CheckFoundationValid()
	{
		return !this.needsFoundation || this.IsSuitableFoundation(this.position);
	}

	// Token: 0x06003B4C RID: 15180 RVA: 0x001491F8 File Offset: 0x001473F8
	public bool IsSuitableFoundation(int cell)
	{
		bool flag = true;
		foreach (CellOffset cellOffset in this.monitorCells)
		{
			if (!Grid.IsCellOffsetValid(cell, cellOffset))
			{
				return false;
			}
			int num = Grid.OffsetCell(cell, cellOffset);
			flag = Grid.Solid[num];
			if (!flag)
			{
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003B4D RID: 15181 RVA: 0x0014924C File Offset: 0x0014744C
	public void OnGroundChanged(object callbackData)
	{
		if (!this.hasFoundation && this.CheckFoundationValid())
		{
			this.hasFoundation = true;
			base.GetComponent<KPrefabID>().RemoveTag(GameTags.Creatures.HasNoFoundation);
			base.Trigger(-1960061727, null);
		}
		if (this.hasFoundation && !this.CheckFoundationValid())
		{
			this.hasFoundation = false;
			base.GetComponent<KPrefabID>().AddTag(GameTags.Creatures.HasNoFoundation, false);
			base.Trigger(-1960061727, null);
		}
	}

	// Token: 0x0400245B RID: 9307
	private int position;

	// Token: 0x0400245C RID: 9308
	[Serialize]
	public bool needsFoundation = true;

	// Token: 0x0400245D RID: 9309
	[Serialize]
	private bool hasFoundation = true;

	// Token: 0x0400245E RID: 9310
	public CellOffset[] monitorCells = new CellOffset[]
	{
		new CellOffset(0, -1)
	};

	// Token: 0x0400245F RID: 9311
	private List<HandleVector<int>.Handle> partitionerEntries = new List<HandleVector<int>.Handle>();
}
