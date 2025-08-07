using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B27 RID: 2855
[AddComponentMenu("KMonoBehaviour/scripts/ArtifactPOIConfigurator")]
public class ArtifactPOIConfigurator : KMonoBehaviour
{
	// Token: 0x06005445 RID: 21573 RVA: 0x001EA4E0 File Offset: 0x001E86E0
	public static ArtifactPOIConfigurator.ArtifactPOIType FindType(HashedString typeId)
	{
		ArtifactPOIConfigurator.ArtifactPOIType artifactPOIType = null;
		if (typeId != HashedString.Invalid)
		{
			artifactPOIType = ArtifactPOIConfigurator._poiTypes.Find((ArtifactPOIConfigurator.ArtifactPOIType t) => t.id == typeId);
		}
		if (artifactPOIType == null)
		{
			global::Debug.LogError(string.Format("Tried finding a harvestable poi with id {0} but it doesn't exist!", typeId.ToString()));
		}
		return artifactPOIType;
	}

	// Token: 0x06005446 RID: 21574 RVA: 0x001EA549 File Offset: 0x001E8749
	public ArtifactPOIConfigurator.ArtifactPOIInstanceConfiguration MakeConfiguration()
	{
		return this.CreateRandomInstance(this.presetType, this.presetMin, this.presetMax);
	}

	// Token: 0x06005447 RID: 21575 RVA: 0x001EA564 File Offset: 0x001E8764
	private ArtifactPOIConfigurator.ArtifactPOIInstanceConfiguration CreateRandomInstance(HashedString typeId, float min, float max)
	{
		int globalWorldSeed = SaveLoader.Instance.clusterDetailSave.globalWorldSeed;
		ClusterGridEntity component = base.GetComponent<ClusterGridEntity>();
		Vector3 position = ClusterGrid.Instance.GetPosition(component);
		KRandom krandom = new KRandom(globalWorldSeed + (int)position.x + (int)position.y);
		return new ArtifactPOIConfigurator.ArtifactPOIInstanceConfiguration
		{
			typeId = typeId,
			rechargeRoll = this.Roll(krandom, min, max)
		};
	}

	// Token: 0x06005448 RID: 21576 RVA: 0x001EA5C4 File Offset: 0x001E87C4
	private float Roll(KRandom randomSource, float min, float max)
	{
		return (float)(randomSource.NextDouble() * (double)(max - min)) + min;
	}

	// Token: 0x040038AC RID: 14508
	private static List<ArtifactPOIConfigurator.ArtifactPOIType> _poiTypes;

	// Token: 0x040038AD RID: 14509
	public static ArtifactPOIConfigurator.ArtifactPOIType defaultArtifactPoiType = new ArtifactPOIConfigurator.ArtifactPOIType("HarvestablePOIArtifacts", null, false, 30000f, 60000f, DlcManager.EXPANSION1, null);

	// Token: 0x040038AE RID: 14510
	public HashedString presetType;

	// Token: 0x040038AF RID: 14511
	public float presetMin;

	// Token: 0x040038B0 RID: 14512
	public float presetMax = 1f;

	// Token: 0x02001C36 RID: 7222
	public class ArtifactPOIType : IHasDlcRestrictions
	{
		// Token: 0x0600AA00 RID: 43520 RVA: 0x003B9B42 File Offset: 0x003B7D42
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x0600AA01 RID: 43521 RVA: 0x003B9B4A File Offset: 0x003B7D4A
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x0600AA02 RID: 43522 RVA: 0x003B9B54 File Offset: 0x003B7D54
		public ArtifactPOIType(string id, string harvestableArtifactID = null, bool destroyOnHarvest = false, float poiRechargeTimeMin = 30000f, float poiRechargeTimeMax = 60000f, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			this.id = id;
			this.idHash = id;
			this.harvestableArtifactID = harvestableArtifactID;
			this.destroyOnHarvest = destroyOnHarvest;
			this.poiRechargeTimeMin = poiRechargeTimeMin;
			this.poiRechargeTimeMax = poiRechargeTimeMax;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
			if (ArtifactPOIConfigurator._poiTypes == null)
			{
				ArtifactPOIConfigurator._poiTypes = new List<ArtifactPOIConfigurator.ArtifactPOIType>();
			}
			ArtifactPOIConfigurator._poiTypes.Add(this);
		}

