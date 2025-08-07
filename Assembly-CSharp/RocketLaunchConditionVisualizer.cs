using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000603 RID: 1539
public class RocketLaunchConditionVisualizer : KMonoBehaviour
{
	// Token: 0x06002488 RID: 9352 RVA: 0x000D0508 File Offset: 0x000CE708
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			this.clusterModule = base.GetComponent<RocketModuleCluster>();
		}
		else
		{
			this.launchConditionManager = base.GetComponent<LaunchConditionManager>();
		}
		this.UpdateAllModuleData();
		base.Subscribe(1512695988, new Action<object>(this.OnAnyRocketModuleChanged));
	}

	// Token: 0x06002489 RID: 9353 RVA: 0x000D055A File Offset: 0x000CE75A
	protected override void OnCleanUp()
	{
		base.Unsubscribe(1512695988, new Action<object>(this.OnAnyRocketModuleChanged));
	}

	// Token: 0x0600248A RID: 9354 RVA: 0x000D0573 File Offset: 0x000CE773
	private void OnAnyRocketModuleChanged(object obj)
	{
		this.UpdateAllModuleData();
	}

	// Token: 0x0600248B RID: 9355 RVA: 0x000D057C File Offset: 0x000CE77C
	private void UpdateAllModuleData()
	{
		if (this.moduleVisualizeData != null)
		{
			this.moduleVisualizeData = null;
		}
		bool flag = this.clusterModule != null;
		List<Ref<RocketModuleCluster>> list = null;
		List<RocketModule> list2 = null;
		if (flag)
		{
			list = new List<Ref<RocketModuleCluster>>(this.clusterModule.CraftInterface.ClusterModules);
			this.moduleVisualizeData = new RocketLaunchConditionVisualizer.RocketModuleVisualizeData[list.Count];
			list.Sort(delegate(Ref<RocketModuleCluster> a, Ref<RocketModuleCluster> b)
			{
				int y = Grid.PosToXY(a.Get().transform.GetPosition()).y;
				int y2 = Grid.PosToXY(b.Get().transform.GetPosition()).y;
				return y.CompareTo(y2);
			});
		}
		else
		{
			list2 = new List<RocketModule>(this.launchConditionManager.rocketModules);
			list2.Sort(delegate(RocketModule a, RocketModule b)
			{
				int y3 = Grid.PosToXY(a.transform.GetPosition()).y;
				int y4 = Grid.PosToXY(b.transform.GetPosition()).y;
				return y3.CompareTo(y4);
			});
			this.moduleVisualizeData = new RocketLaunchConditionVisualizer.RocketModuleVisualizeData[list2.Count];
		}
		for (int i = 0; i < this.moduleVisualizeData.Length; i++)
		{
			RocketModule rocketModule = (flag ? list[i].Get() : list2[i]);
			Building component = rocketModule.GetComponent<Building>();
			this.moduleVisualizeData[i] = new RocketLaunchConditionVisualizer.RocketModuleVisualizeData
			{
				Module = rocketModule,
				RangeMax = Mathf.FloorToInt((float)component.Def.WidthInCells / 2f),
				RangeMin = -Mathf.FloorToInt((float)(component.Def.WidthInCells - 1) / 2f)
			};
		}
	}

	// Token: 0x0400154A RID: 5450
	public RocketLaunchConditionVisualizer.RocketModuleVisualizeData[] moduleVisualizeData;

	// Token: 0x0400154B RID: 5451
	private LaunchConditionManager launchConditionManager;

	// Token: 0x0400154C RID: 5452
	private RocketModuleCluster clusterModule;

	// Token: 0x0200149B RID: 5275
	public struct RocketModuleVisualizeData
	{
		// Token: 0x04006D35 RID: 27957
		public RocketModule Module;

		// Token: 0x04006D36 RID: 27958
		public Vector2I OriginOffset;

		// Token: 0x04006D37 RID: 27959
		public int RangeMin;

		// Token: 0x04006D38 RID: 27960
		public int RangeMax;
	}
}
