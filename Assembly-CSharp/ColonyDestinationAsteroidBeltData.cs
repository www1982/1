using System;
using System.Collections.Generic;
using ProcGen;
using ProcGenGame;
using STRINGS;
using UnityEngine;

// Token: 0x02000C9B RID: 3227
public class ColonyDestinationAsteroidBeltData
{
	// Token: 0x17000740 RID: 1856
	// (get) Token: 0x0600632C RID: 25388 RVA: 0x00254008 File Offset: 0x00252208
	// (set) Token: 0x0600632D RID: 25389 RVA: 0x00254010 File Offset: 0x00252210
	public float TargetScale { get; set; }

	// Token: 0x17000741 RID: 1857
	// (get) Token: 0x0600632E RID: 25390 RVA: 0x00254019 File Offset: 0x00252219
	// (set) Token: 0x0600632F RID: 25391 RVA: 0x00254021 File Offset: 0x00252221
	public float Scale { get; set; }

	// Token: 0x17000742 RID: 1858
	// (get) Token: 0x06006330 RID: 25392 RVA: 0x0025402A File Offset: 0x0025222A
	// (set) Token: 0x06006331 RID: 25393 RVA: 0x00254032 File Offset: 0x00252232
	public int seed { get; private set; }

	// Token: 0x17000743 RID: 1859
	// (get) Token: 0x06006332 RID: 25394 RVA: 0x0025403B File Offset: 0x0025223B
	public string startWorldPath
	{
		get
		{
			return this.startWorld.filePath;
		}
	}

	// Token: 0x17000744 RID: 1860
	// (get) Token: 0x06006333 RID: 25395 RVA: 0x00254048 File Offset: 0x00252248
	// (set) Token: 0x06006334 RID: 25396 RVA: 0x00254050 File Offset: 0x00252250
	public Sprite sprite { get; private set; }

	// Token: 0x17000745 RID: 1861
	// (get) Token: 0x06006335 RID: 25397 RVA: 0x00254059 File Offset: 0x00252259
	// (set) Token: 0x06006336 RID: 25398 RVA: 0x00254061 File Offset: 0x00252261
	public int difficulty { get; private set; }

	// Token: 0x17000746 RID: 1862
	// (get) Token: 0x06006337 RID: 25399 RVA: 0x0025406A File Offset: 0x0025226A
	public string startWorldName
	{
		get
		{
			return Strings.Get(this.startWorld.name);
		}
	}

	// Token: 0x17000747 RID: 1863
	// (get) Token: 0x06006338 RID: 25400 RVA: 0x00254081 File Offset: 0x00252281
	public string properName
	{
		get
		{
			if (this.clusterLayout == null)
			{
				return "";
			}
			return this.clusterLayout.name;
		}
	}

	// Token: 0x17000748 RID: 1864
	// (get) Token: 0x06006339 RID: 25401 RVA: 0x0025409C File Offset: 0x0025229C
	public string beltPath
	{
		get
		{
			if (this.clusterLayout == null)
			{
				return WorldGenSettings.ClusterDefaultName;
			}
			return this.clusterLayout.filePath;
		}
	}

	// Token: 0x17000749 RID: 1865
	// (get) Token: 0x0600633A RID: 25402 RVA: 0x002540B7 File Offset: 0x002522B7
	// (set) Token: 0x0600633B RID: 25403 RVA: 0x002540BF File Offset: 0x002522BF
	public List<global::ProcGen.World> worlds { get; private set; }

	// Token: 0x1700074A RID: 1866
	// (get) Token: 0x0600633C RID: 25404 RVA: 0x002540C8 File Offset: 0x002522C8
	public ClusterLayout Layout
	{
		get
		{
			if (this.mutatedClusterLayout != null)
			{
				return this.mutatedClusterLayout.layout;
			}
			return this.clusterLayout;
		}
	}

	// Token: 0x1700074B RID: 1867
	// (get) Token: 0x0600633D RID: 25405 RVA: 0x002540E4 File Offset: 0x002522E4
	public global::ProcGen.World GetStartWorld
	{
		get
		{
			return this.startWorld;
		}
	}

	// Token: 0x0600633E RID: 25406 RVA: 0x002540EC File Offset: 0x002522EC
	public ColonyDestinationAsteroidBeltData(string staringWorldName, int seed, string clusterPath)
	{
		this.startWorld = SettingsCache.worlds.GetWorldData(staringWorldName);
		this.Scale = (this.TargetScale = this.startWorld.iconScale);
		this.worlds = new List<global::ProcGen.World>();
		if (clusterPath != null)
		{
			this.clusterLayout = SettingsCache.clusterLayouts.GetClusterData(clusterPath);
		}
		this.ReInitialize(seed);
	}

