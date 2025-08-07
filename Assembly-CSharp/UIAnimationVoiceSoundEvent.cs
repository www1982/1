using System;
using UnityEngine;

// Token: 0x02000540 RID: 1344
public class UIAnimationVoiceSoundEvent : SoundEvent
{
	// Token: 0x06001DBC RID: 7612 RVA: 0x000A12EE File Offset: 0x0009F4EE
	public UIAnimationVoiceSoundEvent(string file_name, string sound_name, int frame, bool looping)
		: base(file_name, sound_name, frame, false, looping, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
		this.actualSoundName = sound_name;
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x000A130A File Offset: 0x0009F50A
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		this.PlaySound(behaviour);
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x000A1314 File Offset: 0x0009F514
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		string soundPath = MinionVoice.ByObject(behaviour.controller).UnwrapOr(MinionVoice.Random(), string.Format("Couldn't find MinionVoice on UI {0}, falling back to random voice", behaviour.controller)).GetSoundPath(this.actualSoundName);
		if (this.actualSoundName.Contains(":"))
		{
			float num = float.Parse(this.actualSoundName.Split(':', StringSplitOptions.None)[1]);
			if ((float)global::UnityEngine.Random.Range(0, 100) > num)
			{
				return;
			}
		}
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component == null)
			{
				global::Debug.Log(behaviour.name + " (UI Object) is missing LoopingSounds component.");
			}
			else if (!component.StartSound(soundPath, false, false, false))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", soundPath, behaviour.name) });
			}
			this.lastPlayedLoopingSoundPath = soundPath;
			return;
		}
		try
		{
			if (SoundListenerController.Instance == null)
			{
				KFMOD.PlayUISound(soundPath);
			}
			else
			{
				KFMOD.PlayOneShot(soundPath, SoundListenerController.Instance.transform.GetPosition(), 1f);
			}
		}
		catch
		{
			DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + soundPath + "]" });
		}
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x000A1458 File Offset: 0x0009F658
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null && this.lastPlayedLoopingSoundPath != null)
			{
				component.StopSound(this.lastPlayedLoopingSoundPath);
			}
		}
		this.lastPlayedLoopingSoundPath = null;
	}

	// Token: 0x0400114C RID: 4428
	private string actualSoundName;

	// Token: 0x0400114D RID: 4429
	private string lastPlayedLoopingSoundPath;
}
