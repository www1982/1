using System;
using System.Collections.Generic;
using Delaunay.Geo;
using KSerialization;
using ProcGen;
using ProcGenGame;

namespace Klei
{
	// Token: 0x02000FBF RID: 4031
	public class WorldDetailSave
	{
		// Token: 0x06007CA0 RID: 31904 RVA: 0x0031F746 File Offset: 0x0031D946
		public WorldDetailSave()
		{
			this.overworldCells = new List<WorldDetailSave.OverworldCell>();
		}

		// Token: 0x04005E18 RID: 24088
		public List<WorldDetailSave.OverworldCell> overworldCells;

		// Token: 0x04005E19 RID: 24089
		public int globalWorldSeed;

		// Token: 0x04005E1A RID: 24090
		public int globalWorldLayoutSeed;

		// Token: 0x04005E1B RID: 24091
		public int globalTerrainSeed;

		// Token: 0x04005E1C RID: 24092
		public int globalNoiseSeed;

		// Token: 0x020025B6 RID: 9654
		[SerializationConfig(MemberSerialization.OptOut)]
		public class OverworldCell
		{
			// Token: 0x0600C16D RID: 49517 RVA: 0x00406546 File Offset: 0x00404746
			public OverworldCell()
			{
			}

			// Token: 0x0600C16E RID: 49518 RVA: 0x0040654E File Offset: 0x0040474E
			public OverworldCell(SubWorld.ZoneType zoneType, TerrainCell tc)
			{
				this.poly = tc.poly;
				this.tags = tc.node.tags;
				this.zoneType = zoneType;
			}

			// Token: 0x0400A88C RID: 43148
			public Polygon poly;

			// Token: 0x0400A88D RID: 43149
			public TagSet tags;

			// Token: 0x0400A88E RID: 43150
			public SubWorld.ZoneType zoneType;
		}
	}
}
