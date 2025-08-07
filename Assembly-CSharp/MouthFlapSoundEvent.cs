using System;

// Token: 0x02000533 RID: 1331
public class MouthFlapSoundEvent : SoundEvent
{
	// Token: 0x06001D61 RID: 7521 RVA: 0x0009F6A4 File Offset: 0x0009D8A4
	public MouthFlapSoundEvent(string file_name, string sound_name, int frame, bool is_looping)
		: base(file_name, sound_name, frame, false, is_looping, (float)SoundEvent.IGNORE_INTERVAL, true)
	{
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x0009F6B9 File Offset: 0x0009D8B9
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		behaviour.controller.GetSMI<SpeechMonitor.Instance>().PlaySpeech(base.name, null);
	}
}
