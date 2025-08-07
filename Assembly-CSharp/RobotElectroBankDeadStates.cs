using System;
using UnityEngine;

// Token: 0x02000AD5 RID: 2773
public class RobotElectroBankDeadStates : GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>
{
	// Token: 0x060050AC RID: 20652 RVA: 0x001D4208 File Offset: 0x001D2408
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.powerdown;
		this.powerdown.DefaultState(this.powerdown.pre).ToggleStatusItem(Db.Get().RobotStatusItems.DeadBatteryFlydo, (RobotElectroBankDeadStates.Instance smi) => smi.gameObject, Db.Get().StatusItemCategories.Main).EventTransition(GameHashes.OnStorageChange, this.powerup.grounded, (RobotElectroBankDeadStates.Instance smi) => RobotElectroBankDeadStates.ElectrobankDelivered(smi))
			.Exit(delegate(RobotElectroBankDeadStates.Instance smi)
			{
				if (GameComps.Fallers.Has(smi.gameObject))
				{
					GameComps.Fallers.Remove(smi.gameObject);
				}
			});
		this.powerdown.pre.PlayAnim("power_down_pre").OnAnimQueueComplete(this.powerdown.fall);
		this.powerdown.fall.PlayAnim("power_down_loop", KAnim.PlayMode.Loop).Enter(delegate(RobotElectroBankDeadStates.Instance smi)
		{
			if (!GameComps.Fallers.Has(smi.gameObject))
			{
				GameComps.Fallers.Add(smi.gameObject, Vector2.zero);
			}
		}).Update(delegate(RobotElectroBankDeadStates.Instance smi, float dt)
		{
			if (!GameComps.Gravities.Has(smi.gameObject))
			{
				smi.GoTo(this.powerdown.landed);
			}
		}, UpdateRate.SIM_200ms, false)
			.EventTransition(GameHashes.Landed, this.powerdown.landed, null);
		this.powerdown.landed.PlayAnim("power_down_pst").Enter(delegate(RobotElectroBankDeadStates.Instance smi)
		{
			smi.GetComponent<LoopingSounds>().PauseSound(GlobalAssets.GetSound("Flydo_flying_LP", false), true);
		}).OnAnimQueueComplete(this.powerdown.dead);
		this.powerdown.dead.PlayAnim("dead_battery").EventTransition(GameHashes.OnStorageChange, this.powerup.grounded, (RobotElectroBankDeadStates.Instance smi) => RobotElectroBankDeadStates.ElectrobankDelivered(smi));
		this.powerup.Exit(delegate(RobotElectroBankDeadStates.Instance smi)
		{
			smi.GetComponent<LoopingSounds>().PauseSound(GlobalAssets.GetSound("Flydo_flying_LP", false), false);
			smi.Get<Brain>().Resume("power up");
		});
		this.powerup.grounded.PlayAnim("battery_change_dead").OnAnimQueueComplete(this.powerup.takeoff);
		this.powerup.takeoff.PlayAnim("power_up").OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Robots.Behaviours.NoElectroBank, false);
	}

	// Token: 0x060050AD RID: 20653 RVA: 0x001D4470 File Offset: 0x001D2670
	private static bool ElectrobankDelivered(RobotElectroBankDeadStates.Instance smi)
	{
		foreach (Storage storage in smi.gameObject.GetComponents<Storage>())
		{
			if (storage.storageID == GameTags.ChargedPortableBattery)
			{
				return storage.Has(GameTags.ChargedPortableBattery);
			}
		}
		return false;
	}

	// Token: 0x0400363C RID: 13884
	public RobotElectroBankDeadStates.PowerDown powerdown;

	// Token: 0x0400363D RID: 13885
	public RobotElectroBankDeadStates.PowerUp powerup;

	// Token: 0x0400363E RID: 13886
	public GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State behaviourcomplete;

	// Token: 0x02001BAF RID: 7087
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001BB0 RID: 7088
	public class PowerDown : GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State
	{
		// Token: 0x040083B6 RID: 33718
		public GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State pre;

		// Token: 0x040083B7 RID: 33719
		public GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State fall;

		// Token: 0x040083B8 RID: 33720
		public GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State landed;

		// Token: 0x040083B9 RID: 33721
		public GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State dead;
	}

	// Token: 0x02001BB1 RID: 7089
	public class PowerUp : GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State
	{
		// Token: 0x040083BA RID: 33722
		public GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State grounded;

		// Token: 0x040083BB RID: 33723
		public GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.State takeoff;
	}

	// Token: 0x02001BB2 RID: 7090
	public new class Instance : GameStateMachine<RobotElectroBankDeadStates, RobotElectroBankDeadStates.Instance, IStateMachineTarget, RobotElectroBankDeadStates.Def>.GameInstance
	{
		// Token: 0x0600A84B RID: 43083 RVA: 0x003B400C File Offset: 0x003B220C
		public Instance(Chore<RobotElectroBankDeadStates.Instance> chore, RobotElectroBankDeadStates.Def def)
			: base(chore, def)
		{
			chore.choreType.interruptPriority = Db.Get().ChoreTypes.Die.interruptPriority;
			chore.masterPriority.priority_class = PriorityScreen.PriorityClass.compulsory;
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Robots.Behaviours.NoElectroBank);
		}
	}
}
