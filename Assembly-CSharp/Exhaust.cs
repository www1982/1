using System;
using UnityEngine;

// Token: 0x0200090F RID: 2319
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Exhaust")]
public class Exhaust : KMonoBehaviour, ISim200ms
{
	// Token: 0x06004088 RID: 16520 RVA: 0x00169FA2 File Offset: 0x001681A2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Exhaust>(-592767678, Exhaust.OnConduitStateChangedDelegate);
		base.Subscribe<Exhaust>(-111137758, Exhaust.OnConduitStateChangedDelegate);
		base.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.NoWire;
		this.simRenderLoadBalance = true;
	}

	// Token: 0x06004089 RID: 16521 RVA: 0x00169FDF File Offset: 0x001681DF
	protected override void OnSpawn()
	{
		this.OnConduitStateChanged(null);
	}

	// Token: 0x0600408A RID: 16522 RVA: 0x00169FE8 File Offset: 0x001681E8
	private void OnConduitStateChanged(object data)
	{
		this.operational.SetActive(this.operational.IsOperational && !this.vent.IsBlocked, false);
	}

	// Token: 0x0600408B RID: 16523 RVA: 0x0016A014 File Offset: 0x00168214
	private void CalculateDiseaseTransfer(PrimaryElement item1, PrimaryElement item2, float transfer_rate, out int disease_to_item1, out int disease_to_item2)
	{
		disease_to_item1 = (int)((float)item2.DiseaseCount * transfer_rate);
		disease_to_item2 = (int)((float)item1.DiseaseCount * transfer_rate);
	}

	// Token: 0x0600408C RID: 16524 RVA: 0x0016A030 File Offset: 0x00168230
	public void Sim200ms(float dt)
	{
		this.operational.SetFlag(Exhaust.canExhaust, !this.vent.IsBlocked);
		if (!this.operational.IsOperational)
		{
			if (this.isAnimating)
			{
				this.isAnimating = false;
				this.recentlyExhausted = false;
				base.Trigger(-793429877, null);
			}
			return;
		}
		this.UpdateEmission();
		this.elapsedSwitchTime -= dt;
		if (this.elapsedSwitchTime <= 0f)
		{
			this.elapsedSwitchTime = 1f;
			if (this.recentlyExhausted != this.isAnimating)
			{
				this.isAnimating = this.recentlyExhausted;
				base.Trigger(-793429877, null);
			}
			this.recentlyExhausted = false;
		}
	}

	// Token: 0x0600408D RID: 16525 RVA: 0x0016A0E4 File Offset: 0x001682E4
	public bool IsAnimating()
	{
		return this.isAnimating;
	}

	// Token: 0x0600408E RID: 16526 RVA: 0x0016A0EC File Offset: 0x001682EC
	private void UpdateEmission()
	{
		if (this.consumer.ConsumptionRate == 0f)
		{
			return;
		}
		if (this.storage.items.Count == 0)
		{
			return;
		}
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (Grid.Solid[num])
		{
			return;
		}
		ConduitType typeOfConduit = this.consumer.TypeOfConduit;
		if (typeOfConduit != ConduitType.Gas)
		{
			if (typeOfConduit == ConduitType.Liquid)
			{
				this.EmitLiquid(num);
				return;
			}
		}
		else
		{
			this.EmitGas(num);
		}
	}

	// Token: 0x0600408F RID: 16527 RVA: 0x0016A164 File Offset: 0x00168364
	private bool EmitCommon(int cell, PrimaryElement primary_element, Exhaust.EmitDelegate emit)
	{
		if (primary_element.Mass <= 0f)
		{
			return false;
		}
		int num;
		int num2;
		this.CalculateDiseaseTransfer(this.exhaustPE, primary_element, 0.05f, out num, out num2);
		primary_element.ModifyDiseaseCount(-num, "Exhaust transfer");
		primary_element.AddDisease(this.exhaustPE.DiseaseIdx, num2, "Exhaust transfer");
		this.exhaustPE.ModifyDiseaseCount(-num2, "Exhaust transfer");
		this.exhaustPE.AddDisease(primary_element.DiseaseIdx, num, "Exhaust transfer");
		emit(cell, primary_element);
		if (this.vent != null)
		{
			this.vent.UpdateVentedMass(primary_element.ElementID, primary_element.Mass);
		}
		primary_element.KeepZeroMassObject = true;
		primary_element.Mass = 0f;
		primary_element.ModifyDiseaseCount(int.MinValue, "Exhaust.SimUpdate");
		if (this.lastElementEmmited != primary_element.ElementID)
		{
			this.lastElementEmmited = primary_element.ElementID;
			if (primary_element.Element != null && primary_element.Element.substance != null)
			{
				base.Trigger(-793429877, primary_element.Element.substance.colour);
			}
		}
		this.recentlyExhausted = true;
		return true;
	}

	// Token: 0x06004090 RID: 16528 RVA: 0x0016A28C File Offset: 0x0016848C
	private void EmitLiquid(int cell)
	{
		int num = Grid.CellBelow(cell);
		Exhaust.EmitDelegate emitDelegate = ((Grid.IsValidCell(num) && !Grid.Solid[num]) ? Exhaust.emit_particle : Exhaust.emit_element);
		foreach (GameObject gameObject in this.storage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			if (component.Element.IsLiquid && this.EmitCommon(cell, component, emitDelegate))
			{
				break;
			}
		}
	}

	// Token: 0x06004091 RID: 16529 RVA: 0x0016A328 File Offset: 0x00168528
	private void EmitGas(int cell)
	{
		foreach (GameObject gameObject in this.storage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			if (component.Element.IsGas && this.EmitCommon(cell, component, Exhaust.emit_element))
			{
				break;
			}
		}
	}

	// Token: 0x04002840 RID: 10304
	[MyCmpGet]
	private Vent vent;

	// Token: 0x04002841 RID: 10305
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04002842 RID: 10306
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002843 RID: 10307
	[MyCmpGet]
	private ConduitConsumer consumer;

	// Token: 0x04002844 RID: 10308
	[MyCmpGet]
	private PrimaryElement exhaustPE;

	// Token: 0x04002845 RID: 10309
	private static readonly Operational.Flag canExhaust = new Operational.Flag("canExhaust", Operational.Flag.Type.Requirement);

	// Token: 0x04002846 RID: 10310
	private bool isAnimating;

	// Token: 0x04002847 RID: 10311
	private bool recentlyExhausted;

	// Token: 0x04002848 RID: 10312
	private const float MinSwitchTime = 1f;

	// Token: 0x04002849 RID: 10313
	private float elapsedSwitchTime;

	// Token: 0x0400284A RID: 10314
	private SimHashes lastElementEmmited;

	// Token: 0x0400284B RID: 10315
	private static readonly EventSystem.IntraObjectHandler<Exhaust> OnConduitStateChangedDelegate = new EventSystem.IntraObjectHandler<Exhaust>(delegate(Exhaust component, object data)
	{
		component.OnConduitStateChanged(data);
	});

	// Token: 0x0400284C RID: 10316
	private static Exhaust.EmitDelegate emit_element = delegate(int cell, PrimaryElement primary_element)
	{
		SimMessages.AddRemoveSubstance(cell, primary_element.ElementID, CellEventLogger.Instance.ExhaustSimUpdate, primary_element.Mass, primary_element.Temperature, primary_element.DiseaseIdx, primary_element.DiseaseCount, true, -1);
	};

	// Token: 0x0400284D RID: 10317
	private static Exhaust.EmitDelegate emit_particle = delegate(int cell, PrimaryElement primary_element)
	{
		FallingWater.instance.AddParticle(cell, primary_element.Element.idx, primary_element.Mass, primary_element.Temperature, primary_element.DiseaseIdx, primary_element.DiseaseCount, true, false, true, false);
	};

	// Token: 0x020018A7 RID: 6311
	// (Invoke) Token: 0x06009D46 RID: 40262
	private delegate void EmitDelegate(int cell, PrimaryElement primary_element);
}
