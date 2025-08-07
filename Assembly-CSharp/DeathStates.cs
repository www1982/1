using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class DeathStates : GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>
{
	// Token: 0x06000411 RID: 1041 RVA: 0x0002222C File Offset: 0x0002042C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>.State state = this.loop;
		string text = CREATURES.STATUSITEMS.DEAD.NAME;
		string text2 = CREATURES.STATUSITEMS.DEAD.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main).Enter("EnableGravity", delegate(DeathStates.Instance smi)
		{
			smi.EnableGravityIfNecessary();
		}).Enter("Play Death Animations", delegate(DeathStates.Instance smi)
		{
			smi.PlayDeathAnimations();
		})
			.OnAnimQueueComplete(this.pst)
			.ScheduleGoTo((DeathStates.Instance smi) => smi.def.DIE_ANIMATION_EXPIRATION_TIME, this.pst);
		this.pst.TriggerOnEnter(GameHashes.DeathAnimComplete, null).TriggerOnEnter(GameHashes.Died, null).Enter("Butcher", delegate(DeathStates.Instance smi)
		{
			if (smi.gameObject.GetComponent<Butcherable>() != null)
			{
				smi.GetComponent<Butcherable>().OnButcherComplete();
			}
		})
			.Enter("Destroy", delegate(DeathStates.Instance smi)
			{
				smi.gameObject.AddTag(GameTags.Dead);
				smi.gameObject.DeleteObject();
			})
			.BehaviourComplete(GameTags.Creatures.Die, false);
	}

	// Token: 0x040002FE RID: 766
	private GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>.State loop;

	// Token: 0x040002FF RID: 767
	public GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>.State pst;

	// Token: 0x020010C6 RID: 4294
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400615D RID: 24925
		public float DIE_ANIMATION_EXPIRATION_TIME = 4f;
	}

	// Token: 0x020010C7 RID: 4295
	public new class Instance : GameStateMachine<DeathStates, DeathStates.Instance, IStateMachineTarget, DeathStates.Def>.GameInstance
	{
		// Token: 0x060080B5 RID: 32949 RVA: 0x0032DD0F File Offset: 0x0032BF0F
		public Instance(Chore<DeathStates.Instance> chore, DeathStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Die);
		}

		// Token: 0x060080B6 RID: 32950 RVA: 0x0032DD34 File Offset: 0x0032BF34
		public void EnableGravityIfNecessary()
		{
			if (base.HasTag(GameTags.Creatures.Flyer) && !base.HasTag(GameTags.Stored))
			{
				GameComps.Gravities.Add(base.smi.gameObject, Vector2.zero, delegate
				{
					base.smi.DisableGravity();
				});
			}
		}

		// Token: 0x060080B7 RID: 32951 RVA: 0x0032DD82 File Offset: 0x0032BF82
		public void DisableGravity()
		{
			if (GameComps.Gravities.Has(base.smi.gameObject))
			{
				GameComps.Gravities.Remove(base.smi.gameObject);
			}
		}

		// Token: 0x060080B8 RID: 32952 RVA: 0x0032DDB0 File Offset: 0x0032BFB0
		public void PlayDeathAnimations()
		{
			if (base.gameObject.HasTag(GameTags.PreventDeadAnimation))
			{
				return;
			}
			KAnimControllerBase component = base.gameObject.GetComponent<KAnimControllerBase>();
			if (component != null)
			{
				component.Play("Death", KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}
}
