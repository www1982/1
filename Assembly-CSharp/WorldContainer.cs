using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Delaunay.Geo;
using Klei;
using KSerialization;
using ProcGen;
using ProcGenGame;
using TemplateClasses;
using TUNING;
using UnityEngine;

// Token: 0x02000BE7 RID: 3047
[SerializationConfig(MemberSerialization.OptIn)]
public class WorldContainer : KMonoBehaviour
{
	// Token: 0x1700069D RID: 1693
	// (get) Token: 0x06005B5D RID: 23389 RVA: 0x0020FA19 File Offset: 0x0020DC19
	// (set) Token: 0x06005B5E RID: 23390 RVA: 0x0020FA21 File Offset: 0x0020DC21
	[Serialize]
	public WorldInventory worldInventory { get; private set; }

	// Token: 0x1700069E RID: 1694
	// (get) Token: 0x06005B5F RID: 23391 RVA: 0x0020FA2A File Offset: 0x0020DC2A
	// (set) Token: 0x06005B60 RID: 23392 RVA: 0x0020FA32 File Offset: 0x0020DC32
	public Dictionary<Tag, float> materialNeeds { get; private set; }

	// Token: 0x1700069F RID: 1695
	// (get) Token: 0x06005B61 RID: 23393 RVA: 0x0020FA3B File Offset: 0x0020DC3B
	public bool IsModuleInterior
	{
		get
		{
			return this.isModuleInterior;
		}
	}

	// Token: 0x170006A0 RID: 1696
	// (get) Token: 0x06005B62 RID: 23394 RVA: 0x0020FA43 File Offset: 0x0020DC43
	public bool IsDiscovered
	{
		get
		{
			return this.isDiscovered || DebugHandler.RevealFogOfWar;
		}
	}

	// Token: 0x170006A1 RID: 1697
	// (get) Token: 0x06005B63 RID: 23395 RVA: 0x0020FA54 File Offset: 0x0020DC54
	public bool IsStartWorld
	{
		get
		{
			return this.isStartWorld;
		}
	}

	// Token: 0x170006A2 RID: 1698
	// (get) Token: 0x06005B64 RID: 23396 RVA: 0x0020FA5C File Offset: 0x0020DC5C
	public bool IsDupeVisited
	{
		get
		{
			return this.isDupeVisited;
		}
	}

	// Token: 0x170006A3 RID: 1699
	// (get) Token: 0x06005B65 RID: 23397 RVA: 0x0020FA64 File Offset: 0x0020DC64
	public float DupeVisitedTimestamp
	{
		get
		{
			return this.dupeVisitedTimestamp;
		}
	}

	// Token: 0x170006A4 RID: 1700
	// (get) Token: 0x06005B66 RID: 23398 RVA: 0x0020FA6C File Offset: 0x0020DC6C
	public float DiscoveryTimestamp
	{
		get
		{
			return this.discoveryTimestamp;
		}
	}

	// Token: 0x170006A5 RID: 1701
	// (get) Token: 0x06005B67 RID: 23399 RVA: 0x0020FA74 File Offset: 0x0020DC74
	public bool IsRoverVisted
	{
		get
		{
			return this.isRoverVisited;
		}
	}

	// Token: 0x170006A6 RID: 1702
	// (get) Token: 0x06005B68 RID: 23400 RVA: 0x0020FA7C File Offset: 0x0020DC7C
	public bool IsSurfaceRevealed
	{
		get
		{
			return this.isSurfaceRevealed;
		}
	}

	// Token: 0x170006A7 RID: 1703
	// (get) Token: 0x06005B69 RID: 23401 RVA: 0x0020FA84 File Offset: 0x0020DC84
	public Dictionary<string, int> SunlightFixedTraits
	{
		get
		{
			return this.sunlightFixedTraits;
		}
	}

	// Token: 0x170006A8 RID: 1704
	// (get) Token: 0x06005B6A RID: 23402 RVA: 0x0020FA8C File Offset: 0x0020DC8C
	public Dictionary<string, int> NorthernLightsFixedTraits
	{
		get
		{
			return this.northernLightsFixedTraits;
		}
	}

	// Token: 0x170006A9 RID: 1705
	// (get) Token: 0x06005B6B RID: 23403 RVA: 0x0020FA94 File Offset: 0x0020DC94
	public Dictionary<string, int> LargeImpactorFragmentsFixedTraits
	{
		get
		{
			return this.largeImpactorFragmentsFixedTraits;
		}
	}

	// Token: 0x170006AA RID: 1706
	// (get) Token: 0x06005B6C RID: 23404 RVA: 0x0020FA9C File Offset: 0x0020DC9C
	public Dictionary<string, int> CosmicRadiationFixedTraits
	{
		get
		{
			return this.cosmicRadiationFixedTraits;
		}
	}

	// Token: 0x170006AB RID: 1707
	// (get) Token: 0x06005B6D RID: 23405 RVA: 0x0020FAA4 File Offset: 0x0020DCA4
	public List<string> Biomes
	{
		get
		{
			return this.m_subworldNames;
		}
	}

	// Token: 0x170006AC RID: 1708
	// (get) Token: 0x06005B6E RID: 23406 RVA: 0x0020FAAC File Offset: 0x0020DCAC
	public List<string> GeneratedBiomes
	{
		get
		{
			return this.m_generatedSubworlds;
		}
	}

	// Token: 0x170006AD RID: 1709
	// (get) Token: 0x06005B6F RID: 23407 RVA: 0x0020FAB4 File Offset: 0x0020DCB4
	public List<string> WorldTraitIds
	{
		get
		{
			return this.m_worldTraitIds;
		}
	}

	// Token: 0x170006AE RID: 1710
	// (get) Token: 0x06005B70 RID: 23408 RVA: 0x0020FABC File Offset: 0x0020DCBC
	public List<string> StoryTraitIds
	{
		get
		{
			return this.m_storyTraitIds;
		}
	}

	// Token: 0x170006AF RID: 1711
	// (get) Token: 0x06005B71 RID: 23409 RVA: 0x0020FAC4 File Offset: 0x0020DCC4
	public AlertStateManager.Instance AlertManager
	{
		get
		{
			if (this.m_alertManager == null)
			{
				StateMachineController component = base.GetComponent<StateMachineController>();
				this.m_alertManager = component.GetSMI<AlertStateManager.Instance>();
			}
			global::Debug.Assert(this.m_alertManager != null, "AlertStateManager should never be null.");
			return this.m_alertManager;
		}
	}

	// Token: 0x06005B72 RID: 23410 RVA: 0x0020FB05 File Offset: 0x0020DD05
	public void AddTopPriorityPrioritizable(Prioritizable prioritizable)
	{
		if (!this.yellowAlertTasks.Contains(prioritizable))
		{
			this.yellowAlertTasks.Add(prioritizable);
		}
		this.RefreshHasTopPriorityChore();
	}

	// Token: 0x06005B73 RID: 23411 RVA: 0x0020FB28 File Offset: 0x0020DD28
	public void RemoveTopPriorityPrioritizable(Prioritizable prioritizable)
	{
		for (int i = this.yellowAlertTasks.Count - 1; i >= 0; i--)
		{
			if (this.yellowAlertTasks[i] == prioritizable || this.yellowAlertTasks[i].Equals(null))
			{
				this.yellowAlertTasks.RemoveAt(i);
			}
		}
		this.RefreshHasTopPriorityChore();
	}

