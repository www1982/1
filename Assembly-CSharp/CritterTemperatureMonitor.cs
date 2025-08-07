using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200085D RID: 2141
public class CritterTemperatureMonitor : GameStateMachine<CritterTemperatureMonitor, CritterTemperatureMonitor.Instance, IStateMachineTarget, CritterTemperatureMonitor.Def>
{
	// Token: 0x06003AC3 RID: 15043 RVA: 0x001469C0 File Offset: 0x00144BC0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.comfortable;
		this.uncomfortableEffect = new Effect("EffectCritterTemperatureUncomfortable", CREATURES.MODIFIERS.CRITTER_TEMPERATURE_UNCOMFORTABLE.NAME, CREATURES.MODIFIERS.CRITTER_TEMPERATURE_UNCOMFORTABLE.TOOLTIP, 0f, false, false, true, null, -1f, 0f, null, "");
		this.uncomfortableEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -1f, CREATURES.MODIFIERS.CRITTER_TEMPERATURE_UNCOMFORTABLE.NAME, false, false, true));
		this.deadlyEffect = new Effect("EffectCritterTemperatureDeadly", CREATURES.MODIFIERS.CRITTER_TEMPERATURE_DEADLY.NAME, CREATURES.MODIFIERS.CRITTER_TEMPERATURE_DEADLY.TOOLTIP, 0f, false, false, true, null, -1f, 0f, null, "");
		this.deadlyEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, -2f, CREATURES.MODIFIERS.CRITTER_TEMPERATURE_DEADLY.NAME, false, false, true));
		this.root.Enter(new StateMachine<CritterTemperatureMonitor, CritterTemperatureMonitor.Instance, IStateMachineTarget, CritterTemperatureMonitor.Def>.State.Callback(CritterTemperatureMonitor.RefreshInternalTemperature)).Update(delegate(CritterTemperatureMonitor.Instance smi, float dt)
		{
			StateMachine.BaseState targetState = smi.GetTargetState();
			if (smi.GetCurrentState() != targetState)
			{
				smi.GoTo(targetState);
			}
		}, UpdateRate.SIM_200ms, false).Update(new Action<CritterTemperatureMonitor.Instance, float>(CritterTemperatureMonitor.UpdateInternalTemperature), UpdateRate.SIM_1000ms, false);
		this.hot.TagTransition(GameTags.Dead, this.dead, false).ToggleCreatureThought(Db.Get().Thoughts.Hot, null);
		this.cold.TagTransition(GameTags.Dead, this.dead, false).ToggleCreatureThought(Db.Get().Thoughts.Cold, null);
		this.hot.uncomfortable.ToggleStatusItem(Db.Get().CreatureStatusItems.TemperatureHotUncomfortable, null).ToggleEffect((CritterTemperatureMonitor.Instance smi) => this.uncomfortableEffect);
		this.hot.deadly.ToggleStatusItem(Db.Get().CreatureStatusItems.TemperatureHotDeadly, null).ToggleEffect((CritterTemperatureMonitor.Instance smi) => this.deadlyEffect).Enter(delegate(CritterTemperatureMonitor.Instance smi)
		{
			smi.ResetDamageCooldown();
		})
			.Update(delegate(CritterTemperatureMonitor.Instance smi, float dt)
			{
				smi.TryDamage(dt);
			}, UpdateRate.SIM_200ms, false);
		this.cold.uncomfortable.ToggleStatusItem(Db.Get().CreatureStatusItems.TemperatureColdUncomfortable, null).ToggleEffect((CritterTemperatureMonitor.Instance smi) => this.uncomfortableEffect);
		this.cold.deadly.ToggleStatusItem(Db.Get().CreatureStatusItems.TemperatureColdDeadly, null).ToggleEffect((CritterTemperatureMonitor.Instance smi) => this.deadlyEffect).Enter(delegate(CritterTemperatureMonitor.Instance smi)
		{
			smi.ResetDamageCooldown();
		})
			.Update(delegate(CritterTemperatureMonitor.Instance smi, float dt)
			{
				smi.TryDamage(dt);
			}, UpdateRate.SIM_200ms, false);
		this.dead.DoNothing();
	}

	// Token: 0x06003AC4 RID: 15044 RVA: 0x00146CCE File Offset: 0x00144ECE
	public static void UpdateInternalTemperature(CritterTemperatureMonitor.Instance smi, float dt)
	{
		CritterTemperatureMonitor.RefreshInternalTemperature(smi);
		if (smi.OnUpdate_GetTemperatureInternal != null)
		{
			smi.OnUpdate_GetTemperatureInternal(dt, smi.GetTemperatureInternal());
		}
	}

	// Token: 0x06003AC5 RID: 15045 RVA: 0x00146CF0 File Offset: 0x00144EF0
	public static void RefreshInternalTemperature(CritterTemperatureMonitor.Instance smi)
	{
		if (smi.temperature != null)
		{
			smi.temperature.SetValue(smi.GetTemperatureInternal());
		}
	}

	// Token: 0x04002404 RID: 9220
	public GameStateMachine<CritterTemperatureMonitor, CritterTemperatureMonitor.Instance, IStateMachineTarget, CritterTemperatureMonitor.Def>.State comfortable;

	// Token: 0x04002405 RID: 9221
	public GameStateMachine<CritterTemperatureMonitor, CritterTemperatureMonitor.Instance, IStateMachineTarget, CritterTemperatureMonitor.Def>.State dead;

	// Token: 0x04002406 RID: 9222
	public CritterTemperatureMonitor.TemperatureStates hot;

	// Token: 0x04002407 RID: 9223
	public CritterTemperatureMonitor.TemperatureStates cold;

	// Token: 0x04002408 RID: 9224
	public Effect uncomfortableEffect;

	// Token: 0x04002409 RID: 9225
	public Effect deadlyEffect;

	// Token: 0x020017E9 RID: 6121
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06009AC7 RID: 39623 RVA: 0x0038B2A9 File Offset: 0x003894A9
		public float GetIdealTemperature()
		{
			return (this.temperatureHotUncomfortable + this.temperatureColdUncomfortable) / 2f;
		}

		// Token: 0x04007748 RID: 30536
		public float temperatureHotDeadly = float.MaxValue;

		// Token: 0x04007749 RID: 30537
		public float temperatureHotUncomfortable = float.MaxValue;

		// Token: 0x0400774A RID: 30538
		public float temperatureColdDeadly = float.MinValue;

		// Token: 0x0400774B RID: 30539
		public float temperatureColdUncomfortable = float.MinValue;

		// Token: 0x0400774C RID: 30540
		public float secondsUntilDamageStarts = 1f;

		// Token: 0x0400774D RID: 30541
		public float damagePerSecond = 0.25f;
	}

	// Token: 0x020017EA RID: 6122
	public class TemperatureStates : GameStateMachine<CritterTemperatureMonitor, CritterTemperatureMonitor.Instance, IStateMachineTarget, CritterTemperatureMonitor.Def>.State
	{
		// Token: 0x0400774E RID: 30542
		public GameStateMachine<CritterTemperatureMonitor, CritterTemperatureMonitor.Instance, IStateMachineTarget, CritterTemperatureMonitor.Def>.State uncomfortable;

		// Token: 0x0400774F RID: 30543
		public GameStateMachine<CritterTemperatureMonitor, CritterTemperatureMonitor.Instance, IStateMachineTarget, CritterTemperatureMonitor.Def>.State deadly;
	}

	// Token: 0x020017EB RID: 6123
	public new class Instance : GameStateMachine<CritterTemperatureMonitor, CritterTemperatureMonitor.Instance, IStateMachineTarget, CritterTemperatureMonitor.Def>.GameInstance
	{
		// Token: 0x06009ACA RID: 39626 RVA: 0x0038B320 File Offset: 0x00389520
		public Instance(IStateMachineTarget master, CritterTemperatureMonitor.Def def)
			: base(master, def)
		{
			this.health = master.GetComponent<Health>();
			this.occupyArea = master.GetComponent<OccupyArea>();
			this.primaryElement = master.GetComponent<PrimaryElement>();
			this.temperature = Db.Get().Amounts.CritterTemperature.Lookup(base.gameObject);
			this.pickupable = master.GetComponent<Pickupable>();
		}

		// Token: 0x06009ACB RID: 39627 RVA: 0x0038B385 File Offset: 0x00389585
		public void ResetDamageCooldown()
		{
			this.secondsUntilDamage = base.def.secondsUntilDamageStarts;
		}

		// Token: 0x06009ACC RID: 39628 RVA: 0x0038B398 File Offset: 0x00389598
		public void TryDamage(float deltaSeconds)
		{
			if (this.secondsUntilDamage <= 0f)
			{
				this.health.Damage(base.def.damagePerSecond);
				this.secondsUntilDamage = 1f;
				return;
			}
			this.secondsUntilDamage -= deltaSeconds;
		}

		// Token: 0x06009ACD RID: 39629 RVA: 0x0038B3D8 File Offset: 0x003895D8
		public StateMachine.BaseState GetTargetState()
		{
			bool flag = this.IsEntirelyInVaccum();
			float temperatureExternal = this.GetTemperatureExternal();
			float temperatureInternal = this.GetTemperatureInternal();
			StateMachine.BaseState baseState;
			if (this.pickupable.KPrefabID.HasTag(GameTags.Dead))
			{
				baseState = base.sm.dead;
			}
			else if (!flag && temperatureExternal > base.def.temperatureHotDeadly)
			{
				baseState = base.sm.hot.deadly;
			}
			else if (!flag && temperatureExternal < base.def.temperatureColdDeadly)
			{
				baseState = base.sm.cold.deadly;
			}
			else if (temperatureInternal > base.def.temperatureHotUncomfortable)
			{
				baseState = base.sm.hot.uncomfortable;
			}
			else if (temperatureInternal < base.def.temperatureColdUncomfortable)
			{
				baseState = base.sm.cold.uncomfortable;
			}
			else
			{
				baseState = base.sm.comfortable;
			}
			return baseState;
		}

		// Token: 0x06009ACE RID: 39630 RVA: 0x0038B4BC File Offset: 0x003896BC
		public bool IsEntirelyInVaccum()
		{
			int cachedCell = this.pickupable.cachedCell;
			bool flag;
			if (this.occupyArea != null)
			{
				flag = true;
				for (int i = 0; i < this.occupyArea.OccupiedCellsOffsets.Length; i++)
				{
					int num = Grid.OffsetCell(cachedCell, this.occupyArea.OccupiedCellsOffsets[i]);
					if (!Grid.IsValidCell(num) || !Grid.Element[num].IsVacuum)
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = !Grid.IsValidCell(cachedCell) || Grid.Element[cachedCell].IsVacuum;
			}
			return flag;
		}

		// Token: 0x06009ACF RID: 39631 RVA: 0x0038B54A File Offset: 0x0038974A
		public float GetTemperatureInternal()
		{
			return this.primaryElement.Temperature;
		}

		// Token: 0x06009AD0 RID: 39632 RVA: 0x0038B558 File Offset: 0x00389758
		public float GetTemperatureExternal()
		{
			int cachedCell = this.pickupable.cachedCell;
			if (this.occupyArea != null)
			{
				float num = 0f;
				int num2 = 0;
				for (int i = 0; i < this.occupyArea.OccupiedCellsOffsets.Length; i++)
				{
					int num3 = Grid.OffsetCell(cachedCell, this.occupyArea.OccupiedCellsOffsets[i]);
					if (Grid.IsValidCell(num3))
					{
						bool flag = Grid.Element[num3].id == SimHashes.Vacuum || Grid.Element[num3].id == SimHashes.Void;
						num2++;
						num += (flag ? this.GetTemperatureInternal() : Grid.Temperature[num3]);
					}
				}
				return num / (float)Mathf.Max(1, num2);
			}
			if (Grid.Element[cachedCell].id != SimHashes.Vacuum && Grid.Element[cachedCell].id != SimHashes.Void)
			{
				return Grid.Temperature[cachedCell];
			}
			return this.GetTemperatureInternal();
		}

		// Token: 0x04007750 RID: 30544
		public AmountInstance temperature;

		// Token: 0x04007751 RID: 30545
		public Health health;

		// Token: 0x04007752 RID: 30546
		public OccupyArea occupyArea;

		// Token: 0x04007753 RID: 30547
		public PrimaryElement primaryElement;

		// Token: 0x04007754 RID: 30548
		public Pickupable pickupable;

		// Token: 0x04007755 RID: 30549
		public float secondsUntilDamage;

		// Token: 0x04007756 RID: 30550
		public Action<float, float> OnUpdate_GetTemperatureInternal;
	}
}
