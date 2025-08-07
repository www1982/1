using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A5A RID: 2650
public class PlantElementEmitter : StateMachineComponent<PlantElementEmitter.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06004CC9 RID: 19657 RVA: 0x001BD933 File Offset: 0x001BBB33
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004CCA RID: 19658 RVA: 0x001BD946 File Offset: 0x001BBB46
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>();
	}

	// Token: 0x040032F1 RID: 13041
	[MyCmpGet]
	private WiltCondition wiltCondition;

	// Token: 0x040032F2 RID: 13042
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040032F3 RID: 13043
	public SimHashes emittedElement;

	// Token: 0x040032F4 RID: 13044
	public float emitRate;

	// Token: 0x02001B14 RID: 6932
	public class StatesInstance : GameStateMachine<PlantElementEmitter.States, PlantElementEmitter.StatesInstance, PlantElementEmitter, object>.GameInstance
	{
		// Token: 0x0600A606 RID: 42502 RVA: 0x003AAB7B File Offset: 0x003A8D7B
		public StatesInstance(PlantElementEmitter master)
			: base(master)
		{
		}

		// Token: 0x0600A607 RID: 42503 RVA: 0x003AAB84 File Offset: 0x003A8D84
		public bool IsWilting()
		{
			return !(base.master.wiltCondition == null) && base.master.wiltCondition != null && base.master.wiltCondition.IsWilting();
		}
	}

	// Token: 0x02001B15 RID: 6933
	public class States : GameStateMachine<PlantElementEmitter.States, PlantElementEmitter.StatesInstance, PlantElementEmitter>
	{
		// Token: 0x0600A608 RID: 42504 RVA: 0x003AABC0 File Offset: 0x003A8DC0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.healthy;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.healthy.EventTransition(GameHashes.Wilt, this.wilted, (PlantElementEmitter.StatesInstance smi) => smi.IsWilting()).Update("PlantEmit", delegate(PlantElementEmitter.StatesInstance smi, float dt)
			{
				SimMessages.EmitMass(Grid.PosToCell(smi.master.gameObject), ElementLoader.FindElementByHash(smi.master.emittedElement).idx, smi.master.emitRate * dt, ElementLoader.FindElementByHash(smi.master.emittedElement).defaultValues.temperature, byte.MaxValue, 0, -1);
			}, UpdateRate.SIM_4000ms, false);
			this.wilted.EventTransition(GameHashes.WiltRecover, this.healthy, null);
		}

		// Token: 0x0400819D RID: 33181
		public GameStateMachine<PlantElementEmitter.States, PlantElementEmitter.StatesInstance, PlantElementEmitter, object>.State wilted;

		// Token: 0x0400819E RID: 33182
		public GameStateMachine<PlantElementEmitter.States, PlantElementEmitter.StatesInstance, PlantElementEmitter, object>.State healthy;
	}
}
