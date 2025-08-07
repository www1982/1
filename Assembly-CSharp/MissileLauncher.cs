using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200078B RID: 1931
public class MissileLauncher : GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>
{
	// Token: 0x060032FA RID: 13050 RVA: 0x0011ECE8 File Offset: 0x0011CEE8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Off;
		this.root.Update(delegate(MissileLauncher.Instance smi, float dt)
		{
			smi.HasLineOfSight();
		}, UpdateRate.SIM_200ms, false);
		this.Off.PlayAnim("inoperational").EventTransition(GameHashes.OperationalChanged, this.On, (MissileLauncher.Instance smi) => smi.Operational.IsOperational).Enter(delegate(MissileLauncher.Instance smi)
		{
			smi.Operational.SetActive(false, false);
		});
		this.On.DefaultState(this.On.opening).EventTransition(GameHashes.OperationalChanged, this.On.shutdown, (MissileLauncher.Instance smi) => !smi.Operational.IsOperational).ParamTransition<bool>(this.fullyBlocked, this.Nosurfacesight, GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.IsTrue)
			.ScheduleGoTo(this.shutdownDuration, this.On.idle)
			.Enter(delegate(MissileLauncher.Instance smi)
			{
				smi.Operational.SetActive(smi.Operational.IsOperational, false);
			});
		this.On.opening.PlayAnim("working_pre").OnAnimQueueComplete(this.On.searching).Target(this.cannonTarget)
			.PlayAnim("Cannon_working_pre");
		this.On.searching.PlayAnim("on", KAnim.PlayMode.Loop).Enter(delegate(MissileLauncher.Instance smi)
		{
			smi.sm.rotationComplete.Set(false, smi, false);
			smi.sm.meteorTarget.Set(null, smi, false);
			smi.cannonRotation = smi.def.scanningAngle;
		}).Update("FindMeteor", delegate(MissileLauncher.Instance smi, float dt)
		{
			smi.Searching(dt);
		}, UpdateRate.SIM_EVERY_TICK, false)
			.EventTransition(GameHashes.OnStorageChange, this.NoAmmo, (MissileLauncher.Instance smi) => smi.MissileStorage.Count <= 0 && smi.LongRangeStorage.Count <= 0)
			.ParamTransition<GameObject>(this.meteorTarget, this.Launch.targeting, (MissileLauncher.Instance smi, GameObject meteor) => meteor != null)
			.ParamTransition<GameObject>(this.longRangeTarget, this.Launch.targetingLongRange, (MissileLauncher.Instance smi, GameObject longrange) => smi.ShouldRotateToLongRange())
			.Exit(delegate(MissileLauncher.Instance smi)
			{
				smi.sm.rotationComplete.Set(false, smi, false);
			});
		this.On.idle.Target(this.masterTarget).PlayAnim("idle", KAnim.PlayMode.Loop).UpdateTransition(this.On, (MissileLauncher.Instance smi, float dt) => smi.Operational.IsOperational && smi.MeteorDetected(), UpdateRate.SIM_200ms, false)
			.EventTransition(GameHashes.ClusterDestinationChanged, this.On.searching, (MissileLauncher.Instance smi) => smi.LongRangeStorage.Count > 0)
			.Target(this.cannonTarget)
			.PlayAnim("Cannon_working_pst");
		this.On.shutdown.Target(this.masterTarget).PlayAnim("working_pst").OnAnimQueueComplete(this.Off)
			.Target(this.cannonTarget)
			.PlayAnim("Cannon_working_pst");
		this.Launch.PlayAnim("target_detected", KAnim.PlayMode.Loop).Update("Rotate", delegate(MissileLauncher.Instance smi, float dt)
		{
			smi.RotateToMeteor(dt);
		}, UpdateRate.SIM_EVERY_TICK, false);
		this.Launch.targeting.Update("Targeting", delegate(MissileLauncher.Instance smi, float dt)
		{
			if (smi.sm.meteorTarget.Get(smi).IsNullOrDestroyed())
			{
				smi.GoTo(this.On.searching);
				return;
			}
			if (smi.cannonAnimController.Rotation < smi.def.maxAngle * -1f || smi.cannonAnimController.Rotation > smi.def.maxAngle)
			{
				smi.sm.meteorTarget.Get(smi).GetComponent<Comet>().Targeted = false;
				smi.sm.meteorTarget.Set(null, smi, false);
				smi.GoTo(this.On.searching);
			}
		}, UpdateRate.SIM_EVERY_TICK, false).ParamTransition<bool>(this.rotationComplete, this.Launch.shoot, GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.IsTrue);
		this.Launch.targetingLongRange.Update("TargetingLongRange", delegate(MissileLauncher.Instance smi, float dt)
		{
		}, UpdateRate.SIM_EVERY_TICK, false).ParamTransition<bool>(this.rotationComplete, this.Launch.shoot, GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.IsTrue);
		this.Launch.shoot.ScheduleGoTo(this.shootDelayDuration, this.Launch.pst).Exit("LaunchMissile", delegate(MissileLauncher.Instance smi)
		{
			if (smi.sm.meteorTarget.Get(smi) != null)
			{
				smi.LaunchMissile();
			}
			else if (smi.sm.longRangeTarget.Get(smi) != null)
			{
				smi.LaunchLongRangeMissile();
			}
			this.cannonTarget.Get(smi).GetComponent<KBatchedAnimController>().Play("Cannon_shooting_pre", KAnim.PlayMode.Once, 1f, 0f);
		});
		this.Launch.pst.Target(this.masterTarget).Enter(delegate(MissileLauncher.Instance smi)
		{
			smi.SetOreChunk();
			KAnimControllerBase component = smi.GetComponent<KAnimControllerBase>();
			if (smi.GetComponent<Storage>().Count <= 0)
			{
				component.Play("base_shooting_pst_last", KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			component.Play("base_shooting_pst", KAnim.PlayMode.Once, 1f, 0f);
		}).Target(this.cannonTarget)
			.PlayAnim("Cannon_shooting_pst")
			.OnAnimQueueComplete(this.Cooldown);
		this.Cooldown.Exit(delegate(MissileLauncher.Instance smi)
		{
			smi.SpawnOre();
		}).Enter(delegate(MissileLauncher.Instance smi)
		{
			KAnimControllerBase component2 = smi.GetComponent<KAnimControllerBase>();
			if (smi.GetComponent<Storage>().Count <= 0)
			{
				component2.Play("base_ejecting_last", KAnim.PlayMode.Once, 1f, 0f);
			}
			else
			{
				component2.Play("base_ejecting", KAnim.PlayMode.Once, 1f, 0f);
			}
			smi.sm.rotationComplete.Set(false, smi, false);
			smi.sm.meteorTarget.Set(null, smi, false);
			smi.GoTo(smi.CooldownGoToState);
		});
		this.Cooldown.basic.Update("Rotate", delegate(MissileLauncher.Instance smi, float dt)
		{
			smi.RotateToMeteor(dt);
		}, UpdateRate.SIM_EVERY_TICK, false).OnAnimQueueComplete(this.On.searching);
		this.Cooldown.longrange.QueueAnim("cooldown", true, null).ToggleStatusItem(MissileLauncher.LongRangeCooldown, null).Target(this.cannonTarget)
			.QueueAnim("cooldown_cannon_pre", false, null)
			.QueueAnim("cooldown_cannon", true, null)
			.ScheduleGoTo(MissileLauncher.longrangeCooldownTime, this.On.searching)
			.Exit(delegate(MissileLauncher.Instance smi)
			{
				this.cannonTarget.Get(smi).GetComponent<KBatchedAnimController>().Play("cooldown_cannon_pst", KAnim.PlayMode.Once, 1f, 0f);
			});
		this.Nosurfacesight.Target(this.masterTarget).PlayAnim("working_pst").QueueAnim("error", false, null)
			.ParamTransition<bool>(this.fullyBlocked, this.On, GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.IsFalse)
			.Target(this.cannonTarget)
			.PlayAnim("Cannon_working_pst")
			.Enter(delegate(MissileLauncher.Instance smi)
			{
				smi.Operational.SetActive(false, false);
			});
		this.NoAmmo.PlayAnim("off_open").EventTransition(GameHashes.OnStorageChange, this.On, (MissileLauncher.Instance smi) => smi.MissileStorage.Count > 0 || smi.LongRangeStorage.Count > 0).Enter(delegate(MissileLauncher.Instance smi)
		{
			smi.Operational.SetActive(false, false);
		})
			.Exit(delegate(MissileLauncher.Instance smi)
			{
				smi.GetComponent<KAnimControllerBase>().Play("off_closing", KAnim.PlayMode.Once, 1f, 0f);
			})
			.Target(this.cannonTarget)
			.PlayAnim("Cannon_working_pst");
	}

	// Token: 0x04001E88 RID: 7816
	private static StatusItem NoSurfaceSight = new StatusItem("MissileLauncher_NoSurfaceSight", "BUILDING", "status_item_no_sky", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);

	// Token: 0x04001E89 RID: 7817
	private static StatusItem PartiallyBlockedStatus = new StatusItem("MissileLauncher_PartiallyBlocked", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);

	// Token: 0x04001E8A RID: 7818
	private static StatusItem LongRangeCooldown = new StatusItem("MissileLauncher_LongRangeCooldown", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);

	// Token: 0x04001E8B RID: 7819
	public float shutdownDuration = 50f;

	// Token: 0x04001E8C RID: 7820
	public float shootDelayDuration = 0.25f;

	// Token: 0x04001E8D RID: 7821
	public static float SHELL_MASS = 2.5f;

	// Token: 0x04001E8E RID: 7822
	public static float SHELL_TEMPERATURE = 353.15f;

	// Token: 0x04001E8F RID: 7823
	public StateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.BoolParameter rotationComplete;

	// Token: 0x04001E90 RID: 7824
	public StateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.ObjectParameter<GameObject> meteorTarget = new StateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.ObjectParameter<GameObject>();

	// Token: 0x04001E91 RID: 7825
	public StateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.TargetParameter cannonTarget;

	// Token: 0x04001E92 RID: 7826
	public StateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.BoolParameter fullyBlocked;

	// Token: 0x04001E93 RID: 7827
	public StateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.ObjectParameter<GameObject> longRangeTarget = new StateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.ObjectParameter<GameObject>();

	// Token: 0x04001E94 RID: 7828
	public static float longrangeCooldownTime = 10f;

	// Token: 0x04001E95 RID: 7829
	public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State Off;

	// Token: 0x04001E96 RID: 7830
	public MissileLauncher.OnState On;

	// Token: 0x04001E97 RID: 7831
	public MissileLauncher.LaunchState Launch;

	// Token: 0x04001E98 RID: 7832
	public MissileLauncher.CooldownState Cooldown;

	// Token: 0x04001E99 RID: 7833
	public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State Nosurfacesight;

	// Token: 0x04001E9A RID: 7834
	public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State NoAmmo;

	// Token: 0x02001682 RID: 5762
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040072EF RID: 29423
		public static readonly CellOffset LaunchOffset = new CellOffset(0, 4);

		// Token: 0x040072F0 RID: 29424
		public float launchSpeed = 30f;

		// Token: 0x040072F1 RID: 29425
		public float rotationSpeed = 100f;

		// Token: 0x040072F2 RID: 29426
		public static readonly Vector2I launchRange = new Vector2I(16, 32);

		// Token: 0x040072F3 RID: 29427
		public float scanningAngle = 50f;

		// Token: 0x040072F4 RID: 29428
		public float maxAngle = 80f;
	}

