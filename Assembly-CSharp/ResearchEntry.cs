using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

// Token: 0x02000DAB RID: 3499
[AddComponentMenu("KMonoBehaviour/scripts/ResearchEntry")]
public class ResearchEntry : KMonoBehaviour
{
	// Token: 0x06006DA5 RID: 28069 RVA: 0x00298038 File Offset: 0x00296238
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.techLineMap = new Dictionary<Tech, UILineRenderer>();
		this.BG.color = this.defaultColor;
		foreach (Tech tech in this.targetTech.requiredTech)
		{
			float num = this.targetTech.width / 2f + 18f;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			if (tech.center.y > this.targetTech.center.y + 2f)
			{
				zero = new Vector2(0f, 20f);
				zero2 = new Vector2(0f, -20f);
			}
			else if (tech.center.y < this.targetTech.center.y - 2f)
			{
				zero = new Vector2(0f, -20f);
				zero2 = new Vector2(0f, 20f);
			}
			UILineRenderer component = Util.KInstantiateUI(this.linePrefab, this.lineContainer.gameObject, true).GetComponent<UILineRenderer>();
			float num2 = 32f;
			component.Points = new Vector2[]
			{
				new Vector2(0f, 0f) + zero,
				new Vector2(-num2, 0f) + zero,
				new Vector2(-num2, tech.center.y - this.targetTech.center.y) + zero2,
				new Vector2(-(this.targetTech.center.x - num - (tech.center.x + num)) + 2f, tech.center.y - this.targetTech.center.y) + zero2
			};
			component.LineThickness = (float)this.lineThickness_inactive;
			component.color = this.inactiveLineColor;
			this.techLineMap.Add(tech, component);
		}
		this.QueueStateChanged(false);
		if (this.targetTech != null)
		{
			using (List<TechInstance>.Enumerator enumerator2 = Research.Instance.GetResearchQueue().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.tech == this.targetTech)
					{
						this.QueueStateChanged(true);
					}
				}
			}
		}
	}

	// Token: 0x06006DA6 RID: 28070 RVA: 0x002982F8 File Offset: 0x002964F8
	public void SetTech(Tech newTech)
	{
		if (newTech == null)
		{
			global::Debug.LogError("The research provided is null!");
			return;
		}
		if (this.targetTech == newTech)
		{
			return;
		}
		foreach (ResearchType researchType in Research.Instance.researchTypes.Types)
		{
			if (newTech.costsByResearchTypeID.ContainsKey(researchType.id) && newTech.costsByResearchTypeID[researchType.id] > 0f)
			{
				GameObject gameObject = Util.KInstantiateUI(this.progressBarPrefab, this.progressBarContainer.gameObject, true);
				Image image = gameObject.GetComponentsInChildren<Image>()[2];
				Image component = gameObject.transform.Find("Icon").GetComponent<Image>();
				image.color = researchType.color;
				component.sprite = researchType.sprite;
				this.progressBarsByResearchTypeID[researchType.id] = gameObject;
			}
		}
		if (this.researchScreen == null)
		{
			this.researchScreen = base.transform.parent.GetComponentInParent<ResearchScreen>();
		}
		if (newTech.IsComplete())
		{
			this.ResearchCompleted(false);
		}
		this.targetTech = newTech;
		this.researchName.text = this.targetTech.Name;
		string text = "";
		foreach (TechItem techItem in this.targetTech.unlockedItems)
		{
			if (Game.IsCorrectDlcActiveForCurrentSave(techItem))
			{
				HierarchyReferences component2 = this.GetFreeIcon().GetComponent<HierarchyReferences>();
				if (text != "")
				{
					text += ", ";
				}
				text += techItem.Name;
				component2.GetReference<KImage>("Icon").sprite = techItem.UISprite();
				component2.GetReference<KImage>("Background");
				KImage reference = component2.GetReference<KImage>("DLCOverlay");
				bool flag = techItem.requiredDlcIds != null;
				reference.gameObject.SetActive(flag);
				if (flag)
				{
					reference.color = DlcManager.GetDlcBannerColor(techItem.requiredDlcIds[techItem.requiredDlcIds.Length - 1]);
				}
				string text2 = string.Format("{0}\n{1}", techItem.Name, techItem.description);
				if (flag)
				{
					text2 += "\n";
					foreach (string text3 in techItem.requiredDlcIds)
					{
						text2 += string.Format(RESEARCH.MESSAGING.DLC.DLC_CONTENT, DlcManager.GetDlcTitle(text3));
					}
				}
				component2.GetComponent<ToolTip>().toolTip = text2;
			}
		}
		text = string.Format(UI.RESEARCHSCREEN_UNLOCKSTOOLTIP, text);
		this.researchName.GetComponent<ToolTip>().toolTip = string.Format("{0}\n{1}\n\n{2}", this.targetTech.Name, this.targetTech.desc, text);
		this.toggle.ClearOnClick();
		this.toggle.onClick += this.OnResearchClicked;
		this.toggle.onPointerEnter += delegate
		{
			this.researchScreen.TurnEverythingOff();
			this.OnHover(true, this.targetTech);
		};
		this.toggle.soundPlayer.AcceptClickCondition = () => !this.targetTech.IsComplete();
		this.toggle.onPointerExit += delegate
		{
			this.researchScreen.TurnEverythingOff();
		};
	}

	// Token: 0x06006DA7 RID: 28071 RVA: 0x00298688 File Offset: 0x00296888
	public void SetEverythingOff()
	{
		if (!this.isOn)
		{
			return;
		}
		this.borderHighlight.gameObject.SetActive(false);
		foreach (KeyValuePair<Tech, UILineRenderer> keyValuePair in this.techLineMap)
		{
			keyValuePair.Value.LineThickness = (float)this.lineThickness_inactive;
			keyValuePair.Value.color = this.inactiveLineColor;
		}
		this.isOn = false;
	}

	// Token: 0x06006DA8 RID: 28072 RVA: 0x0029871C File Offset: 0x0029691C
	public void SetEverythingOn()
	{
		if (this.isOn)
		{
			return;
		}
		this.UpdateProgressBars();
		this.borderHighlight.gameObject.SetActive(true);
		foreach (KeyValuePair<Tech, UILineRenderer> keyValuePair in this.techLineMap)
		{
			keyValuePair.Value.LineThickness = (float)this.lineThickness_active;
			keyValuePair.Value.color = this.activeLineColor;
		}
		base.transform.SetAsLastSibling();
		this.isOn = true;
	}

	// Token: 0x06006DA9 RID: 28073 RVA: 0x002987C0 File Offset: 0x002969C0
	public void OnHover(bool entered, Tech hoverSource)
	{
		this.SetEverythingOn();
		foreach (Tech tech in this.targetTech.requiredTech)
		{
			ResearchEntry entry = this.researchScreen.GetEntry(tech);
			if (entry != null)
			{
				entry.OnHover(entered, this.targetTech);
			}
		}
	}

	// Token: 0x06006DAA RID: 28074 RVA: 0x0029883C File Offset: 0x00296A3C
	private void OnResearchClicked()
	{
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch != null && activeResearch.tech != this.targetTech)
		{
			this.researchScreen.CancelResearch();
		}
		Research.Instance.SetActiveResearch(this.targetTech, true);
		if (DebugHandler.InstantBuildMode)
		{
			Research.Instance.CompleteQueue();
		}
		this.UpdateProgressBars();
	}

	// Token: 0x06006DAB RID: 28075 RVA: 0x00298898 File Offset: 0x00296A98
	private void OnResearchCanceled()
	{
		if (this.targetTech.IsComplete())
		{
			return;
		}
		this.toggle.ClearOnClick();
		this.toggle.onClick += this.OnResearchClicked;
		this.researchScreen.CancelResearch();
		Research.Instance.CancelResearch(this.targetTech, true);
	}

	// Token: 0x06006DAC RID: 28076 RVA: 0x002988F4 File Offset: 0x00296AF4
	public void QueueStateChanged(bool isSelected)
	{
		if (isSelected)
		{
			if (!this.targetTech.IsComplete())
			{
				this.toggle.isOn = true;
				this.BG.color = this.pendingColor;
				this.titleBG.color = this.pendingHeaderColor;
				this.toggle.ClearOnClick();
				this.toggle.onClick += this.OnResearchCanceled;
			}
			else
			{
				this.toggle.isOn = false;
			}
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.progressBarsByResearchTypeID)
			{
				keyValuePair.Value.transform.GetChild(0).GetComponentsInChildren<Image>()[1].color = Color.white;
			}
			Image[] array = this.iconPanel.GetComponentsInChildren<Image>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].material = this.StandardUIMaterial;
			}
			return;
		}
		if (this.targetTech.IsComplete())
		{
			this.toggle.isOn = false;
			this.BG.color = this.completedColor;
			this.titleBG.color = this.completedHeaderColor;
			this.defaultColor = this.completedColor;
			this.toggle.ClearOnClick();
			foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.progressBarsByResearchTypeID)
			{
				keyValuePair2.Value.transform.GetChild(0).GetComponentsInChildren<Image>()[1].color = Color.white;
			}
			Image[] array = this.iconPanel.GetComponentsInChildren<Image>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].material = this.StandardUIMaterial;
			}
			return;
		}
		this.toggle.isOn = false;
		this.BG.color = this.defaultColor;
		this.titleBG.color = this.incompleteHeaderColor;
		this.toggle.ClearOnClick();
		this.toggle.onClick += this.OnResearchClicked;
		foreach (KeyValuePair<string, GameObject> keyValuePair3 in this.progressBarsByResearchTypeID)
		{
			keyValuePair3.Value.transform.GetChild(0).GetComponentsInChildren<Image>()[1].color = new Color(0.52156866f, 0.52156866f, 0.52156866f);
		}
	}

	// Token: 0x06006DAD RID: 28077 RVA: 0x00298B98 File Offset: 0x00296D98
	public void UpdateFilterState(bool state)
	{
		this.filterLowlight.gameObject.SetActive(!state);
	}

	// Token: 0x06006DAE RID: 28078 RVA: 0x00298BBB File Offset: 0x00296DBB
	public void SetPercentage(float percent)
	{
	}

	// Token: 0x06006DAF RID: 28079 RVA: 0x00298BC0 File Offset: 0x00296DC0
	public void UpdateProgressBars()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.progressBarsByResearchTypeID)
		{
			Transform child = keyValuePair.Value.transform.GetChild(0);
			float num;
			if (this.targetTech.IsComplete())
			{
				num = 1f;
				child.GetComponentInChildren<LocText>().text = this.targetTech.costsByResearchTypeID[keyValuePair.Key].ToString() + "/" + this.targetTech.costsByResearchTypeID[keyValuePair.Key].ToString();
			}
			else
			{
				TechInstance orAdd = Research.Instance.GetOrAdd(this.targetTech);
				if (orAdd == null)
				{
					continue;
				}
				child.GetComponentInChildren<LocText>().text = orAdd.progressInventory.PointsByTypeID[keyValuePair.Key].ToString() + "/" + this.targetTech.costsByResearchTypeID[keyValuePair.Key].ToString();
				num = orAdd.progressInventory.PointsByTypeID[keyValuePair.Key] / this.targetTech.costsByResearchTypeID[keyValuePair.Key];
			}
			child.GetComponentsInChildren<Image>()[2].fillAmount = num;
			child.GetComponent<ToolTip>().SetSimpleTooltip(Research.Instance.researchTypes.GetResearchType(keyValuePair.Key).description);
		}
	}

	// Token: 0x06006DB0 RID: 28080 RVA: 0x00298D78 File Offset: 0x00296F78
	private GameObject GetFreeIcon()
	{
		GameObject gameObject = Util.KInstantiateUI(this.iconPrefab, this.iconPanel, false);
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06006DB1 RID: 28081 RVA: 0x00298D93 File Offset: 0x00296F93
	private Image GetFreeLine()
	{
		return Util.KInstantiateUI<Image>(this.linePrefab.gameObject, base.gameObject, false);
	}

	// Token: 0x06006DB2 RID: 28082 RVA: 0x00298DAC File Offset: 0x00296FAC
	public void ResearchCompleted(bool notify = true)
	{
		this.BG.color = this.completedColor;
		this.titleBG.color = this.completedHeaderColor;
		this.defaultColor = this.completedColor;
		if (notify)
		{
			this.unlockedTechMetric[ResearchEntry.UnlockedTechKey] = this.targetTech.Id;
			ThreadedHttps<KleiMetrics>.Instance.SendEvent(this.unlockedTechMetric, "ResearchCompleted");
		}
		this.toggle.ClearOnClick();
		if (notify)
		{
			ResearchCompleteMessage researchCompleteMessage = new ResearchCompleteMessage(this.targetTech);
			MusicManager.instance.PlaySong("Stinger_ResearchComplete", false);
			Messenger.Instance.QueueMessage(researchCompleteMessage);
		}
	}

	// Token: 0x04004AFA RID: 19194
	[Header("Labels")]
	[SerializeField]
	private LocText researchName;

	// Token: 0x04004AFB RID: 19195
	[Header("Transforms")]
	[SerializeField]
	private Transform progressBarContainer;

	// Token: 0x04004AFC RID: 19196
	[SerializeField]
	private Transform lineContainer;

	// Token: 0x04004AFD RID: 19197
	[Header("Prefabs")]
	[SerializeField]
	private GameObject iconPanel;

	// Token: 0x04004AFE RID: 19198
	[SerializeField]
	private GameObject iconPrefab;

	// Token: 0x04004AFF RID: 19199
	[SerializeField]
	private GameObject linePrefab;

	// Token: 0x04004B00 RID: 19200
	[SerializeField]
	private GameObject progressBarPrefab;

	// Token: 0x04004B01 RID: 19201
	[Header("Graphics")]
	[SerializeField]
	private Image BG;

	// Token: 0x04004B02 RID: 19202
	[SerializeField]
	private Image titleBG;

	// Token: 0x04004B03 RID: 19203
	[SerializeField]
	private Image borderHighlight;

	// Token: 0x04004B04 RID: 19204
	[SerializeField]
	private Image filterHighlight;

	// Token: 0x04004B05 RID: 19205
	[SerializeField]
	private Image filterLowlight;

	// Token: 0x04004B06 RID: 19206
	[SerializeField]
	private Sprite hoverBG;

	// Token: 0x04004B07 RID: 19207
	[SerializeField]
	private Sprite completedBG;

	// Token: 0x04004B08 RID: 19208
	[Header("Colors")]
	[SerializeField]
	private Color defaultColor = Color.blue;

	// Token: 0x04004B09 RID: 19209
	[SerializeField]
	private Color completedColor = Color.yellow;

	// Token: 0x04004B0A RID: 19210
	[SerializeField]
	private Color pendingColor = Color.magenta;

	// Token: 0x04004B0B RID: 19211
	[SerializeField]
	private Color completedHeaderColor = Color.grey;

	// Token: 0x04004B0C RID: 19212
	[SerializeField]
	private Color incompleteHeaderColor = Color.grey;

	// Token: 0x04004B0D RID: 19213
	[SerializeField]
	private Color pendingHeaderColor = Color.grey;

	// Token: 0x04004B0E RID: 19214
	private Sprite defaultBG;

	// Token: 0x04004B0F RID: 19215
	[MyCmpGet]
	private KToggle toggle;

	// Token: 0x04004B10 RID: 19216
	private ResearchScreen researchScreen;

	// Token: 0x04004B11 RID: 19217
	private Dictionary<Tech, UILineRenderer> techLineMap;

	// Token: 0x04004B12 RID: 19218
	private Tech targetTech;

	// Token: 0x04004B13 RID: 19219
	private bool isOn = true;

	// Token: 0x04004B14 RID: 19220
	private Coroutine fadeRoutine;

	// Token: 0x04004B15 RID: 19221
	public Color activeLineColor;

	// Token: 0x04004B16 RID: 19222
	public Color inactiveLineColor;

	// Token: 0x04004B17 RID: 19223
	public int lineThickness_active = 6;

	// Token: 0x04004B18 RID: 19224
	public int lineThickness_inactive = 2;

	// Token: 0x04004B19 RID: 19225
	public Material StandardUIMaterial;

	// Token: 0x04004B1A RID: 19226
	private Dictionary<string, GameObject> progressBarsByResearchTypeID = new Dictionary<string, GameObject>();

	// Token: 0x04004B1B RID: 19227
	public static readonly string UnlockedTechKey = "UnlockedTech";

	// Token: 0x04004B1C RID: 19228
	private Dictionary<string, object> unlockedTechMetric = new Dictionary<string, object> { 
	{
		ResearchEntry.UnlockedTechKey,
		null
	} };
}
