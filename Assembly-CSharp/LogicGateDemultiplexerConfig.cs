using System;
using STRINGS;

// Token: 0x0200029D RID: 669
public class LogicGateDemultiplexerConfig : LogicGateBaseConfig
{
	// Token: 0x06000D8C RID: 3468 RVA: 0x0004FEC1 File Offset: 0x0004E0C1
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Demultiplexer;
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0004FEC4 File Offset: 0x0004E0C4
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(-1, 3)
			};
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0004FEDA File Offset: 0x0004E0DA
	protected override CellOffset[] OutputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(1, 3),
				new CellOffset(1, 2),
				new CellOffset(1, 1),
				new CellOffset(1, 0)
			};
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000D8F RID: 3471 RVA: 0x0004FF1A File Offset: 0x0004E11A
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				new CellOffset(-1, 0),
				new CellOffset(0, 0)
			};
		}
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x0004FF40 File Offset: 0x0004E140
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEXOR.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEXOR.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEXOR.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0004FF8D File Offset: 0x0004E18D
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateDemultiplexer", "logic_demultiplexer_kanim", 3, 4);
	}

	// Token: 0x040008EA RID: 2282
	public const string ID = "LogicGateDemultiplexer";
}
