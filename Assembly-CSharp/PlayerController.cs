using System;
using System.Collections.Generic;
using Klei.Input;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000A74 RID: 2676
[AddComponentMenu("KMonoBehaviour/scripts/PlayerController")]
public class PlayerController : KMonoBehaviour, IInputHandler
{
	// Token: 0x17000543 RID: 1347
	// (get) Token: 0x06004D75 RID: 19829 RVA: 0x001C0BAD File Offset: 0x001BEDAD
	public string handlerName
	{
		get
		{
			return "PlayerController";
		}
	}

	// Token: 0x17000544 RID: 1348
	// (get) Token: 0x06004D76 RID: 19830 RVA: 0x001C0BB4 File Offset: 0x001BEDB4
	// (set) Token: 0x06004D77 RID: 19831 RVA: 0x001C0BBC File Offset: 0x001BEDBC
	public KInputHandler inputHandler { get; set; }

	// Token: 0x17000545 RID: 1349
	// (get) Token: 0x06004D78 RID: 19832 RVA: 0x001C0BC5 File Offset: 0x001BEDC5
	public InterfaceTool ActiveTool
	{
		get
		{
			return this.activeTool;
		}
	}

	// Token: 0x17000546 RID: 1350
	// (get) Token: 0x06004D79 RID: 19833 RVA: 0x001C0BCD File Offset: 0x001BEDCD
	// (set) Token: 0x06004D7A RID: 19834 RVA: 0x001C0BD4 File Offset: 0x001BEDD4
	public static PlayerController Instance { get; private set; }

	// Token: 0x06004D7B RID: 19835 RVA: 0x001C0BDC File Offset: 0x001BEDDC
	public static void DestroyInstance()
	{
		PlayerController.Instance = null;
	}

	// Token: 0x06004D7C RID: 19836 RVA: 0x001C0BE4 File Offset: 0x001BEDE4
	protected override void OnPrefabInit()
	{
		PlayerController.Instance = this;
		InterfaceTool.InitializeConfigs(this.defaultConfigKey, this.interfaceConfigs);
		this.vim = global::UnityEngine.Object.FindObjectOfType<VirtualInputModule>(true);
		for (int i = 0; i < this.tools.Length; i++)
		{
			GameObject gameObject = Util.KInstantiate(this.tools[i].gameObject, base.gameObject, null);
			this.tools[i] = gameObject.GetComponent<InterfaceTool>();
			this.tools[i].gameObject.SetActive(true);
			this.tools[i].gameObject.SetActive(false);
		}
	}

	// Token: 0x06004D7D RID: 19837 RVA: 0x001C0C75 File Offset: 0x001BEE75
	protected override void OnSpawn()
	{
		if (this.tools.Length == 0)
		{
			return;
		}
		this.ActivateTool(this.tools[0]);
	}

	// Token: 0x06004D7E RID: 19838 RVA: 0x001C0C8F File Offset: 0x001BEE8F
	private void InitializeConfigs()
	{
	}

	// Token: 0x06004D7F RID: 19839 RVA: 0x001C0C91 File Offset: 0x001BEE91
	private Vector3 GetCursorPos()
	{
		return PlayerController.GetCursorPos(KInputManager.GetMousePos());
	}

