using System;
using STRINGS;

// Token: 0x0200070E RID: 1806
public class DetectorNetwork : GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>
{
	// Token: 0x06002D4F RID: 11599 RVA: 0x00103DF0 File Offset: 0x00101FF0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (DetectorNetwork.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.operational.InitializeStates(this).EventTransition(GameHashes.OperationalChanged, this.inoperational, (DetectorNetwork.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
	}

	// Token: 0x04001AAE RID: 6830
	public StateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.FloatParameter networkQuality;

	// Token: 0x04001AAF RID: 6831
	public GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State inoperational;

	// Token: 0x04001AB0 RID: 6832
	public DetectorNetwork.NetworkStates operational;

	// Token: 0x020015A4 RID: 5540
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015A5 RID: 5541
	public class NetworkStates : GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State
	{
		// Token: 0x06009244 RID: 37444 RVA: 0x003664C8 File Offset: 0x003646C8
		public DetectorNetwork.NetworkStates InitializeStates(DetectorNetwork parent)
		{
			base.DefaultState(this.poor);
			GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State state = this.poor;
			string text = BUILDING.STATUSITEMS.NETWORKQUALITY.NAME;
			string text2 = BUILDING.STATUSITEMS.NETWORKQUALITY.TOOLTIP;
			string text3 = "";
			StatusItem.IconType iconType = StatusItem.IconType.Exclamation;
			NotificationType notificationType = NotificationType.BadMinor;
			bool flag = false;
			Func<string, DetectorNetwork.Instance, string> func = new Func<string, DetectorNetwork.Instance, string>(this.StringCallback);
			state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, func, null, null).ParamTransition<float>(parent.networkQuality, this.good, (DetectorNetwork.Instance smi, float p) => (double)p >= 0.8);
			GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State state2 = this.good;
			string text4 = BUILDING.STATUSITEMS.NETWORKQUALITY.NAME;
			string text5 = BUILDING.STATUSITEMS.NETWORKQUALITY.TOOLTIP;
			string text6 = "";
			StatusItem.IconType iconType2 = StatusItem.IconType.Info;
			NotificationType notificationType2 = NotificationType.Neutral;
			bool flag2 = false;
			func = new Func<string, DetectorNetwork.Instance, string>(this.StringCallback);
			state2.ToggleStatusItem(text4, text5, text6, iconType2, notificationType2, flag2, default(HashedString), 129022, func, null, null).ParamTransition<float>(parent.networkQuality, this.poor, (DetectorNetwork.Instance smi, float p) => (double)p < 0.8);
			return this;
		}

		// Token: 0x06009245 RID: 37445 RVA: 0x003665D0 File Offset: 0x003647D0
		private string StringCallback(string str, DetectorNetwork.Instance smi)
		{
			MathUtil.MinMax detectTimeRangeForWorld = Game.Instance.spaceScannerNetworkManager.GetDetectTimeRangeForWorld(smi.GetMyWorldId());
			float num = Game.Instance.spaceScannerNetworkManager.GetQualityForWorld(smi.GetMyWorldId());
			num = num.Remap(new ValueTuple<float, float>(0f, 1f), new ValueTuple<float, float>(0f, 0.5f));
			return str.Replace("{TotalQuality}", GameUtil.GetFormattedPercent(smi.GetNetworkQuality01() * 100f, GameUtil.TimeSlice.None)).Replace("{WorstTime}", GameUtil.GetFormattedTime(detectTimeRangeForWorld.min, "F0")).Replace("{BestTime}", GameUtil.GetFormattedTime(detectTimeRangeForWorld.max, "F0"))
				.Replace("{Coverage}", GameUtil.GetFormattedPercent(num * 100f, GameUtil.TimeSlice.None));
		}

		// Token: 0x04007070 RID: 28784
		public GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State poor;

		// Token: 0x04007071 RID: 28785
		public GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State good;
	}

	// Token: 0x020015A6 RID: 5542
	public new class Instance : GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.GameInstance
	{
		// Token: 0x06009247 RID: 37447 RVA: 0x003666A0 File Offset: 0x003648A0
		public Instance(IStateMachineTarget master, DetectorNetwork.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009248 RID: 37448 RVA: 0x003666AA File Offset: 0x003648AA
		public override void StartSM()
		{
			this.worldId = base.master.gameObject.GetMyWorldId();
			Components.DetectorNetworks.Add(this.worldId, this);
			base.StartSM();
		}

		// Token: 0x06009249 RID: 37449 RVA: 0x003666D9 File Offset: 0x003648D9
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			Components.DetectorNetworks.Remove(this.worldId, this);
		}

		// Token: 0x0600924A RID: 37450 RVA: 0x003666F3 File Offset: 0x003648F3
		public void Internal_SetNetworkQuality(float quality01)
		{
			base.sm.networkQuality.Set(quality01, base.smi, false);
		}

		// Token: 0x0600924B RID: 37451 RVA: 0x0036670E File Offset: 0x0036490E
		public float GetNetworkQuality01()
		{
			return base.sm.networkQuality.Get(base.smi);
		}

		// Token: 0x04007072 RID: 28786
		[NonSerialized]
		private int worldId;
	}
}
