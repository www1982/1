using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200078C RID: 1932
[SerializationConfig(MemberSerialization.OptIn)]
public class MissileLongRangeProjectile : GameStateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>
{
	// Token: 0x06003300 RID: 13056 RVA: 0x0011F608 File Offset: 0x0011D808
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ParamTransition<GameObject>(this.asteroidTarget, this.launch, (MissileLongRangeProjectile.StatesInstance smi, GameObject target) => !target.IsNullOrDestroyed());
		this.launch.Update("Launch", delegate(MissileLongRangeProjectile.StatesInstance smi, float dt)
		{
			smi.UpdateLaunch(dt);
		}, UpdateRate.SIM_EVERY_TICK, false).ParamTransition<bool>(this.triggeroutofworld, this.leaveworld, GameStateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>.IsTrue).Enter(delegate(MissileLongRangeProjectile.StatesInstance smi)
		{
			Vector3 position = smi.master.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingBack);
			smi.smokeTrailFX = Util.KInstantiate(EffectPrefabs.Instance.LongRangeMissileSmokeTrailFX, position);
			smi.smokeTrailFX.transform.SetParent(smi.master.transform);
			smi.smokeTrailFX.SetActive(true);
			smi.StartTakeoff();
			KFMOD.PlayOneShot(GlobalAssets.GetSound("MissileLauncher_Missile_ignite", false), CameraController.Instance.GetVerticallyScaledPosition(position, false), 1f);
		});
		this.leaveworld.Enter(delegate(MissileLongRangeProjectile.StatesInstance smi)
		{
			smi.ExitWorldEnterStarmap();
		});
	}

	// Token: 0x04001E9B RID: 7835
	public GameStateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>.State launch;

	// Token: 0x04001E9C RID: 7836
	public GameStateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>.State leaveworld;

	// Token: 0x04001E9D RID: 7837
	public StateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>.BoolParameter triggeroutofworld = new StateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>.BoolParameter(false);

	// Token: 0x04001E9E RID: 7838
	public StateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>.ObjectParameter<GameObject> asteroidTarget = new StateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>.ObjectParameter<GameObject>();

	// Token: 0x02001688 RID: 5768
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001689 RID: 5769
	public class StatesInstance : GameStateMachine<MissileLongRangeProjectile, MissileLongRangeProjectile.StatesInstance, IStateMachineTarget, MissileLongRangeProjectile.Def>.GameInstance
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060095B5 RID: 38325 RVA: 0x00375667 File Offset: 0x00373867
		private Vector3 Position
		{
			get
			{
				return base.transform.position + this.animController.Offset;
			}
		}

		// Token: 0x060095B6 RID: 38326 RVA: 0x00375684 File Offset: 0x00373884
		public StatesInstance(IStateMachineTarget master, MissileLongRangeProjectile.Def def)
			: base(master, def)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
		}

		// Token: 0x060095B7 RID: 38327 RVA: 0x003756AC File Offset: 0x003738AC
		public override void StartSM()
		{
			base.StartSM();
			if (this.launchedTarget.Get() != null)
			{
				base.sm.asteroidTarget.Set(this.launchedTarget.Get().gameObject, this, false);
				this.myWorld = ClusterManager.Instance.GetWorld(this.myWorldId);
			}
		}

		// Token: 0x060095B8 RID: 38328 RVA: 0x0037570B File Offset: 0x0037390B
		public void StartTakeoff()
		{
			if (GameComps.Fallers.Has(base.gameObject))
			{
				GameComps.Fallers.Remove(base.gameObject);
			}
			base.GetComponent<Pickupable>().handleFallerComponents = false;
		}

		// Token: 0x060095B9 RID: 38329 RVA: 0x0037573C File Offset: 0x0037393C
		public void UpdateLaunch(float dt)
		{
			float num = MathUtil.AngleSigned(Vector3.up, Vector3.up, Vector3.forward);
			this.animController.Rotation = num;
			int num2 = Grid.PosToCell(this.Position);
			Vector2I vector2I = Grid.CellToXY(num2);
			if (!Grid.IsValidCell(num2))
			{
				base.smi.sm.triggeroutofworld.Set(true, base.smi, false);
				return;
			}
			if (Grid.IsValidCellInWorld(Grid.PosToCell(this.Position), this.myWorldId) && (float)vector2I.y < this.myWorld.maximumBounds.y)
			{
				base.transform.SetPosition(base.transform.position + Vector3.up * (this.launchSpeed * dt));
			}
			else
			{
				this.animController.Offset += Vector3.up * (this.launchSpeed * dt);
			}
			ParticleSystem[] componentsInChildren = base.smi.smokeTrailFX.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.transform.SetPositionAndRotation(this.Position, Quaternion.identity);
			}
		}

		// Token: 0x060095BA RID: 38330 RVA: 0x00375868 File Offset: 0x00373A68
		public void PrepareLaunch(GameObject asteroid_target, float speed, Vector3 launchPos, float launchAngle)
		{
			base.gameObject.transform.SetParent(null);
			base.gameObject.layer = LayerMask.NameToLayer("Default");
			launchPos.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingBack);
			base.gameObject.transform.SetLocalPosition(launchPos);
			this.animController.Rotation = launchAngle;
			this.animController.Offset = Vector3.back;
			this.animController.SetVisiblity(true);
			FetchableMonitor.Instance smi = base.gameObject.GetSMI<FetchableMonitor.Instance>();
			if (smi != null)
			{
				smi.SetForceUnfetchable(true);
			}
			base.sm.triggeroutofworld.Set(false, base.smi, false);
			base.sm.asteroidTarget.Set(asteroid_target, base.smi, false);
			this.launchedTarget = new Ref<KPrefabID>(asteroid_target.GetComponent<KPrefabID>());
			this.launchSpeed = speed;
			this.myWorld = base.gameObject.GetMyWorld();
			this.myWorldId = this.myWorld.id;
			ClusterGridEntity component = this.myWorld.GetComponent<ClusterGridEntity>();
			if (component != null)
			{
				this.myLocation = component.Location;
			}
		}

		// Token: 0x060095BB RID: 38331 RVA: 0x00375988 File Offset: 0x00373B88
		public void ExitWorldEnterStarmap()
		{
			GameObject gameObject = base.sm.asteroidTarget.Get(base.smi);
			if (gameObject != null)
			{
				ClusterGridEntity component = gameObject.GetComponent<ClusterGridEntity>();
				if (component != null)
				{
					GameObject gameObject2 = GameUtil.KInstantiate(Assets.GetPrefab("ClusterMapLongRangeMissile"), Grid.SceneLayer.NoLayer, null, 0);
					gameObject2.SetActive(true);
					gameObject2.GetSMI<ClusterMapLongRangeMissile.StatesInstance>().Setup(this.myLocation, component);
				}
				else
				{
					gameObject.Trigger(-2056344675, MissileLongRangeConfig.DamageEventPayload.sharedInstance);
				}
			}
			Util.KDestroyGameObject(base.gameObject);
		}

		// Token: 0x0400732C RID: 29484
		public KBatchedAnimController animController;

		// Token: 0x0400732D RID: 29485
		[Serialize]
		private float launchSpeed;

		// Token: 0x0400732E RID: 29486
		public GameObject smokeTrailFX;

		// Token: 0x0400732F RID: 29487
		private WorldContainer myWorld;

		// Token: 0x04007330 RID: 29488
		[Serialize]
		private AxialI myLocation;

		// Token: 0x04007331 RID: 29489
		[Serialize]
		private int myWorldId = -1;

		// Token: 0x04007332 RID: 29490
		[Serialize]
		private Ref<KPrefabID> launchedTarget = new Ref<KPrefabID>();
	}
}
