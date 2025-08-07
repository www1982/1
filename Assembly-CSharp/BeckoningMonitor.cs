using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000851 RID: 2129
public class BeckoningMonitor : GameStateMachine<BeckoningMonitor, BeckoningMonitor.Instance, IStateMachineTarget, BeckoningMonitor.Def>
{
	// Token: 0x06003A7D RID: 14973 RVA: 0x00145724 File Offset: 0x00143924
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventHandler(GameHashes.CaloriesConsumed, delegate(BeckoningMonitor.Instance smi, object data)
		{
			smi.OnCaloriesConsumed(data);
		}).ToggleBehaviour(GameTags.Creatures.WantsToBeckon, (BeckoningMonitor.Instance smi) => smi.IsReadyToBeckon(), null).Update(delegate(BeckoningMonitor.Instance smi, float dt)
		{
			smi.UpdateBlockedStatusItem();
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x020017D2 RID: 6098
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06009A83 RID: 39555 RVA: 0x0038AB3F File Offset: 0x00388D3F
		public override void Configure(GameObject prefab)
		{
			prefab.AddOrGet<Modifiers>().initialAmounts.Add(Db.Get().Amounts.Beckoning.Id);
		}

		// Token: 0x04007713 RID: 30483
		public float caloriesPerCycle;

		// Token: 0x04007714 RID: 30484
		public string effectId = "MooWellFed";
	}

	// Token: 0x020017D3 RID: 6099
	public new class Instance : GameStateMachine<BeckoningMonitor, BeckoningMonitor.Instance, IStateMachineTarget, BeckoningMonitor.Def>.GameInstance
	{
		// Token: 0x06009A85 RID: 39557 RVA: 0x0038AB78 File Offset: 0x00388D78
		public Instance(IStateMachineTarget master, BeckoningMonitor.Def def)
			: base(master, def)
		{
			this.beckoning = Db.Get().Amounts.Beckoning.Lookup(base.gameObject);
		}

		// Token: 0x06009A86 RID: 39558 RVA: 0x0038ABA4 File Offset: 0x00388DA4
		private bool IsSpaceVisible()
		{
			int num = Grid.PosToCell(this);
			return Grid.IsValidCell(num) && Grid.ExposedToSunlight[num] > 0;
		}

		// Token: 0x06009A87 RID: 39559 RVA: 0x0038ABD0 File Offset: 0x00388DD0
		private bool IsBeckoningAvailable()
		{
			return base.smi.beckoning.value >= base.smi.beckoning.GetMax();
		}

		// Token: 0x06009A88 RID: 39560 RVA: 0x0038ABF7 File Offset: 0x00388DF7
		public bool IsReadyToBeckon()
		{
			return this.IsBeckoningAvailable() && this.IsSpaceVisible();
		}

		// Token: 0x06009A89 RID: 39561 RVA: 0x0038AC0C File Offset: 0x00388E0C
		public void UpdateBlockedStatusItem()
		{
			bool flag = this.IsSpaceVisible();
			if (!flag && this.IsBeckoningAvailable() && this.beckoningBlockedHandle == Guid.Empty)
			{
				this.beckoningBlockedHandle = this.kselectable.AddStatusItem(Db.Get().CreatureStatusItems.BeckoningBlocked, null);
				return;
			}
			if (flag)
			{
				this.beckoningBlockedHandle = this.kselectable.RemoveStatusItem(this.beckoningBlockedHandle, false);
			}
		}

		// Token: 0x06009A8A RID: 39562 RVA: 0x0038AC7C File Offset: 0x00388E7C
		public void OnCaloriesConsumed(object data)
		{
			CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = (CreatureCalorieMonitor.CaloriesConsumedEvent)data;
			EffectInstance effectInstance = this.effects.Get(base.smi.def.effectId);
			if (effectInstance == null)
			{
				effectInstance = this.effects.Add(base.smi.def.effectId, true);
			}
			effectInstance.timeRemaining += caloriesConsumedEvent.calories / base.smi.def.caloriesPerCycle * 600f;
		}

		// Token: 0x04007715 RID: 30485
		private AmountInstance beckoning;

		// Token: 0x04007716 RID: 30486
		[MyCmpGet]
		private Effects effects;

		// Token: 0x04007717 RID: 30487
		[MyCmpGet]
		public KSelectable kselectable;

		// Token: 0x04007718 RID: 30488
		private Guid beckoningBlockedHandle;
	}
}
