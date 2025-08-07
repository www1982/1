using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x0200064F RID: 1615
internal class UpdatePercentCompleteParameter : LoopingSoundParameterUpdater
{
	// Token: 0x0600275D RID: 10077 RVA: 0x000E104E File Offset: 0x000DF24E
	public UpdatePercentCompleteParameter()
		: base("percentComplete")
	{
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x000E106C File Offset: 0x000DF26C
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdatePercentCompleteParameter.Entry entry = new UpdatePercentCompleteParameter.Entry
		{
			worker = sound.transform.GetComponent<WorkerBase>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x000E10C8 File Offset: 0x000DF2C8
	public override void Update(float dt)
	{
		foreach (UpdatePercentCompleteParameter.Entry entry in this.entries)
		{
			if (!(entry.worker == null))
			{
				Workable workable = entry.worker.GetWorkable();
				if (!(workable == null))
				{
					float percentComplete = workable.GetPercentComplete();
					EventInstance ev = entry.ev;
					ev.setParameterByID(entry.parameterId, percentComplete, false);
				}
			}
		}
	}

	// Token: 0x06002760 RID: 10080 RVA: 0x000E1158 File Offset: 0x000DF358
	public override void Remove(LoopingSoundParameterUpdater.Sound sound)
	{
		for (int i = 0; i < this.entries.Count; i++)
		{
			if (this.entries[i].ev.handle == sound.ev.handle)
			{
				this.entries.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x04001702 RID: 5890
	private List<UpdatePercentCompleteParameter.Entry> entries = new List<UpdatePercentCompleteParameter.Entry>();

	// Token: 0x020014E0 RID: 5344
	private struct Entry
	{
		// Token: 0x04006E30 RID: 28208
		public WorkerBase worker;

		// Token: 0x04006E31 RID: 28209
		public EventInstance ev;

		// Token: 0x04006E32 RID: 28210
		public PARAMETER_ID parameterId;
	}
}
