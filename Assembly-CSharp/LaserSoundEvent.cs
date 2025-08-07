using System;

// Token: 0x02000531 RID: 1329
public class LaserSoundEvent : SoundEvent
{
	// Token: 0x06001D5E RID: 7518 RVA: 0x0009F61F File Offset: 0x0009D81F
	public LaserSoundEvent(string file_name, string sound_name, int frame, float min_interval)
		: base(file_name, sound_name, frame, true, true, min_interval, false)
	{
		base.noiseValues = SoundEventVolumeCache.instance.GetVolume("LaserSoundEvent", sound_name);
	}
}
