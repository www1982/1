using System;
using UnityEngine;

// Token: 0x02000763 RID: 1891
[AddComponentMenu("KMonoBehaviour/scripts/LogicGateBase")]
public class LogicGateBase : KMonoBehaviour
{
	// Token: 0x0600309A RID: 12442 RVA: 0x001155C0 File Offset: 0x001137C0
	private int GetActualCell(CellOffset offset)
	{
		Rotatable component = base.GetComponent<Rotatable>();
		if (component != null)
		{
			offset = component.GetRotatedCellOffset(offset);
		}
		return Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), offset);
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x0600309B RID: 12443 RVA: 0x001155FC File Offset: 0x001137FC
	public int InputCellOne
	{
		get
		{
			return this.GetActualCell(this.inputPortOffsets[0]);
		}
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x0600309C RID: 12444 RVA: 0x00115610 File Offset: 0x00113810
	public int InputCellTwo
	{
		get
		{
			return this.GetActualCell(this.inputPortOffsets[1]);
		}
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x0600309D RID: 12445 RVA: 0x00115624 File Offset: 0x00113824
	public int InputCellThree
	{
		get
		{
			return this.GetActualCell(this.inputPortOffsets[2]);
		}
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x0600309E RID: 12446 RVA: 0x00115638 File Offset: 0x00113838
	public int InputCellFour
	{
		get
		{
			return this.GetActualCell(this.inputPortOffsets[3]);
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x0600309F RID: 12447 RVA: 0x0011564C File Offset: 0x0011384C
	public int OutputCellOne
	{
		get
		{
			return this.GetActualCell(this.outputPortOffsets[0]);
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x060030A0 RID: 12448 RVA: 0x00115660 File Offset: 0x00113860
	public int OutputCellTwo
	{
		get
		{
			return this.GetActualCell(this.outputPortOffsets[1]);
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x060030A1 RID: 12449 RVA: 0x00115674 File Offset: 0x00113874
	public int OutputCellThree
	{
		get
		{
			return this.GetActualCell(this.outputPortOffsets[2]);
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x060030A2 RID: 12450 RVA: 0x00115688 File Offset: 0x00113888
	public int OutputCellFour
	{
		get
		{
			return this.GetActualCell(this.outputPortOffsets[3]);
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x060030A3 RID: 12451 RVA: 0x0011569C File Offset: 0x0011389C
	public int ControlCellOne
	{
		get
		{
			return this.GetActualCell(this.controlPortOffsets[0]);
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x060030A4 RID: 12452 RVA: 0x001156B0 File Offset: 0x001138B0
	public int ControlCellTwo
	{
		get
		{
			return this.GetActualCell(this.controlPortOffsets[1]);
		}
	}

	// Token: 0x060030A5 RID: 12453 RVA: 0x001156C4 File Offset: 0x001138C4
	public int PortCell(LogicGateBase.PortId port)
	{
		switch (port)
		{
		case LogicGateBase.PortId.InputOne:
			return this.InputCellOne;
		case LogicGateBase.PortId.InputTwo:
			return this.InputCellTwo;
		case LogicGateBase.PortId.InputThree:
			return this.InputCellThree;
		case LogicGateBase.PortId.InputFour:
			return this.InputCellFour;
		case LogicGateBase.PortId.OutputOne:
			return this.OutputCellOne;
		case LogicGateBase.PortId.OutputTwo:
			return this.OutputCellTwo;
		case LogicGateBase.PortId.OutputThree:
			return this.OutputCellThree;
		case LogicGateBase.PortId.OutputFour:
			return this.OutputCellFour;
		case LogicGateBase.PortId.ControlOne:
			return this.ControlCellOne;
		case LogicGateBase.PortId.ControlTwo:
			return this.ControlCellTwo;
		default:
			return this.OutputCellOne;
		}
	}

	// Token: 0x060030A6 RID: 12454 RVA: 0x00115750 File Offset: 0x00113950
	public bool TryGetPortAtCell(int cell, out LogicGateBase.PortId port)
	{
		if (cell == this.InputCellOne)
		{
			port = LogicGateBase.PortId.InputOne;
			return true;
		}
		if ((this.RequiresTwoInputs || this.RequiresFourInputs) && cell == this.InputCellTwo)
		{
			port = LogicGateBase.PortId.InputTwo;
			return true;
		}
		if (this.RequiresFourInputs && cell == this.InputCellThree)
		{
			port = LogicGateBase.PortId.InputThree;
			return true;
		}
		if (this.RequiresFourInputs && cell == this.InputCellFour)
		{
			port = LogicGateBase.PortId.InputFour;
			return true;
		}
		if (cell == this.OutputCellOne)
		{
			port = LogicGateBase.PortId.OutputOne;
			return true;
		}
		if (this.RequiresFourOutputs && cell == this.OutputCellTwo)
		{
			port = LogicGateBase.PortId.OutputTwo;
			return true;
		}
		if (this.RequiresFourOutputs && cell == this.OutputCellThree)
		{
			port = LogicGateBase.PortId.OutputThree;
			return true;
		}
		if (this.RequiresFourOutputs && cell == this.OutputCellFour)
		{
			port = LogicGateBase.PortId.OutputFour;
			return true;
		}
		if (this.RequiresControlInputs && cell == this.ControlCellOne)
		{
			port = LogicGateBase.PortId.ControlOne;
			return true;
		}
		if (this.RequiresControlInputs && cell == this.ControlCellTwo)
		{
			port = LogicGateBase.PortId.ControlTwo;
			return true;
		}
		port = LogicGateBase.PortId.InputOne;
		return false;
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x060030A7 RID: 12455 RVA: 0x00115836 File Offset: 0x00113A36
	public bool RequiresTwoInputs
	{
		get
		{
			return LogicGateBase.OpRequiresTwoInputs(this.op);
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x060030A8 RID: 12456 RVA: 0x00115843 File Offset: 0x00113A43
	public bool RequiresFourInputs
	{
		get
		{
			return LogicGateBase.OpRequiresFourInputs(this.op);
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x060030A9 RID: 12457 RVA: 0x00115850 File Offset: 0x00113A50
	public bool RequiresFourOutputs
	{
		get
		{
			return LogicGateBase.OpRequiresFourOutputs(this.op);
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x060030AA RID: 12458 RVA: 0x0011585D File Offset: 0x00113A5D
	public bool RequiresControlInputs
	{
		get
		{
			return LogicGateBase.OpRequiresControlInputs(this.op);
		}
	}

	// Token: 0x060030AB RID: 12459 RVA: 0x0011586A File Offset: 0x00113A6A
	public static bool OpRequiresTwoInputs(LogicGateBase.Op op)
	{
		return op != LogicGateBase.Op.Not && op - LogicGateBase.Op.CustomSingle > 2;
	}

	// Token: 0x060030AC RID: 12460 RVA: 0x00115879 File Offset: 0x00113A79
	public static bool OpRequiresFourInputs(LogicGateBase.Op op)
	{
		return op == LogicGateBase.Op.Multiplexer;
	}

	// Token: 0x060030AD RID: 12461 RVA: 0x00115882 File Offset: 0x00113A82
	public static bool OpRequiresFourOutputs(LogicGateBase.Op op)
	{
		return op == LogicGateBase.Op.Demultiplexer;
	}

	// Token: 0x060030AE RID: 12462 RVA: 0x0011588B File Offset: 0x00113A8B
	public static bool OpRequiresControlInputs(LogicGateBase.Op op)
	{
		return op - LogicGateBase.Op.Multiplexer <= 1;
	}

	// Token: 0x04001D02 RID: 7426
	public static LogicModeUI uiSrcData;

	// Token: 0x04001D03 RID: 7427
	public static readonly HashedString OUTPUT_TWO_PORT_ID = new HashedString("LogicGateOutputTwo");

	// Token: 0x04001D04 RID: 7428
	public static readonly HashedString OUTPUT_THREE_PORT_ID = new HashedString("LogicGateOutputThree");

	// Token: 0x04001D05 RID: 7429
	public static readonly HashedString OUTPUT_FOUR_PORT_ID = new HashedString("LogicGateOutputFour");

	// Token: 0x04001D06 RID: 7430
	[SerializeField]
	public LogicGateBase.Op op;

	// Token: 0x04001D07 RID: 7431
	public static CellOffset[] portOffsets = new CellOffset[]
	{
		CellOffset.none,
		new CellOffset(0, 1),
		new CellOffset(1, 0)
	};

	// Token: 0x04001D08 RID: 7432
	public CellOffset[] inputPortOffsets;

	// Token: 0x04001D09 RID: 7433
	public CellOffset[] outputPortOffsets;

	// Token: 0x04001D0A RID: 7434
	public CellOffset[] controlPortOffsets;

	// Token: 0x02001642 RID: 5698
	public enum PortId
	{
		// Token: 0x04007244 RID: 29252
		InputOne,
		// Token: 0x04007245 RID: 29253
		InputTwo,
		// Token: 0x04007246 RID: 29254
		InputThree,
		// Token: 0x04007247 RID: 29255
		InputFour,
		// Token: 0x04007248 RID: 29256
		OutputOne,
		// Token: 0x04007249 RID: 29257
		OutputTwo,
		// Token: 0x0400724A RID: 29258
		OutputThree,
		// Token: 0x0400724B RID: 29259
		OutputFour,
		// Token: 0x0400724C RID: 29260
		ControlOne,
		// Token: 0x0400724D RID: 29261
		ControlTwo
	}

	// Token: 0x02001643 RID: 5699
	public enum Op
	{
		// Token: 0x0400724F RID: 29263
		And,
		// Token: 0x04007250 RID: 29264
		Or,
		// Token: 0x04007251 RID: 29265
		Not,
		// Token: 0x04007252 RID: 29266
		Xor,
		// Token: 0x04007253 RID: 29267
		CustomSingle,
		// Token: 0x04007254 RID: 29268
		Multiplexer,
		// Token: 0x04007255 RID: 29269
		Demultiplexer
	}
}
