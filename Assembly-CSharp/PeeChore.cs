using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200048F RID: 1167
public class PeeChore : Chore<PeeChore.StatesInstance>
{
	// Token: 0x0600187F RID: 6271 RVA: 0x00088C8C File Offset: 0x00086E8C
	public PeeChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.Pee, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new PeeChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x020012A1 RID: 4769
	public class StatesInstance : GameStateMachine<PeeChore.States, PeeChore.StatesInstance, PeeChore, object>.GameInstance
	{
		// Token: 0x0600871D RID: 34589 RVA: 0x003423C4 File Offset: 0x003405C4
		public StatesInstance(PeeChore master, GameObject worker)
			: base(master)
		{
			this.bladder = Db.Get().Amounts.Bladder.Lookup(worker);
			this.bodyTemperature = Db.Get().Amounts.Temperature.Lookup(worker);
			base.sm.worker.Set(worker, base.smi, false);
		}

		// Token: 0x0600871E RID: 34590 RVA: 0x00342469 File Offset: 0x00340669
		public bool IsDonePeeing()
		{
			return this.bladder.value <= 0f;
		}

		// Token: 0x0600871F RID: 34591 RVA: 0x00342480 File Offset: 0x00340680
		public void SpawnDirtyWater(float dt)
		{
			int num = Grid.PosToCell(base.sm.worker.Get<KMonoBehaviour>(base.smi));
			byte index = Db.Get().Diseases.GetIndex(DUPLICANTSTATS.STANDARD.Secretions.PEE_DISEASE);
			float num2 = dt * -this.bladder.GetDelta() / this.bladder.GetMax();
			if (num2 > 0f)
			{
				float num3 = DUPLICANTSTATS.STANDARD.Secretions.PEE_PER_FLOOR_PEE * num2;
				Equippable equippable = base.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
				if (equippable != null)
				{
					equippable.GetComponent<Storage>().AddLiquid(SimHashes.DirtyWater, num3, this.bodyTemperature.value, index, Mathf.CeilToInt((float)DUPLICANTSTATS.STANDARD.Secretions.DISEASE_PER_PEE * num2), false, true);
					return;
				}
				SimMessages.AddRemoveSubstance(num, SimHashes.DirtyWater, CellEventLogger.Instance.Vomit, num3, this.bodyTemperature.value, index, Mathf.CeilToInt((float)DUPLICANTSTATS.STANDARD.Secretions.DISEASE_PER_PEE * num2), true, -1);
			}
		}

		// Token: 0x040066F7 RID: 26359
		public Notification stressfullyEmptyingBladder = new Notification(DUPLICANTS.STATUSITEMS.STRESSFULLYEMPTYINGBLADDER.NOTIFICATION_NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => DUPLICANTS.STATUSITEMS.STRESSFULLYEMPTYINGBLADDER.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);

		// Token: 0x040066F8 RID: 26360
		public AmountInstance bladder;

		// Token: 0x040066F9 RID: 26361
		private AmountInstance bodyTemperature;
	}

	// Token: 0x020012A2 RID: 4770
	public class States : GameStateMachine<PeeChore.States, PeeChore.StatesInstance, PeeChore>
	{
		// Token: 0x06008720 RID: 34592 RVA: 0x00342590 File Offset: 0x00340790
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.running;
			base.Target(this.worker);
			this.running.ToggleAnims("anim_expel_kanim", 0f).ToggleEffect("StressfulyEmptyingBladder").DoNotification((PeeChore.StatesInstance smi) => smi.stressfullyEmptyingBladder)
				.DoReport(ReportManager.ReportType.ToiletIncident, (PeeChore.StatesInstance smi) => 1f, (PeeChore.StatesInstance smi) => this.masterTarget.Get(smi).GetProperName())
				.DoTutorial(Tutorial.TutorialMessages.TM_Mopping)
				.Transition(null, (PeeChore.StatesInstance smi) => smi.IsDonePeeing(), UpdateRate.SIM_200ms)
				.Update("SpawnDirtyWater", delegate(PeeChore.StatesInstance smi, float dt)
				{
					smi.SpawnDirtyWater(dt);
				}, UpdateRate.SIM_200ms, false)
				.PlayAnim("working_loop", KAnim.PlayMode.Loop)
				.ToggleTag(GameTags.MakingMess)
				.Enter(delegate(PeeChore.StatesInstance smi)
				{
					if (Sim.IsRadiationEnabled() && smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).value > 0f)
					{
						smi.master.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, null);
					}
				})
				.Exit(delegate(PeeChore.StatesInstance smi)
				{
					if (Sim.IsRadiationEnabled())
					{
						smi.master.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
						AmountInstance amountInstance = smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id);
						RadiationMonitor.Instance smi2 = smi.master.gameObject.GetSMI<RadiationMonitor.Instance>();
						if (smi2 != null)
						{
							float num = Math.Min(amountInstance.value, 100f * smi2.difficultySettingMod);
							smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id).ApplyDelta(-num);
							if (num >= 1f)
							{
								PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, Mathf.FloorToInt(num).ToString() + UI.UNITSUFFIXES.RADIATION.RADS, smi.master.transform, 1.5f, false);
							}
						}
					}
				});
		}

		// Token: 0x040066FA RID: 26362
		public StateMachine<PeeChore.States, PeeChore.StatesInstance, PeeChore, object>.TargetParameter worker;

		// Token: 0x040066FB RID: 26363
		public GameStateMachine<PeeChore.States, PeeChore.StatesInstance, PeeChore, object>.State running;
	}
}
