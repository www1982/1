using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000578 RID: 1400
[AddComponentMenu("KMonoBehaviour/Workable/Breakable")]
public class Breakable : Workable
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x06001F5A RID: 8026 RVA: 0x000B3489 File Offset: 0x000B1689
	public bool IsInvincible
	{
		get
		{
			return this.hp == null || this.hp.invincible;
		}
	}

	// Token: 0x06001F5B RID: 8027 RVA: 0x000B34A6 File Offset: 0x000B16A6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = false;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_break_kanim") };
		base.SetWorkTime(float.PositiveInfinity);
	}

	// Token: 0x06001F5C RID: 8028 RVA: 0x000B34DE File Offset: 0x000B16DE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Breakables.Add(this);
	}

	// Token: 0x06001F5D RID: 8029 RVA: 0x000B34F1 File Offset: 0x000B16F1
	public bool isBroken()
	{
		return this.hp == null || this.hp.HitPoints <= 0;
	}

	// Token: 0x06001F5E RID: 8030 RVA: 0x000B3514 File Offset: 0x000B1714
	public Notification CreateDamageNotification()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		return new Notification(BUILDING.STATUSITEMS.ANGERDAMAGE.NOTIFICATION, NotificationType.BadMinor, (List<Notification> notificationList, object data) => string.Format(BUILDING.STATUSITEMS.ANGERDAMAGE.NOTIFICATION_TOOLTIP, notificationList.ReduceMessages(false)), component.GetProperName(), false, 0f, null, null, null, true, false, false);
	}

	// Token: 0x06001F5F RID: 8031 RVA: 0x000B356C File Offset: 0x000B176C
	private static string ToolTipResolver(List<Notification> notificationList, object data)
	{
		string text = "";
		for (int i = 0; i < notificationList.Count; i++)
		{
			Notification notification = notificationList[i];
			text += (string)notification.tooltipData;
			if (i < notificationList.Count - 1)
			{
				text += "\n";
			}
		}
		return string.Format(BUILDING.STATUSITEMS.ANGERDAMAGE.NOTIFICATION_TOOLTIP, text);
	}

	// Token: 0x06001F60 RID: 8032 RVA: 0x000B35D4 File Offset: 0x000B17D4
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		this.secondsPerTenPercentDamage = 2f;
		this.tenPercentDamage = Mathf.CeilToInt((float)this.hp.MaxHitPoints * 0.1f);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.AngerDamage, this);
		this.notification = this.CreateDamageNotification();
		base.gameObject.AddOrGet<Notifier>().Add(this.notification, "");
		this.elapsedDamageTime = 0f;
	}

	// Token: 0x06001F61 RID: 8033 RVA: 0x000B3660 File Offset: 0x000B1860
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (this.elapsedDamageTime >= this.secondsPerTenPercentDamage)
		{
			this.elapsedDamageTime -= this.elapsedDamageTime;
			base.Trigger(-794517298, new BuildingHP.DamageSourceInfo
			{
				damage = this.tenPercentDamage,
				source = BUILDINGS.DAMAGESOURCES.MINION_DESTRUCTION,
				popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.MINION_DESTRUCTION
			});
		}
		this.elapsedDamageTime += dt;
		return this.hp.HitPoints <= 0;
	}

	// Token: 0x06001F62 RID: 8034 RVA: 0x000B36F8 File Offset: 0x000B18F8
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.AngerDamage, false);
		base.gameObject.AddOrGet<Notifier>().Remove(this.notification);
		if (worker != null)
		{
			worker.Trigger(-1734580852, null);
		}
	}

	// Token: 0x06001F63 RID: 8035 RVA: 0x000B3753 File Offset: 0x000B1953
	public override bool InstantlyFinish(WorkerBase worker)
	{
		return false;
	}

	// Token: 0x06001F64 RID: 8036 RVA: 0x000B3756 File Offset: 0x000B1956
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Breakables.Remove(this);
	}

	// Token: 0x0400123B RID: 4667
	private const float TIME_TO_BREAK_AT_FULL_HEALTH = 20f;

	// Token: 0x0400123C RID: 4668
	private Notification notification;

	// Token: 0x0400123D RID: 4669
	private float secondsPerTenPercentDamage = float.PositiveInfinity;

	// Token: 0x0400123E RID: 4670
	private float elapsedDamageTime;

	// Token: 0x0400123F RID: 4671
	private int tenPercentDamage = int.MaxValue;

	// Token: 0x04001240 RID: 4672
	[MyCmpGet]
	private BuildingHP hp;
}