	// Token: 0x02001683 RID: 5763
	public new class Instance : GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.GameInstance
	{
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x0600957A RID: 38266 RVA: 0x00373FE8 File Offset: 0x003721E8
		public WorldContainer myWorld
		{
			get
			{
				if (this.worldContainer == null)
				{
					this.worldContainer = this.GetMyWorld();
				}
				return this.worldContainer;
			}
		}

		// Token: 0x0600957B RID: 38267 RVA: 0x0037400C File Offset: 0x0037220C
		public Instance(IStateMachineTarget master, MissileLauncher.Def def)
			: base(master, def)
		{
			Components.MissileLaunchers.Add(this);
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			string text = component.name + ".cannon";
			base.smi.cannonGameObject = new GameObject(text);
			base.smi.cannonGameObject.SetActive(false);
			base.smi.cannonGameObject.transform.parent = component.transform;
			base.smi.cannonGameObject.AddComponent<KPrefabID>().PrefabTag = new Tag(text);
			base.smi.cannonAnimController = base.smi.cannonGameObject.AddComponent<KBatchedAnimController>();
			base.smi.cannonAnimController.AnimFiles = new KAnimFile[] { component.AnimFiles[0] };
			base.smi.cannonAnimController.initialAnim = "Cannon_off";
			base.smi.cannonAnimController.isMovable = true;
			base.smi.cannonAnimController.SetSceneLayer(Grid.SceneLayer.Building);
			component.SetSymbolVisiblity("cannon_target", false);
			bool flag;
			Vector3 vector = component.GetSymbolTransform(new HashedString("cannon_target"), out flag).GetColumn(3);
			vector.z = Grid.GetLayerZ(Grid.SceneLayer.Building);
			base.smi.cannonGameObject.transform.SetPosition(vector);
			this.launchPosition = vector;
			Grid.PosToXY(this.launchPosition, out this.launchXY);
			base.smi.cannonGameObject.SetActive(true);
			base.smi.sm.cannonTarget.Set(base.smi.cannonGameObject, base.smi, false);
			KAnim.Anim anim = component.AnimFiles[0].GetData().GetAnim("Cannon_shooting_pre");
			if (anim != null)
			{
				this.launchAnimTime = anim.totalTime / 2f;
			}
			else
			{
				global::Debug.LogWarning("MissileLauncher anim data is missing");
				this.launchAnimTime = 1f;
			}
			this.meter = new MeterController(component, "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
			this.longRangemeter = new MeterController(component, "meter_target_longrange", "meter_longrange", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
			base.Subscribe(-1201923725, new Action<object>(this.OnHighlight));
			base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
			foreach (Storage storage in base.smi.gameObject.GetComponents<Storage>())
			{
				if (storage.storageID == "MissileBasic")
				{
					this.MissileStorage = storage;
				}
				else if (storage.storageID == "MissileLongRange")
				{
					this.LongRangeStorage = storage;
				}
				else if (storage.storageID == "CondiutStorage")
				{
					this.LoadingStorage = storage;
				}
			}
			base.Subscribe(-1697596308, new Action<object>(this.OnStorage));
			FlatTagFilterable component2 = base.smi.master.GetComponent<FlatTagFilterable>();
			foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.Comet))
			{
				if (!gameObject.HasTag(GameTags.DeprecatedContent))
				{
					if (!component2.tagOptions.Contains(gameObject.PrefabID()))
					{
						component2.tagOptions.Add(gameObject.PrefabID());
						component2.selectedTags.Add(gameObject.PrefabID());
					}
					component2.selectedTags.Remove(GassyMooCometConfig.ID);
				}
			}
			this.ManualDeliveryKgs = base.smi.gameObject.GetComponents<ManualDeliveryKG>();
		}

		// Token: 0x0600957C RID: 38268 RVA: 0x003743FC File Offset: 0x003725FC
		public override void StartSM()
		{
			base.StartSM();
			this.OnStorage(null);
			base.smi.master.GetComponent<FlatTagFilterable>().currentlyUserAssignable = this.AmmunitionIsAllowed("MissileBasic");
			this.clusterDestinationSelector = base.smi.master.GetComponent<EntityClusterDestinationSelector>();
			if (this.clusterDestinationSelector != null)
			{
				this.clusterDestinationSelector.assignable = this.AmmunitionIsAllowed("MissileLongRange");
			}
			this.UpdateAmmunitionDelivery();
			this.UpdateMeterVisibility();
		}

		// Token: 0x0600957D RID: 38269 RVA: 0x00374486 File Offset: 0x00372686
		protected override void OnCleanUp()
		{
			Components.MissileLaunchers.Remove(this);
			base.Unsubscribe(-1201923725, new Action<object>(this.OnHighlight));
			base.OnCleanUp();
		}

		// Token: 0x0600957E RID: 38270 RVA: 0x003744B0 File Offset: 0x003726B0
		private void OnHighlight(object data)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			base.smi.cannonAnimController.HighlightColour = component.HighlightColour;
		}

