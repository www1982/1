using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class CritterCondoStates : GameStateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>
{
	// Token: 0x060003F7 RID: 1015 RVA: 0x0002162C File Offset: 0x0001F82C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.goingToCondo;
		this.root.Enter(new StateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State.Callback(CritterCondoStates.ReserveCondo)).Exit(new StateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State.Callback(CritterCondoStates.UnreserveCondo));
		this.goingToCondo.MoveTo(new Func<CritterCondoStates.Instance, int>(CritterCondoStates.GetCondoInteractCell), this.interact, null, false).ToggleMainStatusItem((CritterCondoStates.Instance smi) => CritterCondoStates.GetTargetCondo(smi).def.moveToStatusItem, null).OnTargetLost(this.targetCondo, null);
		this.interact.DefaultState(this.interact.pre).OnTargetLost(this.targetCondo, null).Enter(delegate(CritterCondoStates.Instance smi)
		{
			this.SetFacing(smi);
			smi.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.BuildingUse);
		})
			.Exit(delegate(CritterCondoStates.Instance smi)
			{
				smi.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Creatures);
			})
			.ToggleMainStatusItem((CritterCondoStates.Instance smi) => CritterCondoStates.GetTargetCondo(smi).def.interactStatusItem, null);
		this.interact.pre.PlayAnim("cc_working_pre").Enter(delegate(CritterCondoStates.Instance smi)
		{
			CritterCondoStates.PlayCondoBuildingAnim(smi, "cc_working_pre");
		}).OnAnimQueueComplete(this.interact.loop);
		this.interact.loop.PlayAnim("cc_working").Enter(delegate(CritterCondoStates.Instance smi)
		{
			CritterCondoStates.PlayCondoBuildingAnim(smi, smi.def.working_anim);
		}).OnAnimQueueComplete(this.interact.pst);
		this.interact.pst.PlayAnim("cc_working_pst").Enter(delegate(CritterCondoStates.Instance smi)
		{
			CritterCondoStates.PlayCondoBuildingAnim(smi, "cc_working_pst");
		}).OnAnimQueueComplete(this.behaviourComplete);
		this.behaviourComplete.BehaviourComplete(GameTags.Creatures.Behaviour_InteractWithCritterCondo, false).Exit(new StateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State.Callback(CritterCondoStates.ApplyEffects));
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00021834 File Offset: 0x0001FA34
	private void SetFacing(CritterCondoStates.Instance smi)
	{
		bool isRotated = CritterCondoStates.GetTargetCondo(smi).Get<Rotatable>().IsRotated;
		smi.Get<Facing>().SetFacing(isRotated);
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00021860 File Offset: 0x0001FA60
	private static CritterCondo.Instance GetTargetCondo(CritterCondoStates.Instance smi)
	{
		GameObject gameObject = smi.sm.targetCondo.Get(smi);
		CritterCondo.Instance instance = ((gameObject != null) ? gameObject.GetSMI<CritterCondo.Instance>() : null);
		if (instance.IsNullOrStopped())
		{
			return null;
		}
		return instance;
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x000218A0 File Offset: 0x0001FAA0
	private static void ReserveCondo(CritterCondoStates.Instance smi)
	{
		CritterCondo.Instance instance = smi.GetSMI<CritterCondoInteractMontior.Instance>().targetCondo;
		if (instance == null)
		{
			return;
		}
		smi.sm.targetCondo.Set(instance.gameObject, smi, false);
		instance.SetReserved(true);
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x000218E0 File Offset: 0x0001FAE0
	private static void UnreserveCondo(CritterCondoStates.Instance smi)
	{
		CritterCondo.Instance instance = CritterCondoStates.GetTargetCondo(smi);
		if (instance == null)
		{
			return;
		}
		instance.GetComponent<KBatchedAnimController>().Play("on", KAnim.PlayMode.Loop, 1f, 0f);
		smi.sm.targetCondo.Set(null, smi);
		instance.SetReserved(false);
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00021934 File Offset: 0x0001FB34
	private static int GetCondoInteractCell(CritterCondoStates.Instance smi)
	{
		CritterCondo.Instance instance = CritterCondoStates.GetTargetCondo(smi);
		if (instance == null)
		{
			return Grid.InvalidCell;
		}
		int num = instance.GetInteractStartCell();
		if (smi.isLargeCritter)
		{
			bool isRotated = instance.Get<Rotatable>().IsRotated;
			Vector2I vector2I = Grid.PosToXY(smi.gameObject.transform.position);
			Vector2I vector2I2 = Grid.CellToXY(num);
			if (vector2I.x > vector2I2.x && !isRotated)
			{
				num = Grid.CellLeft(num);
			}
			else if (vector2I.x < vector2I2.x && isRotated)
			{
				num = Grid.CellRight(num);
			}
		}
		return num;
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x000219C0 File Offset: 0x0001FBC0
	private static void ApplyEffects(CritterCondoStates.Instance smi)
	{
		smi.Get<Effects>().Add(CritterCondoStates.GetTargetCondo(smi).def.effectId, true);
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x000219E0 File Offset: 0x0001FBE0
	private static void PlayCondoBuildingAnim(CritterCondoStates.Instance smi, string anim_name)
	{
		CritterCondo.Instance smi2 = smi.sm.targetCondo.GetSMI<CritterCondo.Instance>(smi);
		if (smi2 != null)
		{
			smi2.UpdateCritterAnims(anim_name, smi.def.entersBuilding, smi.isLargeCritter);
		}
	}

	// Token: 0x040002F2 RID: 754
	public GameStateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State goingToCondo;

	// Token: 0x040002F3 RID: 755
	public CritterCondoStates.InteractState interact;

	// Token: 0x040002F4 RID: 756
	public GameStateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State behaviourComplete;

	// Token: 0x040002F5 RID: 757
	public StateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.TargetParameter targetCondo;

	// Token: 0x020010BC RID: 4284
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006138 RID: 24888
		public bool entersBuilding = true;

		// Token: 0x04006139 RID: 24889
		public string working_anim = "cc_working";
	}

	// Token: 0x020010BD RID: 4285
	public new class Instance : GameStateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.GameInstance
	{
		// Token: 0x0600809E RID: 32926 RVA: 0x0032DAD8 File Offset: 0x0032BCD8
		public Instance(Chore<CritterCondoStates.Instance> chore, CritterCondoStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Behaviour_InteractWithCritterCondo);
			this.isLargeCritter = base.GetComponent<KPrefabID>().HasTag(GameTags.LargeCreature);
		}

		// Token: 0x0400613A RID: 24890
		public bool isLargeCritter;
	}

	// Token: 0x020010BE RID: 4286
	public class InteractState : GameStateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State
	{
		// Token: 0x0400613B RID: 24891
		public GameStateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State pre;

		// Token: 0x0400613C RID: 24892
		public GameStateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State loop;

		// Token: 0x0400613D RID: 24893
		public GameStateMachine<CritterCondoStates, CritterCondoStates.Instance, IStateMachineTarget, CritterCondoStates.Def>.State pst;
	}
}
