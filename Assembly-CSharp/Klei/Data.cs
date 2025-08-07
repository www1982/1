using System;
using System.Collections.Generic;
using ProcGen;
using ProcGenGame;
using VoronoiTree;

namespace Klei
{
	// Token: 0x02000FBD RID: 4029
	public class Data
	{
		// Token: 0x06007C9E RID: 31902 RVA: 0x0031F6C8 File Offset: 0x0031D8C8
		public Data()
		{
			this.worldLayout = new WorldLayout(null, 0);
			this.terrainCells = new List<TerrainCell>();
			this.overworldCells = new List<TerrainCell>();
			this.rivers = new List<global::ProcGen.River>();
			this.gameSpawnData = new GameSpawnData();
			this.world = new Chunk();
			this.voronoiTree = new Tree(0);
		}

		// Token: 0x04005E06 RID: 24070
		public int globalWorldSeed;

		// Token: 0x04005E07 RID: 24071
		public int globalWorldLayoutSeed;

		// Token: 0x04005E08 RID: 24072
		public int globalTerrainSeed;

		// Token: 0x04005E09 RID: 24073
		public int globalNoiseSeed;

		// Token: 0x04005E0A RID: 24074
		public int chunkEdgeSize = 32;

		// Token: 0x04005E0B RID: 24075
		public WorldLayout worldLayout;

		// Token: 0x04005E0C RID: 24076
		public List<TerrainCell> terrainCells;

		// Token: 0x04005E0D RID: 24077
		public List<TerrainCell> overworldCells;

		// Token: 0x04005E0E RID: 24078
		public List<global::ProcGen.River> rivers;

		// Token: 0x04005E0F RID: 24079
		public GameSpawnData gameSpawnData;

		// Token: 0x04005E10 RID: 24080
		public Chunk world;

		// Token: 0x04005E11 RID: 24081
		public Tree voronoiTree;

		// Token: 0x04005E12 RID: 24082
		public AxialI clusterLocation;
	}
}
