using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000591 RID: 1425
internal class UpdateConsumedMassParameter : LoopingSoundParameterUpdater
{
	// Token: 0x06002095 RID: 8341 RVA: 0x000BC2EB File Offset: 0x000BA4EB
	public UpdateConsumedMassParameter()
		: base("consumedMass")
	{
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x000BC308 File Offset: 0x000BA508
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateConsumedMassParameter.Entry entry = new UpdateConsumedMassParameter.Entry
		{
			creatureCalorieMonitor = sound.transform.GetSMI<CreatureCalorieMonitor.Instance>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x000BC364 File Offset: 0x000BA564
	public override void Update(float dt)
	{
		foreach (UpdateConsumedMassParameter.Entry entry in this.entries)
		{
			if (!entry.creatureCalorieMonitor.IsNullOrStopped())
			{
				float fullness = entry.creatureCalorieMonitor.stomach.GetFullness();
				EventInstance ev = entry.ev;
				ev.setParameterByID(entry.parameterId, fullness, false);
			}
		}
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x000BC3E8 File Offset: 0x000BA5E8
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

	// Token: 0x040012F5 RID: 4853
	private List<UpdateConsumedMassParameter.Entry> entries = new List<UpdateConsumedMassParameter.Entry>();

	// Token: 0x020013F5 RID: 5109
	private struct Entry
	{
		// Token: 0x04006B40 RID: 27456
		public CreatureCalorieMonitor.Instance creatureCalorieMonitor;

		// Token: 0x04006B41 RID: 27457
		public EventInstance ev;

		// Token: 0x04006B42 RID: 27458
		public PARAMETER_ID parameterId;
	}
}
