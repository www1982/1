using System;

// Token: 0x020009E2 RID: 2530
public class CringeMonitor : GameStateMachine<CringeMonitor, CringeMonitor.Instance>
{
	// Token: 0x06004A0E RID: 18958 RVA: 0x001ACEC0 File Offset: 0x001AB0C0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.EventHandler(GameHashes.Cringe, new GameStateMachine<CringeMonitor, CringeMonitor.Instance, IStateMachineTarget, object>.GameEvent.Callback(this.TriggerCringe));
		this.cringe.ToggleReactable((CringeMonitor.Instance smi) => smi.GetReactable()).ToggleStatusItem((CringeMonitor.Instance smi) => smi.GetStatusItem(), null).ScheduleGoTo(3f, this.idle);
	}

	// Token: 0x06004A0F RID: 18959 RVA: 0x001ACF52 File Offset: 0x001AB152
	private void TriggerCringe(CringeMonitor.Instance smi, object data)
	{
		if (smi.GetComponent<KPrefabID>().HasTag(GameTags.Suit))
		{
			return;
		}
		smi.SetCringeSourceData(data);
		smi.GoTo(this.cringe);
	}

	// Token: 0x040030D6 RID: 12502
	public GameStateMachine<CringeMonitor, CringeMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x040030D7 RID: 12503
	public GameStateMachine<CringeMonitor, CringeMonitor.Instance, IStateMachineTarget, object>.State cringe;

	// Token: 0x02001A2D RID: 6701
	public new class Instance : GameStateMachine<CringeMonitor, CringeMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A277 RID: 41591 RVA: 0x003A12D9 File Offset: 0x0039F4D9
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600A278 RID: 41592 RVA: 0x003A12E4 File Offset: 0x0039F4E4
		public void SetCringeSourceData(object data)
		{
			string text = (string)data;
			this.statusItem = new StatusItem("CringeSource", text, null, "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
		}

		// Token: 0x0600A279 RID: 41593 RVA: 0x003A1320 File Offset: 0x0039F520
		public Reactable GetReactable()
		{
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "Cringe", Db.Get().ChoreTypes.EmoteHighPriority, 0f, 0f, float.PositiveInfinity, 0f);
			selfEmoteReactable.SetEmote(Db.Get().Emotes.Minion.Cringe);
			selfEmoteReactable.preventChoreInterruption = true;
			return selfEmoteReactable;
		}

		// Token: 0x0600A27A RID: 41594 RVA: 0x003A138C File Offset: 0x0039F58C
		public StatusItem GetStatusItem()
		{
			return this.statusItem;
		}

		// Token: 0x04007EDE RID: 32478
		private StatusItem statusItem;
	}
}
