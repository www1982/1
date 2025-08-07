using System;
using System.Collections.Generic;
using System.Linq;
using Klei.Input;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000978 RID: 2424
[AddComponentMenu("KMonoBehaviour/scripts/InterfaceTool")]
public class InterfaceTool : KMonoBehaviour
{
	// Token: 0x170004F4 RID: 1268
	// (get) Token: 0x060045FC RID: 17916 RVA: 0x00192F2A File Offset: 0x0019112A
	public static InterfaceToolConfig ActiveConfig
	{
		get
		{
			if (InterfaceTool.interfaceConfigMap == null)
			{
				InterfaceTool.InitializeConfigs(global::Action.Invalid, null);
			}
			return InterfaceTool.activeConfigs[InterfaceTool.activeConfigs.Count - 1];
		}
	}

	// Token: 0x060045FD RID: 17917 RVA: 0x00192F50 File Offset: 0x00191150
	public static void ToggleConfig(global::Action configKey)
	{
		if (InterfaceTool.interfaceConfigMap == null)
		{
			InterfaceTool.InitializeConfigs(global::Action.Invalid, null);
		}
		InterfaceToolConfig interfaceToolConfig;
		if (!InterfaceTool.interfaceConfigMap.TryGetValue(configKey, out interfaceToolConfig))
		{
			global::Debug.LogWarning(string.Format("[InterfaceTool] No config is associated with Key: {0}!", configKey) + " Are you sure the configs were initialized properly!");
			return;
		}
		if (InterfaceTool.activeConfigs.BinarySearch(interfaceToolConfig, InterfaceToolConfig.ConfigComparer) <= 0)
		{
			global::Debug.Log(string.Format("[InterfaceTool] Pushing config with key: {0}", configKey));
			InterfaceTool.activeConfigs.Add(interfaceToolConfig);
			InterfaceTool.activeConfigs.Sort(InterfaceToolConfig.ConfigComparer);
			return;
		}
		global::Debug.Log(string.Format("[InterfaceTool] Popping config with key: {0}", configKey));
		InterfaceTool.activeConfigs.Remove(interfaceToolConfig);
	}

	// Token: 0x060045FE RID: 17918 RVA: 0x00193000 File Offset: 0x00191200
	public static void InitializeConfigs(global::Action defaultKey, List<InterfaceToolConfig> configs)
	{
		string text = ((configs == null) ? "null" : configs.Count.ToString());
		global::Debug.Log(string.Format("[InterfaceTool] Initializing configs with values of DefaultKey: {0} Configs: {1}", defaultKey, text));
		if (configs == null || configs.Count == 0)
		{
			InterfaceToolConfig interfaceToolConfig = ScriptableObject.CreateInstance<InterfaceToolConfig>();
			InterfaceTool.interfaceConfigMap = new Dictionary<global::Action, InterfaceToolConfig>();
			InterfaceTool.interfaceConfigMap[interfaceToolConfig.InputAction] = interfaceToolConfig;
			return;
		}
		InterfaceTool.interfaceConfigMap = configs.ToDictionary((InterfaceToolConfig x) => x.InputAction);
		InterfaceTool.ToggleConfig(defaultKey);
	}

	// Token: 0x170004F5 RID: 1269
	// (get) Token: 0x060045FF RID: 17919 RVA: 0x00193099 File Offset: 0x00191299
	public HashedString ViewMode
	{
		get
		{
			return this.viewMode;
		}
	}

