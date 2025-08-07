using System;
using UnityEngine;

// Token: 0x02000BCD RID: 3021
public abstract class UtilityNetworkLink : KMonoBehaviour
{
	// Token: 0x06005A6E RID: 23150 RVA: 0x0020B24A File Offset: 0x0020944A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<UtilityNetworkLink>(774203113, UtilityNetworkLink.OnBuildingBrokenDelegate);
		base.Subscribe<UtilityNetworkLink>(-1735440190, UtilityNetworkLink.OnBuildingFullyRepairedDelegate);
		this.Connect();
	}

	// Token: 0x06005A6F RID: 23151 RVA: 0x0020B27A File Offset: 0x0020947A
	protected override void OnCleanUp()
	{
		base.Unsubscribe<UtilityNetworkLink>(774203113, UtilityNetworkLink.OnBuildingBrokenDelegate, false);
		base.Unsubscribe<UtilityNetworkLink>(-1735440190, UtilityNetworkLink.OnBuildingFullyRepairedDelegate, false);
		this.Disconnect();
		base.OnCleanUp();
	}

	// Token: 0x06005A70 RID: 23152 RVA: 0x0020B2AC File Offset: 0x002094AC
	protected void Connect()
	{
		if (!this.visualizeOnly && !this.connected)
		{
			this.connected = true;
			int num;
			int num2;
			this.GetCells(out num, out num2);
			this.OnConnect(num, num2);
		}
	}

	// Token: 0x06005A71 RID: 23153 RVA: 0x0020B2E2 File Offset: 0x002094E2
	protected virtual void OnConnect(int cell1, int cell2)
	{
	}

	// Token: 0x06005A72 RID: 23154 RVA: 0x0020B2E4 File Offset: 0x002094E4
	protected void Disconnect()
	{
		if (!this.visualizeOnly && this.connected)
		{
			this.connected = false;
			int num;
			int num2;
			this.GetCells(out num, out num2);
			this.OnDisconnect(num, num2);
		}
	}

	// Token: 0x06005A73 RID: 23155 RVA: 0x0020B31A File Offset: 0x0020951A
	protected virtual void OnDisconnect(int cell1, int cell2)
	{
	}

	// Token: 0x06005A74 RID: 23156 RVA: 0x0020B31C File Offset: 0x0020951C
	public void GetCells(out int linked_cell1, out int linked_cell2)
	{
		Building component = base.GetComponent<Building>();
		if (component != null)
		{
			Orientation orientation = component.Orientation;
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.GetCells(num, orientation, out linked_cell1, out linked_cell2);
			return;
		}
		linked_cell1 = -1;
		linked_cell2 = -1;
	}

	// Token: 0x06005A75 RID: 23157 RVA: 0x0020B364 File Offset: 0x00209564
	public void GetCells(int cell, Orientation orientation, out int linked_cell1, out int linked_cell2)
	{
		CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.link1, orientation);
		CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(this.link2, orientation);
		linked_cell1 = Grid.OffsetCell(cell, rotatedCellOffset);
		linked_cell2 = Grid.OffsetCell(cell, rotatedCellOffset2);
	}

	// Token: 0x06005A76 RID: 23158 RVA: 0x0020B3A0 File Offset: 0x002095A0
	public bool AreCellsValid(int cell, Orientation orientation)
	{
		CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(this.link1, orientation);
		CellOffset rotatedCellOffset2 = Rotatable.GetRotatedCellOffset(this.link2, orientation);
		return Grid.IsCellOffsetValid(cell, rotatedCellOffset) && Grid.IsCellOffsetValid(cell, rotatedCellOffset2);
	}

	// Token: 0x06005A77 RID: 23159 RVA: 0x0020B3D9 File Offset: 0x002095D9
	private void OnBuildingBroken(object data)
	{
		this.Disconnect();
	}

	// Token: 0x06005A78 RID: 23160 RVA: 0x0020B3E1 File Offset: 0x002095E1
	private void OnBuildingFullyRepaired(object data)
	{
		this.Connect();
	}

	// Token: 0x06005A79 RID: 23161 RVA: 0x0020B3EC File Offset: 0x002095EC
	public int GetNetworkCell()
	{
		int num;
		int num2;
		this.GetCells(out num, out num2);
		return num;
	}

	// Token: 0x04003C06 RID: 15366
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04003C07 RID: 15367
	[SerializeField]
	public CellOffset link1;

	// Token: 0x04003C08 RID: 15368
	[SerializeField]
	public CellOffset link2;

	// Token: 0x04003C09 RID: 15369
	[SerializeField]
	public bool visualizeOnly;

	// Token: 0x04003C0A RID: 15370
	private bool connected;

	// Token: 0x04003C0B RID: 15371
	private static readonly EventSystem.IntraObjectHandler<UtilityNetworkLink> OnBuildingBrokenDelegate = new EventSystem.IntraObjectHandler<UtilityNetworkLink>(delegate(UtilityNetworkLink component, object data)
	{
		component.OnBuildingBroken(data);
	});

	// Token: 0x04003C0C RID: 15372
	private static readonly EventSystem.IntraObjectHandler<UtilityNetworkLink> OnBuildingFullyRepairedDelegate = new EventSystem.IntraObjectHandler<UtilityNetworkLink>(delegate(UtilityNetworkLink component, object data)
	{
		component.OnBuildingFullyRepaired(data);
	});
}
