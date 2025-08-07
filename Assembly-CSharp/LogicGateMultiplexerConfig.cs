using System;
using STRINGS;

// Token: 0x0200029C RID: 668
public class LogicGateMultiplexerConfig : LogicGateBaseConfig
{
	// Token: 0x06000D85 RID: 3461 RVA: 0x0004FDD9 File Offset: 0x0004DFD9
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Multiplexer;
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0004FDDC File Offset: 0x0004DFDC
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(-1, 3),
				new CellOffset(-1, 2),
				new CellOffset(-1, 1),
				new CellOffset(-1, 0)
			};
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000D87 RID: 3463 RVA: 0x0004FE1C File Offset: 0x0004E01C
	protected override CellOffset[] OutputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(1, 3)
			};
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0004FE32 File Offset: 0x0004E032
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(0, 0),
				new CellOffset(1, 0)
			};
		}
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0004FE58 File Offset: 0x0004E058
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEMULTIPLEXER.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEMULTIPLEXER.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEMULTIPLEXER.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0004FEA5 File Offset: 0x0004E0A5
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateMultiplexer", "logic_multiplexer_kanim", 3, 4);
	}

	// Token: 0x040008E9 RID: 2281
	public const string ID = "LogicGateMultiplexer";
}
