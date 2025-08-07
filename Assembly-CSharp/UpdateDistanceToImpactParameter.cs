using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000834 RID: 2100
internal class UpdateDistanceToImpactParameter : LoopingSoundParameterUpdater
{
	// Token: 0x06003991 RID: 14737 RVA: 0x0013FBA9 File Offset: 0x0013DDA9
	public UpdateDistanceToImpactParameter()
		: base("distanceToImpact")
	{
	}

	// Token: 0x06003992 RID: 14738 RVA: 0x0013FBC8 File Offset: 0x0013DDC8
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateDistanceToImpactParameter.Entry entry = new UpdateDistanceToImpactParameter.Entry
		{
			comet = sound.transform.GetComponent<Comet>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x06003993 RID: 14739 RVA: 0x0013FC24 File Offset: 0x0013DE24
	public override void Update(float dt)
	{
		foreach (UpdateDistanceToImpactParameter.Entry entry in this.entries)
		{
			if (!(entry.comet == null))
			{
				float soundDistance = entry.comet.GetSoundDistance();
				EventInstance ev = entry.ev;
				ev.setParameterByID(entry.parameterId, soundDistance, false);
			}
		}
	}

	// Token: 0x06003994 RID: 14740 RVA: 0x0013FCA4 File Offset: 0x0013DEA4
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

	// Token: 0x040022EC RID: 8940
	private List<UpdateDistanceToImpactParameter.Entry> entries = new List<UpdateDistanceToImpactParameter.Entry>();

	// Token: 0x020017A6 RID: 6054
	private struct Entry
	{
		// Token: 0x0400766B RID: 30315
		public Comet comet;

		// Token: 0x0400766C RID: 30316
		public EventInstance ev;

		// Token: 0x0400766D RID: 30317
		public PARAMETER_ID parameterId;
	}
}
