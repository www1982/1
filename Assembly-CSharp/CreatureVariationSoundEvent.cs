using System;

// Token: 0x02000522 RID: 1314
public class CreatureVariationSoundEvent : SoundEvent
{
	// Token: 0x06001C33 RID: 7219 RVA: 0x0009947B File Offset: 0x0009767B
	public CreatureVariationSoundEvent(string file_name, string sound_name, int frame, bool do_load, bool is_looping, float min_interval, bool is_dynamic)
		: base(file_name, sound_name, frame, do_load, is_looping, min_interval, is_dynamic)
	{
	}

	// Token: 0x06001C34 RID: 7220 RVA: 0x00099490 File Offset: 0x00097690
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		string text = base.sound;
		CreatureBrain component = behaviour.GetComponent<CreatureBrain>();
		if (component != null && !string.IsNullOrEmpty(component.symbolPrefix))
		{
			string sound = GlobalAssets.GetSound(StringFormatter.Combine(component.symbolPrefix, base.name), false);
			if (!string.IsNullOrEmpty(sound))
			{
				text = sound;
			}
		}
		base.PlaySound(behaviour, text);
	}
}
