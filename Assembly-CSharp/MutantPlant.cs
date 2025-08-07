using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020009FE RID: 2558
[SerializationConfig(MemberSerialization.OptIn)]
public class MutantPlant : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x17000523 RID: 1315
	// (get) Token: 0x06004A86 RID: 19078 RVA: 0x001AFD3E File Offset: 0x001ADF3E
	public List<string> MutationIDs
	{
		get
		{
			return this.mutationIDs;
		}
	}

	// Token: 0x17000524 RID: 1316
	// (get) Token: 0x06004A87 RID: 19079 RVA: 0x001AFD46 File Offset: 0x001ADF46
	public bool IsOriginal
	{
		get
		{
			return this.mutationIDs == null || this.mutationIDs.Count == 0;
		}
	}

	// Token: 0x17000525 RID: 1317
	// (get) Token: 0x06004A88 RID: 19080 RVA: 0x001AFD60 File Offset: 0x001ADF60
	public bool IsIdentified
	{
		get
		{
			return this.analyzed && PlantSubSpeciesCatalog.Instance.IsSubSpeciesIdentified(this.SubSpeciesID);
		}
	}

	// Token: 0x17000526 RID: 1318
	// (get) Token: 0x06004A89 RID: 19081 RVA: 0x001AFD7C File Offset: 0x001ADF7C
	// (set) Token: 0x06004A8A RID: 19082 RVA: 0x001AFD9F File Offset: 0x001ADF9F
	public Tag SpeciesID
	{
		get
		{
			global::Debug.Assert(this.speciesID != null, "Ack, forgot to configure the species ID for this mutantPlant!");
			return this.speciesID;
		}
		set
		{
			this.speciesID = value;
		}
	}

	// Token: 0x17000527 RID: 1319
	// (get) Token: 0x06004A8B RID: 19083 RVA: 0x001AFDA8 File Offset: 0x001ADFA8
	public Tag SubSpeciesID
	{
		get
		{
			if (this.cachedSubspeciesID == null)
			{
				this.cachedSubspeciesID = this.GetSubSpeciesInfo().ID;
			}
			return this.cachedSubspeciesID;
		}
	}

	// Token: 0x06004A8C RID: 19084 RVA: 0x001AFDD4 File Offset: 0x001ADFD4
	protected override void OnPrefabInit()
	{
		base.Subscribe<MutantPlant>(-2064133523, MutantPlant.OnAbsorbDelegate);
		base.Subscribe<MutantPlant>(1335436905, MutantPlant.OnSplitFromChunkDelegate);
	}

	// Token: 0x06004A8D RID: 19085 RVA: 0x001AFDF8 File Offset: 0x001ADFF8
	protected override void OnSpawn()
	{
		if (this.IsOriginal || this.HasTag(GameTags.Plant))
		{
			this.analyzed = true;
		}
		if (!this.IsOriginal)
		{
			this.AddTag(GameTags.MutatedSeed);
		}
		this.AddTag(this.SubSpeciesID);
		Components.MutantPlants.Add(this);
		base.OnSpawn();
		this.ApplyMutations();
		this.UpdateNameAndTags();
		PlantSubSpeciesCatalog.Instance.DiscoverSubSpecies(this.GetSubSpeciesInfo(), this);
	}

	// Token: 0x06004A8E RID: 19086 RVA: 0x001AFE6E File Offset: 0x001AE06E
	protected override void OnCleanUp()
	{
		Components.MutantPlants.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06004A8F RID: 19087 RVA: 0x001AFE84 File Offset: 0x001AE084
	private void OnAbsorb(object data)
	{
		MutantPlant component = (data as Pickupable).GetComponent<MutantPlant>();
		global::Debug.Assert(component != null && this.SubSpeciesID == component.SubSpeciesID, "Two seeds of different subspecies just absorbed!");
	}

	// Token: 0x06004A90 RID: 19088 RVA: 0x001AFEC4 File Offset: 0x001AE0C4
	private void OnSplitFromChunk(object data)
	{
		MutantPlant component = (data as Pickupable).GetComponent<MutantPlant>();
		if (component != null)
		{
			component.CopyMutationsTo(this);
		}
	}

	// Token: 0x06004A91 RID: 19089 RVA: 0x001AFEF0 File Offset: 0x001AE0F0
	public void Mutate()
	{
		List<string> list = ((this.mutationIDs != null) ? new List<string>(this.mutationIDs) : new List<string>());
		while (list.Count >= 1 && list.Count > 0)
		{
			list.RemoveAt(global::UnityEngine.Random.Range(0, list.Count));
		}
		list.Add(Db.Get().PlantMutations.GetRandomMutation(this.PrefabID().Name).Id);
		this.SetSubSpecies(list);
	}

	// Token: 0x06004A92 RID: 19090 RVA: 0x001AFF6D File Offset: 0x001AE16D
	public void Analyze()
	{
		this.analyzed = true;
		this.UpdateNameAndTags();
	}

	// Token: 0x06004A93 RID: 19091 RVA: 0x001AFF7C File Offset: 0x001AE17C
	public void ApplyMutations()
	{
		if (this.IsOriginal)
		{
			return;
		}
		foreach (string text in this.mutationIDs)
		{
			Db.Get().PlantMutations.Get(text).ApplyTo(this);
		}
	}

	// Token: 0x06004A94 RID: 19092 RVA: 0x001AFFE8 File Offset: 0x001AE1E8
	public void DummySetSubspecies(List<string> mutations)
	{
		this.mutationIDs = mutations;
	}

	// Token: 0x06004A95 RID: 19093 RVA: 0x001AFFF1 File Offset: 0x001AE1F1
	public void SetSubSpecies(List<string> mutations)
	{
		if (base.gameObject.HasTag(this.SubSpeciesID))
		{
			base.gameObject.RemoveTag(this.SubSpeciesID);
		}
		this.cachedSubspeciesID = Tag.Invalid;
		this.mutationIDs = mutations;
		this.UpdateNameAndTags();
	}

	// Token: 0x06004A96 RID: 19094 RVA: 0x001B002F File Offset: 0x001AE22F
	public PlantSubSpeciesCatalog.SubSpeciesInfo GetSubSpeciesInfo()
	{
		return new PlantSubSpeciesCatalog.SubSpeciesInfo(this.SpeciesID, this.mutationIDs);
	}

	// Token: 0x06004A97 RID: 19095 RVA: 0x001B0042 File Offset: 0x001AE242
	public void CopyMutationsTo(MutantPlant target)
	{
		target.SetSubSpecies(this.mutationIDs);
		target.analyzed = this.analyzed;
	}

	// Token: 0x06004A98 RID: 19096 RVA: 0x001B005C File Offset: 0x001AE25C
	public void UpdateNameAndTags()
	{
		bool flag = !base.IsInitialized() || this.IsIdentified;
		bool flag2 = PlantSubSpeciesCatalog.Instance == null || PlantSubSpeciesCatalog.Instance.GetAllSubSpeciesForSpecies(this.SpeciesID).Count == 1;
		KPrefabID component = base.GetComponent<KPrefabID>();
		component.AddTag(this.SubSpeciesID, false);
		component.SetTag(GameTags.UnidentifiedSeed, !flag);
		base.gameObject.name = component.PrefabTag.ToString() + " (" + this.SubSpeciesID.ToString() + ")";
		base.GetComponent<KSelectable>().SetName(this.GetSubSpeciesInfo().GetNameWithMutations(component.PrefabTag.ProperName(), flag, flag2));
		KSelectable component2 = base.GetComponent<KSelectable>();
		foreach (Guid guid in this.statusItemHandles)
		{
			component2.RemoveStatusItem(guid, false);
		}
		this.statusItemHandles.Clear();
		if (!flag2)
		{
			if (this.IsOriginal)
			{
				this.statusItemHandles.Add(component2.AddStatusItem(Db.Get().CreatureStatusItems.OriginalPlantMutation, null));
				return;
			}
			if (!flag)
			{
				this.statusItemHandles.Add(component2.AddStatusItem(Db.Get().CreatureStatusItems.UnknownMutation, null));
				return;
			}
			foreach (string text in this.mutationIDs)
			{
				this.statusItemHandles.Add(component2.AddStatusItem(Db.Get().CreatureStatusItems.SpecificPlantMutation, Db.Get().PlantMutations.Get(text)));
			}
		}
	}

	// Token: 0x06004A99 RID: 19097 RVA: 0x001B0250 File Offset: 0x001AE450
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		if (this.IsOriginal)
		{
			return null;
		}
		List<Descriptor> list = new List<Descriptor>();
		foreach (string text in this.mutationIDs)
		{
			Db.Get().PlantMutations.Get(text).GetDescriptors(ref list, go);
		}
		return list;
	}

	// Token: 0x06004A9A RID: 19098 RVA: 0x001B02C8 File Offset: 0x001AE4C8
	public List<string> GetSoundEvents()
	{
		List<string> list = new List<string>();
		if (this.mutationIDs != null)
		{
			foreach (string text in this.mutationIDs)
			{
				PlantMutation plantMutation = Db.Get().PlantMutations.Get(text);
				list.AddRange(plantMutation.AdditionalSoundEvents);
			}
		}
		return list;
	}

	// Token: 0x0400313E RID: 12606
	[Serialize]
	private bool analyzed;

	// Token: 0x0400313F RID: 12607
	[Serialize]
	private List<string> mutationIDs;

	// Token: 0x04003140 RID: 12608
	private List<Guid> statusItemHandles = new List<Guid>();

	// Token: 0x04003141 RID: 12609
	private const int MAX_MUTATIONS = 1;

	// Token: 0x04003142 RID: 12610
	[SerializeField]
	private Tag speciesID;

	// Token: 0x04003143 RID: 12611
	private Tag cachedSubspeciesID;

	// Token: 0x04003144 RID: 12612
	private static readonly EventSystem.IntraObjectHandler<MutantPlant> OnAbsorbDelegate = new EventSystem.IntraObjectHandler<MutantPlant>(delegate(MutantPlant component, object data)
	{
		component.OnAbsorb(data);
	});

	// Token: 0x04003145 RID: 12613
	private static readonly EventSystem.IntraObjectHandler<MutantPlant> OnSplitFromChunkDelegate = new EventSystem.IntraObjectHandler<MutantPlant>(delegate(MutantPlant component, object data)
	{
		component.OnSplitFromChunk(data);
	});
}
