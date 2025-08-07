using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000765 RID: 1893
[SkipSaveFileSerialization]
public class LogicGateVisualizer : LogicGateBase
{
	// Token: 0x060030CE RID: 12494 RVA: 0x00117926 File Offset: 0x00115B26
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Register();
	}

	// Token: 0x060030CF RID: 12495 RVA: 0x00117934 File Offset: 0x00115B34
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.Unregister();
	}

	// Token: 0x060030D0 RID: 12496 RVA: 0x00117944 File Offset: 0x00115B44
	private void Register()
	{
		this.Unregister();
		this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.OutputCellOne, false));
		if (base.RequiresFourOutputs)
		{
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.OutputCellTwo, false));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.OutputCellThree, false));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.OutputCellFour, false));
		}
		this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellOne, true));
		if (base.RequiresTwoInputs)
		{
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellTwo, true));
		}
		else if (base.RequiresFourInputs)
		{
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellTwo, true));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellThree, true));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.InputCellFour, true));
		}
		if (base.RequiresControlInputs)
		{
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.ControlCellOne, true));
			this.visChildren.Add(new LogicGateVisualizer.IOVisualizer(base.ControlCellTwo, true));
		}
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		foreach (LogicGateVisualizer.IOVisualizer iovisualizer in this.visChildren)
		{
			logicCircuitManager.AddVisElem(iovisualizer);
		}
	}

	// Token: 0x060030D1 RID: 12497 RVA: 0x00117AC8 File Offset: 0x00115CC8
	private void Unregister()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		foreach (LogicGateVisualizer.IOVisualizer iovisualizer in this.visChildren)
		{
			logicCircuitManager.RemoveVisElem(iovisualizer);
		}
		this.visChildren.Clear();
	}

	// Token: 0x04001D75 RID: 7541
	private List<LogicGateVisualizer.IOVisualizer> visChildren = new List<LogicGateVisualizer.IOVisualizer>();

	// Token: 0x02001646 RID: 5702
	private class IOVisualizer : ILogicUIElement, IUniformGridObject
	{
		// Token: 0x0600946F RID: 37999 RVA: 0x0036F8E3 File Offset: 0x0036DAE3
		public IOVisualizer(int cell, bool input)
		{
			this.cell = cell;
			this.input = input;
		}

		// Token: 0x06009470 RID: 38000 RVA: 0x0036F8F9 File Offset: 0x0036DAF9
		public int GetLogicUICell()
		{
			return this.cell;
		}

		// Token: 0x06009471 RID: 38001 RVA: 0x0036F901 File Offset: 0x0036DB01
		public LogicPortSpriteType GetLogicPortSpriteType()
		{
			if (!this.input)
			{
				return LogicPortSpriteType.Output;
			}
			return LogicPortSpriteType.Input;
		}

		// Token: 0x06009472 RID: 38002 RVA: 0x0036F90E File Offset: 0x0036DB0E
		public Vector2 PosMin()
		{
			return Grid.CellToPos2D(this.cell);
		}

		// Token: 0x06009473 RID: 38003 RVA: 0x0036F920 File Offset: 0x0036DB20
		public Vector2 PosMax()
		{
			return this.PosMin();
		}

		// Token: 0x04007261 RID: 29281
		private int cell;

		// Token: 0x04007262 RID: 29282
		private bool input;
	}
}
