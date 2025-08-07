using System;
using UnityEngine;

namespace FoodRehydrator
{
	// Token: 0x02000EB8 RID: 3768
	public class AccessabilityManager : KMonoBehaviour
	{
		// Token: 0x060078B6 RID: 30902 RVA: 0x002EB3E2 File Offset: 0x002E95E2
		protected override void OnSpawn()
		{
			base.OnSpawn();
			Components.FoodRehydrators.Add(base.gameObject);
			base.Subscribe(824508782, new Action<object>(this.ActiveChangedHandler));
		}

		// Token: 0x060078B7 RID: 30903 RVA: 0x002EB412 File Offset: 0x002E9612
		protected override void OnCleanUp()
		{
			Components.FoodRehydrators.Remove(base.gameObject);
			base.OnCleanUp();
		}

		// Token: 0x060078B8 RID: 30904 RVA: 0x002EB42A File Offset: 0x002E962A
		public void Reserve(GameObject reserver)
		{
			this.reserver = reserver;
			global::Debug.Assert(reserver != null && reserver.GetComponent<MinionResume>() != null);
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x002EB450 File Offset: 0x002E9650
		public void Unreserve()
		{
			this.activeWorkable = null;
			this.reserver = null;
		}

		// Token: 0x060078BA RID: 30906 RVA: 0x002EB460 File Offset: 0x002E9660
		public void SetActiveWorkable(Workable work)
		{
			DebugUtil.DevAssert(this.activeWorkable == null || work == null, "FoodRehydrator::AccessabilityManager activating a second workable", null);
			this.activeWorkable = work;
			this.operational.SetActive(this.activeWorkable != null, false);
		}

		// Token: 0x060078BB RID: 30907 RVA: 0x002EB4AF File Offset: 0x002E96AF
		public bool CanAccess(GameObject worker)
		{
			return this.operational.IsOperational && (this.reserver == null || this.reserver == worker);
		}

		// Token: 0x060078BC RID: 30908 RVA: 0x002EB4DC File Offset: 0x002E96DC
		protected void ActiveChangedHandler(object obj)
		{
			if (!this.operational.IsActive)
			{
				this.CancelActiveWorkable();
			}
		}

		// Token: 0x060078BD RID: 30909 RVA: 0x002EB4F1 File Offset: 0x002E96F1
		public void CancelActiveWorkable()
		{
			if (this.activeWorkable != null)
			{
				this.activeWorkable.StopWork(this.activeWorkable.worker, true);
			}
		}

		// Token: 0x040053D1 RID: 21457
		[MyCmpReq]
		private Operational operational;

		// Token: 0x040053D2 RID: 21458
		private GameObject reserver;

		// Token: 0x040053D3 RID: 21459
		private Workable activeWorkable;
	}
}