		// Token: 0x0600AA03 RID: 43523 RVA: 0x003B9BEC File Offset: 0x003B7DEC
		[Obsolete]
		public ArtifactPOIType(string id, string harvestableArtifactID = null, bool destroyOnHarvest = false, float poiRechargeTimeMin = 30000f, float poiRechargeTimeMax = 60000f, string dlcID = "EXPANSION1_ID")
		{
			this.id = id;
			this.idHash = id;
			this.harvestableArtifactID = harvestableArtifactID;
			this.destroyOnHarvest = destroyOnHarvest;
			this.poiRechargeTimeMin = poiRechargeTimeMin;
			this.poiRechargeTimeMax = poiRechargeTimeMax;
			this.dlcID = dlcID;
			if (ArtifactPOIConfigurator._poiTypes == null)
			{
				ArtifactPOIConfigurator._poiTypes = new List<ArtifactPOIConfigurator.ArtifactPOIType>();
			}
			ArtifactPOIConfigurator._poiTypes.Add(this);
		}

		// Token: 0x04008571 RID: 34161
		public string id;

		// Token: 0x04008572 RID: 34162
		public HashedString idHash;

		// Token: 0x04008573 RID: 34163
		public string harvestableArtifactID;

		// Token: 0x04008574 RID: 34164
		public bool destroyOnHarvest;

		// Token: 0x04008575 RID: 34165
		public float poiRechargeTimeMin;

		// Token: 0x04008576 RID: 34166
		public float poiRechargeTimeMax;

		// Token: 0x04008577 RID: 34167
		[Obsolete]
		public string dlcID;

		// Token: 0x04008578 RID: 34168
		public string[] requiredDlcIds;

		// Token: 0x04008579 RID: 34169
		public string[] forbiddenDlcIds;

		// Token: 0x0400857A RID: 34170
		public List<string> orbitalObject = new List<string> { Db.Get().OrbitalTypeCategories.gravitas.Id };
	}

	// Token: 0x02001C37 RID: 7223
	[Serializable]
	public class ArtifactPOIInstanceConfiguration
	{
		// Token: 0x0600AA04 RID: 43524 RVA: 0x003B9C7C File Offset: 0x003B7E7C
		private void Init()
		{
			if (this.didInit)
			{
				return;
			}
			this.didInit = true;
			this.poiRechargeTime = MathUtil.ReRange(this.rechargeRoll, 0f, 1f, this.poiType.poiRechargeTimeMin, this.poiType.poiRechargeTimeMax);
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600AA05 RID: 43525 RVA: 0x003B9CCA File Offset: 0x003B7ECA
		public ArtifactPOIConfigurator.ArtifactPOIType poiType
		{
			get
			{
				return ArtifactPOIConfigurator.FindType(this.typeId);
			}
		}

		// Token: 0x0600AA06 RID: 43526 RVA: 0x003B9CD7 File Offset: 0x003B7ED7
		public bool DestroyOnHarvest()
		{
			this.Init();
			return this.poiType.destroyOnHarvest;
		}

		// Token: 0x0600AA07 RID: 43527 RVA: 0x003B9CEA File Offset: 0x003B7EEA
		public string GetArtifactID()
		{
			this.Init();
			return this.poiType.harvestableArtifactID;
		}

		// Token: 0x0600AA08 RID: 43528 RVA: 0x003B9CFD File Offset: 0x003B7EFD
		public float GetRechargeTime()
		{
			this.Init();
			return this.poiRechargeTime;
		}

		// Token: 0x0400857B RID: 34171
		public HashedString typeId;

		// Token: 0x0400857C RID: 34172
		private bool didInit;

		// Token: 0x0400857D RID: 34173
		public float rechargeRoll;

		// Token: 0x0400857E RID: 34174
		private float poiRechargeTime;
	}
}
