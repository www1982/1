using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000720 RID: 1824
public class ElectrobankDischarger : Generator
{
	// Token: 0x17000279 RID: 633
	// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x001077EC File Offset: 0x001059EC
	public float ElectrobankJoulesStored
	{
		get
		{
			float num = 0f;
			foreach (Electrobank electrobank in this.storedCells)
			{
				num += electrobank.Charge;
			}
			return num;
		}
	}

	// Token: 0x06002DF2 RID: 11762 RVA: 0x00107848 File Offset: 0x00105A48
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new ElectrobankDischarger.StatesInstance(this);
		this.smi.StartSM();
		base.Subscribe(-1697596308, new Action<object>(this.OnStorageChange));
		this.RefreshCells(null);
		this.filteredStorage = new FilteredStorage(this, null, null, false, Db.Get().ChoreTypes.PowerFetch);
		this.filteredStorage.SetHasMeter(false);
		this.filteredStorage.FilterChanged();
		Storage storage = this.storage;
		storage.onDestroyItemsDropped = (Action<List<GameObject>>)Delegate.Combine(storage.onDestroyItemsDropped, new Action<List<GameObject>>(this.OnBatteriesDroppedFromDeconstruction));
		this.UpdateSymbolSwap();
	}

	// Token: 0x06002DF3 RID: 11763 RVA: 0x001078F4 File Offset: 0x00105AF4
	private void OnBatteriesDroppedFromDeconstruction(List<GameObject> items)
	{
		if (items != null)
		{
			for (int i = 0; i < items.Count; i++)
			{
				Electrobank component = items[i].GetComponent<Electrobank>();
				if (component != null && component.HasTag(GameTags.ChargedPortableBattery) && !component.IsFullyCharged)
				{
					component.RemovePower(component.Charge, true);
				}
			}
		}
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x0010794E File Offset: 0x00105B4E
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
		base.OnCleanUp();
	}

	// Token: 0x06002DF5 RID: 11765 RVA: 0x00107961 File Offset: 0x00105B61
	private void OnStorageChange(object data = null)
	{
		this.RefreshCells(null);
		this.UpdateSymbolSwap();
	}

	// Token: 0x06002DF6 RID: 11766 RVA: 0x00107970 File Offset: 0x00105B70
	public void UpdateMeter()
	{
		if (this.meterController == null)
		{
			this.meterController = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		}
		this.meterController.SetPositionPercent(this.smi.master.ElectrobankJoulesStored / 120000f);
	}

	// Token: 0x06002DF7 RID: 11767 RVA: 0x001079CC File Offset: 0x00105BCC
	public void UpdateSymbolSwap()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		SymbolOverrideController component2 = component.GetComponent<SymbolOverrideController>();
		component.SetSymbolVisiblity("electrobank_l", false);
		if (this.storage.items.Count > 0)
		{
			KAnim.Build.Symbol symbol = this.storage.items[0].GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.symbols[0];
			component2.AddSymbolOverride("electrobank_s", symbol, 0);
			return;
		}
		component2.RemoveSymbolOverride("electrobank_s", 0);
	}

	// Token: 0x06002DF8 RID: 11768 RVA: 0x00107A60 File Offset: 0x00105C60
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		bool flag = false;
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, circuitID != ushort.MaxValue);
		if (!this.operational.IsOperational)
		{
			if (this.operational.IsActive)
			{
				this.operational.SetActive(false, false);
			}
			return;
		}
		float num = 0f;
		float num2 = Mathf.Min(this.wattageRating * dt, this.Capacity - this.JoulesAvailable);
		for (int i = this.storedCells.Count - 1; i >= 0; i--)
		{
			num += this.storedCells[i].RemovePower(num2 - num, true);
			if (num >= num2)
			{
				break;
			}
		}
		if (num > 0f)
		{
			flag = true;
			base.GenerateJoules(num, false);
		}
		this.operational.SetActive(flag, false);
	}

	// Token: 0x06002DF9 RID: 11769 RVA: 0x00107B3C File Offset: 0x00105D3C
	private void RefreshCells(object data = null)
	{
		this.storedCells.Clear();
		foreach (GameObject gameObject in this.storage.GetItems())
		{
			Electrobank component = gameObject.GetComponent<Electrobank>();
			if (component != null)
			{
				this.storedCells.Add(component);
			}
		}
	}

	// Token: 0x04001B15 RID: 6933
	public float wattageRating;

	// Token: 0x04001B16 RID: 6934
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04001B17 RID: 6935
	private ElectrobankDischarger.StatesInstance smi;

	// Token: 0x04001B18 RID: 6936
	private List<Electrobank> storedCells = new List<Electrobank>();

	// Token: 0x04001B19 RID: 6937
	private MeterController meterController;

	// Token: 0x04001B1A RID: 6938
	protected FilteredStorage filteredStorage;

	// Token: 0x020015C4 RID: 5572
	public class StatesInstance : GameStateMachine<ElectrobankDischarger.States, ElectrobankDischarger.StatesInstance, ElectrobankDischarger, object>.GameInstance
	{
		// Token: 0x060092A8 RID: 37544 RVA: 0x00367549 File Offset: 0x00365749
		public StatesInstance(ElectrobankDischarger master)
			: base(master)
		{
		}
	}

	// Token: 0x020015C5 RID: 5573
	public class States : GameStateMachine<ElectrobankDischarger.States, ElectrobankDischarger.StatesInstance, ElectrobankDischarger>
	{
		// Token: 0x060092A9 RID: 37545 RVA: 0x00367554 File Offset: 0x00365754
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.noBattery;
			this.root.EventTransition(GameHashes.ActiveChanged, this.discharging, (ElectrobankDischarger.StatesInstance smi) => smi.GetComponent<Operational>().IsActive);
			this.noBattery.PlayAnim("off").EnterTransition(this.inoperational, (ElectrobankDischarger.StatesInstance smi) => smi.master.storage.items.Count != 0).Enter(delegate(ElectrobankDischarger.StatesInstance smi)
			{
				smi.master.UpdateMeter();
			});
			this.inoperational.PlayAnim("on").Enter(delegate(ElectrobankDischarger.StatesInstance smi)
			{
				smi.master.UpdateMeter();
			}).EnterTransition(this.noBattery, (ElectrobankDischarger.StatesInstance smi) => smi.master.storage.items.Count == 0);
			this.discharging.Enter(delegate(ElectrobankDischarger.StatesInstance smi)
			{
				smi.master.UpdateMeter();
			}).EventTransition(GameHashes.ActiveChanged, this.inoperational, (ElectrobankDischarger.StatesInstance smi) => !smi.GetComponent<Operational>().IsActive).QueueAnim("working_pre", false, null)
				.QueueAnim("working_loop", true, null)
				.Update(delegate(ElectrobankDischarger.StatesInstance smi, float dt)
				{
					smi.master.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.ElectrobankJoulesAvailable, smi.master);
					smi.master.UpdateMeter();
				}, UpdateRate.SIM_200ms, false);
			this.discharging_pst.Enter(delegate(ElectrobankDischarger.StatesInstance smi)
			{
				smi.master.UpdateMeter();
			}).PlayAnim("working_pst");
		}

		// Token: 0x040070D0 RID: 28880
		public GameStateMachine<ElectrobankDischarger.States, ElectrobankDischarger.StatesInstance, ElectrobankDischarger, object>.State noBattery;

		// Token: 0x040070D1 RID: 28881
		public GameStateMachine<ElectrobankDischarger.States, ElectrobankDischarger.StatesInstance, ElectrobankDischarger, object>.State inoperational;

		// Token: 0x040070D2 RID: 28882
		public GameStateMachine<ElectrobankDischarger.States, ElectrobankDischarger.StatesInstance, ElectrobankDischarger, object>.State discharging;

		// Token: 0x040070D3 RID: 28883
		public GameStateMachine<ElectrobankDischarger.States, ElectrobankDischarger.StatesInstance, ElectrobankDischarger, object>.State discharging_pst;
	}
}
