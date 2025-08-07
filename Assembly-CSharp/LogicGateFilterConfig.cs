using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200029B RID: 667
public class LogicGateFilterConfig : LogicGateBaseConfig
{
	// Token: 0x06000D7C RID: 3452 RVA: 0x0004FCBD File Offset: 0x0004DEBD
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.CustomSingle;
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x06000D7D RID: 3453 RVA: 0x0004FCC0 File Offset: 0x0004DEC0
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[] { CellOffset.none };
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0004FCD4 File Offset: 0x0004DED4
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

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000D7F RID: 3455 RVA: 0x0004FCEA File Offset: 0x0004DEEA
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0004FCF0 File Offset: 0x0004DEF0
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEFILTER.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0004FD3D File Offset: 0x0004DF3D
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateFILTER", "logic_filter_kanim", 2, 1);
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0004FD54 File Offset: 0x0004DF54
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicGateFilter logicGateFilter = go.AddComponent<LogicGateFilter>();
		logicGateFilter.op = this.GetLogicOp();
		logicGateFilter.inputPortOffsets = this.InputPortOffsets;
		logicGateFilter.outputPortOffsets = this.OutputPortOffsets;
		logicGateFilter.controlPortOffsets = this.ControlPortOffsets;
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			game_object.GetComponent<LogicGateFilter>().SetPortDescriptions(this.GetDescriptions());
		};
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x040008E8 RID: 2280
	public const string ID = "LogicGateFILTER";
}
