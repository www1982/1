using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using STRINGS;
using UnityEngine;

// Token: 0x020005EB RID: 1515
public class Navigator : StateMachineComponent<Navigator.StatesInstance>, ISaveLoadableDetails
{
	// Token: 0x17000171 RID: 369
	// (get) Token: 0x0600234C RID: 9036 RVA: 0x000CA781 File Offset: 0x000C8981
	// (set) Token: 0x0600234D RID: 9037 RVA: 0x000CA789 File Offset: 0x000C8989
	public KMonoBehaviour target { get; set; }

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600234E RID: 9038 RVA: 0x000CA792 File Offset: 0x000C8992
	// (set) Token: 0x0600234F RID: 9039 RVA: 0x000CA79A File Offset: 0x000C899A
	public CellOffset[] targetOffsets { get; private set; }

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06002350 RID: 9040 RVA: 0x000CA7A3 File Offset: 0x000C89A3
	// (set) Token: 0x06002351 RID: 9041 RVA: 0x000CA7AB File Offset: 0x000C89AB
	public NavGrid NavGrid { get; private set; }

	// Token: 0x06002352 RID: 9042 RVA: 0x000CA7B4 File Offset: 0x000C89B4
	public void Serialize(BinaryWriter writer)
	{
		byte currentNavType = (byte)this.CurrentNavType;
		writer.Write(currentNavType);
		writer.Write(this.distanceTravelledByNavType.Count);
		foreach (KeyValuePair<NavType, int> keyValuePair in this.distanceTravelledByNavType)
		{
			byte key = (byte)keyValuePair.Key;
			writer.Write(key);
			writer.Write(keyValuePair.Value);
		}
	}

