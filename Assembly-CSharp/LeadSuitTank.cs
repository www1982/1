using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020009A5 RID: 2469
[SerializationConfig(MemberSerialization.OptIn)]
public class LeadSuitTank : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x060047C0 RID: 18368 RVA: 0x0019DFAC File Offset: 0x0019C1AC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LeadSuitTank>(-1617557748, LeadSuitTank.OnEquippedDelegate);
		base.Subscribe<LeadSuitTank>(-170173755, LeadSuitTank.OnUnequippedDelegate);
	}

	// Token: 0x060047C1 RID: 18369 RVA: 0x0019DFD6 File Offset: 0x0019C1D6
	public float PercentFull()
	{
		return this.batteryCharge;
	}

	// Token: 0x060047C2 RID: 18370 RVA: 0x0019DFDE File Offset: 0x0019C1DE
	public bool IsEmpty()
	{
		return this.batteryCharge <= 0f;
	}

	// Token: 0x060047C3 RID: 18371 RVA: 0x0019DFF0 File Offset: 0x0019C1F0
	public bool IsFull()
	{
		return this.PercentFull() >= 1f;
	}

	// Token: 0x060047C4 RID: 18372 RVA: 0x0019E002 File Offset: 0x0019C202
	public bool NeedsRecharging()
	{
		return this.PercentFull() <= 0.25f;
	}

	// Token: 0x060047C5 RID: 18373 RVA: 0x0019E014 File Offset: 0x0019C214
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.LEADSUIT_BATTERY, GameUtil.GetFormattedPercent(this.PercentFull() * 100f, GameUtil.TimeSlice.None));
		list.Add(new Descriptor(text, text, Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x060047C6 RID: 18374 RVA: 0x0019E058 File Offset: 0x0019C258
	private void OnEquipped(object data)
	{
		Equipment equipment = (Equipment)data;
		NameDisplayScreen.Instance.SetSuitBatteryDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), new Func<float>(this.PercentFull), true);
		this.leadSuitMonitor = new LeadSuitMonitor.Instance(this, equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject());
		this.leadSuitMonitor.StartSM();
		if (this.NeedsRecharging())
		{
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().AddTag(GameTags.SuitBatteryLow);
		}
	}

	// Token: 0x060047C7 RID: 18375 RVA: 0x0019E0D0 File Offset: 0x0019C2D0
	private void OnUnequipped(object data)
	{
		Equipment equipment = (Equipment)data;
		if (!equipment.destroyed)
		{
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().RemoveTag(GameTags.SuitBatteryLow);
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().RemoveTag(GameTags.SuitBatteryOut);
			NameDisplayScreen.Instance.SetSuitBatteryDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), null, false);
		}
		if (this.leadSuitMonitor != null)
		{
			this.leadSuitMonitor.StopSM("Removed leadsuit tank");
			this.leadSuitMonitor = null;
		}
	}

	// Token: 0x04002F80 RID: 12160
	[Serialize]
	public float batteryCharge = 1f;

	// Token: 0x04002F81 RID: 12161
	public const float REFILL_PERCENT = 0.25f;

	// Token: 0x04002F82 RID: 12162
	public float batteryDuration = 200f;

	// Token: 0x04002F83 RID: 12163
	public float coolingOperationalTemperature = 333.15f;

	// Token: 0x04002F84 RID: 12164
	public Tag coolantTag;

	// Token: 0x04002F85 RID: 12165
	private LeadSuitMonitor.Instance leadSuitMonitor;

	// Token: 0x04002F86 RID: 12166
	private static readonly EventSystem.IntraObjectHandler<LeadSuitTank> OnEquippedDelegate = new EventSystem.IntraObjectHandler<LeadSuitTank>(delegate(LeadSuitTank component, object data)
	{
		component.OnEquipped(data);
	});

	// Token: 0x04002F87 RID: 12167
	private static readonly EventSystem.IntraObjectHandler<LeadSuitTank> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<LeadSuitTank>(delegate(LeadSuitTank component, object data)
	{
		component.OnUnequipped(data);
	});
}