		// Token: 0x0600957F RID: 38271 RVA: 0x003744DC File Offset: 0x003726DC
		private void OnCopySettings(object data)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject != null)
			{
				MissileLauncher.Instance smi = gameObject.GetSMI<MissileLauncher.Instance>();
				if (smi != null)
				{
					this.ammunitionPermissions.Clear();
					foreach (KeyValuePair<Tag, bool> keyValuePair in smi.ammunitionPermissions)
					{
						this.ChangeAmmunition(keyValuePair.Key, smi.AmmunitionIsAllowed(keyValuePair.Key));
					}
					base.smi.master.GetComponent<FlatTagFilterable>().currentlyUserAssignable = this.AmmunitionIsAllowed("MissileBasic");
					this.clusterDestinationSelector = base.smi.master.GetComponent<EntityClusterDestinationSelector>();
					if (this.clusterDestinationSelector != null)
					{
						this.clusterDestinationSelector.assignable = this.AmmunitionIsAllowed("MissileLongRange");
					}
					if (smi.sm.longRangeTarget != null)
					{
						base.sm.longRangeTarget.Set(smi.sm.longRangeTarget.Get(smi), this, false);
					}
				}
			}
		}

		// Token: 0x06009580 RID: 38272 RVA: 0x00374604 File Offset: 0x00372804
		private void OnStorage(object data)
		{
			if (this.LoadingStorage.items.Count > 0)
			{
				KPrefabID component = this.LoadingStorage.items[0].GetComponent<KPrefabID>();
				if (this.AmmunitionIsAllowed(component.PrefabTag))
				{
					Pickupable component2 = component.GetComponent<Pickupable>();
					Storage storage = null;
					if (component.PrefabTag == "MissileBasic")
					{
						storage = this.MissileStorage;
					}
					else if (component.PrefabTag == "MissileLongRange")
					{
						storage = this.LongRangeStorage;
					}
					if (storage != null && storage.Capacity() - storage.MassStored() >= component2.PrimaryElement.Mass)
					{
						this.LoadingStorage.Transfer(component2.gameObject, storage, true, true);
					}
				}
			}
			this.meter.SetPositionPercent(Mathf.Clamp01(this.MissileStorage.MassStored() / this.MissileStorage.capacityKg));
			this.longRangemeter.SetPositionPercent(Mathf.Clamp01(this.LongRangeStorage.MassStored() / this.LongRangeStorage.capacityKg));
		}

		// Token: 0x06009581 RID: 38273 RVA: 0x0037471C File Offset: 0x0037291C
		private void UpdateMeterVisibility()
		{
			this.meter.gameObject.SetActive(this.AmmunitionIsAllowed("MissileBasic"));
			this.longRangemeter.gameObject.SetActive(this.AmmunitionIsAllowed("MissileLongRange"));
		}

		// Token: 0x06009582 RID: 38274 RVA: 0x0037476C File Offset: 0x0037296C
		public void Searching(float dt)
		{
			if (!this.FindMeteor())
			{
				this.FindLongRangeTarget();
			}
			this.RotateCannon(dt, base.def.rotationSpeed / 2f);
			if (base.smi.sm.rotationComplete.Get(base.smi))
			{
				this.cannonRotation *= -1f;
				base.smi.sm.rotationComplete.Set(false, base.smi, false);
			}
		}

		// Token: 0x06009583 RID: 38275 RVA: 0x003747F0 File Offset: 0x003729F0
		private bool FindMeteor()
		{
			if (this.MissileStorage.items.Count > 0)
			{
				GameObject gameObject = this.ChooseClosestInterceptionPoint(this.myWorld.id);
				if (gameObject != null)
				{
					base.smi.sm.meteorTarget.Set(gameObject, base.smi, false);
					gameObject.GetComponent<Comet>().Targeted = true;
					base.smi.cannonRotation = this.CalculateLaunchAngle(gameObject.transform.position);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009584 RID: 38276 RVA: 0x00374874 File Offset: 0x00372A74
		private bool FindLongRangeTarget()
		{
			if (this.LongRangeStorage.items.Count > 0)
			{
				GameObject gameObject = null;
				if (this.clusterDestinationSelector != null)
				{
					if (this.clusterDestinationSelector.GetDestination() != this.myWorld.GetComponent<ClusterGridEntity>().Location)
					{
						ClusterGridEntity visibleEntityOfLayerAtCell = ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(this.clusterDestinationSelector.GetDestination(), EntityLayer.Meteor);
						gameObject = ((visibleEntityOfLayerAtCell != null) ? visibleEntityOfLayerAtCell.gameObject : null);
					}
				}
				else
				{
					GameplayEventInstance gameplayEventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.IdHash, -1);
					if (gameplayEventInstance != null)
					{
						GameObject impactorInstance = ((LargeImpactorEvent.StatesInstance)gameplayEventInstance.smi).impactorInstance;
						gameObject = ((impactorInstance != null) ? impactorInstance.gameObject : null);
					}
				}
				if (gameObject != null)
				{
					Vector3 position = base.transform.position;
					position.y += 50f;
					if (this.IsPathClear(this.launchPosition, position))
					{
						base.smi.sm.longRangeTarget.Set(gameObject, base.smi, false);
						base.smi.cannonRotation = this.CalculateLaunchAngle(position);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06009585 RID: 38277 RVA: 0x003749A8 File Offset: 0x00372BA8
		private float CalculateLaunchAngle(Vector3 targetPosition)
		{
			Vector3 vector = Vector3.Normalize(targetPosition - this.launchPosition);
			return MathUtil.AngleSigned(Vector3.up, vector, Vector3.forward);
		}

		// Token: 0x06009586 RID: 38278 RVA: 0x003749D8 File Offset: 0x00372BD8
		public void LaunchMissile()
		{
			GameObject gameObject = this.MissileStorage.FindFirst("MissileBasic");
			if (gameObject != null)
			{
				Pickupable pickupable = gameObject.GetComponent<Pickupable>();
				if (pickupable.TotalAmount <= 1f)
				{
					this.MissileStorage.Drop(pickupable.gameObject, true);
				}
				else
				{
					pickupable = EntitySplitter.Split(pickupable, 1f, null);
				}
				this.SetMissileElement(gameObject);
				GameObject gameObject2 = base.smi.sm.meteorTarget.Get(base.smi);
				if (!gameObject2.IsNullOrDestroyed())
				{
					pickupable.GetSMI<MissileProjectile.StatesInstance>().PrepareLaunch(gameObject2.GetComponent<Comet>(), base.def.launchSpeed, this.launchPosition, base.smi.cannonRotation);
					this.CooldownGoToState = base.sm.Cooldown.basic;
				}
			}
		}

		// Token: 0x06009587 RID: 38279 RVA: 0x00374AAC File Offset: 0x00372CAC
		public void LaunchLongRangeMissile()
		{
			GameObject gameObject = this.LongRangeStorage.FindFirst("MissileLongRange");
			if (gameObject != null)
			{
				Pickupable pickupable = gameObject.GetComponent<Pickupable>();
				if (pickupable.TotalAmount <= 1f)
				{
					this.LongRangeStorage.Drop(pickupable.gameObject, true);
				}
				else
				{
					pickupable = EntitySplitter.Split(pickupable, 1f, null);
				}
				this.SetMissileElement(gameObject);
				GameObject gameObject2 = base.smi.sm.longRangeTarget.Get(base.smi);
				if (!gameObject2.IsNullOrDestroyed())
				{
					pickupable.GetSMI<MissileLongRangeProjectile.StatesInstance>().PrepareLaunch(gameObject2, base.def.launchSpeed, this.launchPosition, base.smi.cannonRotation);
					this.CooldownGoToState = base.sm.Cooldown.longrange;
					base.smi.sm.longRangeTarget.Set(null, base.smi, false);
				}
			}
		}

		// Token: 0x06009588 RID: 38280 RVA: 0x00374B98 File Offset: 0x00372D98
		private void SetMissileElement(GameObject missile)
		{
			this.missileElement = missile.GetComponent<PrimaryElement>().Element.tag;
			if (Assets.GetPrefab(this.missileElement) == null)
			{
				global::Debug.LogWarning(string.Format("Missing element {0} for missile launcher. Defaulting to IronOre", this.missileElement));
				this.missileElement = GameTags.IronOre;
			}
		}

		// Token: 0x06009589 RID: 38281 RVA: 0x00374BF4 File Offset: 0x00372DF4
		public GameObject ChooseClosestInterceptionPoint(int world_id)
		{
			GameObject gameObject = null;
			List<Comet> items = Components.Meteors.GetItems(world_id);
			float num = (float)MissileLauncher.Def.launchRange.y;
			foreach (Comet comet in items)
			{
				if (!comet.IsNullOrDestroyed() && !comet.Targeted && this.TargetFilter.selectedTags.Contains(comet.typeID))
				{
					Vector3 targetPosition = comet.TargetPosition;
					float num2;
					Vector3 vector = this.CalculateCollisionPoint(targetPosition, comet.Velocity, out num2);
					Grid.PosToCell(vector);
					float num3 = Vector3.Distance(vector, this.launchPosition);
					if (num3 < num && num2 > this.launchAnimTime && this.IsMeteorInRange(vector) && this.IsPathClear(this.launchPosition, targetPosition))
					{
						gameObject = comet.gameObject;
						num = num3;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x0600958A RID: 38282 RVA: 0x00374CF8 File Offset: 0x00372EF8
		private bool IsMeteorInRange(Vector3 interception_point)
		{
			Vector2I vector2I;
			Grid.PosToXY(interception_point, out vector2I);
			return Math.Abs(vector2I.X - this.launchXY.X) <= MissileLauncher.Def.launchRange.X && vector2I.Y - this.launchXY.Y > 0 && vector2I.Y - this.launchXY.Y <= MissileLauncher.Def.launchRange.Y;
		}

		// Token: 0x0600958B RID: 38283 RVA: 0x00374D74 File Offset: 0x00372F74
		public bool IsPathClear(Vector3 startPoint, Vector3 endPoint)
		{
			Vector2I vector2I = Grid.PosToXY(startPoint);
			Vector2I vector2I2 = Grid.PosToXY(endPoint);
			return Grid.TestLineOfSight(vector2I.x, vector2I.y, vector2I2.x, vector2I2.y, new Func<int, bool>(this.IsCellBlockedFromSky), false, true);
		}

		// Token: 0x0600958C RID: 38284 RVA: 0x00374DBC File Offset: 0x00372FBC
		public bool IsCellBlockedFromSky(int cell)
		{
			if (Grid.IsValidCell(cell) && (int)Grid.WorldIdx[cell] == this.myWorld.id)
			{
				return Grid.Solid[cell];
			}
			int num;
			int num2;
			Grid.CellToXY(cell, out num, out num2);
			return num2 <= this.launchXY.Y;
		}

		// Token: 0x0600958D RID: 38285 RVA: 0x00374E0C File Offset: 0x0037300C
		public Vector3 CalculateCollisionPoint(Vector3 targetPosition, Vector3 targetVelocity, out float timeToCollision)
		{
			Vector3 vector = targetVelocity - base.smi.def.launchSpeed * (targetPosition - this.launchPosition).normalized;
			timeToCollision = (targetPosition - this.launchPosition).magnitude / vector.magnitude;
			return targetPosition + targetVelocity * timeToCollision;
		}

		// Token: 0x0600958E RID: 38286 RVA: 0x00374E78 File Offset: 0x00373078
		public void HasLineOfSight()
		{
			bool flag = false;
			bool flag2 = true;
			Extents extents = base.GetComponent<Building>().GetExtents();
			int num = this.launchXY.x - MissileLauncher.Def.launchRange.X;
			int num2 = this.launchXY.x + MissileLauncher.Def.launchRange.X;
			int num3 = extents.y + extents.height;
			int num4 = Grid.XYToCell(Math.Max((int)this.myWorld.minimumBounds.x, num), num3);
			int num5 = Grid.XYToCell(Math.Min((int)this.myWorld.maximumBounds.x, num2), num3);
			for (int i = num4; i <= num5; i++)
			{
				flag = flag || Grid.ExposedToSunlight[i] <= 0;
				flag2 = flag2 && Grid.ExposedToSunlight[i] <= 0;
			}
			this.Selectable.ToggleStatusItem(MissileLauncher.PartiallyBlockedStatus, flag && !flag2, null);
			this.Selectable.ToggleStatusItem(MissileLauncher.NoSurfaceSight, flag2, null);
			base.smi.sm.fullyBlocked.Set(flag2, base.smi, false);
		}

		// Token: 0x0600958F RID: 38287 RVA: 0x00374FA9 File Offset: 0x003731A9
		public bool MeteorDetected()
		{
			return Components.Meteors.GetItems(this.myWorld.id).Count > 0;
		}

		// Token: 0x06009590 RID: 38288 RVA: 0x00374FC8 File Offset: 0x003731C8
		public void SetOreChunk()
		{
			if (!this.missileElement.IsValid)
			{
				global::Debug.LogWarning(string.Format("Missing element {0} for missile launcher. Defaulting to IronOre", this.missileElement));
				this.missileElement = GameTags.IronOre;
			}
			KAnim.Build.Symbol symbolByIndex = Assets.GetPrefab(this.missileElement).GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbolByIndex(0U);
			base.gameObject.GetComponent<SymbolOverrideController>().AddSymbolOverride("Shell", symbolByIndex, 0);
		}

		// Token: 0x06009591 RID: 38289 RVA: 0x0037504C File Offset: 0x0037324C
		public void SpawnOre()
		{
			bool flag;
			Vector3 vector = base.GetComponent<KBatchedAnimController>().GetSymbolTransform("Shell", out flag).GetColumn(3);
			vector.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			Assets.GetPrefab(this.missileElement).GetComponent<PrimaryElement>().Element.substance.SpawnResource(vector, MissileLauncher.SHELL_MASS, MissileLauncher.SHELL_TEMPERATURE, byte.MaxValue, 0, false, false, false);
		}

		// Token: 0x06009592 RID: 38290 RVA: 0x003750C4 File Offset: 0x003732C4
		public void RotateCannon(float dt, float rotation_speed)
		{
			float num = this.cannonRotation - this.simpleAngle;
			if (num > 180f)
			{
				num -= 360f;
			}
			else if (num < -180f)
			{
				num += 360f;
			}
			float num2 = rotation_speed * dt;
			if (num > 0f && num2 < num)
			{
				this.simpleAngle += num2;
				this.cannonAnimController.Rotation = this.simpleAngle;
				return;
			}
			if (num < 0f && -num2 > num)
			{
				this.simpleAngle -= num2;
				this.cannonAnimController.Rotation = this.simpleAngle;
				return;
			}
			this.simpleAngle = this.cannonRotation;
			this.cannonAnimController.Rotation = this.simpleAngle;
			base.smi.sm.rotationComplete.Set(true, base.smi, false);
		}

		// Token: 0x06009593 RID: 38291 RVA: 0x0037519C File Offset: 0x0037339C
		public bool ShouldRotateToLongRange()
		{
			return !base.smi.sm.longRangeTarget.Get(base.smi).IsNullOrDestroyed() && this.LongRangeStorage.items.Count > 0 && this.IsPathClear(this.launchPosition, this.launchPosition + new Vector3(0f, 50f, 0f));
		}

		// Token: 0x06009594 RID: 38292 RVA: 0x0037520C File Offset: 0x0037340C
		public void RotateToMeteor(float dt)
		{
			GameObject gameObject = base.sm.meteorTarget.Get(this);
			float num;
			if (!gameObject.IsNullOrDestroyed())
			{
				num = this.CalculateLaunchAngle(gameObject.transform.position);
			}
			else
			{
				if (!this.ShouldRotateToLongRange())
				{
					return;
				}
				Vector3 position = base.transform.position;
				position.y += 50f;
				num = this.CalculateLaunchAngle(position);
			}
			float num2 = num - this.simpleAngle;
			if (num2 > 180f)
			{
				num2 -= 360f;
			}
			else if (num2 < -180f)
			{
				num2 += 360f;
			}
			float num3 = base.def.rotationSpeed * dt;
			if (num2 > 0f && num3 < num2)
			{
				this.simpleAngle += num3;
				this.cannonAnimController.Rotation = this.simpleAngle;
				return;
			}
			if (num2 < 0f && -num3 > num2)
			{
				this.simpleAngle -= num3;
				this.cannonAnimController.Rotation = this.simpleAngle;
				return;
			}
			base.smi.sm.rotationComplete.Set(true, base.smi, false);
		}

		// Token: 0x06009595 RID: 38293 RVA: 0x00375328 File Offset: 0x00373528
		public void ChangeAmmunition(Tag tag, bool allowed)
		{
			if (!this.ammunitionPermissions.ContainsKey(tag))
			{
				this.ammunitionPermissions.Add(tag, false);
			}
			this.ammunitionPermissions[tag] = allowed;
			this.UpdateAmmunitionDelivery();
			this.OnStorage(null);
			this.UpdateMeterVisibility();
		}

		// Token: 0x06009596 RID: 38294 RVA: 0x00375365 File Offset: 0x00373565
		public bool AmmunitionIsAllowed(Tag tag)
		{
			return this.ammunitionPermissions.ContainsKey(tag) && this.ammunitionPermissions[tag];
		}

		// Token: 0x06009597 RID: 38295 RVA: 0x00375384 File Offset: 0x00373584
		private void UpdateAmmunitionDelivery()
		{
			foreach (ManualDeliveryKG manualDeliveryKG in this.ManualDeliveryKgs)
			{
				bool flag = this.AmmunitionIsAllowed(manualDeliveryKG.RequestedItemTag);
				manualDeliveryKG.Pause(!flag, "ammunitionnotallowed");
			}
		}

		// Token: 0x040072F5 RID: 29429
		[MyCmpReq]
		public Operational Operational;

		// Token: 0x040072F6 RID: 29430
		public Storage MissileStorage;

		// Token: 0x040072F7 RID: 29431
		public Storage LongRangeStorage;

		// Token: 0x040072F8 RID: 29432
		private Storage LoadingStorage;

		// Token: 0x040072F9 RID: 29433
		public ManualDeliveryKG[] ManualDeliveryKgs;

		// Token: 0x040072FA RID: 29434
		[MyCmpReq]
		public KSelectable Selectable;

		// Token: 0x040072FB RID: 29435
		[MyCmpReq]
		public FlatTagFilterable TargetFilter;

		// Token: 0x040072FC RID: 29436
		private EntityClusterDestinationSelector clusterDestinationSelector;

		// Token: 0x040072FD RID: 29437
		[Serialize]
		private Dictionary<Tag, bool> ammunitionPermissions = new Dictionary<Tag, bool> { { "MissileBasic", true } };

		// Token: 0x040072FE RID: 29438
		private Vector3 launchPosition;

		// Token: 0x040072FF RID: 29439
		private Vector2I launchXY;

		// Token: 0x04007300 RID: 29440
		private float launchAnimTime;

		// Token: 0x04007301 RID: 29441
		public KBatchedAnimController cannonAnimController;

		// Token: 0x04007302 RID: 29442
		public GameObject cannonGameObject;

		// Token: 0x04007303 RID: 29443
		public float cannonRotation;

		// Token: 0x04007304 RID: 29444
		public float simpleAngle;

		// Token: 0x04007305 RID: 29445
		private Tag missileElement;

		// Token: 0x04007306 RID: 29446
		private MeterController meter;

		// Token: 0x04007307 RID: 29447
		private MeterController longRangemeter;

		// Token: 0x04007308 RID: 29448
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State CooldownGoToState;

		// Token: 0x04007309 RID: 29449
		private WorldContainer worldContainer;
	}

	// Token: 0x02001684 RID: 5764
	public class OnState : GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State
	{
		// Token: 0x0400730A RID: 29450
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State searching;

		// Token: 0x0400730B RID: 29451
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State opening;

		// Token: 0x0400730C RID: 29452
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State shutdown;

		// Token: 0x0400730D RID: 29453
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State idle;
	}

	// Token: 0x02001685 RID: 5765
	public class LaunchState : GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State
	{
		// Token: 0x0400730E RID: 29454
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State targeting;

		// Token: 0x0400730F RID: 29455
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State targetingLongRange;

		// Token: 0x04007310 RID: 29456
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State shoot;

		// Token: 0x04007311 RID: 29457
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State pst;
	}

	// Token: 0x02001686 RID: 5766
	public class CooldownState : GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State
	{
		// Token: 0x04007312 RID: 29458
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State longrange;

		// Token: 0x04007313 RID: 29459
		public GameStateMachine<MissileLauncher, MissileLauncher.Instance, IStateMachineTarget, MissileLauncher.Def>.State basic;
	}
}
