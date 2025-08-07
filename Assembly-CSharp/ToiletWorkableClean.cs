using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x020007E4 RID: 2020
[AddComponentMenu("KMonoBehaviour/Workable/ToiletWorkableClean")]
public class ToiletWorkableClean : Workable
{
	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x060036B6 RID: 14006 RVA: 0x001303B7 File Offset: 0x0012E5B7
	// (set) Token: 0x060036B5 RID: 14005 RVA: 0x001303AE File Offset: 0x0012E5AE
	public bool IsCloggedByGunk { get; private set; }

	// Token: 0x060036B7 RID: 14007 RVA: 0x001303C0 File Offset: 0x0012E5C0
	public void SetIsCloggedByGunk(bool isIt)
	{
		this.IsCloggedByGunk = isIt;
		this.workAnims = (this.IsCloggedByGunk ? ToiletWorkableClean.CLEAN_GUNK_ANIMS : ToiletWorkableClean.CLEAN_ANIMS);
		this.workingPstComplete = (this.IsCloggedByGunk ? ToiletWorkableClean.PST_GUNK_ANIM : ToiletWorkableClean.PST_ANIM);
		this.workingPstFailed = (this.IsCloggedByGunk ? ToiletWorkableClean.PST_GUNK_ANIM : ToiletWorkableClean.PST_ANIM);
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x00130424 File Offset: 0x0012E624
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Cleaning;
		this.workingStatusItem = Db.Get().MiscStatusItems.Cleaning;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x001304A8 File Offset: 0x0012E6A8
	protected override void OnCompleteWork(WorkerBase worker)
	{
		ToiletWorkableUse component = base.gameObject.GetComponent<ToiletWorkableUse>();
		if (component != null && this.IsCloggedByGunk && base.gameObject.GetComponent<FlushToilet>() == null)
		{
			LiquidSourceManager.Instance.CreateChunk(SimHashes.LiquidGunk, component.lastAmountOfWasteMassRemovedFromDupe, DUPLICANTSTATS.STANDARD.Temperature.Internal.IDEAL, byte.MaxValue, 0, Grid.CellToPos(Grid.PosToCell(base.gameObject), CellAlignment.Top, Grid.SceneLayer.Ore));
		}
		this.timesCleaned++;
		base.OnCompleteWork(worker);
	}

	// Token: 0x04002106 RID: 8454
	[Serialize]
	public int timesCleaned;

	// Token: 0x04002107 RID: 8455
	private static readonly HashedString[] CLEAN_GUNK_ANIMS = new HashedString[] { "degunk_pre", "degunk_loop" };

	// Token: 0x04002108 RID: 8456
	private static readonly HashedString[] CLEAN_ANIMS = new HashedString[] { "unclog_pre", "unclog_loop" };

	// Token: 0x04002109 RID: 8457
	private static readonly HashedString[] PST_ANIM = new HashedString[]
	{
		new HashedString("unclog_pst")
	};

	// Token: 0x0400210A RID: 8458
	private static readonly HashedString[] PST_GUNK_ANIM = new HashedString[]
	{
		new HashedString("degunk_pst")
	};
}
