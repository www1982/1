using System;

// Token: 0x02000A1A RID: 2586
public class ToiletMonitor : GameStateMachine<ToiletMonitor, ToiletMonitor.Instance>
{
	// Token: 0x06004B27 RID: 19239 RVA: 0x001B4130 File Offset: 0x001B2330
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.EventHandler(GameHashes.ToiletSensorChanged, delegate(ToiletMonitor.Instance smi)
		{
			smi.RefreshStatusItem();
		}).Exit("ClearStatusItem", delegate(ToiletMonitor.Instance smi)
		{
			smi.ClearStatusItem();
		});
	}

	// Token: 0x040031D1 RID: 12753
	public GameStateMachine<ToiletMonitor, ToiletMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040031D2 RID: 12754
	public GameStateMachine<ToiletMonitor, ToiletMonitor.Instance, IStateMachineTarget, object>.State unsatisfied;

	// Token: 0x02001AC7 RID: 6855
	public new class Instance : GameStateMachine<ToiletMonitor, ToiletMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A508 RID: 42248 RVA: 0x003A7E5E File Offset: 0x003A605E
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.toiletSensor = base.GetComponent<Sensors>().GetSensor<ToiletSensor>();
		}

		// Token: 0x0600A509 RID: 42249 RVA: 0x003A7E78 File Offset: 0x003A6078
		public void RefreshStatusItem()
		{
			StatusItem statusItem = null;
			if (!this.toiletSensor.AreThereAnyToilets())
			{
				statusItem = Db.Get().DuplicantStatusItems.NoToilets;
			}
			else if (!this.toiletSensor.AreThereAnyUsableToilets())
			{
				statusItem = Db.Get().DuplicantStatusItems.NoUsableToilets;
			}
			else if (this.toiletSensor.GetNearestUsableToilet() == null)
			{
				statusItem = Db.Get().DuplicantStatusItems.ToiletUnreachable;
			}
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Toilet, statusItem, null);
		}

		// Token: 0x0600A50A RID: 42250 RVA: 0x003A7EFF File Offset: 0x003A60FF
		public void ClearStatusItem()
		{
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Toilet, null, null);
		}

		// Token: 0x040080E1 RID: 32993
		private ToiletSensor toiletSensor;
	}
}
