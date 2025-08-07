using System;
using System.Collections.Generic;
using Database;
using KSerialization;
using UnityEngine;

// Token: 0x020006E6 RID: 1766
[SerializationConfig(MemberSerialization.OptIn)]
public class BuildingFacade : KMonoBehaviour
{
	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06002BDE RID: 11230 RVA: 0x000FC99A File Offset: 0x000FAB9A
	public string CurrentFacade
	{
		get
		{
			return this.currentFacade;
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06002BDF RID: 11231 RVA: 0x000FC9A2 File Offset: 0x000FABA2
	public bool IsOriginal
	{
		get
		{
			return this.currentFacade.IsNullOrWhiteSpace();
		}
	}

	// Token: 0x06002BE0 RID: 11232 RVA: 0x000FC9AF File Offset: 0x000FABAF
	protected override void OnPrefabInit()
	{
	}

	// Token: 0x06002BE1 RID: 11233 RVA: 0x000FC9B1 File Offset: 0x000FABB1
	protected override void OnSpawn()
	{
		if (!this.IsOriginal)
		{
			this.ApplyBuildingFacade(Db.GetBuildingFacades().TryGet(this.currentFacade), false);
		}
	}

	// Token: 0x06002BE2 RID: 11234 RVA: 0x000FC9D2 File Offset: 0x000FABD2
	public void ApplyDefaultFacade(bool shouldTryAnimate = false)
	{
		this.currentFacade = "DEFAULT_FACADE";
		this.ClearFacade(shouldTryAnimate);
	}

	// Token: 0x06002BE3 RID: 11235 RVA: 0x000FC9E8 File Offset: 0x000FABE8
	public void ApplyBuildingFacade(BuildingFacadeResource facade, bool shouldTryAnimate = false)
	{
		if (facade == null)
		{
			this.ClearFacade(false);
			return;
		}
		this.currentFacade = facade.Id;
		KAnimFile[] array = new KAnimFile[] { Assets.GetAnim(facade.AnimFile) };
		this.ChangeBuilding(array, facade.Name, facade.Description, facade.InteractFile, shouldTryAnimate);
	}

	// Token: 0x06002BE4 RID: 11236 RVA: 0x000FCA40 File Offset: 0x000FAC40
	private void ClearFacade(bool shouldTryAnimate = false)
	{
		Building component = base.GetComponent<Building>();
		this.ChangeBuilding(component.Def.AnimFiles, component.Def.Name, component.Def.Desc, null, shouldTryAnimate);
	}

	// Token: 0x06002BE5 RID: 11237 RVA: 0x000FCA80 File Offset: 0x000FAC80
	private void ChangeBuilding(KAnimFile[] animFiles, string displayName, string desc, Dictionary<string, string> interactAnimsNames = null, bool shouldTryAnimate = false)
	{
		this.interactAnims.Clear();
		if (interactAnimsNames != null && interactAnimsNames.Count > 0)
		{
			this.interactAnims = new Dictionary<string, KAnimFile[]>();
			foreach (KeyValuePair<string, string> keyValuePair in interactAnimsNames)
			{
				this.interactAnims.Add(keyValuePair.Key, new KAnimFile[] { Assets.GetAnim(keyValuePair.Value) });
			}
		}
		Building[] components = base.GetComponents<Building>();
		foreach (Building building in components)
		{
			building.SetDescriptionFlavour(desc);
			KBatchedAnimController component = building.GetComponent<KBatchedAnimController>();
			HashedString batchGroupID = component.batchGroupID;
			component.SwapAnims(animFiles);
			foreach (KBatchedAnimController kbatchedAnimController in building.GetComponentsInChildren<KBatchedAnimController>(true))
			{
				if (kbatchedAnimController.batchGroupID == batchGroupID)
				{
					kbatchedAnimController.SwapAnims(animFiles);
				}
			}
			if (!this.animateIn.IsNullOrDestroyed())
			{
				global::UnityEngine.Object.Destroy(this.animateIn);
				this.animateIn = null;
			}
			if (shouldTryAnimate)
			{
				this.animateIn = BuildingFacadeAnimateIn.MakeFor(component);
				string text = "Unlocked";
				float num = 1f;
				KFMOD.PlayUISoundWithParameter(GlobalAssets.GetSound(KleiInventoryScreen.GetFacadeItemSoundName(Db.Get().Permits.TryGet(this.currentFacade)) + "_Click", false), text, num);
			}
		}
		base.GetComponent<KSelectable>().SetName(displayName);
		if (base.GetComponent<AnimTileable>() != null && components.Length != 0)
		{
			GameScenePartitioner.Instance.TriggerEvent(components[0].GetExtents(), GameScenePartitioner.Instance.objectLayers[1], null);
		}
	}

	// Token: 0x06002BE6 RID: 11238 RVA: 0x000FCC48 File Offset: 0x000FAE48
	public string GetNextFacade()
	{
		BuildingDef def = base.GetComponent<Building>().Def;
		int num = def.AvailableFacades.FindIndex((string s) => s == this.currentFacade) + 1;
		if (num >= def.AvailableFacades.Count)
		{
			num = 0;
		}
		return def.AvailableFacades[num];
	}

	// Token: 0x040019EB RID: 6635
	[Serialize]
	private string currentFacade;

	// Token: 0x040019EC RID: 6636
	public KAnimFile[] animFiles;

	// Token: 0x040019ED RID: 6637
	public Dictionary<string, KAnimFile[]> interactAnims = new Dictionary<string, KAnimFile[]>();

	// Token: 0x040019EE RID: 6638
	private BuildingFacadeAnimateIn animateIn;
}
