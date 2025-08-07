using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200088A RID: 2186
[AddComponentMenu("KMonoBehaviour/scripts/UprootedMonitor")]
public class UprootedMonitor : KMonoBehaviour
{
	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x06003C25 RID: 15397 RVA: 0x0014D2AF File Offset: 0x0014B4AF
	public bool IsUprooted
	{
		get
		{
			return this.uprooted || base.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted);
		}
	}

	// Token: 0x06003C26 RID: 15398 RVA: 0x0014D2CB File Offset: 0x0014B4CB
	protected override void OnPrefabInit()
	{
		base.Subscribe<UprootedMonitor>(-216549700, UprootedMonitor.OnUprootedDelegate);
		this.position = Grid.PosToCell(base.gameObject);
		base.OnPrefabInit();
	}

	// Token: 0x06003C27 RID: 15399 RVA: 0x0014D2F5 File Offset: 0x0014B4F5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RegisterMonitoredCellsPartitionerEntries();
	}

	// Token: 0x06003C28 RID: 15400 RVA: 0x0014D303 File Offset: 0x0014B503
	public void SetNewMonitorCells(CellOffset[] cellsOffsets)
	{
		this.UnregisterMonitoredCellsPartitionerEntries();
		this.monitorCells = cellsOffsets;
		this.RegisterMonitoredCellsPartitionerEntries();
	}

	// Token: 0x06003C29 RID: 15401 RVA: 0x0014D318 File Offset: 0x0014B518
	private void UnregisterMonitoredCellsPartitionerEntries()
	{
		foreach (HandleVector<int>.Handle handle in this.partitionerEntries)
		{
			GameScenePartitioner.Instance.Free(ref handle);
		}
		this.partitionerEntries.Clear();
	}

	// Token: 0x06003C2A RID: 15402 RVA: 0x0014D37C File Offset: 0x0014B57C
	private void RegisterMonitoredCellsPartitionerEntries()
	{
		foreach (CellOffset cellOffset in this.monitorCells)
		{
			int num = Grid.OffsetCell(this.position, cellOffset);
			if (Grid.IsValidCell(this.position) && Grid.IsValidCell(num))
			{
				this.partitionerEntries.Add(GameScenePartitioner.Instance.Add("UprootedMonitor.OnSpawn", base.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnGroundChanged)));
			}
		}
		this.OnGroundChanged(null);
	}

	// Token: 0x06003C2B RID: 15403 RVA: 0x0014D406 File Offset: 0x0014B606
	protected override void OnCleanUp()
	{
		this.UnregisterMonitoredCellsPartitionerEntries();
		base.OnCleanUp();
	}

	// Token: 0x06003C2C RID: 15404 RVA: 0x0014D414 File Offset: 0x0014B614
	public bool CheckTileGrowable()
	{
		return !this.canBeUprooted || (!this.uprooted && this.IsSuitableFoundation(this.position));
	}

	// Token: 0x06003C2D RID: 15405 RVA: 0x0014D43C File Offset: 0x0014B63C
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
			if (this.customFoundationCheckFn != null)
			{
				flag = this.customFoundationCheckFn(num);
			}
			else
			{
				flag = Grid.Solid[num];
			}
			if (!flag)
			{
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003C2E RID: 15406 RVA: 0x0014D4A5 File Offset: 0x0014B6A5
	public void OnGroundChanged(object callbackData)
	{
		if (!this.CheckTileGrowable())
		{
			this.uprooted = true;
		}
		if (this.uprooted)
		{
			base.GetComponent<KPrefabID>().AddTag(GameTags.Uprooted, false);
			base.Trigger(-216549700, null);
		}
	}

	// Token: 0x040024E1 RID: 9441
	private int position;

	// Token: 0x040024E2 RID: 9442
	[Serialize]
	public bool canBeUprooted = true;

	// Token: 0x040024E3 RID: 9443
	[Serialize]
	private bool uprooted;

	// Token: 0x040024E4 RID: 9444
	public CellOffset[] monitorCells = new CellOffset[]
	{
		new CellOffset(0, -1)
	};

	// Token: 0x040024E5 RID: 9445
	public Func<int, bool> customFoundationCheckFn;

	// Token: 0x040024E6 RID: 9446
	private List<HandleVector<int>.Handle> partitionerEntries = new List<HandleVector<int>.Handle>();

	// Token: 0x040024E7 RID: 9447
	private static readonly EventSystem.IntraObjectHandler<UprootedMonitor> OnUprootedDelegate = new EventSystem.IntraObjectHandler<UprootedMonitor>(delegate(UprootedMonitor component, object data)
	{
		if (!component.uprooted)
		{
			component.GetComponent<KPrefabID>().AddTag(GameTags.Uprooted, false);
			component.uprooted = true;
			component.Trigger(-216549700, null);
		}
	});
}
