using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000709 RID: 1801
public class CritterCondo : GameStateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>
{
	// Token: 0x06002D32 RID: 11570 RVA: 0x00103470 File Offset: 0x00101670
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.PlayAnim("off").EventTransition(GameHashes.UpdateRoom, this.operational, new StateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.Transition.ConditionCallback(CritterCondo.IsOperational)).EventTransition(GameHashes.OperationalChanged, this.operational, new StateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.Transition.ConditionCallback(CritterCondo.IsOperational));
		this.operational.PlayAnim("on", KAnim.PlayMode.Loop).EventTransition(GameHashes.UpdateRoom, this.inoperational, GameStateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.Not(new StateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.Transition.ConditionCallback(CritterCondo.IsOperational))).EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.Not(new StateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.Transition.ConditionCallback(CritterCondo.IsOperational)));
	}

	// Token: 0x06002D33 RID: 11571 RVA: 0x00103522 File Offset: 0x00101722
	private static bool IsOperational(CritterCondo.Instance smi)
	{
		return smi.def.IsCritterCondoOperationalCb(smi);
	}

	// Token: 0x04001A99 RID: 6809
	public GameStateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.State inoperational;

	// Token: 0x04001A9A RID: 6810
	public GameStateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.State operational;

	// Token: 0x0200159F RID: 5535
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x0600922A RID: 37418 RVA: 0x00365D25 File Offset: 0x00363F25
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			return new List<Descriptor>();
		}

		// Token: 0x0400705D RID: 28765
		public Func<CritterCondo.Instance, bool> IsCritterCondoOperationalCb;

		// Token: 0x0400705E RID: 28766
		public Action<KBatchedAnimController, bool> UpdateForegroundVisibilitySymbols;

		// Token: 0x0400705F RID: 28767
		public StatusItem moveToStatusItem;

		// Token: 0x04007060 RID: 28768
		public StatusItem interactStatusItem;

		// Token: 0x04007061 RID: 28769
		public Tag condoTag = "CritterCondo";

		// Token: 0x04007062 RID: 28770
		public string effectId;
	}

	// Token: 0x020015A0 RID: 5536
	public new class Instance : GameStateMachine<CritterCondo, CritterCondo.Instance, IStateMachineTarget, CritterCondo.Def>.GameInstance
	{
		// Token: 0x0600922C RID: 37420 RVA: 0x00365D44 File Offset: 0x00363F44
		public Instance(IStateMachineTarget master, CritterCondo.Def def)
			: base(master, def)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
			KBatchedAnimController[] componentsInChildren = this.animController.GetComponentsInChildren<KBatchedAnimController>();
			this.foregroundController = componentsInChildren.First((KBatchedAnimController kbac) => kbac != this.animController);
		}

		// Token: 0x0600922D RID: 37421 RVA: 0x00365D89 File Offset: 0x00363F89
		public override void StartSM()
		{
			base.StartSM();
			Components.CritterCondos.Add(base.smi.GetMyWorldId(), this);
		}

		// Token: 0x0600922E RID: 37422 RVA: 0x00365DA7 File Offset: 0x00363FA7
		protected override void OnCleanUp()
		{
			Components.CritterCondos.Remove(base.smi.GetMyWorldId(), this);
		}

		// Token: 0x0600922F RID: 37423 RVA: 0x00365DBF File Offset: 0x00363FBF
		public bool IsReserved()
		{
			return base.HasTag(GameTags.Creatures.ReservedByCreature);
		}

		// Token: 0x06009230 RID: 37424 RVA: 0x00365DCC File Offset: 0x00363FCC
		public void SetReserved(bool isReserved)
		{
			if (isReserved)
			{
				base.GetComponent<KPrefabID>().SetTag(GameTags.Creatures.ReservedByCreature, true);
				return;
			}
			if (base.HasTag(GameTags.Creatures.ReservedByCreature))
			{
				base.GetComponent<KPrefabID>().RemoveTag(GameTags.Creatures.ReservedByCreature);
				return;
			}
			global::Debug.LogWarningFormat(base.smi.gameObject, "Tried to unreserve a condo that wasn't reserved", Array.Empty<object>());
		}

		// Token: 0x06009231 RID: 37425 RVA: 0x00365E26 File Offset: 0x00364026
		public int GetInteractStartCell()
		{
			return Grid.PosToCell(this);
		}

		// Token: 0x06009232 RID: 37426 RVA: 0x00365E2E File Offset: 0x0036402E
		public bool CanBeReserved()
		{
			return !this.IsReserved() && CritterCondo.IsOperational(this);
		}

		// Token: 0x06009233 RID: 37427 RVA: 0x00365E40 File Offset: 0x00364040
		public void UpdateCritterAnims(string anim_name, bool enters, bool is_large_critter)
		{
			if (enters)
			{
				this.animController.Play(anim_name, KAnim.PlayMode.Once, 1f, 0f);
			}
			if (base.def.UpdateForegroundVisibilitySymbols != null)
			{
				base.def.UpdateForegroundVisibilitySymbols(this.foregroundController, is_large_critter);
			}
		}

		// Token: 0x04007063 RID: 28771
		private KBatchedAnimController foregroundController;

		// Token: 0x04007064 RID: 28772
		private KBatchedAnimController animController;
	}
}