	// Token: 0x0600633F RID: 25407 RVA: 0x00254168 File Offset: 0x00252368
	public static Sprite GetUISprite(string filename)
	{
		if (filename.IsNullOrWhiteSpace())
		{
			filename = (DlcManager.FeatureClusterSpaceEnabled() ? "asteroid_sandstone_start_kanim" : "Asteroid_sandstone");
		}
		KAnimFile kanimFile;
		Assets.TryGetAnim(filename, out kanimFile);
		if (kanimFile != null)
		{
			return Def.GetUISpriteFromMultiObjectAnim(kanimFile, "ui", false, "");
		}
		return Assets.GetSprite(filename);
	}

	// Token: 0x06006340 RID: 25408 RVA: 0x002541C8 File Offset: 0x002523C8
	public void ReInitialize(int seed)
	{
		this.seed = seed;
		this.paramDescriptors.Clear();
		this.traitDescriptors.Clear();
		this.sprite = ColonyDestinationAsteroidBeltData.GetUISprite(this.startWorld.asteroidIcon);
		this.difficulty = this.clusterLayout.difficulty;
		this.mutatedClusterLayout = WorldgenMixing.DoWorldMixing(this.clusterLayout, seed, true, true);
		this.RemixClusterLayout();
	}

	// Token: 0x06006341 RID: 25409 RVA: 0x00254234 File Offset: 0x00252434
	public void RemixClusterLayout()
	{
		if (!WorldgenMixing.RefreshWorldMixing(this.mutatedClusterLayout, this.seed, true, true))
		{
			DebugUtil.LogWarningArgs(new object[] { "World remix failed, using default cluster instead." });
			this.mutatedClusterLayout = new MutatedClusterLayout(this.clusterLayout);
		}
		this.worlds.Clear();
		for (int i = 0; i < this.Layout.worldPlacements.Count; i++)
		{
			if (i != this.Layout.startWorldIndex)
			{
				this.worlds.Add(SettingsCache.worlds.GetWorldData(this.Layout.worldPlacements[i].world));
			}
		}
	}

	// Token: 0x06006342 RID: 25410 RVA: 0x002542D9 File Offset: 0x002524D9
	public List<AsteroidDescriptor> GetParamDescriptors()
	{
		if (this.paramDescriptors.Count == 0)
		{
			this.paramDescriptors = this.GenerateParamDescriptors();
		}
		return this.paramDescriptors;
	}

	// Token: 0x06006343 RID: 25411 RVA: 0x002542FA File Offset: 0x002524FA
	public List<AsteroidDescriptor> GetTraitDescriptors()
	{
		if (this.traitDescriptors.Count == 0)
		{
			this.traitDescriptors = this.GenerateTraitDescriptors();
		}
		return this.traitDescriptors;
	}

