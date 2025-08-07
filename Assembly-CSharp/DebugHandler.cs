using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x02000895 RID: 2197
public class DebugHandler : IInputHandler
{
	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x001510F4 File Offset: 0x0014F2F4
	// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x001510FB File Offset: 0x0014F2FB
	public static bool NotificationsDisabled { get; private set; }

	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x00151103 File Offset: 0x0014F303
	// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x0015110A File Offset: 0x0014F30A
	public static bool enabled { get; private set; }

	// Token: 0x06003CBA RID: 15546 RVA: 0x00151114 File Offset: 0x0014F314
	public DebugHandler()
	{
		DebugHandler.enabled = File.Exists(Path.Combine(Application.dataPath, "debug_enable.txt"));
		DebugHandler.enabled = DebugHandler.enabled || File.Exists(Path.Combine(Application.dataPath, "../debug_enable.txt"));
		DebugHandler.enabled = DebugHandler.enabled || GenericGameSettings.instance.debugEnable;
	}

	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x06003CBB RID: 15547 RVA: 0x0015117C File Offset: 0x0014F37C
	public string handlerName
	{
		get
		{
			return "DebugHandler";
		}
	}

	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x06003CBC RID: 15548 RVA: 0x00151183 File Offset: 0x0014F383
	// (set) Token: 0x06003CBD RID: 15549 RVA: 0x0015118B File Offset: 0x0014F38B
	public KInputHandler inputHandler { get; set; }

	// Token: 0x06003CBE RID: 15550 RVA: 0x00151194 File Offset: 0x0014F394
	public static int GetMouseCell()
	{
		Vector3 mousePos = KInputManager.GetMousePos();
		mousePos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
		return Grid.PosToCell(Camera.main.ScreenToWorldPoint(mousePos));
	}

	// Token: 0x06003CBF RID: 15551 RVA: 0x001511DC File Offset: 0x0014F3DC
	public static Vector3 GetMousePos()
	{
		Vector3 mousePos = KInputManager.GetMousePos();
		mousePos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
		return Camera.main.ScreenToWorldPoint(mousePos);
	}

