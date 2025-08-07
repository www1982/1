using System;
using UnityEngine;

// Token: 0x02000592 RID: 1426
public class CreatureFallMonitor : GameStateMachine<CreatureFallMonitor, CreatureFallMonitor.Instance, IStateMachineTarget, CreatureFallMonitor.Def>
{
	// Token: 0x06002099 RID: 8345 RVA: 0x000BC440 File Offset: 0x000BA640
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.grounded;
		this.grounded.ToggleBehaviour(GameTags.Creatures.Falling, (CreatureFallMonitor.Instance smi) => smi.ShouldFall(), null);
	}

	// Token: 0x040012F6 RID: 4854
	public static float FLOOR_DISTANCE = -0.065f;

	// Token: 0x040012F7 RID: 4855
	public GameStateMachine<CreatureFallMonitor, CreatureFallMonitor.Instance, IStateMachineTarget, CreatureFallMonitor.Def>.State grounded;

	// Token: 0x040012F8 RID: 4856
	public GameStateMachine<CreatureFallMonitor, CreatureFallMonitor.Instance, IStateMachineTarget, CreatureFallMonitor.Def>.State falling;

	// Token: 0x020013F6 RID: 5110
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006B43 RID: 27459
		public bool canSwim;

		// Token: 0x04006B44 RID: 27460
		public bool checkHead = true;
	}

	// Token: 0x020013F7 RID: 5111
	public new class Instance : GameStateMachine<CreatureFallMonitor, CreatureFallMonitor.Instance, IStateMachineTarget, CreatureFallMonitor.Def>.GameInstance
	{
		// Token: 0x06008C1B RID: 35867 RVA: 0x00354FBC File Offset: 0x003531BC
		public Instance(IStateMachineTarget master, CreatureFallMonitor.Def def)
			: base(master, def)
		{
			this.largeCritter = this.collider.size.y > 1f;
		}

		// Token: 0x06008C1C RID: 35868 RVA: 0x00354FF0 File Offset: 0x003531F0
		public void SnapToGround()
		{
			Vector3 position = base.smi.transform.GetPosition();
			Vector3 vector = Grid.CellToPosCBC(Grid.PosToCell(position), Grid.SceneLayer.Creatures);
			vector.x = position.x;
			base.smi.transform.SetPosition(vector);
			if (this.navigator.IsValidNavType(NavType.Floor))
			{
				this.navigator.SetCurrentNavType(NavType.Floor);
				return;
			}
			if (this.navigator.IsValidNavType(NavType.Hover))
			{
				this.navigator.SetCurrentNavType(NavType.Hover);
			}
		}

		// Token: 0x06008C1D RID: 35869 RVA: 0x00355070 File Offset: 0x00353270
		public bool ShouldFall()
		{
			if (this.kprefabId.HasTag(GameTags.Stored))
			{
				return false;
			}
			Vector3 position = base.smi.transform.GetPosition();
			int num = Grid.PosToCell(position);
			if (Grid.IsValidCell(num) && Grid.Solid[num])
			{
				return false;
			}
			if (this.navigator.IsMoving())
			{
				return false;
			}
			if (this.CanSwimAtCurrentLocation())
			{
				return false;
			}
			if (this.navigator.CurrentNavType != NavType.Swim)
			{
				if (this.navigator.NavGrid.NavTable.IsValid(num, this.navigator.CurrentNavType))
				{
					return false;
				}
				if (this.navigator.CurrentNavType == NavType.Ceiling)
				{
					return true;
				}
				if (this.navigator.CurrentNavType == NavType.LeftWall)
				{
					return true;
				}
				if (this.navigator.CurrentNavType == NavType.RightWall)
				{
					return true;
				}
			}
			Vector3 vector = position;
			vector.y += CreatureFallMonitor.FLOOR_DISTANCE;
			int num2 = Grid.PosToCell(vector);
			return !Grid.IsValidCell(num2) || !Grid.Solid[num2];
		}

		// Token: 0x06008C1E RID: 35870 RVA: 0x00355178 File Offset: 0x00353378
		public bool CanSwimAtCurrentLocation()
		{
			if (base.def.canSwim)
			{
				Vector3 position = base.transform.GetPosition();
				float num = 1f;
				if (!base.def.checkHead)
				{
					num = 0.5f;
				}
				else if (this.largeCritter)
				{
					num = 0.25f;
				}
				position.y += this.collider.size.y * num;
				if (Grid.IsSubstantialLiquid(Grid.PosToCell(position), 0.35f))
				{
					if (!GameComps.Gravities.Has(base.gameObject))
					{
						return true;
					}
					if (GameComps.Gravities.GetData(GameComps.Gravities.GetHandle(base.gameObject)).velocity.magnitude < 2f)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04006B45 RID: 27461
		public string anim = "fall";

		// Token: 0x04006B46 RID: 27462
		[MyCmpReq]
		private KPrefabID kprefabId;

		// Token: 0x04006B47 RID: 27463
		[MyCmpReq]
		private Navigator navigator;

		// Token: 0x04006B48 RID: 27464
		[MyCmpReq]
		private KBoxCollider2D collider;

		// Token: 0x04006B49 RID: 27465
		private bool largeCritter;
	}
}
