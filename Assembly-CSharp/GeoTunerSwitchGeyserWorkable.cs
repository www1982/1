using System;

// Token: 0x02000739 RID: 1849
public class GeoTunerSwitchGeyserWorkable : Workable
{
	// Token: 0x06002EBE RID: 11966 RVA: 0x0010C154 File Offset: 0x0010A354
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_remote_kanim") };
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
	}

	// Token: 0x06002EBF RID: 11967 RVA: 0x0010C188 File Offset: 0x0010A388
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(3f);
	}

	// Token: 0x04001BA7 RID: 7079
	private const string animName = "anim_use_remote_kanim";
}
