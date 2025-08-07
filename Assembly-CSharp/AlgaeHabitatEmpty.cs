using System;
using TUNING;
using UnityEngine;

// Token: 0x020006D7 RID: 1751
[AddComponentMenu("KMonoBehaviour/Workable/AlgaeHabitatEmpty")]
public class AlgaeHabitatEmpty : Workable
{
	// Token: 0x06002B30 RID: 11056 RVA: 0x000F9A0C File Offset: 0x000F7C0C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cleaning;
		this.workingStatusItem = Db.Get().MiscStatusItems.Cleaning;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.workAnims = AlgaeHabitatEmpty.CLEAN_ANIMS;
		this.workingPstComplete = new HashedString[] { AlgaeHabitatEmpty.PST_ANIM };
		this.workingPstFailed = new HashedString[] { AlgaeHabitatEmpty.PST_ANIM };
		this.synchronizeAnims = false;
	}

	// Token: 0x04001986 RID: 6534
	private static readonly HashedString[] CLEAN_ANIMS = new HashedString[] { "sponge_pre", "sponge_loop" };

	// Token: 0x04001987 RID: 6535
	private static readonly HashedString PST_ANIM = new HashedString("sponge_pst");
}
