using System;

// Token: 0x02000E57 RID: 3671
public class SpeedOneShotUpdater : OneShotSoundParameterUpdater
{
	// Token: 0x060074E6 RID: 29926 RVA: 0x002CA039 File Offset: 0x002C8239
	public SpeedOneShotUpdater()
		: base("Speed")
	{
	}

	// Token: 0x060074E7 RID: 29927 RVA: 0x002CA04B File Offset: 0x002C824B
	public override void Play(OneShotSoundParameterUpdater.Sound sound)
	{
		sound.ev.setParameterByID(sound.description.GetParameterId(base.parameter), SpeedLoopingSoundUpdater.GetSpeedParameterValue(), false);
	}
}
