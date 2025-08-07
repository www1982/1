using System;

// Token: 0x0200054B RID: 1355
internal abstract class UserVolumeOneShotUpdater : OneShotSoundParameterUpdater
{
	// Token: 0x06001E05 RID: 7685 RVA: 0x000A2D44 File Offset: 0x000A0F44
	public UserVolumeOneShotUpdater(string parameter, string player_pref)
		: base(parameter)
	{
		this.playerPref = player_pref;
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x000A2D5C File Offset: 0x000A0F5C
	public override void Play(OneShotSoundParameterUpdater.Sound sound)
	{
		if (!string.IsNullOrEmpty(this.playerPref))
		{
			float @float = KPlayerPrefs.GetFloat(this.playerPref);
			sound.ev.setParameterByID(sound.description.GetParameterId(base.parameter), @float, false);
		}
	}

	// Token: 0x0400117F RID: 4479
	private string playerPref;
}
