using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008BE RID: 2238
[AddComponentMenu("KMonoBehaviour/scripts/DietManager")]
public class DietManager : KMonoBehaviour
{
	// Token: 0x06003DF5 RID: 15861 RVA: 0x0015A87A File Offset: 0x00158A7A
	public static void DestroyInstance()
	{
		DietManager.Instance = null;
	}

	// Token: 0x06003DF6 RID: 15862 RVA: 0x0015A882 File Offset: 0x00158A82
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.diets = DietManager.CollectSaveDiets(null);
		DietManager.Instance = this;
	}

	// Token: 0x06003DF7 RID: 15863 RVA: 0x0015A89C File Offset: 0x00158A9C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (Tag tag in DiscoveredResources.Instance.GetDiscovered())
		{
			this.Discover(tag);
		}
		foreach (KeyValuePair<Tag, Diet> keyValuePair in this.diets)
		{
			Diet.Info[] infos = keyValuePair.Value.infos;
			for (int i = 0; i < infos.Length; i++)
			{
				foreach (Tag tag2 in infos[i].consumedTags)
				{
					if (Assets.GetPrefab(tag2) == null)
					{
						global::Debug.LogError(string.Format("Could not find prefab {0}, required by diet for {1}", tag2, keyValuePair.Key));
					}
				}
			}
		}
		DiscoveredResources.Instance.OnDiscover += this.OnWorldInventoryDiscover;
	}

	// Token: 0x06003DF8 RID: 15864 RVA: 0x0015A9E4 File Offset: 0x00158BE4
	private void Discover(Tag tag)
	{
		foreach (KeyValuePair<Tag, Diet> keyValuePair in this.diets)
		{
			if (keyValuePair.Value.GetDietInfo(tag) != null)
			{
				DiscoveredResources.Instance.Discover(tag, keyValuePair.Key);
			}
		}
	}

	// Token: 0x06003DF9 RID: 15865 RVA: 0x0015AA54 File Offset: 0x00158C54
	private void OnWorldInventoryDiscover(Tag category_tag, Tag tag)
	{
		this.Discover(tag);
	}

	// Token: 0x06003DFA RID: 15866 RVA: 0x0015AA60 File Offset: 0x00158C60
	public static Dictionary<Tag, Diet> CollectDiets(Tag[] target_species)
	{
		Dictionary<Tag, Diet> dictionary = new Dictionary<Tag, Diet>();
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			CreatureCalorieMonitor.Def def = kprefabID.GetDef<CreatureCalorieMonitor.Def>();
			BeehiveCalorieMonitor.Def def2 = kprefabID.GetDef<BeehiveCalorieMonitor.Def>();
			Diet diet = null;
			if (def != null)
			{
				diet = def.diet;
			}
			else if (def2 != null)
			{
				diet = def2.diet;
			}
			if (diet != null && (target_species == null || Array.IndexOf<Tag>(target_species, kprefabID.GetComponent<CreatureBrain>().species) >= 0))
			{
				dictionary[kprefabID.PrefabTag] = diet;
			}
		}
		return dictionary;
	}

	// Token: 0x06003DFB RID: 15867 RVA: 0x0015AB08 File Offset: 0x00158D08
	public static Dictionary<Tag, Diet> CollectSaveDiets(Tag[] target_species)
	{
		Dictionary<Tag, Diet> dictionary = new Dictionary<Tag, Diet>();
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			CreatureCalorieMonitor.Def def = kprefabID.GetDef<CreatureCalorieMonitor.Def>();
			BeehiveCalorieMonitor.Def def2 = kprefabID.GetDef<BeehiveCalorieMonitor.Def>();
			Diet diet = null;
			if (def != null)
			{
				diet = def.diet;
			}
			else if (def2 != null)
			{
				diet = def2.diet;
			}
			if (diet != null && (target_species == null || Array.IndexOf<Tag>(target_species, kprefabID.GetComponent<CreatureBrain>().species) >= 0))
			{
				dictionary[kprefabID.PrefabTag] = new Diet(diet);
				dictionary[kprefabID.PrefabTag].FilterDLC();
			}
		}
		return dictionary;
	}

	// Token: 0x06003DFC RID: 15868 RVA: 0x0015ABC8 File Offset: 0x00158DC8
	public Diet GetPrefabDiet(GameObject owner)
	{
		Diet diet;
		if (this.diets.TryGetValue(owner.GetComponent<KPrefabID>().PrefabTag, out diet))
		{
			return diet;
		}
		return null;
	}

	// Token: 0x04002624 RID: 9764
	private Dictionary<Tag, Diet> diets;

	// Token: 0x04002625 RID: 9765
	public static DietManager Instance;
}
