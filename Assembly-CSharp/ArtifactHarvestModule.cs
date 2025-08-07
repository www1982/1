using System;
using UnityEngine;

// Token: 0x02000B25 RID: 2853
public class ArtifactHarvestModule : GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>
{
	// Token: 0x0600543C RID: 21564 RVA: 0x001EA2E4 File Offset: 0x001E84E4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.grounded;
		this.root.Enter(delegate(ArtifactHarvestModule.StatesInstance smi)
		{
			smi.CheckIfCanHarvest();
		});
		this.grounded.TagTransition(GameTags.RocketNotOnGround, this.not_grounded, false);
		this.not_grounded.DefaultState(this.not_grounded.not_harvesting).EventHandler(GameHashes.ClusterLocationChanged, (ArtifactHarvestModule.StatesInstance smi) => Game.Instance, delegate(ArtifactHarvestModule.StatesInstance smi)
		{
			smi.CheckIfCanHarvest();
		}).EventHandler(GameHashes.OnStorageChange, delegate(ArtifactHarvestModule.StatesInstance smi)
		{
			smi.CheckIfCanHarvest();
		})
			.TagTransition(GameTags.RocketNotOnGround, this.grounded, true);
		this.not_grounded.not_harvesting.PlayAnim("loaded").ParamTransition<bool>(this.canHarvest, this.not_grounded.harvesting, GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.IsTrue);
		this.not_grounded.harvesting.PlayAnim("deploying").Update(delegate(ArtifactHarvestModule.StatesInstance smi, float dt)
		{
			smi.HarvestFromPOI(dt);
		}, UpdateRate.SIM_4000ms, false).ParamTransition<bool>(this.canHarvest, this.not_grounded.not_harvesting, GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.IsFalse);
	}

	// Token: 0x040038A7 RID: 14503
	public StateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.BoolParameter canHarvest;

	// Token: 0x040038A8 RID: 14504
	public GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.State grounded;

	// Token: 0x040038A9 RID: 14505
	public ArtifactHarvestModule.NotGroundedStates not_grounded;

	// Token: 0x02001C32 RID: 7218
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001C33 RID: 7219
	public class NotGroundedStates : GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.State
	{
		// Token: 0x04008567 RID: 34151
		public GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.State not_harvesting;

		// Token: 0x04008568 RID: 34152
		public GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.State harvesting;
	}

	// Token: 0x02001C34 RID: 7220
	public class StatesInstance : GameStateMachine<ArtifactHarvestModule, ArtifactHarvestModule.StatesInstance, IStateMachineTarget, ArtifactHarvestModule.Def>.GameInstance
	{
		// Token: 0x0600A9F6 RID: 43510 RVA: 0x003B998E File Offset: 0x003B7B8E
		public StatesInstance(IStateMachineTarget master, ArtifactHarvestModule.Def def)
			: base(master, def)
		{
		}

		// Token: 0x0600A9F7 RID: 43511 RVA: 0x003B9998 File Offset: 0x003B7B98
		public void HarvestFromPOI(float dt)
		{
			ClusterGridEntity poiatCurrentLocation = base.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().GetPOIAtCurrentLocation();
			if (poiatCurrentLocation.IsNullOrDestroyed())
			{
				return;
			}
			ArtifactPOIStates.Instance smi = poiatCurrentLocation.GetSMI<ArtifactPOIStates.Instance>();
			if ((poiatCurrentLocation.GetComponent<ArtifactPOIClusterGridEntity>() || poiatCurrentLocation.GetComponent<HarvestablePOIClusterGridEntity>()) && !smi.IsNullOrDestroyed())
			{
				bool flag = false;
				string artifactToHarvest = smi.GetArtifactToHarvest();
				if (artifactToHarvest != null)
				{
					GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(artifactToHarvest), base.transform.position);
					gameObject.SetActive(true);
					this.receptacle.ForceDeposit(gameObject);
					this.storage.Store(gameObject, false, false, true, false);
					smi.HarvestArtifact();
					if (smi.configuration.DestroyOnHarvest())
					{
						flag = true;
					}
					if (flag)
					{
						poiatCurrentLocation.gameObject.DeleteObject();
					}
				}
			}
		}

		// Token: 0x0600A9F8 RID: 43512 RVA: 0x003B9A60 File Offset: 0x003B7C60
		public bool CheckIfCanHarvest()
		{
			Clustercraft component = base.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>();
			if (component == null)
			{
				return false;
			}
			ClusterGridEntity poiatCurrentLocation = component.GetPOIAtCurrentLocation();
			if (poiatCurrentLocation != null && (poiatCurrentLocation.GetComponent<ArtifactPOIClusterGridEntity>() || poiatCurrentLocation.GetComponent<HarvestablePOIClusterGridEntity>()))
			{
				ArtifactPOIStates.Instance smi = poiatCurrentLocation.GetSMI<ArtifactPOIStates.Instance>();
				if (smi != null && smi.CanHarvestArtifact() && this.receptacle.Occupant == null)
				{
					base.sm.canHarvest.Set(true, this, false);
					return true;
				}
			}
			base.sm.canHarvest.Set(false, this, false);
			return false;
		}

		// Token: 0x04008569 RID: 34153
		[MyCmpReq]
		private Storage storage;

		// Token: 0x0400856A RID: 34154
		[MyCmpReq]
		private SingleEntityReceptacle receptacle;
	}
}
