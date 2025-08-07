using System;
using STRINGS;

// Token: 0x02000296 RID: 662
public class LogicGateAndConfig : LogicGateBaseConfig
{
	// Token: 0x06000D57 RID: 3415 RVA: 0x0004F90E File Offset: 0x0004DB0E
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.And;
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0004F911 File Offset: 0x0004DB11
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

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0004F933 File Offset: 0x0004DB33
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

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0004F949 File Offset: 0x0004DB49
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0004F94C File Offset: 0x0004DB4C
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEAND.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEAND.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEAND.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0004F999 File Offset: 0x0004DB99
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateAND", "logic_and_kanim", 2, 2);
	}

	// Token: 0x040008E3 RID: 2275
	public const string ID = "LogicGateAND";
}