	// Token: 0x06004600 RID: 17920 RVA: 0x001930A1 File Offset: 0x001912A1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.hoverTextConfiguration = base.GetComponent<HoverTextConfiguration>();
	}

	// Token: 0x06004601 RID: 17921 RVA: 0x001930B5 File Offset: 0x001912B5
	public void ActivateTool()
	{
		this.OnActivateTool();
		this.OnMouseMove(PlayerController.GetCursorPos(KInputManager.GetMousePos()));
		Game.Instance.Trigger(1174281782, this);
	}

	// Token: 0x06004602 RID: 17922 RVA: 0x001930E0 File Offset: 0x001912E0
	public virtual bool ShowHoverUI()
	{
		if (ManagementMenu.Instance == null || ManagementMenu.Instance.IsFullscreenUIActive())
		{
			return false;
		}
		Vector3 vector = Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos());
		if (OverlayScreen.Instance == null || !ClusterManager.Instance.IsPositionInActiveWorld(vector) || vector.x < 0f || vector.x > Grid.WidthInMeters || vector.y < 0f || vector.y > Grid.HeightInMeters)
		{
			return false;
		}
		global::UnityEngine.EventSystems.EventSystem current = global::UnityEngine.EventSystems.EventSystem.current;
		return current != null && !current.IsPointerOverGameObject();
	}

	// Token: 0x06004603 RID: 17923 RVA: 0x00193184 File Offset: 0x00191384
	protected virtual void OnActivateTool()
	{
		if (OverlayScreen.Instance != null && this.viewMode != OverlayModes.None.ID && OverlayScreen.Instance.mode != this.viewMode)
		{
			OverlayScreen.Instance.ToggleOverlay(this.viewMode, true);
			InterfaceTool.toolActivatedViewMode = this.viewMode;
		}
		this.SetCursor(this.cursor, this.cursorOffset, CursorMode.Auto);
	}

	// Token: 0x06004604 RID: 17924 RVA: 0x001931F8 File Offset: 0x001913F8
	public void SetCurrentVirtualInputModuleMousMovementMode(bool mouseMovementOnly, Action<VirtualInputModule> extraActions = null)
	{
		global::UnityEngine.EventSystems.EventSystem current = global::UnityEngine.EventSystems.EventSystem.current;
		if (current != null && current.currentInputModule != null)
		{
			VirtualInputModule virtualInputModule = current.currentInputModule as VirtualInputModule;
			if (virtualInputModule != null)
			{
				virtualInputModule.mouseMovementOnly = mouseMovementOnly;
				if (extraActions != null)
				{
					extraActions(virtualInputModule);
				}
			}
		}
	}

	// Token: 0x06004605 RID: 17925 RVA: 0x00193248 File Offset: 0x00191448
	public void DeactivateTool(InterfaceTool new_tool = null)
	{
		this.OnDeactivateTool(new_tool);
		if ((new_tool == null || new_tool == SelectTool.Instance) && InterfaceTool.toolActivatedViewMode != OverlayModes.None.ID && InterfaceTool.toolActivatedViewMode == SimDebugView.Instance.GetMode())
		{
			OverlayScreen.Instance.ToggleOverlay(OverlayModes.None.ID, true);
			InterfaceTool.toolActivatedViewMode = OverlayModes.None.ID;
		}
	}

	// Token: 0x06004606 RID: 17926 RVA: 0x001932B3 File Offset: 0x001914B3
	public virtual void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = null;
	}

	// Token: 0x06004607 RID: 17927 RVA: 0x001932B8 File Offset: 0x001914B8
	protected virtual void OnDeactivateTool(InterfaceTool new_tool)
	{
	}

	// Token: 0x06004608 RID: 17928 RVA: 0x001932BA File Offset: 0x001914BA
	private void OnApplicationFocus(bool focusStatus)
	{
		this.isAppFocused = focusStatus;
	}

	// Token: 0x06004609 RID: 17929 RVA: 0x001932C3 File Offset: 0x001914C3
	public virtual string GetDeactivateSound()
	{
		return "Tile_Cancel";
	}

	// Token: 0x0600460A RID: 17930 RVA: 0x001932CC File Offset: 0x001914CC
	public virtual void OnMouseMove(Vector3 cursor_pos)
	{
		if (this.visualizer == null || !this.isAppFocused)
		{
			return;
		}
		cursor_pos = Grid.CellToPosCBC(Grid.PosToCell(cursor_pos), this.visualizerLayer);
		cursor_pos.z += -0.15f;
		this.visualizer.transform.SetLocalPosition(cursor_pos);
	}

	// Token: 0x0600460B RID: 17931 RVA: 0x00193328 File Offset: 0x00191528
	public virtual void OnKeyDown(KButtonEvent e)
	{
	}

	// Token: 0x0600460C RID: 17932 RVA: 0x0019332A File Offset: 0x0019152A
	public virtual void OnKeyUp(KButtonEvent e)
	{
	}

	// Token: 0x0600460D RID: 17933 RVA: 0x0019332C File Offset: 0x0019152C
	public virtual void OnLeftClickDown(Vector3 cursor_pos)
	{
	}

	// Token: 0x0600460E RID: 17934 RVA: 0x0019332E File Offset: 0x0019152E
	public virtual void OnLeftClickUp(Vector3 cursor_pos)
	{
	}

	// Token: 0x0600460F RID: 17935 RVA: 0x00193330 File Offset: 0x00191530
	public virtual void OnRightClickDown(Vector3 cursor_pos, KButtonEvent e)
	{
	}

	// Token: 0x06004610 RID: 17936 RVA: 0x00193332 File Offset: 0x00191532
	public virtual void OnRightClickUp(Vector3 cursor_pos)
	{
	}

	// Token: 0x06004611 RID: 17937 RVA: 0x00193334 File Offset: 0x00191534
	public virtual void OnFocus(bool focus)
	{
		if (this.visualizer != null)
		{
			this.visualizer.SetActive(focus);
		}
		this.hasFocus = focus;
	}

	// Token: 0x06004612 RID: 17938 RVA: 0x00193358 File Offset: 0x00191558
	protected Vector2 GetRegularizedPos(Vector2 input, bool minimize)
	{
		Vector3 vector = new Vector3(Grid.HalfCellSizeInMeters, Grid.HalfCellSizeInMeters, 0f);
		return Grid.CellToPosCCC(Grid.PosToCell(input), Grid.SceneLayer.Background) + (minimize ? (-vector) : vector);
	}

	// Token: 0x06004613 RID: 17939 RVA: 0x001933A0 File Offset: 0x001915A0
	protected Vector2 GetWorldRestrictedPosition(Vector2 input)
	{
		input.x = Mathf.Clamp(input.x, ClusterManager.Instance.activeWorld.minimumBounds.x, ClusterManager.Instance.activeWorld.maximumBounds.x);
		input.y = Mathf.Clamp(input.y, ClusterManager.Instance.activeWorld.minimumBounds.y, ClusterManager.Instance.activeWorld.maximumBounds.y);
		return input;
	}

	// Token: 0x06004614 RID: 17940 RVA: 0x00193424 File Offset: 0x00191624
	protected void SetCursor(Texture2D new_cursor, Vector2 offset, CursorMode mode)
	{
		if (new_cursor != InterfaceTool.activeCursor && new_cursor != null)
		{
			InterfaceTool.activeCursor = new_cursor;
			try
			{
				Cursor.SetCursor(new_cursor, offset, mode);
				if (PlayerController.Instance.vim != null)
				{
					PlayerController.Instance.vim.SetCursor(new_cursor);
				}
			}
			catch (Exception ex)
			{
				string text = string.Format("SetCursor Failed new_cursor={0} offset={1} mode={2}", new_cursor, offset, mode);
				KCrashReporter.ReportDevNotification("SetCursor Failed", ex.StackTrace, text, false, null);
			}
		}
	}

	// Token: 0x06004615 RID: 17941 RVA: 0x001934B8 File Offset: 0x001916B8
	protected void UpdateHoverElements(List<KSelectable> hits)
	{
		if (this.hoverTextConfiguration != null)
		{
			this.hoverTextConfiguration.UpdateHoverElements(hits);
		}
	}

	// Token: 0x06004616 RID: 17942 RVA: 0x001934D4 File Offset: 0x001916D4
	public virtual void LateUpdate()
	{
		if (!this.populateHitsList)
		{
			this.UpdateHoverElements(null);
			return;
		}
		if (!this.isAppFocused)
		{
			return;
		}
		if (!Grid.IsValidCell(Grid.PosToCell(Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos()))))
		{
			return;
		}
		this.hits.Clear();
		this.GetSelectablesUnderCursor(this.hits);
		KSelectable objectUnderCursor = this.GetObjectUnderCursor<KSelectable>(false, (KSelectable s) => s.GetComponent<KSelectable>().IsSelectable, null);
		this.UpdateHoverElements(this.hits);
		if (!this.hasFocus && this.hoverOverride == null)
		{
			this.ClearHover();
		}
		else if (objectUnderCursor != this.hover)
		{
			this.ClearHover();
			this.hover = objectUnderCursor;
			if (objectUnderCursor != null)
			{
				Game.Instance.Trigger(2095258329, objectUnderCursor.gameObject);
				objectUnderCursor.Hover(!this.playedSoundThisFrame);
				this.playedSoundThisFrame = true;
			}
		}
		this.playedSoundThisFrame = false;
	}

	// Token: 0x06004617 RID: 17943 RVA: 0x001935D8 File Offset: 0x001917D8
	public void GetSelectablesUnderCursor(List<KSelectable> hits)
	{
		if (this.hoverOverride != null)
		{
			hits.Add(this.hoverOverride);
		}
		Camera main = Camera.main;
		Vector3 vector = new Vector3(KInputManager.GetMousePos().x, KInputManager.GetMousePos().y, -main.transform.GetPosition().z);
		Vector3 vector2 = main.ScreenToWorldPoint(vector);
		Vector2 vector3 = new Vector2(vector2.x, vector2.y);
		int num = Grid.PosToCell(vector2);
		if (!Grid.IsValidCell(num) || !Grid.IsVisible(num))
		{
			return;
		}
		Game.Instance.statusItemRenderer.GetIntersections(vector3, hits);
		ListPool<ScenePartitionerEntry, SelectTool>.PooledList pooledList = ListPool<ScenePartitionerEntry, SelectTool>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)vector3.x, (int)vector3.y, 1, 1, GameScenePartitioner.Instance.collisionLayer, pooledList);
		pooledList.Sort((ScenePartitionerEntry x, ScenePartitionerEntry y) => this.SortHoverCards(x, y));
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			KCollider2D kcollider2D = scenePartitionerEntry.obj as KCollider2D;
			if (!(kcollider2D == null) && kcollider2D.Intersects(new Vector2(vector3.x, vector3.y)))
			{
				KSelectable kselectable = kcollider2D.GetComponent<KSelectable>();
				if (kselectable == null)
				{
					kselectable = kcollider2D.GetComponentInParent<KSelectable>();
				}
				if (!(kselectable == null) && kselectable.isActiveAndEnabled && !hits.Contains(kselectable) && kselectable.IsSelectable)
				{
					hits.Add(kselectable);
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06004618 RID: 17944 RVA: 0x0019377C File Offset: 0x0019197C
	public void SetLinkCursor(bool set)
	{
		this.SetCursor(set ? Assets.GetTexture("cursor_hand") : this.cursor, set ? Vector2.zero : this.cursorOffset, CursorMode.Auto);
	}

	// Token: 0x06004619 RID: 17945 RVA: 0x001937AC File Offset: 0x001919AC
	protected T GetObjectUnderCursor<T>(bool cycleSelection, Func<T, bool> condition = null, Component previous_selection = null) where T : MonoBehaviour
	{
		this.intersections.Clear();
		this.GetObjectUnderCursor2D<T>(this.intersections, condition, this.layerMask);
		this.intersections.RemoveAll(new Predicate<InterfaceTool.Intersection>(InterfaceTool.is_component_null));
		if (this.intersections.Count <= 0)
		{
			this.prevIntersectionGroup.Clear();
			return default(T);
		}
		this.curIntersectionGroup.Clear();
		foreach (InterfaceTool.Intersection intersection in this.intersections)
		{
			this.curIntersectionGroup.Add(intersection.component);
		}
		if (!this.prevIntersectionGroup.Equals(this.curIntersectionGroup))
		{
			this.hitCycleCount = 0;
			this.prevIntersectionGroup = this.curIntersectionGroup;
		}
		this.intersections.Sort((InterfaceTool.Intersection a, InterfaceTool.Intersection b) => this.SortSelectables(a.component as KMonoBehaviour, b.component as KMonoBehaviour));
		int num = 0;
		if (cycleSelection)
		{
			num = this.hitCycleCount % this.intersections.Count;
			if (this.intersections[num].component != previous_selection || previous_selection == null)
			{
				num = 0;
				this.hitCycleCount = 0;
			}
			else
			{
				int num2 = this.hitCycleCount + 1;
				this.hitCycleCount = num2;
				num = num2 % this.intersections.Count;
			}
		}
		return this.intersections[num].component as T;
	}

	// Token: 0x0600461A RID: 17946 RVA: 0x0019392C File Offset: 0x00191B2C
	private void GetObjectUnderCursor2D<T>(List<InterfaceTool.Intersection> intersections, Func<T, bool> condition, int layer_mask) where T : MonoBehaviour
	{
		Camera main = Camera.main;
		Vector3 vector = new Vector3(KInputManager.GetMousePos().x, KInputManager.GetMousePos().y, -main.transform.GetPosition().z);
		Vector3 vector2 = main.ScreenToWorldPoint(vector);
		Vector2 vector3 = new Vector2(vector2.x, vector2.y);
		if (this.hoverOverride != null)
		{
			intersections.Add(new InterfaceTool.Intersection
			{
				component = this.hoverOverride,
				distance = -100f
			});
		}
		int num = Grid.PosToCell(vector2);
		if (Grid.IsValidCell(num) && Grid.IsVisible(num))
		{
			Game.Instance.statusItemRenderer.GetIntersections(vector3, intersections);
			ListPool<ScenePartitionerEntry, SelectTool>.PooledList pooledList = ListPool<ScenePartitionerEntry, SelectTool>.Allocate();
			int num2 = 0;
			int num3 = 0;
			Grid.CellToXY(num, out num2, out num3);
			GameScenePartitioner.Instance.GatherEntries(num2, num3, 1, 1, GameScenePartitioner.Instance.collisionLayer, pooledList);
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
			{
				KCollider2D kcollider2D = scenePartitionerEntry.obj as KCollider2D;
				if (!(kcollider2D == null) && kcollider2D.Intersects(new Vector2(vector2.x, vector2.y)))
				{
					T t = kcollider2D.GetComponent<T>();
					if (t == null)
					{
						t = kcollider2D.GetComponentInParent<T>();
					}
					if (!(t == null) && ((1 << t.gameObject.layer) & layer_mask) != 0 && !(t == null) && (condition == null || condition(t)))
					{
						float num4 = t.transform.GetPosition().z - vector2.z;
						bool flag = false;
						for (int i = 0; i < intersections.Count; i++)
						{
							InterfaceTool.Intersection intersection = intersections[i];
							if (intersection.component.gameObject == t.gameObject)
							{
								intersection.distance = Mathf.Min(intersection.distance, num4);
								intersections[i] = intersection;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							intersections.Add(new InterfaceTool.Intersection
							{
								component = t,
								distance = num4
							});
						}
					}
				}
			}
			pooledList.Recycle();
		}
	}

	// Token: 0x0600461B RID: 17947 RVA: 0x00193BD0 File Offset: 0x00191DD0
	private int SortSelectables(KMonoBehaviour x, KMonoBehaviour y)
	{
		if (x == null && y == null)
		{
			return 0;
		}
		if (x == null)
		{
			return -1;
		}
		if (y == null)
		{
			return 1;
		}
		int num = x.transform.GetPosition().z.CompareTo(y.transform.GetPosition().z);
		if (num != 0)
		{
			return num;
		}
		return x.GetInstanceID().CompareTo(y.GetInstanceID());
	}

	// Token: 0x0600461C RID: 17948 RVA: 0x00193C49 File Offset: 0x00191E49
	public void SetHoverOverride(KSelectable hover_override)
	{
		this.hoverOverride = hover_override;
	}

	// Token: 0x0600461D RID: 17949 RVA: 0x00193C54 File Offset: 0x00191E54
	private int SortHoverCards(ScenePartitionerEntry x, ScenePartitionerEntry y)
	{
		KMonoBehaviour kmonoBehaviour = x.obj as KMonoBehaviour;
		KMonoBehaviour kmonoBehaviour2 = y.obj as KMonoBehaviour;
		return this.SortSelectables(kmonoBehaviour, kmonoBehaviour2);
	}

	// Token: 0x0600461E RID: 17950 RVA: 0x00193C81 File Offset: 0x00191E81
	private static bool is_component_null(InterfaceTool.Intersection intersection)
	{
		return !intersection.component;
	}

	// Token: 0x0600461F RID: 17951 RVA: 0x00193C91 File Offset: 0x00191E91
	protected void ClearHover()
	{
		if (this.hover != null)
		{
			KSelectable kselectable = this.hover;
			this.hover = null;
			kselectable.Unhover();
			Game.Instance.Trigger(-1201923725, null);
		}
	}

	// Token: 0x04002E8B RID: 11915
	private static Dictionary<global::Action, InterfaceToolConfig> interfaceConfigMap = null;

	// Token: 0x04002E8C RID: 11916
	private static List<InterfaceToolConfig> activeConfigs = new List<InterfaceToolConfig>();

	// Token: 0x04002E8D RID: 11917
	public const float MaxClickDistance = 0.02f;

	// Token: 0x04002E8E RID: 11918
	public const float DepthBias = -0.15f;

	// Token: 0x04002E8F RID: 11919
	public GameObject visualizer;

	// Token: 0x04002E90 RID: 11920
	public Grid.SceneLayer visualizerLayer = Grid.SceneLayer.Move;

	// Token: 0x04002E91 RID: 11921
	public string placeSound;

	// Token: 0x04002E92 RID: 11922
	protected bool populateHitsList;

	// Token: 0x04002E93 RID: 11923
	[NonSerialized]
	public bool hasFocus;

	// Token: 0x04002E94 RID: 11924
	[SerializeField]
	protected Texture2D cursor;

	// Token: 0x04002E95 RID: 11925
	public Vector2 cursorOffset = new Vector2(2f, 2f);

	// Token: 0x04002E96 RID: 11926
	public global::System.Action OnDeactivate;

	// Token: 0x04002E97 RID: 11927
	private static Texture2D activeCursor = null;

	// Token: 0x04002E98 RID: 11928
	private static HashedString toolActivatedViewMode = OverlayModes.None.ID;

	// Token: 0x04002E99 RID: 11929
	protected HashedString viewMode = OverlayModes.None.ID;

	// Token: 0x04002E9A RID: 11930
	private HoverTextConfiguration hoverTextConfiguration;

	// Token: 0x04002E9B RID: 11931
	private KSelectable hoverOverride;

	// Token: 0x04002E9C RID: 11932
	public KSelectable hover;

	// Token: 0x04002E9D RID: 11933
	protected int layerMask;

	// Token: 0x04002E9E RID: 11934
	protected SelectMarker selectMarker;

	// Token: 0x04002E9F RID: 11935
	private List<RaycastResult> castResults = new List<RaycastResult>();

	// Token: 0x04002EA0 RID: 11936
	private bool isAppFocused = true;

	// Token: 0x04002EA1 RID: 11937
	private List<KSelectable> hits = new List<KSelectable>();

	// Token: 0x04002EA2 RID: 11938
	protected bool playedSoundThisFrame;

	// Token: 0x04002EA3 RID: 11939
	private List<InterfaceTool.Intersection> intersections = new List<InterfaceTool.Intersection>();

	// Token: 0x04002EA4 RID: 11940
	private HashSet<Component> prevIntersectionGroup = new HashSet<Component>();

	// Token: 0x04002EA5 RID: 11941
	private HashSet<Component> curIntersectionGroup = new HashSet<Component>();

	// Token: 0x04002EA6 RID: 11942
	private int hitCycleCount;

	// Token: 0x02001980 RID: 6528
	public struct Intersection
	{
		// Token: 0x04007CBD RID: 31933
		public MonoBehaviour component;

		// Token: 0x04007CBE RID: 31934
		public float distance;
	}
}
