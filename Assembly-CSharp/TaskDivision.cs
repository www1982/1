using System;

// Token: 0x0200083E RID: 2110
internal class TaskDivision<Task, SharedData> where Task : DivisibleTask<SharedData>, new()
{
	// Token: 0x060039D3 RID: 14803 RVA: 0x0014112C File Offset: 0x0013F32C
	public TaskDivision(int taskCount)
	{
		this.tasks = new Task[taskCount];
		for (int num = 0; num != this.tasks.Length; num++)
		{
			this.tasks[num] = new Task();
		}
	}

	// Token: 0x060039D4 RID: 14804 RVA: 0x0014116F File Offset: 0x0013F36F
	public TaskDivision()
		: this(CPUBudget.coreCount)
	{
	}

	// Token: 0x060039D5 RID: 14805 RVA: 0x0014117C File Offset: 0x0013F37C
	public void Initialize(int count)
	{
		int num = count / this.tasks.Length;
		for (int num2 = 0; num2 != this.tasks.Length; num2++)
		{
			this.tasks[num2].start = num2 * num;
			this.tasks[num2].end = this.tasks[num2].start + num;
		}
		DebugUtil.Assert(this.tasks[this.tasks.Length - 1].end + count % this.tasks.Length == count);
		this.tasks[this.tasks.Length - 1].end = count;
	}

	// Token: 0x060039D6 RID: 14806 RVA: 0x00141240 File Offset: 0x0013F440
	public void Run(SharedData sharedData, int threadIndex)
	{
		Task[] array = this.tasks;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Run(sharedData, threadIndex);
		}
	}

	// Token: 0x04002392 RID: 9106
	public Task[] tasks;
}
