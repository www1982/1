using System;
using TUNING;

// Token: 0x02000797 RID: 1943
public class NavTeleporter : KMonoBehaviour
{
	// Token: 0x0600334F RID: 13135 RVA: 0x00121118 File Offset: 0x0011F318
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<KPrefabID>().AddTag(GameTags.NavTeleporters, false);
		this.Register();
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChanged), "NavTeleporterCellChanged");
	}

	// Token: 0x06003350 RID: 13136 RVA: 0x00121164 File Offset: 0x0011F364
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		int cell = this.GetCell();
		if (cell != Grid.InvalidCell)
		{
			Grid.HasNavTeleporter[cell] = false;
		}
		this.Deregister();
		Components.NavTeleporters.Remove(this);
	}

	// Token: 0x06003351 RID: 13137 RVA: 0x001211A3 File Offset: 0x0011F3A3
	public void SetOverrideCell(int cell)
	{
		this.overrideCell = cell;
	}

	// Token: 0x06003352 RID: 13138 RVA: 0x001211AC File Offset: 0x0011F3AC
	public int GetCell()
	{
		if (this.overrideCell >= 0)
		{
			return this.overrideCell;
		}
		return Grid.OffsetCell(Grid.PosToCell(this), this.offset);
	}

	// Token: 0x06003353 RID: 13139 RVA: 0x001211D0 File Offset: 0x0011F3D0
	public void TwoWayTarget(NavTeleporter nt)
	{
		if (this.target != null)
		{
			if (nt != null)
			{
				nt.SetTarget(null);
			}
			this.BreakLink();
		}
		this.target = nt;
		if (this.target != null)
		{
			this.SetLink();
			if (nt != null)
			{
				nt.SetTarget(this);
			}
		}
	}

	// Token: 0x06003354 RID: 13140 RVA: 0x0012122C File Offset: 0x0011F42C
	public void EnableTwoWayTarget(bool enable)
	{
		if (enable)
		{
			this.target.SetLink();
			this.SetLink();
			return;
		}
		this.target.BreakLink();
		this.BreakLink();
	}

	// Token: 0x06003355 RID: 13141 RVA: 0x00121254 File Offset: 0x0011F454
	public void SetTarget(NavTeleporter nt)
	{
		if (this.target != null)
		{
			this.BreakLink();
		}
		this.target = nt;
		if (this.target != null)
		{
			this.SetLink();
		}
	}

	// Token: 0x06003356 RID: 13142 RVA: 0x00121288 File Offset: 0x0011F488
	private void Register()
	{
		int cell = this.GetCell();
		if (!Grid.IsValidCell(cell))
		{
			this.lastRegisteredCell = Grid.InvalidCell;
			return;
		}
		Grid.HasNavTeleporter[cell] = true;
		Pathfinding.Instance.AddDirtyNavGridCell(cell);
		this.lastRegisteredCell = cell;
		if (this.target != null)
		{
			this.SetLink();
		}
	}

	// Token: 0x06003357 RID: 13143 RVA: 0x001212E4 File Offset: 0x0011F4E4
	private void SetLink()
	{
		int cell = this.target.GetCell();
		Pathfinding.Instance.GetNavGrid(DUPLICANTSTATS.STANDARD.BaseStats.NAV_GRID_NAME).teleportTransitions[this.lastRegisteredCell] = cell;
		Pathfinding.Instance.AddDirtyNavGridCell(this.lastRegisteredCell);
	}

	// Token: 0x06003358 RID: 13144 RVA: 0x00121338 File Offset: 0x0011F538
	public void Deregister()
	{
		if (this.lastRegisteredCell != Grid.InvalidCell)
		{
			this.BreakLink();
			Grid.HasNavTeleporter[this.lastRegisteredCell] = false;
			Pathfinding.Instance.AddDirtyNavGridCell(this.lastRegisteredCell);
			this.lastRegisteredCell = Grid.InvalidCell;
		}
	}

	// Token: 0x06003359 RID: 13145 RVA: 0x00121384 File Offset: 0x0011F584
	private void BreakLink()
	{
		Pathfinding.Instance.GetNavGrid(DUPLICANTSTATS.STANDARD.BaseStats.NAV_GRID_NAME).teleportTransitions.Remove(this.lastRegisteredCell);
		Pathfinding.Instance.AddDirtyNavGridCell(this.lastRegisteredCell);
	}

	// Token: 0x0600335A RID: 13146 RVA: 0x001213C0 File Offset: 0x0011F5C0
	private void OnCellChanged()
	{
		this.Deregister();
		this.Register();
		if (this.target != null)
		{
			NavTeleporter component = this.target.GetComponent<NavTeleporter>();
			if (component != null)
			{
				component.SetTarget(this);
			}
		}
	}

	// Token: 0x04001ED7 RID: 7895
	private NavTeleporter target;

	// Token: 0x04001ED8 RID: 7896
	private int lastRegisteredCell = Grid.InvalidCell;

	// Token: 0x04001ED9 RID: 7897
	public CellOffset offset;

	// Token: 0x04001EDA RID: 7898
	private int overrideCell = -1;
}
