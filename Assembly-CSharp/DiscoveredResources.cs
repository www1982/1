using System;
using System.Collections.Generic;
using System.Linq;
using KSerialization;
using UnityEngine;

// Token: 0x020008BF RID: 2239
[SerializationConfig(MemberSerialization.OptIn)]
public class DiscoveredResources : KMonoBehaviour, ISaveLoadable, ISim4000ms
{
	// Token: 0x06003DFE RID: 15870 RVA: 0x0015ABFA File Offset: 0x00158DFA
	public static void DestroyInstance()
	{
		DiscoveredResources.Instance = null;
	}

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x06003DFF RID: 15871 RVA: 0x0015AC04 File Offset: 0x00158E04
	// (remove) Token: 0x06003E00 RID: 15872 RVA: 0x0015AC3C File Offset: 0x00158E3C
	public event Action<Tag, Tag> OnDiscover;

	// Token: 0x06003E01 RID: 15873 RVA: 0x0015AC74 File Offset: 0x00158E74
	public void Discover(Tag tag, Tag categoryTag)
	{
		bool flag = this.Discovered.Add(tag);
		this.DiscoverCategory(categoryTag, tag);
		if (flag)
		{
			if (this.OnDiscover != null)
			{
				this.OnDiscover(categoryTag, tag);
			}
			if (!this.newDiscoveries.ContainsKey(tag))
			{
				this.newDiscoveries.Add(tag, (float)GameClock.Instance.GetCycle() + GameClock.Instance.GetCurrentCycleAsPercentage());
			}
		}
	}

	// Token: 0x06003E02 RID: 15874 RVA: 0x0015ACDC File Offset: 0x00158EDC
	public void Discover(Tag tag)
	{
		this.Discover(tag, DiscoveredResources.GetCategoryForEntity(Assets.GetPrefab(tag).GetComponent<KPrefabID>()));
	}

