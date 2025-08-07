using System;
using STRINGS;

// Token: 0x02000782 RID: 1922
public class MassiveHeatSink : StateMachineComponent<MassiveHeatSink.StatesInstance>
{
	// Token: 0x060032B8 RID: 12984 RVA: 0x0011D7B0 File Offset: 0x0011B9B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x04001E66 RID: 7782
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001E67 RID: 7783
	[MyCmpReq]
	private ElementConverter elementConverter;

	// Token: 0x0200166E RID: 5742
	public class States : GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink>
	{
		// Token: 0x06009510 RID: 38160 RVA: 0x003723AC File Offset: 0x003705AC
		private string AwaitingFuelResolveString(string str, object obj)
		{
			ElementConverter elementConverter = ((MassiveHeatSink.StatesInstance)obj).master.elementConverter;
			string text = elementConverter.consumedElements[0].Tag.ProperName();
			string formattedMass = GameUtil.GetFormattedMass(elementConverter.consumedElements[0].MassConsumptionRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
			str = string.Format(str, text, formattedMass);
			return str;
		}

		// Token: 0x06009511 RID: 38161 RVA: 0x0037240C File Offset: 0x0037060C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.idle, (MassiveHeatSink.StatesInstance smi) => smi.master.operational.IsOperational);
			GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.State state = this.idle.EventTransition(GameHashes.OperationalChanged, this.disabled, (MassiveHeatSink.StatesInstance smi) => !smi.master.operational.IsOperational);
			string text = BUILDING.STATUSITEMS.AWAITINGFUEL.NAME;
			string text2 = BUILDING.STATUSITEMS.AWAITINGFUEL.TOOLTIP;
			string text3 = "";
			StatusItem.IconType iconType = StatusItem.IconType.Exclamation;
			NotificationType notificationType = NotificationType.BadMinor;
			bool flag = false;
			Func<string, MassiveHeatSink.StatesInstance, string> func = new Func<string, MassiveHeatSink.StatesInstance, string>(this.AwaitingFuelResolveString);
			state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, func, null, null).EventTransition(GameHashes.OnStorageChange, this.active, (MassiveHeatSink.StatesInstance smi) => smi.master.elementConverter.HasEnoughMassToStartConverting(false));
			this.active.EventTransition(GameHashes.OperationalChanged, this.disabled, (MassiveHeatSink.StatesInstance smi) => !smi.master.operational.IsOperational).EventTransition(GameHashes.OnStorageChange, this.idle, (MassiveHeatSink.StatesInstance smi) => !smi.master.elementConverter.HasEnoughMassToStartConverting(false)).Enter(delegate(MassiveHeatSink.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			})
				.Exit(delegate(MassiveHeatSink.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				});
		}

		// Token: 0x040072A1 RID: 29345
		public GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.State disabled;

		// Token: 0x040072A2 RID: 29346
		public GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.State idle;

		// Token: 0x040072A3 RID: 29347
		public GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.State active;
	}

	// Token: 0x0200166F RID: 5743
	public class StatesInstance : GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.GameInstance
	{
		// Token: 0x06009513 RID: 38163 RVA: 0x003725B0 File Offset: 0x003707B0
		public StatesInstance(MassiveHeatSink master)
			: base(master)
		{
		}
	}
}