	// Token: 0x06006344 RID: 25412 RVA: 0x0025431C File Offset: 0x0025251C
	private List<AsteroidDescriptor> GenerateParamDescriptors()
	{
		List<AsteroidDescriptor> list = new List<AsteroidDescriptor>();
		if (this.clusterLayout != null && DlcManager.FeatureClusterSpaceEnabled())
		{
			list.Add(new AsteroidDescriptor(string.Format(WORLDS.SURVIVAL_CHANCE.CLUSTERNAME, Strings.Get(this.clusterLayout.name)), Strings.Get(this.clusterLayout.description), Color.white, null, null));
		}
		list.Add(new AsteroidDescriptor(string.Format(WORLDS.SURVIVAL_CHANCE.PLANETNAME, this.startWorldName), null, Color.white, null, null));
		list.Add(new AsteroidDescriptor(Strings.Get(this.startWorld.description), null, Color.white, null, null));
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			list.Add(new AsteroidDescriptor(string.Format(WORLDS.SURVIVAL_CHANCE.MOONNAMES, Array.Empty<object>()), null, Color.white, null, null));
			foreach (global::ProcGen.World world in this.worlds)
			{
				list.Add(new AsteroidDescriptor(string.Format("{0}", Strings.Get(world.name)), Strings.Get(world.description), Color.white, null, null));
			}
		}
		int num = Mathf.Clamp(this.difficulty, 0, ColonyDestinationAsteroidBeltData.survivalOptions.Count - 1);
		global::Tuple<string, string, string> tuple = ColonyDestinationAsteroidBeltData.survivalOptions[num];
		list.Add(new AsteroidDescriptor(string.Format(WORLDS.SURVIVAL_CHANCE.TITLE, tuple.first, tuple.third), null, Color.white, null, null));
		return list;
	}

	// Token: 0x06006345 RID: 25413 RVA: 0x002544D4 File Offset: 0x002526D4
	private List<AsteroidDescriptor> GenerateTraitDescriptors()
	{
		List<AsteroidDescriptor> list = new List<AsteroidDescriptor>();
		List<global::ProcGen.World> list2 = new List<global::ProcGen.World>();
		list2.Add(this.startWorld);
		list2.AddRange(this.worlds);
		for (int i = 0; i < list2.Count; i++)
		{
			global::ProcGen.World world = list2[i];
			if (DlcManager.IsExpansion1Active())
			{
				list.Add(new AsteroidDescriptor("", null, Color.white, null, null));
				list.Add(new AsteroidDescriptor(string.Format("<b>{0}</b>", Strings.Get(world.name)), null, Color.white, null, null));
			}
			List<WorldTrait> worldTraits = this.GetWorldTraits(world);
			foreach (WorldTrait worldTrait in worldTraits)
			{
				string text = worldTrait.filePath.Substring(worldTrait.filePath.LastIndexOf("/") + 1);
				list.Add(new AsteroidDescriptor(string.Format("<color=#{1}>{0}</color>", Strings.Get(worldTrait.name), worldTrait.colorHex), Strings.Get(worldTrait.description), global::Util.ColorFromHex(worldTrait.colorHex), null, text));
			}
			if (worldTraits.Count == 0)
			{
				list.Add(new AsteroidDescriptor(WORLD_TRAITS.NO_TRAITS.NAME, WORLD_TRAITS.NO_TRAITS.DESCRIPTION, Color.white, null, "NoTraits"));
			}
		}
		return list;
	}

	// Token: 0x06006346 RID: 25414 RVA: 0x00254650 File Offset: 0x00252850
	public List<AsteroidDescriptor> GenerateTraitDescriptors(global::ProcGen.World singleWorld, bool includeDefaultTrait = true)
	{
		List<AsteroidDescriptor> list = new List<AsteroidDescriptor>();
		List<global::ProcGen.World> list2 = new List<global::ProcGen.World>();
		list2.Add(this.startWorld);
		list2.AddRange(this.worlds);
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i] == singleWorld)
			{
				global::ProcGen.World world = list2[i];
				List<WorldTrait> worldTraits = this.GetWorldTraits(world);
				foreach (WorldTrait worldTrait in worldTraits)
				{
					string text = worldTrait.filePath.Substring(worldTrait.filePath.LastIndexOf("/") + 1);
					list.Add(new AsteroidDescriptor(string.Format("<color=#{1}>{0}</color>", Strings.Get(worldTrait.name), worldTrait.colorHex), Strings.Get(worldTrait.description), global::Util.ColorFromHex(worldTrait.colorHex), null, text));
				}
				if (worldTraits.Count == 0 && includeDefaultTrait)
				{
					list.Add(new AsteroidDescriptor(WORLD_TRAITS.NO_TRAITS.NAME, WORLD_TRAITS.NO_TRAITS.DESCRIPTION, Color.white, null, "NoTraits"));
				}
			}
		}
		return list;
	}

	// Token: 0x06006347 RID: 25415 RVA: 0x00254798 File Offset: 0x00252998
	public List<WorldTrait> GetWorldTraits(global::ProcGen.World singleWorld)
	{
		List<WorldTrait> list = new List<WorldTrait>();
		List<global::ProcGen.World> list2 = new List<global::ProcGen.World>();
		list2.Add(this.startWorld);
		list2.AddRange(this.worlds);
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i] == singleWorld)
			{
				global::ProcGen.World world = list2[i];
				int num = this.seed;
				if (num > 0)
				{
					num += this.clusterLayout.worldPlacements.FindIndex((WorldPlacement x) => x.world == world.filePath);
				}
				foreach (string text in SettingsCache.GetRandomTraits(num, world))
				{
					WorldTrait cachedWorldTrait = SettingsCache.GetCachedWorldTrait(text, true);
					list.Add(cachedWorldTrait);
				}
			}
		}
		return list;
	}

	// Token: 0x04004313 RID: 17171
	private global::ProcGen.World startWorld;

	// Token: 0x04004314 RID: 17172
	private ClusterLayout clusterLayout;

	// Token: 0x04004315 RID: 17173
	private MutatedClusterLayout mutatedClusterLayout;

	// Token: 0x04004316 RID: 17174
	private List<AsteroidDescriptor> paramDescriptors = new List<AsteroidDescriptor>();

	// Token: 0x04004317 RID: 17175
	private List<AsteroidDescriptor> traitDescriptors = new List<AsteroidDescriptor>();

	// Token: 0x04004318 RID: 17176
	public static List<global::Tuple<string, string, string>> survivalOptions = new List<global::Tuple<string, string, string>>
	{
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.MOSTHOSPITABLE, "", "D2F40C"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.VERYHIGH, "", "7DE419"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.HIGH, "", "36D246"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.NEUTRAL, "", "63C2B7"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.LOW, "", "6A8EB1"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.VERYLOW, "", "937890"),
		new global::Tuple<string, string, string>(WORLDS.SURVIVAL_CHANCE.LEASTHOSPITABLE, "", "9636DF")
	};
}
