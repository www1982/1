using System;
using UnityEngine;

// Token: 0x02000740 RID: 1856
public class GlassForge : ComplexFabricator
{
	// Token: 0x06002F0B RID: 12043 RVA: 0x0010DC74 File Offset: 0x0010BE74
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<GlassForge>(-2094018600, GlassForge.CheckPipesDelegate);
	}

	// Token: 0x06002F0C RID: 12044 RVA: 0x0010DC90 File Offset: 0x0010BE90
	private void CheckPipes(object data)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		int num = Grid.OffsetCell(Grid.PosToCell(this), GlassForgeConfig.outPipeOffset);
		GameObject gameObject = Grid.Objects[num, 16];
		if (!(gameObject != null))
		{
			component.RemoveStatusItem(this.statusHandle, false);
			return;
		}
		if (gameObject.GetComponent<PrimaryElement>().Element.highTemp > ElementLoader.FindElementByHash(SimHashes.MoltenGlass).lowTemp)
		{
			component.RemoveStatusItem(this.statusHandle, false);
			return;
		}
		this.statusHandle = component.AddStatusItem(Db.Get().BuildingStatusItems.PipeMayMelt, null);
	}

	// Token: 0x04001BCE RID: 7118
	private Guid statusHandle;

	// Token: 0x04001BCF RID: 7119
	private static readonly EventSystem.IntraObjectHandler<GlassForge> CheckPipesDelegate = new EventSystem.IntraObjectHandler<GlassForge>(delegate(GlassForge component, object data)
	{
		component.CheckPipes(data);
	});
}
