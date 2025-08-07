using System;
using KSerialization;
using STRINGS;

// Token: 0x02000868 RID: 2152
public class EntombVulnerable : KMonoBehaviour, IWiltCause
{
	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x06003B12 RID: 15122 RVA: 0x00148345 File Offset: 0x00146545
	private OccupyArea occupyArea
	{
		get
		{
			if (this._occupyArea == null)
			{
				this._occupyArea = base.GetComponent<OccupyArea>();
			}
			return this._occupyArea;
		}
	}

	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x06003B13 RID: 15123 RVA: 0x00148367 File Offset: 0x00146567
	public bool GetEntombed
	{
		get
		{
			return this.isEntombed;
		}
	}

	// Token: 0x06003B14 RID: 15124 RVA: 0x00148370 File Offset: 0x00146570
	public void SetStatusItem(StatusItem si)
	{
		bool flag = this.showStatusItemOnEntombed;
		this.SetShowStatusItemOnEntombed(false);
		this.EntombedStatusItem = si;
		this.SetShowStatusItemOnEntombed(flag);
	}

	// Token: 0x06003B15 RID: 15125 RVA: 0x0014839C File Offset: 0x0014659C
	public void SetShowStatusItemOnEntombed(bool val)
	{
		this.showStatusItemOnEntombed = val;
		if (this.isEntombed && this.EntombedStatusItem != null)
		{
			if (this.showStatusItemOnEntombed)
			{
				this.selectable.AddStatusItem(this.EntombedStatusItem, null);
				return;
			}
			this.selectable.RemoveStatusItem(this.EntombedStatusItem, false);
		}
	}

	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x06003B16 RID: 15126 RVA: 0x001483EF File Offset: 0x001465EF
	public string WiltStateString
	{
		get
		{
			return Db.Get().CreatureStatusItems.Entombed.resolveStringCallback(CREATURES.STATUSITEMS.ENTOMBED.LINE_ITEM, base.gameObject);
		}
	}

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x06003B17 RID: 15127 RVA: 0x0014841A File Offset: 0x0014661A
	public WiltCondition.Condition[] Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.Entombed };
		}
	}

	// Token: 0x06003B18 RID: 15128 RVA: 0x00148428 File Offset: 0x00146628
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.EntombedStatusItem == null)
		{
			this.EntombedStatusItem = this.DefaultEntombedStatusItem;
		}
		this.partitionerEntry = GameScenePartitioner.Instance.Add("EntombVulnerable", base.gameObject, this.occupyArea.GetExtents(), GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		this.CheckEntombed();
		if (this.isEntombed)
		{
			base.GetComponent<KPrefabID>().AddTag(GameTags.Entombed, false);
			base.Trigger(-1089732772, true);
		}
	}

	// Token: 0x06003B19 RID: 15129 RVA: 0x001484BB File Offset: 0x001466BB
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06003B1A RID: 15130 RVA: 0x001484D3 File Offset: 0x001466D3
	private void OnSolidChanged(object data)
	{
		this.CheckEntombed();
	}

	// Token: 0x06003B1B RID: 15131 RVA: 0x001484DC File Offset: 0x001466DC
	private void CheckEntombed()
	{
		int num = Grid.PosToCell(base.gameObject.transform.GetPosition());
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		if (!this.IsCellSafe(num))
		{
			if (!this.isEntombed)
			{
				this.isEntombed = true;
				if (this.showStatusItemOnEntombed)
				{
					this.selectable.AddStatusItem(this.EntombedStatusItem, base.gameObject);
				}
				base.GetComponent<KPrefabID>().AddTag(GameTags.Entombed, false);
				base.Trigger(-1089732772, true);
			}
		}
		else if (this.isEntombed)
		{
			this.isEntombed = false;
			this.selectable.RemoveStatusItem(this.EntombedStatusItem, false);
			base.GetComponent<KPrefabID>().RemoveTag(GameTags.Entombed);
			base.Trigger(-1089732772, false);
		}
		if (this.operational != null)
		{
			this.operational.SetFlag(EntombVulnerable.notEntombedFlag, !this.isEntombed);
		}
	}

	// Token: 0x06003B1C RID: 15132 RVA: 0x001485D1 File Offset: 0x001467D1
	public bool IsCellSafe(int cell)
	{
		return this.occupyArea.TestArea(cell, null, EntombVulnerable.IsCellSafeCBDelegate);
	}

	// Token: 0x06003B1D RID: 15133 RVA: 0x001485E5 File Offset: 0x001467E5
	private static bool IsCellSafeCB(int cell, object data)
	{
		return Grid.IsValidCell(cell) && !Grid.Solid[cell];
	}

	// Token: 0x0400243F RID: 9279
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04002440 RID: 9280
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002441 RID: 9281
	private OccupyArea _occupyArea;

	// Token: 0x04002442 RID: 9282
	[Serialize]
	private bool isEntombed;

	// Token: 0x04002443 RID: 9283
	private StatusItem DefaultEntombedStatusItem = Db.Get().CreatureStatusItems.Entombed;

	// Token: 0x04002444 RID: 9284
	[NonSerialized]
	private StatusItem EntombedStatusItem;

	// Token: 0x04002445 RID: 9285
	private bool showStatusItemOnEntombed = true;

	// Token: 0x04002446 RID: 9286
	public static readonly Operational.Flag notEntombedFlag = new Operational.Flag("not_entombed", Operational.Flag.Type.Functional);

	// Token: 0x04002447 RID: 9287
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002448 RID: 9288
	private static readonly Func<int, object, bool> IsCellSafeCBDelegate = (int cell, object data) => EntombVulnerable.IsCellSafeCB(cell, data);
}
