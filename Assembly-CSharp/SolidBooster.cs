using System;
using UnityEngine;

// Token: 0x02000B69 RID: 2921
public class SolidBooster : RocketEngine
{
	// Token: 0x0600570B RID: 22283 RVA: 0x001F892B File Offset: 0x001F6B2B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SolidBooster>(-887025858, SolidBooster.OnRocketLandedDelegate);
	}

	// Token: 0x0600570C RID: 22284 RVA: 0x001F8944 File Offset: 0x001F6B44
	[ContextMenu("Fill Tank")]
	public void FillTank()
	{
		Element element = ElementLoader.GetElement(this.fuelTag);
		GameObject gameObject = element.substance.SpawnResource(base.gameObject.transform.GetPosition(), this.fuelStorage.capacityKg / 2f, element.defaultValues.temperature, byte.MaxValue, 0, false, false, false);
		this.fuelStorage.Store(gameObject, false, false, true, false);
		element = ElementLoader.GetElement(GameTags.OxyRock);
		gameObject = element.substance.SpawnResource(base.gameObject.transform.GetPosition(), this.fuelStorage.capacityKg / 2f, element.defaultValues.temperature, byte.MaxValue, 0, false, false, false);
		this.fuelStorage.Store(gameObject, false, false, true, false);
	}

	// Token: 0x0600570D RID: 22285 RVA: 0x001F8A0C File Offset: 0x001F6C0C
	private void OnRocketLanded(object data)
	{
		if (this.fuelStorage != null && this.fuelStorage.items != null)
		{
			for (int i = this.fuelStorage.items.Count - 1; i >= 0; i--)
			{
				Util.KDestroyGameObject(this.fuelStorage.items[i]);
			}
			this.fuelStorage.items.Clear();
		}
	}

	// Token: 0x04003A3D RID: 14909
	public Storage fuelStorage;

	// Token: 0x04003A3E RID: 14910
	private static readonly EventSystem.IntraObjectHandler<SolidBooster> OnRocketLandedDelegate = new EventSystem.IntraObjectHandler<SolidBooster>(delegate(SolidBooster component, object data)
	{
		component.OnRocketLanded(data);
	});
}
