using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000751 RID: 1873
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Ladder")]
public class Ladder : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06002FA8 RID: 12200 RVA: 0x00110F1C File Offset: 0x0010F11C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Rotatable component = base.GetComponent<Rotatable>();
		foreach (CellOffset cellOffset in this.offsets)
		{
			CellOffset cellOffset2 = cellOffset;
			if (component != null)
			{
				cellOffset2 = component.GetRotatedCellOffset(cellOffset);
			}
			int num = Grid.OffsetCell(Grid.PosToCell(this), cellOffset2);
			Grid.HasPole[num] = this.isPole;
			Grid.HasLadder[num] = !this.isPole;
		}
		base.GetComponent<KPrefabID>().AddTag(GameTags.Ladders, false);
		Components.Ladders.Add(this);
	}

	// Token: 0x06002FA9 RID: 12201 RVA: 0x00110FBA File Offset: 0x0010F1BA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, null);
	}

	// Token: 0x06002FAA RID: 12202 RVA: 0x00110FF0 File Offset: 0x0010F1F0
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Rotatable component = base.GetComponent<Rotatable>();
		foreach (CellOffset cellOffset in this.offsets)
		{
			CellOffset cellOffset2 = cellOffset;
			if (component != null)
			{
				cellOffset2 = component.GetRotatedCellOffset(cellOffset);
			}
			int num = Grid.OffsetCell(Grid.PosToCell(this), cellOffset2);
			if (Grid.Objects[num, 24] == null)
			{
				Grid.HasPole[num] = false;
				Grid.HasLadder[num] = false;
			}
		}
		Components.Ladders.Remove(this);
	}

	// Token: 0x06002FAB RID: 12203 RVA: 0x00111088 File Offset: 0x0010F288
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = null;
		if (this.upwardsMovementSpeedMultiplier != 1f)
		{
			list = new List<Descriptor>();
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.DUPLICANTMOVEMENTBOOST, GameUtil.GetFormattedPercent(this.upwardsMovementSpeedMultiplier * 100f - 100f, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.DUPLICANTMOVEMENTBOOST, GameUtil.GetFormattedPercent(this.upwardsMovementSpeedMultiplier * 100f - 100f, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
		}
		return list;
	}

	// Token: 0x04001C57 RID: 7255
	public float upwardsMovementSpeedMultiplier = 1f;

	// Token: 0x04001C58 RID: 7256
	public float downwardsMovementSpeedMultiplier = 1f;

	// Token: 0x04001C59 RID: 7257
	public bool isPole;

	// Token: 0x04001C5A RID: 7258
	public CellOffset[] offsets = new CellOffset[] { CellOffset.none };
}
