using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E0A RID: 3594
public class MissileSelectionSideScreen : SideScreenContent
{
	// Token: 0x0600716C RID: 29036 RVA: 0x002B258C File Offset: 0x002B078C
	public override int GetSideScreenSortOrder()
	{
		return 500;
	}

	// Token: 0x0600716D RID: 29037 RVA: 0x002B2593 File Offset: 0x002B0793
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetSMI<MissileLauncher.Instance>() != null;
	}

	// Token: 0x0600716E RID: 29038 RVA: 0x002B259E File Offset: 0x002B079E
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetMissileLauncher = target.GetSMI<MissileLauncher.Instance>();
		this.Build();
	}

	// Token: 0x0600716F RID: 29039 RVA: 0x002B25BC File Offset: 0x002B07BC
	private void Build()
	{
		foreach (KeyValuePair<Tag, GameObject> keyValuePair in this.rows)
		{
			Util.KDestroyGameObject(keyValuePair.Value);
		}
		this.rows.Clear();
		this.UpdateLongRangeMissiles();
		foreach (Tag tag in this.ammunitiontags)
		{
			GameObject gameObject = Util.KInstantiateUI(this.rowPrefab, this.listContainer, false);
			gameObject.gameObject.name = tag.ProperName();
			this.rows.Add(tag, gameObject);
		}
		this.Refresh();
	}

	// Token: 0x06007170 RID: 29040 RVA: 0x002B269C File Offset: 0x002B089C
	private void UpdateLongRangeMissiles()
	{
		if (DlcManager.IsExpansion1Active())
		{
			if (!this.ammunitiontags.Contains("MissileLongRange"))
			{
				this.ammunitiontags.Add("MissileLongRange");
				return;
			}
		}
		else
		{
			if (GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.IdHash, -1) == null)
			{
				this.ammunitiontags.Remove("MissileLongRange");
				return;
			}
			if (!this.ammunitiontags.Contains("MissileLongRange"))
			{
				this.ammunitiontags.Add("MissileLongRange");
			}
		}
	}

	// Token: 0x06007171 RID: 29041 RVA: 0x002B2744 File Offset: 0x002B0944
	private void Refresh()
	{
		using (Dictionary<Tag, GameObject>.Enumerator enumerator = this.rows.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<Tag, GameObject> kvp = enumerator.Current;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(kvp.Key.ProperNameStripLink());
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = Def.GetUISprite(kvp.Key, "ui", false).first;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").color = Def.GetUISprite(kvp.Key, "ui", false).second;
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").onClick = delegate
				{
					this.targetMissileLauncher.ChangeAmmunition(kvp.Key, !this.targetMissileLauncher.AmmunitionIsAllowed(kvp.Key));
					ClusterDestinationSelector component = this.targetMissileLauncher.GetComponent<ClusterDestinationSelector>();
					if (component != null)
					{
						component.assignable = this.targetMissileLauncher.AmmunitionIsAllowed("MissileLongRange");
					}
					this.targetMissileLauncher.GetComponent<FlatTagFilterable>().currentlyUserAssignable = this.targetMissileLauncher.AmmunitionIsAllowed("MissileBasic");
					DetailsScreen.Instance.Refresh(SelectTool.Instance.selected.gameObject);
					this.Refresh();
				};
				kvp.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").ChangeState(this.targetMissileLauncher.AmmunitionIsAllowed(kvp.Key) ? 1 : 0);
				kvp.Value.SetActive(true);
			}
		}
	}

	// Token: 0x06007172 RID: 29042 RVA: 0x002B28D8 File Offset: 0x002B0AD8
	public override string GetTitle()
	{
		return UI.UISIDESCREENS.MISSILESELECTIONSIDESCREEN.TITLE;
	}

	// Token: 0x04004E1A RID: 19994
	private MissileLauncher.Instance targetMissileLauncher;

	// Token: 0x04004E1B RID: 19995
	[SerializeField]
	private GameObject rowPrefab;

	// Token: 0x04004E1C RID: 19996
	[SerializeField]
	private GameObject listContainer;

	// Token: 0x04004E1D RID: 19997
	[SerializeField]
	private LocText headerLabel;

	// Token: 0x04004E1E RID: 19998
	private List<Tag> ammunitiontags = new List<Tag> { "MissileBasic" };

	// Token: 0x04004E1F RID: 19999
	private Dictionary<Tag, GameObject> rows = new Dictionary<Tag, GameObject>();
}
