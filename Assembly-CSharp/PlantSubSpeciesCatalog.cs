using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000A6A RID: 2666
[SerializationConfig(MemberSerialization.OptIn)]
public class PlantSubSpeciesCatalog : KMonoBehaviour
{
	// Token: 0x06004D1B RID: 19739 RVA: 0x001BE897 File Offset: 0x001BCA97
	public static void DestroyInstance()
	{
		PlantSubSpeciesCatalog.Instance = null;
	}

	// Token: 0x17000542 RID: 1346
	// (get) Token: 0x06004D1C RID: 19740 RVA: 0x001BE8A0 File Offset: 0x001BCAA0
	public bool AnyNonOriginalDiscovered
	{
		get
		{
			foreach (KeyValuePair<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> keyValuePair in this.discoveredSubspeciesBySpecies)
			{
				if (keyValuePair.Value.Find((PlantSubSpeciesCatalog.SubSpeciesInfo ss) => !ss.IsOriginal).IsValid)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06004D1D RID: 19741 RVA: 0x001BE928 File Offset: 0x001BCB28
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		PlantSubSpeciesCatalog.Instance = this;
	}

	// Token: 0x06004D1E RID: 19742 RVA: 0x001BE936 File Offset: 0x001BCB36
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.EnsureOriginalSubSpecies();
		this.RemoveInvalidMutantPlants();
	}

	// Token: 0x06004D1F RID: 19743 RVA: 0x001BE94A File Offset: 0x001BCB4A
	public List<Tag> GetAllDiscoveredSpecies()
	{
		return this.discoveredSubspeciesBySpecies.Keys.ToList<Tag>();
	}

	// Token: 0x06004D20 RID: 19744 RVA: 0x001BE95C File Offset: 0x001BCB5C
	public List<PlantSubSpeciesCatalog.SubSpeciesInfo> GetAllSubSpeciesForSpecies(Tag speciesID)
	{
		List<PlantSubSpeciesCatalog.SubSpeciesInfo> list;
		if (this.discoveredSubspeciesBySpecies.TryGetValue(speciesID, out list))
		{
			return list;
		}
		return null;
	}

	// Token: 0x06004D21 RID: 19745 RVA: 0x001BE97C File Offset: 0x001BCB7C
	public bool GetOriginalSubSpecies(Tag speciesID, out PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo)
	{
		if (!this.discoveredSubspeciesBySpecies.ContainsKey(speciesID))
		{
			subSpeciesInfo = default(PlantSubSpeciesCatalog.SubSpeciesInfo);
			return false;
		}
		subSpeciesInfo = this.discoveredSubspeciesBySpecies[speciesID].Find((PlantSubSpeciesCatalog.SubSpeciesInfo i) => i.IsOriginal);
		return true;
	}

	// Token: 0x06004D22 RID: 19746 RVA: 0x001BE9D8 File Offset: 0x001BCBD8
	public PlantSubSpeciesCatalog.SubSpeciesInfo GetSubSpecies(Tag speciesID, Tag subSpeciesID)
	{
		return this.discoveredSubspeciesBySpecies[speciesID].Find((PlantSubSpeciesCatalog.SubSpeciesInfo i) => i.ID == subSpeciesID);
	}

	// Token: 0x06004D23 RID: 19747 RVA: 0x001BEA10 File Offset: 0x001BCC10
	public PlantSubSpeciesCatalog.SubSpeciesInfo FindSubSpecies(Tag subSpeciesID)
	{
		Predicate<PlantSubSpeciesCatalog.SubSpeciesInfo> <>9__0;
		foreach (KeyValuePair<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> keyValuePair in this.discoveredSubspeciesBySpecies)
		{
			List<PlantSubSpeciesCatalog.SubSpeciesInfo> value = keyValuePair.Value;
			Predicate<PlantSubSpeciesCatalog.SubSpeciesInfo> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = (PlantSubSpeciesCatalog.SubSpeciesInfo i) => i.ID == subSpeciesID);
			}
			PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = value.Find(predicate);
			if (subSpeciesInfo.ID.IsValid)
			{
				return subSpeciesInfo;
			}
		}
		return default(PlantSubSpeciesCatalog.SubSpeciesInfo);
	}

	// Token: 0x06004D24 RID: 19748 RVA: 0x001BEAB8 File Offset: 0x001BCCB8
	public void DiscoverSubSpecies(PlantSubSpeciesCatalog.SubSpeciesInfo newSubSpeciesInfo, MutantPlant source)
	{
		if (!this.discoveredSubspeciesBySpecies[newSubSpeciesInfo.speciesID].Contains(newSubSpeciesInfo))
		{
			this.discoveredSubspeciesBySpecies[newSubSpeciesInfo.speciesID].Add(newSubSpeciesInfo);
			Notification notification = new Notification(MISC.NOTIFICATIONS.NEWMUTANTSEED.NAME, NotificationType.Good, new Func<List<Notification>, object, string>(this.NewSubspeciesTooltipCB), newSubSpeciesInfo, true, 0f, null, null, source.transform, true, false, false);
			base.gameObject.AddOrGet<Notifier>().Add(notification, "");
		}
	}

	// Token: 0x06004D25 RID: 19749 RVA: 0x001BEB40 File Offset: 0x001BCD40
	private string NewSubspeciesTooltipCB(List<Notification> notifications, object data)
	{
		PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo = (PlantSubSpeciesCatalog.SubSpeciesInfo)data;
		return MISC.NOTIFICATIONS.NEWMUTANTSEED.TOOLTIP.Replace("{Plant}", subSpeciesInfo.speciesID.ProperName());
	}

	// Token: 0x06004D26 RID: 19750 RVA: 0x001BEB70 File Offset: 0x001BCD70
	public void IdentifySubSpecies(Tag subSpeciesID)
	{
		if (this.identifiedSubSpecies.Add(subSpeciesID))
		{
			this.FindSubSpecies(subSpeciesID);
			foreach (object obj in Components.MutantPlants)
			{
				MutantPlant mutantPlant = (MutantPlant)obj;
				if (mutantPlant.HasTag(subSpeciesID))
				{
					mutantPlant.UpdateNameAndTags();
				}
			}
			GeneticAnalysisCompleteMessage geneticAnalysisCompleteMessage = new GeneticAnalysisCompleteMessage(subSpeciesID);
			Messenger.Instance.QueueMessage(geneticAnalysisCompleteMessage);
		}
	}

	// Token: 0x06004D27 RID: 19751 RVA: 0x001BEBF8 File Offset: 0x001BCDF8
	public bool IsSubSpeciesIdentified(Tag subSpeciesID)
	{
		return this.identifiedSubSpecies.Contains(subSpeciesID);
	}

	// Token: 0x06004D28 RID: 19752 RVA: 0x001BEC06 File Offset: 0x001BCE06
	public List<PlantSubSpeciesCatalog.SubSpeciesInfo> GetAllUnidentifiedSubSpecies(Tag speciesID)
	{
		return this.discoveredSubspeciesBySpecies[speciesID].FindAll((PlantSubSpeciesCatalog.SubSpeciesInfo ss) => !this.IsSubSpeciesIdentified(ss.ID));
	}

	// Token: 0x06004D29 RID: 19753 RVA: 0x001BEC28 File Offset: 0x001BCE28
	public bool IsValidPlantableSeed(Tag seedID, Tag subspeciesID)
	{
		if (!seedID.IsValid)
		{
			return false;
		}
		MutantPlant component = Assets.GetPrefab(seedID).GetComponent<MutantPlant>();
		if (component == null)
		{
			return !subspeciesID.IsValid;
		}
		List<PlantSubSpeciesCatalog.SubSpeciesInfo> allSubSpeciesForSpecies = PlantSubSpeciesCatalog.Instance.GetAllSubSpeciesForSpecies(component.SpeciesID);
		return allSubSpeciesForSpecies != null && allSubSpeciesForSpecies.FindIndex((PlantSubSpeciesCatalog.SubSpeciesInfo s) => s.ID == subspeciesID) != -1 && PlantSubSpeciesCatalog.Instance.IsSubSpeciesIdentified(subspeciesID);
	}

	// Token: 0x06004D2A RID: 19754 RVA: 0x001BECAC File Offset: 0x001BCEAC
	private void EnsureOriginalSubSpecies()
	{
		foreach (GameObject gameObject in Assets.GetPrefabsWithComponent<MutantPlant>())
		{
			MutantPlant component = gameObject.GetComponent<MutantPlant>();
			Tag speciesID = component.SpeciesID;
			if (!this.discoveredSubspeciesBySpecies.ContainsKey(speciesID))
			{
				this.discoveredSubspeciesBySpecies[speciesID] = new List<PlantSubSpeciesCatalog.SubSpeciesInfo>();
				this.discoveredSubspeciesBySpecies[speciesID].Add(component.GetSubSpeciesInfo());
			}
			this.identifiedSubSpecies.Add(component.SubSpeciesID);
		}
	}

	// Token: 0x06004D2B RID: 19755 RVA: 0x001BED4C File Offset: 0x001BCF4C
	private void RemoveInvalidMutantPlants()
	{
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> keyValuePair in this.discoveredSubspeciesBySpecies)
		{
			GameObject prefab = Assets.GetPrefab(keyValuePair.Key);
			if (prefab != null && prefab.GetComponent<MutantPlant>() == null)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (Tag tag in list)
		{
			foreach (PlantSubSpeciesCatalog.SubSpeciesInfo subSpeciesInfo in this.discoveredSubspeciesBySpecies[tag])
			{
				this.identifiedSubSpecies.Remove(subSpeciesInfo.ID);
			}
			this.discoveredSubspeciesBySpecies.Remove(tag);
		}
	}

	// Token: 0x04003345 RID: 13125
	public static PlantSubSpeciesCatalog Instance;

	// Token: 0x04003346 RID: 13126
	[Serialize]
	private Dictionary<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>> discoveredSubspeciesBySpecies = new Dictionary<Tag, List<PlantSubSpeciesCatalog.SubSpeciesInfo>>();

	// Token: 0x04003347 RID: 13127
	[Serialize]
	private HashSet<Tag> identifiedSubSpecies = new HashSet<Tag>();

	// Token: 0x02001B33 RID: 6963
	[Serializable]
	public struct SubSpeciesInfo : IEquatable<PlantSubSpeciesCatalog.SubSpeciesInfo>
	{
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600A684 RID: 42628 RVA: 0x003AD977 File Offset: 0x003ABB77
		public bool IsValid
		{
			get
			{
				return this.ID.IsValid;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x0600A685 RID: 42629 RVA: 0x003AD984 File Offset: 0x003ABB84
		public bool IsOriginal
		{
			get
			{
				return this.mutationIDs == null || this.mutationIDs.Count == 0;
			}
		}

		// Token: 0x0600A686 RID: 42630 RVA: 0x003AD99E File Offset: 0x003ABB9E
		public SubSpeciesInfo(Tag speciesID, List<string> mutationIDs)
		{
			this.speciesID = speciesID;
			this.mutationIDs = ((mutationIDs != null) ? new List<string>(mutationIDs) : new List<string>());
			this.ID = PlantSubSpeciesCatalog.SubSpeciesInfo.SubSpeciesIDFromMutations(speciesID, mutationIDs);
		}

		// Token: 0x0600A687 RID: 42631 RVA: 0x003AD9CC File Offset: 0x003ABBCC
		public static Tag SubSpeciesIDFromMutations(Tag speciesID, List<string> mutationIDs)
		{
			if (mutationIDs == null || mutationIDs.Count == 0)
			{
				Tag tag = speciesID;
				return tag.ToString() + "_Original";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(speciesID);
			foreach (string text in mutationIDs)
			{
				stringBuilder.Append("_");
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString().ToTag();
		}

		// Token: 0x0600A688 RID: 42632 RVA: 0x003ADA70 File Offset: 0x003ABC70
		public string GetMutationsNames()
		{
			if (this.mutationIDs == null || this.mutationIDs.Count == 0)
			{
				return CREATURES.PLANT_MUTATIONS.NONE.NAME;
			}
			return string.Join(", ", Db.Get().PlantMutations.GetNamesForMutations(this.mutationIDs));
		}

		// Token: 0x0600A689 RID: 42633 RVA: 0x003ADABC File Offset: 0x003ABCBC
		public string GetNameWithMutations(string properName, bool identified, bool cleanOriginal)
		{
			string text;
			if (this.mutationIDs == null || this.mutationIDs.Count == 0)
			{
				if (cleanOriginal)
				{
					text = properName;
				}
				else
				{
					text = CREATURES.PLANT_MUTATIONS.PLANT_NAME_FMT.Replace("{PlantName}", properName).Replace("{MutationList}", CREATURES.PLANT_MUTATIONS.NONE.NAME);
				}
			}
			else if (!identified)
			{
				text = CREATURES.PLANT_MUTATIONS.PLANT_NAME_FMT.Replace("{PlantName}", properName).Replace("{MutationList}", CREATURES.PLANT_MUTATIONS.UNIDENTIFIED);
			}
			else
			{
				text = CREATURES.PLANT_MUTATIONS.PLANT_NAME_FMT.Replace("{PlantName}", properName).Replace("{MutationList}", string.Join(", ", Db.Get().PlantMutations.GetNamesForMutations(this.mutationIDs)));
			}
			return text;
		}

		// Token: 0x0600A68A RID: 42634 RVA: 0x003ADB74 File Offset: 0x003ABD74
		public static bool operator ==(PlantSubSpeciesCatalog.SubSpeciesInfo obj1, PlantSubSpeciesCatalog.SubSpeciesInfo obj2)
		{
			return obj1.Equals(obj2);
		}

		// Token: 0x0600A68B RID: 42635 RVA: 0x003ADB7E File Offset: 0x003ABD7E
		public static bool operator !=(PlantSubSpeciesCatalog.SubSpeciesInfo obj1, PlantSubSpeciesCatalog.SubSpeciesInfo obj2)
		{
			return !(obj1 == obj2);
		}

		// Token: 0x0600A68C RID: 42636 RVA: 0x003ADB8A File Offset: 0x003ABD8A
		public override bool Equals(object other)
		{
			return other is PlantSubSpeciesCatalog.SubSpeciesInfo && this == (PlantSubSpeciesCatalog.SubSpeciesInfo)other;
		}

		// Token: 0x0600A68D RID: 42637 RVA: 0x003ADBA7 File Offset: 0x003ABDA7
		public bool Equals(PlantSubSpeciesCatalog.SubSpeciesInfo other)
		{
			return this.ID == other.ID;
		}

		// Token: 0x0600A68E RID: 42638 RVA: 0x003ADBBA File Offset: 0x003ABDBA
		public override int GetHashCode()
		{
			return this.ID.GetHashCode();
		}

		// Token: 0x0600A68F RID: 42639 RVA: 0x003ADBD0 File Offset: 0x003ABDD0
		public string GetMutationsTooltip()
		{
			if (this.mutationIDs == null || this.mutationIDs.Count == 0)
			{
				return CREATURES.STATUSITEMS.ORIGINALPLANTMUTATION.TOOLTIP;
			}
			if (!PlantSubSpeciesCatalog.Instance.IsSubSpeciesIdentified(this.ID))
			{
				return CREATURES.STATUSITEMS.UNKNOWNMUTATION.TOOLTIP;
			}
			string text = this.mutationIDs[0];
			PlantMutation plantMutation = Db.Get().PlantMutations.Get(text);
			return CREATURES.STATUSITEMS.SPECIFICPLANTMUTATION.TOOLTIP.Replace("{MutationName}", plantMutation.Name) + "\n" + plantMutation.GetTooltip();
		}

		// Token: 0x040081E1 RID: 33249
		public Tag speciesID;

		// Token: 0x040081E2 RID: 33250
		public Tag ID;

		// Token: 0x040081E3 RID: 33251
		public List<string> mutationIDs;

		// Token: 0x040081E4 RID: 33252
		private const string ORIGINAL_SUFFIX = "_Original";
	}
}
