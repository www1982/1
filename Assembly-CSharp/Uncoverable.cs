using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000649 RID: 1609
[AddComponentMenu("KMonoBehaviour/scripts/Uncoverable")]
public class Uncoverable : KMonoBehaviour
{
	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000DE7D8 File Offset: 0x000DC9D8
	public bool IsUncovered
	{
		get
		{
			return this.hasBeenUncovered;
		}
	}

	// Token: 0x060026E1 RID: 9953 RVA: 0x000DE7E0 File Offset: 0x000DC9E0
	private bool IsAnyCellShowing()
	{
		int num = Grid.PosToCell(this);
		return !this.occupyArea.TestArea(num, null, Uncoverable.IsCellBlockedDelegate);
	}

	// Token: 0x060026E2 RID: 9954 RVA: 0x000DE809 File Offset: 0x000DCA09
	private static bool IsCellBlocked(int cell, object data)
	{
		return Grid.Element[cell].IsSolid && !Grid.Foundation[cell];
	}

	// Token: 0x060026E3 RID: 9955 RVA: 0x000DE829 File Offset: 0x000DCA29
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060026E4 RID: 9956 RVA: 0x000DE834 File Offset: 0x000DCA34
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.IsAnyCellShowing())
		{
			this.hasBeenUncovered = true;
		}
		if (!this.hasBeenUncovered)
		{
			base.GetComponent<KSelectable>().IsSelectable = false;
			Extents extents = this.occupyArea.GetExtents();
			this.partitionerEntry = GameScenePartitioner.Instance.Add("Uncoverable.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		}
	}

	// Token: 0x060026E5 RID: 9957 RVA: 0x000DE8A8 File Offset: 0x000DCAA8
	private void OnSolidChanged(object data)
	{
		if (this.IsAnyCellShowing() && !this.hasBeenUncovered && this.partitionerEntry.IsValid())
		{
			GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
			this.hasBeenUncovered = true;
			base.GetComponent<KSelectable>().IsSelectable = true;
			Notification notification = new Notification(MISC.STATUSITEMS.BURIEDITEM.NOTIFICATION, NotificationType.Good, new Func<List<Notification>, object, string>(Uncoverable.OnNotificationToolTip), this, true, 0f, null, null, null, true, false, false);
			base.gameObject.AddOrGet<Notifier>().Add(notification, "");
		}
	}

	// Token: 0x060026E6 RID: 9958 RVA: 0x000DE938 File Offset: 0x000DCB38
	private static string OnNotificationToolTip(List<Notification> notifications, object data)
	{
		Uncoverable uncoverable = (Uncoverable)data;
		return MISC.STATUSITEMS.BURIEDITEM.NOTIFICATION_TOOLTIP.Replace("{Uncoverable}", uncoverable.GetProperName());
	}

	// Token: 0x060026E7 RID: 9959 RVA: 0x000DE961 File Offset: 0x000DCB61
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x040016B3 RID: 5811
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x040016B4 RID: 5812
	[Serialize]
	private bool hasBeenUncovered;

	// Token: 0x040016B5 RID: 5813
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040016B6 RID: 5814
	private static readonly Func<int, object, bool> IsCellBlockedDelegate = (int cell, object data) => Uncoverable.IsCellBlocked(cell, data);
}
