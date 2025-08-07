using System;
using FMOD.Studio;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000973 RID: 2419
public class DragTool : InterfaceTool
{
	// Token: 0x170004F3 RID: 1267
	// (get) Token: 0x060045BA RID: 17850 RVA: 0x0019184D File Offset: 0x0018FA4D
	public bool Dragging
	{
		get
		{
			return this.dragging;
		}
	}

	// Token: 0x060045BB RID: 17851 RVA: 0x00191855 File Offset: 0x0018FA55
	protected virtual DragTool.Mode GetMode()
	{
		return this.mode;
	}

	// Token: 0x060045BC RID: 17852 RVA: 0x0019185D File Offset: 0x0018FA5D
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		this.dragging = false;
		this.SetMode(this.mode);
	}

	// Token: 0x060045BD RID: 17853 RVA: 0x00191878 File Offset: 0x0018FA78
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		if (KScreenManager.Instance != null)
		{
			KScreenManager.Instance.SetEventSystemEnabled(true);
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		this.RemoveCurrentAreaText();
		base.OnDeactivateTool(new_tool);
	}

	// Token: 0x060045BE RID: 17854 RVA: 0x001918B0 File Offset: 0x0018FAB0
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
			this.areaVisualizerSpriteRenderer = this.areaVisualizer.GetComponent<SpriteRenderer>();
			this.areaVisualizer.transform.SetParent(base.transform);
			this.areaVisualizer.GetComponent<Renderer>().material.color = this.areaColour;
		}
	}

	// Token: 0x060045BF RID: 17855 RVA: 0x00191974 File Offset: 0x0018FB74
	protected override void OnCmpEnable()
	{
		this.dragging = false;
	}

	// Token: 0x060045C0 RID: 17856 RVA: 0x0019197D File Offset: 0x0018FB7D
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

	// Token: 0x060045C1 RID: 17857 RVA: 0x001919B4 File Offset: 0x0018FBB4
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		cursor_pos = this.ClampPositionToWorld(cursor_pos, ClusterManager.Instance.activeWorld);
		this.dragging = true;
		this.downPos = cursor_pos;
		this.cellChangedSinceDown = false;
		this.previousCursorPos = cursor_pos;
		if (this.currentVirtualInputInUse != null)
		{
			this.currentVirtualInputInUse.mouseMovementOnly = false;
			this.currentVirtualInputInUse = null;
		}
		if (!KInputManager.currentControllerIsGamepad)
		{
			KScreenManager.Instance.SetEventSystemEnabled(false);
		}
		else
		{
			global::UnityEngine.EventSystems.EventSystem current = global::UnityEngine.EventSystems.EventSystem.current;
			base.SetCurrentVirtualInputModuleMousMovementMode(true, delegate(VirtualInputModule module)
			{
				this.currentVirtualInputInUse = module;
			});
		}
		this.hasFocus = true;
		this.RemoveCurrentAreaText();
		if (this.areaVisualizerTextPrefab != null)
		{
			this.areaVisualizerText = NameDisplayScreen.Instance.AddAreaText("", this.areaVisualizerTextPrefab);
			NameDisplayScreen.Instance.GetWorldText(this.areaVisualizerText).GetComponent<LocText>().color = this.areaColour;
		}
		DragTool.Mode mode = this.GetMode();
		if (mode == DragTool.Mode.Brush)
		{
			if (this.visualizer != null)
			{
				this.AddDragPoint(cursor_pos);
				return;
			}
		}
		else if (mode == DragTool.Mode.Box || mode == DragTool.Mode.Line)
		{
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(false);
			}
			if (this.areaVisualizer != null)
			{
				this.areaVisualizer.SetActive(true);
				this.areaVisualizer.transform.SetPosition(cursor_pos);
				this.areaVisualizerSpriteRenderer.size = new Vector2(0.01f, 0.01f);
			}
		}
	}

	// Token: 0x060045C2 RID: 17858 RVA: 0x00191B21 File Offset: 0x0018FD21
	public void RemoveCurrentAreaText()
	{
		if (this.areaVisualizerText != Guid.Empty)
		{
			NameDisplayScreen.Instance.RemoveWorldText(this.areaVisualizerText);
			this.areaVisualizerText = Guid.Empty;
		}
	}

	// Token: 0x060045C3 RID: 17859 RVA: 0x00191B50 File Offset: 0x0018FD50
	public void CancelDragging()
	{
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (this.currentVirtualInputInUse != null)
		{
			this.currentVirtualInputInUse.mouseMovementOnly = false;
			this.currentVirtualInputInUse = null;
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		this.dragAxis = DragTool.DragAxis.Invalid;
		if (!this.dragging)
		{
			return;
		}
		this.dragging = false;
		this.RemoveCurrentAreaText();
		DragTool.Mode mode = this.GetMode();
		if ((mode == DragTool.Mode.Box || mode == DragTool.Mode.Line) && this.areaVisualizer != null)
		{
			this.areaVisualizer.SetActive(false);
		}
	}

	// Token: 0x060045C4 RID: 17860 RVA: 0x00191BE0 File Offset: 0x0018FDE0
	public override void OnLeftClickUp(Vector3 cursor_pos)
	{
		KScreenManager.Instance.SetEventSystemEnabled(true);
		if (this.currentVirtualInputInUse != null)
		{
			this.currentVirtualInputInUse.mouseMovementOnly = false;
			this.currentVirtualInputInUse = null;
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			base.SetCurrentVirtualInputModuleMousMovementMode(false, null);
		}
		this.dragAxis = DragTool.DragAxis.Invalid;
		if (!this.dragging)
		{
			return;
		}
		this.dragging = false;
		cursor_pos = this.ClampPositionToWorld(cursor_pos, ClusterManager.Instance.activeWorld);
		this.RemoveCurrentAreaText();
		DragTool.Mode mode = this.GetMode();
		if (mode == DragTool.Mode.Line || Input.GetKey((KeyCode)Global.GetInputManager().GetDefaultController().GetInputForAction(global::Action.DragStraight)))
		{
			cursor_pos = this.SnapToLine(cursor_pos);
		}
		if ((mode == DragTool.Mode.Box || mode == DragTool.Mode.Line) && this.areaVisualizer != null)
		{
			this.areaVisualizer.SetActive(false);
			int num;
			int num2;
			Grid.PosToXY(this.downPos, out num, out num2);
			int num3 = num;
			int num4 = num2;
			int num5;
			int num6;
			Grid.PosToXY(cursor_pos, out num5, out num6);
			if (num5 < num)
			{
				global::Util.Swap<int>(ref num, ref num5);
			}
			if (num6 < num2)
			{
				global::Util.Swap<int>(ref num2, ref num6);
			}
			for (int i = num2; i <= num6; i++)
			{
				for (int j = num; j <= num5; j++)
				{
					int num7 = Grid.XYToCell(j, i);
					if (Grid.IsValidCell(num7) && Grid.IsVisible(num7))
					{
						int num8 = i - num4;
						int num9 = j - num3;
						num8 = Mathf.Abs(num8);
						num9 = Mathf.Abs(num9);
						this.OnDragTool(num7, num8 + num9);
					}
				}
			}
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound(this.GetConfirmSound(), false));
			this.OnDragComplete(this.downPos, cursor_pos);
		}
	}

	// Token: 0x060045C5 RID: 17861 RVA: 0x00191D6F File Offset: 0x0018FF6F
	protected virtual string GetConfirmSound()
	{
		return "Tile_Confirm";
	}

	// Token: 0x060045C6 RID: 17862 RVA: 0x00191D76 File Offset: 0x0018FF76
	protected virtual string GetDragSound()
	{
		return "Tile_Drag";
	}

	// Token: 0x060045C7 RID: 17863 RVA: 0x00191D7D File Offset: 0x0018FF7D
	public override string GetDeactivateSound()
	{
		return "Tile_Cancel";
	}

	// Token: 0x060045C8 RID: 17864 RVA: 0x00191D84 File Offset: 0x0018FF84
	protected Vector3 ClampPositionToWorld(Vector3 position, WorldContainer world)
	{
		position.x = Mathf.Clamp(position.x, world.minimumBounds.x, world.maximumBounds.x);
		position.y = Mathf.Clamp(position.y, world.minimumBounds.y, world.maximumBounds.y);
		return position;
	}

	// Token: 0x060045C9 RID: 17865 RVA: 0x00191DE4 File Offset: 0x0018FFE4
	protected Vector3 SnapToLine(Vector3 cursorPos)
	{
		Vector3 vector = cursorPos - this.downPos;
		if (this.canChangeDragAxis || (!this.canChangeDragAxis && !this.cellChangedSinceDown) || this.dragAxis == DragTool.DragAxis.Invalid)
		{
			this.dragAxis = DragTool.DragAxis.Invalid;
			if (Mathf.Abs(vector.x) < Mathf.Abs(vector.y))
			{
				this.dragAxis = DragTool.DragAxis.Vertical;
			}
			else
			{
				this.dragAxis = DragTool.DragAxis.Horizontal;
			}
		}
		DragTool.DragAxis dragAxis = this.dragAxis;
		if (dragAxis != DragTool.DragAxis.Horizontal)
		{
			if (dragAxis == DragTool.DragAxis.Vertical)
			{
				cursorPos.x = this.downPos.x;
				if (this.lineModeMaxLength != -1 && Mathf.Abs(vector.y) > (float)(this.lineModeMaxLength - 1))
				{
					cursorPos.y = this.downPos.y + Mathf.Sign(vector.y) * (float)(this.lineModeMaxLength - 1);
				}
			}
		}
		else
		{
			cursorPos.y = this.downPos.y;
			if (this.lineModeMaxLength != -1 && Mathf.Abs(vector.x) > (float)(this.lineModeMaxLength - 1))
			{
				cursorPos.x = this.downPos.x + Mathf.Sign(vector.x) * (float)(this.lineModeMaxLength - 1);
			}
		}
		return cursorPos;
	}

	// Token: 0x060045CA RID: 17866 RVA: 0x00191F20 File Offset: 0x00190120
	public override void OnMouseMove(Vector3 cursorPos)
	{
		cursorPos = this.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		if (this.dragging && (Input.GetKey((KeyCode)Global.GetInputManager().GetDefaultController().GetInputForAction(global::Action.DragStraight)) || this.GetMode() == DragTool.Mode.Line))
		{
			cursorPos = this.SnapToLine(cursorPos);
		}
		else
		{
			this.dragAxis = DragTool.DragAxis.Invalid;
		}
		base.OnMouseMove(cursorPos);
		if (!this.dragging)
		{
			return;
		}
		if (Grid.PosToCell(cursorPos) != Grid.PosToCell(this.downPos))
		{
			this.cellChangedSinceDown = true;
		}
		DragTool.Mode mode = this.GetMode();
		if (mode != DragTool.Mode.Brush)
		{
			if (mode - DragTool.Mode.Box <= 1)
			{
				Vector2 vector = Vector3.Max(this.downPos, cursorPos);
				Vector2 vector2 = Vector3.Min(this.downPos, cursorPos);
				vector = base.GetWorldRestrictedPosition(vector);
				vector2 = base.GetWorldRestrictedPosition(vector2);
				vector = base.GetRegularizedPos(vector, false);
				vector2 = base.GetRegularizedPos(vector2, true);
				Vector2 vector3 = vector - vector2;
				Vector2 vector4 = (vector + vector2) * 0.5f;
				this.areaVisualizer.transform.SetPosition(new Vector2(vector4.x, vector4.y));
				int num = (int)(vector.x - vector2.x + (vector.y - vector2.y) - 1f);
				if (this.areaVisualizerSpriteRenderer.size != vector3)
				{
					string sound = GlobalAssets.GetSound(this.GetDragSound(), false);
					if (sound != null)
					{
						Vector3 position = this.areaVisualizer.transform.GetPosition();
						position.z = 0f;
						EventInstance eventInstance = SoundEvent.BeginOneShot(sound, position, 1f, false);
						eventInstance.setParameterByName("tileCount", (float)num, false);
						SoundEvent.EndOneShot(eventInstance);
					}
				}
				this.areaVisualizerSpriteRenderer.size = vector3;
				if (this.areaVisualizerText != Guid.Empty)
				{
					Vector2I vector2I = new Vector2I(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
					LocText component = NameDisplayScreen.Instance.GetWorldText(this.areaVisualizerText).GetComponent<LocText>();
					component.text = string.Format(UI.TOOLS.TOOL_AREA_FMT, vector2I.x, vector2I.y, vector2I.x * vector2I.y);
					Vector2 vector5 = vector4;
					component.transform.SetPosition(vector5);
				}
			}
		}
		else
		{
			this.AddDragPoints(cursorPos, this.previousCursorPos);
			if (this.areaVisualizerText != Guid.Empty)
			{
				int dragLength = this.GetDragLength();
				LocText component2 = NameDisplayScreen.Instance.GetWorldText(this.areaVisualizerText).GetComponent<LocText>();
				component2.text = string.Format(UI.TOOLS.TOOL_LENGTH_FMT, dragLength);
				Vector3 vector6 = Grid.CellToPos(Grid.PosToCell(cursorPos));
				vector6 += new Vector3(0f, 1f, 0f);
				component2.transform.SetPosition(vector6);
			}
		}
		this.previousCursorPos = cursorPos;
	}

	// Token: 0x060045CB RID: 17867 RVA: 0x00192220 File Offset: 0x00190420
	protected virtual void OnDragTool(int cell, int distFromOrigin)
	{
	}

	// Token: 0x060045CC RID: 17868 RVA: 0x00192222 File Offset: 0x00190422
	protected virtual void OnDragComplete(Vector3 cursorDown, Vector3 cursorUp)
	{
	}

	// Token: 0x060045CD RID: 17869 RVA: 0x00192224 File Offset: 0x00190424
	protected virtual int GetDragLength()
	{
		return 0;
	}

	// Token: 0x060045CE RID: 17870 RVA: 0x00192228 File Offset: 0x00190428
	private void AddDragPoint(Vector3 cursorPos)
	{
		cursorPos = this.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		int num = Grid.PosToCell(cursorPos);
		if (Grid.IsValidCell(num) && Grid.IsVisible(num))
		{
			this.OnDragTool(num, 0);
		}
	}

	// Token: 0x060045CF RID: 17871 RVA: 0x00192268 File Offset: 0x00190468
	private void AddDragPoints(Vector3 cursorPos, Vector3 previousCursorPos)
	{
		cursorPos = this.ClampPositionToWorld(cursorPos, ClusterManager.Instance.activeWorld);
		Vector3 vector = cursorPos - previousCursorPos;
		float magnitude = vector.magnitude;
		float num = Grid.CellSizeInMeters * 0.25f;
		int num2 = 1 + (int)(magnitude / num);
		vector.Normalize();
		for (int i = 0; i < num2; i++)
		{
			Vector3 vector2 = previousCursorPos + vector * ((float)i * num);
			this.AddDragPoint(vector2);
		}
	}

	// Token: 0x060045D0 RID: 17872 RVA: 0x001922DD File Offset: 0x001904DD
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.interceptNumberKeysForPriority)
		{
			this.HandlePriortyKeysDown(e);
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x060045D1 RID: 17873 RVA: 0x001922FD File Offset: 0x001904FD
	public override void OnKeyUp(KButtonEvent e)
	{
		if (this.interceptNumberKeysForPriority)
		{
			this.HandlePriorityKeysUp(e);
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x060045D2 RID: 17874 RVA: 0x00192320 File Offset: 0x00190520
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

	// Token: 0x060045D3 RID: 17875 RVA: 0x00192384 File Offset: 0x00190584
	private void HandlePriorityKeysUp(KButtonEvent e)
	{
		global::Action action = e.GetAction();
		if (global::Action.Plan1 <= action && action <= global::Action.Plan10)
		{
			e.TryConsume(action);
		}
	}

	// Token: 0x060045D4 RID: 17876 RVA: 0x001923AC File Offset: 0x001905AC
	protected void SetMode(DragTool.Mode newMode)
	{
		this.mode = newMode;
		switch (this.mode)
		{
		case DragTool.Mode.Brush:
			if (this.areaVisualizer != null)
			{
				this.areaVisualizer.SetActive(false);
			}
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(true);
			}
			base.SetCursor(this.cursor, this.cursorOffset, CursorMode.Auto);
			return;
		case DragTool.Mode.Box:
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(true);
			}
			this.mode = DragTool.Mode.Box;
			base.SetCursor(this.boxCursor, this.cursorOffset, CursorMode.Auto);
			return;
		case DragTool.Mode.Line:
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(true);
			}
			this.mode = DragTool.Mode.Line;
			base.SetCursor(this.boxCursor, this.cursorOffset, CursorMode.Auto);
			return;
		default:
			return;
		}
	}

	// Token: 0x060045D5 RID: 17877 RVA: 0x0019248C File Offset: 0x0019068C
	public override void OnFocus(bool focus)
	{
		DragTool.Mode mode = this.GetMode();
		if (mode == DragTool.Mode.Brush)
		{
			if (this.visualizer != null)
			{
				this.visualizer.SetActive(focus);
			}
			this.hasFocus = focus;
			return;
		}
		if (mode - DragTool.Mode.Box > 1)
		{
			return;
		}
		if (this.visualizer != null && !this.dragging)
		{
			this.visualizer.SetActive(focus);
		}
		this.hasFocus = focus || this.dragging;
	}

	// Token: 0x060045D6 RID: 17878 RVA: 0x00192500 File Offset: 0x00190700
	private void OnTutorialOpened(object data)
	{
		this.dragging = false;
	}

	// Token: 0x060045D7 RID: 17879 RVA: 0x00192509 File Offset: 0x00190709
	public override bool ShowHoverUI()
	{
		return this.dragging || base.ShowHoverUI();
	}

	// Token: 0x04002E6D RID: 11885
	[SerializeField]
	private Texture2D boxCursor;

	// Token: 0x04002E6E RID: 11886
	[SerializeField]
	private GameObject areaVisualizer;

	// Token: 0x04002E6F RID: 11887
	[SerializeField]
	private GameObject areaVisualizerTextPrefab;

	// Token: 0x04002E70 RID: 11888
	[SerializeField]
	private Color32 areaColour = new Color(1f, 1f, 1f, 0.5f);

	// Token: 0x04002E71 RID: 11889
	protected SpriteRenderer areaVisualizerSpriteRenderer;

	// Token: 0x04002E72 RID: 11890
	protected Guid areaVisualizerText;

	// Token: 0x04002E73 RID: 11891
	protected Vector3 placementPivot;

	// Token: 0x04002E74 RID: 11892
	protected bool interceptNumberKeysForPriority;

	// Token: 0x04002E75 RID: 11893
	private bool dragging;

	// Token: 0x04002E76 RID: 11894
	private Vector3 previousCursorPos;

	// Token: 0x04002E77 RID: 11895
	private DragTool.Mode mode = DragTool.Mode.Box;

	// Token: 0x04002E78 RID: 11896
	private DragTool.DragAxis dragAxis = DragTool.DragAxis.Invalid;

	// Token: 0x04002E79 RID: 11897
	protected bool canChangeDragAxis = true;

	// Token: 0x04002E7A RID: 11898
	protected int lineModeMaxLength = -1;

	// Token: 0x04002E7B RID: 11899
	protected Vector3 downPos;

	// Token: 0x04002E7C RID: 11900
	private bool cellChangedSinceDown;

	// Token: 0x04002E7D RID: 11901
	private VirtualInputModule currentVirtualInputInUse;

	// Token: 0x0200197E RID: 6526
	private enum DragAxis
	{
		// Token: 0x04007CB5 RID: 31925
		Invalid = -1,
		// Token: 0x04007CB6 RID: 31926
		None,
		// Token: 0x04007CB7 RID: 31927
		Horizontal,
		// Token: 0x04007CB8 RID: 31928
		Vertical
	}

	// Token: 0x0200197F RID: 6527
	public enum Mode
	{
		// Token: 0x04007CBA RID: 31930
		Brush,
		// Token: 0x04007CBB RID: 31931
		Box,
		// Token: 0x04007CBC RID: 31932
		Line
	}
}
