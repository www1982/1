using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DFC RID: 3580
public class HabitatModuleSideScreen : SideScreenContent
{
	// Token: 0x170007C4 RID: 1988
	// (get) Token: 0x060070FC RID: 28924 RVA: 0x002AFA57 File Offset: 0x002ADC57
	private CraftModuleInterface craftModuleInterface
	{
		get
		{
			return this.targetCraft.GetComponent<CraftModuleInterface>();
		}
	}

	// Token: 0x060070FD RID: 28925 RVA: 0x002AFA64 File Offset: 0x002ADC64
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x060070FE RID: 28926 RVA: 0x002AFA74 File Offset: 0x002ADC74
	public override float GetSortKey()
	{
		return 21f;
	}

	// Token: 0x060070FF RID: 28927 RVA: 0x002AFA7B File Offset: 0x002ADC7B
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Clustercraft>() != null && this.GetPassengerModule(target.GetComponent<Clustercraft>()) != null;
	}

	// Token: 0x06007100 RID: 28928 RVA: 0x002AFAA0 File Offset: 0x002ADCA0
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetCraft = target.GetComponent<Clustercraft>();
		PassengerRocketModule passengerModule = this.GetPassengerModule(this.targetCraft);
		this.RefreshModulePanel(passengerModule);
	}

	// Token: 0x06007101 RID: 28929 RVA: 0x002AFAD4 File Offset: 0x002ADCD4
	private PassengerRocketModule GetPassengerModule(Clustercraft craft)
	{
		foreach (Ref<RocketModuleCluster> @ref in craft.GetComponent<CraftModuleInterface>().ClusterModules)
		{
			PassengerRocketModule component = @ref.Get().GetComponent<PassengerRocketModule>();
			if (component != null)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x06007102 RID: 28930 RVA: 0x002AFB3C File Offset: 0x002ADD3C
	private void RefreshModulePanel(PassengerRocketModule module)
	{
		HierarchyReferences component = base.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("icon").sprite = Def.GetUISprite(module.gameObject, "ui", false).first;
		KButton reference = component.GetReference<KButton>("button");
		reference.ClearOnClick();
		reference.onClick += delegate
		{
			AudioMixer.instance.Start(module.interiorReverbSnapshot);
			AudioMixer.instance.PauseSpaceVisibleSnapshot(true);
			ClusterManager.Instance.SetActiveWorld(module.GetComponent<ClustercraftExteriorDoor>().GetTargetWorld().id);
			ManagementMenu.Instance.CloseAll();
		};
		component.GetReference<LocText>("label").SetText(module.gameObject.GetProperName());
	}

	// Token: 0x04004DC3 RID: 19907
	private Clustercraft targetCraft;

	// Token: 0x04004DC4 RID: 19908
	public GameObject moduleContentContainer;

	// Token: 0x04004DC5 RID: 19909
	public GameObject modulePanelPrefab;
}
