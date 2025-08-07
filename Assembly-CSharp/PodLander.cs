using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000B5B RID: 2907
[SerializationConfig(MemberSerialization.OptIn)]
public class PodLander : StateMachineComponent<PodLander.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x060056A7 RID: 22183 RVA: 0x001F6500 File Offset: 0x001F4700
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x060056A8 RID: 22184 RVA: 0x001F6514 File Offset: 0x001F4714
	public void ReleaseAstronaut()
	{
		if (this.releasingAstronaut)
		{
			return;
		}
		this.releasingAstronaut = true;
		MinionStorage component = base.GetComponent<MinionStorage>();
		List<MinionStorage.Info> storedMinionInfo = component.GetStoredMinionInfo();
		for (int i = storedMinionInfo.Count - 1; i >= 0; i--)
		{
			MinionStorage.Info info = storedMinionInfo[i];
			component.DeserializeMinion(info.id, Grid.CellToPos(Grid.PosToCell(base.smi.master.transform.GetPosition())));
		}
		this.releasingAstronaut = false;
	}

	// Token: 0x060056A9 RID: 22185 RVA: 0x001F658D File Offset: 0x001F478D
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return null;
	}

	// Token: 0x040039E6 RID: 14822
	[Serialize]
	private int landOffLocation;

	// Token: 0x040039E7 RID: 14823
	[Serialize]
	private float flightAnimOffset;

	// Token: 0x040039E8 RID: 14824
	private float rocketSpeed;

	// Token: 0x040039E9 RID: 14825
	public float exhaustEmitRate = 2f;

	// Token: 0x040039EA RID: 14826
	public float exhaustTemperature = 1000f;

	// Token: 0x040039EB RID: 14827
	public SimHashes exhaustElement = SimHashes.CarbonDioxide;

	// Token: 0x040039EC RID: 14828
	private GameObject soundSpeakerObject;

	// Token: 0x040039ED RID: 14829
	private bool releasingAstronaut;

	// Token: 0x02001C86 RID: 7302
	public class StatesInstance : GameStateMachine<PodLander.States, PodLander.StatesInstance, PodLander, object>.GameInstance
	{
		// Token: 0x0600AB7B RID: 43899 RVA: 0x003BF225 File Offset: 0x003BD425
		public StatesInstance(PodLander master)
			: base(master)
		{
		}
	}

	// Token: 0x02001C87 RID: 7303
	public class States : GameStateMachine<PodLander.States, PodLander.StatesInstance, PodLander>
	{
		// Token: 0x0600AB7C RID: 43900 RVA: 0x003BF230 File Offset: 0x003BD430
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.landing;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.landing.PlayAnim("launch_loop", KAnim.PlayMode.Loop).Enter(delegate(PodLander.StatesInstance smi)
			{
				smi.master.flightAnimOffset = 50f;
			}).Update(delegate(PodLander.StatesInstance smi, float dt)
			{
				float num = 10f;
				smi.master.rocketSpeed = num - Mathf.Clamp(Mathf.Pow(smi.timeinstate / 3.5f, 4f), 0f, num - 2f);
				smi.master.flightAnimOffset -= dt * smi.master.rocketSpeed;
				KBatchedAnimController component = smi.master.GetComponent<KBatchedAnimController>();
				component.Offset = Vector3.up * smi.master.flightAnimOffset;
				Vector3 positionIncludingOffset = component.PositionIncludingOffset;
				int num2 = Grid.PosToCell(smi.master.gameObject.transform.GetPosition() + smi.master.GetComponent<KBatchedAnimController>().Offset);
				if (Grid.IsValidCell(num2))
				{
					SimMessages.EmitMass(num2, ElementLoader.GetElementIndex(smi.master.exhaustElement), dt * smi.master.exhaustEmitRate, smi.master.exhaustTemperature, 0, 0, -1);
				}
				if (component.Offset.y <= 0f)
				{
					smi.GoTo(this.crashed);
				}
			}, UpdateRate.SIM_33ms, false);
			this.crashed.PlayAnim("grounded").Enter(delegate(PodLander.StatesInstance smi)
			{
				smi.master.GetComponent<KBatchedAnimController>().Offset = Vector3.zero;
				smi.master.rocketSpeed = 0f;
				smi.master.ReleaseAstronaut();
			});
		}

		// Token: 0x04008698 RID: 34456
		public GameStateMachine<PodLander.States, PodLander.StatesInstance, PodLander, object>.State landing;

		// Token: 0x04008699 RID: 34457
		public GameStateMachine<PodLander.States, PodLander.StatesInstance, PodLander, object>.State crashed;
	}
}
