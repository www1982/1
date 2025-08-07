using System;
using UnityEngine;

// Token: 0x0200046B RID: 1131
public class RobotAi : GameStateMachine<RobotAi, RobotAi.Instance>
{
	// Token: 0x060017D7 RID: 6103 RVA: 0x00084068 File Offset: 0x00082268
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleStateMachine((RobotAi.Instance smi) => new DeathMonitor.Instance(smi.master, new DeathMonitor.Def())).Enter(delegate(RobotAi.Instance smi)
		{
			if (smi.HasTag(GameTags.Dead))
			{
				smi.GoTo(this.dead);
				return;
			}
			smi.GoTo(this.alive);
		});
		this.alive.DefaultState(this.alive.normal).TagTransition(GameTags.Dead, this.dead, false).Toggle("Toggle Component Registration", delegate(RobotAi.Instance smi)
		{
			RobotAi.ToggleRegistration(smi, true);
		}, delegate(RobotAi.Instance smi)
		{
			RobotAi.ToggleRegistration(smi, false);
		});
		this.alive.normal.TagTransition(GameTags.Stored, this.alive.stored, false).Enter(delegate(RobotAi.Instance smi)
		{
			if (!smi.HasTag(GameTags.Robots.Models.FetchDrone))
			{
				smi.fallMonitor = new FallMonitor.Instance(smi.master, false, null);
				smi.fallMonitor.StartSM();
			}
		}).Exit(delegate(RobotAi.Instance smi)
		{
			if (smi.fallMonitor != null)
			{
				smi.fallMonitor.StopSM("StoredRobotAI");
			}
		});
		this.alive.stored.PlayAnim("in_storage").TagTransition(GameTags.Stored, this.alive.normal, true).ToggleBrain("stored")
			.Enter(delegate(RobotAi.Instance smi)
			{
				smi.GetComponent<Navigator>().Pause("stored");
			})
			.Exit(delegate(RobotAi.Instance smi)
			{
				smi.GetComponent<Navigator>().Unpause("unstored");
			});
		this.dead.ToggleBrain("dead").ToggleComponentIfFound<Deconstructable>(false).ToggleStateMachine((RobotAi.Instance smi) => new FallWhenDeadMonitor.Instance(smi.master))
			.Enter("RefreshUserMenu", delegate(RobotAi.Instance smi)
			{
				smi.RefreshUserMenu();
			})
			.Enter("DropStorage", delegate(RobotAi.Instance smi)
			{
				smi.GetComponent<Storage>().DropAll(false, false, default(Vector3), true, null);
			})
			.Enter("Delete", new StateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State.Callback(RobotAi.DeleteOnDeath));
	}

	// Token: 0x060017D8 RID: 6104 RVA: 0x000842B4 File Offset: 0x000824B4
	public static void DeleteOnDeath(RobotAi.Instance smi)
	{
		if (((RobotAi.Def)smi.def).DeleteOnDead)
		{
			smi.gameObject.DeleteObject();
		}
	}

	// Token: 0x060017D9 RID: 6105 RVA: 0x000842D3 File Offset: 0x000824D3
	private static void ToggleRegistration(RobotAi.Instance smi, bool register)
	{
		if (register)
		{
			Components.LiveRobotsIdentities.Add(smi);
			return;
		}
		Components.LiveRobotsIdentities.Remove(smi);
	}

	// Token: 0x04000DC2 RID: 3522
	public RobotAi.AliveStates alive;

	// Token: 0x04000DC3 RID: 3523
	public GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State dead;

	// Token: 0x02001252 RID: 4690
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040065A6 RID: 26022
		public bool DeleteOnDead;
	}

	// Token: 0x02001253 RID: 4691
	public class AliveStates : GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040065A7 RID: 26023
		public GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State normal;

		// Token: 0x040065A8 RID: 26024
		public GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.State stored;
	}

	// Token: 0x02001254 RID: 4692
	public new class Instance : GameStateMachine<RobotAi, RobotAi.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085DC RID: 34268 RVA: 0x00339A5C File Offset: 0x00337C5C
		public Instance(IStateMachineTarget master, RobotAi.Def def)
			: base(master, def)
		{
			ChoreConsumer component = base.GetComponent<ChoreConsumer>();
			component.AddUrge(Db.Get().Urges.EmoteHighPriority);
			component.AddUrge(Db.Get().Urges.EmoteIdle);
			base.Subscribe(-1988963660, new Action<object>(this.OnBeginChore));
		}

		// Token: 0x060085DD RID: 34269 RVA: 0x00339AB8 File Offset: 0x00337CB8
		private void OnBeginChore(object data)
		{
			Storage component = base.GetComponent<Storage>();
			if (component != null)
			{
				component.DropAll(false, false, default(Vector3), true, null);
			}
		}

		// Token: 0x060085DE RID: 34270 RVA: 0x00339AE8 File Offset: 0x00337CE8
		protected override void OnCleanUp()
		{
			base.Unsubscribe(-1988963660, new Action<object>(this.OnBeginChore));
			base.OnCleanUp();
		}

		// Token: 0x060085DF RID: 34271 RVA: 0x00339B07 File Offset: 0x00337D07
		public void RefreshUserMenu()
		{
			Game.Instance.userMenu.Refresh(base.master.gameObject);
		}

		// Token: 0x040065A9 RID: 26025
		public FallMonitor.Instance fallMonitor;
	}
}
