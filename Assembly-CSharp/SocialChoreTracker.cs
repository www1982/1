using System;
using UnityEngine;

// Token: 0x02000A41 RID: 2625
public class SocialChoreTracker
{
	// Token: 0x06004C3D RID: 19517 RVA: 0x001BA61C File Offset: 0x001B881C
	public SocialChoreTracker(GameObject owner, CellOffset[] chore_offsets)
	{
		this.owner = owner;
		this.choreOffsets = chore_offsets;
		this.chores = new Chore[this.choreOffsets.Length];
		Extents extents = new Extents(Grid.PosToCell(owner), this.choreOffsets);
		this.validNavCellChangedPartitionerEntry = GameScenePartitioner.Instance.Add("PrintingPodSocialize", owner, extents, GameScenePartitioner.Instance.validNavCellChangedLayer, new Action<object>(this.OnCellChanged));
	}

	// Token: 0x06004C3E RID: 19518 RVA: 0x001BA690 File Offset: 0x001B8890
	public void Update(bool update = true)
	{
		if (this.updating)
		{
			return;
		}
		this.updating = true;
		int num = 0;
		for (int i = 0; i < this.choreOffsets.Length; i++)
		{
			CellOffset cellOffset = this.choreOffsets[i];
			Chore chore = this.chores[i];
			if (update && num < this.choreCount && this.IsOffsetValid(cellOffset))
			{
				num++;
				if (chore == null || chore.isComplete)
				{
					this.chores[i] = ((this.CreateChoreCB != null) ? this.CreateChoreCB(i) : null);
				}
			}
			else if (chore != null)
			{
				chore.Cancel("locator invalidated");
				this.chores[i] = null;
			}
		}
		this.updating = false;
	}

	// Token: 0x06004C3F RID: 19519 RVA: 0x001BA741 File Offset: 0x001B8941
	private void OnCellChanged(object data)
	{
		if (this.owner.HasTag(GameTags.Operational))
		{
			this.Update(true);
		}
	}

	// Token: 0x06004C40 RID: 19520 RVA: 0x001BA75C File Offset: 0x001B895C
	public void Clear()
	{
		GameScenePartitioner.Instance.Free(ref this.validNavCellChangedPartitionerEntry);
		this.Update(false);
	}

	// Token: 0x06004C41 RID: 19521 RVA: 0x001BA778 File Offset: 0x001B8978
	private bool IsOffsetValid(CellOffset offset)
	{
		int num = Grid.OffsetCell(Grid.PosToCell(this.owner), offset);
		int num2 = Grid.CellBelow(num);
		return GameNavGrids.FloorValidator.IsWalkableCell(num, num2, true);
	}

	// Token: 0x04003285 RID: 12933
	public Func<int, Chore> CreateChoreCB;

	// Token: 0x04003286 RID: 12934
	public int choreCount;

	// Token: 0x04003287 RID: 12935
	private GameObject owner;

	// Token: 0x04003288 RID: 12936
	private CellOffset[] choreOffsets;

	// Token: 0x04003289 RID: 12937
	private Chore[] chores;

	// Token: 0x0400328A RID: 12938
	private HandleVector<int>.Handle validNavCellChangedPartitionerEntry;

	// Token: 0x0400328B RID: 12939
	private bool updating;
}
