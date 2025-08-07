using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000967 RID: 2407
public class BrushTool : InterfaceTool
{
	// Token: 0x170004F0 RID: 1264
	// (get) Token: 0x06004522 RID: 17698 RVA: 0x0018EB21 File Offset: 0x0018CD21
	public bool Dragging
	{
		get
		{
			return this.dragging;
		}
	}

	// Token: 0x06004523 RID: 17699 RVA: 0x0018EB29 File Offset: 0x0018CD29
	protected virtual void PlaySound()
	{
	}

	// Token: 0x06004524 RID: 17700 RVA: 0x0018EB2B File Offset: 0x0018CD2B
	protected virtual void clearVisitedCells()
	{
		this.visitedCells.Clear();
	}

	// Token: 0x06004525 RID: 17701 RVA: 0x0018EB38 File Offset: 0x0018CD38
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		this.dragging = false;
	}

	// Token: 0x06004526 RID: 17702 RVA: 0x0018EB48 File Offset: 0x0018CD48
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.radiusIndicatorColor));
		}
	}

	// Token: 0x06004527 RID: 17703 RVA: 0x0018EBB0 File Offset: 0x0018CDB0
	public virtual void SetBrushSize(int radius)
	{
		if (radius == this.brushRadius)
		{
			return;
		}
		this.brushRadius = radius;
		this.brushOffsets.Clear();
		for (int i = 0; i < this.brushRadius * 2; i++)
		{
			for (int j = 0; j < this.brushRadius * 2; j++)
			{
				if (Vector2.Distance(new Vector2((float)i, (float)j), new Vector2((float)this.brushRadius, (float)this.brushRadius)) < (float)this.brushRadius - 0.8f)
				{
					this.brushOffsets.Add(new Vector2((float)(i - this.brushRadius), (float)(j - this.brushRadius)));
				}
			}
		}
	}

	// Token: 0x06004528 RID: 17704 RVA: 0x0018EC51 File Offset: 0x0018CE51
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		base.OnDeactivateTool(new_tool);
	}

	// Token: 0x06004529 RID: 17705 RVA: 0x0018EC74 File Offset: 0x0018CE74
	protected override void OnPrefabInit()
	{
		Game.Instance.Subscribe(1634669191, new Action<object>(this.OnTutorialOpened));
		base.OnPrefabInit();
		if (this.visualizer != null)
		{
			this.visualizer = global::Util.KInstantiate(this.visualizer, null, null);
		}
		if (this.areaVisualizer != null)
		{
			this.areaVisualizer = global::Util.KInstantiate(this.areaVisualizer, null, null);
			this.areaVisualizer.SetActive(false);
			this.areaVisualizer.GetComponent<RectTransform>().SetParent(base.transform);
			this.areaVisualizer.GetComponent<Renderer>().material.color = this.areaColour;
		}
	}

	// Token: 0x0600452A RID: 17706 RVA: 0x0018ED27 File Offset: 0x0018CF27
	protected override void OnCmpEnable()
	{
		this.dragging = false;
	}

	// Token: 0x0600452B RID: 17707 RVA: 0x0018ED30 File Offset: 0x0018CF30
	protected override void OnCmpDisable()
	{
		if (this.visualizer != null)
		{
			this.visualizer.SetActive(false);
		}
		if (this.areaVisualizer != null)
		{
			this.areaVisualizer.SetActive(false);
		}
	}

	// Token: 0x0600452C RID: 17708 RVA: 0x0018ED66 File Offset: 0x0018CF66
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		cursor_pos -= this.placementPivot;
		this.dragging = true;
		this.downPos = cursor_pos;
		if (!KInputManager.currentControllerIsGamepad)
		{
			KScreenManager.Instance.SetEventSystemEnabled(false);
		}
		else
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(true, null);
		}
		this.Paint();
	}

	// Token: 0x0600452D RID: 17709 RVA: 0x0018EDA8 File Offset: 0x0018CFA8
	public override void OnLeftClickUp(Vector3 cursor_pos)
	{
		cursor_pos -= this.placementPivot;
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		if (!this.dragging)
		{
			return;
		}
		this.dragging = false;
		BrushTool.DragAxis dragAxis = this.dragAxis;
		if (dragAxis == BrushTool.DragAxis.Horizontal)
		{
			cursor_pos.y = this.downPos.y;
			this.dragAxis = BrushTool.DragAxis.None;
			return;
		}
		if (dragAxis != BrushTool.DragAxis.Vertical)
		{
			return;
		}
		cursor_pos.x = this.downPos.x;
		this.dragAxis = BrushTool.DragAxis.None;
	}

	// Token: 0x0600452E RID: 17710 RVA: 0x0018EE30 File Offset: 0x0018D030
	protected virtual string GetConfirmSound()
	{
		return "Tile_Confirm";
	}

	// Token: 0x0600452F RID: 17711 RVA: 0x0018EE37 File Offset: 0x0018D037
	protected virtual string GetDragSound()
	{
		return "Tile_Drag";
	}

	// Token: 0x06004530 RID: 17712 RVA: 0x0018EE3E File Offset: 0x0018D03E
	public override string GetDeactivateSound()
	{
		return "Tile_Cancel";
	}

	// Token: 0x06004531 RID: 17713 RVA: 0x0018EE48 File Offset: 0x0018D048
	private static int GetGridDistance(int cell, int center_cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = Grid.CellToXY(center_cell);
		Vector2I vector2I3 = vector2I - vector2I2;
		return Math.Abs(vector2I3.x) + Math.Abs(vector2I3.y);
	}

	// Token: 0x06004532 RID: 17714 RVA: 0x0018EE80 File Offset: 0x0018D080
	private void Paint()
	{
		int count = this.visitedCells.Count;
		foreach (int num in this.cellsInRadius)
		{
			if (Grid.IsValidCell(num) && (int)Grid.WorldIdx[num] == ClusterManager.Instance.activeWorldId && (!Grid.Foundation[num] || this.affectFoundation))
			{
				this.OnPaintCell(num, Grid.GetCellDistance(this.currentCell, num));
			}
		}
		if (this.lastCell != this.currentCell)
		{
			this.PlayDragSound();
		}
		if (count < this.visitedCells.Count)
		{
			this.PlaySound();
		}
	}

	// Token: 0x06004533 RID: 17715 RVA: 0x0018EF44 File Offset: 0x0018D144
	protected virtual void PlayDragSound()
	{
		string dragSound = this.GetDragSound();
		if (!string.IsNullOrEmpty(dragSound))
		{
			string sound = GlobalAssets.GetSound(dragSound, false);
			if (sound != null)
			{
				Vector3 vector = Grid.CellToPos(this.currentCell);
				vector.z = 0f;
				int cellDistance = Grid.GetCellDistance(Grid.PosToCell(this.downPos), this.currentCell);
				EventInstance eventInstance = SoundEvent.BeginOneShot(sound, vector, 1f, false);
				eventInstance.setParameterByName("tileCount", (float)cellDistance, false);
				SoundEvent.EndOneShot(eventInstance);
			}
		}
	}

	// Token: 0x06004534 RID: 17716 RVA: 0x0018EFC4 File Offset: 0x0018D1C4
	public override void OnMouseMove(Vector3 cursorPos)
	{
		int num = Grid.PosToCell(cursorPos);
		this.currentCell = num;
		base.OnMouseMove(cursorPos);
		this.cellsInRadius.Clear();
		foreach (Vector2 vector in this.brushOffsets)
		{
			int num2 = Grid.OffsetCell(Grid.PosToCell(cursorPos), new CellOffset((int)vector.x, (int)vector.y));
			if (Grid.IsValidCell(num2) && (int)Grid.WorldIdx[num2] == ClusterManager.Instance.activeWorldId)
			{
				this.cellsInRadius.Add(Grid.OffsetCell(Grid.PosToCell(cursorPos), new CellOffset((int)vector.x, (int)vector.y)));
			}
		}
		if (!this.dragging)
		{
			return;
		}
		this.Paint();
		this.lastCell = this.currentCell;
	}

	// Token: 0x06004535 RID: 17717 RVA: 0x0018F0B4 File Offset: 0x0018D2B4
	protected virtual void OnPaintCell(int cell, int distFromOrigin)
	{
		if (!this.visitedCells.Contains(cell))
		{
			this.visitedCells.Add(cell);
		}
	}

	// Token: 0x06004536 RID: 17718 RVA: 0x0018F0D0 File Offset: 0x0018D2D0
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.DragStraight))
		{
			this.dragAxis = BrushTool.DragAxis.None;
		}
		else if (this.interceptNumberKeysForPriority)
		{
			this.HandlePriortyKeysDown(e);
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06004537 RID: 17719 RVA: 0x0018F103 File Offset: 0x0018D303
	public override void OnKeyUp(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.DragStraight))
		{
			this.dragAxis = BrushTool.DragAxis.Invalid;
		}
		else if (this.interceptNumberKeysForPriority)
		{
			this.HandlePriorityKeysUp(e);
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06004538 RID: 17720 RVA: 0x0018F138 File Offset: 0x0018D338
	private void HandlePriortyKeysDown(KButtonEvent e)
	{
		global::Action action = e.GetAction();
		if (global::Action.Plan1 > action || action > global::Action.Plan10 || !e.TryConsume(action))
		{
			return;
		}
		int num = action - global::Action.Plan1 + 1;
		if (num <= 9)
		{
			ToolMenu.Instance.PriorityScreen.SetScreenPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, num), true);
			return;
		}
		ToolMenu.Instance.PriorityScreen.SetScreenPriority(new PrioritySetting(PriorityScreen.PriorityClass.topPriority, 1), true);
	}

	// Token: 0x06004539 RID: 17721 RVA: 0x0018F19C File Offset: 0x0018D39C
	private void HandlePriorityKeysUp(KButtonEvent e)
	{
		global::Action action = e.GetAction();
		if (global::Action.Plan1 <= action && action <= global::Action.Plan10)
		{
			e.TryConsume(action);
		}
	}

	// Token: 0x0600453A RID: 17722 RVA: 0x0018F1C2 File Offset: 0x0018D3C2
	public override void OnFocus(bool focus)
	{
		if (this.visualizer != null)
		{
			this.visualizer.SetActive(focus);
		}
		this.hasFocus = focus;
		base.OnFocus(focus);
	}

	// Token: 0x0600453B RID: 17723 RVA: 0x0018F1EC File Offset: 0x0018D3EC
	private void OnTutorialOpened(object data)
	{
		this.dragging = false;
	}

	// Token: 0x0600453C RID: 17724 RVA: 0x0018F1F5 File Offset: 0x0018D3F5
	public override bool ShowHoverUI()
	{
		return this.dragging || base.ShowHoverUI();
	}

	// Token: 0x0600453D RID: 17725 RVA: 0x0018F207 File Offset: 0x0018D407
	public override void LateUpdate()
	{
		base.LateUpdate();
	}

	// Token: 0x04002E3B RID: 11835
	[SerializeField]
	private Texture2D brushCursor;

	// Token: 0x04002E3C RID: 11836
	[SerializeField]
	private GameObject areaVisualizer;

	// Token: 0x04002E3D RID: 11837
	[SerializeField]
	private Color32 areaColour = new Color(1f, 1f, 1f, 0.5f);

	// Token: 0x04002E3E RID: 11838
	protected Color radiusIndicatorColor = new Color(0.5f, 0.7f, 0.5f, 0.2f);

	// Token: 0x04002E3F RID: 11839
	protected Vector3 placementPivot;

	// Token: 0x04002E40 RID: 11840
	protected bool interceptNumberKeysForPriority;

	// Token: 0x04002E41 RID: 11841
	protected List<Vector2> brushOffsets = new List<Vector2>();

	// Token: 0x04002E42 RID: 11842
	protected bool affectFoundation;

	// Token: 0x04002E43 RID: 11843
	private bool dragging;

	// Token: 0x04002E44 RID: 11844
	protected int brushRadius = -1;

	// Token: 0x04002E45 RID: 11845
	private BrushTool.DragAxis dragAxis = BrushTool.DragAxis.Invalid;

	// Token: 0x04002E46 RID: 11846
	protected Vector3 downPos;

	// Token: 0x04002E47 RID: 11847
	protected int currentCell;

	// Token: 0x04002E48 RID: 11848
	protected int lastCell;

	// Token: 0x04002E49 RID: 11849
	protected List<int> visitedCells = new List<int>();

	// Token: 0x04002E4A RID: 11850
	protected HashSet<int> cellsInRadius = new HashSet<int>();

	// Token: 0x02001978 RID: 6520
	private enum DragAxis
	{
		// Token: 0x04007C94 RID: 31892
		Invalid = -1,
		// Token: 0x04007C95 RID: 31893
		None,
		// Token: 0x04007C96 RID: 31894
		Horizontal,
		// Token: 0x04007C97 RID: 31895
		Vertical
	}
}
