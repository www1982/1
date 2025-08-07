using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000B28 RID: 2856
[AddComponentMenu("KMonoBehaviour/scripts/ArtifactPOIStates")]
public class ArtifactPOIStates : GameStateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>
{
	// Token: 0x0600544B RID: 21579 RVA: 0x001EA60C File Offset: 0x001E880C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.root.Enter(delegate(ArtifactPOIStates.Instance smi)
		{
			if (smi.configuration == null || smi.configuration.typeId == HashedString.Invalid)
			{
				smi.configuration = smi.GetComponent<ArtifactPOIConfigurator>().MakeConfiguration();
				smi.PickNewArtifactToHarvest();
				smi.poiCharge = 1f;
			}
		});
		this.idle.ParamTransition<float>(this.poiCharge, this.recharging, (ArtifactPOIStates.Instance smi, float f) => f < 1f);
		this.recharging.ParamTransition<float>(this.poiCharge, this.idle, (ArtifactPOIStates.Instance smi, float f) => f >= 1f).EventHandler(GameHashes.NewDay, (ArtifactPOIStates.Instance smi) => GameClock.Instance, delegate(ArtifactPOIStates.Instance smi)
		{
			smi.RechargePOI(600f);
		});
	}

	// Token: 0x040038B1 RID: 14513
	public GameStateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.State idle;

	// Token: 0x040038B2 RID: 14514
	public GameStateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.State recharging;

	// Token: 0x040038B3 RID: 14515
	public StateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.FloatParameter poiCharge = new StateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.FloatParameter(1f);

	// Token: 0x02001C39 RID: 7225
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001C3A RID: 7226
	public new class Instance : GameStateMachine<ArtifactPOIStates, ArtifactPOIStates.Instance, IStateMachineTarget, ArtifactPOIStates.Def>.GameInstance, IGameObjectEffectDescriptor
	{
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600AA0D RID: 43533 RVA: 0x003B9D3B File Offset: 0x003B7F3B
		// (set) Token: 0x0600AA0E RID: 43534 RVA: 0x003B9D43 File Offset: 0x003B7F43
		public float poiCharge
		{
			get
			{
				return this._poiCharge;
			}
			set
			{
				this._poiCharge = value;
				base.smi.sm.poiCharge.Set(value, base.smi, false);
			}
		}

		// Token: 0x0600AA0F RID: 43535 RVA: 0x003B9D6A File Offset: 0x003B7F6A
		public Instance(IStateMachineTarget target, ArtifactPOIStates.Def def)
			: base(target, def)
		{
		}

		// Token: 0x0600AA10 RID: 43536 RVA: 0x003B9D74 File Offset: 0x003B7F74
		public void PickNewArtifactToHarvest()
		{
			if (this.numHarvests <= 0 && !string.IsNullOrEmpty(this.configuration.GetArtifactID()))
			{
				this.artifactToHarvest = this.configuration.GetArtifactID();
				ArtifactSelector.Instance.ReserveArtifactID(this.artifactToHarvest, ArtifactType.Any);
				return;
			}
			this.artifactToHarvest = ArtifactSelector.Instance.GetUniqueArtifactID(ArtifactType.Space);
		}

		// Token: 0x0600AA11 RID: 43537 RVA: 0x003B9DD0 File Offset: 0x003B7FD0
		public string GetArtifactToHarvest()
		{
			if (this.CanHarvestArtifact())
			{
				if (string.IsNullOrEmpty(this.artifactToHarvest))
				{
					this.PickNewArtifactToHarvest();
				}
				return this.artifactToHarvest;
			}
			return null;
		}

		// Token: 0x0600AA12 RID: 43538 RVA: 0x003B9DF5 File Offset: 0x003B7FF5
		public void HarvestArtifact()
		{
			if (this.CanHarvestArtifact())
			{
				this.numHarvests++;
				this.poiCharge = 0f;
				this.artifactToHarvest = null;
				this.PickNewArtifactToHarvest();
			}
		}

		// Token: 0x0600AA13 RID: 43539 RVA: 0x003B9E28 File Offset: 0x003B8028
		public void RechargePOI(float dt)
		{
			float num = dt / this.configuration.GetRechargeTime();
			this.DeltaPOICharge(num);
		}

		// Token: 0x0600AA14 RID: 43540 RVA: 0x003B9E4A File Offset: 0x003B804A
		public float RechargeTimeRemaining()
		{
			return (float)Mathf.CeilToInt((this.configuration.GetRechargeTime() - this.configuration.GetRechargeTime() * this.poiCharge) / 600f) * 600f;
		}

		// Token: 0x0600AA15 RID: 43541 RVA: 0x003B9E7C File Offset: 0x003B807C
		public void DeltaPOICharge(float delta)
		{
			this.poiCharge += delta;
			this.poiCharge = Mathf.Min(1f, this.poiCharge);
		}

		// Token: 0x0600AA16 RID: 43542 RVA: 0x003B9EA2 File Offset: 0x003B80A2
		public bool CanHarvestArtifact()
		{
			return this.poiCharge >= 1f;
		}

		// Token: 0x0600AA17 RID: 43543 RVA: 0x003B9EB4 File Offset: 0x003B80B4
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			return new List<Descriptor>();
		}

		// Token: 0x04008580 RID: 34176
		[Serialize]
		public ArtifactPOIConfigurator.ArtifactPOIInstanceConfiguration configuration;

		// Token: 0x04008581 RID: 34177
		[Serialize]
		private float _poiCharge;

		// Token: 0x04008582 RID: 34178
		[Serialize]
		public string artifactToHarvest;

		// Token: 0x04008583 RID: 34179
		[Serialize]
		private int numHarvests;
	}
}
