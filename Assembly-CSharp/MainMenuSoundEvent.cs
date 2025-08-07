using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000532 RID: 1330
public class MainMenuSoundEvent : SoundEvent
{
	// Token: 0x06001D5F RID: 7519 RVA: 0x0009F645 File Offset: 0x0009D845
	public MainMenuSoundEvent(string file_name, string sound_name, int frame)
		: base(file_name, sound_name, frame, true, false, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
	}

	// Token: 0x06001D60 RID: 7520 RVA: 0x0009F65C File Offset: 0x0009D85C
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		EventInstance eventInstance = KFMOD.BeginOneShot(base.sound, Vector3.zero, 1f);
		if (eventInstance.isValid())
		{
			eventInstance.setParameterByName("frame", (float)base.frame, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}
}
