using System;
using System.Collections.Generic;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000BB0 RID: 2992
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SuitTank")]
public class SuitTank : KMonoBehaviour, IGameObjectEffectDescriptor, OxygenBreather.IGasProvider
{
	// Token: 0x06005974 RID: 22900 RVA: 0x00204FD0 File Offset: 0x002031D0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<SuitTank>(-1617557748, SuitTank.OnEquippedDelegate);
		base.Subscribe<SuitTank>(-170173755, SuitTank.OnUnequippedDelegate);
	}

	// Token: 0x06005975 RID: 22901 RVA: 0x00204FFC File Offset: 0x002031FC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.amount != 0f)
		{
			this.storage.AddGasChunk(SimHashes.Oxygen, this.amount, base.GetComponent<PrimaryElement>().Temperature, byte.MaxValue, 0, false, true);
			this.amount = 0f;
		}
		this.equippable = base.GetComponent<Equippable>();
	}

	// Token: 0x06005976 RID: 22902 RVA: 0x0020505D File Offset: 0x0020325D
	public float GetTankAmount()
	{
		if (this.storage == null)
		{
			this.storage = base.GetComponent<Storage>();
		}
		return this.storage.GetMassAvailable(this.elementTag);
	}

	// Token: 0x06005977 RID: 22903 RVA: 0x0020508A File Offset: 0x0020328A
	public float PercentFull()
	{
		return this.GetTankAmount() / this.capacity;
	}

	// Token: 0x06005978 RID: 22904 RVA: 0x00205099 File Offset: 0x00203299
	public bool IsEmpty()
	{
		return this.GetTankAmount() <= 0f;
	}

	// Token: 0x06005979 RID: 22905 RVA: 0x002050AB File Offset: 0x002032AB
	public bool IsFull()
	{
		return this.PercentFull() >= 1f;
	}

	// Token: 0x0600597A RID: 22906 RVA: 0x002050BD File Offset: 0x002032BD
	public bool NeedsRecharging()
	{
		return this.PercentFull() < 0.25f;
	}

	// Token: 0x0600597B RID: 22907 RVA: 0x002050CC File Offset: 0x002032CC
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.elementTag == GameTags.Breathable)
		{
			string text = (this.underwaterSupport ? string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.OXYGEN_TANK_UNDERWATER, GameUtil.GetFormattedMass(this.GetTankAmount(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")) : string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.EFFECTS.OXYGEN_TANK, GameUtil.GetFormattedMass(this.GetTankAmount(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")));
			list.Add(new Descriptor(text, text, Descriptor.DescriptorType.Effect, false));
		}
		return list;
	}

	// Token: 0x0600597C RID: 22908 RVA: 0x00205150 File Offset: 0x00203350
	private void OnEquipped(object data)
	{
		Equipment equipment = (Equipment)data;
		NameDisplayScreen.Instance.SetSuitTankDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), new Func<float>(this.PercentFull), true);
		GameObject targetGameObject = equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
		OxygenBreather component = targetGameObject.GetComponent<OxygenBreather>();
		if (component != null)
		{
			component.GetComponent<Sensors>().GetSensor<SafeCellSensor>().AddIgnoredFlagsSet("SuitTank", this.SafeCellFlagsToIgnoreOnEquipped);
			component.AddGasProvider(this);
		}
		targetGameObject.AddTag(GameTags.HasSuitTank);
	}

	// Token: 0x0600597D RID: 22909 RVA: 0x002051D0 File Offset: 0x002033D0
	private void OnUnequipped(object data)
	{
		Equipment equipment = (Equipment)data;
		if (!equipment.destroyed)
		{
			NameDisplayScreen.Instance.SetSuitTankDisplay(equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject(), new Func<float>(this.PercentFull), false);
			GameObject targetGameObject = equipment.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
			OxygenBreather component = targetGameObject.GetComponent<OxygenBreather>();
			if (component != null)
			{
				component.GetComponent<Sensors>().GetSensor<SafeCellSensor>().RemoveIgnoredFlagsSet("SuitTank");
				component.RemoveGasProvider(this);
			}
			targetGameObject.RemoveTag(GameTags.HasSuitTank);
		}
	}

	// Token: 0x0600597E RID: 22910 RVA: 0x00205250 File Offset: 0x00203450
	public void OnSetOxygenBreather(OxygenBreather oxygen_breather)
	{
	}

	// Token: 0x0600597F RID: 22911 RVA: 0x00205252 File Offset: 0x00203452
	public void OnClearOxygenBreather(OxygenBreather oxygen_breather)
	{
	}

	// Token: 0x06005980 RID: 22912 RVA: 0x00205254 File Offset: 0x00203454
	public bool ConsumeGas(OxygenBreather oxygen_breather, float amount)
	{
		if (this.IsEmpty())
		{
			return false;
		}
		float num = 0f;
		SimHashes simHashes = SimHashes.Vacuum;
		float num2;
		SimUtil.DiseaseInfo diseaseInfo;
		this.storage.ConsumeAndGetDisease(this.elementTag, amount, out num2, out diseaseInfo, out num, out simHashes);
		OxygenBreather.BreathableGasConsumed(oxygen_breather, simHashes, num2, num, diseaseInfo.idx, diseaseInfo.count);
		base.Trigger(608245985, base.gameObject);
		return true;
	}

	// Token: 0x06005981 RID: 22913 RVA: 0x002052B8 File Offset: 0x002034B8
	public bool ShouldEmitCO2()
	{
		bool flag = base.GetComponent<KPrefabID>().HasTag(GameTags.AirtightSuit);
		if (flag)
		{
			return false;
		}
		bool flag2 = this.IsOwnerBionic();
		return !flag && !flag2;
	}

	// Token: 0x06005982 RID: 22914 RVA: 0x002052EC File Offset: 0x002034EC
	public bool ShouldStoreCO2()
	{
		bool flag = base.GetComponent<KPrefabID>().HasTag(GameTags.AirtightSuit);
		if (!flag)
		{
			return false;
		}
		bool flag2 = this.IsOwnerBionic();
		return flag && !flag2;
	}

	// Token: 0x06005983 RID: 22915 RVA: 0x00205320 File Offset: 0x00203520
	public bool IsOwnerBionic()
	{
		bool flag = false;
		if (this.equippable != null && this.equippable.IsAssigned() && this.equippable.isEquipped)
		{
			Ownables soleOwner = this.equippable.assignee.GetSoleOwner();
			if (soleOwner != null)
			{
				GameObject targetGameObject = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject)
				{
					flag = targetGameObject.PrefabID() == BionicMinionConfig.ID;
				}
			}
		}
		return flag;
	}

	// Token: 0x06005984 RID: 22916 RVA: 0x0020539C File Offset: 0x0020359C
	public bool IsLowOxygen()
	{
		return this.NeedsRecharging();
	}

	// Token: 0x06005985 RID: 22917 RVA: 0x002053A4 File Offset: 0x002035A4
	[ContextMenu("SetToRefillAmount")]
	public void SetToRefillAmount()
	{
		float tankAmount = this.GetTankAmount();
		float num = 0.25f * this.capacity;
		if (tankAmount > num)
		{
			this.storage.ConsumeIgnoringDisease(this.elementTag, tankAmount - num);
		}
	}

	// Token: 0x06005986 RID: 22918 RVA: 0x002053DD File Offset: 0x002035DD
	[ContextMenu("Empty")]
	public void Empty()
	{
		this.storage.ConsumeIgnoringDisease(this.elementTag, this.GetTankAmount());
	}

	// Token: 0x06005987 RID: 22919 RVA: 0x002053F6 File Offset: 0x002035F6
	[ContextMenu("Fill Tank")]
	public void FillTank()
	{
		this.Empty();
		this.storage.AddGasChunk(SimHashes.Oxygen, this.capacity, 15f, 0, 0, false, false);
	}

	// Token: 0x06005988 RID: 22920 RVA: 0x0020541E File Offset: 0x0020361E
	public bool HasOxygen()
	{
		return !this.IsEmpty();
	}

	// Token: 0x06005989 RID: 22921 RVA: 0x00205429 File Offset: 0x00203629
	public bool IsBlocked()
	{
		return false;
	}

	// Token: 0x04003B5B RID: 15195
	public SafeCellQuery.SafeFlags SafeCellFlagsToIgnoreOnEquipped = (SafeCellQuery.SafeFlags)464;

	// Token: 0x04003B5C RID: 15196
	[Serialize]
	public string element;

	// Token: 0x04003B5D RID: 15197
	[Serialize]
	public float amount;

	// Token: 0x04003B5E RID: 15198
	public Tag elementTag;

	// Token: 0x04003B5F RID: 15199
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04003B60 RID: 15200
	public float capacity;

	// Token: 0x04003B61 RID: 15201
	public const float REFILL_PERCENT = 0.25f;

	// Token: 0x04003B62 RID: 15202
	public bool underwaterSupport;

	// Token: 0x04003B63 RID: 15203
	private Equippable equippable;

	// Token: 0x04003B64 RID: 15204
	private static readonly EventSystem.IntraObjectHandler<SuitTank> OnEquippedDelegate = new EventSystem.IntraObjectHandler<SuitTank>(delegate(SuitTank component, object data)
	{
		component.OnEquipped(data);
	});

	// Token: 0x04003B65 RID: 15205
	private static readonly EventSystem.IntraObjectHandler<SuitTank> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<SuitTank>(delegate(SuitTank component, object data)
	{
		component.OnUnequipped(data);
	});
}