	// Token: 0x170006B0 RID: 1712
	// (get) Token: 0x06005B74 RID: 23412 RVA: 0x0020FB87 File Offset: 0x0020DD87
	// (set) Token: 0x06005B75 RID: 23413 RVA: 0x0020FB8F File Offset: 0x0020DD8F
	public int ParentWorldId { get; private set; }

	// Token: 0x06005B76 RID: 23414 RVA: 0x0020FB98 File Offset: 0x0020DD98
	public ICollection<int> GetChildWorldIds()
	{
		return this.m_childWorlds;
	}

	// Token: 0x06005B77 RID: 23415 RVA: 0x0020FBA0 File Offset: 0x0020DDA0
	private void OnWorldRemoved(object data)
	{
		int num = ((data is int) ? ((int)data) : 255);
		if (num != 255)
		{
			this.m_childWorlds.Remove(num);
		}
	}

	// Token: 0x06005B78 RID: 23416 RVA: 0x0020FBD8 File Offset: 0x0020DDD8
	private void OnWorldParentChanged(object data)
	{
		WorldParentChangedEventArgs worldParentChangedEventArgs = data as WorldParentChangedEventArgs;
		if (worldParentChangedEventArgs == null)
		{
			return;
		}
		if (worldParentChangedEventArgs.world.ParentWorldId == this.id)
		{
			this.m_childWorlds.Add(worldParentChangedEventArgs.world.id);
		}
		if (worldParentChangedEventArgs.lastParentId == this.ParentWorldId)
		{
			this.m_childWorlds.Remove(worldParentChangedEventArgs.world.id);
		}
	}

	// Token: 0x06005B79 RID: 23417 RVA: 0x0020FC40 File Offset: 0x0020DE40
	public Quadrant[] GetQuadrantOfCell(int cell, int depth = 1)
	{
		Vector2 vector = new Vector2((float)this.WorldSize.x * Grid.CellSizeInMeters, (float)this.worldSize.y * Grid.CellSizeInMeters);
		Vector2 vector2 = Grid.CellToPos2D(Grid.XYToCell(this.WorldOffset.x, this.WorldOffset.y));
		Vector2 vector3 = Grid.CellToPos2D(cell);
		Quadrant[] array = new Quadrant[depth];
		Vector2 vector4 = new Vector2(vector2.x, (float)this.worldOffset.y + vector.y);
		Vector2 vector5 = new Vector2(vector2.x + vector.x, (float)this.worldOffset.y);
		for (int i = 0; i < depth; i++)
		{
			float num = vector5.x - vector4.x;
			float num2 = vector4.y - vector5.y;
			float num3 = num * 0.5f;
			float num4 = num2 * 0.5f;
			if (vector3.x >= vector4.x + num3 && vector3.y >= vector5.y + num4)
			{
				array[i] = Quadrant.NE;
			}
			if (vector3.x >= vector4.x + num3 && vector3.y < vector5.y + num4)
			{
				array[i] = Quadrant.SE;
			}
			if (vector3.x < vector4.x + num3 && vector3.y < vector5.y + num4)
			{
				array[i] = Quadrant.SW;
			}
			if (vector3.x < vector4.x + num3 && vector3.y >= vector5.y + num4)
			{
				array[i] = Quadrant.NW;
			}
			switch (array[i])
			{
			case Quadrant.NE:
				vector4.x += num3;
				vector5.y += num4;
				break;
			case Quadrant.NW:
				vector5.x -= num3;
				vector5.y += num4;
				break;
			case Quadrant.SW:
				vector4.y -= num4;
				vector5.x -= num3;
				break;
			case Quadrant.SE:
				vector4.x += num3;
				vector4.y -= num4;
				break;
			}
		}
		return array;
	}

	// Token: 0x06005B7A RID: 23418 RVA: 0x0020FE6D File Offset: 0x0020E06D
	[OnDeserialized]
	private void OnDeserialized()
	{
		this.ParentWorldId = this.id;
	}

