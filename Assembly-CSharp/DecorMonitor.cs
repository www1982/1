using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020009E7 RID: 2535
public class DecorMonitor : GameStateMachine<DecorMonitor, DecorMonitor.Instance>
{
	// Token: 0x06004A22 RID: 18978 RVA: 0x001AD834 File Offset: 0x001ABA34
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleAttributeModifier("DecorSmoother", (DecorMonitor.Instance smi) => smi.GetDecorModifier(), (DecorMonitor.Instance smi) => true).Update("DecorSensing", delegate(DecorMonitor.Instance smi, float dt)
		{
			smi.Update(dt);
		}, UpdateRate.SIM_200ms, false).EventHandler(GameHashes.NewDay, (DecorMonitor.Instance smi) => GameClock.Instance, delegate(DecorMonitor.Instance smi)
		{
			smi.OnNewDay();
		});
	}

	// Token: 0x040030E7 RID: 12519
	public static float MAXIMUM_DECOR_VALUE = 120f;

	// Token: 0x02001A3E RID: 6718
	public new class Instance : GameStateMachine<DecorMonitor, DecorMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A2B4 RID: 41652 RVA: 0x003A1B24 File Offset: 0x0039FD24
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.cycleTotalDecor = 2250f;
			this.amount = Db.Get().Amounts.Decor.Lookup(base.gameObject);
			this.modifier = new AttributeModifier(Db.Get().Amounts.Decor.deltaAttribute.Id, 1f, DUPLICANTS.NEEDS.DECOR.OBSERVED_DECOR, false, false, false);
		}

		// Token: 0x0600A2B5 RID: 41653 RVA: 0x003A1C55 File Offset: 0x0039FE55
		public AttributeModifier GetDecorModifier()
		{
			return this.modifier;
		}

		// Token: 0x0600A2B6 RID: 41654 RVA: 0x003A1C60 File Offset: 0x0039FE60
		public void Update(float dt)
		{
			int num = Grid.PosToCell(base.gameObject);
			if (!Grid.IsValidCell(num))
			{
				return;
			}
			float decorAtCell = GameUtil.GetDecorAtCell(num);
			this.cycleTotalDecor += decorAtCell * dt;
			float num2 = 0f;
			float num3 = 4.1666665f;
			if (Mathf.Abs(decorAtCell - this.amount.value) > 0.5f)
			{
				if (decorAtCell > this.amount.value)
				{
					num2 = 3f * num3;
				}
				else if (decorAtCell < this.amount.value)
				{
					num2 = -num3;
				}
			}
			else
			{
				this.amount.value = decorAtCell;
			}
			this.modifier.SetValue(num2);
		}

		// Token: 0x0600A2B7 RID: 41655 RVA: 0x003A1D04 File Offset: 0x0039FF04
		public void OnNewDay()
		{
			this.yesterdaysTotalDecor = this.cycleTotalDecor;
			this.cycleTotalDecor = 0f;
			float totalValue = base.gameObject.GetAttributes().Add(Db.Get().Attributes.DecorExpectation).GetTotalValue();
			float num = this.yesterdaysTotalDecor / 600f;
			num += totalValue;
			Effects component = base.gameObject.GetComponent<Effects>();
			foreach (KeyValuePair<float, string> keyValuePair in this.effectLookup)
			{
				if (num < keyValuePair.Key)
				{
					component.Add(keyValuePair.Value, true);
					break;
				}
			}
		}

		// Token: 0x0600A2B8 RID: 41656 RVA: 0x003A1DC8 File Offset: 0x0039FFC8
		public float GetTodaysAverageDecor()
		{
			return this.cycleTotalDecor / (GameClock.Instance.GetCurrentCycleAsPercentage() * 600f);
		}

		// Token: 0x0600A2B9 RID: 41657 RVA: 0x003A1DE1 File Offset: 0x0039FFE1
		public float GetYesterdaysAverageDecor()
		{
			return this.yesterdaysTotalDecor / 600f;
		}

		// Token: 0x04007F0F RID: 32527
		[Serialize]
		private float cycleTotalDecor;

		// Token: 0x04007F10 RID: 32528
		[Serialize]
		private float yesterdaysTotalDecor;

		// Token: 0x04007F11 RID: 32529
		private AmountInstance amount;

		// Token: 0x04007F12 RID: 32530
		private AttributeModifier modifier;

		// Token: 0x04007F13 RID: 32531
		private List<KeyValuePair<float, string>> effectLookup = new List<KeyValuePair<float, string>>
		{
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * -0.25f, "DecorMinus1"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * 0f, "Decor0"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * 0.25f, "Decor1"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * 0.5f, "Decor2"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE * 0.75f, "Decor3"),
			new KeyValuePair<float, string>(DecorMonitor.MAXIMUM_DECOR_VALUE, "Decor4"),
			new KeyValuePair<float, string>(float.MaxValue, "Decor5")
		};
	}
}
