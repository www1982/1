using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E5E RID: 3678
public class SubSpeciesInfoScreen : KModalScreen
{
	// Token: 0x0600754B RID: 30027 RVA: 0x002CEBA4 File Offset: 0x002CCDA4
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x0600754C RID: 30028 RVA: 0x002CEBA7 File Offset: 0x002CCDA7
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x0600754D RID: 30029 RVA: 0x002CEBB0 File Offset: 0x002CCDB0
	private void ClearMutations()
	{
		for (int i = this.mutationLineItems.Count - 1; i >= 0; i--)
		{
			Util.KDestroyGameObject(this.mutationLineItems[i]);
		}
		this.mutationLineItems.Clear();
	}

	// Token: 0x0600754E RID: 30030 RVA: 0x002CEBF1 File Offset: 0x002CCDF1
	public void DisplayDiscovery(Tag speciesID, Tag subSpeciesID, GeneticAnalysisStation station)
	{
		this.SetSubspecies(speciesID, subSpeciesID);
		this.targetStation = station;
	}

	// Token: 0x0600754F RID: 30031 RVA: 0x002CEC04 File Offset: 0x002CCE04
	private void SetSubspecies(Tag speciesID, Tag subSpeciesID)
	{
		this.ClearMutations();
		ref PlantSubSpeciesCatalog.SubSpeciesInfo subSpecies = PlantSubSpeciesCatalog.Instance.GetSubSpecies(speciesID, subSpeciesID);
		this.plantIcon.sprite = Def.GetUISprite(Assets.GetPrefab(speciesID), "ui", false).first;
		foreach (string text in subSpecies.mutationIDs)
		{
			PlantMutation plantMutation = Db.Get().PlantMutations.Get(text);
			GameObject gameObject = Util.KInstantiateUI(this.mutationsItemPrefab, this.mutationsList.gameObject, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("nameLabel").text = plantMutation.Name;
			component.GetReference<LocText>("descriptionLabel").text = plantMutation.description;
			this.mutationLineItems.Add(gameObject);
		}
	}

	// Token: 0x04005155 RID: 20821
	[SerializeField]
	private KButton renameButton;

	// Token: 0x04005156 RID: 20822
	[SerializeField]
	private KButton saveButton;

	// Token: 0x04005157 RID: 20823
	[SerializeField]
	private KButton discardButton;

	// Token: 0x04005158 RID: 20824
	[SerializeField]
	private RectTransform mutationsList;

	// Token: 0x04005159 RID: 20825
	[SerializeField]
	private Image plantIcon;

	// Token: 0x0400515A RID: 20826
	[SerializeField]
	private GameObject mutationsItemPrefab;

	// Token: 0x0400515B RID: 20827
	private List<GameObject> mutationLineItems = new List<GameObject>();

	// Token: 0x0400515C RID: 20828
	private GeneticAnalysisStation targetStation;
}
