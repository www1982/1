using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DFD RID: 3581
public class HarvestModuleSideScreen : SideScreenContent, ISimEveryTick
{
	// Token: 0x170007C5 RID: 1989
	// (get) Token: 0x06007104 RID: 28932 RVA: 0x002AFBD0 File Offset: 0x002ADDD0
	private CraftModuleInterface craftModuleInterface
	{
		get
		{
			return this.targetCraft.GetComponent<CraftModuleInterface>();
		}
	}

	// Token: 0x06007105 RID: 28933 RVA: 0x002AFBDD File Offset: 0x002ADDDD
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06007106 RID: 28934 RVA: 0x002AFBED File Offset: 0x002ADDED
	public override float GetSortKey()
	{
		return 21f;
	}

	// Token: 0x06007107 RID: 28935 RVA: 0x002AFBF4 File Offset: 0x002ADDF4
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Clustercraft>() != null && this.GetResourceHarvestModule(target.GetComponent<Clustercraft>()) != null;
	}

	// Token: 0x06007108 RID: 28936 RVA: 0x002AFC18 File Offset: 0x002ADE18
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetCraft = target.GetComponent<Clustercraft>();
		ResourceHarvestModule.StatesInstance resourceHarvestModule = this.GetResourceHarvestModule(this.targetCraft);
		this.RefreshModulePanel(resourceHarvestModule);
	}

	// Token: 0x06007109 RID: 28937 RVA: 0x002AFC4C File Offset: 0x002ADE4C
	private ResourceHarvestModule.StatesInstance GetResourceHarvestModule(Clustercraft craft)
	{
		foreach (Ref<RocketModuleCluster> @ref in craft.GetComponent<CraftModuleInterface>().ClusterModules)
		{
			GameObject gameObject = @ref.Get().gameObject;
			if (gameObject.GetDef<ResourceHarvestModule.Def>() != null)
			{
				return gameObject.GetSMI<ResourceHarvestModule.StatesInstance>();
			}
		}
		return null;
	}

	// Token: 0x0600710A RID: 28938 RVA: 0x002AFCB8 File Offset: 0x002ADEB8
	private void RefreshModulePanel(StateMachine.Instance module)
	{
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("icon").sprite = Def.GetUISprite(module.gameObject, "ui", false).first;
		component.GetReference<LocText>("label").SetText(module.gameObject.GetProperName());
	}

	// Token: 0x0600710B RID: 28939 RVA: 0x002AFD0C File Offset: 0x002ADF0C
	public void SimEveryTick(float dt)
	{
		if (this.targetCraft.IsNullOrDestroyed())
		{
			return;
		}
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		ResourceHarvestModule.StatesInstance resourceHarvestModule = this.GetResourceHarvestModule(this.targetCraft);
		if (resourceHarvestModule == null)
		{
			return;
		}
		GenericUIProgressBar reference = component.GetReference<GenericUIProgressBar>("progressBar");
		float num = 4f;
		float num2 = resourceHarvestModule.timeinstate % num;
		if (resourceHarvestModule.sm.canHarvest.Get(resourceHarvestModule))
		{
			reference.SetFillPercentage(num2 / num);
			reference.label.SetText(UI.UISIDESCREENS.HARVESTMODULESIDESCREEN.MINING_IN_PROGRESS);
		}
		else
		{
			reference.SetFillPercentage(0f);
			reference.label.SetText(UI.UISIDESCREENS.HARVESTMODULESIDESCREEN.MINING_STOPPED);
		}
		GenericUIProgressBar reference2 = component.GetReference<GenericUIProgressBar>("diamondProgressBar");
		Storage component2 = resourceHarvestModule.GetComponent<Storage>();
		float num3 = component2.MassStored() / component2.Capacity();
		reference2.SetFillPercentage(num3);
		reference2.label.SetText(ElementLoader.GetElement(SimHashes.Diamond.CreateTag()).name + ": " + GameUtil.GetFormattedMass(component2.MassStored(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	}

	// Token: 0x04004DC6 RID: 19910
	private Clustercraft targetCraft;

	// Token: 0x04004DC7 RID: 19911
	public GameObject moduleContentContainer;

	// Token: 0x04004DC8 RID: 19912
	public GameObject modulePanelPrefab;
}
