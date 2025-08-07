using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200059B RID: 1435
public class LureableMonitor : GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>
{
	// Token: 0x060020C2 RID: 8386 RVA: 0x000BD41C File Offset: 0x000BB61C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.cooldown;
		this.cooldown.ScheduleGoTo((LureableMonitor.Instance smi) => smi.def.cooldown, this.nolure);
		this.nolure.PreBrainUpdate(delegate(LureableMonitor.Instance smi)
		{
			smi.FindLure();
		}).ParamTransition<GameObject>(this.targetLure, this.haslure, (LureableMonitor.Instance smi, GameObject p) => p != null);
		this.haslure.ParamTransition<GameObject>(this.targetLure, this.nolure, (LureableMonitor.Instance smi, GameObject p) => p == null).PreBrainUpdate(delegate(LureableMonitor.Instance smi)
		{
			smi.FindLure();
		}).ToggleBehaviour(GameTags.Creatures.MoveToLure, (LureableMonitor.Instance smi) => smi.HasLure(), delegate(LureableMonitor.Instance smi)
		{
			smi.GoTo(this.cooldown);
		});
	}

	// Token: 0x0400130F RID: 4879
	public StateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.TargetParameter targetLure;

	// Token: 0x04001310 RID: 4880
	public GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.State nolure;

	// Token: 0x04001311 RID: 4881
	public GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.State haslure;

	// Token: 0x04001312 RID: 4882
	public GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.State cooldown;

	// Token: 0x02001417 RID: 5143
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06008C6F RID: 35951 RVA: 0x00355DC4 File Offset: 0x00353FC4
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			List<Descriptor> list = new List<Descriptor>();
			foreach (Tag tag in this.lures)
			{
				if (tag == GameTags.Creatures.FlyersLure)
				{
					list.Add(new Descriptor(UI.BUILDINGEFFECTS.CAPTURE_METHOD_FLYING_TRAP, UI.BUILDINGEFFECTS.TOOLTIPS.CAPTURE_METHOD_FLYING_TRAP, Descriptor.DescriptorType.Effect, false));
				}
				else if (tag == GameTags.Creatures.FishTrapLure)
				{
					list.Add(new Descriptor(UI.BUILDINGEFFECTS.CAPTURE_METHOD_FISH_TRAP, UI.BUILDINGEFFECTS.TOOLTIPS.CAPTURE_METHOD_FISH_TRAP, Descriptor.DescriptorType.Effect, false));
				}
			}
			return list;
		}

		// Token: 0x04006BA4 RID: 27556
		public float cooldown = 20f;

		// Token: 0x04006BA5 RID: 27557
		public Tag[] lures;
	}

	// Token: 0x02001418 RID: 5144
	public new class Instance : GameStateMachine<LureableMonitor, LureableMonitor.Instance, IStateMachineTarget, LureableMonitor.Def>.GameInstance
	{
		// Token: 0x06008C71 RID: 35953 RVA: 0x00355E66 File Offset: 0x00354066
		public Instance(IStateMachineTarget master, LureableMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008C72 RID: 35954 RVA: 0x00355E70 File Offset: 0x00354070
		private static bool FindLureCounter(object obj, LureableMonitor.Instance.FindLureCounterContext context)
		{
			Lure.Instance instance = obj as Lure.Instance;
			if (instance == null || !instance.IsActive() || !instance.HasAnyLure(context.inst.def.lures))
			{
				return true;
			}
			int navigationCost = context.inst.navigator.GetNavigationCost(Grid.PosToCell(instance.transform.GetPosition()), instance.LurePoints);
			if (navigationCost != -1 && (context.cost == -1 || navigationCost < context.cost))
			{
				context.cost = navigationCost;
				context.result = instance.gameObject;
			}
			return true;
		}

		// Token: 0x06008C73 RID: 35955 RVA: 0x00355EFC File Offset: 0x003540FC
		public void FindLure()
		{
			LureableMonitor.Instance.context.inst = this;
			LureableMonitor.Instance.context.cost = -1;
			LureableMonitor.Instance.context.result = null;
			GameScenePartitioner.Instance.AsyncSafeVisit<LureableMonitor.Instance.FindLureCounterContext>(Grid.PosToCell(base.smi.transform.GetPosition()), 1, GameScenePartitioner.Instance.lure, new Func<object, LureableMonitor.Instance.FindLureCounterContext, bool>(LureableMonitor.Instance.FindLureCounter), LureableMonitor.Instance.context);
			base.sm.targetLure.Set(LureableMonitor.Instance.context.result, this, false);
		}

		// Token: 0x06008C74 RID: 35956 RVA: 0x00355F82 File Offset: 0x00354182
		public bool HasLure()
		{
			return base.sm.targetLure.Get(this) != null;
		}

		// Token: 0x06008C75 RID: 35957 RVA: 0x00355F9B File Offset: 0x0035419B
		public GameObject GetTargetLure()
		{
			return base.sm.targetLure.Get(this);
		}

		// Token: 0x04006BA6 RID: 27558
		[MyCmpReq]
		private Navigator navigator;

		// Token: 0x04006BA7 RID: 27559
		private static LureableMonitor.Instance.FindLureCounterContext context = new LureableMonitor.Instance.FindLureCounterContext();

		// Token: 0x02002738 RID: 10040
		private class FindLureCounterContext
		{
			// Token: 0x0400AD2B RID: 44331
			public LureableMonitor.Instance inst;

			// Token: 0x0400AD2C RID: 44332
			public int cost;

			// Token: 0x0400AD2D RID: 44333
			public GameObject result;
		}
	}
}
