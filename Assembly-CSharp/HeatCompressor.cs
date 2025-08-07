using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000747 RID: 1863
public class HeatCompressor : StateMachineComponent<HeatCompressor.StatesInstance>
{
	// Token: 0x06002F3A RID: 12090 RVA: 0x0010ED10 File Offset: 0x0010CF10
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.meter.gameObject.GetComponent<KBatchedAnimController>().SetDirty();
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("HeatCube"), base.transform.GetPosition());
		gameObject.SetActive(true);
		this.heatCubeStorage.Store(gameObject, true, false, true, false);
		base.smi.StartSM();
	}

	// Token: 0x06002F3B RID: 12091 RVA: 0x0010EDBF File Offset: 0x0010CFBF
	public void SetStorage(Storage inputStorage, Storage outputStorage, Storage heatCubeStorage)
	{
		this.inputStorage = inputStorage;
		this.outputStorage = outputStorage;
		this.heatCubeStorage = heatCubeStorage;
	}

	// Token: 0x06002F3C RID: 12092 RVA: 0x0010EDD8 File Offset: 0x0010CFD8
	public void CompressHeat(HeatCompressor.StatesInstance smi, float dt)
	{
		smi.heatRemovalTimer -= dt;
		float num = this.heatRemovalRate * dt / (float)this.inputStorage.items.Count;
		foreach (GameObject gameObject in this.inputStorage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			float lowTemp = component.Element.lowTemp;
			GameUtil.DeltaThermalEnergy(component, -num, lowTemp);
			this.energyCompressed += num;
		}
		if (smi.heatRemovalTimer <= 0f)
		{
			for (int i = this.inputStorage.items.Count; i > 0; i--)
			{
				GameObject gameObject2 = this.inputStorage.items[i - 1];
				if (gameObject2)
				{
					this.inputStorage.Transfer(gameObject2, this.outputStorage, false, true);
				}
			}
			smi.StartNewHeatRemoval();
		}
		foreach (GameObject gameObject3 in this.heatCubeStorage.items)
		{
			GameUtil.DeltaThermalEnergy(gameObject3.GetComponent<PrimaryElement>(), this.energyCompressed / (float)this.heatCubeStorage.items.Count, 100000f);
		}
		this.energyCompressed = 0f;
	}

	// Token: 0x06002F3D RID: 12093 RVA: 0x0010EF4C File Offset: 0x0010D14C
	public void EjectHeatCube()
	{
		this.heatCubeStorage.DropAll(base.transform.GetPosition(), false, false, default(Vector3), true, null);
	}

	// Token: 0x04001BF7 RID: 7159
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001BF8 RID: 7160
	private MeterController meter;

	// Token: 0x04001BF9 RID: 7161
	public Storage inputStorage;

	// Token: 0x04001BFA RID: 7162
	public Storage outputStorage;

	// Token: 0x04001BFB RID: 7163
	public Storage heatCubeStorage;

	// Token: 0x04001BFC RID: 7164
	public float heatRemovalRate = 100f;

	// Token: 0x04001BFD RID: 7165
	public float heatRemovalTime = 100f;

	// Token: 0x04001BFE RID: 7166
	[Serialize]
	public float energyCompressed;

	// Token: 0x04001BFF RID: 7167
	public float heat_sink_active_time = 9000f;

	// Token: 0x04001C00 RID: 7168
	[Serialize]
	public float time_active;

	// Token: 0x04001C01 RID: 7169
	public float MAX_CUBE_TEMPERATURE = 3000f;

	// Token: 0x02001615 RID: 5653
	public class StatesInstance : GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor, object>.GameInstance
	{
		// Token: 0x060093DC RID: 37852 RVA: 0x0036D625 File Offset: 0x0036B825
		public StatesInstance(HeatCompressor master)
			: base(master)
		{
		}

		// Token: 0x060093DD RID: 37853 RVA: 0x0036D630 File Offset: 0x0036B830
		public void UpdateMeter()
		{
			float remainingCharge = this.GetRemainingCharge();
			base.master.meter.SetPositionPercent(remainingCharge);
		}

		// Token: 0x060093DE RID: 37854 RVA: 0x0036D658 File Offset: 0x0036B858
		public float GetRemainingCharge()
		{
			PrimaryElement primaryElement = base.smi.master.heatCubeStorage.FindFirstWithMass(GameTags.IndustrialIngredient, 0f);
			float num = 1f;
			if (primaryElement != null)
			{
				num = Mathf.Clamp01(primaryElement.GetComponent<PrimaryElement>().Temperature / base.smi.master.MAX_CUBE_TEMPERATURE);
			}
			return num;
		}

		// Token: 0x060093DF RID: 37855 RVA: 0x0036D6B7 File Offset: 0x0036B8B7
		public bool CanWork()
		{
			return this.GetRemainingCharge() < 1f && base.smi.master.heatCubeStorage.items.Count > 0;
		}

		// Token: 0x060093E0 RID: 37856 RVA: 0x0036D6E5 File Offset: 0x0036B8E5
		public void StartNewHeatRemoval()
		{
			this.heatRemovalTimer = base.smi.master.heatRemovalTime;
		}

		// Token: 0x040071D9 RID: 29145
		[Serialize]
		public float heatRemovalTimer;
	}

	// Token: 0x02001616 RID: 5654
	public class States : GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor>
	{
		// Token: 0x060093E1 RID: 37857 RVA: 0x0036D700 File Offset: 0x0036B900
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inactive;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.root.EventTransition(GameHashes.OperationalChanged, this.inactive, (HeatCompressor.StatesInstance smi) => !smi.GetComponent<Operational>().IsOperational);
			this.inactive.Enter(delegate(HeatCompressor.StatesInstance smi)
			{
				smi.UpdateMeter();
			}).PlayAnim("idle").Transition(this.dropCube, (HeatCompressor.StatesInstance smi) => smi.GetRemainingCharge() >= 1f, UpdateRate.SIM_200ms)
				.Transition(this.active, (HeatCompressor.StatesInstance smi) => smi.GetComponent<Operational>().IsOperational && smi.CanWork(), UpdateRate.SIM_200ms);
			this.active.Enter(delegate(HeatCompressor.StatesInstance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
				smi.StartNewHeatRemoval();
			}).PlayAnim("working_loop", KAnim.PlayMode.Loop).Update(delegate(HeatCompressor.StatesInstance smi, float dt)
			{
				smi.master.time_active += dt;
				smi.UpdateMeter();
				smi.master.CompressHeat(smi, dt);
			}, UpdateRate.SIM_200ms, false)
				.Transition(this.dropCube, (HeatCompressor.StatesInstance smi) => smi.GetRemainingCharge() >= 1f, UpdateRate.SIM_200ms)
				.Transition(this.inactive, (HeatCompressor.StatesInstance smi) => !smi.CanWork(), UpdateRate.SIM_200ms)
				.Exit(delegate(HeatCompressor.StatesInstance smi)
				{
					smi.GetComponent<Operational>().SetActive(false, false);
				});
			this.dropCube.Enter(delegate(HeatCompressor.StatesInstance smi)
			{
				smi.master.EjectHeatCube();
				smi.GoTo(this.inactive);
			});
		}

		// Token: 0x040071DA RID: 29146
		public GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor, object>.State active;

		// Token: 0x040071DB RID: 29147
		public GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor, object>.State inactive;

		// Token: 0x040071DC RID: 29148
		public GameStateMachine<HeatCompressor.States, HeatCompressor.StatesInstance, HeatCompressor, object>.State dropCube;
	}
}
