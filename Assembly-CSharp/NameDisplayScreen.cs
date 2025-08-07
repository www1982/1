using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D76 RID: 3446
public class NameDisplayScreen : KScreen
{
	// Token: 0x06006B16 RID: 27414 RVA: 0x0028717B File Offset: 0x0028537B
	public static void DestroyInstance()
	{
		NameDisplayScreen.Instance = null;
	}

	// Token: 0x06006B17 RID: 27415 RVA: 0x00287183 File Offset: 0x00285383
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		NameDisplayScreen.Instance = this;
	}

	// Token: 0x06006B18 RID: 27416 RVA: 0x00287191 File Offset: 0x00285391
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Health.Register(new Action<Health>(this.OnHealthAdded), null);
		Components.Equipment.Register(new Action<Equipment>(this.OnEquipmentAdded), null);
		this.BindOnOverlayChange();
	}

	// Token: 0x06006B19 RID: 27417 RVA: 0x002871D0 File Offset: 0x002853D0
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.isOverlayChangeBound && OverlayScreen.Instance != null)
		{
			OverlayScreen instance = OverlayScreen.Instance;
			instance.OnOverlayChanged = (Action<HashedString>)Delegate.Remove(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
			this.isOverlayChangeBound = false;
		}
	}

	// Token: 0x06006B1A RID: 27418 RVA: 0x00287228 File Offset: 0x00285428
	private void BindOnOverlayChange()
	{
		if (this.isOverlayChangeBound)
		{
			return;
		}
		if (OverlayScreen.Instance != null)
		{
			OverlayScreen instance = OverlayScreen.Instance;
			instance.OnOverlayChanged = (Action<HashedString>)Delegate.Combine(instance.OnOverlayChanged, new Action<HashedString>(this.OnOverlayChanged));
			this.isOverlayChangeBound = true;
		}
	}

	// Token: 0x06006B1B RID: 27419 RVA: 0x00287278 File Offset: 0x00285478
	public void RemoveWorldEntries(int worldId)
	{
		this.entries.RemoveAll((NameDisplayScreen.Entry entry) => entry.world_go.IsNullOrDestroyed() || entry.world_go.GetMyWorldId() == worldId);
	}

	// Token: 0x06006B1C RID: 27420 RVA: 0x002872AA File Offset: 0x002854AA
	private void OnOverlayChanged(HashedString new_mode)
	{
		HashedString hashedString = this.lastKnownOverlayID;
		this.lastKnownOverlayID = new_mode;
		this.nameDisplayCanvas.enabled = this.lastKnownOverlayID == OverlayModes.None.ID;
	}

	// Token: 0x06006B1D RID: 27421 RVA: 0x002872D5 File Offset: 0x002854D5
	private void OnHealthAdded(Health health)
	{
		this.RegisterComponent(health.gameObject, health, false);
	}

	// Token: 0x06006B1E RID: 27422 RVA: 0x002872E8 File Offset: 0x002854E8
	private void OnEquipmentAdded(Equipment equipment)
	{
		MinionAssignablesProxy component = equipment.GetComponent<MinionAssignablesProxy>();
		GameObject targetGameObject = component.GetTargetGameObject();
		if (targetGameObject)
		{
			this.RegisterComponent(targetGameObject, equipment, false);
			return;
		}
		global::Debug.LogWarningFormat("OnEquipmentAdded proxy target {0} was null.", new object[] { component.TargetInstanceID });
	}

	// Token: 0x06006B1F RID: 27423 RVA: 0x00287334 File Offset: 0x00285534
	private bool ShouldShowName(GameObject representedObject)
	{
		CharacterOverlay component = representedObject.GetComponent<CharacterOverlay>();
		return component != null && component.shouldShowName;
	}

	// Token: 0x06006B20 RID: 27424 RVA: 0x0028735C File Offset: 0x0028555C
	public Guid AddAreaText(string initialText, GameObject prefab)
	{
		NameDisplayScreen.TextEntry textEntry = new NameDisplayScreen.TextEntry();
		textEntry.guid = Guid.NewGuid();
		textEntry.display_go = Util.KInstantiateUI(prefab, this.areaTextDisplayCanvas.gameObject, true);
		textEntry.display_go.GetComponentInChildren<LocText>().text = initialText;
		this.textEntries.Add(textEntry);
		return textEntry.guid;
	}

	// Token: 0x06006B21 RID: 27425 RVA: 0x002873B8 File Offset: 0x002855B8
	public GameObject GetWorldText(Guid guid)
	{
		GameObject gameObject = null;
		foreach (NameDisplayScreen.TextEntry textEntry in this.textEntries)
		{
			if (textEntry.guid == guid)
			{
				gameObject = textEntry.display_go;
				break;
			}
		}
		return gameObject;
	}

	// Token: 0x06006B22 RID: 27426 RVA: 0x00287420 File Offset: 0x00285620
	public void RemoveWorldText(Guid guid)
	{
		int num = -1;
		for (int i = 0; i < this.textEntries.Count; i++)
		{
			if (this.textEntries[i].guid == guid)
			{
				num = i;
				break;
			}
		}
		if (num >= 0)
		{
			global::UnityEngine.Object.Destroy(this.textEntries[num].display_go);
			this.textEntries.RemoveAt(num);
		}
	}

	// Token: 0x06006B23 RID: 27427 RVA: 0x00287488 File Offset: 0x00285688
	public void AddNewEntry(GameObject representedObject)
	{
		NameDisplayScreen.Entry entry = new NameDisplayScreen.Entry();
		entry.world_go = representedObject;
		entry.world_go_anim_controller = representedObject.GetComponent<KAnimControllerBase>();
		GameObject gameObject = (this.ShouldShowName(representedObject) ? this.nameAndBarsPrefab : this.barsPrefab);
		entry.kprfabID = representedObject.GetComponent<KPrefabID>();
		entry.collider = representedObject.GetComponent<KBoxCollider2D>();
		GameObject gameObject2 = Util.KInstantiateUI(gameObject, this.nameDisplayCanvas.gameObject, true);
		entry.display_go = gameObject2;
		entry.display_go_rect = gameObject2.GetComponent<RectTransform>();
		entry.nameLabel = entry.display_go.GetComponentInChildren<LocText>();
		entry.display_go.SetActive(false);
		if (this.worldSpace)
		{
			entry.display_go.transform.localScale = Vector3.one * 0.01f;
		}
		gameObject2.name = representedObject.name + " character overlay";
		entry.Name = representedObject.name;
		entry.refs = gameObject2.GetComponent<HierarchyReferences>();
		this.entries.Add(entry);
		global::UnityEngine.Object component = representedObject.GetComponent<KSelectable>();
		FactionAlignment component2 = representedObject.GetComponent<FactionAlignment>();
		if (component != null)
		{
			if (component2 != null)
			{
				if (component2.Alignment == FactionManager.FactionID.Friendly || component2.Alignment == FactionManager.FactionID.Duplicant)
				{
					this.UpdateName(representedObject);
					return;
				}
			}
			else
			{
				this.UpdateName(representedObject);
			}
		}
	}

	// Token: 0x06006B24 RID: 27428 RVA: 0x002875C0 File Offset: 0x002857C0
	public void RegisterComponent(GameObject representedObject, object component, bool force_new_entry = false)
	{
		NameDisplayScreen.Entry entry = (force_new_entry ? null : this.GetEntry(representedObject));
		if (entry == null)
		{
			CharacterOverlay component2 = representedObject.GetComponent<CharacterOverlay>();
			if (component2 != null)
			{
				component2.Register();
				entry = this.GetEntry(representedObject);
			}
		}
		if (entry == null)
		{
			return;
		}
		Transform reference = entry.refs.GetReference<Transform>("Bars");
		entry.bars_go = reference.gameObject;
		if (component is Health)
		{
			if (!entry.healthBar)
			{
				Health health = (Health)component;
				GameObject gameObject = Util.KInstantiateUI(ProgressBarsConfig.Instance.healthBarPrefab, reference.gameObject, false);
				gameObject.name = "Health Bar";
				health.healthBar = gameObject.GetComponent<HealthBar>();
				health.healthBar.GetComponent<KSelectable>().entityName = UI.METERS.HEALTH.TOOLTIP;
				health.healthBar.GetComponent<KSelectableHealthBar>().IsSelectable = representedObject.GetComponent<MinionBrain>() != null;
				entry.healthBar = health.healthBar;
				entry.healthBar.autoHide = false;
				gameObject.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("HealthBar");
				return;
			}
			global::Debug.LogWarningFormat("Health added twice {0}", new object[] { component });
			return;
		}
		else if (component is OxygenBreather)
		{
			if (!entry.breathBar)
			{
				GameObject gameObject2 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.breathBar = gameObject2.GetComponent<ProgressBar>();
				entry.breathBar.autoHide = false;
				gameObject2.gameObject.GetComponent<ToolTip>().AddMultiStringTooltip("Breath", this.ToolTipStyle_Property);
				gameObject2.name = "Breath Bar";
				gameObject2.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("BreathBar");
				gameObject2.GetComponent<KSelectable>().entityName = UI.METERS.BREATH.TOOLTIP;
				return;
			}
			global::Debug.LogWarningFormat("OxygenBreather added twice {0}", new object[] { component });
			return;
		}
		else if (component is BionicOxygenTankMonitor.Instance)
		{
			if (!entry.bionicOxygenTankBar)
			{
				GameObject gameObject3 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.bionicOxygenTankBar = gameObject3.GetComponent<ProgressBar>();
				entry.bionicOxygenTankBar.autoHide = false;
				gameObject3.name = "Bionic Oxygen Tank Bar";
				gameObject3.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("OxygenTankBar");
				gameObject3.GetComponent<KSelectable>().entityName = UI.METERS.BREATH.TOOLTIP;
				return;
			}
			global::Debug.LogWarningFormat("BionicOxygenTankBar added twice {0}", new object[] { component });
			return;
		}
		else if (component is Equipment)
		{
			if (!entry.suitBar)
			{
				GameObject gameObject4 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.suitBar = gameObject4.GetComponent<ProgressBar>();
				entry.suitBar.autoHide = false;
				gameObject4.name = "Suit Tank Bar";
				gameObject4.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("OxygenTankBar");
				gameObject4.GetComponent<KSelectable>().entityName = UI.METERS.BREATH.TOOLTIP;
			}
			else
			{
				global::Debug.LogWarningFormat("SuitBar added twice {0}", new object[] { component });
			}
			if (!entry.suitFuelBar)
			{
				GameObject gameObject5 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.suitFuelBar = gameObject5.GetComponent<ProgressBar>();
				entry.suitFuelBar.autoHide = false;
				gameObject5.name = "Suit Fuel Bar";
				gameObject5.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("FuelTankBar");
				gameObject5.GetComponent<KSelectable>().entityName = UI.METERS.FUEL.TOOLTIP;
			}
			else
			{
				global::Debug.LogWarningFormat("FuelBar added twice {0}", new object[] { component });
			}
			if (!entry.suitBatteryBar)
			{
				GameObject gameObject6 = Util.KInstantiateUI(ProgressBarsConfig.Instance.progressBarUIPrefab, reference.gameObject, false);
				entry.suitBatteryBar = gameObject6.GetComponent<ProgressBar>();
				entry.suitBatteryBar.autoHide = false;
				gameObject6.name = "Suit Battery Bar";
				gameObject6.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("BatteryBar");
				gameObject6.GetComponent<KSelectable>().entityName = UI.METERS.BATTERY.TOOLTIP;
				return;
			}
			global::Debug.LogWarningFormat("CoolantBar added twice {0}", new object[] { component });
			return;
		}
		else if (component is ThoughtGraph.Instance || component is CreatureThoughtGraph.Instance)
		{
			if (!entry.thoughtBubble)
			{
				GameObject gameObject7 = Util.KInstantiateUI(EffectPrefabs.Instance.ThoughtBubble, entry.display_go, false);
				entry.thoughtBubble = gameObject7.GetComponent<HierarchyReferences>();
				gameObject7.name = ((component is CreatureThoughtGraph.Instance) ? "Creature " : "") + "Thought Bubble";
				GameObject gameObject8 = Util.KInstantiateUI(EffectPrefabs.Instance.ThoughtBubbleConvo, entry.display_go, false);
				entry.thoughtBubbleConvo = gameObject8.GetComponent<HierarchyReferences>();
				gameObject8.name = ((component is CreatureThoughtGraph.Instance) ? "Creature " : "") + "Thought Bubble Convo";
				return;
			}
			global::Debug.LogWarningFormat("ThoughtGraph added twice {0}", new object[] { component });
			return;
		}
		else
		{
			if (!(component is GameplayEventMonitor.Instance))
			{
				if (component is Dreamer.Instance && !entry.dreamBubble)
				{
					GameObject gameObject9 = Util.KInstantiateUI(EffectPrefabs.Instance.DreamBubble, entry.display_go, false);
					gameObject9.name = "Dream Bubble";
					entry.dreamBubble = gameObject9.GetComponent<DreamBubble>();
				}
				return;
			}
			if (!entry.gameplayEventDisplay)
			{
				GameObject gameObject10 = Util.KInstantiateUI(EffectPrefabs.Instance.GameplayEventDisplay, entry.display_go, false);
				entry.gameplayEventDisplay = gameObject10.GetComponent<HierarchyReferences>();
				gameObject10.name = "Gameplay Event Display";
				return;
			}
			global::Debug.LogWarningFormat("GameplayEventDisplay added twice {0}", new object[] { component });
			return;
		}
	}

	// Token: 0x06006B25 RID: 27429 RVA: 0x00287BCD File Offset: 0x00285DCD
	public bool IsVisibleToZoom()
	{
		return !(Game.MainCamera == null) && Game.MainCamera.orthographicSize < this.HideDistance;
	}

	// Token: 0x06006B26 RID: 27430 RVA: 0x00287BF0 File Offset: 0x00285DF0
	private void LateUpdate()
	{
		if (App.isLoading || App.IsExiting)
		{
			return;
		}
		this.BindOnOverlayChange();
		if (Game.MainCamera == null)
		{
			return;
		}
		if (this.lastKnownOverlayID != OverlayModes.None.ID)
		{
			return;
		}
		int count = this.entries.Count;
		bool flag = this.IsVisibleToZoom();
		bool flag2 = flag && this.lastKnownOverlayID == OverlayModes.None.ID;
		if (this.nameDisplayCanvas.enabled != flag2)
		{
			this.nameDisplayCanvas.enabled = flag2;
		}
		if (flag)
		{
			this.RemoveDestroyedEntries();
			this.Culling();
			this.UpdatePos();
			this.HideDeadProgressBars();
		}
	}

	// Token: 0x06006B27 RID: 27431 RVA: 0x00287C90 File Offset: 0x00285E90
	private void Culling()
	{
		if (this.entries.Count == 0)
		{
			return;
		}
		Vector2I vector2I;
		Vector2I vector2I2;
		Grid.GetVisibleCellRangeInActiveWorld(out vector2I, out vector2I2, 4, 1.5f);
		int num = Mathf.Min(500, this.entries.Count);
		for (int i = 0; i < num; i++)
		{
			int num2 = (this.currentUpdateIndex + i) % this.entries.Count;
			NameDisplayScreen.Entry entry = this.entries[num2];
			Vector3 position = entry.world_go.transform.GetPosition();
			bool flag = position.x >= (float)vector2I.x && position.y >= (float)vector2I.y && position.x < (float)vector2I2.x && position.y < (float)vector2I2.y;
			if (entry.visible != flag)
			{
				entry.display_go.SetActive(flag);
			}
			entry.visible = flag;
		}
		this.currentUpdateIndex = (this.currentUpdateIndex + num) % this.entries.Count;
	}

	// Token: 0x06006B28 RID: 27432 RVA: 0x00287D9C File Offset: 0x00285F9C
	private void UpdatePos()
	{
		CameraController instance = CameraController.Instance;
		Transform followTarget = instance.followTarget;
		int count = this.entries.Count;
		for (int i = 0; i < count; i++)
		{
			NameDisplayScreen.Entry entry = this.entries[i];
			if (entry.visible)
			{
				GameObject world_go = entry.world_go;
				if (!(world_go == null))
				{
					Vector3 vector = world_go.transform.GetPosition();
					if (followTarget == world_go.transform)
					{
						vector = instance.followTargetPos;
					}
					else if (entry.world_go_anim_controller != null && entry.collider != null)
					{
						vector.x += entry.collider.offset.x;
						vector.y += entry.collider.offset.y - entry.collider.size.y / 2f;
					}
					entry.display_go_rect.anchoredPosition = (this.worldSpace ? vector : base.WorldToScreen(vector));
				}
			}
		}
	}

	// Token: 0x06006B29 RID: 27433 RVA: 0x00287EC0 File Offset: 0x002860C0
	private void RemoveDestroyedEntries()
	{
		int num = this.entries.Count;
		int i = 0;
		while (i < num)
		{
			if (this.entries[i].world_go == null)
			{
				global::UnityEngine.Object.Destroy(this.entries[i].display_go);
				num--;
				this.entries[i] = this.entries[num];
			}
			else
			{
				i++;
			}
		}
		this.entries.RemoveRange(num, this.entries.Count - num);
	}

	// Token: 0x06006B2A RID: 27434 RVA: 0x00287F4C File Offset: 0x0028614C
	private void HideDeadProgressBars()
	{
		int count = this.entries.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.entries[i].visible && !(this.entries[i].world_go == null) && this.entries[i].kprfabID.HasTag(GameTags.Dead) && this.entries[i].bars_go.activeSelf)
			{
				this.entries[i].bars_go.SetActive(false);
			}
		}
	}

	// Token: 0x06006B2B RID: 27435 RVA: 0x00287FEC File Offset: 0x002861EC
	public void UpdateName(GameObject representedObject)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(representedObject);
		if (entry == null)
		{
			return;
		}
		KSelectable component = representedObject.GetComponent<KSelectable>();
		entry.display_go.name = component.GetProperName() + " character overlay";
		if (entry.nameLabel != null)
		{
			entry.nameLabel.text = component.GetProperName();
			if (representedObject.GetComponent<RocketModule>() != null)
			{
				entry.nameLabel.text = representedObject.GetComponent<RocketModule>().GetParentRocketName();
			}
		}
	}

	// Token: 0x06006B2C RID: 27436 RVA: 0x0028806C File Offset: 0x0028626C
	public void SetDream(GameObject minion_go, Dream dream)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.dreamBubble == null)
		{
			return;
		}
		entry.dreamBubble.SetDream(dream);
		entry.dreamBubble.GetComponent<KSelectable>().entityName = "Dreaming";
		entry.dreamBubble.gameObject.SetActive(true);
		entry.dreamBubble.SetVisibility(true);
	}

	// Token: 0x06006B2D RID: 27437 RVA: 0x002880D4 File Offset: 0x002862D4
	public void StopDreaming(GameObject minion_go)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.dreamBubble == null)
		{
			return;
		}
		entry.dreamBubble.StopDreaming();
		entry.dreamBubble.gameObject.SetActive(false);
	}

	// Token: 0x06006B2E RID: 27438 RVA: 0x00288118 File Offset: 0x00286318
	public void DreamTick(GameObject minion_go, float dt)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.dreamBubble == null)
		{
			return;
		}
		entry.dreamBubble.Tick(dt);
	}

	// Token: 0x06006B2F RID: 27439 RVA: 0x0028814C File Offset: 0x0028634C
	public void SetThoughtBubbleDisplay(GameObject minion_go, bool bVisible, string hover_text, Sprite bubble_sprite, Sprite topic_sprite)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.thoughtBubble == null)
		{
			return;
		}
		this.ApplyThoughtSprite(entry.thoughtBubble, bubble_sprite, "bubble_sprite");
		this.ApplyThoughtSprite(entry.thoughtBubble, topic_sprite, "icon_sprite");
		entry.thoughtBubble.GetComponent<KSelectable>().entityName = hover_text;
		entry.thoughtBubble.gameObject.SetActive(bVisible);
	}

	// Token: 0x06006B30 RID: 27440 RVA: 0x002881BC File Offset: 0x002863BC
	public void SetThoughtBubbleConvoDisplay(GameObject minion_go, bool bVisible, string hover_text, Sprite bubble_sprite, Sprite topic_sprite, Sprite mode_sprite)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.thoughtBubble == null)
		{
			return;
		}
		this.ApplyThoughtSprite(entry.thoughtBubbleConvo, bubble_sprite, "bubble_sprite");
		this.ApplyThoughtSprite(entry.thoughtBubbleConvo, topic_sprite, "icon_sprite");
		this.ApplyThoughtSprite(entry.thoughtBubbleConvo, mode_sprite, "icon_sprite_mode");
		entry.thoughtBubbleConvo.GetComponent<KSelectable>().entityName = hover_text;
		entry.thoughtBubbleConvo.gameObject.SetActive(bVisible);
	}

	// Token: 0x06006B31 RID: 27441 RVA: 0x0028823E File Offset: 0x0028643E
	private void ApplyThoughtSprite(HierarchyReferences active_bubble, Sprite sprite, string target)
	{
		active_bubble.GetReference<Image>(target).sprite = sprite;
	}

	// Token: 0x06006B32 RID: 27442 RVA: 0x00288250 File Offset: 0x00286450
	public void SetGameplayEventDisplay(GameObject minion_go, bool bVisible, string hover_text, Sprite sprite)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.gameplayEventDisplay == null)
		{
			return;
		}
		entry.gameplayEventDisplay.GetReference<Image>("icon_sprite").sprite = sprite;
		entry.gameplayEventDisplay.GetComponent<KSelectable>().entityName = hover_text;
		entry.gameplayEventDisplay.gameObject.SetActive(bVisible);
	}

	// Token: 0x06006B33 RID: 27443 RVA: 0x002882B0 File Offset: 0x002864B0
	public void SetBreathDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.breathBar == null)
		{
			return;
		}
		entry.breathBar.SetUpdateFunc(updatePercentFull);
		entry.breathBar.SetVisibility(bVisible);
	}

	// Token: 0x06006B34 RID: 27444 RVA: 0x002882F0 File Offset: 0x002864F0
	public void SetHealthDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.healthBar == null)
		{
			return;
		}
		entry.healthBar.OnChange();
		entry.healthBar.SetUpdateFunc(updatePercentFull);
		if (entry.healthBar.gameObject.activeSelf != bVisible)
		{
			entry.healthBar.SetVisibility(bVisible);
		}
	}

	// Token: 0x06006B35 RID: 27445 RVA: 0x00288350 File Offset: 0x00286550
	public void SetSuitTankDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.suitBar == null)
		{
			return;
		}
		entry.suitBar.SetUpdateFunc(updatePercentFull);
		entry.suitBar.SetVisibility(bVisible);
	}

	// Token: 0x06006B36 RID: 27446 RVA: 0x00288390 File Offset: 0x00286590
	public void SetBionicOxygenTankDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.bionicOxygenTankBar == null)
		{
			return;
		}
		entry.bionicOxygenTankBar.SetUpdateFunc(updatePercentFull);
		entry.bionicOxygenTankBar.SetVisibility(bVisible);
	}

	// Token: 0x06006B37 RID: 27447 RVA: 0x002883D0 File Offset: 0x002865D0
	public void SetSuitFuelDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.suitFuelBar == null)
		{
			return;
		}
		entry.suitFuelBar.SetUpdateFunc(updatePercentFull);
		entry.suitFuelBar.SetVisibility(bVisible);
	}

	// Token: 0x06006B38 RID: 27448 RVA: 0x00288410 File Offset: 0x00286610
	public void SetSuitBatteryDisplay(GameObject minion_go, Func<float> updatePercentFull, bool bVisible)
	{
		NameDisplayScreen.Entry entry = this.GetEntry(minion_go);
		if (entry == null || entry.suitBatteryBar == null)
		{
			return;
		}
		entry.suitBatteryBar.SetUpdateFunc(updatePercentFull);
		entry.suitBatteryBar.SetVisibility(bVisible);
	}

	// Token: 0x06006B39 RID: 27449 RVA: 0x00288450 File Offset: 0x00286650
	private NameDisplayScreen.Entry GetEntry(GameObject worldObject)
	{
		return this.entries.Find((NameDisplayScreen.Entry entry) => entry.world_go == worldObject);
	}

	// Token: 0x040048F4 RID: 18676
	[SerializeField]
	private float HideDistance;

	// Token: 0x040048F5 RID: 18677
	public static NameDisplayScreen Instance;

	// Token: 0x040048F6 RID: 18678
	[SerializeField]
	private Canvas nameDisplayCanvas;

	// Token: 0x040048F7 RID: 18679
	[SerializeField]
	private Canvas areaTextDisplayCanvas;

	// Token: 0x040048F8 RID: 18680
	public GameObject nameAndBarsPrefab;

	// Token: 0x040048F9 RID: 18681
	public GameObject barsPrefab;

	// Token: 0x040048FA RID: 18682
	public TextStyleSetting ToolTipStyle_Property;

	// Token: 0x040048FB RID: 18683
	[SerializeField]
	private Color selectedColor;

	// Token: 0x040048FC RID: 18684
	[SerializeField]
	private Color defaultColor;

	// Token: 0x040048FD RID: 18685
	public int fontsize_min = 14;

	// Token: 0x040048FE RID: 18686
	public int fontsize_max = 32;

	// Token: 0x040048FF RID: 18687
	public float cameraDistance_fontsize_min = 6f;

	// Token: 0x04004900 RID: 18688
	public float cameraDistance_fontsize_max = 4f;

	// Token: 0x04004901 RID: 18689
	public List<NameDisplayScreen.Entry> entries = new List<NameDisplayScreen.Entry>();

	// Token: 0x04004902 RID: 18690
	public List<NameDisplayScreen.TextEntry> textEntries = new List<NameDisplayScreen.TextEntry>();

	// Token: 0x04004903 RID: 18691
	public bool worldSpace = true;

	// Token: 0x04004904 RID: 18692
	private bool isOverlayChangeBound;

	// Token: 0x04004905 RID: 18693
	private HashedString lastKnownOverlayID = OverlayModes.None.ID;

	// Token: 0x04004906 RID: 18694
	private int currentUpdateIndex;

	// Token: 0x02001F61 RID: 8033
	[Serializable]
	public class Entry
	{
		// Token: 0x04009096 RID: 37014
		public string Name;

		// Token: 0x04009097 RID: 37015
		public bool visible;

		// Token: 0x04009098 RID: 37016
		public GameObject world_go;

		// Token: 0x04009099 RID: 37017
		public GameObject display_go;

		// Token: 0x0400909A RID: 37018
		public GameObject bars_go;

		// Token: 0x0400909B RID: 37019
		public KPrefabID kprfabID;

		// Token: 0x0400909C RID: 37020
		public KBoxCollider2D collider;

		// Token: 0x0400909D RID: 37021
		public KAnimControllerBase world_go_anim_controller;

		// Token: 0x0400909E RID: 37022
		public RectTransform display_go_rect;

		// Token: 0x0400909F RID: 37023
		public LocText nameLabel;

		// Token: 0x040090A0 RID: 37024
		public HealthBar healthBar;

		// Token: 0x040090A1 RID: 37025
		public ProgressBar breathBar;

		// Token: 0x040090A2 RID: 37026
		public ProgressBar suitBar;

		// Token: 0x040090A3 RID: 37027
		public ProgressBar bionicOxygenTankBar;

		// Token: 0x040090A4 RID: 37028
		public ProgressBar suitFuelBar;

		// Token: 0x040090A5 RID: 37029
		public ProgressBar suitBatteryBar;

		// Token: 0x040090A6 RID: 37030
		public DreamBubble dreamBubble;

		// Token: 0x040090A7 RID: 37031
		public HierarchyReferences thoughtBubble;

		// Token: 0x040090A8 RID: 37032
		public HierarchyReferences thoughtBubbleConvo;

		// Token: 0x040090A9 RID: 37033
		public HierarchyReferences gameplayEventDisplay;

		// Token: 0x040090AA RID: 37034
		public HierarchyReferences refs;
	}

	// Token: 0x02001F62 RID: 8034
	public class TextEntry
	{
		// Token: 0x040090AB RID: 37035
		public Guid guid;

		// Token: 0x040090AC RID: 37036
		public GameObject display_go;
	}
}
