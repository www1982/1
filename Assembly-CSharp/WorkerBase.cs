using System;
using Klei.AI;

// Token: 0x02000650 RID: 1616
public abstract class WorkerBase : KMonoBehaviour
{
	// Token: 0x06002761 RID: 10081
	public abstract bool UsesMultiTool();

	// Token: 0x06002762 RID: 10082
	public abstract bool IsFetchDrone();

	// Token: 0x06002763 RID: 10083
	public abstract KBatchedAnimController GetAnimController();

	// Token: 0x06002764 RID: 10084
	public abstract WorkerBase.State GetState();

	// Token: 0x06002765 RID: 10085
	public abstract WorkerBase.StartWorkInfo GetStartWorkInfo();

	// Token: 0x06002766 RID: 10086
	public abstract Workable GetWorkable();

	// Token: 0x06002767 RID: 10087
	public abstract Attributes GetAttributes();

	// Token: 0x06002768 RID: 10088
	public abstract AttributeConverterInstance GetAttributeConverter(string id);

	// Token: 0x06002769 RID: 10089
	public abstract Guid OfferStatusItem(StatusItem item, object data = null);

	// Token: 0x0600276A RID: 10090
	public abstract void RevokeStatusItem(Guid id);

	// Token: 0x0600276B RID: 10091
	public abstract void StartWork(WorkerBase.StartWorkInfo start_work_info);

	// Token: 0x0600276C RID: 10092
	public abstract void StopWork();

	// Token: 0x0600276D RID: 10093
	public abstract bool InstantlyFinish();

	// Token: 0x0600276E RID: 10094
	public abstract WorkerBase.WorkResult Work(float dt);

	// Token: 0x0600276F RID: 10095
	public abstract void SetWorkCompleteData(object data);

	// Token: 0x020014E1 RID: 5345
	public class StartWorkInfo
	{
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06008F3F RID: 36671 RVA: 0x0035D465 File Offset: 0x0035B665
		// (set) Token: 0x06008F40 RID: 36672 RVA: 0x0035D46D File Offset: 0x0035B66D
		public Workable workable { get; set; }

		// Token: 0x06008F41 RID: 36673 RVA: 0x0035D476 File Offset: 0x0035B676
		public StartWorkInfo(Workable workable)
		{
			this.workable = workable;
		}
	}

	// Token: 0x020014E2 RID: 5346
	public enum State
	{
		// Token: 0x04006E35 RID: 28213
		Idle,
		// Token: 0x04006E36 RID: 28214
		Working,
		// Token: 0x04006E37 RID: 28215
		PendingCompletion,
		// Token: 0x04006E38 RID: 28216
		Completing
	}

	// Token: 0x020014E3 RID: 5347
	public enum WorkResult
	{
		// Token: 0x04006E3A RID: 28218
		Success,
		// Token: 0x04006E3B RID: 28219
		InProgress,
		// Token: 0x04006E3C RID: 28220
		Failed
	}
}
