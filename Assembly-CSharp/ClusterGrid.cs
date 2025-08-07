using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using STRINGS;
using UnityEngine;

// Token: 0x02000B71 RID: 2929
public class ClusterGrid
{
	// Token: 0x0600578D RID: 22413 RVA: 0x001FB8F0 File Offset: 0x001F9AF0
	public static void DestroyInstance()
	{
		ClusterGrid.Instance = null;
	}

	// Token: 0x0600578E RID: 22414 RVA: 0x001FB8F8 File Offset: 0x001F9AF8
	private ClusterFogOfWarManager.Instance GetFOWManager()
	{
		if (this.m_fowManager == null)
		{
			this.m_fowManager = SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>();
		}
		return this.m_fowManager;
	}

	// Token: 0x0600578F RID: 22415 RVA: 0x001FB918 File Offset: 0x001F9B18
	public bool IsValidCell(AxialI cell)
	{
		return this.cellContents.ContainsKey(cell);
	}

	// Token: 0x06005790 RID: 22416 RVA: 0x001FB926 File Offset: 0x001F9B26
	public ClusterGrid(int numRings)
	{
		ClusterGrid.Instance = this;
		this.GenerateGrid(numRings);
		this.m_onClusterLocationChangedDelegate = new Action<object>(this.OnClusterLocationChanged);
	}

	// Token: 0x06005791 RID: 22417 RVA: 0x001FB958 File Offset: 0x001F9B58
	public ClusterRevealLevel GetCellRevealLevel(AxialI cell)
	{
		return this.GetFOWManager().GetCellRevealLevel(cell);
	}

	// Token: 0x06005792 RID: 22418 RVA: 0x001FB966 File Offset: 0x001F9B66
	public bool IsCellVisible(AxialI cell)
	{
		return this.GetFOWManager().IsLocationRevealed(cell);
	}

	// Token: 0x06005793 RID: 22419 RVA: 0x001FB974 File Offset: 0x001F9B74
	public float GetRevealCompleteFraction(AxialI cell)
	{
		return this.GetFOWManager().GetRevealCompleteFraction(cell);
	}

	// Token: 0x06005794 RID: 22420 RVA: 0x001FB982 File Offset: 0x001F9B82
	public bool IsVisible(ClusterGridEntity entity)
	{
		return entity.IsVisible && this.IsCellVisible(entity.Location);
	}

	// Token: 0x06005795 RID: 22421 RVA: 0x001FB99C File Offset: 0x001F9B9C
	public List<ClusterGridEntity> GetVisibleEntitiesAtCell(AxialI cell)
	{
		if (this.IsValidCell(cell) && this.GetFOWManager().IsLocationRevealed(cell))
		{
			return this.cellContents[cell].Where((ClusterGridEntity entity) => entity.IsVisible).ToList<ClusterGridEntity>();
		}
		return new List<ClusterGridEntity>();
	}

	// Token: 0x06005796 RID: 22422 RVA: 0x001FB9FC File Offset: 0x001F9BFC
	public ClusterGridEntity GetVisibleEntityOfLayerAtCell(AxialI cell, EntityLayer entityLayer)
	{
		if (this.IsValidCell(cell) && this.GetFOWManager().IsLocationRevealed(cell))
		{
			foreach (ClusterGridEntity clusterGridEntity in this.cellContents[cell])
			{
				if (clusterGridEntity.IsVisible && clusterGridEntity.Layer == entityLayer)
				{
					return clusterGridEntity;
				}
			}
		}
		return null;
	}

