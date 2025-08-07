using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using ProcGen;
using ProcGenGame;
using TemplateClasses;
using UnityEngine;

// Token: 0x02000BE9 RID: 3049
[AddComponentMenu("KMonoBehaviour/scripts/WorldGenSpawner")]
public class WorldGenSpawner : KMonoBehaviour
{
	// Token: 0x06005BC6 RID: 23494 RVA: 0x00212347 File Offset: 0x00210547
	public bool SpawnsRemain()
	{
		return this.spawnables.Count > 0;
	}

	// Token: 0x06005BC7 RID: 23495 RVA: 0x00212358 File Offset: 0x00210558
	public void SpawnEverything()
	{
		for (int i = 0; i < this.spawnables.Count; i++)
		{
			this.spawnables[i].TrySpawn();
		}
	}

	// Token: 0x06005BC8 RID: 23496 RVA: 0x0021238C File Offset: 0x0021058C
	public void SpawnTag(string id)
	{
		for (int i = 0; i < this.spawnables.Count; i++)
		{
			if (this.spawnables[i].spawnInfo.id == id)
			{
				this.spawnables[i].TrySpawn();
			}
		}
	}

	// Token: 0x06005BC9 RID: 23497 RVA: 0x002123E0 File Offset: 0x002105E0
	public void ClearSpawnersInArea(Vector2 root_position, CellOffset[] area)
	{
		for (int i = 0; i < this.spawnables.Count; i++)
		{
			if (Grid.IsCellOffsetOf(Grid.PosToCell(root_position), this.spawnables[i].cell, area))
			{
				this.spawnables[i].FreeResources();
			}
		}
	}

	// Token: 0x06005BCA RID: 23498 RVA: 0x00212433 File Offset: 0x00210633
	public IReadOnlyList<WorldGenSpawner.Spawnable> GetSpawnables()
	{
		return this.spawnables;
	}

	// Token: 0x06005BCB RID: 23499 RVA: 0x0021243C File Offset: 0x0021063C
	protected override void OnSpawn()
	{
		if (!this.hasPlacedTemplates)
		{
			global::Debug.Assert(SaveLoader.Instance.Cluster != null, "Trying to place templates for an already-loaded save, no worldgen data available");
			this.DoReveal(SaveLoader.Instance.Cluster);
			this.PlaceTemplates(SaveLoader.Instance.Cluster);
			this.hasPlacedTemplates = true;
		}
		if (this.spawnInfos == null)
		{
			return;
		}
		for (int i = 0; i < this.spawnInfos.Length; i++)
		{
			this.AddSpawnable(this.spawnInfos[i]);
		}
	}

	// Token: 0x06005BCC RID: 23500 RVA: 0x002124BC File Offset: 0x002106BC
	[OnSerializing]
	private void OnSerializing()
	{
		List<Prefab> list = new List<Prefab>();
		for (int i = 0; i < this.spawnables.Count; i++)
		{
			WorldGenSpawner.Spawnable spawnable = this.spawnables[i];
			if (!spawnable.isSpawned)
			{
				list.Add(spawnable.spawnInfo);
			}
		}
		this.spawnInfos = list.ToArray();
	}

	// Token: 0x06005BCD RID: 23501 RVA: 0x00212512 File Offset: 0x00210712
	private void AddSpawnable(Prefab prefab)
	{
		this.spawnables.Add(new WorldGenSpawner.Spawnable(prefab));
	}

	// Token: 0x06005BCE RID: 23502 RVA: 0x00212528 File Offset: 0x00210728
	public void AddLegacySpawner(Tag tag, int cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		this.AddSpawnable(new Prefab(tag.Name, Prefab.Type.Other, vector2I.x, vector2I.y, SimHashes.Carbon, -1f, 1f, null, 0, Orientation.Neutral, null, null, 0, null));
	}

