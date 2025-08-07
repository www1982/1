using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000594 RID: 1428
public class CritterCondoInteractMontior : GameStateMachine<CritterCondoInteractMontior, CritterCondoInteractMontior.Instance, IStateMachineTarget, CritterCondoInteractMontior.Def>
{
	// Token: 0x0600209F RID: 8351 RVA: 0x000BC574 File Offset: 0x000BA774
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.lookingForCondo;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.ParamTransition<float>(this.remainingSecondsForEffect, this.satisfied, (CritterCondoInteractMontior.Instance smi, float val) => val > 0f);
		this.lookingForCondo.PreBrainUpdate(new Action<CritterCondoInteractMontior.Instance>(CritterCondoInteractMontior.FindCondoTarget)).ToggleBehaviour(GameTags.Creatures.Behaviour_InteractWithCritterCondo, (CritterCondoInteractMontior.Instance smi) => !smi.targetCondo.IsNullOrStopped() && !smi.targetCondo.IsReserved(), delegate(CritterCondoInteractMontior.Instance smi)
		{
			smi.GoTo(this.satisfied);
		});
		this.satisfied.Enter(delegate(CritterCondoInteractMontior.Instance smi)
		{
			this.remainingSecondsForEffect.Set(600f, smi, false);
		}).ScheduleGoTo((CritterCondoInteractMontior.Instance smi) => this.remainingSecondsForEffect.Get(smi), this.lookingForCondo);
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x000BC644 File Offset: 0x000BA844
	private static void FindCondoTarget(CritterCondoInteractMontior.Instance smi)
	{
		using (ListPool<CritterCondo.Instance, CritterCondoInteractMontior>.PooledList pooledList = PoolsFor<CritterCondoInteractMontior>.AllocateList<CritterCondo.Instance>())
		{
			if (!smi.def.requireCavity)
			{
				Vector3 position = smi.gameObject.transform.GetPosition();
				using (List<CritterCondo.Instance>.Enumerator enumerator = Components.CritterCondos.GetItems(smi.GetMyWorldId()).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						CritterCondo.Instance instance = enumerator.Current;
						if (!instance.IsNullOrDestroyed() && !(instance.def.condoTag != smi.def.condoPrefabTag) && (instance.transform.GetPosition() - position).sqrMagnitude <= 256f && instance.CanBeReserved())
						{
							pooledList.Add(instance);
						}
					}
					goto IL_0152;
				}
			}
			int num = Grid.PosToCell(smi.gameObject);
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			if (cavityForCell != null && cavityForCell.room != null)
			{
				foreach (KPrefabID kprefabID in cavityForCell.buildings)
				{
					if (!kprefabID.IsNullOrDestroyed())
					{
						CritterCondo.Instance smi2 = kprefabID.GetSMI<CritterCondo.Instance>();
						if (smi2 != null && kprefabID.HasTag(smi.def.condoPrefabTag) && smi2.CanBeReserved())
						{
							pooledList.Add(smi2);
						}
					}
				}
			}
			IL_0152:
			Navigator component = smi.GetComponent<Navigator>();
			int num2 = -1;
			foreach (CritterCondo.Instance instance2 in pooledList)
			{
				int interactStartCell = instance2.GetInteractStartCell();
				int navigationCost = component.GetNavigationCost(interactStartCell);
				if (navigationCost != -1 && (navigationCost < num2 || num2 == -1))
				{
					num2 = navigationCost;
					smi.targetCondo = instance2;
				}
			}
		}
	}

	// Token: 0x040012FB RID: 4859
	public GameStateMachine<CritterCondoInteractMontior, CritterCondoInteractMontior.Instance, IStateMachineTarget, CritterCondoInteractMontior.Def>.State lookingForCondo;

	// Token: 0x040012FC RID: 4860
	public GameStateMachine<CritterCondoInteractMontior, CritterCondoInteractMontior.Instance, IStateMachineTarget, CritterCondoInteractMontior.Def>.State satisfied;

	// Token: 0x040012FD RID: 4861
	private StateMachine<CritterCondoInteractMontior, CritterCondoInteractMontior.Instance, IStateMachineTarget, CritterCondoInteractMontior.Def>.FloatParameter remainingSecondsForEffect;

	// Token: 0x020013FC RID: 5116
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006B4F RID: 27471
		public bool requireCavity = true;

		// Token: 0x04006B50 RID: 27472
		public Tag condoPrefabTag = "CritterCondo";
	}

	// Token: 0x020013FD RID: 5117
	public new class Instance : GameStateMachine<CritterCondoInteractMontior, CritterCondoInteractMontior.Instance, IStateMachineTarget, CritterCondoInteractMontior.Def>.GameInstance
	{
		// Token: 0x06008C25 RID: 35877 RVA: 0x0035528E File Offset: 0x0035348E
		public Instance(IStateMachineTarget master, CritterCondoInteractMontior.Def def)
			: base(master, def)
		{
		}

		// Token: 0x04006B51 RID: 27473
		public CritterCondo.Instance targetCondo;
	}
}
