using System;
using TUNING;
using UnityEngine;

// Token: 0x020007B8 RID: 1976
[AddComponentMenu("KMonoBehaviour/scripts/ResearchModule")]
public class ResearchModule : KMonoBehaviour
{
	// Token: 0x060034BB RID: 13499 RVA: 0x00127D60 File Offset: 0x00125F60
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KBatchedAnimController>().Play("grounded", KAnim.PlayMode.Loop, 1f, 0f);
		base.Subscribe<ResearchModule>(-1277991738, ResearchModule.OnLaunchDelegate);
		base.Subscribe<ResearchModule>(-887025858, ResearchModule.OnLandDelegate);
	}

	// Token: 0x060034BC RID: 13500 RVA: 0x00127DB5 File Offset: 0x00125FB5
	public void OnLaunch(object data)
	{
	}

	// Token: 0x060034BD RID: 13501 RVA: 0x00127DB8 File Offset: 0x00125FB8
	public void OnLand(object data)
	{
		if (!DlcManager.FeatureClusterSpaceEnabled())
		{
			SpaceDestination.ResearchOpportunity researchOpportunity = SpacecraftManager.instance.GetSpacecraftDestination(SpacecraftManager.instance.GetSpacecraftID(base.GetComponent<RocketModule>().conditionManager.GetComponent<ILaunchableRocket>())).TryCompleteResearchOpportunity();
			if (researchOpportunity != null)
			{
				GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("ResearchDatabank"), base.gameObject.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0);
				gameObject.SetActive(true);
				gameObject.GetComponent<PrimaryElement>().Mass = (float)researchOpportunity.dataValue;
				if (!string.IsNullOrEmpty(researchOpportunity.discoveredRareItem))
				{
					GameObject prefab = Assets.GetPrefab(researchOpportunity.discoveredRareItem);
					if (prefab == null)
					{
						KCrashReporter.Assert(false, "Missing prefab: " + researchOpportunity.discoveredRareItem, null);
					}
					else
					{
						GameUtil.KInstantiate(prefab, base.gameObject.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0).SetActive(true);
					}
				}
			}
		}
		GameObject gameObject2 = GameUtil.KInstantiate(Assets.GetPrefab("ResearchDatabank"), base.gameObject.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0);
		gameObject2.SetActive(true);
		gameObject2.GetComponent<PrimaryElement>().Mass = (float)ROCKETRY.DESTINATION_RESEARCH.EVERGREEN;
	}

	// Token: 0x04001FD5 RID: 8149
	private static readonly EventSystem.IntraObjectHandler<ResearchModule> OnLaunchDelegate = new EventSystem.IntraObjectHandler<ResearchModule>(delegate(ResearchModule component, object data)
	{
		component.OnLaunch(data);
	});

	// Token: 0x04001FD6 RID: 8150
	private static readonly EventSystem.IntraObjectHandler<ResearchModule> OnLandDelegate = new EventSystem.IntraObjectHandler<ResearchModule>(delegate(ResearchModule component, object data)
	{
		component.OnLand(data);
	});
}
