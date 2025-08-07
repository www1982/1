using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009CC RID: 2508
[AddComponentMenu("KMonoBehaviour/scripts/MinionGroupProber")]
public class MinionGroupProber : KMonoBehaviour, IGroupProber, ISim200ms
{
	// Token: 0x06004942 RID: 18754 RVA: 0x001A7578 File Offset: 0x001A5778
	public static void DestroyInstance()
	{
		MinionGroupProber.Instance = null;
	}

	// Token: 0x06004943 RID: 18755 RVA: 0x001A7580 File Offset: 0x001A5780
	public static MinionGroupProber Get()
	{
		return MinionGroupProber.Instance;
	}

	// Token: 0x06004944 RID: 18756 RVA: 0x001A7588 File Offset: 0x001A5788
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MinionGroupProber.Instance = this;
		this.cells = new Dictionary<object, short>[Grid.CellCount];
		for (int i = 0; i < Grid.CellCount; i++)
		{
			this.cells[i] = new Dictionary<object, short>();
		}
		this.cell_cleanup_index = 0;
		this.cell_checks_per_frame = Grid.CellCount / 500;
	}

	// Token: 0x06004945 RID: 18757 RVA: 0x001A75E8 File Offset: 0x001A57E8
	public bool IsReachable(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		foreach (KeyValuePair<object, short> keyValuePair in this.cells[cell])
		{
			object key = keyValuePair.Key;
			short value = keyValuePair.Value;
			KeyValuePair<short, short> keyValuePair2;
			if (this.valid_serial_nos.TryGetValue(key, out keyValuePair2) && (value == keyValuePair2.Key || value == keyValuePair2.Value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004946 RID: 18758 RVA: 0x001A7680 File Offset: 0x001A5880
	public bool IsReachable(int cell, CellOffset[] offsets)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		foreach (CellOffset cellOffset in offsets)
		{
			if (this.IsReachable(Grid.OffsetCell(cell, cellOffset)))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004947 RID: 18759 RVA: 0x001A76C4 File Offset: 0x001A58C4
	public bool IsAllReachable(int cell, CellOffset[] offsets)
	{
		if (this.IsReachable(cell))
		{
			return true;
		}
		foreach (CellOffset cellOffset in offsets)
		{
			if (this.IsReachable(Grid.OffsetCell(cell, cellOffset)))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004948 RID: 18760 RVA: 0x001A7706 File Offset: 0x001A5906
	public bool IsReachable(Workable workable)
	{
		return this.IsReachable(Grid.PosToCell(workable), workable.GetOffsets());
	}

	// Token: 0x06004949 RID: 18761 RVA: 0x001A771C File Offset: 0x001A591C
	public void Occupy(object prober, short serial_no, IEnumerable<int> cells)
	{
		foreach (int num in cells)
		{
			Dictionary<object, short> dictionary = this.cells[num];
			lock (dictionary)
			{
				this.cells[num][prober] = serial_no;
			}
		}
	}

	// Token: 0x0600494A RID: 18762 RVA: 0x001A7798 File Offset: 0x001A5998
	public void SetValidSerialNos(object prober, short previous_serial_no, short serial_no)
	{
		object obj = this.access;
		lock (obj)
		{
			this.valid_serial_nos[prober] = new KeyValuePair<short, short>(previous_serial_no, serial_no);
		}
	}

	// Token: 0x0600494B RID: 18763 RVA: 0x001A77E8 File Offset: 0x001A59E8
	public bool ReleaseProber(object prober)
	{
		object obj = this.access;
		bool flag2;
		lock (obj)
		{
			flag2 = this.valid_serial_nos.Remove(prober);
		}
		return flag2;
	}

	// Token: 0x0600494C RID: 18764 RVA: 0x001A7830 File Offset: 0x001A5A30
	public void Sim200ms(float dt)
	{
		int i = 0;
		while (i < this.cell_checks_per_frame)
		{
			this.pending_removals.Clear();
			foreach (KeyValuePair<object, short> keyValuePair in this.cells[this.cell_cleanup_index])
			{
				KeyValuePair<short, short> keyValuePair2;
				if (!this.valid_serial_nos.TryGetValue(keyValuePair.Key, out keyValuePair2) || (keyValuePair2.Key != keyValuePair.Value && keyValuePair2.Value != keyValuePair.Value))
				{
					this.pending_removals.Add(keyValuePair.Key);
				}
			}
			foreach (object obj in this.pending_removals)
			{
				this.cells[this.cell_cleanup_index].Remove(obj);
			}
			i++;
			this.cell_cleanup_index = (this.cell_cleanup_index + 1) % this.cells.Length;
		}
	}

	// Token: 0x04003048 RID: 12360
	private static MinionGroupProber Instance;

	// Token: 0x04003049 RID: 12361
	private Dictionary<object, short>[] cells;

	// Token: 0x0400304A RID: 12362
	private Dictionary<object, KeyValuePair<short, short>> valid_serial_nos = new Dictionary<object, KeyValuePair<short, short>>();

	// Token: 0x0400304B RID: 12363
	private List<object> pending_removals = new List<object>();

	// Token: 0x0400304C RID: 12364
	private int cell_cleanup_index;

	// Token: 0x0400304D RID: 12365
	private int cell_checks_per_frame;

	// Token: 0x0400304E RID: 12366
	private readonly object access = new object();
}
