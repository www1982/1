using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000537 RID: 1335
[Serializable]
public class RemoteSoundEvent : SoundEvent
{
	// Token: 0x06001D6A RID: 7530 RVA: 0x0009FA65 File Offset: 0x0009DC65
	public RemoteSoundEvent(string file_name, string sound_name, int frame, float min_interval)
		: base(file_name, sound_name, frame, true, false, min_interval, false)
	{
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x0009FA78 File Offset: 0x0009DC78
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		Vector3 vector = behaviour.position;
		vector.z = 0f;
		if (SoundEvent.ObjectIsSelectedAndVisible(behaviour.controller.gameObject))
		{
			vector = SoundEvent.AudioHighlightListenerPosition(vector);
		}
		Workable workable = behaviour.GetComponent<WorkerBase>().GetWorkable();
		if (workable != null)
		{
			Toggleable component = workable.GetComponent<Toggleable>();
			if (component != null)
			{
				IToggleHandler toggleHandlerForWorker = component.GetToggleHandlerForWorker(behaviour.GetComponent<WorkerBase>());
				float num = 1f;
				if (toggleHandlerForWorker != null && toggleHandlerForWorker.IsHandlerOn())
				{
					num = 0f;
				}
				if (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, base.sound, base.soundHash, base.looping, this.isDynamic))
				{
					EventInstance eventInstance = SoundEvent.BeginOneShot(base.sound, vector, SoundEvent.GetVolume(base.objectIsSelectedAndVisible), false);
					eventInstance.setParameterByName("State", num, false);
					SoundEvent.EndOneShot(eventInstance);
				}
			}
		}
	}

	// Token: 0x04001132 RID: 4402
	private const string STATE_PARAMETER = "State";
}
