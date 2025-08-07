using System;

// Token: 0x02000641 RID: 1601
public class FullPuftTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x0600269D RID: 9885 RVA: 0x000DBFE6 File Offset: 0x000DA1E6
	public FullPuftTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x0600269E RID: 9886 RVA: 0x000DBFF0 File Offset: 0x000DA1F0
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		CreatureCalorieMonitor.Instance smi = navigator.GetSMI<CreatureCalorieMonitor.Instance>();
		if (smi != null && smi.stomach.IsReadyToPoop())
		{
			string text = HashCache.Get().Get(transition.anim.HashValue) + "_full";
			if (navigator.animController.HasAnimation(text))
			{
				transition.anim = text;
			}
		}
	}
}
