using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000586 RID: 1414
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Constructable")]
public class Constructable : Workable, ISaveLoadable
{
	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06002037 RID: 8247 RVA: 0x000B9122 File Offset: 0x000B7322
	public Recipe Recipe
	{
		get
		{
			return this.building.Def.CraftRecipe;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06002038 RID: 8248 RVA: 0x000B9134 File Offset: 0x000B7334
	// (set) Token: 0x06002039 RID: 8249 RVA: 0x000B913C File Offset: 0x000B733C
	public IList<Tag> SelectedElementsTags
	{
		get
		{
			return this.selectedElementsTags;
		}
		set
		{
			if (this.selectedElementsTags == null || this.selectedElementsTags.Length != value.Count)
			{
				this.selectedElementsTags = new Tag[value.Count];
			}
			value.CopyTo(this.selectedElementsTags, 0);
		}
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x000B9174 File Offset: 0x000B7374
	public override string GetConversationTopic()
	{
		return this.building.Def.PrefabID;
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x000B9188 File Offset: 0x000B7388
	protected override void OnCompleteWork(WorkerBase worker)
	{
		float num = 0f;
		float num2 = 0f;
		bool flag = true;
		foreach (GameObject gameObject in this.storage.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null))
				{
					num += component.Mass;
					num2 += component.Temperature * component.Mass;
					flag = flag && component.HasTag(GameTags.Liquifiable);
				}
			}
		}
		if (num <= 0f)
		{
			DebugUtil.LogWarningArgs(base.gameObject, new object[]
			{
				"uhhh this constructable is about to generate a nan",
				"Item Count: ",
				this.storage.items.Count
			});
			return;
		}
		if (flag)
		{
			this.initialTemperature = Mathf.Min(num2 / num, 318.15f);
		}
		else
		{
			this.initialTemperature = Mathf.Clamp(num2 / num, 0f, 318.15f);
		}
		KAnimGraphTileVisualizer component2 = base.GetComponent<KAnimGraphTileVisualizer>();
		UtilityConnections connections = ((component2 == null) ? ((UtilityConnections)0) : component2.Connections);
		bool flag2 = true;
		if (this.IsReplacementTile)
		{
			int num3 = Grid.PosToCell(base.transform.GetLocalPosition());
			GameObject replacementCandidate = this.building.Def.GetReplacementCandidate(num3);
			if (replacementCandidate != null)
			{
				flag2 = false;
				SimCellOccupier component3 = replacementCandidate.GetComponent<SimCellOccupier>();
				if (component3 != null)
				{
					component3.DestroySelf(delegate
					{
						if (this != null && this.gameObject != null)
						{
							this.FinishConstruction(connections, worker);
						}
					});
				}
				else
				{
					Conduit component4 = replacementCandidate.GetComponent<Conduit>();
					if (component4 != null)
					{
						component4.GetFlowManager().MarkForReplacement(num3);
					}
					BuildingComplete component5 = replacementCandidate.GetComponent<BuildingComplete>();
					if (component5 != null)
					{
						component5.Subscribe(-21016276, delegate(object data)
						{
							this.FinishConstruction(connections, worker);
						});
					}
					else
					{
						global::Debug.LogWarning("Why am I trying to replace a: " + replacementCandidate.name);
						this.FinishConstruction(connections, worker);
					}
				}
				KAnimGraphTileVisualizer component6 = replacementCandidate.GetComponent<KAnimGraphTileVisualizer>();
				if (component6 != null)
				{
					component6.skipCleanup = true;
				}
				Deconstructable component7 = replacementCandidate.GetComponent<Deconstructable>();
				if (component7 != null)
				{
					component7.SpawnItemsFromConstruction(worker);
				}
				Constructable.ReplaceCallbackParameters replaceCallbackParameters = new Constructable.ReplaceCallbackParameters
				{
					TileLayer = this.building.Def.TileLayer,
					Worker = worker
				};
				replacementCandidate.Trigger(1606648047, replaceCallbackParameters);
				replacementCandidate.DeleteObject();
			}
		}
		if (flag2)
		{
			this.FinishConstruction(connections, worker);
		}
		PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Building, base.GetComponent<KSelectable>().GetName(), base.transform, 1.5f, false);
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x000B9488 File Offset: 0x000B7688
	private void FinishConstruction(UtilityConnections connections, WorkerBase workerForGameplayEvent)
	{
		Rotatable component = base.GetComponent<Rotatable>();
		Orientation orientation = ((component != null) ? component.GetOrientation() : Orientation.Neutral);
		int num = Grid.PosToCell(base.transform.GetLocalPosition());
		this.UnmarkArea();
		GameObject gameObject = this.building.Def.Build(num, orientation, this.storage, this.selectedElementsTags, this.initialTemperature, base.GetComponent<BuildingFacade>().CurrentFacade, true, GameClock.Instance.GetTime());
		BonusEvent.GameplayEventData gameplayEventData = new BonusEvent.GameplayEventData();
		gameplayEventData.building = gameObject.GetComponent<BuildingComplete>();
		gameplayEventData.workable = this;
		gameplayEventData.worker = workerForGameplayEvent;
		gameplayEventData.eventTrigger = GameHashes.NewBuilding;
		GameplayEventManager.Instance.Trigger(-1661515756, gameplayEventData);
		gameObject.transform.rotation = base.transform.rotation;
		Rotatable component2 = gameObject.GetComponent<Rotatable>();
		if (component2 != null)
		{
			component2.SetOrientation(orientation);
		}
		KAnimGraphTileVisualizer component3 = base.GetComponent<KAnimGraphTileVisualizer>();
		if (component3 != null)
		{
			gameObject.GetComponent<KAnimGraphTileVisualizer>().Connections = connections;
			component3.skipCleanup = true;
		}
		KSelectable component4 = base.GetComponent<KSelectable>();
		if (component4 != null && component4.IsSelected && gameObject.GetComponent<KSelectable>() != null)
		{
			component4.Unselect();
			if (PlayerController.Instance.ActiveTool.name == "SelectTool")
			{
				((SelectTool)PlayerController.Instance.ActiveTool).SelectNextFrame(gameObject.GetComponent<KSelectable>(), false);
			}
		}
		gameObject.Trigger(2121280625, this);
		this.storage.ConsumeAllIgnoringDisease();
		this.finished = true;
		this.DeleteObject();
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x000B9624 File Offset: 0x000B7824
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.invalidLocation = new Notification(MISC.NOTIFICATIONS.INVALIDCONSTRUCTIONLOCATION.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.INVALIDCONSTRUCTIONLOCATION.TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);
		this.faceTargetWhenWorking = true;
		base.Subscribe<Constructable>(-1432940121, Constructable.OnReachableChangedDelegate);
		if (this.rotatable == null)
		{
			this.MarkArea();
		}
		if (Db.Get().TechItems.GetTechTierForItem(this.building.Def.PrefabID) > 2)
		{
			this.requireMinionToWork = true;
		}
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Building;
		this.workingStatusItem = null;
		this.attributeConverter = Db.Get().AttributeConverters.ConstructionSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.minimumAttributeMultiplier = 0.75f;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Building.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		Prioritizable.AddRef(base.gameObject);
		this.synchronizeAnims = false;
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x000B977C File Offset: 0x000B797C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		CellOffset[][] array = OffsetGroups.InvertedStandardTable;
		if (this.building.Def.IsTilePiece)
		{
			array = OffsetGroups.InvertedStandardTableWithCorners;
		}
		CellOffset[] array2 = this.building.Def.PlacementOffsets;
		if (this.rotatable != null)
		{
			array2 = new CellOffset[this.building.Def.PlacementOffsets.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = this.rotatable.GetRotatedCellOffset(this.building.Def.PlacementOffsets[i]);
			}
		}
		CellOffset[][] array3 = OffsetGroups.BuildReachabilityTable(array2, array, this.building.Def.ConstructionOffsetFilter);
		base.SetOffsetTable(array3);
		this.storage.SetOffsetTable(array3);
		base.Subscribe<Constructable>(2127324410, Constructable.OnCancelDelegate);
		if (this.rotatable != null)
		{
			this.MarkArea();
		}
		this.fetchList = new FetchList2(this.storage, Db.Get().ChoreTypes.BuildFetch);
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		Element element = ElementLoader.GetElement(this.SelectedElementsTags[0]);
		global::Debug.Assert(element != null, "Missing primary element for Constructable");
		component.ElementID = element.id;
		float num = component.Element.highTemp - 10f;
		component.Temperature = (component.Temperature = Mathf.Min(this.building.Def.Temperature, num));
		foreach (Recipe.Ingredient ingredient in this.Recipe.GetAllIngredients(this.selectedElementsTags))
		{
			this.fetchList.Add(ingredient.tag, null, ingredient.amount, Operational.State.None);
			MaterialNeeds.UpdateNeed(ingredient.tag, ingredient.amount, base.gameObject.GetMyWorldId());
		}
		if (!this.building.Def.IsTilePiece)
		{
			base.gameObject.layer = LayerMask.NameToLayer("Construction");
		}
		this.building.RunOnArea(delegate(int offset_cell)
		{
			if (base.gameObject.GetComponent<ConduitBridge>() == null)
			{
				GameObject gameObject3 = Grid.Objects[offset_cell, 7];
				if (gameObject3 != null)
				{
					gameObject3.DeleteObject();
				}
			}
		});
		if (this.IsReplacementTile && this.building.Def.ReplacementLayer != ObjectLayer.NumLayers)
		{
			int num2 = Grid.PosToCell(base.transform.GetPosition());
			GameObject gameObject = Grid.Objects[num2, (int)this.building.Def.ReplacementLayer];
			if (gameObject == null || gameObject == base.gameObject)
			{
				Grid.Objects[num2, (int)this.building.Def.ReplacementLayer] = base.gameObject;
				if (base.gameObject.GetComponent<SimCellOccupier>() != null)
				{
					int num3 = LayerMask.NameToLayer("Overlay");
					World.Instance.blockTileRenderer.AddBlock(num3, this.building.Def, this.IsReplacementTile, SimHashes.Void, num2);
				}
				TileVisualizer.RefreshCell(num2, this.building.Def.TileLayer, this.building.Def.ReplacementLayer);
			}
			else
			{
				global::Debug.LogError("multiple replacement tiles on the same cell!");
				Util.KDestroyGameObject(base.gameObject);
			}
			GameObject gameObject2 = Grid.Objects[num2, (int)this.building.Def.ObjectLayer];
			if (gameObject2 != null)
			{
				Deconstructable component2 = gameObject2.GetComponent<Deconstructable>();
				if (component2 != null)
				{
					component2.CancelDeconstruction();
				}
			}
		}
		bool flag = this.building.Def.BuildingComplete.GetComponent<Ladder>();
		this.waitForFetchesBeforeDigging = flag || this.building.Def.BuildingComplete.GetComponent<SimCellOccupier>() || this.building.Def.BuildingComplete.GetComponent<Door>() || this.building.Def.BuildingComplete.GetComponent<LiquidPumpingStation>();
		if (flag)
		{
			int num4 = 0;
			int num5 = 0;
			Grid.CellToXY(Grid.PosToCell(this), out num4, out num5);
			int num6 = num5 - 3;
			this.ladderDetectionExtents = new Extents(num4, num6, 1, 5);
			this.ladderPartitionerEntry = GameScenePartitioner.Instance.Add("Constructable.OnNearbyBuildingLayerChanged", base.gameObject, this.ladderDetectionExtents, GameScenePartitioner.Instance.objectLayers[1], new Action<object>(this.OnNearbyBuildingLayerChanged));
			this.OnNearbyBuildingLayerChanged(null);
		}
		this.fetchList.Submit(new global::System.Action(this.OnFetchListComplete), true);
		this.PlaceDiggables();
		new ReachabilityMonitor.Instance(this).StartSM();
		base.Subscribe<Constructable>(493375141, Constructable.OnRefreshUserMenuDelegate);
		Prioritizable component3 = base.GetComponent<Prioritizable>();
		Prioritizable prioritizable = component3;
		prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnPriorityChanged));
		this.OnPriorityChanged(component3.GetMasterPriority());
	}

	// Token: 0x0600203F RID: 8255 RVA: 0x000B9C58 File Offset: 0x000B7E58
	private void OnPriorityChanged(PrioritySetting priority)
	{
		this.building.RunOnArea(delegate(int cell)
		{
			Diggable diggable = Diggable.GetDiggable(cell);
			if (diggable != null)
			{
				diggable.GetComponent<Prioritizable>().SetMasterPriority(priority);
			}
		});
	}

	// Token: 0x06002040 RID: 8256 RVA: 0x000B9C8C File Offset: 0x000B7E8C
	private void MarkArea()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingDef def = this.building.Def;
		Orientation orientation = this.building.Orientation;
		ObjectLayer objectLayer = (this.IsReplacementTile ? def.ReplacementLayer : def.ObjectLayer);
		def.MarkArea(num, orientation, objectLayer, base.gameObject);
		if (def.IsTilePiece)
		{
			if (Grid.Objects[num, (int)def.TileLayer] == null)
			{
				def.MarkArea(num, orientation, def.TileLayer, base.gameObject);
				def.RunOnArea(num, orientation, delegate(int c)
				{
					TileVisualizer.RefreshCell(c, def.TileLayer, def.ReplacementLayer);
				});
			}
			Grid.IsTileUnderConstruction[num] = true;
		}
	}

	// Token: 0x06002041 RID: 8257 RVA: 0x000B9D70 File Offset: 0x000B7F70
	private void UnmarkArea()
	{
		if (this.unmarked)
		{
			return;
		}
		this.unmarked = true;
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingDef def = this.building.Def;
		ObjectLayer objectLayer = (this.IsReplacementTile ? this.building.Def.ReplacementLayer : this.building.Def.ObjectLayer);
		def.UnmarkArea(num, this.building.Orientation, objectLayer, base.gameObject);
		if (def.IsTilePiece)
		{
			Grid.IsTileUnderConstruction[num] = false;
		}
		this.ClearPendingUproots();
	}

	// Token: 0x06002042 RID: 8258 RVA: 0x000B9E08 File Offset: 0x000B8008
	private void ClearPendingUproots()
	{
		foreach (Uprootable uprootable in this.pendingUproots)
		{
			if (!uprootable.IsNullOrDestroyed())
			{
				uprootable.Unsubscribe(-216549700, new Action<object>(this.OnSolidChangedOrDigDestroyed));
				uprootable.Unsubscribe(1198393204, new Action<object>(this.OnSolidChangedOrDigDestroyed));
				uprootable.ForceCancelUproot(null);
			}
		}
		this.pendingUproots.Clear();
	}

	// Token: 0x06002043 RID: 8259 RVA: 0x000B9E9C File Offset: 0x000B809C
	private void OnNearbyBuildingLayerChanged(object data)
	{
		this.hasLadderNearby = false;
		for (int i = this.ladderDetectionExtents.y; i < this.ladderDetectionExtents.y + this.ladderDetectionExtents.height; i++)
		{
			int num = Grid.OffsetCell(0, this.ladderDetectionExtents.x, i);
			if (Grid.IsValidCell(num))
			{
				GameObject gameObject = null;
				Grid.ObjectLayers[1].TryGetValue(num, out gameObject);
				if (gameObject != null && gameObject.GetComponent<Ladder>() != null)
				{
					this.hasLadderNearby = true;
					return;
				}
			}
		}
	}

	// Token: 0x06002044 RID: 8260 RVA: 0x000B9F28 File Offset: 0x000B8128
	private bool IsWire()
	{
		return this.building.Def.name.Contains("Wire");
	}

	// Token: 0x06002045 RID: 8261 RVA: 0x000B9F44 File Offset: 0x000B8144
	public bool IconConnectionAnimation(float delay, int connectionCount, string defName, string soundName)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		if (this.building.Def.Name.Contains(defName))
		{
			Building building = null;
			GameObject gameObject = Grid.Objects[num, 1];
			if (gameObject != null)
			{
				building = gameObject.GetComponent<Building>();
			}
			if (building != null)
			{
				bool flag = this.IsWire();
				int num2 = (flag ? building.GetPowerInputCell() : building.GetUtilityInputCell());
				int num3 = (flag ? num2 : building.GetUtilityOutputCell());
				if (num == num2 || num == num3)
				{
					BuildingCellVisualizer component = building.gameObject.GetComponent<BuildingCellVisualizer>();
					if (component != null && (flag ? ((component.addedPorts & (EntityCellVisualizer.Ports.PowerIn | EntityCellVisualizer.Ports.PowerOut)) > (EntityCellVisualizer.Ports)0) : ((component.addedPorts & (EntityCellVisualizer.Ports.GasIn | EntityCellVisualizer.Ports.GasOut | EntityCellVisualizer.Ports.LiquidIn | EntityCellVisualizer.Ports.LiquidOut | EntityCellVisualizer.Ports.SolidIn | EntityCellVisualizer.Ports.SolidOut)) > (EntityCellVisualizer.Ports)0)))
					{
						component.ConnectedEventWithDelay(delay, connectionCount, num, soundName);
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06002046 RID: 8262 RVA: 0x000BA024 File Offset: 0x000B8224
	protected override void OnCleanUp()
	{
		if (this.IsReplacementTile && this.building.Def.isKAnimTile)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			GameObject gameObject = Grid.Objects[num, (int)this.building.Def.ReplacementLayer];
			if (gameObject == base.gameObject && gameObject.GetComponent<SimCellOccupier>() != null)
			{
				World.Instance.blockTileRenderer.RemoveBlock(this.building.Def, this.IsReplacementTile, SimHashes.Void, num);
			}
		}
		GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.digPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.ladderPartitionerEntry);
		SaveLoadRoot component = base.GetComponent<SaveLoadRoot>();
		if (component != null)
		{
			SaveLoader.Instance.saveManager.Unregister(component);
		}
		if (this.fetchList != null)
		{
			this.fetchList.Cancel("Constructable destroyed");
		}
		this.UnmarkArea();
		HashSetPool<Uprootable, Constructable>.PooledHashSet pooledHashSet = HashSetPool<Uprootable, Constructable>.Allocate();
		foreach (int num2 in this.building.PlacementCells)
		{
			Diggable diggable = Diggable.GetDiggable(num2);
			if (diggable != null)
			{
				diggable.gameObject.DeleteObject();
			}
			Constructable.<OnCleanUp>g__TryAddUprootable|48_0(Grid.Objects[num2, 1], pooledHashSet);
			Constructable.<OnCleanUp>g__TryAddUprootable|48_0(Grid.Objects[num2, 5], pooledHashSet);
		}
		foreach (Uprootable uprootable in pooledHashSet)
		{
			uprootable.Unsubscribe(-216549700, new Action<object>(this.OnSolidChangedOrDigDestroyed));
			uprootable.Unsubscribe(1198393204, new Action<object>(this.OnSolidChangedOrDigDestroyed));
			uprootable.ForceCancelUproot(null);
		}
		pooledHashSet.Recycle();
		base.OnCleanUp();
	}

	// Token: 0x06002047 RID: 8263 RVA: 0x000BA218 File Offset: 0x000B8418
	private void OnDiggableReachabilityChanged(object data)
	{
		if (!this.IsReplacementTile)
		{
			int diggable_count = 0;
			int unreachable_count = 0;
			this.building.RunOnArea(delegate(int offset_cell)
			{
				Diggable diggable = Diggable.GetDiggable(offset_cell);
				if (diggable != null && diggable.isActiveAndEnabled)
				{
					int num = diggable_count + 1;
					diggable_count = num;
					if (!diggable.GetComponent<KPrefabID>().HasTag(GameTags.Reachable))
					{
						num = unreachable_count + 1;
						unreachable_count = num;
					}
				}
			});
			bool flag = unreachable_count > 0 && unreachable_count == diggable_count;
			if (flag != this.hasUnreachableDigs)
			{
				if (flag)
				{
					base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ConstructableDigUnreachable, null);
				}
				else
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ConstructableDigUnreachable, false);
				}
				this.hasUnreachableDigs = flag;
			}
		}
	}

	// Token: 0x06002048 RID: 8264 RVA: 0x000BA2C4 File Offset: 0x000B84C4
	private void PlaceDiggables()
	{
		if (this.waitForFetchesBeforeDigging && this.fetchList != null && !this.hasLadderNearby)
		{
			this.OnDiggableReachabilityChanged(null);
			return;
		}
		if (!this.solidPartitionerEntry.IsValid())
		{
			Extents validPlacementExtents = this.building.GetValidPlacementExtents();
			this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("Constructable.PlaceDiggables", base.gameObject, validPlacementExtents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChangedOrDigDestroyed));
			this.digPartitionerEntry = GameScenePartitioner.Instance.Add("Constructable.PlaceDiggables", base.gameObject, validPlacementExtents, GameScenePartitioner.Instance.digDestroyedLayer, new Action<object>(this.OnSolidChangedOrDigDestroyed));
		}
		bool digs_complete = true;
		if (!this.IsReplacementTile)
		{
			PrioritySetting master_priority = base.GetComponent<Prioritizable>().GetMasterPriority();
			HashSetPool<Uprootable, Constructable>.PooledHashSet uprootables = HashSetPool<Uprootable, Constructable>.Allocate();
			this.building.RunOnArea(delegate(int offset_cell)
			{
				Uprootable uprootable5;
				if (Diggable.IsDiggable(offset_cell))
				{
					digs_complete = false;
					Diggable diggable = Diggable.GetDiggable(offset_cell);
					if (diggable != null && !diggable.isActiveAndEnabled)
					{
						diggable.Unsubscribe(-1432940121, new Action<object>(this.OnDiggableReachabilityChanged));
						diggable = null;
					}
					if (diggable == null)
					{
						diggable = GameUtil.KInstantiate(Assets.GetPrefab(new Tag("DigPlacer")), Grid.SceneLayer.Move, null, 0).GetComponent<Diggable>();
						diggable.choreTypeIdHash = Db.Get().ChoreTypes.BuildDig.IdHash;
						diggable.gameObject.SetActive(true);
						diggable.transform.SetPosition(Grid.CellToPosCBC(offset_cell, Grid.SceneLayer.Move));
						Grid.Objects[offset_cell, 7] = diggable.gameObject;
						diggable.Subscribe(-1432940121, new Action<object>(this.OnDiggableReachabilityChanged));
					}
					diggable.GetComponent<Prioritizable>().SetMasterPriority(master_priority);
					RenderUtil.EnableRenderer(diggable.transform, false);
					SaveLoadRoot component = diggable.GetComponent<SaveLoadRoot>();
					if (component != null)
					{
						global::UnityEngine.Object.Destroy(component);
						return;
					}
				}
				else if (this.building.Def.ObjectLayer == ObjectLayer.Building && Uprootable.CanUproot(Grid.Objects[offset_cell, 5], out uprootable5))
				{
					uprootables.Add(uprootable5);
				}
			});
			if (uprootables.Count != 0)
			{
				digs_complete = false;
			}
			ListPool<Uprootable, Constructable>.PooledList pooledList = ListPool<Uprootable, Constructable>.Allocate();
			ListPool<Uprootable, Constructable>.PooledList pooledList2 = ListPool<Uprootable, Constructable>.Allocate();
			foreach (Uprootable uprootable in this.pendingUproots)
			{
				if (uprootable.IsNullOrDestroyed())
				{
					pooledList2.Add(uprootable);
				}
				else if (!uprootables.Contains(uprootable))
				{
					pooledList.Add(uprootable);
				}
			}
			foreach (Uprootable uprootable2 in pooledList)
			{
				uprootable2.Unsubscribe(-216549700, new Action<object>(this.OnSolidChangedOrDigDestroyed));
				uprootable2.Unsubscribe(1198393204, new Action<object>(this.OnSolidChangedOrDigDestroyed));
				this.pendingUproots.Remove(uprootable2);
			}
			pooledList.Recycle();
			foreach (Uprootable uprootable3 in pooledList2)
			{
				this.pendingUproots.Remove(uprootable3);
			}
			pooledList2.Recycle();
			foreach (Uprootable uprootable4 in uprootables)
			{
				bool flag = this.pendingUproots.Add(uprootable4);
				uprootable4.choreTypeIdHash = Db.Get().ChoreTypes.BuildUproot.IdHash;
				uprootable4.MarkForUproot(true);
				if (flag)
				{
					uprootable4.Subscribe(-216549700, new Action<object>(this.OnSolidChangedOrDigDestroyed));
					uprootable4.Subscribe(1198393204, new Action<object>(this.OnSolidChangedOrDigDestroyed));
				}
			}
			uprootables.Recycle();
			this.OnDiggableReachabilityChanged(null);
		}
		bool flag2 = this.building.Def.IsValidBuildLocation(base.gameObject, base.transform.GetPosition(), this.building.Orientation, this.IsReplacementTile);
		if (flag2)
		{
			this.notifier.Remove(this.invalidLocation);
		}
		else
		{
			this.notifier.Add(this.invalidLocation, "");
		}
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.InvalidBuildingLocation, !flag2, this);
		bool flag3 = digs_complete && flag2 && this.fetchList == null;
		if (flag3 && this.buildChore == null)
		{
			this.buildChore = new WorkChore<Constructable>(Db.Get().ChoreTypes.Build, this, null, true, new Action<Chore>(this.UpdateBuildState), new Action<Chore>(this.UpdateBuildState), new Action<Chore>(this.UpdateBuildState), true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			this.UpdateBuildState(this.buildChore);
			return;
		}
		if (!flag3 && this.buildChore != null)
		{
			this.buildChore.Cancel("Need to dig");
			this.buildChore = null;
		}
	}

	// Token: 0x06002049 RID: 8265 RVA: 0x000BA6EC File Offset: 0x000B88EC
	private void OnFetchListComplete()
	{
		this.fetchList = null;
		this.PlaceDiggables();
		this.ClearMaterialNeeds();
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x000BA704 File Offset: 0x000B8904
	private void ClearMaterialNeeds()
	{
		if (this.materialNeedsCleared)
		{
			return;
		}
		foreach (Recipe.Ingredient ingredient in this.Recipe.GetAllIngredients(this.SelectedElementsTags))
		{
			MaterialNeeds.UpdateNeed(ingredient.tag, -ingredient.amount, base.gameObject.GetMyWorldId());
		}
		this.materialNeedsCleared = true;
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x000BA762 File Offset: 0x000B8962
	private void OnSolidChangedOrDigDestroyed(object data)
	{
		if (this == null || this.finished)
		{
			return;
		}
		this.PlaceDiggables();
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x000BA77C File Offset: 0x000B897C
	private void UpdateBuildState(Chore chore)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		StatusItem statusItem = (chore.InProgress() ? Db.Get().BuildingStatusItems.UnderConstruction : Db.Get().BuildingStatusItems.UnderConstructionNoWorker);
		component.SetStatusItem(Db.Get().StatusItemCategories.Main, statusItem, null);
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x000BA7D0 File Offset: 0x000B89D0
	[OnDeserialized]
	internal void OnDeserialized()
	{
		if (this.ids != null)
		{
			this.selectedElements = new Element[this.ids.Length];
			for (int i = 0; i < this.ids.Length; i++)
			{
				this.selectedElements[i] = ElementLoader.FindElementByHash((SimHashes)this.ids[i]);
			}
			if (this.selectedElementsTags == null)
			{
				this.selectedElementsTags = new Tag[this.ids.Length];
				for (int j = 0; j < this.ids.Length; j++)
				{
					this.selectedElementsTags[j] = ElementLoader.FindElementByHash((SimHashes)this.ids[j]).tag;
				}
			}
			global::Debug.Assert(this.selectedElements.Length == this.selectedElementsTags.Length);
			for (int k = 0; k < this.selectedElements.Length; k++)
			{
				global::Debug.Assert(this.selectedElements[k].tag == this.SelectedElementsTags[k]);
			}
		}
	}

	// Token: 0x0600204E RID: 8270 RVA: 0x000BA8BC File Offset: 0x000B8ABC
	private void OnReachableChanged(object data)
	{
		KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
		if ((bool)data)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ConstructionUnreachable, false);
			if (component != null)
			{
				component.TintColour = Game.Instance.uiColours.Build.validLocation;
				return;
			}
		}
		else
		{
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ConstructionUnreachable, this);
			if (component != null)
			{
				component.TintColour = Game.Instance.uiColours.Build.unreachable;
			}
		}
	}

	// Token: 0x0600204F RID: 8271 RVA: 0x000BA964 File Offset: 0x000B8B64
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_cancel", UI.USERMENUACTIONS.CANCELCONSTRUCTION.NAME, new global::System.Action(this.OnPressCancel), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CANCELCONSTRUCTION.TOOLTIP, true), 1f);
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x000BA9BE File Offset: 0x000B8BBE
	private void OnPressCancel()
	{
		base.gameObject.Trigger(2127324410, null);
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x000BA9D1 File Offset: 0x000B8BD1
	private void OnCancel(object data = null)
	{
		DetailsScreen.Instance.Show(false);
		this.ClearMaterialNeeds();
		this.ClearPendingUproots();
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x000BAABC File Offset: 0x000B8CBC
	[CompilerGenerated]
	internal static void <OnCleanUp>g__TryAddUprootable|48_0(GameObject plant, HashSet<Uprootable> _uprootables)
	{
		if (plant == null)
		{
			return;
		}
		Uprootable component = plant.GetComponent<Uprootable>();
		if (component == null)
		{
			return;
		}
		_uprootables.Add(component);
	}

	// Token: 0x040012B1 RID: 4785
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x040012B2 RID: 4786
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x040012B3 RID: 4787
	[MyCmpAdd]
	private Prioritizable prioritizable;

	// Token: 0x040012B4 RID: 4788
	[MyCmpReq]
	private Building building;

	// Token: 0x040012B5 RID: 4789
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x040012B6 RID: 4790
	private Notification invalidLocation;

	// Token: 0x040012B7 RID: 4791
	private float initialTemperature = -1f;

	// Token: 0x040012B8 RID: 4792
	[Serialize]
	private bool isPrioritized;

	// Token: 0x040012B9 RID: 4793
	private FetchList2 fetchList;

	// Token: 0x040012BA RID: 4794
	private Chore buildChore;

	// Token: 0x040012BB RID: 4795
	private bool materialNeedsCleared;

	// Token: 0x040012BC RID: 4796
	private bool hasUnreachableDigs;

	// Token: 0x040012BD RID: 4797
	private bool finished;

	// Token: 0x040012BE RID: 4798
	private bool unmarked;

	// Token: 0x040012BF RID: 4799
	public bool isDiggingRequired = true;

	// Token: 0x040012C0 RID: 4800
	private bool waitForFetchesBeforeDigging;

	// Token: 0x040012C1 RID: 4801
	private bool hasLadderNearby;

	// Token: 0x040012C2 RID: 4802
	private Extents ladderDetectionExtents;

	// Token: 0x040012C3 RID: 4803
	[Serialize]
	public bool IsReplacementTile;

	// Token: 0x040012C4 RID: 4804
	private HandleVector<int>.Handle solidPartitionerEntry;

	// Token: 0x040012C5 RID: 4805
	private HandleVector<int>.Handle digPartitionerEntry;

	// Token: 0x040012C6 RID: 4806
	private HandleVector<int>.Handle ladderPartitionerEntry;

	// Token: 0x040012C7 RID: 4807
	private readonly HashSet<Uprootable> pendingUproots = new HashSet<Uprootable>();

	// Token: 0x040012C8 RID: 4808
	private LoggerFSS log = new LoggerFSS("Constructable", 35);

	// Token: 0x040012C9 RID: 4809
	[Serialize]
	private Tag[] selectedElementsTags;

	// Token: 0x040012CA RID: 4810
	private Element[] selectedElements;

	// Token: 0x040012CB RID: 4811
	[Serialize]
	private int[] ids;

	// Token: 0x040012CC RID: 4812
	private static readonly EventSystem.IntraObjectHandler<Constructable> OnReachableChangedDelegate = new EventSystem.IntraObjectHandler<Constructable>(delegate(Constructable component, object data)
	{
		component.OnReachableChanged(data);
	});

	// Token: 0x040012CD RID: 4813
	private static readonly EventSystem.IntraObjectHandler<Constructable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Constructable>(delegate(Constructable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x040012CE RID: 4814
	private static readonly EventSystem.IntraObjectHandler<Constructable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Constructable>(delegate(Constructable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x020013CB RID: 5067
	public struct ReplaceCallbackParameters
	{
		// Token: 0x04006AAD RID: 27309
		public ObjectLayer TileLayer;

		// Token: 0x04006AAE RID: 27310
		public WorkerBase Worker;
	}
}
