using System;
using STRINGS;

// Token: 0x02000298 RID: 664
public class LogicGateXorConfig : LogicGateBaseConfig
{
	// Token: 0x06000D65 RID: 3429 RVA: 0x0004FA5D File Offset: 0x0004DC5D
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.Xor;
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0004FA60 File Offset: 0x0004DC60
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

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000D67 RID: 3431 RVA: 0x0004FA82 File Offset: 0x0004DC82
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

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0004FA98 File Offset: 0x0004DC98
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0004FA9C File Offset: 0x0004DC9C
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

	// Token: 0x06000D6A RID: 3434 RVA: 0x0004FAE9 File Offset: 0x0004DCE9
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateXOR", "logic_xor_kanim", 2, 2);
	}

	// Token: 0x040008E5 RID: 2277
	public const string ID = "LogicGateXOR";
}
