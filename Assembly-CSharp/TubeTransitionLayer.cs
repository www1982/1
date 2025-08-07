using System;
using UnityEngine;

// Token: 0x02000643 RID: 1603
public class TubeTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x060026A6 RID: 9894 RVA: 0x000DC30D File Offset: 0x000DA50D
	public TubeTransitionLayer(Navigator navigator)
		: base(navigator)
	{
		this.tube_traveller = navigator.GetSMI<TubeTraveller.Instance>();
		if (this.tube_traveller != null && navigator.CurrentNavType == NavType.Tube && !this.tube_traveller.inTube)
		{
			this.tube_traveller.OnTubeTransition(true);
		}
	}

	// Token: 0x060026A7 RID: 9895 RVA: 0x000DC34C File Offset: 0x000DA54C
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		this.tube_traveller.OnPathAdvanced(null);
		if (transition.start != NavType.Tube && transition.end == NavType.Tube)
		{
			int num = Grid.PosToCell(navigator);
			this.entrance = this.GetEntrance(num);
			return;
		}
		this.entrance = null;
	}

	// Token: 0x060026A8 RID: 9896 RVA: 0x000DC39C File Offset: 0x000DA59C
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		if (transition.start != NavType.Tube && transition.end == NavType.Tube && this.entrance)
		{
			this.entrance.ConsumeCharge(navigator.gameObject);
			this.entrance = null;
		}
		this.tube_traveller.OnTubeTransition(transition.end == NavType.Tube);
	}

	// Token: 0x060026A9 RID: 9897 RVA: 0x000DC3FC File Offset: 0x000DA5FC
	private TravelTubeEntrance GetEntrance(int cell)
	{
		if (!Grid.HasUsableTubeEntrance(cell, this.tube_traveller.prefabInstanceID))
		{
			return null;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject != null)
		{
			TravelTubeEntrance component = gameObject.GetComponent<TravelTubeEntrance>();
			if (component != null && component.isSpawned)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x04001694 RID: 5780
	private TubeTraveller.Instance tube_traveller;

	// Token: 0x04001695 RID: 5781
	private TravelTubeEntrance entrance;
}
