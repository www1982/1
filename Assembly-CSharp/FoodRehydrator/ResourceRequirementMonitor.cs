using System;

namespace FoodRehydrator
{
	// Token: 0x02000EB9 RID: 3769
	public class ResourceRequirementMonitor : KMonoBehaviour
	{
		// Token: 0x060078BF RID: 30911 RVA: 0x002EB520 File Offset: 0x002E9720
		protected override void OnSpawn()
		{
			base.OnSpawn();
			Storage[] components = base.GetComponents<Storage>();
			DebugUtil.DevAssert(components.Length == 2, "Incorrect number of storages on foodrehydrator", null);
			this.packages = components[0];
			this.water = components[1];
			base.Subscribe<ResourceRequirementMonitor>(-1697596308, ResourceRequirementMonitor.OnStorageChangedDelegate);
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x002EB56E File Offset: 0x002E976E
		protected float GetAvailableWater()
		{
			return this.water.GetMassAvailable(GameTags.Water);
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x002EB580 File Offset: 0x002E9780
		protected bool HasSufficientResources()
		{
			return this.packages.items.Count > 0 && this.GetAvailableWater() > 1f;
		}

		// Token: 0x060078C2 RID: 30914 RVA: 0x002EB5A4 File Offset: 0x002E97A4
		protected void OnStorageChanged(object _)
		{
			this.operational.SetFlag(ResourceRequirementMonitor.flag, this.HasSufficientResources());
		}

		// Token: 0x040053D4 RID: 21460
		[MyCmpReq]
		private Operational operational;

		// Token: 0x040053D5 RID: 21461
		private Storage packages;

		// Token: 0x040053D6 RID: 21462
		private Storage water;

		// Token: 0x040053D7 RID: 21463
		private static readonly Operational.Flag flag = new Operational.Flag("HasSufficientResources", Operational.Flag.Type.Requirement);

		// Token: 0x040053D8 RID: 21464
		private static readonly EventSystem.IntraObjectHandler<ResourceRequirementMonitor> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<ResourceRequirementMonitor>(delegate(ResourceRequirementMonitor component, object data)
		{
			component.OnStorageChanged(data);
		});
	}
}