	// Token: 0x06005BCF RID: 23503 RVA: 0x00212574 File Offset: 0x00210774
	public List<Tag> GetUnspawnedWithType<T>(int worldID) where T : KMonoBehaviour
	{
		List<Tag> list = new List<Tag>();
		List<WorldGenSpawner.Spawnable> list2 = this.spawnables;
		Predicate<WorldGenSpawner.Spawnable> <>9__0;
		Predicate<WorldGenSpawner.Spawnable> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = (WorldGenSpawner.Spawnable match) => !match.isSpawned && (int)Grid.WorldIdx[match.cell] == worldID && Assets.GetPrefab(match.spawnInfo.id) != null && Assets.GetPrefab(match.spawnInfo.id).GetComponent<T>() != null);
		}
		foreach (WorldGenSpawner.Spawnable spawnable in list2.FindAll(predicate))
		{
			list.Add(spawnable.spawnInfo.id);
		}
		return list;
	}

	// Token: 0x06005BD0 RID: 23504 RVA: 0x00212610 File Offset: 0x00210810
	public List<WorldGenSpawner.Spawnable> GeInfoOfUnspawnedWithType<T>(int worldID) where T : KMonoBehaviour
	{
		List<WorldGenSpawner.Spawnable> list = new List<WorldGenSpawner.Spawnable>();
		List<WorldGenSpawner.Spawnable> list2 = this.spawnables;
		Predicate<WorldGenSpawner.Spawnable> <>9__0;
		Predicate<WorldGenSpawner.Spawnable> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = (WorldGenSpawner.Spawnable match) => !match.isSpawned && (int)Grid.WorldIdx[match.cell] == worldID && Assets.GetPrefab(match.spawnInfo.id) != null && Assets.GetPrefab(match.spawnInfo.id).GetComponent<T>() != null);
		}
		foreach (WorldGenSpawner.Spawnable spawnable in list2.FindAll(predicate))
		{
			list.Add(spawnable);
		}
		return list;
	}

	// Token: 0x06005BD1 RID: 23505 RVA: 0x002126A0 File Offset: 0x002108A0
	public WorldGenSpawner.Spawnable GetSpawnableInCell(int cell)
	{
		return this.spawnables.Find((WorldGenSpawner.Spawnable s) => s.cell == cell);
	}

	// Token: 0x06005BD2 RID: 23506 RVA: 0x002126D4 File Offset: 0x002108D4
	public List<Tag> GetSpawnersWithTag(Tag tag, int worldID, bool includeSpawned = false)
	{
		List<Tag> list = new List<Tag>();
		List<WorldGenSpawner.Spawnable> list2 = this.spawnables;
		Predicate<WorldGenSpawner.Spawnable> <>9__0;
		Predicate<WorldGenSpawner.Spawnable> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = (WorldGenSpawner.Spawnable match) => (includeSpawned || !match.isSpawned) && (int)Grid.WorldIdx[match.cell] == worldID && match.spawnInfo.id == tag);
		}
		foreach (WorldGenSpawner.Spawnable spawnable in list2.FindAll(predicate))
		{
			list.Add(spawnable.spawnInfo.id);
		}
		return list;
	}

	// Token: 0x06005BD3 RID: 23507 RVA: 0x00212780 File Offset: 0x00210980
	public List<WorldGenSpawner.Spawnable> GetSpawnablesWithTag(Tag tag, int worldID, bool includeSpawned = false)
	{
		List<WorldGenSpawner.Spawnable> list = new List<WorldGenSpawner.Spawnable>();
		List<WorldGenSpawner.Spawnable> list2 = this.spawnables;
		Predicate<WorldGenSpawner.Spawnable> <>9__0;
		Predicate<WorldGenSpawner.Spawnable> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = (WorldGenSpawner.Spawnable match) => (includeSpawned || !match.isSpawned) && (int)Grid.WorldIdx[match.cell] == worldID && match.spawnInfo.id == tag);
		}
		foreach (WorldGenSpawner.Spawnable spawnable in list2.FindAll(predicate))
		{
			list.Add(spawnable);
		}
		return list;
	}

	// Token: 0x06005BD4 RID: 23508 RVA: 0x0021281C File Offset: 0x00210A1C
	public List<WorldGenSpawner.Spawnable> GetSpawnablesWithTag(bool includeSpawned = false, params Tag[] tags)
	{
		List<WorldGenSpawner.Spawnable> list = new List<WorldGenSpawner.Spawnable>();
		List<WorldGenSpawner.Spawnable> list2 = this.spawnables;
		Predicate<WorldGenSpawner.Spawnable> <>9__0;
		Predicate<WorldGenSpawner.Spawnable> predicate;
		if ((predicate = <>9__0) == null)
		{
			predicate = (<>9__0 = (WorldGenSpawner.Spawnable match) => includeSpawned || !match.isSpawned);
		}
		foreach (WorldGenSpawner.Spawnable spawnable in list2.FindAll(predicate))
		{
			foreach (Tag tag in tags)
			{
				if (spawnable.spawnInfo.id == tag)
				{
					list.Add(spawnable);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x06005BD5 RID: 23509 RVA: 0x002128E8 File Offset: 0x00210AE8
	private void PlaceTemplates(Cluster clusterLayout)
	{
		this.spawnables = new List<WorldGenSpawner.Spawnable>();
		foreach (WorldGen worldGen in clusterLayout.worlds)
		{
			foreach (Prefab prefab in worldGen.SpawnData.buildings)
			{
				prefab.location_x += worldGen.data.world.offset.x;
				prefab.location_y += worldGen.data.world.offset.y;
				prefab.type = Prefab.Type.Building;
				this.AddSpawnable(prefab);
			}
			foreach (Prefab prefab2 in worldGen.SpawnData.elementalOres)
			{
				prefab2.location_x += worldGen.data.world.offset.x;
				prefab2.location_y += worldGen.data.world.offset.y;
				prefab2.type = Prefab.Type.Ore;
				this.AddSpawnable(prefab2);
			}
			foreach (Prefab prefab3 in worldGen.SpawnData.otherEntities)
			{
				prefab3.location_x += worldGen.data.world.offset.x;
				prefab3.location_y += worldGen.data.world.offset.y;
				prefab3.type = Prefab.Type.Other;
				this.AddSpawnable(prefab3);
			}
			foreach (Prefab prefab4 in worldGen.SpawnData.pickupables)
			{
				prefab4.location_x += worldGen.data.world.offset.x;
				prefab4.location_y += worldGen.data.world.offset.y;
				prefab4.type = Prefab.Type.Pickupable;
				this.AddSpawnable(prefab4);
			}
			foreach (Tag tag in worldGen.SpawnData.discoveredResources)
			{
				DiscoveredResources.Instance.Discover(tag);
			}
			worldGen.SpawnData.buildings.Clear();
			worldGen.SpawnData.elementalOres.Clear();
			worldGen.SpawnData.otherEntities.Clear();
			worldGen.SpawnData.pickupables.Clear();
			worldGen.SpawnData.discoveredResources.Clear();
		}
	}

	// Token: 0x06005BD6 RID: 23510 RVA: 0x00212C84 File Offset: 0x00210E84
	private void DoReveal(Cluster clusterLayout)
	{
		foreach (WorldGen worldGen in clusterLayout.worlds)
		{
			Game.Instance.Reset(worldGen.SpawnData, worldGen.WorldOffset);
		}
		for (int i = 0; i < Grid.CellCount; i++)
		{
			Grid.Revealed[i] = false;
			Grid.Spawnable[i] = 0;
		}
		float num = 16.5f;
		int num2 = 18;
		Vector2I vector2I = clusterLayout.currentWorld.SpawnData.baseStartPos;
		vector2I += clusterLayout.currentWorld.WorldOffset;
		GridVisibility.Reveal(vector2I.x, vector2I.y, num2, num);
	}

	// Token: 0x04003CD4 RID: 15572
	[Serialize]
	private Prefab[] spawnInfos;

	// Token: 0x04003CD5 RID: 15573
	[Serialize]
	private bool hasPlacedTemplates;

	// Token: 0x04003CD6 RID: 15574
	private List<WorldGenSpawner.Spawnable> spawnables = new List<WorldGenSpawner.Spawnable>();

	// Token: 0x02001D19 RID: 7449
	public class Spawnable
	{
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600AD21 RID: 44321 RVA: 0x003C3DCD File Offset: 0x003C1FCD
		// (set) Token: 0x0600AD22 RID: 44322 RVA: 0x003C3DD5 File Offset: 0x003C1FD5
		public Prefab spawnInfo { get; private set; }

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600AD23 RID: 44323 RVA: 0x003C3DDE File Offset: 0x003C1FDE
		// (set) Token: 0x0600AD24 RID: 44324 RVA: 0x003C3DE6 File Offset: 0x003C1FE6
		public bool isSpawned { get; private set; }

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600AD25 RID: 44325 RVA: 0x003C3DEF File Offset: 0x003C1FEF
		// (set) Token: 0x0600AD26 RID: 44326 RVA: 0x003C3DF7 File Offset: 0x003C1FF7
		public int cell { get; private set; }

		// Token: 0x0600AD27 RID: 44327 RVA: 0x003C3E00 File Offset: 0x003C2000
		public Spawnable(Prefab spawn_info)
		{
			this.spawnInfo = spawn_info;
			int num = Grid.XYToCell(this.spawnInfo.location_x, this.spawnInfo.location_y);
			GameObject prefab = Assets.GetPrefab(spawn_info.id);
			if (prefab != null)
			{
				WorldSpawnableMonitor.Def def = prefab.GetDef<WorldSpawnableMonitor.Def>();
				if (def != null && def.adjustSpawnLocationCb != null)
				{
					num = def.adjustSpawnLocationCb(num);
				}
			}
			this.cell = num;
			global::Debug.Assert(Grid.IsValidCell(this.cell));
			if (Grid.Spawnable[this.cell] > 0)
			{
				this.TrySpawn();
				return;
			}
			this.fogOfWarPartitionerEntry = GameScenePartitioner.Instance.Add("WorldGenSpawner.OnReveal", this, this.cell, GameScenePartitioner.Instance.fogOfWarChangedLayer, new Action<object>(this.OnReveal));
		}

		// Token: 0x0600AD28 RID: 44328 RVA: 0x003C3ECE File Offset: 0x003C20CE
		private void OnReveal(object data)
		{
			if (Grid.Spawnable[this.cell] > 0)
			{
				this.TrySpawn();
			}
		}

		// Token: 0x0600AD29 RID: 44329 RVA: 0x003C3EE5 File Offset: 0x003C20E5
		private void OnSolidChanged(object data)
		{
			if (!Grid.Solid[this.cell])
			{
				GameScenePartitioner.Instance.Free(ref this.solidChangedPartitionerEntry);
				Game.Instance.GetComponent<EntombedItemVisualizer>().RemoveItem(this.cell);
				this.Spawn();
			}
		}

		// Token: 0x0600AD2A RID: 44330 RVA: 0x003C3F24 File Offset: 0x003C2124
		public void FreeResources()
		{
			if (this.solidChangedPartitionerEntry.IsValid())
			{
				GameScenePartitioner.Instance.Free(ref this.solidChangedPartitionerEntry);
				if (Game.Instance != null)
				{
					Game.Instance.GetComponent<EntombedItemVisualizer>().RemoveItem(this.cell);
				}
			}
			GameScenePartitioner.Instance.Free(ref this.fogOfWarPartitionerEntry);
			this.isSpawned = true;
		}

		// Token: 0x0600AD2B RID: 44331 RVA: 0x003C3F88 File Offset: 0x003C2188
		public void TrySpawn()
		{
			if (this.isSpawned)
			{
				return;
			}
			if (this.solidChangedPartitionerEntry.IsValid())
			{
				return;
			}
			WorldContainer world = ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[this.cell]);
			bool flag = world != null && world.IsDiscovered;
			GameObject prefab = Assets.GetPrefab(this.GetPrefabTag());
			if (!(prefab != null))
			{
				if (flag)
				{
					GameScenePartitioner.Instance.Free(ref this.fogOfWarPartitionerEntry);
					this.Spawn();
				}
				return;
			}
			if (!(flag | prefab.HasTag(GameTags.WarpTech)))
			{
				return;
			}
			GameScenePartitioner.Instance.Free(ref this.fogOfWarPartitionerEntry);
			bool flag2 = false;
			if (prefab.GetComponent<Pickupable>() != null && !prefab.HasTag(GameTags.Creatures.Digger))
			{
				flag2 = true;
			}
			else if (prefab.GetDef<BurrowMonitor.Def>() != null)
			{
				flag2 = true;
			}
			if (flag2 && Grid.Solid[this.cell])
			{
				this.solidChangedPartitionerEntry = GameScenePartitioner.Instance.Add("WorldGenSpawner.OnSolidChanged", this, this.cell, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
				Game.Instance.GetComponent<EntombedItemVisualizer>().AddItem(this.cell);
				return;
			}
			this.Spawn();
		}

		// Token: 0x0600AD2C RID: 44332 RVA: 0x003C40BC File Offset: 0x003C22BC
		private Tag GetPrefabTag()
		{
			Mob mob = SettingsCache.mobs.GetMob(this.spawnInfo.id);
			if (mob != null && mob.prefabName != null)
			{
				return new Tag(mob.prefabName);
			}
			return new Tag(this.spawnInfo.id);
		}

		// Token: 0x0600AD2D RID: 44333 RVA: 0x003C4108 File Offset: 0x003C2308
		private void Spawn()
		{
			this.isSpawned = true;
			GameObject gameObject = WorldGenSpawner.Spawnable.GetSpawnableCallback(this.spawnInfo.type)(this.spawnInfo, 0);
			if (gameObject != null && gameObject)
			{
				gameObject.SetActive(true);
				gameObject.Trigger(1119167081, this.spawnInfo);
			}
			this.FreeResources();
		}

		// Token: 0x0600AD2E RID: 44334 RVA: 0x003C4168 File Offset: 0x003C2368
		public static WorldGenSpawner.Spawnable.PlaceEntityFn GetSpawnableCallback(Prefab.Type type)
		{
			switch (type)
			{
			case Prefab.Type.Building:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlaceBuilding);
			case Prefab.Type.Ore:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlaceElementalOres);
			case Prefab.Type.Pickupable:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlacePickupables);
			case Prefab.Type.Other:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlaceOtherEntities);
			default:
				return new WorldGenSpawner.Spawnable.PlaceEntityFn(TemplateLoader.PlaceOtherEntities);
			}
		}

		// Token: 0x04008833 RID: 34867
		private HandleVector<int>.Handle fogOfWarPartitionerEntry;

		// Token: 0x04008834 RID: 34868
		private HandleVector<int>.Handle solidChangedPartitionerEntry;

		// Token: 0x020028D9 RID: 10457
		// (Invoke) Token: 0x0600CD8C RID: 52620
		public delegate GameObject PlaceEntityFn(Prefab prefab, int root_cell);
	}
}
