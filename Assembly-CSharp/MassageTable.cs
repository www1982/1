using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000781 RID: 1921
public class MassageTable : RelaxationPoint, IGameObjectEffectDescriptor, IActivationRangeTarget
{
	// Token: 0x17000322 RID: 802
	// (get) Token: 0x060032A5 RID: 12965 RVA: 0x0011D46F File Offset: 0x0011B66F
	public string ActivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.MASSAGETABLE.ACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x060032A6 RID: 12966 RVA: 0x0011D47B File Offset: 0x0011B67B
	public string DeactivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.MASSAGETABLE.DEACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x060032A7 RID: 12967 RVA: 0x0011D487 File Offset: 0x0011B687
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<MassageTable>(-905833192, MassageTable.OnCopySettingsDelegate);
	}

	// Token: 0x060032A8 RID: 12968 RVA: 0x0011D4A0 File Offset: 0x0011B6A0
	private void OnCopySettings(object data)
	{
		MassageTable component = ((GameObject)data).GetComponent<MassageTable>();
		if (component != null)
		{
			this.ActivateValue = component.ActivateValue;
			this.DeactivateValue = component.DeactivateValue;
		}
	}

	// Token: 0x060032A9 RID: 12969 RVA: 0x0011D4DC File Offset: 0x0011B6DC
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		Effects component = worker.GetComponent<Effects>();
		for (int i = 0; i < MassageTable.EffectsRemoved.Length; i++)
		{
			string text = MassageTable.EffectsRemoved[i];
			component.Remove(text);
		}
	}

	// Token: 0x060032AA RID: 12970 RVA: 0x0011D518 File Offset: 0x0011B718
	public new List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.STRESSREDUCEDPERMINUTE, GameUtil.GetFormattedPercent(this.stressModificationValue / 600f * 60f, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.STRESSREDUCEDPERMINUTE, GameUtil.GetFormattedPercent(this.stressModificationValue / 600f * 60f, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		if (MassageTable.EffectsRemoved.Length != 0)
		{
			Descriptor descriptor2 = default(Descriptor);
			descriptor2.SetupDescriptor(UI.BUILDINGEFFECTS.REMOVESEFFECTSUBTITLE, UI.BUILDINGEFFECTS.TOOLTIPS.REMOVESEFFECTSUBTITLE, Descriptor.DescriptorType.Effect);
			list.Add(descriptor2);
			for (int i = 0; i < MassageTable.EffectsRemoved.Length; i++)
			{
				string text = MassageTable.EffectsRemoved[i];
				string text2 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".NAME");
				string text3 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".CAUSE");
				Descriptor descriptor3 = default(Descriptor);
				descriptor3.IncreaseIndent();
				descriptor3.SetupDescriptor("• " + string.Format(UI.BUILDINGEFFECTS.REMOVEDEFFECT, text2), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REMOVEDEFFECT, text3), Descriptor.DescriptorType.Effect);
				list.Add(descriptor3);
			}
		}
		return list;
	}

	// Token: 0x060032AB RID: 12971 RVA: 0x0011D678 File Offset: 0x0011B878
	protected override WorkChore<RelaxationPoint> CreateWorkChore()
	{
		WorkChore<RelaxationPoint> workChore = new WorkChore<RelaxationPoint>(Db.Get().ChoreTypes.StressHeal, this, null, true, null, null, null, false, null, true, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
		workChore.AddPrecondition(MassageTable.IsStressAboveActivationRange, this);
		return workChore;
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x060032AC RID: 12972 RVA: 0x0011D6C8 File Offset: 0x0011B8C8
	// (set) Token: 0x060032AD RID: 12973 RVA: 0x0011D6D0 File Offset: 0x0011B8D0
	public float ActivateValue
	{
		get
		{
			return this.activateValue;
		}
		set
		{
			this.activateValue = value;
		}
	}

	// Token: 0x17000325 RID: 805
	// (get) Token: 0x060032AE RID: 12974 RVA: 0x0011D6D9 File Offset: 0x0011B8D9
	// (set) Token: 0x060032AF RID: 12975 RVA: 0x0011D6E1 File Offset: 0x0011B8E1
	public float DeactivateValue
	{
		get
		{
			return this.stopStressingValue;
		}
		set
		{
			this.stopStressingValue = value;
		}
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x060032B0 RID: 12976 RVA: 0x0011D6EA File Offset: 0x0011B8EA
	public bool UseWholeNumbers
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x060032B1 RID: 12977 RVA: 0x0011D6ED File Offset: 0x0011B8ED
	public float MinValue
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x060032B2 RID: 12978 RVA: 0x0011D6F4 File Offset: 0x0011B8F4
	public float MaxValue
	{
		get
		{
			return 100f;
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x060032B3 RID: 12979 RVA: 0x0011D6FB File Offset: 0x0011B8FB
	public string ActivationRangeTitleText
	{
		get
		{
			return UI.UISIDESCREENS.ACTIVATION_RANGE_SIDE_SCREEN.NAME;
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x060032B4 RID: 12980 RVA: 0x0011D707 File Offset: 0x0011B907
	public string ActivateSliderLabelText
	{
		get
		{
			return UI.UISIDESCREENS.ACTIVATION_RANGE_SIDE_SCREEN.ACTIVATE;
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x060032B5 RID: 12981 RVA: 0x0011D713 File Offset: 0x0011B913
	public string DeactivateSliderLabelText
	{
		get
		{
			return UI.UISIDESCREENS.ACTIVATION_RANGE_SIDE_SCREEN.DEACTIVATE;
		}
	}

	// Token: 0x04001E62 RID: 7778
	[Serialize]
	private float activateValue = 50f;

	// Token: 0x04001E63 RID: 7779
	private static readonly string[] EffectsRemoved = new string[] { "SoreBack" };

	// Token: 0x04001E64 RID: 7780
	private static readonly EventSystem.IntraObjectHandler<MassageTable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<MassageTable>(delegate(MassageTable component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001E65 RID: 7781
	private static readonly Chore.Precondition IsStressAboveActivationRange = new Chore.Precondition
	{
		id = "IsStressAboveActivationRange",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_STRESS_ABOVE_ACTIVATION_RANGE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			IActivationRangeTarget activationRangeTarget = (IActivationRangeTarget)data;
			return Db.Get().Amounts.Stress.Lookup(context.consumerState.gameObject).value >= activationRangeTarget.ActivateValue;
		}
	};
}
