using System;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000EAE RID: 3758
	[AddComponentMenu("Event/Virtual Input Module")]
	public class VirtualInputModule : PointerInputModule, IInputHandler
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06007837 RID: 30775 RVA: 0x002E990B File Offset: 0x002E7B0B
		public string handlerName
		{
			get
			{
				return "VirtualCursorInput";
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06007838 RID: 30776 RVA: 0x002E9912 File Offset: 0x002E7B12
		// (set) Token: 0x06007839 RID: 30777 RVA: 0x002E991A File Offset: 0x002E7B1A
		public KInputHandler inputHandler { get; set; }

		// Token: 0x0600783A RID: 30778 RVA: 0x002E9924 File Offset: 0x002E7B24
		protected VirtualInputModule()
		{
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600783B RID: 30779 RVA: 0x002E999A File Offset: 0x002E7B9A
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public VirtualInputModule.InputMode inputMode
		{
			get
			{
				return VirtualInputModule.InputMode.Mouse;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600783C RID: 30780 RVA: 0x002E999D File Offset: 0x002E7B9D
		// (set) Token: 0x0600783D RID: 30781 RVA: 0x002E99A5 File Offset: 0x002E7BA5
		[Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead (UnityUpgradable) -> forceModuleActive")]
		public bool allowActivationOnMobileDevice
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x0600783E RID: 30782 RVA: 0x002E99AE File Offset: 0x002E7BAE
		// (set) Token: 0x0600783F RID: 30783 RVA: 0x002E99B6 File Offset: 0x002E7BB6
		public bool forceModuleActive
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06007840 RID: 30784 RVA: 0x002E99BF File Offset: 0x002E7BBF
		// (set) Token: 0x06007841 RID: 30785 RVA: 0x002E99C7 File Offset: 0x002E7BC7
		public float inputActionsPerSecond
		{
			get
			{
				return this.m_InputActionsPerSecond;
			}
			set
			{
				this.m_InputActionsPerSecond = value;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06007842 RID: 30786 RVA: 0x002E99D0 File Offset: 0x002E7BD0
		// (set) Token: 0x06007843 RID: 30787 RVA: 0x002E99D8 File Offset: 0x002E7BD8
		public float repeatDelay
		{
			get
			{
				return this.m_RepeatDelay;
			}
			set
			{
				this.m_RepeatDelay = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06007844 RID: 30788 RVA: 0x002E99E1 File Offset: 0x002E7BE1
		// (set) Token: 0x06007845 RID: 30789 RVA: 0x002E99E9 File Offset: 0x002E7BE9
		public string horizontalAxis
		{
			get
			{
				return this.m_HorizontalAxis;
			}
			set
			{
				this.m_HorizontalAxis = value;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06007846 RID: 30790 RVA: 0x002E99F2 File Offset: 0x002E7BF2
		// (set) Token: 0x06007847 RID: 30791 RVA: 0x002E99FA File Offset: 0x002E7BFA
		public string verticalAxis
		{
			get
			{
				return this.m_VerticalAxis;
			}
			set
			{
				this.m_VerticalAxis = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06007848 RID: 30792 RVA: 0x002E9A03 File Offset: 0x002E7C03
		// (set) Token: 0x06007849 RID: 30793 RVA: 0x002E9A0B File Offset: 0x002E7C0B
		public string submitButton
		{
			get
			{
				return this.m_SubmitButton;
			}
			set
			{
				this.m_SubmitButton = value;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600784A RID: 30794 RVA: 0x002E9A14 File Offset: 0x002E7C14
		// (set) Token: 0x0600784B RID: 30795 RVA: 0x002E9A1C File Offset: 0x002E7C1C
		public string cancelButton
		{
			get
			{
				return this.m_CancelButton;
			}
			set
			{
				this.m_CancelButton = value;
			}
		}

		// Token: 0x0600784C RID: 30796 RVA: 0x002E9A25 File Offset: 0x002E7C25
		public void SetCursor(Texture2D tex)
		{
			this.UpdateModule();
			if (this.m_VirtualCursor)
			{
				this.m_VirtualCursor.GetComponent<RawImage>().texture = tex;
			}
		}

		// Token: 0x0600784D RID: 30797 RVA: 0x002E9A4C File Offset: 0x002E7C4C
		public override void UpdateModule()
		{
			GameInputManager inputManager = Global.GetInputManager();
			if (inputManager.GetControllerCount() <= 1)
			{
				return;
			}
			if (this.inputHandler == null || !this.inputHandler.UsesController(this, inputManager.GetController(1)))
			{
				KInputHandler.Add(inputManager.GetController(1), this, int.MaxValue);
				if (!inputManager.usedMenus.Contains(this))
				{
					inputManager.usedMenus.Add(this);
				}
				this.debugName = SceneManager.GetActiveScene().name + "-VirtualInputModule";
			}
			if (this.m_VirtualCursor == null)
			{
				this.m_VirtualCursor = GameObject.Find("VirtualCursor").GetComponent<RectTransform>();
			}
			if (this.m_canvasCamera == null)
			{
				this.m_canvasCamera = base.gameObject.AddComponent<Camera>();
				this.m_canvasCamera.enabled = false;
			}
			if (CameraController.Instance != null)
			{
				this.m_canvasCamera.CopyFrom(CameraController.Instance.overlayCamera);
			}
			else if (this.CursorCanvasShouldBeOverlay)
			{
				this.m_canvasCamera.CopyFrom(GameObject.Find("FrontEndCamera").GetComponent<Camera>());
			}
			if (this.m_canvasCamera != null && this.VCcam == null)
			{
				this.VCcam = GameObject.Find("VirtualCursorCamera").GetComponent<Camera>();
				if (this.VCcam != null)
				{
					if (this.m_virtualCursorCanvas == null)
					{
						this.m_virtualCursorCanvas = GameObject.Find("VirtualCursorCanvas").GetComponent<Canvas>();
						this.m_virtualCursorScaler = this.m_virtualCursorCanvas.GetComponent<CanvasScaler>();
					}
					if (this.CursorCanvasShouldBeOverlay)
					{
						this.m_virtualCursorCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
						this.VCcam.orthographic = false;
					}
					else
					{
						this.VCcam.orthographic = this.m_canvasCamera.orthographic;
						this.VCcam.orthographicSize = this.m_canvasCamera.orthographicSize;
						this.VCcam.transform.position = this.m_canvasCamera.transform.position;
						this.VCcam.enabled = true;
						this.m_virtualCursorCanvas.renderMode = RenderMode.ScreenSpaceCamera;
						this.m_virtualCursorCanvas.worldCamera = this.VCcam;
					}
				}
			}
			if (this.m_canvasCamera != null && this.VCcam != null)
			{
				this.VCcam.orthographic = this.m_canvasCamera.orthographic;
				this.VCcam.orthographicSize = this.m_canvasCamera.orthographicSize;
				this.VCcam.transform.position = this.m_canvasCamera.transform.position;
				this.VCcam.aspect = this.m_canvasCamera.aspect;
				this.VCcam.enabled = true;
			}
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			if (this.m_virtualCursorScaler != null && this.m_virtualCursorScaler.referenceResolution != vector)
			{
				this.m_virtualCursorScaler.referenceResolution = vector;
			}
			this.m_LastMousePosition = this.m_MousePosition;
			this.m_VirtualCursor.localScale = Vector2.one;
			Vector2 steamCursorMovement = KInputManager.steamInputInterpreter.GetSteamCursorMovement();
			float num = 1f / (4500f / vector.x);
			steamCursorMovement.x *= num;
			steamCursorMovement.y *= num;
			this.m_VirtualCursor.anchoredPosition += steamCursorMovement * this.m_VirtualCursorSpeed;
			this.m_VirtualCursor.anchoredPosition = new Vector2(Mathf.Clamp(this.m_VirtualCursor.anchoredPosition.x, 0f, vector.x), Mathf.Clamp(this.m_VirtualCursor.anchoredPosition.y, 0f, vector.y));
			KInputManager.virtualCursorPos = new Vector3F(this.m_VirtualCursor.anchoredPosition.x, this.m_VirtualCursor.anchoredPosition.y, 0f);
			this.m_MousePosition = this.m_VirtualCursor.anchoredPosition;
		}

		// Token: 0x0600784E RID: 30798 RVA: 0x002E9E4A File Offset: 0x002E804A
		public override bool IsModuleSupported()
		{
			return this.m_ForceModuleActive || Input.mousePresent;
		}

		// Token: 0x0600784F RID: 30799 RVA: 0x002E9E5C File Offset: 0x002E805C
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			if (KInputManager.currentControllerIsGamepad)
			{
				return true;
			}
			bool forceModuleActive = this.m_ForceModuleActive;
			Input.GetButtonDown(this.m_SubmitButton);
			return forceModuleActive | Input.GetButtonDown(this.m_CancelButton) | !Mathf.Approximately(Input.GetAxisRaw(this.m_HorizontalAxis), 0f) | !Mathf.Approximately(Input.GetAxisRaw(this.m_VerticalAxis), 0f) | ((this.m_MousePosition - this.m_LastMousePosition).sqrMagnitude > 0f) | Input.GetMouseButtonDown(0);
		}

		// Token: 0x06007850 RID: 30800 RVA: 0x002E9EF4 File Offset: 0x002E80F4
		public override void ActivateModule()
		{
			base.ActivateModule();
			if (this.m_canvasCamera == null)
			{
				this.m_canvasCamera = base.gameObject.AddComponent<Camera>();
				this.m_canvasCamera.enabled = false;
			}
			if (Input.mousePosition.x > 0f && Input.mousePosition.x < (float)Screen.width && Input.mousePosition.y > 0f && Input.mousePosition.y < (float)Screen.height)
			{
				this.m_VirtualCursor.anchoredPosition = Input.mousePosition;
			}
			else
			{
				this.m_VirtualCursor.anchoredPosition = new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2));
			}
			this.m_VirtualCursor.anchoredPosition = new Vector2(Mathf.Clamp(this.m_VirtualCursor.anchoredPosition.x, 0f, (float)Screen.width), Mathf.Clamp(this.m_VirtualCursor.anchoredPosition.y, 0f, (float)Screen.height));
			this.m_VirtualCursor.localScale = Vector2.zero;
			this.m_MousePosition = this.m_VirtualCursor.anchoredPosition;
			this.m_LastMousePosition = this.m_VirtualCursor.anchoredPosition;
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			if (this.m_VirtualCursor == null)
			{
				this.m_VirtualCursor = GameObject.Find("VirtualCursor").GetComponent<RectTransform>();
			}
			if (this.m_canvasCamera == null)
			{
				this.m_canvasCamera = GameObject.Find("FrontEndCamera").GetComponent<Camera>();
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x06007851 RID: 30801 RVA: 0x002EA0B0 File Offset: 0x002E82B0
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
			this.conButtonStates.affirmativeDown = false;
			this.conButtonStates.affirmativeHoldTime = 0f;
			this.conButtonStates.negativeDown = false;
			this.conButtonStates.negativeHoldTime = 0f;
		}

		// Token: 0x06007852 RID: 30802 RVA: 0x002EA104 File Offset: 0x002E8304
		public override void Process()
		{
			bool flag = this.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= this.SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
			this.ProcessMouseEvent();
		}

		// Token: 0x06007853 RID: 30803 RVA: 0x002EA144 File Offset: 0x002E8344
		protected bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			if (Input.GetButtonDown(this.m_SubmitButton))
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
			}
			if (Input.GetButtonDown(this.m_CancelButton))
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		// Token: 0x06007854 RID: 30804 RVA: 0x002EA1BC File Offset: 0x002E83BC
		private Vector2 GetRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			zero.x = Input.GetAxisRaw(this.m_HorizontalAxis);
			zero.y = Input.GetAxisRaw(this.m_VerticalAxis);
			if (Input.GetButtonDown(this.m_HorizontalAxis))
			{
				if (zero.x < 0f)
				{
					zero.x = -1f;
				}
				if (zero.x > 0f)
				{
					zero.x = 1f;
				}
			}
			if (Input.GetButtonDown(this.m_VerticalAxis))
			{
				if (zero.y < 0f)
				{
					zero.y = -1f;
				}
				if (zero.y > 0f)
				{
					zero.y = 1f;
				}
			}
			return zero;
		}

		// Token: 0x06007855 RID: 30805 RVA: 0x002EA274 File Offset: 0x002E8474
		protected bool SendMoveEventToSelectedObject()
		{
			float unscaledTime = Time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			if (Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f))
			{
				this.m_ConsecutiveMoveCount = 0;
				return false;
			}
			bool flag = Input.GetButtonDown(this.m_HorizontalAxis) || Input.GetButtonDown(this.m_VerticalAxis);
			bool flag2 = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
			if (!flag)
			{
				if (flag2 && this.m_ConsecutiveMoveCount == 1)
				{
					flag = unscaledTime > this.m_PrevActionTime + this.m_RepeatDelay;
				}
				else
				{
					flag = unscaledTime > this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond;
				}
			}
			if (!flag)
			{
				return false;
			}
			AxisEventData axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
			ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
			if (!flag2)
			{
				this.m_ConsecutiveMoveCount = 0;
			}
			this.m_ConsecutiveMoveCount++;
			this.m_PrevActionTime = unscaledTime;
			this.m_LastMoveVector = rawMoveVector;
			return axisEventData.used;
		}

		// Token: 0x06007856 RID: 30806 RVA: 0x002EA387 File Offset: 0x002E8587
		protected void ProcessMouseEvent()
		{
			this.ProcessMouseEvent(0);
		}

		// Token: 0x06007857 RID: 30807 RVA: 0x002EA390 File Offset: 0x002E8590
		protected void ProcessMouseEvent(int id)
		{
			if (this.mouseMovementOnly)
			{
				return;
			}
			PointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(id);
			PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
			this.m_CurrentFocusedGameObject = eventData.buttonData.pointerCurrentRaycast.gameObject;
			this.ProcessControllerPress(eventData, true);
			this.ProcessControllerPress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData, false);
			this.ProcessMove(eventData.buttonData);
			this.ProcessDrag(eventData.buttonData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
			if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject), eventData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06007858 RID: 30808 RVA: 0x002EA480 File Offset: 0x002E8680
		protected bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		// Token: 0x06007859 RID: 30809 RVA: 0x002EA4C8 File Offset: 0x002E86C8
		protected void ProcessMousePress(PointerInputModule.MouseButtonEventData data)
		{
			PointerEventData buttonData = data.buttonData;
			GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				buttonData.eligibleForClick = true;
				buttonData.delta = Vector2.zero;
				buttonData.dragging = false;
				buttonData.useDragThreshold = true;
				buttonData.pressPosition = buttonData.position;
				buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
				buttonData.position = this.m_VirtualCursor.anchoredPosition;
				base.DeselectIfSelectionChanged(gameObject, buttonData);
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == buttonData.lastPress)
				{
					if (unscaledTime - buttonData.clickTime < 0.3f)
					{
						PointerEventData pointerEventData = buttonData;
						int num = pointerEventData.clickCount + 1;
						pointerEventData.clickCount = num;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.clickTime = unscaledTime;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.pointerPress = gameObject2;
				buttonData.rawPointerPress = gameObject;
				buttonData.clickTime = unscaledTime;
				buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (data.ReleasedThisFrame())
			{
				ExecuteEvents.Execute<IPointerUpHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
				}
				else if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, buttonData, ExecuteEvents.dropHandler);
				}
				buttonData.eligibleForClick = false;
				buttonData.pointerPress = null;
				buttonData.rawPointerPress = null;
				if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
				}
				buttonData.dragging = false;
				buttonData.pointerDrag = null;
				if (gameObject != buttonData.pointerEnter)
				{
					base.HandlePointerExitAndEnter(buttonData, null);
					base.HandlePointerExitAndEnter(buttonData, gameObject);
				}
			}
		}

		// Token: 0x0600785A RID: 30810 RVA: 0x002EA6D4 File Offset: 0x002E88D4
		public void OnKeyDown(KButtonEvent e)
		{
			if (KInputManager.currentControllerIsGamepad)
			{
				if (e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.ShiftMouseLeft))
				{
					if (this.conButtonStates.affirmativeDown)
					{
						this.conButtonStates.affirmativeHoldTime = this.conButtonStates.affirmativeHoldTime + Time.unscaledDeltaTime;
					}
					if (!this.conButtonStates.affirmativeDown)
					{
						this.leftFirstClick = true;
						this.leftReleased = false;
					}
					this.conButtonStates.affirmativeDown = true;
					return;
				}
				if (e.IsAction(global::Action.MouseRight))
				{
					if (this.conButtonStates.negativeDown)
					{
						this.conButtonStates.negativeHoldTime = this.conButtonStates.negativeHoldTime + Time.unscaledDeltaTime;
					}
					if (!this.conButtonStates.negativeDown)
					{
						this.rightFirstClick = true;
						this.rightReleased = false;
					}
					this.conButtonStates.negativeDown = true;
				}
			}
		}

		// Token: 0x0600785B RID: 30811 RVA: 0x002EA798 File Offset: 0x002E8998
		public void OnKeyUp(KButtonEvent e)
		{
			if (KInputManager.currentControllerIsGamepad)
			{
				if (e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.ShiftMouseLeft))
				{
					this.conButtonStates.affirmativeHoldTime = 0f;
					this.leftReleased = true;
					this.leftFirstClick = false;
					this.conButtonStates.affirmativeDown = false;
					return;
				}
				if (e.IsAction(global::Action.MouseRight))
				{
					this.conButtonStates.negativeHoldTime = 0f;
					this.rightReleased = true;
					this.rightFirstClick = false;
					this.conButtonStates.negativeDown = false;
				}
			}
		}

		// Token: 0x0600785C RID: 30812 RVA: 0x002EA81C File Offset: 0x002E8A1C
		protected void ProcessControllerPress(PointerInputModule.MouseButtonEventData data, bool leftClick)
		{
			if (this.leftClickData == null)
			{
				this.leftClickData = data.buttonData;
			}
			if (this.rightClickData == null)
			{
				this.rightClickData = data.buttonData;
			}
			if (leftClick)
			{
				PointerEventData buttonData = data.buttonData;
				GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
				buttonData.position = this.m_VirtualCursor.anchoredPosition;
				if (this.leftFirstClick)
				{
					buttonData.button = PointerEventData.InputButton.Left;
					buttonData.eligibleForClick = true;
					buttonData.delta = Vector2.zero;
					buttonData.dragging = false;
					buttonData.useDragThreshold = true;
					buttonData.pressPosition = buttonData.position;
					buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
					buttonData.position = new Vector2(KInputManager.virtualCursorPos.x, KInputManager.virtualCursorPos.y);
					base.DeselectIfSelectionChanged(gameObject, buttonData);
					GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
					if (gameObject2 == null)
					{
						gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
					}
					float unscaledTime = Time.unscaledTime;
					if (gameObject2 == buttonData.lastPress)
					{
						if (unscaledTime - buttonData.clickTime < 0.3f)
						{
							PointerEventData pointerEventData = buttonData;
							int num = pointerEventData.clickCount + 1;
							pointerEventData.clickCount = num;
						}
						else
						{
							buttonData.clickCount = 1;
						}
						buttonData.clickTime = unscaledTime;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.pointerPress = gameObject2;
					buttonData.rawPointerPress = gameObject;
					buttonData.clickTime = unscaledTime;
					buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
					if (buttonData.pointerDrag != null)
					{
						ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
					}
					this.leftFirstClick = false;
					return;
				}
				if (this.leftReleased)
				{
					buttonData.button = PointerEventData.InputButton.Left;
					ExecuteEvents.Execute<IPointerUpHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
					GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
					if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
					{
						ExecuteEvents.Execute<IPointerClickHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
					}
					else if (buttonData.pointerDrag != null && buttonData.dragging)
					{
						ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, buttonData, ExecuteEvents.dropHandler);
					}
					buttonData.eligibleForClick = false;
					buttonData.pointerPress = null;
					buttonData.rawPointerPress = null;
					if (buttonData.pointerDrag != null && buttonData.dragging)
					{
						ExecuteEvents.Execute<IEndDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
					}
					buttonData.dragging = false;
					buttonData.pointerDrag = null;
					if (gameObject != buttonData.pointerEnter)
					{
						base.HandlePointerExitAndEnter(buttonData, null);
						base.HandlePointerExitAndEnter(buttonData, gameObject);
					}
					this.leftReleased = false;
					return;
				}
			}
			else
			{
				PointerEventData buttonData2 = data.buttonData;
				GameObject gameObject3 = buttonData2.pointerCurrentRaycast.gameObject;
				buttonData2.position = this.m_VirtualCursor.anchoredPosition;
				if (this.rightFirstClick)
				{
					buttonData2.button = PointerEventData.InputButton.Right;
					buttonData2.eligibleForClick = true;
					buttonData2.delta = Vector2.zero;
					buttonData2.dragging = false;
					buttonData2.useDragThreshold = true;
					buttonData2.pressPosition = buttonData2.position;
					buttonData2.pointerPressRaycast = buttonData2.pointerCurrentRaycast;
					buttonData2.position = this.m_VirtualCursor.anchoredPosition;
					base.DeselectIfSelectionChanged(gameObject3, buttonData2);
					GameObject gameObject4 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject3, buttonData2, ExecuteEvents.pointerDownHandler);
					if (gameObject4 == null)
					{
						gameObject4 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject3);
					}
					float unscaledTime2 = Time.unscaledTime;
					if (gameObject4 == buttonData2.lastPress)
					{
						if (unscaledTime2 - buttonData2.clickTime < 0.3f)
						{
							PointerEventData pointerEventData2 = buttonData2;
							int num = pointerEventData2.clickCount + 1;
							pointerEventData2.clickCount = num;
						}
						else
						{
							buttonData2.clickCount = 1;
						}
						buttonData2.clickTime = unscaledTime2;
					}
					else
					{
						buttonData2.clickCount = 1;
					}
					buttonData2.pointerPress = gameObject4;
					buttonData2.rawPointerPress = gameObject3;
					buttonData2.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject3);
					if (buttonData2.pointerDrag != null)
					{
						ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData2.pointerDrag, buttonData2, ExecuteEvents.initializePotentialDrag);
					}
					this.rightFirstClick = false;
					return;
				}
				if (this.rightReleased)
				{
					buttonData2.button = PointerEventData.InputButton.Right;
					ExecuteEvents.Execute<IPointerUpHandler>(buttonData2.pointerPress, buttonData2, ExecuteEvents.pointerUpHandler);
					GameObject eventHandler2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject3);
					if (buttonData2.pointerPress == eventHandler2 && buttonData2.eligibleForClick)
					{
						ExecuteEvents.Execute<IPointerClickHandler>(buttonData2.pointerPress, buttonData2, ExecuteEvents.pointerClickHandler);
					}
					else if (buttonData2.pointerDrag != null && buttonData2.dragging)
					{
						ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject3, buttonData2, ExecuteEvents.dropHandler);
					}
					buttonData2.eligibleForClick = false;
					buttonData2.pointerPress = null;
					buttonData2.rawPointerPress = null;
					if (buttonData2.pointerDrag != null && buttonData2.dragging)
					{
						ExecuteEvents.Execute<IEndDragHandler>(buttonData2.pointerDrag, buttonData2, ExecuteEvents.endDragHandler);
					}
					buttonData2.dragging = false;
					buttonData2.pointerDrag = null;
					if (gameObject3 != buttonData2.pointerEnter)
					{
						base.HandlePointerExitAndEnter(buttonData2, null);
						base.HandlePointerExitAndEnter(buttonData2, gameObject3);
					}
					this.rightReleased = false;
					return;
				}
			}
		}

		// Token: 0x0600785D RID: 30813 RVA: 0x002EACF8 File Offset: 0x002E8EF8
		protected override PointerInputModule.MouseState GetMousePointerEventData(int id)
		{
			PointerEventData pointerEventData;
			bool pointerData = base.GetPointerData(-1, out pointerEventData, true);
			pointerEventData.Reset();
			Vector2 vector = RectTransformUtility.WorldToScreenPoint(this.m_canvasCamera, this.m_VirtualCursor.position);
			if (pointerData)
			{
				pointerEventData.position = vector;
			}
			Vector2 anchoredPosition = this.m_VirtualCursor.anchoredPosition;
			pointerEventData.delta = anchoredPosition - pointerEventData.position;
			pointerEventData.position = anchoredPosition;
			pointerEventData.scrollDelta = Input.mouseScrollDelta;
			pointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(pointerEventData, this.m_RaycastResultCache);
			RaycastResult raycastResult = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			pointerEventData.pointerCurrentRaycast = raycastResult;
			this.m_RaycastResultCache.Clear();
			PointerEventData pointerEventData2;
			base.GetPointerData(-2, out pointerEventData2, true);
			base.CopyFromTo(pointerEventData, pointerEventData2);
			pointerEventData2.button = PointerEventData.InputButton.Right;
			PointerEventData pointerEventData3;
			base.GetPointerData(-3, out pointerEventData3, true);
			base.CopyFromTo(pointerEventData, pointerEventData3);
			pointerEventData3.button = PointerEventData.InputButton.Middle;
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Left, base.StateForMouseButton(0), pointerEventData);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Right, base.StateForMouseButton(1), pointerEventData2);
			this.m_MouseState.SetButtonState(PointerEventData.InputButton.Middle, base.StateForMouseButton(2), pointerEventData3);
			return this.m_MouseState;
		}

		// Token: 0x0400538F RID: 21391
		private float m_PrevActionTime;

		// Token: 0x04005390 RID: 21392
		private Vector2 m_LastMoveVector;

		// Token: 0x04005391 RID: 21393
		private int m_ConsecutiveMoveCount;

		// Token: 0x04005392 RID: 21394
		private string debugName;

		// Token: 0x04005393 RID: 21395
		private Vector2 m_LastMousePosition;

		// Token: 0x04005394 RID: 21396
		private Vector2 m_MousePosition;

		// Token: 0x04005395 RID: 21397
		public bool mouseMovementOnly;

		// Token: 0x04005396 RID: 21398
		[SerializeField]
		private RectTransform m_VirtualCursor;

		// Token: 0x04005397 RID: 21399
		[SerializeField]
		private float m_VirtualCursorSpeed = 1f;

		// Token: 0x04005398 RID: 21400
		[SerializeField]
		private Vector2 m_VirtualCursorOffset = Vector2.zero;

		// Token: 0x04005399 RID: 21401
		[SerializeField]
		private Camera m_canvasCamera;

		// Token: 0x0400539A RID: 21402
		private Camera VCcam;

		// Token: 0x0400539B RID: 21403
		public bool CursorCanvasShouldBeOverlay;

		// Token: 0x0400539C RID: 21404
		private Canvas m_virtualCursorCanvas;

		// Token: 0x0400539D RID: 21405
		private CanvasScaler m_virtualCursorScaler;

		// Token: 0x0400539E RID: 21406
		private PointerEventData leftClickData;

		// Token: 0x0400539F RID: 21407
		private PointerEventData rightClickData;

		// Token: 0x040053A0 RID: 21408
		private VirtualInputModule.ControllerButtonStates conButtonStates;

		// Token: 0x040053A1 RID: 21409
		private GameObject m_CurrentFocusedGameObject;

		// Token: 0x040053A2 RID: 21410
		private bool leftReleased;

		// Token: 0x040053A3 RID: 21411
		private bool rightReleased;

		// Token: 0x040053A4 RID: 21412
		private bool leftFirstClick;

		// Token: 0x040053A5 RID: 21413
		private bool rightFirstClick;

		// Token: 0x040053A6 RID: 21414
		[SerializeField]
		private string m_HorizontalAxis = "Horizontal";

		// Token: 0x040053A7 RID: 21415
		[SerializeField]
		private string m_VerticalAxis = "Vertical";

		// Token: 0x040053A8 RID: 21416
		[SerializeField]
		private string m_SubmitButton = "Submit";

		// Token: 0x040053A9 RID: 21417
		[SerializeField]
		private string m_CancelButton = "Cancel";

		// Token: 0x040053AA RID: 21418
		[SerializeField]
		private float m_InputActionsPerSecond = 10f;

		// Token: 0x040053AB RID: 21419
		[SerializeField]
		private float m_RepeatDelay = 0.5f;

		// Token: 0x040053AC RID: 21420
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
		private bool m_ForceModuleActive;

		// Token: 0x040053AD RID: 21421
		private readonly PointerInputModule.MouseState m_MouseState = new PointerInputModule.MouseState();

		// Token: 0x020020C6 RID: 8390
		[Obsolete("Mode is no longer needed on input module as it handles both mouse and keyboard simultaneously.", false)]
		public enum InputMode
		{
			// Token: 0x04009546 RID: 38214
			Mouse,
			// Token: 0x04009547 RID: 38215
			Buttons
		}

		// Token: 0x020020C7 RID: 8391
		private struct ControllerButtonStates
		{
			// Token: 0x04009548 RID: 38216
			public bool affirmativeDown;

			// Token: 0x04009549 RID: 38217
			public float affirmativeHoldTime;

			// Token: 0x0400954A RID: 38218
			public bool negativeDown;

			// Token: 0x0400954B RID: 38219
			public float negativeHoldTime;
		}
	}
}
