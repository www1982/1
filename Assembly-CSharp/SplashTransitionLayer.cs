using System;
using UnityEngine;

// Token: 0x02000640 RID: 1600
public class SplashTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x06002698 RID: 9880 RVA: 0x000DBEF3 File Offset: 0x000DA0F3
	public SplashTransitionLayer(Navigator navigator)
		: base(navigator)
	{
		this.lastSplashTime = Time.time;
	}

	// Token: 0x06002699 RID: 9881 RVA: 0x000DBF08 File Offset: 0x000DA108
	private void RefreshSplashes(Navigator navigator, Navigator.ActiveTransition transition)
	{
		if (navigator == null)
		{
			return;
		}
		if (transition.end == NavType.Tube)
		{
			return;
		}
		Vector3 position = navigator.transform.GetPosition();
		if (this.lastSplashTime + 1f < Time.time && Grid.Element[Grid.PosToCell(position)].IsLiquid)
		{
			this.lastSplashTime = Time.time;
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("splash_step_kanim", position + new Vector3(0f, 0.75f, -0.1f), null, false, Grid.SceneLayer.Front, false);
			kbatchedAnimController.Play("fx1", KAnim.PlayMode.Once, 1f, 0f);
			kbatchedAnimController.destroyOnAnimComplete = true;
		}
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x000DBFB0 File Offset: 0x000DA1B0
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		this.RefreshSplashes(navigator, transition);
	}

	// Token: 0x0600269B RID: 9883 RVA: 0x000DBFC2 File Offset: 0x000DA1C2
	public override void UpdateTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.UpdateTransition(navigator, transition);
		this.RefreshSplashes(navigator, transition);
	}

	// Token: 0x0600269C RID: 9884 RVA: 0x000DBFD4 File Offset: 0x000DA1D4
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		this.RefreshSplashes(navigator, transition);
	}

	// Token: 0x04001691 RID: 5777
	private float lastSplashTime;

	// Token: 0x04001692 RID: 5778
	private const float SPLASH_INTERVAL = 1f;
}
