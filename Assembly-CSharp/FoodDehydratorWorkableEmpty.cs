using System;

// Token: 0x0200072E RID: 1838
public class FoodDehydratorWorkableEmpty : Workable
{
	// Token: 0x06002E60 RID: 11872 RVA: 0x00109F69 File Offset: 0x00108169
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Emptying;
		this.workAnims = FoodDehydratorWorkableEmpty.WORK_ANIMS;
		this.workingPstComplete = FoodDehydratorWorkableEmpty.WORK_ANIMS_PST;
		this.workingPstFailed = FoodDehydratorWorkableEmpty.WORK_ANIMS_FAIL_PST;
	}

	// Token: 0x04001B70 RID: 7024
	private static readonly HashedString[] WORK_ANIMS = new HashedString[] { "empty_pre", "empty_loop" };

	// Token: 0x04001B71 RID: 7025
	private static readonly HashedString[] WORK_ANIMS_PST = new HashedString[] { "empty_pst" };

	// Token: 0x04001B72 RID: 7026
	private static readonly HashedString[] WORK_ANIMS_FAIL_PST = new HashedString[] { "" };
}
