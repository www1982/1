using System;
using FMOD.Studio;
using Klei.AI;
using UnityEngine;

// Token: 0x02000541 RID: 1345
public class VoiceSoundEvent : SoundEvent
{
	// Token: 0x06001DC0 RID: 7616 RVA: 0x000A1499 File Offset: 0x0009F699
	public VoiceSoundEvent(string file_name, string sound_name, int frame, bool is_looping)
		: base(file_name, sound_name, frame, false, is_looping, (float)SoundEvent.IGNORE_INTERVAL, true)
	{
		base.noiseValues = SoundEventVolumeCache.instance.GetVolume("VoiceSoundEvent", sound_name);
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x000A14CF File Offset: 0x0009F6CF
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		VoiceSoundEvent.PlayVoice(base.name, behaviour.controller, this.intervalBetweenSpeaking, base.looping, false);
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x000A14F0 File Offset: 0x0009F6F0
	public static EventInstance PlayVoice(string name, KBatchedAnimController controller, float interval_between_speaking, bool looping, bool objectIsSelectedAndVisible = false)
	{
		EventInstance eventInstance = default(EventInstance);
		MinionIdentity component = controller.GetComponent<MinionIdentity>();
		if (component == null || (name.Contains("state") && Time.time - component.timeLastSpoke < interval_between_speaking))
		{
			return eventInstance;
		}
		bool flag = component.model == BionicMinionConfig.MODEL;
		if (name.Contains(":"))
		{
			float num = float.Parse(name.Split(':', StringSplitOptions.None)[1]);
			if ((float)global::UnityEngine.Random.Range(0, 100) > num)
			{
				return eventInstance;
			}
		}
		WorkerBase component2 = controller.GetComponent<WorkerBase>();
		string assetName = VoiceSoundEvent.GetAssetName(name, component2);
		StaminaMonitor.Instance smi = component2.GetSMI<StaminaMonitor.Instance>();
		if (!name.Contains("sleep_") && smi != null && smi.IsSleeping())
		{
			return eventInstance;
		}
		Vector3 vector = component2.transform.GetPosition();
		vector.z = 0f;
		if (SoundEvent.ObjectIsSelectedAndVisible(controller.gameObject))
		{
			vector = SoundEvent.AudioHighlightListenerPosition(vector);
		}
		string sound = GlobalAssets.GetSound(assetName, true);
		if (!SoundEvent.ShouldPlaySound(controller, sound, looping, false))
		{
			return eventInstance;
		}
		if (sound != null)
		{
			if (looping)
			{
				LoopingSounds component3 = controller.GetComponent<LoopingSounds>();
				if (component3 == null)
				{
					global::Debug.Log(controller.name + " is missing LoopingSounds component. ");
				}
				else if (!component3.StartSound(sound))
				{
					DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", sound, controller.name) });
				}
				else
				{
					component3.UpdateFirstParameter(sound, "isBionic", (float)(flag ? 1 : 0));
				}
			}
			else
			{
				eventInstance = SoundEvent.BeginOneShot(sound, vector, 1f, false);
				eventInstance.setParameterByName("isBionic", (float)(flag ? 1 : 0), false);
				if (sound.Contains("sleep_") && controller.GetComponent<Traits>().HasTrait("Snorer"))
				{
					eventInstance.setParameterByName("snoring", 1f, false);
				}
				SoundEvent.EndOneShot(eventInstance);
				component.timeLastSpoke = Time.time;
			}
		}
		else if (AudioDebug.Get().debugVoiceSounds)
		{
			global::Debug.LogWarning("Missing voice sound: " + assetName);
		}
		return eventInstance;
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x000A1700 File Offset: 0x0009F900
	private static string GetAssetName(string name, Component cmp)
	{
		string text = "F01";
		if (cmp != null)
		{
			MinionIdentity component = cmp.GetComponent<MinionIdentity>();
			if (component != null)
			{
				text = component.GetVoiceId();
			}
		}
		string text2 = name;
		if (name.Contains(":"))
		{
			text2 = name.Split(':', StringSplitOptions.None)[0];
		}
		return StringFormatter.Combine("DupVoc_", text, "_", text2);
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x000A1760 File Offset: 0x0009F960
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null)
			{
				string sound = GlobalAssets.GetSound(VoiceSoundEvent.GetAssetName(base.name, component), true);
				component.StopSound(sound);
			}
		}
	}

	// Token: 0x0400114E RID: 4430
	public static float locomotionSoundProb = 50f;

	// Token: 0x0400114F RID: 4431
	public float timeLastSpoke;

	// Token: 0x04001150 RID: 4432
	public float intervalBetweenSpeaking = 10f;
}
