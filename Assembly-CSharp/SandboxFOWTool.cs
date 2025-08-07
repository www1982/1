using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000982 RID: 2434
public class SandboxFOWTool : BrushTool
{
	// Token: 0x0600468A RID: 18058 RVA: 0x001957D2 File Offset: 0x001939D2
	public static void DestroyInstance()
	{
		SandboxFOWTool.instance = null;
	}

	// Token: 0x170004F9 RID: 1273
	// (get) Token: 0x0600468B RID: 18059 RVA: 0x001957DA File Offset: 0x001939DA
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x0600468C RID: 18060 RVA: 0x001957E6 File Offset: 0x001939E6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxFOWTool.instance = this;
	}

	// Token: 0x0600468D RID: 18061 RVA: 0x001957F4 File Offset: 0x001939F4
	protected override string GetDragSound()
	{
		return "";
	}

	// Token: 0x0600468E RID: 18062 RVA: 0x001957FB File Offset: 0x001939FB
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x0600468F RID: 18063 RVA: 0x00195808 File Offset: 0x00193A08
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
	}

	// Token: 0x06004690 RID: 18064 RVA: 0x0019583F File Offset: 0x00193A3F
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
		this.ev.release();
	}

	// Token: 0x06004691 RID: 18065 RVA: 0x00195864 File Offset: 0x00193A64
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.recentlyAffectedCells)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.recentlyAffectedCellColor));
		}
		foreach (int num2 in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num2, this.radiusIndicatorColor));
		}
	}

	// Token: 0x06004692 RID: 18066 RVA: 0x0019591C File Offset: 0x00193B1C
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x06004693 RID: 18067 RVA: 0x00195925 File Offset: 0x00193B25
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		Grid.Reveal(cell, byte.MaxValue, true);
	}

	// Token: 0x06004694 RID: 18068 RVA: 0x0019593C File Offset: 0x00193B3C
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		int intSetting = this.settings.GetIntSetting("SandboxTools.BrushSize");
		this.ev = KFMOD.CreateInstance(GlobalAssets.GetSound("SandboxTool_Reveal", false));
		this.ev.setParameterByName("BrushSize", (float)intSetting, false);
		this.ev.start();
	}

	// Token: 0x06004695 RID: 18069 RVA: 0x00195997 File Offset: 0x00193B97
	public override void OnLeftClickUp(Vector3 cursor_pos)
	{
		base.OnLeftClickUp(cursor_pos);
		this.ev.stop(STOP_MODE.ALLOWFADEOUT);
		this.ev.release();
	}

	// Token: 0x04002EC4 RID: 11972
	public static SandboxFOWTool instance;

	// Token: 0x04002EC5 RID: 11973
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x04002EC6 RID: 11974
	protected Color recentlyAffectedCellColor = new Color(1f, 1f, 1f, 0.1f);

	// Token: 0x04002EC7 RID: 11975
	private EventInstance ev;
}
