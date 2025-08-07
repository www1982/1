using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200029A RID: 666
public class LogicGateBufferConfig : LogicGateBaseConfig
{
	// Token: 0x06000D73 RID: 3443 RVA: 0x0004FBA1 File Offset: 0x0004DDA1
	protected override LogicGateBase.Op GetLogicOp()
	{
		return LogicGateBase.Op.CustomSingle;
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0004FBA4 File Offset: 0x0004DDA4
	protected override CellOffset[] InputPortOffsets
	{
		get
		{
			return new CellOffset[] { CellOffset.none };
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0004FBB8 File Offset: 0x0004DDB8
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

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0004FBCE File Offset: 0x0004DDCE
	protected override CellOffset[] ControlPortOffsets
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x0004FBD4 File Offset: 0x0004DDD4
	protected override LogicGate.LogicGateDescriptions GetDescriptions()
	{
		return new LogicGate.LogicGateDescriptions
		{
			outputOne = new LogicGate.LogicGateDescriptions.Description
			{
				name = BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_NAME,
				active = BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_ACTIVE,
				inactive = BUILDINGS.PREFABS.LOGICGATEBUFFER.OUTPUT_INACTIVE
			}
		};
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x0004FC21 File Offset: 0x0004DE21
	public override BuildingDef CreateBuildingDef()
	{
		return base.CreateBuildingDef("LogicGateBUFFER", "logic_buffer_kanim", 2, 1);
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x0004FC38 File Offset: 0x0004DE38
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicGateBuffer logicGateBuffer = go.AddComponent<LogicGateBuffer>();
		logicGateBuffer.op = this.GetLogicOp();
		logicGateBuffer.inputPortOffsets = this.InputPortOffsets;
		logicGateBuffer.outputPortOffsets = this.OutputPortOffsets;
		logicGateBuffer.controlPortOffsets = this.ControlPortOffsets;
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			game_object.GetComponent<LogicGateBuffer>().SetPortDescriptions(this.GetDescriptions());
		};
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x040008E7 RID: 2279
	public const string ID = "LogicGateBUFFER";
}
