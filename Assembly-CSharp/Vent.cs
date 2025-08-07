using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000BD5 RID: 3029
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Vent")]
public class Vent : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x17000691 RID: 1681
	// (get) Token: 0x06005AC7 RID: 23239 RVA: 0x0020CCF7 File Offset: 0x0020AEF7
	// (set) Token: 0x06005AC8 RID: 23240 RVA: 0x0020CCFF File Offset: 0x0020AEFF
	public int SortKey
	{
		get
		{
			return this.sortKey;
		}
		set
		{
			this.sortKey = value;
		}
	}

	// Token: 0x06005AC9 RID: 23241 RVA: 0x0020CD08 File Offset: 0x0020AF08
	public void UpdateVentedMass(SimHashes element, float mass)
	{
		if (!this.lifeTimeVentMass.ContainsKey(element))
		{
			this.lifeTimeVentMass.Add(element, mass);
			return;
		}
		Dictionary<SimHashes, float> dictionary = this.lifeTimeVentMass;
		dictionary[element] += mass;
	}

	// Token: 0x06005ACA RID: 23242 RVA: 0x0020CD4A File Offset: 0x0020AF4A
	public float GetVentedMass(SimHashes element)
	{
		if (this.lifeTimeVentMass.ContainsKey(element))
		{
			return this.lifeTimeVentMass[element];
		}
		return 0f;
	}

	// Token: 0x06005ACB RID: 23243 RVA: 0x0020CD6C File Offset: 0x0020AF6C
	public bool Closed()
	{
		bool flag = false;
		return (this.operational.Flags.TryGetValue(LogicOperationalController.LogicOperationalFlag, out flag) && !flag) || (this.operational.Flags.TryGetValue(BuildingEnabledButton.EnabledFlag, out flag) && !flag);
	}

	// Token: 0x06005ACC RID: 23244 RVA: 0x0020CDB8 File Offset: 0x0020AFB8
	protected override void OnSpawn()
	{
		Building component = base.GetComponent<Building>();
		this.cell = component.GetUtilityOutputCell();
		this.smi = new Vent.StatesInstance(this);
		this.smi.StartSM();
	}

	// Token: 0x06005ACD RID: 23245 RVA: 0x0020CDF0 File Offset: 0x0020AFF0
	public Vent.State GetEndPointState()
	{
		Vent.State state = Vent.State.Invalid;
		Endpoint endpoint = this.endpointType;
		if (endpoint != Endpoint.Source)
		{
			if (endpoint == Endpoint.Sink)
			{
				state = Vent.State.Ready;
				int num = this.cell;
				if (!this.IsValidOutputCell(num))
				{
					state = (Grid.Solid[num] ? Vent.State.Blocked : Vent.State.OverPressure);
				}
			}
		}
		else
		{
			state = (this.IsConnected() ? Vent.State.Ready : Vent.State.Blocked);
		}
		return state;
	}

	// Token: 0x06005ACE RID: 23246 RVA: 0x0020CE44 File Offset: 0x0020B044
	public bool IsConnected()
	{
		UtilityNetwork networkForCell = Conduit.GetNetworkManager(this.conduitType).GetNetworkForCell(this.cell);
		return networkForCell != null && (networkForCell as FlowUtilityNetwork).HasSinks;
	}

	// Token: 0x17000692 RID: 1682
	// (get) Token: 0x06005ACF RID: 23247 RVA: 0x0020CE78 File Offset: 0x0020B078
	public bool IsBlocked
	{
		get
		{
			return this.GetEndPointState() != Vent.State.Ready;
		}
	}

	// Token: 0x06005AD0 RID: 23248 RVA: 0x0020CE88 File Offset: 0x0020B088
	private bool IsValidOutputCell(int output_cell)
	{
		bool flag = false;
		if ((this.structure == null || !this.structure.IsEntombed() || !this.Closed()) && !Grid.Solid[output_cell])
		{
			flag = Grid.Mass[output_cell] < this.overpressureMass;
		}
		return flag;
	}

	// Token: 0x06005AD1 RID: 23249 RVA: 0x0020CEDC File Offset: 0x0020B0DC
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		string formattedMass = GameUtil.GetFormattedMass(this.overpressureMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.BUILDINGEFFECTS.OVER_PRESSURE_MASS, formattedMass), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.OVER_PRESSURE_MASS, formattedMass), Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x04003C29 RID: 15401
	private int cell = -1;

	// Token: 0x04003C2A RID: 15402
	private int sortKey;

	// Token: 0x04003C2B RID: 15403
	[Serialize]
	public Dictionary<SimHashes, float> lifeTimeVentMass = new Dictionary<SimHashes, float>();

	// Token: 0x04003C2C RID: 15404
	private Vent.StatesInstance smi;

	// Token: 0x04003C2D RID: 15405
	[SerializeField]
	public ConduitType conduitType = ConduitType.Gas;

	// Token: 0x04003C2E RID: 15406
	[SerializeField]
	public Endpoint endpointType;

	// Token: 0x04003C2F RID: 15407
	[SerializeField]
	public float overpressureMass = 1f;

	// Token: 0x04003C30 RID: 15408
	[NonSerialized]
	public bool showConnectivityIcons = true;

	// Token: 0x04003C31 RID: 15409
	[MyCmpGet]
	[NonSerialized]
	public Structure structure;

	// Token: 0x04003C32 RID: 15410
	[MyCmpGet]
	[NonSerialized]
	public Operational operational;

	// Token: 0x02001D01 RID: 7425
	public enum State
	{
		// Token: 0x040087E7 RID: 34791
		Invalid,
		// Token: 0x040087E8 RID: 34792
		Ready,
		// Token: 0x040087E9 RID: 34793
		Blocked,
		// Token: 0x040087EA RID: 34794
		OverPressure,
		// Token: 0x040087EB RID: 34795
		Closed
	}

	// Token: 0x02001D02 RID: 7426
	public class StatesInstance : GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.GameInstance
	{
		// Token: 0x0600ACCA RID: 44234 RVA: 0x003C29A5 File Offset: 0x003C0BA5
		public StatesInstance(Vent master)
			: base(master)
		{
			this.exhaust = master.GetComponent<Exhaust>();
		}

		// Token: 0x0600ACCB RID: 44235 RVA: 0x003C29BA File Offset: 0x003C0BBA
		public bool NeedsExhaust()
		{
			return this.exhaust != null && base.master.GetEndPointState() != Vent.State.Ready && base.master.endpointType == Endpoint.Source;
		}

		// Token: 0x0600ACCC RID: 44236 RVA: 0x003C29E8 File Offset: 0x003C0BE8
		public bool Blocked()
		{
			return base.master.GetEndPointState() == Vent.State.Blocked && base.master.endpointType > Endpoint.Source;
		}

		// Token: 0x0600ACCD RID: 44237 RVA: 0x003C2A08 File Offset: 0x003C0C08
		public bool OverPressure()
		{
			return this.exhaust != null && base.master.GetEndPointState() == Vent.State.OverPressure && base.master.endpointType > Endpoint.Source;
		}

		// Token: 0x0600ACCE RID: 44238 RVA: 0x003C2A38 File Offset: 0x003C0C38
		public void CheckTransitions()
		{
			if (this.NeedsExhaust())
			{
				base.smi.GoTo(base.sm.needExhaust);
				return;
			}
			if (base.master.Closed())
			{
				base.smi.GoTo(base.sm.closed);
				return;
			}
			if (this.Blocked())
			{
				base.smi.GoTo(base.sm.open.blocked);
				return;
			}
			if (this.OverPressure())
			{
				base.smi.GoTo(base.sm.open.overPressure);
				return;
			}
			base.smi.GoTo(base.sm.open.idle);
		}

		// Token: 0x0600ACCF RID: 44239 RVA: 0x003C2AEB File Offset: 0x003C0CEB
		public StatusItem SelectStatusItem(StatusItem gas_status_item, StatusItem liquid_status_item)
		{
			if (base.master.conduitType != ConduitType.Gas)
			{
				return liquid_status_item;
			}
			return gas_status_item;
		}

		// Token: 0x040087EC RID: 34796
		private Exhaust exhaust;
	}

	// Token: 0x02001D03 RID: 7427
	public class States : GameStateMachine<Vent.States, Vent.StatesInstance, Vent>
	{
		// Token: 0x0600ACD0 RID: 44240 RVA: 0x003C2B00 File Offset: 0x003C0D00
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.open.idle;
			this.root.Update("CheckTransitions", delegate(Vent.StatesInstance smi, float dt)
			{
				smi.CheckTransitions();
			}, UpdateRate.SIM_200ms, false);
			this.open.TriggerOnEnter(GameHashes.VentOpen, null);
			this.closed.TriggerOnEnter(GameHashes.VentClosed, null);
			this.open.blocked.ToggleStatusItem((Vent.StatesInstance smi) => smi.SelectStatusItem(Db.Get().BuildingStatusItems.GasVentObstructed, Db.Get().BuildingStatusItems.LiquidVentObstructed), null);
			this.open.overPressure.ToggleStatusItem((Vent.StatesInstance smi) => smi.SelectStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure, Db.Get().BuildingStatusItems.LiquidVentOverPressure), null);
		}

		// Token: 0x040087ED RID: 34797
		public Vent.States.OpenState open;

		// Token: 0x040087EE RID: 34798
		public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State closed;

		// Token: 0x040087EF RID: 34799
		public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State needExhaust;

		// Token: 0x020028D1 RID: 10449
		public class OpenState : GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State
		{
			// Token: 0x0400B4E7 RID: 46311
			public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State idle;

			// Token: 0x0400B4E8 RID: 46312
			public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State blocked;

			// Token: 0x0400B4E9 RID: 46313
			public GameStateMachine<Vent.States, Vent.StatesInstance, Vent, object>.State overPressure;
		}
	}
}
