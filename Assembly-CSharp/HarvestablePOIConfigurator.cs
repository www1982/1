using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B40 RID: 2880
[AddComponentMenu("KMonoBehaviour/scripts/HarvestablePOIConfigurator")]
public class HarvestablePOIConfigurator : KMonoBehaviour
{
	// Token: 0x060055AF RID: 21935 RVA: 0x001F1CF0 File Offset: 0x001EFEF0
	public static HarvestablePOIConfigurator.HarvestablePOIType FindType(HashedString typeId)
	{
		HarvestablePOIConfigurator.HarvestablePOIType harvestablePOIType = null;
		if (typeId != HashedString.Invalid)
		{
			harvestablePOIType = HarvestablePOIConfigurator._poiTypes.Find((HarvestablePOIConfigurator.HarvestablePOIType t) => t.id == typeId);
		}
		if (harvestablePOIType == null)
		{
			global::Debug.LogError(string.Format("Tried finding a harvestable poi with id {0} but it doesn't exist!", typeId.ToString()));
		}
		return harvestablePOIType;
	}

	// Token: 0x060055B0 RID: 21936 RVA: 0x001F1D59 File Offset: 0x001EFF59
	public HarvestablePOIConfigurator.HarvestablePOIInstanceConfiguration MakeConfiguration()
	{
		return this.CreateRandomInstance(this.presetType, this.presetMin, this.presetMax);
	}

	// Token: 0x060055B1 RID: 21937 RVA: 0x001F1D74 File Offset: 0x001EFF74
	private HarvestablePOIConfigurator.HarvestablePOIInstanceConfiguration CreateRandomInstance(HashedString typeId, float min, float max)
	{
		int globalWorldSeed = SaveLoader.Instance.clusterDetailSave.globalWorldSeed;
		ClusterGridEntity component = base.GetComponent<ClusterGridEntity>();
		Vector3 position = ClusterGrid.Instance.GetPosition(component);
		KRandom krandom = new KRandom(globalWorldSeed + (int)position.x + (int)position.y);
		return new HarvestablePOIConfigurator.HarvestablePOIInstanceConfiguration
		{
			typeId = typeId,
			capacityRoll = this.Roll(krandom, min, max),
			rechargeRoll = this.Roll(krandom, min, max)
		};
	}

	// Token: 0x060055B2 RID: 21938 RVA: 0x001F1DE3 File Offset: 0x001EFFE3
	private float Roll(KRandom randomSource, float min, float max)
	{
		return (float)(randomSource.NextDouble() * (double)(max - min)) + min;
	}

	// Token: 0x04003936 RID: 14646
	private static List<HarvestablePOIConfigurator.HarvestablePOIType> _poiTypes;

	// Token: 0x04003937 RID: 14647
	public HashedString presetType;

	// Token: 0x04003938 RID: 14648
	public float presetMin;

	// Token: 0x04003939 RID: 14649
	public float presetMax = 1f;

	// Token: 0x02001C5C RID: 7260
	public class HarvestablePOIType : IHasDlcRestrictions
	{
		// Token: 0x0600AAA8 RID: 43688 RVA: 0x003BB379 File Offset: 0x003B9579
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x0600AAA9 RID: 43689 RVA: 0x003BB381 File Offset: 0x003B9581
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x0600AAAA RID: 43690 RVA: 0x003BB38C File Offset: 0x003B958C
		public HarvestablePOIType(string id, Dictionary<SimHashes, float> harvestableElements, float poiCapacityMin = 54000f, float poiCapacityMax = 81000f, float poiRechargeMin = 30000f, float poiRechargeMax = 60000f, bool canProvideArtifacts = true, List<string> orbitalObject = null, int maxNumOrbitingObjects = 20, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			this.id = id;
			this.idHash = id;
			this.harvestableElements = harvestableElements;
			this.poiCapacityMin = poiCapacityMin;
			this.poiCapacityMax = poiCapacityMax;
			this.poiRechargeMin = poiRechargeMin;
			this.poiRechargeMax = poiRechargeMax;
			this.canProvideArtifacts = canProvideArtifacts;
			this.orbitalObject = orbitalObject;
			this.maxNumOrbitingObjects = maxNumOrbitingObjects;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
			if (HarvestablePOIConfigurator._poiTypes == null)
			{
				HarvestablePOIConfigurator._poiTypes = new List<HarvestablePOIConfigurator.HarvestablePOIType>();
			}
			HarvestablePOIConfigurator._poiTypes.Add(this);
		}