	// Token: 0x06003E03 RID: 15875 RVA: 0x0015ACF5 File Offset: 0x00158EF5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		DiscoveredResources.Instance = this;
	}

	// Token: 0x06003E04 RID: 15876 RVA: 0x0015AD03 File Offset: 0x00158F03
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.FilterDisabledContent();
	}

	// Token: 0x06003E05 RID: 15877 RVA: 0x0015AD14 File Offset: 0x00158F14
	private void FilterDisabledContent()
	{
		HashSet<Tag> hashSet = new HashSet<Tag>();
		foreach (Tag tag in this.Discovered)
		{
			Element element = ElementLoader.GetElement(tag);
			if (element != null && element.disabled)
			{
				hashSet.Add(tag);
			}
			else
			{
				GameObject gameObject = Assets.TryGetPrefab(tag);
				if (gameObject != null && gameObject.HasTag(GameTags.DeprecatedContent))
				{
					hashSet.Add(tag);
				}
				else if (gameObject == null)
				{
					hashSet.Add(tag);
				}
			}
		}
		foreach (Tag tag2 in hashSet)
		{
			this.Discovered.Remove(tag2);
		}
		foreach (KeyValuePair<Tag, HashSet<Tag>> keyValuePair in this.DiscoveredCategories)
		{
			foreach (Tag tag3 in hashSet)
			{
				if (keyValuePair.Value.Contains(tag3))
				{
					keyValuePair.Value.Remove(tag3);
				}
			}
		}
		foreach (string text in new List<string> { "Pacu", "PacuCleaner", "PacuTropical", "PacuBaby", "PacuCleanerBaby", "PacuTropicalBaby" })
		{
			if (this.DiscoveredCategories.ContainsKey(text))
			{
				List<Tag> list = this.DiscoveredCategories[text].ToList<Tag>();
				SolidConsumerMonitor.Def def = Assets.GetPrefab(text).GetDef<SolidConsumerMonitor.Def>();
				foreach (Tag tag4 in list)
				{
					if (def.diet.GetDietInfo(tag4) == null)
					{
						this.DiscoveredCategories[text].Remove(tag4);
					}
				}
			}
		}
		if (this.DiscoveredCategories.ContainsKey(GameTags.IndustrialIngredient))
		{
			foreach (string text2 in new List<string> { "CrabShell", "CrabWoodShell" })
			{
				if (this.DiscoveredCategories[GameTags.IndustrialIngredient].Contains(text2))
				{
					this.DiscoveredCategories[GameTags.IndustrialIngredient].Remove(text2);
					this.DiscoverCategory(GameTags.Organics, text2);
				}
			}
		}
	}

	// Token: 0x06003E06 RID: 15878 RVA: 0x0015B070 File Offset: 0x00159270
	public bool CheckAllDiscoveredAreNew()
	{
		foreach (Tag tag in this.Discovered)
		{
			if (!this.newDiscoveries.ContainsKey(tag))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06003E07 RID: 15879 RVA: 0x0015B0D4 File Offset: 0x001592D4
	private void DiscoverCategory(Tag category_tag, Tag item_tag)
	{
		HashSet<Tag> hashSet;
		if (!this.DiscoveredCategories.TryGetValue(category_tag, out hashSet))
		{
			hashSet = new HashSet<Tag>();
			this.DiscoveredCategories[category_tag] = hashSet;
		}
		hashSet.Add(item_tag);
	}

	// Token: 0x06003E08 RID: 15880 RVA: 0x0015B10C File Offset: 0x0015930C
	public HashSet<Tag> GetDiscovered()
	{
		return this.Discovered;
	}

	// Token: 0x06003E09 RID: 15881 RVA: 0x0015B114 File Offset: 0x00159314
	public bool IsDiscovered(Tag tag)
	{
		return this.Discovered.Contains(tag) || this.DiscoveredCategories.ContainsKey(tag);
	}

	// Token: 0x06003E0A RID: 15882 RVA: 0x0015B134 File Offset: 0x00159334
	public bool AnyDiscovered(ICollection<Tag> tags)
	{
		foreach (Tag tag in tags)
		{
			if (this.IsDiscovered(tag))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003E0B RID: 15883 RVA: 0x0015B188 File Offset: 0x00159388
	public bool TryGetDiscoveredResourcesFromTag(Tag tag, out HashSet<Tag> resources)
	{
		return this.DiscoveredCategories.TryGetValue(tag, out resources);
	}

	// Token: 0x06003E0C RID: 15884 RVA: 0x0015B198 File Offset: 0x00159398
	public HashSet<Tag> GetDiscoveredResourcesFromTag(Tag tag)
	{
		HashSet<Tag> hashSet;
		if (this.DiscoveredCategories.TryGetValue(tag, out hashSet))
		{
			return hashSet;
		}
		return new HashSet<Tag>();
	}

	// Token: 0x06003E0D RID: 15885 RVA: 0x0015B1BC File Offset: 0x001593BC
	public Dictionary<Tag, HashSet<Tag>> GetDiscoveredResourcesFromTagSet(TagSet tagSet)
	{
		Dictionary<Tag, HashSet<Tag>> dictionary = new Dictionary<Tag, HashSet<Tag>>();
		foreach (Tag tag in tagSet)
		{
			HashSet<Tag> hashSet;
			if (this.DiscoveredCategories.TryGetValue(tag, out hashSet))
			{
				dictionary[tag] = hashSet;
			}
		}
		return dictionary;
	}

	// Token: 0x06003E0E RID: 15886 RVA: 0x0015B21C File Offset: 0x0015941C
	public static Tag GetCategoryForTags(HashSet<Tag> tags)
	{
		Tag tag = Tag.Invalid;
		foreach (Tag tag2 in tags)
		{
			if (GameTags.AllCategories.Contains(tag2) || GameTags.IgnoredMaterialCategories.Contains(tag2))
			{
				tag = tag2;
				break;
			}
		}
		return tag;
	}

	// Token: 0x06003E0F RID: 15887 RVA: 0x0015B288 File Offset: 0x00159488
	public static Tag GetCategoryForEntity(KPrefabID entity)
	{
		ElementChunk component = entity.GetComponent<ElementChunk>();
		if (component != null)
		{
			return component.GetComponent<PrimaryElement>().Element.materialCategory;
		}
		return DiscoveredResources.GetCategoryForTags(entity.Tags);
	}

	// Token: 0x06003E10 RID: 15888 RVA: 0x0015B2C4 File Offset: 0x001594C4
	public void Sim4000ms(float dt)
	{
		float num = GameClock.Instance.GetTimeInCycles() + GameClock.Instance.GetCurrentCycleAsPercentage();
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, float> keyValuePair in this.newDiscoveries)
		{
			if (num - keyValuePair.Value > 3f)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (Tag tag in list)
		{
			this.newDiscoveries.Remove(tag);
		}
	}

	// Token: 0x04002626 RID: 9766
	public static DiscoveredResources Instance;

	// Token: 0x04002627 RID: 9767
	[Serialize]
	private HashSet<Tag> Discovered = new HashSet<Tag>();

	// Token: 0x04002628 RID: 9768
	[Serialize]
	private Dictionary<Tag, HashSet<Tag>> DiscoveredCategories = new Dictionary<Tag, HashSet<Tag>>();

	// Token: 0x0400262A RID: 9770
	[Serialize]
	public Dictionary<Tag, float> newDiscoveries = new Dictionary<Tag, float>();
}
