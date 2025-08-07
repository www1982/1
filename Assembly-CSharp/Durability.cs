using System;
using Klei.CustomSettings;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x020008CD RID: 2253
[AddComponentMenu("KMonoBehaviour/scripts/Durability")]
public class Durability : KMonoBehaviour
{
	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x06003E7B RID: 15995 RVA: 0x0015D8AC File Offset: 0x0015BAAC
	// (set) Token: 0x06003E7C RID: 15996 RVA: 0x0015D8B4 File Offset: 0x0015BAB4
	public float TimeEquipped
	{
		get
		{
			return this.timeEquipped;
		}
		set
		{
			this.timeEquipped = value;
		}
	}

	// Token: 0x06003E7D RID: 15997 RVA: 0x0015D8BD File Offset: 0x0015BABD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Durability>(-1617557748, Durability.OnEquippedDelegate);
		base.Subscribe<Durability>(-170173755, Durability.OnUnequippedDelegate);
	}

	// Token: 0x06003E7E RID: 15998 RVA: 0x0015D8E8 File Offset: 0x0015BAE8
	protected override void OnSpawn()
	{
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.Durability, base.gameObject);
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.Durability);
		if (currentQualitySetting != null)
		{
			string id = currentQualitySetting.id;
			if (id == "Indestructible")
			{
				this.difficultySettingMod = EQUIPMENT.SUITS.INDESTRUCTIBLE_DURABILITY_MOD;
				return;
			}
			if (id == "Reinforced")
			{
				this.difficultySettingMod = EQUIPMENT.SUITS.REINFORCED_DURABILITY_MOD;
				return;
			}
			if (id == "Flimsy")
			{
				this.difficultySettingMod = EQUIPMENT.SUITS.FLIMSY_DURABILITY_MOD;
				return;
			}
			if (!(id == "Threadbare"))
			{
				return;
			}
			this.difficultySettingMod = EQUIPMENT.SUITS.THREADBARE_DURABILITY_MOD;
		}
	}

	// Token: 0x06003E7F RID: 15999 RVA: 0x0015D994 File Offset: 0x0015BB94
	private void OnEquipped()
	{
		if (!this.isEquipped)
		{
			this.isEquipped = true;
			this.timeEquipped = GameClock.Instance.GetTimeInCycles();
		}
	}

	// Token: 0x06003E80 RID: 16000 RVA: 0x0015D9B8 File Offset: 0x0015BBB8
	private void OnUnequipped()
	{
		if (this.isEquipped)
		{
			this.isEquipped = false;
			float num = GameClock.Instance.GetTimeInCycles() - this.timeEquipped;
			this.DeltaDurability(num * this.durabilityLossPerCycle);
		}
	}

	// Token: 0x06003E81 RID: 16001 RVA: 0x0015D9F4 File Offset: 0x0015BBF4
	private void DeltaDurability(float delta)
	{
		delta *= this.difficultySettingMod;
		this.durability = Mathf.Clamp01(this.durability + delta);
	}

	// Token: 0x06003E82 RID: 16002 RVA: 0x0015DA14 File Offset: 0x0015BC14
	public void ConvertToWornObject()
	{
		GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(this.wornEquipmentPrefabID), Grid.SceneLayer.Ore, null, 0);
		gameObject.transform.SetPosition(base.transform.GetPosition());
		gameObject.GetComponent<PrimaryElement>().SetElement(base.GetComponent<PrimaryElement>().ElementID, false);
		gameObject.SetActive(true);
		EquippableFacade component = base.GetComponent<EquippableFacade>();
		if (component != null)
		{
			gameObject.GetComponent<RepairableEquipment>().facadeID = component.FacadeID;
		}
		Storage component2 = base.gameObject.GetComponent<Storage>();
		if (component2)
		{
			JetSuitTank component3 = base.gameObject.GetComponent<JetSuitTank>();
			if (component3)
			{
				component2.AddLiquid(SimHashes.Petroleum, component3.amount, base.GetComponent<PrimaryElement>().Temperature, byte.MaxValue, 0, false, true);
			}
			component2.DropAll(false, false, default(Vector3), true, null);
		}
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06003E83 RID: 16003 RVA: 0x0015DB00 File Offset: 0x0015BD00
	public float GetDurability()
	{
		if (this.isEquipped)
		{
			float num = GameClock.Instance.GetTimeInCycles() - this.timeEquipped;
			return this.durability - num * this.durabilityLossPerCycle;
		}
		return this.durability;
	}

	// Token: 0x06003E84 RID: 16004 RVA: 0x0015DB3D File Offset: 0x0015BD3D
	public bool IsWornOut()
	{
		return this.GetDurability() <= 0f;
	}

	// Token: 0x04002674 RID: 9844
	private static readonly EventSystem.IntraObjectHandler<Durability> OnEquippedDelegate = new EventSystem.IntraObjectHandler<Durability>(delegate(Durability component, object data)
	{
		component.OnEquipped();
	});

	// Token: 0x04002675 RID: 9845
	private static readonly EventSystem.IntraObjectHandler<Durability> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<Durability>(delegate(Durability component, object data)
	{
		component.OnUnequipped();
	});

	// Token: 0x04002676 RID: 9846
	[Serialize]
	private bool isEquipped;

	// Token: 0x04002677 RID: 9847
	[Serialize]
	private float timeEquipped;

	// Token: 0x04002678 RID: 9848
	[Serialize]
	private float durability = 1f;

	// Token: 0x04002679 RID: 9849
	public float durabilityLossPerCycle = -0.1f;

	// Token: 0x0400267A RID: 9850
	public string wornEquipmentPrefabID;

	// Token: 0x0400267B RID: 9851
	private float difficultySettingMod = 1f;
}
