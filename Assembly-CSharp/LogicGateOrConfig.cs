using System;
using STRINGS;

// Token: 0x02000297 RID: 663
public class LogicGateOrConfig : LogicGateBaseConfig
{
	// Token: 0x06000D5E RID: 3422 RVA: 0x0004F9B5 File Offset: 0x0004DBB5
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Or;
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[]
			{
				CellOffset.none,
				new CellOffset(0, 1)
			};
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0004F9DA File Offset: 0x0004DBDA
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

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0004F9F0 File Offset: 0x0004DBF0
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0004F9F4 File Offset: 0x0004DBF4
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEOR.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEOR.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEOR.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0004FA41 File Offset: 0x0004DC41
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateOR", "logic_or_kanim", 2, 2);
	}

	// Token: 0x040008E4 RID: 2276
	public const string ID = "LogicGateOR";
}
