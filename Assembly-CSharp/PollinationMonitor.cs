using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000A6B RID: 2667
public class PollinationMonitor : GameStateMachine<PollinationMonitor, PollinationMonitor.StatesInstance, IStateMachineTarget, PollinationMonitor.Def>
{
	// Token: 0x06004D2E RID: 19758 RVA: 0x001BEE9B File Offset: 0x001BD09B
	public static bool IsPollinationEffect(Effect effect)
	{
		return Array.IndexOf<HashedString>(PollinationMonitor.PollinationEffects, effect.IdHash) != -1;
	}

	// Token: 0x06004D2F RID: 19759 RVA: 0x001BEEB4 File Offset: 0x001BD0B4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.initialize;
		this.initialize.Enter(delegate(PollinationMonitor.StatesInstance smi)
		{
			if (smi.effects == null)
			{
				smi.GoTo(this.not_pollinated);
				return;
			}
			bool flag = false;
			foreach (HashedString hashedString in PollinationMonitor.PollinationEffects)
			{
				if (smi.effects.HasEffect(hashedString))
				{
					flag = true;
					break;
				}
			}
			smi.GoTo(flag ? this.pollinated : this.not_pollinated);
		});
		this.not_pollinated.Enter(delegate(PollinationMonitor.StatesInstance smi)
		{
			smi.Trigger(-200207042, false);
		}).EventHandler(GameHashes.EffectAdded, delegate(PollinationMonitor.StatesInstance smi, object data)
		{
			if (PollinationMonitor.IsPollinationEffect(data as Effect))
			{
				smi.GoTo(this.pollinated);
			}
		});
		this.pollinated.Enter(delegate(PollinationMonitor.StatesInstance smi)
		{
			smi.Trigger(-200207042, true);
		}).EventHandler(GameHashes.EffectRemoved, delegate(PollinationMonitor.StatesInstance smi, object data)
		{
			if (!PollinationMonitor.IsPollinationEffect(data as Effect))
			{
				return;
			}
			if (smi.effects == null)
			{
				smi.GoTo(this.not_pollinated);
				return;
			}
			bool flag2 = false;
			foreach (HashedString hashedString2 in PollinationMonitor.PollinationEffects)
			{
				if (smi.effects.HasEffect(hashedString2))
				{
					flag2 = true;
					break;
				}
			}
			if (!flag2)
			{
				smi.GoTo(this.not_pollinated);
			}
		});
	}

	// Token: 0x04003348 RID: 13128
	public static readonly string INITIALLY_POLLINATED_EFFECT = "InitiallyPollinated";

	// Token: 0x04003349 RID: 13129
	public static readonly HashedString[] PollinationEffects = new HashedString[]
	{
		PollinationMonitor.INITIALLY_POLLINATED_EFFECT,
		"DivergentCropTended",
		"DivergentCropTendedWorm",
		"ButterflyPollinated"
	};

	// Token: 0x0400334A RID: 13130
	public GameStateMachine<PollinationMonitor, PollinationMonitor.StatesInstance, IStateMachineTarget, PollinationMonitor.Def>.State initialize;

	// Token: 0x0400334B RID: 13131
	public GameStateMachine<PollinationMonitor, PollinationMonitor.StatesInstance, IStateMachineTarget, PollinationMonitor.Def>.State not_pollinated;

	// Token: 0x0400334C RID: 13132
	public GameStateMachine<PollinationMonitor, PollinationMonitor.StatesInstance, IStateMachineTarget, PollinationMonitor.Def>.State pollinated;

	// Token: 0x0400334D RID: 13133
	private readonly StateMachine<PollinationMonitor, PollinationMonitor.StatesInstance, IStateMachineTarget, PollinationMonitor.Def>.BoolParameter spawn_pollinated = new StateMachine<PollinationMonitor, PollinationMonitor.StatesInstance, IStateMachineTarget, PollinationMonitor.Def>.BoolParameter(false);

	// Token: 0x02001B38 RID: 6968
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x0600A69A RID: 42650 RVA: 0x003ADCD7 File Offset: 0x003ABED7
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			return new List<Descriptor>
			{
				new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_POLLINATION, UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_POLLINATION, Descriptor.DescriptorType.Requirement, false)
			};
		}
	}

	// Token: 0x02001B39 RID: 6969
	public class StatesInstance : GameStateMachine<PollinationMonitor, PollinationMonitor.StatesInstance, IStateMachineTarget, PollinationMonitor.Def>.GameInstance, IWiltCause
	{
		// Token: 0x0600A69C RID: 42652 RVA: 0x003ADD07 File Offset: 0x003ABF07
		public StatesInstance(IStateMachineTarget master, PollinationMonitor.Def def)
			: base(master, def)
		{
			this.effects = base.GetComponent<Effects>();
			base.Subscribe(1119167081, delegate(object _)
			{
				base.sm.spawn_pollinated.Set(true, this, false);
			});
		}

		// Token: 0x0600A69D RID: 42653 RVA: 0x003ADD34 File Offset: 0x003ABF34
		public override void StartSM()
		{
			base.StartSM();
			if (base.sm.spawn_pollinated.Get(this))
			{
				base.sm.spawn_pollinated.Set(false, this, false);
				if (this.effects != null)
				{
					this.effects.Add(PollinationMonitor.INITIALLY_POLLINATED_EFFECT, true).timeRemaining *= global::UnityEngine.Random.Range(0.75f, 1f);
				}
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600A69E RID: 42654 RVA: 0x003ADDA8 File Offset: 0x003ABFA8
		public WiltCondition.Condition[] Conditions
		{
			get
			{
				return new WiltCondition.Condition[] { WiltCondition.Condition.Pollination };
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600A69F RID: 42655 RVA: 0x003ADDB5 File Offset: 0x003ABFB5
		public string WiltStateString
		{
			get
			{
				if (!base.IsInsideState(base.sm.not_pollinated))
				{
					return "";
				}
				return Db.Get().CreatureStatusItems.NotPollinated.GetName(this);
			}
		}

		// Token: 0x040081EC RID: 33260
		public Effects effects;
	}
}