	// Token: 0x06002353 RID: 9043 RVA: 0x000CA83C File Offset: 0x000C8A3C
	public void Deserialize(IReader reader)
	{
		NavType navType = (NavType)reader.ReadByte();
		if (!SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 11))
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				NavType navType2 = (NavType)reader.ReadByte();
				int num2 = reader.ReadInt32();
				if (this.distanceTravelledByNavType.ContainsKey(navType2))
				{
					this.distanceTravelledByNavType[navType2] = num2;
				}
			}
		}
		bool flag = false;
		NavType[] validNavTypes = this.NavGrid.ValidNavTypes;
		for (int j = 0; j < validNavTypes.Length; j++)
		{
			if (validNavTypes[j] == navType)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.CurrentNavType = navType;
		}
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x000CA8E4 File Offset: 0x000C8AE4
	protected override void OnPrefabInit()
	{
		this.transitionDriver = new TransitionDriver(this);
		this.targetLocator = Util.KInstantiate(Assets.GetPrefab(TargetLocator.ID), null, null).GetComponent<KPrefabID>();
		this.targetLocator.gameObject.SetActive(true);
		this.log = new LoggerFSS("Navigator", 35);
		this.simRenderLoadBalance = true;
		this.autoRegisterSimRender = false;
		this.NavGrid = Pathfinding.Instance.GetNavGrid(this.NavGridName);
		base.GetComponent<PathProber>().SetValidNavTypes(this.NavGrid.ValidNavTypes, this.maxProbingRadius);
		this.distanceTravelledByNavType = new Dictionary<NavType, int>();
		for (int i = 0; i < 11; i++)
		{
			this.distanceTravelledByNavType.Add((NavType)i, 0);
		}
	}

	// Token: 0x06002355 RID: 9045 RVA: 0x000CA9A8 File Offset: 0x000C8BA8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Navigator>(1623392196, Navigator.OnDefeatedDelegate);
		base.Subscribe<Navigator>(-1506500077, Navigator.OnDefeatedDelegate);
		base.Subscribe<Navigator>(493375141, Navigator.OnRefreshUserMenuDelegate);
		base.Subscribe<Navigator>(-1503271301, Navigator.OnSelectObjectDelegate);
		base.Subscribe<Navigator>(856640610, Navigator.OnStoreDelegate);
		if (this.updateProber)
		{
			SimAndRenderScheduler.instance.Add(this, false);
		}
		this.pathProbeTask = new Navigator.PathProbeTask(this);
		this.SetCurrentNavType(this.CurrentNavType);
		this.SubscribeUnstuckFunctions();
	}

	// Token: 0x06002356 RID: 9046 RVA: 0x000CAA42 File Offset: 0x000C8C42
	private void SubscribeUnstuckFunctions()
	{
		if (this.CurrentNavType == NavType.Tube)
		{
			GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingTileChanged));
		}
	}

	// Token: 0x06002357 RID: 9047 RVA: 0x000CAA6F File Offset: 0x000C8C6F
	private void UnsubscribeUnstuckFunctions()
	{
		GameScenePartitioner.Instance.RemoveGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingTileChanged));
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x000CAA94 File Offset: 0x000C8C94
	private void OnBuildingTileChanged(int cell, object building)
	{
		if (this.CurrentNavType == NavType.Tube && building == null)
		{
			bool flag = cell == Grid.PosToCell(this);
			if (base.smi != null && flag)
			{
				this.SetCurrentNavType(NavType.Floor);
				this.UnsubscribeUnstuckFunctions();
			}
		}
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x000CAAD1 File Offset: 0x000C8CD1
	protected override void OnCleanUp()
	{
		this.UnsubscribeUnstuckFunctions();
		base.OnCleanUp();
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x000CAADF File Offset: 0x000C8CDF
	public bool IsMoving()
	{
		return base.smi.IsInsideState(base.smi.sm.normal.moving);
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x000CAB01 File Offset: 0x000C8D01
	public bool GoTo(int cell, CellOffset[] offsets = null)
	{
		if (offsets == null)
		{
			offsets = new CellOffset[1];
		}
		this.targetLocator.transform.SetPosition(Grid.CellToPosCBC(cell, Grid.SceneLayer.Move));
		return this.GoTo(this.targetLocator, offsets, NavigationTactics.ReduceTravelDistance);
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x000CAB39 File Offset: 0x000C8D39
	public bool GoTo(int cell, CellOffset[] offsets, NavTactic tactic)
	{
		if (offsets == null)
		{
			offsets = new CellOffset[1];
		}
		this.targetLocator.transform.SetPosition(Grid.CellToPosCBC(cell, Grid.SceneLayer.Move));
		return this.GoTo(this.targetLocator, offsets, tactic);
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x000CAB6D File Offset: 0x000C8D6D
	public void UpdateTarget(int cell)
	{
		this.targetLocator.transform.SetPosition(Grid.CellToPosCBC(cell, Grid.SceneLayer.Move));
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x000CAB88 File Offset: 0x000C8D88
	public bool GoTo(KMonoBehaviour target, CellOffset[] offsets, NavTactic tactic)
	{
		if (tactic == null)
		{
			tactic = NavigationTactics.ReduceTravelDistance;
		}
		base.smi.GoTo(base.smi.sm.normal.moving);
		base.smi.sm.moveTarget.Set(target.gameObject, base.smi, false);
		this.tactic = tactic;
		this.target = target;
		this.targetOffsets = offsets;
		this.ClearReservedCell();
		this.AdvancePath(true);
		return this.IsMoving();
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x000CAC0A File Offset: 0x000C8E0A
	public void BeginTransition(NavGrid.Transition transition)
	{
		this.transitionDriver.EndTransition();
		base.smi.GoTo(base.smi.sm.normal.moving);
		this.transitionDriver.BeginTransition(this, transition, this.defaultSpeed);
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x000CAC4C File Offset: 0x000C8E4C
	private bool ValidatePath(ref PathFinder.Path path, out bool atNextNode)
	{
		atNextNode = false;
		bool flag = false;
		if (path.IsValid())
		{
			int num = Grid.PosToCell(this.target);
			flag = this.reservedCell != NavigationReservations.InvalidReservation && this.CanReach(this.reservedCell);
			flag &= Grid.IsCellOffsetOf(this.reservedCell, num, this.targetOffsets);
		}
		if (flag)
		{
			int num2 = Grid.PosToCell(this);
			flag = num2 == path.nodes[0].cell && this.CurrentNavType == path.nodes[0].navType;
			flag |= (atNextNode = num2 == path.nodes[1].cell && this.CurrentNavType == path.nodes[1].navType);
		}
		if (!flag)
		{
			return false;
		}
		PathFinderAbilities currentAbilities = this.GetCurrentAbilities();
		return PathFinder.ValidatePath(this.NavGrid, currentAbilities, ref path);
	}

	// Token: 0x06002361 RID: 9057 RVA: 0x000CAD34 File Offset: 0x000C8F34
	public void AdvancePath(bool trigger_advance = true)
	{
		int num = Grid.PosToCell(this);
		if (this.target == null)
		{
			base.Trigger(-766531887, null);
			this.Stop(false, true);
		}
		else if (num == this.reservedCell && this.CurrentNavType != NavType.Tube)
		{
			this.Stop(true, true);
		}
		else
		{
			bool flag2;
			bool flag = !this.ValidatePath(ref this.path, out flag2);
			if (flag2)
			{
				this.path.nodes.RemoveAt(0);
			}
			if (flag)
			{
				int num2 = Grid.PosToCell(this.target);
				int cellPreferences = this.tactic.GetCellPreferences(num2, this.targetOffsets, this);
				this.SetReservedCell(cellPreferences);
				if (this.reservedCell == NavigationReservations.InvalidReservation)
				{
					this.Stop(false, true);
				}
				else
				{
					PathFinder.PotentialPath potentialPath = new PathFinder.PotentialPath(num, this.CurrentNavType, this.flags);
					PathFinder.UpdatePath(this.NavGrid, this.GetCurrentAbilities(), potentialPath, PathFinderQueries.cellQuery.Reset(this.reservedCell), ref this.path);
				}
			}
			if (this.path.IsValid())
			{
				this.BeginTransition(this.NavGrid.transitions[(int)this.path.nodes[1].transitionId]);
				this.distanceTravelledByNavType[this.CurrentNavType] = Mathf.Max(this.distanceTravelledByNavType[this.CurrentNavType] + 1, this.distanceTravelledByNavType[this.CurrentNavType]);
			}
			else if (this.path.HasArrived())
			{
				this.Stop(true, true);
			}
			else
			{
				this.ClearReservedCell();
				this.Stop(false, true);
			}
		}
		if (trigger_advance)
		{
			base.Trigger(1347184327, null);
		}
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x000CAED9 File Offset: 0x000C90D9
	public NavGrid.Transition GetNextTransition()
	{
		return this.NavGrid.transitions[(int)this.path.nodes[1].transitionId];
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x000CAF04 File Offset: 0x000C9104
	public void Stop(bool arrived_at_destination = false, bool play_idle = true)
	{
		this.target = null;
		this.targetOffsets = null;
		this.path.Clear();
		base.smi.sm.moveTarget.Set(null, base.smi);
		this.transitionDriver.EndTransition();
		if (play_idle)
		{
			HashedString idleAnim = this.NavGrid.GetIdleAnim(this.CurrentNavType);
			this.animController.Play(idleAnim, KAnim.PlayMode.Loop, 1f, 0f);
		}
		if (arrived_at_destination)
		{
			base.smi.GoTo(base.smi.sm.normal.arrived);
			return;
		}
		if (base.smi.GetCurrentState() == base.smi.sm.normal.moving)
		{
			this.ClearReservedCell();
			base.smi.GoTo(base.smi.sm.normal.failed);
		}
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x000CAFE9 File Offset: 0x000C91E9
	private void SimEveryTick(float dt)
	{
		if (this.IsMoving())
		{
			this.transitionDriver.UpdateTransition(dt);
		}
	}

	// Token: 0x06002365 RID: 9061 RVA: 0x000CAFFF File Offset: 0x000C91FF
	public void Sim4000ms(float dt)
	{
		this.UpdateProbe(true);
	}

	// Token: 0x06002366 RID: 9062 RVA: 0x000CB008 File Offset: 0x000C9208
	public void UpdateProbe(bool forceUpdate = false)
	{
		if (forceUpdate || !this.executePathProbeTaskAsync)
		{
			this.pathProbeTask.Update();
			this.pathProbeTask.Run(null, 0);
		}
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x000CB02D File Offset: 0x000C922D
	public void DrawPath()
	{
		if (base.gameObject.activeInHierarchy && this.IsMoving())
		{
			NavPathDrawer.Instance.DrawPath(this.animController.GetPivotSymbolPosition(), this.path);
		}
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x000CB05F File Offset: 0x000C925F
	public void Pause(string reason)
	{
		base.smi.sm.isPaused.Set(true, base.smi, false);
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x000CB07F File Offset: 0x000C927F
	public void Unpause(string reason)
	{
		base.smi.sm.isPaused.Set(false, base.smi, false);
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x000CB09F File Offset: 0x000C929F
	private void OnDefeated(object data)
	{
		this.ClearReservedCell();
		this.Stop(false, false);
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x000CB0AF File Offset: 0x000C92AF
	private void ClearReservedCell()
	{
		if (this.reservedCell != NavigationReservations.InvalidReservation)
		{
			NavigationReservations.Instance.RemoveOccupancy(this.reservedCell);
			this.reservedCell = NavigationReservations.InvalidReservation;
		}
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x000CB0D9 File Offset: 0x000C92D9
	private void SetReservedCell(int cell)
	{
		this.ClearReservedCell();
		this.reservedCell = cell;
		NavigationReservations.Instance.AddOccupancy(cell);
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x000CB0F3 File Offset: 0x000C92F3
	public int GetReservedCell()
	{
		return this.reservedCell;
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x000CB0FB File Offset: 0x000C92FB
	public int GetAnchorCell()
	{
		return this.AnchorCell;
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x000CB103 File Offset: 0x000C9303
	public bool IsValidNavType(NavType nav_type)
	{
		return this.NavGrid.HasNavTypeData(nav_type);
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x000CB114 File Offset: 0x000C9314
	public void SetCurrentNavType(NavType nav_type)
	{
		this.CurrentNavType = nav_type;
		this.AnchorCell = NavTypeHelper.GetAnchorCell(nav_type, Grid.PosToCell(this));
		NavGrid.NavTypeData navTypeData = this.NavGrid.GetNavTypeData(this.CurrentNavType);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		Vector2 one = Vector2.one;
		if (navTypeData.flipX)
		{
			one.x = -1f;
		}
		if (navTypeData.flipY)
		{
			one.y = -1f;
		}
		component.navMatrix = Matrix2x3.Translate(navTypeData.animControllerOffset * 200f) * Matrix2x3.Rotate(navTypeData.rotation) * Matrix2x3.Scale(one);
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x000CB1B8 File Offset: 0x000C93B8
	private void OnRefreshUserMenu(object data)
	{
		if (base.gameObject.HasTag(GameTags.Dead))
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((NavPathDrawer.Instance.GetNavigator() != this) ? new KIconButtonMenu.ButtonInfo("action_navigable_regions", UI.USERMENUACTIONS.DRAWPATHS.NAME, new global::System.Action(this.OnDrawPaths), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DRAWPATHS.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_navigable_regions", UI.USERMENUACTIONS.DRAWPATHS.NAME_OFF, new global::System.Action(this.OnDrawPaths), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DRAWPATHS.TOOLTIP_OFF, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 0.1f);
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_follow_cam", UI.USERMENUACTIONS.FOLLOWCAM.NAME, new global::System.Action(this.OnFollowCam), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.FOLLOWCAM.TOOLTIP, true), 0.3f);
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x000CB2BB File Offset: 0x000C94BB
	private void OnFollowCam()
	{
		if (CameraController.Instance.followTarget == base.transform)
		{
			CameraController.Instance.ClearFollowTarget();
			return;
		}
		CameraController.Instance.SetFollowTarget(base.transform);
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x000CB2EF File Offset: 0x000C94EF
	private void OnDrawPaths()
	{
		if (NavPathDrawer.Instance.GetNavigator() != this)
		{
			NavPathDrawer.Instance.SetNavigator(this);
			return;
		}
		NavPathDrawer.Instance.ClearNavigator();
	}

	// Token: 0x06002374 RID: 9076 RVA: 0x000CB319 File Offset: 0x000C9519
	private void OnSelectObject(object data)
	{
		NavPathDrawer.Instance.ClearNavigator();
	}

	// Token: 0x06002375 RID: 9077 RVA: 0x000CB325 File Offset: 0x000C9525
	public void OnStore(object data)
	{
		if (data is Storage || (data != null && (bool)data))
		{
			this.Stop(false, true);
		}
	}

	// Token: 0x06002376 RID: 9078 RVA: 0x000CB348 File Offset: 0x000C9548
	public PathFinderAbilities GetCurrentAbilities()
	{
		this.abilities.Refresh();
		return this.abilities;
	}

	// Token: 0x06002377 RID: 9079 RVA: 0x000CB35B File Offset: 0x000C955B
	public void SetAbilities(PathFinderAbilities abilities)
	{
		this.abilities = abilities;
	}

	// Token: 0x06002378 RID: 9080 RVA: 0x000CB364 File Offset: 0x000C9564
	public bool CanReach(IApproachable approachable)
	{
		return this.CanReach(approachable.GetCell(), approachable.GetOffsets());
	}

	// Token: 0x06002379 RID: 9081 RVA: 0x000CB378 File Offset: 0x000C9578
	public bool CanReach(int cell, CellOffset[] offsets)
	{
		foreach (CellOffset cellOffset in offsets)
		{
			int num = Grid.OffsetCell(cell, cellOffset);
			if (this.CanReach(num))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600237A RID: 9082 RVA: 0x000CB3B1 File Offset: 0x000C95B1
	public bool CanReach(int cell)
	{
		return this.GetNavigationCost(cell) != -1;
	}

	// Token: 0x0600237B RID: 9083 RVA: 0x000CB3C0 File Offset: 0x000C95C0
	public int GetNavigationCost(int cell)
	{
		if (Grid.IsValidCell(cell))
		{
			return this.PathProber.GetCost(cell);
		}
		return -1;
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x000CB3D8 File Offset: 0x000C95D8
	public int GetNavigationCostIgnoreProberOffset(int cell, CellOffset[] offsets)
	{
		return this.PathProber.GetNavigationCostIgnoreProberOffset(cell, offsets);
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x000CB3E8 File Offset: 0x000C95E8
	public int GetNavigationCost(int cell, CellOffset[] offsets)
	{
		int num = -1;
		int num2 = offsets.Length;
		for (int i = 0; i < num2; i++)
		{
			int num3 = Grid.OffsetCell(cell, offsets[i]);
			int navigationCost = this.GetNavigationCost(num3);
			if (navigationCost != -1 && (num == -1 || navigationCost < num))
			{
				num = navigationCost;
			}
		}
		return num;
	}

	// Token: 0x0600237E RID: 9086 RVA: 0x000CB430 File Offset: 0x000C9630
	public int GetNavigationCost(int cell, IReadOnlyList<CellOffset> offsets)
	{
		int num = -1;
		int count = offsets.Count;
		for (int i = 0; i < count; i++)
		{
			int num2 = Grid.OffsetCell(cell, offsets[i]);
			int navigationCost = this.GetNavigationCost(num2);
			if (navigationCost != -1 && (num == -1 || navigationCost < num))
			{
				num = navigationCost;
			}
		}
		return num;
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x000CB47B File Offset: 0x000C967B
	public int GetNavigationCost(IApproachable approachable)
	{
		return this.GetNavigationCost(approachable.GetCell(), approachable.GetOffsets());
	}

	// Token: 0x06002380 RID: 9088 RVA: 0x000CB490 File Offset: 0x000C9690
	public void RunQuery(PathFinderQuery query)
	{
		int num = Grid.PosToCell(this);
		PathFinder.PotentialPath potentialPath = new PathFinder.PotentialPath(num, this.CurrentNavType, this.flags);
		PathFinder.Run(this.NavGrid, this.GetCurrentAbilities(), potentialPath, query);
	}

	// Token: 0x06002381 RID: 9089 RVA: 0x000CB4CB File Offset: 0x000C96CB
	public void SetFlags(PathFinder.PotentialPath.Flags new_flags)
	{
		this.flags |= new_flags;
	}

	// Token: 0x06002382 RID: 9090 RVA: 0x000CB4DB File Offset: 0x000C96DB
	public void ClearFlags(PathFinder.PotentialPath.Flags new_flags)
	{
		this.flags &= ~new_flags;
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x000CB4ED File Offset: 0x000C96ED
	[Conditional("ENABLE_DETAILED_NAVIGATOR_PROFILE_INFO")]
	public static void BeginDetailedSample(string region_name)
	{
	}

	// Token: 0x06002384 RID: 9092 RVA: 0x000CB4EF File Offset: 0x000C96EF
	[Conditional("ENABLE_DETAILED_NAVIGATOR_PROFILE_INFO")]
	public static void EndDetailedSample(string region_name)
	{
	}

	// Token: 0x04001480 RID: 5248
	public bool DebugDrawPath;

	// Token: 0x04001484 RID: 5252
	[MyCmpAdd]
	public PathProber PathProber;

	// Token: 0x04001485 RID: 5253
	[MyCmpAdd]
	public Facing facing;

	// Token: 0x04001486 RID: 5254
	public float defaultSpeed = 1f;

	// Token: 0x04001487 RID: 5255
	public TransitionDriver transitionDriver;

	// Token: 0x04001488 RID: 5256
	public string NavGridName;

	// Token: 0x04001489 RID: 5257
	public bool updateProber;

	// Token: 0x0400148A RID: 5258
	public int maxProbingRadius;

	// Token: 0x0400148B RID: 5259
	public PathFinder.PotentialPath.Flags flags;

	// Token: 0x0400148C RID: 5260
	private LoggerFSS log;

	// Token: 0x0400148D RID: 5261
	public Dictionary<NavType, int> distanceTravelledByNavType;

	// Token: 0x0400148E RID: 5262
	public Grid.SceneLayer sceneLayer = Grid.SceneLayer.Move;

	// Token: 0x0400148F RID: 5263
	private PathFinderAbilities abilities;

	// Token: 0x04001490 RID: 5264
	[MyCmpReq]
	public KBatchedAnimController animController;

	// Token: 0x04001491 RID: 5265
	[NonSerialized]
	public PathFinder.Path path;

	// Token: 0x04001492 RID: 5266
	public NavType CurrentNavType;

	// Token: 0x04001493 RID: 5267
	private int AnchorCell;

	// Token: 0x04001494 RID: 5268
	private KPrefabID targetLocator;

	// Token: 0x04001495 RID: 5269
	private int reservedCell = NavigationReservations.InvalidReservation;

	// Token: 0x04001496 RID: 5270
	private NavTactic tactic;

	// Token: 0x04001497 RID: 5271
	public Navigator.PathProbeTask pathProbeTask;

	// Token: 0x04001498 RID: 5272
	private static readonly EventSystem.IntraObjectHandler<Navigator> OnDefeatedDelegate = new EventSystem.IntraObjectHandler<Navigator>(delegate(Navigator component, object data)
	{
		component.OnDefeated(data);
	});

	// Token: 0x04001499 RID: 5273
	private static readonly EventSystem.IntraObjectHandler<Navigator> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Navigator>(delegate(Navigator component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x0400149A RID: 5274
	private static readonly EventSystem.IntraObjectHandler<Navigator> OnSelectObjectDelegate = new EventSystem.IntraObjectHandler<Navigator>(delegate(Navigator component, object data)
	{
		component.OnSelectObject(data);
	});

	// Token: 0x0400149B RID: 5275
	private static readonly EventSystem.IntraObjectHandler<Navigator> OnStoreDelegate = new EventSystem.IntraObjectHandler<Navigator>(delegate(Navigator component, object data)
	{
		component.OnStore(data);
	});

	// Token: 0x0400149C RID: 5276
	public bool executePathProbeTaskAsync;

	// Token: 0x02001476 RID: 5238
	public class ActiveTransition
	{
		// Token: 0x06008DD0 RID: 36304 RVA: 0x003597F4 File Offset: 0x003579F4
		public void Init(NavGrid.Transition transition, float default_speed)
		{
			this.x = transition.x;
			this.y = transition.y;
			this.isLooping = transition.isLooping;
			this.start = transition.start;
			this.end = transition.end;
			this.preAnim = transition.preAnim;
			this.anim = transition.anim;
			this.speed = default_speed;
			this.animSpeed = transition.animSpeed;
			this.navGridTransition = transition;
		}

		// Token: 0x06008DD1 RID: 36305 RVA: 0x0035987C File Offset: 0x00357A7C
		public void Copy(Navigator.ActiveTransition other)
		{
			this.x = other.x;
			this.y = other.y;
			this.isLooping = other.isLooping;
			this.start = other.start;
			this.end = other.end;
			this.preAnim = other.preAnim;
			this.anim = other.anim;
			this.speed = other.speed;
			this.animSpeed = other.animSpeed;
			this.navGridTransition = other.navGridTransition;
		}

		// Token: 0x04006CA7 RID: 27815
		public int x;

		// Token: 0x04006CA8 RID: 27816
		public int y;

		// Token: 0x04006CA9 RID: 27817
		public bool isLooping;

		// Token: 0x04006CAA RID: 27818
		public NavType start;

		// Token: 0x04006CAB RID: 27819
		public NavType end;

		// Token: 0x04006CAC RID: 27820
		public HashedString preAnim;

		// Token: 0x04006CAD RID: 27821
		public HashedString anim;

		// Token: 0x04006CAE RID: 27822
		public float speed;

		// Token: 0x04006CAF RID: 27823
		public float animSpeed = 1f;

		// Token: 0x04006CB0 RID: 27824
		public Func<bool> isCompleteCB;

		// Token: 0x04006CB1 RID: 27825
		public NavGrid.Transition navGridTransition;
	}

	// Token: 0x02001477 RID: 5239
	public class StatesInstance : GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.GameInstance
	{
		// Token: 0x06008DD3 RID: 36307 RVA: 0x00359914 File Offset: 0x00357B14
		public StatesInstance(Navigator master)
			: base(master)
		{
		}
	}

	// Token: 0x02001478 RID: 5240
	public class States : GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator>
	{
		// Token: 0x06008DD4 RID: 36308 RVA: 0x00359920 File Offset: 0x00357B20
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.normal.stopped;
			this.saveHistory = true;
			this.normal.ParamTransition<bool>(this.isPaused, this.paused, GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.IsTrue).Update("NavigatorProber", delegate(Navigator.StatesInstance smi, float dt)
			{
				smi.master.Sim4000ms(dt);
			}, UpdateRate.SIM_4000ms, false);
			this.normal.moving.Enter(delegate(Navigator.StatesInstance smi)
			{
				smi.Trigger(1027377649, GameHashes.ObjectMovementWakeUp);
			}).Update("UpdateNavigator", delegate(Navigator.StatesInstance smi, float dt)
			{
				smi.master.SimEveryTick(dt);
			}, UpdateRate.SIM_EVERY_TICK, true).Exit(delegate(Navigator.StatesInstance smi)
			{
				smi.Trigger(1027377649, GameHashes.ObjectMovementSleep);
			});
			this.normal.arrived.TriggerOnEnter(GameHashes.DestinationReached, null).GoTo(this.normal.stopped);
			this.normal.failed.TriggerOnEnter(GameHashes.NavigationFailed, null).GoTo(this.normal.stopped);
			this.normal.stopped.Enter(delegate(Navigator.StatesInstance smi)
			{
				smi.master.SubscribeUnstuckFunctions();
			}).DoNothing().Exit(delegate(Navigator.StatesInstance smi)
			{
				smi.master.UnsubscribeUnstuckFunctions();
			});
			this.paused.ParamTransition<bool>(this.isPaused, this.normal, GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.IsFalse);
		}

		// Token: 0x04006CB2 RID: 27826
		public StateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.TargetParameter moveTarget;

		// Token: 0x04006CB3 RID: 27827
		public StateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.BoolParameter isPaused = new StateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.BoolParameter(false);

		// Token: 0x04006CB4 RID: 27828
		public Navigator.States.NormalStates normal;

		// Token: 0x04006CB5 RID: 27829
		public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State paused;

		// Token: 0x0200273F RID: 10047
		public class NormalStates : GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State
		{
			// Token: 0x0400AD3D RID: 44349
			public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State moving;

			// Token: 0x0400AD3E RID: 44350
			public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State arrived;

			// Token: 0x0400AD3F RID: 44351
			public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State failed;

			// Token: 0x0400AD40 RID: 44352
			public GameStateMachine<Navigator.States, Navigator.StatesInstance, Navigator, object>.State stopped;
		}
	}

	// Token: 0x02001479 RID: 5241
	public struct PathProbeTask : IWorkItem<object>
	{
		// Token: 0x06008DD6 RID: 36310 RVA: 0x00359AE0 File Offset: 0x00357CE0
		public PathProbeTask(Navigator navigator)
		{
			this.navigator = navigator;
			this.cell = -1;
		}

		// Token: 0x06008DD7 RID: 36311 RVA: 0x00359AF0 File Offset: 0x00357CF0
		public void Update()
		{
			this.cell = Grid.PosToCell(this.navigator);
			this.navigator.abilities.Refresh();
		}

		// Token: 0x06008DD8 RID: 36312 RVA: 0x00359B14 File Offset: 0x00357D14
		public void Run(object sharedData, int threadIndex)
		{
			this.navigator.PathProber.UpdateProbe(this.navigator.NavGrid, this.cell, this.navigator.CurrentNavType, this.navigator.abilities, this.navigator.flags);
		}

		// Token: 0x04006CB6 RID: 27830
		private int cell;

		// Token: 0x04006CB7 RID: 27831
		private Navigator navigator;
	}

	// Token: 0x0200147A RID: 5242
	public class Scanner<T> where T : KMonoBehaviour
	{
		// Token: 0x06008DD9 RID: 36313 RVA: 0x00359B63 File Offset: 0x00357D63
		public Scanner(int radius, ScenePartitionerLayer layer, Func<T, bool> filterFn)
		{
			this.radius = radius;
			this.layer = layer;
			this.filterFn = filterFn;
			this.offsets = Navigator.Scanner<T>.NO_OFFSETS;
			this.offsetsFn = null;
			this.early_out_threshold = null;
		}

		// Token: 0x06008DDA RID: 36314 RVA: 0x00359B9E File Offset: 0x00357D9E
		public void SetConstantOffsets(CellOffset[] offsets)
		{
			this.offsets = offsets;
		}

		// Token: 0x06008DDB RID: 36315 RVA: 0x00359BA7 File Offset: 0x00357DA7
		public void SetDynamicOffsetsFn(Action<T, List<CellOffset>> offsetsFn)
		{
			this.offsetsFn = offsetsFn;
		}

		// Token: 0x06008DDC RID: 36316 RVA: 0x00359BB0 File Offset: 0x00357DB0
		public void SetEarlyOutThreshold(int early_out_threshold)
		{
			this.early_out_threshold = new int?(early_out_threshold);
		}

		// Token: 0x06008DDD RID: 36317 RVA: 0x00359BBE File Offset: 0x00357DBE
		private int NavCostFromConstantOffsets(Navigator navigator, T destinationObject, CellOffset[] offsets)
		{
			return navigator.GetNavigationCost(Grid.PosToCell(destinationObject.gameObject), offsets);
		}

		// Token: 0x06008DDE RID: 36318 RVA: 0x00359BD8 File Offset: 0x00357DD8
		private int NavCostFromDynamicOffsets(Navigator navigator, T destinationObject, Action<T, List<CellOffset>> offsetsFn)
		{
			ListPool<CellOffset, Navigator>.PooledList pooledList = ListPool<CellOffset, Navigator>.Allocate();
			offsetsFn(destinationObject, pooledList);
			int navigationCost = navigator.GetNavigationCost(Grid.PosToCell(destinationObject.gameObject), pooledList);
			pooledList.Recycle();
			return navigationCost;
		}

		// Token: 0x06008DDF RID: 36319 RVA: 0x00359C10 File Offset: 0x00357E10
		public T Scan(Vector2I gridPos, Navigator navigator)
		{
			ListPool<ScenePartitionerEntry, Navigator>.PooledList pooledList = ListPool<ScenePartitionerEntry, Navigator>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(gridPos.x - this.radius, gridPos.y - this.radius, this.radius * 2, this.radius * 2, this.layer, pooledList);
			T t = default(T);
			int num = -1;
			if (this.early_out_threshold != null)
			{
				pooledList.Shuffle<ScenePartitionerEntry>();
				if (this.offsetsFn != null)
				{
					for (int i = 0; i < pooledList.Count; i++)
					{
						T t2 = pooledList[i].obj as T;
						if (this.filterFn(t2))
						{
							int num2 = this.NavCostFromDynamicOffsets(navigator, t2, this.offsetsFn);
							if (num2 != -1 && (t == null || num2 < num))
							{
								t = t2;
								num = num2;
								if (num2 <= this.early_out_threshold.Value)
								{
									break;
								}
							}
						}
					}
				}
				else
				{
					for (int j = 0; j < pooledList.Count; j++)
					{
						T t3 = pooledList[j].obj as T;
						if (this.filterFn(t3))
						{
							int num3 = this.NavCostFromConstantOffsets(navigator, t3, this.offsets);
							if (num3 != -1 && (t == null || num3 < num))
							{
								t = t3;
								num = num3;
								if (num3 <= this.early_out_threshold.Value)
								{
									break;
								}
							}
						}
					}
				}
			}
			else if (this.offsetsFn != null)
			{
				for (int k = 0; k < pooledList.Count; k++)
				{
					T t4 = pooledList[k].obj as T;
					if (this.filterFn(t4))
					{
						int num4 = this.NavCostFromDynamicOffsets(navigator, t4, this.offsetsFn);
						if (num4 != -1 && (t == null || num4 < num))
						{
							t = t4;
							num = num4;
						}
					}
				}
			}
			else
			{
				for (int l = 0; l < pooledList.Count; l++)
				{
					T t5 = pooledList[l].obj as T;
					if (this.filterFn(t5))
					{
						int num5 = this.NavCostFromConstantOffsets(navigator, t5, this.offsets);
						if (num5 != -1 && (t == null || num5 < num))
						{
							t = t5;
							num = num5;
						}
					}
				}
			}
			pooledList.Recycle();
			return t;
		}

		// Token: 0x04006CB8 RID: 27832
		private static readonly CellOffset[] NO_OFFSETS = new CellOffset[]
		{
			new CellOffset(0, 0)
		};

		// Token: 0x04006CB9 RID: 27833
		private readonly int radius;

		// Token: 0x04006CBA RID: 27834
		private readonly ScenePartitionerLayer layer;

		// Token: 0x04006CBB RID: 27835
		private readonly Func<T, bool> filterFn;

		// Token: 0x04006CBC RID: 27836
		private CellOffset[] offsets;

		// Token: 0x04006CBD RID: 27837
		private Action<T, List<CellOffset>> offsetsFn;

		// Token: 0x04006CBE RID: 27838
		private int? early_out_threshold;
	}
}
