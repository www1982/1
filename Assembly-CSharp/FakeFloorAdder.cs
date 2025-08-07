using System;

// Token: 0x02000916 RID: 2326
public class FakeFloorAdder : KMonoBehaviour
{
	// Token: 0x060040A6 RID: 16550 RVA: 0x0016A700 File Offset: 0x00168900
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.initiallyActive)
		{
			this.SetFloor(true);
		}
	}

	// Token: 0x060040A7 RID: 16551 RVA: 0x0016A718 File Offset: 0x00168918
	public void SetFloor(bool active)
	{
		if (this.isActive == active)
		{
			return;
		}
		int num = Grid.PosToCell(this);
		Rotatable component = base.GetComponent<Rotatable>();
		foreach (CellOffset cellOffset in this.floorOffsets)
		{
			CellOffset cellOffset2 = ((component == null) ? cellOffset : component.GetRotatedCellOffset(cellOffset));
			int num2 = Grid.OffsetCell(num, cellOffset2);
			if (active)
			{
				Grid.FakeFloor.Add(num2);
			}
			else
			{
				Grid.FakeFloor.Remove(num2);
			}
			Pathfinding.Instance.AddDirtyNavGridCell(num2);
		}
		this.isActive = active;
	}

	// Token: 0x060040A8 RID: 16552 RVA: 0x0016A7AC File Offset: 0x001689AC
	protected override void OnCleanUp()
	{
		this.SetFloor(false);
		base.OnCleanUp();
	}

	// Token: 0x0400285B RID: 10331
	public CellOffset[] floorOffsets;

	// Token: 0x0400285C RID: 10332
	public bool initiallyActive = true;

	// Token: 0x0400285D RID: 10333
	private bool isActive;
}
