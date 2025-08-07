using System;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x02000860 RID: 2144
public class DiggerMonitor : GameStateMachine<DiggerMonitor, DiggerMonitor.Instance, IStateMachineTarget, DiggerMonitor.Def>
{
	// Token: 0x06003AE3 RID: 15075 RVA: 0x00147350 File Offset: 0x00145550
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.loop.EventTransition(GameHashes.BeginMeteorBombardment, (DiggerMonitor.Instance smi) => Game.Instance, this.dig, (DiggerMonitor.Instance smi) => smi.CanTunnel());
		this.dig.ToggleBehaviour(GameTags.Creatures.Tunnel, (DiggerMonitor.Instance smi) => true, null).GoTo(this.loop);
	}

	// Token: 0x04002415 RID: 9237
	public GameStateMachine<DiggerMonitor, DiggerMonitor.Instance, IStateMachineTarget, DiggerMonitor.Def>.State loop;

	// Token: 0x04002416 RID: 9238
	public GameStateMachine<DiggerMonitor, DiggerMonitor.Instance, IStateMachineTarget, DiggerMonitor.Def>.State dig;

	// Token: 0x020017F2 RID: 6130
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06009AE1 RID: 39649 RVA: 0x0038B735 File Offset: 0x00389935
		// (set) Token: 0x06009AE2 RID: 39650 RVA: 0x0038B73D File Offset: 0x0038993D
		public int depthToDig { get; set; }
	}

	// Token: 0x020017F3 RID: 6131
	public new class Instance : GameStateMachine<DiggerMonitor, DiggerMonitor.Instance, IStateMachineTarget, DiggerMonitor.Def>.GameInstance
	{
		// Token: 0x06009AE4 RID: 39652 RVA: 0x0038B750 File Offset: 0x00389950
		public Instance(IStateMachineTarget master, DiggerMonitor.Def def)
			: base(master, def)
		{
			global::World instance = global::World.Instance;
			instance.OnSolidChanged = (Action<int>)Delegate.Combine(instance.OnSolidChanged, new Action<int>(this.OnSolidChanged));
			this.OnDestinationReachedDelegate = new Action<object>(this.OnDestinationReached);
			master.Subscribe(387220196, this.OnDestinationReachedDelegate);
			master.Subscribe(-766531887, this.OnDestinationReachedDelegate);
		}

		// Token: 0x06009AE5 RID: 39653 RVA: 0x0038B7C8 File Offset: 0x003899C8
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			global::World instance = global::World.Instance;
			instance.OnSolidChanged = (Action<int>)Delegate.Remove(instance.OnSolidChanged, new Action<int>(this.OnSolidChanged));
			base.master.Unsubscribe(387220196, this.OnDestinationReachedDelegate);
			base.master.Unsubscribe(-766531887, this.OnDestinationReachedDelegate);
		}

		// Token: 0x06009AE6 RID: 39654 RVA: 0x0038B82D File Offset: 0x00389A2D
		private void OnDestinationReached(object data)
		{
			this.CheckInSolid();
		}

		// Token: 0x06009AE7 RID: 39655 RVA: 0x0038B838 File Offset: 0x00389A38
		private void CheckInSolid()
		{
			Navigator component = base.gameObject.GetComponent<Navigator>();
			if (component == null)
			{
				return;
			}
			int num = Grid.PosToCell(base.gameObject);
			if (component.CurrentNavType != NavType.Solid && Grid.IsSolidCell(num))
			{
				component.SetCurrentNavType(NavType.Solid);
				return;
			}
			if (component.CurrentNavType == NavType.Solid && !Grid.IsSolidCell(num))
			{
				component.SetCurrentNavType(NavType.Floor);
				base.gameObject.AddTag(GameTags.Creatures.Falling);
			}
		}

		// Token: 0x06009AE8 RID: 39656 RVA: 0x0038B8AB File Offset: 0x00389AAB
		private void OnSolidChanged(int cell)
		{
			this.CheckInSolid();
		}

		// Token: 0x06009AE9 RID: 39657 RVA: 0x0038B8B4 File Offset: 0x00389AB4
		public bool CanTunnel()
		{
			int num = Grid.PosToCell(this);
			if (global::World.Instance.zoneRenderData.GetSubWorldZoneType(num) == SubWorld.ZoneType.Space)
			{
				int num2 = num;
				while (Grid.IsValidCell(num2) && !Grid.Solid[num2])
				{
					num2 = Grid.CellAbove(num2);
				}
				if (!Grid.IsValidCell(num2))
				{
					return this.FoundValidDigCell();
				}
			}
			return false;
		}

		// Token: 0x06009AEA RID: 39658 RVA: 0x0038B90C File Offset: 0x00389B0C
		private bool FoundValidDigCell()
		{
			int num = base.smi.def.depthToDig;
			int num2 = Grid.PosToCell(base.smi.master.gameObject);
			this.lastDigCell = num2;
			int num3 = Grid.CellBelow(num2);
			while (this.IsValidDigCell(num3, null) && num > 0)
			{
				num3 = Grid.CellBelow(num3);
				num--;
			}
			if (num > 0)
			{
				num3 = GameUtil.FloodFillFind<object>(new Func<int, object, bool>(this.IsValidDigCell), null, num2, base.smi.def.depthToDig, false, true);
			}
			this.lastDigCell = num3;
			return this.lastDigCell != -1;
		}

		// Token: 0x06009AEB RID: 39659 RVA: 0x0038B9A8 File Offset: 0x00389BA8
		private bool IsValidDigCell(int cell, object arg = null)
		{
			if (Grid.IsValidCell(cell) && Grid.Solid[cell])
			{
				if (!Grid.HasDoor[cell] && !Grid.Foundation[cell])
				{
					ushort num = Grid.ElementIdx[cell];
					Element element = ElementLoader.elements[(int)num];
					return Grid.Element[cell].hardness < 150 && !element.HasTag(GameTags.RefinedMetal);
				}
				GameObject gameObject = Grid.Objects[cell, 1];
				if (gameObject != null)
				{
					PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
					return Grid.Element[cell].hardness < 150 && !component.Element.HasTag(GameTags.RefinedMetal);
				}
			}
			return false;
		}

		// Token: 0x04007767 RID: 30567
		[Serialize]
		public int lastDigCell = -1;

		// Token: 0x04007768 RID: 30568
		private Action<object> OnDestinationReachedDelegate;
	}
}