	// Token: 0x06005797 RID: 22423 RVA: 0x001FBA80 File Offset: 0x001F9C80
	public ClusterGridEntity GetVisibleEntityOfLayerAtAdjacentCell(AxialI cell, EntityLayer entityLayer)
	{
		return AxialUtil.GetRing(cell, 1).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetVisibleEntitiesAtCell)).FirstOrDefault((ClusterGridEntity entity) => entity.Layer == entityLayer);
	}

	// Token: 0x06005798 RID: 22424 RVA: 0x001FBAC4 File Offset: 0x001F9CC4
	public List<ClusterGridEntity> GetHiddenEntitiesOfLayerAtCell(AxialI cell, EntityLayer entityLayer)
	{
		return (from entity in AxialUtil.GetRing(cell, 0).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetHiddenEntitiesAtCell))
			where entity.Layer == entityLayer
			select entity).ToList<ClusterGridEntity>();
	}

	// Token: 0x06005799 RID: 22425 RVA: 0x001FBB0C File Offset: 0x001F9D0C
	public List<ClusterGridEntity> GetEntitiesOfLayerAtCell(AxialI cell, EntityLayer entityLayer)
	{
		return (from entity in AxialUtil.GetRing(cell, 0).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetEntitiesOnCell))
			where entity.Layer == entityLayer
			select entity).ToList<ClusterGridEntity>();
	}

	// Token: 0x0600579A RID: 22426 RVA: 0x001FBB54 File Offset: 0x001F9D54
	public ClusterGridEntity GetEntityOfLayerAtCell(AxialI cell, EntityLayer entityLayer)
	{
		return AxialUtil.GetRing(cell, 0).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetEntitiesOnCell)).FirstOrDefault((ClusterGridEntity entity) => entity.Layer == entityLayer);
	}

	// Token: 0x0600579B RID: 22427 RVA: 0x001FBB98 File Offset: 0x001F9D98
	public List<ClusterGridEntity> GetHiddenEntitiesAtCell(AxialI cell)
	{
		if (this.cellContents.ContainsKey(cell) && !this.GetFOWManager().IsLocationRevealed(cell))
		{
			return this.cellContents[cell].Where((ClusterGridEntity entity) => entity.IsVisible).ToList<ClusterGridEntity>();
		}
		return new List<ClusterGridEntity>();
	}

	// Token: 0x0600579C RID: 22428 RVA: 0x001FBBFC File Offset: 0x001F9DFC
	public List<ClusterGridEntity> GetNotVisibleEntitiesAtAdjacentCell(AxialI cell)
	{
		return AxialUtil.GetRing(cell, 1).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetHiddenEntitiesAtCell)).ToList<ClusterGridEntity>();
	}

	// Token: 0x0600579D RID: 22429 RVA: 0x001FBC1C File Offset: 0x001F9E1C
	public List<ClusterGridEntity> GetNotVisibleEntitiesOfLayerAtAdjacentCell(AxialI cell, EntityLayer entityLayer)
	{
		return (from entity in AxialUtil.GetRing(cell, 1).SelectMany(new Func<AxialI, IEnumerable<ClusterGridEntity>>(this.GetHiddenEntitiesAtCell))
			where entity.Layer == entityLayer
			select entity).ToList<ClusterGridEntity>();
	}

	// Token: 0x0600579E RID: 22430 RVA: 0x001FBC64 File Offset: 0x001F9E64
	public bool GetVisibleUnidentifiedMeteorShowerWithinRadius(AxialI center, int radius, out ClusterMapMeteorShower.Instance result)
	{
		for (int i = 0; i <= radius; i++)
		{
			foreach (AxialI axialI in AxialUtil.GetRing(center, i))
			{
				if (this.IsValidCell(axialI) && this.GetFOWManager().IsLocationRevealed(axialI))
				{
					foreach (ClusterGridEntity clusterGridEntity in this.GetEntitiesOfLayerAtCell(axialI, EntityLayer.Meteor))
					{
						ClusterMapMeteorShower.Instance smi = clusterGridEntity.GetSMI<ClusterMapMeteorShower.Instance>();
						if (smi != null && !smi.HasBeenIdentified)
						{
							result = smi;
							return true;
						}
					}
				}
			}
		}
		result = null;
		return false;
	}

	// Token: 0x0600579F RID: 22431 RVA: 0x001FBD3C File Offset: 0x001F9F3C
	public ClusterGridEntity GetAsteroidAtCell(AxialI cell)
	{
		if (!this.cellContents.ContainsKey(cell))
		{
			return null;
		}
		return this.cellContents[cell].Where((ClusterGridEntity e) => e.Layer == EntityLayer.Asteroid).FirstOrDefault<ClusterGridEntity>();
	}

	// Token: 0x060057A0 RID: 22432 RVA: 0x001FBD8E File Offset: 0x001F9F8E
	public bool HasVisibleAsteroidAtCell(AxialI cell)
	{
		return this.GetVisibleEntityOfLayerAtCell(cell, EntityLayer.Asteroid) != null;
	}

	// Token: 0x060057A1 RID: 22433 RVA: 0x001FBD9E File Offset: 0x001F9F9E
	public void RegisterEntity(ClusterGridEntity entity)
	{
		this.cellContents[entity.Location].Add(entity);
		entity.Subscribe(-1298331547, this.m_onClusterLocationChangedDelegate);
	}

	// Token: 0x060057A2 RID: 22434 RVA: 0x001FBDC9 File Offset: 0x001F9FC9
	public void UnregisterEntity(ClusterGridEntity entity)
	{
		this.cellContents[entity.Location].Remove(entity);
		entity.Unsubscribe(-1298331547, this.m_onClusterLocationChangedDelegate);
	}

	// Token: 0x060057A3 RID: 22435 RVA: 0x001FBDF4 File Offset: 0x001F9FF4
	public void OnClusterLocationChanged(object data)
	{
		ClusterLocationChangedEvent clusterLocationChangedEvent = (ClusterLocationChangedEvent)data;
		global::Debug.Assert(this.IsValidCell(clusterLocationChangedEvent.oldLocation), string.Format("ChangeEntityCell move order FROM invalid location: {0} {1}", clusterLocationChangedEvent.oldLocation, clusterLocationChangedEvent.entity));
		global::Debug.Assert(this.IsValidCell(clusterLocationChangedEvent.newLocation), string.Format("ChangeEntityCell move order TO invalid location: {0} {1}", clusterLocationChangedEvent.newLocation, clusterLocationChangedEvent.entity));
		this.cellContents[clusterLocationChangedEvent.oldLocation].Remove(clusterLocationChangedEvent.entity);
		this.cellContents[clusterLocationChangedEvent.newLocation].Add(clusterLocationChangedEvent.entity);
	}

	// Token: 0x060057A4 RID: 22436 RVA: 0x001FBE99 File Offset: 0x001FA099
	private AxialI GetNeighbor(AxialI cell, AxialI direction)
	{
		return cell + direction;
	}

	// Token: 0x060057A5 RID: 22437 RVA: 0x001FBEA4 File Offset: 0x001FA0A4
	public int GetCellRing(AxialI cell)
	{
		Vector3I vector3I = cell.ToCube();
		Vector3I vector3I2 = new Vector3I(vector3I.x, vector3I.y, vector3I.z);
		Vector3I vector3I3 = new Vector3I(0, 0, 0);
		return (int)((float)((Mathf.Abs(vector3I2.x - vector3I3.x) + Mathf.Abs(vector3I2.y - vector3I3.y) + Mathf.Abs(vector3I2.z - vector3I3.z)) / 2));
	}

	// Token: 0x060057A6 RID: 22438 RVA: 0x001FBF18 File Offset: 0x001FA118
	private void CleanUpGrid()
	{
		this.cellContents.Clear();
	}

	// Token: 0x060057A7 RID: 22439 RVA: 0x001FBF28 File Offset: 0x001FA128
	private int GetHexDistance(AxialI a, AxialI b)
	{
		Vector3I vector3I = a.ToCube();
		Vector3I vector3I2 = b.ToCube();
		return Mathf.Max(new int[]
		{
			Mathf.Abs(vector3I.x - vector3I2.x),
			Mathf.Abs(vector3I.y - vector3I2.y),
			Mathf.Abs(vector3I.z - vector3I2.z)
		});
	}

	// Token: 0x060057A8 RID: 22440 RVA: 0x001FBF90 File Offset: 0x001FA190
	public List<ClusterGridEntity> GetEntitiesInRange(AxialI center, int range = 1)
	{
		List<ClusterGridEntity> list = new List<ClusterGridEntity>();
		foreach (KeyValuePair<AxialI, List<ClusterGridEntity>> keyValuePair in this.cellContents)
		{
			if (this.GetHexDistance(keyValuePair.Key, center) <= range)
			{
				list.AddRange(keyValuePair.Value);
			}
		}
		return list;
	}

	// Token: 0x060057A9 RID: 22441 RVA: 0x001FC004 File Offset: 0x001FA204
	public List<ClusterGridEntity> GetEntitiesOnCell(AxialI cell)
	{
		return this.cellContents[cell];
	}

	// Token: 0x060057AA RID: 22442 RVA: 0x001FC012 File Offset: 0x001FA212
	public bool IsInRange(AxialI a, AxialI b, int range = 1)
	{
		return this.GetHexDistance(a, b) <= range;
	}

	// Token: 0x060057AB RID: 22443 RVA: 0x001FC024 File Offset: 0x001FA224
	private void GenerateGrid(int rings)
	{
		this.CleanUpGrid();
		this.numRings = rings;
		for (int i = -rings + 1; i < rings; i++)
		{
			for (int j = -rings + 1; j < rings; j++)
			{
				for (int k = -rings + 1; k < rings; k++)
				{
					if (i + j + k == 0)
					{
						AxialI axialI = new AxialI(i, j);
						this.cellContents.Add(axialI, new List<ClusterGridEntity>());
					}
				}
			}
		}
	}

	// Token: 0x060057AC RID: 22444 RVA: 0x001FC08C File Offset: 0x001FA28C
	public AxialI GetRandomCellAtEdgeOfUniverse()
	{
		int num = this.numRings - 1;
		List<AxialI> rings = AxialUtil.GetRings(AxialI.ZERO, num, num);
		return rings.ElementAt(global::UnityEngine.Random.Range(0, rings.Count));
	}

	// Token: 0x060057AD RID: 22445 RVA: 0x001FC0C4 File Offset: 0x001FA2C4
	public Vector3 GetPosition(ClusterGridEntity entity)
	{
		float num = (float)entity.Location.R;
		float num2 = (float)entity.Location.Q;
		List<ClusterGridEntity> list = this.cellContents[entity.Location];
		if (list.Count <= 1 || !entity.SpaceOutInSameHex())
		{
			return AxialUtil.AxialToWorld(num, num2);
		}
		int num3 = 0;
		int num4 = 0;
		foreach (ClusterGridEntity clusterGridEntity in list)
		{
			if (entity == clusterGridEntity)
			{
				num3 = num4;
			}
			if (clusterGridEntity.SpaceOutInSameHex())
			{
				num4++;
			}
		}
		if (list.Count > num4)
		{
			num4 += 5;
			num3 += 5;
		}
		else if (num4 > 0)
		{
			num4++;
			num3++;
		}
		if (num4 == 0 || num4 == 1)
		{
			return AxialUtil.AxialToWorld(num, num2);
		}
		float num5 = Mathf.Min(Mathf.Pow((float)num4, 0.5f), 1f) * 0.5f;
		float num6 = Mathf.Pow((float)num3 / (float)num4, 0.5f);
		float num7 = 0.81f;
		float num8 = Mathf.Pow((float)num4, 0.5f) * num7;
		float num9 = 6.2831855f * num8 * num6;
		float num10 = Mathf.Cos(num9) * num5 * num6;
		float num11 = Mathf.Sin(num9) * num5 * num6;
		return AxialUtil.AxialToWorld(num, num2) + new Vector3(num10, num11, 0f);
	}

	// Token: 0x060057AE RID: 22446 RVA: 0x001FC248 File Offset: 0x001FA448
	public List<AxialI> GetPath(AxialI start, AxialI end, ClusterDestinationSelector destination_selector)
	{
		string text;
		return this.GetPath(start, end, destination_selector, out text, false);
	}

	// Token: 0x060057AF RID: 22447 RVA: 0x001FC264 File Offset: 0x001FA464
	public List<AxialI> GetPath(AxialI start, AxialI end, ClusterDestinationSelector destination_selector, out string fail_reason, bool dodgeHiddenAsteroids = false)
	{
		ClusterGrid.<>c__DisplayClass41_0 CS$<>8__locals1;
		CS$<>8__locals1.<>4__this = this;
		CS$<>8__locals1.destination_selector = destination_selector;
		CS$<>8__locals1.start = start;
		CS$<>8__locals1.end = end;
		CS$<>8__locals1.dodgeHiddenAsteroids = dodgeHiddenAsteroids;
		fail_reason = null;
		if (!CS$<>8__locals1.destination_selector.canNavigateFogOfWar && !this.IsCellVisible(CS$<>8__locals1.end))
		{
			fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_FOG_OF_WAR;
			return null;
		}
		ClusterGridEntity visibleEntityOfLayerAtCell = this.GetVisibleEntityOfLayerAtCell(CS$<>8__locals1.end, EntityLayer.Asteroid);
		if (visibleEntityOfLayerAtCell != null && CS$<>8__locals1.destination_selector.requireLaunchPadOnAsteroidDestination)
		{
			bool flag = false;
			using (IEnumerator enumerator = Components.LaunchPads.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((LaunchPad)enumerator.Current).GetMyWorldLocation() == visibleEntityOfLayerAtCell.Location)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_NO_LAUNCH_PAD;
				return null;
			}
		}
		if (visibleEntityOfLayerAtCell == null && CS$<>8__locals1.destination_selector.requireAsteroidDestination)
		{
			fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_REQUIRE_ASTEROID;
			return null;
		}
		if (CS$<>8__locals1.destination_selector.requiredEntityLayer != EntityLayer.None && this.GetVisibleEntityOfLayerAtCell(CS$<>8__locals1.end, CS$<>8__locals1.destination_selector.requiredEntityLayer) == null)
		{
			fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_METEOR_TARGET;
			return null;
		}
		CS$<>8__locals1.frontier = new HashSet<AxialI>();
		CS$<>8__locals1.visited = new HashSet<AxialI>();
		CS$<>8__locals1.buffer = new HashSet<AxialI>();
		CS$<>8__locals1.cameFrom = new Dictionary<AxialI, AxialI>();
		CS$<>8__locals1.frontier.Add(CS$<>8__locals1.start);
		while (!CS$<>8__locals1.frontier.Contains(CS$<>8__locals1.end) && CS$<>8__locals1.frontier.Count > 0)
		{
			this.<GetPath>g__ExpandFrontier|41_0(ref CS$<>8__locals1);
		}
		if (CS$<>8__locals1.frontier.Contains(CS$<>8__locals1.end))
		{
			List<AxialI> list = new List<AxialI>();
			AxialI axialI = CS$<>8__locals1.end;
			while (axialI != CS$<>8__locals1.start)
			{
				list.Add(axialI);
				axialI = CS$<>8__locals1.cameFrom[axialI];
			}
			list.Reverse();
			return list;
		}
		fail_reason = UI.CLUSTERMAP.TOOLTIP_INVALID_DESTINATION_NO_PATH;
		return null;
	}

	// Token: 0x060057B0 RID: 22448 RVA: 0x001FC490 File Offset: 0x001FA690
	public void GetLocationDescription(AxialI location, out Sprite sprite, out string label, out string sublabel)
	{
		ClusterGridEntity clusterGridEntity = this.GetVisibleEntitiesAtCell(location).Find((ClusterGridEntity x) => x.Layer == EntityLayer.Asteroid);
		ClusterGridEntity visibleEntityOfLayerAtAdjacentCell = this.GetVisibleEntityOfLayerAtAdjacentCell(location, EntityLayer.Asteroid);
		if (clusterGridEntity != null)
		{
			sprite = clusterGridEntity.GetUISprite();
			label = clusterGridEntity.Name;
			WorldContainer component = clusterGridEntity.GetComponent<WorldContainer>();
			sublabel = Strings.Get(component.worldType);
			return;
		}
		if (visibleEntityOfLayerAtAdjacentCell != null)
		{
			sprite = visibleEntityOfLayerAtAdjacentCell.GetUISprite();
			label = UI.SPACEDESTINATIONS.ORBIT.NAME_FMT.Replace("{Name}", visibleEntityOfLayerAtAdjacentCell.Name);
			WorldContainer component2 = visibleEntityOfLayerAtAdjacentCell.GetComponent<WorldContainer>();
			sublabel = Strings.Get(component2.worldType);
			return;
		}
		if (this.IsCellVisible(location))
		{
			sprite = Assets.GetSprite("hex_unknown");
			label = UI.SPACEDESTINATIONS.EMPTY_SPACE.NAME;
			sublabel = "";
			return;
		}
		sprite = Assets.GetSprite("unknown_far");
		label = UI.SPACEDESTINATIONS.FOG_OF_WAR_SPACE.NAME;
		sublabel = "";
	}

	// Token: 0x060057B1 RID: 22449 RVA: 0x001FC5A0 File Offset: 0x001FA7A0
	[CompilerGenerated]
	private void <GetPath>g__ExpandFrontier|41_0(ref ClusterGrid.<>c__DisplayClass41_0 A_1)
	{
		A_1.buffer.Clear();
		foreach (AxialI axialI in A_1.frontier)
		{
			foreach (AxialI axialI2 in AxialI.DIRECTIONS)
			{
				AxialI neighbor = this.GetNeighbor(axialI, axialI2);
				if (!A_1.visited.Contains(neighbor) && this.IsValidCell(neighbor) && (this.IsCellVisible(neighbor) || A_1.destination_selector.canNavigateFogOfWar) && (!this.HasVisibleAsteroidAtCell(neighbor) || !(neighbor != A_1.start) || !(neighbor != A_1.end)) && (!A_1.dodgeHiddenAsteroids || !(ClusterGrid.Instance.GetAsteroidAtCell(neighbor) != null) || ClusterGrid.Instance.GetAsteroidAtCell(neighbor).IsVisibleInFOW == ClusterRevealLevel.Visible || !(neighbor != A_1.start) || !(neighbor != A_1.end)))
				{
					A_1.buffer.Add(neighbor);
					if (!A_1.cameFrom.ContainsKey(neighbor))
					{
						A_1.cameFrom.Add(neighbor, axialI);
					}
				}
			}
			A_1.visited.Add(axialI);
		}
		HashSet<AxialI> frontier = A_1.frontier;
		A_1.frontier = A_1.buffer;
		A_1.buffer = frontier;
	}

	// Token: 0x04003A6E RID: 14958
	public static ClusterGrid Instance;

	// Token: 0x04003A6F RID: 14959
	public const float NodeDistanceScale = 600f;

	// Token: 0x04003A70 RID: 14960
	private const float MAX_OFFSET_RADIUS = 0.5f;

	// Token: 0x04003A71 RID: 14961
	public int numRings;

	// Token: 0x04003A72 RID: 14962
	private ClusterFogOfWarManager.Instance m_fowManager;

	// Token: 0x04003A73 RID: 14963
	private Action<object> m_onClusterLocationChangedDelegate;

	// Token: 0x04003A74 RID: 14964
	public Dictionary<AxialI, List<ClusterGridEntity>> cellContents = new Dictionary<AxialI, List<ClusterGridEntity>>();
}
