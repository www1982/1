using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000642 RID: 1602
public class DoorTransitionLayer : TransitionDriver.InterruptOverrideLayer
{
	// Token: 0x0600269F RID: 9887 RVA: 0x000DC05B File Offset: 0x000DA25B
	public DoorTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x060026A0 RID: 9888 RVA: 0x000DC070 File Offset: 0x000DA270
	private bool AreAllDoorsOpen()
	{
		foreach (INavDoor navDoor in this.doors)
		{
			if (navDoor != null && !navDoor.IsOpen())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x000DC0D0 File Offset: 0x000DA2D0
	protected override bool IsOverrideComplete()
	{
		return base.IsOverrideComplete() && this.AreAllDoorsOpen();
	}

	// Token: 0x060026A2 RID: 9890 RVA: 0x000DC0E4 File Offset: 0x000DA2E4
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		if (this.doors.Count > 0)
		{
			return;
		}
		int num = Grid.PosToCell(navigator);
		int num2 = Grid.OffsetCell(num, transition.x, transition.y);
		this.AddDoor(num2);
		if (navigator.CurrentNavType != NavType.Tube)
		{
			this.AddDoor(Grid.CellAbove(num2));
		}
		for (int i = 0; i < transition.navGridTransition.voidOffsets.Length; i++)
		{
			int num3 = Grid.OffsetCell(num, transition.navGridTransition.voidOffsets[i]);
			this.AddDoor(num3);
		}
		if (this.doors.Count == 0)
		{
			return;
		}
		if (!this.AreAllDoorsOpen())
		{
			base.BeginTransition(navigator, transition);
			transition.anim = navigator.NavGrid.GetIdleAnim(navigator.CurrentNavType);
			transition.start = this.originalTransition.start;
			transition.end = this.originalTransition.start;
		}
		foreach (INavDoor navDoor in this.doors)
		{
			navDoor.Open();
		}
	}

	// Token: 0x060026A3 RID: 9891 RVA: 0x000DC208 File Offset: 0x000DA408
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		if (this.doors.Count == 0)
		{
			return;
		}
		foreach (INavDoor navDoor in this.doors)
		{
			if (!navDoor.IsNullOrDestroyed())
			{
				navDoor.Close();
			}
		}
		this.doors.Clear();
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x000DC284 File Offset: 0x000DA484
	private void AddDoor(int cell)
	{
		INavDoor door = this.GetDoor(cell);
		if (!door.IsNullOrDestroyed() && !this.doors.Contains(door))
		{
			this.doors.Add(door);
		}
	}

	// Token: 0x060026A5 RID: 9893 RVA: 0x000DC2BC File Offset: 0x000DA4BC
	private INavDoor GetDoor(int cell)
	{
		if (!Grid.HasDoor[cell])
		{
			return null;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject != null)
		{
			INavDoor navDoor = gameObject.GetComponent<INavDoor>();
			if (navDoor == null)
			{
				navDoor = gameObject.GetSMI<INavDoor>();
			}
			if (navDoor != null && navDoor.isSpawned)
			{
				return navDoor;
			}
		}
		return null;
	}

	// Token: 0x04001693 RID: 5779
	private List<INavDoor> doors = new List<INavDoor>();
}
