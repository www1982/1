using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200087F RID: 2175
[SkipSaveFileSerialization]
public class ReceptacleMonitor : StateMachineComponent<ReceptacleMonitor.StatesInstance>, IGameObjectEffectDescriptor, IWiltCause, ISim1000ms
{
	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x0014BDC3 File Offset: 0x00149FC3
	public bool Replanted
	{
		get
		{
			return this.replanted;
		}
	}

	// Token: 0x06003BD6 RID: 15318 RVA: 0x0014BDCB File Offset: 0x00149FCB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003BD7 RID: 15319 RVA: 0x0014BDDE File Offset: 0x00149FDE
	public PlantablePlot GetReceptacle()
	{
		return (PlantablePlot)base.smi.sm.receptacle.Get(base.smi);
	}

	// Token: 0x06003BD8 RID: 15320 RVA: 0x0014BE00 File Offset: 0x0014A000
	public void SetReceptacle(PlantablePlot plot = null)
	{
		if (plot == null)
		{
			base.smi.sm.receptacle.Set(null, base.smi, false);
			this.replanted = false;
		}
		else
		{
			base.smi.sm.receptacle.Set(plot, base.smi, false);
			this.replanted = true;
		}
		base.Trigger(-1636776682, null);
	}

	// Token: 0x06003BD9 RID: 15321 RVA: 0x0014BE70 File Offset: 0x0014A070
	public void Sim1000ms(float dt)
	{
		if (base.smi.sm.receptacle.Get(base.smi) == null)
		{
			base.smi.GoTo(base.smi.sm.wild);
			return;
		}
		Operational component = base.smi.sm.receptacle.Get(base.smi).GetComponent<Operational>();
		if (component == null)
		{
			base.smi.GoTo(base.smi.sm.operational);
			return;
		}
		if (component.IsOperational)
		{
			base.smi.GoTo(base.smi.sm.operational);
			return;
		}
		base.smi.GoTo(base.smi.sm.inoperational);
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x06003BDA RID: 15322 RVA: 0x0014BF41 File Offset: 0x0014A141
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.Receptacle };
		}
	}

	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x06003BDB RID: 15323 RVA: 0x0014BF50 File Offset: 0x0014A150
	public string WiltStateString
	{
		get
		{
			string text = "";
			if (base.smi.IsInsideState(base.smi.sm.inoperational))
			{
				text += CREATURES.STATUSITEMS.RECEPTACLEINOPERATIONAL.NAME;
			}
			return text;
		}
	}

	// Token: 0x06003BDC RID: 15324 RVA: 0x0014BF92 File Offset: 0x0014A192
	public bool HasReceptacle()
	{
		return !base.smi.IsInsideState(base.smi.sm.wild);
	}

	// Token: 0x06003BDD RID: 15325 RVA: 0x0014BFB2 File Offset: 0x0014A1B2
	public bool HasOperationalReceptacle()
	{
		return base.smi.IsInsideState(base.smi.sm.operational);
	}

	// Token: 0x06003BDE RID: 15326 RVA: 0x0014BFCF File Offset: 0x0014A1CF
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_RECEPTACLE, UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_RECEPTACLE, Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x040024B8 RID: 9400
	private bool replanted;

	// Token: 0x02001838 RID: 6200
	public class StatesInstance : GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.GameInstance
	{
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06009BEB RID: 39915 RVA: 0x0038FA26 File Offset: 0x0038DC26
		public SingleEntityReceptacle ReceptacleObject
		{
			get
			{
				return base.sm.receptacle.Get(this);
			}
		}

		// Token: 0x06009BEC RID: 39916 RVA: 0x0038FA39 File Offset: 0x0038DC39
		public StatesInstance(ReceptacleMonitor master)
			: base(master)
		{
		}
	}

	// Token: 0x02001839 RID: 6201
	public class States : GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor>
	{
		// Token: 0x06009BED RID: 39917 RVA: 0x0038FA44 File Offset: 0x0038DC44
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.wild;
			base.serializable = StateMachine.SerializeType.Never;
			this.wild.TriggerOnEnter(GameHashes.ReceptacleOperational, null);
			this.inoperational.TriggerOnEnter(GameHashes.ReceptacleInoperational, null);
			this.operational.TriggerOnEnter(GameHashes.ReceptacleOperational, null);
		}

		// Token: 0x0400784D RID: 30797
		public StateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.ObjectParameter<SingleEntityReceptacle> receptacle;

		// Token: 0x0400784E RID: 30798
		public GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.State wild;

		// Token: 0x0400784F RID: 30799
		public GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.State inoperational;

		// Token: 0x04007850 RID: 30800
		public GameStateMachine<ReceptacleMonitor.States, ReceptacleMonitor.StatesInstance, ReceptacleMonitor, object>.State operational;
	}
}
