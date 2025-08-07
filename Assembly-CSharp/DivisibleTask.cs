using System;

// Token: 0x0200083D RID: 2109
internal abstract class DivisibleTask<SharedData> : IWorkItem<SharedData>
{
	// Token: 0x060039D0 RID: 14800 RVA: 0x00141113 File Offset: 0x0013F313
	public void Run(SharedData sharedData, int threadIndex)
	{
		this.RunDivision(sharedData);
	}

	// Token: 0x060039D1 RID: 14801 RVA: 0x0014111C File Offset: 0x0013F31C
	protected DivisibleTask(string name)
	{
		this.name = name;
	}

	// Token: 0x060039D2 RID: 14802
	protected abstract void RunDivision(SharedData sharedData);

	// Token: 0x0400238F RID: 9103
	public string name;

	// Token: 0x04002390 RID: 9104
	public int start;

	// Token: 0x04002391 RID: 9105
	public int end;
}
