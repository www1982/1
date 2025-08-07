using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000A4E RID: 2638
[RequireComponent(typeof(Health))]
[AddComponentMenu("KMonoBehaviour/scripts/OxygenBreather")]
public class OxygenBreather : KMonoBehaviour, ISim200ms
{
	// Token: 0x17000538 RID: 1336
	// (get) Token: 0x06004C75 RID: 19573 RVA: 0x001BBAF1 File Offset: 0x001B9CF1
	// (set) Token: 0x06004C74 RID: 19572 RVA: 0x001BBAE8 File Offset: 0x001B9CE8
	public KPrefabID prefabID { get; private set; }

	// Token: 0x17000539 RID: 1337
	// (get) Token: 0x06004C76 RID: 19574 RVA: 0x001BBAF9 File Offset: 0x001B9CF9
	public float ConsumptionRate
	{
		get
		{
			if (this.airConsumptionRate != null)
			{
				return this.airConsumptionRate.GetTotalValue();
			}
			return 0f;
		}
	}

	// Token: 0x1700053A RID: 1338
	// (get) Token: 0x06004C77 RID: 19575 RVA: 0x001BBB14 File Offset: 0x001B9D14
	public float CO2EmitRate
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.co2Accumulator);
		}
	}

	// Token: 0x1700053B RID: 1339
	// (get) Token: 0x06004C78 RID: 19576 RVA: 0x001BBB2B File Offset: 0x001B9D2B
	public HandleVector<int>.Handle O2Accumulator
	{
		get
		{
			return this.o2Accumulator;
		}
	}

	// Token: 0x06004C79 RID: 19577 RVA: 0x001BBB34 File Offset: 0x001B9D34
	public OxygenBreather.IGasProvider GetCurrentGasProvider()
	{
		if (this.gasProviders.Count == 0)
		{
			return null;
		}
		OxygenBreather.IGasProvider gasProvider = null;
		for (int i = this.gasProviders.Count - 1; i >= 0; i--)
		{
			OxygenBreather.IGasProvider gasProvider2 = this.gasProviders[i];
			if (!gasProvider2.IsBlocked())
			{
				gasProvider = gasProvider2;
				if (gasProvider2.HasOxygen())
				{
					break;
				}
			}
		}
		return gasProvider;
	}

	// Token: 0x06004C7A RID: 19578 RVA: 0x001BBB8C File Offset: 0x001B9D8C
	public bool IsLowOxygen()
	{
		OxygenBreather.IGasProvider currentGasProvider = this.GetCurrentGasProvider();
		return currentGasProvider == null || currentGasProvider.IsLowOxygen();
	}

	// Token: 0x1700053C RID: 1340
	// (get) Token: 0x06004C7B RID: 19579 RVA: 0x001BBBAB File Offset: 0x001B9DAB
	public bool HasOxygen
	{
		get
		{
			return this.hasAir;
		}
	}

	// Token: 0x1700053D RID: 1341
	// (get) Token: 0x06004C7C RID: 19580 RVA: 0x001BBBB3 File Offset: 0x001B9DB3
	public bool IsOutOfOxygen
	{
		get
		{
			return !this.hasAir;
		}
	}

	// Token: 0x06004C7D RID: 19581 RVA: 0x001BBBBE File Offset: 0x001B9DBE
	protected override void OnPrefabInit()
	{
		GameUtil.SubscribeToTags<OxygenBreather>(this, OxygenBreather.OnDeadTagAddedDelegate, true);
		this.prefabID = base.GetComponent<KPrefabID>();
	}

	// Token: 0x06004C7E RID: 19582 RVA: 0x001BBBD8 File Offset: 0x001B9DD8
	protected override void OnSpawn()
	{
		this.airConsumptionRate = Db.Get().Attributes.AirConsumptionRate.Lookup(this);
		this.o2Accumulator = Game.Instance.accumulators.Add("O2", this);
		this.co2Accumulator = Game.Instance.accumulators.Add("CO2", this);
		bool flag = base.gameObject.PrefabID() == BionicMinionConfig.ID;
		KSelectable component = base.GetComponent<KSelectable>();
		this.o2StatusItem = component.AddStatusItem(flag ? Db.Get().DuplicantStatusItems.BreathingO2Bionic : Db.Get().DuplicantStatusItems.BreathingO2, this);
		this.cO2StatusItem = component.AddStatusItem(Db.Get().DuplicantStatusItems.EmittingCO2, this);
		this.temperature = Db.Get().Amounts.Temperature.Lookup(this);
		NameDisplayScreen.Instance.RegisterComponent(base.gameObject, this, false);
	}

	// Token: 0x06004C7F RID: 19583 RVA: 0x001BBCD4 File Offset: 0x001B9ED4
	private void BreathableGasConsumed(SimHashes elementConsumed, float massConsumed, float temperature, byte disseaseIDX, int disseaseCount)
	{
		if (this.prefabID.HasTag(GameTags.Dead) || this.O2Accumulator == HandleVector<int>.Handle.InvalidHandle)
		{
			return;
		}
		if (elementConsumed == SimHashes.ContaminatedOxygen)
		{
			base.Trigger(-935848905, massConsumed);
		}
		Game.Instance.accumulators.Accumulate(this.O2Accumulator, massConsumed);
		float num = -massConsumed;
		ReportManager.Instance.ReportValue(ReportManager.ReportType.OxygenCreated, num, base.gameObject.GetProperName(), null);
		if (this.onBreathableGasConsumed != null)
		{
			this.onBreathableGasConsumed(elementConsumed, massConsumed, temperature, disseaseIDX, disseaseCount);
		}
	}

	// Token: 0x06004C80 RID: 19584 RVA: 0x001BBD6B File Offset: 0x001B9F6B
	public static void BreathableGasConsumed(OxygenBreather breather, SimHashes elementConsumed, float massConsumed, float temperature, byte disseaseIDX, int disseaseCount)
	{
		if (breather != null)
		{
			breather.BreathableGasConsumed(elementConsumed, massConsumed, temperature, disseaseIDX, disseaseCount);
		}
	}

	// Token: 0x06004C81 RID: 19585 RVA: 0x001BBD84 File Offset: 0x001B9F84
	public void Sim200ms(float dt)
	{
		if (!base.gameObject.HasTag(GameTags.Dead))
		{
			float num = this.airConsumptionRate.GetTotalValue() * dt;
			OxygenBreather.IGasProvider currentGasProvider = this.GetCurrentGasProvider();
			bool flag = currentGasProvider != null && currentGasProvider.ConsumeGas(this, num);
			if (flag)
			{
				if (currentGasProvider.ShouldEmitCO2())
				{
					if (this.cO2StatusItem != Guid.Empty)
					{
						this.cO2StatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.EmittingCO2, this);
					}
					float num2 = num * this.O2toCO2conversion;
					Game.Instance.accumulators.Accumulate(this.co2Accumulator, num2);
					this.accumulatedCO2 += num2;
					if (this.accumulatedCO2 >= this.minCO2ToEmit)
					{
						this.accumulatedCO2 -= this.minCO2ToEmit;
						Vector3 position = base.transform.GetPosition();
						Vector3 vector = position;
						vector.x += (this.facing.GetFacing() ? (-this.mouthOffset.x) : this.mouthOffset.x);
						vector.y += this.mouthOffset.y;
						vector.z -= 0.5f;
						if (Mathf.FloorToInt(vector.x) != Mathf.FloorToInt(position.x))
						{
							vector.x = Mathf.Floor(position.x) + (this.facing.GetFacing() ? 0.01f : 0.99f);
						}
						CO2Manager.instance.SpawnBreath(vector, this.minCO2ToEmit, this.temperature.value, this.facing.GetFacing());
					}
				}
				else if (currentGasProvider.ShouldStoreCO2())
				{
					if (this.cO2StatusItem != Guid.Empty)
					{
						this.cO2StatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.EmittingCO2, this);
					}
					Equippable equippable = base.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
					if (equippable != null)
					{
						float num3 = num * this.O2toCO2conversion;
						Game.Instance.accumulators.Accumulate(this.co2Accumulator, num3);
						this.accumulatedCO2 += num3;
						if (this.accumulatedCO2 >= this.minCO2ToEmit)
						{
							this.accumulatedCO2 -= this.minCO2ToEmit;
							equippable.GetComponent<Storage>().AddGasChunk(SimHashes.CarbonDioxide, this.minCO2ToEmit, this.temperature.value, byte.MaxValue, 0, false, true);
						}
					}
				}
				else if (this.cO2StatusItem != Guid.Empty)
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(this.cO2StatusItem, false);
					this.cO2StatusItem = Guid.Empty;
				}
			}
			if (flag != this.hasAir)
			{
				this.hasAirTimer.Start();
				if (this.hasAirTimer.TryStop(2f))
				{
					this.hasAir = flag;
					base.Trigger(-933153513, this.hasAir);
					return;
				}
			}
			else
			{
				this.hasAirTimer.Stop();
			}
		}
	}

	// Token: 0x06004C82 RID: 19586 RVA: 0x001BC090 File Offset: 0x001BA290
	public void AddGasProvider(OxygenBreather.IGasProvider gas_provider)
	{
		global::Debug.Assert(gas_provider != null, "Error at OxygenBreather.cs  adding gas provider, the gas provider param is null!");
		global::Debug.Assert(!this.gasProviders.Contains(gas_provider), "Error at OxygenBreather.cs adding gas provider, the gas provider was already added to the gas providers list!");
		this.gasProviders.Add(gas_provider);
		gas_provider.OnSetOxygenBreather(this);
	}

	// Token: 0x06004C83 RID: 19587 RVA: 0x001BC0CC File Offset: 0x001BA2CC
	public bool RemoveGasProvider(OxygenBreather.IGasProvider provider)
	{
		if (this.gasProviders.Count > 0 && this.gasProviders.Contains(provider))
		{
			OxygenBreather.IGasProvider gasProvider = this.gasProviders[this.gasProviders.Count - 1];
			this.gasProviders.Remove(provider);
			provider.OnClearOxygenBreather(this);
			return true;
		}
		return false;
	}

	// Token: 0x06004C84 RID: 19588 RVA: 0x001BC125 File Offset: 0x001BA325
	private void OnDeath(object data)
	{
		base.enabled = false;
		KSelectable component = base.GetComponent<KSelectable>();
		component.RemoveStatusItem(Db.Get().DuplicantStatusItems.BreathingO2, false);
		component.RemoveStatusItem(Db.Get().DuplicantStatusItems.EmittingCO2, false);
	}

	// Token: 0x06004C85 RID: 19589 RVA: 0x001BC164 File Offset: 0x001BA364
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.o2Accumulator);
		Game.Instance.accumulators.Remove(this.co2Accumulator);
		this.o2Accumulator = HandleVector<int>.InvalidHandle;
		this.co2Accumulator = HandleVector<int>.InvalidHandle;
		while (this.gasProviders.Count > 0)
		{
			OxygenBreather.IGasProvider gasProvider = this.gasProviders[this.gasProviders.Count - 1];
			this.RemoveGasProvider(gasProvider);
		}
		base.OnCleanUp();
	}

	// Token: 0x040032A6 RID: 12966
	public float O2toCO2conversion = 0.5f;

	// Token: 0x040032A7 RID: 12967
	public Vector2 mouthOffset;

	// Token: 0x040032A8 RID: 12968
	[Serialize]
	public float accumulatedCO2;

	// Token: 0x040032A9 RID: 12969
	[SerializeField]
	public float minCO2ToEmit = 0.3f;

	// Token: 0x040032AA RID: 12970
	private bool hasAir = true;

	// Token: 0x040032AB RID: 12971
	private Timer hasAirTimer = new Timer();

	// Token: 0x040032AC RID: 12972
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x040032AD RID: 12973
	[MyCmpGet]
	private Facing facing;

	// Token: 0x040032AF RID: 12975
	private HandleVector<int>.Handle o2Accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x040032B0 RID: 12976
	private HandleVector<int>.Handle co2Accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x040032B1 RID: 12977
	private AmountInstance temperature;

	// Token: 0x040032B2 RID: 12978
	public float lowOxygenThreshold;

	// Token: 0x040032B3 RID: 12979
	public float noOxygenThreshold;

	// Token: 0x040032B4 RID: 12980
	private AttributeInstance airConsumptionRate;

	// Token: 0x040032B5 RID: 12981
	public Action<SimHashes, float, float, byte, int> onBreathableGasConsumed;

	// Token: 0x040032B6 RID: 12982
	private static readonly EventSystem.IntraObjectHandler<OxygenBreather> OnDeadTagAddedDelegate = GameUtil.CreateHasTagHandler<OxygenBreather>(GameTags.Dead, delegate(OxygenBreather component, object data)
	{
		component.OnDeath(data);
	});

	// Token: 0x040032B7 RID: 12983
	private List<OxygenBreather.IGasProvider> gasProviders = new List<OxygenBreather.IGasProvider>();

	// Token: 0x040032B8 RID: 12984
	private Guid o2StatusItem;

	// Token: 0x040032B9 RID: 12985
	private Guid cO2StatusItem;

	// Token: 0x02001B05 RID: 6917
	public interface IGasProvider
	{
		// Token: 0x0600A5D3 RID: 42451
		void OnSetOxygenBreather(OxygenBreather oxygen_breather);

		// Token: 0x0600A5D4 RID: 42452
		void OnClearOxygenBreather(OxygenBreather oxygen_breather);

		// Token: 0x0600A5D5 RID: 42453
		bool ConsumeGas(OxygenBreather oxygen_breather, float amount);

		// Token: 0x0600A5D6 RID: 42454
		bool ShouldEmitCO2();

		// Token: 0x0600A5D7 RID: 42455
		bool ShouldStoreCO2();

		// Token: 0x0600A5D8 RID: 42456
		bool IsLowOxygen();

		// Token: 0x0600A5D9 RID: 42457
		bool HasOxygen();

		// Token: 0x0600A5DA RID: 42458
		bool IsBlocked();
	}
}
