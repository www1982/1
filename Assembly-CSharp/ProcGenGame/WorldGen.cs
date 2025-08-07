using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Delaunay.Geo;
using Klei;
using Klei.CustomSettings;
using KSerialization;
using LibNoiseDotNet.Graphics.Tools.Noise.Builder;
using ProcGen;
using ProcGen.Map;
using ProcGen.Noise;
using STRINGS;
using UnityEngine;
using VoronoiTree;

namespace ProcGenGame
{
	// Token: 0x02000E9E RID: 3742
	[Serializable]
	public class WorldGen
	{
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06007760 RID: 30560 RVA: 0x002E0ACD File Offset: 0x002DECCD
		public static string WORLDGEN_SAVE_FILENAME
		{
			get
			{
				return global::System.IO.Path.Combine(global::Util.RootFolder(), "WorldGenDataSave.worldgen");
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06007761 RID: 30561 RVA: 0x002E0ADE File Offset: 0x002DECDE
		public static Diseases diseaseStats
		{
			get
			{
				if (WorldGen.m_diseasesDb == null)
				{
					WorldGen.m_diseasesDb = new Diseases(null, true);
				}
				return WorldGen.m_diseasesDb;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06007762 RID: 30562 RVA: 0x002E0AF8 File Offset: 0x002DECF8
		public int BaseLeft
		{
			get
			{
				return this.Settings.GetBaseLocation().left;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06007763 RID: 30563 RVA: 0x002E0B0A File Offset: 0x002DED0A
		public int BaseRight
		{
			get
			{
				return this.Settings.GetBaseLocation().right;
			}
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06007764 RID: 30564 RVA: 0x002E0B1C File Offset: 0x002DED1C
		public int BaseTop
		{
			get
			{
				return this.Settings.GetBaseLocation().top;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06007765 RID: 30565 RVA: 0x002E0B2E File Offset: 0x002DED2E
		public int BaseBot
		{
			get
			{
				return this.Settings.GetBaseLocation().bottom;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06007766 RID: 30566 RVA: 0x002E0B40 File Offset: 0x002DED40
		// (set) Token: 0x06007767 RID: 30567 RVA: 0x002E0B48 File Offset: 0x002DED48
		public Data data { get; private set; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06007768 RID: 30568 RVA: 0x002E0B51 File Offset: 0x002DED51
		public bool HasData
		{
			get
			{
				return this.data != null;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06007769 RID: 30569 RVA: 0x002E0B5C File Offset: 0x002DED5C
		public bool HasNoiseData
		{
			get
			{
				return this.HasData && this.data.world != null;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x0600776A RID: 30570 RVA: 0x002E0B76 File Offset: 0x002DED76
		public float[] DensityMap
		{
			get
			{
				return this.data.world.density;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600776B RID: 30571 RVA: 0x002E0B88 File Offset: 0x002DED88
		public float[] HeatMap
		{
			get
			{
				return this.data.world.heatOffset;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600776C RID: 30572 RVA: 0x002E0B9A File Offset: 0x002DED9A
		public float[] OverrideMap
		{
			get
			{
				return this.data.world.overrides;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x0600776D RID: 30573 RVA: 0x002E0BAC File Offset: 0x002DEDAC
		public float[] BaseNoiseMap
		{
			get
			{
				return this.data.world.data;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600776E RID: 30574 RVA: 0x002E0BBE File Offset: 0x002DEDBE
		public float[] DefaultTendMap
		{
			get
			{
				return this.data.world.defaultTemp;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x0600776F RID: 30575 RVA: 0x002E0BD0 File Offset: 0x002DEDD0
		public Chunk World
		{
			get
			{
				return this.data.world;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06007770 RID: 30576 RVA: 0x002E0BDD File Offset: 0x002DEDDD
		public Vector2I WorldSize
		{
			get
			{
				return this.data.world.size;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06007771 RID: 30577 RVA: 0x002E0BEF File Offset: 0x002DEDEF
		public Vector2I WorldOffset
		{
			get
			{
				return this.data.world.offset;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06007772 RID: 30578 RVA: 0x002E0C01 File Offset: 0x002DEE01
		public int HiddenYOffset
		{
			get
			{
				return this.data.world.hiddenY;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06007773 RID: 30579 RVA: 0x002E0C13 File Offset: 0x002DEE13
		public WorldLayout WorldLayout
		{
			get
			{
				return this.data.worldLayout;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06007774 RID: 30580 RVA: 0x002E0C20 File Offset: 0x002DEE20
		public List<TerrainCell> OverworldCells
		{
			get
			{
				return this.data.overworldCells;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06007775 RID: 30581 RVA: 0x002E0C2D File Offset: 0x002DEE2D
		public List<TerrainCell> TerrainCells
		{
			get
			{
				return this.data.terrainCells;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06007776 RID: 30582 RVA: 0x002E0C3A File Offset: 0x002DEE3A
		public List<River> Rivers
		{
			get
			{
				return this.data.rivers;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06007777 RID: 30583 RVA: 0x002E0C47 File Offset: 0x002DEE47
		public GameSpawnData SpawnData
		{
			get
			{
				return this.data.gameSpawnData;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06007778 RID: 30584 RVA: 0x002E0C54 File Offset: 0x002DEE54
		public int ChunkEdgeSize
		{
			get
			{
				return this.data.chunkEdgeSize;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06007779 RID: 30585 RVA: 0x002E0C61 File Offset: 0x002DEE61
		public HashSet<int> ClaimedCells
		{
			get
			{
				return this.claimedCells;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600777A RID: 30586 RVA: 0x002E0C69 File Offset: 0x002DEE69
		public HashSet<int> HighPriorityClaimedCells
		{
			get
			{
				return this.highPriorityClaims;
			}
		}

		// Token: 0x0600777B RID: 30587 RVA: 0x002E0C71 File Offset: 0x002DEE71
		public void ClearClaimedCells()
		{
			this.claimedCells.Clear();
			this.highPriorityClaims.Clear();
		}

		// Token: 0x0600777C RID: 30588 RVA: 0x002E0C89 File Offset: 0x002DEE89
		public void AddHighPriorityCells(HashSet<int> cells)
		{
			this.highPriorityClaims.Union(cells);
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600777D RID: 30589 RVA: 0x002E0C98 File Offset: 0x002DEE98
		// (set) Token: 0x0600777E RID: 30590 RVA: 0x002E0CA0 File Offset: 0x002DEEA0
		public WorldGenSettings Settings { get; private set; }

		// Token: 0x0600777F RID: 30591 RVA: 0x002E0CAC File Offset: 0x002DEEAC
		public WorldGen(string worldName, List<string> chosenWorldTraits, List<string> chosenStoryTraits, bool assertMissingTraits)
		{
			WorldGen.LoadSettings(false);
			this.Settings = new WorldGenSettings(worldName, chosenWorldTraits, chosenStoryTraits, assertMissingTraits);
			this.data = new Data();
			this.data.chunkEdgeSize = this.Settings.GetIntSetting("ChunkEdgeSize");
		}

		// Token: 0x06007780 RID: 30592 RVA: 0x002E0D38 File Offset: 0x002DEF38
		public WorldGen(string worldName, Data data, List<string> chosenTraits, List<string> chosenStoryTraits, bool assertMissingTraits)
		{
			WorldGen.LoadSettings(false);
			this.Settings = new WorldGenSettings(worldName, chosenTraits, chosenStoryTraits, assertMissingTraits);
			this.data = data;
		}

		// Token: 0x06007781 RID: 30593 RVA: 0x002E0DA4 File Offset: 0x002DEFA4
		public WorldGen(WorldPlacement world, int seed, List<string> chosenWorldTraits, List<string> chosenStoryTraits, bool assertMissingTraits)
		{
			WorldGen.LoadSettings(false);
			this.Settings = new WorldGenSettings(world, seed, chosenWorldTraits, chosenStoryTraits, assertMissingTraits);
			this.data = new Data();
			this.data.chunkEdgeSize = this.Settings.GetIntSetting("ChunkEdgeSize");
		}

		// Token: 0x06007782 RID: 30594 RVA: 0x002E0E2F File Offset: 0x002DF02F
		public static void SetupDefaultElements()
		{
			WorldGen.voidElement = ElementLoader.FindElementByHash(SimHashes.Void);
			WorldGen.vacuumElement = ElementLoader.FindElementByHash(SimHashes.Vacuum);
			WorldGen.katairiteElement = ElementLoader.FindElementByHash(SimHashes.Katairite);
			WorldGen.unobtaniumElement = ElementLoader.FindElementByHash(SimHashes.Unobtanium);
		}

		// Token: 0x06007783 RID: 30595 RVA: 0x002E0E6D File Offset: 0x002DF06D
		public void Reset()
		{
			this.wasLoaded = false;
		}

		// Token: 0x06007784 RID: 30596 RVA: 0x002E0E78 File Offset: 0x002DF078
		public static void LoadSettings(bool in_async_thread = false)
		{
			bool is_playing = Application.isPlaying;
			if (in_async_thread)
			{
				WorldGen.loadSettingsTask = Task.Run(delegate
				{
					WorldGen.LoadSettings_Internal(is_playing, true);
				});
				return;
			}
			if (WorldGen.loadSettingsTask != null)
			{
				WorldGen.loadSettingsTask.Wait();
				WorldGen.loadSettingsTask = null;
			}
			WorldGen.LoadSettings_Internal(is_playing, false);
		}

		// Token: 0x06007785 RID: 30597 RVA: 0x002E0ED3 File Offset: 0x002DF0D3
		public static void WaitForPendingLoadSettings()
		{
			if (WorldGen.loadSettingsTask != null)
			{
				WorldGen.loadSettingsTask.Wait();
				WorldGen.loadSettingsTask = null;
			}
		}

		// Token: 0x06007786 RID: 30598 RVA: 0x002E0EEC File Offset: 0x002DF0EC
		public static IEnumerator ListenForLoadSettingsErrorRoutine()
		{
			while (WorldGen.loadSettingsTask != null)
			{
				if (WorldGen.loadSettingsTask.Exception != null)
				{
					throw WorldGen.loadSettingsTask.Exception;
				}
				yield return null;
			}
			yield break;
		}

		// Token: 0x06007787 RID: 30599 RVA: 0x002E0EF4 File Offset: 0x002DF0F4
		private static void LoadSettings_Internal(bool is_playing, bool preloadTemplates = false)
		{
			ListPool<YamlIO.Error, WorldGen>.PooledList pooledList = ListPool<YamlIO.Error, WorldGen>.Allocate();
			if (SettingsCache.LoadFiles(pooledList))
			{
				TemplateCache.Init();
				if (preloadTemplates)
				{
					foreach (global::ProcGen.World world in SettingsCache.worlds.worldCache.Values)
					{
						if (world.worldTemplateRules != null)
						{
							foreach (global::ProcGen.World.TemplateSpawnRules templateSpawnRules in world.worldTemplateRules)
							{
								foreach (string text in templateSpawnRules.names)
								{
									TemplateCache.GetTemplate(text);
								}
							}
						}
					}
					foreach (SubWorld subWorld in SettingsCache.subworlds.Values)
					{
						if (subWorld.subworldTemplateRules != null)
						{
							foreach (global::ProcGen.World.TemplateSpawnRules templateSpawnRules2 in subWorld.subworldTemplateRules)
							{
								foreach (string text2 in templateSpawnRules2.names)
								{
									TemplateCache.GetTemplate(text2);
								}
							}
						}
					}
					foreach (KeyValuePair<string, DlcManager.DlcInfo> keyValuePair in DlcManager.DLC_PACKS)
					{
						if (DlcManager.IsContentSubscribed(keyValuePair.Value.id))
						{
							string text3 = keyValuePair.Value.directory + "::poi/asteroid_impacts";
							string text4 = TemplateCache.RewriteTemplatePath(text3);
							if (Directory.Exists(text4))
							{
								foreach (string text5 in Directory.GetFiles(text4, "*.yaml"))
								{
									TemplateCache.GetTemplate(global::System.IO.Path.Combine(text3 ?? "", global::System.IO.Path.GetFileNameWithoutExtension(text5)));
								}
							}
						}
					}
				}
				if (CustomGameSettings.Instance != null)
				{
					foreach (KeyValuePair<string, WorldMixingSettings> keyValuePair2 in SettingsCache.worldMixingSettings)
					{
						string key = keyValuePair2.Key;
						if (keyValuePair2.Value.isModded && CustomGameSettings.Instance.GetWorldMixingSettingForWorldgenFile(key) == null)
						{
							WorldMixingSettingConfig worldMixingSettingConfig = new WorldMixingSettingConfig(key, key, null, null, true, -1L);
							CustomGameSettings.Instance.AddMixingSettingsConfig(worldMixingSettingConfig);
						}
					}
					foreach (KeyValuePair<string, SubworldMixingSettings> keyValuePair3 in SettingsCache.subworldMixingSettings)
					{
						string key2 = keyValuePair3.Key;
						if (keyValuePair3.Value.isModded && CustomGameSettings.Instance.GetSubworldMixingSettingForWorldgenFile(key2) == null)
						{
							SubworldMixingSettingConfig subworldMixingSettingConfig = new SubworldMixingSettingConfig(key2, key2, null, null, true, -1L);
							CustomGameSettings.Instance.AddMixingSettingsConfig(subworldMixingSettingConfig);
						}
					}
				}
			}
			CustomGameSettings.Instance != null;
			if (is_playing)
			{
				Global.Instance.modManager.HandleErrors(pooledList);
			}
			else
			{
				foreach (YamlIO.Error error in pooledList)
				{
					YamlIO.LogError(error, false);
				}
			}
			pooledList.Recycle();
		}

		// Token: 0x06007788 RID: 30600 RVA: 0x002E12E0 File Offset: 0x002DF4E0
		public void InitRandom(int worldSeed, int layoutSeed, int terrainSeed, int noiseSeed)
		{
			this.data.globalWorldSeed = worldSeed;
			this.data.globalWorldLayoutSeed = layoutSeed;
			this.data.globalTerrainSeed = terrainSeed;
			this.data.globalNoiseSeed = noiseSeed;
			this.myRandom = new SeededRandom(worldSeed);
		}

		// Token: 0x06007789 RID: 30601 RVA: 0x002E1320 File Offset: 0x002DF520
		public void Initialise(WorldGen.OfflineCallbackFunction callbackFn, Action<OfflineWorldGen.ErrorInfo> error_cb, int worldSeed = -1, int layoutSeed = -1, int terrainSeed = -1, int noiseSeed = -1, bool debug = false, bool skipPlacingTemplates = false)
		{
			if (this.wasLoaded)
			{
				global::Debug.LogError("Initialise called after load");
				return;
			}
			this.successCallbackFn = callbackFn;
			this.errorCallback = error_cb;
			global::Debug.Assert(this.successCallbackFn != null);
			this.isRunningDebugGen = debug;
			this.skipPlacingTemplates = skipPlacingTemplates;
			this.running = false;
			int num = global::UnityEngine.Random.Range(0, int.MaxValue);
			if (worldSeed == -1)
			{
				worldSeed = num;
			}
			if (layoutSeed == -1)
			{
				layoutSeed = num;
			}
			if (terrainSeed == -1)
			{
				terrainSeed = num;
			}
			if (noiseSeed == -1)
			{
				noiseSeed = num;
			}
			this.data.gameSpawnData = new GameSpawnData();
			this.InitRandom(worldSeed, layoutSeed, terrainSeed, noiseSeed);
			this.successCallbackFn(UI.WORLDGEN.COMPLETE.key, 0f, WorldGenProgressStages.Stages.Failure);
			WorldLayout.SetLayerGradient(SettingsCache.layers.LevelLayers);
		}

		// Token: 0x0600778A RID: 30602 RVA: 0x002E13E6 File Offset: 0x002DF5E6
		public bool GenerateOffline()
		{
			if (!this.GenerateWorldData())
			{
				this.successCallbackFn(UI.WORLDGEN.FAILED.key, 1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			return true;
		}

		// Token: 0x0600778B RID: 30603 RVA: 0x002E140F File Offset: 0x002DF60F
		private void PlaceTemplateSpawners(Vector2I position, TemplateContainer template, ref Dictionary<int, int> claimedCells)
		{
			this.data.gameSpawnData.AddTemplate(template, position, ref claimedCells);
		}

		// Token: 0x0600778C RID: 30604 RVA: 0x002E1424 File Offset: 0x002DF624
		public bool RenderOffline(bool doSettle, uint simSeed, BinaryWriter writer, ref Sim.Cell[] cells, ref Sim.DiseaseCell[] dc, int baseId, ref List<WorldTrait> placedStoryTraits, bool isStartingWorld = false)
		{
			float[] array = null;
			dc = null;
			HashSet<int> hashSet = new HashSet<int>();
			this.POIBounds = new List<RectInt>();
			this.WriteOverWorldNoise(this.successCallbackFn);
			if (!this.RenderToMap(this.successCallbackFn, ref cells, ref array, ref dc, ref hashSet, ref this.POIBounds))
			{
				this.successCallbackFn(UI.WORLDGEN.FAILED.key, -100f, WorldGenProgressStages.Stages.Failure);
				if (!this.isRunningDebugGen)
				{
					return false;
				}
			}
			foreach (int num in hashSet)
			{
				cells[num].SetValues(WorldGen.unobtaniumElement, ElementLoader.elements);
				this.claimedPOICells[num] = 1;
			}
			try
			{
				if (!this.skipPlacingTemplates)
				{
					this.POISpawners = TemplateSpawning.DetermineTemplatesForWorld(this.Settings, this.data.terrainCells, this.myRandom, ref this.POIBounds, this.isRunningDebugGen, ref placedStoryTraits, this.successCallbackFn);
				}
			}
			catch (WorldgenException ex)
			{
				if (!this.isRunningDebugGen)
				{
					this.ReportWorldGenError(ex, ex.userMessage);
					return false;
				}
			}
			catch (Exception ex2)
			{
				if (!this.isRunningDebugGen)
				{
					this.ReportWorldGenError(ex2, null);
					return false;
				}
			}
			if (isStartingWorld)
			{
				this.EnsureEnoughElementsInStartingBiome(cells);
			}
			List<TerrainCell> terrainCellsForTag = this.GetTerrainCellsForTag(WorldGenTags.StartWorld);
			foreach (TerrainCell terrainCell in this.OverworldCells)
			{
				foreach (TerrainCell terrainCell2 in terrainCellsForTag)
				{
					if (terrainCell.poly.PointInPolygon(terrainCell2.poly.Centroid()))
					{
						terrainCell.node.tags.Add(WorldGenTags.StartWorld);
						break;
					}
				}
			}
			if (doSettle)
			{
				this.running = WorldGenSimUtil.DoSettleSim(this.Settings, writer, simSeed, ref cells, ref array, ref dc, this.successCallbackFn, this.data, this.POISpawners, this.errorCallback, baseId);
			}
			if (!this.skipPlacingTemplates)
			{
				foreach (TemplateSpawning.TemplateSpawner templateSpawner in this.POISpawners)
				{
					this.PlaceTemplateSpawners(templateSpawner.position, templateSpawner.container, ref this.claimedPOICells);
				}
			}
			if (doSettle)
			{
				this.SpawnMobsAndTemplates(cells, array, dc, new HashSet<int>(this.claimedPOICells.Keys));
			}
			this.successCallbackFn(UI.WORLDGEN.COMPLETE.key, 1f, WorldGenProgressStages.Stages.Complete);
			this.running = false;
			return true;
		}

		// Token: 0x0600778D RID: 30605 RVA: 0x002E1730 File Offset: 0x002DF930
		private void SpawnMobsAndTemplates(Sim.Cell[] cells, float[] bgTemp, Sim.DiseaseCell[] dc, HashSet<int> claimedCells)
		{
			MobSpawning.DetectNaturalCavities(this.TerrainCells, this.successCallbackFn, cells);
			SeededRandom seededRandom = new SeededRandom(this.data.globalTerrainSeed);
			for (int i = 0; i < this.TerrainCells.Count; i++)
			{
				HashSet<int> hashSet = new HashSet<int>();
				float num = (float)i / (float)this.TerrainCells.Count;
				this.successCallbackFn(UI.WORLDGEN.PLACINGCREATURES.key, num, WorldGenProgressStages.Stages.PlacingCreatures);
				TerrainCell terrainCell = this.TerrainCells[i];
				Dictionary<int, string> dictionary = MobSpawning.PlaceFeatureAmbientMobs(this.Settings, terrainCell, seededRandom, cells, bgTemp, dc, claimedCells, this.isRunningDebugGen, ref hashSet);
				if (dictionary != null)
				{
					this.data.gameSpawnData.AddRange(dictionary);
				}
				dictionary = MobSpawning.PlaceBiomeAmbientMobs(this.Settings, terrainCell, seededRandom, cells, bgTemp, dc, claimedCells, this.isRunningDebugGen, ref hashSet);
				if (dictionary != null)
				{
					this.data.gameSpawnData.AddRange(dictionary);
				}
			}
			this.successCallbackFn(UI.WORLDGEN.PLACINGCREATURES.key, 1f, WorldGenProgressStages.Stages.PlacingCreatures);
		}

		// Token: 0x0600778E RID: 30606 RVA: 0x002E183C File Offset: 0x002DFA3C
		public void ReportWorldGenError(Exception e, string errorMessage = null)
		{
			if (errorMessage == null)
			{
				errorMessage = UI.FRONTEND.SUPPORTWARNINGS.WORLD_GEN_FAILURE;
			}
			bool flag = FileSystem.IsModdedFile(SettingsCache.RewriteWorldgenPathYaml(this.Settings.world.filePath));
			string text = ((CustomGameSettings.Instance != null) ? CustomGameSettings.Instance.GetSettingsCoordinate() : this.data.globalWorldLayoutSeed.ToString());
			global::Debug.LogWarning(string.Format("Worldgen Failure on seed {0}, modded={1}", text, flag));
			if (this.errorCallback != null)
			{
				this.errorCallback(new OfflineWorldGen.ErrorInfo
				{
					errorDesc = string.Format(errorMessage, text),
					exception = e
				});
			}
			GenericGameSettings.instance.devAutoWorldGenActive = false;
			if (!flag)
			{
				KCrashReporter.ReportError("WorldgenFailure: ", e.StackTrace, null, null, text + " - " + e.Message, false, new string[] { KCrashReporter.CRASH_CATEGORY.WORLDGENFAILURE }, null);
			}
		}

		// Token: 0x0600778F RID: 30607 RVA: 0x002E1926 File Offset: 0x002DFB26
		public void SetWorldSize(int width, int height)
		{
			this.data.world = new Chunk(0, 0, width, height);
		}

		// Token: 0x06007790 RID: 30608 RVA: 0x002E193C File Offset: 0x002DFB3C
		public void SetHiddenYOffset(int offset)
		{
			this.data.world.hiddenY = offset;
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x002E194F File Offset: 0x002DFB4F
		public Vector2I GetSize()
		{
			return this.data.world.size;
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x002E1961 File Offset: 0x002DFB61
		public void SetPosition(Vector2I position)
		{
			this.data.world.offset = position;
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x002E1974 File Offset: 0x002DFB74
		public Vector2I GetPosition()
		{
			return this.data.world.offset;
		}

		// Token: 0x06007794 RID: 30612 RVA: 0x002E1986 File Offset: 0x002DFB86
		public void SetClusterLocation(AxialI location)
		{
			this.data.clusterLocation = location;
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x002E1994 File Offset: 0x002DFB94
		public AxialI GetClusterLocation()
		{
			return this.data.clusterLocation;
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x002E19A4 File Offset: 0x002DFBA4
		public bool GenerateNoiseData(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			try
			{
				this.running = updateProgressFn(UI.WORLDGEN.SETUPNOISE.key, 0f, WorldGenProgressStages.Stages.SetupNoise);
				if (!this.running)
				{
					return false;
				}
				this.SetupNoise(updateProgressFn);
				this.running = updateProgressFn(UI.WORLDGEN.SETUPNOISE.key, 1f, WorldGenProgressStages.Stages.SetupNoise);
				if (!this.running)
				{
					return false;
				}
				this.GenerateUnChunkedNoise(updateProgressFn);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				this.ReportWorldGenError(ex, null);
				WorldGenLogger.LogException(message, stackTrace);
				this.running = this.successCallbackFn(new StringKey("Exception in GenerateNoiseData"), -1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			return true;
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x002E1A68 File Offset: 0x002DFC68
		public bool GenerateLayout(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			try
			{
				this.running = updateProgressFn(UI.WORLDGEN.WORLDLAYOUT.key, 0f, WorldGenProgressStages.Stages.WorldLayout);
				if (!this.running)
				{
					return false;
				}
				global::Debug.Assert(this.data.world.size.x != 0 && this.data.world.size.y != 0, "Map size has not been set");
				this.data.worldLayout = new WorldLayout(this, this.data.world.size.x, this.data.world.size.y, this.data.globalWorldLayoutSeed);
				this.running = updateProgressFn(UI.WORLDGEN.WORLDLAYOUT.key, 1f, WorldGenProgressStages.Stages.WorldLayout);
				this.data.voronoiTree = null;
				try
				{
					this.data.voronoiTree = this.WorldLayout.GenerateOverworld(this.Settings.world.layoutMethod == global::ProcGen.World.LayoutMethod.PowerTree, this.isRunningDebugGen);
					this.WorldLayout.PopulateSubworlds();
					this.CompleteLayout(updateProgressFn);
				}
				catch (Exception ex)
				{
					string message = ex.Message;
					string stackTrace = ex.StackTrace;
					WorldGenLogger.LogException(message, stackTrace);
					this.ReportWorldGenError(ex, null);
					this.running = updateProgressFn(new StringKey("Exception in InitVoronoiTree"), -1f, WorldGenProgressStages.Stages.Failure);
					return false;
				}
				this.data.overworldCells = new List<TerrainCell>(40);
				for (int i = 0; i < this.data.voronoiTree.ChildCount(); i++)
				{
					global::VoronoiTree.Tree tree = this.data.voronoiTree.GetChild(i) as global::VoronoiTree.Tree;
					Cell cell = this.data.worldLayout.overworldGraph.FindNodeByID(tree.site.id);
					this.data.overworldCells.Add(new TerrainCellLogged(cell, tree.site, tree.minDistanceToTag));
				}
				this.running = updateProgressFn(UI.WORLDGEN.WORLDLAYOUT.key, 1f, WorldGenProgressStages.Stages.WorldLayout);
			}
			catch (Exception ex2)
			{
				string message2 = ex2.Message;
				string stackTrace2 = ex2.StackTrace;
				WorldGenLogger.LogException(message2, stackTrace2);
				this.ReportWorldGenError(ex2, null);
				this.successCallbackFn(new StringKey("Exception in GenerateLayout"), -1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			return true;
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x002E1CF4 File Offset: 0x002DFEF4
		public bool CompleteLayout(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			try
			{
				this.running = updateProgressFn(UI.WORLDGEN.COMPLETELAYOUT.key, 0f, WorldGenProgressStages.Stages.CompleteLayout);
				if (!this.running)
				{
					return false;
				}
				this.data.terrainCells = null;
				this.running = updateProgressFn(UI.WORLDGEN.COMPLETELAYOUT.key, 0.65f, WorldGenProgressStages.Stages.CompleteLayout);
				if (!this.running)
				{
					return false;
				}
				this.running = updateProgressFn(UI.WORLDGEN.COMPLETELAYOUT.key, 0.75f, WorldGenProgressStages.Stages.CompleteLayout);
				if (!this.running)
				{
					return false;
				}
				this.data.terrainCells = new List<TerrainCell>(4000);
				List<global::VoronoiTree.Node> list = new List<global::VoronoiTree.Node>();
				this.data.voronoiTree.ForceLowestToLeaf();
				this.ApplyStartNode();
				this.ApplySwapTags();
				this.data.voronoiTree.GetLeafNodes(list, null);
				WorldLayout.ResetMapGraphFromVoronoiTree(list, this.WorldLayout.localGraph, true);
				for (int i = 0; i < list.Count; i++)
				{
					global::VoronoiTree.Node node = list[i];
					Cell tn = this.data.worldLayout.localGraph.FindNodeByID(node.site.id);
					if (tn != null)
					{
						TerrainCell terrainCell = this.data.terrainCells.Find((TerrainCell c) => c.node == tn);
						if (terrainCell == null)
						{
							TerrainCell terrainCell2 = new TerrainCellLogged(tn, node.site, node.parent.minDistanceToTag);
							this.data.terrainCells.Add(terrainCell2);
						}
						else
						{
							global::Debug.LogWarning("Duplicate cell found" + terrainCell.node.NodeId.ToString());
						}
					}
				}
				for (int j = 0; j < this.data.terrainCells.Count; j++)
				{
					TerrainCell terrainCell3 = this.data.terrainCells[j];
					for (int k = j + 1; k < this.data.terrainCells.Count; k++)
					{
						int num = 0;
						TerrainCell terrainCell4 = this.data.terrainCells[k];
						LineSegment lineSegment;
						if (terrainCell4.poly.SharesEdge(terrainCell3.poly, ref num, out lineSegment) == Polygon.Commonality.Edge)
						{
							terrainCell3.neighbourTerrainCells.Add(k);
							terrainCell4.neighbourTerrainCells.Add(j);
						}
					}
				}
				this.running = updateProgressFn(UI.WORLDGEN.COMPLETELAYOUT.key, 1f, WorldGenProgressStages.Stages.CompleteLayout);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				WorldGenLogger.LogException(message, stackTrace);
				this.successCallbackFn(new StringKey("Exception in CompleteLayout"), -1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			return true;
		}

		// Token: 0x06007799 RID: 30617 RVA: 0x002E1FD8 File Offset: 0x002E01D8
		public void UpdateVoronoiNodeTags(global::VoronoiTree.Node node)
		{
			global::ProcGen.Node node2;
			if (node.tags.Contains(WorldGenTags.Overworld))
			{
				node2 = this.WorldLayout.overworldGraph.FindNodeByID(node.site.id);
			}
			else
			{
				node2 = this.WorldLayout.localGraph.FindNodeByID(node.site.id);
			}
			if (node2 != null)
			{
				node2.tags.Union(node.tags);
			}
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x002E2047 File Offset: 0x002E0247
		public bool GenerateWorldData()
		{
			return this.GenerateNoiseData(this.successCallbackFn) && this.GenerateLayout(this.successCallbackFn);
		}

		// Token: 0x0600779B RID: 30619 RVA: 0x002E2068 File Offset: 0x002E0268
		public void EnsureEnoughElementsInStartingBiome(Sim.Cell[] cells)
		{
			List<StartingWorldElementSetting> defaultStartingElements = this.Settings.GetDefaultStartingElements();
			List<TerrainCell> terrainCellsForTag = this.GetTerrainCellsForTag(WorldGenTags.StartWorld);
			foreach (StartingWorldElementSetting startingWorldElementSetting in defaultStartingElements)
			{
				float amount = startingWorldElementSetting.amount;
				Element element = ElementLoader.GetElement(new Tag(((SimHashes)Enum.Parse(typeof(SimHashes), startingWorldElementSetting.element, true)).ToString()));
				float num = 0f;
				int num2 = 0;
				foreach (TerrainCell terrainCell in terrainCellsForTag)
				{
					foreach (int num3 in terrainCell.GetAllCells())
					{
						if (element.idx == cells[num3].elementIdx)
						{
							num2++;
							num += cells[num3].mass;
						}
					}
				}
				DebugUtil.DevAssert(num2 > 0, string.Format("No {0} found in starting biome and trying to ensure at least {1}. Skipping.", element.id, amount), null);
				if (num < amount && num2 > 0)
				{
					float num4 = num / (float)num2;
					float num5 = (amount - num) / (float)num2;
					DebugUtil.DevAssert(num4 + num5 <= 2f * element.maxMass, string.Format("Number of cells ({0}) of {1} in the starting biome is insufficient, this will result in extremely dense cells. {2} but expecting less than {3}", new object[]
					{
						num2,
						element.id,
						num4 + num5,
						2f * element.maxMass
					}), null);
					foreach (TerrainCell terrainCell2 in terrainCellsForTag)
					{
						foreach (int num6 in terrainCell2.GetAllCells())
						{
							if (element.idx == cells[num6].elementIdx)
							{
								int num7 = num6;
								cells[num7].mass = cells[num7].mass + num5;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600779C RID: 30620 RVA: 0x002E233C File Offset: 0x002E053C
		public bool RenderToMap(WorldGen.OfflineCallbackFunction updateProgressFn, ref Sim.Cell[] cells, ref float[] bgTemp, ref Sim.DiseaseCell[] dcs, ref HashSet<int> borderCells, ref List<RectInt> poiBounds)
		{
			global::Debug.Assert(Grid.WidthInCells == this.Settings.world.worldsize.x);
			global::Debug.Assert(Grid.HeightInCells == this.Settings.world.worldsize.y);
			global::Debug.Assert(Grid.CellCount == Grid.WidthInCells * Grid.HeightInCells);
			global::Debug.Assert(Grid.CellSizeInMeters != 0f);
			borderCells = new HashSet<int>();
			cells = new Sim.Cell[Grid.CellCount];
			bgTemp = new float[Grid.CellCount];
			dcs = new Sim.DiseaseCell[Grid.CellCount];
			this.running = updateProgressFn(UI.WORLDGEN.CLEARINGLEVEL.key, 0f, WorldGenProgressStages.Stages.ClearingLevel);
			if (!this.running)
			{
				return false;
			}
			for (int i = 0; i < cells.Length; i++)
			{
				cells[i].SetValues(WorldGen.katairiteElement, ElementLoader.elements);
				bgTemp[i] = -1f;
				dcs[i] = default(Sim.DiseaseCell);
				dcs[i].diseaseIdx = byte.MaxValue;
				this.running = updateProgressFn(UI.WORLDGEN.CLEARINGLEVEL.key, (float)i / (float)Grid.CellCount, WorldGenProgressStages.Stages.ClearingLevel);
				if (!this.running)
				{
					return false;
				}
			}
			updateProgressFn(UI.WORLDGEN.CLEARINGLEVEL.key, 1f, WorldGenProgressStages.Stages.ClearingLevel);
			try
			{
				this.ProcessByTerrainCell(cells, bgTemp, dcs, updateProgressFn, this.highPriorityClaims);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				WorldGenLogger.LogException(message, stackTrace);
				this.running = updateProgressFn(new StringKey("Exception in ProcessByTerrainCell"), -1f, WorldGenProgressStages.Stages.Failure);
				return false;
			}
			if (this.Settings.GetBoolSetting("DrawWorldBorder"))
			{
				SeededRandom seededRandom = new SeededRandom(0);
				this.DrawWorldBorder(cells, this.data.world, seededRandom, ref borderCells, ref poiBounds, updateProgressFn);
				updateProgressFn(UI.WORLDGEN.DRAWWORLDBORDER.key, 1f, WorldGenProgressStages.Stages.DrawWorldBorder);
			}
			this.data.gameSpawnData.baseStartPos = this.data.worldLayout.GetStartLocation();
			foreach (global::ProcGen.World.ModifyLayoutTagsRule modifyLayoutTagsRule in this.Settings.world.modifyLayoutTags)
			{
				foreach (TerrainCell terrainCell in this.data.terrainCells)
				{
					if (TemplateSpawning.DoesCellMatchFilters(terrainCell, modifyLayoutTagsRule.allowedCellsFilter))
					{
						foreach (string text in modifyLayoutTagsRule.addTags)
						{
							terrainCell.node.tags.Add(text);
						}
						foreach (string text2 in modifyLayoutTagsRule.removeTags)
						{
							terrainCell.node.tags.Remove(text2);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600779D RID: 30621 RVA: 0x002E26F8 File Offset: 0x002E08F8
		public SubWorld GetSubWorldForNode(global::VoronoiTree.Tree node)
		{
			global::ProcGen.Node node2 = this.WorldLayout.overworldGraph.FindNodeByID(node.site.id);
			if (node2 == null)
			{
				return null;
			}
			if (!this.Settings.HasSubworld(node2.type))
			{
				return null;
			}
			return this.Settings.GetSubWorld(node2.type);
		}

		// Token: 0x0600779E RID: 30622 RVA: 0x002E274C File Offset: 0x002E094C
		public global::VoronoiTree.Tree GetOverworldForNode(Leaf leaf)
		{
			if (leaf == null)
			{
				return null;
			}
			return this.data.worldLayout.GetVoronoiTree().GetChildContainingLeaf(leaf);
		}

		// Token: 0x0600779F RID: 30623 RVA: 0x002E2769 File Offset: 0x002E0969
		public Leaf GetLeafForTerrainCell(TerrainCell cell)
		{
			if (cell == null)
			{
				return null;
			}
			return this.data.worldLayout.GetVoronoiTree().GetNodeForSite(cell.site) as Leaf;
		}

		// Token: 0x060077A0 RID: 30624 RVA: 0x002E2790 File Offset: 0x002E0990
		public List<TerrainCell> GetTerrainCellsForTag(Tag tag)
		{
			List<TerrainCell> list = new List<TerrainCell>();
			List<global::VoronoiTree.Node> leafNodesWithTag = this.WorldLayout.GetLeafNodesWithTag(tag);
			for (int i = 0; i < leafNodesWithTag.Count; i++)
			{
				global::VoronoiTree.Node node = leafNodesWithTag[i];
				TerrainCell terrainCell = this.data.terrainCells.Find((TerrainCell cell) => cell.site.id == node.site.id);
				if (terrainCell != null)
				{
					list.Add(terrainCell);
				}
			}
			return list;
		}

		// Token: 0x060077A1 RID: 30625 RVA: 0x002E2800 File Offset: 0x002E0A00
		private void GetStartCells(out int baseX, out int baseY)
		{
			Vector2I startLocation = new Vector2I(this.data.world.size.x / 2, (int)((float)this.data.world.size.y * 0.7f));
			if (this.data.worldLayout != null)
			{
				startLocation = this.data.worldLayout.GetStartLocation();
			}
			baseX = startLocation.x;
			baseY = startLocation.y;
		}

		// Token: 0x060077A2 RID: 30626 RVA: 0x002E2878 File Offset: 0x002E0A78
		public void FinalizeStartLocation()
		{
			if (string.IsNullOrEmpty(this.Settings.world.startSubworldName))
			{
				return;
			}
			List<global::VoronoiTree.Node> startNodes = this.WorldLayout.GetStartNodes();
			global::Debug.Assert(startNodes.Count > 0, "Couldn't find a start node on a world that expects it!!");
			TagSet tagSet = new TagSet { WorldGenTags.StartLocation };
			for (int i = 1; i < startNodes.Count; i++)
			{
				startNodes[i].tags.Remove(tagSet);
			}
		}

		// Token: 0x060077A3 RID: 30627 RVA: 0x002E28F0 File Offset: 0x002E0AF0
		private void SwitchNodes(global::VoronoiTree.Node n1, global::VoronoiTree.Node n2)
		{
			if (n1 is global::VoronoiTree.Tree || n2 is global::VoronoiTree.Tree)
			{
				global::Debug.Log("WorldGen::SwitchNodes() Skipping tree node");
				return;
			}
			Diagram.Site site = n1.site;
			n1.site = n2.site;
			n2.site = site;
			Cell cell = this.data.worldLayout.localGraph.FindNodeByID(n1.site.id);
			global::ProcGen.Node node = this.data.worldLayout.localGraph.FindNodeByID(n2.site.id);
			string type = cell.type;
			cell.SetType(node.type);
			node.SetType(type);
		}

		// Token: 0x060077A4 RID: 30628 RVA: 0x002E298C File Offset: 0x002E0B8C
		private void ApplyStartNode()
		{
			List<global::VoronoiTree.Node> leafNodesWithTag = this.data.worldLayout.GetLeafNodesWithTag(WorldGenTags.StartLocation);
			if (leafNodesWithTag.Count == 0)
			{
				return;
			}
			global::VoronoiTree.Node node = leafNodesWithTag[0];
			global::VoronoiTree.Tree parent = node.parent;
			node.parent.AddTagToChildren(WorldGenTags.IgnoreCaveOverride);
			node.parent.tags.Remove(WorldGenTags.StartLocation);
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x002E29EC File Offset: 0x002E0BEC
		private void ApplySwapTags()
		{
			List<global::VoronoiTree.Node> list = new List<global::VoronoiTree.Node>();
			for (int i = 0; i < this.data.voronoiTree.ChildCount(); i++)
			{
				if (this.data.voronoiTree.GetChild(i).tags.Contains(WorldGenTags.SwapLakesToBelow))
				{
					list.Add(this.data.voronoiTree.GetChild(i));
				}
			}
			foreach (global::VoronoiTree.Node node in list)
			{
				if (!node.tags.Contains(WorldGenTags.CenteralFeature))
				{
					List<global::VoronoiTree.Node> list2 = new List<global::VoronoiTree.Node>();
					((global::VoronoiTree.Tree)node).GetNodesWithoutTag(WorldGenTags.CenteralFeature, list2);
					this.SwapNodesAround(WorldGenTags.Wet, true, list2, node.site.poly.Centroid());
				}
			}
		}

		// Token: 0x060077A6 RID: 30630 RVA: 0x002E2AD8 File Offset: 0x002E0CD8
		private void SwapNodesAround(Tag swapTag, bool sendTagToBottom, List<global::VoronoiTree.Node> nodes, Vector2 pivot)
		{
			nodes.ShuffleSeeded(this.myRandom.RandomSource());
			List<global::VoronoiTree.Node> list = new List<global::VoronoiTree.Node>();
			List<global::VoronoiTree.Node> list2 = new List<global::VoronoiTree.Node>();
			foreach (global::VoronoiTree.Node node in nodes)
			{
				bool flag = node.tags.Contains(swapTag);
				bool flag2 = node.site.poly.Centroid().y > pivot.y;
				bool flag3 = (flag2 && sendTagToBottom) || (!flag2 && !sendTagToBottom);
				if (flag && flag3)
				{
					if (list2.Count > 0)
					{
						this.SwitchNodes(node, list2[0]);
						list2.RemoveAt(0);
					}
					else
					{
						list.Add(node);
					}
				}
				else if (!flag && !flag3)
				{
					if (list.Count > 0)
					{
						this.SwitchNodes(node, list[0]);
						list.RemoveAt(0);
					}
					else
					{
						list2.Add(node);
					}
				}
			}
			if (list2.Count > 0)
			{
				int num = 0;
				while (num < list.Count && list2.Count > 0)
				{
					this.SwitchNodes(list[num], list2[0]);
					list2.RemoveAt(0);
					num++;
				}
			}
		}

		// Token: 0x060077A7 RID: 30631 RVA: 0x002E2C28 File Offset: 0x002E0E28
		public void GetElementForBiomePoint(Chunk chunk, ElementBandConfiguration elementBands, Vector2I pos, out Element element, out Sim.PhysicsData pd, out Sim.DiseaseCell dc, float erode)
		{
			TerrainCell.ElementOverride elementOverride = TerrainCell.GetElementOverride(WorldGen.voidElement.tag.ToString(), null);
			elementOverride = this.GetElementFromBiomeElementTable(chunk, pos, elementBands, erode);
			element = elementOverride.element;
			pd = elementOverride.pdelement;
			dc = elementOverride.dc;
		}

		// Token: 0x060077A8 RID: 30632 RVA: 0x002E2C80 File Offset: 0x002E0E80
		public void ConvertIntersectingCellsToType(MathUtil.Pair<Vector2, Vector2> segment, string type)
		{
			List<Vector2I> line = global::ProcGen.Util.GetLine(segment.First, segment.Second);
			for (int i = 0; i < this.data.terrainCells.Count; i++)
			{
				if (this.data.terrainCells[i].node.type != type)
				{
					for (int j = 0; j < line.Count; j++)
					{
						if (this.data.terrainCells[i].poly.Contains(line[j]))
						{
							this.data.terrainCells[i].node.SetType(type);
						}
					}
				}
			}
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x002E2D38 File Offset: 0x002E0F38
		public string GetSubWorldType(Vector2I pos)
		{
			for (int i = 0; i < this.data.overworldCells.Count; i++)
			{
				if (this.data.overworldCells[i].poly.Contains(pos))
				{
					return this.data.overworldCells[i].node.type;
				}
			}
			return null;
		}

		// Token: 0x060077AA RID: 30634 RVA: 0x002E2DA0 File Offset: 0x002E0FA0
		private void ProcessByTerrainCell(Sim.Cell[] map_cells, float[] bgTemp, Sim.DiseaseCell[] dcs, WorldGen.OfflineCallbackFunction updateProgressFn, HashSet<int> hightPriorityCells)
		{
			updateProgressFn(UI.WORLDGEN.PROCESSING.key, 0f, WorldGenProgressStages.Stages.Processing);
			SeededRandom seededRandom = new SeededRandom(this.data.globalTerrainSeed);
			try
			{
				for (int i = 0; i < this.data.terrainCells.Count; i++)
				{
					updateProgressFn(UI.WORLDGEN.PROCESSING.key, (float)i / (float)this.data.terrainCells.Count, WorldGenProgressStages.Stages.Processing);
					this.data.terrainCells[i].Process(this, map_cells, bgTemp, dcs, this.data.world, seededRandom);
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				string stackTrace = ex.StackTrace;
				updateProgressFn(new StringKey("Exception in TerrainCell.Process"), -1f, WorldGenProgressStages.Stages.Failure);
				global::Debug.LogError("Error:" + message + "\n" + stackTrace);
			}
			List<Border> list = new List<Border>();
			updateProgressFn(UI.WORLDGEN.BORDERS.key, 0f, WorldGenProgressStages.Stages.Borders);
			try
			{
				List<Edge> edgesWithTag = this.data.worldLayout.overworldGraph.GetEdgesWithTag(WorldGenTags.EdgeUnpassable);
				for (int j = 0; j < edgesWithTag.Count; j++)
				{
					Edge edge = edgesWithTag[j];
					List<Cell> cells2 = this.data.worldLayout.overworldGraph.GetNodes(edge);
					global::Debug.Assert(cells2[0] != cells2[1], "Both nodes on an arc were the same. Allegedly this means it was a world border but I don't think we do that anymore.");
					TerrainCell terrainCell = this.data.overworldCells.Find((TerrainCell c) => c.node == cells2[0]);
					TerrainCell terrainCell2 = this.data.overworldCells.Find((TerrainCell c) => c.node == cells2[1]);
					global::Debug.Assert(terrainCell != null && terrainCell2 != null, "NULL Terrainell nodes with EdgeUnpassable");
					terrainCell.LogInfo("BORDER WITH " + terrainCell2.site.id.ToString(), "UNPASSABLE", 0f);
					terrainCell2.LogInfo("BORDER WITH " + terrainCell.site.id.ToString(), "UNPASSABLE", 0f);
					list.Add(new Border(new Neighbors(terrainCell, terrainCell2), edge.corner0.position, edge.corner1.position)
					{
						element = SettingsCache.borders["impenetrable"],
						width = (float)seededRandom.RandomRange(2, 3)
					});
				}
				List<Edge> edgesWithTag2 = this.data.worldLayout.overworldGraph.GetEdgesWithTag(WorldGenTags.EdgeClosed);
				for (int k = 0; k < edgesWithTag2.Count; k++)
				{
					Edge edge2 = edgesWithTag2[k];
					if (!edgesWithTag.Contains(edge2))
					{
						List<Cell> cells = this.data.worldLayout.overworldGraph.GetNodes(edge2);
						global::Debug.Assert(cells[0] != cells[1], "Both nodes on an arc were the same. Allegedly this means it was a world border but I don't think we do that anymore.");
						TerrainCell terrainCell3 = this.data.overworldCells.Find((TerrainCell c) => c.node == cells[0]);
						TerrainCell terrainCell4 = this.data.overworldCells.Find((TerrainCell c) => c.node == cells[1]);
						global::Debug.Assert(terrainCell3 != null && terrainCell4 != null, "NULL Terraincell nodes with EdgeClosed");
						string borderOverride = this.Settings.GetSubWorld(terrainCell3.node.type).borderOverride;
						string borderOverride2 = this.Settings.GetSubWorld(terrainCell4.node.type).borderOverride;
						string text;
						if (!string.IsNullOrEmpty(borderOverride2) && !string.IsNullOrEmpty(borderOverride))
						{
							int borderOverridePriority = this.Settings.GetSubWorld(terrainCell3.node.type).borderOverridePriority;
							int borderOverridePriority2 = this.Settings.GetSubWorld(terrainCell4.node.type).borderOverridePriority;
							if (borderOverridePriority == borderOverridePriority2)
							{
								text = ((seededRandom.RandomValue() > 0.5f) ? borderOverride2 : borderOverride);
								terrainCell3.LogInfo("BORDER WITH " + terrainCell4.site.id.ToString(), "Picked Random:" + text, 0f);
								terrainCell4.LogInfo("BORDER WITH " + terrainCell3.site.id.ToString(), "Picked Random:" + text, 0f);
							}
							else
							{
								text = ((borderOverridePriority > borderOverridePriority2) ? borderOverride : borderOverride2);
								terrainCell3.LogInfo("BORDER WITH " + terrainCell4.site.id.ToString(), "Picked priority:" + text, 0f);
								terrainCell4.LogInfo("BORDER WITH " + terrainCell3.site.id.ToString(), "Picked priority:" + text, 0f);
							}
						}
						else if (string.IsNullOrEmpty(borderOverride2) && string.IsNullOrEmpty(borderOverride))
						{
							text = "hardToDig";
							terrainCell3.LogInfo("BORDER WITH " + terrainCell4.site.id.ToString(), "Both null", 0f);
							terrainCell4.LogInfo("BORDER WITH " + terrainCell3.site.id.ToString(), "Both null", 0f);
						}
						else
						{
							text = ((!string.IsNullOrEmpty(borderOverride2)) ? borderOverride2 : borderOverride);
							terrainCell3.LogInfo("BORDER WITH " + terrainCell4.site.id.ToString(), "Picked specific " + text, 0f);
							terrainCell4.LogInfo("BORDER WITH " + terrainCell3.site.id.ToString(), "Picked specific " + text, 0f);
						}
						if (!(text == "NONE"))
						{
							Border border = new Border(new Neighbors(terrainCell3, terrainCell4), edge2.corner0.position, edge2.corner1.position);
							border.element = SettingsCache.borders[text];
							MinMax minMax = new MinMax(1.5f, 2f);
							MinMax borderSizeOverride = this.Settings.GetSubWorld(terrainCell3.node.type).borderSizeOverride;
							MinMax borderSizeOverride2 = this.Settings.GetSubWorld(terrainCell4.node.type).borderSizeOverride;
							bool flag = borderSizeOverride.min != 0f || borderSizeOverride.max != 0f;
							bool flag2 = borderSizeOverride2.min != 0f || borderSizeOverride2.max != 0f;
							if (flag && flag2)
							{
								minMax = ((borderSizeOverride.max > borderSizeOverride2.max) ? borderSizeOverride : borderSizeOverride2);
							}
							else if (flag)
							{
								minMax = borderSizeOverride;
							}
							else if (flag2)
							{
								minMax = borderSizeOverride2;
							}
							border.width = seededRandom.RandomRange(minMax.min, minMax.max);
							list.Add(border);
						}
					}
				}
			}
			catch (Exception ex2)
			{
				string message2 = ex2.Message;
				string stackTrace2 = ex2.StackTrace;
				updateProgressFn(new StringKey("Exception in Border creation"), -1f, WorldGenProgressStages.Stages.Failure);
				global::Debug.LogError("Error:" + message2 + " " + stackTrace2);
			}
			try
			{
				if (this.data.world.defaultTemp == null)
				{
					this.data.world.defaultTemp = new float[this.data.world.density.Length];
				}
				for (int l = 0; l < this.data.world.defaultTemp.Length; l++)
				{
					this.data.world.defaultTemp[l] = bgTemp[l];
				}
			}
			catch (Exception ex3)
			{
				string message3 = ex3.Message;
				string stackTrace3 = ex3.StackTrace;
				updateProgressFn(new StringKey("Exception in border.defaultTemp"), -1f, WorldGenProgressStages.Stages.Failure);
				global::Debug.LogError("Error:" + message3 + " " + stackTrace3);
			}
			try
			{
				TerrainCell.SetValuesFunction setValuesFunction = delegate(int index, object elem, Sim.PhysicsData pd, Sim.DiseaseCell dc)
				{
					if (!Grid.IsValidCell(index))
					{
						global::Debug.LogError(string.Concat(new string[]
						{
							"Process::SetValuesFunction Index [",
							index.ToString(),
							"] is not valid. cells.Length [",
							map_cells.Length.ToString(),
							"]"
						}));
						return;
					}
					if (this.highPriorityClaims.Contains(index))
					{
						return;
					}
					if ((elem as Element).HasTag(GameTags.Special))
					{
						pd = (elem as Element).defaultValues;
					}
					map_cells[index].SetValues(elem as Element, pd, ElementLoader.elements);
					dcs[index] = dc;
				};
				for (int m = 0; m < list.Count; m++)
				{
					Border border2 = list[m];
					SubWorld subWorld = this.Settings.GetSubWorld(border2.neighbors.n0.node.type);
					SubWorld subWorld2 = this.Settings.GetSubWorld(border2.neighbors.n1.node.type);
					float num = (SettingsCache.temperatures[subWorld.temperatureRange].min + SettingsCache.temperatures[subWorld.temperatureRange].max) / 2f;
					float num2 = (SettingsCache.temperatures[subWorld2.temperatureRange].min + SettingsCache.temperatures[subWorld2.temperatureRange].max) / 2f;
					float num3 = Mathf.Min(SettingsCache.temperatures[subWorld.temperatureRange].min, SettingsCache.temperatures[subWorld2.temperatureRange].min);
					float num4 = Mathf.Max(SettingsCache.temperatures[subWorld.temperatureRange].max, SettingsCache.temperatures[subWorld2.temperatureRange].max);
					float num5 = (num + num2) / 2f;
					float num6 = num4 - num3;
					float num7 = 2f;
					float num8 = 5f;
					int num9 = 1;
					if (num6 >= 150f)
					{
						num7 = 0f;
						num8 = border2.width * 0.2f;
						num9 = 2;
						border2.width = Mathf.Max(border2.width, 2f);
						float num10 = num - 273.15f;
						float num11 = num2 - 273.15f;
						if (Mathf.Abs(num10) < Mathf.Abs(num11))
						{
							num5 = num;
						}
						else
						{
							num5 = num2;
						}
					}
					border2.Stagger(seededRandom, (float)seededRandom.RandomRange(8, 13), seededRandom.RandomRange(num7, num8));
					border2.ConvertToMap(this.data.world, setValuesFunction, num, num2, num5, seededRandom, num9);
				}
			}
			catch (Exception ex4)
			{
				string message4 = ex4.Message;
				string stackTrace4 = ex4.StackTrace;
				updateProgressFn(new StringKey("Exception in border.ConvertToMap"), -1f, WorldGenProgressStages.Stages.Failure);
				global::Debug.LogError("Error:" + message4 + " " + stackTrace4);
			}
		}

		// Token: 0x060077AB RID: 30635 RVA: 0x002E38A4 File Offset: 0x002E1AA4
		private void DrawWorldBorder(Sim.Cell[] cells, Chunk world, SeededRandom rnd, ref HashSet<int> borderCells, ref List<RectInt> poiBounds, WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			WorldGen.<>c__DisplayClass139_0 CS$<>8__locals1 = new WorldGen.<>c__DisplayClass139_0();
			CS$<>8__locals1.world = world;
			bool boolSetting = this.Settings.GetBoolSetting("DrawWorldBorderForce");
			int intSetting = this.Settings.GetIntSetting("WorldBorderThickness");
			int intSetting2 = this.Settings.GetIntSetting("WorldBorderRange");
			ushort idx = WorldGen.vacuumElement.idx;
			ushort idx2 = WorldGen.voidElement.idx;
			ushort idx3 = WorldGen.unobtaniumElement.idx;
			float temperature = WorldGen.unobtaniumElement.defaultValues.temperature;
			float mass = WorldGen.unobtaniumElement.defaultValues.mass;
			int num = 0;
			int num2 = 0;
			updateProgressFn(UI.WORLDGEN.DRAWWORLDBORDER.key, 0f, WorldGenProgressStages.Stages.DrawWorldBorder);
			int num3 = CS$<>8__locals1.world.size.y - 1;
			int num4 = 0;
			int num5 = CS$<>8__locals1.world.size.x - 1;
			List<TerrainCell> terrainCellsForTag = this.GetTerrainCellsForTag(WorldGenTags.RemoveWorldBorderOverVacuum);
			int y;
			int num9;
			for (y = num3; y >= 0; y = num9 - 1)
			{
				updateProgressFn(UI.WORLDGEN.DRAWWORLDBORDER.key, (float)y / (float)num3 * 0.33f, WorldGenProgressStages.Stages.DrawWorldBorder);
				num = Mathf.Max(-intSetting2, Mathf.Min(num + rnd.RandomRange(-2, 2), intSetting2));
				bool flag = terrainCellsForTag.Find((TerrainCell n) => n.poly.Contains(new Vector2(0f, (float)y))) != null;
				for (int i = 0; i < intSetting + num; i++)
				{
					int num6 = Grid.XYToCell(i, y);
					if (boolSetting || (cells[num6].elementIdx != idx && cells[num6].elementIdx != idx2 && flag) || !flag)
					{
						borderCells.Add(num6);
						cells[num6].SetValues(idx3, temperature, mass);
						num4 = Mathf.Max(num4, i);
					}
				}
				num2 = Mathf.Max(-intSetting2, Mathf.Min(num2 + rnd.RandomRange(-2, 2), intSetting2));
				bool flag2 = terrainCellsForTag.Find((TerrainCell n) => n.poly.Contains(new Vector2((float)(CS$<>8__locals1.world.size.x - 1), (float)y))) != null;
				for (int j = 0; j < intSetting + num2; j++)
				{
					int num7 = CS$<>8__locals1.world.size.x - 1 - j;
					int num8 = Grid.XYToCell(num7, y);
					if (boolSetting || (cells[num8].elementIdx != idx && cells[num8].elementIdx != idx2 && flag2) || !flag2)
					{
						borderCells.Add(num8);
						cells[num8].SetValues(idx3, temperature, mass);
						num5 = Mathf.Min(num5, num7);
					}
				}
				num9 = y;
			}
			this.POIBounds.Add(new RectInt(0, 0, num4 + 1, this.World.size.y));
			this.POIBounds.Add(new RectInt(num5, 0, CS$<>8__locals1.world.size.x - num5, this.World.size.y));
			int num10 = 0;
			int num11 = 0;
			int num12 = 0;
			int num13 = this.World.size.y - 1;
			int x;
			for (x = 0; x < CS$<>8__locals1.world.size.x; x = num9 + 1)
			{
				updateProgressFn(UI.WORLDGEN.DRAWWORLDBORDER.key, (float)x / (float)CS$<>8__locals1.world.size.x * 0.66f + 0.33f, WorldGenProgressStages.Stages.DrawWorldBorder);
				num10 = Mathf.Max(-intSetting2, Mathf.Min(num10 + rnd.RandomRange(-2, 2), intSetting2));
				bool flag3 = terrainCellsForTag.Find((TerrainCell n) => n.poly.Contains(new Vector2((float)x, 0f))) != null;
				for (int k = 0; k < intSetting + num10; k++)
				{
					int num14 = Grid.XYToCell(x, k);
					if (boolSetting || (cells[num14].elementIdx != idx && cells[num14].elementIdx != idx2 && flag3) || !flag3)
					{
						borderCells.Add(num14);
						cells[num14].SetValues(idx3, temperature, mass);
						num12 = Mathf.Max(num12, k);
					}
				}
				num11 = Mathf.Max(-intSetting2, Mathf.Min(num11 + rnd.RandomRange(-2, 2), intSetting2));
				bool flag4 = terrainCellsForTag.Find((TerrainCell n) => n.poly.Contains(new Vector2((float)x, (float)(CS$<>8__locals1.world.size.y - 1)))) != null;
				for (int l = 0; l < intSetting + num11; l++)
				{
					int num15 = CS$<>8__locals1.world.size.y - 1 - l;
					int num16 = Grid.XYToCell(x, num15);
					if (boolSetting || (cells[num16].elementIdx != idx && cells[num16].elementIdx != idx2 && flag4) || !flag4)
					{
						borderCells.Add(num16);
						cells[num16].SetValues(idx3, temperature, mass);
						num13 = Mathf.Min(num13, num15);
					}
				}
				num9 = x;
			}
			this.POIBounds.Add(new RectInt(0, 0, this.World.size.x, num12 + 1));
			this.POIBounds.Add(new RectInt(0, num13, this.World.size.x, this.World.size.y - num13));
		}

		// Token: 0x060077AC RID: 30636 RVA: 0x002E3E70 File Offset: 0x002E2070
		private void SetupNoise(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			updateProgressFn(UI.WORLDGEN.BUILDNOISESOURCE.key, 0f, WorldGenProgressStages.Stages.SetupNoise);
			this.heatSource = this.BuildNoiseSource(this.data.world.size.x, this.data.world.size.y, "noise/Heat");
			updateProgressFn(UI.WORLDGEN.BUILDNOISESOURCE.key, 1f, WorldGenProgressStages.Stages.SetupNoise);
		}

		// Token: 0x060077AD RID: 30637 RVA: 0x002E3EE8 File Offset: 0x002E20E8
		public NoiseMapBuilderPlane BuildNoiseSource(int width, int height, string name)
		{
			global::ProcGen.Noise.Tree tree = SettingsCache.noise.GetTree(name);
			global::Debug.Assert(tree != null, name);
			return this.BuildNoiseSource(width, height, tree);
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x002E3F14 File Offset: 0x002E2114
		public NoiseMapBuilderPlane BuildNoiseSource(int width, int height, global::ProcGen.Noise.Tree tree)
		{
			Vector2f lowerBound = tree.settings.lowerBound;
			Vector2f upperBound = tree.settings.upperBound;
			global::Debug.Assert(lowerBound.x < upperBound.x, string.Concat(new string[]
			{
				"BuildNoiseSource X range broken [l: ",
				lowerBound.x.ToString(),
				" h: ",
				upperBound.x.ToString(),
				"]"
			}));
			global::Debug.Assert(lowerBound.y < upperBound.y, string.Concat(new string[]
			{
				"BuildNoiseSource Y range broken [l: ",
				lowerBound.y.ToString(),
				" h: ",
				upperBound.y.ToString(),
				"]"
			}));
			global::Debug.Assert(width > 0, "BuildNoiseSource width <=0: [" + width.ToString() + "]");
			global::Debug.Assert(height > 0, "BuildNoiseSource height <=0: [" + height.ToString() + "]");
			NoiseMapBuilderPlane noiseMapBuilderPlane = new NoiseMapBuilderPlane(lowerBound.x, upperBound.x, lowerBound.y, upperBound.y, false);
			noiseMapBuilderPlane.SetSize(width, height);
			noiseMapBuilderPlane.SourceModule = tree.BuildFinalModule(this.data.globalNoiseSeed);
			return noiseMapBuilderPlane;
		}

		// Token: 0x060077AF RID: 30639 RVA: 0x002E405C File Offset: 0x002E225C
		private void GetMinMaxDataValues(float[] data, int width, int height)
		{
		}

		// Token: 0x060077B0 RID: 30640 RVA: 0x002E4060 File Offset: 0x002E2260
		public static NoiseMap BuildNoiseMap(Vector2 offset, float zoom, NoiseMapBuilderPlane nmbp, int width, int height, NoiseMapBuilderCallback cb = null)
		{
			double num = (double)offset.x;
			double num2 = (double)offset.y;
			if (zoom == 0f)
			{
				zoom = 0.01f;
			}
			double num3 = num * (double)zoom;
			double num4 = (num + (double)width) * (double)zoom;
			double num5 = num2 * (double)zoom;
			double num6 = (num2 + (double)height) * (double)zoom;
			NoiseMap noiseMap = new NoiseMap(width, height);
			nmbp.NoiseMap = noiseMap;
			nmbp.SetBounds((float)num3, (float)num4, (float)num5, (float)num6);
			nmbp.CallBack = cb;
			nmbp.Build();
			return noiseMap;
		}

		// Token: 0x060077B1 RID: 30641 RVA: 0x002E40D8 File Offset: 0x002E22D8
		public static float[] GenerateNoise(Vector2 offset, float zoom, NoiseMapBuilderPlane nmbp, int width, int height, NoiseMapBuilderCallback cb = null)
		{
			NoiseMap noiseMap = WorldGen.BuildNoiseMap(offset, zoom, nmbp, width, height, cb);
			float[] array = new float[noiseMap.Width * noiseMap.Height];
			noiseMap.CopyTo(ref array);
			return array;
		}

		// Token: 0x060077B2 RID: 30642 RVA: 0x002E4110 File Offset: 0x002E2310
		public static void Normalise(float[] data)
		{
			global::Debug.Assert(data != null && data.Length != 0, "MISSING DATA FOR NORMALIZE");
			float num = float.MaxValue;
			float num2 = float.MinValue;
			for (int i = 0; i < data.Length; i++)
			{
				num = Mathf.Min(data[i], num);
				num2 = Mathf.Max(data[i], num2);
			}
			float num3 = num2 - num;
			for (int j = 0; j < data.Length; j++)
			{
				data[j] = (data[j] - num) / num3;
			}
		}

		// Token: 0x060077B3 RID: 30643 RVA: 0x002E4184 File Offset: 0x002E2384
		private void GenerateUnChunkedNoise(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			Vector2 vector = new Vector2(0f, 0f);
			updateProgressFn(UI.WORLDGEN.GENERATENOISE.key, 0f, WorldGenProgressStages.Stages.GenerateNoise);
			NoiseMapBuilderCallback noiseMapBuilderCallback = delegate(int line)
			{
				updateProgressFn(UI.WORLDGEN.GENERATENOISE.key, (float)((int)(0f + 0.25f * ((float)line / (float)this.data.world.size.y))), WorldGenProgressStages.Stages.GenerateNoise);
			};
			noiseMapBuilderCallback = delegate(int line)
			{
				updateProgressFn(UI.WORLDGEN.GENERATENOISE.key, (float)((int)(0.25f + 0.25f * ((float)line / (float)this.data.world.size.y))), WorldGenProgressStages.Stages.GenerateNoise);
			};
			if (noiseMapBuilderCallback == null)
			{
				global::Debug.LogError("nupd is null");
			}
			this.data.world.heatOffset = WorldGen.GenerateNoise(vector, SettingsCache.noise.GetZoomForTree("noise/Heat"), this.heatSource, this.data.world.size.x, this.data.world.size.y, noiseMapBuilderCallback);
			this.data.world.data = new float[this.data.world.heatOffset.Length];
			this.data.world.density = new float[this.data.world.heatOffset.Length];
			this.data.world.overrides = new float[this.data.world.heatOffset.Length];
			updateProgressFn(UI.WORLDGEN.NORMALISENOISE.key, 0.5f, WorldGenProgressStages.Stages.GenerateNoise);
			if (SettingsCache.noise.ShouldNormaliseTree("noise/Heat"))
			{
				WorldGen.Normalise(this.data.world.heatOffset);
			}
			updateProgressFn(UI.WORLDGEN.NORMALISENOISE.key, 1f, WorldGenProgressStages.Stages.GenerateNoise);
		}

		// Token: 0x060077B4 RID: 30644 RVA: 0x002E4320 File Offset: 0x002E2520
		public void WriteOverWorldNoise(WorldGen.OfflineCallbackFunction updateProgressFn)
		{
			Dictionary<HashedString, WorldGen.NoiseNormalizationStats> dictionary = new Dictionary<HashedString, WorldGen.NoiseNormalizationStats>();
			float num = (float)this.OverworldCells.Count;
			float perCell = 1f / num;
			float currentProgress = 0f;
			foreach (TerrainCell terrainCell in this.OverworldCells)
			{
				global::ProcGen.Noise.Tree tree = SettingsCache.noise.GetTree("noise/Default");
				global::ProcGen.Noise.Tree tree2 = SettingsCache.noise.GetTree("noise/DefaultCave");
				global::ProcGen.Noise.Tree tree3 = SettingsCache.noise.GetTree("noise/DefaultDensity");
				string text = "noise/Default";
				string text2 = "noise/DefaultCave";
				string text3 = "noise/DefaultDensity";
				SubWorld subWorld = this.Settings.GetSubWorld(terrainCell.node.type);
				if (subWorld == null)
				{
					global::Debug.Log("Couldnt find Subworld for overworld node [" + terrainCell.node.type + "] using defaults");
				}
				else
				{
					if (subWorld.biomeNoise != null)
					{
						global::ProcGen.Noise.Tree tree4 = SettingsCache.noise.GetTree(subWorld.biomeNoise);
						if (tree4 != null)
						{
							tree = tree4;
							text = subWorld.biomeNoise;
						}
					}
					if (subWorld.overrideNoise != null)
					{
						global::ProcGen.Noise.Tree tree5 = SettingsCache.noise.GetTree(subWorld.overrideNoise);
						if (tree5 != null)
						{
							tree2 = tree5;
							text2 = subWorld.overrideNoise;
						}
					}
					if (subWorld.densityNoise != null)
					{
						global::ProcGen.Noise.Tree tree6 = SettingsCache.noise.GetTree(subWorld.densityNoise);
						if (tree6 != null)
						{
							tree3 = tree6;
							text3 = subWorld.densityNoise;
						}
					}
				}
				WorldGen.NoiseNormalizationStats noiseNormalizationStats;
				if (!dictionary.TryGetValue(text, out noiseNormalizationStats))
				{
					noiseNormalizationStats = new WorldGen.NoiseNormalizationStats(this.BaseNoiseMap);
					dictionary.Add(text, noiseNormalizationStats);
				}
				WorldGen.NoiseNormalizationStats noiseNormalizationStats2;
				if (!dictionary.TryGetValue(text2, out noiseNormalizationStats2))
				{
					noiseNormalizationStats2 = new WorldGen.NoiseNormalizationStats(this.OverrideMap);
					dictionary.Add(text2, noiseNormalizationStats2);
				}
				WorldGen.NoiseNormalizationStats noiseNormalizationStats3;
				if (!dictionary.TryGetValue(text3, out noiseNormalizationStats3))
				{
					noiseNormalizationStats3 = new WorldGen.NoiseNormalizationStats(this.DensityMap);
					dictionary.Add(text3, noiseNormalizationStats3);
				}
				int num2 = (int)Mathf.Ceil(terrainCell.poly.bounds.width + 2f);
				int height = (int)Mathf.Ceil(terrainCell.poly.bounds.height + 2f);
				int num3 = (int)Mathf.Floor(terrainCell.poly.bounds.xMin - 1f);
				int num4 = (int)Mathf.Floor(terrainCell.poly.bounds.yMin - 1f);
				Vector2 vector2;
				Vector2 vector = (vector2 = new Vector2((float)num3, (float)num4));
				NoiseMapBuilderCallback noiseMapBuilderCallback = delegate(int line)
				{
					updateProgressFn(UI.WORLDGEN.GENERATENOISE.key, (float)((int)(currentProgress + perCell * ((float)line / (float)height))), WorldGenProgressStages.Stages.NoiseMapBuilder);
				};
				NoiseMapBuilderPlane noiseMapBuilderPlane = this.BuildNoiseSource(num2, height, tree);
				NoiseMap noiseMap = WorldGen.BuildNoiseMap(vector, tree.settings.zoom, noiseMapBuilderPlane, num2, height, noiseMapBuilderCallback);
				NoiseMapBuilderPlane noiseMapBuilderPlane2 = this.BuildNoiseSource(num2, height, tree2);
				NoiseMap noiseMap2 = WorldGen.BuildNoiseMap(vector, tree2.settings.zoom, noiseMapBuilderPlane2, num2, height, noiseMapBuilderCallback);
				NoiseMapBuilderPlane noiseMapBuilderPlane3 = this.BuildNoiseSource(num2, height, tree3);
				NoiseMap noiseMap3 = WorldGen.BuildNoiseMap(vector, tree3.settings.zoom, noiseMapBuilderPlane3, num2, height, noiseMapBuilderCallback);
				vector2.x = (float)((int)Mathf.Floor(terrainCell.poly.bounds.xMin));
				while (vector2.x <= (float)((int)Mathf.Ceil(terrainCell.poly.bounds.xMax)))
				{
					vector2.y = (float)((int)Mathf.Floor(terrainCell.poly.bounds.yMin));
					while (vector2.y <= (float)((int)Mathf.Ceil(terrainCell.poly.bounds.yMax)))
					{
						if (terrainCell.poly.PointInPolygon(vector2))
						{
							int num5 = Grid.XYToCell((int)vector2.x, (int)vector2.y);
							if (tree.settings.normalise)
							{
								noiseNormalizationStats.cells.Add(num5);
							}
							if (tree2.settings.normalise)
							{
								noiseNormalizationStats2.cells.Add(num5);
							}
							if (tree3.settings.normalise)
							{
								noiseNormalizationStats3.cells.Add(num5);
							}
							int num6 = (int)vector2.x - num3;
							int num7 = (int)vector2.y - num4;
							this.BaseNoiseMap[num5] = noiseMap.GetValue(num6, num7);
							this.OverrideMap[num5] = noiseMap2.GetValue(num6, num7);
							this.DensityMap[num5] = noiseMap3.GetValue(num6, num7);
							noiseNormalizationStats.min = Mathf.Min(this.BaseNoiseMap[num5], noiseNormalizationStats.min);
							noiseNormalizationStats.max = Mathf.Max(this.BaseNoiseMap[num5], noiseNormalizationStats.max);
							noiseNormalizationStats2.min = Mathf.Min(this.OverrideMap[num5], noiseNormalizationStats2.min);
							noiseNormalizationStats2.max = Mathf.Max(this.OverrideMap[num5], noiseNormalizationStats2.max);
							noiseNormalizationStats3.min = Mathf.Min(this.DensityMap[num5], noiseNormalizationStats3.min);
							noiseNormalizationStats3.max = Mathf.Max(this.DensityMap[num5], noiseNormalizationStats3.max);
						}
						vector2.y += 1f;
					}
					vector2.x += 1f;
				}
			}
			foreach (KeyValuePair<HashedString, WorldGen.NoiseNormalizationStats> keyValuePair in dictionary)
			{
				float num8 = keyValuePair.Value.max - keyValuePair.Value.min;
				foreach (int num9 in keyValuePair.Value.cells)
				{
					keyValuePair.Value.noise[num9] = (keyValuePair.Value.noise[num9] - keyValuePair.Value.min) / num8;
				}
			}
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x002E49C8 File Offset: 0x002E2BC8
		private float GetValue(Chunk chunk, Vector2I pos)
		{
			int num = pos.x + this.data.world.size.x * pos.y;
			if (num < 0 || num >= chunk.data.Length)
			{
				throw new ArgumentOutOfRangeException("chunkDataIndex [" + num.ToString() + "]", "chunk data length [" + chunk.data.Length.ToString() + "]");
			}
			return chunk.data[num];
		}

		// Token: 0x060077B6 RID: 30646 RVA: 0x002E4A4C File Offset: 0x002E2C4C
		public bool InChunkRange(Chunk chunk, Vector2I pos)
		{
			int num = pos.x + this.data.world.size.x * pos.y;
			return num >= 0 && num < chunk.data.Length;
		}

		// Token: 0x060077B7 RID: 30647 RVA: 0x002E4A8F File Offset: 0x002E2C8F
		private TerrainCell.ElementOverride GetElementFromBiomeElementTable(Chunk chunk, Vector2I pos, List<ElementGradient> table, float erode)
		{
			return WorldGen.GetElementFromBiomeElementTable(this.GetValue(chunk, pos) * erode, table);
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x002E4AA4 File Offset: 0x002E2CA4
		public static TerrainCell.ElementOverride GetElementFromBiomeElementTable(float value, List<ElementGradient> table)
		{
			TerrainCell.ElementOverride elementOverride = TerrainCell.GetElementOverride(WorldGen.voidElement.tag.ToString(), null);
			if (table.Count == 0)
			{
				return elementOverride;
			}
			for (int i = 0; i < table.Count; i++)
			{
				global::Debug.Assert(table[i].content != null, i.ToString());
				if (value < table[i].maxValue)
				{
					return TerrainCell.GetElementOverride(table[i].content, table[i].overrides);
				}
			}
			return TerrainCell.GetElementOverride(table[table.Count - 1].content, table[table.Count - 1].overrides);
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x002E4B60 File Offset: 0x002E2D60
		public static bool CanLoad(string fileName)
		{
			if (fileName == null || fileName == "")
			{
				return false;
			}
			bool flag;
			try
			{
				if (File.Exists(fileName))
				{
					using (BinaryReader binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open)))
					{
						return binaryReader.BaseStream.CanRead;
					}
				}
				flag = false;
			}
			catch (FileNotFoundException)
			{
				flag = false;
			}
			catch (Exception ex)
			{
				DebugUtil.LogWarningArgs(new object[] { "Failed to read " + fileName + "\n" + ex.ToString() });
				flag = false;
			}
			return flag;
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x002E4C08 File Offset: 0x002E2E08
		public static WorldGen Load(IReader reader, bool defaultDiscovered)
		{
			WorldGen worldGen2;
			try
			{
				WorldGenSave worldGenSave = new WorldGenSave();
				Deserializer.Deserialize(worldGenSave, reader);
				WorldGen worldGen = new WorldGen(worldGenSave.worldID, worldGenSave.data, worldGenSave.traitIDs, worldGenSave.storyTraitIDs, false);
				worldGen.isStartingWorld = true;
				if (worldGenSave.version.x != 1 || worldGenSave.version.y > 1)
				{
					DebugUtil.LogErrorArgs(new object[] { string.Concat(new string[]
					{
						"LoadWorldGenSim Error! Wrong save version Current: [",
						1.ToString(),
						".",
						1.ToString(),
						"] File: [",
						worldGenSave.version.x.ToString(),
						".",
						worldGenSave.version.y.ToString(),
						"]"
					}) });
					worldGen.wasLoaded = false;
				}
				else
				{
					worldGen.wasLoaded = true;
				}
				worldGen2 = worldGen;
			}
			catch (Exception ex)
			{
				DebugUtil.LogErrorArgs(new object[] { "WorldGen.Load Error!\n", ex.Message, ex.StackTrace });
				worldGen2 = null;
			}
			return worldGen2;
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x002E4D3C File Offset: 0x002E2F3C
		public void DrawDebug()
		{
		}

		// Token: 0x040052FB RID: 21243
		private const string _WORLDGEN_SAVE_FILENAME = "WorldGenDataSave.worldgen";

		// Token: 0x040052FC RID: 21244
		private const int heatScale = 2;

		// Token: 0x040052FD RID: 21245
		private const int UNPASSABLE_EDGE_COUNT = 4;

		// Token: 0x040052FE RID: 21246
		private const string heat_noise_name = "noise/Heat";

		// Token: 0x040052FF RID: 21247
		private const string default_base_noise_name = "noise/Default";

		// Token: 0x04005300 RID: 21248
		private const string default_cave_noise_name = "noise/DefaultCave";

		// Token: 0x04005301 RID: 21249
		private const string default_density_noise_name = "noise/DefaultDensity";

		// Token: 0x04005302 RID: 21250
		public const int WORLDGEN_SAVE_MAJOR_VERSION = 1;

		// Token: 0x04005303 RID: 21251
		public const int WORLDGEN_SAVE_MINOR_VERSION = 1;

		// Token: 0x04005304 RID: 21252
		private const float EXTREME_TEMPERATURE_BORDER_RANGE = 150f;

		// Token: 0x04005305 RID: 21253
		private const float EXTREME_TEMPERATURE_BORDER_MIN_WIDTH = 2f;

		// Token: 0x04005306 RID: 21254
		public static Element voidElement;

		// Token: 0x04005307 RID: 21255
		public static Element vacuumElement;

		// Token: 0x04005308 RID: 21256
		public static Element katairiteElement;

		// Token: 0x04005309 RID: 21257
		public static Element unobtaniumElement;

		// Token: 0x0400530A RID: 21258
		private static Diseases m_diseasesDb;

		// Token: 0x0400530B RID: 21259
		public bool isRunningDebugGen;

		// Token: 0x0400530C RID: 21260
		public bool skipPlacingTemplates;

		// Token: 0x0400530E RID: 21262
		private HashSet<int> claimedCells = new HashSet<int>();

		// Token: 0x0400530F RID: 21263
		public Dictionary<int, int> claimedPOICells = new Dictionary<int, int>();

		// Token: 0x04005310 RID: 21264
		private HashSet<int> highPriorityClaims = new HashSet<int>();

		// Token: 0x04005311 RID: 21265
		public List<RectInt> POIBounds = new List<RectInt>();

		// Token: 0x04005312 RID: 21266
		public List<TemplateSpawning.TemplateSpawner> POISpawners;

		// Token: 0x04005313 RID: 21267
		private WorldGen.OfflineCallbackFunction successCallbackFn;

		// Token: 0x04005314 RID: 21268
		private bool running = true;

		// Token: 0x04005315 RID: 21269
		private Action<OfflineWorldGen.ErrorInfo> errorCallback;

		// Token: 0x04005316 RID: 21270
		private SeededRandom myRandom;

		// Token: 0x04005317 RID: 21271
		private NoiseMapBuilderPlane heatSource;

		// Token: 0x04005319 RID: 21273
		private bool wasLoaded;

		// Token: 0x0400531A RID: 21274
		public int polyIndex = -1;

		// Token: 0x0400531B RID: 21275
		public bool isStartingWorld;

		// Token: 0x0400531C RID: 21276
		public bool isModuleInterior;

		// Token: 0x0400531D RID: 21277
		private static Task loadSettingsTask;

		// Token: 0x020020A9 RID: 8361
		// (Invoke) Token: 0x0600B6E6 RID: 46822
		public delegate bool OfflineCallbackFunction(StringKey stringKeyRoot, float completePercent, WorldGenProgressStages.Stages stage);

		// Token: 0x020020AA RID: 8362
		public enum GenerateSection
		{
			// Token: 0x040094E6 RID: 38118
			SolarSystem,
			// Token: 0x040094E7 RID: 38119
			WorldNoise,
			// Token: 0x040094E8 RID: 38120
			WorldLayout,
			// Token: 0x040094E9 RID: 38121
			RenderToMap,
			// Token: 0x040094EA RID: 38122
			CollectSpawners
		}

		// Token: 0x020020AB RID: 8363
		private class NoiseNormalizationStats
		{
			// Token: 0x0600B6E9 RID: 46825 RVA: 0x003E2BFE File Offset: 0x003E0DFE
			public NoiseNormalizationStats(float[] noise)
			{
				this.noise = noise;
			}

			// Token: 0x040094EB RID: 38123
			public float[] noise;

			// Token: 0x040094EC RID: 38124
			public float min = float.MaxValue;

			// Token: 0x040094ED RID: 38125
			public float max = float.MinValue;

			// Token: 0x040094EE RID: 38126
			public HashSet<int> cells = new HashSet<int>();
		}
	}
}