	// Token: 0x06005B7B RID: 23419 RVA: 0x0020FE7C File Offset: 0x0020E07C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.worldInventory = base.GetComponent<WorldInventory>();
		this.materialNeeds = new Dictionary<Tag, float>();
		ClusterManager.Instance.RegisterWorldContainer(this);
		Game.Instance.Subscribe(880851192, new Action<object>(this.OnWorldParentChanged));
		ClusterManager.Instance.Subscribe(-1078710002, new Action<object>(this.OnWorldRemoved));
	}

	// Token: 0x06005B7C RID: 23420 RVA: 0x0020FEEC File Offset: 0x0020E0EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.gameObject.AddOrGet<InfoDescription>().DescriptionLocString = this.worldDescription;
		this.RefreshHasTopPriorityChore();
		this.UpgradeFixedTraits();
		this.RefreshFixedTraits();
		if (DlcManager.IsPureVanilla())
		{
			this.isStartWorld = true;
			this.isDupeVisited = true;
		}
	}

	// Token: 0x06005B7D RID: 23421 RVA: 0x0020FF3C File Offset: 0x0020E13C
	protected override void OnCleanUp()
	{
		SaveGame.Instance.materialSelectorSerializer.WipeWorldSelectionData(this.id);
		Game.Instance.Unsubscribe(880851192, new Action<object>(this.OnWorldParentChanged));
		ClusterManager.Instance.Unsubscribe(-1078710002, new Action<object>(this.OnWorldRemoved));
		base.OnCleanUp();
	}

	// Token: 0x06005B7E RID: 23422 RVA: 0x0020FF9C File Offset: 0x0020E19C
	private void UpgradeFixedTraits()
	{
		if (this.sunlightFixedTrait == null || this.sunlightFixedTrait == "")
		{
			new Dictionary<int, string>
			{
				{
					160000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_HIGH
				},
				{
					0,
					FIXEDTRAITS.SUNLIGHT.NAME.NONE
				},
				{
					10000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_LOW
				},
				{
					20000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_LOW
				},
				{
					30000,
					FIXEDTRAITS.SUNLIGHT.NAME.LOW
				},
				{
					35000,
					FIXEDTRAITS.SUNLIGHT.NAME.MED_LOW
				},
				{
					40000,
					FIXEDTRAITS.SUNLIGHT.NAME.MED
				},
				{
					50000,
					FIXEDTRAITS.SUNLIGHT.NAME.MED_HIGH
				},
				{
					60000,
					FIXEDTRAITS.SUNLIGHT.NAME.HIGH
				},
				{
					80000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_HIGH
				},
				{
					120000,
					FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_HIGH
				}
			}.TryGetValue(this.sunlight, out this.sunlightFixedTrait);
		}
		if (this.cosmicRadiationFixedTrait == null || this.cosmicRadiationFixedTrait == "")
		{
			new Dictionary<int, string>
			{
				{
					0,
					FIXEDTRAITS.COSMICRADIATION.NAME.NONE
				},
				{
					6,
					FIXEDTRAITS.COSMICRADIATION.NAME.VERY_VERY_LOW
				},
				{
					12,
					FIXEDTRAITS.COSMICRADIATION.NAME.VERY_LOW
				},
				{
					18,
					FIXEDTRAITS.COSMICRADIATION.NAME.LOW
				},
				{
					21,
					FIXEDTRAITS.COSMICRADIATION.NAME.MED_LOW
				},
				{
					25,
					FIXEDTRAITS.COSMICRADIATION.NAME.MED
				},
				{
					31,
					FIXEDTRAITS.COSMICRADIATION.NAME.MED_HIGH
				},
				{
					37,
					FIXEDTRAITS.COSMICRADIATION.NAME.HIGH
				},
				{
					50,
					FIXEDTRAITS.COSMICRADIATION.NAME.VERY_HIGH
				},
				{
					75,
					FIXEDTRAITS.COSMICRADIATION.NAME.VERY_VERY_HIGH
				}
			}.TryGetValue(this.cosmicRadiation, out this.cosmicRadiationFixedTrait);
		}
	}

	// Token: 0x06005B7F RID: 23423 RVA: 0x0021013D File Offset: 0x0020E33D
	private void RefreshFixedTraits()
	{
		this.sunlight = this.GetSunlightValueFromFixedTrait();
		this.cosmicRadiation = this.GetCosmicRadiationValueFromFixedTrait();
		this.northernlights = this.GetNorthernlightValueFromFixedTrait();
		this.largeImpactorFragments = this.GetLargeImpactorFragmentsValueFromFixedTrait();
	}

	// Token: 0x06005B80 RID: 23424 RVA: 0x0021016F File Offset: 0x0020E36F
	private void RefreshHasTopPriorityChore()
	{
		if (this.AlertManager != null)
		{
			this.AlertManager.SetHasTopPriorityChore(this.yellowAlertTasks.Count > 0);
		}
	}

	// Token: 0x06005B81 RID: 23425 RVA: 0x00210192 File Offset: 0x0020E392
	public List<string> GetSeasonIds()
	{
		return this.m_seasonIds;
	}

	// Token: 0x06005B82 RID: 23426 RVA: 0x0021019A File Offset: 0x0020E39A
	public bool IsRedAlert()
	{
		return this.m_alertManager.IsRedAlert();
	}

	// Token: 0x06005B83 RID: 23427 RVA: 0x002101A7 File Offset: 0x0020E3A7
	public bool IsYellowAlert()
	{
		return this.m_alertManager.IsYellowAlert();
	}

	// Token: 0x06005B84 RID: 23428 RVA: 0x002101B4 File Offset: 0x0020E3B4
	public string GetRandomName()
	{
		if (!this.overrideName.IsNullOrWhiteSpace())
		{
			return Strings.Get(this.overrideName);
		}
		return GameUtil.GenerateRandomWorldName(this.nameTables);
	}

	// Token: 0x06005B85 RID: 23429 RVA: 0x002101DF File Offset: 0x0020E3DF
	public void SetID(int id)
	{
		this.id = id;
		this.ParentWorldId = id;
	}

	// Token: 0x06005B86 RID: 23430 RVA: 0x002101F0 File Offset: 0x0020E3F0
	public void SetParentIdx(int parentIdx)
	{
		this.parentChangeArgs.lastParentId = this.ParentWorldId;
		this.parentChangeArgs.world = this;
		this.ParentWorldId = parentIdx;
		Game.Instance.Trigger(880851192, this.parentChangeArgs);
		this.parentChangeArgs.lastParentId = 255;
	}

	// Token: 0x170006B1 RID: 1713
	// (get) Token: 0x06005B87 RID: 23431 RVA: 0x00210246 File Offset: 0x0020E446
	public Vector2 minimumBounds
	{
		get
		{
			return new Vector2((float)this.worldOffset.x, (float)this.worldOffset.y);
		}
	}

	// Token: 0x170006B2 RID: 1714
	// (get) Token: 0x06005B88 RID: 23432 RVA: 0x00210268 File Offset: 0x0020E468
	public Vector2 maximumBounds
	{
		get
		{
			return new Vector2((float)(this.worldOffset.x + (this.worldSize.x - 1)), (float)(this.worldOffset.y + (this.worldSize.y - this.hiddenYOffset - 1)));
		}
	}

	// Token: 0x170006B3 RID: 1715
	// (get) Token: 0x06005B89 RID: 23433 RVA: 0x002102B5 File Offset: 0x0020E4B5
	public Vector2I WorldSize
	{
		get
		{
			return this.worldSize;
		}
	}

	// Token: 0x170006B4 RID: 1716
	// (get) Token: 0x06005B8A RID: 23434 RVA: 0x002102BD File Offset: 0x0020E4BD
	public Vector2I WorldOffset
	{
		get
		{
			return this.worldOffset;
		}
	}

	// Token: 0x170006B5 RID: 1717
	// (get) Token: 0x06005B8B RID: 23435 RVA: 0x002102C5 File Offset: 0x0020E4C5
	public int HiddenYOffset
	{
		get
		{
			return this.hiddenYOffset;
		}
	}

	// Token: 0x170006B6 RID: 1718
	// (get) Token: 0x06005B8C RID: 23436 RVA: 0x002102CD File Offset: 0x0020E4CD
	public bool FullyEnclosedBorder
	{
		get
		{
			return this.fullyEnclosedBorder;
		}
	}

	// Token: 0x170006B7 RID: 1719
	// (get) Token: 0x06005B8D RID: 23437 RVA: 0x002102D5 File Offset: 0x0020E4D5
	public int Height
	{
		get
		{
			return this.worldSize.y;
		}
	}

	// Token: 0x170006B8 RID: 1720
	// (get) Token: 0x06005B8E RID: 23438 RVA: 0x002102E2 File Offset: 0x0020E4E2
	public int Width
	{
		get
		{
			return this.worldSize.x;
		}
	}

	// Token: 0x06005B8F RID: 23439 RVA: 0x002102EF File Offset: 0x0020E4EF
	public void SetDiscovered(bool reveal_surface = false)
	{
		if (!this.isDiscovered)
		{
			this.discoveryTimestamp = GameUtil.GetCurrentTimeInCycles();
		}
		this.isDiscovered = true;
		if (reveal_surface)
		{
			this.LookAtSurface();
		}
		Game.Instance.Trigger(-521212405, this);
	}

	// Token: 0x06005B90 RID: 23440 RVA: 0x00210324 File Offset: 0x0020E524
	public void SetDupeVisited()
	{
		if (!this.isDupeVisited)
		{
			this.dupeVisitedTimestamp = GameUtil.GetCurrentTimeInCycles();
			this.isDupeVisited = true;
			Game.Instance.Trigger(-434755240, this);
		}
	}

	// Token: 0x06005B91 RID: 23441 RVA: 0x00210350 File Offset: 0x0020E550
	public void SetRoverLanded()
	{
		this.isRoverVisited = true;
	}

	// Token: 0x06005B92 RID: 23442 RVA: 0x00210359 File Offset: 0x0020E559
	public void SetRocketInteriorWorldDetails(int world_id, Vector2I size, Vector2I offset)
	{
		this.SetID(world_id);
		this.fullyEnclosedBorder = true;
		this.worldOffset = offset;
		this.worldSize = size;
		this.isDiscovered = true;
		this.isModuleInterior = true;
		this.m_seasonIds = new List<string>();
	}

	// Token: 0x06005B93 RID: 23443 RVA: 0x00210390 File Offset: 0x0020E590
	private static int IsClockwise(Vector2 first, Vector2 second, Vector2 origin)
	{
		if (first == second)
		{
			return 0;
		}
		Vector2 vector = first - origin;
		Vector2 vector2 = second - origin;
		float num = Mathf.Atan2(vector.x, vector.y);
		float num2 = Mathf.Atan2(vector2.x, vector2.y);
		if (num < num2)
		{
			return 1;
		}
		if (num > num2)
		{
			return -1;
		}
		if (vector.sqrMagnitude >= vector2.sqrMagnitude)
		{
			return -1;
		}
		return 1;
	}

	// Token: 0x06005B94 RID: 23444 RVA: 0x002103FC File Offset: 0x0020E5FC
	public void PlaceInteriorTemplate(string template_name, global::System.Action callback)
	{
		TemplateContainer template = TemplateCache.GetTemplate(template_name);
		Vector2 pos = new Vector2((float)(this.worldSize.x / 2 + this.worldOffset.x), (float)(this.worldSize.y / 2 + this.worldOffset.y));
		TemplateLoader.Stamp(template, pos, callback);
		float num = template.info.size.X / 2f;
		float num2 = template.info.size.Y / 2f;
		float num3 = Math.Max(num, num2);
		GridVisibility.Reveal((int)pos.x, (int)pos.y, (int)num3 + 3 + 5, num3 + 3f);
		WorldDetailSave clusterDetailSave = SaveLoader.Instance.clusterDetailSave;
		this.overworldCell = new WorldDetailSave.OverworldCell();
		List<Vector2> list = new List<Vector2>(template.cells.Count);
		foreach (Prefab prefab in template.buildings)
		{
			if (prefab.id == "RocketWallTile")
			{
				Vector2 vector = new Vector2((float)prefab.location_x + pos.x, (float)prefab.location_y + pos.y);
				if (vector.x > pos.x)
				{
					vector.x += 0.5f;
				}
				if (vector.y > pos.y)
				{
					vector.y += 0.5f;
				}
				list.Add(vector);
			}
		}
		list.Sort((Vector2 v1, Vector2 v2) => WorldContainer.IsClockwise(v1, v2, pos));
		Polygon polygon = new Polygon(list);
		this.overworldCell.poly = polygon;
		this.overworldCell.zoneType = SubWorld.ZoneType.RocketInterior;
		this.overworldCell.tags = new TagSet { WorldGenTags.RocketInterior };
		clusterDetailSave.overworldCells.Add(this.overworldCell);
		for (int i = 0; i < this.worldSize.y; i++)
		{
			for (int j = 0; j < this.worldSize.x; j++)
			{
				Vector2I vector2I = new Vector2I(this.worldOffset.x + j, this.worldOffset.y + i);
				int num4 = Grid.XYToCell(vector2I.x, vector2I.y);
				if (polygon.Contains(new Vector2((float)vector2I.x, (float)vector2I.y)))
				{
					SimMessages.ModifyCellWorldZone(num4, 14);
					global::World.Instance.zoneRenderData.worldZoneTypes[num4] = SubWorld.ZoneType.RocketInterior;
				}
				else
				{
					SimMessages.ModifyCellWorldZone(num4, byte.MaxValue);
					global::World.Instance.zoneRenderData.worldZoneTypes[num4] = SubWorld.ZoneType.Space;
				}
			}
		}
	}

	// Token: 0x06005B95 RID: 23445 RVA: 0x00210708 File Offset: 0x0020E908
	private int GetDefaultValueForFixedTraitCategory(Dictionary<string, int> traitCategory)
	{
		if (traitCategory == this.largeImpactorFragmentsFixedTraits)
		{
			return FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.DEFAULT_VALUE;
		}
		if (traitCategory == this.northernLightsFixedTraits)
		{
			return FIXEDTRAITS.NORTHERNLIGHTS.DEFAULT_VALUE;
		}
		if (traitCategory == this.sunlightFixedTraits)
		{
			return FIXEDTRAITS.SUNLIGHT.DEFAULT_VALUE;
		}
		if (traitCategory == this.cosmicRadiationFixedTraits)
		{
			return FIXEDTRAITS.COSMICRADIATION.DEFAULT_VALUE;
		}
		return 0;
	}

	// Token: 0x06005B96 RID: 23446 RVA: 0x00210747 File Offset: 0x0020E947
	private string GetDefaultFixedTraitFor(Dictionary<string, int> traitCategory)
	{
		if (traitCategory == this.largeImpactorFragmentsFixedTraits)
		{
			return FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.NAME.DEFAULT;
		}
		if (traitCategory == this.northernLightsFixedTraits)
		{
			return FIXEDTRAITS.NORTHERNLIGHTS.NAME.DEFAULT;
		}
		if (traitCategory == this.sunlightFixedTraits)
		{
			return FIXEDTRAITS.SUNLIGHT.NAME.DEFAULT;
		}
		if (traitCategory == this.cosmicRadiationFixedTraits)
		{
			return FIXEDTRAITS.COSMICRADIATION.NAME.DEFAULT;
		}
		return null;
	}

	// Token: 0x06005B97 RID: 23447 RVA: 0x00210788 File Offset: 0x0020E988
	private string GetFixedTraitsFor(Dictionary<string, int> traitCategory, WorldGen world)
	{
		foreach (string text in world.Settings.world.fixedTraits)
		{
			if (traitCategory.ContainsKey(text))
			{
				return text;
			}
		}
		return this.GetDefaultFixedTraitFor(traitCategory);
	}

	// Token: 0x06005B98 RID: 23448 RVA: 0x002107F4 File Offset: 0x0020E9F4
	private int GetFixedTraitValueForTrait(Dictionary<string, int> traitCategory, ref string trait)
	{
		if (trait == null)
		{
			trait = this.GetDefaultFixedTraitFor(traitCategory);
		}
		if (traitCategory.ContainsKey(trait))
		{
			return traitCategory[trait];
		}
		return this.GetDefaultValueForFixedTraitCategory(traitCategory);
	}

	// Token: 0x06005B99 RID: 23449 RVA: 0x0021081D File Offset: 0x0020EA1D
	private string GetLargeImpactorFragmentsFixedTraits(WorldGen world)
	{
		return this.GetFixedTraitsFor(this.LargeImpactorFragmentsFixedTraits, world);
	}

	// Token: 0x06005B9A RID: 23450 RVA: 0x0021082C File Offset: 0x0020EA2C
	private string GetNorthernlightFixedTraits(WorldGen world)
	{
		return this.GetFixedTraitsFor(this.northernLightsFixedTraits, world);
	}

	// Token: 0x06005B9B RID: 23451 RVA: 0x0021083B File Offset: 0x0020EA3B
	private string GetSunlightFromFixedTraits(WorldGen world)
	{
		return this.GetFixedTraitsFor(this.sunlightFixedTraits, world);
	}

	// Token: 0x06005B9C RID: 23452 RVA: 0x0021084A File Offset: 0x0020EA4A
	private string GetCosmicRadiationFromFixedTraits(WorldGen world)
	{
		return this.GetFixedTraitsFor(this.cosmicRadiationFixedTraits, world);
	}

	// Token: 0x06005B9D RID: 23453 RVA: 0x00210859 File Offset: 0x0020EA59
	private int GetLargeImpactorFragmentsValueFromFixedTrait()
	{
		return this.GetFixedTraitValueForTrait(this.largeImpactorFragmentsFixedTraits, ref this.largeImpactorFragmentsFixedTrait);
	}

	// Token: 0x06005B9E RID: 23454 RVA: 0x0021086D File Offset: 0x0020EA6D
	private int GetNorthernlightValueFromFixedTrait()
	{
		return this.GetFixedTraitValueForTrait(this.northernLightsFixedTraits, ref this.northernLightFixedTrait);
	}

	// Token: 0x06005B9F RID: 23455 RVA: 0x00210881 File Offset: 0x0020EA81
	private int GetSunlightValueFromFixedTrait()
	{
		return this.GetFixedTraitValueForTrait(this.sunlightFixedTraits, ref this.sunlightFixedTrait);
	}

	// Token: 0x06005BA0 RID: 23456 RVA: 0x00210895 File Offset: 0x0020EA95
	private int GetCosmicRadiationValueFromFixedTrait()
	{
		return this.GetFixedTraitValueForTrait(this.cosmicRadiationFixedTraits, ref this.cosmicRadiationFixedTrait);
	}

	// Token: 0x06005BA1 RID: 23457 RVA: 0x002108AC File Offset: 0x0020EAAC
	public void SetWorldDetails(WorldGen world)
	{
		if (world != null)
		{
			this.fullyEnclosedBorder = world.Settings.GetBoolSetting("DrawWorldBorder") && world.Settings.GetBoolSetting("DrawWorldBorderOverVacuum");
			this.worldOffset = world.GetPosition();
			this.worldSize = world.GetSize();
			this.hiddenYOffset = world.HiddenYOffset;
			this.isDiscovered = world.isStartingWorld;
			this.isStartWorld = world.isStartingWorld;
			this.worldName = world.Settings.world.filePath;
			this.nameTables = world.Settings.world.nameTables;
			this.worldTags = ((world.Settings.world.worldTags != null) ? world.Settings.world.worldTags.ToArray().ToTagArray() : new Tag[0]);
			this.worldDescription = world.Settings.world.description;
			this.worldType = world.Settings.world.name;
			this.isModuleInterior = world.Settings.world.moduleInterior;
			this.m_seasonIds = new List<string>(world.Settings.world.seasons);
			this.m_generatedSubworlds = world.Settings.world.generatedSubworlds;
			this.largeImpactorFragmentsFixedTrait = this.GetLargeImpactorFragmentsFixedTraits(world);
			this.northernLightFixedTrait = this.GetNorthernlightFixedTraits(world);
			this.sunlightFixedTrait = this.GetSunlightFromFixedTraits(world);
			this.cosmicRadiationFixedTrait = this.GetCosmicRadiationFromFixedTraits(world);
			this.sunlight = this.GetSunlightValueFromFixedTrait();
			this.northernlights = this.GetNorthernlightValueFromFixedTrait();
			this.cosmicRadiation = this.GetCosmicRadiationValueFromFixedTrait();
			this.currentCosmicIntensity = (float)this.cosmicRadiation;
			HashSet<string> hashSet = new HashSet<string>();
			foreach (string text in world.Settings.world.generatedSubworlds)
			{
				text = text.Substring(0, text.LastIndexOf('/'));
				text = text.Substring(text.LastIndexOf('/') + 1, text.Length - (text.LastIndexOf('/') + 1));
				hashSet.Add(text);
			}
			this.m_subworldNames = hashSet.ToList<string>();
			this.m_worldTraitIds = new List<string>();
			this.m_worldTraitIds.AddRange(world.Settings.GetWorldTraitIDs());
			this.m_storyTraitIds = new List<string>();
			this.m_storyTraitIds.AddRange(world.Settings.GetStoryTraitIDs());
			return;
		}
		this.fullyEnclosedBorder = false;
		this.worldOffset = Vector2I.zero;
		this.worldSize = new Vector2I(Grid.WidthInCells, Grid.HeightInCells);
		this.isDiscovered = true;
		this.isStartWorld = true;
		this.isDupeVisited = true;
		this.m_seasonIds = new List<string> { Db.Get().GameplaySeasons.MeteorShowers.Id };
	}

	// Token: 0x06005BA2 RID: 23458 RVA: 0x00210BA0 File Offset: 0x0020EDA0
	public bool ContainsPoint(Vector2 point)
	{
		return point.x >= (float)this.worldOffset.x && point.y >= (float)this.worldOffset.y && point.x < (float)(this.worldOffset.x + this.worldSize.x) && point.y < (float)(this.worldOffset.y + this.worldSize.y);
	}

	// Token: 0x06005BA3 RID: 23459 RVA: 0x00210C18 File Offset: 0x0020EE18
	public void LookAtSurface()
	{
		if (!this.IsDupeVisited)
		{
			this.RevealSurface();
		}
		Vector3? vector = this.SetSurfaceCameraPos();
		if (ClusterManager.Instance.activeWorldId == this.id && vector != null)
		{
			CameraController.Instance.SnapTo(vector.Value);
		}
	}

	// Token: 0x06005BA4 RID: 23460 RVA: 0x00210C68 File Offset: 0x0020EE68
	public void RevealSurface()
	{
		if (this.isSurfaceRevealed)
		{
			return;
		}
		this.isSurfaceRevealed = true;
		for (int i = 0; i < this.worldSize.x; i++)
		{
			for (int j = this.worldSize.y - 1; j >= 0; j--)
			{
				int num = Grid.XYToCell(i + this.worldOffset.x, j + this.worldOffset.y);
				if (!Grid.IsValidCell(num) || Grid.IsSolidCell(num) || Grid.IsLiquid(num))
				{
					break;
				}
				GridVisibility.Reveal(i + this.worldOffset.X, j + this.worldOffset.y, 7, 1f);
			}
		}
	}

	// Token: 0x06005BA5 RID: 23461 RVA: 0x00210D13 File Offset: 0x0020EF13
	public void RevealHiddenY()
	{
		this.hiddenYOffset = 0;
	}

	// Token: 0x06005BA6 RID: 23462 RVA: 0x00210D1C File Offset: 0x0020EF1C
	private Vector3? SetSurfaceCameraPos()
	{
		if (SaveGame.Instance != null)
		{
			int num = (int)this.maximumBounds.y;
			for (int i = 0; i < this.worldSize.X; i++)
			{
				for (int j = this.worldSize.y - 1; j >= 0; j--)
				{
					int num2 = j + this.worldOffset.y;
					int num3 = Grid.XYToCell(i + this.worldOffset.x, num2);
					if (Grid.IsValidCell(num3) && (Grid.Solid[num3] || Grid.IsLiquid(num3)))
					{
						num = Math.Min(num, num2);
						break;
					}
				}
			}
			int num4 = (num + this.worldOffset.y + this.worldSize.y) / 2;
			Vector3 vector = new Vector3((float)(this.WorldOffset.x + this.Width / 2), (float)num4, 0f);
			SaveGame.Instance.GetComponent<UserNavigation>().SetWorldCameraStartPosition(this.id, vector);
			return new Vector3?(vector);
		}
		return null;
	}

	// Token: 0x06005BA7 RID: 23463 RVA: 0x00210E30 File Offset: 0x0020F030
	public void EjectAllDupes(Vector3 spawn_pos)
	{
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.GetWorldItems(this.id, false))
		{
			minionIdentity.transform.SetLocalPosition(spawn_pos);
		}
	}

	// Token: 0x06005BA8 RID: 23464 RVA: 0x00210E94 File Offset: 0x0020F094
	public void SpacePodAllDupes(AxialI sourceLocation, SimHashes podElement)
	{
		foreach (MinionIdentity minionIdentity in Components.MinionIdentities.GetWorldItems(this.id, false))
		{
			if (!minionIdentity.HasTag(GameTags.Dead))
			{
				Vector3 vector = new Vector3(-1f, -1f, 0f);
				GameObject gameObject = global::Util.KInstantiate(Assets.GetPrefab("EscapePod"), vector);
				gameObject.GetComponent<PrimaryElement>().SetElement(podElement, true);
				gameObject.SetActive(true);
				gameObject.GetComponent<MinionStorage>().SerializeMinion(minionIdentity.gameObject);
				TravellingCargoLander.StatesInstance smi = gameObject.GetSMI<TravellingCargoLander.StatesInstance>();
				smi.StartSM();
				smi.Travel(sourceLocation, ClusterUtil.ClosestVisibleAsteroidToLocation(sourceLocation).Location);
			}
		}
	}

	// Token: 0x06005BA9 RID: 23465 RVA: 0x00210F6C File Offset: 0x0020F16C
	public void DestroyWorldBuildings(out HashSet<int> noRefundTiles)
	{
		this.TransferBuildingMaterials(out noRefundTiles);
		foreach (ClustercraftInteriorDoor clustercraftInteriorDoor in Components.ClusterCraftInteriorDoors.GetWorldItems(this.id, false))
		{
			clustercraftInteriorDoor.DeleteObject();
		}
		this.ClearWorldZones();
	}

	// Token: 0x06005BAA RID: 23466 RVA: 0x00210FD4 File Offset: 0x0020F1D4
	public void TransferResourcesToParentWorld(Vector3 spawn_pos, HashSet<int> noRefundTiles)
	{
		this.TransferPickupables(spawn_pos);
		this.TransferLiquidsSolidsAndGases(spawn_pos, noRefundTiles);
	}

	// Token: 0x06005BAB RID: 23467 RVA: 0x00210FE8 File Offset: 0x0020F1E8
	public void TransferResourcesToDebris(AxialI sourceLocation, HashSet<int> noRefundTiles, SimHashes debrisContainerElement)
	{
		List<Storage> list = new List<Storage>();
		this.TransferPickupablesToDebris(ref list, debrisContainerElement);
		this.TransferLiquidsSolidsAndGasesToDebris(ref list, noRefundTiles, debrisContainerElement);
		foreach (Storage storage in list)
		{
			RailGunPayload.StatesInstance smi = storage.GetSMI<RailGunPayload.StatesInstance>();
			smi.StartSM();
			smi.Travel(sourceLocation, ClusterUtil.ClosestVisibleAsteroidToLocation(sourceLocation).Location);
		}
	}

	// Token: 0x06005BAC RID: 23468 RVA: 0x00211064 File Offset: 0x0020F264
	private void TransferBuildingMaterials(out HashSet<int> noRefundTiles)
	{
		HashSet<int> retTemplateFoundationCells = new HashSet<int>();
		ListPool<ScenePartitionerEntry, ClusterManager>.PooledList pooledList = ListPool<ScenePartitionerEntry, ClusterManager>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)this.minimumBounds.x, (int)this.minimumBounds.y, this.Width, this.Height, GameScenePartitioner.Instance.completeBuildings, pooledList);
		Action<int> <>9__0;
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			BuildingComplete buildingComplete = scenePartitionerEntry.obj as BuildingComplete;
			if (buildingComplete != null)
			{
				Deconstructable component = buildingComplete.GetComponent<Deconstructable>();
				if (component != null && !buildingComplete.HasTag(GameTags.NoRocketRefund))
				{
					PrimaryElement component2 = buildingComplete.GetComponent<PrimaryElement>();
					float temperature = component2.Temperature;
					byte diseaseIdx = component2.DiseaseIdx;
					int diseaseCount = component2.DiseaseCount;
					int num = 0;
					while (num < component.constructionElements.Length && buildingComplete.Def.Mass.Length > num)
					{
						Element element = ElementLoader.GetElement(component.constructionElements[num]);
						if (element != null)
						{
							element.substance.SpawnResource(buildingComplete.transform.GetPosition(), buildingComplete.Def.Mass[num], temperature, diseaseIdx, diseaseCount, false, false, false);
						}
						else
						{
							GameObject prefab = Assets.GetPrefab(component.constructionElements[num]);
							int num2 = 0;
							while ((float)num2 < buildingComplete.Def.Mass[num])
							{
								GameUtil.KInstantiate(prefab, buildingComplete.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0).SetActive(true);
								num2++;
							}
						}
						num++;
					}
				}
				SimCellOccupier component3 = buildingComplete.GetComponent<SimCellOccupier>();
				if (component3 != null && component3.doReplaceElement)
				{
					Building building = buildingComplete;
					Action<int> action;
					if ((action = <>9__0) == null)
					{
						action = (<>9__0 = delegate(int cell)
						{
							retTemplateFoundationCells.Add(cell);
						});
					}
					building.RunOnArea(action);
				}
				Storage component4 = buildingComplete.GetComponent<Storage>();
				if (component4 != null)
				{
					component4.DropAll(false, false, default(Vector3), true, null);
				}
				PlantablePlot component5 = buildingComplete.GetComponent<PlantablePlot>();
				if (component5 != null)
				{
					SeedProducer seedProducer = ((component5.Occupant != null) ? component5.Occupant.GetComponent<SeedProducer>() : null);
					if (seedProducer != null)
					{
						seedProducer.DropSeed(null);
					}
				}
				buildingComplete.DeleteObject();
			}
		}
		pooledList.Clear();
		noRefundTiles = retTemplateFoundationCells;
	}

	// Token: 0x06005BAD RID: 23469 RVA: 0x002112F4 File Offset: 0x0020F4F4
	private void TransferPickupables(Vector3 pos)
	{
		int num = Grid.PosToCell(pos);
		ListPool<ScenePartitionerEntry, ClusterManager>.PooledList pooledList = ListPool<ScenePartitionerEntry, ClusterManager>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)this.minimumBounds.x, (int)this.minimumBounds.y, this.Width, this.Height, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			if (scenePartitionerEntry.obj != null)
			{
				Pickupable pickupable = scenePartitionerEntry.obj as Pickupable;
				if (pickupable != null)
				{
					pickupable.gameObject.transform.SetLocalPosition(Grid.CellToPosCBC(num, Grid.SceneLayer.Move));
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06005BAE RID: 23470 RVA: 0x002113C0 File Offset: 0x0020F5C0
	private void TransferLiquidsSolidsAndGases(Vector3 pos, HashSet<int> noRefundTiles)
	{
		int num = (int)this.minimumBounds.x;
		while ((float)num <= this.maximumBounds.x)
		{
			int num2 = (int)this.minimumBounds.y;
			while ((float)num2 <= this.maximumBounds.y)
			{
				int num3 = Grid.XYToCell(num, num2);
				if (!noRefundTiles.Contains(num3))
				{
					Element element = Grid.Element[num3];
					if (element != null && !element.IsVacuum)
					{
						element.substance.SpawnResource(pos, Grid.Mass[num3], Grid.Temperature[num3], Grid.DiseaseIdx[num3], Grid.DiseaseCount[num3], false, false, false);
					}
				}
				num2++;
			}
			num++;
		}
	}

	// Token: 0x06005BAF RID: 23471 RVA: 0x00211478 File Offset: 0x0020F678
	private void TransferPickupablesToDebris(ref List<Storage> debrisObjects, SimHashes debrisContainerElement)
	{
		ListPool<ScenePartitionerEntry, ClusterManager>.PooledList pooledList = ListPool<ScenePartitionerEntry, ClusterManager>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)this.minimumBounds.x, (int)this.minimumBounds.y, this.Width, this.Height, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			if (scenePartitionerEntry.obj != null)
			{
				Pickupable pickupable = scenePartitionerEntry.obj as Pickupable;
				if (pickupable != null)
				{
					if (pickupable.KPrefabID.HasTag(GameTags.BaseMinion))
					{
						global::Util.KDestroyGameObject(pickupable.gameObject);
					}
					else
					{
						pickupable.PrimaryElement.Units = (float)Mathf.Max(1, Mathf.RoundToInt(pickupable.PrimaryElement.Units * 0.5f));
						if ((debrisObjects.Count == 0 || debrisObjects[debrisObjects.Count - 1].RemainingCapacity() == 0f) && pickupable.PrimaryElement.Mass > 0f)
						{
							debrisObjects.Add(CraftModuleInterface.SpawnRocketDebris(" from World Objects", debrisContainerElement));
						}
						Storage storage = debrisObjects[debrisObjects.Count - 1];
						while (pickupable.PrimaryElement.Mass > storage.RemainingCapacity())
						{
							int num = Mathf.Max(1, Mathf.RoundToInt(storage.RemainingCapacity() / pickupable.PrimaryElement.MassPerUnit));
							Pickupable pickupable2 = pickupable.Take((float)num);
							storage.Store(pickupable2.gameObject, false, false, true, false);
							storage = CraftModuleInterface.SpawnRocketDebris(" from World Objects", debrisContainerElement);
							debrisObjects.Add(storage);
						}
						if (pickupable.PrimaryElement.Mass > 0f)
						{
							storage.Store(pickupable.gameObject, false, false, true, false);
						}
					}
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06005BB0 RID: 23472 RVA: 0x0021166C File Offset: 0x0020F86C
	private void TransferLiquidsSolidsAndGasesToDebris(ref List<Storage> debrisObjects, HashSet<int> noRefundTiles, SimHashes debrisContainerElement)
	{
		int num = (int)this.minimumBounds.x;
		while ((float)num <= this.maximumBounds.x)
		{
			int num2 = (int)this.minimumBounds.y;
			while ((float)num2 <= this.maximumBounds.y)
			{
				int num3 = Grid.XYToCell(num, num2);
				if (!noRefundTiles.Contains(num3))
				{
					Element element = Grid.Element[num3];
					if (element != null && !element.IsVacuum)
					{
						float num4 = Grid.Mass[num3];
						num4 *= 0.5f;
						if ((debrisObjects.Count == 0 || debrisObjects[debrisObjects.Count - 1].RemainingCapacity() == 0f) && num4 > 0f)
						{
							debrisObjects.Add(CraftModuleInterface.SpawnRocketDebris(" from World Tiles", debrisContainerElement));
						}
						Storage storage = debrisObjects[debrisObjects.Count - 1];
						while (num4 > 0f)
						{
							float num5 = Mathf.Min(num4, storage.RemainingCapacity());
							num4 -= num5;
							storage.AddOre(element.id, num5, Grid.Temperature[num3], Grid.DiseaseIdx[num3], Grid.DiseaseCount[num3], false, true);
							if (num4 > 0f)
							{
								storage = CraftModuleInterface.SpawnRocketDebris(" from World Tiles", debrisContainerElement);
								debrisObjects.Add(storage);
							}
						}
					}
				}
				num2++;
			}
			num++;
		}
	}

	// Token: 0x06005BB1 RID: 23473 RVA: 0x002117D4 File Offset: 0x0020F9D4
	public void CancelChores()
	{
		for (int i = 0; i < 45; i++)
		{
			int num = (int)this.minimumBounds.x;
			while ((float)num <= this.maximumBounds.x)
			{
				int num2 = (int)this.minimumBounds.y;
				while ((float)num2 <= this.maximumBounds.y)
				{
					int num3 = Grid.XYToCell(num, num2);
					GameObject gameObject = Grid.Objects[num3, i];
					if (gameObject != null)
					{
						gameObject.Trigger(2127324410, true);
					}
					num2++;
				}
				num++;
			}
		}
		List<Chore> list;
		GlobalChoreProvider.Instance.choreWorldMap.TryGetValue(this.id, out list);
		int num4 = 0;
		while (list != null && num4 < list.Count)
		{
			Chore chore = list[num4];
			if (chore != null && chore.target != null && !chore.isNull)
			{
				chore.Cancel("World destroyed");
			}
			num4++;
		}
		List<FetchChore> list2;
		GlobalChoreProvider.Instance.fetchMap.TryGetValue(this.id, out list2);
		int num5 = 0;
		while (list2 != null && num5 < list2.Count)
		{
			FetchChore fetchChore = list2[num5];
			if (fetchChore != null && fetchChore.target != null && !fetchChore.isNull)
			{
				fetchChore.Cancel("World destroyed");
			}
			num5++;
		}
	}

	// Token: 0x06005BB2 RID: 23474 RVA: 0x0021192C File Offset: 0x0020FB2C
	public void ClearWorldZones()
	{
		if (this.overworldCell != null)
		{
			WorldDetailSave clusterDetailSave = SaveLoader.Instance.clusterDetailSave;
			int num = -1;
			for (int i = 0; i < SaveLoader.Instance.clusterDetailSave.overworldCells.Count; i++)
			{
				WorldDetailSave.OverworldCell overworldCell = SaveLoader.Instance.clusterDetailSave.overworldCells[i];
				if (overworldCell.zoneType == this.overworldCell.zoneType && overworldCell.tags != null && this.overworldCell.tags != null && overworldCell.tags.ContainsAll(this.overworldCell.tags) && overworldCell.poly.bounds == this.overworldCell.poly.bounds)
				{
					num = i;
					break;
				}
			}
			if (num >= 0)
			{
				clusterDetailSave.overworldCells.RemoveAt(num);
			}
		}
		int num2 = (int)this.minimumBounds.y;
		while ((float)num2 <= this.maximumBounds.y)
		{
			int num3 = (int)this.minimumBounds.x;
			while ((float)num3 <= this.maximumBounds.x)
			{
				SimMessages.ModifyCellWorldZone(Grid.XYToCell(num3, num2), byte.MaxValue);
				num3++;
			}
			num2++;
		}
	}

	// Token: 0x06005BB3 RID: 23475 RVA: 0x00211A64 File Offset: 0x0020FC64
	public int GetSafeCell()
	{
		if (this.IsModuleInterior)
		{
			using (List<RocketControlStation>.Enumerator enumerator = Components.RocketControlStations.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RocketControlStation rocketControlStation = enumerator.Current;
					if (rocketControlStation.GetMyWorldId() == this.id)
					{
						return Grid.PosToCell(rocketControlStation);
					}
				}
				goto IL_00A2;
			}
		}
		foreach (Telepad telepad in Components.Telepads.Items)
		{
			if (telepad.GetMyWorldId() == this.id)
			{
				return Grid.PosToCell(telepad);
			}
		}
		IL_00A2:
		return Grid.XYToCell(this.worldOffset.x + this.worldSize.x / 2, this.worldOffset.y + this.worldSize.y / 2);
	}

	// Token: 0x06005BB4 RID: 23476 RVA: 0x00211B68 File Offset: 0x0020FD68
	public string GetStatus()
	{
		return ColonyDiagnosticUtility.Instance.GetWorldDiagnosticResultStatus(this.id);
	}

	// Token: 0x04003C9C RID: 15516
	[Serialize]
	public int id = -1;

	// Token: 0x04003C9D RID: 15517
	[Serialize]
	public Tag prefabTag;

	// Token: 0x04003CA0 RID: 15520
	[Serialize]
	private Vector2I worldOffset;

	// Token: 0x04003CA1 RID: 15521
	[Serialize]
	private Vector2I worldSize;

	// Token: 0x04003CA2 RID: 15522
	[Serialize]
	private bool fullyEnclosedBorder;

	// Token: 0x04003CA3 RID: 15523
	[Serialize]
	private int hiddenYOffset;

	// Token: 0x04003CA4 RID: 15524
	[Serialize]
	private bool isModuleInterior;

	// Token: 0x04003CA5 RID: 15525
	[Serialize]
	private WorldDetailSave.OverworldCell overworldCell;

	// Token: 0x04003CA6 RID: 15526
	[Serialize]
	private bool isDiscovered;

	// Token: 0x04003CA7 RID: 15527
	[Serialize]
	private bool isStartWorld;

	// Token: 0x04003CA8 RID: 15528
	[Serialize]
	private bool isDupeVisited;

	// Token: 0x04003CA9 RID: 15529
	[Serialize]
	private float dupeVisitedTimestamp = -1f;

	// Token: 0x04003CAA RID: 15530
	[Serialize]
	private float discoveryTimestamp = -1f;

	// Token: 0x04003CAB RID: 15531
	[Serialize]
	private bool isRoverVisited;

	// Token: 0x04003CAC RID: 15532
	[Serialize]
	private bool isSurfaceRevealed;

	// Token: 0x04003CAD RID: 15533
	[Serialize]
	public string worldName;

	// Token: 0x04003CAE RID: 15534
	[Serialize]
	public string[] nameTables;

	// Token: 0x04003CAF RID: 15535
	[Serialize]
	public Tag[] worldTags;

	// Token: 0x04003CB0 RID: 15536
	[Serialize]
	public string overrideName;

	// Token: 0x04003CB1 RID: 15537
	[Serialize]
	public string worldType;

	// Token: 0x04003CB2 RID: 15538
	[Serialize]
	public string worldDescription;

	// Token: 0x04003CB3 RID: 15539
	[Serialize]
	public int northernlights = FIXEDTRAITS.NORTHERNLIGHTS.DEFAULT_VALUE;

	// Token: 0x04003CB4 RID: 15540
	[Serialize]
	public int largeImpactorFragments = FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.DEFAULT_VALUE;

	// Token: 0x04003CB5 RID: 15541
	[Serialize]
	public int sunlight = FIXEDTRAITS.SUNLIGHT.DEFAULT_VALUE;

	// Token: 0x04003CB6 RID: 15542
	[Serialize]
	public int cosmicRadiation = FIXEDTRAITS.COSMICRADIATION.DEFAULT_VALUE;

	// Token: 0x04003CB7 RID: 15543
	[Serialize]
	public float currentSunlightIntensity;

	// Token: 0x04003CB8 RID: 15544
	[Serialize]
	public float currentCosmicIntensity = (float)FIXEDTRAITS.COSMICRADIATION.DEFAULT_VALUE;

	// Token: 0x04003CB9 RID: 15545
	[Serialize]
	public string sunlightFixedTrait;

	// Token: 0x04003CBA RID: 15546
	[Serialize]
	public string cosmicRadiationFixedTrait;

	// Token: 0x04003CBB RID: 15547
	[Serialize]
	public string northernLightFixedTrait;

	// Token: 0x04003CBC RID: 15548
	[Serialize]
	public string largeImpactorFragmentsFixedTrait;

	// Token: 0x04003CBD RID: 15549
	[Serialize]
	public int fixedTraitsUpdateVersion = 1;

	// Token: 0x04003CBE RID: 15550
	private Dictionary<string, int> sunlightFixedTraits = new Dictionary<string, int>
	{
		{
			FIXEDTRAITS.SUNLIGHT.NAME.NONE,
			FIXEDTRAITS.SUNLIGHT.NONE
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_LOW,
			FIXEDTRAITS.SUNLIGHT.VERY_VERY_LOW
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_LOW,
			FIXEDTRAITS.SUNLIGHT.VERY_LOW
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.LOW,
			FIXEDTRAITS.SUNLIGHT.LOW
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.MED_LOW,
			FIXEDTRAITS.SUNLIGHT.MED_LOW
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.MED,
			FIXEDTRAITS.SUNLIGHT.MED
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.MED_HIGH,
			FIXEDTRAITS.SUNLIGHT.MED_HIGH
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.HIGH,
			FIXEDTRAITS.SUNLIGHT.HIGH
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_HIGH,
			FIXEDTRAITS.SUNLIGHT.VERY_HIGH
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_HIGH,
			FIXEDTRAITS.SUNLIGHT.VERY_VERY_HIGH
		},
		{
			FIXEDTRAITS.SUNLIGHT.NAME.VERY_VERY_VERY_HIGH,
			FIXEDTRAITS.SUNLIGHT.VERY_VERY_VERY_HIGH
		}
	};

	// Token: 0x04003CBF RID: 15551
	private Dictionary<string, int> northernLightsFixedTraits = new Dictionary<string, int>
	{
		{
			FIXEDTRAITS.NORTHERNLIGHTS.NAME.NONE,
			FIXEDTRAITS.NORTHERNLIGHTS.NONE
		},
		{
			FIXEDTRAITS.NORTHERNLIGHTS.NAME.ENABLED,
			FIXEDTRAITS.NORTHERNLIGHTS.ENABLED
		}
	};

	// Token: 0x04003CC0 RID: 15552
	private Dictionary<string, int> largeImpactorFragmentsFixedTraits = new Dictionary<string, int>
	{
		{
			FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.NAME.NONE,
			FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.NONE
		},
		{
			FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.NAME.ALLOWED,
			FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.ALLOWED
		}
	};

	// Token: 0x04003CC1 RID: 15553
	private Dictionary<string, int> cosmicRadiationFixedTraits = new Dictionary<string, int>
	{
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.NONE,
			FIXEDTRAITS.COSMICRADIATION.NONE
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.VERY_VERY_LOW,
			FIXEDTRAITS.COSMICRADIATION.VERY_VERY_LOW
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.VERY_LOW,
			FIXEDTRAITS.COSMICRADIATION.VERY_LOW
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.LOW,
			FIXEDTRAITS.COSMICRADIATION.LOW
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.MED_LOW,
			FIXEDTRAITS.COSMICRADIATION.MED_LOW
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.MED,
			FIXEDTRAITS.COSMICRADIATION.MED
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.MED_HIGH,
			FIXEDTRAITS.COSMICRADIATION.MED_HIGH
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.HIGH,
			FIXEDTRAITS.COSMICRADIATION.HIGH
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.VERY_HIGH,
			FIXEDTRAITS.COSMICRADIATION.VERY_HIGH
		},
		{
			FIXEDTRAITS.COSMICRADIATION.NAME.VERY_VERY_HIGH,
			FIXEDTRAITS.COSMICRADIATION.VERY_VERY_HIGH
		}
	};

	// Token: 0x04003CC2 RID: 15554
	[Serialize]
	private List<string> m_seasonIds;

	// Token: 0x04003CC3 RID: 15555
	[Serialize]
	private List<string> m_subworldNames;

	// Token: 0x04003CC4 RID: 15556
	[Serialize]
	private List<string> m_worldTraitIds;

	// Token: 0x04003CC5 RID: 15557
	[Serialize]
	private List<string> m_storyTraitIds;

	// Token: 0x04003CC6 RID: 15558
	[Serialize]
	private List<string> m_generatedSubworlds;

	// Token: 0x04003CC7 RID: 15559
	private WorldParentChangedEventArgs parentChangeArgs = new WorldParentChangedEventArgs();

	// Token: 0x04003CC8 RID: 15560
	[MySmiReq]
	private AlertStateManager.Instance m_alertManager;

	// Token: 0x04003CC9 RID: 15561
	private List<Prioritizable> yellowAlertTasks = new List<Prioritizable>();

	// Token: 0x04003CCB RID: 15563
	private List<int> m_childWorlds = new List<int>();
}
