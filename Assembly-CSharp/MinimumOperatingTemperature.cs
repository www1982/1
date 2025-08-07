using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020005DF RID: 1503
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/MinimumOperatingTemperature")]
public class MinimumOperatingTemperature : KMonoBehaviour, ISim200ms, IGameObjectEffectDescriptor
{
	// Token: 0x060022C9 RID: 8905 RVA: 0x000C7C87 File Offset: 0x000C5E87
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.TestTemperature(true);
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x000C7C96 File Offset: 0x000C5E96
	public void Sim200ms(float dt)
	{
		this.TestTemperature(false);
	}

	// Token: 0x060022CB RID: 8907 RVA: 0x000C7CA0 File Offset: 0x000C5EA0
	private void TestTemperature(bool force)
	{
		bool flag;
		if (this.primaryElement.Temperature < this.minimumTemperature)
		{
			flag = false;
		}
		else
		{
			flag = true;
			for (int i = 0; i < this.building.PlacementCells.Length; i++)
			{
				int num = this.building.PlacementCells[i];
				float num2 = Grid.Temperature[num];
				float num3 = Grid.Mass[num];
				if ((num2 != 0f || num3 != 0f) && num2 < this.minimumTemperature)
				{
					flag = false;
					break;
				}
			}
		}
		if (!flag)
		{
			this.lastOffTime = Time.time;
		}
		if ((flag != this.isWarm && !flag) || (flag != this.isWarm && flag && Time.time > this.lastOffTime + 5f) || force)
		{
			this.isWarm = flag;
			this.operational.SetFlag(MinimumOperatingTemperature.warmEnoughFlag, this.isWarm);
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.TooCold, !this.isWarm, this);
		}
	}

	// Token: 0x060022CC RID: 8908 RVA: 0x000C7DAA File Offset: 0x000C5FAA
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x060022CD RID: 8909 RVA: 0x000C7DC4 File Offset: 0x000C5FC4
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.MINIMUM_TEMP, GameUtil.GetFormattedTemperature(this.minimumTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.MINIMUM_TEMP, GameUtil.GetFormattedTemperature(this.minimumTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect, false);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x04001428 RID: 5160
	[MyCmpReq]
	private Building building;

	// Token: 0x04001429 RID: 5161
	[MyCmpReq]
	private Operational operational;

	// Token: 0x0400142A RID: 5162
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x0400142B RID: 5163
	public float minimumTemperature = 275.15f;

	// Token: 0x0400142C RID: 5164
	private const float TURN_ON_DELAY = 5f;

	// Token: 0x0400142D RID: 5165
	private float lastOffTime;

	// Token: 0x0400142E RID: 5166
	public static readonly Operational.Flag warmEnoughFlag = new Operational.Flag("warm_enough", Operational.Flag.Type.Functional);

	// Token: 0x0400142F RID: 5167
	private bool isWarm;

	// Token: 0x04001430 RID: 5168
	private HandleVector<int>.Handle partitionerEntry;
}
