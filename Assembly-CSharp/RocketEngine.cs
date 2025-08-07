using System;
using KSerialization;
using STRINGS;

// Token: 0x02000B63 RID: 2915
[SerializationConfig(MemberSerialization.OptIn)]
public class RocketEngine : StateMachineComponent<RocketEngine.StatesInstance>
{
	// Token: 0x060056DA RID: 22234 RVA: 0x001F7658 File Offset: 0x001F5858
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		if (this.mainEngine)
		{
			base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketPrep, new RequireAttachedComponent(base.gameObject.GetComponent<AttachableBuilding>(), typeof(FuelTank), UI.STARMAP.COMPONENT.FUEL_TANK));
		}
	}

	// Token: 0x04003A0E RID: 14862
	public float exhaustEmitRate = 50f;

	// Token: 0x04003A0F RID: 14863
	public float exhaustTemperature = 1500f;

	// Token: 0x04003A10 RID: 14864
	public SpawnFXHashes explosionEffectHash;

	// Token: 0x04003A11 RID: 14865
	public SimHashes exhaustElement = SimHashes.CarbonDioxide;

	// Token: 0x04003A12 RID: 14866
	public Tag fuelTag;

	// Token: 0x04003A13 RID: 14867
	public float efficiency = 1f;

	// Token: 0x04003A14 RID: 14868
	public bool requireOxidizer = true;

	// Token: 0x04003A15 RID: 14869
	public bool mainEngine = true;

	// Token: 0x02001C8D RID: 7309
	public class StatesInstance : GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine, object>.GameInstance
	{
		// Token: 0x0600AB99 RID: 43929 RVA: 0x003BFC2E File Offset: 0x003BDE2E
		public StatesInstance(RocketEngine smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001C8E RID: 7310
	public class States : GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine>
	{
		// Token: 0x0600AB9A RID: 43930 RVA: 0x003BFC38 File Offset: 0x003BDE38
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.PlayAnim("grounded", KAnim.PlayMode.Loop).EventTransition(GameHashes.IgniteEngine, this.burning, null);
			this.burning.EventTransition(GameHashes.RocketLanded, this.burnComplete, null).PlayAnim("launch_pre").QueueAnim("launch_loop", true, null)
				.Update(delegate(RocketEngine.StatesInstance smi, float dt)
				{
					int num = Grid.PosToCell(smi.master.gameObject.transform.GetPosition() + smi.master.GetComponent<KBatchedAnimController>().Offset);
					if (Grid.IsValidCell(num))
					{
						SimMessages.EmitMass(num, ElementLoader.GetElementIndex(smi.master.exhaustElement), dt * smi.master.exhaustEmitRate, smi.master.exhaustTemperature, 0, 0, -1);
					}
					int num2 = 10;
					for (int i = 1; i < num2; i++)
					{
						int num3 = Grid.OffsetCell(num, -1, -i);
						int num4 = Grid.OffsetCell(num, 0, -i);
						int num5 = Grid.OffsetCell(num, 1, -i);
						if (Grid.IsValidCell(num3))
						{
							SimMessages.ModifyEnergy(num3, smi.master.exhaustTemperature / (float)(i + 1), 3200f, SimMessages.EnergySourceID.Burner);
						}
						if (Grid.IsValidCell(num4))
						{
							SimMessages.ModifyEnergy(num4, smi.master.exhaustTemperature / (float)i, 3200f, SimMessages.EnergySourceID.Burner);
						}
						if (Grid.IsValidCell(num5))
						{
							SimMessages.ModifyEnergy(num5, smi.master.exhaustTemperature / (float)(i + 1), 3200f, SimMessages.EnergySourceID.Burner);
						}
					}
				}, UpdateRate.SIM_200ms, false);
			this.burnComplete.PlayAnim("grounded", KAnim.PlayMode.Loop).EventTransition(GameHashes.IgniteEngine, this.burning, null);
		}

		// Token: 0x040086AC RID: 34476
		public GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine, object>.State idle;

		// Token: 0x040086AD RID: 34477
		public GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine, object>.State burning;

		// Token: 0x040086AE RID: 34478
		public GameStateMachine<RocketEngine.States, RocketEngine.StatesInstance, RocketEngine, object>.State burnComplete;
	}
}
