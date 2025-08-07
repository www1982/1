using System;

// Token: 0x02000476 RID: 1142
public interface IWorkerPrioritizable
{
	// Token: 0x06001811 RID: 6161
	bool GetWorkerPriority(WorkerBase worker, out int priority);
}