	// Token: 0x06003CC0 RID: 15552 RVA: 0x0015121C File Offset: 0x0014F41C
	private void SpawnMinion(bool addAtmoSuit = false)
	{
		if (Immigration.Instance == null)
		{
			return;
		}
		if (!Grid.IsValidBuildingCell(DebugHandler.GetMouseCell()))
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.DEBUG_TOOLS.INVALID_LOCATION, null, DebugHandler.GetMousePos(), 1.5f, false, true);
			return;
		}
		MinionStartingStats minionStartingStats = new MinionStartingStats(false, null, null, true);
		GameObject prefab = Assets.GetPrefab(BaseMinionConfig.GetMinionIDForModel(minionStartingStats.personality.model));
		GameObject gameObject = Util.KInstantiate(prefab, null, null);
		gameObject.name = prefab.name;
		Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
		Vector3 vector = Grid.CellToPosCBC(DebugHandler.GetMouseCell(), Grid.SceneLayer.Move);
		gameObject.transform.SetLocalPosition(vector);
		gameObject.SetActive(true);
		minionStartingStats.Apply(gameObject);
		if (addAtmoSuit)
		{
			gameObject.Subscribe(1589886948, new Action<object>(this.AddAtmosuitAfterSpawn));
		}
		gameObject.GetMyWorld().SetDupeVisited();
	}

	// Token: 0x06003CC1 RID: 15553 RVA: 0x00151304 File Offset: 0x0014F504
	private void AddAtmosuitAfterSpawn(object o)
	{
		GameObject gameObject = (GameObject)o;
		Vector3 localPosition = gameObject.transform.localPosition;
		GameObject gameObject2 = GameUtil.KInstantiate(Assets.GetPrefab("Atmo_Suit"), localPosition, Grid.SceneLayer.Creatures, null, 0);
		gameObject2.SetActive(true);
		SuitTank component = gameObject2.GetComponent<SuitTank>();
		GameObject gameObject3 = GameUtil.KInstantiate(Assets.GetPrefab(GameTags.Oxygen), localPosition, Grid.SceneLayer.Ore, null, 0);
		gameObject3.GetComponent<PrimaryElement>().Units = component.capacity;
		gameObject3.SetActive(true);
		component.storage.Store(gameObject3, true, false, true, false);
		Equippable component2 = gameObject2.GetComponent<Equippable>();
		gameObject.GetComponent<MinionIdentity>().ValidateProxy();
		Equipment component3 = gameObject.GetComponent<MinionIdentity>().assignableProxy.Get().GetComponent<Equipment>();
		component2.Assign(component3.GetComponent<IAssignableIdentity>());
		component2.isEquipped = true;
		gameObject2.GetComponent<EquippableWorkable>().CancelChore("Debug Handler");
		gameObject.Unsubscribe(1589886948, new Action<object>(this.AddAtmosuitAfterSpawn));
	}

	// Token: 0x06003CC2 RID: 15554 RVA: 0x001513EA File Offset: 0x0014F5EA
	public static void SetDebugEnabled(bool debugEnabled)
	{
		DebugHandler.enabled = debugEnabled;
	}

	// Token: 0x06003CC3 RID: 15555 RVA: 0x001513F2 File Offset: 0x0014F5F2
	public static void ToggleDisableNotifications()
	{
		DebugHandler.NotificationsDisabled = !DebugHandler.NotificationsDisabled;
	}

	// Token: 0x06003CC4 RID: 15556 RVA: 0x00151404 File Offset: 0x0014F604
	private string GetScreenshotFileName()
	{
		string activeSaveFilePath = SaveLoader.GetActiveSaveFilePath();
		string text = Path.Combine(Path.GetDirectoryName(activeSaveFilePath), "screenshot");
		string fileName = Path.GetFileName(activeSaveFilePath);
		Directory.CreateDirectory(text);
		string text2 = string.Concat(new string[]
		{
			Path.GetFileNameWithoutExtension(fileName),
			"_",
			GameClock.Instance.GetCycle().ToString(),
			"_",
			global::System.DateTime.Now.ToString("yyyy-MM-dd_HH\\hmm\\mss\\s"),
			".png"
		});
		return Path.Combine(text, text2);
	}

	// Token: 0x06003CC5 RID: 15557 RVA: 0x00151494 File Offset: 0x0014F694
	public void OnKeyDown(KButtonEvent e)
	{
		if (!DebugHandler.enabled)
		{
			return;
		}
		if (e.TryConsume(global::Action.DebugSpawnMinion))
		{
			this.SpawnMinion(false);
		}
		else if (e.TryConsume(global::Action.DebugSpawnMinionAtmoSuit))
		{
			this.SpawnMinion(true);
		}
		else if (e.TryConsume(global::Action.DebugCheerEmote))
		{
			for (int i = 0; i < Components.MinionIdentities.Count; i++)
			{
				new EmoteChore(Components.MinionIdentities[i].GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, "anim_cheer_kanim", new HashedString[] { "cheer_pre", "cheer_loop", "cheer_pst" }, null);
				new EmoteChore(Components.MinionIdentities[i].GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, "anim_cheer_kanim", new HashedString[] { "cheer_pre", "cheer_loop", "cheer_pst" }, null);
			}
		}
		else if (e.TryConsume(global::Action.DebugSpawnStressTest))
		{
			for (int j = 0; j < 60; j++)
			{
				this.SpawnMinion(false);
			}
		}
		else if (e.TryConsume(global::Action.DebugSuperTestMode))
		{
			if (!this.superTestMode)
			{
				Time.timeScale = 15f;
				this.superTestMode = true;
			}
			else
			{
				Time.timeScale = 1f;
				this.superTestMode = false;
			}
		}
		else if (e.TryConsume(global::Action.DebugUltraTestMode))
		{
			if (!this.ultraTestMode)
			{
				Time.timeScale = 30f;
				this.ultraTestMode = true;
			}
			else
			{
				Time.timeScale = 1f;
				this.ultraTestMode = false;
			}
		}
		else if (e.TryConsume(global::Action.DebugSlowTestMode))
		{
			if (!this.slowTestMode)
			{
				Time.timeScale = 0.06f;
				this.slowTestMode = true;
			}
			else
			{
				Time.timeScale = 1f;
				this.slowTestMode = false;
			}
		}
		else if (e.TryConsume(global::Action.DebugDig) && Game.Instance != null)
		{
			SimMessages.Dig(DebugHandler.GetMouseCell(), -1, false);
		}
		else if (e.TryConsume(global::Action.DebugToggleFastWorkers) && Game.Instance != null)
		{
			Game.Instance.FastWorkersModeActive = !Game.Instance.FastWorkersModeActive;
		}
		else if (e.TryConsume(global::Action.DebugInstantBuildMode) && Game.Instance != null)
		{
			DebugHandler.InstantBuildMode = !DebugHandler.InstantBuildMode;
			InterfaceTool.ToggleConfig(global::Action.DebugInstantBuildMode);
			Game.Instance.Trigger(1557339983, null);
			if (PlanScreen.Instance != null)
			{
				PlanScreen.Instance.Refresh();
			}
			if (BuildMenu.Instance != null)
			{
				BuildMenu.Instance.Refresh();
			}
			if (OverlayMenu.Instance != null)
			{
				OverlayMenu.Instance.Refresh();
			}
			if (ConsumerManager.instance != null)
			{
				ConsumerManager.instance.RefreshDiscovered(null);
			}
			if (ManagementMenu.Instance != null)
			{
				ManagementMenu.Instance.Refresh();
			}
			if (SelectTool.Instance.selected != null)
			{
				DetailsScreen.Instance.Refresh(SelectTool.Instance.selected.gameObject);
			}
			Game.Instance.Trigger(1594320620, "all_the_things");
		}
		else if (e.TryConsume(global::Action.DebugExplosion) && Game.Instance != null)
		{
			Vector3 mousePos = KInputManager.GetMousePos();
			mousePos.z = -Camera.main.transform.GetPosition().z - Grid.CellSizeInMeters;
			GameUtil.CreateExplosion(Camera.main.ScreenToWorldPoint(mousePos));
		}
		else if (e.TryConsume(global::Action.DebugLockCursor) && GenericGameSettings.instance != null)
		{
			if (GenericGameSettings.instance.developerDebugEnable)
			{
				KInputManager.isMousePosLocked = !KInputManager.isMousePosLocked;
				KInputManager.lockedMousePos = KInputManager.GetMousePos();
			}
		}
		else
		{
			if (e.TryConsume(global::Action.DebugDiscoverAllElements))
			{
				if (!(DiscoveredResources.Instance != null))
				{
					goto IL_0CE3;
				}
				using (List<Element>.Enumerator enumerator = ElementLoader.elements.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Element element = enumerator.Current;
						DiscoveredResources.Instance.Discover(element.tag, element.GetMaterialCategoryTag());
					}
					goto IL_0CE3;
				}
			}
			if (e.TryConsume(global::Action.DebugToggleUI))
			{
				DebugHandler.ToggleScreenshotMode();
			}
			else if (e.TryConsume(global::Action.SreenShot1x))
			{
				ScreenCapture.CaptureScreenshot(this.GetScreenshotFileName(), 1);
			}
			else if (e.TryConsume(global::Action.SreenShot2x))
			{
				ScreenCapture.CaptureScreenshot(this.GetScreenshotFileName(), 2);
			}
			else if (e.TryConsume(global::Action.SreenShot8x))
			{
				ScreenCapture.CaptureScreenshot(this.GetScreenshotFileName(), 8);
			}
			else if (e.TryConsume(global::Action.SreenShot32x))
			{
				ScreenCapture.CaptureScreenshot(this.GetScreenshotFileName(), 32);
			}
			else if (e.TryConsume(global::Action.DebugCellInfo))
			{
				DebugHandler.DebugCellInfo = !DebugHandler.DebugCellInfo;
			}
			else if (e.TryConsume(global::Action.DebugToggle))
			{
				if (Game.Instance != null)
				{
					SaveGame.Instance.worldGenSpawner.SpawnEverything();
				}
				InterfaceTool.ToggleConfig(global::Action.DebugToggle);
				if (DebugPaintElementScreen.Instance != null)
				{
					bool activeSelf = DebugPaintElementScreen.Instance.gameObject.activeSelf;
					DebugPaintElementScreen.Instance.gameObject.SetActive(!activeSelf);
					if (DebugElementMenu.Instance && DebugElementMenu.Instance.root.activeSelf)
					{
						DebugElementMenu.Instance.root.SetActive(false);
					}
					DebugBaseTemplateButton.Instance.gameObject.SetActive(!activeSelf);
					PropertyTextures.FogOfWarScale = (float)((!activeSelf) ? 1 : 0);
					if (CameraController.Instance != null)
					{
						CameraController.Instance.EnableFreeCamera(!activeSelf);
					}
					DebugHandler.RevealFogOfWar = !DebugHandler.RevealFogOfWar;
					Game.Instance.Trigger(-1991583975, null);
				}
			}
			else if (e.TryConsume(global::Action.DebugCollectGarbage))
			{
				GC.Collect();
			}
			else if (e.TryConsume(global::Action.DebugInvincible))
			{
				DebugHandler.InvincibleMode = !DebugHandler.InvincibleMode;
			}
			else if (e.TryConsume(global::Action.DebugVisualTest) && Scenario.Instance != null)
			{
				Scenario.Instance.SetupVisualTest();
			}
			else if (e.TryConsume(global::Action.DebugGameplayTest) && Scenario.Instance != null)
			{
				Scenario.Instance.SetupGameplayTest();
			}
			else if (e.TryConsume(global::Action.DebugElementTest) && Scenario.Instance != null)
			{
				Scenario.Instance.SetupElementTest();
			}
			else if (e.TryConsume(global::Action.ToggleProfiler) && Game.Instance != null)
			{
				Sim.SIM_HandleMessage(-409964931, 0, null);
			}
			else if (e.TryConsume(global::Action.DebugRefreshNavCell) && Pathfinding.Instance != null)
			{
				Pathfinding.Instance.RefreshNavCell(DebugHandler.GetMouseCell());
			}
			else if (e.TryConsume(global::Action.DebugToggleSelectInEditor))
			{
				DebugHandler.SetSelectInEditor(!DebugHandler.SelectInEditor);
			}
			else
			{
				if (e.TryConsume(global::Action.DebugGotoTarget) && Game.Instance != null)
				{
					global::Debug.Log("Debug GoTo");
					Game.Instance.Trigger(775300118, null);
					using (List<Brain>.Enumerator enumerator2 = Components.Brains.Items.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							Brain brain = enumerator2.Current;
							DebugGoToMonitor.Instance smi = brain.GetSMI<DebugGoToMonitor.Instance>();
							if (smi != null)
							{
								smi.GoToCursor();
							}
							CreatureDebugGoToMonitor.Instance smi2 = brain.GetSMI<CreatureDebugGoToMonitor.Instance>();
							if (smi2 != null)
							{
								smi2.GoToCursor();
							}
						}
						goto IL_0CE3;
					}
				}
				if (e.TryConsume(global::Action.DebugTeleport))
				{
					if (SelectTool.Instance == null)
					{
						return;
					}
					KSelectable selected = SelectTool.Instance.selected;
					if (selected != null)
					{
						Navigator component = selected.GetComponent<Navigator>();
						if (component != null && component.IsMoving())
						{
							component.Stop(false, true);
						}
						int mouseCell = DebugHandler.GetMouseCell();
						if (!Grid.IsValidBuildingCell(mouseCell))
						{
							PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.DEBUG_TOOLS.INVALID_LOCATION, null, DebugHandler.GetMousePos(), 1.5f, false, true);
							return;
						}
						selected.transform.SetPosition(Grid.CellToPosCBC(mouseCell, Grid.SceneLayer.Move));
					}
				}
				else if (!e.TryConsume(global::Action.DebugPlace) && (!e.TryConsume(global::Action.DebugSelectMaterial) || !(Camera.main != null)))
				{
					if (e.TryConsume(global::Action.DebugNotification) && GenericGameSettings.instance != null && Tutorial.Instance != null)
					{
						if (GenericGameSettings.instance.developerDebugEnable)
						{
							Tutorial.Instance.DebugNotification();
						}
					}
					else if (e.TryConsume(global::Action.DebugNotificationMessage) && GenericGameSettings.instance != null && Tutorial.Instance != null)
					{
						if (GenericGameSettings.instance.developerDebugEnable)
						{
							Tutorial.Instance.DebugNotificationMessage();
						}
					}
					else if (e.TryConsume(global::Action.DebugSuperSpeed))
					{
						if (SpeedControlScreen.Instance != null)
						{
							SpeedControlScreen.Instance.ToggleRidiculousSpeed();
						}
					}
					else if (e.TryConsume(global::Action.DebugGameStep))
					{
						if (SpeedControlScreen.Instance != null)
						{
							SpeedControlScreen.Instance.DebugStepFrame();
						}
					}
					else if (e.TryConsume(global::Action.DebugSimStep) && Game.Instance != null)
					{
						Game.Instance.ForceSimStep();
					}
					else if (e.TryConsume(global::Action.DebugToggleMusic))
					{
						AudioDebug.Get().ToggleMusic();
					}
					else if (e.TryConsume(global::Action.DebugTileTest) && Scenario.Instance != null)
					{
						Scenario.Instance.SetupTileTest();
					}
					else if (e.TryConsume(global::Action.DebugForceLightEverywhere) && PropertyTextures.instance != null)
					{
						PropertyTextures.instance.ForceLightEverywhere = !PropertyTextures.instance.ForceLightEverywhere;
					}
					else if (e.TryConsume(global::Action.DebugPathFinding))
					{
						DebugHandler.DebugPathFinding = !DebugHandler.DebugPathFinding;
						global::Debug.Log("DebugPathFinding=" + DebugHandler.DebugPathFinding.ToString());
					}
					else if (!e.TryConsume(global::Action.DebugFocus))
					{
						if (e.TryConsume(global::Action.DebugReportBug) && GenericGameSettings.instance != null)
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								int num = 0;
								string validSaveFilename;
								for (;;)
								{
									validSaveFilename = SaveScreen.GetValidSaveFilename("bug_report_savefile_" + num.ToString());
									if (!File.Exists(validSaveFilename))
									{
										break;
									}
									num++;
								}
								if (SaveLoader.Instance != null)
								{
									SaveLoader.Instance.Save(validSaveFilename, false, false);
								}
								KCrashReporter.ReportBug("Bug Report", GameObject.Find("ScreenSpaceOverlayCanvas"));
							}
							else
							{
								global::Debug.Log("Debug crash keys are not enabled.");
							}
						}
						else if (e.TryConsume(global::Action.DebugTriggerException) && GenericGameSettings.instance != null)
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								throw new ArgumentException("My test exception");
							}
						}
						else if (e.TryConsume(global::Action.DebugTriggerError) && GenericGameSettings.instance != null)
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								global::UnityEngine.Debug.Log("trigger error");
								KCrashReporter.disableDeduping = true;
								global::Debug.LogError("Oooops! Testing error!");
							}
						}
						else if (e.TryConsume(global::Action.DebugDumpGCRoots) && GenericGameSettings.instance != null)
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								GarbageProfiler.DebugDumpRootItems();
							}
						}
						else if (e.TryConsume(global::Action.DebugDumpGarbageReferences) && GenericGameSettings.instance != null)
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								GarbageProfiler.DebugDumpGarbageStats();
							}
						}
						else if (e.TryConsume(global::Action.DebugDumpEventData) && GenericGameSettings.instance != null)
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								KObjectManager.Instance.DumpEventData();
							}
						}
						else if (e.TryConsume(global::Action.DebugDumpSceneParitionerLeakData) && GenericGameSettings.instance != null)
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
							}
						}
						else if (e.TryConsume(global::Action.DebugCrashSim) && GenericGameSettings.instance != null)
						{
							if (GenericGameSettings.instance.developerDebugEnable)
							{
								new Thread(delegate
								{
									Sim.SIM_DebugCrash();
								}).Start();
							}
						}
						else if (e.TryConsume(global::Action.DebugNextCall))
						{
							DebugHandler.DebugNextCall = true;
						}
						else if (e.TryConsume(global::Action.DebugTogglePersonalPriorityComparison))
						{
							Chore.ENABLE_PERSONAL_PRIORITIES = !Chore.ENABLE_PERSONAL_PRIORITIES;
						}
						else if (e.TryConsume(global::Action.DebugToggleClusterFX) && CameraController.Instance != null)
						{
							CameraController.Instance.ToggleClusterFX();
						}
					}
				}
			}
		}
		IL_0CE3:
		if (e.Consumed && Game.Instance != null)
		{
			Game.Instance.debugWasUsed = true;
			KCrashReporter.debugWasUsed = true;
		}
	}

	// Token: 0x06003CC6 RID: 15558 RVA: 0x001521C8 File Offset: 0x001503C8
	public static void SetSelectInEditor(bool select_in_editor)
	{
	}

	// Token: 0x06003CC7 RID: 15559 RVA: 0x001521CC File Offset: 0x001503CC
	public static void ToggleScreenshotMode()
	{
		DebugHandler.ScreenshotMode = !DebugHandler.ScreenshotMode;
		DebugHandler.UpdateUI();
		if (CameraController.Instance != null)
		{
			CameraController.Instance.EnableFreeCamera(DebugHandler.ScreenshotMode);
		}
		if (KScreenManager.Instance != null)
		{
			KScreenManager.Instance.DisableInput(DebugHandler.ScreenshotMode);
		}
	}

	// Token: 0x06003CC8 RID: 15560 RVA: 0x00152224 File Offset: 0x00150424
	public static void SetTimelapseMode(bool enabled, int world_id = 0)
	{
		DebugHandler.TimelapseMode = enabled;
		if (enabled)
		{
			DebugHandler.activeWorldBeforeOverride = ClusterManager.Instance.activeWorldId;
			ClusterManager.Instance.TimelapseModeOverrideActiveWorld(world_id);
		}
		else
		{
			ClusterManager.Instance.TimelapseModeOverrideActiveWorld(DebugHandler.activeWorldBeforeOverride);
		}
		World.Instance.zoneRenderData.OnActiveWorldChanged();
		DebugHandler.UpdateUI();
	}

	// Token: 0x06003CC9 RID: 15561 RVA: 0x0015227C File Offset: 0x0015047C
	private static void UpdateUI()
	{
		if (GameScreenManager.Instance == null)
		{
			return;
		}
		DebugHandler.HideUI = DebugHandler.TimelapseMode || DebugHandler.ScreenshotMode;
		float num = (DebugHandler.HideUI ? 0f : 1f);
		GameScreenManager.Instance.ssHoverTextCanvas.GetComponent<CanvasGroup>().alpha = num;
		GameScreenManager.Instance.ssCameraCanvas.GetComponent<CanvasGroup>().alpha = num;
		GameScreenManager.Instance.ssOverlayCanvas.GetComponent<CanvasGroup>().alpha = num;
		GameScreenManager.Instance.worldSpaceCanvas.GetComponent<CanvasGroup>().alpha = num;
		GameScreenManager.Instance.screenshotModeCanvas.GetComponent<CanvasGroup>().alpha = 1f - num;
	}

	// Token: 0x04002530 RID: 9520
	public static bool InstantBuildMode;

	// Token: 0x04002531 RID: 9521
	public static bool InvincibleMode;

	// Token: 0x04002532 RID: 9522
	public static bool SelectInEditor;

	// Token: 0x04002533 RID: 9523
	public static bool DebugPathFinding;

	// Token: 0x04002534 RID: 9524
	public static bool ScreenshotMode;

	// Token: 0x04002535 RID: 9525
	public static bool TimelapseMode;

	// Token: 0x04002536 RID: 9526
	public static bool HideUI;

	// Token: 0x04002537 RID: 9527
	public static bool DebugCellInfo;

	// Token: 0x04002538 RID: 9528
	public static bool DebugNextCall;

	// Token: 0x04002539 RID: 9529
	public static bool RevealFogOfWar;

	// Token: 0x0400253D RID: 9533
	private bool superTestMode;

	// Token: 0x0400253E RID: 9534
	private bool ultraTestMode;

	// Token: 0x0400253F RID: 9535
	private bool slowTestMode;

	// Token: 0x04002540 RID: 9536
	private static int activeWorldBeforeOverride = -1;

	// Token: 0x02001861 RID: 6241
	public enum PaintMode
	{
		// Token: 0x040078CB RID: 30923
		None,
		// Token: 0x040078CC RID: 30924
		Element,
		// Token: 0x040078CD RID: 30925
		Hot,
		// Token: 0x040078CE RID: 30926
		Cold
	}
}
