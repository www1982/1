using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000576 RID: 1398
public class BaggableCritterCapacityTracker : KMonoBehaviour, ISim1000ms, IUserControlledCapacity
{
	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06001F39 RID: 7993 RVA: 0x000B2C23 File Offset: 0x000B0E23
	// (set) Token: 0x06001F3A RID: 7994 RVA: 0x000B2C2B File Offset: 0x000B0E2B
	[Serialize]
	public int creatureLimit { get; set; } = 20;

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06001F3B RID: 7995 RVA: 0x000B2C34 File Offset: 0x000B0E34
	// (set) Token: 0x06001F3C RID: 7996 RVA: 0x000B2C3C File Offset: 0x000B0E3C
	public int storedCreatureCount { get; private set; }

	// Token: 0x06001F3D RID: 7997 RVA: 0x000B2C48 File Offset: 0x000B0E48
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(this);
		this.cavityCell = Grid.OffsetCell(num, this.cavityOffset);
		this.filter = base.GetComponent<TreeFilterable>();
		TreeFilterable treeFilterable = this.filter;
		treeFilterable.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Combine(treeFilterable.OnFilterChanged, new Action<HashSet<Tag>>(this.RefreshCreatureCount));
		base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
		if (this.requireLiquidOffset)
		{
			this.partitionerEntry = GameScenePartitioner.Instance.Add("BaggableCritterCapacityTracker.OnSpawn", base.gameObject, new Extents(this.cavityCell, new CellOffset[]
			{
				new CellOffset(0, 0)
			}), GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnLiquidChanged));
			this.OnLiquidChanged(null);
			return;
		}
		base.Subscribe(144050788, new Action<object>(this.RefreshCreatureCount));
	}

	// Token: 0x06001F3E RID: 7998 RVA: 0x000B2D38 File Offset: 0x000B0F38
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (BaggableCritterCapacityTracker.capacityStatusItem == null)
		{
			BaggableCritterCapacityTracker.capacityStatusItem = new StatusItem("CritterCapacity", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			BaggableCritterCapacityTracker.capacityStatusItem.resolveStringCallback = delegate(string str, object data)
			{
				IUserControlledCapacity userControlledCapacity = (IUserControlledCapacity)data;
				string text = Util.FormatWholeNumber(Mathf.Floor(userControlledCapacity.AmountStored));
				string text2 = Util.FormatWholeNumber(userControlledCapacity.UserMaxCapacity);
				str = str.Replace("{Stored}", text).Replace("{StoredUnits}", ((int)userControlledCapacity.AmountStored == 1) ? BUILDING.STATUSITEMS.CRITTERCAPACITY.UNIT : BUILDING.STATUSITEMS.CRITTERCAPACITY.UNITS).Replace("{Capacity}", text2)
					.Replace("{CapacityUnits}", ((int)userControlledCapacity.UserMaxCapacity == 1) ? BUILDING.STATUSITEMS.CRITTERCAPACITY.UNIT : BUILDING.STATUSITEMS.CRITTERCAPACITY.UNITS);
				return str;
			};
		}
		this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, BaggableCritterCapacityTracker.capacityStatusItem, this);
	}

	// Token: 0x06001F3F RID: 7999 RVA: 0x000B2DC4 File Offset: 0x000B0FC4
	protected override void OnCleanUp()
	{
		if (this.requireLiquidOffset)
		{
			GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		}
		TreeFilterable treeFilterable = this.filter;
		treeFilterable.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Remove(treeFilterable.OnFilterChanged, new Action<HashSet<Tag>>(this.RefreshCreatureCount));
		base.Unsubscribe(144050788);
		base.OnCleanUp();
	}

	// Token: 0x06001F40 RID: 8000 RVA: 0x000B2E24 File Offset: 0x000B1024
	private void OnLiquidChanged(object data)
	{
		if (this.requireLiquidOffset)
		{
			bool flag = Grid.IsLiquid(this.cavityCell);
			if (flag)
			{
				this.RefreshCreatureCount(null);
			}
			this.operational.SetFlag(BaggableCritterCapacityTracker.isInLiquid, flag);
			this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NotSubmerged, !flag, this);
			this.selectable.ToggleStatusItem(BaggableCritterCapacityTracker.capacityStatusItem, flag, this);
		}
	}

	// Token: 0x06001F41 RID: 8001 RVA: 0x000B2E94 File Offset: 0x000B1094
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		BaggableCritterCapacityTracker component = gameObject.GetComponent<BaggableCritterCapacityTracker>();
		if (component == null)
		{
			return;
		}
		this.creatureLimit = component.creatureLimit;
	}

	// Token: 0x06001F42 RID: 8002 RVA: 0x000B2ED0 File Offset: 0x000B10D0
	public void RefreshCreatureCount(object data = null)
	{
		int storedCreatureCount = this.storedCreatureCount;
		if (this.requireLiquidOffset)
		{
			this.storedCreatureCount = this.RefreshSwimmingCreatureCount();
		}
		else
		{
			this.storedCreatureCount = this.RefreshOtherCreatureCount();
		}
		if (this.onCountChanged != null && this.storedCreatureCount != storedCreatureCount)
		{
			this.onCountChanged();
		}
	}

	// Token: 0x06001F43 RID: 8003 RVA: 0x000B2F24 File Offset: 0x000B1124
	private int RefreshOtherCreatureCount()
	{
		int num = 0;
		CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(this.cavityCell);
		if (cavityForCell != null)
		{
			foreach (KPrefabID kprefabID in cavityForCell.creatures)
			{
				if (!kprefabID.HasTag(GameTags.Creatures.Bagged) && !kprefabID.HasTag(GameTags.Trapped) && (!this.filteredCount || this.filter.AcceptedTags.Contains(kprefabID.PrefabTag)))
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06001F44 RID: 8004 RVA: 0x000B2FCC File Offset: 0x000B11CC
	private int RefreshSwimmingCreatureCount()
	{
		return FishOvercrowingManager.Instance.GetFishCavityCount(this.cavityCell, this.filter.AcceptedTags);
	}

	// Token: 0x06001F45 RID: 8005 RVA: 0x000B2FE9 File Offset: 0x000B11E9
	public void Sim1000ms(float dt)
	{
		this.RefreshCreatureCount(null);
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06001F46 RID: 8006 RVA: 0x000B2FF2 File Offset: 0x000B11F2
	// (set) Token: 0x06001F47 RID: 8007 RVA: 0x000B2FFB File Offset: 0x000B11FB
	float IUserControlledCapacity.UserMaxCapacity
	{
		get
		{
			return (float)this.creatureLimit;
		}
		set
		{
			this.creatureLimit = Mathf.RoundToInt(value);
			if (this.onCountChanged != null)
			{
				this.onCountChanged();
			}
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06001F48 RID: 8008 RVA: 0x000B301C File Offset: 0x000B121C
	float IUserControlledCapacity.AmountStored
	{
		get
		{
			return (float)this.storedCreatureCount;
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06001F49 RID: 8009 RVA: 0x000B3025 File Offset: 0x000B1225
	float IUserControlledCapacity.MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06001F4A RID: 8010 RVA: 0x000B302C File Offset: 0x000B122C
	float IUserControlledCapacity.MaxCapacity
	{
		get
		{
			return (float)this.maximumCreatures;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x06001F4B RID: 8011 RVA: 0x000B3035 File Offset: 0x000B1235
	bool IUserControlledCapacity.WholeValues
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000127 RID: 295
	// (get) Token: 0x06001F4C RID: 8012 RVA: 0x000B3038 File Offset: 0x000B1238
	LocString IUserControlledCapacity.CapacityUnits
	{
		get
		{
			return UI.UISIDESCREENS.CAPTURE_POINT_SIDE_SCREEN.UNITS_SUFFIX;
		}
	}

	// Token: 0x04001223 RID: 4643
	public int maximumCreatures = 40;

	// Token: 0x04001224 RID: 4644
	public bool requireLiquidOffset;

	// Token: 0x04001225 RID: 4645
	public CellOffset cavityOffset;

	// Token: 0x04001226 RID: 4646
	public bool filteredCount;

	// Token: 0x04001227 RID: 4647
	public global::System.Action onCountChanged;

	// Token: 0x04001228 RID: 4648
	private int cavityCell;

	// Token: 0x04001229 RID: 4649
	[MyCmpReq]
	private TreeFilterable filter;

	// Token: 0x0400122A RID: 4650
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400122B RID: 4651
	private static readonly Operational.Flag isInLiquid = new Operational.Flag("isInLiquid", Operational.Flag.Type.Requirement);

	// Token: 0x0400122C RID: 4652
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x0400122D RID: 4653
	private static StatusItem capacityStatusItem;

	// Token: 0x0400122E RID: 4654
	private HandleVector<int>.Handle partitionerEntry;
}