	// Token: 0x06004D80 RID: 19840 RVA: 0x001C0CA0 File Offset: 0x001BEEA0
	public static Vector3 GetCursorPos(Vector3 mouse_pos)
	{
		RaycastHit raycastHit;
		Vector3 vector;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(mouse_pos), out raycastHit, float.PositiveInfinity, Game.BlockSelectionLayerMask))
		{
			vector = raycastHit.point;
		}
		else
		{
			mouse_pos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
			vector = Camera.main.ScreenToWorldPoint(mouse_pos);
		}
		float num = vector.x;
		float num2 = vector.y;
		num = Mathf.Max(num, 0f);
		num = Mathf.Min(num, Grid.WidthInMeters);
		num2 = Mathf.Max(num2, 0f);
		num2 = Mathf.Min(num2, Grid.HeightInMeters);
		vector.x = num;
		vector.y = num2;
		return vector;
	}

	// Token: 0x06004D81 RID: 19841 RVA: 0x001C0D54 File Offset: 0x001BEF54
	private void UpdateHover()
	{
		global::UnityEngine.EventSystems.EventSystem current = global::UnityEngine.EventSystems.EventSystem.current;
		if (current != null)
		{
			this.activeTool.OnFocus(!current.IsPointerOverGameObject());
		}
	}

	// Token: 0x06004D82 RID: 19842 RVA: 0x001C0D84 File Offset: 0x001BEF84
	private void Update()
	{
		this.UpdateDrag();
		if (this.activeTool && this.activeTool.enabled)
		{
			this.UpdateHover();
			Vector3 cursorPos = this.GetCursorPos();
			if (cursorPos != this.prevMousePos)
			{
				this.prevMousePos = cursorPos;
				this.activeTool.OnMouseMove(cursorPos);
			}
		}
		if (Input.GetKeyDown(KeyCode.F12) && (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)))
		{
			this.DebugHidingCursor = !this.DebugHidingCursor;
			Cursor.visible = !this.DebugHidingCursor;
			HoverTextScreen.Instance.Show(!this.DebugHidingCursor);
		}
	}

	// Token: 0x06004D83 RID: 19843 RVA: 0x001C0E33 File Offset: 0x001BF033
	private void OnCleanup()
	{
		Global.GetInputManager().usedMenus.Remove(this);
	}

	// Token: 0x06004D84 RID: 19844 RVA: 0x001C0E46 File Offset: 0x001BF046
	private void LateUpdate()
	{
		if (this.queueStopDrag)
		{
			this.queueStopDrag = false;
			this.dragging = false;
			this.dragAction = global::Action.Invalid;
			this.dragDelta = Vector3.zero;
			this.worldDragDelta = Vector3.zero;
		}
	}

	// Token: 0x06004D85 RID: 19845 RVA: 0x001C0E7C File Offset: 0x001BF07C
	public void ActivateTool(InterfaceTool tool)
	{
		if (this.activeTool == tool)
		{
			return;
		}
		this.DeactivateTool(tool);
		this.activeTool = tool;
		this.activeTool.enabled = true;
		this.activeTool.gameObject.SetActive(true);
		this.activeTool.ActivateTool();
		this.UpdateHover();
	}

	// Token: 0x06004D86 RID: 19846 RVA: 0x001C0ED4 File Offset: 0x001BF0D4
	public void ToolDeactivated(InterfaceTool tool)
	{
		if (this.activeTool == tool && this.activeTool != null)
		{
			this.DeactivateTool(null);
		}
		if (this.activeTool == null)
		{
			this.ActivateTool(SelectTool.Instance);
		}
	}

	// Token: 0x06004D87 RID: 19847 RVA: 0x001C0F12 File Offset: 0x001BF112
	private void DeactivateTool(InterfaceTool new_tool = null)
	{
		if (this.activeTool != null)
		{
			this.activeTool.enabled = false;
			this.activeTool.gameObject.SetActive(false);
			InterfaceTool interfaceTool = this.activeTool;
			this.activeTool = null;
			interfaceTool.DeactivateTool(new_tool);
		}
	}

	// Token: 0x06004D88 RID: 19848 RVA: 0x001C0F52 File Offset: 0x001BF152
	public bool IsUsingDefaultTool()
	{
		return this.tools.Length != 0 && this.activeTool == this.tools[0];
	}

	// Token: 0x06004D89 RID: 19849 RVA: 0x001C0F72 File Offset: 0x001BF172
	private void StartDrag(global::Action action)
	{
		if (this.dragAction == global::Action.Invalid)
		{
			this.dragAction = action;
			this.startDragPos = KInputManager.GetMousePos();
			this.startDragTime = Time.unscaledTime;
		}
	}

	// Token: 0x06004D8A RID: 19850 RVA: 0x001C0F9C File Offset: 0x001BF19C
	private void UpdateDrag()
	{
		this.dragDelta = Vector2.zero;
		Vector3 mousePos = KInputManager.GetMousePos();
		if (!this.dragging && this.CanDrag() && ((mousePos - this.startDragPos).sqrMagnitude > 36f || Time.unscaledTime - this.startDragTime > 0.3f))
		{
			this.dragging = true;
		}
		if (DistributionPlatform.Initialized && KInputManager.currentControllerIsGamepad && this.dragging)
		{
			return;
		}
		if (this.dragging)
		{
			this.dragDelta = mousePos - this.startDragPos;
			this.worldDragDelta = Camera.main.ScreenToWorldPoint(mousePos) - Camera.main.ScreenToWorldPoint(this.startDragPos);
			this.startDragPos = mousePos;
		}
	}

	// Token: 0x06004D8B RID: 19851 RVA: 0x001C1062 File Offset: 0x001BF262
	private void StopDrag(global::Action action)
	{
		if (this.dragAction == action)
		{
			this.queueStopDrag = true;
			if (KInputManager.currentControllerIsGamepad)
			{
				this.dragging = false;
			}
		}
	}

	// Token: 0x06004D8C RID: 19852 RVA: 0x001C1084 File Offset: 0x001BF284
	public void CancelDragging()
	{
		this.queueStopDrag = true;
		if (this.activeTool != null)
		{
			DragTool dragTool = this.activeTool as DragTool;
			if (dragTool != null)
			{
				dragTool.CancelDragging();
			}
		}
	}

	// Token: 0x06004D8D RID: 19853 RVA: 0x001C10C1 File Offset: 0x001BF2C1
	public void OnCancelInput()
	{
		this.CancelDragging();
	}

	// Token: 0x06004D8E RID: 19854 RVA: 0x001C10CC File Offset: 0x001BF2CC
	public void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.ToggleScreenshotMode))
		{
			DebugHandler.ToggleScreenshotMode();
			return;
		}
		if (DebugHandler.HideUI && e.TryConsume(global::Action.Escape))
		{
			DebugHandler.ToggleScreenshotMode();
			return;
		}
		bool flag = true;
		if (e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.ShiftMouseLeft))
		{
			this.StartDrag(global::Action.MouseLeft);
		}
		else if (e.IsAction(global::Action.MouseRight))
		{
			this.StartDrag(global::Action.MouseRight);
		}
		else if (e.IsAction(global::Action.MouseMiddle))
		{
			this.StartDrag(global::Action.MouseMiddle);
		}
		else
		{
			flag = false;
		}
		if (this.activeTool == null || !this.activeTool.enabled)
		{
			return;
		}
		List<RaycastResult> list = new List<RaycastResult>();
		PointerEventData pointerEventData = new PointerEventData(global::UnityEngine.EventSystems.EventSystem.current);
		pointerEventData.position = KInputManager.GetMousePos();
		global::UnityEngine.EventSystems.EventSystem current = global::UnityEngine.EventSystems.EventSystem.current;
		if (current != null)
		{
			current.RaycastAll(pointerEventData, list);
			if (list.Count > 0)
			{
				return;
			}
		}
		if (flag && !this.draggingAllowed)
		{
			e.TryConsume(e.GetAction());
			return;
		}
		if (e.TryConsume(global::Action.MouseLeft) || e.TryConsume(global::Action.ShiftMouseLeft))
		{
			this.activeTool.OnLeftClickDown(this.GetCursorPos());
			return;
		}
		if (e.IsAction(global::Action.MouseRight))
		{
			this.activeTool.OnRightClickDown(this.GetCursorPos(), e);
			return;
		}
		this.activeTool.OnKeyDown(e);
	}

	// Token: 0x06004D8F RID: 19855 RVA: 0x001C1208 File Offset: 0x001BF408
	public void OnKeyUp(KButtonEvent e)
	{
		bool flag = true;
		if (e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.ShiftMouseLeft))
		{
			this.StopDrag(global::Action.MouseLeft);
		}
		else if (e.IsAction(global::Action.MouseRight))
		{
			this.StopDrag(global::Action.MouseRight);
		}
		else if (e.IsAction(global::Action.MouseMiddle))
		{
			this.StopDrag(global::Action.MouseMiddle);
		}
		else
		{
			flag = false;
		}
		if (this.activeTool == null || !this.activeTool.enabled)
		{
			return;
		}
		if (!this.activeTool.hasFocus)
		{
			return;
		}
		if (flag && !this.draggingAllowed)
		{
			e.TryConsume(e.GetAction());
			return;
		}
		if (!KInputManager.currentControllerIsGamepad)
		{
			if (e.TryConsume(global::Action.MouseLeft) || e.TryConsume(global::Action.ShiftMouseLeft))
			{
				this.activeTool.OnLeftClickUp(this.GetCursorPos());
				return;
			}
			if (e.IsAction(global::Action.MouseRight))
			{
				this.activeTool.OnRightClickUp(this.GetCursorPos());
				return;
			}
			this.activeTool.OnKeyUp(e);
			return;
		}
		else
		{
			if (e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.ShiftMouseLeft))
			{
				this.activeTool.OnLeftClickUp(this.GetCursorPos());
				return;
			}
			if (e.IsAction(global::Action.MouseRight))
			{
				this.activeTool.OnRightClickUp(this.GetCursorPos());
				return;
			}
			this.activeTool.OnKeyUp(e);
			return;
		}
	}

	// Token: 0x06004D90 RID: 19856 RVA: 0x001C1339 File Offset: 0x001BF539
	public bool ConsumeIfNotDragging(KButtonEvent e, global::Action action)
	{
		return (this.dragAction != action || !this.dragging) && e.TryConsume(action);
	}

	// Token: 0x06004D91 RID: 19857 RVA: 0x001C1355 File Offset: 0x001BF555
	public bool IsDragging()
	{
		return this.dragging && this.CanDrag();
	}

	// Token: 0x06004D92 RID: 19858 RVA: 0x001C1367 File Offset: 0x001BF567
	public bool CanDrag()
	{
		return this.draggingAllowed && this.dragAction > global::Action.Invalid;
	}

	// Token: 0x06004D93 RID: 19859 RVA: 0x001C137C File Offset: 0x001BF57C
	public void AllowDragging(bool allow)
	{
		this.draggingAllowed = allow;
	}

	// Token: 0x06004D94 RID: 19860 RVA: 0x001C1385 File Offset: 0x001BF585
	public Vector3 GetDragDelta()
	{
		return this.dragDelta;
	}

	// Token: 0x06004D95 RID: 19861 RVA: 0x001C138D File Offset: 0x001BF58D
	public Vector3 GetWorldDragDelta()
	{
		if (!this.draggingAllowed)
		{
			return Vector3.zero;
		}
		return this.worldDragDelta;
	}

	// Token: 0x04003384 RID: 13188
	[SerializeField]
	private global::Action defaultConfigKey;

	// Token: 0x04003385 RID: 13189
	[SerializeField]
	private List<InterfaceToolConfig> interfaceConfigs;

	// Token: 0x04003387 RID: 13191
	public InterfaceTool[] tools;

	// Token: 0x04003388 RID: 13192
	private InterfaceTool activeTool;

	// Token: 0x04003389 RID: 13193
	public VirtualInputModule vim;

	// Token: 0x0400338B RID: 13195
	private bool DebugHidingCursor;

	// Token: 0x0400338C RID: 13196
	private Vector3 prevMousePos = new Vector3(float.PositiveInfinity, 0f, 0f);

	// Token: 0x0400338D RID: 13197
	private const float MIN_DRAG_DIST_SQR = 36f;

	// Token: 0x0400338E RID: 13198
	private const float MIN_DRAG_TIME = 0.3f;

	// Token: 0x0400338F RID: 13199
	private global::Action dragAction;

	// Token: 0x04003390 RID: 13200
	private bool draggingAllowed = true;

	// Token: 0x04003391 RID: 13201
	private bool dragging;

	// Token: 0x04003392 RID: 13202
	private bool queueStopDrag;

	// Token: 0x04003393 RID: 13203
	private Vector3 startDragPos;

	// Token: 0x04003394 RID: 13204
	private float startDragTime;

	// Token: 0x04003395 RID: 13205
	private Vector3 dragDelta;

	// Token: 0x04003396 RID: 13206
	private Vector3 worldDragDelta;
}
