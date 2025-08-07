using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000DAC RID: 3500
public class ResearchScreen : KModalScreen
{
	// Token: 0x06006DB8 RID: 28088 RVA: 0x00298F1F File Offset: 0x0029711F
	public bool IsBeingResearched(Tech tech)
	{
		return Research.Instance.IsBeingResearched(tech);
	}

	// Token: 0x06006DB9 RID: 28089 RVA: 0x00298F2C File Offset: 0x0029712C
	public override float GetSortKey()
	{
		if (base.isEditing)
		{
			return 50f;
		}
		return 20f;
	}

	// Token: 0x06006DBA RID: 28090 RVA: 0x00298F44 File Offset: 0x00297144
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
		Transform transform = base.transform;
		while (this.m_Raycaster == null)
		{
			this.m_Raycaster = transform.GetComponent<GraphicRaycaster>();
			if (this.m_Raycaster == null)
			{
				transform = transform.parent;
			}
		}
	}

	// Token: 0x06006DBB RID: 28091 RVA: 0x00298F96 File Offset: 0x00297196
	private void ZoomOut()
	{
		this.targetZoom = Mathf.Clamp(this.targetZoom - this.zoomAmountPerButton, this.minZoom, this.maxZoom);
		this.zoomCenterLock = true;
	}

	// Token: 0x06006DBC RID: 28092 RVA: 0x00298FC3 File Offset: 0x002971C3
	private void ZoomIn()
	{
		this.targetZoom = Mathf.Clamp(this.targetZoom + this.zoomAmountPerButton, this.minZoom, this.maxZoom);
		this.zoomCenterLock = true;
	}

	// Token: 0x06006DBD RID: 28093 RVA: 0x00298FF0 File Offset: 0x002971F0
	public void ZoomToTech(string techID, bool highlight = false)
	{
		Vector2 vector = this.entryMap[Db.Get().Techs.Get(techID)].rectTransform().GetLocalPosition() + new Vector2(-this.foreground.rectTransform().rect.size.x / 2f, this.foreground.rectTransform().rect.size.y / 2f);
		this.forceTargetPosition = -vector;
		this.zoomingToTarget = true;
		this.targetZoom = this.maxZoom;
		if (highlight)
		{
			this.sideBar.SetSearch(Db.Get().Techs.Get(techID).Name);
		}
	}

	// Token: 0x06006DBE RID: 28094 RVA: 0x002990BC File Offset: 0x002972BC
	private void Update()
	{
		if (!base.canvas.enabled)
		{
			return;
		}
		RectTransform component = this.scrollContent.GetComponent<RectTransform>();
		if (this.isDragging && !KInputManager.isFocused)
		{
			this.AbortDragging();
		}
		Vector2 anchoredPosition = component.anchoredPosition;
		float num = Mathf.Min(this.effectiveZoomSpeed * Time.unscaledDeltaTime, 0.9f);
		this.currentZoom = Mathf.Lerp(this.currentZoom, this.targetZoom, num);
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = KInputManager.GetMousePos();
		Vector2 vector3 = (this.zoomCenterLock ? (component.InverseTransformPoint(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2))) * this.currentZoom) : (component.InverseTransformPoint(vector2) * this.currentZoom));
		component.localScale = new Vector3(this.currentZoom, this.currentZoom, 1f);
		vector = (this.zoomCenterLock ? (component.InverseTransformPoint(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2))) * this.currentZoom) : (component.InverseTransformPoint(vector2) * this.currentZoom)) - vector3;
		float num2 = this.keyboardScrollSpeed;
		if (this.panUp)
		{
			this.keyPanDelta -= Vector2.up * Time.unscaledDeltaTime * num2;
		}
		else if (this.panDown)
		{
			this.keyPanDelta += Vector2.up * Time.unscaledDeltaTime * num2;
		}
		if (this.panLeft)
		{
			this.keyPanDelta += Vector2.right * Time.unscaledDeltaTime * num2;
		}
		else if (this.panRight)
		{
			this.keyPanDelta -= Vector2.right * Time.unscaledDeltaTime * num2;
		}
		if (KInputManager.currentControllerIsGamepad)
		{
			Vector2 vector4 = KInputManager.steamInputInterpreter.GetSteamCameraMovement();
			vector4 *= -1f;
			this.keyPanDelta = vector4 * Time.unscaledDeltaTime * num2 * 2f;
		}
		Vector2 vector5 = new Vector2(Mathf.Lerp(0f, this.keyPanDelta.x, Time.unscaledDeltaTime * this.keyPanEasing), Mathf.Lerp(0f, this.keyPanDelta.y, Time.unscaledDeltaTime * this.keyPanEasing));
		this.keyPanDelta -= vector5;
		Vector2 vector6 = Vector2.zero;
		if (this.isDragging)
		{
			Vector2 vector7 = KInputManager.GetMousePos() - this.dragLastPosition;
			vector6 += vector7;
			this.dragLastPosition = KInputManager.GetMousePos();
			this.dragInteria = Vector2.ClampMagnitude(this.dragInteria + vector7, 400f);
		}
		this.dragInteria *= Mathf.Max(0f, 1f - Time.unscaledDeltaTime * 4f);
		Vector2 vector8 = anchoredPosition + vector + this.keyPanDelta + vector6;
		if (!this.isDragging)
		{
			Vector2 size = base.GetComponent<RectTransform>().rect.size;
			Vector2 vector9 = new Vector2((-component.rect.size.x / 2f - 250f) * this.currentZoom, -250f * this.currentZoom);
			Vector2 vector10 = new Vector2(250f * this.currentZoom, (component.rect.size.y + 250f) * this.currentZoom - size.y);
			Vector2 vector11 = new Vector2(Mathf.Clamp(vector8.x, vector9.x, vector10.x), Mathf.Clamp(vector8.y, vector9.y, vector10.y));
			this.forceTargetPosition = new Vector2(Mathf.Clamp(this.forceTargetPosition.x, vector9.x, vector10.x), Mathf.Clamp(this.forceTargetPosition.y, vector9.y, vector10.y));
			Vector2 vector12 = vector11 + this.dragInteria - vector8;
			if (!this.panLeft && !this.panRight && !this.panUp && !this.panDown)
			{
				vector8 += vector12 * this.edgeClampFactor * Time.unscaledDeltaTime;
			}
			else
			{
				vector8 += vector12;
				if (vector12.x < 0f)
				{
					this.keyPanDelta.x = Mathf.Min(0f, this.keyPanDelta.x);
				}
				if (vector12.x > 0f)
				{
					this.keyPanDelta.x = Mathf.Max(0f, this.keyPanDelta.x);
				}
				if (vector12.y < 0f)
				{
					this.keyPanDelta.y = Mathf.Min(0f, this.keyPanDelta.y);
				}
				if (vector12.y > 0f)
				{
					this.keyPanDelta.y = Mathf.Max(0f, this.keyPanDelta.y);
				}
			}
		}
		if (this.zoomingToTarget)
		{
			vector8 = Vector2.Lerp(vector8, this.forceTargetPosition, Time.unscaledDeltaTime * 4f);
			if (Vector3.Distance(vector8, this.forceTargetPosition) < 1f || this.isDragging || this.panLeft || this.panRight || this.panUp || this.panDown)
			{
				this.zoomingToTarget = false;
			}
		}
		component.anchoredPosition = vector8;
	}

	// Token: 0x06006DBF RID: 28095 RVA: 0x002996B8 File Offset: 0x002978B8
	protected override void OnSpawn()
	{
		base.Subscribe(Research.Instance.gameObject, -1914338957, new Action<object>(this.OnActiveResearchChanged));
		base.Subscribe(Game.Instance.gameObject, -107300940, new Action<object>(this.OnResearchComplete));
		base.Subscribe(Game.Instance.gameObject, -1974454597, delegate(object o)
		{
			this.Show(false);
		});
		this.pointDisplayMap = new Dictionary<string, LocText>();
		foreach (ResearchType researchType in Research.Instance.researchTypes.Types)
		{
			this.pointDisplayMap[researchType.id] = Util.KInstantiateUI(this.pointDisplayCountPrefab, this.pointDisplayContainer, true).GetComponentInChildren<LocText>();
			this.pointDisplayMap[researchType.id].text = Research.Instance.globalPointInventory.PointsByTypeID[researchType.id].ToString();
			this.pointDisplayMap[researchType.id].transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(researchType.description);
			this.pointDisplayMap[researchType.id].transform.parent.GetComponentInChildren<Image>().sprite = researchType.sprite;
		}
		this.pointDisplayContainer.transform.parent.gameObject.SetActive(Research.Instance.UseGlobalPointInventory);
		this.entryMap = new Dictionary<Tech, ResearchEntry>();
		List<Tech> resources = Db.Get().Techs.resources;
		resources.Sort((Tech x, Tech y) => y.center.y.CompareTo(x.center.y));
		List<TechTreeTitle> resources2 = Db.Get().TechTreeTitles.resources;
		resources2.Sort((TechTreeTitle x, TechTreeTitle y) => y.center.y.CompareTo(x.center.y));
		float num = 0f;
		float num2 = 125f;
		Vector2 vector = new Vector2(num, num2);
		for (int i = 0; i < resources2.Count; i++)
		{
			ResearchTreeTitle researchTreeTitle = Util.KInstantiateUI<ResearchTreeTitle>(this.researchTreeTitlePrefab.gameObject, this.treeTitles, false);
			TechTreeTitle techTreeTitle = resources2[i];
			researchTreeTitle.name = techTreeTitle.Name + " Title";
			Vector3 vector2 = techTreeTitle.center + vector;
			researchTreeTitle.transform.rectTransform().anchoredPosition = vector2;
			float num3 = techTreeTitle.height;
			if (i + 1 < resources2.Count)
			{
				TechTreeTitle techTreeTitle2 = resources2[i + 1];
				Vector3 vector3 = techTreeTitle2.center + vector;
				num3 += vector2.y - (vector3.y + techTreeTitle2.height);
			}
			else
			{
				num3 += 600f;
			}
			researchTreeTitle.transform.rectTransform().sizeDelta = new Vector2(techTreeTitle.width, num3);
			researchTreeTitle.SetLabel(techTreeTitle.Name);
			researchTreeTitle.SetColor(i);
		}
		List<Vector2> list = new List<Vector2>();
		float num4 = 0f;
		float num5 = 0f;
		Vector2 vector4 = new Vector2(num4, num5);
		for (int j = 0; j < resources.Count; j++)
		{
			ResearchEntry researchEntry = Util.KInstantiateUI<ResearchEntry>(this.entryPrefab.gameObject, this.scrollContent, false);
			Tech tech = resources[j];
			researchEntry.name = tech.Name + " Panel";
			Vector3 vector5 = tech.center + vector4;
			researchEntry.transform.rectTransform().anchoredPosition = vector5;
			researchEntry.transform.rectTransform().sizeDelta = new Vector2(tech.width, tech.height);
			this.entryMap.Add(tech, researchEntry);
			if (tech.edges.Count > 0)
			{
				for (int k = 0; k < tech.edges.Count; k++)
				{
					ResourceTreeNode.Edge edge = tech.edges[k];
					if (edge.path == null)
					{
						list.AddRange(edge.SrcTarget);
					}
					else
					{
						ResourceTreeNode.Edge.EdgeType edgeType = edge.edgeType;
						if (edgeType <= ResourceTreeNode.Edge.EdgeType.QuadCurveEdge || edgeType - ResourceTreeNode.Edge.EdgeType.BezierEdge <= 1)
						{
							list.Add(edge.SrcTarget[0]);
							list.Add(edge.path[0]);
							for (int l = 1; l < edge.path.Count; l++)
							{
								list.Add(edge.path[l - 1]);
								list.Add(edge.path[l]);
							}
							list.Add(edge.path[edge.path.Count - 1]);
							list.Add(edge.SrcTarget[1]);
						}
						else
						{
							list.AddRange(edge.path);
						}
					}
				}
			}
		}
		for (int m = 0; m < list.Count; m++)
		{
			list[m] = new Vector2(list[m].x, list[m].y + this.foreground.transform.rectTransform().rect.height);
		}
		foreach (KeyValuePair<Tech, ResearchEntry> keyValuePair in this.entryMap)
		{
			keyValuePair.Value.SetTech(keyValuePair.Key);
		}
		this.CloseButton.soundPlayer.Enabled = false;
		this.CloseButton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
		base.StartCoroutine(this.WaitAndSetActiveResearch());
		base.OnSpawn();
		this.scrollContent.GetComponent<RectTransform>().anchoredPosition = new Vector2(250f, -250f);
		this.zoomOutButton.onClick += delegate
		{
			this.ZoomOut();
		};
		this.zoomInButton.onClick += delegate
		{
			this.ZoomIn();
		};
		base.gameObject.SetActive(true);
		this.Show(false);
	}

	// Token: 0x06006DC0 RID: 28096 RVA: 0x00299D60 File Offset: 0x00297F60
	public override void OnBeginDrag(PointerEventData eventData)
	{
		base.OnBeginDrag(eventData);
		this.isDragging = true;
	}

	// Token: 0x06006DC1 RID: 28097 RVA: 0x00299D70 File Offset: 0x00297F70
	public override void OnEndDrag(PointerEventData eventData)
	{
		base.OnEndDrag(eventData);
		this.AbortDragging();
	}

	// Token: 0x06006DC2 RID: 28098 RVA: 0x00299D7F File Offset: 0x00297F7F
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		base.Unsubscribe(Game.Instance.gameObject, -1974454597, delegate(object o)
		{
			this.Deactivate();
		});
	}

	// Token: 0x06006DC3 RID: 28099 RVA: 0x00299DA8 File Offset: 0x00297FA8
	private IEnumerator WaitAndSetActiveResearch()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		TechInstance targetResearch = Research.Instance.GetTargetResearch();
		if (targetResearch != null)
		{
			this.SetActiveResearch(targetResearch.tech);
		}
		yield break;
	}

	// Token: 0x06006DC4 RID: 28100 RVA: 0x00299DB7 File Offset: 0x00297FB7
	public Vector3 GetEntryPosition(Tech tech)
	{
		if (!this.entryMap.ContainsKey(tech))
		{
			global::Debug.LogError("The Tech provided was not present in the dictionary");
			return Vector3.zero;
		}
		return this.entryMap[tech].transform.GetPosition();
	}

	// Token: 0x06006DC5 RID: 28101 RVA: 0x00299DED File Offset: 0x00297FED
	public ResearchEntry GetEntry(Tech tech)
	{
		if (this.entryMap == null)
		{
			return null;
		}
		if (!this.entryMap.ContainsKey(tech))
		{
			global::Debug.LogError("The Tech provided was not present in the dictionary");
			return null;
		}
		return this.entryMap[tech];
	}

	// Token: 0x06006DC6 RID: 28102 RVA: 0x00299E20 File Offset: 0x00298020
	public void SetEntryPercentage(Tech tech, float percent)
	{
		ResearchEntry entry = this.GetEntry(tech);
		if (entry != null)
		{
			entry.SetPercentage(percent);
		}
	}

	// Token: 0x06006DC7 RID: 28103 RVA: 0x00299E48 File Offset: 0x00298048
	public void TurnEverythingOff()
	{
		foreach (KeyValuePair<Tech, ResearchEntry> keyValuePair in this.entryMap)
		{
			keyValuePair.Value.SetEverythingOff();
		}
	}

	// Token: 0x06006DC8 RID: 28104 RVA: 0x00299EA0 File Offset: 0x002980A0
	public void TurnEverythingOn()
	{
		foreach (KeyValuePair<Tech, ResearchEntry> keyValuePair in this.entryMap)
		{
			keyValuePair.Value.SetEverythingOn();
		}
	}

	// Token: 0x06006DC9 RID: 28105 RVA: 0x00299EF8 File Offset: 0x002980F8
	private void SelectAllEntries(Tech tech, bool isSelected)
	{
		ResearchEntry entry = this.GetEntry(tech);
		if (entry != null)
		{
			entry.QueueStateChanged(isSelected);
		}
		foreach (Tech tech2 in tech.requiredTech)
		{
			this.SelectAllEntries(tech2, isSelected);
		}
	}

	// Token: 0x06006DCA RID: 28106 RVA: 0x00299F64 File Offset: 0x00298164
	private void OnResearchComplete(object data)
	{
		if (data is Tech)
		{
			Tech tech = (Tech)data;
			ResearchEntry entry = this.GetEntry(tech);
			if (entry != null)
			{
				entry.ResearchCompleted(true);
			}
			this.UpdateProgressBars();
			this.UpdatePointDisplay();
		}
	}

	// Token: 0x06006DCB RID: 28107 RVA: 0x00299FA4 File Offset: 0x002981A4
	private void UpdatePointDisplay()
	{
		foreach (ResearchType researchType in Research.Instance.researchTypes.Types)
		{
			this.pointDisplayMap[researchType.id].text = string.Format("{0}: {1}", Research.Instance.researchTypes.GetResearchType(researchType.id).name, Research.Instance.globalPointInventory.PointsByTypeID[researchType.id].ToString());
		}
	}

	// Token: 0x06006DCC RID: 28108 RVA: 0x0029A058 File Offset: 0x00298258
	private void OnActiveResearchChanged(object data)
	{
		List<TechInstance> list = (List<TechInstance>)data;
		foreach (TechInstance techInstance in list)
		{
			ResearchEntry entry = this.GetEntry(techInstance.tech);
			if (entry != null)
			{
				entry.QueueStateChanged(true);
			}
		}
		this.UpdateProgressBars();
		this.UpdatePointDisplay();
		if (list.Count > 0)
		{
			this.currentResearch = list[list.Count - 1].tech;
		}
	}

	// Token: 0x06006DCD RID: 28109 RVA: 0x0029A0F4 File Offset: 0x002982F4
	private void UpdateProgressBars()
	{
		foreach (KeyValuePair<Tech, ResearchEntry> keyValuePair in this.entryMap)
		{
			keyValuePair.Value.UpdateProgressBars();
		}
	}

	// Token: 0x06006DCE RID: 28110 RVA: 0x0029A14C File Offset: 0x0029834C
	public void CancelResearch()
	{
		List<TechInstance> researchQueue = Research.Instance.GetResearchQueue();
		foreach (TechInstance techInstance in researchQueue)
		{
			ResearchEntry entry = this.GetEntry(techInstance.tech);
			if (entry != null)
			{
				entry.QueueStateChanged(false);
			}
		}
		researchQueue.Clear();
	}

	// Token: 0x06006DCF RID: 28111 RVA: 0x0029A1C4 File Offset: 0x002983C4
	private void SetActiveResearch(Tech newResearch)
	{
		if (newResearch != this.currentResearch && this.currentResearch != null)
		{
			this.SelectAllEntries(this.currentResearch, false);
		}
		this.currentResearch = newResearch;
		if (this.currentResearch != null)
		{
			this.SelectAllEntries(this.currentResearch, true);
		}
	}

	// Token: 0x06006DD0 RID: 28112 RVA: 0x0029A200 File Offset: 0x00298400
	public override void Show(bool show = true)
	{
		this.mouseOver = false;
		this.scrollContentChildFitter.enabled = show;
		foreach (Canvas canvas in base.GetComponentsInChildren<Canvas>(true))
		{
			if (canvas.enabled != show)
			{
				canvas.enabled = show;
			}
		}
		CanvasGroup component = base.GetComponent<CanvasGroup>();
		if (component != null)
		{
			component.interactable = show;
			component.blocksRaycasts = show;
			component.ignoreParentGroups = true;
		}
		this.OnShow(show);
	}

	// Token: 0x06006DD1 RID: 28113 RVA: 0x0029A278 File Offset: 0x00298478
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.sideBar.ResetFilter();
		}
		if (show)
		{
			CameraController.Instance.DisableUserCameraControl = true;
			if (DetailsScreen.Instance != null)
			{
				DetailsScreen.Instance.gameObject.SetActive(false);
			}
		}
		else
		{
			CameraController.Instance.DisableUserCameraControl = false;
			if (SelectTool.Instance.selected != null && !DetailsScreen.Instance.gameObject.activeSelf)
			{
				DetailsScreen.Instance.gameObject.SetActive(true);
				DetailsScreen.Instance.Refresh(SelectTool.Instance.selected.gameObject);
			}
		}
		this.UpdateProgressBars();
		this.UpdatePointDisplay();
	}

	// Token: 0x06006DD2 RID: 28114 RVA: 0x0029A32A File Offset: 0x0029852A
	private void AbortDragging()
	{
		this.isDragging = false;
		this.draggingJustEnded = true;
	}

	// Token: 0x06006DD3 RID: 28115 RVA: 0x0029A33A File Offset: 0x0029853A
	private void LateUpdate()
	{
		this.draggingJustEnded = false;
	}

	// Token: 0x06006DD4 RID: 28116 RVA: 0x0029A344 File Offset: 0x00298544
	public override void OnKeyUp(KButtonEvent e)
	{
		if (!base.canvas.enabled)
		{
			return;
		}
		if (!e.Consumed)
		{
			if (e.IsAction(global::Action.MouseRight) && !this.isDragging && !this.draggingJustEnded)
			{
				ManagementMenu.Instance.CloseAll();
			}
			if (e.IsAction(global::Action.MouseRight) || e.IsAction(global::Action.MouseLeft) || e.IsAction(global::Action.MouseMiddle))
			{
				this.AbortDragging();
			}
			if (this.panUp && e.TryConsume(global::Action.PanUp))
			{
				this.panUp = false;
				return;
			}
			if (this.panDown && e.TryConsume(global::Action.PanDown))
			{
				this.panDown = false;
				return;
			}
			if (this.panRight && e.TryConsume(global::Action.PanRight))
			{
				this.panRight = false;
				return;
			}
			if (this.panLeft && e.TryConsume(global::Action.PanLeft))
			{
				this.panLeft = false;
				return;
			}
		}
		base.OnKeyUp(e);
	}

	// Token: 0x06006DD5 RID: 28117 RVA: 0x0029A42C File Offset: 0x0029862C
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!base.canvas.enabled)
		{
			return;
		}
		if (!e.Consumed)
		{
			if (e.TryConsume(global::Action.MouseRight))
			{
				this.dragStartPosition = KInputManager.GetMousePos();
				this.dragLastPosition = KInputManager.GetMousePos();
				return;
			}
			if (e.TryConsume(global::Action.MouseLeft))
			{
				this.dragStartPosition = KInputManager.GetMousePos();
				this.dragLastPosition = KInputManager.GetMousePos();
				return;
			}
			if (KInputManager.GetMousePos().x > this.sideBar.rectTransform().sizeDelta.x && CameraController.IsMouseOverGameWindow)
			{
				if (e.TryConsume(global::Action.ZoomIn))
				{
					this.targetZoom = Mathf.Clamp(this.targetZoom + this.zoomAmountPerScroll, this.minZoom, this.maxZoom);
					this.zoomCenterLock = false;
					return;
				}
				if (e.TryConsume(global::Action.ZoomOut))
				{
					this.targetZoom = Mathf.Clamp(this.targetZoom - this.zoomAmountPerScroll, this.minZoom, this.maxZoom);
					this.zoomCenterLock = false;
					return;
				}
			}
			if (e.TryConsume(global::Action.Escape))
			{
				ManagementMenu.Instance.CloseAll();
				return;
			}
			if (e.TryConsume(global::Action.PanLeft))
			{
				this.panLeft = true;
				return;
			}
			if (e.TryConsume(global::Action.PanRight))
			{
				this.panRight = true;
				return;
			}
			if (e.TryConsume(global::Action.PanUp))
			{
				this.panUp = true;
				return;
			}
			if (e.TryConsume(global::Action.PanDown))
			{
				this.panDown = true;
				return;
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x04004B1D RID: 19229
	private const float SCROLL_BUFFER = 250f;

	// Token: 0x04004B1E RID: 19230
	[SerializeField]
	private Image BG;

	// Token: 0x04004B1F RID: 19231
	public ResearchEntry entryPrefab;

	// Token: 0x04004B20 RID: 19232
	public ResearchTreeTitle researchTreeTitlePrefab;

	// Token: 0x04004B21 RID: 19233
	public GameObject foreground;

	// Token: 0x04004B22 RID: 19234
	public GameObject scrollContent;

	// Token: 0x04004B23 RID: 19235
	public GameObject treeTitles;

	// Token: 0x04004B24 RID: 19236
	public GameObject pointDisplayCountPrefab;

	// Token: 0x04004B25 RID: 19237
	public GameObject pointDisplayContainer;

	// Token: 0x04004B26 RID: 19238
	private Dictionary<string, LocText> pointDisplayMap;

	// Token: 0x04004B27 RID: 19239
	private Dictionary<Tech, ResearchEntry> entryMap;

	// Token: 0x04004B28 RID: 19240
	[SerializeField]
	private KButton zoomOutButton;

	// Token: 0x04004B29 RID: 19241
	[SerializeField]
	private KButton zoomInButton;

	// Token: 0x04004B2A RID: 19242
	[SerializeField]
	private ResearchScreenSideBar sideBar;

	// Token: 0x04004B2B RID: 19243
	private Tech currentResearch;

	// Token: 0x04004B2C RID: 19244
	public KButton CloseButton;

	// Token: 0x04004B2D RID: 19245
	private GraphicRaycaster m_Raycaster;

	// Token: 0x04004B2E RID: 19246
	private PointerEventData m_PointerEventData;

	// Token: 0x04004B2F RID: 19247
	private Vector3 currentScrollPosition;

	// Token: 0x04004B30 RID: 19248
	private bool panUp;

	// Token: 0x04004B31 RID: 19249
	private bool panDown;

	// Token: 0x04004B32 RID: 19250
	private bool panLeft;

	// Token: 0x04004B33 RID: 19251
	private bool panRight;

	// Token: 0x04004B34 RID: 19252
	[SerializeField]
	private KChildFitter scrollContentChildFitter;

	// Token: 0x04004B35 RID: 19253
	private bool isDragging;

	// Token: 0x04004B36 RID: 19254
	private Vector3 dragStartPosition;

	// Token: 0x04004B37 RID: 19255
	private Vector3 dragLastPosition;

	// Token: 0x04004B38 RID: 19256
	private Vector2 dragInteria;

	// Token: 0x04004B39 RID: 19257
	private Vector2 forceTargetPosition;

	// Token: 0x04004B3A RID: 19258
	private bool zoomingToTarget;

	// Token: 0x04004B3B RID: 19259
	private bool draggingJustEnded;

	// Token: 0x04004B3C RID: 19260
	private float targetZoom = 1f;

	// Token: 0x04004B3D RID: 19261
	private float currentZoom = 1f;

	// Token: 0x04004B3E RID: 19262
	private bool zoomCenterLock;

	// Token: 0x04004B3F RID: 19263
	private Vector2 keyPanDelta = Vector3.zero;

	// Token: 0x04004B40 RID: 19264
	[SerializeField]
	private float effectiveZoomSpeed = 5f;

	// Token: 0x04004B41 RID: 19265
	[SerializeField]
	private float zoomAmountPerScroll = 0.05f;

	// Token: 0x04004B42 RID: 19266
	[SerializeField]
	private float zoomAmountPerButton = 0.5f;

	// Token: 0x04004B43 RID: 19267
	[SerializeField]
	private float minZoom = 0.15f;

	// Token: 0x04004B44 RID: 19268
	[SerializeField]
	private float maxZoom = 1f;

	// Token: 0x04004B45 RID: 19269
	[SerializeField]
	private float keyboardScrollSpeed = 200f;

	// Token: 0x04004B46 RID: 19270
	[SerializeField]
	private float keyPanEasing = 1f;

	// Token: 0x04004B47 RID: 19271
	[SerializeField]
	private float edgeClampFactor = 0.5f;

	// Token: 0x02001FAD RID: 8109
	public enum ResearchState
	{
		// Token: 0x040091BB RID: 37307
		Available,
		// Token: 0x040091BC RID: 37308
		ActiveResearch,
		// Token: 0x040091BD RID: 37309
		ResearchComplete,
		// Token: 0x040091BE RID: 37310
		MissingPrerequisites,
		// Token: 0x040091BF RID: 37311
		StateCount
	}
}
