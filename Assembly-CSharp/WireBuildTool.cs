using System;

// Token: 0x02000998 RID: 2456
public class WireBuildTool : BaseUtilityBuildTool
{
	// Token: 0x0600473B RID: 18235 RVA: 0x0019AD22 File Offset: 0x00198F22
	public static void DestroyInstance()
	{
		WireBuildTool.Instance = null;
	}

	// Token: 0x0600473C RID: 18236 RVA: 0x0019AD2A File Offset: 0x00198F2A
	protected override void OnPrefabInit()
	{
		WireBuildTool.Instance = this;
		base.OnPrefabInit();
		this.viewMode = OverlayModes.Power.ID;
	}

	// Token: 0x0600473D RID: 18237 RVA: 0x0019AD44 File Offset: 0x00198F44
	protected override void ApplyPathToConduitSystem()
	{
		if (this.path.Count < 2)
		{
			return;
		}
		for (int i = 1; i < this.path.Count; i++)
		{
			if (this.path[i - 1].valid && this.path[i].valid)
			{
				int cell = this.path[i - 1].cell;
				int cell2 = this.path[i].cell;
				UtilityConnections utilityConnections = UtilityConnectionsExtensions.DirectionFromToCell(cell, this.path[i].cell);
				if (utilityConnections != (UtilityConnections)0)
				{
					UtilityConnections utilityConnections2 = utilityConnections.InverseDirection();
					this.conduitMgr.AddConnection(utilityConnections, cell, false);
					this.conduitMgr.AddConnection(utilityConnections2, cell2, false);
				}
			}
		}
	}

	// Token: 0x04002F17 RID: 12055
	public static WireBuildTool Instance;
}
