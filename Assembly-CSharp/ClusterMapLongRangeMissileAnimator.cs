using System;
using UnityEngine;

// Token: 0x02000B78 RID: 2936
public class ClusterMapLongRangeMissileAnimator : GameStateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer>
{
	// Token: 0x060057CC RID: 22476 RVA: 0x001FCAFC File Offset: 0x001FACFC
	public override void InitializeStates(out StateMachine.BaseState defaultState)
	{
		defaultState = this.moving;
		this.root.OnTargetLost(this.entityTarget, null).Target(this.entityTarget).TagTransition(GameTags.LongRangeMissileMoving, this.moving, false)
			.TagTransition(GameTags.LongRangeMissileIdle, this.idle, false)
			.TagTransition(GameTags.LongRangeMissileExploding, this.exploding, false);
		this.moving.Enter(delegate(ClusterMapLongRangeMissileAnimator.StatesInstance smi)
		{
			smi.PlayVisAnim("inflight_loop", KAnim.PlayMode.Loop);
		});
		this.idle.Enter(delegate(ClusterMapLongRangeMissileAnimator.StatesInstance smi)
		{
			smi.PlayVisAnim("idle_loop", KAnim.PlayMode.Loop);
		});
		this.exploding.DefaultState(this.exploding.pre);
		this.exploding.pre.ScheduleGoTo(10f, this.exploding.animating).EventTransition(GameHashes.ClusterMapTravelAnimatorMoveComplete, this.exploding.animating, null);
		this.exploding.animating.Enter(delegate(ClusterMapLongRangeMissileAnimator.StatesInstance smi)
		{
			smi.PlayVisAnim("explode", KAnim.PlayMode.Once);
			smi.SubscribeOnVisAnimComplete(delegate(object _)
			{
				smi.GoTo(this.exploding.post);
			});
		});
		this.exploding.post.Enter(delegate(ClusterMapLongRangeMissileAnimator.StatesInstance smi)
		{
			if (smi.entity != null)
			{
				smi.entity.Trigger(-1311384361, null);
			}
			smi.GoTo(null);
		});
	}

	// Token: 0x04003A91 RID: 14993
	public StateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer, object>.TargetParameter entityTarget;

	// Token: 0x04003A92 RID: 14994
	public GameStateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer, object>.State moving;

	// Token: 0x04003A93 RID: 14995
	public GameStateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer, object>.State idle;

	// Token: 0x04003A94 RID: 14996
	public ClusterMapLongRangeMissileAnimator.ExplodingStates exploding;

	// Token: 0x02001CAD RID: 7341
	public class ExplodingStates : GameStateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer, object>.State
	{
		// Token: 0x04008704 RID: 34564
		public GameStateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer, object>.State pre;

		// Token: 0x04008705 RID: 34565
		public GameStateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer, object>.State animating;

		// Token: 0x04008706 RID: 34566
		public GameStateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer, object>.State post;
	}

	// Token: 0x02001CAE RID: 7342
	public class StatesInstance : GameStateMachine<ClusterMapLongRangeMissileAnimator, ClusterMapLongRangeMissileAnimator.StatesInstance, ClusterMapVisualizer, object>.GameInstance
	{
		// Token: 0x0600ABF4 RID: 44020 RVA: 0x003C0AD4 File Offset: 0x003BECD4
		public StatesInstance(ClusterMapVisualizer master, ClusterGridEntity entity)
			: base(master)
		{
			this.entity = entity;
			base.sm.entityTarget.Set(entity, this);
		}

		// Token: 0x0600ABF5 RID: 44021 RVA: 0x003C0AFD File Offset: 0x003BECFD
		public void PlayVisAnim(string animName, KAnim.PlayMode playMode)
		{
			base.GetComponent<ClusterMapVisualizer>().PlayAnim(animName, playMode);
		}

		// Token: 0x0600ABF6 RID: 44022 RVA: 0x003C0B0C File Offset: 0x003BED0C
		public void ToggleVisAnim(bool on)
		{
			ClusterMapVisualizer component = base.GetComponent<ClusterMapVisualizer>();
			if (!on)
			{
				component.GetFirstAnimController().Play("grounded", KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x0600ABF7 RID: 44023 RVA: 0x003C0B44 File Offset: 0x003BED44
		public void SubscribeOnVisAnimComplete(Action<object> action)
		{
			ClusterMapVisualizer component = base.GetComponent<ClusterMapVisualizer>();
			this.UnsubscribeOnVisAnimComplete();
			this.animCompleteSubscriber = component.GetFirstAnimController().gameObject;
			this.animCompleteHandle = this.animCompleteSubscriber.Subscribe(-1061186183, action);
		}

		// Token: 0x0600ABF8 RID: 44024 RVA: 0x003C0B86 File Offset: 0x003BED86
		public void UnsubscribeOnVisAnimComplete()
		{
			if (this.animCompleteHandle != -1)
			{
				DebugUtil.DevAssert(this.animCompleteSubscriber != null, "ClustermapBallisticAnimator animCompleteSubscriber GameObject is null. Whatever the previous gameObject in this variable was, it may not have unsubscribed from an event properly", null);
				this.animCompleteSubscriber.Unsubscribe(this.animCompleteHandle);
				this.animCompleteHandle = -1;
			}
		}

		// Token: 0x0600ABF9 RID: 44025 RVA: 0x003C0BC0 File Offset: 0x003BEDC0
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			this.UnsubscribeOnVisAnimComplete();
		}

		// Token: 0x04008707 RID: 34567
		public ClusterGridEntity entity;

		// Token: 0x04008708 RID: 34568
		private int animCompleteHandle = -1;

		// Token: 0x04008709 RID: 34569
		private GameObject animCompleteSubscriber;
	}
}
