using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005B6 RID: 1462
[AddComponentMenu("KMonoBehaviour/scripts/Facing")]
public class Facing : KMonoBehaviour
{
	// Token: 0x060021BA RID: 8634 RVA: 0x000C32F6 File Offset: 0x000C14F6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.log = new LoggerFS("Facing", 35);
	}

	// Token: 0x060021BB RID: 8635 RVA: 0x000C3310 File Offset: 0x000C1510
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateMirror();
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x000C3320 File Offset: 0x000C1520
	public void Face(float target_x)
	{
		float x = base.transform.GetLocalPosition().x;
		if (target_x < x)
		{
			this.SetFacing(true);
			return;
		}
		if (target_x > x)
		{
			this.SetFacing(false);
		}
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x000C3358 File Offset: 0x000C1558
	public void Face(Vector3 target_pos)
	{
		int num = Grid.CellColumn(Grid.PosToCell(base.transform.GetLocalPosition()));
		int num2 = Grid.CellColumn(Grid.PosToCell(target_pos));
		if (num > num2)
		{
			this.SetFacing(true);
			return;
		}
		if (num2 > num)
		{
			this.SetFacing(false);
		}
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x000C339E File Offset: 0x000C159E
	[ContextMenu("Flip")]
	public void SwapFacing()
	{
		this.SetFacing(!this.facingLeft);
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x000C33AF File Offset: 0x000C15AF
	private void UpdateMirror()
	{
		if (this.kanimController != null && this.kanimController.FlipX != this.facingLeft)
		{
			this.kanimController.FlipX = this.facingLeft;
			bool flag = this.facingLeft;
		}
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x000C33EA File Offset: 0x000C15EA
	public bool GetFacing()
	{
		return this.facingLeft;
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x000C33F2 File Offset: 0x000C15F2
	public void SetFacing(bool mirror_x)
	{
		this.facingLeft = mirror_x;
		this.UpdateMirror();
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x000C3404 File Offset: 0x000C1604
	public int GetFrontCell()
	{
		int num = Grid.PosToCell(this);
		if (this.GetFacing())
		{
			return Grid.CellLeft(num);
		}
		return Grid.CellRight(num);
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x000C3430 File Offset: 0x000C1630
	public int GetBackCell()
	{
		int num = Grid.PosToCell(this);
		if (!this.GetFacing())
		{
			return Grid.CellLeft(num);
		}
		return Grid.CellRight(num);
	}

	// Token: 0x040013AB RID: 5035
	[MyCmpGet]
	private KAnimControllerBase kanimController;

	// Token: 0x040013AC RID: 5036
	private LoggerFS log;

	// Token: 0x040013AD RID: 5037
	[Serialize]
	public bool facingLeft;
}