		// Token: 0x0600AAAB RID: 43691 RVA: 0x003BB41C File Offset: 0x003B961C
		[Obsolete]
		public HarvestablePOIType(string id, Dictionary<SimHashes, float> harvestableElements, float poiCapacityMin = 54000f, float poiCapacityMax = 81000f, float poiRechargeMin = 30000f, float poiRechargeMax = 60000f, bool canProvideArtifacts = true, List<string> orbitalObject = null, int maxNumOrbitingObjects = 20, string dlcID = "EXPANSION1_ID")
			: this(id, harvestableElements, poiCapacityMin, poiCapacityMax, poiRechargeMin, poiRechargeMax, canProvideArtifacts, orbitalObject, maxNumOrbitingObjects, null, null)
		{
			this.requiredDlcIds = DlcManager.EXPANSION1;
		}

		// Token: 0x04008607 RID: 34311
		public string id;

		// Token: 0x04008608 RID: 34312
		public HashedString idHash;

		// Token: 0x04008609 RID: 34313
		public Dictionary<SimHashes, float> harvestableElements;

		// Token: 0x0400860A RID: 34314
		public float poiCapacityMin;

		// Token: 0x0400860B RID: 34315
		public float poiCapacityMax;

		// Token: 0x0400860C RID: 34316
		public float poiRechargeMin;

		// Token: 0x0400860D RID: 34317
		public float poiRechargeMax;

		// Token: 0x0400860E RID: 34318
		public bool canProvideArtifacts;

		// Token: 0x0400860F RID: 34319
		[Obsolete]
		public string dlcID;

		// Token: 0x04008610 RID: 34320
		public string[] requiredDlcIds;

		// Token: 0x04008611 RID: 34321
		public string[] forbiddenDlcIds;

		// Token: 0x04008612 RID: 34322
		public List<string> orbitalObject;

		// Token: 0x04008613 RID: 34323
		public int maxNumOrbitingObjects;
	}

	// Token: 0x02001C5D RID: 7261
	[Serializable]
	public class HarvestablePOIInstanceConfiguration
	{
		// Token: 0x0600AAAC RID: 43692 RVA: 0x003BB44C File Offset: 0x003B964C
		private void Init()
		{
			if (this.didInit)
			{
				return;
			}
			this.didInit = true;
			this.poiTotalCapacity = MathUtil.ReRange(this.capacityRoll, 0f, 1f, this.poiType.poiCapacityMin, this.poiType.poiCapacityMax);
			this.poiRecharge = MathUtil.ReRange(this.rechargeRoll, 0f, 1f, this.poiType.poiRechargeMin, this.poiType.poiRechargeMax);
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600AAAD RID: 43693 RVA: 0x003BB4CB File Offset: 0x003B96CB
		public HarvestablePOIConfigurator.HarvestablePOIType poiType
		{
			get
			{
				return HarvestablePOIConfigurator.FindType(this.typeId);
			}
		}

		// Token: 0x0600AAAE RID: 43694 RVA: 0x003BB4D8 File Offset: 0x003B96D8
		public Dictionary<SimHashes, float> GetElementsWithWeights()
		{
			this.Init();
			return this.poiType.harvestableElements;
		}

		// Token: 0x0600AAAF RID: 43695 RVA: 0x003BB4EB File Offset: 0x003B96EB
		public bool CanProvideArtifacts()
		{
			this.Init();
			return this.poiType.canProvideArtifacts;
		}

		// Token: 0x0600AAB0 RID: 43696 RVA: 0x003BB4FE File Offset: 0x003B96FE
		public float GetMaxCapacity()
		{
			this.Init();
			return this.poiTotalCapacity;
		}

		// Token: 0x0600AAB1 RID: 43697 RVA: 0x003BB50C File Offset: 0x003B970C
		public float GetRechargeTime()
		{
			this.Init();
			return this.poiRecharge;
		}

		// Token: 0x04008614 RID: 34324
		public HashedString typeId;

		// Token: 0x04008615 RID: 34325
		private bool didInit;

		// Token: 0x04008616 RID: 34326
		public float capacityRoll;

		// Token: 0x04008617 RID: 34327
		public float rechargeRoll;

		// Token: 0x04008618 RID: 34328
		private float poiTotalCapacity;

		// Token: 0x04008619 RID: 34329
		private float poiRecharge;
	}
}
