using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000BAA RID: 2986
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Sublimates")]
public class Sublimates : KMonoBehaviour, ISim200ms
{
	// Token: 0x17000683 RID: 1667
	// (get) Token: 0x06005941 RID: 22849 RVA: 0x00203F00 File Offset: 0x00202100
	public float Temperature
	{
		get
		{
			return this.primaryElement.Temperature;
		}
	}

	// Token: 0x06005942 RID: 22850 RVA: 0x00203F0D File Offset: 0x0020210D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Sublimates>(-2064133523, Sublimates.OnAbsorbDelegate);
		base.Subscribe<Sublimates>(1335436905, Sublimates.OnSplitFromChunkDelegate);
		this.simRenderLoadBalance = true;
	}

	// Token: 0x06005943 RID: 22851 RVA: 0x00203F3E File Offset: 0x0020213E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.flowAccumulator = Game.Instance.accumulators.Add("EmittedMass", this);
		this.RefreshStatusItem(Sublimates.EmitState.Emitting);
	}

	// Token: 0x06005944 RID: 22852 RVA: 0x00203F68 File Offset: 0x00202168
	protected override void OnCleanUp()
	{
		this.flowAccumulator = Game.Instance.accumulators.Remove(this.flowAccumulator);
		base.OnCleanUp();
	}

	// Token: 0x06005945 RID: 22853 RVA: 0x00203F8C File Offset: 0x0020218C
	private void OnAbsorb(object data)
	{
		Pickupable pickupable = (Pickupable)data;
		if (pickupable != null)
		{
			Sublimates component = pickupable.GetComponent<Sublimates>();
			if (component != null)
			{
				this.sublimatedMass += component.sublimatedMass;
			}
		}
	}

	// Token: 0x06005946 RID: 22854 RVA: 0x00203FCC File Offset: 0x002021CC
	private void OnSplitFromChunk(object data)
	{
		Pickupable pickupable = data as Pickupable;
		PrimaryElement primaryElement = pickupable.PrimaryElement;
		Sublimates component = pickupable.GetComponent<Sublimates>();
		if (component == null)
		{
			return;
		}
		float mass = this.primaryElement.Mass;
		float mass2 = primaryElement.Mass;
		float num = mass / (mass2 + mass);
		this.sublimatedMass = component.sublimatedMass * num;
		float num2 = 1f - num;
		component.sublimatedMass *= num2;
	}

	// Token: 0x06005947 RID: 22855 RVA: 0x00204038 File Offset: 0x00202238
	private unsafe bool SimMightOffcellOverpressure(int cell, SimHashes offgass)
	{
		SimHashes id = Grid.Element[cell].id;
		if (id == offgass || id == SimHashes.Vacuum)
		{
			return false;
		}
		IntPtr intPtr = stackalloc byte[(UIntPtr)12];
		*intPtr = Grid.CellLeft(cell);
		*(intPtr + 4) = Grid.CellRight(cell);
		*(intPtr + (IntPtr)2 * 4) = Grid.CellAbove(cell);
		ReadOnlySpan<int> readOnlySpan = new Span<int>(intPtr, 3);
		bool flag = false;
		ReadOnlySpan<int> readOnlySpan2 = readOnlySpan;
		for (int i = 0; i < readOnlySpan2.Length; i++)
		{
			int num = *readOnlySpan2[i];
			if (Grid.IsValidCell(num))
			{
				if (Grid.Element[num].id == id)
				{
					return false;
				}
				if (Grid.Element[num].id == offgass)
				{
					flag = true;
					if (Grid.Mass[num] < this.info.maxDestinationMass)
					{
						return false;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x06005948 RID: 22856 RVA: 0x002040F8 File Offset: 0x002022F8
	public void Sim200ms(float dt)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		bool flag = this.HasTag(GameTags.Sealed);
		Pickupable component = base.GetComponent<Pickupable>();
		Storage storage = ((component != null) ? component.storage : null);
		if (flag && !this.decayStorage)
		{
			return;
		}
		if (flag && storage != null && storage.HasTag(GameTags.CorrosionProof))
		{
			return;
		}
		Element element = ElementLoader.FindElementByHash(this.info.sublimatedElement);
		if (this.primaryElement.Temperature <= element.lowTemp)
		{
			this.RefreshStatusItem(Sublimates.EmitState.BlockedOnTemperature);
			return;
		}
		float num2 = Grid.Mass[num];
		if (num2 < this.info.maxDestinationMass)
		{
			float num3 = this.primaryElement.Mass;
			if (num3 > 0f)
			{
				float num4 = Mathf.Pow(num3, this.info.massPower);
				float num5 = Mathf.Max(this.info.sublimationRate, this.info.sublimationRate * num4);
				num5 *= dt;
				num5 = Mathf.Min(num5, num3);
				this.sublimatedMass += num5;
				num3 -= num5;
				if (this.sublimatedMass > this.info.minSublimationAmount)
				{
					float num6 = this.sublimatedMass / this.primaryElement.Mass;
					byte b;
					int num7;
					if (this.info.diseaseIdx == 255)
					{
						b = this.primaryElement.DiseaseIdx;
						num7 = (int)((float)this.primaryElement.DiseaseCount * num6);
						this.primaryElement.ModifyDiseaseCount(-num7, "Sublimates.SimUpdate");
					}
					else
					{
						float num8 = this.sublimatedMass / this.info.sublimationRate;
						b = this.info.diseaseIdx;
						num7 = (int)((float)this.info.diseaseCount * num8);
					}
					float num9 = Mathf.Min(this.sublimatedMass, this.info.maxDestinationMass - num2);
					if (num9 <= 0f || this.SimMightOffcellOverpressure(num, element.id))
					{
						this.RefreshStatusItem(Sublimates.EmitState.BlockedOnPressure);
						return;
					}
					this.Emit(num, num9, this.primaryElement.Temperature, b, num7);
					this.sublimatedMass = Mathf.Max(0f, this.sublimatedMass - num9);
					this.primaryElement.Mass = Mathf.Max(0f, this.primaryElement.Mass - num9);
					this.UpdateStorage();
					this.RefreshStatusItem(Sublimates.EmitState.Emitting);
					if (flag && this.decayStorage && storage != null)
					{
						storage.Trigger(-794517298, new BuildingHP.DamageSourceInfo
						{
							damage = 1,
							source = BUILDINGS.DAMAGESOURCES.CORROSIVE_ELEMENT,
							popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.CORROSIVE_ELEMENT,
							fullDamageEffectName = "smoke_damage_kanim"
						});
						return;
					}
				}
			}
			else if (this.sublimatedMass > 0f)
			{
				float num10 = Mathf.Min(this.sublimatedMass, this.info.maxDestinationMass - num2);
				if (num10 > 0f && !this.SimMightOffcellOverpressure(num, element.id))
				{
					this.Emit(num, num10, this.primaryElement.Temperature, this.primaryElement.DiseaseIdx, this.primaryElement.DiseaseCount);
					this.sublimatedMass = Mathf.Max(0f, this.sublimatedMass - num10);
					this.primaryElement.Mass = Mathf.Max(0f, this.primaryElement.Mass - num10);
					this.UpdateStorage();
					this.RefreshStatusItem(Sublimates.EmitState.Emitting);
					return;
				}
				this.RefreshStatusItem(Sublimates.EmitState.BlockedOnPressure);
				return;
			}
			else if (!this.primaryElement.KeepZeroMassObject)
			{
				Util.KDestroyGameObject(base.gameObject);
				return;
			}
		}
		else
		{
			this.RefreshStatusItem(Sublimates.EmitState.BlockedOnPressure);
		}
	}

	// Token: 0x06005949 RID: 22857 RVA: 0x002044D0 File Offset: 0x002026D0
	private void UpdateStorage()
	{
		Pickupable component = base.GetComponent<Pickupable>();
		if (component != null && component.storage != null)
		{
			component.storage.Trigger(-1697596308, base.gameObject);
		}
	}

	// Token: 0x0600594A RID: 22858 RVA: 0x00204514 File Offset: 0x00202714
	private void Emit(int cell, float mass, float temperature, byte disease_idx, int disease_count)
	{
		SimMessages.AddRemoveSubstance(cell, this.info.sublimatedElement, CellEventLogger.Instance.SublimatesEmit, mass, temperature, disease_idx, disease_count, true, -1);
		Game.Instance.accumulators.Accumulate(this.flowAccumulator, mass);
		if (this.spawnFXHash != SpawnFXHashes.None)
		{
			base.transform.GetPosition().z = Grid.GetLayerZ(Grid.SceneLayer.Front);
			Game.Instance.SpawnFX(this.spawnFXHash, base.transform.GetPosition(), 0f);
		}
	}

	// Token: 0x0600594B RID: 22859 RVA: 0x0020459C File Offset: 0x0020279C
	public float AvgFlowRate()
	{
		return Game.Instance.accumulators.GetAverageRate(this.flowAccumulator);
	}

	// Token: 0x0600594C RID: 22860 RVA: 0x002045B4 File Offset: 0x002027B4
	private void RefreshStatusItem(Sublimates.EmitState newEmitState)
	{
		if (newEmitState == this.lastEmitState)
		{
			return;
		}
		switch (newEmitState)
		{
		case Sublimates.EmitState.Emitting:
			if (this.info.sublimatedElement == SimHashes.Oxygen)
			{
				this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmittingOxygenAvg, this);
			}
			else
			{
				this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmittingGasAvg, this);
			}
			break;
		case Sublimates.EmitState.BlockedOnPressure:
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmittingBlockedHighPressure, this);
			break;
		case Sublimates.EmitState.BlockedOnTemperature:
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.EmittingBlockedLowTemperature, this);
			break;
		}
		this.lastEmitState = newEmitState;
	}

	// Token: 0x04003B39 RID: 15161
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04003B3A RID: 15162
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04003B3B RID: 15163
	[SerializeField]
	public SpawnFXHashes spawnFXHash;

	// Token: 0x04003B3C RID: 15164
	public bool decayStorage;

	// Token: 0x04003B3D RID: 15165
	[SerializeField]
	public Sublimates.Info info;

	// Token: 0x04003B3E RID: 15166
	[Serialize]
	private float sublimatedMass;

	// Token: 0x04003B3F RID: 15167
	private HandleVector<int>.Handle flowAccumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04003B40 RID: 15168
	private Sublimates.EmitState lastEmitState = (Sublimates.EmitState)(-1);

	// Token: 0x04003B41 RID: 15169
	private static readonly EventSystem.IntraObjectHandler<Sublimates> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<Sublimates>(delegate(Sublimates component, object data)
	{
		component.OnAbsorb(data);
	});

	// Token: 0x04003B42 RID: 15170
	private static readonly EventSystem.IntraObjectHandler<Sublimates> OnSplitFromChunkDelegate = new EventSystem.IntraObjectHandler<Sublimates>(delegate(Sublimates component, object data)
	{
		component.OnSplitFromChunk(data);
	});

	// Token: 0x02001CDD RID: 7389
	[Serializable]
	public struct Info
	{
		// Token: 0x0600AC60 RID: 44128 RVA: 0x003C1A94 File Offset: 0x003BFC94
		public Info(float rate, float min_amount, float max_destination_mass, float mass_power, SimHashes element, byte disease_idx = 255, int disease_count = 0)
		{
			this.sublimationRate = rate;
			this.minSublimationAmount = min_amount;
			this.maxDestinationMass = max_destination_mass;
			this.massPower = mass_power;
			this.sublimatedElement = element;
			this.diseaseIdx = disease_idx;
			this.diseaseCount = disease_count;
		}

		// Token: 0x0400878E RID: 34702
		public float sublimationRate;

		// Token: 0x0400878F RID: 34703
		public float minSublimationAmount;

		// Token: 0x04008790 RID: 34704
		public float maxDestinationMass;

		// Token: 0x04008791 RID: 34705
		public float massPower;

		// Token: 0x04008792 RID: 34706
		public byte diseaseIdx;

		// Token: 0x04008793 RID: 34707
		public int diseaseCount;

		// Token: 0x04008794 RID: 34708
		[HashedEnum]
		public SimHashes sublimatedElement;
	}

	// Token: 0x02001CDE RID: 7390
	private enum EmitState
	{
		// Token: 0x04008796 RID: 34710
		Emitting,
		// Token: 0x04008797 RID: 34711
		BlockedOnPressure,
		// Token: 0x04008798 RID: 34712
		BlockedOnTemperature
	}
}
