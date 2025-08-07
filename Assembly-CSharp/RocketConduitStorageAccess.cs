using System;
using UnityEngine;

// Token: 0x02000ADB RID: 2779
public class RocketConduitStorageAccess : KMonoBehaviour, ISim200ms
{
	// Token: 0x060050C8 RID: 20680 RVA: 0x001D50EC File Offset: 0x001D32EC
	protected override void OnSpawn()
	{
		WorldContainer myWorld = this.GetMyWorld();
		this.craftModuleInterface = myWorld.GetComponent<CraftModuleInterface>();
	}

	// Token: 0x060050C9 RID: 20681 RVA: 0x001D510C File Offset: 0x001D330C
	public void Sim200ms(float dt)
	{
		if (this.operational != null && !this.operational.IsOperational)
		{
			return;
		}
		float num = this.storage.MassStored();
		if (num < this.targetLevel - 0.01f || num > this.targetLevel + 0.01f)
		{
			if (this.operational != null)
			{
				this.operational.SetActive(true, false);
			}
			float num2 = this.targetLevel - num;
			foreach (Ref<RocketModuleCluster> @ref in this.craftModuleInterface.ClusterModules)
			{
				CargoBayCluster component = @ref.Get().GetComponent<CargoBayCluster>();
				if (component != null && component.storageType == this.cargoType)
				{
					if (num2 > 0f && component.storage.MassStored() > 0f)
					{
						for (int i = component.storage.items.Count - 1; i >= 0; i--)
						{
							GameObject gameObject = component.storage.items[i];
							if (!(this.filterable != null) || !(this.filterable.SelectedTag != GameTags.Void) || !(gameObject.PrefabID() != this.filterable.SelectedTag))
							{
								Pickupable pickupable = gameObject.GetComponent<Pickupable>().Take(num2);
								if (pickupable != null)
								{
									num2 -= pickupable.PrimaryElement.Mass;
									this.storage.Store(pickupable.gameObject, true, false, true, false);
								}
								if (num2 <= 0f)
								{
									break;
								}
							}
						}
						if (num2 <= 0f)
						{
							break;
						}
					}
					if (num2 < 0f && component.storage.RemainingCapacity() > 0f)
					{
						Mathf.Min(-num2, component.storage.RemainingCapacity());
						for (int j = this.storage.items.Count - 1; j >= 0; j--)
						{
							Pickupable pickupable2 = this.storage.items[j].GetComponent<Pickupable>().Take(-num2);
							if (pickupable2 != null)
							{
								num2 += pickupable2.PrimaryElement.Mass;
								component.storage.Store(pickupable2.gameObject, true, false, true, false);
							}
							if (num2 >= 0f)
							{
								break;
							}
						}
						if (num2 >= 0f)
						{
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x04003655 RID: 13909
	[SerializeField]
	public Storage storage;

	// Token: 0x04003656 RID: 13910
	[SerializeField]
	public float targetLevel;

	// Token: 0x04003657 RID: 13911
	[SerializeField]
	public CargoBay.CargoType cargoType;

	// Token: 0x04003658 RID: 13912
	[MyCmpGet]
	private Filterable filterable;

	// Token: 0x04003659 RID: 13913
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400365A RID: 13914
	private const float TOLERANCE = 0.01f;

	// Token: 0x0400365B RID: 13915
	private CraftModuleInterface craftModuleInterface;
}
