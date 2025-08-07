using System;

// Token: 0x0200053F RID: 1343
public class UIAnimationSoundEvent : SoundEvent
{
	// Token: 0x06001DB8 RID: 7608 RVA: 0x000A11B0 File Offset: 0x0009F3B0
	public UIAnimationSoundEvent(string file_name, string sound_name, int frame, bool looping)
		: base(file_name, sound_name, frame, true, looping, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x000A11C5 File Offset: 0x0009F3C5
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		this.PlaySound(behaviour);
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x000A11D0 File Offset: 0x0009F3D0
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component == null)
			{
				Debug.Log(behaviour.name + " (UI Object) is missing LoopingSounds component.");
				return;
			}
			if (!component.StartSound(base.sound, false, false, false))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", base.sound, behaviour.name) });
				return;
			}
		}
		else
		{
			try
			{
				if (SoundListenerController.Instance == null)
				{
					KFMOD.PlayUISound(base.sound);
				}
				else
				{
					KFMOD.PlayOneShot(base.sound, SoundListenerController.Instance.transform.GetPosition(), 1f);
				}
			}
			catch
			{
				DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + base.sound + "]" });
			}
		}
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x000A12BC File Offset: 0x0009F4BC
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null)
			{
				component.StopSound(base.sound);
			}
		}
	}
}
