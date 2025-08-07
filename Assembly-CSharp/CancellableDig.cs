using System;

// Token: 0x0200057D RID: 1405
[SkipSaveFileSerialization]
public class CancellableDig : Cancellable
{
	// Token: 0x06001FCB RID: 8139 RVA: 0x000B6B10 File Offset: 0x000B4D10
	protected override void OnCancel(object data)
	{
		if (data != null && (bool)data)
		{
			this.OnAnimationDone("ScaleDown");
			return;
		}
		EasingAnimations componentInChildren = base.GetComponentInChildren<EasingAnimations>();
		int num = Grid.PosToCell(this);
		if (componentInChildren.IsPlaying && Grid.Element[num].hardness == 255)
		{
			EasingAnimations easingAnimations = componentInChildren;
			easingAnimations.OnAnimationDone = (Action<string>)Delegate.Combine(easingAnimations.OnAnimationDone, new Action<string>(this.DoCancelAnim));
			return;
		}
		EasingAnimations easingAnimations2 = componentInChildren;
		easingAnimations2.OnAnimationDone = (Action<string>)Delegate.Combine(easingAnimations2.OnAnimationDone, new Action<string>(this.OnAnimationDone));
		componentInChildren.PlayAnimation("ScaleDown", 0.1f);
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x000B6BB8 File Offset: 0x000B4DB8
	private void DoCancelAnim(string animName)
	{
		EasingAnimations componentInChildren = base.GetComponentInChildren<EasingAnimations>();
		componentInChildren.OnAnimationDone = (Action<string>)Delegate.Remove(componentInChildren.OnAnimationDone, new Action<string>(this.DoCancelAnim));
		componentInChildren.OnAnimationDone = (Action<string>)Delegate.Combine(componentInChildren.OnAnimationDone, new Action<string>(this.OnAnimationDone));
		componentInChildren.PlayAnimation("ScaleDown", 0.1f);
	}

	// Token: 0x06001FCD RID: 8141 RVA: 0x000B6C1E File Offset: 0x000B4E1E
	private void OnAnimationDone(string animationName)
	{
		if (animationName != "ScaleDown")
		{
			return;
		}
		EasingAnimations componentInChildren = base.GetComponentInChildren<EasingAnimations>();
		componentInChildren.OnAnimationDone = (Action<string>)Delegate.Remove(componentInChildren.OnAnimationDone, new Action<string>(this.OnAnimationDone));
		this.DeleteObject();
	}
}
