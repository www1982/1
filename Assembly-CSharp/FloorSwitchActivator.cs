using System;
using UnityEngine;

// Token: 0x02000922 RID: 2338
[AddComponentMenu("KMonoBehaviour/scripts/FloorSwitchActivator")]
public class FloorSwitchActivator : KMonoBehaviour
{
	// Token: 0x170004B0 RID: 1200
	// (get) Token: 0x06004136 RID: 16694 RVA: 0x0016E907 File Offset: 0x0016CB07
	public PrimaryElement PrimaryElement
	{
		get
		{
			return this.primaryElement;
		}
	}

	// Token: 0x06004137 RID: 16695 RVA: 0x0016E90F File Offset: 0x0016CB0F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Register();
		this.OnCellChange();
	}

	// Token: 0x06004138 RID: 16696 RVA: 0x0016E923 File Offset: 0x0016CB23
	protected override void OnCleanUp()
	{
		this.Unregister();
		base.OnCleanUp();
	}

	// Token: 0x06004139 RID: 16697 RVA: 0x0016E934 File Offset: 0x0016CB34
	private void OnCellChange()
	{
		int num = Grid.PosToCell(this);
		GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, num);
		if (Grid.IsValidCell(this.last_cell_occupied) && num != this.last_cell_occupied)
		{
			this.NotifyChanged(this.last_cell_occupied);
		}
		this.NotifyChanged(num);
		this.last_cell_occupied = num;
	}

	// Token: 0x0600413A RID: 16698 RVA: 0x0016E989 File Offset: 0x0016CB89
	private void NotifyChanged(int cell)
	{
		GameScenePartitioner.Instance.TriggerEvent(cell, GameScenePartitioner.Instance.floorSwitchActivatorChangedLayer, this);
	}

	// Token: 0x0600413B RID: 16699 RVA: 0x0016E9A1 File Offset: 0x0016CBA1
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.Register();
	}

	// Token: 0x0600413C RID: 16700 RVA: 0x0016E9AF File Offset: 0x0016CBAF
	protected override void OnCmpDisable()
	{
		this.Unregister();
		base.OnCmpDisable();
	}

	// Token: 0x0600413D RID: 16701 RVA: 0x0016E9C0 File Offset: 0x0016CBC0
	private void Register()
	{
		if (this.registered)
		{
			return;
		}
		int num = Grid.PosToCell(this);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("FloorSwitchActivator.Register", this, num, GameScenePartitioner.Instance.floorSwitchActivatorLayer, null);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "FloorSwitchActivator.Register");
		this.registered = true;
	}

	// Token: 0x0600413E RID: 16702 RVA: 0x0016EA28 File Offset: 0x0016CC28
	private void Unregister()
	{
		if (!this.registered)
		{
			return;
		}
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
		if (this.last_cell_occupied > -1)
		{
			this.NotifyChanged(this.last_cell_occupied);
		}
		this.registered = false;
	}

	// Token: 0x040028BE RID: 10430
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x040028BF RID: 10431
	private bool registered;

	// Token: 0x040028C0 RID: 10432
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040028C1 RID: 10433
	private int last_cell_occupied = -1;
}
