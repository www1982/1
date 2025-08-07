using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000FEE RID: 4078
	public class PeriodicEmoteSickness : Sickness.SicknessComponent
	{
		// Token: 0x06007DDE RID: 32222 RVA: 0x003265AC File Offset: 0x003247AC
		public PeriodicEmoteSickness(Emote emote, float cooldown)
		{
			this.emote = emote;
			this.cooldown = cooldown;
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x003265C2 File Offset: 0x003247C2
		public override object OnInfect(GameObject go, SicknessInstance diseaseInstance)
		{
			PeriodicEmoteSickness.StatesInstance statesInstance = new PeriodicEmoteSickness.StatesInstance(diseaseInstance, this);
			statesInstance.StartSM();
			return statesInstance;
		}

		// Token: 0x06007DE0 RID: 32224 RVA: 0x003265D1 File Offset: 0x003247D1
		public override void OnCure(GameObject go, object instance_data)
		{
			((PeriodicEmoteSickness.StatesInstance)instance_data).StopSM("Cured");
		}

		// Token: 0x04005EF3 RID: 24307
		private Emote emote;

		// Token: 0x04005EF4 RID: 24308
		private float cooldown;

		// Token: 0x020025CA RID: 9674
		public class StatesInstance : GameStateMachine<PeriodicEmoteSickness.States, PeriodicEmoteSickness.StatesInstance, SicknessInstance, object>.GameInstance
		{
			// Token: 0x0600C193 RID: 49555 RVA: 0x00406D90 File Offset: 0x00404F90
			public StatesInstance(SicknessInstance master, PeriodicEmoteSickness periodicEmoteSickness)
				: base(master)
			{
				this.periodicEmoteSickness = periodicEmoteSickness;
			}

			// Token: 0x0600C194 RID: 49556 RVA: 0x00406DA0 File Offset: 0x00404FA0
			public Reactable GetReactable()
			{
				return new SelfEmoteReactable(base.master.gameObject, "PeriodicEmoteSickness", Db.Get().ChoreTypes.Emote, 0f, this.periodicEmoteSickness.cooldown, float.PositiveInfinity, 0f).SetEmote(this.periodicEmoteSickness.emote).SetOverideAnimSet("anim_sneeze_kanim");
			}

			// Token: 0x0400A8C9 RID: 43209
			public PeriodicEmoteSickness periodicEmoteSickness;
		}

		// Token: 0x020025CB RID: 9675
		public class States : GameStateMachine<PeriodicEmoteSickness.States, PeriodicEmoteSickness.StatesInstance, SicknessInstance>
		{
			// Token: 0x0600C195 RID: 49557 RVA: 0x00406E0A File Offset: 0x0040500A
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.root;
				this.root.ToggleReactable((PeriodicEmoteSickness.StatesInstance smi) => smi.GetReactable());
			}
		}
	}
}
