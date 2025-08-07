using System;

// Token: 0x0200089C RID: 2204
public enum BuildLocationRule
{
	// Token: 0x04002566 RID: 9574
	Anywhere,
	// Token: 0x04002567 RID: 9575
	OnFloor,
	// Token: 0x04002568 RID: 9576
	OnFloorOverSpace,
	// Token: 0x04002569 RID: 9577
	OnCeiling,
	// Token: 0x0400256A RID: 9578
	OnWall,
	// Token: 0x0400256B RID: 9579
	InCorner,
	// Token: 0x0400256C RID: 9580
	Tile,
	// Token: 0x0400256D RID: 9581
	NotInTiles,
	// Token: 0x0400256E RID: 9582
	Conduit,
	// Token: 0x0400256F RID: 9583
	LogicBridge,
	// Token: 0x04002570 RID: 9584
	WireBridge,
	// Token: 0x04002571 RID: 9585
	HighWattBridgeTile,
	// Token: 0x04002572 RID: 9586
	BuildingAttachPoint,
	// Token: 0x04002573 RID: 9587
	OnFloorOrBuildingAttachPoint,
	// Token: 0x04002574 RID: 9588
	OnFoundationRotatable,
	// Token: 0x04002575 RID: 9589
	BelowRocketCeiling,
	// Token: 0x04002576 RID: 9590
	OnRocketEnvelope,
	// Token: 0x04002577 RID: 9591
	WallFloor,
	// Token: 0x04002578 RID: 9592
	NoLiquidConduitAtOrigin
}
