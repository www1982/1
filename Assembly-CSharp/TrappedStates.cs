using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class TrappedStates : GameStateMachine<TrappedStates, TrappedStates.Instance, IStateMachineTarget, TrappedStates.Def>
{
	// Token: 0x060004DE RID: 1246 RVA: 0x00027C94 File Offset: 0x00025E94
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.trapped;
		GameStateMachine<TrappedStates, TrappedStates.Instance, IStateMachineTarget, TrappedStates.Def>.State root = this.root;
		string text = CREATURES.STATUSITEMS.TRAPPED.NAME;
		string text2 = CREATURES.STATUSITEMS.TRAPPED.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		root.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, main);
		this.trapped.Enter(delegate(TrappedStates.Instance smi)
		{
			Navigator component = smi.GetComponent<Navigator>();
			if (component.IsValidNavType(NavType.Floor))
			{
				component.SetCurrentNavType(NavType.Floor);
			}
		}).ToggleTag(GameTags.Creatures.Deliverable).PlayAnim(new Func<TrappedStates.Instance, string>(TrappedStates.GetTrappedAnimName), KAnim.PlayMode.Loop)
			.TagTransition(GameTags.Trapped, null, true);
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00027D48 File Offset: 0x00025F48
	public static string GetTrappedAnimName(TrappedStates.Instance smi)
	{
		string text = "trapped";
		int num = Grid.PosToCell(smi.transform.GetPosition());
		Pickupable component = smi.gameObject.GetComponent<Pickupable>();
		GameObject gameObject = ((component != null) ? component.storage.gameObject : Grid.Objects[num, 1]);
		if (gameObject != null)
		{
			if (gameObject.GetComponent<TrappedStates.ITrapStateAnimationInstructions>() != null)
			{
				string trappedAnimationName = gameObject.GetComponent<TrappedStates.ITrapStateAnimationInstructions>().GetTrappedAnimationName();
				if (trappedAnimationName != null)
				{
					return trappedAnimationName;
				}
			}
			if (gameObject.GetSMI<TrappedStates.ITrapStateAnimationInstructions>() != null)
			{
				string trappedAnimationName2 = gameObject.GetSMI<TrappedStates.ITrapStateAnimationInstructions>().GetTrappedAnimationName();
				if (trappedAnimationName2 != null)
				{
					return trappedAnimationName2;
				}
			}
		}
		Trappable component2 = smi.gameObject.GetComponent<Trappable>();
		if (component2 != null && component2.HasTag(GameTags.Creatures.Swimmer) && Grid.IsValidCell(num) && !Grid.IsLiquid(num))
		{
			text = "trapped_onLand";
		}
		return text;
	}

	// Token: 0x04000379 RID: 889
	public const string DEFAULT_TRAPPED_ANIM_NAME = "trapped";

	// Token: 0x0400037A RID: 890
	private GameStateMachine<TrappedStates, TrappedStates.Instance, IStateMachineTarget, TrappedStates.Def>.State trapped;

	// Token: 0x0200114B RID: 4427
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200114C RID: 4428
	public interface ITrapStateAnimationInstructions
	{
		// Token: 0x06008205 RID: 33285
		string GetTrappedAnimationName();
	}

	// Token: 0x0200114D RID: 4429
	public new class Instance : GameStateMachine<TrappedStates, TrappedStates.Instance, IStateMachineTarget, TrappedStates.Def>.GameInstance
	{
		// Token: 0x06008206 RID: 33286 RVA: 0x003309BA File Offset: 0x0032EBBA
		public Instance(Chore<TrappedStates.Instance> chore, TrappedStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(TrappedStates.Instance.IsTrapped, null);
		}

		// Token: 0x0400628C RID: 25228
		public static readonly Chore.Precondition IsTrapped = new Chore.Precondition
		{
			id = "IsTrapped",
			fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return context.consumerState.prefabid.HasTag(GameTags.Trapped);
			}
		};
	}
}
