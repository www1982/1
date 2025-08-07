using System;
using System.Collections.Generic;

namespace Klei
{
	// Token: 0x02000FC1 RID: 4033
	public class ClusterLayoutSave
	{
		// Token: 0x06007CA2 RID: 31906 RVA: 0x0031F76C File Offset: 0x0031D96C
		public ClusterLayoutSave()
		{
			this.worlds = new List<ClusterLayoutSave.World>();
		}

		// Token: 0x04005E23 RID: 24099
		public string ID;

		// Token: 0x04005E24 RID: 24100
		public Vector2I version;

		// Token: 0x04005E25 RID: 24101
		public List<ClusterLayoutSave.World> worlds;

		// Token: 0x04005E26 RID: 24102
		public Vector2I size;

		// Token: 0x04005E27 RID: 24103
		public int currentWorldIdx;

		// Token: 0x04005E28 RID: 24104
		public int numRings;

		// Token: 0x04005E29 RID: 24105
		public Dictionary<ClusterLayoutSave.POIType, List<AxialI>> poiLocations = new Dictionary<ClusterLayoutSave.POIType, List<AxialI>>();

		// Token: 0x04005E2A RID: 24106
		public Dictionary<AxialI, string> poiPlacements = new Dictionary<AxialI, string>();

		// Token: 0x020025B7 RID: 9655
		public class World
		{
			// Token: 0x0400A88F RID: 43151
			public Data data = new Data();

			// Token: 0x0400A890 RID: 43152
			public string name = string.Empty;

			// Token: 0x0400A891 RID: 43153
			public bool isDiscovered;

			// Token: 0x0400A892 RID: 43154
			public List<string> traits = new List<string>();

			// Token: 0x0400A893 RID: 43155
			public List<string> storyTraits = new List<string>();

			// Token: 0x0400A894 RID: 43156
			public List<string> seasons = new List<string>();

			// Token: 0x0400A895 RID: 43157
			public List<string> generatedSubworlds = new List<string>();
		}

		// Token: 0x020025B8 RID: 9656
		public enum POIType
		{
			// Token: 0x0400A897 RID: 43159
			TemporalTear,
			// Token: 0x0400A898 RID: 43160
			ResearchDestination
		}
	}
}
