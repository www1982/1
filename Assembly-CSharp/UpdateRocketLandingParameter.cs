using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000B55 RID: 2901
internal class UpdateRocketLandingParameter : LoopingSoundParameterUpdater
{
	// Token: 0x06005667 RID: 22119 RVA: 0x001F4E43 File Offset: 0x001F3043
	public UpdateRocketLandingParameter()
		: base("rocketLanding")
	{
	}

	// Token: 0x06005668 RID: 22120 RVA: 0x001F4E60 File Offset: 0x001F3060
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UpdateRocketLandingParameter.Entry entry = new UpdateRocketLandingParameter.Entry
		{
			rocketModule = sound.transform.GetComponent<RocketModule>(),
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x06005669 RID: 22121 RVA: 0x001F4EBC File Offset: 0x001F30BC
	public override void Update(float dt)
	{
		foreach (UpdateRocketLandingParameter.Entry entry in this.entries)
		{
			if (!(entry.rocketModule == null))
			{
				LaunchConditionManager conditionManager = entry.rocketModule.conditionManager;
				if (!(conditionManager == null))
				{
					ILaunchableRocket component = conditionManager.GetComponent<ILaunchableRocket>();
					if (component != null)
					{
						if (component.isLanding)
						{
							EventInstance eventInstance = entry.ev;
							eventInstance.setParameterByID(entry.parameterId, 1f, false);
						}
						else
						{
							EventInstance eventInstance = entry.ev;
							eventInstance.setParameterByID(entry.parameterId, 0f, false);
						}
					}
				}
			}
		}
	}

	// Token: 0x0600566A RID: 22122 RVA: 0x001F4F78 File Offset: 0x001F3178
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

	// Token: 0x040039C7 RID: 14791
	private List<UpdateRocketLandingParameter.Entry> entries = new List<UpdateRocketLandingParameter.Entry>();

	// Token: 0x02001C7A RID: 7290
	private struct Entry
	{
		// Token: 0x04008677 RID: 34423
		public RocketModule rocketModule;

		// Token: 0x04008678 RID: 34424
		public EventInstance ev;

		// Token: 0x04008679 RID: 34425
		public PARAMETER_ID parameterId;
	}
}
