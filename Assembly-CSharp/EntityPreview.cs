using System;
using UnityEngine;

// Token: 0x02000867 RID: 2151
[AddComponentMenu("KMonoBehaviour/scripts/EntityPreview")]
public class EntityPreview : KMonoBehaviour
{
	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x06003B07 RID: 15111 RVA: 0x001480AD File Offset: 0x001462AD
	// (set) Token: 0x06003B08 RID: 15112 RVA: 0x001480B5 File Offset: 0x001462B5
	public bool Valid { get; private set; }

	// Token: 0x06003B09 RID: 15113 RVA: 0x001480C0 File Offset: 0x001462C0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("EntityPreview", base.gameObject, this.occupyArea.GetExtents(), GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnAreaChanged));
		if (this.objectLayer != ObjectLayer.NumLayers)
		{
			this.objectPartitionerEntry = GameScenePartitioner.Instance.Add("EntityPreview", base.gameObject, this.occupyArea.GetExtents(), GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnAreaChanged));
		}
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "EntityPreview.OnSpawn");
		this.OnAreaChanged(null);
	}

	// Token: 0x06003B0A RID: 15114 RVA: 0x00148188 File Offset: 0x00146388
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.objectPartitionerEntry);
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
		base.OnCleanUp();
	}

	// Token: 0x06003B0B RID: 15115 RVA: 0x001481D7 File Offset: 0x001463D7
	private void OnCellChange()
	{
		GameScenePartitioner.Instance.UpdatePosition(this.solidPartitionerEntry, this.occupyArea.GetExtents());
		GameScenePartitioner.Instance.UpdatePosition(this.objectPartitionerEntry, this.occupyArea.GetExtents());
		this.OnAreaChanged(null);
	}

	// Token: 0x06003B0C RID: 15116 RVA: 0x00148216 File Offset: 0x00146416
	public void SetSolid()
	{
		this.occupyArea.ApplyToCells = true;
	}

	// Token: 0x06003B0D RID: 15117 RVA: 0x00148224 File Offset: 0x00146424
	private void OnAreaChanged(object obj)
	{
		this.UpdateValidity();
	}

	// Token: 0x06003B0E RID: 15118 RVA: 0x0014822C File Offset: 0x0014642C
	public void UpdateValidity()
	{
		bool valid = this.Valid;
		this.Valid = this.occupyArea.TestArea(Grid.PosToCell(this), this, EntityPreview.ValidTestDelegate);
		if (this.Valid)
		{
			this.animController.TintColour = Color.white;
		}
		else
		{
			this.animController.TintColour = Color.red;
		}
		if (valid != this.Valid)
		{
			base.Trigger(-1820564715, this.Valid);
		}
	}

	// Token: 0x06003B0F RID: 15119 RVA: 0x001482B0 File Offset: 0x001464B0
	private static bool ValidTest(int cell, object data)
	{
		EntityPreview entityPreview = (EntityPreview)data;
		return Grid.IsValidCell(cell) && !Grid.Solid[cell] && (entityPreview.objectLayer == ObjectLayer.NumLayers || Grid.Objects[cell, (int)entityPreview.objectLayer] == entityPreview.gameObject || Grid.Objects[cell, (int)entityPreview.objectLayer] == null);
	}

	// Token: 0x04002437 RID: 9271
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x04002438 RID: 9272
	[MyCmpReq]
	private KBatchedAnimController animController;

	// Token: 0x04002439 RID: 9273
	[MyCmpGet]
	private Storage storage;

	// Token: 0x0400243A RID: 9274
	public ObjectLayer objectLayer = ObjectLayer.NumLayers;

	// Token: 0x0400243C RID: 9276
	private HandleVector<int>.Handle solidPartitionerEntry;

	// Token: 0x0400243D RID: 9277
	private HandleVector<int>.Handle objectPartitionerEntry;

	// Token: 0x0400243E RID: 9278
	private static readonly Func<int, object, bool> ValidTestDelegate = (int cell, object data) => EntityPreview.ValidTest(cell, data);
}
