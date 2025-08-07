using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000A04 RID: 2564
public class RecreationTimeMonitor : GameStateMachine<RecreationTimeMonitor, RecreationTimeMonitor.Instance, IStateMachineTarget, RecreationTimeMonitor.Def>
{
	// Token: 0x06004ABF RID: 19135 RVA: 0x001B152C File Offset: 0x001AF72C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.EventHandler(GameHashes.ScheduleBlocksTick, delegate(RecreationTimeMonitor.Instance smi)
		{
			smi.OnScheduleBlocksTick();
		}).Update(delegate(RecreationTimeMonitor.Instance smi, float dt)
		{
			smi.RefreshTimes();
		}, UpdateRate.SIM_200ms, false);
		this.bonusActive.ToggleEffect((RecreationTimeMonitor.Instance smi) => smi.moraleEffect).EventHandler(GameHashes.ScheduleBlocksTick, delegate(RecreationTimeMonitor.Instance smi)
		{
			smi.OnScheduleBlocksTick();
		}).Update(delegate(RecreationTimeMonitor.Instance smi, float dt)
		{
			smi.RefreshTimes();
		}, UpdateRate.SIM_200ms, false);
	}

	// Token: 0x0400316F RID: 12655
	public const int MAX_BONUS = 5;

	// Token: 0x04003170 RID: 12656
	public const float BONUS_DURATION_STANDARD = 600f;

	// Token: 0x04003171 RID: 12657
	public const float BONUS_DURATION_BIONICS = 1800f;

	// Token: 0x04003172 RID: 12658
	public GameStateMachine<RecreationTimeMonitor, RecreationTimeMonitor.Instance, IStateMachineTarget, RecreationTimeMonitor.Def>.State idle;

	// Token: 0x04003173 RID: 12659
	public GameStateMachine<RecreationTimeMonitor, RecreationTimeMonitor.Instance, IStateMachineTarget, RecreationTimeMonitor.Def>.State bonusActive;

	// Token: 0x02001A89 RID: 6793
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A8A RID: 6794
	public new class Instance : GameStateMachine<RecreationTimeMonitor, RecreationTimeMonitor.Instance, IStateMachineTarget, RecreationTimeMonitor.Def>.GameInstance
	{
		// Token: 0x0600A3F0 RID: 41968 RVA: 0x003A4E5C File Offset: 0x003A305C
		public Instance(IStateMachineTarget master, RecreationTimeMonitor.Def def)
			: base(master, def)
		{
			this.bonus_duration = ((base.gameObject.PrefabID() == BionicMinionConfig.ID) ? 1800f : 600f);
			this.schedulable = master.GetComponent<Schedulable>();
			this.moraleModifier = new AttributeModifier(Db.Get().Attributes.QualityOfLife.Id, 0f, delegate
			{
				if (Mathf.Clamp(this.moraleAddedTimes.Count - 1, 0, 5) == 5)
				{
					return DUPLICANTS.MODIFIERS.BREAK_BONUS.MAX_NAME;
				}
				return DUPLICANTS.MODIFIERS.BREAK_BONUS.NAME;
			}, false, false);
			this.moraleEffect.Add(this.moraleModifier);
			if ((SaveLoader.Instance.GameInfo.saveMajorVersion != 0 || SaveLoader.Instance.GameInfo.saveMinorVersion != 0) && SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 35))
			{
				this.RestoreFromSchedule();
			}
		}

		// Token: 0x0600A3F1 RID: 41969 RVA: 0x003A4F73 File Offset: 0x003A3173
		public override void StartSM()
		{
			base.StartSM();
			this.RefreshTimes();
		}

		// Token: 0x0600A3F2 RID: 41970 RVA: 0x003A4F84 File Offset: 0x003A3184
		public void RefreshTimes()
		{
			for (int i = this.moraleAddedTimes.Count - 1; i >= 0; i--)
			{
				if (GameClock.Instance.GetTime() - this.moraleAddedTimes[i] > this.bonus_duration)
				{
					this.moraleAddedTimes.RemoveAt(i);
				}
			}
			int num = Math.Clamp(this.moraleAddedTimes.Count - 1, 0, 5);
			this.moraleModifier.SetValue((float)num);
			if (num > 0)
			{
				if (base.smi.GetCurrentState() != base.smi.sm.bonusActive)
				{
					base.smi.GoTo(base.smi.sm.bonusActive);
					return;
				}
			}
			else if (base.smi.GetCurrentState() != base.smi.sm.idle)
			{
				base.smi.GoTo(base.smi.sm.idle);
			}
		}

		// Token: 0x0600A3F3 RID: 41971 RVA: 0x003A506C File Offset: 0x003A326C
		public void OnScheduleBlocksTick()
		{
			if (ScheduleManager.Instance.GetSchedule(this.schedulable).GetPreviousScheduleBlock().GroupId == Db.Get().ScheduleGroups.Recreation.Id)
			{
				this.moraleAddedTimes.Add(GameClock.Instance.GetTime());
			}
		}

		// Token: 0x0600A3F4 RID: 41972 RVA: 0x003A50C4 File Offset: 0x003A32C4
		private void RestoreFromSchedule()
		{
			Effects component = base.GetComponent<Effects>();
			foreach (string text in new string[] { "Break1", "Break2", "Break3", "Break4", "Break5" })
			{
				if (component.HasEffect(text))
				{
					component.Remove(text);
				}
			}
			Schedule schedule = ScheduleManager.Instance.GetSchedule(this.schedulable);
			List<ScheduleBlock> blocks = schedule.GetBlocks();
			int currentBlockIdx = schedule.GetCurrentBlockIdx();
			int num = 24;
			if (GameClock.Instance.GetTime() <= this.bonus_duration)
			{
				num = Math.Min(currentBlockIdx, Mathf.FloorToInt(GameClock.Instance.GetTime() / 25f));
			}
			for (int j = currentBlockIdx - num; j < currentBlockIdx; j++)
			{
				int k = j;
				global::Debug.Assert(blocks.Count > 0);
				while (k < 0)
				{
					k += blocks.Count;
				}
				if (blocks[k].GroupId == Db.Get().ScheduleGroups.Recreation.Id)
				{
					int num2;
					if (k > currentBlockIdx)
					{
						num2 = blocks.Count - k + currentBlockIdx - 1;
					}
					else
					{
						num2 = currentBlockIdx - k - 1;
					}
					float num3 = (float)num2 * 25f;
					float num4 = GameClock.Instance.GetTime() - num3;
					global::Debug.Assert(num4 > 0f);
					this.moraleAddedTimes.Add(num4);
				}
			}
		}

		// Token: 0x04008003 RID: 32771
		[Serialize]
		public List<float> moraleAddedTimes = new List<float>();

		// Token: 0x04008004 RID: 32772
		public Effect moraleEffect = new Effect("RecTimeEffect", "Rec Time Effect", "Rec Time Effect Description", 0f, false, false, false, null, -1f, 0f, null, "");

		// Token: 0x04008005 RID: 32773
		private Schedulable schedulable;

		// Token: 0x04008006 RID: 32774
		private AttributeModifier moraleModifier;

		// Token: 0x04008007 RID: 32775
		private int shiftValue;

		// Token: 0x04008008 RID: 32776
		private float bonus_duration;
	}
}
