using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200040A RID: 1034
public class SpecialCargoBayCluster : GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>
{
	// Token: 0x0600152D RID: 5421 RVA: 0x00078924 File Offset: 0x00076B24
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.close;
		this.close.DefaultState(this.close.idle);
		this.close.closing.Target(this.Door).PlayAnim("close").OnAnimQueueComplete(this.close.idle)
			.Target(this.masterTarget);
		this.close.idle.Target(this.Door).PlayAnim("close_idle").ParamTransition<bool>(this.IsDoorOpen, this.open.opening, GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.IsTrue)
			.Target(this.masterTarget);
		this.close.cloud.Target(this.Door).PlayAnim("play_cloud").OnAnimQueueComplete(this.close.idle)
			.Target(this.masterTarget);
		this.open.DefaultState(this.close.idle);
		this.open.opening.Target(this.Door).PlayAnim("open").OnAnimQueueComplete(this.open.idle)
			.Target(this.masterTarget);
		this.open.idle.Target(this.Door).PlayAnim("open_idle").Enter(new StateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State.Callback(SpecialCargoBayCluster.DropInventory))
			.Enter(new StateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State.Callback(SpecialCargoBayCluster.CloseDoorAutomatically))
			.ParamTransition<bool>(this.IsDoorOpen, this.close.closing, GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.IsFalse)
			.Target(this.masterTarget);
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x00078AD2 File Offset: 0x00076CD2
	public static void CloseDoorAutomatically(SpecialCargoBayCluster.Instance smi)
	{
		smi.CloseDoorAutomatically();
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x00078ADA File Offset: 0x00076CDA
	public static void DropInventory(SpecialCargoBayCluster.Instance smi)
	{
		smi.DropInventory();
	}

	// Token: 0x04000C89 RID: 3209
	public const string DOOR_METER_TARGET_NAME = "fg_meter_target";

	// Token: 0x04000C8A RID: 3210
	public const string TRAPPED_CRITTER_PIVOT_SYMBOL_NAME = "critter";

	// Token: 0x04000C8B RID: 3211
	public const string LOOT_SYMBOL_NAME = "loot";

	// Token: 0x04000C8C RID: 3212
	public const string DEATH_CLOUD_ANIM_NAME = "play_cloud";

	// Token: 0x04000C8D RID: 3213
	private const string OPEN_DOOR_ANIM_NAME = "open";

	// Token: 0x04000C8E RID: 3214
	private const string CLOSE_DOOR_ANIM_NAME = "close";

	// Token: 0x04000C8F RID: 3215
	private const string OPEN_DOOR_IDLE_ANIM_NAME = "open_idle";

	// Token: 0x04000C90 RID: 3216
	private const string CLOSE_DOOR_IDLE_ANIM_NAME = "close_idle";

	// Token: 0x04000C91 RID: 3217
	public SpecialCargoBayCluster.OpenStates open;

	// Token: 0x04000C92 RID: 3218
	public SpecialCargoBayCluster.CloseStates close;

	// Token: 0x04000C93 RID: 3219
	public StateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.BoolParameter IsDoorOpen;

	// Token: 0x04000C94 RID: 3220
	public StateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.TargetParameter Door;

	// Token: 0x02001210 RID: 4624
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040064F9 RID: 25849
		public Vector2 trappedOffset = new Vector2(0f, -0.3f);
	}

	// Token: 0x02001211 RID: 4625
	public class OpenStates : GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State
	{
		// Token: 0x040064FA RID: 25850
		public GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State opening;

		// Token: 0x040064FB RID: 25851
		public GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State idle;
	}

	// Token: 0x02001212 RID: 4626
	public class CloseStates : GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State
	{
		// Token: 0x040064FC RID: 25852
		public GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State closing;

		// Token: 0x040064FD RID: 25853
		public GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State idle;

		// Token: 0x040064FE RID: 25854
		public GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.State cloud;
	}

