using System;

// Token: 0x02000343 RID: 835
public class MorbRoverMakerRevealWorkable : Workable
{
	// Token: 0x06001142 RID: 4418 RVA: 0x00065024 File Offset: 0x00063224
	protected override void OnPrefabInit()
	{
		this.workAnims = new HashedString[] { "reveal_working_pre", "reveal_working_loop" };
		this.workingPstComplete = new HashedString[] { "reveal_working_pst" };
		this.workingPstFailed = new HashedString[] { "reveal_working_pst" };
		base.OnPrefabInit();
		this.workingStatusItem = Db.Get().BuildingStatusItems.MorbRoverMakerBuildingRevealed;
		base.SetWorkerStatusItem(Db.Get().DuplicantStatusItems.MorbRoverMakerWorkingOnRevealing);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_gravitas_morb_tank_kanim") };
		this.lightEfficiencyBonus = true;
		this.synchronizeAnims = true;
		base.SetWorkTime(15f);
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x00065100 File Offset: 0x00063300
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
	}

	// Token: 0x04000AE6 RID: 2790
	public const string WORKABLE_PRE_ANIM_NAME = "reveal_working_pre";

	// Token: 0x04000AE7 RID: 2791
	public const string WORKABLE_LOOP_ANIM_NAME = "reveal_working_loop";

	// Token: 0x04000AE8 RID: 2792
	public const string WORKABLE_PST_ANIM_NAME = "reveal_working_pst";
}
