using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000B56 RID: 2902
internal class UpdateRocketSpeedParameter : LoopingSoundParameterUpdater
{
	// Token: 0x0600566B RID: 22123 RVA: 0x001F4FD0 File Offset: 0x001F31D0
	public UpdateRocketSpeedParameter()
		: base("rocketSpeed")
	{
	}

	// Token: 0x0600566C RID: 22124 RVA: 0x001F4FF0 File Offset: 0x001F31F0
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateRocketSpeedParameter.Entry entry = new UpdateRocketSpeedParameter.Entry
		{
			rocketModule = sound.transform.GetComponent<RocketModule>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x0600566D RID: 22125 RVA: 0x001F504C File Offset: 0x001F324C
	public override void Update(float dt)
	{
		foreach (UpdateRocketSpeedParameter.Entry entry in this.entries)
		{
			if (!(entry.rocketModule == null))
			{
				LaunchConditionManager conditionManager = entry.rocketModule.conditionManager;
				if (!(conditionManager == null))
				{
					ILaunchableRocket component = conditionManager.GetComponent<ILaunchableRocket>();
					if (component != null)
					{
						EventInstance ev = entry.ev;
						ev.setParameterByID(entry.parameterId, component.rocketSpeed, false);
					}
				}
			}
		}
	}

	// Token: 0x0600566E RID: 22126 RVA: 0x001F50E4 File Offset: 0x001F32E4
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

	// Token: 0x040039C8 RID: 14792
	private List<UpdateRocketSpeedParameter.Entry> entries = new List<UpdateRocketSpeedParameter.Entry>();

	// Token: 0x02001C7B RID: 7291
	private struct Entry
	{
		// Token: 0x0400867A RID: 34426
		public RocketModule rocketModule;

		// Token: 0x0400867B RID: 34427
		public EventInstance ev;

		// Token: 0x0400867C RID: 34428
		public PARAMETER_ID parameterId;
	}
}
