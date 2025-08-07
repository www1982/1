using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000898 RID: 2200
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/DecorProvider")]
public class DecorProvider : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06003CD2 RID: 15570 RVA: 0x00152540 File Offset: 0x00150740
	private void AddDecor()
	{
		this.currDecor = 0f;
		if (this.decor != null)
		{
			this.currDecor = this.decor.GetTotalValue();
		}
		if (this.prefabId.HasTag(GameTags.Stored))
		{
			this.currDecor = 0f;
		}
		int num = Grid.PosToCell(base.gameObject);
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		if (!Grid.Transparent[num] && Grid.Solid[num] && this.simCellOccupier == null)
		{
			this.currDecor = 0f;
		}
		if (this.currDecor == 0f)
		{
			return;
		}
		this.cellCount = 0;
		int num2 = 5;
		if (this.decorRadius != null)
		{
			num2 = (int)this.decorRadius.GetTotalValue();
		}
		Extents extents = this.occupyArea.GetExtents();
		extents.x = Mathf.Max(extents.x - num2, 0);
		extents.y = Mathf.Max(extents.y - num2, 0);
		extents.width = Mathf.Min(extents.width + num2 * 2, Grid.WidthInCells - 1);
		extents.height = Mathf.Min(extents.height + num2 * 2, Grid.HeightInCells - 1);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("DecorProvider.SplatCollectDecorProviders", base.gameObject, extents, GameScenePartitioner.Instance.decorProviderLayer, this.onCollectDecorProvidersCallback);
		this.solidChangedPartitionerEntry = GameScenePartitioner.Instance.Add("DecorProvider.SplatSolidCheck", base.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, this.refreshPartionerCallback);
		int num3 = extents.x + extents.width;
		int num4 = extents.y + extents.height;
		int num5 = extents.x;
		int num6 = extents.y;
		int num7;
		int num8;
		Grid.CellToXY(num, out num7, out num8);
		num3 = Math.Min(num3, Grid.WidthInCells);
		num4 = Math.Min(num4, Grid.HeightInCells);
		num5 = Math.Max(0, num5);
		num6 = Math.Max(0, num6);
		int num9 = (num3 - num5) * (num4 - num6);
		if (this.cells == null || this.cells.Length != num9)
		{
			this.cells = new int[num9];
		}
		for (int i = num5; i < num3; i++)
		{
			for (int j = num6; j < num4; j++)
			{
				if (Grid.VisibilityTest(num7, num8, i, j, false))
				{
					int num10 = Grid.XYToCell(i, j);
					if (Grid.IsValidCell(num10))
					{
						Grid.Decor[num10] += this.currDecor;
						int[] array = this.cells;
						int num11 = this.cellCount;
						this.cellCount = num11 + 1;
						array[num11] = num10;
					}
				}
			}
		}
	}

	// Token: 0x06003CD3 RID: 15571 RVA: 0x001527D6 File Offset: 0x001509D6
	public void Clear()
	{
		if (this.currDecor == 0f)
		{
			return;
		}
		this.RemoveDecor();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.solidChangedPartitionerEntry);
	}

	// Token: 0x06003CD4 RID: 15572 RVA: 0x0015280C File Offset: 0x00150A0C
	private void RemoveDecor()
	{
		if (this.currDecor == 0f)
		{
			return;
		}
		for (int i = 0; i < this.cellCount; i++)
		{
			int num = this.cells[i];
			if (Grid.IsValidCell(num))
			{
				Grid.Decor[num] -= this.currDecor;
			}
		}
	}

	// Token: 0x06003CD5 RID: 15573 RVA: 0x00152860 File Offset: 0x00150A60
	public void Refresh()
	{
		this.Clear();
		this.AddDecor();
		bool flag = this.prefabId.HasTag(RoomConstraints.ConstraintTags.Decor20);
		bool flag2 = this.decor.GetTotalValue() >= 20f;
		if (flag != flag2)
		{
			if (flag2)
			{
				this.prefabId.AddTag(RoomConstraints.ConstraintTags.Decor20, false);
			}
			else
			{
				this.prefabId.RemoveTag(RoomConstraints.ConstraintTags.Decor20);
			}
			int num = Grid.PosToCell(this);
			if (Grid.IsValidCell(num))
			{
				Game.Instance.roomProber.SolidChangedEvent(num, true);
			}
		}
	}

	// Token: 0x06003CD6 RID: 15574 RVA: 0x001528E8 File Offset: 0x00150AE8
	public float GetDecorForCell(int cell)
	{
		for (int i = 0; i < this.cellCount; i++)
		{
			if (this.cells[i] == cell)
			{
				return this.currDecor;
			}
		}
		return 0f;
	}

	// Token: 0x06003CD7 RID: 15575 RVA: 0x0015291D File Offset: 0x00150B1D
	public void SetValues(EffectorValues values)
	{
		this.baseDecor = (float)values.amount;
		this.baseRadius = (float)values.radius;
		if (base.IsInitialized())
		{
			this.UpdateBaseDecorModifiers();
		}
	}

	// Token: 0x06003CD8 RID: 15576 RVA: 0x00152948 File Offset: 0x00150B48
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.decor = this.GetAttributes().Add(Db.Get().BuildingAttributes.Decor);
		this.decorRadius = this.GetAttributes().Add(Db.Get().BuildingAttributes.DecorRadius);
		this.UpdateBaseDecorModifiers();
	}

	// Token: 0x06003CD9 RID: 15577 RVA: 0x001529A4 File Offset: 0x00150BA4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.refreshCallback = new global::System.Action(this.Refresh);
		this.refreshPartionerCallback = delegate(object data)
		{
			this.Refresh();
		};
		this.onCollectDecorProvidersCallback = new Action<object>(this.OnCollectDecorProviders);
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange), "DecorProvider.OnSpawn");
		AttributeInstance attributeInstance = this.decor;
		attributeInstance.OnDirty = (global::System.Action)Delegate.Combine(attributeInstance.OnDirty, this.refreshCallback);
		AttributeInstance attributeInstance2 = this.decorRadius;
		attributeInstance2.OnDirty = (global::System.Action)Delegate.Combine(attributeInstance2.OnDirty, this.refreshCallback);
		this.Refresh();
	}

	// Token: 0x06003CDA RID: 15578 RVA: 0x00152A58 File Offset: 0x00150C58
	private void UpdateBaseDecorModifiers()
	{
		Attributes attributes = this.GetAttributes();
		if (this.baseDecorModifier != null)
		{
			attributes.Remove(this.baseDecorModifier);
			attributes.Remove(this.baseDecorRadiusModifier);
			this.baseDecorModifier = null;
			this.baseDecorRadiusModifier = null;
		}
		if (this.baseDecor != 0f)
		{
			this.baseDecorModifier = new AttributeModifier(Db.Get().BuildingAttributes.Decor.Id, this.baseDecor, UI.TOOLTIPS.BASE_VALUE, false, false, true);
			this.baseDecorRadiusModifier = new AttributeModifier(Db.Get().BuildingAttributes.DecorRadius.Id, this.baseRadius, UI.TOOLTIPS.BASE_VALUE, false, false, true);
			attributes.Add(this.baseDecorModifier);
			attributes.Add(this.baseDecorRadiusModifier);
		}
	}

	// Token: 0x06003CDB RID: 15579 RVA: 0x00152B23 File Offset: 0x00150D23
	private void OnCellChange()
	{
		this.Refresh();
	}

	// Token: 0x06003CDC RID: 15580 RVA: 0x00152B2B File Offset: 0x00150D2B
	private void OnCollectDecorProviders(object data)
	{
		((List<DecorProvider>)data).Add(this);
	}

	// Token: 0x06003CDD RID: 15581 RVA: 0x00152B39 File Offset: 0x00150D39
	public string GetName()
	{
		if (string.IsNullOrEmpty(this.overrideName))
		{
			return base.GetComponent<KSelectable>().GetName();
		}
		return this.overrideName;
	}

	// Token: 0x06003CDE RID: 15582 RVA: 0x00152B5C File Offset: 0x00150D5C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (base.isSpawned)
		{
			AttributeInstance attributeInstance = this.decor;
			attributeInstance.OnDirty = (global::System.Action)Delegate.Remove(attributeInstance.OnDirty, this.refreshCallback);
			AttributeInstance attributeInstance2 = this.decorRadius;
			attributeInstance2.OnDirty = (global::System.Action)Delegate.Remove(attributeInstance2.OnDirty, this.refreshCallback);
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new global::System.Action(this.OnCellChange));
		}
		this.Clear();
	}

	// Token: 0x06003CDF RID: 15583 RVA: 0x00152BDC File Offset: 0x00150DDC
	public List<Descriptor> GetEffectDescriptions()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.decor != null && this.decorRadius != null)
		{
			float totalValue = this.decor.GetTotalValue();
			float totalValue2 = this.decorRadius.GetTotalValue();
			string text = ((this.baseDecor > 0f) ? "produced" : "consumed");
			string text2 = ((this.baseDecor > 0f) ? UI.BUILDINGEFFECTS.TOOLTIPS.DECORPROVIDED : UI.BUILDINGEFFECTS.TOOLTIPS.DECORDECREASED);
			text2 = text2 + "\n\n" + this.decor.GetAttributeValueTooltip();
			string text3 = GameUtil.AddPositiveSign(totalValue.ToString(), totalValue > 0f);
			Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.DECORPROVIDED, text, text3, totalValue2), string.Format(text2, text3, totalValue2), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor);
		}
		else if (this.baseDecor != 0f)
		{
			string text4 = ((this.baseDecor >= 0f) ? "produced" : "consumed");
			string text5 = ((this.baseDecor >= 0f) ? UI.BUILDINGEFFECTS.TOOLTIPS.DECORPROVIDED : UI.BUILDINGEFFECTS.TOOLTIPS.DECORDECREASED);
			string text6 = GameUtil.AddPositiveSign(this.baseDecor.ToString(), this.baseDecor > 0f);
			Descriptor descriptor2 = new Descriptor(string.Format(UI.BUILDINGEFFECTS.DECORPROVIDED, text4, text6, this.baseRadius), string.Format(text5, text6, this.baseRadius), Descriptor.DescriptorType.Effect, false);
			list.Add(descriptor2);
		}
		return list;
	}

	// Token: 0x06003CE0 RID: 15584 RVA: 0x00152D71 File Offset: 0x00150F71
	public static int GetLightDecorBonus(int cell)
	{
		if (Grid.LightIntensity[cell] > 0)
		{
			return DECOR.LIT_BONUS;
		}
		return 0;
	}

	// Token: 0x06003CE1 RID: 15585 RVA: 0x00152D88 File Offset: 0x00150F88
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return this.GetEffectDescriptions();
	}

	// Token: 0x04002544 RID: 9540
	public const string ID = "DecorProvider";

	// Token: 0x04002545 RID: 9541
	public float baseRadius;

	// Token: 0x04002546 RID: 9542
	public float baseDecor;

	// Token: 0x04002547 RID: 9543
	public string overrideName;

	// Token: 0x04002548 RID: 9544
	public global::System.Action refreshCallback;

	// Token: 0x04002549 RID: 9545
	public Action<object> refreshPartionerCallback;

	// Token: 0x0400254A RID: 9546
	public Action<object> onCollectDecorProvidersCallback;

	// Token: 0x0400254B RID: 9547
	public AttributeInstance decor;

	// Token: 0x0400254C RID: 9548
	public AttributeInstance decorRadius;

	// Token: 0x0400254D RID: 9549
	private AttributeModifier baseDecorModifier;

	// Token: 0x0400254E RID: 9550
	private AttributeModifier baseDecorRadiusModifier;

	// Token: 0x0400254F RID: 9551
	[MyCmpReq]
	private KPrefabID prefabId;

	// Token: 0x04002550 RID: 9552
	[MyCmpReq]
	public OccupyArea occupyArea;

	// Token: 0x04002551 RID: 9553
	[MyCmpGet]
	public SimCellOccupier simCellOccupier;

	// Token: 0x04002552 RID: 9554
	private int[] cells;

	// Token: 0x04002553 RID: 9555
	private int cellCount;

	// Token: 0x04002554 RID: 9556
	public float currDecor;

	// Token: 0x04002555 RID: 9557
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002556 RID: 9558
	private HandleVector<int>.Handle solidChangedPartitionerEntry;
}
