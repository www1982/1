using System;
using System.Collections.Generic;
using Delaunay.Geo;
using KSerialization;
using ProcGen;
using ProcGen.Map;
using UnityEngine;
using VoronoiTree;

namespace ProcGenGame
{
	// Token: 0x02000E9C RID: 3740
	[SerializationConfig(MemberSerialization.OptIn)]
	public class TerrainCell
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06007730 RID: 30512 RVA: 0x002DE815 File Offset: 0x002DCA15
		public Polygon poly
		{
			get
			{
				return this.site.poly;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06007731 RID: 30513 RVA: 0x002DE822 File Offset: 0x002DCA22
		// (set) Token: 0x06007732 RID: 30514 RVA: 0x002DE82A File Offset: 0x002DCA2A
		public Cell node { get; private set; }

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06007733 RID: 30515 RVA: 0x002DE833 File Offset: 0x002DCA33
		// (set) Token: 0x06007734 RID: 30516 RVA: 0x002DE83B File Offset: 0x002DCA3B
		public Diagram.Site site { get; private set; }

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06007735 RID: 30517 RVA: 0x002DE844 File Offset: 0x002DCA44
		// (set) Token: 0x06007736 RID: 30518 RVA: 0x002DE84C File Offset: 0x002DCA4C
		public Dictionary<Tag, int> distancesToTags { get; private set; }

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06007737 RID: 30519 RVA: 0x002DE855 File Offset: 0x002DCA55
		public bool HasMobs
		{
			get
			{
				return this.mobs != null && this.mobs.Count > 0;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06007738 RID: 30520 RVA: 0x002DE86F File Offset: 0x002DCA6F
		// (set) Token: 0x06007739 RID: 30521 RVA: 0x002DE877 File Offset: 0x002DCA77
		public List<KeyValuePair<int, Tag>> mobs { get; private set; }

		// Token: 0x0600773A RID: 30522 RVA: 0x002DE880 File Offset: 0x002DCA80
		protected TerrainCell()
		{
		}

		// Token: 0x0600773B RID: 30523 RVA: 0x002DE893 File Offset: 0x002DCA93
		protected TerrainCell(Cell node, Diagram.Site site, Dictionary<Tag, int> distancesToTags)
		{
			this.node = node;
			this.site = site;
			this.distancesToTags = distancesToTags;
		}

		// Token: 0x0600773C RID: 30524 RVA: 0x002DE8BB File Offset: 0x002DCABB
		public virtual void LogInfo(string evt, string param, float value)
		{
			global::Debug.Log(string.Concat(new string[]
			{
				evt,
				":",
				param,
				"=",
				value.ToString()
			}));
		}

		// Token: 0x0600773D RID: 30525 RVA: 0x002DE8F0 File Offset: 0x002DCAF0
		public void InitializeCells(HashSet<int> claimedCells)
		{
			if (this.allCells != null)
			{
				return;
			}
			this.allCells = new List<int>();
			this.availableTerrainPoints = new HashSet<int>();
			this.availableSpawnPoints = new HashSet<int>();
			int num = (int)this.poly.bounds.y;
			while ((float)num < this.poly.bounds.y + this.poly.bounds.height)
			{
				int num2 = (int)this.poly.bounds.x;
				while ((float)num2 < this.poly.bounds.x + this.poly.bounds.width)
				{
					if (this.poly.Contains(new Vector2((float)num2, (float)num)))
					{
						int num3 = Grid.XYToCell(num2, num);
						this.availableTerrainPoints.Add(num3);
						this.availableSpawnPoints.Add(num3);
						if (claimedCells.Add(num3))
						{
							this.allCells.Add(num3);
						}
					}
					num2++;
				}
				num++;
			}
			this.LogInfo("Initialise cells", "", (float)this.allCells.Count);
		}

		// Token: 0x0600773E RID: 30526 RVA: 0x002DEA22 File Offset: 0x002DCC22
		public List<int> GetAllCells()
		{
			return new List<int>(this.allCells);
		}

		// Token: 0x0600773F RID: 30527 RVA: 0x002DEA30 File Offset: 0x002DCC30
		public List<int> GetAvailableSpawnCellsAll()
		{
			List<int> list = new List<int>();
			foreach (int num in this.availableSpawnPoints)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06007740 RID: 30528 RVA: 0x002DEA8C File Offset: 0x002DCC8C
		public List<int> GetAvailableSpawnCellsFeature()
		{
			List<int> list = new List<int>();
			HashSet<int> hashSet = new HashSet<int>(this.availableSpawnPoints);
			hashSet.ExceptWith(this.availableTerrainPoints);
			foreach (int num in hashSet)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x002DEAF8 File Offset: 0x002DCCF8
		public List<int> GetAvailableSpawnCellsBiome()
		{
			List<int> list = new List<int>();
			HashSet<int> hashSet = new HashSet<int>(this.availableSpawnPoints);
			hashSet.ExceptWith(this.featureSpawnPoints);
			foreach (int num in hashSet)
			{
				list.Add(num);
			}
			return list;
		}

		// Token: 0x06007742 RID: 30530 RVA: 0x002DEB64 File Offset: 0x002DCD64
		private bool RemoveFromAvailableSpawnCells(int cell)
		{
			return this.availableSpawnPoints.Remove(cell);
		}

		// Token: 0x06007743 RID: 30531 RVA: 0x002DEB74 File Offset: 0x002DCD74
		public void AddMobs(IEnumerable<KeyValuePair<int, Tag>> newMobs)
		{
			foreach (KeyValuePair<int, Tag> keyValuePair in newMobs)
			{
				this.AddMob(keyValuePair);
			}
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x002DEBBC File Offset: 0x002DCDBC
		private void AddMob(int cellIdx, string tag)
		{
			this.AddMob(new KeyValuePair<int, Tag>(cellIdx, new Tag(tag)));
		}

		// Token: 0x06007745 RID: 30533 RVA: 0x002DEBD0 File Offset: 0x002DCDD0
		public void AddMob(KeyValuePair<int, Tag> mob)
		{
			if (this.mobs == null)
			{
				this.mobs = new List<KeyValuePair<int, Tag>>();
			}
			this.mobs.Add(mob);
			bool flag = this.RemoveFromAvailableSpawnCells(mob.Key);
			this.LogInfo("\t\t\tRemoveFromAvailableCells", mob.Value.Name + ": " + (flag ? "success" : "failed"), (float)mob.Key);
			if (!flag)
			{
				if (!this.allCells.Contains(mob.Key))
				{
					global::Debug.Assert(false, string.Concat(new string[]
					{
						"Couldnt find cell [",
						mob.Key.ToString(),
						"] we dont own, to remove for mob [",
						mob.Value.Name,
						"]"
					}));
					return;
				}
				global::Debug.Assert(false, string.Concat(new string[]
				{
					"Couldnt find cell [",
					mob.Key.ToString(),
					"] to remove for mob [",
					mob.Value.Name,
					"]"
				}));
			}
		}

		// Token: 0x06007746 RID: 30534 RVA: 0x002DECFC File Offset: 0x002DCEFC
		protected string GetSubWorldType(WorldGen worldGen)
		{
			Vector2I vector2I = new Vector2I((int)this.site.poly.Centroid().x, (int)this.site.poly.Centroid().y);
			return worldGen.GetSubWorldType(vector2I);
		}

		// Token: 0x06007747 RID: 30535 RVA: 0x002DED44 File Offset: 0x002DCF44
		protected Temperature.Range GetTemperatureRange(WorldGen worldGen)
		{
			string subWorldType = this.GetSubWorldType(worldGen);
			if (subWorldType == null)
			{
				return Temperature.Range.Mild;
			}
			if (!worldGen.Settings.HasSubworld(subWorldType))
			{
				return Temperature.Range.Mild;
			}
			return worldGen.Settings.GetSubWorld(subWorldType).temperatureRange;
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x002DED80 File Offset: 0x002DCF80
		protected void GetTemperatureRange(WorldGen worldGen, ref float min, ref float range)
		{
			Temperature.Range temperatureRange = this.GetTemperatureRange(worldGen);
			min = SettingsCache.temperatures[temperatureRange].min;
			range = SettingsCache.temperatures[temperatureRange].max - min;
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x002DEDBC File Offset: 0x002DCFBC
		protected float GetDensityMassForCell(Chunk world, int cellIdx, float mass)
		{
			if (!Grid.IsValidCell(cellIdx))
			{
				return 0f;
			}
			global::Debug.Assert(world.density[cellIdx] >= 0f && world.density[cellIdx] <= 1f, "Density [" + world.density[cellIdx].ToString() + "] out of range [0-1]");
			float num = 0.2f * (world.density[cellIdx] - 0.5f);
			float num2 = mass + mass * num;
			if (num2 > 10000f)
			{
				num2 = 10000f;
			}
			return num2;
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x002DEE4C File Offset: 0x002DD04C
		private void HandleSprinkleOfElement(WorldGenSettings settings, Tag targetTag, Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd)
		{
			Element element = ElementLoader.FindElementByName(settings.GetFeature(targetTag.Name).GetOneWeightedSimHash("SprinkleOfElementChoices", rnd).element);
			global::ProcGen.Room room = null;
			SettingsCache.rooms.TryGetValue(targetTag.Name, out room);
			SampleDescriber sampleDescriber = room;
			Sim.PhysicsData defaultValues = element.defaultValues;
			Sim.DiseaseCell invalid = Sim.DiseaseCell.Invalid;
			for (int i = 0; i < this.terrainPositions.Count; i++)
			{
				if (!(this.terrainPositions[i].Value != targetTag))
				{
					float num = rnd.RandomRange(sampleDescriber.blobSize.min, sampleDescriber.blobSize.max);
					List<Vector2I> filledCircle = global::ProcGen.Util.GetFilledCircle(Grid.CellToPos2D(this.terrainPositions[i].Key), num);
					for (int j = 0; j < filledCircle.Count; j++)
					{
						int num2 = Grid.XYToCell(filledCircle[j].x, filledCircle[j].y);
						if (Grid.IsValidCell(num2))
						{
							defaultValues.mass = this.GetDensityMassForCell(world, num2, element.defaultValues.mass);
							defaultValues.temperature = temperatureMin + world.heatOffset[num2] * temperatureRange;
							SetValues(num2, element, defaultValues, invalid);
						}
					}
				}
			}
		}

		// Token: 0x0600774B RID: 30539 RVA: 0x002DEFB4 File Offset: 0x002DD1B4
		private HashSet<Vector2I> DigFeature(global::ProcGen.Room.Shape shape, float size, List<int> bordersWidths, SeededRandom rnd, out List<Vector2I> featureCenterPoints, out List<List<Vector2I>> featureBorders)
		{
			HashSet<Vector2I> hashSet = new HashSet<Vector2I>();
			featureCenterPoints = new List<Vector2I>();
			featureBorders = new List<List<Vector2I>>();
			if (size < 1f)
			{
				return hashSet;
			}
			Vector2 vector = this.site.poly.Centroid();
			this.finalSize = size;
			switch (shape)
			{
			case global::ProcGen.Room.Shape.Circle:
				featureCenterPoints = global::ProcGen.Util.GetFilledCircle(vector, this.finalSize);
				break;
			case global::ProcGen.Room.Shape.Blob:
				featureCenterPoints = global::ProcGen.Util.GetBlob(vector, this.finalSize, rnd.RandomSource());
				break;
			case global::ProcGen.Room.Shape.Square:
				featureCenterPoints = global::ProcGen.Util.GetFilledRectangle(vector, this.finalSize, this.finalSize, rnd, 2f, 2f);
				break;
			case global::ProcGen.Room.Shape.TallThin:
				featureCenterPoints = global::ProcGen.Util.GetFilledRectangle(vector, this.finalSize / 4f, this.finalSize, rnd, 2f, 2f);
				break;
			case global::ProcGen.Room.Shape.ShortWide:
				featureCenterPoints = global::ProcGen.Util.GetFilledRectangle(vector, this.finalSize, this.finalSize / 4f, rnd, 2f, 2f);
				break;
			case global::ProcGen.Room.Shape.Splat:
				featureCenterPoints = global::ProcGen.Util.GetSplat(vector, this.finalSize, rnd.RandomSource());
				break;
			}
			hashSet.UnionWith(featureCenterPoints);
			if (featureCenterPoints.Count != 0 && bordersWidths != null && bordersWidths.Count > 0 && bordersWidths[0] > 0)
			{
				int num = 0;
				while (num < bordersWidths.Count && bordersWidths[num] > 0)
				{
					featureBorders.Add(global::ProcGen.Util.GetBorder(hashSet, bordersWidths[num]));
					hashSet.UnionWith(featureBorders[num]);
					num++;
				}
			}
			return hashSet;
		}

		// Token: 0x0600774C RID: 30540 RVA: 0x002DF150 File Offset: 0x002DD350
		public static TerrainCell.ElementOverride GetElementOverride(string element, SampleDescriber.Override overrides)
		{
			global::Debug.Assert(element != null && element.Length > 0);
			TerrainCell.ElementOverride elementOverride = new TerrainCell.ElementOverride
			{
				element = ElementLoader.FindElementByName(element)
			};
			global::Debug.Assert(elementOverride.element != null, "Couldn't find an element called " + element);
			elementOverride.pdelement = elementOverride.element.defaultValues;
			elementOverride.dc = Sim.DiseaseCell.Invalid;
			elementOverride.mass = elementOverride.element.defaultValues.mass;
			elementOverride.temperature = elementOverride.element.defaultValues.temperature;
			if (overrides == null)
			{
				return elementOverride;
			}
			elementOverride.overrideMass = false;
			elementOverride.overrideTemperature = false;
			elementOverride.overrideDiseaseIdx = false;
			elementOverride.overrideDiseaseAmount = false;
			if (overrides.massOverride != null)
			{
				elementOverride.mass = overrides.massOverride.Value;
				elementOverride.overrideMass = true;
			}
			if (overrides.massMultiplier != null)
			{
				elementOverride.mass *= overrides.massMultiplier.Value;
				elementOverride.overrideMass = true;
			}
			if (overrides.temperatureOverride != null)
			{
				elementOverride.temperature = overrides.temperatureOverride.Value;
				elementOverride.overrideTemperature = true;
			}
			if (overrides.temperatureMultiplier != null)
			{
				elementOverride.temperature *= overrides.temperatureMultiplier.Value;
				elementOverride.overrideTemperature = true;
			}
			if (overrides.diseaseOverride != null)
			{
				elementOverride.diseaseIdx = WorldGen.diseaseStats.GetIndex(overrides.diseaseOverride);
				elementOverride.overrideDiseaseIdx = true;
			}
			if (overrides.diseaseAmountOverride != null)
			{
				elementOverride.diseaseAmount = overrides.diseaseAmountOverride.Value;
				elementOverride.overrideDiseaseAmount = true;
			}
			if (elementOverride.overrideTemperature)
			{
				elementOverride.pdelement.temperature = elementOverride.temperature;
			}
			if (elementOverride.overrideMass)
			{
				elementOverride.pdelement.mass = elementOverride.mass;
			}
			if (elementOverride.overrideDiseaseIdx)
			{
				elementOverride.dc.diseaseIdx = elementOverride.diseaseIdx;
			}
			if (elementOverride.overrideDiseaseAmount)
			{
				elementOverride.dc.elementCount = elementOverride.diseaseAmount;
			}
			return elementOverride;
		}

		// Token: 0x0600774D RID: 30541 RVA: 0x002DF394 File Offset: 0x002DD594
		private bool IsFeaturePointContainedInBorder(Vector2 point, WorldGen worldGen)
		{
			if (!this.node.tags.Contains(WorldGenTags.AllowExceedNodeBorders))
			{
				return true;
			}
			if (!this.poly.Contains(point))
			{
				TerrainCell terrainCell = worldGen.TerrainCells.Find((TerrainCell x) => x.poly.Contains(point));
				if (terrainCell != null)
				{
					SubWorld subWorld = worldGen.Settings.GetSubWorld(this.node.GetSubworld());
					SubWorld subWorld2 = worldGen.Settings.GetSubWorld(terrainCell.node.GetSubworld());
					if (subWorld.zoneType != subWorld2.zoneType)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600774E RID: 30542 RVA: 0x002DF434 File Offset: 0x002DD634
		private void ApplyPlaceElementForRoom(FeatureSettings feature, string group, List<Vector2I> cells, WorldGen worldGen, Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd, HashSet<int> highPriorityClaims)
		{
			if (cells == null || cells.Count == 0)
			{
				return;
			}
			if (!feature.HasGroup(group))
			{
				return;
			}
			switch (feature.ElementChoiceGroups[group].selectionMethod)
			{
			case global::ProcGen.Room.Selection.Weighted:
			case global::ProcGen.Room.Selection.WeightedResample:
			{
				for (int i = 0; i < cells.Count; i++)
				{
					int num = Grid.XYToCell(cells[i].x, cells[i].y);
					if (Grid.IsValidCell(num) && !highPriorityClaims.Contains(num) && this.IsFeaturePointContainedInBorder(cells[i], worldGen))
					{
						WeightedSimHash oneWeightedSimHash = feature.GetOneWeightedSimHash(group, rnd);
						TerrainCell.ElementOverride elementOverride = TerrainCell.GetElementOverride(oneWeightedSimHash.element, oneWeightedSimHash.overrides);
						if (!elementOverride.overrideTemperature)
						{
							elementOverride.pdelement.temperature = temperatureMin + world.heatOffset[num] * temperatureRange;
						}
						if (!elementOverride.overrideMass)
						{
							elementOverride.pdelement.mass = this.GetDensityMassForCell(world, num, elementOverride.mass);
						}
						SetValues(num, elementOverride.element, elementOverride.pdelement, elementOverride.dc);
					}
				}
				return;
			}
			case global::ProcGen.Room.Selection.HorizontalSlice:
			{
				int num2 = int.MaxValue;
				int num3 = int.MinValue;
				for (int j = 0; j < cells.Count; j++)
				{
					num2 = Mathf.Min(cells[j].y, num2);
					num3 = Mathf.Max(cells[j].y, num3);
				}
				int num4 = num3 - num2;
				for (int k = 0; k < cells.Count; k++)
				{
					int num5 = Grid.XYToCell(cells[k].x, cells[k].y);
					if (Grid.IsValidCell(num5) && !highPriorityClaims.Contains(num5) && this.IsFeaturePointContainedInBorder(cells[k], worldGen))
					{
						float num6 = 1f - (float)(cells[k].y - num2) / (float)num4;
						WeightedSimHash weightedSimHashAtChoice = feature.GetWeightedSimHashAtChoice(group, num6);
						TerrainCell.ElementOverride elementOverride2 = TerrainCell.GetElementOverride(weightedSimHashAtChoice.element, weightedSimHashAtChoice.overrides);
						if (!elementOverride2.overrideTemperature)
						{
							elementOverride2.pdelement.temperature = temperatureMin + world.heatOffset[num5] * temperatureRange;
						}
						if (!elementOverride2.overrideMass)
						{
							elementOverride2.pdelement.mass = this.GetDensityMassForCell(world, num5, elementOverride2.mass);
						}
						SetValues(num5, elementOverride2.element, elementOverride2.pdelement, elementOverride2.dc);
					}
				}
				return;
			}
			}
			WeightedSimHash oneWeightedSimHash2 = feature.GetOneWeightedSimHash(group, rnd);
			for (int l = 0; l < cells.Count; l++)
			{
				int num7 = Grid.XYToCell(cells[l].x, cells[l].y);
				if (Grid.IsValidCell(num7) && !highPriorityClaims.Contains(num7) && this.IsFeaturePointContainedInBorder(cells[l], worldGen))
				{
					TerrainCell.ElementOverride elementOverride3 = TerrainCell.GetElementOverride(oneWeightedSimHash2.element, oneWeightedSimHash2.overrides);
					if (!elementOverride3.overrideTemperature)
					{
						elementOverride3.pdelement.temperature = temperatureMin + world.heatOffset[num7] * temperatureRange;
					}
					if (!elementOverride3.overrideMass)
					{
						elementOverride3.pdelement.mass = this.GetDensityMassForCell(world, num7, elementOverride3.mass);
					}
					SetValues(num7, elementOverride3.element, elementOverride3.pdelement, elementOverride3.dc);
				}
			}
		}

		// Token: 0x0600774F RID: 30543 RVA: 0x002DF7DC File Offset: 0x002DD9DC
		private int GetIndexForLocation(List<Vector2I> points, Mob.Location location, SeededRandom rnd)
		{
			int num = -1;
			if (points == null || points.Count == 0)
			{
				return num;
			}
			if (location == Mob.Location.Air || location == Mob.Location.Solid)
			{
				return rnd.RandomRange(0, points.Count);
			}
			for (int i = 0; i < points.Count; i++)
			{
				if (Grid.IsValidCell(Grid.XYToCell(points[i].x, points[i].y)))
				{
					if (num == -1)
					{
						num = i;
					}
					else if (location != Mob.Location.Floor)
					{
						if (location == Mob.Location.Ceiling && points[i].y > points[num].y)
						{
							num = i;
						}
					}
					else if (points[i].y < points[num].y)
					{
						num = i;
					}
				}
			}
			return num;
		}

		// Token: 0x06007750 RID: 30544 RVA: 0x002DF890 File Offset: 0x002DDA90
		private void PlaceMobsInRoom(WorldGenSettings settings, List<MobReference> mobTags, List<Vector2I> points, SeededRandom rnd)
		{
			if (points == null)
			{
				return;
			}
			if (this.mobs == null)
			{
				this.mobs = new List<KeyValuePair<int, Tag>>();
			}
			for (int i = 0; i < mobTags.Count; i++)
			{
				if (!settings.HasMob(mobTags[i].type))
				{
					global::Debug.LogError("Missing sample description for tag [" + mobTags[i].type + "]");
				}
				else
				{
					Mob mob = settings.GetMob(mobTags[i].type);
					int num = Mathf.RoundToInt(mobTags[i].count.GetRandomValueWithinRange(rnd));
					for (int j = 0; j < num; j++)
					{
						int indexForLocation = this.GetIndexForLocation(points, mob.location, rnd);
						if (indexForLocation == -1)
						{
							break;
						}
						if (points.Count <= indexForLocation)
						{
							return;
						}
						int num2 = Grid.XYToCell(points[indexForLocation].x, points[indexForLocation].y);
						points.RemoveAt(indexForLocation);
						this.AddMob(num2, mobTags[i].type);
					}
				}
			}
		}

		// Token: 0x06007751 RID: 30545 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		private int[] ConvertNoiseToPoints(float[] basenoise, float minThreshold = 0.9f, float maxThreshold = 1f)
		{
			if (basenoise == null)
			{
				return null;
			}
			List<int> list = new List<int>();
			float width = this.site.poly.bounds.width;
			float height = this.site.poly.bounds.height;
			for (float num = this.site.position.y - height / 2f; num < this.site.position.y + height / 2f; num += 1f)
			{
				for (float num2 = this.site.position.x - width / 2f; num2 < this.site.position.x + width / 2f; num2 += 1f)
				{
					int num3 = Grid.PosToCell(new Vector2(num2, num));
					if (this.site.poly.Contains(new Vector2(num2, num)))
					{
						float num4 = (float)((int)basenoise[num3]);
						if (num4 >= minThreshold && num4 <= maxThreshold && !list.Contains(num3))
						{
							list.Add(Grid.PosToCell(new Vector2(num2, num)));
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06007752 RID: 30546 RVA: 0x002DFADC File Offset: 0x002DDCDC
		private void ApplyForeground(WorldGen worldGen, Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd)
		{
			this.LogInfo("Apply foregreound", (this.node.tags != null).ToString(), (float)((this.node.tags != null) ? this.node.tags.Count : 0));
			if (this.node.tags != null)
			{
				FeatureSettings featureSettings = worldGen.Settings.TryGetFeature(this.node.type);
				this.LogInfo("\tFeature?", (featureSettings != null).ToString(), 0f);
				if (featureSettings == null && this.node.tags != null)
				{
					List<Tag> list = new List<Tag>();
					foreach (Tag tag in this.node.tags)
					{
						if (worldGen.Settings.HasFeature(tag.Name))
						{
							list.Add(tag);
						}
					}
					this.LogInfo("\tNo feature, checking possible feature tags, found", "", (float)list.Count);
					if (list.Count > 0)
					{
						Tag tag2 = list[rnd.RandomSource().Next(list.Count)];
						featureSettings = worldGen.Settings.GetFeature(tag2.Name);
						this.LogInfo("\tPicked feature", tag2.Name, 0f);
					}
				}
				if (featureSettings != null)
				{
					this.LogInfo("APPLY FOREGROUND", this.node.type, 0f);
					float num = featureSettings.blobSize.GetRandomValueWithinRange(rnd);
					float num2 = this.poly.DistanceToClosestEdge(null);
					if (!this.node.tags.Contains(WorldGenTags.AllowExceedNodeBorders) && num2 < num)
					{
						if (this.debugMode)
						{
							global::Debug.LogWarning(string.Concat(new string[]
							{
								this.node.type,
								" ",
								featureSettings.shape.ToString(),
								"  blob size too large to fit in node. Size reduced. ",
								num.ToString(),
								"->",
								(num2 - 6f).ToString()
							}));
						}
						num = num2 - 6f;
					}
					if (num <= 0f)
					{
						return;
					}
					List<Vector2I> list2;
					List<List<Vector2I>> list3;
					HashSet<Vector2I> hashSet = this.DigFeature(featureSettings.shape, num, featureSettings.borders, rnd, out list2, out list3);
					this.featureSpawnPoints = new HashSet<int>();
					foreach (Vector2I vector2I in hashSet)
					{
						this.featureSpawnPoints.Add(Grid.XYToCell(vector2I.x, vector2I.y));
					}
					this.LogInfo("\t\t", "claimed points", (float)this.featureSpawnPoints.Count);
					this.availableTerrainPoints.ExceptWith(this.featureSpawnPoints);
					this.ApplyPlaceElementForRoom(featureSettings, "RoomCenterElements", list2, worldGen, world, SetValues, temperatureMin, temperatureRange, rnd, worldGen.HighPriorityClaimedCells);
					if (list3 != null)
					{
						for (int i = 0; i < list3.Count; i++)
						{
							this.ApplyPlaceElementForRoom(featureSettings, "RoomBorderChoices" + i.ToString(), list3[i], worldGen, world, SetValues, temperatureMin, temperatureRange, rnd, worldGen.HighPriorityClaimedCells);
						}
					}
					if (featureSettings.tags.Contains(WorldGenTags.HighPriorityFeature.Name))
					{
						worldGen.AddHighPriorityCells(this.featureSpawnPoints);
					}
				}
			}
		}

		// Token: 0x06007753 RID: 30547 RVA: 0x002DFE74 File Offset: 0x002DE074
		private void ApplyBackground(WorldGen worldGen, Chunk world, TerrainCell.SetValuesFunction SetValues, float temperatureMin, float temperatureRange, SeededRandom rnd)
		{
			this.LogInfo("Apply Background", this.node.type, 0f);
			float floatSetting = worldGen.Settings.GetFloatSetting("CaveOverrideMaxValue");
			float floatSetting2 = worldGen.Settings.GetFloatSetting("CaveOverrideSliverValue");
			Leaf leafForTerrainCell = worldGen.GetLeafForTerrainCell(this);
			bool flag = leafForTerrainCell.tags.Contains(WorldGenTags.IgnoreCaveOverride);
			bool flag2 = leafForTerrainCell.tags.Contains(WorldGenTags.CaveVoidSliver);
			bool flag3 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToCentroid);
			bool flag4 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToCentroidInv);
			bool flag5 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToEdge);
			bool flag6 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToEdgeInv);
			bool flag7 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToBorder);
			bool flag8 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToBorderWeak);
			bool flag9 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToBorderInv);
			bool flag10 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToWorldTop);
			bool flag11 = leafForTerrainCell.tags.Contains(WorldGenTags.ErodePointToWorldTopOrSide);
			bool flag12 = leafForTerrainCell.tags.Contains(WorldGenTags.DistFunctionPointCentroid);
			bool flag13 = leafForTerrainCell.tags.Contains(WorldGenTags.DistFunctionPointEdge);
			this.LogInfo("Getting Element Bands", this.node.type, 0f);
			ElementBandConfiguration elementBandConfiguration = worldGen.Settings.GetElementBandForBiome(this.node.type);
			if (elementBandConfiguration == null && this.node.biomeSpecificTags != null)
			{
				this.LogInfo("\tType is not a biome, checking tags", "", (float)this.node.tags.Count);
				List<ElementBandConfiguration> list = new List<ElementBandConfiguration>();
				foreach (Tag tag in this.node.biomeSpecificTags)
				{
					ElementBandConfiguration elementBandForBiome = worldGen.Settings.GetElementBandForBiome(tag.Name);
					if (elementBandForBiome != null)
					{
						list.Add(elementBandForBiome);
						this.LogInfo("\tFound biome", tag.Name, 0f);
					}
				}
				if (list.Count > 0)
				{
					int num = rnd.RandomSource().Next(list.Count);
					elementBandConfiguration = list[num];
					this.LogInfo("\tPicked biome", "", (float)num);
				}
			}
			DebugUtil.Assert(elementBandConfiguration != null, "A node didn't get assigned a biome! ", this.node.type);
			foreach (int num2 in this.availableTerrainPoints)
			{
				Vector2I vector2I = Grid.CellToXY(num2);
				if (!worldGen.HighPriorityClaimedCells.Contains(num2))
				{
					float num3 = world.overrides[num2];
					if (!flag && num3 >= 100f)
					{
						if (num3 >= 300f)
						{
							SetValues(num2, WorldGen.voidElement, WorldGen.voidElement.defaultValues, Sim.DiseaseCell.Invalid);
						}
						else if (num3 >= 200f)
						{
							SetValues(num2, WorldGen.unobtaniumElement, WorldGen.unobtaniumElement.defaultValues, Sim.DiseaseCell.Invalid);
						}
						else
						{
							SetValues(num2, WorldGen.katairiteElement, WorldGen.katairiteElement.defaultValues, Sim.DiseaseCell.Invalid);
						}
					}
					else
					{
						float num4 = 1f;
						Vector2 vector = new Vector2((float)vector2I.x, (float)vector2I.y);
						if (flag3 || flag4)
						{
							float num5 = 15f;
							if (flag13)
							{
								float num6 = 0f;
								MathUtil.Pair<Vector2, Vector2> closestEdge = this.poly.GetClosestEdge(vector, ref num6);
								num5 = Vector2.Distance(closestEdge.First + (closestEdge.Second - closestEdge.First) * num6, vector);
							}
							num4 = Vector2.Distance(this.poly.Centroid(), vector) / num5;
							num4 = Mathf.Max(0f, Mathf.Min(1f, num4));
							if (flag4)
							{
								num4 = 1f - num4;
							}
						}
						if (flag6 || flag5)
						{
							float num7 = 0f;
							MathUtil.Pair<Vector2, Vector2> closestEdge2 = this.poly.GetClosestEdge(vector, ref num7);
							Vector2 vector2 = closestEdge2.First + (closestEdge2.Second - closestEdge2.First) * num7;
							float num8 = 15f;
							if (flag12)
							{
								num8 = Vector2.Distance(this.poly.Centroid(), vector);
							}
							num4 = Vector2.Distance(vector2, vector) / num8;
							num4 = Mathf.Max(0f, Mathf.Min(1f, num4));
							if (flag6)
							{
								num4 = 1f - num4;
							}
						}
						if (flag9 || flag7)
						{
							List<Edge> edgesWithTag = worldGen.WorldLayout.overworldGraph.GetEdgesWithTag(WorldGenTags.EdgeClosed);
							float num9 = float.MaxValue;
							foreach (Edge edge in edgesWithTag)
							{
								MathUtil.Pair<Vector2, Vector2> pair = new MathUtil.Pair<Vector2, Vector2>(edge.corner0.position, edge.corner1.position);
								float num10 = 0f;
								num9 = Mathf.Min(Mathf.Abs(MathUtil.GetClosestPointBetweenPointAndLineSegment(pair, vector, ref num10)), num9);
							}
							float num11 = (flag8 ? 7f : 20f);
							if (flag12)
							{
								num11 = Vector2.Distance(this.poly.Centroid(), vector);
							}
							num4 = num9 / num11;
							num4 = Mathf.Max(0f, Mathf.Min(1f, num4));
							if (flag9)
							{
								num4 = 1f - num4;
							}
						}
						if (flag10)
						{
							float y = (float)worldGen.WorldSize.y;
							float num12 = 38f;
							float num13 = 58f;
							float num14 = y - vector.y;
							if (num14 < num12)
							{
								num4 = 0f;
							}
							else if (num14 < num13)
							{
								num4 = Mathf.Clamp01((num14 - num12) / (num13 - num12));
							}
							else
							{
								num4 = 1f;
							}
						}
						if (flag11)
						{
							float y2 = (float)worldGen.WorldSize.y;
							int x = worldGen.WorldSize.x;
							float num15 = 2f;
							float num16 = 10f;
							float num17 = y2 - vector.y;
							float x2 = vector.x;
							float num18 = (float)x - vector.x;
							float num19 = Mathf.Min(new float[] { num17, x2, num18 });
							if (num19 < num15)
							{
								num4 = 0f;
							}
							else if (num19 < num16)
							{
								num4 = Mathf.Clamp01((num19 - num15) / (num16 - num15));
							}
							else
							{
								num4 = 1f;
							}
						}
						Element element;
						Sim.PhysicsData defaultValues;
						Sim.DiseaseCell diseaseCell;
						worldGen.GetElementForBiomePoint(world, elementBandConfiguration, vector2I, out element, out defaultValues, out diseaseCell, num4);
						defaultValues.mass += defaultValues.mass * 0.2f * (world.density[vector2I.x + world.size.x * vector2I.y] - 0.5f);
						if (!element.IsVacuum && element.id != SimHashes.Katairite && element.id != SimHashes.Unobtanium)
						{
							float num20 = temperatureMin;
							if (element.lowTempTransition != null && temperatureMin < element.lowTemp)
							{
								num20 = element.lowTemp;
							}
							defaultValues.temperature = num20 + world.heatOffset[num2] * temperatureRange;
						}
						if (element.IsSolid && !flag && num3 > floatSetting && num3 < 100f)
						{
							if (flag2 && num3 > floatSetting2)
							{
								element = WorldGen.voidElement;
							}
							else
							{
								element = WorldGen.vacuumElement;
							}
							defaultValues = element.defaultValues;
						}
						SetValues(num2, element, defaultValues, diseaseCell);
					}
				}
			}
			if (this.node.tags.Contains(WorldGenTags.SprinkleOfOxyRock))
			{
				this.HandleSprinkleOfElement(worldGen.Settings, WorldGenTags.SprinkleOfOxyRock, world, SetValues, temperatureMin, temperatureRange, rnd);
			}
			if (this.node.tags.Contains(WorldGenTags.SprinkleOfMetal))
			{
				this.HandleSprinkleOfElement(worldGen.Settings, WorldGenTags.SprinkleOfMetal, world, SetValues, temperatureMin, temperatureRange, rnd);
			}
		}

		// Token: 0x06007754 RID: 30548 RVA: 0x002E0690 File Offset: 0x002DE890
		private void GenerateActionCells(WorldGenSettings settings, Tag tag, HashSet<int> possiblePoints, SeededRandom rnd)
		{
			global::ProcGen.Room room = null;
			SettingsCache.rooms.TryGetValue(tag.Name, out room);
			SampleDescriber sampleDescriber = room;
			if (sampleDescriber == null && settings.HasMob(tag.Name))
			{
				sampleDescriber = settings.GetMob(tag.Name);
			}
			if (sampleDescriber == null)
			{
				return;
			}
			HashSet<int> hashSet = new HashSet<int>();
			float randomValueWithinRange = sampleDescriber.density.GetRandomValueWithinRange(rnd);
			SampleDescriber.PointSelectionMethod selectMethod = sampleDescriber.selectMethod;
			List<Vector2> list;
			if (selectMethod != SampleDescriber.PointSelectionMethod.RandomPoints)
			{
				if (selectMethod != SampleDescriber.PointSelectionMethod.Centroid)
				{
				}
				list = new List<Vector2>();
				list.Add(this.node.position);
			}
			else
			{
				list = PointGenerator.GetRandomPoints(this.poly, randomValueWithinRange, 0f, null, sampleDescriber.sampleBehaviour, true, rnd, true, true);
			}
			foreach (Vector2 vector in list)
			{
				int num = Grid.XYToCell((int)vector.x, (int)vector.y);
				if (possiblePoints.Contains(num))
				{
					hashSet.Add(num);
				}
			}
			if (room != null && room.mobselection == global::ProcGen.Room.Selection.None)
			{
				if (this.terrainPositions == null)
				{
					this.terrainPositions = new List<KeyValuePair<int, Tag>>();
				}
				foreach (int num2 in hashSet)
				{
					if (Grid.IsValidCell(num2))
					{
						this.terrainPositions.Add(new KeyValuePair<int, Tag>(num2, tag));
					}
				}
			}
		}

		// Token: 0x06007755 RID: 30549 RVA: 0x002E0818 File Offset: 0x002DEA18
		private void DoProcess(WorldGen worldGen, Chunk world, TerrainCell.SetValuesFunction SetValues, SeededRandom rnd)
		{
			float num = 265f;
			float num2 = 30f;
			this.InitializeCells(worldGen.ClaimedCells);
			this.GetTemperatureRange(worldGen, ref num, ref num2);
			this.ApplyForeground(worldGen, world, SetValues, num, num2, rnd);
			for (int i = 0; i < this.node.tags.Count; i++)
			{
				this.GenerateActionCells(worldGen.Settings, this.node.tags[i], this.availableTerrainPoints, rnd);
			}
			this.ApplyBackground(worldGen, world, SetValues, num, num2, rnd);
		}

		// Token: 0x06007756 RID: 30550 RVA: 0x002E08A4 File Offset: 0x002DEAA4
		public void Process(WorldGen worldGen, Sim.Cell[] cells, float[] bgTemp, Sim.DiseaseCell[] dcs, Chunk world, SeededRandom rnd)
		{
			TerrainCell.SetValuesFunction setValuesFunction = delegate(int index, object elem, Sim.PhysicsData pd, Sim.DiseaseCell dc)
			{
				if (Grid.IsValidCell(index))
				{
					if (pd.temperature == 0f || (elem as Element).HasTag(GameTags.Special))
					{
						bgTemp[index] = -1f;
					}
					cells[index].SetValues(elem as Element, pd, ElementLoader.elements);
					dcs[index] = dc;
					return;
				}
				global::Debug.LogError(string.Concat(new string[]
				{
					"Process::SetValuesFunction Index [",
					index.ToString(),
					"] is not valid. cells.Length [",
					cells.Length.ToString(),
					"]"
				}));
			};
			this.DoProcess(worldGen, world, setValuesFunction, rnd);
		}

		// Token: 0x06007757 RID: 30551 RVA: 0x002E08E4 File Offset: 0x002DEAE4
		public void Process(WorldGen worldGen, Chunk world, SeededRandom rnd)
		{
			TerrainCell.SetValuesFunction setValuesFunction = delegate(int index, object elem, Sim.PhysicsData pd, Sim.DiseaseCell dc)
			{
				SimMessages.ModifyCell(index, (elem as Element).idx, pd.temperature, pd.mass, dc.diseaseIdx, dc.elementCount, SimMessages.ReplaceType.Replace, false, -1);
			};
			this.DoProcess(worldGen, world, setValuesFunction, rnd);
		}

		// Token: 0x06007758 RID: 30552 RVA: 0x002E091C File Offset: 0x002DEB1C
		public int DistanceToTag(Tag tag)
		{
			int num;
			if (!this.distancesToTags.TryGetValue(tag, out num))
			{
				DebugUtil.DevLogError(string.Format("DistanceToTag could not find tag '{0}', did forget to include a start template?", tag));
			}
			return num;
		}

		// Token: 0x06007759 RID: 30553 RVA: 0x002E094F File Offset: 0x002DEB4F
		public bool IsSafeToSpawnPOI(List<TerrainCell> allCells, bool log = true)
		{
			return this.IsSafeToSpawnPOI(allCells, TerrainCell.noPOISpawnTags, TerrainCell.noPOISpawnTagSet, log);
		}

		// Token: 0x0600775A RID: 30554 RVA: 0x002E0963 File Offset: 0x002DEB63
		public bool IsSafeToSpawnPOIRelaxed(List<TerrainCell> allCells, bool log = true)
		{
			return this.IsSafeToSpawnPOI(allCells, TerrainCell.relaxedNoPOISpawnTags, TerrainCell.relaxedNoPOISpawnTagSet, log);
		}

		// Token: 0x0600775B RID: 30555 RVA: 0x002E0977 File Offset: 0x002DEB77
		public bool IsSafeToSpawnPOINearStart(List<TerrainCell> allCells, bool log = true)
		{
			return this.IsSafeToSpawnPOI(allCells, TerrainCell.noPOISpawnTagsNearStart, TerrainCell.noPOISpawnNearStartTagSet, log);
		}

		// Token: 0x0600775C RID: 30556 RVA: 0x002E098B File Offset: 0x002DEB8B
		public bool IsSafeToSpawnPOINearStartRelaxed(List<TerrainCell> allCells, bool log = true)
		{
			return this.IsSafeToSpawnPOI(allCells, TerrainCell.relaxedNoPOISpawnTagsAllowNearStart, TerrainCell.relaxedNoPOISpawnAllowNearStartTagSet, log);
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x002E099F File Offset: 0x002DEB9F
		private bool IsSafeToSpawnPOI(List<TerrainCell> allCells, Tag[] noSpawnTags, TagSet noSpawnTagSet, bool log)
		{
			return !this.node.tags.ContainsOne(noSpawnTagSet);
		}

		// Token: 0x040052E3 RID: 21219
		private const float MASS_VARIATION = 0.2f;

		// Token: 0x040052E8 RID: 21224
		public List<KeyValuePair<int, Tag>> terrainPositions;

		// Token: 0x040052E9 RID: 21225
		public List<KeyValuePair<int, Tag>> poi;

		// Token: 0x040052EA RID: 21226
		public List<int> neighbourTerrainCells = new List<int>();

		// Token: 0x040052EB RID: 21227
		private float finalSize;

		// Token: 0x040052EC RID: 21228
		private bool debugMode;

		// Token: 0x040052ED RID: 21229
		private List<int> allCells;

		// Token: 0x040052EE RID: 21230
		private HashSet<int> availableTerrainPoints;

		// Token: 0x040052EF RID: 21231
		private HashSet<int> featureSpawnPoints;

		// Token: 0x040052F0 RID: 21232
		private HashSet<int> availableSpawnPoints;

		// Token: 0x040052F1 RID: 21233
		public const int DONT_SET_TEMPERATURE_DEFAULTS = -1;

		// Token: 0x040052F2 RID: 21234
		private static readonly Tag[] noPOISpawnTags = new Tag[]
		{
			WorldGenTags.StartLocation,
			WorldGenTags.AtStart,
			WorldGenTags.NearStartLocation,
			WorldGenTags.POI,
			WorldGenTags.Feature
		};

		// Token: 0x040052F3 RID: 21235
		private static readonly TagSet noPOISpawnTagSet = new TagSet(TerrainCell.noPOISpawnTags);

		// Token: 0x040052F4 RID: 21236
		private static readonly Tag[] relaxedNoPOISpawnTags = new Tag[]
		{
			WorldGenTags.StartLocation,
			WorldGenTags.AtStart,
			WorldGenTags.NearStartLocation,
			WorldGenTags.POI
		};

		// Token: 0x040052F5 RID: 21237
		private static readonly TagSet relaxedNoPOISpawnTagSet = new TagSet(TerrainCell.relaxedNoPOISpawnTags);

		// Token: 0x040052F6 RID: 21238
		private static readonly Tag[] noPOISpawnTagsNearStart = new Tag[]
		{
			WorldGenTags.POI,
			WorldGenTags.Feature
		};

		// Token: 0x040052F7 RID: 21239
		private static readonly TagSet noPOISpawnNearStartTagSet = new TagSet(TerrainCell.noPOISpawnTagsNearStart);

		// Token: 0x040052F8 RID: 21240
		private static readonly Tag[] relaxedNoPOISpawnTagsAllowNearStart = new Tag[] { WorldGenTags.POI };

		// Token: 0x040052F9 RID: 21241
		private static readonly TagSet relaxedNoPOISpawnAllowNearStartTagSet = new TagSet(TerrainCell.relaxedNoPOISpawnTagsAllowNearStart);

		// Token: 0x020020A4 RID: 8356
		// (Invoke) Token: 0x0600B6DB RID: 46811
		public delegate void SetValuesFunction(int index, object elem, Sim.PhysicsData pd, Sim.DiseaseCell dc);

		// Token: 0x020020A5 RID: 8357
		public struct ElementOverride
		{
			// Token: 0x040094D4 RID: 38100
			public Element element;

			// Token: 0x040094D5 RID: 38101
			public Sim.PhysicsData pdelement;

			// Token: 0x040094D6 RID: 38102
			public Sim.DiseaseCell dc;

			// Token: 0x040094D7 RID: 38103
			public float mass;

			// Token: 0x040094D8 RID: 38104
			public float temperature;

			// Token: 0x040094D9 RID: 38105
			public byte diseaseIdx;

			// Token: 0x040094DA RID: 38106
			public int diseaseAmount;

			// Token: 0x040094DB RID: 38107
			public bool overrideMass;

			// Token: 0x040094DC RID: 38108
			public bool overrideTemperature;

			// Token: 0x040094DD RID: 38109
			public bool overrideDiseaseIdx;

			// Token: 0x040094DE RID: 38110
			public bool overrideDiseaseAmount;
		}
	}
}
