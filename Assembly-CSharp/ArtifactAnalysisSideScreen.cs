using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DD1 RID: 3537
public class ArtifactAnalysisSideScreen : SideScreenContent
{
	// Token: 0x06006F96 RID: 28566 RVA: 0x002A78B0 File Offset: 0x002A5AB0
	public override string GetTitle()
	{
		if (this.targetArtifactStation != null)
		{
			return string.Format(base.GetTitle(), this.targetArtifactStation.GetProperName());
		}
		return base.GetTitle();
	}

	// Token: 0x06006F97 RID: 28567 RVA: 0x002A78DD File Offset: 0x002A5ADD
	public override void ClearTarget()
	{
		this.targetArtifactStation = null;
		base.ClearTarget();
	}

	// Token: 0x06006F98 RID: 28568 RVA: 0x002A78EC File Offset: 0x002A5AEC
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetSMI<ArtifactAnalysisStation.StatesInstance>() != null;
	}

	// Token: 0x06006F99 RID: 28569 RVA: 0x002A78F8 File Offset: 0x002A5AF8
	private void RefreshRows()
	{
		if (this.undiscoveredRow == null)
		{
			this.undiscoveredRow = Util.KInstantiateUI(this.rowPrefab, this.rowContainer, true);
			HierarchyReferences component = this.undiscoveredRow.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("label").SetText(UI.UISIDESCREENS.ARTIFACTANALYSISSIDESCREEN.NO_ARTIFACTS_DISCOVERED);
			component.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.ARTIFACTANALYSISSIDESCREEN.NO_ARTIFACTS_DISCOVERED_TOOLTIP);
			component.GetReference<Image>("icon").sprite = Assets.GetSprite("unknown");
			component.GetReference<Image>("icon").color = Color.grey;
		}
		List<string> analyzedArtifactIDs = ArtifactSelector.Instance.GetAnalyzedArtifactIDs();
		this.undiscoveredRow.SetActive(analyzedArtifactIDs.Count == 0);
		foreach (string text in analyzedArtifactIDs)
		{
			if (!this.rows.ContainsKey(text))
			{
				GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.rowContainer, true);
				this.rows.Add(text, gameObject);
				GameObject artifactPrefab = Assets.GetPrefab(text);
				HierarchyReferences component2 = gameObject.GetComponent<HierarchyReferences>();
				component2.GetReference<LocText>("label").SetText(artifactPrefab.GetProperName());
				component2.GetReference<Image>("icon").sprite = Def.GetUISprite(artifactPrefab, text, false).first;
				component2.GetComponent<KButton>().onClick += delegate
				{
					this.OpenEvent(artifactPrefab);
				};
			}
		}
	}

	// Token: 0x06006F9A RID: 28570 RVA: 0x002A7AA8 File Offset: 0x002A5CA8
	private void OpenEvent(GameObject artifactPrefab)
	{
		SimpleEvent.StatesInstance statesInstance = GameplayEventManager.Instance.StartNewEvent(Db.Get().GameplayEvents.ArtifactReveal, -1, null).smi as SimpleEvent.StatesInstance;
		statesInstance.artifact = artifactPrefab;
		artifactPrefab.GetComponent<KPrefabID>();
		artifactPrefab.GetComponent<InfoDescription>();
		string text = artifactPrefab.PrefabID().Name.ToUpper();
		text = text.Replace("ARTIFACT_", "");
		string text2 = "STRINGS.UI.SPACEARTIFACTS." + text + ".ARTIFACT";
		string text3 = string.Format("<b>{0}</b>", artifactPrefab.GetProperName());
		StringEntry stringEntry;
		Strings.TryGet(text2, out stringEntry);
		if (stringEntry != null && !stringEntry.String.IsNullOrWhiteSpace())
		{
			text3 = text3 + "\n\n" + stringEntry.String;
		}
		if (text3 != null && !text3.IsNullOrWhiteSpace())
		{
			statesInstance.SetTextParameter("desc", text3);
		}
		statesInstance.ShowEventPopup();
	}

	// Token: 0x06006F9B RID: 28571 RVA: 0x002A7B7E File Offset: 0x002A5D7E
	public override void SetTarget(GameObject target)
	{
		this.targetArtifactStation = target;
		base.SetTarget(target);
		this.RefreshRows();
	}

	// Token: 0x04004CBF RID: 19647
	[SerializeField]
	private GameObject rowPrefab;

	// Token: 0x04004CC0 RID: 19648
	private GameObject targetArtifactStation;

	// Token: 0x04004CC1 RID: 19649
	[SerializeField]
	private GameObject rowContainer;

	// Token: 0x04004CC2 RID: 19650
	private Dictionary<string, GameObject> rows = new Dictionary<string, GameObject>();

	// Token: 0x04004CC3 RID: 19651
	private GameObject undiscoveredRow;
}