	// Token: 0x02001213 RID: 4627
	public new class Instance : GameStateMachine<SpecialCargoBayCluster, SpecialCargoBayCluster.Instance, IStateMachineTarget, SpecialCargoBayCluster.Def>.GameInstance
	{
		// Token: 0x060084CB RID: 33995 RVA: 0x003369DD File Offset: 0x00334BDD
		public void PlayDeathCloud()
		{
			if (base.IsInsideState(base.sm.close.idle))
			{
				this.GoTo(base.sm.close.cloud);
			}
		}

		// Token: 0x060084CC RID: 33996 RVA: 0x00336A0D File Offset: 0x00334C0D
		public void CloseDoor()
		{
			base.sm.IsDoorOpen.Set(false, base.smi, false);
		}

		// Token: 0x060084CD RID: 33997 RVA: 0x00336A28 File Offset: 0x00334C28
		public void OpenDoor()
		{
			base.sm.IsDoorOpen.Set(true, base.smi, false);
		}

		// Token: 0x060084CE RID: 33998 RVA: 0x00336A44 File Offset: 0x00334C44
		public Instance(IStateMachineTarget master, SpecialCargoBayCluster.Def def)
			: base(master, def)
		{
			this.buildingAnimController = base.GetComponent<KBatchedAnimController>();
			this.doorMeter = new MeterController(this.buildingAnimController, "fg_meter_target", "close_idle", Meter.Offset.UserSpecified, Grid.SceneLayer.BuildingFront, Array.Empty<string>());
			this.doorAnimController = this.doorMeter.meterController;
			KBatchedAnimTracker componentInChildren = this.doorAnimController.GetComponentInChildren<KBatchedAnimTracker>();
			componentInChildren.forceAlwaysAlive = true;
			componentInChildren.matchParentOffset = true;
			base.sm.Door.Set(this.doorAnimController.gameObject, base.smi, false);
			Storage[] components = base.gameObject.GetComponents<Storage>();
			this.critterStorage = components[0];
			this.sideProductStorage = components[1];
			base.Subscribe(1655598572, new Action<object>(this.OnLaunchConditionChanged));
		}

		// Token: 0x060084CF RID: 33999 RVA: 0x00336B09 File Offset: 0x00334D09
		public void CloseDoorAutomatically()
		{
			this.CloseDoor();
		}

		// Token: 0x060084D0 RID: 34000 RVA: 0x00336B11 File Offset: 0x00334D11
		public override void StartSM()
		{
			base.StartSM();
		}

		// Token: 0x060084D1 RID: 34001 RVA: 0x00336B1C File Offset: 0x00334D1C
		private void OnLaunchConditionChanged(object obj)
		{
			if (this.rocketModuleCluster.CraftInterface != null)
			{
				Clustercraft component = this.rocketModuleCluster.CraftInterface.GetComponent<Clustercraft>();
				if (component != null && component.Status == Clustercraft.CraftStatus.Launching)
				{
					this.CloseDoor();
				}
			}
		}

		// Token: 0x060084D2 RID: 34002 RVA: 0x00336B68 File Offset: 0x00334D68
		public void DropInventory()
		{
			List<GameObject> list = new List<GameObject>();
			List<GameObject> list2 = new List<GameObject>();
			foreach (GameObject gameObject in this.critterStorage.items)
			{
				if (gameObject != null)
				{
					Baggable component = gameObject.GetComponent<Baggable>();
					if (component != null)
					{
						component.keepWrangledNextTimeRemovedFromStorage = true;
					}
				}
			}
			Storage storage = this.critterStorage;
			bool flag = false;
			bool flag2 = false;
			List<GameObject> list3 = list;
			storage.DropAll(flag, flag2, default(Vector3), true, list3);
			Storage storage2 = this.sideProductStorage;
			bool flag3 = false;
			bool flag4 = false;
			list3 = list2;
			storage2.DropAll(flag3, flag4, default(Vector3), true, list3);
			foreach (GameObject gameObject2 in list)
			{
				KBatchedAnimController component2 = gameObject2.GetComponent<KBatchedAnimController>();
				Vector3 storePositionForCritter = this.GetStorePositionForCritter(gameObject2);
				gameObject2.transform.SetPosition(storePositionForCritter);
				component2.SetSceneLayer(Grid.SceneLayer.Creatures);
				component2.Play("trussed", KAnim.PlayMode.Loop, 1f, 0f);
			}
			foreach (GameObject gameObject3 in list2)
			{
				KBatchedAnimController component3 = gameObject3.GetComponent<KBatchedAnimController>();
				Vector3 storePositionForDrops = this.GetStorePositionForDrops();
				gameObject3.transform.SetPosition(storePositionForDrops);
				component3.SetSceneLayer(Grid.SceneLayer.Ore);
			}
		}

		// Token: 0x060084D3 RID: 34003 RVA: 0x00336CF8 File Offset: 0x00334EF8
		public Vector3 GetCritterPositionOffet(GameObject critter)
		{
			KBatchedAnimController component = critter.GetComponent<KBatchedAnimController>();
			Vector3 zero = Vector3.zero;
			zero.x = base.def.trappedOffset.x - component.Offset.x;
			zero.y = base.def.trappedOffset.y - component.Offset.y;
			return zero;
		}

		// Token: 0x060084D4 RID: 34004 RVA: 0x00336D5C File Offset: 0x00334F5C
		public Vector3 GetStorePositionForCritter(GameObject critter)
		{
			Vector3 critterPositionOffet = this.GetCritterPositionOffet(critter);
			bool flag;
			return this.buildingAnimController.GetSymbolTransform("critter", out flag).GetColumn(3) + critterPositionOffet;
		}

		// Token: 0x060084D5 RID: 34005 RVA: 0x00336D9C File Offset: 0x00334F9C
		public Vector3 GetStorePositionForDrops()
		{
			bool flag;
			return this.buildingAnimController.GetSymbolTransform("loot", out flag).GetColumn(3);
		}

		// Token: 0x040064FF RID: 25855
		public MeterController doorMeter;

		// Token: 0x04006500 RID: 25856
		private Storage critterStorage;

		// Token: 0x04006501 RID: 25857
		private Storage sideProductStorage;

		// Token: 0x04006502 RID: 25858
		private KBatchedAnimController buildingAnimController;

		// Token: 0x04006503 RID: 25859
		private KBatchedAnimController doorAnimController;

		// Token: 0x04006504 RID: 25860
		[MyCmpGet]
		private RocketModuleCluster rocketModuleCluster;
	}
}
