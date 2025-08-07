using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;

// Token: 0x02000B98 RID: 2968
[Serialize]
[SerializationConfig(MemberSerialization.OptIn)]
[Serializable]
public class SpaceScannerWorldData
{
	// Token: 0x060058A1 RID: 22689 RVA: 0x00200726 File Offset: 0x001FE926
	[Serialize]
	public SpaceScannerWorldData(int worldId)
	{
		this.worldId = worldId;
	}

	// Token: 0x060058A2 RID: 22690 RVA: 0x00200756 File Offset: 0x001FE956
	public WorldContainer GetWorld()
	{
		if (this.world == null)
		{
			this.world = ClusterManager.Instance.GetWorld(this.worldId);
		}
		return this.world;
	}

	// Token: 0x04003AE1 RID: 15073
	[NonSerialized]
	private WorldContainer world;

	// Token: 0x04003AE2 RID: 15074
	[Serialize]
	public int worldId;

	// Token: 0x04003AE3 RID: 15075
	[Serialize]
	public float networkQuality01;

	// Token: 0x04003AE4 RID: 15076
	[Serialize]
	public Dictionary<string, float> targetIdToRandomValue01Map = new Dictionary<string, float>();

	// Token: 0x04003AE5 RID: 15077
	[Serialize]
	public HashSet<string> targetIdsDetected = new HashSet<string>();

	// Token: 0x04003AE6 RID: 15078
	[NonSerialized]
	public SpaceScannerWorldData.Scratchpad scratchpad = new SpaceScannerWorldData.Scratchpad();

	// Token: 0x02001CC1 RID: 7361
	public class Scratchpad
	{
		// Token: 0x0400873B RID: 34619
		public List<ClusterTraveler> ballisticObjects = new List<ClusterTraveler>();

		// Token: 0x0400873C RID: 34620
		public HashSet<MeteorShowerEvent.StatesInstance> lastDetectedMeteorShowers = new HashSet<MeteorShowerEvent.StatesInstance>();

		// Token: 0x0400873D RID: 34621
		public HashSet<LaunchConditionManager> lastDetectedRocketsBaseGame = new HashSet<LaunchConditionManager>();

		// Token: 0x0400873E RID: 34622
		public HashSet<Clustercraft> lastDetectedRocketsDLC1 = new HashSet<Clustercraft>();
	}
}
