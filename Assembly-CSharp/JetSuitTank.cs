using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000999 RID: 2457
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/JetSuitTank")]
public class JetSuitTank : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x0600473F RID: 18239 RVA: 0x0019AE12 File Offset: 0x00199012
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.amount = 25f;
		base.Subscribe<JetSuitTank>(-1617557748, JetSuitTank.OnEquippedDelegate);
		base.Subscribe<JetSuitTank>(-170173755, JetSuitTank.OnUnequippedDelegate);
	}

	// Token: 0x06004740 RID: 18240 RVA: 0x0019AE47 File Offset: 0x00199047
	public float PercentFull()
	{
		return this.amount / 25f;
	}

	// Token: 0x06004741 RID: 18241 RVA: 0x0019AE55 File Offset: 0x00199055
	public bool IsEmpty()
	{
		return this.amount <= 0f;
	}

	// Token: 0x06004742 RID: 18242 RVA: 0x0019AE67 File Offset: 0x00199067
	public bool IsFull()
	{
		return this.PercentFull() >= 1f;
	}

	// Token: 0x06004743 RID: 18243 RVA: 0x0019AE79 File Offset: 0x00199079
	public bool NeedsRecharging()
	{
		return this.PercentFull() < 0.25f;
	}

	// Token: 0x06004744 RID: 18244 RVA: 0x0019AE88 File Offset: 0x00199088
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string text = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.JETSUIT_TANK, GameUtil.GetFormattedMass(this.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		list.Add(new Descriptor(text, text, Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x06004745 RID: 18245 RVA: 0x0019AECC File Offset: 0x001990CC
	private void OnEquipped(object data)
	{
		Equipment equipment = (Equipment)data;
		NameDisplayScreen.Instance.SetSuitFuelDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), new Func<float>(this.PercentFull), true);
		this.jetSuitMonitor = new JetSuitMonitor.Instance(this, equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject());
		this.jetSuitMonitor.StartSM();
		if (this.IsEmpty())
		{
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().AddTag(GameTags.JetSuitOutOfFuel);
		}
	}

	// Token: 0x06004746 RID: 18246 RVA: 0x0019AF44 File Offset: 0x00199144
	private void OnUnequipped(object data)
	{
		Equipment equipment = (Equipment)data;
		if (!equipment.destroyed)
		{
			equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().RemoveTag(GameTags.JetSuitOutOfFuel);
			NameDisplayScreen.Instance.SetSuitFuelDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), null, false);
			Navigator component = equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject().GetComponent<Navigator>();
			if (component && component.CurrentNavType == NavType.Hover)
			{
				component.SetCurrentNavType(NavType.Floor);
			}
		}
		if (this.jetSuitMonitor != null)
		{
			this.jetSuitMonitor.StopSM("Removed jetsuit tank");
			this.jetSuitMonitor = null;
		}
	}

	// Token: 0x04002F18 RID: 12056
	[MyCmpGet]
	private ElementEmitter elementConverter;

	// Token: 0x04002F19 RID: 12057
	[Serialize]
	public float amount;

	// Token: 0x04002F1A RID: 12058
	public const float FUEL_CAPACITY = 25f;

	// Token: 0x04002F1B RID: 12059
	public const float FUEL_BURN_RATE = 0.1f;

	// Token: 0x04002F1C RID: 12060
	public const float CO2_EMITTED_PER_FUEL_BURNED = 3f;

	// Token: 0x04002F1D RID: 12061
	public const float EMIT_TEMPERATURE = 473.15f;

	// Token: 0x04002F1E RID: 12062
	public const float REFILL_PERCENT = 0.25f;

	// Token: 0x04002F1F RID: 12063
	private JetSuitMonitor.Instance jetSuitMonitor;

	// Token: 0x04002F20 RID: 12064
	private static readonly EventSystem.IntraObjectHandler<JetSuitTank> OnEquippedDelegate = new EventSystem.IntraObjectHandler<JetSuitTank>(delegate(JetSuitTank component, object data)
	{
		component.OnEquipped(data);
	});

	// Token: 0x04002F21 RID: 12065
	private static readonly EventSystem.IntraObjectHandler<JetSuitTank> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<JetSuitTank>(delegate(JetSuitTank component, object data)
	{
		component.OnUnequipped(data);
	});
}
