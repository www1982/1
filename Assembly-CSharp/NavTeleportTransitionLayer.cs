using System;

// Token: 0x02000646 RID: 1606
public class NavTeleportTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x060026AF RID: 9903 RVA: 0x000DC604 File Offset: 0x000DA804
	public NavTeleportTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x060026B0 RID: 9904 RVA: 0x000DC610 File Offset: 0x000DA810
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		if (transition.start == NavType.Teleport)
		{
			int num = Grid.PosToCell(navigator);
			int num2;
			int num3;
			Grid.CellToXY(num, out num2, out num3);
			int num4 = navigator.NavGrid.teleportTransitions[num];
			int num5;
			int num6;
			Grid.CellToXY(navigator.NavGrid.teleportTransitions[num], out num5, out num6);
			transition.x = num5 - num2;
			transition.y = num6 - num3;
		}
	}
}
