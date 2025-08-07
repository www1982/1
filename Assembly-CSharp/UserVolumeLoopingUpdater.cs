using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000546 RID: 1350
internal abstract class UserVolumeLoopingUpdater : LoopingSoundParameterUpdater
{
	// Token: 0x06001DFD RID: 7677 RVA: 0x000A2BB8 File Offset: 0x000A0DB8
	public UserVolumeLoopingUpdater(string parameter, string player_pref)
		: base(parameter)
	{
		this.playerPref = player_pref;
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x000A2BD8 File Offset: 0x000A0DD8
	public override void Add(LoopingSoundParameterUpdater.Sound sound)
	{
		UserVolumeLoopingUpdater.Entry entry = new UserVolumeLoopingUpdater.Entry
		{
			ev = sound.ev,
			parameterId = sound.description.GetParameterId(base.parameter)
		};
		this.entries.Add(entry);
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x000A2C24 File Offset: 0x000A0E24
	public override void Update(float dt)
	{
		if (string.IsNullOrEmpty(this.playerPref))
		{
			return;
		}
		float @float = KPlayerPrefs.GetFloat(this.playerPref);
		foreach (UserVolumeLoopingUpdater.Entry entry in this.entries)
		{
			EventInstance ev = entry.ev;
			ev.setParameterByID(entry.parameterId, @float, false);
		}
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x000A2CA4 File Offset: 0x000A0EA4
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

	// Token: 0x0400117D RID: 4477
	private List<UserVolumeLoopingUpdater.Entry> entries = new List<UserVolumeLoopingUpdater.Entry>();

	// Token: 0x0400117E RID: 4478
	private string playerPref;

	// Token: 0x0200139C RID: 5020
	private struct Entry
	{
		// Token: 0x040069FA RID: 27130
		public EventInstance ev;

		// Token: 0x040069FB RID: 27131
		public PARAMETER_ID parameterId;
	}
}
