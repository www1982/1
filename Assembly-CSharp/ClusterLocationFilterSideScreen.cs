using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DE3 RID: 3555
public class ClusterLocationFilterSideScreen : SideScreenContent
{
	// Token: 0x0600704B RID: 28747 RVA: 0x002AB039 File Offset: 0x002A9239
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<LogicClusterLocationSensor>() != null;
	}

	// Token: 0x0600704C RID: 28748 RVA: 0x002AB047 File Offset: 0x002A9247
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.sensor = target.GetComponent<LogicClusterLocationSensor>();
		this.Build();
	}

	// Token: 0x0600704D RID: 28749 RVA: 0x002AB064 File Offset: 0x002A9264
	private void ClearRows()
	{
		if (this.emptySpaceRow != null)
		{
			Util.KDestroyGameObject(this.emptySpaceRow);
		}
		foreach (KeyValuePair<AxialI, GameObject> keyValuePair in this.worldRows)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.worldRows.Clear();
	}

	// Token: 0x0600704E RID: 28750 RVA: 0x002AB0E0 File Offset: 0x002A92E0
	private void Build()
	{
		this.headerLabel.SetText(UI.UISIDESCREENS.CLUSTERLOCATIONFILTERSIDESCREEN.HEADER);
		this.ClearRows();
		this.emptySpaceRow = Util.KInstantiateUI(this.rowPrefab, this.listContainer, false);
		this.emptySpaceRow.SetActive(true);
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			if (!worldContainer.IsModuleInterior)
			{
				GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.listContainer, false);
				gameObject.gameObject.name = worldContainer.GetProperName();
				AxialI myWorldLocation = worldContainer.GetMyWorldLocation();
				global::Debug.Assert(!this.worldRows.ContainsKey(myWorldLocation), "Adding two worlds/POI with the same cluster location to ClusterLocationFilterSideScreen UI: " + worldContainer.GetProperName());
				this.worldRows.Add(myWorldLocation, gameObject);
			}
		}
		this.Refresh();
	}

	// Token: 0x0600704F RID: 28751 RVA: 0x002AB1DC File Offset: 0x002A93DC
	private void Refresh()
	{
		this.emptySpaceRow.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(UI.UISIDESCREENS.CLUSTERLOCATIONFILTERSIDESCREEN.EMPTY_SPACE_ROW);
		this.emptySpaceRow.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = Def.GetUISprite("hex_soft", "ui", false).first;
		this.emptySpaceRow.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").color = Color.black;
		this.emptySpaceRow.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").onClick = delegate
		{
			this.sensor.SetSpaceEnabled(!this.sensor.ActiveInSpace);
			this.Refresh();
		};
		this.emptySpaceRow.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").ChangeState(this.sensor.ActiveInSpace ? 1 : 0);
		using (Dictionary<AxialI, GameObject>.Enumerator enumerator = this.worldRows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<AxialI, GameObject> kvp = enumerator.Current;
				ClusterGridEntity clusterGridEntity = ClusterGrid.Instance.cellContents[kvp.Key][0];
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(clusterGridEntity.GetProperName());
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = Def.GetUISprite(clusterGridEntity, "ui", false).first;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").color = Def.GetUISprite(clusterGridEntity, "ui", false).second;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").onClick = delegate
				{
					this.sensor.SetLocationEnabled(kvp.Key, !this.sensor.CheckLocationSelected(kvp.Key));
					this.Refresh();
				};
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").ChangeState(this.sensor.CheckLocationSelected(kvp.Key) ? 1 : 0);
				kvp.Value.SetActive(ClusterGrid.Instance.GetCellRevealLevel(kvp.Key) == ClusterRevealLevel.Visible);
			}
		}
	}

	// Token: 0x04004D3A RID: 19770
	private LogicClusterLocationSensor sensor;

	// Token: 0x04004D3B RID: 19771
	[SerializeField]
	private GameObject rowPrefab;

	// Token: 0x04004D3C RID: 19772
	[SerializeField]
	private GameObject listContainer;

	// Token: 0x04004D3D RID: 19773
	[SerializeField]
	private LocText headerLabel;

	// Token: 0x04004D3E RID: 19774
	private Dictionary<AxialI, GameObject> worldRows = new Dictionary<AxialI, GameObject>();

	// Token: 0x04004D3F RID: 19775
	private GameObject emptySpaceRow;
}
