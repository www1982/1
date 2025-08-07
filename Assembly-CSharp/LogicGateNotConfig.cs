using System;
using STRINGS;

// Token: 0x02000299 RID: 665
public class LogicGateNotConfig : LogicGateBaseConfig
{
	// Token: 0x06000D6C RID: 3436 RVA: 0x0004FB05 File Offset: 0x0004DD05
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Not;
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000D6D RID: 3437 RVA: 0x0004FB08 File Offset: 0x0004DD08
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[] { CellOffset.none };
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0004FB1C File Offset: 0x0004DD1C
	protected override CellOffset[] OutputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(1, 0)
			};
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0004FB32 File Offset: 0x0004DD32
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0004FB38 File Offset: 0x0004DD38
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATENOT.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATENOT.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATENOT.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0004FB85 File Offset: 0x0004DD85
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateNOT", "logic_not_kanim", 2, 1);
	}

	// Token: 0x040008E6 RID: 2278
	public const string ID = "LogicGateNOT";
}
